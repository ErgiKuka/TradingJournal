using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;

namespace TradingJournal.Pl.PlaceHolder.Settings
{
    public partial class FrmSettings : UserControl, IResponsiveChildForm
    {
        private readonly ResponsiveLayoutManager _layoutManager;
        private SettingsManager _settings = new();
        private readonly DatabaseBackupService _bkp;
        private static readonly TimeSpan UNDO_TTL = TimeSpan.FromHours(24);

        // --- Responsive host wiring (normal <-> maximized) ---
        private Form? _hostForm;
        private bool _responsiveWired = false;

        private static bool IsDesignHost =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime
            || Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);

        public FrmSettings()
        {
            InitializeComponent();

            if (IsDesignHost)
                return;

            _settings = SettingsManager.Load();
            _bkp = new DatabaseBackupService(msg => Debug.WriteLine(msg));

            try
            {
                RoundedFormHelper.RoundPanel(pnlScroll, 20);
                RoundedFormHelper.RoundPanel(pnlAccountMoney, 16);
                RoundedFormHelper.RoundPanel(pnlThemeAndNotifications, 16);
                RoundedFormHelper.RoundPanel(pnlRecycleBin, 16);
                RoundedFormHelper.RoundPanel(pnlBackupRestore, 16);
                RoundedFormHelper.RoundPanel(pnlMaintenance, 16);
                RoundedFormHelper.RoundPanel(pnlImportExport, 16);
            }
            catch { }

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();
            WireResponsiveHost();
            WireEvents();

            dgvAccountHistory.CellPainting += DgvAccountHistory_CellPainting;
            ThemeManager.ThemeChanged += (s, e) =>
            {
                ApplyTheme();
            };

            BindSettingsToUi();
            ApplyTheme();

            _ = KickOffSilentRecycleBinPurge();
        }

