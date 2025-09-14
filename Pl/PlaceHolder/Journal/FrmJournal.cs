using FontAwesome.Sharp;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingJournal.Core.Data;
using TradingJournal.Core.Data.Entities;
using TradingJournal.Core.Logic;
using TradingJournal.Core.Logic.Manager;
using TradingJournal.Core.Logic.Services;
using TradingJournal.Core.Managers;
using Color = System.Drawing.Color;
using FontStyle = System.Drawing.FontStyle;
using Image = System.Drawing.Image;

namespace TradingJournal.Pl.PlaceHolder.Journal
{
    public partial class FrmJournal : Form, IResponsiveChildForm
    {

        private readonly HttpClient _httpClient = new HttpClient();
        private int? _tradeIdToUpdate = null;
        private readonly ResponsiveLayoutManager _layoutManager;

        public FrmJournal()
        {
            InitializeComponent();

            _layoutManager = new ResponsiveLayoutManager(this);
            InitializeResponsiveLayouts();

            LoadSymbols();

            RoundedFormHelper.RoundPanel(pnlInformations, 30);
            RoundedFormHelper.RoundPanel(pnlData, 30);
            RoundedFormHelper.RoundPanel(pnlData_Max, 30);
            RoundedFormHelper.RoundPanel(pnlInformations_Max, 30);

            RoundedFormHelper.MakeButtonRounded(btnUploadScreenshot, 30);
            RoundedFormHelper.MakeButtonRounded(btnAddTrade, 30);
            RoundedFormHelper.MakeButtonRounded(btnCancelUpdate, 30);
            RoundedFormHelper.MakeButtonRounded(btnUpdateTrade, 30);
            RoundedFormHelper.MakeButtonRounded(btnClearData, 30);

            RoundedFormHelper.RoundTextBox(txtEntryPrice, 20);
            RoundedFormHelper.RoundTextBox(txtStopLoss, 20);
            RoundedFormHelper.RoundTextBox(txtTakeProfit, 20);
            RoundedFormHelper.RoundTextBox(txtExitPrice, 20);
            RoundedFormHelper.RoundTextBox(txtMargin, 20);
            RoundedFormHelper.RoundTextBox(txtProfitLoss, 20);
        }

        private void InitializeResponsiveLayouts()
        {
            // --- Register Controls in the TOP panel (pnlInformations) ---
            // The manager will automatically store their current (normal) state.
            // We just need to tell it the maximized parent and layout.
            _layoutManager.RegisterControl(lblSymbol, pnlInformations, pnlInformations_Max, new Point(79, 37), new Size(85, 26));
            _layoutManager.RegisterControl(cbSymbol, pnlInformations, pnlInformations_Max, new Point(79, 64), new Size(203, 39));
            _layoutManager.RegisterControl(lblTradeType, pnlInformations, pnlInformations_Max, new Point(456, 37), new Size(117, 26));
            _layoutManager.RegisterControl(cbTradeType, pnlInformations, pnlInformations_Max, new Point(456, 64), new Size(202, 39));
            _layoutManager.RegisterControl(lblEntryPrice, pnlInformations, pnlInformations_Max, new Point(79, 132), new Size(116, 26));
            _layoutManager.RegisterControl(txtEntryPrice, pnlInformations, pnlInformations_Max, new Point(79, 165), new Size(203, 32));
            _layoutManager.RegisterControl(lblExitPrice, pnlInformations, pnlInformations_Max, new Point(456, 132), new Size(101, 26));
            _layoutManager.RegisterControl(txtExitPrice, pnlInformations, pnlInformations_Max, new Point(456, 165), new Size(203, 32));
            _layoutManager.RegisterControl(lblStopLoss, pnlInformations, pnlInformations_Max, new Point(79, 236), new Size(104, 26));
            _layoutManager.RegisterControl(txtStopLoss, pnlInformations, pnlInformations_Max, new Point(79, 272), new Size(203, 32));
            _layoutManager.RegisterControl(lblTakeProfit, pnlInformations, pnlInformations_Max, new Point(456, 236), new Size(115, 26));
            _layoutManager.RegisterControl(txtTakeProfit, pnlInformations, pnlInformations_Max, new Point(456, 272), new Size(203, 32));
            _layoutManager.RegisterControl(lblProfitLoss, pnlInformations, pnlInformations_Max, new Point(877, 32), new Size(126, 26));
            _layoutManager.RegisterControl(txtProfitLoss, pnlInformations, pnlInformations_Max, new Point(877, 65), new Size(203, 32));
            _layoutManager.RegisterControl(lblMargin, pnlInformations, pnlInformations_Max, new Point(877, 120), new Size(78, 26));
            _layoutManager.RegisterControl(txtMargin, pnlInformations, pnlInformations_Max, new Point(876, 160), new Size(203, 32));
            _layoutManager.RegisterControl(btnUploadScreenshot, pnlInformations, pnlInformations_Max, new Point(1266, 50), new Size(240, 49));
            _layoutManager.RegisterControl(txtScreenshotLink, pnlInformations, pnlInformations_Max, new Point(1282, 113), new Size(203, 34));
            _layoutManager.RegisterControl(pbScreenshot, pnlInformations, pnlInformations_Max, new Point(1241, 166), new Size(294, 182));
            _layoutManager.RegisterControl(btnAddTrade, pnlInformations, pnlInformations_Max, new Point(863, 236), new Size(219, 49));
            _layoutManager.RegisterControl(btnClearData, pnlInformations, pnlInformations_Max, new Point(876, 300), new Size(190, 37));
            _layoutManager.RegisterControl(btnUpdateTrade, pnlInformations, pnlInformations_Max, new Point(835, 270), new Size(131, 49));
            _layoutManager.RegisterControl(btnCancelUpdate, pnlInformations, pnlInformations_Max, new Point(996, 270), new Size(131, 49));

            // --- Register Controls in the BOTTOM panel (pnlData) ---
            _layoutManager.RegisterControl(dgvData, pnlData, pnlData_Max, new Point(20, 58), new Size(1542, 453));
            _layoutManager.RegisterControl(chbAllTrades, pnlData, pnlData_Max, new Point(1204, 19), new Size(177, 30));
            _layoutManager.RegisterControl(dtpFilterDate, pnlData, pnlData_Max, new Point(1387, 13), new Size(175, 39));

            _layoutManager.RegisterStateAction(FormWindowStateExtended.Normal, ApplyNormalGridStyle);
            _layoutManager.RegisterStateAction(FormWindowStateExtended.Maximized, ApplyMaximizedGridStyle);
        }

