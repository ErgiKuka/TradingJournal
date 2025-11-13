using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Managers;
using ScottPlot;

namespace TradingJournal.Pl.PlaceHolder.Statistics
{
    public partial class FrmStatistics : UserControl, IResponsiveChildForm
    {
        private readonly ResponsiveLayoutManager _layoutManager;
        private FrmCalculator? _calculatorFormInstance;
        private enum ChartResolution { Intraday, Daily }

        public FrmStatistics()
        {
            InitializeComponent();
            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();
            RoundAllPanels();
            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            // Form background
            this.BackColor = ThemeManager.BackgroundColor;

            // Panels
            pnlTotalPnl.BackColor = ThemeManager.PanelColor;
            pnlWinRate.BackColor = ThemeManager.PanelColor;
            pnlProfitFactor.BackColor = ThemeManager.PanelColor;
            pnlTotalTrades.BackColor = ThemeManager.PanelColor;
            pnlAvgWinning.BackColor = ThemeManager.PanelColor;
            pnlAvgLosing.BackColor = ThemeManager.PanelColor;
            pnlChart.BackColor = ThemeManager.PanelColor;
            pnlCalculator.BackColor = ThemeManager.PanelColor;

            pnlTotalPnl_Max.BackColor = ThemeManager.PanelColor;
            pnlWinRate_Max.BackColor = ThemeManager.PanelColor;
            pnlProfitFactor_Max.BackColor = ThemeManager.PanelColor;
            pnlTotalTrades_Max.BackColor = ThemeManager.PanelColor;
            pnlAvgWinning_Max.BackColor = ThemeManager.PanelColor;
            pnlAvgLosing_Max.BackColor = ThemeManager.PanelColor;
            pnlChart_Max.BackColor = ThemeManager.PanelColor;
            pnlCalculator_Max.BackColor = ThemeManager.PanelColor;

            // Labels (neutral labels follow theme color)
            lblSymbol.ForeColor = ThemeManager.TextColor;
            label5.ForeColor = ThemeManager.TextColor;
            label2.ForeColor = ThemeManager.TextColor;
            label7.ForeColor = ThemeManager.TextColor;
            label9.ForeColor = ThemeManager.TextColor;
            label11.ForeColor = ThemeManager.TextColor;

            // KPI values (reset base color, UpdateKpiLabels will recolor later)
            lblTotalPnlValue.ForeColor = ThemeManager.TextColor;
            lblWinRateValue.ForeColor = ThemeManager.TextColor;
            lblProfitFactorValue.ForeColor = ThemeManager.TextColor;
            lblTotalTradesValue.ForeColor = ThemeManager.TextColor;
            lblAvgWinValue.ForeColor = ThemeManager.TextColor;
            lblAvgLossValue.ForeColor = ThemeManager.TextColor;

            // Radio buttons
            rbDaily.ForeColor = ThemeManager.TextColor;
            rbWeekly.ForeColor = ThemeManager.TextColor;
            rbMonthly.ForeColor = ThemeManager.TextColor;
            rbAllTime.ForeColor = ThemeManager.TextColor;

            // Button
            btnOpenCalculator.BackColor = ThemeManager.DarkButtonColor;
            btnOpenCalculator.ForeColor = ThemeManager.TextColor;
        }

