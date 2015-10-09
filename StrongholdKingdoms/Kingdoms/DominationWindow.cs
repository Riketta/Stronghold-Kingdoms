namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DominationWindow : MyFormBase
    {
        private BitmapButton btnClose;
        private IContainer components;
        private Label lblDominationInfo;
        private Label lblDuration;

        public DominationWindow()
        {
            this.InitializeComponent();
            base.closeCallback = new MyFormBase.MFBClose(this.domCloseCallback);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblDuration.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
            this.Text = base.Title = SK.Text("Domination_World", "Domination World");
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.lblDominationInfo.Text = SK.Text("Domination_Info", "Domination World will end in");
            this.lblDuration.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.closeDominatonWindow();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void domCloseCallback()
        {
            InterfaceMgr.Instance.closeDominatonWindow();
        }

        private void InitializeComponent()
        {
            this.lblDuration = new Label();
            this.lblDominationInfo = new Label();
            this.btnClose = new BitmapButton();
            base.SuspendLayout();
            this.lblDuration.BackColor = ARGBColors.Transparent;
            this.lblDuration.ForeColor = ARGBColors.Black;
            this.lblDuration.Location = new Point(3, 0x62);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new Size(0x1a3, 20);
            this.lblDuration.TabIndex = 0x10;
            this.lblDuration.Text = "0";
            this.lblDuration.TextAlign = ContentAlignment.MiddleCenter;
            this.lblDominationInfo.BackColor = ARGBColors.Transparent;
            this.lblDominationInfo.ForeColor = ARGBColors.Black;
            this.lblDominationInfo.Location = new Point(0, 0x35);
            this.lblDominationInfo.Name = "lblDominationInfo";
            this.lblDominationInfo.Size = new Size(0x1a6, 0x18);
            this.lblDominationInfo.TabIndex = 0x11;
            this.lblDominationInfo.Text = "Domination World will end in";
            this.lblDominationInfo.TextAlign = ContentAlignment.MiddleCenter;
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
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
            this.btnClose.Location = new Point(0x11b, 0x90);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(0x81, 0x1a);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x80, 0x91, 0x9c);
            base.ClientSize = new Size(0x1a8, 0xb6);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.lblDominationInfo);
            base.Controls.Add(this.lblDuration);
            base.Name = "DominationWindow";
            base.ShowClose = true;
            this.Text = "DominationWindow";
            base.Controls.SetChildIndex(this.lblDuration, 0);
            base.Controls.SetChildIndex(this.lblDominationInfo, 0);
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.ResumeLayout(false);
        }

        public void updateText(string text)
        {
            this.lblDuration.Text = text;
        }
    }
}

