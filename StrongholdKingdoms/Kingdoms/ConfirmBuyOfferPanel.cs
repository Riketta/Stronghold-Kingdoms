namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ConfirmBuyOfferPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel background = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDImage bottomRightImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton confirmButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDCheckBox confirmCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDLabel confirmLabel = new CustomSelfDrawPanel.CSDLabel();
        private CardClickPlayDelegate m_callback;
        private NumericUpDown numMultiple;
        private CustomSelfDrawPanel.CSDLabel packTypeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage topLeftImage = new CustomSelfDrawPanel.CSDImage();

        public ConfirmBuyOfferPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            if (!this.confirmCheck.Checked)
            {
                Program.mySettings.BuyMultipleCardPacks = false;
                Program.mySettings.Save();
            }
            InterfaceMgr.Instance.closeConfirmBuyOfferPopup();
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

        public void init(CustomSelfDrawPanel.UICardOffer offer, CardClickPlayDelegate callback)
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
            int profileCrowns = GameEngine.Instance.World.ProfileCrowns;
            int crownCost = offer.Offer.CrownCost;
            int num3 = (int) Math.Floor((decimal) (profileCrowns / crownCost));
            this.numMultiple = new NumericUpDown();
            base.Controls.Add(this.numMultiple);
            this.numMultiple.Minimum = 1M;
            this.numMultiple.Maximum = num3;
            this.numMultiple.Increment = 1M;
            this.numMultiple.Left = (base.Width / 2) - (this.numMultiple.Width / 2);
            this.numMultiple.Top = (base.Height / 2) - (this.numMultiple.Height / 2);
            this.numMultiple.DecimalPlaces = 0;
            this.numMultiple.KeyUp += new KeyEventHandler(this.numMultiple_KeyUp);
            this.confirmLabel.Text = SK.Text("ConfirmBuyOffer_PleaseConfirm", "Please Confirm how many of this type of card pack you want to buy.");
            this.confirmLabel.Color = ARGBColors.Black;
            this.confirmLabel.Position = new Point(20, 10);
            this.confirmLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.confirmLabel.Size = new Size(this.background.Width - 40, 80);
            this.confirmLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.background.addControl(this.confirmLabel);
            this.packTypeLabel.Text = offer.nameLabel.Text;
            this.packTypeLabel.Color = ARGBColors.Black;
            this.packTypeLabel.Position = new Point(20, 100);
            this.packTypeLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.packTypeLabel.Size = new Size(this.background.Width - 40, 80);
            this.packTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.background.addControl(this.packTypeLabel);
            this.confirmButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.confirmButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
            this.confirmButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
            this.confirmButton.Position = new Point(230, 190);
            this.confirmButton.Text.Text = SK.Text("ConfirmBuyOffer_BuyOffer", "Buy Offer");
            this.confirmButton.TextYOffset = -2;
            this.confirmButton.Text.Color = ARGBColors.Black;
            if (Program.mySettings.LanguageIdent == "pl")
            {
                this.confirmButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            }
            else
            {
                this.confirmButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            }
            this.confirmButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playCard), "ConfirmBuyOfferPanel_confirm_buy_pack");
            this.background.addControl(this.confirmButton);
            this.cancelButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.cancelButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
            this.cancelButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
            this.cancelButton.Position = new Point(30, 190);
            this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.cancelButton.TextYOffset = -2;
            this.cancelButton.Text.Color = ARGBColors.Black;
            this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ConfirmBuyOfferPanel_cancel");
            this.background.addControl(this.cancelButton);
            this.confirmCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.confirmCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.confirmCheck.Position = new Point(20, 240);
            this.confirmCheck.Checked = true;
            this.confirmCheck.CBLabel.Text = SK.Text("ConfirmBuyOffer_AlwaysAsk", "Always ask to buy multiple card packs.");
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
                Program.mySettings.BuyMultipleCardPacks = false;
                Program.mySettings.Save();
            }
            InterfaceMgr.Instance.BuyOfferMultiple = (int) this.numMultiple.Value;
            InterfaceMgr.Instance.closeConfirmBuyOfferPopup();
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

