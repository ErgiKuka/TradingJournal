using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingJournal.Pl.Skeleton
{
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();

            RoundedFormHelper.ApplyRoundedCorners(this, 80);
            RoundedFormHelper.EnableDrag(this);

            MakeButtonRounded(btnDashboard, 40);
            MakeButtonRounded(btnJournal, 40);
            MakeButtonRounded(btnStatistics, 40);
            MakeButtonRounded(btnSettings, 40);
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {

        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            FrmHome.ActiveForm.WindowState = FormWindowState.Minimized;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MakeButtonRounded(Button btn, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90); // top-left
            path.AddArc(new Rectangle(btn.Width - radius, 0, radius, radius), 270, 90); // top-right
            path.AddArc(new Rectangle(btn.Width - radius, btn.Height - radius, radius, radius), 0, 90); // bottom-right
            path.AddArc(new Rectangle(0, btn.Height - radius, radius, radius), 90, 90); // bottom-left
            path.CloseFigure();
            btn.Region = new Region(path);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
        private void MouseEnterEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(35, 52, 74);
        }
        private void MouseLeaveEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(27, 38, 59);
        }
        private void ActiveButtonEffect(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(30, 58, 95);
        }
        }
}
