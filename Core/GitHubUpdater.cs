using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TradingJournal.Core.Data;

namespace TradingJournal.Core
{
    public class GitHubUpdater
    {
        private readonly string _repoOwner;
        private readonly string _repoName;
        private readonly Action<string> _log;

        public GitHubUpdater(string repoOwner, string repoName, Action<string>? log = null)
        {
            _repoOwner = repoOwner;
            _repoName = repoName;
            _log = log ?? (_ => { });
        }

        /// <summary>
        /// Checks GitHub latest release and, if newer than currentVersion, backs up DB, downloads installer and runs it.
        /// </summary>
        public async Task CheckForUpdatesAsync(string currentVersion, Label statusLabel)
        {
            void UI(string s)
            {
                try { if (statusLabel?.IsHandleCreated == true) statusLabel.Invoke((Action)(() => statusLabel.Text = s)); }
                catch { /* ignore */ }
            }

            string apiUrl = $"https://api.github.com/repos/{_repoOwner}/{_repoName}/releases/latest";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("TradingJournal-UpdateChecker");

            try
            {
                UI("Checking for updates…");
                _log("Updater: hitting GitHub releases/latest.");

                // fetch JSON
                var json = await client.GetStringAsync(apiUrl);
                var release = JsonSerializer.Deserialize<GitHubRelease>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (release?.Assets == null || release.Assets.Length == 0)
                {
                    UI("Ready to start.");
                    _log("Updater: no assets on latest release.");
                    return;
                }

                var latestVersionStr = (release.TagName ?? "").Trim().TrimStart('v', 'V');
                if (!VersionTryParseSafe(latestVersionStr, out var latest))
                {
                    UI("Cannot parse latest version.");
                    _log($"Updater: cannot parse version from tag '{release.TagName}'.");
                    return;
                }

                if (!VersionTryParseSafe(currentVersion, out var current))
                {
                    // if current is malformed, assume update is needed
                    current = new Version(0, 0, 0, 0);
                }

                if (latest <= current)
                {
                    UI("You are up to date!");
                    _log($"Updater: current {current} >= latest {latest}, no update.");
                    return;
                }

                // pick an .exe Inno installer asset
                var setupAsset = release.Assets.FirstOrDefault(a =>
                    !string.IsNullOrWhiteSpace(a.Name) &&
                    a.Name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase));

                if (setupAsset == null)
                {
                    UI("Update found, but no installer.");
                    _log("Updater: latest release has no .exe asset.");
                    return;
                }

                // confirm with the user
                var result = MessageBox.Show(
                    $"A new version ({latest}) is available! You have {current}.\n\nDownload and install now?",
                    "Update Available",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result != DialogResult.Yes)
                {
                    UI("Update deferred. Starting app…");
                    _log("Updater: user chose not to update now.");
                    return;
                }

                // --------- PRE-UPDATE BACKUP (safe) ---------
                try
                {
                    using var db = new AppDbContext();
                    BackupOnce(db, out var backupPath);
                    if (!string.IsNullOrEmpty(backupPath))
                        _log("Updater: pre-update backup at " + backupPath);
                }
                catch (Exception ex)
                {
                    // Don't block the update if backup fails—just log
                    _log("Updater: pre-update backup failed: " + ex);
                }

                // --------- DOWNLOAD INSTALLER ---------
                string tempInstallerPath = Path.Combine(Path.GetTempPath(), setupAsset.Name!);
                UI("Downloading update…");
                _log($"Updater: downloading installer to {tempInstallerPath}");

                using (var stream = await client.GetStreamAsync(setupAsset.BrowserDownloadUrl!))
                using (var fileStream = new FileStream(tempInstallerPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await stream.CopyToAsync(fileStream);
                }

                // --------- RUN INSTALLER (elevated, silent, logs) ---------
                UI("Applying update… Please wait.");
                var setupLog = Path.Combine(Path.GetTempPath(), "TJ-setup.log");

                var psi = new ProcessStartInfo
                {
                    FileName = tempInstallerPath,
                    Arguments = $"/SILENT /CLOSEAPPLICATIONS /RESTARTAPPLICATIONS /LOG=\"{setupLog}\"",
                    UseShellExecute = true,
                    Verb = "runas" // elevation/UAC for Program Files
                };

                _log("Updater: starting installer with args: " + psi.Arguments);
                Process.Start(psi);

                // exit the app so installer can replace files
                Application.Exit();
            }
            catch (Exception ex)
            {
                UI("Update check failed. Starting app…");
                _log("Updater: exception: " + ex);
            }
        }

        // ----- helpers -----

        private static bool VersionTryParseSafe(string s, out Version v)
        {
            v = new Version(0, 0, 0, 0);
            if (string.IsNullOrWhiteSpace(s)) return false;

            // Normalize to at most 4 parts
            var parts = s.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return false;

            // fill missing parts with zeros
            var padded = parts.Take(4).ToList();
            while (padded.Count < 4) padded.Add("0");

            var normalized = string.Join(".", padded);
            return Version.TryParse(normalized, out v);
        }

        /// <summary>
        /// Minimal backup (no UI dependency). Tries file copy then SQLite online backup.
        /// </summary>
        private void BackupOnce(AppDbContext db, out string? backupPath)
        {
            backupPath = null;
            var livePath = GetLiveDbPath(db);
            if (string.IsNullOrWhiteSpace(livePath) || !File.Exists(livePath))
            {
                _log("Updater: live DB not found for backup.");
                return;
            }

            var backupRoot = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TradingJournal", "db-backups");
            Directory.CreateDirectory(backupRoot);

            var path = Path.Combine(backupRoot, $"tradingjournal-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sqlite.bak");

            if (TryFileCopyBackup(livePath, path) || TrySqliteOnlineBackup(livePath, path))
            {
                backupPath = path;
                RotateBackups(backupRoot, keep: 15);
            }
        }

        private static string GetLiveDbPath(AppDbContext db)
        {
            try
            {
                var c = db.Database.GetDbConnection();
                return (c is SqliteConnection sc) ? sc.DataSource : c.DataSource;
            }
            catch { return ""; }
        }

        private static bool TryFileCopyBackup(string src, string dest)
        {
            try { File.Copy(src, dest, overwrite: false); return true; }
            catch { return false; }
        }

        private static bool TrySqliteOnlineBackup(string srcPath, string destPath)
        {
            try
            {
                using var src = new SqliteConnection($"Data Source={srcPath}");
                using var dst = new SqliteConnection($"Data Source={destPath}");
                src.Open(); dst.Open();
                src.BackupDatabase(dst);
                return true;
            }
            catch { return false; }
        }

        private static void RotateBackups(string backupDir, int keep)
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
    }

    // --- DTOs for GitHub API (nullable to avoid warnings) ---

    internal sealed class GitHubRelease
    {
        [JsonPropertyName("tag_name")]
        public string? TagName { get; set; }

        [JsonPropertyName("assets")]
        public GitHubAsset[]? Assets { get; set; } = Array.Empty<GitHubAsset>();
    }

    internal sealed class GitHubAsset
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("browser_download_url")]
        public string? BrowserDownloadUrl { get; set; }
    }
}
