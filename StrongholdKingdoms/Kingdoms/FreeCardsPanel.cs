namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using Stronghold.AuthClient;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class FreeCardsPanel : CustomSelfDrawPanel
    {
        private static FreeCardsPanel _lastInstance = null;
        private CustomSelfDrawPanel.CSDButton buyCrownsButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage cardBackImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage cards10Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards10Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards6Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards7Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards7Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards8Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards8Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage cards9Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cards9Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel comingsoon = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel comingSoon10Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel comingSoon6Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel comingSoon7Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel comingSoon8Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel comingSoon9Label = new CustomSelfDrawPanel.CSDLabel();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage crestImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel description10Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description6Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description7Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description8Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel description9Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage fanImage = new CustomSelfDrawPanel.CSDImage();
        private FreeCardsData freeCardInfo;
        private CustomSelfDrawPanel.CSDExtendingPanel greenArea = new CustomSelfDrawPanel.CSDExtendingPanel();
        private bool inIncreaseLevel;
        private CustomSelfDrawPanel.CSDLabel labelFreeCards = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelNextCards = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelVeteranLevel = new CustomSelfDrawPanel.CSDLabel();
        private DateTime lastIncreaseLevelClick = DateTime.MinValue;
        private static DateTime lastVeteranLevelTime = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDButton level10Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level6Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level7Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level8Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton level9Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel MainPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDImage parchmentImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton revealButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage tick10Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick7Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick8Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage tick9Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDHorzProgressBar timeProgress = new CustomSelfDrawPanel.CSDHorzProgressBar();

        public FreeCardsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void buyCrowns()
        {
            InterfaceMgr.Instance.openPlayCardsWindow(0);
            ((PlayCardsWindow) InterfaceMgr.Instance.getCardWindow()).GetCrowns();
        }

        private void cardClicked()
        {
            InterfaceMgr.Instance.closeFreeCardsPopup();
            InterfaceMgr.Instance.openPlayCardsWindow(0);
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closeFreeCardsPopup();
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

        private void friendClicked()
        {
            string fileName = URLs.InviteAFriendURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.LanguageIdent.ToLower() + "&colour=" + GFXLibrary.invite_ad_colour.ToString();
            try
            {
                Process.Start(fileName);
            }
            catch (Exception)
            {
                MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
            }
        }

        private Color getButtonTextColour(int row)
        {
            if (this.isButtonEnabled(row))
            {
                return ARGBColors.Black;
            }
            return ARGBColors.Gray;
        }

        private BaseImage getCardImage(int row)
        {
            int index = 0;
            switch (row)
            {
                case 0:
                    index = 0;
                    break;

                case 1:
                    index = 2;
                    break;

                case 2:
                    index = 4;
                    break;

                case 3:
                    index = 6;
                    break;

                case 4:
                    index = 8;
                    break;

                case 5:
                    index = 8;
                    break;

                case 6:
                    index = 8;
                    break;

                case 7:
                    index = 8;
                    break;

                case 8:
                    index = 10;
                    break;

                case 9:
                    index = 12;
                    break;
            }
            if ((row + 1) == this.freeCardInfo.CurrentVeteranLevel)
            {
                index++;
            }
            return GFXLibrary.free_card_screen_cardback_array[index];
        }

        private Color getTextColour(int row)
        {
            if (this.freeCardInfo.VeteranStages[row])
            {
                return ARGBColors.Green;
            }
            return ARGBColors.Gray;
        }

        private BaseImage getTickImage(int row)
        {
            if (this.freeCardInfo.VeteranStages[row])
            {
                return GFXLibrary.checkbox_checked;
            }
            return GFXLibrary.checkbox_unchecked;
        }

        private void getVeteranLevelCallback(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode.Value == 1)
            {
                int? nullable3;
                int? nullable5;
                int? nullable7;
                int? nullable9;
                int? nullable11;
                int? nullable13;
                int? nullable15;
                int? nullable17;
                int? nullable19;
                int? nullable21;
                bool[] flagArray2 = new bool[10];

                nullable3 = null;
                nullable5 = null;
                nullable7 = null;
                nullable9 = null;
                nullable11 = null;
                nullable13 = null;
                nullable15 = null;
                nullable17 = null;
                nullable19 = null;
                nullable21 = null;
                if (((XmlRpcCardsResponse) response).VeteranLevel1.HasValue)
                {
                    nullable3 = ((XmlRpcCardsResponse) response).VeteranLevel1;
                }
                flagArray2[0] = (nullable3.GetValueOrDefault() == 1) && nullable3.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel2.HasValue)
                {
                    nullable5 = ((XmlRpcCardsResponse) response).VeteranLevel2;
                }
                flagArray2[1] = (nullable5.GetValueOrDefault() == 1) && nullable5.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel3.HasValue)
                {
                    nullable7 = ((XmlRpcCardsResponse) response).VeteranLevel3;
                }
                flagArray2[2] = (nullable7.GetValueOrDefault() == 1) && nullable7.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel4.HasValue)
                {
                    nullable9 = ((XmlRpcCardsResponse) response).VeteranLevel4;
                }
                flagArray2[3] = (nullable9.GetValueOrDefault() == 1) && nullable9.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel5.HasValue)
                {
                    nullable11 = ((XmlRpcCardsResponse) response).VeteranLevel5;
                }
                flagArray2[4] = (nullable11.GetValueOrDefault() == 1) && nullable11.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel6.HasValue)
                {
                    nullable13 = ((XmlRpcCardsResponse) response).VeteranLevel6;
                }
                flagArray2[5] = (nullable13.GetValueOrDefault() == 1) && nullable13.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel7.HasValue)
                {
                    nullable15 = ((XmlRpcCardsResponse) response).VeteranLevel7;
                }
                flagArray2[6] = (nullable15.GetValueOrDefault() == 1) && nullable15.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel8.HasValue)
                {
                    nullable17 = ((XmlRpcCardsResponse) response).VeteranLevel8;
                }
                flagArray2[7] = (nullable17.GetValueOrDefault() == 1) && nullable17.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel9.HasValue)
                {
                    nullable19 = ((XmlRpcCardsResponse) response).VeteranLevel9;
                }
                flagArray2[8] = (nullable19.GetValueOrDefault() == 1) && nullable19.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel10.HasValue)
                {
                    nullable21 = ((XmlRpcCardsResponse) response).VeteranLevel10;
                }
                flagArray2[9] = (nullable21.GetValueOrDefault() == 1) && nullable21.HasValue;
                bool[] stages = flagArray2;
                GameEngine.Instance.World.importFreeCardData(((XmlRpcCardsResponse) response).VeteranCurrentLevel.Value, stages, DateTime.Now.AddSeconds((double) ((XmlRpcCardsResponse) response).VeteranSecondsLeft.Value), DateTime.Now);
            }
            this.init(false);
        }

        private void increaseLevel()
        {
            if (this.freeCardInfo.CurrentVeteranLevel < 10)
            {
                if (this.inIncreaseLevel)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastIncreaseLevelClick);
                    if (span.TotalSeconds < 60.0)
                    {
                        return;
                    }
                }
                this.inIncreaseLevel = true;
                this.lastIncreaseLevelClick = DateTime.Now;
                XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath).veteranLevelUp(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")), new CardsEndResponseDelegate(this.increaseLevelCallback), this);
            }
        }

        private void increaseLevelCallback(ICardsProvider provider, ICardsResponse response)
        {
            this.inIncreaseLevel = false;
            if (response.SuccessCode.Value == 1)
            {
                int? nullable3;
                int? nullable5;
                int? nullable7;
                int? nullable9;
                int? nullable11;
                int? nullable13;
                int? nullable15;
                int? nullable17;
                int? nullable19;
                int? nullable21;
                bool[] flagArray2 = new bool[10];

                nullable3 = null;
                nullable5 = null;
                nullable7 = null;
                nullable9 = null;
                nullable11 = null;
                nullable13 = null;
                nullable15 = null;
                nullable17 = null;
                nullable19 = null;
                nullable21 = null;
                if (((XmlRpcCardsResponse)response).VeteranLevel1.HasValue)
                {
                    nullable3 = ((XmlRpcCardsResponse) response).VeteranLevel1;
                }
                flagArray2[0] = (nullable3.GetValueOrDefault() == 1) && nullable3.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel2.HasValue)
                {
                    nullable5 = ((XmlRpcCardsResponse) response).VeteranLevel2;
                }
                flagArray2[1] = (nullable5.GetValueOrDefault() == 1) && nullable5.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel3.HasValue)
                {
                    nullable7 = ((XmlRpcCardsResponse) response).VeteranLevel3;
                }
                flagArray2[2] = (nullable7.GetValueOrDefault() == 1) && nullable7.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel4.HasValue)
                {
                    nullable9 = ((XmlRpcCardsResponse) response).VeteranLevel4;
                }
                flagArray2[3] = (nullable9.GetValueOrDefault() == 1) && nullable9.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel5.HasValue)
                {
                    nullable11 = ((XmlRpcCardsResponse) response).VeteranLevel5;
                }
                flagArray2[4] = (nullable11.GetValueOrDefault() == 1) && nullable11.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel6.HasValue)
                {
                    nullable13 = ((XmlRpcCardsResponse) response).VeteranLevel6;
                }
                flagArray2[5] = (nullable13.GetValueOrDefault() == 1) && nullable13.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel7.HasValue)
                {
                    nullable15 = ((XmlRpcCardsResponse) response).VeteranLevel7;
                }
                flagArray2[6] = (nullable15.GetValueOrDefault() == 1) && nullable15.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel8.HasValue)
                {
                    nullable17 = ((XmlRpcCardsResponse) response).VeteranLevel8;
                }
                flagArray2[7] = (nullable17.GetValueOrDefault() == 1) && nullable17.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel9.HasValue)
                {
                    nullable19 = ((XmlRpcCardsResponse) response).VeteranLevel9;
                }
                flagArray2[8] = (nullable19.GetValueOrDefault() == 1) && nullable19.HasValue;
                if (((XmlRpcCardsResponse) response).VeteranLevel10.HasValue)
                {
                    nullable21 = ((XmlRpcCardsResponse) response).VeteranLevel10;
                }
                flagArray2[9] = (nullable21.GetValueOrDefault() == 1) && nullable21.HasValue;
                bool[] stages = flagArray2;
                GameEngine.Instance.World.importFreeCardData(((XmlRpcCardsResponse) response).VeteranCurrentLevel.Value, stages, DateTime.Now.AddSeconds((double) ((XmlRpcCardsResponse) response).VeteranSecondsLeft.Value), DateTime.Now);
            }
            else
            {
                MyMessageBox.Show(SK.Text("FreeCardsPanel_Unable_Increase", "Unable to increase level."), SK.Text("GENERIC_Error", "Error"));
            }
            this.init(false);
        }

        public void init(bool initialCall)
        {
            CustomSelfDrawPanel.CSDImage image2;
            this.freeCardInfo = GameEngine.Instance.World.FreeCardInfo;
            if (!this.freeCardInfo.VeteranStages[0] && initialCall)
            {
                RemoteServices.Instance.set_InitialiseFreeCards_UserCallBack(new RemoteServices.InitialiseFreeCards_UserCallBack(this.initialiseFreeCardsCallback));
                RemoteServices.Instance.InitialiseFreeCards();
            }
            else if (initialCall && (DateTime.Now.Subtract(lastVeteranLevelTime).TotalSeconds > 120.0))
            {
                this.UpdateVeteranLevelData();
                lastVeteranLevelTime = DateTime.Now;
            }
            TimeSpan span = this.freeCardInfo.timeUntilNextFreeCard();
            bool flag = false;
            if (((span.TotalSeconds <= 0.0) && this.freeCardInfo.VeteranStages[0]) && (this.freeCardInfo.CurrentVeteranLevel > 0))
            {
                flag = true;
            }
            int num = 0x2e;
            base.clearControls();
            this.mainBackgroundImage.Image = GFXLibrary.dummy;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.MainPanel.Size = base.Size;
            this.MainPanel.Position = new Point(0, 0);
            this.mainBackgroundImage.addControl(this.MainPanel);
            this.MainPanel.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
            CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left,
                Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size,
                Position = new Point(0, 0)
            };
            this.MainPanel.addControl(control);
            image2 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right,
                Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size,
                Position = new Point((this.MainPanel.Width - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Width) - 6, (this.MainPanel.Height - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Height) - 6)
            };
            this.MainPanel.addControl(image2);
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.closeImage.CustomTooltipID = 0x2774;
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            CustomSelfDrawPanel.CSDFill fill2 = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x74)
            };
            this.mainBackgroundImage.addControl(fill2);
            this.labelTitle.Position = new Point(0x1b, 5);
            this.labelTitle.Size = new Size(600, 0x40);
            this.labelTitle.Text = SK.Text("FreeCardsPanel_Free_Cards", "Free Cards");
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            if ((this.freeCardInfo.CurrentVeteranLevel > 0) && !flag)
            {
                this.greenArea.Size = new Size(0x3bb, num * this.freeCardInfo.CurrentVeteranLevel);
                this.greenArea.Position = new Point(20, 0x7b);
                this.mainBackgroundImage.addControl(this.greenArea);
                this.greenArea.Create((Image) GFXLibrary.free_card_screen_green_panel_top_left, (Image) GFXLibrary.free_card_screen_green_panel_top_mid, (Image) GFXLibrary.free_card_screen_green_panel_top_right, (Image) GFXLibrary.free_card_screen_green_panel_mid_left, (Image) GFXLibrary.free_card_screen_green_panel_mid_mid, (Image) GFXLibrary.free_card_screen_green_panel_mid_right, (Image) GFXLibrary.free_card_screen_green_panel_bottom_left, (Image) GFXLibrary.free_card_screen_green_panel_bottom_mid, (Image) GFXLibrary.free_card_screen_green_panel_bottom_right);
            }
            this.timeProgress.Position = new Point(0x80, 0x55);
            this.timeProgress.Size = new Size(210, 6);
            this.mainBackgroundImage.addControl(this.timeProgress);
            this.timeProgress.Create((Image) GFXLibrary.free_card_screen_progbar_left, (Image) GFXLibrary.free_card_screen_progbar_mid, (Image) GFXLibrary.free_card_screen_progbar_right, (Image) GFXLibrary.free_card_screen_progbar_fill, (Image) GFXLibrary.free_card_screen_progbar_fill, (Image) GFXLibrary.free_card_screen_progbar_fill);
            this.timeProgress.setValues(this.freeCardInfo.durationHours() - span.TotalHours, this.freeCardInfo.durationHours());
            this.labelVeteranLevel.Position = new Point(0x7d, 0x24);
            this.labelVeteranLevel.Size = new Size(600, 0x40);
            this.labelVeteranLevel.Text = SK.Text("FreeCardsPanel_Level", "Kingdoms Veteran Level") + " : " + this.freeCardInfo.CurrentVeteranLevel.ToString();
            this.labelVeteranLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelVeteranLevel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.labelVeteranLevel.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelVeteranLevel);
            this.labelFreeCards.Position = new Point(0x80, 0x40);
            this.labelFreeCards.Size = new Size(600, 0x40);
            this.labelFreeCards.Text = SK.Text("FreeCardsPanel_Cards_Per_Week", "Cards per week") + " : " + this.freeCardInfo.freeCardsPerWeek().ToString();
            this.labelFreeCards.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelFreeCards.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.labelFreeCards.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelFreeCards);
            string str = SK.Text("FreeCardsPanel_Next_Card", "Next Card") + ": ";
            if (((span.TotalSeconds >= 1.0) && (span.TotalDays < 100.0)) && (this.freeCardInfo.CurrentVeteranLevel > 0))
            {
                str = str + VillageMap.createBuildTimeString((int) span.TotalSeconds);
            }
            this.labelNextCards.Position = new Point(0x80, 0x61);
            this.labelNextCards.Size = new Size(600, 0x40);
            this.labelNextCards.Text = str;
            this.labelNextCards.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelNextCards.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.labelNextCards.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelNextCards);
            if (this.freeCardInfo.CurrentVeteranLevel > 0)
            {
                this.crestImage.Image = (Image) GFXLibrary.free_card_screen_wax_array[this.freeCardInfo.CurrentVeteranLevel - 1];
                this.crestImage.Position = new Point(0x15, 0x23);
                this.mainBackgroundImage.addControl(this.crestImage);
            }
            if (!flag)
            {
                int x = 0x19;
                int y = 0x7b;
                int num4 = 0x9b;
                int num5 = 0x83;
                int num6 = 190;
                int num7 = 0x87;
                int num8 = 540;
                int num9 = 0x83;
                this.cards1Image.Image = (Image) this.getCardImage(0);
                this.cards1Image.Position = new Point(x, y);
                this.mainBackgroundImage.addControl(this.cards1Image);
                this.cards1Label.Position = new Point(-5, -5);
                this.cards1Label.Size = this.cards1Image.Size;
                this.cards1Label.Text = "1";
                this.cards1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards1Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards1Label.Color = ARGBColors.White;
                this.cards1Label.DropShadowColor = ARGBColors.Black;
                this.cards1Image.addControl(this.cards1Label);
                this.tick1Image.Image = (Image) this.getTickImage(0);
                this.tick1Image.Position = new Point(num4, num5);
                this.mainBackgroundImage.addControl(this.tick1Image);
                this.description1Label.Position = new Point(num6, num7);
                this.description1Label.Size = new Size(600, 0x40);
                this.description1Label.Text = SK.Text("FreeCardsPanel__Site_Village", "Site Village");
                this.description1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description1Label.Color = this.getTextColour(0);
                this.mainBackgroundImage.addControl(this.description1Label);
                this.level1Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                this.level1Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                this.level1Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                this.level1Button.Position = new Point(num8, num9);
                this.level1Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                this.level1Button.TextYOffset = -2;
                this.level1Button.Text.Color = this.getButtonTextColour(0);
                this.level1Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.level1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                this.level1Button.Data = 0;
                this.level1Button.Visible = this.isButtonVisible(0);
                this.level1Button.Enabled = this.isButtonEnabled(0);
                this.mainBackgroundImage.addControl(this.level1Button);
                this.cards2Image.Image = (Image) this.getCardImage(1);
                this.cards2Image.Position = new Point(x, y + num);
                this.mainBackgroundImage.addControl(this.cards2Image);
                this.cards2Label.Position = new Point(-5, -5);
                this.cards2Label.Size = this.cards1Image.Size;
                this.cards2Label.Text = "2";
                this.cards2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards2Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards2Label.Color = ARGBColors.White;
                this.cards2Label.DropShadowColor = ARGBColors.Black;
                this.cards2Image.addControl(this.cards2Label);
                this.tick2Image.Image = (Image) this.getTickImage(1);
                this.tick2Image.Position = new Point(num4, num5 + num);
                this.mainBackgroundImage.addControl(this.tick2Image);
                this.description2Label.Position = new Point(num6, num7 + num);
                this.description2Label.Size = new Size(600, 0x40);
                this.description2Label.Text = SK.Text("FreeCardsPanel_Rank_Yokel", "Get to Rank of Yokel") + " (3)";
                this.description2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description2Label.Color = this.getTextColour(1);
                this.mainBackgroundImage.addControl(this.description2Label);
                this.level2Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                this.level2Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                this.level2Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                this.level2Button.Position = new Point(num8, num9 + num);
                this.level2Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                this.level2Button.TextYOffset = -2;
                this.level2Button.Text.Color = this.getButtonTextColour(1);
                this.level2Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.level2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                this.level2Button.Data = 1;
                this.level2Button.Visible = this.isButtonVisible(1);
                this.level2Button.Enabled = this.isButtonEnabled(1);
                this.mainBackgroundImage.addControl(this.level2Button);
                this.cards3Image.Image = (Image) this.getCardImage(2);
                this.cards3Image.Position = new Point(x, y + (2 * num));
                this.mainBackgroundImage.addControl(this.cards3Image);
                this.cards3Label.Position = new Point(-5, -5);
                this.cards3Label.Size = this.cards1Image.Size;
                this.cards3Label.Text = "3";
                this.cards3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards3Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards3Label.Color = ARGBColors.White;
                this.cards3Label.DropShadowColor = ARGBColors.Black;
                this.cards3Image.addControl(this.cards3Label);
                this.tick3Image.Image = (Image) this.getTickImage(2);
                this.tick3Image.Position = new Point(num4, num5 + (2 * num));
                this.mainBackgroundImage.addControl(this.tick3Image);
                this.description3Label.Position = new Point(num6, num7 + (2 * num));
                this.description3Label.Size = new Size(600, 0x40);
                this.description3Label.Text = SK.Text("FreeCardsPanel_Rank_Villein", "Get to Rank of Villein") + " (6)";
                this.description3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description3Label.Color = this.getTextColour(2);
                this.mainBackgroundImage.addControl(this.description3Label);
                this.level3Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                this.level3Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                this.level3Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                this.level3Button.Position = new Point(num8, num9 + (2 * num));
                this.level3Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                this.level3Button.TextYOffset = -2;
                this.level3Button.Text.Color = this.getButtonTextColour(2);
                this.level3Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.level3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                this.level3Button.Data = 2;
                this.level3Button.Visible = this.isButtonVisible(2);
                this.level3Button.Enabled = this.isButtonEnabled(2);
                this.mainBackgroundImage.addControl(this.level3Button);
                this.cards4Image.Image = (Image) this.getCardImage(3);
                this.cards4Image.Position = new Point(x, y + (3 * num));
                this.mainBackgroundImage.addControl(this.cards4Image);
                this.cards4Label.Position = new Point(-5, -5);
                this.cards4Label.Size = this.cards1Image.Size;
                this.cards4Label.Text = "4";
                this.cards4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards4Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards4Label.Color = ARGBColors.White;
                this.cards4Label.DropShadowColor = ARGBColors.Black;
                this.cards4Image.addControl(this.cards4Label);
                this.tick4Image.Image = (Image) this.getTickImage(3);
                this.tick4Image.Position = new Point(num4, num5 + (3 * num));
                this.mainBackgroundImage.addControl(this.tick4Image);
                this.description4Label.Position = new Point(num6, num7 + (3 * num));
                this.description4Label.Size = new Size(600, 0x40);
                this.description4Label.Text = SK.Text("FreeCardsPanel_First_Purchase_Crowns", "First Purchase of Crowns");
                this.description4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description4Label.Color = this.getTextColour(3);
                this.mainBackgroundImage.addControl(this.description4Label);
                this.level4Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                this.level4Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                this.level4Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                this.level4Button.Position = new Point(num8, num9 + (3 * num));
                this.level4Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                this.level4Button.TextYOffset = -2;
                this.level4Button.Text.Color = this.getButtonTextColour(3);
                this.level4Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.level4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                this.level4Button.Data = 3;
                this.level4Button.Visible = this.isButtonVisible(3);
                this.level4Button.Enabled = this.isButtonEnabled(3);
                this.mainBackgroundImage.addControl(this.level4Button);
                this.cards5Image.Image = (Image) this.getCardImage(4);
                this.cards5Image.Position = new Point(x, y + (4 * num));
                this.mainBackgroundImage.addControl(this.cards5Image);
                this.cards5Label.Position = new Point(-5, -5);
                this.cards5Label.Size = this.cards1Image.Size;
                this.cards5Label.Text = "5";
                this.cards5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards5Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards5Label.Color = ARGBColors.White;
                this.cards5Label.DropShadowColor = ARGBColors.Black;
                this.cards5Image.addControl(this.cards5Label);
                this.tick5Image.Image = (Image) this.getTickImage(4);
                this.tick5Image.Position = new Point(num4, num5 + (4 * num));
                this.mainBackgroundImage.addControl(this.tick5Image);
                this.description5Label.Position = new Point(num6, num7 + (4 * num));
                this.description5Label.Size = new Size(600, 0x40);
                this.description5Label.Text = SK.Text("FreeCardsPanel_Rank_Commoner", "Get to Rank of Commoner") + " (9)";
                this.description5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description5Label.Color = this.getTextColour(4);
                this.mainBackgroundImage.addControl(this.description5Label);
                this.level5Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                this.level5Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                this.level5Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                this.level5Button.Position = new Point(num8, num9 + (4 * num));
                this.level5Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                this.level5Button.TextYOffset = -2;
                this.level5Button.Text.Color = this.getButtonTextColour(4);
                this.level5Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.level5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                this.level5Button.Data = 4;
                this.level5Button.Visible = this.isButtonVisible(4);
                this.level5Button.Enabled = this.isButtonEnabled(4);
                this.mainBackgroundImage.addControl(this.level5Button);
                this.cards6Image.Image = (Image) this.getCardImage(5);
                this.cards6Image.Position = new Point(x, y + (5 * num));
                this.mainBackgroundImage.addControl(this.cards6Image);
                this.cards6Label.Position = new Point(-5, -5);
                this.cards6Label.Size = this.cards1Image.Size;
                this.cards6Label.Text = "6";
                this.cards6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards6Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards6Label.Color = ARGBColors.White;
                this.cards6Label.DropShadowColor = ARGBColors.Black;
                this.cards6Image.addControl(this.cards6Label);
                this.tick6Image.Image = (Image) this.getTickImage(5);
                this.tick6Image.Position = new Point(num4, num5 + (5 * num));
                this.mainBackgroundImage.addControl(this.tick6Image);
                this.description6Label.Position = new Point(num6, num7 + (5 * num));
                this.description6Label.Size = new Size(600, 0x40);
                this.description6Label.Text = SK.Text("FreeCardsPanel_Donate_Resources", "Donate Resources to your Parish Capital");
                this.description6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description6Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description6Label.Color = this.getTextColour(5);
                this.mainBackgroundImage.addControl(this.description6Label);
                if (!GameEngine.Instance.World.InviteSystemNotImplemented)
                {
                    this.level6Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                    this.level6Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                    this.level6Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                    this.level6Button.Position = new Point(num8, num9 + (5 * num));
                    this.level6Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                    this.level6Button.TextYOffset = -2;
                    this.level6Button.Text.Color = this.getButtonTextColour(5);
                    this.level6Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.level6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                    this.level6Button.Data = 5;
                    this.level6Button.Visible = this.isButtonVisible(5);
                    this.level6Button.Enabled = this.isButtonEnabled(5);
                    this.mainBackgroundImage.addControl(this.level6Button);
                }
                this.cards7Image.Image = (Image) this.getCardImage(6);
                this.cards7Image.Position = new Point(x, y + (6 * num));
                this.mainBackgroundImage.addControl(this.cards7Image);
                this.cards7Label.Position = new Point(-5, -5);
                this.cards7Label.Size = this.cards1Image.Size;
                this.cards7Label.Text = "7";
                this.cards7Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards7Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards7Label.Color = ARGBColors.White;
                this.cards7Label.DropShadowColor = ARGBColors.Black;
                this.cards7Image.addControl(this.cards7Label);
                this.tick7Image.Image = (Image) this.getTickImage(6);
                this.tick7Image.Position = new Point(num4, num5 + (6 * num));
                this.mainBackgroundImage.addControl(this.tick7Image);
                this.description7Label.Position = new Point(num6, num7 + (6 * num));
                this.description7Label.Size = new Size(600, 0x40);
                this.description7Label.Text = SK.Text("FreeCardsPanel_Rank_Yeoman", "Get to Rank of Yeoman") + " (11)";
                this.description7Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description7Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description7Label.Color = this.getTextColour(6);
                this.mainBackgroundImage.addControl(this.description7Label);
                if (!GameEngine.Instance.World.InviteSystemNotImplemented)
                {
                    this.level7Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                    this.level7Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                    this.level7Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                    this.level7Button.Position = new Point(num8, num9 + (6 * num));
                    this.level7Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                    this.level7Button.TextYOffset = -2;
                    this.level7Button.Text.Color = this.getButtonTextColour(6);
                    this.level7Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.level7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                    this.level7Button.Data = 6;
                    this.level7Button.Visible = this.isButtonVisible(6);
                    this.level7Button.Enabled = this.isButtonEnabled(6);
                    this.mainBackgroundImage.addControl(this.level7Button);
                }
                this.cards8Image.Image = (Image) this.getCardImage(7);
                this.cards8Image.Position = new Point(x, y + (7 * num));
                this.mainBackgroundImage.addControl(this.cards8Image);
                this.cards8Label.Position = new Point(-5, -5);
                this.cards8Label.Size = this.cards1Image.Size;
                this.cards8Label.Text = "8";
                this.cards8Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards8Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards8Label.Color = ARGBColors.White;
                this.cards8Label.DropShadowColor = ARGBColors.Black;
                this.cards8Image.addControl(this.cards8Label);
                this.tick8Image.Image = (Image) this.getTickImage(7);
                this.tick8Image.Position = new Point(num4, num5 + (7 * num));
                this.mainBackgroundImage.addControl(this.tick8Image);
                this.description8Label.Position = new Point(num6, num7 + (7 * num));
                this.description8Label.Size = new Size(600, 0x40);
                this.description8Label.Text = SK.Text("FreeCardsPanel_Factions", "Create or Join a Faction");
                this.description8Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description8Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description8Label.Color = this.getTextColour(7);
                this.mainBackgroundImage.addControl(this.description8Label);
                if (!GameEngine.Instance.World.InviteSystemNotImplemented)
                {
                    this.level8Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                    this.level8Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                    this.level8Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                    this.level8Button.Position = new Point(num8, num9 + (7 * num));
                    this.level8Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                    this.level8Button.TextYOffset = -2;
                    this.level8Button.Text.Color = this.getButtonTextColour(7);
                    this.level8Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.level8Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                    this.level8Button.Data = 7;
                    this.level8Button.Visible = this.isButtonVisible(7);
                    this.level8Button.Enabled = this.isButtonEnabled(7);
                    this.mainBackgroundImage.addControl(this.level8Button);
                }
                this.cards9Image.Image = (Image) this.getCardImage(8);
                this.cards9Image.Position = new Point(x, y + (8 * num));
                this.mainBackgroundImage.addControl(this.cards9Image);
                this.cards9Label.Position = new Point(-6, -5);
                this.cards9Label.Size = this.cards1Image.Size;
                this.cards9Label.Text = "10";
                this.cards9Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards9Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards9Label.Color = ARGBColors.White;
                this.cards9Label.DropShadowColor = ARGBColors.Black;
                this.cards9Image.addControl(this.cards9Label);
                this.tick9Image.Image = (Image) this.getTickImage(8);
                this.tick9Image.Position = new Point(num4, num5 + (8 * num));
                this.mainBackgroundImage.addControl(this.tick9Image);
                this.description9Label.Position = new Point(num6, num7 + (8 * num));
                this.description9Label.Size = new Size(600, 0x40);
                this.description9Label.Text = SK.Text("FreeCardsPanel_Second_Crowns", "Second purchase of Crowns");
                this.description9Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description9Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description9Label.Color = this.getTextColour(8);
                this.mainBackgroundImage.addControl(this.description9Label);
                if (!GameEngine.Instance.World.InviteSystemNotImplemented)
                {
                    this.level9Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                    this.level9Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                    this.level9Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                    this.level9Button.Position = new Point(num8, num9 + (8 * num));
                    this.level9Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                    this.level9Button.TextYOffset = -2;
                    this.level9Button.Text.Color = this.getButtonTextColour(8);
                    this.level9Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.level9Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                    this.level9Button.Data = 8;
                    this.level9Button.Visible = this.isButtonVisible(8);
                    this.level9Button.Enabled = this.isButtonEnabled(8);
                    this.mainBackgroundImage.addControl(this.level9Button);
                }
                this.cards10Image.Image = (Image) this.getCardImage(9);
                this.cards10Image.Position = new Point(x, y + (9 * num));
                this.mainBackgroundImage.addControl(this.cards10Image);
                this.cards10Label.Position = new Point(-6, -5);
                this.cards10Label.Size = this.cards1Image.Size;
                this.cards10Label.Text = "14";
                this.cards10Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.cards10Label.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
                this.cards10Label.Color = ARGBColors.White;
                this.cards10Label.DropShadowColor = ARGBColors.Black;
                this.cards10Image.addControl(this.cards10Label);
                this.tick10Image.Image = (Image) this.getTickImage(9);
                this.tick10Image.Position = new Point(num4, num5 + (9 * num));
                this.mainBackgroundImage.addControl(this.tick10Image);
                this.description10Label.Position = new Point(num6, num7 + (9 * num));
                this.description10Label.Size = new Size(600, 0x40);
                this.description10Label.Text = SK.Text("FreeCardsPanel_Rank_Squire", "Get to Rank of Squire") + " (15)";
                this.description10Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.description10Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.description10Label.Color = this.getTextColour(9);
                this.mainBackgroundImage.addControl(this.description10Label);
                if (!GameEngine.Instance.World.InviteSystemNotImplemented)
                {
                    this.level10Button.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                    this.level10Button.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                    this.level10Button.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                    this.level10Button.Position = new Point(num8, num9 + (9 * num));
                    this.level10Button.Text.Text = SK.Text("FreeCardsPanel_Increase_Level", "Increase Level");
                    this.level10Button.TextYOffset = -2;
                    this.level10Button.Text.Color = this.getButtonTextColour(9);
                    this.level10Button.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.level10Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.increaseLevel), "FreeCardsPanel_increase_level");
                    this.level10Button.Data = 9;
                    this.level10Button.Visible = this.isButtonVisible(9);
                    this.level10Button.Enabled = this.isButtonEnabled(9);
                    this.mainBackgroundImage.addControl(this.level10Button);
                }
                if ((this.freeCardInfo.VeteranStages[2] && !this.isButtonVisible(3)) && !this.freeCardInfo.VeteranStages[3])
                {
                    this.buyCrownsButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                    this.buyCrownsButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                    this.buyCrownsButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                    this.buyCrownsButton.Position = new Point(num8, num9 + (3 * num));
                    this.buyCrownsButton.Text.Text = SK.Text("BuyCrownsPanel_Buy_Crowns", "Buy Crowns");
                    this.buyCrownsButton.TextYOffset = -2;
                    this.buyCrownsButton.Text.Color = ARGBColors.Black;
                    this.buyCrownsButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.buyCrownsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyCrowns));
                    this.buyCrownsButton.Data = 3;
                    this.buyCrownsButton.Visible = true;
                    this.buyCrownsButton.Enabled = true;
                    this.mainBackgroundImage.addControl(this.buyCrownsButton);
                }
                else if ((this.freeCardInfo.VeteranStages[7] && !this.isButtonVisible(8)) && !this.freeCardInfo.VeteranStages[8])
                {
                    this.buyCrownsButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                    this.buyCrownsButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                    this.buyCrownsButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                    this.buyCrownsButton.Position = new Point(num8, num9 + (8 * num));
                    this.buyCrownsButton.Text.Text = SK.Text("BuyCrownsPanel_Buy_Crowns", "Buy Crowns");
                    this.buyCrownsButton.TextYOffset = -2;
                    this.buyCrownsButton.Text.Color = ARGBColors.Black;
                    this.buyCrownsButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.buyCrownsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyCrowns));
                    this.buyCrownsButton.Data = 3;
                    this.buyCrownsButton.Visible = true;
                    this.buyCrownsButton.Enabled = true;
                    this.mainBackgroundImage.addControl(this.buyCrownsButton);
                }
            }
            else
            {
                this.parchmentImage.Image = (Image) GFXLibrary.you_got_free_card_screen_parchment;
                this.parchmentImage.Position = new Point(0xaf, 0x7d);
                this.mainBackgroundImage.addControl(this.parchmentImage);
                this.cardBackImage.Image = (Image) GFXLibrary.you_got_free_card_screen_cardback;
                this.cardBackImage.Position = new Point(0xe2, 40);
                this.parchmentImage.addControl(this.cardBackImage);
                this.revealButton.ImageNorm = (Image) GFXLibrary.cardpanel_button_blue_normal;
                this.revealButton.ImageOver = (Image) GFXLibrary.cardpanel_button_blue_over;
                this.revealButton.ImageClick = (Image) GFXLibrary.cardpanel_button_blue_pressed;
                this.revealButton.Position = new Point(0xf8, 310);
                this.revealButton.Text.Text = SK.Text("FreeCardsPanel_Reveal", "Reveal");
                this.revealButton.TextYOffset = -2;
                this.revealButton.Text.Color = ARGBColors.Black;
                this.revealButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.revealButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.revealCard), "FreeCardsPanel_reveal_card");
                this.parchmentImage.addControl(this.revealButton);
                if ((!GameEngine.Instance.World.isBigpointAccount && !Program.bigpointInstall) && (!Program.aeriaInstall && !Program.bigpointPartnerInstall))
                {
                    CustomSelfDrawPanel.CSDButton button = new CustomSelfDrawPanel.CSDButton {
                        ImageNorm = (Image) GFXLibrary.banner_ad_friend,
                        OverBrighten = true,
                        Position = new Point(0xc9, (this.parchmentImage.Y + 400) - 10)
                    };
                    button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.friendClicked), "FreeCardsPanel_invite_friend");
                    this.mainBackgroundImage.addControl(button);
                }
            }
            this.fanImage.Image = (Image) GFXLibrary.free_card_screen_card_fan;
            this.fanImage.Position = new Point(650, 6);
            this.mainBackgroundImage.addControl(this.fanImage);
            base.Invalidate();
        }

        private void initialiseFreeCardsCallback(InitialiseFreeCards_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.UpdateVeteranLevelData();
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private bool isButtonEnabled(int row)
        {
            return (row == this.freeCardInfo.CurrentVeteranLevel);
        }

        private bool isButtonVisible(int row)
        {
            return (((row + 1) > this.freeCardInfo.CurrentVeteranLevel) && this.freeCardInfo.VeteranStages[row]);
        }

        public void revealCard()
        {
            _lastInstance = this;
            XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath).getFreeCard(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")), new CardsEndResponseDelegate(FreeCardsPanel.revealCardCallback), this);
            this.revealButton.Enabled = false;
        }

        public static void revealCardCallback(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode.Value == 1)
            {
                try
                {
                    int? nullable3;
                    int? nullable5;
                    int? nullable7;
                    int? nullable9;
                    int? nullable11;
                    int? nullable13;
                    int? nullable15;
                    int? nullable17;
                    int? nullable19;
                    int? nullable21;
                    if (_lastInstance != null)
                    {
                        try
                        {
                            _lastInstance.Invoke(new CardsEndResponseDelegate(_lastInstance.revealCardCallbackPanel), new object[] { provider, response });
                        }
                        catch (Exception)
                        {
                        }
                        _lastInstance = null;
                    }
                    GFXLibrary.Instance.closeBigCardsLoader();
                    bool[] flagArray2 = new bool[10];

                    nullable3 = null;
                    nullable5 = null;
                    nullable7 = null;
                    nullable9 = null;
                    nullable11 = null;
                    nullable13 = null;
                    nullable15 = null;
                    nullable17 = null;
                    nullable19 = null;
                    nullable21 = null;
                    if (((XmlRpcCardsResponse) response).VeteranLevel1.HasValue)
                    {
                        nullable3 = ((XmlRpcCardsResponse) response).VeteranLevel1;
                    }
                    flagArray2[0] = (nullable3.GetValueOrDefault() == 1) && nullable3.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel2.HasValue)
                    {
                        nullable5 = ((XmlRpcCardsResponse) response).VeteranLevel2;
                    }
                    flagArray2[1] = (nullable5.GetValueOrDefault() == 1) && nullable5.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel3.HasValue)
                    {
                        nullable7 = ((XmlRpcCardsResponse) response).VeteranLevel3;
                    }
                    flagArray2[2] = (nullable7.GetValueOrDefault() == 1) && nullable7.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel4.HasValue)
                    {
                        nullable9 = ((XmlRpcCardsResponse) response).VeteranLevel4;
                    }
                    flagArray2[3] = (nullable9.GetValueOrDefault() == 1) && nullable9.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel5.HasValue)
                    {
                        nullable11 = ((XmlRpcCardsResponse) response).VeteranLevel5;
                    }
                    flagArray2[4] = (nullable11.GetValueOrDefault() == 1) && nullable11.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel6.HasValue)
                    {
                        nullable13 = ((XmlRpcCardsResponse) response).VeteranLevel6;
                    }
                    flagArray2[5] = (nullable13.GetValueOrDefault() == 1) && nullable13.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel7.HasValue)
                    {
                        nullable15 = ((XmlRpcCardsResponse) response).VeteranLevel7;
                    }
                    flagArray2[6] = (nullable15.GetValueOrDefault() == 1) && nullable15.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel8.HasValue)
                    {
                        nullable17 = ((XmlRpcCardsResponse) response).VeteranLevel8;
                    }
                    flagArray2[7] = (nullable17.GetValueOrDefault() == 1) && nullable17.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel9.HasValue)
                    {
                        nullable19 = ((XmlRpcCardsResponse) response).VeteranLevel9;
                    }
                    flagArray2[8] = (nullable19.GetValueOrDefault() == 1) && nullable19.HasValue;
                    if (((XmlRpcCardsResponse) response).VeteranLevel10.HasValue)
                    {
                        nullable21 = ((XmlRpcCardsResponse) response).VeteranLevel10;
                    }
                    flagArray2[9] = (nullable21.GetValueOrDefault() == 1) && nullable21.HasValue;
                    bool[] stages = flagArray2;
                    GameEngine.Instance.World.importFreeCardData(((XmlRpcCardsResponse) response).VeteranCurrentLevel.Value, stages, DateTime.Now.AddSeconds((double) ((XmlRpcCardsResponse) response).VeteranSecondsLeft.Value), DateTime.Now);
                }
                catch (Exception exception)
                {
                    MyMessageBox.Show(exception.Message, SK.Text("GENERIC_Error", "Error"));
                }
            }
            else
            {
                MyMessageBox.Show(response.Message, SK.Text("FreeCardsPanel_Cannot_Free_Card", "Could not get free card."));
            }
        }

        private void revealCardCallbackPanel(ICardsProvider provider, ICardsResponse response)
        {
            try
            {
                foreach (string str in response.Strings.Split(";".ToCharArray()))
                {
                    string[] strArray2 = str.Split(",".ToCharArray());
                    if (strArray2.Length == 2)
                    {
                        GameEngine.Instance.World.ProfileCards.Add(Convert.ToInt32(strArray2[0].Trim()), CardTypes.getCardDefinitionFromString(strArray2[1].Trim()));
                        CustomSelfDrawPanel.UICard control = BuyCardsPanel.makeUICard(CardTypes.getCardDefinitionFromString(strArray2[1].Trim()), Convert.ToInt32(strArray2[0].Trim()), GameEngine.Instance.World.getRank() + 1);
                        control.Position = new Point(12, 11);
                        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardClicked), "FreeCardsPanel_play_card");
                        this.cardBackImage.addControl(control);
                        this.revealButton.Visible = false;
                        base.Invalidate();
                    }
                }
                this.revealButton.Enabled = true;
            }
            catch (Exception)
            {
            }
        }

        public void update()
        {
            string str = SK.Text("FreeCardsPanel_Next_Card", "Next Card") + ": ";
            TimeSpan span = this.freeCardInfo.timeUntilNextFreeCard();
            if (((span.TotalSeconds >= 1.0) && (span.TotalDays < 100.0)) && (this.freeCardInfo.CurrentVeteranLevel > 0))
            {
                str = str + VillageMap.createBuildTimeString((int) span.TotalSeconds);
            }
            this.labelNextCards.Text = str;
            this.timeProgress.setValues(this.freeCardInfo.durationHours() - span.TotalHours, this.freeCardInfo.durationHours());
        }

        public void UpdateVeteranLevelData()
        {
            XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath).getVeteranLevel(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")), new CardsEndResponseDelegate(this.getVeteranLevelCallback), this);
        }
    }
}

