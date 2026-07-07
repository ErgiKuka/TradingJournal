using System.Collections.Generic;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Core.Logic.Manager
{
    /// <summary>
    /// Calculates the stop-loss price and TP ladder that follow from a chosen
    /// margin + leverage + entry, sized so a stop-out loses exactly the risk amount.
    ///
    /// This is a direct, verified port of the "SL &amp; TP from Leverage" spreadsheet.
    /// It is pure (no WinForms, no DB, no static state), so it can be unit-tested in isolation.
    /// </summary>
    public class LeverageSlTpCalculator
    {
        public LeverageSlTpResult Calculate(LeverageSlTpInput input)
        {
            var result = new LeverageSlTpResult();

            if (input == null)
            {
                result.Errors.Add("No input provided.");
                return result;
            }

            // --- Hard validation: anything here makes the numbers meaningless (division by zero, negative prices). ---
            if (input.AccountBalance <= 0) result.Errors.Add("Account balance must be greater than 0.");
            if (input.RiskFraction <= 0) result.Errors.Add("Risk per trade must be greater than 0.");
            if (input.EntryPrice <= 0) result.Errors.Add("Entry price must be greater than 0.");
            if (input.Margin <= 0) result.Errors.Add("Margin must be greater than 0.");
            if (input.Leverage <= 0) result.Errors.Add("Leverage must be greater than 0.");

            if (result.Errors.Count > 0)
                return result; // IsValid stays false, numeric fields stay 0.

            // --- Core figures ---
            result.RiskAmount = input.AccountBalance * input.RiskFraction;      // B7
            result.PositionSizeUsdt = input.Margin * input.Leverage;           // B14 (notional)
            result.PositionSizeCoins = result.PositionSizeUsdt / input.EntryPrice; // B15

            result.StopLossDistancePercent = result.RiskAmount / result.PositionSizeUsdt; // B19
            result.StopLossPrice = input.Direction == TradeDirection.Long                 // B20
                ? input.EntryPrice * (1m - result.StopLossDistancePercent)
                : input.EntryPrice * (1m + result.StopLossDistancePercent);

            result.LiquidationDistancePercent = 1m / input.Leverage;           // B21

            // --- Soft warnings (plan still computes, but flag the danger) ---
            if (result.RiskAmount > input.Margin) // B16
            {
                result.Warnings.Add(
                    "Risk amount exceeds your margin. You would be liquidated before the stop is reached — " +
                    "reduce risk % or increase margin.");
            }

            if (result.StopLossDistancePercent >= result.LiquidationDistancePercent) // B22
            {
                result.Warnings.Add(
                    "Stop-loss distance is at or beyond the approximate liquidation distance — " +
                    "reduce leverage or increase margin so the stop sits before liquidation.");
            }

            // --- Take-profit ladder ---
            decimal allocation = 0m;
            int level = 1;
            var takeProfits = input.TakeProfits ?? new List<TakeProfitInput>();

            foreach (var tp in takeProfits)
            {
                decimal movePercent = tp.SlMultiple * result.StopLossDistancePercent; // C
                decimal targetPrice = input.Direction == TradeDirection.Long           // D
                    ? input.EntryPrice * (1m + movePercent)
                    : input.EntryPrice * (1m - movePercent);

                decimal quantityUsdt = result.PositionSizeUsdt * (tp.PositionPercent / 100m); // F

                decimal priceDiff = input.Direction == TradeDirection.Long
                    ? targetPrice - input.EntryPrice
                    : input.EntryPrice - targetPrice;

                // P&L on the notional slice closed at this level:
                // priceDiff * (coin quantity for this slice) where coin qty = quantityUsdt / entry.
                decimal profitLoss = priceDiff * (quantityUsdt / input.EntryPrice); // G

                result.TakeProfits.Add(new TakeProfitResult
                {
                    Level = level,
                    SlMultiple = tp.SlMultiple,
                    PositionPercent = tp.PositionPercent,
                    MovePercent = movePercent,
                    TargetPrice = targetPrice,
                    QuantityUsdt = quantityUsdt,
                    ProfitLoss = profitLoss
                });

                allocation += tp.PositionPercent;
                result.TotalProfitLoss += profitLoss;
                level++;
            }

            // Margin and RiskAmount are guaranteed > 0 above, so these divisions are safe.
            result.ReturnOnMargin = result.TotalProfitLoss / input.Margin;   // D35
            result.RewardToRisk = result.TotalProfitLoss / result.RiskAmount; // D36

            result.AllocationIsComplete = allocation == 100m; // B31
            if (!result.AllocationIsComplete)
                result.Warnings.Add($"Take-profit allocation totals {allocation:0.##}% (should be 100%).");

            result.IsValid = true;
            return result;
        }
    }
}