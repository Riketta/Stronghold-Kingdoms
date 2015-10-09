namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MainRightHandPanel : UserControl, IDockWindow
    {
        private IContainer components;
        private DockWindow dockWindow;

        public MainRightHandPanel()
        {
            this.dockWindow = new DockWindow(this);
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
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

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            this.BackgroundImage = Resources.right_side_panel_large_stone_tan;
            base.Name = "MainRightHandPanel";
            base.Size = new Size(200, 0x236);
            base.ResumeLayout(false);
        }

        public void RemoveControl(UserControl control)
        {
            this.dockWindow.RemoveControl(control);
        }
    }
}

