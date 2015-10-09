namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ViewAllCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
        private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
        private int AvailablePanelWidth;
        private static int BorderPadding = 0x10;
        private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.UICardsButtons cardButtons;
        private CardBarGDI cardsInPlay = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private int ContentWidth;
        private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
        private int currentCardSection = -1;
        private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel labelBottom = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
        private string strCrowns = SK.Text("BuyCrownsPanel_Crowns", "Crowns");
        private string strOrderNow = SK.Text("BuyCrownsPanel_Order_Now", "Order Now");

        public ViewAllCardsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closePlayCardsWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int cardSection)
        {
            CustomSelfDrawPanel.CSDImage image2;
            this.currentCardSection = cardSection;
            base.clearControls();
            this.mainBackgroundImage.Image = GFXLibrary.dummy;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.ContentWidth = base.Width - (2 * BorderPadding);
            this.AvailablePanelWidth = 800;
            CustomSelfDrawPanel.CSDExtendingPanel control = new CustomSelfDrawPanel.CSDExtendingPanel {
                Size = base.Size,
                Position = new Point(0, 0)
            };
            this.mainBackgroundImage.addControl(control);
            control.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
            CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left,
                Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size,
                Position = new Point(0, 0)
            };
            control.addControl(image);
            image2 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right,
                Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size,
                Position = new Point((control.Width - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Width) - 6, (control.Height - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Height) - 6)
            };
            control.addControl(image2);
            this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
            this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 550);
            this.AvailablePanel.Position = new Point(8, (base.Height - 8) - 550);
            this.AvailablePanel.Alpha = 0.8f;
            this.mainBackgroundImage.addControl(this.AvailablePanel);
            this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
            this.cardsInPlay.init(cardSection, 0x70, false, 14, 3, 0);
            this.cardsInPlay.Position = new Point(0, 5);
            this.AvailablePanel.addControl(this.cardsInPlay);
            int width = base.Width;
            int borderPadding = BorderPadding;
            int num3 = this.AvailablePanel.Width;
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            this.greyout.FillColor = Color.FromArgb(0xd7, 0x19, 0x19, 0x19);
            this.greyout.Size = new Size(this.mainBackgroundImage.Width, this.AvailablePanel.Y + this.AvailablePanel.Height);
            this.greyout.Position = new Point(0, 0);
            this.greyout.setClickDelegate(delegate {
            });
            CustomSelfDrawPanel.CSDImage closeGrey = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_button_close_normal,
                Size = this.closeImage.Image.Size
            };
            closeGrey.setMouseOverDelegate(() => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            closeGrey.Position = new Point((base.Width - 14) - 0x11, 10);
            this.greyout.addControl(closeGrey);
            this.labelTitle.Position = new Point(0x1b, 8);
            this.labelTitle.Size = new Size(0x3a7, 0x40);
            this.labelTitle.Text = SK.Text("ViewAllCardsPanel_Cards_In_Play", "Cards In Play");
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            CustomSelfDrawPanel.UICardsButtons buttons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) base.ParentForm) {
                Position = new Point(0x328, 0x25)
            };
            this.mainBackgroundImage.addControl(buttons);
            this.cardButtons = buttons;
            if (cardSection != 0)
            {
                CustomSelfDrawPanel.CSDButton button = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.button_cards_all_normal,
                    ImageOver = (Image) GFXLibrary.button_cards_all_over,
                    ImageClick = (Image) GFXLibrary.button_cards_all_over,
                    Position = new Point(750, 0)
                };
                button.Text.Text = SK.Text("PlayCardsPanel_All_Your_Cards", "All Your Cards");
                button.TextYOffset = -3;
                button.Text.Color = ARGBColors.Black;
                button.Text.Size = new Size(button.Size.Width - 0x2d, button.Size.Height);
                button.Text.Position = new Point(0x2d, 0);
                button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showAllCardsClick), "PlayCardsPanel_show_all_cards");
                this.mainBackgroundImage.addControl(button);
            }
            CustomSelfDrawPanel.CSDButton button2 = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal,
                ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over,
                ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed,
                Position = new Point(580, 0x203)
            };
            button2.Text.Text = SK.Text("PlayCardsPanel_Return", "Back To Play Cards");
            button2.TextYOffset = -2;
            button2.Text.Color = ARGBColors.Black;
            if ((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "tr"))
            {
                button2.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            }
            else
            {
                button2.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            }
            button2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnClicked), "PlayCardsPanel_Back_To_PlayCards");
            this.AvailablePanel.addControl(button2);
            CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0x1b, 0x233),
                Size = new Size(0x3a7, 0x40),
                Text = SK.Text("ViewAllCardsPanel_Cancel", "Click on a Card Circle to cancel that card."),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.mainBackgroundImage.addControl(label);
            this.mainBackgroundImage.invalidate();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void returnClicked()
        {
            ((PlayCardsWindow) base.ParentForm).SwitchPanel(1);
        }

        private void showAllCardsClick()
        {
            ((PlayCardsWindow) base.ParentForm).SetCardSection(0);
            this.init(0);
            base.Invalidate();
        }

        public void update()
        {
            this.cardsInPlay.update();
        }
    }
}

