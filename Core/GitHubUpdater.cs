using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingJournal.Core
{
    public class GitHubUpdater
    {
        private readonly string _repoOwner;
        private readonly string _repoName;

        public GitHubUpdater(string repoOwner, string repoName)
        {
            _repoOwner = repoOwner;
            _repoName = repoName;
        }

        public async Task CheckForUpdatesAsync(string currentVersion, Label statusLabel)
        {
            string apiUrl = $"https://api.github.com/repos/{_repoOwner}/{_repoName}/releases/latest";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("request");

            try
            {
                statusLabel.Invoke((Action)(() => statusLabel.Text = "Checking for updates..."));

                string json = await client.GetStringAsync(apiUrl);
                var release = JsonSerializer.Deserialize<GitHubRelease>(json);

                if (release != null)
                {
                    string latestVersion = release.tag_name.TrimStart('v');

                    if (latestVersion != currentVersion)
                    {
                        string downloadUrl = release.assets[0].browser_download_url;
                        string tempFile = Path.Combine(Path.GetTempPath(), "TradingJournalUpdate.zip");

                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Downloading update..."));

                        // Download the file
                        using (var stream = await client.GetStreamAsync(downloadUrl))
                        using (var fileStream = new FileStream(tempFile, FileMode.Create))
                        {
                            await stream.CopyToAsync(fileStream);
                        }

                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Applying update..."));

                        string appPath = AppDomain.CurrentDomain.BaseDirectory;

                        // Extract zip and overwrite files
                        ZipFile.ExtractToDirectory(tempFile, appPath, true);

                        // Restart app
                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Restarting..."));

                        string exePath = Application.ExecutablePath;

                        Process.Start(new ProcessStartInfo
                        {
                            FileName = exePath,
                            UseShellExecute = true
                        });

                        Application.Exit();
                    }
                    else
                    {
                        statusLabel.Invoke((Action)(() => statusLabel.Text = "No updates found!"));
                    }
                }
            }
            catch (Exception ex)
            {
                statusLabel.Invoke((Action)(() => statusLabel.Text = "Update check failed!"));
                Console.WriteLine("Update check failed: " + ex.Message);
            }
        }
    }

    public class GitHubRelease
    {
        public string tag_name { get; set; }
        public GitHubAsset[] assets { get; set; }
    }

    public class GitHubAsset
    {
        public string name { get; set; }
        public string browser_download_url { get; set; }
    }
}
