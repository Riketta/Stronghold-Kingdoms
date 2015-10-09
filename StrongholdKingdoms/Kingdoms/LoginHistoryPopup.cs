namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LoginHistoryPopup : Form
    {
        private Button btnClose;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label label3;
        private List<LoginHistoryInfo> loginHistory;
        private Panel pnlList;

        public LoginHistoryPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.loginHistory = GameEngine.Instance.World.getLoginHistory(true);
            this.updateList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnClose = new Button();
            this.pnlList = new Panel();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            base.SuspendLayout();
            this.btnClose.Location = new Point(0x170, 0x160);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.pnlList.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pnlList.AutoScroll = true;
            this.pnlList.BackColor = ARGBColors.White;
            this.pnlList.Location = new Point(12, 0x25);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new Size(0x1af, 300);
            this.pnlList.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3a, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x79, 0x15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Login Time";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x16d, 0x15);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2f, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Duration";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x1c7, 0x183);
            base.ControlBox = false;
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.pnlList);
            base.Controls.Add(this.btnClose);
            base.Icon = Resources.shk_icon;
            base.Name = "LoginHistoryPopup";
            this.Text = "Login History";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void update()
        {
            if (this.loginHistory == null)
            {
                this.loginHistory = GameEngine.Instance.World.getLoginHistory(false);
                this.updateList();
            }
        }

        public void updateList()
        {
            if (this.loginHistory != null)
            {
                this.pnlList.Controls.Clear();
                int y = 0;
                foreach (LoginHistoryInfo info in this.loginHistory)
                {
                    LoginHistoryPanelLine line = new LoginHistoryPanelLine {
                        Location = new Point(0, y)
                    };
                    line.init(info.ipAddress, info.LastLogin, info.duration);
                    this.pnlList.Controls.Add(line);
                    y += line.Height;
                }
                this.pnlList.ResumeLayout(false);
                this.pnlList.PerformLayout();
            }
        }
    }
}

