using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Pl.PlaceHolder.Statistics
{
    public partial class FrmCalculator : Form
    {

        private bool _isUpdating = false;
        public FrmCalculator()
        {
            InitializeComponent();

            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            pnlButtons.BackColor = ThemeManager.CalcPanelColor;
            pnlSharedInputs.BackColor = ThemeManager.CalcPanelColor;
            panel1.BackColor = ThemeManager.CalcPanelColor;
            pnlPnlRr.BackColor = ThemeManager.CalcPanelColor;
            pnlLiquidation.BackColor = ThemeManager.CalcPanelColor;
            pnlTargetPrice.BackColor = ThemeManager.CalcPanelColor;

            // Labels në panele
            foreach (Control ctrl in new[] { pnlSharedInputs, panel1, pnlPnlRr, pnlLiquidation, pnlTargetPrice })
            {
                foreach (Control child in ctrl.Controls)
                {
                    if (child is Label lbl)
                        lbl.ForeColor = ThemeManager.TextColor;
                }
            }

            // --- TextBoxes ---
            foreach (var tb in new[] { txtEntryPrice, txtMargin, txtLeverage, txtPnlExitPrice, txtPnlStopLoss,
                               txtWalletBalance, txtTargetPnl, txtTargetRoi })
            {
                tb.BackColor = ThemeManager.CalcTextBoxColor;
                tb.ForeColor = ThemeManager.TextColor;
                tb.BorderStyle = BorderStyle.FixedSingle;
            }

            // --- RichTextBox (liq info) ---
            rtbLiqInfo.BackColor = ThemeManager.TextBoxColor;
            rtbLiqInfo.ForeColor = ThemeManager.TextColor;

            // --- Buttons ---
            foreach (var btn in new[] { btnCalculate, btnReset, btnCopyResults })
            {
                btn.BackColor = ThemeManager.ButtonColor;
                btn.ForeColor = ThemeManager.ActionButtonTextColor;
                btn.FlatAppearance.BorderSize = 0;
            }
            foreach (var cmb in new[] { cmbDirection, cmbMarginMode })
            {
                cmb.BackColor = ThemeManager.TextBoxColor;
                cmb.ForeColor = ThemeManager.TextColor;
                cmb.FlatStyle = FlatStyle.Flat;
            }

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtEntryPrice.Text, out decimal entryPrice))
            {
                MessageBox.Show("Please enter a valid Entry Price.", "Input Error");
                return;
            }
            if (!decimal.TryParse(txtMargin.Text, out decimal margin))
            {
                MessageBox.Show("Please enter a valid Margin.", "Input Error");
                return;
            }

            // guard against null SelectedItem
            string directionText = cmbDirection.SelectedItem?.ToString() ?? cmbDirection.Text ?? string.Empty;
            bool isLong = directionText.Equals("Long", StringComparison.OrdinalIgnoreCase);

            int leverage = trackBarLeverage.Value;

            if (entryPrice == 0) return;
            decimal positionSize = margin * leverage;
            decimal quantity = positionSize / entryPrice;

            // --- Mode: PnL / Risk-Reward ---
            if (rbModePnl.Checked)
            {
                if (!decimal.TryParse(txtPnlExitPrice.Text, out decimal exitPrice))
                {
                    MessageBox.Show("Please enter a valid Exit Price", "Input Error");
                    return;
                }

                // stop-loss is optional now
                bool hasStopLoss = decimal.TryParse(txtPnlStopLoss.Text, out decimal stopLoss);

                // Binance PnL formula
                decimal pnl = (exitPrice - entryPrice) * quantity * (isLong ? 1 : -1);

                decimal? risk = null;
                decimal? rr = null;

                if (hasStopLoss)
                {
                    // Binance Risk formula
                    decimal calculatedRisk = (stopLoss - entryPrice) * quantity * (isLong ? 1 : -1);
                    risk = calculatedRisk;
                    rr = (Math.Abs(calculatedRisk) > 0) ? pnl / Math.Abs(calculatedRisk) : (decimal?)null;
                }

                decimal maxLoss = 0;

                string marginMode = cmbMarginMode.SelectedItem?.ToString() ?? cmbMarginMode.Text ?? string.Empty;
                if (marginMode.Equals("Isolated", StringComparison.OrdinalIgnoreCase))
                {
                    maxLoss = margin; // isolated → you only lose your margin
                }
                else // Cross
                {
                    if (!decimal.TryParse(txtWalletBalance.Text, out decimal walletBalance))
                    {
                        MessageBox.Show("Please enter a valid Wallet Balance for Cross Margin calculation.", "Input Error");
                        return;
                    }

                    decimal maintenanceMarginRate = 0.005m;
                    decimal maintenanceMargin = positionSize * maintenanceMarginRate;

                    maxLoss = margin + walletBalance - maintenanceMargin;
                }

                // update the label
                lblMaxLoss.Text = maxLoss.ToString("C");
                lblMaxLoss.ForeColor = Color.FromArgb(231, 76, 60);

                UpdatePnlLabels(pnl, risk, hasStopLoss ? pnl : (decimal?)null, rr);
            }

            // --- Mode: Liquidation Price ---
            else if (rbModeLiquidation.Checked)
            {
                // Binance maintenance margin (example: 0.005 = 0.5% default)
                decimal maintenanceMarginRate = 0.005m;

                decimal liquidationPrice = 0;

                string marginMode = cmbMarginMode.SelectedItem?.ToString() ?? cmbMarginMode.Text ?? string.Empty;

                if (marginMode.Equals("Isolated", StringComparison.OrdinalIgnoreCase))
                {
                    if (isLong)
                        liquidationPrice = entryPrice * (1 - (1m / leverage) + maintenanceMarginRate);
                    else
                        liquidationPrice = entryPrice * (1 + (1m / leverage) - maintenanceMarginRate);
                }
                else // Cross
                {
                    if (!decimal.TryParse(txtWalletBalance.Text, out decimal walletBalance))
                    {
                        MessageBox.Show("Please enter a valid Wallet Balance for Cross Margin calculation.", "Input Error");
                        return;
                    }

                    decimal positionMaintenanceMargin = positionSize * maintenanceMarginRate;

                    if (isLong)
                        liquidationPrice = (entryPrice * quantity - (walletBalance + margin - positionMaintenanceMargin)) / quantity;
                    else
                        liquidationPrice = (entryPrice * quantity + (walletBalance + margin - positionMaintenanceMargin)) / quantity;
                }

                lblLiqPriceValue.Text = liquidationPrice.ToString("C");
                lblLiqPriceValue.ForeColor = Color.FromArgb(231, 76, 60);
            }

            // --- Mode: Target Price ---
            else if (rbModeTargetPrice.Checked)
            {
                decimal targetPnl = 0;

                if (decimal.TryParse(txtTargetPnl.Text, out decimal pnlInput) && pnlInput != 0)
                    targetPnl = pnlInput;
                else if (decimal.TryParse(txtTargetRoi.Text, out decimal roiInput) && roiInput != 0)
                    targetPnl = margin * (roiInput / 100);
                else
                {
                    MessageBox.Show("Please enter a valid Target Profit ($) or a valid Target ROI (%).", "Input Error");
                    return;
                }

                if (quantity == 0) return;

                decimal targetPrice = isLong
                    ? entryPrice + (targetPnl / quantity)
                    : entryPrice - (targetPnl / quantity);

                lblTargetPriceValue.Text = targetPrice.ToString("C");
                lblTargetPriceValue.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        private void rbMode_CheckedChanged(object? sender, EventArgs e)
        {
            pnlPnlRr.Visible = rbModePnl.Checked;
            pnlLiquidation.Visible = rbModeLiquidation.Checked;
            pnlTargetPrice.Visible = rbModeTargetPrice.Checked;

            // **NEW:** Clear the specific input fields when the mode changes
            ClearModeSpecificInputs();
        }

        private void FrmCalculator_Load(object sender, EventArgs e)
        {
            cmbDirection.SelectedIndex = 0;
            cmbMarginMode.SelectedIndex = 0;
            trackBarLeverage.Value = 1; // Default to 1x
            txtLeverage.Text = "1";
            lblLeverageValue.Text = "Leverage:";

            // Hook up all event handlers
            rbModePnl.CheckedChanged += rbMode_CheckedChanged;
            rbModeLiquidation.CheckedChanged += rbMode_CheckedChanged;
            rbModeTargetPrice.CheckedChanged += rbMode_CheckedChanged;
            txtEntryPrice.TextChanged += txtInput_TextChanged;
            txtMargin.TextChanged += txtInput_TextChanged;
            trackBarLeverage.Scroll += trackBarLeverage_Scroll;
            txtLeverage.TextChanged += txtLeverage_TextChanged;
            txtLeverage.KeyDown += txtLeverage_KeyDown;
            txtTargetPnl.TextChanged += txtTargetPnl_TextChanged;
            txtTargetRoi.TextChanged += txtTargetRoi_TextChanged;
            cmbMarginMode.SelectedIndexChanged += cmbMarginMode_SelectedIndexChanged;

            // Set initial UI state
            rbMode_CheckedChanged(this, EventArgs.Empty);
            cmbMarginMode_SelectedIndexChanged(this, EventArgs.Empty);
            UpdateCalculations();
            ApplyTheme();
        }

        private void trackBarLeverage_Scroll(object? sender, EventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;
            txtLeverage.Text = trackBarLeverage.Value.ToString();
            UpdateCalculations();
            _isUpdating = false;
        }

        private void UpdateCalculations()
        {
            if (decimal.TryParse(txtMargin.Text, out decimal margin) && int.TryParse(txtLeverage.Text, out int leverage))
                lblPositionSizeValue.Text = $"Position Size: {(margin * leverage):C2}";
            else
                lblPositionSizeValue.Text = "Position Size: --";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEntryPrice.Text = "";
            txtMargin.Text = "";
            trackBarLeverage.Value = 1; // Reset to 1x
            txtLeverage.Text = "1";
            lblMaxLoss.Text = "--";
            lblPositionSizeValue.Text = "Position Size: --";

            // Clear all mode-specific inputs
            ClearModeSpecificInputs();
        }
        private void ClearModeSpecificInputs()
        {
            // Clear PnL panel (make stop-loss optional: do not clear wallet balance)
            txtPnlExitPrice.Text = "";
            // leave txtPnlStopLoss.Text as-is so user doesn't have to re-enter when switching modes
            label8.Text = "--";
            lblMaxLoss.Text = "--";
            lblRiskValue.Text = "--";
            lblRewardValue.Text = "--";
            lblRrValue.Text = "--";

            // Clear Liquidation panel
            // Do not clear txtWalletBalance here to preserve user input when switching between panels
            lblLiqPriceValue.Text = "--";

            // Clear Target Price panel
            txtTargetPnl.Text = "";
            txtTargetRoi.Text = "";
            lblTargetPriceValue.Text = "--";
        }

        private void btnCopyResults_Click(object sender, EventArgs e)
        {
            if (lblPositionSizeValue.Text == "Position Size: --")
            {
                MessageBox.Show("Please perform a calculation before copying.", "No Results");
                return;
            }

            StringBuilder summary = new StringBuilder();
            summary.AppendLine("--- Trade Calculation Summary ---");

            // Append the shared details first
            summary.AppendLine($"Direction: {cmbDirection.SelectedItem}");
            summary.AppendLine($"Leverage: {lblLeverageValue.Text}");
            summary.AppendLine($"Entry Price: {txtEntryPrice.Text}");
            summary.AppendLine($"Quantity: {txtMargin.Text}");
            summary.AppendLine($"Cost: {lblPositionSizeValue.Text.Replace("Cost: ", "")}");
            summary.AppendLine("---------------------------------");

            // Append the results from the currently visible panel
            if (pnlPnlRr.Visible)
            {
                summary.AppendLine("Mode: PnL / Risk-Reward");
                summary.AppendLine($"Potential PnL: {label8.Text}"); // Using correct name
                summary.AppendLine($"Risk: {lblRiskValue.Text}");
                summary.AppendLine($"Reward: {lblRewardValue.Text}");
                summary.AppendLine($"R/R Ratio: {lblRrValue.Text}");
            }
            else if (pnlLiquidation.Visible)
            {
                summary.AppendLine("Mode: Liquidation");
                summary.AppendLine($"Est. Liquidation Price: {lblLiqPriceValue.Text}");
            }
            else if (pnlTargetPrice.Visible)
            {
                summary.AppendLine("Mode: Target Price");
                summary.AppendLine($"Required Exit Price: {lblTargetPriceValue.Text}");
            }

            // Copy the final string to the clipboard
            Clipboard.SetText(summary.ToString());

            MessageBox.Show("Calculation results copied to clipboard!", "Copied!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Updated to accept nullable risk/reward/rr and display "--" when not provided
        private void UpdatePnlLabels(decimal pnl, decimal? risk, decimal? reward, decimal? rrRatio)
        {
            Color positiveColor = Color.FromArgb(46, 204, 113);
            Color negativeColor = Color.FromArgb(231, 76, 60);
            Color neutralColor = Color.Gray;

            label8.Text = pnl.ToString("C");
            label8.ForeColor = pnl >= 0 ? positiveColor : negativeColor;

            if (risk.HasValue)
            {
                lblRiskValue.Text = risk.Value.ToString("C");
                // risk is typically negative for loss; color accordingly (loss = negative)
                lblRiskValue.ForeColor = risk.Value <= 0 ? negativeColor : positiveColor;
            }
            else
            {
                lblRiskValue.Text = "--";
                lblRiskValue.ForeColor = neutralColor;
            }

            if (reward.HasValue)
            {
                lblRewardValue.Text = reward.Value.ToString("C");
                lblRewardValue.ForeColor = reward.Value >= 0 ? positiveColor : negativeColor;
            }
            else
            {
                lblRewardValue.Text = "--";
                lblRewardValue.ForeColor = neutralColor;
            }

            if (rrRatio.HasValue)
            {
                lblRrValue.Text = rrRatio.Value.ToString("F2");
                lblRrValue.ForeColor = rrRatio.Value >= 1 ? positiveColor : negativeColor;
            }
            else
            {
                lblRrValue.Text = "--";
                lblRrValue.ForeColor = neutralColor;
            }
        }

        private void txtLeverage_TextChanged(object? sender, EventArgs e)
        {
            if (_isUpdating) return;
            if (int.TryParse(txtLeverage.Text, out int leverage) && leverage >= trackBarLeverage.Minimum && leverage <= trackBarLeverage.Maximum)
            {
                _isUpdating = true;
                trackBarLeverage.Value = leverage;
                UpdateCalculations();
                _isUpdating = false;
            }
        }

        private void txtInput_TextChanged(object? sender, EventArgs e)
        {
            UpdateCalculations();
        }

        private void txtLeverage_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCalculate.Focus();
            }
        }


        private void txtTargetPnl_TextChanged(object? sender, EventArgs e)
        {
            if (_isUpdating || string.IsNullOrWhiteSpace(txtTargetPnl.Text)) return;
            _isUpdating = true;
            txtTargetRoi.Text = "";
            _isUpdating = false;
        }

        private void txtTargetRoi_TextChanged(object? sender, EventArgs e)
        {
            if (_isUpdating || string.IsNullOrWhiteSpace(txtTargetRoi.Text)) return;
            _isUpdating = true;
            txtTargetPnl.Text = "";
            _isUpdating = false;
        }

        private void cmbMarginMode_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool isCrossMode = (cmbMarginMode.SelectedItem?.ToString() ?? cmbMarginMode.Text ?? string.Empty) == "Cross";
            lblWalletBalance.Visible = isCrossMode;
            txtWalletBalance.Visible = isCrossMode;

            if (isCrossMode)
                rtbLiqInfo.Text = "Cross mode uses your wallet balance to prevent liquidation. This is an estimate if this were your only open position.";
            else
                rtbLiqInfo.Text = "Isolated mode only uses the margin allocated to this position. Your wallet balance is not at risk.";
        }
    }
}