namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WheelPopup : Form
    {
        private IContainer components;
        private WheelPanel wheelPanel;

        public WheelPopup()
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

        public void init(int wheelType)
        {
            this.wheelPanel.init(true, wheelType);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(WheelPopup));
            this.wheelPanel = new WheelPanel();
            base.SuspendLayout();
            this.wheelPanel.Location = new Point(0, 0);
            this.wheelPanel.Name = "wheelPanel";
            this.wheelPanel.PanelActive = true;
            this.wheelPanel.Size = new Size(0x3e8, 600);
            this.wheelPanel.StoredGraphics = null;
            this.wheelPanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3e8, 600);
            base.ControlBox = false;
            base.Controls.Add(this.wheelPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x3e8, 600);
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x3e8, 600);
            base.Name = "WheelPopup";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Free Cards";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.ResumeLayout(false);
        }

        public void update()
        {
            this.wheelPanel.update();
        }
    }
}

