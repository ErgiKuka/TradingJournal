using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Kraken Futures v3, scoped to PF_ multi-collateral perpetuals (linear, USD-quoted, size in base
    /// currency) so the app's sizing math (qty = notional / price) stays correct. Inverse (PI_) and
    /// dated (FI_) contracts are deliberately excluded — their size semantics differ and would mis-size.
    ///
    /// ⚠ VERIFY ON THE DEMO ENV FIRST (demo-futures.kraken.com). Defaults to demo. The write path
    /// (sendorder) and auth are confirmed against Kraken's live OpenAPI spec; the read-path FIELD NAMES
    /// on /accounts, /tickers and /instruments are mapped from the documented shapes but should be
    /// confirmed against a real demo response — they're marked below.
    ///
    /// KNOWN MODEL LIMITS (not bugs — Kraken simply doesn't expose these per position):
    ///   • openpositions returns only side/symbol/entry price/size. So Margin, LiquidationPrice and
    ///     Leverage are NOT available per position and come back as 0. MarkPrice + UnrealizedPnl are
    ///     derived from a /tickers call.
    ///   • Kraken uses portfolio (cross) margining, not per-position isolated/cross. MarginMode is
    ///     reported as Cross and the Isolated request is ignored.
    ///   • Leverage is set per-market via /leveragepreferences (a max), not per-order. The leverage
    ///     "bracket" guard is permissive here; Kraken enforces real limits at order time.
    /// </summary>
    public sealed class KrakenFuturesClient : IExchangeClient, IDisposable
    {
        private const string LiveHttp = "https://futures.kraken.com";
        private const string DemoHttp = "https://demo-futures.kraken.com";
        private const string UrlPrefix = "/derivatives/api/v3/";   // the URL you actually call
        private const string SignPrefix = "/api/v3/";              // the path used in the Authent hash

        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        private static readonly ConcurrentDictionary<string, SymbolRules> RulesCache = new();

        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly string _http;

        public string Exchange => "Kraken";

        public KrakenFuturesClient(string apiKey, string apiSecret, bool useTestnet)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _http = useTestnet ? DemoHttp : LiveHttp;
        }

        // ----------------------------------------------------------------- Reads
        public async Task<AccountBalance> GetBalanceAsync()
        {
            using var doc = await SignedRequestAsync(HttpMethod.Get, "accounts").ConfigureAwait(false);
            var root = doc.RootElement;

            // Multi-collateral "flex" account holds the USD-denominated portfolio figures.
            // CONFIRM field names on demo: availableMargin / portfolioValue / marginEquity.
            if (root.TryGetProperty("accounts", out var accounts) &&
                accounts.TryGetProperty("flex", out var flex))
            {
                decimal wallet = FirstNonZero(DecN(flex, "portfolioValue"), DecN(flex, "marginEquity"));
                decimal available = FirstNonZero(DecN(flex, "availableMargin"), DecN(flex, "availableFunds"));
                return new AccountBalance { WalletUsdt = wallet, AvailableUsdt = available };
            }
            return new AccountBalance();
        }

        public async Task<TickerPrice> GetPriceAsync(string symbol)
        {
            var prices = await MarkPricesAsync().ConfigureAwait(false);
            return new TickerPrice
            {
                Symbol = symbol,
                Price = prices.TryGetValue(symbol, out var p) ? p : 0m
            };
        }

        public async Task<IReadOnlyList<string>> GetSymbolsAsync()
        {
            using var doc = await SignedRequestAsync(HttpMethod.Get, "instruments").ConfigureAwait(false);
            var list = new List<string>();
            if (doc.RootElement.TryGetProperty("instruments", out var arr))
            {
                foreach (var i in arr.EnumerateArray())
                {
                    var sym = i.TryGetProperty("symbol", out var s) ? s.GetString() : null;
                    bool tradeable = !i.TryGetProperty("tradeable", out var t) || t.ValueKind != JsonValueKind.False;
                    if (!string.IsNullOrEmpty(sym) && tradeable &&
                        sym!.StartsWith("PF_", StringComparison.OrdinalIgnoreCase))
                        list.Add(sym.ToUpperInvariant());
                }
            }
            list.Sort(StringComparer.Ordinal);
            return list;
        }

        public async Task<SymbolRules> GetSymbolRulesAsync(string symbol)
        {
            if (RulesCache.TryGetValue(symbol, out var cached)) return cached;

            using var doc = await SignedRequestAsync(HttpMethod.Get, "instruments").ConfigureAwait(false);
            if (doc.RootElement.TryGetProperty("instruments", out var arr))
            {
                foreach (var i in arr.EnumerateArray())
                {
                    if (!string.Equals(i.TryGetProperty("symbol", out var s) ? s.GetString() : null,
                            symbol, StringComparison.OrdinalIgnoreCase)) continue;

                    // contractValueTradePrecision = decimal places allowed for order size -> step = 10^-p.
                    int sizePrec = (int)DecN(i, "contractValueTradePrecision");
                    decimal step = sizePrec >= 0 ? 1m / Pow10(sizePrec) : Pow10(-sizePrec);

                    var rules = new SymbolRules
                    {
                        Symbol = symbol,
                        StepSize = step == 0m ? 0.0001m : step,
                        TickSize = DecN(i, "tickSize"),
                        MinNotional = 0m   // Kraken enforces a min *size*, not notional; left to the exchange
                    };
                    RulesCache[symbol] = rules;
                    return rules;
                }
            }
            throw new Exception($"Symbol {symbol} not found on Kraken Futures (PF_ perpetuals only).");
        }

        public Task<LeverageBracket> GetLeverageBracketAsync(string symbol)
        {
            // Kraken doesn't expose Binance-style notional tiers. Return one permissive tier so the
            // app's client-side cap guard doesn't false-reject; Kraken enforces real leverage limits
            // at order time (rejects with fixedLeverageTooHigh).
            var bracket = new LeverageBracket
            {
                Symbol = symbol,
                Tiers = new List<LeverageTier>
                {
                    new LeverageTier { Bracket = 1, MaxLeverage = 50, NotionalFloor = 0m, NotionalCap = decimal.MaxValue }
                }
            };
            return Task.FromResult(bracket);
        }

        public async Task<IReadOnlyList<PositionInfo>> GetPositionsAsync()
        {
            using var doc = await SignedRequestAsync(HttpMethod.Get, "openpositions").ConfigureAwait(false);
            if (!doc.RootElement.TryGetProperty("openPositions", out var arr))
                return Array.Empty<PositionInfo>();

            // One tickers call to derive mark price + unrealized PnL (openpositions doesn't include them).
            IReadOnlyDictionary<string, decimal> marks;
            try { marks = await MarkPricesAsync().ConfigureAwait(false); }
            catch { marks = new Dictionary<string, decimal>(); }

            var list = new List<PositionInfo>();
            foreach (var p in arr.EnumerateArray())
            {
                var symbol = p.TryGetProperty("symbol", out var s) ? s.GetString() ?? string.Empty : string.Empty;
                if (!symbol.StartsWith("PF_", StringComparison.OrdinalIgnoreCase)) continue; // scope to PF_

                bool isLong = string.Equals(p.TryGetProperty("side", out var sd) ? sd.GetString() : null,
                    "long", StringComparison.OrdinalIgnoreCase);
                decimal size = DecN(p, "size");
                if (size == 0m) continue;

                decimal entry = DecN(p, "price");
                decimal mark = marks.TryGetValue(symbol, out var m) ? m : entry;
                decimal uPnl = (mark - entry) * size * (isLong ? 1m : -1m);   // linear PF_ contracts

                list.Add(new PositionInfo
                {
                    Symbol = symbol,
                    Side = isLong ? OrderSide.Buy : OrderSide.Sell,
                    Quantity = size,
                    EntryPrice = entry,
                    MarkPrice = mark,
                    UnrealizedPnl = uPnl,
                    LiquidationPrice = 0m,   // not available per position (portfolio margining)
                    Margin = 0m,             // not available per position
                    Leverage = 0m,           // not available per position
                    MarginMode = MarginMode.Cross,
                    StopLoss = null,
                    TakeProfit = null
                });
            }
            return list;
        }

        // ----------------------------------------------------------------- Writes
        public async Task SetLeverageAsync(string symbol, int leverage, MarginMode mode)
        {
            // Per-market max leverage. MarginMode isolated/cross is not a Kraken concept here (portfolio
            // margining), so the mode argument is intentionally ignored.
            var query = $"symbol={symbol}&maxLeverage={leverage.ToString(CultureInfo.InvariantCulture)}";
            try
            {
                using var _ = await SignedRequestAsync(HttpMethod.Put, "leveragepreferences", query).ConfigureAwait(false);
            }
            catch (Exception ex) when (ex.Message.Contains("no change", StringComparison.OrdinalIgnoreCase))
            {
                // already at that leverage — ignore
            }
        }

        public async Task<OrderResult> PlaceOrderAsync(OrderRequest req)
        {
            var side = req.Side == OrderSide.Buy ? "buy" : "sell";
            string entryId;

            // Entry
            var q = new List<string> { $"orderType={KrakenOrderType(req.Kind)}", $"symbol={req.Symbol}",
                                       $"side={side}", $"size={Num(req.Quantity)}" };
            if (req.Kind == OrderKind.Limit) q.Add($"limitPrice={Num(req.Price ?? 0m)}");
            if (req.Kind == OrderKind.Conditional)
            {
                q.Add($"stopPrice={Num(req.TriggerPrice ?? 0m)}");
                q.Add("triggerSignal=mark");
            }
            if (req.ReduceOnly) q.Add("reduceOnly=true");

            using (var doc = await SignedRequestAsync(HttpMethod.Post, "sendorder", string.Join("&", q)).ConfigureAwait(false))
            {
                if (!ReadOrderId(doc, out var status))
                    throw new Exception($"Kraken rejected the order: {status}");
                entryId = OrderIdOrThrow(doc);
            }

            // Protective SL / TP as separate reduce-only trigger orders (Kraken has no attach-on-entry).
            var warnings = new List<string>();
            var protectSide = req.Side == OrderSide.Buy ? "sell" : "buy";
            if (req.StopLoss.HasValue)
                try { await SendTriggerReduceOnlyAsync(req.Symbol, protectSide, "stp", req.StopLoss.Value).ConfigureAwait(false); }
                catch (Exception ex) { warnings.Add($"SL not set ({ex.Message})"); }
            if (req.TakeProfit.HasValue)
                try { await SendTriggerReduceOnlyAsync(req.Symbol, protectSide, "take_profit", req.TakeProfit.Value).ConfigureAwait(false); }
                catch (Exception ex) { warnings.Add($"TP not set ({ex.Message})"); }

            var msg = warnings.Count == 0
                ? "Order placed."
                : "Entry placed, but " + string.Join("; ", warnings) + ". Set them on the exchange.";
            return new OrderResult { Success = true, OrderId = entryId, Message = msg };
        }

        private async Task SendTriggerReduceOnlyAsync(string symbol, string side, string orderType, decimal stopPrice)
        {
            var q = $"orderType={orderType}&symbol={symbol}&side={side}&size=0&stopPrice={Num(stopPrice)}" +
                    "&triggerSignal=mark&reduceOnly=true";
            // size=0 with reduceOnly closes the resting position size on trigger for PF_ markets;
            // if your demo testing shows size is required, pass the current position size instead.
            using var _ = await SignedRequestAsync(HttpMethod.Post, "sendorder", q).ConfigureAwait(false);
        }

        public async Task<OrderResult> ClosePositionAsync(string symbol, decimal? quantity)
        {
            decimal size = 0m; bool isLong = true;
            foreach (var p in await GetPositionsAsync().ConfigureAwait(false))
                if (string.Equals(p.Symbol, symbol, StringComparison.OrdinalIgnoreCase))
                { size = p.Quantity; isLong = p.Side == OrderSide.Buy; break; }

            if (size == 0m) return OrderResult.Fail("No open position to close.");
            decimal qty = quantity ?? size;
            var side = isLong ? "sell" : "buy";

            var q = $"orderType=mkt&symbol={symbol}&side={side}&size={Num(qty)}&reduceOnly=true";
            using var doc = await SignedRequestAsync(HttpMethod.Post, "sendorder", q).ConfigureAwait(false);
            return ReadOrderId(doc, out var status) ? OrderResult.Ok(OrderIdOrThrow(doc))
                                                    : OrderResult.Fail($"Close rejected: {status}");
        }

        public Task<OrderResult> UpdateStopTakeAsync(string symbol, decimal? stopLoss, decimal? takeProfit)
            => throw new NotImplementedException("Editing TP/SL on Kraken is the next step (cancel + re-send trigger orders).");

        // ----------------------------------------------------------------- Closed trades (auto-journal)
        /// <summary>
        /// Reconstructs round-trip trades from recent fills. The last-100 fills query (no lastFillTime)
        /// carries realized_pnl, so PnL is real; older paginated queries drop it, so this intentionally
        /// does NOT paginate (fine for the short reconcile windows). Margin can't be recovered, so
        /// imported trades carry entry notional as margin (same limitation as Binance).
        /// </summary>
        public async Task<IReadOnlyList<ClosedTrade>> GetRecentClosedTradesAsync(DateTime sinceUtc)
        {
            var since = sinceUtc.ToUniversalTime();
            using var doc = await SignedRequestAsync(HttpMethod.Get, "fills").ConfigureAwait(false);
            if (!doc.RootElement.TryGetProperty("fills", out var arr))
                return Array.Empty<ClosedTrade>();

            // Group fills by symbol; the folder walks one net position at a time.
            var bySymbol = new Dictionary<string, List<ExchangeFill>>(StringComparer.Ordinal);
            foreach (var f in arr.EnumerateArray())
            {
                var symbol = f.TryGetProperty("symbol", out var s) ? s.GetString() ?? string.Empty : string.Empty;
                if (!symbol.StartsWith("PF_", StringComparison.OrdinalIgnoreCase)) continue;

                var fill = new ExchangeFill
                {
                    Symbol = symbol,
                    IsBuy = string.Equals(f.TryGetProperty("side", out var sd) ? sd.GetString() : null, "buy", StringComparison.OrdinalIgnoreCase),
                    Quantity = DecN(f, "size"),
                    Price = DecN(f, "price"),
                    RealizedPnl = DecN(f, "realized_pnl"),   // null -> 0 (open/increase fills)
                    TradeId = f.TryGetProperty("fill_id", out var id) ? id.GetString() ?? string.Empty : string.Empty,
                    TimeUtc = ParseUtc(f, "fillTime")
                };
                if (!bySymbol.TryGetValue(symbol, out var bucket)) bySymbol[symbol] = bucket = new List<ExchangeFill>();
                bucket.Add(fill);
            }

            var closed = new List<ClosedTrade>();
            foreach (var bucket in bySymbol.Values)
            {
                bucket.Sort((a, b) => a.TimeUtc.CompareTo(b.TimeUtc));   // fills arrive newest-first; fold oldest-first
                foreach (var t in ClosedTradeBuilder.Fold(bucket))
                    if (t.ClosedAtUtc >= since) closed.Add(t);
            }

            closed.Sort((a, b) => a.ClosedAtUtc.CompareTo(b.ClosedAtUtc));
            return closed;
        }

        // ----------------------------------------------------------------- Helpers
        private async Task<IReadOnlyDictionary<string, decimal>> MarkPricesAsync()
        {
            using var doc = await SignedRequestAsync(HttpMethod.Get, "tickers").ConfigureAwait(false);
            var map = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            if (doc.RootElement.TryGetProperty("tickers", out var arr))
                foreach (var t in arr.EnumerateArray())
                {
                    var sym = t.TryGetProperty("symbol", out var s) ? s.GetString() : null;
                    if (string.IsNullOrEmpty(sym)) continue;
                    decimal price = FirstNonZero(DecN(t, "markPrice"), DecN(t, "last"));
                    map[sym!] = price;
                }
            return map;
        }

        private static string KrakenOrderType(OrderKind kind) => kind switch
        {
            OrderKind.Limit => "lmt",
            OrderKind.Conditional => "stp",
            _ => "mkt"   // immediate-or-cancel with 1% price protection = Kraken's "market"
        };

        private static bool ReadOrderId(JsonDocument doc, out string status)
        {
            status = "unknown";
            var root = doc.RootElement;
            if (root.TryGetProperty("result", out var r) && r.GetString() == "error")
            {
                status = root.TryGetProperty("error", out var e) ? e.GetString() ?? "error" : "error";
                return false;
            }
            if (root.TryGetProperty("sendStatus", out var ss) && ss.TryGetProperty("status", out var st))
            {
                status = st.GetString() ?? "unknown";
                return status == "placed" || status == "filled" || status == "partiallyFilled";
            }
            return false;
        }

        private static string OrderIdOrThrow(JsonDocument doc)
        {
            if (doc.RootElement.TryGetProperty("sendStatus", out var ss) &&
                ss.TryGetProperty("order_id", out var id))
                return id.GetString() ?? string.Empty;
            return string.Empty;
        }

        // ----------------------------------------------------------------- HTTP
        private async Task<JsonDocument> SignedRequestAsync(HttpMethod method, string endpoint, string query = "")
        {
            string nonce = KrakenFuturesSignature.Nonce();
            string postData = query ?? string.Empty;
            string authent = KrakenFuturesSignature.Authent(postData, nonce, SignPrefix + endpoint, _apiSecret);

            string url = _http + UrlPrefix + endpoint + (postData.Length > 0 ? "?" + postData : string.Empty);
            using var req = new HttpRequestMessage(method, url);
            req.Headers.Add("APIKey", _apiKey);
            req.Headers.Add("Nonce", nonce);
            req.Headers.Add("Authent", authent);

            var resp = await Http.SendAsync(req).ConfigureAwait(false);
            var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
                throw new Exception(DescribeError(body, (int)resp.StatusCode));

            var doc = JsonDocument.Parse(body);
            // Kraken returns HTTP 200 with result:"error" for business errors — surface those too.
            if (doc.RootElement.TryGetProperty("result", out var r) && r.GetString() == "error"
                && method == HttpMethod.Get)
            {
                var msg = doc.RootElement.TryGetProperty("error", out var e) ? e.GetString() : "unknown error";
                doc.Dispose();
                throw new Exception($"Kraken: {msg}");
            }
            return doc;
        }

        private static string DescribeError(string body, int status)
        {
            try
            {
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("error", out var e))
                    return $"Kraken: {e.GetString()} (HTTP {status}).";
            }
            catch { }
            return $"Kraken request failed (HTTP {status}).";
        }

        // Kraken Futures returns JSON numbers (not strings) for most numeric fields.
        private static decimal DecN(JsonElement e, string prop)
        {
            if (!e.TryGetProperty(prop, out var v)) return 0m;
            return v.ValueKind switch
            {
                JsonValueKind.Number => v.GetDecimal(),
                JsonValueKind.String => decimal.TryParse(v.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0m,
                _ => 0m
            };
        }

        private static DateTime ParseUtc(JsonElement e, string prop)
        {
            if (e.TryGetProperty(prop, out var v) && v.GetString() is string s &&
                DateTimeOffset.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var dto))
                return dto.UtcDateTime;
            return DateTime.UtcNow;
        }

        private static decimal Pow10(int n) { decimal r = 1m; for (int i = 0; i < n; i++) r *= 10m; return r; }
        private static decimal FirstNonZero(decimal a, decimal b) => a != 0m ? a : b;
        private static string Num(decimal v) => v.ToString("0.########", CultureInfo.InvariantCulture);

        public void Dispose() { }
    }
}