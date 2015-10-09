namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SelectTradingResourcePopup : Form
    {
        private IContainer components;
        private LogoutPanel m_parent;
        private LogoutOptionsWindow2 m_parentWindow;
        private SelectTradingResourcePanel selectTradingResourcePanel;

        public SelectTradingResourcePopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int currentResource, Point parentLocation, LogoutPanel parent, LogoutOptionsWindow2 parentWindow)
        {
            this.m_parent = parent;
            this.m_parentWindow = parentWindow;
            this.selectTradingResourcePanel.init(currentResource, this, this.m_parent);
            this.updateLocation(parentLocation);
            base.Show(this.m_parentWindow);
        }

        private void InitializeComponent()
        {
            this.selectTradingResourcePanel = new SelectTradingResourcePanel();
            base.SuspendLayout();
            this.selectTradingResourcePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.selectTradingResourcePanel.BackColor = ARGBColors.Yellow;
            this.selectTradingResourcePanel.Location = new Point(0, 0);
            this.selectTradingResourcePanel.Name = "selectTradingResourcePanel";
            this.selectTradingResourcePanel.Size = new Size(0x124, 0x10a);
            this.selectTradingResourcePanel.StoredGraphics = null;
            this.selectTradingResourcePanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.ClientSize = new Size(0x124, 0x10a);
            base.ControlBox = false;
            base.Controls.Add(this.selectTradingResourcePanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SelectTradingResourcePopup";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Donation";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.ResumeLayout(false);
        }

        public void updateLocation(Point location)
        {
            int x = location.X;
            int y = location.Y - 20;
            Screen screen = Screen.FromPoint(location);
            int num3 = x + base.Width;
            if (num3 > (screen.WorkingArea.Width + screen.WorkingArea.X))
            {
                x = (screen.WorkingArea.Width + screen.WorkingArea.X) - base.Width;
            }
            int num4 = y + base.Height;
            if (num4 > (screen.WorkingArea.Height + screen.WorkingArea.Y))
            {
                y = (screen.WorkingArea.Height + screen.WorkingArea.Y) - base.Height;
            }
            base.Location = new Point(x, y);
        }
    }
}

