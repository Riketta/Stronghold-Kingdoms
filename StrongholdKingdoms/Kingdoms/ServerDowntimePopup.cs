namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ServerDowntimePopup : MyFormBase
    {
        private BitmapButton btnClose;
        private IContainer components;
        private Label lblExplanation;
        private Label lblHeader;
        private Label lblMinutes;

        public ServerDowntimePopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.Text = base.Title = SK.Text("ServerDowntime_Scheduled_DownTime", "Scheduled Server Maintenance");
            this.lblHeader.Text = SK.Text("ServerDowntime_Planned", "There is a Planned Downtime in");
            this.lblMinutes.Text = "0 " + SK.Text("ServerDowntime_Minutes", "Minutes");
            this.lblExplanation.Text = SK.Text("ServerDowntime_Explanation", "Please ensure you have logged out safely in advance of this downtime.");
            this.lblHeader.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.lblMinutes.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.lblExplanation.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("ServerDowntimePopup_close");
            GameEngine.Instance.clearDowntimePopup();
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
            this.btnClose = new BitmapButton();
            this.lblHeader = new Label();
            this.lblMinutes = new Label();
            this.lblExplanation = new Label();
            base.SuspendLayout();
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnClose.Location = new Point(0x112, 0xbc);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.lblHeader.BackColor = ARGBColors.Transparent;
            this.lblHeader.Location = new Point(0x21, 50);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new Size(0x127, 0x1d);
            this.lblHeader.TabIndex = 14;
            this.lblHeader.Text = "There is a Planned Downtime in";
            this.lblHeader.TextAlign = ContentAlignment.TopCenter;
            this.lblMinutes.BackColor = ARGBColors.Transparent;
            this.lblMinutes.Location = new Point(0x21, 0x55);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new Size(0x127, 0x1d);
            this.lblMinutes.TabIndex = 15;
            this.lblMinutes.Text = "X Minutes";
            this.lblMinutes.TextAlign = ContentAlignment.TopCenter;
            this.lblExplanation.BackColor = ARGBColors.Transparent;
            this.lblExplanation.Location = new Point(0x21, 120);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new Size(0x127, 0x35);
            this.lblExplanation.TabIndex = 0x10;
            this.lblExplanation.Text = "Please ensure you have logged out safely in advance of this downtime.";
            this.lblExplanation.TextAlign = ContentAlignment.TopCenter;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x169, 0xdf);
            base.Controls.Add(this.lblExplanation);
            base.Controls.Add(this.lblMinutes);
            base.Controls.Add(this.lblHeader);
            base.Controls.Add(this.btnClose);
            base.Name = "ServerDowntimePopup";
            base.ShowClose = true;
            this.Text = "ServerDowntimePopup";
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.lblHeader, 0);
            base.Controls.SetChildIndex(this.lblMinutes, 0);
            base.Controls.SetChildIndex(this.lblExplanation, 0);
            base.ResumeLayout(false);
        }

        public void show(int minutes)
        {
            if (InterfaceMgr.Instance.ParentForm != null)
            {
                InterfaceMgr.Instance.ParentForm.TopMost = true;
                InterfaceMgr.Instance.ParentForm.Focus();
                InterfaceMgr.Instance.ParentForm.BringToFront();
                InterfaceMgr.Instance.ParentForm.Focus();
                InterfaceMgr.Instance.ParentForm.TopMost = false;
            }
            if (minutes <= 120)
            {
                this.lblMinutes.Text = minutes.ToString() + " " + SK.Text("ServerDowntime_Minutes", "Minutes");
            }
            else
            {
                this.lblMinutes.Text = ((minutes / 60)).ToString() + " " + SK.Text("VillageMapPanel_Hours", "Hours");
            }
            bool flag = false;
            Form activeForm = Form.ActiveForm;
            if (((activeForm != null) && (activeForm.ProductName == base.ProductName)) && (activeForm.WindowState == FormWindowState.Normal))
            {
                flag = true;
            }
            if (flag)
            {
                base.Show(activeForm);
            }
            else
            {
                base.Show();
            }
            base.TopMost = false;
            base.TopMost = true;
            base.Focus();
            base.BringToFront();
            base.Focus();
            base.TopMost = false;
        }
    }
}

