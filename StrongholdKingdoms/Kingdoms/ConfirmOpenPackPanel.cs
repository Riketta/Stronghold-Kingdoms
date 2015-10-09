namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ConfirmOpenPackPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLine bottom = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDImage bottomRightImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton confirmButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDCheckBox confirmCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDLabel confirmLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLine left = new CustomSelfDrawPanel.CSDLine();
        private CardClickPlayDelegate m_callback;
        private CustomSelfDrawPanel.CSDButton maxButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton middleButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton minButton = new CustomSelfDrawPanel.CSDButton();
        private NumericUpDown numMultiple;
        private CustomSelfDrawPanel.CSDLabel packTypeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLine right = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine top = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDImage topLeftImage = new CustomSelfDrawPanel.CSDImage();

        public ConfirmOpenPackPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            if (!this.confirmCheck.Checked)
            {
                Program.mySettings.OpenMultipleCardPacks = false;
                Program.mySettings.Save();
            }
            InterfaceMgr.Instance.OpenPackMultiple = (int) this.numMultiple.Value;
            InterfaceMgr.Instance.closeConfirmOpenPackPopup();
            Form form = InterfaceMgr.Instance.getCardWindow();
            if (form != null)
            {
                form.TopMost = true;
                form.TopMost = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(CustomSelfDrawPanel.UICardPack pack, CardClickPlayDelegate callback)
        {
            this.m_callback = callback;
            base.clearControls();
            this.background.Size = base.Size;
            this.background.Position = new Point(0, 0);
            base.addControl(this.background);
            this.background.Create((Image) GFXLibrary.cardpanel_grey_9slice_left_top, (Image) GFXLibrary.cardpanel_grey_9slice_middle_top, (Image) GFXLibrary.cardpanel_grey_9slice_right_top, (Image) GFXLibrary.cardpanel_grey_9slice_left_middle, (Image) GFXLibrary.cardpanel_grey_9slice_middle_middle, (Image) GFXLibrary.cardpanel_grey_9slice_right_middle, (Image) GFXLibrary.cardpanel_grey_9slice_left_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_middle_bottom, (Image) GFXLibrary.cardpanel_grey_9slice_right_bottom);
            this.topLeftImage.Image = (Image) GFXLibrary.cardpanel_grey_9slice_gradation_top_left;
            this.topLeftImage.Position = new Point(0, 0);
            this.background.addControl(this.topLeftImage);
            this.bottomRightImage.Image = (Image) GFXLibrary.cardpanel_grey_9slice_gradation_bottom;
            this.bottomRightImage.Position = new Point(this.background.Width - this.bottomRightImage.Image.Width, this.background.Height - this.bottomRightImage.Image.Height);
            this.background.addControl(this.bottomRightImage);
            string category = GameEngine.Instance.World.ProfileCardOffers[pack.PackIDs[0]].Category;
            int num = 0;
            foreach (CardTypes.UserCardPack pack2 in GameEngine.Instance.World.ProfileUserCardPacks.Values)
            {
                if (GameEngine.Instance.World.ProfileCardOffers[pack2.PackID].Category == category)
                {
                    num += pack2.Count;
                }
            }
            if (num > 10)
            {
                num = 10;
            }
            int num2 = num;
            this.numMultiple = new NumericUpDown();
            base.Controls.Add(this.numMultiple);
            this.numMultiple.Minimum = 1M;
            this.numMultiple.Maximum = num2;
            this.numMultiple.Increment = 1M;
            this.numMultiple.Left = (base.Width / 2) - (this.numMultiple.Width / 2);
            this.numMultiple.Top = ((base.Height / 2) - (this.numMultiple.Height / 2)) - 20;
            this.numMultiple.DecimalPlaces = 0;
            this.numMultiple.KeyUp += new KeyEventHandler(this.numMultiple_KeyUp);
            this.confirmLabel.Text = SK.Text("ConfirmOpenPack_HowMany", "How many packs of this type would you like to open?");
            this.confirmLabel.Color = ARGBColors.Black;
            this.confirmLabel.Position = new Point(20, 10);
            this.confirmLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.confirmLabel.Size = new Size(this.background.Width - 40, 80);
            this.confirmLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.background.addControl(this.confirmLabel);
            this.packTypeLabel.Text = pack.nameText;
            this.packTypeLabel.Color = ARGBColors.Black;
            this.packTypeLabel.Position = new Point(20, 80);
            this.packTypeLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.packTypeLabel.Size = new Size(this.background.Width - 40, 80);
            this.packTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.background.addControl(this.packTypeLabel);
            this.confirmButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.confirmButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
            this.confirmButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
            this.confirmButton.Position = new Point(230, 190);
            this.confirmButton.Text.Text = SK.Text("ConfirmOpenPack_OpenPacks", "Open Packs");
            this.confirmButton.TextYOffset = -2;
            this.confirmButton.Text.Color = ARGBColors.Black;
            this.confirmButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.confirmButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playCard), "ConfirmOpenPackPanel_confirm_open_pack");
            this.background.addControl(this.confirmButton);
            this.minButton.ImageNorm = (Image) GFXLibrary.building_icon_circle;
            this.minButton.ImageOver = (Image) GFXLibrary.building_icon_circle;
            this.minButton.ImageClick = (Image) GFXLibrary.building_icon_circle;
            this.minButton.Position = new Point(this.numMultiple.Left, 0x87);
            this.minButton.Text.Text = this.numMultiple.Minimum.ToString();
            this.minButton.TextYOffset = -1;
            this.minButton.Text.Color = ARGBColors.Black;
            this.minButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.minButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.minAmount), "SetOpenPackAmount_Minimum");
            this.background.addControl(this.minButton);
            this.middleButton.ImageNorm = (Image) GFXLibrary.building_icon_circle;
            this.middleButton.ImageOver = (Image) GFXLibrary.building_icon_circle;
            this.middleButton.ImageClick = (Image) GFXLibrary.building_icon_circle;
            this.middleButton.Position = new Point((this.numMultiple.Left + (this.numMultiple.Width / 2)) - (this.middleButton.Width / 2), 0x87);
            this.middleButton.TextYOffset = -1;
            this.middleButton.Text.Text = (((int) (this.numMultiple.Minimum + this.numMultiple.Maximum)) / 2).ToString();
            this.middleButton.Text.Color = ARGBColors.Black;
            this.middleButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.middleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.middleAmount), "SetOpenPackAmount_Middle");
            this.background.addControl(this.middleButton);
            this.maxButton.ImageNorm = (Image) GFXLibrary.building_icon_circle;
            this.maxButton.ImageOver = (Image) GFXLibrary.building_icon_circle;
            this.maxButton.ImageClick = (Image) GFXLibrary.building_icon_circle;
            this.maxButton.Position = new Point((this.numMultiple.Left + this.numMultiple.Width) - this.maxButton.Width, 0x87);
            this.maxButton.TextYOffset = -1;
            this.maxButton.Text.Text = this.numMultiple.Maximum.ToString();
            this.maxButton.Text.Color = ARGBColors.Black;
            this.maxButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.maxButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.maxAmount), "SetOpenPackAmount_Maximum");
            this.background.addControl(this.maxButton);
            this.left.Position = new Point(this.numMultiple.Left - 5, this.numMultiple.Top - 5);
            this.left.Height = ((this.minButton.Position.Y + this.minButton.Height) - this.left.Position.Y) + 5;
            this.left.LineColor = ARGBColors.Black;
            this.left.Width = 0;
            this.background.addControl(this.left);
            this.right.Position = new Point(this.numMultiple.Right + 5, this.numMultiple.Top - 5);
            this.right.Height = ((this.minButton.Position.Y + this.minButton.Height) - this.right.Position.Y) + 5;
            this.right.LineColor = ARGBColors.Black;
            this.right.Width = 0;
            this.background.addControl(this.right);
            this.top.Position = new Point(this.numMultiple.Left - 5, this.numMultiple.Top - 5);
            this.top.Width = this.right.Position.X - this.left.Position.X;
            this.top.LineColor = ARGBColors.Black;
            this.top.Height = 0;
            this.background.addControl(this.top);
            this.bottom.Position = new Point(this.numMultiple.Left - 5, (this.minButton.Position.Y + this.minButton.Height) + 5);
            this.bottom.Width = this.right.Position.X - this.left.Position.X;
            this.bottom.LineColor = ARGBColors.Black;
            this.bottom.Height = 0;
            this.background.addControl(this.bottom);
            this.cancelButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.cancelButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
            this.cancelButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
            this.cancelButton.Position = new Point(30, 190);
            this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.cancelButton.TextYOffset = -2;
            this.cancelButton.Text.Color = ARGBColors.Black;
            this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ConfirmOpenPackPanel_cancel");
            this.background.addControl(this.cancelButton);
            this.confirmCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.confirmCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.confirmCheck.Position = new Point(20, 240);
            this.confirmCheck.Checked = true;
            this.confirmCheck.CBLabel.Text = SK.Text("ConfirmOpenPack_AlwaysAsk", "Always ask to open multiple packs?");
            this.confirmCheck.CBLabel.Color = ARGBColors.Black;
            this.confirmCheck.CBLabel.Position = new Point(20, -1);
            this.confirmCheck.CBLabel.Size = new Size(360, 0x23);
            this.confirmCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.background.addControl(this.confirmCheck);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void maxAmount()
        {
            this.numMultiple.Value = this.numMultiple.Maximum;
        }

        private void middleAmount()
        {
            this.numMultiple.Value = (int) ((this.numMultiple.Minimum + this.numMultiple.Maximum) / 2M);
        }

        private void minAmount()
        {
            this.numMultiple.Value = 1M;
        }

        private void numMultiple_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if ((int.Parse(this.numMultiple.Text) < this.numMultiple.Minimum) || (int.Parse(this.numMultiple.Text) > this.numMultiple.Maximum))
                {
                    this.numMultiple.Text = "";
                    this.numMultiple.Value = this.numMultiple.Minimum;
                }
            }
            catch (Exception)
            {
                this.numMultiple.Text = "";
                this.numMultiple.Value = this.numMultiple.Minimum;
            }
        }

        private void playCard()
        {
            if (!this.confirmCheck.Checked)
            {
                Program.mySettings.OpenMultipleCardPacks = false;
                Program.mySettings.Save();
            }
            InterfaceMgr.Instance.OpenPackMultiple = (int) this.numMultiple.Value;
            InterfaceMgr.Instance.closeConfirmOpenPackPopup();
            Form form = InterfaceMgr.Instance.getCardWindow();
            if (form != null)
            {
                form.TopMost = true;
                form.TopMost = false;
            }
            if (this.m_callback != null)
            {
                this.m_callback(false);
            }
        }

        public void update()
        {
        }

        public int Multiple
        {
            get
            {
                return (int) this.numMultiple.Value;
            }
        }

        public delegate void CardClickPlayDelegate(bool fromClick);
    }
}

