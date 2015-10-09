namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AdminInfoPopup : MyFormBase
    {
        private static string adminMessage = "";
        private BitmapButton btnExit;
        private BitmapButton btnSend;
        private IContainer components;
        private static char[] delims = new char[] { '\n', '\r', ' ' };
        private static AdminInfoPopup lastPopup = null;
        private TextBox textBox1;

        public AdminInfoPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("AdminInfoPopup_close");
            base.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            RemoteServices.Instance.SetAdminMessage(lastPopup.textBox1.Text, 0);
            adminMessage = lastPopup.textBox1.Text;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init()
        {
            this.btnExit.Text = SK.Text("Admin_Exit", "Exit");
            this.Text = base.Title = SK.Text("Admin_Message", "Admin's Message");
        }

        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.btnExit = new BitmapButton();
            this.btnSend = new BitmapButton();
            base.SuspendLayout();
            this.textBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.textBox1.BackColor = ARGBColors.White;
            this.textBox1.Location = new Point(12, 0x2e);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = ScrollBars.Vertical;
            this.textBox1.Size = new Size(0x1fd, 0x16e);
            this.textBox1.TabIndex = 0;
            this.btnExit.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnExit.BorderColor = ARGBColors.DarkBlue;
            this.btnExit.BorderDrawing = true;
            this.btnExit.FocusRectangleEnabled = false;
            this.btnExit.Image = null;
            this.btnExit.ImageBorderColor = ARGBColors.Chocolate;
            this.btnExit.ImageBorderEnabled = true;
            this.btnExit.ImageDropShadow = true;
            this.btnExit.ImageFocused = null;
            this.btnExit.ImageInactive = null;
            this.btnExit.ImageMouseOver = null;
            this.btnExit.ImageNormal = null;
            this.btnExit.ImagePressed = null;
            this.btnExit.InnerBorderColor = ARGBColors.LightGray;
            this.btnExit.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnExit.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnExit.Location = new Point(0x1a9, 0x1a6);
            this.btnExit.Name = "btnExit";
            this.btnExit.OffsetPressedContent = true;
            this.btnExit.Padding2 = 5;
            this.btnExit.Size = new Size(0x60, 0x17);
            this.btnExit.StretchImage = false;
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.TextDropShadow = false;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnSend.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnSend.BorderColor = ARGBColors.DarkBlue;
            this.btnSend.BorderDrawing = true;
            this.btnSend.FocusRectangleEnabled = false;
            this.btnSend.Image = null;
            this.btnSend.ImageBorderColor = ARGBColors.Chocolate;
            this.btnSend.ImageBorderEnabled = true;
            this.btnSend.ImageDropShadow = true;
            this.btnSend.ImageFocused = null;
            this.btnSend.ImageInactive = null;
            this.btnSend.ImageMouseOver = null;
            this.btnSend.ImageNormal = null;
            this.btnSend.ImagePressed = null;
            this.btnSend.InnerBorderColor = ARGBColors.LightGray;
            this.btnSend.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnSend.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnSend.Location = new Point(12, 0x1a6);
            this.btnSend.Name = "btnSend";
            this.btnSend.OffsetPressedContent = true;
            this.btnSend.Padding2 = 5;
            this.btnSend.Size = new Size(0x60, 0x17);
            this.btnSend.StretchImage = false;
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.TextDropShadow = false;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x215, 0x1c9);
            base.Controls.Add(this.btnSend);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.textBox1);
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AdminInfoPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Admin's Message";
            base.TopMost = true;
            base.Controls.SetChildIndex(this.textBox1, 0);
            base.Controls.SetChildIndex(this.btnExit, 0);
            base.Controls.SetChildIndex(this.btnSend, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public static void setMessage(string message)
        {
            adminMessage = message;
        }

        public static void showAdminEdit()
        {
            AdminInfoPopup popup = new AdminInfoPopup {
                btnSend = { Visible = true },
                textBox1 = { ReadOnly = false, Text = adminMessage }
            };
            popup.init();
            popup.Show();
            lastPopup = popup;
        }

        public static void showMessage()
        {
            if (adminMessage.StartsWith("http://"))
            {
                string[] strArray = adminMessage.Split(delims);
                if (strArray.Length > 0)
                {
                    VideoWindow.ShowVideo(strArray[0], false);
                    return;
                }
            }
            AdminInfoPopup popup = new AdminInfoPopup {
                btnSend = { Visible = false },
                textBox1 = { ReadOnly = true, Text = adminMessage }
            };
            popup.init();
            popup.Show();
            popup.btnExit.Focus();
            lastPopup = popup;
            RemoteServices.Instance.ShowAdminMessage = false;
        }
    }
}

