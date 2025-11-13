using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;
using TradingJournal.Core.Managers;

namespace TradingJournal.Pl.PlaceHolder.Recovery_Planner
{
    public partial class FrmRecoveryPlanner : UserControl, IResponsiveChildForm
    {
        private readonly BinanceApiService _binance = new BinanceApiService();
        private readonly RecoveryCaseManager _manager = new RecoveryCaseManager();
        private readonly BindingSource _bs = new BindingSource();
        private FrmAllocations? _allocationsWindow;

        private int? _caseIdToUpdate = null;
        private readonly System.Windows.Forms.Timer _priceTimer;

        private readonly ResponsiveLayoutManager _layoutManager;

        // Whole words (no odd mid-word abbreviations).

        // Short/Full labels tightened so they fit on fullscreen without truncation.
        private readonly Dictionary<string, (string Short, string Full)> _headerText = new()
        {
            { "Symbol",              ("Symbol",         "Symbol") },
            { "EntryDate",           ("Start Date",     "Start Date") },
            { "EntryPrice",          ("Entry Price",    "Entry Price") },
            { "InitialInvestedUSDT", ("Start Cost $",   "Start Cost $") },
            { "InitialQuantity",     ("Start Qty",      "Start Qty") },

            { "TotalQuantity",       ("Total Qty",      "Total Qty") },
            { "TotalInvestedUSDT",   ("Total Cost $",   "Total Cost $") },
            { "AverageEntryPrice",   ("Avg Entry (BE)", "Avg Entry (BE)") },

            { "CurrentPrice",        ("Price Now",      "Price Now") },
            { "CurrentValue",        ("Value Now $",    "Value Now $") },
            { "UnrealizedPnL",       ("Unrealized P/L", "Unrealized P/L") },
            { "PriceToBreakEven",    ("To BE (Price)",  "To BE (Price)") }, // delta to break even
            { "ProgressPct",         ("Progress %",     "Progress %") },

            { "Status",              ("Status",         "Status") },
        };

        public FrmRecoveryPlanner()
        {
            InitializeComponent();

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            TryRoundUi();
            ConfigureInputsVisuals();
            CenterIconButtons();

            rbRcModeInvested.CheckedChanged += AmountMode_CheckedChanged;
            rbRcModeQuantity.CheckedChanged += AmountMode_CheckedChanged;

            cbRcSymbol.SelectedIndexChanged += async (s, e) => { UpdateFormTitle(); await UpdateTopLiveAsync(); };
            dtpEntryDate.ValueChanged += (s, e) => UpdateFormTitle();
            txtRcEntryPrice.TextChanged += async (s, e) => { UpdateFormTitle(); await UpdateTopLiveAsync(); };
            txtRcInvestedUSDT.TextChanged += async (s, e) => await UpdateTopLiveAsync();
            txtRcQuantity.TextChanged += async (s, e) => await UpdateTopLiveAsync();

            btnRcAddCase.Click += btnRcAddCase_Click;
            btnRcUpdateCase.Click += btnRcUpdateCase_Click;
            btnRcCancelUpdate.Click += (s, e) => ExitUpdateMode();
            btnRcClear.Click += (s, e) => ClearForm();

            dgvRecoveryCases.CellPainting += dgvRecoveryCases_CellPainting;   // also paints header with ellipsis
            dgvRecoveryCases.CellFormatting += Dgv_CellFormatting;

            this.SizeChanged += (_, __) => ApplyWindowStateLayout();

            btnRcPauseResumeCase.Click += btnRcPauseResumeCase_Click;
            btnRcCloseCase.Click += btnRcCloseCase_Click;
            btnRcWriteOff.Click += btnRcWriteOff_Click;

            ThemeManager.ThemeChanged += async (s, e) =>
            {
                ApplyTheme();
                await UpdateTopLiveAsync();
                ApplyWindowStateLayout();
            };
            ApplyTheme();

            _priceTimer = new System.Windows.Forms.Timer { Interval = 60000 };
            _priceTimer.Tick += async (s, e) => await UpdateTopLiveAsync();
            _priceTimer.Start();

            InitializeStatusFilter();
            _ = LoadSymbols();
            _ = LoadCasesAsync();

            ConfigureStableGrid();
            ToggleAmountUi();
            SetupGrid();
            ApplyBaseGridStyling();
            ApplyNormalGridFonts();

            UpdateFormTitle();
            _ = UpdateTopLiveAsync();
        }

