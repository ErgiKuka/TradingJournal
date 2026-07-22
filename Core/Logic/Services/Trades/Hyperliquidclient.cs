using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Services;
using HyperLiquid.Net;              // HyperLiquidEnvironment
using HyperLiquid.Net.Clients;      // HyperLiquidRestClient
using HyperLiquid.Net.Objects;      // HyperLiquidCredentials
using HlEnums = HyperLiquid.Net.Enums;   // library OrderSide / OrderType / MarginType, aliased away from the app's own enums

namespace TradingJournal.Core.Logic.Services.Exchange
{
    /// <summary>
    /// Hyperliquid client — a thin adapter over the JKorf HyperLiquid.Net library, which performs the
    /// EIP-712 wallet signing internally (native on Windows). Property names below were confirmed against
    /// the library's model source.
    ///
    /// CREDENTIALS: walletAddress = main account address; privateKey = AGENT/API wallet key (never the master).
    /// QUIRKS: futures symbols are base-only ("ETH"); market orders need a price (we pass the current mid);
    ///         Hyperliquid does not return a per-position mark price, so we derive it from notional / size.
    /// ⚠ VERIFY ON TESTNET FIRST (client defaults to testnet).
    /// </summary>
    public sealed class HyperliquidClient : IExchangeClient, IDisposable
    {
        private readonly HyperLiquidRestClient _client;

        public string Exchange => "Hyperliquid";

        public HyperliquidClient(string walletAddress, string privateKey, bool useTestnet)
        {
            _client = new HyperLiquidRestClient(options =>
            {
                options.ApiCredentials = new HyperLiquidCredentials(walletAddress, privateKey);
                options.Environment = useTestnet ? HyperLiquidEnvironment.Testnet : HyperLiquidEnvironment.Live;
                options.BuilderFeePercentage = 0; // do NOT pay the library's default 1bps builder fee
            });
        }

        // Names no library type, so it never depends on where the result type lives. Every library result
        // exposes .Success / .Error / .Data; we read those through 'var' and only pass primitives here.
        private static void Ensure(bool success, string? error)
        {
            if (!success) throw new Exception($"Hyperliquid: {error ?? "request failed"}");
        }

        // ----------------------------------------------------------------- Reads
        public async Task<AccountBalance> GetBalanceAsync()
        {
            var res = await _client.FuturesApi.Account.GetAccountInfoAsync().ConfigureAwait(false);
            Ensure(res.Success, res.Error?.Message);
            var acc = res.Data!;
            return new AccountBalance
            {
                WalletUsdt = acc.MarginSummary.AccountValue,
                AvailableUsdt = acc.Withdrawable
            };
        }

        public async Task<TickerPrice> GetPriceAsync(string symbol)
        {
            var res = await _client.FuturesApi.ExchangeData.GetPricesAsync().ConfigureAwait(false);
            Ensure(res.Success, res.Error?.Message);
            var prices = res.Data!;
            decimal price = prices.TryGetValue(symbol, out var p) ? p : 0m;
            return new TickerPrice { Symbol = symbol, Price = price };
        }

        public async Task<IReadOnlyList<string>> GetSymbolsAsync()
        {
            var res = await _client.FuturesApi.ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);
            Ensure(res.Success, res.Error?.Message);
            var info = res.Data!;
            var names = info.Symbols
                .Where(s => !s.IsDelisted && !string.IsNullOrEmpty(s.Name))
                .Select(s => s.Name)
                .ToList();
            names.Sort(StringComparer.Ordinal);
            return names;
        }

        public async Task<SymbolRules> GetSymbolRulesAsync(string symbol)
        {
            var res = await _client.FuturesApi.ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);
            Ensure(res.Success, res.Error?.Message);
            var info = res.Data!;
            var s = info.Symbols.FirstOrDefault(x => string.Equals(x.Name, symbol, StringComparison.OrdinalIgnoreCase))
                    ?? throw new Exception($"Symbol {symbol} not found on Hyperliquid.");

            int sizeDecimals = s.QuantityDecimals;   // szDecimals
            decimal step = 1m; for (int i = 0; i < sizeDecimals; i++) step /= 10m;

