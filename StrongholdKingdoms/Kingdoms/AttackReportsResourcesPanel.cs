namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AttackReportsResourcesPanel : MyFormBase
    {
        private BitmapButton btnClose;
        private IContainer components;
        private Panel img1;
        private Panel img2;
        private Panel img3;
        private Panel img4;
        private Panel img5;
        private Panel img6;
        private Panel img7;
        private Panel img8;
        private Label lblResource1;
        private Label lblResource2;
        private Label lblResource3;
        private Label lblResource4;
        private Label lblResource5;
        private Label lblResource6;
        private Label lblResource7;
        private Label lblResource8;

        public AttackReportsResourcesPanel()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
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
            this.img1 = new Panel();
            this.img3 = new Panel();
            this.img5 = new Panel();
            this.img7 = new Panel();
            this.lblResource1 = new Label();
            this.lblResource3 = new Label();
            this.lblResource5 = new Label();
            this.lblResource7 = new Label();
            this.lblResource8 = new Label();
            this.lblResource6 = new Label();
            this.lblResource4 = new Label();
            this.lblResource2 = new Label();
            this.img8 = new Panel();
            this.img6 = new Panel();
            this.img4 = new Panel();
            this.img2 = new Panel();
            this.btnClose = new BitmapButton();
            base.SuspendLayout();
            this.img1.BackColor = ARGBColors.Transparent;
            this.img1.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img1.BackgroundImageLayout = ImageLayout.Center;
            this.img1.Location = new Point(0x20, 0x2e);
            this.img1.Name = "img1";
            this.img1.Size = new Size(0x20, 0x20);
            this.img1.TabIndex = 0x6c;
            this.img3.BackColor = ARGBColors.Transparent;
            this.img3.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img3.BackgroundImageLayout = ImageLayout.Center;
            this.img3.Location = new Point(0x20, 0x54);
            this.img3.Name = "img3";
            this.img3.Size = new Size(0x20, 0x20);
            this.img3.TabIndex = 0x6c;
            this.img5.BackColor = ARGBColors.Transparent;
            this.img5.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img5.BackgroundImageLayout = ImageLayout.Center;
            this.img5.Location = new Point(0x20, 0x7a);
            this.img5.Name = "img5";
            this.img5.Size = new Size(0x20, 0x20);
            this.img5.TabIndex = 0x6c;
            this.img7.BackColor = ARGBColors.Transparent;
            this.img7.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img7.BackgroundImageLayout = ImageLayout.Center;
            this.img7.Location = new Point(0x20, 160);
            this.img7.Name = "img7";
            this.img7.Size = new Size(0x20, 0x20);
            this.img7.TabIndex = 0x6c;
            this.lblResource1.BackColor = ARGBColors.Transparent;
            this.lblResource1.Location = new Point(70, 0x37);
            this.lblResource1.Name = "lblResource1";
            this.lblResource1.Size = new Size(0x51, 13);
            this.lblResource1.TabIndex = 0x6f;
            this.lblResource1.Text = "0";
            this.lblResource1.TextAlign = ContentAlignment.TopRight;
            this.lblResource3.BackColor = ARGBColors.Transparent;
            this.lblResource3.Location = new Point(70, 0x5e);
            this.lblResource3.Name = "lblResource3";
            this.lblResource3.Size = new Size(0x51, 13);
            this.lblResource3.TabIndex = 0x70;
            this.lblResource3.Text = "0";
            this.lblResource3.TextAlign = ContentAlignment.TopRight;
            this.lblResource5.BackColor = ARGBColors.Transparent;
            this.lblResource5.Location = new Point(70, 0x83);
            this.lblResource5.Name = "lblResource5";
            this.lblResource5.Size = new Size(0x51, 13);
            this.lblResource5.TabIndex = 0x71;
            this.lblResource5.Text = "0";
            this.lblResource5.TextAlign = ContentAlignment.TopRight;
            this.lblResource7.BackColor = ARGBColors.Transparent;
            this.lblResource7.Location = new Point(70, 0xa9);
            this.lblResource7.Name = "lblResource7";
            this.lblResource7.Size = new Size(0x51, 13);
            this.lblResource7.TabIndex = 0x72;
            this.lblResource7.Text = "0";
            this.lblResource7.TextAlign = ContentAlignment.TopRight;
            this.lblResource8.BackColor = ARGBColors.Transparent;
            this.lblResource8.Location = new Point(0x100, 0xa9);
            this.lblResource8.Name = "lblResource8";
            this.lblResource8.Size = new Size(0x51, 13);
            this.lblResource8.TabIndex = 0x7a;
            this.lblResource8.Text = "0";
            this.lblResource8.TextAlign = ContentAlignment.TopRight;
            this.lblResource6.BackColor = ARGBColors.Transparent;
            this.lblResource6.Location = new Point(0x100, 0x83);
            this.lblResource6.Name = "lblResource6";
            this.lblResource6.Size = new Size(0x51, 13);
            this.lblResource6.TabIndex = 0x79;
            this.lblResource6.Text = "0";
            this.lblResource6.TextAlign = ContentAlignment.TopRight;
            this.lblResource4.BackColor = ARGBColors.Transparent;
            this.lblResource4.Location = new Point(0x100, 0x5e);
            this.lblResource4.Name = "lblResource4";
            this.lblResource4.Size = new Size(0x51, 13);
            this.lblResource4.TabIndex = 120;
            this.lblResource4.Text = "0";
            this.lblResource4.TextAlign = ContentAlignment.TopRight;
            this.lblResource2.BackColor = ARGBColors.Transparent;
            this.lblResource2.Location = new Point(0x100, 0x37);
            this.lblResource2.Name = "lblResource2";
            this.lblResource2.Size = new Size(0x51, 13);
            this.lblResource2.TabIndex = 0x77;
            this.lblResource2.Text = "0";
            this.lblResource2.TextAlign = ContentAlignment.TopRight;
            this.img8.BackColor = ARGBColors.Transparent;
            this.img8.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img8.BackgroundImageLayout = ImageLayout.Center;
            this.img8.Location = new Point(0xda, 160);
            this.img8.Name = "img8";
            this.img8.Size = new Size(0x20, 0x20);
            this.img8.TabIndex = 0x74;
            this.img6.BackColor = ARGBColors.Transparent;
            this.img6.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img6.BackgroundImageLayout = ImageLayout.Center;
            this.img6.Location = new Point(0xda, 0x7a);
            this.img6.Name = "img6";
            this.img6.Size = new Size(0x20, 0x20);
            this.img6.TabIndex = 0x73;
            this.img4.BackColor = ARGBColors.Transparent;
            this.img4.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img4.BackgroundImageLayout = ImageLayout.Center;
            this.img4.Location = new Point(0xda, 0x54);
            this.img4.Name = "img4";
            this.img4.Size = new Size(0x20, 0x20);
            this.img4.TabIndex = 0x76;
            this.img2.BackColor = ARGBColors.Transparent;
            this.img2.BackgroundImage = (Image) GFXLibrary.com_32_wood;
            this.img2.BackgroundImageLayout = ImageLayout.Center;
            this.img2.Location = new Point(0xda, 0x2e);
            this.img2.Name = "img2";
            this.img2.Size = new Size(0x20, 0x20);
            this.img2.TabIndex = 0x75;
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
            this.btnClose.Location = new Point(0x103, 0xd0);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(0x62, 0x17);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 0x7b;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x17a, 0xf3);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.lblResource8);
            base.Controls.Add(this.lblResource6);
            base.Controls.Add(this.lblResource4);
            base.Controls.Add(this.lblResource2);
            base.Controls.Add(this.img8);
            base.Controls.Add(this.img6);
            base.Controls.Add(this.img4);
            base.Controls.Add(this.img2);
            base.Controls.Add(this.lblResource7);
            base.Controls.Add(this.lblResource5);
            base.Controls.Add(this.lblResource3);
            base.Controls.Add(this.lblResource1);
            base.Controls.Add(this.img7);
            base.Controls.Add(this.img5);
            base.Controls.Add(this.img3);
            base.Controls.Add(this.img1);
            base.Icon = Resources.shk_icon;
            base.Name = "AttackReportsResourcesPanel";
            base.ShowClose = false;
            base.ShowIcon = false;
            this.Text = "Resources";
            base.TopMost = true;
            base.Controls.SetChildIndex(this.img1, 0);
            base.Controls.SetChildIndex(this.img3, 0);
            base.Controls.SetChildIndex(this.img5, 0);
            base.Controls.SetChildIndex(this.img7, 0);
            base.Controls.SetChildIndex(this.lblResource1, 0);
            base.Controls.SetChildIndex(this.lblResource3, 0);
            base.Controls.SetChildIndex(this.lblResource5, 0);
            base.Controls.SetChildIndex(this.lblResource7, 0);
            base.Controls.SetChildIndex(this.img2, 0);
            base.Controls.SetChildIndex(this.img4, 0);
            base.Controls.SetChildIndex(this.img6, 0);
            base.Controls.SetChildIndex(this.img8, 0);
            base.Controls.SetChildIndex(this.lblResource2, 0);
            base.Controls.SetChildIndex(this.lblResource4, 0);
            base.Controls.SetChildIndex(this.lblResource6, 0);
            base.Controls.SetChildIndex(this.lblResource8, 0);
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.ResumeLayout(false);
        }

        public void setResources(GetReport_ReturnType data)
        {
            this.Text = base.Title = SK.Text("GENERIC_Resources", "Resources");
            this.img1.Visible = false;
            this.img2.Visible = false;
            this.img3.Visible = false;
            this.img4.Visible = false;
            this.img5.Visible = false;
            this.img6.Visible = false;
            this.img7.Visible = false;
            this.img8.Visible = false;
            this.lblResource1.Visible = false;
            this.lblResource2.Visible = false;
            this.lblResource3.Visible = false;
            this.lblResource4.Visible = false;
            this.lblResource5.Visible = false;
            this.lblResource6.Visible = false;
            this.lblResource7.Visible = false;
            this.lblResource8.Visible = false;
            switch (data.genericData30)
            {
                case 2:
                    this.img1.Visible = true;
                    this.lblResource1.Visible = true;
                    this.img1.BackgroundImage = (Image) GFXLibrary.com_32_wood;
                    this.lblResource1.Text = data.genericData22.ToString();
                    this.img2.Visible = true;
                    this.lblResource2.Visible = true;
                    this.img2.BackgroundImage = (Image) GFXLibrary.com_32_stone;
                    this.lblResource2.Text = data.genericData23.ToString();
                    this.img3.Visible = true;
                    this.lblResource3.Visible = true;
                    this.img3.BackgroundImage = (Image) GFXLibrary.com_32_iron;
                    this.lblResource3.Text = data.genericData24.ToString();
                    this.img4.Visible = true;
                    this.lblResource4.Visible = true;
                    this.img4.BackgroundImage = (Image) GFXLibrary.com_32_pitch;
                    this.lblResource4.Text = data.genericData25.ToString();
                    return;

                case 3:
                    break;

                case 4:
                    this.img1.Visible = true;
                    this.lblResource1.Visible = true;
                    this.img1.BackgroundImage = (Image) GFXLibrary.com_32_apples;
                    this.lblResource1.Text = data.genericData22.ToString();
                    this.img2.Visible = true;
                    this.lblResource2.Visible = true;
                    this.img2.BackgroundImage = (Image) GFXLibrary.com_32_bread;
                    this.lblResource2.Text = data.genericData23.ToString();
                    this.img3.Visible = true;
                    this.lblResource3.Visible = true;
                    this.img3.BackgroundImage = (Image) GFXLibrary.com_32_cheese;
                    this.lblResource3.Text = data.genericData24.ToString();
                    this.img4.Visible = true;
                    this.lblResource4.Visible = true;
                    this.img4.BackgroundImage = (Image) GFXLibrary.com_32_meat;
                    this.lblResource4.Text = data.genericData25.ToString();
                    this.img5.Visible = true;
                    this.lblResource5.Visible = true;
                    this.img5.BackgroundImage = (Image) GFXLibrary.com_32_fish;
                    this.lblResource5.Text = data.genericData26.ToString();
                    this.img6.Visible = true;
                    this.lblResource6.Visible = true;
                    this.img6.BackgroundImage = (Image) GFXLibrary.com_32_veg;
                    this.lblResource6.Text = data.genericData27.ToString();
                    return;

                case 5:
                    this.img1.Visible = true;
                    this.lblResource1.Visible = true;
                    this.img1.BackgroundImage = (Image) GFXLibrary.com_32_furniture;
                    this.lblResource1.Text = data.genericData22.ToString();
                    this.img2.Visible = true;
                    this.lblResource2.Visible = true;
                    this.img2.BackgroundImage = (Image) GFXLibrary.com_32_clothing;
                    this.lblResource2.Text = data.genericData23.ToString();
                    this.img3.Visible = true;
                    this.lblResource3.Visible = true;
                    this.img3.BackgroundImage = (Image) GFXLibrary.com_32_venison;
                    this.lblResource3.Text = data.genericData24.ToString();
                    this.img4.Visible = true;
                    this.lblResource4.Visible = true;
                    this.img4.BackgroundImage = (Image) GFXLibrary.com_32_wine;
                    this.lblResource4.Text = data.genericData25.ToString();
                    this.img5.Visible = true;
                    this.lblResource5.Visible = true;
                    this.img5.BackgroundImage = (Image) GFXLibrary.com_32_salt;
                    this.lblResource5.Text = data.genericData26.ToString();
                    this.img6.Visible = true;
                    this.lblResource6.Visible = true;
                    this.img6.BackgroundImage = (Image) GFXLibrary.com_32_metalwork;
                    this.lblResource6.Text = data.genericData27.ToString();
                    this.img7.Visible = true;
                    this.lblResource7.Visible = true;
                    this.img7.BackgroundImage = (Image) GFXLibrary.com_32_spice;
                    this.lblResource7.Text = data.genericData28.ToString();
                    this.img8.Visible = true;
                    this.lblResource8.Visible = true;
                    this.img8.BackgroundImage = (Image) GFXLibrary.com_32_silk;
                    this.lblResource8.Text = data.genericData29.ToString();
                    return;

                case 6:
                    this.img1.Visible = true;
                    this.lblResource1.Visible = true;
                    this.img1.BackgroundImage = (Image) GFXLibrary.com_32_ale;
                    this.lblResource1.Text = data.genericData22.ToString();
                    return;

                case 7:
                    this.img1.Visible = true;
                    this.lblResource1.Visible = true;
                    this.img1.BackgroundImage = (Image) GFXLibrary.com_32_bows;
                    this.lblResource1.Text = data.genericData22.ToString();
                    this.img2.Visible = true;
                    this.lblResource2.Visible = true;
                    this.img2.BackgroundImage = (Image) GFXLibrary.com_32_pikes;
                    this.lblResource2.Text = data.genericData23.ToString();
                    this.img3.Visible = true;
                    this.lblResource3.Visible = true;
                    this.img3.BackgroundImage = (Image) GFXLibrary.com_32_swords;
                    this.lblResource3.Text = data.genericData24.ToString();
                    this.img4.Visible = true;
                    this.lblResource4.Visible = true;
                    this.img4.BackgroundImage = (Image) GFXLibrary.com_32_armour;
                    this.lblResource4.Text = data.genericData25.ToString();
                    this.img5.Visible = true;
                    this.lblResource5.Visible = true;
                    this.img5.BackgroundImage = (Image) GFXLibrary.com_32_catapults;
                    this.lblResource5.Text = data.genericData26.ToString();
                    break;

                default:
                    return;
            }
        }
    }
}