        private void TryRoundUi()
        {
            try
            {
                RoundedFormHelper.RoundPanel(pnlCaseEditor, 30);
                RoundedFormHelper.RoundPanel(pnlCases, 30);
                RoundedFormHelper.RoundPanel(pnlCases_Max, 30);
                RoundedFormHelper.RoundPanel(pnlCaseEditor_Max, 30);

                RoundedFormHelper.MakeButtonRounded(btnRcAddCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcUpdateCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcCancelUpdate, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcClear, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcPauseResumeCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcCloseCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcWriteOff, 24);
            }
            catch { }
        }

        private void ConfigureInputsVisuals()
        {
            foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity })
            {
                tb.BorderStyle = BorderStyle.None;
                tb.Multiline = true;
                tb.AutoSize = false;
                tb.Padding = new Padding(0);
                tb.Margin = new Padding(0);
            }
        }

        private void CenterIconButtons()
        {
            foreach (var b in new[]
            {
                btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff
            })
            {
                b.IconChar = IconChar.None;
                b.AutoSize = false;
                b.UseCompatibleTextRendering = true;
                b.TextAlign = ContentAlignment.MiddleCenter;
                b.ImageAlign = ContentAlignment.MiddleCenter;
                b.TextImageRelation = TextImageRelation.Overlay;
                b.Padding = Padding.Empty;
                b.Margin = new Padding(0);
            }
        }

        #region Theme
        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            pnlCaseEditor.BackColor = ThemeManager.PanelColor;
            pnlCases.BackColor = ThemeManager.PanelColor;
            pnlCaseEditor_Max.BackColor = ThemeManager.PanelColor;
            pnlCases_Max.BackColor = ThemeManager.PanelColor;

            foreach (var lbl in pnlCaseEditor.Controls.OfType<Label>())
                lbl.ForeColor = ThemeManager.DataTextColor;
            foreach (var lbl in pnlCases.Controls.OfType<Label>())
                lbl.ForeColor = ThemeManager.DataTextColor;

            foreach (var cb in new[] { cbRcSymbol, cbRcStatusFilter })
            {
                cb.BackColor = ThemeManager.PanelColor;
                cb.ForeColor = ThemeManager.TextColor;
                cb.FlatStyle = FlatStyle.Flat;
                cb.IntegralHeight = false;
                cb.DropDownHeight = 240;
            }

            foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity })
            {
                tb.BackColor = ThemeManager.TextBoxColor;
                tb.ForeColor = ThemeManager.TextColor;
                tb.BorderStyle = BorderStyle.None;
            }

            var toolbarBlue = ThemeManager.UploadScreenshotButtonColor;
            btnRcPauseResumeCase.BackColor = toolbarBlue;
            btnRcCloseCase.BackColor = toolbarBlue;
            btnRcWriteOff.BackColor = toolbarBlue;

            lblRcHint.AutoSize = false;
            lblRcHint.AutoEllipsis = true;
            lblRcHint.TextAlign = ContentAlignment.MiddleLeft;

            foreach (var b in new[] { btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                                      btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff })
            {
                b.ForeColor = ThemeManager.ActionButtonTextColor;
                b.FlatAppearance.BorderSize = 0;
            }

            dgvRecoveryCases.BackgroundColor = ThemeManager.PanelColor;
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.EnableHeadersVisualStyles = false;

            dgvRecoveryCases.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            dgvRecoveryCases.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DataGrid;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            grpRcAmountMode.ForeColor = ThemeManager.TextColor;
        }
        #endregion

        #region Init helpers
        private void InitializeStatusFilter()
        {
            cbRcStatusFilter.Items.Clear();
            cbRcStatusFilter.Items.Add("All");
            cbRcStatusFilter.Items.Add(RecoveryCaseStatus.Active);
            cbRcStatusFilter.Items.Add(RecoveryCaseStatus.Paused);
            cbRcStatusFilter.Items.Add(RecoveryCaseStatus.Closed);
            cbRcStatusFilter.Items.Add(RecoveryCaseStatus.WrittenOff);
            cbRcStatusFilter.SelectedIndex = 0;
        }

        private async Task LoadSymbols()
        {
            try
            {
                var s = await new System.Net.Http.HttpClient().GetStringAsync("https://api.binance.com/api/v3/exchangeInfo");
                using var doc = System.Text.Json.JsonDocument.Parse(s);
                var list = new List<string>();
                foreach (var el in doc.RootElement.GetProperty("symbols").EnumerateArray())
                {
                    var sym = el.GetProperty("symbol").GetString();
                    if (sym != null && sym.EndsWith("USDT", StringComparison.OrdinalIgnoreCase))
                        list.Add(sym.Replace("USDT", "/USDT"));
                }
                list.Sort(StringComparer.OrdinalIgnoreCase);
                cbRcSymbol.DataSource = list;
                cbRcSymbol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading symbols: {ex.Message}");
            }
        }
        #endregion

        #region Amount mode toggle
        private void AmountMode_CheckedChanged(object? sender, EventArgs e) => ToggleAmountUi();

        private void ToggleAmountUi()
        {
            bool investedMode = rbRcModeInvested.Checked;

            lblRcInvested.Visible = investedMode;
            txtRcInvestedUSDT.Visible = investedMode;

            lblRcQuantity.Visible = !investedMode;
            txtRcQuantity.Visible = !investedMode;

            if (investedMode)
                txtRcQuantity.Text = string.Empty;
            else
                txtRcInvestedUSDT.Text = string.Empty;
        }
        #endregion

        #region Grid
        private void SetupGrid()
        {
            dgvRecoveryCases.AutoGenerateColumns = false;
            dgvRecoveryCases.Columns.Clear();

            dgvRecoveryCases.AllowUserToAddRows = false;
            dgvRecoveryCases.ReadOnly = true;
            dgvRecoveryCases.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvRecoveryCases.MultiSelect = false;
            dgvRecoveryCases.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvRecoveryCases.RowHeadersVisible = false;


            dgvRecoveryCases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvRecoveryCases.ScrollBars = ScrollBars.Both;
            dgvRecoveryCases.AllowUserToResizeColumns = true;

            var updateCol = new DataGridViewButtonColumn { Name = "UpdateColumn", HeaderText = "Update", Text = "Update", UseColumnTextForButtonValue = true, Width = 84, AutoSizeMode = DataGridViewAutoSizeColumnMode.None };
            var deleteCol = new DataGridViewButtonColumn { Name = "DeleteColumn", HeaderText = "Delete", Text = "Delete", UseColumnTextForButtonValue = true, Width = 84, AutoSizeMode = DataGridViewAutoSizeColumnMode.None };
            dgvRecoveryCases.Columns.Add(updateCol);
            dgvRecoveryCases.Columns.Add(deleteCol);

            // Data columns (give each a FillWeight so proportions stay stable)
            DataGridViewTextBoxColumn C(string prop, float weight)
                => new DataGridViewTextBoxColumn { DataPropertyName = prop, Name = prop, HeaderText = _headerText[prop].Short, FillWeight = weight, MinimumWidth = 70 };

            dgvRecoveryCases.Columns.AddRange(new DataGridViewColumn[]
            {
                C("Symbol",  80), C("EntryDate", 90), C("EntryPrice", 80),
                C("InitialInvestedUSDT", 90), C("InitialQuantity", 90),
                C("TotalQuantity", 90), C("TotalInvestedUSDT", 95),
                C("AverageEntryPrice", 100), C("CurrentPrice", 80),
                C("CurrentValue", 95), C("UnrealizedPnL", 95),
                C("PriceToBreakEven", 105), C("ProgressPct", 80),
                C("Status", 80),
            });

            dgvRecoveryCases.DataSource = _bs;

            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvRecoveryCases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void ConfigureStableGrid()
        {
            dgvRecoveryCases.ScrollBars = ScrollBars.Vertical;
            dgvRecoveryCases.Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0);
            dgvRecoveryCases.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // turn on double-buffering (private prop) to reduce visual jitter
            try
            {
                var pi = typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                pi?.SetValue(dgvRecoveryCases, true, null);
            }
            catch { /* best effort */ }
        }

        private void ApplyBaseGridStyling()
        {
            dgvRecoveryCases.BackgroundColor = ThemeManager.DataGrid;
            dgvRecoveryCases.GridColor = Color.FromArgb(45, 51, 73);
            dgvRecoveryCases.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.DataGridHeader;
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dgvRecoveryCases.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            dgvRecoveryCases.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DataGrid;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
        }

        // Paint "Update"/"Delete" buttons AND make header text show ellipsis (…) when it doesn't fit.
        private void dgvRecoveryCases_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                var text = dgvRecoveryCases.Columns[e.ColumnIndex].HeaderText;
                var bounds = e.CellBounds; bounds.Inflate(-4, -2);
                TextRenderer.DrawText(e.Graphics, text,
                    dgvRecoveryCases.ColumnHeadersDefaultCellStyle.Font,
                    bounds,
                    dgvRecoveryCases.ColumnHeadersDefaultCellStyle.ForeColor,
                    TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
                return;
            }

            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvRecoveryCases.Columns["DeleteColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var r = e.CellBounds; r.Inflate(-2, -2);
                using var br = new SolidBrush(Color.FromArgb(220, 53, 69));
                e.Graphics.FillRectangle(br, r);
                TextRenderer.DrawText(e.Graphics, "Delete", e.CellStyle.Font, r, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }

            if (e.ColumnIndex == dgvRecoveryCases.Columns["UpdateColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var r = e.CellBounds; r.Inflate(-2, -2);
                using var br = new SolidBrush(Color.FromArgb(0, 123, 255));
                e.Graphics.FillRectangle(br, r);
                TextRenderer.DrawText(e.Graphics, "Update", e.CellStyle.Font, r, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }
        }

        // Limit decimals & add some colors
        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is null) return;

            var prop = dgvRecoveryCases.Columns[e.ColumnIndex].DataPropertyName;

            if ((prop == "UnrealizedPnL" || prop == "ProgressPct") && e.Value != null)
            {
                if (prop == "UnrealizedPnL" && decimal.TryParse(e.Value.ToString(), out var pnl))
                    e.CellStyle.ForeColor = pnl >= 0 ? ThemeManager.PositiveColor : ThemeManager.NegativeColor;
                else if (prop == "ProgressPct" && int.TryParse(e.Value.ToString(), out var pct))
                {
                    if (pct >= 100) e.CellStyle.ForeColor = ThemeManager.PositiveColor;
                    else if (pct >= 60) e.CellStyle.ForeColor = ThemeManager.AccentColor;
                    else if (pct >= 20) e.CellStyle.ForeColor = ThemeManager.WarningColor;
                    else e.CellStyle.ForeColor = ThemeManager.NegativeColor;
                }
            }

            bool isMoney = prop is "InitialInvestedUSDT" or "TotalInvestedUSDT" or "CurrentValue" or "UnrealizedPnL";
            bool isPrice = prop is "EntryPrice" or "AverageEntryPrice" or "CurrentPrice" or "PriceToBreakEven";
            bool isQty = prop is "InitialQuantity" or "TotalQuantity";

            if (isMoney && decimal.TryParse(e.Value.ToString(), out var money))
                e.Value = money.ToString("0.00", CultureInfo.CurrentCulture);
            else if ((isPrice || isQty) && decimal.TryParse(e.Value.ToString(), out var num))
                e.Value = num >= 1m ? num.ToString("0.###", CultureInfo.CurrentCulture)
                                    : num.ToString("0.########", CultureInfo.CurrentCulture);
        }
        #endregion

        #region Load/Refresh cases
        private async Task LoadCasesAsync()
        {
            try
            {
                RecoveryCaseStatus? filter = null;
                if (cbRcStatusFilter.SelectedItem is RecoveryCaseStatus st) filter = st;

                var rows = await _manager.GetRowsAsync(filter);
                _bs.DataSource = rows;
                ApplyWindowStateLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load cases: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Top live KPIs
        private async Task UpdateTopLiveAsync()
        {
            try
            {
                var (ok, symbolRaw) = GetSelectedSymbolRaw();
                if (!ok)
                {
                    lblRcCurrentPrice.Text = "--";
                    lblRcCurrentValue.Text = "--";
                    lblRcUnrealized.Text = "--";
                    return;
                }

                var currentPrice = await _binance.GetLastPriceAsync(symbolRaw);
                lblRcCurrentPrice.Text = currentPrice.ToString(currentPrice >= 1m ? "0.###" : "0.########");

                var entryPrice = ParseDec(txtRcEntryPrice.Text);
                var invested = ParseDec(txtRcInvestedUSDT.Text);
                var qty = ParseDec(txtRcQuantity.Text);

                decimal initQty = qty ?? ((invested.HasValue && entryPrice.HasValue && entryPrice.Value > 0)
                    ? invested.Value / entryPrice.Value : 0m);
                decimal initInvested = invested ?? (initQty * (entryPrice ?? 0m));

                decimal currentValue = initQty * currentPrice;
                decimal unreal = currentValue - initInvested;
                decimal avgEntry = initQty > 0m ? initInvested / initQty : 0m;

                lblRcCurrentValue.Text = currentValue.ToString("0.00");
                lblRcUnrealized.Text = unreal.ToString("0.00");


                lblRcUnrealized.ForeColor = unreal >= 0 ? ThemeManager.PositiveColor : ThemeManager.NegativeColor;

            }
            catch
            {
                // best effort
            }
        }
        #endregion

        #region Add/Update/Delete/Status
        private async void btnRcAddCase_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs(out var symbolRaw, out var entryDate, out var entryPrice, out var invested, out var qty))
                return;

            try
            {
                _manager.AddCase(symbolRaw, entryDate, entryPrice, invested, qty, notes: null);
                MessageBox.Show("Case added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                await LoadCasesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Add failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRcUpdateCase_Click(object? sender, EventArgs e)
        {
            if (_caseIdToUpdate == null) return;

            if (!ValidateInputs(out var symbolRaw, out var entryDate, out var entryPrice, out var invested, out var qty))
                return;

            try
            {
                _manager.UpdateCase(_caseIdToUpdate.Value, symbolRaw, entryDate, entryPrice, invested, qty, notes: null);
                ExitUpdateMode();
                await LoadCasesAsync();
                MessageBox.Show("Case updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dgvRecoveryCases_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var id = (int)((dgvRecoveryCases.Rows[e.RowIndex].DataBoundItem as RecoveryCaseRow)!.Id);

            if (dgvRecoveryCases.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                if (MessageBox.Show("Delete this case (allocations will also be deleted)?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _manager.DeleteCase(id);
                    await LoadCasesAsync();
                }
                return;
            }

            if (dgvRecoveryCases.Columns[e.ColumnIndex].Name == "UpdateColumn")
            {
                var row = (RecoveryCaseRow)_bs[e.RowIndex];

                cbRcSymbol.Text = row.Symbol.Replace("USDT", "/USDT");
                dtpEntryDate.Value = row.EntryDate;
                txtRcEntryPrice.Text = row.EntryPrice.ToString(row.EntryPrice >= 1m ? "0.###" : "0.########");

                if (row.InitialQuantity.HasValue)
                {
                    rbRcModeQuantity.Checked = true;
                    txtRcQuantity.Text = row.InitialQuantity.Value.ToString("0.###");
                    txtRcInvestedUSDT.Text = "";
                }
                else
                {
                    rbRcModeInvested.Checked = true;
                    txtRcInvestedUSDT.Text = row.InitialInvestedUSDT?.ToString("0.00") ?? "";
                    txtRcQuantity.Text = "";
                }

                EnterUpdateMode(id);
            }
        }

        private void dgvRecoveryCases_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = (RecoveryCaseRow)_bs[e.RowIndex];

            if (_allocationsWindow == null || _allocationsWindow.IsDisposed)
            {
                _allocationsWindow = new FrmAllocations(row.Id);
                _allocationsWindow.FormClosed += async (_, __) =>
                {
                    _allocationsWindow = null;
                    await LoadCasesAsync();
                };
                _allocationsWindow.Show(this);
            }
            else
            {
                _allocationsWindow.Focus();
            }
        }

        private async void btnRcPauseResumeCase_Click(object sender, EventArgs e)
        {
            if (dgvRecoveryCases.CurrentRow?.DataBoundItem is not RecoveryCaseRow row) return;

            var newStatus =
                row.Status == RecoveryCaseStatus.Paused ? RecoveryCaseStatus.Active :
                row.Status == RecoveryCaseStatus.Active ? RecoveryCaseStatus.Paused :
                row.Status; // if Closed/WrittenOff, keep as-is

            if (newStatus != row.Status)
            {
                _manager.ChangeStatus(row.Id, newStatus);
                await LoadCasesAsync();
            }
        }

        private async void btnRcCloseCase_Click(object sender, EventArgs e)
        {
            if (dgvRecoveryCases.CurrentRow?.DataBoundItem is not RecoveryCaseRow row) return;
            _manager.ChangeStatus(row.Id, RecoveryCaseStatus.Closed);
            await LoadCasesAsync();
        }

        private async void btnRcWriteOff_Click(object sender, EventArgs e)
        {
            if (dgvRecoveryCases.CurrentRow?.DataBoundItem is not RecoveryCaseRow row) return;
            if (row.Status != RecoveryCaseStatus.WrittenOff)
            {
                _manager.ChangeStatus(row.Id, RecoveryCaseStatus.WrittenOff);
                await LoadCasesAsync();
            }
        }
        #endregion

        #region Update mode / Clear
        private void EnterUpdateMode(int id)
        {
            _caseIdToUpdate = id;
            btnRcAddCase.Visible = false;
            btnRcUpdateCase.Visible = true;
            btnRcCancelUpdate.Visible = true;
            btnRcClear.Visible = false;
        }

        private void ExitUpdateMode()
        {
            _caseIdToUpdate = null;
            btnRcAddCase.Visible = true;
            btnRcUpdateCase.Visible = false;
            btnRcCancelUpdate.Visible = false;
            btnRcClear.Visible = true;
            ClearForm();
        }

        private void ClearForm()
        {
            cbRcSymbol.SelectedIndex = -1;
            dtpEntryDate.Value = DateTime.Now;
            txtRcEntryPrice.Text = "";
            rbRcModeInvested.Checked = true;
            txtRcInvestedUSDT.Text = "";
            txtRcQuantity.Text = "";
            lblRcCurrentPrice.Text = "--";
            lblRcCurrentValue.Text = "--";
            lblRcUnrealized.Text = "--"; ;
            UpdateFormTitle();
        }
        #endregion

        #region Parse/Validate helpers
        private bool ValidateInputs(out string symbolRaw,
                                    out DateTime entryDate,
                                    out decimal entryPrice,
                                    out decimal? invested,
                                    out decimal? qty)
        {
            symbolRaw = "";
            entryDate = dtpEntryDate.Value.Date;
            entryPrice = 0m;
            invested = null;
            qty = null;

            if (string.IsNullOrWhiteSpace(cbRcSymbol.Text))
            {
                MessageBox.Show("Symbol is required.");
                return false;
            }
            symbolRaw = cbRcSymbol.Text.Replace("/USDT", "USDT", StringComparison.OrdinalIgnoreCase);

            if (!decimal.TryParse((txtRcEntryPrice.Text ?? "").Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out entryPrice) || entryPrice <= 0)
            {
                MessageBox.Show("Entry Price must be a positive number.");
                return false;
            }

            if (rbRcModeInvested.Checked)
            {
                var v = ParseDec(txtRcInvestedUSDT.Text);
                if (!v.HasValue || v.Value < 0)
                {
                    MessageBox.Show("Invested (USDT) must be a non-negative number.");
                    return false;
                }
                invested = v.Value;
                // store computed qty to keep single-column quantity logic consistent
                qty = v.Value / entryPrice;
            }
            else
            {
                var v = ParseDec(txtRcQuantity.Text);
                if (!v.HasValue || v.Value <= 0)
                {
                    MessageBox.Show("Quantity must be a positive number.");
                    return false;
                }
                qty = v.Value;
                invested = null;
            }

            return true;
        }

        private (bool ok, string symbolRaw) GetSelectedSymbolRaw()
        {
            if (string.IsNullOrWhiteSpace(cbRcSymbol.Text)) return (false, "");
            return (true, cbRcSymbol.Text.Replace("/USDT", "USDT", StringComparison.OrdinalIgnoreCase));
        }

        private decimal? ParseDec(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (decimal.TryParse(s.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out var d)) return d;
            return null;
        }
        #endregion

        #region Title + Responsive
        private void UpdateFormTitle()
        {
            var sym = string.IsNullOrWhiteSpace(cbRcSymbol.Text) ? "—" : cbRcSymbol.Text.Trim();
            var entry = string.IsNullOrWhiteSpace(txtRcEntryPrice.Text) ? "—" : txtRcEntryPrice.Text.Trim();
            this.Text = $"Recovery (DCA) • {sym} • Entry: {entry}";
        }

        private void InitializeResponsiveLayouts()
        {
            // ---------- Case Editor (TOP) ----------
            _layoutManager.RegisterControl(lblRcSymbol, pnlCaseEditor, pnlCaseEditor_Max, new Point(40, 30), new Size(90, 28));
            _layoutManager.RegisterControl(cbRcSymbol, pnlCaseEditor, pnlCaseEditor_Max, new Point(40, 58), new Size(280, 37));

            _layoutManager.RegisterControl(lblRcEntryDate, pnlCaseEditor, pnlCaseEditor_Max, new Point(382, 30), new Size(120, 28));
            _layoutManager.RegisterControl(dtpEntryDate, pnlCaseEditor, pnlCaseEditor_Max, new Point(382, 58), new Size(160, 32));

            _layoutManager.RegisterControl(lblRcEntryPrice, pnlCaseEditor, pnlCaseEditor_Max, new Point(720, 34), new Size(140, 28));
            _layoutManager.RegisterControl(txtRcEntryPrice, pnlCaseEditor, pnlCaseEditor_Max, new Point(720, 66), new Size(300, 34));

            _layoutManager.RegisterControl(grpRcAmountMode, pnlCaseEditor, pnlCaseEditor_Max, new Point(45, 124), new Size(295, 115));

            _layoutManager.RegisterControl(lblRcInvested, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 152), new Size(210, 30));
            _layoutManager.RegisterControl(txtRcInvestedUSDT, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 184), new Size(310, 36));
            _layoutManager.RegisterControl(lblRcQuantity, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 152), new Size(130, 30));
            _layoutManager.RegisterControl(txtRcQuantity, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 184), new Size(310, 36));

            _layoutManager.RegisterControl(btnRcAddCase, pnlCaseEditor, pnlCaseEditor_Max, new Point(860, 132), new Size(180, 44));
            _layoutManager.RegisterControl(btnRcUpdateCase, pnlCaseEditor, pnlCaseEditor_Max, new Point(860, 132), new Size(110, 44));
            _layoutManager.RegisterControl(btnRcCancelUpdate, pnlCaseEditor, pnlCaseEditor_Max, new Point(990, 132), new Size(110, 44));
            _layoutManager.RegisterControl(btnRcClear, pnlCaseEditor, pnlCaseEditor_Max, new Point(870, 188), new Size(160, 32));

            _layoutManager.RegisterControl(lblRcCurrentPriceCaption, pnlCaseEditor, pnlCaseEditor_Max, new Point(33, 262), new Size(150, 28));
            _layoutManager.RegisterControl(lblRcCurrentPrice, pnlCaseEditor, pnlCaseEditor_Max, new Point(33, 303), new Size(200, 28));
            _layoutManager.RegisterControl(lblRcCurrentValueCaption, pnlCaseEditor, pnlCaseEditor_Max, new Point(275, 262), new Size(150, 28));
            _layoutManager.RegisterControl(lblRcCurrentValue, pnlCaseEditor, pnlCaseEditor_Max, new Point(275, 303), new Size(200, 28));
            _layoutManager.RegisterControl(lblRcUnrealizedCaption, pnlCaseEditor, pnlCaseEditor_Max, new Point(517, 262), new Size(180, 28));
            _layoutManager.RegisterControl(lblRcUnrealized, pnlCaseEditor, pnlCaseEditor_Max, new Point(517, 303), new Size(200, 28));
            // ---------- Cases (BOTTOM) ----------
            _layoutManager.RegisterControl(lblRcHint, pnlCases, pnlCases_Max, new Point(28, 16), new Size(420, 28));
            _layoutManager.RegisterControl(cbRcStatusFilter, pnlCases, pnlCases_Max, new Point(545, 14), new Size(320, 37));

            _layoutManager.RegisterControl(btnRcPauseResumeCase, pnlCases, pnlCases_Max, new Point(900, 12), new Size(150, 40));
            _layoutManager.RegisterControl(btnRcCloseCase, pnlCases, pnlCases_Max, new Point(1135, 12), new Size(150, 40));
            _layoutManager.RegisterControl(btnRcWriteOff, pnlCases, pnlCases_Max, new Point(1370, 12), new Size(150, 40));

            _layoutManager.RegisterControl(dgvRecoveryCases, pnlCases, pnlCases_Max, new Point(28, 62), new Size(1600, 420));

            _layoutManager.RegisterStateAction(FormWindowStateExtended.Normal, () =>
            {
                ApplyNormalGridFonts();

                var small = new Font("Times New Roman", 11.5F, FontStyle.Regular);
                var edit = new Font("Times New Roman", 14.5F, FontStyle.Regular);
                var rb = new Font("Times New Roman", 12.5F, FontStyle.Regular);

                foreach (var l in pnlCaseEditor.Controls.OfType<Label>()) l.Font = small;
                lblRcHint.Font = small;

                dtpEntryDate.Font = new Font("Times New Roman", 12.5F);

                txtRcEntryPrice.Font = txtRcInvestedUSDT.Font = txtRcQuantity.Font = edit;
                foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity }) tb.Height = 28;

                rbRcModeInvested.Font = rbRcModeQuantity.Font = rb;

                foreach (var b in new[] { btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                                          btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff })
                    b.Font = new Font("Times New Roman", 12.5F);
            });

            _layoutManager.RegisterStateAction(FormWindowStateExtended.Maximized, () =>
            {
                ApplyMaxGridFonts();

                var big = new Font("Times New Roman", 14F, FontStyle.Regular);
                var bigEdit = new Font("Times New Roman", 16.5F, FontStyle.Regular);
                var bigRb = new Font("Times New Roman", 14.5F, FontStyle.Regular);

                foreach (var l in pnlCaseEditor.Controls.OfType<Label>()) l.Font = big;
                lblRcHint.Font = big;

                dtpEntryDate.Font = new Font("Times New Roman", 14.5F);

                txtRcEntryPrice.Font = txtRcInvestedUSDT.Font = txtRcQuantity.Font = bigEdit;
                foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity }) tb.Height = 36;

                rbRcModeInvested.Font = rbRcModeQuantity.Font = bigRb;

                foreach (var b in new[] { btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                                          btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff })
                    b.Font = new Font("Times New Roman", 15F);
            });
        }

        private void ApplyNormalGridFonts()
        {
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvRecoveryCases.ColumnHeadersHeight = 26;
            dgvRecoveryCases.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dgvRecoveryCases.RowTemplate.Height = 30;
        }

        private void ApplyMaxGridFonts()
        {
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvRecoveryCases.ColumnHeadersHeight = 32; // slightly smaller so long labels still fit
            dgvRecoveryCases.DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            dgvRecoveryCases.RowTemplate.Height = 34;
        }
        private void ApplyWindowStateLayout()
        {
            var host = this.FindForm();
            bool maximized = host != null && host.WindowState == FormWindowState.Maximized;


            foreach (DataGridViewColumn c in dgvRecoveryCases.Columns)
                if (_headerText.TryGetValue(c.Name, out var t))
                    c.HeaderText = maximized ? t.Full : t.Short;

            if (maximized) ApplyMaxGridFonts(); else ApplyNormalGridFonts();
        }

        public void SetWindowState(FormWindowStateExtended newState)
        {
            _layoutManager.SetWindowState(newState);
        }
        #endregion

        private void pnlCaseEditor_Paint(object sender, PaintEventArgs e) { }
        private void dgvRecoveryCases_SelectionChanged(object sender, EventArgs e) { }
        private async void cbRcStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => await LoadCasesAsync();
    }
}
