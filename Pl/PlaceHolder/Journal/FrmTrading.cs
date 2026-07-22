using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;
using TradingJournal.Core.Logic.Services.Exchange;

namespace TradingJournal.Pl.PlaceHolder.Journal
{
    /// <summary>
    /// Live trading. Amount is the MARGIN committed (position = amount x leverage). Buy/Sell buttons
    /// place the order; Liq/Cost/Max preview updates live; the grid shows all positions with a Close
    /// button. Sizing/precision/cap live in TradingManager. Platform balance shows only here.
    ///
    /// Auto-journal: when enabled, closed exchange trades are imported into the journal on connect and
    /// on a timer. It only imports trades closed AFTER it was switched on, and de-dupes by ExternalId
    /// so nothing is ever journaled twice.
    /// </summary>
    public partial class FrmTrading : UserControl, IResponsiveChildForm
    {
        private const string ColSymbol = "colPosSymbol";
        private const string ColSide = "colPosSide";
        private const string ColQty = "colPosQty";
        private const string ColEntry = "colPosEntry";
        private const string ColMark = "colPosMark";
        private const string ColLiq = "colPosLiq";
        private const string ColMargin = "colPosMargin";
        private const string ColPnl = "colPosPnl";
        private const string ColRoi = "colPosRoi";
        private const string ColLev = "colPosLev";
        private const string ColTp = "colPosTp";
        private const string ColClose = "colPosClose";

        private static readonly Color BuyColor = Color.FromArgb(14, 203, 129);
        private static readonly Color SellColor = Color.FromArgb(246, 70, 93);
        private static readonly Color LongColor = Color.MediumSeaGreen;
        private static readonly Color ShortColor = Color.OrangeRed;
        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

        private readonly PlatformManager _platforms = new PlatformManager();
        private readonly TradingManager _trading = new TradingManager();
        private readonly TradeReconciler _reconciler = new TradeReconciler();
        private readonly ResponsiveLayoutManager _layoutManager;
        private readonly System.Windows.Forms.Timer _positionsTimer;
        private readonly System.Windows.Forms.Timer _priceTimer;
        private readonly System.Windows.Forms.Timer _journalTimer;

        private CheckBox? chkAutoJournal;
        private string _platformKey = string.Empty;
        private bool _reconciling;

        private IExchangeClient _client;
        private bool _pollingPositions;
        private bool _pollingPrice;
        private decimal _lastPrice;
        private decimal _availableUsdt;

        public FrmTrading()
        {
            InitializeComponent();

            _positionsTimer = new System.Windows.Forms.Timer { Interval = 3000 };
            _positionsTimer.Tick += async (s, e) => await RefreshPositionsAsync();
            _priceTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _priceTimer.Tick += async (s, e) => await RefreshPriceAsync();
            _journalTimer = new System.Windows.Forms.Timer { Interval = 30000 };
            _journalTimer.Tick += async (s, e) => await ReconcileJournalAsync(announceIdle: false);

            SetupGrid();
            SetupOrderControls();
            SetupSideButtons();
            BuildAutoJournalToggle();   // before the responsive layout so it's registered with its panel

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            btnConnect.Click += async (s, e) => await ConnectAsync();
            btnBuy.Click += async (s, e) => await PlaceOrderAsync(OrderSide.Buy);
            btnSell.Click += async (s, e) => await PlaceOrderAsync(OrderSide.Sell);

            cmbOrderType.SelectedIndexChanged += (s, e) => { ApplyOrderTypeVisibility(); UpdateOrderInfo(); };
            cmbSymbol.SelectedIndexChanged += (s, e) => { _lastPrice = 0m; UpdateOrderInfo(); };
            txtUsdtAmount.TextChanged += (s, e) => UpdateOrderInfo();
            txtLeverage.TextChanged += (s, e) => UpdateOrderInfo();
            txtLimitPrice.TextChanged += (s, e) => UpdateOrderInfo();
            txtTriggerPrice.TextChanged += (s, e) => UpdateOrderInfo();
            dgvPositions.CellContentClick += async (s, e) => await Grid_CellContentClick(e);

            this.Load += (s, e) => RefreshPlatforms();
            ThemeManager.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) =>
            {
                ThemeManager.ThemeChanged -= OnThemeChanged;
                _positionsTimer.Stop(); _positionsTimer.Dispose();
                _priceTimer.Stop(); _priceTimer.Dispose();
                _journalTimer.Stop(); _journalTimer.Dispose();
                (_client as IDisposable)?.Dispose();
            };
            ApplyTheme();
        }

