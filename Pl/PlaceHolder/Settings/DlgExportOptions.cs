using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Pl.PlaceHolder.Settings
{
    public partial class DlgExportOptions : Form
    {
        private CancellationTokenSource _cts;

        public DlgExportOptions()
        {
            InitializeComponent();
            WireEvents();
            ApplyDefaults();

            ApplyTheme();
        }

        private void WireEvents()
        {
            btnBrowseSave.Click += BtnBrowseSave_Click;
            btnExportRun.Click += BtnExportRun_Click;
            btnExportCancel.Click += BtnExportCancel_Click;

            txtSavePath.TextChanged += (s, e) => RefreshExportButtonEnabled();
            chkSrcJournal.CheckedChanged += (s, e) => { UpdateSaveUiHint(); RefreshExportButtonEnabled(); };
            chkSrcRecovery.CheckedChanged += (s, e) => { UpdateSaveUiHint(); RefreshExportButtonEnabled(); };
            chkSrcStatistics.CheckedChanged += (s, e) => { UpdateSaveUiHint(); RefreshExportButtonEnabled(); };
            chkSrcTransactions.CheckedChanged += (s, e) => { UpdateSaveUiHint(); RefreshExportButtonEnabled(); };

            chkUseDateRange.CheckedChanged += (s, e) =>
            {
                dtFrom.Enabled = dtTo.Enabled = chkUseDateRange.Checked;
            };
            cbFormat.SelectedIndexChanged += (s, e) => UpdateSaveUiHint();
        }

        private void ApplyDefaults()
        {
            chkSrcJournal.Checked = true;
            chkSrcRecovery.Checked = false;
            chkSrcStatistics.Checked = false;
            chkSrcTransactions.Checked = false;

            if (cbFormat.Items.Count > 0) cbFormat.SelectedIndex = 0;

            chkUseDateRange.Checked = false;
            dtFrom.Enabled = dtTo.Enabled = false;
            dtFrom.Value = DateTime.Today.AddDays(-30);
            dtTo.Value = DateTime.Today;

            // Default folder (used when multi-source CSV)
            txtSavePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            progressExport.Style = ProgressBarStyle.Blocks;
            progressExport.Value = 0;
            txtSavePath.Clear();
            UpdateSaveUiHint();
            RefreshExportButtonEnabled();
        }

        private ExportFormat GetSelectedFormat()
        {
            var fmtStr = (cbFormat.SelectedItem?.ToString() ?? "Excel").ToLower(CultureInfo.InvariantCulture);
            return fmtStr.Contains("csv") ? ExportFormat.Csv :
                   fmtStr.Contains("pdf") ? ExportFormat.Pdf :
                   ExportFormat.Excel;
        }

        private int CountSelectedSources()
        {
            return (chkSrcJournal.Checked ? 1 : 0) +
                   (chkSrcRecovery.Checked ? 1 : 0) +
                   (chkSrcStatistics.Checked ? 1 : 0) +
                   (chkSrcTransactions.Checked ? 1 : 0);
        }

        private bool IsMultiCsv(ExportFormat fmt, int srcCount) => fmt == ExportFormat.Csv && srcCount > 1;

        private string NormalizeSavePath(string rawPath, ExportFormat fmt, bool multiCsv)
        {
            if (string.IsNullOrWhiteSpace(rawPath)) return rawPath ?? "";


            if (multiCsv)
            {
                // Must be a folder; if a file was provided, convert to its directory
                if (File.Exists(rawPath))
                    return Path.GetDirectoryName(rawPath) ?? rawPath;

                // If it looks like a file path (has extension), turn it into a folder with same name (optional)
                if (!Directory.Exists(rawPath) && !string.IsNullOrEmpty(Path.GetExtension(rawPath)))
                {
                    var dir = Path.GetDirectoryName(rawPath);
                    var stem = Path.GetFileNameWithoutExtension(rawPath);
                    return Path.Combine(string.IsNullOrEmpty(dir) ? Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) : dir, stem);
                }
                return rawPath;
            }
            else
            {
                // Single file; enforce extension
                var dir = Path.GetDirectoryName(rawPath);
                var stem = Path.GetFileNameWithoutExtension(rawPath);
                var ext = fmt == ExportFormat.Excel ? ".xlsx" :
                          fmt == ExportFormat.Pdf ? ".pdf" : ".csv";
                if (string.IsNullOrWhiteSpace(stem)) stem = "export";
                if (string.IsNullOrWhiteSpace(dir))
                    dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                return Path.Combine(dir, stem + ext);
            }
        }

        private bool PathLooksValidForCurrentChoice(out string reason)
        {
            reason = "";
            var fmt = GetSelectedFormat();
            var srcCount = CountSelectedSources();
            var multiCsv = IsMultiCsv(fmt, srcCount);
            var p = txtSavePath.Text?.Trim() ?? "";

            if (string.IsNullOrEmpty(p)) { reason = "Choose a save location."; return false; }

            if (multiCsv)
            {
                // Folder must exist or be creatable
                try
                {
                    if (!Directory.Exists(p))
                        Directory.CreateDirectory(p);
                    return true;
                }
                catch (Exception ex)
                {
                    reason = $"Folder not valid: {ex.Message}";
                    return false;
                }
            }
            else
            {
                // Single file: directory must exist or be creatable
                try
                {
                    var norm = NormalizeSavePath(p, fmt, multiCsv: false);
                    var dir = Path.GetDirectoryName(norm);
                    if (string.IsNullOrEmpty(dir)) { reason = "Invalid path."; return false; }
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    // Check extension
                    var ext = Path.GetExtension(norm).ToLowerInvariant();
                    if (fmt == ExportFormat.Excel && ext != ".xlsx") { reason = "Excel export needs .xlsx"; return false; }
                    if (fmt == ExportFormat.Pdf && ext != ".pdf") { reason = "PDF export needs .pdf"; return false; }
                    if (fmt == ExportFormat.Csv && ext != ".csv") { reason = "CSV export needs .csv"; return false; }
                    return true;
                }
                catch (Exception ex)
                {
                    reason = $"Save path invalid: {ex.Message}";
                    return false;
                }
            }
        }

        private void RefreshExportButtonEnabled()
        {
            var anySrc = CountSelectedSources() > 0;
            var ok = anySrc && PathLooksValidForCurrentChoice(out _);
            btnExportRun.Enabled = ok;
        }


        private void UpdateSaveUiHint()
        {
            var fmt = GetSelectedFormat();
            var sources = CountSelectedSources();
            var multiCsv = IsMultiCsv(fmt, sources);

            lblSaveTo.Text = multiCsv ? "Save folder" : "Save as";

            // Normalize/align extension or folder hint live (but preserve directory if user already typed one)
            var current = txtSavePath.Text?.Trim() ?? "";
            txtSavePath.Text = NormalizeSavePath(current, fmt, multiCsv);
        }

        private void BtnBrowseSave_Click(object sender, EventArgs e)
        {
            var format = (cbFormat.SelectedItem?.ToString() ?? "Excel").ToLower(CultureInfo.InvariantCulture);
            var sources = (chkSrcJournal.Checked ? 1 : 0) +
                          (chkSrcRecovery.Checked ? 1 : 0) +
                          (chkSrcStatistics.Checked ? 1 : 0) +
                          (chkSrcTransactions.Checked ? 1 : 0);
            var multiCsv = format.Contains("csv") && sources > 1;

            if (multiCsv)
            {
                using var fbd = new FolderBrowserDialog { Description = "Choose a folder for multiple CSV files." };
                if (fbd.ShowDialog(this) == DialogResult.OK)
                    txtSavePath.Text = fbd.SelectedPath;
                return;
            }

            using var sfd = new SaveFileDialog();
            if (format.Contains("csv"))
            {
                sfd.Filter = "CSV file|*.csv";
                sfd.FileName = "export.csv";
            }
            else if (format.Contains("pdf"))
            {
                sfd.Filter = "PDF file|*.pdf";
                sfd.FileName = "export.pdf";
            }
            else
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.FileName = "export.xlsx";
            }

            if (sfd.ShowDialog(this) == DialogResult.OK)
                txtSavePath.Text = sfd.FileName;
        }

        private async void BtnExportRun_Click(object sender, EventArgs e)
        {
            // Validate again
            if (!PathLooksValidForCurrentChoice(out var reason))
            {
                MessageBox.Show(this, reason, "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var format = GetSelectedFormat();
            var useRange = chkUseDateRange.Checked;
            var from = useRange ? dtFrom.Value.Date : (DateTime?)null;
            var to = useRange ? dtTo.Value.Date : (DateTime?)null;

            // Normalize path definitively (prevents the Excel/CSV mismatch issue)
            var sources = CountSelectedSources();
            var multiCsv = IsMultiCsv(format, sources);
            var normalizedPath = NormalizeSavePath(txtSavePath.Text, format, multiCsv);
            txtSavePath.Text = normalizedPath;  // reflect to UI

            // Build request
            var req = new ExportRequest
            {
                IncludeJournal = chkSrcJournal.Checked,
                IncludeRecovery = chkSrcRecovery.Checked,
                IncludeStatistics = chkSrcStatistics.Checked,
                IncludeTransactions = chkSrcTransactions.Checked,
                UseDateRange = useRange,
                From = from,
                To = to,
                IncludeComputed = checkBox1.Checked,
                Format = format,
                OutputPath = normalizedPath
            };

            ToggleInputs(false);
            progressExport.Style = ProgressBarStyle.Marquee;
            _cts = new CancellationTokenSource();

            try
            {
                var svc = new ExportService();
                var result = await svc.ExportAsync(req, null, _cts.Token);
                progressExport.Style = ProgressBarStyle.Blocks;
                progressExport.Value = result.Success ? 100 : 0;

                if (result.Success)
                {
                    var what = format == ExportFormat.Csv && multiCsv
                        ? $"Created {result.CreatedPaths.Count} CSV file(s) in:\n{normalizedPath}"
                        : $"Created:\n{string.Join("\n", result.CreatedPaths)}";

                    var details = string.IsNullOrWhiteSpace(result.Message) ? "" : $"\n\n{result.Message}";
                    MessageBox.Show(this, $"Export completed.\n\nCreated:\n{string.Join("\n", result.CreatedPaths)}{details}",
                        "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtSavePath.Clear();
                    txtSavePath.SelectAll();
                }
                else
                {
                    MessageBox.Show(this, result.Message ?? "Export failed.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show(this, "Export cancelled.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Export failed:\n{ex.Message}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleInputs(true);
                progressExport.Style = ProgressBarStyle.Blocks;
                progressExport.Value = 0;
                _cts?.Dispose();
                _cts = null;
                RefreshExportButtonEnabled(); // re-check validity
            }
        }

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            btnBrowseSave.BackColor = ThemeManager.ButtonColor;
            btnBrowseSave.ForeColor = ThemeManager.TextColor;
            btnExportCancel.BackColor = ThemeManager.ButtonColor;
            btnExportCancel.ForeColor = ThemeManager.TextColor;
            btnExportRun.BackColor = ThemeManager.ButtonColor;
            btnExportRun.ForeColor = ThemeManager.TextColor;

            lblExTitle.ForeColor = ThemeManager.TextColor;
            lblSaveTo.ForeColor = ThemeManager.TextColor;
            lblFormat.ForeColor = ThemeManager.TextColor;
            lblFrom.ForeColor = ThemeManager.TextColor;
            lblTo.ForeColor = ThemeManager.TextColor;

            chkSrcJournal.ForeColor = ThemeManager.TextColor;
            chkSrcRecovery.ForeColor = ThemeManager.TextColor;
            chkSrcStatistics.ForeColor = ThemeManager.TextColor;
            chkSrcTransactions.ForeColor = ThemeManager.TextColor;
            chkUseDateRange.ForeColor = ThemeManager.TextColor;
            checkBox1.ForeColor = ThemeManager.TextColor;

            gpbExportFormat.ForeColor = ThemeManager.TextColor;
            gpbExportPath.ForeColor = ThemeManager.TextColor;
            gpbExportSources.ForeColor = ThemeManager.TextColor;
            gpbExportDateRange.ForeColor = ThemeManager.TextColor;

            cbFormat.BackColor = ThemeManager.PanelColor;
            cbFormat.ForeColor = ThemeManager.TextColor;
            cbFormat.FlatStyle = FlatStyle.Flat;

            txtSavePath.BackColor = ThemeManager.TextBoxColor;
            txtSavePath.ForeColor = ThemeManager.TextColor;
        }


        private void BtnExportCancel_Click(object sender, EventArgs e) => _cts?.Cancel();

        private void ToggleInputs(bool enabled)
        {
            foreach (Control c in Controls) c.Enabled = enabled;
            if (!enabled) btnExportCancel.Enabled = true;
        }
    }
}
