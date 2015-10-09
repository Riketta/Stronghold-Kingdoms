namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AdvancedCastleOptionsPopup : Form
    {
        private AdvancedCastleOptionsPanel advancedCastleOptionsPanel;
        private IContainer components;

        public AdvancedCastleOptionsPopup()
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

        public void init(bool castleSetup)
        {
            this.advancedCastleOptionsPanel.init(this, castleSetup);
        }

        private void InitializeComponent()
        {
            this.advancedCastleOptionsPanel = new AdvancedCastleOptionsPanel();
            base.SuspendLayout();
            this.advancedCastleOptionsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.advancedCastleOptionsPanel.BackColor = ARGBColors.Fuchsia;
            this.advancedCastleOptionsPanel.Location = new Point(0, 0);
            this.advancedCastleOptionsPanel.Name = "advancedCastleOptionsPanel";
            this.advancedCastleOptionsPanel.Size = new Size(0x124, 0x10a);
            this.advancedCastleOptionsPanel.StoredGraphics = null;
            this.advancedCastleOptionsPanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.ClientSize = new Size(0x124, 0x10a);
            base.ControlBox = false;
            base.Controls.Add(this.advancedCastleOptionsPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "AdvancedCastleOptionsPopup";
            base.Opacity = 0.95;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Report Capture";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.ResumeLayout(false);
        }

        public void update()
        {
            this.advancedCastleOptionsPanel.update();
        }
    }
}

