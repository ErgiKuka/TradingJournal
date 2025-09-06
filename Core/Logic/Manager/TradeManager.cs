using System;
using System.IO;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;

namespace TradingJournal.Core.Managers
{
    public class TradeManager
    {
        public void AddTrade(string symbol, string tradeType,
                             decimal entryPrice, decimal exitPrice,
                             decimal stopLoss, decimal takeProfit,
                             decimal margin, decimal profitLoss,
                             string screenshotLink = null,
                             string screenshotFilePath = null)
        {
            byte[] imageBytes = null;

            if (!string.IsNullOrEmpty(screenshotFilePath) && File.Exists(screenshotFilePath))
            {
                imageBytes = File.ReadAllBytes(screenshotFilePath);
            }

            var trade = new Trade
            {
                Symbol = symbol,
                TradeType = tradeType,
                EntryPrice = entryPrice,
                ExitPrice = exitPrice,
                StopLoss = stopLoss,
                TakeProfit = takeProfit,
                Margin = margin,
                ProfitLoss = profitLoss,
                ScreenshotLink = screenshotLink,
                ScreenshotImage = imageBytes
            };

            using (var db = new AppDbContext())
            {
                db.Trades.Add(trade);
                db.SaveChanges();
            }
        }
    }
}
