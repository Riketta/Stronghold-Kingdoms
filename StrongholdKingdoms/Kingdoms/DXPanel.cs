namespace Kingdoms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DXPanel : Panel
    {
        public bool allowDraw;
        public bool resizing;
        public static bool skipPaint;

        public DXPanel()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.UserPaint, true);
            this.BackColor = ARGBColors.Black;
            this.resizing = false;
        }

        public void AllowDraw()
        {
            this.allowDraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (GameEngine.Instance != null)
            {
                if (!skipPaint && this.allowDraw)
                {
                    this.allowDraw = false;
                    GameEngine.Instance.OnPaintCallback();
                }
                skipPaint = false;
            }
            else
            {
                Pen pen = new Pen(ARGBColors.Black);
                e.Graphics.DrawRectangle(pen, e.ClipRectangle);
                pen.Dispose();
            }
        }

        protected override void OnResize(EventArgs e)
        {
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if ((base.Size.Width != 0) && (base.Size.Height != 0))
            {
                if (base.Visible)
                {
                    base.Visible = false;
                    base.OnSizeChanged(e);
                    base.Visible = true;
                }
                else
                {
                    base.OnSizeChanged(e);
                }
            }
        }
    }
}