        #region Events / Wiring
        private void WireEvents()
        {
            // Theme
            rbDarkMode.CheckedChanged += rbTheme_CheckedChanged;
            rbLightMode.CheckedChanged += rbTheme_CheckedChanged;

            // Account
            rbDeposit.CheckedChanged += (s, e) => UpdateTxnButtonText();
            rbWithdraw.CheckedChanged += (s, e) => UpdateTxnButtonText();
            btnChangeBalance.Click += btnChangeBalance_Click;
            btnResetBalance.Click += btnReset_Click;

            // Export (full)
            lnkExportAllTransactions.LinkClicked -= LnkExportAllTransactions_LinkClicked;
            lnkExportAllTransactions.LinkClicked += LnkExportAllTransactions_LinkClicked;

            // ---- Startup layout radios (PERSIST ONLY; do NOT reflow now)
            rbNormal.CheckedChanged += (s, e) =>
            {
                if (!rbNormal.Checked) return;
                ResponsiveStartupPrefs.SetDesired(FormWindowStateExtended.Normal);
            };
            rbMaximized.CheckedChanged += (s, e) =>
            {
                if (!rbMaximized.Checked) return;
                ResponsiveStartupPrefs.SetDesired(FormWindowStateExtended.Maximized);
            };

            // Recycle bin
            numRetentionDays.ValueChanged += (s, e) =>
            {
                _settings.RecycleBin.RetentionDays = (int)numRetentionDays.Value;
                _settings.Save();
            };

            btnOpenRecycleBin.Click += (s, e) =>
            {
                try
                {
                    using var dlg = new TradingJournal.Pl.PlaceHolder.Settings.DlgRecycleBin
                    {
                        StartPosition = FormStartPosition.CenterParent
                    };
                    dlg.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to open Recycle Bin:\n" + ex.Message,
                        "Recycle Bin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnEmptyRecycleBin.Click += async (s, e) =>
            {
                if (MessageBox.Show(this,
                        "Permanently delete all items from the Recycle Bin?",
                        "Empty Recycle Bin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                try
                {
                    using var db = new AppDbContext();
                    await RecycleBinService.EmptyAllAsync(db);
                    MessageBox.Show(this, "Recycle Bin emptied.", "Recycle Bin",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to empty Recycle Bin:\n" + ex.Message,
                        "Recycle Bin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Notifications
            chkNotifyRecoveryMilestone.CheckedChanged += (s, e) =>
            {
                _settings.Notifications.RecoveryMilestoneEnabled = chkNotifyRecoveryMilestone.Checked;
                _settings.Save();
            };
            numRecoveryMilestone.ValueChanged += (s, e) =>
            {
                _settings.Notifications.RecoveryMilestonePct = Convert.ToInt32(numRecoveryMilestone.Value);
                _settings.Save();
            };
            chkNotifyBackupStatus.CheckedChanged += (s, e) =>
            {
                _settings.Notifications.BackupStatusEnabled = chkNotifyBackupStatus.Checked;
                _settings.Save();
            };
            chkNotifyDailySummary.CheckedChanged += (s, e) =>
            {
                _settings.Notifications.DailySummaryEnabled = chkNotifyDailySummary.Checked;
                _settings.Save();
            };
            btnTestNotifications.Click += (s, e) => AppNotifier.ShowInfo("This is a test notification.");

            // Backup/Restore
            chkEnableAutoBackup.CheckedChanged += (s, e) =>
            {
                _settings.Backup.AutoEnabled = chkEnableAutoBackup.Checked;
                _settings.Save();
            };
            dtpBackupTime.ValueChanged += (s, e) =>
            {
                _settings.Backup.Time = dtpBackupTime.Value.ToString("HH:mm", CultureInfo.InvariantCulture);
                _settings.Save();
            };
            cbBackupFrequency.SelectedIndexChanged += (s, e) =>
            {
                _settings.Backup.Frequency = cbBackupFrequency.SelectedItem?.ToString() ?? "Daily";
                _settings.Save();
            };
            numKeepLast.ValueChanged += (s, e) =>
            {
                _settings.Backup.KeepLast = Convert.ToInt32(numKeepLast.Value);
                _settings.Save();
            };
            btnBackupNow.Click += btnBackupNow_Click;
            btnRestoreLatest.Click += btnRestoreLatest_Click;
            btnRestoreFrom.Click += btnRestoreFrom_Click;
            btnUndoLastRestore.Click += btnUndoLastRestore_Click;
            btnOpenBackupFolder.Click += (s, e) => _bkp.OpenBackupFolder();

            // Maintenance
            btnOpenLogsFolder.Click += (s, e) =>
            {
                try
                {
                    var logs = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "TradingJournal", "logs");
                    Directory.CreateDirectory(logs);
                    Process.Start(new ProcessStartInfo { FileName = "explorer", Arguments = logs, UseShellExecute = true });
                }
                catch { }
            };

            btnValidateDatabase.Click += (s, e) =>
            {
                try
                {
                    using var db = new AppDbContext();
                    db.Database.CanConnect();
                    MessageBox.Show("Database validated successfully.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database validation failed:\n" + ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCompactDatabase.Click += (s, e) =>
            {
                try
                {
                    using var db = new AppDbContext();
                    var path = DatabaseBackupService.GetLiveDbPath(db);
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        using var conn = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={path}");
                        conn.Open();
                        using var cmd = conn.CreateCommand();
                        cmd.CommandText = "VACUUM;";
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Database compacted (VACUUM).", "Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Compaction failed:\n" + ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Import / Export
            btnExport.Click += (s, e) => { using var dlg = new DlgExportOptions(); dlg.ShowDialog(this); };
            btnImport.Click += (s, e) => { using var dlg = new DlgImportOptions(); dlg.ShowDialog(this); };

            lnkExportAllTransactions.LinkClicked += async (s, e) =>
            {
                try
                {
                    using var sfd = new System.Windows.Forms.SaveFileDialog
                    {
                        Title = "Export Account Transactions (PDF)",
                        Filter = "PDF file|*.pdf",
                        FileName = $"transactions_{DateTime.Now:yyyyMMdd}.pdf"
                    };
                    if (sfd.ShowDialog(this) != DialogResult.OK) return;

                    var svc = new ExportService();
                    var req = new ExportRequest
                    {
                        IncludeJournal = false,
                        IncludeRecovery = false,
                        IncludeStatistics = false,
                        IncludeTransactions = true,
                        UseDateRange = false,
                        Format = ExportFormat.Pdf,
                        OutputPath = sfd.FileName
                    };
                    var res = await svc.ExportAsync(req);
                    MessageBox.Show(this, res.Success ? "Transactions exported." : ("Export failed: " + res.Message),
                        "Export", MessageBoxButtons.OK, res.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Export failed:\n" + ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Templates
            btnDownloadJournalTemplate.Click -= null;
            btnDownloadJournalTemplate.Click += (s, e) =>
            {
                try
                {
                    using var sfd = new System.Windows.Forms.SaveFileDialog
                    {
                        Title = "Save Journal Template",
                        Filter = "CSV file|*.csv",
                        FileName = "journal_template.csv"
                    };
                    if (sfd.ShowDialog(this) != DialogResult.OK) return;

                    var csv =
                        "Date,Symbol,Side,EntryPrice,ExitPrice,StopLoss,TakeProfit,Margin,ProfitLoss,ScreenshotLink\r\n" +
                        "2025-01-01,BTCUSDT,Long,42000,0,0,0,0.010000,0,\r\n" +
                        "2025-01-03,ETHUSDT,Short,2300,0,0,0,0.500000,0,";
                    File.WriteAllText(sfd.FileName, csv);

                    var readmePath = Path.Combine(Path.GetDirectoryName(sfd.FileName)!, "journal_template_README.txt");
                    var readme =
@"JOURNAL TEMPLATE (CSV)

Required columns:
- Date (YYYY-MM-DD)
- Symbol (e.g., BTCUSDT)
- Side (Long|Short)
- EntryPrice (decimal)
- Margin (decimal; your position size/qty)

Optional columns:
- ExitPrice, StopLoss, TakeProfit, ProfitLoss, ScreenshotLink

Notes:
- Use Long/Short (not Buy/Sell).
- Numbers can be plain decimals. Leave optional fields blank or 0.";
                    File.WriteAllText(readmePath, readme);

                    MessageBox.Show(this, $"Template saved:\n{sfd.FileName}\n{readmePath}", "Template",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to save template:\n" + ex.Message,
                        "Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnDownloadRecoveryTemplate.Click -= null;
            btnDownloadRecoveryTemplate.Click += (s, e) =>
            {
                try
                {
                    using var fbd = new FolderBrowserDialog { Description = "Choose a folder to save recovery templates" };
                    if (fbd.ShowDialog(this) != DialogResult.OK) return;

                    var casesPath = Path.Combine(fbd.SelectedPath, "recovery_cases_template.csv");
                    var allocsPath = Path.Combine(fbd.SelectedPath, "recovery_allocations_template.csv");
                    var readmePath = Path.Combine(fbd.SelectedPath, "recovery_templates_README.txt");

                    var casesCsv =
                        "CaseRef,Symbol,EntryDate,EntryPrice,InvestedUSDT,Quantity,Status\r\n" +
                        "RC-BTC-1,BTCUSDT,2025-01-01,42000,420,0.010000,Active\r\n" +
                        "RC-ETH-1,ETHUSDT,2025-02-10,2300,,0.500000,Active";
                    var allocCsv =
                        "CaseRef,Symbol,EntryDate,TradeDate,EntryPrice,InvestedUSDT,Quantity\r\n" +
                        "RC-BTC-1,, ,2025-01-05,41000,410,\r\n" +
                        ",ETHUSDT,2025-02-10,2025-02-12,2250,,0.250000";
                    var readme =
@"RECOVERY TEMPLATES (CSV)

CASES (recovery_cases_template.csv)
Columns:
- CaseRef (optional but recommended)
- Symbol (e.g., BTCUSDT)
- EntryDate (YYYY-MM-DD)
- EntryPrice (decimal)
- InvestedUSDT OR Quantity (provide at least one)
- Status (Active|Paused|Closed|WrittenOff) [optional]

ALLOCATIONS (recovery_allocations_template.csv)
Columns:
- CaseRef OR (Symbol + EntryDate) to link
- TradeDate (YYYY-MM-DD)
- EntryPrice (decimal)
- InvestedUSDT OR Quantity (provide at least one)";
                    File.WriteAllText(casesPath, casesCsv);
                    File.WriteAllText(allocsPath, allocCsv);
                    File.WriteAllText(readmePath, readme);

                    MessageBox.Show(this, $"Recovery templates saved:\n{casesPath}\n{allocsPath}\n{readmePath}",
                        "Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to save templates:\n" + ex.Message,
                        "Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }
        #endregion

        #region Responsive
        private void InitializeResponsiveLayouts()
        {
            // ---------- Account & Money ----------
            _layoutManager.RegisterControl(lblAccTitle, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 16), new Size(320, 40));
            _layoutManager.RegisterControl(lblAccCurrent, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 72), new Size(200, 28));
            _layoutManager.RegisterControl(lblCurrentBalance, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 108), new Size(300, 28));

            _layoutManager.RegisterControl(rbDeposit, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 152), new Size(120, 30));
            _layoutManager.RegisterControl(rbWithdraw, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(154, 152), new Size(120, 30));

            _layoutManager.RegisterControl(lblAccNew, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 200), new Size(420, 28));
            _layoutManager.RegisterControl(txtNewAccountBalance, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 242), new Size(260, 36));
            _layoutManager.RegisterControl(btnChangeBalance, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(300, 242), new Size(170, 36));
            _layoutManager.RegisterControl(btnResetBalance, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(1440, 242), new Size(170, 36));

            _layoutManager.RegisterControl(lblAcctHistoryHeader, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 300), new Size(360, 30));
            _layoutManager.RegisterControl(dgvAccountHistory, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 340), new Size(1600, 380));
            _layoutManager.RegisterControl(lblAcctHistoryNote, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 730), new Size(520, 26));
            _layoutManager.RegisterControl(lnkExportAllTransactions, pnlAccountMoney, pnlAccountMoney_Max,
                new Point(22, 760), new Size(360, 26));

            // ---------- Theme & Notifications ----------
            _layoutManager.RegisterControl(lblThemeAndNotificationsHeader, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 16), new Size(380, 40));
            _layoutManager.RegisterControl(groupBox1, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 70), new Size(360, 110));
            _layoutManager.RegisterControl(groupBox2, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(420, 70), new Size(360, 110));

            _layoutManager.RegisterControl(chkNotifyRecoveryMilestone, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 190), new Size(360, 32));
            _layoutManager.RegisterControl(lblRecoveryMilestoneCaption, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 232), new Size(300, 28));
            _layoutManager.RegisterControl(numRecoveryMilestone, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(28, 270), new Size(120, 28));
            _layoutManager.RegisterControl(lblRecoveryMilestoneSuffix, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(160, 270), new Size(140, 28));

            _layoutManager.RegisterControl(chkNotifyBackupStatus, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 308), new Size(260, 32));
            _layoutManager.RegisterControl(lblBackupNotifyHint, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 344), new Size(540, 30));
            _layoutManager.RegisterControl(chkNotifyDailySummary, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 382), new Size(430, 32));
            _layoutManager.RegisterControl(btnTestNotifications, pnlThemeAndNotifications, pnlThemeAndNotifications_Max,
                new Point(22, 424), new Size(200, 36));

            // ---------- Recycle bin ----------
            _layoutManager.RegisterControl(lblRecycleBinHeader, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(22, 16), new Size(420, 40));
            _layoutManager.RegisterControl(lblRecycleBinDesc, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(22, 70), new Size(900, 30));
            _layoutManager.RegisterControl(lblRetentionCaption, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(22, 110), new Size(220, 28));
            _layoutManager.RegisterControl(numRetentionDays, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(22, 144), new Size(120, 28));
            _layoutManager.RegisterControl(lblRetentionHint, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(160, 144), new Size(320, 28));

            _layoutManager.RegisterControl(btnOpenRecycleBin, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(22, 184), new Size(200, 36));
            _layoutManager.RegisterControl(btnEmptyRecycleBin, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(240, 184), new Size(220, 36));
            _layoutManager.RegisterControl(lblRecycleBinInfo, pnlRecycleBin, pnlRecycleBin_Max,
                new Point(22, 236), new Size(520, 24));

            // ---------- Backup & Restore ----------
            _layoutManager.RegisterControl(lblBkTitle, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 16), new Size(380, 40));
            _layoutManager.RegisterControl(chkEnableAutoBackup, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 70), new Size(260, 32));
            _layoutManager.RegisterControl(lbllBackupTime, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 110), new Size(180, 28));
            _layoutManager.RegisterControl(dtpBackupTime, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 144), new Size(160, 30));
            _layoutManager.RegisterControl(lbllBackupFrequency, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 190), new Size(140, 28));
            _layoutManager.RegisterControl(cbBackupFrequency, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 224), new Size(220, 30));
            _layoutManager.RegisterControl(lblKeepLast, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 270), new Size(140, 28));
            _layoutManager.RegisterControl(numKeepLast, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(22, 304), new Size(140, 30));
            _layoutManager.RegisterControl(lblKeepSuffix, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(170, 306), new Size(140, 28));

            _layoutManager.RegisterControl(btnBackupNow, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(480, 120), new Size(180, 36));
            _layoutManager.RegisterControl(btnOpenBackupFolder, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(680, 120), new Size(220, 36));
            _layoutManager.RegisterControl(btnRestoreLatest, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(480, 208), new Size(220, 36));
            _layoutManager.RegisterControl(btnUndoLastRestore, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(720, 208), new Size(220, 36));
            _layoutManager.RegisterControl(btnRestoreFrom, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(480, 296), new Size(200, 36));
            _layoutManager.RegisterControl(lblLastBackup, pnlBackupRestore, pnlBackupRestore_Max,
                new Point(380, 360), new Size(480, 30));

            // ---------- Maintenance ----------
            _layoutManager.RegisterControl(lblMaintTitle, pnlMaintenance, pnlMaintenance_Max,
                new Point(22, 16), new Size(500, 40));
            _layoutManager.RegisterControl(lblDbPath, pnlMaintenance, pnlMaintenance_Max,
                new Point(22, 70), new Size(200, 28));
            _layoutManager.RegisterControl(txtDbPath, pnlMaintenance, pnlMaintenance_Max,
                new Point(22, 104), new Size(600, 34));
            _layoutManager.RegisterControl(btnOpenLogsFolder, pnlMaintenance, pnlMaintenance_Max,
                new Point(980, 96), new Size(220, 36));
            _layoutManager.RegisterControl(btnValidateDatabase, pnlMaintenance, pnlMaintenance_Max,
                new Point(980, 156), new Size(220, 36));
            _layoutManager.RegisterControl(lblBackupPath, pnlMaintenance, pnlMaintenance_Max,
                new Point(22, 188), new Size(200, 28));
            _layoutManager.RegisterControl(txtBackupPath, pnlMaintenance, pnlMaintenance_Max,
                new Point(22, 222), new Size(600, 34));
            _layoutManager.RegisterControl(btnCompactDatabase, pnlMaintenance, pnlMaintenance_Max,
                new Point(980, 222), new Size(220, 36));

            // ---------- Import / Export ----------
            _layoutManager.RegisterControl(lblIETitle, pnlImportExport, pnlImportExport_Max,
                new Point(22, 16), new Size(280, 40));
            _layoutManager.RegisterControl(lblIEInfo, pnlImportExport, pnlImportExport_Max,
                new Point(22, 70), new Size(800, 60));
            _layoutManager.RegisterControl(btnExport, pnlImportExport, pnlImportExport_Max,
                new Point(22, 150), new Size(220, 36));
            _layoutManager.RegisterControl(btnImport, pnlImportExport, pnlImportExport_Max,
                new Point(260, 150), new Size(220, 36));
            _layoutManager.RegisterControl(btnDownloadJournalTemplate, pnlImportExport, pnlImportExport_Max,
                new Point(22, 230), new Size(280, 36));
            _layoutManager.RegisterControl(btnDownloadRecoveryTemplate, pnlImportExport, pnlImportExport_Max,
                new Point(320, 230), new Size(320, 36));
        }

        public void SetWindowState(FormWindowStateExtended state) => _layoutManager.SetWindowState(state);

        private void WireResponsiveHost()
        {
            if (_responsiveWired) return;
            this.HandleCreated += (_, __) => TryAttachToHostForm();
            this.ParentChanged += (_, __) => TryAttachToHostForm();
            TryAttachToHostForm();
            _responsiveWired = true;
        }

        private void TryAttachToHostForm()
        {
            if (IsDesignHost) return;

            var newHost = this.FindForm();
            if (ReferenceEquals(newHost, _hostForm))
            {
                UpdateStateFromHost();
                return;
            }

            if (_hostForm is not null)
            {
                _hostForm.SizeChanged -= HostForm_SizeChanged;
                _hostForm.HandleDestroyed -= HostForm_HandleDestroyed;
            }

            _hostForm = newHost;

            if (_hostForm is not null && !_hostForm.IsDisposed)
            {
                _hostForm.SizeChanged += HostForm_SizeChanged;
                _hostForm.HandleDestroyed += HostForm_HandleDestroyed;
                UpdateStateFromHost();
            }
        }

        private void HostForm_SizeChanged(object? sender, EventArgs e) => UpdateStateFromHost();

        private void HostForm_HandleDestroyed(object? sender, EventArgs e)
        {
            if (_hostForm is not null)
            {
                _hostForm.SizeChanged -= HostForm_SizeChanged;
                _hostForm.HandleDestroyed -= HostForm_HandleDestroyed;
                _hostForm = null;
            }
        }

        private void UpdateStateFromHost()
        {
            if (_hostForm is null) return;

            var state = _hostForm.WindowState == FormWindowState.Maximized
                ? FormWindowStateExtended.Maximized
                : FormWindowStateExtended.Normal;

            _layoutManager.SetWindowState(state);
        }
        #endregion

        #region Bind + Theme
        private void BindSettingsToUi()
        {
            if (IsDesignHost) return;

            // Account
            UpdateBalanceLabel();
            txtNewAccountBalance.Text = string.Empty;
            rbDeposit.Checked = true;
            UpdateTxnButtonText();

            LoadRecentAccountHistory();

            // Theme
            if (_settings.Theme == AppTheme.Dark) rbDarkMode.Checked = true;
            else rbLightMode.Checked = true;

            // Notifications
            chkNotifyRecoveryMilestone.Checked = _settings.Notifications.RecoveryMilestoneEnabled;
            numRecoveryMilestone.Value = Math.Max(1, Math.Min(100, _settings.Notifications.RecoveryMilestonePct));
            chkNotifyBackupStatus.Checked = _settings.Notifications.BackupStatusEnabled;
            chkNotifyDailySummary.Checked = _settings.Notifications.DailySummaryEnabled;

            // Startup layout radios reflect persisted preference
            var saved = ResponsiveStartupPrefs.GetDesired();
            if (saved == FormWindowStateExtended.Maximized) rbMaximized.Checked = true;
            else rbNormal.Checked = true;

            // Recycle Bin
            numRetentionDays.Value = Math.Max(1, Math.Min(180, _settings.RecycleBin.RetentionDays == 0 ? 30 : _settings.RecycleBin.RetentionDays));

            // Backup schedule
            chkEnableAutoBackup.Checked = _settings.Backup.AutoEnabled;
            var timeOk = DateTime.TryParseExact(_settings.Backup.Time, "HH:mm", CultureInfo.InvariantCulture,
                                                DateTimeStyles.None, out var t);
            var time = timeOk ? t : DateTime.Today.AddHours(23);
            dtpBackupTime.Value = DateTime.Today.AddHours(time.Hour).AddMinutes(time.Minute);

            cbBackupFrequency.SelectedItem = _settings.Backup.Frequency switch
            {
                "Weekly" => "Weekly",
                "Monthly" => "Monthly",
                _ => "Daily"
            };
            numKeepLast.Value = Math.Max(1, Math.Min(50, _settings.Backup.KeepLast == 0 ? 15 : _settings.Backup.KeepLast));

            // Maintenance paths
            try
            {
                using var db = new AppDbContext();
                txtDbPath.Text = DatabaseBackupService.GetLiveDbPath(db);
                txtBackupPath.Text = _bkp.BackupRoot;
            }
            catch { }

            UpdateLastBackupLabel();
        }

        private void LnkExportAllTransactions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using var dlg = new DlgExportOptions { StartPosition = FormStartPosition.CenterParent, ShowInTaskbar = false };
                dlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to open Export dialog:\n" + ex.Message,
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyTheme()
        {
            if (IsDesignHost) return;

            this.BackColor = ThemeManager.BackgroundColor;
            pnlScroll.BackColor = ThemeManager.BackgroundColor;

            foreach (var p in new[] { pnlAccountMoney, pnlThemeAndNotifications, pnlRecycleBin, pnlBackupRestore, pnlMaintenance, pnlImportExport,
                                      pnlAccountMoney_Max, pnlThemeAndNotifications_Max, pnlRecycleBin_Max, pnlBackupRestore_Max, pnlMaintenance_Max, pnlImportExport_Max })
            {
                p.BackColor = ThemeManager.PanelColor;
            }

            foreach (var lbl in this.Controls.OfType<Control>().SelectMany(FindLabels))
                lbl.ForeColor = ThemeManager.TextColor;

            foreach (var tb in this.Controls.OfType<Control>().SelectMany(FindTextBoxes))
            {
                tb.BackColor = ThemeManager.TextBoxColor;
                tb.ForeColor = ThemeManager.TextColor;
            }

            foreach (var btn in this.Controls.OfType<Control>().SelectMany(FindIconButtons))
            {
                if (btn == btnChangeBalance || btn == btnResetBalance) continue;
                btn.BackColor = ThemeManager.DarkButtonColor;
                btn.ForeColor = ThemeManager.ActionButtonTextColor;
                btn.FlatAppearance.BorderSize = 0;
            }

            btnResetBalance.BackColor = Color.FromArgb(180, 40, 40);
            btnResetBalance.ForeColor = ThemeManager.ActionButtonTextColor;
            btnResetBalance.FlatAppearance.BorderSize = 0;

            UpdateTxnButtonText();
            ApplyAccountGridStyling();

            cbBackupFrequency.BackColor = ThemeManager.PanelColor;
            cbBackupFrequency.ForeColor = ThemeManager.TextColor;

            groupBox1.ForeColor = ThemeManager.TextColor;
            groupBox2.ForeColor = ThemeManager.TextColor;

            btnBackupNow.ForeColor = ThemeManager.TextColor;
            btnRestoreLatest.ForeColor = ThemeManager.TextColor;
            btnRestoreFrom.ForeColor = ThemeManager.TextColor;
            btnUndoLastRestore.ForeColor = ThemeManager.TextColor;
            btnOpenBackupFolder.ForeColor = ThemeManager.TextColor;
            btnDownloadJournalTemplate.ForeColor = ThemeManager.TextColor;
            btnDownloadRecoveryTemplate.ForeColor = ThemeManager.TextColor;
            btnExport.ForeColor = ThemeManager.TextColor;
            btnImport.ForeColor = ThemeManager.TextColor;
            btnValidateDatabase.ForeColor = ThemeManager.TextColor;
            btnCompactDatabase.ForeColor = ThemeManager.TextColor;
            btnExport.ForeColor = ThemeManager.TextColor;
            btnTestNotifications.ForeColor = ThemeManager.TextColor;
            btnEmptyRecycleBin.ForeColor = ThemeManager.TextColor;
            btnOpenRecycleBin.ForeColor = ThemeManager.TextColor;
            btnOpenLogsFolder.ForeColor = ThemeManager.TextColor;

            rbDarkMode.ForeColor = ThemeManager.TextColor;
            rbLightMode.ForeColor = ThemeManager.TextColor;
            rbDeposit.ForeColor = ThemeManager.TextColor;
            rbWithdraw.ForeColor = ThemeManager.TextColor;
            rbNormal.ForeColor = ThemeManager.TextColor;
            rbMaximized.ForeColor = ThemeManager.TextColor;

            dtpBackupTime.CalendarMonthBackground = ThemeManager.PanelColor;
            dtpBackupTime.CalendarForeColor = ThemeManager.TextColor;


            rbDarkMode.ForeColor = ThemeManager.TextColor;
            rbLightMode.ForeColor = ThemeManager.TextColor;
            chkNotifyRecoveryMilestone.ForeColor = ThemeManager.TextColor;
            chkNotifyBackupStatus.ForeColor = ThemeManager.TextColor;
            chkNotifyDailySummary.ForeColor = ThemeManager.TextColor;
            chkEnableAutoBackup.ForeColor = ThemeManager.TextColor;

            if (lnkExportAllTransactions != null)
            {
                lnkExportAllTransactions.LinkColor = ThemeManager.AccentColor;
                lnkExportAllTransactions.ActiveLinkColor = ThemeManager.AccentColor;
                lnkExportAllTransactions.VisitedLinkColor = ThemeManager.AccentColor;
                lnkExportAllTransactions.ForeColor = ThemeManager.TextColor;
                lnkExportAllTransactions.LinkBehavior = LinkBehavior.HoverUnderline;
                lnkExportAllTransactions.Enabled = true;
                lnkExportAllTransactions.TabStop = true;
            }
        }


        private static Label[] FindLabels(Control root) =>
            root.Controls.OfType<Label>().Concat(root.Controls.Cast<Control>().SelectMany(FindLabels)).ToArray();

        private static TextBox[] FindTextBoxes(Control root) =>
            root.Controls.OfType<TextBox>().Concat(root.Controls.Cast<Control>().SelectMany(FindTextBoxes)).ToArray();

        private static FontAwesome.Sharp.IconButton[] FindIconButtons(Control root) =>
            root.Controls.OfType<FontAwesome.Sharp.IconButton>().Concat(root.Controls.Cast<Control>().SelectMany(FindIconButtons)).ToArray();

        private void UpdateBalanceLabel() => lblCurrentBalance.Text = _settings.AccountBalance.ToString("N2");
        #endregion

        #region Handlers (Account / Theme / Backup / Undo)
        private void UpdateTxnButtonText()
        {
            btnChangeBalance.Text = rbDeposit.Checked ? "Deposit" : "Withdraw";
            btnChangeBalance.BackColor = rbDeposit.Checked ? Color.FromArgb(30, 95, 58) : Color.FromArgb(95, 30, 30);
            btnChangeBalance.ForeColor = ThemeManager.ActionButtonTextColor;
        }

        private void btnChangeBalance_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtNewAccountBalance.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var amount) || amount <= 0)
            {
                MessageBox.Show(this, "Please enter a valid positive number.", "Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rbDeposit.Checked) _settings.AccountBalance += amount;
            else _settings.AccountBalance -= amount;

            _settings.Save();
            UpdateBalanceLabel();
            txtNewAccountBalance.Clear();

            try
            {
                using var db = new AppDbContext();
                AccountTransactionsService.Append(db, new AccountTransactionRecord
                {
                    Date = DateTime.Now,
                    Type = rbDeposit.Checked ? AccountTransactionType.Deposit : AccountTransactionType.Withdraw,
                    Amount = amount,
                    Note = ""
                });
            }
            catch { }

            LoadRecentAccountHistory();

            var msg = rbDeposit.Checked ? "Deposit recorded." : "Withdrawal recorded.";
            MessageBox.Show(this, msg, "Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rbTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked) return;
            _settings.Theme = rbDarkMode.Checked ? AppTheme.Dark : AppTheme.Light;
            _settings.Save();
            ThemeManager.SetTheme(_settings.Theme);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this,
                    "Reset Account Balance to 0 and revert theme to Dark?",
                    "Reset",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            _settings.AccountBalance = 0;
            _settings.Theme = AppTheme.Dark;
            _settings.Save();

            ThemeManager.SetTheme(_settings.Theme);

            BindSettingsToUi();
            MessageBox.Show(this, "Settings reset.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBackupNow_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            if (_bkp.BackupNow(db, out var path, keep: _settings.Backup.KeepLast))
            {
                UpdateLastBackupLabel(forceType: "Manual");
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowInfo("Backup created successfully.");
                MessageBox.Show(this, $"Backup created:\n{path}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowError("Backup failed.");
                MessageBox.Show(this, "Backup failed.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestoreLatest_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            var latest = _bkp.GetBackups().FirstOrDefault();
            if (latest == null)
            {
                MessageBox.Show(this, "No backups found.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var msg = $"Restore from latest backup?\n\n{latest.Name}\nCreated: {latest.CreationTime}\n\n" +
                      "Restoring will overwrite current data. Continue?";
            if (MessageBox.Show(this, msg, "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (MessageBox.Show(this, "Final confirmation: Proceed?\nThe app will restart.", "Confirm Restore",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (_bkp.RestoreFromFileWithUndo(db, latest.FullName, $"Latest: {latest.Name}"))
            {
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowWarning("Database restored. App will restart.");
                MessageBox.Show(this, "Restored. The app will restart.", "Restore",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowError("Restore failed.");
                MessageBox.Show(this, "Restore failed.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestoreFrom_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            using var ofd = new System.Windows.Forms.OpenFileDialog
            {
                InitialDirectory = _bkp.BackupRoot,
                Filter = "TradingJournal backups (*.sqlite.bak)|*.sqlite.bak|All files (*.*)|*.*",
                Title = "Choose a backup to restore"
            };
            if (ofd.ShowDialog(this) != DialogResult.OK) return;

            if (MessageBox.Show(this, $"Restore from:\n{ofd.FileName}\n\nOverwrite current data?", "Confirm Restore",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (MessageBox.Show(this, "Final confirmation: Proceed?\nThe app will restart.", "Confirm Restore",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (_bkp.RestoreFromFileWithUndo(db, ofd.FileName, $"Chosen: {Path.GetFileName(ofd.FileName)}"))
            {
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowWarning("Database restored. App will restart.");
                MessageBox.Show(this, "Restored. The app will restart.", "Restore",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowError("Restore failed.");
                MessageBox.Show(this, "Restore failed.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUndoLastRestore_Click(object sender, EventArgs e)
        {
            if (!_bkp.TryGetUndoInfo(out var info))
            {
                MessageBox.Show(this, "No undo snapshot available.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var age = DateTime.UtcNow - info.CreatedUtc;
            var expired = age > UNDO_TTL;
            var details = $"Undo snapshot:\n- File: {info.UndoPath}\n- Created (UTC): {info.CreatedUtc:yyyy-MM-dd HH:mm:ss}\n" +
                          $"- Source: {info.SourceLabel}\n- Age: {age:g}\n" +
                          (expired ? "- Status: EXPIRED\n" : $"- Expires in: {(UNDO_TTL - age):g}\n");

            if (expired)
            {
                if (MessageBox.Show(this, details + "\nThis undo has expired. Clear it now?",
                    "Undo (Expired)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _bkp.ClearUndo(deleteUndoFile: false);
                    MessageBox.Show(this, "Expired undo cleared.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            if (MessageBox.Show(this, details + "\nUndo the most recent restore? This will overwrite current data.",
                "Confirm Undo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            if (MessageBox.Show(this, "Final confirmation: proceed with UNDO?\nThe app will restart.",
                "Confirm Undo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            using var db = new AppDbContext();
            if (_bkp.UndoLastRestore(db))
            {
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowWarning("Restore undone. App will restart.");
                MessageBox.Show(this, "Undo complete. The app will restart.", "Undo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                if (_settings.Notifications.BackupStatusEnabled)
                    AppNotifier.ShowError("Undo failed.");
                MessageBox.Show(this, "Undo failed.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Helpers (history + last backup label + silent purge)
        private void LoadRecentAccountHistory()
        {
            try
            {
                using var db = new AppDbContext();
                var since = DateTime.Today.AddDays(-180);

                var rows = AccountTransactionsService.QuerySince(db, since)
                    .OrderByDescending(x => x.Date)
                    .Select(x => new { x.Date, x.Type, Amount = x.Amount })
                    .ToList();

                dgvAccountHistory.DataSource = rows;

                const string delCol = "DeleteColumn";
                if (!dgvAccountHistory.Columns.Contains(delCol))
                {
                    var btn = new DataGridViewButtonColumn
                    {
                        Name = delCol,
                        HeaderText = "Delete",
                        Text = "Delete",
                        UseColumnTextForButtonValue = true
                    };
                    dgvAccountHistory.Columns.Add(btn);
                    dgvAccountHistory.Columns[delCol].DisplayIndex = 0;
                    dgvAccountHistory.CellContentClick += DgvAccountHistory_CellContentClick;
                }
                else
                {
                    dgvAccountHistory.Columns[delCol].DisplayIndex = 0;
                }

                ApplyAccountGridStyling();
            }
            catch
            {
                dgvAccountHistory.DataSource = null;
            }
        }

        private void DgvAccountHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvAccountHistory.Columns[e.ColumnIndex] is not DataGridViewButtonColumn) return;

            var row = dgvAccountHistory.Rows[e.RowIndex].DataBoundItem;
            if (row == null) return;

            if (MessageBox.Show(this, "Delete this transaction?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using var db = new AppDbContext();
                AccountTransactionsService.DeleteMatching(db, row);
                LoadRecentAccountHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to delete transaction:\n" + ex.Message,
                    "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvAccountHistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var delIndex = dgvAccountHistory.Columns["DeleteColumn"].Index;
            if (e.ColumnIndex != delIndex) return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            var buttonBounds = e.CellBounds;
            buttonBounds.Inflate(-2, -2);
            var buttonColor = Color.FromArgb(220, 53, 69);

            ControlPaint.DrawButton(e.Graphics, buttonBounds, ButtonState.Normal);
            using var br = new SolidBrush(buttonColor);
            e.Graphics.FillRectangle(br, buttonBounds);

            TextRenderer.DrawText(e.Graphics, "Delete", e.CellStyle.Font, buttonBounds, Color.White,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

            e.Handled = true;
        }

        private void ApplyAccountGridStyling()
        {
            var g = dgvAccountHistory;
            g.BackgroundColor = ThemeManager.DataGrid;
            g.GridColor = Color.FromArgb(45, 51, 73);
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.DataGridHeader;
            g.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            g.ColumnHeadersHeight = 30;
            g.EnableHeadersVisualStyles = false;
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
        }


        private void UpdateLastBackupLabel(string? forceType = null)
        {
            try
            {
                var last = _bkp.GetBackups().FirstOrDefault();
                if (last == null)
                {
                    lblLastBackup.Text = "Last Backup: —";
                    return;
                }

                var type = forceType ?? "Automatic/Manual";
                lblLastBackup.Text = $"Last Backup: {last.CreationTime:G} ({type})";
            }
            catch
            {
                lblLastBackup.Text = "Last Backup: —";
            }
        }

        public void RefreshLastBackupLabelForAuto()
        {
            var method = GetType().GetMethod("UpdateLastBackupLabel",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            method?.Invoke(this, new object?[] { "Automatic" });
        }

        private async Task KickOffSilentRecycleBinPurge()
        {
            try
            {
                using var db = new AppDbContext();
                await RecycleBinService.PurgeExpiredAsync(db);
            }
            catch { }
        }

        // Daily summary helper left as-is (unchanged)
        public static void TryNotifyDailySummaryIfNeeded(SettingsManager s)
        {
            if (!(s?.Notifications?.DailySummaryEnabled ?? false)) return;

            const string rt = @"Software\TradingJournal\UX";
            const string name = "DailySummaryLastShown";
            try
            {
                using var k = Registry.CurrentUser.CreateSubKey(rt);
                var raw = k?.GetValue(name)?.ToString();
                if (DateTime.TryParse(raw, out var last) && last.Date == DateTime.Now.Date) return;

                AppNotifier.ShowInfo("Daily Summary — Have a great trading day!");
                k?.SetValue(name, DateTime.Now.ToString("O"), RegistryValueKind.String);
            }
            catch { }
        }
        #endregion
    }
}
