namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class HouseListPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton factionInfoButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea factionsSortArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton gloryInfoButton = new CustomSelfDrawPanel.CSDButton();
        private GloryNegComparer gloryNegComparer = new GloryNegComparer();
        private GloryPosComparer gloryPosComparer = new GloryPosComparer();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea houseSortArea = new CustomSelfDrawPanel.CSDArea();
        public static HouseListPanel instance;
        private List<HouseLine> lineList = new List<HouseLine>();
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private NameNegComparer nameNegComparer = new NameNegComparer();
        private NamePosComparer namePosComparer = new NamePosComparer();
        private int pageMode;
        public const int PANEL_ID = 0x33;
        private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
        private PlayersNegComparer playersNegComparer = new PlayersNegComparer();
        private PlayersPosComparer playersPosComparer = new PlayersPosComparer();
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private PointsNegComparer pointsNegComparer = new PointsNegComparer();
        private PointsPosComparer pointsPosComparer = new PointsPosComparer();
        private CustomSelfDrawPanel.CSDArea pointsSortArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private int sortMethod = -1;
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public HouseListPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addFactions()
        {
            int yourRank = GameEngine.Instance.World.getYourFactionRank();
            FactionData yourFaction = GameEngine.Instance.World.YourFaction;
            int yourHouseID = 0;
            if (yourFaction != null)
            {
                yourHouseID = yourFaction.houseID;
            }
            int appliedToHouse = 0;
            if ((GameEngine.Instance.World.HouseVoteInfo != null) && (GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID > 0))
            {
                appliedToHouse = GameEngine.Instance.World.HouseVoteInfo.appliedToHouseID;
            }
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            int position = 0;
            HouseData[] houseInfo = GameEngine.Instance.World.HouseInfo;
            List<HouseData> list = new List<HouseData>();
            foreach (HouseData data2 in houseInfo)
            {
                list.Add(data2);
            }
            switch (this.sortMethod)
            {
                case 0:
                    list.Sort(this.namePosComparer);
                    break;

                case 1:
                    list.Sort(this.nameNegComparer);
                    break;

                case 2:
                    if (this.pageMode != 0)
                    {
                        list.Sort(this.gloryPosComparer);
                        break;
                    }
                    list.Sort(this.playersPosComparer);
                    break;

                case 3:
                    if (this.pageMode != 0)
                    {
                        list.Sort(this.gloryNegComparer);
                        break;
                    }
                    list.Sort(this.playersNegComparer);
                    break;

                case 4:
                    if (this.pageMode != 0)
                    {
                        list.Sort(this.gloryPosComparer);
                        break;
                    }
                    list.Sort(this.pointsPosComparer);
                    break;

                case 5:
                    if (this.pageMode != 0)
                    {
                        list.Sort(this.gloryNegComparer);
                        break;
                    }
                    list.Sort(this.pointsNegComparer);
                    break;
            }
            foreach (HouseData data3 in list)
            {
                HouseLine control = new HouseLine();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(data3, yourHouseID, yourRank, appliedToHouse, position, this, this.pageMode == 1);
                this.wallScrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
                position++;
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
            this.update();
            base.Invalidate();
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

        private void factionInfoClick()
        {
            this.pageMode = 0;
            this.init(true);
        }

        public void GetHouseGloryPointsCallBack(GetHouseGloryPoints_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.HouseGloryPoints = returnData.gloryPoints;
                GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
            }
        }

        private void gloryInfoClick()
        {
            this.pageMode = 1;
            this.init(true);
        }

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            if (GameEngine.Instance.World.testGloryPointsUpdate())
            {
                RemoteServices.Instance.set_GetHouseGloryPoints_UserCallBack(new RemoteServices.GetHouseGloryPoints_UserCallBack(this.GetHouseGloryPointsCallBack));
                RemoteServices.Instance.GetHouseGloryPoints();
            }
            this.sidebar.addSideBar(7, this);
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 0x27);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(250, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(400, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider3Image.Position = new Point(560, 0);
            this.headerLabelsImage.addControl(this.divider3Image);
            this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House");
            this.houseLabel.Color = ARGBColors.Black;
            this.houseLabel.Position = new Point(9, -2);
            this.houseLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.houseLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.houseLabel);
            this.houseSortArea.Position = new Point(0, 0);
            this.houseSortArea.Size = new Size(250, this.headerLabelsImage.Height);
            this.houseSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortNameClick), "HouseListPanel_sort_house");
            this.headerLabelsImage.addControl(this.houseSortArea);
            if (this.pageMode == 0)
            {
                this.playersLabel.Text = SK.Text("GENERIC_Factions", "Factions");
            }
            else
            {
                this.playersLabel.Text = SK.Text("FactionInvites_Glory_Rank", "Glory Rank");
            }
            this.playersLabel.Color = ARGBColors.Black;
            this.playersLabel.Position = new Point(0xff, -2);
            this.playersLabel.Size = new Size(130, this.headerLabelsImage.Height);
            this.playersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.playersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.playersLabel);
            this.factionsSortArea.Position = new Point(250, 0);
            this.factionsSortArea.Size = new Size(150, this.headerLabelsImage.Height);
            this.factionsSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortFactionsClick), "HouseListPanel_sort_faction");
            this.headerLabelsImage.addControl(this.factionsSortArea);
            if (this.pageMode == 0)
            {
                this.pointsLabel.Text = SK.Text("FactionsPanel_Points", "Points");
            }
            else
            {
                this.pointsLabel.Text = SK.Text("FactionInvites_Glory_Points", "Glory Points");
            }
            this.pointsLabel.Color = ARGBColors.Black;
            this.pointsLabel.Position = new Point(0x195, -2);
            this.pointsLabel.Size = new Size(160, this.headerLabelsImage.Height);
            this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.pointsLabel);
            this.pointsSortArea.Position = new Point(400, 0);
            this.pointsSortArea.Size = new Size(160, this.headerLabelsImage.Height);
            this.pointsSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortPointsClick), "HouseListPanel_sort_points");
            this.headerLabelsImage.addControl(this.pointsSortArea);
            this.membershipLabel.Text = SK.Text("FactionInvites_Membership", "Membership");
            this.membershipLabel.Color = ARGBColors.Black;
            this.membershipLabel.Position = new Point(0x235, -2);
            this.membershipLabel.Size = new Size(0xaf, this.headerLabelsImage.Height);
            this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.membershipLabel);
            this.factionInfoButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.factionInfoButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.factionInfoButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.factionInfoButton.Position = new Point(100, 5);
            this.factionInfoButton.Text.Text = SK.Text("HouseInfoPanel_Faction_Info", "Faction Info");
            this.factionInfoButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.factionInfoButton.TextYOffset = -3;
            if (this.pageMode == 0)
            {
                this.factionInfoButton.Text.Color = ARGBColors.White;
                this.factionInfoButton.Text.DropShadowColor = ARGBColors.Black;
            }
            else
            {
                this.factionInfoButton.Text.Color = ARGBColors.Black;
                this.factionInfoButton.Text.clearDropShadow();
            }
            this.factionInfoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionInfoClick), "HouseInfoPanel_leave");
            this.mainBackgroundImage.addControl(this.factionInfoButton);
            this.gloryInfoButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.gloryInfoButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.gloryInfoButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.gloryInfoButton.Position = new Point(470, 5);
            this.gloryInfoButton.Text.Text = SK.Text("HouseInfoPanel_Glory_Info", "Glory Info");
            this.gloryInfoButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.gloryInfoButton.TextYOffset = -3;
            if (this.pageMode == 1)
            {
                this.gloryInfoButton.Text.Color = ARGBColors.White;
                this.gloryInfoButton.Text.DropShadowColor = ARGBColors.Black;
            }
            else
            {
                this.gloryInfoButton.Text.Color = ARGBColors.Black;
                this.gloryInfoButton.Text.clearDropShadow();
            }
            this.gloryInfoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gloryInfoClick), "HouseInfoPanel_leave");
            this.mainBackgroundImage.addControl(this.gloryInfoButton);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("HouseInfo_All_Houses", "All Houses"));
            this.wallScrollArea.Position = new Point(0x19, 0x44);
            this.wallScrollArea.Size = new Size(0x2c1, (height - 0x26) - 30);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, (height - 0x26) - 30));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x2dd, 0x44);
            this.wallScrollBar.Size = new Size(0x18, (height - 0x26) - 30);
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
            base.Name = "HouseListPanel";
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

        public void logout()
        {
            this.sortMethod = -1;
            this.pageMode = 0;
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

        public void selfJoinHouse(int houseID)
        {
            RemoteServices.Instance.set_SelfJoinHouse_UserCallBack(new RemoteServices.SelfJoinHouse_UserCallBack(this.selfJoinHouseCallback));
            RemoteServices.Instance.SelfJoinHouse(RemoteServices.Instance.UserFactionID, houseID, GameEngine.Instance.World.StoredFactionChangesPos);
        }

        public void selfJoinHouseCallback(SelfJoinHouse_ReturnType returnData)
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
                if ((returnData.yourFaction != null) && (returnData.yourFaction.houseID > 0))
                {
                    InterfaceMgr.Instance.showHousePanel(returnData.yourFaction.houseID);
                }
                else
                {
                    this.init(false);
                }
            }
            else if ((returnData.m_errorCode == ErrorCodes.ErrorCode.HOUSE_FULL) || (returnData.m_errorCode == ErrorCodes.ErrorCode.HOUSE_FACTION_NEEDS_5_MEMBERS))
            {
                if ((returnData.m_errorCode == ErrorCodes.ErrorCode.HOUSE_FACTION_NEEDS_5_MEMBERS) && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1))
                {
                    MyMessageBox.Show(SK.Text("ERRORCODE_HOUSE_FACTION_NEEDS_10_MEMBERS", "Your faction needs 10 members to join a house."), SK.Text("FactionsPanel_House_Join_Error", "House Join Error"));
                }
                else
                {
                    MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode), SK.Text("FactionsPanel_House_Join_Error", "House Join Error"));
                }
            }
        }

        private void sortFactionsClick()
        {
            if (this.sortMethod == 2)
            {
                this.sortMethod = 3;
            }
            else
            {
                this.sortMethod = 2;
            }
            this.addFactions();
        }

        private void sortNameClick()
        {
            if (this.sortMethod == 0)
            {
                this.sortMethod = 1;
            }
            else
            {
                this.sortMethod = 0;
            }
            this.addFactions();
        }

        private void sortPointsClick()
        {
            if (this.pageMode == 0)
            {
                if (this.sortMethod == 4)
                {
                    this.sortMethod = 5;
                }
                else
                {
                    this.sortMethod = 4;
                }
                this.addFactions();
            }
            else
            {
                this.sortFactionsClick();
            }
        }

        public void update()
        {
            this.sidebar.update();
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x44 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class GloryNegComparer : IComparer<HouseData>
        {
            public int Compare(HouseData y, HouseData x)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.loser != y.loser)
                {
                    if (x.loser)
                    {
                        return 1;
                    }
                    return -1;
                }
                if (!x.loser)
                {
                    int num = GameEngine.Instance.World.getGloryPoints(x.houseID);
                    int num2 = GameEngine.Instance.World.getGloryPoints(y.houseID);
                    if (x.houseID == 0)
                    {
                        num = -1;
                    }
                    if (y.houseID == 0)
                    {
                        num2 = -1;
                    }
                    if (num > num2)
                    {
                        return -1;
                    }
                    if (num < num2)
                    {
                        return 1;
                    }
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }

        public class GloryPosComparer : IComparer<HouseData>
        {
            public int Compare(HouseData x, HouseData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.loser != y.loser)
                {
                    if (x.loser)
                    {
                        return 1;
                    }
                    return -1;
                }
                if (!x.loser)
                {
                    int num = GameEngine.Instance.World.getGloryPoints(x.houseID);
                    int num2 = GameEngine.Instance.World.getGloryPoints(y.houseID);
                    if (x.houseID == 0)
                    {
                        num = -1;
                    }
                    if (y.houseID == 0)
                    {
                        num2 = -1;
                    }
                    if (num > num2)
                    {
                        return -1;
                    }
                    if (num < num2)
                    {
                        return 1;
                    }
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }

        public class HouseLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage allianceImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel houseMotto = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel houseName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton joinButton = new CustomSelfDrawPanel.CSDButton();
            private bool m_applied;
            private HouseData m_houseData;
            private HouseListPanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel numPlayersLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
            private MyMessageBoxPopUp PopUpRef;

            public void clickedLine()
            {
                GameEngine.Instance.playInterfaceSound("HouseListPanel_faction");
                if (this.m_houseData.houseID > 0)
                {
                    InterfaceMgr.Instance.showHousePanel(this.m_houseData.houseID);
                }
            }

            public void init(HouseData houseData, int yourHouseID, int yourRank, int appliedToHouse, int position, HouseListPanel parent, bool gloryMode)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_houseData = houseData;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(0, 0);
                this.backgroundImage.Size = new Size(this.backgroundImage.Size.Width, 0x33);
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                if (houseData.houseID > 0)
                {
                    this.houseImage.Image = (Image) GFXLibrary.house_circles_medium[houseData.houseID - 1];
                    this.houseImage.Position = new Point(5, 0);
                    this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.houseImage.CustomTooltipID = 0x904;
                    this.houseImage.CustomTooltipData = houseData.houseID;
                    this.backgroundImage.addControl(this.houseImage);
                }
                NumberFormatInfo nFI = GameEngine.NFI;
                Color black = ARGBColors.Black;
                if (houseData.houseID == yourHouseID)
                {
                    black = ARGBColors.Yellow;
                }
                this.houseName.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + houseData.houseID.ToString();
                this.houseName.Color = black;
                this.houseName.Position = new Point(0x40, 5);
                this.houseName.Size = new Size(280, this.backgroundImage.Height);
                this.houseName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.houseName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.houseName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.houseName);
                this.houseMotto.Text = "\"" + CustomTooltipManager.getHouseMotto(houseData.houseID) + "\"";
                this.houseMotto.Color = black;
                this.houseMotto.Position = new Point(0x40, 30);
                this.houseMotto.Size = new Size(280, this.backgroundImage.Height);
                this.houseMotto.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.houseMotto.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.houseMotto.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.houseMotto);
                int num = -1;
                if (!gloryMode)
                {
                    if (houseData.houseID == 0)
                    {
                        this.numPlayersLabel.Text = GameEngine.Instance.World.countHouseMembers(0).ToString("N", nFI);
                    }
                    else
                    {
                        this.numPlayersLabel.Text = houseData.numFactions.ToString("N", nFI);
                    }
                }
                else
                {
                    int num2 = GameEngine.Instance.World.getGloryRank(houseData.houseID);
                    if ((houseData.houseID == 0) || (num2 < 0))
                    {
                        this.numPlayersLabel.Text = "";
                    }
                    else
                    {
                        this.numPlayersLabel.Text = (num2 + 1).ToString("N", nFI);
                        num = GameEngine.Instance.World.getGloryPoints(houseData.houseID);
                    }
                }
                this.numPlayersLabel.Color = ARGBColors.Black;
                this.numPlayersLabel.Position = new Point(0xeb, 0);
                this.numPlayersLabel.Size = new Size(100, this.backgroundImage.Height);
                this.numPlayersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.numPlayersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.numPlayersLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.numPlayersLabel);
                if (!gloryMode)
                {
                    this.pointsLabel.Text = houseData.points.ToString("N", nFI);
                }
                else if (num >= 0)
                {
                    this.pointsLabel.Text = num.ToString("N", nFI);
                }
                else
                {
                    this.pointsLabel.Text = "";
                }
                this.pointsLabel.Color = ARGBColors.Black;
                this.pointsLabel.Position = new Point(410, 0);
                this.pointsLabel.Size = new Size(100, this.backgroundImage.Height);
                this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.pointsLabel);
                if (houseData.houseID > 0)
                {
                    this.membershipLabel.Color = ARGBColors.Black;
                    this.membershipLabel.Position = new Point(570, 3);
                    this.membershipLabel.Size = new Size(130, this.backgroundImage.Height);
                    this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.membershipLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.membershipLabel);
                    string str = "";
                    this.joinButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                    this.joinButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.joinButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                    this.joinButton.Position = new Point(0x237, 0x18);
                    this.joinButton.Text.Text = str = SK.Text("HouseInfoLine_Join", "Join");
                    this.joinButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.joinButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.joinButton.TextYOffset = -3;
                    this.joinButton.Text.Color = ARGBColors.Black;
                    this.joinButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.joinClicked), "HouseListPanel_join");
                    this.backgroundImage.addControl(this.joinButton);
                    this.joinButton.Visible = false;
                    if ((yourHouseID == 0) && (appliedToHouse > 0))
                    {
                        if (houseData.houseID == appliedToHouse)
                        {
                            str = SK.Text("HouseInfoLine_Applied", "Applied");
                            this.joinButton.Visible = true;
                            this.m_applied = true;
                            if (yourRank != 1)
                            {
                                this.joinButton.Enabled = false;
                            }
                        }
                    }
                    else if (((houseData.numFactions < GameEngine.Instance.LocalWorldData.Houses_MaxFactions) && (yourHouseID == 0)) && ((yourRank == 1) && (houseData.houseID != 0)))
                    {
                        if (houseData.numFactions >= GameEngine.Instance.LocalWorldData.Houses_SelfJoinLimit)
                        {
                            str = SK.Text("HouseInfoLine_Apply", "Apply");
                            this.joinButton.Visible = true;
                        }
                        else
                        {
                            str = SK.Text("HouseInfoLine_Join", "Join");
                            this.joinButton.Visible = true;
                        }
                    }
                    if ((houseData.houseID == 10) && GameEngine.Instance.LocalWorldData.AIWorld)
                    {
                        this.membershipLabel.Text = SK.Text("FactionInvites_Membership_closed", "Closed");
                        this.joinButton.Visible = false;
                    }
                    else if (houseData.numFactions < GameEngine.Instance.LocalWorldData.Houses_MaxFactions)
                    {
                        this.membershipLabel.Text = SK.Text("FactionInvites_Membership_open", "Open");
                    }
                    else
                    {
                        this.membershipLabel.Text = SK.Text("FactionInvites_Membership_closed", "Closed");
                    }
                    this.joinButton.Text.Text = str;
                    if (!this.joinButton.Visible)
                    {
                        this.membershipLabel.Position = new Point(570, 0);
                        this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    }
                }
                int num3 = GameEngine.Instance.World.getYourHouseRelation(houseData.houseID);
                if (num3 != 0)
                {
                    if (num3 > 0)
                    {
                        this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[0];
                        this.allianceImage.CustomTooltipID = 0x8ff;
                    }
                    else
                    {
                        this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[2];
                        this.allianceImage.CustomTooltipID = 0x900;
                    }
                    this.allianceImage.Position = new Point(0xee, 12);
                    this.allianceImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.allianceImage);
                }
                base.invalidate();
            }

            private void Join()
            {
                this.m_parent.selfJoinHouse(-1);
            }

            private void joinClicked()
            {
                if (this.m_parent != null)
                {
                    if (!this.m_applied)
                    {
                        this.m_parent.selfJoinHouse(this.m_houseData.houseID);
                    }
                    else
                    {
                        MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                        if (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FactionInvite_Cancel_Application", "Cancel Application"), yesNo) == DialogResult.Yes)
                        {
                            this.Join();
                        }
                    }
                }
            }

            public void update()
            {
            }
        }

        public class NameNegComparer : IComparer<HouseData>
        {
            public int Compare(HouseData y, HouseData x)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }

        public class NamePosComparer : IComparer<HouseData>
        {
            public int Compare(HouseData x, HouseData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }

        public class PlayersNegComparer : IComparer<HouseData>
        {
            public int Compare(HouseData y, HouseData x)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.numFactions > y.numFactions)
                {
                    return -1;
                }
                if (x.numFactions < y.numFactions)
                {
                    return 1;
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }

        public class PlayersPosComparer : IComparer<HouseData>
        {
            public int Compare(HouseData x, HouseData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.numFactions > y.numFactions)
                {
                    return -1;
                }
                if (x.numFactions < y.numFactions)
                {
                    return 1;
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }

        public class PointsNegComparer : IComparer<HouseData>
        {
            public int Compare(HouseData y, HouseData x)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.points > y.points)
                {
                    return -1;
                }
                if (x.points < y.points)
                {
                    return 1;
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }

        public class PointsPosComparer : IComparer<HouseData>
        {
            public int Compare(HouseData x, HouseData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.points > y.points)
                {
                    return -1;
                }
                if (x.points < y.points)
                {
                    return 1;
                }
                return x.houseID.CompareTo(y.houseID);
            }
        }
    }
}

