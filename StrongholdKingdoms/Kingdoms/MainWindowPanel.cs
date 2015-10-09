namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MainWindowPanel : UserControl, IDockWindow
    {
        private IContainer components;
        private DockWindow dockWindow;
        private bool m_allowDraw = true;

        public MainWindowPanel()
        {
            this.dockWindow = new DockWindow(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        public void AddControl(UserControl control, int x, int y)
        {
            this.dockWindow.AddControl(control, x, y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void doDraw(bool allowDraw)
        {
            this.m_allowDraw = allowDraw;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.Name = "MainWindowPanel";
            base.Size = new Size(0x1b7, 0x197);
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (this.m_allowDraw)
            {
                base.OnPaintBackground(e);
            }
        }

        public void RemoveControl(UserControl control)
        {
            this.dockWindow.RemoveControl(control);
        }
    }
}

