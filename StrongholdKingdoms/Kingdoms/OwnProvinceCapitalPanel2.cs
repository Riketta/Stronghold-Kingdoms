namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class OwnProvinceCapitalPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDImage backImage;
        private CustomSelfDrawPanel.CSDButton castleButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel lblProtected = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblProtectionType = new CustomSelfDrawPanel.CSDLabel();
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDButton resourcesButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton troopsButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageButton = new CustomSelfDrawPanel.CSDButton();

        public OwnProvinceCapitalPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void castleClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
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

        public void init()
        {
            base.clearControls();
            this.backImage = this.backGround.init(true, 0x2710);
            base.addControl(this.backGround);
            this.tradeButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
            this.tradeButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[7];
            this.tradeButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[14];
            this.tradeButton.Position = new Point(80, 0x8e);
            this.tradeButton.CustomTooltipID = 0x989;
            this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradeClick), "OwnProvinceCapitalPanel2_trade");
            this.backImage.addControl(this.tradeButton);
            this.villageButton.ImageNorm = (Image) GFXLibrary.int_world_icon_village;
            this.villageButton.OverBrighten = true;
            this.villageButton.MoveOnClick = true;
            this.villageButton.Position = new Point(0x1d, 0x70);
            this.villageButton.CustomTooltipID = 0x985;
            this.villageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClick), "OwnProvinceCapitalPanel2_view_village");
            this.backImage.addControl(this.villageButton);
            this.castleButton.ImageNorm = (Image) GFXLibrary.int_world_icon_castle;
            this.castleButton.OverBrighten = true;
            this.castleButton.MoveOnClick = true;
            this.castleButton.Position = new Point(0x40, 0x70);
            this.castleButton.CustomTooltipID = 0x986;
            this.castleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "OwnProvinceCapitalPanel2_view_castle");
            this.backImage.addControl(this.castleButton);
            this.resourcesButton.ImageNorm = (Image) GFXLibrary.int_world_icon_resource;
            this.resourcesButton.OverBrighten = true;
            this.resourcesButton.MoveOnClick = true;
            this.resourcesButton.Position = new Point(0x63, 0x70);
            this.resourcesButton.CustomTooltipID = 0x987;
            this.resourcesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourcesClick), "OwnProvinceCapitalPanel2_view_resources");
            this.backImage.addControl(this.resourcesButton);
            this.troopsButton.ImageNorm = (Image) GFXLibrary.int_world_icon_troops;
            this.troopsButton.OverBrighten = true;
            this.troopsButton.MoveOnClick = true;
            this.troopsButton.Position = new Point(0x86, 0x70);
            this.troopsButton.CustomTooltipID = 0x98a;
            this.troopsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopsClick), "OwnProvinceCapitalPanel2_make_troops");
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
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "OwnProvinceCapitalPanel2";
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

        private void resourcesClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            InterfaceMgr.Instance.setVillageTabSubMode(0x3ed);
        }

        private void tradeClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            InterfaceMgr.Instance.getVillageTabBar().changeTab(3);
        }

        private void troopsClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            InterfaceMgr.Instance.setVillageTabSubMode(0x3ec);
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
            this.tradeButton.Position = new Point(80, 0x8e + num);
            this.villageButton.Position = new Point(0x1d, (0x70 + num) + num2);
            this.castleButton.Position = new Point(0x40, (0x70 + num) + num2);
            this.resourcesButton.Position = new Point(0x63, (0x70 + num) + num2);
            this.troopsButton.Position = new Point(0x86, (0x70 + num) + num2);
            this.backGround.invalidate();
        }

        private void villageClick()
        {
            InterfaceMgr.Instance.getMainTabBar().changeTab(2);
            InterfaceMgr.Instance.getVillageTabBar().changeTab(0);
        }
    }
}

