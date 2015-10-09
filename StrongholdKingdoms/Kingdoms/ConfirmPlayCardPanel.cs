namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ConfirmPlayCardPanel : CustomSelfDrawPanel
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
        private CustomSelfDrawPanel.CSDImage topLeftImage = new CustomSelfDrawPanel.CSDImage();

        public ConfirmPlayCardPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            if (!this.confirmCheck.Checked)
            {
                Program.mySettings.ConfirmPlayCard = false;
                Program.mySettings.Save();
            }
            InterfaceMgr.Instance.closeConfirmPlayCardPopup();
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

        public void init(CardTypes.CardDefinition def, CardClickPlayDelegate callback)
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
            CustomSelfDrawPanel.UICard control = BuyCardsPanel.makeUICard(def, RemoteServices.Instance.UserID, 0x2710);
            GFXLibrary.Instance.closeBigCardsLoader();
            control.Position = new Point(0x75, 50);
            this.background.addControl(control);
            this.confirmLabel.Text = SK.Text("ConfirmPlayCardPanel_Are_You_Sure", "Are you sure you want to play this card?");
            this.confirmLabel.Color = ARGBColors.Black;
            this.confirmLabel.Position = new Point(0, 10);
            this.confirmLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.confirmLabel.Size = new Size(this.background.Width, 50);
            this.confirmLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.background.addControl(this.confirmLabel);
            this.confirmButton.Text.Text = SK.Text("ConfirmPlayCardPanel_Play_Card", "Play Card");
            this.confirmButton.TextYOffset = -2;
            this.confirmButton.Text.Color = ARGBColors.Black;
            this.confirmButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.confirmButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.confirmButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
            this.confirmButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
            this.confirmButton.Position = new Point(230, 310);
            this.confirmButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playCard), "ConfirmPlayCardPanel_confirm_play_card");
            this.background.addControl(this.confirmButton);
            this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.cancelButton.TextYOffset = -2;
            this.cancelButton.Text.Color = ARGBColors.Black;
            this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.cancelButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.cancelButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
            this.cancelButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
            this.cancelButton.Position = new Point(30, 310);
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ConfirmPlayCardPanel_cancel");
            this.background.addControl(this.cancelButton);
            this.confirmCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.confirmCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.confirmCheck.Position = new Point(20, 360);
            this.confirmCheck.Checked = true;
            this.confirmCheck.CBLabel.Text = SK.Text("ConfirmPlayCardPanel_Always", "Always confirm playing cards?");
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

        private void playCard()
        {
            if (!this.confirmCheck.Checked)
            {
                Program.mySettings.ConfirmPlayCard = false;
                Program.mySettings.Save();
            }
            InterfaceMgr.Instance.closeConfirmPlayCardPopup();
            Form form = InterfaceMgr.Instance.getCardWindow();
            if (form != null)
            {
                form.TopMost = true;
                form.TopMost = false;
            }
            if (this.m_callback != null)
            {
                this.m_callback(false, false);
            }
        }

        public void update()
        {
        }

        public delegate void CardClickPlayDelegate(bool fromClick, bool fromValidate);
    }
}

