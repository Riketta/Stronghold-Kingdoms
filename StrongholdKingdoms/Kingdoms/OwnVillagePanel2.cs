namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class OwnVillagePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDImage backImage;
        private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton leftButton = new CustomSelfDrawPanel.CSDButton();
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
        private int numInfos;
        private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton resourcesButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
        private int selectedProtection;
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopsButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageButton = new CustomSelfDrawPanel.CSDButton();

        public OwnVillagePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void btnK_Click()
        {
        }

        private void btnScouts_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-7);
        }

        private void btnSendOutResources_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-3);
        }

        private void btnSendReinforcements_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-11);
        }

        private void btnSendSpies_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-14);
        }

        private void btnSendTroops_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-5);
        }

        private void castleClick()
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
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

        private void imgCastle_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.getVillageTabBar().changeTab(1);
        }

        private void imgResources_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.setVillageTabSubMode(5);
        }

        private void imgTroops_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.setVillageTabSubMode(4);
        }

        private void imgVillage_Click(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
        }

        private void infoLeft()
        {
            this.selectedProtection--;
            if (this.selectedProtection < 0)
            {
                this.selectedProtection = this.numInfos - 1;
            }
        }

        private void infoRight()
        {
            this.selectedProtection++;
            if (this.selectedProtection >= this.numInfos)
            {
                this.selectedProtection = 0;
            }
        }

        public void init()
        {
            base.clearControls();
            this.backImage = this.backGround.init(true, 0x2710);
            base.addControl(this.backGround);
            this.tradeButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
            this.tradeButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[7];
            this.tradeButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[14];
            this.tradeButton.Position = new Point(10, 0x8e);
            this.tradeButton.CustomTooltipID = 0x97f;
            this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendResources), "OwnVillagePanel2_trade");
            this.backImage.addControl(this.tradeButton);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton.Position = new Point(0x2d, 0x8e);
            this.attackButton.CustomTooltipID = 0x980;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendTroops), "OwnVillagePanel2_attack");
            this.backImage.addControl(this.attackButton);
            this.scoutButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.scoutButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
            this.scoutButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x11];
            this.scoutButton.Position = new Point(80, 0x8e);
            this.scoutButton.CustomTooltipID = 0x981;
            this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendScouts), "OwnVillagePanel2_scouts");
            this.backImage.addControl(this.scoutButton);
            this.reinforceButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
            this.reinforceButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[9];
            this.reinforceButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x10];
            this.reinforceButton.Position = new Point(0x73, 0x8e);
            this.reinforceButton.CustomTooltipID = 0x982;
            this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendReinforcements), "OwnVillagePanel2_reinforcements");
            this.backImage.addControl(this.reinforceButton);
            this.monkButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
            this.monkButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[11];
            this.monkButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x12];
            this.monkButton.Position = new Point(150, 0x8e);
            this.monkButton.CustomTooltipID = 0x983;
            this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendMonks), "OwnVillagePanel2_monks");
            this.backImage.addControl(this.monkButton);
            this.villageButton.ImageNorm = (Image) GFXLibrary.int_world_icon_village;
            this.villageButton.OverBrighten = true;
            this.villageButton.MoveOnClick = true;
            this.villageButton.Position = new Point(0x1d, 0x70);
            this.villageButton.CustomTooltipID = 0x985;
            this.villageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClick), "OwnVillagePanel2_village");
            this.backImage.addControl(this.villageButton);
            this.castleButton.ImageNorm = (Image) GFXLibrary.int_world_icon_castle;
            this.castleButton.OverBrighten = true;
            this.castleButton.MoveOnClick = true;
            this.castleButton.Position = new Point(0x40, 0x70);
            this.castleButton.CustomTooltipID = 0x986;
            this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "OwnVillagePanel2_castle");
            this.backImage.addControl(this.castleButton);
            this.resourcesButton.ImageNorm = (Image) GFXLibrary.int_world_icon_resource;
            this.resourcesButton.OverBrighten = true;
            this.resourcesButton.MoveOnClick = true;
            this.resourcesButton.Position = new Point(0x63, 0x70);
            this.resourcesButton.CustomTooltipID = 0x987;
            this.resourcesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourcesClick), "OwnVillagePanel2_resources");
            this.backImage.addControl(this.resourcesButton);
            this.troopsButton.ImageNorm = (Image) GFXLibrary.int_world_icon_troops;
            this.troopsButton.OverBrighten = true;
            this.troopsButton.MoveOnClick = true;
            this.troopsButton.Position = new Point(0x86, 0x70);
            this.troopsButton.CustomTooltipID = 0x988;
            this.troopsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopsClick), "OwnVillagePanel2_troops");
            this.backImage.addControl(this.troopsButton);
            this.lblProtectionType.Text = "";
            this.lblProtectionType.Color = ARGBColors.Black;
            this.lblProtectionType.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.lblProtectionType.Position = new Point(0, 0x26);
            this.lblProtectionType.Size = new Size(this.backImage.Width, 0x17);
            this.lblProtectionType.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backImage.addControl(this.lblProtectionType);
            this.lblProtected.Text = "";
            this.lblProtected.Color = ARGBColors.Black;
            this.lblProtected.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.lblProtected.Position = new Point(6, 0x30);
            this.lblProtected.Size = new Size(this.backImage.Width - 12, 0x4a);
            this.lblProtected.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backImage.addControl(this.lblProtected);
            this.leftButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_left_norm;
            this.leftButton.ImageOver = (Image) GFXLibrary.r_arrow_small_left_over;
            this.leftButton.Position = new Point(5, 50);
            this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "OwnVillagePanel2_protection_left");
            this.leftButton.Visible = false;
            this.backImage.addControl(this.leftButton);
            this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
            this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
            this.rightButton.Position = new Point(170, 50);
            this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "OwnVillagePanel2_protection_right");
            this.rightButton.Visible = false;
            this.backImage.addControl(this.rightButton);
            this.updateSize();
            this.numInfos = 0;
            this.selectedProtection = 0;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "OwnVillagePanel2";
            base.Size = new Size(0xc7, 0xd5);
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

        private void lblVillageName_Click(object sender, EventArgs e)
        {
            if (this.m_selectedVillage >= 0)
            {
                GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            }
        }

        private void resourcesClick()
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.setVillageTabSubMode(5);
        }

        private void sendMonks()
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-14);
        }

        private void sendReinforcements()
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-11);
        }

        private void sendResources()
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-3);
        }

        private void sendScouts()
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-7);
        }

        private void sendTroops()
        {
            GameEngine.Instance.World.zoomToVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.resetVillageReportPanelData();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(-5);
        }

        private void troopsClick()
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.setVillageTabSubMode(4);
        }

        public void update()
        {
            this.backGround.update();
            int[] numArray = new int[3];
            TimeSpan[] spanArray = new TimeSpan[3];
            int numInfos = this.numInfos;
            this.numInfos = 0;
            bool visible = this.lblProtected.Visible;
            int num2 = 0;
            TimeSpan span = new TimeSpan();
            if (GameEngine.Instance.World.isVillageExcommunicated(this.m_selectedVillage))
            {
                DateTime time = GameEngine.Instance.World.getExcommunicationTime(this.m_selectedVillage);
                DateTime time2 = VillageMap.getCurrentServerTime();
                span = (TimeSpan) (time - time2);
                num2 = 3;
                spanArray[this.numInfos] = span;
                numArray[this.numInfos] = num2;
                this.numInfos++;
            }
            if (GameEngine.Instance.World.isVillageInterdictProtected(this.m_selectedVillage))
            {
                DateTime time3 = GameEngine.Instance.World.getInterdictTime(this.m_selectedVillage);
                DateTime time4 = VillageMap.getCurrentServerTime();
                span = (TimeSpan) (time3 - time4);
                num2 = 1;
                spanArray[this.numInfos] = span;
                numArray[this.numInfos] = num2;
                this.numInfos++;
            }
            if (GameEngine.Instance.World.isVillagePeaceTimeProtected(this.m_selectedVillage))
            {
                DateTime time5 = GameEngine.Instance.World.getPeaceTime(this.m_selectedVillage);
                DateTime time6 = VillageMap.getCurrentServerTime();
                TimeSpan span2 = (TimeSpan) (time5 - time6);
                if (span2 > span)
                {
                    span = span2;
                    num2 = 2;
                }
                spanArray[this.numInfos] = span2;
                numArray[this.numInfos] = 2;
                this.numInfos++;
            }
            if (GameEngine.Instance.World.isVillageVacationProtected(this.m_selectedVillage))
            {
                num2 = 4;
                numArray[this.numInfos] = 4;
                this.numInfos++;
            }
            if (this.numInfos > 0)
            {
                if (this.selectedProtection < this.numInfos)
                {
                    num2 = numArray[(this.numInfos - 1) - this.selectedProtection];
                    span = spanArray[(this.numInfos - 1) - this.selectedProtection];
                }
                else
                {
                    this.selectedProtection = 0;
                }
            }
            switch (num2)
            {
                case 1:
                {
                    int totalSeconds = (int) span.TotalSeconds;
                    string str = VillageMap.createBuildTimeStringFull(totalSeconds);
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + str;
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
                    this.lblProtectionType.Visible = true;
                    this.lblProtected.Visible = true;
                    break;
                }
                case 2:
                {
                    int secsLeft = (int) span.TotalSeconds;
                    string str2 = VillageMap.createBuildTimeStringFull(secsLeft);
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + str2;
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Peace", "Peace");
                    this.lblProtectionType.Visible = true;
                    this.lblProtected.Visible = true;
                    break;
                }
                case 3:
                {
                    int num5 = (int) span.TotalSeconds;
                    string str3 = VillageMap.createBuildTimeStringFull(num5);
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Excom_For_X_Time", "No Monks for") + " : " + str3;
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Excom", "Excommunicated");
                    this.lblProtectionType.Visible = true;
                    this.lblProtected.Visible = true;
                    break;
                }
                case 4:
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked", "Cannot be attacked");
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Vacation", "Vacation Mode");
                    this.lblProtectionType.Visible = true;
                    this.lblProtected.Visible = true;
                    break;

                default:
                    this.lblProtectionType.Visible = false;
                    this.lblProtected.Visible = false;
                    break;
            }
            if (visible != this.lblProtected.Visible)
            {
                this.updateSize();
                if (!visible)
                {
                    this.selectedProtection = 0;
                }
            }
            if (numInfos != this.numInfos)
            {
                if (this.numInfos >= 2)
                {
                    this.leftButton.Visible = true;
                    this.rightButton.Visible = true;
                }
                else
                {
                    this.leftButton.Visible = false;
                    this.rightButton.Visible = false;
                }
            }
        }

        public void updateOwnVillageText(int selectedVillage)
        {
            this.m_selectedVillage = selectedVillage;
            this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
            this.backGround.updatePanelTypeFromVillageID(selectedVillage);
            this.backGround.setActionFromVillage(selectedVillage, -1);
            this.updateSize();
            this.update();
        }

        private void updateSize()
        {
            bool visible = this.lblProtected.Visible;
            int num = 0;
            int num2 = 0;
            if (!visible)
            {
                this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_132;
                num = -63;
                num2 = -4;
            }
            else
            {
                this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_192;
            }
            this.tradeButton.Position = new Point(10, 0x8e + num);
            this.attackButton.Position = new Point(0x2d, 0x8e + num);
            this.scoutButton.Position = new Point(80, 0x8e + num);
            this.reinforceButton.Position = new Point(0x73, 0x8e + num);
            this.monkButton.Position = new Point(150, 0x8e + num);
            this.villageButton.Position = new Point(0x1d, (0x70 + num) + num2);
            this.castleButton.Position = new Point(0x40, (0x70 + num) + num2);
            this.resourcesButton.Position = new Point(0x63, (0x70 + num) + num2);
            this.troopsButton.Position = new Point(0x86, (0x70 + num) + num2);
            this.backGround.invalidate();
        }

        private void villageClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
        }
    }
}

