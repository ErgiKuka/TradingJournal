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
using TradingJournal.Pl.Skeleton;

namespace TradingJournal.Pl.Startup
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();

            RoundedFormHelper.ApplyRoundedCorners(this, 60);
            RoundedFormHelper.EnableDrag(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void FrmLoading_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.LoadingSpinner;

            await Task.Delay(500);

            var updater = new GitHubUpdater("ErgiKuka", "TradingJournal");
            await updater.CheckForUpdatesAsync(Application.ProductVersion, label1);

            await Task.Delay(1300);

            FrmHome frm = new FrmHome();
            frm.Show();
            this.Hide();
        }

    }
}
