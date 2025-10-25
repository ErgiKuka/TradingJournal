using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Pl.Startup;

namespace TradingJournal
{
    internal static class Program
    {
        //Per me bo publish filein edhe athere me e perdor tek inno setupi

//        dotnet clean.\TradingJournal.csproj
//        dotnet publish.\TradingJournal.csproj -c Release -r win-x64 `
//-p:PublishSingleFile= true `
//-p:SelfContained= true `
//-p:IncludeNativeLibrariesForSelfExtract= true




                                [STAThread]
        static void Main()
        {
            // ------- simple file logger --------
            var logDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TradingJournal", "logs");
            Directory.CreateDirectory(logDir);
            var logFile = Path.Combine(logDir, $"app-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");

            void Log(string msg)
            {
                try { File.AppendAllText(logFile, $"[{DateTime.Now:HH:mm:ss}] {msg}\r\n"); } catch { /* ignore */ }
            }

            // ------- global exception handlers -------
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                Log("ThreadException:\r\n" + e.Exception);
                MessageBox.Show("The app hit an unexpected error.\n\nLog:\n" + logFile,
                                "TradingJournal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Log("UnhandledException:\r\n" + e.ExceptionObject?.ToString());
            };

            static void RotateBackups(string backupDir, int keep = 10)
            {
                try
                {
                    var files = new DirectoryInfo(backupDir)
                        .GetFiles("tradingjournal-*.sqlite.bak")
                        .OrderByDescending(f => f.CreationTimeUtc)
                        .ToList();

                    foreach (var f in files.Skip(keep))
                        f.Delete();
                }
                catch { /* best-effort */ }
            }

            static bool ShouldBackupToday(string markerDir)
            {
                Directory.CreateDirectory(markerDir);
                var marker = Path.Combine(markerDir, $"db-backup-{DateTime.UtcNow:yyyy-MM-dd}.flag");
                if (File.Exists(marker)) return false;
                File.WriteAllText(marker, DateTime.UtcNow.ToString("O"));
                return true;
            }

            try
            {
                Log("App starting…");

                // one-time “settings upgraded” flag (no Properties.Settings used)
                TrySafeSettingsUpgrade(Log);

                // theme + winforms bootstrap
                var settings = SettingsManager.Load();
                ThemeManager.SetTheme(settings.Theme);
                ApplicationConfiguration.Initialize();

                // ---------- DATABASE: backup, seed history if needed, migrate ----------
                using (var db = new AppDbContext())
                {
                    // locate DB path for backup
                    string dbPath = "";
                    try
                    {
                        var connObj = db.Database.GetDbConnection();
                        dbPath = (connObj is SqliteConnection sc) ? sc.DataSource : connObj.DataSource;
                    }
                    catch { /* best-effort */ }

                    // backup the sqlite file (best-effort)
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(dbPath))
                        {
                            string fullPath = Path.IsPathRooted(dbPath) ? dbPath : Path.GetFullPath(dbPath);
                            if (File.Exists(fullPath))
                            {
                                var backupDir = Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                    "TradingJournal", "db-backups");
                                Directory.CreateDirectory(backupDir);
                                var backupPath = Path.Combine(backupDir,
                                            $"tradingjournal-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sqlite.bak");
                                RotateBackups(backupDir, keep: 15);
                                Log("DB backup created: " + backupPath);
                            }
                        }
                    }
                    catch (Exception ex) { Log("DB backup failed (continuing): " + ex); }

