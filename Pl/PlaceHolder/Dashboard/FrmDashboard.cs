using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Pl.PlaceHolder.Dashboard
{
    public partial class FrmDashboard : Form
    {
        private BinanceApiService binanceService;
        private List<CryptoData> allCryptos;
        private List<CryptoData> filteredCryptos;
        private System.Windows.Forms.Timer refreshTimer;
        private Dictionary<string, Image> cryptoIcons = new Dictionary<string, Image>();

        private Panel pnlCryptoListContainer;
        private Panel pnlCryptoItems;

        public FrmDashboard()
        {
            InitializeComponent();
            binanceService = new BinanceApiService();
            allCryptos = new List<CryptoData>();
            filteredCryptos = new List<CryptoData>();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            lblCurrentDate.Text = DateTime.Now.ToString("dd MMM, yyyy");

            RoundedFormHelper.RoundPanel(panel1, 20);
            RoundedFormHelper.RoundPanel(panel2, 20);
            RoundedFormHelper.MakeButtonRounded(btnRefresh, 20);

            btnRefresh.IconChar = IconChar.RotateRight;
            btnRefresh.IconColor = Color.Green;
            btnRefresh.IconSize = 25;
            //btnRefresh.TextImageRelation = TextImageRelation.ImageBeforeText;

            LoadDashboardData();
            SetupCryptoSection();
            LoadCryptoData();

            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 60000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();

            // Link the existing refresh button
            if (btnRefresh != null)
            {
                btnRefresh.Click += BtnRefresh_Click;
            }
        }

        private void SetupCryptoSection()
        {
            if (panel2 == null) return;

            panel2.Controls.Clear();
            panel2.BackColor = Color.FromArgb(27, 38, 59);
            panel2.AutoScroll = true;

            var lblLivePrices = new Label
            {
                Text = "Live Prices",
                Location = new Point(20, 20),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            panel2.Controls.Add(lblLivePrices);

            // Assuming btnRefresh is already on the form/panel2 and named btnRefresh
            // No need to create it dynamically here

            var txtSearch = new TextBox
            {
                Name = "txtCryptoSearch",
                Location = new Point(20, lblLivePrices.Bottom + 15),
                Size = new Size(panel2.Width - 40, 35),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(30, 58, 95),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Search cryptocurrencies..."
            };

            txtSearch.Enter += (s, e) => {
                if (txtSearch.Text == "Search cryptocurrencies...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.White;
                }
            };

            txtSearch.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search cryptocurrencies...";
                    txtSearch.ForeColor = Color.LightGray;
                }
            };

            txtSearch.TextChanged += TxtSearch_TextChanged;
            panel2.Controls.Add(txtSearch);

            pnlCryptoListContainer = new Panel
            {
                Name = "pnlCryptoListContainer",
                Location = new Point(20, txtSearch.Bottom + 15),
                Size = new Size(panel2.Width - 20, panel2.Height - txtSearch.Bottom - 35),
                BackColor = Color.Transparent,
                AutoScroll = true
            };
            panel2.Controls.Add(pnlCryptoListContainer);

            pnlCryptoItems = new Panel
            {
                Name = "pnlCryptoItems",
                Location = new Point(0, 0),
                Size = new Size(pnlCryptoListContainer.Width - SystemInformation.VerticalScrollBarWidth, 10),
                BackColor = Color.Transparent
            };
            pnlCryptoListContainer.Controls.Add(pnlCryptoItems);
        }

        private async void LoadCryptoData()
        {
            if (pnlCryptoItems == null) return;

            try
            {
                pnlCryptoItems.Controls.Clear();

                var loadingLabel = new Label
                {
                    Text = "Loading cryptocurrency data...",
                    Location = new Point(20, 20),
                    Size = new Size(300, 30),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12),
                    BackColor = Color.Transparent
                };
                pnlCryptoItems.Controls.Add(loadingLabel);

                allCryptos = await binanceService.GetTopCryptosAsync();
                filteredCryptos = new List<CryptoData>(allCryptos);

                pnlCryptoItems.Controls.Remove(loadingLabel);
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
            using (var webClient = new WebClient())
            {
                foreach (var crypto in allCryptos.Take(20))
                {
                    try
                    {
                        var imageBytes = await webClient.DownloadDataTaskAsync(crypto.IconUrl);
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            var image = Image.FromStream(ms);
                            cryptoIcons[crypto.Symbol] = new Bitmap(image, new Size(32, 32));
                        }
                    }
                    catch
                    {
                        // Continue without icon if loading fails
                    }
                }

                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(DisplayCryptos));
                }
            }
        }

        private void DisplayCryptos()
        {
            if (pnlCryptoItems == null) return;

            int currentScrollPos = pnlCryptoListContainer.VerticalScroll.Value;

            pnlCryptoItems.SuspendLayout();
            pnlCryptoItems.Controls.Clear();

            int yPosition = 10;
            foreach (var crypto in filteredCryptos.Take(10))
            {
                var cryptoPanel = CreateCryptoPanel(crypto, yPosition);
                pnlCryptoItems.Controls.Add(cryptoPanel);
                yPosition += 70;
            }

            pnlCryptoItems.Height = yPosition + 10;

            pnlCryptoItems.ResumeLayout();

            if (currentScrollPos > 0 && currentScrollPos < pnlCryptoListContainer.VerticalScroll.Maximum)
            {
                pnlCryptoListContainer.VerticalScroll.Value = currentScrollPos;
            }
            pnlCryptoListContainer.Invalidate();
        }

        private Panel CreateCryptoPanel(CryptoData crypto, int yPosition)
        {
            var panel = new Panel
            {
                Name = $"cryptoPanel_{crypto.Symbol}",
                Location = new Point(10, yPosition),
                Size = new Size(pnlCryptoItems.Width - 20, 60),
                BackColor = Color.FromArgb(44, 62, 80),
                Margin = new Padding(5)
            };
            RoundedFormHelper.RoundPanel(panel, 12);

            Control iconControl;
            if (cryptoIcons.ContainsKey(crypto.Symbol))
            {
                iconControl = new PictureBox
                {
                    Image = cryptoIcons[crypto.Symbol],
                    Location = new Point(15, 15),
                    Size = new Size(32, 32),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
            }
            else
            {
                iconControl = new Label
                {
                    Text = crypto.Symbol.Replace("USDT", "").Substring(0, Math.Min(3, crypto.Symbol.Replace("USDT", "").Length)),
                    Location = new Point(15, 15),
                    Size = new Size(32, 32),
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = GetCryptoColor(crypto.Symbol),
                    TextAlign = ContentAlignment.MiddleCenter
                };
            }

            var nameLabel = new Label
            {
                Text = crypto.Name,
                Location = new Point(60, 8),
                Size = new Size(150, 18),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            var symbolLabel = new Label
            {
                Text = crypto.Symbol.Replace("USDT", "/USDT"),
                Location = new Point(60, 28),
                Size = new Size(150, 15),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.LightGray,
                BackColor = Color.Transparent
            };

            var priceLabel = new Label
            {
                Text = crypto.FormattedPrice,
                Location = new Point(220, 12),
                Size = new Size(120, 22),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            var changeLabel = new Label
            {
                Text = $"{(crypto.PriceChangePercent >= 0 ? "+" : "")}{crypto.PriceChangePercent:F2}%",
                Location = new Point(350, 8),
                Size = new Size(80, 18),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = crypto.PriceChangePercent >= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            var highLowLabel = new Label
            {
                Text = $"H: {crypto.High24h:F4} L: {crypto.Low24h:F4}",
                Location = new Point(350, 28),
                Size = new Size(120, 15),
                Font = new Font("Segoe UI", 7),
                ForeColor = Color.LightGray,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            var marketCapLabel = new Label
            {
                Text = $"Vol: {crypto.FormattedVolume}",
                Location = new Point(480, 12),
                Size = new Size(100, 22),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.LightGray,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight
            };

            panel.Controls.AddRange(new Control[] {
                iconControl, nameLabel, symbolLabel, priceLabel,
                changeLabel, highLowLabel, marketCapLabel
            });

            return panel;
        }

        private Color GetCryptoColor(string symbol)
        {
            var colors = new Dictionary<string, Color>
            {
                {"BTCUSDT", Color.FromArgb(247, 147, 26)},   // Bitcoin Orange
                {"ETHUSDT", Color.FromArgb(98, 126, 234)},   // Ethereum Blue
                {"BNBUSDT", Color.FromArgb(243, 186, 47)},   // BNB Yellow
                {"SOLUSDT", Color.FromArgb(220, 31, 255)},   // Solana Purple
                {"ADAUSDT", Color.FromArgb(0, 51, 173)},     // Cardano Blue
                {"XRPUSDT", Color.FromArgb(35, 37, 49)},     // XRP Dark
                {"DOGEUSDT", Color.FromArgb(198, 147, 35)},  // Dogecoin Gold
                {"SHIBUSDT", Color.FromArgb(255, 140, 0)},   // Shiba Orange
                {"AVAXUSDT", Color.FromArgb(232, 65, 66)},   // Avalanche Red
                {"DOTUSDT", Color.FromArgb(230, 1, 122)},    // Polkadot Pink
                {"MATICUSDT", Color.FromArgb(130, 71, 229)}, // Polygon Purple
                {"LINKUSDT", Color.FromArgb(43, 109, 240)},  // Chainlink Blue
                {"UNIUSDT", Color.FromArgb(255, 0, 122)},    // Uniswap Pink
                {"LTCUSDT", Color.FromArgb(52, 118, 183)},   // Litecoin Blue
                {"SUIUSDT", Color.FromArgb(79, 172, 254)}    // Sui Blue
            };

            return colors.ContainsKey(symbol) ? colors[symbol] : Color.FromArgb(100, 149, 237);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            var txtSearch = panel2.Controls.Find("txtCryptoSearch", true).FirstOrDefault() as TextBox;
            if (txtSearch.Text == "Search cryptocurrencies..." || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                filteredCryptos = new List<CryptoData>(allCryptos);
            }
            else
            {
                var searchTerm = txtSearch.Text.ToLower();
                filteredCryptos = allCryptos.Where(c =>
                    c.Name.ToLower().Contains(searchTerm) ||
                    c.Symbol.ToLower().Contains(searchTerm)).ToList();
            }
            DisplayCryptos();
        }

        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {
            await RefreshCryptoData();
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshCryptoData();
        }

        private async Task RefreshCryptoData()
        {
            try
            {
                allCryptos = await binanceService.GetTopCryptosAsync();
                var txtSearch = panel2.Controls.Find("txtCryptoSearch", true).FirstOrDefault() as TextBox;
                if (txtSearch != null)
                {
                    TxtSearch_TextChanged(txtSearch, EventArgs.Empty);
                }
                else
                {
                    filteredCryptos = new List<CryptoData>(allCryptos);
                    DisplayCryptos();
                }
            }
            catch (Exception ex)
            {
                // Handle refresh error silently
            }
        }

        private void LoadDashboardData()
        {
            List<Trade> allTrades;
            using (var db = new AppDbContext())
            {
                allTrades = db.Trades.ToList();
            }
            var dashboardManager = new DashboardManager();
            var report = dashboardManager.GenerateReport(allTrades);
            UpdateUi(report);
        }

        private void UpdateUi(DashboardReport report)
        {
            lblPortfolioValue.Text = report.TotalPortfolioValue.ToString("C");
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

        public List<string> GetAvailableCryptoSymbols()
        {
            return binanceService.GetAllSymbols();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            refreshTimer?.Stop();
            refreshTimer?.Dispose();

            foreach (var icon in cryptoIcons.Values)
            {
                icon?.Dispose();
            }
            cryptoIcons.Clear();

            base.OnFormClosing(e);
        }
    }
}
