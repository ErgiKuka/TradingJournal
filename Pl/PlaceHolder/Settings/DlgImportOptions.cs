using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Pl.PlaceHolder.Settings
{
    public partial class DlgImportOptions : Form
    {
        private static readonly string[] ExcelExtensions = { ".xlsx", ".xlsm", ".xltx", ".xltm" };
        private static readonly string[] CsvExtensions = { ".csv", ".txt" };

        // Must match the order of cbImFormat.Items in the designer: { "CSV (.csv)", "Excel (.xlsx)" }
        private const int FormatIndexCsv = 0;
        private const int FormatIndexExcel = 1;

        private readonly IImportService _importer = new ImportService();
        private CancellationTokenSource? _cts;

        public DlgImportOptions()
        {
            InitializeComponent();

            cbTarget.SelectedIndex = 0;                 // Journal (Trades)
            cbImFormat.SelectedIndex = FormatIndexCsv;  // index 0 is CSV, not Excel
            cbImFormat.Enabled = false;                 // display only — the file extension decides

            lblImStatus.Text = "--";
            btnImportRun.Enabled = false;

            btnImportCancel.Click += OnCancelClicked;
            btnImBrowse.Click += OnBrowseClicked;
            btnValidate.Click += async (s, e) => await RunValidateAsync();
            btnImportRun.Click += async (s, e) => await RunImportAsync();

            txtImFile.TextChanged += (s, e) =>
            {
                SyncFormatCombo(txtImFile.Text);
                btnImportRun.Enabled = false;   // any file change invalidates a previous Validate
            };

            cbTarget.SelectedIndexChanged += (s, e) => btnImportRun.Enabled = false;

            ApplyTheme();
        }

        // ------------------------------------------------------------------ format resolution

        /// <summary>
        /// The file extension is the single source of truth for the import format.
        ///
        /// The previous version derived the format from cbImFormat.SelectedIndex with an inverted
        /// mapping (index 0 is "CSV (.csv)" but was mapped to ImportFormat.Excel), so choosing CSV
        /// handed the file to ClosedXML and surfaced its
        /// "Extension 'csv' is not supported" error. Any UI-driven mapping is a standing risk of
        /// the dialog and the file disagreeing, so the combo is now a mirror, not an input.
        /// </summary>
        private static ImportFormat ResolveFormat(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();

            if (CsvExtensions.Contains(ext)) return ImportFormat.Csv;
            if (ExcelExtensions.Contains(ext)) return ImportFormat.Excel;

            throw new NotSupportedException(
                $"'{ext}' is not a supported import file. Use .csv or {string.Join(", ", ExcelExtensions)}.");
        }

        private void SyncFormatCombo(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            if (ExcelExtensions.Contains(ext)) cbImFormat.SelectedIndex = FormatIndexExcel;
            else if (CsvExtensions.Contains(ext)) cbImFormat.SelectedIndex = FormatIndexCsv;
        }

        private ImportRequest BuildRequest() => new ImportRequest
        {
            Target = cbTarget.SelectedIndex == 1 ? ImportTarget.Recovery : ImportTarget.Journal,
            Format = ResolveFormat(txtImFile.Text),
            FilePath = txtImFile.Text
        };

        private bool TryGetSelectedFile()
        {
            if (!string.IsNullOrWhiteSpace(txtImFile.Text) && File.Exists(txtImFile.Text))
                return true;

            MessageBox.Show("Please choose a file to import.", "Import",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // ------------------------------------------------------------------ handlers

        private void OnBrowseClicked(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Choose file to import",
                Filter = "Excel/CSV|*.xlsx;*.xlsm;*.csv|CSV|*.csv;*.txt|Excel|*.xlsx;*.xlsm;*.xltx;*.xltm|All files|*.*"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
                txtImFile.Text = ofd.FileName;   // TextChanged syncs the format combo
        }

        private void OnCancelClicked(object? sender, EventArgs e)
        {
            _cts?.Cancel();
            Close();
        }

        private async Task RunValidateAsync()
        {
            if (!TryGetSelectedFile()) return;

            gridPreview.DataSource = null;
            btnImportRun.Enabled = false;
            lblImStatus.Text = "Validating…";

            using var cts = new CancellationTokenSource();
            _cts = cts;
            SetBusy(true);

            try
            {
                var result = await _importer.ValidateAsync(BuildRequest(), cts.Token);

                lblImStatus.Text = result.Message ?? (result.IsValid ? "Validation OK." : "Validation failed.");
                if (result.Preview != null) gridPreview.DataSource = result.Preview;

                btnImportRun.Enabled = result.IsValid;
            }
            catch (OperationCanceledException) { lblImStatus.Text = "Cancelled."; }
            catch (NotSupportedException ex)
            {
                lblImStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                lblImStatus.Text = "Validation failed.";
                MessageBox.Show("Validation failed:\n" + ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
                _cts = null;
            }
        }

        private async Task RunImportAsync()
        {
            if (!TryGetSelectedFile()) return;

            lblImStatus.Text = "Importing…";

            using var cts = new CancellationTokenSource();
            _cts = cts;
            SetBusy(true);

            try
            {
                var (ok, message, rows) = await _importer.ImportAsync(BuildRequest(), cts.Token);

                lblImStatus.Text = message ?? (ok ? $"Imported {rows} row(s)." : "Import failed.");

                if (ok)
                {
                    MessageBox.Show(message ?? $"Import complete. Rows affected: {rows}",
                        "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(message ?? "Import failed.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (OperationCanceledException) { lblImStatus.Text = "Cancelled."; }
            catch (NotSupportedException ex)
            {
                lblImStatus.Text = ex.Message;
                MessageBox.Show(ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                lblImStatus.Text = "Import failed.";
                MessageBox.Show("Import failed:\n" + ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
                _cts = null;
            }
        }

        /// <summary>
        /// The progress bar cannot show real progress: IImportService reports no intermediate
        /// state. Marquee is honest about that; a bar stuck at 0 looked like a hang.
        /// </summary>
        private void SetBusy(bool busy)
        {
            progressImport.Style = busy ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
            if (!busy) progressImport.Value = 0;

            btnValidate.Enabled = !busy;
            btnImBrowse.Enabled = !busy;
            cbTarget.Enabled = !busy;
            if (busy) btnImportRun.Enabled = false;
        }

        // ------------------------------------------------------------------ theming

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
    }
}