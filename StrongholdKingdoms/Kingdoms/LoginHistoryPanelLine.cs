namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LoginHistoryPanelLine : UserControl
    {
        private IContainer components;
        private Label lblDuration;
        private Label lblIP;
        private Label lblLoginTime;

        public LoginHistoryPanelLine()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(string ipAddr, DateTime lastTime, TimeSpan duration)
        {
            this.lblIP.Text = ipAddr;
            this.lblLoginTime.Text = lastTime.ToShortTimeString() + "  -  " + lastTime.ToLongDateString();
            if (duration != TimeSpan.MinValue)
            {
                this.lblDuration.Text = VillageMap.createBuildTimeString((int) duration.TotalSeconds);
            }
            else
            {
                this.lblDuration.Text = "";
            }
        }

        private void InitializeComponent()
        {
            this.lblIP = new Label();
            this.lblLoginTime = new Label();
            this.lblDuration = new Label();
            base.SuspendLayout();
            this.lblIP.Location = new Point(3, 0);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new Size(0x68, 15);
            this.lblIP.TabIndex = 0;
            this.lblIP.Text = "255.255.255.255";
            this.lblLoginTime.Location = new Point(0x71, 0);
            this.lblLoginTime.Name = "lblLoginTime";
            this.lblLoginTime.Size = new Size(0xc5, 15);
            this.lblLoginTime.TabIndex = 1;
            this.lblLoginTime.Text = "label1";
            this.lblDuration.Location = new Point(0x13c, 0);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new Size(0x5b, 15);
            this.lblDuration.TabIndex = 2;
            this.lblDuration.Text = "label1";
            this.lblDuration.TextAlign = ContentAlignment.TopRight;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.Controls.Add(this.lblDuration);
            base.Controls.Add(this.lblLoginTime);
            base.Controls.Add(this.lblIP);
            base.Name = "LoginHistoryPanelLine";
            base.Size = new Size(410, 0x12);
            base.ResumeLayout(false);
        }
    }
}

