using System;
using System.Threading.Tasks;
using TradingJournal.Core.Logic.Services.Exchange;

namespace TradingJournal.Core.Logic.Manager
{
    /// <summary>What the user asked for in the Trade form (before sizing/precision).</summary>
    public sealed class NewTradeRequest
    {
        public string Symbol { get; set; } = string.Empty;
        public OrderSide Side { get; set; }
        public OrderKind Kind { get; set; }
        public decimal UsdtAmount { get; set; }     // MARGIN the user commits (what they spend)
        public int Leverage { get; set; }
        public MarginMode MarginMode { get; set; }
        public decimal? LimitPrice { get; set; }
        public decimal? TriggerPrice { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
    }

    public readonly struct TradeOutcome
    {
        public bool Ok { get; }
        public string Message { get; }
        private TradeOutcome(bool ok, string msg) { Ok = ok; Message = msg; }
        public static TradeOutcome Success(string m) => new TradeOutcome(true, m);
        public static TradeOutcome Fail(string m) => new TradeOutcome(false, m);
    }

    /// <summary>
    /// Turns a margin-sized request into a precise, affordable exchange order.
    ///
    /// Sizing model: the user enters the MARGIN they commit (UsdtAmount). Position notional =
    /// margin * leverage; quantity = notional / price (floored to the symbol's step). The cap is
    /// simply margin &lt;= available balance.
    /// </summary>
    public class TradingManager
    {
        public async Task<(bool ok, string preview, OrderRequest order, decimal margin)> PrepareAsync(
            IExchangeClient client, NewTradeRequest r)
        {
            if (string.IsNullOrWhiteSpace(r.Symbol)) return (false, "Select a symbol.", null, 0);
            if (r.UsdtAmount <= 0) return (false, "Enter a USDT amount greater than 0.", null, 0);
            if (r.Leverage <= 0) return (false, "Leverage must be greater than 0.", null, 0);
            if (r.Kind == OrderKind.Limit && !(r.LimitPrice > 0)) return (false, "Enter a limit price.", null, 0);
            if (r.Kind == OrderKind.Conditional && !(r.TriggerPrice > 0)) return (false, "Enter a trigger price.", null, 0);

            decimal margin = r.UsdtAmount; // what the user spends

            var balance = await client.GetBalanceAsync();
            if (margin > balance.AvailableUsdt)
                return (false, $"This needs {margin:0.##} USDT margin but only {balance.AvailableUsdt:0.##} is available.", null, 0);

            var rules = await client.GetSymbolRulesAsync(r.Symbol);

            decimal sizingPrice =
                r.Kind == OrderKind.Limit ? r.LimitPrice.Value :
                r.Kind == OrderKind.Conditional ? r.TriggerPrice.Value :
                (await client.GetPriceAsync(r.Symbol)).Price;
            if (sizingPrice <= 0) return (false, "Couldn't get a price to size the order.", null, 0);

            decimal notional = margin * r.Leverage;
            decimal qty = FloorToStep(notional / sizingPrice, rules.StepSize);
            if (qty <= 0)
                return (false, $"Margin too small for {r.Symbol} at {r.Leverage}x (step {Trim(rules.StepSize)}).", null, 0);

            decimal actualNotional = qty * sizingPrice;
            if (rules.MinNotional > 0 && actualNotional < rules.MinNotional)
            {
                return (false, $"Position value {actualNotional:0.##} USDT is below the exchange minimum of {rules.MinNotional:0.##}. Increase margin or leverage.", null, 0);
            }


            // --- Leverage-bracket guard: turn Binance -4028 / -2027 into a clear pre-submit message. ---
            var bracket = await client.GetLeverageBracketAsync(r.Symbol);

            if (r.Leverage > bracket.MaxLeverage)
                return (false,
                    $"{r.Symbol} allows at most {bracket.MaxLeverage}x leverage — you entered {r.Leverage}x.",
                    null, 0);

            decimal maxNotional = bracket.MaxNotionalAt(r.Leverage);
            if (maxNotional > 0 && actualNotional > maxNotional)
            {
                int allowedLev = bracket.MaxLeverageForNotional(actualNotional);
                decimal maxMargin = maxNotional / r.Leverage;
                return (false,
                    $"At {r.Leverage}x, {r.Symbol} caps the position at {maxNotional:0.##} USDT " +
                    $"(~{maxMargin:0.##} USDT margin). Reduce the amount, or drop leverage to " +
                    $"{allowedLev}x for a position this size.",
                    null, 0);
            }

            var order = new OrderRequest
            {
                Symbol = r.Symbol,
                Side = r.Side,
                Kind = r.Kind,
                Quantity = qty,
                Price = r.Kind == OrderKind.Limit ? RoundToTick(r.LimitPrice.Value, rules.TickSize) : (decimal?)null,
                TriggerPrice = r.Kind == OrderKind.Conditional ? RoundToTick(r.TriggerPrice.Value, rules.TickSize) : (decimal?)null,
                StopLoss = r.StopLoss.HasValue ? RoundToTick(r.StopLoss.Value, rules.TickSize) : (decimal?)null,
                TakeProfit = r.TakeProfit.HasValue ? RoundToTick(r.TakeProfit.Value, rules.TickSize) : (decimal?)null
            };

            string dir = r.Side == OrderSide.Buy ? "LONG" : "SHORT";
            string preview =
                $"{dir} {Trim(qty)} {r.Symbol}  ({r.Kind}, {r.Leverage}x {r.MarginMode})\n" +
                $"Margin ~{margin:0.##} USDT · position ~{actualNotional:0.##} USDT" +
                (order.StopLoss.HasValue ? $"\nSL {Trim(order.StopLoss.Value)}" : "") +
                (order.TakeProfit.HasValue ? $"  TP {Trim(order.TakeProfit.Value)}" : "");

            return (true, preview, order, margin);
        }

        public async Task<TradeOutcome> PlaceAsync(IExchangeClient client, OrderRequest order, int leverage, MarginMode mode)
        {
            try
            {
                await client.SetLeverageAsync(order.Symbol, leverage, mode);
                var res = await client.PlaceOrderAsync(order);
                return res.Success ? TradeOutcome.Success(res.Message) : TradeOutcome.Fail(res.Message);
            }
            catch (Exception ex)
            {
                return TradeOutcome.Fail(ex.Message);
            }
        }

        private static decimal FloorToStep(decimal value, decimal step)
            => step <= 0 ? value : Math.Floor(value / step) * step;

        private static decimal RoundToTick(decimal value, decimal tick)
            => tick <= 0 ? value : Math.Round(value / tick, MidpointRounding.AwayFromZero) * tick;

        private static string Trim(decimal v) => v.ToString("0.########", System.Globalization.CultureInfo.InvariantCulture);
    }
}