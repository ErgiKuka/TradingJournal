using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.Json;
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
    public partial class FrmRecoveryPlanner : Form, IResponsiveChildForm
    {
        private readonly BinanceApiService _binance = new BinanceApiService();
        private readonly RecoveryCaseManager _manager = new RecoveryCaseManager();
        private readonly BindingSource _bs = new BindingSource();
        private FrmAllocations? _allocationsWindow;

        private int? _caseIdToUpdate = null;
        private readonly System.Windows.Forms.Timer _priceTimer;

        // Responsive layout manager
        private readonly ResponsiveLayoutManager _layoutManager;

        public FrmRecoveryPlanner()
        {
            InitializeComponent();

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();   // register normal<>max mappings

            TryRoundUi();
            ConfigureInputsVisuals();        // makes textboxes taller + consistent
            CenterIconButtons();             // centers text without changing sizes

            // radio toggles
            rbRcModeInvested.CheckedChanged += AmountMode_CheckedChanged;
            rbRcModeQuantity.CheckedChanged += AmountMode_CheckedChanged;

            // recompute top KPIs + update title
            cbRcSymbol.SelectedIndexChanged += async (s, e) => { UpdateFormTitle(); await UpdateTopLiveAsync(); };
            cbRcCaseType.SelectedIndexChanged += async (s, e) => { UpdateFormTitle(); await UpdateTopLiveAsync(); };
            dateTimePicker1.ValueChanged += (s, e) => UpdateFormTitle();
            txtRcEntryPrice.TextChanged += async (s, e) => { UpdateFormTitle(); await UpdateTopLiveAsync(); };
            txtRcInvestedUSDT.TextChanged += async (s, e) => await UpdateTopLiveAsync();
            txtRcQuantity.TextChanged += async (s, e) => await UpdateTopLiveAsync();

            btnRcAddCase.Click += btnRcAddCase_Click;
            btnRcUpdateCase.Click += btnRcUpdateCase_Click;
            btnRcCancelUpdate.Click += (s, e) => ExitUpdateMode();
            btnRcClear.Click += (s, e) => ClearForm();

            dgvRecoveryCases.CellContentClick += dgvRecoveryCases_CellContentClick;
            dgvRecoveryCases.CellDoubleClick += dgvRecoveryCases_CellDoubleClick;
            dgvRecoveryCases.CellPainting += dgvRecoveryCases_CellPainting;
            dgvRecoveryCases.CellFormatting += Dgv_CellFormatting;

            btnRcPauseResumeCase.Click += btnRcPauseResumeCase_Click;
            btnRcCloseCase.Click += btnRcCloseCase_Click;
            btnRcWriteOff.Click += btnRcWriteOff_Click;

            // theme
            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();
            ApplyTheme();

            // timer for live price
            _priceTimer = new System.Windows.Forms.Timer { Interval = 60000 };
            _priceTimer.Tick += async (s, e) => await UpdateTopLiveAsync();
            _priceTimer.Start();

            // init lists
            InitializeCaseType();
            InitializeStatusFilter();
            _ = LoadSymbols();      // async
            _ = LoadCasesAsync();   // async

            ToggleAmountUi();
            SetupGrid();
            ApplyBaseGridStyling();
            ApplyNormalGridFonts(); // start in normal mode

            UpdateFormTitle();
        }

        private void TryRoundUi()
        {
            try
            {
                RoundedFormHelper.RoundPanel(pnlCaseEditor, 30);
                RoundedFormHelper.RoundPanel(pnlCases, 30);

                RoundedFormHelper.MakeButtonRounded(btnRcAddCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcUpdateCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcCancelUpdate, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcClear, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcPauseResumeCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcCloseCase, 24);
                RoundedFormHelper.MakeButtonRounded(btnRcWriteOff, 24);
            }
            catch { /* ignore if helper not present */ }
        }

        // Give textboxes a thicker look (single line look, but with height control)
        private void ConfigureInputsVisuals()
        {
            foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity })
            {
                tb.BorderStyle = BorderStyle.None;
                tb.Multiline = true;     // allows height control
                tb.AutoSize = false;     // don't auto-shrink to font height
                tb.Padding = new Padding(0);
                tb.Margin = new Padding(0);
            }
        }

        // Ensure IconButtons center their text and don’t truncate
        private void CenterIconButtons()
        {
            foreach (var b in new[]
            {
                btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff
            })
            {
                b.IconChar = IconChar.None;                       // no icon spacing
                b.AutoSize = false;                               // don’t shrink and clip
                b.UseCompatibleTextRendering = true;              // better centering
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

            // Panels
            pnlCaseEditor.BackColor = ThemeManager.PanelColor;
            pnlCases.BackColor = ThemeManager.PanelColor;
            pnlCaseEditor_Max.BackColor = ThemeManager.PanelColor;
            pnlCases_Max.BackColor = ThemeManager.PanelColor;

            // Labels in BOTH panels
            foreach (var lbl in pnlCaseEditor.Controls.OfType<Label>())
                lbl.ForeColor = ThemeManager.DataTextColor;
            foreach (var lbl in pnlCases.Controls.OfType<Label>())
                lbl.ForeColor = ThemeManager.DataTextColor;
            // (lblRcHint lives in pnlCases, so now it tracks theme like the rest)

            // GroupBox + radio buttons (make them readable on both themes)
            if (grpRcAmountMode != null)
            {
                grpRcAmountMode.BackColor = ThemeManager.PanelColor;
                grpRcAmountMode.ForeColor = ThemeManager.DataTextColor;
            }
            foreach (var rb in grpRcAmountMode.Controls.OfType<RadioButton>())
                rb.ForeColor = ThemeManager.TextColor;

            // Combos
            foreach (var cb in new[] { cbRcSymbol, cbRcCaseType, cbRcStatusFilter })
            {
                cb.BackColor = ThemeManager.PanelColor;
                cb.ForeColor = ThemeManager.TextColor;
                cb.FlatStyle = FlatStyle.Flat;
                cb.IntegralHeight = false;
                cb.DropDownHeight = 240;
            }

            // Inputs
            foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity })
            {
                tb.BackColor = ThemeManager.TextBoxColor;
                tb.ForeColor = ThemeManager.TextColor;
                tb.BorderStyle = BorderStyle.None;
            }

            // Top toolbar buttons
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

            // Grid colors
            dgvRecoveryCases.BackgroundColor = ThemeManager.PanelColor;
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.EnableHeadersVisualStyles = false;

            // Body + alternating rows (match Journal)
            dgvRecoveryCases.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            dgvRecoveryCases.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DataGrid;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
        }
        #endregion

        #region Init helpers
        private void InitializeCaseType()
        {
            cbRcCaseType.Items.Clear();
            cbRcCaseType.Items.Add(RecoveryCaseType.HeldBag);
            cbRcCaseType.Items.Add(RecoveryCaseType.RealizedLoss);
            cbRcCaseType.SelectedIndex = 0;
        }

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
                using var doc = JsonDocument.Parse(s);
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
            dgvRecoveryCases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var updateCol = new DataGridViewButtonColumn
            {
                Name = "UpdateColumn",
                HeaderText = "Update",
                Text = "Update",
                UseColumnTextForButtonValue = true,
                Width = 84
            };
            var deleteCol = new DataGridViewButtonColumn
            {
                Name = "DeleteColumn",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 84
            };
            dgvRecoveryCases.Columns.Add(updateCol);
            dgvRecoveryCases.Columns.Add(deleteCol);

            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", Name = "CaseId", Visible = false });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Symbol", HeaderText = "Symbol" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CaseType", HeaderText = "Type" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EntryDate", HeaderText = "Entry Date" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EntryPrice", HeaderText = "Entry Price" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvestedUSDT", HeaderText = "Invested (USDT)" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CurrentPrice", HeaderText = "Current Price" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CurrentValue", HeaderText = "Current Value" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RecoveredSoFar", HeaderText = "Recovered" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NeededToBreakEven", HeaderText = "Needed" });
            dgvRecoveryCases.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProgressPct", HeaderText = "Progress (%)" });

            dgvRecoveryCases.DataSource = _bs;
        }

        private void ApplyBaseGridStyling()
        {
            dgvRecoveryCases.BackgroundColor = ThemeManager.DataGrid;
            dgvRecoveryCases.BorderStyle = BorderStyle.None;
            dgvRecoveryCases.GridColor = Color.FromArgb(45, 51, 73);
            dgvRecoveryCases.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.DataGridHeader;
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            // Set in ApplyTheme too (kept here so Normal/Max swaps don't lose it)
            dgvRecoveryCases.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            dgvRecoveryCases.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DataGrid;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dgvRecoveryCases.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
        }

        private void ApplyNormalGridFonts()
        {
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvRecoveryCases.ColumnHeadersHeight = 36;
            dgvRecoveryCases.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dgvRecoveryCases.RowTemplate.Height = 30;
        }

        private void ApplyMaxGridFonts()
        {
            dgvRecoveryCases.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            dgvRecoveryCases.ColumnHeadersHeight = 42;
            dgvRecoveryCases.DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            dgvRecoveryCases.RowTemplate.Height = 36;
        }

        private void dgvRecoveryCases_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvRecoveryCases.Columns["DeleteColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var buttonBounds = e.CellBounds; buttonBounds.Inflate(-2, -2);
                using var br = new SolidBrush(Color.FromArgb(220, 53, 69));
                e.Graphics.FillRectangle(br, buttonBounds);
                TextRenderer.DrawText(e.Graphics, "Delete", e.CellStyle.Font, buttonBounds, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }

            if (e.ColumnIndex == dgvRecoveryCases.Columns["UpdateColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var buttonBounds = e.CellBounds; buttonBounds.Inflate(-2, -2);
                using var br = new SolidBrush(Color.FromArgb(0, 123, 255));
                e.Graphics.FillRectangle(br, buttonBounds);
                TextRenderer.DrawText(e.Graphics, "Update", e.CellStyle.Font, buttonBounds, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load cases: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRecoveryCases.Columns[e.ColumnIndex].DataPropertyName == "NeededToBreakEven" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out var needed))
                {
                    e.CellStyle.ForeColor = needed <= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
                }
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
                    lblRcNeeded.Text = "--";
                    return;
                }

                var currentPrice = await _binance.GetLastPriceAsync(symbolRaw);
                lblRcCurrentPrice.Text = currentPrice.ToString(currentPrice >= 1m ? "0.00####" : "0.########");

                var caseType = GetCaseType();
                var entryPrice = ParseDec(txtRcEntryPrice.Text);
                var invested = ParseDec(txtRcInvestedUSDT.Text);
                var qty = ParseDec(txtRcQuantity.Text);

                if (caseType == RecoveryCaseType.HeldBag)
                {
                    decimal useQty = qty ?? ((invested.HasValue && entryPrice.HasValue && entryPrice.Value > 0)
                        ? invested.Value / entryPrice.Value : 0m);

                    decimal currentValue = useQty * currentPrice;
                    lblRcCurrentValue.Text = currentValue.ToString("0.00");

                    decimal investedVal = invested ?? (useQty * (entryPrice ?? 0m));
                    decimal unreal = currentValue - investedVal;
                    lblRcUnrealized.Text = unreal.ToString("0.00");

                    decimal neededNow = Math.Max(0m, investedVal - currentValue);
                    lblRcNeeded.Text = neededNow.ToString("0.00");
                }
                else
                {
                    lblRcCurrentValue.Text = "-";
                    lblRcUnrealized.Text = "-";
                    var loss = invested ?? 0m;
                    lblRcNeeded.Text = loss.ToString("0.00");
                }
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
            if (!ValidateInputs(out var symbolRaw, out var caseType, out var entryDate, out var entryPrice, out var invested, out var qty))
                return;

            try
            {
                _manager.AddCase(symbolRaw, caseType, entryDate, entryPrice, invested, qty, notes: null);
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

            if (!ValidateInputs(out var symbolRaw, out var caseType, out var entryDate, out var entryPrice, out var invested, out var qty))
                return;

            try
            {
                _manager.UpdateCase(_caseIdToUpdate.Value, symbolRaw, caseType, entryDate, entryPrice, invested, qty, notes: null);
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
                cbRcCaseType.SelectedItem = row.CaseType;
                dateTimePicker1.Value = row.EntryDate;
                txtRcEntryPrice.Text = row.EntryPrice.ToString();

                if (row.Quantity.HasValue)
                {
                    rbRcModeQuantity.Checked = true;
                    txtRcQuantity.Text = row.Quantity.Value.ToString();
                    txtRcInvestedUSDT.Text = "";
                }
                else
                {
                    rbRcModeInvested.Checked = true;
                    txtRcInvestedUSDT.Text = row.InvestedUSDT?.ToString() ?? "";
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
            var newStatus = row.Status == RecoveryCaseStatus.Active ? RecoveryCaseStatus.Paused : RecoveryCaseStatus.Active;
            _manager.ChangeStatus(row.Id, newStatus);
            await LoadCasesAsync();
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
            _manager.ChangeStatus(row.Id, RecoveryCaseStatus.WrittenOff);
            await LoadCasesAsync();
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
            cbRcCaseType.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            txtRcEntryPrice.Text = "";
            rbRcModeInvested.Checked = true;
            txtRcInvestedUSDT.Text = "";
            txtRcQuantity.Text = "";
            lblRcCurrentPrice.Text = "--";
            lblRcCurrentValue.Text = "--";
            lblRcUnrealized.Text = "--";
            lblRcNeeded.Text = "--";
            UpdateFormTitle();
        }
        #endregion

        #region Parse/Validate helpers
        private bool ValidateInputs(out string symbolRaw,
                                    out RecoveryCaseType caseType,
                                    out DateTime entryDate,
                                    out decimal entryPrice,
                                    out decimal? invested,
                                    out decimal? qty)
        {
            symbolRaw = "";
            caseType = RecoveryCaseType.HeldBag;
            entryDate = dateTimePicker1.Value.Date;
            entryPrice = 0m;
            invested = null;
            qty = null;

            if (string.IsNullOrWhiteSpace(cbRcSymbol.Text))
            {
                MessageBox.Show("Symbol is required.");
                return false;
            }
            symbolRaw = cbRcSymbol.Text.Replace("/USDT", "USDT", StringComparison.OrdinalIgnoreCase);

            caseType = GetCaseType();

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
                qty = null;
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

        private RecoveryCaseType GetCaseType()
            => cbRcCaseType.SelectedItem is RecoveryCaseType ct ? ct : RecoveryCaseType.HeldBag;

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
            var type = cbRcCaseType.SelectedItem?.ToString() ?? "—";
            var entry = string.IsNullOrWhiteSpace(txtRcEntryPrice.Text) ? "—" : txtRcEntryPrice.Text.Trim();
            this.Text = $"Recovery Planner • {sym} • {type} • Entry: {entry}";
        }

        private void InitializeResponsiveLayouts()
        {
            // ---------- Case Editor (TOP) ----------
            _layoutManager.RegisterControl(lblRcSymbol, pnlCaseEditor, pnlCaseEditor_Max, new Point(40, 30), new Size(90, 28));
            _layoutManager.RegisterControl(cbRcSymbol, pnlCaseEditor, pnlCaseEditor_Max, new Point(40, 58), new Size(280, 37));

            _layoutManager.RegisterControl(lblRcCaseType, pnlCaseEditor, pnlCaseEditor_Max, new Point(346, 30), new Size(120, 28));
            _layoutManager.RegisterControl(cbRcCaseType, pnlCaseEditor, pnlCaseEditor_Max, new Point(346, 58), new Size(280, 37));

            _layoutManager.RegisterControl(lblRcEntryDate, pnlCaseEditor, pnlCaseEditor_Max, new Point(662, 30), new Size(120, 28));
            _layoutManager.RegisterControl(dateTimePicker1, pnlCaseEditor, pnlCaseEditor_Max, new Point(662, 58), new Size(160, 32));

            _layoutManager.RegisterControl(lblRcEntryPrice, pnlCaseEditor, pnlCaseEditor_Max, new Point(860, 34), new Size(140, 28));
            _layoutManager.RegisterControl(txtRcEntryPrice, pnlCaseEditor, pnlCaseEditor_Max, new Point(860, 66), new Size(300, 34));

            _layoutManager.RegisterControl(grpRcAmountMode, pnlCaseEditor, pnlCaseEditor_Max, new Point(45, 124), new Size(295, 115));

            // Shift Invested/Quantity further right and keep them taller
            _layoutManager.RegisterControl(lblRcInvested, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 152), new Size(210, 30));
            _layoutManager.RegisterControl(txtRcInvestedUSDT, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 184), new Size(310, 36));
            _layoutManager.RegisterControl(lblRcQuantity, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 152), new Size(130, 30));
            _layoutManager.RegisterControl(txtRcQuantity, pnlCaseEditor, pnlCaseEditor_Max, new Point(386, 184), new Size(310, 36));

            // Buttons (your positions/sizes preserved)
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
            _layoutManager.RegisterControl(lblRcNeededCaption, pnlCaseEditor, pnlCaseEditor_Max, new Point(753, 262), new Size(260, 28));
            _layoutManager.RegisterControl(lblRcNeeded, pnlCaseEditor, pnlCaseEditor_Max, new Point(753, 303), new Size(200, 28));

            // ---------- Cases (BOTTOM) ----------
            _layoutManager.RegisterControl(lblRcHint, pnlCases, pnlCases_Max, new Point(28, 16), new Size(420, 28));
            _layoutManager.RegisterControl(cbRcStatusFilter, pnlCases, pnlCases_Max, new Point(545, 14), new Size(320, 37));

            _layoutManager.RegisterControl(btnRcPauseResumeCase, pnlCases, pnlCases_Max, new Point(900, 12), new Size(150, 40));
            _layoutManager.RegisterControl(btnRcCloseCase, pnlCases, pnlCases_Max, new Point(1135, 12), new Size(150, 40));
            _layoutManager.RegisterControl(btnRcWriteOff, pnlCases, pnlCases_Max, new Point(1370, 12), new Size(150, 40));

            _layoutManager.RegisterControl(dgvRecoveryCases, pnlCases, pnlCases_Max, new Point(28, 62), new Size(1600, 420));

            // ----- Font scaling actions -----
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Normal, () =>
            {
                ApplyNormalGridFonts();

                var small = new Font("Times New Roman", 11.5F, FontStyle.Regular);
                var edit = new Font("Times New Roman", 14.5F, FontStyle.Regular);
                var rb = new Font("Times New Roman", 12.5F, FontStyle.Regular);

                foreach (var l in pnlCaseEditor.Controls.OfType<Label>()) l.Font = small;
                lblRcHint.Font = small;

                cbRcSymbol.Font = cbRcCaseType.Font = cbRcStatusFilter.Font = new Font("Times New Roman", 13.8F);
                dateTimePicker1.Font = new Font("Times New Roman", 12.5F);

                txtRcEntryPrice.Font = txtRcInvestedUSDT.Font = txtRcQuantity.Font = edit;
                foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity }) tb.Height = 28;

                rbRcModeInvested.Font = rbRcModeQuantity.Font = rb;

                foreach (var b in new[] { btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                                          btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff })
                    b.Font = new Font("Times New Roman", 12.5F);

                foreach (var b in new[] { btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                                          btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff })
                    b.TextAlign = ContentAlignment.MiddleCenter;
            });

            _layoutManager.RegisterStateAction(FormWindowStateExtended.Maximized, () =>
            {
                ApplyMaxGridFonts();

                var big = new Font("Times New Roman", 14F, FontStyle.Regular);
                var bigEdit = new Font("Times New Roman", 16.5F, FontStyle.Regular);
                var bigRb = new Font("Times New Roman", 14.5F, FontStyle.Regular);

                foreach (var l in pnlCaseEditor.Controls.OfType<Label>()) l.Font = big;
                lblRcHint.Font = big;

                cbRcSymbol.Font = cbRcCaseType.Font = cbRcStatusFilter.Font = new Font("Times New Roman", 15.5F);
                dateTimePicker1.Font = new Font("Times New Roman", 14.5F);

                txtRcEntryPrice.Font = txtRcInvestedUSDT.Font = txtRcQuantity.Font = bigEdit;
                foreach (var tb in new[] { txtRcEntryPrice, txtRcInvestedUSDT, txtRcQuantity }) tb.Height = 36;

                rbRcModeInvested.Font = rbRcModeQuantity.Font = bigRb;

                foreach (var b in new[] { btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                                          btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff })
                    b.Font = new Font("Times New Roman", 15F);

                foreach (var b in new[] { btnRcAddCase, btnRcUpdateCase, btnRcCancelUpdate, btnRcClear,
                                          btnRcPauseResumeCase, btnRcCloseCase, btnRcWriteOff })
                    b.TextAlign = ContentAlignment.MiddleCenter;
            });
        }

        // IResponsiveChildForm implementation (used by parent)
        public void SetWindowState(FormWindowStateExtended newState)
        {
            _layoutManager.SetWindowState(newState);
        }
        #endregion

        // Empty designer handlers you had:
        private void pnlCaseEditor_Paint(object sender, PaintEventArgs e) { }
        private void dgvRecoveryCases_SelectionChanged(object sender, EventArgs e) { }
        private async void cbRcStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => await LoadCasesAsync();
    }
}
