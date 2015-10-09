namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SendArmyWindow : Form
    {
        private bool closing;
        private IContainer components;
        private SendArmyPanel sendArmyPanel;

        public SendArmyWindow()
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

        public void init(int parentFromVillage, int fromVillageID, int toVillageID, string villageName, double distance, BattleHonourData honourData, bool gotCaptain, CastleMapAttackerSetupPanel parent)
        {
            this.sendArmyPanel.init(parentFromVillage, fromVillageID, toVillageID, villageName, distance, honourData, gotCaptain, parent);
        }

        private void InitializeComponent()
        {
            this.sendArmyPanel = new SendArmyPanel();
            base.SuspendLayout();
            this.sendArmyPanel.Location = new Point(0, 0);
            this.sendArmyPanel.Name = "sendArmyPanel";
            this.sendArmyPanel.PanelActive = true;
            this.sendArmyPanel.Size = new Size(700, 0x1e2);
            this.sendArmyPanel.StoredGraphics = null;
            this.sendArmyPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(700, 0x1e2);
            base.ControlBox = false;
            base.Controls.Add(this.sendArmyPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(700, 0x1e2);
            this.MinimumSize = new Size(700, 0x1e2);
            base.Name = "SendArmyWindow";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "SendArmyWindow";
            base.FormClosing += new FormClosingEventHandler(this.SendArmyWindow_FormClosing);
            base.ResumeLayout(false);
        }

        private void SendArmyWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeLaunchAttackPopup();
            }
        }

        public void update()
        {
            this.sendArmyPanel.update();
        }

        public void villageLoaded(int villageID)
        {
        }
    }
}

