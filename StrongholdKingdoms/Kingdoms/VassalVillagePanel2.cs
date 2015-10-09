namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VassalVillagePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private bool attackMode;
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDImage backImage;
        private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDArea drawArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton leftButton = new CustomSelfDrawPanel.CSDButton();
        private int m_selectedVillage = -1;
        private int numInfos;
        private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton rightButton = new CustomSelfDrawPanel.CSDButton();
        private int selectedProtection;
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton vassalButton = new CustomSelfDrawPanel.CSDButton();

        public VassalVillagePanel2()
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
            this.attackMode = false;
            base.clearControls();
            this.backImage = this.backGround.init(true, 0x2710);
            this.drawArea.Size = this.backImage.Size;
            this.drawArea.Position = new Point(0, 0);
            this.drawArea.Visible = true;
            this.backImage.addControl(this.drawArea);
            base.addControl(this.backGround);
            this.tradeButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
            this.tradeButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[7];
            this.tradeButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[14];
            this.tradeButton.Position = new Point(0x1d, 0x8e);
            this.tradeButton.CustomTooltipID = 0x96a;
            this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendResources), "VassalVillagePanel2_trade");
            this.drawArea.addControl(this.tradeButton);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton.Position = new Point(0x40, 0x8e);
            this.attackButton.CustomTooltipID = 0x995;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendTroops), "VassalVillagePanel2_attack_from");
            this.drawArea.addControl(this.attackButton);
            this.reinforceButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
            this.reinforceButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[9];
            this.reinforceButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x10];
            this.reinforceButton.Position = new Point(0x63, 0x8e);
            this.reinforceButton.CustomTooltipID = 0x993;
            this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendReinforcements), "VassalVillagePanel2_manage_troops");
            this.drawArea.addControl(this.reinforceButton);
            this.vassalButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
            this.vassalButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[12];
            this.vassalButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x13];
            this.vassalButton.Position = new Point(0x86, 0x8e);
            this.vassalButton.CustomTooltipID = 0x994;
            this.vassalButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.vassalClick), "VassalVillagePanel2_manage_vassals");
            this.drawArea.addControl(this.vassalButton);
            this.castleButton.ImageNorm = (Image) GFXLibrary.mrhp_reports;
            this.castleButton.OverBrighten = true;
            this.castleButton.MoveOnClick = true;
            this.castleButton.Position = new Point(80, 0x70);
            this.castleButton.CustomTooltipID = 0x98d;
            this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "VassalVillagePanel2_view_castle_report");
            this.drawArea.addControl(this.castleButton);
            this.lblProtectionType.Text = "";
            this.lblProtectionType.Color = ARGBColors.Black;
            this.lblProtectionType.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.lblProtectionType.Position = new Point(0, 0x26);
            this.lblProtectionType.Size = new Size(this.backImage.Width, 0x17);
            this.lblProtectionType.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.drawArea.addControl(this.lblProtectionType);
            this.lblProtected.Text = "";
            this.lblProtected.Color = ARGBColors.Black;
            this.lblProtected.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.lblProtected.Position = new Point(6, 0x30);
            this.lblProtected.Size = new Size(this.backImage.Width - 12, 0x4a);
            this.lblProtected.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.drawArea.addControl(this.lblProtected);
            this.leftButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_left_norm;
            this.leftButton.ImageOver = (Image) GFXLibrary.r_arrow_small_left_over;
            this.leftButton.Position = new Point(5, 50);
            this.leftButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoLeft), "VassalVillagePanel2_protection_left");
            this.leftButton.Visible = false;
            this.drawArea.addControl(this.leftButton);
            this.rightButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
            this.rightButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
            this.rightButton.Position = new Point(170, 50);
            this.rightButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoRight), "VassalVillagePanel2_protection_right");
            this.rightButton.Visible = false;
            this.drawArea.addControl(this.rightButton);
            this.updateSize();
            this.numInfos = 0;
            this.selectedProtection = 0;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "VassalVillagePanel2";
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

        private void sendReinforcements()
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.setVassalArmiesVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.setVillageTabSubMode(15);
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

        private void sendTroops()
        {
            InterfaceMgr.Instance.setVassalAttackMode(this.m_selectedVillage);
            this.attackMode = true;
            this.backImage.Size = new Size(1, 1);
            this.drawArea.Visible = false;
            this.backGround.updateHeading(SK.Text("VassalVillagePanel_Attack_From_Here", "Attack From Here"));
        }

        public void update()
        {
            this.backGround.update();
            if (this.attackMode && (InterfaceMgr.Instance.SelectedVassalVillage < 0))
            {
                this.backImage.Size = this.backImage.Image.Size;
                this.drawArea.Visible = true;
                this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(this.m_selectedVillage));
            }
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
                    string str = VillageMap.createBuildTimeString(totalSeconds);
                    this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + str;
                    this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
                    this.lblProtectionType.Visible = true;
                    break;
                }
                case 2:
                {
                    int secsLeft = (int) span.TotalSeconds;
                    string str2 = VillageMap.createBuildTimeString(secsLeft);
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
            this.tradeButton.Position = new Point(0x1d, 0x8e + num);
            this.attackButton.Position = new Point(0x40, 0x8e + num);
            this.reinforceButton.Position = new Point(0x63, 0x8e + num);
            this.vassalButton.Position = new Point(0x86, 0x8e + num);
            this.castleButton.Position = new Point(80, (0x70 + num) + num2);
            this.backGround.invalidate();
        }

        public void updateVassalVillageText(int selectedVillage)
        {
            this.attackMode = false;
            this.m_selectedVillage = selectedVillage;
            this.backGround.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
            this.backGround.updatePanelTypeFromVillageID(selectedVillage);
            this.backGround.setActionFromVillage(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage);
            this.updateSize();
            this.update();
        }

        private void vassalClick()
        {
            GameEngine.Instance.SkipVillageTab();
            InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            InterfaceMgr.Instance.setVillageTabSubMode(8);
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

