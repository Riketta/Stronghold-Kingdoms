namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Windows.Forms;

    [ComVisible(true), PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    public class MyFormBase : Form
    {
        private Color bottomColor = Color.FromArgb(0x9f, 180, 0xc1);
        private const int cCaption = 0x19;
        private const int cGrip = 0x10;
        public MFBClose closeCallback;
        private IContainer components;
        public const int HT_CAPTION = 2;
        private bool inSizeChanged;
        private Label label3;
        private int lastWidth = -1;
        private Label lblTitle;
        private Panel panel1;
        private MFBTitlePanel panel2;
        private Panel panel3;
        private Panel panel4;
        private bool resizable;
        private Size resizeOrig;
        private Point RESIZESTART;
        private bool rightResize;
        private Color topColor = Color.FromArgb(0x56, 0x62, 0x6a);
        public const int WM_NCLBUTTONDOWN = 0xa1;

        public MyFormBase()
        {
            this.InitializeComponent();
            try
            {
                this.panel1.BackgroundImage = (Image) GFXLibrary.messageboxclose;
                this.panel3.BackgroundImage = (Image) GFXLibrary.message_box_maximize_normal;
                this.panel4.BackgroundImage = (Image) GFXLibrary.message_box_minimize_normal;
                this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
                this.lblTitle.Font = FontManager.GetFont("Microsoft Sans Serif", 9.75f, FontStyle.Bold);
                Form parentForm = InterfaceMgr.Instance.ParentForm;
                if ((parentForm != null) && (parentForm.WindowState != FormWindowState.Minimized))
                {
                    Point location = parentForm.Location;
                    Size size = parentForm.Size;
                    Size size2 = base.Size;
                    Point point2 = new Point(((size.Width - size2.Width) / 2) + location.X, ((size.Height - size2.Height) / 2) + location.Y);
                    base.Location = point2;
                }
                else
                {
                    base.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            catch (Exception)
            {
                UniversalDebugLog.Log("An exception occurred in myformbase constructor");
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

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.panel3 = new Panel();
            this.panel4 = new Panel();
            this.panel2 = new MFBTitlePanel();
            this.label3 = new Label();
            this.lblTitle = new Label();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.panel1.BackColor = ARGBColors.Black;
            this.panel1.Location = new Point(0x14b, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x12, 0x12);
            this.panel1.TabIndex = 10;
            this.panel1.Visible = false;
            this.panel1.MouseLeave += new EventHandler(this.panel1_MouseLeave);
            this.panel1.MouseClick += new MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseEnter += new EventHandler(this.panel1_MouseEnter);
            this.panel3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.panel3.BackColor = ARGBColors.Black;
            this.panel3.Location = new Point(0x133, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x12, 0x12);
            this.panel3.TabIndex = 11;
            this.panel3.Visible = false;
            this.panel3.MouseLeave += new EventHandler(this.panel3_MouseLeave);
            this.panel3.Click += new EventHandler(this.panel3_Click);
            this.panel3.MouseEnter += new EventHandler(this.panel3_MouseEnter);
            this.panel4.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.panel4.BackColor = ARGBColors.Black;
            this.panel4.Location = new Point(0x11b, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x12, 0x12);
            this.panel4.TabIndex = 11;
            this.panel4.Visible = false;
            this.panel4.MouseLeave += new EventHandler(this.panel4_MouseLeave);
            this.panel4.Click += new EventHandler(this.panel4_Click);
            this.panel4.MouseEnter += new EventHandler(this.panel4_MouseEnter);
            this.panel2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.panel2.ClickThru = false;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Location = new Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.PanelActive = true;
            this.panel2.Size = new Size(0x167, 30);
            this.panel2.StoredGraphics = null;
            this.panel2.TabIndex = 12;
            this.panel2.MouseDown += new MouseEventHandler(this.panel2_MouseDown);
            this.panel2.SizeChanged += new EventHandler(this.panel2_SizeChanged);
            this.label3.AutoSize = true;
            this.label3.BackColor = ARGBColors.Transparent;
            this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = ARGBColors.White;
            this.label3.Location = new Point(0xb3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 0x10);
            this.label3.TabIndex = 9;
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = ARGBColors.Transparent;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = ARGBColors.White;
            this.lblTitle.Location = new Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0x21, 0x10);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "title";
            this.lblTitle.MouseDown += new MouseEventHandler(this.lblTitle_MouseDown);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.ClientSize = new Size(0x169, 0x7b);
            base.ControlBox = false;
            base.Controls.Add(this.panel4);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.FormBorderStyle = FormBorderStyle.None;
            base.ShowInTaskbar = false;
            base.Icon = Resources.shk_icon;
            base.Name = "MyFormBase";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Rename Village";
            base.Paint += new PaintEventHandler(this.MyFormBase_Paint);
            base.SizeChanged += new EventHandler(this.MyFormBase_SizeChanged);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        private void MyFormBase_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(0x56, 0x62, 0x6a), 1f);
            Rectangle rect = new Rectangle(1, 1, (base.Width - 1) - 2, (base.Height - 1) - 2);
            LinearGradientBrush brush = new LinearGradientBrush(rect, this.topColor, this.bottomColor, LinearGradientMode.Vertical);
            graphics.FillRectangle(brush, rect);
            graphics.DrawRectangle(pen, rect);
            if (this.resizable)
            {
                Rectangle bounds = new Rectangle(base.ClientSize.Width - 0x10, base.ClientSize.Height - 0x10, 0x10, 0x10);
                ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, bounds);
            }
            brush.Dispose();
            pen.Dispose();
        }

        private void MyFormBase_SizeChanged(object sender, EventArgs e)
        {
            if (this.resizable)
            {
                base.Invalidate();
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MyFormBase_close");
            if (this.closeCallback != null)
            {
                this.closeCallback();
            }
            base.Close();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.panel1.BackgroundImage = (Image) GFXLibrary.messageboxclose_over;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            this.panel1.BackgroundImage = (Image) GFXLibrary.messageboxclose;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            if (!this.inSizeChanged && !this.rightResize)
            {
                this.inSizeChanged = true;
                this.panel2.Width = base.Width - 2;
                this.panel2.init(this.panel2.Width);
                if (base.WindowState != FormWindowState.Minimized)
                {
                    this.panel1.Location = new Point(this.panel2.Width - 0x1c, this.panel1.Location.Y);
                    this.panel3.Location = new Point((this.panel2.Width - 0x1c) - 0x18, this.panel3.Location.Y);
                    this.panel4.Location = new Point((this.panel2.Width - 0x1c) - 0x30, this.panel4.Location.Y);
                }
                this.inSizeChanged = false;
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MyFormBase_maximized");
            if (base.WindowState != FormWindowState.Maximized)
            {
                base.WindowState = FormWindowState.Maximized;
            }
            else
            {
                base.WindowState = FormWindowState.Normal;
            }
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            this.panel3.BackgroundImage = (Image) GFXLibrary.message_box_maximize_over;
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            this.panel3.BackgroundImage = (Image) GFXLibrary.message_box_maximize_normal;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MyFormBase_minimized");
            base.WindowState = FormWindowState.Minimized;
        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            this.panel4.BackgroundImage = (Image) GFXLibrary.message_box_minimize_over;
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            this.panel4.BackgroundImage = (Image) GFXLibrary.message_box_minimize_normal;
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public void setGradient(Color top, Color bottom)
        {
            this.topColor = top;
            this.bottomColor = bottom;
        }

        public void showMinMax()
        {
            this.panel3.Visible = true;
            this.panel4.Visible = true;
        }

        private void ucRightResize_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.rightResize)
            {
                this.rightResize = true;
                this.resizeOrig = base.Size;
                this.lastWidth = -1;
                this.RESIZESTART = base.PointToScreen(new Point(e.X, e.Y));
            }
        }

        private void ucRightResize_MouseEnter(object sender, EventArgs e)
        {
            if (!this.rightResize)
            {
                this.Cursor = Cursors.SizeWE;
            }
        }

        private void ucRightResize_MouseLeave(object sender, EventArgs e)
        {
            if (!this.rightResize)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ucRightResize_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = base.PointToScreen(new Point(e.X, e.Y));
            if (this.rightResize)
            {
                int width = this.resizeOrig.Width + (point.X - this.RESIZESTART.X);
                if (width != this.lastWidth)
                {
                    base.Size = new Size(width, base.Height);
                    this.lastWidth = width;
                    base.Invalidate();
                    this.Title = width.ToString();
                }
            }
            if (this.rightResize)
            {
                this.Cursor = Cursors.SizeWE;
            }
        }

        private void ucRightResize_MouseUp(object sender, MouseEventArgs e)
        {
            this.rightResize = false;
            this.Cursor = Cursors.Default;
        }

        protected override void WndProc(ref Message m)
        {
            if ((m.Msg == 0x84) && this.resizable)
            {
                Point p = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 0x10);
                p = base.PointToClient(p);
                if ((p.X >= (base.ClientSize.Width - 0x10)) && (p.Y >= (base.ClientSize.Height - 0x10)))
                {
                    m.Result = (IntPtr) 0x11;
                    return;
                }
                if (p.X >= (base.ClientSize.Width - 4))
                {
                    m.Result = (IntPtr) 11;
                    return;
                }
                if (p.Y >= (base.ClientSize.Height - 4))
                {
                    m.Result = (IntPtr) 15;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        public bool Resizable
        {
            get
            {
                return this.resizable;
            }
            set
            {
                this.resizable = value;
            }
        }

        public bool ShowBar
        {
            get
            {
                return this.panel2.Visible;
            }
            set
            {
                this.panel2.Visible = value;
            }
        }

        public bool ShowClose
        {
            get
            {
                return this.panel1.Visible;
            }
            set
            {
                this.panel1.Visible = value;
            }
        }

        public string Title
        {
            set
            {
                this.lblTitle.Text = value;
            }
        }

        public delegate void MFBClose();
    }
}

