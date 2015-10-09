namespace Kingdoms
{
    using CommonTypes;
    using Stronghold.AuthClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class BuyCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
        private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
        private int AvailablePanelWidth;
        private static int BorderPadding = 0x10;
        private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.UICardsButtons cardButtons;
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private MyMessageBoxPopUp confirmBuyCardPopUp;
        private int ContentWidth;
        private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
        private int currentCardSection = -1;
        private int diamondAnimFrame;
        private DateTime diamondAnimStartTime = DateTime.Now;
        private bool extendedMultiOpen;
        private int extendedMultiOpened;
        private int extendedMultiOpenLeft;
        private CustomSelfDrawPanel.UICardPack extendedPackClicked;
        private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel labelBottom = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.UICardOffer lastoffer;
        private CustomSelfDrawPanel.UICardPack lastpack;
        private CustomSelfDrawPanel.UICard lastRequestCard;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
        private int numRevealCards;
        private static Image offerimage = null;
        private List<CustomSelfDrawPanel.UICardOffer> OfferList;
        private int offerX;
        private int openedPackID = -1;
        private bool openingPack;
        private Dictionary<string, CustomSelfDrawPanel.UICardPack> packControls = new Dictionary<string, CustomSelfDrawPanel.UICardPack>();
        private static Image packimage = null;
        private int packWidth;
        private int packX;
        private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
        private PreValidateCardToBePlayed_ReturnType returnDataRef;
        private CustomSelfDrawPanel.UICard[] revealCards = new CustomSelfDrawPanel.UICard[50];
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
        private int selectedVillage;
        private bool waitingResponse;

        public BuyCardsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void AddOfferControls()
        {
            this.OfferList.Sort((Comparison<CustomSelfDrawPanel.UICardOffer>) ((first, next) => first.Offer.Sequence.CompareTo(next.Offer.Sequence)));
            int num = 100;
            int height = 0;
            for (int i = 0; i < this.OfferList.Count; i++)
            {
                CustomSelfDrawPanel.UICardOffer offer = this.OfferList[i];
                offer.Position = new Point((i & 1) * 330, 5 + (num * i));
                this.AvailablePanelContent.addControl(offer);
                height = (offer.Position.Y + offer.Height) + 4;
            }
            this.AvailablePanelContent.Position = new Point(BorderPadding, 0);
            this.AvailablePanelContent.Size = new Size(this.AvailablePanel.Width, height);
            this.AvailablePanelContent.ClipRect = new Rectangle(0, 0, this.AvailablePanel.Width - BorderPadding, this.AvailablePanel.Height);
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
            CustomSelfDrawPanel.CSDControl control = new CustomSelfDrawPanel.CSDControl {
                Position = new Point(0, 0),
                Size = base.Size
            };
            this.mainBackgroundImage.addControl(control);
            this.mainBackgroundImage.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelHandler));
        }

        private void AvailableContentScroll()
        {
            int y = this.scrollbarAvailable.Value;
            this.AvailablePanelContent.Position = new Point(this.AvailablePanelContent.Position.X, -y);
            this.AvailablePanelContent.ClipRect = new Rectangle(this.AvailablePanelContent.ClipRect.X, y, this.AvailablePanelContent.ClipRect.Width, this.AvailablePanelContent.ClipRect.Height);
            this.AvailablePanelContent.invalidate();
            this.AvailablePanel.invalidate();
        }

        private void BuyPackAfterConfirmation()
        {
            try
            {
                CustomSelfDrawPanel.UICardOffer lastoffer = this.lastoffer;
                XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), lastoffer.Offer.ID.ToString());
                if (InterfaceMgr.Instance.BuyOfferMultiple > 1)
                {
                    req.Multiple = new int?(InterfaceMgr.Instance.BuyOfferMultiple);
                }
                provider.buyCardOffer(req, new CardsEndResponseDelegate(this.OfferBought), this);
                WorldMap world = GameEngine.Instance.World;
                world.ProfileCrowns -= lastoffer.Offer.CrownCost * InterfaceMgr.Instance.BuyOfferMultiple;
                this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
                this.confirmBuyCardPopUp.Close();
            }
            catch (Exception exception)
            {
                UniversalDebugLog.Log("Exception " + exception.ToString());
            }
        }

        private void cardClickPlayFalseFromClickTrueValidate()
        {
            this.doCardClickPlay(false, true);
        }

        private void cardClickPlayTrueFromClickFalseValidate()
        {
            this.doCardClickPlay(true, false);
        }

        private void CardPlayed(ICardsProvider provider, ICardsResponse response)
        {
            if (!response.SuccessCode.HasValue || (response.SuccessCode.Value != 1))
            {
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_failed");
                MyMessageBox.Show(PlayCardsWindow.translateCardError(response.Message, this.lastRequestCard.Definition.id), SK.Text("BuyCardsPanel_Cannot_Play_Cards", "Could not play card."));
                try
                {
                    GameEngine.Instance.World.addProfileCard(this.lastRequestCard.UserID, CardTypes.getStringFromCard(this.lastRequestCard.Definition.id));
                    this.lastRequestCard.Visible = true;
                    this.greyout.invalidate();
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
                this.greyout.removeControl(this.lastRequestCard);
                GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_success");
                GameEngine.Instance.World.ProfileCardsSet.Remove(this.lastRequestCard.UserID);
                GameEngine.Instance.World.CardPlayed(this.lastRequestCard.Definition.cardCategory, this.lastRequestCard.Definition.id, this.selectedVillage);
                GameEngine.Instance.addRecentCard(this.lastRequestCard.Definition.id);
            }
            this.waitingResponse = false;
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closePlayCardsWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        public void CloseGrey()
        {
            this.mainBackgroundImage.removeControl(this.greyout);
            this.cardButtons.Available = true;
            this.mainBackgroundImage.invalidate();
            this.numRevealCards = 0;
        }

        private void ClosePopUp()
        {
            if (this.confirmBuyCardPopUp != null)
            {
                if (this.confirmBuyCardPopUp.Created)
                {
                    this.confirmBuyCardPopUp.Close();
                }
                this.confirmBuyCardPopUp = null;
            }
        }

        private void ContinuePreValidateCardToBePlayed()
        {
            PreValidateCardToBePlayed_ReturnType returnDataRef = this.returnDataRef;
            if (returnDataRef.canPlayFully)
            {
                this.doCardClickPlay(false, true);
            }
            else if (!returnDataRef.canPlayPartially)
            {
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
                        str = SK.Text("RETURNED_CARD_ERROR_BASE2", "There isn't enough room to fully play this card.") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(returnDataRef.cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15_21", "Number of Catapults gained will be") + " : " + returnDataRef.numCanPlay.ToString();
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
                    this.doCardClickPlay(false, true);
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
            if (!this.waitingResponse && ((base.ClickedControl.GetType() == typeof(CustomSelfDrawPanel.UICard)) || !fromClick))
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
                if (!GameEngine.Instance.World.isCapital(villageID))
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
                else if (fromClick && Program.mySettings.ConfirmPlayCard)
                {
                    GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card_open_confirmation");
                    base.PanelActive = false;
                    this.waitingResponse = false;
                    InterfaceMgr.Instance.openConfirmPlayCardPopup(clickedControl.Definition, new ConfirmPlayCardPanel.CardClickPlayDelegate(this.doCardClickPlay));
                }
                else if (!fromValidate && CardTypes.cardNeedsValidation(CardTypes.getCardType(clickedControl.Definition.id)))
                {
                    this.validateCardPossible(CardTypes.getCardType(clickedControl.Definition.id), this.selectedVillage);
                }
                else
                {
                    if (InterfaceMgr.Instance.getCardWindow() != null)
                    {
                        CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.getCardWindow());
                    }
                    if (fromClick)
                    {
                        GameEngine.Instance.playInterfaceSound("PlayCardsPanel_play_card");
                    }
                    int num3 = clickedControl.UserIDList[0];
                    provider.PlayUserCard(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), num3.ToString(), this.selectedVillage.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()), new CardsEndResponseDelegate(this.CardPlayed), this);
                    try
                    {
                        GameEngine.Instance.World.removeProfileCard(clickedControl.UserIDList[0]);
                        clickedControl.Visible = false;
                    }
                    catch (Exception exception)
                    {
                        MyMessageBox.Show(exception.Message, SK.Text("BuyCardsPanel_Error_Report", "ERROR: Please report this error message"));
                    }
                    this.greyout.invalidate();
                }
            }
        }

        private bool doExtendedMultiOpen()
        {
            string category = GameEngine.Instance.World.ProfileCardOffers[this.extendedPackClicked.PackIDs[0]].Category;
            foreach (CardTypes.UserCardPack pack in GameEngine.Instance.World.ProfileUserCardPacks.Values)
            {
                if ((GameEngine.Instance.World.ProfileCardOffers[pack.PackID].Category == category) && (pack.Count > 0))
                {
                    this.openedPackID = pack.PackID;
                    XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                    int num = GameEngine.Instance.World.getRank() + 1;
                    XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), num.ToString(), pack.PackID.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()) {
                        Multiple = new int?(this.extendedMultiOpenLeft)
                    };
                    if (pack.Count < this.extendedMultiOpenLeft)
                    {
                        this.extendedMultiOpen = true;
                        this.extendedMultiOpenLeft -= pack.Count;
                        req.Multiple = new int?(pack.Count);
                        this.extendedMultiOpened = pack.Count;
                    }
                    else
                    {
                        this.extendedMultiOpened = this.extendedMultiOpenLeft;
                        this.extendedMultiOpen = false;
                        this.extendedPackClicked = null;
                    }
                    provider.openCardPack(req, new CardsEndResponseDelegate(this.PackOpened), this);
                    return true;
                }
            }
            return false;
        }

        public void doOfferClicked(bool initialClick)
        {
            CustomSelfDrawPanel.UICardOffer parent = (CustomSelfDrawPanel.UICardOffer) base.ClickedControl.Parent;
            if (initialClick && (parent.Offer.CrownCost > GameEngine.Instance.World.ProfileCrowns))
            {
                BuyCrownsPopup popup = new BuyCrownsPopup();
                popup.init(parent.Offer.CrownCost - GameEngine.Instance.World.ProfileCrowns, base.ParentForm);
                popup.Show(base.ParentForm);
            }
            else if (initialClick && Program.mySettings.BuyMultipleCardPacks)
            {
                GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_offer_open_confirmation");
                base.PanelActive = false;
                this.waitingResponse = false;
                InterfaceMgr.Instance.openConfirmBuyOfferPopup(parent, new ConfirmBuyOfferPanel.CardClickPlayDelegate(this.doOfferClicked));
            }
            else
            {
                if (initialClick)
                {
                    GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_offer");
                    InterfaceMgr.Instance.BuyOfferMultiple = 0;
                }
                if (parent.Offer.CrownCost > GameEngine.Instance.World.ProfileCrowns)
                {
                    BuyCrownsPopup popup2 = new BuyCrownsPopup();
                    popup2.init(parent.Offer.CrownCost - GameEngine.Instance.World.ProfileCrowns, base.ParentForm);
                    popup2.Show(base.ParentForm);
                }
                else
                {
                    if (InterfaceMgr.Instance.BuyOfferMultiple == 0)
                    {
                        InterfaceMgr.Instance.BuyOfferMultiple = 1;
                    }
                    string iD = string.Empty;
                    switch (parent.Offer.Category)
                    {
                        case "FARMING":
                            iD = "CARD_OFFERS_Food_Pack";
                            break;

                        case "CASTLE":
                            iD = "CARD_OFFERS_Castle_Pack";
                            break;

                        case "DEFENSE":
                        case "DEFENCE":
                            iD = "CARD_OFFERS_Defense_Pack";
                            break;

                        case "RANDOM":
                            iD = "CARD_OFFERS_Random_Pack";
                            break;

                        case "INDUSTRY":
                            iD = "CARD_OFFERS_Industry_Pack";
                            break;

                        case "RESEARCH":
                            iD = "CARD_OFFERS_Industry_Pack";
                            break;

                        case "ARMY":
                            iD = "CARD_OFFERS_Army_Pack";
                            break;

                        case "SUPERFARMING":
                            iD = "CARD_OFFERS_Super_Food_Pack";
                            break;

                        case "SUPERDEFENSE":
                        case "SUPERDEFENCE":
                            iD = "CARD_OFFERS_Super_Defense_Pack";
                            break;

                        case "SUPERRANDOM":
                            iD = "CARD_OFFERS_Super_Random_Pack";
                            break;

                        case "SUPERINDUSTRY":
                            iD = "CARD_OFFERS_Super_Industry_Pack";
                            break;

                        case "SUPERARMY":
                            iD = "CARD_OFFERS_Super_Army_Pack";
                            break;

                        case "ULTIMATEFARMING":
                            iD = "CARD_OFFERS_Ultimate_Food_Pack";
                            break;

                        case "ULTIMATEDEFENSE":
                        case "ULTIMATEDEFENCE":
                            iD = "CARD_OFFERS_Ultimate_Defense_Pack";
                            break;

                        case "ULTIMATERANDOM":
                            iD = "CARD_OFFERS_Ultimate_Random_Pack";
                            break;

                        case "ULTIMATEINDUSTRY":
                            iD = "CARD_OFFERS_Ultimate_Industry_Pack";
                            break;

                        case "ULTIMATEARMY":
                            iD = "CARD_OFFERS_Ultimate_Army_Pack";
                            break;

                        case "PLATINUM":
                            iD = "CARD_OFFERS_Platinum_Pack";
                            break;
                    }
                    string str2 = SK.Text(iD);
                    string[] strArray = new string[] { InterfaceMgr.Instance.BuyOfferMultiple.ToString(), " x ", str2, Environment.NewLine, SK.Text("BuyCardsPanel_Crowns_Cost", "Crowns Cost"), " : ", (parent.Offer.CrownCost * InterfaceMgr.Instance.BuyOfferMultiple).ToString() };
                    if (MyMessageBox.Show(string.Concat(strArray), SK.Text("BuyCardsPanel_Confirm_Purchase", "Confirm Purchase"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        this.lastoffer = parent;
                        XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                        XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), parent.Offer.ID.ToString());
                        if (InterfaceMgr.Instance.BuyOfferMultiple > 1)
                        {
                            req.Multiple = new int?(InterfaceMgr.Instance.BuyOfferMultiple);
                        }
                        provider.buyCardOffer(req, new CardsEndResponseDelegate(this.OfferBought), this);
                        WorldMap world = GameEngine.Instance.World;
                        world.ProfileCrowns -= parent.Offer.CrownCost * InterfaceMgr.Instance.BuyOfferMultiple;
                        this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
                    }
                }
            }
        }

        public void doOpenPack(bool initialClick)
        {
            if (!this.openingPack)
            {
                bool flag = false;
                CustomSelfDrawPanel.UICardPack clickedControl = (CustomSelfDrawPanel.UICardPack) base.ClickedControl;
                if (initialClick && Program.mySettings.OpenMultipleCardPacks)
                {
                    GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_pack_open_confirmation");
                    base.PanelActive = false;
                    this.waitingResponse = false;
                    InterfaceMgr.Instance.openConfirmOpenPackPopup(clickedControl, new ConfirmOpenPackPanel.CardClickPlayDelegate(this.doOpenPack));
                }
                else
                {
                    if (initialClick)
                    {
                        GameEngine.Instance.playInterfaceSound("BuyCardsPanel_open_pack");
                        InterfaceMgr.Instance.OpenPackMultiple = 0;
                    }
                    this.openingPack = true;
                    this.extendedMultiOpen = false;
                    this.extendedMultiOpenLeft = 0;
                    this.extendedMultiOpened = 0;
                    foreach (CardTypes.UserCardPack pack2 in GameEngine.Instance.World.ProfileUserCardPacks.Values)
                    {
                        if ((pack2.PackID == clickedControl.PackIDs[0]) && (pack2.Count > 0))
                        {
                            this.openedPackID = pack2.PackID;
                            this.lastpack = clickedControl;
                            XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                            int num = GameEngine.Instance.World.getRank() + 1;
                            XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), num.ToString(), pack2.PackID.ToString(), RemoteServices.Instance.ProfileWorldID.ToString()) {
                                Multiple = new int?(InterfaceMgr.Instance.OpenPackMultiple)
                            };
                            if ((InterfaceMgr.Instance.OpenPackMultiple > 0) && (pack2.Count < InterfaceMgr.Instance.OpenPackMultiple))
                            {
                                this.extendedMultiOpen = true;
                                this.extendedMultiOpenLeft = InterfaceMgr.Instance.OpenPackMultiple - pack2.Count;
                                this.extendedPackClicked = clickedControl;
                                req.Multiple = new int?(pack2.Count);
                                this.extendedMultiOpened = pack2.Count;
                            }
                            provider.openCardPack(req, new CardsEndResponseDelegate(this.PackOpened), this);
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        MyMessageBox.Show(SK.Text("BuyCardsPanel_No_More_Available", "You have no more packs of that type available."), SK.Text("GENERIC_Error", "Error"));
                        this.openingPack = false;
                    }
                }
            }
        }

        public void GetOffercontrolList()
        {
            if (offerimage == null)
            {
                offerimage = new Bitmap(180, 150);
                using (Graphics graphics = Graphics.FromImage(offerimage))
                {
                    graphics.FillRectangle(Brushes.Green, 0, 0, offerimage.Width, offerimage.Height);
                }
            }
            this.OfferList = new List<CustomSelfDrawPanel.UICardOffer>();
            foreach (CardTypes.CardOffer offer in GameEngine.Instance.World.ProfileCardOffers.Values)
            {
                CustomSelfDrawPanel.CSDButton button;
                CustomSelfDrawPanel.CSDButton button2;
                string str5;
                if (offer.Buyable != 1)
                {
                    continue;
                }
                if (offer.Category == "PLATINUM")
                {
                    offer.Buyable = 0;
                    continue;
                }
                CustomSelfDrawPanel.UICardOffer off = new CustomSelfDrawPanel.UICardOffer {
                    Offer = offer,
                    baseImage = new CustomSelfDrawPanel.CSDImage()
                };
                off.baseImage.Position = new Point(0, 20);
                off.packImage = new CustomSelfDrawPanel.CSDImage();
                off.packImage.Position = new Point(10, -7);
                off.packOverImage = new CustomSelfDrawPanel.CSDImage();
                off.packOverImage.Position = new Point(10, -7);
                off.crownImage = new CustomSelfDrawPanel.CSDImage();
                off.crownImage.Position = new Point(330, 0x10);
                string str = string.Empty;
                string str2 = string.Empty;
                string iD = string.Empty;
                string defaultText = string.Empty;
                switch (offer.Category)
                {
                    case "FARMING":
                        str = "card_pack_food_standard_normal";
                        str2 = "card_pack_food_standard_over";
                        iD = "CARD_OFFERS_Food_Pack";
                        defaultText = "Food Pack";
                        goto Label_078F;

                    case "CASTLE":
                        str = "card_pack_castle_standard_normal";
                        str2 = "card_pack_castle_standard_over";
                        iD = "CARD_OFFERS_Castle_Pack";
                        defaultText = "Castle Pack";
                        goto Label_078F;

                    case "DEFENSE":
                    case "DEFENCE":
                        str = "card_pack_defence_standard_normal";
                        str2 = "card_pack_defence_standard_over";
                        iD = "CARD_OFFERS_Defense_Pack";
                        defaultText = "Defence Pack";
                        goto Label_078F;

                    case "RANDOM":
                        str = "card_pack_random_standard_normal";
                        str2 = "card_pack_random_standard_over";
                        iD = "CARD_OFFERS_Random_Pack";
                        defaultText = "Random Pack";
                        goto Label_078F;

                    case "INDUSTRY":
                        str = "card_pack_Industry_standard_normal";
                        str2 = "card_pack_Industry_standard_over";
                        iD = "CARD_OFFERS_Industry_Pack";
                        defaultText = "Industry Pack";
                        goto Label_078F;

                    case "RESEARCH":
                        str = "card_pack_research_silver_normal";
                        str2 = "card_pack_research_silver_over";
                        iD = "CARD_OFFERS_Industry_Pack";
                        defaultText = "Industry Pack";
                        goto Label_078F;

                    case "ARMY":
                        str = "card_pack_army_standard_normal";
                        str2 = "card_pack_army_standard_over";
                        iD = "CARD_OFFERS_Army_Pack";
                        defaultText = "Army Pack";
                        goto Label_078F;

                    case "SUPERFARMING":
                        str = "card_pack_food_silver_normal";
                        str2 = "card_pack_food_silver_over";
                        iD = "CARD_OFFERS_Super_Food_Pack";
                        defaultText = "Super Food Pack";
                        goto Label_078F;

                    case "SUPERDEFENSE":
                    case "SUPERDEFENCE":
                        str = "card_pack_defence_silver_normal";
                        str2 = "card_pack_defence_silver_over";
                        iD = "CARD_OFFERS_Super_Defense_Pack";
                        defaultText = "Super Defence Pack";
                        goto Label_078F;

                    case "SUPERRANDOM":
                        str = "card_pack_random_silver_normal";
                        str2 = "card_pack_random_silver_over";
                        iD = "CARD_OFFERS_Super_Random_Pack";
                        defaultText = "Super Random Pack";
                        button = new CustomSelfDrawPanel.CSDButton {
                            ImageNorm = (Image) GFXLibrary.mrhp_button_more_info_solid[0],
                            ImageOver = (Image) GFXLibrary.mrhp_button_more_info_solid[1],
                            MoveOnClick = true,
                            Position = new Point(270, 100)
                        };
                        button.Text.Text = SK.Text("UserInfo_MoreInfo", "More Info");
                        if (!(Program.mySettings.LanguageIdent == "it"))
                        {
                            break;
                        }
                        button.Text.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                        goto Label_0521;

                    case "SUPERINDUSTRY":
                        str = "card_pack_Industry_silver_normal";
                        str2 = "card_pack_Industry_silver_over";
                        iD = "CARD_OFFERS_Super_Industry_Pack";
                        defaultText = "Super Industry Pack";
                        goto Label_078F;

                    case "SUPERARMY":
                        str = "card_pack_army_silver_normal";
                        str2 = "card_pack_army_silver_over";
                        iD = "CARD_OFFERS_Super_Army_Pack";
                        defaultText = "Super Army Pack";
                        goto Label_078F;

                    case "ULTIMATEFARMING":
                        str = "card_pack_food_gold_normal";
                        str2 = "card_pack_food_gold_over";
                        iD = "CARD_OFFERS_Ultimate_Food_Pack";
                        defaultText = "Ultimate Food Pack";
                        goto Label_078F;

                    case "ULTIMATEDEFENSE":
                    case "ULTIMATEDEFENCE":
                        str = "card_pack_defence_gold_normal";
                        str2 = "card_pack_defence_gold_over";
                        iD = "CARD_OFFERS_Ultimate_Defense_Pack";
                        defaultText = "Ultimate Defence Pack";
                        goto Label_078F;

                    case "ULTIMATERANDOM":
                        str = "card_pack_random_gold_normal";
                        str2 = "card_pack_random_gold_over";
                        iD = "CARD_OFFERS_Ultimate_Random_Pack";
                        defaultText = "Ultimate Random Pack";
                        button2 = new CustomSelfDrawPanel.CSDButton {
                            ImageNorm = (Image) GFXLibrary.mrhp_button_more_info_solid[0],
                            ImageOver = (Image) GFXLibrary.mrhp_button_more_info_solid[1],
                            MoveOnClick = true,
                            Position = new Point(270, 100)
                        };
                        button2.Text.Text = SK.Text("UserInfo_MoreInfo", "More Info");
                        if (!(Program.mySettings.LanguageIdent == "it"))
                        {
                            goto Label_06B9;
                        }
                        button2.Text.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                        goto Label_06D5;

                    case "ULTIMATEINDUSTRY":
                        str = "card_pack_Industry_gold_normal";
                        str2 = "card_pack_Industry_gold_over";
                        iD = "CARD_OFFERS_Ultimate_Industry_Pack";
                        defaultText = "Ultimate Industry Pack";
                        goto Label_078F;

                    case "ULTIMATEARMY":
                        str = "card_pack_army_gold_normal";
                        str2 = "card_pack_army_gold_over";
                        iD = "CARD_OFFERS_Ultimate_Army_Pack";
                        defaultText = "Ultimate Army Pack";
                        goto Label_078F;

                    case "PLATINUM":
                        str = "card_pack_army_gold_normal";
                        str2 = "card_pack_army_gold_over";
                        iD = "CARD_OFFERS_Platinum_Pack";
                        defaultText = "Platinum Pack";
                        goto Label_078F;

                    default:
                        goto Label_078F;
                }
                button.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            Label_0521:
                button.TextYOffset = -3;
                button.Text.Position = new Point(-3, 0);
                button.Text.Color = ARGBColors.Black;
                button.Text.DropShadowColor = Color.FromArgb(60, 90, 100);
                button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.moreSuperClicked));
                off.addControl(button);
                goto Label_078F;
            Label_06B9:
                button2.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            Label_06D5:
                button2.TextYOffset = -3;
                button2.Text.Position = new Point(-3, 0);
                button2.Text.Color = ARGBColors.Black;
                button2.Text.DropShadowColor = Color.FromArgb(60, 90, 100);
                button2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.moreUltimateClicked));
                off.addControl(button2);
            Label_078F:
                str5 = "";
                off.baseImage.Image = (Image) GFXLibrary.card_offer_background;
                if (GFXLibrary.CardPackImages == null)
                {
                    UniversalDebugLog.Log("CARDPACK IMAGES IS NULL");
                }
                UniversalDebugLog.Log("Num packimages: " + GFXLibrary.CardPackImages.Count);
                off.packImage.Image = GFXLibrary.CardPackImages[str];
                off.packOverImage.Image = GFXLibrary.CardPackImages[str2];
                str5 = SK.Text(iD, defaultText);
                off.crownImage.Image = (Image) GFXLibrary.card_offer_pieces[2];
                off.packImage.Visible = true;
                off.packOverImage.Visible = false;
                off.baseImage.setMouseOverDelegate(delegate {
                    off.packImage.Visible = false;
                    off.packOverImage.Visible = true;
                    off.baseImage.Image = (Image) GFXLibrary.card_offer_background_over;
                }, delegate {
                    off.packImage.Visible = true;
                    off.packOverImage.Visible = false;
                    off.baseImage.Image = (Image) GFXLibrary.card_offer_background;
                });
                off.nameLabel = new CustomSelfDrawPanel.CSDLabel();
                off.nameLabel.Position = new Point(0x5e, 0x1d);
                off.nameLabel.Text = str5;
                off.nameLabel.Size = new Size(300, 30);
                off.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                off.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                off.nameLabel.Color = ARGBColors.Black;
                off.descLabel = new CustomSelfDrawPanel.CSDLabel();
                off.descLabel.Position = new Point(0x5e, 0x2e);
                off.descLabel.Text = SK.Text(iD + "_desc");
                off.descLabel.Size = new Size(0xf5, 0x2d);
                off.descLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                off.descLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                off.descLabel.Color = ARGBColors.Black;
                off.cardLabel = new CustomSelfDrawPanel.CSDLabel();
                off.cardLabel.Position = new Point(0xbf, 0x3b);
                off.cardLabel.Text = SK.Text("BUY_CARDS_5_per_pack", "5 Cards per Pack");
                off.cardLabel.Size = new Size(200, 30);
                off.cardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
                off.cardLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                off.cardLabel.Color = ARGBColors.Black;
                off.costLabel = new CustomSelfDrawPanel.CSDLabel();
                off.costLabel.Position = new Point(0x132, 0x1c);
                off.costLabel.Text = offer.CrownCost.ToString();
                off.costLabel.Size = new Size(40, 30);
                off.costLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                off.costLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
                off.costLabel.Color = ARGBColors.Black;
                off.addControl(off.baseImage);
                off.addControl(off.packImage);
                off.addControl(off.packOverImage);
                off.addControl(off.nameLabel);
                off.addControl(off.descLabel);
                off.addControl(off.crownImage);
                off.addControl(off.cardLabel);
                off.addControl(off.costLabel);
                off.baseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OfferClicked));
                off.Size = new Size(off.baseImage.Size.Width, 140);
                this.OfferList.Add(off);
            }
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
            this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 0x177);
            this.AvailablePanel.Position = new Point(8, (base.Height - 8) - 550);
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
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x27, new Point((((base.Width - 1) - 0x11) - 50) + 3, 5), true);
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
            closeGrey.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.CloseGrey), "BuyCardsPanel_close_overlay");
            closeGrey.Position = new Point((base.Width - 14) - 0x11, 10);
            this.greyout.addControl(closeGrey);
            CustomSelfDrawPanel.UICardsButtons buttons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) base.ParentForm) {
                Position = new Point(0x328, 0x25)
            };
            this.mainBackgroundImage.addControl(buttons);
            this.cardButtons = buttons;
            this.labelTitle.Position = new Point(0x1b, 8);
            this.labelTitle.Size = new Size(0x3a7, 0x40);
            this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            this.labelBottom.Position = new Point(0x1b, (this.AvailablePanel.Y + this.AvailablePanel.Height) + 4);
            this.labelBottom.Size = new Size(800, 0x40);
            this.labelBottom.Text = SK.Text("BuyCardsPanel_Click_To_Open", "Click on a pack to open it");
            this.labelBottom.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelBottom.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
            this.labelBottom.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelBottom);
            this.packWidth = 100;
            this.packX = this.AvailablePanel.X + BorderPadding;
            this.offerX = (this.packX + this.packWidth) - 0x10;
            this.GetOffercontrolList();
            this.AddOfferControls();
            this.UpdatePacks();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        public static CustomSelfDrawPanel.UICard makeUICard(CardTypes.CardDefinition def, int userid, int playerRank)
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

        public void moreSuperClicked()
        {
            this.showMorePopup(0);
        }

        public void moreUltimateClicked()
        {
            this.showMorePopup(1);
        }

        private void mouseWheelHandler(int delta)
        {
            if (delta > 0)
            {
                if ((this.scrollbarAvailable.Value - (delta * 15)) > 0)
                {
                    this.scrollbarAvailable.Value += delta * -15;
                }
                else
                {
                    this.scrollbarAvailable.Value = 0;
                }
                this.AvailableContentScroll();
            }
            else if (delta < 0)
            {
                if ((this.scrollbarAvailable.Value - (delta * 15)) < this.scrollbarAvailable.Max)
                {
                    this.scrollbarAvailable.Value += delta * -15;
                }
                else
                {
                    this.scrollbarAvailable.Value = this.scrollbarAvailable.Max;
                }
                this.AvailableContentScroll();
            }
        }

        public void OfferBought(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode == 1)
            {
                if (GameEngine.Instance.World.ProfileUserCardPacks.ContainsKey(this.lastoffer.Offer.ID))
                {
                    if (InterfaceMgr.Instance.BuyOfferMultiple < 1)
                    {
                        CardTypes.UserCardPack local1 = GameEngine.Instance.World.ProfileUserCardPacks[this.lastoffer.Offer.ID];
                        local1.Count++;
                    }
                    else
                    {
                        CardTypes.UserCardPack local2 = GameEngine.Instance.World.ProfileUserCardPacks[this.lastoffer.Offer.ID];
                        local2.Count += InterfaceMgr.Instance.BuyOfferMultiple;
                    }
                }
                else
                {
                    CardTypes.UserCardPack pack = new CardTypes.UserCardPack {
                        Count = InterfaceMgr.Instance.BuyOfferMultiple,
                        PackID = this.lastoffer.Offer.ID
                    };
                    GameEngine.Instance.World.ProfileUserCardPacks.Add(this.lastoffer.Offer.ID, pack);
                }
                this.UpdatePacks();
            }
            else
            {
                WorldMap world = GameEngine.Instance.World;
                world.ProfileCrowns += this.lastoffer.Offer.CrownCost * InterfaceMgr.Instance.BuyOfferMultiple;
                this.labelTitle.Text = SK.Text("BuyCardsPanel_Buy_and_Open_Packs", "Buy and Open Card Packs: Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString();
                MyMessageBox.Show(response.Message, SK.Text("GENERIC_Error", "Error"));
            }
        }

        public void OfferClicked()
        {
            this.doOfferClicked(true);
        }

        public void OpenGrey()
        {
            this.mainBackgroundImage.addControl(this.greyout);
            this.cardButtons.Available = false;
            this.mainBackgroundImage.invalidate();
        }

        public void OpenPack()
        {
            this.doOpenPack(true);
        }

        public void PackOpened(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode.Value != 1)
            {
                MyMessageBox.Show(response.Message, SK.Text("BuyCardsPanel_Could_Not_Open_Pack", "Could not open pack."));
                this.UpdatePacks();
            }
            else
            {
                foreach (CardTypes.UserCardPack pack in GameEngine.Instance.World.ProfileUserCardPacks.Values)
                {
                    if (pack.PackID == this.openedPackID)
                    {
                        if (this.extendedMultiOpened > 0)
                        {
                            pack.Count -= this.extendedMultiOpened;
                        }
                        else if (InterfaceMgr.Instance.OpenPackMultiple < 1)
                        {
                            pack.Count--;
                        }
                        else
                        {
                            pack.Count -= InterfaceMgr.Instance.OpenPackMultiple;
                        }
                        if ((this.extendedMultiOpen && (this.extendedMultiOpenLeft > 0)) && this.doExtendedMultiOpen())
                        {
                            foreach (string str in response.Strings.Split(";".ToCharArray()))
                            {
                                string[] strArray2 = str.Split(",".ToCharArray());
                                if (strArray2.Length == 2)
                                {
                                    GameEngine.Instance.World.ProfileCards.Add(Convert.ToInt32(strArray2[0].Trim()), CardTypes.getCardDefinitionFromString(strArray2[1].Trim()));
                                }
                            }
                            return;
                        }
                        this.UpdatePacks();
                        break;
                    }
                }
                try
                {
                    this.CloseGrey();
                    for (int i = 0; i < 50; i++)
                    {
                        if (this.revealCards[i] != null)
                        {
                            this.revealCards[i].clearControls();
                            this.greyout.removeControl(this.revealCards[i]);
                        }
                    }
                    bool flag = false;
                    int index = 0;
                    List<CustomSelfDrawPanel.UICard> list = new List<CustomSelfDrawPanel.UICard>();
                    string[] strArray3 = response.Strings.Split(";".ToCharArray());
                    int length = strArray3.Length;
                    int num4 = -10 * ((length / 5) - 1);
                    foreach (string str2 in strArray3)
                    {
                        string[] strArray4 = str2.Split(",".ToCharArray());
                        if (strArray4.Length == 2)
                        {
                            GameEngine.Instance.World.ProfileCards.Add(Convert.ToInt32(strArray4[0].Trim()), CardTypes.getCardDefinitionFromString(strArray4[1].Trim()));
                            CustomSelfDrawPanel.UICard item = makeUICard(CardTypes.getCardDefinitionFromString(strArray4[1].Trim()), Convert.ToInt32(strArray4[0].Trim()), GameEngine.Instance.World.getRank() + 1);
                            item.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardClickPlayTrueFromClickFalseValidate));
                            list.Add(item);
                        }
                    }
                    list.Sort(CustomSelfDrawPanel.UICard.cardsNameComparer);
                    foreach (CustomSelfDrawPanel.UICard card2 in list)
                    {
                        this.revealCards[index] = card2;
                        this.revealCards[index].Position = new Point((15 + ((index % 5) * (200 - (length / 5)))) + ((index / 5) * 5), (0x5f + num4) + (20 * (index / 5)));
                        this.greyout.addControl(this.revealCards[index]);
                        switch (this.revealCards[index].Definition.cardGrade)
                        {
                            case 0x200000:
                            case 0x400000:
                            case 0x80000:
                            case 0x100000:
                                flag = true;
                                break;
                        }
                        index++;
                    }
                    GFXLibrary.Instance.closeBigCardsLoader();
                    this.numRevealCards = index;
                    if (flag)
                    {
                        GameEngine.Instance.playInterfaceSound("BuyCardsPanel_found_rare_card");
                    }
                    this.OpenGrey();
                    this.openingPack = false;
                }
                catch (Exception exception)
                {
                    MyMessageBox.Show(exception.Message, SK.Text("GENERIC_Error", "Error"));
                }
            }
            this.openingPack = false;
        }

        public void preValidateCardToBePlayedCallBack(PreValidateCardToBePlayed_ReturnType returnData)
        {
            this.waitingResponse = false;
            if (returnData.Success)
            {
                this.returnDataRef = returnData;
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

        public void sh3Clicked()
        {
            string str = "";
            if (Program.mySettings.LanguageIdent == "ru")
            {
                str = "http://www.stronghold3.com/?lang=ru";
            }
            else if (Program.mySettings.LanguageIdent == "de")
            {
                str = "http://store.steampowered.com/app/47400?l=german";
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                str = "http://store.steampowered.com/app/47400?l=french";
            }
            else
            {
                str = "http://store.steampowered.com/app/47400";
            }
            new Process { StartInfo = { FileName = str } }.Start();
        }

        public void showMorePopup(int mode)
        {
            GameEngine.Instance.openSuperPackInfo(mode);
        }

        public void strongholdCollectionClicked()
        {
            string str = "";
            if (Program.mySettings.LanguageIdent == "de")
            {
                str = URLs.StrongholdCollectionLink_de;
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                str = URLs.StrongholdCollectionLink_fr;
            }
            else if (Program.mySettings.LanguageIdent == "ru")
            {
                str = URLs.StrongholdCollectionLink_ru;
            }
            else
            {
                str = URLs.StrongholdCollectionLink_en;
            }
            new Process { StartInfo = { FileName = str } }.Start();
        }

        public void update()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.diamondAnimStartTime);
            this.diamondAnimFrame = (int) (span.TotalMilliseconds / 33.0);
            for (int i = 0; i < this.numRevealCards; i++)
            {
                CustomSelfDrawPanel.UICard card = this.revealCards[i];
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

        public void UpdatePacks()
        {
            if (packimage == null)
            {
                packimage = new Bitmap(this.packWidth, 0x88);
                using (Graphics graphics = Graphics.FromImage(packimage))
                {
                    graphics.FillRectangle(Brushes.Green, 0, 0, packimage.Width, packimage.Height);
                }
            }
            foreach (CustomSelfDrawPanel.UICardPack pack in this.packControls.Values)
            {
                this.mainBackgroundImage.removeControl(pack);
            }
            this.packControls.Clear();
            this.AvailablePanelContent.invalidate();
            int packWidth = this.packWidth;
            int num2 = 0;
            foreach (CardTypes.UserCardPack pack2 in GameEngine.Instance.World.ProfileUserCardPacks.Values)
            {
                if (!this.packControls.ContainsKey(GameEngine.Instance.World.ProfileCardOffers[pack2.PackID].Category) && (pack2.Count > 0))
                {
                    num2++;
                }
            }
            if (num2 >= 8)
            {
                packWidth = 0x4b;
            }
            foreach (CardTypes.UserCardPack pack3 in GameEngine.Instance.World.ProfileUserCardPacks.Values)
            {
                if (!this.packControls.ContainsKey(GameEngine.Instance.World.ProfileCardOffers[pack3.PackID].Category) && (pack3.Count > 0))
                {
                    CustomSelfDrawPanel.UICardPack packControl = new CustomSelfDrawPanel.UICardPack {
                        baseImage = new CustomSelfDrawPanel.CSDImage()
                    };
                    packControl.baseImage.Image = packimage;
                    packControl.baseImage.Size = packimage.Size;
                    string str = string.Empty;
                    string str2 = string.Empty;
                    string iD = string.Empty;
                    int num3 = -1;
                    switch (GameEngine.Instance.World.ProfileCardOffers[pack3.PackID].Category)
                    {
                        case "FARMING":
                            str2 = "card_pack_food_standard_normal";
                            str = "card_pack_food_standard_over";
                            num3 = 0x283d;
                            iD = "CARD_OFFERS_Food_Pack";
                            break;

                        case "CASTLE":
                            str2 = "card_pack_castle_standard_normal";
                            str = "card_pack_castle_standard_over";
                            num3 = 0x283e;
                            iD = "CARD_OFFERS_Castle_Pack";
                            break;

                        case "DEFENSE":
                        case "DEFENCE":
                            str2 = "card_pack_defence_standard_normal";
                            str = "card_pack_defence_standard_over";
                            num3 = 0x283f;
                            iD = "CARD_OFFERS_Defense_Pack";
                            break;

                        case "ARMY":
                            str2 = "card_pack_army_standard_normal";
                            str = "card_pack_army_standard_over";
                            num3 = 0x2840;
                            iD = "CARD_OFFERS_Army_Pack";
                            break;

                        case "RANDOM":
                            str2 = "card_pack_random_standard_normal";
                            str = "card_pack_random_standard_over";
                            num3 = 0x2841;
                            iD = "CARD_OFFERS_Random_Pack";
                            break;

                        case "INDUSTRY":
                            str2 = "card_pack_Industry_standard_normal";
                            str = "card_pack_Industry_standard_over";
                            num3 = 0x2842;
                            iD = "CARD_OFFERS_Industry_Pack";
                            break;

                        case "EXCLUSIVE":
                            str2 = "card_pack_exclusive_silver_normal";
                            str = "card_pack_exclusive_silver_over";
                            num3 = 0x2844;
                            iD = "CARD_OFFERS_Exclusive_Pack";
                            break;

                        case "RESEARCH":
                            str2 = "card_pack_research_silver_normal";
                            str = "card_pack_research_silver_over";
                            num3 = 0x2843;
                            iD = "CARD_OFFERS_Research_Pack";
                            break;

                        case "SUPERFARMING":
                            str2 = "card_pack_food_silver_normal";
                            str = "card_pack_food_silver_over";
                            iD = "CARD_OFFERS_Super_Food_Pack";
                            num3 = 0x2845;
                            break;

                        case "SUPERDEFENSE":
                        case "SUPERDEFENCE":
                            str2 = "card_pack_defence_silver_normal";
                            str = "card_pack_defence_silver_over";
                            iD = "CARD_OFFERS_Super_Defense_Pack";
                            num3 = 0x2846;
                            break;

                        case "SUPERRANDOM":
                            str2 = "card_pack_random_silver_normal";
                            str = "card_pack_random_silver_over";
                            iD = "CARD_OFFERS_Super_Random_Pack";
                            num3 = 0x2848;
                            break;

                        case "SUPERINDUSTRY":
                            str2 = "card_pack_Industry_silver_normal";
                            str = "card_pack_Industry_silver_over";
                            iD = "CARD_OFFERS_Super_Industry_Pack";
                            num3 = 0x2849;
                            break;

                        case "SUPERARMY":
                            str2 = "card_pack_army_silver_normal";
                            str = "card_pack_army_silver_over";
                            iD = "CARD_OFFERS_Super_Army_Pack";
                            num3 = 0x2847;
                            break;

                        case "ULTIMATEFARMING":
                            str2 = "card_pack_food_gold_normal";
                            str = "card_pack_food_gold_over";
                            iD = "CARD_OFFERS_Ultimate_Food_Pack";
                            num3 = 0x284a;
                            break;

                        case "ULTIMATEDEFENSE":
                        case "ULTIMATEDEFENCE":
                            str2 = "card_pack_defence_gold_normal";
                            str = "card_pack_defence_gold_over";
                            iD = "CARD_OFFERS_Ultimate_Defense_Pack";
                            num3 = 0x284b;
                            break;

                        case "ULTIMATERANDOM":
                            str2 = "card_pack_random_gold_normal";
                            str = "card_pack_random_gold_over";
                            iD = "CARD_OFFERS_Ultimate_Random_Pack";
                            num3 = 0x284d;
                            break;

                        case "ULTIMATEINDUSTRY":
                            str2 = "card_pack_Industry_gold_normal";
                            str = "card_pack_Industry_gold_over";
                            iD = "CARD_OFFERS_Ultimate_Industry_Pack";
                            num3 = 0x284e;
                            break;

                        case "ULTIMATEARMY":
                            str2 = "card_pack_army_gold_normal";
                            str = "card_pack_army_gold_over";
                            iD = "CARD_OFFERS_Ultimate_Army_Pack";
                            num3 = 0x284c;
                            break;

                        case "PLATINUM":
                            str2 = "card_pack_army_gold_normal";
                            str = "card_pack_army_gold_over";
                            iD = "CARD_OFFERS_Platinum_Pack";
                            num3 = 0x2851;
                            break;

                        default:
                            str2 = "card_pack_Industry_standard_normal";
                            str = "card_pack_Industry_standard_over";
                            iD = "CARD_OFFERS_Industry_Pack";
                            break;
                    }
                    packControl.nameText = SK.Text(iD);
                    try
                    {
                        packControl.baseImage.Image = GFXLibrary.CardPackImages[str2];
                    }
                    catch (Exception)
                    {
                        packControl.baseImage.Image = GFXLibrary.CardPackImages["card_pack_open_Industry-Pack"];
                    }
                    try
                    {
                        packControl.overImage.Image = GFXLibrary.CardPackImages[str];
                    }
                    catch (Exception)
                    {
                        packControl.overImage.Image = GFXLibrary.CardPackImages["card_pack_open_Industry-Pack_over"];
                    }
                    packControl.addControl(packControl.baseImage);
                    packControl.addControl(packControl.overImage);
                    packControl.baseImage.Visible = true;
                    packControl.overImage.Visible = false;
                    packControl.CustomTooltipID = num3;
                    packControl.setMouseOverDelegate(delegate {
                        packControl.baseImage.Visible = false;
                        packControl.overImage.Visible = true;
                    }, delegate {
                        packControl.baseImage.Visible = true;
                        packControl.overImage.Visible = false;
                    });
                    packControl.Size = packControl.baseImage.Size;
                    if (this.packControls.Count > 0)
                    {
                        packControl.Position = new Point(this.AvailablePanel.X + ((packWidth + 4) * this.packControls.Count), (base.Height - 4) - packControl.Height);
                    }
                    else
                    {
                        packControl.Position = new Point(this.AvailablePanel.X, (base.Height - 4) - packControl.Height);
                    }
                    packControl.ClickArea = new Rectangle(8, 0, 0x4b, packControl.Height);
                    packControl.PackIDs.Add(pack3.PackID);
                    this.packControls.Add(GameEngine.Instance.World.ProfileCardOffers[pack3.PackID].Category, packControl);
                    continue;
                }
                if (pack3.Count > 0)
                {
                    this.packControls[GameEngine.Instance.World.ProfileCardOffers[pack3.PackID].Category].PackIDs.Add(pack3.PackID);
                }
            }
            foreach (CustomSelfDrawPanel.UICardPack pack4 in this.packControls.Values)
            {
                CustomSelfDrawPanel.CSDImage image;
                image = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.cardpanel_pack_open_circle,
                    Size = ((Image)GFXLibrary.cardpanel_pack_open_circle).Size,
                    Position = new Point((pack4.Width - ((Image)GFXLibrary.cardpanel_pack_open_circle).Width) - 4, (pack4.Height - ((Image)GFXLibrary.cardpanel_pack_open_circle).Height) - (((Image)GFXLibrary.cardpanel_pack_open_circle).Height / 2))
                };
                pack4.addControl(image);
                pack4.nameLabel = new CustomSelfDrawPanel.CSDLabel();
                int num4 = 0;
                foreach (CardTypes.UserCardPack pack5 in GameEngine.Instance.World.ProfileUserCardPacks.Values)
                {
                    if (pack4.PackIDs.Contains(pack5.PackID))
                    {
                        num4 += pack5.Count;
                    }
                }
                pack4.nameLabel.Text = num4.ToString();
                pack4.nameLabel.Position = new Point((image.X - 2) - 50, image.Y - 2);
                pack4.nameLabel.Size = new Size(image.Size.Width + 100, image.Size.Height);
                pack4.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                pack4.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                pack4.nameLabel.Color = ARGBColors.Black;
                pack4.addControl(pack4.nameLabel);
                if (pack4.PackIDs.Count > 0)
                {
                    pack4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OpenPack));
                    this.mainBackgroundImage.addControl(pack4);
                }
            }
            this.mainBackgroundImage.invalidate();
        }

        public void validateCardPossible(int cardType, int villageID)
        {
            RemoteServices.Instance.set_PreValidateCardToBePlayed_UserCallBack(new RemoteServices.PreValidateCardToBePlayed_UserCallBack(this.preValidateCardToBePlayedCallBack));
            RemoteServices.Instance.PreValidateCardToBePlayed(cardType, villageID);
        }
    }
}

