namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ScoutPopupWindow : Form
    {
        private bool closing;
        private IContainer components;
        private ScoutPopupPanel scoutPopupPanel;

        public ScoutPopupWindow()
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

        public void init(int villageID, bool reset)
        {
            this.scoutPopupPanel.init(villageID, reset);
        }

        private void InitializeComponent()
        {
            this.scoutPopupPanel = new ScoutPopupPanel();
            base.SuspendLayout();
            this.scoutPopupPanel.Location = new Point(0, 0);
            this.scoutPopupPanel.Name = "scoutPopupPanel";
            this.scoutPopupPanel.PanelActive = true;
            this.scoutPopupPanel.Size = new Size(700, 0x1e2);
            this.scoutPopupPanel.StoredGraphics = null;
            this.scoutPopupPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(700, 0x1e2);
            base.ControlBox = false;
            base.Controls.Add(this.scoutPopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(700, 0x1e2);
            this.MinimumSize = new Size(700, 0x1e2);
            base.Name = "ScoutPopupWindow";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "ScoutPopupWindow";
            base.FormClosing += new FormClosingEventHandler(this.ScoutPopupPanel_FormClosing);
            base.ResumeLayout(false);
        }

        private void ScoutPopupPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeScoutPopupWindow();
            }
        }

        public void update()
        {
            this.scoutPopupPanel.update();
        }

        public void villageLoaded(int villageID)
        {
            this.scoutPopupPanel.onVillageLoadUpdate(villageID, true);
        }
    }
}