        private void OnThemeChanged(object sender, EventArgs e) => ApplyTheme();

        public void SetWindowState(FormWindowStateExtended newState) => _layoutManager.SetWindowState(newState);

        // Move each panel's controls into its wider *_Max twin when the window is maximized, so the
        // module spans the full width instead of sitting at fixed width with empty space beside it.
        private void InitializeResponsiveLayouts()
        {
            RegisterSectionAsIs(pnlPlatforms, pnlPlatforms_Max);
            RegisterSectionAsIs(pnlActiveTrades, pnlActiveTrades_Max);
            RegisterSectionAsIs(pnlAddTrades, pnlAddTrades_Max);
        }

        private void RegisterSectionAsIs(Panel normal, Panel max)
        {
            foreach (Control c in normal.Controls)
                _layoutManager.RegisterControl(c, normal, max, c.Location, c.Size);
        }

        // Stop the AutoScroll panel from jumping to whatever control gets focus/updates.
        protected override Point ScrollToControl(Control activeControl) => AutoScrollPosition;

        // ----------------------------------------------------------------- Setup
        private void SetupOrderControls()
        {
            AddItems(cmbOrderType, "Market", "Limit", "Conditional");
            AddItems(cmbMarginMode, "Cross", "Isolated");

            cmbSymbol.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSymbol.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbSymbol.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (string.IsNullOrWhiteSpace(txtLeverage.Text)) txtLeverage.Text = "10";
            ApplyOrderTypeVisibility();
            SetOrderControlsEnabled(false);
            UpdateOrderInfo();
        }

