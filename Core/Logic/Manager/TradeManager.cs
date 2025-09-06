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

            // Calculate PnL if not manually entered
            if (profitLoss == 0 && exitPrice > 0 && entryPrice > 0)
            {
                decimal assetQuantity = margin / entryPrice;
                decimal calculatedPnl; // Use a temporary variable

                if (tradeType.Equals("Long", StringComparison.OrdinalIgnoreCase))
                {
                    calculatedPnl = (exitPrice - entryPrice) * assetQuantity;
                }
                else // It's a "Short"
                {
                    calculatedPnl = (entryPrice - exitPrice) * assetQuantity;
                }

                // --- ROUND THE RESULT ---
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
                Date = DateTime.Now
            };

            using (var db = new AppDbContext())
            {
                db.Trades.Add(trade);
                db.SaveChanges();
            }
        }

        public void UpdateTrade(int tradeId, string symbol, string tradeType,
                     decimal entryPrice, decimal exitPrice,
                     decimal stopLoss, decimal takeProfit,
                     decimal margin, decimal profitLoss,
                     string screenshotFilePath = null) // Allow updating screenshots too
        {
            using (var db = new AppDbContext())
            {
                var tradeToUpdate = db.Trades.Find(tradeId);
                if (tradeToUpdate == null)
                {
                    // Or throw an exception, depending on how you want to handle errors
                    return;
                }

                // --- RE-RUN THE CALCULATION LOGIC ---
                // If the user cleared the PnL box, or if the exit price changed, recalculate.
                if (profitLoss == 0 || tradeToUpdate.ExitPrice != exitPrice)
                {
                    if (exitPrice > 0 && entryPrice > 0)
                    {
                        decimal assetQuantity = margin / entryPrice;
                        decimal calculatedPnl; // Use a temporary variable

                        if (tradeType.Equals("Long", StringComparison.OrdinalIgnoreCase))
                        {
                            calculatedPnl = (exitPrice - entryPrice) * assetQuantity;
                        }
                        else // It's a "Short"
                        {
                            calculatedPnl = (entryPrice - exitPrice) * assetQuantity;
                        }

                        // --- ROUND THE RESULT ---
                        profitLoss = Math.Round(calculatedPnl, 2);
                    }
                }

                // --- Update the entity's properties ---
                tradeToUpdate.Symbol = symbol;
                tradeToUpdate.TradeType = tradeType;
                tradeToUpdate.EntryPrice = entryPrice;
                tradeToUpdate.ExitPrice = exitPrice;
                tradeToUpdate.StopLoss = stopLoss;
                tradeToUpdate.TakeProfit = takeProfit;
                tradeToUpdate.Margin = margin;
                tradeToUpdate.ProfitLoss = profitLoss; // Assign the newly calculated or user-provided PnL

                // Optional: Handle screenshot updates
                if (!string.IsNullOrEmpty(screenshotFilePath) && File.Exists(screenshotFilePath))
                {
                    tradeToUpdate.ScreenshotImage = File.ReadAllBytes(screenshotFilePath);
                }

                db.SaveChanges();
            }
        }

    }
}
