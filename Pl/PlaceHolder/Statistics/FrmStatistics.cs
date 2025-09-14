// FrmStatistics.cs

using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic; // For IResponsiveChildForm
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager; // For ResponsiveLayoutManager
using TradingJournal.Core.Managers; // For StatisticsManager

namespace TradingJournal.Pl.PlaceHolder.Statistics
{
    public partial class FrmStatistics : Form, IResponsiveChildForm
    {
        private readonly ResponsiveLayoutManager _layoutManager;
        private FrmCalculator _calculatorFormInstance;

        public FrmStatistics()
        {
            InitializeComponent();

            // Initialize the manager and configure all responsive layouts
            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            // Round the corners of all panels
            RoundAllPanels();
        }

        private void RoundAllPanels()
        {
            // Normal State Panels
            RoundedFormHelper.RoundPanel(pnlTotalPnl, 20);
            RoundedFormHelper.RoundPanel(pnlWinRate, 20);
            RoundedFormHelper.RoundPanel(pnlProfitFactor, 20);
            RoundedFormHelper.RoundPanel(pnlTotalTrades, 20);
            RoundedFormHelper.RoundPanel(pnlAvgWinning, 20);
            RoundedFormHelper.RoundPanel(pnlAvgLosing, 20);
            RoundedFormHelper.RoundPanel(pnlChart, 20);
            RoundedFormHelper.RoundPanel(pnlCalculator, 20);
            RoundedFormHelper.MakeButtonRounded(btnOpenCalculator, 30);

            // Maximized State Panels
            RoundedFormHelper.RoundPanel(pnlTotalPnl_Max, 20);
            RoundedFormHelper.RoundPanel(pnlWinRate_Max, 20);
            RoundedFormHelper.RoundPanel(pnlProfitFactor_Max, 20);
            RoundedFormHelper.RoundPanel(pnlTotalTrades_Max, 20);
            RoundedFormHelper.RoundPanel(pnlAvgWinning_Max, 20);
            RoundedFormHelper.RoundPanel(pnlAvgLosing_Max, 20);
            RoundedFormHelper.RoundPanel(pnlChart_Max, 20);
            RoundedFormHelper.RoundPanel(pnlCalculator_Max, 20);
        }

        private void InitializeResponsiveLayouts()
        {
            // --- Register Top Statistics Panels and their Children ---
            _layoutManager.RegisterControl(lblTotalPnlValue, pnlTotalPnl, pnlTotalPnl_Max, new Point(62, 94), new Size(23, 26));
            _layoutManager.RegisterControl(lblSymbol, pnlTotalPnl, pnlTotalPnl_Max, new Point(44, 50), new Size(95, 26));

            _layoutManager.RegisterControl(lblWinRateValue, pnlWinRate, pnlWinRate_Max, new Point(65, 94), new Size(23, 26));
            _layoutManager.RegisterControl(label5, pnlWinRate, pnlWinRate_Max, new Point(44, 50), new Size(97, 26));

            _layoutManager.RegisterControl(lblProfitFactorValue, pnlProfitFactor, pnlProfitFactor_Max, new Point(70, 94), new Size(23, 26));
            _layoutManager.RegisterControl(label2, pnlProfitFactor, pnlProfitFactor_Max, new Point(30, 50), new Size(130, 26));

            _layoutManager.RegisterControl(lblTotalTradesValue, pnlTotalTrades, pnlTotalTrades_Max, new Point(75, 94), new Size(23, 26));
            _layoutManager.RegisterControl(label7, pnlTotalTrades, pnlTotalTrades_Max, new Point(30, 50), new Size(126, 26));

            _layoutManager.RegisterControl(lblAvgWinValue, pnlAvgWinning, pnlAvgWinning_Max, new Point(63, 94), new Size(23, 26));
            _layoutManager.RegisterControl(label9, pnlAvgWinning, pnlAvgWinning_Max, new Point(4, 50), new Size(193, 26));

            _layoutManager.RegisterControl(lblAvgLossValue, pnlAvgLosing, pnlAvgLosing_Max, new Point(60, 94), new Size(23, 26));
            _layoutManager.RegisterControl(label11, pnlAvgLosing, pnlAvgLosing_Max, new Point(10, 50), new Size(177, 26));

            // --- Register Chart Panel and its Children ---
            _layoutManager.RegisterControl(formsPlotPnl, pnlChart, pnlChart_Max, new Point(41, 58), new Size(1507, 435));
            _layoutManager.RegisterControl(rbDaily, pnlChart, pnlChart_Max, new Point(563, 14), new Size(75, 26));
            _layoutManager.RegisterControl(rbWeekly, pnlChart, pnlChart_Max, new Point(683, 14), new Size(89, 26));
            _layoutManager.RegisterControl(rbMonthly, pnlChart, pnlChart_Max, new Point(815, 14), new Size(96, 26));
            _layoutManager.RegisterControl(rbAllTime, pnlChart, pnlChart_Max, new Point(939, 14), new Size(96, 26));

            // --- Register Calculator Panel and its Children ---
            _layoutManager.RegisterControl(btnOpenCalculator, pnlCalculator, pnlCalculator_Max, new Point(660, 55), new Size(279, 71));
        }

