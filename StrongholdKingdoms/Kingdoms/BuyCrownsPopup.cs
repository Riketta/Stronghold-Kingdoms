namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BuyCrownsPopup : MyFormBase
    {
        private BitmapButton btnBuyCrowns;
        private IContainer components;
        private Label label3;
        private Label lblMessage;
        private Form m_parent;

        public BuyCrownsPopup()
        {
            this.InitializeComponent();
            this.lblMessage.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
            base.Title = this.Text = SK.Text("BuyCardsPanel_Low_Crowns", "Crown stocks are too low m'lord");
            this.btnBuyCrowns.Text = SK.Text("BuyCrownsPanel_Buy_Crowns", "Buy Crowns");
            this.btnBuyCrowns.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ((PlayCardsWindow) this.m_parent).GetCrowns();
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

        public void init(int numCrownsNeeded, Form parent)
        {
            this.m_parent = parent;
            this.lblMessage.Text = SK.Text("BuyCardsPanel_Cannot_Afford", "You cannot afford this.") + Environment.NewLine + Environment.NewLine + SK.Text("BuyCardsPanel_Extra_Crowns_Needed", "Extra Crowns Needed") + " : " + numCrownsNeeded.ToString();
        }

        private void InitializeComponent()
        {
            this.btnBuyCrowns = new BitmapButton();
            this.label3 = new Label();
            this.lblMessage = new Label();
            base.SuspendLayout();
            this.btnBuyCrowns.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnBuyCrowns.BorderColor = ARGBColors.DarkBlue;
            this.btnBuyCrowns.BorderDrawing = true;
            this.btnBuyCrowns.FocusRectangleEnabled = false;
            this.btnBuyCrowns.Image = null;
            this.btnBuyCrowns.ImageBorderColor = ARGBColors.Chocolate;
            this.btnBuyCrowns.ImageBorderEnabled = true;
            this.btnBuyCrowns.ImageDropShadow = true;
            this.btnBuyCrowns.ImageFocused = null;
            this.btnBuyCrowns.ImageInactive = null;
            this.btnBuyCrowns.ImageMouseOver = null;
            this.btnBuyCrowns.ImageNormal = null;
            this.btnBuyCrowns.ImagePressed = null;
            this.btnBuyCrowns.InnerBorderColor = ARGBColors.LightGray;
            this.btnBuyCrowns.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnBuyCrowns.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnBuyCrowns.Location = new Point(0x73, 0x79);
            this.btnBuyCrowns.Name = "btnBuyCrowns";
            this.btnBuyCrowns.OffsetPressedContent = true;
            this.btnBuyCrowns.Padding2 = 5;
            this.btnBuyCrowns.Size = new Size(0xc9, 0x27);
            this.btnBuyCrowns.StretchImage = false;
            this.btnBuyCrowns.TabIndex = 2;
            this.btnBuyCrowns.Text = "Buy Crowns";
            this.btnBuyCrowns.TextDropShadow = false;
            this.btnBuyCrowns.UseVisualStyleBackColor = false;
            this.btnBuyCrowns.Click += new EventHandler(this.btnOK_Click);
            this.label3.AutoSize = true;
            this.label3.BackColor = ARGBColors.Transparent;
            this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = ARGBColors.White;
            this.label3.Location = new Point(0xb3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 0x10);
            this.label3.TabIndex = 9;
            this.lblMessage.BackColor = ARGBColors.Transparent;
            this.lblMessage.Location = new Point(12, 0x2c);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(0x196, 0x41);
            this.lblMessage.TabIndex = 13;
            this.lblMessage.Text = "label1";
            this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.ClientSize = new Size(430, 0xc7);
            base.Controls.Add(this.lblMessage);
            base.Controls.Add(this.btnBuyCrowns);
            base.Icon = Resources.shk_icon;
            base.Name = "BuyCrownsPopup";
            base.ShowClose = true;
            base.Controls.SetChildIndex(this.btnBuyCrowns, 0);
            base.Controls.SetChildIndex(this.lblMessage, 0);
            base.ResumeLayout(false);
        }
    }
}

