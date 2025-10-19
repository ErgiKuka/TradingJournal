using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Services;
using TradingJournal.Core.Managers;

namespace TradingJournal.Pl.PlaceHolder.Recovery_Planner
{
    public partial class FrmAllocations : Form
    {
        private readonly int _caseId;
        private readonly BinanceApiService _binance = new BinanceApiService();
        private readonly RecoveryAllocationManager _alcMgr = new RecoveryAllocationManager();
        private readonly RecoveryCaseManager _caseMgr = new RecoveryCaseManager();
        private readonly BindingSource _bs = new BindingSource();
        private int? _updateId = null;
        private readonly System.Windows.Forms.Timer _priceTimer;

        private RecoveryCase? _case; // loaded for KPIs

        // guards to prevent accidental double-handling (e.g., duplicate event wiring)
        private bool _inAddHandler = false;
        private bool _inUpdateHandler = false;

        // ----- constructor receives the Case Id -----
        public FrmAllocations(int recoveryCaseId)
        {
            _caseId = recoveryCaseId;
            InitializeComponent();

            // Round panels & buttons to match Journal
            TryRoundUi();

            // theme + UI
            ApplyTheme();
            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();

            // grid
            SetupGrid();
            ApplyBaseGridStyling();
            ApplyReadableGridFonts();
            dataGridView1.CellContentClick += DataGrid_CellContentClick;
            dataGridView1.CellPainting += DataGrid_CellPainting;

            // amount mode toggles
            rbAlcModeMargin.CheckedChanged += (_, __) => ToggleAmountInputs();
            rbAlcModeQuantity.CheckedChanged += (_, __) => ToggleAmountInputs();
            ToggleAmountInputs();

            // buttons
            btnAlcAdd.Click += btnAlcAdd_Click;   // single, guarded
            btnAlcUpdate.Click += btnAlcUpdate_Click;
            btnAlcCancelUpdate.Click += (_, __) => ExitUpdateMode();
            btnAlcClear.Click += (_, __) => ClearInputs();

            // periodic price (for KPIs display)
            _priceTimer = new System.Windows.Forms.Timer { Interval = 60_000 };
            _priceTimer.Tick += async (_, __) => await RefreshKpisAsync();
            _priceTimer.Start();

            // initial load
            _ = LoadCaseAndAllocationsAsync();
        }

        private void TryRoundUi()
        {
            try
            {
                RoundedFormHelper.RoundPanel(pnlAlcEditor, 30);
                RoundedFormHelper.RoundPanel(pnlAlcGrid, 30);
                RoundedFormHelper.RoundPanel(pnlAlcKpis, 30);

                RoundedFormHelper.MakeButtonRounded(btnAlcAdd, 24);
                RoundedFormHelper.MakeButtonRounded(btnAlcUpdate, 24);
                RoundedFormHelper.MakeButtonRounded(btnAlcCancelUpdate, 24);
                RoundedFormHelper.MakeButtonRounded(btnAlcClear, 24);
            }
            catch { /* ignore */ }
        }

        private static decimal ComputeProfitOrZero(decimal entry, decimal? exit, decimal? margin, decimal? qty)
        {
            if (!exit.HasValue) return 0m;

            decimal effectiveQty = qty ?? ((margin.HasValue && entry > 0m) ? (margin.Value / entry) : 0m);
            if (effectiveQty <= 0m) return 0m;

            var p = (exit.Value - entry) * effectiveQty;
            return p < 0m ? 0m : p; // clamp negatives to 0 for “allocated”
        }

        #region Theme
        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            foreach (var p in new[] { pnlAlcEditor, pnlAlcGrid, pnlAlcKpis })
                p.BackColor = ThemeManager.PanelColor;

            // Labels (all containers)
            foreach (var lbl in pnlAlcEditor.Controls.OfType<Label>()
                         .Concat(pnlAlcGrid.Controls.OfType<Label>())
                         .Concat(pnlAlcKpis.Controls.OfType<Label>()))
                lbl.ForeColor = ThemeManager.DataTextColor;

