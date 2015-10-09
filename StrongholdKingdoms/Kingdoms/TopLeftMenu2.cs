namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class TopLeftMenu2 : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton cardsButton = new CustomSelfDrawPanel.CSDButton();
        private CastleInfoBar2 castleInfoBar = new CastleInfoBar2();
        private IContainer components;
        private CustomSelfDrawPanel.CSDArea contextTabBar = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea controlsArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel currentGoldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea currentGoldToolTip = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel currentHonourLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea currentHonourToolTip = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel faithPointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea faithpointsToolTip = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel gameDateLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage panelConnectorImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea pointsToolTip = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage secondAgeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel userNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private VillageInfoBar2 villageInfoBar = new VillageInfoBar2();

        public TopLeftMenu2()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void cardsClick()
        {
            GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_screen_top");
            InterfaceMgr.Instance.openPlayCardsWindow(0);
        }

        public bool contextBarVisible()
        {
            return this.contextTabBar.Visible;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public int getCardAreaXPos()
        {
            return this.cardsButton.Position.X;
        }

        public VillageInfoBar2 getVillageInfoBar()
        {
            return this.villageInfoBar;
        }

        private void imgRealShield_Click()
        {
            GameEngine.Instance.playInterfaceSound("TopLeftMenu_ShieldClicked");
            Process.Start(URLs.shieldDesignerURL + "?UserGUID=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&SessionGUID=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
        }

        public void init()
        {
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.interface_bar_top_left_empty;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.panelConnectorImage.Image = (Image) GFXLibrary.menubar_connecter_left;
            this.panelConnectorImage.Position = new Point(0x161, 0);
            base.addControl(this.panelConnectorImage);
            this.controlsArea.Position = new Point(0, 0);
            this.controlsArea.Size = base.Size;
            base.addControl(this.controlsArea);
            Image image = GameEngine.Instance.World.getPlayerShieldImage(0x45, 0x4d);
            if (image != null)
            {
                this.shieldImage.Image = image;
                this.shieldImage.Position = new Point(2, 2);
                this.shieldImage.CustomTooltipID = 0xfaf;
                this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.imgRealShield_Click));
                this.controlsArea.addControl(this.shieldImage);
            }
            this.SetFaithPoints(0.0);
            this.secondAgeImage.Image = (Image) GFXLibrary.secondAgeLogo;
            this.secondAgeImage.Visible = false;
            this.secondAgeImage.CustomTooltipID = 8;
            this.secondAgeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.secondAgeImage_Click));
            this.controlsArea.addControl(this.secondAgeImage);
            this.userNameLabel.Position = new Point(0x67, 0);
            this.userNameLabel.Size = new Size(0xe0, 0x12);
            this.userNameLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 12f);
            this.userNameLabel.Color = ARGBColors.Black;
            this.userNameLabel.CustomTooltipID = 2;
            this.controlsArea.addControl(this.userNameLabel);
            this.currentGoldLabel.Position = new Point(130, 0x40);
            this.currentGoldLabel.Size = new Size(80, 0x12);
            this.currentGoldLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.currentGoldLabel.Color = ARGBColors.White;
            this.currentGoldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.currentGoldLabel.CustomTooltipID = 5;
            this.controlsArea.addControl(this.currentGoldLabel);
            this.currentGoldToolTip.Position = new Point(90, 0x40);
            this.currentGoldToolTip.Size = new Size(40, 0x12);
            this.currentGoldToolTip.CustomTooltipID = 5;
            this.controlsArea.addControl(this.currentGoldToolTip);
            this.currentHonourLabel.Position = new Point(130, 40);
            this.currentHonourLabel.Size = new Size(80, 0x12);
            this.currentHonourLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.currentHonourLabel.Color = ARGBColors.White;
            this.currentHonourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.currentHonourLabel.CustomTooltipID = 4;
            this.controlsArea.addControl(this.currentHonourLabel);
            this.currentHonourToolTip.Position = new Point(90, 40);
            this.currentHonourToolTip.Size = new Size(40, 0x12);
            this.currentHonourToolTip.CustomTooltipID = 4;
            this.controlsArea.addControl(this.currentHonourToolTip);
            this.rankLabel.Position = new Point(0x68, 0x10);
            this.rankLabel.Size = new Size(0xe0, 0x17);
            this.rankLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 9f);
            this.rankLabel.Color = ARGBColors.Black;
            this.rankLabel.CustomTooltipID = 3;
            this.controlsArea.addControl(this.rankLabel);
            this.pointsLabel.Position = new Point(0x107, 40);
            this.pointsLabel.Size = new Size(80, 0x12);
            this.pointsLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.pointsLabel.Color = ARGBColors.White;
            this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.pointsLabel.CustomTooltipID = 7;
            this.controlsArea.addControl(this.pointsLabel);
            this.pointsToolTip.Position = new Point(0xdf, 40);
            this.pointsToolTip.Size = new Size(40, 0x12);
            this.pointsToolTip.CustomTooltipID = 7;
            this.controlsArea.addControl(this.pointsToolTip);
            this.faithPointsLabel.Position = new Point(0x107, 0x40);
            this.faithPointsLabel.Color = ARGBColors.White;
            this.faithPointsLabel.Size = new Size(0x39, 0x12);
            this.faithPointsLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.faithPointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.faithPointsLabel.CustomTooltipID = 6;
            this.controlsArea.addControl(this.faithPointsLabel);
            this.faithpointsToolTip.Position = new Point(0xdf, 0x40);
            this.faithpointsToolTip.Size = new Size(40, 0x12);
            this.faithpointsToolTip.CustomTooltipID = 6;
            this.controlsArea.addControl(this.faithpointsToolTip);
            this.cardsButton.Position = new Point(0x162, 0);
            this.cardsButton.CustomTooltipID = 1;
            this.cardsButton.ClickArea = new Rectangle(0x25, 0, 0x88, 0x51);
            this.cardsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsClick));
            this.controlsArea.addControl(this.cardsButton);
            this.gameDateLabel.Text = "";
            this.gameDateLabel.Position = new Point(6, 4);
            this.gameDateLabel.Size = new Size(0xa2, 0x12);
            this.gameDateLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.gameDateLabel.Color = ARGBColors.Black;
            if ((GameEngine.Instance.LocalWorldData != null) && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1))
            {
                this.gameDateLabel.CustomTooltipID = 11;
            }
            else
            {
                this.gameDateLabel.CustomTooltipID = 0;
            }
            this.gameDateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.cardsButton.addControl(this.gameDateLabel);
            this.resize();
            this.contextTabBar.Position = new Point(0, 0x58);
            this.contextTabBar.Size = new Size(530, 0x20);
            this.contextTabBar.Visible = true;
            this.controlsArea.addControl(this.contextTabBar);
            this.villageInfoBar.init();
            this.villageInfoBar.Position = new Point(0, 0);
            this.villageInfoBar.Size = new Size(530, 0x20);
            this.villageInfoBar.Visible = false;
            this.contextTabBar.addControl(this.villageInfoBar);
            this.castleInfoBar.init();
            this.castleInfoBar.Position = new Point(0, 0);
            this.castleInfoBar.Size = new Size(530, 0x20);
            this.castleInfoBar.Visible = false;
            this.contextTabBar.addControl(this.castleInfoBar);
            InterfaceMgr.Instance.setVillageInfoBar(this.villageInfoBar, this.castleInfoBar);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(TopLeftMenu2));
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "TopLeftMenu2";
            base.Size = new Size(0x20f, 120);
            base.ResumeLayout(false);
        }

        private void onClickHonour()
        {
            InterfaceMgr.Instance.getMainTabBar().tabPage5_Enter();
        }

        private void onClickSettings()
        {
            InterfaceMgr.Instance.openGreyOutWindow(false);
        }

        public void resize()
        {
            this.cardsButton.Position = new Point(base.Width - this.cardsButton.Width, 0);
            int width = (base.Width - this.mainBackgroundImage.Image.Width) - 0xac;
            if (width < 1)
            {
                width = 1;
            }
            this.panelConnectorImage.Size = new Size(width, this.panelConnectorImage.Image.Size.Height);
            this.updateSecondAgeImage();
            this.controlsArea.Size = base.Size;
            this.cardsButton.invalidate();
        }

        private void secondAgeImage_Click()
        {
            if ((GameEngine.Instance.LocalWorldData != null) && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1))
            {
                GameEngine.Instance.openLostVillage(10);
            }
            else if (GameEngine.Instance.World.FifthAgeWorld)
            {
                GameEngine.Instance.openLostVillage(5);
            }
            else if (GameEngine.Instance.World.FourthAgeWorld)
            {
                GameEngine.Instance.openLostVillage(4);
            }
            else if (GameEngine.Instance.World.ThirdAgeWorld)
            {
                GameEngine.Instance.openLostVillage(3);
            }
            else
            {
                GameEngine.Instance.openLostVillage(2);
            }
        }

        public void setCards(CardData cardData)
        {
            bool flag = false;
            if (cardData.premiumCard != 0)
            {
                flag = true;
            }
            if (flag)
            {
                this.cardsButton.ImageNorm = (Image) GFXLibrary.menubar_middle_gold;
                this.cardsButton.ImageOver = (Image) GFXLibrary.menubar_middle_gold_over;
                this.cardsButton.ImageClick = (Image) GFXLibrary.menubar_middle_gold_over;
            }
            else
            {
                this.cardsButton.ImageNorm = (Image) GFXLibrary.menubar_middle;
                this.cardsButton.ImageOver = (Image) GFXLibrary.menubar_middle_over;
                this.cardsButton.ImageClick = (Image) GFXLibrary.menubar_middle_over;
            }
        }

        public void setContextBarVisible(bool state)
        {
            this.contextTabBar.Visible = state;
        }

        public void SetFaithPoints(double points)
        {
            try
            {
                if (points > 2147483647.0)
                {
                    points = 2147483647.0;
                }
                NumberFormatInfo nFI = GameEngine.NFI;
                int num = (int) points;
                if ((num == 0) && ((GameEngine.Instance.World.UserResearchData == null) || (GameEngine.Instance.World.UserResearchData.Research_Theology == 0)))
                {
                    this.faithPointsLabel.Text = "";
                    this.mainBackgroundImage.Image = (Image) GFXLibrary.interface_bar_top_left_empty;
                    this.faithPointsLabel.Visible = false;
                }
                else
                {
                    this.faithPointsLabel.Text = num.ToString("N", nFI);
                    this.mainBackgroundImage.Image = (Image) GFXLibrary.menubar_left_faith;
                    this.faithPointsLabel.Visible = true;
                }
            }
            catch (Exception)
            {
            }
        }

        public void setGold(double newGold)
        {
            if (newGold > 9.2233720368547758E+18)
            {
                newGold = 9.2233720368547758E+18;
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            this.currentGoldLabel.Text = ((long) newGold).ToString("N", nFI);
        }

        public void setHonour(double newHonour, int rank)
        {
            if (newHonour > 9.2233720368547758E+18)
            {
                newHonour = 9.2233720368547758E+18;
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            this.currentHonourLabel.Text = ((long) newHonour).ToString("N", nFI);
        }

        public void setPoints(int points)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            this.pointsLabel.Text = points.ToString("N", nFI);
        }

        public void setRank(int rank)
        {
            this.rankLabel.Text = Rankings.getRankingName(rank, RemoteServices.Instance.UserAvatar.male) + " (" + ((rank + 1)).ToString() + ")";
        }

        public void setServerTime(string serverTime)
        {
            this.gameDateLabel.Text = serverTime;
        }

        public void setUserName(string userName)
        {
            this.userNameLabel.Text = userName;
        }

        public void update()
        {
        }

        private void updateSecondAgeImage()
        {
            if (GameEngine.Instance.World.SecondAgeWorld || ((GameEngine.Instance.LocalWorldData != null) && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)))
            {
                if ((GameEngine.Instance.LocalWorldData != null) && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1))
                {
                    this.secondAgeImage.Image = (Image) GFXLibrary.dominationWorldLogo;
                    this.secondAgeImage.CustomTooltipID = 10;
                }
                else if (GameEngine.Instance.World.FifthAgeWorld)
                {
                    this.secondAgeImage.Image = (Image) GFXLibrary.fifthAgeLogo;
                    this.secondAgeImage.CustomTooltipID = 13;
                }
                else if (GameEngine.Instance.World.FourthAgeWorld)
                {
                    this.secondAgeImage.Image = (Image) GFXLibrary.fourthAgeLogo;
                    this.secondAgeImage.CustomTooltipID = 12;
                }
                else if (GameEngine.Instance.World.ThirdAgeWorld)
                {
                    this.secondAgeImage.Image = (Image) GFXLibrary.thirdAgeLogo;
                    this.secondAgeImage.CustomTooltipID = 9;
                }
                else if (GameEngine.Instance.World.SecondAgeWorld)
                {
                    this.secondAgeImage.Image = (Image) GFXLibrary.secondAgeLogo;
                    this.secondAgeImage.CustomTooltipID = 8;
                }
                int num = this.getCardAreaXPos();
                if (num > 0x1eb)
                {
                    this.secondAgeImage.Size = new Size(0x89, 0x48);
                    this.secondAgeImage.Position = new Point(((((num - 0x162) - 0x89) / 2) + 1) + 0x161, 9);
                    this.secondAgeImage.Visible = true;
                }
                else
                {
                    this.secondAgeImage.Visible = false;
                }
            }
            else
            {
                this.secondAgeImage.Visible = false;
            }
            this.secondAgeImage.invalidate();
            this.panelConnectorImage.invalidate();
        }
    }
}

