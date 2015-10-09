namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using Stronghold.AuthClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PremiumCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
        private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
        private int AvailablePanelWidth;
        private static int BorderPadding = 0x10;
        private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
        private bool buying;
        private MyMessageBoxPopUp buyTokenPopUp;
        private int buytype;
        private CustomSelfDrawPanel.UICardsButtons cardsButtons;
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private int ContentWidth;
        private int crowns;
        private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
        private int currentCardSection = -1;
        private double currentExpirySeconds;
        private CustomSelfDrawPanel.CSDHorzProgressBar ExpiryBar;
        private int expiryBarCurrent;
        private static int expiryBarMax = 200;
        private double expiryDays;
        private double expiryHours;
        private CustomSelfDrawPanel.CSDLabel expiryLabel;
        private double expiryMinutes;
        private MyMessageBoxPopUp extendPremiumPopUp;
        private Bitmap greenbar = new Bitmap(0x1d, 3);
        private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
        private Size InnerBarDimentions = new Size(0xc4, 12);
        private CustomSelfDrawPanel.CSDImage InplayPanelContent = new CustomSelfDrawPanel.CSDImage();
        private bool inSend;
        private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private int lastminute;
        private CardTypes.PremiumToken lastToken;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
        private double maxExpirySeconds = 604800.0;
        private Size OuterBardimentions = new Size(200, 0x10);
        private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
        private MyMessageBoxPopUp playPremiumPopup;
        private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
        private bool premiumInPlay;
        private CustomSelfDrawPanel.CSDImage PremiumInplayImage = new CustomSelfDrawPanel.CSDImage();
        private Image premiumTokenImage;
        private List<CustomSelfDrawPanel.CSDImage> PremiumTokens = new List<CustomSelfDrawPanel.CSDImage>();
        private CustomSelfDrawPanel.CSDLabel PremiumTokensLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarInplay = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDFill TimerInner;
        private CustomSelfDrawPanel.CSDFill TimerOuter;
        private List<CustomSelfDrawPanel.UICard> UICardList = new List<CustomSelfDrawPanel.UICard>();
        private List<CustomSelfDrawPanel.UICard> UICardListInplay = new List<CustomSelfDrawPanel.UICard>();

        public PremiumCardsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void AvailableContentScroll()
        {
            int y = this.scrollbarAvailable.Value;
            this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, BorderPadding - y);
            this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
            this.AvailablePanelContent.invalidate();
            this.AvailablePanel.invalidate();
        }

        private void BoughtOffer(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode != 1)
            {
                MyMessageBox.Show(response.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
                WorldMap world = GameEngine.Instance.World;
                world.ProfileCrowns += this.crowns;
                this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
            }
            else
            {
                int result = 0;
                int.TryParse(response.Strings, out result);
                CardTypes.PremiumToken token = new CardTypes.PremiumToken {
                    Reward = 0,
                    Type = this.buytype,
                    UserPremiumTokenID = result,
                    WorldID = RemoteServices.Instance.ProfileWorldID
                };
                GameEngine.Instance.World.ProfilePremiumTokens.Add(result, token);
                this.UpdatePremiumTokens();
            }
            this.buying = false;
        }

        private void BoughtTokenPopUp()
        {
            try
            {
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""));
                if (base.ClickedControl.Data == 0x1010)
                {
                    req.PackID = "2";
                }
                else if (base.ClickedControl.Data == 0x1012)
                {
                    req.PackID = "6";
                }
                provider.buyPremium(req, new CardsEndResponseDelegate(this.BoughtOffer), this);
                WorldMap world = GameEngine.Instance.World;
                world.ProfileCrowns -= this.crowns;
                this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
            }
            catch (Exception exception)
            {
                UniversalDebugLog.Log(exception.ToString());
            }
            this.buyTokenPopUp.Close();
        }

        private void ClickedOffer()
        {
            int num = 30;
            if (base.ClickedControl.Data == 0x1010)
            {
                num = 30;
            }
            else if (base.ClickedControl.Data == 0x1012)
            {
                num = 100;
            }
            if (GameEngine.Instance.World.ProfileCrowns < num)
            {
                BuyCrownsPopup popup = new BuyCrownsPopup();
                popup.init(num - GameEngine.Instance.World.ProfileCrowns, base.ParentForm);
                popup.Show(base.ParentForm);
            }
            else if (!this.buying)
            {
                this.buying = true;
                string txtMessage = "";
                if (base.ClickedControl.Data == 0x1010)
                {
                    this.crowns = 30;
                    txtMessage = SK.Text("PremiumCardsPanel_7Day_Premium", "Buy one 7-Day Premium Token for 30 Crowns?  To activate the Premium Token you must click on it to set it into play on the game world.") + Environment.NewLine;
                }
                else if (base.ClickedControl.Data == 0x1012)
                {
                    this.crowns = 100;
                    txtMessage = SK.Text("PremiumCardsPanel_30Day_Premium", "Buy one 30-Day Premium Token for 100 Crowns?  To activate the Premium Token you must click on it to set it into play on the game world.") + Environment.NewLine;
                }
                this.buytype = base.ClickedControl.Data;
                if (MyMessageBox.Show(txtMessage, SK.Text("BuyCardsPanel_Confirm_Purchase", "Confirm Purchase"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                    XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""));
                    if (base.ClickedControl.Data == 0x1010)
                    {
                        req.PackID = "2";
                    }
                    else if (base.ClickedControl.Data == 0x1012)
                    {
                        req.PackID = "6";
                    }
                    provider.buyPremium(req, new CardsEndResponseDelegate(this.BoughtOffer), this);
                    WorldMap world = GameEngine.Instance.World;
                    world.ProfileCrowns -= this.crowns;
                    this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
                }
                else
                {
                    this.buying = false;
                }
            }
        }

        private void ClickedToken()
        {
            if (!this.inSend)
            {
                int data = base.ClickedControl.Data;
                int type = GameEngine.Instance.World.ProfilePremiumTokens[data].Type;
                DateTime time = VillageMap.getCurrentServerTime();
                if (this.premiumInPlay)
                {
                    this.currentExpirySeconds = GameEngine.Instance.World.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
                    time = time.AddSeconds(this.currentExpirySeconds);
                    if ((GameEngine.Instance.World.ProfilePremiumTokens[data].Type == 0x1011) && (GameEngine.Instance.World.UserCardData.premiumCard == 0x1011))
                    {
                        MyMessageBox.Show(SK.Text("PremiumCardsPanel_Already_In_Play_2_2", "You cannot extend a 2 day Premium Token using another 2 day Premium Token."), SK.Text("GENERIC_Error", "Error"));
                        return;
                    }
                    if (MyMessageBox.Show(SK.Text("PremiumCardsPanel_ExtendToken", "You currently have a Premium Token in play, do you wish to extend this by playing another Token?"), SK.Text("PremiumCardsPanel_ExtendWarning", "Extend Premium Token"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                    type = 0x1014;
                }
                else
                {
                    string str = "";
                    switch (GameEngine.Instance.World.ProfilePremiumTokens[data].Type)
                    {
                        case 0x1010:
                            str = SK.Text("PremiumCardsPanel_7day", "7 Day Premium Token");
                            break;

                        case 0x1011:
                            str = SK.Text("TOOLTIPS_QUEST_REWARD_PREMIUM_CARD", "2 Day Premium Token");
                            break;

                        case 0x1012:
                            str = SK.Text("PremiumCardsPanel_30day", "30 Day Premium Token");
                            break;
                    }
                    if (MyMessageBox.Show(str + Environment.NewLine + Environment.NewLine + SK.Text("PremiumCardsPanel_PlayToken", "You are about to play this Premium Token. This Premium Token will only affect the current game world.") + Environment.NewLine + Environment.NewLine + SK.Text("PremiumCardsPanel_PlayToken2", "Are you sure you wish to play this Token?"), SK.Text("PremiumCardsPanel_PlayToken_Header", "Play Premium Token"), MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                this.lastToken = GameEngine.Instance.World.ProfilePremiumTokens[data];
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")) {
                    WorldID = RemoteServices.Instance.ProfileWorldID.ToString(),
                    UserCardID = data.ToString()
                };
                if (this.lastToken.Type == 0x1010)
                {
                    req.CardString = "CARDTYPE_PREMIUM";
                }
                if (this.lastToken.Type == 0x1011)
                {
                    req.CardString = "CARDTYPE_PREMIUM2";
                }
                if (this.lastToken.Type == 0x1012)
                {
                    req.CardString = "CARDTYPE_PREMIUM30";
                }
                if (InterfaceMgr.Instance.getCardWindow() != null)
                {
                    CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
                }
                this.inSend = true;
                provider.playPremium(req, new CardsEndResponseDelegate(this.PlayedToken), this);
                this.premiumInPlay = true;
                this.currentExpirySeconds = 604800.0;
                if (GameEngine.Instance.World.ProfilePremiumTokens[data].Type == 0x1010)
                {
                    GameEngine.Instance.World.UserCardData.premiumCardExpiry = time.AddDays(7.0);
                }
                if (GameEngine.Instance.World.ProfilePremiumTokens[data].Type == 0x1012)
                {
                    GameEngine.Instance.World.UserCardData.premiumCardExpiry = time.AddDays(30.0);
                }
                if (GameEngine.Instance.World.ProfilePremiumTokens[data].Type == 0x1011)
                {
                    GameEngine.Instance.World.UserCardData.premiumCardExpiry = time.AddDays(2.0);
                }
                GameEngine.Instance.World.UserCardData.premiumCard = type;
                GameEngine.Instance.World.ProfilePremiumTokens.Remove(data);
                this.UpdatePremiumTokens();
                this.UpdateExpiry();
            }
        }

        private void CloseBuyTokenPopUp()
        {
            if (this.buyTokenPopUp != null)
            {
                if (this.buyTokenPopUp.Created)
                {
                    this.buyTokenPopUp.Close();
                }
                this.buyTokenPopUp = null;
            }
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closePlayCardsWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        private void CloseExtendPremiumPopUp()
        {
            if (this.extendPremiumPopUp != null)
            {
                if (this.extendPremiumPopUp.Created)
                {
                    this.extendPremiumPopUp.Close();
                }
                this.extendPremiumPopUp = null;
            }
        }

        private void ClosePlayPremiumPopUp()
        {
            if (this.playPremiumPopup != null)
            {
                if (this.playPremiumPopup.Created)
                {
                    this.playPremiumPopup.Close();
                }
                this.playPremiumPopup = null;
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

        private void ExtendOrPlayPremiumToken()
        {
            try
            {
                int data = base.ClickedControl.Data;
                int num2 = 0x1014;
                DateTime time = VillageMap.getCurrentServerTime();
                this.currentExpirySeconds = GameEngine.Instance.World.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
                time = time.AddSeconds(this.currentExpirySeconds);
                this.lastToken = GameEngine.Instance.World.ProfilePremiumTokens[data];
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")) {
                    WorldID = RemoteServices.Instance.ProfileWorldID.ToString(),
                    UserCardID = data.ToString()
                };
                if (this.lastToken.Type == 0x1010)
                {
                    req.CardString = "CARDTYPE_PREMIUM";
                }
                if (this.lastToken.Type == 0x1011)
                {
                    req.CardString = "CARDTYPE_PREMIUM2";
                }
                if (this.lastToken.Type == 0x1012)
                {
                    req.CardString = "CARDTYPE_PREMIUM30";
                }
                if (InterfaceMgr.Instance.getCardWindow() != null)
                {
                    CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
                }
                this.inSend = true;
                provider.playPremium(req, new CardsEndResponseDelegate(this.PlayedToken), this);
                this.premiumInPlay = true;
                this.currentExpirySeconds = 604800.0;
                if (GameEngine.Instance.World.ProfilePremiumTokens[data].Type == 0x1010)
                {
                    GameEngine.Instance.World.UserCardData.premiumCardExpiry = time.AddDays(7.0);
                }
                if (GameEngine.Instance.World.ProfilePremiumTokens[data].Type == 0x1012)
                {
                    GameEngine.Instance.World.UserCardData.premiumCardExpiry = time.AddDays(30.0);
                }
                if (GameEngine.Instance.World.ProfilePremiumTokens[data].Type == 0x1011)
                {
                    GameEngine.Instance.World.UserCardData.premiumCardExpiry = time.AddDays(2.0);
                }
                GameEngine.Instance.World.UserCardData.premiumCard = num2;
                GameEngine.Instance.World.ProfilePremiumTokens.Remove(data);
                this.UpdatePremiumTokens();
                this.UpdateExpiry();
            }
            catch (Exception exception)
            {
                UniversalDebugLog.Log(exception.ToString());
            }
        }

        private void ExtendPremiumToken()
        {
            this.ExtendOrPlayPremiumToken();
            this.extendPremiumPopUp.Close();
        }

        public void init(int cardSection)
        {
            CustomSelfDrawPanel.CSDImage image2;
            this.inSend = false;
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
            this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 0x181);
            this.AvailablePanel.Position = new Point(8, (base.Height - 8) - 0x181);
            this.AvailablePanel.Alpha = 0.8f;
            this.mainBackgroundImage.addControl(this.AvailablePanel);
            this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
            int width = base.Width;
            int borderPadding = BorderPadding;
            int num8 = this.AvailablePanel.Width;
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.closeImage.CustomTooltipID = 0x2774;
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x25, new Point((((base.Width - 1) - 0x11) - 50) + 3, 5), true);
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            this.greyout.FillColor = Color.FromArgb(0xd7, 0x19, 0x19, 0x19);
            this.greyout.Size = new Size(this.mainBackgroundImage.Width, this.mainBackgroundImage.Height);
            this.greyout.Position = new Point(0, 0);
            this.cardsButtons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) base.ParentForm);
            this.cardsButtons.Position = new Point(0x328, 0x25);
            this.mainBackgroundImage.addControl(this.cardsButtons);
            this.labelTitle.Position = new Point(0x1b, 8);
            this.labelTitle.Size = new Size(0x3a7, 0x40);
            this.labelTitle.Text = SK.Text("PremiumCardsPanel_Buy_and_Open_Packs", "Buy and Play Premium Tokens: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            this.premiumInPlay = GameEngine.Instance.World.UserCardData.premiumCard > 0;
            if (GameEngine.Instance.World.UserCardData.premiumCard == 0x1010)
            {
                this.maxExpirySeconds = 604800.0;
            }
            else if (GameEngine.Instance.World.UserCardData.premiumCard == 0x1012)
            {
                this.maxExpirySeconds = 2592000.0;
            }
            else if (GameEngine.Instance.World.UserCardData.premiumCard == 0x1011)
            {
                this.maxExpirySeconds = 172800.0;
            }
            else if (GameEngine.Instance.World.UserCardData.premiumCard == 0x1014)
            {
                this.maxExpirySeconds = 0.0;
            }
            this.currentExpirySeconds = GameEngine.Instance.World.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
            this.expiryDays = this.currentExpirySeconds / 86400.0;
            this.expiryHours = (this.currentExpirySeconds % 86400.0) / 3600.0;
            this.expiryMinutes = (this.currentExpirySeconds % 3600.0) / 60.0;
            if (this.maxExpirySeconds > 0.0)
            {
                double num = this.currentExpirySeconds / this.maxExpirySeconds;
                this.expiryBarCurrent = Convert.ToInt32(Math.Floor((double) (num * expiryBarMax)));
            }
            else
            {
                this.expiryBarCurrent = -1;
            }
            if (GameEngine.Instance.World.UserCardData.premiumCard > 0)
            {
                this.premiumTokenImage = GFXLibrary.PremiumTokens[GameEngine.Instance.World.UserCardData.premiumCard][0];
            }
            else
            {
                this.premiumTokenImage = GFXLibrary.PremiumTokens[0x1010][0];
            }
            this.PremiumInplayImage.Visible = false;
            this.PremiumInplayImage.Image = this.premiumTokenImage;
            this.PremiumInplayImage.Size = this.premiumTokenImage.Size;
            this.PremiumInplayImage.Position = new Point(((this.AvailablePanel.X + this.AvailablePanel.Width) - 0x20) - this.PremiumInplayImage.Width, this.cardsButtons.Y + 8);
            this.PremiumInplayImage.setMouseOverDelegate(delegate {
                if (GameEngine.Instance.World.UserCardData.premiumCard > 0)
                {
                    this.PremiumInplayImage.Image = GFXLibrary.PremiumTokens[GameEngine.Instance.World.UserCardData.premiumCard][1];
                }
            }, delegate {
                if (GameEngine.Instance.World.UserCardData.premiumCard > 0)
                {
                    this.PremiumInplayImage.Image = GFXLibrary.PremiumTokens[GameEngine.Instance.World.UserCardData.premiumCard][0];
                }
            });
            this.mainBackgroundImage.addControl(this.PremiumInplayImage);
            if (this.expiryBarCurrent >= 0)
            {
                this.TimerOuter = new CustomSelfDrawPanel.CSDFill();
                this.TimerInner = new CustomSelfDrawPanel.CSDFill();
            }
            else
            {
                this.TimerInner = null;
                this.TimerOuter = null;
            }
            this.PremiumTokensLabel = new CustomSelfDrawPanel.CSDLabel();
            this.PremiumTokensLabel.Position = new Point(this.AvailablePanel.X + 0x20, this.AvailablePanel.Y - 0x18);
            this.PremiumTokensLabel.Size = new Size(450, 0x20);
            this.PremiumTokensLabel.Text = SK.Text("PremiumCardsPanel_Current_Tokens", "Current Premium Tokens") + " : " + GameEngine.Instance.World.ProfilePremiumTokens.Count.ToString() + ((GameEngine.Instance.World.ProfilePremiumTokens.Count > 0) ? (" (" + SK.Text("PremiumCardsPanel_Click_To_Play", "click one to play") + ")") : "");
            this.PremiumTokensLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.PremiumTokensLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.PremiumTokensLabel.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.PremiumTokensLabel);
            CustomSelfDrawPanel.CSDImage image3 = new CustomSelfDrawPanel.CSDImage();
            BaseImage image4 = GFXLibrary.cardpanel_premium_ad;
            image3.Image = (Image) image4;
            image3.Size = image4.Size;
            image3.Position = new Point(0, 0);
            CustomSelfDrawPanel.CSDImage PremiumAdvert7 = new CustomSelfDrawPanel.CSDImage();
            CustomSelfDrawPanel.CSDImage PremiumAdvert30 = new CustomSelfDrawPanel.CSDImage();
            BaseImage AdImage7 = GFXLibrary.premiumAdvert7;
            BaseImage AdImage7_over = GFXLibrary.premiumAdvert7_over;
            BaseImage AdImage30 = GFXLibrary.premiumAdvert30;
            BaseImage AdImage30_over = GFXLibrary.premiumAdvert30_over;
            PremiumAdvert7.Image = (Image) AdImage7;
            PremiumAdvert7.Size = AdImage7.Size;
            PremiumAdvert7.Position = new Point(0, 0);
            this.AvailablePanelContent.addControl(PremiumAdvert7);
            PremiumAdvert30.Image = (Image) AdImage30;
            PremiumAdvert30.Size = AdImage30.Size;
            PremiumAdvert30.Position = new Point(0x16b, 0);
            this.AvailablePanelContent.addControl(PremiumAdvert30);
            PremiumAdvert7.Data = 0x1010;
            PremiumAdvert30.Data = 0x1012;
            PremiumAdvert7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedOffer), "PremiumCardsPanel_buy_premium");
            PremiumAdvert30.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedOffer), "PremiumCardsPanel_buy_premium");
            PremiumAdvert7.setMouseOverDelegate(() => PremiumAdvert7.Image = (Image) AdImage7_over, () => PremiumAdvert7.Image = (Image) AdImage7);
            PremiumAdvert30.setMouseOverDelegate(() => PremiumAdvert30.Image = (Image) AdImage30_over, () => PremiumAdvert30.Image = (Image) AdImage30);
            image3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedOffer), "PremiumCardsPanel_buy_premium");
            image3.Data = 0x1010;
            CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, image3.Height + 8),
                Size = new Size(600, 0x20),
                Text = SK.Text("PremiumCardsPanel_Benefits", "Premium Benefits"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold),
                Color = ARGBColors.Gold
            };
            this.AvailablePanelContent.addControl(label);
            CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, ((image3.Height + 8) + label.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Building_Queue", "Building Queue"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label2);
            CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label2.Y + label2.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Buildings_Queue_Info", "This allows up to 5 buildings to be queued for construction in the village. You can also move all buildings within your village screen."),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label3);
            CustomSelfDrawPanel.CSDImage image5 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[0],
                Position = new Point(4, ((label2.Y + label2.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image5);
            CustomSelfDrawPanel.CSDLabel label4 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label3.Y + label3.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Research_Queue", "Research Queue"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label4);
            CustomSelfDrawPanel.CSDLabel label5 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label4.Y + label4.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Research_Queue_Info", "This allows up to 5 researches to be queued in the research screen."),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label5);
            CustomSelfDrawPanel.CSDImage image6 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[1],
                Position = new Point(4, ((label4.Y + label4.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image6);
            CustomSelfDrawPanel.CSDLabel label6 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label5.Y + label5.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Auto_Trading", "Auto Trading"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label6);
            CustomSelfDrawPanel.CSDLabel label7 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label6.Y + label6.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Auto_Trading_Info", "This allows the trade one type of good to the parish capitals market automatically while you are logged out.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")",
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label7);
            CustomSelfDrawPanel.CSDImage image7 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[2],
                Position = new Point(4, ((label6.Y + label6.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image7);
            CustomSelfDrawPanel.CSDLabel label8 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label7.Y + label7.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Auto_Scouting", "Auto Scouting"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label8);
            CustomSelfDrawPanel.CSDLabel label9 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label8.Y + label8.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Auto_Scouting_Info", "This will send out all available scouts to stashes within the parish the village is located, automatically while you are logged out.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")",
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label9);
            CustomSelfDrawPanel.CSDImage image8 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[3],
                Position = new Point(4, ((label8.Y + label8.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image8);
            CustomSelfDrawPanel.CSDLabel label10 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label9.Y + label9.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Auto_Attacking", "Auto Attacking"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label10);
            CustomSelfDrawPanel.CSDLabel label11 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label10.Y + label10.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Auto_Attacking_Info", "This will send out attacks to chosen targets automatically while you are logged out.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")",
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label11);
            CustomSelfDrawPanel.CSDImage image9 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[4],
                Position = new Point(4, ((label10.Y + label10.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image9);
            CustomSelfDrawPanel.CSDLabel label12 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label11.Y + label11.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Auto_Recruit", "Auto Recruit"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label12);
            CustomSelfDrawPanel.CSDLabel label13 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label12.Y + label12.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Auto_Recruit_Info", "This will automatically conscript idle peasants to your army.") + " (" + SK.Text("PremiumCardsPanel_Auto_Extra", "Activates once every 2 to 4 hours.") + ")",
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label13);
            CustomSelfDrawPanel.CSDImage image10 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[5],
                Position = new Point(4, ((label12.Y + label12.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image10);
            CustomSelfDrawPanel.CSDLabel label14 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label13.Y + label13.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Village_Overview", "Village Overview"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label14);
            CustomSelfDrawPanel.CSDLabel label15 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label14.Y + label14.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Village_Overview_Info", "This allows players to keep track of essential information on all their villages, such as income from taxes, housing capacity, popularity and more."),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label15);
            CustomSelfDrawPanel.CSDImage image11 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[6],
                Position = new Point(4, ((label14.Y + label14.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image11);
            CustomSelfDrawPanel.CSDLabel label16 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label15.Y + label15.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_Vacation_Mode", "Vacation Mode"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label16);
            CustomSelfDrawPanel.CSDLabel label17 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label16.Y + label16.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_Vacation_Mode_Info", "This allows players to protect their villages from attack for up to 15 days."),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label17);
            CustomSelfDrawPanel.CSDImage image12 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[7],
                Position = new Point(4, ((label16.Y + label16.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image12);
            CustomSelfDrawPanel.CSDLabel label18 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, (label17.Y + label17.Height) + 4),
                Size = new Size(600, 30),
                Text = SK.Text("PremiumCardsPanel_AdvancedTrading", "Advanced Trading Option"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Color = ARGBColors.Goldenrod
            };
            this.AvailablePanelContent.addControl(label18);
            CustomSelfDrawPanel.CSDLabel label19 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(110, ((label18.Y + label18.Height) + 4) - 11),
                Size = new Size(590, 50),
                Text = SK.Text("PremiumCardsPanel_AdvancedTrading_Info", "This allows players to find the best prices for goods in nearby Markets."),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.White
            };
            this.AvailablePanelContent.addControl(label19);
            CustomSelfDrawPanel.CSDImage image13 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.premiumIcons[8],
                Position = new Point(4, ((label18.Y + label18.Height) + 4) - 10)
            };
            this.AvailablePanelContent.addControl(image13);
            int height = (label19.Y + label19.Height) + 6;
            this.AvailablePanelContent.Position = new Point(BorderPadding, BorderPadding);
            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
            this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - BorderPadding, this.AvailablePanel.Height - (BorderPadding * 2));
            this.AvailablePanel.addControl(this.AvailablePanelContent);
            if (height < this.AvailablePanelContent.ClipRect.Height)
            {
                height = this.AvailablePanelContent.ClipRect.Height;
            }
            this.scrollbarAvailable.Position = new Point((this.AvailablePanel.Width - BorderPadding) - (BorderPadding / 2), this.AvailablePanel.Y + (BorderPadding / 2));
            this.scrollbarAvailable.Size = new Size(BorderPadding, this.AvailablePanel.Height - BorderPadding);
            this.mainBackgroundImage.addControl(this.scrollbarAvailable);
            this.scrollbarAvailable.Value = 0;
            this.scrollbarAvailable.StepSize = 200;
            this.scrollbarAvailable.Max = this.AvailablePanelContent.Height - this.AvailablePanelContent.ClipRect.Height;
            this.scrollbarAvailable.NumVisibleLines = this.AvailablePanelContent.ClipRect.Height;
            this.scrollbarAvailable.OffsetTL = new Point(1, 5);
            this.scrollbarAvailable.OffsetBR = new Point(0, -10);
            this.scrollbarAvailable.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.AvailableContentScroll));
            this.scrollbarAvailable.Create(null, null, null, (Image) GFXLibrary.cardpanel_scroll_thumb_top, (Image) GFXLibrary.cardpanel_scroll_thumb_mid, (Image) GFXLibrary.cardpanel_scroll_thumb_botom);
            if (height <= this.AvailablePanelContent.ClipRect.Height)
            {
                this.scrollbarAvailable.Visible = false;
            }
            this.expiryLabel = new CustomSelfDrawPanel.CSDLabel();
            if (this.TimerInner != null)
            {
                this.ExpiryBar = new CustomSelfDrawPanel.CSDHorzProgressBar();
                this.ExpiryBar.Size = new Size(170, 0);
                this.ExpiryBar.Position = new Point(this.PremiumInplayImage.X - 13, this.PremiumInplayImage.Y + this.PremiumInplayImage.Height);
                this.ExpiryBar.Create((Image) GFXLibrary.cardpanel_prem_timer_back_left, (Image) GFXLibrary.cardpanel_prem_timer_back_mid, (Image) GFXLibrary.cardpanel_prem_timer_back_right, (Image) GFXLibrary.cardpanel_prem_timer_fill_left, (Image) GFXLibrary.cardpanel_prem_timer_fill_mid, (Image) GFXLibrary.cardpanel_prem_timer_fill_right);
                this.ExpiryBar.setValues(this.currentExpirySeconds, this.maxExpirySeconds);
                this.mainBackgroundImage.addControl(this.ExpiryBar);
                this.expiryLabel.Position = new Point(this.ExpiryBar.X, this.ExpiryBar.Y + this.ExpiryBar.Height);
                this.expiryLabel.Size = new Size(this.ExpiryBar.Width, 0x10);
            }
            else
            {
                this.expiryLabel.Position = new Point(this.PremiumInplayImage.X - 13, this.PremiumInplayImage.Y + this.PremiumInplayImage.Height);
                this.expiryLabel.Size = new Size(170, 0x10);
            }
            this.expiryLabel.Visible = false;
            this.expiryLabel.Text = Math.Floor(this.expiryDays).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryHours).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryMinutes).ToString().PadLeft(2, '0') + " (" + SK.Text("PremiumCardsPanel_Day_Hour_Minute", "dd:hh:mm") + ")";
            this.expiryLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.expiryLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.expiryLabel.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.expiryLabel);
            this.UpdatePremiumTokens();
            this.UpdateExpiry();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void PlayedToken(ICardsProvider provider, ICardsResponse response)
        {
            this.inSend = false;
            if (response.SuccessCode != 1)
            {
                MyMessageBox.Show(PlayCardsWindow.translateCardError(response.Message, 0), SK.Text("GENERIC_Error", "Error"));
                GameEngine.Instance.World.ProfilePremiumTokens.Add(this.lastToken.UserPremiumTokenID, this.lastToken);
                this.premiumInPlay = false;
                GameEngine.Instance.World.UserCardData.premiumCard = 0;
                GameEngine.Instance.World.UserCardData.premiumCardExpiry = VillageMap.getCurrentServerTime();
                this.UpdatePremiumTokens();
                this.UpdateExpiry();
                if (InterfaceMgr.Instance.getCardWindow() != null)
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
                }
            }
            else
            {
                GameEngine.Instance.World.CardPlayed(-1, GameEngine.Instance.World.UserCardData.premiumCard, -1);
            }
        }

        private void PlayPremiumToken()
        {
            this.ExtendOrPlayPremiumToken();
            this.playPremiumPopup.Close();
        }

        public void update()
        {
            if (this.lastminute != VillageMap.getCurrentServerTime().Minute)
            {
                this.UpdateExpiry();
            }
        }

        public void UpdateExpiry()
        {
            if ((this.premiumInPlay && (this.currentExpirySeconds > 0.0)) && (GameEngine.Instance.World.UserCardData.premiumCard > 0))
            {
                if (GameEngine.Instance.World.UserCardData.premiumCard == 0x1010)
                {
                    this.maxExpirySeconds = 604800.0;
                }
                else if (GameEngine.Instance.World.UserCardData.premiumCard == 0x1012)
                {
                    this.maxExpirySeconds = 2592000.0;
                }
                else if (GameEngine.Instance.World.UserCardData.premiumCard == 0x1011)
                {
                    this.maxExpirySeconds = 172800.0;
                }
                if (GameEngine.Instance.World.UserCardData.premiumCard > 0)
                {
                    this.PremiumInplayImage.Image = GFXLibrary.PremiumTokens[GameEngine.Instance.World.UserCardData.premiumCard][0];
                }
                else
                {
                    this.PremiumInplayImage.Image = GFXLibrary.PremiumTokens[0x1010][0];
                }
                this.PremiumInplayImage.Visible = true;
                if (this.ExpiryBar != null)
                {
                    this.ExpiryBar.Visible = true;
                }
                this.expiryLabel.Visible = true;
                this.currentExpirySeconds = GameEngine.Instance.World.UserCardData.premiumCardExpiry.Subtract(VillageMap.getCurrentServerTime()).TotalSeconds;
                this.expiryDays = this.currentExpirySeconds / 86400.0;
                this.expiryHours = (this.currentExpirySeconds % 86400.0) / 3600.0;
                this.expiryMinutes = (this.currentExpirySeconds % 3600.0) / 60.0;
                if (this.TimerInner != null)
                {
                    this.ExpiryBar.setValues(this.currentExpirySeconds, this.maxExpirySeconds);
                }
                this.expiryLabel.Text = Math.Floor(this.expiryDays).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryHours).ToString().PadLeft(2, '0') + ":" + Math.Floor(this.expiryMinutes).ToString().PadLeft(2, '0') + " (" + SK.Text("PremiumCardsPanel_Day_Hour_Minute", "dd:hh:mm") + ")";
                this.lastminute = VillageMap.getCurrentServerTime().Minute;
            }
            else
            {
                this.PremiumInplayImage.Visible = false;
                this.expiryLabel.Visible = false;
                if (this.ExpiryBar != null)
                {
                    this.ExpiryBar.Visible = false;
                }
            }
        }

        public void UpdatePremiumTokens()
        {
            foreach (CustomSelfDrawPanel.CSDImage image in this.PremiumTokens)
            {
                this.mainBackgroundImage.removeControl(image);
            }
            this.PremiumTokens.Clear();
            int num = 0x2d;
            int num2 = 0;
            foreach (CardTypes.PremiumToken token in GameEngine.Instance.World.ProfilePremiumTokens.Values)
            {
                if (((token.Type & 0x1010) == 0x1010) && GFXLibrary.PremiumTokens.ContainsKey(token.Type))
                {
                    num2++;
                }
            }
            if (num2 > 0x18)
            {
                num = 15;
            }
            else if (num2 > 0x10)
            {
                num = 20;
            }
            else if (num2 > 11)
            {
                num = 30;
            }
            int num3 = 0;
            foreach (CardTypes.PremiumToken token2 in GameEngine.Instance.World.ProfilePremiumTokens.Values)
            {
                if (((token2.Type & 0x1010) == 0x1010) && GFXLibrary.PremiumTokens.ContainsKey(token2.Type))
                {
                    CustomSelfDrawPanel.CSDImage im = new CustomSelfDrawPanel.CSDImage {
                        Data = token2.UserPremiumTokenID,
                        Position = new Point((this.AvailablePanel.X + 0x20) + (num3 * num), this.cardsButtons.Y + 8),
                        Size = this.premiumTokenImage.Size
                    };
                    BaseImage normalImage = GFXLibrary.PremiumTokens[token2.Type][0];
                    BaseImage overImage = GFXLibrary.PremiumTokens[token2.Type][1];
                    im.Image = (Image) normalImage;
                    im.setMouseOverDelegate(() => im.Image = (Image) overImage, () => im.Image = (Image) normalImage);
                    this.PremiumTokens.Add(im);
                    this.mainBackgroundImage.addControl(im);
                    im.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickedToken), "PremiumCardsPanel_play_token");
                    num3++;
                    if (num3 == 0x20)
                    {
                        break;
                    }
                }
            }
            this.mainBackgroundImage.invalidate();
            this.PremiumTokensLabel.Text = SK.Text("PremiumCardsPanel_Current_Tokens", "Current Premium Tokens") + " : " + GameEngine.Instance.World.ProfilePremiumTokens.Count.ToString() + ((GameEngine.Instance.World.ProfilePremiumTokens.Count > 0) ? (" (" + SK.Text("PremiumCardsPanel_Click_To_Play", "click one to play") + ")") : "");
        }
    }
}

