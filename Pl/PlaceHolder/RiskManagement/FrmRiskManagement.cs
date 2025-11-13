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
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Pl.PlaceHolder.RiskManagement
{
    public partial class FrmRiskManagement : UserControl, IResponsiveChildForm
    {
        public int DefaultRisk;
        private readonly ResponsiveLayoutManager _layoutManager;
        public FrmRiskManagement()
        {
            InitializeComponent();
            LoadDashboardData();
            ApplyTheme();
            _layoutManager = new ResponsiveLayoutManager(this);

            InitializeResponsiveLayouts();
            RoundedFormHelper.RoundPanel(pnlRiskInformation, 20);
            RoundedFormHelper.RoundPanel(pnlRiskInformationMax, 20);
        }
        private void FrmRiskManagement_Load(object sender, EventArgs e)
        {
            CalculateRisk();
            ApplyTheme();
            ApplyFontStyles(isMaximized: false);
        }

        public void SetWindowState(FormWindowStateExtended newState)
        {
            _layoutManager.SetWindowState(newState);
        }

        private void LoadDashboardData()
        {
            using (var db = new AppDbContext())
            {
                var allTrades = db.Trades.ToList();
                var settings = SettingsManager.Load();
                var dashboardManager = new DashboardManager(settings);
                var report = dashboardManager.GenerateReport(allTrades);
                UpdateUi(report);
            }
        }

        private void ApplyTheme()
        {
            // Form background
            this.BackColor = ThemeManager.BackgroundColor;
            lblCurrentBalance.ForeColor = ThemeManager.TextColor;

            // Panels
            pnlRiskInformation.BackColor = ThemeManager.PanelColor;
            pnlRiskInformationMax.BackColor = ThemeManager.PanelColor;

            // Label
            label1.ForeColor = ThemeManager.TextColor;
            label3.ForeColor = ThemeManager.TextColor;
            label4.ForeColor = ThemeManager.TextColor;
            label5.ForeColor = ThemeManager.TextColor;
            label6.ForeColor = ThemeManager.TextColor;
            label8.ForeColor = ThemeManager.TextColor;
            label9.ForeColor = ThemeManager.TextColor;
            label11.ForeColor = ThemeManager.TextColor;
            label13.ForeColor = ThemeManager.TextColor;
            label15.ForeColor = ThemeManager.TextColor;
            lblEntryPercentage.ForeColor = ThemeManager.TextColor;
            lblRisk5.ForeColor = ThemeManager.TextColor;
            lblRisk10.ForeColor = ThemeManager.TextColor;
            lblRisk15.ForeColor = ThemeManager.TextColor;
            lblRisk20.ForeColor = ThemeManager.TextColor;
            lblRisk25.ForeColor = ThemeManager.TextColor;
            lblBalRisk1.ForeColor = ThemeManager.TextColor;
            lblBalRisk2.ForeColor = ThemeManager.TextColor;
            lblBalRisk3.ForeColor = ThemeManager.TextColor;
            lblBalRisk4.ForeColor = ThemeManager.TextColor;
            lblBalRisk5.ForeColor = ThemeManager.TextColor;
            lblCurrentBalance.ForeColor = ThemeManager.TextColor;
            label17.ForeColor = ThemeManager.TextColor;
            label19.ForeColor = ThemeManager.TextColor;
            label21.ForeColor = ThemeManager.TextColor;

            // Textbox
            txtRiskPercentage.BackColor = ThemeManager.TextBoxColor;
        }

        private void UpdateUi(DashboardReport report)
        {
            lblCurrentBalance.Text = report.TotalPortfolioValue.ToString("C");
        }

        private void InitializeResponsiveLayouts()
        {
            // --- Register the controls OUTSIDE the table ---
            // These are the labels and textbox at the top of the panel.

            // Current Balance (Title and Value)
            _layoutManager.RegisterControl(label8, pnlRiskInformation, pnlRiskInformationMax, new Point(50, 40), new Size(150, 30));
            _layoutManager.RegisterControl(lblCurrentBalance, pnlRiskInformation, pnlRiskInformationMax, new Point(50, 80), new Size(200, 30));

            // Entry Percentage (Title and Value)
            _layoutManager.RegisterControl(label6, pnlRiskInformation, pnlRiskInformationMax, new Point(350, 40), new Size(180, 30));
            _layoutManager.RegisterControl(lblEntryPercentage, pnlRiskInformation, pnlRiskInformationMax, new Point(350, 80), new Size(200, 30));

            // Risk Input (Title and TextBox)
            _layoutManager.RegisterControl(label5, pnlRiskInformation, pnlRiskInformationMax, new Point(650, 40), new Size(250, 30));
            _layoutManager.RegisterControl(txtRiskPercentage, pnlRiskInformation, pnlRiskInformationMax, new Point(650, 80), new Size(180, 35));


            // --- Register the TableLayoutPanel itself ---
            // This is the key step. We move the entire table as one unit.
            // We will center it in the maximized panel and make it much larger.
            // NEW, SMALLER SIZE
            _layoutManager.RegisterControl(tableLayoutPanel1, pnlRiskInformation, pnlRiskInformationMax, new Point(250, 150), new Size(1150, 200));



            // --- Register Actions to change font sizes ---
            // This makes the text larger in maximized mode, improving readability.
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Normal, () => ApplyFontStyles(isMaximized: false));
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Maximized, () => ApplyFontStyles(isMaximized: true));
        }

        // NEW METHOD WITH SMALLER MAXIMIZED FONTS
        private void ApplyFontStyles(bool isMaximized)
        {
            // Define the font sizes for normal and maximized states
            Font titleFont = isMaximized ? new Font("Times New Roman", 14F, FontStyle.Regular) : new Font("Times New Roman", 12F, FontStyle.Regular);
            Font valueFont = isMaximized ? new Font("Times New Roman", 12F, FontStyle.Bold) : new Font("Times New Roman", 12F, FontStyle.Regular);

            // Apply fonts to the top controls
            label8.Font = titleFont;
            lblCurrentBalance.Font = valueFont;
            label6.Font = titleFont;
            lblEntryPercentage.Font = valueFont;
            label5.Font = titleFont;
            txtRiskPercentage.Font = isMaximized ? new Font("Times New Roman", 14F) : new Font("Times New Roman", 13.8F);

            // Apply fonts to all labels within the TableLayoutPanel
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is Label lbl)
                {
                    if (lbl.Name.StartsWith("lbl")) // Value labels like "lblRisk5"
                    {
                        lbl.Font = valueFont;
                    }
                    else // Title labels like "label4"
                    {
                        lbl.Font = titleFont;
                    }
                }
            }
        }


        private void CalculateRisk()
        {
            decimal DefaultRisk;

            if (string.IsNullOrWhiteSpace(txtRiskPercentage.Text))
            {
                DefaultRisk = 10;
            }
            else
            {
                DefaultRisk = decimal.Parse(txtRiskPercentage.Text);
            }

            decimal accountBalance = decimal.Parse(lblCurrentBalance.Text, System.Globalization.NumberStyles.Currency);

            decimal TradeEntryPercentage = (accountBalance * (DefaultRisk/100));

            lblEntryPercentage.Text = TradeEntryPercentage.ToString("C");

            lblRisk5.Text = (TradeEntryPercentage * 0.05m).ToString("C0");
            lblRisk10.Text = (TradeEntryPercentage * 0.10m).ToString("C0");
            lblRisk15.Text = (TradeEntryPercentage * 0.15m).ToString("C0");
            lblRisk20.Text = (TradeEntryPercentage * 0.20m).ToString("C0");
            lblRisk25.Text = (TradeEntryPercentage * 0.25m).ToString("C0");

            lblBalRisk1.Text = (accountBalance * 0.01m).ToString("C");
            lblBalRisk2.Text = (accountBalance * 0.02m).ToString("C");
            lblBalRisk3.Text = (accountBalance * 0.03m).ToString("C");
            lblBalRisk4.Text = (accountBalance * 0.04m).ToString("C");
            lblBalRisk5.Text = (accountBalance * 0.05m).ToString("C");
        }

        private void txtRiskPercentage_TextChanged(object sender, EventArgs e)
        {
            CalculateRisk();
        }

    }
}
