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

        private readonly ConcurrentDictionary<string, LeverageBracket> _bracketCache = new();

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

        public async Task<LeverageBracket> GetLeverageBracketAsync(string symbol)
        {
            if (_bracketCache.TryGetValue(symbol, out var cached)) return cached;

            using var doc = await SignedRequestAsync(HttpMethod.Get, "/fapi/v1/leverageBracket",
                $"symbol={symbol}").ConfigureAwait(false);

            // With a symbol supplied, Binance may return a one-element array OR a bare object.
            JsonElement node = doc.RootElement;
            if (node.ValueKind == JsonValueKind.Array)
            {
                JsonElement match = default; bool found = false;
                foreach (var e in node.EnumerateArray())
                    if (e.GetProperty("symbol").GetString() == symbol) { match = e; found = true; break; }
                if (!found) throw new Exception($"No leverage bracket returned for {symbol}.");
                node = match;
            }

            var tiers = new List<LeverageTier>();
            foreach (var b in node.GetProperty("brackets").EnumerateArray())
                tiers.Add(new LeverageTier
                {
                    Bracket = (int)DecAny(b, "bracket"),
                    MaxLeverage = (int)DecAny(b, "initialLeverage"),
                    NotionalFloor = DecAny(b, "notionalFloor"),
                    NotionalCap = DecAny(b, "notionalCap")
                });
            tiers.Sort((a, c) => a.NotionalFloor.CompareTo(c.NotionalFloor));

            var bracket = new LeverageBracket { Symbol = symbol, Tiers = tiers };
            _bracketCache[symbol] = bracket;
            return bracket;
        }

        // leverageBracket returns numbers (not strings); the string-only Dec() would throw on them.
        private static decimal DecAny(JsonElement e, string prop)
        {
            if (!e.TryGetProperty(prop, out var v)) return 0m;
            return v.ValueKind switch
            {
                JsonValueKind.Number => v.GetDecimal(),
                JsonValueKind.String => decimal.TryParse(v.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0m,
                _ => 0m
            };
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

        // ----------------------------------------------------------------- Closed-trade reconcile (auto-journal)

        /// <summary>Binance serves at most 7 days per /fapi/v1/userTrades query.</summary>
        private static readonly TimeSpan MaxQueryWindow = TimeSpan.FromDays(7);

        /// <summary>Binance keeps 6 months of futures trade history; older windows return nothing.</summary>
        private static readonly TimeSpan HistoryHorizon = TimeSpan.FromDays(180);

        /// <summary>
        /// How far BEFORE the watermark to start scanning fills. A position opened before the
        /// watermark and closed after it has its opening fills outside the window; without them
        /// ClosedTradeBuilder.Fold sees a closing fill with no lot open and treats it as OPENING a
        /// fresh lot in the wrong direction, producing an inverted or missing trade. Folding from
        /// earlier and then discarding lots that closed before the watermark fixes that.
        /// 7 days covers any realistic swing hold; anything longer needs a manual CSV import.
        /// </summary>
        private static readonly TimeSpan FoldLookback = TimeSpan.FromDays(7);

        private const int PageLimit = 1000;   // Binance max for both /income and /userTrades

        /// <summary>
        /// Reconstructs completed round-trip trades since <paramref name="sinceUtc"/> so the journal
        /// can import them. Strategy: use the realized-PnL income feed to discover which symbols had
        /// closes in the range, then pull each symbol's fills and fold them into round trips via
        /// <see cref="ClosedTradeBuilder"/>.
        ///
        /// The range is split into windows of at most 7 days because Binance rejects or truncates
        /// anything wider, and each window is paged because both endpoints cap at 1000 rows. Fills
        /// for one symbol are concatenated across ALL windows before folding — folding per window
        /// would cut every position that straddles a window boundary in half.
        ///
        /// Contract with TradeReconciler: this either covers the whole range up to now, or throws.
        /// It must never return a partial list, because the caller advances its watermark to "now"
        /// on any successful return.
        ///
        /// Still assumes one-way position mode. Margin cannot be recovered from fills, so imported
        /// trades carry entry *notional* as margin (see ClosedTradeBuilder).
        /// </summary>
        public async Task<IReadOnlyList<ClosedTrade>> GetRecentClosedTradesAsync(DateTime sinceUtc)
        {
            var now = DateTime.UtcNow;
            var since = sinceUtc.ToUniversalTime();
            if (since >= now) return Array.Empty<ClosedTrade>();

            var horizon = now - HistoryHorizon;
            if (since < horizon) since = horizon;

            var scanFrom = since - FoldLookback;
            if (scanFrom < horizon) scanFrom = horizon;

            // Discovery runs over the REAL gap. The fold lookback only matters for FETCHING a
            // discovered symbol's opening fills — a symbol with no realized-PnL event since the
            // watermark has no trade to journal, so scanning the lookback for it is pure waste.
            // With a 30s poll timer that waste was 2 income calls (weight 30 each) per platform
            // per tick, forever.
            var discoveryWindows = SplitWindows(since, now, MaxQueryWindow);
            var fillWindows = SplitWindows(scanFrom, now, MaxQueryWindow);

            AutoJournalTrace.Write(
                $"    [binance] discover {since:o} -> {now:o} in {discoveryWindows.Count} window(s); " +
                $"fills from {scanFrom:o} in {fillWindows.Count} window(s); emit filter: closedAt >= {since:o}");

            // 1) Which symbols had a realized-PnL event anywhere in the range.
            var symbols = new HashSet<string>(StringComparer.Ordinal);
            foreach (var (from, to) in discoveryWindows)
                foreach (var s in await RealizedPnlSymbolsAsync(Ms(from), Ms(to)).ConfigureAwait(false))
                    symbols.Add(s);

            // 2) Per symbol: gather every fill across the whole range, then fold once.
            AutoJournalTrace.Write(
                $"    [binance] income REALIZED_PNL found {symbols.Count} symbol(s): " +
                $"[{string.Join(", ", symbols)}]");

            var closed = new List<ClosedTrade>();

            foreach (var symbol in symbols)
            {
                var fills = new List<ExchangeFill>();
                foreach (var (from, to) in fillWindows)
                    fills.AddRange(await UserTradesAsync(symbol, Ms(from), Ms(to)).ConfigureAwait(false));

                if (fills.Count == 0)
                {
                    AutoJournalTrace.Write($"    [binance] {symbol}: 0 fills in range");
                    continue;
                }

                // Window edges touch and Binance's bounds are inclusive, so boundary fills arrive
                // twice. A duplicate fill would be folded as a real scale-in and corrupt the VWAP.
                fills = DistinctByTradeId(fills);
                fills.Sort((a, b) => a.TimeUtc.CompareTo(b.TimeUtc));

                var folded = ClosedTradeBuilder.Fold(fills);
                int kept = 0;
                foreach (var t in folded)
                    if (t.ClosedAtUtc >= since) { closed.Add(t); kept++; }   // drop lots closed before the mark

                AutoJournalTrace.Write(
                    $"    [binance] {symbol}: {fills.Count} fill(s) -> {folded.Count} round-trip(s) -> {kept} kept");
            }

            closed.Sort((a, b) => a.ClosedAtUtc.CompareTo(b.ClosedAtUtc));
            return closed;
        }

        private static long Ms(DateTime utc) => new DateTimeOffset(utc, TimeSpan.Zero).ToUnixTimeMilliseconds();

        /// <summary>
        /// Splits [from, to] into consecutive spans of at most <paramref name="max"/>. Adjacent spans
        /// share an endpoint; the caller de-duplicates.
        /// </summary>
        private static List<(DateTime From, DateTime To)> SplitWindows(DateTime from, DateTime to, TimeSpan max)
        {
            var list = new List<(DateTime, DateTime)>();
            var cursor = from;

            // Bounded by construction: HistoryHorizon / MaxQueryWindow = 180/7 ≈ 26 windows worst case.
            while (cursor < to)
            {
                var end = cursor + max;
                if (end > to) end = to;
                list.Add((cursor, end));
                cursor = end;
            }
            return list;
        }

        private static List<ExchangeFill> DistinctByTradeId(List<ExchangeFill> fills)
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            var result = new List<ExchangeFill>(fills.Count);

            foreach (var f in fills)
            {
                // A fill with no id can't be de-duplicated; keep it rather than guess.
                if (string.IsNullOrEmpty(f.TradeId) || seen.Add(f.TradeId)) result.Add(f);
            }
            return result;
        }

        // Symbols that had a realized-PnL event in [startMs, endMs] (cheap discovery; avoids scanning
        // every symbol). Paged: /fapi/v1/income caps at 1000 rows per call.
        private async Task<IReadOnlyList<string>> RealizedPnlSymbolsAsync(long startMs, long endMs)
        {
            var set = new HashSet<string>(StringComparer.Ordinal);
            long cursor = startMs;

            while (true)
            {
                using var doc = await SignedRequestAsync(HttpMethod.Get, "/fapi/v1/income",
                    $"incomeType=REALIZED_PNL&startTime={cursor}&endTime={endMs}&limit={PageLimit}")
                    .ConfigureAwait(false);

                int count = 0;
                long newest = cursor;

                foreach (var e in doc.RootElement.EnumerateArray())
                {
                    count++;
                    if (e.TryGetProperty("symbol", out var s) && s.GetString() is string name && name.Length > 0)
                        set.Add(name);
                    if (e.TryGetProperty("time", out var t) && t.GetInt64() > newest) newest = t.GetInt64();
                }

                if (count < PageLimit) break;               // last page
                if (newest <= cursor) break;                // all rows share one ms — cannot advance, stop
                cursor = newest + 1;
                if (cursor > endMs) break;
            }

            return new List<string>(set);
        }

        // Fills for one symbol in [startMs, endMs]. Paged the same way: fromId cannot be combined
        // with startTime/endTime on this endpoint, so paging advances the start time instead.
        private async Task<List<ExchangeFill>> UserTradesAsync(string symbol, long startMs, long endMs)
        {
            var fills = new List<ExchangeFill>();
            long cursor = startMs;
            string? nonUsdtFeeAsset = null;

            while (true)
            {
                using var doc = await SignedRequestAsync(HttpMethod.Get, "/fapi/v1/userTrades",
                    $"symbol={symbol}&startTime={cursor}&endTime={endMs}&limit={PageLimit}")
                    .ConfigureAwait(false);

                int count = 0;
                long newest = cursor;

                foreach (var e in doc.RootElement.EnumerateArray())
                {
                    count++;
                    long tMs = e.GetProperty("time").GetInt64();
                    if (tMs > newest) newest = tMs;

                    // Binance reports realizedPnl GROSS of commission; the fee is a separate field.
                    // Only take it when it is charged in USDT — with BNB fee deduction enabled the
                    // asset is BNB, and converting it would need a rate we do not have here. A wrong
                    // fee is worse than a missing one, so leave it at 0 and say so in the trace.
                    var feeAsset = e.TryGetProperty("commissionAsset", out var ca) ? ca.GetString() : null;
                    decimal fee = 0m;
                    if (string.Equals(feeAsset, "USDT", StringComparison.OrdinalIgnoreCase))
                        fee = Math.Abs(Dec(e, "commission"));
                    else if (!string.IsNullOrEmpty(feeAsset))
                        nonUsdtFeeAsset = feeAsset;

                    fills.Add(new ExchangeFill
                    {
                        Symbol = symbol,
                        IsBuy = string.Equals(e.GetProperty("side").GetString(), "BUY", StringComparison.OrdinalIgnoreCase),
                        Quantity = Dec(e, "qty"),
                        Price = Dec(e, "price"),
                        RealizedPnl = Dec(e, "realizedPnl"),
                        Commission = fee,
                        TradeId = e.TryGetProperty("id", out var id) ? id.GetRawText() : string.Empty,
                        TimeUtc = DateTimeOffset.FromUnixTimeMilliseconds(tMs).UtcDateTime
                    });
                }

                if (count < PageLimit) break;
                if (newest <= cursor) break;
                cursor = newest;   // not +1: fills sharing this ms must be re-fetched, then de-duplicated by id
                if (cursor > endMs) break;
            }

            if (nonUsdtFeeAsset != null)
                AutoJournalTrace.Write(
                    $"    [binance] WARNING {symbol}: fees charged in {nonUsdtFeeAsset}, not USDT. " +
                    $"Commission excluded — imported PnL for this symbol is gross of fees.");

            return fills;
        }

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