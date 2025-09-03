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

    public static void EnableDrag(Form form)
    {
        form.MouseDown += (s, e) =>
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(form.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        };
    }
}
