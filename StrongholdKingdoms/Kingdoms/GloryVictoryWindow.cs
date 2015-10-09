namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GloryVictoryWindow : Form
    {
        private IContainer components;
        private GloryVictoryPanel gloryVictoryPanel;

        public GloryVictoryWindow()
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

        public void init(Form parent)
        {
            if (parent != null)
            {
                base.Location = new Point(parent.Location.X + ((parent.Width - base.Width) / 2), parent.Location.Y + ((parent.Height - base.Height) / 2));
            }
            this.gloryVictoryPanel.init(this);
        }

        private void InitializeComponent()
        {
            this.gloryVictoryPanel = new GloryVictoryPanel();
            base.SuspendLayout();
            this.gloryVictoryPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.gloryVictoryPanel.ClickThru = false;
            this.gloryVictoryPanel.Location = new Point(0, 0);
            this.gloryVictoryPanel.Name = "newQuestsCompletedPanel";
            this.gloryVictoryPanel.PanelActive = true;
            this.gloryVictoryPanel.Size = new Size(0x1e9, 350);
            this.gloryVictoryPanel.StoredGraphics = null;
            this.gloryVictoryPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x1e9, 350);
            base.Controls.Add(this.gloryVictoryPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            this.MaximumSize = new Size(700, 0x1bb);
            this.MinimumSize = new Size(0x1db, 350);
            base.Name = "GloryVictoryWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.ResumeLayout(false);
        }
    }
}

