namespace Kingdoms
{
    using Kingdoms.Properties;
    using StatTracking;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LogoutOptionsWindow2 : Form
    {
        public static bool closing;
        private IContainer components;
        private LogoutPanel currentPanel;

        public LogoutOptionsWindow2()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            base.TransparencyKey = Color.FromArgb(0xff, 0xff, 0, 0xff);
            this.BackColor = base.TransparencyKey;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(bool normalLogout)
        {
            this.currentPanel.init(normalLogout, false);
        }

        public void init(bool normalLogout, bool advertOnly)
        {
            this.currentPanel.init(normalLogout, advertOnly);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(LogoutOptionsWindow2));
            this.currentPanel = new LogoutPanel();
            base.SuspendLayout();
            this.currentPanel.Location = new Point(0, 0);
            this.currentPanel.Name = "logoutPanel";
            this.currentPanel.Size = new Size(0x3e8, 600);
            this.currentPanel.StoredGraphics = null;
            this.currentPanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3e8, 600);
            base.ControlBox = false;
            base.Controls.Add(this.currentPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "LogoutOptionsWindow2";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "LogoutOptionsWindow2";
            base.FormClosing += new FormClosingEventHandler(this.Logout_FormClosing);
            base.ResumeLayout(false);
        }

        private void Logout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !closing)
            {
                closing = true;
                this.currentPanel.vacationModeCloseCheck();
                this.currentPanel.closePopup();
                StatTrackingClient.Instance().ActivateTrigger(0x1a, false);
                InterfaceMgr.Instance.closeLogoutWindow();
            }
        }

        public void update()
        {
            this.currentPanel.update();
        }
    }
}

