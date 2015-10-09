namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class HouseInfoPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage6 = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel data1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data1LabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data2LabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data3LabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data4LabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data5LabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data6Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data6LabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data7Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel data7LabelValue = new CustomSelfDrawPanel.CSDLabel();
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
        private CustomSelfDrawPanel.CSDLabel diplomacyLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton diplomacyNeutralButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage gloryImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDFill greyOverlay = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private int houseIDRef;
        private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel houseMottoLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel houseNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool houseVisitSent;
        private bool inHouseVote;
        public static HouseInfoPanel instance = null;
        private CustomSelfDrawPanel.CSDLabel lastVisitLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel leadershipVoteLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton leaveHouseButton = new CustomSelfDrawPanel.CSDButton();
        private List<HouseLine> lineList = new List<HouseLine>();
        private int m_houseLeaderFactionID = -1;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel membershipVoteLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDLabel nextProclamationLabel = new CustomSelfDrawPanel.CSDLabel();
        public const int PANEL_ID = 0x34;
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private MyMessageBoxPopUp PopUpRef;
        public static int SelectedHouse = -1;
        private CustomSelfDrawPanel.CSDButton sendProclamationButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public HouseInfoPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addDiplomacyOverlay()
        {
            this.removeOverlay();
            this.greyOverlay.Size = base.Size;
            this.greyOverlay.FillColor = Color.FromArgb(0x80, 0, 0, 0);
            this.greyOverlay.setClickDelegate(delegate {
            });
            this.mainBackgroundImage.addControl(this.greyOverlay);
            this.diplomacyHeaderImage.Size = new Size(400, 40);
            this.diplomacyHeaderImage.Position = new Point(((base.Width - 200) - 400) / 2, 100);
            this.greyOverlay.addControl(this.diplomacyHeaderImage);
            this.diplomacyHeaderImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            this.diplomacyBackgroundImage.Size = new Size(400, 300);
            this.diplomacyBackgroundImage.Position = new Point(((base.Width - 200) - 400) / 2, 140);
            this.greyOverlay.addControl(this.diplomacyBackgroundImage);
            this.diplomacyBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            this.diplomacyHeadingLabel.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
            this.diplomacyHeadingLabel.Color = ARGBColors.White;
            this.diplomacyHeadingLabel.Position = new Point(0, 0);
            this.diplomacyHeadingLabel.Size = this.diplomacyHeaderImage.Size;
            this.diplomacyHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.diplomacyHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.diplomacyHeaderImage.addControl(this.diplomacyHeadingLabel);
            this.diplomacyFactionLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + SelectedHouse.ToString();
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
            int num = GameEngine.Instance.World.getYourHouseRelation(SelectedHouse);
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
            this.diplomacyNeutralButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBreakAlliance_Click), "HouseInfoPanel_neutral");
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
            this.diplomacyAllyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAlly_Click), "HouseInfoPanel_ally");
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
            this.diplomacyEnemyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnMakeEnemy_Click), "HouseInfoPanel_enemy");
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
            this.diplomacyCancelButton.setClickDelegate(() => this.removeOverlay(), "HouseInfoPanel_cancel");
            this.diplomacyBackgroundImage.addControl(this.diplomacyCancelButton);
            this.diplomacyEnemyButton.Enabled = true;
            this.diplomacyAllyButton.Enabled = true;
            this.diplomacyNeutralButton.Enabled = true;
        }

        public void addFactions()
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            FactionData yourFaction = GameEngine.Instance.World.YourFaction;
            int houseID = 0;
            int num3 = 0;
            int houseLeaderVote = -1;
            if (yourFaction != null)
            {
                houseID = yourFaction.houseID;
                houseLeaderVote = yourFaction.houseLeaderVote;
            }
            if ((houseID != 0) && (houseID == SelectedHouse))
            {
                if ((GameEngine.Instance.World.HouseVoteInfo != null) && (GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID > 0))
                {
                    int appliedToHouseID = GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID;
                }
                num3 = GameEngine.Instance.World.getYourFactionRank();
            }
            int position = 0;
            FactionData[] dataArray = GameEngine.Instance.World.getHouseFactions(SelectedHouse);
            HouseVoteData houseVoteInfo = GameEngine.Instance.World.HouseVoteInfo;
            if (houseLeaderVote < 0)
            {
                houseLeaderVote = this.m_houseLeaderFactionID;
            }
            bool flag = false;
            foreach (FactionData data3 in dataArray)
            {
                if (houseLeaderVote == data3.factionID)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                houseLeaderVote = this.m_houseLeaderFactionID;
            }
            foreach (FactionData data4 in dataArray)
            {
                if (!data4.active || (data4.numMembers == 0))
                {
                    continue;
                }
                HouseLine control = new HouseLine();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(data4, position, this);
                if (((houseVoteInfo != null) && ((num3 == 1) || (num3 == 2))) && (houseID == SelectedHouse))
                {
                    bool vote = false;
                    if (houseVoteInfo.contains(data4.factionID, ref vote))
                    {
                        int numPos = 0;
                        int numNeg = 0;
                        if (houseVoteInfo.voteTotals != null)
                        {
                            for (int i = 0; i < (houseVoteInfo.voteTotals.Length / 3); i++)
                            {
                                if (houseVoteInfo.voteTotals[i, 0] == data4.factionID)
                                {
                                    numPos = houseVoteInfo.voteTotals[i, 1];
                                    numNeg = houseVoteInfo.voteTotals[i, 2];
                                    break;
                                }
                            }
                        }
                        control.extendVote(vote, numPos, numNeg, num3 == 1);
                    }
                }
                if (((num3 == 1) || (num3 == 2)) && (houseID == SelectedHouse))
                {
                    bool flag3 = false;
                    if (houseLeaderVote == data4.factionID)
                    {
                        flag3 = true;
                    }
                    control.extendLeader(flag3, num3 == 1);
                }
                this.wallScrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
                position++;
            }
            if ((((houseID == SelectedHouse) && (houseVoteInfo != null)) && ((houseVoteInfo.applications != null) && (houseVoteInfo.applications.Length > 0))) && ((num3 == 1) || (num3 == 2)))
            {
                HouseLine line2 = new HouseLine {
                    Position = new Point(0, y)
                };
                line2.applicationLine();
                this.wallScrollArea.addControl(line2);
                y += line2.Height;
                this.lineList.Add(line2);
                position++;
                foreach (int num9 in houseVoteInfo.applications)
                {
                    FactionData factionData = GameEngine.Instance.World.getFaction(num9);
                    bool flag4 = false;
                    if ((factionData != null) && houseVoteInfo.contains(factionData.factionID, ref flag4))
                    {
                        int num10 = 0;
                        int num11 = 0;
                        if (houseVoteInfo.voteTotals != null)
                        {
                            for (int j = 0; j < (houseVoteInfo.voteTotals.Length / 3); j++)
                            {
                                if (houseVoteInfo.voteTotals[j, 0] == factionData.factionID)
                                {
                                    num10 = houseVoteInfo.voteTotals[j, 1];
                                    num11 = houseVoteInfo.voteTotals[j, 2];
                                    break;
                                }
                            }
                        }
                        line2 = new HouseLine();
                        if (y != 0)
                        {
                            y += 5;
                        }
                        line2.Position = new Point(0, y);
                        line2.init(factionData, position, this);
                        line2.extendVote(flag4, num10, num11, num3 == 1);
                        line2.setAsApplication();
                        this.wallScrollArea.addControl(line2);
                        y += line2.Height;
                        this.lineList.Add(line2);
                        position++;
                    }
                }
            }
            this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, y);
            if (y < this.wallScrollBar.Height)
            {
                this.wallScrollBar.Visible = false;
            }
            else
            {
                this.wallScrollBar.Visible = true;
                this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
                this.wallScrollBar.Max = y - this.wallScrollBar.Height;
            }
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
            this.updateRelationshipText();
            this.update();
            base.Invalidate();
        }

        private void btnAlly_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            RemoteServices.Instance.set_CreateHouseRelationship_UserCallBack(new RemoteServices.CreateHouseRelationship_UserCallBack(this.createHouseRelationshipCallback));
            RemoteServices.Instance.CreateHouseRelationship(SelectedHouse, 1);
        }

        private void btnBreakAlliance_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            RemoteServices.Instance.set_CreateHouseRelationship_UserCallBack(new RemoteServices.CreateHouseRelationship_UserCallBack(this.createHouseRelationshipCallback));
            RemoteServices.Instance.CreateHouseRelationship(SelectedHouse, 0);
        }

        private void btnMakeEnemy_Click()
        {
            this.diplomacyEnemyButton.Enabled = false;
            this.diplomacyAllyButton.Enabled = false;
            this.diplomacyNeutralButton.Enabled = false;
            RemoteServices.Instance.set_CreateHouseRelationship_UserCallBack(new RemoteServices.CreateHouseRelationship_UserCallBack(this.createHouseRelationshipCallback));
            RemoteServices.Instance.CreateHouseRelationship(SelectedHouse, -1);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
            InterfaceMgr.Instance.closeDonatePopup();
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        private void createHouseRelationshipCallback(CreateHouseRelationship_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.HouseAllies = returnData.yourHouseAllies;
                GameEngine.Instance.World.HouseEnemies = returnData.yourHouseEnemies;
                this.init(false);
            }
            else
            {
                this.diplomacyEnemyButton.Enabled = true;
                this.diplomacyAllyButton.Enabled = true;
                this.diplomacyNeutralButton.Enabled = true;
            }
        }

        private void diplomacyClicked()
        {
            this.addDiplomacyOverlay();
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

        public void GetHouseGloryPointsCallBack(GetHouseGloryPoints_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.HouseGloryPoints = returnData.gloryPoints;
                GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
                this.init(false);
            }
        }

        public void houseVote(int targetFaction, bool application, bool vote)
        {
            RemoteServices.Instance.set_HouseVote_UserCallBack(new RemoteServices.HouseVote_UserCallBack(this.houseVoteCallback));
            RemoteServices.Instance.HouseVote(targetFaction, application, vote, GameEngine.Instance.World.StoredFactionChangesPos);
        }

        public void houseVoteCallback(HouseVote_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.factionsList != null)
                {
                    GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
                }
                GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
                GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
            }
            this.init(false);
        }

        public void houseVoteHouseLeader(int targetFaction)
        {
            if (!this.inHouseVote)
            {
                int houseID = 0;
                FactionData yourFaction = GameEngine.Instance.World.YourFaction;
                if (yourFaction != null)
                {
                    houseID = yourFaction.houseID;
                }
                this.inHouseVote = true;
                RemoteServices.Instance.set_HouseVoteHouseLeader_UserCallBack(new RemoteServices.HouseVoteHouseLeader_UserCallBack(this.houseVoteHouseLeaderCallback));
                RemoteServices.Instance.HouseVoteHouseLeader(RemoteServices.Instance.UserFactionID, houseID, targetFaction, GameEngine.Instance.World.StoredFactionChangesPos);
            }
        }

        public void houseVoteHouseLeaderCallback(HouseVoteHouseLeader_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.factionsList != null)
                {
                    GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
                }
                GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
                GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
            }
            this.inHouseVote = false;
            this.init(false);
        }

        public void init(bool resized)
        {
            int height = base.Height;
            NumberFormatInfo nFI = GameEngine.NFI;
            instance = this;
            this.inHouseVote = false;
            base.clearControls();
            if (GameEngine.Instance.World.testGloryPointsUpdate())
            {
                RemoteServices.Instance.set_GetHouseGloryPoints_UserCallBack(new RemoteServices.GetHouseGloryPoints_UserCallBack(this.GetHouseGloryPointsCallBack));
                RemoteServices.Instance.GetHouseGloryPoints();
            }
            this.sidebar.addSideBar(8, this);
            HouseData data = null;
            try
            {
                data = GameEngine.Instance.World.HouseInfo[SelectedHouse];
                this.m_houseLeaderFactionID = data.leadingFactionID;
            }
            catch (Exception)
            {
                data = new HouseData();
                this.m_houseLeaderFactionID = -1;
            }
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.backImage1.Image = (Image) GFXLibrary.faction_tanback;
            this.backImage1.Position = new Point((this.mainBackgroundImage.Size.Width - this.backImage1.Size.Width) - 0x19, 12);
            this.mainBackgroundImage.addControl(this.backImage1);
            this.backImage2.Image = (Image) GFXLibrary.faction_title_band;
            this.backImage2.Position = new Point(20, 20);
            this.mainBackgroundImage.addControl(this.backImage2);
            this.barImage1.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
            this.barImage1.Position = new Point(0xc9, 70);
            this.mainBackgroundImage.addControl(this.barImage1);
            this.barImage2.Image = (Image) GFXLibrary.faction_bar_tan_1_lighter;
            this.barImage2.Position = new Point(0xc9, 0x5e);
            this.mainBackgroundImage.addControl(this.barImage2);
            this.barImage3.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
            this.barImage3.Position = new Point(0xc9, 0x76);
            this.mainBackgroundImage.addControl(this.barImage3);
            this.barImage4.Image = (Image) GFXLibrary.faction_bar_tan_2_heavier;
            this.barImage4.Position = new Point(460, 70);
            this.mainBackgroundImage.addControl(this.barImage4);
            this.barImage5.Image = (Image) GFXLibrary.faction_bar_tan_2_lighter;
            this.barImage5.Position = new Point(460, 0x5e);
            this.mainBackgroundImage.addControl(this.barImage5);
            this.barImage6.Image = (Image) GFXLibrary.faction_bar_tan_2_heavier;
            this.barImage6.Position = new Point(460, 0x76);
            this.mainBackgroundImage.addControl(this.barImage6);
            this.houseNameLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + SelectedHouse.ToString();
            this.houseNameLabel.Color = ARGBColors.Black;
            this.houseNameLabel.Position = new Point(0xcd, 10);
            this.houseNameLabel.Size = new Size(600, 40);
            this.houseNameLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Regular);
            this.houseNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.houseNameLabel);
            this.houseMottoLabel.Text = "\"" + CustomTooltipManager.getHouseMotto(SelectedHouse) + "\"";
            this.houseMottoLabel.Color = ARGBColors.Black;
            this.houseMottoLabel.Position = new Point(0xcd, 0x29);
            this.houseMottoLabel.Size = new Size(600, 40);
            this.houseMottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.houseMottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.houseMottoLabel);
            this.houseImage.Image = (Image) GFXLibrary.house_circles_large[SelectedHouse - 1];
            this.houseImage.Position = new Point(0x20, 0x18);
            this.mainBackgroundImage.addControl(this.houseImage);
            this.data1Label.Text = SK.Text("GENERIC_Factions", "Factions");
            this.data1Label.Color = ARGBColors.Black;
            this.data1Label.Position = new Point(210, 0x49);
            this.data1Label.Size = new Size(600, 40);
            this.data1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.data1Label);
            if (SelectedHouse == 0)
            {
                this.data1LabelValue.Text = GameEngine.Instance.World.countHouseMembers(SelectedHouse).ToString("N", nFI);
            }
            else
            {
                this.data1LabelValue.Text = data.numFactions.ToString("N", nFI);
            }
            this.data1LabelValue.Color = ARGBColors.Black;
            this.data1LabelValue.Position = new Point(200, 0x49);
            this.data1LabelValue.Size = new Size(230, 40);
            this.data1LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data1LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.data1LabelValue);
            this.data2Label.Text = SK.Text("FactionInvites_Total_Points", "Total Points");
            this.data2Label.Color = ARGBColors.Black;
            this.data2Label.Position = new Point(210, 0x61);
            this.data2Label.Size = new Size(600, 40);
            this.data2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.data2Label);
            this.data2LabelValue.Text = data.points.ToString("N", nFI);
            this.data2LabelValue.Color = ARGBColors.Black;
            this.data2LabelValue.Position = new Point(200, 0x61);
            this.data2LabelValue.Size = new Size(230, 40);
            this.data2LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data2LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.data2LabelValue);
            this.data3Label.Text = SK.Text("FactionInvites_Members", "Members");
            this.data3Label.Color = ARGBColors.Black;
            this.data3Label.Position = new Point(210, 0x79);
            this.data3Label.Size = new Size(600, 40);
            this.data3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.data3Label);
            this.data3LabelValue.Text = GameEngine.Instance.World.getNumHouseMembers(SelectedHouse).ToString("N", nFI);
            this.data3LabelValue.Color = ARGBColors.Black;
            this.data3LabelValue.Position = new Point(200, 0x79);
            this.data3LabelValue.Size = new Size(230, 40);
            this.data3LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data3LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.data3LabelValue);
            this.data4Label.Text = SK.Text("FactionInvites_Marshall", "Marshall");
            this.data4Label.Color = ARGBColors.Black;
            this.data4Label.Position = new Point(0x1d3, 0x49);
            this.data4Label.Size = new Size(600, 40);
            this.data4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.data4Label);
            this.data4LabelValue.Text = data.leaderUserName;
            this.data4LabelValue.Color = ARGBColors.Black;
            this.data4LabelValue.Position = new Point(0x205, 0x49);
            this.data4LabelValue.Size = new Size(230, 40);
            this.data4LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data4LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.data4LabelValue);
            this.data5Label.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
            this.data5Label.Color = ARGBColors.Black;
            this.data5Label.Position = new Point(0x1d3, 0x61);
            this.data5Label.Size = new Size(600, 40);
            this.data5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.data5Label);
            this.data5LabelValue.Text = "";
            if (data.leadingFactionID >= 0)
            {
                FactionData data2 = GameEngine.Instance.World.getFaction(data.leadingFactionID);
                if (data2 != null)
                {
                    this.data5LabelValue.Text = data2.factionName;
                }
            }
            this.data5LabelValue.Color = ARGBColors.Black;
            this.data5LabelValue.Position = new Point(0x205, 0x4b);
            this.data5LabelValue.Size = new Size(230, 60);
            this.data5LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data5LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.mainBackgroundImage.addControl(this.data5LabelValue);
            int num2 = GameEngine.Instance.World.getGloryRank(SelectedHouse);
            if (num2 >= 0)
            {
                this.gloryImage.Image = (Image) GFXLibrary.glory_frame;
                this.gloryImage.Position = new Point(490, 10);
                this.mainBackgroundImage.addControl(this.gloryImage);
                this.data6Label.Text = SK.Text("FactionInvites_Glory_Rank", "Glory Rank");
                this.data6Label.Color = ARGBColors.Black;
                this.data6Label.Position = new Point(0x1f9, 0x1b);
                this.data6Label.Size = new Size(600, 40);
                this.data6Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                this.data6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.mainBackgroundImage.addControl(this.data6Label);
                this.data6LabelValue.Text = (num2 + 1).ToString("N", nFI);
                this.data6LabelValue.Color = ARGBColors.Black;
                this.data6LabelValue.Position = new Point(0x2b6, 0x1b);
                this.data6LabelValue.Size = new Size(0x1d, 40);
                this.data6LabelValue.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                this.data6LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.mainBackgroundImage.addControl(this.data6LabelValue);
            }
            this.data7Label.Text = SK.Text("FactionInvites_Glory Victories", "Glory Victories");
            this.data7Label.Color = ARGBColors.Black;
            this.data7Label.Position = new Point(0x1d3, 0x79);
            this.data7Label.Size = new Size(600, 40);
            this.data7Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data7Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.data7Label);
            this.data7LabelValue.Text = data.numVictories.ToString("N", nFI);
            this.data7LabelValue.Color = ARGBColors.Black;
            this.data7LabelValue.Position = new Point(0x205, 0x79);
            this.data7LabelValue.Size = new Size(230, 40);
            this.data7LabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.data7LabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.data7LabelValue);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 0x9f);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(290, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(440, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.factionLabel.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
            this.factionLabel.Color = ARGBColors.Black;
            this.factionLabel.Position = new Point(9, -2);
            this.factionLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.factionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.factionLabel);
            this.pointsLabel.Text = SK.Text("FactionsPanel_Points", "Points");
            this.pointsLabel.Color = ARGBColors.Black;
            this.pointsLabel.Position = new Point(0x127, -2);
            this.pointsLabel.Size = new Size(140, this.headerLabelsImage.Height);
            this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.pointsLabel);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + SelectedHouse.ToString());
            FactionData yourFaction = GameEngine.Instance.World.YourFaction;
            int index = 0;
            if (yourFaction != null)
            {
                index = yourFaction.houseID;
            }
            bool flag = false;
            int num4 = GameEngine.Instance.World.getYourFactionRank();
            if ((index != 0) && (index == SelectedHouse))
            {
                this.leaderVisited();
                if ((GameEngine.Instance.World.HouseVoteInfo != null) && (GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID > 0))
                {
                    int appliedToHouseID = GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID;
                }
                switch (num4)
                {
                    case 1:
                        this.leaveHouseButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                        this.leaveHouseButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                        this.leaveHouseButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                        this.leaveHouseButton.Position = new Point(560, height - 30);
                        this.leaveHouseButton.Text.Text = SK.Text("FactionsPanel_Leave_House", "Leave House");
                        this.leaveHouseButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                        this.leaveHouseButton.TextYOffset = -3;
                        this.leaveHouseButton.Text.Color = ARGBColors.Black;
                        this.leaveHouseButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaveHouseClick), "HouseInfoPanel_leave");
                        this.mainBackgroundImage.addControl(this.leaveHouseButton);
                        flag = true;
                        break;
                    // TODO: case 1 должен включать case 2
                    // case 1:
                    case 2:
                    {
                        string str = "";
                        TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - yourFaction.lastHouseDate);
                        if ((span.TotalDays < 1.0) && (VillageMap.getCurrentServerTime().Day == yourFaction.lastHouseDate.Day))
                        {
                            str = SK.Text("FactionsPanel_Today", "Today");
                        }
                        else if ((span.TotalDays < 2.0) && (VillageMap.getCurrentServerTime().Day == yourFaction.lastHouseDate.AddDays(1.0).Day))
                        {
                            str = SK.Text("FactionsPanel_Yesterday", "Yesterday");
                        }
                        else
                        {
                            int totalDays = (int) span.TotalDays;
                            if (totalDays < 2)
                            {
                                totalDays = 2;
                            }
                            str = totalDays.ToString() + " " + SK.Text("FactionsPanel_X_Days_Ago", "Days ago");
                        }
                        this.lastVisitLabel.Text = SK.Text("FactionsPanel_Last_General_Visit", "Last General Visit") + " : " + str;
                        this.lastVisitLabel.Color = ARGBColors.Black;
                        this.lastVisitLabel.Position = new Point(10, height - 0x19);
                        this.lastVisitLabel.Size = new Size(400, 40);
                        this.lastVisitLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                        this.lastVisitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                        this.mainBackgroundImage.addControl(this.lastVisitLabel);
                        flag = true;
                        break;
                    }
                }
                this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider3Image.Position = new Point(610, 0);
                this.headerLabelsImage.addControl(this.divider3Image);
                this.membershipVoteLabel.Text = SK.Text("FactionsPanel_Membership_Vote", "Membership Vote");
                this.membershipVoteLabel.Color = ARGBColors.Black;
                this.membershipVoteLabel.Position = new Point(0x1bd, -2);
                this.membershipVoteLabel.Size = new Size(160, this.headerLabelsImage.Height);
                this.membershipVoteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.membershipVoteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.headerLabelsImage.addControl(this.membershipVoteLabel);
                this.leadershipVoteLabel.Text = SK.Text("FactionsPanel_Leadership_Vote", "Leadership Vote");
                this.leadershipVoteLabel.Color = ARGBColors.Black;
                this.leadershipVoteLabel.Position = new Point(0x253, -2);
                this.leadershipVoteLabel.Size = new Size(160, this.headerLabelsImage.Height);
                this.leadershipVoteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.leadershipVoteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.headerLabelsImage.addControl(this.leadershipVoteLabel);
                if ((GameEngine.Instance.World.getYourHouseRank() == 10) && (num4 == 1))
                {
                    this.sendProclamationButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                    this.sendProclamationButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                    this.sendProclamationButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                    this.sendProclamationButton.Position = new Point(330, height - 30);
                    this.sendProclamationButton.Text.Text = SK.Text("Capitials_Proclamation", "Send Proclamation");
                    this.sendProclamationButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.sendProclamationButton.TextYOffset = -3;
                    this.sendProclamationButton.Text.Color = ARGBColors.Black;
                    this.sendProclamationButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendProclamation), "HouseInfoPanel_sendProclamation");
                    this.mainBackgroundImage.addControl(this.sendProclamationButton);
                    DateTime lastProclomationDate = GameEngine.Instance.World.HouseInfo[index].lastProclomationDate;
                    TimeSpan span2 = (TimeSpan) (VillageMap.getCurrentServerTime() - lastProclomationDate);
                    this.nextProclamationLabel.Text = SK.Text("FactionsPanel_Next_Proclamation_Time", "Next proclamation available in") + " : ";
                    this.nextProclamationLabel.Color = ARGBColors.White;
                    this.nextProclamationLabel.Position = new Point(330, height - 0x20);
                    this.nextProclamationLabel.Size = new Size(160, this.headerLabelsImage.Height);
                    this.nextProclamationLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.nextProclamationLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.mainBackgroundImage.addControl(this.nextProclamationLabel);
                    if (span2.TotalDays >= 1.0)
                    {
                        this.sendProclamationButton.Enabled = true;
                        this.sendProclamationButton.Visible = true;
                        this.nextProclamationLabel.Visible = false;
                    }
                    else
                    {
                        TimeSpan span3 = TimeSpan.FromDays(1.0) - span2;
                        this.sendProclamationButton.Enabled = false;
                        this.sendProclamationButton.Visible = false;
                        this.nextProclamationLabel.Text = this.nextProclamationLabel.Text + TimeSpan.FromSeconds((double) ((((span3.Hours * 60) * 60) + (span3.Minutes * 60)) + span3.Seconds)).ToString();
                        this.nextProclamationLabel.Visible = true;
                    }
                }
            }
            else if (index > 0)
            {
                bool flag2 = false;
                if ((num4 == 1) && (GameEngine.Instance.World.getYourHouseRank() == 10))
                {
                    flag2 = true;
                }
                if (flag2)
                {
                    this.diplomacyButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                    this.diplomacyButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                    this.diplomacyButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                    this.diplomacyButton.Position = new Point(0x22f, height - 30);
                    this.diplomacyButton.Text.Text = "";
                    this.diplomacyButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.diplomacyButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.diplomacyButton.TextYOffset = -3;
                    this.diplomacyButton.Text.Color = ARGBColors.Black;
                    this.diplomacyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClicked), "HouseInfoPanel_diplomacy");
                    this.mainBackgroundImage.addControl(this.diplomacyButton);
                    flag = true;
                }
                else
                {
                    this.diplomacyLabel.Text = "";
                    this.diplomacyLabel.Color = ARGBColors.Black;
                    this.diplomacyLabel.Position = new Point(520, height - 0x19);
                    this.diplomacyLabel.Size = new Size(240, 40);
                    this.diplomacyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                    this.diplomacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                    this.mainBackgroundImage.addControl(this.diplomacyLabel);
                    flag = true;
                }
            }
            this.wallScrollArea.Position = new Point(0x19, 0xbc);
            if (flag)
            {
                this.wallScrollArea.Size = new Size(0x2c1, height - 240);
                this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, height - 240));
            }
            else
            {
                this.wallScrollArea.Size = new Size(0x2c1, height - 0xbc);
                this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, height - 0xbc));
            }
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num11 = this.wallScrollBar.Value;
            this.wallScrollBar.Position = new Point(0x2dd, 0xbc);
            if (flag)
            {
                this.wallScrollBar.Size = new Size(0x18, height - 240);
            }
            else
            {
                this.wallScrollBar.Size = new Size(0x18, height - 0xbc);
            }
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.addFactions();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "HouseInfoPanel";
            base.Size = new Size(0x3e0, 0x236);
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

        private void leaderVisited()
        {
            if ((GameEngine.Instance.World.getYourFactionRank() == 1) && !this.houseVisitSent)
            {
                this.houseVisitSent = true;
                RemoteServices.Instance.TouchHouseVisitDate(RemoteServices.Instance.UserFactionID);
            }
        }

        public void leaveHouse(int houseID)
        {
            this.houseIDRef = houseID;
            if (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FactionsPanel_Leave_House", "Leave House"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.LeaveHouseTrue();
            }
        }

        public void leaveHouseCallback(LeaveHouse_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.factionsList != null)
                {
                    GameEngine.Instance.World.processFactionsList(returnData.factionsList, returnData.currentFactionChangePos);
                }
                GameEngine.Instance.World.HouseInfo = returnData.m_houseData;
                GameEngine.Instance.World.HouseVoteInfo = returnData.m_houseVoteData;
                GameEngine.Instance.World.YourFaction = returnData.yourFaction;
            }
            this.init(false);
        }

        private void leaveHouseClick()
        {
            if (GameEngine.Instance.World.YourFaction != null)
            {
                int houseID = GameEngine.Instance.World.YourFaction.houseID;
                if (houseID > 0)
                {
                    this.leaveHouse(houseID);
                }
            }
        }

        private void LeaveHouseTrue()
        {
            RemoteServices.Instance.set_LeaveHouse_UserCallBack(new RemoteServices.LeaveHouse_UserCallBack(this.leaveHouseCallback));
            RemoteServices.Instance.LeaveHouse(RemoteServices.Instance.UserFactionID, this.houseIDRef, GameEngine.Instance.World.StoredFactionChangesPos);
        }

        public void logout()
        {
            this.houseVisitSent = false;
        }

        private void mouseWheelMoved(int delta)
        {
            if (this.wallScrollBar.Visible)
            {
                if (delta < 0)
                {
                    this.wallScrollBar.scrollDown(40);
                }
                else if (delta > 0)
                {
                    this.wallScrollBar.scrollUp(40);
                }
            }
        }

        public void removeOverlay()
        {
            this.mainBackgroundImage.removeControl(this.greyOverlay);
            this.greyOverlay.clearControls();
            base.Invalidate();
        }

        private void sendProclamation()
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
            InterfaceMgr.Instance.getMainTabBar().selectDummyTabFast(0x15);
            InterfaceMgr.Instance.sendProclamation(2, GameEngine.Instance.World.YourHouse);
        }

        public void update()
        {
            this.sidebar.update();
        }

        public void updateRelationshipText()
        {
            int num = GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID);
            if (((RemoteServices.Instance.UserFactionID < 0) || (num == 0)) || (num == SelectedHouse))
            {
                this.diplomacyButton.Visible = false;
                this.diplomacyLabel.Visible = false;
            }
            else
            {
                string str = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy") + " : ";
                this.diplomacyButton.Visible = true;
                this.diplomacyLabel.Visible = true;
                int num2 = 0;
                if (SelectedHouse >= 0)
                {
                    num2 = GameEngine.Instance.World.getYourHouseRelation(SelectedHouse);
                }
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
                this.diplomacyLabel.Text = str;
                this.diplomacyButton.Text.Text = str;
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0xbc - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class HouseLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage allianceImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
            private CustomSelfDrawPanel.CSDButton leaderVote = new CustomSelfDrawPanel.CSDButton();
            private bool m_application;
            private FactionData m_factionData;
            private HouseInfoPanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton negVote = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDLabel newLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel numPlayersLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel posLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton posVote = new CustomSelfDrawPanel.CSDButton();

            public void applicationLine()
            {
                this.clearControls();
                this.factionName.Text = SK.Text("HouseFactionsLine_Application", "Applications");
                this.factionName.Color = ARGBColors.Black;
                this.factionName.Position = new Point(9, 2);
                this.factionName.Size = new Size(GFXLibrary.lineitem_strip_02_light.Width, GFXLibrary.lineitem_strip_02_light.Height);
                this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                base.addControl(this.factionName);
                this.Size = GFXLibrary.lineitem_strip_02_light.Size;
            }

            public void clickedLine()
            {
                GameEngine.Instance.playInterfaceSound("HouseInfoPanel_faction");
                InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
            }

            public void extendLeader(bool vote, bool leader)
            {
                if (!vote)
                {
                    this.leaderVote.ImageNorm = (Image) GFXLibrary.radio_green[2];
                    this.leaderVote.ImageOver = (Image) GFXLibrary.radio_green[1];
                    this.leaderVote.ImageClick = (Image) GFXLibrary.radio_green[1];
                    this.leaderVote.Position = new Point(0x256, 7);
                    if (leader)
                    {
                        this.leaderVote.Active = true;
                        this.leaderVote.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaderClicked), "HouseInfoPanel_");
                    }
                    else
                    {
                        this.leaderVote.Active = false;
                    }
                    this.backgroundImage.addControl(this.leaderVote);
                }
                else
                {
                    this.leaderVote.ImageNorm = (Image) GFXLibrary.radio_green[0];
                    this.leaderVote.ImageOver = (Image) GFXLibrary.radio_green[0];
                    this.leaderVote.ImageClick = (Image) GFXLibrary.radio_green[0];
                    this.leaderVote.Active = false;
                    this.leaderVote.Position = new Point(0x256, 7);
                    this.backgroundImage.addControl(this.leaderVote);
                }
            }

            public void extendVote(bool vote, int numPos, int numNeg, bool leader)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                this.posLabel.Text = numPos.ToString("N", nFI);
                this.posLabel.Color = ARGBColors.Black;
                this.posLabel.Position = new Point(0x149, 0);
                this.posLabel.Size = new Size(100, this.backgroundImage.Height);
                this.posLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.posLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.backgroundImage.addControl(this.posLabel);
                this.newLabel.Text = numNeg.ToString("N", nFI);
                this.newLabel.Color = ARGBColors.Black;
                this.newLabel.Position = new Point(0x185, 0);
                this.newLabel.Size = new Size(100, this.backgroundImage.Height);
                this.newLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.newLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.backgroundImage.addControl(this.newLabel);
                if (!vote)
                {
                    this.posVote.ImageNorm = (Image) GFXLibrary.radio_green[2];
                    this.posVote.ImageOver = (Image) GFXLibrary.radio_green[1];
                    this.posVote.ImageClick = (Image) GFXLibrary.radio_green[1];
                    this.posVote.Position = new Point(0x1b2, 7);
                    if (leader)
                    {
                        this.posVote.Active = true;
                        this.posVote.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.posVoteClicked), "HouseInfoPanel_");
                    }
                    else
                    {
                        this.posVote.Active = false;
                    }
                    this.backgroundImage.addControl(this.posVote);
                    this.negVote.ImageNorm = (Image) GFXLibrary.radio_green[0];
                    this.negVote.ImageOver = (Image) GFXLibrary.radio_green[0];
                    this.negVote.ImageClick = (Image) GFXLibrary.radio_green[0];
                    this.negVote.Active = false;
                    this.negVote.Position = new Point(0x1ee, 7);
                    this.backgroundImage.addControl(this.negVote);
                }
                else
                {
                    this.posVote.ImageNorm = (Image) GFXLibrary.radio_green[0];
                    this.posVote.ImageOver = (Image) GFXLibrary.radio_green[0];
                    this.posVote.ImageClick = (Image) GFXLibrary.radio_green[0];
                    this.posVote.Active = false;
                    this.posVote.Position = new Point(0x1b2, 7);
                    this.backgroundImage.addControl(this.posVote);
                    this.negVote.ImageNorm = (Image) GFXLibrary.radio_green[2];
                    this.negVote.ImageOver = (Image) GFXLibrary.radio_green[1];
                    this.negVote.ImageClick = (Image) GFXLibrary.radio_green[1];
                    this.negVote.Position = new Point(0x1ee, 7);
                    if (leader)
                    {
                        this.negVote.Active = true;
                        this.negVote.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.negVoteClicked), "HouseInfoPanel_");
                    }
                    else
                    {
                        this.negVote.Active = false;
                    }
                    this.backgroundImage.addControl(this.negVote);
                }
            }

            public void init(FactionData factionData, int position, HouseInfoPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_factionData = factionData;
                this.m_application = false;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(60, 0);
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.flagImage.createFromFlagData(factionData.flagData);
                this.flagImage.Position = new Point(0, 0);
                this.flagImage.Scale = 0.25;
                this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                base.addControl(this.flagImage);
                NumberFormatInfo nFI = GameEngine.NFI;
                this.factionName.Text = factionData.factionName;
                this.factionName.Color = ARGBColors.Black;
                this.factionName.Position = new Point(9, 0);
                this.factionName.Size = new Size(220, this.backgroundImage.Height);
                this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.factionName);
                this.pointsLabel.Text = factionData.points.ToString("N", nFI);
                this.pointsLabel.Color = ARGBColors.Black;
                this.pointsLabel.Position = new Point(0xeb, 0);
                this.pointsLabel.Size = new Size(100, this.backgroundImage.Height);
                this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.pointsLabel);
                base.invalidate();
            }

            public void leaderClicked()
            {
                this.m_parent.houseVoteHouseLeader(this.m_factionData.factionID);
            }

            public void negVoteClicked()
            {
                this.negVote.Active = false;
                this.m_parent.houseVote(this.m_factionData.factionID, this.m_application, false);
            }

            public void posVoteClicked()
            {
                this.posVote.Active = false;
                this.m_parent.houseVote(this.m_factionData.factionID, this.m_application, true);
            }

            public void setAsApplication()
            {
                this.m_application = true;
            }

            public void update()
            {
            }
        }
    }
}

