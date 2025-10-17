using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core;
using TradingJournal.Core.Logic.Helpers;
using TradingJournal.Pl.Skeleton;

namespace TradingJournal.Pl.Startup
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();

            RoundedFormHelper.ApplyRoundedCorners(this, 60);
            ThemeManager.ThemeChanged += (s, e) => ApplyTheme();
            ApplyTheme();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplyTheme()
        {
            // Background i formës
            this.BackColor = ThemeManager.BackgroundColor;
            pictureBox2.BackColor = ThemeManager.BackgroundColor;

            // Label
            lblStatus.ForeColor = ThemeManager.TextColor;

            // Nëse ke më shumë controls (p.sh. titull, logo, etj.)
            // kalon nëpër ta dhe ndrysho ForeColor/BackColor sipas rastit
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label lbl) lbl.ForeColor = ThemeManager.TextColor;
                if (ctrl is TextBox txt)
                {
                    txt.BackColor = ThemeManager.TextBoxColor;
                    txt.ForeColor = ThemeManager.TextColor;
                }
            }
        }

        private async void FrmLoading_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.InfiniteGif;

            // --- Start of Update Code ---
            try
            {
                string currentVersion = "1.8"; // This MUST match the version in your Inno Setup script
                string repoOwner = "ErgiKuka";
                string repoName = "TradingJournal"; // Your main repository

                var updater = new GitHubUpdater(repoOwner, repoName);
                await updater.CheckForUpdatesAsync(currentVersion, lblStatus); // Use your label here
            }
            catch (Exception ex)
            {
                // If the update check itself fails, we don't want the app to crash.
                // We just log it and continue.
                Console.WriteLine("A critical error occurred in the update process: " + ex.Message);
                lblStatus.Text = "Could not check for updates.";
            }
            // --- End of Update Code ---

            // If the app wasn't closed by the updater, continue starting
            if (!this.IsDisposed)
            {
                await Task.Delay(1500); // A short delay so the user can read the final status message

                FrmHome frm = new FrmHome();
                frm.Show();
                this.Hide();
            }
            ApplyTheme();
        }

    }
}