            // Inputs
            foreach (var tb in new[] { txtAlcEntryPrice, txtAlcExitPrice, txtAlcMargin, txtAlcQuantity, txtAlcProfit })
            {
                tb.BackColor = ThemeManager.TextBoxColor;
                tb.ForeColor = ThemeManager.TextColor;
                tb.BorderStyle = BorderStyle.None;
            }

            // GroupBoxes + radios (apply generically so we don't depend on designer names)
            foreach (var grp in pnlAlcEditor.Controls.OfType<GroupBox>())
            {
                grp.BackColor = ThemeManager.PanelColor;
                grp.ForeColor = ThemeManager.DataTextColor;
                foreach (var rb in grp.Controls.OfType<RadioButton>())
                    rb.ForeColor = ThemeManager.TextColor;
            }

            foreach (var b in new[] { btnAlcAdd, btnAlcUpdate, btnAlcCancelUpdate, btnAlcClear })
                b.ForeColor = ThemeManager.ActionButtonTextColor;

            // progress bar
            prgKpiProgress.Style = ProgressBarStyle.Continuous;
            prgKpiProgress.Minimum = 0;
            prgKpiProgress.Maximum = 100;

            // DataGrid: match Journal alternation/selection
            ApplyBaseGridStyling();
        }
        #endregion

        #region Grid
        private void SetupGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

            dataGridView1.Columns.Clear();

            var updateCol = new DataGridViewButtonColumn
            {
                Name = "UpdateColumn",
                HeaderText = "Update",
                Text = "Update",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            var deleteCol = new DataGridViewButtonColumn
            {
                Name = "DeleteColumn",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 70
            };

            dataGridView1.Columns.Add(updateCol);
            dataGridView1.Columns.Add(deleteCol);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", Name = "AllocationId", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TradeDate", HeaderText = "Date" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EntryPrice", HeaderText = "Entry Price" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ExitPrice", HeaderText = "Exit Price" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MarginUSDT", HeaderText = "Margin (USDT)" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty (Base)" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AllocatedUSDT", HeaderText = "Profit Alloc (USDT)" });

            dataGridView1.DataSource = _bs;
        }

        private void ApplyBaseGridStyling()
        {
            // Colors consistent with Journal
            dataGridView1.BackgroundColor = ThemeManager.PanelColor;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.GridColor = Color.FromArgb(45, 51, 73);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Headers
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dataGridView1.EnableHeadersVisualStyles = false;

            // Prevent header highlighting looking odd
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.DataGridHeader;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            // Rows (primary) + selection
            dataGridView1.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            dataGridView1.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            // Alternating row colors + selection (match Journal)
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DataGrid;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
        }