        private void SetupSideButtons()
        {
            foreach (var (btn, color) in new[] { (btnBuy, BuyColor), (btnSell, SellColor) })
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = color;
                btn.ForeColor = Color.White;
            }
        }

        private void StyleActionButton(Control b)
        {
            if (b is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = ThemeManager.ActiveButtonColor;
                btn.ForeColor = ThemeManager.TextColor;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = ThemeManager.ButtonHoverColor;
            }
        }

        private static void AddItems(ComboBox box, params string[] items)
        {
            if (box.Items.Count == 0) box.Items.AddRange(items);
            box.DropDownStyle = ComboBoxStyle.DropDownList;
            if (box.SelectedIndex < 0) box.SelectedIndex = 0;
        }

        private void ApplyOrderTypeVisibility()
        {
            var kind = cmbOrderType.SelectedItem?.ToString();
            bool isLimit = kind == "Limit";
            bool isConditional = kind == "Conditional";
            txtLimitPrice.Enabled = isLimit;
            txtTriggerPrice.Enabled = isConditional;
            if (!isLimit) txtLimitPrice.Clear();
            if (!isConditional) txtTriggerPrice.Clear();
        }

        private void SetOrderControlsEnabled(bool on)
        {
            foreach (var c in new Control[] { cmbSymbol, cmbOrderType, cmbMarginMode, txtLeverage,
                                              txtUsdtAmount, txtLimitPrice, txtTriggerPrice,
                                              txtStopLoss, txtTakeProfit, btnBuy, btnSell })
                c.Enabled = on;
            if (on) ApplyOrderTypeVisibility();
        }

        private void SetupGrid()
        {
            dgvPositions.AllowUserToAddRows = false;
            dgvPositions.AllowUserToDeleteRows = false;
            dgvPositions.RowHeadersVisible = false;
            dgvPositions.MultiSelect = false;
            dgvPositions.ReadOnly = true;
            dgvPositions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPositions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPositions.ColumnHeadersHeight = 34;
            dgvPositions.RowTemplate.Height = 32;
        }

        // ----------------------------------------------------------------- Auto-journal toggle (code-created)
        // Built in code rather than the Designer so no .Designer.cs edit is needed. It sits in the
        // Platforms panel between the platform dropdown (y=157) and the Connect button (y=263). If it
        // overlaps anything on your build, just change the Location below.
        private void BuildAutoJournalToggle()
        {
            chkAutoJournal = new CheckBox
            {
                Name = "chkAutoJournal",
                Text = "Auto-journal closed trades",
                AutoSize = true,
                Location = new Point(45, 212),
                ForeColor = ThemeManager.TextColor,
                Checked = SettingsManager.Load().AutoJournal.Enabled
            };
            chkAutoJournal.CheckedChanged += async (s, e) => await OnAutoJournalToggled();
            pnlPlatforms.Controls.Add(chkAutoJournal);
        }

        private async Task OnAutoJournalToggled()
        {
            if (chkAutoJournal == null) return;

            var settings = SettingsManager.Load();
            settings.AutoJournal.Enabled = chkAutoJournal.Checked;
            settings.Save();

            if (chkAutoJournal.Checked)
            {
                if (_client != null)
                {
                    _journalTimer.Start();
                    await ReconcileJournalAsync(announceIdle: true);
                }
                else
                {
                    SetStatus("Auto-journal on. It starts importing once you connect.", true);
                }
            }
            else
            {
                _journalTimer.Stop();
                SetStatus("Auto-journal off.", true);
            }
        }

        // Pulls closed trades from the exchange and journals the new ones. Overlap-guarded so the timer,
        // the connect step and the toggle can't run it twice at once.
        private async Task ReconcileJournalAsync(bool announceIdle)
        {
            if (_client == null || _reconciling) return;
            if (chkAutoJournal == null || !chkAutoJournal.Checked) return;
            if (string.IsNullOrEmpty(_platformKey)) return;

            _reconciling = true;
            try
            {
                var result = await _reconciler.ReconcileAsync(_client, _platformKey);
                if (!result.Ok) { SetStatus($"Auto-journal: {result.Error}", false); return; }

                if (result.Added > 0) SetStatus($"Auto-journaled {result.Added} new trade(s).", true);
                else if (announceIdle) SetStatus("Auto-journal on. No new closed trades yet.", true);
            }
            catch (Exception ex) { SetStatus($"Auto-journal failed: {ex.Message}", false); }
            finally { _reconciling = false; }
        }

        // ----------------------------------------------------------------- Platforms / connect
        private void RefreshPlatforms()
        {
            var list = new List<ExchangePlatform>(_platforms.GetAll());
            cmbPlatform.DisplayMember = nameof(ExchangePlatform.Name);
            cmbPlatform.ValueMember = nameof(ExchangePlatform.Id);
            cmbPlatform.DataSource = list;
            cmbPlatform.SelectedIndex = list.Count > 0 ? 0 : -1;
            btnConnect.Enabled = list.Count > 0;
            if (list.Count == 0) SetStatus("Add a platform first (Platforms tab).", ok: false);
        }

        private async Task ConnectAsync()
        {
            if (!(cmbPlatform.SelectedItem is ExchangePlatform platform)) { SetStatus("Select a platform.", false); return; }

            _positionsTimer.Stop(); _priceTimer.Stop(); _journalTimer.Stop();
            (_client as IDisposable)?.Dispose(); _client = null;
            _platformKey = $"{platform.Exchange}:{platform.Id}";
            SetOrderControlsEnabled(false);
            btnConnect.Enabled = false;
            SetStatus($"Connecting to {platform.Name}…", true);

            try
            {
                var creds = _platforms.GetCredentials(platform.Id);
                _client = ExchangeClientFactory.Create(creds);

                await RefreshBalanceAsync();

                var symbols = await _client.GetSymbolsAsync();
                cmbSymbol.DataSource = new List<string>(symbols);

                await RefreshPositionsAsync();
                _positionsTimer.Start();
                _priceTimer.Start();
                SetOrderControlsEnabled(true);

                SetStatus($"Connected to {platform.Name} ({(creds.UseTestnet ? "TESTNET" : "LIVE")}).", true);

                if (chkAutoJournal != null && chkAutoJournal.Checked)
                {
                    _journalTimer.Start();
                    await ReconcileJournalAsync(announceIdle: false);
                }
            }
            catch (Exception ex)
            {
                lblBalance.Text = "—";
                dgvPositions.Rows.Clear();
                SetStatus(ex.Message, false);
            }
            finally { btnConnect.Enabled = true; }
        }

        private async Task RefreshBalanceAsync()
        {
            if (_client == null) return;
            try
            {
                var b = await _client.GetBalanceAsync();
                _availableUsdt = b.AvailableUsdt;
                lblBalance.Text = $"{b.AvailableUsdt:0.00} USDT available  ·  {b.WalletUsdt:0.00} wallet";
                UpdateOrderInfo();
            }
            catch { }
        }

        // ----------------------------------------------------------------- Live price + info
        private async Task RefreshPriceAsync()
        {
            if (_client == null || _pollingPrice) return;
            var symbol = cmbSymbol.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(symbol)) return;

            _pollingPrice = true;
            try
            {
                var t = await _client.GetPriceAsync(symbol);
                lblPrice.ForeColor = t.Price > _lastPrice ? LongColor : t.Price < _lastPrice ? ShortColor : ThemeManager.TextColor;
                lblPrice.Text = $"{symbol}  {t.Price.ToString("0.####", Inv)}";
                _lastPrice = t.Price;
                UpdateOrderInfo();
            }
            catch { }
            finally { _pollingPrice = false; }
        }

        /// <summary>Cost = the margin you commit; Max = available margin; Liq = rough per-side estimate.</summary>
        private void UpdateOrderInfo()
        {
            lblBuyLiq.Text = lblSellLiq.Text = "-- USDT";
            lblBuyMax.Text = lblSellMax.Text = _availableUsdt.ToString("0.00", Inv) + " USDT";

            bool haveAmt = NumericInput.TryParseDecimal(txtUsdtAmount.Text, out var margin) && margin > 0;
            lblBuyCost.Text = lblSellCost.Text = (haveAmt ? margin : 0m).ToString("0.00", Inv) + " USDT";

            bool haveLev = NumericInput.TryParseDecimal(txtLeverage.Text, out var lev) && lev >= 1;
            decimal entry = SizingPrice();
            if (entry > 0 && haveLev)
            {
                decimal d = 1m / lev;
                lblBuyLiq.Text = (entry * (1m - d)).ToString("0.####", Inv) + " USDT";
                lblSellLiq.Text = (entry * (1m + d)).ToString("0.####", Inv) + " USDT";
            }
        }

        private decimal SizingPrice()
        {
            var kind = cmbOrderType.SelectedItem?.ToString();
            if (kind == "Limit" && NumericInput.TryParseDecimal(txtLimitPrice.Text, out var lp)) return lp;
            if (kind == "Conditional" && NumericInput.TryParseDecimal(txtTriggerPrice.Text, out var tp)) return tp;
            return _lastPrice;
        }

        // ----------------------------------------------------------------- Place order
        private async Task PlaceOrderAsync(OrderSide side)
        {
            if (_client == null) { SetStatus("Connect to a platform first.", false); return; }

            var req = ReadOrderForm(side, out var error);
            if (req == null) { SetStatus(error, false); return; }

            btnBuy.Enabled = btnSell.Enabled = false;
            try
            {
                var (ok, preview, order, _) = await _trading.PrepareAsync(_client, req);
                if (!ok) { SetStatus(preview, false); return; }

                if (MessageBox.Show(preview + "\n\nPlace this order?", "Confirm order",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                { SetStatus("Order cancelled.", true); return; }

                var outcome = await _trading.PlaceAsync(_client, order, req.Leverage, req.MarginMode);
                SetStatus(outcome.Message, outcome.Ok);
                if (outcome.Ok) { await RefreshPositionsAsync(); await RefreshBalanceAsync(); }
            }
            catch (Exception ex) { SetStatus(ex.Message, false); }
            finally { btnBuy.Enabled = btnSell.Enabled = true; }
        }

        private NewTradeRequest ReadOrderForm(OrderSide side, out string error)
        {
            error = null;
            var symbol = cmbSymbol.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(symbol)) { error = "Select a symbol."; return null; }
            if (!NumericInput.TryParseDecimal(txtUsdtAmount.Text, out var usdt)) { error = "Enter the USDT amount (margin)."; return null; }
            if (!NumericInput.TryParseDecimal(txtLeverage.Text, out var lev) || lev < 1) { error = "Enter a valid leverage."; return null; }

            var kind = cmbOrderType.SelectedItem?.ToString() switch
            {
                "Limit" => OrderKind.Limit,
                "Conditional" => OrderKind.Conditional,
                _ => OrderKind.Market
            };

            decimal? limit = null, trigger = null, sl = null, tp = null;
            if (kind == OrderKind.Limit && !TryOptional(txtLimitPrice, out limit, ref error, "limit price")) return null;
            if (kind == OrderKind.Conditional && !TryOptional(txtTriggerPrice, out trigger, ref error, "trigger price")) return null;
            if (!TryOptional(txtStopLoss, out sl, ref error, "stop-loss", optional: true)) return null;
            if (!TryOptional(txtTakeProfit, out tp, ref error, "take-profit", optional: true)) return null;

            return new NewTradeRequest
            {
                Symbol = symbol,
                Side = side,
                Kind = kind,
                UsdtAmount = usdt,
                Leverage = (int)lev,
                MarginMode = cmbMarginMode.SelectedItem?.ToString() == "Isolated" ? MarginMode.Isolated : MarginMode.Cross,
                LimitPrice = limit,
                TriggerPrice = trigger,
                StopLoss = sl,
                TakeProfit = tp
            };
        }

        private static bool TryOptional(TextBox box, out decimal? value, ref string error, string label, bool optional = false)
        {
            value = null;
            if (string.IsNullOrWhiteSpace(box.Text)) { if (optional) return true; error = $"Enter a {label}."; return false; }
            if (!NumericInput.TryParseDecimal(box.Text, out var v)) { error = $"Invalid {label}."; return false; }
            value = v;
            return true;
        }

        // ----------------------------------------------------------------- Positions + close
        private async Task RefreshPositionsAsync()
        {
            if (_client == null || _pollingPositions) return;
            _pollingPositions = true;
            try { FillPositions(await _client.GetPositionsAsync()); }
            catch (Exception ex) { SetStatus($"Positions update failed: {ex.Message}", false); }
            finally { _pollingPositions = false; }
        }

        private void FillPositions(IReadOnlyList<PositionInfo> positions)
        {
            dgvPositions.Rows.Clear();
            foreach (var p in positions)
            {
                bool isLong = p.Side == OrderSide.Buy;
                decimal roi = p.Margin > 0 ? p.UnrealizedPnl / p.Margin * 100m : 0m;

                int i = dgvPositions.Rows.Add();
                var row = dgvPositions.Rows[i];
                row.Cells[ColSymbol].Value = p.Symbol;
                row.Cells[ColSide].Value = isLong ? "▲ Long" : "▼ Short";
                row.Cells[ColQty].Value = p.Quantity.ToString("0.######", Inv);
                row.Cells[ColEntry].Value = p.EntryPrice.ToString("0.####", Inv);
                row.Cells[ColMark].Value = p.MarkPrice.ToString("0.####", Inv);
                row.Cells[ColLiq].Value = p.LiquidationPrice.ToString("0.####", Inv);
                row.Cells[ColMargin].Value = p.Margin.ToString("0.00", Inv) + " USDT";
                row.Cells[ColPnl].Value = p.UnrealizedPnl.ToString("0.00", Inv) + " USDT";
                row.Cells[ColRoi].Value = roi.ToString("0.00", Inv) + " %";
                row.Cells[ColLev].Value = p.Leverage.ToString("0", Inv) + "x " + p.MarginMode;

                var pnlColor = p.UnrealizedPnl > 0 ? LongColor : p.UnrealizedPnl < 0 ? ShortColor : ThemeManager.TextColor;
                row.Cells[ColSide].Style.ForeColor = isLong ? LongColor : ShortColor;
                row.Cells[ColPnl].Style.ForeColor = pnlColor;
                row.Cells[ColRoi].Style.ForeColor = pnlColor;
                row.Cells[ColLiq].Style.ForeColor = ShortColor;
            }
        }

        private async Task Grid_CellContentClick(DataGridViewCellEventArgs e)
        {
            if (_client == null || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var colName = dgvPositions.Columns[e.ColumnIndex].Name;
            var row = dgvPositions.Rows[e.RowIndex];
            var symbol = row.Cells[ColSymbol].Value?.ToString();
            if (string.IsNullOrWhiteSpace(symbol)) return;

            if (colName == ColClose) await CloseFullAsync(symbol);
            else if (colName == ColTp) await TakeProfitAsync(symbol);
        }

        private async Task CloseFullAsync(string symbol)
        {
            if (MessageBox.Show($"Close your {symbol} position at market?", "Close position",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                var res = await _client.ClosePositionAsync(symbol, null);
                SetStatus(res.Success ? $"Closed {symbol}." : res.Message, res.Success);
                await RefreshPositionsAsync(); await RefreshBalanceAsync();
            }
            catch (Exception ex) { SetStatus(ex.Message, false); }
        }

        private async Task TakeProfitAsync(string symbol)
        {
            // Live position size + unrealized PnL from the exchange (source of truth; a grid cell was fragile).
            decimal posQty = 0m, pnl = 0m;
            try
            {
                foreach (var p in await _client.GetPositionsAsync())
                    if (string.Equals(p.Symbol, symbol, StringComparison.OrdinalIgnoreCase))
                    { posQty = p.Quantity; pnl = p.UnrealizedPnl; break; }
            }
            catch (Exception ex) { SetStatus(ex.Message, false); return; }

            if (posQty <= 0) { SetStatus($"No open {symbol} position to reduce.", false); return; }
            if (pnl <= 0)
            {
                SetStatus($"{symbol} isn't in profit (uPnL {FormatUsdt(pnl)}) — nothing to take.", false);
                return;
            }

            SymbolRules rules;
            try { rules = await _client.GetSymbolRulesAsync(symbol); }
            catch (Exception ex) { SetStatus(ex.Message, false); return; }

            // Ask how much PROFIT to realise, in USDT. Max is the current unrealized PnL — that's all you've made.
            var requestedProfit = PromptUsdt(symbol, pnl);
            if (requestedProfit == null) return;

            // Profit is uniform per coin, so realising P of the total PnL means closing exactly P/PnL of the
            // position. Take half the profit and half the position closes; take all of it and the trade closes.
            decimal fraction = requestedProfit.Value / pnl;
            decimal qty = FloorToStep(posQty * fraction, rules.StepSize);
            if (qty <= 0)
            {
                SetStatus($"That amount is too small to close a whole step of {symbol}.", false);
                return;
            }
            if (qty >= posQty) { await CloseFullAsync(symbol); return; } // taking all the profit = closing the trade

            try
            {
                var res = await _client.ClosePositionAsync(symbol, qty); // reduceOnly; the rest stays open
                decimal pctClosed = qty / posQty * 100m;
                SetStatus(res.Success
                    ? $"Took ≈ {FormatUsdt(requestedProfit.Value)} profit on {symbol} " +
                      $"(closed {FormatQty(qty)}, ≈ {pctClosed.ToString("0.#", Inv)}% of the position). Rest still open."
                    : res.Message, res.Success);
            }
            catch (Exception ex) { SetStatus(ex.Message, false); }
            finally
            {
                await RefreshPositionsAsync();
                await RefreshBalanceAsync();
            }
        }

        private static decimal FloorToStep(decimal value, decimal step)
            => step <= 0 ? value : Math.Floor(value / step) * step;

        private static string FormatQty(decimal v) => v.ToString("0.########", Inv);

        private static string FormatUsdt(decimal v) => v.ToString("0.00", Inv) + " USDT";

        private static decimal? PromptUsdt(string symbol, decimal maxUsdt)
        {
            using var form = new Form
            {
                Text = "Take profit",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(360, 148),
                BackColor = ThemeManager.PanelColor,
                ForeColor = ThemeManager.TextColor
            };
            var lbl = new Label
            {
                Text = $"Profit to take on {symbol}, in USDT.\nMax {FormatUsdt(maxUsdt)}  (all profit — closes the trade)",
                Left = 12,
                Top = 12,
                Width = 336,
                Height = 46,
                ForeColor = ThemeManager.TextColor
            };
            var txt = new TextBox
            {
                Left = 12,
                Top = 64,
                Width = 336,
                Text = maxUsdt.ToString("0.00", Inv),
                BackColor = ThemeManager.TextBoxColor,
                ForeColor = ThemeManager.TextColor
            };
            var ok = new Button
            {
                Text = "Take profit",
                Left = 230,
                Top = 104,
                Width = 118,
                DialogResult = DialogResult.OK,
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeManager.ButtonColor,
                ForeColor = ThemeManager.TextColor
            };
            form.Controls.AddRange(new Control[] { lbl, txt, ok });
            form.AcceptButton = ok;
            if (form.ShowDialog() != DialogResult.OK) return null;
            if (!NumericInput.TryParseDecimal(txt.Text, out var v) || v <= 0) return null;
            return v > maxUsdt ? maxUsdt : v; // clamp to the full position value
        }

        private void SetStatus(string message, bool ok)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = ok ? Color.MediumSeaGreen : Color.OrangeRed;
        }

        // ----------------------------------------------------------------- Theme
        private void ApplyTheme()
        {
            BackColor = ThemeManager.BackgroundColor;
            StyleRecursively(this);
            StyleGrid();
            StyleActionButton(btnConnect); // keep it visible (distinct from the panel)
            if (chkAutoJournal != null) chkAutoJournal.ForeColor = ThemeManager.TextColor;
        }

        private void StyleRecursively(Control root)
        {
            foreach (Control c in root.Controls)
            {
                switch (c)
                {
                    case DataGridView _: break;
                    case Panel panel: panel.BackColor = ThemeManager.PanelColor; break;
                    case TextBox tb: tb.BackColor = ThemeManager.TextBoxColor; tb.ForeColor = ThemeManager.TextColor; break;
                    case ComboBox cmb: cmb.BackColor = ThemeManager.TextBoxColor; cmb.ForeColor = ThemeManager.TextColor; cmb.FlatStyle = FlatStyle.Flat; break;
                    case Button btn:
                        if (btn == btnBuy || btn == btnSell || btn == btnConnect) break; // colored separately
                        btn.FlatStyle = FlatStyle.Flat; btn.ForeColor = ThemeManager.TextColor; btn.BackColor = ThemeManager.ButtonColor;
                        break;
                    case Label lbl: lbl.ForeColor = ThemeManager.TextColor; break;
                }
                if (c.HasChildren && !(c is DataGridView)) StyleRecursively(c);
            }
        }

        private void StyleGrid()
        {
            dgvPositions.EnableHeadersVisualStyles = false;
            dgvPositions.BackgroundColor = ThemeManager.PanelColor;
            dgvPositions.GridColor = ThemeManager.BackgroundColor;
            dgvPositions.BorderStyle = BorderStyle.None;
            dgvPositions.DefaultCellStyle.BackColor = ThemeManager.TextBoxColor;
            dgvPositions.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvPositions.DefaultCellStyle.SelectionBackColor = ThemeManager.ButtonHoverColor;
            dgvPositions.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
            dgvPositions.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.ButtonColor;
            dgvPositions.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;

            // Colour the action buttons so they don't render as default grey Windows buttons.
            StyleButtonColumn(ColClose, SellColor, Color.White); // red = close / exit
            StyleButtonColumn(ColTp, BuyColor, Color.White);     // green = take profit
        }

        private void StyleButtonColumn(string columnName, Color back, Color fore)
        {
            if (dgvPositions.Columns.Contains(columnName) &&
                dgvPositions.Columns[columnName] is DataGridViewButtonColumn col)
            {
                col.FlatStyle = FlatStyle.Flat;
                col.DefaultCellStyle.BackColor = back;
                col.DefaultCellStyle.ForeColor = fore;
                col.DefaultCellStyle.SelectionBackColor = back;
                col.DefaultCellStyle.SelectionForeColor = fore;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}