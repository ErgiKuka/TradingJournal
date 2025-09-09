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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void FrmLoading_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.LoadingSpinner;

            await Task.Delay(2000);

            FrmHome frm = new FrmHome();
            frm.Show();
            this.Hide();
        }

    }
}
