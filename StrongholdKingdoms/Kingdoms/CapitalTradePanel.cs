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

    public class CapitalTradePanel : CustomSelfDrawPanel, IDockableControl
    {
        private int BACKUP_buyLevel;
        private int BACKUP_resource = -1;
        private CustomSelfDrawPanel.CSDButton buyButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel buyCostLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel buyCostValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage buyHeadingImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel buyHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel buyMax = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel buyMin = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel buyNumber = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel BuyPriceHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel buySubWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel buyTaxLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar buyTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDExtendingPanel buyWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private int currentResource = -1;
        private int currentResourcePacketSize = 1;
        private DockableControl dockableControl;
        private static List<WorldMap.VillageNameItem> exchangeFavourites = new List<WorldMap.VillageNameItem>();
        private static List<WorldMap.VillageNameItem> exchangeHistory = new List<WorldMap.VillageNameItem>();
        private CustomSelfDrawPanel.CSDImage highlightLine1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine8 = new CustomSelfDrawPanel.CSDImage();
        public static CapitalTradePanel instance = null;
        private int lastTab = -1;
        private CustomSelfDrawPanel.CSDExtendingPanel leftWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea1 = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea2 = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea3 = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel localHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel localLabel8 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel midWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel priceLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel priceLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel priceLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel priceLabel4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel priceLabel5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel priceLabel6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel priceLabel7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel priceLabel8 = new CustomSelfDrawPanel.CSDLabel();
        private int selectedStockExchange = -1;
        private CustomSelfDrawPanel.CSDButton selectRow1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sendEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage stockExchangeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel stockExchangeLabel = new CustomSelfDrawPanel.CSDLabel();
        public SparseArray stockExchanges = new SparseArray();
        private CustomSelfDrawPanel.CSDLabel storedHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel8 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton tabButton1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton worldMapButton = new CustomSelfDrawPanel.CSDButton();

        public CapitalTradePanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public static void addFavourites(GenericVillageHistoryData[] newData)
        {
            exchangeFavourites.Clear();
            if (newData != null)
            {
                foreach (GenericVillageHistoryData data in newData)
                {
                    WorldMap.VillageNameItem item = new WorldMap.VillageNameItem();
                    if (GameEngine.Instance.World.isCapital(data.villageID))
                    {
                        item.villageID = data.villageID;
                        exchangeFavourites.Add(item);
                    }
                }
            }
        }

        public static void addHistory(GenericVillageHistoryData[] newData)
        {
            exchangeHistory.Clear();
            if (newData != null)
            {
                foreach (GenericVillageHistoryData data in newData)
                {
                    WorldMap.VillageNameItem item = new WorldMap.VillageNameItem();
                    if (GameEngine.Instance.World.isCapital(data.villageID))
                    {
                        item.villageID = data.villageID;
                        exchangeHistory.Add(item);
                    }
                }
            }
        }

        private void buyClick()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                village.stockExchangeTrade(village.VillageID, this.currentResource, this.buyTrack.Value, true);
            }
        }

        public void closeClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(-1);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
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

        private void editSendValue()
        {
            InterfaceMgr.Instance.setFloatingValueSentDelegate(new InterfaceMgr.FloatingValueSent(this.floatingValueCB));
            Point point = InterfaceMgr.Instance.ParentForm.PointToScreen(new Point((620 + base.Location.X) + 0xd9, ((0xfe + base.Location.Y) + 120) - 50));
            FloatingInput.open(point.X, point.Y, this.buyTrack.Value, this.buyTrack.Max, InterfaceMgr.Instance.ParentForm);
        }

        private void findOnWorldClicked()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.BACKUP_resource = this.currentResource;
                this.BACKUP_buyLevel = this.buyTrack.Value;
                GameEngine.Instance.World.zoomToVillage(village.VillageID);
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(4);
                InterfaceMgr.Instance.StockExchangeBuyingVillage = village.VillageID;
            }
        }

        private void floatingValueCB(int value)
        {
            this.buyTrack.Value = value;
            this.updateValues();
        }

        private int getLineFromResource(int resource)
        {
            for (int i = 0; i < 8; i++)
            {
                if (this.getRowButton(i).Data == resource)
                {
                    return i;
                }
            }
            return 0;
        }

        private CustomSelfDrawPanel.CSDButton getRowButton(int row)
        {
            switch (row)
            {
                case 0:
                    return this.selectRow1;

                case 1:
                    return this.selectRow2;

                case 2:
                    return this.selectRow3;

                case 3:
                    return this.selectRow4;

                case 4:
                    return this.selectRow5;

                case 5:
                    return this.selectRow6;

                case 6:
                    return this.selectRow7;

                case 7:
                    return this.selectRow8;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDImage getRowHighlight(int row)
        {
            switch (row)
            {
                case 0:
                    return this.highlightLine1;

                case 1:
                    return this.highlightLine2;

                case 2:
                    return this.highlightLine3;

                case 3:
                    return this.highlightLine4;

                case 4:
                    return this.highlightLine5;

                case 5:
                    return this.highlightLine6;

                case 6:
                    return this.highlightLine7;

                case 7:
                    return this.highlightLine8;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDLabel getRowLocal(int row)
        {
            switch (row)
            {
                case 0:
                    return this.localLabel1;

                case 1:
                    return this.localLabel2;

                case 2:
                    return this.localLabel3;

                case 3:
                    return this.localLabel4;

                case 4:
                    return this.localLabel5;

                case 5:
                    return this.localLabel6;

                case 6:
                    return this.localLabel7;

                case 7:
                    return this.localLabel8;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDLabel getRowPrice(int row)
        {
            switch (row)
            {
                case 0:
                    return this.priceLabel1;

                case 1:
                    return this.priceLabel2;

                case 2:
                    return this.priceLabel3;

                case 3:
                    return this.priceLabel4;

                case 4:
                    return this.priceLabel5;

                case 5:
                    return this.priceLabel6;

                case 6:
                    return this.priceLabel7;

                case 7:
                    return this.priceLabel8;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDLabel getRowStored(int row)
        {
            switch (row)
            {
                case 0:
                    return this.storedLabel1;

                case 1:
                    return this.storedLabel2;

                case 2:
                    return this.storedLabel3;

                case 3:
                    return this.storedLabel4;

                case 4:
                    return this.storedLabel5;

                case 5:
                    return this.storedLabel6;

                case 6:
                    return this.storedLabel7;

                case 7:
                    return this.storedLabel8;
            }
            return null;
        }

        public void getStockExchangeDataCallback(GetStockExchangeData_ReturnType returnData)
        {
            if (returnData.Success)
            {
                StockExchangeInfo info = new StockExchangeInfo {
                    villageID = returnData.villageID,
                    woodLevel = returnData.woodLevel,
                    stoneLevel = returnData.stoneLevel,
                    ironLevel = returnData.ironLevel,
                    pitchLevel = returnData.pitchLevel,
                    aleLevel = returnData.aleLevel,
                    applesLevel = returnData.applesLevel,
                    breadLevel = returnData.breadLevel,
                    meatLevel = returnData.meatLevel,
                    cheeseLevel = returnData.cheeseLevel,
                    vegLevel = returnData.vegLevel,
                    fishLevel = returnData.fishLevel,
                    bowsLevel = returnData.bowsLevel,
                    pikesLevel = returnData.pikesLevel,
                    swordsLevel = returnData.swordsLevel,
                    armourLevel = returnData.armourLevel,
                    catapultsLevel = returnData.catapultsLevel,
                    furnitureLevel = returnData.furnitureLevel,
                    clothesLevel = returnData.clothesLevel,
                    saltLevel = returnData.saltLevel,
                    venisonLevel = returnData.venisonLevel,
                    silkLevel = returnData.silkLevel,
                    spicesLevel = returnData.spicesLevel,
                    metalwareLevel = returnData.metalwareLevel,
                    wineLevel = returnData.wineLevel
                };
                this.stockExchanges[returnData.villageID] = info;
                int line = this.getLineFromResource(this.currentResource);
                this.selectHighlightLine(line);
                this.updateValues();
            }
        }

        private CustomSelfDrawPanel.CSDButton getVillageHistory(int line)
        {
            return null;
        }

        public void init()
        {
            instance = this;
            base.clearControls();
            int num = 70;
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            this.stockExchangeLabel.Text = SK.Text("CapitalTradePanel_", "Purchase Goods");
            this.stockExchangeLabel.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
            this.stockExchangeLabel.DropShadowColor = Color.FromArgb(0x4a, 0x43, 0x30);
            this.stockExchangeLabel.Position = new Point(9, 9);
            this.stockExchangeLabel.Size = new Size(0x3e0, 50);
            this.stockExchangeLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
            this.stockExchangeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundArea.addControl(this.stockExchangeLabel);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CapitalTradePanel_close");
            this.mainBackgroundArea.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 12, new Point(0x382, 10));
            this.midWindow.Size = new Size(0xe4, (0x1c1 - num) - 150);
            this.midWindow.Position = new Point(0x177, 0x7c);
            this.mainBackgroundArea.addControl(this.midWindow);
            this.midWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.leftWindow.Size = new Size(0x14f, (0x1c1 - num) - 150);
            this.leftWindow.Position = new Point(0x2d, 0x7c);
            this.mainBackgroundArea.addControl(this.leftWindow);
            this.leftWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.lightArea1.Size = new Size(0x61, 0xb3);
            this.lightArea1.Position = new Point(0xd8, 0x66 - num);
            this.leftWindow.addControl(this.lightArea1);
            this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.localHeadingLabel.Text = SK.Text("TRADE_Local", "Local");
            this.localHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55 - num);
            this.localHeadingLabel.Position = new Point(0, -35);
            this.localHeadingLabel.Size = new Size(0x61, 30);
            this.localHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.localHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea1.addControl(this.localHeadingLabel);
            this.lightArea2.Size = new Size(0x61, 0xb3);
            this.lightArea2.Position = new Point(0x15, 0x66 - num);
            this.midWindow.addControl(this.lightArea2);
            this.lightArea2.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.storedHeadingLabel.Text = SK.Text("TRADE_At_Exchange", "At Exchange");
            this.storedHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55 - num);
            this.storedHeadingLabel.Position = new Point(0, -35);
            this.storedHeadingLabel.Size = new Size(0x61, 30);
            this.storedHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.storedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea2.addControl(this.storedHeadingLabel);
            this.lightArea3.Size = new Size(0x4d, 0xb3);
            this.lightArea3.Position = new Point(0x81, 0x66 - num);
            this.midWindow.addControl(this.lightArea3);
            this.lightArea3.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.BuyPriceHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy_Price", "Buy Price");
            this.BuyPriceHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55 - num);
            this.BuyPriceHeadingLabel.Position = new Point(-30, -35);
            this.BuyPriceHeadingLabel.Size = new Size(0x89, 30);
            this.BuyPriceHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.BuyPriceHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea3.addControl(this.BuyPriceHeadingLabel);
            this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
            this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
            this.tabButton1.Position = new Point(2, -13);
            this.tabButton1.MoveOnClick = false;
            this.tabButton1.Data = 1;
            this.tabButton1.Visible = false;
            this.leftWindow.addControl(this.tabButton1);
            this.buyWindow.Size = new Size(0x150, 0x91);
            this.buyWindow.Position = new Point(0x273, 0xa6);
            this.mainBackgroundArea.addControl(this.buyWindow);
            this.buyWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.buySubWindow.Size = new Size(0x93, 50);
            this.buySubWindow.Position = new Point(0xb2, 0x20);
            this.buyWindow.addControl(this.buySubWindow);
            this.buySubWindow.Create((Image) GFXLibrary.int_insetpanel_b_top_left, (Image) GFXLibrary.int_insetpanel_b_middle_top, (Image) GFXLibrary.int_insetpanel_b_top_right, (Image) GFXLibrary.int_insetpanel_b_middle_left, (Image) GFXLibrary.int_insetpanel_b_middle, (Image) GFXLibrary.int_insetpanel_b_middle_right, (Image) GFXLibrary.int_insetpanel_b_bottom_left, (Image) GFXLibrary.int_insetpanel_b_middle_bottom, (Image) GFXLibrary.int_insetpanel_b_bottom_right);
            this.buyHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy", "Buy") + " ";
            this.buyHeadingLabel.Color = ARGBColors.Black;
            this.buyHeadingLabel.Position = new Point(90, -30);
            this.buyHeadingLabel.Size = new Size(0xf6, 30);
            this.buyHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.buyHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
            this.buyWindow.addControl(this.buyHeadingLabel);
            this.buyHeadingImage.Image = null;
            this.buyHeadingImage.Position = new Point(5, -50);
            this.buyWindow.addControl(this.buyHeadingImage);
            this.buyTaxLabel.Text = SK.Text("CapitalTradePanel_25_Tax", "+25% Tax");
            this.buyTaxLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.buyTaxLabel.Position = new Point(0x15, 0x6c);
            this.buyTaxLabel.Size = new Size(0x4a, 30);
            this.buyTaxLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.buyTaxLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.buyWindow.addControl(this.buyTaxLabel);
            this.buyCostLabel.Text = SK.Text("CapitalTradePanel_Cost", "Cost") + ":";
            this.buyCostLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.buyCostLabel.Position = new Point(0, 13);
            this.buyCostLabel.Size = new Size(0x4a, 30);
            this.buyCostLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.buyCostLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.buySubWindow.addControl(this.buyCostLabel);
            this.buyNumber.Text = "0";
            this.buyNumber.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.buyNumber.Position = new Point(0x3f, -4);
            this.buyNumber.Size = new Size(70, 30);
            this.buyNumber.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.buyNumber.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.buySubWindow.addControl(this.buyNumber);
            this.buyCostValue.Text = "0";
            this.buyCostValue.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.buyCostValue.Position = new Point(0x3f, 13);
            this.buyCostValue.Size = new Size(70, 30);
            this.buyCostValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.buyCostValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.buySubWindow.addControl(this.buyCostValue);
            this.sendEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.MoveOnClick = true;
            this.sendEditButton.OverBrighten = true;
            this.sendEditButton.Position = new Point(7, 5);
            this.sendEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "CapitalTradePanel_editValue");
            this.buySubWindow.addControl(this.sendEditButton);
            this.buyButton.Position = new Point(0xb1, 0x5e);
            this.buyButton.Size = new Size(0x99, 0x26);
            this.buyButton.Text.Text = SK.Text("CapitalTradePanel_Buy", "Buy");
            this.buyButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.buyButton.TextYOffset = -1;
            this.buyButton.Text.Color = ARGBColors.Black;
            this.buyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyClick), "CapitalTradePanel_buy");
            this.buyWindow.addControl(this.buyButton);
            this.buyButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.buyButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.buyTrack.Position = new Point(0x15, 0x29);
            this.buyTrack.Margin = new Rectangle(3, -1, 1, 0);
            this.buyTrack.Value = 0;
            this.buyTrack.Max = 1;
            this.buyTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
            this.buyWindow.addControl(this.buyTrack);
            this.buyTrack.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.int_slidebar_thumb_middle_normal, (Image) GFXLibrary.int_slidebar_thumb_left_normal, (Image) GFXLibrary.int_slidebar_thumb_right_normal, (Image) GFXLibrary.int_slidebar_thumb_middle_in, (Image) GFXLibrary.int_slidebar_thumb_middle_over);
            this.buyMin.Text = "0";
            this.buyMin.Color = ARGBColors.Black;
            this.buyMin.Position = new Point(-2, 0x4a);
            this.buyMin.Size = new Size(50, 30);
            this.buyMin.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.buyMin.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.buyWindow.addControl(this.buyMin);
            this.buyMax.Text = "0";
            this.buyMax.Color = ARGBColors.Black;
            this.buyMax.Position = new Point(0x7e, 0x4a);
            this.buyMax.Size = new Size(50, 30);
            this.buyMax.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.buyMax.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.buyWindow.addControl(this.buyMax);
            this.highlightLine1.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine1.Position = new Point(0x99, (0x6f - num) + 5);
            this.highlightLine1.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine1);
            this.highlightLine2.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine2.Position = new Point(0x99, ((0x6f - num) + 5) + 40);
            this.highlightLine2.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine2);
            this.highlightLine3.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine3.Position = new Point(0x99, ((0x6f - num) + 5) + 80);
            this.highlightLine3.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine3);
            this.highlightLine4.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine4.Position = new Point(0x99, ((0x6f - num) + 5) + 120);
            this.highlightLine4.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine4);
            this.highlightLine5.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine5.Position = new Point(0x99, 0x10f);
            this.highlightLine5.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine5);
            this.highlightLine6.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine6.Position = new Point(0x99, 0x137);
            this.highlightLine6.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine6);
            this.highlightLine7.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine7.Position = new Point(0x99, 0x15f);
            this.highlightLine7.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine7);
            this.highlightLine8.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine8.Position = new Point(0x99, 0x187);
            this.highlightLine8.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine8);
            this.selectRow1.Position = new Point(-134, -3);
            this.selectRow1.Size = new Size(0xbf, 0x26);
            this.selectRow1.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow1.Text.Position = new Point(0x5b, 0);
            this.selectRow1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow1.TextYOffset = -1;
            this.selectRow1.Text.Color = ARGBColors.Black;
            this.selectRow1.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow1.createSubText("0");
            this.selectRow1.Text2.Size = new Size(0x2e, this.selectRow1.Text2.Size.Height);
            this.selectRow1.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine1.addControl(this.selectRow1);
            this.selectRow1.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow1.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow2.Position = new Point(-134, -3);
            this.selectRow2.Size = new Size(0xbf, 0x26);
            this.selectRow2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow2.Text.Position = new Point(0x5b, 0);
            this.selectRow2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow2.TextYOffset = -1;
            this.selectRow2.Text.Color = ARGBColors.Black;
            this.selectRow2.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow2.createSubText("0");
            this.selectRow2.Text2.Size = new Size(0x2e, this.selectRow2.Text2.Size.Height);
            this.selectRow2.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine2.addControl(this.selectRow2);
            this.selectRow2.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow2.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow3.Position = new Point(-134, -3);
            this.selectRow3.Size = new Size(0xbf, 0x26);
            this.selectRow3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow3.Text.Position = new Point(0x5b, 0);
            this.selectRow3.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow3.TextYOffset = -1;
            this.selectRow3.Text.Color = ARGBColors.Black;
            this.selectRow3.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow3.createSubText("0");
            this.selectRow3.Text2.Size = new Size(0x2e, this.selectRow3.Text2.Size.Height);
            this.selectRow3.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine3.addControl(this.selectRow3);
            this.selectRow3.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow3.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow4.Position = new Point(-134, -3);
            this.selectRow4.Size = new Size(0xbf, 0x26);
            this.selectRow4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow4.Text.Position = new Point(0x5b, 0);
            this.selectRow4.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow4.TextYOffset = -1;
            this.selectRow4.Text.Color = ARGBColors.Black;
            this.selectRow4.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow4.createSubText("0");
            this.selectRow4.Text2.Size = new Size(0x2e, this.selectRow4.Text2.Size.Height);
            this.selectRow4.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine4.addControl(this.selectRow4);
            this.selectRow4.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow4.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow5.Position = new Point(-134, -3);
            this.selectRow5.Size = new Size(0xbf, 0x26);
            this.selectRow5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow5.Text.Position = new Point(0x5b, 0);
            this.selectRow5.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow5.TextYOffset = -1;
            this.selectRow5.Text.Color = ARGBColors.Black;
            this.selectRow5.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow5.createSubText("0");
            this.selectRow5.Text2.Size = new Size(0x2e, this.selectRow5.Text2.Size.Height);
            this.selectRow5.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine5.addControl(this.selectRow5);
            this.selectRow5.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow5.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow6.Position = new Point(-134, -3);
            this.selectRow6.Size = new Size(0xbf, 0x26);
            this.selectRow6.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow6.Text.Position = new Point(0x5b, 0);
            this.selectRow6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow6.TextYOffset = -1;
            this.selectRow6.Text.Color = ARGBColors.Black;
            this.selectRow6.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow6.createSubText("0");
            this.selectRow6.Text2.Size = new Size(0x2e, this.selectRow6.Text2.Size.Height);
            this.selectRow6.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine6.addControl(this.selectRow6);
            this.selectRow6.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow6.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow7.Position = new Point(-134, -3);
            this.selectRow7.Size = new Size(0xbf, 0x26);
            this.selectRow7.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow7.Text.Position = new Point(0x5b, 0);
            this.selectRow7.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow7.TextYOffset = -1;
            this.selectRow7.Text.Color = ARGBColors.Black;
            this.selectRow7.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow7.createSubText("0");
            this.selectRow7.Text2.Size = new Size(0x2e, this.selectRow7.Text2.Size.Height);
            this.selectRow7.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine7.addControl(this.selectRow7);
            this.selectRow7.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow7.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow8.Position = new Point(-134, -3);
            this.selectRow8.Size = new Size(0xbf, 0x26);
            this.selectRow8.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow8.Text.Position = new Point(0x5b, 0);
            this.selectRow8.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow8.TextYOffset = -1;
            this.selectRow8.Text.Color = ARGBColors.Black;
            this.selectRow8.ImageIconPosition = new Point(0x2e, -3);
            this.selectRow8.createSubText("0");
            this.selectRow8.Text2.Size = new Size(0x2e, this.selectRow8.Text2.Size.Height);
            this.selectRow8.Text2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.selectRow8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine8.addControl(this.selectRow8);
            this.selectRow8.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow8.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.localLabel1.Text = "0";
            this.localLabel1.Color = ARGBColors.Black;
            this.localLabel1.Position = new Point(0x3f, 1);
            this.localLabel1.Size = new Size(0x61, 0x1f);
            this.localLabel1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine1.addControl(this.localLabel1);
            this.localLabel2.Text = "0";
            this.localLabel2.Color = ARGBColors.Black;
            this.localLabel2.Position = new Point(0x3f, 1);
            this.localLabel2.Size = new Size(0x61, 0x1f);
            this.localLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine2.addControl(this.localLabel2);
            this.localLabel3.Text = "0";
            this.localLabel3.Color = ARGBColors.Black;
            this.localLabel3.Position = new Point(0x3f, 1);
            this.localLabel3.Size = new Size(0x61, 0x1f);
            this.localLabel3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine3.addControl(this.localLabel3);
            this.localLabel4.Text = "0";
            this.localLabel4.Color = ARGBColors.Black;
            this.localLabel4.Position = new Point(0x3f, 1);
            this.localLabel4.Size = new Size(0x61, 0x1f);
            this.localLabel4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine4.addControl(this.localLabel4);
            this.localLabel5.Text = "0";
            this.localLabel5.Color = ARGBColors.Black;
            this.localLabel5.Position = new Point(0x3f, 1);
            this.localLabel5.Size = new Size(0x61, 0x1f);
            this.localLabel5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine5.addControl(this.localLabel5);
            this.localLabel6.Text = "0";
            this.localLabel6.Color = ARGBColors.Black;
            this.localLabel6.Position = new Point(0x3f, 1);
            this.localLabel6.Size = new Size(0x61, 0x1f);
            this.localLabel6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine6.addControl(this.localLabel6);
            this.localLabel7.Text = "0";
            this.localLabel7.Color = ARGBColors.Black;
            this.localLabel7.Position = new Point(0x3f, 1);
            this.localLabel7.Size = new Size(0x61, 0x1f);
            this.localLabel7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine7.addControl(this.localLabel7);
            this.localLabel8.Text = "0";
            this.localLabel8.Color = ARGBColors.Black;
            this.localLabel8.Position = new Point(0x3f, 1);
            this.localLabel8.Size = new Size(0x61, 0x1f);
            this.localLabel8.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.localLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine8.addControl(this.localLabel8);
            this.storedLabel1.Text = "0";
            this.storedLabel1.Color = ARGBColors.Black;
            this.storedLabel1.Position = new Point(0xc6, 1);
            this.storedLabel1.Size = new Size(0x61, 0x1f);
            this.storedLabel1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine1.addControl(this.storedLabel1);
            this.storedLabel2.Text = "0";
            this.storedLabel2.Color = ARGBColors.Black;
            this.storedLabel2.Position = new Point(0xc6, 1);
            this.storedLabel2.Size = new Size(0x61, 0x1f);
            this.storedLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine2.addControl(this.storedLabel2);
            this.storedLabel3.Text = "0";
            this.storedLabel3.Color = ARGBColors.Black;
            this.storedLabel3.Position = new Point(0xc6, 1);
            this.storedLabel3.Size = new Size(0x61, 0x1f);
            this.storedLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine3.addControl(this.storedLabel3);
            this.storedLabel4.Text = "0";
            this.storedLabel4.Color = ARGBColors.Black;
            this.storedLabel4.Position = new Point(0xc6, 1);
            this.storedLabel4.Size = new Size(0x61, 0x1f);
            this.storedLabel4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine4.addControl(this.storedLabel4);
            this.storedLabel5.Text = "0";
            this.storedLabel5.Color = ARGBColors.Black;
            this.storedLabel5.Position = new Point(0xc6, 1);
            this.storedLabel5.Size = new Size(0x61, 0x1f);
            this.storedLabel5.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine5.addControl(this.storedLabel5);
            this.storedLabel6.Text = "0";
            this.storedLabel6.Color = ARGBColors.Black;
            this.storedLabel6.Position = new Point(0xc6, 1);
            this.storedLabel6.Size = new Size(0x61, 0x1f);
            this.storedLabel6.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine6.addControl(this.storedLabel6);
            this.storedLabel7.Text = "0";
            this.storedLabel7.Color = ARGBColors.Black;
            this.storedLabel7.Position = new Point(0xc6, 1);
            this.storedLabel7.Size = new Size(0x61, 0x1f);
            this.storedLabel7.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine7.addControl(this.storedLabel7);
            this.storedLabel8.Text = "0";
            this.storedLabel8.Color = ARGBColors.Black;
            this.storedLabel8.Position = new Point(0xc6, 1);
            this.storedLabel8.Size = new Size(0x61, 0x1f);
            this.storedLabel8.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine8.addControl(this.storedLabel8);
            this.priceLabel1.Text = "0";
            this.priceLabel1.Color = ARGBColors.Black;
            this.priceLabel1.Position = new Point(0x132, 1);
            this.priceLabel1.Size = new Size(0x4d, 0x1f);
            this.priceLabel1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine1.addControl(this.priceLabel1);
            this.priceLabel2.Text = "0";
            this.priceLabel2.Color = ARGBColors.Black;
            this.priceLabel2.Position = new Point(0x132, 1);
            this.priceLabel2.Size = new Size(0x4d, 0x1f);
            this.priceLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine2.addControl(this.priceLabel2);
            this.priceLabel3.Text = "0";
            this.priceLabel3.Color = ARGBColors.Black;
            this.priceLabel3.Position = new Point(0x132, 1);
            this.priceLabel3.Size = new Size(0x4d, 0x1f);
            this.priceLabel3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine3.addControl(this.priceLabel3);
            this.priceLabel4.Text = "0";
            this.priceLabel4.Color = ARGBColors.Black;
            this.priceLabel4.Position = new Point(0x132, 1);
            this.priceLabel4.Size = new Size(0x4d, 0x1f);
            this.priceLabel4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine4.addControl(this.priceLabel4);
            this.priceLabel5.Text = "0";
            this.priceLabel5.Color = ARGBColors.Black;
            this.priceLabel5.Position = new Point(0x132, 1);
            this.priceLabel5.Size = new Size(0x4d, 0x1f);
            this.priceLabel5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine5.addControl(this.priceLabel5);
            this.priceLabel6.Text = "0";
            this.priceLabel6.Color = ARGBColors.Black;
            this.priceLabel6.Position = new Point(0x132, 1);
            this.priceLabel6.Size = new Size(0x4d, 0x1f);
            this.priceLabel6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine6.addControl(this.priceLabel6);
            this.priceLabel7.Text = "0";
            this.priceLabel7.Color = ARGBColors.Black;
            this.priceLabel7.Position = new Point(0x132, 1);
            this.priceLabel7.Size = new Size(0x4d, 0x1f);
            this.priceLabel7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine7.addControl(this.priceLabel7);
            this.priceLabel8.Text = "0";
            this.priceLabel8.Color = ARGBColors.Black;
            this.priceLabel8.Position = new Point(0x132, 1);
            this.priceLabel8.Size = new Size(0x4d, 0x1f);
            this.priceLabel8.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine8.addControl(this.priceLabel8);
            this.lastTab = -1;
            this.manageTabs(1);
            if (this.selectedStockExchange >= 0)
            {
                this.resetBackupData();
                this.selectStockExchange(this.selectedStockExchange);
                this.selectHighlightLine(0);
            }
            this.update();
        }

        private void initArmouryTab()
        {
        }

        private void initGranaryTab()
        {
        }

        private void initHallTab()
        {
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CapitalTradePanel";
            base.Size = new Size(0x3e0, 0x236);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        private void initStockpileTab()
        {
            this.highlightLine1.Visible = true;
            this.highlightLine2.Visible = true;
            this.highlightLine3.Visible = true;
            this.highlightLine4.Visible = true;
            this.highlightLine5.Visible = false;
            this.highlightLine6.Visible = false;
            this.highlightLine7.Visible = false;
            this.highlightLine8.Visible = false;
            this.setRowInfo(0, 6);
            this.setRowInfo(1, 7);
            this.setRowInfo(2, 8);
            this.setRowInfo(3, 9);
            this.setRowInfo(4, 12);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void manageTabs(int tabID)
        {
            if (tabID != this.lastTab)
            {
                this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
                this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
                if (tabID == 1)
                {
                    this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_selected;
                    this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_selected;
                    this.selectHighlightLine(0);
                    this.initStockpileTab();
                    this.selectHighlightLine(0);
                }
                this.lastTab = tabID;
                base.Invalidate();
            }
        }

        public void resetBackupData()
        {
            this.BACKUP_resource = -1;
            this.BACKUP_buyLevel = 0xc350;
        }

        private void rowClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data != this.currentResource)
                {
                    this.buyTrack.Max = 0xc350;
                    this.buyTrack.Value = 0xc350;
                    GameEngine.Instance.playInterfaceSound("CapitalTradePanel_line_clicked");
                    this.selectHighlightLine(this.getLineFromResource(clickedControl.Data));
                    base.Invalidate();
                }
            }
        }

        private void selectHighlightLine(int line)
        {
            this.highlightLine1.Image = null;
            this.highlightLine2.Image = null;
            this.highlightLine3.Image = null;
            this.highlightLine4.Image = null;
            this.highlightLine5.Image = null;
            this.highlightLine6.Image = null;
            this.highlightLine7.Image = null;
            this.highlightLine8.Image = null;
            CustomSelfDrawPanel.CSDButton button = this.getRowButton(line);
            this.currentResource = button.Data;
            CustomSelfDrawPanel.CSDImage image = this.getRowHighlight(line);
            image.Image = (Image) GFXLibrary.int_white_highlight_bar;
            image.Size = new Size(400, 0x1f);
            this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
            this.buyHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy", "Buy") + " : " + VillageBuildingsData.getResourceNames(this.currentResource);
            this.buyHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
            this.buyTrack.Max = 0xc350;
            this.buyTrack.Value = 0xc350;
            this.showBuySellWindow();
        }

        public void selectStockExchange(int villageID)
        {
            if (villageID < 0)
            {
                this.selectedStockExchange = -1;
            }
            else
            {
                this.selectedStockExchange = villageID;
                RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(new RemoteServices.GetStockExchangeData_UserCallBack(this.getStockExchangeDataCallback));
                RemoteServices.Instance.GetStockExchangeData(villageID, true);
                this.currentResource = this.BACKUP_resource;
                if (this.BACKUP_resource >= 0)
                {
                    this.lastTab = -1;
                    switch (this.BACKUP_resource)
                    {
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 12:
                            this.manageTabs(1);
                            break;

                        case 13:
                        case 14:
                        case 15:
                        case 0x10:
                        case 0x11:
                        case 0x12:
                            this.manageTabs(2);
                            break;

                        case 0x13:
                        case 0x15:
                        case 0x16:
                        case 0x17:
                        case 0x18:
                        case 0x19:
                        case 0x1a:
                        case 0x21:
                            this.manageTabs(4);
                            break;

                        case 0x1c:
                        case 0x1d:
                        case 30:
                        case 0x1f:
                        case 0x20:
                            this.manageTabs(3);
                            break;
                    }
                    int line = this.getLineFromResource(this.BACKUP_resource);
                    this.selectHighlightLine(line);
                    this.buyTrack.Max = 0x7a120;
                    this.buyTrack.Value = this.BACKUP_buyLevel;
                }
                this.updateValues();
            }
        }

        private void setRowInfo(int line, int resource)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            CustomSelfDrawPanel.CSDButton button = this.getRowButton(line);
            button.ImageIcon = (Image) GFXLibrary.getCommodity32DSImage(resource);
            button.Text.Text = VillageBuildingsData.getResourceNames(resource);
            button.Data = resource;
            button.Text2.Text = GameEngine.Instance.LocalWorldData.traderCarryingLevels[resource].ToString("N", nFI);
            if (Program.mySettings.LanguageIdent == "pt")
            {
                if ((resource == 0x16) || (resource == 0x1a))
                {
                    button.Size = new Size(0xbf, 0x26);
                    button.UseTextSize = true;
                    button.Text.Size = new Size(100, 0x26);
                    button.Text.Position = new Point(0x5b, 0);
                }
                else
                {
                    button.Size = new Size(0xbf, 0x26);
                    button.UseTextSize = false;
                    button.Text.Size = button.Size;
                    button.Text.Position = new Point(0x5b, 0);
                }
            }
            else
            {
                button.Size = new Size(0xbf, 0x26);
                button.UseTextSize = false;
                button.Text.Size = button.Size;
                button.Text.Position = new Point(0x5b, 0);
            }
        }

        private void setRowValues(int row, int localValue, int stockLevel, int priceValue)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            CustomSelfDrawPanel.CSDLabel label = this.getRowLocal(row);
            label.Text = "";
            if (localValue >= 0)
            {
                label.Text = localValue.ToString("N", nFI);
            }
            CustomSelfDrawPanel.CSDLabel label2 = this.getRowStored(row);
            label2.Text = "";
            if (stockLevel >= 0)
            {
                label2.Text = stockLevel.ToString("N", nFI);
            }
            CustomSelfDrawPanel.CSDLabel label3 = this.getRowPrice(row);
            label3.Text = "";
            if (priceValue >= 0)
            {
                label3.Text = priceValue.ToString("N", nFI);
            }
        }

        private void showBuySellWindow()
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            bool visible = this.buyWindow.Visible;
            this.buyWindow.Visible = false;
            VillageMap village = GameEngine.Instance.Village;
            if ((((village != null) && (this.currentResource >= 0)) && (GameEngine.Instance.World.isUserVillage(village.VillageID) && (this.selectedStockExchange >= 0))) && (this.stockExchanges[this.selectedStockExchange] != null))
            {
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                StockExchangeInfo info2 = (StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
                village.getResourceLevel(this.currentResource);
                int storedLevel = info2.getLevel(this.currentResource);
                int num2 = TradingCalcs.calcGoldCost(localWorldData, storedLevel, this.currentResource, storedLevel + this.buyTrack.Value);
                if (storedLevel > 0)
                {
                    this.buyWindow.Visible = true;
                    int num3 = (int) GameEngine.Instance.World.getCurrentGold();
                    int num4 = storedLevel;
                    int num1 = num3 / num2;
                    int max = this.buyTrack.Max;
                    if (num4 > max)
                    {
                        this.buyTrack.Max = num4;
                    }
                    else if (num4 < max)
                    {
                        if (this.buyTrack.Value > num4)
                        {
                            this.buyTrack.Value = num4;
                        }
                        this.buyTrack.Max = num4;
                    }
                    int num6 = (this.buyTrack.Value * num2) / this.currentResourcePacketSize;
                    if ((num6 <= 0) && (this.buyTrack.Value > 0))
                    {
                        num6 = 1;
                    }
                    this.buyCostValue.Text = num6.ToString("N", nFI);
                    this.buyNumber.Text = this.buyTrack.Value.ToString("N", nFI);
                    this.buyMax.Text = this.buyTrack.Max.ToString("N", nFI);
                }
            }
            this.validateBuySellButtons();
            if (visible != this.buyWindow.Visible)
            {
                this.mainBackgroundImage.invalidate();
            }
        }

        private void swipeLeft()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabLeft();
        }

        private void swiperight()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabRight();
        }

        private void tabClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data != this.lastTab)
                {
                    this.manageTabs(clickedControl.Data);
                }
            }
        }

        private void tracksMoved()
        {
            this.showBuySellWindow();
            this.buyWindow.invalidate();
        }

        public void update()
        {
            this.updateValues();
        }

        public void updateValues()
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                if (this.selectedStockExchange < 0)
                {
                    this.selectStockExchange(village.VillageID);
                }
                StockExchangeInfo info = null;
                if ((this.selectedStockExchange >= 0) && (this.stockExchanges[this.selectedStockExchange] != null))
                {
                    info = (StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
                }
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                if (this.lastTab == 1)
                {
                    VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                    village.getStockpileLevels(levels);
                    VillageMap.InnLevels levels2 = new VillageMap.InnLevels();
                    village.getInnLevels(levels2);
                    if (info == null)
                    {
                        this.setRowValues(0, (int) levels.woodLevel, -1, -1);
                        this.setRowValues(1, (int) levels.stoneLevel, -1, -1);
                        this.setRowValues(2, (int) levels.ironLevel, -1, -1);
                        this.setRowValues(3, (int) levels.pitchLevel, -1, -1);
                    }
                    else
                    {
                        this.setRowValues(0, (int) levels.woodLevel, info.woodLevel, TradingCalcs.calcSellCost(localWorldData, info.getFakeLevel(6), 6));
                        this.setRowValues(1, (int) levels.stoneLevel, info.stoneLevel, TradingCalcs.calcSellCost(localWorldData, info.getFakeLevel(7), 7));
                        this.setRowValues(2, (int) levels.ironLevel, info.ironLevel, TradingCalcs.calcSellCost(localWorldData, info.getFakeLevel(8), 8));
                        this.setRowValues(3, (int) levels.pitchLevel, info.pitchLevel, TradingCalcs.calcSellCost(localWorldData, info.getFakeLevel(9), 9));
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    this.setRowValues(i, -1, -1, -1);
                }
            }
            this.showBuySellWindow();
        }

        private void validateBuySellButtons()
        {
            if (this.buyWindow.Visible && (this.buyTrack.Value > 0))
            {
                this.buyButton.Enabled = true;
            }
            else
            {
                this.buyButton.Enabled = false;
            }
        }

        public static List<WorldMap.VillageNameItem> ExchangeFavourites
        {
            get
            {
                return exchangeFavourites;
            }
        }

        public static List<WorldMap.VillageNameItem> ExchangeHistory
        {
            get
            {
                return exchangeHistory;
            }
        }

        public class StockExchangeInfo
        {
            public int aleLevel;
            public int applesLevel;
            public int armourLevel;
            public int bowsLevel;
            public int breadLevel;
            public int catapultsLevel;
            public int cheeseLevel;
            public int clothesLevel;
            public int fishLevel;
            public int furnitureLevel;
            public int ironLevel;
            public int meatLevel;
            public int metalwareLevel;
            public int pikesLevel;
            public int pitchLevel;
            public int saltLevel;
            public int silkLevel;
            public int spicesLevel;
            public int stoneLevel;
            public int swordsLevel;
            public int vegLevel;
            public int venisonLevel;
            public int villageID = -1;
            public int wineLevel;
            public int woodLevel;

            public int getFakeLevel(int resource)
            {
                return this.getLevel(resource);
            }

            public int getLevel(int resource)
            {
                switch (resource)
                {
                    case 6:
                        return this.woodLevel;

                    case 7:
                        return this.stoneLevel;

                    case 8:
                        return this.ironLevel;

                    case 9:
                        return this.pitchLevel;

                    case 12:
                        return this.aleLevel;

                    case 13:
                        return this.applesLevel;

                    case 14:
                        return this.breadLevel;

                    case 15:
                        return this.vegLevel;

                    case 0x10:
                        return this.meatLevel;

                    case 0x11:
                        return this.cheeseLevel;

                    case 0x12:
                        return this.fishLevel;

                    case 0x13:
                        return this.clothesLevel;

                    case 0x15:
                        return this.furnitureLevel;

                    case 0x16:
                        return this.venisonLevel;

                    case 0x17:
                        return this.saltLevel;

                    case 0x18:
                        return this.spicesLevel;

                    case 0x19:
                        return this.silkLevel;

                    case 0x1a:
                        return this.metalwareLevel;

                    case 0x1c:
                        return this.pikesLevel;

                    case 0x1d:
                        return this.bowsLevel;

                    case 30:
                        return this.swordsLevel;

                    case 0x1f:
                        return this.armourLevel;

                    case 0x20:
                        return this.catapultsLevel;

                    case 0x21:
                        return this.wineLevel;
                }
                return 0;
            }
        }
    }
}

