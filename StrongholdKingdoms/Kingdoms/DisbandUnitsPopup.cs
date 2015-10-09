namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DisbandUnitsPopup : MyFormBase
    {
        private IContainer components;
        private DisbandTroopsPanel customPanel;

        public DisbandUnitsPopup()
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

        public void init(int troopType)
        {
            this.Text = base.Title = SK.Text("GENERIC_Disband", "Disband");
            this.customPanel.init(this, troopType, false);
        }

        private void InitializeComponent()
        {
            this.customPanel = new DisbandTroopsPanel();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.ClickThru = false;
            this.customPanel.Location = new Point(0, 0x22);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.Size = base.Size;
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0x63;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x124, 0xcf);
            base.Controls.Add(this.customPanel);
            base.Icon = Resources.shk_icon;
            base.Name = "DisbandUnitsPopup";
            base.ShowClose = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Disband";
            base.Controls.SetChildIndex(this.customPanel, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