        #region Unchanged Layout and Setup Code
        private void RoundAllPanels()
        {
            RoundedFormHelper.RoundPanel(pnlTotalPnl, 20);
            RoundedFormHelper.RoundPanel(pnlWinRate, 20);
            RoundedFormHelper.RoundPanel(pnlProfitFactor, 20);
            RoundedFormHelper.RoundPanel(pnlTotalTrades, 20);
            RoundedFormHelper.RoundPanel(pnlAvgWinning, 20);
            RoundedFormHelper.RoundPanel(pnlAvgLosing, 20);
            RoundedFormHelper.RoundPanel(pnlChart, 20);
            RoundedFormHelper.RoundPanel(pnlCalculator, 20);
            RoundedFormHelper.MakeButtonRounded(btnOpenCalculator, 30);
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
            _layoutManager.RegisterControl(lblTotalPnlValue, pnlTotalPnl, pnlTotalPnl_Max, new Point(59, 71), new Size(24, 27));
            _layoutManager.RegisterControl(lblSymbol, pnlTotalPnl, pnlTotalPnl_Max, new Point(45, 36), new Size(98, 27));
            _layoutManager.RegisterControl(lblWinRateValue, pnlWinRate, pnlWinRate_Max, new Point(60, 71), new Size(24, 27));
            _layoutManager.RegisterControl(label5, pnlWinRate, pnlWinRate_Max, new Point(43, 36), new Size(103, 27));
            _layoutManager.RegisterControl(lblProfitFactorValue, pnlProfitFactor, pnlProfitFactor_Max, new Point(59, 71), new Size(24, 27));
            _layoutManager.RegisterControl(label2, pnlProfitFactor, pnlProfitFactor_Max, new Point(32, 36), new Size(135, 400));
            _layoutManager.RegisterControl(lblTotalTradesValue, pnlTotalTrades, pnlTotalTrades_Max, new Point(65, 71), new Size(24, 27));
            _layoutManager.RegisterControl(label7, pnlTotalTrades, pnlTotalTrades_Max, new Point(35, 36), new Size(130, 27));
            _layoutManager.RegisterControl(lblAvgWinValue, pnlAvgWinning, pnlAvgWinning_Max, new Point(43, 71), new Size(24, 27));
            _layoutManager.RegisterControl(label9, pnlAvgWinning, pnlAvgWinning_Max, new Point(30, 36), new Size(198, 27));
            _layoutManager.RegisterControl(lblAvgLossValue, pnlAvgLosing, pnlAvgLosing_Max, new Point(43, 71), new Size(24, 27));
            _layoutManager.RegisterControl(label11, pnlAvgLosing, pnlAvgLosing_Max, new Point(30, 36), new Size(182, 27));
            _layoutManager.RegisterControl(formsPlotPnl, pnlChart, pnlChart_Max, new Point(41, 58), new Size(1507, 435));
            _layoutManager.RegisterControl(rbDaily, pnlChart, pnlChart_Max, new Point(563, 14), new Size(75, 26));
            _layoutManager.RegisterControl(rbWeekly, pnlChart, pnlChart_Max, new Point(683, 14), new Size(89, 26));
            _layoutManager.RegisterControl(rbMonthly, pnlChart, pnlChart_Max, new Point(815, 14), new Size(96, 26));
            _layoutManager.RegisterControl(rbAllTime, pnlChart, pnlChart_Max, new Point(939, 14), new Size(96, 26));
            _layoutManager.RegisterControl(btnOpenCalculator, pnlCalculator, pnlCalculator_Max, new Point(701, 70), new Size(310, 65));
        }

        public void SetWindowState(FormWindowStateExtended newState)
        {
            _layoutManager.SetWindowState(newState);
        }
        #endregion

        #region --- Form Logic (Updated) ---

        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            LoadStatistics();
            ApplyTheme();
        }

        private void LoadStatistics()
        {
            List<Trade> trades;
            using (var db = new AppDbContext())
            {
                var (start, end, res) = GetRequestedRange();

                IQueryable<Trade> q = db.Trades;

                if (rbDaily.Checked)
                {
                    var day = start!.Value.Date;
                    var next = day.AddDays(1);
                    q = q.Where(t => t.Date >= day && t.Date < next);
                }
                else if (rbWeekly.Checked || rbMonthly.Checked)
                {
                    var s = start!.Value.Date;
                    var e = end!.Value.Date.AddDays(1);
                    q = q.Where(t => t.Date >= s && t.Date < e);
                }
                // All-time -> no additional filter

                trades = q.OrderBy(t => t.Date).ToList();
            }

            btnOpenCalculator.IconChar = IconChar.Calculator;
            btnOpenCalculator.IconColor = System.Drawing.Color.Orange;
            btnOpenCalculator.IconSize = 25;
            btnOpenCalculator.TextImageRelation = TextImageRelation.ImageBeforeText;

            var statsManager = new StatisticsManager();
            var report = statsManager.GenerateReport(trades);

            UpdateKpiLabels(report);
            UpdatePnlChart(trades);   // pass raw trades; we’ll aggregate inside
        }

