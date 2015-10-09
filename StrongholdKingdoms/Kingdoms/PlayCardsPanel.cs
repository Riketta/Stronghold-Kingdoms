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
    using System.Threading;
    using System.Windows.Forms;

    public class PlayCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
        private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
        private int AvailablePanelWidth;
        private float bigCardAlpha;
        private float bigCardAlphaTarget = 1f;
        private static int BorderPadding = 0x10;
        private CustomSelfDrawPanel.CSDButton clearSearchButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton compressButton = new CustomSelfDrawPanel.CSDButton();
        private bool compressedCards;
        private int ContentWidth;
        private int currentCardSection = -1;
        private int diamondAnimFrame;
        private DateTime diamondAnimStartTime = DateTime.Now;
        private List<CustomSelfDrawPanel.UICard> dummyCards = new List<CustomSelfDrawPanel.UICard>();
        private CustomSelfDrawPanel.CSDButton expandButton = new CustomSelfDrawPanel.CSDButton();
        private static float fadeStep = 0.1f;
        private List<CustomSelfDrawPanel.CSDButton> FilterButtons = new List<CustomSelfDrawPanel.CSDButton>();
        private Bitmap greenbar = new Bitmap(0x1d, 3);
        private CustomSelfDrawPanel.CSDImage InplayPanelContent = new CustomSelfDrawPanel.CSDImage();
        private int InplayPanelWidth;
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.UICard LastMouseoverCard;
        private DateTime lastRefresh = DateTime.Now;
        private CustomSelfDrawPanel.UICard lastRequestCard;
        private int lastRequestUserID;
        private DateTime lastTickCall = DateTime.Now.AddSeconds(-60.0);
        private DateTime lastUpdatedProgressBars = DateTime.Now.AddSeconds(30.0);
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarInplay = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
        private string sectionName;
        private int selectedVillage;
        private CustomSelfDrawPanel.CSDImage sortBack = new CustomSelfDrawPanel.CSDImage();
        private int sortByMode = -1;
        private CustomSelfDrawPanel.CSDButton sortByName = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sortByQuantity = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sortByRarity = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sortByType = new CustomSelfDrawPanel.CSDButton();
        private List<CustomSelfDrawPanel.UICard> UICardList = new List<CustomSelfDrawPanel.UICard>();
        private List<CustomSelfDrawPanel.UICard> UICardListInplay = new List<CustomSelfDrawPanel.UICard>();
        private bool usingRecentFilter;
        private bool waitingResponse;

        public PlayCardsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void addFilterButton(int category, BaseImage[] buttonImage, int index, int currentFilter)
        {
            this.addFilterButton(category, buttonImage[GFXLibrary.ButtonStateNormal], buttonImage[GFXLibrary.ButtonStateOver], buttonImage[GFXLibrary.ButtonStatePressed], index, currentFilter);
        }

        private void addFilterButton(int category, BaseImage normalImage, BaseImage overImage, BaseImage clickedImage, int index, int currentFilter)
        {
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
                item.Position = new Point((this.AvailablePanel.X + this.AvailablePanel.Width) - 0x54, (this.AvailablePanel.Y + 8) + (index * 0x18));
            }
            else
            {
                item.ImageNorm = (Image) normalImage;
                item.ImageOver = (Image) overImage;
                item.ImageClick = (Image) clickedImage;
                item.Data = category;
                item.CustomTooltipData = category;
                item.CustomTooltipID = 0x2779;
                item.Position = new Point((this.AvailablePanel.X + this.AvailablePanel.Width) - 0x54, (this.AvailablePanel.Y + 8) + (index * 0x18));
                item.ClipRect = new Rectangle(0, 6, 0x33, 0x16);
                item.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.NewFilterClick), "PlayCardsPanel_filter");
            }
            this.FilterButtons.Add(item);
            this.mainBackgroundImage.addControl(item);
        }

        private void AvailableContentScroll()
        {
            int y = this.scrollbarAvailable.Value;
            this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, BorderPadding - y);
            this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
            this.AvailablePanelContent.invalidate();
            this.AvailablePanel.invalidate();
        }

        private void cardClickPlay()
        {
            this.doCardClickPlay(true, false);
        }

        private void cardMouseLeave()
        {
            if (this.LastMouseoverCard != null)
            {
                this.LastMouseoverCard.Hilight(ARGBColors.LightGray);
            }
        }

        private void cardMouseOver()
        {
            if (base.OverControl.GetType() == typeof(CustomSelfDrawPanel.UICard))
            {
                CustomSelfDrawPanel.UICard overControl = (CustomSelfDrawPanel.UICard) base.OverControl;
                overControl.Hilight(ARGBColors.White);
                this.LastMouseoverCard = overControl;
            }
        }

        public void CardPlayed(ICardsProvider provider, ICardsResponse response)
        {
            if (!response.SuccessCode.HasValue || (response.SuccessCode.Value != 1))
            {
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
                MyMessageBox.Show(PlayCardsWindow.translateCardError(response.Message, this.lastRequestCard.Definition.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
                try
                {
                    GameEngine.Instance.World.addProfileCard(this.lastRequestUserID, CardTypes.getStringFromCard(this.lastRequestCard.Definition.id));
                    this.GetCardsAvailable(true);
                    int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                    this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                    this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                }
                catch (Exception exception)
                {
                    MyMessageBox.Show(exception.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
                }
                if (InterfaceMgr.Instance.getCardWindow() != null)
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.getCardWindow());
                }
            }
            else
            {
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
                GameEngine.Instance.World.ProfileCardsSet.Remove(this.lastRequestUserID);
                GameEngine.Instance.addRecentCard(this.lastRequestCard.Definition.id);
                if (this.lastRequestCard.UserIDList.Count > 0)
                {
                    this.lastRequestCard.UserID = this.lastRequestCard.UserIDList[0];
                }
                if (CardTypes.getBasicUniqueCardType(this.lastRequestCard.Definition.id) != -1)
                {
                    disableCardsInPlay(CardTypes.getBasicUniqueCardType(this.lastRequestCard.Definition.id), this.UICardList);
                    this.AvailablePanelContent.invalidate();
                }
                GameEngine.Instance.World.CardPlayed(this.lastRequestCard.Definition.cardCategory, this.lastRequestCard.Definition.id, this.selectedVillage);
                StatTrackingClient.Instance().ActivateTrigger(15, this.usingRecentFilter);
                StatTrackingClient.Instance().ActivateTrigger(0x11, (((PlayCardsWindow) base.ParentForm).getNameSearchText().Length <= 0) ? ((object) 0) : ((object) this.clearSearchButton.Visible));
                StatTrackingClient.Instance().ActivateTrigger(6, this.lastRequestCard.Definition.id);
                if ((GameEngine.Instance.World.getTutorialStage() == 8) || (GameEngine.Instance.World.getTutorialStage() == 12))
                {
                    InterfaceMgr.Instance.closePlayCardsWindow();
                    InterfaceMgr.Instance.ParentForm.TopMost = true;
                    InterfaceMgr.Instance.ParentForm.TopMost = false;
                }
            }
            this.waitingResponse = false;
            if (this.usingRecentFilter)
            {
                this.labelTitle.Text = SK.Text("CardPanel_Recent", "Recently Played") + " : " + GameEngine.Instance.World.countCardsInCategory(0x100000).ToString();
            }
            else
            {
                this.labelTitle.Text = this.sectionName + " : " + GameEngine.Instance.World.ProfileCardsSearch.Count.ToString();
            }
        }

        private void clearSearchClicked()
        {
            this.searchButton.Visible = true;
            this.clearSearchButton.Visible = false;
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = false;
            this.handleSearchTextChanged();
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
                int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                this.AvailableContentScroll();
                base.Invalidate();
            }
        }

        public static void disableCardsInPlay(List<CustomSelfDrawPanel.UICard> cardList)
        {
            CardData userCardData = GameEngine.Instance.World.UserCardData;
            List<int> list = new List<int>();
            int length = userCardData.cards.Length;
            for (int i = 0; i < length; i++)
            {
                int item = CardTypes.getBasicUniqueCardType(CardTypes.getCardType(userCardData.cards[i]));
                if (!list.Contains(item) && (item != -1))
                {
                    list.Add(item);
                }
            }
            foreach (CustomSelfDrawPanel.UICard card in cardList)
            {
                if (card.Enabled && list.Contains(CardTypes.getBasicUniqueCardType(card.Definition.id)))
                {
                    card.Hilight(ARGBColors.Gray);
                }
            }
        }

        public static void disableCardsInPlay(int basicType, List<CustomSelfDrawPanel.UICard> cardList)
        {
            foreach (CustomSelfDrawPanel.UICard card in cardList)
            {
                if (card.Enabled && (basicType == CardTypes.getBasicUniqueCardType(card.Definition.id)))
                {
                    card.Hilight(ARGBColors.Gray);
                }
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

        private void doCardClickPlay(bool fromClick, bool fromValidate)
        {
            try
            {
                if (!this.waitingResponse)
                {
                    if ((base.ClickedControl.GetType() == typeof(CustomSelfDrawPanel.UICard)) || !fromClick)
                    {
                        CustomSelfDrawPanel.UICard clickedControl = null;
                        if (fromClick)
                        {
                            clickedControl = (CustomSelfDrawPanel.UICard) base.ClickedControl;
                        }
                        else
                        {
                            clickedControl = this.lastRequestCard;
                        }
                        this.lastRequestCard = clickedControl;
                        this.waitingResponse = true;
                        XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                        this.selectedVillage = -1;
                        int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                        if (!GameEngine.Instance.World.isCapital(villageID) || (CardTypes.getCardType(clickedControl.Definition.id) == 0xc04))
                        {
                            this.selectedVillage = villageID;
                        }
                        int num2 = GameEngine.Instance.World.getRank() + 1;
                        if (this.lastRequestCard.Definition.cardRank > num2)
                        {
                            MyMessageBox.Show(SK.Text("BuyCardsPanel_Rank_Too_low", "Your rank is too low to play this card.") + Environment.NewLine + SK.Text("BuyCardsPanel_Current_Rank", "Current Rank") + " : " + num2.ToString() + Environment.NewLine + SK.Text("BuyCardsPanel_Required_Rank", "Required Rank") + " : " + this.lastRequestCard.Definition.cardRank.ToString(), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
                            this.waitingResponse = false;
                        }
                        else if ((((this.lastRequestCard.Definition.id == 0xc25) || (this.lastRequestCard.Definition.id == 0xc26)) || ((this.lastRequestCard.Definition.id == 0xc27) || (this.lastRequestCard.Definition.id == 0xc28))) && ((GameEngine.Instance.Village != null) && (GameEngine.Instance.Village.countBuildingType(0x23) == 0)))
                        {
                            MyMessageBox.Show(SK.Text("PlayCard_No_Inn_Available", "An inn must be built at the current village before this card can be played."));
                            this.waitingResponse = false;
                        }
                        else
                        {
                            if (fromClick && Program.mySettings.ConfirmPlayCard)
                            {
                                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
                                base.PanelActive = false;
                                this.waitingResponse = false;
                                InterfaceMgr.Instance.openConfirmPlayCardPopup(clickedControl.Definition, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.doCardClickPlay));
                                return;
                            }
                            if (!fromValidate && CardTypes.cardNeedsValidation(CardTypes.getCardType(clickedControl.Definition.id)))
                            {
                                this.validateCardPossible(CardTypes.getCardType(clickedControl.Definition.id), this.selectedVillage);
                                return;
                            }
                            try
                            {
                                if (InterfaceMgr.Instance.getCardWindow() != null)
                                {
                                    CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
                                }
                                if (fromClick)
                                {
                                    GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
                                }
                                int num4 = clickedControl.UserIDList[0];
                                provider.PlayUserCard(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), num4.ToString(), this.selectedVillage.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.CardPlayed), this);
                                GameEngine.Instance.World.removeProfileCard(clickedControl.UserIDList[0]);
                                if ((this.lastRequestCard.cardCount > 1) || this.usingRecentFilter)
                                {
                                    this.lastRequestUserID = clickedControl.UserIDList[0];
                                    this.lastRequestCard.UserIDList.Remove(clickedControl.UserIDList[0]);
                                    this.lastRequestCard.cardCount--;
                                    if (this.lastRequestCard.cardCount > 1)
                                    {
                                        this.lastRequestCard.countLabel.Text = this.lastRequestCard.cardCount.ToString();
                                        if (this.lastRequestCard.cardCount >= 100)
                                        {
                                            this.lastRequestCard.countLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                                        }
                                        else
                                        {
                                            this.lastRequestCard.countLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                                        }
                                    }
                                    else
                                    {
                                        this.lastRequestCard.countLabel.Text = "";
                                        if (this.usingRecentFilter && (this.lastRequestCard.cardCount < 1))
                                        {
                                            this.lastRequestCard.buyCardsLabel.Visible = true;
                                            this.lastRequestCard.Hilight(ARGBColors.Gray);
                                            this.lastRequestCard.setClickDelegate(null);
                                            this.lastRequestCard.buyCardsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.linkToBuy));
                                        }
                                    }
                                    this.AvailablePanelContent.invalidate();
                                    if (this.usingRecentFilter)
                                    {
                                        this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                                    }
                                }
                                else
                                {
                                    this.UICardList.Remove(this.lastRequestCard);
                                    this.lastRequestUserID = clickedControl.UserIDList[0];
                                    int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                                    this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                                    this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                                }
                            }
                            catch (Exception exception)
                            {
                                MyMessageBox.Show(exception.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
                            }
                        }
                    }
                    if (this.usingRecentFilter)
                    {
                        this.labelTitle.Text = SK.Text("CARDFILTER_RECENT2", "Recent") + " : " + GameEngine.Instance.World.countCardsInCategory(0x100000).ToString();
                    }
                    else
                    {
                        this.labelTitle.Text = this.sectionName + " : " + GameEngine.Instance.World.ProfileCardsSearch.Count.ToString();
                    }
                }
            }
            catch (Exception exception2)
            {
                UniversalDebugLog.Log(exception2.ToString());
            }
        }

        private void expandClicked()
        {
            if (this.compressedCards)
            {
                this.compressedCards = false;
                this.scrollbarAvailable.Value = 0;
                int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
                this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
                this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
                this.AvailableContentScroll();
                base.Invalidate();
            }
        }

        public void FilterClick()
        {
            if (!this.waitingResponse)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
                if (data != 0x3e7)
                {
                    filter.cardFilter = data;
                }
                else
                {
                    filter.cardColour = 2;
                }
                GameEngine.Instance.World.searchProfileCards(filter, "", ((PlayCardsWindow) base.ParentForm).getNameSearchText());
            }
            this.GetCardsAvailable(true);
            int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
            this.scrollbarAvailable.Value = 0;
            this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
            this.AvailableContentScroll();
            base.Invalidate();
        }

        public void forceSearch()
        {
            this.searchButton.Visible = false;
            this.clearSearchButton.Visible = true;
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = true;
            this.handleSearchTextChanged();
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
            foreach (CustomSelfDrawPanel.UICard card in this.UICardList)
            {
                card.clearControls();
                if (card.Parent != null)
                {
                    card.Parent.removeControl(card);
                }
            }
            this.UICardList.Clear();
            int num3 = GameEngine.Instance.World.getRank() + 1;
            foreach (int num4 in GameEngine.Instance.World.ProfileCardsSearch)
            {
                int key = GameEngine.Instance.World.ProfileCards[num4].id;
                try
                {
                    if (dictionary.ContainsKey(key))
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
                        item.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardClickPlay));
                        item.ScaleAll(0.95);
                        this.UICardList.Add(item);
                        dictionary.Remove(key);
                        if (num3 < item.Definition.cardRank)
                        {
                            item.Hilight(ARGBColors.Gray);
                        }
                        else
                        {
                            item.Hilight(ARGBColors.White);
                        }
                        continue;
                    }
                    foreach (CustomSelfDrawPanel.UICard card3 in this.UICardList)
                    {
                        if (card3.Definition.id == key)
                        {
                            card3.UserIDList.Add(num4);
                            continue;
                        }
                    }
                }
                catch (Exception exception)
                {
                    UniversalDebugLog.Log("EXCEPTION " + exception.ToString());
                }
            }
            GFXLibrary.Instance.closeBigCardsLoader();
        }

        private void GetCardsRecent()
        {
            this.UICardList.Clear();
            List<CustomSelfDrawPanel.UICard> list = new List<CustomSelfDrawPanel.UICard>();
            int playerRank = GameEngine.Instance.World.getRank() + 1;
            foreach (CardTypes.CardDefinition definition in CardTypes.cardList)
            {
                if (GameEngine.Instance.recentCards.Contains(definition.id))
                {
                    List<int> userid = new List<int>();
                    foreach (int num2 in GameEngine.Instance.World.ProfileCards.Keys)
                    {
                        if (GameEngine.Instance.World.ProfileCards[num2].id == definition.id)
                        {
                            userid.Add(num2);
                        }
                    }
                    list.Add(this.makeUICard(definition, userid, playerRank));
                }
            }
            foreach (int num3 in GameEngine.Instance.recentCards)
            {
                foreach (CustomSelfDrawPanel.UICard card in list)
                {
                    if (card.Definition.id == num3)
                    {
                        this.UICardList.Add(card);
                    }
                }
            }
        }

        public void handleSearchTextChanged()
        {
            GameEngine.Instance.World.searchProfileCardsRedoLast(((PlayCardsWindow) base.ParentForm).getNameSearchText());
            this.GetCardsAvailable(false);
            int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
            this.scrollbarAvailable.Value = 0;
            this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
            this.AvailableContentScroll();
            base.Invalidate();
        }

        private void HideBigCard()
        {
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
            this.InplayPanelWidth = (this.ContentWidth - BorderPadding) - this.AvailablePanelWidth;
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
            this.sortBack.Image = (Image) GFXLibrary.sort_back;
            this.sortBack.Position = new Point(12, this.AvailablePanel.Height - 0x25);
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
            this.sortByName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByNameClicked), "PlayCardsPanel_sort_by_name");
            this.sortBack.addControl(this.sortByName);
            this.sortByType.ImageNorm = (Image) GFXLibrary.sort_normal;
            this.sortByType.ImageOver = (Image) GFXLibrary.sort_over;
            this.sortByType.ImageClick = (Image) GFXLibrary.sort_in;
            this.sortByType.Position = new Point(0xe4, 4);
            this.sortByType.Text.Text = SK.Text("Card_Sorting_Type", "Sort By Type");
            this.sortByType.Text.Color = ARGBColors.White;
            this.sortByType.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.sortByType.TextYOffset = -1;
            this.sortByType.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByTypeClicked), "PlayCardsPanel_sort_by_type");
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
            this.sortByRarity.ImageNorm = (Image) GFXLibrary.sort_normal;
            this.sortByRarity.ImageOver = (Image) GFXLibrary.sort_over;
            this.sortByRarity.ImageClick = (Image) GFXLibrary.sort_in;
            this.sortByRarity.Position = new Point(0x170, 4);
            this.sortByRarity.Text.Text = SK.Text("Card_Sorting_Rarity", "Sort By Rarity");
            this.sortByRarity.Text.Color = ARGBColors.White;
            this.sortByRarity.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.sortByRarity.TextYOffset = -1;
            this.sortByRarity.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortByRarityClicked), "PlayCardsPanel_sort_by_rarity");
            this.compressButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
            this.compressButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
            this.compressButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
            this.compressButton.Position = new Point(0x2a5, 0x10);
            this.compressButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.compressClicked), "PlayCardsPanel_compress_cards");
            this.sortBack.addControl(this.compressButton);
            this.expandButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
            this.expandButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
            this.expandButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
            this.expandButton.Position = new Point(0x2a5, -2);
            this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClicked), "PlayCardsPanel_expand_cards");
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
            int width = base.Width;
            int borderPadding = BorderPadding;
            int num3 = this.AvailablePanel.Width;
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.closeImage.CustomTooltipID = 0x2774;
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x19, new Point((((base.Width - 1) - 0x11) - 50) + 3, 5), true);
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            CustomSelfDrawPanel.UICardsButtons buttons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) base.ParentForm) {
                Position = new Point(0x328, 0x25)
            };
            this.mainBackgroundImage.addControl(buttons);
            this.labelTitle.Position = new Point(0x1b, 8);
            this.labelTitle.Size = new Size(600, 0x40);
            this.labelTitle.Text = "";
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            if (cardSection != 0)
            {
                CustomSelfDrawPanel.CSDButton button = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.button_cards_all_normal,
                    ImageOver = (Image) GFXLibrary.button_cards_all_over,
                    ImageClick = (Image) GFXLibrary.button_cards_all_over,
                    Position = new Point(390, 0)
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
                ImageNorm = (Image) GFXLibrary.button_cards_in_play_normal,
                ImageOver = (Image) GFXLibrary.button_cards_in_play_over,
                ImageClick = (Image) GFXLibrary.button_cards_in_play_over,
                Position = new Point(570, 0)
            };
            button2.Text.Text = SK.Text("PlayCardsPanel_Cards_In_Play", "Cards In Play");
            button2.TextYOffset = -3;
            button2.Text.Color = ARGBColors.Black;
            button2.Text.Size = new Size(button2.Size.Width - 30, button2.Size.Height);
            button2.Text.Position = new Point(30, 0);
            button2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            button2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showCardsInPlay), "PlayCardsPanel_cards_in_play");
            this.mainBackgroundImage.addControl(button2);
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
            CardTypes.CardDefinition filter = new CardTypes.CardDefinition {
                cardCategory = cardSection
            };
            if ((GameEngine.Instance.World.getTutorialStage() == 8) || (GameEngine.Instance.World.getTutorialStage() == 12))
            {
                filter.rewardcard = true;
            }
            filter.rewardcard = true;
            GameEngine.Instance.World.searchProfileCards(filter, "", ((PlayCardsWindow) base.ParentForm).getNameSearchText());
            if (cardSection < 15)
            {
                this.sectionName = CardSections.getName(cardSection);
            }
            else
            {
                this.sectionName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(CardFilters.getName2(cardSection).ToLower());
            }
            this.labelTitle.Text = this.sectionName + " : " + GameEngine.Instance.World.ProfileCardsSearch.Count.ToString();
            this.GetCardsAvailable(false);
            this.RenderCards();
            this.mainBackgroundImage.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelHandler));
            if (cardSection == 0)
            {
                this.InitFilters();
            }
            if (cardSection == 0)
            {
                ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = !this.searchButton.Visible;
            }
            else
            {
                ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = false;
            }
            base.Invalidate();
        }

        private void InitFilters()
        {
            foreach (CustomSelfDrawPanel.CSDButton button in this.FilterButtons)
            {
                this.mainBackgroundImage.removeControl(button);
            }
            this.FilterButtons.Clear();
            int currentFilter = 0;
            if (this.usingRecentFilter)
            {
                currentFilter = 0x100000;
            }
            else if (GameEngine.Instance.World.lastUserCardSearchCriteria.cardRank != 0)
            {
                currentFilter = 0x200000;
            }
            else if (GameEngine.Instance.World.lastUserCardSearchCriteria != null)
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
            this.addFilterButton(0x100000, GFXLibrary.card_type_buttons_recent_normal, GFXLibrary.card_type_buttons_recent_over, GFXLibrary.card_type_buttons_recent_in, num2++, currentFilter);
            this.addFilterButton(0x200000, GFXLibrary.CardFilters_Playable, num2++, currentFilter);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void InplayContentScroll()
        {
            int y = this.scrollbarInplay.Value;
            this.InplayPanelContent.Position = new Point(this.InplayPanelContent.Position.X, BorderPadding - y);
            this.InplayPanelContent.ClipRect = new Rectangle(this.InplayPanelContent.ClipRect.X, y, this.InplayPanelContent.ClipRect.Width, this.InplayPanelContent.ClipRect.Height);
            this.InplayPanelContent.invalidate();
            this.AvailablePanel.invalidate();
        }

        private void linkToBuy()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDLabel clickedControl = (CustomSelfDrawPanel.CSDLabel) base.ClickedControl;
                if (this.usingRecentFilter)
                {
                    ((PlayCardsWindow) base.ParentForm).SwitchToManageAndFilter(((CustomSelfDrawPanel.UICard) base.ClickedControl.Parent).Definition.newCardCategoryFilter, clickedControl.Data);
                }
                else
                {
                    ((PlayCardsWindow) base.ParentForm).SwitchToManageAndFilter(GameEngine.Instance.World.lastUserCardSearchCriteria.newCardCategoryFilter, clickedControl.Data);
                }
            }
        }

        private CustomSelfDrawPanel.UICard makeUICard(CardTypes.CardDefinition def, List<int> userid, int playerRank)
        {
            Color red;
            CustomSelfDrawPanel.UICard card = new CustomSelfDrawPanel.UICard {
                cardCount = userid.Count
            };
            if (card.cardCount > 0)
            {
                card.UserID = userid[0];
            }
            else
            {
                card.UserID = -1;
            }
            foreach (int num in userid)
            {
                card.UserIDList.Add(num);
            }
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
            int num2 = CardTypes.getGrade(card.Definition.cardGrade);
            switch (num2)
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
            switch (num2)
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
                Size = new Size(card.Width, card.Height)
            };
            if (card.cardCount > 1)
            {
                control.Text = card.cardCount.ToString();
            }
            else
            {
                control.Text = "";
            }
            control.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            control.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            control.Color = ARGBColors.Yellow;
            control.DropShadowColor = ARGBColors.Black;
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
                Text = "",
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold),
                Color = red,
                DropShadowColor = ARGBColors.Black
            };
            card.addControl(label2);
            card.rankLabel = label2;
            if (def.cardPoints > 0)
            {
                CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("CARDS_GetCard", "Get Card"),
                    Position = new Point(0, 0),
                    Size = new Size(0x9d, 0xd9),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                    Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                    Color = ARGBColors.White,
                    Data = def.id,
                    Visible = card.cardCount == 0
                };
                card.buyCardsLabel = label3;
                card.addControl(label3);
                if (card.cardCount == 0)
                {
                    card.buyCardsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.linkToBuy));
                }
            }
            if (card.cardCount == 0)
            {
                card.Hilight(ARGBColors.Gray);
            }
            else
            {
                card.Hilight(ARGBColors.White);
                card.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardClickPlay));
            }
            card.ScaleAll(0.95);
            return card;
        }

        private void mouseWheelHandler(int delta)
        {
            if ((((delta > 0) && ((this.scrollbarAvailable.Value - (delta * 15)) > 0)) || ((delta < 0) && ((this.scrollbarAvailable.Value - (delta * 15)) < this.scrollbarAvailable.Max))) && !this.waitingResponse)
            {
                this.scrollbarAvailable.Value += delta * -15;
                this.AvailableContentScroll();
            }
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
            CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
            if (!this.waitingResponse)
            {
                CardTypes.CardDefinition filter = new CardTypes.CardDefinition();
                int data = clickedControl.Data;
                this.usingRecentFilter = data == 0x100000;
                if (data == 0x200000)
                {
                    filter.cardRank = GameEngine.Instance.World.getRank() + 1;
                }
                else
                {
                    filter.newCardCategoryFilter = data;
                }
                GameEngine.Instance.World.searchProfileCards(filter, "", ((PlayCardsWindow) base.ParentForm).getNameSearchText());
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
                this.InitFilters();
            }
            this.labelTitle.Text = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(CardFilters.getName2(clickedControl.CustomTooltipData).ToLower()) + " : " + GameEngine.Instance.World.countCardsInCategory(clickedControl.CustomTooltipData).ToString();
            if (this.usingRecentFilter)
            {
                this.sortBack.Visible = false;
                this.GetCardsRecent();
                ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = false;
                this.searchButton.Visible = false;
            }
            else
            {
                this.GetCardsAvailable(true);
            }
            this.clearSearchButton.Visible = ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible;
            int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
            this.scrollbarAvailable.Value = 0;
            this.UpdateScrollbar(this.scrollbarAvailable, this.AvailablePanelContent);
            this.AvailableContentScroll();
            base.Invalidate();
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
                    this.doCardClickPlay(false, true);
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
                        new MyMessageBoxPopUp();
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
                        this.doCardClickPlay(false, true);
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
            if (GameEngine.Instance.World.getTutorialStage() == 8)
            {
                list.Sort(CustomSelfDrawPanel.UICard.TUT2cardsNameComparer);
            }
            else if (GameEngine.Instance.World.getTutorialStage() == 12)
            {
                list.Sort(CustomSelfDrawPanel.UICard.TUTcardsNameComparer);
            }
            else if (this.sortByMode == 0)
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
                list.Sort(CustomSelfDrawPanel.UICard.cardsQuantityComparer);
            }
            else if (this.sortByMode == 8)
            {
                list.Sort(CustomSelfDrawPanel.UICard.cardsQuantityComparerReverse);
            }
            int num = GameEngine.Instance.World.getRank() + 1;
            content.clearDirectControlsOnly();
            foreach (CustomSelfDrawPanel.UICard card in this.dummyCards)
            {
                card.clearControls();
            }
            this.dummyCards.Clear();
            int num2 = 0;
            int x = 0x10;
            int y = 0;
            if (this.currentCardSection == 0)
            {
                x = 0;
            }
            if (numArray != null)
            {
                this.sortBack.Visible = false;
                this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanel.Height - (BorderPadding * 2));
                int num5 = 0;
                int num6 = -1;
                for (int i = 0; i < numArray.Length; i += 3)
                {
                    if (numArray[i + 2] != num6)
                    {
                        int cardType = numArray[i];
                        int num9 = numArray[i + 1] * 0xb2;
                        int num10 = (numArray[i + 2] - num5) * 0xed;
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
                        if ((!flag && (((def.cardRank <= 0) || (def.cardRarity <= 0)) || (def.available != 1))) && (num9 == 0))
                        {
                            bool flag2 = false;
                            int num11 = CardTypes.getCardType(cardType);
                            if ((num11 >= 0xbd7) && (num11 <= 0xbf5))
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
                                num5++;
                                num6 = numArray[i + 2];
                                continue;
                            }
                        }
                        if ((num10 + 0xed) > num2)
                        {
                            num2 = num10 + 0xed;
                        }
                        if (flag)
                        {
                            control.Position = new Point(num9, num10);
                            content.addControl(control);
                            if ((num < control.Definition.cardRank) || (control.UserIDList.Count == 0))
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
                        else if (((def.cardRank > 0) && (def.cardRarity > 0)) && (def.available == 1))
                        {
                            CustomSelfDrawPanel.UICard card4 = BuyCardsPanel.makeUICard(def, RemoteServices.Instance.UserID, 0x2710);
                            card4.Position = new Point(num9, num10);
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
                            CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel();
                            if (def.cardPoints > 0)
                            {
                                label.Text = SK.Text("CARDS_GetCard", "Get Card");
                                label.Data = def.id;
                                label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.linkToBuy));
                            }
                            else
                            {
                                label.Text = SK.Text("CARDS_No_Cards", "No Cards");
                            }
                            label.Position = new Point(num9 + 3, num10 + 5);
                            label.Size = new Size(0x9d, 0xd9);
                            label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                            label.Color = ARGBColors.White;
                            label.CustomTooltipID = 0x2775;
                            label.CustomTooltipData = cardType;
                            content.addControl(label);
                        }
                    }
                }
            }
            else
            {
                if (!this.usingRecentFilter)
                {
                    this.sortBack.Visible = true;
                }
                this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, this.AvailablePanelContent.ClipRect.Y, this.AvailablePanelContent.ClipRect.Width, (this.AvailablePanel.Height - (BorderPadding * 2)) - 0x18);
                int num13 = 0;
                int num14 = 0;
                foreach (CustomSelfDrawPanel.UICard card5 in list)
                {
                    card5.Position = new Point(x, y);
                    content.addControl(card5);
                    num14 = y;
                    if (x > width)
                    {
                        x = 0x10;
                        if (this.currentCardSection == 0)
                        {
                            x = 0;
                        }
                        if (!this.compressedCards)
                        {
                            y += card5.Height + 8;
                        }
                        else
                        {
                            y += 0x3a;
                        }
                    }
                    else
                    {
                        x += card5.Width + 12;
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
                    if ((num < card5.Definition.cardRank) || (card5.UserIDList.Count == 0))
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
            disableCardsInPlay(this.UICardList);
            content.invalidate();
            return num2;
        }

        public void RenderCards()
        {
            int height = this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            this.AvailablePanelContent.Position = new Point(BorderPadding, BorderPadding);
            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
            this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - BorderPadding, (this.AvailablePanel.Height - (BorderPadding * 2)) - 0x18);
            this.AvailablePanel.addControl(this.AvailablePanelContent);
            if (height < this.AvailablePanelContent.ClipRect.Height)
            {
                height = this.AvailablePanelContent.ClipRect.Height;
            }
            this.scrollbarAvailable.Position = new Point((this.AvailablePanel.Width - BorderPadding) - (BorderPadding / 2), this.AvailablePanel.Y + (BorderPadding / 2));
            this.scrollbarAvailable.Size = new Size(BorderPadding, (this.AvailablePanel.Height - BorderPadding) - 10);
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
            else
            {
                this.scrollbarAvailable.Visible = true;
            }
            int num1 = this.AvailablePanelContent.Height;
            int num2 = this.AvailablePanelContent.ClipRect.Height;
        }

        public void searchClicked()
        {
            this.searchButton.Visible = false;
            this.clearSearchButton.Visible = true;
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Visible = true;
            ((PlayCardsWindow) base.ParentForm).tbSearchBox.Focus();
            this.handleSearchTextChanged();
        }

        private void SetBigCardAlpha(float alpha)
        {
        }

        private void showAllCardsClick()
        {
            ((PlayCardsWindow) base.ParentForm).SetCardSection(0);
            this.init(0);
            base.Invalidate();
        }

        private void ShowBigCard(CustomSelfDrawPanel.UICard card)
        {
        }

        private void showCardsInPlay()
        {
            ((PlayCardsWindow) base.ParentForm).SwitchPanel(8);
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
            this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
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
            this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            this.sortByName.Alpha = 0.5f;
            this.sortByType.Alpha = 0.5f;
            this.sortByQuantity.Alpha = 1f;
        }

        private void sortByRarityClicked()
        {
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
            this.RefreshCards(this.AvailablePanelContent, this.UICardList, 500);
            this.sortByName.Alpha = 1f;
            this.sortByType.Alpha = 0.5f;
            this.sortByQuantity.Alpha = 0.5f;
        }

        public void update()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.diamondAnimStartTime);
            int num = (int) (span.TotalMilliseconds / 33.0);
            foreach (CustomSelfDrawPanel.UICard card in this.UICardList)
            {
                if (this.diamondAnimFrame != num)
                {
                    BaseImage image = null;
                    if (card.Definition.cardGrade == 0x80000)
                    {
                        image = GFXLibrary.card_diamond_anim[(num / 1) % GFXLibrary.card_diamond_anim.Length];
                    }
                    else if (card.Definition.cardGrade == 0x200000)
                    {
                        image = GFXLibrary.card_diamond3_anim[(num / 1) % GFXLibrary.card_diamond3_anim.Length];
                    }
                    else if (card.Definition.cardGrade == 0x100000)
                    {
                        image = GFXLibrary.card_diamond2_anim[(num / 1) % GFXLibrary.card_diamond2_anim.Length];
                    }
                    else if (card.Definition.cardGrade == 0x40000)
                    {
                        image = GFXLibrary.card_gold_anim[(num / 1) % GFXLibrary.card_gold_anim.Length];
                    }
                    else if (card.Definition.cardGrade == 0x400000)
                    {
                        image = GFXLibrary.card_sapphire_anim[(num / 1) % GFXLibrary.card_sapphire_anim.Length];
                    }
                    if (image != null)
                    {
                        card.bigGradeImage.Image = (Image) image;
                        card.bigGradeImage.invalidateXtra();
                    }
                }
            }
            this.diamondAnimFrame = num;
        }

        private void UpdateAlpha()
        {
            if (this.bigCardAlpha != this.bigCardAlphaTarget)
            {
                if (this.bigCardAlpha < this.bigCardAlphaTarget)
                {
                    this.bigCardAlpha += fadeStep;
                    if (this.bigCardAlpha > this.bigCardAlphaTarget)
                    {
                        this.bigCardAlpha = this.bigCardAlphaTarget;
                    }
                }
                else
                {
                    this.bigCardAlpha -= fadeStep;
                    if (this.bigCardAlpha < this.bigCardAlphaTarget)
                    {
                        this.bigCardAlpha = this.bigCardAlphaTarget;
                    }
                }
                this.SetBigCardAlpha(this.bigCardAlpha);
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

