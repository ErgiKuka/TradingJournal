using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TradingJournal.Core.Data;

namespace TradingJournal.Core.Logic.Services
{
    /// <summary>
    /// SQLite backup/restore utility for TradingJournal.
    /// - Daily backup (first app launch per day)
    /// - Manual "Backup Now"
    /// - Restore latest or chosen backup (with automatic undo snapshot)
    /// - Undo last restore (valid for 24h or until the next daily backup)
    /// - WAL-aware (online backup preferred; WAL checkpoint + file copy fallback)
    /// - Retains the newest N backups
    /// </summary>
    public sealed class DatabaseBackupService
    {
        private readonly Action<string> _log;

        public DatabaseBackupService(Action<string> log)
        {
            _log = log ?? (_ => { });
        }

        // ---- Paths / Patterns ----

        public string BackupRoot =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                         "TradingJournal", "db-backups");

        private string MarkerRoot => Path.Combine(BackupRoot, "markers");

        // JSON isn’t necessary; a simple pipe-delimited line is enough:
        // <undoPath>|<createdUtc ISO 8601>|<sourceLabel>
        private string UndoMarkerPath => Path.Combine(BackupRoot, "last-undo.txt");

        public string BackupPattern => "tradingjournal-*.sqlite.bak";

        public sealed class UndoInfo
        {
            public string UndoPath { get; init; } = "";
            public DateTime CreatedUtc { get; init; }
            public string SourceLabel { get; init; } = "";
        }

        // ---- Entry points ----

        /// <summary>
        /// Call at app start. Creates at most one backup per UTC calendar day.
        /// Keeps the newest <paramref name="keep"/> backups (default 15).
        /// Also invalidates any pending UNDO (your rule: "a day OR next backup").
        /// </summary>
        public void EnsureDailyBackup(AppDbContext db, int keep = 15)
        {
            var live = GetLiveDbPath(db);
            if (string.IsNullOrWhiteSpace(live) || !File.Exists(live)) return;

            Directory.CreateDirectory(BackupRoot);

            if (!ShouldBackupToday()) return;

            if (TryBackup(live, out var backupPath))
                _log($"DB backup created: {backupPath}");
            else
                _log("DB backup failed (both methods).");

            RotateBackups(keep);

            // Invalidate undo on the day’s backup (your “backup OR 24h” rule)
            ClearUndo();
        }

        /// <summary>
        /// Manual backup from UI. Returns true and outputs created file path on success.
        /// </summary>
        public bool BackupNow(AppDbContext db, out string? createdPath, int keep = 15)
        {
            createdPath = null;
            var live = GetLiveDbPath(db);
            if (string.IsNullOrWhiteSpace(live) || !File.Exists(live)) return false;

            Directory.CreateDirectory(BackupRoot);

            var path = Path.Combine(BackupRoot, $"tradingjournal-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sqlite.bak");

            if (TrySqliteOnlineBackup(live, path) || TryFileCopyBackup(live, path))
            {
                _log($"DB backup created: {path}");
                createdPath = path;
                RotateBackups(keep);
                return true;
            }

            _log("BackupNow failed.");
            return false;
        }

        /// <summary>
        /// Restore the newest backup with undo snapshot.
        /// </summary>
        public bool RestoreLatest(AppDbContext db)
        {
            var latest = GetBackups().FirstOrDefault();
            return latest != null && RestoreFromFileWithUndo(db, latest.FullName, $"Latest: {latest.Name}");
        }