        private void UpdateKpiLabels(TradingPerformanceReport report)
        {
            System.Drawing.Color positiveColor = System.Drawing.Color.FromArgb(46, 204, 113);
            System.Drawing.Color negativeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            System.Drawing.Color neutralColor = System.Drawing.Color.White;

            lblTotalPnlValue.Text = report.TotalPnL.ToString("C");
            lblTotalPnlValue.ForeColor = report.TotalPnL >= 0 ? positiveColor : negativeColor;

            lblWinRateValue.Text = $"{report.WinRate:F2}%";
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

        private void UpdatePnlChart(List<Trade> trades)
        {
            formsPlotPnl.Plot.Clear();

            // Theme
            formsPlotPnl.Plot.FigureBackground.Color = ScottPlot.Color.FromColor(ThemeManager.ChartFigureBackground);
            formsPlotPnl.Plot.DataBackground.Color = ScottPlot.Color.FromColor(ThemeManager.ChartDataBackground);
            formsPlotPnl.Plot.Grid.MajorLineColor = ScottPlot.Color.FromColor(ThemeManager.ChartGridColor);

            var xAxis = formsPlotPnl.Plot.Axes.Bottom;
            var yAxis = formsPlotPnl.Plot.Axes.Left;

            xAxis.Label.Text = "Date";
            yAxis.Label.Text = "Cumulative PnL";
            xAxis.Label.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartAxisLabelColor);
            yAxis.Label.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartAxisLabelColor);
            xAxis.TickLabelStyle.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartTickLabelColor);
            yAxis.TickLabelStyle.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartTickLabelColor);

            // Decide resolution and range
            var (start, end, res) = GetRequestedRange();

            // Build series
            var (xs, ys, dailyPnL, dates) = BuildSeries(trades, res, start, end);

            if (xs.Length == 0)
            {
                formsPlotPnl.Refresh();
                return;
            }

            // Cumulative line
            var line = formsPlotPnl.Plot.Add.Scatter(xs, ys);
            line.Color = ScottPlot.Colors.CornflowerBlue;
            line.LineWidth = 2;

            // Dots:
            // - Intraday: one per trade (use per-point profit)
            // - Daily   : one per day (use dailyPnL)
            for (int i = 0; i < xs.Length; i++)
            {
                bool isWin = dailyPnL[i] >= 0;
                var marker = formsPlotPnl.Plot.Add.Marker(xs[i], ys[i]);
                marker.Shape = ScottPlot.MarkerShape.FilledCircle;
                marker.Size = res == ChartResolution.Intraday ? 8 : 10;
                marker.Color = isWin
                    ? ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(46, 204, 113))
                    : ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(231, 76, 60));
            }

            // Better date ticks:
            var autoTicks = new ScottPlot.TickGenerators.DateTimeAutomatic();

            // If your ScottPlot version exposes MinTickSpacing, you can enforce daily ticks like this:
            // autoTicks.MinTickSpacing = TimeSpan.FromDays(1);

            xAxis.TickGenerator = autoTicks;

            formsPlotPnl.Plot.Axes.AutoScale();
            formsPlotPnl.Refresh();
        }

        private (double[] xs, double[] ys, List<double> dailyPnL, List<DateTime> dates)
    BuildSeries(List<Trade> trades, ChartResolution res, DateTime? rangeStart = null, DateTime? rangeEnd = null)
        {
            // 1) always sort by the trade's date (not insertion order)
            trades = trades.OrderBy(t => t.Date).ToList();

            // 2) aggregate by CALENDAR DAY so a trade saved for 2025-10-23 plots at 2025-10-23
            var pnlByDay = trades
                .GroupBy(t => t.Date.Date)
                .ToDictionary(g => g.Key, g => (double)g.Sum(x => x.ProfitLoss));

            if (pnlByDay.Count == 0)
                return (Array.Empty<double>(), Array.Empty<double>(), new List<double>(), new List<DateTime>());

            // 3) build a continuous day range and fill missing days with 0
            DateTime start = (rangeStart ?? pnlByDay.Keys.Min()).Date;
            DateTime end = (rangeEnd ?? pnlByDay.Keys.Max()).Date;

            var days = new List<DateTime>();
            for (var d = start; d <= end; d = d.AddDays(1))
                days.Add(d);

            var dailyPnL = days.Select(d => pnlByDay.TryGetValue(d, out var v) ? v : 0.0).ToList();

            // 4) cumulative PnL across time (this is what makes later points shift
            //    when a back-dated trade is added/edited)
            var xs = days.Select(d => d.ToOADate()).ToArray();
            var ys = new double[dailyPnL.Count];
            double cum = 0;
            for (int i = 0; i < dailyPnL.Count; i++)
            {
                cum += dailyPnL[i];
                ys[i] = cum;
            }

            return (xs, ys, dailyPnL, days);
        }


        // Calendar-aligned helpers (place here in the same file)
        private static DateTime StartOfWeek(DateTime dt, DayOfWeek firstDayOfWeek = DayOfWeek.Monday)
        {
            int diff = (7 + (dt.DayOfWeek - firstDayOfWeek)) % 7;
            return dt.Date.AddDays(-diff);
        }

        private (DateTime? start, DateTime? end, ChartResolution res) GetRequestedRange()
        {
            var today = DateTime.Today;

            if (rbDaily.Checked)
                return (today, today, ChartResolution.Intraday);

            if (rbWeekly.Checked)
            {
                DateTime weekStart = StartOfWeek(today, DayOfWeek.Monday);
                DateTime weekEnd = weekStart.AddDays(6); // Monday .. Sunday
                return (weekStart, weekEnd, ChartResolution.Daily);
            }

            if (rbMonthly.Checked)
            {
                DateTime monthStart = new DateTime(today.Year, today.Month, 1);
                DateTime monthEnd = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
                return (monthStart, monthEnd, ChartResolution.Daily);
            }

            // All time: let BuildSeries derive bounds from data
            return (null, null, ChartResolution.Daily);
        }


        private void rbDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDaily.Checked) LoadStatistics();
        }

        private void rbWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWeekly.Checked) LoadStatistics();
        }

        private void rbMonthly_CheckedChanged(object sender, EventArgs e)
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