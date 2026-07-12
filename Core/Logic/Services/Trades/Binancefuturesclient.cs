using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Binance USDT-M Futures client. VERIFY ON TESTNET before any live key.
    ///
    /// Note (Binance change 2025-12-09): conditional orders (STOP_MARKET / TAKE_PROFIT_MARKET, and
    /// therefore our "Conditional" entries and all SL/TP) MUST go to /fapi/v1/algoOrder now — the old
    /// /fapi/v1/order returns -4120 for them. Market/Limit entries still use /fapi/v1/order.
    /// </summary>
    public sealed class BinanceFuturesClient : IExchangeClient, IDisposable
    {
        private const string LiveBase = "https://fapi.binance.com";
        private const string TestnetBase = "https://testnet.binancefuture.com";

        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        private static readonly ConcurrentDictionary<string, SymbolRules> RulesCache = new();

        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly string _baseUrl;

        public string Exchange => "Binance";

        public BinanceFuturesClient(string apiKey, string apiSecret, bool useTestnet)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _baseUrl = useTestnet ? TestnetBase : LiveBase;
        }

        // ----------------------------------------------------------------- Reads
        public async Task<AccountBalance> GetBalanceAsync()
        {
            using var doc = await SignedRequestAsync(HttpMethod.Get, "/fapi/v2/balance").ConfigureAwait(false);
            foreach (var e in doc.RootElement.EnumerateArray())
                if (e.GetProperty("asset").GetString() == "USDT")
                    return new AccountBalance { WalletUsdt = Dec(e, "balance"), AvailableUsdt = Dec(e, "availableBalance") };
            return new AccountBalance();
        }

        public async Task<TickerPrice> GetPriceAsync(string symbol)
        {
            using var doc = await PublicGetAsync("/fapi/v1/ticker/price", $"symbol={symbol}").ConfigureAwait(false);
            var r = doc.RootElement;
            return new TickerPrice { Symbol = r.GetProperty("symbol").GetString() ?? symbol, Price = Dec(r, "price") };
        }

        public async Task<IReadOnlyList<string>> GetSymbolsAsync()
        {
            using var doc = await PublicGetAsync("/fapi/v1/exchangeInfo").ConfigureAwait(false);
            var list = new List<string>();
            foreach (var s in doc.RootElement.GetProperty("symbols").EnumerateArray())
            {
                if (s.GetProperty("status").GetString() == "TRADING" &&
                    s.GetProperty("quoteAsset").GetString() == "USDT" &&
                    s.GetProperty("contractType").GetString() == "PERPETUAL")
                {
                    var name = s.GetProperty("symbol").GetString();
                    if (!string.IsNullOrEmpty(name)) list.Add(name);
                }
            }
            list.Sort(StringComparer.Ordinal);
            return list;
        }

        public async Task<SymbolRules> GetSymbolRulesAsync(string symbol)
        {
            if (RulesCache.TryGetValue(symbol, out var cached)) return cached;

            using var doc = await PublicGetAsync("/fapi/v1/exchangeInfo", $"symbol={symbol}").ConfigureAwait(false);
            foreach (var s in doc.RootElement.GetProperty("symbols").EnumerateArray())
            {
                if (s.GetProperty("symbol").GetString() != symbol) continue;
                var rules = new SymbolRules { Symbol = symbol };
                foreach (var f in s.GetProperty("filters").EnumerateArray())
                {
                    switch (f.GetProperty("filterType").GetString())
                    {
                        case "LOT_SIZE": rules.StepSize = Dec(f, "stepSize"); break;
                        case "PRICE_FILTER": rules.TickSize = Dec(f, "tickSize"); break;
                        case "MIN_NOTIONAL":
                            if (f.TryGetProperty("notional", out var n) && n.GetString() is string ns)
                                rules.MinNotional = decimal.Parse(ns, CultureInfo.InvariantCulture);
                            break;
                    }
                }
                RulesCache[symbol] = rules;
                return rules;
            }
            throw new Exception($"Symbol {symbol} not found on the exchange.");
        }

        public async Task<IReadOnlyList<PositionInfo>> GetPositionsAsync()
        {
            using var doc = await SignedRequestAsync(HttpMethod.Get, "/fapi/v2/positionRisk").ConfigureAwait(false);
            var list = new List<PositionInfo>();
            foreach (var p in doc.RootElement.EnumerateArray())
            {
                decimal amt = Dec(p, "positionAmt");
                if (amt == 0m) continue;

                decimal lev = Dec(p, "leverage");
                bool isolated = string.Equals(p.GetProperty("marginType").GetString(), "isolated", StringComparison.OrdinalIgnoreCase);
                decimal notional = Math.Abs(Dec(p, "notional"));
                decimal margin = isolated ? Dec(p, "isolatedMargin") : (lev > 0 ? notional / lev : 0m);

                list.Add(new PositionInfo
                {
                    Symbol = p.GetProperty("symbol").GetString() ?? string.Empty,
                    Side = amt > 0 ? OrderSide.Buy : OrderSide.Sell,
                    Quantity = Math.Abs(amt),
                    EntryPrice = Dec(p, "entryPrice"),
                    MarkPrice = Dec(p, "markPrice"),
                    UnrealizedPnl = Dec(p, "unRealizedProfit"),
                    LiquidationPrice = Dec(p, "liquidationPrice"),
                    Margin = margin,
                    Leverage = lev,
                    MarginMode = isolated ? MarginMode.Isolated : MarginMode.Cross,
                    StopLoss = null,
                    TakeProfit = null
                });
            }
            return list;
        }

        // ----------------------------------------------------------------- Writes
        public async Task SetLeverageAsync(string symbol, int leverage, MarginMode mode)
        {
            try
            {
                var mt = mode == MarginMode.Isolated ? "ISOLATED" : "CROSSED";
                using var _ = await SignedRequestAsync(HttpMethod.Post, "/fapi/v1/marginType",
                    $"symbol={symbol}&marginType={mt}").ConfigureAwait(false);
            }
            catch (Exception ex) when (ex.Message.Contains("No need to change margin type") || ex.Message.Contains("-4046"))
            {
                // already the requested mode — ignore
            }

            using var __ = await SignedRequestAsync(HttpMethod.Post, "/fapi/v1/leverage",
                $"symbol={symbol}&leverage={leverage}").ConfigureAwait(false);
        }

        public async Task<OrderResult> PlaceOrderAsync(OrderRequest req)
        {
            string entryId;

            if (req.Kind == OrderKind.Conditional)
            {
                // Conditional entry -> algo endpoint, with a quantity (opens a position on trigger).
                entryId = await PlaceAlgoConditionalAsync(req.Symbol, req.Side == OrderSide.Buy ? "BUY" : "SELL",
                    "STOP_MARKET", req.TriggerPrice ?? 0m, quantity: req.Quantity, closePosition: false).ConfigureAwait(false);
            }
            else
            {
                var p = new List<string> { $"symbol={req.Symbol}", $"side={(req.Side == OrderSide.Buy ? "BUY" : "SELL")}" };
                if (req.Kind == OrderKind.Limit)
                {
                    p.Add("type=LIMIT"); p.Add("timeInForce=GTC");
                    p.Add($"quantity={Num(req.Quantity)}"); p.Add($"price={Num(req.Price ?? 0m)}");
                }
                else
                {
                    p.Add("type=MARKET"); p.Add($"quantity={Num(req.Quantity)}");
                }
                if (req.ReduceOnly) p.Add("reduceOnly=true");

                using var doc = await SignedRequestAsync(HttpMethod.Post, "/fapi/v1/order", string.Join("&", p)).ConfigureAwait(false);
                entryId = doc.RootElement.GetProperty("orderId").GetRawText();
            }

            // Protective SL/TP -> algo endpoint, non-fatal (a failure must not hide the open entry).
            var warnings = new List<string>();
            var protectSide = req.Side == OrderSide.Buy ? "SELL" : "BUY";
            if (req.StopLoss.HasValue)
                try { await PlaceAlgoConditionalAsync(req.Symbol, protectSide, "STOP_MARKET", req.StopLoss.Value, null, true).ConfigureAwait(false); }
                catch (Exception ex) { warnings.Add($"SL not set ({ex.Message})"); }
            if (req.TakeProfit.HasValue)
                try { await PlaceAlgoConditionalAsync(req.Symbol, protectSide, "TAKE_PROFIT_MARKET", req.TakeProfit.Value, null, true).ConfigureAwait(false); }
                catch (Exception ex) { warnings.Add($"TP not set ({ex.Message})"); }

            var msg = warnings.Count == 0
                ? "Order placed."
                : "Entry placed, but " + string.Join("; ", warnings) + ". Set them on the exchange.";
            return new OrderResult { Success = true, OrderId = entryId, Message = msg };
        }

        /// <summary>Places a conditional order via the new Algo endpoint (required since 2025-12-09).</summary>
        private async Task<string> PlaceAlgoConditionalAsync(string symbol, string side, string orderType,
            decimal triggerPrice, decimal? quantity, bool closePosition)
        {
            var p = new List<string>
            {
                "algoType=CONDITIONAL",
                $"symbol={symbol}",
                $"side={side}",
                $"orderType={orderType}",
                $"triggerPrice={Num(triggerPrice)}"
            };
            if (closePosition) p.Add("closePosition=true");
            else if (quantity.HasValue) p.Add($"quantity={Num(quantity.Value)}");

            using var doc = await SignedRequestAsync(HttpMethod.Post, "/fapi/v1/algoOrder", string.Join("&", p)).ConfigureAwait(false);
            var root = doc.RootElement;
            if (root.TryGetProperty("algoId", out var a)) return a.GetRawText();
            if (root.TryGetProperty("orderId", out var o)) return o.GetRawText();
            return string.Empty;
        }

        public async Task<OrderResult> ClosePositionAsync(string symbol, decimal? quantity)
        {
            using var pos = await SignedRequestAsync(HttpMethod.Get, "/fapi/v2/positionRisk", $"symbol={symbol}").ConfigureAwait(false);
            decimal amt = 0m;
            foreach (var e in pos.RootElement.EnumerateArray()) { amt = Dec(e, "positionAmt"); break; }
            if (amt == 0m) return OrderResult.Fail("No open position to close.");

            var side = amt > 0 ? "SELL" : "BUY";
            decimal qty = quantity ?? Math.Abs(amt);
            var p = $"symbol={symbol}&side={side}&type=MARKET&quantity={Num(qty)}&reduceOnly=true";

            using var doc = await SignedRequestAsync(HttpMethod.Post, "/fapi/v1/order", p).ConfigureAwait(false);
            return OrderResult.Ok(doc.RootElement.GetProperty("orderId").GetRawText());
        }

        public Task<OrderResult> UpdateStopTakeAsync(string symbol, decimal? stopLoss, decimal? takeProfit)
            => throw new NotImplementedException("Mid-trade TP/SL editing is the next step (cancel + replace algo orders).");

        public Task<IReadOnlyList<ClosedTrade>> GetRecentClosedTradesAsync(DateTime sinceUtc)
            => throw new NotImplementedException("Reconcile needs fills aggregation - a later focused step.");

        // ----------------------------------------------------------------- HTTP
        private async Task<JsonDocument> SignedRequestAsync(HttpMethod method, string path, string query = "")
        {
            long ts = BinanceSignature.Timestamp();
            string q = string.IsNullOrEmpty(query) ? $"timestamp={ts}&recvWindow=5000"
                                                   : $"{query}&timestamp={ts}&recvWindow=5000";
            string signature = BinanceSignature.Sign(q, _apiSecret);
            string url = $"{_baseUrl}{path}?{q}&signature={signature}";

            using var req = new HttpRequestMessage(method, url);
            req.Headers.Add("X-MBX-APIKEY", _apiKey);

            var resp = await Http.SendAsync(req).ConfigureAwait(false);
            var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode) throw new Exception(DescribeError(body, resp.StatusCode));
            return JsonDocument.Parse(body);
        }

        private async Task<JsonDocument> PublicGetAsync(string path, string query = "")
        {
            string url = string.IsNullOrEmpty(query) ? $"{_baseUrl}{path}" : $"{_baseUrl}{path}?{query}";
            var resp = await Http.GetAsync(url).ConfigureAwait(false);
            var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode) throw new Exception(DescribeError(body, resp.StatusCode));
            return JsonDocument.Parse(body);
        }

        private static string DescribeError(string body, HttpStatusCode status)
        {
            try
            {
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("msg", out var msg))
                {
                    var code = doc.RootElement.TryGetProperty("code", out var c) ? c.GetRawText() : "?";
                    return $"Binance: {msg.GetString()} (code {code})";
                }
            }
            catch { }
            return $"Binance request failed (HTTP {(int)status}).";
        }

        private static decimal Dec(JsonElement e, string prop)
        {
            if (!e.TryGetProperty(prop, out var v)) return 0m;
            var s = v.GetString();
            return string.IsNullOrEmpty(s) ? 0m : decimal.Parse(s, CultureInfo.InvariantCulture);
        }

        private static string Num(decimal v) => v.ToString("0.########", CultureInfo.InvariantCulture);

        public void Dispose() { }
    }
}