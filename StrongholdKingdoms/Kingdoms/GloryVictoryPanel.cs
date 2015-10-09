namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GloryVictoryPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel dayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eliminatedHouse1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eliminatedHouse2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eliminatedLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel factionNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel leadByLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel leaderNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lostStarsHouse1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lostStarsHouse2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lostStarsLabel = new CustomSelfDrawPanel.CSDLabel();
        private Form m_parent;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel ofLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage overlayImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel starsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel victoriousHouseLabel = new CustomSelfDrawPanel.CSDLabel();

        public GloryVictoryPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            this.m_parent.Close();
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
            InterfaceMgr.Instance.closeGloryVictoryWindowPopup();
            GloryRoundData houseGloryRoundData = GameEngine.Instance.World.HouseGloryRoundData;
            InterfaceMgr.Instance.showFactionPanel(houseGloryRoundData.factionID);
        }

        private void houseClicked()
        {
            InterfaceMgr.Instance.closeGloryVictoryWindowPopup();
            int data = base.ClickedControl.Data;
            InterfaceMgr.Instance.showHousePanel(data);
        }

        public void init(Form parent)
        {
            this.m_parent = parent;
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.mail2_mail_panel_middle_middle;
            this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), base.Size);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            base.addControl(this.mainBackgroundImage);
            this.overlayImage.Image = (Image) GFXLibrary.char_achievementOverlay;
            this.overlayImage.Position = new Point(0, 0);
            this.mainBackgroundImage.addControl(this.overlayImage);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(base.Width - 40, 0);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "GloryResultPanel_close");
            this.overlayImage.addControl(this.closeButton);
            int y = 0x37;
            this.headerLabel.Text = SK.Text("Glory_Glory_Victor", "Last Glory Round Result");
            this.headerLabel.Position = new Point(0, 0);
            this.headerLabel.Size = new Size(base.Width, 30);
            this.headerLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabel.Color = ARGBColors.White;
            this.headerLabel.RolloverColor = ARGBColors.Yellow;
            this.headerLabel.DropShadowColor = ARGBColors.Black;
            this.overlayImage.addControl(this.headerLabel);
            GloryRoundData houseGloryRoundData = GameEngine.Instance.World.HouseGloryRoundData;
            TimeSpan span = (TimeSpan) (houseGloryRoundData.victoryTime - GameEngine.Instance.World.m_worldStartDate);
            this.dayLabel.Text = SK.Text("MENU_Day_X", "Day") + " " + ((int) span.TotalDays).ToString();
            this.dayLabel.Position = new Point(0, 0x1c);
            this.dayLabel.Size = new Size(base.Width - 0x19, 30);
            this.dayLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.dayLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.dayLabel.Color = ARGBColors.White;
            this.dayLabel.RolloverColor = ARGBColors.Yellow;
            this.dayLabel.DropShadowColor = ARGBColors.Black;
            this.overlayImage.addControl(this.dayLabel);
            this.victoriousHouseLabel.Text = SK.Text("Glory_Victorious_House", "Victorious House") + " - " + houseGloryRoundData.winnerHouseID.ToString();
            this.victoriousHouseLabel.Position = new Point(0, y);
            this.victoriousHouseLabel.Size = new Size(base.Width, 20);
            this.victoriousHouseLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.victoriousHouseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.victoriousHouseLabel.Color = ARGBColors.White;
            this.victoriousHouseLabel.RolloverColor = ARGBColors.Yellow;
            this.victoriousHouseLabel.DropShadowColor = ARGBColors.Black;
            this.victoriousHouseLabel.Data = houseGloryRoundData.winnerHouseID;
            this.victoriousHouseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_winning_house");
            this.overlayImage.addControl(this.victoriousHouseLabel);
            this.leadByLabel.Text = SK.Text("Glory_Lead_By", "Lead By");
            this.leadByLabel.Position = new Point(0, (y + 20) - 2);
            this.leadByLabel.Size = new Size(base.Width, 20);
            this.leadByLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.leadByLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.leadByLabel.Color = ARGBColors.White;
            this.leadByLabel.DropShadowColor = ARGBColors.Black;
            this.overlayImage.addControl(this.leadByLabel);
            this.leaderNameLabel.Text = houseGloryRoundData.marshallName;
            this.leaderNameLabel.Position = new Point(0, y + 40);
            this.leaderNameLabel.Size = new Size(base.Width, 20);
            this.leaderNameLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.leaderNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.leaderNameLabel.Color = ARGBColors.White;
            this.leaderNameLabel.RolloverColor = ARGBColors.Yellow;
            this.leaderNameLabel.DropShadowColor = ARGBColors.Black;
            this.leaderNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClicked), "GloryResult_winning_player");
            this.overlayImage.addControl(this.leaderNameLabel);
            this.ofLabel.Text = SK.Text("Glory_Of", "Of");
            if (this.ofLabel.Text == "/")
            {
                this.ofLabel.Text = "";
            }
            this.ofLabel.Position = new Point(0, (y + 60) - 2);
            this.ofLabel.Size = new Size(base.Width, 20);
            this.ofLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.ofLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.ofLabel.Color = ARGBColors.White;
            this.ofLabel.DropShadowColor = ARGBColors.Black;
            this.overlayImage.addControl(this.ofLabel);
            this.factionNameLabel.Text = houseGloryRoundData.factionName;
            this.factionNameLabel.Position = new Point(0, y + 80);
            this.factionNameLabel.Size = new Size(base.Width, 20);
            this.factionNameLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.factionNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.factionNameLabel.Color = ARGBColors.White;
            this.factionNameLabel.RolloverColor = ARGBColors.Yellow;
            this.factionNameLabel.DropShadowColor = ARGBColors.Black;
            this.factionNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "GloryResult_winning_faction");
            this.overlayImage.addControl(this.factionNameLabel);
            this.starsLabel.Text = SK.Text("Glory_CurrentStars", "Current Stars") + " : " + houseGloryRoundData.numStars.ToString();
            this.starsLabel.Position = new Point(0, y + 120);
            this.starsLabel.Size = new Size(base.Width, 20);
            this.starsLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.starsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.starsLabel.Color = ARGBColors.White;
            this.starsLabel.DropShadowColor = ARGBColors.Black;
            this.overlayImage.addControl(this.starsLabel);
            int num3 = y + 160;
            if ((houseGloryRoundData.houseEliminated1 > 0) || (houseGloryRoundData.houseEliminated2 > 0))
            {
                this.eliminatedLabel.Text = SK.Text("Glory_Houses_Eliminated", "Houses Eliminated");
                this.eliminatedLabel.Position = new Point(0, num3);
                this.eliminatedLabel.Size = new Size(base.Width, 20);
                this.eliminatedLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                this.eliminatedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.eliminatedLabel.Color = ARGBColors.White;
                this.eliminatedLabel.DropShadowColor = ARGBColors.Black;
                this.overlayImage.addControl(this.eliminatedLabel);
                num3 += 0x19;
                if (houseGloryRoundData.houseEliminated1 > 0)
                {
                    this.eliminatedHouse1Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseEliminated1.ToString();
                    this.eliminatedHouse1Label.Position = new Point(0, num3);
                    this.eliminatedHouse1Label.Size = new Size(base.Width, 20);
                    this.eliminatedHouse1Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                    this.eliminatedHouse1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.eliminatedHouse1Label.Color = ARGBColors.White;
                    this.eliminatedHouse1Label.RolloverColor = ARGBColors.Yellow;
                    this.eliminatedHouse1Label.DropShadowColor = ARGBColors.Black;
                    this.eliminatedHouse1Label.Data = houseGloryRoundData.houseEliminated1;
                    this.eliminatedHouse1Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_eliminated_house");
                    this.overlayImage.addControl(this.eliminatedHouse1Label);
                    num3 += 20;
                }
                if (houseGloryRoundData.houseEliminated2 > 0)
                {
                    this.eliminatedHouse2Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseEliminated2.ToString();
                    this.eliminatedHouse2Label.Position = new Point(0, num3);
                    this.eliminatedHouse2Label.Size = new Size(base.Width, 20);
                    this.eliminatedHouse2Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                    this.eliminatedHouse2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.eliminatedHouse2Label.Color = ARGBColors.White;
                    this.eliminatedHouse2Label.RolloverColor = ARGBColors.Yellow;
                    this.eliminatedHouse2Label.DropShadowColor = ARGBColors.Black;
                    this.eliminatedHouse2Label.Data = houseGloryRoundData.houseEliminated2;
                    this.eliminatedHouse2Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_eliminated_house");
                    this.overlayImage.addControl(this.eliminatedHouse2Label);
                    num3 += 20;
                }
                num3 += 10;
            }
            if ((houseGloryRoundData.houseLostStar1 > 0) || (houseGloryRoundData.houseLostStar2 > 0))
            {
                this.lostStarsLabel.Text = SK.Text("Glory_Lost_a_Star", "Lost a Star");
                this.lostStarsLabel.Position = new Point(0, num3);
                this.lostStarsLabel.Size = new Size(base.Width, 20);
                this.lostStarsLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                this.lostStarsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lostStarsLabel.Color = ARGBColors.White;
                this.lostStarsLabel.DropShadowColor = ARGBColors.Black;
                this.overlayImage.addControl(this.lostStarsLabel);
                num3 += 0x19;
                if (houseGloryRoundData.houseLostStar1 > 0)
                {
                    this.lostStarsHouse1Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseLostStar1.ToString();
                    this.lostStarsHouse1Label.Position = new Point(0, num3);
                    this.lostStarsHouse1Label.Size = new Size(base.Width, 20);
                    this.lostStarsHouse1Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                    this.lostStarsHouse1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.lostStarsHouse1Label.Color = ARGBColors.White;
                    this.lostStarsHouse1Label.RolloverColor = ARGBColors.Yellow;
                    this.lostStarsHouse1Label.DropShadowColor = ARGBColors.Black;
                    this.lostStarsHouse1Label.Data = houseGloryRoundData.houseLostStar1;
                    this.lostStarsHouse1Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_house_losing_star");
                    this.overlayImage.addControl(this.lostStarsHouse1Label);
                    num3 += 20;
                }
                if (houseGloryRoundData.houseLostStar2 > 0)
                {
                    this.lostStarsHouse2Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseLostStar2.ToString();
                    this.lostStarsHouse2Label.Position = new Point(0, num3);
                    this.lostStarsHouse2Label.Size = new Size(base.Width, 20);
                    this.lostStarsHouse2Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                    this.lostStarsHouse2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.lostStarsHouse2Label.Color = ARGBColors.White;
                    this.lostStarsHouse2Label.RolloverColor = ARGBColors.Yellow;
                    this.lostStarsHouse2Label.DropShadowColor = ARGBColors.Black;
                    this.lostStarsHouse2Label.Data = houseGloryRoundData.houseLostStar2;
                    this.lostStarsHouse2Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_house_losing_star");
                    this.overlayImage.addControl(this.lostStarsHouse2Label);
                    num3 += 20;
                }
                num3 += 10;
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void playerClicked()
        {
            InterfaceMgr.Instance.closeGloryVictoryWindowPopup();
            InterfaceMgr.Instance.changeTab(0);
            GloryRoundData houseGloryRoundData = GameEngine.Instance.World.HouseGloryRoundData;
            WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                userID = houseGloryRoundData.marshallUserID
            };
            InterfaceMgr.Instance.showUserInfoScreen(userInfo);
        }
    }
}

