using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;

namespace TradingJournal.Pl.PlaceHolder.RiskManagement
{
    /// <summary>
    /// "Stop-Loss & Take-Profit from Margin + Leverage" calculator.
    /// Reads inputs, calls LeverageSlTpCalculator, renders the result.
    /// Results carry semantic colour (green = healthy, amber = marginal, red = warning).
    /// </summary>
    public partial class FrmSlTpFromLeverage : UserControl, IResponsiveChildForm
    {
        // Column names — must match the DataGridView columns in the designer.
        private const string ColLevel = "colLevel";
        private const string ColMultiple = "colMultiple";
        private const string ColPercent = "colPercent";
        private const string ColMovePct = "colMovePct";
        private const string ColPrice = "colPrice";
        private const string ColQty = "colQty";
        private const string ColPnl = "colPnl";

        private static readonly Color OkColor = Color.MediumSeaGreen;
        private static readonly Color WarnColor = Color.OrangeRed;
        private static readonly Color CautionColor = Color.Goldenrod;

        private static readonly string[] ComputedColumns = { ColLevel, ColMovePct, ColPrice, ColQty, ColPnl };

        private readonly ResponsiveLayoutManager _layoutManager;
        private readonly LeverageSlTpCalculator _calculator = new LeverageSlTpCalculator();
        private readonly ToolTip _tip = new ToolTip();
        private bool _isUpdating;

        public FrmSlTpFromLeverage()
        {
            InitializeComponent();

            _isUpdating = true;
            SetupDirectionCombo();
            SetupGrid();
            SetDefaultInputs();
            PrefillBalanceFromSettings();
            _isUpdating = false;

            WireEvents();
            SetupTooltips();

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            ThemeManager.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) => ThemeManager.ThemeChanged -= OnThemeChanged; // avoid static-event leak
            ApplyTheme();
        }

        private void OnThemeChanged(object sender, EventArgs e) => ApplyTheme();

        private void FrmSlTpFromLeverage_Load(object sender, EventArgs e) => Recalculate();

        // ----------------------------------------------------------------- Responsive
        public void SetWindowState(FormWindowStateExtended newState)
                => _layoutManager.SetWindowState(newState);