        private void ApplyNormalGridStyle()
        {
            // Header Style
            dgvData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvData.ColumnHeadersHeight = 30; // Default height

            // Row and Cell Style
            dgvData.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvData.RowTemplate.Height = 25; // Default row height

            // Button Column Font
            dgvData.Columns["UpdateColumn"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvData.Columns["DeleteColumn"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
        }

        private void ApplyMaximizedGridStyle()
        {
            // Header Style - Larger Font and Height
            dgvData.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvData.ColumnHeadersHeight = 40;

            // Row and Cell Style - Larger Font and Height
            dgvData.DefaultCellStyle.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            dgvData.RowTemplate.Height = 35;

            // Button Column Font - Larger Font
            dgvData.Columns["UpdateColumn"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvData.Columns["DeleteColumn"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        /// <summary>
        /// Implements the interface by delegating the call to the layout manager.
        /// </summary>
        public void SetWindowState(FormWindowStateExtended newState)
        {
            _layoutManager.SetWindowState(newState);
        }

        private void FrmJournal_Load(object sender, EventArgs e)
        {
            btnUploadScreenshot.IconChar = IconChar.Upload;
            btnUploadScreenshot.IconColor = System.Drawing.Color.Black;
            btnUploadScreenshot.IconSize = 25;
            btnUploadScreenshot.TextImageRelation = TextImageRelation.ImageBeforeText;

            btnAddTrade.IconChar = IconChar.Add;
            btnAddTrade.IconColor = Color.Green;
            btnAddTrade.IconSize = 25;
            btnAddTrade.TextImageRelation = TextImageRelation.ImageBeforeText;


            var updateButtonColumn = new DataGridViewButtonColumn();
            updateButtonColumn.Name = "UpdateColumn";
            updateButtonColumn.HeaderText = "Update";
            updateButtonColumn.Text = "Update";
            updateButtonColumn.UseColumnTextForButtonValue = true; // This makes every button show "Update"
            dgvData.Columns.Add(updateButtonColumn);

            var deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "DeleteColumn";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true; // This makes every button show "Delete"
            dgvData.Columns.Add(deleteButtonColumn);

            chbAllTrades.Checked = false;
            dtpFilterDate.Enabled = true;

            ApplyBaseGridStyling();

            ApplyNormalGridStyle();

            dtpFilterDate.Value = DateTime.Now;
            if (chbAllTrades.Checked)
                LoadTrades(null); // Load all trades
            else
                LoadTrades(dtpFilterDate.Value);
        }

        private void btnAddTrade_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbSymbol.Text) || cbTradeType.SelectedItem == null)
            {
                MessageBox.Show("Symbol and Trade Type are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Use decimal.TryParse for all numeric fields
            decimal entryPrice, exitPrice = 0, stopLoss = 0, takeProfit = 0, margin = 0, profitLoss = 0;

            if (!decimal.TryParse(txtEntryPrice.Text, out entryPrice))
            {
                MessageBox.Show("Please enter a valid Entry Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal.TryParse(txtExitPrice.Text, out exitPrice);
            decimal.TryParse(txtStopLoss.Text, out stopLoss);
            decimal.TryParse(txtTakeProfit.Text, out takeProfit);
            decimal.TryParse(txtMargin.Text, out margin);
            decimal.TryParse(txtProfitLoss.Text, out profitLoss);


            var manager = new TradeManager();

            string screenshotPath = txtScreenshotLink.Text;

            try
            {
                manager.AddTrade(
                    cbSymbol.Text,
                    cbTradeType.SelectedItem.ToString(),
                    entryPrice,
                    exitPrice,
                    stopLoss,
                    takeProfit,
                    margin,
                    profitLoss,
                    screenshotFilePath: screenshotPath // Pass the file path
                );

                MessageBox.Show("Trade added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (chbAllTrades.Checked)
                    LoadTrades(null); // Load all trades
                else
                    LoadTrades(dtpFilterDate.Value);
                ClearForm();
            }
            catch (Exception ex)
            {
                // Catch potential errors from the manager (e.g., file not found)
                MessageBox.Show($"An error occurred while adding the trade: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            cbSymbol.Text = "";
            cbTradeType.SelectedIndex = -1;
            txtEntryPrice.Text = "   ";
            txtExitPrice.Text = "   ";
            txtStopLoss.Text = "   ";
            txtTakeProfit.Text = "   ";
            txtMargin.Text = "   ";
            txtProfitLoss.Text = "   ";
            txtScreenshotLink.Clear();
            //txtScreenshotLink.Text = "   ";
            pbScreenshot.Image = null;
            txtScreenshotLink.PlaceholderText = "Upload link . . .";
        }

        private void LoadTrades(DateTime? filterDate = null)
        {
            using (var db = new AppDbContext())
            {
                // Start with a queryable list of all trades
                var query = db.Trades.AsQueryable();

                // If a date is provided, filter the query
                if (filterDate.HasValue)
                {
                    // This filters for trades that are on the same day, month, and year
                    query = query.Where(t => t.Date.Date == filterDate.Value.Date);
                }

                var trades = query.OrderByDescending(t => t.Date).ToList(); // Show newest first

                //lblTotalPnl.Text = $"Total PnL: {totalPnl:C}"; // Format as currency
                //lblTotalPnl.ForeColor = totalPnl >= 0 ? Color.Green : Color.Red;

                dgvData.DataSource = trades.Select(t => new
                {
                    t.Id,
                    t.Symbol,
                    t.TradeType,
                    t.EntryPrice,
                    t.ExitPrice,
                    t.StopLoss,
                    t.TakeProfit,
                    t.Margin,
                    t.ProfitLoss,
                    t.Date,
                    t.ScreenshotLink,
                    Risk = t.Risk,       // This will now execute the C# getter
                    Reward = t.Reward,   // This will now execute the C# getter
                    RR = t.RR            // This will now execute the C# getter
                }).ToList();

                if (dgvData.Columns["Id"] != null)
                {
                    dgvData.Columns["Id"].Visible = false;
                }

                decimal totalPnl = trades.Sum(t => t.ProfitLoss);
            }
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

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Get the ID of the trade from the clicked row.
            // Make sure your grid has a column for the ID.
            if (dgvData.Rows[e.RowIndex].Cells["Id"].Value is int tradeId)
            {
                byte[] imageBytes;
                using (var db = new AppDbContext())
                {
                    // Find the trade and get only the screenshot bytes
                    imageBytes = db.Trades
                                   .Where(t => t.Id == tradeId)
                                   .Select(t => t.ScreenshotImage)
                                   .FirstOrDefault();
                }

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Convert byte array to Image
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);

                        // Show the image in the new form
                        using (var viewer = new FrmImageViewer(image))
                        {
                            viewer.ShowDialog();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No screenshot is available for this trade.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dtpFilterDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTrades(dtpFilterDate.Value);
        }

        private void btnUploadScreenshot_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // If it's a file, allow the drop and show a "copy" cursor.
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                // Otherwise, deny the drop.
                e.Effect = DragDropEffects.None;
            }
        }

        private void btnUploadScreenshot_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // We only care about the first file.
            if (files != null && files.Length > 0)
            {
                string filePath = files[0];

                // --- Optional but Recommended: Validate the file type ---
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };

                if (allowedExtensions.Contains(extension))
                {
                    // The file is a valid image, so process it.
                    // This is the SAME logic from your btnUploadScreenshot_Click and OpenFileDialog.
                    var image = Image.FromFile(filePath);
                    pbScreenshot.Image = image;
                    txtScreenshotLink.Text = filePath; // Store the path to be saved.
                }
                else
                {
                    MessageBox.Show("Please drop a valid image file (JPG, PNG, BMP).", "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void EnterUpdateMode(int tradeId)
        {
            _tradeIdToUpdate = tradeId;

            btnAddTrade.Visible = false;
            btnClearData.Visible = false;
            btnUpdateTrade.Visible = true;
            btnCancelUpdate.Visible = true;
        }

        private void ExitUpdateMode()
        {
            _tradeIdToUpdate = null;

            btnAddTrade.Visible = true;
            btnClearData.Visible = true;
            btnUpdateTrade.Visible = false;
            btnCancelUpdate.Visible = false;

            ClearForm();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int tradeId = (int)dgvData.Rows[e.RowIndex].Cells["Id"].Value;

            // --- Check if the DELETE button was clicked ---
            if (dgvData.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                // Ask for confirmation
                var confirmResult = MessageBox.Show("Are you sure you want to delete this trade?",
                                                 "Confirm Deletion",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    using (var db = new AppDbContext())
                    {
                        var tradeToDelete = db.Trades.Find(tradeId);
                        if (tradeToDelete != null)
                        {
                            db.Trades.Remove(tradeToDelete);
                            db.SaveChanges();
                        }
                    }
                    if (chbAllTrades.Checked)
                        LoadTrades(null); // Load all trades
                    else
                        LoadTrades(dtpFilterDate.Value); // Refresh the grid
                }
            }
            // --- Check if the UPDATE button was clicked ---
            else if (dgvData.Columns[e.ColumnIndex].Name == "UpdateColumn")
            {
                // 1. Get the trade data from the database
                Trade tradeToUpdate;
                using (var db = new AppDbContext())
                {
                    tradeToUpdate = db.Trades.Find(tradeId);
                }

                if (tradeToUpdate != null)
                {
                    // 2. Populate the form fields
                    txtEntryPrice.Text = tradeToUpdate.EntryPrice.ToString();
                    txtExitPrice.Text = tradeToUpdate.ExitPrice.ToString();
                    txtStopLoss.Text = tradeToUpdate.StopLoss.ToString();
                    txtTakeProfit.Text = tradeToUpdate.TakeProfit.ToString();
                    txtMargin.Text = tradeToUpdate.Margin.ToString(); // Position Size
                    txtProfitLoss.Text = tradeToUpdate.ProfitLoss.ToString();
                    cbSymbol.Text = tradeToUpdate.Symbol;
                    cbTradeType.SelectedItem = tradeToUpdate.TradeType;

                    // 3. Switch the form to "Update Mode"
                    EnterUpdateMode(tradeId);
                }
            }
        }

        private void btnUpdateTrade_Click(object sender, EventArgs e)
        {
            if (_tradeIdToUpdate == null) return; // Safety check

            // --- (Your existing parsing and validation logic is perfect, keep it) ---
            if (string.IsNullOrWhiteSpace(cbSymbol.Text) || cbTradeType.SelectedItem == null)
            {
                MessageBox.Show("Symbol and Trade Type are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal entryPrice, exitPrice = 0, stopLoss = 0, takeProfit = 0, margin = 0, profitLoss = 0;
            if (!decimal.TryParse(txtEntryPrice.Text, out entryPrice))
            {
                MessageBox.Show("Please enter a valid Entry Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal.TryParse(txtExitPrice.Text, out exitPrice);
            decimal.TryParse(txtStopLoss.Text, out stopLoss);
            decimal.TryParse(txtTakeProfit.Text, out takeProfit);
            decimal.TryParse(txtMargin.Text, out margin);
            decimal.TryParse(txtProfitLoss.Text, out profitLoss);
            string screenshotPath = txtScreenshotLink.Text;
            // --- End of parsing logic ---

            try
            {
                // --- CALL THE NEW MANAGER METHOD ---
                var manager = new TradeManager();
                manager.UpdateTrade(
                    _tradeIdToUpdate.Value,
                    cbSymbol.Text,
                    cbTradeType.SelectedItem.ToString(),
                    entryPrice,
                    exitPrice,
                    stopLoss,
                    takeProfit,
                    margin,
                    profitLoss,
                    screenshotFilePath: screenshotPath
                );

                if (chbAllTrades.Checked)
                    LoadTrades(null); // Load all trades
                else
                    LoadTrades(dtpFilterDate.Value); // Refresh the grid

                ExitUpdateMode(); // Revert the form to "Add Mode"
                MessageBox.Show("Trade updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the trade: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            ExitUpdateMode();
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ApplyBaseGridStyling()
        {
            // --- Core Colors & Look (unchanging styles) ---
            dgvData.BackgroundColor = Color.FromArgb(27, 38, 59);
            dgvData.BorderStyle = BorderStyle.None;
            dgvData.GridColor = Color.FromArgb(45, 51, 73);

            // --- Header Colors ---
            dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 51, 73);
            dgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvData.EnableHeadersVisualStyles = false;

            // --- Row & Cell Colors ---
            dgvData.RowHeadersVisible = false;
            dgvData.DefaultCellStyle.BackColor = Color.FromArgb(24, 30, 54);
            dgvData.DefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
            dgvData.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 51, 73);
            dgvData.DefaultCellStyle.SelectionForeColor = Color.White;

            // --- Alternating Row Style ---
            dgvData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(34, 40, 64);

            // --- Button Column Colors ---
            dgvData.Columns["UpdateColumn"].DefaultCellStyle.BackColor = Color.FromArgb(24, 30, 54);
            dgvData.Columns["DeleteColumn"].DefaultCellStyle.BackColor = Color.FromArgb(24, 30, 54);
            dgvData.Columns["UpdateColumn"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 51, 73);
            dgvData.Columns["DeleteColumn"].DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 51, 73);
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // --- Paint the DELETE button ---
            if (e.ColumnIndex == dgvData.Columns["DeleteColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Define the button's appearance
                var buttonBounds = e.CellBounds;
                buttonBounds.Inflate(-2, -2); // Make the button slightly smaller than the cell
                var buttonColor = Color.FromArgb(220, 53, 69); // A nice bootstrap-style red

                // Draw the button
                ControlPaint.DrawButton(e.Graphics, buttonBounds, ButtonState.Normal);
                e.Graphics.FillRectangle(new SolidBrush(buttonColor), buttonBounds);

                // Draw the text
                TextRenderer.DrawText(e.Graphics, "Delete", e.CellStyle.Font, buttonBounds, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                e.Handled = true; // Tell the grid we've handled the painting for this cell
            }

            // --- Paint the UPDATE button ---
            if (e.ColumnIndex == dgvData.Columns["UpdateColumn"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var buttonBounds = e.CellBounds;
                buttonBounds.Inflate(-2, -2);
                var buttonColor = Color.FromArgb(0, 123, 255); // A nice bootstrap-style blue

                ControlPaint.DrawButton(e.Graphics, buttonBounds, ButtonState.Normal);
                e.Graphics.FillRectangle(new SolidBrush(buttonColor), buttonBounds);

                TextRenderer.DrawText(e.Graphics, "Update", e.CellStyle.Font, buttonBounds, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                e.Handled = true;
            }
        }

        private async void LoadSymbols()
        {
            try
            {
                string url = "https://api.binance.com/api/v3/exchangeInfo";
                var response = await _httpClient.GetStringAsync(url);

                using var doc = JsonDocument.Parse(response);
                var symbols = new List<string>();

                foreach (var element in doc.RootElement.GetProperty("symbols").EnumerateArray())
                {
                    string symbol = element.GetProperty("symbol").GetString();

                    // Only take symbols ending with "USDT"
                    if (symbol.EndsWith("USDT"))
                    {
                        // Insert a slash before USDT → e.g., "SOLUSDT" → "SOL/USDT"
                        string formatted = symbol.Replace("USDT", "/USDT");
                        symbols.Add(formatted);
                    }
                }

                cbSymbol.DataSource = symbols;
                cbSymbol.SelectedIndex = -1; // No default selection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading symbols: {ex.Message}");
            }
        }

        private void chbAllTrades_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAllTrades.Checked)
            {
                dtpFilterDate.Enabled = false;       // Disable calendar
                LoadTrades(null);                    // Show ALL trades
            }
            else
            {
                dtpFilterDate.Enabled = true;        // Enable calendar
                LoadTrades(dtpFilterDate.Value);     // Show trades for selected day
            }
        }
    }
}
