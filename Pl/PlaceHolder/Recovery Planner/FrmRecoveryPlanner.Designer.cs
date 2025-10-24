namespace TradingJournal.Pl.PlaceHolder.Recovery_Planner
{
    partial class FrmRecoveryPlanner
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
            pnlCases = new Panel();
            dgvRecoveryCases = new DataGridView();
            btnRcWriteOff = new FontAwesome.Sharp.IconButton();
            btnRcCloseCase = new FontAwesome.Sharp.IconButton();
            btnRcPauseResumeCase = new FontAwesome.Sharp.IconButton();
            cbRcStatusFilter = new ComboBox();
            lblRcHint = new Label();
            pnlCaseEditor = new Panel();
            btnRcClear = new FontAwesome.Sharp.IconButton();
            btnRcCancelUpdate = new FontAwesome.Sharp.IconButton();
            btnRcUpdateCase = new FontAwesome.Sharp.IconButton();
            btnRcAddCase = new FontAwesome.Sharp.IconButton();
            lblRcNeeded = new Label();
            lblRcNeededCaption = new Label();
            lblRcUnrealized = new Label();
            lblRcUnrealizedCaption = new Label();
            lblRcCurrentValue = new Label();
            lblRcCurrentValueCaption = new Label();
            lblRcCurrentPrice = new Label();
            lblRcCurrentPriceCaption = new Label();
            txtRcInvestedUSDT = new TextBox();
            lblRcInvested = new Label();
            grpRcAmountMode = new GroupBox();
            rbRcModeQuantity = new RadioButton();
            rbRcModeInvested = new RadioButton();
            txtRcEntryPrice = new TextBox();
            lblRcEntryPrice = new Label();
            lblRcEntryDate = new Label();
            dateTimePicker1 = new DateTimePicker();
            lblRcCaseType = new Label();
            cbRcCaseType = new ComboBox();
            lblRcSymbol = new Label();
            cbRcSymbol = new ComboBox();
            txtRcQuantity = new TextBox();
            lblRcQuantity = new Label();
            pnlCaseEditor_Max = new Panel();
            pnlCases_Max = new Panel();
            pnlCases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecoveryCases).BeginInit();
            pnlCaseEditor.SuspendLayout();
            grpRcAmountMode.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCases
            // 
            pnlCases.BackColor = Color.FromArgb(27, 38, 59);
            pnlCases.Controls.Add(dgvRecoveryCases);
            pnlCases.Controls.Add(btnRcWriteOff);
            pnlCases.Controls.Add(btnRcCloseCase);
            pnlCases.Controls.Add(btnRcPauseResumeCase);
            pnlCases.Controls.Add(cbRcStatusFilter);
            pnlCases.Controls.Add(lblRcHint);
            pnlCases.Location = new Point(13, 384);
            pnlCases.Margin = new Padding(2, 3, 2, 3);
            pnlCases.Name = "pnlCases";
            pnlCases.Size = new Size(1067, 509);
            pnlCases.TabIndex = 20;
            // 
            // dgvRecoveryCases
            // 
            dgvRecoveryCases.AllowUserToAddRows = false;
            dgvRecoveryCases.AllowUserToDeleteRows = false;
            dgvRecoveryCases.AllowUserToResizeColumns = false;
            dgvRecoveryCases.AllowUserToResizeRows = false;
            dgvRecoveryCases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecoveryCases.Location = new Point(18, 56);
            dgvRecoveryCases.Margin = new Padding(3, 4, 3, 4);
            dgvRecoveryCases.Name = "dgvRecoveryCases";
            dgvRecoveryCases.ReadOnly = true;
            dgvRecoveryCases.RowHeadersWidth = 51;
            dgvRecoveryCases.Size = new Size(1030, 433);
            dgvRecoveryCases.TabIndex = 28;
            dgvRecoveryCases.CellContentClick += dgvRecoveryCases_CellContentClick;
            dgvRecoveryCases.CellDoubleClick += dgvRecoveryCases_CellDoubleClick;
            dgvRecoveryCases.SelectionChanged += dgvRecoveryCases_SelectionChanged;
            // 
            // btnRcWriteOff
            // 
            btnRcWriteOff.BackColor = Color.FromArgb(30, 58, 95);
            btnRcWriteOff.FlatAppearance.BorderSize = 0;
            btnRcWriteOff.FlatStyle = FlatStyle.Flat;
            btnRcWriteOff.Font = new Font("Times New Roman", 12F);
            btnRcWriteOff.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRcWriteOff.IconColor = Color.Black;
            btnRcWriteOff.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRcWriteOff.Location = new Point(894, 8);
            btnRcWriteOff.Margin = new Padding(2, 3, 2, 3);
            btnRcWriteOff.Name = "btnRcWriteOff";
            btnRcWriteOff.Size = new Size(154, 41);
            btnRcWriteOff.TabIndex = 27;
            btnRcWriteOff.Text = "Write-off";
            btnRcWriteOff.UseVisualStyleBackColor = false;
            btnRcWriteOff.Click += btnRcWriteOff_Click;
            // 
            // btnRcCloseCase
            // 
            btnRcCloseCase.BackColor = Color.FromArgb(30, 58, 95);
            btnRcCloseCase.FlatAppearance.BorderSize = 0;
            btnRcCloseCase.FlatStyle = FlatStyle.Flat;
            btnRcCloseCase.Font = new Font("Times New Roman", 12F);
            btnRcCloseCase.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRcCloseCase.IconColor = Color.Black;
            btnRcCloseCase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRcCloseCase.Location = new Point(717, 8);
            btnRcCloseCase.Margin = new Padding(2, 3, 2, 3);
            btnRcCloseCase.Name = "btnRcCloseCase";
            btnRcCloseCase.Size = new Size(154, 41);
            btnRcCloseCase.TabIndex = 26;
            btnRcCloseCase.Text = "Close Case";
            btnRcCloseCase.UseVisualStyleBackColor = false;
            btnRcCloseCase.Click += btnRcCloseCase_Click;
            // 
            // btnRcPauseResumeCase
            // 
            btnRcPauseResumeCase.BackColor = Color.FromArgb(30, 58, 95);
            btnRcPauseResumeCase.FlatAppearance.BorderSize = 0;
            btnRcPauseResumeCase.FlatStyle = FlatStyle.Flat;
            btnRcPauseResumeCase.Font = new Font("Times New Roman", 12F);
            btnRcPauseResumeCase.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRcPauseResumeCase.IconColor = Color.Black;
            btnRcPauseResumeCase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRcPauseResumeCase.Location = new Point(539, 7);
            btnRcPauseResumeCase.Margin = new Padding(2, 3, 2, 3);
            btnRcPauseResumeCase.Name = "btnRcPauseResumeCase";
            btnRcPauseResumeCase.Size = new Size(154, 41);
            btnRcPauseResumeCase.TabIndex = 25;
            btnRcPauseResumeCase.Text = "Pause / Resume";
            btnRcPauseResumeCase.UseVisualStyleBackColor = false;
            btnRcPauseResumeCase.Click += btnRcPauseResumeCase_Click;
            // 
            // cbRcStatusFilter
            // 
            cbRcStatusFilter.BackColor = Color.FromArgb(30, 58, 95);
            cbRcStatusFilter.FlatStyle = FlatStyle.Flat;
            cbRcStatusFilter.Font = new Font("Times New Roman", 13.8F);
            cbRcStatusFilter.FormattingEnabled = true;
            cbRcStatusFilter.Location = new Point(335, 11);
            cbRcStatusFilter.Margin = new Padding(2, 3, 2, 3);
            cbRcStatusFilter.Name = "cbRcStatusFilter";
            cbRcStatusFilter.Size = new Size(186, 34);
            cbRcStatusFilter.TabIndex = 18;
            cbRcStatusFilter.Text = "  ";
            cbRcStatusFilter.SelectedIndexChanged += cbRcStatusFilter_SelectedIndexChanged;
            // 
            // lblRcHint
            // 
            lblRcHint.AutoSize = true;
            lblRcHint.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcHint.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcHint.Location = new Point(16, 16);
            lblRcHint.Margin = new Padding(2, 0, 2, 0);
            lblRcHint.Name = "lblRcHint";
            lblRcHint.Size = new Size(351, 22);
            lblRcHint.TabIndex = 17;
            lblRcHint.Text = "Double-click a case to open its Allocations";
            // 
            // pnlCaseEditor
            // 
            pnlCaseEditor.BackColor = Color.FromArgb(27, 38, 59);
            pnlCaseEditor.Controls.Add(btnRcClear);
            pnlCaseEditor.Controls.Add(btnRcCancelUpdate);
            pnlCaseEditor.Controls.Add(btnRcUpdateCase);
            pnlCaseEditor.Controls.Add(btnRcAddCase);
            pnlCaseEditor.Controls.Add(lblRcNeeded);
            pnlCaseEditor.Controls.Add(lblRcNeededCaption);
            pnlCaseEditor.Controls.Add(lblRcUnrealized);
            pnlCaseEditor.Controls.Add(lblRcUnrealizedCaption);
            pnlCaseEditor.Controls.Add(lblRcCurrentValue);
            pnlCaseEditor.Controls.Add(lblRcCurrentValueCaption);
            pnlCaseEditor.Controls.Add(lblRcCurrentPrice);
            pnlCaseEditor.Controls.Add(lblRcCurrentPriceCaption);
            pnlCaseEditor.Controls.Add(txtRcInvestedUSDT);
            pnlCaseEditor.Controls.Add(lblRcInvested);
            pnlCaseEditor.Controls.Add(grpRcAmountMode);
            pnlCaseEditor.Controls.Add(txtRcEntryPrice);
            pnlCaseEditor.Controls.Add(lblRcEntryPrice);
            pnlCaseEditor.Controls.Add(lblRcEntryDate);
            pnlCaseEditor.Controls.Add(dateTimePicker1);
            pnlCaseEditor.Controls.Add(lblRcCaseType);
            pnlCaseEditor.Controls.Add(cbRcCaseType);
            pnlCaseEditor.Controls.Add(lblRcSymbol);
            pnlCaseEditor.Controls.Add(cbRcSymbol);
            pnlCaseEditor.Controls.Add(txtRcQuantity);
            pnlCaseEditor.Controls.Add(lblRcQuantity);
            pnlCaseEditor.Location = new Point(13, 15);
            pnlCaseEditor.Margin = new Padding(2, 3, 2, 3);
            pnlCaseEditor.Name = "pnlCaseEditor";
            pnlCaseEditor.Size = new Size(1067, 352);
            pnlCaseEditor.TabIndex = 19;
            pnlCaseEditor.Paint += pnlCaseEditor_Paint;
            // 
            // btnRcClear
            // 
            btnRcClear.BackColor = Color.IndianRed;
            btnRcClear.FlatAppearance.BorderSize = 0;
            btnRcClear.FlatStyle = FlatStyle.Flat;
            btnRcClear.Font = new Font("Times New Roman", 12F);
            btnRcClear.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRcClear.IconColor = Color.Black;
            btnRcClear.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRcClear.Location = new Point(539, 195);
            btnRcClear.Margin = new Padding(2, 3, 2, 3);
            btnRcClear.Name = "btnRcClear";
            btnRcClear.Size = new Size(174, 40);
            btnRcClear.TabIndex = 27;
            btnRcClear.Text = "Clear Fields";
            btnRcClear.UseVisualStyleBackColor = false;
            // 
            // btnRcCancelUpdate
            // 
            btnRcCancelUpdate.BackColor = Color.Crimson;
            btnRcCancelUpdate.FlatAppearance.BorderSize = 0;
            btnRcCancelUpdate.FlatStyle = FlatStyle.Flat;
            btnRcCancelUpdate.Font = new Font("Times New Roman", 12F);
            btnRcCancelUpdate.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRcCancelUpdate.IconColor = Color.Black;
            btnRcCancelUpdate.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRcCancelUpdate.Location = new Point(649, 163);
            btnRcCancelUpdate.Margin = new Padding(2, 3, 2, 3);
            btnRcCancelUpdate.Name = "btnRcCancelUpdate";
            btnRcCancelUpdate.Size = new Size(120, 52);
            btnRcCancelUpdate.TabIndex = 26;
            btnRcCancelUpdate.Text = "Cancel";
            btnRcCancelUpdate.UseVisualStyleBackColor = false;
            btnRcCancelUpdate.Visible = false;
            // 
            // btnRcUpdateCase
            // 
            btnRcUpdateCase.BackColor = Color.DarkGreen;
            btnRcUpdateCase.FlatAppearance.BorderSize = 0;
            btnRcUpdateCase.FlatStyle = FlatStyle.Flat;
            btnRcUpdateCase.Font = new Font("Times New Roman", 12F);
            btnRcUpdateCase.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRcUpdateCase.IconColor = Color.Black;
            btnRcUpdateCase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRcUpdateCase.Location = new Point(502, 163);
            btnRcUpdateCase.Margin = new Padding(2, 3, 2, 3);
            btnRcUpdateCase.Name = "btnRcUpdateCase";
            btnRcUpdateCase.Size = new Size(120, 52);
            btnRcUpdateCase.TabIndex = 25;
            btnRcUpdateCase.Text = "Update Case";
            btnRcUpdateCase.UseVisualStyleBackColor = false;
            btnRcUpdateCase.Visible = false;
            // 
            // btnRcAddCase
            // 
            btnRcAddCase.BackColor = Color.FromArgb(30, 58, 95);
            btnRcAddCase.FlatAppearance.BorderSize = 0;
            btnRcAddCase.FlatStyle = FlatStyle.Flat;
            btnRcAddCase.Font = new Font("Times New Roman", 12F);
            btnRcAddCase.IconChar = FontAwesome.Sharp.IconChar.None;
            btnRcAddCase.IconColor = Color.Black;
            btnRcAddCase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRcAddCase.Location = new Point(528, 127);
            btnRcAddCase.Margin = new Padding(2, 3, 2, 3);
            btnRcAddCase.Name = "btnRcAddCase";
            btnRcAddCase.Size = new Size(200, 52);
            btnRcAddCase.TabIndex = 24;
            btnRcAddCase.Text = "Add Case";
            btnRcAddCase.UseVisualStyleBackColor = false;
            // 
            // lblRcNeeded
            // 
            lblRcNeeded.AutoSize = true;
            lblRcNeeded.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcNeeded.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcNeeded.Location = new Point(487, 307);
            lblRcNeeded.Margin = new Padding(2, 0, 2, 0);
            lblRcNeeded.Name = "lblRcNeeded";
            lblRcNeeded.Size = new Size(24, 22);
            lblRcNeeded.TabIndex = 23;
            lblRcNeeded.Text = "--";
            // 
            // lblRcNeededCaption
            // 
            lblRcNeededCaption.AutoSize = true;
            lblRcNeededCaption.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcNeededCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcNeededCaption.Location = new Point(487, 265);
            lblRcNeededCaption.Margin = new Padding(2, 0, 2, 0);
            lblRcNeededCaption.Name = "lblRcNeededCaption";
            lblRcNeededCaption.Size = new Size(188, 22);
            lblRcNeededCaption.TabIndex = 22;
            lblRcNeededCaption.Text = "Needed to Break Even";
            // 
            // lblRcUnrealized
            // 
            lblRcUnrealized.AutoSize = true;
            lblRcUnrealized.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcUnrealized.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcUnrealized.Location = new Point(335, 307);
            lblRcUnrealized.Margin = new Padding(2, 0, 2, 0);
            lblRcUnrealized.Name = "lblRcUnrealized";
            lblRcUnrealized.Size = new Size(24, 22);
            lblRcUnrealized.TabIndex = 21;
            lblRcUnrealized.Text = "--";
            // 
            // lblRcUnrealizedCaption
            // 
            lblRcUnrealizedCaption.AutoSize = true;
            lblRcUnrealizedCaption.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcUnrealizedCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcUnrealizedCaption.Location = new Point(335, 265);
            lblRcUnrealizedCaption.Margin = new Padding(2, 0, 2, 0);
            lblRcUnrealizedCaption.Name = "lblRcUnrealizedCaption";
            lblRcUnrealizedCaption.Size = new Size(129, 22);
            lblRcUnrealizedCaption.TabIndex = 20;
            lblRcUnrealizedCaption.Text = "Unrealized P/L";
            // 
            // lblRcCurrentValue
            // 
            lblRcCurrentValue.AutoSize = true;
            lblRcCurrentValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentValue.Location = new Point(178, 307);
            lblRcCurrentValue.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentValue.Name = "lblRcCurrentValue";
            lblRcCurrentValue.Size = new Size(24, 22);
            lblRcCurrentValue.TabIndex = 19;
            lblRcCurrentValue.Text = "--";
            // 
            // lblRcCurrentValueCaption
            // 
            lblRcCurrentValueCaption.AutoSize = true;
            lblRcCurrentValueCaption.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentValueCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentValueCaption.Location = new Point(178, 265);
            lblRcCurrentValueCaption.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentValueCaption.Name = "lblRcCurrentValueCaption";
            lblRcCurrentValueCaption.Size = new Size(118, 22);
            lblRcCurrentValueCaption.TabIndex = 18;
            lblRcCurrentValueCaption.Text = "Current Value";
            // 
            // lblRcCurrentPrice
            // 
            lblRcCurrentPrice.AutoSize = true;
            lblRcCurrentPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentPrice.Location = new Point(22, 307);
            lblRcCurrentPrice.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentPrice.Name = "lblRcCurrentPrice";
            lblRcCurrentPrice.Size = new Size(24, 22);
            lblRcCurrentPrice.TabIndex = 17;
            lblRcCurrentPrice.Text = "--";
            // 
            // lblRcCurrentPriceCaption
            // 
            lblRcCurrentPriceCaption.AutoSize = true;
            lblRcCurrentPriceCaption.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentPriceCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentPriceCaption.Location = new Point(22, 265);
            lblRcCurrentPriceCaption.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentPriceCaption.Name = "lblRcCurrentPriceCaption";
            lblRcCurrentPriceCaption.Size = new Size(116, 22);
            lblRcCurrentPriceCaption.TabIndex = 16;
            lblRcCurrentPriceCaption.Text = "Current Price";
            // 
            // txtRcInvestedUSDT
            // 
            txtRcInvestedUSDT.BackColor = Color.FromArgb(30, 58, 95);
            txtRcInvestedUSDT.BorderStyle = BorderStyle.None;
            txtRcInvestedUSDT.Font = new Font("Times New Roman", 13.8F);
            txtRcInvestedUSDT.Location = new Point(224, 185);
            txtRcInvestedUSDT.Margin = new Padding(2, 3, 2, 3);
            txtRcInvestedUSDT.Name = "txtRcInvestedUSDT";
            txtRcInvestedUSDT.Size = new Size(185, 27);
            txtRcInvestedUSDT.TabIndex = 13;
            txtRcInvestedUSDT.Text = "   ";
            // 
            // lblRcInvested
            // 
            lblRcInvested.AutoSize = true;
            lblRcInvested.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcInvested.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcInvested.Location = new Point(224, 153);
            lblRcInvested.Margin = new Padding(2, 0, 2, 0);
            lblRcInvested.Name = "lblRcInvested";
            lblRcInvested.Size = new Size(145, 22);
            lblRcInvested.TabIndex = 12;
            lblRcInvested.Text = "Invested (USDT)";
            // 
            // grpRcAmountMode
            // 
            grpRcAmountMode.Controls.Add(rbRcModeQuantity);
            grpRcAmountMode.Controls.Add(rbRcModeInvested);
            grpRcAmountMode.ForeColor = Color.FromArgb(156, 163, 175);
            grpRcAmountMode.Location = new Point(16, 125);
            grpRcAmountMode.Margin = new Padding(3, 4, 3, 4);
            grpRcAmountMode.Name = "grpRcAmountMode";
            grpRcAmountMode.Padding = new Padding(3, 4, 3, 4);
            grpRcAmountMode.Size = new Size(186, 113);
            grpRcAmountMode.TabIndex = 11;
            grpRcAmountMode.TabStop = false;
            grpRcAmountMode.Text = "Amount Entered";
            // 
            // rbRcModeQuantity
            // 
            rbRcModeQuantity.AutoSize = true;
            rbRcModeQuantity.Location = new Point(17, 63);
            rbRcModeQuantity.Margin = new Padding(3, 4, 3, 4);
            rbRcModeQuantity.Name = "rbRcModeQuantity";
            rbRcModeQuantity.Size = new Size(131, 24);
            rbRcModeQuantity.TabIndex = 1;
            rbRcModeQuantity.Text = "Quantity (Base)";
            rbRcModeQuantity.UseVisualStyleBackColor = true;
            // 
            // rbRcModeInvested
            // 
            rbRcModeInvested.AutoSize = true;
            rbRcModeInvested.Checked = true;
            rbRcModeInvested.Location = new Point(17, 29);
            rbRcModeInvested.Margin = new Padding(3, 4, 3, 4);
            rbRcModeInvested.Name = "rbRcModeInvested";
            rbRcModeInvested.Size = new Size(135, 24);
            rbRcModeInvested.TabIndex = 0;
            rbRcModeInvested.TabStop = true;
            rbRcModeInvested.Text = "Invested (USDT)";
            rbRcModeInvested.UseVisualStyleBackColor = true;
            // 
            // txtRcEntryPrice
            // 
            txtRcEntryPrice.BackColor = Color.FromArgb(30, 58, 95);
            txtRcEntryPrice.BorderStyle = BorderStyle.None;
            txtRcEntryPrice.Font = new Font("Times New Roman", 13.8F);
            txtRcEntryPrice.Location = new Point(567, 55);
            txtRcEntryPrice.Margin = new Padding(2, 3, 2, 3);
            txtRcEntryPrice.Name = "txtRcEntryPrice";
            txtRcEntryPrice.Size = new Size(185, 27);
            txtRcEntryPrice.TabIndex = 10;
            txtRcEntryPrice.Text = "   ";
            // 
            // lblRcEntryPrice
            // 
            lblRcEntryPrice.AutoSize = true;
            lblRcEntryPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcEntryPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcEntryPrice.Location = new Point(567, 23);
            lblRcEntryPrice.Margin = new Padding(2, 0, 2, 0);
            lblRcEntryPrice.Name = "lblRcEntryPrice";
            lblRcEntryPrice.Size = new Size(99, 22);
            lblRcEntryPrice.TabIndex = 9;
            lblRcEntryPrice.Text = "Entry Price";
            // 
            // lblRcEntryDate
            // 
            lblRcEntryDate.AutoSize = true;
            lblRcEntryDate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcEntryDate.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcEntryDate.Location = new Point(429, 20);
            lblRcEntryDate.Margin = new Padding(2, 0, 2, 0);
            lblRcEntryDate.Name = "lblRcEntryDate";
            lblRcEntryDate.Size = new Size(94, 22);
            lblRcEntryDate.TabIndex = 7;
            lblRcEntryDate.Text = "Entry Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(429, 49);
            dateTimePicker1.Margin = new Padding(3, 4, 3, 4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(122, 30);
            dateTimePicker1.TabIndex = 6;
            // 
            // lblRcCaseType
            // 
            lblRcCaseType.AutoSize = true;
            lblRcCaseType.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCaseType.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCaseType.Location = new Point(224, 20);
            lblRcCaseType.Margin = new Padding(2, 0, 2, 0);
            lblRcCaseType.Name = "lblRcCaseType";
            lblRcCaseType.Size = new Size(93, 22);
            lblRcCaseType.TabIndex = 5;
            lblRcCaseType.Text = "Case Type";
            // 
            // cbRcCaseType
            // 
            cbRcCaseType.BackColor = Color.FromArgb(30, 58, 95);
            cbRcCaseType.FlatStyle = FlatStyle.Flat;
            cbRcCaseType.Font = new Font("Times New Roman", 13.8F);
            cbRcCaseType.FormattingEnabled = true;
            cbRcCaseType.Location = new Point(224, 49);
            cbRcCaseType.Margin = new Padding(2, 3, 2, 3);
            cbRcCaseType.Name = "cbRcCaseType";
            cbRcCaseType.Size = new Size(186, 34);
            cbRcCaseType.TabIndex = 4;
            cbRcCaseType.Text = "  ";
            // 
            // lblRcSymbol
            // 
            lblRcSymbol.AutoSize = true;
            lblRcSymbol.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcSymbol.Location = new Point(16, 20);
            lblRcSymbol.Margin = new Padding(2, 0, 2, 0);
            lblRcSymbol.Name = "lblRcSymbol";
            lblRcSymbol.Size = new Size(70, 22);
            lblRcSymbol.TabIndex = 3;
            lblRcSymbol.Text = "Symbol";
            // 
            // cbRcSymbol
            // 
            cbRcSymbol.BackColor = Color.FromArgb(30, 58, 95);
            cbRcSymbol.FlatStyle = FlatStyle.Flat;
            cbRcSymbol.Font = new Font("Times New Roman", 13.8F);
            cbRcSymbol.FormattingEnabled = true;
            cbRcSymbol.Location = new Point(16, 49);
            cbRcSymbol.Margin = new Padding(2, 3, 2, 3);
            cbRcSymbol.Name = "cbRcSymbol";
            cbRcSymbol.Size = new Size(186, 34);
            cbRcSymbol.TabIndex = 2;
            cbRcSymbol.Text = "  ";
            // 
            // txtRcQuantity
            // 
            txtRcQuantity.BackColor = Color.FromArgb(30, 58, 95);
            txtRcQuantity.BorderStyle = BorderStyle.None;
            txtRcQuantity.Font = new Font("Times New Roman", 13.8F);
            txtRcQuantity.Location = new Point(224, 185);
            txtRcQuantity.Margin = new Padding(2, 3, 2, 3);
            txtRcQuantity.Name = "txtRcQuantity";
            txtRcQuantity.Size = new Size(185, 27);
            txtRcQuantity.TabIndex = 15;
            txtRcQuantity.Text = "   ";
            txtRcQuantity.Visible = false;
            // 
            // lblRcQuantity
            // 
            lblRcQuantity.AutoSize = true;
            lblRcQuantity.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcQuantity.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcQuantity.Location = new Point(224, 153);
            lblRcQuantity.Margin = new Padding(2, 0, 2, 0);
            lblRcQuantity.Name = "lblRcQuantity";
            lblRcQuantity.Size = new Size(76, 22);
            lblRcQuantity.TabIndex = 14;
            lblRcQuantity.Text = "Quantity";
            lblRcQuantity.Visible = false;
            // 
            // pnlCaseEditor_Max
            // 
            pnlCaseEditor_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlCaseEditor_Max.Location = new Point(13, 15);
            pnlCaseEditor_Max.Margin = new Padding(2, 3, 2, 3);
            pnlCaseEditor_Max.Name = "pnlCaseEditor_Max";
            pnlCaseEditor_Max.Size = new Size(1901, 509);
            pnlCaseEditor_Max.TabIndex = 21;
            pnlCaseEditor_Max.Visible = false;
            // 
            // pnlCases_Max
            // 
            pnlCases_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlCases_Max.Location = new Point(13, 592);
            pnlCases_Max.Margin = new Padding(2, 3, 2, 3);
            pnlCases_Max.Name = "pnlCases_Max";
            pnlCases_Max.Size = new Size(1901, 703);
            pnlCases_Max.TabIndex = 22;
            pnlCases_Max.Visible = false;
            // 
            // FrmRecoveryPlanner
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1942, 1102);
            Controls.Add(pnlCases);
            Controls.Add(pnlCaseEditor);
            Controls.Add(pnlCaseEditor_Max);
            Controls.Add(pnlCases_Max);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmRecoveryPlanner";
            Text = "FrmRecoveryPlanner";
            pnlCases.ResumeLayout(false);
            pnlCases.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecoveryCases).EndInit();
            pnlCaseEditor.ResumeLayout(false);
            pnlCaseEditor.PerformLayout();
            grpRcAmountMode.ResumeLayout(false);
            grpRcAmountMode.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCases;
        private Panel pnlCaseEditor;
        private Label lblRcSymbol;
        private ComboBox cbRcSymbol;
        private Label lblRcCaseType;
        private ComboBox cbRcCaseType;
        private Label lblRcEntryDate;
        private DateTimePicker dateTimePicker1;
        private Label lblRcEntryPrice;
        private TextBox txtRcEntryPrice;
        private GroupBox grpRcAmountMode;
        private RadioButton rbRcModeQuantity;
        private RadioButton rbRcModeInvested;
        private TextBox txtRcQuantity;
        private Label lblRcQuantity;
        private TextBox txtRcInvestedUSDT;
        private Label lblRcInvested;
        private Label lblRcNeeded;
        private Label lblRcNeededCaption;
        private Label lblRcUnrealized;
        private Label lblRcUnrealizedCaption;
        private Label lblRcCurrentValue;
        private Label lblRcCurrentValueCaption;
        private Label lblRcCurrentPrice;
        private Label lblRcCurrentPriceCaption;
        private FontAwesome.Sharp.IconButton btnRcClear;
        private FontAwesome.Sharp.IconButton btnRcCancelUpdate;
        private FontAwesome.Sharp.IconButton btnRcUpdateCase;
        private FontAwesome.Sharp.IconButton btnRcAddCase;
        private Label lblRcHint;
        private FontAwesome.Sharp.IconButton btnRcWriteOff;
        private FontAwesome.Sharp.IconButton btnRcCloseCase;
        private FontAwesome.Sharp.IconButton btnRcPauseResumeCase;
        private ComboBox cbRcStatusFilter;
        private DataGridView dgvRecoveryCases;
        private Panel pnlCaseEditor_Max;
        private Panel pnlCases_Max;
    }
}