namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SendMonkWindow : Form
    {
        private bool closing;
        private IContainer components;
        private SendMonkPanel sendMonkPanel;

        public SendMonkWindow()
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

        public void init(int villageID)
        {
            this.sendMonkPanel.init(villageID);
        }

        private void InitializeComponent()
        {
            this.sendMonkPanel = new SendMonkPanel();
            base.SuspendLayout();
            this.sendMonkPanel.Location = new Point(0, 0);
            this.sendMonkPanel.Name = "sendMonkPanel";
            this.sendMonkPanel.PanelActive = true;
            this.sendMonkPanel.Size = new Size(700, 0x1e2);
            this.sendMonkPanel.StoredGraphics = null;
            this.sendMonkPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(700, 0x1e2);
            base.ControlBox = false;
            base.Controls.Add(this.sendMonkPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(700, 0x1e2);
            this.MinimumSize = new Size(700, 0x1e2);
            base.Name = "SendMonkWindow";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "SendMonkWindow";
            base.FormClosing += new FormClosingEventHandler(this.SendMonkPanel_FormClosing);
            base.ResumeLayout(false);
        }

        private void SendMonkPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeSendMonkWindow();
            }
        }

        public void update()
        {
            this.sendMonkPanel.update();
        }

        public void villageLoaded(int villageID)
        {
            this.sendMonkPanel.onVillageLoadUpdate(villageID, true);
        }
    }
}

