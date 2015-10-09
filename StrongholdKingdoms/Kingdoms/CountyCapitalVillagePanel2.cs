namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CountyCapitalVillagePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDImage backImage;
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton reinforceButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();

        public CountyCapitalVillagePanel2()
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

        private void btnSendCourtiers_Click(object sender, EventArgs e)
        {
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
            if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
            {
                GameEngine.Instance.SkipVillageTab();
                InterfaceMgr.Instance.getMainTabBar().changeTab(1);
            }
            else
            {
                InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            }
            InterfaceMgr.Instance.setCapitalSendTargetVillage(this.m_selectedVillage);
            InterfaceMgr.Instance.setVillageTabSubMode(0x11);
        }

        private void btnTradeWith_Click()
        {
            if (this.m_selectedVillage >= 0)
            {
                InterfaceMgr.Instance.selectStockExchange(-1);
                if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                {
                    GameEngine.Instance.SkipVillageTab();
                    InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                }
                else
                {
                    InterfaceMgr.Instance.getMainTabBar().changeTab(2);
                }
                InterfaceMgr.Instance.setVillageTabSubMode(3);
                InterfaceMgr.Instance.resetVillageReportPanelData();
                InterfaceMgr.Instance.selectStockExchange(this.m_selectedVillage);
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

        public void init()
        {
            base.clearControls();
            this.backImage = this.backGround.init(false, 0x5dd);
            base.addControl(this.backGround);
            this.tradeButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
            this.tradeButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[7];
            this.tradeButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[14];
            this.tradeButton.Position = new Point(10, 0x31);
            this.tradeButton.Enabled = false;
            this.tradeButton.CustomTooltipID = 0x96a;
            this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnTradeWith_Click), "CountyCapitalVillagePanel2_trade");
            this.backImage.addControl(this.tradeButton);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton.Position = new Point(0x2d, 0x31);
            this.attackButton.Enabled = false;
            this.attackButton.CustomTooltipID = 0x96b;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "CountyCapitalVillagePanel2_attack");
            this.backImage.addControl(this.attackButton);
            this.scoutButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.scoutButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
            this.scoutButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x11];
            this.scoutButton.Position = new Point(80, 0x31);
            this.scoutButton.Enabled = false;
            this.scoutButton.CustomTooltipID = 0x96c;
            this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "CountyCapitalVillagePanel2_scout");
            this.backImage.addControl(this.scoutButton);
            this.reinforceButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
            this.reinforceButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[9];
            this.reinforceButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x10];
            this.reinforceButton.Position = new Point(0x73, 0x31);
            this.reinforceButton.Enabled = false;
            this.reinforceButton.CustomTooltipID = 0x96d;
            this.reinforceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendTroops_Click), "CountyCapitalVillagePanel2_reinforce");
            this.backImage.addControl(this.reinforceButton);
            this.monkButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
            this.monkButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[11];
            this.monkButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x12];
            this.monkButton.Position = new Point(150, 0x31);
            this.monkButton.Enabled = false;
            this.monkButton.CustomTooltipID = 0x96e;
            this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnSendMonks_Click), "CountyCapitalVillagePanel2_sendmonks");
            this.backImage.addControl(this.monkButton);
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
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "CountyCapitalVillagePanel2";
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

        public void update()
        {
            this.backGround.update();
            bool visible = this.lblProtectionType.Visible;
            int num = 0;
            TimeSpan span = new TimeSpan();
            if (GameEngine.Instance.World.isVillageInterdictProtected(this.m_selectedVillage))
            {
                DateTime time = GameEngine.Instance.World.getInterdictTime(this.m_selectedVillage);
                DateTime time2 = VillageMap.getCurrentServerTime();
                span = (TimeSpan) (time - time2);
                num = 1;
            }
            if (num == 1)
            {
                int totalSeconds = (int) span.TotalSeconds;
                string str = VillageMap.createBuildTimeStringFull(totalSeconds);
                this.lblProtected.TextDiffOnly = SK.Text("OtherVillagePanel_Cannot_Be_Attacked_For_X_Time", "Cannot be attacked for") + " : " + str;
                this.lblProtectionType.TextDiffOnly = SK.Text("OtherVillagePanel_Interdict", "Interdict");
                this.lblProtectionType.Visible = true;
            }
            else
            {
                this.lblProtected.TextDiffOnly = "";
                this.lblProtectionType.TextDiffOnly = "";
                this.lblProtectionType.Visible = false;
            }
            if (visible != this.lblProtectionType.Visible)
            {
                this.updateSize();
            }
        }

        public void updateCountyCapitalVillageText(int selectedVillage, int ownVillage)
        {
            this.m_selectedVillage = selectedVillage;
            this.backGround.updateHeading(GameEngine.Instance.World.getVillageName(selectedVillage));
            this.backGround.setActionFromVillage(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage);
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
                bool flag = true;
                if (GameEngine.Instance.World.isCapital(ownVillage))
                {
                    this.scoutButton.Enabled = false;
                    flag = false;
                    this.monkButton.Enabled = false;
                    this.reinforceButton.Enabled = false;
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
            this.updateSize();
            this.update();
        }

        private void updateSize()
        {
            bool visible = this.lblProtectionType.Visible;
            int num = 0;
            if (!visible)
            {
                this.backImage.Image = (Image) GFXLibrary.mrhp_world_panel_102;
                num = -95;
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
            this.backGround.invalidate();
        }
    }
}

