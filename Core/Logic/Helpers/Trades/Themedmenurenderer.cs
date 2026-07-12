using System.Drawing;
using System.Windows.Forms;
using TradingJournal.Core.Logic.Helpers;

namespace TradingJournal.Pl.PlaceHolder
{
    /// <summary>
    /// Renders MenuStrip top-level items as themed buttons: a filled background, a border, and a
    /// hover state — so the nav reads as buttons rather than plain text. Shared by the hubs.
    /// </summary>
    public sealed class ThemedMenuRenderer : ToolStripProfessionalRenderer
    {
        public ThemedMenuRenderer() : base(new Palette()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var g = e.Graphics;
            var rect = new Rectangle(Point.Empty, e.Item.Size);

            bool hot = e.Item.Selected || e.Item.Pressed;
            Color fill = hot ? ThemeManager.ButtonHoverColor : ThemeManager.ButtonColor;

            using (var b = new SolidBrush(fill))
                g.FillRectangle(b, rect);
            using (var pen = new Pen(ThemeManager.ButtonHoverColor))
                g.DrawRectangle(pen, 0, 0, rect.Width - 1, rect.Height - 1);
        }

        private sealed class Palette : ProfessionalColorTable
        {
            public override Color ToolStripGradientBegin => ThemeManager.BackgroundColor;
            public override Color ToolStripGradientMiddle => ThemeManager.BackgroundColor;
            public override Color ToolStripGradientEnd => ThemeManager.BackgroundColor;
            public override Color MenuBorder => ThemeManager.BackgroundColor;
        }
    }
}