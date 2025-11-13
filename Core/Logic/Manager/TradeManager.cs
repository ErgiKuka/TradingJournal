using System;
using System.IO;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Core.Managers
{
    public class TradeManager
    {
        public void AddTrade(string symbol, string tradeType,
                     decimal entryPrice, decimal exitPrice,
                     decimal stopLoss, decimal takeProfit,
                     decimal margin, decimal profitLoss,
                     DateTime date,
                     string screenshotLink = null,
                     string screenshotFilePath = null)
        {
            byte[] imageBytes = null;

            if (!string.IsNullOrEmpty(screenshotFilePath) && File.Exists(screenshotFilePath))
                imageBytes = File.ReadAllBytes(screenshotFilePath);

            if (profitLoss == 0 && exitPrice > 0 && entryPrice > 0)
            {
                decimal assetQuantity = margin / entryPrice;
                decimal calculatedPnl;
                if (tradeType.Equals("Long", StringComparison.OrdinalIgnoreCase))
                    calculatedPnl = (exitPrice - entryPrice) * assetQuantity;
                else
                    calculatedPnl = (entryPrice - exitPrice) * assetQuantity;

                profitLoss = Math.Round(calculatedPnl, 2);
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
                ScreenshotImage = imageBytes,
                Date = date
            };

            using var db = new AppDbContext();
            db.Trades.Add(trade);
            db.SaveChanges();
        }

        public void DeleteTrade(int id, int? retentionDays = null)
        {
            using var db = new AppDbContext();
            var t = db.Trades.Find(id);
            if (t == null) return;

            var days = retentionDays ?? SettingsManager.Load().RecycleBin.RetentionDays;
            RecycleBinService.CaptureTradeAsync(db, t, days, "Trade deleted").GetAwaiter().GetResult();

            db.Trades.Remove(t);
            db.SaveChanges();
        }

        public void UpdateTrade(int tradeId, string symbol, string tradeType,
                     decimal entryPrice, decimal exitPrice,
                     decimal stopLoss, decimal takeProfit,
                     decimal margin, decimal profitLoss,
                     DateTime date,
                     string screenshotFilePath = null)
        {
            using var db = new AppDbContext();
            var tradeToUpdate = db.Trades.Find(tradeId);
            if (tradeToUpdate == null) return;

            if (profitLoss == 0 || tradeToUpdate.ExitPrice != exitPrice)
            {
                if (exitPrice > 0 && entryPrice > 0)
                {
                    decimal assetQuantity = margin / entryPrice;
                    decimal calculatedPnl;
                    if (tradeType.Equals("Long", StringComparison.OrdinalIgnoreCase))
                        calculatedPnl = (exitPrice - entryPrice) * assetQuantity;
                    else
                        calculatedPnl = (entryPrice - exitPrice) * assetQuantity;
                    profitLoss = Math.Round(calculatedPnl, 2);
                }
            }

            tradeToUpdate.Symbol = symbol;
            tradeToUpdate.TradeType = tradeType;
            tradeToUpdate.EntryPrice = entryPrice;
            tradeToUpdate.ExitPrice = exitPrice;
            tradeToUpdate.StopLoss = stopLoss;
            tradeToUpdate.TakeProfit = takeProfit;
            tradeToUpdate.Margin = margin;
            tradeToUpdate.ProfitLoss = profitLoss;
            tradeToUpdate.Date = date;

            if (!string.IsNullOrEmpty(screenshotFilePath) && File.Exists(screenshotFilePath))
                tradeToUpdate.ScreenshotImage = File.ReadAllBytes(screenshotFilePath);

            db.SaveChanges();
        }
    }
}
