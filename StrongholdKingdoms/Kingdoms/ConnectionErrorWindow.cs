namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConnectionErrorWindow : MyFormBase
    {
        private BitmapButton btnLogout;
        private IContainer components;
        private DateTime lastRetry = DateTime.MinValue;
        private Label lblMessage;
        private static ConnectionErrorWindow popup;
        private DateTime startTime = DateTime.MinValue;

        public ConnectionErrorWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblMessage.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
            this.Text = base.Title = SK.Text("ConnectioError_Title", "Problem with Connection to Server");
            this.btnLogout.Text = SK.Text("ConnectioError_logout", "Quit to Login Screen");
            this.lblMessage.Text = SK.Text("ConnectioError_message", "Your Stronghold Kingdoms client is having problems connecting to the game servers. Trying to connect to the server again...");
            base.ShowClose = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.forceLogout();
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
            this.startTime = DateTime.Now;
        }

        private void InitializeComponent()
        {
            this.btnLogout = new BitmapButton();
            this.lblMessage = new Label();
            base.SuspendLayout();
            this.btnLogout.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnLogout.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnLogout.BorderColor = ARGBColors.DarkBlue;
            this.btnLogout.BorderDrawing = true;
            this.btnLogout.FocusRectangleEnabled = false;
            this.btnLogout.Image = null;
            this.btnLogout.ImageBorderColor = ARGBColors.Chocolate;
            this.btnLogout.ImageBorderEnabled = true;
            this.btnLogout.ImageDropShadow = true;
            this.btnLogout.ImageFocused = null;
            this.btnLogout.ImageInactive = null;
            this.btnLogout.ImageMouseOver = null;
            this.btnLogout.ImageNormal = null;
            this.btnLogout.ImagePressed = null;
            this.btnLogout.InnerBorderColor = ARGBColors.LightGray;
            this.btnLogout.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnLogout.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnLogout.Location = new Point(0xdd, 0x6f);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.OffsetPressedContent = true;
            this.btnLogout.Padding2 = 5;
            this.btnLogout.Size = new Size(0xbf, 0x1a);
            this.btnLogout.StretchImage = false;
            this.btnLogout.TabIndex = 20;
            this.btnLogout.Text = "Quit to Login Screen";
            this.btnLogout.TextDropShadow = false;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);
            this.lblMessage.BackColor = ARGBColors.Transparent;
            this.lblMessage.Location = new Point(11, 0x22);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(0x191, 0x45);
            this.lblMessage.TabIndex = 0x15;
            this.lblMessage.Text = "label1";
            this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x80, 0x91, 0x9c);
            base.ClientSize = new Size(0x1a8, 0x95);
            base.Controls.Add(this.lblMessage);
            base.Controls.Add(this.btnLogout);
            base.Name = "ConnectionErrorWindow";
            base.ShowClose = true;
            this.Text = "ConnectionErrorWindow";
            base.Controls.SetChildIndex(this.btnLogout, 0);
            base.Controls.SetChildIndex(this.lblMessage, 0);
            base.ResumeLayout(false);
        }

        public void update()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.startTime);
            if (span.TotalMinutes > 10.0)
            {
                GameEngine.Instance.forceLogout();
            }
            else
            {
                TimeSpan span2 = (TimeSpan) (DateTime.Now - this.lastRetry);
                if (span2.TotalSeconds > 30.0)
                {
                    RemoteServices.Instance.clearQueues();
                    this.lastRetry = DateTime.Now;
                    RemoteServices.Instance.LeaderBoard(-3, -1, -1, DateTime.MinValue);
                }
            }
        }
    }
}

