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
        private readonly SettingsManager _settings;

        public DashboardManager(SettingsManager settings)
        {
            _settings = settings;
        }

        public DashboardReport GenerateReport(List<Trade> allTrades)
        {
            var report = new DashboardReport();

            if (allTrades == null || !allTrades.Any())
            {
                report.TotalPortfolioValue = _settings.AccountBalance;
                report.TotalTradingAccountBalance = 0;
                report.TodaysPnL = 0;
                return report;
            }

            decimal totalPnL = allTrades.Sum(t => t.ProfitLoss);

            // This is the new base balance from settings
            report.TotalPortfolioValue = _settings.AccountBalance + totalPnL;
            report.TotalTradingAccountBalance = totalPnL;

            DateTime today = DateTime.Now.Date;
            report.TodaysPnL = allTrades
                .Where(t => t.Date.Date == today)
                .Sum(t => t.ProfitLoss);

            return report;
        }
    }
}
