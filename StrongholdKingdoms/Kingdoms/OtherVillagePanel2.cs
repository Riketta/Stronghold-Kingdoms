namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class OtherVillagePanel2 : CustomSelfDrawPanel, IDockableControl
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
        private MyMessageBoxPopUp PopUpRef;
        private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton renameButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
        private int selectedProtection;
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton vassalButton = new CustomSelfDrawPanel.CSDButton();

        public OtherVillagePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void castleClick()
        {
            RemoteServices.Instance.set_ViewCastle_UserCallBack(new RemoteServices.ViewCastle_UserCallBack(this.viewCastleCallback));
            RemoteServices.Instance.ViewCastle_Village(this.m_selectedVillage);
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

        public void forceDisable()
        {
            this.attackButton.Enabled = false;
            this.vassalButton.Enabled = false;
            this.reinforceButton.Enabled = false;
            this.scoutButton.Enabled = false;
            this.monkButton.Enabled = false;
            this.tradeButton.Enabled = false;
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
            this.tradeButton.CustomTooltipID = 0x96a;
            this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendResources), "OtherVillagePanel2_trade");
            this.backImage.addControl(this.tradeButton);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton.Position = new Point(0x2d, 0x8e);
            this.attackButton.CustomTooltipID = 0x96b;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendTroops), "OtherVillagePanel2_attack");
            this.backImage.addControl(this.attackButton);
            this.scoutButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.scoutButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
            this.scoutButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x11];
            this.scoutButton.Position = new Point(80, 0x8e);
            this.scoutButton.CustomTooltipID = 0x96c;
            this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendScouts), "OtherVillagePanel2_scout");
            this.backImage.addControl(this.scoutButton);
            this.reinforceButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
            this.reinforceButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[9];
            this.reinforceButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x10];
            this.reinforceButton.Position = new Point(0x73, 0x8e);
            this.reinforceButton.CustomTooltipID = 0x96d;
            this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendReinforcements), "OtherVillagePanel2_reinforce");
            this.backImage.addControl(this.reinforceButton);
            this.monkButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
            this.monkButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[11];
            this.monkButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x12];
            this.monkButton.Position = new Point(150, 0x8e);
            this.monkButton.CustomTooltipID = 0x96e;
            this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendMonks), "OtherVillagePanel2_sendmonks");
            this.backImage.addControl(this.monkButton);
            this.vassalButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
            this.vassalButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[12];
            this.vassalButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x13];
            this.vassalButton.Position = new Point(0x73, 0x70);
            this.vassalButton.CustomTooltipID = 0x98e;
            this.vassalButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.vassalClick), "OtherVillagePanel2_make_vassal");
            this.backImage.addControl(this.vassalButton);
            this.castleButton.ImageNorm = (Image) GFXLibrary.mrhp_reports;
            this.castleButton.OverBrighten = true;
            this.castleButton.MoveOnClick = true;
            this.castleButton.Position = new Point(0x40, 0x70);
            this.castleButton.CustomTooltipID = 0x98d;
            this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "OtherVillagePanel2_view_castle");
            this.backImage.addControl(this.castleButton);
            if (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator)
            {
                this.renameButton.ImageNorm = (Image) GFXLibrary.faction_pen;
                this.renameButton.OverBrighten = true;
                this.renameButton.MoveOnClick = true;
                this.renameButton.Position = new Point(0x8b, 0x39);
                this.renameButton.CustomTooltipID = 0x2896;
                this.renameButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resetNameClick));
                this.backImage.addControl(this.renameButton);
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
            this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "OtherVillagePanel2_protection_left");
            this.leftButton.Visible = false;
            this.backImage.addControl(this.leftButton);
            this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
            this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
            this.rightButton.Position = new Point(170, 50);
            this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "OtherVillagePanel2_protection_right");
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
            base.Name = "OtherVillagePanel2";
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

        private void ResetName()
        {
            int selectedVillage = InterfaceMgr.Instance.SelectedVillage;
            RemoteServices.Instance.VillageResetName(selectedVillage);
        }

        private void resetNameClick()
        {
            if (MyMessageBox.Show(SK.Text("Mod_Reset_Default", "Are you sure you want to reset the village name to its default?"), SK.Text("Mod_Confirm", "Confirm"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.ResetName();
            }
        }

        private void sendMonks()
        {
            if (this.m_selectedVillage >= 0)
            {
                InterfaceMgr.Instance.openSendMonkWindow(this.m_selectedVillage);
            }
        }

        private void sendReinforcements()
        {
            if (this.m_selectedVillage >= 0)
            {
                if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                {
                    GameEngine.Instance.SkipVillageTab();
                    InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                }
                else
                {
                    InterfaceMgr.Instance.getMainTabBar().changeTab(2);
                }
                InterfaceMgr.Instance.setVillageTabSubMode(6);
                InterfaceMgr.Instance.getVillageTabBar().changeTabGfxOnly(9);
                InterfaceMgr.Instance.setReinforcementVillage(this.m_selectedVillage);
            }
        }

        private void sendResources()
        {
            if (this.m_selectedVillage >= 0)
            {
                GameEngine.Instance.SkipVillageTab();
                InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                InterfaceMgr.Instance.setVillageTabSubMode(2);
                InterfaceMgr.Instance.tradeWithResume(this.m_selectedVillage, false);
            }
        }

        private void sendScouts()
        {
            if (this.m_selectedVillage >= 0)
            {
                InterfaceMgr.Instance.openScoutPopupWindow(this.m_selectedVillage, true);
            }
        }

        private void sendTroops()
        {
            if (this.m_selectedVillage >= 0)
            {
                GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.m_selectedVillage);
            }
        }

        public void update()
        {
            this.backGround.update();
            int[] numArray = new int[3];
            TimeSpan[] spanArray = new TimeSpan[3];
            int numInfos = this.numInfos;
            this.numInfos = 0;
            bool visible = this.lblProtectionType.Visible;
            int num2 = 0;
            TimeSpan span = new TimeSpan();
            if (GameEngine.Instance.World.isVillageInterdictProtected(this.m_selectedVillage))
            {
                DateTime time = GameEngine.Instance.World.getInterdictTime(this.m_selectedVillage);
                DateTime time2 = VillageMap.getCurrentServerTime();
                span = (TimeSpan) (time - time2);
                num2 = 1;
                spanArray[this.numInfos] = span;
                numArray[this.numInfos] = num2;
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
                    num2 = 2;
                }
                spanArray[this.numInfos] = span2;
                numArray[this.numInfos] = 2;
                this.numInfos++;
            }
            if (GameEngine.Instance.World.isVillageVacationProtected(this.m_selectedVillage))
            {
                num2 = 3;
                numArray[this.numInfos] = 3;
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
                case 3:
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked", "Cannot be attacked");
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Vacation", "Vacation Mode");
                    this.lblProtectionType.Visible = true;
                    break;

                default:
                {
                    int num5 = GameEngine.Instance.World.getVillageFaction(InterfaceMgr.Instance.OwnSelectedVillage);
                    int num6 = GameEngine.Instance.World.getVillageFaction(this.m_selectedVillage);
                    if (GameEngine.Instance.World.isUserVillage(this.m_selectedVillage))
                    {
                        this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Attack_Own_Village", "Cannot attack your own village");
                        this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Your_Village", "Your Village");
                        this.lblProtectionType.Visible = true;
                    }
                    else if ((num5 == num6) && (num5 >= 0))
                    {
                        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
                        {
                            this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Attack_Faction_Member", "Cannot attack Faction Member");
                        }
                        this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Your_Faction", "Your Faction");
                        this.lblProtectionType.Visible = true;
                    }
                    else
                    {
                        this.lblProtected.TextDiffOnly = "";
                        this.lblProtectionType.Visible = false;
                    }
                    break;
                }
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

        public void updateOtherVillageText(int selectedVillage)
        {
            this.m_selectedVillage = selectedVillage;
            this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
            this.backGround.updatePanelTypeFromVillageID(selectedVillage);
            this.backGround.setActionFromVillage(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage);
            if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
            {
                this.tradeButton.Enabled = false;
                this.vassalButton.Enabled = false;
                this.monkButton.Enabled = false;
                this.scoutButton.Enabled = false;
                if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    this.attackButton.Enabled = false;
                    this.reinforceButton.Enabled = false;
                }
                else
                {
                    this.attackButton.Enabled = true;
                    this.reinforceButton.Enabled = true;
                }
            }
            else
            {
                this.attackButton.Enabled = true;
                this.reinforceButton.Enabled = true;
                this.tradeButton.Enabled = true;
                this.scoutButton.Enabled = true;
                this.monkButton.Enabled = true;
                int num = GameEngine.Instance.World.numVassalsAllowed();
                int num2 = GameEngine.Instance.World.countVassals();
                if (num > num2)
                {
                    this.vassalButton.Enabled = true;
                }
                else
                {
                    this.vassalButton.Enabled = false;
                }
            }
            this.updateSize();
            this.update();
        }

        private void updateSize()
        {
            bool visible = this.lblProtectionType.Visible;
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
            this.vassalButton.Position = new Point(0x60, (0x70 + num) + num2);
            this.castleButton.Position = new Point(0x40, (0x70 + num) + num2);
            this.renameButton.Position = new Point(0x95, (0x70 + num) + num2);
            this.backGround.invalidate();
        }

        private void vassalClick()
        {
            if (this.m_selectedVillage >= 0)
            {
                GameEngine.Instance.SkipVillageTab();
                InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                InterfaceMgr.Instance.setVillageTabSubMode(8);
                InterfaceMgr.Instance.resetVillageReportPanelData();
                InterfaceMgr.Instance.selectVassalTarget(this.m_selectedVillage);
            }
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

