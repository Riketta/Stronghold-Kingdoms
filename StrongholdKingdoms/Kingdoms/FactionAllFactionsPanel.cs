namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class FactionAllFactionsPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea factionSortArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static FactionAllFactionsPanel instance;
        private List<FactionsAllLine> lineList = new List<FactionsAllLine>();
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private NameNegComparer nameNegComparer = new NameNegComparer();
        private NamePosComparer namePosComparer = new NamePosComparer();
        private OpenNegComparer openNegComparer = new OpenNegComparer();
        private OpenPosComparer openPosComparer = new OpenPosComparer();
        private CustomSelfDrawPanel.CSDArea openSortArea = new CustomSelfDrawPanel.CSDArea();
        public const int PANEL_ID = 0x2b;
        private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
        private PlayersNegComparer playersNegComparer = new PlayersNegComparer();
        private PlayersPosComparer playersPosComparer = new PlayersPosComparer();
        private CustomSelfDrawPanel.CSDArea playersSortArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        private PointsNegComparer pointsNegComparer = new PointsNegComparer();
        private PointsPosComparer pointsPosComparer = new PointsPosComparer();
        private CustomSelfDrawPanel.CSDArea pointsSortArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private int sortMethod = -1;
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public FactionAllFactionsPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addFactions()
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            int position = 0;
            SparseArray array = GameEngine.Instance.World.getAllFactions();
            List<FactionData> list = new List<FactionData>();
            foreach (FactionData data in array)
            {
                if ((data.active && (data.numMembers != 0)) && (data.factionName.Length != 0))
                {
                    list.Add(data);
                }
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
                    list.Sort(this.playersPosComparer);
                    break;

                case 3:
                    list.Sort(this.playersNegComparer);
                    break;

                case 4:
                    list.Sort(this.pointsPosComparer);
                    break;

                case 5:
                    list.Sort(this.pointsNegComparer);
                    break;

                case 6:
                    list.Sort(this.openPosComparer);
                    break;

                case 7:
                    list.Sort(this.openNegComparer);
                    break;
            }
            foreach (FactionData data2 in list)
            {
                FactionsAllLine control = new FactionsAllLine();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(data2, position, this);
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

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            this.sidebar.addSideBar(2, this);
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 9);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(290, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(440, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider3Image.Position = new Point(610, 0);
            this.headerLabelsImage.addControl(this.divider3Image);
            this.factionLabel.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
            this.factionLabel.Color = ARGBColors.Black;
            this.factionLabel.Position = new Point(9, -2);
            this.factionLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.factionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.factionLabel);
            this.factionSortArea.Position = new Point(0, 0);
            this.factionSortArea.Size = new Size(290, this.headerLabelsImage.Height);
            this.factionSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortNameClick), "FactionAllFactionsPanel_sort_faction");
            this.headerLabelsImage.addControl(this.factionSortArea);
            this.playersLabel.Text = SK.Text("FactionInvites_Players", "Players");
            this.playersLabel.Color = ARGBColors.Black;
            this.playersLabel.Position = new Point(0x127, -2);
            this.playersLabel.Size = new Size(140, this.headerLabelsImage.Height);
            this.playersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.playersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.playersLabel);
            this.playersSortArea.Position = new Point(290, 0);
            this.playersSortArea.Size = new Size(150, this.headerLabelsImage.Height);
            this.playersSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortPlayersClick), "FactionAllFactionsPanel_sort_players");
            this.headerLabelsImage.addControl(this.playersSortArea);
            this.pointsLabel.Text = SK.Text("FactionsPanel_Points", "Points");
            this.pointsLabel.Color = ARGBColors.Black;
            this.pointsLabel.Position = new Point(0x1bd, -2);
            this.pointsLabel.Size = new Size(160, this.headerLabelsImage.Height);
            this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.pointsLabel);
            this.pointsSortArea.Position = new Point(440, 0);
            this.pointsSortArea.Size = new Size(170, this.headerLabelsImage.Height);
            this.pointsSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortPointsClick), "FactionAllFactionsPanel_sort_points");
            this.headerLabelsImage.addControl(this.pointsSortArea);
            this.membershipLabel.Text = SK.Text("FactionInvites_Membership", "Membership");
            this.membershipLabel.Color = ARGBColors.Black;
            this.membershipLabel.Position = new Point(0x267, -2);
            this.membershipLabel.Size = new Size(110, this.headerLabelsImage.Height);
            this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.membershipLabel);
            this.openSortArea.Position = new Point(610, 0);
            this.openSortArea.Size = new Size(120, this.headerLabelsImage.Height);
            this.openSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortOpenClick), "FactionAllFactionsPanel_sort_points");
            this.headerLabelsImage.addControl(this.openSortArea);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_All_Factions", "All Factions"));
            this.wallScrollArea.Position = new Point(0x19, 0x26);
            this.wallScrollArea.Size = new Size(0x2c1, height - 0x26);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, height - 0x26));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Position = new Point(0x2dd, 0x26);
            this.wallScrollBar.Size = new Size(0x18, height - 0x26);
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            if (!resized)
            {
                CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
            }
            this.addFactions();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "FactionAllFactionsPanel";
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

        private void sortOpenClick()
        {
            if (this.sortMethod == 6)
            {
                this.sortMethod = 7;
            }
            else
            {
                this.sortMethod = 6;
            }
            this.addFactions();
        }

        private void sortPlayersClick()
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

        private void sortPointsClick()
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

        public void update()
        {
            this.sidebar.update();
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x26 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class FactionsAllLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage allianceImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
            private FactionData m_factionData;
            private FactionAllFactionsPanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel numPlayersLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();

            public void clickedLine()
            {
                GameEngine.Instance.playInterfaceSound("FactionAllFactionsPanel_entry_clicked");
                InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
            }

            public void init(FactionData factionData, int position, FactionAllFactionsPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_factionData = factionData;
                this.ClipVisible = true;
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
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
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
                this.numPlayersLabel.Text = factionData.numMembers.ToString("N", nFI);
                this.numPlayersLabel.Color = ARGBColors.Black;
                this.numPlayersLabel.Position = new Point(0xd7, 0);
                this.numPlayersLabel.Size = new Size(100, this.backgroundImage.Height);
                this.numPlayersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.numPlayersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.numPlayersLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.numPlayersLabel);
                this.pointsLabel.Text = factionData.points.ToString("N", nFI);
                this.pointsLabel.Color = ARGBColors.Black;
                this.pointsLabel.Position = new Point(390, 0);
                this.pointsLabel.Size = new Size(100, this.backgroundImage.Height);
                this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.pointsLabel);
                if (factionData.numMembers < GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
                {
                    if (factionData.openForApplications)
                    {
                        this.membershipLabel.Text = SK.Text("FactionInvites_Membership_open", "Open");
                    }
                    else
                    {
                        this.membershipLabel.Text = SK.Text("FactionInvites_Membership_closed", "Closed");
                    }
                }
                else
                {
                    this.membershipLabel.Text = SK.Text("FactionInvites_Membership_Full", "Full");
                }
                this.membershipLabel.Color = ARGBColors.Black;
                this.membershipLabel.Position = new Point(530, 0);
                this.membershipLabel.Size = new Size(160, this.backgroundImage.Height);
                this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.membershipLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.membershipLabel);
                int num = GameEngine.Instance.World.getYourFactionRelation(factionData.factionID);
                if (num != 0)
                {
                    if (num > 0)
                    {
                        this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[0];
                        this.allianceImage.CustomTooltipID = 0x8ff;
                    }
                    else
                    {
                        this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[2];
                        this.allianceImage.CustomTooltipID = 0x900;
                    }
                    this.allianceImage.Position = new Point(0xda, 2);
                    this.allianceImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.allianceImage);
                }
                base.invalidate();
            }

            public void update()
            {
            }
        }

        public class NameNegComparer : IComparer<FactionData>
        {
            public int Compare(FactionData y, FactionData x)
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
                return x.factionName.CompareTo(y.factionName);
            }
        }

        public class NamePosComparer : IComparer<FactionData>
        {
            public int Compare(FactionData x, FactionData y)
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
                return x.factionName.CompareTo(y.factionName);
            }
        }

        public class OpenNegComparer : IComparer<FactionData>
        {
            public int Compare(FactionData y, FactionData x)
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
                if (x.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
                {
                    if (y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
                    {
                        return x.factionName.CompareTo(y.factionName);
                    }
                    return 1;
                }
                if (y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
                {
                    return -1;
                }
                if (x.openForApplications)
                {
                    if (y.openForApplications)
                    {
                        return x.factionName.CompareTo(y.factionName);
                    }
                    return -1;
                }
                if (y.openForApplications)
                {
                    return 1;
                }
                return x.factionName.CompareTo(y.factionName);
            }
        }

        public class OpenPosComparer : IComparer<FactionData>
        {
            public int Compare(FactionData x, FactionData y)
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
                if (x.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
                {
                    if (y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
                    {
                        return x.factionName.CompareTo(y.factionName);
                    }
                    return 1;
                }
                if (y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
                {
                    return -1;
                }
                if (x.openForApplications)
                {
                    if (y.openForApplications)
                    {
                        return x.factionName.CompareTo(y.factionName);
                    }
                    return -1;
                }
                if (y.openForApplications)
                {
                    return 1;
                }
                return x.factionName.CompareTo(y.factionName);
            }
        }

        public class PlayersNegComparer : IComparer<FactionData>
        {
            public int Compare(FactionData y, FactionData x)
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
                if (x.numMembers > y.numMembers)
                {
                    return -1;
                }
                if (x.numMembers < y.numMembers)
                {
                    return 1;
                }
                return x.factionName.CompareTo(y.factionName);
            }
        }

        public class PlayersPosComparer : IComparer<FactionData>
        {
            public int Compare(FactionData x, FactionData y)
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
                if (x.numMembers > y.numMembers)
                {
                    return -1;
                }
                if (x.numMembers < y.numMembers)
                {
                    return 1;
                }
                return x.factionName.CompareTo(y.factionName);
            }
        }

        public class PointsNegComparer : IComparer<FactionData>
        {
            public int Compare(FactionData y, FactionData x)
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
                return x.factionName.CompareTo(y.factionName);
            }
        }

        public class PointsPosComparer : IComparer<FactionData>
        {
            public int Compare(FactionData x, FactionData y)
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
                return x.factionName.CompareTo(y.factionName);
            }
        }
    }
}

