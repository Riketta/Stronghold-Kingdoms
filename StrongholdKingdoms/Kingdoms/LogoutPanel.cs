namespace Kingdoms
{
    using CommonTypes;
    using StatTracking;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class LogoutPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDArea attackArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDCheckBox attackCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox attackCheck_AI = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox attackCheck_Bandits = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox attackCheck_Wolves = new CustomSelfDrawPanel.CSDCheckBox();
        private const int CHECK_HORZ_SPACING = 0x55;
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private static Image hrImage;
        private CustomSelfDrawPanel.CSDLabel labelCrowns = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private bool logoutPressed;
        private bool m_normalLogout = true;
        private SelectTradingResourcePopup m_resourcePopup;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private bool premium;
        private CustomSelfDrawPanel.CSDArea recruitArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDCheckBox recruitCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Archers = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Catapults = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Peasants = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Pikemen = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDCheckBox recruitCheck_Swordsmen = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDLabel recruitmentInfoLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel recruitNumber_Archers = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel recruitNumber_Catapults = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel recruitNumber_Peasants = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel recruitNumber_Pikemen = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel recruitNumber_Swordsmen = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Archers = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Catapults = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Peasants = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Pikemen = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDTrackBar recruitTrackBar_Swordsmen = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDArea repairArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDCheckBox repairCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDArea scoutingArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDCheckBox scoutingCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDArea tradingArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDCheckBox tradingCheck = new CustomSelfDrawPanel.CSDCheckBox();
        private CustomSelfDrawPanel.CSDButton tradingCircleButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel tradingPercentLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage tradingResourceImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDTrackBar tradingTrackBar = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDArea transferArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDCheckBox transferCheck = new CustomSelfDrawPanel.CSDCheckBox();

        public LogoutPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void attackToggled()
        {
            this.attackArea.Visible = this.attackCheck.Checked;
        }

        private void cardsClicked()
        {
            StatTrackingClient.Instance().ActivateTrigger(0x1a, true);
            this.closePopup();
            InterfaceMgr.Instance.closeLogoutWindow();
            InterfaceMgr.Instance.openPlayCardsWindow(0).SwitchPanel(4);
        }

        private void closeClick()
        {
            StatTrackingClient.Instance().ActivateTrigger(0x1a, false);
            if (this.m_normalLogout)
            {
                this.closePopup();
                InterfaceMgr.Instance.closeLogoutWindow();
                InterfaceMgr.Instance.ParentForm.TopMost = true;
                InterfaceMgr.Instance.ParentForm.TopMost = false;
            }
            else
            {
                this.doQuit();
            }
        }

        public bool closePopup()
        {
            bool flag = false;
            if (this.m_resourcePopup != null)
            {
                if (this.m_resourcePopup.Created)
                {
                    this.m_resourcePopup.Close();
                    flag = true;
                }
                this.m_resourcePopup = null;
            }
            return flag;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void doLogout()
        {
            if (!this.logoutPressed)
            {
                this.logoutPressed = true;
                Sound.stopVillageEnvironmental();
                if (InterfaceMgr.Instance.ParentForm != null)
                {
                    InterfaceMgr.Instance.ParentForm.Hide();
                }
                if (!this.premium)
                {
                    LoggingOutPopup.open(true, false, false, false, false, false, false, 6, 0, false, false, false, false, false, false, 500, 500, 500, 500, 250);
                }
                else
                {
                    LoggingOutPopup.open(true, this.scoutingCheck.Checked, this.tradingCheck.Checked, this.attackCheck.Checked, this.attackCheck_Wolves.Checked, this.attackCheck_Bandits.Checked, this.attackCheck_AI.Checked, this.tradingResourceImage.Data, this.tradingTrackBar.Value, this.recruitCheck.Checked, this.recruitCheck_Peasants.Checked, this.recruitCheck_Archers.Checked, this.recruitCheck_Pikemen.Checked, this.recruitCheck_Swordsmen.Checked, this.recruitCheck_Catapults.Checked, this.recruitTrackBar_Peasants.Value * 10, this.recruitTrackBar_Archers.Value * 10, this.recruitTrackBar_Pikemen.Value * 10, this.recruitTrackBar_Swordsmen.Value * 10, this.recruitTrackBar_Catapults.Value * 5);
                }
                this.closePopup();
                InterfaceMgr.Instance.closeLogoutWindow();
            }
        }

        private void doQuit()
        {
            Sound.stopVillageEnvironmental();
            if (!this.premium)
            {
                RemoteServices.Instance.LogOut(true, false, false, false, false, false, false, 6, 0, false, false, false, false, false, false, 500, 500, 500, 500, 250);
            }
            else
            {
                RemoteServices.Instance.LogOut(true, this.scoutingCheck.Checked, this.tradingCheck.Checked, this.attackCheck.Checked, this.attackCheck_Wolves.Checked, this.attackCheck_Bandits.Checked, this.attackCheck_AI.Checked, this.tradingResourceImage.Data, this.tradingTrackBar.Value, this.recruitCheck.Checked, this.recruitCheck_Peasants.Checked, this.recruitCheck_Archers.Checked, this.recruitCheck_Pikemen.Checked, this.recruitCheck_Swordsmen.Checked, this.recruitCheck_Catapults.Checked, this.recruitTrackBar_Peasants.Value * 10, this.recruitTrackBar_Archers.Value * 10, this.recruitTrackBar_Pikemen.Value * 10, this.recruitTrackBar_Swordsmen.Value * 10, this.recruitTrackBar_Catapults.Value * 5);
            }
            GameEngine.Instance.sessionExpired(-1);
            GameEngine.Instance.FlagQuitGame();
            this.closePopup();
            LogoutOptionsWindow2.closing = true;
            InterfaceMgr.Instance.closeLogoutWindow();
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

        public void init(bool normalLogout, bool advertOnly)
        {
            CustomSelfDrawPanel.CSDImage image2;
            CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate newDelegate = null;
            CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate delegate3 = null;
            CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate delegate4 = null;
            CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate delegate5 = null;
            CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate delegate6 = null;
            CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate delegate7 = null;
            this.m_normalLogout = normalLogout;
            base.clearControls();
            this.mainBackgroundImage.Image = GFXLibrary.dummy;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
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
            if (hrImage == null)
            {
                hrImage = new Bitmap(base.Width - 10, 1);
                using (Graphics graphics = Graphics.FromImage(hrImage))
                {
                    graphics.Clear(Color.FromArgb(0xff, 130, 0x81, 0x7e));
                }
            }
            CustomSelfDrawPanel.CSDImage image3 = new CustomSelfDrawPanel.CSDImage {
                Image = hrImage,
                Size = hrImage.Size,
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(image3);
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "LogoutPanel_close");
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.closeImage.CustomTooltipID = 0x578;
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x29, new Point((((base.Width - 14) - 0x11) - 50) + 3, 5), true);
            CustomSelfDrawPanel.CSDImage image4 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.logout_background_lhs,
                Position = new Point(4, 40)
            };
            control.addControl(image4);
            this.labelTitle.Position = new Point(0x1b, 8);
            this.labelTitle.Size = new Size(600, 0x40);
            if (advertOnly)
            {
                this.labelTitle.Text = SK.Text("LogoutPanel_Expiration", "Premium Token Expired");
            }
            else
            {
                this.labelTitle.Text = SK.Text("LogoutPanel_Logout", "Logout");
            }
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            NumberFormatInfo nFI = GameEngine.NFI;
            this.labelCrowns.Position = new Point(0, 8);
            this.labelCrowns.Size = new Size(900, 0x40);
            this.labelCrowns.Text = SK.Text("LogoutPanel_Crowns_In_Treasury", "Crowns in your treasury") + " : " + GameEngine.Instance.World.ProfileCrowns.ToString("N", nFI);
            this.labelCrowns.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.labelCrowns.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelCrowns.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelCrowns);
            CardData userCardData = GameEngine.Instance.World.UserCardData;
            if (userCardData.premiumCard == 0)
            {
                this.premium = false;
                CustomSelfDrawPanel.CSDButton button = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.logout_ad_1premfor30crown_01,
                    ImageOver = (Image) GFXLibrary.logout_ad_1premfor30crown_01_over,
                    Position = new Point(0x177, 50)
                };
                button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsClicked), "LogoutPanel_premium");
                this.mainBackgroundImage.addControl(button);
                int num = 0x23;
                int y = 0x35;
                CustomSelfDrawPanel.CSDExtendingPanel panel2 = new CustomSelfDrawPanel.CSDExtendingPanel {
                    Size = new Size(0x252, 0x164),
                    Position = new Point(image4.Position.X + 0x174, (image4.Position.Y + 0x4c) + 0x13),
                    Alpha = 0.1f
                };
                this.mainBackgroundImage.addControl(panel2);
                panel2.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
                CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Premium_1", "With premium you command"),
                    Position = new Point(0, 5),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width, 50),
                    Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
                };
                panel2.addControl(label);
                CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Premium_2", "even when you are offline!"),
                    Position = new Point(0, 0x21),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width, 50),
                    Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
                };
                panel2.addControl(label2);
                CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_BuildQueue_1", "Build-queue, build up to 5 buildings in your village at one time."),
                    Position = new Point(0x41, y),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width - 0x4b, 50),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                panel2.addControl(label3);
                CustomSelfDrawPanel.CSDLabel label4 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_BuildQueue_2", "Research queue, 5 more items can be added to your research queue."),
                    Position = new Point(0x41, y + num),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width - 0x4b, 50),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                panel2.addControl(label4);
                CustomSelfDrawPanel.CSDLabel label5 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Scouting_1", "Use Auto scouting to forage for goods."),
                    Position = new Point(0x41, y + (num * 2)),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width - 0x4b, 50),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                panel2.addControl(label5);
                CustomSelfDrawPanel.CSDLabel label6 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Scouting_2", "Auto Trade - lets you set and trade the surplus of one goods type."),
                    Position = new Point(0x41, y + (num * 3)),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width - 0x4b, 50),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                panel2.addControl(label6);
                CustomSelfDrawPanel.CSDLabel label7 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Attacks_1", "Specify targets and Auto Attack will dispatch your armies."),
                    Position = new Point(0x41, y + (num * 4)),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width - 0x4b, 50),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                panel2.addControl(label7);
                CustomSelfDrawPanel.CSDLabel label8 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Attacks_2", "Keep your army topped up with Auto Recruit."),
                    Position = new Point(0x41, y + (num * 5)),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width - 0x4b, 50),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                panel2.addControl(label8);
                CustomSelfDrawPanel.CSDLabel label9 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Overview", "Keep track of key stats across all your villages with the Village Overview."),
                    Position = new Point(0x41, y + (num * 6)),
                    Color = ARGBColors.Black,
                    Size = new Size(panel2.Width - 0x4b, 50),
                    Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                panel2.addControl(label9);
                CustomSelfDrawPanel.CSDImage image5 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.icon_building,
                    Position = new Point(0x12, y + 7)
                };
                panel2.addControl(image5);
                CustomSelfDrawPanel.CSDImage image6 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.icon_research,
                    Position = new Point(0x12, (y + 7) + num)
                };
                panel2.addControl(image6);
                CustomSelfDrawPanel.CSDImage image7 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[2],
                    Position = new Point(15, (y + 5) + (num * 2))
                };
                panel2.addControl(image7);
                CustomSelfDrawPanel.CSDImage image8 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[1],
                    Position = new Point(15, (y + 5) + (num * 3))
                };
                panel2.addControl(image8);
                CustomSelfDrawPanel.CSDImage image9 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[0],
                    Position = new Point(15, (y + 5) + (num * 4))
                };
                panel2.addControl(image9);
                CustomSelfDrawPanel.CSDImage image10 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[0],
                    Position = new Point(15, (y + 5) + (num * 5))
                };
                panel2.addControl(image10);
                CustomSelfDrawPanel.CSDImage image11 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[4],
                    Position = new Point(15, (y + 5) + (num * 6))
                };
                panel2.addControl(image11);
            }
            else
            {
                this.premium = true;
                CustomSelfDrawPanel.CSDExtendingPanel panel3 = new CustomSelfDrawPanel.CSDExtendingPanel {
                    Size = new Size(0x252, 0x1b0),
                    Alpha = 0.1f,
                    Position = new Point(image4.Position.X + 0x174, image4.Position.Y + 20)
                };
                this.mainBackgroundImage.addControl(panel3);
                panel3.Create((Image) GFXLibrary.cardpanel_panel_black_top_left, (Image) GFXLibrary.cardpanel_panel_black_top_mid, (Image) GFXLibrary.cardpanel_panel_black_top_right, (Image) GFXLibrary.cardpanel_panel_black_mid_left, (Image) GFXLibrary.cardpanel_panel_black_mid_mid, (Image) GFXLibrary.cardpanel_panel_black_mid_right, (Image) GFXLibrary.cardpanel_panel_black_bottom_left, (Image) GFXLibrary.cardpanel_panel_black_bottom_mid, (Image) GFXLibrary.cardpanel_panel_black_bottom_right);
                CustomSelfDrawPanel.CSDImage image12 = new CustomSelfDrawPanel.CSDImage();
                if (userCardData.premiumCard == 0x1012)
                {
                    image12.Image = (Image) GFXLibrary.logout_premium_token_30;
                }
                else if (userCardData.premiumCard == 0x1011)
                {
                    image12.Image = (Image) GFXLibrary.logout_premium_token_2;
                }
                else if (userCardData.premiumCard == 0x1014)
                {
                    image12.Image = (Image) GFXLibrary.logout_premium_token_extendable;
                }
                else
                {
                    image12.Image = (Image) GFXLibrary.logout_premium_token;
                }
                image12.Position = new Point(-8, -8);
                image12.CustomTooltipID = 0x58d;
                image4.addControl(image12);
                CustomSelfDrawPanel.CSDImage image13 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_gradation_band,
                    Position = new Point(0x26, 30)
                };
                panel3.addControl(image13);
                CustomSelfDrawPanel.CSDImage image14 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[1],
                    Position = new Point(-4, -4)
                };
                if (newDelegate == null)
                {
                    newDelegate = delegate {
                        this.tradingCheck.Checked = !this.tradingCheck.Checked;
                        this.tradingToggled();
                    };
                }
                image14.setClickDelegate(newDelegate, "Generic_check_box_toggled");
                image14.CustomTooltipID = 0x579;
                image13.addControl(image14);
                this.tradingCheck.Position = new Point(-30, 2);
                this.tradingCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                this.tradingCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                this.tradingCheck.Checked = RemoteServices.Instance.UserOptions.autoTrade;
                this.tradingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.tradingToggled));
                this.tradingCheck.CustomTooltipID = 0x579;
                image13.addControl(this.tradingCheck);
                CustomSelfDrawPanel.CSDLabel label10 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Auto_Trading", "Auto Trading"),
                    Position = new Point(40, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(140, image13.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                image13.addControl(label10);
                this.tradingArea.Position = new Point(0x87, -20);
                this.tradingArea.Size = new Size(0x1ac, image13.Height + 0x29);
                this.tradingArea.Visible = this.tradingCheck.Checked;
                image13.addControl(this.tradingArea);
                int autoTradeResource = RemoteServices.Instance.UserOptions.autoTradeResource;
                if (autoTradeResource == -1)
                {
                    autoTradeResource = 6;
                }
                this.tradingCircleButton.ImageNorm = (Image) GFXLibrary.logout_bits[7];
                this.tradingCircleButton.ImageOver = (Image) GFXLibrary.logout_bits[8];
                this.tradingCircleButton.Position = new Point(0, 1);
                this.tradingCircleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradingResourceClicked), "LogoutPanel_resources");
                this.tradingCircleButton.CustomTooltipID = 0x57f;
                this.tradingCircleButton.CustomTooltipData = autoTradeResource;
                this.tradingArea.addControl(this.tradingCircleButton);
                this.tradingResourceImage.Image = (Image) GFXLibrary.getCommodity64DSImage(autoTradeResource);
                this.tradingResourceImage.Size = new Size(0x45, 0x45);
                this.tradingResourceImage.Data = autoTradeResource;
                this.tradingResourceImage.Position = new Point(0, 0);
                this.tradingCircleButton.addControl(this.tradingResourceImage);
                this.tradingTrackBar.Position = new Point(0xd7, 0x19);
                this.tradingTrackBar.Margin = new Rectangle(0x49, -4, 0, 0);
                this.tradingTrackBar.Max = 100;
                this.tradingTrackBar.Value = RemoteServices.Instance.UserOptions.autoTradePercent;
                this.tradingTrackBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
                this.tradingArea.addControl(this.tradingTrackBar);
                this.tradingTrackBar.CustomTooltipID = 0x580;
                this.tradingTrackBar.Create((Image) GFXLibrary.logout_slider_back, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
                this.tradingPercentLabel.Text = "0%";
                this.tradingPercentLabel.Position = new Point(0, 0);
                this.tradingPercentLabel.Color = ARGBColors.Black;
                this.tradingPercentLabel.Size = new Size(0x3a, 0x17);
                this.tradingPercentLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.tradingPercentLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.tradingTrackBar.addControl(this.tradingPercentLabel);
                CustomSelfDrawPanel.CSDLabel label11 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Trade_Over", "Trade Over"),
                    Position = new Point(0, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(210, this.tradingArea.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT
                };
                this.tradingArea.addControl(label11);
                this.tracksMoved();
                CustomSelfDrawPanel.CSDImage image15 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_gradation_band,
                    Position = new Point(0x26, 100)
                };
                panel3.addControl(image15);
                CustomSelfDrawPanel.CSDImage image16 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[2],
                    Position = new Point(-4, -4)
                };
                if (delegate3 == null)
                {
                    delegate3 = delegate {
                        this.scoutingCheck.Checked = !this.scoutingCheck.Checked;
                        this.scoutingToggled();
                    };
                }
                image16.setClickDelegate(delegate3, "Generic_check_box_toggled");
                image16.CustomTooltipID = 0x57a;
                image15.addControl(image16);
                this.scoutingCheck.Position = new Point(-30, 2);
                this.scoutingCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                this.scoutingCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                this.scoutingCheck.Checked = RemoteServices.Instance.UserOptions.autoScout;
                this.scoutingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.scoutingToggled));
                this.scoutingCheck.CustomTooltipID = 0x57a;
                image15.addControl(this.scoutingCheck);
                CustomSelfDrawPanel.CSDLabel label12 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Auto_Scouting", "Auto Scouting"),
                    Position = new Point(40, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(140, image15.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                image15.addControl(label12);
                this.scoutingArea.Position = new Point(0x87, -20);
                this.scoutingArea.Size = new Size(0x1ac, image15.Height + 0x29);
                this.scoutingArea.Visible = this.scoutingCheck.Checked;
                image15.addControl(this.scoutingArea);
                CustomSelfDrawPanel.CSDLabel label13 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Auto_Scouting2", "Scout within your Parishes"),
                    Position = new Point(0, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(0x18e, this.scoutingArea.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT
                };
                this.scoutingArea.addControl(label13);
                CustomSelfDrawPanel.CSDImage image17 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_bits[14],
                    Position = new Point(0, 1)
                };
                this.scoutingArea.addControl(image17);
                CustomSelfDrawPanel.CSDImage image18 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_gradation_band,
                    Position = new Point(0x26, 170)
                };
                panel3.addControl(image18);
                CustomSelfDrawPanel.CSDImage image19 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[0x18],
                    Position = new Point(-4, -4)
                };
                if (delegate4 == null)
                {
                    delegate4 = delegate {
                        this.attackCheck.Checked = !this.attackCheck.Checked;
                        this.attackToggled();
                    };
                }
                image19.setClickDelegate(delegate4, "Generic_check_box_toggled");
                image19.CustomTooltipID = 0x57b;
                image18.addControl(image19);
                this.attackCheck.Position = new Point(-30, 2);
                this.attackCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                this.attackCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                this.attackCheck.Checked = RemoteServices.Instance.UserOptions.autoAttack;
                this.attackCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.attackToggled));
                this.attackCheck.CustomTooltipID = 0x57b;
                image18.addControl(this.attackCheck);
                CustomSelfDrawPanel.CSDLabel label14 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Auto_Attack", "Auto Attack"),
                    Position = new Point(40, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(140, image18.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                image18.addControl(label14);
                this.attackArea.Position = new Point(0x87, -20);
                this.attackArea.Size = new Size(0x1ac, image18.Height + 40);
                this.attackArea.Visible = this.attackCheck.Checked;
                image18.addControl(this.attackArea);
                this.attackCheck_Bandits.Position = new Point(0, 1);
                this.attackCheck_Bandits.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.attackCheck_Bandits.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.attackCheck_Bandits.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.attackCheck_Bandits.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.attackCheck_Bandits.Checked = RemoteServices.Instance.UserOptions.autoAttackBandit;
                this.attackCheck_Bandits.CustomTooltipID = 0x581;
                this.attackArea.addControl(this.attackCheck_Bandits);
                CustomSelfDrawPanel.CSDImage image20 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.scout_screen_icons[0x18],
                    Position = new Point(-20, -11)
                };
                this.attackCheck_Bandits.addControl(image20);
                this.attackCheck_Wolves.Position = new Point(0x55, 1);
                this.attackCheck_Wolves.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.attackCheck_Wolves.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.attackCheck_Wolves.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.attackCheck_Wolves.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.attackCheck_Wolves.Checked = RemoteServices.Instance.UserOptions.autoAttackWolf;
                this.attackCheck_Wolves.CustomTooltipID = 0x582;
                this.attackArea.addControl(this.attackCheck_Wolves);
                CustomSelfDrawPanel.CSDImage image21 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.scout_screen_icons[0x19],
                    Position = new Point(-8, -14)
                };
                this.attackCheck_Wolves.addControl(image21);
                this.attackCheck_AI.Position = new Point(170, 1);
                this.attackCheck_AI.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.attackCheck_AI.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.attackCheck_AI.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.attackCheck_AI.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.attackCheck_AI.Checked = RemoteServices.Instance.UserOptions.autoAttackAI;
                this.attackCheck_AI.CustomTooltipID = 0x583;
                this.attackArea.addControl(this.attackCheck_AI);
                CustomSelfDrawPanel.CSDImage image22 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.scout_screen_icons[0x1c],
                    Position = new Point(-17, -11)
                };
                this.attackCheck_AI.addControl(image22);
                CustomSelfDrawPanel.CSDImage image23 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_gradation_band,
                    Position = new Point(0x26, 240)
                };
                panel3.addControl(image23);
                CustomSelfDrawPanel.CSDImage image24 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[0],
                    Position = new Point(-4, -4)
                };
                if (delegate5 == null)
                {
                    delegate5 = delegate {
                        this.recruitCheck.Checked = !this.recruitCheck.Checked;
                        this.recruitToggled();
                    };
                }
                image24.setClickDelegate(delegate5, "Generic_check_box_toggled");
                image24.CustomTooltipID = 0x57c;
                image23.addControl(image24);
                this.recruitCheck.Position = new Point(-30, 2);
                this.recruitCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                this.recruitCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                this.recruitCheck.Checked = RemoteServices.Instance.UserOptions.autoRecruit;
                this.recruitCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggled));
                this.recruitCheck.CustomTooltipID = 0x57c;
                image23.addControl(this.recruitCheck);
                CustomSelfDrawPanel.CSDLabel label15 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Auto_Recruit", "Auto Recruit"),
                    Position = new Point(40, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(140, image23.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                image23.addControl(label15);
                this.recruitArea.Position = new Point(0x87, -20);
                this.recruitArea.Size = new Size(0x1ac, ((image23.Height + 40) + 40) + 30);
                this.recruitArea.Visible = this.recruitCheck.Checked;
                image23.addControl(this.recruitArea);
                this.recruitCheck_Peasants.Position = new Point(0, 1);
                this.recruitCheck_Peasants.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.recruitCheck_Peasants.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.recruitCheck_Peasants.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.recruitCheck_Peasants.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.recruitCheck_Peasants.Checked = RemoteServices.Instance.UserOptions.autoRecruitPeasants;
                this.recruitCheck_Peasants.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
                this.recruitCheck_Peasants.CustomTooltipID = 0x584;
                this.recruitArea.addControl(this.recruitCheck_Peasants);
                this.recruitTrackBar_Peasants.Position = new Point(this.recruitCheck_Peasants.Position.X + 3, 0x4b);
                this.recruitTrackBar_Peasants.Margin = new Rectangle(0, -4, 0, 0);
                this.recruitTrackBar_Peasants.Max = 50;
                this.recruitTrackBar_Peasants.Value = RemoteServices.Instance.UserOptions.autoRecruitPeasants_Caps / 10;
                this.recruitTrackBar_Peasants.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
                this.recruitArea.addControl(this.recruitTrackBar_Peasants);
                this.recruitTrackBar_Peasants.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
                this.recruitNumber_Peasants.Text = "0";
                this.recruitNumber_Peasants.Position = new Point(this.recruitCheck_Peasants.Position.X, 0x69);
                this.recruitNumber_Peasants.Color = ARGBColors.Black;
                this.recruitNumber_Peasants.Size = new Size(this.recruitCheck_Peasants.Width, 20);
                this.recruitNumber_Peasants.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.recruitNumber_Peasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.recruitArea.addControl(this.recruitNumber_Peasants);
                CustomSelfDrawPanel.CSDImage image25 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_bits[9],
                    Position = new Point(0, 0)
                };
                this.recruitCheck_Peasants.addControl(image25);
                this.recruitCheck_Archers.Position = new Point(0x55, 1);
                this.recruitCheck_Archers.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.recruitCheck_Archers.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.recruitCheck_Archers.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.recruitCheck_Archers.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.recruitCheck_Archers.Checked = RemoteServices.Instance.UserOptions.autoRecruitArchers;
                this.recruitCheck_Archers.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
                this.recruitCheck_Archers.CustomTooltipID = 0x585;
                this.recruitArea.addControl(this.recruitCheck_Archers);
                this.recruitTrackBar_Archers.Position = new Point(this.recruitCheck_Archers.Position.X + 3, 0x4b);
                this.recruitTrackBar_Archers.Margin = new Rectangle(0, -4, 0, 0);
                this.recruitTrackBar_Archers.Max = 50;
                this.recruitTrackBar_Archers.Value = RemoteServices.Instance.UserOptions.autoRecruitArchers_Caps / 10;
                this.recruitTrackBar_Archers.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
                this.recruitArea.addControl(this.recruitTrackBar_Archers);
                this.recruitTrackBar_Archers.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
                this.recruitNumber_Archers.Text = "0";
                this.recruitNumber_Archers.Position = new Point(this.recruitCheck_Archers.Position.X, 0x69);
                this.recruitNumber_Archers.Color = ARGBColors.Black;
                this.recruitNumber_Archers.Size = new Size(this.recruitCheck_Archers.Width, 20);
                this.recruitNumber_Archers.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.recruitNumber_Archers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.recruitArea.addControl(this.recruitNumber_Archers);
                CustomSelfDrawPanel.CSDImage image26 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_bits[10],
                    Position = new Point(0, 0)
                };
                this.recruitCheck_Archers.addControl(image26);
                this.recruitCheck_Pikemen.Position = new Point(170, 1);
                this.recruitCheck_Pikemen.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.recruitCheck_Pikemen.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.recruitCheck_Pikemen.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.recruitCheck_Pikemen.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.recruitCheck_Pikemen.Checked = RemoteServices.Instance.UserOptions.autoRecruitPikemen;
                this.recruitCheck_Pikemen.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
                this.recruitCheck_Pikemen.CustomTooltipID = 0x586;
                this.recruitArea.addControl(this.recruitCheck_Pikemen);
                this.recruitTrackBar_Pikemen.Position = new Point(this.recruitCheck_Pikemen.Position.X + 3, 0x4b);
                this.recruitTrackBar_Pikemen.Margin = new Rectangle(0, -4, 0, 0);
                this.recruitTrackBar_Pikemen.Max = 50;
                this.recruitTrackBar_Pikemen.Value = RemoteServices.Instance.UserOptions.autoRecruitPikemen_Caps / 10;
                this.recruitTrackBar_Pikemen.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
                this.recruitArea.addControl(this.recruitTrackBar_Pikemen);
                this.recruitTrackBar_Pikemen.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
                this.recruitNumber_Pikemen.Text = "0";
                this.recruitNumber_Pikemen.Position = new Point(this.recruitCheck_Pikemen.Position.X, 0x69);
                this.recruitNumber_Pikemen.Color = ARGBColors.Black;
                this.recruitNumber_Pikemen.Size = new Size(this.recruitCheck_Pikemen.Width, 20);
                this.recruitNumber_Pikemen.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.recruitNumber_Pikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.recruitArea.addControl(this.recruitNumber_Pikemen);
                CustomSelfDrawPanel.CSDImage image27 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_bits[11],
                    Position = new Point(0, 0)
                };
                this.recruitCheck_Pikemen.addControl(image27);
                this.recruitCheck_Swordsmen.Position = new Point(0xff, 1);
                this.recruitCheck_Swordsmen.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.recruitCheck_Swordsmen.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.recruitCheck_Swordsmen.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.recruitCheck_Swordsmen.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.recruitCheck_Swordsmen.Checked = RemoteServices.Instance.UserOptions.autoRecruitSwordsmen;
                this.recruitCheck_Swordsmen.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
                this.recruitCheck_Swordsmen.CustomTooltipID = 0x587;
                this.recruitArea.addControl(this.recruitCheck_Swordsmen);
                this.recruitTrackBar_Swordsmen.Position = new Point(this.recruitCheck_Swordsmen.Position.X + 3, 0x4b);
                this.recruitTrackBar_Swordsmen.Margin = new Rectangle(0, -4, 0, 0);
                this.recruitTrackBar_Swordsmen.Max = 50;
                this.recruitTrackBar_Swordsmen.Value = RemoteServices.Instance.UserOptions.autoRecruitSwordsmen_Caps / 10;
                this.recruitTrackBar_Swordsmen.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
                this.recruitArea.addControl(this.recruitTrackBar_Swordsmen);
                this.recruitTrackBar_Swordsmen.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
                this.recruitNumber_Swordsmen.Text = "0";
                this.recruitNumber_Swordsmen.Position = new Point(this.recruitCheck_Swordsmen.Position.X, 0x69);
                this.recruitNumber_Swordsmen.Color = ARGBColors.Black;
                this.recruitNumber_Swordsmen.Size = new Size(this.recruitCheck_Swordsmen.Width, 20);
                this.recruitNumber_Swordsmen.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.recruitNumber_Swordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.recruitArea.addControl(this.recruitNumber_Swordsmen);
                CustomSelfDrawPanel.CSDImage image28 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_bits[12],
                    Position = new Point(0, 0)
                };
                this.recruitCheck_Swordsmen.addControl(image28);
                this.recruitCheck_Catapults.Position = new Point(340, 1);
                this.recruitCheck_Catapults.CheckedImage = (Image) GFXLibrary.logout_bits[2];
                this.recruitCheck_Catapults.UncheckedImage = (Image) GFXLibrary.logout_bits[0];
                this.recruitCheck_Catapults.CheckedOverImage = (Image) GFXLibrary.logout_bits[3];
                this.recruitCheck_Catapults.UncheckedOverImage = (Image) GFXLibrary.logout_bits[1];
                this.recruitCheck_Catapults.Checked = RemoteServices.Instance.UserOptions.autoRecruitCatapults;
                this.recruitCheck_Catapults.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.recruitToggledUnit));
                this.recruitCheck_Catapults.CustomTooltipID = 0x588;
                this.recruitArea.addControl(this.recruitCheck_Catapults);
                this.recruitTrackBar_Catapults.Position = new Point(this.recruitCheck_Catapults.Position.X + 3, 0x4b);
                this.recruitTrackBar_Catapults.Margin = new Rectangle(0, -4, 0, 0);
                this.recruitTrackBar_Catapults.Max = 50;
                this.recruitTrackBar_Catapults.Value = RemoteServices.Instance.UserOptions.autoRecruitCatapults_Caps / 5;
                this.recruitTrackBar_Catapults.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.recruitTracksMoved));
                this.recruitArea.addControl(this.recruitTrackBar_Catapults);
                this.recruitTrackBar_Catapults.Create((Image) GFXLibrary.logout_slider_back2, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb, (Image) GFXLibrary.logout_slider_thumb);
                this.recruitNumber_Catapults.Text = "0";
                this.recruitNumber_Catapults.Position = new Point(this.recruitCheck_Catapults.Position.X, 0x69);
                this.recruitNumber_Catapults.Color = ARGBColors.Black;
                this.recruitNumber_Catapults.Size = new Size(this.recruitCheck_Catapults.Width, 20);
                this.recruitNumber_Catapults.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.recruitNumber_Catapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.recruitArea.addControl(this.recruitNumber_Catapults);
                CustomSelfDrawPanel.CSDImage image29 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_bits[13],
                    Position = new Point(0, 0)
                };
                this.recruitCheck_Catapults.addControl(image29);
                this.recruitmentInfoLabel.Text = SK.Text("Logout_Recruitment_Cap", "Set Recruitment Cap");
                this.recruitmentInfoLabel.Position = new Point(-7, 0x7d);
                this.recruitmentInfoLabel.Color = ARGBColors.Black;
                this.recruitmentInfoLabel.Size = new Size(this.recruitArea.Width, 0x17);
                this.recruitmentInfoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.recruitmentInfoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.recruitArea.addControl(this.recruitmentInfoLabel);
                this.recruitTracksMoved();
                this.updateRecruitVisibility();
                CustomSelfDrawPanel.CSDImage image30 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_gradation_band,
                    Position = new Point(0x26, 310),
                    Visible = false
                };
                panel3.addControl(image30);
                CustomSelfDrawPanel.CSDImage image31 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[1],
                    Position = new Point(-4, -4)
                };
                if (delegate6 == null)
                {
                    delegate6 = delegate {
                        this.transferCheck.Checked = !this.transferCheck.Checked;
                        this.transferToggled();
                    };
                }
                image31.setClickDelegate(delegate6, "Generic_check_box_toggled");
                image31.CustomTooltipID = 0x57e;
                image30.addControl(image31);
                this.transferCheck.Position = new Point(-30, 2);
                this.transferCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                this.transferCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                this.transferCheck.Checked = false;
                this.transferCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.transferToggled));
                this.transferCheck.CustomTooltipID = 0x57e;
                image30.addControl(this.transferCheck);
                CustomSelfDrawPanel.CSDLabel label16 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Auto_transfer", "Auto Transfer"),
                    Position = new Point(40, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(140, image30.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                image30.addControl(label16);
                this.transferArea.Position = new Point(0x87, -20);
                this.transferArea.Size = new Size(0x1ac, image30.Height + 40);
                this.transferArea.Visible = this.transferCheck.Checked;
                image30.addControl(this.transferArea);
                CustomSelfDrawPanel.CSDImage image32 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_gradation_band,
                    Position = new Point(0x26, 380),
                    Visible = false
                };
                panel3.addControl(image32);
                CustomSelfDrawPanel.CSDImage image33 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.wl_moving_unit_icons[0x19],
                    Position = new Point(-4, -4)
                };
                if (delegate7 == null)
                {
                    delegate7 = delegate {
                        this.repairCheck.Checked = !this.repairCheck.Checked;
                        this.repairToggled();
                    };
                }
                image33.setClickDelegate(delegate7, "Generic_check_box_toggled");
                image33.CustomTooltipID = 0x57d;
                image32.addControl(image33);
                this.repairCheck.Position = new Point(-30, 2);
                this.repairCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                this.repairCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                this.repairCheck.Checked = false;
                this.repairCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.repairToggled));
                this.repairCheck.CustomTooltipID = 0x57d;
                image32.addControl(this.repairCheck);
                CustomSelfDrawPanel.CSDLabel label17 = new CustomSelfDrawPanel.CSDLabel {
                    Text = SK.Text("LogoutPanel_Auto_Rebuild", "Auto Rebuild"),
                    Position = new Point(40, 0),
                    Color = ARGBColors.Black,
                    Size = new Size(140, image32.Height),
                    Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                };
                image32.addControl(label17);
                this.repairArea.Position = new Point(0x87, -20);
                this.repairArea.Size = new Size(0x1ac, image32.Height + 40);
                this.repairArea.Visible = this.repairCheck.Checked;
                image32.addControl(this.repairArea);
                CustomSelfDrawPanel.CSDImage image34 = new CustomSelfDrawPanel.CSDImage {
                    Image = (Image) GFXLibrary.logout_bits[4],
                    Position = new Point(0, 0)
                };
                this.repairArea.addControl(image34);
            }
            if ((!GameEngine.Instance.World.isBigpointAccount && !Program.bigpointInstall) && (!Program.aeriaInstall && !Program.bigpointPartnerInstall))
            {
                CustomSelfDrawPanel.CSDButton button2 = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.banner_ad_friend,
                    OverBrighten = true,
                    Position = new Point(0x177, 0x1f0)
                };
                button2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.friendClicked), "LogoutPanel_invite_a_friend");
                this.mainBackgroundImage.addControl(button2);
            }
            if (normalLogout && !advertOnly)
            {
                CustomSelfDrawPanel.CSDButton button3 = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                    ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                    ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                    Position = new Point(0x1a7, 0x1c5)
                };
                button3.Text.Text = SK.Text("LogoutPanel_Swap_Worlds", "Swap Worlds");
                button3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                button3.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                button3.TextYOffset = -2;
                button3.Text.Color = ARGBColors.Black;
                button3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.doLogout), "LogoutPanel_swap_worlds");
                button3.CustomTooltipID = 0x58c;
                control.addControl(button3);
                this.logoutPressed = false;
            }
            if (!advertOnly)
            {
                CustomSelfDrawPanel.CSDButton button4 = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                    ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                    ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                    Position = new Point(0x313, 0x1c5)
                };
                button4.Text.Text = SK.Text("GENERIC_Exit", "Exit");
                button4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                button4.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                button4.TextYOffset = -2;
                button4.Text.Color = ARGBColors.Black;
                button4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.doQuit), "LogoutPanel_exit");
                button4.CustomTooltipID = 0x58a;
                control.addControl(button4);
            }
            if (normalLogout && !advertOnly)
            {
                CustomSelfDrawPanel.CSDButton button5 = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                    ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                    ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                    Position = new Point(0x25d, 0x1c5)
                };
                button5.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                button5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                button5.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                button5.TextYOffset = -2;
                button5.Text.Color = ARGBColors.Black;
                button5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "LogoutPanel_cancel");
                button5.CustomTooltipID = 0x58b;
                control.addControl(button5);
            }
            else if (advertOnly)
            {
                CustomSelfDrawPanel.CSDButton button6 = new CustomSelfDrawPanel.CSDButton {
                    ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                    ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                    ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                    Position = new Point(0x25d, 0x1c5)
                };
                button6.Text.Text = SK.Text("GENERIC_OK", "OK");
                button6.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                button6.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                button6.TextYOffset = -2;
                button6.Text.Color = ARGBColors.Black;
                button6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "LogoutPanel_cancel");
                control.addControl(button6);
            }
            this.update();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void recruitToggled()
        {
            this.recruitArea.Visible = this.recruitCheck.Checked;
            this.mainBackgroundImage.invalidate();
            this.updateRecruitVisibility();
        }

        public void recruitToggledUnit()
        {
            this.updateRecruitVisibility();
            this.mainBackgroundImage.invalidate();
        }

        public void recruitTracksMoved()
        {
            this.recruitNumber_Peasants.Text = (this.recruitTrackBar_Peasants.Value * 10).ToString();
            this.recruitNumber_Archers.Text = (this.recruitTrackBar_Archers.Value * 10).ToString();
            this.recruitNumber_Pikemen.Text = (this.recruitTrackBar_Pikemen.Value * 10).ToString();
            this.recruitNumber_Swordsmen.Text = (this.recruitTrackBar_Swordsmen.Value * 10).ToString();
            this.recruitNumber_Catapults.Text = (this.recruitTrackBar_Catapults.Value * 5).ToString();
        }

        private void repairToggled()
        {
            this.repairArea.Visible = this.repairCheck.Checked;
        }

        public void resourceSelected(int resource)
        {
            this.tradingResourceImage.Image = (Image) GFXLibrary.getCommodity64DSImage(resource);
            this.tradingResourceImage.Size = new Size(0x45, 0x45);
            this.tradingResourceImage.Data = resource;
            this.tradingCircleButton.CustomTooltipData = resource;
            this.closePopup();
        }

        private void scoutingToggled()
        {
            this.scoutingArea.Visible = this.scoutingCheck.Checked;
        }

        public void tracksMoved()
        {
            this.tradingPercentLabel.Text = this.tradingTrackBar.Value.ToString() + "%";
        }

        private void tradingResourceClicked()
        {
            if (!this.closePopup())
            {
                this.m_resourcePopup = new SelectTradingResourcePopup();
                Point p = this.tradingCircleButton.getPanelPosition();
                p = new Point((p.X + (this.tradingCircleButton.Width / 2)) - 300, (p.Y + this.tradingCircleButton.Height) + 20);
                p = base.Parent.PointToScreen(p);
                this.m_resourcePopup.init(this.tradingResourceImage.Data, p, this, (LogoutOptionsWindow2) base.Parent);
            }
        }

        private void tradingToggled()
        {
            this.tradingArea.Visible = this.tradingCheck.Checked;
        }

        private void transferToggled()
        {
            this.transferArea.Visible = this.transferCheck.Checked;
        }

        public void update()
        {
        }

        public void updateRecruitVisibility()
        {
            this.recruitTrackBar_Peasants.Visible = this.recruitNumber_Peasants.Visible = this.recruitCheck_Peasants.Checked;
            this.recruitTrackBar_Archers.Visible = this.recruitNumber_Archers.Visible = this.recruitCheck_Archers.Checked;
            this.recruitTrackBar_Pikemen.Visible = this.recruitNumber_Pikemen.Visible = this.recruitCheck_Pikemen.Checked;
            this.recruitTrackBar_Swordsmen.Visible = this.recruitNumber_Swordsmen.Visible = this.recruitCheck_Swordsmen.Checked;
            this.recruitTrackBar_Catapults.Visible = this.recruitNumber_Catapults.Visible = this.recruitCheck_Catapults.Checked;
        }

        public void vacationModeCloseCheck()
        {
            if (!this.m_normalLogout)
            {
                this.doQuit();
            }
        }

        private class ResourceItem
        {
            public int resourceBuildingID;
            public string resourceName;

            public ResourceItem(string name, int id)
            {
                this.resourceName = name;
                this.resourceBuildingID = id;
            }

            public override string ToString()
            {
                return this.resourceName;
            }
        }
    }
}

