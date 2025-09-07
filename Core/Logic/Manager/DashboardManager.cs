using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Core.Logic.Manager
{
    public class DashboardManager
    {
        public DashboardReport GenerateReport(List<Trade> allTrades)
        {
            var report = new DashboardReport();

            if (allTrades == null || !allTrades.Any())
            {
                return report; // Return an empty report (all zeros)
            }

            // 1. Calculate Total Portfolio Value (the sum of all PnL ever)
            report.TotalPortfolioValue = allTrades.Sum(t => t.ProfitLoss);

            // 2. Calculate Today's PnL
            DateTime today = DateTime.Now.Date;
            report.TodaysPnL = allTrades
                .Where(t => t.Date.Date == today)
                .Sum(t => t.ProfitLoss);

            return report;
        }
    }
}
