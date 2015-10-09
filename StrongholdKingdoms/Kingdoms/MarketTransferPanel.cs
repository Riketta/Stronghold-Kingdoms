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

    public class MarketTransferPanel : CustomSelfDrawPanel, IDockableControl
    {
        private int BACKUP_resource = -1;
        private int BACKUP_sendLevel;
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private int currentResource = -1;
        private int currentResourcePacketSize = 1;
        private CustomSelfDrawPanel.CSDExtendingPanel deliveryTimeArea = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel deliveryTimeAreaLabel = new CustomSelfDrawPanel.CSDLabel();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton exchangeArrowButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel exchangeNameBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel exchangeNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage highlightLine1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel infoWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        public static MarketTransferPanel instance = null;
        private int lastTab = -1;
        private DateTime lastTradeTime = DateTime.MinValue;
        private StockExchangePanel.StockExchangeInfo lastVillageData;
        private CustomSelfDrawPanel.CSDExtendingPanel leftWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea1 = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea2 = new CustomSelfDrawPanel.CSDExtendingPanel();
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
        private CustomSelfDrawPanel.CSDButton newTradingButton = new CustomSelfDrawPanel.CSDButton();
        private int numFreeTraders;
        private int numTraders;
        private int selectedTargetVillage = -1;
        private CustomSelfDrawPanel.CSDButton selectRow1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sendButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton sendEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage sendHeadingImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel sendHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sendMax = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sendMin = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sendNumber = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sendNumberPackets = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel sendSubWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDTrackBar sendTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDExtendingPanel sendWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
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
        private CustomSelfDrawPanel.CSDButton tabButton2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabButton3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabButton4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel traderCapacityLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel traderCapacityValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage traderIconImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel tradersAvailableLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel tradersAvailableValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage tradeWithImage = new CustomSelfDrawPanel.CSDImage();
        private static List<WorldMap.VillageNameItem> villageFavourites = new List<WorldMap.VillageNameItem>();
        private static List<WorldMap.VillageNameItem> villageHistory = new List<WorldMap.VillageNameItem>();
        public static VillageHistoryComparer villageHistoryComparer = new VillageHistoryComparer();
        private CustomSelfDrawPanel.CSDButton villageOwnPageDown = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageOwnPageUp = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage villageSelectPanel = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel villageSelectPanelLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton villageSelectPanelTab1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectPanelTab2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage10 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage10Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage10Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage11 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage11Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage11Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage12 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage12Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage12Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage13 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage13Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage13Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage14 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage14Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage14Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage15 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage15Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage15Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage16 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage16Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage16Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage17 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage17Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage17Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage1Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage1Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage2Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage2Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage3Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage3Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage4Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage4Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage5Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage5Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage6Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage6Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage7Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage7Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage8Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage8Favourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage9 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage9Delete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage9Favourite = new CustomSelfDrawPanel.CSDButton();
        private int villageTabMode;
        private int villageTabOwnPage;
        private CustomSelfDrawPanel.CSDButton worldMapButton = new CustomSelfDrawPanel.CSDButton();

        public MarketTransferPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public static void addFavourites(List<GenericVillageHistoryData> newData)
        {
            villageFavourites.Clear();
            if (newData != null)
            {
                foreach (GenericVillageHistoryData data in newData)
                {
                    WorldMap.VillageNameItem item = new WorldMap.VillageNameItem {
                        villageID = data.villageID
                    };
                    villageFavourites.Add(item);
                }
            }
        }

        public static void addHistory(List<GenericVillageHistoryData> newData)
        {
            villageHistory.Clear();
            if (newData != null)
            {
                foreach (GenericVillageHistoryData data in newData)
                {
                    WorldMap.VillageNameItem item = new WorldMap.VillageNameItem {
                        villageID = data.villageID,
                        villageName = GameEngine.Instance.World.getVillageName(data.villageID)
                    };
                    villageHistory.Add(item);
                }
            }
        }

        private void addVillageToHistory(int villageID)
        {
            bool flag = false;
            foreach (WorldMap.VillageNameItem item in villageHistory)
            {
                if (item.villageID == villageID)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                WorldMap.VillageNameItem item2 = new WorldMap.VillageNameItem {
                    villageID = villageID,
                    villageName = GameEngine.Instance.World.getVillageName(villageID)
                };
                villageHistory.Add(item2);
                this.updateVillageHistory();
            }
        }

        public void backupData()
        {
            this.BACKUP_resource = this.currentResource;
            this.BACKUP_sendLevel = this.sendTrack.Value;
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

        public void dirtyStockExchangeInfo(int selectedStockExchange)
        {
            if (this.stockExchanges[selectedStockExchange] != null)
            {
                StockExchangePanel.StockExchangeInfo info = (StockExchangePanel.StockExchangeInfo) this.stockExchanges[selectedStockExchange];
                info.lastTime = DateTime.MinValue;
            }
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
            Point point = InterfaceMgr.Instance.ParentForm.PointToScreen(new Point((620 + base.Location.X) + 0xd9, ((360 + base.Location.Y) + 120) - 50));
            FloatingInput.open(point.X, point.Y, this.sendTrack.Value, this.sendTrack.Max, InterfaceMgr.Instance.ParentForm);
        }

        private void exchangeArrowClick()
        {
            if (this.exchangeArrowButton.Data == 0)
            {
                GameEngine.Instance.playInterfaceSound("MarketTransferPanel_village_list_open");
                this.showVillagePanel(true);
            }
            else
            {
                GameEngine.Instance.playInterfaceSound("MarketTransferPanel_village_list_close");
                this.showVillagePanel(false);
            }
        }

        private void findOnWorldClicked()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.BACKUP_resource = this.currentResource;
                this.BACKUP_sendLevel = this.sendTrack.Value;
                GameEngine.Instance.World.zoomToVillage(village.VillageID);
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(3);
            }
        }

        private void floatingValueCB(int value)
        {
            this.sendTrack.Value = value;
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
                StockExchangePanel.StockExchangeInfo info = new StockExchangePanel.StockExchangeInfo {
                    villageID = returnData.villageID,
                    lastTime = DateTime.Now,
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
                this.lastVillageData = info;
                this.stockExchanges[returnData.villageID] = info;
                this.updateValues();
            }
        }

        private CustomSelfDrawPanel.CSDButton getVillageHistory(int line)
        {
            switch (line)
            {
                case 0:
                    return this.villageSelectVillage1;

                case 1:
                    return this.villageSelectVillage2;

                case 2:
                    return this.villageSelectVillage3;

                case 3:
                    return this.villageSelectVillage4;

                case 4:
                    return this.villageSelectVillage5;

                case 5:
                    return this.villageSelectVillage6;

                case 6:
                    return this.villageSelectVillage7;

                case 7:
                    return this.villageSelectVillage8;

                case 8:
                    return this.villageSelectVillage9;

                case 9:
                    return this.villageSelectVillage10;

                case 10:
                    return this.villageSelectVillage11;

                case 11:
                    return this.villageSelectVillage12;

                case 12:
                    return this.villageSelectVillage13;

                case 13:
                    return this.villageSelectVillage14;

                case 14:
                    return this.villageSelectVillage15;

                case 15:
                    return this.villageSelectVillage16;

                case 0x10:
                    return this.villageSelectVillage17;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDButton getVillageHistoryDelete(int line)
        {
            switch (line)
            {
                case 0:
                    return this.villageSelectVillage1Delete;

                case 1:
                    return this.villageSelectVillage2Delete;

                case 2:
                    return this.villageSelectVillage3Delete;

                case 3:
                    return this.villageSelectVillage4Delete;

                case 4:
                    return this.villageSelectVillage5Delete;

                case 5:
                    return this.villageSelectVillage6Delete;

                case 6:
                    return this.villageSelectVillage7Delete;

                case 7:
                    return this.villageSelectVillage8Delete;

                case 8:
                    return this.villageSelectVillage9Delete;

                case 9:
                    return this.villageSelectVillage10Delete;

                case 10:
                    return this.villageSelectVillage11Delete;

                case 11:
                    return this.villageSelectVillage12Delete;

                case 12:
                    return this.villageSelectVillage13Delete;

                case 13:
                    return this.villageSelectVillage14Delete;

                case 14:
                    return this.villageSelectVillage15Delete;

                case 15:
                    return this.villageSelectVillage16Delete;

                case 0x10:
                    return this.villageSelectVillage17Delete;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDButton getVillageHistoryFavourite(int line)
        {
            switch (line)
            {
                case 0:
                    return this.villageSelectVillage1Favourite;

                case 1:
                    return this.villageSelectVillage2Favourite;

                case 2:
                    return this.villageSelectVillage3Favourite;

                case 3:
                    return this.villageSelectVillage4Favourite;

                case 4:
                    return this.villageSelectVillage5Favourite;

                case 5:
                    return this.villageSelectVillage6Favourite;

                case 6:
                    return this.villageSelectVillage7Favourite;

                case 7:
                    return this.villageSelectVillage8Favourite;

                case 8:
                    return this.villageSelectVillage9Favourite;

                case 9:
                    return this.villageSelectVillage10Favourite;

                case 10:
                    return this.villageSelectVillage11Favourite;

                case 11:
                    return this.villageSelectVillage12Favourite;

                case 12:
                    return this.villageSelectVillage13Favourite;

                case 13:
                    return this.villageSelectVillage14Favourite;

                case 14:
                    return this.villageSelectVillage15Favourite;

                case 15:
                    return this.villageSelectVillage16Favourite;

                case 0x10:
                    return this.villageSelectVillage17Favourite;
            }
            return null;
        }

        public void init()
        {
            instance = this;
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("MarketTradeScreen_Trade_With_Village", "Trade with Village"));
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MarketTransferPanel_close");
            this.closeButton.CustomTooltipID = 0x321;
            this.mainBackgroundArea.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 4, new Point(0x382, 10));
            this.midWindow.Size = new Size(0x125, 0x1c1);
            this.midWindow.Position = new Point(0x15d, 0x4a);
            this.mainBackgroundArea.addControl(this.midWindow);
            this.midWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.leftWindow.Size = new Size(0x14f, 0x1c1);
            this.leftWindow.Position = new Point(0x13, 0x4a);
            this.mainBackgroundArea.addControl(this.leftWindow);
            this.leftWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.newTradingButton.ImageNorm = (Image) GFXLibrary.se_tabs[0];
            this.newTradingButton.ImageOver = (Image) GFXLibrary.se_tabs[1];
            this.newTradingButton.ImageClick = (Image) GFXLibrary.se_tabs[1];
            this.newTradingButton.Position = new Point(20, -17);
            this.newTradingButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.stockExchangeClick), "MarketTransferPanel_stock_exchange");
            this.newTradingButton.ClickArea = new Rectangle(0x5f, 0, 0x5e, 0x19);
            this.newTradingButton.CustomTooltipID = 0x326;
            this.midWindow.addControl(this.newTradingButton);
            this.lightArea1.Size = new Size(0x61, 0x149);
            this.lightArea1.Position = new Point(0xd8, 0x66);
            this.leftWindow.addControl(this.lightArea1);
            this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.localHeadingLabel.Text = SK.Text("TRADE_Local", "Local");
            this.localHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.localHeadingLabel.Position = new Point(0, -35);
            this.localHeadingLabel.Size = new Size(0x61, 30);
            this.localHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.localHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea1.addControl(this.localHeadingLabel);
            this.lightArea2.Size = new Size(0xfb, 0x149);
            this.lightArea2.Position = new Point(0x15, 0x66);
            this.midWindow.addControl(this.lightArea2);
            this.lightArea2.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.storedHeadingLabel.Text = SK.Text("MarketTradeScreen_At_Target", "At Target");
            this.storedHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.storedHeadingLabel.Position = new Point(0, -35);
            this.storedHeadingLabel.Size = new Size(0xfb, 30);
            this.storedHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.storedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea2.addControl(this.storedHeadingLabel);
            this.exchangeNameBar.Size = new Size(270, 0x1f);
            this.exchangeNameBar.Position = new Point(11, 9);
            this.exchangeNameBar.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick));
            this.midWindow.addControl(this.exchangeNameBar);
            this.exchangeNameBar.Create((Image) GFXLibrary.int_lineitem_inset_left, (Image) GFXLibrary.int_lineitem_inset_middle, (Image) GFXLibrary.int_lineitem_inset_right);
            this.exchangeNameLabel.Text = SK.Text("TRADE_Select_Village", "Select Village");
            this.exchangeNameLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.exchangeNameLabel.Position = new Point(0x11, 7);
            this.exchangeNameLabel.Size = new Size((this.exchangeNameBar.Size.Width - 0x11) - 20, this.exchangeNameBar.Size.Height - 13);
            this.exchangeNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.exchangeNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.exchangeNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick));
            this.exchangeNameBar.addControl(this.exchangeNameLabel);
            this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
            this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
            this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
            this.exchangeArrowButton.Position = new Point(0xf6, 7);
            this.exchangeArrowButton.MoveOnClick = false;
            this.exchangeArrowButton.Data = 0;
            this.exchangeArrowButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick));
            this.exchangeNameBar.addControl(this.exchangeArrowButton);
            this.deliveryTimeArea.Size = new Size(0x102, 0x20);
            this.deliveryTimeArea.Position = new Point(0x10, 0x2b);
            this.midWindow.addControl(this.deliveryTimeArea);
            this.deliveryTimeArea.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.deliveryTimeAreaLabel.Text = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":   88m 44s";
            this.deliveryTimeAreaLabel.Color = ARGBColors.Black;
            this.deliveryTimeAreaLabel.Position = new Point(0, 0);
            this.deliveryTimeAreaLabel.Size = this.deliveryTimeArea.Size;
            this.deliveryTimeAreaLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.deliveryTimeAreaLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.deliveryTimeArea.addControl(this.deliveryTimeAreaLabel);
            this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
            this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
            this.tabButton1.Position = new Point(2, -13);
            this.tabButton1.MoveOnClick = false;
            this.tabButton1.Data = 1;
            this.tabButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "MarketTransferPanel_resources_tab");
            this.tabButton1.Enabled = true;
            this.tabButton1.CustomTooltipID = 0x322;
            this.leftWindow.addControl(this.tabButton1);
            this.tabButton2.ImageNorm = (Image) GFXLibrary.int_storage_tab_02_normal;
            this.tabButton2.ImageOver = (Image) GFXLibrary.int_storage_tab_02_over;
            this.tabButton2.Position = new Point(0x53, -13);
            this.tabButton2.MoveOnClick = false;
            this.tabButton2.Data = 2;
            this.tabButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "MarketTransferPanel_food_tab");
            this.tabButton2.Enabled = true;
            this.tabButton2.CustomTooltipID = 0x323;
            this.leftWindow.addControl(this.tabButton2);
            this.tabButton3.ImageNorm = (Image) GFXLibrary.int_storage_tab_03_normal;
            this.tabButton3.ImageOver = (Image) GFXLibrary.int_storage_tab_03_over;
            this.tabButton3.Position = new Point(0xa1, -13);
            this.tabButton3.MoveOnClick = false;
            this.tabButton3.Data = 3;
            this.tabButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "MarketTransferPanel_weapons_tab");
            this.tabButton3.Enabled = true;
            this.tabButton3.CustomTooltipID = 0x324;
            this.leftWindow.addControl(this.tabButton3);
            this.tabButton4.ImageNorm = (Image) GFXLibrary.int_storage_tab_04_normal;
            this.tabButton4.ImageOver = (Image) GFXLibrary.int_storage_tab_04_over;
            this.tabButton4.Position = new Point(0xef, -13);
            this.tabButton4.MoveOnClick = false;
            this.tabButton4.Data = 4;
            this.tabButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "MarketTransferPanel_banquetting_tab");
            this.tabButton4.Enabled = true;
            this.tabButton4.CustomTooltipID = 0x325;
            this.leftWindow.addControl(this.tabButton4);
            this.sendWindow.Size = new Size(0x150, 0x91);
            this.sendWindow.Position = new Point(0x27d, 0x110);
            this.mainBackgroundArea.addControl(this.sendWindow);
            this.sendWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.sendSubWindow.Size = new Size(0x93, 50);
            this.sendSubWindow.Position = new Point(0xb2, 0x20);
            this.sendWindow.addControl(this.sendSubWindow);
            this.sendSubWindow.Create((Image) GFXLibrary.int_insetpanel_b_top_left, (Image) GFXLibrary.int_insetpanel_b_middle_top, (Image) GFXLibrary.int_insetpanel_b_top_right, (Image) GFXLibrary.int_insetpanel_b_middle_left, (Image) GFXLibrary.int_insetpanel_b_middle, (Image) GFXLibrary.int_insetpanel_b_middle_right, (Image) GFXLibrary.int_insetpanel_b_bottom_left, (Image) GFXLibrary.int_insetpanel_b_middle_bottom, (Image) GFXLibrary.int_insetpanel_b_bottom_right);
            this.sendHeadingLabel.Text = SK.Text("MarketTradeScreen_Send", "Send") + " ";
            this.sendHeadingLabel.Color = ARGBColors.Black;
            this.sendHeadingLabel.Position = new Point(90, -30);
            this.sendHeadingLabel.Size = new Size(0xf6, 30);
            this.sendHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.sendHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
            this.sendWindow.addControl(this.sendHeadingLabel);
            this.sendHeadingImage.Image = null;
            this.sendHeadingImage.Position = new Point(5, -50);
            this.sendWindow.addControl(this.sendHeadingImage);
            this.sendNumber.Text = "0";
            this.sendNumber.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.sendNumber.Position = new Point(0x3f, -6);
            this.sendNumber.Size = new Size(70, 30);
            this.sendNumber.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sendNumber.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.sendSubWindow.addControl(this.sendNumber);
            this.sendEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.MoveOnClick = true;
            this.sendEditButton.OverBrighten = true;
            this.sendEditButton.Position = new Point(7, 5);
            this.sendEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "MarketTradeScreen_editValue");
            this.sendSubWindow.addControl(this.sendEditButton);
            this.sendButton.Position = new Point(0xb1, 0x5e);
            this.sendButton.Size = new Size(0x99, 0x26);
            this.sendButton.Text.Text = SK.Text("MarketTradeScreen_Send", "Send");
            this.sendButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sendButton.TextYOffset = -1;
            this.sendButton.Text.Color = ARGBColors.Black;
            this.sendButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendClick), "MarketTransferPanel_send");
            this.sendWindow.addControl(this.sendButton);
            this.sendButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.sendButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.sendTrack.Position = new Point(0x15, 0x29);
            this.sendTrack.Margin = new Rectangle(3, -1, 1, 0);
            this.sendTrack.Value = 0;
            this.sendTrack.Max = 1;
            this.sendTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
            this.sendWindow.addControl(this.sendTrack);
            this.sendTrack.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.int_slidebar_thumb_middle_normal, (Image) GFXLibrary.int_slidebar_thumb_left_normal, (Image) GFXLibrary.int_slidebar_thumb_right_normal, (Image) GFXLibrary.int_slidebar_thumb_middle_in, (Image) GFXLibrary.int_slidebar_thumb_middle_over);
            this.sendMin.Text = "0";
            this.sendMin.Color = ARGBColors.Black;
            this.sendMin.Position = new Point(-2, 0x4a);
            this.sendMin.Size = new Size(50, 30);
            this.sendMin.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.sendMin.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.sendWindow.addControl(this.sendMin);
            this.sendMax.Text = "0";
            this.sendMax.Color = ARGBColors.Black;
            this.sendMax.Position = new Point(0x7e, 0x4a);
            this.sendMax.Size = new Size(50, 30);
            this.sendMax.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.sendMax.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.sendWindow.addControl(this.sendMax);
            this.sendNumberPackets.Text = SK.Text("TradeScreen_Merchants", "Merchants") + " : 0";
            this.sendNumberPackets.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.sendNumberPackets.Position = new Point(0xa1, 0x2c);
            this.sendNumberPackets.Size = new Size(150, 30);
            this.sendNumberPackets.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sendNumberPackets.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.sendWindow.addControl(this.sendNumberPackets);
            this.highlightLine1.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine1.Position = new Point(0x99, 0x6f);
            this.highlightLine1.Size = new Size(0xb1, 0x1f);
            this.leftWindow.addControl(this.highlightLine1);
            this.highlightLine2.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine2.Position = new Point(0x99, 0x97);
            this.highlightLine2.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine2);
            this.highlightLine3.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine3.Position = new Point(0x99, 0xbf);
            this.highlightLine3.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine3);
            this.highlightLine4.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine4.Position = new Point(0x99, 0xe7);
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
            this.storedLabel1.Text = "?";
            this.storedLabel1.Color = ARGBColors.Black;
            this.storedLabel1.Position = new Point(0xc6, 1);
            this.storedLabel1.Size = new Size(0xfb, 0x1f);
            this.storedLabel1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine1.addControl(this.storedLabel1);
            this.storedLabel2.Text = "?";
            this.storedLabel2.Color = ARGBColors.Black;
            this.storedLabel2.Position = new Point(0xc6, 1);
            this.storedLabel2.Size = new Size(0xfb, 0x1f);
            this.storedLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine2.addControl(this.storedLabel2);
            this.storedLabel3.Text = "?";
            this.storedLabel3.Color = ARGBColors.Black;
            this.storedLabel3.Position = new Point(0xc6, 1);
            this.storedLabel3.Size = new Size(0xfb, 0x1f);
            this.storedLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine3.addControl(this.storedLabel3);
            this.storedLabel4.Text = "?";
            this.storedLabel4.Color = ARGBColors.Black;
            this.storedLabel4.Position = new Point(0xc6, 1);
            this.storedLabel4.Size = new Size(0xfb, 0x1f);
            this.storedLabel4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine4.addControl(this.storedLabel4);
            this.storedLabel5.Text = "?";
            this.storedLabel5.Color = ARGBColors.Black;
            this.storedLabel5.Position = new Point(0xc6, 1);
            this.storedLabel5.Size = new Size(0xfb, 0x1f);
            this.storedLabel5.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine5.addControl(this.storedLabel5);
            this.storedLabel6.Text = "?";
            this.storedLabel6.Color = ARGBColors.Black;
            this.storedLabel6.Position = new Point(0xc6, 1);
            this.storedLabel6.Size = new Size(0xfb, 0x1f);
            this.storedLabel6.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine6.addControl(this.storedLabel6);
            this.storedLabel7.Text = "?";
            this.storedLabel7.Color = ARGBColors.Black;
            this.storedLabel7.Position = new Point(0xc6, 1);
            this.storedLabel7.Size = new Size(0xfb, 0x1f);
            this.storedLabel7.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine7.addControl(this.storedLabel7);
            this.storedLabel8.Text = "?";
            this.storedLabel8.Color = ARGBColors.Black;
            this.storedLabel8.Position = new Point(0xc6, 1);
            this.storedLabel8.Size = new Size(0xfb, 0x1f);
            this.storedLabel8.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.storedLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.highlightLine8.addControl(this.storedLabel8);
            this.infoWindow.Size = new Size(0x150, 0x41);
            this.infoWindow.Position = new Point(0x27d, 0x1cb);
            this.mainBackgroundArea.addControl(this.infoWindow);
            this.infoWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.traderCapacityLabel.Text = SK.Text("MarketTradeScreen_Merchant_Capacity", "Merchant Capacity");
            this.traderCapacityLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.traderCapacityLabel.Position = new Point(0x69, -1);
            this.traderCapacityLabel.Size = new Size(0xe7, 30);
            this.traderCapacityLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.traderCapacityLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
            this.infoWindow.addControl(this.traderCapacityLabel);
            this.traderCapacityValue.Text = "0";
            this.traderCapacityValue.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.traderCapacityValue.Position = new Point(0xe8, -1);
            this.traderCapacityValue.Size = new Size(80, 30);
            this.traderCapacityValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.traderCapacityValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.infoWindow.addControl(this.traderCapacityValue);
            this.tradersAvailableLabel.Text = SK.Text("MarketTradeScreen_Merchant_Available", "Merchants Available");
            this.tradersAvailableLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.tradersAvailableLabel.Position = new Point(0x69, 0x12);
            this.tradersAvailableLabel.Size = new Size(0xe7, 30);
            this.tradersAvailableLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.tradersAvailableLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
            this.infoWindow.addControl(this.tradersAvailableLabel);
            this.tradersAvailableValue.Text = "0";
            this.tradersAvailableValue.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.tradersAvailableValue.Position = new Point(0xe8, 0x12);
            this.tradersAvailableValue.Size = new Size(80, 30);
            this.tradersAvailableValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.tradersAvailableValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.infoWindow.addControl(this.tradersAvailableValue);
            this.traderIconImage.Image = (Image) GFXLibrary.int_icon_trader;
            this.traderIconImage.Position = new Point(0x10, -26);
            this.infoWindow.addControl(this.traderIconImage);
            this.villageSelectPanel.Image = (Image) GFXLibrary.int_villagelist_panel;
            this.villageSelectPanel.Position = new Point(0x164, 0x6d);
            this.villageSelectPanel.Visible = false;
            this.mainBackgroundArea.addControl(this.villageSelectPanel);
            this.villageSelectPanelTab1.ImageNorm = (Image) GFXLibrary.tab_villagename_forward;
            this.villageSelectPanelTab1.ImageOver = (Image) GFXLibrary.tab_villagename_forward;
            this.villageSelectPanelTab1.ImageClick = (Image) GFXLibrary.tab_villagename_forward;
            this.villageSelectPanelTab1.Position = new Point(0, 3);
            this.villageSelectPanelTab1.Text.Text = SK.Text("MarketTradeScreen_Own", "Own");
            this.villageSelectPanelTab1.TextYOffset = -1;
            this.villageSelectPanelTab1.Data = 0;
            this.villageSelectPanelTab1.MoveOnClick = false;
            this.villageSelectPanelTab1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectPanelTab1.Text.Color = ARGBColors.Black;
            this.villageSelectPanelTab1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageTabClicked), "MarketTransferPanel_own_villages");
            this.villageSelectPanel.addControl(this.villageSelectPanelTab1);
            this.villageSelectPanelTab2.ImageNorm = (Image) GFXLibrary.tab_villagename_back;
            this.villageSelectPanelTab2.ImageOver = (Image) GFXLibrary.tab_villagename_over;
            this.villageSelectPanelTab2.ImageClick = (Image) GFXLibrary.tab_villagename_over;
            this.villageSelectPanelTab2.Position = new Point(0x8a, 3);
            this.villageSelectPanelTab2.Text.Text = SK.Text("MarketTradeScreen_Recent", "Recent");
            this.villageSelectPanelTab2.TextYOffset = -1;
            this.villageSelectPanelTab2.Data = 1;
            this.villageSelectPanelTab2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectPanelTab2.Text.Color = ARGBColors.Black;
            this.villageSelectPanelTab2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageTabClicked), "MarketTransferPanel_recent_villages");
            this.villageSelectPanel.addControl(this.villageSelectPanelTab2);
            this.villageSelectVillage1.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage1.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage1.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage1.ImageNorm = null;
            this.villageSelectVillage1.Position = new Point(20, 0x15);
            this.villageSelectVillage1.Text.Text = "Village 1";
            this.villageSelectVillage1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage1.Text.Position = new Point(5, 0);
            this.villageSelectVillage1.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage1.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage1.TextYOffset = 0;
            this.villageSelectVillage1.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage1.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage1);
            this.villageSelectVillage2.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage2.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage2.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage2.ImageNorm = null;
            this.villageSelectVillage2.Position = new Point(20, 0x27);
            this.villageSelectVillage2.Text.Text = "Village 2";
            this.villageSelectVillage2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage2.Text.Position = new Point(5, 0);
            this.villageSelectVillage2.Text.Size = new Size(this.villageSelectVillage2.Text.Size.Width - 10, this.villageSelectVillage2.Text.Size.Height);
            this.villageSelectVillage2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage2.TextYOffset = 0;
            this.villageSelectVillage2.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage2.Data = 1;
            this.villageSelectPanel.addControl(this.villageSelectVillage2);
            this.villageSelectVillage3.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage3.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage3.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage3.ImageNorm = null;
            this.villageSelectVillage3.Position = new Point(20, 0x39);
            this.villageSelectVillage3.Text.Text = "Village 1";
            this.villageSelectVillage3.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage3.Text.Position = new Point(5, 0);
            this.villageSelectVillage3.Text.Size = new Size(this.villageSelectVillage3.Text.Size.Width - 10, this.villageSelectVillage3.Text.Size.Height);
            this.villageSelectVillage3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage3.TextYOffset = 0;
            this.villageSelectVillage3.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage3.Data = 2;
            this.villageSelectPanel.addControl(this.villageSelectVillage3);
            this.villageSelectVillage4.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage4.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage4.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage4.ImageNorm = null;
            this.villageSelectVillage4.Position = new Point(20, 0x4b);
            this.villageSelectVillage4.Text.Text = "Village 4";
            this.villageSelectVillage4.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage4.Text.Position = new Point(5, 0);
            this.villageSelectVillage4.Text.Size = new Size(this.villageSelectVillage4.Text.Size.Width - 10, this.villageSelectVillage4.Text.Size.Height);
            this.villageSelectVillage4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage4.TextYOffset = 0;
            this.villageSelectVillage4.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage4.Data = 3;
            this.villageSelectPanel.addControl(this.villageSelectVillage4);
            this.villageSelectVillage5.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage5.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage5.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage5.ImageNorm = null;
            this.villageSelectVillage5.Position = new Point(20, 0x5d);
            this.villageSelectVillage5.Text.Text = "Village 5";
            this.villageSelectVillage5.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage5.Text.Position = new Point(5, 0);
            this.villageSelectVillage5.Text.Size = new Size(this.villageSelectVillage5.Text.Size.Width - 10, this.villageSelectVillage5.Text.Size.Height);
            this.villageSelectVillage5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage5.TextYOffset = 0;
            this.villageSelectVillage5.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage5.Data = 4;
            this.villageSelectPanel.addControl(this.villageSelectVillage5);
            this.villageSelectVillage6.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage6.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage6.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage6.ImageNorm = null;
            this.villageSelectVillage6.Position = new Point(20, 0x6f);
            this.villageSelectVillage6.Text.Text = "Village 6";
            this.villageSelectVillage6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage6.Text.Position = new Point(5, 0);
            this.villageSelectVillage6.Text.Size = new Size(this.villageSelectVillage6.Text.Size.Width - 10, this.villageSelectVillage6.Text.Size.Height);
            this.villageSelectVillage6.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage6.TextYOffset = 0;
            this.villageSelectVillage6.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage6.Data = 5;
            this.villageSelectPanel.addControl(this.villageSelectVillage6);
            this.villageSelectVillage7.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage7.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage7.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage7.ImageNorm = null;
            this.villageSelectVillage7.Position = new Point(20, 0x81);
            this.villageSelectVillage7.Text.Text = "Village 7";
            this.villageSelectVillage7.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage7.Text.Position = new Point(5, 0);
            this.villageSelectVillage7.Text.Size = new Size(this.villageSelectVillage7.Text.Size.Width - 10, this.villageSelectVillage7.Text.Size.Height);
            this.villageSelectVillage7.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage7.TextYOffset = 0;
            this.villageSelectVillage7.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage7.Data = 6;
            this.villageSelectPanel.addControl(this.villageSelectVillage7);
            this.villageSelectVillage8.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage8.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage8.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage8.ImageNorm = null;
            this.villageSelectVillage8.Position = new Point(20, 0x93);
            this.villageSelectVillage8.Text.Text = "Village 8";
            this.villageSelectVillage8.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage8.Text.Position = new Point(5, 0);
            this.villageSelectVillage8.Text.Size = new Size(this.villageSelectVillage8.Text.Size.Width - 10, this.villageSelectVillage8.Text.Size.Height);
            this.villageSelectVillage8.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage8.TextYOffset = 0;
            this.villageSelectVillage8.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage8.Data = 7;
            this.villageSelectPanel.addControl(this.villageSelectVillage8);
            this.villageSelectVillage9.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage9.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage9.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage9.ImageNorm = null;
            this.villageSelectVillage9.Position = new Point(20, 0xa5);
            this.villageSelectVillage9.Text.Text = "Village 9";
            this.villageSelectVillage9.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage9.Text.Position = new Point(5, 0);
            this.villageSelectVillage9.Text.Size = new Size(this.villageSelectVillage9.Text.Size.Width - 10, this.villageSelectVillage9.Text.Size.Height);
            this.villageSelectVillage9.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage9.TextYOffset = 0;
            this.villageSelectVillage9.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage9.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage9.Data = 8;
            this.villageSelectPanel.addControl(this.villageSelectVillage9);
            this.villageSelectVillage10.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage10.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage10.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage10.ImageNorm = null;
            this.villageSelectVillage10.Position = new Point(20, 0xb7);
            this.villageSelectVillage10.Text.Text = "Village 10";
            this.villageSelectVillage10.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage10.Text.Position = new Point(5, 0);
            this.villageSelectVillage10.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage10.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage10.TextYOffset = 0;
            this.villageSelectVillage10.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage10.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage10.Data = 9;
            this.villageSelectPanel.addControl(this.villageSelectVillage10);
            this.villageSelectVillage11.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage11.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage11.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage11.ImageNorm = null;
            this.villageSelectVillage11.Position = new Point(20, 0xc9);
            this.villageSelectVillage11.Text.Text = "Village 11";
            this.villageSelectVillage11.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage11.Text.Position = new Point(5, 0);
            this.villageSelectVillage11.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage11.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage11.TextYOffset = 0;
            this.villageSelectVillage11.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage11.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage11.Data = 10;
            this.villageSelectPanel.addControl(this.villageSelectVillage11);
            this.villageSelectVillage12.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage12.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage12.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage12.ImageNorm = null;
            this.villageSelectVillage12.Position = new Point(20, 0xdb);
            this.villageSelectVillage12.Text.Text = "Village 12";
            this.villageSelectVillage12.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage12.Text.Position = new Point(5, 0);
            this.villageSelectVillage12.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage12.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage12.TextYOffset = 0;
            this.villageSelectVillage12.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage12.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage12.Data = 11;
            this.villageSelectPanel.addControl(this.villageSelectVillage12);
            this.villageSelectVillage13.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage13.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage13.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage13.ImageNorm = null;
            this.villageSelectVillage13.Position = new Point(20, 0xed);
            this.villageSelectVillage13.Text.Text = "Village 13";
            this.villageSelectVillage13.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage13.Text.Position = new Point(5, 0);
            this.villageSelectVillage13.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage13.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage13.TextYOffset = 0;
            this.villageSelectVillage13.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage13.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage13.Data = 12;
            this.villageSelectPanel.addControl(this.villageSelectVillage13);
            this.villageSelectVillage14.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage14.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage14.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage14.ImageNorm = null;
            this.villageSelectVillage14.Position = new Point(20, 0xff);
            this.villageSelectVillage14.Text.Text = "Village 14";
            this.villageSelectVillage14.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage14.Text.Position = new Point(5, 0);
            this.villageSelectVillage14.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage14.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage14.TextYOffset = 0;
            this.villageSelectVillage14.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage14.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage14.Data = 13;
            this.villageSelectPanel.addControl(this.villageSelectVillage14);
            this.villageSelectVillage15.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage15.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage15.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage15.ImageNorm = null;
            this.villageSelectVillage15.Position = new Point(20, 0x111);
            this.villageSelectVillage15.Text.Text = "Village 15";
            this.villageSelectVillage15.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage15.Text.Position = new Point(5, 0);
            this.villageSelectVillage15.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage15.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage15.TextYOffset = 0;
            this.villageSelectVillage15.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage15.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage15.Data = 14;
            this.villageSelectPanel.addControl(this.villageSelectVillage15);
            this.villageSelectVillage16.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage16.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage16.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage16.ImageNorm = null;
            this.villageSelectVillage16.Position = new Point(20, 0x123);
            this.villageSelectVillage16.Text.Text = "Village 16";
            this.villageSelectVillage16.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage16.Text.Position = new Point(5, 0);
            this.villageSelectVillage16.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage16.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage16.TextYOffset = 0;
            this.villageSelectVillage16.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage16.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage16.Data = 15;
            this.villageSelectPanel.addControl(this.villageSelectVillage16);
            this.villageSelectVillage17.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage17.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage17.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage17.ImageNorm = null;
            this.villageSelectVillage17.Position = new Point(20, 0x135);
            this.villageSelectVillage17.Text.Text = "Village 17";
            this.villageSelectVillage17.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage17.Text.Position = new Point(5, 0);
            this.villageSelectVillage17.Text.Size = new Size(this.villageSelectVillage1.Text.Size.Width - 10, this.villageSelectVillage1.Text.Size.Height);
            this.villageSelectVillage17.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage17.TextYOffset = 0;
            this.villageSelectVillage17.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage17.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage17.Data = 0x10;
            this.villageSelectPanel.addControl(this.villageSelectVillage17);
            this.villageSelectVillage1Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage1Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage1Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage1Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage1Delete.Position = new Point(0xff, 0x15);
            this.villageSelectVillage1Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage1Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage1Delete);
            this.villageSelectVillage1Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage1Favourite.OverBrighten = true;
            this.villageSelectVillage1Favourite.Position = new Point(1, 0x13);
            this.villageSelectVillage1Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage1Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage1Favourite);
            this.villageSelectVillage2Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage2Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage2Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage2Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage2Delete.Position = new Point(0xff, 0x27);
            this.villageSelectVillage2Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage2Delete.Data = 1;
            this.villageSelectPanel.addControl(this.villageSelectVillage2Delete);
            this.villageSelectVillage2Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage2Favourite.OverBrighten = true;
            this.villageSelectVillage2Favourite.Position = new Point(1, 0x25);
            this.villageSelectVillage2Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage2Favourite.Data = 1;
            this.villageSelectPanel.addControl(this.villageSelectVillage2Favourite);
            this.villageSelectVillage3Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage3Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage3Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage3Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage3Delete.Position = new Point(0xff, 0x39);
            this.villageSelectVillage3Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage3Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage3Delete);
            this.villageSelectVillage3Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage3Favourite.OverBrighten = true;
            this.villageSelectVillage3Favourite.Position = new Point(1, 0x37);
            this.villageSelectVillage3Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage3Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage3Favourite);
            this.villageSelectVillage4Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage4Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage4Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage4Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage4Delete.Position = new Point(0xff, 0x4b);
            this.villageSelectVillage4Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage4Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage4Delete);
            this.villageSelectVillage4Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage4Favourite.OverBrighten = true;
            this.villageSelectVillage4Favourite.Position = new Point(1, 0x49);
            this.villageSelectVillage4Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage4Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage4Favourite);
            this.villageSelectVillage5Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage5Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage5Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage5Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage5Delete.Position = new Point(0xff, 0x5d);
            this.villageSelectVillage5Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage5Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage5Delete);
            this.villageSelectVillage5Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage5Favourite.OverBrighten = true;
            this.villageSelectVillage5Favourite.Position = new Point(1, 0x5b);
            this.villageSelectVillage5Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage5Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage5Favourite);
            this.villageSelectVillage6Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage6Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage6Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage6Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage6Delete.Position = new Point(0xff, 0x6f);
            this.villageSelectVillage6Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage6Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage6Delete);
            this.villageSelectVillage6Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage6Favourite.OverBrighten = true;
            this.villageSelectVillage6Favourite.Position = new Point(1, 0x6d);
            this.villageSelectVillage6Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage6Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage6Favourite);
            this.villageSelectVillage7Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage7Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage7Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage7Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage7Delete.Position = new Point(0xff, 0x81);
            this.villageSelectVillage7Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage7Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage7Delete);
            this.villageSelectVillage7Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage7Favourite.OverBrighten = true;
            this.villageSelectVillage7Favourite.Position = new Point(1, 0x7f);
            this.villageSelectVillage7Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage7Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage7Favourite);
            this.villageSelectVillage8Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage8Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage8Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage8Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage8Delete.Position = new Point(0xff, 0x93);
            this.villageSelectVillage8Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage8Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage8Delete);
            this.villageSelectVillage8Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage8Favourite.OverBrighten = true;
            this.villageSelectVillage8Favourite.Position = new Point(1, 0x91);
            this.villageSelectVillage8Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage8Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage8Favourite);
            this.villageSelectVillage9Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage9Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage9Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage9Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage9Delete.Position = new Point(0xff, 0xa5);
            this.villageSelectVillage9Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage9Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage9Delete);
            this.villageSelectVillage9Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage9Favourite.OverBrighten = true;
            this.villageSelectVillage9Favourite.Position = new Point(1, 0xa3);
            this.villageSelectVillage9Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage9Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage9Favourite);
            this.villageSelectVillage10Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage10Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage10Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage10Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage10Delete.Position = new Point(0xff, 0xb7);
            this.villageSelectVillage10Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage10Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage10Delete);
            this.villageSelectVillage10Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage10Favourite.OverBrighten = true;
            this.villageSelectVillage10Favourite.Position = new Point(1, 0xb5);
            this.villageSelectVillage10Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage10Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage10Favourite);
            this.villageSelectVillage11Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage11Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage11Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage11Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage11Delete.Position = new Point(0xff, 0xc9);
            this.villageSelectVillage11Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage11Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage11Delete);
            this.villageSelectVillage11Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage11Favourite.OverBrighten = true;
            this.villageSelectVillage11Favourite.Position = new Point(1, 0xc7);
            this.villageSelectVillage11Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage11Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage11Favourite);
            this.villageSelectVillage12Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage12Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage12Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage12Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage12Delete.Position = new Point(0xff, 0xdb);
            this.villageSelectVillage12Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage12Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage12Delete);
            this.villageSelectVillage12Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage12Favourite.OverBrighten = true;
            this.villageSelectVillage12Favourite.Position = new Point(1, 0xd9);
            this.villageSelectVillage12Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage12Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage12Favourite);
            this.villageSelectVillage13Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage13Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage13Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage13Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage13Delete.Position = new Point(0xff, 0xed);
            this.villageSelectVillage13Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage13Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage13Delete);
            this.villageSelectVillage13Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage13Favourite.OverBrighten = true;
            this.villageSelectVillage13Favourite.Position = new Point(1, 0xeb);
            this.villageSelectVillage13Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage13Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage13Favourite);
            this.villageSelectVillage14Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage14Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage14Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage14Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage14Delete.Position = new Point(0xff, 0xff);
            this.villageSelectVillage14Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage14Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage14Delete);
            this.villageSelectVillage14Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage14Favourite.OverBrighten = true;
            this.villageSelectVillage14Favourite.Position = new Point(1, 0xfd);
            this.villageSelectVillage14Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage14Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage14Favourite);
            this.villageSelectVillage15Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage15Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage15Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage15Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage15Delete.Position = new Point(0xff, 0x111);
            this.villageSelectVillage15Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage15Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage15Delete);
            this.villageSelectVillage15Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage15Favourite.OverBrighten = true;
            this.villageSelectVillage15Favourite.Position = new Point(1, 0x10f);
            this.villageSelectVillage15Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage15Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage15Favourite);
            this.villageSelectVillage16Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage16Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage16Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage16Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage16Delete.Position = new Point(0xff, 0x123);
            this.villageSelectVillage16Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage16Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage16Delete);
            this.villageSelectVillage16Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage16Favourite.OverBrighten = true;
            this.villageSelectVillage16Favourite.Position = new Point(1, 0x121);
            this.villageSelectVillage16Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage16Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage16Favourite);
            this.villageSelectVillage17Delete.ImageNorm = (Image) GFXLibrary.trashcan_normal;
            this.villageSelectVillage17Delete.ImageOver = (Image) GFXLibrary.trashcan_over;
            this.villageSelectVillage17Delete.ImageClick = (Image) GFXLibrary.trashcan_clicked;
            this.villageSelectVillage17Delete.Size = new Size((GFXLibrary.trashcan_normal.Width * 3) / 4, (GFXLibrary.trashcan_normal.Height * 3) / 4);
            this.villageSelectVillage17Delete.Position = new Point(0xff, 0x135);
            this.villageSelectVillage17Delete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageRecentDeleteClicked), "FactionNewForumPanel_delete_thread");
            this.villageSelectVillage17Delete.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage17Delete);
            this.villageSelectVillage17Favourite.ImageNorm = (Image) GFXLibrary.star_market_1;
            this.villageSelectVillage17Favourite.OverBrighten = true;
            this.villageSelectVillage17Favourite.Position = new Point(1, 0x133);
            this.villageSelectVillage17Favourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.villageSelectVillage17Favourite.Data = 0;
            this.villageSelectPanel.addControl(this.villageSelectVillage17Favourite);
            this.worldMapButton.ImageNorm = (Image) GFXLibrary.int_button_findonmap_normal;
            this.worldMapButton.ImageOver = (Image) GFXLibrary.int_button_findonmap_over;
            this.worldMapButton.ImageClick = (Image) GFXLibrary.int_button_findonmap_in;
            this.worldMapButton.Position = new Point(0x38, 0x158);
            this.worldMapButton.Text.Text = SK.Text("MarketTradeScreen_Find_On_Map", "Find on map");
            this.worldMapButton.TextYOffset = -5;
            this.worldMapButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.worldMapButton.Text.Color = ARGBColors.Black;
            this.worldMapButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.findOnWorldClicked), "MarketTransferPanel_find_on_map");
            this.villageSelectPanel.addControl(this.worldMapButton);
            this.villageOwnPageUp.ImageNorm = (Image) GFXLibrary.int_button_droparrow_up_normal;
            this.villageOwnPageUp.ImageOver = (Image) GFXLibrary.int_button_droparrow_up_over;
            this.villageOwnPageUp.ImageClick = (Image) GFXLibrary.int_button_droparrow_up_down;
            this.villageOwnPageUp.Position = new Point(200, 0x13a);
            this.villageOwnPageUp.MoveOnClick = false;
            this.villageOwnPageUp.Data = 0;
            this.villageOwnPageUp.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnPageClicked), "MarketTransferPanel_page_up");
            this.villageSelectPanel.addControl(this.villageOwnPageUp);
            this.villageOwnPageDown.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
            this.villageOwnPageDown.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
            this.villageOwnPageDown.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
            this.villageOwnPageDown.Position = new Point(230, 0x13a);
            this.villageOwnPageDown.MoveOnClick = false;
            this.villageOwnPageDown.Data = 1;
            this.villageOwnPageDown.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnPageClicked), "MarketTransferPanel_page_down");
            this.villageSelectPanel.addControl(this.villageOwnPageDown);
            this.villageTabMode = 0;
            this.villageTabOwnPage = 0;
            this.updateVillageHistory();
            this.cardbar.Position = new Point(0, 0);
            this.mainBackgroundArea.addControl(this.cardbar);
            this.cardbar.init(1);
            this.lastTab = -1;
            this.manageTabs(1);
            this.updateDeliveryTime(-1);
            if (this.selectedTargetVillage >= 0)
            {
                this.resetBackupData();
                this.resume(this.selectedTargetVillage, false);
                this.selectHighlightLine(0);
            }
            this.update();
        }

        private void initArmouryTab()
        {
            this.highlightLine1.Visible = true;
            this.highlightLine2.Visible = true;
            this.highlightLine3.Visible = true;
            this.highlightLine4.Visible = true;
            this.highlightLine5.Visible = true;
            this.highlightLine6.Visible = false;
            this.highlightLine7.Visible = false;
            this.highlightLine8.Visible = false;
            this.setRowInfo(0, 0x1d);
            this.setRowInfo(1, 0x1c);
            this.setRowInfo(2, 0x1f);
            this.setRowInfo(3, 30);
            this.setRowInfo(4, 0x20);
        }

        private void initGranaryTab()
        {
            this.highlightLine1.Visible = true;
            this.highlightLine2.Visible = true;
            this.highlightLine3.Visible = true;
            this.highlightLine4.Visible = true;
            this.highlightLine5.Visible = true;
            this.highlightLine6.Visible = true;
            this.highlightLine7.Visible = true;
            this.highlightLine8.Visible = false;
            this.setRowInfo(0, 13);
            this.setRowInfo(1, 0x11);
            this.setRowInfo(2, 0x10);
            this.setRowInfo(3, 14);
            this.setRowInfo(4, 15);
            this.setRowInfo(5, 0x12);
            this.setRowInfo(6, 12);
        }

        private void initHallTab()
        {
            this.highlightLine1.Visible = true;
            this.highlightLine2.Visible = true;
            this.highlightLine3.Visible = true;
            this.highlightLine4.Visible = true;
            this.highlightLine5.Visible = true;
            this.highlightLine6.Visible = true;
            this.highlightLine7.Visible = true;
            this.highlightLine8.Visible = true;
            this.setRowInfo(0, 0x16);
            this.setRowInfo(1, 0x15);
            this.setRowInfo(2, 0x1a);
            this.setRowInfo(3, 0x13);
            this.setRowInfo(4, 0x21);
            this.setRowInfo(5, 0x17);
            this.setRowInfo(6, 0x18);
            this.setRowInfo(7, 0x19);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "MarketTransferPanel2";
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
            this.selectedTargetVillage = -1;
        }

        private void manageTabs(int tabID)
        {
            if (tabID != this.lastTab)
            {
                this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_normal;
                this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_over;
                this.tabButton2.ImageNorm = (Image) GFXLibrary.int_storage_tab_02_normal;
                this.tabButton2.ImageOver = (Image) GFXLibrary.int_storage_tab_02_over;
                this.tabButton3.ImageNorm = (Image) GFXLibrary.int_storage_tab_03_normal;
                this.tabButton3.ImageOver = (Image) GFXLibrary.int_storage_tab_03_over;
                this.tabButton4.ImageNorm = (Image) GFXLibrary.int_storage_tab_04_normal;
                this.tabButton4.ImageOver = (Image) GFXLibrary.int_storage_tab_04_over;
                switch (tabID)
                {
                    case 1:
                        this.tabButton1.ImageNorm = (Image) GFXLibrary.int_storage_tab_01_selected;
                        this.tabButton1.ImageOver = (Image) GFXLibrary.int_storage_tab_01_selected;
                        this.selectHighlightLine(0);
                        this.initStockpileTab();
                        this.selectHighlightLine(0);
                        break;

                    case 2:
                        this.tabButton2.ImageNorm = (Image) GFXLibrary.int_storage_tab_02_selected;
                        this.tabButton2.ImageOver = (Image) GFXLibrary.int_storage_tab_02_selected;
                        this.selectHighlightLine(0);
                        this.initGranaryTab();
                        this.selectHighlightLine(0);
                        break;

                    case 3:
                        this.tabButton3.ImageNorm = (Image) GFXLibrary.int_storage_tab_03_selected;
                        this.tabButton3.ImageOver = (Image) GFXLibrary.int_storage_tab_03_selected;
                        this.selectHighlightLine(0);
                        this.initArmouryTab();
                        this.selectHighlightLine(0);
                        break;

                    case 4:
                        this.tabButton4.ImageNorm = (Image) GFXLibrary.int_storage_tab_04_selected;
                        this.tabButton4.ImageOver = (Image) GFXLibrary.int_storage_tab_04_selected;
                        this.selectHighlightLine(0);
                        this.initHallTab();
                        this.selectHighlightLine(0);
                        break;
                }
                this.lastTab = tabID;
                base.Invalidate();
            }
        }

        public void resetBackupData()
        {
            this.BACKUP_resource = -1;
            this.BACKUP_sendLevel = 0x1dcd6500;
        }

        public void resume(int villageID, bool keepInfo)
        {
            this.tabButton1.Enabled = true;
            this.tabButton2.Enabled = true;
            this.tabButton3.Enabled = true;
            this.tabButton4.Enabled = true;
            if (keepInfo)
            {
                this.currentResource = this.BACKUP_resource;
            }
            else
            {
                this.BACKUP_sendLevel = 0;
                this.currentResource = 0;
                this.BACKUP_resource = -1;
            }
            if (villageID < 0)
            {
                this.selectedTargetVillage = -1;
            }
            else
            {
                this.selectedTargetVillage = villageID;
                this.lastVillageData = null;
                if (GameEngine.Instance.World.isUserVillage(this.selectedTargetVillage))
                {
                    bool flag = true;
                    if (this.stockExchanges[this.selectedTargetVillage] != null)
                    {
                        StockExchangePanel.StockExchangeInfo info = (StockExchangePanel.StockExchangeInfo) this.stockExchanges[this.selectedTargetVillage];
                        TimeSpan span = (TimeSpan) (DateTime.Now - info.lastTime);
                        if (span.TotalMinutes < 3.0)
                        {
                            flag = false;
                        }
                        this.lastVillageData = info;
                    }
                    if (flag)
                    {
                        RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(new RemoteServices.GetStockExchangeData_UserCallBack(this.getStockExchangeDataCallback));
                        RemoteServices.Instance.GetStockExchangeData(villageID, false);
                    }
                }
                this.updateDeliveryTime(villageID);
                this.exchangeNameLabel.Text = GameEngine.Instance.World.getVillageName(villageID);
                if (GameEngine.Instance.World.isCapital(villageID))
                {
                    if (((this.BACKUP_resource != 6) && (this.BACKUP_resource != 7)) && ((this.BACKUP_resource != 8) && (this.BACKUP_resource != 9)))
                    {
                        this.BACKUP_resource = -1;
                        this.manageTabs(1);
                    }
                    this.tabButton2.Enabled = false;
                    this.tabButton3.Enabled = false;
                    this.tabButton4.Enabled = false;
                }
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
                            this.manageTabs(1);
                            break;

                        case 12:
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
                    this.sendTrack.Max = 0x1dcd6500;
                    this.sendTrack.Value = this.BACKUP_sendLevel;
                }
                this.updateValues();
            }
        }

        private void rowClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data != this.currentResource)
                {
                    this.sendTrack.Max = 0x1dcd6500;
                    this.sendTrack.Value = 0x1dcd6500;
                    GameEngine.Instance.playInterfaceSound("MarketTransferPanel_resource_clicked");
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
            image.Size = new Size(0x1d1, 0x1f);
            this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
            this.sendHeadingLabel.Text = SK.Text("MarketTradeScreen_Send", "Send") + " " + VillageBuildingsData.getResourceNames(this.currentResource);
            this.sendHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
            this.sendTrack.Max = 0x1dcd6500;
            this.sendTrack.Value = 0x1dcd6500;
            this.showSendWindow();
        }

        private void sendClick()
        {
            this.validateSendButtons();
            if (this.sendButton.Enabled)
            {
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - this.lastTradeTime);
                if (span.TotalSeconds >= 2.0)
                {
                    this.lastTradeTime = now;
                    VillageMap village = GameEngine.Instance.Village;
                    if (village != null)
                    {
                        this.dirtyStockExchangeInfo(this.selectedTargetVillage);
                        village.sendResources(this.selectedTargetVillage, this.currentResource, this.sendTrack.Value);
                        this.addVillageToHistory(this.selectedTargetVillage);
                        string tag = "";
                        switch (this.currentResource)
                        {
                            case 6:
                                tag = "MarketResource_Wood";
                                break;

                            case 7:
                                tag = "MarketResource_Stone";
                                break;

                            case 8:
                                tag = "MarketResource_Iron";
                                break;

                            case 9:
                                tag = "MarketResource_Pitch";
                                break;

                            case 12:
                                tag = "MarketResource_Ale";
                                break;

                            case 13:
                                tag = "MarketResource_Apples";
                                break;

                            case 14:
                                tag = "MarketResource_Bread";
                                break;

                            case 15:
                                tag = "MarketResource_Veg";
                                break;

                            case 0x10:
                                tag = "MarketResource_Meat";
                                break;

                            case 0x11:
                                tag = "MarketResource_Cheese";
                                break;

                            case 0x12:
                                tag = "MarketResource_Fish";
                                break;

                            case 0x13:
                                tag = "MarketResource_Clothes";
                                break;

                            case 0x15:
                                tag = "MarketResource_Furniture";
                                break;

                            case 0x16:
                                tag = "MarketResource_Venison";
                                break;

                            case 0x17:
                                tag = "MarketResource_Salt";
                                break;

                            case 0x18:
                                tag = "MarketResource_Spices";
                                break;

                            case 0x19:
                                tag = "MarketResource_Salt";
                                break;

                            case 0x1a:
                                tag = "MarketResource_Metalware";
                                break;

                            case 0x1c:
                                tag = "MarketResource_Pikes";
                                break;

                            case 0x1d:
                                tag = "MarketResource_Bows";
                                break;

                            case 30:
                                tag = "MarketResource_Swords";
                                break;

                            case 0x1f:
                                tag = "MarketResource_Armour";
                                break;

                            case 0x20:
                                tag = "MarketResource_Catapults";
                                break;

                            case 0x21:
                                tag = "MarketResource_Wine";
                                break;
                        }
                        Sound.playDelayedInterfaceSound(tag, 100);
                    }
                }
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
            label2.Text = "?";
            if (stockLevel >= 0)
            {
                label2.Text = stockLevel.ToString("N", nFI);
            }
        }

        private void showSendWindow()
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            bool visible = this.sendWindow.Visible;
            this.sendWindow.Visible = false;
            VillageMap village = GameEngine.Instance.Village;
            if ((village != null) && (this.currentResource >= 0))
            {
                int num = (int) village.getResourceLevel(this.currentResource);
                int num2 = this.numFreeTraders * GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
                if (num > num2)
                {
                    num = num2;
                }
                int max = this.sendTrack.Max;
                if (num > max)
                {
                    this.sendTrack.Max = num;
                }
                else if (num < max)
                {
                    if (this.sendTrack.Value > num)
                    {
                        this.sendTrack.Value = num;
                    }
                    this.sendTrack.Max = num;
                }
                this.sendMax.Text = this.sendTrack.Max.ToString("N", nFI);
                this.sendNumber.Text = this.sendTrack.Value.ToString("N", nFI);
                int num4 = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
                if (num4 == 0)
                {
                    num4 = 1;
                }
                this.sendNumberPackets.Text = SK.Text("TradeScreen_Merchants", "Merchants") + " : " + (((this.sendTrack.Value + (num4 - 1)) / num4)).ToString("N", nFI);
                if (((num > 0) && (num2 > 0)) && (this.selectedTargetVillage >= 0))
                {
                    this.sendWindow.Visible = true;
                }
            }
            this.validateSendButtons();
            if (visible != this.sendWindow.Visible)
            {
                this.mainBackgroundImage.invalidate();
            }
        }

        private void showVillagePanel(bool show)
        {
            this.villageSelectPanel.Visible = show;
            if (show)
            {
                this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_up_normal;
                this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_up_over;
                this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_up_down;
                this.exchangeArrowButton.Data = 1;
                this.updateVillageHistory();
            }
            else
            {
                this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
                this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
                this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
                this.exchangeArrowButton.Data = 0;
            }
        }

        public void stockExchangeClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(3);
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
            this.showSendWindow();
            this.sendWindow.invalidate();
        }

        private void turnPageClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data == 0)
                {
                    this.villageTabOwnPage--;
                    if (this.villageTabOwnPage < 0)
                    {
                        this.villageTabOwnPage = 0;
                    }
                }
                else
                {
                    this.villageTabOwnPage++;
                }
                this.updateVillageHistory();
            }
        }

        public void update()
        {
            if (this.currentResource >= 0)
            {
                this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
            }
            this.updateValues();
            this.cardbar.update();
            this.updateDeliveryTime(this.selectedTargetVillage);
        }

        private void updateDeliveryTime(int villageID)
        {
            VillageMap village = GameEngine.Instance.Village;
            if ((villageID >= 0) && (village != null))
            {
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                Point point = GameEngine.Instance.World.getVillageLocation(village.VillageID);
                Point point2 = GameEngine.Instance.World.getVillageLocation(villageID);
                int x = point.X;
                int y = point.Y;
                GameEngine.Instance.World.getVillageRegion(village.VillageID);
                int num3 = point2.X;
                int num4 = point2.Y;
                GameEngine.Instance.World.getVillageRegion(villageID);
                double d = 0.0;
                d = ((x - num3) * (x - num3)) + ((y - num4) * (y - num4));
                d = Math.Sqrt(d) * (localWorldData.traderMoveSpeed * localWorldData.gamePlaySpeed);
                d = GameEngine.Instance.World.UserResearchData.adjustTradeTimes(d) * CardTypes.cards_adjustTradeTimes(GameEngine.Instance.World.UserCardData);
                string str = VillageMap.createBuildTimeString((int) CardTypes.cards_adjustTradeTimesCompleteDelivery(GameEngine.Instance.World.UserCardData, d));
                this.deliveryTimeAreaLabel.TextDiffOnly = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":  " + str;
            }
            else
            {
                this.deliveryTimeAreaLabel.TextDiffOnly = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":  ";
            }
        }

        public void updateValues()
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            VillageMap village = GameEngine.Instance.Village;
            if (village == null)
            {
                for (int i = 0; i < 8; i++)
                {
                    this.setRowValues(i, -1, -1, -1);
                }
                this.tradersAvailableValue.Text = "0/0";
                this.traderCapacityValue.Text = "0";
            }
            else
            {
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                StockExchangePanel.StockExchangeInfo lastVillageData = null;
                if ((this.selectedTargetVillage >= 0) && GameEngine.Instance.World.isUserVillage(this.selectedTargetVillage))
                {
                    lastVillageData = this.lastVillageData;
                }
                switch (this.lastTab)
                {
                    case 1:
                    {
                        VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                        village.getStockpileLevels(levels);
                        if (lastVillageData != null)
                        {
                            this.setRowValues(0, (int) levels.woodLevel, lastVillageData.woodLevel, -1);
                            this.setRowValues(1, (int) levels.stoneLevel, lastVillageData.stoneLevel, -1);
                            this.setRowValues(2, (int) levels.ironLevel, lastVillageData.ironLevel, -1);
                            this.setRowValues(3, (int) levels.pitchLevel, lastVillageData.pitchLevel, -1);
                            break;
                        }
                        this.setRowValues(0, (int) levels.woodLevel, -1, -1);
                        this.setRowValues(1, (int) levels.stoneLevel, -1, -1);
                        this.setRowValues(2, (int) levels.ironLevel, -1, -1);
                        this.setRowValues(3, (int) levels.pitchLevel, -1, -1);
                        break;
                    }
                    case 2:
                    {
                        VillageMap.GranaryLevels levels2 = new VillageMap.GranaryLevels();
                        village.getGranaryLevels(levels2);
                        VillageMap.InnLevels levels3 = new VillageMap.InnLevels();
                        village.getInnLevels(levels3);
                        if (lastVillageData != null)
                        {
                            this.setRowValues(0, (int) levels2.applesLevel, lastVillageData.applesLevel, -1);
                            this.setRowValues(1, (int) levels2.cheeseLevel, lastVillageData.cheeseLevel, -1);
                            this.setRowValues(2, (int) levels2.meatLevel, lastVillageData.meatLevel, -1);
                            this.setRowValues(3, (int) levels2.breadLevel, lastVillageData.breadLevel, -1);
                            this.setRowValues(4, (int) levels2.vegLevel, lastVillageData.vegLevel, -1);
                            this.setRowValues(5, (int) levels2.fishLevel, lastVillageData.fishLevel, -1);
                            this.setRowValues(6, (int) levels3.aleLevel, lastVillageData.aleLevel, -1);
                            break;
                        }
                        this.setRowValues(0, (int) levels2.applesLevel, -1, -1);
                        this.setRowValues(1, (int) levels2.cheeseLevel, -1, -1);
                        this.setRowValues(2, (int) levels2.meatLevel, -1, -1);
                        this.setRowValues(3, (int) levels2.breadLevel, -1, -1);
                        this.setRowValues(4, (int) levels2.vegLevel, -1, -1);
                        this.setRowValues(5, (int) levels2.fishLevel, -1, -1);
                        this.setRowValues(6, (int) levels3.aleLevel, -1, -1);
                        break;
                    }
                    case 3:
                    {
                        VillageMap.ArmouryLevels levels4 = new VillageMap.ArmouryLevels();
                        village.getArmouryLevels(levels4);
                        if (lastVillageData != null)
                        {
                            this.setRowValues(0, (int) levels4.bowsLevel, lastVillageData.bowsLevel, -1);
                            this.setRowValues(1, (int) levels4.pikesLevel, lastVillageData.pikesLevel, -1);
                            this.setRowValues(2, (int) levels4.armourLevel, lastVillageData.armourLevel, -1);
                            this.setRowValues(3, (int) levels4.swordsLevel, lastVillageData.swordsLevel, -1);
                            this.setRowValues(4, (int) levels4.catapultsLevel, lastVillageData.catapultsLevel, -1);
                            break;
                        }
                        this.setRowValues(0, (int) levels4.bowsLevel, -1, -1);
                        this.setRowValues(1, (int) levels4.pikesLevel, -1, -1);
                        this.setRowValues(2, (int) levels4.armourLevel, -1, -1);
                        this.setRowValues(3, (int) levels4.swordsLevel, -1, -1);
                        this.setRowValues(4, (int) levels4.catapultsLevel, -1, -1);
                        break;
                    }
                    case 4:
                    {
                        VillageMap.TownHallLevels levels5 = new VillageMap.TownHallLevels();
                        village.getTownHallLevels(levels5);
                        if (lastVillageData != null)
                        {
                            this.setRowValues(0, (int) levels5.venisonLevel, lastVillageData.venisonLevel, -1);
                            this.setRowValues(1, (int) levels5.furnitureLevel, lastVillageData.furnitureLevel, -1);
                            this.setRowValues(2, (int) levels5.metalwareLevel, lastVillageData.metalwareLevel, -1);
                            this.setRowValues(3, (int) levels5.clothesLevel, lastVillageData.clothesLevel, -1);
                            this.setRowValues(4, (int) levels5.wineLevel, lastVillageData.wineLevel, -1);
                            this.setRowValues(5, (int) levels5.saltLevel, lastVillageData.saltLevel, -1);
                            this.setRowValues(6, (int) levels5.spicesLevel, lastVillageData.spicesLevel, -1);
                            this.setRowValues(7, (int) levels5.silkLevel, lastVillageData.silkLevel, -1);
                            break;
                        }
                        this.setRowValues(0, (int) levels5.venisonLevel, -1, -1);
                        this.setRowValues(1, (int) levels5.furnitureLevel, -1, -1);
                        this.setRowValues(2, (int) levels5.metalwareLevel, -1, -1);
                        this.setRowValues(3, (int) levels5.clothesLevel, -1, -1);
                        this.setRowValues(4, (int) levels5.wineLevel, -1, -1);
                        this.setRowValues(5, (int) levels5.saltLevel, -1, -1);
                        this.setRowValues(6, (int) levels5.spicesLevel, -1, -1);
                        this.setRowValues(7, (int) levels5.silkLevel, -1, -1);
                        break;
                    }
                }
                this.numTraders = village.numTraders();
                this.numFreeTraders = village.numFreeTraders();
                if (this.numFreeTraders > this.numTraders)
                {
                    village.refreshTraderNumbers();
                }
                this.tradersAvailableValue.Text = this.numFreeTraders.ToString() + "/" + this.numTraders.ToString();
                this.traderCapacityValue.Text = (this.currentResourcePacketSize * this.numFreeTraders).ToString("N", nFI);
            }
            this.showSendWindow();
        }

        public void updateVillageHistory()
        {
            for (int i = 0; i < 0x11; i++)
            {
                this.getVillageHistory(i).Visible = false;
                this.getVillageHistoryFavourite(i).Visible = false;
                this.getVillageHistoryDelete(i).Visible = false;
            }
            if (this.villageTabMode == 0)
            {
                List<WorldMap.VillageNameItem> list = GameEngine.Instance.World.getUserVillageNamesList();
                List<WorldMap.VillageNameItem> list2 = new List<WorldMap.VillageNameItem>();
                foreach (WorldMap.VillageNameItem item in list)
                {
                    if (item.villageID >= 0)
                    {
                        list2.Add(item);
                    }
                }
                int line = 0;
                int num3 = this.villageTabOwnPage * 0x10;
                while ((line < 0x10) && (num3 < list2.Count))
                {
                    if (num3 < list2.Count)
                    {
                        WorldMap.VillageNameItem item2 = list2[num3];
                        CustomSelfDrawPanel.CSDButton button4 = this.getVillageHistory(line);
                        button4.Visible = true;
                        button4.Text.Text = GameEngine.Instance.World.getVillageName(item2.villageID);
                        button4.Data = item2.villageID;
                    }
                    num3++;
                    line++;
                }
                if (list2.Count <= 0x10)
                {
                    this.villageOwnPageDown.Visible = false;
                    this.villageOwnPageUp.Visible = false;
                }
                else
                {
                    this.villageOwnPageDown.Visible = true;
                    this.villageOwnPageUp.Visible = true;
                    if (this.villageTabOwnPage == 0)
                    {
                        this.villageOwnPageUp.Visible = false;
                    }
                    else if (this.villageTabOwnPage >= ((list2.Count - 1) / 0x10))
                    {
                        this.villageOwnPageDown.Visible = false;
                    }
                }
            }
            else
            {
                this.villageOwnPageDown.Visible = false;
                this.villageOwnPageUp.Visible = false;
                int num4 = 0;
                while ((num4 < 0x11) && (num4 < villageFavourites.Count))
                {
                    WorldMap.VillageNameItem item3 = villageFavourites[num4];
                    CustomSelfDrawPanel.CSDButton button5 = this.getVillageHistory(num4);
                    button5.Visible = true;
                    button5.Text.Text = GameEngine.Instance.World.getExchangeName(item3.villageID);
                    button5.Data = item3.villageID;
                    CustomSelfDrawPanel.CSDButton button6 = this.getVillageHistoryFavourite(num4);
                    button6.ImageNorm = (Image) GFXLibrary.star_market_1;
                    button6.Visible = true;
                    button6.Data = item3.villageID;
                    button6.CustomTooltipID = 0x32b;
                    this.getVillageHistoryDelete(num4).Data = item3.villageID;
                    num4++;
                }
                for (int j = 0; (num4 < 0x11) && (j < villageHistory.Count); j++)
                {
                    WorldMap.VillageNameItem item4 = villageHistory[j];
                    bool flag = false;
                    foreach (WorldMap.VillageNameItem item5 in villageFavourites)
                    {
                        if (item5.villageID == item4.villageID)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        CustomSelfDrawPanel.CSDButton button8 = this.getVillageHistory(num4);
                        button8.Visible = true;
                        button8.Text.Text = GameEngine.Instance.World.getExchangeName(item4.villageID);
                        button8.Data = item4.villageID;
                        CustomSelfDrawPanel.CSDButton button9 = this.getVillageHistoryFavourite(num4);
                        button9.ImageNorm = (Image) GFXLibrary.star_market_2;
                        button9.Visible = true;
                        button9.Data = item4.villageID;
                        button9.CustomTooltipID = 0x32c;
                        CustomSelfDrawPanel.CSDButton button10 = this.getVillageHistoryDelete(num4);
                        button10.Visible = true;
                        button10.Data = item4.villageID;
                        button10.CustomTooltipID = 0x32d;
                        num4++;
                    }
                }
            }
        }

        private void validateSendButtons()
        {
            if (this.sendWindow.Visible)
            {
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    if ((this.selectedTargetVillage >= 0) && (this.selectedTargetVillage != village.VillageID))
                    {
                        if ((this.sendTrack.Value > 0) && (this.sendTrack.Value <= ((int) village.getResourceLevel(this.currentResource))))
                        {
                            this.sendButton.Enabled = true;
                        }
                        else
                        {
                            this.sendButton.Enabled = false;
                        }
                    }
                    else
                    {
                        this.sendButton.Enabled = false;
                    }
                }
                else
                {
                    this.sendButton.Enabled = false;
                }
            }
            else
            {
                this.sendButton.Enabled = false;
            }
        }

        private void villageClicked()
        {
            if (base.ClickedControl != null)
            {
                GameEngine.Instance.playInterfaceSound("MarketTransferPanel_village_clicked");
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                this.BACKUP_resource = this.currentResource;
                this.BACKUP_sendLevel = this.sendTrack.Value;
                this.resume(clickedControl.Data, true);
                this.showVillagePanel(false);
            }
        }

        private void villageFavouriteClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                bool flag = false;
                foreach (WorldMap.VillageNameItem item in villageFavourites)
                {
                    if (item.villageID == data)
                    {
                        flag = true;
                        villageFavourites.Remove(item);
                        break;
                    }
                }
                if (flag)
                {
                    RemoteServices.Instance.UpdateVillageFavourites(1, data);
                    clickedControl.ImageNorm = (Image) GFXLibrary.star_market_2;
                    for (int i = 0; i < 0x11; i++)
                    {
                        CustomSelfDrawPanel.CSDButton button2 = this.getVillageHistoryDelete(i);
                        if (button2.Data == clickedControl.Data)
                        {
                            button2.Visible = true;
                            return;
                        }
                    }
                }
                else
                {
                    RemoteServices.Instance.UpdateVillageFavourites(0, data);
                    WorldMap.VillageNameItem item2 = new WorldMap.VillageNameItem {
                        villageID = data,
                        villageName = GameEngine.Instance.World.getVillageName(data)
                    };
                    villageFavourites.Add(item2);
                    clickedControl.ImageNorm = (Image) GFXLibrary.star_market_1;
                    for (int j = 0; j < 0x11; j++)
                    {
                        CustomSelfDrawPanel.CSDButton button3 = this.getVillageHistoryDelete(j);
                        if (button3.Data == clickedControl.Data)
                        {
                            button3.Visible = false;
                            return;
                        }
                    }
                }
            }
        }

        private void villageRecentDeleteClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                RemoteServices.Instance.UpdateVillageFavourites(7, data);
                foreach (WorldMap.VillageNameItem item in villageHistory)
                {
                    if (item.villageID == data)
                    {
                        villageHistory.Remove(item);
                        this.updateVillageHistory();
                        break;
                    }
                }
            }
        }

        private void villageTabClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data != this.villageTabMode)
                {
                    if (clickedControl.Data == 0)
                    {
                        this.villageSelectPanelTab1.ImageNorm = (Image) GFXLibrary.tab_villagename_forward;
                        this.villageSelectPanelTab1.ImageOver = (Image) GFXLibrary.tab_villagename_forward;
                        this.villageSelectPanelTab1.ImageClick = (Image) GFXLibrary.tab_villagename_forward;
                        this.villageSelectPanelTab1.MoveOnClick = false;
                        this.villageSelectPanelTab2.ImageNorm = (Image) GFXLibrary.tab_villagename_back;
                        this.villageSelectPanelTab2.ImageOver = (Image) GFXLibrary.tab_villagename_over;
                        this.villageSelectPanelTab2.ImageClick = (Image) GFXLibrary.tab_villagename_over;
                        this.villageSelectPanelTab2.MoveOnClick = true;
                    }
                    else
                    {
                        this.villageSelectPanelTab2.ImageNorm = (Image) GFXLibrary.tab_villagename_forward;
                        this.villageSelectPanelTab2.ImageOver = (Image) GFXLibrary.tab_villagename_forward;
                        this.villageSelectPanelTab2.ImageClick = (Image) GFXLibrary.tab_villagename_forward;
                        this.villageSelectPanelTab2.MoveOnClick = false;
                        this.villageSelectPanelTab1.ImageNorm = (Image) GFXLibrary.tab_villagename_back;
                        this.villageSelectPanelTab1.ImageOver = (Image) GFXLibrary.tab_villagename_over;
                        this.villageSelectPanelTab1.ImageClick = (Image) GFXLibrary.tab_villagename_over;
                        this.villageSelectPanelTab1.MoveOnClick = true;
                        this.villageTabOwnPage = 0;
                    }
                    this.villageTabMode = clickedControl.Data;
                    this.updateVillageHistory();
                }
            }
        }

        public int SelectedTargetVillage
        {
            get
            {
                return this.selectedTargetVillage;
            }
        }

        public class VillageHistoryComparer : IComparer<WorldMap.VillageNameItem>
        {
            public int Compare(WorldMap.VillageNameItem x, WorldMap.VillageNameItem y)
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
                return x.villageName.CompareTo(y.villageName);
            }
        }
    }
}

