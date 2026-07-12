using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services.Trades;

namespace TradingJournal.Pl.PlaceHolder.Journal
{
    /// <summary>
    /// Add and manage exchange connections. Credential fields adapt to the selected exchange
    /// (key+secret for Binance/Bybit/Kraken, wallet+private key for Hyperliquid). Keys are
    /// DPAPI-encrypted on save; the grid only shows label / exchange / mode / date — never keys.
    /// </summary>
    public partial class FrmPlatforms : UserControl, IResponsiveChildForm
    {
        private const string ColId = "colId";
        private const string ColName = "colPlatName";
        private const string ColExchange = "colExchange";
        private const string ColTestnet = "colTestnet";
        private const string ColCreated = "colCreated";
        private const string ColDelete = "colDelete";

        private readonly PlatformManager _platforms = new PlatformManager();
        private readonly ResponsiveLayoutManager _layoutManager;

        // The two generic credential rows, paired label + textbox.
        private Label[] _credLabels;
        private TextBox[] _credBoxes;

        public FrmPlatforms()
        {
            InitializeComponent();

            _credLabels = new[] { lblCred1, lblCred2 };
            _credBoxes = new[] { txtCred1, txtCred2 };

            SetupExchangeCombo();
            SetupGrid();
            ApplyExchangeFields();

            cmbExchange.SelectedIndexChanged += (s, e) => ApplyExchangeFields();
            btnAddPlatform.Click += BtnAddPlatform_Click;
            dgvPlatforms.CellContentClick += DgvPlatforms_CellContentClick;
            this.Load += FrmPlatforms_Load; // populate the grid when the control is shown

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            ThemeManager.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) => ThemeManager.ThemeChanged -= OnThemeChanged;
            ApplyTheme();
        }

        private void OnThemeChanged(object sender, EventArgs e) => ApplyTheme();

        private void FrmPlatforms_Load(object sender, EventArgs e) => RefreshGrid();

        // Move each panel's controls into its wider *_Max twin on maximize (same pattern as the other modules).
        public void SetWindowState(FormWindowStateExtended newState) => _layoutManager.SetWindowState(newState);

        private void InitializeResponsiveLayouts()
        {
            RegisterSectionAsIs(pnlPlatforms, pnlPlatforms_Max);
            RegisterSectionAsIs(pnlPlatformsList, pnlPlatformsList_Max);
        }

        private void RegisterSectionAsIs(Panel normal, Panel max)
        {
            foreach (Control c in normal.Controls)
                _layoutManager.RegisterControl(c, normal, max, c.Location, c.Size);
        }