        // This method correctly delegates the state change to the manager
        public void SetWindowState(FormWindowStateExtended newState)
        {
            _layoutManager.SetWindowState(newState);
        }

        #region --- Your Original Logic (Restored) ---

        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            List<Trade> trades;
            using (var db = new AppDbContext())
            {
                DateTime now = DateTime.Now.Date;
                IQueryable<Trade> query = db.Trades.AsQueryable();

                if (rbDaily.Checked)
                {
                    query = query.Where(t => t.Date.Date == now);
                }
                else if (rbWeekly.Checked)
                {
                    DateTime oneWeekAgo = now.AddDays(-7);
                    query = query.Where(t => t.Date >= oneWeekAgo && t.Date <= now);
                }
                else if (rbMonthly.Checked)
                {
                    DateTime oneMonthAgo = now.AddDays(-30);
                    query = query.Where(t => t.Date >= oneMonthAgo && t.Date <= now);
                }
                // If rbAllTime is checked, no date filter is applied.

                trades = query.ToList();
            }

            btnOpenCalculator.IconChar = IconChar.Calculator;
            btnOpenCalculator.IconColor = Color.Orange;
            btnOpenCalculator.IconSize = 25;
            btnOpenCalculator.TextImageRelation = TextImageRelation.ImageBeforeText;

            var statsManager = new StatisticsManager();
            var report = statsManager.GenerateReport(trades);

            UpdateKpiLabels(report);
            UpdatePnlChart(report);
        }

        private void UpdateKpiLabels(TradingPerformanceReport report)
        {
            Color positiveColor = Color.FromArgb(46, 204, 113);
            Color negativeColor = Color.FromArgb(231, 76, 60);
            Color neutralColor = Color.White;

            lblTotalPnlValue.Text = report.TotalPnL.ToString("C");
            lblTotalPnlValue.ForeColor = report.TotalPnL >= 0 ? positiveColor : negativeColor;

            lblWinRateValue.Text = $"{report.WinRate:F2}%"; // Using F2 for better formatting
            lblWinRateValue.ForeColor = report.WinRate >= 50 ? positiveColor : negativeColor;

            lblProfitFactorValue.Text = report.ProfitFactor.ToString("F2");
            lblProfitFactorValue.ForeColor = report.ProfitFactor >= 1 ? positiveColor : negativeColor;

            lblTotalTradesValue.Text = report.TotalTrades.ToString();
            lblTotalTradesValue.ForeColor = neutralColor;

            lblAvgWinValue.Text = report.AverageWinningTrade.ToString("C");
            lblAvgWinValue.ForeColor = positiveColor;

            lblAvgLossValue.Text = report.AverageLosingTrade.ToString("C");
            lblAvgLossValue.ForeColor = negativeColor;
        }

        private void UpdatePnlChart(TradingPerformanceReport report)
        {
            formsPlotPnl.Plot.Clear();

            formsPlotPnl.Plot.FigureBackground.Color = ScottPlot.Color.FromColor(Color.FromArgb(27, 38, 59));
            formsPlotPnl.Plot.DataBackground.Color = ScottPlot.Color.FromColor(Color.FromArgb(27, 38, 59));
            var xAxis = formsPlotPnl.Plot.Axes.Bottom;
            var yAxis = formsPlotPnl.Plot.Axes.Left;
            formsPlotPnl.Plot.Grid.MajorLineColor = ScottPlot.Color.FromColor(Color.FromArgb(45, 51, 73));
            xAxis.Label.Text = "Date";
            yAxis.Label.Text = "Cumulative PnL";
            xAxis.Label.ForeColor = ScottPlot.Colors.White;
            yAxis.Label.ForeColor = ScottPlot.Colors.White;
            xAxis.TickLabelStyle.ForeColor = ScottPlot.Colors.LightGray;
            yAxis.TickLabelStyle.ForeColor = ScottPlot.Colors.LightGray;

            if (report.PnlOverTime.Any())
            {
                double[] pnlValues = report.PnlOverTime.Select(p => (double)p.Value).ToArray();
                var linePlot = formsPlotPnl.Plot.Add.Signal(pnlValues);
                linePlot.Color = ScottPlot.Colors.Blue;
                linePlot.LineWidth = 2;

                double[] tickPositions = Enumerable.Range(0, report.PnlOverTime.Count).Select(i => (double)i).ToArray();
                string[] tickLabels = report.PnlOverTime.Select(p => p.Date.ToString("M/d")).ToArray();

                xAxis.TickGenerator = new ScottPlot.TickGenerators.NumericManual(tickPositions, tickLabels);
                xAxis.Label.Rotation = 45;
            }

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
                _calculatorFormInstance = new FrmCalculator();
                _calculatorFormInstance.Show();
            }
            else
            {
                _calculatorFormInstance.BringToFront();
            }
        }

        #endregion
    }
}
