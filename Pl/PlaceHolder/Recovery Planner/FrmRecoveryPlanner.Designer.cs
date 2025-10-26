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
            dtpEntryDate = new DateTimePicker();
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
            pnlCases.Location = new Point(11, 288);
            pnlCases.Margin = new Padding(2);
            pnlCases.Name = "pnlCases";
            pnlCases.Size = new Size(934, 382);
            pnlCases.TabIndex = 20;
            // 
            // dgvRecoveryCases
            // 
            dgvRecoveryCases.AllowUserToAddRows = false;
            dgvRecoveryCases.AllowUserToDeleteRows = false;
            dgvRecoveryCases.AllowUserToResizeColumns = false;
            dgvRecoveryCases.AllowUserToResizeRows = false;
            dgvRecoveryCases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecoveryCases.Location = new Point(16, 42);
            dgvRecoveryCases.Name = "dgvRecoveryCases";
            dgvRecoveryCases.ReadOnly = true;
            dgvRecoveryCases.RowHeadersWidth = 51;
            dgvRecoveryCases.Size = new Size(901, 325);
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
            btnRcWriteOff.Location = new Point(782, 6);
            btnRcWriteOff.Margin = new Padding(2);
            btnRcWriteOff.Name = "btnRcWriteOff";
            btnRcWriteOff.Size = new Size(135, 31);
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
            btnRcCloseCase.Location = new Point(627, 6);
            btnRcCloseCase.Margin = new Padding(2);
            btnRcCloseCase.Name = "btnRcCloseCase";
            btnRcCloseCase.Size = new Size(135, 31);
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
            btnRcPauseResumeCase.Location = new Point(472, 5);
            btnRcPauseResumeCase.Margin = new Padding(2);
            btnRcPauseResumeCase.Name = "btnRcPauseResumeCase";
            btnRcPauseResumeCase.Size = new Size(135, 31);
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
            cbRcStatusFilter.Location = new Point(293, 8);
            cbRcStatusFilter.Margin = new Padding(2);
            cbRcStatusFilter.Name = "cbRcStatusFilter";
            cbRcStatusFilter.Size = new Size(163, 28);
            cbRcStatusFilter.TabIndex = 18;
            cbRcStatusFilter.Text = "  ";
            cbRcStatusFilter.SelectedIndexChanged += cbRcStatusFilter_SelectedIndexChanged;
            // 
            // lblRcHint
            // 
            lblRcHint.AutoSize = true;
            lblRcHint.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcHint.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcHint.Location = new Point(14, 12);
            lblRcHint.Margin = new Padding(2, 0, 2, 0);
            lblRcHint.Name = "lblRcHint";
            lblRcHint.Size = new Size(266, 19);
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
            pnlCaseEditor.Controls.Add(dtpEntryDate);
            pnlCaseEditor.Controls.Add(lblRcSymbol);
            pnlCaseEditor.Controls.Add(cbRcSymbol);
            pnlCaseEditor.Controls.Add(txtRcQuantity);
            pnlCaseEditor.Controls.Add(lblRcQuantity);
            pnlCaseEditor.Location = new Point(11, 11);
            pnlCaseEditor.Margin = new Padding(2);
            pnlCaseEditor.Name = "pnlCaseEditor";
            pnlCaseEditor.Size = new Size(934, 264);
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
            btnRcClear.Location = new Point(455, 141);
            btnRcClear.Margin = new Padding(2);
            btnRcClear.Name = "btnRcClear";
            btnRcClear.Size = new Size(152, 30);
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
            btnRcCancelUpdate.Location = new Point(551, 117);
            btnRcCancelUpdate.Margin = new Padding(2);
            btnRcCancelUpdate.Name = "btnRcCancelUpdate";
            btnRcCancelUpdate.Size = new Size(105, 39);
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
            btnRcUpdateCase.Location = new Point(422, 117);
            btnRcUpdateCase.Margin = new Padding(2);
            btnRcUpdateCase.Name = "btnRcUpdateCase";
            btnRcUpdateCase.Size = new Size(105, 39);
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
            btnRcAddCase.Location = new Point(445, 90);
            btnRcAddCase.Margin = new Padding(2);
            btnRcAddCase.Name = "btnRcAddCase";
            btnRcAddCase.Size = new Size(175, 39);
            btnRcAddCase.TabIndex = 24;
            btnRcAddCase.Text = "Add Case";
            btnRcAddCase.UseVisualStyleBackColor = false;
            // 
            // lblRcUnrealized
            // 
            lblRcUnrealized.AutoSize = true;
            lblRcUnrealized.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcUnrealized.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcUnrealized.Location = new Point(293, 230);
            lblRcUnrealized.Margin = new Padding(2, 0, 2, 0);
            lblRcUnrealized.Name = "lblRcUnrealized";
            lblRcUnrealized.Size = new Size(21, 19);
            lblRcUnrealized.TabIndex = 21;
            lblRcUnrealized.Text = "--";
            // 
            // lblRcUnrealizedCaption
            // 
            lblRcUnrealizedCaption.AutoSize = true;
            lblRcUnrealizedCaption.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcUnrealizedCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcUnrealizedCaption.Location = new Point(293, 199);
            lblRcUnrealizedCaption.Margin = new Padding(2, 0, 2, 0);
            lblRcUnrealizedCaption.Name = "lblRcUnrealizedCaption";
            lblRcUnrealizedCaption.Size = new Size(99, 19);
            lblRcUnrealizedCaption.TabIndex = 20;
            lblRcUnrealizedCaption.Text = "Unrealized P/L";
            // 
            // lblRcCurrentValue
            // 
            lblRcCurrentValue.AutoSize = true;
            lblRcCurrentValue.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentValue.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentValue.Location = new Point(156, 230);
            lblRcCurrentValue.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentValue.Name = "lblRcCurrentValue";
            lblRcCurrentValue.Size = new Size(21, 19);
            lblRcCurrentValue.TabIndex = 19;
            lblRcCurrentValue.Text = "--";
            // 
            // lblRcCurrentValueCaption
            // 
            lblRcCurrentValueCaption.AutoSize = true;
            lblRcCurrentValueCaption.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentValueCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentValueCaption.Location = new Point(156, 199);
            lblRcCurrentValueCaption.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentValueCaption.Name = "lblRcCurrentValueCaption";
            lblRcCurrentValueCaption.Size = new Size(92, 19);
            lblRcCurrentValueCaption.TabIndex = 18;
            lblRcCurrentValueCaption.Text = "Current Value";
            // 
            // lblRcCurrentPrice
            // 
            lblRcCurrentPrice.AutoSize = true;
            lblRcCurrentPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentPrice.Location = new Point(19, 230);
            lblRcCurrentPrice.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentPrice.Name = "lblRcCurrentPrice";
            lblRcCurrentPrice.Size = new Size(21, 19);
            lblRcCurrentPrice.TabIndex = 17;
            lblRcCurrentPrice.Text = "--";
            // 
            // lblRcCurrentPriceCaption
            // 
            lblRcCurrentPriceCaption.AutoSize = true;
            lblRcCurrentPriceCaption.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcCurrentPriceCaption.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcCurrentPriceCaption.Location = new Point(19, 199);
            lblRcCurrentPriceCaption.Margin = new Padding(2, 0, 2, 0);
            lblRcCurrentPriceCaption.Name = "lblRcCurrentPriceCaption";
            lblRcCurrentPriceCaption.Size = new Size(90, 19);
            lblRcCurrentPriceCaption.TabIndex = 16;
            lblRcCurrentPriceCaption.Text = "Current Price";
            // 
            // txtRcInvestedUSDT
            // 
            txtRcInvestedUSDT.BackColor = Color.FromArgb(30, 58, 95);
            txtRcInvestedUSDT.BorderStyle = BorderStyle.None;
            txtRcInvestedUSDT.Font = new Font("Times New Roman", 13.8F);
            txtRcInvestedUSDT.Location = new Point(199, 131);
            txtRcInvestedUSDT.Margin = new Padding(2);
            txtRcInvestedUSDT.Name = "txtRcInvestedUSDT";
            txtRcInvestedUSDT.Size = new Size(162, 22);
            txtRcInvestedUSDT.TabIndex = 13;
            txtRcInvestedUSDT.Text = "   ";
            // 
            // lblRcInvested
            // 
            lblRcInvested.AutoSize = true;
            lblRcInvested.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcInvested.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcInvested.Location = new Point(199, 107);
            lblRcInvested.Margin = new Padding(2, 0, 2, 0);
            lblRcInvested.Name = "lblRcInvested";
            lblRcInvested.Size = new Size(114, 19);
            lblRcInvested.TabIndex = 12;
            lblRcInvested.Text = "Invested (USDT)";
            // 
            // grpRcAmountMode
            // 
            grpRcAmountMode.Controls.Add(rbRcModeQuantity);
            grpRcAmountMode.Controls.Add(rbRcModeInvested);
            grpRcAmountMode.ForeColor = Color.FromArgb(156, 163, 175);
            grpRcAmountMode.Location = new Point(14, 94);
            grpRcAmountMode.Name = "grpRcAmountMode";
            grpRcAmountMode.Size = new Size(163, 85);
            grpRcAmountMode.TabIndex = 11;
            grpRcAmountMode.TabStop = false;
            grpRcAmountMode.Text = "Amount Entered";
            // 
            // rbRcModeQuantity
            // 
            rbRcModeQuantity.AutoSize = true;
            rbRcModeQuantity.Location = new Point(15, 47);
            rbRcModeQuantity.Name = "rbRcModeQuantity";
            rbRcModeQuantity.Size = new Size(106, 19);
            rbRcModeQuantity.TabIndex = 1;
            rbRcModeQuantity.Text = "Quantity (Base)";
            rbRcModeQuantity.UseVisualStyleBackColor = true;
            // 
            // rbRcModeInvested
            // 
            rbRcModeInvested.AutoSize = true;
            rbRcModeInvested.Checked = true;
            rbRcModeInvested.Location = new Point(15, 22);
            rbRcModeInvested.Name = "rbRcModeInvested";
            rbRcModeInvested.Size = new Size(108, 19);
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
            txtRcEntryPrice.Location = new Point(392, 40);
            txtRcEntryPrice.Margin = new Padding(2);
            txtRcEntryPrice.Name = "txtRcEntryPrice";
            txtRcEntryPrice.Size = new Size(162, 22);
            txtRcEntryPrice.TabIndex = 10;
            txtRcEntryPrice.Text = "   ";
            // 
            // lblRcEntryPrice
            // 
            lblRcEntryPrice.AutoSize = true;
            lblRcEntryPrice.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcEntryPrice.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcEntryPrice.Location = new Point(392, 16);
            lblRcEntryPrice.Margin = new Padding(2, 0, 2, 0);
            lblRcEntryPrice.Name = "lblRcEntryPrice";
            lblRcEntryPrice.Size = new Size(76, 19);
            lblRcEntryPrice.TabIndex = 9;
            lblRcEntryPrice.Text = "Entry Price";
            // 
            // lblRcEntryDate
            // 
            lblRcEntryDate.AutoSize = true;
            lblRcEntryDate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcEntryDate.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcEntryDate.Location = new Point(199, 18);
            lblRcEntryDate.Margin = new Padding(2, 0, 2, 0);
            lblRcEntryDate.Name = "lblRcEntryDate";
            lblRcEntryDate.Size = new Size(74, 19);
            lblRcEntryDate.TabIndex = 7;
            lblRcEntryDate.Text = "Entry Date";
            // 
            // dtpEntryDate
            // 
            dtpEntryDate.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEntryDate.Format = DateTimePickerFormat.Short;
            dtpEntryDate.Location = new Point(199, 40);
            dtpEntryDate.Name = "dtpEntryDate";
            dtpEntryDate.Size = new Size(163, 26);
            dtpEntryDate.TabIndex = 6;
            // 
            // lblRcSymbol
            // 
            lblRcSymbol.AutoSize = true;
            lblRcSymbol.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcSymbol.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcSymbol.Location = new Point(14, 15);
            lblRcSymbol.Margin = new Padding(2, 0, 2, 0);
            lblRcSymbol.Name = "lblRcSymbol";
            lblRcSymbol.Size = new Size(55, 19);
            lblRcSymbol.TabIndex = 3;
            lblRcSymbol.Text = "Symbol";
            // 
            // cbRcSymbol
            // 
            cbRcSymbol.BackColor = Color.FromArgb(30, 58, 95);
            cbRcSymbol.FlatStyle = FlatStyle.Flat;
            cbRcSymbol.Font = new Font("Times New Roman", 13.8F);
            cbRcSymbol.FormattingEnabled = true;
            cbRcSymbol.Location = new Point(14, 37);
            cbRcSymbol.Margin = new Padding(2);
            cbRcSymbol.Name = "cbRcSymbol";
            cbRcSymbol.Size = new Size(163, 28);
            cbRcSymbol.TabIndex = 2;
            cbRcSymbol.Text = "  ";
            // 
            // txtRcQuantity
            // 
            txtRcQuantity.BackColor = Color.FromArgb(30, 58, 95);
            txtRcQuantity.BorderStyle = BorderStyle.None;
            txtRcQuantity.Font = new Font("Times New Roman", 13.8F);
            txtRcQuantity.Location = new Point(199, 131);
            txtRcQuantity.Margin = new Padding(2);
            txtRcQuantity.Name = "txtRcQuantity";
            txtRcQuantity.Size = new Size(162, 22);
            txtRcQuantity.TabIndex = 15;
            txtRcQuantity.Text = "   ";
            txtRcQuantity.Visible = false;
            // 
            // lblRcQuantity
            // 
            lblRcQuantity.AutoSize = true;
            lblRcQuantity.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRcQuantity.ForeColor = Color.FromArgb(156, 163, 175);
            lblRcQuantity.Location = new Point(199, 107);
            lblRcQuantity.Margin = new Padding(2, 0, 2, 0);
            lblRcQuantity.Name = "lblRcQuantity";
            lblRcQuantity.Size = new Size(60, 19);
            lblRcQuantity.TabIndex = 14;
            lblRcQuantity.Text = "Quantity";
            lblRcQuantity.Visible = false;
            // 
            // pnlCaseEditor_Max
            // 
            pnlCaseEditor_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlCaseEditor_Max.Location = new Point(11, 11);
            pnlCaseEditor_Max.Margin = new Padding(2);
            pnlCaseEditor_Max.Name = "pnlCaseEditor_Max";
            pnlCaseEditor_Max.Size = new Size(1663, 382);
            pnlCaseEditor_Max.TabIndex = 21;
            pnlCaseEditor_Max.Visible = false;
            // 
            // pnlCases_Max
            // 
            pnlCases_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlCases_Max.Location = new Point(11, 444);
            pnlCases_Max.Margin = new Padding(2);
            pnlCases_Max.Name = "pnlCases_Max";
            pnlCases_Max.Size = new Size(1663, 527);
            pnlCases_Max.TabIndex = 22;
            pnlCases_Max.Visible = false;
            // 
            // FrmRecoveryPlanner
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            ClientSize = new Size(1698, 825);
            Controls.Add(pnlCases);
            Controls.Add(pnlCaseEditor);
            Controls.Add(pnlCaseEditor_Max);
            Controls.Add(pnlCases_Max);
            FormBorderStyle = FormBorderStyle.None;
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
        private Label lblRcEntryDate;
        private DateTimePicker dtpEntryDate;
        private Label lblRcEntryPrice;
        private TextBox txtRcEntryPrice;
        private GroupBox grpRcAmountMode;
        private RadioButton rbRcModeQuantity;
        private RadioButton rbRcModeInvested;
        private TextBox txtRcQuantity;
        private Label lblRcQuantity;
        private TextBox txtRcInvestedUSDT;
        private Label lblRcInvested;
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