        private void ApplyReadableGridFonts()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dataGridView1.RowTemplate.Height = 30;
        }

        private void DataGrid_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // DELETE button style (match Journal)
            if (e.ColumnIndex == dataGridView1.Columns["DeleteColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var buttonBounds = e.CellBounds;
                buttonBounds.Inflate(-2, -2);
                var buttonColor = Color.FromArgb(220, 53, 69); // red

                ControlPaint.DrawButton(e.Graphics, buttonBounds, ButtonState.Normal);
                using var br = new SolidBrush(buttonColor);
                e.Graphics.FillRectangle(br, buttonBounds);

                TextRenderer.DrawText(e.Graphics, "Delete", e.CellStyle.Font, buttonBounds, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                e.Handled = true;
            }

            // UPDATE button style (match Journal)
            if (e.ColumnIndex == dataGridView1.Columns["UpdateColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var buttonBounds = e.CellBounds;
                buttonBounds.Inflate(-2, -2);
                var buttonColor = Color.FromArgb(0, 123, 255); // blue

                ControlPaint.DrawButton(e.Graphics, buttonBounds, ButtonState.Normal);
                using var br = new SolidBrush(buttonColor);
                e.Graphics.FillRectangle(br, buttonBounds);

                TextRenderer.DrawText(e.Graphics, "Update", e.CellStyle.Font, buttonBounds, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                e.Handled = true;
            }
        }
        #endregion

        #region Load + KPIs
        private async Task LoadCaseAndAllocationsAsync()
        {
            _case = await _alcMgr.GetCaseAsync(_caseId);
            if (_case == null)
            {
                MessageBox.Show("Recovery case not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            UpdateWindowTitle();

            // header values
            lblKpiSymbol.Text = _case.Symbol;
            lblKpiEntryPrice.Text = _case.EntryPrice.ToString("0.########");

            // grid
            var rows = await _alcMgr.GetRowsAsync(_caseId);
            _bs.DataSource = rows;

            await RefreshKpisAsync();
        }

        private void UpdateWindowTitle()
        {
            if (_case == null) return;

            // e.g., "Allocations – SOL/USDT • Entry: 10/18/2025 • Status: Active"
            var nice = _case.Symbol?.EndsWith("USDT", StringComparison.OrdinalIgnoreCase) == true
                ? _case.Symbol.Replace("USDT", "/USDT", StringComparison.OrdinalIgnoreCase)
                : _case.Symbol;

            var date = _case.EntryDate.ToString("M/d/yyyy");
            var status = _case.Status.ToString();

            this.Text = $"Allocations – {nice} • Entry: {date} • Status: {status}";
        }

        private async Task RefreshKpisAsync()
        {
            if (_case == null) return;

            // live price
            decimal currentPrice = 0m;
            try { currentPrice = await _binance.GetLastPriceAsync(_case.Symbol); }
            catch { /* network hiccup: leave zero */ }

            lblKpiCurrentPrice.Text = currentPrice > 0 ? currentPrice.ToString(currentPrice >= 1m ? "0.00####" : "0.########") : "--";

            // compute derived values
            decimal qty = _case.Quantity ?? (_case.InvestedUSDT.HasValue && _case.EntryPrice > 0m
                            ? _case.InvestedUSDT.Value / _case.EntryPrice : 0m);
            lblKpiQuantity.Text = qty > 0 ? qty.ToString("0.########") : "--";

            decimal invested = _case.InvestedUSDT ?? (qty * _case.EntryPrice);
            lblKpiInvested.Text = invested > 0 ? invested.ToString("0.00") : "--";

            decimal avgCost = qty > 0 ? invested / qty : 0m;
            lblKpiAvgCost.Text = qty > 0 ? avgCost.ToString("0.########") : "--";

            decimal currentValue = qty * currentPrice;
            lblKpiCurrentValue.Text = currentPrice > 0 ? currentValue.ToString("0.00") : "--";

            // recovered so far
            decimal recovered;
            using (var db = new TradingJournal.Core.Data.AppDbContext())
                recovered = await RecoveryAllocationManager.GetRecoveredSoFarAsync(db, _caseId);
            lblKpiRecovered.Text = recovered.ToString("0.00");

            // needed to break even
            decimal lossNow = Math.Max(0m, invested - currentValue);
            decimal needed = Math.Max(0m, lossNow - recovered);
            lblKpiNeeded.Text = needed.ToString("0.00");
            lblKpiNeeded.ForeColor = needed <= 0m ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);

            // progress — 100% at break-even
            int progress;
            var denom = recovered + needed;
            if (denom <= 0m) progress = 0;
            else progress = (int)Math.Round((double)(recovered / denom) * 100.0, MidpointRounding.AwayFromZero);
            progress = Math.Max(0, Math.Min(100, progress));
            prgKpiProgress.Value = progress;
            lblKpiProgressPct.Text = $"{progress}%";

            // Auto-close the case when recovered enough
            if (needed <= 0m && _case.Status == RecoveryCaseStatus.Active)
            {
                try
                {
                    _caseMgr.ChangeStatus(_case.Id, RecoveryCaseStatus.Closed);
                    _case.Status = RecoveryCaseStatus.Closed;
                    UpdateWindowTitle(); // reflect status change in title
                }
                catch
                {
                    // ignore — non-critical UX enhancement
                }
            }
        }
        #endregion

        #region Add / Update / Delete
        private void ToggleAmountInputs()
        {
            bool marginMode = rbAlcModeMargin.Checked;

            lblAlcMargin.Visible = marginMode;
            txtAlcMargin.Visible = marginMode;

            lblAlcQuantity.Visible = !marginMode;
            txtAlcQuantity.Visible = !marginMode;

            if (marginMode)
                txtAlcQuantity.Text = string.Empty;
            else
                txtAlcMargin.Text = string.Empty;
        }

        private void ClearInputs()
        {
            dtpAlcTradeDate.Value = DateTime.Now;
            txtAlcEntryPrice.Text = "";
            txtAlcExitPrice.Text = "";
            txtAlcMargin.Text = "";
            txtAlcQuantity.Text = "";
            txtAlcProfit.Text = "";
            rbAlcModeMargin.Checked = true;

            _updateId = null;
            btnAlcAdd.Visible = true;
            btnAlcUpdate.Visible = false;
            btnAlcCancelUpdate.Visible = false;
            btnAlcClear.Visible = true;
        }

        private static bool TryParseDec(string s, out decimal d)
            => decimal.TryParse((s ?? "").Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out d);

        private async void btnAlcAdd_Click(object sender, EventArgs e)
        {
            if (_inAddHandler) return; // prevent duplicate execution if the click is wired twice
            _inAddHandler = true;
            try
            {
                // 1) Entry price
                if (!TryParseDec(txtAlcEntryPrice.Text, out var entry) || entry <= 0m)
                {
                    MessageBox.Show("Entry Price must be a positive number.");
                    return;
                }

                // 2) Exit price (optional unless we need to auto-calc profit)
                decimal? exit = null;
                if (!string.IsNullOrWhiteSpace(txtAlcExitPrice.Text))
                {
                    if (!TryParseDec(txtAlcExitPrice.Text, out var epx) || epx <= 0m)
                    {
                        MessageBox.Show("Exit Price must be positive.");
                        return;
                    }
                    exit = epx;
                }

                // 3) Amount mode -> parse margin/qty (optional if Profit is typed)
                decimal? margin = null;
                decimal? qty = null;
                if (rbAlcModeMargin.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(txtAlcMargin.Text))
                    {
                        if (!TryParseDec(txtAlcMargin.Text, out var m) || m < 0m)
                        {
                            MessageBox.Show("Margin (USDT) must be non-negative.");
                            return;
                        }
                        margin = m;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtAlcQuantity.Text))
                    {
                        if (!TryParseDec(txtAlcQuantity.Text, out var q) || q <= 0m)
                        {
                            MessageBox.Show("Quantity must be positive.");
                            return;
                        }
                        qty = q;
                    }
                }

                // 4) Profit: optional. If blank, we must be able to compute it.
                decimal alloc;
                if (string.IsNullOrWhiteSpace(txtAlcProfit.Text))
                {
                    if (!exit.HasValue)
                    {
                        MessageBox.Show("Exit Price is required when Profit is empty (to compute profit).");
                        return;
                    }
                    if (!(qty.HasValue || margin.HasValue))
                    {
                        MessageBox.Show("Enter Quantity or Margin (matching the selected mode) when Profit is empty.");
                        return;
                    }
                    alloc = ComputeProfitOrZero(entry, exit, margin, qty);
                }
                else
                {
                    if (!TryParseDec(txtAlcProfit.Text, out alloc) || alloc < 0m)
                    {
                        MessageBox.Show("Profit (USDT) must be a non-negative number.");
                        return;
                    }
                }

                try
                {
                    _alcMgr.Add(_caseId, dtpAlcTradeDate.Value.Date, entry, exit, margin, qty, alloc);
                    await ReloadAfterChangeAsync("Allocation added!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Add failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _inAddHandler = false;
            }
        }

        private async void btnAlcUpdate_Click(object sender, EventArgs e)
        {
            if (_inUpdateHandler) return;
            _inUpdateHandler = true;
            try
            {
                if (_updateId == null) return;

                if (!TryParseDec(txtAlcEntryPrice.Text, out var entry) || entry <= 0m)
                {
                    MessageBox.Show("Entry Price must be a positive number.");
                    return;
                }

                decimal? exit = null;
                if (!string.IsNullOrWhiteSpace(txtAlcExitPrice.Text))
                {
                    if (!TryParseDec(txtAlcExitPrice.Text, out var epx) || epx <= 0m)
                    {
                        MessageBox.Show("Exit Price must be positive.");
                        return;
                    }
                    exit = epx;
                }

                decimal? margin = null;
                decimal? qty = null;
                if (rbAlcModeMargin.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(txtAlcMargin.Text))
                    {
                        if (!TryParseDec(txtAlcMargin.Text, out var m) || m < 0m)
                        {
                            MessageBox.Show("Margin (USDT) must be non-negative.");
                            return;
                        }
                        margin = m;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtAlcQuantity.Text))
                    {
                        if (!TryParseDec(txtAlcQuantity.Text, out var q) || q <= 0m)
                        {
                            MessageBox.Show("Quantity must be positive.");
                            return;
                        }
                        qty = q;
                    }
                }

                decimal alloc;
                if (string.IsNullOrWhiteSpace(txtAlcProfit.Text))
                {
                    if (!exit.HasValue)
                    {
                        MessageBox.Show("Exit Price is required when Profit is empty (to compute profit).");
                        return;
                    }
                    if (!(qty.HasValue || margin.HasValue))
                    {
                        MessageBox.Show("Enter Quantity or Margin (matching the selected mode) when Profit is empty.");
                        return;
                    }
                    alloc = ComputeProfitOrZero(entry, exit, margin, qty);
                }
                else
                {
                    if (!TryParseDec(txtAlcProfit.Text, out alloc) || alloc < 0m)
                    {
                        MessageBox.Show("Profit (USDT) must be a non-negative number.");
                        return;
                    }
                }

                try
                {
                    _alcMgr.Update(_updateId.Value, dtpAlcTradeDate.Value.Date, entry, exit, margin, qty, alloc);
                    await ReloadAfterChangeAsync("Allocation updated!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _inUpdateHandler = false;
            }
        }

        private async Task ReloadAfterChangeAsync(string okMessage)
        {
            ClearInputs();
            var rows = await _alcMgr.GetRowsAsync(_caseId);
            _bs.DataSource = rows;
            await RefreshKpisAsync();
            MessageBox.Show(okMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void DataGrid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = (AllocationRow)_bs[e.RowIndex];

            if (dataGridView1.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                if (MessageBox.Show("Delete this allocation?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _alcMgr.Delete(row.Id);
                        await ReloadAfterChangeAsync("Allocation deleted!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Delete failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "UpdateColumn")
            {
                // load row into editor
                dtpAlcTradeDate.Value = row.TradeDate;
                txtAlcEntryPrice.Text = row.EntryPrice.ToString();
                txtAlcExitPrice.Text = row.ExitPrice?.ToString() ?? "";

                if (row.Quantity.HasValue)
                {
                    rbAlcModeQuantity.Checked = true;
                    txtAlcQuantity.Text = row.Quantity.Value.ToString();
                    txtAlcMargin.Text = "";
                }
                else
                {
                    rbAlcModeMargin.Checked = true;
                    txtAlcMargin.Text = row.MarginUSDT?.ToString() ?? "";
                    txtAlcQuantity.Text = "";
                }

                txtAlcProfit.Text = row.AllocatedUSDT.ToString();

                EnterUpdateMode(row.Id);
            }
        }

        private void EnterUpdateMode(int id)
        {
            _updateId = id;
            btnAlcAdd.Visible = false;
            btnAlcUpdate.Visible = true;
            btnAlcCancelUpdate.Visible = true;
            btnAlcClear.Visible = false;
        }

        private void ExitUpdateMode()
        {
            _updateId = null;
            btnAlcAdd.Visible = true;
            btnAlcUpdate.Visible = false;
            btnAlcCancelUpdate.Visible = false;
            btnAlcClear.Visible = true;
            ClearInputs();
        }
        #endregion
    }
}
