using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingJournal.Pl.PlaceHolder.Journal
{
    public partial class FrmImageViewer : Form
    {
        public FrmImageViewer(Image imageToShow)
        {
            InitializeComponent();
            pbFullImage.Image = imageToShow;
        }

    }
}
