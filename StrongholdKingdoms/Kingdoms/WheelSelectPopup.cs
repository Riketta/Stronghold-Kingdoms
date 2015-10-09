namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WheelSelectPopup : Form
    {
        private IContainer components;
        private WheelSelectPanel wheelSelectPanel;

        public WheelSelectPopup()
        {
            this.InitializeComponent();
            base.TransparencyKey = Color.FromArgb(0xff, 0xff, 0, 0xff);
            this.BackColor = base.TransparencyKey;
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

        public void init()
        {
            this.wheelSelectPanel.init(true);
        }

        private void InitializeComponent()
        {
            this.wheelSelectPanel = new WheelSelectPanel();
            base.SuspendLayout();
            this.wheelSelectPanel.ClickThru = false;
            this.wheelSelectPanel.Location = new Point(0, 0);
            this.wheelSelectPanel.Name = "wheelSelectPanel";
            this.wheelSelectPanel.NoDrawBackground = false;
            this.wheelSelectPanel.PanelActive = true;
            this.wheelSelectPanel.SelfDrawBackground = false;
            this.wheelSelectPanel.Size = new Size(0x3e8, 250);
            this.wheelSelectPanel.StoredGraphics = null;
            this.wheelSelectPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3e8, 250);
            base.ControlBox = false;
            base.Controls.Add(this.wheelSelectPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x3e8, 250);
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x3e8, 250);
            base.Name = "WheelSelectPopup";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Free Cards";
            base.TransparencyKey = Color.FromArgb(0xff, 0, 0xff);
            base.ResumeLayout(false);
        }
    }
}

