namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MyMessageBoxPopUp : MyFormBase
    {
        public bool closing;
        private MyMessageBoxPanel panel;

        public MyMessageBoxPopUp()
        {
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            base.ClientSize = new Size(400, 0x7d);
            this.panel = new MyMessageBoxPanel();
            this.panel.Size = new Size(base.Size.Width, base.Size.Height - 0x22);
            this.panel.Location = new Point(0, 0x22);
            base.Icon = Resources.shk_icon;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.Controls.Add(this.panel);
            this.Text = "MessageBoxPopUp";
            base.Controls.SetChildIndex(this.panel, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
            base.FormClosing += new FormClosingEventHandler(this.MyMessageBoxPopUps_FormClosing);
        }

        public void init(string message, string title, int type, CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftButton)
        {
            this.Text = base.Title = title;
            this.panel.init(this, message, type, leftButton, (CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
        }

        public void init(string message, string title, int type, CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftButton, bool leaveGreaoutOpenOnClose)
        {
            this.Text = base.Title = title;
            this.panel.init(this, message, type, leftButton, leaveGreaoutOpenOnClose);
            if (leaveGreaoutOpenOnClose)
            {
                this.closing = true;
            }
        }

        private void MyMessageBoxPopUps_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeGreyOut();
            }
        }
    }
}

