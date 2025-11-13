using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Pl.PlaceHolder.Settings
{
    public partial class DlgRecycleBin : Form
    {
        private List<Core.Data.Entities.RecycleBinItem> _all = new();
        private List<Core.Data.Entities.RecycleBinItem> _view = new();

        private const string COL_RESTORE = "RestoreColumn";
        private const string COL_DELETE = "DeleteColumn";

        public DlgRecycleBin()
        {
            InitializeComponent();
            SetupBinGrid();
            WireEvents();

            ApplyTheme();

            cbType.SelectedIndex = 0;
            chkUseDateRange.Checked = false;
            ToggleDatePickers(false);
        }

        #region Wiring
        private void WireEvents()
        {
            Load += async (_, __) =>
            {
                await RefreshDataAsync();

                var types = _all.Select(a => a.EntityType)
                                .Where(t => !string.IsNullOrWhiteSpace(t))
                                .Distinct(StringComparer.OrdinalIgnoreCase)
                                .OrderBy(t => t)
                                .ToList();

                cbType.Items.Clear();
                cbType.Items.Add("All");
                cbType.Items.AddRange(types.Cast<object>().ToArray());
                cbType.SelectedIndex = 0;
            };

            btnRefresh.Click += async (_, __) => await RefreshDataAsync();
            btnPurgeExpired.Click += async (_, __) => await PurgeExpiredAsync();
            btnEmptyBin.Click += async (_, __) => await EmptyAllAsync();

            cbType.SelectedIndexChanged += (_, __) => ApplyFiltersAndBind();
            txtSearch.TextChanged += (_, __) => ApplyFiltersAndBind();
            chkShowExpired.CheckedChanged += (_, __) => ApplyFiltersAndBind();

            chkUseDateRange.CheckedChanged += (_, __) =>
            {
                ToggleDatePickers(chkUseDateRange.Checked);
                ApplyFiltersAndBind();
            };
            dtpFrom.ValueChanged += (_, __) => { if (chkUseDateRange.Checked) ApplyFiltersAndBind(); };
            dtpTo.ValueChanged += (_, __) => { if (chkUseDateRange.Checked) ApplyFiltersAndBind(); };

            dgvBin.CellContentClick += DgvBin_CellContentClick;
            dgvBin.CellPainting += DgvBin_CellPainting;
            dgvBin.CellFormatting += DgvBin_CellFormatting;

            // Ignore header clicks entirely (no sorting / no selection)
            dgvBin.ColumnHeaderMouseClick += (_, __) => { /* no-op */ };
        }
        #endregion

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;
            pnlBottom.BackColor = ThemeManager.PanelColor;
            pnlTop.BackColor = ThemeManager.PanelColor;

            btnEmptyBin.BackColor = ThemeManager.ButtonColor;
            btnPurgeExpired.BackColor = ThemeManager.ButtonColor;
            btnRefresh.BackColor = ThemeManager.ButtonColor;

            cbType.BackColor = ThemeManager.PanelColor;
            cbType.ForeColor = ThemeManager.TextColor;
            cbType.FlatStyle = FlatStyle.Flat;

            lblCount.ForeColor = ThemeManager.TextColor;
            lblRetention.ForeColor = ThemeManager.TextColor;
            chkShowExpired.ForeColor = ThemeManager.TextColor;
            chkUseDateRange.ForeColor = ThemeManager.TextColor;
            dtpFrom.BackColor = ThemeManager.TextBoxColor;
            dtpFrom.ForeColor = ThemeManager.TextColor;
            dtpTo.BackColor = ThemeManager.TextBoxColor;
            dtpTo.ForeColor = ThemeManager.TextColor;
            lblFilters.ForeColor = ThemeManager.TextColor;
            lblFrom.ForeColor = ThemeManager.TextColor;
            lblTo.ForeColor = ThemeManager.TextColor;
            lblHint.ForeColor = ThemeManager.TextColor;
            lblSearch.ForeColor = ThemeManager.TextColor;
            lblTitle.ForeColor = ThemeManager.TextColor;


            txtSearch.BackColor = ThemeManager.TextBoxColor;
            txtSearch.ForeColor = ThemeManager.TextColor;

            ApplyGridStyling();
        }


        #region Data
        private async Task RefreshDataAsync()
        {
            try
            {
                using var db = new AppDbContext();
                _all = await RecycleBinService.ListAsync(db);

                var s = SettingsManager.Load();
                var days = s.RecycleBin?.RetentionDays ?? RecycleBinService.RetentionDaysDefault;
                lblRetention.Text = $"Retention: {days} days (Settings)";

                ApplyFiltersAndBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to load Recycle Bin:\n" + ex.Message,
                    "Recycle Bin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFiltersAndBind()
        {
            IEnumerable<Core.Data.Entities.RecycleBinItem> q = _all;

            if (!chkShowExpired.Checked)
            {
                var now = DateTime.UtcNow;
                q = q.Where(x => x.ExpiresUtc == null || x.ExpiresUtc > now);
            }

            var typeSel = (cbType.SelectedItem as string) ?? "All";
            if (!string.Equals(typeSel, "All", StringComparison.OrdinalIgnoreCase))
                q = q.Where(x => string.Equals(x.EntityType, typeSel, StringComparison.OrdinalIgnoreCase));

            if (chkUseDateRange.Checked)
            {
                var localFrom = dtpFrom.Value.Date;
                var localTo = dtpTo.Value.Date.AddDays(1).AddTicks(-1);

                var fromUtc = DateTime.SpecifyKind(localFrom, DateTimeKind.Local).ToUniversalTime();
                var toUtc = DateTime.SpecifyKind(localTo, DateTimeKind.Local).ToUniversalTime();

                q = q.Where(x => x.DeletedUtc >= fromUtc && x.DeletedUtc <= toUtc);
            }

            var text = (txtSearch.Text ?? "").Trim();
            if (!string.IsNullOrEmpty(text))
            {
                var t = text.ToLowerInvariant();
                q = q.Where(x =>
                    (!string.IsNullOrEmpty(x.EntityKey) && x.EntityKey.ToLower().Contains(t)) ||
                    (!string.IsNullOrEmpty(x.Note) && x.Note.ToLower().Contains(t)) ||
                    (!string.IsNullOrEmpty(x.PayloadJson) && x.PayloadJson.ToLower().Contains(t)));
            }

            _view = q.OrderByDescending(x => x.DeletedUtc).ToList();

            var rows = _view.Select(x => new
            {
                x.Id,
                Date = x.DeletedUtc,
                Type = x.EntityType,
                Preview = x.EntityKey ?? "",
                SizeBytes = (long)(x.PayloadJson?.Length ?? 0),
                Note = x.Note ?? "",
                Expires = x.ExpiresUtc
            }).ToList();

            dgvBin.DataSource = rows;

            EnsureActionButtonColumns();
            ApplyGridStyling();
            UpdateFooterCounts();
        }

        private void UpdateFooterCounts()
        {
            var now = DateTime.UtcNow;
            var total = _view.Count;
            var expired = _view.Count(x => x.ExpiresUtc != null && x.ExpiresUtc <= now);
            lblCount.Text = $"Items: {total} ({expired} expired)";
        }
        #endregion

        #region Actions
        private async Task PurgeExpiredAsync()
        {
            try
            {
                using var db = new AppDbContext();
                var n = await RecycleBinService.PurgeExpiredAsync(db);
                MessageBox.Show(this, $"Purged {n} expired item(s).", "Recycle Bin",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to purge expired:\n" + ex.Message,
                    "Recycle Bin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task EmptyAllAsync()
        {
            if (MessageBox.Show(this, "Permanently delete ALL items from Recycle Bin?",
                    "Empty Bin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using var db = new AppDbContext();
                await RecycleBinService.EmptyAllAsync(db);
                await RefreshDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to empty bin:\n" + ex.Message,
                    "Recycle Bin", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DgvBin_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var name = dgvBin.Columns[e.ColumnIndex].Name;
            if (name != COL_RESTORE && name != COL_DELETE) return;

            var rowObj = dgvBin.Rows[e.RowIndex].DataBoundItem;
            if (rowObj == null) return;

            var id = (int)rowObj.GetType().GetProperty("Id")!.GetValue(rowObj)!;

            using var db = new AppDbContext();

            if (name == COL_RESTORE)
            {
                if (MessageBox.Show(this, "Restore this item?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                var ok = await RecycleBinService.RestoreAsync(db, id);
                if (!ok)
                    MessageBox.Show(this, "Restore failed.", "Recycle Bin",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show(this, "Permanently delete this item?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                await RecycleBinService.DeleteItemAsync(db, id);
            }

            await RefreshDataAsync();
        }
        #endregion

        #region Grid
        private void SetupBinGrid()
        {
            var g = dgvBin;

            // Behavior like Account History, with horizontal scroll
            g.AllowUserToAddRows = g.AllowUserToDeleteRows = g.AllowUserToResizeRows = false;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.EditMode = DataGridViewEditMode.EditProgrammatically;
            g.ReadOnly = true;
            g.RowHeadersVisible = false;

            g.AutoGenerateColumns = true;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // horizontal scroll
            g.ScrollBars = ScrollBars.Both;
            g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Same palette as Settings grids
            g.BackgroundColor = ThemeManager.DataGrid;
            g.GridColor = Color.FromArgb(45, 51, 73);

            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            g.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // No header resizing by user
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 34;
            g.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // Prevent header “selection” highlight looking clickable
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = g.ColumnHeadersDefaultCellStyle.BackColor;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = g.ColumnHeadersDefaultCellStyle.ForeColor;

            g.DefaultCellStyle.BackColor = Color.FromArgb(24, 30, 54);
            g.DefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
            g.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 51, 73);
            g.DefaultCellStyle.SelectionForeColor = Color.White;
            g.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            g.RowTemplate.Height = 25;

            EnableDoubleBuffering(g);
        }

        private void EnsureActionButtonColumns()
        {
            if (!dgvBin.Columns.Contains(COL_DELETE))
            {
                var btn = new DataGridViewButtonColumn
                {
                    Name = COL_DELETE,
                    HeaderText = "Delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                };
                dgvBin.Columns.Add(btn);
            }

            if (!dgvBin.Columns.Contains(COL_RESTORE))
            {
                var btn = new DataGridViewButtonColumn
                {
                    Name = COL_RESTORE,
                    HeaderText = "Restore",
                    Text = "Restore",
                    UseColumnTextForButtonValue = true
                };
                dgvBin.Columns.Add(btn);
            }

            dgvBin.Columns[COL_DELETE].DisplayIndex = 0;
            dgvBin.Columns[COL_RESTORE].DisplayIndex = 1;

            dgvBin.Columns[COL_DELETE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBin.Columns[COL_RESTORE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvBin.Columns[COL_DELETE].Width = 90;
            dgvBin.Columns[COL_RESTORE].Width = 100;

            // Ensure NotSortable for all columns (no header “click” sorting)
            foreach (DataGridViewColumn c in dgvBin.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void ApplyGridStyling()
        {
            var d = dgvBin;

            if (d.Columns.Contains("Date")) d.Columns["Date"].HeaderText = "Date";
            if (d.Columns.Contains("Type")) d.Columns["Type"].HeaderText = "Type";
            if (d.Columns.Contains("Preview")) d.Columns["Preview"].HeaderText = "Preview";
            if (d.Columns.Contains("SizeBytes")) d.Columns["SizeBytes"].HeaderText = "Size";
            if (d.Columns.Contains("Note")) d.Columns["Note"].HeaderText = "Note";
            if (d.Columns.Contains("Expires")) d.Columns["Expires"].HeaderText = "Expires";

            if (d.Columns.Contains("Id")) d.Columns["Id"].Visible = false;

            SetWidth(d, COL_DELETE, 90);
            SetWidth(d, COL_RESTORE, 100);
            SetWidth(d, "Date", 170);
            SetWidth(d, "Type", 140);
            SetWidth(d, "Preview", 280);
            SetWidth(d, "SizeBytes", 100);
            SetWidth(d, "Note", 320);
            SetWidth(d, "Expires", 170);

            foreach (DataGridViewColumn c in d.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable; // reinforce after binding
            var g = dgvBin;

            g.BackgroundColor = ThemeManager.DataGrid;
            g.GridColor = Color.FromArgb(45, 51, 73);

            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            g.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            g.ColumnHeadersHeight = 30;

            g.RowHeadersVisible = false;
            g.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            g.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            g.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            g.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
            g.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            g.RowTemplate.Height = 25;

            g.AlternatingRowsDefaultCellStyle.BackColor = ThemeManager.DataGrid;
            g.AlternatingRowsDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            g.AlternatingRowsDefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            g.AlternatingRowsDefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;

            foreach (DataGridViewColumn c in g.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

        }

        private static void SetWidth(DataGridView g, string name, int w)
        {
            if (!g.Columns.Contains(name)) return;
            var c = g.Columns[name];
            c.Width = w;
            c.MinimumWidth = w;
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void DgvBin_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var isDelete = dgvBin.Columns[e.ColumnIndex].Name == COL_DELETE;
            var isRestore = dgvBin.Columns[e.ColumnIndex].Name == COL_RESTORE;
            if (!isDelete && !isRestore) return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            var buttonBounds = e.CellBounds;
            buttonBounds.Inflate(-2, -2);

            var buttonColor = isDelete
                ? Color.FromArgb(220, 53, 69)
                : Color.FromArgb(30, 95, 58);

            ControlPaint.DrawButton(e.Graphics, buttonBounds, ButtonState.Normal);
            using var br = new SolidBrush(buttonColor);
            e.Graphics.FillRectangle(br, buttonBounds);

            var text = isDelete ? "Delete" : "Restore";
            TextRenderer.DrawText(e.Graphics, text, e.CellStyle.Font, buttonBounds, Color.White,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

            e.Handled = true;
        }

        private void DgvBin_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvBin.Columns[e.ColumnIndex].Name;

            if ((col == "Date" || col == "Expires") && e.Value is DateTime dt)
            {
                e.Value = dt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                e.FormattingApplied = true;
            }
            else if (col == "SizeBytes" && e.Value is long size)
            {
                string fmt = size < 1_000_000 ? $"{size / 1024.0:0.#} KB" : $"{size / 1_048_576.0:0.#} MB";
                e.Value = fmt;
                e.FormattingApplied = true;
            }
        }

        private static void EnableDoubleBuffering(DataGridView gv)
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, gv, new object[] { true });
        }

        private void ToggleDatePickers(bool on)
        {
            dtpFrom.Enabled = on;
            dtpTo.Enabled = on;
        }
        #endregion
    }
}
