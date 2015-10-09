namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GreyOutPanel : UserControl
    {
        private Bitmap _backBuffer;
        private IContainer components;
        private bool forceBackgroundRedraw;
        private Rectangle innerArea = new Rectangle();

        public GreyOutPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void drawImageStretched(Graphics g, Image image, float x, float y, float width, float height)
        {
            RectangleF ef;
            if (image.Width == 1)
            {
                ef = new RectangleF(0f, 0f, 1E-05f, (float) image.Height);
            }
            else
            {
                ef = new RectangleF(0f, 0f, (float) image.Width, 1E-05f);
            }
            RectangleF destRect = new RectangleF(x, y, width, height);
            g.DrawImage(image, destRect, ef, GraphicsUnit.Pixel);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if ((this._backBuffer == null) || this.forceBackgroundRedraw)
            {
                if (this._backBuffer == null)
                {
                    this._backBuffer = new Bitmap(base.Size.Width, base.Size.Height);
                }
                this.forceBackgroundRedraw = false;
                Graphics g = Graphics.FromImage(this._backBuffer);
                Brush brush = new SolidBrush(ARGBColors.Black);
                g.FillRectangle(brush, new Rectangle(base.Location, base.Size));
                brush.Dispose();
                int x = this.innerArea.X;
                int y = this.innerArea.Y;
                if ((x > 0) || (y > 0))
                {
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topleft, x - 0x80, y - 0x80, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topright, x + this.innerArea.Width, y - 0x80, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomleft, x - 0x80, y + this.innerArea.Height, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomright, x + this.innerArea.Width, y + this.innerArea.Height, 0x80, 0x80);
                    if (x > 0)
                    {
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_top, (float) x, (float) (y - 0x80), (float) this.innerArea.Width, 128f);
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_bottom, (float) x, (float) (y + this.innerArea.Height), (float) this.innerArea.Width, 128f);
                    }
                    if (y > 0)
                    {
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_left, (float) (x - 0x80), (float) y, 128f, (float) this.innerArea.Height);
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_right, (float) (x + this.innerArea.Width), (float) y, 128f, (float) this.innerArea.Height);
                    }
                }
                g.Dispose();
            }
            if (e != null)
            {
                e.Graphics.DrawImageUnscaled(this._backBuffer, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this._backBuffer != null)
            {
                this._backBuffer.Dispose();
                this._backBuffer = null;
                base.Invalidate();
            }
            base.OnSizeChanged(e);
        }

        public void setInnerArea(Rectangle area)
        {
            this.innerArea = area;
            this.forceBackgroundRedraw = true;
        }
    }
}