        private void SetupExchangeCombo()
        {
            if (cmbExchange.Items.Count == 0)
                cmbExchange.Items.AddRange(ExchangeCatalog.Exchanges);
            cmbExchange.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cmbExchange.SelectedIndex < 0) cmbExchange.SelectedIndex = 0;
        }

        /// <summary>Relabel / show / hide the generic credential rows for the selected exchange.</summary>
        private void ApplyExchangeFields()
        {
            var fields = ExchangeCatalog.FieldsFor(cmbExchange.SelectedItem?.ToString());

            for (int i = 0; i < _credBoxes.Length; i++)
            {
                bool used = i < fields.Count;
                _credLabels[i].Visible = used;
                _credBoxes[i].Visible = used;
                _credBoxes[i].Clear();

                if (used)
                {
                    _credLabels[i].Text = fields[i].Label;
                    _credBoxes[i].UseSystemPasswordChar = fields[i].Secret;
                }
            }
        }

        private void SetupGrid()
        {
            dgvPlatforms.AllowUserToAddRows = false;
            dgvPlatforms.AllowUserToDeleteRows = false;
            dgvPlatforms.RowHeadersVisible = false;
            dgvPlatforms.MultiSelect = false;
            dgvPlatforms.ReadOnly = true; // delete goes through the button column
            dgvPlatforms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPlatforms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPlatforms.ColumnHeadersHeight = 34;
            dgvPlatforms.RowTemplate.Height = 34;

            if (dgvPlatforms.Columns.Contains(ColId))
                dgvPlatforms.Columns[ColId].Visible = false;
        }

        private void BtnAddPlatform_Click(object sender, EventArgs e)
        {
            try
            {
                var exchange = cmbExchange.SelectedItem?.ToString();
                var fields = ExchangeCatalog.FieldsFor(exchange);

                var creds = new Dictionary<string, string>();
                for (int i = 0; i < fields.Count && i < _credBoxes.Length; i++)
                    creds[fields[i].Key] = _credBoxes[i].Text;

                _platforms.Add(txtPlatformName.Text, exchange, creds, chkTestnet.Checked);

                txtPlatformName.Clear();
                foreach (var box in _credBoxes) box.Clear();

                ShowStatus("Platform saved. Keys are encrypted on this Windows account.", ok: true);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                ShowStatus(ex.Message, ok: false);
            }
        }

        private void DgvPlatforms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvPlatforms.Columns[e.ColumnIndex].Name != ColDelete) return;

            var row = dgvPlatforms.Rows[e.RowIndex];
            var id = Convert.ToInt32(row.Cells[ColId].Value);
            var name = row.Cells[ColName].Value?.ToString() ?? "this platform";

            var confirm = MessageBox.Show(
                $"Delete \"{name}\" and its stored keys? This cannot be undone.",
                "Delete platform", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            _platforms.Delete(id);
            ShowStatus($"Deleted \"{name}\".", ok: true);
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvPlatforms.Rows.Clear();
            foreach (ExchangePlatform p in _platforms.GetAll())
            {
                int i = dgvPlatforms.Rows.Add();
                var row = dgvPlatforms.Rows[i];
                row.Cells[ColId].Value = p.Id;
                row.Cells[ColName].Value = p.Name;
                row.Cells[ColExchange].Value = p.Exchange;
                row.Cells[ColTestnet].Value = p.UseTestnet ? "Testnet" : "Live";
                row.Cells[ColCreated].Value = p.CreatedUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            }
        }

        private void ShowStatus(string message, bool ok)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = ok ? Color.MediumSeaGreen : Color.OrangeRed;
        }

        // ----------------------------------------------------------------- Theme
        private void ApplyTheme()
        {
            BackColor = ThemeManager.BackgroundColor;
            StyleRecursively(this);
            StyleGrid();
        }

        private void StyleRecursively(Control root)
        {
            foreach (Control c in root.Controls)
            {
                switch (c)
                {
                    case DataGridView _:
                        break;
                    case Panel panel:
                        panel.BackColor = ThemeManager.PanelColor;
                        break;
                    case TextBox tb:
                        tb.BackColor = ThemeManager.TextBoxColor;
                        tb.ForeColor = ThemeManager.TextColor;
                        break;
                    case ComboBox cmb:
                        cmb.BackColor = ThemeManager.TextBoxColor;
                        cmb.ForeColor = ThemeManager.TextColor;
                        cmb.FlatStyle = FlatStyle.Flat;
                        break;
                    case CheckBox chk:
                        chk.ForeColor = ThemeManager.TextColor;
                        break;
                    case Button btn:
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.ForeColor = ThemeManager.TextColor;
                        btn.BackColor = ThemeManager.ActiveButtonColor;
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
            dgvPlatforms.EnableHeadersVisualStyles = false;
            dgvPlatforms.BackgroundColor = ThemeManager.PanelColor;
            dgvPlatforms.GridColor = ThemeManager.BackgroundColor;
            dgvPlatforms.BorderStyle = BorderStyle.None;
            dgvPlatforms.DefaultCellStyle.BackColor = ThemeManager.TextBoxColor;
            dgvPlatforms.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            dgvPlatforms.DefaultCellStyle.SelectionBackColor = ThemeManager.ButtonHoverColor;
            dgvPlatforms.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
            dgvPlatforms.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.ButtonColor;
            dgvPlatforms.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
        }
    }
}