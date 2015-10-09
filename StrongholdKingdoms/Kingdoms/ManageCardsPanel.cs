namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using StatTracking;
    using Stronghold.AuthClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ManageCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
    {
        private CardTypes.CardDefinition autoCardDef;
        private int autoCardUserID;
        private int autoCardVillageID;
        private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
        private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
        private int AvailablePanelWidth;
        private static int BorderPadding = 0x10;
        private CustomSelfDrawPanel.CSDImage buttonBonus = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage buttonCash = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage buttonCatalog = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDCheckBox buyAndPlayCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
        private bool buyingCard;
        private List<CustomSelfDrawPanel.UICard> CardCatalog = new List<CustomSelfDrawPanel.UICard>();
        private CustomSelfDrawPanel.UICardsButtons cardsButtons;
        private CustomSelfDrawPanel.CSDLabel cardTitle;
        private bool cashingIn;
        private CardTypes.CardDefinition CatalogFilterDefinition = new CardTypes.CardDefinition();
        private CustomSelfDrawPanel.CSDButton clearSearchButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton compressButton = new CustomSelfDrawPanel.CSDButton();
        private bool compressedCards;
        private int ContentWidth;
        private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
        private int currentCardSection = -1;
        private int diamondAnimFrame;
        private DateTime diamondAnimStartTime = DateTime.Now;
        private List<CustomSelfDrawPanel.UICard> dummyCards = new List<CustomSelfDrawPanel.UICard>();
        private CustomSelfDrawPanel.CSDImage DynamicButton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel DynamicButtonLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel DynamicLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage DynamicPanel = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage[] EmptyCards = new CustomSelfDrawPanel.CSDImage[5];
        private CustomSelfDrawPanel.CSDButton expandButton = new CustomSelfDrawPanel.CSDButton();
        private int failedPurchaseCard = -1;
        private int failedPurchaseCost = -1;
        private bool fastCashIn;
        private CustomSelfDrawPanel.CSDCheckBox fastCashInCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
        private List<CustomSelfDrawPanel.CSDButton> FilterButtons = new List<CustomSelfDrawPanel.CSDButton>();
        private List<CustomSelfDrawPanel.CSDFloatingText> floatingLabels = new List<CustomSelfDrawPanel.CSDFloatingText>();
        private Bitmap greenbar = new Bitmap(0x1d, 3);
        private CustomSelfDrawPanel.CSDImage imageTitlePoints = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage InplayPanelContent = new CustomSelfDrawPanel.CSDImage();
        private int InplayPanelWidth;
        private CustomSelfDrawPanel.CSDLabel labelBuyCash = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel LabelClickToRemove = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitlePoints = new CustomSelfDrawPanel.CSDLabel();
        private XmlRpcCardsResponse lastCashResponse;
        private DateTime lastRefresh = DateTime.Now;
        private DateTime lastTickCall = DateTime.Now.AddSeconds(-60.0);
        private DateTime lastUpdatedProgressBars = DateTime.Now.AddSeconds(30.0);
        public int LayoutPanelMode;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
        private const int MAX_CASHIN_CARDS = 60;
        private int newcardcost;
        private CardTypes.CardDefinition newcarddef;
        private string newcardnames = "";
        private int NumCardsCachingIn;
        public static int PANEL_MODE_BUY = 2;
        public static int PANEL_MODE_CASH = 1;
        public int PanelMode;
        private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
        private bool playingSpinSound;
        private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.UICard[] SetCards = new CustomSelfDrawPanel.UICard[60];
        private List<CustomSelfDrawPanel.UICard> ShoppingCart = new List<CustomSelfDrawPanel.UICard>();
        public bool showingbonus;
        private CustomSelfDrawPanel.CSDImageAnim[] SlotAnims = new CustomSelfDrawPanel.CSDImageAnim[5];
        private CustomSelfDrawPanel.CSDImage SlotHolder = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage sortBack = new CustomSelfDrawPanel.CSDImage();
        private int sortByMode = -1;
        private CustomSelfDrawPanel.CSDButton sortByName = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sortByQuantity = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sortByType = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDControl[] SpinnerInners = new CustomSelfDrawPanel.CSDControl[5];
        private CustomSelfDrawPanel.CSDControl[] Spinners = new CustomSelfDrawPanel.CSDControl[5];
        private bool[] spinning = new bool[5];
        private int spinSoundSoundID;
        private DateTime spinSoundStopLastTime = DateTime.MinValue;
        private bool[] spinSoundStopPlayed = new bool[5];
        private int spinspeed;
        private DateTime spinstart;
        private Dictionary<int, int> symbolOffsets = new Dictionary<int, int>();
        private CustomSelfDrawPanel.CSDVertImageScroller[] SymbolScrollers = new CustomSelfDrawPanel.CSDVertImageScroller[5];
        private int[] SymbolTargets = new int[5];
        private CustomSelfDrawPanel.CSDArea TabBuyArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea TabCashArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage TabSelector = new CustomSelfDrawPanel.CSDImage();
        private string TextCartEmpty = SK.Text("ManageCandsPanel_Buy", "Click on cards below to buy them");
        private string TextCartFull = SK.Text("ManageCandsPanel_Confirm,", "Click Here to confirm purchase!");
        private string TextCash = SK.Text("ManageCandsPanel_Cash_In_Here", "Click Here to cash in!");
        private string TextEmptyMultiSet = SK.Text("ManageCandsPanel_Make_MultiSet", "Click on cards below to make a set of at least 5");
        private string TextEmptySet = SK.Text("ManageCandsPanel_Make_Set", "Click on cards below to make a set of 5");
        private string TextIncompleteSetStart = (SK.Text("ManageCandsPanel_Choose_More", "More Cards Needed") + ": ");
        private string TextRemove = (SK.Text("ManageCandsPanel_Cancel_Purchase", "Click Card to cancel purchase") + ": ");
        private string TextRemoveSet = (SK.Text("ManageCandsPanel_Remove_From_Set", "Click Card to remove from set") + ": ");
        private List<CustomSelfDrawPanel.UICard> UICardList = new List<CustomSelfDrawPanel.UICard>();
        private List<CustomSelfDrawPanel.UICard> UICardListInplay = new List<CustomSelfDrawPanel.UICard>();
        private bool waitingResponse;

        public ManageCardsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addCardToCard(int cardType, bool showMessages)
        {
            CardTypes.CardDefinition definition = CardTypes.getCardDefinition(cardType);
            int num = 0;
            int fakeCardPoints = 0;
            foreach (CustomSelfDrawPanel.UICard card in this.ShoppingCart)
            {
                num += card.Definition.cardPoints;
            }
            num += definition.cardPoints;
            if (GameEngine.Instance.World.getTutorialStage() == 0x66)
            {
                fakeCardPoints = GameEngine.Instance.World.FakeCardPoints;
            }
            else
            {
                fakeCardPoints = GameEngine.Instance.World.ProfileCardpoints;
            }
            if (num > fakeCardPoints)
            {
                this.failedPurchaseCard = cardType;
                this.failedPurchaseCost = definition.cardPoints;
                if (showMessages)
                {
                    bool flag = MyMessageBox.Show(SK.Text("ManageCandsPanel_Not_Enough_Points", "That would cost more Card Points than you currently have. Would you like to trade existing cards for more points?"), SK.Text("ManageCandsPanel_Not_Enough_Points_Heading", "Not Enough Card Points"), MessageBoxButtons.YesNo) == DialogResult.Yes;
                    StatTrackingClient.Instance().ActivateTrigger(0x12, flag);
                    StatTrackingClient.Instance().ActivateTrigger(0x16, cardType);
                    if (flag)
                    {
                        this.SwitchToCash();
                        this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_left;
                        this.TabSelector.ClickArea = new Rectangle(0xc4, 0, 0x76, 30);
                    }
                }
                if (GameEngine.Instance.World.getTutorialStage() == 0x66)
                {
                    GameEngine.Instance.World.handleQuestObjectiveHappening(0x2717);
                }
            }
            else if (GameEngine.Instance.World.ShoppingCartCards.Count > 0x18)
            {
                if (showMessages)
                {
                    MyMessageBox.Show(SK.Text("ManageCandsPanel_Cards_Limit", "You may only buy up to 25 cards at a time."), SK.Text("ManageCandsPanel_Cards_Limit_Heading", "Maximum Reached"));
                }
            }
            else
            {
                StatTrackingClient.Instance().ActivateTrigger(20, cardType);
                GameEngine.Instance.World.ShoppingCartCards.Add(cardType);
                this.RefreshCart();
            }
        }

        private void addFilterButton(int category, BaseImage[] buttonImage, int index, int currentFilter)
        {
            this.addFilterButton(category, buttonImage[GFXLibrary.ButtonStateNormal], buttonImage[GFXLibrary.ButtonStateOver], buttonImage[GFXLibrary.ButtonStatePressed], index, currentFilter);
        }

        private void addFilterButton(int category, BaseImage normalImage, BaseImage overImage, BaseImage clickedImage, int index, int currentFilter)
        {
            int num = 0x17;
            int num2 = 3;
            if (GameEngine.Instance.World.NewCategoriesAvailable_FullHeight)
            {
                num = 0x15;
                num2 = 4;
            }
            CustomSelfDrawPanel.CSDButton item = new CustomSelfDrawPanel.CSDButton();
            if (currentFilter == category)
            {
                item.ImageNorm = (Image) overImage;
                item.ImageOver = (Image) overImage;
                item.ImageClick = (Image) overImage;
                item.Data = category;
                item.CustomTooltipData = category;
                item.CustomTooltipID = 0x2779;
                item.ClipRect = new Rectangle(0, 6, 0x33, 0x16);
                item.Position = new Point((this.AvailablePanel.X + this.AvailablePanel.Width) - 0x54, (this.AvailablePanel.Y + num2) + (index * num));
            }
            else
            {
                item.ImageNorm = (Image) normalImage;
                item.ImageOver = (Image) overImage;
                item.ImageClick = (Image) clickedImage;
                item.Data = category;
                item.CustomTooltipData = category;
                item.CustomTooltipID = 0x2779;
                item.Position = new Point((this.AvailablePanel.X + this.AvailablePanel.Width) - 0x54, (this.AvailablePanel.Y + num2) + (index * num));
                item.ClipRect = new Rectangle(0, 6, 0x33, 0x16);
                item.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.NewFilterClick), "PlayCardsPanel_filter");
            }
            this.FilterButtons.Add(item);
            this.mainBackgroundImage.addControl(item);
        }

        public void AddFloatingText(string text)
        {
            CustomSelfDrawPanel.CSDFloatingText item = new CustomSelfDrawPanel.CSDFloatingText();
            int num = -5;
            if (this.fastCashIn)
            {
                num = -1;
            }
            item.init(new Point(this.EmptyCards[0].X, this.EmptyCards[0].Y), new Size(this.EmptyCards[0].Width * 5, this.EmptyCards[0].Height), ARGBColors.Yellow, ARGBColors.Black, 0, num, -10, text, 0x20, 33.0, 3000.0, DXTimer.GetCurrentMilliseconds(), this.mainBackgroundImage);
            this.floatingLabels.Add(item);
        }

        private void addPointsData()
        {
            Graphics graphics = base.CreateGraphics();
            Size size = graphics.MeasureString(this.labelTitle.Text, this.labelTitle.Font, 0x3e8).ToSize();
            graphics.Dispose();
            this.imageTitlePoints.Position = new Point((this.labelTitle.X + size.Width) + 5, 5);
            this.labelTitlePoints.Position = new Point((this.labelTitle.X + size.Width) + 0x23, this.labelTitle.Y);
            this.labelTitlePoints.Text = GameEngine.Instance.World.ProfileCardpoints.ToString();
        }

        private void autoPlayCard(int userID, CardTypes.CardDefinition def, bool fromClick, bool fromValidate)
        {
            if (!this.waitingResponse)
            {
                this.waitingResponse = true;
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                int villageID = -1;
                int num2 = InterfaceMgr.Instance.getSelectedMenuVillage();
                if (!GameEngine.Instance.World.isCapital(num2))
                {
                    villageID = num2;
                }
                int num3 = GameEngine.Instance.World.getRank() + 1;
                if (def.cardRank > num3)
                {
                    MyMessageBox.Show(SK.Text("BuyCardsPanel_Rank_Too_low", "Your rank is too low to play this card.") + Environment.NewLine + SK.Text("BuyCardsPanel_Current_Rank", "Current Rank") + " : " + num3.ToString() + Environment.NewLine + SK.Text("BuyCardsPanel_Required_Rank", "Required Rank") + " : " + def.cardRank.ToString(), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
                    this.waitingResponse = false;
                }
                else
                {
                    this.autoCardUserID = userID;
                    this.autoCardVillageID = villageID;
                    this.autoCardDef = def;
                    if (fromClick && Program.mySettings.ConfirmPlayCard)
                    {
                        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
                        base.PanelActive = false;
                        this.waitingResponse = false;
                        InterfaceMgr.Instance.openConfirmPlayCardPopup(this.autoCardDef, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.autoPlayCardDelegate));
                    }
                    else if (!fromValidate && CardTypes.cardNeedsValidation(CardTypes.getCardType(this.autoCardDef.id)))
                    {
                        this.validateCardPossible(CardTypes.getCardType(this.autoCardDef.id), villageID);
                    }
                    else
                    {
                        if (InterfaceMgr.Instance.getCardWindow() != null)
                        {
                            CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
                        }
                        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
                        StatTrackingClient.Instance().ActivateTrigger(0x10, this.buyAndPlayCheckBox.Checked);
                        provider.PlayUserCard(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), userID.ToString(), villageID.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.CardPlayed), this);
                        try
                        {
                            GameEngine.Instance.World.removeProfileCard(userID);
                        }
                        catch (Exception exception)
                        {
                            MyMessageBox.Show(exception.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
                        }
                    }
                }
            }
        }

        public void autoPlayCardDelegate(bool fromClick, bool fromValidate)
        {
            this.autoPlayCard(this.autoCardUserID, this.autoCardDef, fromClick, fromValidate);
        }

        private void AvailableContentScroll()
        {
            int y = this.scrollbarAvailable.Value;
            this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, 8 - y);
            this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
            this.AvailablePanelContent.invalidate();
            this.AvailablePanel.invalidate();
        }

        private void buyAndPlayCheckChanged()
        {
            if (this.buyAndPlayCheckBox.Checked)
            {
                this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Get_And_Play_Card", "Get and Play Card");
            }
            else
            {
                this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
            }
        }

        private void CardPlayed(ICardsProvider provider, ICardsResponse response)
        {
            if (!response.SuccessCode.HasValue || (response.SuccessCode.Value != 1))
            {
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
                MyMessageBox.Show(PlayCardsWindow.translateCardError(response.Message, this.autoCardDef.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
                try
                {
                    GameEngine.Instance.World.addProfileCard(this.autoCardUserID, CardTypes.getStringFromCard(this.autoCardDef.id));
                }
                catch (Exception exception)
                {
                    MyMessageBox.Show(exception.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
                }
                if (InterfaceMgr.Instance.getCardWindow() != null)
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
                }
                StatTrackingClient.Instance().ActivateTrigger(0x10, false);
            }
            else
            {
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
                GameEngine.Instance.World.ProfileCardsSet.Remove(this.autoCardUserID);
                GameEngine.Instance.World.CardPlayed(this.autoCardDef.cardCategory, this.autoCardDef.id, this.autoCardVillageID);
                GameEngine.Instance.addRecentCard(this.autoCardDef.id);
                StatTrackingClient.Instance().ActivateTrigger(6, this.autoCardDef.id);
            }
            this.waitingResponse = false;
        }

        private void CashClick()
        {
            if (!this.cashingIn && !this.buyingCard)
            {
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest {
                    UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "")
                };
                string str = "";
                this.NumCardsCachingIn = 0;
                foreach (CustomSelfDrawPanel.UICard card in this.SetCards)
                {
                    if (card.Visible)
                    {
                        if (str.Length > 0)
                        {
                            str = str + ",";
                        }
                        str = str + card.UserID.ToString();
                        this.NumCardsCachingIn++;
                    }
                }
                req.CardString = str;
                provider.cashInCards(req, new CardsEndResponseDelegate(this.CashClickCallback), this);
                for (int i = 0; i < this.SlotAnims.Length; i++)
                {
                    this.SlotAnims[i].Visible = true;
                    this.SlotAnims[i].Playing = !this.fastCashIn;
                }
                this.SlotHolder.Visible = true;
                this.cashingIn = true;
                this.lastCashResponse = null;
                this.cardsButtons.Available = false;
                this.spinspeed = 0x40;
                this.spinstart = DateTime.Now;
                if (!this.fastCashIn && !this.playingSpinSound)
                {
                    this.playingSpinSound = true;
                    GameEngine.Instance.playInterfaceSound("CardSpinners_spin");
                    for (int j = 0; j < 5; j++)
                    {
                        this.spinSoundStopPlayed[j] = false;
                    }
                    this.spinSoundSoundID = 1;
                }
                this.mainBackgroundImage.invalidate();
                this.DynamicLabel.Visible = false;
            }
        }

        private void CashClickCallback(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode == 1)
            {
                StatTrackingClient.Instance().ActivateTrigger(0x13, null);
                foreach (int num in GameEngine.Instance.World.ProfileCardsSet)
                {
                    GameEngine.Instance.World.ProfileCards.Remove(num);
                }
                GameEngine.Instance.World.ProfileCardsSet.Clear();
                this.fastCashInCheckBox.Enabled = true;
                this.lastCashResponse = (XmlRpcCardsResponse) response;
                WorldMap world = GameEngine.Instance.World;
                world.ProfileCardpoints += response.Newpoints.Value;
            }
            else
            {
                MyMessageBox.Show(response.Message, SK.Text("GENERIC_Error", "Error"));
                this.GetCardsAvailable(true);
                int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                this.cashingIn = false;
                this.cardsButtons.Available = true;
                this.InitEmptyCards();
                this.RefreshSet();
                this.InitSpinners();
            }
        }

        private void clearSearchClicked()
        {
            this.searchButton.Visible = true;
            this.clearSearchButton.Visible = false;
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = false;
            this.handleSearchTextChanged();
        }

        private void ClickBuyMultiple()
        {
            if (!this.cashingIn && !this.buyingCard)
            {
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest {
                    UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", ""),
                    WorldID = RemoteServices.Instance.ProfileWorldID.ToString(),
                    CardString = ""
                };
                if (GameEngine.Instance.World.getTutorialStage() == 0x66)
                {
                    req.CardPoints = 1;
                }
                this.newcardcost = 0;
                for (int i = 0; i < this.ShoppingCart.Count; i++)
                {
                    req.CardString = req.CardString + this.ShoppingCart[i].Definition.name;
                    this.newcardcost += this.ShoppingCart[i].Definition.cardPoints;
                    if (i < (this.ShoppingCart.Count - 1))
                    {
                        req.CardString = req.CardString + ",";
                    }
                }
                this.newcardnames = req.CardString;
                provider.buyMultipleCards(req, new CardsEndResponseDelegate(this.MultipleCallback), this);
                this.buyingCard = true;
                this.cardsButtons.Available = false;
            }
        }

        private void ClickCardCart()
        {
            CustomSelfDrawPanel.UICard clickedControl = (CustomSelfDrawPanel.UICard) base.ClickedControl;
            this.addCardToCard(clickedControl.Definition.id, true);
        }

        private void ClickCardSet()
        {
            this.ClickCardSet(true);
        }

        private void ClickCardSet(bool initialCall)
        {
            if (!this.cashingIn && !this.buyingCard)
            {
                if ((GameEngine.Instance.World.ProfileCardsSet.Count < 5) || ((GameEngine.Instance.World.ProfileCardsSet.Count < 60) && this.fastCashIn))
                {
                    CustomSelfDrawPanel.UICard clickedControl = (CustomSelfDrawPanel.UICard) base.ClickedControl;
                    if ((clickedControl.cardCount > 1) && (clickedControl.UserIDList.Count > 1))
                    {
                        int item = clickedControl.UserIDList[0];
                        if (GameEngine.Instance.World.ProfileCards[item].rewardcard)
                        {
                            GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
                            MyMessageBox.Show(SK.Text("ManageCandsPanel_Cannot_Cash_Rewards", "You cannot cash in reward cards."), SK.Text("GENERIC_Error", "Error"));
                        }
                        else
                        {
                            clickedControl.UserIDList.Remove(item);
                            clickedControl.cardCount--;
                            if (clickedControl.cardCount > 1)
                            {
                                clickedControl.countLabel.Text = clickedControl.cardCount.ToString();
                                if (clickedControl.cardCount >= 100)
                                {
                                    clickedControl.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                                }
                                else
                                {
                                    clickedControl.countLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                clickedControl.countLabel.Text = "";
                            }
                            this.AvailablePanelContent.invalidate();
                            clickedControl.UserID = clickedControl.UserIDList[0];
                            if (!GameEngine.Instance.World.ProfileCardsSet.Contains(item))
                            {
                                if (initialCall)
                                {
                                    GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set");
                                }
                                if (GameEngine.Instance.World.ProfileCardsSet.Count >= 5)
                                {
                                    int index = GameEngine.Instance.World.ProfileCardsSet.Count % 5;
                                    int num3 = GameEngine.Instance.World.ProfileCardsSet[index];
                                    GameEngine.Instance.World.ProfileCardsSet.Remove(num3);
                                    GameEngine.Instance.World.ProfileCardsSet.Insert(index, item);
                                    GameEngine.Instance.World.ProfileCardsSet.Add(num3);
                                }
                                else
                                {
                                    GameEngine.Instance.World.ProfileCardsSet.Add(item);
                                }
                                if (GameEngine.shiftPressedAlways)
                                {
                                    this.ClickCardSet(false);
                                }
                            }
                            else
                            {
                                MyMessageBox.Show(SK.Text("ManageCandsPanel_Already_In_Set", "It appears that card is already in the set."), SK.Text("GENERIC_Error", "Error"));
                            }
                        }
                    }
                    else if ((clickedControl.UserIDList.Count > 0) && GameEngine.Instance.World.ProfileCards[clickedControl.UserIDList[0]].rewardcard)
                    {
                        GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
                        MyMessageBox.Show(SK.Text("ManageCandsPanel_Cannot_Cash_Rewards", "You cannot cash in reward cards."), SK.Text("GENERIC_Error", "Error"));
                    }
                    else
                    {
                        this.UICardList.Remove(clickedControl);
                        int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                        this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                        this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                        if (clickedControl.UserIDList.Count > 0)
                        {
                            clickedControl.UserID = clickedControl.UserIDList[0];
                            int num5 = clickedControl.UserIDList[0];
                            if (!GameEngine.Instance.World.ProfileCardsSet.Contains(num5))
                            {
                                GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set");
                                if (GameEngine.Instance.World.ProfileCardsSet.Count >= 5)
                                {
                                    int num6 = GameEngine.Instance.World.ProfileCardsSet.Count % 5;
                                    int num7 = GameEngine.Instance.World.ProfileCardsSet[num6];
                                    GameEngine.Instance.World.ProfileCardsSet.Remove(num7);
                                    GameEngine.Instance.World.ProfileCardsSet.Insert(num6, num5);
                                    GameEngine.Instance.World.ProfileCardsSet.Add(num7);
                                }
                                else
                                {
                                    GameEngine.Instance.World.ProfileCardsSet.Add(num5);
                                }
                            }
                            else
                            {
                                GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
                                MyMessageBox.Show(SK.Text("ManageCandsPanel_Already_In_Set", "It appears that card is already in the set."), SK.Text("GENERIC_Error", "Error"));
                            }
                        }
                        else
                        {
                            GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_error");
                            MyMessageBox.Show(SK.Text("ManageCandsPanel_Not_Own_Card", "It appears you do not own that card."), SK.Text("GENERIC_Error", "Error"));
                        }
                    }
                    if (GameEngine.Instance.World.ProfileCardsSet.Count > 5)
                    {
                        this.fastCashInCheckBox.Enabled = false;
                    }
                    else
                    {
                        this.fastCashInCheckBox.Enabled = true;
                    }
                    this.RefreshSet();
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("ManageCardsPanel_cash_in_card_set_full_error");
                }
            }
        }

        private void ClickCardUncart()
        {
            this.LabelClickToRemove.Text = "";
            CustomSelfDrawPanel.UICard clickedControl = (CustomSelfDrawPanel.UICard) base.ClickedControl;
            GameEngine.Instance.World.ShoppingCartCards.RemoveAt(clickedControl.UserID);
            this.RefreshCart();
        }

        private void ClickCardUnset()
        {
            if (!this.cashingIn && !this.buyingCard)
            {
                if (GameEngine.shiftPressedAlways)
                {
                    GameEngine.Instance.World.ProfileCardsSet.Clear();
                }
                else
                {
                    CustomSelfDrawPanel.UICard clickedControl = (CustomSelfDrawPanel.UICard) base.ClickedControl;
                    GameEngine.Instance.World.ProfileCardsSet.Remove(clickedControl.UserID);
                }
                this.GetCardsAvailable(true);
                int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                this.RefreshSet();
                this.LabelClickToRemove.Text = "";
                if (GameEngine.Instance.World.ProfileCardsSet.Count > 5)
                {
                    this.fastCashInCheckBox.Enabled = false;
                }
                else
                {
                    this.fastCashInCheckBox.Enabled = true;
                }
            }
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closePlayCardsWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        private void compressClicked()
        {
            if (!this.compressedCards)
            {
                this.compressedCards = true;
                this.scrollbarAvailable.Value = 0;
                int height = 0;
                if (this.PanelMode == PANEL_MODE_BUY)
                {
                    height = this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
                }
                else
                {
                    height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                }
                this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                this.AvailableContentScroll();
                base.Invalidate();
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

        private void expandClicked()
        {
            if (this.compressedCards)
            {
                this.compressedCards = false;
                this.scrollbarAvailable.Value = 0;
                int height = 0;
                if (this.PanelMode == PANEL_MODE_BUY)
                {
                    height = this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
                }
                else
                {
                    height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                }
                this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                this.AvailableContentScroll();
                base.Invalidate();
            }
        }

        private void GetCardsAvailable(bool redosearch)
        {
            if (redosearch)
            {
                GameEngine.Instance.World.searchProfileCardsRedoLast();
            }
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            foreach (int num in GameEngine.Instance.World.ProfileCardsSearch)
            {
                int id = GameEngine.Instance.World.ProfileCards[num].id;
                if (!GameEngine.Instance.World.ProfileCardsSet.Contains(num))
                {
                    if (dictionary.ContainsKey(id))
                    {
                        Dictionary<int, int> dictionary2;
                        int num7;
                        (dictionary2 = dictionary)[num7 = id] = dictionary2[num7] + 1;
                    }
                    else
                    {
                        dictionary.Add(id, 1);
                    }
                }
            }
            CustomSelfDrawPanel.UICard card = null;
            foreach (CustomSelfDrawPanel.UICard card2 in this.UICardList)
            {
                card2.clearControls();
                if (card2.Parent != null)
                {
                    card2.Parent.removeControl(card2);
                }
            }
            this.UICardList.Clear();
            int num3 = GameEngine.Instance.World.getRank() + 1;
            foreach (int num4 in GameEngine.Instance.World.ProfileCardsSearch)
            {
                int key = GameEngine.Instance.World.ProfileCards[num4].id;
                if (dictionary.ContainsKey(key) && !GameEngine.Instance.World.ProfileCardsSet.Contains(num4))
                {
                    CustomSelfDrawPanel.UICard item = new CustomSelfDrawPanel.UICard {
                        cardCount = dictionary[key],
                        UserID = num4
                    };
                    item.UserIDList.Add(num4);
                    item.Definition = GameEngine.Instance.World.ProfileCards[num4];
                    switch (item.Definition.cardColour)
                    {
                        case 1:
                            item.bigFrame = GFXLibrary.BlueCardOverlayBig;
                            item.bigFrameOver = GFXLibrary.BlueCardOverlayBigOver;
                            break;

                        case 2:
                            item.bigFrame = GFXLibrary.GreenCardOverlayBig;
                            item.bigFrameOver = GFXLibrary.GreenCardOverlayBigOver;
                            break;

                        case 3:
                            item.bigFrame = GFXLibrary.PurpleCardOverlayBig;
                            item.bigFrameOver = GFXLibrary.PurpleCardOverlayBigOver;
                            break;

                        case 4:
                            item.bigFrame = GFXLibrary.RedCardOverlayBig;
                            item.bigFrameOver = GFXLibrary.RedCardOverlayBigOver;
                            break;

                        case 5:
                            item.bigFrame = GFXLibrary.YellowCardOverlayBig;
                            item.bigFrameOver = GFXLibrary.YellowCardOverlayBigOver;
                            break;
                    }
                    item.bigImage = GFXLibrary.Instance.getCardImageBig(item.Definition.id);
                    item.Size = item.bigFrame.Size;
                    item.CustomTooltipID = 0x2775;
                    item.CustomTooltipData = item.Definition.id;
                    item.bigGradeImage = new CustomSelfDrawPanel.CSDImage();
                    int num6 = CardTypes.getGrade(item.Definition.cardGrade);
                    switch (num6)
                    {
                        case 0x10000:
                            item.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
                            item.bigGradeImage.Position = new Point(item.Width - item.bigGradeImage.Width, 0);
                            break;

                        case 0x20000:
                            item.bigGradeImage.Image = (Image) GFXLibrary.CardGradeSilver;
                            item.bigGradeImage.Position = new Point(item.Width - item.bigGradeImage.Width, 0);
                            break;

                        case 0x40000:
                            item.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[0];
                            item.bigGradeImage.Position = new Point((item.Width - item.bigGradeImage.Width) - 3, 0);
                            break;

                        case 0x200000:
                            item.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[0];
                            item.bigGradeImage.Position = new Point((item.Width - item.bigGradeImage.Width) - 3, -10);
                            break;

                        case 0x400000:
                            item.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[0];
                            item.bigGradeImage.Position = new Point((item.Width - item.bigGradeImage.Width) - 3, -12);
                            break;

                        case 0x80000:
                            item.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[0];
                            item.bigGradeImage.Position = new Point((item.Width - item.bigGradeImage.Width) - 3, -2);
                            break;

                        case 0x100000:
                            item.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[0];
                            item.bigGradeImage.Position = new Point((item.Width - item.bigGradeImage.Width) - 3, -7);
                            break;

                        default:
                            item.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
                            item.bigGradeImage.Position = new Point(item.Width - item.bigGradeImage.Width, 0);
                            break;
                    }
                    item.bigBaseImage = new CustomSelfDrawPanel.CSDImage();
                    item.bigBaseImage.Position = new Point(10, 11);
                    item.bigBaseImage.Size = item.bigImage.Size;
                    item.bigBaseImage.Image = (Image) item.bigImage;
                    item.addControl(item.bigBaseImage);
                    item.bigFrameImage = new CustomSelfDrawPanel.CSDImage();
                    item.bigFrameImage.Position = new Point(0, 0);
                    item.bigFrameImage.Size = item.bigFrame.Size;
                    item.bigFrameImage.Image = (Image) item.bigFrame;
                    item.addControl(item.bigFrameImage);
                    switch (num6)
                    {
                        case 0x40000:
                            item.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                            item.bigFrameExtraImage.Position = new Point(0, 0);
                            item.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_gold;
                            item.addControl(item.bigFrameExtraImage);
                            break;

                        case 0x80000:
                        case 0x100000:
                        case 0x200000:
                            item.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                            item.bigFrameExtraImage.Position = new Point(0, 0);
                            item.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_diamond;
                            item.addControl(item.bigFrameExtraImage);
                            break;

                        case 0x400000:
                            item.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                            item.bigFrameExtraImage.Position = new Point(0, 0);
                            item.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_sapphire;
                            item.addControl(item.bigFrameExtraImage);
                            break;
                    }
                    item.bigGradeImage.Size = item.bigGradeImage.Image.Size;
                    item.addControl(item.bigGradeImage);
                    item.bigTitle = new CustomSelfDrawPanel.CSDLabel();
                    item.bigTitle.Text = CardTypes.getDescriptionFromCard(item.Definition.id);
                    item.bigTitle.Size = new Size(110, 0x30);
                    item.bigTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    if (((((item.Definition.id == 0x709) || (item.Definition.id == 0x606)) || ((item.Definition.id == 0xc41) || (item.Definition.id == 0x50a))) || ((item.Definition.id == 0x605) || (item.Definition.id == 0x607))) && (Program.mySettings.LanguageIdent == "de"))
                    {
                        item.bigTitle.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                    }
                    else
                    {
                        item.bigTitle.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    }
                    item.bigTitle.Color = ARGBColors.White;
                    item.bigTitle.DropShadowColor = ARGBColors.Black;
                    item.bigTitle.Position = new Point(0x26, 12);
                    item.addControl(item.bigTitle);
                    item.bigEffect = new CustomSelfDrawPanel.CSDLabel();
                    item.bigEffect.Text = item.Definition.EffectText;
                    item.bigEffect.Size = new Size(150, 0x40);
                    item.bigEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    item.bigEffect.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    if ((Program.mySettings.LanguageIdent == "de") && CardTypes.isGermanSmallDesc(item.Definition.id))
                    {
                        item.bigEffect.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                    }
                    item.bigEffect.Color = ARGBColors.White;
                    item.bigEffect.DropShadowColor = ARGBColors.Black;
                    item.bigEffect.Position = new Point(14, 0xae);
                    item.addControl(item.bigEffect);
                    CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                        Position = new Point(2, 2),
                        Size = new Size(item.Width, item.Height),
                        Text = "",
                        Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                        Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold),
                        Color = ARGBColors.Yellow,
                        DropShadowColor = ARGBColors.Black
                    };
                    item.addControl(control);
                    item.countLabel = control;
                    if (num3 < item.Definition.cardRank)
                    {
                        Color red = ARGBColors.Red;
                    }
                    else
                    {
                        Color white = ARGBColors.White;
                    }
                    CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                        Position = new Point(150, 220),
                        Size = new Size(20, 13),
                        Text = item.Definition.cardRank.ToString(),
                        Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                        Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold),
                        Color = ARGBColors.White,
                        DropShadowColor = ARGBColors.Black
                    };
                    item.addControl(label2);
                    item.rankLabel = label2;
                    item.ScaleAll(0.95);
                    this.UICardList.Add(item);
                    dictionary.Remove(key);
                    card = item;
                    if (num3 < item.Definition.cardRank)
                    {
                        item.Hilight(ARGBColors.Gray);
                    }
                    else
                    {
                        item.Hilight(ARGBColors.White);
                    }
                    item.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardSet));
                    continue;
                }
                if ((card != null) && !GameEngine.Instance.World.ProfileCardsSet.Contains(num4))
                {
                    card.UserIDList.Add(num4);
                }
            }
            GFXLibrary.Instance.closeBigCardsLoader();
        }

        public void handleSearchTextChanged()
        {
            if (this.PanelMode == PANEL_MODE_CASH)
            {
                GameEngine.Instance.World.searchProfileCardsRedoLast(((PlayCardsWindow) base.ParentForm).getNameSearchText());
                this.SwitchToCash();
            }
            else
            {
                GameEngine.Instance.World.lastUserCardNameFilter = ((PlayCardsWindow) base.ParentForm).getNameSearchText();
                this.SwitchToBuy();
            }
        }

        public void init(int cardSection)
        {
            CustomSelfDrawPanel.CSDImage image2;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate overDelegate = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate leaveDelegate = null;
            this.currentCardSection = cardSection;
            base.clearControls();
            this.mainBackgroundImage.Image = GFXLibrary.dummy;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.ContentWidth = base.Width - (2 * BorderPadding);
            this.AvailablePanelWidth = 800;
            this.InplayPanelWidth = (this.ContentWidth - BorderPadding) - this.AvailablePanelWidth;
            CustomSelfDrawPanel.CSDExtendingPanel panel = new CustomSelfDrawPanel.CSDExtendingPanel {
                Size = base.Size,
                Position = new Point(0, 0)
            };
            this.mainBackgroundImage.addControl(panel);
            panel.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
            CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left,
                Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size,
                Position = new Point(0, 0)
            };
            panel.addControl(image);
            image2 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right,
                Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size,
                Position = new Point((panel.Width - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Width) - 6, (panel.Height - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Height) - 6)
            };
            panel.addControl(image2);
            this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
            this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 0x177);
            this.AvailablePanel.Position = new Point(8, (base.Height - 8) - 0x177);
            this.AvailablePanel.Alpha = 0.8f;
            this.mainBackgroundImage.addControl(this.AvailablePanel);
            this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
            int width = base.Width;
            int borderPadding = BorderPadding;
            int num3 = this.AvailablePanel.Width;
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
            this.closeImage.CustomTooltipID = 0x2774;
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 40, new Point((((base.Width - 1) - 0x11) - 50) + 3, 5), true);
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            this.cardsButtons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) base.ParentForm);
            this.cardsButtons.Position = new Point(0x328, 0x25);
            this.mainBackgroundImage.addControl(this.cardsButtons);
            this.labelTitle.Position = new Point(0x1b, 8);
            this.labelTitle.Size = new Size(600, 0x40);
            this.labelTitle.Text = "";
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            this.labelTitlePoints.Position = new Point(0x1b, 8);
            this.labelTitlePoints.Size = new Size(600, 0x40);
            this.labelTitlePoints.Text = "";
            this.labelTitlePoints.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitlePoints.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitlePoints.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitlePoints);
            this.imageTitlePoints.Image = (Image) GFXLibrary.cardpanel_manage_card_points_icon;
            this.imageTitlePoints.Position = new Point(400, 5);
            this.mainBackgroundImage.addControl(this.imageTitlePoints);
            this.searchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_button_normal;
            this.searchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_button_over;
            this.searchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_button_pushed;
            this.searchButton.Position = new Point(0x32b, 7);
            this.searchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchClicked), "StatsPanel_search");
            this.searchButton.CustomTooltipID = 0x284f;
            this.searchButton.Visible = true;
            this.mainBackgroundImage.addControl(this.searchButton);
            this.clearSearchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_clear_button_normal;
            this.clearSearchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_clear_button_over;
            this.clearSearchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_clear_button_pushed;
            this.clearSearchButton.Position = new Point(740, 7);
            this.clearSearchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearSearchClicked), "StatsPanel_clear_search");
            this.clearSearchButton.Visible = false;
            this.clearSearchButton.CustomTooltipID = 0x2850;
            this.mainBackgroundImage.addControl(this.clearSearchButton);
            this.addPointsData();
            CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point((2 * BorderPadding) + this.AvailablePanelWidth, BorderPadding),
                Size = new Size(300, 0x40),
                Text = SK.Text("ManageCandsPanel_Cards_In_Play", "Cards In Play"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold),
                Color = ARGBColors.White,
                DropShadowColor = ARGBColors.Black
            };
            this.cardTitle = new CustomSelfDrawPanel.CSDLabel();
            this.cardTitle.Position = new Point(0x10, 40);
            this.cardTitle.Size = new Size(600, 0x40);
            this.cardTitle.Text = string.Empty;
            this.cardTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.cardTitle.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.cardTitle.Color = ARGBColors.White;
            this.cardTitle.DropShadowColor = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.cardTitle);
            this.labelFeedback = new CustomSelfDrawPanel.CSDLabel();
            this.labelFeedback.Position = new Point(0x10, 500);
            this.labelFeedback.Size = new Size(600, 0x40);
            this.labelFeedback.Text = "";
            this.labelFeedback.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelFeedback.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.labelFeedback.Color = ARGBColors.White;
            this.labelFeedback.DropShadowColor = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelFeedback);
            this.buttonCash = new CustomSelfDrawPanel.CSDImage();
            this.buttonBonus = new CustomSelfDrawPanel.CSDImage();
            this.buttonCatalog = new CustomSelfDrawPanel.CSDImage();
            this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.buttonCash.Size = this.buttonCash.Image.Size;
            this.buttonBonus.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.buttonBonus.Size = this.buttonBonus.Image.Size;
            this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
            this.buttonCatalog.Size = this.buttonCash.Image.Size;
            this.buttonCash.Position = new Point((this.AvailablePanel.X + (this.AvailablePanel.Width / 2)) - this.buttonCash.Width, this.cardsButtons.Y + 4);
            this.buttonBonus.Position = new Point(this.buttonCash.X, this.buttonCash.Y);
            this.buttonCatalog.Position = new Point(this.buttonCash.X - this.buttonCash.Width, this.buttonCash.Y);
            this.buttonBonus.setMouseOverDelegate(() => this.buttonBonus.Image = (Image) GFXLibrary.cardpanel_button_blue_over, () => this.buttonBonus.Image = (Image) GFXLibrary.cardpanel_button_blue_normal);
            this.buttonCash.setMouseOverDelegate(() => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_over, () => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal);
            this.buttonCatalog.setMouseOverDelegate(() => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_over, () => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_normal);
            this.buttonBonus.Visible = false;
            this.buttonCash.Visible = true;
            this.fastCashIn = Program.mySettings.fastCashIn;
            this.fastCashInCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
            this.fastCashInCheckBox.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.fastCashInCheckBox.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.fastCashInCheckBox.Position = new Point(this.AvailablePanel.X + 590, this.cardsButtons.Y + 160);
            this.fastCashInCheckBox.Checked = this.fastCashIn;
            this.fastCashInCheckBox.CBLabel.Text = SK.Text("ManageCards_multicashin", "Multi-Cash In");
            this.fastCashInCheckBox.CBLabel.Color = ARGBColors.Black;
            this.fastCashInCheckBox.CBLabel.Position = new Point(20, -1);
            this.fastCashInCheckBox.CBLabel.Size = new Size(250, 0x19);
            this.fastCashInCheckBox.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.fastCashInCheckBox.setCheckChangedDelegate(delegate {
                Program.mySettings.fastCashIn = this.fastCashIn = this.fastCashInCheckBox.Checked;
                this.RefreshSet();
            });
            this.mainBackgroundImage.addControl(this.fastCashInCheckBox);
            this.buyAndPlayCheckBox = new CustomSelfDrawPanel.CSDCheckBox();
            this.buyAndPlayCheckBox.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
            this.buyAndPlayCheckBox.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
            this.buyAndPlayCheckBox.Position = new Point(this.AvailablePanel.X + 100, this.cardsButtons.Y + 0x5c);
            this.buyAndPlayCheckBox.Checked = false;
            this.buyAndPlayCheckBox.Visible = false;
            this.buyAndPlayCheckBox.CBLabel.Text = SK.Text("ManageCards_buyAndPlay", "Play Card Immediately");
            this.buyAndPlayCheckBox.CBLabel.Color = ARGBColors.Black;
            this.buyAndPlayCheckBox.CBLabel.Position = new Point(20, -1);
            this.buyAndPlayCheckBox.CBLabel.Size = new Size(250, 0x19);
            this.buyAndPlayCheckBox.CBLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.buyAndPlayCheckBox.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.buyAndPlayCheckChanged));
            this.mainBackgroundImage.addControl(this.buyAndPlayCheckBox);
            CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, -2),
                Size = this.buttonCash.Size,
                Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.Black
            };
            this.buttonCash.addControl(label2);
            CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(0, -2),
                Size = this.buttonCash.Size,
                Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In"),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Color = ARGBColors.Black
            };
            this.buttonBonus.addControl(label3);
            this.labelBuyCash = new CustomSelfDrawPanel.CSDLabel();
            this.labelBuyCash.Position = new Point(0, -2);
            this.labelBuyCash.Size = this.buttonCash.Size;
            this.labelBuyCash.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
            this.labelBuyCash.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.labelBuyCash.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.labelBuyCash.Color = ARGBColors.Black;
            this.LabelClickToRemove = new CustomSelfDrawPanel.CSDLabel();
            this.LabelClickToRemove.Position = new Point(this.AvailablePanel.X, this.cardsButtons.Y);
            this.LabelClickToRemove.Size = new Size(600, 0x12);
            this.LabelClickToRemove.Text = "";
            this.LabelClickToRemove.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.LabelClickToRemove.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.LabelClickToRemove.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.LabelClickToRemove);
            this.buttonCatalog.addControl(this.labelBuyCash);
            this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToBuy), "ManageCardsPanel_switch_to_buy_cards");
            if (GameEngine.Instance.World.ProfileCardsSet.Count < 5)
            {
                this.buttonCash.Colorise = ARGBColors.Gray;
                this.buttonCash.setMouseOverDelegate(null, null);
                this.buttonCash.setClickDelegate(null);
            }
            else
            {
                this.buttonCash.Colorise = ARGBColors.White;
                if (overDelegate == null)
                {
                    overDelegate = () => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_over;
                }
                if (leaveDelegate == null)
                {
                    leaveDelegate = () => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
                }
                this.buttonCash.setMouseOverDelegate(overDelegate, leaveDelegate);
                this.buttonCash.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CashClick), "ManageCardsPanel_switch_to_cash_in");
            }
            this.LayoutPanelMode = this.PanelMode = PANEL_MODE_CASH;
            CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
            GameEngine.Instance.World.searchProfileCards(filter, "", ((PlayCardsWindow) base.ParentForm).getNameSearchText());
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = false;
            this.ResizeAvailable(0x177);
            this.GetCardsAvailable(false);
            this.RenderCards(this.UICardList);
            this.InitEmptyCards();
            this.RefreshSet();
            this.InitDynamicPanel();
            this.CatalogFilterDefinition.cardCategory = 0;
            this.CatalogFilterDefinition.cardColour = 0;
            this.InitCatalog();
            CustomSelfDrawPanel.CSDControl control = new CustomSelfDrawPanel.CSDControl {
                Position = new Point(0, 0),
                Size = base.Size
            };
            this.mainBackgroundImage.addControl(control);
            this.mainBackgroundImage.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelHandler));
            this.InitFilters();
            this.InitTabs();
            this.mainBackgroundImage.invalidate();
            GameEngine.shiftPressedAlways = false;
        }

        private void InitCatalog()
        {
            if (GameEngine.Instance.World.getTutorialStage() == 0x66)
            {
                if (this.CardCatalog != null)
                {
                    foreach (CustomSelfDrawPanel.UICard card in this.CardCatalog)
                    {
                        card.clearControls();
                        if (card.Parent != null)
                        {
                            card.Parent.removeControl(card);
                        }
                    }
                }
                this.CardCatalog.Clear();
                this.CardCatalog = new List<CustomSelfDrawPanel.UICard>();
                CustomSelfDrawPanel.UICard item = this.makeUICard(CardTypes.getCardDefinition(0xc29), 0, GameEngine.Instance.World.getRank() + 1);
                GFXLibrary.Instance.closeBigCardsLoader();
                this.CardCatalog.Add(item);
                item.countLabel.Text = item.Definition.cardPoints.ToString();
                if (item.Definition.cardPoints >= 100)
                {
                    item.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                }
                item.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardCart), "ManageCardsPanel_purchase_card");
            }
            else
            {
                string name = ((PlayCardsWindow) base.ParentForm).getNameSearchText();
                if (this.CardCatalog != null)
                {
                    foreach (CustomSelfDrawPanel.UICard card3 in this.CardCatalog)
                    {
                        card3.clearControls();
                        if (card3.Parent != null)
                        {
                            card3.Parent.removeControl(card3);
                        }
                    }
                }
                this.CardCatalog.Clear();
                this.CardCatalog = new List<CustomSelfDrawPanel.UICard>();
                foreach (CardTypes.CardDefinition definition in CardTypes.cardList)
                {
                    if ((((((definition.cardRank > 0) && (definition.cardRarity > 0)) && ((definition.available == 1) && (definition.cardPoints > 0))) && ((this.CatalogFilterDefinition.cardCategory == 0) || (this.CatalogFilterDefinition.cardCategory == definition.cardCategory))) && ((this.CatalogFilterDefinition.cardColour == 0) || (this.CatalogFilterDefinition.cardColour == definition.cardColour))) && (((this.CatalogFilterDefinition.newCardCategoryFilter == 0) || CardTypes.isCardInNewCategory(definition.id, this.CatalogFilterDefinition.newCardCategoryFilter)) && ((name.Length == 0) || CardTypes.containsName(definition.id, name))))
                    {
                        CustomSelfDrawPanel.UICard card4 = this.makeUICard(definition, 0, GameEngine.Instance.World.getRank() + 1);
                        this.CardCatalog.Add(card4);
                        card4.countLabel.Text = card4.Definition.cardPoints.ToString();
                        if (card4.Definition.cardPoints >= 100)
                        {
                            card4.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                        }
                        card4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardCart), "ManageCardsPanel_purchase_card");
                    }
                }
                GFXLibrary.Instance.closeBigCardsLoader();
            }
            this.CardCatalog.Sort(delegate (CustomSelfDrawPanel.UICard card1, CustomSelfDrawPanel.UICard card2) {
                if (card1.Definition.cardPoints != card2.Definition.cardPoints)
                {
                    return card1.Definition.cardPoints.CompareTo(card2.Definition.cardPoints);
                }
                return CardTypes.getDescriptionFromCard(card1.Definition.id).CompareTo(CardTypes.getDescriptionFromCard(card2.Definition.id));
            });
        }

        private void InitDynamicPanel()
        {
            this.DynamicPanel.Position = new Point(((this.EmptyCards[4].X + this.EmptyCards[4].Width) / 2) - 6, this.EmptyCards[0].Y / 2);
            this.DynamicPanel.Size = new Size(this.cardsButtons.X - this.DynamicPanel.Position.X, this.EmptyCards[0].Height / 2);
            this.DynamicLabel = new CustomSelfDrawPanel.CSDLabel();
            this.DynamicLabel.Position = new Point(0, 0);
            this.DynamicLabel.Size = this.DynamicPanel.Size;
            if (!this.fastCashIn)
            {
                this.DynamicLabel.Text = this.TextEmptySet;
            }
            else
            {
                this.DynamicLabel.Text = this.TextEmptyMultiSet;
            }
            this.DynamicLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.DynamicLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.DynamicLabel.Color = ARGBColors.Black;
            this.DynamicPanel.addControl(this.DynamicLabel);
            this.DynamicLabel.Visible = true;
            this.DynamicButton = new CustomSelfDrawPanel.CSDImage();
            this.DynamicButton.Image = (Image) GFXLibrary.cardpanel_cashin_normal;
            this.DynamicButton.Size = this.DynamicButton.Image.Size;
            this.DynamicButton.Position = new Point((this.DynamicPanel.Width / 2) - (this.DynamicButton.Width / 2), (this.DynamicPanel.Height / 2) - (this.DynamicButton.Height / 2));
            this.DynamicPanel.addControl(this.DynamicButton);
            this.DynamicButton.Visible = false;
            this.DynamicButton.setMouseOverDelegate(() => this.DynamicButton.Image = (Image) GFXLibrary.cardpanel_cashin_over, () => this.DynamicButton.Image = (Image) GFXLibrary.cardpanel_cashin_normal);
            this.DynamicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CashClick), "ManageCardsPanel_switch_to_cash_in");
            this.DynamicButtonLabel = new CustomSelfDrawPanel.CSDLabel();
            this.DynamicButtonLabel.Position = new Point(0x77, 0x15);
            this.DynamicButtonLabel.Size = new Size(0x90, 0x42);
            this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In");
            this.DynamicButtonLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            if (Program.mySettings.LanguageIdent == "ru")
            {
                this.DynamicButtonLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            }
            else
            {
                this.DynamicButtonLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            }
            this.DynamicButtonLabel.Color = ARGBColors.Black;
            this.DynamicButtonLabel.Visible = true;
            this.DynamicButton.addControl(this.DynamicButtonLabel);
            this.mainBackgroundImage.addControl(this.DynamicPanel);
            this.InitSpinners();
        }

        private void InitEmptyCards()
        {
            for (int i = 0; i < 5; i++)
            {
                if (this.EmptyCards[i] != null)
                {
                    this.mainBackgroundImage.removeControl(this.EmptyCards[i]);
                }
                this.EmptyCards[i] = new CustomSelfDrawPanel.CSDImage();
                this.EmptyCards[i].Image = (Image) GFXLibrary.CardBackBig;
                this.EmptyCards[i].Size = this.EmptyCards[i].Image.Size;
                this.EmptyCards[i].Scale = 0.5;
                this.mainBackgroundImage.addControl(this.EmptyCards[i]);
                this.EmptyCards[i].Position = new Point(((i * this.EmptyCards[i].Width) + this.AvailablePanel.X) + 8, ((this.buttonCash.Y + this.buttonCash.Height) * 2) + 8);
                this.EmptyCards[i].Visible = true;
            }
            for (int j = 0; j < 60; j++)
            {
                this.SetCards[j] = new CustomSelfDrawPanel.UICard();
                this.SetCards[j].Visible = false;
            }
        }

        private void InitFilters()
        {
            foreach (CustomSelfDrawPanel.CSDButton button in this.FilterButtons)
            {
                this.mainBackgroundImage.removeControl(button);
            }
            this.FilterButtons.Clear();
            int currentFilter = 0;
            if (GameEngine.Instance.World.lastUserCardSearchCriteria != null)
            {
                currentFilter = GameEngine.Instance.World.lastUserCardSearchCriteria.newCardCategoryFilter;
            }
            int num2 = 0;
            this.addFilterButton(0, GFXLibrary.CardFilters_All, num2++, currentFilter);
            if ((currentFilter & 0x1000) != 0)
            {
                this.addFilterButton(0x1000, GFXLibrary.CardFilters_Food, num2++, currentFilter);
                this.addFilterButton(0x1001, GFXLibrary.CardFilters_Apples, num2++, currentFilter);
                this.addFilterButton(0x1002, GFXLibrary.CardFilters_Cheese, num2++, currentFilter);
                this.addFilterButton(0x1003, GFXLibrary.CardFilters_Meat, num2++, currentFilter);
                this.addFilterButton(0x1004, GFXLibrary.CardFilters_Bread, num2++, currentFilter);
                this.addFilterButton(0x1005, GFXLibrary.CardFilters_Veg, num2++, currentFilter);
                this.addFilterButton(0x1006, GFXLibrary.CardFilters_Fish, num2++, currentFilter);
                this.addFilterButton(0x1007, GFXLibrary.cardTypeButtons[0x6f], GFXLibrary.cardTypeButtons[0x70], GFXLibrary.cardTypeButtons[0x71], num2++, currentFilter);
            }
            else
            {
                this.addFilterButton(0x1000, GFXLibrary.CardFilters_Food, num2++, currentFilter);
            }
            if ((currentFilter & 0x2000) != 0)
            {
                this.addFilterButton(0x2000, GFXLibrary.CardFilters_Resources, num2++, currentFilter);
                this.addFilterButton(0x2001, GFXLibrary.cardTypeButtons[0], GFXLibrary.cardTypeButtons[1], GFXLibrary.cardTypeButtons[2], num2++, currentFilter);
                this.addFilterButton(0x2002, GFXLibrary.cardTypeButtons[3], GFXLibrary.cardTypeButtons[4], GFXLibrary.cardTypeButtons[5], num2++, currentFilter);
                this.addFilterButton(0x2003, GFXLibrary.cardTypeButtons[6], GFXLibrary.cardTypeButtons[7], GFXLibrary.cardTypeButtons[8], num2++, currentFilter);
                this.addFilterButton(0x2004, GFXLibrary.cardTypeButtons[9], GFXLibrary.cardTypeButtons[10], GFXLibrary.cardTypeButtons[11], num2++, currentFilter);
            }
            else
            {
                this.addFilterButton(0x2000, GFXLibrary.CardFilters_Resources, num2++, currentFilter);
            }
            if ((currentFilter & 0x4000) != 0)
            {
                this.addFilterButton(0x4000, GFXLibrary.CardFilters_Honour, num2++, currentFilter);
                this.addFilterButton(0x4001, GFXLibrary.cardTypeButtons[12], GFXLibrary.cardTypeButtons[13], GFXLibrary.cardTypeButtons[14], num2++, currentFilter);
                this.addFilterButton(0x4002, GFXLibrary.cardTypeButtons[15], GFXLibrary.cardTypeButtons[0x10], GFXLibrary.cardTypeButtons[0x11], num2++, currentFilter);
                this.addFilterButton(0x4003, GFXLibrary.cardTypeButtons[0x12], GFXLibrary.cardTypeButtons[0x13], GFXLibrary.cardTypeButtons[20], num2++, currentFilter);
                this.addFilterButton(0x4004, GFXLibrary.cardTypeButtons[0x15], GFXLibrary.cardTypeButtons[0x16], GFXLibrary.cardTypeButtons[0x17], num2++, currentFilter);
                this.addFilterButton(0x4005, GFXLibrary.cardTypeButtons[0x18], GFXLibrary.cardTypeButtons[0x19], GFXLibrary.cardTypeButtons[0x1a], num2++, currentFilter);
                if (GameEngine.Instance.World.NewCategoriesAvailable_Salt)
                {
                    this.addFilterButton(0x4006, GFXLibrary.cardTypeButtons[0x1b], GFXLibrary.cardTypeButtons[0x1c], GFXLibrary.cardTypeButtons[0x1d], num2++, currentFilter);
                }
                if (GameEngine.Instance.World.NewCategoriesAvailable_Spice)
                {
                    this.addFilterButton(0x4007, GFXLibrary.cardTypeButtons[30], GFXLibrary.cardTypeButtons[0x1f], GFXLibrary.cardTypeButtons[0x20], num2++, currentFilter);
                }
                if (GameEngine.Instance.World.NewCategoriesAvailable_Silk)
                {
                    this.addFilterButton(0x4008, GFXLibrary.cardTypeButtons[0x21], GFXLibrary.cardTypeButtons[0x22], GFXLibrary.cardTypeButtons[0x23], num2++, currentFilter);
                }
            }
            else
            {
                this.addFilterButton(0x4000, GFXLibrary.CardFilters_Honour, num2++, currentFilter);
            }
            if ((currentFilter & 0x8000) != 0)
            {
                this.addFilterButton(0x8000, GFXLibrary.CardFilters_Weapons2, num2++, currentFilter);
                this.addFilterButton(0x8001, GFXLibrary.cardTypeButtons[0x24], GFXLibrary.cardTypeButtons[0x25], GFXLibrary.cardTypeButtons[0x26], num2++, currentFilter);
                this.addFilterButton(0x8002, GFXLibrary.cardTypeButtons[0x27], GFXLibrary.cardTypeButtons[40], GFXLibrary.cardTypeButtons[0x29], num2++, currentFilter);
                this.addFilterButton(0x8003, GFXLibrary.cardTypeButtons[0x2a], GFXLibrary.cardTypeButtons[0x2b], GFXLibrary.cardTypeButtons[0x2c], num2++, currentFilter);
                this.addFilterButton(0x8004, GFXLibrary.cardTypeButtons[0x2d], GFXLibrary.cardTypeButtons[0x2e], GFXLibrary.cardTypeButtons[0x2f], num2++, currentFilter);
                if (GameEngine.Instance.World.NewCategoriesAvailable_Catapults)
                {
                    this.addFilterButton(0x8005, GFXLibrary.cardTypeButtons[0x30], GFXLibrary.cardTypeButtons[0x31], GFXLibrary.cardTypeButtons[50], num2++, currentFilter);
                }
            }
            else
            {
                this.addFilterButton(0x8000, GFXLibrary.CardFilters_Weapons2, num2++, currentFilter);
            }
            if ((currentFilter & 0x10000) != 0)
            {
                this.addFilterButton(0x10000, GFXLibrary.CardFilters_Castle, num2++, currentFilter);
                this.addFilterButton(0x10001, GFXLibrary.cardTypeButtons[0x33], GFXLibrary.cardTypeButtons[0x34], GFXLibrary.cardTypeButtons[0x35], num2++, currentFilter);
                this.addFilterButton(0x10002, GFXLibrary.cardTypeButtons[0x36], GFXLibrary.cardTypeButtons[0x37], GFXLibrary.cardTypeButtons[0x38], num2++, currentFilter);
                this.addFilterButton(0x10003, GFXLibrary.cardTypeButtons[0x39], GFXLibrary.cardTypeButtons[0x3a], GFXLibrary.cardTypeButtons[0x3b], num2++, currentFilter);
                this.addFilterButton(0x10004, GFXLibrary.cardTypeButtons[60], GFXLibrary.cardTypeButtons[0x3d], GFXLibrary.cardTypeButtons[0x3e], num2++, currentFilter);
            }
            else
            {
                this.addFilterButton(0x10000, GFXLibrary.CardFilters_Castle, num2++, currentFilter);
            }
            if ((currentFilter & 0x20000) != 0)
            {
                this.addFilterButton(0x20000, GFXLibrary.CardFilters_Army, num2++, currentFilter);
                this.addFilterButton(0x20001, GFXLibrary.cardTypeButtons[0x3f], GFXLibrary.cardTypeButtons[0x40], GFXLibrary.cardTypeButtons[0x41], num2++, currentFilter);
                this.addFilterButton(0x20002, GFXLibrary.cardTypeButtons[0x42], GFXLibrary.cardTypeButtons[0x43], GFXLibrary.cardTypeButtons[0x44], num2++, currentFilter);
                this.addFilterButton(0x20003, GFXLibrary.cardTypeButtons[0x45], GFXLibrary.cardTypeButtons[70], GFXLibrary.cardTypeButtons[0x47], num2++, currentFilter);
                this.addFilterButton(0x20004, GFXLibrary.cardTypeButtons[0x48], GFXLibrary.cardTypeButtons[0x49], GFXLibrary.cardTypeButtons[0x4a], num2++, currentFilter);
                if (GameEngine.Instance.World.NewCategoriesAvailable_Strategy)
                {
                    this.addFilterButton(0x20005, GFXLibrary.cardTypeButtons[0x4b], GFXLibrary.cardTypeButtons[0x4c], GFXLibrary.cardTypeButtons[0x4d], num2++, currentFilter);
                }
            }
            else
            {
                this.addFilterButton(0x20000, GFXLibrary.CardFilters_Army, num2++, currentFilter);
            }
            if ((currentFilter & 0x40000) != 0)
            {
                this.addFilterButton(0x40000, GFXLibrary.CardFilters_Specialist, num2++, currentFilter);
                this.addFilterButton(0x40001, GFXLibrary.cardTypeButtons[0x4e], GFXLibrary.cardTypeButtons[0x4f], GFXLibrary.cardTypeButtons[80], num2++, currentFilter);
                this.addFilterButton(0x40002, GFXLibrary.cardTypeButtons[0x51], GFXLibrary.cardTypeButtons[0x52], GFXLibrary.cardTypeButtons[0x53], num2++, currentFilter);
                this.addFilterButton(0x40003, GFXLibrary.cardTypeButtons[0x54], GFXLibrary.cardTypeButtons[0x55], GFXLibrary.cardTypeButtons[0x56], num2++, currentFilter);
                this.addFilterButton(0x40004, GFXLibrary.cardTypeButtons[0x57], GFXLibrary.cardTypeButtons[0x58], GFXLibrary.cardTypeButtons[0x59], num2++, currentFilter);
                this.addFilterButton(0x40005, GFXLibrary.cardTypeButtons[90], GFXLibrary.cardTypeButtons[0x5b], GFXLibrary.cardTypeButtons[0x5c], num2++, currentFilter);
                this.addFilterButton(0x40006, GFXLibrary.cardTypeButtons[0x5d], GFXLibrary.cardTypeButtons[0x5e], GFXLibrary.cardTypeButtons[0x5f], num2++, currentFilter);
                if (GameEngine.Instance.World.NewCategoriesAvailable_Capacity)
                {
                    this.addFilterButton(0x40007, GFXLibrary.cardTypeButtons[0x60], GFXLibrary.cardTypeButtons[0x61], GFXLibrary.cardTypeButtons[0x62], num2++, currentFilter);
                }
                this.addFilterButton(0x40008, GFXLibrary.cardTypeButtons[0x63], GFXLibrary.cardTypeButtons[100], GFXLibrary.cardTypeButtons[0x65], num2++, currentFilter);
            }
            else
            {
                this.addFilterButton(0x40000, GFXLibrary.CardFilters_Specialist, num2++, currentFilter);
            }
            if (GameEngine.Instance.World.NewCategoriesAvailable_Parish)
            {
                if ((currentFilter & 0x80000) != 0)
                {
                    this.addFilterButton(0x80000, GFXLibrary.CardFilters_Parish, num2++, currentFilter);
                }
                else
                {
                    this.addFilterButton(0x80000, GFXLibrary.CardFilters_Parish, num2++, currentFilter);
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void InitSpinners()
        {
            this.DynamicPanel.removeControl(this.SlotHolder);
            this.SlotHolder = new CustomSelfDrawPanel.CSDImage();
            this.SlotHolder.Image = (Image) GFXLibrary.CardSlotFrame;
            this.SlotHolder.Position = new Point((this.DynamicPanel.Width / 2) - (this.SlotHolder.Width / 2), (this.DynamicPanel.Height / 2) - (this.SlotHolder.Height / 2));
            this.SlotHolder.Size = GFXLibrary.CardSlotFrame.Size;
            this.DynamicPanel.addControl(this.SlotHolder);
            this.SlotHolder.Visible = false;
            for (int i = 0; i < this.SlotAnims.Length; i++)
            {
                this.SlotHolder.removeControl(this.SlotAnims[i]);
                this.SlotAnims[i] = new CustomSelfDrawPanel.CSDImageAnim();
                this.SlotAnims[i].Position = new Point(11 + (i * 0x3d), 11);
                this.SlotAnims[i].SetFrames(GFXLibrary.CardSlotAnimFrames);
                this.SlotAnims[i].Size = GFXLibrary.CardSlotAnimFrames[0].Size;
                this.SlotAnims[i].FrameData = GFXLibrary.CardSlotAnimData;
                this.SlotAnims[i].Playing = false;
                this.SlotHolder.addControl(this.SlotAnims[i]);
                this.SlotAnims[i].Visible = false;
            }
            for (int j = 0; j < this.SymbolScrollers.Length; j++)
            {
                this.DynamicPanel.removeControl(this.SymbolScrollers[j]);
                this.SymbolScrollers[j] = new CustomSelfDrawPanel.CSDVertImageScroller();
                this.SymbolScrollers[j].init(new Point(j * (GFXLibrary.cardpanel_symbol_crown.Width - 10), 0), new BaseImage[] { GFXLibrary.cardpanel_symbol_apple, GFXLibrary.cardpanel_symbol_crown, GFXLibrary.cardpanel_symbol_hawk, GFXLibrary.cardpanel_symbol_jester, GFXLibrary.cardpanel_symbol_shield, GFXLibrary.cardpanel_symbol_tower, GFXLibrary.cardpanel_symbol_wolf }, new int[] { 0x1000000, 0x40000000, 0x4000000, 0x20000000, 0x8000000, 0x10000000, 0x2000000 });
                this.DynamicPanel.addControl(this.SymbolScrollers[j]);
                this.SymbolScrollers[j].Visible = false;
            }
        }

        private void InitTabs()
        {
            this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_left;
            this.TabSelector.Size = this.TabSelector.Image.Size;
            this.TabSelector.Position = new Point((this.AvailablePanel.X + this.AvailablePanel.Width) - this.TabSelector.Width, (this.DynamicPanel.Y - this.TabSelector.Height) + 6);
            this.mainBackgroundImage.addControl(this.TabSelector);
            this.TabCashArea.Position = new Point(0x4f, 0);
            this.TabCashArea.Size = new Size(0x76, 30);
            this.TabCashArea.CustomTooltipID = 0x2777;
            this.TabSelector.addControl(this.TabCashArea);
            this.TabBuyArea.Position = new Point(0xc4, 0);
            this.TabBuyArea.Size = new Size(0x76, 30);
            this.TabBuyArea.CustomTooltipID = 0x2778;
            this.TabSelector.addControl(this.TabBuyArea);
            this.TabSelector.ClickArea = new Rectangle(0xc4, 0, 0x76, 30);
            this.TabSelector.setClickDelegate(delegate {
                if (!this.cashingIn && !this.buyingCard)
                {
                    if (this.PanelMode == PANEL_MODE_CASH)
                    {
                        GameEngine.Instance.playInterfaceSound("ManageCardsPanel_switch_to_buy_cards");
                        this.SwitchToBuy();
                        this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_right;
                        this.TabSelector.ClickArea = new Rectangle(0x4f, 0, 0x76, 30);
                    }
                    else
                    {
                        GameEngine.Instance.playInterfaceSound("ManageCardsPanel_switch_to_cash_in");
                        this.SwitchToCash();
                        this.TabSelector.Image = (Image) GFXLibrary.cardpanel_manage_tabs_white_left;
                        this.TabSelector.ClickArea = new Rectangle(0xc4, 0, 0x76, 30);
                    }
                }
            });
        }

        private CustomSelfDrawPanel.UICard makeUICard(CardTypes.CardDefinition def, int userid, int playerRank)
        {
            Color red;
            CustomSelfDrawPanel.UICard card = new CustomSelfDrawPanel.UICard {
                UserID = userid
            };
            card.UserIDList.Add(userid);
            card.Definition = def;
            switch (card.Definition.cardColour)
            {
                case 1:
                    card.bigFrame = GFXLibrary.BlueCardOverlayBig;
                    card.bigFrameOver = GFXLibrary.BlueCardOverlayBigOver;
                    break;

                case 2:
                    card.bigFrame = GFXLibrary.GreenCardOverlayBig;
                    card.bigFrameOver = GFXLibrary.GreenCardOverlayBigOver;
                    break;

                case 3:
                    card.bigFrame = GFXLibrary.PurpleCardOverlayBig;
                    card.bigFrameOver = GFXLibrary.PurpleCardOverlayBigOver;
                    break;

                case 4:
                    card.bigFrame = GFXLibrary.RedCardOverlayBig;
                    card.bigFrameOver = GFXLibrary.RedCardOverlayBigOver;
                    break;

                case 5:
                    card.bigFrame = GFXLibrary.YellowCardOverlayBig;
                    card.bigFrameOver = GFXLibrary.YellowCardOverlayBigOver;
                    break;

                default:
                    card.bigFrame = GFXLibrary.GreenCardOverlayBig;
                    card.bigFrameOver = GFXLibrary.GreenCardOverlayBigOver;
                    break;
            }
            card.bigImage = GFXLibrary.Instance.getCardImageBig(card.Definition.id);
            card.Size = card.bigFrame.Size;
            card.CustomTooltipID = 0x2775;
            card.CustomTooltipData = card.Definition.id;
            card.bigGradeImage = new CustomSelfDrawPanel.CSDImage();
            int num = CardTypes.getGrade(card.Definition.cardGrade);
            switch (num)
            {
                case 0x10000:
                    card.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
                    card.bigGradeImage.Position = new Point(card.Width - card.bigGradeImage.Width, 0);
                    break;

                case 0x20000:
                    card.bigGradeImage.Image = (Image) GFXLibrary.CardGradeSilver;
                    card.bigGradeImage.Position = new Point(card.Width - card.bigGradeImage.Width, 0);
                    break;

                case 0x40000:
                    card.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[0];
                    card.bigGradeImage.Position = new Point((card.Width - card.bigGradeImage.Width) - 3, 0);
                    break;

                case 0x200000:
                    card.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[0];
                    card.bigGradeImage.Position = new Point((card.Width - card.bigGradeImage.Width) - 3, -10);
                    break;

                case 0x400000:
                    card.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[0];
                    card.bigGradeImage.Position = new Point((card.Width - card.bigGradeImage.Width) - 3, -12);
                    break;

                case 0x80000:
                    card.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[0];
                    card.bigGradeImage.Position = new Point((card.Width - card.bigGradeImage.Width) - 3, -2);
                    break;

                case 0x100000:
                    card.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[0];
                    card.bigGradeImage.Position = new Point((card.Width - card.bigGradeImage.Width) - 3, -7);
                    break;

                default:
                    card.bigGradeImage.Image = (Image) GFXLibrary.CardGradeBronze;
                    card.bigGradeImage.Position = new Point(card.Width - card.bigGradeImage.Width, 0);
                    break;
            }
            card.bigBaseImage = new CustomSelfDrawPanel.CSDImage();
            card.bigBaseImage.Position = new Point(10, 11);
            card.bigBaseImage.Size = card.bigImage.Size;
            card.bigBaseImage.Image = (Image) card.bigImage;
            card.addControl(card.bigBaseImage);
            card.bigFrameImage = new CustomSelfDrawPanel.CSDImage();
            card.bigFrameImage.Position = new Point(0, 0);
            card.bigFrameImage.Size = card.bigFrame.Size;
            card.bigFrameImage.Image = (Image) card.bigFrame;
            card.addControl(card.bigFrameImage);
            switch (num)
            {
                case 0x40000:
                    card.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                    card.bigFrameExtraImage.Position = new Point(0, 0);
                    card.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_gold;
                    card.addControl(card.bigFrameExtraImage);
                    break;

                case 0x80000:
                case 0x100000:
                case 0x200000:
                    card.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                    card.bigFrameExtraImage.Position = new Point(0, 0);
                    card.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_diamond;
                    card.addControl(card.bigFrameExtraImage);
                    break;

                case 0x400000:
                    card.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                    card.bigFrameExtraImage.Position = new Point(0, 0);
                    card.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_sapphire;
                    card.addControl(card.bigFrameExtraImage);
                    break;
            }
            card.bigGradeImage.Size = card.bigGradeImage.Image.Size;
            card.addControl(card.bigGradeImage);
            card.bigTitle = new CustomSelfDrawPanel.CSDLabel();
            card.bigTitle.Text = CardTypes.getDescriptionFromCard(card.Definition.id);
            card.bigTitle.Size = new Size(110, 0x30);
            card.bigTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            if (((((card.Definition.id == 0x709) || (card.Definition.id == 0x606)) || ((card.Definition.id == 0xc41) || (card.Definition.id == 0x50a))) || ((card.Definition.id == 0x605) || (card.Definition.id == 0x607))) && (Program.mySettings.LanguageIdent == "de"))
            {
                card.bigTitle.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            }
            else
            {
                card.bigTitle.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            }
            card.bigTitle.Color = ARGBColors.White;
            card.bigTitle.DropShadowColor = ARGBColors.Black;
            card.bigTitle.Position = new Point(0x26, 12);
            card.addControl(card.bigTitle);
            card.bigEffect = new CustomSelfDrawPanel.CSDLabel();
            card.bigEffect.Text = card.Definition.EffectText;
            card.bigEffect.Size = new Size(150, 0x40);
            card.bigEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            card.bigEffect.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            if ((Program.mySettings.LanguageIdent == "de") && CardTypes.isGermanSmallDesc(card.Definition.id))
            {
                card.bigEffect.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            }
            card.bigEffect.Color = ARGBColors.White;
            card.bigEffect.DropShadowColor = ARGBColors.Black;
            card.bigEffect.Position = new Point(14, 0xae);
            card.addControl(card.bigEffect);
            CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(2, 2),
                Size = new Size(card.Width, card.Height),
                Text = "",
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT,
                Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold),
                Color = ARGBColors.Yellow,
                DropShadowColor = ARGBColors.Black
            };
            card.addControl(control);
            card.countLabel = control;
            if (playerRank < card.Definition.cardRank)
            {
                red = ARGBColors.Red;
            }
            else
            {
                red = ARGBColors.White;
            }
            CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                Position = new Point(150, 220),
                Size = new Size(20, 13),
                Text = card.Definition.cardRank.ToString(),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold),
                Color = red,
                DropShadowColor = ARGBColors.Black
            };
            card.addControl(label2);
            card.rankLabel = label2;
            if (playerRank < card.Definition.cardRank)
            {
                card.Hilight(ARGBColors.Gray);
            }
            else
            {
                card.Hilight(ARGBColors.White);
            }
            card.ScaleAll(0.95);
            return card;
        }

        private void mouseWheelHandler(int delta)
        {
            if ((((delta > 0) && ((this.scrollbarAvailable.Value - (delta * 15)) > 0)) || ((delta < 0) && ((this.scrollbarAvailable.Value - (delta * 15)) < this.scrollbarAvailable.Max))) && (!this.cashingIn && !this.buyingCard))
            {
                this.scrollbarAvailable.Value += delta * -15;
                this.AvailableContentScroll();
            }
        }

        private void MultipleCallback(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode == 1)
            {
                string str = response.Strings.TrimEnd(",".ToCharArray());
                string[] strArray = this.newcardnames.Split(",".ToCharArray());
                string[] strArray2 = str.Split(",".ToCharArray());
                for (int i = 0; i < strArray.Length; i++)
                {
                    int key = Convert.ToInt32(strArray2[(strArray.Length - 1) - i].Trim());
                    GameEngine.Instance.World.ProfileCards.Add(key, CardTypes.getCardDefinitionFromString(strArray[i].Trim()));
                    if (GameEngine.Instance.World.ProfileCards[key].id == 0xc29)
                    {
                        GameEngine.Instance.World.handleQuestObjectiveHappening(0x2717);
                    }
                }
                if (GameEngine.Instance.World.getTutorialStage() == 0x66)
                {
                    GameEngine.Instance.World.FakeCardPoints = 0;
                }
                else
                {
                    WorldMap world = GameEngine.Instance.World;
                    world.ProfileCardpoints -= this.newcardcost;
                }
                this.labelTitle.Text = SK.Text("ManageCandsPanel_Get_New_Cards_Points", "Get New Cards: Current Card Points");
                this.addPointsData();
                if (GameEngine.Instance.World.FakeCardPoints > 0)
                {
                    this.labelTitlePoints.Text = this.labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
                }
                if (GameEngine.Instance.World.getTutorialStage() == 0x66)
                {
                    this.closeClick();
                }
                if ((strArray2.Length == 1) && this.buyAndPlayCheckBox.Checked)
                {
                    int userID = Convert.ToInt32(strArray2[0].Trim());
                    this.autoPlayCard(userID, CardTypes.getCardDefinitionFromString(strArray[0].Trim()), true, false);
                }
            }
            else
            {
                MyMessageBox.Show(response.Message, SK.Text("GENERIC_Error", "Error"));
            }
            this.cardsButtons.Available = true;
            this.buyingCard = false;
            GameEngine.Instance.World.ShoppingCartCards.Clear();
            this.RefreshCart();
            this.LabelClickToRemove.Text = "";
        }

        private void Navigate(int panelType)
        {
            ((PlayCardsWindow) base.ParentForm).SwitchPanel(panelType);
        }

        public void navigateTest()
        {
            this.Navigate(2);
        }

        public void NewFilterClick()
        {
            if (!this.cashingIn && !this.buyingCard)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                if (this.PanelMode == PANEL_MODE_CASH)
                {
                    CardTypes.CardDefinition filter = new CardTypes.CardDefinition {
                        newCardCategoryFilter = data
                    };
                    this.CatalogFilterDefinition = new CardTypes.CardDefinition();
                    this.CatalogFilterDefinition.newCardCategoryFilter = data;
                    if ((data & 0xff) == 0)
                    {
                        if (!this.searchButton.Visible && !((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible)
                        {
                            this.searchButton.Visible = true;
                        }
                        ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = !this.searchButton.Visible;
                    }
                    else
                    {
                        ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = false;
                        this.searchButton.Visible = false;
                    }
                    GameEngine.Instance.World.searchProfileCards(filter, "", ((PlayCardsWindow) base.ParentForm).getNameSearchText());
                    this.SwitchToCash();
                }
                else if (this.PanelMode == PANEL_MODE_BUY)
                {
                    this.CatalogFilterDefinition = new CardTypes.CardDefinition();
                    this.CatalogFilterDefinition.newCardCategoryFilter = data;
                    CardTypes.CardDefinition definition2 = new CardTypes.CardDefinition {
                        newCardCategoryFilter = data
                    };
                    if ((data & 0xff) == 0)
                    {
                        if (!this.searchButton.Visible && !((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible)
                        {
                            this.searchButton.Visible = true;
                        }
                        ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = !this.searchButton.Visible;
                    }
                    else
                    {
                        ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = false;
                        this.searchButton.Visible = false;
                    }
                    GameEngine.Instance.World.lastUserCardSearchCriteria = definition2;
                    GameEngine.Instance.World.lastUserCardNameFilter = ((PlayCardsWindow) base.ParentForm).getNameSearchText();
                    this.SwitchToBuy();
                }
                this.clearSearchButton.Visible = ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible;
            }
        }

        public void preValidateCardToBePlayedCallBack(PreValidateCardToBePlayed_ReturnType returnData)
        {
            this.waitingResponse = false;
            if (returnData.Success)
            {
                if ((CardTypes.isMercenaryTroopCardType(returnData.cardType) && (returnData.otherErrorCode == 0x270f)) && (MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_UNIT_SPACE", "There is not enough unit space to accomodate these troops. If troops are dispatched from this village some may be lost upon their return.") + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) == DialogResult.No))
                {
                    return;
                }
                if (returnData.canPlayFully)
                {
                    this.autoPlayCardDelegate(false, true);
                    return;
                }
                if (!returnData.canPlayPartially)
                {
                    if (returnData.otherErrorCode != 0)
                    {
                        if (returnData.otherErrorCode == -2)
                        {
                            MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnData.cardType, 5), SK.Text("GENERIC_Error", "Error"));
                        }
                        else if (returnData.otherErrorCode == -3)
                        {
                            GameEngine.Instance.displayedVillageLost(returnData.villageID, true);
                        }
                    }
                    else
                    {
                        switch (returnData.cardType)
                        {
                            case 0xc0d:
                            case 0xc0e:
                            case 0xc0f:
                            case 0xc10:
                            case 0xc11:
                            case 0xc12:
                            case 0xc13:
                            case 0xc14:
                            case 0xc15:
                            case 0xc16:
                            case 0xc17:
                            case 0xc18:
                            case 0xc19:
                            case 0xc1a:
                            case 0xc1b:
                            case 0xc1c:
                            case 0xc1d:
                            case 0xc1e:
                            case 0xc1f:
                            case 0xc20:
                            case 0xc21:
                            case 0xc22:
                            case 0xc23:
                            case 0xc24:
                                MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_101", "Not enough space in the Granary."), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xc25:
                            case 0xc26:
                            case 0xc27:
                            case 0xc28:
                                MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_102", "Not enough space in the Inn."), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xc29:
                            case 0xc2a:
                            case 0xc2b:
                            case 0xc2c:
                            case 0xc2d:
                            case 0xc2e:
                            case 0xc2f:
                            case 0xc30:
                            case 0xc31:
                            case 0xc32:
                            case 0xc33:
                            case 0xc34:
                            case 0xc35:
                            case 0xc36:
                            case 0xc37:
                            case 0xc38:
                                MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_103", "Not enough space on the Stockpile."), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xc39:
                            case 0xc3a:
                            case 0xc3b:
                            case 0xc3c:
                            case 0xc3d:
                            case 0xc3e:
                            case 0xc3f:
                            case 0xc40:
                            case 0xc41:
                            case 0xc42:
                            case 0xc43:
                            case 0xc44:
                            case 0xc45:
                            case 0xc46:
                            case 0xc47:
                            case 0xc48:
                            case 0xc49:
                            case 0xc4a:
                            case 0xc4b:
                            case 0xc4c:
                            case 0xc4d:
                            case 0xc4e:
                            case 0xc4f:
                            case 0xc50:
                            case 0xc51:
                            case 0xc52:
                            case 0xc53:
                            case 0xc54:
                            case 0xc55:
                            case 0xc56:
                            case 0xc57:
                            case 0xc58:
                                MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_104", "Not enough space in the Village Hall."), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xc59:
                            case 0xc5a:
                            case 0xc5b:
                            case 0xc5c:
                            case 0xc5d:
                            case 0xc5e:
                            case 0xc5f:
                            case 0xc60:
                            case 0xc61:
                            case 0xc62:
                            case 0xc63:
                            case 0xc64:
                            case 0xc65:
                            case 0xc66:
                            case 0xc67:
                            case 0xc68:
                            case 0xc69:
                            case 0xc6a:
                            case 0xc6b:
                            case 0xc6c:
                                MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_105", "Not enough space in the Armoury."), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xcc0:
                            case 0xcc1:
                            case 0xcc2:
                            case 0xcc3:
                            case 0xcc4:
                            case 0xcc5:
                            case 0xcc6:
                            case 0xcc7:
                            case 0xcc8:
                            case 0xcc9:
                            case 0xcca:
                            case 0xccb:
                            case 0xccc:
                            case 0xccd:
                            case 0xcce:
                            case 0xccf:
                            case 0xcd0:
                            case 0xcd1:
                            case 0xcd2:
                            case 0xcd3:
                                MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnData.cardType, 1), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xcd7:
                            case 0xcd8:
                            case 0xcd9:
                                MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnData.cardType, 2), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xcda:
                            case 0xcdb:
                            case 0xcdc:
                                MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnData.cardType, 3), SK.Text("GENERIC_Error", "Error"));
                                break;

                            case 0xcdd:
                            case 0xcde:
                            case 0xcdf:
                                MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnData.cardType, 4), SK.Text("GENERIC_Error", "Error"));
                                break;
                        }
                    }
                }
                else
                {
                    string str2 = "";
                    switch (returnData.cardType)
                    {
                        case 0xc0d:
                        case 0xc0e:
                        case 0xc0f:
                        case 0xc10:
                        case 0xc11:
                        case 0xc12:
                        case 0xc13:
                        case 0xc14:
                        case 0xc15:
                        case 0xc16:
                        case 0xc17:
                        case 0xc18:
                        case 0xc19:
                        case 0xc1a:
                        case 0xc1b:
                        case 0xc1c:
                        case 0xc1d:
                        case 0xc1e:
                        case 0xc1f:
                        case 0xc20:
                        case 0xc21:
                        case 0xc22:
                        case 0xc23:
                        case 0xc24:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_5", "Amount of Food gained will be") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xc25:
                        case 0xc26:
                        case 0xc27:
                        case 0xc28:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_6", "Amount of Ale gained will be") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xc29:
                        case 0xc2a:
                        case 0xc2b:
                        case 0xc2c:
                        case 0xc2d:
                        case 0xc2e:
                        case 0xc2f:
                        case 0xc30:
                        case 0xc31:
                        case 0xc32:
                        case 0xc33:
                        case 0xc34:
                        case 0xc35:
                        case 0xc36:
                        case 0xc37:
                        case 0xc38:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_7", "Amount of Resources gained will be") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xc39:
                        case 0xc3a:
                        case 0xc3b:
                        case 0xc3c:
                        case 0xc3d:
                        case 0xc3e:
                        case 0xc3f:
                        case 0xc40:
                        case 0xc41:
                        case 0xc42:
                        case 0xc43:
                        case 0xc44:
                        case 0xc45:
                        case 0xc46:
                        case 0xc47:
                        case 0xc48:
                        case 0xc49:
                        case 0xc4a:
                        case 0xc4b:
                        case 0xc4c:
                        case 0xc4d:
                        case 0xc4e:
                        case 0xc4f:
                        case 0xc50:
                        case 0xc51:
                        case 0xc52:
                        case 0xc53:
                        case 0xc54:
                        case 0xc55:
                        case 0xc56:
                        case 0xc57:
                        case 0xc58:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_8", "Amount of Honour Goods gained will be") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xc59:
                        case 0xc5a:
                        case 0xc5b:
                        case 0xc5c:
                        case 0xc5d:
                        case 0xc5e:
                        case 0xc5f:
                        case 0xc60:
                        case 0xc65:
                        case 0xc66:
                        case 0xc67:
                        case 0xc68:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_9", "Number of Weapons gained will be") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xc61:
                        case 0xc62:
                        case 0xc63:
                        case 0xc64:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_10", "Amount of Armour gained will be") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xc69:
                        case 0xc6a:
                        case 0xc6b:
                        case 0xc6c:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_11", "Number of Catapults gained will be") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xcc0:
                        case 0xcc1:
                        case 0xcc2:
                        case 0xcc3:
                        case 0xcc4:
                        case 0xcc5:
                        case 0xcc6:
                        case 0xcc7:
                        case 0xcc8:
                        case 0xcc9:
                        case 0xcca:
                        case 0xccb:
                        case 0xccc:
                        case 0xccd:
                        case 0xcce:
                        case 0xccf:
                        case 0xcd0:
                        case 0xcd1:
                        case 0xcd2:
                        case 0xcd3:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_1", "Number of Troops that can be recruited") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xcd7:
                        case 0xcd8:
                        case 0xcd9:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_2", "Number of Scouts that can be recruited") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xcda:
                        case 0xcdb:
                        case 0xcdc:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_3", "Number of Monks that can be recruited") + " : " + returnData.numCanPlay.ToString();
                            break;

                        case 0xcdd:
                        case 0xcde:
                        case 0xcdf:
                            str2 = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnData.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_4", "Number of Merchants that can be recruited") + " : " + returnData.numCanPlay.ToString();
                            break;
                    }
                    if (MyMessageBox.Show(str2 + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.autoPlayCardDelegate(false, true);
                        return;
                    }
                }
            }
            UniversalDebugLog.Log("Failed");
        }

        private int RefreshCards(CustomSelfDrawPanel.CSDImage content, List<CustomSelfDrawPanel.UICard> list, int width)
        {
            int[] numArray = null;
            if (GameEngine.Instance.World.lastUserCardSearchCriteria != null)
            {
                switch (GameEngine.Instance.World.lastUserCardSearchCriteria.newCardCategoryFilter)
                {
                    case 0x1001:
                        numArray = CardTypes.newCategories_ApplesOrder;
                        break;

                    case 0x1002:
                        numArray = CardTypes.newCategories_CheeseOrder;
                        break;

                    case 0x1003:
                        numArray = CardTypes.newCategories_MeatOrder;
                        break;

                    case 0x1004:
                        numArray = CardTypes.newCategories_BreadOrder;
                        break;

                    case 0x1005:
                        numArray = CardTypes.newCategories_VegOrder;
                        break;

                    case 0x1006:
                        numArray = CardTypes.newCategories_FishOrder;
                        break;

                    case 0x1007:
                        numArray = CardTypes.newCategories_AleOrder;
                        break;

                    case 0x2001:
                        numArray = CardTypes.newCategories_WoodOrder;
                        break;

                    case 0x2002:
                        numArray = CardTypes.newCategories_StoneOrder;
                        break;

                    case 0x2003:
                        numArray = CardTypes.newCategories_IronOrder;
                        break;

                    case 0x2004:
                        numArray = CardTypes.newCategories_PitchOrder;
                        break;

                    case 0x4001:
                        numArray = CardTypes.newCategories_VenisonOrder;
                        break;

                    case 0x4002:
                        numArray = CardTypes.newCategories_FurnitureOrder;
                        break;

                    case 0x4003:
                        numArray = CardTypes.newCategories_MetalwareOrder;
                        break;

                    case 0x4004:
                        numArray = CardTypes.newCategories_ClothesOrder;
                        break;

                    case 0x4005:
                        numArray = CardTypes.newCategories_WineOrder;
                        break;

                    case 0x4006:
                        numArray = CardTypes.newCategories_SaltOrder;
                        break;

                    case 0x4007:
                        numArray = CardTypes.newCategories_SpicesOrder;
                        break;

                    case 0x4008:
                        numArray = CardTypes.newCategories_SilkOrder;
                        break;

                    case 0x8001:
                        numArray = CardTypes.newCategories_BowsOrder;
                        break;

                    case 0x8002:
                        numArray = CardTypes.newCategories_PikesOrder;
                        break;

                    case 0x8003:
                        numArray = CardTypes.newCategories_ArmourOrder;
                        break;

                    case 0x8004:
                        numArray = CardTypes.newCategories_SwordsOrder;
                        break;

                    case 0x8005:
                        numArray = CardTypes.newCategories_CatapultsOrder;
                        break;

                    case 0x10001:
                        numArray = CardTypes.newCategories_CastleConOrder;
                        break;

                    case 0x10002:
                        numArray = CardTypes.newCategories_DefencesOrder;
                        break;

                    case 0x10003:
                        numArray = CardTypes.newCategories_WallsOrder;
                        break;

                    case 0x10004:
                        numArray = CardTypes.newCategories_KnightsOrder;
                        break;

                    case 0x20001:
                        numArray = CardTypes.newCategories_ScoutingOrder;
                        break;

                    case 0x20002:
                        numArray = CardTypes.newCategories_SpeedOrder;
                        break;

                    case 0x20003:
                        numArray = CardTypes.newCategories_RecruitmentOrder;
                        break;

                    case 0x20004:
                        numArray = CardTypes.newCategories_TroopsOrder;
                        break;

                    case 0x20005:
                        numArray = CardTypes.newCategories_DiplomacyOrder;
                        break;

                    case 0x40001:
                        numArray = CardTypes.newCategories_TradeOrder;
                        break;

                    case 0x40002:
                        numArray = CardTypes.newCategories_ReligionOrder;
                        break;

                    case 0x40003:
                        numArray = CardTypes.newCategories_HonourOrder;
                        break;

                    case 0x40004:
                        numArray = CardTypes.newCategories_GoldOrder;
                        break;

                    case 0x40005:
                        numArray = CardTypes.newCategories_PopOrder;
                        break;

                    case 0x40006:
                        numArray = CardTypes.newCategories_ResearchOrder;
                        break;

                    case 0x40007:
                        numArray = CardTypes.newCategories_CapacityOrder;
                        break;

                    case 0x40008:
                        numArray = CardTypes.newCategories_ConstructionOrder;
                        break;
                }
            }
            if (this.sortByMode == 0)
            {
                list.Sort(CustomSelfDrawPanel.UICard.cardsNameComparer);
            }
            else if (this.sortByMode == 1)
            {
                list.Sort(CustomSelfDrawPanel.UICard.cardsIDComparer);
            }
            else if (this.sortByMode == 2)
            {
                list.Sort(CustomSelfDrawPanel.UICard.cardsNameComparerReverse);
            }
            else if (this.sortByMode == 3)
            {
                list.Sort(CustomSelfDrawPanel.UICard.cardsIDComparerReverse);
            }
            else if (this.sortByMode == 7)
            {
                if (this.PanelMode == PANEL_MODE_BUY)
                {
                    list.Sort(CustomSelfDrawPanel.UICard.cardsPriceComparer);
                }
                else
                {
                    list.Sort(CustomSelfDrawPanel.UICard.cardsQuantityComparer);
                }
            }
            else if (this.sortByMode == 8)
            {
                if (this.PanelMode == PANEL_MODE_BUY)
                {
                    list.Sort(CustomSelfDrawPanel.UICard.cardsPriceComparerReverse);
                }
                else
                {
                    list.Sort(CustomSelfDrawPanel.UICard.cardsQuantityComparerReverse);
                }
            }
            int num = GameEngine.Instance.World.getRank() + 1;
            content.clearDirectControlsOnly();
            foreach (CustomSelfDrawPanel.UICard card in this.dummyCards)
            {
                card.clearControls();
            }
            this.dummyCards.Clear();
            int num2 = 0;
            if (numArray != null)
            {
                this.sortBack.Visible = false;
                this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanel.Height - (BorderPadding * 2));
                int num3 = 0;
                int num4 = -1;
                for (int i = 0; i < numArray.Length; i += 3)
                {
                    if (numArray[i + 2] != num4)
                    {
                        int cardType = numArray[i];
                        int x = numArray[i + 1] * 0xb2;
                        int y = (numArray[i + 2] - num3) * 0xed;
                        bool flag = false;
                        CustomSelfDrawPanel.UICard control = null;
                        foreach (CustomSelfDrawPanel.UICard card3 in list)
                        {
                            if (CardTypes.getCardType(card3.Definition.id) == cardType)
                            {
                                flag = true;
                                control = card3;
                            }
                        }
                        CardTypes.CardDefinition def = CardTypes.getCardDefinition(cardType);
                        if ((!flag && (((def.cardRank <= 0) || (def.cardRarity <= 0)) || ((def.available != 1) || ((def.cardPoints <= 0) && (this.LayoutPanelMode == PANEL_MODE_BUY))))) && (x == 0))
                        {
                            bool flag2 = false;
                            int num9 = CardTypes.getCardType(cardType);
                            if ((num9 >= 0xbd7) && (num9 <= 0xbf5))
                            {
                                for (int j = 0; j < numArray.Length; j += 3)
                                {
                                    if (((numArray[j + 2] == numArray[i + 2]) && (cardType != numArray[j])) && (CardTypes.getCardDefinition(numArray[j]).available == 1))
                                    {
                                        numArray[j + 1]--;
                                        flag2 = true;
                                    }
                                }
                            }
                            if (!flag2)
                            {
                                num3++;
                                num4 = numArray[i + 2];
                                continue;
                            }
                        }
                        if ((y + 0xed) > num2)
                        {
                            num2 = y + 0xed;
                        }
                        if (flag)
                        {
                            control.Position = new Point(x, y);
                            content.addControl(control);
                            if (num < control.Definition.cardRank)
                            {
                                control.rankLabel.Color = ARGBColors.Red;
                                control.Hilight(ARGBColors.Gray);
                            }
                            else
                            {
                                control.rankLabel.Color = ARGBColors.White;
                                control.Hilight(ARGBColors.White);
                            }
                            if (control.cardCount > 1)
                            {
                                control.countLabel.Text = control.cardCount.ToString();
                                if (control.cardCount >= 100)
                                {
                                    control.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                                }
                                else
                                {
                                    control.countLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                                }
                            }
                        }
                        else if ((((def.cardRank > 0) && (def.cardRarity > 0)) && (def.available == 1)) && ((def.cardPoints > 0) || (this.LayoutPanelMode != PANEL_MODE_BUY)))
                        {
                            CustomSelfDrawPanel.UICard card4 = BuyCardsPanel.makeUICard(def, RemoteServices.Instance.UserID, 0x2710);
                            card4.Position = new Point(x, y);
                            content.addControl(card4);
                            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                                FillColor = Color.FromArgb(170, 0, 0, 0),
                                Alpha = 0.2f,
                                Position = new Point(2, 1),
                                Size = new Size((card4.Size.Width - 2) - 4, (card4.Size.Height - 1) - 5)
                            };
                            card4.addControl(fill);
                            this.dummyCards.Add(card4);
                            card4.CustomTooltipID = 0x2775;
                            card4.CustomTooltipData = cardType;
                            CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel {
                                Text = SK.Text("CARDS_No_Cards", "No Cards"),
                                Position = new Point(x + 3, y + 5),
                                Size = new Size(0x9d, 0xd9),
                                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                                Color = ARGBColors.White,
                                CustomTooltipID = 0x2775,
                                CustomTooltipData = cardType
                            };
                            content.addControl(label);
                        }
                    }
                }
            }
            else
            {
                this.sortBack.Visible = true;
                this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, (this.AvailablePanel.Height - (BorderPadding * 2)) - 20);
                int num11 = 0;
                int num12 = 0;
                int num13 = 0;
                int num14 = 0;
                foreach (CustomSelfDrawPanel.UICard card5 in list)
                {
                    card5.Position = new Point(num11, num12);
                    content.addControl(card5);
                    num14 = num12;
                    if (num11 > width)
                    {
                        num11 = 0;
                        if (!this.compressedCards)
                        {
                            num12 += card5.Height + 8;
                        }
                        else
                        {
                            num12 += 0x3a;
                        }
                    }
                    else
                    {
                        num11 += card5.Width + 12;
                    }
                    if (this.compressedCards && (num13 < (list.Count - 4)))
                    {
                        card5.ClipRect = new Rectangle(0, 0, card5.Width, 60);
                        card5.bigEffect.Visible = false;
                        card5.rankLabel.Visible = false;
                    }
                    else
                    {
                        card5.ClipRect = Rectangle.Empty;
                        card5.bigEffect.Visible = true;
                        card5.rankLabel.Visible = true;
                    }
                    if (this.compressedCards)
                    {
                        CustomSelfDrawPanel.CSDLine line = new CustomSelfDrawPanel.CSDLine {
                            Position = new Point(card5.Position.X + 3, card5.Position.Y + 1),
                            Size = new Size(card5.Width - 7, 0),
                            LineColor = Color.FromArgb(0x80, ARGBColors.Black)
                        };
                        content.addControl(line);
                    }
                    num13++;
                    if (num < card5.Definition.cardRank)
                    {
                        card5.rankLabel.Color = ARGBColors.Red;
                        card5.Hilight(ARGBColors.Gray);
                    }
                    else
                    {
                        card5.rankLabel.Color = ARGBColors.White;
                        card5.Hilight(ARGBColors.White);
                    }
                    if (card5.cardCount > 1)
                    {
                        card5.countLabel.Text = card5.cardCount.ToString();
                        if (card5.cardCount >= 100)
                        {
                            card5.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                        }
                        else
                        {
                            card5.countLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                        }
                    }
                }
                if (list.Count > 0)
                {
                    num2 = (num14 + list[0].Height) + 8;
                }
            }
            content.invalidate();
            return num2;
        }

        private void RefreshCart()
        {
            foreach (CustomSelfDrawPanel.UICard card in this.ShoppingCart)
            {
                this.mainBackgroundImage.removeControl(card);
            }
            this.ShoppingCart.Clear();
            int userid = 0;
            int num2 = 0;
            foreach (int num3 in GameEngine.Instance.World.ShoppingCartCards)
            {
                CustomSelfDrawPanel.UICard newcard = this.makeUICard(CardTypes.getCardDefinition(num3), userid, GameEngine.Instance.World.getRank() + 1);
                newcard.setMouseOverDelegate(delegate {
                    this.LabelClickToRemove.Text = this.TextRemove + CardTypes.getDescriptionFromCard(newcard.Definition.id);
                    newcard.MouseOver();
                }, delegate {
                    this.LabelClickToRemove.Text = "";
                    newcard.MouseOut();
                });
                this.mainBackgroundImage.addControl(newcard);
                newcard.ScaleAll(0.5);
                newcard.Position = new Point(Convert.ToInt32(Math.Floor((double) (this.EmptyCards[0].X * 0.5))) + (userid * 0x10), Convert.ToInt32(Math.Floor((double) (this.EmptyCards[0].Y * 0.5))));
                newcard.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardUncart), "ManageCardsPanel_un_purchase_card");
                newcard.Visible = true;
                this.ShoppingCart.Add(newcard);
                userid++;
                num2 += newcard.Definition.cardPoints;
            }
            GFXLibrary.Instance.closeBigCardsLoader();
            if (this.ShoppingCart.Count == 0)
            {
                this.DynamicLabel.Color = ARGBColors.Black;
                this.DynamicLabel.setClickDelegate(null);
                this.DynamicLabel.setMouseOverDelegate(null, null);
                this.DynamicLabel.Text = this.TextCartEmpty;
                this.DynamicButton.Visible = false;
                this.DynamicButtonLabel.Visible = false;
                this.DynamicLabel.Visible = true;
            }
            else
            {
                this.DynamicLabel.setClickDelegate(null);
                this.DynamicLabel.setMouseOverDelegate(null, null);
                this.DynamicLabel.Text = "";
                this.DynamicLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + SK.Text("ManageCandsPanel_Cards_Points_Value", "Card Point Value") + " : " + num2.ToString();
                this.DynamicButton.Visible = true;
                this.DynamicButtonLabel.Visible = true;
                this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
                this.DynamicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickBuyMultiple), "ManageCardsPanel_get_cards");
            }
            this.buyAndPlayCheckBox.Visible = (this.ShoppingCart.Count == 1) && (GameEngine.Instance.World.getTutorialStage() != 0x66);
            this.buyAndPlayCheckBox.Checked = false;
            this.mainBackgroundImage.invalidate();
        }

        private void RefreshSet()
        {
            for (int i = 0x3b; i >= 0; i--)
            {
                if (this.SetCards[i] != null)
                {
                    this.mainBackgroundImage.removeControl(this.SetCards[i]);
                }
                if (GameEngine.Instance.World.ProfileCardsSet.Count > i)
                {
                    this.SetCards[i] = this.makeUICard(GameEngine.Instance.World.ProfileCards[GameEngine.Instance.World.ProfileCardsSet[i]], GameEngine.Instance.World.ProfileCardsSet[i], GameEngine.Instance.World.getRank() + 1);
                    this.mainBackgroundImage.addControl(this.SetCards[i]);
                    this.SetCards[i].ScaleAll(0.5);
                    int x = Convert.ToInt32(Math.Floor((double) (this.EmptyCards[i % 5].X * 0.5))) + (i / 5);
                    int y = Convert.ToInt32(Math.Floor((double) (this.EmptyCards[i % 5].Y * 0.5))) - ((i / 5) * 2);
                    this.SetCards[i].Position = new Point(x, y);
                    this.SetCards[i].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClickCardUnset), "ManageCardsPanel_remove_card");
                    CustomSelfDrawPanel.UICard deelgatecard = this.SetCards[i];
                    this.SetCards[i].setMouseOverDelegate(delegate {
                        this.LabelClickToRemove.Text = this.TextRemoveSet + CardTypes.getDescriptionFromCard(deelgatecard.Definition.id);
                        deelgatecard.MouseOver();
                    }, delegate {
                        this.LabelClickToRemove.Text = "";
                        deelgatecard.MouseOut();
                    });
                    this.SetCards[i].Visible = true;
                    if (i < 5)
                    {
                        this.EmptyCards[i].Visible = false;
                    }
                }
                else
                {
                    this.SetCards[i].Visible = false;
                    if (i < 5)
                    {
                        this.EmptyCards[i].Visible = true;
                    }
                }
            }
            GFXLibrary.Instance.closeBigCardsLoader();
            this.labelTitle.Text = SK.Text("ManageCandsPanel_Cash_In_Cards_Title", "Cash in Cards: Current Card Points");
            this.addPointsData();
            if (GameEngine.Instance.World.FakeCardPoints > 0)
            {
                this.labelTitlePoints.Text = this.labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
            }
            this.DynamicLabel.Text = this.TextCash;
            this.DynamicLabel.Color = ARGBColors.Black;
            this.DynamicLabel.setClickDelegate(null);
            this.DynamicLabel.setMouseOverDelegate(null, null);
            this.DynamicLabel.Visible = true;
            this.DynamicButton.Visible = false;
            if (GameEngine.Instance.World.ProfileCardsSet.Count == 0)
            {
                if (!this.fastCashIn)
                {
                    this.DynamicLabel.Text = this.TextEmptySet;
                }
                else
                {
                    this.DynamicLabel.Text = this.TextEmptyMultiSet;
                }
            }
            else if (GameEngine.Instance.World.ProfileCardsSet.Count >= 5)
            {
                this.DynamicLabel.Text = this.TextCash;
                this.DynamicLabel.Visible = false;
                this.DynamicButton.Visible = true;
                this.DynamicButtonLabel.Visible = true;
                this.DynamicButtonLabel.Text = SK.Text("ManageCandsPanel_Cash_In", "Cash In") + " (" + GameEngine.Instance.World.ProfileCardsSet.Count.ToString() + ")";
                this.DynamicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CashClick), "ManageCardsPanel_cash_in_cards");
            }
            else
            {
                this.DynamicLabel.Text = this.TextIncompleteSetStart + ((5 - GameEngine.Instance.World.ProfileCardsSet.Count)).ToString();
            }
            this.mainBackgroundImage.invalidate();
        }

        public void RenderCards(List<CustomSelfDrawPanel.UICard> list)
        {
            int height = this.RefreshCards(this.AvailablePanelContent, list, 500);
            this.AvailablePanelContent.Position = new Point(12, 8);
            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
            if (this.sortBack.Visible)
            {
                this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - BorderPadding, (this.AvailablePanel.Height - (BorderPadding * 2)) - 20);
            }
            else
            {
                this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - BorderPadding, (this.AvailablePanel.Height - (BorderPadding * 2)) + 0x10);
            }
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
        }

        private void ResizeAvailable(int height)
        {
            this.mainBackgroundImage.removeControl(this.scrollbarAvailable);
            this.mainBackgroundImage.removeControl(this.AvailablePanel);
            this.AvailablePanel.clearDirectControlsOnly();
            this.AvailablePanelContent.clearDirectControlsOnly();
            this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
            this.scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
            this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, height);
            this.AvailablePanel.Position = new Point(8, (base.Height - 8) - height);
            this.AvailablePanel.Alpha = 0.8f;
            this.mainBackgroundImage.addControl(this.AvailablePanel);
            this.AvailablePanel.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
            this.mainBackgroundImage.invalidate();
            this.sortBack.clearControls();
            this.sortBack.Image = (Image) GFXLibrary.sort_back;
            this.sortBack.Position = new Point(8, this.AvailablePanel.Height - 0x25);
            this.sortBack.Visible = true;
            this.AvailablePanel.addControl(this.sortBack);
            this.sortByName.ImageNorm = (Image) GFXLibrary.sort_normal;
            this.sortByName.ImageOver = (Image) GFXLibrary.sort_over;
            this.sortByName.ImageClick = (Image) GFXLibrary.sort_in;
            this.sortByName.Position = new Point(7, 4);
            this.sortByName.Text.Text = SK.Text("Card_Sorting_Name", "Sort By Name");
            this.sortByName.Text.Color = ARGBColors.White;
            this.sortByName.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.sortByName.TextYOffset = -1;
            this.sortByName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByNameClicked), "ManageCardsPanel_sort_by_name");
            this.sortBack.addControl(this.sortByName);
            this.sortByType.ImageNorm = (Image) GFXLibrary.sort_normal;
            this.sortByType.ImageOver = (Image) GFXLibrary.sort_over;
            this.sortByType.ImageClick = (Image) GFXLibrary.sort_in;
            this.sortByType.Position = new Point(0xe4, 4);
            this.sortByType.Text.Text = SK.Text("Card_Sorting_Type", "Sort By Type");
            this.sortByType.Text.Color = ARGBColors.White;
            this.sortByType.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.sortByType.TextYOffset = -1;
            this.sortByType.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByTypeClicked), "ManageCardsPanel_sort_by_type");
            this.sortBack.addControl(this.sortByType);
            this.sortByQuantity.ImageNorm = (Image) GFXLibrary.sort_normal;
            this.sortByQuantity.ImageOver = (Image) GFXLibrary.sort_over;
            this.sortByQuantity.ImageClick = (Image) GFXLibrary.sort_in;
            this.sortByQuantity.Position = new Point(0x1c1, 4);
            this.sortByQuantity.Text.Text = SK.Text("Card_Sorting_Quantity", "Sort By Quantity");
            this.sortByQuantity.Text.Color = ARGBColors.White;
            this.sortByQuantity.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.sortByQuantity.TextYOffset = -1;
            this.sortByQuantity.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByQuantityClicked), "PlayCardsPanel_sort_by_type");
            this.sortBack.addControl(this.sortByQuantity);
            this.compressButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
            this.compressButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
            this.compressButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
            this.compressButton.Position = new Point(0x2a1, 0x10);
            this.compressButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.compressClicked), "ManageCardsPanel_compressed_cards");
            this.sortBack.addControl(this.compressButton);
            this.expandButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
            this.expandButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
            this.expandButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
            this.expandButton.Position = new Point(0x2a1, -2);
            this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClicked), "ManageCardsPanel_expand_cards");
            this.sortBack.addControl(this.expandButton);
            if ((this.sortByMode == 0) || (this.sortByMode == 2))
            {
                this.sortByName.Alpha = 0.5f;
                this.sortByType.Alpha = 1f;
                this.sortByQuantity.Alpha = 0.5f;
            }
            else if ((this.sortByMode == 1) || (this.sortByMode == 3))
            {
                this.sortByName.Alpha = 1f;
                this.sortByType.Alpha = 0.5f;
                this.sortByQuantity.Alpha = 0.5f;
            }
            else if ((this.sortByMode == 7) || (this.sortByMode == 8))
            {
                this.sortByName.Alpha = 0.5f;
                this.sortByType.Alpha = 0.5f;
                this.sortByQuantity.Alpha = 1f;
            }
            else
            {
                this.sortByName.Alpha = 1f;
                this.sortByType.Alpha = 1f;
                this.sortByQuantity.Alpha = 1f;
            }
        }

        private void searchClicked()
        {
            this.searchButton.Visible = false;
            this.clearSearchButton.Visible = true;
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = true;
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Focus();
            this.handleSearchTextChanged();
        }

        public void setFilter(int filterGroup)
        {
            this.CatalogFilterDefinition = new CardTypes.CardDefinition();
            this.CatalogFilterDefinition.newCardCategoryFilter = filterGroup;
            CardTypes.CardDefinition definition = new CardTypes.CardDefinition {
                newCardCategoryFilter = filterGroup
            };
            GameEngine.Instance.World.lastUserCardSearchCriteria = definition;
        }

        private void sortByNameClicked()
        {
            if (this.sortByMode != 0)
            {
                this.sortByMode = 0;
            }
            else
            {
                this.sortByMode = 2;
            }
            if (this.PanelMode == PANEL_MODE_BUY)
            {
                this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
            }
            else
            {
                this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            }
            this.sortByName.Alpha = 0.5f;
            this.sortByType.Alpha = 1f;
            this.sortByQuantity.Alpha = 0.5f;
        }

        private void sortByQuantityClicked()
        {
            if (this.sortByMode != 7)
            {
                this.sortByMode = 7;
            }
            else
            {
                this.sortByMode = 8;
            }
            if (this.PanelMode == PANEL_MODE_BUY)
            {
                this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
            }
            else
            {
                this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            }
            this.sortByName.Alpha = 0.5f;
            this.sortByType.Alpha = 0.5f;
            this.sortByQuantity.Alpha = 1f;
        }

        private void sortByTypeClicked()
        {
            if (this.sortByMode != 1)
            {
                this.sortByMode = 1;
            }
            else
            {
                this.sortByMode = 3;
            }
            if (this.PanelMode == PANEL_MODE_BUY)
            {
                this.RefreshCards(this.AvailablePanelContent, this.CardCatalog, 500);
            }
            else
            {
                this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            }
            this.sortByName.Alpha = 1f;
            this.sortByType.Alpha = 0.5f;
            this.sortByQuantity.Alpha = 0.5f;
        }

        public void SwitchToBuy()
        {
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate overDelegate = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate leaveDelegate = null;
            if (!this.cashingIn && !this.buyingCard)
            {
                this.LayoutPanelMode = PANEL_MODE_BUY;
                this.InitCatalog();
                this.ResizeAvailable(0x177);
                this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToCash), "ManageCardsPanel_switch_to_cash_in");
                this.RenderCards(this.CardCatalog);
                this.labelTitle.Text = SK.Text("ManageCandsPanel_Get_New_Cards_Points", "Get New Cards: Current Card Points");
                this.addPointsData();
                if (GameEngine.Instance.World.FakeCardPoints > 0)
                {
                    this.labelTitlePoints.Text = this.labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
                }
                this.DynamicButton.Visible = false;
                this.DynamicButtonLabel.Visible = false;
                this.DynamicLabel.Visible = true;
                this.fastCashInCheckBox.Visible = false;
                if (this.failedPurchaseCard != -1)
                {
                    int profileCardpoints = GameEngine.Instance.World.ProfileCardpoints;
                    int failedPurchaseCost = this.failedPurchaseCost;
                    foreach (CustomSelfDrawPanel.UICard card in this.ShoppingCart)
                    {
                        failedPurchaseCost += card.Definition.cardPoints;
                    }
                    if (failedPurchaseCost <= profileCardpoints)
                    {
                        GameEngine.Instance.World.ShoppingCartCards.Add(this.failedPurchaseCard);
                        this.failedPurchaseCard = -1;
                        this.failedPurchaseCost = -1;
                    }
                }
                this.RefreshCart();
                CustomSelfDrawPanel.CSDImage[] emptyCards = this.EmptyCards;
                for (int i = 0; i < emptyCards.Length; i++)
                {
                    CustomSelfDrawPanel.CSDControl control = emptyCards[i];
                    control.Visible = false;
                }
                foreach (CustomSelfDrawPanel.UICard card2 in this.SetCards)
                {
                    card2.Visible = false;
                }
                this.buttonCatalog.Colorise = ARGBColors.Gray;
                this.buttonCatalog.setMouseOverDelegate(null, null);
                this.buttonCatalog.setClickDelegate(null);
                this.buttonCash.Colorise = ARGBColors.White;
                if (overDelegate == null)
                {
                    overDelegate = () => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_over;
                }
                if (leaveDelegate == null)
                {
                    leaveDelegate = () => this.buttonCash.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
                }
                this.buttonCash.setMouseOverDelegate(overDelegate, leaveDelegate);
                this.buttonCash.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToCash), "ManageCardsPanel_switch_to_cash_in");
                if (GameEngine.Instance.World.getTutorialStage() != 0x66)
                {
                    this.InitFilters();
                }
                else
                {
                    foreach (CustomSelfDrawPanel.CSDButton button in this.FilterButtons)
                    {
                        this.mainBackgroundImage.removeControl(button);
                    }
                    this.FilterButtons.Clear();
                }
                this.PanelMode = PANEL_MODE_BUY;
                this.buyAndPlayCheckBox.Visible = (this.ShoppingCart.Count == 1) && (GameEngine.Instance.World.getTutorialStage() != 0x66);
                this.buyAndPlayCheckBox.Checked = false;
                this.sortByQuantity.Text.Text = SK.Text("Card_Sorting_Price", "Sort By Price");
            }
        }

        private void SwitchToCash()
        {
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate overDelegate = null;
            CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate leaveDelegate = null;
            if (!this.cashingIn && !this.buyingCard)
            {
                this.LayoutPanelMode = PANEL_MODE_CASH;
                GameEngine.Instance.World.searchProfileCardsRedoLast();
                this.ResizeAvailable(0x177);
                this.GetCardsAvailable(false);
                this.RenderCards(this.UICardList);
                this.InitEmptyCards();
                this.RefreshSet();
                this.InitSpinners();
                this.buttonCatalog.Position = new Point(this.buttonCash.X - this.buttonCash.Width, this.buttonCash.Y);
                this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToBuy), "ManageCardsPanel_switch_to_buy_cards");
                this.labelBuyCash.Text = SK.Text("ManageCandsPanel_Get_Cards", "Get Cards");
                this.labelTitle.Text = SK.Text("ManageCandsPanel_Cash_In_Card_Points", "Cash in Cards: Current Card Points");
                this.addPointsData();
                if (GameEngine.Instance.World.FakeCardPoints > 0)
                {
                    this.labelTitlePoints.Text = this.labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
                }
                foreach (CustomSelfDrawPanel.UICard card in this.ShoppingCart)
                {
                    card.Visible = false;
                }
                this.fastCashInCheckBox.Visible = true;
                this.buttonCash.Colorise = ARGBColors.Gray;
                this.buttonCash.setMouseOverDelegate(null, null);
                this.buttonCash.setClickDelegate(null);
                this.buttonCatalog.Colorise = ARGBColors.White;
                if (overDelegate == null)
                {
                    overDelegate = () => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_over;
                }
                if (leaveDelegate == null)
                {
                    leaveDelegate = () => this.buttonCatalog.Image = (Image) GFXLibrary.cardpanel_button_blue_normal;
                }
                this.buttonCatalog.setMouseOverDelegate(overDelegate, leaveDelegate);
                this.buttonCatalog.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.SwitchToBuy), "ManageCardsPanel_switch_to_buy_cards");
                this.InitFilters();
                this.PanelMode = PANEL_MODE_CASH;
                this.buyAndPlayCheckBox.Visible = false;
                this.sortByQuantity.Text.Text = SK.Text("Card_Sorting_Quantity", "Sort By Quantity");
            }
        }

        public bool TUTORIAL_cardsInCart()
        {
            return (this.ShoppingCart.Count > 0);
        }

        public void update()
        {
            double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
            foreach (CustomSelfDrawPanel.CSDFloatingText text in this.floatingLabels)
            {
                text.move(currentMilliseconds);
            }
            if (this.cashingIn)
            {
                if (this.lastCashResponse != null)
                {
                    TimeSpan span3 = (TimeSpan) (DateTime.Now - this.spinstart);
                    if ((span3.TotalSeconds > 1.0) && (this.spinspeed > 0x20))
                    {
                        this.spinspeed /= 2;
                        this.spinstart = DateTime.Now;
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    if (this.spinspeed == 0x20)
                    {
                        if (!this.SlotAnims[i].Animate(currentMilliseconds, this.lastCashResponse.SymbolList[i]) && !this.spinSoundStopPlayed[i])
                        {
                            this.spinSoundStopPlayed[i] = true;
                            TimeSpan span = (TimeSpan) (DateTime.Now - this.spinSoundStopLastTime);
                            if (span.TotalMilliseconds > 500.0)
                            {
                                GameEngine.Instance.playInterfaceSound("CardSpinners_stop_" + this.spinSoundSoundID.ToString());
                                this.spinSoundStopLastTime = DateTime.Now;
                            }
                            this.spinSoundSoundID++;
                        }
                    }
                    else
                    {
                        this.SlotAnims[i].Animate(currentMilliseconds);
                    }
                    if (!this.SlotAnims[i].Playing && (this.lastCashResponse != null))
                    {
                        this.SlotAnims[i].Image = GFXLibrary.CardSlotStillSymbols[this.lastCashResponse.SymbolList[i]];
                    }
                }
                this.DynamicPanel.invalidate();
                if (((!this.SlotAnims[0].Playing && !this.SlotAnims[1].Playing) && (!this.SlotAnims[2].Playing && !this.SlotAnims[3].Playing)) && (!this.SlotAnims[4].Playing && (this.lastCashResponse != null)))
                {
                    GameEngine.Instance.AudioEngine.Stop("CardSpinners_spin");
                    this.playingSpinSound = false;
                    if (!this.showingbonus)
                    {
                        this.floatingLabels.Clear();
                        int num3 = this.NumCardsCachingIn * 5;
                        if (this.lastCashResponse.Newpoints.Value == num3)
                        {
                            this.AddFloatingText("+" + this.lastCashResponse.Newpoints.Value.ToString() + " " + SK.Text("ManageCandsPanel_Card_Points", "Card Points") + "! " + SK.Text("ManageCandsPanel_No_Bonus", "No Bonus"));
                            GameEngine.Instance.playInterfaceSound("CardSpinners_bonus0");
                        }
                        else
                        {
                            string[] strArray2 = new string[] { "+", this.lastCashResponse.Newpoints.Value.ToString(), " ", SK.Text("ManageCandsPanel_Card_Points", "Card Points"), "! (", SK.Text("ManageCandsPanel_Bonus", "Bonus"), " ", (this.lastCashResponse.Newpoints.Value - num3).ToString(), ")" };
                            this.AddFloatingText(string.Concat(strArray2));
                            GameEngine.Instance.playInterfaceSound("CardSpinners_bonus" + ((this.lastCashResponse.Newpoints.Value - num3)).ToString());
                        }
                        this.labelTitle.Text = SK.Text("ManageCandsPanel_Cash_In_Cards_Title", "Cash in Cards: Current Card Points");
                        this.addPointsData();
                        if (GameEngine.Instance.World.FakeCardPoints > 0)
                        {
                            this.labelTitlePoints.Text = this.labelTitlePoints.Text + " (+" + GameEngine.Instance.World.FakeCardPoints.ToString() + ")";
                        }
                        this.showingbonus = true;
                    }
                    else
                    {
                        bool flag = true;
                        foreach (CustomSelfDrawPanel.CSDFloatingText text2 in this.floatingLabels)
                        {
                            if (text2.live)
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            this.GetCardsAvailable(true);
                            int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                            this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                            this.RefreshSet();
                            this.cashingIn = false;
                            this.cardsButtons.Available = true;
                            this.InitEmptyCards();
                            this.InitSpinners();
                            this.showingbonus = false;
                            this.cashingIn = false;
                            this.cardsButtons.Available = true;
                            this.LabelClickToRemove.Text = "";
                        }
                    }
                }
            }
            TimeSpan span2 = (TimeSpan) (DateTime.Now - this.diamondAnimStartTime);
            this.diamondAnimFrame = (int) (span2.TotalMilliseconds / 33.0);
            if (this.PanelMode == PANEL_MODE_CASH)
            {
                foreach (CustomSelfDrawPanel.UICard card in this.UICardList)
                {
                    if (card.Definition.cardGrade == 0x80000)
                    {
                        card.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_diamond_anim.Length];
                        card.bigGradeImage.invalidateXtra();
                    }
                    else if (card.Definition.cardGrade == 0x100000)
                    {
                        card.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_diamond2_anim.Length];
                        card.bigGradeImage.invalidateXtra();
                    }
                    else if (card.Definition.cardGrade == 0x200000)
                    {
                        card.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_diamond3_anim.Length];
                        card.bigGradeImage.invalidateXtra();
                    }
                    else if (card.Definition.cardGrade == 0x40000)
                    {
                        card.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_gold_anim.Length];
                        card.bigGradeImage.invalidateXtra();
                    }
                    else if (card.Definition.cardGrade == 0x400000)
                    {
                        card.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_sapphire_anim.Length];
                        card.bigGradeImage.invalidateXtra();
                    }
                }
            }
            else
            {
                foreach (CustomSelfDrawPanel.UICard card2 in this.CardCatalog)
                {
                    if (card2.Definition.cardGrade == 0x80000)
                    {
                        card2.bigGradeImage.Image = (Image) GFXLibrary.card_diamond_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_diamond_anim.Length];
                        card2.bigGradeImage.invalidateXtra();
                    }
                    else if (card2.Definition.cardGrade == 0x100000)
                    {
                        card2.bigGradeImage.Image = (Image) GFXLibrary.card_diamond2_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_diamond2_anim.Length];
                        card2.bigGradeImage.invalidateXtra();
                    }
                    else if (card2.Definition.cardGrade == 0x200000)
                    {
                        card2.bigGradeImage.Image = (Image) GFXLibrary.card_diamond3_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_diamond3_anim.Length];
                        card2.bigGradeImage.invalidateXtra();
                    }
                    else if (card2.Definition.cardGrade == 0x40000)
                    {
                        card2.bigGradeImage.Image = (Image) GFXLibrary.card_gold_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_gold_anim.Length];
                        card2.bigGradeImage.invalidateXtra();
                    }
                    else if (card2.Definition.cardGrade == 0x400000)
                    {
                        card2.bigGradeImage.Image = (Image) GFXLibrary.card_sapphire_anim[(this.diamondAnimFrame / 1) % GFXLibrary.card_sapphire_anim.Length];
                        card2.bigGradeImage.invalidateXtra();
                    }
                }
            }
        }

        public void UpdateScrollbar(CustomSelfDrawPanel.CSDVertScrollBar bar, CustomSelfDrawPanel.CSDImage content)
        {
            bar.Visible = content.Height > content.ClipRect.Height;
            bar.Max = content.Height - content.ClipRect.Height;
            bar.NumVisibleLines = content.ClipRect.Height;
        }

        public void validateCardPossible(int cardType, int villageID)
        {
            RemoteServices.Instance.set_PreValidateCardToBePlayed_UserCallBack(new RemoteServices.PreValidateCardToBePlayed_UserCallBack(this.preValidateCardToBePlayedCallBack));
            RemoteServices.Instance.PreValidateCardToBePlayed(cardType, villageID);
        }
    }
}

