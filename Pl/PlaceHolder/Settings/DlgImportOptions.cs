using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Pl.PlaceHolder.Settings
{
    public partial class DlgImportOptions : Form
    {
        private readonly IImportService _importer = new ImportService();

        public DlgImportOptions()
        {
            InitializeComponent();

            // Defaults
            cbTarget.SelectedIndex = 0; // Journal
            cbImFormat.SelectedIndex = 0; // Excel by UI default
            lblImStatus.Text = "--";
            btnImportRun.Enabled = false;

            btnImportCancel.Click += (s, e) => Close();

            btnImBrowse.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog
                {
                    Title = "Choose file to import",
                    Filter = "Excel/CSV|*.xlsx;*.csv|All files|*.*"
                };
                if (ofd.ShowDialog(this) == DialogResult.OK)
                    txtImFile.Text = ofd.FileName;
            };
            ApplyTheme();

            btnValidate.Click += async (s, e) => await RunValidateAsync();
            btnImportRun.Click += async (s, e) => await RunImportAsync();
        }

        private async Task RunValidateAsync()
        {
            gridPreview.DataSource = null;
            btnImportRun.Enabled = false;
            lblImStatus.Text = "--";

            if (string.IsNullOrWhiteSpace(txtImFile.Text) || !File.Exists(txtImFile.Text))
            {
                MessageBox.Show("Please choose a file to import.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var req = new ImportRequest
            {
                Target = cbTarget.SelectedIndex == 0 ? ImportTarget.Journal : ImportTarget.Recovery,
                Format = cbImFormat.SelectedIndex == 0 ? ImportFormat.Excel : ImportFormat.Csv,
                FilePath = txtImFile.Text
            };

            try
            {
                var result = await _importer.ValidateAsync(req, CancellationToken.None);
                lblImStatus.Text = result.Message ?? (result.IsValid ? "Validation OK." : "Validation failed.");
                if (result.Preview != null)
                    gridPreview.DataSource = result.Preview;

                btnImportRun.Enabled = result.IsValid;
            }
            catch (NotSupportedException ex) { MessageBox.Show(ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex) { MessageBox.Show("Validation failed:\n" + ex, "Import", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            cbTarget.BackColor = ThemeManager.PanelColor;
            cbTarget.ForeColor = ThemeManager.TextColor;

            cbImFormat.BackColor = ThemeManager.PanelColor;
            cbImFormat.ForeColor = ThemeManager.TextColor;

            lblImFile.ForeColor = ThemeManager.TextColor;
            lblImFormat.ForeColor = ThemeManager.TextColor;
            lblImStatus.ForeColor = ThemeManager.TextColor;

            lblImTitle.ForeColor = ThemeManager.TextColor;
            lblTarget.ForeColor = ThemeManager.TextColor;

            btnImBrowse.BackColor = ThemeManager.ButtonColor;
            btnImBrowse.ForeColor = ThemeManager.TextColor;
            btnImportCancel.BackColor = ThemeManager.ButtonColor;
            btnImportCancel.ForeColor = ThemeManager.TextColor;
            btnValidate.BackColor = ThemeManager.ButtonColor;
            btnValidate.ForeColor = ThemeManager.TextColor;
            btnImportRun.BackColor = ThemeManager.ButtonColor;
            btnImportRun.ForeColor = ThemeManager.TextColor;

            gpbImportFormat.ForeColor = ThemeManager.TextColor;
            gpbImportPath.ForeColor = ThemeManager.TextColor;
            gpbImportSources.ForeColor = ThemeManager.TextColor;

            txtImFile.BackColor = ThemeManager.TextBoxColor;
            txtImFile.ForeColor = ThemeManager.TextColor;

            gridPreview.BackgroundColor = ThemeManager.DataGrid;
            gridPreview.DefaultCellStyle.BackColor = ThemeManager.DataPanelColor;
            gridPreview.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
            gridPreview.DefaultCellStyle.SelectionBackColor = ThemeManager.BackgroundColor;
            gridPreview.DefaultCellStyle.SelectionForeColor = ThemeManager.TextColor;
            gridPreview.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            gridPreview.RowTemplate.Height = 25;

            gridPreview.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            gridPreview.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            gridPreview.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            gridPreview.ColumnHeadersHeight = 30;
            gridPreview.EnableHeadersVisualStyles = false;
        }



        private async Task RunImportAsync()
        {
            progressImport.Value = 0;

            var req = new ImportRequest
            {
                Target = cbTarget.SelectedIndex == 0 ? ImportTarget.Journal : ImportTarget.Recovery,
                Format = cbImFormat.SelectedIndex == 0 ? ImportFormat.Excel : ImportFormat.Csv,
                FilePath = txtImFile.Text
            };

            try
            {
                var (ok, message, rows) = await _importer.ImportAsync(req, CancellationToken.None);
                lblImStatus.Text = message ?? (ok ? $"Imported {rows} rows." : "Import failed.");
                if (ok)
                {
                    MessageBox.Show($"Import complete. Rows affected: {rows}", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(message ?? "Import failed.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (NotSupportedException ex) { MessageBox.Show(ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            catch (Exception ex) { MessageBox.Show("Import failed:\n" + ex, "Import", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
