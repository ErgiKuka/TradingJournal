// TradingJournal.Pl.PlaceHolder.Statistics/FrmStatistics.cs

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
    public partial class FrmStatistics : Form, IResponsiveChildForm
    {
        private readonly ResponsiveLayoutManager _layoutManager;
        private FrmCalculator? _calculatorFormInstance;

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
                DateTime today = DateTime.Now.Date;
                DateTime tomorrow = today.AddDays(1);

                IQueryable<Trade> query = db.Trades.AsQueryable();

                if (rbDaily.Checked)
                {
                    query = query.Where(t => t.Date >= today && t.Date < tomorrow);
                }
                else if (rbWeekly.Checked)
                {
                    DateTime oneWeekAgo = today.AddDays(-7);
                    query = query.Where(t => t.Date >= oneWeekAgo && t.Date < tomorrow);
                }
                else if (rbMonthly.Checked)
                {
                    DateTime oneMonthAgo = today.AddDays(-30);
                    query = query.Where(t => t.Date >= oneMonthAgo && t.Date < tomorrow);
                }
                else if (rbAllTime.Checked)
                {
                    // no filter = all trades
                }

                trades = query.ToList();
            }

            btnOpenCalculator.IconChar = IconChar.Calculator;
            btnOpenCalculator.IconColor = System.Drawing.Color.Orange;
            btnOpenCalculator.IconSize = 25;
            btnOpenCalculator.TextImageRelation = TextImageRelation.ImageBeforeText;

            var statsManager = new StatisticsManager();
            var report = statsManager.GenerateReport(trades);

            UpdateKpiLabels(report);
            UpdatePnlChart(report);
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

        private void UpdatePnlChart(TradingPerformanceReport report)
        {
            formsPlotPnl.Plot.Clear();

            // styling
            formsPlotPnl.Plot.FigureBackground.Color = ScottPlot.Color.FromColor(ThemeManager.ChartFigureBackground);
            formsPlotPnl.Plot.DataBackground.Color = ScottPlot.Color.FromColor(ThemeManager.ChartDataBackground);

            // Axes
            var xAxis = formsPlotPnl.Plot.Axes.Bottom;
            var yAxis = formsPlotPnl.Plot.Axes.Left;

            formsPlotPnl.Plot.Grid.MajorLineColor = ScottPlot.Color.FromColor(ThemeManager.ChartGridColor);

            xAxis.Label.Text = "Date";
            yAxis.Label.Text = "Cumulative PnL";

            xAxis.Label.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartAxisLabelColor);
            yAxis.Label.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartAxisLabelColor);

            xAxis.TickLabelStyle.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartTickLabelColor);
            yAxis.TickLabelStyle.ForeColor = ScottPlot.Color.FromColor(ThemeManager.ChartTickLabelColor);

            // Keep your PnL line and markers logic as-is...
            if (report.PnlOverTime.Any())
            {
                double[] xs = report.PnlOverTime.Select(p => p.Date.ToOADate()).ToArray();
                double[] ys = report.PnlOverTime.Select(p => (double)p.Value).ToArray();

                var linePlot = formsPlotPnl.Plot.Add.Scatter(xs, ys);
                linePlot.Color = ScottPlot.Colors.CornflowerBlue;
                linePlot.LineWidth = 2;

                for (int i = 0; i < report.IndividualPnLs.Count; i++)
                {
                    double x = xs[i];
                    double y = ys[i];
                    bool isWin = report.IndividualPnLs[i] >= 0;

                    var marker = formsPlotPnl.Plot.Add.Marker(x, y);
                    marker.Shape = ScottPlot.MarkerShape.FilledCircle;
                    marker.Size = 10;
                    marker.Color = isWin
                        ? ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(46, 204, 113))
                        : ScottPlot.Color.FromColor(System.Drawing.Color.FromArgb(231, 76, 60));
                }

                xAxis.TickGenerator = new ScottPlot.TickGenerators.DateTimeAutomatic();
            }

            formsPlotPnl.Plot.Axes.AutoScale();
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
