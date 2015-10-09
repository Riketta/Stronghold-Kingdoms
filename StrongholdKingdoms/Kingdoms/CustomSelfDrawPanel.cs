namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using StatTracking;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class CustomSelfDrawPanel : UserControl
    {
        public CSDControl baseControl = new CSDControl();
        public CSDControl ClickedControl;
        private static Point clickedPosition = new Point();
        public bool ClickHandled;
        private bool clickThru;
        private Stack<Rectangle> clipRectStack = new Stack<Rectangle>();
        private Stack<Region> clipStack = new Stack<Region>();
        private IContainer components;
        private Rectangle currentClip = Rectangle.Empty;
        private bool inDXDraw;
        private bool inNormalDraw;
        private static List<InvalidRectpair> invalidRectList = new List<InvalidRectpair>();
        private Point lastMousePosition = new Point();
        public static Color MailBodyColor = Color.FromArgb(0xea, 0xf5, 0xfd);
        public static Color MailLineColor = Color.FromArgb(0xdf, 0xed, 0xf9);
        public static Color MailLineOverColor = Color.FromArgb(0xdf, 0xed, 0xf9);
        public static Color MailOverColor = Color.FromArgb(0xf7, 0xfc, 0xfe);
        public static Color MailSelectedColor = Color.FromArgb(0xc0, 0xde, 0xed);
        public static Color MailSelectedOverColor = Color.FromArgb(0xdd, 0xf1, 0xf9);
        private Point mouseDownLocation = new Point();
        private static Point mousePosition = new Point();
        private bool mouseReallyPressed;
        private bool noDrawBackground;
        public CSDControl OverControl;
        private bool panelActive = true;
        private static Rectangle screenClipRect = new Rectangle();
        private bool selfDrawBackground;
        public static CSDControl StaticClickedControl = null;
        private Graphics storedGraphics;
        public bool tooltipSet;
        private List<CSDControl> trapMouseEvents = new List<CSDControl>();

        public CustomSelfDrawPanel()
        {
            this.InitializeComponent();
            base.MouseWheel += new MouseEventHandler(this.CustomSelfDrawPanel_MouseWheel);
        }

        public void addControl(CSDControl control)
        {
            this.baseControl.setCustomSelfDrawPanel(this);
            this.baseControl.addControl(control);
        }

        public void addTrapMouseEvent(CSDControl control)
        {
            if (!this.trapMouseEvents.Contains(control))
            {
                this.trapMouseEvents.Add(control);
            }
        }

        public void clearControls()
        {
            this.baseControl.clearControls();
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

        private ImageAttributes createAlphaBrighten(float alpha)
        {
            ColorMatrix matrix;
            matrix = new ColorMatrix {
                Matrix00 = 1f,
                Matrix11 = 1f,
                Matrix22 = 1f,
                Matrix44 = 1f,

                Matrix30 = 0.1f,
                Matrix31 = 0.1f,
                Matrix32 = 0.1f,
                Matrix33 = alpha
            };
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix);
            return attributes;
        }

        private ImageAttributes createColour(Color color, float alpha)
        {
            ColorMatrix newColorMatrix = new ColorMatrix {
                Matrix00 = ((float) color.R) / 255f,
                Matrix11 = ((float) color.G) / 255f,
                Matrix22 = ((float) color.B) / 255f,
                Matrix44 = 1f,
                Matrix33 = alpha
            };
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(newColorMatrix);
            return attributes;
        }

        private void CustomSelfDrawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panelActive)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point location = e.Location;
                    this.lastMousePosition = location;
                    if ((Math.Abs((int) (this.lastMousePosition.X - this.mouseDownLocation.X)) <= 4) && (Math.Abs((int) (this.lastMousePosition.Y - this.mouseDownLocation.Y)) <= 4))
                    {
                        this.ClickHandled = this.baseControl.parentClicked(location);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Point mousePos = e.Location;
                    this.lastMousePosition = mousePos;
                    if ((Math.Abs((int) (this.lastMousePosition.X - this.mouseDownLocation.X)) <= 4) && (Math.Abs((int) (this.lastMousePosition.Y - this.mouseDownLocation.Y)) <= 4))
                    {
                        this.baseControl.parentRightClicked(mousePos);
                    }
                }
            }
        }

        private void CustomSelfDrawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.panelActive)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point location = e.Location;
                    this.mouseReallyPressed = true;
                    this.lastMousePosition = location;
                    this.mouseDownLocation = location;
                    this.baseControl.parentMouseDown(location);
                }
                if (e.Button == MouseButtons.Right)
                {
                    Point point2 = e.Location;
                    this.lastMousePosition = point2;
                    this.mouseDownLocation = point2;
                }
            }
        }

        private void CustomSelfDrawPanel_MouseEnter(object sender, EventArgs e)
        {
            if (!this.panelActive)
            {
            }
        }

        private void CustomSelfDrawPanel_MouseLeave(object sender, EventArgs e)
        {
            if (this.panelActive)
            {
                CustomTooltipManager.MouseLeaveTooltipArea();
                this.baseControl.handleMouseLeave(null);
            }
        }

        private void CustomSelfDrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.panelActive)
            {
                CustomTooltipManager.MouseLeaveTooltipArea();
                this.tooltipSet = false;
                Point location = e.Location;
                this.lastMousePosition = location;
                if (this.baseControl.parentMouseOver(location) == null)
                {
                    this.baseControl.handleMouseLeave(null);
                }
                this.manageTrappedMouseEvents();
            }
        }

        private void CustomSelfDrawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.panelActive)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Point location = e.Location;
                    this.mouseReallyPressed = false;
                    this.lastMousePosition = location;
                    this.baseControl.parentMouseUp(location);
                    this.manageTrappedMouseEvents();
                }
                if (e.Button == MouseButtons.Right)
                {
                    Point point2 = e.Location;
                    this.lastMousePosition = point2;
                }
            }
        }

        private void CustomSelfDrawPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.panelActive)
            {
                int delta = e.Delta / SystemInformation.MouseWheelScrollDelta;
                Point location = e.Location;
                this.lastMousePosition = location;
                this.baseControl.parentMouseWheel(location, delta);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void drawControls()
        {
            this.baseControl.drawControls(new Point(0, 0));
        }

        public static void drawGradientPanel(Graphics mGraphics, Size size)
        {
            drawGradientPanel(mGraphics, new Point(), size);
        }

        public static void drawGradientPanel(Graphics mGraphics, Point pos, Size size)
        {
            Rectangle rect = new Rectangle(pos.X, pos.Y, size.Width, size.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0x56, 0x62, 0x6a), Color.FromArgb(0x9f, 180, 0xc1), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(brush, rect);
            Pen pen = new Pen(Color.FromArgb(0x9f, 180, 0xc1), 1f);
            Rectangle rectangle2 = new Rectangle(pos.X, pos.Y, size.Width - 1, size.Height - 1);
            mGraphics.DrawRectangle(pen, rectangle2);
            Pen pen2 = new Pen(Color.FromArgb(0x56, 0x62, 0x6a), 1f);
            Rectangle rectangle3 = new Rectangle(pos.X + 1, pos.Y, size.Width - 3, size.Height - 2);
            mGraphics.DrawRectangle(pen2, rectangle3);
            pen.Dispose();
            pen2.Dispose();
            brush.Dispose();
        }

        protected void drawImage(Image image, Point dest)
        {
            Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destRect = new Rectangle(dest.X, dest.Y, image.Width, image.Height);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
            }
        }

        protected void drawImage(Image image, Point dest, float alpha)
        {
            Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destRect = new Rectangle(dest.X, dest.Y, image.Width, image.Height);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
            }
        }

        protected void drawImage(Image image, Rectangle source, Rectangle dest)
        {
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, dest, source, GraphicsUnit.Pixel);
            }
        }

        protected void drawImage(Image image, RectangleF source, RectangleF dest)
        {
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, dest, source, GraphicsUnit.Pixel);
            }
        }

        protected void drawImage(Image image, Point dest, float alpha, double scale)
        {
            double num = image.Width * scale;
            double num2 = image.Height * scale;
            Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) num, (int) num2);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
            }
        }

        protected void drawImage(Image image, Rectangle source, Rectangle dest, double scale)
        {
            double num = dest.Width * scale;
            double num2 = dest.Height * scale;
            Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) num, (int) num2);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destRect, source, GraphicsUnit.Pixel);
            }
        }

        protected void drawImage(Image image, Rectangle source, Rectangle dest, float alpha)
        {
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
            }
        }

        protected void drawImage(Image image, RectangleF source, RectangleF dest, float alpha)
        {
            PointF tf = new PointF(dest.Left, dest.Top);
            PointF tf2 = new PointF(dest.Right, dest.Top);
            PointF tf3 = new PointF(dest.Left, dest.Bottom);
            PointF[] destPoints = new PointF[] { tf, tf2, tf3 };
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destPoints, source, GraphicsUnit.Pixel, this.createAlpha(alpha));
            }
        }

        protected void drawImage(Image image, Point dest, float alpha, double scale, Color color)
        {
            double num = image.Width * scale;
            double num2 = image.Height * scale;
            Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) num, (int) num2);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, this.createColour(color, alpha));
            }
        }

        protected void drawImage(Image image, Rectangle source, Rectangle dest, float alpha, double scale)
        {
            double num = dest.Width * scale;
            double num2 = dest.Height * scale;
            Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) num, (int) num2);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destRect, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createAlpha(alpha));
            }
        }

        protected void drawImage(Image image, Rectangle source, Rectangle dest, float alpha, Color color)
        {
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createColour(color, alpha));
            }
        }

        protected void drawImage(Image image, RectangleF source, RectangleF dest, float alpha, Color color)
        {
            PointF tf = new PointF(dest.Left, dest.Top);
            PointF tf2 = new PointF(dest.Right, dest.Top);
            PointF tf3 = new PointF(dest.Left, dest.Bottom);
            PointF[] destPoints = new PointF[] { tf, tf2, tf3 };
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destPoints, source, GraphicsUnit.Pixel, this.createColour(color, alpha));
            }
        }

        protected void drawImage(Image image, Rectangle source, Rectangle dest, float alpha, double scale, Color color)
        {
            double num = dest.Width * scale;
            double num2 = dest.Height * scale;
            Rectangle destRect = new Rectangle(dest.X, dest.Y, (int) num, (int) num2);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, destRect, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createColour(color, alpha));
            }
        }

        protected void drawImageBrighten(Image image, Rectangle source, Rectangle dest, float alpha)
        {
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawImage(image, dest, source.X, source.Y, source.Width, source.Height, GraphicsUnit.Pixel, this.createAlphaBrighten(alpha));
            }
        }

        protected void drawImageColourMap(Image image, Rectangle source, Rectangle dest, ColorMap[] colourMap)
        {
            if (this.StoredGraphics != null)
            {
                ImageAttributes imageAttrs = new ImageAttributes();
                imageAttrs.SetRemapTable(colourMap);
                this.StoredGraphics.DrawImage(image, dest, (float) source.X, (float) source.Y, (float) source.Width, (float) source.Height, GraphicsUnit.Pixel, imageAttrs);
            }
        }

        protected void drawImageMirrorRotate(Image image, Rectangle source, Rectangle dest, bool mirrored, float rotate, PointF rotateCentre)
        {
            if (this.StoredGraphics != null)
            {
                RectangleF srcRect = new RectangleF((float) source.X, (float) source.Y, (float) source.Width, (float) source.Height);
                GraphicsPath path = new GraphicsPath();
                Point[] points = new Point[] { new Point(0, 0), new Point(dest.Width, 0), new Point(0, dest.Height) };
                path.AddPolygon(points);
                Matrix matrix = new Matrix();
                if (mirrored)
                {
                    matrix = new Matrix(-1f, 0f, 0f, 1f, 0f, 0f);
                }
                if (rotate != 0f)
                {
                    if (rotateCentre.X == -1000f)
                    {
                        matrix.RotateAt(rotate, new PointF((float) (source.Width / 2), (float) (source.Height / 2)));
                    }
                    else
                    {
                        matrix.RotateAt(rotate, rotateCentre);
                    }
                }
                if (mirrored)
                {
                    matrix.Translate((float) (dest.X + source.Width), (float) dest.Y, MatrixOrder.Append);
                }
                else
                {
                    matrix.Translate((float) dest.X, (float) dest.Y, MatrixOrder.Append);
                }
                Matrix transform = this.StoredGraphics.Transform;
                path.Transform(matrix);
                PointF[] pathPoints = path.PathPoints;
                this.StoredGraphics.DrawImage(image, pathPoints, srcRect, GraphicsUnit.Pixel);
                this.StoredGraphics.Transform = transform;
            }
        }

        protected void drawImageMirrorRotateAlpha(Image image, Rectangle source, Rectangle dest, bool mirrored, float rotate, PointF rotateCentre, float alpha)
        {
            if (this.StoredGraphics != null)
            {
                RectangleF srcRect = new RectangleF((float) source.X, (float) source.Y, (float) source.Width, (float) source.Height);
                GraphicsPath path = new GraphicsPath();
                Point[] points = new Point[] { new Point(0, 0), new Point(dest.Width, 0), new Point(0, dest.Height) };
                path.AddPolygon(points);
                Matrix matrix = new Matrix();
                if (mirrored)
                {
                    matrix = new Matrix(-1f, 0f, 0f, 1f, 0f, 0f);
                }
                if (rotate != 0f)
                {
                    if (rotateCentre.X == -1000f)
                    {
                        matrix.RotateAt(rotate, new PointF((float) (source.Width / 2), (float) (source.Height / 2)));
                    }
                    else
                    {
                        matrix.RotateAt(rotate, rotateCentre);
                    }
                }
                if (mirrored)
                {
                    matrix.Translate((float) (dest.X + source.Width), (float) dest.Y, MatrixOrder.Append);
                }
                else
                {
                    matrix.Translate((float) dest.X, (float) dest.Y, MatrixOrder.Append);
                }
                Matrix transform = this.StoredGraphics.Transform;
                path.Transform(matrix);
                PointF[] pathPoints = path.PathPoints;
                this.StoredGraphics.DrawImage(image, pathPoints, srcRect, GraphicsUnit.Pixel, this.createAlpha(alpha));
                this.StoredGraphics.Transform = transform;
            }
        }

        protected void drawLine(Color col, Point start, Point end)
        {
            if (this.StoredGraphics != null)
            {
                Pen pen = new Pen(col);
                this.StoredGraphics.DrawLine(pen, start, end);
                pen.Dispose();
            }
        }

        protected void drawRect(Rectangle area, Color col)
        {
            if (this.StoredGraphics != null)
            {
                Pen pen = new Pen(col);
                this.StoredGraphics.DrawRectangle(pen, area);
                pen.Dispose();
            }
        }

        protected void drawSpecialGradient(Rectangle rect)
        {
            if (this.StoredGraphics != null)
            {
                drawGradientPanel(this.StoredGraphics, rect.Location, rect.Size);
            }
        }

        protected void drawString(string text, Rectangle displayRect, Color color, Font font, CSD_Text_Alignment alignment)
        {
            SolidBrush brush = new SolidBrush(color);
            StringFormat format = new StringFormat();
            switch (alignment)
            {
                case CSD_Text_Alignment.TOP_LEFT:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    break;

                case CSD_Text_Alignment.TOP_CENTER:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    break;

                case CSD_Text_Alignment.TOP_RIGHT:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Near;
                    break;

                case CSD_Text_Alignment.CENTER_LEFT:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;
                    break;

                case CSD_Text_Alignment.CENTER_CENTER:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    break;

                case CSD_Text_Alignment.CENTER_RIGHT:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Center;
                    break;

                case CSD_Text_Alignment.BOTTOM_LEFT:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Far;
                    break;

                case CSD_Text_Alignment.BOTTOM_CENTER:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;
                    break;

                case CSD_Text_Alignment.BOTTOM_RIGHT:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Far;
                    break;
            }
            RectangleF layoutRectangle = new RectangleF((float) displayRect.X, (float) displayRect.Y, (float) displayRect.Width, (float) displayRect.Height);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.DrawString(text, font, brush, layoutRectangle, format);
            }
            brush.Dispose();
        }

        public void endPaint()
        {
            this.inNormalDraw = false;
            this.inDXDraw = false;
            this.StoredGraphics = null;
        }

        private void fillRect(Rectangle fillArea, Color fillColor)
        {
            SolidBrush brush = new SolidBrush(fillColor);
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.FillRectangle(brush, fillArea);
            }
            brush.Dispose();
        }

        public void forceStyle()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public Rectangle getCurrentClip()
        {
            if (this.StoredGraphics != null)
            {
                return this.currentClip;
            }
            return Rectangle.Empty;
        }

        public static Point GetMousePosition()
        {
            return mousePosition;
        }

        protected SizeF getStringBounds(string text, int displayWidth, Font font)
        {
            if (this.StoredGraphics != null)
            {
                return this.StoredGraphics.MeasureString(text, font, displayWidth);
            }
            return new SizeF();
        }

        public bool getToolTip(ref int data)
        {
            return this.baseControl.getToolTip(this.lastMousePosition, ref data);
        }

        protected void handleMouseLeave(CSDControl control)
        {
            this.baseControl.handleMouseLeave(control);
        }

        public bool initFromDX(Graphics g, CSDControl control)
        {
            if (!this.inNormalDraw)
            {
                this.inDXDraw = true;
                this.StoredGraphics = g;
                return true;
            }
            return false;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "CustomSelfDrawPanel";
            base.MouseLeave += new EventHandler(this.CustomSelfDrawPanel_MouseLeave);
            base.MouseMove += new MouseEventHandler(this.CustomSelfDrawPanel_MouseMove);
            base.MouseDoubleClick += new MouseEventHandler(this.CustomSelfDrawPanel_MouseClick);
            base.MouseClick += new MouseEventHandler(this.CustomSelfDrawPanel_MouseClick);
            base.MouseDown += new MouseEventHandler(this.CustomSelfDrawPanel_MouseDown);
            base.MouseUp += new MouseEventHandler(this.CustomSelfDrawPanel_MouseUp);
            base.MouseEnter += new EventHandler(this.CustomSelfDrawPanel_MouseEnter);
            base.ResumeLayout(false);
        }

        public bool initOnPaint(PaintEventArgs e)
        {
            if (!this.inDXDraw)
            {
                this.inNormalDraw = true;
                this.StoredGraphics = e.Graphics;
                this.StoredGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                return true;
            }
            return false;
        }

        public void InvalidateCached(Rectangle rect)
        {
            InvalidRectpair item = new InvalidRectpair {
                rect = rect,
                panel = this
            };
            invalidRectList.Add(item);
        }

        public void manageTrappedMouseEvents()
        {
            List<CSDControl> list = null;
            bool flag = true;
            while (flag)
            {
                flag = false;
                try
                {
                    foreach (CSDControl control in this.trapMouseEvents)
                    {
                        if (control.Visible)
                        {
                            control.mouseEventTrapped();
                        }
                        else
                        {
                            if (list == null)
                            {
                                list = new List<CSDControl>();
                            }
                            list.Add(control);
                        }
                    }
                    continue;
                }
                catch (Exception)
                {
                    flag = true;
                    continue;
                }
            }
            if (list != null)
            {
                foreach (CSDControl control2 in list)
                {
                    this.trapMouseEvents.Remove(control2);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (this.initOnPaint(e))
                {
                    screenClipRect = e.ClipRectangle;
                    this.drawControls();
                    this.endPaint();
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            try
            {
                if (!this.selfDrawBackground)
                {
                    if (!this.noDrawBackground)
                    {
                        base.OnPaintBackground(pevent);
                    }
                }
                else if (((base.Parent != null) && (base.Parent.BackgroundImage != null)) && (pevent.Graphics != null))
                {
                    new Rectangle(0, 0, base.Parent.BackgroundImage.Width, base.Parent.BackgroundImage.Height);
                    Rectangle destRect = new Rectangle(-base.Location.X, -base.Location.Y, base.Parent.Size.Width, base.Parent.Size.Height);
                    ImageAttributes imageAttr = new ImageAttributes();
                    imageAttr.SetWrapMode(WrapMode.Tile);
                    pevent.Graphics.DrawImage(base.Parent.BackgroundImage, destRect, 0, 0, base.Parent.BackgroundImage.Width, base.Parent.BackgroundImage.Height, GraphicsUnit.Pixel, imageAttr);
                }
            }
            catch (Exception)
            {
            }
        }

        public static void processInvalidRectCache()
        {
            if (invalidRectList.Count > 0)
            {
                foreach (InvalidRectpair rectpair in invalidRectList)
                {
                    rectpair.panel.Invalidate(rectpair.rect);
                }
                invalidRectList.Clear();
            }
        }

        public void removeControl(CSDControl control)
        {
            this.baseControl.setCustomSelfDrawPanel(this);
            this.baseControl.removeControl(control);
        }

        public void removeTrapMouseEvent(CSDControl control)
        {
            this.trapMouseEvents.Remove(control);
        }

        public void restoreClipRegion()
        {
            if (this.StoredGraphics != null)
            {
                this.StoredGraphics.Clip = this.clipStack.Pop();
                this.currentClip = this.clipRectStack.Pop();
            }
        }

        public void setClipRegion(Rectangle clipRect)
        {
            if (this.StoredGraphics != null)
            {
                this.clipRectStack.Push(this.currentClip);
                this.currentClip = clipRect;
                this.clipStack.Push(this.StoredGraphics.Clip);
                Region region = this.StoredGraphics.Clip.Clone();
                region.Intersect(clipRect);
                this.StoredGraphics.Clip = region;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0x84) && this.clickThru)
            {
                m.Result = (IntPtr) (-1);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public bool ClickThru
        {
            get
            {
                return this.clickThru;
            }
            set
            {
                this.clickThru = value;
            }
        }

        public Point LastMousePosition
        {
            get
            {
                return this.lastMousePosition;
            }
        }

        public bool MouseReallyPressed
        {
            get
            {
                return this.mouseReallyPressed;
            }
        }

        public bool NoDrawBackground
        {
            get
            {
                return this.noDrawBackground;
            }
            set
            {
                this.noDrawBackground = value;
            }
        }

        public bool PanelActive
        {
            get
            {
                return this.panelActive;
            }
            set
            {
                this.panelActive = value;
            }
        }

        public bool SelfDrawBackground
        {
            get
            {
                return this.selfDrawBackground;
            }
            set
            {
                this.selfDrawBackground = value;
            }
        }

        public Graphics StoredGraphics
        {
            get
            {
                return this.storedGraphics;
            }
            set
            {
                this.storedGraphics = value;
            }
        }

        public enum CSD_Text_Alignment
        {
            TOP_LEFT,
            TOP_CENTER,
            TOP_RIGHT,
            CENTER_LEFT,
            CENTER_CENTER,
            CENTER_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_CENTER,
            BOTTOM_RIGHT
        }

        public class CSDArea : CustomSelfDrawPanel.CSDControl
        {
        }

        public class CSDButton : CustomSelfDrawPanel.CSDControl
        {
            private bool active = true;
            private float alpha = 1f;
            protected CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate buttonMouseLeaveDelegate;
            protected CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate buttonMouseOverDelegate;
            private CustomSelfDrawPanel.CSDHorzExtendingPanel clickExt;
            private Color fillRectColor = ARGBColors.White;
            private Color fillRectOverColor = ARGBColors.White;
            private bool fillRectOverVariant;
            private bool fillRectVariant;
            private bool forceFillRect;
            private Image imageClick;
            private Image imageHighlight;
            private Image imageIcon;
            private float imageIconAlpha = 1f;
            private Rectangle imageIconClipRect = new Rectangle(0, 0, 0, 0);
            private Point imageIconPosition = new Point(0, 0);
            private Image imageNorm;
            private Image imageOver;
            private bool lateTextRender;
            private bool moveOnClick = true;
            private CustomSelfDrawPanel.CSDHorzExtendingPanel normalExt;
            private bool overBrighten;
            private CustomSelfDrawPanel.CSDHorzExtendingPanel overExt;
            private bool stretchButtons;
            public CustomSelfDrawPanel.CSDLabel Text;
            public CustomSelfDrawPanel.CSDLabel Text2;
            private int textYOffset = -3; // TODO: Исправить смещение
            private bool useTextSize;

            public CSDButton()
            {
                base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonLeave));
                base.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
                this.Text = new CustomSelfDrawPanel.CSDLabel();
                this.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.Text.setParent(this);
            }

            private void buttonDown()
            {
                base.invalidate();
            }

            private void buttonLeave()
            {
                base.invalidate();
                if (this.buttonMouseLeaveDelegate != null)
                {
                    this.buttonMouseLeaveDelegate();
                }
            }

            private void buttonOver()
            {
                base.invalidate();
                if (this.buttonMouseOverDelegate != null)
                {
                    this.buttonMouseOverDelegate();
                }
            }

            private void buttonUp()
            {
                base.invalidate();
            }

            public void createSubText(string text)
            {
                this.Text2 = new CustomSelfDrawPanel.CSDLabel();
                this.Text2.Size = this.Size;
                this.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.Text2.Text = text;
                this.Text2.setParent(this);
            }

            public override void draw(Point parentLocation)
            {
                Rectangle dest = base.Rectangle;
                dest.X += parentLocation.X;
                dest.Y += parentLocation.Y;
                Point point = new Point(0, this.textYOffset);
                Image imageNorm = this.imageNorm;
                Rectangle source = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
                if (imageNorm != null)
                {
                    source = new Rectangle(0, 0, imageNorm.Width, imageNorm.Height);
                }
                if (this.active)
                {
                    if (this.stretchButtons)
                    {
                        CustomSelfDrawPanel.CSDHorzExtendingPanel normalExt = this.normalExt;
                        if (base.mouseDownFlag)
                        {
                            if (this.clickExt != null)
                            {
                                normalExt = this.clickExt;
                                if (this.moveOnClick)
                                {
                                    point.X++;
                                    point.Y++;
                                }
                            }
                            else
                            {
                                if (this.overExt != null)
                                {
                                    normalExt = this.overExt;
                                }
                                if (this.moveOnClick)
                                {
                                    dest.X++;
                                    dest.Y++;
                                }
                            }
                        }
                        else if (base.mouseOverFlag && (this.overExt != null))
                        {
                            normalExt = this.overExt;
                        }
                        if (normalExt != null)
                        {
                            float num = this.alpha;
                            if (!this.Enabled)
                            {
                                num /= 2f;
                            }
                            Point point2 = new Point(dest.X, dest.Y);
                            normalExt.forceDraw(point2, num);
                        }
                    }
                    else if (this.imageHighlight == null)
                    {
                        if (base.mouseDownFlag)
                        {
                            if (this.imageClick != null)
                            {
                                imageNorm = this.imageClick;
                                if (this.moveOnClick)
                                {
                                    point.X++;
                                    point.Y++;
                                }
                            }
                            else
                            {
                                imageNorm = this.imageOver;
                                if (this.moveOnClick)
                                {
                                    dest.X++;
                                    dest.Y++;
                                }
                            }
                        }
                        else if (base.mouseOverFlag)
                        {
                            imageNorm = this.imageOver;
                        }
                    }
                    else if (base.mouseDownFlag && this.moveOnClick)
                    {
                        dest.X++;
                        dest.Y++;
                    }
                }
                float alpha = this.alpha;
                if (!this.Enabled)
                {
                    alpha /= 2f;
                }
                if (imageNorm != null)
                {
                    if (alpha == 1f)
                    {
                        base.csd.drawImage(imageNorm, source, dest);
                    }
                    else
                    {
                        base.csd.drawImage(imageNorm, source, dest, alpha);
                    }
                }
                else if ((base.mouseOverFlag && (this.imageOver == null)) && this.overBrighten)
                {
                    if (alpha == 1f)
                    {
                        base.csd.drawImageBrighten(this.imageNorm, source, dest, 1f);
                    }
                    else
                    {
                        base.csd.drawImageBrighten(this.imageNorm, source, dest, alpha);
                    }
                }
                if ((this.active && base.mouseOverFlag) && (this.imageHighlight != null))
                {
                    base.csd.drawImage(this.imageHighlight, source, dest);
                }
                if ((this.fillRectVariant && !base.mouseOverFlag) && !base.mouseDownFlag)
                {
                    base.csd.fillRect(dest, this.fillRectColor);
                }
                if (this.fillRectOverVariant && ((base.mouseOverFlag || base.mouseDownFlag) || this.forceFillRect))
                {
                    base.csd.fillRect(dest, this.fillRectOverColor);
                }
                if (!this.lateTextRender)
                {
                    if (this.Text.Text.Length > 0)
                    {
                        Point point3;
                        if ((imageNorm == null) && !this.useTextSize)
                        {
                            this.Text.Size = this.Size;
                        }
                        point3 = new Point(dest.X, dest.Y)
                        {
                            X = dest.X + point.X,
                            Y = dest.Y + point.Y
                        };
                        this.Text.draw(point3);
                    }
                    if ((this.Text2 != null) && (this.Text2.Text.Length > 0))
                    {
                        Point point4;
                        point4 = new Point(dest.X, dest.Y) {
                            X = dest.X + point.X,
                            Y = dest.Y + point.Y
                        };
                        this.Text2.draw(point4);
                    }
                }
                if (this.imageIcon != null)
                {
                    Rectangle rectangle3 = new Rectangle(dest.X + this.imageIconPosition.X, dest.Y + this.imageIconPosition.Y, this.imageIcon.Width, this.imageIcon.Height);
                    Rectangle rectangle4 = new Rectangle(0, 0, this.imageIcon.Width, this.imageIcon.Height);
                    if (!this.imageIconClipRect.IsEmpty)
                    {
                        Rectangle clipRect = new Rectangle((dest.X + this.imageIconPosition.X) + this.imageIconClipRect.X, (dest.Y + this.imageIconPosition.Y) + this.imageIconClipRect.Y, this.imageIconClipRect.Width, this.imageIconClipRect.Height);
                        base.csd.setClipRegion(clipRect);
                    }
                    if (this.imageIconAlpha == 1f)
                    {
                        base.csd.drawImage(this.imageIcon, rectangle4, rectangle3);
                    }
                    else
                    {
                        base.csd.drawImage(this.imageIcon, rectangle4, rectangle3, this.imageIconAlpha);
                    }
                    if (!this.imageIconClipRect.IsEmpty)
                    {
                        base.csd.restoreClipRegion();
                    }
                }
                if (this.lateTextRender)
                {
                    if (this.Text.Text.Length > 0)
                    {
                        Point point5;
                        if ((imageNorm == null) && !this.useTextSize)
                        {
                            this.Text.Size = this.Size;
                        }
                        point5 = new Point(dest.X, dest.Y) {
                            X = dest.X + point.X,
                            Y = dest.Y + point.Y
                        };
                        this.Text.draw(point5);
                    }
                    if ((this.Text2 != null) && (this.Text2.Text.Length > 0))
                    {
                        Point point6;
                        point6 = new Point(dest.X, dest.Y) {
                            X = dest.X + point.X,
                            Y = dest.Y + point.Y
                        };
                        this.Text2.draw(point6);
                    }
                }
            }

            public void setButtonMouseOverDelegate(CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate overDelegate, CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate leaveDelegate)
            {
                this.buttonMouseOverDelegate = overDelegate;
                this.buttonMouseLeaveDelegate = leaveDelegate;
            }

            public void setClickExtImage(Image left, Image mid, Image right)
            {
                this.stretchButtons = true;
                this.clickExt = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
                this.clickExt.Size = this.Size;
                this.clickExt.Position = new Point(0, 0);
                this.clickExt.Create(left, mid, right);
                if (!this.useTextSize)
                {
                    this.Text.Size = this.Size;
                }
            }

            public void setNormalExtImage(Image left, Image mid, Image right)
            {
                this.stretchButtons = true;
                this.normalExt = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
                this.normalExt.Size = this.Size;
                this.normalExt.Position = new Point(0, 0);
                this.normalExt.Create(left, mid, right);
                this.normalExt.setParent(this);
                if (!this.useTextSize)
                {
                    this.Text.Size = this.Size;
                }
            }

            public void setOverExtImage(Image left, Image mid, Image right)
            {
                this.stretchButtons = true;
                this.overExt = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
                this.overExt.Size = this.Size;
                this.overExt.Position = new Point(0, 0);
                this.overExt.Create(left, mid, right);
                this.overExt.setParent(this);
                if (!this.useTextSize)
                {
                    this.Text.Size = this.Size;
                }
            }

            public void setSizeToImage()
            {
                if (this.imageNorm != null)
                {
                    this.Size = this.imageNorm.Size;
                    if (!this.useTextSize)
                    {
                        this.Text.Size = this.Size;
                    }
                }
            }

            public bool Active
            {
                get
                {
                    return this.active;
                }
                set
                {
                    this.active = value;
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
                    this.alpha = value;
                }
            }

            public Color FillRectColor
            {
                get
                {
                    return this.fillRectColor;
                }
                set
                {
                    this.fillRectColor = value;
                    this.fillRectVariant = true;
                }
            }

            public Color FillRectOverColor
            {
                get
                {
                    return this.fillRectOverColor;
                }
                set
                {
                    this.fillRectOverColor = value;
                    this.fillRectOverVariant = true;
                }
            }

            public bool FillRectVariant
            {
                get
                {
                    return this.fillRectVariant;
                }
                set
                {
                    this.fillRectVariant = value;
                }
            }

            public bool ForceFillRect
            {
                get
                {
                    return this.forceFillRect;
                }
                set
                {
                    this.forceFillRect = value;
                }
            }

            public Image ImageClick
            {
                get
                {
                    return this.imageClick;
                }
                set
                {
                    if (this.imageClick != value)
                    {
                        base.invalidate();
                    }
                    this.imageClick = value;
                }
            }

            public Image ImageHighlight
            {
                get
                {
                    return this.imageHighlight;
                }
                set
                {
                    if (this.imageHighlight != value)
                    {
                        base.invalidate();
                    }
                    this.imageHighlight = value;
                }
            }

            public Image ImageIcon
            {
                get
                {
                    return this.imageIcon;
                }
                set
                {
                    if (this.imageIcon != value)
                    {
                        base.invalidate();
                    }
                    this.imageIcon = value;
                }
            }

            public float ImageIconAlpha
            {
                get
                {
                    return this.imageIconAlpha;
                }
                set
                {
                    if (this.imageIconAlpha != value)
                    {
                        base.invalidate();
                    }
                    this.imageIconAlpha = value;
                }
            }

            public Rectangle ImageIconClipRect
            {
                get
                {
                    return this.imageIconClipRect;
                }
                set
                {
                    this.imageIconClipRect = value;
                }
            }

            public Point ImageIconPosition
            {
                get
                {
                    return this.imageIconPosition;
                }
                set
                {
                    this.imageIconPosition = value;
                }
            }

            public Image ImageNorm
            {
                get
                {
                    return this.imageNorm;
                }
                set
                {
                    if (this.imageNorm != value)
                    {
                        base.invalidate();
                    }
                    this.imageNorm = value;
                    this.setSizeToImage();
                }
            }

            public Image ImageNormAndOver
            {
                get
                {
                    return this.imageNorm;
                }
                set
                {
                    if (this.imageNorm != value)
                    {
                        base.invalidate();
                    }
                    this.imageNorm = value;
                    this.imageOver = value;
                    this.setSizeToImage();
                }
            }

            public Image ImageOver
            {
                get
                {
                    return this.imageOver;
                }
                set
                {
                    if (this.imageOver != value)
                    {
                        base.invalidate();
                    }
                    this.imageOver = value;
                }
            }

            public bool LateTextRender
            {
                get
                {
                    return this.lateTextRender;
                }
                set
                {
                    this.lateTextRender = value;
                }
            }

            public bool MoveOnClick
            {
                get
                {
                    return this.moveOnClick;
                }
                set
                {
                    this.moveOnClick = value;
                }
            }

            public bool OverBrighten
            {
                get
                {
                    return this.overBrighten;
                }
                set
                {
                    this.overBrighten = value;
                }
            }

            public int TextYOffset
            {
                get
                {
                    return this.textYOffset;
                }
                set
                {
                    this.textYOffset = value;
                }
            }

            public bool UseTextSize
            {
                get
                {
                    return this.useTextSize;
                }
                set
                {
                    this.useTextSize = value;
                }
            }
        }

        public class CSDCheckBox : CustomSelfDrawPanel.CSDImage
        {
            private bool boxChecked;
            private CSD_CheckChangedDelegate checkChangedDelegate;
            private Image checkedImage;
            private Image checkedOverImage;
            private CustomSelfDrawPanel.CSDArea clickArea;
            private bool over;
            private CustomSelfDrawPanel.CSDLabel textLabel;
            private Image uncheckedImage;
            private Image uncheckedOverImage;

            public CSDCheckBox()
            {
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggled), "Generic_check_box_toggled");
                base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.enterCB), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.leaveCB));
            }

            public override void addedToParent()
            {
                this.initClickArea();
            }

            private void enterCB()
            {
                this.over = true;
                this.updateImage();
            }

            public void initClickArea()
            {
                if (((this.CBLabel != null) && (this.clickArea == null)) && (base.csd != null))
                {
                    Rectangle rectangle = new Rectangle {
                        X = base.Image.Width,
                        Y = this.CBLabel.Y
                    };
                    Size textSize = this.CBLabel.TextSize;
                    rectangle.Width = this.CBLabel.Position.X + textSize.Width;
                    rectangle.Height = textSize.Height;
                    this.clickArea = new CustomSelfDrawPanel.CSDArea();
                    this.clickArea.Position = rectangle.Location;
                    this.clickArea.Size = rectangle.Size;
                    this.clickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggled), "Generic_check_box_toggled");
                    base.addControl(this.clickArea);
                }
            }

            public bool isMouseOver()
            {
                return this.over;
            }

            private void leaveCB()
            {
                this.over = false;
                this.updateImage();
            }

            public override void onClear()
            {
                this.textLabel = null;
            }

            public void setCheckChangedDelegate(CSD_CheckChangedDelegate newDelegate)
            {
                this.checkChangedDelegate = newDelegate;
                this.initClickArea();
            }

            private void toggled()
            {
                this.Checked = !this.boxChecked;
                if (this.checkChangedDelegate != null)
                {
                    base.csd.ClickedControl = this;
                    this.checkChangedDelegate();
                }
            }

            private void updateImage()
            {
                if (this.boxChecked)
                {
                    if (!this.over || (this.checkedOverImage == null))
                    {
                        if (base.Image != this.checkedImage)
                        {
                            base.Image = this.checkedImage;
                        }
                    }
                    else if (base.Image != this.checkedOverImage)
                    {
                        base.Image = this.checkedOverImage;
                    }
                }
                else if (!this.over || (this.uncheckedOverImage == null))
                {
                    if (base.Image != this.uncheckedImage)
                    {
                        base.Image = this.uncheckedImage;
                    }
                }
                else if (base.Image != this.uncheckedOverImage)
                {
                    base.Image = this.uncheckedOverImage;
                }
                base.invalidate();
            }

            public CustomSelfDrawPanel.CSDLabel CBLabel
            {
                get
                {
                    if (this.textLabel == null)
                    {
                        this.textLabel = new CustomSelfDrawPanel.CSDLabel();
                        this.textLabel.CustomTooltipID = base.CustomTooltipID;
                        this.textLabel.CustomTooltipData = base.CustomTooltipData;
                        base.addControl(this.textLabel);
                    }
                    return this.textLabel;
                }
            }

            public bool Checked
            {
                get
                {
                    return this.boxChecked;
                }
                set
                {
                    this.boxChecked = value;
                    this.updateImage();
                }
            }

            public Image CheckedImage
            {
                set
                {
                    this.checkedImage = value;
                }
            }

            public Image CheckedOverImage
            {
                set
                {
                    this.checkedOverImage = value;
                }
            }

            public Image UncheckedImage
            {
                set
                {
                    this.uncheckedImage = value;
                    base.Image = value;
                }
            }

            public Image UncheckedOverImage
            {
                set
                {
                    this.uncheckedOverImage = value;
                }
            }

            public delegate void CSD_CheckChangedDelegate();
        }

        public class CSDColorBar : CustomSelfDrawPanel.CSDControl
        {
            private int bottomMargin;
            private Image[] images = new Image[8];
            private int leftMargin;
            private bool markerShown;
            private double markerValue;
            private double maxNumber = 1.0;
            private double number;
            private int rightMargin;
            private int topMargin;

            public void clearMarker()
            {
                this.markerShown = false;
                base.invalidate();
            }

            public override void draw(Point parentLocation)
            {
                Rectangle dest = base.Rectangle;
                dest.X += parentLocation.X;
                dest.Y += parentLocation.Y;
                int index = 0;
                if (this.number < 0.0)
                {
                    index = 4;
                }
                Rectangle source = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
                base.csd.drawImage(this.images[index], source, dest);
                dest.X += this.leftMargin;
                dest.Y += this.topMargin;
                dest.Width -= this.leftMargin + this.rightMargin;
                dest.Height -= this.topMargin + this.bottomMargin;
                if (this.number != 0.0)
                {
                    double maxNumber = Math.Abs(this.number);
                    if (maxNumber > this.maxNumber)
                    {
                        maxNumber = this.maxNumber;
                    }
                    double num3 = ((dest.Width - this.images[index + 1].Size.Width) - this.images[index + 3].Size.Width) - 1.0;
                    double num4 = (num3 / this.maxNumber) * maxNumber;
                    int num5 = ((int) num4) - 1;
                    dest.X++;
                    dest.Y++;
                    base.csd.drawImage(this.images[index + 1], dest.Location);
                    Point location = dest.Location;
                    location.X += this.images[index + 1].Size.Width;
                    if (num5 > 0)
                    {
                        for (int i = 0; i < num5; i++)
                        {
                            Point point2 = location;
                            point2.X += i;
                            base.csd.drawImage(this.images[index + 2], point2);
                        }
                    }
                    location.X += num5;
                    base.csd.drawImage(this.images[index + 3], location);
                    dest.X--;
                    dest.Y--;
                }
                if (this.markerShown)
                {
                    double num7 = Math.Abs(this.markerValue);
                    if (num7 > this.maxNumber)
                    {
                        num7 = this.maxNumber;
                    }
                    double num8 = ((this.Size.Width - this.images[index + 1].Size.Width) - this.images[index + 3].Size.Width) - 1.0;
                    double num9 = (num8 / this.maxNumber) * num7;
                    base.csd.drawLine(ARGBColors.Black, new Point((dest.X + 1) + ((int) num9), dest.Y), new Point((dest.X + 1) + ((int) num9), (dest.Y + dest.Height) - 2));
                }
            }

            public void setImages(Image positiveBack, Image positiveLeft, Image positiveMid, Image positiveRight, Image negativeBack, Image negativeLeft, Image negativeMid, Image negativeRight)
            {
                base.invalidate();
                this.images[0] = positiveBack;
                this.images[1] = positiveLeft;
                this.images[2] = positiveMid;
                this.images[3] = positiveRight;
                this.images[4] = negativeBack;
                this.images[5] = negativeLeft;
                this.images[6] = negativeMid;
                this.images[7] = negativeRight;
                this.Size = this.images[0].Size;
            }

            public void SetMargin(int lm, int tm, int rm, int bm)
            {
                this.leftMargin = lm;
                this.rightMargin = rm;
                this.topMargin = tm;
                this.bottomMargin = bm;
            }

            public void setMarker(double marker)
            {
                this.markerValue = marker;
                this.markerShown = true;
                base.invalidate();
            }

            public double MaxValue
            {
                get
                {
                    return this.maxNumber;
                }
                set
                {
                    if (this.maxNumber != value)
                    {
                        base.invalidate();
                    }
                    this.maxNumber = value;
                }
            }

            public double Number
            {
                get
                {
                    return this.number;
                }
                set
                {
                    if (this.number != value)
                    {
                        base.invalidate();
                    }
                    this.number = value;
                }
            }
        }

        public class CSDControl
        {
            private System.Drawing.Rectangle clickArea = System.Drawing.Rectangle.Empty;
            private CSD_ClickDelegate clickDelegate;
            private System.Drawing.Rectangle clipRect = System.Drawing.Rectangle.Empty;
            private bool clipVisible;
            public List<CustomSelfDrawPanel.CSDControl> csdControls = new List<CustomSelfDrawPanel.CSDControl>();
            private int customTooltipData;
            private int customTooltipID;
            private bool customTooltipWasOver;
            protected int dataValue;
            protected long dataValueL;
            private bool enabled = true;
            private Point lastRelativeMousePos = new Point();
            protected CustomSelfDrawPanel m_csd;
            protected CSD_MouseDownDelegate mouseDownDelegate;
            protected bool mouseDownFlag;
            protected CSD_MouseOverDelegate mouseLeaveDelegate;
            protected CSD_MouseOverDelegate mouseOverDelegate;
            protected bool mouseOverFlag;
            protected CSD_MouseDownDelegate mouseUpDelegate;
            protected CSD_MouseWheelDelegate mouseWheelDelegate;
            protected CustomSelfDrawPanel.CSDControl parent;
            private Point position = new Point(0, 0);
            private System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 1, 1);
            private CSD_ClickDelegate rightClickDelegate;
            private double scale = 1.0;
            private System.Drawing.Size size = new System.Drawing.Size(1, 1);
            private string soundTag = "";
            protected object tag;
            private int tooltip;
            private bool tooltipActive;
            private bool visible = true;

            public void addControl(CustomSelfDrawPanel.CSDControl control)
            {
                if ((control.parent != null) && control.parent.csdControls.Contains(control))
                {
                    control.parent.removeControl(control);
                }
                control.m_csd = this.csd;
                control.parent = this;
                this.csdControls.Add(control);
                control.addedToParent();
            }

            public virtual void addedToParent()
            {
            }

            public virtual void clearControls()
            {
                for (int i = 0; i < this.csdControls.Count; i++)
                {
                    this.csdControls[i].clearControls();
                    this.csdControls[i] = null;
                }
                this.csdControls.Clear();
                this.onClear();
            }

            public void clearDirectControlsOnly()
            {
                this.csdControls.Clear();
            }

            public virtual void draw(Point parentLocation)
            {
            }

            public void drawControls(Point parentLocation)
            {
                if (this.Visible)
                {
                    Point point2;
                    if (this.clipVisible)
                    {
                        System.Drawing.Rectangle a = this.csd.getCurrentClip();
                        if (a != System.Drawing.Rectangle.Empty)
                        {
                            Point point;
                            if (this.Scale == 1.0)
                            {
                                point = new Point(parentLocation.X + this.X, parentLocation.Y + this.Y);
                            }
                            else
                            {
                                point = new Point(parentLocation.X + ((int) (this.X * this.scale)), parentLocation.Y + ((int) (this.Y * this.scale)));
                            }
                            if (System.Drawing.Rectangle.Intersect(a, new System.Drawing.Rectangle(point, this.Size)) == System.Drawing.Rectangle.Empty)
                            {
                                return;
                            }
                            if (System.Drawing.Rectangle.Intersect(CustomSelfDrawPanel.screenClipRect, new System.Drawing.Rectangle(point, this.Size)) == System.Drawing.Rectangle.Empty)
                            {
                                return;
                            }
                        }
                    }
                    if (this.clipRect != System.Drawing.Rectangle.Empty)
                    {
                        System.Drawing.Rectangle clipRect = new System.Drawing.Rectangle((this.clipRect.X + parentLocation.X) + this.X, (this.clipRect.Y + parentLocation.Y) + this.Y, this.clipRect.Width, this.clipRect.Height);
                        this.csd.setClipRegion(clipRect);
                    }
                    this.draw(parentLocation);
                    if (this.Scale == 1.0)
                    {
                        point2 = new Point(parentLocation.X + this.X, parentLocation.Y + this.Y);
                    }
                    else
                    {
                        point2 = new Point(parentLocation.X + ((int) (this.X * this.scale)), parentLocation.Y + ((int) (this.Y * this.scale)));
                    }
                    foreach (CustomSelfDrawPanel.CSDControl control in this.csdControls)
                    {
                        control.drawControls(point2);
                    }
                    if (this.clipRect != System.Drawing.Rectangle.Empty)
                    {
                        this.csd.restoreClipRegion();
                    }
                }
            }

            public Point getPanelPosition()
            {
                Point point = new Point(this.X, this.Y);
                for (CustomSelfDrawPanel.CSDControl control = this.parent; control != null; control = control.parent)
                {
                    point.X += control.X;
                    point.Y += control.Y;
                }
                return point;
            }

            public bool getToolTip(Point mousePos, ref int data)
            {
                bool flag = false;
                if (this.Visible && this.Enabled)
                {
                    mousePos = new Point((int) (((double) mousePos.X) / this.Scale), (int) (((double) mousePos.Y) / this.Scale));
                    Point pt = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
                    if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(pt))
                    {
                        return false;
                    }
                    foreach (CustomSelfDrawPanel.CSDControl control in this.csdControls)
                    {
                        if (control.getToolTip(pt, ref data))
                        {
                            flag = true;
                        }
                    }
                    if (this.tooltipActive && this.mouseOver(mousePos))
                    {
                        flag = true;
                        data = this.tooltip;
                    }
                }
                return flag;
            }

            public void handleMouseLeave(CustomSelfDrawPanel.CSDControl overControl)
            {
                foreach (CustomSelfDrawPanel.CSDControl control in this.csdControls)
                {
                    control.handleMouseLeave(overControl);
                }
                if (this.mouseOverFlag && (this != overControl))
                {
                    this.mouseOverFlag = false;
                    if (this.mouseDownFlag)
                    {
                        this.mouseDownFlag = false;
                        if (this.mouseUpDelegate != null)
                        {
                            this.mouseUpDelegate();
                        }
                    }
                    if (this.mouseLeaveDelegate != null)
                    {
                        this.mouseLeaveDelegate();
                    }
                    this.csd.OverControl = null;
                }
            }

            public void invalidate()
            {
                if (this.csd != null)
                {
                    Point point = new Point(this.X, this.Y);
                    for (CustomSelfDrawPanel.CSDControl control = this.parent; control != null; control = control.parent)
                    {
                        point.X += control.X;
                        point.Y += control.Y;
                    }
                    System.Drawing.Rectangle rc = new System.Drawing.Rectangle(point.X, point.Y, this.Width, this.Height);
                    if (!this.csd.inNormalDraw)
                    {
                        this.csd.Invalidate(rc);
                    }
                    else
                    {
                        this.csd.InvalidateCached(rc);
                    }
                }
            }

            public void invalidateXtra()
            {
                if (this.csd != null)
                {
                    Point point = new Point(this.X, this.Y);
                    for (CustomSelfDrawPanel.CSDControl control = this.parent; control != null; control = control.parent)
                    {
                        point.X += control.X;
                        point.Y += control.Y;
                    }
                    System.Drawing.Rectangle rc = new System.Drawing.Rectangle(point.X - 10, point.Y - 10, this.Width + 20, this.Height + 20);
                    this.csd.Invalidate(rc);
                }
            }

            public virtual void mouseEventTrapped()
            {
            }

            public bool mouseOver(Point mousePos)
            {
                System.Drawing.Rectangle rect = this.rect;
                if (this.Scale != 1.0)
                {
                    rect.Width = (int) (rect.Width * this.Scale);
                    rect.Height = (int) (rect.Height * this.Scale);
                }
                if (!this.clickArea.IsEmpty)
                {
                    rect.Width = this.clickArea.Width;
                    rect.Height = this.clickArea.Height;
                    rect.X += this.clickArea.X;
                    rect.Y += this.clickArea.Y;
                }
                return rect.Contains(mousePos);
            }

            public virtual void onClear()
            {
            }

            public bool parentClicked(Point mousePos)
            {
                if (this.Visible && this.Enabled)
                {
                    Point pt = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
                    if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(pt))
                    {
                        return false;
                    }
                    for (int i = this.csdControls.Count - 1; i >= 0; i--)
                    {
                        if (this.csdControls[i].parentClicked(pt))
                        {
                            return true;
                        }
                    }
                    if ((this.clickDelegate != null) && this.mouseOver(mousePos))
                    {
                        this.csd.ClickedControl = this;
                        CustomSelfDrawPanel.StaticClickedControl = this;
                        this.lastRelativeMousePos = mousePos;
                        if (this.sTag.Length > 0)
                        {
                            GameEngine.Instance.playInterfaceSound(this.sTag);
                        }
                        this.clickDelegate();
                        return true;
                    }
                }
                return false;
            }

            public CustomSelfDrawPanel.CSDControl parentMouseDown(Point mousePos)
            {
                CustomSelfDrawPanel.CSDControl control = null;
                if (!this.Visible || !this.Enabled)
                {
                    return control;
                }
                Point pt = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
                if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(pt))
                {
                    return null;
                }
                for (int i = this.csdControls.Count - 1; i >= 0; i--)
                {
                    control = this.csdControls[i].parentMouseDown(pt);
                    if (control != null)
                    {
                        return control;
                    }
                }
                if ((this.mouseDownDelegate == null) || !this.mouseOver(mousePos))
                {
                    return control;
                }
                if (!this.mouseDownFlag)
                {
                    this.mouseDownFlag = true;
                    this.lastRelativeMousePos = mousePos;
                    this.mouseDownDelegate();
                }
                return this;
            }

            public CustomSelfDrawPanel.CSDControl parentMouseOver(Point mousePos)
            {
                CustomSelfDrawPanel.CSDControl control = null;
                if (!this.Visible || !this.Enabled)
                {
                    return control;
                }
                Point pt = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
                if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(pt))
                {
                    return null;
                }
                for (int i = this.csdControls.Count - 1; i >= 0; i--)
                {
                    control = this.csdControls[i].parentMouseOver(pt);
                    if (control != null)
                    {
                        return control;
                    }
                }
                if (this.customTooltipID != 0)
                {
                    if (this.mouseOver(mousePos))
                    {
                        if (!this.csd.tooltipSet)
                        {
                            this.csd.tooltipSet = true;
                            CustomTooltipManager.MouseEnterTooltipArea(this.customTooltipID, this.customTooltipData, this.csd.FindForm());
                            this.customTooltipWasOver = true;
                        }
                    }
                    else if (this.customTooltipWasOver)
                    {
                        this.customTooltipWasOver = false;
                        CustomTooltipManager.MouseLeaveTooltipArea();
                    }
                }
                if ((this.mouseOverDelegate == null) || !this.mouseOver(mousePos))
                {
                    return control;
                }
                if (this.csd.OverControl != this)
                {
                    this.csd.handleMouseLeave(this);
                    this.csd.OverControl = this;
                }
                if (!this.mouseOverFlag)
                {
                    this.mouseOverFlag = true;
                    this.lastRelativeMousePos = mousePos;
                    this.mouseOverDelegate();
                }
                return this;
            }

            public CustomSelfDrawPanel.CSDControl parentMouseUp(Point mousePos)
            {
                CustomSelfDrawPanel.CSDControl control = null;
                if (!this.Visible)
                {
                    return control;
                }
                Point pt = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
                if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(pt))
                {
                    return null;
                }
                for (int i = this.csdControls.Count - 1; i >= 0; i--)
                {
                    control = this.csdControls[i].parentMouseUp(pt);
                    if (control != null)
                    {
                        return control;
                    }
                }
                if ((this.mouseUpDelegate == null) || !this.mouseOver(mousePos))
                {
                    return control;
                }
                if (this.mouseDownFlag)
                {
                    this.mouseDownFlag = false;
                    this.lastRelativeMousePos = mousePos;
                    this.mouseUpDelegate();
                }
                return this;
            }

            public CustomSelfDrawPanel.CSDControl parentMouseWheel(Point mousePos, int delta)
            {
                CustomSelfDrawPanel.CSDControl control = null;
                if (this.Visible && this.Enabled)
                {
                    Point pt = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
                    if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(pt))
                    {
                        return null;
                    }
                    for (int i = this.csdControls.Count - 1; i >= 0; i--)
                    {
                        control = this.csdControls[i].parentMouseWheel(pt, delta);
                        if (control != null)
                        {
                            return control;
                        }
                    }
                    if ((this.mouseWheelDelegate != null) && this.mouseOver(mousePos))
                    {
                        this.lastRelativeMousePos = mousePos;
                        this.mouseWheelDelegate(delta);
                        return this;
                    }
                }
                return control;
            }

            public bool parentRightClicked(Point mousePos)
            {
                if (this.Visible && this.Enabled)
                {
                    Point pt = new Point(mousePos.X - this.X, mousePos.Y - this.Y);
                    if (!this.ClipRect.IsEmpty && !this.ClipRect.Contains(pt))
                    {
                        return false;
                    }
                    for (int i = this.csdControls.Count - 1; i >= 0; i--)
                    {
                        if (this.csdControls[i].parentRightClicked(pt))
                        {
                            return true;
                        }
                    }
                    if ((this.rightClickDelegate != null) && this.mouseOver(mousePos))
                    {
                        this.csd.ClickedControl = this;
                        this.lastRelativeMousePos = mousePos;
                        if (this.sTag.Length > 0)
                        {
                            GameEngine.Instance.playInterfaceSound(this.sTag);
                        }
                        this.rightClickDelegate();
                        return true;
                    }
                }
                return false;
            }

            public void removeControl(CustomSelfDrawPanel.CSDControl control)
            {
                if (control != null)
                {
                    this.csdControls.Remove(control);
                    control.parent = null;
                }
            }

            public void setChildrensScale(double scale)
            {
                foreach (CustomSelfDrawPanel.CSDControl control in this.csdControls)
                {
                    control.setScale(scale);
                }
            }

            public void setClickDelegate(CSD_ClickDelegate newDelegate)
            {
                this.clickDelegate = newDelegate;
            }

            public void setClickDelegate(CSD_ClickDelegate newDelegate, string tag)
            {
                this.clickDelegate = newDelegate;
                this.sTag = tag;
            }

            public void setCustomSelfDrawPanel(CustomSelfDrawPanel newCSD)
            {
                this.m_csd = newCSD;
            }

            public void setMouseDownDelegate(CSD_MouseDownDelegate downDelegate, CSD_MouseDownDelegate upDelegate)
            {
                this.mouseDownDelegate = downDelegate;
                this.mouseUpDelegate = upDelegate;
            }

            public void setMouseOverDelegate(CSD_MouseOverDelegate overDelegate, CSD_MouseOverDelegate leaveDelegate)
            {
                this.mouseOverDelegate = overDelegate;
                this.mouseLeaveDelegate = leaveDelegate;
            }

            public void setMouseWheelDelegate(CSD_MouseWheelDelegate newDelegate)
            {
                this.mouseWheelDelegate = newDelegate;
            }

            public void setParent(CustomSelfDrawPanel.CSDControl newParent)
            {
                this.parent = newParent;
            }

            public void setRightClickDelegate(CSD_ClickDelegate newDelegate)
            {
                this.rightClickDelegate = newDelegate;
            }

            public void setScale(double scale)
            {
                this.Scale = scale;
                foreach (CustomSelfDrawPanel.CSDControl control in this.csdControls)
                {
                    control.setScale(scale);
                }
            }

            public virtual void unityOverridableUpdate(Point parentLocation)
            {
            }

            public System.Drawing.Rectangle ClickArea
            {
                get
                {
                    return this.clickArea;
                }
                set
                {
                    this.clickArea = value;
                }
            }

            public System.Drawing.Rectangle ClipRect
            {
                get
                {
                    return this.clipRect;
                }
                set
                {
                    this.clipRect = value;
                }
            }

            public virtual bool ClipVisible
            {
                get
                {
                    return this.clipVisible;
                }
                set
                {
                    this.clipVisible = value;
                }
            }

            public List<CustomSelfDrawPanel.CSDControl> Controls
            {
                get
                {
                    return this.csdControls;
                }
            }

            public CustomSelfDrawPanel csd
            {
                get
                {
                    if (this.m_csd != null)
                    {
                        return this.m_csd;
                    }
                    if (this.parent != null)
                    {
                        this.m_csd = this.parent.csd;
                        return this.m_csd;
                    }
                    return null;
                }
            }

            public int CustomTooltipData
            {
                get
                {
                    return this.customTooltipData;
                }
                set
                {
                    this.customTooltipData = value;
                }
            }

            public int CustomTooltipID
            {
                get
                {
                    return this.customTooltipID;
                }
                set
                {
                    this.customTooltipID = value;
                }
            }

            public int Data
            {
                get
                {
                    return this.dataValue;
                }
                set
                {
                    this.dataValue = value;
                }
            }

            public long DataL
            {
                get
                {
                    return this.dataValueL;
                }
                set
                {
                    this.dataValueL = value;
                }
            }

            public virtual bool Enabled
            {
                get
                {
                    return this.enabled;
                }
                set
                {
                    if (this.enabled != value)
                    {
                        this.invalidate();
                    }
                    this.enabled = value;
                }
            }

            public int Height
            {
                get
                {
                    return this.size.Height;
                }
                set
                {
                    this.size.Height = value;
                    this.rect.Height = value;
                }
            }

            public Point LastRelativeMousePos
            {
                get
                {
                    return this.lastRelativeMousePos;
                }
            }

            public bool MouseDownFlag
            {
                get
                {
                    return this.mouseDownFlag;
                }
                set
                {
                    this.mouseDownFlag = value;
                }
            }

            public CustomSelfDrawPanel.CSDControl Parent
            {
                get
                {
                    return this.parent;
                }
            }

            public virtual Point Position
            {
                get
                {
                    return this.position;
                }
                set
                {
                    this.position = value;
                    this.rect.Location = value;
                }
            }

            public System.Drawing.Rectangle Rectangle
            {
                get
                {
                    return this.rect;
                }
            }

            public double Scale
            {
                get
                {
                    return this.scale;
                }
                set
                {
                    this.scale = value;
                }
            }

            public virtual System.Drawing.Size Size
            {
                get
                {
                    return this.size;
                }
                set
                {
                    this.size = value;
                    this.rect.Size = value;
                }
            }

            public string sTag
            {
                get
                {
                    return this.soundTag;
                }
                set
                {
                    this.soundTag = value;
                }
            }

            public object Tag
            {
                get
                {
                    return this.tag;
                }
                set
                {
                    this.tag = value;
                }
            }

            public int Tooltip
            {
                get
                {
                    return this.tooltip;
                }
                set
                {
                    this.tooltip = value;
                    this.tooltipActive = true;
                }
            }

            public bool Visible
            {
                get
                {
                    return this.visible;
                }
                set
                {
                    if (this.visible != value)
                    {
                        this.invalidate();
                    }
                    this.visible = value;
                }
            }

            public int Width
            {
                get
                {
                    return this.size.Width;
                }
                set
                {
                    this.size.Width = value;
                    this.rect.Width = value;
                }
            }

            public int X
            {
                get
                {
                    return this.position.X;
                }
                set
                {
                    this.position.X = value;
                    this.rect.X = value;
                }
            }

            public int Y
            {
                get
                {
                    return this.position.Y;
                }
                set
                {
                    this.position.Y = value;
                    this.rect.Y = value;
                }
            }

            public delegate void CSD_ClickDelegate();

            public delegate void CSD_MouseDownDelegate();

            public delegate void CSD_MouseOverDelegate();

            public delegate void CSD_MouseWheelDelegate(int delta);

            public delegate void CSD_ScrollBarChangedDelegate();

            public delegate void CSD_ValueChangedDelegate();
        }

        public class CSDDragPanel : CustomSelfDrawPanel.CSDControl
        {
            private bool held;
            private Point heldPosition = new Point(0, 0);
            protected CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate valueChangedDelegate;
            private int xDiff;
            private int yDiff;

            public CSDDragPanel()
            {
                base.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
            }

            private void buttonDown()
            {
                if (!this.held)
                {
                    this.heldPosition = base.csd.LastMousePosition;
                    this.held = true;
                    this.yDiff = 0;
                }
                else
                {
                    this.xDiff = base.csd.LastMousePosition.X - this.heldPosition.X;
                    this.yDiff = base.csd.LastMousePosition.Y - this.heldPosition.Y;
                }
                if ((this.valueChangedDelegate != null) && ((this.xDiff != 0) || (this.yDiff != 0)))
                {
                    this.valueChangedDelegate();
                }
                this.heldPosition = new Point(base.csd.LastMousePosition.X, base.csd.LastMousePosition.Y);
                base.csd.addTrapMouseEvent(this);
            }

            private void buttonUp()
            {
                if (!base.csd.MouseReallyPressed)
                {
                    this.held = false;
                    this.yDiff = 0;
                    this.xDiff = 0;
                    if (this.valueChangedDelegate != null)
                    {
                        this.valueChangedDelegate();
                    }
                    base.csd.removeTrapMouseEvent(this);
                }
            }

            public override void mouseEventTrapped()
            {
                if (this.held)
                {
                    if (!base.csd.MouseReallyPressed)
                    {
                        base.mouseDownFlag = false;
                        this.buttonUp();
                    }
                    else
                    {
                        this.xDiff = base.csd.LastMousePosition.X - this.heldPosition.X;
                        this.yDiff = base.csd.LastMousePosition.Y - this.heldPosition.Y;
                        if ((this.valueChangedDelegate != null) && ((this.xDiff != 0) || (this.yDiff != 0)))
                        {
                            this.valueChangedDelegate();
                        }
                        this.heldPosition = new Point(base.csd.LastMousePosition.X, base.csd.LastMousePosition.Y);
                    }
                }
            }

            public void setValueChangeDelegate(CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate newDelegate)
            {
                this.valueChangedDelegate = newDelegate;
            }

            public int XDiff
            {
                get
                {
                    return this.xDiff;
                }
            }

            public int YDiff
            {
                get
                {
                    return this.yDiff;
                }
            }
        }

        public class CSDExtendingPanel : CustomSelfDrawPanel.CSDControl
        {
            private float alpha = 1f;
            public CustomSelfDrawPanel.CSDImage BottomLeft = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage BottomMid = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage BottomRight = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage Left = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage Right = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage TopLeft = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage TopMid = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage TopRight = new CustomSelfDrawPanel.CSDImage();

            public void Create(Image topLeftImage, Image topMidImage, Image topRightImage, Image leftImage, Image midImage, Image rightImage, Image bottomLeftImage, Image bottomMidImage, Image bottomRightImage)
            {
                this.TopLeft.Image = topLeftImage;
                this.TopLeft.Position = new Point(0, 0);
                this.TopLeft.Alpha = this.alpha;
                base.addControl(this.TopLeft);
                this.TopRight.Image = topRightImage;
                this.TopRight.Position = new Point(base.Width - this.TopRight.Image.Width, 0);
                this.TopRight.Alpha = this.alpha;
                base.addControl(this.TopRight);
                this.TopMid.Image = topMidImage;
                this.TopMid.Position = new Point(this.TopLeft.Image.Width, 0);
                this.TopMid.Size = new Size((base.Width - this.TopLeft.Image.Width) - this.TopRight.Image.Width, this.TopMid.Image.Height);
                this.TopMid.Alpha = this.alpha;
                base.addControl(this.TopMid);
                this.Left.Image = leftImage;
                this.Left.Position = new Point(0, this.TopLeft.Image.Height);
                this.Left.Size = new Size(this.Left.Image.Width, (base.Height - this.TopLeft.Image.Height) - bottomLeftImage.Width);
                this.Left.Alpha = this.alpha;
                base.addControl(this.Left);
                this.Right.Image = rightImage;
                this.Right.Position = new Point(base.Width - this.Right.Image.Width, this.TopRight.Image.Height);
                this.Right.Size = new Size(this.Right.Image.Width, (base.Height - this.TopRight.Image.Height) - bottomRightImage.Width);
                this.Right.Alpha = this.alpha;
                base.addControl(this.Right);
                this.BottomLeft.Image = bottomLeftImage;
                this.BottomLeft.Position = new Point(0, base.Height - this.BottomLeft.Height);
                this.BottomLeft.Alpha = this.alpha;
                base.addControl(this.BottomLeft);
                this.BottomRight.Image = bottomRightImage;
                this.BottomRight.Position = new Point(base.Width - this.BottomRight.Image.Width, base.Height - this.BottomRight.Height);
                this.BottomRight.Alpha = this.alpha;
                base.addControl(this.BottomRight);
                this.BottomMid.Image = bottomMidImage;
                this.BottomMid.Position = new Point(this.BottomLeft.Image.Width, base.Height - this.BottomMid.Image.Height);
                this.BottomMid.Size = new Size((base.Width - this.BottomLeft.Image.Width) - this.BottomRight.Image.Width, this.BottomMid.Image.Height);
                this.BottomMid.Alpha = this.alpha;
                base.addControl(this.BottomMid);
                if (midImage != null)
                {
                    this.Mid.Image = midImage;
                    this.Mid.Position = new Point(this.Left.Image.Width, this.TopMid.Image.Height);
                    this.Mid.Size = new Size((base.Width - this.Left.Image.Width) - this.Right.Image.Width, (base.Height - this.TopMid.Image.Height) - this.BottomMid.Image.Height);
                    this.Mid.Alpha = this.alpha;
                    base.addControl(this.Mid);
                }
            }

            public void ForceTiling()
            {
                this.TopMid.Tile = true;
                this.Left.Tile = true;
                this.BottomMid.Tile = true;
                this.Right.Tile = true;
                this.Mid.Tile = true;
            }

            public float Alpha
            {
                get
                {
                    return this.alpha;
                }
                set
                {
                    if (value > 1f)
                    {
                        this.alpha = 1f;
                    }
                    else
                    {
                        this.alpha = value;
                    }
                }
            }
        }

        public class CSDFactionFlagImage : CustomSelfDrawPanel.CSDImage
        {
            private ColorMap[] colourMap;
            private CustomSelfDrawPanel.CSDImage flagOverlayImage;

            public void createFromFlagData(int flagData)
            {
                int flag = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                FactionData.getFlagData(flagData, ref flag, ref num2, ref num3, ref num4, ref num5);
                if ((flag >= 0) && (flag < GFXLibrary.factionFlags.Length))
                {
                    base.Image = (Image) GFXLibrary.factionFlags[flag];
                }
                else
                {
                    base.Image = (Image) GFXLibrary.factionFlags[0];
                }
                this.ColourMap = FactionData.getColourMap(num2, num3, num4, num5, 0xff);
            }

            public override void draw(Point parentLocation)
            {
                if (base.image != null)
                {
                    Rectangle dest = base.Rectangle;
                    dest.X += parentLocation.X;
                    dest.Y += parentLocation.Y;
                    Rectangle source = new Rectangle(0, 0, base.image.Width, base.image.Height);
                    if (base.Scale != 1.0)
                    {
                        dest.Width = (int) (dest.Width * base.Scale);
                        dest.Height = (int) (dest.Height * base.Scale);
                    }
                    if (this.colourMap != null)
                    {
                        base.csd.drawImageColourMap(base.image, source, dest, this.colourMap);
                    }
                    else
                    {
                        base.csd.drawImage(base.image, source, dest);
                    }
                    if (base.Scale == 1.0)
                    {
                        base.csd.drawImage((Image) GFXLibrary.faction_flag_outline_100, source, dest);
                    }
                    else if (base.Scale > 0.40000000596046448)
                    {
                        base.csd.drawImage((Image) GFXLibrary.faction_flag_outline_50, new Rectangle(0, 0, GFXLibrary.faction_flag_outline_50.Width, GFXLibrary.faction_flag_outline_50.Height), dest);
                    }
                    else
                    {
                        base.csd.drawImage((Image) GFXLibrary.faction_flag_outline_25, new Rectangle(0, 0, GFXLibrary.faction_flag_outline_25.Width, GFXLibrary.faction_flag_outline_25.Height), dest);
                    }
                }
            }

            public ColorMap[] ColourMap
            {
                get
                {
                    return this.colourMap;
                }
                set
                {
                    this.colourMap = value;
                }
            }
        }

        public class CSDFill : CustomSelfDrawPanel.CSDControl
        {
            private float alpha = 1f;
            private bool border;
            private Color fillColor;
            private bool specialGradient;

            public override void draw(Point parentLocation)
            {
                Rectangle rect = base.Rectangle;
                rect.X += parentLocation.X;
                rect.Y += parentLocation.Y;
                if (this.specialGradient)
                {
                    base.csd.drawSpecialGradient(rect);
                }
                else
                {
                    base.csd.fillRect(rect, this.fillColor);
                    if (this.border)
                    {
                        Color black = ARGBColors.Black;
                        base.csd.drawLine(black, new Point(rect.X, rect.Y), new Point((rect.X + this.Size.Width) - 1, rect.Y));
                        base.csd.drawLine(black, new Point(rect.X, rect.Y), new Point(rect.X, (rect.Y + this.Size.Height) - 2));
                        base.csd.drawLine(black, new Point((rect.X + this.Size.Width) - 1, (rect.Y + this.Size.Height) - 1), new Point((rect.X + this.Size.Width) - 1, rect.Y));
                        base.csd.drawLine(black, new Point((rect.X + this.Size.Width) - 1, (rect.Y + this.Size.Height) - 1), new Point(rect.X, (rect.Y + this.Size.Height) - 1));
                    }
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
                    if (value > 1f)
                    {
                        this.alpha = 1f;
                    }
                    else
                    {
                        this.alpha = value;
                    }
                }
            }

            public bool Border
            {
                get
                {
                    return this.border;
                }
                set
                {
                    if (this.border != value)
                    {
                        base.invalidate();
                    }
                    this.border = value;
                }
            }

            public Color FillColor
            {
                get
                {
                    return this.fillColor;
                }
                set
                {
                    if (this.fillColor != value)
                    {
                        base.invalidate();
                    }
                    this.fillColor = value;
                }
            }

            public bool SpecialGradient
            {
                get
                {
                    return this.specialGradient;
                }
                set
                {
                    if (this.specialGradient != value)
                    {
                        base.invalidate();
                    }
                    this.specialGradient = value;
                }
            }
        }

        public class CSDFloatingText : CustomSelfDrawPanel.CSDLabel
        {
            public Color BaseColor;
            public Color BaseDrop;
            public int currentAlpha;
            public int da;
            public int dx;
            public int dy;
            public double interval;
            public double last;
            public double lifespan;
            public bool live = true;
            public double start;

            public void init(Point pos, Size size, Color basecolor, Color dropcolor, int _dx, int _dy, int _da, string text, int fontsize, double _interval, double _lifespan, double _start, CustomSelfDrawPanel.CSDControl _parent)
            {
                this.Position = pos;
                this.Size = size;
                base.Text = text;
                base.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                base.Font = FontManager.GetFont("Arial", (float) fontsize, FontStyle.Bold);
                base.Color = basecolor;
                base.DropShadowColor = dropcolor;
                this.BaseColor = basecolor;
                this.BaseDrop = dropcolor;
                this.dx = _dx;
                this.dy = _dy;
                this.da = _da;
                this.interval = _interval;
                this.lifespan = _lifespan;
                this.start = _start;
                _parent.addControl(this);
                this.currentAlpha = 0xff;
            }

            public void move(double now)
            {
                if (this.live)
                {
                    if ((now - this.start) < this.lifespan)
                    {
                        if ((now - this.last) >= this.interval)
                        {
                            this.Position = new Point(this.Position.X + this.dx, this.Position.Y + this.dy);
                            this.currentAlpha += this.da;
                            if (this.currentAlpha < 0)
                            {
                                this.currentAlpha = 0;
                            }
                            else if (this.currentAlpha > 0xff)
                            {
                                this.currentAlpha = 0xff;
                            }
                            base.Color = Color.FromArgb(this.currentAlpha, this.BaseColor);
                            base.DropShadowColor = Color.FromArgb(this.currentAlpha, this.BaseDrop);
                            this.last = now;
                        }
                    }
                    else
                    {
                        if (base.parent != null)
                        {
                            base.parent.removeControl(this);
                        }
                        this.live = false;
                        if (base.parent != null)
                        {
                            base.parent.invalidate();
                        }
                    }
                }
            }
        }

        public class CSDHorzExtendingPanel : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDImage Left = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage Right = new CustomSelfDrawPanel.CSDImage();

            public void Create(Image leftImage, Image midImage, Image rightImage)
            {
                base.removeControl(this.Left);
                base.removeControl(this.Right);
                base.removeControl(this.Mid);
                this.Left.Image = leftImage;
                this.Left.Position = new Point(0, 0);
                base.addControl(this.Left);
                this.Right.Image = rightImage;
                this.Right.Position = new Point(base.Width - this.Right.Image.Width, 0);
                base.addControl(this.Right);
                this.Mid.Image = midImage;
                this.Mid.Position = new Point(this.Left.Image.Width, 0);
                this.Mid.Size = new Size((base.Width - this.Left.Image.Width) - this.Right.Image.Width, this.Mid.Image.Height);
                this.Mid.Tile = true;
                base.addControl(this.Mid);
                this.Size = new Size(this.Size.Width, this.Left.Image.Height);
            }

            public void CreateX(Image leftImage, Image midImage, Image rightImage, int midShorten, int rightExtra)
            {
                base.removeControl(this.Left);
                base.removeControl(this.Right);
                base.removeControl(this.Mid);
                this.Left.Image = leftImage;
                this.Left.Position = new Point(0, 0);
                base.addControl(this.Left);
                this.Right.Image = rightImage;
                this.Right.Position = new Point((base.Width - this.Right.Image.Width) + rightExtra, 0);
                this.Mid.Image = midImage;
                this.Mid.Position = new Point(this.Left.Image.Width, 0);
                this.Mid.Size = new Size(((base.Width - this.Left.Image.Width) - this.Right.Image.Width) - midShorten, this.Mid.Image.Height);
                this.Mid.Tile = true;
                this.Mid.ClipRect = new Rectangle(0, 0, this.Mid.Size.Width, this.Mid.Size.Height);
                base.addControl(this.Mid);
                base.addControl(this.Right);
                this.Size = new Size(this.Size.Width, this.Left.Image.Height);
            }

            public void forceDraw(Point parentLocation, float alpha)
            {
                Rectangle rectangle = base.Rectangle;
                rectangle.X += parentLocation.X;
                rectangle.Y += parentLocation.Y;
                Point point = new Point(rectangle.X, rectangle.Y);
                this.Left.Alpha = alpha;
                this.Left.draw(point);
                this.Mid.Alpha = alpha;
                this.Mid.draw(point);
                this.Right.Alpha = alpha;
                this.Right.draw(point);
            }

            public void resize()
            {
                this.Right.Position = new Point(base.Width - this.Right.Image.Width, 0);
                this.Mid.Position = new Point(this.Left.Image.Width, 0);
                this.Mid.Size = new Size((base.Width - this.Left.Image.Width) - this.Right.Image.Width, this.Mid.Image.Height);
            }
        }

        public class CSDHorzProgressBar : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDImage barLeft = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage barMid = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage barRight = new CustomSelfDrawPanel.CSDImage();
            private bool created;
            public CustomSelfDrawPanel.CSDImage Left = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
            private Point offset = new Point(0, 0);
            public CustomSelfDrawPanel.CSDImage Right = new CustomSelfDrawPanel.CSDImage();

            public void Create(Image leftImage, Image midImage, Image rightImage, Image innerLeftImage, Image innerMidImage, Image innerRightImage)
            {
                if (((leftImage != null) && (midImage != null)) && (rightImage != null))
                {
                    this.Left.Image = leftImage;
                    this.Left.Position = new Point(0, 0);
                    base.addControl(this.Left);
                    this.Right.Image = rightImage;
                    this.Right.Position = new Point(base.Width - this.Right.Image.Width, 0);
                    base.addControl(this.Right);
                    this.Mid.Image = midImage;
                    this.Mid.Position = new Point(this.Left.Image.Width, 0);
                    this.Mid.Size = new Size((base.Width - this.Left.Image.Width) - this.Right.Image.Width, this.Mid.Image.Height);
                    this.Mid.Tile = true;
                    base.addControl(this.Mid);
                    this.Size = new Size(this.Size.Width, this.Left.Image.Height);
                }
                else
                {
                    this.Size = new Size(this.Size.Width, innerLeftImage.Height);
                }
                this.barLeft.Image = innerLeftImage;
                this.barLeft.Position = this.offset;
                base.addControl(this.barLeft);
                this.barMid.Image = innerMidImage;
                this.barMid.Tile = true;
                base.addControl(this.barMid);
                this.barRight.Image = innerRightImage;
                base.addControl(this.barRight);
                this.created = true;
                this.setValues(1.0, 1.0);
            }

            public void setValues(double curValue, double maxValue)
            {
                if (this.created)
                {
                    if (maxValue <= 0.0)
                    {
                        this.barLeft.Visible = false;
                        this.barRight.Visible = false;
                        this.barMid.Visible = false;
                    }
                    else
                    {
                        this.barLeft.Visible = true;
                        this.barRight.Visible = true;
                        this.barMid.Visible = true;
                        if (curValue > maxValue)
                        {
                            curValue = maxValue;
                        }
                        double num = ((base.Width - (this.offset.X * 2)) - this.barLeft.Image.Width) - this.barRight.Image.Width;
                        double a = (num * curValue) / maxValue;
                        a = Math.Round(a);
                        this.barRight.Position = new Point((this.offset.X + ((int) a)) + this.barLeft.Width, this.offset.Y);
                        this.barMid.Position = new Point(this.offset.X + this.barLeft.Width, this.offset.Y);
                        this.barMid.Size = new Size((int) a, this.barLeft.Image.Height);
                        this.barMid.ClipRect = new Rectangle(new Point(0, 0), this.barMid.Size);
                    }
                }
            }

            public Point Offset
            {
                get
                {
                    return this.offset;
                }
                set
                {
                    this.offset = value;
                }
            }
        }

        public class CSDImage : CustomSelfDrawPanel.CSDControl
        {
            private float alpha = 1f;
            private Color colorise = ARGBColors.White;
            protected System.Drawing.Image image;
            private bool mirrorFlip;
            private float rotate;
            private PointF rotateCentre = ((PointF) new Point(-1000, -1000));
            private bool tile;

            public override void draw(Point parentLocation)
            {
                if (this.image != null)
                {
                    Rectangle dest = base.Rectangle;
                    if (base.Scale != 1.0)
                    {
                        dest.X = (int) (dest.X * base.Scale);
                        dest.Y = (int) (dest.Y * base.Scale);
                    }
                    dest.X += parentLocation.X;
                    dest.Y += parentLocation.Y;
                    Rectangle source = new Rectangle(0, 0, this.image.Width, this.image.Height);
                    if (!this.Tile)
                    {
                        if (base.Scale != 1.0)
                        {
                            if ((this.alpha == 1f) && (this.colorise == ARGBColors.White))
                            {
                                base.csd.drawImage(this.image, source, dest, base.Scale);
                            }
                            else if ((this.alpha > 0f) || (this.colorise != ARGBColors.White))
                            {
                                base.csd.drawImage(this.image, source, dest, this.alpha, base.Scale, this.colorise);
                            }
                        }
                        else if ((this.alpha == 1f) && (this.colorise == ARGBColors.White))
                        {
                            if ((source.Width == dest.Width) && (source.Height == dest.Height))
                            {
                                if (!this.mirrorFlip && (this.rotate == 0f))
                                {
                                    base.csd.drawImage(this.image, source, dest);
                                }
                                else
                                {
                                    base.csd.drawImageMirrorRotate(this.image, source, dest, this.mirrorFlip, this.rotate, this.rotateCentre);
                                }
                            }
                            else
                            {
                                float width = source.Width;
                                float height = source.Height;
                                if (source.Width != dest.Width)
                                {
                                    width -= 0.999f;
                                }
                                if (source.Height != dest.Height)
                                {
                                    height -= 0.999f;
                                }
                                RectangleF ef = new RectangleF((float) source.X, (float) source.Y, width, height);
                                RectangleF ef2 = new RectangleF((float) dest.X, (float) dest.Y, (float) dest.Width, (float) dest.Height);
                                base.csd.drawImage(this.image, ef, ef2);
                            }
                        }
                        else if ((this.alpha > 0f) || (this.colorise != ARGBColors.White))
                        {
                            if ((source.Width == dest.Width) && (source.Height == dest.Height))
                            {
                                if (!this.mirrorFlip && (this.rotate == 0f))
                                {
                                    base.csd.drawImage(this.image, source, dest, this.alpha, this.colorise);
                                }
                                else
                                {
                                    base.csd.drawImageMirrorRotateAlpha(this.image, source, dest, this.mirrorFlip, this.rotate, this.rotateCentre, this.alpha);
                                }
                            }
                            else
                            {
                                float num3 = source.Width;
                                float num4 = source.Height;
                                if (source.Width != dest.Width)
                                {
                                    num3 -= 0.999f;
                                }
                                if (source.Height != dest.Height)
                                {
                                    num4 -= 0.999f;
                                }
                                RectangleF ef3 = new RectangleF((float) source.X, (float) source.Y, num3, num4);
                                RectangleF ef4 = new RectangleF((float) dest.X, (float) dest.Y, (float) dest.Width, (float) dest.Height);
                                base.csd.drawImage(this.image, ef3, ef4, this.alpha, this.colorise);
                            }
                        }
                    }
                    else if (this.image.Size.Width == 1)
                    {
                        RectangleF ef5 = new RectangleF((float) source.X, (float) source.Y, 0.001f, (float) source.Height);
                        RectangleF ef6 = new RectangleF((float) dest.X, (float) dest.Y, (float) dest.Width, (float) dest.Height);
                        if ((this.alpha == 1f) && (this.colorise == ARGBColors.White))
                        {
                            base.csd.drawImage(this.image, ef5, ef6);
                        }
                        else
                        {
                            base.csd.drawImage(this.image, ef5, ef6, this.alpha, this.colorise);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < this.Size.Height; i += this.image.Size.Height)
                        {
                            for (int j = 0; j < this.Size.Width; j += this.image.Size.Width)
                            {
                                Rectangle rectangle3 = new Rectangle(dest.X + j, dest.Y + i, this.image.Width, this.image.Height);
                                if ((this.alpha == 1f) && (this.colorise == ARGBColors.White))
                                {
                                    base.csd.drawImage(this.image, source, rectangle3);
                                }
                                else if ((this.alpha > 0f) || (this.colorise != ARGBColors.White))
                                {
                                    base.csd.drawImage(this.image, source, rectangle3, this.alpha, this.colorise);
                                }
                            }
                        }
                    }
                }
            }

            public void setSizeToImage()
            {
                if (this.image != null)
                {
                    this.Size = this.image.Size;
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
                    this.alpha = value;
                }
            }

            public Color Colorise
            {
                get
                {
                    return this.colorise;
                }
                set
                {
                    if (this.colorise != value)
                    {
                        base.invalidate();
                    }
                    this.colorise = value;
                }
            }

            public System.Drawing.Image Image
            {
                get
                {
                    return this.image;
                }
                set
                {
                    if (this.image != value)
                    {
                        base.invalidate();
                    }
                    this.image = value;
                    this.setSizeToImage();
                }
            }

            public System.Drawing.Image ImageNoInvalidate
            {
                get
                {
                    return this.image;
                }
                set
                {
                    this.image = value;
                }
            }

            public System.Drawing.Image ImageNoSizing
            {
                get
                {
                    return this.image;
                }
                set
                {
                    if (this.image != value)
                    {
                        base.invalidate();
                    }
                    this.image = value;
                }
            }

            public bool MirrorFlip
            {
                get
                {
                    return this.mirrorFlip;
                }
                set
                {
                    this.mirrorFlip = value;
                }
            }

            public float Rotate
            {
                get
                {
                    return this.rotate;
                }
                set
                {
                    this.rotate = value;
                }
            }

            public PointF RotateCentre
            {
                get
                {
                    return this.rotateCentre;
                }
                set
                {
                    this.rotateCentre = value;
                }
            }

            public bool Tile
            {
                get
                {
                    return this.tile;
                }
                set
                {
                    this.tile = value;
                }
            }
        }

        public class CSDImageAnim : CustomSelfDrawPanel.CSDImage
        {
            public int CurrentFrame;
            private int CurrentStep;
            public int FirstFrame;
            public int[] FrameData;
            public BaseImage[] Frames;
            public double Interval;
            public bool Playing;
            public int Step = 1;

            public void Animate(double now)
            {
                this.Animate(now, -1);
            }

            public bool Animate(double now, int target)
            {
                if (this.Playing)
                {
                    this.CurrentFrame = (this.CurrentFrame + 1) % this.Frames.Length;
                    base.Image = (Image) this.Frames[this.CurrentFrame];
                    if (this.FrameData[this.CurrentFrame] == target)
                    {
                        this.Playing = false;
                    }
                }
                return this.Playing;
            }

            public void NoLoopAnim()
            {
                if (this.Playing)
                {
                    this.CurrentStep++;
                    if (this.CurrentStep >= this.Step)
                    {
                        this.CurrentFrame++;
                        this.CurrentStep = 0;
                        if (this.CurrentFrame >= this.Frames.Length)
                        {
                            this.Playing = false;
                            if (base.parent != null)
                            {
                                base.parent.removeControl(this);
                            }
                        }
                        else
                        {
                            base.ImageNoSizing = (Image) this.Frames[this.CurrentFrame];
                        }
                    }
                }
            }

            public void SetFrames(BaseImage[] frames)
            {
                this.Playing = false;
                this.Frames = frames;
                this.FrameData = new int[frames.Length];
                for (int i = 0; i < this.FrameData.Length; i++)
                {
                    this.FrameData[i] = 0;
                }
                this.CurrentFrame = 0;
                this.Interval = 33.0;
                this.CurrentStep = 0;
            }
        }

        public class CSDLabel : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSD_Text_Alignment alignment;
            private System.Drawing.Color color = ARGBColors.Black;
            private System.Drawing.Color defaultColor = ARGBColors.Black;
            private bool dropShadow;
            private System.Drawing.Color dropShadowColor = ARGBColors.Black;
            private System.Drawing.Font font = FontManager.GetFont("Arial", 8.25f);
            private System.Drawing.Color rolloverColor = ARGBColors.Black;
            private string text = "";

            public void clearDropShadow()
            {
                this.dropShadow = false;
            }

            private void colourRolloff()
            {
                this.Color = this.defaultColor;
            }

            private void colourRollover()
            {
                this.Color = this.rolloverColor;
            }

            public override void draw(Point parentLocation)
            {
                Rectangle displayRect = base.Rectangle;
                System.Drawing.Font font = this.font;
                if (base.Scale != 1.0)
                {
                    if ((this.font.SizeInPoints * ((float) base.Scale)) < 6f)
                    {
                        return;
                    }
                    displayRect.X = (int) (displayRect.X * base.Scale);
                    displayRect.Y = (int) (displayRect.Y * base.Scale);
                    displayRect.Width = (int) (displayRect.Width * base.Scale);
                    displayRect.Height = (int) (displayRect.Height * base.Scale);
                    font = new System.Drawing.Font(this.font.FontFamily, this.font.SizeInPoints * ((float) base.Scale), this.font.Style);
                }
                displayRect.X += parentLocation.X;
                displayRect.Y += parentLocation.Y;
                if (this.dropShadow)
                {
                    displayRect.X++;
                    displayRect.Y++;
                    base.csd.drawString(this.Text, displayRect, this.dropShadowColor, font, this.alignment);
                    displayRect.X--;
                    displayRect.Y--;
                }
                base.csd.drawString(this.Text, displayRect, this.color, font, this.alignment);
            }

            public CustomSelfDrawPanel.CSD_Text_Alignment Alignment
            {
                get
                {
                    return this.alignment;
                }
                set
                {
                    this.alignment = value;
                }
            }

            public System.Drawing.Color Color
            {
                get
                {
                    return this.color;
                }
                set
                {
                    this.color = value;
                    base.invalidate();
                }
            }

            public System.Drawing.Color DropShadowColor
            {
                get
                {
                    return this.dropShadowColor;
                }
                set
                {
                    this.dropShadowColor = value;
                    this.dropShadow = true;
                    base.invalidate();
                }
            }

            public System.Drawing.Font Font
            {
                get
                {
                    return this.font;
                }
                set
                {
                    this.font = value;
                }
            }

            public System.Drawing.Color RolloverColor
            {
                get
                {
                    return this.rolloverColor;
                }
                set
                {
                    this.rolloverColor = value;
                    base.invalidate();
                    base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.colourRollover), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.colourRolloff));
                    this.defaultColor = this.color;
                }
            }

            public string Text
            {
                get
                {
                    return this.text;
                }
                set
                {
                    this.text = value;
                    base.invalidate();
                }
            }

            public string TextDiffOnly
            {
                get
                {
                    return this.text;
                }
                set
                {
                    if (this.text != value)
                    {
                        this.text = value;
                        base.invalidate();
                    }
                }
            }

            public Size TextSize
            {
                get
                {
                    Size size = new Size();
                    Graphics graphics = base.csd.CreateGraphics();
                    size = graphics.MeasureString(this.text, this.Font, base.Width).ToSize();
                    graphics.Dispose();
                    return size;
                }
            }

            public Size TextSizeX
            {
                get
                {
                    Size size = new Size();
                    Graphics graphics = base.csd.CreateGraphics();
                    size = graphics.MeasureString(this.text, this.Font, base.Width).ToSize();
                    graphics.Dispose();
                    size.Width += 2;
                    size.Height += 2;
                    return size;
                }
            }
        }

        public class CSDLine : CustomSelfDrawPanel.CSDControl
        {
            private Color lineColor = ARGBColors.White;

            public override void draw(Point parentLocation)
            {
                Rectangle rectangle = base.Rectangle;
                rectangle.X += parentLocation.X;
                rectangle.Y += parentLocation.Y;
                base.csd.drawLine(this.lineColor, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X + this.Size.Width, rectangle.Y + this.Size.Height));
            }

            public Color LineColor
            {
                get
                {
                    return this.lineColor;
                }
                set
                {
                    if (this.lineColor != value)
                    {
                        base.invalidate();
                    }
                    this.lineColor = value;
                }
            }
        }

        public class CSDListBox : CustomSelfDrawPanel.CSDControl
        {
            private bool created;
            public List<CustomSelfDrawPanel.CSDListItem> dataItems = new List<CustomSelfDrawPanel.CSDListItem>();
            private CSD_LineClickedDelegate doubleClickedDelegate;
            private DateTime doubleClickTime = DateTime.MinValue;
            private CustomSelfDrawPanel.CSDButton downArrow = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDListEntry[] entries;
            public List<CustomSelfDrawPanel.CSDListItem> highlightedItems = new List<CustomSelfDrawPanel.CSDListItem>();
            private CSD_LineClickedDelegate lineClickedDelegate;
            private int m_numRows;
            private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
            private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
            private CustomSelfDrawPanel.CSDImage scrollTabLines = new CustomSelfDrawPanel.CSDImage();
            private int selectedItemID = -1;
            private CustomSelfDrawPanel.CSDButton upArrow = new CustomSelfDrawPanel.CSDButton();

            private void cellClicked()
            {
                if (base.csd.ClickedControl != null)
                {
                    CustomSelfDrawPanel.CSDListEntry clickedControl = (CustomSelfDrawPanel.CSDListEntry) base.csd.ClickedControl;
                    int num = clickedControl.Data + this.scrollBar.Value;
                    DateTime now = DateTime.Now;
                    bool flag = false;
                    if ((num >= 0) && (num < this.dataItems.Count))
                    {
                        if (num == this.selectedItemID)
                        {
                            TimeSpan span = (TimeSpan) (now - this.doubleClickTime);
                            if (span.TotalSeconds < 2.0)
                            {
                                flag = true;
                                this.doubleClickTime = DateTime.MinValue;
                            }
                        }
                        this.doubleClickTime = now;
                        this.selectedItemID = num;
                        this.updateEntries();
                        if (this.lineClickedDelegate != null)
                        {
                            this.lineClickedDelegate(this.dataItems[num]);
                        }
                        if (flag && (this.doubleClickedDelegate != null))
                        {
                            this.doubleClickedDelegate(this.dataItems[num]);
                        }
                    }
                }
            }

            public void clearSelectedItem()
            {
                this.selectedItemID = -1;
                this.updateEntries();
            }

            public bool contains(string testText)
            {
                foreach (CustomSelfDrawPanel.CSDListItem item in this.dataItems)
                {
                    if (item.Text == testText)
                    {
                        return true;
                    }
                }
                return false;
            }

            public void Create(int numRows, int elementHeight)
            {
                if (base.csd != null)
                {
                    if (!this.created)
                    {
                        this.created = true;
                        this.m_numRows = numRows;
                        this.entries = new CustomSelfDrawPanel.CSDListEntry[numRows];
                        for (int i = 0; i < numRows; i++)
                        {
                            CustomSelfDrawPanel.CSDListEntry control = new CustomSelfDrawPanel.CSDListEntry {
                                Position = new Point(0, i * elementHeight),
                                Size = new Size(this.Size.Width - 0x10, elementHeight),
                                Data = i
                            };
                            control.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cellClicked), "CSD_List_Box_entry_clicked");
                            this.entries[i] = control;
                            base.addControl(control);
                            control.setup();
                        }
                        this.scrollBar.Position = new Point(this.Size.Width - 0x10, 0x11);
                        this.scrollBar.Size = new Size(0x10, ((this.Size.Height - 0x11) - 0x11) - 1);
                        base.addControl(this.scrollBar);
                        this.scrollBar.Value = 0;
                        this.scrollBar.Max = 0;
                        this.scrollBar.NumVisibleLines = numRows;
                        this.scrollBar.TabMinSize = 0x1a;
                        this.scrollBar.OffsetTL = new Point(0, 0);
                        this.scrollBar.OffsetBR = new Point(0, 0);
                        this.scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
                        this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarValueMoved));
                        this.scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.scrollBarMoved));
                        this.upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
                        this.upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
                        this.upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
                        this.upArrow.Position = new Point(this.scrollBar.Position.X, 0);
                        this.upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ScrollUp), "CSD_List_Box_scroll_up");
                        base.addControl(this.upArrow);
                        this.downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
                        this.downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
                        this.downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
                        this.downArrow.Position = new Point(this.scrollBar.Position.X, this.scrollBar.Position.Y + this.scrollBar.Size.Height);
                        this.downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ScrollDown), "CSD_List_Box_scroll_down");
                        base.addControl(this.downArrow);
                        this.scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
                        this.scrollTabLines.Position = new Point(this.scrollBar.TabPosition.X, ((this.scrollBar.TabSize - 8) / 2) + this.scrollBar.TabPosition.Y);
                        this.scrollBar.addControl(this.scrollTabLines);
                        this.mouseWheelOverlay.Position = new Point(0, 0);
                        this.mouseWheelOverlay.Size = this.Size;
                        this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
                        base.addControl(this.mouseWheelOverlay);
                    }
                    this.selectedItemID = -1;
                    this.updateEntries();
                }
            }

            public CustomSelfDrawPanel.CSDListItem getSelectedItem()
            {
                if ((this.selectedItemID >= 0) && (this.selectedItemID < this.dataItems.Count))
                {
                    return this.dataItems[this.selectedItemID];
                }
                return null;
            }

            private void mouseWheelMoved(int delta)
            {
                if (this.scrollBar.Visible)
                {
                    if (delta < 0)
                    {
                        this.ScrollDown(3);
                    }
                    else if (delta > 0)
                    {
                        this.ScrollUp(3);
                    }
                }
            }

            public void populate(List<CustomSelfDrawPanel.CSDListItem> items)
            {
                this.dataItems = items;
                this.updateEntries();
            }

            private void scrollBarMoved()
            {
                this.scrollTabLines.Position = new Point(this.scrollBar.TabPosition.X, ((this.scrollBar.TabSize - 8) / 2) + this.scrollBar.TabPosition.Y);
            }

            private void scrollBarValueMoved()
            {
                this.updateEntries();
            }

            private void ScrollDown()
            {
                this.ScrollDown(1);
            }

            private void ScrollDown(int diff)
            {
                int max = this.scrollBar.Value;
                if (max < this.scrollBar.Max)
                {
                    max += diff;
                    if (max > this.scrollBar.Max)
                    {
                        max = this.scrollBar.Max;
                    }
                    this.scrollBar.Value = max;
                    this.scrollBar.invalidate();
                    this.scrollBarValueMoved();
                    this.scrollBarMoved();
                    base.invalidate();
                }
            }

            private void ScrollUp()
            {
                this.ScrollUp(1);
            }

            private void ScrollUp(int diff)
            {
                int num = this.scrollBar.Value;
                if (num > 0)
                {
                    num -= diff;
                    if (num < 0)
                    {
                        num = 0;
                    }
                    this.scrollBar.Value = num;
                    this.scrollBar.invalidate();
                    this.scrollBarMoved();
                    this.scrollBarValueMoved();
                    base.invalidate();
                }
            }

            public void setDoubleClickedDelegate(CSD_LineClickedDelegate callback)
            {
                this.doubleClickedDelegate = callback;
            }

            public void setLineClickedDelegate(CSD_LineClickedDelegate callback)
            {
                this.lineClickedDelegate = callback;
            }

            public void updateEntries()
            {
                int num = this.scrollBar.Max + this.m_numRows;
                if (num > this.dataItems.Count)
                {
                    int num2 = Math.Max(0, this.dataItems.Count - this.m_numRows);
                    if (this.scrollBar.Value > num2)
                    {
                        this.scrollBar.Value = num2;
                    }
                    this.scrollBar.Max = num2;
                }
                else
                {
                    this.scrollBar.Max = Math.Max(0, this.dataItems.Count - this.m_numRows);
                }
                for (int i = 0; i < this.m_numRows; i++)
                {
                    CustomSelfDrawPanel.CSDListEntry entry = this.entries[i];
                    entry.Text.Text = "";
                    entry.reset();
                }
                int num4 = this.scrollBar.Value;
                int index = 0;
                while ((index < this.m_numRows) && (num4 < this.dataItems.Count))
                {
                    CustomSelfDrawPanel.CSDListItem item = this.dataItems[num4];
                    CustomSelfDrawPanel.CSDListEntry entry2 = this.entries[index];
                    entry2.Text.Text = item.Text;
                    if (num4 == this.selectedItemID)
                    {
                        if (this.highlightedItems.Contains(item))
                        {
                            entry2.BodyColor = ARGBColors.GreenYellow;
                            entry2.OverColor = ARGBColors.Honeydew;
                        }
                        else
                        {
                            entry2.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                            entry2.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                        }
                    }
                    else if (this.highlightedItems.Contains(item))
                    {
                        entry2.BodyColor = ARGBColors.LightGreen;
                        entry2.OverColor = ARGBColors.Chartreuse;
                    }
                    entry2.invalidate();
                    index++;
                    num4++;
                }
                this.scrollBar.recalc();
                this.scrollBar.invalidate();
                this.scrollBarMoved();
            }

            public override bool Enabled
            {
                get
                {
                    return true;
                }
                set
                {
                    if (this.created)
                    {
                        if (this.entries[0].Enabled != value)
                        {
                            base.invalidate();
                        }
                        foreach (CustomSelfDrawPanel.CSDListEntry entry in this.entries)
                        {
                            entry.Enabled = value;
                        }
                    }
                }
            }

            public delegate void CSD_LineClickedDelegate(CustomSelfDrawPanel.CSDListItem item);
        }

        private class CSDListEntry : CustomSelfDrawPanel.CSDControl
        {
            private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
            private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
            private Color lineColor = CustomSelfDrawPanel.MailLineColor;
            private Color lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
            private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
            private Color overColor = CustomSelfDrawPanel.MailOverColor;
            private bool setupComplete;
            private CustomSelfDrawPanel.CSDLabel textLabel = new CustomSelfDrawPanel.CSDLabel();

            private void mouseLeave()
            {
                this.main.FillColor = this.bodyColor;
                this.line.FillColor = this.lineColor;
                base.invalidate();
            }

            private void mouseOver()
            {
                if ((this.textLabel.Text.Length > 0) && base.parent.Enabled)
                {
                    this.main.FillColor = this.overColor;
                    this.line.FillColor = this.lineOverColor;
                }
            }

            public void reset()
            {
                this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
                this.lineColor = CustomSelfDrawPanel.MailLineColor;
                this.overColor = CustomSelfDrawPanel.MailOverColor;
                this.lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
                this.main.FillColor = this.bodyColor;
                this.line.FillColor = this.lineColor;
            }

            public void setup()
            {
                this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
                this.main.FillColor = this.bodyColor;
                this.line.Position = new Point(0, this.Size.Height - 1);
                this.line.Size = new Size(this.Size.Width, 1);
                this.line.FillColor = this.lineColor;
                this.textLabel.Position = new Point(3, 2);
                this.textLabel.Size = new Size(0x103, this.Size.Height - 4);
                this.textLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
                base.addControl(this.main);
                base.addControl(this.line);
                this.main.addControl(this.textLabel);
                this.setupComplete = true;
            }

            public Color BodyColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.bodyColor != value)
                        {
                            this.main.invalidate();
                        }
                        this.main.FillColor = value;
                    }
                    this.bodyColor = value;
                }
            }

            public Color LineColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.lineColor != value)
                        {
                            this.line.invalidate();
                        }
                        this.line.FillColor = value;
                    }
                    this.lineColor = value;
                }
            }

            public Color LineOverColor
            {
                set
                {
                    this.lineOverColor = value;
                }
            }

            public Color OverColor
            {
                set
                {
                    this.overColor = value;
                }
            }

            public CustomSelfDrawPanel.CSDLabel Text
            {
                get
                {
                    return this.textLabel;
                }
            }
        }

        public class CSDListItem
        {
            private long data;
            private string text = "";

            public int Data
            {
                get
                {
                    return (int) this.data;
                }
                set
                {
                    this.data = value;
                }
            }

            public long DataL
            {
                get
                {
                    return this.data;
                }
                set
                {
                    this.data = value;
                }
            }

            public string Text
            {
                get
                {
                    return this.text;
                }
                set
                {
                    this.text = value;
                }
            }
        }

        public class CSDRectangle : CustomSelfDrawPanel.CSDControl
        {
            private Color lineColor = ARGBColors.White;

            public override void draw(Point parentLocation)
            {
                Rectangle rectangle = base.Rectangle;
                rectangle.X += parentLocation.X;
                rectangle.Y += parentLocation.Y;
                base.csd.drawLine(this.lineColor, new Point(rectangle.X, rectangle.Y), new Point((rectangle.X + this.Size.Width) - 1, rectangle.Y));
                base.csd.drawLine(this.lineColor, new Point(rectangle.X, rectangle.Y), new Point(rectangle.X, (rectangle.Y + this.Size.Height) - 2));
                base.csd.drawLine(this.lineColor, new Point((rectangle.X + this.Size.Width) - 1, (rectangle.Y + this.Size.Height) - 1), new Point((rectangle.X + this.Size.Width) - 1, rectangle.Y));
                base.csd.drawLine(this.lineColor, new Point((rectangle.X + this.Size.Width) - 1, (rectangle.Y + this.Size.Height) - 1), new Point(rectangle.X, (rectangle.Y + this.Size.Height) - 1));
            }

            public Color LineColor
            {
                get
                {
                    return this.lineColor;
                }
                set
                {
                    if (this.lineColor != value)
                    {
                        base.invalidate();
                    }
                    this.lineColor = value;
                }
            }
        }

        public class CSDScrollLabel : CustomSelfDrawPanel.CSDControl
        {
            private System.Drawing.Color color = ARGBColors.Black;
            private bool dirty = true;
            private System.Drawing.Font font = FontManager.GetFont("Arial", 8.25f);
            private string text = "";
            private int textHeight;
            private CSD_TextHeightChanged textHeightDelegate;
            private int verticalOffset;

            public override void draw(Point parentLocation)
            {
                if (this.dirty)
                {
                    SizeF ef = base.csd.getStringBounds(this.Text, base.Rectangle.Width, this.font);
                    this.textHeight = (int) ef.Height;
                    this.dirty = false;
                    if (this.textHeightDelegate != null)
                    {
                        this.textHeightDelegate(this.textHeight);
                    }
                }
                Rectangle clipRect = base.Rectangle;
                System.Drawing.Font font = this.font;
                if (base.Scale != 1.0)
                {
                    if ((this.font.SizeInPoints * ((float) base.Scale)) < 6f)
                    {
                        return;
                    }
                    clipRect.X = (int) (clipRect.X * base.Scale);
                    clipRect.Y = (int) (clipRect.Y * base.Scale);
                    clipRect.Width = (int) (clipRect.Width * base.Scale);
                    clipRect.Height = (int) (clipRect.Height * base.Scale);
                    font = new System.Drawing.Font(this.font.FontFamily, this.font.SizeInPoints * ((float) base.Scale), this.font.Style);
                }
                clipRect.X += parentLocation.X;
                clipRect.Y += parentLocation.Y;
                base.csd.setClipRegion(clipRect);
                clipRect.Y -= this.verticalOffset;
                clipRect.Height = 0x186a0;
                base.csd.drawString(this.Text, clipRect, this.color, font, CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT);
                base.csd.restoreClipRegion();
            }

            public void setTextHeightChangedCallback(CSD_TextHeightChanged callback)
            {
                this.textHeightDelegate = callback;
            }

            public System.Drawing.Color Color
            {
                get
                {
                    return this.color;
                }
                set
                {
                    this.color = value;
                    base.invalidate();
                }
            }

            public System.Drawing.Font Font
            {
                set
                {
                    this.font = value;
                    this.dirty = true;
                }
            }

            public string Text
            {
                get
                {
                    return this.text;
                }
                set
                {
                    this.text = value;
                    base.invalidate();
                    this.dirty = true;
                }
            }

            public int TextHeight
            {
                get
                {
                    return this.textHeight;
                }
            }

            public int VerticalOffset
            {
                get
                {
                    return this.verticalOffset;
                }
                set
                {
                    this.verticalOffset = value;
                    base.invalidate();
                }
            }

            public delegate void CSD_TextHeightChanged(int textHeight);
        }

        public class CSDTabControl : CustomSelfDrawPanel.CSDControl
        {
            private int currentSelected;
            private List<CustomSelfDrawPanel.CSDTabItem> items = new List<CustomSelfDrawPanel.CSDTabItem>();
            private TabClickedCallback soundsCallback;

            public void addOverlayImages(int tab, BaseImage overlayNorm, BaseImage overlaySelected, int alpha)
            {
                if ((tab >= 0) && (tab < this.items.Count))
                {
                    float num = ((float) alpha) / 255f;
                    this.items[tab].overlayNormImage = (Image) overlayNorm;
                    this.items[tab].overlaySelectedImage = (Image) overlaySelected;
                    this.items[tab].overlayAlpha = num;
                    if (overlayNorm != null)
                    {
                        this.items[tab].overlayImageWidth = overlayNorm.Width;
                    }
                }
            }

            public int Create(int numIcons, BaseImage[] images)
            {
                this.items.Clear();
                int xPos = 0;
                for (int i = 0; i < numIcons; i++)
                {
                    CustomSelfDrawPanel.CSDTabItem item = new CustomSelfDrawPanel.CSDTabItem();
                    if ((i * 2) < images.Length)
                    {
                        item.normImage = (Image) images[i * 2];
                        item.selectedImage = (Image) images[(i * 2) + 1];
                    }
                    xPos += item.setup(xPos, this, i, i == this.currentSelected);
                    this.items.Add(item);
                    base.addControl(item);
                }
                this.currentSelected = 0;
                this.updateAll();
                return xPos;
            }

            public void setCallback(int tab, TabClickedCallback callback, int tooltip)
            {
                if ((tab >= 0) && (tab < this.items.Count))
                {
                    this.items[tab].setCallback(callback);
                    this.items[tab].setTooltip(tooltip);
                }
            }

            public void setOverlayAlpha(int tab, int alpha)
            {
                if ((tab >= 0) && (tab < this.items.Count))
                {
                    float num = ((float) alpha) / 255f;
                    if (num != this.items[tab].overlayAlpha)
                    {
                        this.items[tab].overlayAlpha = num;
                    }
                }
            }

            public void setOverlayWidth(int tab, int width)
            {
                if ((tab >= 0) && (tab < this.items.Count))
                {
                    this.items[tab].setOverlayWidth(width);
                }
            }

            public void setSoundCallback(TabClickedCallback callback)
            {
                this.soundsCallback = callback;
            }

            public void setTabText(int tab, string text)
            {
                if ((tab >= 0) && (tab < this.items.Count))
                {
                    this.items[tab].setText(text);
                }
            }

            public void setTooltip(int tab, int tooltip)
            {
                if ((tab >= 0) && (tab < this.items.Count))
                {
                    this.items[tab].setTooltip(tooltip);
                }
            }

            public void tabClicked(int index, bool fromClick)
            {
                this.currentSelected = index;
                this.updateAll();
                if ((this.currentSelected >= 0) && (this.currentSelected < this.items.Count))
                {
                    this.items[this.currentSelected].runCallback();
                }
                if (fromClick && (this.soundsCallback != null))
                {
                    this.soundsCallback();
                }
            }

            private void updateAll()
            {
                int num = 0;
                foreach (CustomSelfDrawPanel.CSDTabItem item in this.items)
                {
                    item.updateImage(num == this.currentSelected);
                    num++;
                }
            }

            public void updateImageArray(BaseImage[] images)
            {
                int num = 0;
                foreach (CustomSelfDrawPanel.CSDTabItem item in this.items)
                {
                    if ((num * 2) < images.Length)
                    {
                        item.normImage = (Image) images[num * 2];
                        item.selectedImage = (Image) images[(num * 2) + 1];
                    }
                    item.updateImage(num == this.currentSelected);
                    num++;
                }
            }

            public int SelectedIndex
            {
                get
                {
                    return this.currentSelected;
                }
                set
                {
                    this.currentSelected = value;
                    this.tabClicked(value, false);
                }
            }

            public delegate void TabClickedCallback();
        }

        private class CSDTabItem : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDTabControl.TabClickedCallback m_callback;
            private float m_overlayAlpha = 1f;
            private Image m_overlayNormImage;
            private Image m_overlaySelectedImage;
            private CustomSelfDrawPanel.CSDTabControl m_parentControl;
            private bool m_selected;
            private CustomSelfDrawPanel.CSDButton mainButton = new CustomSelfDrawPanel.CSDButton();
            public Image normImage;
            public int overlayImageWidth = 1;
            public Image selectedImage;

            public void runCallback()
            {
                if (this.m_callback != null)
                {
                    this.m_callback();
                }
            }

            public void setCallback(CustomSelfDrawPanel.CSDTabControl.TabClickedCallback callback)
            {
                this.m_callback = callback;
            }

            public void setOverlayWidth(int width)
            {
                if (width != this.mainButton.ImageIconClipRect.Width)
                {
                    this.mainButton.ImageIconClipRect = new Rectangle(0, 0, width, this.mainButton.Height);
                    this.mainButton.invalidate();
                }
            }

            public void setText(string text)
            {
                this.mainButton.Text.Text = text;
            }

            public void setTooltip(int tooltip)
            {
                this.mainButton.CustomTooltipID = tooltip;
            }

            public int setup(int xPos, CustomSelfDrawPanel.CSDTabControl parentControl, int index, bool selected)
            {
                this.m_selected = selected;
                this.m_parentControl = parentControl;
                if (!selected)
                {
                    this.mainButton.ImageNormAndOver = this.normImage;
                }
                else
                {
                    this.mainButton.ImageNormAndOver = this.selectedImage;
                }
                this.mainButton.MoveOnClick = false;
                this.mainButton.Position = new Point(xPos, 0);
                this.mainButton.Data = index;
                this.mainButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked));
                this.mainButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.mainButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.mainButton.Text.Position = new Point(0, 5);
                this.mainButton.Text.Color = ARGBColors.White;
                this.mainButton.LateTextRender = true;
                base.addControl(this.mainButton);
                this.Size = this.mainButton.Size;
                if (this.mainButton.ImageNormAndOver != null)
                {
                    return this.mainButton.ImageNormAndOver.Width;
                }
                return 1;
            }

            private void tabClicked()
            {
                if (this.m_parentControl != null)
                {
                    this.m_parentControl.tabClicked(base.csd.ClickedControl.Data, true);
                }
            }

            public void updateImage(bool selected)
            {
                this.m_selected = selected;
                if (!selected)
                {
                    if (this.normImage != this.mainButton.ImageNormAndOver)
                    {
                        this.mainButton.ImageNormAndOver = this.normImage;
                        this.mainButton.invalidate();
                    }
                    if (this.overlayNormImage != this.mainButton.ImageIcon)
                    {
                        this.overlayNormImage = this.overlayNormImage;
                    }
                }
                else
                {
                    if (this.mainButton.ImageNormAndOver != this.selectedImage)
                    {
                        this.mainButton.ImageNormAndOver = this.selectedImage;
                        this.mainButton.invalidate();
                    }
                    if (this.overlaySelectedImage != this.mainButton.ImageIcon)
                    {
                        this.overlaySelectedImage = this.overlaySelectedImage;
                    }
                }
            }

            public float overlayAlpha
            {
                get
                {
                    return this.m_overlayAlpha;
                }
                set
                {
                    this.m_overlayAlpha = value;
                    this.mainButton.ImageIconAlpha = value;
                }
            }

            public Image overlayNormImage
            {
                get
                {
                    return this.m_overlayNormImage;
                }
                set
                {
                    this.m_overlayNormImage = value;
                    if (value != null)
                    {
                        if (!this.m_selected && (this.mainButton.ImageIcon != value))
                        {
                            this.mainButton.ImageIcon = value;
                            this.mainButton.invalidate();
                        }
                    }
                    else if (this.mainButton.ImageIcon != value)
                    {
                        this.mainButton.ImageIcon = null;
                        this.mainButton.invalidate();
                    }
                }
            }

            public Image overlaySelectedImage
            {
                get
                {
                    return this.m_overlaySelectedImage;
                }
                set
                {
                    this.m_overlaySelectedImage = value;
                    if (value != null)
                    {
                        if (this.m_selected && (this.mainButton.ImageIcon != value))
                        {
                            this.mainButton.ImageIcon = value;
                            this.mainButton.invalidate();
                        }
                    }
                    else if (this.mainButton.ImageIcon != value)
                    {
                        this.mainButton.ImageIcon = null;
                        this.mainButton.invalidate();
                    }
                }
            }
        }

        public class CSDTrackBar : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
            private int baseValue;
            private int baseXPos;
            private bool clickedOnBar;
            private bool created;
            private int currentValue;
            public bool held;
            private CustomSelfDrawPanel.CSDImage leftTab = new CustomSelfDrawPanel.CSDImage();
            private bool m_mouseOverFlag;
            private Image m_tabDownImage;
            private Image m_tabImage;
            private Image m_tabOverImage;
            private Rectangle margin = new Rectangle();
            private int maxTabPosition;
            private int maxValue;
            private int minTabPosition;
            private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
            private CustomSelfDrawPanel.CSDImage rightTab = new CustomSelfDrawPanel.CSDImage();
            private int stepSize = 1;
            private int stepValue = 1;
            private CustomSelfDrawPanel.CSDImage tab = new CustomSelfDrawPanel.CSDImage();
            protected CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate valueChangedDelegate;

            private void backgroundButtonDown()
            {
                this.clickedOnBar = true;
            }

            private void backgroundButtonUp()
            {
                if (this.clickedOnBar)
                {
                    int x = this.background.LastRelativeMousePos.X;
                    int num2 = this.tab.Position.X + (this.tab.Width / 2);
                    int currentValue = this.currentValue;
                    this.tab.invalidate();
                    if (x < num2)
                    {
                        this.currentValue -= this.stepValue;
                        if (this.currentValue < 0)
                        {
                            this.currentValue = 0;
                        }
                        this.recalc();
                    }
                    else
                    {
                        this.currentValue += this.stepValue;
                        if (this.currentValue > this.maxValue)
                        {
                            this.currentValue = this.maxValue;
                        }
                        this.recalc();
                    }
                    if ((currentValue != this.currentValue) && (this.valueChangedDelegate != null))
                    {
                        this.valueChangedDelegate();
                    }
                    this.background.invalidate();
                    this.tab.invalidate();
                }
                this.clickedOnBar = false;
            }

            private void buttonDown()
            {
                if (!this.held)
                {
                    this.baseXPos = base.csd.LastMousePosition.X;
                    this.baseValue = this.Value;
                    this.held = true;
                    this.updateTabImages();
                }
                else
                {
                    int diff = base.csd.LastMousePosition.X - this.baseXPos;
                    if (diff != 0)
                    {
                        this.moveTabPosition(this.baseXPos, diff);
                    }
                }
            }

            private void buttonLeave()
            {
                this.m_mouseOverFlag = false;
                this.updateTabImages();
            }

            private void buttonOver()
            {
                this.m_mouseOverFlag = true;
                this.updateTabImages();
            }

            private void buttonUp()
            {
            }

            private void calcStepSize()
            {
                int num = (this.background.Size.Width - this.margin.Left) - this.margin.Width;
                if (this.maxValue <= 1)
                {
                    this.stepSize = num;
                }
                else
                {
                    this.stepSize = num / this.maxValue;
                }
            }

            public void Create(Image backImage, Image tabImage, Image leftImage, Image rightImage, Image tabDownImage, Image tabOverImage)
            {
                this.Create(backImage, tabImage, leftImage, rightImage, tabDownImage, tabOverImage, true);
            }

            public void Create(Image backImage, Image tabImage, Image leftImage, Image rightImage, Image tabDownImage, Image tabOverImage, bool backClick)
            {
                this.m_tabImage = tabImage;
                this.m_tabDownImage = tabDownImage;
                this.m_tabOverImage = tabOverImage;
                Size size = this.Size;
                if (backImage != null)
                {
                    this.background.Image = backImage;
                    this.background.Size = backImage.Size;
                    base.addControl(this.background);
                    size = backImage.Size;
                }
                else
                {
                    this.background.Size = size;
                    base.addControl(this.background);
                }
                this.tab.Image = tabImage;
                this.minTabPosition = this.margin.Left - (this.tab.Size.Width / 2);
                this.maxTabPosition = this.minTabPosition + ((size.Width - this.margin.Left) - this.margin.Width);
                this.tab.Position = new Point(this.minTabPosition, this.margin.Top);
                this.background.addControl(this.tab);
                this.created = true;
                this.recalc();
                this.calcStepSize();
                this.Size = size;
                this.tab.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
                this.tab.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.buttonLeave));
                if (backClick)
                {
                    this.background.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonUp));
                }
                this.mouseWheelOverlay.Position = new Point(0, 0);
                this.mouseWheelOverlay.Size = size;
                this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
                base.addControl(this.mouseWheelOverlay);
            }

            public override void draw(Point parentLocation)
            {
                if (this.held)
                {
                    if (!base.csd.MouseReallyPressed)
                    {
                        this.tab.MouseDownFlag = false;
                        this.held = false;
                        this.updateTabImages();
                    }
                    else
                    {
                        this.buttonDown();
                    }
                }
            }

            public void invalidateTab()
            {
                this.tab.invalidate();
            }

            private void mouseWheelMoved(int delta)
            {
                this.tab.invalidate();
                int currentValue = this.currentValue;
                if (delta < 0)
                {
                    this.currentValue -= this.stepValue;
                    if (this.currentValue < 0)
                    {
                        this.currentValue = 0;
                    }
                    this.recalc();
                }
                else if (delta > 0)
                {
                    this.currentValue += this.stepValue;
                    if (this.currentValue > this.maxValue)
                    {
                        this.currentValue = this.maxValue;
                    }
                    this.recalc();
                }
                if ((currentValue != this.currentValue) && (this.valueChangedDelegate != null))
                {
                    this.valueChangedDelegate();
                }
                this.background.invalidate();
                this.tab.invalidate();
            }

            private void moveTabPosition(int baseYPos, int diff)
            {
                int currentValue = this.currentValue;
                this.tab.invalidate();
                if (this.maxValue == 0)
                {
                    this.currentValue = 0;
                }
                else
                {
                    int num2 = (this.background.Size.Width - this.margin.Left) - this.margin.Width;
                    int num3 = (num2 * this.baseValue) / Math.Max(this.maxValue, 1);
                    num3 += diff;
                    if (num3 < 0)
                    {
                        num3 = 0;
                    }
                    if (num3 >= num2)
                    {
                        num3 = num2;
                    }
                    this.currentValue = (this.maxValue * (num3 + (this.stepSize / 2))) / Math.Max(this.maxTabPosition - this.minTabPosition, 1);
                }
                this.recalc();
                if ((currentValue != this.currentValue) && (this.valueChangedDelegate != null))
                {
                    this.valueChangedDelegate();
                }
                base.invalidate();
                this.background.invalidate();
                this.tab.invalidate();
            }

            public void recalc()
            {
                if (this.created)
                {
                    if (this.maxValue > 0)
                    {
                        int num = (this.background.Size.Width - this.margin.Left) - this.margin.Width;
                        int num2 = (num * this.Value) / Math.Max(this.maxValue, 1);
                        this.tab.Position = new Point(this.minTabPosition + num2, this.margin.Top);
                    }
                    else
                    {
                        this.tab.Position = new Point(this.minTabPosition, this.margin.Top);
                    }
                }
            }

            public void setValueChangeDelegate(CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate newDelegate)
            {
                this.valueChangedDelegate = newDelegate;
            }

            private void updateTabImages()
            {
                if (this.held)
                {
                    this.tab.Image = this.m_tabDownImage;
                }
                else if (this.m_mouseOverFlag)
                {
                    this.tab.Image = this.m_tabOverImage;
                }
                else
                {
                    this.tab.Image = this.m_tabImage;
                }
                this.tab.invalidate();
            }

            public Rectangle Margin
            {
                get
                {
                    return this.margin;
                }
                set
                {
                    this.margin = value;
                    this.recalc();
                }
            }

            public int Max
            {
                get
                {
                    return this.maxValue;
                }
                set
                {
                    this.maxValue = value;
                    this.recalc();
                    this.calcStepSize();
                }
            }

            public int StepValue
            {
                get
                {
                    return this.stepValue;
                }
                set
                {
                    this.stepValue = value;
                    if (this.stepValue < 1)
                    {
                        this.stepValue = 1;
                    }
                }
            }

            public int Value
            {
                get
                {
                    return this.currentValue;
                }
                set
                {
                    this.currentValue = value;
                    this.recalc();
                }
            }
        }

        public class CSDVertExtendingPanel : CustomSelfDrawPanel.CSDControl
        {
            protected CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate areaMouseDownDelegate;
            protected CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate areaMouseUpDelegate;
            public CustomSelfDrawPanel.CSDImage Bottom = new CustomSelfDrawPanel.CSDImage();
            private bool created;
            private bool held;
            private Point heldPosition = new Point(0, 0);
            private bool inResize;
            public CustomSelfDrawPanel.CSDImage Mid = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage Top = new CustomSelfDrawPanel.CSDImage();
            private int yDiff;

            public CSDVertExtendingPanel()
            {
                base.setMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
            }

            private void buttonDown()
            {
                if (!this.held)
                {
                    this.heldPosition = base.csd.LastMousePosition;
                    this.held = true;
                    this.yDiff = 0;
                }
                else
                {
                    this.yDiff = base.csd.LastMousePosition.Y - this.heldPosition.Y;
                }
                if (this.areaMouseDownDelegate != null)
                {
                    this.areaMouseDownDelegate();
                }
            }

            private void buttonUp()
            {
                this.held = false;
                this.yDiff = 0;
                if (this.areaMouseUpDelegate != null)
                {
                    this.areaMouseUpDelegate();
                }
            }

            public void Create(Image topImage, Image midImage, Image bottomImage)
            {
                if (((topImage != null) && (midImage != null)) && (bottomImage != null))
                {
                    this.Top.Image = topImage;
                    this.Top.Position = new Point(0, 0);
                    base.addControl(this.Top);
                    this.Bottom.Image = bottomImage;
                    this.Bottom.Position = new Point(0, base.Height - this.Bottom.Image.Height);
                    base.addControl(this.Bottom);
                    this.Mid.Image = midImage;
                    this.Mid.Position = new Point(0, this.Top.Image.Height);
                    this.Mid.Size = new System.Drawing.Size(this.Mid.Image.Width, (base.Height - this.Top.Image.Height) - this.Bottom.Image.Height);
                    this.Mid.Tile = true;
                    base.addControl(this.Mid);
                    this.Size = new System.Drawing.Size(this.Top.Image.Width, this.Size.Height);
                    this.created = true;
                }
            }

            public override void draw(Point parentLocation)
            {
                if (this.held)
                {
                    if (!base.csd.MouseReallyPressed)
                    {
                        base.mouseDownFlag = false;
                        this.buttonUp();
                    }
                    else
                    {
                        this.yDiff = base.csd.LastMousePosition.Y - this.heldPosition.Y;
                        if (this.areaMouseDownDelegate != null)
                        {
                            this.areaMouseDownDelegate();
                        }
                    }
                }
            }

            public int getMinSize()
            {
                return ((this.Top.Image.Height + this.Bottom.Image.Height) + 1);
            }

            public void resize()
            {
                if (this.created && !this.inResize)
                {
                    this.inResize = true;
                    this.Bottom.Position = new Point(0, base.Height - this.Bottom.Image.Height);
                    this.Mid.Size = new System.Drawing.Size(this.Mid.Image.Width, (base.Height - this.Top.Image.Height) - this.Bottom.Image.Height);
                    this.Size = new System.Drawing.Size(this.Top.Image.Width, this.Size.Height);
                    this.inResize = false;
                }
            }

            public void setAreaMouseDownDelegate(CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate downDelegate, CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate upDelegate)
            {
                this.areaMouseDownDelegate = downDelegate;
                this.areaMouseUpDelegate = upDelegate;
            }

            public override System.Drawing.Size Size
            {
                get
                {
                    return base.Size;
                }
                set
                {
                    base.Size = value;
                    this.resize();
                }
            }

            public int YDiff
            {
                get
                {
                    return this.yDiff;
                }
            }
        }

        public class CSDVertImageScroller : CustomSelfDrawPanel.CSDControl
        {
            public Dictionary<int, int> ImageOffsets = new Dictionary<int, int>();
            public CustomSelfDrawPanel.CSDImage[] Images;
            public Point initialPosition;
            public bool scrolling;

            public void init(Point position, BaseImage[] sourceImages, int[] sourceIDs)
            {
                this.initialPosition = position;
                this.Position = position;
                this.Images = new CustomSelfDrawPanel.CSDImage[sourceImages.Length + 1];
                int height = 0;
                int width = 0;
                for (int i = 0; i <= sourceImages.Length; i++)
                {
                    CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
                    if (i == sourceImages.Length)
                    {
                        control.Size = sourceImages[0].Size;
                        control.Image = (Image) sourceImages[0];
                    }
                    else
                    {
                        control.Image = (Image) sourceImages[i];
                        control.Size = sourceImages[i].Size;
                    }
                    if (i == 0)
                    {
                        control.Position = new Point(0, 0);
                    }
                    else
                    {
                        control.Position = new Point(0, this.Images[i - 1].Y - control.Height);
                    }
                    this.Images[i] = control;
                    if ((i < sourceIDs.Length) && !this.ImageOffsets.ContainsKey(sourceIDs[i]))
                    {
                        this.ImageOffsets.Add(sourceIDs[i], -control.Y);
                    }
                    else if (i == sourceIDs.Length)
                    {
                        this.ImageOffsets[sourceIDs[0]] = -control.Y;
                    }
                    base.addControl(control);
                    if (base.Width < control.Width)
                    {
                        width = control.Width;
                    }
                    if (i < sourceImages.Length)
                    {
                        height += control.Height;
                    }
                }
                this.Size = new Size(width, height);
                base.ClipRect = new Rectangle(0, 0, this.Images[0].Width, this.Images[0].Height);
            }

            public void scroll(int speed)
            {
                this.scrolling = true;
                this.Position = new Point(this.Position.X, this.Position.Y + speed);
                base.ClipRect = new Rectangle(base.ClipRect.X, base.ClipRect.Y - speed, base.ClipRect.Width, base.ClipRect.Height);
                if (this.Position.Y > (this.initialPosition.Y + base.Height))
                {
                    this.Position = new Point(this.Position.X, this.Position.Y - base.Height);
                    base.ClipRect = new Rectangle(base.ClipRect.X, base.ClipRect.Y + base.Height, base.ClipRect.Width, base.ClipRect.Height);
                }
            }

            public void scroll(int speed, int stop)
            {
                if (((this.Position.Y - this.initialPosition.Y) <= this.ImageOffsets[stop]) && (((this.Position.Y - this.initialPosition.Y) + speed) >= this.ImageOffsets[stop]))
                {
                    this.Position = new Point(this.Position.X, this.initialPosition.Y + this.ImageOffsets[stop]);
                    base.ClipRect = new Rectangle(base.ClipRect.X, -this.ImageOffsets[stop], base.ClipRect.Width, base.ClipRect.Height);
                    this.scrolling = false;
                }
                else
                {
                    this.scroll(speed);
                }
            }
        }

        public class CSDVertScrollBar : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDVertExtendingPanel background = new CustomSelfDrawPanel.CSDVertExtendingPanel();
            private int baseYPos;
            private bool clickedOnBar;
            private bool created;
            private int currentValue;
            private bool held;
            private int maxTabPosition;
            private int maxValue;
            private int minTabPosition;
            private Point offsetBR = new Point();
            private Point offsetTL = new Point();
            protected CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate scrollChangedDelegate;
            private int stepSize;
            private CustomSelfDrawPanel.CSDVertExtendingPanel tab = new CustomSelfDrawPanel.CSDVertExtendingPanel();
            private int tabMinSize;
            protected CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate valueChangedDelegate;
            private int visibleLines = 1;

            private void backgroundButtonDown()
            {
                this.clickedOnBar = true;
            }

            private void backgroundButtonUp()
            {
                if (this.clickedOnBar)
                {
                    if (this.StepSize != 0)
                    {
                        if (this.background.LastRelativeMousePos.Y < (this.tab.Y + (this.tab.Height / 2)))
                        {
                            int num = this.Value;
                            int num2 = this.Value - this.stepSize;
                            if (num2 < 0)
                            {
                                num2 = 0;
                            }
                            if (num2 != num)
                            {
                                this.Value = num2;
                                if (this.valueChangedDelegate != null)
                                {
                                    this.valueChangedDelegate();
                                }
                                if (this.scrollChangedDelegate != null)
                                {
                                    this.scrollChangedDelegate();
                                }
                            }
                        }
                        else
                        {
                            int num3 = this.Value + this.stepSize;
                            this.Value = num3;
                            this.moveTabPosition(this.tab.Y, 0);
                            if (this.valueChangedDelegate != null)
                            {
                                this.valueChangedDelegate();
                            }
                            if (this.scrollChangedDelegate != null)
                            {
                                this.scrollChangedDelegate();
                            }
                        }
                    }
                    else if (this.background.LastRelativeMousePos.Y < (this.tab.Y + (this.tab.Height / 2)))
                    {
                        this.moveTabPosition(this.tab.Y, -this.tab.Height);
                    }
                    else
                    {
                        this.moveTabPosition(this.tab.Y, this.tab.Height);
                    }
                }
                this.clickedOnBar = false;
            }

            private void buttonDown()
            {
                if (!this.held)
                {
                    this.held = true;
                    this.baseYPos = this.tab.Position.Y;
                    this.tab.invalidate();
                }
                else
                {
                    this.moveTabPosition(this.baseYPos, this.tab.YDiff);
                }
            }

            private void buttonUp()
            {
                this.held = false;
            }

            public void Create(Image topImage, Image midImage, Image bottomImage, Image tabTopImage, Image tabMidImage, Image tabBottomImage)
            {
                this.background.Size = this.Size;
                base.addControl(this.background);
                this.background.Create(topImage, midImage, bottomImage);
                this.tab.Size = new Size((this.background.Size.Width - this.offsetTL.X) - this.offsetBR.X, (this.background.Size.Height - this.offsetTL.Y) - this.offsetBR.Y);
                this.tab.Position = new Point(0, this.offsetTL.Y);
                this.background.addControl(this.tab);
                this.tab.Create(tabTopImage, tabMidImage, tabBottomImage);
                this.created = true;
                this.recalc();
                this.tab.setAreaMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.buttonUp));
                this.background.setAreaMouseDownDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonDown), new CustomSelfDrawPanel.CSDControl.CSD_MouseDownDelegate(this.backgroundButtonUp));
            }

            private void moveTabPosition(int baseYPos, int diff)
            {
                int y = baseYPos + diff;
                if (y < this.minTabPosition)
                {
                    y = this.minTabPosition;
                }
                if (y >= this.maxTabPosition)
                {
                    y = this.maxTabPosition;
                }
                this.tab.Position = new Point(0, y);
                int currentValue = this.currentValue;
                if ((this.maxTabPosition - this.minTabPosition) > 0)
                {
                    this.currentValue = (this.maxValue * (y - this.minTabPosition)) / Math.Max(this.maxTabPosition - this.minTabPosition, 1);
                }
                else
                {
                    this.currentValue = 0;
                }
                if ((currentValue != this.currentValue) && (this.valueChangedDelegate != null))
                {
                    this.valueChangedDelegate();
                }
                if (this.scrollChangedDelegate != null)
                {
                    this.scrollChangedDelegate();
                }
                this.background.invalidate();
                this.tab.invalidate();
            }

            public void recalc()
            {
                if (this.created)
                {
                    int height = (this.background.Size.Height - this.offsetTL.Y) - this.offsetBR.Y;
                    if (this.maxValue > 0)
                    {
                        int num2 = this.tab.getMinSize() + this.tabMinSize;
                        int num3 = (height * this.NumVisibleLines) / Math.Max(this.maxValue + this.NumVisibleLines, 1);
                        if (num3 < num2)
                        {
                            num3 = num2;
                        }
                        int num4 = height - num3;
                        int num5 = (num4 * this.Value) / Math.Max(this.maxValue, 1);
                        this.tab.Size = new Size(this.tab.Size.Width, num3);
                        this.tab.Position = new Point(0, this.offsetTL.Y + num5);
                        this.maxTabPosition = this.minTabPosition + num4;
                    }
                    else
                    {
                        this.tab.Size = new Size(this.tab.Size.Width, height);
                        this.tab.Position = new Point(0, this.offsetTL.Y);
                        this.maxTabPosition = this.minTabPosition;
                    }
                    this.minTabPosition = this.offsetTL.Y;
                }
            }

            public void scrollDown()
            {
                this.moveTabPosition(this.tab.Y, this.tab.Height);
            }

            public void scrollDown(int amount)
            {
                this.moveTabPosition(this.tab.Y, amount);
            }

            public void scrollUp()
            {
                this.moveTabPosition(this.tab.Y, -this.tab.Height);
            }

            public void scrollUp(int amount)
            {
                this.moveTabPosition(this.tab.Y, -amount);
            }

            public void setScrollChangeDelegate(CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate newDelegate)
            {
                this.scrollChangedDelegate = newDelegate;
            }

            public void setValueChangeDelegate(CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate newDelegate)
            {
                this.valueChangedDelegate = newDelegate;
            }

            public int Max
            {
                get
                {
                    return this.maxValue;
                }
                set
                {
                    this.maxValue = value;
                    this.recalc();
                }
            }

            public int NumVisibleLines
            {
                get
                {
                    return this.visibleLines;
                }
                set
                {
                    this.visibleLines = value;
                    this.recalc();
                }
            }

            public Point OffsetBR
            {
                get
                {
                    return this.offsetBR;
                }
                set
                {
                    this.offsetBR = value;
                }
            }

            public Point OffsetTL
            {
                get
                {
                    return this.offsetTL;
                }
                set
                {
                    this.offsetTL = value;
                }
            }

            public int StepSize
            {
                get
                {
                    return this.stepSize;
                }
                set
                {
                    this.stepSize = value;
                }
            }

            public int TabMinSize
            {
                get
                {
                    return this.tabMinSize;
                }
                set
                {
                    this.tabMinSize = value;
                    this.recalc();
                }
            }

            public Point TabPosition
            {
                get
                {
                    return this.tab.Position;
                }
            }

            public int TabSize
            {
                get
                {
                    return this.tab.Size.Height;
                }
            }

            public int Value
            {
                get
                {
                    return this.currentValue;
                }
                set
                {
                    this.currentValue = value;
                    this.recalc();
                }
            }
        }

        public class FactionPanelSideBar : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDVertExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDVertExtendingPanel();
            private CustomSelfDrawPanel.CSDImage factionButtonBackground = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton factionChatButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionDiplomacyButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionForumButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionInvitesButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionLeaveFactionButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionMailFactionButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionMyFactionButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionOfficersButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionShowAllButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton factionStartFactionButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house10Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house11Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house12Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house13Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house14Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house15Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house16Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house17Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house18Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house19Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house1Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house20Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house2Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house3Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house4Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house5Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house6Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house7Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house8Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton house9Button = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDImage houseOverlay = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton houseShowAllButton = new CustomSelfDrawPanel.CSDButton();
            private MyMessageBoxPopUp leaveFactionPopup;
            private static int m_currentSidebarMode = -1;
            private static bool m_factionDataUpdated = false;
            private static DateTime m_lastFactionUpdate = DateTime.MinValue;
            private CustomSelfDrawPanel m_parent;
            public const int SIDEBAR_MODE_ALL_FACTIONS = 2;
            public const int SIDEBAR_MODE_DIPLOMACY = 4;
            public const int SIDEBAR_MODE_FORUM = 6;
            public const int SIDEBAR_MODE_HOUSE_INFO = 8;
            public const int SIDEBAR_MODE_HOUSE_LIST = 7;
            public const int SIDEBAR_MODE_INVITES = 0;
            public const int SIDEBAR_MODE_MY_FACTION = 1;
            public const int SIDEBAR_MODE_OFFICERS = 3;
            public const int SIDEBAR_MODE_START_FACTION = 5;
            public const int SIDEBAR_WIDTH = 200;

            public void addSideBar(int mode, CustomSelfDrawPanel parent)
            {
                CustomSelfDrawPanel.CSDButton button;
                m_factionDataUpdated = false;
                m_currentSidebarMode = mode;
                this.m_parent = parent;
                this.clearControls();
                this.Position = new Point(parent.Width - 200, 0);
                this.Size = new Size(200, parent.Height);
                parent.removeControl(this);
                parent.addControl(this);
                this.backgroundImage.Position = new Point(0, 0);
                this.backgroundImage.Size = new Size(200, parent.Height);
                base.addControl(this.backgroundImage);
                this.backgroundImage.Create((Image) GFXLibrary.faction_background, (Image) GFXLibrary.faction_background_bottom, (Image) GFXLibrary.faction_background_bottom);
                this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background;
                this.factionButtonBackground.Position = new Point(0, 0);
                this.factionButtonBackground.Visible = false;
                this.backgroundImage.addControl(this.factionButtonBackground);
                int y = 10;
                int num2 = 40;
                switch (mode)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    {
                        this.factionShowAllButton.ImageNorm = (Image) GFXLibrary.faction_buttons[0];
                        this.factionShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[1];
                        this.factionShowAllButton.Position = new Point(7, y);
                        this.factionShowAllButton.MoveOnClick = true;
                        this.factionShowAllButton.Text.Text = SK.Text("FactionsSidebar_Show_All", "Show All");
                        this.factionShowAllButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                        this.factionShowAllButton.Text.Position = new Point(0x54, 0);
                        this.factionShowAllButton.Text.Size = new Size(0x5e, 40);
                        this.factionShowAllButton.TextYOffset = -3;
                        this.factionShowAllButton.Text.Color = ARGBColors.Black;
                        this.factionShowAllButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showAllClicked), "FactionPanelSideBar_all_factions");
                        this.factionShowAllButton.Active = true;
                        this.factionShowAllButton.CustomTooltipID = 0x92e;
                        this.backgroundImage.addControl(this.factionShowAllButton);
                        y += num2;
                        int num3 = GameEngine.Instance.World.getYourFactionRank();
                        if (RemoteServices.Instance.UserFactionID >= 0)
                        {
                            this.factionMyFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[2];
                            this.factionMyFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[3];
                            this.factionMyFactionButton.Position = new Point(7, y);
                            this.factionMyFactionButton.MoveOnClick = true;
                            this.factionMyFactionButton.Text.Text = SK.Text("FactionsSidebar_My_Faction", "My Faction");
                            this.factionMyFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.factionMyFactionButton.Text.Position = new Point(0x54, 0);
                            this.factionMyFactionButton.Text.Size = new Size(0x5e, 40);
                            this.factionMyFactionButton.TextYOffset = -3;
                            this.factionMyFactionButton.Text.Color = ARGBColors.Black;
                            this.factionMyFactionButton.Active = true;
                            this.factionMyFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.myFactionClicked), "FactionPanelSideBar_my_faction");
                            this.factionMyFactionButton.CustomTooltipID = 0x92f;
                            this.backgroundImage.addControl(this.factionMyFactionButton);
                            y += num2;
                            this.factionDiplomacyButton.ImageNorm = (Image) GFXLibrary.faction_buttons[4];
                            this.factionDiplomacyButton.ImageOver = (Image) GFXLibrary.faction_buttons[5];
                            this.factionDiplomacyButton.Position = new Point(7, y);
                            this.factionDiplomacyButton.MoveOnClick = true;
                            this.factionDiplomacyButton.Text.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
                            this.factionDiplomacyButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.factionDiplomacyButton.Text.Position = new Point(0x54, 0);
                            this.factionDiplomacyButton.Text.Size = new Size(0x5e, 40);
                            this.factionDiplomacyButton.TextYOffset = -3;
                            this.factionDiplomacyButton.Text.Color = ARGBColors.Black;
                            this.factionDiplomacyButton.Active = true;
                            this.factionDiplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClicked), "FactionPanelSideBar_diplomacy");
                            this.factionDiplomacyButton.CustomTooltipID = 0x930;
                            this.backgroundImage.addControl(this.factionDiplomacyButton);
                            y += num2;
                            switch (num3)
                            {
                                case 2:
                                case 1:
                                    this.factionOfficersButton.ImageNorm = (Image) GFXLibrary.faction_buttons[6];
                                    this.factionOfficersButton.ImageOver = (Image) GFXLibrary.faction_buttons[7];
                                    this.factionOfficersButton.Position = new Point(7, y);
                                    this.factionOfficersButton.MoveOnClick = true;
                                    this.factionOfficersButton.Text.Text = SK.Text("FactionsSidebar_Officers", "Officers");
                                    this.factionOfficersButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                                    this.factionOfficersButton.Text.Position = new Point(0x54, 0);
                                    this.factionOfficersButton.Text.Size = new Size(0x5e, 40);
                                    this.factionOfficersButton.TextYOffset = -3;
                                    this.factionOfficersButton.Text.Color = ARGBColors.Black;
                                    this.factionOfficersButton.Active = true;
                                    this.factionOfficersButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.officersClicked), "FactionPanelSideBar_officers");
                                    this.factionOfficersButton.CustomTooltipID = 0x931;
                                    this.backgroundImage.addControl(this.factionOfficersButton);
                                    y += num2;
                                    break;
                            }
                            this.factionForumButton.ImageNorm = (Image) GFXLibrary.faction_buttons[8];
                            this.factionForumButton.ImageOver = (Image) GFXLibrary.faction_buttons[9];
                            this.factionForumButton.Position = new Point(7, y);
                            this.factionForumButton.MoveOnClick = true;
                            this.factionForumButton.Text.Text = SK.Text("FactionsSidebar_Forum", "Forum");
                            this.factionForumButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.factionForumButton.Text.Position = new Point(0x54, 0);
                            this.factionForumButton.Text.Size = new Size(0x5e, 40);
                            this.factionForumButton.TextYOffset = -3;
                            this.factionForumButton.Text.Color = ARGBColors.Black;
                            this.factionForumButton.Active = true;
                            this.factionForumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumClicked), "FactionPanelSideBar_forum");
                            this.factionForumButton.CustomTooltipID = 0x932;
                            this.backgroundImage.addControl(this.factionForumButton);
                            y += num2;
                        }
                        if ((num3 == 2) || (num3 == 1))
                        {
                            this.factionMailFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[10];
                            this.factionMailFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[11];
                            this.factionMailFactionButton.Position = new Point(7, y);
                            this.factionMailFactionButton.MoveOnClick = true;
                            this.factionMailFactionButton.Text.Text = SK.Text("FactionsPanel_MailFaction", "Mail To Faction");
                            this.factionMailFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.factionMailFactionButton.Text.Position = new Point(0x54, 0);
                            this.factionMailFactionButton.Text.Size = new Size(0x5e, 40);
                            this.factionMailFactionButton.TextYOffset = -3;
                            this.factionMailFactionButton.Text.Color = ARGBColors.Black;
                            this.factionMailFactionButton.Active = true;
                            this.factionMailFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailFactionClicked), "FactionPanelSideBar_mail_faction");
                            this.factionMailFactionButton.CustomTooltipID = 0x933;
                            this.backgroundImage.addControl(this.factionMailFactionButton);
                            y += num2;
                        }
                        this.factionInvitesButton.ImageNorm = (Image) GFXLibrary.faction_buttons[12];
                        this.factionInvitesButton.ImageOver = (Image) GFXLibrary.faction_buttons[13];
                        this.factionInvitesButton.Position = new Point(7, y);
                        this.factionInvitesButton.MoveOnClick = true;
                        this.factionInvitesButton.Text.Text = SK.Text("FactionsPanel_Users", "Invites");
                        this.factionInvitesButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                        this.factionInvitesButton.Text.Position = new Point(0x54, 0);
                        this.factionInvitesButton.Text.Size = new Size(0x5e, 40);
                        this.factionInvitesButton.TextYOffset = -3;
                        this.factionInvitesButton.Text.Color = ARGBColors.Black;
                        this.factionInvitesButton.Active = true;
                        this.factionInvitesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.invitesClicked), "FactionPanelSideBar_invites");
                        this.factionInvitesButton.CustomTooltipID = 0x934;
                        this.backgroundImage.addControl(this.factionInvitesButton);
                        y += num2;
                        if (RemoteServices.Instance.UserFactionID < 0)
                        {
                            this.factionStartFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[0x10];
                            this.factionStartFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[0x11];
                            this.factionStartFactionButton.Position = new Point(7, y);
                            this.factionStartFactionButton.MoveOnClick = true;
                            this.factionStartFactionButton.Text.Text = SK.Text("FactionsSidebar_Start_Faction", "Start a Faction");
                            this.factionStartFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.factionStartFactionButton.Text.Position = new Point(0x54, 0);
                            this.factionStartFactionButton.Text.Size = new Size(0x5e, 40);
                            this.factionStartFactionButton.TextYOffset = -3;
                            this.factionStartFactionButton.Text.Color = ARGBColors.Black;
                            this.factionStartFactionButton.Active = true;
                            this.factionStartFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.startFactionClicked), "FactionPanelSideBar_start_faction");
                            this.factionChatButton.CustomTooltipID = 0x936;
                            this.backgroundImage.addControl(this.factionStartFactionButton);
                            y += num2;
                            CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundImage, 0x17, new Point(this.backgroundImage.Width - 0x26, y + 5));
                        }
                        else
                        {
                            this.factionChatButton.ImageNorm = (Image) GFXLibrary.faction_buttons[14];
                            this.factionChatButton.ImageOver = (Image) GFXLibrary.faction_buttons[15];
                            this.factionChatButton.Position = new Point(7, y);
                            this.factionChatButton.MoveOnClick = true;
                            this.factionChatButton.Text.Text = SK.Text("GENERIC_Chat", "Chat");
                            this.factionChatButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.factionChatButton.Text.Position = new Point(0x54, 0);
                            this.factionChatButton.Text.Size = new Size(0x5e, 40);
                            this.factionChatButton.TextYOffset = -3;
                            this.factionChatButton.Text.Color = ARGBColors.Black;
                            this.factionChatButton.Active = true;
                            this.factionChatButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.chatClicked), "FactionPanelSideBar_chat");
                            this.factionChatButton.CustomTooltipID = 0x935;
                            this.backgroundImage.addControl(this.factionChatButton);
                            y += num2;
                            CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundImage, 0x17, new Point(this.backgroundImage.Width - 0x26, y + 5));
                            this.factionLeaveFactionButton.ImageNorm = (Image) GFXLibrary.faction_buttons[0x12];
                            this.factionLeaveFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[0x13];
                            this.factionLeaveFactionButton.Position = new Point(7, base.Height - 0x2d);
                            this.factionLeaveFactionButton.MoveOnClick = true;
                            this.factionLeaveFactionButton.Text.Text = SK.Text("FactionsPanel_Leave_Faction", "Leave Faction");
                            this.factionLeaveFactionButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.factionLeaveFactionButton.Text.Position = new Point(0x54, 0);
                            this.factionLeaveFactionButton.Text.Size = new Size(0x5e, 40);
                            this.factionLeaveFactionButton.TextYOffset = -3;
                            this.factionLeaveFactionButton.Text.Color = ARGBColors.Black;
                            this.factionLeaveFactionButton.Active = true;
                            this.factionLeaveFactionButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaveFactionClicked), "FactionPanelSideBar_leave_faction");
                            this.factionLeaveFactionButton.CustomTooltipID = 0x937;
                            this.backgroundImage.addControl(this.factionLeaveFactionButton);
                        }
                        break;
                    }
                    case 7:
                    case 8:
                    {
                        CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundImage, 0x18, new Point(this.backgroundImage.Width - 0x26, 0x34));
                        this.houseShowAllButton.ImageNorm = (Image) GFXLibrary.faction_buttons[20];
                        this.houseShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[0x15];
                        this.houseShowAllButton.Position = new Point(7, y);
                        this.houseShowAllButton.MoveOnClick = true;
                        this.houseShowAllButton.Text.Text = SK.Text("FactionsSidebar_Show_All", "Show All");
                        this.houseShowAllButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                        this.houseShowAllButton.Text.Position = new Point(0x54, 0);
                        this.houseShowAllButton.Text.Size = new Size(0x5e, 40);
                        this.houseShowAllButton.TextYOffset = -3;
                        this.houseShowAllButton.Text.Color = ARGBColors.Black;
                        this.houseShowAllButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showAllHousesClicked), "FactionPanelSideBar_all_houses");
                        this.houseShowAllButton.Active = true;
                        this.backgroundImage.addControl(this.houseShowAllButton);
                        y += num2 + 1;
                        int num4 = 1;
                        int x = 0x2f;
                        int num6 = 0x6b;
                        int num7 = -15;
                        int num8 = 0x19;
                        this.house1Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house1Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house1Button.Position = new Point(x, y);
                        this.house1Button.MoveOnClick = true;
                        this.house1Button.CustomTooltipID = 0x903;
                        this.house1Button.CustomTooltipData = num4;
                        this.house1Button.Data = num4++;
                        this.house1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house1Button.Active = true;
                        this.backgroundImage.addControl(this.house1Button);
                        y += num8;
                        this.house2Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house2Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house2Button.Position = new Point(num6, y + num7);
                        this.house2Button.MoveOnClick = true;
                        this.house2Button.CustomTooltipID = 0x903;
                        this.house2Button.CustomTooltipData = num4;
                        this.house2Button.Data = num4++;
                        this.house2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house2Button.Active = true;
                        this.backgroundImage.addControl(this.house2Button);
                        y += num8;
                        this.house3Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house3Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house3Button.Position = new Point(x, y);
                        this.house3Button.MoveOnClick = true;
                        this.house3Button.CustomTooltipID = 0x903;
                        this.house3Button.CustomTooltipData = num4;
                        this.house3Button.Data = num4++;
                        this.house3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house3Button.Active = true;
                        this.backgroundImage.addControl(this.house3Button);
                        y += num8;
                        this.house4Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house4Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house4Button.Position = new Point(num6, y + num7);
                        this.house4Button.MoveOnClick = true;
                        this.house4Button.CustomTooltipID = 0x903;
                        this.house4Button.CustomTooltipData = num4;
                        this.house4Button.Data = num4++;
                        this.house4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house4Button.Active = true;
                        this.backgroundImage.addControl(this.house4Button);
                        y += num8;
                        this.house5Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house5Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house5Button.Position = new Point(x, y);
                        this.house5Button.MoveOnClick = true;
                        this.house5Button.CustomTooltipID = 0x903;
                        this.house5Button.CustomTooltipData = num4;
                        this.house5Button.Data = num4++;
                        this.house5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house5Button.Active = true;
                        this.backgroundImage.addControl(this.house5Button);
                        y += num8;
                        this.house6Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house6Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house6Button.Position = new Point(num6, y + num7);
                        this.house6Button.MoveOnClick = true;
                        this.house6Button.CustomTooltipID = 0x903;
                        this.house6Button.CustomTooltipData = num4;
                        this.house6Button.Data = num4++;
                        this.house6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house6Button.Active = true;
                        this.backgroundImage.addControl(this.house6Button);
                        y += num8;
                        this.house7Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house7Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house7Button.Position = new Point(x, y);
                        this.house7Button.MoveOnClick = true;
                        this.house7Button.CustomTooltipID = 0x903;
                        this.house7Button.CustomTooltipData = num4;
                        this.house7Button.Data = num4++;
                        this.house7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house7Button.Active = true;
                        this.backgroundImage.addControl(this.house7Button);
                        y += num8;
                        this.house8Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house8Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house8Button.Position = new Point(num6, y + num7);
                        this.house8Button.MoveOnClick = true;
                        this.house8Button.CustomTooltipID = 0x903;
                        this.house8Button.CustomTooltipData = num4;
                        this.house8Button.Data = num4++;
                        this.house8Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house8Button.Active = true;
                        this.backgroundImage.addControl(this.house8Button);
                        y += num8;
                        this.house9Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house9Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house9Button.Position = new Point(x, y);
                        this.house9Button.MoveOnClick = true;
                        this.house9Button.CustomTooltipID = 0x903;
                        this.house9Button.CustomTooltipData = num4;
                        this.house9Button.Data = num4++;
                        this.house9Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house9Button.Active = true;
                        this.backgroundImage.addControl(this.house9Button);
                        y += num8;
                        this.house10Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house10Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house10Button.Position = new Point(num6, y + num7);
                        this.house10Button.MoveOnClick = true;
                        this.house10Button.CustomTooltipID = 0x903;
                        this.house10Button.CustomTooltipData = num4;
                        this.house10Button.Data = num4++;
                        this.house10Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house10Button.Active = true;
                        this.backgroundImage.addControl(this.house10Button);
                        y += num8;
                        this.house11Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house11Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house11Button.Position = new Point(x, y);
                        this.house11Button.MoveOnClick = true;
                        this.house11Button.CustomTooltipID = 0x903;
                        this.house11Button.CustomTooltipData = num4;
                        this.house11Button.Data = num4++;
                        this.house11Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house11Button.Active = true;
                        this.backgroundImage.addControl(this.house11Button);
                        y += num8;
                        this.house12Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house12Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house12Button.Position = new Point(num6, y + num7);
                        this.house12Button.MoveOnClick = true;
                        this.house12Button.CustomTooltipID = 0x903;
                        this.house12Button.CustomTooltipData = num4;
                        this.house12Button.Data = num4++;
                        this.house12Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house12Button.Active = true;
                        this.backgroundImage.addControl(this.house12Button);
                        y += num8;
                        this.house13Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house13Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house13Button.Position = new Point(x, y);
                        this.house13Button.MoveOnClick = true;
                        this.house13Button.CustomTooltipID = 0x903;
                        this.house13Button.CustomTooltipData = num4;
                        this.house13Button.Data = num4++;
                        this.house13Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house13Button.Active = true;
                        this.backgroundImage.addControl(this.house13Button);
                        y += num8;
                        this.house14Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house14Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house14Button.Position = new Point(num6, y + num7);
                        this.house14Button.MoveOnClick = true;
                        this.house14Button.CustomTooltipID = 0x903;
                        this.house14Button.CustomTooltipData = num4;
                        this.house14Button.Data = num4++;
                        this.house14Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house14Button.Active = true;
                        this.backgroundImage.addControl(this.house14Button);
                        y += num8;
                        this.house15Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house15Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house15Button.Position = new Point(x, y);
                        this.house15Button.MoveOnClick = true;
                        this.house15Button.CustomTooltipID = 0x903;
                        this.house15Button.CustomTooltipData = num4;
                        this.house15Button.Data = num4++;
                        this.house15Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house15Button.Active = true;
                        this.backgroundImage.addControl(this.house15Button);
                        y += num8;
                        this.house16Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house16Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house16Button.Position = new Point(num6, y + num7);
                        this.house16Button.MoveOnClick = true;
                        this.house16Button.CustomTooltipID = 0x903;
                        this.house16Button.CustomTooltipData = num4;
                        this.house16Button.Data = num4++;
                        this.house16Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house16Button.Active = true;
                        this.backgroundImage.addControl(this.house16Button);
                        y += num8;
                        this.house17Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house17Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house17Button.Position = new Point(x, y);
                        this.house17Button.MoveOnClick = true;
                        this.house17Button.CustomTooltipID = 0x903;
                        this.house17Button.CustomTooltipData = num4;
                        this.house17Button.Data = num4++;
                        this.house17Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house17Button.Active = true;
                        this.backgroundImage.addControl(this.house17Button);
                        y += num8;
                        this.house18Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house18Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house18Button.Position = new Point(num6, y + num7);
                        this.house18Button.MoveOnClick = true;
                        this.house18Button.CustomTooltipID = 0x903;
                        this.house18Button.CustomTooltipData = num4;
                        this.house18Button.Data = num4++;
                        this.house18Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house18Button.Active = true;
                        this.backgroundImage.addControl(this.house18Button);
                        y += num8;
                        this.house19Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house19Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house19Button.Position = new Point(x, y);
                        this.house19Button.MoveOnClick = true;
                        this.house19Button.CustomTooltipID = 0x903;
                        this.house19Button.CustomTooltipData = num4;
                        this.house19Button.Data = num4++;
                        this.house19Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house19Button.Active = true;
                        this.backgroundImage.addControl(this.house19Button);
                        y += num8;
                        this.house20Button.ImageNorm = (Image) GFXLibrary.house_circles_medium[num4 - 1];
                        this.house20Button.ImageOver = (Image) GFXLibrary.house_circles_medium[(num4 - 1) + 20];
                        this.house20Button.Position = new Point(num6, y + num7);
                        this.house20Button.MoveOnClick = true;
                        this.house20Button.CustomTooltipID = 0x903;
                        this.house20Button.CustomTooltipData = num4;
                        this.house20Button.Data = num4++;
                        this.house20Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectHouseClicked));
                        this.house20Button.Active = true;
                        this.backgroundImage.addControl(this.house20Button);
                        y += num8;
                        break;
                    }
                }
                int yourHouse = GameEngine.Instance.World.YourHouse;
                Point empty = Point.Empty;
                switch (yourHouse)
                {
                    case 1:
                        empty = this.house1Button.Position;
                        break;

                    case 2:
                        empty = this.house2Button.Position;
                        break;

                    case 3:
                        empty = this.house3Button.Position;
                        break;

                    case 4:
                        empty = this.house4Button.Position;
                        break;

                    case 5:
                        empty = this.house5Button.Position;
                        break;

                    case 6:
                        empty = this.house6Button.Position;
                        break;

                    case 7:
                        empty = this.house7Button.Position;
                        break;

                    case 8:
                        empty = this.house8Button.Position;
                        break;

                    case 9:
                        empty = this.house9Button.Position;
                        break;

                    case 10:
                        empty = this.house10Button.Position;
                        break;

                    case 11:
                        empty = this.house11Button.Position;
                        break;

                    case 12:
                        empty = this.house12Button.Position;
                        break;

                    case 13:
                        empty = this.house13Button.Position;
                        break;

                    case 14:
                        empty = this.house14Button.Position;
                        break;

                    case 15:
                        empty = this.house15Button.Position;
                        break;

                    case 0x10:
                        empty = this.house16Button.Position;
                        break;

                    case 0x11:
                        empty = this.house17Button.Position;
                        break;

                    case 0x12:
                        empty = this.house18Button.Position;
                        break;

                    case 0x13:
                        empty = this.house19Button.Position;
                        break;

                    case 20:
                        empty = this.house20Button.Position;
                        break;
                }
                if (!empty.IsEmpty)
                {
                    this.houseOverlay.Image = (Image) GFXLibrary.house_circles_medium_selected_top;
                    this.houseOverlay.Position = empty;
                    this.backgroundImage.addControl(this.houseOverlay);
                }
                this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background;
                switch (mode)
                {
                    case 0:
                        this.factionInvitesButton.Active = false;
                        this.factionInvitesButton.ImageOver = (Image) GFXLibrary.faction_buttons[12];
                        this.factionButtonBackground.Visible = true;
                        this.factionButtonBackground.Position = new Point(0, this.factionInvitesButton.Position.Y - 3);
                        goto Label_241F;

                    case 1:
                        if (FactionMyFactionPanel.SelectedFaction == RemoteServices.Instance.UserFactionID)
                        {
                            this.factionMyFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[2];
                            this.factionMyFactionButton.Active = false;
                            this.factionButtonBackground.Visible = true;
                            this.factionButtonBackground.Position = new Point(0, this.factionMyFactionButton.Position.Y - 3);
                        }
                        goto Label_241F;

                    case 2:
                        this.factionShowAllButton.Active = false;
                        this.factionShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[0];
                        this.factionButtonBackground.Visible = true;
                        this.factionButtonBackground.Position = new Point(0, this.factionShowAllButton.Position.Y - 3);
                        goto Label_241F;

                    case 3:
                        this.factionOfficersButton.Active = false;
                        this.factionOfficersButton.ImageOver = (Image) GFXLibrary.faction_buttons[6];
                        this.factionButtonBackground.Visible = true;
                        this.factionButtonBackground.Position = new Point(0, this.factionOfficersButton.Position.Y - 3);
                        goto Label_241F;

                    case 4:
                        this.factionDiplomacyButton.Active = false;
                        this.factionDiplomacyButton.ImageOver = (Image) GFXLibrary.faction_buttons[4];
                        this.factionButtonBackground.Visible = true;
                        this.factionButtonBackground.Position = new Point(0, this.factionDiplomacyButton.Position.Y - 3);
                        goto Label_241F;

                    case 5:
                        if (RemoteServices.Instance.UserFactionID < 0)
                        {
                            this.factionStartFactionButton.Active = false;
                            this.factionStartFactionButton.ImageOver = (Image) GFXLibrary.faction_buttons[0x10];
                            this.factionButtonBackground.Visible = true;
                            this.factionButtonBackground.Position = new Point(0, this.factionStartFactionButton.Position.Y - 3);
                        }
                        goto Label_241F;

                    case 6:
                        this.factionForumButton.Active = false;
                        this.factionForumButton.ImageOver = (Image) GFXLibrary.faction_buttons[8];
                        this.factionButtonBackground.Visible = true;
                        this.factionButtonBackground.Position = new Point(0, this.factionForumButton.Position.Y - 3);
                        goto Label_241F;

                    case 7:
                        this.houseShowAllButton.Active = false;
                        this.houseShowAllButton.ImageOver = (Image) GFXLibrary.faction_buttons[0];
                        this.factionButtonBackground.Visible = true;
                        this.factionButtonBackground.Position = new Point(0, this.houseShowAllButton.Position.Y - 3);
                        goto Label_241F;

                    case 8:
                        button = null;
                        switch (HouseInfoPanel.SelectedHouse)
                        {
                            case 1:
                                button = this.house1Button;
                                goto Label_23F9;

                            case 2:
                                button = this.house2Button;
                                goto Label_23F9;

                            case 3:
                                button = this.house3Button;
                                goto Label_23F9;

                            case 4:
                                button = this.house4Button;
                                goto Label_23F9;

                            case 5:
                                button = this.house5Button;
                                goto Label_23F9;

                            case 6:
                                button = this.house6Button;
                                goto Label_23F9;

                            case 7:
                                button = this.house7Button;
                                goto Label_23F9;

                            case 8:
                                button = this.house8Button;
                                goto Label_23F9;

                            case 9:
                                button = this.house9Button;
                                goto Label_23F9;

                            case 10:
                                button = this.house10Button;
                                goto Label_23F9;

                            case 11:
                                button = this.house11Button;
                                goto Label_23F9;

                            case 12:
                                button = this.house12Button;
                                goto Label_23F9;

                            case 13:
                                button = this.house13Button;
                                goto Label_23F9;

                            case 14:
                                button = this.house14Button;
                                goto Label_23F9;

                            case 15:
                                button = this.house15Button;
                                goto Label_23F9;

                            case 0x10:
                                button = this.house16Button;
                                goto Label_23F9;

                            case 0x11:
                                button = this.house17Button;
                                goto Label_23F9;

                            case 0x12:
                                button = this.house18Button;
                                goto Label_23F9;

                            case 0x13:
                                button = this.house19Button;
                                goto Label_23F9;

                            case 20:
                                button = this.house20Button;
                                goto Label_23F9;
                        }
                        break;

                    default:
                        goto Label_241F;
                }
            Label_23F9:
                if (button != null)
                {
                    button.ImageNorm = button.ImageHighlight = button.ImageOver;
                    button.Active = false;
                }
            Label_241F:
                if (this.factionButtonBackground.Position.Y < 10)
                {
                    this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background1;
                }
                else if (this.factionButtonBackground.Position.Y < 50)
                {
                    this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background2;
                }
                else if (this.factionButtonBackground.Position.Y < 90)
                {
                    this.factionButtonBackground.Image = (Image) GFXLibrary.faction_button_background3;
                }
            }

            private void chatClicked()
            {
                if (RemoteServices.Instance.UserFactionID >= 0)
                {
                    InterfaceMgr.Instance.initChatPanel(5, RemoteServices.Instance.UserFactionID);
                }
            }

            private void ClosePopUp()
            {
                if (this.leaveFactionPopup != null)
                {
                    if (this.leaveFactionPopup.Created)
                    {
                        this.leaveFactionPopup.Close();
                    }
                    InterfaceMgr.Instance.closeGreyOut();
                    this.leaveFactionPopup = null;
                }
            }

            private void diplomacyClicked()
            {
                InterfaceMgr.Instance.setVillageTabSubMode(0x2c, false);
            }

            public static void downloadCurrentFactionInfo()
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - m_lastFactionUpdate);
                if (span.TotalMinutes > 5.0)
                {
                    m_lastFactionUpdate = DateTime.Now;
                    RemoteServices.Instance.set_GetFactionData_UserCallBack(new RemoteServices.GetFactionData_UserCallBack(CustomSelfDrawPanel.FactionPanelSideBar.getFactionDataCallback));
                    RemoteServices.Instance.GetFactionData(RemoteServices.Instance.UserFactionID, GameEngine.Instance.World.StoredFactionChangesPos);
                }
            }

            public void factionLeaveCallback(FactionLeave_ReturnType returnData)
            {
                if (returnData.Success)
                {
                    RemoteServices.Instance.UserFactionID = -1;
                    GameEngine.Instance.World.FactionMembers = null;
                    GameEngine.Instance.World.FactionInvites = returnData.invites;
                    GameEngine.Instance.World.FactionApplications = returnData.applications;
                    GameEngine.Instance.World.FactionAllies = null;
                    GameEngine.Instance.World.FactionEnemies = null;
                    GameEngine.Instance.World.HouseAllies = null;
                    GameEngine.Instance.World.HouseEnemies = null;
                    GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
                    GameEngine.Instance.setNextFactionPage(-1);
                    InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
                }
            }

            public static void forceReUpdate()
            {
                m_lastFactionUpdate = DateTime.MinValue;
            }

            private void forumClicked()
            {
                InterfaceMgr.Instance.setVillageTabSubMode(0x2d, false);
            }

            public static void getFactionDataCallback(GetFactionData_ReturnType returnData)
            {
                if (returnData.Success)
                {
                    if (returnData.factionsList != null)
                    {
                        GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
                    }
                    GameEngine.Instance.World.FactionMembers = returnData.members;
                    GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                    GameEngine.Instance.World.FactionInvites = returnData.invites;
                    GameEngine.Instance.World.FactionApplications = returnData.applications;
                    GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
                    GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
                    GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
                    GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
                    GameEngine.Instance.World.HouseAllies = returnData.yourHouseAllies;
                    GameEngine.Instance.World.HouseEnemies = returnData.yourHouseEnemies;
                    GameEngine.Instance.World.YourFactionVote = returnData.yourLeaderVote;
                    m_factionDataUpdated = true;
                }
            }

            private void invitesClicked()
            {
                InterfaceMgr.Instance.setVillageTabSubMode(0x29, false);
            }

            private void LeaveFaction()
            {
                RemoteServices.Instance.set_FactionLeave_UserCallBack(new RemoteServices.FactionLeave_UserCallBack(this.factionLeaveCallback));
                RemoteServices.Instance.FactionLeave();
                InterfaceMgr.Instance.closeGreyOut();
                this.leaveFactionPopup.Close();
            }

            private void leaveFactionClicked()
            {
                this.ClosePopUp();
                InterfaceMgr.Instance.openGreyOutWindow(false);
                this.leaveFactionPopup = new MyMessageBoxPopUp();
                this.leaveFactionPopup.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FactionsPanel_Leave_Faction", "Leave Faction"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.LeaveFaction));
                this.leaveFactionPopup.Show(InterfaceMgr.Instance.getGreyOutWindow());
            }

            public static void logout()
            {
                m_lastFactionUpdate = DateTime.MinValue;
                m_factionDataUpdated = false;
            }

            public void mailFactionClicked()
            {
                FactionMemberData[] factionMembers = GameEngine.Instance.World.FactionMembers;
                if (factionMembers != null)
                {
                    List<string> list = new List<string>();
                    foreach (FactionMemberData data in factionMembers)
                    {
                        if ((data.userID != RemoteServices.Instance.UserID) && (data.status >= 0))
                        {
                            list.Add(data.userName);
                        }
                    }
                    MailScreen.setFromFaction();
                    InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x15);
                    InterfaceMgr.Instance.mailTo(RemoteServices.Instance.UserID, list.ToArray());
                }
            }

            private void myFactionClicked()
            {
                InterfaceMgr.Instance.showFactionPanel(RemoteServices.Instance.UserFactionID);
            }

            private void officersClicked()
            {
                InterfaceMgr.Instance.setVillageTabSubMode(0x2e, false);
            }

            public void selectHouseClicked()
            {
                GameEngine.Instance.playInterfaceSound("FactionPanelSideBar_house");
                int data = this.m_parent.ClickedControl.Data;
                InterfaceMgr.Instance.showHousePanel(data);
            }

            private void showAllClicked()
            {
                InterfaceMgr.Instance.setVillageTabSubMode(0x2b, false);
            }

            public void showAllHousesClicked()
            {
                InterfaceMgr.Instance.setVillageTabSubMode(0x33, false);
            }

            private void startFactionClicked()
            {
                InterfaceMgr.Instance.showStartFactionPanel();
            }

            public void update()
            {
                if (m_factionDataUpdated)
                {
                    m_factionDataUpdated = false;
                    if (this.m_parent != null)
                    {
                        if (m_currentSidebarMode == 2)
                        {
                            ((FactionAllFactionsPanel) this.m_parent).init(false);
                        }
                        else if (m_currentSidebarMode == 0)
                        {
                            ((FactionInvitePanel) this.m_parent).init(false);
                        }
                        else if (m_currentSidebarMode == 1)
                        {
                            ((FactionMyFactionPanel) this.m_parent).init(false);
                        }
                        else if (m_currentSidebarMode == 4)
                        {
                            ((FactionDiplomacyPanel) this.m_parent).init(false);
                        }
                    }
                }
            }
        }

        public interface ICardsPanel
        {
            void init(int cardsection);
            void update();
        }

        private class InvalidRectpair
        {
            public CustomSelfDrawPanel panel;
            public Rectangle rect;
        }

        public class MedalImage : CustomSelfDrawPanel.CSDControl
        {
            private int m_achievement;
            private int m_achievementRank;
            private CustomSelfDrawPanel.MedalWindow m_parent;
            private int m_rawAchievement;
            private CustomSelfDrawPanel.CSDImage medalImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage medalMetal = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage nail = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage ribbonBase = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage ribbonOverlay = new CustomSelfDrawPanel.CSDImage();

            private void achClicked()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.achievementClicked(this.m_rawAchievement);
                }
            }

            public static int getAchievementImage(int achievement)
            {
                switch (achievement)
                {
                    case 1:
                        return 4;

                    case 2:
                        return 5;

                    case 3:
                        return 6;

                    case 4:
                        return 7;

                    case 5:
                        return 8;

                    case 6:
                        return 0x31;

                    case 7:
                        return 50;

                    case 8:
                        return 0x33;

                    case 9:
                        return 0x34;

                    case 10:
                        return 10;

                    case 11:
                        return 11;

                    case 12:
                        return 12;

                    case 13:
                        return 13;

                    case 14:
                        return 14;

                    case 15:
                        return 0x38;

                    case 0x10:
                        return 0x39;

                    case 0x21:
                        return 15;

                    case 0x22:
                        return 0x10;

                    case 0x23:
                        return 0x11;

                    case 0x24:
                        return 0x12;

                    case 0x25:
                        return 0x13;

                    case 0x41:
                        return 20;

                    case 0x42:
                        return 0x15;

                    case 0x43:
                        return 0x16;

                    case 0x61:
                        return 0x17;

                    case 0x62:
                        return 0x18;

                    case 0x63:
                        return 0x19;

                    case 100:
                        return 0x1a;

                    case 0x65:
                        return 0x1b;

                    case 0x81:
                        return 0x1c;

                    case 130:
                        return 0x1d;

                    case 0x83:
                        return 30;

                    case 0xa1:
                        return 0x1f;

                    case 0xa2:
                        return 0x20;

                    case 0xa3:
                        return 0x21;

                    case 0xc1:
                        return 0x22;

                    case 0xc2:
                        return 0x23;

                    case 0xc3:
                        return 0x24;

                    case 0xe1:
                        return 0x25;

                    case 0xe2:
                        return 0x26;

                    case 0x101:
                        return 0x27;

                    case 0x121:
                        return 40;

                    case 290:
                        return 0x29;

                    case 0x161:
                        return 0x2b;

                    case 0x162:
                        return 0x2c;

                    case 0x141:
                        return 0x2a;

                    case 0x181:
                        return 0x2d;

                    case 0x182:
                        return 0x2e;

                    case 0x183:
                        return 0x2f;

                    case 0x184:
                        return 0x30;
                }
                return 0;
            }

            private int getRibbonColour(int achievement)
            {
                switch (achievement)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 0x10:
                        return 0;

                    case 0x21:
                    case 0x22:
                    case 0x23:
                    case 0x24:
                    case 0x25:
                        return 1;

                    case 0x41:
                    case 0x42:
                    case 0x43:
                        return 2;

                    case 0x61:
                    case 0x62:
                    case 0x63:
                    case 100:
                    case 0x65:
                        return 3;

                    case 0x81:
                    case 130:
                    case 0x83:
                        return 4;

                    case 0xa1:
                    case 0xa2:
                    case 0xa3:
                        return 5;

                    case 0xc1:
                    case 0xc2:
                    case 0xc3:
                        return 6;

                    case 0xe1:
                    case 0xe2:
                        return 7;

                    case 0x101:
                        return 8;

                    case 0x121:
                    case 290:
                        return 9;

                    case 0x161:
                    case 0x162:
                        return 11;

                    case 0x141:
                        return 10;

                    case 0x181:
                    case 0x182:
                    case 0x183:
                    case 0x184:
                        return 12;
                }
                return 0;
            }

            public void init(int achievement, CustomSelfDrawPanel.MedalWindow parent)
            {
                this.m_rawAchievement = achievement;
                this.m_parent = parent;
                int num = 0xbba;
                bool flag = true;
                if (achievement < 0)
                {
                    flag = false;
                    achievement = -achievement;
                    num = 0xbb9;
                }
                if ((parent != null) && parent.ownPlayer)
                {
                    num += 2;
                }
                this.m_achievement = achievement & 0xfff;
                int index = 0;
                switch ((achievement & 0x70000000))
                {
                    case 0x10000000:
                        this.m_achievementRank = 1;
                        index = 1;
                        break;

                    case 0x20000000:
                        this.m_achievementRank = 2;
                        index = 2;
                        break;

                    case 0x40000000:
                        this.m_achievementRank = 3;
                        index = 3;
                        break;

                    case 0x50000000:
                        this.m_achievementRank = 4;
                        index = 0x35;
                        break;

                    case 0x60000000:
                        this.m_achievementRank = 5;
                        index = 0x36;
                        break;

                    case 0x70000000:
                        this.m_achievementRank = 6;
                        index = 0x37;
                        break;

                    default:
                        this.m_achievementRank = 0;
                        index = 0;
                        break;
                }
                this.clearControls();
                Color white = ARGBColors.White;
                float num3 = 1f;
                if (!flag)
                {
                    white = Color.FromArgb(0x80, 0x80, 0x80, 0x80);
                    num3 = 0.7f;
                }
                this.Size = new Size(0x51, 110);
                int num4 = this.getRibbonColour(this.m_achievement);
                this.ribbonBase.Image = (Image) GFXLibrary.achievement_ribbons_base[num4];
                this.ribbonBase.Position = new Point(0, 0);
                this.ribbonBase.Colorise = white;
                this.ribbonBase.Alpha = num3;
                this.ribbonBase.CustomTooltipID = num;
                this.ribbonBase.CustomTooltipData = achievement;
                base.addControl(this.ribbonBase);
                if (this.m_achievementRank != 0)
                {
                    if (this.m_achievementRank == 1)
                    {
                        this.ribbonOverlay.Image = (Image) GFXLibrary.achievement_ribbons_edges[num4];
                    }
                    else if (this.m_achievementRank == 2)
                    {
                        this.ribbonOverlay.Image = (Image) GFXLibrary.achievement_ribbons_centre[num4];
                    }
                    else if (this.m_achievementRank >= 3)
                    {
                        this.ribbonOverlay.Image = (Image) GFXLibrary.ribbon_comp_centerstripe_gold;
                    }
                    this.ribbonOverlay.Position = new Point(0, 0);
                    this.ribbonOverlay.Colorise = white;
                    this.ribbonOverlay.Alpha = num3;
                    this.ribbonOverlay.CustomTooltipID = num;
                    this.ribbonOverlay.CustomTooltipData = achievement;
                    this.ribbonBase.addControl(this.ribbonOverlay);
                }
                this.nail.Image = (Image) GFXLibrary.ribbon_comp_nail;
                this.nail.Position = new Point(0, 0);
                this.nail.Colorise = white;
                this.nail.Alpha = num3;
                this.nail.CustomTooltipID = num;
                this.nail.CustomTooltipData = achievement;
                this.ribbonBase.addControl(this.nail);
                this.medalMetal.Image = (Image) GFXLibrary.medal_images[index];
                this.medalMetal.Position = new Point(8, 0x3a);
                this.medalMetal.Colorise = white;
                this.medalMetal.Alpha = num3;
                this.medalMetal.CustomTooltipID = num;
                this.medalMetal.CustomTooltipData = achievement;
                base.addControl(this.medalMetal);
                this.medalImage.Image = (Image) GFXLibrary.medal_images[getAchievementImage(this.m_achievement)];
                this.medalImage.Position = new Point(0, 0);
                this.medalImage.Colorise = white;
                this.medalImage.Alpha = num3;
                this.medalImage.CustomTooltipID = num;
                this.medalImage.CustomTooltipData = achievement;
                this.medalMetal.addControl(this.medalImage);
                if (this.m_rawAchievement >= 0)
                {
                    base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.achClicked));
                }
                base.invalidate();
            }
        }

        public class MedalWindow : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.MedalWindow _childWindow;
            private const int ach_area_x = 0;
            private const int ach_area_y = 30;
            private CustomSelfDrawPanel.CSDImage ach_bottom_overlay = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage ach_top_inset = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage ach_top_overlay = new CustomSelfDrawPanel.CSDImage();
            public static AchievementComparer achievementComparer = new AchievementComparer();
            private CustomSelfDrawPanel.CSDLabel achievementsLabel = new CustomSelfDrawPanel.CSDLabel();
            private static int[] activeAchievements = new int[] { 
                1, 2, 5, 11, 12, 13, 14, 0x22, 0x25, 0xa3, 0xe2, 0x101, 0x121, 4, 6, 7, 
                8, 9, 15, 0x10, 10, 3, 0x41, 0x42, 0x43, 0x81, 0x83, 130, 290, 0xa2, 0xa1, 0x162, 
                0x161, 0x65, 100, 0x184, 0x182, 0x183, 0x181, 0xe1, 0xc2, 0xc3, 0x141
             };
            private CustomSelfDrawPanel.CSDButton facebookShareButton = new CustomSelfDrawPanel.CSDButton();
            private int fb_achievement;
            private string fb_caption = "";
            private string fb_title = "";
            private CustomSelfDrawPanel.MedalImage medal1 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal10 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal11 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal12 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal13 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal14 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal15 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal16 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal17 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal18 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal19 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal2 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal20 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal21 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal22 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal23 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal24 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal25 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal26 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal27 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal28 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal29 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal3 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal30 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal31 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal32 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal33 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal34 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal35 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal36 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal37 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal38 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal39 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal4 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal40 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal41 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal42 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal43 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal44 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal45 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal5 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal6 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal7 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal8 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.MedalImage medal9 = new CustomSelfDrawPanel.MedalImage();
            private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
            public bool ownPlayer = true;
            private CustomSelfDrawPanel.CSDImage popupOverlayImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDArea scrollArea = new CustomSelfDrawPanel.CSDArea();
            private CustomSelfDrawPanel.CSDArea scrollArea2 = new CustomSelfDrawPanel.CSDArea();
            private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
            private CustomSelfDrawPanel.CSDImage scrollImage = new CustomSelfDrawPanel.CSDImage();

            public void achievementClicked(int achievement)
            {
                if (this._childWindow != null)
                {
                    List<int> earnedAchievements = new List<int> {
                        achievement
                    };
                    this._childWindow.init(earnedAchievements, false, false, -150);
                    this._childWindow.Visible = true;
                }
            }

            private void facebookShareClicked()
            {
                string str = string.Concat(new object[] { "http://login.strongholdkingdoms.com/facebook/js_share.php?u=", RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "&eventid=", this.fb_achievement, "&desc=", this.fb_title, "&lang=", Program.mySettings.LanguageIdent, "&worldid=", Program.mySettings.LastWorldID, "&caption=", this.fb_caption });
                try
                {
                    StatTrackingClient.Instance().ActivateTrigger(30, 0);
                    new Process { StartInfo = { FileName = str } }.Start();
                }
                catch (Exception)
                {
                }
            }

            public static int getAchievementRanking(int achievement)
            {
                switch ((achievement & 0xfffffff))
                {
                    case 1:
                        return 4;

                    case 2:
                        return 8;

                    case 3:
                        return 13;

                    case 4:
                        return 14;

                    case 5:
                        return 0x11;

                    case 6:
                        return 0x13;

                    case 7:
                        return 0x16;

                    case 8:
                        return 0x1c;

                    case 9:
                        return 0x21;

                    case 10:
                        return 0x1b;

                    case 11:
                        return 0x1c;

                    case 12:
                        return 0x19;

                    case 13:
                        return 0x13;

                    case 14:
                        return 0x16;

                    case 15:
                        return 0x1d;

                    case 0x10:
                        return 30;

                    case 0x21:
                        return 10;

                    case 0x22:
                        return 0x10;

                    case 0x23:
                        return 0;

                    case 0x24:
                        return 0;

                    case 0x25:
                        return 15;

                    case 0x41:
                        return 0x1c;

                    case 0x42:
                        return 0x19;

                    case 0x43:
                        return 0x18;

                    case 0x61:
                        return 7;

                    case 0x62:
                        return 8;

                    case 0x63:
                        return 0x18;

                    case 100:
                        return 0x17;

                    case 0x65:
                        return 0x13;

                    case 0x81:
                        return 0x1a;

                    case 130:
                        return 0x21;

                    case 0x83:
                        return 0x16;

                    case 0xa1:
                        return 5;

                    case 0xa2:
                        return 3;

                    case 0xa3:
                        return 11;

                    case 0xc1:
                        return 0x1f;

                    case 0xc2:
                        return 0x15;

                    case 0xc3:
                        return 0x10;

                    case 0xe1:
                        return 0x2e;

                    case 0xe2:
                        return 9;

                    case 0x101:
                        return 14;

                    case 0x121:
                        return 15;

                    case 290:
                        return 0x12;

                    case 0x161:
                        return 20;

                    case 0x162:
                        return 0x20;

                    case 0x141:
                        return 0;

                    case 0x181:
                        return 30;

                    case 0x182:
                        return 40;

                    case 0x183:
                        return 0x2d;

                    case 0x184:
                        return 50;
                }
                return 0;
            }

            public void init(List<int> earnedAchievements, bool addUnearned, bool popupOverlay, int heightDiff)
            {
                this.ownPlayer = !popupOverlay;
                this.clearControls();
                this.Size = new Size(0x1db, 350);
                this.scrollArea.Position = new Point(7, 30);
                this.scrollArea.Size = new Size(0x1db, 0x7d0);
                this.scrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x1db, 0x131 + heightDiff));
                base.addControl(this.scrollArea);
                this.scrollImage.Image = (Image) GFXLibrary.achievement_woodback_middletile;
                this.scrollImage.Size = this.scrollArea.Size;
                this.scrollImage.Tile = true;
                this.scrollImage.Position = new Point(0, 0);
                this.scrollArea.addControl(this.scrollImage);
                this.scrollArea2.Position = new Point(0, 0);
                this.scrollArea2.Size = this.scrollImage.Size;
                this.scrollImage.addControl(this.scrollArea2);
                this.mouseWheelOverlay.Position = this.scrollArea.Position;
                this.mouseWheelOverlay.Size = this.scrollArea.Size;
                this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
                base.addControl(this.mouseWheelOverlay);
                this.scrollBar.Position = new Point(0x1b4, 0x37);
                this.scrollBar.Size = new Size(0x20, 0x113 + heightDiff);
                base.addControl(this.scrollBar);
                this.scrollBar.Value = 0;
                this.scrollBar.Max = (920 - (0x131 + heightDiff)) + 20;
                this.scrollBar.NumVisibleLines = 0x131 + heightDiff;
                this.scrollBar.Create(null, null, null, (Image) GFXLibrary.scroll_thumb_top, (Image) GFXLibrary.scroll_thumb_mid, (Image) GFXLibrary.scroll_thumb_bottom);
                this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
                this.ach_top_overlay.Image = (Image) GFXLibrary.panel_cover_top;
                this.ach_top_overlay.Position = new Point(0, 0);
                base.addControl(this.ach_top_overlay);
                this.ach_bottom_overlay.Image = (Image) GFXLibrary.panel_cover_bottom;
                this.ach_bottom_overlay.Position = new Point(0, (30 + (0x131 + heightDiff)) - 0x2d);
                base.addControl(this.ach_bottom_overlay);
                if (popupOverlay)
                {
                    this.popupOverlayImage.Image = (Image) GFXLibrary.char_achievementOverlay;
                    this.popupOverlayImage.Position = new Point(0, 0);
                    base.addControl(this.popupOverlayImage);
                }
                List<int> list = processAchievements(earnedAchievements, addUnearned);
                int x = 0x19;
                int y = 10;
                int num3 = 80;
                int num4 = 0x73;
                if (list.Count > 0)
                {
                    this.medal1.init(list[0], this);
                    this.medal1.Position = new Point(x, y);
                    this.scrollArea2.addControl(this.medal1);
                }
                if (list.Count > 1)
                {
                    this.medal2.init(list[1], this);
                    this.medal2.Position = new Point(x + num3, y);
                    this.scrollArea2.addControl(this.medal2);
                }
                if (list.Count > 2)
                {
                    this.medal3.init(list[2], this);
                    this.medal3.Position = new Point(x + (num3 * 2), y);
                    this.scrollArea2.addControl(this.medal3);
                }
                if (list.Count > 3)
                {
                    this.medal4.init(list[3], this);
                    this.medal4.Position = new Point(x + (num3 * 3), y);
                    this.scrollArea2.addControl(this.medal4);
                }
                if (list.Count > 4)
                {
                    this.medal5.init(list[4], this);
                    this.medal5.Position = new Point(x + (num3 * 4), y);
                    this.scrollArea2.addControl(this.medal5);
                }
                if (list.Count > 5)
                {
                    this.medal6.init(list[5], this);
                    this.medal6.Position = new Point(x, y + num4);
                    this.scrollArea2.addControl(this.medal6);
                }
                if (list.Count > 6)
                {
                    this.medal7.init(list[6], this);
                    this.medal7.Position = new Point(x + num3, y + num4);
                    this.scrollArea2.addControl(this.medal7);
                }
                if (list.Count > 7)
                {
                    this.medal8.init(list[7], this);
                    this.medal8.Position = new Point(x + (num3 * 2), y + num4);
                    this.scrollArea2.addControl(this.medal8);
                }
                if (list.Count > 8)
                {
                    this.medal9.init(list[8], this);
                    this.medal9.Position = new Point(x + (num3 * 3), y + num4);
                    this.scrollArea2.addControl(this.medal9);
                }
                if (list.Count > 9)
                {
                    this.medal10.init(list[9], this);
                    this.medal10.Position = new Point(x + (num3 * 4), y + num4);
                    this.scrollArea2.addControl(this.medal10);
                }
                if (list.Count > 10)
                {
                    this.medal11.init(list[10], this);
                    this.medal11.Position = new Point(x, y + (num4 * 2));
                    this.scrollArea2.addControl(this.medal11);
                }
                if (list.Count > 11)
                {
                    this.medal12.init(list[11], this);
                    this.medal12.Position = new Point(x + num3, y + (num4 * 2));
                    this.scrollArea2.addControl(this.medal12);
                }
                if (list.Count > 12)
                {
                    this.medal13.init(list[12], this);
                    this.medal13.Position = new Point(x + (num3 * 2), y + (num4 * 2));
                    this.scrollArea2.addControl(this.medal13);
                }
                if (list.Count > 13)
                {
                    this.medal14.init(list[13], this);
                    this.medal14.Position = new Point(x + (num3 * 3), y + (num4 * 2));
                    this.scrollArea2.addControl(this.medal14);
                }
                if (list.Count > 14)
                {
                    this.medal15.init(list[14], this);
                    this.medal15.Position = new Point(x + (num3 * 4), y + (num4 * 2));
                    this.scrollArea2.addControl(this.medal15);
                }
                if (list.Count > 15)
                {
                    this.medal16.init(list[15], this);
                    this.medal16.Position = new Point(x, y + (num4 * 3));
                    this.scrollArea2.addControl(this.medal16);
                }
                if (list.Count > 0x10)
                {
                    this.medal17.init(list[0x10], this);
                    this.medal17.Position = new Point(x + num3, y + (num4 * 3));
                    this.scrollArea2.addControl(this.medal17);
                }
                if (list.Count > 0x11)
                {
                    this.medal18.init(list[0x11], this);
                    this.medal18.Position = new Point(x + (num3 * 2), y + (num4 * 3));
                    this.scrollArea2.addControl(this.medal18);
                }
                if (list.Count > 0x12)
                {
                    this.medal19.init(list[0x12], this);
                    this.medal19.Position = new Point(x + (num3 * 3), y + (num4 * 3));
                    this.scrollArea2.addControl(this.medal19);
                }
                if (list.Count > 0x13)
                {
                    this.medal20.init(list[0x13], this);
                    this.medal20.Position = new Point(x + (num3 * 4), y + (num4 * 3));
                    this.scrollArea2.addControl(this.medal20);
                }
                if (list.Count > 20)
                {
                    this.medal21.init(list[20], this);
                    this.medal21.Position = new Point(x, y + (num4 * 4));
                    this.scrollArea2.addControl(this.medal21);
                }
                if (list.Count > 0x15)
                {
                    this.medal22.init(list[0x15], this);
                    this.medal22.Position = new Point(x + num3, y + (num4 * 4));
                    this.scrollArea2.addControl(this.medal22);
                }
                if (list.Count > 0x16)
                {
                    this.medal23.init(list[0x16], this);
                    this.medal23.Position = new Point(x + (num3 * 2), y + (num4 * 4));
                    this.scrollArea2.addControl(this.medal23);
                }
                if (list.Count > 0x17)
                {
                    this.medal24.init(list[0x17], this);
                    this.medal24.Position = new Point(x + (num3 * 3), y + (num4 * 4));
                    this.scrollArea2.addControl(this.medal24);
                }
                if (list.Count > 0x18)
                {
                    this.medal25.init(list[0x18], this);
                    this.medal25.Position = new Point(x + (num3 * 4), y + (num4 * 4));
                    this.scrollArea2.addControl(this.medal25);
                }
                if (list.Count > 0x19)
                {
                    this.medal26.init(list[0x19], this);
                    this.medal26.Position = new Point(x, y + (num4 * 5));
                    this.scrollArea2.addControl(this.medal26);
                }
                if (list.Count > 0x1a)
                {
                    this.medal27.init(list[0x1a], this);
                    this.medal27.Position = new Point(x + num3, y + (num4 * 5));
                    this.scrollArea2.addControl(this.medal27);
                }
                if (list.Count > 0x1b)
                {
                    this.medal28.init(list[0x1b], this);
                    this.medal28.Position = new Point(x + (num3 * 2), y + (num4 * 5));
                    this.scrollArea2.addControl(this.medal28);
                }
                if (list.Count > 0x1c)
                {
                    this.medal29.init(list[0x1c], this);
                    this.medal29.Position = new Point(x + (num3 * 3), y + (num4 * 5));
                    this.scrollArea2.addControl(this.medal29);
                }
                if (list.Count > 0x1d)
                {
                    this.medal30.init(list[0x1d], this);
                    this.medal30.Position = new Point(x + (num3 * 4), y + (num4 * 5));
                    this.scrollArea2.addControl(this.medal30);
                }
                if (list.Count > 30)
                {
                    this.medal31.init(list[30], this);
                    this.medal31.Position = new Point(x, y + (num4 * 6));
                    this.scrollArea2.addControl(this.medal31);
                }
                if (list.Count > 0x1f)
                {
                    this.medal32.init(list[0x1f], this);
                    this.medal32.Position = new Point(x + num3, y + (num4 * 6));
                    this.scrollArea2.addControl(this.medal32);
                }
                if (list.Count > 0x20)
                {
                    this.medal33.init(list[0x20], this);
                    this.medal33.Position = new Point(x + (num3 * 2), y + (num4 * 6));
                    this.scrollArea2.addControl(this.medal33);
                }
                if (list.Count > 0x21)
                {
                    this.medal34.init(list[0x21], this);
                    this.medal34.Position = new Point(x + (num3 * 3), y + (num4 * 6));
                    this.scrollArea2.addControl(this.medal34);
                }
                if (list.Count > 0x22)
                {
                    this.medal35.init(list[0x22], this);
                    this.medal35.Position = new Point(x + (num3 * 4), y + (num4 * 6));
                    this.scrollArea2.addControl(this.medal35);
                }
                if (list.Count > 0x23)
                {
                    this.medal36.init(list[0x23], this);
                    this.medal36.Position = new Point(x, y + (num4 * 7));
                    this.scrollArea2.addControl(this.medal36);
                }
                if (list.Count > 0x24)
                {
                    this.medal37.init(list[0x24], this);
                    this.medal37.Position = new Point(x + num3, y + (num4 * 7));
                    this.scrollArea2.addControl(this.medal37);
                }
                if (list.Count > 0x25)
                {
                    this.medal38.init(list[0x25], this);
                    this.medal38.Position = new Point(x + (num3 * 2), y + (num4 * 7));
                    this.scrollArea2.addControl(this.medal38);
                }
                if (list.Count > 0x26)
                {
                    this.medal39.init(list[0x26], this);
                    this.medal39.Position = new Point(x + (num3 * 3), y + (num4 * 7));
                    this.scrollArea2.addControl(this.medal39);
                }
                if (list.Count > 0x27)
                {
                    this.medal40.init(list[0x27], this);
                    this.medal40.Position = new Point(x + (num3 * 4), y + (num4 * 7));
                    this.scrollArea2.addControl(this.medal40);
                }
                if (list.Count > 40)
                {
                    this.medal41.init(list[40], this);
                    this.medal41.Position = new Point(x, y + (num4 * 8));
                    this.scrollArea2.addControl(this.medal41);
                }
                if (list.Count > 0x29)
                {
                    this.medal42.init(list[0x29], this);
                    this.medal42.Position = new Point(x + num3, y + (num4 * 8));
                    this.scrollArea2.addControl(this.medal42);
                }
                if (list.Count > 0x2a)
                {
                    this.medal43.init(list[0x2a], this);
                    this.medal43.Position = new Point(x + (num3 * 2), y + (num4 * 8));
                    this.scrollArea2.addControl(this.medal43);
                }
                if (list.Count > 0x2b)
                {
                    this.medal44.init(list[0x2b], this);
                    this.medal44.Position = new Point(x + (num3 * 3), y + (num4 * 8));
                    this.scrollArea2.addControl(this.medal44);
                }
                if (list.Count > 0x2c)
                {
                    this.medal45.init(list[0x2c], this);
                    this.medal45.Position = new Point(x + (num3 * 4), y + (num4 * 8));
                    this.scrollArea2.addControl(this.medal45);
                }
                int num5 = (list.Count + 4) / 5;
                int num6 = ((num5 * 0x73) - 0x131) + 20;
                if (num6 < 0)
                {
                    num6 = 0;
                    this.scrollBar.Visible = false;
                }
                else
                {
                    this.scrollBar.Visible = true;
                }
                this.scrollBar.Max = num6;
                if (heightDiff == 0)
                {
                    this.achievementsLabel.Text = SK.Text("GENERIC_Achievements", "Achievements");
                    this.achievementsLabel.Position = new Point(0, 0);
                    this.achievementsLabel.Size = new Size(this.ach_top_overlay.Width, 30);
                    this.achievementsLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                    this.achievementsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    if (!popupOverlay)
                    {
                        this.achievementsLabel.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                        this.achievementsLabel.DropShadowColor = Color.FromArgb(0x38, 50, 0x24);
                        this.ach_top_overlay.addControl(this.achievementsLabel);
                    }
                    else
                    {
                        this.achievementsLabel.Color = ARGBColors.White;
                        this.achievementsLabel.DropShadowColor = ARGBColors.Black;
                        this.popupOverlayImage.addControl(this.achievementsLabel);
                    }
                }
                else if (earnedAchievements.Count == 1)
                {
                    int achievement = earnedAchievements[0];
                    int num8 = achievement & 0xfff;
                    int rankLevel = 0;
                    string str = CustomTooltipManager.getAchievementRank(achievement);
                    switch ((achievement & 0x70000000))
                    {
                        case 0x10000000:
                            rankLevel = 1;
                            break;

                        case 0x20000000:
                            rankLevel = 2;
                            break;

                        case 0x40000000:
                            rankLevel = 3;
                            break;

                        case 0x50000000:
                            rankLevel = 4;
                            break;

                        case 0x60000000:
                            rankLevel = 5;
                            break;

                        case 0x70000000:
                            rankLevel = 6;
                            break;

                        default:
                            rankLevel = 0;
                            break;
                    }
                    string str2 = CustomTooltipManager.getAchievementTitle(num8);
                    this.fb_title = str2;
                    string str3 = CustomTooltipManager.getAchievementRequirement(num8, rankLevel);
                    this.fb_caption = str3;
                    string str4 = str2 + Environment.NewLine + str + Environment.NewLine + str3;
                    this.achievementsLabel.Text = str4;
                    this.achievementsLabel.Position = new Point(0x69, 0x2d);
                    this.achievementsLabel.Size = new Size(350, 110);
                    this.achievementsLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                    this.achievementsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.achievementsLabel.Color = ARGBColors.White;
                    this.ach_top_overlay.addControl(this.achievementsLabel);
                    this.fb_achievement = achievement + 0x3e8;
                    this.facebookShareButton.ImageNorm = (Image) GFXLibrary.facebookBrownNorm;
                    this.facebookShareButton.ImageOver = (Image) GFXLibrary.facebookBrownOver;
                    this.facebookShareButton.ImageClick = (Image) GFXLibrary.facebookBrownClick;
                    this.facebookShareButton.Position = new Point(0x149, 0x9b);
                    this.facebookShareButton.UseTextSize = true;
                    this.facebookShareButton.Text.Text = SK.Text("FACEBOOK_Share", "Share");
                    this.facebookShareButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.facebookShareButton.Text.Position = new Point(20, 2);
                    this.facebookShareButton.Text.Size = new Size(110, 0x15);
                    this.facebookShareButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.facebookShareButton.TextYOffset = 0;
                    this.facebookShareButton.Text.Color = ARGBColors.Black;
                    this.facebookShareButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.facebookShareClicked));
                    this.ach_top_overlay.addControl(this.facebookShareButton);
                }
            }

            private void mouseWheelMoved(int delta)
            {
                if (delta < 0)
                {
                    this.scrollBar.scrollDown(6);
                }
                else if (delta > 0)
                {
                    this.scrollBar.scrollUp(6);
                }
            }

            public static List<int> processAchievements(List<int> achievements, bool addUnEarned)
            {
                if (achievements == null)
                {
                    achievements = new List<int>();
                }
                List<int> list = new List<int>();
                foreach (int num in achievements)
                {
                    int num2 = num & 0xfffffff;
                    int num3 = num & 0x70000000;
                    bool flag = false;
                    for (int i = 0; i < list.Count; i++)
                    {
                        int num5 = list[i];
                        int num6 = num5 & 0xfffffff;
                        if (num6 == num2)
                        {
                            int num7 = num5 & 0x70000000;
                            if (num3 > num7)
                            {
                                list[i] = num;
                            }
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        list.Add(num);
                    }
                }
                list.Sort(achievementComparer);
                if (addUnEarned)
                {
                    List<int> list2 = new List<int>();
                    foreach (int num8 in list)
                    {
                        int item = num8 & 0xfffffff;
                        list2.Add(item);
                    }
                    List<int> collection = new List<int>();
                    foreach (int num10 in activeAchievements)
                    {
                        if (!list2.Contains(num10))
                        {
                            collection.Add(num10);
                        }
                    }
                    if (collection.Count > 1)
                    {
                        collection.Sort(achievementComparer);
                        collection.Reverse();
                    }
                    for (int j = 0; j < collection.Count; j++)
                    {
                        collection[j] = -collection[j];
                    }
                    list.AddRange(collection);
                }
                return list;
            }

            private void scrollBarMoved()
            {
                int y = this.scrollBar.Value;
                this.scrollArea.Position = new Point(this.scrollArea.X, 30 - y);
                this.scrollArea.ClipRect = new Rectangle(this.scrollArea.ClipRect.X, y, this.scrollArea.ClipRect.Width, this.scrollArea.ClipRect.Height);
                this.scrollArea.invalidate();
            }

            public void setChildWindow(CustomSelfDrawPanel.MedalWindow child)
            {
                this._childWindow = child;
            }

            public class AchievementComparer : IComparer<int>
            {
                public int Compare(int x, int y)
                {
                    int num = x & 0x70000000;
                    int num2 = y & 0x70000000;
                    if (num < num2)
                    {
                        return 1;
                    }
                    if (num > num2)
                    {
                        return -1;
                    }
                    int achievement = x & 0xfffffff;
                    int num4 = y & 0xfffffff;
                    int num5 = CustomSelfDrawPanel.MedalWindow.getAchievementRanking(achievement);
                    int num6 = CustomSelfDrawPanel.MedalWindow.getAchievementRanking(num4);
                    if (num5 < num6)
                    {
                        return 1;
                    }
                    if (num5 > num6)
                    {
                        return -1;
                    }
                    return 0;
                }
            }
        }

        public class MRHP_Background : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage actionIcon = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage avatarUnderlayImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage backGroundImage = new CustomSelfDrawPanel.CSDImage();
            public const int HEADER_TYPE_ATTACK = 0x3e8;
            public const int HEADER_TYPE_CHARTER = 0x5e1;
            public const int HEADER_TYPE_COUNTRY = 0x5df;
            public const int HEADER_TYPE_COUNTY = 0x5dd;
            public const int HEADER_TYPE_FILTER = 0x5e2;
            public const int HEADER_TYPE_MONK = 0x3e9;
            public const int HEADER_TYPE_NONE = 0x2710;
            public const int HEADER_TYPE_PARISH = 0x5dc;
            public const int HEADER_TYPE_PARISH_PLAGUE = 0x5e0;
            public const int HEADER_TYPE_PROVINCE = 0x5de;
            public const int HEADER_TYPE_RAT = 0x3ee;
            public const int HEADER_TYPE_REINFORCEMENT = 0x3eb;
            public const int HEADER_TYPE_SCOUT = 0x3ea;
            public const int HEADER_TYPE_TERRAIN_FOREST = 0x7d9;
            public const int HEADER_TYPE_TERRAIN_HIGHLAND = 0x7d1;
            public const int HEADER_TYPE_TERRAIN_LOWLAND = 0x7d0;
            public const int HEADER_TYPE_TERRAIN_MARSH = 0x7d6;
            public const int HEADER_TYPE_TERRAIN_MOUNTAIN_PEAK = 0x7d2;
            public const int HEADER_TYPE_TERRAIN_PLAINS = 0x7d7;
            public const int HEADER_TYPE_TERRAIN_RIVER1 = 0x7d3;
            public const int HEADER_TYPE_TERRAIN_RIVER2 = 0x7d4;
            public const int HEADER_TYPE_TERRAIN_SALT = 0x7d5;
            public const int HEADER_TYPE_TERRAIN_VALLEY = 0x7d8;
            public const int HEADER_TYPE_TRADE = 0x3ec;
            public const int HEADER_TYPE_VASSAL = 0x3ed;
            private CustomSelfDrawPanel.CSDImage headerGlowLong = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage headerGlowSmall = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage headerIcon = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage headerImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel headingLabel = new CustomSelfDrawPanel.CSDLabel();
            private int headingVillageID = -1;
            private int LastVillageType;
            private CustomSelfDrawPanel.CSDLabel panelLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel subHeadingLabel = new CustomSelfDrawPanel.CSDLabel();

            public CustomSelfDrawPanel.WikiLinkControl addWikiLink(int id)
            {
                return CustomSelfDrawPanel.WikiLinkControl.init(this.headerImage, id, new Point(150, 0x15));
            }

            public void centerSubHeading()
            {
                this.headerGlowLong.Position = new Point(((this.backGroundImage.Image.Width - this.headerGlowLong.Image.Width) / 2) - 20, 10);
                this.headerGlowLong.Size = new Size(this.headerGlowLong.Image.Width + 40, this.headerGlowLong.Height);
                this.subHeadingLabel.Size = new Size(this.headerGlowLong.Size.Width - 0x18, 20);
            }

            private void headingClicked()
            {
                if (this.headingVillageID >= 0)
                {
                    GameEngine.Instance.World.zoomToVillage(this.headingVillageID);
                }
            }

            public void hideBackground()
            {
                this.backGroundImage.Size = new Size(1, 1);
            }

            public CustomSelfDrawPanel.CSDImage init(bool tall, int villageBackgroundType)
            {
                return this.init(tall, villageBackgroundType, "", "", "");
            }

            public CustomSelfDrawPanel.CSDImage init(bool tall, int villageBackgroundType, string heading, string subHeading, string panelText)
            {
                this.headingVillageID = -1;
                this.Size = new Size(0xc7, 0xd5);
                this.clearControls();
                this.avatarUnderlayImage.Image = (Image) GFXLibrary.mrhp_avatar_frame;
                this.avatarUnderlayImage.Position = new Point(0, 0xb6);
                this.avatarUnderlayImage.ClipRect = new Rectangle(0, 0, 200, 0x1f);
                base.addControl(this.avatarUnderlayImage);
                if (!tall)
                {
                    this.backGroundImage.Image = (Image) GFXLibrary.mrhp_world_panel_102;
                }
                else
                {
                    this.backGroundImage.Image = (Image) GFXLibrary.mrhp_world_panel_192;
                }
                this.backGroundImage.Position = new Point(6, 20);
                base.addControl(this.backGroundImage);
                this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                this.headerImage.Position = new Point(-1, -17);
                this.backGroundImage.addControl(this.headerImage);
                this.headerGlowLong.Image = (Image) GFXLibrary.mrhp_location_portrait_glow_long;
                this.headerGlowLong.Position = new Point(0x2d, 10);
                this.headerImage.addControl(this.headerGlowLong);
                this.headerGlowSmall.Image = (Image) GFXLibrary.mrhp_location_portrait_glow_short;
                this.headerGlowSmall.Position = new Point(0, -9);
                this.headerImage.addControl(this.headerGlowSmall);
                this.headerIcon.Image = (Image) GFXLibrary.wl_moving_unit_icons[0];
                this.headerIcon.Position = new Point(0x11, 0x1a);
                this.headerGlowSmall.addControl(this.headerIcon);
                this.actionIcon.Image = (Image) GFXLibrary.wl_moving_unit_icons[0];
                this.actionIcon.Position = new Point(0x8d, 0x11);
                this.actionIcon.Visible = false;
                this.headerImage.addControl(this.actionIcon);
                this.headingLabel.Text = "";
                this.headingLabel.Color = ARGBColors.Black;
                this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.headingLabel.Position = new Point(14, 5);
                this.headingLabel.Size = new Size(0xa8, 0x17);
                this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.headingLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.headingClicked), "MRHP_Background_heading");
                this.headerImage.addControl(this.headingLabel);
                this.subHeadingLabel.Text = "";
                this.subHeadingLabel.Color = ARGBColors.Black;
                this.subHeadingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.subHeadingLabel.Position = new Point(12, 0x12);
                this.subHeadingLabel.Size = new Size(0x84, 20);
                this.subHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.headerGlowLong.addControl(this.subHeadingLabel);
                this.panelLabel.Text = "";
                this.panelLabel.Color = ARGBColors.Black;
                this.panelLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.panelLabel.Position = new Point(0, 0x26);
                this.panelLabel.Size = new Size(this.backGroundImage.Width, 0x17);
                this.panelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backGroundImage.addControl(this.panelLabel);
                this.LastVillageType = -1;
                this.update(villageBackgroundType, heading, subHeading, panelText);
                return this.backGroundImage;
            }

            public void initTravelButton(CustomSelfDrawPanel.CSDButton button)
            {
                button.ImageNorm = (Image) GFXLibrary.mrhp_travelling_buttons[0];
                button.ImageOver = (Image) GFXLibrary.mrhp_travelling_buttons[1];
                button.ImageClick = (Image) GFXLibrary.mrhp_travelling_buttons[2];
                button.Text.TextDiffOnly = "";
                button.Text.Color = ARGBColors.Black;
                button.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                button.Text.Size = new Size(130, 0x34);
                button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                button.Text.Position = new Point(40, -9);
                button.ImageIcon = (Image) GFXLibrary.mrhp_village_type_miniicons[0];
                button.ImageIconPosition = new Point(10, 0);
            }

            private int remapTerrainToGFX(int type)
            {
                switch (type)
                {
                    case 2:
                        return 3;

                    case 3:
                        return 4;

                    case 4:
                        return 2;
                }
                return type;
            }

            public void removeWikiLink(CustomSelfDrawPanel.WikiLinkControl wikiLink)
            {
                if (wikiLink != null)
                {
                    this.headerImage.removeControl(wikiLink);
                }
            }

            public void setAction(int action)
            {
                this.actionIcon.Position = new Point(0x8d, 0x11);
                this.actionIcon.Visible = action != 0x2710;
                switch (action)
                {
                    case 0x3e8:
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
                        return;

                    case 0x3e9:
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
                        return;

                    case 0x3ea:
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
                        return;

                    case 0x3eb:
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
                        return;

                    case 0x3ec:
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
                        return;

                    case 0x3ed:
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
                        return;
                }
            }

            public void setActionFromVillage(int selectedVillage, int targetVillage)
            {
                this.actionIcon.Position = new Point(0x8d, 0x11);
                if (targetVillage < 0)
                {
                    if (GameEngine.Instance.World.isUserVillage(selectedVillage))
                    {
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x18];
                        this.actionIcon.Visible = true;
                        return;
                    }
                    if (GameEngine.Instance.World.isUserRelatedVillage(selectedVillage))
                    {
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x1b];
                        this.actionIcon.Visible = true;
                        return;
                    }
                }
                else
                {
                    if (GameEngine.Instance.World.isUserVillage(targetVillage))
                    {
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x18];
                        this.actionIcon.Visible = true;
                        return;
                    }
                    if (GameEngine.Instance.World.isUserRelatedVillage(targetVillage))
                    {
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x1b];
                        this.actionIcon.Visible = true;
                        return;
                    }
                    if (GameEngine.Instance.World.isVassal(selectedVillage, targetVillage))
                    {
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[30];
                        this.actionIcon.Visible = true;
                        return;
                    }
                    if (GameEngine.Instance.World.isVassal(targetVillage, selectedVillage))
                    {
                        this.actionIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x21];
                        this.actionIcon.Visible = true;
                        return;
                    }
                    int num = 0;
                    WorldMap.VillageData data = GameEngine.Instance.World.getVillageData(targetVillage);
                    if (((data != null) && (data.factionID >= 0)) && (RemoteServices.Instance.UserFactionID >= 0))
                    {
                        if (data.factionID != RemoteServices.Instance.UserFactionID)
                        {
                            int num2 = GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID);
                            int otherHouseID = GameEngine.Instance.World.getHouse(data.factionID);
                            if (num2 != otherHouseID)
                            {
                                int num4 = GameEngine.Instance.World.getYourHouseRelation(otherHouseID);
                                if (num4 > 0)
                                {
                                    num = 1;
                                }
                                else if (num4 < 0)
                                {
                                    num = -1;
                                }
                            }
                            if (num == 0)
                            {
                                int num5 = GameEngine.Instance.World.getYourFactionRelation(data.factionID);
                                if (num5 > 0)
                                {
                                    num = 1;
                                }
                                else if (num5 < 0)
                                {
                                    num = -1;
                                }
                            }
                        }
                        else
                        {
                            num = 2;
                        }
                    }
                    switch (num)
                    {
                        case 2:
                            this.actionIcon.Image = (Image) GFXLibrary.faction_relationships[1];
                            this.actionIcon.Visible = true;
                            this.actionIcon.Position = new Point(0x8d, 20);
                            return;

                        case 1:
                            this.actionIcon.Image = (Image) GFXLibrary.faction_relationships[0];
                            this.actionIcon.Visible = true;
                            this.actionIcon.Position = new Point(0x8d, 20);
                            return;

                        case -1:
                            this.actionIcon.Image = (Image) GFXLibrary.faction_relationships[2];
                            this.actionIcon.Visible = true;
                            this.actionIcon.Position = new Point(0x8d, 20);
                            return;
                    }
                }
                this.actionIcon.Visible = false;
            }

            public void setTooltipData(int tooltipData)
            {
                this.headerImage.CustomTooltipData = tooltipData;
            }

            public void showFade(bool state)
            {
                this.avatarUnderlayImage.Visible = state;
            }

            public void stretchBackground()
            {
                this.Size = new Size(0xc7, 0x111);
                this.backGroundImage.Size = new Size(GFXLibrary.mrhp_world_panel_192.Width, GFXLibrary.mrhp_world_panel_192.Height + 60);
            }

            public void update()
            {
                if (this.avatarUnderlayImage.Visible != InterfaceMgr.Instance.isUserInfoVisible())
                {
                    this.avatarUnderlayImage.Visible = InterfaceMgr.Instance.isUserInfoVisible();
                    base.invalidate();
                }
            }

            public void update(int villageBackgroundType, string heading, string subHeading, string panelText)
            {
                if (((this.LastVillageType == villageBackgroundType) && (heading == this.headingLabel.Text)) && ((subHeading == this.subHeadingLabel.Text) && (this.panelLabel.Text == panelText)))
                {
                    return;
                }
                int num = 0;
                int num2 = 0;
                this.LastVillageType = villageBackgroundType;
                if (this.headingLabel.TextDiffOnly != heading)
                {
                    int num3 = 0;
                    Graphics graphics = InterfaceMgr.Instance.ParentForm.CreateGraphics();
                    if (graphics.MeasureString(heading, FontManager.GetFont("Arial", 9f, FontStyle.Bold), 0xa8).ToSize().Height > 0x12)
                    {
                        num3 = 1;
                        if (graphics.MeasureString(heading, FontManager.GetFont("Arial", 8f, FontStyle.Bold), 0xa8).ToSize().Height > 0x12)
                        {
                            num3 = 2;
                            if (graphics.MeasureString(heading, FontManager.GetFont("Arial", 8f, FontStyle.Regular), 0xa8).ToSize().Height > 0x12)
                            {
                                num3 = 3;
                            }
                        }
                    }
                    graphics.Dispose();
                    switch (num3)
                    {
                        case 0:
                            this.headingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                            this.headingLabel.Position = new Point(14, 5);
                            this.headingLabel.Size = new Size(0xa8, 0x17);
                            this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            break;

                        case 1:
                            this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                            this.headingLabel.Position = new Point(14, 5);
                            this.headingLabel.Size = new Size(0xa8, 0x17);
                            this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            break;

                        case 2:
                            this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.headingLabel.Position = new Point(14, 5);
                            this.headingLabel.Size = new Size(0xa8, 0x17);
                            this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            break;

                        case 3:
                            this.headingLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            this.headingLabel.Position = new Point(0x12, 5);
                            this.headingLabel.Size = new Size(500, 0x17);
                            this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                            break;
                    }
                }
                this.headingLabel.TextDiffOnly = heading;
                this.subHeadingLabel.TextDiffOnly = subHeading;
                this.panelLabel.TextDiffOnly = panelText;
                if (subHeading.Length > 0)
                {
                    this.headerGlowLong.Visible = true;
                }
                else
                {
                    this.headerGlowLong.Visible = false;
                }
                this.headerIcon.Position = new Point(0x11, 0x1a);
                int num4 = villageBackgroundType;
                if (num4 <= 0x3ee)
                {
                    switch (num4)
                    {
                        case 3:
                        case 4:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[15];
                            num = 0x978;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 5:
                        case 6:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x10];
                            num = 0x979;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 7:
                        case 8:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x12];
                            num = 0x97a;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 9:
                        case 10:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x12];
                            num = 0x97b;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 11:
                        case 12:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x12];
                            num = 0x97c;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 13:
                        case 14:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x12];
                            num = 0x97d;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 15:
                        case 0x10:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x17];
                            num = 0x98f;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 0x11:
                        case 0x12:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x17];
                            num = 0x990;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 20:
                        case 0x15:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x11];
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 30:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x1d];
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 40:
                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                        case 0x2c:
                        case 0x2d:
                        case 0x2e:
                        case 0x2f:
                        case 0x30:
                        case 0x31:
                        case 50:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x18];
                            num = 0x991;
                            num2 = villageBackgroundType;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 0x33:
                        case 0x34:
                        case 0x35:
                        case 0x36:
                        case 0x37:
                        case 0x38:
                        case 0x39:
                        case 0x3a:
                        case 0x3b:
                        case 60:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x19];
                            num = 0x991;
                            num2 = villageBackgroundType;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 0x3d:
                        case 0x3e:
                        case 0x3f:
                        case 0x40:
                        case 0x41:
                        case 0x42:
                        case 0x43:
                        case 0x44:
                        case 0x45:
                        case 70:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x1a];
                            num = 0x991;
                            num2 = villageBackgroundType;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 0x47:
                        case 0x48:
                        case 0x49:
                        case 0x4a:
                        case 0x4b:
                        case 0x4c:
                        case 0x4d:
                        case 0x4e:
                        case 0x4f:
                        case 80:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x1b];
                            num = 0x991;
                            num2 = villageBackgroundType;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 0x51:
                        case 0x52:
                        case 0x53:
                        case 0x54:
                        case 0x55:
                        case 0x56:
                        case 0x57:
                        case 0x58:
                        case 0x59:
                        case 90:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x1c];
                            num = 0x991;
                            num2 = villageBackgroundType;
                            this.headerGlowSmall.Visible = false;
                            break;

                        case 100:
                        case 0x6a:
                        case 0x6b:
                        case 0x6c:
                        case 0x6d:
                        case 0x70:
                        case 0x71:
                        case 0x72:
                        case 0x73:
                        case 0x74:
                        case 0x75:
                        case 0x76:
                        case 0x77:
                        case 0x79:
                        case 0x7a:
                        case 0x7b:
                        case 0x7c:
                        case 0x7d:
                        case 0x7e:
                        case 0x80:
                        case 0x81:
                        case 130:
                        case 0x83:
                        case 0x84:
                        case 0x85:
                            if (villageBackgroundType == 100)
                            {
                                this.headerIcon.Position = new Point(-19, -3);
                                if (!HolidayPeriods.xmas(VillageMap.getCurrentServerTime()))
                                {
                                    this.headerIcon.Image = (Image) GFXLibrary.scout_screen_icons[0x1d];
                                }
                                else
                                {
                                    this.headerIcon.Image = (Image) GFXLibrary.scout_screen_icons[0x3b];
                                }
                                this.headerGlowSmall.Visible = true;
                            }
                            else
                            {
                                this.headerIcon.Image = (Image) GFXLibrary.getCommodity32DSImage(villageBackgroundType - 100);
                                this.headerGlowSmall.Visible = true;
                            }
                            num2 = villageBackgroundType;
                            num = 0x97e;
                            break;

                        case 0x3e8:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
                            this.headerGlowSmall.Visible = true;
                            break;

                        case 0x3e9:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
                            this.headerGlowSmall.Visible = true;
                            break;

                        case 0x3ea:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
                            this.headerGlowSmall.Visible = true;
                            break;

                        case 0x3eb:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
                            this.headerGlowSmall.Visible = true;
                            break;

                        case 0x3ec:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
                            this.headerGlowSmall.Visible = true;
                            break;

                        case 0x3ed:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
                            this.headerGlowSmall.Visible = true;
                            break;

                        case 0x3ee:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                            this.headerIcon.Image = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x15];
                            this.headerGlowSmall.Visible = true;
                            break;
                    }
                }
                else
                {
                    switch (num4)
                    {
                        case 0x5dc:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[11];
                            this.headerGlowSmall.Visible = false;
                            num = 0x974;
                            goto Label_0C4D;

                        case 0x5dd:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[12];
                            this.headerGlowSmall.Visible = false;
                            num = 0x975;
                            goto Label_0C4D;

                        case 0x5de:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[13];
                            this.headerGlowSmall.Visible = false;
                            num = 0x976;
                            goto Label_0C4D;

                        case 0x5df:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[14];
                            this.headerGlowSmall.Visible = false;
                            num = 0x977;
                            goto Label_0C4D;

                        case 0x5e0:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[20];
                            this.headerGlowSmall.Visible = false;
                            num = 0x992;
                            goto Label_0C4D;

                        case 0x5e1:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x15];
                            this.headerGlowSmall.Visible = false;
                            num = 0x98c;
                            goto Label_0C4D;

                        case 0x5e2:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[0x16];
                            this.headerGlowSmall.Visible = false;
                            goto Label_0C4D;

                        case 0x7d0:
                        case 0x7d1:
                        case 0x7d2:
                        case 0x7d3:
                        case 0x7d4:
                        case 0x7d5:
                        case 0x7d6:
                        case 0x7d7:
                        case 0x7d8:
                        case 0x7d9:
                            this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[this.remapTerrainToGFX(villageBackgroundType - 0x7d0)];
                            this.headerGlowSmall.Visible = false;
                            num = 0x984;
                            num2 = villageBackgroundType - 0x7d0;
                            goto Label_0C4D;
                    }
                    if (num4 == 0x2710)
                    {
                        this.headerImage.Image = (Image) GFXLibrary.mrhp_location_portrait[10];
                        this.headerGlowSmall.Visible = false;
                    }
                }
            Label_0C4D:
                this.headerImage.CustomTooltipID = num;
                this.headerImage.CustomTooltipData = num2;
            }

            public void updateHeading(string heading)
            {
                this.headingVillageID = -1;
                this.update(this.LastVillageType, heading, this.subHeadingLabel.Text, this.panelLabel.Text);
            }

            public void updatePanelText(string panelText)
            {
                this.update(this.LastVillageType, this.headingLabel.Text, this.subHeadingLabel.Text, panelText);
            }

            public void updatePanelType(int villageBackgroundType)
            {
                this.update(villageBackgroundType, this.headingLabel.Text, this.subHeadingLabel.Text, this.panelLabel.Text);
            }

            public void updatePanelTypeFromVillageID(int villageID)
            {
                this.headingVillageID = villageID;
                int villageBackgroundType = 0x2710;
                if (GameEngine.Instance.World.isSpecial(villageID))
                {
                    villageBackgroundType = GameEngine.Instance.World.getSpecial(villageID);
                    if (GameEngine.Instance.LocalWorldData.AIWorld)
                    {
                        switch (villageBackgroundType)
                        {
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                                villageBackgroundType = 0x7d0 + GameEngine.Instance.World.getVillageTerrainType(villageID);
                                break;
                        }
                    }
                }
                else if (GameEngine.Instance.World.isRegionCapital(villageID))
                {
                    villageBackgroundType = 0x5dc;
                }
                else if (GameEngine.Instance.World.isCountyCapital(villageID))
                {
                    villageBackgroundType = 0x5dd;
                }
                else if (GameEngine.Instance.World.isProvinceCapital(villageID))
                {
                    villageBackgroundType = 0x5de;
                }
                else if (GameEngine.Instance.World.isCountryCapital(villageID))
                {
                    villageBackgroundType = 0x5df;
                }
                else if (GameEngine.Instance.World.getVillageUserID(villageID) < 0)
                {
                    villageBackgroundType = 0x5e1;
                }
                else
                {
                    villageBackgroundType = 0x7d0 + GameEngine.Instance.World.getVillageTerrainType(villageID);
                }
                this.updatePanelType(villageBackgroundType);
            }

            public void updateSubHeading(string subHeading)
            {
                this.update(this.LastVillageType, this.headingLabel.Text, subHeading, this.panelLabel.Text);
            }

            public void updateTravelButton(CustomSelfDrawPanel.CSDButton button, int villageID)
            {
                try
                {
                    string text = GameEngine.Instance.World.getVillageNameOrType(villageID);
                    int num = 0;
                    Graphics graphics = InterfaceMgr.Instance.ParentForm.CreateGraphics();
                    Size size = graphics.MeasureString(text, button.Text.Font, 0x62).ToSize();
                    if (size.Height > 0x12)
                    {
                        num = 1;
                        size = graphics.MeasureString(text, button.Text.Font, 0x80).ToSize();
                        if (size.Height > 0x12)
                        {
                            num = 2;
                        }
                    }
                    if (num == 0)
                    {
                        button.Text.Size = new Size(100, 0x34);
                        button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                        button.Text.Position = new Point(40, -9);
                    }
                    else if (num == 1)
                    {
                        button.Text.Size = new Size(130, 0x34);
                        button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                        button.Text.Position = new Point(40, -9);
                    }
                    else if (num == 2)
                    {
                        if (size.Width < 0x7e)
                        {
                            button.Text.Size = new Size(size.Width + 4, 0x34);
                        }
                        else
                        {
                            button.Text.Size = new Size(130, 0x34);
                        }
                        button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                        button.Text.Position = new Point(40, -9);
                    }
                    graphics.Dispose();
                    button.Text.TextDiffOnly = text;
                    if (!GameEngine.Instance.World.isSpecial(villageID))
                    {
                        goto Label_0641;
                    }
                    int num2 = GameEngine.Instance.World.getSpecial(villageID);
                    switch (num2)
                    {
                        case 3:
                        case 4:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x18];
                            button.ImageIconPosition = new Point(-26, -33);
                            return;

                        case 5:
                        case 6:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x19];
                            button.ImageIconPosition = new Point(-18, -35);
                            return;

                        case 7:
                        case 8:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x1c];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 9:
                        case 10:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x1c];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 11:
                        case 12:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x1c];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 13:
                        case 14:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x1c];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 15:
                        case 0x10:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x35];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 0x11:
                        case 0x12:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x35];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 0x13:
                        case 0x16:
                        case 0x17:
                        case 0x18:
                        case 0x19:
                        case 0x1a:
                        case 0x1b:
                        case 0x1c:
                        case 0x1d:
                        case 30:
                        case 0x1f:
                        case 0x20:
                        case 0x21:
                        case 0x22:
                        case 0x23:
                        case 0x24:
                        case 0x25:
                        case 0x26:
                        case 0x27:
                        case 0x5b:
                        case 0x5c:
                        case 0x5d:
                        case 0x5e:
                        case 0x5f:
                        case 0x60:
                        case 0x61:
                        case 0x62:
                        case 0x63:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                        case 0x69:
                        case 110:
                        case 0x6f:
                        case 120:
                        case 0x7f:
                        case 0x80:
                        case 0x81:
                        case 130:
                        case 0x83:
                        case 0x84:
                            return;

                        case 20:
                        case 0x15:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x1a];
                            button.ImageIconPosition = new Point(-26, -33);
                            return;

                        case 40:
                        case 0x29:
                        case 0x2a:
                        case 0x2b:
                        case 0x2c:
                        case 0x2d:
                        case 0x2e:
                        case 0x2f:
                        case 0x30:
                        case 0x31:
                        case 50:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x36];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 0x33:
                        case 0x34:
                        case 0x35:
                        case 0x36:
                        case 0x37:
                        case 0x38:
                        case 0x39:
                        case 0x3a:
                        case 0x3b:
                        case 60:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x37];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 0x3d:
                        case 0x3e:
                        case 0x3f:
                        case 0x40:
                        case 0x41:
                        case 0x42:
                        case 0x43:
                        case 0x44:
                        case 0x45:
                        case 70:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x38];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 0x47:
                        case 0x48:
                        case 0x49:
                        case 0x4a:
                        case 0x4b:
                        case 0x4c:
                        case 0x4d:
                        case 0x4e:
                        case 0x4f:
                        case 80:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x39];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 0x51:
                        case 0x52:
                        case 0x53:
                        case 0x54:
                        case 0x55:
                        case 0x56:
                        case 0x57:
                        case 0x58:
                        case 0x59:
                        case 90:
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x3a];
                            button.ImageIconPosition = new Point(-26, -31);
                            return;

                        case 100:
                            if (HolidayPeriods.xmas(VillageMap.getCurrentServerTime()))
                            {
                                break;
                            }
                            button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x1d];
                            goto Label_0605;

                        case 0x6a:
                        case 0x6b:
                        case 0x6c:
                        case 0x6d:
                        case 0x70:
                        case 0x71:
                        case 0x72:
                        case 0x73:
                        case 0x74:
                        case 0x75:
                        case 0x76:
                        case 0x77:
                        case 0x79:
                        case 0x7a:
                        case 0x7b:
                        case 0x7c:
                        case 0x7d:
                        case 0x7e:
                        case 0x85:
                            button.ImageIcon = (Image) GFXLibrary.getCommodity32DSImage(num2 - 100);
                            button.ImageIconPosition = new Point(6, -7);
                            return;

                        default:
                            return;
                    }
                    button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x3b];
                Label_0605:
                    button.ImageIconPosition = new Point(-31, -33);
                    return;
                Label_0641:
                    if (GameEngine.Instance.World.isRegionCapital(villageID))
                    {
                        button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[8];
                        button.ImageIconPosition = new Point(-6, -16);
                    }
                    else if (GameEngine.Instance.World.isCountyCapital(villageID))
                    {
                        button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[9];
                        button.ImageIconPosition = new Point(-6, -16);
                    }
                    else if (GameEngine.Instance.World.isProvinceCapital(villageID))
                    {
                        button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[10];
                        button.ImageIconPosition = new Point(-6, -16);
                    }
                    else if (GameEngine.Instance.World.isCountryCapital(villageID))
                    {
                        button.ImageIcon = (Image) GFXLibrary.parishwall_village_center_achievement_icons[11];
                        button.ImageIconPosition = new Point(-6, -16);
                    }
                    else
                    {
                        button.ImageIcon = (Image) GFXLibrary.mrhp_village_type_miniicons[this.remapTerrainToGFX(GameEngine.Instance.World.getVillageTerrainType(villageID)) * 3];
                        button.ImageIconPosition = new Point(10, 0);
                    }
                }
                catch (Exception)
                {
                }
            }

            public void updateTravelButton(CustomSelfDrawPanel.CSDButton button, string villageString)
            {
                button.Text.TextDiffOnly = villageString;
                button.ImageIcon = (Image) GFXLibrary.scout_screen_icons[0x1a];
                button.ImageIconPosition = new Point(-26, -33);
            }
        }

        public class ParishChatPanel : CustomSelfDrawPanel.CSDControl
        {
            private bool allowBackFill = true;
            private CustomSelfDrawPanel.CSDArea chatScrollArea = new CustomSelfDrawPanel.CSDArea();
            private CustomSelfDrawPanel.CSDVertScrollBar chatScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
            private int currentChatHeight;
            private List<Chat_TextEntry> currentText = new List<Chat_TextEntry>();
            private bool locked;
            private int m_id = -1;
            private ParishWallPanel m_parent;
            private CustomSelfDrawPanel.CSDLabel oldMessagesLabel = new CustomSelfDrawPanel.CSDLabel();
            private bool repopulate;

            private void chatScrollBarMoved()
            {
                int y = this.chatScrollBar.Value;
                this.chatScrollArea.Position = new Point(this.chatScrollArea.X, -y);
                this.chatScrollArea.ClipRect = new Rectangle(this.chatScrollArea.ClipRect.X, y, this.chatScrollArea.ClipRect.Width, this.chatScrollArea.ClipRect.Height);
                this.chatScrollArea.invalidate();
                this.chatScrollBar.invalidate();
            }

            public void downloadOlderMessages()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.backfillPage(this.m_id);
                    this.oldMessagesLabel.Enabled = false;
                    this.oldMessagesLeave();
                }
            }

            public void freeOldMessagesButton()
            {
                this.oldMessagesLabel.Enabled = true;
            }

            public void importText(Chat_TextEntry[] newText, bool backFill, long deleteID)
            {
                int length = newText.Length;
                if (((length != 0) || this.repopulate) || (backFill || (deleteID >= 0L)))
                {
                    this.repopulate = false;
                    if (backFill && (length == 0))
                    {
                        this.allowBackFill = false;
                    }
                    if (deleteID >= 0L)
                    {
                        this.allowBackFill = true;
                        for (int i = 0; i < this.currentText.Count; i++)
                        {
                            if (this.currentText[i].textID == deleteID)
                            {
                                this.currentText.Remove(this.currentText[i]);
                                break;
                            }
                        }
                    }
                    else if (!backFill)
                    {
                        List<Chat_TextEntry> list = new List<Chat_TextEntry>();
                        list.AddRange(newText);
                        list.AddRange(this.currentText);
                        this.currentText = list;
                    }
                    else
                    {
                        this.currentText.AddRange(newText);
                        length = 0;
                    }
                    if (this.currentText.Count > 150)
                    {
                        this.currentText.RemoveRange(150, this.currentText.Count - 150);
                    }
                    int max = this.chatScrollBar.Value;
                    bool flag = false;
                    if (this.chatScrollArea.Y == 0)
                    {
                        flag = true;
                    }
                    this.clearControls();
                    this.chatScrollArea.Position = new Point(0, 0);
                    this.chatScrollArea.Size = new Size(base.Width - 60, base.Height);
                    this.chatScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(base.Width - 60, base.Height));
                    base.addControl(this.chatScrollArea);
                    this.chatScrollBar.Visible = false;
                    this.chatScrollBar.Position = new Point(base.Width - 0x1a, 0);
                    this.chatScrollBar.Size = new Size(0x18, base.Height);
                    base.addControl(this.chatScrollBar);
                    this.chatScrollBar.Value = 0;
                    this.chatScrollBar.Max = 100;
                    this.chatScrollBar.NumVisibleLines = 0x19;
                    this.chatScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
                    this.chatScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.chatScrollBarMoved));
                    int y = 0;
                    int num5 = 0;
                    int num6 = 0;
                    foreach (Chat_TextEntry entry in this.currentText)
                    {
                        CustomSelfDrawPanel.ParishWallChatEntry control = new CustomSelfDrawPanel.ParishWallChatEntry {
                            Position = new Point(0, y)
                        };
                        control.init(entry, this.m_parent);
                        this.chatScrollArea.addControl(control);
                        y += control.Height;
                        this.currentChatHeight += control.Height;
                        if ((num5 < (this.currentText.Count - 1)) || this.allowBackFill)
                        {
                            CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage {
                                Image = (Image) GFXLibrary.parishwall_dividing_line,
                                Position = new Point(0, y + 3)
                            };
                            this.chatScrollArea.addControl(image);
                            y += 10;
                        }
                        if ((num5 + 1) == length)
                        {
                            num6 = y;
                        }
                        num5++;
                    }
                    if (this.allowBackFill)
                    {
                        this.oldMessagesLabel.Text = SK.Text("ParishWallPanel_Older_Messages", "Older Messages");
                        this.oldMessagesLabel.Color = ARGBColors.Blue;
                        this.oldMessagesLabel.Position = new Point(0x3f, y + 3);
                        this.oldMessagesLabel.Size = new Size(0x195, 30);
                        this.oldMessagesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                        this.oldMessagesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                        this.oldMessagesLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.downloadOlderMessages), "ParishChatPanel_view_older_messages");
                        this.oldMessagesLabel.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.oldMessagesOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.oldMessagesLeave));
                        this.oldMessagesLabel.Enabled = true;
                        this.chatScrollArea.addControl(this.oldMessagesLabel);
                        y += 0x19;
                    }
                    this.chatScrollArea.Size = new Size(this.chatScrollArea.Width, y);
                    if (y < this.chatScrollBar.Height)
                    {
                        this.chatScrollBar.Visible = false;
                    }
                    else
                    {
                        this.chatScrollBar.Visible = true;
                        this.chatScrollBar.NumVisibleLines = this.chatScrollBar.Height;
                        this.chatScrollBar.Max = y - this.chatScrollBar.Height;
                    }
                    this.chatScrollArea.invalidate();
                    this.chatScrollBar.invalidate();
                    if (this.m_parent != null)
                    {
                        this.m_parent.Invalidate();
                    }
                    if (!flag)
                    {
                        max += num6;
                        if ((max > 0) && this.chatScrollBar.Visible)
                        {
                            if (max >= this.chatScrollBar.Max)
                            {
                                max = this.chatScrollBar.Max;
                            }
                            this.chatScrollBar.Value = max;
                            this.chatScrollBarMoved();
                        }
                    }
                }
            }

            public void oldMessagesLeave()
            {
                this.oldMessagesLabel.Color = ARGBColors.Blue;
            }

            public void oldMessagesOver()
            {
                this.oldMessagesLabel.Color = ARGBColors.Aquamarine;
            }

            public void reset(ParishWallPanel parent, int id)
            {
                this.m_parent = parent;
                this.m_id = id;
                this.currentText.Clear();
                this.currentChatHeight = 0;
                this.clearControls();
                this.locked = false;
                this.allowBackFill = true;
                if (id == 3)
                {
                    this.locked = true;
                }
            }

            public void scrollToBottom()
            {
                this.chatScrollBar.Value = 0;
                this.chatScrollBarMoved();
            }

            public void setAsSteward()
            {
                this.locked = false;
            }

            public void setUnreads(int numUnread)
            {
                string title = "";
                switch (this.m_id)
                {
                    case 0:
                        title = SK.Text("ParishWallPanel_General", "General");
                        break;

                    case 1:
                        title = SK.Text("ParishWallPanel_War", "War");
                        break;

                    case 2:
                        title = SK.Text("ParishWallPanel_inn", "Inn");
                        break;

                    case 3:
                        title = SK.Text("ParishWallPanel_Steward", "Steward");
                        break;

                    case 4:
                        title = SK.Text("GENERIC_Parish", "Parish");
                        break;

                    case 5:
                        title = SK.Text("MENU_Help", "Help");
                        break;
                }
                if (numUnread > 0)
                {
                    title = title + " (" + numUnread.ToString() + ")";
                }
                this.m_parent.setTabText(this.m_id, title);
            }

            public bool Locked
            {
                get
                {
                    return this.locked;
                }
            }

            public bool Repopulate
            {
                set
                {
                    this.repopulate = value;
                }
            }
        }

        public class ParishWallChatEntry : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDLabel bodyText = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel dateText = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton deleteButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDImage effectImage = new CustomSelfDrawPanel.CSDImage();
            private ParishWallPanel parent;
            private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
            private MyMessageBoxPopUp PopUpRef;
            private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
            private static int spaceWidth = -1;
            private CustomSelfDrawPanel.CSDArea textArea = new CustomSelfDrawPanel.CSDArea();
            private long textID = -1L;

            private void copyTextToClipboardClick()
            {
                if (((base.csd != null) && (base.csd.ClickedControl != null)) && (base.csd.ClickedControl.GetType() == typeof(CustomSelfDrawPanel.CSDLabel)))
                {
                    CustomSelfDrawPanel.CSDLabel clickedControl = (CustomSelfDrawPanel.CSDLabel) base.csd.ClickedControl;
                    Clipboard.SetText(clickedControl.Text.TrimStart(null));
                }
            }

            private void deleteClicked()
            {
                if (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Post", "Delete This Post"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.DeletePost();
                }
            }

            private void DeletePost()
            {
                RemoteServices.Instance.Chat_Admin_Command(6, (int) this.textID);
                if (this.parent != null)
                {
                    this.parent.deleteWallPost(this.textID);
                }
            }

            public void init(Chat_TextEntry textEntry, ParishWallPanel window)
            {
                this.parent = window;
                this.textID = textEntry.textID;
                this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(textEntry.userID, 0x20, 0x24);
                this.shieldImage.Position = new Point(15, 7);
                base.addControl(this.shieldImage);
                this.playerName.Text = textEntry.username;
                this.playerName.Color = ARGBColors.Blue;
                this.playerName.Position = new Point(0, 0);
                this.playerName.Size = new Size(0x195, 30);
                this.playerName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClicked), "ParishChatPanel_user");
                this.playerName.Data = textEntry.userID;
                this.playerName.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerLeave));
                this.playerName.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
                this.bodyText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                Graphics graphics = window.CreateGraphics();
                Size size = graphics.MeasureString(textEntry.username, this.playerName.Font, 0xf4240).ToSize();
                this.playerName.Size = new Size(size.Width + 5, 20);
                if (spaceWidth < 0)
                {
                    spaceWidth = graphics.MeasureString(" ", this.bodyText.Font, 0xf4240).ToSize().Width;
                }
                string text = "";
                for (int i = size.Width + 15; i > 0; i -= spaceWidth)
                {
                    text = text + " ";
                }
                text = text + textEntry.text;
                this.textArea = new CustomSelfDrawPanel.CSDArea();
                this.textArea.Position = new Point(0x3f, 0);
                this.textArea.Size = new Size(0x195, 0x3e8);
                base.addControl(this.textArea);
                this.bodyText.Text = text;
                this.bodyText.Color = ARGBColors.Black;
                this.bodyText.Position = new Point(0, 0);
                this.bodyText.Size = new Size(0x195, 0x3e8);
                this.bodyText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.bodyText.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
                this.textArea.addControl(this.bodyText);
                this.textArea.addControl(this.playerName);
                int height = graphics.MeasureString(text, this.bodyText.Font, 0x195).ToSize().Height + 20;
                if (height < 0x3f)
                {
                    height = 0x3f;
                }
                TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - textEntry.postedTime);
                if (span.TotalMinutes < 1.0)
                {
                    int totalSeconds = (int) span.TotalSeconds;
                    if (totalSeconds <= 0)
                    {
                        totalSeconds = 1;
                    }
                    if (totalSeconds != 1)
                    {
                        this.dateText.Text = totalSeconds.ToString() + " " + SK.Text("ParishWallPanel_X_Seconds_Ago", "seconds ago");
                    }
                    else
                    {
                        this.dateText.Text = totalSeconds.ToString() + " " + SK.Text("ParishWallPanel_X_Second_Ago", "second ago");
                    }
                }
                else if (span.TotalHours < 1.0)
                {
                    int totalMinutes = (int) span.TotalMinutes;
                    if (totalMinutes <= 0)
                    {
                        totalMinutes = 1;
                    }
                    if (totalMinutes != 1)
                    {
                        this.dateText.Text = totalMinutes.ToString() + " " + SK.Text("ParishWallPanel_X_Minutes_Ago", "minutes ago");
                    }
                    else
                    {
                        this.dateText.Text = totalMinutes.ToString() + " " + SK.Text("ParishWallPanel_X_Minute_Ago", "minute ago");
                    }
                }
                else if (span.TotalHours < 24.0)
                {
                    int totalHours = (int) span.TotalHours;
                    if (totalHours <= 0)
                    {
                        totalHours = 1;
                    }
                    if (totalHours != 1)
                    {
                        this.dateText.Text = totalHours.ToString() + " " + SK.Text("ParishWallPanel_X_Hours_Ago", "hours ago");
                    }
                    else
                    {
                        this.dateText.Text = totalHours.ToString() + " " + SK.Text("ParishWallPanel_X_Hour_Ago", "hour ago");
                    }
                }
                else
                {
                    int totalDays = (int) span.TotalDays;
                    if (totalDays <= 0)
                    {
                        totalDays = 1;
                    }
                    if (totalDays != 1)
                    {
                        this.dateText.Text = totalDays.ToString() + " " + SK.Text("ParishWallPanel_X_Days_Ago", "days ago");
                    }
                    else
                    {
                        this.dateText.Text = totalDays.ToString() + " " + SK.Text("ParishWallPanel_X_Day_Ago", "day ago");
                    }
                }
                this.dateText.Color = Color.FromArgb(0x4d, 0x4f, 0x51);
                this.dateText.Position = new Point(0x3f, height - 20);
                this.dateText.Size = new Size(0x195, 30);
                this.dateText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                this.dateText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                base.addControl(this.dateText);
                this.Size = new Size(0x195, height);
                this.textArea.ClipVisible = true;
                graphics.Dispose();
                if ((RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator) || (RemoteServices.Instance.UserID == ParishWallPanel.m_userIDOnCurrent))
                {
                    this.deleteButton.ImageNorm = (Image) GFXLibrary.trashcan_normal;
                    this.deleteButton.ImageOver = (Image) GFXLibrary.trashcan_over;
                    this.deleteButton.ImageClick = (Image) GFXLibrary.trashcan_clicked;
                    this.deleteButton.Position = new Point(0x1bd, height - 20);
                    this.deleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "FactionNewForumPostsPanel_delete_post");
                    base.addControl(this.deleteButton);
                }
            }

            public void playerClicked()
            {
                if (base.csd.ClickedControl != null)
                {
                    int data = base.csd.ClickedControl.Data;
                    InterfaceMgr.Instance.changeTab(0);
                    WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                        userID = data
                    };
                    InterfaceMgr.Instance.showUserInfoScreen(userInfo);
                }
            }

            public void playerLeave()
            {
                this.playerName.Color = ARGBColors.Blue;
            }

            public void playerOver()
            {
                this.playerName.Color = ARGBColors.Aquamarine;
            }
        }

        public class ParishWallEntry : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private Point donatePopupLocation = new Point();
            private CustomSelfDrawPanel.CSDImage effectImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel effectText = new CustomSelfDrawPanel.CSDLabel();
            private int m_lineID;
            private CustomSelfDrawPanel m_parent;
            private int m_villageID = -1;
            private WallInfo m_wallInfo;
            private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();

            public void clickDonate()
            {
                if ((base.csd.ClickedControl != null) && this.backgroundImage.Enabled)
                {
                    CustomSelfDrawPanel.CSDControl clickedControl = base.csd.ClickedControl;
                    this.donatePopupLocation = clickedControl.getPanelPosition();
                    this.donatePopupLocation = new Point(this.donatePopupLocation.X + clickedControl.Width, this.donatePopupLocation.Y);
                    this.donatePopupLocation = this.m_parent.PointToScreen(this.donatePopupLocation);
                    this.showDetailedInfo(this.m_wallInfo.userID, this.m_wallInfo.entryType);
                    this.leaveDonate();
                }
            }

            public BaseImage getCapitalBuildingImage(int buildingType)
            {
                switch (buildingType)
                {
                    case 0x4f:
                        return GFXLibrary.townbuilding_Woodcutter_normal;

                    case 80:
                        return GFXLibrary.townbuilding_stonequarry_normal;

                    case 0x51:
                        return GFXLibrary.townbuilding_iron_normal;

                    case 0x52:
                        return GFXLibrary.townbuilding_pitch_normal;

                    case 0x53:
                        return GFXLibrary.townbuilding_ale_normal;

                    case 0x54:
                        return GFXLibrary.townbuilding_apples_normal;

                    case 0x55:
                        return GFXLibrary.townbuilding_cheese_normal;

                    case 0x56:
                        return GFXLibrary.townbuilding_meat_normal;

                    case 0x57:
                        return GFXLibrary.townbuilding_bread_normal;

                    case 0x58:
                        return GFXLibrary.townbuilding_veg_normal;

                    case 0x59:
                        return GFXLibrary.townbuilding_fish_normal;

                    case 90:
                        return GFXLibrary.townbuilding_bows_normal;

                    case 0x5b:
                        return GFXLibrary.townbuilding_pikes_normal;

                    case 0x5c:
                        return GFXLibrary.townbuilding_armour_normal;

                    case 0x5d:
                        return GFXLibrary.townbuilding_sword_normal;

                    case 0x5e:
                        return GFXLibrary.townbuilding_catapults_normal;

                    case 0x5f:
                        return GFXLibrary.townbuilding_venison_normal;

                    case 0x60:
                        return GFXLibrary.townbuilding_wine_normal;

                    case 0x61:
                        return GFXLibrary.townbuilding_salt_normal;

                    case 0x62:
                        return GFXLibrary.townbuilding_carpenter_normal;

                    case 0x63:
                        return GFXLibrary.townbuilding_tailor_normal;

                    case 100:
                        return GFXLibrary.townbuilding_metalware_normal;

                    case 0x65:
                        return GFXLibrary.townbuilding_spice_normal;

                    case 0x66:
                        return GFXLibrary.townbuilding_silk_normal;

                    case 0x67:
                        return GFXLibrary.townbuilding_architectsguild_normal;

                    case 0x68:
                        return GFXLibrary.townbuilding_Labourersbillets_normal;

                    case 0x69:
                        return GFXLibrary.townbuilding_castellanshouse_normal;

                    case 0x6a:
                        return GFXLibrary.townbuilding_sergeantsatarmsoffice_normal;

                    case 0x6b:
                        return GFXLibrary.townbuilding_stables_normal;

                    case 0x6c:
                        return GFXLibrary.townbuilding_barracks_normal;

                    case 0x6d:
                        return GFXLibrary.townbuilding_peasntshall_normal;

                    case 110:
                        return GFXLibrary.townbuilding_archeryrange_normal;

                    case 0x6f:
                        return GFXLibrary.townbuilding_pikemandrillyard_normal;

                    case 0x70:
                        return GFXLibrary.townbuilding_combatarena_normal;

                    case 0x71:
                        return GFXLibrary.townbuilding_siegeengineersguild_normal;

                    case 0x72:
                        return GFXLibrary.townbuilding_officersquarters_normal;

                    case 0x73:
                        return GFXLibrary.townbuilding_militaryschool_normal;

                    case 0x74:
                        return GFXLibrary.townbuilding_supplydepot_normal;

                    case 0x75:
                        return GFXLibrary.townbuilding_townhall_normal;

                    case 0x76:
                        return GFXLibrary.townbuilding_church_normal;

                    case 0x77:
                        return GFXLibrary.townbuilding_towngarden_normal;

                    case 120:
                        return GFXLibrary.townbuilding_statue_normal;

                    case 0x79:
                        return GFXLibrary.townbuilding_turretmaker_normal;

                    case 0x7a:
                        return GFXLibrary.townbuilding_tunnellorsguild_normal;

                    case 0x7b:
                        return GFXLibrary.townbuilding_ballistamaker_normal;
                }
                return null;
            }

            public void init(WallInfo wallInfo, int lineID, int villageID, CustomSelfDrawPanel window)
            {
                this.m_parent = window;
                this.m_villageID = villageID;
                this.m_lineID = lineID;
                this.m_wallInfo = wallInfo;
                this.clearControls();
                if ((lineID & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_01;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_02;
                }
                this.backgroundImage.Position = new Point(0, 0);
                this.backgroundImage.setClickDelegate(null);
                this.backgroundImage.setMouseOverDelegate(null, null);
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(wallInfo.userID, 0x20, 0x24);
                this.shieldImage.Position = new Point(10, 5);
                this.backgroundImage.addControl(this.shieldImage);
                this.playerName.Text = wallInfo.username;
                this.playerName.Color = ARGBColors.Black;
                this.playerName.Position = new Point(60, 4);
                this.playerName.Size = new Size(0xd6, 0x10);
                this.playerName.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClicked), "ParishChatPanel_user");
                this.playerName.Data = wallInfo.userID;
                this.playerName.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.playerLeave));
                this.backgroundImage.addControl(this.playerName);
                Graphics graphics = window.CreateGraphics();
                Size size = graphics.MeasureString(wallInfo.username, this.playerName.Font, 0xd6).ToSize();
                this.playerName.Size = new Size(size.Width + 5, 0x10);
                graphics.Dispose();
                this.effectText.Text = "";
                this.effectText.Color = Color.FromArgb(0x26, 0x4d, 0);
                this.effectText.Position = new Point(60, 0x13);
                this.effectText.Size = new Size(0xd6, 0x1c);
                this.effectText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.effectText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.backgroundImage.addControl(this.effectText);
                int index = -1;
                switch (wallInfo.entryType)
                {
                    case 1:
                        this.effectText.Text = SK.Text("ParishWallPanel_Donates_Goods", "Donates Goods");
                        index = 2;
                        this.backgroundImage.Data = wallInfo.entryType;
                        this.backgroundImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.overDonate), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.leaveDonate));
                        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickDonate), "ParishChatPanel_donate_popup");
                        break;

                    case 2:
                        this.effectText.Text = SK.Text("ParishWallPanel_Upgrades", "Upgrades") + " : " + VillageBuildingsData.getBuildingName(wallInfo.data1);
                        this.effectImage.Image = (Image) this.getCapitalBuildingImage(wallInfo.data1);
                        this.effectImage.Size = new Size(60, 60);
                        this.effectImage.Position = new Point(0x110, -7);
                        this.backgroundImage.addControl(this.effectImage);
                        break;

                    case 10:
                        this.effectText.Text = SK.Text("ParishWallPanel_Destroys_Bandit_Camp", "Destroys a Bandit Camp");
                        index = 3;
                        break;

                    case 11:
                        this.effectText.Text = SK.Text("ParishWallPanel_Destroys_Wolf_Lair", "Destroys a Wolf Lair");
                        index = 1;
                        break;

                    case 12:
                        this.effectText.Text = SK.Text("ParishWallPanel_Defeats_rat", "Defeats the Rat");
                        index = 12;
                        break;

                    case 13:
                        this.effectText.Text = SK.Text("ParishWallPanel_Defeats_Snake", "Defeats the Snake");
                        index = 12;
                        break;

                    case 14:
                        this.effectText.Text = SK.Text("ParishWallPanel_Defeats_Pig", "Defeats the Pig");
                        index = 12;
                        break;

                    case 15:
                        this.effectText.Text = SK.Text("Defeats_Wolf", "Defeats the Wolf");
                        index = 12;
                        break;

                    case 0x10:
                        this.effectText.Text = SK.Text("Defeats_Paladin", "Defeats Paladin's Castle");
                        index = 14;
                        break;

                    case 0x11:
                        this.effectText.Text = SK.Text("Defeats_Paladin", "Defeats Paladin's Castle");
                        index = 14;
                        break;

                    case 0x12:
                        this.effectText.Text = SK.Text("Defeats_Treasure_Castle", "Defeats a Treasure Castle");
                        index = 15;
                        break;

                    case 20:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Bandit_Camp", "Attacks a Bandit Camp");
                        index = 3;
                        break;

                    case 0x15:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Wolf_Lair", "Attacks a Wolf Lair");
                        index = 1;
                        break;

                    case 0x16:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Rat", "Attacks the Rat");
                        index = 12;
                        break;

                    case 0x17:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Snake", "Attacks the Snake");
                        index = 12;
                        break;

                    case 0x18:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Pig", "Attacks the Pig");
                        index = 12;
                        break;

                    case 0x19:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Wolf", "Attacks the Wolf");
                        index = 12;
                        break;

                    case 0x1a:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Paladin", "Attacks Paladin's Castle");
                        index = 14;
                        break;

                    case 0x1b:
                        this.effectText.Text = SK.Text("ParishWallPanel_Attacks_Paladin", "Attacks Paladin's Castle");
                        index = 14;
                        break;

                    case 0x1c:
                        this.effectText.Text = SK.Text("ParishWallPanel__Treasure_Castle", "Attacks a Treasure Castle");
                        index = 15;
                        break;

                    case 30:
                        this.effectText.Text = SK.Text("ParishWallPanel_Capture_Flag", "Captures a Flag");
                        index = 4;
                        break;

                    case 0x1f:
                        this.effectText.Text = SK.Text("ParishWallPanel_Taken_Flag", "Taken Flag");
                        index = 5;
                        break;

                    case 40:
                        this.effectText.Text = SK.Text("ParishWallPanel_Blesses", "Blesses the Parish");
                        index = 7;
                        break;

                    case 0x2a:
                        this.effectText.Text = SK.Text("ParishWallPanel_Influences", "Influences Election");
                        index = 7;
                        break;

                    case 0x2b:
                        this.effectText.Text = SK.Text("ParishWallPanel_Inquisition", "Inquisitions the Parish");
                        index = 7;
                        break;

                    case 0x2c:
                        this.effectText.Text = SK.Text("ParishWallPanel_Heals", "Heals some disease in the parish");
                        index = 7;
                        break;

                    case 50:
                        this.effectText.Text = SK.Text("ParishWallPanel_Joins_Parish", "Joins the Parish");
                        index = 8;
                        break;

                    case 0x33:
                        this.effectText.Text = SK.Text("ParishWallPanel_Promotes_To", "Promotes To") + " : " + Rankings.getRankingName(wallInfo.data1, wallInfo.data2 == 0);
                        index = 0;
                        break;

                    case 0x34:
                        this.effectText.Text = SK.Text("ParishWallPanel_Becomes_Steward", "Becomes Steward");
                        index = 13;
                        break;

                    case 0x35:
                        this.effectText.Text = SK.Text("ParishWallPanel_Becomes_Sheriff", "Becomes Sheriff");
                        index = 13;
                        break;

                    case 0x36:
                        this.effectText.Text = SK.Text("ParishWallPanel_Becomes_Governor", "Becomes Governor");
                        index = 13;
                        break;

                    case 0x37:
                        this.effectText.Text = SK.Text("ParishWallPanel_Becomes_King", "Becomes King");
                        index = 13;
                        break;
                }
                if (index >= 0)
                {
                    this.effectImage.Image = (Image) GFXLibrary.parishwall_village_center_achievement_icons[index];
                    this.effectImage.Position = new Point(0x112, -5);
                    this.backgroundImage.addControl(this.effectImage);
                }
            }

            public void leaveDonate()
            {
                if ((this.m_lineID & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_01;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_02;
                }
            }

            public void overDonate()
            {
                if (this.backgroundImage.Enabled)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.parishwall_tan_bar_03;
                }
            }

            public void parishWallDetailInfoCallBack(ParishWallDetailInfo_ReturnType returnData)
            {
                this.backgroundImage.Enabled = true;
                if ((returnData.Success && (returnData.detailedInfo != null)) && ((returnData.detailedInfo.Count > 0) && (returnData.detailedInfo[0].entryType == 1)))
                {
                    if (returnData.m_errorID != 0x3e7)
                    {
                        returnData.m_errorID = 0x3e7;
                        GameEngine.Instance.World.registerParishWallDonateDetails(returnData);
                    }
                    DonatePopup.CreateDonatePopup(this.donatePopupLocation, returnData);
                }
            }

            public void playerClicked()
            {
                if (base.csd.ClickedControl != null)
                {
                    int data = base.csd.ClickedControl.Data;
                    InterfaceMgr.Instance.changeTab(0);
                    WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                        userID = data
                    };
                    InterfaceMgr.Instance.showUserInfoScreen(userInfo);
                }
            }

            public void playerLeave()
            {
                this.playerName.Color = ARGBColors.Black;
            }

            public void playerOver()
            {
                this.playerName.Color = ARGBColors.White;
            }

            public void showDetailedInfo(int userID, int wallType)
            {
                ParishWallDetailInfo_ReturnType returnData = GameEngine.Instance.World.getParishWallDonateDetails(this.m_villageID, userID);
                if (returnData == null)
                {
                    RemoteServices.Instance.set_ParishWallDetailInfo_UserCallBack(new RemoteServices.ParishWallDetailInfo_UserCallBack(this.parishWallDetailInfoCallBack));
                    RemoteServices.Instance.ParishWallDetailInfo(this.m_villageID, userID, wallType);
                    this.backgroundImage.Enabled = false;
                }
                else
                {
                    this.parishWallDetailInfoCallBack(returnData);
                }
            }
        }

        public class ResourceButton : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDButton baseButton = new CustomSelfDrawPanel.CSDButton();
            private LogoutPanel m_logoutParent;
            private CustomSelfDrawPanel.CSDImage resourceImage = new CustomSelfDrawPanel.CSDImage();

            public void buttonClicked()
            {
                if (base.csd.ClickedControl != null)
                {
                    int data = base.csd.ClickedControl.Data;
                    if (this.m_logoutParent != null)
                    {
                        this.m_logoutParent.resourceSelected(data);
                    }
                }
            }

            public void init(int resource, LogoutPanel logoutParent)
            {
                this.m_logoutParent = logoutParent;
                this.baseButton.ImageNorm = (Image) GFXLibrary.logout_bits[7];
                this.baseButton.ImageOver = (Image) GFXLibrary.logout_bits[8];
                this.baseButton.Position = new Point(0, 1);
                this.baseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buttonClicked), "SelectTradingResourcePanel_resource");
                this.baseButton.Data = resource;
                base.addControl(this.baseButton);
                this.baseButton.CustomTooltipID = 0x589;
                this.baseButton.CustomTooltipData = resource;
                this.Size = this.baseButton.Size;
                this.resourceImage.Image = (Image) GFXLibrary.getCommodity64DSImage(resource);
                this.resourceImage.Data = resource;
                this.resourceImage.Position = new Point(0, 0);
                this.resourceImage.Size = new Size(0x45, 0x45);
                this.baseButton.addControl(this.resourceImage);
            }
        }

        public class UICard : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDImage bigBaseImage;
            public CustomSelfDrawPanel.CSDLabel bigEffect;
            public BaseImage bigFrame;
            public CustomSelfDrawPanel.CSDImage bigFrameExtraImage;
            public CustomSelfDrawPanel.CSDImage bigFrameImage;
            public BaseImage bigFrameOver;
            public CustomSelfDrawPanel.CSDImage bigGradeImage;
            public BaseImage bigImage;
            public CustomSelfDrawPanel.CSDLabel bigTitle;
            public CustomSelfDrawPanel.CSDLabel buyCardsLabel;
            public int cardCount = 1;
            public static CardsIDComparer cardsIDComparer = new CardsIDComparer();
            public static CardsIDComparerReverse cardsIDComparerReverse = new CardsIDComparerReverse();
            public static CardsNameComparer cardsNameComparer = new CardsNameComparer();
            public static CardsNameComparerReverse cardsNameComparerReverse = new CardsNameComparerReverse();
            public static CardsPriceComparer cardsPriceComparer = new CardsPriceComparer();
            public static CardsPriceComparerReverse cardsPriceComparerReverse = new CardsPriceComparerReverse();
            public static CardsQuantityComparer cardsQuantityComparer = new CardsQuantityComparer();
            public static CardsQuantityComparerReverse cardsQuantityComparerReverse = new CardsQuantityComparerReverse();
            public CustomSelfDrawPanel.CSDLabel countLabel;
            public CardTypes.CardDefinition Definition;
            public CustomSelfDrawPanel.CSDImage progressBar;
            public CustomSelfDrawPanel.CSDLabel rankLabel;
            public double RemainingTime;
            public int SearchIndex;
            public int SetIndex;
            public CustomSelfDrawPanel.CSDImage smallBase;
            public CustomSelfDrawPanel.CSDImage smallFrame;
            public double TotalTime;
            public static TUT2CardsNameComparer TUT2cardsNameComparer = new TUT2CardsNameComparer();
            public static TUTCardsNameComparer TUTcardsNameComparer = new TUTCardsNameComparer();
            public int UserID;
            public List<int> UserIDList = new List<int>();

            public UICard()
            {
                base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.MouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.MouseOut));
                this.ClipVisible = true;
            }

            public override void clearControls()
            {
                base.clearControls();
                this.bigFrame = null;
                this.bigFrameImage = null;
                this.bigFrameOver = null;
            }

            public void Hilight(Color c)
            {
                if (this.bigBaseImage != null)
                {
                    this.bigBaseImage.Colorise = c;
                }
                if (this.bigFrameImage != null)
                {
                    this.bigFrameImage.Colorise = c;
                }
                if (this.bigFrameExtraImage != null)
                {
                    this.bigFrameExtraImage.Colorise = c;
                }
                base.invalidate();
            }

            public void MouseOut()
            {
                if (((this.bigFrameImage != null) && (this.bigFrame != null)) && (this.bigFrameOver != null))
                {
                    this.bigFrameImage.Image = (Image) this.bigFrame;
                }
            }

            public void MouseOver()
            {
                if (((this.bigFrameImage != null) && (this.bigFrame != null)) && (this.bigFrameOver != null))
                {
                    this.bigFrameImage.Image = (Image) this.bigFrameOver;
                }
            }

            public void ScaleAll(double factor)
            {
                this.Size = new Size(Convert.ToInt32(Math.Floor((double) (this.bigFrame.Width * factor))), Convert.ToInt32(Math.Floor((double) (this.bigFrame.Height * factor))));
                this.bigBaseImage.Scale = factor;
                this.bigFrameImage.Scale = factor;
                this.bigGradeImage.Scale = factor;
                this.bigEffect.Scale = factor;
                this.bigTitle.Scale = factor;
                this.rankLabel.Scale = factor;
                this.countLabel.Scale = factor;
                if (this.bigFrameExtraImage != null)
                {
                    this.bigFrameExtraImage.Scale = factor;
                }
            }

            public void setAlpha(float factor)
            {
                this.bigBaseImage.Alpha = factor;
            }

            public void setProgress(double secondspassed)
            {
                this.RemainingTime -= secondspassed;
                double num = this.RemainingTime / this.TotalTime;
                int width = Convert.ToInt32(Math.Floor((double) (num * this.progressBar.ClipRect.Width)));
                this.progressBar.ClipRect = new Rectangle(0, 0, width, this.progressBar.ClipRect.Height);
            }

            public class CardsIDComparer : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard x, CustomSelfDrawPanel.UICard y)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    int id = x.Definition.id;
                    int card = y.Definition.id;
                    int num3 = CardTypes.getCardType(id);
                    int num4 = CardTypes.getCardType(card);
                    if (num3 <= 0x110)
                    {
                        num3 += 0xa60;
                    }
                    if (num4 <= 0x110)
                    {
                        num4 += 0xa60;
                    }
                    if (num3 < num4)
                    {
                        return -1;
                    }
                    if (num3 > num4)
                    {
                        return 1;
                    }
                    if (id < card)
                    {
                        return -1;
                    }
                    if (id > card)
                    {
                        return 1;
                    }
                    return 0;
                }
            }

            public class CardsIDComparerReverse : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard y, CustomSelfDrawPanel.UICard x)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    int id = x.Definition.id;
                    int card = y.Definition.id;
                    int num3 = CardTypes.getCardType(id);
                    int num4 = CardTypes.getCardType(card);
                    if (num3 <= 0x110)
                    {
                        num3 += 0xa60;
                    }
                    if (num4 <= 0x110)
                    {
                        num4 += 0xa60;
                    }
                    if (num3 < num4)
                    {
                        return -1;
                    }
                    if (num3 > num4)
                    {
                        return 1;
                    }
                    if (id < card)
                    {
                        return -1;
                    }
                    if (id > card)
                    {
                        return 1;
                    }
                    return 0;
                }
            }

            public class CardsNameComparer : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard x, CustomSelfDrawPanel.UICard y)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    string str = CardTypes.getDescriptionFromCard(x.Definition.id).ToLower();
                    string strB = CardTypes.getDescriptionFromCard(y.Definition.id).ToLower();
                    return str.CompareTo(strB);
                }
            }

            public class CardsNameComparerReverse : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard y, CustomSelfDrawPanel.UICard x)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    string str = CardTypes.getDescriptionFromCard(x.Definition.id).ToLower();
                    string strB = CardTypes.getDescriptionFromCard(y.Definition.id).ToLower();
                    return str.CompareTo(strB);
                }
            }

            public class CardsPriceComparer : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard x, CustomSelfDrawPanel.UICard y)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    if (x.Definition.cardPoints > y.Definition.cardPoints)
                    {
                        return -1;
                    }
                    if (x.Definition.cardPoints >= y.Definition.cardPoints)
                    {
                        int id = x.Definition.id;
                        int card = y.Definition.id;
                        int num3 = CardTypes.getCardType(id);
                        int num4 = CardTypes.getCardType(card);
                        if (num3 <= 0x110)
                        {
                            num3 += 0xa60;
                        }
                        if (num4 <= 0x110)
                        {
                            num4 += 0xa60;
                        }
                        if (num3 < num4)
                        {
                            return -1;
                        }
                        if (num3 > num4)
                        {
                            return 1;
                        }
                        if (id < card)
                        {
                            return -1;
                        }
                        if (id > card)
                        {
                            return 1;
                        }
                    }
                    return 0;
                }
            }

            public class CardsPriceComparerReverse : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard y, CustomSelfDrawPanel.UICard x)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    if (x.Definition.cardPoints > y.Definition.cardPoints)
                    {
                        return -1;
                    }
                    if (x.Definition.cardPoints >= y.Definition.cardPoints)
                    {
                        int id = x.Definition.id;
                        int card = y.Definition.id;
                        int num3 = CardTypes.getCardType(id);
                        int num4 = CardTypes.getCardType(card);
                        if (num3 <= 0x110)
                        {
                            num3 += 0xa60;
                        }
                        if (num4 <= 0x110)
                        {
                            num4 += 0xa60;
                        }
                        if (num3 < num4)
                        {
                            return -1;
                        }
                        if (num3 > num4)
                        {
                            return 1;
                        }
                        if (id < card)
                        {
                            return -1;
                        }
                        if (id > card)
                        {
                            return 1;
                        }
                    }
                    return 0;
                }
            }

            public class CardsQuantityComparer : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard x, CustomSelfDrawPanel.UICard y)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    if (x.cardCount > y.cardCount)
                    {
                        return -1;
                    }
                    if (x.cardCount >= y.cardCount)
                    {
                        int id = x.Definition.id;
                        int card = y.Definition.id;
                        int num3 = CardTypes.getCardType(id);
                        int num4 = CardTypes.getCardType(card);
                        if (num3 <= 0x110)
                        {
                            num3 += 0xa60;
                        }
                        if (num4 <= 0x110)
                        {
                            num4 += 0xa60;
                        }
                        if (num3 < num4)
                        {
                            return -1;
                        }
                        if (num3 > num4)
                        {
                            return 1;
                        }
                        if (id < card)
                        {
                            return -1;
                        }
                        if (id > card)
                        {
                            return 1;
                        }
                    }
                    return 0;
                }
            }

            public class CardsQuantityComparerReverse : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard y, CustomSelfDrawPanel.UICard x)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    if (x.cardCount > y.cardCount)
                    {
                        return -1;
                    }
                    if (x.cardCount >= y.cardCount)
                    {
                        int id = x.Definition.id;
                        int card = y.Definition.id;
                        int num3 = CardTypes.getCardType(id);
                        int num4 = CardTypes.getCardType(card);
                        if (num3 <= 0x110)
                        {
                            num3 += 0xa60;
                        }
                        if (num4 <= 0x110)
                        {
                            num4 += 0xa60;
                        }
                        if (num3 < num4)
                        {
                            return -1;
                        }
                        if (num3 > num4)
                        {
                            return 1;
                        }
                        if (id < card)
                        {
                            return -1;
                        }
                        if (id > card)
                        {
                            return 1;
                        }
                    }
                    return 0;
                }
            }

            public class TUT2CardsNameComparer : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard x, CustomSelfDrawPanel.UICard y)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    string str = CardTypes.getDescriptionFromCard(x.Definition.id).ToLower();
                    string strB = CardTypes.getDescriptionFromCard(y.Definition.id).ToLower();
                    if (CardTypes.getCardType(x.Definition.id) == 0x301)
                    {
                        str = "00000";
                    }
                    if (CardTypes.getCardType(y.Definition.id) == 0x301)
                    {
                        strB = "00000";
                    }
                    return str.CompareTo(strB);
                }
            }

            public class TUTCardsNameComparer : IComparer<CustomSelfDrawPanel.UICard>
            {
                public int Compare(CustomSelfDrawPanel.UICard x, CustomSelfDrawPanel.UICard y)
                {
                    if ((x == null) || (x.Definition == null))
                    {
                        if ((y != null) && (y.Definition != null))
                        {
                            return -1;
                        }
                        return 0;
                    }
                    if ((y == null) || (y.Definition == null))
                    {
                        return 1;
                    }
                    string str = CardTypes.getDescriptionFromCard(x.Definition.id).ToLower();
                    string strB = CardTypes.getDescriptionFromCard(y.Definition.id).ToLower();
                    if (CardTypes.getCardType(x.Definition.id) == 0xc81)
                    {
                        str = "00000";
                    }
                    if (CardTypes.getCardType(y.Definition.id) == 0xc81)
                    {
                        strB = "00000";
                    }
                    return str.CompareTo(strB);
                }
            }
        }

        public class UICardOffer : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDImage baseImage = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDLabel cardLabel = new CustomSelfDrawPanel.CSDLabel();
            public CustomSelfDrawPanel.CSDLabel costLabel = new CustomSelfDrawPanel.CSDLabel();
            public CustomSelfDrawPanel.CSDImage crownImage = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDLabel descLabel = new CustomSelfDrawPanel.CSDLabel();
            public CustomSelfDrawPanel.CSDImage fanImage = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
            public CardTypes.CardOffer Offer;
            public CustomSelfDrawPanel.CSDImage packImage = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDImage packOverImage = new CustomSelfDrawPanel.CSDImage();
        }

        public class UICardPack : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDImage baseImage = new CustomSelfDrawPanel.CSDImage();
            public CustomSelfDrawPanel.CSDLabel descriptionLabel = new CustomSelfDrawPanel.CSDLabel();
            public CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
            public string nameText = string.Empty;
            public CustomSelfDrawPanel.CSDImage overImage = new CustomSelfDrawPanel.CSDImage();
            public List<int> PackIDs = new List<int>();
        }

        public class UICardsButtons : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDButton buyButton;
            public PlayCardsWindow cardsWindow;
            public CustomSelfDrawPanel.CSDButton crownsButton;
            public CustomSelfDrawPanel.CSDButton inviteButton;
            public CustomSelfDrawPanel.CSDButton manageButton;
            public bool mAvailable;
            public CustomSelfDrawPanel.CSDButton premiumButton;

            public UICardsButtons(PlayCardsWindow window)
            {
                this.cardsWindow = window;
                this.mAvailable = true;
                this.buyButton = new CustomSelfDrawPanel.CSDButton();
                this.premiumButton = new CustomSelfDrawPanel.CSDButton();
                this.crownsButton = new CustomSelfDrawPanel.CSDButton();
                this.manageButton = new CustomSelfDrawPanel.CSDButton();
                this.inviteButton = new CustomSelfDrawPanel.CSDButton();
                this.buyButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_getcards_normal;
                this.premiumButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_getpremium_normal;
                this.crownsButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_buycrowns_normal;
                this.manageButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_choose_cards_normal;
                this.inviteButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_v2_friend_normal;
                this.buyButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_getcards_over;
                this.premiumButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_getpremium_over;
                this.crownsButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_buycrowns_over;
                this.manageButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_choose_cards_over;
                this.inviteButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_v2_friend_over;
                CustomSelfDrawPanel.CSDButton buyButton = null;
                switch (window.CurrentPanelID)
                {
                    case 2:
                        buyButton = this.buyButton;
                        break;

                    case 4:
                        buyButton = this.premiumButton;
                        break;

                    case 6:
                        buyButton = this.manageButton;
                        break;

                    case 7:
                        buyButton = this.crownsButton;
                        break;
                }
                if (buyButton != null)
                {
                    buyButton.ImageNorm = (Image) GFXLibrary.cardpanel_RH_button_back_normal;
                    buyButton.ImageOver = (Image) GFXLibrary.cardpanel_RH_button_back_over;
                    CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                        Position = new Point(0, 0x1f),
                        Size = buyButton.Size,
                        Text = SK.Text("CARDS_BackToPlayCarads", "Back to Play Cards"),
                        Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                        Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular),
                        Color = ARGBColors.Black
                    };
                    buyButton.addControl(control);
                }
                this.inviteButton.Position = new Point(11, 7);
                this.manageButton.Position = new Point(11, 0x75);
                this.buyButton.Position = new Point(11, 0xe3);
                this.premiumButton.Position = new Point(11, 0x151);
                this.crownsButton.Position = new Point(11, 0x1bf);
                this.buyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyclick), "UICardsButtons_get_cards");
                this.premiumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.premiumclick), "UICardsButtons_premium");
                this.crownsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsWindow.GetCrowns), "UICardsButtons_get_crowns");
                this.manageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.manageclick), "UICardsButtons_swap_cards");
                this.inviteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsWindow.InviteAFriend), "UICardsButtons_invite_a_friend");
                this.Size = new Size(200, ((this.buyButton.Height + this.premiumButton.Height) + this.crownsButton.Height) + this.manageButton.Height);
                base.addControl(this.buyButton);
                base.addControl(this.premiumButton);
                base.addControl(this.crownsButton);
                base.addControl(this.manageButton);
                if ((GameEngine.Instance.World.isBigpointAccount || Program.bigpointInstall) || (Program.aeriaInstall || Program.bigpointPartnerInstall))
                {
                    this.manageButton.Position = new Point(11, 0x3e);
                    this.buyButton.Position = new Point(11, 0xac);
                    this.premiumButton.Position = new Point(11, 0x11a);
                    this.crownsButton.Position = new Point(11, 0x188);
                }
                else
                {
                    base.addControl(this.inviteButton);
                }
            }

            public void buyclick()
            {
                this.cardsWindow.SwitchPanel(2);
            }

            public void manageclick()
            {
                this.cardsWindow.SwitchPanel(6);
            }

            public void premiumclick()
            {
                this.cardsWindow.SwitchPanel(4);
            }

            public bool Available
            {
                get
                {
                    return this.mAvailable;
                }
                set
                {
                    this.mAvailable = value;
                    this.buyButton.Enabled = this.mAvailable;
                    this.crownsButton.Enabled = this.mAvailable;
                    this.manageButton.Enabled = this.mAvailable;
                    this.premiumButton.Enabled = this.mAvailable;
                    this.inviteButton.Enabled = this.mAvailable;
                    if (this.mAvailable)
                    {
                    }
                }
            }
        }

        public class WikiLinkControl : CustomSelfDrawPanel.CSDButton
        {
            public const int ACHIEVEMENTS = 0x2c;
            public const int ATTACKS = 20;
            public const int BUY_CARDS = 0x27;
            public const int BUY_CROWNS = 0x26;
            public const int BUY_PREMIUM_TOKENS = 0x25;
            public const int CARDS = 0x19;
            public const int CHAT = 0x1b;
            public const int COAT_OF_ARMS = 0x1f;
            public const int DOMINATION_RULES = 0x2f;
            public const int DONATE_TO_PARISH = 0x2a;
            public const int FACTIONS_HOUSES_FACTION = 0x17;
            public const int FACTIONS_HOUSES_GLORY = 0x16;
            public const int FACTIONS_HOUSES_HOUSE = 0x18;
            public const int FIFTHAGE = 0x33;
            public const int FOURTHAGE = 0x30;
            public const int LEADERBOARD = 0x1c;
            public const int LOGOUT = 0x29;
            private int m_ID = -1;
            public const int MAIL = 0x1a;
            public const int MY_ACCOUNT = 0x1d;
            public const int PALADIN_CASTLES = 50;
            public const int PARISH_CAPITAL_CAPITAL_INFO = 14;
            public const int PARISH_CAPITAL_CASTLE_MAP = 10;
            public const int PARISH_CAPITAL_PARISH_FORUM = 0x10;
            public const int PARISH_CAPITAL_RESOURCES = 11;
            public const int PARISH_CAPITAL_TRADE = 12;
            public const int PARISH_CAPITAL_TROOPS = 13;
            public const int PARISH_CAPITAL_VILLAGE_MAP = 9;
            public const int PARISH_CAPITAL_VOTE = 15;
            public const int QUEST_WHEEL = 0x20;
            public const int QUESTS = 0x13;
            public const int RANK = 0x12;
            public const int REPORTS = 0x15;
            public const int RESEARCH = 0x11;
            public const int SECONDAGE = 0x2d;
            public const int SEND_ATTACK = 0x21;
            public const int SEND_MONKS = 0x23;
            public const int SEND_SCOUTS = 0x22;
            public const int SEND_TRADER_MERCHANTS = 0x24;
            public const int SETTINGS = 30;
            public const int SWAP_CARDS = 40;
            public const int THIRDAGE = 0x2e;
            public const int TREASURE_CASTLES = 0x31;
            private static string[] urls = new string[] { 
                "http://help.strongholdkingdoms.com/index.php/World_Map", "http://hilfe.strongholdkingdoms.de/index.php/Weltkarte", "http://aide.strongholdkingdoms.com/index.php/Carte_du_Monde", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9A%D0%B0%D1%80%D1%82%D0%B0_%D0%BC%D0%B8%D1%80%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Mapa_del_mundo", "http://pomoc.strongholdkingdoms.com/index.php/Mapa_%C5%9Bwiata", "http://yardim.strongholdkingdoms.com/index.php/D%C3%BCnya_Haritas%C4%B1", "http://aiuto.strongholdkingdoms.com/index.php/Mappa_del_mondo", "http://ajuda.strongholdkingdoms.com/index.php/Mapa_do_mundo", "", "", "http://help.strongholdkingdoms.com/index.php/Villages", "http://hilfe.strongholdkingdoms.de/index.php/D%C3%B6rfer", "http://aide.strongholdkingdoms.com/index.php/Village", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A7%D0%B0%D0%92%D0%BE:%D0%92%D0%B0%D1%88%D0%B0_%D0%B4%D0%B5%D1%80%D0%B5%D0%B2%D0%BD%D1%8F", "http://ayuda.strongholdkingdoms.com/index.php/Aldeas", 
                "http://pomoc.strongholdkingdoms.com/index.php/Wioski", "http://yardim.strongholdkingdoms.com/index.php/K%C3%B6yler", "http://aiuto.strongholdkingdoms.com/index.php/Villaggi", "http://ajuda.strongholdkingdoms.com/index.php/Aldeia", "", "", "http://help.strongholdkingdoms.com/index.php/Castles", "http://hilfe.strongholdkingdoms.de/index.php/Burgen", "http://aide.strongholdkingdoms.com/index.php/Ch%C3%A2teau", "http://help.ru.strongholdkingdoms.com/index.php/%D0%97%D0%B0%D0%BC%D0%BA%D0%B8", "http://ayuda.strongholdkingdoms.com/index.php/Castillos", "http://pomoc.strongholdkingdoms.com/index.php/Zamki", "http://yardim.strongholdkingdoms.com/index.php/Kaleler", "http://aiuto.strongholdkingdoms.com/index.php/Castelli", "http://ajuda.strongholdkingdoms.com/index.php/Castelos", "", 
                "", "http://help.strongholdkingdoms.com/index.php/Resources", "http://hilfe.strongholdkingdoms.de/index.php/Ressourcen", "http://aide.strongholdkingdoms.com/index.php/Ressources", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Recursos", "http://pomoc.strongholdkingdoms.com/index.php/Surowce", "http://yardim.strongholdkingdoms.com/index.php/Kaynaklar", "http://aiuto.strongholdkingdoms.com/index.php/Risorse", "http://ajuda.strongholdkingdoms.com/index.php/Recursos", "", "", "http://help.strongholdkingdoms.com/index.php/Trading", "http://hilfe.strongholdkingdoms.de/index.php/Handel", "http://aide.strongholdkingdoms.com/index.php/Commerce", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A7%D0%B0%D0%92%D0%BE:%D0%9D%D0%B0%D0%BB%D0%BE%D0%B3%D0%B8_%D0%B8_%D1%82%D0%BE%D1%80%D0%B3%D0%BE%D0%B2%D0%BB%D1%8F", 
                "http://ayuda.strongholdkingdoms.com/index.php/Comerciar", "http://pomoc.strongholdkingdoms.com/index.php/Handel", "http://yardim.strongholdkingdoms.com/index.php/Ticaret", "http://aiuto.strongholdkingdoms.com/index.php/Commercio", "http://ajuda.strongholdkingdoms.com/index.php/Com\x00e9rcio", "", "", "http://help.strongholdkingdoms.com/index.php/Barracks", "http://hilfe.strongholdkingdoms.de/index.php/Kaserne", "http://aide.strongholdkingdoms.com/index.php/Garnison", "http://help.ru.strongholdkingdoms.com/index.php/%D0%91%D0%B0%D1%80%D0%B0%D0%BA%D0%B8", "http://ayuda.strongholdkingdoms.com/index.php/Barracas", "http://pomoc.strongholdkingdoms.com/index.php/Koszary", "http://yardim.strongholdkingdoms.com/index.php/K%C4%B1%C5%9Flalar", "http://aiuto.strongholdkingdoms.com/index.php/Guarnigione", "http://ajuda.strongholdkingdoms.com/index.php/Quartel", 
                "", "", "http://help.strongholdkingdoms.com/index.php/Units", "http://hilfe.strongholdkingdoms.de/index.php/Einheiten", "http://aide.strongholdkingdoms.com/index.php/Sp%C3%A9cialistes", "http://help.ru.strongholdkingdoms.com/index.php/%D0%AE%D0%BD%D0%B8%D1%82%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Unidades", "http://pomoc.strongholdkingdoms.com/index.php/Jednostki", "http://yardim.strongholdkingdoms.com/index.php/Birimler", "http://aiuto.strongholdkingdoms.com/index.php/Unit%C3%A0", "http://ajuda.strongholdkingdoms.com/index.php/Unidades", "", "", "http://help.strongholdkingdoms.com/index.php/Banquets", "http://hilfe.strongholdkingdoms.de/index.php/Bankette", "http://aide.strongholdkingdoms.com/index.php/Banquets", 
                "http://help.ru.strongholdkingdoms.com/index.php/%D0%9F%D0%BE%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B8:%D0%91%D0%B0%D0%BD%D0%BA%D0%B5%D1%82%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Banquetes", "http://pomoc.strongholdkingdoms.com/index.php/Uczty", "http://yardim.strongholdkingdoms.com/index.php/Ziyafetler", "http://aiuto.strongholdkingdoms.com/index.php/Banchetti", "http://ajuda.strongholdkingdoms.com/index.php/Banquete", "", "", "http://help.strongholdkingdoms.com/index.php/Vassals_%26_Liege_Lords", "http://hilfe.strongholdkingdoms.de/index.php/Vassalle_%26_Lehnsherren", "http://aide.strongholdkingdoms.com/index.php/Vassaux", "http://help.ru.strongholdkingdoms.com/index.php/%D0%92%D0%B0%D1%81%D1%81%D0%B0%D0%BB%D1%8B_%D0%B8_%D1%81%D0%B5%D0%BD%D1%8C%D0%BE%D1%80%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Vasallos_y_se%C3%B1ores_feudales", "http://pomoc.strongholdkingdoms.com/index.php/Wasale_i_seniorzy", "http://yardim.strongholdkingdoms.com/index.php/Vasallar_ve_S%C3%BCzerenler", "http://aiuto.strongholdkingdoms.com/index.php/Vassalli_e_feudatari", 
                "http://ajuda.strongholdkingdoms.com/index.php/Vassalos_%26_Senhores_feudais", "", "", "http://help.strongholdkingdoms.com/index.php/Parishes_%26_Capitals#Capital_Town", "http://hilfe.strongholdkingdoms.de/index.php/Gemeinden_%26_Hauptst%C3%A4dte", "http://aide.strongholdkingdoms.com/index.php/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales#Bourg_de_capitale", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Parroquias_y_capitales", "http://pomoc.strongholdkingdoms.com/index.php/Flagami", "http://yardim.strongholdkingdoms.com/index.php/Pari%C5%9Fler_ve_Ba%C5%9Fkentler", "http://aiuto.strongholdkingdoms.com/index.php/Distretti_e_capitali", "http://ajuda.strongholdkingdoms.com/index.php/Par%C3%B3quias_e_Capitais#Capital", "", "", "http://help.strongholdkingdoms.com/index.php/Parishes_%26_Capitals#Capital_Castle", "http://hilfe.strongholdkingdoms.de/index.php/Gemeinden_%26_Hauptst%C3%A4dte", 
                "http://aide.strongholdkingdoms.com/index.php/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales#Ch.C3.A2teau_d.27une_capitale", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Parroquias_y_capitales", "http://pomoc.strongholdkingdoms.com/index.php/Flagami", "http://yardim.strongholdkingdoms.com/index.php/Pari%C5%9Fler_ve_Ba%C5%9Fkentler", "http://aiuto.strongholdkingdoms.com/index.php/Distretti_e_capitali#Castello_della_capitale", "http://ajuda.strongholdkingdoms.com/index.php/Par%C3%B3quias_e_Capitais#Castelo_da_capital", "", "", "http://help.strongholdkingdoms.com/index.php/Resources", "http://hilfe.strongholdkingdoms.de/index.php/Ressourcen", "http://aide.strongholdkingdoms.com/index.php/Ressources", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A0%D0%B5%D1%81%D1%83%D1%80%D1%81%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Recursos", "http://pomoc.strongholdkingdoms.com/index.php/Surowce", "http://yardim.strongholdkingdoms.com/index.php/Kaynaklar", 
                "http://aiuto.strongholdkingdoms.com/index.php/Risorse", "http://ajuda.strongholdkingdoms.com/index.php/Recursos", "", "", "http://help.strongholdkingdoms.com/index.php/Parishes_%26_Capitals", "http://hilfe.strongholdkingdoms.de/index.php/Gemeinden_%26_Hauptst%C3%A4dte", "http://aide.strongholdkingdoms.com/index.php/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Parroquias_y_capitales", "http://pomoc.strongholdkingdoms.com/index.php/Flagami", "http://yardim.strongholdkingdoms.com/index.php/Pari%C5%9Fler_ve_Ba%C5%9Fkentler", "http://aiuto.strongholdkingdoms.com/index.php/Capitale", "http://ajuda.strongholdkingdoms.com/index.php/Par%C3%B3quias_e_Capitais", "", "", "http://help.strongholdkingdoms.com/index.php/Barracks", 
                "http://hilfe.strongholdkingdoms.de/index.php/Kaserne", "http://aide.strongholdkingdoms.com/index.php/Garnison", "http://help.ru.strongholdkingdoms.com/index.php/%D0%91%D0%B0%D1%80%D0%B0%D0%BA%D0%B8", "http://ayuda.strongholdkingdoms.com/index.php/Barracas", "http://pomoc.strongholdkingdoms.com/index.php/Koszary", "http://yardim.strongholdkingdoms.com/index.php/Pari%C5%9Fler_ve_Ba%C5%9Fkentler", "http://aiuto.strongholdkingdoms.com/index.php/Guarnigione", "http://ajuda.strongholdkingdoms.com/index.php/Quartel", "", "", "http://help.strongholdkingdoms.com/index.php/Parishes_%26_Capitals", "http://hilfe.strongholdkingdoms.de/index.php/Gemeinden_%26_Hauptst%C3%A4dte", "http://aide.strongholdkingdoms.com/index.php/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Parroquias_y_capitales", "http://pomoc.strongholdkingdoms.com/index.php/Flagami", 
                "http://yardim.strongholdkingdoms.com/index.php/Pari%C5%9Fler_ve_Ba%C5%9Fkentler", "http://aiuto.strongholdkingdoms.com/index.php/Capitale", "http://ajuda.strongholdkingdoms.com/index.php/Par%C3%B3quias_e_Capitais", "", "", "http://help.strongholdkingdoms.com/index.php/Parishes_%26_Capitals#Voting", "http://hilfe.strongholdkingdoms.de/index.php/Gemeinden_%26_Hauptst%C3%A4dte#Abstimmen", "http://aide.strongholdkingdoms.com/index.php/Pr%C3%A9v%C3%B4t%C3%A9s_et_Capitales#.C3.89lection", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%BA%D1%80%D1%83%D0%B3%D0%B0_%D0%B8_%D1%81%D1%82%D0%BE%D0%BB%D0%B8%D1%86%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Parroquias_y_capitales", "http://pomoc.strongholdkingdoms.com/index.php/Flagami", "http://yardim.strongholdkingdoms.com/index.php/Pari%C5%9Fler_ve_Ba%C5%9Fkentler", "http://aiuto.strongholdkingdoms.com/index.php/Capitale#Votare", "http://ajuda.strongholdkingdoms.com/index.php/Par%C3%B3quias_e_Capitais", "", "", 
                "http://help.strongholdkingdoms.com/index.php/Communication", "http://hilfe.strongholdkingdoms.de/index.php/Kommunikation", "http://aide.strongholdkingdoms.com/index.php/Communication", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%B1%D1%89%D0%B5%D0%BD%D0%B8%D0%B5", "http://ayuda.strongholdkingdoms.com/index.php/Medios_para_comunicarse", "http://pomoc.strongholdkingdoms.com/index.php/Czatu", "http://yardim.strongholdkingdoms.com/index.php/%C4%B0leti%C5%9Fim", "http://aiuto.strongholdkingdoms.com/index.php/Comunicazioni", "http://ajuda.strongholdkingdoms.com/index.php/Comunica\x00e7\x00e3o", "", "", "http://help.strongholdkingdoms.com/index.php/Research", "http://hilfe.strongholdkingdoms.de/index.php/Forschung", "http://aide.strongholdkingdoms.com/index.php/Recherches", "http://help.ru.strongholdkingdoms.com/index.php/%D0%98%D1%81%D1%81%D0%BB%D0%B5%D0%B4%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F", "http://ayuda.strongholdkingdoms.com/index.php/Investigaci%C3%B3n", 
                "http://pomoc.strongholdkingdoms.com/index.php/Badania", "http://yardim.strongholdkingdoms.com/index.php/Ara%C5%9Ft%C4%B1rma", "http://aiuto.strongholdkingdoms.com/index.php/Ricerca", "http://ajuda.strongholdkingdoms.com/index.php/Pesquisar", "", "", "http://help.strongholdkingdoms.com/index.php/Ranks", "http://hilfe.strongholdkingdoms.de/index.php/Rang", "http://aide.strongholdkingdoms.com/index.php/Rangs", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A0%D0%B0%D0%BD%D0%B3%D0%B8", "http://ayuda.strongholdkingdoms.com/index.php/Niveles", "http://pomoc.strongholdkingdoms.com/index.php/Rang", "http://yardim.strongholdkingdoms.com/index.php/Mertebeler", "http://aiuto.strongholdkingdoms.com/index.php/Ranghi", "http://ajuda.strongholdkingdoms.com/index.php/Postos", "", 
                "", "http://help.strongholdkingdoms.com/index.php/Quests", "http://hilfe.strongholdkingdoms.de/index.php/Quest", "http://aide.strongholdkingdoms.com/index.php/Qu%C3%AAtes", "http://help.ru.strongholdkingdoms.com/index.php/%D0%97%D0%B0%D0%B4%D0%B0%D0%BD%D0%B8%D1%8F", "http://ayuda.strongholdkingdoms.com/index.php/Misiones", "http://pomoc.strongholdkingdoms.com/index.php/Misji", "http://yardim.strongholdkingdoms.com/index.php/G%C3%B6revler", "http://aiuto.strongholdkingdoms.com/index.php/Missioni", "http://ajuda.strongholdkingdoms.com/index.php/Miss\x00f5es", "", "", "http://help.strongholdkingdoms.com/index.php/Combat", "http://hilfe.strongholdkingdoms.de/index.php/Kampf", "http://aide.strongholdkingdoms.com/index.php/Combat", "http://help.ru.strongholdkingdoms.com/index.php/%D0%91%D0%B8%D1%82%D0%B2%D0%B0", 
                "http://ayuda.strongholdkingdoms.com/index.php/Combate", "http://pomoc.strongholdkingdoms.com/index.php/Walka", "http://yardim.strongholdkingdoms.com/index.php/Muharebe", "http://aiuto.strongholdkingdoms.com/index.php/Combattimento", "http://ajuda.strongholdkingdoms.com/index.php/Combate", "", "", "http://help.strongholdkingdoms.com/index.php/Reports", "http://hilfe.strongholdkingdoms.de/index.php/Berichte", "http://aide.strongholdkingdoms.com/index.php/Rapports", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D1%82%D1%87%D1%91%D1%82%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Informes", "http://pomoc.strongholdkingdoms.com/index.php/Raporty", "http://yardim.strongholdkingdoms.com/index.php/Raporlar", "http://aiuto.strongholdkingdoms.com/index.php/Rapporti", "http://ajuda.strongholdkingdoms.com/index.php/Relat\x00f3rios", 
                "", "", "http://help.strongholdkingdoms.com/index.php/Glory", "http://hilfe.strongholdkingdoms.de/index.php/Herrlichkeit", "http://aide.strongholdkingdoms.com/index.php/Gloire", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A1%D0%BB%D0%B0%D0%B2%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Gloria", "http://pomoc.strongholdkingdoms.com/index.php/Punkty_chwa%C5%82y", "http://yardim.strongholdkingdoms.com/index.php/%C5%9Ean", "http://aiuto.strongholdkingdoms.com/index.php/Gloria", "http://ajuda.strongholdkingdoms.com/index.php/Gl\x00f3ria", "", "", "http://help.strongholdkingdoms.com/index.php/Factions_%26_Houses", "http://hilfe.strongholdkingdoms.de/index.php/H%C3%A4user", "http://aide.strongholdkingdoms.com/index.php/Maisons", 
                "http://help.ru.strongholdkingdoms.com/index.php/%D0%A4%D1%80%D0%B0%D0%BA%D1%86%D0%B8%D0%B8_%D0%B8_%D0%94%D0%BE%D0%BC%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Facciones_y_Casas", "http://pomoc.strongholdkingdoms.com/index.php/Dom", "http://yardim.strongholdkingdoms.com/index.php/%C4%B0htilaflar_ve_Haneler", "http://aiuto.strongholdkingdoms.com/index.php/Fazioni_e_casati", "http://ajuda.strongholdkingdoms.com/index.php/Fac\x00e7\x00f5es_e_Casas", "", "", "http://help.strongholdkingdoms.com/index.php/Factions_%26_Houses", "http://hilfe.strongholdkingdoms.de/index.php/H%C3%A4user", "http://aide.strongholdkingdoms.com/index.php/Maisons", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A4%D1%80%D0%B0%D0%BA%D1%86%D0%B8%D0%B8_%D0%B8_%D0%94%D0%BE%D0%BC%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Facciones_y_Casas", "http://pomoc.strongholdkingdoms.com/index.php/Dom", "http://yardim.strongholdkingdoms.com/index.php/%C4%B0htilaflar_ve_Haneler", "http://aiuto.strongholdkingdoms.com/index.php/Fazioni_e_casati", 
                "http://ajuda.strongholdkingdoms.com/index.php/Fac\x00e7\x00f5es_e_Casas", "", "", "http://help.strongholdkingdoms.com/index.php/Strategy_Cards", "http://hilfe.strongholdkingdoms.de/index.php/Strategiekarten", "http://aide.strongholdkingdoms.com/index.php/Cartes_Strat%C3%A9giques", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A1%D1%82%D1%80%D0%B0%D1%82%D0%B5%D0%B3%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B5_%D0%BA%D0%B0%D1%80%D1%82%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Cartas_de_estrategia", "http://pomoc.strongholdkingdoms.com/index.php/Karty_strategiczne", "http://yardim.strongholdkingdoms.com/index.php/%C3%9Ccretsiz_Strateji_Kartlar%C4%B1", "http://aiuto.strongholdkingdoms.com/index.php/Carte_strategiche", "http://ajuda.strongholdkingdoms.com/index.php/Cartas_de_estrat\x00e9gia", "", "", "http://help.strongholdkingdoms.com/index.php/Communication", "http://hilfe.strongholdkingdoms.de/index.php/Kommunikation", 
                "http://aide.strongholdkingdoms.com/index.php/Communication", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%B1%D1%89%D0%B5%D0%BD%D0%B8%D0%B5", "http://ayuda.strongholdkingdoms.com/index.php/Medios_para_comunicarse", "http://pomoc.strongholdkingdoms.com/index.php/Czatu", "http://yardim.strongholdkingdoms.com/index.php/%C4%B0leti%C5%9Fim", "http://aiuto.strongholdkingdoms.com/index.php/Comunicazioni", "http://ajuda.strongholdkingdoms.com/index.php/Comunica\x00e7\x00e3o", "", "", "http://help.strongholdkingdoms.com/index.php/Communication", "http://hilfe.strongholdkingdoms.de/index.php/Kommunikation", "http://aide.strongholdkingdoms.com/index.php/Communication", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%B1%D1%89%D0%B5%D0%BD%D0%B8%D0%B5", "http://ayuda.strongholdkingdoms.com/index.php/Medios_para_comunicarse", "http://pomoc.strongholdkingdoms.com/index.php/Czatu", "http://yardim.strongholdkingdoms.com/index.php/%C4%B0leti%C5%9Fim", 
                "http://aiuto.strongholdkingdoms.com/index.php/Comunicazioni", "http://ajuda.strongholdkingdoms.com/index.php/Comunica\x00e7\x00e3o", "", "", "http://help.strongholdkingdoms.com/index.php/Leaderboard", "http://hilfe.strongholdkingdoms.de/index.php/Bestenliste", "http://aide.strongholdkingdoms.com/index.php/Classement", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A0%D0%B5%D0%B9%D1%82%D0%B8%D0%BD%D0%B3", "http://ayuda.strongholdkingdoms.com/index.php/Tabla_de_clasificaci%C3%B3n", "http://pomoc.strongholdkingdoms.com/index.php/Tablica_wynik%C3%B3w", "http://yardim.strongholdkingdoms.com/index.php/Liderlik_Tablosu", "http://aiuto.strongholdkingdoms.com/index.php/Classifica", "http://ajuda.strongholdkingdoms.com/index.php/Placar_de_L\x00edderes", "", "", "http://help.strongholdkingdoms.com/index.php/Account_Details", 
                "http://hilfe.strongholdkingdoms.de/index.php/Konten_Einzelheiten", "http://aide.strongholdkingdoms.com/index.php/Profil_Joueur", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A3%D1%87%D0%B5%D1%82%D0%BD%D0%B0%D1%8F_%D0%B7%D0%B0%D0%BF%D0%B8%D1%81%D1%8C", "http://ayuda.strongholdkingdoms.com/index.php/Detalles_de_la_cuenta", "http://pomoc.strongholdkingdoms.com/index.php/Szczeg%C3%B3%C5%82y_konta", "http://yardim.strongholdkingdoms.com/index.php/Hesap_Bilgileri", "http://aiuto.strongholdkingdoms.com/index.php/Dettagli_dell%E2%80%99account", "http://ajuda.strongholdkingdoms.com/index.php/Detalhes_da_conta", "", "", "http://help.strongholdkingdoms.com/index.php/Options_%26_Settings", "http://hilfe.strongholdkingdoms.de/index.php/Optionen/Einstellungen", "http://aide.strongholdkingdoms.com/index.php/Options", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9D%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B8", "http://ayuda.strongholdkingdoms.com/index.php/Opciones_y_configuraci%C3%B3n", "http://pomoc.strongholdkingdoms.com/index.php/Opcje_i_ustawienia", 
                "http://yardim.strongholdkingdoms.com/index.php/Se%C3%A7enekler_ve_Ayarlar", "http://aiuto.strongholdkingdoms.com/index.php/Opzioni_e_impostazioni", "http://ajuda.strongholdkingdoms.com/index.php/Op\x00e7\x00f5es_e_configura\x00e7\x00f5es", "", "", "http://help.strongholdkingdoms.com/index.php/Coat_of_Arms", "http://hilfe.strongholdkingdoms.de/index.php/Wappen", "http://aide.strongholdkingdoms.com/index.php/Armoiries", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A1%D0%BE%D0%B7%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5_%D0%B3%D0%B5%D1%80%D0%B1%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Escudo_de_armas", "http://pomoc.strongholdkingdoms.com/index.php/Herb", "http://yardim.strongholdkingdoms.com/index.php/Armal%C4%B1_Kalkan", "http://aiuto.strongholdkingdoms.com/index.php/Blasone", "http://ajuda.strongholdkingdoms.com/index.php/Bras\x00e3o", "", "", 
                "http://help.strongholdkingdoms.com/index.php/Wheel", "http://hilfe.strongholdkingdoms.de/index.php/Quest_Gl%C3%BCcksrad", "http://aide.strongholdkingdoms.com/index.php/Roue_des_Qu%C3%AAtes", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9A%D0%BE%D0%BB%D0%B5%D1%81%D0%BE", "http://ayuda.strongholdkingdoms.com/index.php/Ruleta", "http://pomoc.strongholdkingdoms.com/index.php/Ko%C5%82o", "http://yardim.strongholdkingdoms.com/index.php/%C3%87ark", "http://aiuto.strongholdkingdoms.com/index.php/Ruota", "http://ajuda.strongholdkingdoms.com/index.php/Roda", "", "", "http://help.strongholdkingdoms.com/index.php/Combat", "http://hilfe.strongholdkingdoms.de/index.php/Kampf", "http://aide.strongholdkingdoms.com/index.php/Combat", "http://help.ru.strongholdkingdoms.com/index.php/%D0%91%D0%B8%D1%82%D0%B2%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Combate", 
                "http://pomoc.strongholdkingdoms.com/index.php/Walki", "http://yardim.strongholdkingdoms.com/index.php/Muharebe", "http://aiuto.strongholdkingdoms.com/index.php/Combattimento", "http://ajuda.strongholdkingdoms.com/index.php/Combate", "", "", "http://help.strongholdkingdoms.com/index.php/Scouts", "http://hilfe.strongholdkingdoms.de/index.php/Kundschafter", "http://aide.strongholdkingdoms.com/index.php/Eclaireurs", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A0%D0%B0%D0%B7%D0%B2%D0%B5%D0%B4%D0%BA%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Exploradores", "http://pomoc.strongholdkingdoms.com/index.php/Zwiadu", "http://yardim.strongholdkingdoms.com/index.php/Ke%C5%9Fif_Erleri", "http://aiuto.strongholdkingdoms.com/index.php/Esploratori", "http://ajuda.strongholdkingdoms.com/index.php/Batedores", "", 
                "", "http://help.strongholdkingdoms.com/index.php/Monks", "http://hilfe.strongholdkingdoms.de/index.php/M%C3%B6nche", "http://aide.strongholdkingdoms.com/index.php/Moine", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9C%D0%BE%D0%BD%D0%B0%D1%85", "http://ayuda.strongholdkingdoms.com/index.php/Monjes", "http://pomoc.strongholdkingdoms.com/index.php/Mnichom", "http://yardim.strongholdkingdoms.com/index.php/Ke%C5%9Fi%C5%9Fler", "http://aiuto.strongholdkingdoms.com/index.php/Monaco", "http://ajuda.strongholdkingdoms.com/index.php/Monges", "", "", "http://help.strongholdkingdoms.com/index.php/Trading", "http://hilfe.strongholdkingdoms.de/index.php/Handel", "http://aide.strongholdkingdoms.com/index.php/Commerce", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9F%D1%80%D0%B5%D0%BC%D0%B8%D1%83%D0%BC-%D0%B6%D0%B5%D1%82%D0%BE%D0%BD%D1%8B", 
                "http://ayuda.strongholdkingdoms.com/index.php/Comerciar", "http://pomoc.strongholdkingdoms.com/index.php/Handel", "http://yardim.strongholdkingdoms.com/index.php/Ticaret", "http://aiuto.strongholdkingdoms.com/index.php/Commercio", "http://ajuda.strongholdkingdoms.com/index.php/Com\x00e9rcio", "", "", "http://help.strongholdkingdoms.com/index.php/Premium_Tokens", "http://hilfe.strongholdkingdoms.de/index.php/Premium-Token", "http://aide.strongholdkingdoms.com/index.php/Jetons_Premium", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9F%D1%80%D0%B5%D0%BC%D0%B8%D1%83%D0%BC-%D0%B6%D0%B5%D1%82%D0%BE%D0%BD%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Vales_Premium", "http://pomoc.strongholdkingdoms.com/index.php/Premium", "http://yardim.strongholdkingdoms.com/index.php/Premium_Token", "http://aiuto.strongholdkingdoms.com/index.php/Gettoni_Premium", "http://ajuda.strongholdkingdoms.com/index.php/Fichas_pr\x00eamio", 
                "", "", "http://help.strongholdkingdoms.com/index.php/Firefly_Crowns", "http://hilfe.strongholdkingdoms.de/index.php/Firefly-Kronen", "http://aide.strongholdkingdoms.com/index.php/Couronne", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9A%D1%80%D0%BE%D0%BD%D1%8B_Firefly", "http://ayuda.strongholdkingdoms.com/index.php/Coronas_Firefly", "http://pomoc.strongholdkingdoms.com/index.php/Korony", "http://yardim.strongholdkingdoms.com/index.php/Firefly_Sikkeleri", "http://aiuto.strongholdkingdoms.com/index.php/Corone_Firefly", "http://ajuda.strongholdkingdoms.com/index.php/Coroas_Firefly", "", "", "http://help.strongholdkingdoms.com/index.php/Strategy_Cards", "http://hilfe.strongholdkingdoms.de/index.php/Strategiekarten", "http://aide.strongholdkingdoms.com/index.php/Cartes_Strat%C3%A9giques", 
                "http://help.ru.strongholdkingdoms.com/index.php/%D0%A1%D1%82%D1%80%D0%B0%D1%82%D0%B5%D0%B3%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B5_%D0%BA%D0%B0%D1%80%D1%82%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Cartas_de_estrategia", "http://pomoc.strongholdkingdoms.com/index.php/Karty_strategiczne", "http://yardim.strongholdkingdoms.com/index.php/Strateji_Kartlar%C4%B1", "http://aiuto.strongholdkingdoms.com/index.php/Carte_strategiche", "http://ajuda.strongholdkingdoms.com/index.php/Cartas_de_estrat\x00e9gia", "", "", "http://help.strongholdkingdoms.com/index.php/Strategy_Cards", "http://hilfe.strongholdkingdoms.de/index.php/Strategiekarten", "http://aide.strongholdkingdoms.com/index.php/Cartes_Strat%C3%A9giques", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A1%D1%82%D1%80%D0%B0%D1%82%D0%B5%D0%B3%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B5_%D0%BA%D0%B0%D1%80%D1%82%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Cartas_de_estrategia", "http://pomoc.strongholdkingdoms.com/index.php/Karty_strategiczne", "http://yardim.strongholdkingdoms.com/index.php/Strateji_Kartlar%C4%B1", "http://aiuto.strongholdkingdoms.com/index.php/Carte_strategiche", 
                "http://ajuda.strongholdkingdoms.com/index.php/Cartas_de_estrat\x00e9gia", "", "", "http://help.strongholdkingdoms.com/index.php/Premium_Tokens", "http://hilfe.strongholdkingdoms.de/index.php/Premium-Token", "http://aide.strongholdkingdoms.com/index.php/Jetons_Premium", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9F%D1%80%D0%B5%D0%BC%D0%B8%D1%83%D0%BC-%D0%B6%D0%B5%D1%82%D0%BE%D0%BD%D1%8B", "http://ayuda.strongholdkingdoms.com/index.php/Vales_Premium", "http://pomoc.strongholdkingdoms.com/index.php/Premium", "http://yardim.strongholdkingdoms.com/index.php/Premium_Token", "http://aiuto.strongholdkingdoms.com/index.php/Gettoni_Premium", "http://ajuda.strongholdkingdoms.com/index.php/Fichas_pr\x00eamio", "", "", "http://help.strongholdkingdoms.com/index.php/Donate_to_Parish", "http://hilfe.strongholdkingdoms.de/index.php/Ressourcen_an_die_Hauptstadt_spenden", 
                "http://aide.strongholdkingdoms.com/index.php/Donation_%C3%A0_la_Pr%C3%A9v%C3%B4t%C3%A9", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9F%D0%BE%D0%B6%D0%B5%D1%80%D1%82%D0%B2%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F_%D0%B2_%D0%BE%D0%BA%D1%80%D1%83%D0%B3", "http://ayuda.strongholdkingdoms.com/index.php/Donar_a_la_parroquia", "http://pomoc.strongholdkingdoms.com/index.php/Datki_na_rzecz_gminy", "http://yardim.strongholdkingdoms.com/index.php/Pari%C5%9F%27e_Ba%C4%9F%C4%B1%C5%9F", "http://aiuto.strongholdkingdoms.com/index.php/Donazioni_al_distretto", "http://ajuda.strongholdkingdoms.com/index.php/Doe_\x00e0_par\x00f3quia", "", "", "http://help.strongholdkingdoms.com/index.php/Villages_Overview", "http://hilfe.strongholdkingdoms.de/index.php/Dorf%C3%BCbersichtsanzeige", "http://aide.strongholdkingdoms.com/index.php/Vue_d%27Ensemble_Village", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%B1%D0%B7%D0%BE%D1%80_%D0%B4%D0%B5%D1%80%D0%B5%D0%B2%D0%BD%D0%B8", "http://ayuda.strongholdkingdoms.com/index.php/Vistazo_general_de_las_aldeas", "http://pomoc.strongholdkingdoms.com/index.php/Przegl%C4%85d_wiosek", "http://yardim.strongholdkingdoms.com/index.php/K%C3%B6ylere_genel_bak%C4%B1%C5%9F", 
                "http://aiuto.strongholdkingdoms.com/index.php/Quadro_dei_villaggi", "http://ajuda.strongholdkingdoms.com/index.php/Vis\x00e3o_geral_das_aldeias", "", "", "http://help.strongholdkingdoms.com/index.php/Achievements", "http://hilfe.strongholdkingdoms.de/index.php/Errungenschaften", "http://aide.strongholdkingdoms.com/index.php/Ach%C3%A8vements", "http://help.ru.strongholdkingdoms.com/index.php/%D0%94%D0%BE%D1%81%D1%82%D0%B8%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F", "http://ayuda.strongholdkingdoms.com/index.php/Logros", "http://pomoc.strongholdkingdoms.com/index.php/Osi%C4%85gni%C4%99cie", "http://yardim.strongholdkingdoms.com/index.php/Ba%C5%9Far%C4%B1lar", "http://aiuto.strongholdkingdoms.com/index.php/Imprese", "http://ajuda.strongholdkingdoms.com/index.php/Conquistas", "", "", "http://help.strongholdkingdoms.com/index.php/The_Second_Age", 
                "http://hilfe.strongholdkingdoms.de/index.php/Die_Zweite_Epoche", "http://aide.strongholdkingdoms.com/index.php/Deuxi%C3%A8me_%C3%88re", "http://help.ru.strongholdkingdoms.com/index.php/%D0%92%D1%82%D0%BE%D1%80%D0%B0%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/La_segunda_edad", "http://pomoc.strongholdkingdoms.com/index.php/Druga_Epoka", "http://yardim.strongholdkingdoms.com/index.php/%C4%B0kinci_%C3%87a%C4%9F", "http://aiuto.strongholdkingdoms.com/index.php/La_Seconda_Epoca", "http://ajuda.strongholdkingdoms.com/index.php/A_Segunda_Era", "", "", "http://help.strongholdkingdoms.com/index.php/The_Third_Age", "http://hilfe.strongholdkingdoms.de/index.php/Die_Dritte_Epoche", "http://aide.strongholdkingdoms.com/index.php/Troisi%C3%A8me_%C3%88re", "http://help.ru.strongholdkingdoms.com/index.php/%D0%A2%D1%80%D0%B5%D1%82%D1%8C%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/La_tercera_edad", "http://pomoc.strongholdkingdoms.com/index.php/Trzecia_Epoka", 
                "http://yardim.strongholdkingdoms.com/index.php/%C3%9C%C3%A7%C3%BCnc%C3%BC_%C3%87a%C4%9F", "http://aiuto.strongholdkingdoms.com/index.php/La_Terza_Epoca", "http://ajuda.strongholdkingdoms.com/index.php/A_Terceira_Era", "", "", "http://help.strongholdkingdoms.com/index.php/Domination_World", "http://hilfe.strongholdkingdoms.de/index.php/Domination_Welt", "http://aide.strongholdkingdoms.com/index.php/Monde_de_Domination", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9C%D0%B8%D1%80_Domination", "http://ayuda.strongholdkingdoms.com/index.php/Mundo_Domination", "http://pomoc.strongholdkingdoms.com/index.php/%C5%9Awiat_Domination", "http://yardim.strongholdkingdoms.com/index.php/Domination_D%C3%BCnyas%C4%B1", "http://aiuto.strongholdkingdoms.com/index.php/Mondo_Domination", "http://ajuda.strongholdkingdoms.com/index.php/Mundo_Domina%C3%A7%C3%A3o", "", "", 
                "http://help.strongholdkingdoms.com/index.php/The_Fourth_Age", "http://hilfe.strongholdkingdoms.de/index.php/Die_Vierte_Epoche", "http://aide.strongholdkingdoms.com/index.php/Quatri%C3%A8me_%C3%88re", "http://help.ru.strongholdkingdoms.com/index.php/Четвёртая_Эпоха", "http://ayuda.strongholdkingdoms.com/index.php/La_cuarta_edad", "http://pomoc.strongholdkingdoms.com/index.php/Czwarta_Epoka", "http://yardim.strongholdkingdoms.com/index.php/D\x00f6rd\x00fcnc\x00fc_\x00c7ağ", "http://aiuto.strongholdkingdoms.com/index.php/La_Quarta_Epoca", "http://ajuda.strongholdkingdoms.com/index.php/A_Quarta_Era", "", "", "http://help.strongholdkingdoms.com/index.php/Treasure_Castle", "http://hilfe.strongholdkingdoms.de/index.php/Schatzburg", "http://aide.strongholdkingdoms.com/index.php/Ch%C3%A2teau_au_Tr%C3%A9sor", "http://help.ru.strongholdkingdoms.com/index.php/%D0%97%D0%B0%D0%BC%D0%BA%D0%B8_%D1%81_%D1%81%D0%BE%D0%BA%D1%80%D0%BE%D0%B2%D0%B8%D1%89%D0%B0%D0%BC%D0%B8", "http://ayuda.strongholdkingdoms.com/index.php/Castillo_del_tesoro", 
                "http://pomoc.strongholdkingdoms.com/index.php/Zamki_ze_skarbami", "http://yardim.strongholdkingdoms.com/index.php/Define_Kaleleri", "http://aiuto.strongholdkingdoms.com/index.php/Castello_del_tesoro", "http://ajuda.strongholdkingdoms.com/index.php/", "", "", "http://help.strongholdkingdoms.com/index.php/Paladin_Castles", "http://hilfe.strongholdkingdoms.de/index.php/Burg_des_Paladins", "http://aide.strongholdkingdoms.com/index.php/Ch%C3%A2teau_du_Paladin", "http://help.ru.strongholdkingdoms.com/index.php/%D0%97%D0%B0%D0%BC%D0%BE%D0%BA_%D0%9F%D0%B0%D0%BB%D0%B0%D0%B4%D0%B8%D0%BD%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/Castillos_de_Palad%C3%ADn", "http://pomoc.strongholdkingdoms.com/index.php/Zamek_Paladyna", "http://yardim.strongholdkingdoms.com/index.php/Paladin_Kaleleri", "http://aiuto.strongholdkingdoms.com/index.php/Castello_del_Paladino", "http://ajuda.strongholdkingdoms.com/index.php/", "", 
                "", "http://help.strongholdkingdoms.com/index.php/The_Fifth_Age", "http://hilfe.strongholdkingdoms.de/index.php/Die_F%C3%BCnfte_Epoche", "http://aide.strongholdkingdoms.com/index.php/Cinqui%C3%A8me_Ere", "http://help.ru.strongholdkingdoms.com/index.php/%D0%9F%D1%8F%D1%82%D0%B0%D1%8F_%D0%AD%D0%BF%D0%BE%D1%85%D0%B0", "http://ayuda.strongholdkingdoms.com/index.php/La_quinta_edad", "http://pomoc.strongholdkingdoms.com/index.php/Piąta_Epoka", "http://yardim.strongholdkingdoms.com/index.php/Beşinci_\x00c7ağ", "http://aiuto.strongholdkingdoms.com/index.php/La_quinta_epoca", "http://ajuda.strongholdkingdoms.com/index.php/", "", ""
             };
            public const int VILLAGE_CASTLE_MAP = 2;
            public const int VILLAGE_HOLD_A_BANQUET = 7;
            public const int VILLAGE_RESOURCES = 3;
            public const int VILLAGE_TRADE = 4;
            public const int VILLAGE_TROOPS = 5;
            public const int VILLAGE_UNITS = 6;
            public const int VILLAGE_VASSALS = 8;
            public const int VILLAGE_VILLAGE_MAP = 1;
            public const int VILLAGES_OVERVIEW = 0x2b;
            public const int WORLD_MAP = 0;

            public void helpClicked()
            {
                openHelpLink(this.m_ID);
            }

            public static CustomSelfDrawPanel.WikiLinkControl init(CustomSelfDrawPanel parent, int screenID, Point position)
            {
                CustomSelfDrawPanel.WikiLinkControl control = new CustomSelfDrawPanel.WikiLinkControl {
                    ImageNorm = (Image) GFXLibrary.int_button_Q_normal,
                    ImageOver = (Image) GFXLibrary.int_button_Q_over,
                    ImageClick = (Image) GFXLibrary.int_button_Q_in,
                    Position = position,
                    m_ID = screenID,
                    CustomTooltipData = screenID,
                    CustomTooltipID = 0x1130
                };
                control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(control.helpClicked));
                parent.addControl(control);
                return control;
            }

            public static CustomSelfDrawPanel.WikiLinkControl init(CustomSelfDrawPanel.CSDControl parent, int screenID, Point position)
            {
                return init(parent, screenID, position, false);
            }

            public static CustomSelfDrawPanel.WikiLinkControl init(CustomSelfDrawPanel.CSDControl parent, int screenID, Point position, bool scaledSmaller)
            {
                CustomSelfDrawPanel.WikiLinkControl control = new CustomSelfDrawPanel.WikiLinkControl {
                    ImageNorm = (Image) GFXLibrary.int_button_Q_normal,
                    ImageOver = (Image) GFXLibrary.int_button_Q_over,
                    ImageClick = (Image) GFXLibrary.int_button_Q_in
                };
                if (scaledSmaller)
                {
                    control.Size = new Size(0x1c, 0x1c);
                }
                control.Position = position;
                control.m_ID = screenID;
                control.CustomTooltipData = screenID;
                control.CustomTooltipID = 0x1130;
                control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(control.helpClicked));
                parent.addControl(control);
                return control;
            }

            public static void openHelpLink(int index)
            {
                int num = 0;
                switch (Program.mySettings.LanguageIdent)
                {
                    case "en":
                        num = 0;
                        break;

                    case "de":
                        num = 1;
                        break;

                    case "fr":
                        num = 2;
                        break;

                    case "ru":
                        num = 3;
                        break;

                    case "es":
                        num = 4;
                        break;

                    case "pl":
                        num = 5;
                        break;

                    case "tr":
                        num = 6;
                        break;

                    case "it":
                        num = 7;
                        break;

                    case "pt":
                        num = 8;
                        break;
                }
                if (urls[(index * 11) + num].Length == 0)
                {
                    num = 0;
                }
                try
                {
                    Process.Start(urls[(index * 11) + num]);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}

