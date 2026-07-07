using System.Collections.Generic;

namespace TradingJournal.Core.Logic.Helpers
{
    /// <summary>
    /// Trade side. Kept independent of the EF Trade entity on purpose:
    /// pure calculation logic should not depend on persistence types.
    /// Map to/from your existing "Long"/"Short" strings at the UI boundary.
    /// </summary>
    public enum TradeDirection
    {
        Long,
        Short
    }

    /// <summary>
    /// One planned take-profit level (an input row).
    /// Mirrors the "SL &amp; TP from Leverage" sheet, columns B (multiple) and E (% of position).
    /// </summary>
    public class TakeProfitInput
    {
        /// <summary>How many multiples of the SL distance % this TP sits from entry (e.g. 2 = double the SL distance, in profit direction).</summary>
        public decimal SlMultiple { get; set; }

        /// <summary>Share of the position closed at this level, expressed 0..100 (e.g. 34 for 34%).</summary>
        public decimal PositionPercent { get; set; }
    }

    /// <summary>
    /// All user inputs for the SL/TP-from-leverage calculation.
    /// This is a plain data object with no behaviour so it is trivial to unit-test the engine.
    /// </summary>
    public class LeverageSlTpInput
    {
        public decimal AccountBalance { get; set; }

        /// <summary>Risk per trade as a FRACTION, not a percent (0.02 = 2%). The form converts "2" -&gt; 0.02.</summary>
        public decimal RiskFraction { get; set; }

        public TradeDirection Direction { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal Margin { get; set; }
        public decimal Leverage { get; set; }

        public IReadOnlyList<TakeProfitInput> TakeProfits { get; set; } = new List<TakeProfitInput>();
    }

    /// <summary>One computed take-profit level (an output row).</summary>
    public class TakeProfitResult
    {
        public int Level { get; set; }
        public decimal SlMultiple { get; set; }
        public decimal PositionPercent { get; set; }

        /// <summary>Price move from entry as a FRACTION (0.0123 = 1.23%).</summary>
        public decimal MovePercent { get; set; }

        public decimal TargetPrice { get; set; }

        /// <summary>Notional (USDT) closed at this level.</summary>
        public decimal QuantityUsdt { get; set; }

        public decimal ProfitLoss { get; set; }
    }

    /// <summary>
    /// Full result of the calculation. Splits HARD errors (block calculation) from
    /// SOFT warnings (calculation still runs, but the plan is risky) so the UI can react appropriately.
    /// </summary>
    public class LeverageSlTpResult
    {
        public bool IsValid { get; set; }

        /// <summary>Hard validation failures. When any exist, numeric fields are left at 0 and IsValid is false.</summary>
        public List<string> Errors { get; } = new List<string>();

        /// <summary>Non-fatal warnings (margin check, liquidation proximity, allocation != 100%).</summary>
        public List<string> Warnings { get; } = new List<string>();

        public decimal RiskAmount { get; set; }

        /// <summary>Position size as USDT notional (margin * leverage).</summary>
        public decimal PositionSizeUsdt { get; set; }

        public decimal PositionSizeCoins { get; set; }

        /// <summary>Stop-loss distance from entry as a FRACTION (risk amount / notional).</summary>
        public decimal StopLossDistancePercent { get; set; }

        public decimal StopLossPrice { get; set; }

        /// <summary>Rough liquidation distance as a FRACTION (1 / leverage). Ignores maintenance margin and fees.</summary>
        public decimal LiquidationDistancePercent { get; set; }

        public List<TakeProfitResult> TakeProfits { get; } = new List<TakeProfitResult>();

        public decimal TotalProfitLoss { get; set; }

        /// <summary>Total P&amp;L / margin (as a fraction; 1.83 = 183%).</summary>
        public decimal ReturnOnMargin { get; set; }

        /// <summary>Total P&amp;L / risk amount (R multiple).</summary>
        public decimal RewardToRisk { get; set; }

        public bool AllocationIsComplete { get; set; }
    }
}