namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BuyVillagePopupWindow : Form
    {
        private BuyVillagePopupPanel buyVillagePopupPanel;
        private bool closing;
        private IContainer components;

        public BuyVillagePopupWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void BuyVillagePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeBuyVillagePopupWindow();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int villageID, bool buy)
        {
            this.buyVillagePopupPanel.init(villageID, buy);
        }

        private void InitializeComponent()
        {
            this.buyVillagePopupPanel = new BuyVillagePopupPanel();
            base.SuspendLayout();
            this.buyVillagePopupPanel.ClickThru = false;
            this.buyVillagePopupPanel.Location = new Point(0, 0);
            this.buyVillagePopupPanel.Name = "buyVillagePopupPanel";
            this.buyVillagePopupPanel.PanelActive = true;
            this.buyVillagePopupPanel.Size = new Size(700, 0x1f7);
            this.buyVillagePopupPanel.StoredGraphics = null;
            this.buyVillagePopupPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(700, 0x1f7);
            base.ControlBox = false;
            base.Controls.Add(this.buyVillagePopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            this.MaximumSize = new Size(700, 0x1f7);
            this.MinimumSize = new Size(700, 0x1f7);
            base.Name = "BuyVillagePopupWindow";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "BuyVillagePopupWindow";
            base.FormClosing += new FormClosingEventHandler(this.BuyVillagePopupPanel_FormClosing);
            base.ResumeLayout(false);
        }

        public void update()
        {
            this.buyVillagePopupPanel.update();
        }
    }
}

