using FontAwesome.Sharp;
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
using TradingJournal.Core.Managers;

namespace TradingJournal.Pl.PlaceHolder.Journal
{
    public partial class FrmJournal : Form
    {
        public FrmJournal()
        {
            InitializeComponent();

            RoundedFormHelper.RoundPanel(pnlInformations, 30);
            RoundedFormHelper.RoundPanel(pnlData, 30);

            RoundedFormHelper.MakeButtonRounded(btnUploadScreenshot, 30);
            RoundedFormHelper.MakeButtonRounded(btnAddTrade, 30);

            RoundedFormHelper.RoundTextBox(txtEntryPrice, 20);
            RoundedFormHelper.RoundTextBox(txtStopLoss, 20);
            RoundedFormHelper.RoundTextBox(txtTakeProfit, 20);
            RoundedFormHelper.RoundTextBox(txtExitPrice, 20);
            RoundedFormHelper.RoundTextBox(txtMargin, 20);
            RoundedFormHelper.RoundTextBox(txtProfitLoss, 20);
        }

        private void FrmJournal_Load(object sender, EventArgs e)
        {
            btnUploadScreenshot.IconChar = IconChar.Upload;
            btnUploadScreenshot.IconColor = Color.Black;
            btnUploadScreenshot.IconSize = 25;
            btnUploadScreenshot.TextImageRelation = TextImageRelation.ImageBeforeText;

            btnAddTrade.IconChar = IconChar.Add;
            btnAddTrade.IconColor = Color.Green;
            btnAddTrade.IconSize = 25;
            btnAddTrade.TextImageRelation = TextImageRelation.ImageBeforeText;
        }

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddTrade_Click(object sender, EventArgs e)
        {
            var manager = new TradeManager();

            manager.AddTrade(
                cbSymbol.Text,
                cbTradeType.SelectedItem.ToString(),
                decimal.Parse(txtEntryPrice.Text),
                decimal.Parse(txtExitPrice.Text),
                decimal.Parse(txtStopLoss.Text),
                decimal.Parse(txtTakeProfit.Text),
                decimal.Parse(txtMargin.Text),
                decimal.Parse(txtProfitLoss.Text),
                txtScreenshotLink.Text          // if user entered a link
                //txtScreenshotFilePath.Text       // if user uploaded a file
            );

            MessageBox.Show("Trade added successfully!");
        }

        private void btnUploadScreenshot_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Load the image to the PictureBox
                    var image = Image.FromFile(openFileDialog.FileName);
                    pbScreenshot.Image = image;

                    // Save the file path temporarily (you could store bytes directly too)
                    txtScreenshotLink.Text = openFileDialog.FileName;
                }
            }
        }
    }
}