            return new SymbolRules
            {
                Symbol = symbol,
                StepSize = step,
                // Hyperliquid prices use a significant-figure rule (≤5 sig figs) rather than a fixed tick;
                // 0 = "no explicit tick". If your UI needs a tick, derive it from the current price's scale.
                TickSize = 0m,
                MinNotional = 0m   // ~$10 minimum order value enforced by the exchange
            };
        }

        public async Task<LeverageBracket> GetLeverageBracketAsync(string symbol)
        {
            var res = await _client.FuturesApi.ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);
            Ensure(res.Success, res.Error?.Message);
            var info = res.Data!;
            var s = info.Symbols.FirstOrDefault(x => string.Equals(x.Name, symbol, StringComparison.OrdinalIgnoreCase));
            int maxLev = s?.MaxLeverage ?? 20;

            return new LeverageBracket
            {
                Symbol = symbol,
                Tiers = new List<LeverageTier>
                {
                    new LeverageTier { Bracket = 1, MaxLeverage = maxLev, NotionalFloor = 0m, NotionalCap = decimal.MaxValue }
                }
            };
        }

        public async Task<IReadOnlyList<PositionInfo>> GetPositionsAsync()
        {
            var res = await _client.FuturesApi.Account.GetAccountInfoAsync().ConfigureAwait(false);
            Ensure(res.Success, res.Error?.Message);
            var acc = res.Data!;
            var list = new List<PositionInfo>();

            // acc.Positions is an array of wrappers; the actual fields live on wrapper.Position.
            foreach (var wrapper in acc.Positions)
            {
                var p = wrapper.Position;
                decimal size = p.PositionQuantity ?? 0m;   // "szi": signed (long > 0, short < 0)
                if (size == 0m) continue;

                bool isLong = size > 0m;
                decimal qty = Math.Abs(size);
                decimal entry = p.AverageEntryPrice ?? 0m;
                decimal notional = p.PositionValue ?? 0m;
                decimal mark = qty > 0m ? notional / qty : entry;   // Hyperliquid omits mark; derive it
                bool isolated = p.Leverage?.MarginType == HlEnums.MarginType.Isolated;

                list.Add(new PositionInfo
                {
                    Symbol = p.Symbol,
                    Side = isLong ? OrderSide.Buy : OrderSide.Sell,
                    Quantity = qty,
                    EntryPrice = entry,
                    MarkPrice = mark,
                    UnrealizedPnl = p.UnrealizedPnl ?? 0m,
                    LiquidationPrice = p.LiquidationPrice ?? 0m,
                    Margin = p.MarginUsed ?? 0m,
                    Leverage = p.Leverage?.Value ?? 0,
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
            var marginType = mode == MarginMode.Isolated ? HlEnums.MarginType.Isolated : HlEnums.MarginType.Cross;
            var res = await _client.FuturesApi.Trading.SetLeverageAsync(symbol, leverage, marginType).ConfigureAwait(false);
            Ensure(res.Success, res.Error?.Message);
        }

        public async Task<OrderResult> PlaceOrderAsync(OrderRequest req)
        {
            var side = req.Side == OrderSide.Buy ? HlEnums.OrderSide.Buy : HlEnums.OrderSide.Sell;
            decimal price = req.Price ?? (req.Kind == OrderKind.Market ? (await GetPriceAsync(req.Symbol)).Price : 0m);
            var type = req.Kind == OrderKind.Limit ? HlEnums.OrderType.Limit : HlEnums.OrderType.Market;

            var entry = req.Kind == OrderKind.Conditional
                ? await _client.FuturesApi.Trading.PlaceOrderAsync(
                      req.Symbol, side, HlEnums.OrderType.Market, req.Quantity, price,
                      triggerPrice: req.TriggerPrice, reduceOnly: req.ReduceOnly).ConfigureAwait(false)
                : await _client.FuturesApi.Trading.PlaceOrderAsync(
                      req.Symbol, side, type, req.Quantity, price, reduceOnly: req.ReduceOnly).ConfigureAwait(false);
            Ensure(entry.Success, entry.Error?.Message);
            string entryId = entry.Data!.OrderId.ToString();

            // Protective SL/TP as reduce-only trigger orders on the opposite side (non-fatal). Verify the
            // trigger direction/behavior on testnet — this is the one write-path detail worth checking.
            var warnings = new List<string>();
            var protectSide = req.Side == OrderSide.Buy ? HlEnums.OrderSide.Sell : HlEnums.OrderSide.Buy;
            if (req.StopLoss.HasValue)
                try
                {
                    var sl = await _client.FuturesApi.Trading.PlaceOrderAsync(
                        req.Symbol, protectSide, HlEnums.OrderType.Market, req.Quantity, req.StopLoss.Value,
                        triggerPrice: req.StopLoss, reduceOnly: true).ConfigureAwait(false);
                    Ensure(sl.Success, sl.Error?.Message);
                }
                catch (Exception ex) { warnings.Add($"SL not set ({ex.Message})"); }
            if (req.TakeProfit.HasValue)
                try
                {
                    var tp = await _client.FuturesApi.Trading.PlaceOrderAsync(
                        req.Symbol, protectSide, HlEnums.OrderType.Market, req.Quantity, req.TakeProfit.Value,
                        triggerPrice: req.TakeProfit, reduceOnly: true).ConfigureAwait(false);
                    Ensure(tp.Success, tp.Error?.Message);
                }
                catch (Exception ex) { warnings.Add($"TP not set ({ex.Message})"); }

            var msg = warnings.Count == 0 ? "Order placed."
                    : "Entry placed, but " + string.Join("; ", warnings) + ". Set them on the exchange.";
            return new OrderResult { Success = true, OrderId = entryId, Message = msg };
        }

        public async Task<OrderResult> ClosePositionAsync(string symbol, decimal? quantity)
        {
            decimal size = 0m; bool isLong = true;
            foreach (var pos in await GetPositionsAsync().ConfigureAwait(false))
                if (string.Equals(pos.Symbol, symbol, StringComparison.OrdinalIgnoreCase))
                { size = pos.Quantity; isLong = pos.Side == OrderSide.Buy; break; }

            if (size == 0m) return OrderResult.Fail("No open position to close.");
            decimal qty = quantity ?? size;
            var side = isLong ? HlEnums.OrderSide.Sell : HlEnums.OrderSide.Buy;
            decimal price = (await GetPriceAsync(symbol)).Price;

            var res = await _client.FuturesApi.Trading.PlaceOrderAsync(
                symbol, side, HlEnums.OrderType.Market, qty, price, reduceOnly: true).ConfigureAwait(false);
            if (!res.Success) return OrderResult.Fail($"Close rejected: {res.Error?.Message}");
            return OrderResult.Ok(res.Data!.OrderId.ToString());
        }

        public Task<OrderResult> UpdateStopTakeAsync(string symbol, decimal? stopLoss, decimal? takeProfit)
            => throw new NotImplementedException("Editing TP/SL on Hyperliquid is a follow-up (EditOrderAsync with tp/sl).");

        public Task<IReadOnlyList<ClosedTrade>> GetRecentClosedTradesAsync(DateTime sinceUtc)
            => throw new NotImplementedException("Hyperliquid auto-journal import is a later step.");

        public void Dispose() => _client?.Dispose();
    }
}