        // ----------------------------------------------------------------- Setup
        private void SetupDirectionCombo()
        {
            if (cmbDirection.Items.Count == 0)
            {
                cmbDirection.Items.Add("Long");
                cmbDirection.Items.Add("Short");
            }
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cmbDirection.SelectedIndex < 0) cmbDirection.SelectedIndex = 0; // Long
        }

        private void InitializeResponsiveLayouts()
        {
            RegisterSectionAsIs(pnlSlnTp, pnlSlnTp_Max);
            RegisterSectionAsIs(pnlTpLevels, pnlTpLevels_Max);
            RegisterSectionAsIs(pnlMetrics, pnlMetrics_Max);
            RegisterSectionAsIs(pnlChecks, pnlChecks_Max);
        }

        private void RegisterSectionAsIs(Panel normal, Panel max)
        {
            foreach (Control c in normal.Controls)
                _layoutManager.RegisterControl(c, normal, max, c.Location, c.Size);
        }

        private void SetupGrid()
        {
            dgvTakeProfits.AllowUserToAddRows = false;
            dgvTakeProfits.AllowUserToDeleteRows = false;
            dgvTakeProfits.RowHeadersVisible = false;
            dgvTakeProfits.MultiSelect = false;
            dgvTakeProfits.ScrollBars = ScrollBars.None;               // 3 fixed rows never scroll
            dgvTakeProfits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Taller header + rows (set BEFORE adding rows) so the table doesn't look starved.
            dgvTakeProfits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTakeProfits.ColumnHeadersHeight = 36;
            dgvTakeProfits.RowTemplate.Height = 44;

            // Stretch to fill the panel width, and keep it filled if the panel resizes.
            if (dgvTakeProfits.Parent is Control host)
            {
                dgvTakeProfits.Width = host.ClientSize.Width - (2 * dgvTakeProfits.Left);
                dgvTakeProfits.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            foreach (var name in ComputedColumns)
                if (dgvTakeProfits.Columns.Contains(name))
                    dgvTakeProfits.Columns[name].ReadOnly = true;

            dgvTakeProfits.Rows.Clear();
            AddTpRow("TP1", "2", "34");
            AddTpRow("TP2", "3", "33");
            AddTpRow("TP3", "4", "33");
        }

        private void AddTpRow(string level, string multiple, string percent)
        {
            int i = dgvTakeProfits.Rows.Add();
            var row = dgvTakeProfits.Rows[i];
            row.Cells[ColLevel].Value = level;
            row.Cells[ColMultiple].Value = multiple;
            row.Cells[ColPercent].Value = percent;
        }

        private void SetDefaultInputs()
        {
            if (string.IsNullOrWhiteSpace(txtRiskPercent.Text)) txtRiskPercent.Text = "2";
            if (string.IsNullOrWhiteSpace(txtLeverage.Text)) txtLeverage.Text = "10";
        }

        // Contextual notes live as hover text on the field they apply to, so the screen stays clean.
        private void SetupTooltips()
        {
            _tip.InitialDelay = 300;
            _tip.ReshowDelay = 100;
            _tip.AutoPopDelay = 30000; // keep long tips on screen long enough to read

            _tip.SetToolTip(lblTitle,
                "How this works: it runs backward — it finds the stop-loss price that loses exactly your " +
                "risk amount for the margin and leverage you chose.\n" +
                "It ignores funding and trading fees, so real P&L on perps will be slightly lower.");

            _tip.SetToolTip(lblTableTitle,
                "TP1/TP2/TP3 are multiples of the SL distance %: 1× = same move as the SL but in the profit " +
                "direction, 2× = double, etc.\nThe % move, TP price, Qty and PnL columns are auto-calculated.");

            _tip.SetToolTip(txtRiskPercent,
                "Risk per trade as a % of balance. The stop is placed so a full stop-out loses exactly this amount.");

            _tip.SetToolTip(txtMargin,
                "Margin you post, in USDT. Position size = margin × leverage. Leave this empty and use the " +
                "coins box to size the trade in coins instead.");

            _tip.SetToolTip(txtCoinQty,
                "Size the trade directly in coins (e.g. 3 for 3 ETH) instead of USDT. Filling this clears the " +
                "USDT margin box; the stop-loss and TP ladder are then sized from this position.");

            _tip.SetToolTip(txtLeverage,
                "Liquidation distance shown is a rough 1/leverage estimate — it ignores exchange maintenance margin " +
                "and fees. Check your exchange's real liquidation price before trading.");
        }

        private void PrefillBalanceFromSettings()
        {
            if (!string.IsNullOrWhiteSpace(txtBalance.Text)) return;
            try
            {
                using (var db = new AppDbContext())
                {
                    var trades = db.Trades.ToList();
                    var settings = SettingsManager.Load();
                    var report = new DashboardManager(settings).GenerateReport(trades);
                    txtBalance.Text = report.TotalPortfolioValue.ToString("0.##",
                        System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch { /* leave blank; user types it in */ }
        }

        private void WireEvents()
        {
            foreach (var tb in InputTextBoxes())
                tb.TextChanged += Input_Changed;

            // Margin (USDT) and coin quantity are mutually exclusive: filling one clears the other,
            // so exactly one drives the calculation.
            txtMargin.TextChanged += MarginBox_TextChanged;
            txtCoinQty.TextChanged += CoinBox_TextChanged;

            cmbDirection.SelectedIndexChanged += Input_Changed;
            dgvTakeProfits.CellEndEdit += Dgv_CellEndEdit;
        }

        private void MarginBox_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            if (txtMargin.TextLength > 0 && txtCoinQty.TextLength > 0)
            {
                _isUpdating = true;      // clearing the other box must not re-enter this logic
                txtCoinQty.Clear();
                _isUpdating = false;
            }
            Recalculate();
        }

        private void CoinBox_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            if (txtCoinQty.TextLength > 0 && txtMargin.TextLength > 0)
            {
                _isUpdating = true;
                txtMargin.Clear();
                _isUpdating = false;
            }
            Recalculate();
        }

        private IEnumerable<TextBox> InputTextBoxes()
        {
            yield return txtBalance;
            yield return txtRiskPercent;
            yield return txtEntryPrice;
            yield return txtLeverage;
        }

        private void Input_Changed(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            Recalculate();
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_isUpdating || e.ColumnIndex < 0) return;
            var name = dgvTakeProfits.Columns[e.ColumnIndex].Name;
            if (name == ColMultiple || name == ColPercent)
                Recalculate();
        }

        // ----------------------------------------------------------------- Read inputs
        private bool TryBuildInput(out LeverageSlTpInput input, out string parseError)
        {
            input = null;
            parseError = null;

            if (!NumericInput.TryParseDecimal(txtBalance.Text, out var balance))
            { parseError = "Enter a valid account balance."; return false; }
            if (!NumericInput.TryParseDecimal(txtRiskPercent.Text, out var riskPercent))
            { parseError = "Enter a valid risk % (e.g. 2)."; return false; }
            if (!NumericInput.TryParseDecimal(txtEntryPrice.Text, out var entry))
            { parseError = "Enter a valid entry price."; return false; }
            if (!NumericInput.TryParseDecimal(txtLeverage.Text, out var leverage))
            { parseError = "Enter a valid leverage."; return false; }

            // The two size boxes are kept mutually exclusive by the TextChanged handlers, so mode is
            // simply "whichever one has a value". Coins wins if both are somehow non-empty.
            bool useCoins = !string.IsNullOrWhiteSpace(txtCoinQty.Text);
            decimal margin = 0m, coinQty = 0m;
            if (useCoins)
            {
                if (!NumericInput.TryParseDecimal(txtCoinQty.Text, out coinQty))
                { parseError = "Enter a valid coin quantity."; return false; }
            }
            else if (!NumericInput.TryParseDecimal(txtMargin.Text, out margin))
            {
                parseError = "Enter a margin in USDT, or a position size in coins.";
                return false;
            }

            var takeProfits = new List<TakeProfitInput>();
            foreach (DataGridViewRow row in dgvTakeProfits.Rows)
            {
                if (row.IsNewRow) continue;

                var level = row.Cells[ColLevel].Value?.ToString() ?? "TP";
                if (!NumericInput.TryParseDecimal(row.Cells[ColMultiple].Value?.ToString(), out var m))
                { parseError = $"Enter a valid SL multiple for {level}."; return false; }
                if (!NumericInput.TryParseDecimal(row.Cells[ColPercent].Value?.ToString(), out var p))
                { parseError = $"Enter a valid % of position for {level}."; return false; }

                takeProfits.Add(new TakeProfitInput { SlMultiple = m, PositionPercent = p });
            }

            input = new LeverageSlTpInput
            {
                AccountBalance = balance,
                RiskFraction = riskPercent / 100m,
                Direction = ReadDirection(),
                EntryPrice = entry,
                SizingMode = useCoins ? SizingMode.Coins : SizingMode.Margin,
                Margin = margin,
                CoinQuantity = coinQty,
                Leverage = leverage,
                TakeProfits = takeProfits
            };
            return true;
        }

        private TradeDirection ReadDirection()
        {
            var text = cmbDirection.SelectedItem?.ToString() ?? cmbDirection.Text ?? "Long";
            return text.Trim().Equals("Short", StringComparison.OrdinalIgnoreCase)
                ? TradeDirection.Short
                : TradeDirection.Long;
        }

        // ----------------------------------------------------------------- Calculate + render
        private void Recalculate()
        {
            if (!TryBuildInput(out var input, out var parseError))
            {
                ShowIncomplete(parseError);
                return;
            }

            var result = _calculator.Calculate(input);
            if (!result.IsValid)
            {
                ShowIncomplete(string.Join("  ", result.Errors));
                return;
            }

            RenderResult(result);
        }

        private void RenderResult(LeverageSlTpResult r)
        {
            lblRiskAmount.Text = FormatMoney(r.RiskAmount);
            lblPositionUsdt.Text = FormatMoney(r.PositionSizeUsdt);
            lblPositionCoins.Text = FormatCoins(r.PositionSizeCoins);
            lblSlDistancePct.Text = FormatPercent(r.StopLossDistancePercent);
            lblStopLossPrice.Text = FormatPrice(r.StopLossPrice);
            lblLiqDistancePct.Text = FormatPercent(r.LiquidationDistancePercent);
            lblTotalPnl.Text = FormatMoney(r.TotalProfitLoss);
            lblReturnOnMargin.Text = FormatReturn(r.ReturnOnMargin);
            lblRewardToRisk.Text = FormatR(r.RewardToRisk);

            decimal allocationSum = 0m;
            foreach (var tp in r.TakeProfits) allocationSum += tp.PositionPercent;

            var marginMsg = FindWarning(r, "Risk amount exceeds");
            var liqMsg = FindWarning(r, "liquidation distance");
            bool marginWarn = marginMsg != null;
            bool liqWarn = liqMsg != null;

            // Short text in the label (AutoSize), full explanation in the tooltip.
            lblMarginCheck.Text = marginWarn ? "Risk exceeds margin!" : "OK";
            lblLiqCheck.Text = liqWarn ? "SL beyond liquidation!" : "OK";
            lblAllocationCheck.Text = r.AllocationIsComplete ? "OK — 100%" : $"{allocationSum:0.##}% (need 100%)";

            _tip.SetToolTip(lblMarginCheck, marginWarn ? marginMsg : "Risk amount is within your margin.");
            _tip.SetToolTip(lblLiqCheck, liqWarn ? liqMsg : "Stop sits before the approximate liquidation distance.");
            _tip.SetToolTip(lblAllocationCheck, r.AllocationIsComplete
                ? "All take-profit levels total 100%."
                : (FindWarning(r, "allocation totals") ?? "Take-profit % must total 100%."));

            // Semantic colours: green = healthy, red = warning.
            lblMarginCheck.ForeColor = marginWarn ? WarnColor : OkColor;
            lblLiqCheck.ForeColor = liqWarn ? WarnColor : OkColor;
            lblAllocationCheck.ForeColor = r.AllocationIsComplete ? OkColor : WarnColor;

            // Reward-to-risk by quality of the setup.
            lblRewardToRisk.ForeColor =
                r.RewardToRisk >= 2m ? OkColor :        // solid
                r.RewardToRisk >= 1m ? CautionColor :   // marginal
                                       WarnColor;       // losing edge

            _isUpdating = true;
            for (int i = 0; i < r.TakeProfits.Count && i < dgvTakeProfits.Rows.Count; i++)
            {
                var tp = r.TakeProfits[i];
                var row = dgvTakeProfits.Rows[i];
                row.Cells[ColMovePct].Value = FormatPercent(tp.MovePercent);
                row.Cells[ColPrice].Value = FormatPrice(tp.TargetPrice);
                row.Cells[ColQty].Value = FormatMoney(tp.QuantityUsdt);
                row.Cells[ColPnl].Value = FormatMoney(tp.ProfitLoss);

                // Green = profit, red = loss (edge case), neutral = flat.
                row.Cells[ColPnl].Style.ForeColor =
                    tp.ProfitLoss > 0 ? OkColor : tp.ProfitLoss < 0 ? WarnColor : ThemeManager.TextColor;
            }
            _isUpdating = false;

            lblStatus.Text = string.Empty;
        }

        private void ShowIncomplete(string message)
        {
            foreach (var lbl in ValueLabels()) lbl.Text = "--";

            lblMarginCheck.Text = lblLiqCheck.Text = lblAllocationCheck.Text = "--";
            _tip.SetToolTip(lblMarginCheck, string.Empty);
            _tip.SetToolTip(lblLiqCheck, string.Empty);
            _tip.SetToolTip(lblAllocationCheck, string.Empty);

            // Reset semantic colours so no stale green/red lingers while input is incomplete.
            lblMarginCheck.ForeColor = lblLiqCheck.ForeColor =
                lblAllocationCheck.ForeColor = lblRewardToRisk.ForeColor = ThemeManager.TextColor;

            _isUpdating = true;
            foreach (DataGridViewRow row in dgvTakeProfits.Rows)
            {
                if (row.IsNewRow) continue;
                row.Cells[ColMovePct].Value = row.Cells[ColPrice].Value =
                    row.Cells[ColQty].Value = row.Cells[ColPnl].Value = string.Empty;
                row.Cells[ColPnl].Style.ForeColor = ThemeManager.TextColor;
            }
            _isUpdating = false;

            lblStatus.Text = message ?? "Waiting for valid input…";
            lblStatus.ForeColor = WarnColor;
        }

        private IEnumerable<Label> ValueLabels()
        {
            yield return lblRiskAmount;
            yield return lblPositionUsdt;
            yield return lblPositionCoins;
            yield return lblSlDistancePct;
            yield return lblStopLossPrice;
            yield return lblLiqDistancePct;
            yield return lblTotalPnl;
            yield return lblReturnOnMargin;
            yield return lblRewardToRisk;
        }

        private static string FindWarning(LeverageSlTpResult r, string contains)
        {
            foreach (var w in r.Warnings)
                if (w.IndexOf(contains, StringComparison.OrdinalIgnoreCase) >= 0)
                    return w;
            return null;
        }

        // ----------------------------------------------------------------- Formatting
        private static readonly System.Globalization.CultureInfo Inv =
            System.Globalization.CultureInfo.InvariantCulture;

        private static string FormatMoney(decimal v) => v.ToString("0.00", Inv) + " USDT";
        private static string FormatCoins(decimal v) => v.ToString("0.######", Inv);
        private static string FormatPercent(decimal fraction) => (fraction * 100m).ToString("0.00", Inv) + " %";
        private static string FormatReturn(decimal fraction) => (fraction * 100m).ToString("0.0", Inv) + " %";
        private static string FormatR(decimal v) => v.ToString("0.00", Inv) + " R";
        private static string FormatPrice(decimal v)
        {
            var abs = Math.Abs(v);
            return abs > 0 && abs < 1m ? v.ToString("0.########", Inv) : v.ToString("0.####", Inv);
        }

        // ----------------------------------------------------------------- Theme
        private void ApplyTheme()
        {
            BackColor = ThemeManager.BackgroundColor;
            StyleRecursively(this);
            StyleGrid();
            Recalculate(); // re-applies values + semantic colours so they survive a dark/light switch
        }

        private void StyleRecursively(Control root)
        {
            foreach (Control c in root.Controls)
            {
                switch (c)
                {
                    case DataGridView _:
                        break; // handled by StyleGrid
                    case Panel panel:
                        panel.BackColor = ThemeManager.PanelColor;
                        break;
                    case TextBox tb:
                        tb.BackColor = ThemeManager.TextBoxColor;
                        tb.ForeColor = ThemeManager.TextColor;
                        break; // leave BorderStyle as set in the designer
                    case ComboBox cmb:
                        cmb.BackColor = ThemeManager.TextBoxColor;
                        cmb.ForeColor = ThemeManager.TextColor;
                        cmb.FlatStyle = FlatStyle.Flat;
                        break;
                    case Label lbl:
                        lbl.ForeColor = ThemeManager.TextColor;
                        break;
                }

                if (c.HasChildren && !(c is DataGridView))
                    StyleRecursively(c);
            }
        }

        private void StyleGrid()
        {
            dgvTakeProfits.EnableHeadersVisualStyles = false;
            dgvTakeProfits.BackgroundColor = ThemeManager.PanelColor;
            dgvTakeProfits.GridColor = ThemeManager.BackgroundColor;
            dgvTakeProfits.BorderStyle = BorderStyle.None;

            dgvTakeProfits.DefaultCellStyle.BackColor = ThemeManager.TextBoxColor;
            dgvTakeProfits.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvTakeProfits.DefaultCellStyle.SelectionBackColor = ThemeManager.ButtonHoverColor;
            dgvTakeProfits.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            dgvTakeProfits.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.ButtonColor;
            dgvTakeProfits.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;

            // Visually mark the read-only computed columns.
            foreach (var name in ComputedColumns)
                if (dgvTakeProfits.Columns.Contains(name))
                    dgvTakeProfits.Columns[name].DefaultCellStyle.BackColor = ThemeManager.PanelColor;
        }
    }
}