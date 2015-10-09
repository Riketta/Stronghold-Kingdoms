namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class UserInfoPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage avatarBackImage = new CustomSelfDrawPanel.CSDImage();
        public Bitmap avatarBitmap;
        private CustomSelfDrawPanel.CSDImage avatarImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
        private int lastFlagData = -1;
        private int lastShieldUserID = -1;
        private int lastUserID = -1;
        private WorldMap.CachedUserInfo m_userInfo;
        private CustomSelfDrawPanel.CSDButton mailToButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton moreInfo = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();

        public UserInfoPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            this.closing();
        }

        private void closing()
        {
            GameEngine.Instance.World.showShieldUser(-1);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
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

        private void factionClicked()
        {
            if ((this.m_userInfo != null) && (this.m_userInfo.factionID >= 0))
            {
                InterfaceMgr.Instance.showFactionPanel(this.m_userInfo.factionID);
            }
        }

        private void houseClicked()
        {
            if ((this.m_userInfo != null) && (this.m_userInfo.factionID >= 0))
            {
                FactionData data = GameEngine.Instance.World.getFaction(this.m_userInfo.factionID);
                if (data != null)
                {
                    InterfaceMgr.Instance.showHousePanel(data.houseID);
                }
            }
        }

        public void init()
        {
            this.lastShieldUserID = -1;
            base.clearControls();
            this.avatarBackImage.Image = (Image) GFXLibrary.mrhp_avatar_frame_background;
            this.avatarBackImage.Position = new Point(0, 110);
            base.addControl(this.avatarBackImage);
            this.avatarImage.Image = null;
            this.avatarImage.Visible = false;
            this.avatarImage.Position = new Point(0x47, 0x71);
            this.background.Image = (Image) GFXLibrary.mrhp_avatar_frame;
            this.background.Position = new Point(0, 0);
            base.addControl(this.avatarImage);
            base.addControl(this.background);
            this.flagImage.createFromFlagData(0);
            this.flagImage.Position = new Point(0x88, 0x30);
            this.flagImage.Scale = 0.25;
            this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "UserInfoPanel2_faction_flag");
            this.flagImage.Visible = false;
            this.flagImage.CustomTooltipID = 0x9c5;
            this.background.addControl(this.flagImage);
            this.houseImage.Image = null;
            this.houseImage.Position = new Point(15, 0x26);
            this.houseImage.Visible = false;
            this.houseImage.CustomTooltipID = 0x903;
            this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "UserInfoPanel2_house");
            this.background.addControl(this.houseImage);
            this.shieldImage.Image = null;
            this.shieldImage.Position = new Point(0x4a, 0x1f);
            this.shieldImage.Visible = false;
            this.background.addControl(this.shieldImage);
            this.nameLabel.Text = "";
            this.nameLabel.Color = ARGBColors.Black;
            this.nameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.nameLabel.Position = new Point(8, 0x4f);
            this.nameLabel.Size = new Size(this.background.Width - 12, 0x2d);
            this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.nameClicked), "UserInfoPanel2_name");
            this.background.addControl(this.nameLabel);
            this.rankLabel.Text = "";
            this.rankLabel.Color = ARGBColors.Black;
            this.rankLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.rankLabel.Position = new Point(8, 0x13d);
            this.rankLabel.Size = new Size(this.background.Width - 12, 20);
            this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.background.addControl(this.rankLabel);
            this.pointsLabel.Text = "";
            this.pointsLabel.Color = ARGBColors.Black;
            this.pointsLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.pointsLabel.Position = new Point(8, 0x14e);
            this.pointsLabel.Size = new Size(this.background.Width - 12, 20);
            this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.background.addControl(this.pointsLabel);
            this.moreInfo.ImageNorm = (Image) GFXLibrary.mrhp_button_more_info_solid[0];
            this.moreInfo.ImageOver = (Image) GFXLibrary.mrhp_button_more_info_solid[1];
            this.moreInfo.Position = new Point(((200 - this.moreInfo.ImageNorm.Width) / 2) + 6, 0x161);
            this.moreInfo.MoveOnClick = true;
            this.moreInfo.Text.Text = SK.Text("UserInfo_MoreInfo", "More Info");
            if (((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "tr")) || (Program.mySettings.LanguageIdent == "pt"))
            {
                this.moreInfo.Text.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Bold);
            }
            else
            {
                this.moreInfo.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            }
            this.moreInfo.TextYOffset = -3;
            this.moreInfo.Text.Position = new Point(-3, 0);
            this.moreInfo.Text.Color = ARGBColors.Black;
            this.moreInfo.Text.DropShadowColor = Color.FromArgb(60, 90, 100);
            this.moreInfo.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.nameClicked), "UserInfoPanel2_more_info");
            this.background.addControl(this.moreInfo);
            this.mailToButton.ImageNorm = (Image) GFXLibrary.mrhp_button_envelope[0];
            this.mailToButton.ImageOver = (Image) GFXLibrary.mrhp_button_envelope[1];
            this.mailToButton.ImageClick = (Image) GFXLibrary.mrhp_button_envelope[2];
            this.mailToButton.Position = new Point(0x9d, 0x103);
            this.mailToButton.CustomTooltipID = 0x9c6;
            this.mailToButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailToClick), "UserInfoPanel2_mail_to");
            this.background.addControl(this.mailToButton);
            this.lastFlagData = -1;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "UserInfoPanel2";
            base.Size = new Size(200, 0x17a);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void mailToClick()
        {
            if ((this.m_userInfo != null) && (this.m_userInfo.userID >= 0))
            {
                int userID = this.m_userInfo.userID;
                string userName = this.m_userInfo.userName;
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x15);
                InterfaceMgr.Instance.mailTo(userID, userName);
            }
        }

        private void nameClicked()
        {
            if (this.m_userInfo != null)
            {
                InterfaceMgr.Instance.showUserInfoScreen(this.m_userInfo);
            }
        }

        public void updateVillageInfo(WorldMap.VillageRolloverInfo villageInfo, WorldMap.CachedUserInfo userInfo)
        {
            CustomTooltipManager.UserInfo = userInfo;
            this.m_userInfo = userInfo;
            this.pointsLabel.CustomTooltipID = 0;
            this.rankLabel.CustomTooltipID = 0;
            if (userInfo == null)
            {
                this.avatarImage.Visible = false;
                this.rankLabel.TextDiffOnly = "";
                this.nameLabel.TextDiffOnly = "";
                this.pointsLabel.TextDiffOnly = "";
            }
            else
            {
                this.pointsLabel.CustomTooltipID = 0x9c7;
                this.rankLabel.CustomTooltipID = 0x9c7;
                NumberFormatInfo nFI = GameEngine.NFI;
                this.nameLabel.TextDiffOnly = userInfo.userName;
                if (userInfo.avatarData != null)
                {
                    this.rankLabel.TextDiffOnly = Rankings.getRankingName(userInfo.rank, userInfo.avatarData.male);
                }
                else
                {
                    this.rankLabel.TextDiffOnly = Rankings.getRankingName(userInfo.rank);
                }
                if (userInfo.userID != this.lastShieldUserID)
                {
                    this.lastShieldUserID = userInfo.userID;
                    this.shieldImage.Image = GameEngine.Instance.World.getWorldShieldOrBlank(userInfo.userID, 0x2f, 0x36);
                    if (this.shieldImage.Image != null)
                    {
                        GameEngine.Instance.World.showShieldUser(this.lastShieldUserID);
                        this.shieldImage.Visible = true;
                    }
                    else
                    {
                        this.shieldImage.Visible = false;
                    }
                }
                if (userInfo.factionID >= 0)
                {
                    FactionData data = GameEngine.Instance.World.getFaction(userInfo.factionID);
                    if (data != null)
                    {
                        if (this.lastFlagData != data.flagData)
                        {
                            this.flagImage.createFromFlagData(data.flagData);
                        }
                        this.flagImage.CustomTooltipData = data.factionID;
                        this.flagImage.Visible = true;
                        if (data.houseID > 0)
                        {
                            this.houseImage.Image = this.houseImage.Image = (Image) GFXLibrary.house_circles_medium[data.houseID - 1];
                            this.houseImage.CustomTooltipData = data.houseID;
                            this.houseImage.Visible = true;
                        }
                        else
                        {
                            this.houseImage.Visible = false;
                        }
                    }
                    else
                    {
                        this.flagImage.Visible = false;
                        this.houseImage.Visible = false;
                    }
                }
                else
                {
                    this.flagImage.Visible = false;
                    this.houseImage.Visible = false;
                }
                this.avatarImage.Image = userInfo.avatarBitmap;
                this.avatarImage.Visible = true;
                int numVillages = userInfo.numVillages;
                if (GameEngine.Instance.LocalWorldData.AIWorld)
                {
                    switch (userInfo.userID)
                    {
                        case 1:
                            numVillages = GameEngine.Instance.World.countRatsCastles();
                            break;

                        case 2:
                            numVillages = GameEngine.Instance.World.countSnakesCastles();
                            break;

                        case 3:
                            numVillages = GameEngine.Instance.World.countPigsCastles();
                            break;

                        case 4:
                            numVillages = GameEngine.Instance.World.countWolfsCastles();
                            break;
                    }
                }
                this.pointsLabel.TextDiffOnly = SK.Text("GENERIC_Villages", "Villages") + " : " + numVillages.ToString("N", nFI);
                this.lastUserID = userInfo.userID;
            }
        }
    }
}