        /// <summary>
        /// Restore specific backup file with automatic undo snapshot of the current DB.
        /// Deletes any stale -wal/-shm after restore to avoid replay.
        /// </summary>
        public bool RestoreFromFileWithUndo(AppDbContext db, string backupPath, string? sourceLabel = null)
        {
            var live = GetLiveDbPath(db);
            if (string.IsNullOrWhiteSpace(live)) return false;

            try { db.Dispose(); } catch { }
            try { SqliteConnection.ClearAllPools(); } catch { }

            try
            {
                Directory.CreateDirectory(BackupRoot);

                // 1) Make undo snapshot of current DB (what we’re about to overwrite)
                string? undoPath = null;
                if (File.Exists(live))
                {
                    undoPath = Path.Combine(
                        BackupRoot,
                        $"undo-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sqlite");

                    File.Copy(live, undoPath, overwrite: false);
                    WriteUndoMarker(new UndoInfo
                    {
                        UndoPath = undoPath,
                        CreatedUtc = DateTime.UtcNow,
                        SourceLabel = sourceLabel ?? Path.GetFileName(backupPath)
                    });
                }

                // 2) Copy backup over live DB
                File.Copy(backupPath, live, overwrite: true);

                // 3) Remove WAL/SHM so they cannot replay over the restored DB
                TryDelete(live + "-wal");
                TryDelete(live + "-shm");

                _log($"Restored DB from backup: {backupPath}. Undo saved: {undoPath ?? "(none)"}");
                return true;
            }
            catch (Exception ex)
            {
                _log("Restore failed: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Undo the most recent restore, if an undo snapshot is available.
        /// </summary>
        public bool UndoLastRestore(AppDbContext db)
        {
            var live = GetLiveDbPath(db);
            if (string.IsNullOrWhiteSpace(live)) return false;

            try { db.Dispose(); } catch { }
            try { SqliteConnection.ClearAllPools(); } catch { }

            try
            {
                if (!TryGetUndoInfo(out var info)) return false;

                // Optional: keep a copy of the current DB (which we are undoing)
                var badCopy = Path.Combine(
                    Path.GetDirectoryName(live)!,
                    Path.GetFileNameWithoutExtension(live) + $".bad-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sqlite");
                if (File.Exists(live))
                    File.Copy(live, badCopy, overwrite: false);

                File.Copy(info.UndoPath, live, overwrite: true);
                TryDelete(live + "-wal");
                TryDelete(live + "-shm");

                // Clear the marker after successful undo
                ClearUndo();

                _log($"Undo restore complete. Restored from: {info.UndoPath}");
                return true;
            }
            catch (Exception ex)
            {
                _log("Undo restore failed: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Get current undo metadata if present.
        /// </summary>
        public bool TryGetUndoInfo(out UndoInfo info)
        {
            info = new UndoInfo();
            try
            {
                if (!File.Exists(UndoMarkerPath)) return false;
                var line = File.ReadAllText(UndoMarkerPath).Trim();
                // format: <path>|<createdUtc>|<label>
                var parts = line.Split('|');
                if (parts.Length < 3) return false;

                var path = parts[0];
                var created = DateTime.Parse(parts[1], null, DateTimeStyles.RoundtripKind);
                var label = parts[2];

                if (!File.Exists(path)) return false;

                info = new UndoInfo
                {
                    UndoPath = path,
                    CreatedUtc = created,
                    SourceLabel = label
                };
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if there’s an undo and whether it’s within TTL.
        /// </summary>
        public bool IsUndoValid(TimeSpan ttl, out UndoInfo info)
        {
            if (!TryGetUndoInfo(out info)) return false;
            var age = DateTime.UtcNow - info.CreatedUtc;
            return age <= ttl;
        }

        /// <summary>
        /// Removes the undo marker (and optionally the undo file if you want).
        /// </summary>
        public void ClearUndo(bool deleteUndoFile = false)
        {
            try
            {
                if (deleteUndoFile && TryGetUndoInfo(out var info))
                {
                    TryDelete(info.UndoPath);
                }
            }
            catch { /* ignore */ }

            TryDelete(UndoMarkerPath);
        }

        /// <summary>
        /// Enumerate backups newest-first.
        /// </summary>
        public IEnumerable<FileInfo> GetBackups() =>
            Directory.Exists(BackupRoot)
                ? new DirectoryInfo(BackupRoot)
                    .GetFiles(BackupPattern)
                    .OrderByDescending(f => f.CreationTimeUtc)
                : Enumerable.Empty<FileInfo>();

        public void OpenBackupFolder()
        {
            Directory.CreateDirectory(BackupRoot);
            var psi = new ProcessStartInfo
            {
                FileName = "explorer",
                Arguments = BackupRoot,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        // ---- Helpers ----

        public static string GetLiveDbPath(AppDbContext db)
        {
            try
            {
                var c = db.Database.GetDbConnection();
                return (c is SqliteConnection sc) ? sc.DataSource : c.DataSource;
            }
            catch { return ""; }
        }

        private bool ShouldBackupToday()
        {
            try
            {
                Directory.CreateDirectory(MarkerRoot);
                var marker = Path.Combine(MarkerRoot, $"db-backup-{DateTime.UtcNow:yyyy-MM-dd}.flag");
                if (File.Exists(marker)) return false;
                File.WriteAllText(marker, DateTime.UtcNow.ToString("O"));
                return true;
            }
            catch { return false; }
        }

        private void RotateBackups(int keep)
        {
            try
            {
                var files = GetBackups().ToList();
                foreach (var f in files.Skip(keep))
                    f.Delete();
            }
            catch { /* best-effort */ }
        }

        /// <summary>
        /// Preferred: Online backup via SQLite API. Fallback: WAL checkpoint + file copy (including -wal/-shm).
        /// </summary>
        private bool TryBackup(string livePath, out string backupPath)
        {
            backupPath = Path.Combine(BackupRoot, $"tradingjournal-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sqlite.bak");

            if (TrySqliteOnlineBackup(livePath, backupPath)) return true;

            TryWalCheckpoint(livePath);
            if (TryFileCopyBackup(livePath, backupPath)) return true;

            return false;
        }

        private static void TryWalCheckpoint(string livePath)
        {
            try
            {
                using var c = new SqliteConnection($"Data Source={livePath}");
                c.Open();
                using var cmd = c.CreateCommand();
                cmd.CommandText = "PRAGMA wal_checkpoint(TRUNCATE);";
                cmd.ExecuteNonQuery();
            }
            catch { /* best-effort */ }
        }

        private static bool TryFileCopyBackup(string src, string dest)
        {
            try
            {
                File.Copy(src, dest, overwrite: false);

                var walSrc = src + "-wal";
                var shmSrc = src + "-shm";
                var walDest = dest + "-wal";
                var shmDest = dest + "-shm";

                if (File.Exists(walSrc))
                    File.Copy(walSrc, walDest, overwrite: false);
                if (File.Exists(shmSrc))
                    File.Copy(shmSrc, shmDest, overwrite: false);

                return true;
            }
            catch { return false; }
        }

        private static bool TrySqliteOnlineBackup(string srcPath, string destPath)
        {
            try
            {
                using var src = new SqliteConnection($"Data Source={srcPath}");
                using var dst = new SqliteConnection($"Data Source={destPath}");
                src.Open();
                dst.Open();
                src.BackupDatabase(dst);
                return true;
            }
            catch { return false; }
        }

        private static void TryDelete(string path)
        {
            try { if (File.Exists(path)) File.Delete(path); } catch { }
        }

        private void WriteUndoMarker(UndoInfo info)
        {
            try
            {
                var line = $"{info.UndoPath}|{info.CreatedUtc.ToString("O")}|{info.SourceLabel}";
                File.WriteAllText(UndoMarkerPath, line);
            }
            catch { /* ignore */ }
        }
    }
}
