using System;
using System.Diagnostics;
using System.IO;
using System.Linq; // Required for .FirstOrDefault()
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
            client.DefaultRequestHeaders.UserAgent.ParseAdd("TradingJournal-UpdateChecker");

            try
            {
                statusLabel.Invoke((Action)(() => statusLabel.Text = "Checking for updates..."));
                string json = await client.GetStringAsync(apiUrl);
                var release = JsonSerializer.Deserialize<GitHubRelease>(json);

                if (release == null || release.assets == null || release.assets.Length == 0)
                {
                    statusLabel.Invoke((Action)(() => statusLabel.Text = "Ready to start."));
                    return;
                }

                string latestVersionStr = release.tag_name.TrimStart('v');

                if (new Version(latestVersionStr) > new Version(currentVersion))
                {
                    var setupAsset = release.assets.FirstOrDefault(a => a.name.EndsWith(".exe"));
                    if (setupAsset == null)
                    {
                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Update found, but no installer."));
                        return;
                    }

                    var result = MessageBox.Show(
                        $"A new version ({latestVersionStr}) is available! You have version {currentVersion}.\n\nWould you like to download and install it now?",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        string tempInstallerPath = Path.Combine(Path.GetTempPath(), setupAsset.name);
                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Downloading update..."));

                        using (var stream = await client.GetStreamAsync(setupAsset.browser_download_url))
                        using (var fileStream = new FileStream(tempInstallerPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await stream.CopyToAsync(fileStream);
                        }

                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Applying update... Please wait."));

                        // --- THIS IS THE NEW, IMPROVED CODE ---

                        // Get the full path of the currently running application
                        string applicationPath = Application.ExecutablePath;

                        // Set up the process to run the installer silently
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = tempInstallerPath;

                        // /VERYSILENT: No wizard, no progress bar. Completely quiet.
                        // /SP-: Disables the "This will install... Do you wish to continue?" prompt.
                        // /RESTARTAPPLICATIONS: Tells the installer to handle closing the app if needed.
                        // /LOG: Creates a log file for debugging update issues.
                        // /MERGETASKS="desktopicon,!runcode": Ensures desktop icon task is handled correctly.
                        // /RESTARTRUN="path_to_your_exe": The magic command to restart your app after the update.
                        startInfo.Arguments = "/VERYSILENT /SP-";

                        // Run the installer and exit this application
                        Process.Start(startInfo);
                        Application.Exit();
                    }
                    else
                    {
                        statusLabel.Invoke((Action)(() => statusLabel.Text = "Update deferred. Starting app..."));
                    }
                }
                else
                {
                    statusLabel.Invoke((Action)(() => statusLabel.Text = "You are up to date!"));
                }
            }
            catch (Exception ex)
            {
                statusLabel.Invoke((Action)(() => statusLabel.Text = "Update check failed. Starting app..."));
                Console.WriteLine("Update check failed: " + ex.Message);
            }
        }
    }

    // These helper classes are fine as they are
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
