namespace Dotnetrix_Samples
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class TabControlEx : TabControl
    {
        private Container components;
        private Color m_Backcolor = Color.Empty;
        private const int TCN_FIRST = -550;
        private const int TCN_SELCHANGING = -552;
        private const int WM_NOTIFY = 0x4e;
        private const int WM_REFLECT = 0x2000;
        private const int WM_USER = 0x400;

        [Description("Occurs as a tab is being changed.")]
        public event SelectedTabPageChangeEventHandler SelectedIndexChanging;

        public TabControlEx()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
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
            base.OnPaint(e);
            e.Graphics.Clear(this.BackColor);
            Rectangle clientRectangle = base.ClientRectangle;
            if (base.TabCount > 0)
            {
                clientRectangle = base.SelectedTab.Bounds;
                StringFormat format = new StringFormat {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                clientRectangle.Inflate(3, 3);
                TabPage page = base.TabPages[base.SelectedIndex];
                SolidBrush brush = new SolidBrush(page.BackColor);
                Pen pen = new Pen(Color.FromArgb(0xcf, 0xda, 0xe0));
                e.Graphics.FillRectangle(brush, clientRectangle);
                ControlPaint.DrawBorder(e.Graphics, clientRectangle, brush.Color, ButtonBorderStyle.Outset);
                for (int i = 0; i <= (base.TabCount - 1); i++)
                {
                    page = base.TabPages[i];
                    clientRectangle = base.GetTabRect(i);
                    ButtonBorderStyle solid = ButtonBorderStyle.Solid;
                    if (i == base.SelectedIndex)
                    {
                        solid = ButtonBorderStyle.Outset;
                    }
                    brush.Color = page.BackColor;
                    e.Graphics.FillRectangle(brush, clientRectangle);
                    brush.Color = Color.FromArgb(130, 0x91, 0x9b);
                    ControlPaint.DrawBorder(e.Graphics, clientRectangle, brush.Color, solid);
                    brush.Color = page.ForeColor;
                    if ((base.Alignment == TabAlignment.Left) || (base.Alignment == TabAlignment.Right))
                    {
                        float angle = 90f;
                        if (base.Alignment == TabAlignment.Left)
                        {
                            angle = 270f;
                        }
                        PointF tf = new PointF((float) (clientRectangle.Left + (clientRectangle.Width >> 1)), (float) (clientRectangle.Top + (clientRectangle.Height >> 1)));
                        e.Graphics.TranslateTransform(tf.X, tf.Y);
                        e.Graphics.RotateTransform(angle);
                        clientRectangle = new Rectangle(-(clientRectangle.Height >> 1), -(clientRectangle.Width >> 1), clientRectangle.Height, clientRectangle.Width);
                    }
                    if (page.Enabled)
                    {
                        e.Graphics.DrawString(page.Text, this.Font, brush, clientRectangle, format);
                    }
                    else
                    {
                        ControlPaint.DrawStringDisabled(e.Graphics, page.Text, this.Font, page.BackColor, clientRectangle, format);
                    }
                    e.Graphics.ResetTransform();
                    if (i != base.SelectedIndex)
                    {
                        e.Graphics.DrawLine(pen, new Point(clientRectangle.Left, clientRectangle.Bottom - 1), new Point(clientRectangle.Right, clientRectangle.Bottom - 1));
                    }
                }
                pen.Dispose();
                brush.Dispose();
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            base.Invalidate();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            base.Invalidate();
        }

        public override void ResetBackColor()
        {
            this.m_Backcolor = Color.Empty;
            base.Invalidate();
        }

        public bool ShouldSerializeBackColor()
        {
            return !this.m_Backcolor.Equals(Color.Empty);
        }

        private TabPage TestTab(Point pt)
        {
            for (int i = 0; i <= (base.TabCount - 1); i++)
            {
                if (base.GetTabRect(i).Contains(pt.X, pt.Y))
                {
                    return base.TabPages[i];
                }
            }
            return null;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x204e)
            {
                NMHDR nmhdr = (NMHDR) Marshal.PtrToStructure(m.LParam, typeof(NMHDR));
                if (nmhdr.code == -552)
                {
                    TabPage nextTab = this.TestTab(base.PointToClient(Cursor.Position));
                    if (nextTab != null)
                    {
                        TabPageChangeEventArgs e = new TabPageChangeEventArgs(base.SelectedTab, nextTab);
                        if (this.SelectedIndexChanging != null)
                        {
                            this.SelectedIndexChanging(this, e);
                        }
                        if (e.Cancel || !nextTab.Enabled)
                        {
                            m.Result = new IntPtr(1);
                            return;
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        [Description("The background color used to display text and graphics in a control."), Browsable(true)]
        public override Color BackColor
        {
            get
            {
                if (!this.m_Backcolor.Equals(Color.Empty))
                {
                    return this.m_Backcolor;
                }
                if (base.Parent == null)
                {
                    return Control.DefaultBackColor;
                }
                return base.Parent.BackColor;
            }
            set
            {
                if (!this.m_Backcolor.Equals(value))
                {
                    this.m_Backcolor = value;
                    base.Invalidate();
                    base.OnBackColorChanged(EventArgs.Empty);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr HWND;
            public uint idFrom;
            public int code;
            public override string ToString()
            {
                return string.Format("Hwnd: {0}, ControlID: {1}, Code: {2}", this.HWND, this.idFrom, this.code);
            }
        }
    }
}

