// FrmDashboard.cs

using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;
using TradingJournal.Core.Managers;

namespace TradingJournal.Pl.PlaceHolder.Dashboard
{
    public partial class FrmDashboard : Form, IResponsiveChildForm
    {
        private readonly ResponsiveLayoutManager _layoutManager;
        private readonly BinanceApiService _binanceService;
        private List<CryptoData> _allCryptos;
        private List<CryptoData> _filteredCryptos;
        private readonly System.Windows.Forms.Timer _refreshTimer;
        private readonly Dictionary<string, Image> _cryptoIcons = new Dictionary<string, Image>();

        private Panel? _pnlCryptoListContainer;
        private Panel? _pnlCryptoItems;

        public FrmDashboard()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            _binanceService = new BinanceApiService();
            _allCryptos = new List<CryptoData>();
            _filteredCryptos = new List<CryptoData>();

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            _refreshTimer = new System.Windows.Forms.Timer { Interval = 60000 };

            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            pnlPortfolio.BackColor = ThemeManager.PanelColor;
            pnlLivePrices.BackColor = ThemeManager.PanelColor;
            pnlPortfolio_Max.BackColor = ThemeManager.PanelColor;
            pnlLivePrices_Max.BackColor = ThemeManager.PanelColor;

            lblPortfolioValue.ForeColor = ThemeManager.TextColor;
            lblTodaysPnl.ForeColor = ThemeManager.TextColor;
            lblTradingBalanceValue.ForeColor = ThemeManager.TextColor;
            lblCurrentDate.ForeColor = ThemeManager.TextColor;
            btnRefresh.BackColor = ThemeManager.ButtonColor;

            // Optionally loop over controls inside live prices panel
            foreach (Control ctrl in pnlLivePrices.Controls)
            {
                if (ctrl is Label lbl) lbl.ForeColor = ThemeManager.TextColor;
            }
        }

