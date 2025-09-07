using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Pl.PlaceHolder.Statistics
{
    public partial class FrmStatistics : Form
    {
        private FrmCalculator _calculatorFormInstance;

        public FrmStatistics()
        {
            InitializeComponent();

            RoundedFormHelper.RoundPanel(panel1, 20);
            RoundedFormHelper.RoundPanel(panel2, 20);
            RoundedFormHelper.RoundPanel(panel3, 20);
            RoundedFormHelper.RoundPanel(panel4, 20);
            RoundedFormHelper.RoundPanel(panel5, 20);
            RoundedFormHelper.RoundPanel(panel6, 20);
            RoundedFormHelper.RoundPanel(panel7, 20);
            RoundedFormHelper.RoundPanel(panel8, 20);

            RoundedFormHelper.MakeButtonRounded(btnOpenCalculator, 30);

        }
        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            List<Trade> trades;
            using (var db = new AppDbContext())
            {
                // Get the current date to use as a reference
                DateTime now = DateTime.Now.Date;
                IQueryable<Trade> query = db.Trades.AsQueryable();

                // --- THIS IS THE NEW LOGIC ---
                if (rbDaily.Checked)
                {
                    // Filter for trades that happened today
                    query = query.Where(t => t.Date >= now);
                }
                else if (rbWeekly.Checked)
                {
                    // Get the date 7 days ago
                    DateTime oneWeekAgo = now.AddDays(-7);
                    query = query.Where(t => t.Date >= oneWeekAgo);
                }
                else if (rbMonthly.Checked)
                {
                    // Get the date 30 days ago (a simple approximation for a month)
                    DateTime oneMonthAgo = now.AddDays(-30);
                    query = query.Where(t => t.Date >= oneMonthAgo);
                }
                // If rbAllTime is checked, we don't add any date filter, so it gets all trades.

                // Execute the query
                trades = query.ToList();
            }

            btnOpenCalculator.IconChar = IconChar.Calculator;
            btnOpenCalculator.IconColor = Color.Orange;
            btnOpenCalculator.IconSize = 25;
            btnOpenCalculator.TextImageRelation = TextImageRelation.ImageBeforeText;

            // 1. Generate the report
            var statsManager = new StatisticsManager();
            var report = statsManager.GenerateReport(trades);

            // 2. Populate the KPI labels
            UpdateKpiLabels(report);

            // 3. NEW: Update the chart
            UpdatePnlChart(report);
        }

        private void UpdateKpiLabels(TradingPerformanceReport report)
        {
            // Define our theme colors for consistency
            Color positiveColor = Color.FromArgb(46, 204, 113); // A nice green
            Color negativeColor = Color.FromArgb(231, 76, 60); // A nice red
            Color neutralColor = Color.White; // Or your default text color

            // --- Total PnL ---
            lblTotalPnlValue.Text = report.TotalPnL.ToString("C");
            lblTotalPnlValue.ForeColor = report.TotalPnL >= 0 ? positiveColor : negativeColor;

            // --- Win Rate ---
            lblWinRateValue.Text = $"{report.WinRate}%";
            lblWinRateValue.ForeColor = report.WinRate >= 50 ? positiveColor : negativeColor;

            // --- Profit Factor ---
            lblProfitFactorValue.Text = report.ProfitFactor.ToString("F2");
            lblProfitFactorValue.ForeColor = report.ProfitFactor >= 1 ? positiveColor : negativeColor;

            // --- Total Trades ---
            lblTotalTradesValue.Text = report.TotalTrades.ToString();
            lblTotalTradesValue.ForeColor = neutralColor; // Neutral stat

            // --- Average Winning Trade ---
            lblAvgWinValue.Text = report.AverageWinningTrade.ToString("C");
            lblAvgWinValue.ForeColor = positiveColor; // Always green

            // --- Average Losing Trade ---
            lblAvgLossValue.Text = report.AverageLosingTrade.ToString("C");
            lblAvgLossValue.ForeColor = negativeColor; // Always red

            //// --- Best Trade ---
            //lblBestTradeValue.Text = report.BestTrade.ToString("C");
            //lblBestTradeValue.ForeColor = positiveColor; // Always green

            //// --- Worst Trade ---
            //lblWorstTradeValue.Text = report.WorstTrade.ToString("C");
            //lblWorstTradeValue.ForeColor = negativeColor; // Always red
        }


        private void UpdatePnlChart(TradingPerformanceReport report)
        {
            // Clear any old data from the plot
            formsPlotPnl.Plot.Clear();

            // --- Style the Plot (This part is correct) ---
            formsPlotPnl.Plot.FigureBackground.Color = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(27, 38, 59));
            formsPlotPnl.Plot.DataBackground.Color = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(27, 38, 59));
            var xAxis = formsPlotPnl.Plot.Axes.Bottom;
            var yAxis = formsPlotPnl.Plot.Axes.Left;
            formsPlotPnl.Plot.Grid.MajorLineColor = ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(45, 51, 73));
            xAxis.Label.Text = "Date";
            yAxis.Label.Text = "Cumulative PnL";
            xAxis.Label.ForeColor = ScottPlot.Colors.White;
            yAxis.Label.ForeColor = ScottPlot.Colors.White;
            xAxis.TickLabelStyle.ForeColor = ScottPlot.Colors.LightGray;
            yAxis.TickLabelStyle.ForeColor = ScottPlot.Colors.LightGray;

            // --- Plot the Data ---
            if (report.PnlOverTime.Any())
            {
                // Convert PnL values to an array of doubles
                double[] pnlValues = report.PnlOverTime.Select(p => (double)p.Value).ToArray();

                // Add the PnL data as a simple signal plot (Y values only)
                var linePlot = formsPlotPnl.Plot.Add.Signal(pnlValues);
                linePlot.Color = ScottPlot.Colors.Blue;
                linePlot.LineWidth = 2;

                // 1. Create an array of positions for our ticks (0, 1, 2, ...)
                double[] tickPositions = Enumerable.Range(0, report.PnlOverTime.Count)
                                                   .Select(i => (double)i)
                                                   .ToArray();

                // 2. Create an array of labels from our dates, formatted as strings
                string[] tickLabels = report.PnlOverTime.Select(p => p.Date.ToString("M/d"))
                                                        .ToArray();

                // 3. Manually assign these positions and labels to the X-axis
                xAxis.TickGenerator = new ScottPlot.TickGenerators.NumericManual(tickPositions, tickLabels);
                xAxis.Label.Rotation = 45;
            }

            // Refresh the plot to display the new data and styling
            formsPlotPnl.Refresh();
        }

        private void rbDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDaily.Checked) LoadStatistics();
        }

        private void rbWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeekly.Checked) LoadStatistics();
        }

        private void vrbMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonthly.Checked) LoadStatistics();
        }

        private void rbAllTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllTime.Checked) LoadStatistics();
        }

        private void btnOpenCalculator_Click(object sender, EventArgs e)
        {
            if (_calculatorFormInstance == null || _calculatorFormInstance.IsDisposed)
            {
                // If not, create a new instance
                _calculatorFormInstance = new FrmCalculator();
                _calculatorFormInstance.Show();
            }
            else
            {
                // If it is already open, just bring it to the front
                _calculatorFormInstance.BringToFront();
            }
        }
    }
}
