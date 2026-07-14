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
            this.BackColor = ThemeManager.BackgroundColor;
            pictureBox2.BackColor = ThemeManager.BackgroundColor;


            lblStatus.ForeColor = ThemeManager.TextColor;

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

            try
            {
                string currentVersion = "1.9.11.2";
                string repoOwner = "ErgiKuka";
                string repoName = "TradingJournal"; 

                var updater = new GitHubUpdater(repoOwner, repoName);
                await updater.CheckForUpdatesAsync(currentVersion, lblStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine("A critical error occurred in the update process: " + ex.Message);
                lblStatus.Text = "Could not check for updates.";
            }

            if (!this.IsDisposed)
            {
                await Task.Delay(1500);

                FrmHome frm = new FrmHome();
                frm.Show();
                this.Hide();
            }
            ApplyTheme();
        }

    }
}
