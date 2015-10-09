namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class AdminStatsPopup : MyFormBase
    {
        private BitmapButton btnClose;
        private IContainer components;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label lblLast24;
        private Label lblLast3;
        private Label lblLast7;
        private Label lblNumActiveUsers;
        private Label lblNumUsersLoggedIn;

        public AdminStatsPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
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

        public void init(GetAdminStats_ReturnType returnData)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            this.Text = base.Title = "Admin Info";
            this.lblNumUsersLoggedIn.Text = returnData.usersLoggedIn.ToString("N", nFI);
            this.lblLast24.Text = returnData.usersLogged24Hours.ToString("N", nFI);
            this.lblLast3.Text = returnData.usersLogged3Days.ToString("N", nFI);
            this.lblLast7.Text = returnData.usersLogged7Days.ToString("N", nFI);
            this.lblNumActiveUsers.Text = returnData.usersActiveLastHour.ToString("N", nFI);
        }

        private void InitializeComponent()
        {
            this.btnClose = new BitmapButton();
            this.label1 = new Label();
            this.lblNumUsersLoggedIn = new Label();
            this.lblLast7 = new Label();
            this.label3 = new Label();
            this.lblLast3 = new Label();
            this.label5 = new Label();
            this.lblLast24 = new Label();
            this.label7 = new Label();
            this.lblNumActiveUsers = new Label();
            this.label4 = new Label();
            base.SuspendLayout();
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.BorderColor = ARGBColors.DarkBlue;
            this.btnClose.BorderDrawing = true;
            this.btnClose.FocusRectangleEnabled = false;
            this.btnClose.Image = null;
            this.btnClose.ImageBorderColor = ARGBColors.Chocolate;
            this.btnClose.ImageBorderEnabled = true;
            this.btnClose.ImageDropShadow = true;
            this.btnClose.ImageFocused = null;
            this.btnClose.ImageInactive = null;
            this.btnClose.ImageMouseOver = null;
            this.btnClose.ImageNormal = null;
            this.btnClose.ImagePressed = null;
            this.btnClose.InnerBorderColor = ARGBColors.LightGray;
            this.btnClose.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnClose.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnClose.Location = new Point(0xe9, 0xbb);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.label1.AutoSize = true;
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.Location = new Point(0x15, 0x2a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x6d, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Spare Feedin Villages";
            this.lblNumUsersLoggedIn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblNumUsersLoggedIn.BackColor = ARGBColors.Transparent;
            this.lblNumUsersLoggedIn.Location = new Point(0xf9, 0x2a);
            this.lblNumUsersLoggedIn.Name = "lblNumUsersLoggedIn";
            this.lblNumUsersLoggedIn.Size = new Size(0x2b, 13);
            this.lblNumUsersLoggedIn.TabIndex = 2;
            this.lblNumUsersLoggedIn.Text = "0";
            this.lblNumUsersLoggedIn.TextAlign = ContentAlignment.TopRight;
            this.lblLast7.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblLast7.BackColor = ARGBColors.Transparent;
            this.lblLast7.Location = new Point(0xf9, 0xa2);
            this.lblLast7.Name = "lblLast7";
            this.lblLast7.Size = new Size(0x2b, 13);
            this.lblLast7.TabIndex = 4;
            this.lblLast7.Text = "0";
            this.lblLast7.TextAlign = ContentAlignment.TopRight;
            this.label3.AutoSize = true;
            this.label3.BackColor = ARGBColors.Transparent;
            this.label3.Location = new Point(0x15, 0xa2);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xc4, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Number of Users Logged In Last 7 Days";
            this.lblLast3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblLast3.BackColor = ARGBColors.Transparent;
            this.lblLast3.Location = new Point(0xf9, 0x84);
            this.lblLast3.Name = "lblLast3";
            this.lblLast3.Size = new Size(0x2b, 13);
            this.lblLast3.TabIndex = 6;
            this.lblLast3.Text = "0";
            this.lblLast3.TextAlign = ContentAlignment.TopRight;
            this.label5.AutoSize = true;
            this.label5.BackColor = ARGBColors.Transparent;
            this.label5.Location = new Point(0x15, 0x84);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0xc4, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Number of Users Logged In Last 3 Days";
            this.lblLast24.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblLast24.BackColor = ARGBColors.Transparent;
            this.lblLast24.Location = new Point(0xf9, 0x66);
            this.lblLast24.Name = "lblLast24";
            this.lblLast24.Size = new Size(0x2b, 13);
            this.lblLast24.TabIndex = 8;
            this.lblLast24.Text = "0";
            this.lblLast24.TextAlign = ContentAlignment.TopRight;
            this.label7.AutoSize = true;
            this.label7.BackColor = ARGBColors.Transparent;
            this.label7.Location = new Point(0x15, 0x66);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0xce, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Number of Users Logged In Last 24 Hours";
            this.lblNumActiveUsers.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblNumActiveUsers.BackColor = ARGBColors.Transparent;
            this.lblNumActiveUsers.Location = new Point(0xf9, 0x48);
            this.lblNumActiveUsers.Name = "lblNumActiveUsers";
            this.lblNumActiveUsers.Size = new Size(0x2b, 13);
            this.lblNumActiveUsers.TabIndex = 10;
            this.lblNumActiveUsers.Text = "0";
            this.lblNumActiveUsers.TextAlign = ContentAlignment.TopRight;
            this.label4.AutoSize = true;
            this.label4.BackColor = ARGBColors.Transparent;
            this.label4.Location = new Point(0x15, 0x48);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xa3, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Number of Users Currently Active";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(320, 220);
            base.Controls.Add(this.lblNumActiveUsers);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.lblLast24);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.lblLast3);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.lblLast7);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lblNumUsersLoggedIn);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnClose);
            base.Icon = Resources.shk_icon;
            base.Name = "AdminStatsPopup";
            base.ShowClose = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Admin Info";
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.lblNumUsersLoggedIn, 0);
            base.Controls.SetChildIndex(this.label3, 0);
            base.Controls.SetChildIndex(this.lblLast7, 0);
            base.Controls.SetChildIndex(this.label5, 0);
            base.Controls.SetChildIndex(this.lblLast3, 0);
            base.Controls.SetChildIndex(this.label7, 0);
            base.Controls.SetChildIndex(this.lblLast24, 0);
            base.Controls.SetChildIndex(this.label4, 0);
            base.Controls.SetChildIndex(this.lblNumActiveUsers, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

