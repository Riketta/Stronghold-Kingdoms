namespace Kingdoms
{
    using CommonTypes;
    using StatTracking;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class UserInfoScreen2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton achievementsButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel achievementsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton adminButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage avatarImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundCentre = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundLeft = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundRight = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton diplomacyAllyButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDExtendingPanel diplomacyBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDButton diplomacyButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton diplomacyCancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel diplomacyCurrentLabelHeader = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton diplomacyEnemyButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel diplomacyFactionLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel diplomacyHeaderImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel diplomacyHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton diplomacyNeutralButton = new CustomSelfDrawPanel.CSDButton();
        private bool diplomacyOverlayVisible;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton editAvatarButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton editButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDImage flagImageShadow = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDFill greyOverlay = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel headerLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage houseImageShadow = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton inviteButton = new CustomSelfDrawPanel.CSDButton();
        private static Image lastCreatedAvatar = null;
        private List<VillageLine> lineList = new List<VillageLine>();
        private int m_houseID;
        private int m_userID = -1;
        private WorldMap.CachedUserInfo m_userInfo;
        private CustomSelfDrawPanel.CSDButton mailButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDExtendingPanel mainBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDArea mainBodyArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea mainHeaderArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage positionImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton questsButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel questsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel regionLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel standingLabelLabel = new CustomSelfDrawPanel.CSDLabel();
        public static VillageComparer villageComparer = new VillageComparer();
        private CustomSelfDrawPanel.CSDLabel villageLabel = new CustomSelfDrawPanel.CSDLabel();

        public UserInfoScreen2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void achievementsClicked()
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_achievements");
            InterfaceMgr.Instance.openAchievements(this.m_userInfo.achievements);
        }

        public void addDiplomacyOverlay()
        {
            if (this.m_userInfo != null)
            {
                this.removeOverlay();
                this.diplomacyOverlayVisible = true;
                this.greyOverlay.Position = new Point(0, -this.mainHeaderArea.Height);
                this.greyOverlay.Size = base.Size;
                this.greyOverlay.FillColor = Color.FromArgb(0x80, 0, 0, 0);
                this.greyOverlay.setClickDelegate(delegate {
                });
                this.mainBackgroundImage.addControl(this.greyOverlay);
                this.diplomacyHeaderImage.Size = new Size(400, 40);
                this.diplomacyHeaderImage.Position = new Point((base.Width - 400) / 2, 100);
                this.greyOverlay.addControl(this.diplomacyHeaderImage);
                this.diplomacyHeaderImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
                this.diplomacyBackgroundImage.Size = new Size(400, 300);
                this.diplomacyBackgroundImage.Position = new Point((base.Width - 400) / 2, 140);
                this.greyOverlay.addControl(this.diplomacyBackgroundImage);
                this.diplomacyBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
                this.diplomacyHeadingLabel.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
                this.diplomacyHeadingLabel.Color = ARGBColors.White;
                this.diplomacyHeadingLabel.Position = new Point(0, 0);
                this.diplomacyHeadingLabel.Size = this.diplomacyHeaderImage.Size;
                this.diplomacyHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                this.diplomacyHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyHeaderImage.addControl(this.diplomacyHeadingLabel);
                this.diplomacyFactionLabel.Text = this.m_userInfo.userName;
                this.diplomacyFactionLabel.Color = ARGBColors.Black;
                this.diplomacyFactionLabel.Position = new Point(0, 8);
                this.diplomacyFactionLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
                this.diplomacyFactionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.diplomacyFactionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.diplomacyBackgroundImage.addControl(this.diplomacyFactionLabel);
                this.diplomacyCurrentLabelHeader.Text = SK.Text("GENERIC_Current_Relationship", "Current Relationship");
                this.diplomacyCurrentLabelHeader.Color = ARGBColors.Black;
                this.diplomacyCurrentLabelHeader.Position = new Point(0, 40);
                this.diplomacyCurrentLabelHeader.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
                this.diplomacyCurrentLabelHeader.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.diplomacyCurrentLabelHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.diplomacyBackgroundImage.addControl(this.diplomacyCurrentLabelHeader);
                string str = "";
                int num = GameEngine.Instance.World.getUserRelationship(this.m_userInfo.userID);
                if (num == 0)
                {
                    str = str + SK.Text("GENERIC_Neutral", "Neutral");
                }
                else if (num > 0)
                {
                    str = str + SK.Text("GENERIC_Ally", "Ally");
                }
                else if (num < 0)
                {
                    str = str + SK.Text("GENERIC_Enemy", "Enemy");
                }
                this.diplomacyCurrentLabel.Text = str;
                this.diplomacyCurrentLabel.Color = ARGBColors.Black;
                this.diplomacyCurrentLabel.Position = new Point(0, 0x41);
                this.diplomacyCurrentLabel.Size = new Size(this.diplomacyBackgroundImage.Width, 30);
                this.diplomacyCurrentLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.diplomacyCurrentLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.diplomacyBackgroundImage.addControl(this.diplomacyCurrentLabel);
                this.diplomacyNeutralButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.diplomacyNeutralButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.diplomacyNeutralButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.diplomacyNeutralButton.Position = new Point(0x5f, 100);
                this.diplomacyNeutralButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_neutral", "Set As Neutral");
                this.diplomacyNeutralButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyNeutralButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyNeutralButton.TextYOffset = -3;
                this.diplomacyNeutralButton.Text.Color = ARGBColors.Black;
                this.diplomacyNeutralButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBreakAlliance_Click), "FactionMyFactionPanel_neutral_clicked");
                this.diplomacyBackgroundImage.addControl(this.diplomacyNeutralButton);
                this.diplomacyAllyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.diplomacyAllyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.diplomacyAllyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.diplomacyAllyButton.Position = new Point(0x5f, 150);
                this.diplomacyAllyButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_ally", "Set As Ally");
                this.diplomacyAllyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyAllyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyAllyButton.TextYOffset = -3;
                this.diplomacyAllyButton.Text.Color = ARGBColors.Black;
                this.diplomacyAllyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAlly_Click), "FactionMyFactionPanel_ally_clicked");
                this.diplomacyBackgroundImage.addControl(this.diplomacyAllyButton);
                this.diplomacyEnemyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.diplomacyEnemyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.diplomacyEnemyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.diplomacyEnemyButton.Position = new Point(0x5f, 200);
                this.diplomacyEnemyButton.Text.Text = SK.Text("FactionsDiplomacy_Set_as_enemy", "Set As Enemy");
                this.diplomacyEnemyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyEnemyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyEnemyButton.TextYOffset = -3;
                this.diplomacyEnemyButton.Text.Color = ARGBColors.Black;
                this.diplomacyEnemyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnMakeEnemy_Click), "FactionMyFactionPanel_enemy_clicked");
                this.diplomacyBackgroundImage.addControl(this.diplomacyEnemyButton);
                this.diplomacyCancelButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.diplomacyCancelButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.diplomacyCancelButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.diplomacyCancelButton.Position = new Point(130, 250);
                this.diplomacyCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                this.diplomacyCancelButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyCancelButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyCancelButton.TextYOffset = -3;
                this.diplomacyCancelButton.Text.Color = ARGBColors.Black;
                this.diplomacyCancelButton.setClickDelegate(() => this.removeOverlay(), "FactionMyFactionPanel_dipomacy_close");
                this.diplomacyBackgroundImage.addControl(this.diplomacyCancelButton);
                this.diplomacyEnemyButton.Enabled = true;
                this.diplomacyAllyButton.Enabled = true;
                this.diplomacyNeutralButton.Enabled = true;
            }
        }

        private void addVillages(int[] villages)
        {
            int num = 0;
            int villageID = -1;
            this.outgoingScrollArea.Position = new Point(0x62, 0x85);
            this.outgoingScrollArea.Size = new Size(360, 360);
            this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(360, 360));
            this.backgroundRight.addControl(this.outgoingScrollArea);
            int num1 = this.outgoingScrollBar.Value;
            this.outgoingScrollBar.Position = new Point(0x1cf, 0x85);
            this.outgoingScrollBar.Size = new Size(0x18, 360);
            this.backgroundRight.addControl(this.outgoingScrollBar);
            this.outgoingScrollBar.Value = 0;
            this.outgoingScrollBar.Max = 100;
            this.outgoingScrollBar.NumVisibleLines = 0x19;
            this.outgoingScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.outgoingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            List<int> list = new List<int>(villages);
            list.Sort(villageComparer);
            this.outgoingScrollArea.clearControls();
            int y = 0;
            for (int i = 0; i < list.Count; i++)
            {
                int num5 = list[i];
                VillageLine control = new VillageLine();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(num5, this, i);
                this.outgoingScrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
                WorldMap.VillageData data = GameEngine.Instance.World.getVillageData(num5);
                if ((data != null) && data.Capital)
                {
                    int num6 = 0;
                    if (data.regionCapital)
                    {
                        num6 = 1;
                    }
                    if (data.countyCapital)
                    {
                        num6 = 2;
                    }
                    if (data.provinceCapital)
                    {
                        num6 = 3;
                    }
                    if (data.countryCapital)
                    {
                        num6 = 4;
                    }
                    if (num6 > num)
                    {
                        num = num6;
                        villageID = num5;
                    }
                }
            }
            this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, y);
            if (y < this.outgoingScrollBar.Height)
            {
                this.outgoingScrollBar.Visible = false;
            }
            else
            {
                this.outgoingScrollBar.Visible = true;
                this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
                this.outgoingScrollBar.Max = y - this.outgoingScrollBar.Height;
            }
            this.outgoingScrollArea.invalidate();
            this.outgoingScrollBar.invalidate();
            if (num > 0)
            {
                this.positionImage.Image = (Image) GFXLibrary.char_position[num - 1];
                this.positionImage.Visible = true;
                string str = "";
                switch (num)
                {
                    case 1:
                        str = SK.Text("ParishWallPanel_Steward", "Steward") + " - " + GameEngine.Instance.World.getVillageName(villageID);
                        break;

                    case 2:
                        str = SK.Text("ParishWallPanel_Sheriff", "Sheriff") + " - " + GameEngine.Instance.World.getCountyName(GameEngine.Instance.World.getCountyFromVillageID(villageID));
                        break;

                    case 3:
                        str = SK.Text("ParishWallPanel_Governor", "Governor") + " - " + GameEngine.Instance.World.getProvinceName(GameEngine.Instance.World.getProvinceFromVillageID(villageID));
                        break;

                    case 4:
                        str = SK.Text("ParishWallPanel_King", "King") + " - " + GameEngine.Instance.World.getCountryName(GameEngine.Instance.World.getCountryFromVillageID(villageID));
                        break;
                }
                this.headerLabel2.Text = str;
            }
        }

        private void adminClick()
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_close");
            GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
            InterfaceMgr.Instance.closeParishPanel();
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
            InterfaceMgr.Instance.showUserInfoScreenAdmin(this.m_userInfo);
        }

        private void btnAlly_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            if (GameEngine.Instance.World.UserRelations.Count == 0)
            {
                StatTrackingClient.Instance().ActivateTrigger(11, 0);
            }
            GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, 1, this.m_userInfo.userName);
            RemoteServices.Instance.set_CreateUserRelationship_UserCallBack(new RemoteServices.CreateUserRelationship_UserCallBack(this.createUserRelationshipCallback));
            RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, 1);
        }

        private void btnBreakAlliance_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, 0, this.m_userInfo.userName);
            RemoteServices.Instance.set_CreateUserRelationship_UserCallBack(new RemoteServices.CreateUserRelationship_UserCallBack(this.createUserRelationshipCallback));
            RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, 0);
        }

        private void btnMakeEnemy_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            if (GameEngine.Instance.World.UserRelations.Count == 0)
            {
                StatTrackingClient.Instance().ActivateTrigger(11, 0);
            }
            GameEngine.Instance.World.setUserRelationship(this.m_userInfo.userID, -1, this.m_userInfo.userName);
            RemoteServices.Instance.set_CreateUserRelationship_UserCallBack(new RemoteServices.CreateUserRelationship_UserCallBack(this.createUserRelationshipCallback));
            RemoteServices.Instance.CreateUserRelationship(this.m_userInfo.userID, -1);
        }

        private void closeClick()
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_close");
            GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
            InterfaceMgr.Instance.closeParishPanel();
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        private void createUserRelationshipCallback(CreateUserRelationship_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.diplomacyOverlayVisible = false;
                this.init(this.m_userInfo);
            }
            else
            {
                this.diplomacyEnemyButton.Enabled = true;
                this.diplomacyAllyButton.Enabled = true;
                this.diplomacyNeutralButton.Enabled = true;
            }
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void editAvatarClicked()
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_edit_avatar");
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
        }

        private void editClicked()
        {
            Process.Start(URLs.shieldDesignerURL + "?UserGUID=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&SessionGUID=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
        }

        private void factionClicked()
        {
            if ((this.m_userInfo != null) && (this.m_userInfo.factionID >= 0))
            {
                GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction");
                InterfaceMgr.Instance.closeParishPanel();
                InterfaceMgr.Instance.showFactionPanel(this.m_userInfo.factionID);
            }
        }

        public static int getInt32FromString(string text)
        {
            if (text.Length != 0)
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch (Exception)
                {
                }
            }
            return 0;
        }

        private void houseClicked()
        {
            if ((this.m_userInfo != null) && (this.m_houseID > 0))
            {
                InterfaceMgr.Instance.closeParishPanel();
                InterfaceMgr.Instance.showHousePanel(this.m_houseID);
            }
        }

        public void init(WorldMap.CachedUserInfo userInfo)
        {
            base.clearControls();
            NumberFormatInfo nFI = GameEngine.NFI;
            this.m_houseID = 0;
            if (userInfo == null)
            {
                userInfo = new WorldMap.CachedUserInfo();
                userInfo.userID = this.m_userID;
            }
            this.m_userID = userInfo.userID;
            WorldMap.VillageRolloverInfo villageInfo = null;
            GameEngine.Instance.World.retrieveUserData(-1, userInfo.userID, ref villageInfo, ref userInfo, true, true);
            this.m_userInfo = userInfo;
            this.mainBackgroundImage.Size = new Size(base.Width, base.Height - 40);
            this.mainBackgroundImage.Position = new Point(0, 40);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            this.mainHeaderArea.Position = new Point(0, -40);
            this.mainHeaderArea.Size = new Size(0x3e0, 0x2d);
            this.mainBackgroundImage.addControl(this.mainHeaderArea);
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            this.mainHeaderArea.addControl(this.headerImage);
            this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            this.positionImage.Image = (Image) GFXLibrary.char_position[0];
            this.positionImage.Position = new Point(9, 7);
            this.positionImage.Visible = false;
            this.mainHeaderArea.addControl(this.positionImage);
            if (userInfo != null)
            {
                this.headerLabel.Text = userInfo.userName;
            }
            else
            {
                this.headerLabel.Text = "";
            }
            this.headerLabel.Color = ARGBColors.White;
            this.headerLabel.DropShadowColor = ARGBColors.Black;
            this.headerLabel.Position = new Point(0x27, 10);
            this.headerLabel.Size = new Size(500, 50);
            this.headerLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainHeaderArea.addControl(this.headerLabel);
            Size textSizeX = this.headerLabel.TextSizeX;
            this.headerLabel2.Text = "";
            this.headerLabel2.Color = Color.FromArgb(0xad, 0xc3, 0xd0);
            this.headerLabel2.DropShadowColor = ARGBColors.Black;
            this.headerLabel2.Position = new Point((this.headerLabel.Position.X + textSizeX.Width) + 5, 12);
            this.headerLabel2.Size = new Size(700, 0x1c);
            this.headerLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.headerLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainHeaderArea.addControl(this.headerLabel2);
            if ((userInfo != null) && (userInfo.standing >= 0))
            {
                this.standingLabelLabel.Text = SK.Text("UserInfoScreen_Rank", "Rank") + " : " + userInfo.standing.ToString("N", nFI);
            }
            else
            {
                this.standingLabelLabel.Text = "";
            }
            this.standingLabelLabel.Color = Color.FromArgb(0xad, 0xc3, 0xd0);
            this.standingLabelLabel.DropShadowColor = ARGBColors.Black;
            this.standingLabelLabel.Position = new Point(650, 12);
            this.standingLabelLabel.Size = new Size(700, 0x1c);
            this.standingLabelLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.standingLabelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainHeaderArea.addControl(this.standingLabelLabel);
            if (userInfo != null)
            {
                this.pointsLabel.Text = SK.Text("GENERIC_Points", "Points") + " : " + userInfo.points.ToString("N", nFI);
            }
            else
            {
                this.pointsLabel.Text = "";
            }
            this.pointsLabel.Color = Color.FromArgb(0xad, 0xc3, 0xd0);
            this.pointsLabel.DropShadowColor = ARGBColors.Black;
            this.pointsLabel.Position = new Point(0x307, 12);
            this.pointsLabel.Size = new Size(700, 0x1c);
            this.pointsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainHeaderArea.addControl(this.pointsLabel);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 4);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "UserInfo2_close");
            this.closeButton.CustomTooltipID = 0x1f6;
            this.mainHeaderArea.addControl(this.closeButton);
            if ((userInfo != null) && (userInfo.avatarData != null))
            {
                this.backgroundLeft.Image = (Image) GFXLibrary.char_portraite_shadow;
                this.backgroundLeft.Position = new Point(5, 0);
                this.mainBackgroundImage.addControl(this.backgroundLeft);
            }
            this.backgroundRight.Image = (Image) GFXLibrary.char_villagelist_inset;
            this.backgroundRight.Position = new Point((base.Width - 7) - GFXLibrary.char_villagelist_inset.Width, 1);
            this.mainBackgroundImage.addControl(this.backgroundRight);
            this.backgroundCentre.Image = (Image) GFXLibrary.char_shieldcomp_back;
            this.backgroundCentre.Position = new Point(0x12b, 1);
            this.mainBackgroundImage.addControl(this.backgroundCentre);
            if (userInfo != null)
            {
                this.nameLabel.Text = userInfo.userName;
                this.nameLabel.Color = ARGBColors.Black;
                this.nameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.nameLabel.Position = new Point(11, 30);
                this.nameLabel.Size = new Size(180, 0x2d);
                this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundCentre.addControl(this.nameLabel);
                if (userInfo.avatarData != null)
                {
                    this.rankLabel.TextDiffOnly = Rankings.getRankingName(userInfo.rank, userInfo.avatarData.male);
                }
                else
                {
                    this.rankLabel.TextDiffOnly = Rankings.getRankingName(userInfo.rank);
                }
                this.rankLabel.Color = ARGBColors.Black;
                this.rankLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.rankLabel.Position = new Point(11, 0x3d);
                this.rankLabel.Size = new Size(180, 20);
                this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.backgroundCentre.addControl(this.rankLabel);
                if (userInfo.avatarData != null)
                {
                    if (lastCreatedAvatar != null)
                    {
                        lastCreatedAvatar.Dispose();
                    }
                    lastCreatedAvatar = this.avatarImage.Image = Avatar.CreateAvatar(userInfo.avatarData, ARGBColors.Transparent);
                    this.avatarImage.Position = new Point(0x49, 0x16);
                    this.mainBackgroundImage.addControl(this.avatarImage);
                }
                this.shieldImage.Image = GameEngine.Instance.World.getWorldShieldOrBlank(userInfo.userID, 140, 0x9c);
                if (this.shieldImage.Image != null)
                {
                    this.shieldImage.Position = new Point(0x18, 0x66);
                    if (userInfo.userID == RemoteServices.Instance.UserID)
                    {
                        this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "UserInfo2_edit_shield_shield_clicked");
                        this.shieldImage.CustomTooltipID = 0xfaf;
                    }
                    else
                    {
                        this.shieldImage.setClickDelegate(null);
                        this.shieldImage.CustomTooltipID = 0;
                    }
                    this.backgroundCentre.addControl(this.shieldImage);
                }
                if (userInfo.factionID >= 0)
                {
                    FactionData data = GameEngine.Instance.World.getFaction(userInfo.factionID);
                    if (data != null)
                    {
                        this.flagImageShadow.Image = (Image) GFXLibrary.char_shadow_faction;
                        this.flagImageShadow.Position = new Point(130, 0x103);
                        this.backgroundCentre.addControl(this.flagImageShadow);
                        this.flagImage.createFromFlagData(data.flagData);
                        this.flagImage.CustomTooltipData = data.factionID;
                        this.flagImage.Position = new Point(0x80, 0x101);
                        this.flagImage.Scale = 0.25;
                        this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "UserInfoPanel2_faction_flag");
                        this.flagImage.CustomTooltipID = 0x9c5;
                        this.backgroundCentre.addControl(this.flagImage);
                        this.factionLabel.Text = data.factionNameAbrv;
                        this.factionLabel.Color = ARGBColors.Black;
                        this.factionLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                        this.factionLabel.Position = new Point(11, 0x137);
                        this.factionLabel.Size = new Size(180, 20);
                        this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                        this.factionLabel.CustomTooltipID = 0x9c5;
                        this.factionLabel.CustomTooltipData = data.factionID;
                        this.factionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "UserInfoPanel2_faction_flag");
                        this.backgroundCentre.addControl(this.factionLabel);
                        if (data.houseID > 0)
                        {
                            this.houseImageShadow.Image = (Image) GFXLibrary.char_shadow_house;
                            this.houseImageShadow.Position = new Point(10, 0xf7);
                            this.backgroundCentre.addControl(this.houseImageShadow);
                            this.houseImage.Image = this.houseImage.Image = (Image) GFXLibrary.house_circles_medium[data.houseID - 1];
                            this.houseImage.CustomTooltipData = data.houseID;
                            this.houseImage.Position = new Point(10, 0xf7);
                            this.houseImage.CustomTooltipID = 0x903;
                            this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "UserInfoPanel2_house");
                            this.backgroundCentre.addControl(this.houseImage);
                            this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + data.houseID.ToString();
                            this.houseLabel.Color = ARGBColors.Black;
                            this.houseLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                            this.houseLabel.Position = new Point(11, 0x11d);
                            this.houseLabel.Size = new Size(180, 20);
                            this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                            this.houseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "UserInfoPanel2_house");
                            this.backgroundCentre.addControl(this.houseLabel);
                            this.m_houseID = data.houseID;
                        }
                    }
                }
                int num = GameEngine.Instance.World.getYourFactionRank();
                if ((((GameEngine.Instance.World.YourFaction != null) && (userInfo != null)) && ((userInfo.userID != RemoteServices.Instance.UserID) && (GameEngine.Instance.World.FactionMembers != null))) && (num > 0))
                {
                    this.inviteButton.ImageNorm = (Image) GFXLibrary.char_but_invite[0];
                    this.inviteButton.ImageOver = (Image) GFXLibrary.char_but_invite[1];
                    this.inviteButton.Position = new Point(0x3e, 0x15a);
                    this.inviteButton.MoveOnClick = true;
                    this.inviteButton.Text.Text = SK.Text("UserInfoScreen_InviteToFaction", "Invite To Faction");
                    this.inviteButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                    this.inviteButton.Text.Position = new Point(3, 0x1a);
                    this.inviteButton.Text.Size = new Size(70, 0x1b);
                    this.inviteButton.TextYOffset = 0;
                    this.inviteButton.Text.Color = ARGBColors.Black;
                    this.inviteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.inviteToFactionClicked), "UserInfoPanel2_invite_to_faction_clicked");
                    this.backgroundCentre.addControl(this.inviteButton);
                }
                this.mailButton.ImageNorm = (Image) GFXLibrary.char_but_mail[0];
                this.mailButton.ImageOver = (Image) GFXLibrary.char_but_mail[1];
                this.mailButton.Position = new Point(0x3e, 0x1a1);
                this.mailButton.MoveOnClick = true;
                this.mailButton.Text.Text = SK.Text("User_Send_A_Message", "Send a Message");
                this.mailButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                this.mailButton.Text.Position = new Point(3, 0x1a);
                this.mailButton.Text.Size = new Size(70, 0x1b);
                this.mailButton.TextYOffset = 0;
                this.mailButton.Text.Color = ARGBColors.Black;
                this.mailButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendMailClicked), "UserInfoPanel2_send_mail_clicked");
                this.backgroundCentre.addControl(this.mailButton);
                this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.diplomacyButton.Position = new Point(0x27b, 0x1fd);
                if (userInfo.userID == RemoteServices.Instance.UserID)
                {
                    this.diplomacyButton.Text.Text = SK.Text("User_Manage_Relations", "Manage Diplomacy");
                    this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.manageDiplomacyClicked), "FactionMyFactionPanel_diplomacy");
                }
                else
                {
                    string str = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy") + " : ";
                    int num2 = GameEngine.Instance.World.getUserRelationship(userInfo.userID);
                    if (num2 == 0)
                    {
                        str = str + SK.Text("GENERIC_Neutral", "Neutral");
                    }
                    else if (num2 > 0)
                    {
                        str = str + SK.Text("GENERIC_Ally", "Ally");
                    }
                    else if (num2 < 0)
                    {
                        str = str + SK.Text("GENERIC_Enemy", "Enemy");
                    }
                    this.diplomacyButton.Text.Text = str;
                    this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addDiplomacyOverlay), "FactionMyFactionPanel_diplomacy");
                }
                this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.diplomacyButton.TextYOffset = -3;
                this.diplomacyButton.Text.Color = ARGBColors.Black;
                this.mainBackgroundImage.addControl(this.diplomacyButton);
            }
            if (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator)
            {
                this.adminButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
                this.adminButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
                this.adminButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
                this.adminButton.Position = new Point(0x48, 0x1e1);
                this.adminButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.adminClick));
                this.adminButton.CustomTooltipID = 0xc1d;
                this.backgroundCentre.addControl(this.adminButton);
            }
            if ((userInfo != null) && (this.m_userInfo.userID == RemoteServices.Instance.UserID))
            {
                this.editButton.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info;
                this.editButton.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_over;
                this.editButton.MoveOnClick = true;
                this.editButton.Position = new Point(0x39, 0x4f);
                this.editButton.Text.Text = SK.Text("User_Edit", "Edit");
                this.editButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                this.editButton.TextYOffset = -3;
                this.editButton.Text.Color = Color.FromArgb(0xe9, 0xe7, 0xd5);
                this.editButton.Text.Position = new Point(-3, 0);
                this.editButton.Text.DropShadowColor = ARGBColors.Black;
                this.editButton.CustomTooltipID = 0xfaf;
                this.editButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "UserInfo2_edit_shield_button_clicked");
                this.backgroundCentre.addControl(this.editButton);
                this.editAvatarButton.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info;
                this.editAvatarButton.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_over;
                this.editAvatarButton.MoveOnClick = true;
                this.editAvatarButton.Position = new Point(0x6a, 0x1f3);
                this.editAvatarButton.Text.Text = SK.Text("User_Edit", "Edit");
                this.editAvatarButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                this.editAvatarButton.TextYOffset = -3;
                this.editAvatarButton.Text.Color = Color.FromArgb(0xe9, 0xe7, 0xd5);
                this.editAvatarButton.Text.Position = new Point(-3, 0);
                this.editAvatarButton.Text.DropShadowColor = ARGBColors.Black;
                this.editAvatarButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editAvatarClicked), "UserInfo2_edit_avatar_clicked");
                this.mainBackgroundImage.addControl(this.editAvatarButton);
            }
            this.achievementsButton.ImageNorm = (Image) GFXLibrary.char_but_achievement[0];
            this.achievementsButton.ImageOver = (Image) GFXLibrary.char_but_achievement[1];
            this.achievementsButton.ImageClick = (Image) GFXLibrary.char_but_achievement[2];
            this.achievementsButton.Position = new Point(0x248, 11);
            this.achievementsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.achievementsClicked));
            this.mainBackgroundImage.addControl(this.achievementsButton);
            if ((userInfo != null) && (userInfo.achievements != null))
            {
                this.achievementsLabel.Text = SK.Text("GENERIC_Achievements", "Achievements") + " : " + userInfo.achievements.Count.ToString("N", nFI);
            }
            else
            {
                this.achievementsLabel.Text = "";
            }
            this.achievementsLabel.Color = ARGBColors.White;
            this.achievementsLabel.DropShadowColor = ARGBColors.Black;
            this.achievementsLabel.Position = new Point(0x270, 0x11);
            this.achievementsLabel.Size = new Size(300, 0x1c);
            this.achievementsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.achievementsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.achievementsLabel);
            this.questsButton.ImageNorm = (Image) GFXLibrary.char_but_quest[0];
            this.questsButton.ImageOver = (Image) GFXLibrary.char_but_quest[1];
            this.questsButton.ImageClick = (Image) GFXLibrary.char_but_quest[2];
            if ((userInfo == null) || (userInfo.completedQuests == null))
            {
                this.questsButton.ImageOver = (Image) GFXLibrary.char_but_quest[0];
                this.questsButton.ImageClick = (Image) GFXLibrary.char_but_quest[0];
                this.questsButton.setClickDelegate(null);
            }
            else
            {
                this.questsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.questsClicked), "UserInfo2_quests_clicked");
            }
            this.questsButton.Position = new Point(0x248, 0x30);
            this.mainBackgroundImage.addControl(this.questsButton);
            if (userInfo != null)
            {
                this.questsLabel.Text = SK.Text("User_Quests_Complete", "Quests Completed") + " : " + userInfo.numQuests.ToString("N", nFI);
            }
            else
            {
                this.questsLabel.Text = "";
            }
            this.questsLabel.Color = ARGBColors.White;
            this.questsLabel.DropShadowColor = ARGBColors.Black;
            this.questsLabel.Position = new Point(0x270, 0x36);
            this.questsLabel.Size = new Size(300, 0x1c);
            this.questsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.questsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.questsLabel);
            this.headerLabelsImage.Size = new Size(400, 0x1c);
            this.headerLabelsImage.Position = new Point(0x59, 0x62);
            this.backgroundRight.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(0xda, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.villageLabel.Text = SK.Text("GENERIC_Village", "Village");
            this.villageLabel.Color = ARGBColors.Black;
            this.villageLabel.Position = new Point(20, -3);
            this.villageLabel.Size = new Size(0xd0, this.headerLabelsImage.Height);
            this.villageLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.villageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.villageLabel);
            this.regionLabel.Text = SK.Text("Users_Region", "Region");
            this.regionLabel.Color = ARGBColors.Black;
            this.regionLabel.Position = new Point(0xde, -3);
            this.regionLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.regionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.regionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.regionLabel);
            if ((userInfo != null) && (userInfo.villages != null))
            {
                this.addVillages(userInfo.villages);
            }
            base.Invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 590);
            this.MinimumSize = new Size(0x3e0, 590);
            base.Name = "UserInfoScreen2";
            base.Size = new Size(0x3e0, 590);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public void inviteToFactionClicked()
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction_invite");
            InterfaceMgr.Instance.clearControls();
            InterfaceMgr.Instance.inviteToFaction(this.m_userInfo.userName);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void manageDiplomacyClicked()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(60, false);
        }

        private void questsClicked()
        {
            InterfaceMgr.Instance.openNewQuestsCompletedPopup(this.m_userInfo.completedQuests);
        }

        public void removeOverlay()
        {
            this.mainBackgroundImage.removeControl(this.greyOverlay);
            this.greyOverlay.clearControls();
            base.Invalidate();
            this.diplomacyOverlayVisible = false;
        }

        private void sendMailClicked()
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
            if (this.m_userInfo != null)
            {
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x15);
                InterfaceMgr.Instance.mailTo(this.m_userInfo.userID, this.m_userInfo.userName);
            }
        }

        public void update()
        {
            WorldMap.CachedUserInfo userInfo = GameEngine.Instance.World.getStoredUserInfo(this.m_userID);
            if (this.m_userInfo != userInfo)
            {
                this.init(userInfo);
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.outgoingScrollBar.Value;
            this.outgoingScrollArea.Position = new Point(this.outgoingScrollArea.X, 0x85 - y);
            this.outgoingScrollArea.ClipRect = new Rectangle(this.outgoingScrollArea.ClipRect.X, y, this.outgoingScrollArea.ClipRect.Width, this.outgoingScrollArea.ClipRect.Height);
            this.outgoingScrollArea.invalidate();
            this.outgoingScrollBar.invalidate();
        }

        public class VillageComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                WorldMap.VillageData data = GameEngine.Instance.World.getVillageData(x);
                WorldMap.VillageData data2 = GameEngine.Instance.World.getVillageData(y);
                if (data == null)
                {
                    if (data2 == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (data2 == null)
                {
                    return 1;
                }
                if (data.Capital && !data2.Capital)
                {
                    return 1;
                }
                if (!data.Capital && data2.Capital)
                {
                    return -1;
                }
                if (data.Capital && data2.Capital)
                {
                    int num = 0;
                    int num2 = 0;
                    if (data.countyCapital)
                    {
                        num = 1;
                    }
                    else if (data.provinceCapital)
                    {
                        num = 2;
                    }
                    else if (data.countryCapital)
                    {
                        num = 3;
                    }
                    if (data2.countyCapital)
                    {
                        num2 = 1;
                    }
                    else if (data2.provinceCapital)
                    {
                        num2 = 2;
                    }
                    else if (data2.countryCapital)
                    {
                        num2 = 3;
                    }
                    if (num < num2)
                    {
                        return -1;
                    }
                    if (num2 < num)
                    {
                        return 1;
                    }
                }
                return data.villageName.CompareTo(data2.villageName);
            }
        }

        public class VillageLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private UserInfoScreen2 m_parent;
            private int m_villageID = -1;
            private CustomSelfDrawPanel.CSDLabel regionLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage villageImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel villageNameLabel = new CustomSelfDrawPanel.CSDLabel();

            public void init(int villageID, UserInfoScreen2 parent, int position)
            {
                this.m_villageID = villageID;
                this.m_parent = parent;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.char_line_01;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.char_line_02;
                }
                this.backgroundImage.Position = new Point(0, 5);
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                base.addControl(this.backgroundImage);
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.Size = new Size(360, 30);
                this.villageNameLabel.Text = GameEngine.Instance.World.getVillageName(villageID);
                this.villageNameLabel.Color = ARGBColors.Black;
                this.villageNameLabel.RolloverColor = ARGBColors.White;
                this.villageNameLabel.Position = new Point(50, -10);
                this.villageNameLabel.Size = new Size(160, this.backgroundImage.Height + 20);
                this.villageNameLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.villageNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.villageNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.addControl(this.villageNameLabel);
                this.regionLabel.Color = ARGBColors.Black;
                this.regionLabel.RolloverColor = ARGBColors.White;
                this.regionLabel.Position = new Point(220, -10);
                this.regionLabel.Size = new Size(140, this.backgroundImage.Height + 20);
                this.regionLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.regionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.regionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.addControl(this.regionLabel);
                if (GameEngine.Instance.World.isCapital(villageID))
                {
                    int num = 0;
                    if (GameEngine.Instance.World.isRegionCapital(villageID))
                    {
                        num = 0;
                        int countyID = GameEngine.Instance.World.getVillageCounty(villageID);
                        this.regionLabel.Text = GameEngine.Instance.World.getCountyName(countyID);
                    }
                    else if (GameEngine.Instance.World.isCountyCapital(villageID))
                    {
                        num = 1;
                        int num3 = GameEngine.Instance.World.getVillageCounty(villageID);
                        this.regionLabel.Text = GameEngine.Instance.World.getCountyName(num3);
                    }
                    else if (GameEngine.Instance.World.isProvinceCapital(villageID))
                    {
                        num = 2;
                        int provinceID = GameEngine.Instance.World.getProvinceFromVillageID(villageID);
                        this.regionLabel.Text = GameEngine.Instance.World.getProvinceName(provinceID);
                    }
                    else if (GameEngine.Instance.World.isCountryCapital(villageID))
                    {
                        num = 3;
                        int countryID = GameEngine.Instance.World.getCountryFromVillageID(villageID);
                        this.regionLabel.Text = GameEngine.Instance.World.getCountryName(countryID);
                    }
                    this.villageImage.Image = (Image) GFXLibrary.char_position[num + 4];
                    this.villageImage.Position = new Point(10, -4);
                    this.villageImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    this.backgroundImage.addControl(this.villageImage);
                }
                else
                {
                    int index = GameEngine.Instance.World.getVillageSize(villageID);
                    this.villageImage.Image = (Image) GFXLibrary.char_village_icons[index];
                    this.villageImage.Position = new Point(-5, -18);
                    this.villageImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    this.backgroundImage.addControl(this.villageImage);
                    int num7 = GameEngine.Instance.World.getVillageCounty(villageID);
                    this.regionLabel.Text = GameEngine.Instance.World.getCountyName(num7);
                }
            }

            private void lineClicked()
            {
                if (this.m_villageID >= 0)
                {
                    if (RemoteServices.Instance.Admin && GameEngine.shiftPressed)
                    {
                        AGUR agur = new AGUR();
                        agur.init(this.m_villageID);
                        agur.Show(InterfaceMgr.Instance.ParentForm);
                    }
                    else
                    {
                        GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
                        Point point = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
                        GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                        InterfaceMgr.Instance.closeParishPanel();
                        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
                    }
                }
            }

            public bool update(double localTime)
            {
                return true;
            }
        }
    }
}