        private void InitializeResponsiveLayouts()
        {
            // --- Register Portfolio Panel and its Children ---
            _layoutManager.RegisterControl(label1, pnlPortfolio, pnlPortfolio_Max, new Point(37, 37), new Size(169, 45));
            _layoutManager.RegisterControl(lblPortfolioValue, pnlPortfolio, pnlPortfolio_Max, new Point(37, 97), new Size(70, 53));

            _layoutManager.RegisterControl(lblTodaysPnl, pnlPortfolio, pnlPortfolio_Max, new Point(1371, 109), new Size(34, 39));

            _layoutManager.RegisterControl(label3, pnlPortfolio, pnlPortfolio_Max, new Point(280, 37), new Size(200, 32));
            _layoutManager.RegisterControl(lblTradingBalanceValue, pnlPortfolio, pnlPortfolio_Max, new Point(280, 97), new Size(70, 53));

            // --- Register Standalone Controls directly on the Form ---
            _layoutManager.RegisterControl(lblCurrentDate, this, this, new Point(1473, 23), new Size(75, 35));
            _layoutManager.RegisterControl(btnRefresh, this, this, new Point(1618, 20), new Size(41, 34));

            _layoutManager.RegisterControl(pnlPortfolio, this, pnlPortfolio_Max, pnlPortfolio_Max.Location, pnlPortfolio_Max.Size);
            _layoutManager.RegisterControl(pnlLivePrices, this, pnlLivePrices_Max, pnlLivePrices_Max.Location, pnlLivePrices_Max.Size);


            // --- Register Actions to Build Dynamic UI ---
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Normal, () => BuildLivePricesUI(pnlLivePrices));
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Maximized, () => BuildLivePricesUI(pnlLivePrices_Max));
        }

        public void SetWindowState(FormWindowStateExtended newState)
        {
            _layoutManager.SetWindowState(newState);
        }

        #region --- Unchanged Code ---
        private void FrmDashboard_Load(object? sender, EventArgs e)
        {
            lblCurrentDate.Text = DateTime.Now.ToString("dd MMM, yyyy");

            RoundedFormHelper.RoundPanel(pnlPortfolio, 20);
            RoundedFormHelper.RoundPanel(pnlLivePrices, 20);
            RoundedFormHelper.RoundPanel(pnlPortfolio_Max, 20);
            RoundedFormHelper.RoundPanel(pnlLivePrices_Max, 20);
            RoundedFormHelper.MakeButtonRounded(btnRefresh, 20);

            btnRefresh.IconChar = IconChar.RotateRight;
            btnRefresh.IconColor = Color.Green;
            btnRefresh.IconSize = 25;
            btnRefresh.Click += BtnRefresh_Click;

            LoadDashboardData();
            BuildLivePricesUI(pnlLivePrices);
            LoadCryptoData();

            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();

            ApplyTheme();
        }

        private void BuildLivePricesUI(Panel targetPanel)
        {
            targetPanel.Controls.Clear();
            targetPanel.AutoScroll = true;

            var lblLivePrices = new Label { Text = "Live Prices", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = ThemeManager.TextColor };
            targetPanel.Controls.Add(lblLivePrices);

            var txtSearch = new TextBox { Name = "txtCryptoSearch", Location = new Point(20, lblLivePrices.Bottom + 15), Size = new Size(targetPanel.Width - 40, 35), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, Font = new Font("Segoe UI", 11), BackColor = ThemeManager.CryptoTextBoxColor, ForeColor = ThemeManager.CryptoCurrentColor, BorderStyle = BorderStyle.FixedSingle, Text = "Search cryptocurrencies..." };
            txtSearch.Enter += (s, e) => { if (txtSearch.Text == "Search cryptocurrencies...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.LightGray; } };
            txtSearch.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Search cryptocurrencies..."; txtSearch.ForeColor = Color.LightGray; } };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            targetPanel.Controls.Add(txtSearch);

            _pnlCryptoListContainer = new Panel { Name = "pnlCryptoListContainer", Location = new Point(20, txtSearch.Bottom + 15), Size = new Size(targetPanel.Width - 20, targetPanel.Height - txtSearch.Bottom - 35), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, AutoScroll = true };
            targetPanel.Controls.Add(_pnlCryptoListContainer);

            _pnlCryptoItems = new Panel { Name = "pnlCryptoItems", Location = new Point(0, 0), Size = new Size(_pnlCryptoListContainer.Width - SystemInformation.VerticalScrollBarWidth, 10), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            _pnlCryptoListContainer.Controls.Add(_pnlCryptoItems);
        }

        private async void LoadCryptoData()
        {
            if (_pnlCryptoItems == null) return;
            try
            {
                _pnlCryptoItems.Controls.Clear();
                var loadingLabel = new Label { Text = "Loading cryptocurrency data...", Location = new Point(20, 20), AutoSize = true, ForeColor = ThemeManager.CryptoCurrentColor, Font = new Font("Segoe UI", 12) };
                _pnlCryptoItems.Controls.Add(loadingLabel);

                _allCryptos = await _binanceService.GetTopCryptosAsync();
                _filteredCryptos = new List<CryptoData>(_allCryptos);

                _pnlCryptoItems.Controls.Remove(loadingLabel);
                DisplayCryptos();

                _ = Task.Run(LoadCryptoIcons);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading crypto data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCryptoIcons()
        {
            using (var httpClient = new HttpClient())
            {
                foreach (var crypto in _allCryptos.Take(20))
                {
                    try
                    {
                        var imageBytes = await httpClient.GetByteArrayAsync(crypto.IconUrl);
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            var image = Image.FromStream(ms);
                            _cryptoIcons[crypto.Symbol] = new Bitmap(image, new Size(32, 32));
                        }
                    }
                    catch { /* Continue without icon */ }
                }
            }
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                this.Invoke(new Action(DisplayCryptos));
            }
        }

        private void DisplayCryptos()
        {
            if (_pnlCryptoItems == null || _pnlCryptoListContainer == null) return;

            int currentScrollPos = _pnlCryptoListContainer.VerticalScroll.Value;
            _pnlCryptoItems.SuspendLayout();
            _pnlCryptoItems.Controls.Clear();

            int yPosition = 10;
            foreach (var crypto in _filteredCryptos.Take(10))
            {
                var cryptoPanel = CreateCryptoPanel(crypto, yPosition);
                _pnlCryptoItems.Controls.Add(cryptoPanel);
                yPosition += 70;
            }
            _pnlCryptoItems.Height = yPosition + 10;
            _pnlCryptoItems.ResumeLayout();

            if (currentScrollPos > 0 && currentScrollPos < _pnlCryptoListContainer.VerticalScroll.Maximum)
            {
                _pnlCryptoListContainer.VerticalScroll.Value = currentScrollPos;
            }
            _pnlCryptoListContainer.Invalidate();
        }

        private Panel CreateCryptoPanel(CryptoData crypto, int yPosition)
        {
            var panel = new Panel { Name = $"cryptoPanel_{crypto.Symbol}", Location = new Point(10, yPosition), Size = new Size(_pnlCryptoItems.Width - 20, 60), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right, BackColor = ThemeManager.BackgroundColor, Margin = new Padding(5) };
            RoundedFormHelper.RoundPanel(panel, 12);

            Control iconControl;
            if (_cryptoIcons.ContainsKey(crypto.Symbol))
            {
                iconControl = new PictureBox { Image = _cryptoIcons[crypto.Symbol], Location = new Point(15, 15), Size = new Size(32, 32), SizeMode = PictureBoxSizeMode.StretchImage, BackColor = Color.Transparent };
            }
            else
            {
                iconControl = new Label { Text = crypto.Symbol.Replace("USDT", "").Substring(0, Math.Min(3, crypto.Symbol.Replace("USDT", "").Length)), Location = new Point(15, 15), Size = new Size(32, 32), Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.White, BackColor = GetCryptoColor(crypto.Symbol), TextAlign = ContentAlignment.MiddleCenter };
            }

            var nameLabel = new Label { Text = crypto.Name, Location = new Point(60, 8), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = ThemeManager.CryptoCurrentColor, BackColor = Color.Transparent };
            var symbolLabel = new Label { Text = crypto.Symbol.Replace("USDT", "/USDT"), Location = new Point(60, 28), AutoSize = true, Font = new Font("Segoe UI", 8), ForeColor = ThemeManager.CryptoCurrentColor, BackColor = Color.Transparent };
            var priceLabel = new Label { Text = crypto.FormattedPrice, Location = new Point(220, 12), Size = new Size(120, 22), Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = ThemeManager.CryptoCurrentColor, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleRight };
            var changeLabel = new Label { Text = $"{(crypto.PriceChangePercent >= 0 ? "+" : "")}{crypto.PriceChangePercent:F2}%", Location = new Point(350, 8), Size = new Size(80, 18), Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = crypto.PriceChangePercent >= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60), BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleRight };
            var highLowLabel = new Label { Text = $"H: {crypto.High24h:F4} L: {crypto.Low24h:F4}", Location = new Point(350, 28), Size = new Size(120, 15), Font = new Font("Segoe UI", 7), ForeColor = ThemeManager.CryptoCurrentColor, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleRight };
            var marketCapLabel = new Label { Text = $"Vol: {crypto.FormattedVolume}", Location = new Point(480, 12), Size = new Size(100, 22), Font = new Font("Segoe UI", 9), ForeColor = ThemeManager.CryptoCurrentColor, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleRight };

            panel.Controls.AddRange(new Control[] { iconControl, nameLabel, symbolLabel, priceLabel, changeLabel, highLowLabel, marketCapLabel });
            return panel;
        }

        private Color GetCryptoColor(string symbol)
        {
            var colors = new Dictionary<string, Color> { { "BTCUSDT", Color.FromArgb(247, 147, 26) }, { "ETHUSDT", Color.FromArgb(98, 126, 234) }, { "BNBUSDT", Color.FromArgb(243, 186, 47) }, { "SOLUSDT", Color.FromArgb(220, 31, 255) }, { "ADAUSDT", Color.FromArgb(0, 51, 173) }, { "XRPUSDT", Color.FromArgb(35, 37, 49) }, { "DOGEUSDT", Color.FromArgb(198, 147, 35) }, { "SHIBUSDT", Color.FromArgb(255, 140, 0) }, { "AVAXUSDT", Color.FromArgb(232, 65, 66) }, { "DOTUSDT", Color.FromArgb(230, 1, 122) }, { "MATICUSDT", Color.FromArgb(130, 71, 229) }, { "LINKUSDT", Color.FromArgb(43, 109, 240) }, { "UNIUSDT", Color.FromArgb(255, 0, 122) }, { "LTCUSDT", Color.FromArgb(52, 118, 183) }, { "SUIUSDT", Color.FromArgb(79, 172, 254) } };
            return colors.ContainsKey(symbol) ? colors[symbol] : Color.FromArgb(100, 149, 237);
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox txtSearch) return;
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.Text == "Search cryptocurrencies...") { _filteredCryptos = new List<CryptoData>(_allCryptos); }
            else { var searchTerm = txtSearch.Text.ToLower(); _filteredCryptos = _allCryptos.Where(c => c.Name.ToLower().Contains(searchTerm) || c.Symbol.ToLower().Contains(searchTerm)).ToList(); }
            DisplayCryptos();
        }

        private async void RefreshTimer_Tick(object? sender, EventArgs e) => await RefreshCryptoData();
        private async void BtnRefresh_Click(object? sender, EventArgs e) => await RefreshCryptoData();

        private async Task RefreshCryptoData()
        {
            try
            {
                _allCryptos = await _binanceService.GetTopCryptosAsync();
                var searchBox = this.Controls.Find("txtCryptoSearch", true).FirstOrDefault() as TextBox;
                if (searchBox != null) { TxtSearch_TextChanged(searchBox, EventArgs.Empty); }
                else { _filteredCryptos = new List<CryptoData>(_allCryptos); DisplayCryptos(); }
            }
            catch { /* Silent fail */ }
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

        private void UpdateUi(DashboardReport report)
        {
            lblPortfolioValue.Text = report.TotalPortfolioValue.ToString("C");
            lblTradingBalanceValue.Text = report.TotalTradingAccountBalance.ToString("C");

            if (report.TodaysPnL > 0)
            {
                lblTodaysPnl.Text = $"+{report.TodaysPnL:C}";
                lblTodaysPnl.ForeColor = Color.FromArgb(46, 204, 113);
            }
            else if (report.TodaysPnL < 0)
            {
                lblTodaysPnl.Text = report.TodaysPnL.ToString("C");
                lblTodaysPnl.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                lblTodaysPnl.Text = "$0.00";
                lblTodaysPnl.ForeColor = Color.Gray;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            foreach (var icon in _cryptoIcons.Values) { icon?.Dispose(); }
            _cryptoIcons.Clear();
            base.OnFormClosing(e);
        }

        private void panel1_Paint(object? sender, PaintEventArgs e)
        {
            // This method can be left empty. It just needs to exist to prevent a designer error.
        }
        #endregion

        private void pnlPortfolio_Max_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
