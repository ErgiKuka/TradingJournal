using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class RoundedFormHelper
{
    // Apply rounded corners to any form
    public static void ApplyRoundedCorners(Form form, int radius = 20)
    {
        var path = new GraphicsPath();
        path.StartFigure();

        // Top-left corner
        path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
        // Top-right corner
        path.AddArc(new Rectangle(form.Width - radius, 0, radius, radius), 270, 90);
        // Bottom-right corner
        path.AddArc(new Rectangle(form.Width - radius, form.Height - radius, radius, radius), 0, 90);
        // Bottom-left corner
        path.AddArc(new Rectangle(0, form.Height - radius, radius, radius), 90, 90);

        path.CloseFigure();
        form.Region = new Region(path);
    }

    // Allow dragging borderless form
    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    private const int WM_NCLBUTTONDOWN = 0xA1;
    private const int HTCAPTION = 0x2;

    public static void EnableDrag(Form form, Control dragArea)
    {
        dragArea.MouseDown += (s, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(form.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        };
    }
    public static void MakeButtonRounded(Button btn, int radius)
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
    public static void RoundPanel(Panel panel, int radius = 15)
    {
        var path = new GraphicsPath();
        path.AddArc(0, 0, radius, radius, 180, 90);
        path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
        path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
        path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
        path.CloseAllFigures();
        panel.Region = new Region(path);
    }

    public static void RoundTextBox(TextBox textBox, int radius = 15)
    {
        var path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90); // top-left
        path.AddArc(new Rectangle(textBox.Width - radius, 0, radius, radius), 270, 90); // top-right
        path.AddArc(new Rectangle(textBox.Width - radius, textBox.Height - radius, radius, radius), 0, 90); // bottom-right
        path.AddArc(new Rectangle(0, textBox.Height - radius, radius, radius), 90, 90); // bottom-left
        path.CloseFigure();

        textBox.Region = new Region(path);
    }
}
