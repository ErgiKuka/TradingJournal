using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            client.DefaultRequestHeaders.UserAgent.ParseAdd("request"); // Required by GitHub API

            try
            {
                // Update label: checking updates
                statusLabel.Invoke((Action)(() => statusLabel.Text = "Checking for updates..."));

                string json = await client.GetStringAsync(apiUrl);
                var release = JsonSerializer.Deserialize<GitHubRelease>(json);

                if (release != null)
                {
                    string latestVersion = release.tag_name.TrimStart('v');

                    if (latestVersion != currentVersion)
                    {
                        string downloadUrl = release.assets[0].browser_download_url;

                        // Update label: updating
                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Updating..."));

                        // Ask user to download
                        DialogResult result = MessageBox.Show(
                            $"New version {latestVersion} available! Download now?",
                            "Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = downloadUrl,
                                UseShellExecute = true
                            });
                        }
                    }
                    else
                    {
                        // Update label: no updates
                        statusLabel.Invoke((Action)(() => statusLabel.Text = "No updates found!"));
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: show error
                statusLabel.Invoke((Action)(() => statusLabel.Text = "Update check failed"));
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
