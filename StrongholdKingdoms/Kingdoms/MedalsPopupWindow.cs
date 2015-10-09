namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MedalsPopupWindow : Form
    {
        private IContainer components;
        private MedalsPopupPanel medalsPopupPanel;

        public MedalsPopupWindow()
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

        public void init(List<int> achievements, Form parent)
        {
            if (parent != null)
            {
                base.Location = new Point(parent.Location.X + ((parent.Width - base.Width) / 2), parent.Location.Y + ((parent.Height - base.Height) / 2));
            }
            this.medalsPopupPanel.init(achievements, this);
        }

        private void InitializeComponent()
        {
            this.medalsPopupPanel = new MedalsPopupPanel();
            base.SuspendLayout();
            this.medalsPopupPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.medalsPopupPanel.ClickThru = false;
            this.medalsPopupPanel.Location = new Point(0, 0);
            this.medalsPopupPanel.Name = "medalsPopupPanel";
            this.medalsPopupPanel.PanelActive = true;
            this.medalsPopupPanel.Size = new Size(0x1e9, 350);
            this.medalsPopupPanel.StoredGraphics = null;
            this.medalsPopupPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x1e9, 350);
            base.Controls.Add(this.medalsPopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            this.MaximumSize = new Size(700, 0x1bb);
            this.MinimumSize = new Size(0x1db, 350);
            base.Name = "MedalsPopupWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.ResumeLayout(false);
        }
    }
}

