using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Bybit V5 client, USDT perpetuals (category "linear"), Unified Trading Account.
    ///
    /// ⚠ IMPORTANT — this client is written to the published Bybit V5 spec but has NOT been executed
    /// against the live or testnet API from here. Treat it as a starting implementation:
    ///   1. Test EVERY method on Bybit TESTNET first (create a testnet key). Defaults use testnet.
    ///   2. The read path (balance/price/symbols/positions/closed trades) is lowest risk. The WRITE
    ///      path (leverage, order, close) moves real money — verify order params, precision, margin-mode
    ///      switching, and the long/short direction mapping in closed-pnl before pointing it at a live key.
    ///   3. Assumes one-way position mode (positionIdx 0), matching the rest of the app.
    ///
    /// Endpoints most likely to need per-account tweaks are flagged inline (balance fields, margin mode,
    /// risk-limit tiers, closed-pnl side).
    /// </summary>
    public sealed class BybitFuturesClient : IExchangeClient, IDisposable
    {
        private const string LiveBase = "https://api.bybit.com";
        private const string TestnetBase = "https://api-testnet.bybit.com";
        private const string Category = "linear";
        private const string RecvWindow = "5000";

        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        private static readonly ConcurrentDictionary<string, SymbolRules> RulesCache = new();
        private readonly ConcurrentDictionary<string, LeverageBracket> _bracketCache = new();

        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly string _baseUrl;

        public string Exchange => "Bybit";

        public BybitFuturesClient(string apiKey, string apiSecret, bool useTestnet)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _baseUrl = useTestnet ? TestnetBase : LiveBase;
        }

        // ----------------------------------------------------------------- Reads
        public async Task<AccountBalance> GetBalanceAsync()
        {
            using var doc = await SignedGetAsync("/v5/account/wallet-balance", "accountType=UNIFIED").ConfigureAwait(false);
            var list = Result(doc).GetProperty("list");
            if (list.GetArrayLength() == 0) return new AccountBalance();

            var acct = list[0];
            // Account-level totals are the reliable figures for a USDT-margined unified account.
            decimal wallet = DecP(acct, "totalWalletBalance");
            decimal available = DecP(acct, "totalAvailableBalance");

            // Fall back to the USDT coin row if account-level available is blank (some account types).
            if (available == 0m && acct.TryGetProperty("coin", out var coins))
                foreach (var c in coins.EnumerateArray())
                    if (c.GetProperty("coin").GetString() == "USDT")
                    {
                        if (wallet == 0m) wallet = DecP(c, "walletBalance");
                        available = FirstNonZero(DecP(c, "availableToWithdraw"), DecP(c, "walletBalance"));
                        break;
                    }

            return new AccountBalance { WalletUsdt = wallet, AvailableUsdt = available };
        }

        public async Task<TickerPrice> GetPriceAsync(string symbol)
        {
            using var doc = await PublicGetAsync("/v5/market/tickers", $"category={Category}&symbol={symbol}").ConfigureAwait(false);
            var row = Result(doc).GetProperty("list")[0];
            return new TickerPrice { Symbol = symbol, Price = DecP(row, "lastPrice") };
        }

        public async Task<IReadOnlyList<string>> GetSymbolsAsync()
        {
            using var doc = await PublicGetAsync("/v5/market/instruments-info", $"category={Category}&limit=1000").ConfigureAwait(false);
            var list = new List<string>();
            foreach (var s in Result(doc).GetProperty("list").EnumerateArray())
            {
                if (s.GetProperty("status").GetString() == "Trading" &&
                    s.GetProperty("quoteCoin").GetString() == "USDT")
                {
                    var name = s.GetProperty("symbol").GetString();
                    if (!string.IsNullOrEmpty(name)) list.Add(name);
                }
            }
            list.Sort(StringComparer.Ordinal);
            return list;   // NOTE: 1000-row cap, no cursor pagination. Enough for linear USDT perps today.
        }

        public async Task<SymbolRules> GetSymbolRulesAsync(string symbol)
        {
            if (RulesCache.TryGetValue(symbol, out var cached)) return cached;

            using var doc = await PublicGetAsync("/v5/market/instruments-info", $"category={Category}&symbol={symbol}").ConfigureAwait(false);
            var arr = Result(doc).GetProperty("list");
            if (arr.GetArrayLength() == 0) throw new Exception($"Symbol {symbol} not found on Bybit.");

            var s = arr[0];
            var lot = s.GetProperty("lotSizeFilter");
            var price = s.GetProperty("priceFilter");

            var rules = new SymbolRules
            {
                Symbol = symbol,
                StepSize = DecP(lot, "qtyStep"),
                TickSize = DecP(price, "tickSize"),
                MinNotional = lot.TryGetProperty("minNotionalValue", out var mn) ? Str2Dec(mn.GetString()) : 0m
            };
            RulesCache[symbol] = rules;
            return rules;
        }

        public async Task<LeverageBracket> GetLeverageBracketAsync(string symbol)
        {
            if (_bracketCache.TryGetValue(symbol, out var cached)) return cached;

            using var doc = await PublicGetAsync("/v5/market/risk-limit", $"category={Category}&symbol={symbol}").ConfigureAwait(false);

            var tiers = new List<LeverageTier>();
            decimal prevCap = 0m;
            foreach (var r in Result(doc).GetProperty("list").EnumerateArray())
            {
                decimal cap = DecP(r, "riskLimitValue");       // notional cap for this tier (USDT)
                int maxLev = (int)DecP(r, "maxLeverage");
                tiers.Add(new LeverageTier
                {
                    Bracket = tiers.Count + 1,
                    MaxLeverage = maxLev,
                    NotionalFloor = prevCap,
                    NotionalCap = cap
                });
                prevCap = cap;
            }
            tiers.Sort((a, c) => a.NotionalFloor.CompareTo(c.NotionalFloor));

            var bracket = new LeverageBracket { Symbol = symbol, Tiers = tiers };
            _bracketCache[symbol] = bracket;
            return bracket;
        }

        public async Task<IReadOnlyList<PositionInfo>> GetPositionsAsync()
        {
            using var doc = await SignedGetAsync("/v5/position/list", $"category={Category}&settleCoin=USDT").ConfigureAwait(false);
            var list = new List<PositionInfo>();
            foreach (var p in Result(doc).GetProperty("list").EnumerateArray())
            {
                decimal size = DecP(p, "size");
                if (size == 0m) continue;

                bool isLong = string.Equals(p.GetProperty("side").GetString(), "Buy", StringComparison.OrdinalIgnoreCase);
                bool isolated = DecP(p, "tradeMode") == 1m;   // 0 = cross, 1 = isolated

                list.Add(new PositionInfo
                {
                    Symbol = p.GetProperty("symbol").GetString() ?? string.Empty,
                    Side = isLong ? OrderSide.Buy : OrderSide.Sell,
                    Quantity = size,
                    EntryPrice = DecP(p, "avgPrice"),
                    MarkPrice = DecP(p, "markPrice"),
                    UnrealizedPnl = DecP(p, "unrealisedPnl"),
                    LiquidationPrice = DecP(p, "liqPrice"),
                    Margin = DecP(p, "positionIM"),   // initial margin held for the position
                    Leverage = DecP(p, "leverage"),
                    MarginMode = isolated ? MarginMode.Isolated : MarginMode.Cross,
                    StopLoss = NullableDec(p, "stopLoss"),
                    TakeProfit = NullableDec(p, "takeProfit")
                });
            }
            return list;
        }

        // ----------------------------------------------------------------- Writes
        public async Task SetLeverageAsync(string symbol, int leverage, MarginMode mode)
        {
            var lev = leverage.ToString(CultureInfo.InvariantCulture);

            // Margin mode first (tradeMode 0 = cross, 1 = isolated). Benign if already set.
            try
            {
                await SignedPostAsync("/v5/position/switch-isolated", new Dictionary<string, object>
                {
                    ["category"] = Category,
                    ["symbol"] = symbol,
                    ["tradeMode"] = mode == MarginMode.Isolated ? 1 : 0,
                    ["buyLeverage"] = lev,
                    ["sellLeverage"] = lev
                }).ConfigureAwait(false);
            }
            catch (Exception ex) when (IsBenign(ex)) { /* already in that mode */ }

            try
            {
                await SignedPostAsync("/v5/position/set-leverage", new Dictionary<string, object>
                {
                    ["category"] = Category,
                    ["symbol"] = symbol,
                    ["buyLeverage"] = lev,
                    ["sellLeverage"] = lev
                }).ConfigureAwait(false);
            }
            catch (Exception ex) when (IsBenign(ex)) { /* leverage not modified */ }
        }

        public async Task<OrderResult> PlaceOrderAsync(OrderRequest req)
        {
            var body = new Dictionary<string, object>
            {
                ["category"] = Category,
                ["symbol"] = req.Symbol,
                ["side"] = req.Side == OrderSide.Buy ? "Buy" : "Sell",
                ["qty"] = Num(req.Quantity),
                ["positionIdx"] = 0
            };

            if (req.Kind == OrderKind.Limit)
            {
                body["orderType"] = "Limit";
                body["price"] = Num(req.Price ?? 0m);
                body["timeInForce"] = "GTC";
            }
            else if (req.Kind == OrderKind.Conditional)
            {
                // Conditional (stop) entry: trigger direction 1 = triggers on rise, 2 = on fall.
                body["orderType"] = "Market";
                body["triggerPrice"] = Num(req.TriggerPrice ?? 0m);
                body["triggerDirection"] = req.Side == OrderSide.Buy ? 1 : 2;   // verify vs your intended trigger side
            }
            else
            {
                body["orderType"] = "Market";
            }

            if (req.ReduceOnly) body["reduceOnly"] = true;

            // Bybit attaches SL/TP directly to the entry order (no separate algo call needed).
            if (req.StopLoss.HasValue) body["stopLoss"] = Num(req.StopLoss.Value);
            if (req.TakeProfit.HasValue) body["takeProfit"] = Num(req.TakeProfit.Value);

            using var doc = await SignedPostAsync("/v5/order/create", body).ConfigureAwait(false);
            var id = Result(doc).TryGetProperty("orderId", out var o) ? o.GetString() ?? string.Empty : string.Empty;
            return new OrderResult { Success = true, OrderId = id, Message = "Order placed." };
        }

        public async Task<OrderResult> ClosePositionAsync(string symbol, decimal? quantity)
        {
            decimal size = 0m; bool isLong = true;
            foreach (var p in await GetPositionsAsync().ConfigureAwait(false))
                if (string.Equals(p.Symbol, symbol, StringComparison.OrdinalIgnoreCase))
                { size = p.Quantity; isLong = p.Side == OrderSide.Buy; break; }

            if (size == 0m) return OrderResult.Fail("No open position to close.");
            decimal qty = quantity ?? size;

            using var doc = await SignedPostAsync("/v5/order/create", new Dictionary<string, object>
            {
                ["category"] = Category,
                ["symbol"] = symbol,
                ["side"] = isLong ? "Sell" : "Buy",   // opposite of the position
                ["orderType"] = "Market",
                ["qty"] = Num(qty),
                ["reduceOnly"] = true,
                ["positionIdx"] = 0
            }).ConfigureAwait(false);

            var id = Result(doc).TryGetProperty("orderId", out var o) ? o.GetString() ?? string.Empty : string.Empty;
            return OrderResult.Ok(id);
        }

        public async Task<OrderResult> UpdateStopTakeAsync(string symbol, decimal? stopLoss, decimal? takeProfit)
        {
            var body = new Dictionary<string, object>
            {
                ["category"] = Category,
                ["symbol"] = symbol,
                ["positionIdx"] = 0,
                // "0" clears the level on Bybit; leave it out to keep the existing one.
                ["stopLoss"] = stopLoss.HasValue ? Num(stopLoss.Value) : "0",
                ["takeProfit"] = takeProfit.HasValue ? Num(takeProfit.Value) : "0"
            };
            using var _ = await SignedPostAsync("/v5/position/trading-stop", body).ConfigureAwait(false);
            return OrderResult.Ok(symbol);
        }

        // ----------------------------------------------------------------- Closed trades (auto-journal)
        /// <summary>
        /// Uses Bybit's closed-PnL feed, which already gives entry/exit VWAP, realized PnL AND the leverage
        /// used — so imported Bybit trades carry a real margin figure (unlike the Binance fills path).
        ///
        /// Direction note: closed-pnl "side" is the side of the position-closing order; closing a long is a
        /// Sell. If longs/shorts come out inverted in testing, flip the mapping below.
        /// </summary>
        public async Task<IReadOnlyList<ClosedTrade>> GetRecentClosedTradesAsync(DateTime sinceUtc)
        {
            var sinceUtcNorm = sinceUtc.ToUniversalTime();
            long startMs = new DateTimeOffset(sinceUtcNorm).ToUnixTimeMilliseconds();

            using var doc = await SignedGetAsync("/v5/position/closed-pnl", $"category={Category}&startTime={startMs}&limit=100").ConfigureAwait(false);

            var closed = new List<ClosedTrade>();
            foreach (var r in Result(doc).GetProperty("list").EnumerateArray())
            {
                var symbol = r.GetProperty("symbol").GetString() ?? string.Empty;
                decimal qty = DecP(r, "qty");
                decimal entry = DecP(r, "avgEntryPrice");
                decimal exit = DecP(r, "avgExitPrice");
                decimal lev = DecP(r, "leverage");
                decimal pnl = DecP(r, "closedPnl");
                long createdMs = Str2Long(r.TryGetProperty("createdTime", out var ct) ? ct.GetString() : null);
                var closedAt = DateTimeOffset.FromUnixTimeMilliseconds(createdMs).UtcDateTime;
                if (closedAt < sinceUtcNorm) continue;

                bool isLong = string.Equals(r.GetProperty("side").GetString(), "Sell", StringComparison.OrdinalIgnoreCase);
                string orderId = r.TryGetProperty("orderId", out var oid) ? oid.GetString() ?? string.Empty : string.Empty;

                closed.Add(new ClosedTrade
                {
                    Symbol = symbol,
                    IsLong = isLong,
                    EntryPrice = entry,
                    ExitPrice = exit,
                    Margin = lev > 0 ? entry * qty / lev : entry * qty,   // real margin (leverage is provided)
                    RealizedPnl = pnl,
                    ExternalId = $"BYBIT:{symbol}:{orderId}:{createdMs}",
                    ClosedAtUtc = closedAt
                });
            }

            closed.Sort((a, b) => a.ClosedAtUtc.CompareTo(b.ClosedAtUtc));
            return closed;
        }

        // ----------------------------------------------------------------- HTTP
        private async Task<JsonDocument> SignedGetAsync(string path, string query)
        {
            string ts = BybitSignature.Timestamp();
            string sign = BybitSignature.Sign(ts, _apiKey, RecvWindow, query, _apiSecret);

            using var req = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}{path}?{query}");
            AddAuthHeaders(req, ts, sign);
            return await SendAsync(req).ConfigureAwait(false);
        }

        private async Task<JsonDocument> SignedPostAsync(string path, Dictionary<string, object> body)
        {
            string json = JsonSerializer.Serialize(body);
            string ts = BybitSignature.Timestamp();
            string sign = BybitSignature.Sign(ts, _apiKey, RecvWindow, json, _apiSecret);

            using var req = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}{path}")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            AddAuthHeaders(req, ts, sign);
            return await SendAsync(req).ConfigureAwait(false);
        }

        private async Task<JsonDocument> PublicGetAsync(string path, string query)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}{path}?{query}");
            return await SendAsync(req).ConfigureAwait(false);
        }

        private void AddAuthHeaders(HttpRequestMessage req, string ts, string sign)
        {
            req.Headers.Add("X-BAPI-API-KEY", _apiKey);
            req.Headers.Add("X-BAPI-TIMESTAMP", ts);
            req.Headers.Add("X-BAPI-RECV-WINDOW", RecvWindow);
            req.Headers.Add("X-BAPI-SIGN", sign);
        }

        private static async Task<JsonDocument> SendAsync(HttpRequestMessage req)
        {
            var resp = await Http.SendAsync(req).ConfigureAwait(false);
            var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
                throw new Exception($"Bybit request failed (HTTP {(int)resp.StatusCode}).");
            return JsonDocument.Parse(body);
        }

        // Bybit wraps every response in { retCode, retMsg, result }. Non-zero retCode = error.
        private static JsonElement Result(JsonDocument doc)
        {
            var root = doc.RootElement;
            int code = root.TryGetProperty("retCode", out var rc) ? rc.GetInt32() : -1;
            if (code != 0)
            {
                var msg = root.TryGetProperty("retMsg", out var rm) ? rm.GetString() : "unknown error";
                throw new Exception($"Bybit: {msg} (retCode {code})");
            }
            return root.GetProperty("result");
        }

        private static bool IsBenign(Exception ex) =>
            ex.Message.Contains("110043") ||   // leverage not modified
            ex.Message.Contains("34036") ||    // margin mode not modified
            ex.Message.Contains("not modified", StringComparison.OrdinalIgnoreCase);

        // ----------------------------------------------------------------- Parsing helpers (Bybit returns numbers as strings)
        private static decimal DecP(JsonElement e, string prop)
            => e.TryGetProperty(prop, out var v) ? Str2Dec(v.ValueKind == JsonValueKind.String ? v.GetString() : v.GetRawText()) : 0m;

        private static decimal? NullableDec(JsonElement e, string prop)
        {
            if (!e.TryGetProperty(prop, out var v)) return null;
            var s = v.ValueKind == JsonValueKind.String ? v.GetString() : v.GetRawText();
            if (string.IsNullOrWhiteSpace(s) || s == "0") return null;
            return Str2Dec(s);
        }

        private static decimal Str2Dec(string? s)
            => decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0m;

        private static long Str2Long(string? s)
            => long.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var l) ? l : 0L;

        private static decimal FirstNonZero(decimal a, decimal b) => a != 0m ? a : b;

        private static string Num(decimal v) => v.ToString("0.########", CultureInfo.InvariantCulture);

        public void Dispose() { }
    }
}