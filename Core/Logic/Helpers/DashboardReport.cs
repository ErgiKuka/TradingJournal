using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingJournal.Core.Logic.Helpers
{
    public class DashboardReport
    {
        public decimal TotalPortfolioValue { get; set; }
        public decimal TotalTradingAccountBalance { get; set; }
        public decimal TodaysPnL { get; set; }
    }
}
