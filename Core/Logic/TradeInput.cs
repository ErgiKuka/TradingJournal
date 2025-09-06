namespace TradingJournal.Core.Logic
{
    public class TradeInput
    {
        public string Symbol { get; set; }
        public string TradeType { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal ExitPrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal Margin { get; set; }
        public decimal ProfitLoss { get; set; }

        // Screenshots
        public string? ScreenshotLink { get; set; }    // optional link
        public byte[]? ScreenshotImage { get; set; }   // actual file bytes
    }
}
