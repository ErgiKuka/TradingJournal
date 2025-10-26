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
        private RecoveryCase? _case;

        // Notify parent (optional) when status flips (so parent can refresh its grid)
        public event Action<int, RecoveryCaseStatus>? CaseStatusChanged;

        // header text variants (Normal vs Maximized)
        private readonly Dictionary<string, (string Short, string Full)> _headerText = new()
        {
            { "EntryPrice", ("Entry", "Entry Price") },
            { "InvestedUSDT", ("Inv...", "Invested (USDT)") },
            { "Quantity", ("Qty", "Qty (Base)") },
            { "RunningTotalQty", ("Run Qty", "Run Total Qty") },
            { "RunningTotalInvested", ("Run Inv", "Run Total Invested") },
            { "RunningAvgEntry", ("Run Avg", "Run Avg Entry") }
        };

        public FrmAllocations(int recoveryCaseId)
        {
            _caseId = recoveryCaseId;
            InitializeComponent();

            TryRoundUi();

            ThemeManager.ThemeChanged += async (s, e) =>
            {
                ApplyTheme();
                await RefreshKpisAsync();
            };
            ApplyTheme();

            SetupGrid();
            ApplyBaseGridStyling();
            ApplyReadableGridFonts();

            dataGridView1.CellContentClick += DataGrid_CellContentClick;
            dataGridView1.CellPainting += DataGrid_CellPainting;
            dataGridView1.CellFormatting += DataGrid_CellFormatting;

            // Only Invested/Quantity modes; no Exit/Profit columns
            rbAlcModeMargin.CheckedChanged += (_, __) => ToggleAmountInputs();
            rbAlcModeQuantity.CheckedChanged += (_, __) => ToggleAmountInputs();
            ToggleAmountInputs();

            btnAlcUpdate.Click += btnAlcUpdate_Click;
            btnAlcCancelUpdate.Click += (_, __) => ExitUpdateMode();
            btnAlcClear.Click += (_, __) => ClearInputs();

            // Keep your dynamic maximize behavior
            this.SizeChanged += (_, __) => ApplyWindowStateLayout();

            _priceTimer = new System.Windows.Forms.Timer { Interval = 60_000 };
            _priceTimer.Tick += async (_, __) => await RefreshKpisAsync();
            _priceTimer.Start();

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
            catch { }
        }

        private static decimal ComputeQuantity(decimal entry, decimal? invested, decimal? qty)
        {
            if (qty.HasValue) return qty.Value;
            if (invested.HasValue && entry > 0m) return invested.Value / entry;
            return 0m;
        }

        #region Theme
        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            foreach (var p in new[] { pnlAlcEditor, pnlAlcGrid, pnlAlcKpis })
                p.BackColor = ThemeManager.PanelColor;

            foreach (var lbl in pnlAlcEditor.Controls.OfType<Label>()
                         .Concat(pnlAlcGrid.Controls.OfType<Label>())
                         .Concat(pnlAlcKpis.Controls.OfType<Label>()))
                lbl.ForeColor = ThemeManager.DataTextColor;

            foreach (var tb in new[] { txtAlcEntryPrice, txtAlcMargin, txtAlcQuantity })
            {
                tb.BackColor = ThemeManager.TextBoxColor;
                tb.ForeColor = ThemeManager.TextColor;
                tb.BorderStyle = BorderStyle.None;
            }

            prgKpiProgress.Style = ProgressBarStyle.Continuous;
            prgKpiProgress.Minimum = 0;
            prgKpiProgress.Maximum = 100;

            ApplyBaseGridStyling();
            ApplyWindowStateLayout();   // respect current state immediately
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

            var updateCol = new DataGridViewButtonColumn { Name = "UpdateColumn", HeaderText = "Update", Text = "Update", UseColumnTextForButtonValue = true, Width = 70 };
            var deleteCol = new DataGridViewButtonColumn { Name = "DeleteColumn", HeaderText = "Delete", Text = "Delete", UseColumnTextForButtonValue = true, Width = 70 };

            dataGridView1.Columns.Add(updateCol);
            dataGridView1.Columns.Add(deleteCol);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", Name = "AllocationId", Visible = false });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TradeDate", HeaderText = "Date" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EntryPrice", Name = "EntryPrice", HeaderText = "Entry Price" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvestedUSDT", Name = "InvestedUSDT", HeaderText = "Invested (USDT)" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", Name = "Quantity", HeaderText = "Qty (Base)" });

            // running totals/avg come from your row-projection
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RunningTotalQty", Name = "RunningTotalQty", HeaderText = "Run Total Qty" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RunningTotalInvested", Name = "RunningTotalInvested", HeaderText = "Run Total Invested" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RunningAvgEntry", Name = "RunningAvgEntry", HeaderText = "Run Avg Entry" });

            dataGridView1.DataSource = _bs;
        }

        private void ApplyBaseGridStyling()
        {
            dataGridView1.BackgroundColor = ThemeManager.PanelColor;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.GridColor = Color.FromArgb(45, 51, 73);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeManager.DataGridHeader;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dataGridView1.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            dataGridView1.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DataGrid;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            grpAlcAmountMode.ForeColor = ThemeManager.TextColor;

            lblKpiInvested.ForeColor = ThemeManager.WarningColor;
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

            if (e.ColumnIndex == dataGridView1.Columns["DeleteColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var buttonBounds = e.CellBounds; buttonBounds.Inflate(-2, -2);
                var buttonColor = Color.FromArgb(220, 53, 69);
                using var br = new SolidBrush(buttonColor);
                e.Graphics.FillRectangle(br, buttonBounds);
                TextRenderer.DrawText(e.Graphics, "Delete", e.CellStyle.Font, buttonBounds, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }

            if (e.ColumnIndex == dataGridView1.Columns["UpdateColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var buttonBounds = e.CellBounds; buttonBounds.Inflate(-2, -2);
                var buttonColor = Color.FromArgb(0, 123, 255);
                using var br = new SolidBrush(buttonColor);
                e.Graphics.FillRectangle(br, buttonBounds);
                TextRenderer.DrawText(e.Graphics, "Update", e.CellStyle.Font, buttonBounds, Color.White,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }
        }

        // Format numbers: money -> 2dp, prices/qty -> up to 3dp (or more if <1)
        private void DataGrid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value is null) return;
            var col = dataGridView1.Columns[e.ColumnIndex];

            bool IsMoneyCol = col.Name is "InvestedUSDT" or "RunningTotalInvested";
            bool IsPriceCol = col.Name is "EntryPrice" or "RunningAvgEntry";
            bool IsQtyCol = col.Name is "Quantity" or "RunningTotalQty";

            if (IsMoneyCol && decimal.TryParse(e.Value.ToString(), out var money))
                e.Value = money.ToString("0.00", CultureInfo.CurrentCulture);
            else if ((IsPriceCol || IsQtyCol) && decimal.TryParse(e.Value.ToString(), out var num))
                e.Value = num >= 1m ? num.ToString("0.###", CultureInfo.CurrentCulture)
                                    : num.ToString("0.########", CultureInfo.CurrentCulture);
        }
        #endregion

        #region Window state layout (restore your normal/max behavior)
        private void ApplyWindowStateLayout()
        {
            bool maximized = this.WindowState == FormWindowState.Maximized;

            // header height
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", maximized ? 12 : 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = maximized ? 36 : 26;

            // header captions
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                if (_headerText.TryGetValue(c.Name, out var t))
                    c.HeaderText = maximized ? t.Full : t.Short;
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

            lblKpiSymbol.Text = _case.Symbol;
            lblKpiEntryPrice.Text = _case.EntryPrice.ToString(_case.EntryPrice >= 1m ? "0.###" : "0.########");

            var rows = await _alcMgr.GetRowsAsync(_caseId);
            _bs.DataSource = rows;

            await RefreshKpisAsync();
        }

        private void UpdateWindowTitle()
        {
            if (_case == null) return;

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

            decimal currentPrice = 0m;
            try { currentPrice = await _binance.GetLastPriceAsync(_case.Symbol); }
            catch { }

            lblKpiCurrentPrice.Text = currentPrice > 0 ? (currentPrice >= 1m ? currentPrice.ToString("0.###") : currentPrice.ToString("0.########")) : "--";

            // totals = initial + allocations
            var rows = (_bs.List as IEnumerable<AllocationRow>) ?? Enumerable.Empty<AllocationRow>();

            decimal initQty = _case.Quantity ?? (_case.InvestedUSDT.HasValue && _case.EntryPrice > 0m
                            ? _case.InvestedUSDT.Value / _case.EntryPrice : 0m);
            decimal initInv = _case.InvestedUSDT ?? (initQty * _case.EntryPrice);

            // Use the single Quantity column; if a row is in USDT, infer its qty just for math
            decimal addQty = rows.Sum(r =>
            {
                if (r.Quantity.HasValue) return r.Quantity.Value;
                if (r.InvestedUSDT.HasValue && r.EntryPrice > 0m) return r.InvestedUSDT.Value / r.EntryPrice;
                return 0m;
            });
            decimal addInv = rows.Sum(r => r.InvestedUSDT ?? ((r.Quantity ?? 0m) * r.EntryPrice));

            decimal totalQty = initQty + addQty;
            decimal totalInv = initInv + addInv;

            lblKpiQuantity.Text = totalQty > 0 ? (totalQty >= 1m ? totalQty.ToString("0.###") : totalQty.ToString("0.########")) : "--";
            lblKpiInvested.Text = totalInv > 0 ? totalInv.ToString("0.00") : "--";

            decimal avgCost = totalQty > 0 ? totalInv / totalQty : 0m;
            lblKpiAvgCost.Text = totalQty > 0 ? (avgCost >= 1m ? avgCost.ToString("0.###") : avgCost.ToString("0.########")) : "--";

            decimal currentValue = totalQty * currentPrice;
            lblKpiCurrentValue.Text = currentPrice > 0 ? currentValue.ToString("0.00") : "--";

            // Break-even = avg cost
            lblKpiRecoveredCaption.Text = "Break-even Price:";
            lblKpiRecovered.Text = totalQty > 0
                ? (avgCost >= 1m ? avgCost.ToString("0.###") : avgCost.ToString("0.########"))
                : "--";
            lblKpiRecovered.ForeColor = ThemeManager.AccentColor;

            // Decide secondary KPI (profit vs delta-to-BE)
            decimal unreal = currentValue - totalInv; // define ONCE
            decimal neededPriceDelta = 0m;            // keep in scope for color use below

            if (currentPrice > 0 && totalQty > 0 && currentPrice >= avgCost)
            {
                // Above BE -> show current profit
                lblKpiNeededCaption.Text = "Current Profit:";
                lblKpiNeeded.Text = unreal.ToString("0.00");
                lblKpiNeeded.ForeColor = unreal >= 0 ? ThemeManager.PositiveColor : ThemeManager.NegativeColor;
            }
            else
            {
                lblKpiNeededCaption.Text = "Current Profit:";
                lblKpiNeeded.Text = unreal.ToString("0.00");
                lblKpiNeeded.ForeColor = unreal <= 0 ? ThemeManager.NegativeColor : ThemeManager.NegativeColor;
            }

            // Progress %
            int progress;
            if (avgCost <= 0m || currentPrice <= 0m) progress = 0;
            else progress = (int)Math.Round(Math.Min(100.0, Math.Max(0.0, (double)(currentPrice / avgCost) * 100.0)),
                                            MidpointRounding.AwayFromZero);
            prgKpiProgress.Value = Math.Max(prgKpiProgress.Minimum, Math.Min(prgKpiProgress.Maximum, progress));
            lblKpiProgressPct.Text = $"{progress}%";

            // Auto-flip status (with a tiny epsilon to avoid jitter)
            try
            {
                const decimal EPS = 0.0000001m;
                if (avgCost > 0m && currentPrice > 0m)
                {
                    var target = (currentPrice + EPS >= avgCost)
                        ? RecoveryCaseStatus.WrittenOff
                        : RecoveryCaseStatus.Active;

                    if (_case.Status != target)
                    {
                        _caseMgr.ChangeStatus(_case.Id, target);
                        _case.Status = target; // keep local in sync
                        UpdateWindowTitle();
                        CaseStatusChanged?.Invoke(_case.Id, _case.Status); // notify parent (optional)
                    }
                }
            }
            catch
            {
                // best-effort, non-fatal
            }

            // Colors for primary KPIs
            lblKpiCurrentPrice.ForeColor = ThemeManager.AccentColor;
            lblKpiCurrentValue.ForeColor = unreal > 0m ? ThemeManager.PositiveColor :
                                           unreal < 0m ? ThemeManager.NegativeColor :
                                           ThemeManager.DataTextColor;
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
            if (marginMode) txtAlcQuantity.Text = string.Empty;
            else txtAlcMargin.Text = string.Empty;
        }

        private void ClearInputs()
        {
            dtpAlcTradeDate.Value = DateTime.Now;
            txtAlcEntryPrice.Text = "";
            txtAlcMargin.Text = "";
            txtAlcQuantity.Text = "";
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
            if (!TryParseDec(txtAlcEntryPrice.Text, out var entry) || entry <= 0m)
            {
                MessageBox.Show("Entry Price must be a positive number.");
                return;
            }

            decimal? invested = null;
            decimal? qty = null;

            if (rbAlcModeMargin.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtAlcMargin.Text))
                {
                    if (!TryParseDec(txtAlcMargin.Text, out var m) || m < 0m)
                    {
                        MessageBox.Show("Invested (USDT) must be non-negative.");
                        return;
                    }
                    invested = m;
                    // per your request: compute & STORE Quantity (single column)
                    qty = ComputeQuantity(entry, invested, null);
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

            if (!invested.HasValue && !qty.HasValue)
            {
                MessageBox.Show("Enter Invested (USDT) or Quantity.");
                return;
            }

            _alcMgr.Add(_caseId, dtpAlcTradeDate.Value.Date, entry, invested, qty);
            await ReloadAfterChangeAsync("Allocation added!");
        }

        private async void btnAlcUpdate_Click(object sender, EventArgs e)
        {
            if (_updateId == null) return;

            if (!TryParseDec(txtAlcEntryPrice.Text, out var entry) || entry <= 0m)
            {
                MessageBox.Show("Entry Price must be a positive number.");
                return;
            }

            decimal? invested = null;
            decimal? qty = null;

            if (rbAlcModeMargin.Checked)
            {
                if (!string.IsNullOrWhiteSpace(txtAlcMargin.Text))
                {
                    if (!TryParseDec(txtAlcMargin.Text, out var m) || m < 0m)
                    {
                        MessageBox.Show("Invested (USDT) must be non-negative.");
                        return;
                    }
                    invested = m;
                    // Keep single quantity column policy on updates too
                    qty = ComputeQuantity(entry, invested, null);
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

            if (!invested.HasValue && !qty.HasValue)
            {
                MessageBox.Show("Enter Invested (USDT) or Quantity.");
                return;
            }

            _alcMgr.Update(_updateId.Value, dtpAlcTradeDate.Value.Date, entry, invested, qty);
            await ReloadAfterChangeAsync("Allocation updated!");
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
                dtpAlcTradeDate.Value = row.TradeDate;
                txtAlcEntryPrice.Text = row.EntryPrice.ToString("0.###");

                if (row.Quantity.HasValue)
                {
                    rbAlcModeQuantity.Checked = true;
                    txtAlcQuantity.Text = row.Quantity.Value.ToString("0.###");
                    txtAlcMargin.Text = "";
                }
                else
                {
                    rbAlcModeMargin.Checked = true;
                    txtAlcMargin.Text = row.InvestedUSDT?.ToString("0.00") ?? "";
                    txtAlcQuantity.Text = "";
                }

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
