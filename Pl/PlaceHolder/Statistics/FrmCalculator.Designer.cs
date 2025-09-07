namespace TradingJournal.Pl.PlaceHolder.Statistics
{
    partial class FrmCalculator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rbModePnl = new RadioButton();
            rbModeLiquidation = new RadioButton();
            rbModeTargetPrice = new RadioButton();
            panel1 = new Panel();
            pnlSharedInputs = new Panel();
            txtLeverage = new TextBox();
            lblPositionSizeValue = new Label();
            txtMargin = new TextBox();
            txtEntryPrice = new TextBox();
            label2 = new Label();
            label1 = new Label();
            trackBarLeverage = new TrackBar();
            lblLeverageValue = new Label();
            cmbDirection = new ComboBox();
            label5 = new Label();
            pnlButtons = new Panel();
            btnReset = new FontAwesome.Sharp.IconButton();
            btnCopyResults = new FontAwesome.Sharp.IconButton();
            btnCalculate = new FontAwesome.Sharp.IconButton();
            pnlPnlRr = new Panel();
            label13 = new Label();
            lblRiskValue = new Label();
            lblRewardValue = new Label();
            lblRrValue = new Label();
            label11 = new Label();
            txtPnlStopLoss = new TextBox();
            txtPnlExitPrice = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            pnlTargetPrice = new Panel();
            txtTargetPnl = new TextBox();
            txtTargetRoi = new TextBox();
            lblTargetPriceValue = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            pnlLiquidation = new Panel();
            rtbLiqInfo = new RichTextBox();
            txtWalletBalance = new TextBox();
            cmbMarginMode = new ComboBox();
            lblLiqPriceValue = new Label();
            label12 = new Label();
            lblWalletBalance = new Label();
            label9 = new Label();
            label10 = new Label();
            lblMaxLoss = new Label();
            panel1.SuspendLayout();
            pnlSharedInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLeverage).BeginInit();
            pnlButtons.SuspendLayout();
            pnlPnlRr.SuspendLayout();
            pnlTargetPrice.SuspendLayout();
            pnlLiquidation.SuspendLayout();
            SuspendLayout();
            // 
            // rbModePnl
            // 
            rbModePnl.AutoSize = true;
            rbModePnl.Checked = true;
            rbModePnl.ForeColor = Color.FromArgb(156, 163, 175);
            rbModePnl.Location = new Point(41, 18);
            rbModePnl.Name = "rbModePnl";
            rbModePnl.Size = new Size(77, 24);
            rbModePnl.TabIndex = 3;
            rbModePnl.TabStop = true;
            rbModePnl.Text = "PnL/RR";
            rbModePnl.UseVisualStyleBackColor = true;
            // 
            // rbModeLiquidation
            // 
            rbModeLiquidation.AutoSize = true;
            rbModeLiquidation.ForeColor = Color.FromArgb(156, 163, 175);
            rbModeLiquidation.Location = new Point(162, 18);
            rbModeLiquidation.Name = "rbModeLiquidation";
            rbModeLiquidation.Size = new Size(105, 24);
            rbModeLiquidation.TabIndex = 4;
            rbModeLiquidation.Text = "Liquidation";
            rbModeLiquidation.UseVisualStyleBackColor = true;
            // 
            // rbModeTargetPrice
            // 
            rbModeTargetPrice.AutoSize = true;
            rbModeTargetPrice.ForeColor = Color.FromArgb(156, 163, 175);
            rbModeTargetPrice.Location = new Point(311, 18);
            rbModeTargetPrice.Name = "rbModeTargetPrice";
            rbModeTargetPrice.Size = new Size(107, 24);
            rbModeTargetPrice.TabIndex = 5;
            rbModeTargetPrice.Text = "Target Price";
            rbModeTargetPrice.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 58, 95);
            panel1.Controls.Add(rbModePnl);
            panel1.Controls.Add(rbModeTargetPrice);
            panel1.Controls.Add(rbModeLiquidation);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(609, 60);
            panel1.TabIndex = 6;
            // 
            // pnlSharedInputs
            // 
            pnlSharedInputs.BackColor = Color.FromArgb(30, 58, 95);
            pnlSharedInputs.Controls.Add(txtLeverage);
            pnlSharedInputs.Controls.Add(lblPositionSizeValue);
            pnlSharedInputs.Controls.Add(txtMargin);
            pnlSharedInputs.Controls.Add(txtEntryPrice);
            pnlSharedInputs.Controls.Add(label2);
            pnlSharedInputs.Controls.Add(label1);
            pnlSharedInputs.Controls.Add(trackBarLeverage);
            pnlSharedInputs.Controls.Add(lblLeverageValue);
            pnlSharedInputs.Controls.Add(cmbDirection);
            pnlSharedInputs.Controls.Add(label5);
            pnlSharedInputs.Location = new Point(12, 89);
            pnlSharedInputs.Name = "pnlSharedInputs";
            pnlSharedInputs.Size = new Size(286, 386);
            pnlSharedInputs.TabIndex = 7;
            // 
            // txtLeverage
            // 
            txtLeverage.BackColor = Color.FromArgb(13, 27, 42);
            txtLeverage.BorderStyle = BorderStyle.None;
            txtLeverage.Font = new Font("Times New Roman", 13.8F);
            txtLeverage.ForeColor = SystemColors.Window;
            txtLeverage.Location = new Point(142, 84);
            txtLeverage.Name = "txtLeverage";
            txtLeverage.Size = new Size(81, 27);
            txtLeverage.TabIndex = 12;
            txtLeverage.Text = "   ";
            txtLeverage.TextChanged += txtLeverage_TextChanged;
            txtLeverage.KeyDown += txtLeverage_KeyDown;
            // 
            // lblPositionSizeValue
            // 
            lblPositionSizeValue.AutoSize = true;
            lblPositionSizeValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPositionSizeValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblPositionSizeValue.Location = new Point(21, 335);
            lblPositionSizeValue.Name = "lblPositionSizeValue";
            lblPositionSizeValue.Size = new Size(139, 22);
            lblPositionSizeValue.TabIndex = 11;
            lblPositionSizeValue.Text = "Position Size: --";
            // 
            // txtMargin
            // 
            txtMargin.BackColor = Color.FromArgb(13, 27, 42);
            txtMargin.BorderStyle = BorderStyle.None;
            txtMargin.Font = new Font("Times New Roman", 13.8F);
            txtMargin.ForeColor = SystemColors.Window;
            txtMargin.Location = new Point(26, 267);
            txtMargin.Name = "txtMargin";
            txtMargin.Size = new Size(203, 27);
            txtMargin.TabIndex = 10;
            txtMargin.Text = "   ";
            // 
            // txtEntryPrice
            // 
            txtEntryPrice.BackColor = Color.FromArgb(13, 27, 42);
            txtEntryPrice.BorderStyle = BorderStyle.None;
            txtEntryPrice.Font = new Font("Times New Roman", 13.8F);
            txtEntryPrice.ForeColor = SystemColors.Window;
            txtEntryPrice.Location = new Point(26, 194);
            txtEntryPrice.Name = "txtEntryPrice";
            txtEntryPrice.Size = new Size(203, 27);
            txtEntryPrice.TabIndex = 9;
            txtEntryPrice.Text = "   ";
            txtEntryPrice.TextChanged += txtInput_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(156, 163, 175);
            label2.Location = new Point(26, 232);
            label2.Name = "label2";
            label2.Size = new Size(142, 22);
            label2.TabIndex = 8;
            label2.Text = "Margin (USDT):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(156, 163, 175);
            label1.Location = new Point(26, 164);
            label1.Name = "label1";
            label1.Size = new Size(105, 22);
            label1.TabIndex = 7;
            label1.Text = "Entry Price:";
            // 
            // trackBarLeverage
            // 
            trackBarLeverage.Location = new Point(21, 114);
            trackBarLeverage.Maximum = 125;
            trackBarLeverage.Minimum = 1;
            trackBarLeverage.Name = "trackBarLeverage";
            trackBarLeverage.Size = new Size(202, 56);
            trackBarLeverage.TabIndex = 6;
            trackBarLeverage.TickFrequency = 10;
            trackBarLeverage.Value = 1;
            trackBarLeverage.Scroll += trackBarLeverage_Scroll;
            // 
            // lblLeverageValue
            // 
            lblLeverageValue.AutoSize = true;
            lblLeverageValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLeverageValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblLeverageValue.Location = new Point(21, 80);
            lblLeverageValue.Name = "lblLeverageValue";
            lblLeverageValue.Size = new Size(89, 22);
            lblLeverageValue.TabIndex = 5;
            lblLeverageValue.Text = "Leverage:";
            // 
            // cmbDirection
            // 
            cmbDirection.BackColor = Color.FromArgb(30, 58, 95);
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.FlatStyle = FlatStyle.Flat;
            cmbDirection.Font = new Font("Times New Roman", 13.8F);
            cmbDirection.FormattingEnabled = true;
            cmbDirection.Items.AddRange(new object[] { "Long", "Short" });
            cmbDirection.Location = new Point(21, 41);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(202, 34);
            cmbDirection.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(156, 163, 175);
            label5.Location = new Point(21, 16);
            label5.Name = "label5";
            label5.Size = new Size(136, 22);
            label5.TabIndex = 3;
            label5.Text = "Trade Direction";
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.FromArgb(30, 58, 95);
            pnlButtons.Controls.Add(btnReset);
            pnlButtons.Controls.Add(btnCopyResults);
            pnlButtons.Controls.Add(btnCalculate);
            pnlButtons.Location = new Point(12, 499);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(609, 108);
            pnlButtons.TabIndex = 8;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(27, 38, 59);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Times New Roman", 12F);
            btnReset.ForeColor = SystemColors.Control;
            btnReset.IconChar = FontAwesome.Sharp.IconChar.None;
            btnReset.IconColor = Color.Black;
            btnReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReset.Location = new Point(427, 30);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(130, 49);
            btnReset.TabIndex = 13;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // btnCopyResults
            // 
            btnCopyResults.BackColor = Color.FromArgb(27, 38, 59);
            btnCopyResults.FlatAppearance.BorderSize = 0;
            btnCopyResults.FlatStyle = FlatStyle.Flat;
            btnCopyResults.Font = new Font("Times New Roman", 12F);
            btnCopyResults.ForeColor = SystemColors.Control;
            btnCopyResults.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCopyResults.IconColor = Color.Black;
            btnCopyResults.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCopyResults.Location = new Point(239, 30);
            btnCopyResults.Name = "btnCopyResults";
            btnCopyResults.Size = new Size(130, 49);
            btnCopyResults.TabIndex = 12;
            btnCopyResults.Text = "Copy Results";
            btnCopyResults.UseVisualStyleBackColor = false;
            btnCopyResults.Click += btnCopyResults_Click;
            // 
            // btnCalculate
            // 
            btnCalculate.BackColor = Color.FromArgb(27, 38, 59);
            btnCalculate.FlatAppearance.BorderSize = 0;
            btnCalculate.FlatStyle = FlatStyle.Flat;
            btnCalculate.Font = new Font("Times New Roman", 12F);
            btnCalculate.ForeColor = SystemColors.Control;
            btnCalculate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnCalculate.IconColor = Color.Black;
            btnCalculate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCalculate.Location = new Point(51, 30);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(130, 49);
            btnCalculate.TabIndex = 11;
            btnCalculate.Text = "Calculate";
            btnCalculate.UseVisualStyleBackColor = false;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // pnlPnlRr
            // 
            pnlPnlRr.BackColor = Color.FromArgb(30, 58, 95);
            pnlPnlRr.Controls.Add(lblMaxLoss);
            pnlPnlRr.Controls.Add(label10);
            pnlPnlRr.Controls.Add(label13);
            pnlPnlRr.Controls.Add(lblRiskValue);
            pnlPnlRr.Controls.Add(lblRewardValue);
            pnlPnlRr.Controls.Add(lblRrValue);
            pnlPnlRr.Controls.Add(label11);
            pnlPnlRr.Controls.Add(txtPnlStopLoss);
            pnlPnlRr.Controls.Add(txtPnlExitPrice);
            pnlPnlRr.Controls.Add(label8);
            pnlPnlRr.Controls.Add(label7);
            pnlPnlRr.Controls.Add(label6);
            pnlPnlRr.Controls.Add(label4);
            pnlPnlRr.Controls.Add(label3);
            pnlPnlRr.Location = new Point(339, 89);
            pnlPnlRr.Name = "pnlPnlRr";
            pnlPnlRr.Size = new Size(282, 386);
            pnlPnlRr.TabIndex = 9;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.FromArgb(156, 163, 175);
            label13.Location = new Point(25, 329);
            label13.Name = "label13";
            label13.Size = new Size(78, 22);
            label13.TabIndex = 16;
            label13.Text = "R/R      :";
            // 
            // lblRiskValue
            // 
            lblRiskValue.AutoSize = true;
            lblRiskValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRiskValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblRiskValue.Location = new Point(128, 285);
            lblRiskValue.Name = "lblRiskValue";
            lblRiskValue.Size = new Size(24, 22);
            lblRiskValue.TabIndex = 15;
            lblRiskValue.Text = "--";
            // 
            // lblRewardValue
            // 
            lblRewardValue.AutoSize = true;
            lblRewardValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRewardValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblRewardValue.Location = new Point(128, 307);
            lblRewardValue.Name = "lblRewardValue";
            lblRewardValue.Size = new Size(24, 22);
            lblRewardValue.TabIndex = 14;
            lblRewardValue.Text = "--";
            // 
            // lblRrValue
            // 
            lblRrValue.AutoSize = true;
            lblRrValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRrValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblRrValue.Location = new Point(128, 329);
            lblRrValue.Name = "lblRrValue";
            lblRrValue.Size = new Size(24, 22);
            lblRrValue.TabIndex = 13;
            lblRrValue.Text = "--";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.FromArgb(156, 163, 175);
            label11.Location = new Point(25, 307);
            label11.Name = "label11";
            label11.Size = new Size(79, 22);
            label11.TabIndex = 12;
            label11.Text = "Reward:";
            // 
            // txtPnlStopLoss
            // 
            txtPnlStopLoss.BackColor = Color.FromArgb(13, 27, 42);
            txtPnlStopLoss.BorderStyle = BorderStyle.None;
            txtPnlStopLoss.Font = new Font("Times New Roman", 13.8F);
            txtPnlStopLoss.ForeColor = SystemColors.Window;
            txtPnlStopLoss.Location = new Point(25, 131);
            txtPnlStopLoss.Name = "txtPnlStopLoss";
            txtPnlStopLoss.Size = new Size(203, 27);
            txtPnlStopLoss.TabIndex = 11;
            txtPnlStopLoss.Text = "   ";
            // 
            // txtPnlExitPrice
            // 
            txtPnlExitPrice.BackColor = Color.FromArgb(13, 27, 42);
            txtPnlExitPrice.BorderStyle = BorderStyle.None;
            txtPnlExitPrice.Font = new Font("Times New Roman", 13.8F);
            txtPnlExitPrice.ForeColor = SystemColors.Window;
            txtPnlExitPrice.Location = new Point(25, 49);
            txtPnlExitPrice.Name = "txtPnlExitPrice";
            txtPnlExitPrice.Size = new Size(203, 27);
            txtPnlExitPrice.TabIndex = 10;
            txtPnlExitPrice.Text = "   ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(156, 163, 175);
            label8.Location = new Point(25, 237);
            label8.Name = "label8";
            label8.Size = new Size(24, 22);
            label8.TabIndex = 8;
            label8.Text = "--";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(156, 163, 175);
            label7.Location = new Point(25, 285);
            label7.Name = "label7";
            label7.Size = new Size(77, 22);
            label7.TabIndex = 7;
            label7.Text = "Risk     :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(156, 163, 175);
            label6.Location = new Point(25, 215);
            label6.Name = "label6";
            label6.Size = new Size(122, 22);
            label6.TabIndex = 6;
            label6.Text = "Potential PnL:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(156, 163, 175);
            label4.Location = new Point(25, 97);
            label4.Name = "label4";
            label4.Size = new Size(137, 22);
            label4.TabIndex = 5;
            label4.Text = "Stop-Loss Price";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(156, 163, 175);
            label3.Location = new Point(32, 21);
            label3.Name = "label3";
            label3.Size = new Size(196, 22);
            label3.TabIndex = 4;
            label3.Text = "Exit Price (Take Profit)";
            // 
            // pnlTargetPrice
            // 
            pnlTargetPrice.BackColor = Color.FromArgb(30, 58, 95);
            pnlTargetPrice.Controls.Add(txtTargetPnl);
            pnlTargetPrice.Controls.Add(txtTargetRoi);
            pnlTargetPrice.Controls.Add(lblTargetPriceValue);
            pnlTargetPrice.Controls.Add(label16);
            pnlTargetPrice.Controls.Add(label17);
            pnlTargetPrice.Controls.Add(label18);
            pnlTargetPrice.Location = new Point(339, 89);
            pnlTargetPrice.Name = "pnlTargetPrice";
            pnlTargetPrice.Size = new Size(282, 386);
            pnlTargetPrice.TabIndex = 11;
            pnlTargetPrice.Visible = false;
            // 
            // txtTargetPnl
            // 
            txtTargetPnl.BackColor = Color.FromArgb(13, 27, 42);
            txtTargetPnl.BorderStyle = BorderStyle.None;
            txtTargetPnl.Font = new Font("Times New Roman", 13.8F);
            txtTargetPnl.ForeColor = SystemColors.Window;
            txtTargetPnl.Location = new Point(24, 56);
            txtTargetPnl.Name = "txtTargetPnl";
            txtTargetPnl.Size = new Size(203, 27);
            txtTargetPnl.TabIndex = 13;
            txtTargetPnl.Text = "   ";
            txtTargetPnl.TextChanged += txtTargetPnl_TextChanged;
            // 
            // txtTargetRoi
            // 
            txtTargetRoi.BackColor = Color.FromArgb(13, 27, 42);
            txtTargetRoi.BorderStyle = BorderStyle.None;
            txtTargetRoi.Font = new Font("Times New Roman", 13.8F);
            txtTargetRoi.ForeColor = SystemColors.Window;
            txtTargetRoi.Location = new Point(24, 143);
            txtTargetRoi.Name = "txtTargetRoi";
            txtTargetRoi.Size = new Size(203, 27);
            txtTargetRoi.TabIndex = 12;
            txtTargetRoi.Text = "   ";
            txtTargetRoi.TextChanged += txtTargetRoi_TextChanged;
            // 
            // lblTargetPriceValue
            // 
            lblTargetPriceValue.AutoSize = true;
            lblTargetPriceValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTargetPriceValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblTargetPriceValue.Location = new Point(24, 255);
            lblTargetPriceValue.Name = "lblTargetPriceValue";
            lblTargetPriceValue.Size = new Size(24, 22);
            lblTargetPriceValue.TabIndex = 8;
            lblTargetPriceValue.Text = "--";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label16.ForeColor = Color.FromArgb(156, 163, 175);
            label16.Location = new Point(24, 215);
            label16.Name = "label16";
            label16.Size = new Size(173, 22);
            label16.TabIndex = 7;
            label16.Text = "Required Exit Price:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.FromArgb(156, 163, 175);
            label17.Location = new Point(24, 114);
            label17.Name = "label17";
            label17.Size = new Size(134, 22);
            label17.TabIndex = 6;
            label17.Text = "Target ROI (%)";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label18.ForeColor = Color.FromArgb(156, 163, 175);
            label18.Location = new Point(24, 21);
            label18.Name = "label18";
            label18.Size = new Size(139, 22);
            label18.TabIndex = 5;
            label18.Text = "Target Profit ($)";
            // 
            // pnlLiquidation
            // 
            pnlLiquidation.BackColor = Color.FromArgb(30, 58, 95);
            pnlLiquidation.Controls.Add(rtbLiqInfo);
            pnlLiquidation.Controls.Add(txtWalletBalance);
            pnlLiquidation.Controls.Add(cmbMarginMode);
            pnlLiquidation.Controls.Add(lblLiqPriceValue);
            pnlLiquidation.Controls.Add(label12);
            pnlLiquidation.Controls.Add(lblWalletBalance);
            pnlLiquidation.Controls.Add(label9);
            pnlLiquidation.Location = new Point(339, 89);
            pnlLiquidation.Name = "pnlLiquidation";
            pnlLiquidation.Size = new Size(282, 386);
            pnlLiquidation.TabIndex = 10;
            pnlLiquidation.Visible = false;
            // 
            // rtbLiqInfo
            // 
            rtbLiqInfo.BackColor = Color.FromArgb(27, 38, 59);
            rtbLiqInfo.BorderStyle = BorderStyle.None;
            rtbLiqInfo.ForeColor = SystemColors.Window;
            rtbLiqInfo.Location = new Point(33, 289);
            rtbLiqInfo.Name = "rtbLiqInfo";
            rtbLiqInfo.ReadOnly = true;
            rtbLiqInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbLiqInfo.Size = new Size(212, 79);
            rtbLiqInfo.TabIndex = 13;
            rtbLiqInfo.Text = "";
            // 
            // txtWalletBalance
            // 
            txtWalletBalance.BackColor = Color.FromArgb(13, 27, 42);
            txtWalletBalance.BorderStyle = BorderStyle.None;
            txtWalletBalance.Font = new Font("Times New Roman", 13.8F);
            txtWalletBalance.ForeColor = SystemColors.Window;
            txtWalletBalance.Location = new Point(24, 143);
            txtWalletBalance.Name = "txtWalletBalance";
            txtWalletBalance.Size = new Size(203, 27);
            txtWalletBalance.TabIndex = 12;
            txtWalletBalance.Text = "   ";
            // 
            // cmbMarginMode
            // 
            cmbMarginMode.BackColor = Color.FromArgb(30, 58, 95);
            cmbMarginMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarginMode.FlatStyle = FlatStyle.Flat;
            cmbMarginMode.Font = new Font("Times New Roman", 13.8F);
            cmbMarginMode.FormattingEnabled = true;
            cmbMarginMode.Items.AddRange(new object[] { "Isolated", "Cross" });
            cmbMarginMode.Location = new Point(24, 49);
            cmbMarginMode.Name = "cmbMarginMode";
            cmbMarginMode.Size = new Size(202, 34);
            cmbMarginMode.TabIndex = 10;
            cmbMarginMode.SelectedIndexChanged += cmbMarginMode_SelectedIndexChanged;
            // 
            // lblLiqPriceValue
            // 
            lblLiqPriceValue.AutoSize = true;
            lblLiqPriceValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLiqPriceValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblLiqPriceValue.Location = new Point(24, 255);
            lblLiqPriceValue.Name = "lblLiqPriceValue";
            lblLiqPriceValue.Size = new Size(24, 22);
            lblLiqPriceValue.TabIndex = 8;
            lblLiqPriceValue.Text = "--";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.FromArgb(156, 163, 175);
            label12.Location = new Point(23, 215);
            label12.Name = "label12";
            label12.Size = new Size(237, 22);
            label12.TabIndex = 7;
            label12.Text = "Estimated Liquidation Price:";
            // 
            // lblWalletBalance
            // 
            lblWalletBalance.AutoSize = true;
            lblWalletBalance.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWalletBalance.ForeColor = Color.FromArgb(156, 163, 175);
            lblWalletBalance.Location = new Point(24, 114);
            lblWalletBalance.Name = "lblWalletBalance";
            lblWalletBalance.Size = new Size(199, 22);
            lblWalletBalance.TabIndex = 6;
            lblWalletBalance.Text = "Wallet Balance (USDT)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(156, 163, 175);
            label9.Location = new Point(24, 21);
            label9.Name = "label9";
            label9.Size = new Size(118, 22);
            label9.TabIndex = 5;
            label9.Text = "Margin Mode";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(156, 163, 175);
            label10.Location = new Point(25, 351);
            label10.Name = "label10";
            label10.Size = new Size(93, 22);
            label10.TabIndex = 17;
            label10.Text = "Max Loss:";
            // 
            // lblMaxLoss
            // 
            lblMaxLoss.AutoSize = true;
            lblMaxLoss.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMaxLoss.ForeColor = Color.FromArgb(156, 163, 175);
            lblMaxLoss.Location = new Point(128, 351);
            lblMaxLoss.Name = "lblMaxLoss";
            lblMaxLoss.Size = new Size(24, 22);
            lblMaxLoss.TabIndex = 18;
            lblMaxLoss.Text = "--";
            // 
            // FrmCalculator
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 38, 59);
            ClientSize = new Size(636, 619);
            Controls.Add(pnlButtons);
            Controls.Add(pnlSharedInputs);
            Controls.Add(panel1);
            Controls.Add(pnlPnlRr);
            Controls.Add(pnlLiquidation);
            Controls.Add(pnlTargetPrice);
            Name = "FrmCalculator";
            StartPosition = FormStartPosition.CenterScreen;
            Load += FrmCalculator_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlSharedInputs.ResumeLayout(false);
            pnlSharedInputs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLeverage).EndInit();
            pnlButtons.ResumeLayout(false);
            pnlPnlRr.ResumeLayout(false);
            pnlPnlRr.PerformLayout();
            pnlTargetPrice.ResumeLayout(false);
            pnlTargetPrice.PerformLayout();
            pnlLiquidation.ResumeLayout(false);
            pnlLiquidation.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RadioButton rbModePnl;
        private RadioButton rbModeLiquidation;
        private RadioButton rbModeTargetPrice;
        private Panel panel1;
        private Panel pnlSharedInputs;
        private Panel pnlButtons;
        private Panel pnlPnlRr;
        private Label label5;
        private ComboBox cmbDirection;
        private Label lblLeverageValue;
        private TrackBar trackBarLeverage;
        private Label label2;
        private Label label1;
        private TextBox txtMargin;
        private TextBox txtEntryPrice;
        private Label lblPositionSizeValue;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label4;
        private Label label3;
        private TextBox txtPnlExitPrice;
        private TextBox txtPnlStopLoss;
        private Label lblRewardValue;
        private Label lblRrValue;
        private Label label11;
        private Label label13;
        private Label lblRiskValue;
        private FontAwesome.Sharp.IconButton btnReset;
        private FontAwesome.Sharp.IconButton btnCopyResults;
        private FontAwesome.Sharp.IconButton btnCalculate;
        private Panel pnlLiquidation;
        private Label lblLiqPriceValue;
        private Label label12;
        private Label lblWalletBalance;
        private Label label9;
        private ComboBox cmbMarginMode;
        private TextBox txtWalletBalance;
        private Panel pnlTargetPrice;
        private TextBox txtTargetRoi;
        private Label lblTargetPriceValue;
        private Label label16;
        private Label label17;
        private Label label18;
        private TextBox txtTargetPnl;
        private TextBox txtLeverage;
        private RichTextBox rtbLiqInfo;
        private Label lblMaxLoss;
        private Label label10;
    }
}