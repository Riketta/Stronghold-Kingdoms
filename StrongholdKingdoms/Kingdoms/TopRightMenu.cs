namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TopRightMenu : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton btnVillageLeft = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnVillagesRight = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private FactionTabBar2 factionTabBar1 = new FactionTabBar2();
        private Color highlightColour = Color.FromArgb(0xe8, 230, 0xe4);
        private CustomSelfDrawPanel.CSDLabel lblVillageName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        public MainMenuBar2 mainMenuBar = new MainMenuBar2();
        private MainTabBar2 mainTabBar1 = new MainTabBar2();
        private CustomSelfDrawPanel.CSDImage villageButton = new CustomSelfDrawPanel.CSDImage();
        private MenuPopup villageListMenu;
        private VillageTabBar2 villageTabBar1 = new VillageTabBar2();

        public TopRightMenu()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void btnVillageClick()
        {
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_left");
            InterfaceMgr.Instance.centerOnVillage();
        }

        private void btnVillageLeft_Click()
        {
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_left");
            InterfaceMgr.Instance.selectedVillageNameLeft();
        }

        private void btnVillagesRight_Click()
        {
            GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_right");
            InterfaceMgr.Instance.selectedVillageNameRight();
        }

        private void comboVillageList_SelectionChangeCommitted(int id)
        {
            if (id >= 0)
            {
                GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_droplist_selected");
                InterfaceMgr.Instance.selectUserVillage(id, true);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void doubleClickedItem(int id)
        {
            if (id >= 0)
            {
                if (!GameEngine.Instance.World.isCapital(id))
                {
                    InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                }
                else
                {
                    InterfaceMgr.Instance.getMainTabBar().changeTab(2);
                }
            }
        }

        public FactionTabBar2 getFactionTabBar()
        {
            return this.factionTabBar1;
        }

        public MainTabBar2 getMainTabBar()
        {
            return this.mainTabBar1;
        }

        public VillageTabBar2 getVillageTabBar()
        {
            return this.villageTabBar1;
        }

        private void highlightCountiesVillages(int testCountyID, List<WorldMap.VillageNameItem> namesList)
        {
            foreach (WorldMap.VillageNameItem item in namesList)
            {
                if (((item.villageID >= 0) && (GameEngine.Instance.World.isRegionCapital(item.villageID) || !item.capital)) && (GameEngine.Instance.World.getCountyFromVillageID(item.villageID) == testCountyID))
                {
                    this.villageListMenu.highlightByID(item.villageID, this.highlightColour);
                }
            }
        }

        private void highlightCountriesVillages(int testCountryID, List<WorldMap.VillageNameItem> namesList)
        {
            foreach (WorldMap.VillageNameItem item in namesList)
            {
                if (((item.villageID >= 0) && ((GameEngine.Instance.World.isRegionCapital(item.villageID) || GameEngine.Instance.World.isCountyCapital(item.villageID)) || (GameEngine.Instance.World.isProvinceCapital(item.villageID) || !item.capital))) && (GameEngine.Instance.World.getCountryFromVillageID(item.villageID) == testCountryID))
                {
                    this.villageListMenu.highlightByID(item.villageID, this.highlightColour);
                }
            }
        }

        private void highlightProvincesVillages(int testProvinceID, List<WorldMap.VillageNameItem> namesList)
        {
            foreach (WorldMap.VillageNameItem item in namesList)
            {
                if (((item.villageID >= 0) && ((GameEngine.Instance.World.isRegionCapital(item.villageID) || GameEngine.Instance.World.isCountyCapital(item.villageID)) || !item.capital)) && (GameEngine.Instance.World.getProvinceFromVillageID(item.villageID) == testProvinceID))
                {
                    this.villageListMenu.highlightByID(item.villageID, this.highlightColour);
                }
            }
        }

        private void highlightRegionsVillages(int testRegionID, List<WorldMap.VillageNameItem> namesList)
        {
            foreach (WorldMap.VillageNameItem item in namesList)
            {
                if (((item.villageID >= 0) && !item.capital) && (GameEngine.Instance.World.getParishFromVillageID(item.villageID) == testRegionID))
                {
                    this.villageListMenu.highlightByID(item.villageID, this.highlightColour);
                }
            }
        }

        public void init()
        {
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.menubar_top;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(0x1cf, 120);
            base.addControl(this.mainBackgroundImage);
            this.btnVillageLeft.ImageNorm = (Image) GFXLibrary.villagename_button_left_normal;
            this.btnVillageLeft.ImageOver = (Image) GFXLibrary.villagename_button_left_highlight;
            this.btnVillageLeft.ImageClick = (Image) GFXLibrary.villagename_button_left_selected;
            this.btnVillageLeft.Position = new Point(5, 0x1d);
            this.btnVillageLeft.CustomTooltipID = 20;
            this.btnVillageLeft.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnVillageLeft_Click));
            this.mainBackgroundImage.addControl(this.btnVillageLeft);
            this.btnVillagesRight.ImageNorm = (Image) GFXLibrary.villagename_button_right_normal;
            this.btnVillagesRight.ImageOver = (Image) GFXLibrary.villagename_button_right_highlight;
            this.btnVillagesRight.ImageClick = (Image) GFXLibrary.villagename_button_right_selected;
            this.btnVillagesRight.Position = new Point(0x18, 0x1d);
            this.btnVillagesRight.CustomTooltipID = 20;
            this.btnVillagesRight.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnVillagesRight_Click));
            this.mainBackgroundImage.addControl(this.btnVillagesRight);
            this.villageButton.Image = (Image) GFXLibrary.villagename_body;
            this.villageButton.Position = new Point(0x31, 0x1d);
            this.mainBackgroundImage.addControl(this.villageButton);
            this.lblVillageName.Position = new Point(20, -1);
            this.lblVillageName.Size = new Size(this.villageButton.Size.Width - 0x23, this.villageButton.Height);
            this.lblVillageName.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblVillageName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblVillageName.Color = ARGBColors.Black;
            this.lblVillageName.CustomTooltipID = 0x15;
            this.lblVillageName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillageName_Click));
            this.villageButton.addControl(this.lblVillageName);
            this.mainTabBar1.Position = new Point(3, 0x33);
            this.mainTabBar1.Size = new Size(460, 40);
            this.mainBackgroundImage.addControl(this.mainTabBar1);
            this.villageTabBar1.Position = new Point(3, 0x58);
            this.villageTabBar1.Size = new Size(460, 40);
            this.mainBackgroundImage.addControl(this.villageTabBar1);
            this.factionTabBar1.Position = new Point(3, 0x58);
            this.factionTabBar1.Size = new Size(460, 40);
            this.factionTabBar1.Visible = false;
            this.mainBackgroundImage.addControl(this.factionTabBar1);
            this.factionTabBar1.Visible = false;
            this.mainMenuBar.Position = new Point(0, 0);
            this.mainMenuBar.Size = new Size(base.Width, 0x19);
            this.mainBackgroundImage.addControl(this.mainMenuBar);
            this.mainTabBar1.init();
            this.villageTabBar1.init();
            this.factionTabBar1.init();
            this.mainMenuBar.init2();
            this.resize();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            this.MinimumSize = new Size(0x1cf, 0);
            base.Name = "TopRightMenu";
            base.Size = new Size(0x1cf, 120);
            base.ResumeLayout(false);
        }

        private void lblVillageName_Click()
        {
            if (((this.villageListMenu != null) && this.villageListMenu.Visible) || InterfaceMgr.Instance.menuPopupClosedRecently())
            {
                GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_droplist_close");
                InterfaceMgr.Instance.closeMenuPopup();
            }
            else
            {
                GameEngine.Instance.playInterfaceSound("WorldMapScreen_village_droplist_open");
                this.villageListMenu = new MenuPopup();
                this.villageListMenu.setCallBack(new MenuPopup.MenuCallback(this.comboVillageList_SelectionChangeCommitted));
                this.villageListMenu.setDoubleClickCallBack(new MenuPopup.MenuCallback(this.doubleClickedItem));
                this.villageListMenu.setLineHeight(15);
                this.villageListMenu.closeOnClickOnly();
                this.villageListMenu.mouseOverDelegates(new MenuPopup.MenuItemRolloverDelegate(this.mouseOverItem), new MenuPopup.MenuItemRolloverDelegate(this.mouseLeaveItem));
                this.villageListMenu.setBackColour(Color.FromArgb(0xff, 0xba, 0xaf, 0xa3));
                Point point = base.PointToScreen(this.villageButton.Position);
                this.villageListMenu.setPosition(point.X + 0x12, point.Y + 0x15);
                List<WorldMap.VillageNameItem> list = GameEngine.Instance.World.getUserVillageNamesListAndCapitals();
                int num = 0;
                foreach (WorldMap.VillageNameItem item in list)
                {
                    if (item.capital || (item.villageID < 0))
                    {
                        break;
                    }
                    num++;
                }
                if (num >= 3)
                {
                    this.villageListMenu.addMenuItem(SK.Text("Menu_Your_Villages", "Your Villages") + " (" + num.ToString() + ")", -1).Enabled = false;
                }
                else
                {
                    this.villageListMenu.addMenuItem(SK.Text("Menu_Your_Villages", "Your Villages"), -1).Enabled = false;
                }
                this.villageListMenu.addBar();
                bool flag = false;
                foreach (WorldMap.VillageNameItem item2 in list)
                {
                    if (item2.villageID < 0)
                    {
                        this.villageListMenu.newColumn();
                        flag = true;
                    }
                    bool bold = false;
                    if (flag && GameEngine.Instance.World.isUserVillage(item2.villageID))
                    {
                        bold = true;
                    }
                    CustomSelfDrawPanel.CSDControl control3 = this.villageListMenu.addMenuItem(item2.villageName, item2.villageID, bold);
                    if (item2.villageID < 0)
                    {
                        control3.Enabled = false;
                        this.villageListMenu.addBar();
                    }
                }
                this.villageListMenu.showMenu();
                MainWindow.captureCloseMenuEvent = true;
            }
        }

        private void mouseLeaveItem(int id)
        {
        }

        private void mouseOverItem(int id)
        {
            this.villageListMenu.clearHighlights();
            if (id >= 0)
            {
                List<WorldMap.VillageNameItem> namesList = GameEngine.Instance.World.getUserVillageNamesListAndCapitals();
                if (GameEngine.Instance.World.isRegionCapital(id))
                {
                    int testRegionID = GameEngine.Instance.World.getParishFromVillageID(id);
                    this.highlightRegionsVillages(testRegionID, namesList);
                    int countyID = GameEngine.Instance.World.getCountyFromVillageID(id);
                    int num3 = GameEngine.Instance.World.getCountyCapitalVillage(countyID);
                    this.villageListMenu.highlightByID(num3, this.highlightColour);
                    int provinceID = GameEngine.Instance.World.getProvinceFromVillageID(id);
                    int num5 = GameEngine.Instance.World.getProvinceCapital(provinceID);
                    this.villageListMenu.highlightByID(num5, this.highlightColour);
                    int countryID = GameEngine.Instance.World.getCountryFromVillageID(id);
                    int num7 = GameEngine.Instance.World.getCountryCapital(countryID);
                    this.villageListMenu.highlightByID(num7, this.highlightColour);
                }
                else if (GameEngine.Instance.World.isCountyCapital(id))
                {
                    int testCountyID = GameEngine.Instance.World.getCountyFromVillageID(id);
                    this.highlightCountiesVillages(testCountyID, namesList);
                    int num9 = GameEngine.Instance.World.getProvinceFromVillageID(id);
                    int num10 = GameEngine.Instance.World.getProvinceCapital(num9);
                    this.villageListMenu.highlightByID(num10, this.highlightColour);
                    int num11 = GameEngine.Instance.World.getCountryFromVillageID(id);
                    int num12 = GameEngine.Instance.World.getCountryCapital(num11);
                    this.villageListMenu.highlightByID(num12, this.highlightColour);
                }
                else if (GameEngine.Instance.World.isProvinceCapital(id))
                {
                    int testProvinceID = GameEngine.Instance.World.getProvinceFromVillageID(id);
                    this.highlightProvincesVillages(testProvinceID, namesList);
                    int num14 = GameEngine.Instance.World.getCountryFromVillageID(id);
                    int num15 = GameEngine.Instance.World.getCountryCapital(num14);
                    this.villageListMenu.highlightByID(num15, this.highlightColour);
                }
                else if (GameEngine.Instance.World.isCountryCapital(id))
                {
                    int testCountryID = GameEngine.Instance.World.getCountryFromVillageID(id);
                    this.highlightCountriesVillages(testCountryID, namesList);
                }
                else
                {
                    int parishID = GameEngine.Instance.World.getParishFromVillageID(id);
                    int num18 = GameEngine.Instance.World.getParishCapital(parishID);
                    this.villageListMenu.highlightByID(num18, this.highlightColour);
                    int num19 = GameEngine.Instance.World.getCountyFromVillageID(id);
                    int num20 = GameEngine.Instance.World.getCountyCapitalVillage(num19);
                    this.villageListMenu.highlightByID(num20, this.highlightColour);
                    int num21 = GameEngine.Instance.World.getProvinceFromVillageID(id);
                    int num22 = GameEngine.Instance.World.getProvinceCapital(num21);
                    this.villageListMenu.highlightByID(num22, this.highlightColour);
                    int num23 = GameEngine.Instance.World.getCountryFromVillageID(id);
                    int num24 = GameEngine.Instance.World.getCountryCapital(num23);
                    this.villageListMenu.highlightByID(num24, this.highlightColour);
                }
            }
        }

        public void resize()
        {
            this.mainBackgroundImage.Size = new Size(base.Width, 120);
            this.btnVillageLeft.Position = new Point(-458 + base.Width, this.btnVillageLeft.Position.Y);
            this.btnVillagesRight.Position = new Point(-439 + base.Width, this.btnVillagesRight.Position.Y);
            this.villageButton.Position = new Point(-414 + base.Width, this.villageButton.Position.Y);
            this.factionTabBar1.Position = new Point(-460 + base.Width, this.factionTabBar1.Position.Y);
            this.mainTabBar1.Position = new Point(-460 + base.Width, this.mainTabBar1.Position.Y);
            this.villageTabBar1.Position = new Point(-460 + base.Width, this.villageTabBar1.Position.Y);
            this.factionTabBar1.Position = new Point(-460 + base.Width, this.factionTabBar1.Position.Y);
            this.mainMenuBar.Size = new Size(base.Width, 0x19);
            this.mainMenuBar.resize();
            base.Invalidate();
        }

        public void setSelectedVillageName(string villageName, bool asCapital)
        {
            this.lblVillageName.Text = villageName;
        }

        public void showFactionTabBar(bool state)
        {
            this.factionTabBar1.Visible = state;
        }

        public void showVillageTab(bool state)
        {
            this.villageTabBar1.Visible = state;
        }
    }
}

