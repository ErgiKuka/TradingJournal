using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingJournal.Core.Logic.Helpers
{
    public class TradingPerformanceReport
    {
        public decimal TotalPnL { get; set; }
        public double WinRate { get; set; }
        public decimal ProfitFactor { get; set; }
        public int TotalTrades { get; set; }
        public decimal AverageWinningTrade { get; set; }
        public decimal AverageLosingTrade { get; set; }
        public decimal BestTrade { get; set; }
        public decimal WorstTrade { get; set; }
        public List<DataPoint> PnlOverTime { get; set; } = new List<DataPoint>();
    }

    // A helper class to hold data for our chart
    public class DataPoint
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
