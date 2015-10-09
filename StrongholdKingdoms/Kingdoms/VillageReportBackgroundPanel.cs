namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VillageReportBackgroundPanel : UserControl, IDockableControl, IDockWindow
    {
        private Bitmap _backBuffer;
        private AllArmiesPanel2 allArmiesPanel = new AllArmiesPanel2();
        private AllVassalsPanel allVassalsPanel = new AllVassalsPanel();
        private AllVillagesPanel allVillagesPanel = new AllVillagesPanel();
        private AvatarEditorPanel avatarEditorPanel = new AvatarEditorPanel();
        private Image backgroundImage;
        private BarracksPanel barracksPanel = new BarracksPanel();
        private CapitalBarracksPanel capitalBarracksPanel = new CapitalBarracksPanel();
        private CapitalDonateResourcesPanel2 capitalDonateResourcesPanel = new CapitalDonateResourcesPanel2();
        private CapitalForumPostsPanel capitalForumPostsPanel = new CapitalForumPostsPanel();
        private CapitalResourcesPanel2 capitalResourcesPanel = new CapitalResourcesPanel2();
        private CapitalSendTroopsPanel2 capitalSendTroopsPanel = new CapitalSendTroopsPanel2();
        private CapitalTradePanel capitalTradePanel = new CapitalTradePanel();
        private IContainer components;
        private CountryFrontPagePanel2 countryFrontPagePanel = new CountryFrontPagePanel2();
        private CountryVotePanel countryPanel = new CountryVotePanel();
        private CountyFrontPagePanel2 countyFrontPagePanel = new CountyFrontPagePanel2();
        private CountyVotePanel countyPanel = new CountyVotePanel();
        private int currentPanelHeight;
        private int currentPanelWidth;
        private DockableControl dockableControl;
        private DockWindow dockWindow;
        private FactionAllFactionsPanel factionAllFactionsPanel = new FactionAllFactionsPanel();
        private FactionDiplomacyPanel factionDiplomacyPanel = new FactionDiplomacyPanel();
        private FactionInvitePanel factionInvitePanel = new FactionInvitePanel();
        private FactionMyFactionPanel factionMyFactionPanel = new FactionMyFactionPanel();
        private FactionNewForumPanel factionNewForumPanel = new FactionNewForumPanel();
        private FactionNewForumPostsPanel factionNewForumPostsPanel = new FactionNewForumPostsPanel();
        private FactionOfficersPanel factionOfficersPanel = new FactionOfficersPanel();
        private FactionStartFactionPanel factionStartFactionPanel = new FactionStartFactionPanel();
        public bool forceBackgroundRedraw = true;
        private GloryPanel gloryPanel = new GloryPanel();
        private HoldBanquetPanel holdBanquetPanel = new HoldBanquetPanel();
        private HouseInfoPanel houseInfoPanel = new HouseInfoPanel();
        private HouseListPanel houseListPanel = new HouseListPanel();
        private int lastPanelType = -1;
        private int lastVillageVisited = -1;
        private MarketTransferPanel marketTransferPanel = new MarketTransferPanel();
        private NewQuestsPanel newQuestsPanel = new NewQuestsPanel();
        private CapitalForumPanel parishForumPanel = new CapitalForumPanel();
        private ParishWallPanel parishFrontPagePanel = new ParishWallPanel();
        private ParishVotePanel parishPanel = new ParishVotePanel();
        private ProvinceFrontPagePanel2 provinceFrontPagePanel = new ProvinceFrontPagePanel2();
        private ProvinceVotePanel provincePanel = new ProvinceVotePanel();
        private QuestsPanel2 questsPanel = new QuestsPanel2();
        private RankingsPanel rankingsPanel = new RankingsPanel();
        private ReportsPanel reportsPanel = new ReportsPanel();
        private ResourcesPanel2 resourcesPanel = new ResourcesPanel2();
        private StatsPanel statsPanel = new StatsPanel();
        private StockExchangePanel stockExchangePanel = new StockExchangePanel();
        private UnitsPanel2 unitsPanel = new UnitsPanel2();
        private UnknownPanel unknownPanel = new UnknownPanel();
        private UserDiplomacyPanel userDiplomacyPanel = new UserDiplomacyPanel();
        public UserInfoScreen2 userInfoScreen = new UserInfoScreen2();
        private VassalArmiesPanel2 vassalArmiesPanel = new VassalArmiesPanel2();
        private VillageVassalsPanel vassalControlPanel = new VillageVassalsPanel();
        private VillageArmiesPanel2 villageArmiesPanel = new VillageArmiesPanel2();
        private VillageReinforcementsPanel2 villageReinforcementsPanel = new VillageReinforcementsPanel2();

        public VillageReportBackgroundPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.dockWindow = new DockWindow(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        public void AddControl(UserControl control, int x, int y)
        {
            this.dockWindow.AddControl(control, x, y);
        }

        public void capitalDonateResourcesInit(int villageID, VillageMapBuilding selectedBuilding)
        {
            this.capitalDonateResourcesPanel.init(villageID, selectedBuilding);
        }

        public void clearAllReports()
        {
            this.reportsPanel.clearAllReports();
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void closeSubControls()
        {
            this.holdBanquetPanel.closeControl(true);
            this.marketTransferPanel.closeControl(true);
            this.stockExchangePanel.closeControl(true);
            this.barracksPanel.closeControl(true);
            this.resourcesPanel.closeControl(true);
            this.villageReinforcementsPanel.closeControl(true);
            this.villageArmiesPanel.closeControl(true);
            this.vassalControlPanel.closeControl(true);
            this.avatarEditorPanel.closeControl(true);
            this.rankingsPanel.closeControl(true);
            this.userInfoScreen.closeControl(true);
            this.gloryPanel.closeControl(true);
            this.statsPanel.closeControl(true);
            this.reportsPanel.closeControl(true);
            this.unknownPanel.closeControl(true);
            this.vassalArmiesPanel.closeControl(true);
            this.capitalSendTroopsPanel.closeControl(true);
            this.unitsPanel.closeControl(true);
            this.allArmiesPanel.closeControl(true);
            this.allVassalsPanel.closeControl(true);
            this.questsPanel.closeControl(true);
            this.newQuestsPanel.closeControl(true);
            this.factionInvitePanel.closeControl(true);
            this.factionMyFactionPanel.closeControl(true);
            this.factionStartFactionPanel.closeControl(true);
            this.factionAllFactionsPanel.closeControl(true);
            this.factionNewForumPostsPanel.closeControl(true);
            this.factionOfficersPanel.closeControl(true);
            this.factionDiplomacyPanel.closeControl(true);
            this.factionNewForumPanel.closeControl(true);
            this.houseInfoPanel.closeControl(true);
            this.houseListPanel.closeControl(true);
            this.allVillagesPanel.closeControl(true);
            this.userDiplomacyPanel.closeControl(true);
            this.capitalResourcesPanel.closeControl(true);
            this.capitalTradePanel.closeControl(true);
            this.capitalBarracksPanel.closeControl(true);
            this.parishPanel.closeControl(true);
            this.parishForumPanel.closeControl(true);
            this.parishFrontPagePanel.closeControl(true);
            this.capitalForumPostsPanel.closeControl(true);
            this.countyPanel.closeControl(true);
            this.countyFrontPagePanel.closeControl(true);
            this.provincePanel.closeControl(true);
            this.provinceFrontPagePanel.closeControl(true);
            this.countryPanel.closeControl(true);
            this.countryFrontPagePanel.closeControl(true);
            this.capitalDonateResourcesPanel.closeControl(true);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void deleteReportFolder(string folderName, int mode)
        {
            this.reportsPanel.deleteReportFolder(folderName, mode);
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

        private void drawImageStretched(Graphics g, Image image, float x, float y, float width, float height)
        {
            RectangleF ef;
            if (image.Width == 1)
            {
                ef = new RectangleF(0f, 0f, 1E-05f, (float) image.Height);
            }
            else
            {
                ef = new RectangleF(0f, 0f, (float) image.Width, 1E-05f);
            }
            RectangleF destRect = new RectangleF(x, y, width, height);
            g.DrawImage(image, destRect, ef, GraphicsUnit.Pixel);
        }

        public void flushParishFrontPageInfo(int parishID)
        {
            this.parishFrontPagePanel.flushData(parishID);
        }

        public void forceUpdateParishFrontPage()
        {
            this.parishFrontPagePanel.forceUpdateParish();
        }

        public Point getLocation()
        {
            switch (this.lastPanelType)
            {
                case 0x13:
                    return this.rankingsPanel.Location;
            }
            return new Point();
        }

        public object getReportData(long reportID)
        {
            return this.reportsPanel.getReportData(reportID);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Black;
            base.Name = "VillageReportBackgroundPanel";
            base.Size = new Size(630, 0x1ca);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public void inviteToFaction(string username)
        {
            this.factionOfficersPanel.inviteToFaction(username);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isTab0OverLayActive()
        {
            return this.capitalDonateResourcesPanel.isVisible();
        }

        public bool isTextInputScreenActive()
        {
            return (this.parishFrontPagePanel.isVisible() || this.statsPanel.isVisible());
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void leaderboardSearchComplete(LeaderBoardSearchResults results)
        {
            this.statsPanel.searchComplete(results);
        }

        public void logout()
        {
            this.parishPanel.logout();
            this.parishFrontPagePanel.logout();
            this.countyPanel.logout();
            this.countyFrontPagePanel.logout();
            this.provincePanel.logout();
            this.provinceFrontPagePanel.logout();
            this.countryPanel.logout();
            this.countryFrontPagePanel.logout();
            this.marketTransferPanel.logout();
            this.stockExchangePanel.logout();
            this.factionNewForumPanel.clearForum();
            this.factionNewForumPostsPanel.logout();
            this.parishForumPanel.clearForum();
            this.capitalForumPostsPanel.logout();
            this.houseInfoPanel.logout();
            this.factionAllFactionsPanel.logout();
            this.allVillagesPanel.logout();
            this.houseListPanel.logout();
            this.rankingsPanel.logout();
        }

        public void moveReports(string folderName)
        {
            this.reportsPanel.moveReports(folderName);
        }

        public void newVillageLoaded()
        {
            bool flag = true;
            if ((this.lastPanelType == 6) || (this.lastPanelType == 10))
            {
                flag = false;
            }
            if (flag)
            {
                if (this.lastPanelType >= 0x3e8)
                {
                    if (!InterfaceMgr.Instance.isSelectedVillageACapital())
                    {
                        switch (this.lastPanelType)
                        {
                            case 0x3ea:
                            case 0x3eb:
                                InterfaceMgr.Instance.setVillageTabSubMode(3);
                                return;

                            case 0x3ec:
                                InterfaceMgr.Instance.setVillageTabSubMode(4);
                                return;

                            case 0x3ed:
                                InterfaceMgr.Instance.setVillageTabSubMode(5);
                                return;
                        }
                        InterfaceMgr.Instance.getVillageTabBar().changeTab(9);
                        InterfaceMgr.Instance.getVillageTabBar().changeTab(0);
                        return;
                    }
                }
                else if ((this.lastPanelType >= 0) && InterfaceMgr.Instance.isSelectedVillageACapital())
                {
                    switch (this.lastPanelType)
                    {
                        case 2:
                        case 3:
                            InterfaceMgr.Instance.setVillageTabSubMode(0x3eb);
                            return;

                        case 4:
                            InterfaceMgr.Instance.setVillageTabSubMode(0x3ec);
                            return;

                        case 5:
                            InterfaceMgr.Instance.setVillageTabSubMode(0x3ed);
                            return;

                        case 9:
                            InterfaceMgr.Instance.setVillageTabSubMode(0x3f1);
                            return;
                    }
                    InterfaceMgr.Instance.getVillageTabBar().changeTab(6);
                    return;
                }
            }
            switch (this.lastPanelType)
            {
                case 0x452:
                case 0x4b6:
                case 0x51a:
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(6);
                    return;

                case 0x453:
                case 0x4b7:
                case 0x51b:
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(7);
                    return;

                case 0x454:
                case 0x4b8:
                case 0x51c:
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
                    break;

                case 1:
                    this.holdBanquetPanel.init();
                    return;

                case 2:
                    this.marketTransferPanel.backupData();
                    this.marketTransferPanel.resume(this.marketTransferPanel.SelectedTargetVillage, true);
                    return;

                case 3:
                case 5:
                case 9:
                case 10:
                case 14:
                case 0x10:
                case 0x3ec:
                case 0x3ed:
                    break;

                case 4:
                    this.barracksPanel.init();
                    return;

                case 6:
                    this.villageReinforcementsPanel.resume();
                    this.villageReinforcementsPanel.init(true);
                    return;

                case 7:
                    this.villageArmiesPanel.init(false);
                    return;

                case 8:
                    this.vassalControlPanel.reinit();
                    return;

                case 11:
                case 12:
                case 13:
                    this.unknownPanel.init();
                    return;

                case 15:
                    this.vassalArmiesPanel.init(false);
                    return;

                case 0x11:
                    this.capitalSendTroopsPanel.init(true);
                    return;

                case 0x12:
                    this.unitsPanel.init();
                    return;

                case 0x3eb:
                    this.capitalTradePanel.selectStockExchange(-1);
                    this.capitalTradePanel.init();
                    return;

                case 0x3ee:
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(6);
                    return;

                case 0x3ef:
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(7);
                    return;

                case 0x3f0:
                    if (this.lastVillageVisited == InterfaceMgr.Instance.getSelectedMenuVillage())
                    {
                        this.parishFrontPagePanel.init(false);
                        return;
                    }
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
                    return;

                case 0x3f1:
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(7);
                    return;

                case 0x3fd:
                    InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(0);
                    return;

                default:
                    return;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if ((this._backBuffer == null) || this.forceBackgroundRedraw)
            {
                if (this._backBuffer == null)
                {
                    if ((base.ClientSize.Width == 0) || (base.ClientSize.Height == 0))
                    {
                        return;
                    }
                    this._backBuffer = new Bitmap(base.ClientSize.Width, base.ClientSize.Height);
                }
                this.forceBackgroundRedraw = false;
                Graphics g = Graphics.FromImage(this._backBuffer);
                if (this.backgroundImage != null)
                {
                    for (int i = 0; i < base.ClientSize.Height; i += 0x200)
                    {
                        for (int j = 0; j < base.ClientSize.Width; j += 0x200)
                        {
                            g.DrawImageUnscaledAndClipped(this.backgroundImage, new Rectangle(j, i, 0x200, 0x200));
                        }
                    }
                }
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topleft, 0, 0, 0x80, 0x80);
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topright, base.ClientSize.Width - 0x80, 0, 0x80, 0x80);
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomleft, 0, base.ClientSize.Height - 0x80, 0x80, 0x80);
                g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomright, base.ClientSize.Width - 0x80, base.ClientSize.Height - 0x80, 0x80, 0x80);
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_top, 128f, 0f, (float) (base.ClientSize.Width - 0x100), 128f);
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_bottom, 128f, (float) (base.ClientSize.Height - 0x80), (float) (base.ClientSize.Width - 0x100), 128f);
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_left, 0f, 128f, 128f, (float) (base.ClientSize.Height - 0x100));
                this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_right, (float) (base.ClientSize.Width - 0x80), 128f, 128f, (float) (base.ClientSize.Height - 0x100));
                int num3 = ((base.ClientSize.Width - this.currentPanelWidth) / 2) + 8;
                int num4 = ((base.ClientSize.Height - this.currentPanelHeight) / 2) + 8;
                if ((num3 > 0) || (num4 > 0))
                {
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topleft, num3 - 0x80, num4 - 0x80, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topright, num3 + this.currentPanelWidth, num4 - 0x80, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomleft, num3 - 0x80, num4 + this.currentPanelHeight, 0x80, 0x80);
                    g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomright, num3 + this.currentPanelWidth, num4 + this.currentPanelHeight, 0x80, 0x80);
                    if (num3 > 0)
                    {
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_top, (float) num3, (float) (num4 - 0x80), (float) this.currentPanelWidth, 128f);
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_bottom, (float) num3, (float) (num4 + this.currentPanelHeight), (float) this.currentPanelWidth, 128f);
                    }
                    if (num4 > 0)
                    {
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_left, (float) (num3 - 0x80), (float) num4, 128f, (float) this.currentPanelHeight);
                        this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_right, (float) (num3 + this.currentPanelWidth), (float) num4, 128f, (float) this.currentPanelHeight);
                    }
                }
                g.Dispose();
            }
            if (e != null)
            {
                e.Graphics.DrawImageUnscaled(this._backBuffer, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this._backBuffer != null)
            {
                this._backBuffer.Dispose();
                this._backBuffer = null;
                base.Invalidate();
            }
            base.OnSizeChanged(e);
        }

        public bool queryDeleteReport(long reportID)
        {
            return this.reportsPanel.queryDeleteReport(reportID);
        }

        public void questPanelCompleteQuest(int quest)
        {
            this.questsPanel.completeQuest(quest);
        }

        public void questPanelInit()
        {
            this.questsPanel.init(false);
        }

        public void RemoveControl(UserControl control)
        {
            this.dockWindow.RemoveControl(control);
        }

        public void resetData()
        {
            this.stockExchangePanel.resetBackupData();
            this.marketTransferPanel.resetBackupData();
        }

        public void screenResize()
        {
            this.holdBanquetPanel.Location = new Point((base.Size.Width - this.holdBanquetPanel.Size.Width) / 2, (base.Size.Height - this.holdBanquetPanel.Size.Height) / 2);
            this.marketTransferPanel.Location = new Point((base.Size.Width - this.marketTransferPanel.Size.Width) / 2, (base.Size.Height - this.marketTransferPanel.Size.Height) / 2);
            this.stockExchangePanel.Location = new Point((base.Size.Width - this.stockExchangePanel.Size.Width) / 2, (base.Size.Height - this.stockExchangePanel.Size.Height) / 2);
            this.barracksPanel.Location = new Point((base.Size.Width - this.barracksPanel.Size.Width) / 2, (base.Size.Height - this.barracksPanel.Size.Height) / 2);
            this.unitsPanel.Location = new Point((base.Size.Width - this.barracksPanel.Size.Width) / 2, (base.Size.Height - this.unitsPanel.Size.Height) / 2);
            this.resourcesPanel.Location = new Point((base.Size.Width - this.resourcesPanel.Size.Width) / 2, (base.Size.Height - this.resourcesPanel.Size.Height) / 2);
            this.avatarEditorPanel.Location = new Point((base.Size.Width - this.avatarEditorPanel.Size.Width) / 2, (base.Size.Height - this.avatarEditorPanel.Size.Height) / 2);
            this.rankingsPanel.Location = new Point((base.Size.Width - this.rankingsPanel.Size.Width) / 2, (base.Size.Height - this.rankingsPanel.Size.Height) / 2);
            this.userInfoScreen.Location = new Point((base.Size.Width - this.userInfoScreen.Size.Width) / 2, (base.Size.Height - this.userInfoScreen.Size.Height) / 2);
            this.unknownPanel.Location = new Point((base.Size.Width - this.unknownPanel.Size.Width) / 2, (base.Size.Height - this.unknownPanel.Size.Height) / 2);
            this.vassalArmiesPanel.Location = new Point((base.Size.Width - this.vassalArmiesPanel.Size.Width) / 2, (base.Size.Height - this.vassalArmiesPanel.Size.Height) / 2);
            this.villageReinforcementsPanel.Location = new Point((base.Size.Width - this.villageReinforcementsPanel.Size.Width) / 2, 0);
            if (this.lastPanelType == 6)
            {
                this.currentPanelHeight = base.Height;
                this.villageReinforcementsPanel.init(true);
                this.villageReinforcementsPanel.Invalidate();
            }
            this.capitalResourcesPanel.Location = new Point((base.Size.Width - this.capitalResourcesPanel.Size.Width) / 2, (base.Size.Height - this.capitalResourcesPanel.Size.Height) / 2);
            this.capitalTradePanel.Location = new Point((base.Size.Width - this.capitalTradePanel.Size.Width) / 2, (base.Size.Height - this.capitalTradePanel.Size.Height) / 2);
            this.capitalBarracksPanel.Location = new Point((base.Size.Width - this.capitalBarracksPanel.Size.Width) / 2, (base.Size.Height - this.capitalBarracksPanel.Size.Height) / 2);
            this.parishPanel.Location = new Point((base.Size.Width - this.parishPanel.Size.Width) / 2, 0);
            this.parishPanel.Size = new Size(0x3e0, base.Height);
            if (this.lastPanelType == 0x3ee)
            {
                this.currentPanelHeight = base.Height;
                this.parishPanel.init(true);
                this.parishPanel.Invalidate();
            }
            this.parishForumPanel.Location = new Point((base.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
            this.parishForumPanel.Size = new Size(0x3e0, base.Height);
            if (((this.lastPanelType == 0x3ef) || (this.lastPanelType == 0x453)) || ((this.lastPanelType == 0x4b7) || (this.lastPanelType == 0x51b)))
            {
                this.currentPanelHeight = base.Height;
                this.parishForumPanel.init(true);
                this.parishForumPanel.Invalidate();
            }
            this.parishFrontPagePanel.Location = new Point((base.Size.Width - this.parishFrontPagePanel.Size.Width) / 2, 0);
            this.parishFrontPagePanel.Size = new Size(0x3e0, base.Height);
            if (this.lastPanelType == 0x3f0)
            {
                this.currentPanelHeight = base.Height;
                this.parishFrontPagePanel.init(true);
                this.parishFrontPagePanel.Invalidate();
            }
            this.capitalForumPostsPanel.Location = new Point((base.Size.Width - this.capitalForumPostsPanel.Size.Width) / 2, 0);
            this.capitalForumPostsPanel.Size = new Size(0x3e0, base.Height);
            if (this.lastPanelType == 0x3f1)
            {
                this.currentPanelHeight = base.Height;
                this.capitalForumPostsPanel.init(true);
                this.capitalForumPostsPanel.Invalidate();
            }
            this.statsPanel.Location = new Point((base.Size.Width - this.statsPanel.Size.Width) / 2, 0);
            this.statsPanel.Size = new Size(this.statsPanel.Width, base.Height);
            if (this.lastPanelType == 20)
            {
                this.currentPanelHeight = base.Height;
                this.statsPanel.init(true);
                this.statsPanel.Invalidate();
            }
            this.reportsPanel.Location = new Point((base.Size.Width - this.reportsPanel.Size.Width) / 2, 0);
            this.reportsPanel.Size = new Size(this.reportsPanel.Width, base.Height);
            if (this.lastPanelType == 0x15)
            {
                this.currentPanelHeight = base.Height;
                this.reportsPanel.init(true);
                this.reportsPanel.Invalidate();
            }
            this.villageArmiesPanel.Location = new Point((base.Size.Width - this.villageArmiesPanel.Size.Width) / 2, 0);
            this.villageArmiesPanel.Size = new Size(this.villageArmiesPanel.Width, base.Height);
            if (this.lastPanelType == 7)
            {
                this.currentPanelHeight = base.Height;
                this.villageArmiesPanel.init(true);
                this.villageArmiesPanel.Invalidate();
            }
            this.vassalControlPanel.Location = new Point((base.Size.Width - this.vassalControlPanel.Size.Width) / 2, 0);
            this.vassalControlPanel.Size = new Size(this.vassalControlPanel.Width, base.Height);
            if (this.lastPanelType == 8)
            {
                this.currentPanelHeight = base.Height;
                this.vassalControlPanel.init(true);
                this.vassalControlPanel.Invalidate();
            }
            this.allArmiesPanel.Location = new Point((base.Size.Width - this.allArmiesPanel.Size.Width) / 2, 0);
            this.allArmiesPanel.Size = new Size(this.allArmiesPanel.Width, base.Height);
            if (this.lastPanelType == 0x17)
            {
                this.currentPanelHeight = base.Height;
                this.allArmiesPanel.init(true, -1);
                this.allArmiesPanel.Invalidate();
            }
            this.allVassalsPanel.Location = new Point((base.Size.Width - this.allVassalsPanel.Size.Width) / 2, 0);
            this.allVassalsPanel.Size = new Size(this.allVassalsPanel.Width, base.Height);
            if (this.lastPanelType == 0x18)
            {
                this.currentPanelHeight = base.Height;
                this.allVassalsPanel.init(true);
                this.allVassalsPanel.Invalidate();
            }
            this.questsPanel.Location = new Point((base.Size.Width - this.questsPanel.Size.Width) / 2, 0);
            this.questsPanel.Size = new Size(this.questsPanel.Width, base.Height);
            if (this.lastPanelType == 0x19)
            {
                this.currentPanelHeight = base.Height;
                this.questsPanel.init(true);
                this.questsPanel.Invalidate();
            }
            this.newQuestsPanel.Location = new Point((base.Size.Width - this.newQuestsPanel.Size.Width) / 2, 0);
            this.newQuestsPanel.Size = new Size(this.newQuestsPanel.Width, base.Height);
            if (this.lastPanelType == 0x1a)
            {
                this.currentPanelHeight = base.Height;
                this.newQuestsPanel.init(true);
                this.newQuestsPanel.Invalidate();
            }
            this.factionInvitePanel.Location = new Point((base.Size.Width - this.factionInvitePanel.Size.Width) / 2, 0);
            this.factionInvitePanel.Size = new Size(this.factionInvitePanel.Width, base.Height);
            if (this.lastPanelType == 0x29)
            {
                this.currentPanelHeight = base.Height;
                this.factionInvitePanel.init(true);
                this.factionInvitePanel.Invalidate();
            }
            this.factionMyFactionPanel.Location = new Point((base.Size.Width - this.factionMyFactionPanel.Size.Width) / 2, 0);
            this.factionMyFactionPanel.Size = new Size(this.factionMyFactionPanel.Width, base.Height);
            if (this.lastPanelType == 0x2a)
            {
                this.currentPanelHeight = base.Height;
                this.factionMyFactionPanel.init(true);
                this.factionMyFactionPanel.Invalidate();
            }
            this.factionStartFactionPanel.Location = new Point((base.Size.Width - this.factionStartFactionPanel.Size.Width) / 2, 0);
            this.factionStartFactionPanel.Size = new Size(this.factionStartFactionPanel.Width, base.Height);
            if (this.lastPanelType == 0x2f)
            {
                this.currentPanelHeight = base.Height;
                this.factionStartFactionPanel.init(true);
                this.factionStartFactionPanel.Invalidate();
            }
            this.factionAllFactionsPanel.Location = new Point((base.Size.Width - this.factionAllFactionsPanel.Size.Width) / 2, 0);
            this.factionAllFactionsPanel.Size = new Size(this.factionAllFactionsPanel.Width, base.Height);
            if (this.lastPanelType == 0x2b)
            {
                this.currentPanelHeight = base.Height;
                this.factionAllFactionsPanel.init(true);
                this.factionAllFactionsPanel.Invalidate();
            }
            this.allVillagesPanel.Location = new Point((base.Size.Width - this.allVillagesPanel.Size.Width) / 2, 0);
            this.allVillagesPanel.Size = new Size(this.allVillagesPanel.Width, base.Height);
            if (this.lastPanelType == 100)
            {
                this.currentPanelHeight = base.Height;
                this.allVillagesPanel.init(true);
                this.allVillagesPanel.Invalidate();
            }
            this.factionNewForumPostsPanel.Location = new Point((base.Size.Width - this.factionNewForumPostsPanel.Size.Width) / 2, 0);
            this.factionNewForumPostsPanel.Size = new Size(this.factionNewForumPostsPanel.Width, base.Height);
            if (this.lastPanelType == 0x30)
            {
                this.currentPanelHeight = base.Height;
                this.factionNewForumPostsPanel.init(true);
                this.factionNewForumPostsPanel.Invalidate();
            }
            this.factionOfficersPanel.Location = new Point((base.Size.Width - this.factionOfficersPanel.Size.Width) / 2, 0);
            this.factionOfficersPanel.Size = new Size(this.factionOfficersPanel.Width, base.Height);
            if (this.lastPanelType == 0x2e)
            {
                this.currentPanelHeight = base.Height;
                this.factionOfficersPanel.init(true);
                this.factionOfficersPanel.Invalidate();
            }
            this.factionDiplomacyPanel.Location = new Point((base.Size.Width - this.factionDiplomacyPanel.Size.Width) / 2, 0);
            this.factionDiplomacyPanel.Size = new Size(this.factionDiplomacyPanel.Width, base.Height);
            if (this.lastPanelType == 0x2c)
            {
                this.currentPanelHeight = base.Height;
                this.factionDiplomacyPanel.init(true);
                this.factionDiplomacyPanel.Invalidate();
            }
            this.factionNewForumPanel.Location = new Point((base.Size.Width - this.factionNewForumPanel.Size.Width) / 2, 0);
            this.factionNewForumPanel.Size = new Size(this.factionNewForumPanel.Width, base.Height);
            if (this.lastPanelType == 0x2d)
            {
                this.currentPanelHeight = base.Height;
                this.factionNewForumPanel.init(true);
                this.factionNewForumPanel.Invalidate();
            }
            this.houseInfoPanel.Location = new Point((base.Size.Width - this.houseInfoPanel.Size.Width) / 2, 0);
            this.houseInfoPanel.Size = new Size(this.houseInfoPanel.Width, base.Height);
            if (this.lastPanelType == 0x34)
            {
                this.currentPanelHeight = base.Height;
                this.houseInfoPanel.init(true);
                this.houseInfoPanel.Invalidate();
            }
            this.houseListPanel.Location = new Point((base.Size.Width - this.houseListPanel.Size.Width) / 2, 0);
            this.houseListPanel.Size = new Size(this.houseListPanel.Width, base.Height);
            if (this.lastPanelType == 0x33)
            {
                this.currentPanelHeight = base.Height;
                this.houseListPanel.init(true);
                this.houseListPanel.Invalidate();
            }
            this.countyPanel.Location = new Point((base.Size.Width - this.countyPanel.Size.Width) / 2, 0);
            this.countyPanel.Size = new Size(this.countyPanel.Width, base.Height);
            if (this.lastPanelType == 0x452)
            {
                this.currentPanelHeight = base.Height;
                this.countyPanel.init(true);
                this.countyPanel.Invalidate();
            }
            this.countyFrontPagePanel.Location = new Point((base.Size.Width - this.countyFrontPagePanel.Size.Width) / 2, (base.Size.Height - this.countyFrontPagePanel.Size.Height) / 2);
            this.provincePanel.Location = new Point((base.Size.Width - this.provincePanel.Size.Width) / 2, 0);
            this.provincePanel.Size = new Size(this.provincePanel.Width, base.Height);
            if (this.lastPanelType == 0x4b6)
            {
                this.currentPanelHeight = base.Height;
                this.provincePanel.init(true);
                this.provincePanel.Invalidate();
            }
            this.provinceFrontPagePanel.Location = new Point((base.Size.Width - this.provinceFrontPagePanel.Size.Width) / 2, (base.Size.Height - this.provinceFrontPagePanel.Size.Height) / 2);
            this.countryPanel.Location = new Point((base.Size.Width - this.countryPanel.Size.Width) / 2, 0);
            this.countryPanel.Size = new Size(this.countryPanel.Width, base.Height);
            if (this.lastPanelType == 0x51a)
            {
                this.currentPanelHeight = base.Height;
                this.countryPanel.init(true);
                this.countryPanel.Invalidate();
            }
            this.countryFrontPagePanel.Location = new Point((base.Size.Width - this.countryFrontPagePanel.Size.Width) / 2, (base.Size.Height - this.countryFrontPagePanel.Size.Height) / 2);
            this.capitalDonateResourcesPanel.Location = new Point((base.Size.Width - this.capitalDonateResourcesPanel.Size.Width) / 2, (base.Size.Height - this.capitalDonateResourcesPanel.Size.Height) / 2);
            this.userDiplomacyPanel.Location = new Point((base.Size.Width - this.userDiplomacyPanel.Size.Width) / 2, 0);
            this.userDiplomacyPanel.Size = new Size(this.userDiplomacyPanel.Width, base.Height);
            if (this.lastPanelType == 60)
            {
                this.currentPanelHeight = base.Height;
                this.userDiplomacyPanel.init(true);
                this.userDiplomacyPanel.Invalidate();
            }
            if (this.lastPanelType == 0x16)
            {
                int width = base.Width;
                int height = base.Height;
                if (width > 0x640)
                {
                    width = 0x640;
                }
                if (height > 0x400)
                {
                    height = 0x400;
                }
                this.gloryPanel.Size = new Size(width, height);
                this.currentPanelWidth = width;
                this.currentPanelHeight = height;
                this.currentPanelWidth -= 0xa8;
                this.currentPanelHeight -= 0xa8;
                this.gloryPanel.Location = new Point((base.Size.Width - this.gloryPanel.Size.Width) / 2, (base.Size.Height - this.gloryPanel.Size.Height) / 2);
                this.gloryPanel.init();
            }
        }

        public void selectAttackTarget(int villageID)
        {
        }

        public void selectExchange(int villageID)
        {
            this.stockExchangePanel.selectStockExchange(villageID);
        }

        public void selectScoutsTarget(int villageID)
        {
        }

        public void selectVassalVillage(int villageID)
        {
            this.vassalControlPanel.setVassalVillage(villageID);
        }

        public void setBackgroundImage(Image image)
        {
            if (this.backgroundImage != image)
            {
                this.backgroundImage = image;
                this.forceBackgroundRedraw = true;
            }
        }

        public void setCapitalSendTargetVillage(int villageID)
        {
            this.capitalSendTroopsPanel.setTargetVillage(villageID);
        }

        public void setReinforcementVillage(int villageID)
        {
            this.villageReinforcementsPanel.setReinforcementVillage(villageID);
        }

        public void setReportAlreadyRead(long reportID)
        {
            this.reportsPanel.setReportAlreadyRead(reportID);
        }

        public void setReportData(object reportData, long reportID)
        {
            this.reportsPanel.setReportData(reportData, reportID);
        }

        public void setVassalArmiesVillage(int villageID)
        {
            this.vassalArmiesPanel.setVassalArmiesVillage(villageID);
        }

        public void setVassalTargetVillage(int villageID, int targetVillage)
        {
        }

        public void showPanel(int panelID)
        {
            if (panelID != -1)
            {
                GFXLibrary.getPanelDescFromID(panelID);
            }
            if (this.lastPanelType == 0x3f0)
            {
                this.parishFrontPagePanel.leaving();
            }
            this.lastVillageVisited = InterfaceMgr.Instance.getSelectedMenuVillage();
            this.lastPanelType = panelID;
            this.closeSubControls();
            if (panelID >= 0)
            {
                switch (panelID)
                {
                    case 1:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.holdBanquetPanel.initProperties(true, "Hold Banquet", this);
                        this.holdBanquetPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.holdBanquetPanel.display(this, (base.Size.Width - this.holdBanquetPanel.Size.Width) / 2, (base.Size.Height - this.holdBanquetPanel.Size.Height) / 2);
                        this.holdBanquetPanel.BringToFront();
                        this.holdBanquetPanel.init();
                        this.currentPanelWidth = this.holdBanquetPanel.Size.Width;
                        this.currentPanelHeight = this.holdBanquetPanel.Size.Height;
                        break;

                    case 2:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.marketTransferPanel.initProperties(true, "Market Transfer", this);
                        this.marketTransferPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.marketTransferPanel.display(this, (base.Size.Width - this.marketTransferPanel.Size.Width) / 2, (base.Size.Height - this.marketTransferPanel.Size.Height) / 2);
                        this.marketTransferPanel.BringToFront();
                        this.marketTransferPanel.init();
                        this.currentPanelWidth = this.marketTransferPanel.Size.Width;
                        this.currentPanelHeight = this.marketTransferPanel.Size.Height;
                        break;

                    case 3:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.stockExchangePanel.initProperties(true, "Stock Exchange", this);
                        this.stockExchangePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.stockExchangePanel.display(this, (base.Size.Width - this.stockExchangePanel.Size.Width) / 2, (base.Size.Height - this.stockExchangePanel.Size.Height) / 2);
                        this.stockExchangePanel.BringToFront();
                        this.stockExchangePanel.init();
                        this.currentPanelWidth = this.stockExchangePanel.Size.Width;
                        this.currentPanelHeight = this.stockExchangePanel.Size.Height;
                        break;

                    case 4:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.barracksPanel.initProperties(true, "Barracks", this);
                        this.barracksPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.barracksPanel.display(this, (base.Size.Width - this.barracksPanel.Size.Width) / 2, (base.Size.Height - this.barracksPanel.Size.Height) / 2);
                        this.barracksPanel.BringToFront();
                        this.barracksPanel.init();
                        this.currentPanelWidth = this.barracksPanel.Size.Width;
                        this.currentPanelHeight = this.barracksPanel.Size.Height;
                        break;

                    case 5:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.resourcesPanel.initProperties(true, "Resources", this);
                        this.resourcesPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.resourcesPanel.display(this, (base.Size.Width - this.resourcesPanel.Size.Width) / 2, (base.Size.Height - this.resourcesPanel.Size.Height) / 2);
                        this.resourcesPanel.BringToFront();
                        this.resourcesPanel.init();
                        this.currentPanelWidth = this.resourcesPanel.Size.Width;
                        this.currentPanelHeight = this.resourcesPanel.Size.Height;
                        break;

                    case 6:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.villageReinforcementsPanel.initProperties(true, "Reinforcements", this);
                        this.villageReinforcementsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.villageReinforcementsPanel.Size = new Size(this.villageReinforcementsPanel.Size.Width, base.Height);
                        this.villageReinforcementsPanel.display(this, (base.Size.Width - this.villageReinforcementsPanel.Size.Width) / 2, 0);
                        this.villageReinforcementsPanel.BringToFront();
                        this.villageReinforcementsPanel.setReinforcementVillage(-1);
                        this.villageReinforcementsPanel.init(false);
                        this.currentPanelWidth = this.villageReinforcementsPanel.Size.Width;
                        this.currentPanelHeight = this.villageReinforcementsPanel.Size.Height;
                        break;

                    case 7:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.villageArmiesPanel.initProperties(true, "Armies", this);
                        this.villageArmiesPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.villageArmiesPanel.Size = new Size(this.villageArmiesPanel.Size.Width, base.Height);
                        this.villageArmiesPanel.display(this, (base.Size.Width - this.villageArmiesPanel.Size.Width) / 2, 0);
                        this.villageArmiesPanel.BringToFront();
                        this.villageArmiesPanel.init(false);
                        this.currentPanelWidth = this.villageArmiesPanel.Size.Width;
                        this.currentPanelHeight = this.villageArmiesPanel.Size.Height;
                        break;

                    case 8:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.vassalControlPanel.initProperties(true, "Vassals", this);
                        this.vassalControlPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.vassalControlPanel.Size = new Size(this.vassalControlPanel.Size.Width, base.Height);
                        this.vassalControlPanel.display(this, (base.Size.Width - this.vassalControlPanel.Size.Width) / 2, 0);
                        this.vassalControlPanel.BringToFront();
                        this.vassalControlPanel.init(false);
                        this.currentPanelWidth = this.vassalControlPanel.Size.Width;
                        this.currentPanelHeight = this.vassalControlPanel.Size.Height;
                        break;

                    case 10:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.avatarEditorPanel.initProperties(true, "Avatar", this);
                        this.avatarEditorPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.avatarEditorPanel.display(this, (base.Size.Width - this.avatarEditorPanel.Size.Width) / 2, (base.Size.Height - this.avatarEditorPanel.Size.Height) / 2);
                        this.avatarEditorPanel.BringToFront();
                        this.avatarEditorPanel.init();
                        this.currentPanelWidth = this.avatarEditorPanel.Size.Width;
                        this.currentPanelHeight = this.avatarEditorPanel.Size.Height;
                        break;

                    case 11:
                    case 12:
                    case 13:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.unknownPanel.initProperties(true, "Unknown", this);
                        this.unknownPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.unknownPanel.display(this, (base.Size.Width - this.unknownPanel.Size.Width) / 2, (base.Size.Height - this.unknownPanel.Size.Height) / 2);
                        this.unknownPanel.BringToFront();
                        this.unknownPanel.init();
                        this.currentPanelWidth = this.unknownPanel.Size.Width;
                        this.currentPanelHeight = this.unknownPanel.Size.Height;
                        break;

                    case 15:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.vassalArmiesPanel.initProperties(true, "Vassal Troops", this);
                        this.vassalArmiesPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.vassalArmiesPanel.display(this, (base.Size.Width - this.vassalArmiesPanel.Size.Width) / 2, (base.Size.Height - this.vassalArmiesPanel.Size.Height) / 2);
                        this.vassalArmiesPanel.BringToFront();
                        this.vassalArmiesPanel.init(false);
                        this.currentPanelWidth = this.vassalArmiesPanel.Size.Width;
                        this.currentPanelHeight = this.vassalArmiesPanel.Size.Height;
                        break;

                    case 0x11:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.capitalSendTroopsPanel.initProperties(true, "Vassal Attacks", this);
                        this.capitalSendTroopsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.capitalSendTroopsPanel.display(this, (base.Size.Width - this.capitalSendTroopsPanel.Size.Width) / 2, (base.Size.Height - this.capitalSendTroopsPanel.Size.Height) / 2);
                        this.capitalSendTroopsPanel.BringToFront();
                        this.capitalSendTroopsPanel.init(false);
                        this.currentPanelWidth = this.capitalSendTroopsPanel.Size.Width;
                        this.currentPanelHeight = this.capitalSendTroopsPanel.Size.Height;
                        break;

                    case 0x12:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.unitsPanel.initProperties(true, "Units", this);
                        this.unitsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.unitsPanel.display(this, (base.Size.Width - this.unitsPanel.Size.Width) / 2, (base.Size.Height - this.unitsPanel.Size.Height) / 2);
                        this.unitsPanel.BringToFront();
                        this.unitsPanel.init();
                        this.currentPanelWidth = this.unitsPanel.Size.Width;
                        this.currentPanelHeight = this.unitsPanel.Size.Height;
                        break;

                    case 0x13:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.rankingsPanel.initProperties(true, "Rankings", this);
                        this.rankingsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.rankingsPanel.display(this, (base.Size.Width - this.rankingsPanel.Size.Width) / 2, (base.Size.Height - this.rankingsPanel.Size.Height) / 2);
                        this.rankingsPanel.BringToFront();
                        this.rankingsPanel.init(true);
                        this.currentPanelWidth = this.rankingsPanel.Size.Width;
                        this.currentPanelHeight = this.rankingsPanel.Size.Height;
                        break;

                    case 20:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.statsPanel.initProperties(true, "Stats", this);
                        this.statsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.statsPanel.Size = new Size(this.statsPanel.Size.Width, base.Height);
                        this.statsPanel.display(this, (base.Size.Width - this.statsPanel.Size.Width) / 2, 0);
                        this.statsPanel.BringToFront();
                        this.statsPanel.init(false);
                        this.currentPanelWidth = this.statsPanel.Size.Width;
                        this.currentPanelHeight = this.statsPanel.Size.Height;
                        break;

                    case 0x15:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.reportsPanel.initProperties(true, "Reports", this);
                        this.reportsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.reportsPanel.Size = new Size(this.reportsPanel.Size.Width, base.Height);
                        this.reportsPanel.display(this, (base.Size.Width - this.reportsPanel.Size.Width) / 2, 0);
                        this.reportsPanel.BringToFront();
                        this.reportsPanel.init(false);
                        this.currentPanelWidth = this.reportsPanel.Size.Width;
                        this.currentPanelHeight = this.reportsPanel.Size.Height;
                        break;

                    case 0x16:
                    {
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.gloryPanel.initProperties(true, "Glory", this);
                        this.gloryPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        int width = base.Width;
                        int height = base.Height;
                        if (width > 0x640)
                        {
                            width = 0x640;
                        }
                        if (height > 0x400)
                        {
                            height = 0x400;
                        }
                        this.gloryPanel.Size = new Size(width, height);
                        this.gloryPanel.display(this, (base.Size.Width - this.gloryPanel.Size.Width) / 2, (base.Size.Height - this.gloryPanel.Size.Height) / 2);
                        this.gloryPanel.BringToFront();
                        this.gloryPanel.init();
                        this.currentPanelWidth = width;
                        this.currentPanelHeight = height;
                        break;
                    }
                    case 0x17:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.allArmiesPanel.initProperties(true, "Armies", this);
                        this.allArmiesPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.allArmiesPanel.Size = new Size(this.allArmiesPanel.Size.Width, base.Height);
                        this.allArmiesPanel.display(this, (base.Size.Width - this.allArmiesPanel.Size.Width) / 2, 0);
                        this.allArmiesPanel.BringToFront();
                        this.allArmiesPanel.preInit();
                        this.allArmiesPanel.init(false, 0);
                        this.currentPanelWidth = this.allArmiesPanel.Size.Width;
                        this.currentPanelHeight = this.allArmiesPanel.Size.Height;
                        break;

                    case 0x18:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.allVassalsPanel.initProperties(true, "All Vassals", this);
                        this.allVassalsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.allVassalsPanel.Size = new Size(this.allVassalsPanel.Size.Width, base.Height);
                        this.allVassalsPanel.display(this, (base.Size.Width - this.allVassalsPanel.Size.Width) / 2, 0);
                        this.allVassalsPanel.BringToFront();
                        this.allVassalsPanel.init(false);
                        this.currentPanelWidth = this.allVassalsPanel.Size.Width;
                        this.currentPanelHeight = this.allVassalsPanel.Size.Height;
                        break;

                    case 0x19:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.questsPanel.initProperties(true, "Quests", this);
                        this.questsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.questsPanel.Size = new Size(this.questsPanel.Size.Width, base.Height);
                        this.questsPanel.display(this, (base.Size.Width - this.questsPanel.Size.Width) / 2, 0);
                        this.questsPanel.BringToFront();
                        this.questsPanel.init(false);
                        this.currentPanelWidth = this.questsPanel.Size.Width;
                        this.currentPanelHeight = this.questsPanel.Size.Height;
                        break;

                    case 0x1a:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.newQuestsPanel.initProperties(true, "New Quests", this);
                        this.newQuestsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.newQuestsPanel.Size = new Size(this.questsPanel.Size.Width, base.Height);
                        this.newQuestsPanel.display(this, (base.Size.Width - this.newQuestsPanel.Size.Width) / 2, 0);
                        this.newQuestsPanel.BringToFront();
                        this.newQuestsPanel.init(false);
                        this.currentPanelWidth = this.newQuestsPanel.Size.Width;
                        this.currentPanelHeight = this.newQuestsPanel.Size.Height;
                        break;

                    case 0x29:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionInvitePanel.initProperties(true, "Faction Invites", this);
                        this.factionInvitePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionInvitePanel.Size = new Size(this.factionInvitePanel.Size.Width, base.Height);
                        this.factionInvitePanel.display(this, (base.Size.Width - this.factionInvitePanel.Size.Width) / 2, 0);
                        this.factionInvitePanel.BringToFront();
                        this.factionInvitePanel.init(false);
                        this.currentPanelWidth = this.factionInvitePanel.Size.Width;
                        this.currentPanelHeight = this.factionInvitePanel.Size.Height;
                        break;

                    case 0x2a:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionMyFactionPanel.initProperties(true, "Faction my Faction", this);
                        this.factionMyFactionPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionMyFactionPanel.Size = new Size(this.factionMyFactionPanel.Size.Width, base.Height);
                        this.factionMyFactionPanel.display(this, (base.Size.Width - this.factionMyFactionPanel.Size.Width) / 2, 0);
                        this.factionMyFactionPanel.BringToFront();
                        this.factionMyFactionPanel.init(false);
                        this.currentPanelWidth = this.factionMyFactionPanel.Size.Width;
                        this.currentPanelHeight = this.factionMyFactionPanel.Size.Height;
                        break;

                    case 0x2b:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionAllFactionsPanel.initProperties(true, "Faction All Factions", this);
                        this.factionAllFactionsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionAllFactionsPanel.Size = new Size(this.factionAllFactionsPanel.Size.Width, base.Height);
                        this.factionAllFactionsPanel.display(this, (base.Size.Width - this.factionAllFactionsPanel.Size.Width) / 2, 0);
                        this.factionAllFactionsPanel.BringToFront();
                        this.factionAllFactionsPanel.init(false);
                        this.currentPanelWidth = this.factionAllFactionsPanel.Size.Width;
                        this.currentPanelHeight = this.factionAllFactionsPanel.Size.Height;
                        break;

                    case 0x2c:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionDiplomacyPanel.initProperties(true, "Faction Diplomacy", this);
                        this.factionDiplomacyPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionDiplomacyPanel.Size = new Size(this.factionDiplomacyPanel.Size.Width, base.Height);
                        this.factionDiplomacyPanel.display(this, (base.Size.Width - this.factionDiplomacyPanel.Size.Width) / 2, 0);
                        this.factionDiplomacyPanel.BringToFront();
                        this.factionDiplomacyPanel.init(false);
                        this.currentPanelWidth = this.factionDiplomacyPanel.Size.Width;
                        this.currentPanelHeight = this.factionDiplomacyPanel.Size.Height;
                        break;

                    case 0x2d:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionNewForumPanel.initProperties(true, "Faction Forum", this);
                        this.factionNewForumPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionNewForumPanel.Size = new Size(this.factionNewForumPanel.Size.Width, base.Height);
                        this.factionNewForumPanel.display(this, (base.Size.Width - this.factionNewForumPanel.Size.Width) / 2, 0);
                        this.factionNewForumPanel.BringToFront();
                        this.factionNewForumPanel.init(false);
                        this.currentPanelWidth = this.factionNewForumPanel.Size.Width;
                        this.currentPanelHeight = this.factionNewForumPanel.Size.Height;
                        break;

                    case 0x2e:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionOfficersPanel.initProperties(true, "Faction Officers", this);
                        this.factionOfficersPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionOfficersPanel.Size = new Size(this.factionOfficersPanel.Size.Width, base.Height);
                        this.factionOfficersPanel.display(this, (base.Size.Width - this.factionOfficersPanel.Size.Width) / 2, 0);
                        this.factionOfficersPanel.BringToFront();
                        this.factionOfficersPanel.init(false);
                        this.currentPanelWidth = this.factionOfficersPanel.Size.Width;
                        this.currentPanelHeight = this.factionOfficersPanel.Size.Height;
                        break;

                    case 0x2f:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionStartFactionPanel.initProperties(true, "Faction Start Faction", this);
                        this.factionStartFactionPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionStartFactionPanel.Size = new Size(this.factionStartFactionPanel.Size.Width, base.Height);
                        this.factionStartFactionPanel.display(this, (base.Size.Width - this.factionStartFactionPanel.Size.Width) / 2, 0);
                        this.factionStartFactionPanel.BringToFront();
                        this.factionStartFactionPanel.init(false);
                        this.currentPanelWidth = this.factionStartFactionPanel.Size.Width;
                        this.currentPanelHeight = this.factionStartFactionPanel.Size.Height;
                        break;

                    case 0x30:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.factionNewForumPostsPanel.initProperties(true, "Faction All Factions", this);
                        this.factionNewForumPostsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.factionNewForumPostsPanel.Size = new Size(this.factionNewForumPostsPanel.Size.Width, base.Height);
                        this.factionNewForumPostsPanel.display(this, (base.Size.Width - this.factionNewForumPostsPanel.Size.Width) / 2, 0);
                        this.factionNewForumPostsPanel.BringToFront();
                        this.factionNewForumPostsPanel.init(false);
                        this.currentPanelWidth = this.factionNewForumPostsPanel.Size.Width;
                        this.currentPanelHeight = this.factionNewForumPostsPanel.Size.Height;
                        break;

                    case 0x33:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.houseListPanel.initProperties(true, "House list panel", this);
                        this.houseListPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.houseListPanel.Size = new Size(this.houseListPanel.Size.Width, base.Height);
                        this.houseListPanel.display(this, (base.Size.Width - this.houseListPanel.Size.Width) / 2, 0);
                        this.houseListPanel.BringToFront();
                        this.houseListPanel.init(false);
                        this.currentPanelWidth = this.houseListPanel.Size.Width;
                        this.currentPanelHeight = this.houseListPanel.Size.Height;
                        break;

                    case 0x34:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.houseInfoPanel.initProperties(true, "House info panel", this);
                        this.houseInfoPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.houseInfoPanel.Size = new Size(this.houseInfoPanel.Size.Width, base.Height);
                        this.houseInfoPanel.display(this, (base.Size.Width - this.houseInfoPanel.Size.Width) / 2, 0);
                        this.houseInfoPanel.BringToFront();
                        this.houseInfoPanel.init(false);
                        this.currentPanelWidth = this.houseInfoPanel.Size.Width;
                        this.currentPanelHeight = this.houseInfoPanel.Size.Height;
                        break;

                    case 60:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.userDiplomacyPanel.initProperties(true, "User Diplomacy", this);
                        this.userDiplomacyPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.userDiplomacyPanel.Size = new Size(this.userDiplomacyPanel.Size.Width, base.Height);
                        this.userDiplomacyPanel.display(this, (base.Size.Width - this.userDiplomacyPanel.Size.Width) / 2, 0);
                        this.userDiplomacyPanel.BringToFront();
                        this.userDiplomacyPanel.init(false);
                        this.currentPanelWidth = this.userDiplomacyPanel.Size.Width;
                        this.currentPanelHeight = this.userDiplomacyPanel.Size.Height;
                        break;

                    case 0x63:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.userInfoScreen.initProperties(true, "User Info", this);
                        this.userInfoScreen.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.userInfoScreen.display(this, (base.Size.Width - this.userInfoScreen.Size.Width) / 2, (base.Size.Height - this.userInfoScreen.Size.Height) / 2);
                        this.userInfoScreen.BringToFront();
                        this.currentPanelWidth = this.userInfoScreen.Size.Width;
                        this.currentPanelHeight = this.userInfoScreen.Size.Height;
                        break;

                    case 100:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.allVillagesPanel.initProperties(true, "All Villages", this);
                        this.allVillagesPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.allVillagesPanel.Size = new Size(this.allVillagesPanel.Size.Width, base.Height);
                        this.allVillagesPanel.display(this, (base.Size.Width - this.allVillagesPanel.Size.Width) / 2, 0);
                        this.allVillagesPanel.BringToFront();
                        this.allVillagesPanel.init(false);
                        this.currentPanelWidth = this.allVillagesPanel.Size.Width;
                        this.currentPanelHeight = this.allVillagesPanel.Size.Height;
                        break;

                    case 0x3eb:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.capitalTradePanel.initProperties(true, "Capital Trade", this);
                        this.capitalTradePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.capitalTradePanel.display(this, (base.Size.Width - this.capitalTradePanel.Size.Width) / 2, (base.Size.Height - this.capitalTradePanel.Size.Height) / 2);
                        this.capitalTradePanel.BringToFront();
                        this.capitalTradePanel.selectStockExchange(-1);
                        this.capitalTradePanel.init();
                        this.currentPanelWidth = this.capitalTradePanel.Size.Width;
                        this.currentPanelHeight = this.capitalTradePanel.Size.Height;
                        InterfaceMgr.Instance.updateVillageInfoBar();
                        break;

                    case 0x3ec:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.capitalBarracksPanel.initProperties(true, "Mercenaries", this);
                        this.capitalBarracksPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.capitalBarracksPanel.display(this, (base.Size.Width - this.capitalBarracksPanel.Size.Width) / 2, (base.Size.Height - this.capitalBarracksPanel.Size.Height) / 2);
                        this.capitalBarracksPanel.BringToFront();
                        this.capitalBarracksPanel.init();
                        this.currentPanelWidth = this.capitalBarracksPanel.Size.Width;
                        this.currentPanelHeight = this.capitalBarracksPanel.Size.Height;
                        break;

                    case 0x3ed:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.capitalResourcesPanel.initProperties(true, "Resources", this);
                        this.capitalResourcesPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.capitalResourcesPanel.display(this, (base.Size.Width - this.capitalResourcesPanel.Size.Width) / 2, (base.Size.Height - this.capitalResourcesPanel.Size.Height) / 2);
                        this.capitalResourcesPanel.BringToFront();
                        this.capitalResourcesPanel.init();
                        this.currentPanelWidth = this.capitalResourcesPanel.Size.Width;
                        this.currentPanelHeight = this.capitalResourcesPanel.Size.Height;
                        break;

                    case 0x3ee:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.parishPanel.initProperties(true, "Parish Vote", this);
                        this.parishPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.parishPanel.Size = new Size(this.parishPanel.Size.Width, base.Height);
                        this.parishPanel.display(this, (base.Size.Width - this.parishPanel.Size.Width) / 2, 0);
                        this.parishPanel.BringToFront();
                        this.parishPanel.init(false);
                        this.currentPanelWidth = this.parishPanel.Size.Width;
                        this.currentPanelHeight = base.Height;
                        break;

                    case 0x3ef:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.parishForumPanel.initProperties(true, "Parish Forum", this);
                        this.parishForumPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, base.Height);
                        this.parishForumPanel.display(this, (base.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
                        this.parishForumPanel.BringToFront();
                        this.parishForumPanel.setArea(GameEngine.Instance.World.getParishFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 3);
                        this.parishForumPanel.init(false);
                        this.currentPanelWidth = this.parishForumPanel.Size.Width;
                        this.currentPanelHeight = this.parishForumPanel.Size.Height;
                        break;

                    case 0x3f0:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.parishFrontPagePanel.initProperties(true, "Parish Info", this);
                        this.parishFrontPagePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.parishFrontPagePanel.Size = new Size(this.parishFrontPagePanel.Size.Width, base.Height);
                        this.parishFrontPagePanel.display(this, (base.Size.Width - this.parishFrontPagePanel.Size.Width) / 2, 0);
                        this.parishFrontPagePanel.BringToFront();
                        this.parishFrontPagePanel.init(false);
                        this.currentPanelWidth = this.parishFrontPagePanel.Size.Width;
                        this.currentPanelHeight = base.Height;
                        break;

                    case 0x3f1:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.capitalForumPostsPanel.initProperties(true, "Forum Post Info", this);
                        this.capitalForumPostsPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.capitalForumPostsPanel.Size = new Size(this.capitalForumPostsPanel.Size.Width, base.Height);
                        this.capitalForumPostsPanel.display(this, (base.Size.Width - this.capitalForumPostsPanel.Size.Width) / 2, 0);
                        this.capitalForumPostsPanel.BringToFront();
                        this.capitalForumPostsPanel.init(false);
                        this.currentPanelWidth = this.capitalForumPostsPanel.Size.Width;
                        this.currentPanelHeight = base.Height;
                        break;

                    case 0x452:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.countyPanel.initProperties(true, "County Vote", this);
                        this.countyPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.countyPanel.Size = new Size(this.countyPanel.Size.Width, base.Height);
                        this.countyPanel.display(this, (base.Size.Width - this.countyPanel.Size.Width) / 2, 0);
                        this.countyPanel.BringToFront();
                        this.countyPanel.init(false);
                        this.currentPanelWidth = this.countyPanel.Size.Width;
                        this.currentPanelHeight = base.Height;
                        break;

                    case 0x453:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.parishForumPanel.initProperties(true, "County Forum", this);
                        this.parishForumPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, base.Height);
                        this.parishForumPanel.display(this, (base.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
                        this.parishForumPanel.BringToFront();
                        this.parishForumPanel.setArea(GameEngine.Instance.World.getCountyFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 2);
                        this.parishForumPanel.init(false);
                        this.currentPanelWidth = this.parishForumPanel.Size.Width;
                        this.currentPanelHeight = this.parishForumPanel.Size.Height;
                        break;

                    case 0x454:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.countyFrontPagePanel.initProperties(true, "County Info", this);
                        this.countyFrontPagePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.countyFrontPagePanel.display(this, (base.Size.Width - this.countyFrontPagePanel.Size.Width) / 2, (base.Size.Height - this.countyFrontPagePanel.Size.Height) / 2);
                        this.countyFrontPagePanel.BringToFront();
                        this.countyFrontPagePanel.init();
                        this.currentPanelWidth = this.countyFrontPagePanel.Size.Width;
                        this.currentPanelHeight = this.countyFrontPagePanel.Size.Height;
                        break;

                    case 0x3fd:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.capitalDonateResourcesPanel.initProperties(true, "Parish Donate Resources", this);
                        this.capitalDonateResourcesPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.capitalDonateResourcesPanel.display(this, (base.Size.Width - this.capitalDonateResourcesPanel.Size.Width) / 2, (base.Size.Height - this.capitalDonateResourcesPanel.Size.Height) / 2);
                        this.capitalDonateResourcesPanel.BringToFront();
                        this.capitalDonateResourcesPanel.init();
                        this.currentPanelWidth = this.capitalDonateResourcesPanel.Size.Width;
                        this.currentPanelHeight = this.capitalDonateResourcesPanel.Size.Height;
                        break;

                    case 0x4b6:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.provincePanel.initProperties(true, "Province Vote", this);
                        this.provincePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.provincePanel.Size = new Size(this.countyPanel.Size.Width, base.Height);
                        this.provincePanel.display(this, (base.Size.Width - this.provincePanel.Size.Width) / 2, 0);
                        this.provincePanel.BringToFront();
                        this.provincePanel.init(false);
                        this.currentPanelWidth = this.provincePanel.Size.Width;
                        this.currentPanelHeight = base.Height;
                        break;

                    case 0x4b7:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.parishForumPanel.initProperties(true, "Province Forum", this);
                        this.parishForumPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, base.Height);
                        this.parishForumPanel.display(this, (base.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
                        this.parishForumPanel.BringToFront();
                        this.parishForumPanel.setArea(GameEngine.Instance.World.getProvinceFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 1);
                        this.parishForumPanel.init(false);
                        this.currentPanelWidth = this.parishForumPanel.Size.Width;
                        this.currentPanelHeight = this.parishForumPanel.Size.Height;
                        break;

                    case 0x4b8:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.provinceFrontPagePanel.initProperties(true, "Province Info", this);
                        this.provinceFrontPagePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.provinceFrontPagePanel.display(this, (base.Size.Width - this.provinceFrontPagePanel.Size.Width) / 2, (base.Size.Height - this.provinceFrontPagePanel.Size.Height) / 2);
                        this.provinceFrontPagePanel.BringToFront();
                        this.provinceFrontPagePanel.init();
                        this.currentPanelWidth = this.provinceFrontPagePanel.Size.Width;
                        this.currentPanelHeight = this.provinceFrontPagePanel.Size.Height;
                        break;

                    case 0x51a:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.countryPanel.initProperties(true, "Country Vote", this);
                        this.countryPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.countryPanel.Size = new Size(this.countyPanel.Size.Width, base.Height);
                        this.countryPanel.display(this, (base.Size.Width - this.countryPanel.Size.Width) / 2, 0);
                        this.countryPanel.BringToFront();
                        this.countryPanel.init(false);
                        this.currentPanelWidth = this.countryPanel.Size.Width;
                        this.currentPanelHeight = base.Height;
                        break;

                    case 0x51b:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.parishForumPanel.initProperties(true, "Country Forum", this);
                        this.parishForumPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, base.Height);
                        this.parishForumPanel.display(this, (base.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
                        this.parishForumPanel.BringToFront();
                        this.parishForumPanel.setArea(GameEngine.Instance.World.getCountryFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 0);
                        this.parishForumPanel.init(false);
                        this.currentPanelWidth = this.parishForumPanel.Size.Width;
                        this.currentPanelHeight = this.parishForumPanel.Size.Height;
                        break;

                    case 0x51c:
                        this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
                        this.countryFrontPagePanel.initProperties(true, "Country Info", this);
                        this.countryFrontPagePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                        this.countryFrontPagePanel.display(this, (base.Size.Width - this.countryFrontPagePanel.Size.Width) / 2, (base.Size.Height - this.countryFrontPagePanel.Size.Height) / 2);
                        this.countryFrontPagePanel.BringToFront();
                        this.countryFrontPagePanel.init();
                        this.currentPanelWidth = this.countryFrontPagePanel.Size.Width;
                        this.currentPanelHeight = this.countryFrontPagePanel.Size.Height;
                        break;
                }
                this.currentPanelWidth -= 0xa8;
                this.currentPanelHeight -= 0xa8;
                this.forceBackgroundRedraw = true;
                this.OnPaint(null);
            }
        }

        public void tradeWithResume(int villageID, bool keepInfo)
        {
            this.marketTransferPanel.resume(villageID, keepInfo);
        }

        public void update(int panelID)
        {
            switch (panelID)
            {
                case 0x4b6:
                    this.provincePanel.update();
                    return;

                case 0x4b7:
                case 0x51b:
                case 0x453:
                case 0x3ef:
                    this.parishForumPanel.update();
                    return;

                case 0x4b8:
                    this.provinceFrontPagePanel.update();
                    return;

                case 0x51a:
                    this.countryPanel.update();
                    return;

                case 0x51c:
                    this.countryFrontPagePanel.update();
                    break;

                case 0x452:
                    this.countyPanel.update();
                    return;

                case 0x454:
                    this.countyFrontPagePanel.update();
                    return;

                case 0x3fd:
                    this.capitalDonateResourcesPanel.update();
                    return;

                case 0:
                case 9:
                case 14:
                case 0x10:
                case 0x1b:
                case 0x1c:
                case 0x1d:
                case 30:
                case 0x1f:
                case 0x20:
                case 0x21:
                case 0x22:
                case 0x23:
                case 0x24:
                case 0x25:
                case 0x26:
                case 0x27:
                case 40:
                case 0x31:
                case 50:
                case 0x35:
                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                case 0x3a:
                case 0x3b:
                    break;

                case 1:
                    this.holdBanquetPanel.update();
                    return;

                case 2:
                    this.marketTransferPanel.update();
                    return;

                case 3:
                    this.stockExchangePanel.update();
                    return;

                case 4:
                    this.barracksPanel.update();
                    return;

                case 5:
                    this.resourcesPanel.update();
                    return;

                case 6:
                    this.villageReinforcementsPanel.update();
                    return;

                case 7:
                    this.villageArmiesPanel.update();
                    return;

                case 8:
                    this.vassalControlPanel.update();
                    return;

                case 10:
                    this.avatarEditorPanel.update();
                    return;

                case 11:
                case 12:
                case 13:
                    this.unknownPanel.update();
                    return;

                case 15:
                    this.vassalArmiesPanel.update();
                    return;

                case 0x11:
                    this.capitalSendTroopsPanel.update();
                    return;

                case 0x12:
                    this.unitsPanel.update();
                    return;

                case 0x13:
                    this.rankingsPanel.update();
                    return;

                case 20:
                    this.statsPanel.update();
                    return;

                case 0x15:
                    this.reportsPanel.update();
                    return;

                case 0x16:
                    this.gloryPanel.update();
                    return;

                case 0x17:
                    this.allArmiesPanel.update();
                    return;

                case 0x18:
                    this.allVassalsPanel.update();
                    return;

                case 0x19:
                    this.questsPanel.update();
                    return;

                case 0x1a:
                    this.newQuestsPanel.update();
                    return;

                case 0x29:
                    this.factionInvitePanel.update();
                    return;

                case 0x2a:
                    this.factionMyFactionPanel.update();
                    return;

                case 0x2b:
                    this.factionAllFactionsPanel.update();
                    return;

                case 0x2c:
                    this.factionDiplomacyPanel.update();
                    return;

                case 0x2d:
                    this.factionNewForumPanel.update();
                    return;

                case 0x2e:
                    this.factionOfficersPanel.update();
                    return;

                case 0x2f:
                    this.factionStartFactionPanel.update();
                    return;

                case 0x30:
                    this.factionNewForumPostsPanel.update();
                    return;

                case 0x33:
                    this.houseListPanel.update();
                    return;

                case 0x34:
                    this.houseInfoPanel.update();
                    return;

                case 60:
                    this.userDiplomacyPanel.update();
                    return;

                case 0x63:
                    this.userInfoScreen.update();
                    return;

                case 100:
                    this.allVillagesPanel.update();
                    return;

                case 0x3eb:
                    this.capitalTradePanel.update();
                    return;

                case 0x3ec:
                    this.capitalBarracksPanel.update();
                    return;

                case 0x3ed:
                    this.capitalResourcesPanel.update();
                    return;

                case 0x3ee:
                    this.parishPanel.update();
                    return;

                case 0x3f0:
                    this.parishFrontPagePanel.update();
                    return;

                case 0x3f1:
                    this.capitalForumPostsPanel.update();
                    return;

                default:
                    return;
            }
        }

        public bool wasShowingVassalSendScreen()
        {
            return (this.lastPanelType == 15);
        }
    }
}

