namespace TradingJournal.Pl.PlaceHolder.Journal
{
    partial class FrmPlatforms
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlPlatforms = new Panel();
            btnAddPlatform = new FontAwesome.Sharp.IconButton();
            chkTestnet = new CheckBox();
            lblTitle = new Label();
            cmbExchange = new ComboBox();
            txtCred1 = new TextBox();
            txtCred2 = new TextBox();
            txtPlatformName = new TextBox();
            lblStatus = new Label();
            lblWarning = new Label();
            lblCapExchange = new Label();
            lblCred2 = new Label();
            lblCred1 = new Label();
            lblCapPlatName = new Label();
            pnlPlatforms_Max = new Panel();
            pnlPlatformsList = new Panel();
            lblTableTitle = new Label();
            dgvPlatforms = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colPlatName = new DataGridViewTextBoxColumn();
            colExchange = new DataGridViewTextBoxColumn();
            colTestnet = new DataGridViewTextBoxColumn();
            colCreated = new DataGridViewTextBoxColumn();
            colDelete = new DataGridViewButtonColumn();
            pnlPlatformsList_Max = new Panel();
            pnlPlatforms.SuspendLayout();
            pnlPlatformsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPlatforms).BeginInit();
            SuspendLayout();
            // 
            // pnlPlatforms
            // 
            pnlPlatforms.BackColor = Color.FromArgb(27, 38, 59);
            pnlPlatforms.Controls.Add(btnAddPlatform);
            pnlPlatforms.Controls.Add(chkTestnet);
            pnlPlatforms.Controls.Add(lblTitle);
            pnlPlatforms.Controls.Add(cmbExchange);
            pnlPlatforms.Controls.Add(txtCred1);
            pnlPlatforms.Controls.Add(txtCred2);
            pnlPlatforms.Controls.Add(txtPlatformName);
            pnlPlatforms.Controls.Add(lblStatus);
            pnlPlatforms.Controls.Add(lblWarning);
            pnlPlatforms.Controls.Add(lblCapExchange);
            pnlPlatforms.Controls.Add(lblCred2);
            pnlPlatforms.Controls.Add(lblCred1);
            pnlPlatforms.Controls.Add(lblCapPlatName);
            pnlPlatforms.Location = new Point(19, 28);
            pnlPlatforms.Margin = new Padding(2);
            pnlPlatforms.Name = "pnlPlatforms";
            pnlPlatforms.Size = new Size(1008, 551);
            pnlPlatforms.TabIndex = 24;
            // 
            // btnAddPlatform
            // 
            btnAddPlatform.BackColor = Color.FromArgb(30, 58, 95);
            btnAddPlatform.FlatAppearance.BorderSize = 0;
            btnAddPlatform.FlatStyle = FlatStyle.Flat;
            btnAddPlatform.Font = new Font("Times New Roman", 12F);
            btnAddPlatform.IconChar = FontAwesome.Sharp.IconChar.None;
            btnAddPlatform.IconColor = Color.Black;
            btnAddPlatform.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddPlatform.Location = new Point(49, 391);
            btnAddPlatform.Margin = new Padding(2);
            btnAddPlatform.Name = "btnAddPlatform";
            btnAddPlatform.Size = new Size(192, 37);
            btnAddPlatform.TabIndex = 14;
            btnAddPlatform.Text = "Add platform";
            btnAddPlatform.UseVisualStyleBackColor = false;
            // 
            // chkTestnet
            // 
            chkTestnet.AutoSize = true;
            chkTestnet.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkTestnet.ForeColor = Color.FromArgb(156, 163, 175);
            chkTestnet.Location = new Point(49, 347);
            chkTestnet.Margin = new Padding(3, 2, 3, 2);
            chkTestnet.Name = "chkTestnet";
            chkTestnet.Size = new Size(95, 23);
            chkTestnet.TabIndex = 13;
            chkTestnet.Text = "Use testnet";
            chkTestnet.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTitle.Location = new Point(21, 16);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(373, 32);
            lblTitle.TabIndex = 12;
            lblTitle.Text = "Regisnter a Trading Platform";
            // 
            // cmbExchange
            // 
            cmbExchange.BackColor = Color.FromArgb(30, 58, 95);
            cmbExchange.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExchange.FlatStyle = FlatStyle.Flat;
            cmbExchange.Font = new Font("Times New Roman", 13.8F);
            cmbExchange.FormattingEnabled = true;
            cmbExchange.Location = new Point(265, 116);
            cmbExchange.Margin = new Padding(2);
            cmbExchange.Name = "cmbExchange";
            cmbExchange.Size = new Size(177, 28);
            cmbExchange.TabIndex = 11;
            // 
            // txtCred1
            // 
            txtCred1.BackColor = Color.FromArgb(30, 58, 95);
            txtCred1.BorderStyle = BorderStyle.None;
            txtCred1.Font = new Font("Times New Roman", 13.8F);
            txtCred1.Location = new Point(48, 206);
            txtCred1.Margin = new Padding(2);
            txtCred1.Name = "txtCred1";
            txtCred1.Size = new Size(313, 22);
            txtCred1.TabIndex = 8;
            txtCred1.Text = "   ";
            // 
            // txtCred2
            // 
            txtCred2.BackColor = Color.FromArgb(30, 58, 95);
            txtCred2.BorderStyle = BorderStyle.None;
            txtCred2.Font = new Font("Times New Roman", 13.8F);
            txtCred2.Location = new Point(48, 291);
            txtCred2.Margin = new Padding(2);
            txtCred2.Name = "txtCred2";
            txtCred2.Size = new Size(313, 22);
            txtCred2.TabIndex = 7;
            txtCred2.Text = "   ";
            // 
            // txtPlatformName
            // 
            txtPlatformName.BackColor = Color.FromArgb(30, 58, 95);
            txtPlatformName.BorderStyle = BorderStyle.None;
            txtPlatformName.Font = new Font("Times New Roman", 13.8F);
            txtPlatformName.Location = new Point(48, 118);
            txtPlatformName.Margin = new Padding(2);
            txtPlatformName.Name = "txtPlatformName";
            txtPlatformName.Size = new Size(177, 22);
            txtPlatformName.TabIndex = 6;
            txtPlatformName.Text = "   ";
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(156, 163, 175);
            lblStatus.Location = new Point(48, 503);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(525, 16);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "--";
            // 
            // lblWarning
            // 
            lblWarning.AutoSize = true;
            lblWarning.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWarning.ForeColor = Color.FromArgb(156, 163, 175);
            lblWarning.Location = new Point(48, 464);
            lblWarning.Margin = new Padding(4, 0, 4, 0);
            lblWarning.Name = "lblWarning";
            lblWarning.Size = new Size(389, 19);
            lblWarning.TabIndex = 4;
            lblWarning.Text = "Create keys with withdrawals DISABLED and an IP allow-list.";
            // 
            // lblCapExchange
            // 
            lblCapExchange.AutoSize = true;
            lblCapExchange.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapExchange.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapExchange.Location = new Point(265, 87);
            lblCapExchange.Margin = new Padding(4, 0, 4, 0);
            lblCapExchange.Name = "lblCapExchange";
            lblCapExchange.Size = new Size(67, 19);
            lblCapExchange.TabIndex = 3;
            lblCapExchange.Text = "Exchange";
            // 
            // lblCred2
            // 
            lblCred2.AutoSize = true;
            lblCred2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCred2.ForeColor = Color.FromArgb(156, 163, 175);
            lblCred2.Location = new Point(48, 260);
            lblCred2.Margin = new Padding(4, 0, 4, 0);
            lblCred2.Name = "lblCred2";
            lblCred2.Size = new Size(21, 19);
            lblCred2.TabIndex = 2;
            lblCred2.Text = "--";
            // 
            // lblCred1
            // 
            lblCred1.AutoSize = true;
            lblCred1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCred1.ForeColor = Color.FromArgb(156, 163, 175);
            lblCred1.Location = new Point(48, 175);
            lblCred1.Margin = new Padding(4, 0, 4, 0);
            lblCred1.Name = "lblCred1";
            lblCred1.Size = new Size(21, 19);
            lblCred1.TabIndex = 1;
            lblCred1.Text = "--";
            // 
            // lblCapPlatName
            // 
            lblCapPlatName.AutoSize = true;
            lblCapPlatName.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCapPlatName.ForeColor = Color.FromArgb(156, 163, 175);
            lblCapPlatName.Location = new Point(48, 92);
            lblCapPlatName.Margin = new Padding(4, 0, 4, 0);
            lblCapPlatName.Name = "lblCapPlatName";
            lblCapPlatName.Size = new Size(96, 19);
            lblCapPlatName.TabIndex = 0;
            lblCapPlatName.Text = "Platform name";
            // 
            // pnlPlatforms_Max
            // 
            pnlPlatforms_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlPlatforms_Max.Location = new Point(19, 26);
            pnlPlatforms_Max.Margin = new Padding(2);
            pnlPlatforms_Max.Name = "pnlPlatforms_Max";
            pnlPlatforms_Max.Size = new Size(1656, 553);
            pnlPlatforms_Max.TabIndex = 25;
            pnlPlatforms_Max.Visible = false;
            // 
            // pnlPlatformsList
            // 
            pnlPlatformsList.BackColor = Color.FromArgb(27, 38, 59);
            pnlPlatformsList.Controls.Add(lblTableTitle);
            pnlPlatformsList.Controls.Add(dgvPlatforms);
            pnlPlatformsList.Location = new Point(19, 600);
            pnlPlatformsList.Margin = new Padding(2);
            pnlPlatformsList.Name = "pnlPlatformsList";
            pnlPlatformsList.Size = new Size(1008, 262);
            pnlPlatformsList.TabIndex = 26;
            // 
            // lblTableTitle
            // 
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTableTitle.ForeColor = Color.FromArgb(156, 163, 175);
            lblTableTitle.Location = new Point(22, 15);
            lblTableTitle.Margin = new Padding(2, 0, 2, 0);
            lblTableTitle.Name = "lblTableTitle";
            lblTableTitle.Size = new Size(261, 32);
            lblTableTitle.TabIndex = 13;
            lblTableTitle.Text = "Registerd Platforms";
            // 
            // dgvPlatforms
            // 
            dgvPlatforms.AllowUserToAddRows = false;
            dgvPlatforms.AllowUserToDeleteRows = false;
            dgvPlatforms.AllowUserToResizeColumns = false;
            dgvPlatforms.AllowUserToResizeRows = false;
            dgvPlatforms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlatforms.Columns.AddRange(new DataGridViewColumn[] { colId, colPlatName, colExchange, colTestnet, colCreated, colDelete });
            dgvPlatforms.Location = new Point(18, 71);
            dgvPlatforms.Margin = new Padding(4, 3, 4, 3);
            dgvPlatforms.Name = "dgvPlatforms";
            dgvPlatforms.RowHeadersWidth = 51;
            dgvPlatforms.Size = new Size(963, 191);
            dgvPlatforms.TabIndex = 0;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.Width = 125;
            // 
            // colPlatName
            // 
            colPlatName.HeaderText = "Name";
            colPlatName.MinimumWidth = 6;
            colPlatName.Name = "colPlatName";
            colPlatName.Width = 125;
            // 
            // colExchange
            // 
            colExchange.HeaderText = "Exchange";
            colExchange.MinimumWidth = 6;
            colExchange.Name = "colExchange";
            colExchange.Width = 125;
            // 
            // colTestnet
            // 
            colTestnet.HeaderText = "Mode";
            colTestnet.MinimumWidth = 6;
            colTestnet.Name = "colTestnet";
            colTestnet.Width = 125;
            // 
            // colCreated
            // 
            colCreated.HeaderText = "Added";
            colCreated.MinimumWidth = 6;
            colCreated.Name = "colCreated";
            colCreated.Width = 125;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "";
            colDelete.MinimumWidth = 6;
            colDelete.Name = "colDelete";
            colDelete.Text = "Delete";
            colDelete.UseColumnTextForButtonValue = true;
            colDelete.Width = 125;
            // 
            // pnlPlatformsList_Max
            // 
            pnlPlatformsList_Max.BackColor = Color.FromArgb(27, 38, 59);
            pnlPlatformsList_Max.Location = new Point(19, 598);
            pnlPlatformsList_Max.Margin = new Padding(2);
            pnlPlatformsList_Max.Name = "pnlPlatformsList_Max";
            pnlPlatformsList_Max.Size = new Size(1656, 262);
            pnlPlatformsList_Max.TabIndex = 25;
            pnlPlatformsList_Max.Visible = false;
            // 
            // FrmPlatforms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 27, 42);
            Controls.Add(pnlPlatformsList);
            Controls.Add(pnlPlatforms);
            Controls.Add(pnlPlatforms_Max);
            Controls.Add(pnlPlatformsList_Max);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmPlatforms";
            Size = new Size(1699, 881);
            pnlPlatforms.ResumeLayout(false);
            pnlPlatforms.PerformLayout();
            pnlPlatformsList.ResumeLayout(false);
            pnlPlatformsList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPlatforms).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSlnTp;
        private Panel pnlPlatforms;
        private Label lblTitle;
        private Panel pnlPlatforms_Max;
        private ComboBox cmbExchange;
        private TextBox txtCred1;
        private TextBox txtCred2;
        private TextBox txtPlatformName;
        private Label lblStatus;
        private Label lblWarning;
        private Label lblCapExchange;
        private Label lblCred2;
        private Label lblCred1;
        private Label lblCapPlatName;
        private CheckBox chkTestnet;
        private FontAwesome.Sharp.IconButton btnAddPlatform;
        private Panel pnlPlatformsList;
        private Label lblTableTitle;
        private DataGridView dgvPlatforms;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colPlatName;
        private DataGridViewTextBoxColumn colExchange;
        private DataGridViewTextBoxColumn colTestnet;
        private DataGridViewTextBoxColumn colCreated;
        private DataGridViewButtonColumn colDelete;
        private Panel pnlPlatformsList_Max;
    }
}
