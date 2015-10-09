namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ParishCapitalVillagePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDImage backImage;
        private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private int lastPlague = -100;
        private CustomSelfDrawPanel.CSDLabel lblPlagueValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton leftButton = new CustomSelfDrawPanel.CSDButton();
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDButton mapEdit = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
        private int numInfos;
        private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
        private int selectedProtection;
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();

        public ParishCapitalVillagePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void btnAttack_Click()
        {
            GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.m_selectedVillage);
        }

        private void btnScout_Click()
        {
            if (this.m_selectedVillage >= 0)
            {
                InterfaceMgr.Instance.openScoutPopupWindow(this.m_selectedVillage, true);
            }
        }

        private void btnSendCourtiers_Click()
        {
            if (this.m_selectedVillage >= 0)
            {
                InterfaceMgr.Instance.showParishPanel(0);
            }
        }

        private void btnSendMonks_Click()
        {
            if (this.m_selectedVillage >= 0)
            {
                InterfaceMgr.Instance.openSendMonkWindow(this.m_selectedVillage);
            }
        }

        private void btnSendTroops_Click()
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.setCapitalSendTargetVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.setVillageTabSubMode(0x11);
        }

        private void btnTradeWith_Click()
        {
            if (this.m_selectedVillage >= 0)
            {
                InterfaceMgr.Instance.selectStockExchange(-1);
                GameEngine.Instance.SkipVillageTab();
                InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                InterfaceMgr.Instance.setVillageTabSubMode(3);
                InterfaceMgr.Instance.resetVillageReportPanelData();
                InterfaceMgr.Instance.selectStockExchange(this.m_selectedVillage);
            }
        }

        private void castleClick()
        {
            if (!GameEngine.Instance.World.doesUserHaveVillageInParishByCapital(this.m_selectedVillage))
            {
                RemoteServices.Instance.set_ViewCastle_UserCallBack(new RemoteServices.ViewCastle_UserCallBack(this.viewCastleCallback));
                RemoteServices.Instance.ViewCastle_Village(this.m_selectedVillage);
            }
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
            this.tradeButton.Position = new Point(10, 0x31);
            this.tradeButton.Enabled = false;
            this.tradeButton.CustomTooltipID = 0x96a;
            this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnTradeWith_Click), "ParishCapitalVillagePanel2_trade");
            this.backImage.addControl(this.tradeButton);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton.Position = new Point(0x2d, 0x31);
            this.attackButton.Enabled = false;
            this.attackButton.CustomTooltipID = 0x96b;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "ParishCapitalVillagePanel2_attack");
            this.backImage.addControl(this.attackButton);
            this.scoutButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.scoutButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
            this.scoutButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x11];
            this.scoutButton.Position = new Point(80, 0x31);
            this.scoutButton.Enabled = false;
            this.scoutButton.CustomTooltipID = 0x96c;
            this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "ParishCapitalVillagePanel2_scout");
            this.backImage.addControl(this.scoutButton);
            this.reinforceButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
            this.reinforceButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[9];
            this.reinforceButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x10];
            this.reinforceButton.Position = new Point(0x73, 0x31);
            this.reinforceButton.Enabled = false;
            this.reinforceButton.CustomTooltipID = 0x96d;
            this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendTroops_Click), "ParishCapitalVillagePanel2_reinforce");
            this.backImage.addControl(this.reinforceButton);
            this.monkButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
            this.monkButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[11];
            this.monkButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x12];
            this.monkButton.Position = new Point(150, 0x31);
            this.monkButton.Enabled = false;
            this.monkButton.CustomTooltipID = 0x96e;
            this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendMonks_Click), "ParishCapitalVillagePanel2_sendmonks");
            this.backImage.addControl(this.monkButton);
            this.castleButton.ImageNorm = (Image) GFXLibrary.mrhp_reports;
            this.castleButton.OverBrighten = true;
            this.castleButton.MoveOnClick = true;
            this.castleButton.Position = new Point(0x52, 0x70);
            this.castleButton.CustomTooltipID = 0x98d;
            this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "ParishCapitalVillagePanel2_view_castle");
            this.backImage.addControl(this.castleButton);
            if (GameEngine.Instance.World.MapEditing)
            {
                this.mapEdit.ImageNorm = (Image) GFXLibrary.faction_pen;
                this.mapEdit.ImageOver = (Image) GFXLibrary.faction_pen;
                this.mapEdit.ImageClick = (Image) GFXLibrary.faction_pen;
                this.mapEdit.MoveOnClick = true;
                this.mapEdit.OverBrighten = true;
                this.mapEdit.Position = new Point(0xa8, 0x70);
                this.mapEdit.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mapEditClicked));
                this.backImage.addControl(this.mapEdit);
            }
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
            this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "ParishCapitalVillagePanel2_protection_left");
            this.leftButton.Visible = false;
            this.backImage.addControl(this.leftButton);
            this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
            this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
            this.rightButton.Position = new Point(170, 50);
            this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "ParishCapitalVillagePanel2_protection_right");
            this.rightButton.Visible = false;
            this.backImage.addControl(this.rightButton);
            this.lblPlagueValue.Text = "";
            this.lblPlagueValue.Color = ARGBColors.Black;
            this.lblPlagueValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.lblPlagueValue.Position = new Point(0x52, 10);
            this.lblPlagueValue.Size = new Size(0x30, 0x16);
            this.lblPlagueValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.backImage.addControl(this.lblPlagueValue);
            this.lastPlague = -100;
            this.numInfos = 0;
            this.selectedProtection = 0;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "ParishCapitalVillagePanel2";
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

        private void mapEditClicked()
        {
            RenameVillagePopup popup = new RenameVillagePopup();
            popup.setParishVillageID(this.m_selectedVillage, GameEngine.Instance.World.getVillageName(this.m_selectedVillage));
            popup.Show(InterfaceMgr.Instance.ParentForm);
        }

        public void update()
        {
            this.backGround.update();
            int[] numArray = new int[3];
            TimeSpan[] spanArray = new TimeSpan[3];
            int numInfos = this.numInfos;
            this.numInfos = 0;
            int tooltipData = GameEngine.Instance.World.getParishPlagueLevel(this.m_selectedVillage);
            if (tooltipData != this.lastPlague)
            {
                if (tooltipData <= 0)
                {
                    this.backGround.updatePanelType(0x5dc);
                    this.lblPlagueValue.TextDiffOnly = "";
                }
                else if (this.lastPlague <= 0)
                {
                    this.backGround.updatePanelType(0x5e0);
                    this.lblPlagueValue.TextDiffOnly = tooltipData.ToString();
                }
                this.backGround.setTooltipData(tooltipData);
                this.lastPlague = tooltipData;
            }
            bool visible = this.lblProtectionType.Visible;
            int num3 = 0;
            TimeSpan span = new TimeSpan();
            if (GameEngine.Instance.World.isVillageInterdictProtected(this.m_selectedVillage))
            {
                DateTime time = GameEngine.Instance.World.getInterdictTime(this.m_selectedVillage);
                DateTime time2 = VillageMap.getCurrentServerTime();
                span = (TimeSpan) (time - time2);
                num3 = 1;
                spanArray[this.numInfos] = span;
                numArray[this.numInfos] = num3;
                this.numInfos++;
            }
            if (GameEngine.Instance.World.isVillagePeaceTimeProtected(this.m_selectedVillage))
            {
                DateTime time3 = GameEngine.Instance.World.getPeaceTime(this.m_selectedVillage);
                DateTime time4 = VillageMap.getCurrentServerTime();
                TimeSpan span2 = (TimeSpan) (time3 - time4);
                if (span2 > span)
                {
                    span = span2;
                    num3 = 2;
                    spanArray[this.numInfos] = span;
                    numArray[this.numInfos] = num3;
                    this.numInfos++;
                }
            }
            if (this.numInfos > 0)
            {
                if (this.selectedProtection < this.numInfos)
                {
                    num3 = numArray[(this.numInfos - 1) - this.selectedProtection];
                    span = spanArray[(this.numInfos - 1) - this.selectedProtection];
                }
                else
                {
                    this.selectedProtection = 0;
                }
            }
            switch (num3)
            {
                case 1:
                {
                    int totalSeconds = (int) span.TotalSeconds;
                    string str = VillageMap.createBuildTimeStringFull(totalSeconds);
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + str;
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
                    this.lblProtectionType.Visible = true;
                    break;
                }
                case 2:
                {
                    int secsLeft = (int) span.TotalSeconds;
                    string str2 = VillageMap.createBuildTimeStringFull(secsLeft);
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + str2;
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Peace", "Peace");
                    this.lblProtectionType.Visible = true;
                    break;
                }
                default:
                    this.lblProtected.TextDiffOnly = "";
                    this.lblProtectionType.TextDiffOnly = "";
                    this.lblProtectionType.Visible = false;
                    break;
            }
            if (visible != this.lblProtectionType.Visible)
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

        public void updateParishCapitalVillageText(int selectedVillage, int ownVillage)
        {
            bool flag = true;
            this.m_selectedVillage = selectedVillage;
            this.lastPlague = -100;
            this.lblPlagueValue.TextDiffOnly = "";
            this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
            this.backGround.updatePanelTypeFromVillageID(selectedVillage);
            this.backGround.setActionFromVillage(ownVillage, selectedVillage);
            if ((ownVillage < 0) || !GameEngine.Instance.World.isUserVillage(ownVillage))
            {
                this.scoutButton.Enabled = false;
                this.tradeButton.Enabled = false;
                this.attackButton.Enabled = false;
                this.monkButton.Enabled = false;
                this.reinforceButton.Enabled = false;
            }
            else
            {
                this.scoutButton.Enabled = true;
                this.tradeButton.Enabled = true;
                this.attackButton.Enabled = true;
                this.monkButton.Enabled = true;
                this.reinforceButton.Enabled = true;
                if (GameEngine.Instance.World.isCapital(ownVillage))
                {
                    this.scoutButton.Enabled = false;
                    this.tradeButton.Enabled = false;
                    this.reinforceButton.Enabled = false;
                    this.monkButton.Enabled = false;
                    flag = false;
                }
                else
                {
                    this.scoutButton.Enabled = true;
                }
                if ((selectedVillage < 0) || (ownVillage < 0))
                {
                    this.tradeButton.Enabled = false;
                }
                else
                {
                    if (!GameEngine.Instance.World.allowExchangeTrade(selectedVillage, ownVillage))
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        this.tradeButton.Enabled = true;
                    }
                    else
                    {
                        this.tradeButton.Enabled = false;
                    }
                }
            }
            if (!GameEngine.Instance.World.doesUserHaveVillageInParishByCapital(this.m_selectedVillage))
            {
                this.castleButton.Visible = true;
            }
            else
            {
                this.castleButton.Visible = false;
            }
            this.updateSize();
            this.update();
        }

        private void updateSize()
        {
            bool visible = this.lblProtectionType.Visible;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
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
            if (!this.castleButton.Visible)
            {
                num3 = -15;
            }
            this.tradeButton.Position = new Point(10, (0x8e + num) + num3);
            this.attackButton.Position = new Point(0x2d, (0x8e + num) + num3);
            this.scoutButton.Position = new Point(80, (0x8e + num) + num3);
            this.reinforceButton.Position = new Point(0x73, (0x8e + num) + num3);
            this.monkButton.Position = new Point(150, (0x8e + num) + num3);
            this.castleButton.Position = new Point(0x52, (0x70 + num) + num2);
            this.mapEdit.Position = new Point(0xa8, (0x70 + num) + num2);
            this.backGround.invalidate();
        }

        public void viewCastleCallback(ViewCastle_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.closeControl(true);
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
                GameEngine.Instance.InitCastleView(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, 0, returnData.defencesLevel, returnData.villageID, returnData.landType);
                CastleMapBattlePanel2.fromWorld();
                InterfaceMgr.Instance.castleBattleTimes(returnData.lastCastleTime, returnData.lastTroopTime);
            }
        }
    }
}

