namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class EmailOptInPopup : MyFormBase
    {
        private BitmapButton btnClose;
        private CheckBox cbMailOptIn;
        private IContainer components;
        private Label label3;
        public ProfileLoginWindow m_Parent;

        public EmailOptInPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            base.ShowClose = false;
            this.cbMailOptIn.Text = SK.Text("EMAIL_OptIn", "Please Tick here if you would like us to contact you via email with information related to Stronghold Kingdoms, including exclusive offers and competitions.");
            this.cbMailOptIn.Checked = false;
            base.Title = this.Text = SK.Text("EMAIL_OptInHeader", "Stronghold Kingdoms Email Preferences");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_close");
            if (this.m_Parent != null)
            {
                this.m_Parent.SetEmailOptInState(this.cbMailOptIn.Checked);
            }
            base.Close();
        }

        private void cbMailOptIn_CheckedChanged(object sender, EventArgs e)
        {
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
            this.label3 = new Label();
            this.btnClose = new BitmapButton();
            this.cbMailOptIn = new CheckBox();
            base.SuspendLayout();
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 0x4c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4f, 13);
            this.label3.TabIndex = 0x10;
            this.label3.Text = "Search Results";
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
            this.btnClose.Location = new Point(0x150, 0x73);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(110, 0x1b);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 0x11;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.cbMailOptIn.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.cbMailOptIn.BackColor = ARGBColors.Transparent;
            this.cbMailOptIn.Location = new Point(0x20, 0x25);
            this.cbMailOptIn.Name = "cbMailOptIn";
            this.cbMailOptIn.Size = new Size(0x18e, 0x48);
            this.cbMailOptIn.TabIndex = 0x12;
            this.cbMailOptIn.Text = "Mail Opt In";
            this.cbMailOptIn.UseVisualStyleBackColor = false;
            this.cbMailOptIn.CheckedChanged += new EventHandler(this.cbMailOptIn_CheckedChanged);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x1ca, 0x9a);
            base.Controls.Add(this.cbMailOptIn);
            base.Controls.Add(this.btnClose);
            base.Icon = Resources.shk_icon;
            base.Name = "EmailOptInPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Add Users";
            base.TopMost = true;
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.cbMailOptIn, 0);
            base.ResumeLayout(false);
        }
    }
}

