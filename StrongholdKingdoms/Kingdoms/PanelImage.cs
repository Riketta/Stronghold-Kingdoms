namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class PanelImage : Panel
    {
        private float alpha = 1f;
        private IContainer components;

        public PanelImage()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        private ImageAttributes createAlpha(float alpha)
        {
            ColorMatrix matrix;
            matrix = new ColorMatrix {
                Matrix00 = 1f,
                Matrix11 = 1f,
                Matrix22 = 1f,
                Matrix44 = 1f,
                Matrix33 = alpha
            };
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix);
            return attributes;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle destRect = new Rectangle(0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height);
            e.Graphics.DrawImage(this.BackgroundImage, destRect, destRect.X, destRect.Y, destRect.Width, destRect.Height, GraphicsUnit.Pixel, this.createAlpha(this.alpha));
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.BackColor == ARGBColors.Transparent)
            {
                Rectangle rect = new Rectangle(-base.Location.X, -base.Location.Y, base.Parent.Width, base.Parent.Height);
                LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0xc5, 0xbd, 0x9e), Color.FromArgb(0x8d, 0x7e, 0x69), LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, rect);
                brush.Dispose();
            }
            else
            {
                Brush brush2 = new SolidBrush(this.BackColor);
                Rectangle rectangle2 = new Rectangle(0, 0, base.Size.Width, base.Size.Height);
                e.Graphics.FillRectangle(brush2, rectangle2);
                brush2.Dispose();
            }
        }

        public float Alpha
        {
            get
            {
                return this.alpha;
            }
            set
            {
                if (this.alpha != value)
                {
                    this.alpha = value;
                    base.Invalidate();
                }
            }
        }
    }
}

