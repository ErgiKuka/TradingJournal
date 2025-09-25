// TradingJournal.Core/Logic/Manager/StatisticsManager.cs

using System;
using System.Collections.Generic;
using System.Linq;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Helpers; // Assuming your report classes are here

namespace TradingJournal.Core.Logic.Manager
{

    public class StatisticsManager
    {
        public TradingPerformanceReport GenerateReport(List<Trade> trades)
        {
            var report = new TradingPerformanceReport();

            if (trades == null || !trades.Any())
            {
                return report; // Return an empty report
            }

            var orderedTrades = trades.OrderBy(t => t.Date).ToList();

            report.TotalTrades = orderedTrades.Count;
            report.TotalPnL = orderedTrades.Sum(t => t.ProfitLoss);

            var winningTrades = orderedTrades.Where(t => t.ProfitLoss > 0).ToList();
            var losingTrades = orderedTrades.Where(t => t.ProfitLoss < 0).ToList();

            // Win Rate
            if (report.TotalTrades > 0)
            {
                report.WinRate = Math.Round((double)winningTrades.Count / report.TotalTrades * 100, 2);
            }

            // Gross Profits and Losses
            decimal grossProfits = winningTrades.Sum(t => t.ProfitLoss);
            decimal grossLosses = Math.Abs(losingTrades.Sum(t => t.ProfitLoss));

            // Profit Factor
            if (grossLosses > 0)
            {
                report.ProfitFactor = Math.Round(grossProfits / grossLosses, 2);
            }
            else if (grossProfits > 0)
            {
                report.ProfitFactor = 999; // A large number to represent "infinite"
            }

            // Averages
            if (winningTrades.Any())
            {
                report.AverageWinningTrade = Math.Round(winningTrades.Average(t => t.ProfitLoss), 2);
            }
            if (losingTrades.Any())
            {
                report.AverageLosingTrade = Math.Round(losingTrades.Average(t => t.ProfitLoss), 2);
            }

            // Best and Worst Trades
            if (orderedTrades.Any())
            {
                report.BestTrade = orderedTrades.Max(t => t.ProfitLoss);
                report.WorstTrade = orderedTrades.Min(t => t.ProfitLoss);
            }

            // Data for the chart: Cumulative PnL and Individual PnLs
            decimal cumulativePnl = 0;
            foreach (var trade in orderedTrades)
            {
                cumulativePnl += trade.ProfitLoss;
                report.PnlOverTime.Add(new DataPoint { Date = trade.Date, Value = cumulativePnl });

                // *** THIS IS THE NEW PART ***
                // Store the individual PnL of each trade in sequence
                report.IndividualPnLs.Add(trade.ProfitLoss);
            }

            return report;
        }
    }
}
