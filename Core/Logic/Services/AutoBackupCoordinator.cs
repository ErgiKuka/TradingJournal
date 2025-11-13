using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic.Helpers;   // AppNotifier
using TradingJournal.Core.Logic.Manager;   // SettingsManager

namespace TradingJournal.Core.Logic.Services
{
    /// <summary>
    /// Runs the automatic backup once per LOCAL day after the scheduled time,
    /// respecting Daily/Weekly/Monthly. Works on startup and with a minute scheduler.
    /// </summary>
    public static class AutoBackupCoordinator
    {
        private static System.Windows.Forms.Timer? _timer;
        private static bool _isRunning = false;

        /// <summary>Raised after a successful automatic backup. Arg = created backup path.</summary>
        public static event Action<string>? BackupCompleted;

        /// <summary>Call this once on app start to run an immediate check AND start a 1-min scheduler.</summary>
        public static void StartScheduler()
        {
            // Run once at startup
            RunIfDue();

            // Minute tick (idempotent thanks to daily marker)
            _timer ??= new System.Windows.Forms.Timer { Interval = 60_000 };
            _timer.Tick -= OnTick;
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private static void OnTick(object? s, EventArgs e) => RunIfDue();

        /// <summary>
        /// Checks settings and performs an automatic backup if due (once per local day, after scheduled time).
        /// Safe to call repeatedly; will not duplicate thanks to a daily marker file.
        /// </summary>
        public static void RunIfDue()
        {
            if (_isRunning) return;
            _isRunning = true;
            try
            {
                var settings = SettingsManager.Load();
                if (settings?.Backup is null || !settings.Backup.AutoEnabled)
                    return;

                var nowLocal = DateTime.Now;
                if (!IsScheduledDay(settings.Backup.Frequency, nowLocal))
                    return;

                // Parse "HH:mm" or fallback to 23:00
                var (hh, mm) = ParseTimeOrDefault(settings.Backup.Time, 23, 0);
                var scheduledTodayLocal = new DateTime(nowLocal.Year, nowLocal.Month, nowLocal.Day, hh, mm, 0);
                Debug.WriteLine($"[AutoBackup] Enabled={settings.Backup.AutoEnabled}, Freq={settings.Backup.Frequency}, Time={settings.Backup.Time}, Now={DateTime.Now}");

                // Only after scheduled time
                if (nowLocal < scheduledTodayLocal)
                    return;

                using var db = new AppDbContext();
                var backupSvc = new DatabaseBackupService(_ => { });

                // Marker to ensure once per LOCAL day
                var markerPath = Path.Combine(backupSvc.BackupRoot, $"auto-{nowLocal:yyyy-MM-dd}.flag");
                if (File.Exists(markerPath))
                    return;

                var ok = backupSvc.BackupNow(db, out var createdPath, keep: settings.Backup.KeepLast);
                if (ok)
                {
                    File.WriteAllText(markerPath, nowLocal.ToString("O", CultureInfo.InvariantCulture));

                    if (settings.Notifications?.BackupStatusEnabled == true)
                        AppNotifier.ShowInfo("Automatic backup created successfully.");

                    // NEW: enforce retention explicitly
                    backupSvc.EnforceRetention(settings.Backup.KeepLast);

                    if (!string.IsNullOrWhiteSpace(createdPath))
                        BackupCompleted?.Invoke(createdPath);
                }
                else
                {
                    if (settings.Notifications?.BackupStatusEnabled == true)
                        AppNotifier.ShowError("Automatic backup failed.");
                }

            }
            catch
            {
                // best-effort; never throw on scheduler
            }
            finally
            {
                _isRunning = false;
            }
        }

        private static bool IsScheduledDay(string? freq, DateTime localNow)
        {
            switch ((freq ?? "Daily").Trim())
            {
                case "Daily":
                    return true;
                case "Weekly":
                    // Simple policy: run Mondays
                    return localNow.DayOfWeek == DayOfWeek.Monday;
                case "Monthly":
                    // Run on the 1st
                    return localNow.Day == 1;
                default:
                    return true;
            }
        }

        private static (int hh, int mm) ParseTimeOrDefault(string? s, int defH, int defM)
        {
            if (!string.IsNullOrWhiteSpace(s) &&
                TimeSpan.TryParseExact(s.Trim(), "hh\\:mm", CultureInfo.InvariantCulture, out var ts))
            {
                return (ts.Hours, ts.Minutes);
            }
            return (defH, defM);
        }
    }
}
