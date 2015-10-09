namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using Stronghold.AuthClient;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CardBarGDI : CustomSelfDrawPanel.CSDControl
    {
        private CustomSelfDrawPanel.UICard animatedCard;
        private bool animationComplete = true;
        private int animationCounter = 10;
        private int BASE_CARD_POS = 140;
        private const int BASE_CIRCLE_STRIDE = 0x35;
        private const int BASE_CIRCLE_XPOS = 10;
        private const int BASE_HEIGHT = 0xa2;
        private List<CardCircle> cardCircles = new List<CardCircle>();
        private int cardTextTimer;
        private CustomSelfDrawPanel.CSDImage circleCards = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel circleCardsText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.UICard clickedCard;
        public int currentCardSection = -1;
        public bool Dirty;
        private List<DisplayCardInfo> displayedCards = new List<DisplayCardInfo>();
        private int lastAvailableToPlay = -1;
        private CustomSelfDrawPanel.CSDLabel mainText = new CustomSelfDrawPanel.CSDLabel();
        private List<DisplayCardInfo> newDisplayedCards = new List<DisplayCardInfo>();
        private int numCardCirclesVisible = 10;
        private PreValidateCardToBePlayed_ReturnType returnDataRef;
        private int selectedVillage = -1;
        private bool showExtras;
        private Dictionary<int, int> suggestedCardCounts = new Dictionary<int, int>();
        private List<CustomSelfDrawPanel.UICard> suggestedCards = new List<CustomSelfDrawPanel.UICard>();
        private bool suggestedCardsValid;
        private CustomSelfDrawPanel.CSDImage suggestedCollapse = new CustomSelfDrawPanel.CSDImage();
        private List<CustomSelfDrawPanel.UICard> suggestedDisplayedCards = new List<CustomSelfDrawPanel.UICard>();
        private CustomSelfDrawPanel.CSDImage suggestedExpand = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage suggestedNext = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage suggestedPrev = new CustomSelfDrawPanel.CSDImage();
        private bool suggestedPrevActive;
        private bool waitingResponse;

        public static bool addPercent(int card)
        {
            switch (CardTypes.getCardType(card))
            {
                case 0xa81:
                case 0xa82:
                    return true;
            }
            return false;
        }

        public static bool addPlus(int card)
        {
            switch (CardTypes.getCardType(card))
            {
                case 0x907:
                case 0x908:
                case 0x909:
                case 0xb07:
                case 0xb08:
                case 0xb09:
                case 0xb0b:
                case 0xb0c:
                case 0xb0d:
                case 0xb0e:
                case 0xb0f:
                    return true;
            }
            return false;
        }

        public static bool addX(int card)
        {
            switch (CardTypes.getCardType(card))
            {
                case 0x101:
                case 0x102:
                case 0x103:
                case 0x201:
                case 0x202:
                case 0x203:
                case 0x204:
                case 0x205:
                case 0x206:
                case 0x207:
                case 520:
                case 0x209:
                case 0x20a:
                case 0x20b:
                case 0x20c:
                case 0x20d:
                case 0x20e:
                case 0x20f:
                case 0x210:
                case 0x211:
                case 530:
                case 0x213:
                case 0x214:
                case 0x215:
                case 0x216:
                case 0x217:
                case 0x218:
                case 0x219:
                case 0x21a:
                case 0x21b:
                case 540:
                case 0x21d:
                case 0x21e:
                case 0x301:
                case 770:
                case 0x303:
                case 0x304:
                case 0x305:
                case 0x306:
                case 0x307:
                case 0x308:
                case 0x309:
                case 0x30a:
                case 0x30b:
                case 780:
                case 0x30d:
                case 0x30e:
                case 0x30f:
                case 0x401:
                case 0x402:
                case 0x403:
                case 0x404:
                case 0x405:
                case 0x406:
                case 0x407:
                case 0x408:
                case 0x409:
                case 0x40a:
                case 0x40b:
                case 0x40c:
                case 0x40d:
                case 0x40e:
                case 0x40f:
                case 0x501:
                case 0x502:
                case 0x503:
                case 0x504:
                case 0x505:
                case 0x506:
                case 0x507:
                case 0x508:
                case 0x509:
                case 0x50a:
                case 0x50b:
                case 0x50c:
                case 0x50d:
                case 0x50e:
                case 0x50f:
                case 0x510:
                case 0x511:
                case 0x512:
                case 0x513:
                case 0x514:
                case 0x515:
                case 0x516:
                case 0x517:
                case 0x518:
                case 0x519:
                case 0x51a:
                case 0x51b:
                case 0x601:
                case 0x602:
                case 0x603:
                case 0x605:
                case 0x606:
                case 0x607:
                case 0x708:
                case 0x709:
                case 0x70a:
                case 0x801:
                case 0x802:
                case 0x803:
                case 0x804:
                case 0x805:
                case 0x806:
                case 0x807:
                case 0x808:
                case 0x809:
                case 0x80a:
                case 0x80b:
                case 0x80c:
                case 0x80d:
                case 0x80e:
                case 0x80f:
                case 0x810:
                case 0x811:
                case 0x812:
                case 0x813:
                case 0x814:
                case 0x815:
                case 0x816:
                case 0x817:
                case 0x818:
                case 0x901:
                case 0x902:
                case 0x903:
                case 0x904:
                case 0x905:
                case 0x906:
                case 0xa01:
                case 0xa02:
                case 0xa03:
                case 0xa04:
                case 0xa05:
                case 0xa06:
                case 0xa83:
                case 0xa84:
                case 0xa85:
                case 0xa86:
                case 0xa87:
                case 0xa88:
                case 0xb01:
                case 0xb02:
                case 0xb03:
                case 0xb04:
                case 0xb05:
                case 0xb06:
                case 0xb0a:
                case 0xb81:
                case 0xb82:
                case 0xb83:
                case 0xb84:
                case 0xb85:
                case 0xb86:
                case 0xb87:
                case 0xb88:
                case 0xb89:
                case 0xb8a:
                case 0xb8b:
                case 0xb8c:
                case 0xb8d:
                case 0xb8e:
                case 0xb8f:
                case 0xb90:
                case 0xb91:
                case 0xb92:
                case 0xb93:
                case 0xb94:
                case 0xb95:
                case 0xb96:
                case 0xb97:
                case 0xb98:
                case 0xb99:
                case 0xb9d:
                case 0xb9e:
                case 0xb9f:
                    return true;
            }
            return false;
        }

        public void cardClickPlayFalseFromClickTrueValidate()
        {
            this.clickPlay(false, true);
        }

        private void cardPlayed(ICardsProvider provider, ICardsResponse response)
        {
            if (!response.SuccessCode.HasValue || (response.SuccessCode.Value != 1))
            {
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
                MyMessageBox.Show(PlayCardsWindow.translateCardError(response.Message, this.clickedCard.Definition.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
                try
                {
                    GameEngine.Instance.World.addProfileCard(this.clickedCard.UserIDList[0], CardTypes.getStringFromCard(this.clickedCard.Definition.id));
                }
                catch (Exception exception)
                {
                    MyMessageBox.Show(exception.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
                }
            }
            else
            {
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
                GameEngine.Instance.World.ProfileCardsSet.Remove(this.clickedCard.UserIDList[0]);
                GameEngine.Instance.addRecentCard(this.clickedCard.Definition.id);
                if (this.clickedCard.UserIDList.Count > 0)
                {
                    this.clickedCard.UserID = this.clickedCard.UserIDList[0];
                }
                GameEngine.Instance.World.CardPlayed(this.clickedCard.Definition.cardCategory, this.clickedCard.Definition.id, this.selectedVillage);
                if (this.clickedCard.cardCount > 1)
                {
                    this.clickedCard.UserIDList.Remove(this.clickedCard.UserID);
                    this.clickedCard.UserID = this.clickedCard.UserIDList[0];
                    this.clickedCard.cardCount--;
                    this.clickedCard.countLabel.Text = this.clickedCard.cardCount.ToString();
                }
                else
                {
                    this.clickedCard.setClickDelegate(null);
                    CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage {
                        Position = new Point(0, 0),
                        Size = this.clickedCard.bigImage.Size,
                        Image = (Image) GFXLibrary.CardBackBig
                    };
                    control.setScale(0.25);
                    this.clickedCard.CustomTooltipID = 0;
                    this.clickedCard.CustomTooltipData = -1;
                    this.clickedCard.addControl(control);
                }
            }
            this.Dirty = true;
            base.invalidate();
            this.clickedCard = null;
            this.waitingResponse = false;
        }

        public void circleClicked()
        {
            GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_map");
            InterfaceMgr.Instance.openPlayCardsWindow(this.currentCardSection);
        }

        public void clickCollapse()
        {
            for (int i = 0; (i < this.displayedCards.Count) && (i < this.numCardCirclesVisible); i++)
            {
                this.cardCircles[i].Visible = true;
            }
            foreach (CustomSelfDrawPanel.UICard card in this.suggestedDisplayedCards)
            {
                base.removeControl(card);
            }
            this.suggestedDisplayedCards.Clear();
            this.suggestedExpand.Enabled = this.suggestedExpand.Visible = true;
            this.suggestedNext.Enabled = this.suggestedNext.Visible = false;
            this.suggestedPrev.Enabled = this.suggestedPrev.Visible = false;
            this.suggestedCollapse.Enabled = this.suggestedCollapse.Visible = false;
            this.animationComplete = false;
            this.refresh();
            this.Dirty = true;
            base.invalidate();
        }

        public void clickExpand()
        {
            for (int i = 0; i < this.numCardCirclesVisible; i++)
            {
                this.cardCircles[i].Visible = false;
            }
            this.circleCards.Position = new Point(10, 1);
            foreach (CustomSelfDrawPanel.UICard card in this.suggestedCards)
            {
                if (this.suggestedDisplayedCards.Count < 10)
                {
                    this.suggestedDisplayedCards.Add(card);
                    card.Position = new Point(this.BASE_CARD_POS, 1);
                    base.addControl(card);
                }
            }
            this.suggestedExpand.Visible = this.suggestedExpand.Enabled = false;
            this.suggestedNext.Enabled = this.suggestedNext.Visible = this.suggestedCards.Count > 10;
            this.suggestedPrev.Visible = true;
            this.suggestedPrev.Enabled = false;
            this.suggestedPrev.Alpha = 0.5f;
            this.suggestedCollapse.Enabled = this.suggestedCollapse.Visible = true;
            this.animationComplete = false;
            this.Dirty = true;
            base.invalidate();
        }

        public void clickGoLeft()
        {
            if (this.suggestedDisplayedCards[0] != this.suggestedCards[0])
            {
                int num = this.suggestedCards.IndexOf(this.suggestedDisplayedCards[0]) - 10;
                foreach (CustomSelfDrawPanel.UICard card in this.suggestedDisplayedCards)
                {
                    base.removeControl(card);
                }
                this.suggestedDisplayedCards.Clear();
                for (int i = num; i < this.suggestedCards.Count; i++)
                {
                    if (this.suggestedDisplayedCards.Count == 10)
                    {
                        break;
                    }
                    this.suggestedDisplayedCards.Add(this.suggestedCards[i]);
                    this.suggestedCards[i].Position = new Point(this.BASE_CARD_POS, 1);
                    base.addControl(this.suggestedCards[i]);
                }
                this.suggestedNext.Enabled = this.suggestedNext.Visible = this.suggestedDisplayedCards[this.suggestedDisplayedCards.Count - 1] != this.suggestedCards[this.suggestedCards.Count - 1];
                this.suggestedPrev.Data = 5;
                this.suggestedPrev.Image = (Image) GFXLibrary.cardbar_left[2];
                if (this.suggestedDisplayedCards[0] == this.suggestedCards[0])
                {
                    this.suggestedPrev.Enabled = false;
                    this.suggestedPrev.Alpha = 0.5f;
                }
                this.animationComplete = false;
                this.Dirty = true;
                base.invalidate();
            }
        }

        public void clickGoRight()
        {
            int num = this.suggestedCards.IndexOf(this.suggestedDisplayedCards[this.suggestedDisplayedCards.Count - 1]) + 1;
            foreach (CustomSelfDrawPanel.UICard card in this.suggestedDisplayedCards)
            {
                base.removeControl(card);
            }
            this.suggestedDisplayedCards.Clear();
            for (int i = num; i < this.suggestedCards.Count; i++)
            {
                if (this.suggestedDisplayedCards.Count == 10)
                {
                    break;
                }
                this.suggestedDisplayedCards.Add(this.suggestedCards[i]);
                this.suggestedCards[i].Position = new Point(this.BASE_CARD_POS, 1);
                base.addControl(this.suggestedCards[i]);
            }
            this.suggestedNext.Enabled = this.suggestedNext.Visible = this.suggestedDisplayedCards[this.suggestedDisplayedCards.Count - 1] != this.suggestedCards[this.suggestedCards.Count - 1];
            this.suggestedNext.Data = 5;
            this.suggestedNext.Image = (Image) GFXLibrary.cardbar_right[2];
            this.animationComplete = false;
            this.suggestedPrev.Enabled = true;
            this.suggestedPrev.Alpha = 1f;
            this.suggestedPrev.CustomTooltipID = 0x2715;
            this.Dirty = true;
            base.invalidate();
        }

        private void clickPlay(bool fromClick, bool fromValidate)
        {
            if (!this.waitingResponse)
            {
                if ((CustomSelfDrawPanel.StaticClickedControl != null) && fromClick)
                {
                    this.clickedCard = (CustomSelfDrawPanel.UICard) CustomSelfDrawPanel.StaticClickedControl;
                }
                if (this.clickedCard != null)
                {
                    this.waitingResponse = true;
                    XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                    CustomSelfDrawPanel.UICard clickedCard = this.clickedCard;
                    this.selectedVillage = -1;
                    int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                    if (!GameEngine.Instance.World.isCapital(villageID) || (CardTypes.getCardType(clickedCard.Definition.id) == 0xc04))
                    {
                        this.selectedVillage = villageID;
                    }
                    int num2 = GameEngine.Instance.World.getRank() + 1;
                    if (clickedCard.Definition.cardRank > num2)
                    {
                        MyMessageBox.Show(SK.Text("BuyCardsPanel_Rank_Too_low", "Your rank is too low to play this card.") + Environment.NewLine + SK.Text("BuyCardsPanel_Current_Rank", "Current Rank") + " : " + num2.ToString() + Environment.NewLine + SK.Text("BuyCardsPanel_Required_Rank", "Required Rank") + " : " + clickedCard.Definition.cardRank.ToString(), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
                        this.waitingResponse = false;
                    }
                    else if ((((clickedCard.Definition.id == 0xc25) || (clickedCard.Definition.id == 0xc26)) || ((clickedCard.Definition.id == 0xc27) || (clickedCard.Definition.id == 0xc28))) && ((GameEngine.Instance.Village != null) && (GameEngine.Instance.Village.countBuildingType(0x23) == 0)))
                    {
                        MyMessageBox.Show(SK.Text("PlayCard_No_Inn_Available", "An inn must be built at the current village before this card can be played."));
                        this.waitingResponse = false;
                    }
                    else if (fromClick && Program.mySettings.ConfirmPlayCard)
                    {
                        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
                        this.waitingResponse = false;
                        InterfaceMgr.Instance.openConfirmPlayCardPopup(clickedCard.Definition, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.clickPlay));
                    }
                    else if (!fromValidate && CardTypes.cardNeedsValidation(CardTypes.getCardType(clickedCard.Definition.id)))
                    {
                        this.validateCardPossible(CardTypes.getCardType(clickedCard.Definition.id), this.selectedVillage);
                    }
                    else
                    {
                        try
                        {
                            if (fromClick)
                            {
                                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
                            }
                            this.animationCounter = 0;
                            this.animatedCard = this.clickedCard;
                            this.animatedCard.Y += 2;
                            int num3 = clickedCard.UserIDList[0];
                            provider.PlayUserCard(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), num3.ToString(), this.selectedVillage.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.cardPlayed), InterfaceMgr.Instance.getDXBasePanel());
                            GameEngine.Instance.World.removeProfileCard(clickedCard.UserIDList[0]);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        private void clickPlayDelegate()
        {
            this.clickPlay(true, false);
        }

        private void ContinuePreValidateCardToBePlayed()
        {
            PreValidateCardToBePlayed_ReturnType returnDataRef = this.returnDataRef;
            if (returnDataRef.canPlayFully)
            {
                this.clickPlay(false, true);
            }
            else if (!returnDataRef.canPlayPartially)
            {
                this.clickedCard.Hilight(ARGBColors.White);
                if (returnDataRef.otherErrorCode != 0)
                {
                    if (returnDataRef.otherErrorCode == -2)
                    {
                        MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnDataRef.cardType, 5), SK.Text("GENERIC_Error", "Error"));
                    }
                    else if (returnDataRef.otherErrorCode == -3)
                    {
                        GameEngine.Instance.displayedVillageLost(returnDataRef.villageID, true);
                    }
                }
                else
                {
                    switch (returnDataRef.cardType)
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
                            MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_101", "Not enough space in the Granary."), SK.Text("GENERIC_Error", "Error"));
                            return;

                        case 0xc25:
                        case 0xc26:
                        case 0xc27:
                        case 0xc28:
                            MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_102", "Not enough space in the Inn."), SK.Text("GENERIC_Error", "Error"));
                            return;

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
                            MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_103", "Not enough space on the Stockpile."), SK.Text("GENERIC_Error", "Error"));
                            return;

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
                            MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_104", "Not enough space in the Village Hall."), SK.Text("GENERIC_Error", "Error"));
                            return;

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
                            MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_105", "Not enough space in the Armoury."), SK.Text("GENERIC_Error", "Error"));
                            return;

                        case 0xc6d:
                        case 0xc6e:
                        case 0xc6f:
                        case 0xc70:
                        case 0xc71:
                        case 0xc72:
                        case 0xc73:
                        case 0xc74:
                        case 0xc75:
                        case 0xc76:
                        case 0xc77:
                        case 0xc78:
                        case 0xc79:
                        case 0xc7a:
                        case 0xc7b:
                        case 0xc7c:
                        case 0xc7d:
                        case 0xc7e:
                        case 0xc7f:
                        case 0xc80:
                        case 0xc81:
                        case 0xc82:
                        case 0xc83:
                        case 0xc84:
                        case 0xc85:
                        case 0xc86:
                        case 0xc87:
                        case 0xc88:
                        case 0xc89:
                        case 0xc8a:
                        case 0xc8b:
                        case 0xc8c:
                        case 0xc8d:
                        case 0xc8e:
                        case 0xc8f:
                        case 0xc90:
                        case 0xc91:
                        case 0xc92:
                        case 0xc93:
                        case 0xc94:
                        case 0xc95:
                        case 0xc96:
                        case 0xc97:
                        case 0xc98:
                        case 0xc99:
                        case 0xc9a:
                        case 0xc9b:
                        case 0xc9c:
                        case 0xc9d:
                        case 0xc9e:
                        case 0xc9f:
                        case 0xca0:
                        case 0xca1:
                        case 0xca2:
                        case 0xca3:
                        case 0xca4:
                        case 0xca5:
                        case 0xca6:
                        case 0xca7:
                        case 0xca8:
                        case 0xca9:
                        case 0xcaa:
                        case 0xcab:
                        case 0xcac:
                        case 0xcad:
                        case 0xcae:
                        case 0xcaf:
                        case 0xcb0:
                        case 0xcb1:
                        case 0xcb2:
                        case 0xcb3:
                        case 0xcb4:
                        case 0xcb5:
                        case 0xcb6:
                        case 0xcb7:
                        case 0xcb8:
                        case 0xcb9:
                        case 0xcba:
                        case 0xcbb:
                        case 0xcbc:
                        case 0xcbd:
                        case 0xcbe:
                        case 0xcbf:
                        case 0xcd4:
                        case 0xcd5:
                        case 0xcd6:
                            return;

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
                            MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnDataRef.cardType, 1), SK.Text("GENERIC_Error", "Error"));
                            return;

                        case 0xcd7:
                        case 0xcd8:
                        case 0xcd9:
                            MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnDataRef.cardType, 2), SK.Text("GENERIC_Error", "Error"));
                            return;

                        case 0xcda:
                        case 0xcdb:
                        case 0xcdc:
                            MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnDataRef.cardType, 3), SK.Text("GENERIC_Error", "Error"));
                            return;

                        case 0xcdd:
                        case 0xcde:
                        case 0xcdf:
                            MyMessageBox.Show(PlayCardsWindow.translateCardError("", returnDataRef.cardType, 4), SK.Text("GENERIC_Error", "Error"));
                            return;
                    }
                }
            }
            else
            {
                string str = "";
                switch (returnDataRef.cardType)
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
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_5", "Amount of Food gained will be") + " : " + returnDataRef.numCanPlay.ToString();
                        break;

                    case 0xc25:
                    case 0xc26:
                    case 0xc27:
                    case 0xc28:
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_6", "Amount of Ale gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_7", "Amount of Resources gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_8", "Amount of Honour Goods gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_9", "Number of Weapons gained will be") + " : " + returnDataRef.numCanPlay.ToString();
                        break;

                    case 0xc61:
                    case 0xc62:
                    case 0xc63:
                    case 0xc64:
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_10", "Amount of Armour gained will be") + " : " + returnDataRef.numCanPlay.ToString();
                        break;

                    case 0xc69:
                    case 0xc6a:
                    case 0xc6b:
                    case 0xc6c:
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_11", "Number of Catapults gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_1", "Number of Troops that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
                        break;

                    case 0xcd7:
                    case 0xcd8:
                    case 0xcd9:
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_2", "Number of Scouts that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
                        break;

                    case 0xcda:
                    case 0xcdb:
                    case 0xcdc:
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_3", "Number of Monks that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
                        break;

                    case 0xcdd:
                    case 0xcde:
                    case 0xcdf:
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_4", "Number of Merchants that can be recruited") + " : " + returnDataRef.numCanPlay.ToString();
                        break;
                }
                if (MyMessageBox.Show(str + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.clickPlay(false, true);
                }
                else
                {
                    this.clickedCard.Hilight(ARGBColors.White);
                }
            }
        }

        private bool equalCards(List<DisplayCardInfo> list1, List<DisplayCardInfo> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            int count = list1.Count;
            for (int i = 0; i < count; i++)
            {
                if (!list1[i].equals(list2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public void flagAsRendered()
        {
            this.Dirty = false;
        }

        private int getCardImage(int card)
        {
            card = CardTypes.getCardType(card);
            switch (card)
            {
                case 0x101:
                case 0x102:
                case 0x103:
                    return 40;

                case 0x107:
                    return 0x39;

                case 0x108:
                case 0x10b:
                case 0x10c:
                    return 0x5e;

                case 0x109:
                case 0x10d:
                case 270:
                    return 0x23;

                case 0x10a:
                case 0x10f:
                case 0x110:
                    return 0x3a;

                case 0x201:
                case 0x202:
                case 0x203:
                    return 3;

                case 0x204:
                case 0x205:
                case 0x206:
                    return 8;

                case 0x207:
                case 520:
                case 0x209:
                    return 14;

                case 0x20a:
                case 0x20b:
                case 0x20c:
                    return 6;

                case 0x20d:
                case 0x20e:
                case 0x20f:
                    return 11;

                case 0x210:
                case 0x211:
                case 530:
                    return 0x19;

                case 0x213:
                case 0x214:
                case 0x215:
                    return 10;

                case 0x216:
                case 0x217:
                case 0x218:
                    return 50;

                case 0x219:
                case 0x21a:
                case 0x21b:
                    return 2;

                case 540:
                case 0x21d:
                case 0x21e:
                    return 0x33;

                case 0x301:
                case 770:
                case 0x303:
                    return 0x1c;

                case 0x304:
                case 0x305:
                case 0x306:
                    return 0x17;

                case 0x307:
                case 0x308:
                case 0x309:
                    return 13;

                case 0x30a:
                case 0x30b:
                case 780:
                    return 0x13;

                case 0x30d:
                case 0x30e:
                case 0x30f:
                    return 0x34;

                case 0x401:
                case 0x402:
                case 0x403:
                    return 5;

                case 0x404:
                case 0x405:
                case 0x406:
                    return 0x12;

                case 0x407:
                case 0x408:
                case 0x409:
                    return 0x18;

                case 0x40a:
                case 0x40b:
                case 0x40c:
                    return 4;

                case 0x40d:
                case 0x40e:
                case 0x40f:
                    return 7;

                case 0x501:
                case 0x502:
                case 0x503:
                    return 1;

                case 0x504:
                case 0x505:
                case 0x506:
                    return 0x1a;

                case 0x507:
                case 0x508:
                case 0x509:
                    return 12;

                case 0x50a:
                case 0x50b:
                case 0x50c:
                    return 15;

                case 0x50d:
                case 0x50e:
                case 0x50f:
                    return 9;

                case 0x510:
                case 0x511:
                case 0x512:
                    return 0x1b;

                case 0x513:
                case 0x514:
                case 0x515:
                    return 20;

                case 0x516:
                case 0x517:
                case 0x518:
                    return 0x16;

                case 0x519:
                case 0x51a:
                case 0x51b:
                    return 0x15;

                case 0x601:
                case 0x602:
                case 0x603:
                    return 0x22;

                case 0x605:
                case 0x606:
                case 0x607:
                    return 0x59;

                case 0x708:
                case 0x709:
                case 0x70a:
                    return 0x5d;

                case 0x801:
                case 0x802:
                case 0x803:
                    return 0x20;

                case 0x804:
                case 0x805:
                case 0x806:
                    return 0x27;

                case 0x807:
                case 0x808:
                case 0x809:
                    return 0x56;

                case 0x80a:
                case 0x80b:
                case 0x80c:
                    return 0x57;

                case 0x80d:
                case 0x80e:
                case 0x80f:
                    return 0x3b;

                case 0x810:
                case 0x811:
                case 0x812:
                    return 0x66;

                case 0x813:
                case 0x814:
                case 0x815:
                    return 0x65;

                case 0x816:
                case 0x817:
                case 0x818:
                    return 0x20;

                case 0x901:
                case 0x902:
                case 0x903:
                    return 0x21;

                case 0x904:
                    return 0x62;

                case 0x905:
                    return 0x63;

                case 0x906:
                    return 100;

                case 0x907:
                case 0x908:
                case 0x909:
                    return 0x24;

                case 0xa01:
                case 0xa02:
                case 0xa03:
                    return 0x5f;

                case 0xa04:
                case 0xa05:
                case 0xa06:
                    return 0x60;

                case 0xa07:
                case 0xa08:
                case 0xa09:
                    return 0x61;

                case 0xa81:
                case 0xa82:
                    return 0x35;

                case 0xa83:
                case 0xa84:
                case 0xa85:
                    return 0x21;

                case 0xa86:
                case 0xa87:
                case 0xa88:
                    return 0x5f;

                case 0xb01:
                    return 0x25;

                case 0xb02:
                    return 0x26;

                case 0xb03:
                    return 0x29;

                case 0xb04:
                    return 0x38;

                case 0xb05:
                    return 0x58;

                case 0xb06:
                    return 0x2a;

                case 0xb07:
                case 0xb08:
                case 0xb09:
                    return 1;

                case 0xb0a:
                    return 0x67;

                case 0xb41:
                case 0xb42:
                case 0xb43:
                    return 0x22;

                case 0xb47:
                case 0xb48:
                case 0xb49:
                    return 0x6a;

                case 0xb81:
                case 0xb82:
                case 0xb83:
                    return 0x2b;

                case 0xb84:
                case 0xb85:
                case 0xb86:
                    return 0x2c;

                case 0xb87:
                case 0xb88:
                case 0xb89:
                    return 0x2d;

                case 0xb8a:
                case 0xb8b:
                case 0xb8c:
                    return 0x2e;

                case 0xb8d:
                case 0xb8e:
                case 0xb8f:
                    return 0x2f;

                case 0xb90:
                case 0xb91:
                case 0xb92:
                    return 0x30;

                case 0xb93:
                case 0xb94:
                case 0xb95:
                    return 0x31;

                case 0xb96:
                    return 0x36;

                case 0xb97:
                    return 0x37;

                case 0xb98:
                    return 0x5c;

                case 0xb99:
                    return 0x5b;

                case 0xb9a:
                case 0xb9b:
                case 0xb9c:
                    return 90;

                case 0xb9d:
                case 0xb9e:
                case 0xb9f:
                    return 0x10;

                case 0xbd7:
                    return 60;

                case 0xbd8:
                    return 0x3d;

                case 0xbd9:
                    return 0x3e;

                case 0xbda:
                    return 0x3f;

                case 0xbdb:
                    return 0x40;

                case 0xbdc:
                    return 0x41;

                case 0xbde:
                    return 0x69;

                case 0xbdf:
                    return 0x42;

                case 0xbe0:
                    return 0x43;

                case 0xbe1:
                    return 0x44;

                case 0xbe2:
                    return 0x45;

                case 0xbe3:
                    return 70;

                case 0xbe4:
                    return 0x47;

                case 0xbe5:
                    return 0x48;

                case 0xbe6:
                    return 0x49;

                case 0xbe7:
                    return 0x4a;

                case 0xbe8:
                    return 0x4b;

                case 0xbe9:
                    return 0x4c;

                case 0xbea:
                    return 0x4d;

                case 0xbeb:
                    return 0x4e;

                case 0xbec:
                    return 0x4f;

                case 0xbed:
                    return 80;

                case 0xbef:
                    return 0x51;

                case 0xbf0:
                    return 0x52;

                case 0xbf1:
                    return 0x53;

                case 0xbf2:
                    return 0x54;

                case 0xbf3:
                    return 0x55;

                case 0xc04:
                    return 0x68;
            }
            return 0x1d;
        }

        private CardCircle getCircle(int index)
        {
            if (index < this.cardCircles.Count)
            {
                return this.cardCircles[index];
            }
            if (this.cardCircles.Count == 0)
            {
                CardCircle item = new CardCircle();
                this.cardCircles.Add(item);
            }
            return this.cardCircles[0];
        }

        public void init(int cardSection)
        {
            this.init(cardSection, 10, true, 14, 0, 0);
        }

        public void init(int cardSection, int numVisible, bool extras, int cardsPerRow, int xExtra, int yExtra)
        {
            this.numCardCirclesVisible = numVisible;
            this.showExtras = extras;
            this.clearControls();
            this.currentCardSection = cardSection;
            if ((numVisible == 10) && this.showExtras)
            {
                this.Size = new Size(980, 0xa2);
            }
            else
            {
                this.Size = new Size(800, 0x271);
            }
            if (this.showExtras)
            {
                this.mainText.Color = ARGBColors.White;
                this.mainText.DropShadowColor = ARGBColors.Black;
                this.mainText.Position = new Point(10, -50);
                this.mainText.Size = new Size(980, 0xa2);
                this.mainText.Text = "";
                this.mainText.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                this.mainText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.cardTextTimer = 210;
                base.addControl(this.mainText);
            }
            int x = 10;
            int y = 5;
            this.cardCircles.Clear();
            for (int i = 0; i < this.numCardCirclesVisible; i++)
            {
                CardCircle control = new CardCircle();
                control.init(this.currentCardSection, extras);
                control.Position = new Point(x, y);
                base.addControl(control);
                this.cardCircles.Add(control);
                if (((i + 1) % cardsPerRow) == 0)
                {
                    x = 10;
                    y += 0xa2 + yExtra;
                }
                else
                {
                    x += 0x35 + xExtra;
                }
            }
            if (this.showExtras)
            {
                this.circleCards.Position = new Point(x, 1);
                this.circleCards.Image = (Image) GFXLibrary.card_circles_card;
                this.circleCards.Data = -1;
                this.circleCards.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.circleClicked));
                this.circleCards.CustomTooltipID = 0x2711;
                base.addControl(this.circleCards);
                this.circleCardsText.Color = ARGBColors.White;
                this.circleCardsText.DropShadowColor = ARGBColors.Black;
                this.circleCardsText.Position = new Point(0, 0x19);
                this.circleCardsText.Size = new Size(this.circleCards.Width - 1, 0x26);
                this.circleCardsText.Text = "5";
                this.circleCardsText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                this.circleCardsText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.circleCards.addControl(this.circleCardsText);
                x += 0x35;
                this.suggestedExpand.Image = (Image) GFXLibrary.cardbar_expand[0];
                this.suggestedExpand.Position = new Point(((0x35 * this.displayedCards.Count) + 0x10) + this.circleCards.Image.Size.Width, 0x12);
                this.suggestedExpand.Data = 0;
                this.suggestedExpand.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickExpand));
                this.suggestedExpand.CustomTooltipID = 0x2712;
                this.suggestedExpand.Visible = true;
                this.suggestedExpand.Enabled = true;
                base.addControl(this.suggestedExpand);
                this.suggestedCollapse.Image = (Image) GFXLibrary.cardbar_collapse[0];
                this.suggestedCollapse.Position = new Point(0x10 + this.circleCards.Image.Size.Width, 0x12);
                this.suggestedCollapse.Data = 0;
                this.suggestedCollapse.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickCollapse));
                this.suggestedCollapse.CustomTooltipID = 0x2713;
                this.suggestedCollapse.Visible = false;
                this.suggestedCollapse.Enabled = false;
                base.addControl(this.suggestedCollapse);
                this.suggestedNext.Image = (Image) GFXLibrary.cardbar_right[1];
                this.suggestedNext.Position = new Point((0x39 + this.circleCards.Image.Size.Width) + 0x1ef, 0x12);
                this.suggestedNext.Data = 0;
                this.suggestedNext.Visible = false;
                this.suggestedNext.Enabled = false;
                this.suggestedNext.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickGoRight));
                this.suggestedNext.CustomTooltipID = 0x2714;
                base.addControl(this.suggestedNext);
                this.suggestedPrev.Image = (Image) GFXLibrary.cardbar_left[1];
                this.suggestedPrev.Position = new Point(0x39 + this.circleCards.Image.Size.Width, 0x12);
                this.suggestedPrev.Data = 0;
                this.suggestedPrev.Visible = false;
                this.suggestedPrev.Enabled = false;
                this.suggestedPrev.Alpha = 0.5f;
                this.suggestedPrev.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickGoLeft));
                this.suggestedPrev.CustomTooltipID = 0x2715;
                base.addControl(this.suggestedPrev);
            }
            this.refresh();
            if (this.lastAvailableToPlay == 0)
            {
                this.mainText.Text = SK.Text("CardBarGDI_Click_To_Buy", "Click to Buy Cards");
            }
        }

        public CustomSelfDrawPanel.UICard makeUICard(CardTypes.CardDefinition def, int userid, int playerRank)
        {
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
            card.bigGradeImage.Alpha = 0f;
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
                    card.bigFrameExtraImage.Alpha = 0f;
                    card.addControl(card.bigFrameExtraImage);
                    break;

                case 0x80000:
                case 0x100000:
                case 0x200000:
                    card.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                    card.bigFrameExtraImage.Position = new Point(0, 0);
                    card.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_diamond;
                    card.bigFrameExtraImage.Alpha = 0f;
                    card.addControl(card.bigFrameExtraImage);
                    break;

                case 0x400000:
                    card.bigFrameExtraImage = new CustomSelfDrawPanel.CSDImage();
                    card.bigFrameExtraImage.Position = new Point(0, 0);
                    card.bigFrameExtraImage.Image = (Image) GFXLibrary.card_frame_overlay_sapphire;
                    card.bigFrameExtraImage.Alpha = 0f;
                    card.addControl(card.bigFrameExtraImage);
                    break;
            }
            card.bigGradeImage.Size = card.bigGradeImage.Image.Size;
            card.addControl(card.bigGradeImage);
            card.bigTitle = new CustomSelfDrawPanel.CSDLabel();
            card.bigTitle.Text = "";
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
                Position = new Point(0, Convert.ToInt32((double) (card.Height * 0.72))),
                Size = new Size(card.Width, card.Height / 4),
                Text = "1",
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                Font = FontManager.GetFont("Arial", 36f, FontStyle.Bold),
                Color = ARGBColors.White,
                DropShadowColor = ARGBColors.Black
            };
            card.addControl(control);
            card.countLabel = control;
            CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                Text = ""
            };
            card.addControl(label2);
            card.rankLabel = label2;
            card.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickPlayDelegate));
            card.ScaleAll(0.25);
            return card;
        }

        public void preValidateCardToBePlayedCallBack(PreValidateCardToBePlayed_ReturnType returnData)
        {
            this.waitingResponse = false;
            this.returnDataRef = returnData;
            if (returnData.Success)
            {
                if (CardTypes.isMercenaryTroopCardType(returnData.cardType) && (returnData.otherErrorCode == 0x270f))
                {
                    switch (MyMessageBox.Show(SK.Text("RETURNED_CARD_ERROR_UNIT_SPACE", "There is not enough unit space to accomodate these troops. If troops are dispatched from this village some may be lost upon their return.") + Environment.NewLine + Environment.NewLine + SK.Text("PlayCard_Still_Play", "Do you still wish to Play this Card?"), SK.Text("PlayCards_Confirm_play", "Confirm Play Card"), MessageBoxButtons.YesNo))
                    {
                        case DialogResult.No:
                            return;

                        case DialogResult.Yes:
                            this.ContinuePreValidateCardToBePlayed();
                            return;
                    }
                }
                else
                {
                    this.ContinuePreValidateCardToBePlayed();
                }
            }
        }

        public void refresh()
        {
            this.displayedCards.Clear();
            this.newDisplayedCards.Clear();
            this.suggestedCards.Clear();
            this.suggestedCardsValid = false;
            this.suggestedCardCounts.Clear();
            foreach (CustomSelfDrawPanel.UICard card in this.suggestedDisplayedCards)
            {
                base.removeControl(card);
            }
            this.suggestedDisplayedCards.Clear();
            this.suggestedExpand.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickExpand));
            this.lastAvailableToPlay = -1;
            this.update();
            PlayCardsPanel.disableCardsInPlay(this.suggestedDisplayedCards);
        }

        public void toggleActive(bool value)
        {
            float factor = 1f;
            if (!value)
            {
                factor = 0.5f;
                this.suggestedPrevActive = this.suggestedPrev.Enabled;
                this.suggestedPrev.Enabled = false;
                this.suggestedPrev.Alpha = 0.5f;
            }
            else
            {
                this.suggestedPrev.Enabled = this.suggestedPrevActive;
                this.suggestedPrev.Alpha = this.suggestedPrevActive ? 1f : 0.5f;
            }
            this.circleCards.Alpha = this.suggestedExpand.Alpha = this.suggestedCollapse.Alpha = this.suggestedNext.Alpha = factor;
            foreach (CustomSelfDrawPanel.UICard card in this.suggestedDisplayedCards)
            {
                card.Enabled = value;
                card.setAlpha(factor);
            }
            foreach (CardCircle circle in this.cardCircles)
            {
                circle.Enabled = value;
                circle.setAlpha(factor);
            }
            this.circleCards.Enabled = this.suggestedExpand.Enabled = this.suggestedCollapse.Enabled = this.suggestedNext.Enabled = value;
            base.invalidate();
            this.Dirty = true;
        }

        public bool update()
        {
            this.newDisplayedCards.Clear();
            CardData userCardData = GameEngine.Instance.World.UserCardData;
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            DateTime time = VillageMap.getCurrentServerTime();
            int length = userCardData.cards.Length;
            for (int i = 0; i < length; i++)
            {
                int num3 = userCardData.cards[i];
                int num4 = CardTypes.getCardCategory(num3);
                if (((num4 == this.currentCardSection) || (this.currentCardSection == 0)) || ((this.currentCardSection == 9) && ((num4 == 6) || (num4 == 7))))
                {
                    DateTime time2 = userCardData.cardsExpiry[i];
                    TimeSpan span = (TimeSpan) (time2 - time);
                    int num5 = 1;
                    num5 = CardTypes.getCardDuration(num3);
                    int totalMinutes = (int) span.TotalMinutes;
                    if (totalMinutes < 0)
                    {
                        totalMinutes = 0;
                    }
                    int num7 = (totalMinutes * 0x40) / (num5 * 60);
                    if (num7 < 0)
                    {
                        num7 = 0;
                    }
                    else if (num7 >= 0x40)
                    {
                        num7 = 0x3f;
                    }
                    if (span.TotalDays > 100.0)
                    {
                        num7 = 0x40;
                    }
                    DisplayCardInfo item = new DisplayCardInfo {
                        card = num3,
                        currentFrame = num7,
                        effect = CardTypes.getCardEffectValue(num3),
                        imageID = this.getCardImage(num3) - 1
                    };
                    this.newDisplayedCards.Add(item);
                }
            }
            int num8 = GameEngine.Instance.World.countPlayableCards(this.currentCardSection);
            bool flag = false;
            if (num8 != this.lastAvailableToPlay)
            {
                flag = true;
            }
            if (!this.equalCards(this.displayedCards, this.newDisplayedCards))
            {
                flag = true;
                this.displayedCards.Clear();
                foreach (DisplayCardInfo info2 in this.newDisplayedCards)
                {
                    this.displayedCards.Add(info2);
                }
            }
            if (this.showExtras && (this.cardTextTimer > 0))
            {
                this.cardTextTimer--;
                if (this.cardTextTimer <= 0)
                {
                    this.mainText.Visible = false;
                    flag = true;
                }
                else if (this.cardTextTimer < 10)
                {
                    this.mainText.Color = Color.FromArgb(this.cardTextTimer * 0x19, this.cardTextTimer * 0x19, this.cardTextTimer * 0x19, this.cardTextTimer * 0x19);
                    this.mainText.DropShadowColor = Color.FromArgb(this.cardTextTimer * 0x19, 0, 0, 0);
                    flag = true;
                }
            }
            if (this.showExtras && !this.suggestedCardsValid)
            {
                CardTypes.CardDefinition filter = new CardTypes.CardDefinition {
                    cardCategory = this.currentCardSection
                };
                GameEngine.Instance.World.searchProfileCards(filter, "meta", GameEngine.Instance.World.lastUserCardNameFilter);
                int playerRank = GameEngine.Instance.World.getRank() + 1;
                foreach (int num10 in GameEngine.Instance.World.ProfileCardsSearch)
                {
                    if ((GameEngine.Instance.World.ProfileCards[num10].cardRank <= playerRank) && (GameEngine.Instance.World.ProfileCards[num10].id != 0xc04))
                    {
                        int id = GameEngine.Instance.World.ProfileCards[num10].id;
                        if (this.suggestedCardCounts.ContainsKey(id))
                        {
                            foreach (CustomSelfDrawPanel.UICard card in this.suggestedCards)
                            {
                                if (card.Definition.id == GameEngine.Instance.World.ProfileCards[num10].id)
                                {
                                    card.UserIDList.Add(num10);
                                    card.cardCount++;
                                    card.countLabel.Text = card.cardCount.ToString();
                                }
                            }
                        }
                        else
                        {
                            this.suggestedCardCounts.Add(GameEngine.Instance.World.ProfileCards[num10].id, 1);
                            CustomSelfDrawPanel.UICard card2 = this.makeUICard(GameEngine.Instance.World.ProfileCards[num10], num10, playerRank);
                            this.suggestedCards.Add(card2);
                        }
                    }
                }
                this.suggestedExpand.Visible = this.suggestedExpand.Enabled = this.suggestedCards.Count != 0;
                this.suggestedCardsValid = true;
            }
            if (this.showExtras && !this.animationComplete)
            {
                this.animationComplete = true;
                if (!this.suggestedExpand.Visible)
                {
                    for (int j = 0; j < this.suggestedDisplayedCards.Count; j++)
                    {
                        if (this.suggestedDisplayedCards[j].Position.X < (this.BASE_CARD_POS + (0x2f * j)))
                        {
                            this.animationComplete = false;
                            this.suggestedDisplayedCards[j].X = Math.Min((int) (this.suggestedDisplayedCards[j].Position.X + 70), (int) (this.BASE_CARD_POS + (0x2f * j)));
                        }
                    }
                }
                this.Dirty = true;
                base.invalidate();
            }
            if ((this.showExtras && (this.animatedCard != null)) && (this.animationCounter < 30))
            {
                int red = ((this.animationCounter % 10) + 11) * 12;
                this.animatedCard.Hilight(Color.FromArgb(red, red, red));
                this.animationCounter++;
                if (this.animationCounter == 10)
                {
                    this.animatedCard.Y -= 2;
                }
                this.Dirty = true;
                base.invalidate();
            }
            else if (this.showExtras && (this.animatedCard != null))
            {
                this.animatedCard.Hilight(ARGBColors.White);
                this.animatedCard = null;
                this.Dirty = true;
                base.invalidate();
            }
            if (this.showExtras && (this.suggestedNext.Data > 0))
            {
                this.suggestedNext.Data--;
                if (this.suggestedNext.Data == 0)
                {
                    this.suggestedNext.Image = (Image) GFXLibrary.cardbar_right[1];
                    this.Dirty = true;
                    base.invalidate();
                }
            }
            if (this.showExtras && (this.suggestedPrev.Data > 0))
            {
                this.suggestedPrev.Data--;
                if (this.suggestedPrev.Data == 0)
                {
                    this.suggestedPrev.Image = (Image) GFXLibrary.cardbar_left[1];
                    this.Dirty = true;
                    base.invalidate();
                }
            }
            if (flag)
            {
                this.Dirty = true;
                this.lastAvailableToPlay = num8;
                int count = this.displayedCards.Count;
                if (count > this.numCardCirclesVisible)
                {
                    count = this.numCardCirclesVisible;
                }
                this.circleCardsText.Text = num8.ToString();
                if (this.suggestedDisplayedCards.Count != 0)
                {
                    base.invalidate();
                    return true;
                }
                for (int k = 0; k < this.numCardCirclesVisible; k++)
                {
                    this.getCircle(k).Visible = false;
                }
                for (int m = 0; m < count; m++)
                {
                    CardCircle circle2 = this.getCircle(m);
                    DisplayCardInfo info3 = this.displayedCards[m];
                    circle2.Image = GFXLibrary.card_circles_timer[info3.currentFrame];
                    circle2.Visible = true;
                    circle2.FXImage = GFXLibrary.card_circles_icons[info3.imageID];
                    circle2.scaleFXImage(info3.imageID == 0x21);
                    int effect = (int) info3.effect;
                    NumberFormatInfo provider = null;
                    if (effect == info3.effect)
                    {
                        provider = GameEngine.NFI;
                    }
                    else
                    {
                        provider = GameEngine.NFI_D1;
                    }
                    string str = "";
                    if (addX(info3.card))
                    {
                        str = "x" + info3.effect.ToString("N", provider);
                    }
                    else if (addPlus(info3.card))
                    {
                        str = "+" + info3.effect.ToString("N", provider);
                    }
                    else if (info3.effect != 0.0)
                    {
                        str = info3.effect.ToString("N", provider);
                    }
                    else
                    {
                        str = "";
                    }
                    if (addPercent(info3.card))
                    {
                        str = str + "%";
                    }
                    circle2.Text = str;
                    circle2.CustomTooltipID = 0x2710;
                    circle2.CustomTooltipData = info3.card;
                }
                if (this.showExtras)
                {
                    this.circleCards.X = 10 + (0x35 * count);
                    this.suggestedExpand.X = ((0x35 * count) + 0x10) + this.circleCards.Image.Size.Width;
                    this.mainText.X = (this.circleCards.X + 0x35) + 5;
                }
                base.invalidate();
            }
            return flag;
        }

        public void validateCardPossible(int cardType, int villageID)
        {
            RemoteServices.Instance.set_PreValidateCardToBePlayed_UserCallBack(new RemoteServices.PreValidateCardToBePlayed_UserCallBack(this.preValidateCardToBePlayedCallBack));
            RemoteServices.Instance.PreValidateCardToBePlayed(cardType, villageID);
        }

        public class CardCircle : CustomSelfDrawPanel.CSDControl
        {
            private MyMessageBoxPopUp cancelCardPopUp;
            private CustomSelfDrawPanel.CSDImage circle1 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage circle1SubImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel circle1Text = new CustomSelfDrawPanel.CSDLabel();
            private int m_cardSection;

            private void cancelCardCallback(CancelCard_ReturnType returnData)
            {
                if (returnData.Success && (returnData.m_cardData != null))
                {
                    GameEngine.Instance.World.UserCardData = returnData.m_cardData;
                }
            }

            private void CancelCardClicked()
            {
                this.cancelCardSharedFunction(base.CustomTooltipData);
                this.cancelCardPopUp.Close();
            }

            private void cancelCardSharedFunction(int cardType)
            {
                RemoteServices.Instance.set_CancelCard_UserCallBack(new RemoteServices.CancelCard_UserCallBack(this.cancelCardCallback));
                RemoteServices.Instance.CancelCard(cardType);
            }

            public void circleClicked()
            {
                GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_map");
                InterfaceMgr.Instance.openPlayCardsWindow(this.m_cardSection);
            }

            public void circleClickedCancel()
            {
                int customTooltipData = base.CustomTooltipData;
                GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_map");
                if (MyMessageBox.Show(CardTypes.getDescriptionFromCard(customTooltipData) + Environment.NewLine + Environment.NewLine + SK.Text("ViewCards_Cancel_Card_1", "Are you sure you wish to cancel this card?") + Environment.NewLine + Environment.NewLine + SK.Text("ViewCards_Cancel_Card_2", "If you cancel this card, the effect of the card will end and you will lose the card.") + Environment.NewLine + Environment.NewLine, SK.Text("ViewCards_Cancel_Card", "Cancel Card in Play"), MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2, 0) == DialogResult.Yes)
                {
                    this.cancelCardSharedFunction(customTooltipData);
                }
            }

            private void ClosePopUp()
            {
                if (this.cancelCardPopUp != null)
                {
                    if (this.cancelCardPopUp.Created)
                    {
                        this.cancelCardPopUp.Close();
                    }
                    this.cancelCardPopUp = null;
                }
            }

            public void init(int cardSection, bool extras)
            {
                this.m_cardSection = cardSection;
                this.circle1.Position = new Point(0, 0);
                this.circle1.Image = (System.Drawing.Image) GFXLibrary.card_circles_timer[0];
                this.circle1.Data = 10;
                if (extras)
                {
                    this.circle1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.circleClicked));
                }
                else
                {
                    this.circle1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.circleClickedCancel));
                }
                base.addControl(this.circle1);
                this.circle1SubImage.Position = new Point(-6, 0);
                this.circle1SubImage.Image = (System.Drawing.Image) GFXLibrary.popularityFace;
                this.circle1.addControl(this.circle1SubImage);
                this.circle1Text.Color = ARGBColors.White;
                this.circle1Text.DropShadowColor = ARGBColors.Black;
                this.circle1Text.Position = new Point(0, 20);
                this.circle1Text.Size = new Size(this.circle1.Width - 5, 0x1f);
                this.circle1Text.Text = "??";
                this.circle1Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                this.circle1Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.circle1.addControl(this.circle1Text);
                this.Size = this.circle1.Size;
            }

            public void scaleFXImage(bool state)
            {
                if (!state)
                {
                    this.circle1SubImage.Scale = 1.0;
                    this.circle1SubImage.Position = new Point(-6, 0);
                }
                else
                {
                    this.circle1SubImage.Scale = 0.75;
                    this.circle1SubImage.Position = new Point(4, 10);
                }
            }

            public void setAlpha(float value)
            {
                this.circle1.Alpha = value;
                this.circle1SubImage.Alpha = value;
            }

            public BaseImage FXImage
            {
                set
                {
                    this.circle1SubImage.Image = (System.Drawing.Image) value;
                }
            }

            public BaseImage Image
            {
                set
                {
                    this.circle1.Image = (System.Drawing.Image) value;
                }
            }

            public string Text
            {
                set
                {
                    this.circle1Text.Text = value;
                }
            }
        }

        private class DisplayCardInfo
        {
            public int card;
            public int currentFrame = -1;
            public double effect;
            public int imageID = -1;

            public bool equals(CardBarGDI.DisplayCardInfo dci)
            {
                return (((dci.card == this.card) && (dci.currentFrame == this.currentFrame)) && ((dci.imageID == this.imageID) && (dci.effect == this.effect)));
            }
        }
    }
}

