namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class BitmapButton : Button
    {
        private Color _BorderColor = ARGBColors.DarkBlue;
        private bool _BorderDrawing = true;
        private bool _FocusRectangleEnabled;
        private Color _ImageBorderColor = ARGBColors.Chocolate;
        private bool _ImageBorderEnabled = true;
        private bool _ImageDropShadow = true;
        private System.Drawing.Image _ImageFocused;
        private System.Drawing.Image _ImageInactive;
        private System.Drawing.Image _ImageMouseOver;
        private System.Drawing.Image _ImageNormal;
        private System.Drawing.Image _ImagePressed;
        private Color _InnerBorderColor = ARGBColors.LightGray;
        private Color _InnerBorderColor_Focus = ARGBColors.LightBlue;
        private Color _InnerBorderColor_MouseOver = ARGBColors.Gold;
        private bool _OffsetPressedContent = true;
        private int _Padding = 5;
        private bool _StretchImage;
        private bool _TextDropShadow;
        private BtnState btnState = BtnState.Normal;
        private bool CapturingMouse;
        private Container components;

        public BitmapButton()
        {
            this.InitializeComponent();
        }

        private static Color Blend(Color SColor, Color DColor, int Percentage)
        {
            int red = SColor.R + (((DColor.R - SColor.R) * Percentage) / 100);
            int green = SColor.G + (((DColor.G - SColor.G) * Percentage) / 100);
            int blue = SColor.B + (((DColor.B - SColor.B) * Percentage) / 100);
            return Color.FromArgb(red, green, blue);
        }

        private void border_Contract(int nPixel, ref Point[] pts)
        {
            int num = nPixel;
            pts[0].X += num;
            pts[0].Y += num;
            pts[1].X -= num;
            pts[1].Y += num;
            pts[2].X -= num;
            pts[2].Y += num;
            pts[3].X -= num;
            pts[3].Y += num;
            pts[4].X -= num;
            pts[4].Y -= num;
            pts[5].X -= num;
            pts[5].Y -= num;
            pts[6].X -= num;
            pts[6].Y -= num;
            pts[7].X += num;
            pts[7].Y -= num;
            pts[8].X += num;
            pts[8].Y -= num;
            pts[9].X += num;
            pts[9].Y -= num;
            pts[10].X += num;
            pts[10].Y += num;
            pts[11].X += num;
            pts[10].Y += num;
        }

        private Point[] border_Get(int nLeftEdge, int nTopEdge, int nWidth, int nHeight)
        {
            int x = nWidth;
            int y = nHeight;
            Point[] pointArray = new Point[] { new Point(1, 0), new Point(x - 1, 0), new Point(x - 1, 1), new Point(x, 1), new Point(x, y - 1), new Point(x - 1, y - 1), new Point(x - 1, y), new Point(1, y), new Point(1, y - 1), new Point(0, y - 1), new Point(0, 1), new Point(1, 1) };
            for (int i = 0; i < pointArray.Length; i++)
            {
                pointArray[i].Offset(nLeftEdge, nTopEdge);
            }
            return pointArray;
        }

        private Point Calculate_LeftEdgeTopEdge(ContentAlignment Alignment, Rectangle rect, int nWidth, int nHeight)
        {
            Point point = new Point(0, 0);
            switch (Alignment)
            {
                case ContentAlignment.TopLeft:
                    point.X = 0;
                    point.Y = 0;
                    break;

                case ContentAlignment.TopCenter:
                    point.X = (rect.Width - nWidth) / 2;
                    point.Y = 0;
                    break;

                case ContentAlignment.TopRight:
                    point.X = rect.Width - nWidth;
                    point.Y = 0;
                    break;

                case ContentAlignment.MiddleLeft:
                    point.X = 0;
                    point.Y = (rect.Height - nHeight) / 2;
                    break;

                case ContentAlignment.MiddleCenter:
                    point.X = (rect.Width - nWidth) / 2;
                    point.Y = (rect.Height - nHeight) / 2;
                    break;

                case ContentAlignment.MiddleRight:
                    point.X = rect.Width - nWidth;
                    point.Y = (rect.Height - nHeight) / 2;
                    break;

                case ContentAlignment.BottomLeft:
                    point.X = 0;
                    point.Y = rect.Height - nHeight;
                    break;

                case ContentAlignment.BottomCenter:
                    point.X = (rect.Width - nWidth) / 2;
                    point.Y = rect.Height - nHeight;
                    break;

                case ContentAlignment.BottomRight:
                    point.X = rect.Width - nWidth;
                    point.Y = rect.Height - nHeight;
                    break;
            }
            point.X += rect.Left;
            point.Y += rect.Top;
            return point;
        }

        public Bitmap ConvertToGrayscale(Bitmap source)
        {
            Bitmap bitmap = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color pixel = source.GetPixel(j, i);
                    int red = (int) (((pixel.R * 0.3) + (pixel.G * 0.59)) + (pixel.B * 0.11));
                    bitmap.SetPixel(j, i, Color.FromArgb(red, red, red));
                }
            }
            return bitmap;
        }

        private void CreateRegion(int nContract)
        {
            Point[] pts = this.border_Get(0, 0, base.Width, base.Height);
            this.border_Contract(nContract, ref pts);
            GraphicsPath path = new GraphicsPath();
            path.AddLines(pts);
            base.Region = new Region(path);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (base.Region != null)
                {
                    base.Region.Dispose();
                    base.Region = null;
                }
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private System.Drawing.Image GetCurrentImage(BtnState btnState)
        {
            System.Drawing.Image imageNormal = this.ImageNormal;
            switch (btnState)
            {
                case BtnState.Inactive:
                    if (this.ImageInactive == null)
                    {
                        if (imageNormal != null)
                        {
                            this.ImageInactive = this.ConvertToGrayscale(new Bitmap(this.ImageNormal));
                        }
                        return this.ImageNormal;
                    }
                    return this.ImageInactive;

                case BtnState.Normal:
                    if (this.Focused && (this.ImageFocused != null))
                    {
                        imageNormal = this.ImageFocused;
                    }
                    return imageNormal;

                case BtnState.MouseOver:
                    if (this.ImageMouseOver != null)
                    {
                        imageNormal = this.ImageMouseOver;
                    }
                    return imageNormal;

                case BtnState.Pushed:
                    if (this.ImagePressed != null)
                    {
                        imageNormal = this.ImagePressed;
                    }
                    return imageNormal;
            }
            return imageNormal;
        }

        private Rectangle GetImageDestinationRect()
        {
            Rectangle rectangle = new Rectangle(0, 0, 0, 0);
            System.Drawing.Image currentImage = this.GetCurrentImage(this.btnState);
            if (currentImage != null)
            {
                if (this.StretchImage)
                {
                    rectangle.Width = base.Width;
                    rectangle.Height = base.Height;
                    return rectangle;
                }
                rectangle.Width = currentImage.Width;
                rectangle.Height = currentImage.Height;
                Rectangle rect = new Rectangle(0, 0, base.Width, base.Height);
                rect.Inflate(-this.Padding2, -this.Padding2);
                Point pos = this.Calculate_LeftEdgeTopEdge(base.ImageAlign, rect, currentImage.Width, currentImage.Height);
                rectangle.Offset(pos);
            }
            return rectangle;
        }

        private Rectangle GetTextDestinationRect()
        {
            Rectangle imageDestinationRect = this.GetImageDestinationRect();
            Rectangle rectangle2 = new Rectangle(0, 0, 0, 0);
            switch (base.ImageAlign)
            {
                case ContentAlignment.TopLeft:
                    rectangle2 = new Rectangle(0, imageDestinationRect.Bottom, base.Width, base.Height - imageDestinationRect.Bottom);
                    break;

                case ContentAlignment.TopCenter:
                    rectangle2 = new Rectangle(0, imageDestinationRect.Bottom, base.Width, base.Height - imageDestinationRect.Bottom);
                    break;

                case ContentAlignment.TopRight:
                    rectangle2 = new Rectangle(0, imageDestinationRect.Bottom, base.Width, base.Height - imageDestinationRect.Bottom);
                    break;

                case ContentAlignment.MiddleLeft:
                    rectangle2 = new Rectangle(imageDestinationRect.Right, 0, base.Width - imageDestinationRect.Right, base.Height);
                    break;

                case ContentAlignment.MiddleCenter:
                    rectangle2 = new Rectangle(0, imageDestinationRect.Bottom, base.Width, base.Height - imageDestinationRect.Bottom);
                    break;

                case ContentAlignment.MiddleRight:
                    rectangle2 = new Rectangle(0, 0, imageDestinationRect.Left, base.Height);
                    break;

                case ContentAlignment.BottomLeft:
                    rectangle2 = new Rectangle(0, 0, base.Width, imageDestinationRect.Top);
                    break;

                case ContentAlignment.BottomCenter:
                    rectangle2 = new Rectangle(0, 0, base.Width, imageDestinationRect.Top);
                    break;

                case ContentAlignment.BottomRight:
                    rectangle2 = new Rectangle(0, 0, base.Width, imageDestinationRect.Top);
                    break;
            }
            rectangle2.Inflate(-this.Padding2, -this.Padding2);
            return rectangle2;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (base.Enabled)
            {
                this.btnState = BtnState.Normal;
            }
            else
            {
                this.btnState = BtnState.Inactive;
                this.CapturingMouse = false;
                base.Capture = false;
            }
            base.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (base.Enabled)
            {
                this.btnState = BtnState.Normal;
            }
            base.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            base.Capture = true;
            this.CapturingMouse = true;
            this.btnState = BtnState.Pushed;
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.CapturingMouse)
            {
                if (base.Enabled)
                {
                    this.btnState = BtnState.Normal;
                }
                else
                {
                    this.btnState = BtnState.Inactive;
                }
                base.Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.CapturingMouse)
            {
                Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
                this.btnState = BtnState.Normal;
                if (((e.X >= rectangle.Left) && (e.X <= rectangle.Right)) && ((e.Y >= rectangle.Top) && (e.Y <= rectangle.Bottom)))
                {
                    this.btnState = BtnState.Pushed;
                }
                base.Capture = true;
                base.Invalidate();
            }
            else if (this.btnState != BtnState.MouseOver)
            {
                this.btnState = BtnState.MouseOver;
                base.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (base.Enabled)
            {
                this.btnState = BtnState.Normal;
            }
            else
            {
                this.btnState = BtnState.Inactive;
            }
            base.Invalidate();
            this.CapturingMouse = false;
            base.Capture = false;
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.CreateRegion(0);
            this.paint_Background(e);
            this.paint_Text(e);
            this.paint_Image(e);
            if (this.BorderDrawing)
            {
                this.paint_Border(e);
                this.paint_InnerBorder(e);
                this.paint_FocusBorder(e);
            }
        }

        private void paint_Background(PaintEventArgs e)
        {
            if ((e != null) && (e.Graphics != null))
            {
                Graphics graphics = e.Graphics;
                Rectangle rect = new Rectangle(0, 0, base.Size.Width, base.Size.Height);
                Color backColor = this.BackColor;
                if (this.btnState == BtnState.Inactive)
                {
                    backColor = ARGBColors.LightGray;
                }
                backColor = ControlPaint.Light(backColor, 0.1f);
                Color[] colorArray = null;
                float[] numArray = null;
                if (this.btnState == BtnState.Pushed)
                {
                    colorArray = new Color[] { Blend(this.BackColor, ARGBColors.White, 80), Blend(this.BackColor, ARGBColors.White, 40), Blend(this.BackColor, ARGBColors.Black, 0), Blend(this.BackColor, ARGBColors.Black, 0), Blend(this.BackColor, ARGBColors.White, 40), Blend(this.BackColor, ARGBColors.White, 80) };
                    numArray = new float[] { 0f, 0.05f, 0.4f, 0.6f, 0.95f, 1f };
                }
                else
                {
                    colorArray = new Color[] { Blend(backColor, ARGBColors.White, 80), Blend(backColor, ARGBColors.White, 90), Blend(backColor, ARGBColors.White, 30), Blend(backColor, ARGBColors.White, 0), Blend(backColor, ARGBColors.Black, 30), Blend(backColor, ARGBColors.Black, 20) };
                    numArray = new float[] { 0f, 0.15f, 0.4f, 0.65f, 0.95f, 1f };
                }
                ColorBlend blend = new ColorBlend {
                    Colors = colorArray,
                    Positions = numArray
                };
                LinearGradientBrush brush = new LinearGradientBrush(rect, this.BackColor, Blend(this.BackColor, this.BackColor, 10), LinearGradientMode.Vertical) {
                    InterpolationColors = blend
                };
                graphics.FillRectangle(brush, rect);
                brush.Dispose();
            }
        }

        private void paint_Border(PaintEventArgs e)
        {
            if ((e != null) && (e.Graphics != null))
            {
                Pen pen = new Pen(this.BorderColor, 1f);
                Point[] points = this.border_Get(0, 0, base.Width - 1, base.Height - 1);
                e.Graphics.DrawLines(pen, points);
                pen.Dispose();
            }
        }

        private void paint_FocusBorder(PaintEventArgs e)
        {
            if (((e != null) && (e.Graphics != null)) && (this.Focused && this.FocusRectangleEnabled))
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(3, 3, base.Width - 6, base.Height - 6), ARGBColors.Black, this.BackColor);
            }
        }

        private void paint_Image(PaintEventArgs e)
        {
            if ((e != null) && (e.Graphics != null))
            {
                System.Drawing.Image currentImage = this.GetCurrentImage(this.btnState);
                if (currentImage != null)
                {
                    Graphics g = e.Graphics;
                    Rectangle imageDestinationRect = this.GetImageDestinationRect();
                    if ((this.btnState == BtnState.Pushed) && this._OffsetPressedContent)
                    {
                        imageDestinationRect.Offset(1, 1);
                    }
                    if (this.StretchImage)
                    {
                        g.DrawImage(currentImage, imageDestinationRect, 0, 0, currentImage.Width, currentImage.Height, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        this.GetImageDestinationRect();
                        g.DrawImage(currentImage, imageDestinationRect, 0, 0, currentImage.Width, currentImage.Height, GraphicsUnit.Pixel);
                    }
                    this.paint_ImageBorder(g, imageDestinationRect);
                }
            }
        }

        private void paint_ImageBorder(Graphics g, Rectangle ImageRect)
        {
            Rectangle rect = ImageRect;
            if (this.ImageDropShadow)
            {
                Pen pen = new Pen(Color.FromArgb(80, 0, 0, 0));
                Pen pen2 = new Pen(Color.FromArgb(40, 0, 0, 0));
                g.DrawLine(pen, new Point(rect.Right, rect.Bottom), new Point(rect.Right + 1, rect.Bottom));
                g.DrawLine(pen, new Point(rect.Right + 1, rect.Top + 1), new Point(rect.Right + 1, rect.Bottom));
                g.DrawLine(pen2, new Point(rect.Right + 2, rect.Top + 2), new Point(rect.Right + 2, rect.Bottom + 1));
                g.DrawLine(pen, new Point(rect.Left + 1, rect.Bottom + 1), new Point(rect.Right, rect.Bottom + 1));
                g.DrawLine(pen2, new Point(rect.Left + 1, rect.Bottom + 2), new Point(rect.Right + 1, rect.Bottom + 2));
                pen.Dispose();
                pen2.Dispose();
            }
            if (this.ImageBorderEnabled)
            {
                Color[] colorArray = null;
                float[] numArray = null;
                Color imageBorderColor = this.ImageBorderColor;
                if (!base.Enabled)
                {
                    imageBorderColor = ARGBColors.LightGray;
                }
                colorArray = new Color[] { Blend(imageBorderColor, ARGBColors.White, 40), Blend(imageBorderColor, ARGBColors.White, 20), Blend(imageBorderColor, ARGBColors.White, 30), Blend(imageBorderColor, ARGBColors.White, 0), Blend(imageBorderColor, ARGBColors.Black, 30), Blend(imageBorderColor, ARGBColors.Black, 70) };
                numArray = new float[] { 0f, 0.2f, 0.5f, 0.6f, 0.9f, 1f };
                ColorBlend blend = new ColorBlend {
                    Colors = colorArray,
                    Positions = numArray
                };
                LinearGradientBrush brush = new LinearGradientBrush(rect, this.BackColor, Blend(this.BackColor, this.BackColor, 10), LinearGradientMode.Vertical) {
                    InterpolationColors = blend
                };
                Pen pen3 = new Pen(brush, 1f);
                Pen pen4 = new Pen(ARGBColors.Black);
                rect.Inflate(1, 1);
                Point[] pts = this.border_Get(rect.Left, rect.Top, rect.Width, rect.Height);
                this.border_Contract(1, ref pts);
                g.DrawLines(pen4, pts);
                this.border_Contract(1, ref pts);
                g.DrawLines(pen3, pts);
                pen4.Dispose();
                pen3.Dispose();
                brush.Dispose();
            }
        }

        private void paint_InnerBorder(PaintEventArgs e)
        {
            if ((e != null) && (e.Graphics != null))
            {
                Graphics graphics = e.Graphics;
                Rectangle rect = new Rectangle(0, 0, base.Size.Width, base.Size.Height);
                Color backColor = this.BackColor;
                bool flag = false;
                switch (this.btnState)
                {
                    case BtnState.Inactive:
                        backColor = ARGBColors.Gray;
                        break;

                    case BtnState.Normal:
                        if (!this.Focused)
                        {
                            backColor = this.InnerBorderColor;
                            break;
                        }
                        backColor = this.InnerBorderColor_Focus;
                        flag = true;
                        break;

                    case BtnState.MouseOver:
                        backColor = this.InnerBorderColor_MouseOver;
                        break;

                    case BtnState.Pushed:
                        backColor = Blend(this.InnerBorderColor_Focus, ARGBColors.Black, 10);
                        flag = true;
                        break;
                }
                Color[] colorArray = null;
                float[] numArray = null;
                if (this.btnState == BtnState.Pushed)
                {
                    colorArray = new Color[] { Blend(backColor, ARGBColors.Black, 20), Blend(backColor, ARGBColors.Black, 10), Blend(backColor, ARGBColors.White, 0), Blend(backColor, ARGBColors.White, 50), Blend(backColor, ARGBColors.White, 0x55), Blend(backColor, ARGBColors.White, 90) };
                    numArray = new float[] { 0f, 0.2f, 0.5f, 0.6f, 0.9f, 1f };
                }
                else
                {
                    colorArray = new Color[] { Blend(backColor, ARGBColors.White, 80), Blend(backColor, ARGBColors.White, 60), Blend(backColor, ARGBColors.White, 10), Blend(backColor, ARGBColors.White, 0), Blend(backColor, ARGBColors.Black, 20), Blend(backColor, ARGBColors.Black, 50) };
                    numArray = new float[] { 0f, 0.2f, 0.5f, 0.6f, 0.9f, 1f };
                }
                ColorBlend blend = new ColorBlend {
                    Colors = colorArray,
                    Positions = numArray
                };
                LinearGradientBrush brush = new LinearGradientBrush(rect, this.BackColor, Blend(this.BackColor, this.BackColor, 10), LinearGradientMode.Vertical) {
                    InterpolationColors = blend
                };
                Pen pen = new Pen(brush, 1f);
                Point[] pts = this.border_Get(0, 0, base.Width - 1, base.Height - 1);
                this.border_Contract(1, ref pts);
                e.Graphics.DrawLines(pen, pts);
                this.border_Contract(1, ref pts);
                e.Graphics.DrawLines(pen, pts);
                if (flag)
                {
                    this.border_Contract(1, ref pts);
                    e.Graphics.DrawLines(pen, pts);
                }
                pen.Dispose();
                brush.Dispose();
            }
        }

        private void paint_Text(PaintEventArgs e)
        {
            if ((e != null) && (e.Graphics != null))
            {
                Rectangle textDestinationRect = this.GetTextDestinationRect();
                StringFormat format = new StringFormat {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                if ((this.btnState == BtnState.Pushed) && this.OffsetPressedContent)
                {
                    textDestinationRect.Offset(1, 1);
                }
                SizeF ef = this.txt_Size(e.Graphics, this.Text, this.Font);
                Point point = this.Calculate_LeftEdgeTopEdge(this.TextAlign, textDestinationRect, (int) ef.Width, (int) ef.Height);
                if (this.btnState == BtnState.Inactive)
                {
                    textDestinationRect.Offset(1, 1);
                    Brush brush = new SolidBrush(ARGBColors.White);
                    e.Graphics.DrawString(this.Text, this.Font, brush, textDestinationRect, format);
                    brush.Dispose();
                    textDestinationRect.Offset(-1, -1);
                    Brush brush2 = new SolidBrush(Color.FromArgb(50, 50, 50));
                    e.Graphics.DrawString(this.Text, this.Font, brush2, textDestinationRect, format);
                    brush2.Dispose();
                }
                else
                {
                    if (this.TextDropShadow)
                    {
                        Brush brush3 = new SolidBrush(Color.FromArgb(50, ARGBColors.Black));
                        Brush brush4 = new SolidBrush(Color.FromArgb(20, ARGBColors.Black));
                        e.Graphics.DrawString(this.Text, this.Font, brush3, (float) point.X, (float) (point.Y + 1));
                        e.Graphics.DrawString(this.Text, this.Font, brush3, (float) (point.X + 1), (float) point.Y);
                        e.Graphics.DrawString(this.Text, this.Font, brush4, (float) (point.X + 1), (float) (point.Y + 1));
                        e.Graphics.DrawString(this.Text, this.Font, brush4, (float) point.X, (float) (point.Y + 2));
                        e.Graphics.DrawString(this.Text, this.Font, brush4, (float) (point.X + 2), (float) point.Y);
                        brush3.Dispose();
                        brush4.Dispose();
                    }
                    Brush brush5 = new SolidBrush(this.ForeColor);
                    e.Graphics.DrawString(this.Text, this.Font, brush5, textDestinationRect, format);
                    brush5.Dispose();
                }
            }
        }

        private static Color Shade(Color SColor, int RED, int GREEN, int BLUE)
        {
            int r = SColor.R;
            int g = SColor.G;
            int b = SColor.B;
            r += RED;
            if (r > 0xff)
            {
                r = 0xff;
            }
            if (r < 0)
            {
                r = 0;
            }
            g += GREEN;
            if (g > 0xff)
            {
                g = 0xff;
            }
            if (g < 0)
            {
                g = 0;
            }
            b += BLUE;
            if (b > 0xff)
            {
                b = 0xff;
            }
            if (b < 0)
            {
                b = 0;
            }
            return Color.FromArgb(r, g, b);
        }

        private SizeF txt_Size(Graphics g, string strText, Font font)
        {
            return g.MeasureString(strText, font);
        }

        [Browsable(true), RefreshProperties(RefreshProperties.Repaint), Category("Appearance"), Description("Color of the border around the button")]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                this._BorderColor = value;
            }
        }

        [Description("enables the drawing of the border"), Category("Appearance"), Browsable(true), RefreshProperties(RefreshProperties.Repaint)]
        public bool BorderDrawing
        {
            get
            {
                return this._BorderDrawing;
            }
            set
            {
                this._BorderDrawing = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("enables the focus rectangle"), RefreshProperties(RefreshProperties.Repaint)]
        public bool FocusRectangleEnabled
        {
            get
            {
                return this._FocusRectangleEnabled;
            }
            set
            {
                this._FocusRectangleEnabled = value;
            }
        }

        [Browsable(false)]
        public System.Drawing.Image Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint), Browsable(true), Category("Appearance"), Description("Color of the border around the image")]
        public Color ImageBorderColor
        {
            get
            {
                return this._ImageBorderColor;
            }
            set
            {
                this._ImageBorderColor = value;
            }
        }

        [Category("Appearance"), Browsable(true), RefreshProperties(RefreshProperties.Repaint), Description("Enables the bordering of the image")]
        public bool ImageBorderEnabled
        {
            get
            {
                return this._ImageBorderEnabled;
            }
            set
            {
                this._ImageBorderEnabled = value;
            }
        }

        [Browsable(true), RefreshProperties(RefreshProperties.Repaint), Category("Appearance"), Description("enables the image to cast a shadow")]
        public bool ImageDropShadow
        {
            get
            {
                return this._ImageDropShadow;
            }
            set
            {
                this._ImageDropShadow = value;
            }
        }

        [Description("Image to be displayed while the button has focus"), RefreshProperties(RefreshProperties.Repaint), Browsable(true), Category("Appearance")]
        public System.Drawing.Image ImageFocused
        {
            get
            {
                return this._ImageFocused;
            }
            set
            {
                this._ImageFocused = value;
            }
        }

        [Category("Appearance"), RefreshProperties(RefreshProperties.Repaint), Description("Image to be displayed while the button is inactive"), Browsable(true)]
        public System.Drawing.Image ImageInactive
        {
            get
            {
                return this._ImageInactive;
            }
            set
            {
                this._ImageInactive = value;
            }
        }

        [Browsable(false)]
        public int ImageIndex
        {
            get
            {
                return base.ImageIndex;
            }
            set
            {
                base.ImageIndex = value;
            }
        }

        [Browsable(false)]
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return base.ImageList;
            }
            set
            {
                base.ImageList = value;
            }
        }

        [Description("Image to be displayed while the button state is MouseOver"), RefreshProperties(RefreshProperties.Repaint), Browsable(true), Category("Appearance")]
        public System.Drawing.Image ImageMouseOver
        {
            get
            {
                return this._ImageMouseOver;
            }
            set
            {
                this._ImageMouseOver = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint), Browsable(true), Category("Appearance"), Description("Image to be displayed while the button state is in normal state")]
        public System.Drawing.Image ImageNormal
        {
            get
            {
                return this._ImageNormal;
            }
            set
            {
                this._ImageNormal = value;
            }
        }

        [Browsable(true), Category("Appearance"), Description("Image to be displayed while the button state is pressed"), RefreshProperties(RefreshProperties.Repaint)]
        public System.Drawing.Image ImagePressed
        {
            get
            {
                return this._ImagePressed;
            }
            set
            {
                this._ImagePressed = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint), Browsable(true), Category("Appearance"), Description("Color of the inner border when the button does not hvae focus")]
        public Color InnerBorderColor
        {
            get
            {
                return this._InnerBorderColor;
            }
            set
            {
                this._InnerBorderColor = value;
            }
        }

        [Browsable(true), RefreshProperties(RefreshProperties.Repaint), Category("Appearance"), Description("Color of the inner border when the button has focus")]
        public Color InnerBorderColor_Focus
        {
            get
            {
                return this._InnerBorderColor_Focus;
            }
            set
            {
                this._InnerBorderColor_Focus = value;
            }
        }

        [Browsable(true), RefreshProperties(RefreshProperties.Repaint), Category("Appearance"), Description("color of the inner border when the mouse is over the button")]
        public Color InnerBorderColor_MouseOver
        {
            get
            {
                return this._InnerBorderColor_MouseOver;
            }
            set
            {
                this._InnerBorderColor_MouseOver = value;
            }
        }

        [Browsable(true), Description("Set to true if to offset image/text when button is pressed"), RefreshProperties(RefreshProperties.Repaint), Category("Appearance")]
        public bool OffsetPressedContent
        {
            get
            {
                return this._OffsetPressedContent;
            }
            set
            {
                this._OffsetPressedContent = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint), Description("padded pixels around the image and text"), Browsable(true), Category("Appearance")]
        public int Padding2
        {
            get
            {
                return this._Padding;
            }
            set
            {
                this._Padding = value;
            }
        }

        [Browsable(true), Description("stretch the impage to the size of the button"), RefreshProperties(RefreshProperties.Repaint), Category("Appearance")]
        public bool StretchImage
        {
            get
            {
                return this._StretchImage;
            }
            set
            {
                this._StretchImage = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint), Category("Appearance"), Description("enables the text to cast a shadow"), Browsable(true)]
        public bool TextDropShadow
        {
            get
            {
                return this._TextDropShadow;
            }
            set
            {
                this._TextDropShadow = value;
            }
        }
    }
}

