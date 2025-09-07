using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Core.Logic.Manager
{
    public class StatisticsManager
    {
        public TradingPerformanceReport GenerateReport(List<Trade> trades)
        {
            var report = new TradingPerformanceReport();

            if (trades == null || !trades.Any())
            {
                return report; // Return an empty report if there are no trades
            }

            report.TotalTrades = trades.Count;
            report.TotalPnL = trades.Sum(t => t.ProfitLoss);

            var winningTrades = trades.Where(t => t.ProfitLoss > 0).ToList();
            var losingTrades = trades.Where(t => t.ProfitLoss < 0).ToList();

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
            if (trades.Any())
            {
                report.BestTrade = trades.Max(t => t.ProfitLoss);
                report.WorstTrade = trades.Min(t => t.ProfitLoss);
            }

            // Data for the chart: Cumulative PnL over time
            decimal cumulativePnl = 0;
            foreach (var trade in trades.OrderBy(t => t.Date))
            {
                cumulativePnl += trade.ProfitLoss;
                report.PnlOverTime.Add(new DataPoint { Date = trade.Date, Value = cumulativePnl });
            }

            return report;
        }
    }
}