                    // seed __EFMigrationsHistory for legacy DBs that were created via EnsureCreated()
                    try
                    {
                        Log("Checking migration state…");
                        var conn = db.Database.GetDbConnection();
                        conn.Open();

                        bool TableExists(string name)
                        {
                            using var cmd = conn.CreateCommand();
                            cmd.CommandText = "SELECT 1 FROM sqlite_master WHERE type='table' AND name=$n";
                            var p = cmd.CreateParameter(); p.ParameterName = "$n"; p.Value = name; cmd.Parameters.Add(p);
                            var r = cmd.ExecuteScalar();
                            return r != null && r != DBNull.Value;
                        }

                        int HistoryCount()
                        {
                            using var cmd = conn.CreateCommand();
                            cmd.CommandText = "SELECT COUNT(*) FROM __EFMigrationsHistory";
                            var r = cmd.ExecuteScalar();
                            return (r is long l) ? (int)l : Convert.ToInt32(r);
                        }

                        // tables that exist in your “legacy” DB made by EnsureCreated
                        var hasTradesTable = TableExists("Trades");

                        // tables introduced by your later migration(s)
                        var hasRecoveryCases = TableExists("RecoveryCases");
                        var hasRecoveryAllocations = TableExists("RecoveryAllocations");

                        var hasHistoryTable = TableExists("__EFMigrationsHistory");
                        var historyRows = hasHistoryTable ? HistoryCount() : 0;

                        // Helper: ensure history table exists
                        void EnsureHistoryTable()
                        {
                            if (!hasHistoryTable)
                            {
                                using var create = conn.CreateCommand();
                                create.CommandText =
                                    "CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (" +
                                    "  MigrationId TEXT NOT NULL CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY," +
                                    "  ProductVersion TEXT NOT NULL" +
                                    ")";
                                create.ExecuteNonQuery();
                                hasHistoryTable = true;
                            }
                        }

                        // Helper: seed a single migration id as applied
                        void SeedSingleMigration(string migrationId)
                        {
                            var productVersion = typeof(Microsoft.EntityFrameworkCore.Migrations.IMigrator)
                                                     .Assembly.GetName().Version?.ToString() ?? "8.0.0";
                            using var ins = conn.CreateCommand();
                            ins.CommandText =
                                "INSERT OR IGNORE INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ($id, $ver)";
                            var p1 = ins.CreateParameter(); p1.ParameterName = "$id"; p1.Value = migrationId; ins.Parameters.Add(p1);
                            var p2 = ins.CreateParameter(); p2.ParameterName = "$ver"; p2.Value = productVersion; ins.Parameters.Add(p2);
                            ins.ExecuteNonQuery();
                            Log("Seeded baseline migration as applied: " + migrationId);
                        }

                        // Figure out the first migration (by id order)
                        var allMigrations = db.Database.GetMigrations().ToList();
                        var firstMigration = allMigrations.OrderBy(x => x).FirstOrDefault();

                        // --- Baseline old DBs (tables exist, history empty) ---
                        if (hasTradesTable && historyRows == 0 && firstMigration != null)
                        {
                            Log("Legacy DB detected (tables exist, empty __EFMigrationsHistory). Seeding FIRST migration only…");
                            EnsureHistoryTable();
                            SeedSingleMigration(firstMigration);
                        }

                        // --- Repair case: history has rows but Recovery tables are missing (happens if we seeded all) ---
                        if (historyRows > 0 && (!hasRecoveryCases || !hasRecoveryAllocations) && firstMigration != null)
                        {
                            Log("Repairing migrations history (history present but recovery tables missing) …");
                            using (var del = conn.CreateCommand())
                            {
                                del.CommandText = "DELETE FROM __EFMigrationsHistory";
                                del.ExecuteNonQuery();
                            }
                            SeedSingleMigration(firstMigration);
                        }

                        Log("Applying EF Core migrations…");
                        db.Database.Migrate();
                        Log("Migrations applied successfully.");
                    }
                    catch (Exception ex)
                    {
                        Log("EF migration failed: " + ex);
                        MessageBox.Show(
                            "Database migration failed. Your data was backed up.\n\nLog:\n" + logFile,
                            "TradingJournal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // safer to bail
                    }
                }
                // ----------------------------------------------------------------------

                // run app
                Application.Run(new FrmLoading());

                Log("App exited normally.");
            }
            catch (Exception ex)
            {
                Log("Fatal in Main:\r\n" + ex);
                MessageBox.Show("The app hit a fatal error.\n\nLog:\n" + logFile,
                                "TradingJournal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // one-time per version “upgrade” marker using a simple flag file
        static void TrySafeSettingsUpgrade(Action<string> Log)
        {
            try
            {
                var appVersion = Application.ProductVersion; // fine to keep hard-coded elsewhere if you prefer
                var dir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "TradingJournal", "settings");
                Directory.CreateDirectory(dir);
                var flag = Path.Combine(dir, $"upgraded-{appVersion}.flag");
                if (File.Exists(flag))
                {
                    Log("Settings already upgraded for this version.");
                    return;
                }

                // If you ever need to migrate your own settings, do it here.
                // SettingsManager.UpgradeIfNeeded(); // (no-op for now)

                File.WriteAllText(flag, DateTime.Now.ToString("O"));
                Log("Settings upgraded via flag file.");
            }
            catch (Exception ex)
            {
                Log("Settings upgrade step failed: " + ex);
            }
        }
    }
}
