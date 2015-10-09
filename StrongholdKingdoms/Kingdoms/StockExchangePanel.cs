namespace Kingdoms
{
    using CommonTypes;
    using StatTracking;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class StockExchangePanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDCheckBox advancedOptions = new CustomSelfDrawPanel.CSDCheckBox();
        private int BACKUP_buyLevel;
        private int BACKUP_resource = -1;
        private int BACKUP_sellLevel;
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
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private List<int> closeCapitalsToTest = new List<int>();
        public const int CLOSEST_SEARCH_NUMBER = 20;
        private IContainer components;
        private int currentResource = -1;
        private int currentResourcePacketSize = 1;
        private int currentResourcePacketSizeREAL = 1;
        private CustomSelfDrawPanel.CSDExtendingPanel deliveryTimeArea = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel deliveryTimeAreaLabel = new CustomSelfDrawPanel.CSDLabel();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton exchangeArrowButton = new CustomSelfDrawPanel.CSDButton();
        private static List<WorldMap.VillageNameItem> exchangeFavourites = new List<WorldMap.VillageNameItem>();
        private static List<WorldMap.VillageNameItem> exchangeHistory = new List<WorldMap.VillageNameItem>();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel exchangeNameBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel exchangeNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel fourthAgeMessage = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton highestPriceRow1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton highestPriceRow2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton highestPriceRow3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton highestPriceRow4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton highestPriceRow5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton highestPriceRow6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton highestPriceRow7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton highestPriceRow8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage highlightLine1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage highlightLine8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel infoWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        public static StockExchangePanel instance = null;
        private int lastHighlightResource = -1;
        private bool lastPremiumType;
        private int lastTab = -1;
        private DateTime lastTradeTime = DateTime.MinValue;
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
        private CustomSelfDrawPanel.CSDButton lowestPriceRow1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton lowestPriceRow2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton lowestPriceRow3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton lowestPriceRow4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton lowestPriceRow5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton lowestPriceRow6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton lowestPriceRow7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton lowestPriceRow8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel midWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDButton newTradingButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel noResearchText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel noResearchWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private int numFreeTraders;
        private int numTraders;
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
        private CustomSelfDrawPanel.CSDButton sellButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel sellCostLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sellCostValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage sellHeadingImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel sellHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sellMax = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sellMin = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sellNumber = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel sellSubWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDTrackBar sellTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDExtendingPanel sellWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDImage stockExchangeImage = new CustomSelfDrawPanel.CSDImage();
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
        private CustomSelfDrawPanel.CSDImage villageSelectPanel = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage villageSelectPanelHeader = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel villageSelectPanelLabel = new CustomSelfDrawPanel.CSDLabel();
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
        private CustomSelfDrawPanel.CSDButton worldMapButton = new CustomSelfDrawPanel.CSDButton();

        public StockExchangePanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public static void addFavourites(List<GenericVillageHistoryData> newData)
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

        public static void addHistory(List<GenericVillageHistoryData> newData)
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

        private void addVillageToHistory(int villageID)
        {
            bool flag = false;
            foreach (WorldMap.VillageNameItem item in exchangeHistory)
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
                    villageName = GameEngine.Instance.World.getExchangeName(villageID)
                };
                exchangeHistory.Add(item2);
                this.updateExchangeHistory();
            }
        }

        private void advancedToggle()
        {
            if (this.advancedOptions.Checked)
            {
                StatTrackingClient.Instance().ActivateTrigger(10, 0);
            }
            Program.mySettings.AdvancedTrading = this.advancedOptions.Checked;
            this.updateAdvancedOptions();
        }

        private void buyClick()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = (TimeSpan) (now - this.lastTradeTime);
            if (span.TotalSeconds >= 3.0)
            {
                this.lastTradeTime = now;
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    this.dirtyStockExchangeInfo(this.selectedStockExchange);
                    double num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.currentResource, GameEngine.Instance.World.UserCardData, false) - village.getResourceLevel(this.currentResource);
                    int num2 = this.buyTrack.Value * this.currentResourcePacketSize;
                    if (num < Convert.ToDouble(num2))
                    {
                        if (MyMessageBox.Show(SK.Text("Stock_Exchange_Space_Warning", "You do not have enough space to store all of the goods. Do you wish to continue with the trade? You will receive :") + " " + Convert.ToInt32(num).ToString(), SK.Text("Stock_Exchange_Space_Warning_Title", "Insufficient Storage Space"), MessageBoxButtons.YesNo) != DialogResult.No)
                        {
                            this.BuyClickConinue();
                        }
                    }
                    else
                    {
                        this.BuyClickConinue();
                    }
                }
            }
        }

        private void BuyClickConinue()
        {
            GameEngine.Instance.Village.stockExchangeTrade(this.selectedStockExchange, this.currentResource, this.buyTrack.Value * this.currentResourcePacketSize, true);
            this.addVillageToHistory(this.selectedStockExchange);
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
                StockExchangeInfo info = (StockExchangeInfo) this.stockExchanges[selectedStockExchange];
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

        private void exchangeArrowClick()
        {
            if (this.exchangeArrowButton.Data == 0)
            {
                GameEngine.Instance.playInterfaceSound("StockExchangePanel_village_list_open");
                this.showVillagePanel(true);
            }
            else
            {
                GameEngine.Instance.playInterfaceSound("StockExchangePanel_village_list_close");
                this.showVillagePanel(false);
            }
        }

        private void findOnWorldClicked()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.BACKUP_resource = this.currentResource;
                this.BACKUP_sellLevel = this.sellTrack.Value;
                this.BACKUP_buyLevel = this.buyTrack.Value;
                GameEngine.Instance.World.zoomToVillage(village.VillageID);
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(4);
                InterfaceMgr.Instance.StockExchangeBuyingVillage = village.VillageID;
            }
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

        private CustomSelfDrawPanel.CSDButton getRowHighestButton(int row)
        {
            switch (row)
            {
                case 0:
                    return this.highestPriceRow1;

                case 1:
                    return this.highestPriceRow2;

                case 2:
                    return this.highestPriceRow3;

                case 3:
                    return this.highestPriceRow4;

                case 4:
                    return this.highestPriceRow5;

                case 5:
                    return this.highestPriceRow6;

                case 6:
                    return this.highestPriceRow7;

                case 7:
                    return this.highestPriceRow8;
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

        private CustomSelfDrawPanel.CSDButton getRowLowestButton(int row)
        {
            switch (row)
            {
                case 0:
                    return this.lowestPriceRow1;

                case 1:
                    return this.lowestPriceRow2;

                case 2:
                    return this.lowestPriceRow3;

                case 3:
                    return this.lowestPriceRow4;

                case 4:
                    return this.lowestPriceRow5;

                case 5:
                    return this.lowestPriceRow6;

                case 6:
                    return this.lowestPriceRow7;

                case 7:
                    return this.lowestPriceRow8;
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
                    lastTime = DateTime.Now,
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
                if (returnData.otherVillages != null)
                {
                    foreach (GetStockExchangeData_ReturnType type in returnData.otherVillages)
                    {
                        StockExchangeInfo info2 = new StockExchangeInfo {
                            lastTime = DateTime.Now.AddMinutes(1.0),
                            villageID = type.villageID,
                            woodLevel = type.woodLevel,
                            stoneLevel = type.stoneLevel,
                            ironLevel = type.ironLevel,
                            pitchLevel = type.pitchLevel,
                            aleLevel = type.aleLevel,
                            applesLevel = type.applesLevel,
                            breadLevel = type.breadLevel,
                            meatLevel = type.meatLevel,
                            cheeseLevel = type.cheeseLevel,
                            vegLevel = type.vegLevel,
                            fishLevel = type.fishLevel,
                            bowsLevel = type.bowsLevel,
                            pikesLevel = type.pikesLevel,
                            swordsLevel = type.swordsLevel,
                            armourLevel = type.armourLevel,
                            catapultsLevel = type.catapultsLevel,
                            furnitureLevel = type.furnitureLevel,
                            clothesLevel = type.clothesLevel,
                            saltLevel = type.saltLevel,
                            venisonLevel = type.venisonLevel,
                            silkLevel = type.silkLevel,
                            spicesLevel = type.spicesLevel,
                            metalwareLevel = type.metalwareLevel,
                            wineLevel = type.wineLevel
                        };
                        this.stockExchanges[type.villageID] = info2;
                    }
                }
                int line = this.getLineFromResource(this.currentResource);
                this.selectHighlightLine(line);
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

        private void highestPricedClicked()
        {
            if (GameEngine.Instance.World.isAccountPremium())
            {
                int data = base.ClickedControl.Data;
                int num2 = 0x5f5e100;
                int selectedStockExchange = this.selectedStockExchange;
                int num4 = 0x3b9aca00;
                int villageID = this.selectedStockExchange;
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    villageID = village.VillageID;
                }
                foreach (int num6 in this.closeCapitalsToTest)
                {
                    if (this.stockExchanges[num6] != null)
                    {
                        int num7 = ((StockExchangeInfo) this.stockExchanges[num6]).getLevel(data);
                        if (num7 < num4)
                        {
                            num4 = num7;
                            selectedStockExchange = num6;
                            num2 = GameEngine.Instance.World.getSquareDistance(villageID, num6);
                        }
                        else if (num7 == num4)
                        {
                            int num8 = GameEngine.Instance.World.getSquareDistance(villageID, num6);
                            if (num8 < num2)
                            {
                                selectedStockExchange = num6;
                                num2 = num8;
                            }
                        }
                    }
                }
                this.BACKUP_resource = data;
                this.selectStockExchange(selectedStockExchange);
            }
        }

        public void init()
        {
            this.lastPremiumType = GameEngine.Instance.World.isAccountPremium();
            instance = this;
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("TRADE_Stock_Exchange", "Stock Exchange"));
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "StockExchangePanel_close");
            this.closeButton.CustomTooltipID = 800;
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
            this.newTradingButton.ImageNorm = (Image) GFXLibrary.se_tabs[2];
            this.newTradingButton.ImageOver = (Image) GFXLibrary.se_tabs[3];
            this.newTradingButton.ImageClick = (Image) GFXLibrary.se_tabs[3];
            this.newTradingButton.Position = new Point(20, -17);
            this.newTradingButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tradingClick), "StockExchangePanel_trading");
            this.newTradingButton.ClickArea = new Rectangle(0, 0, 0x5f, 0x19);
            this.newTradingButton.CustomTooltipID = 0x327;
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
            this.lightArea2.Size = new Size(0x61, 0x149);
            this.lightArea2.Position = new Point(0x15, 0x66);
            this.midWindow.addControl(this.lightArea2);
            this.lightArea2.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.storedHeadingLabel.Text = SK.Text("TRADE_At_Exchange", "At Exchange");
            this.storedHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.storedHeadingLabel.Position = new Point(0, -35);
            this.storedHeadingLabel.Size = new Size(0x61, 30);
            this.storedHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.storedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea2.addControl(this.storedHeadingLabel);
            this.lightArea3.Size = new Size(0x4d, 0x149);
            this.lightArea3.Position = new Point(0x81, 0x66);
            this.midWindow.addControl(this.lightArea3);
            this.lightArea3.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.BuyPriceHeadingLabel.Text = SK.Text("TRADE_Price", "Price");
            this.BuyPriceHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.BuyPriceHeadingLabel.Position = new Point(0, -35);
            this.BuyPriceHeadingLabel.Size = new Size(0x4d, 30);
            this.BuyPriceHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.BuyPriceHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea3.addControl(this.BuyPriceHeadingLabel);
            this.exchangeNameBar.Size = new Size(270, 0x1f);
            this.exchangeNameBar.Position = new Point(11, 9);
            this.exchangeNameBar.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick));
            this.midWindow.addControl(this.exchangeNameBar);
            this.exchangeNameBar.Create((Image) GFXLibrary.int_lineitem_inset_left, (Image) GFXLibrary.int_lineitem_inset_middle, (Image) GFXLibrary.int_lineitem_inset_right);
            this.exchangeNameLabel.Text = SK.Text("TRADE_Selected_Exchange", "Select Exchange");
            this.exchangeNameLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.exchangeNameLabel.Position = new Point(0x11, 7);
            this.exchangeNameLabel.Size = new Size((this.exchangeNameBar.Size.Width - 0x11) - 20, this.exchangeNameBar.Size.Height - 13);
            this.exchangeNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.exchangeNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
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
            this.tabButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_resource_tab");
            this.tabButton1.CustomTooltipID = 0x322;
            this.leftWindow.addControl(this.tabButton1);
            this.tabButton2.ImageNorm = (Image) GFXLibrary.int_storage_tab_02_normal;
            this.tabButton2.ImageOver = (Image) GFXLibrary.int_storage_tab_02_over;
            this.tabButton2.Position = new Point(0x53, -13);
            this.tabButton2.MoveOnClick = false;
            this.tabButton2.Data = 2;
            this.tabButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_food_tab");
            this.tabButton2.CustomTooltipID = 0x323;
            this.leftWindow.addControl(this.tabButton2);
            this.tabButton3.ImageNorm = (Image) GFXLibrary.int_storage_tab_03_normal;
            this.tabButton3.ImageOver = (Image) GFXLibrary.int_storage_tab_03_over;
            this.tabButton3.Position = new Point(0xa1, -13);
            this.tabButton3.MoveOnClick = false;
            this.tabButton3.Data = 3;
            this.tabButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_weapons_tab");
            this.tabButton3.CustomTooltipID = 0x324;
            this.leftWindow.addControl(this.tabButton3);
            this.tabButton4.ImageNorm = (Image) GFXLibrary.int_storage_tab_04_normal;
            this.tabButton4.ImageOver = (Image) GFXLibrary.int_storage_tab_04_over;
            this.tabButton4.Position = new Point(0xef, -13);
            this.tabButton4.MoveOnClick = false;
            this.tabButton4.Data = 4;
            this.tabButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "StockExchangePanel_banquetting_tab");
            this.tabButton4.CustomTooltipID = 0x325;
            this.leftWindow.addControl(this.tabButton4);
            this.buyWindow.Size = new Size(0x150, 0x91);
            this.buyWindow.Position = new Point(0x27d, 0x4a);
            this.mainBackgroundArea.addControl(this.buyWindow);
            this.buyWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.buySubWindow.Size = new Size(0x93, 50);
            this.buySubWindow.Position = new Point(0xb2, 0x20);
            this.buyWindow.addControl(this.buySubWindow);
            this.buySubWindow.Create((Image) GFXLibrary.int_insetpanel_b_top_left, (Image) GFXLibrary.int_insetpanel_b_middle_top, (Image) GFXLibrary.int_insetpanel_b_top_right, (Image) GFXLibrary.int_insetpanel_b_middle_left, (Image) GFXLibrary.int_insetpanel_b_middle, (Image) GFXLibrary.int_insetpanel_b_middle_right, (Image) GFXLibrary.int_insetpanel_b_bottom_left, (Image) GFXLibrary.int_insetpanel_b_middle_bottom, (Image) GFXLibrary.int_insetpanel_b_bottom_right);
            this.buyHeadingLabel.Text = SK.Text("TRADE_Buy", "Buy") + " ";
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
            this.buyButton.Position = new Point(0xb1, 0x5e);
            this.buyButton.Size = new Size(0x99, 0x26);
            this.buyButton.Text.Text = SK.Text("CapitalTradePanel_Buy", "Buy");
            this.buyButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.buyButton.TextYOffset = -1;
            this.buyButton.Text.Color = ARGBColors.Black;
            this.buyButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyClick), "StockExchangePanel_buy");
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
            this.sellWindow.Size = new Size(0x150, 0x91);
            this.sellWindow.Position = new Point(0x27d, 0x110);
            this.mainBackgroundArea.addControl(this.sellWindow);
            this.sellWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.sellSubWindow.Size = new Size(0x93, 50);
            this.sellSubWindow.Position = new Point(0xb2, 0x20);
            this.sellWindow.addControl(this.sellSubWindow);
            this.sellSubWindow.Create((Image) GFXLibrary.int_insetpanel_b_top_left, (Image) GFXLibrary.int_insetpanel_b_middle_top, (Image) GFXLibrary.int_insetpanel_b_top_right, (Image) GFXLibrary.int_insetpanel_b_middle_left, (Image) GFXLibrary.int_insetpanel_b_middle, (Image) GFXLibrary.int_insetpanel_b_middle_right, (Image) GFXLibrary.int_insetpanel_b_bottom_left, (Image) GFXLibrary.int_insetpanel_b_middle_bottom, (Image) GFXLibrary.int_insetpanel_b_bottom_right);
            this.sellHeadingLabel.Text = SK.Text("TRADE_Sell", "Sell") + " ";
            this.sellHeadingLabel.Color = ARGBColors.Black;
            this.sellHeadingLabel.Position = new Point(90, -30);
            this.sellHeadingLabel.Size = new Size(0xf6, 30);
            this.sellHeadingLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.sellHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
            this.sellWindow.addControl(this.sellHeadingLabel);
            this.sellHeadingImage.Image = null;
            this.sellHeadingImage.Position = new Point(5, -50);
            this.sellWindow.addControl(this.sellHeadingImage);
            this.sellCostLabel.Text = SK.Text("TRADE_Income", "Income") + ":";
            this.sellCostLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.sellCostLabel.Position = new Point(-10, 13);
            this.sellCostLabel.Size = new Size(0x54, 30);
            if (Program.mySettings.LanguageIdent == "de")
            {
                this.sellCostLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            }
            else
            {
                this.sellCostLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            }
            this.sellCostLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.sellSubWindow.addControl(this.sellCostLabel);
            this.sellNumber.Text = "0";
            this.sellNumber.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.sellNumber.Position = new Point(0x3f, -4);
            this.sellNumber.Size = new Size(70, 30);
            this.sellNumber.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sellNumber.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.sellSubWindow.addControl(this.sellNumber);
            this.sellCostValue.Text = "0";
            this.sellCostValue.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.sellCostValue.Position = new Point(0x3f, 13);
            this.sellCostValue.Size = new Size(70, 30);
            this.sellCostValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sellCostValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.sellSubWindow.addControl(this.sellCostValue);
            this.sellButton.Position = new Point(0xb1, 0x5e);
            this.sellButton.Size = new Size(0x99, 0x26);
            this.sellButton.Text.Text = SK.Text("TRADE_Sell", "Sell");
            this.sellButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sellButton.TextYOffset = -1;
            this.sellButton.Text.Color = ARGBColors.Black;
            this.sellButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sellClick), "StockExchangePanel_sell");
            this.sellWindow.addControl(this.sellButton);
            this.sellButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.sellButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.sellTrack.Position = new Point(0x15, 0x29);
            this.sellTrack.Margin = new Rectangle(3, -1, 1, 0);
            this.sellTrack.Value = 0;
            this.sellTrack.Max = 1;
            this.sellTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
            this.sellWindow.addControl(this.sellTrack);
            this.sellTrack.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.int_slidebar_thumb_middle_normal, (Image) GFXLibrary.int_slidebar_thumb_left_normal, (Image) GFXLibrary.int_slidebar_thumb_right_normal, (Image) GFXLibrary.int_slidebar_thumb_middle_in, (Image) GFXLibrary.int_slidebar_thumb_middle_over);
            this.sellMin.Text = "0";
            this.sellMin.Color = ARGBColors.Black;
            this.sellMin.Position = new Point(-2, 0x4a);
            this.sellMin.Size = new Size(50, 30);
            this.sellMin.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.sellMin.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.sellWindow.addControl(this.sellMin);
            this.sellMax.Text = "0";
            this.sellMax.Color = ARGBColors.Black;
            this.sellMax.Position = new Point(0x7e, 0x4a);
            this.sellMax.Size = new Size(50, 30);
            this.sellMax.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            this.sellMax.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.sellWindow.addControl(this.sellMax);
            this.fourthAgeMessage.Text = SK.Text("TRADE_NO_WEAPONS_4TH_AGE", "Weapons cannot be bought or sold in this Age.");
            this.fourthAgeMessage.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.fourthAgeMessage.Position = new Point(0x10, 0x6f);
            this.fourthAgeMessage.Size = new Size(300, 100);
            this.fourthAgeMessage.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.fourthAgeMessage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.fourthAgeMessage.Visible = false;
            this.leftWindow.addControl(this.fourthAgeMessage);
            this.highlightLine1.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine1.Position = new Point(0x99, 0x6f);
            this.highlightLine1.Size = new Size(0x1d1, 0x1f);
            this.leftWindow.addControl(this.highlightLine1);
            this.highlightLine2.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine2.Position = new Point(0x99, 0x97);
            this.highlightLine2.Size = new Size(0x1d1, 0x1f);
            this.leftWindow.addControl(this.highlightLine2);
            this.highlightLine3.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine3.Position = new Point(0x99, 0xbf);
            this.highlightLine3.Size = new Size(0x1d1, 0x1f);
            this.leftWindow.addControl(this.highlightLine3);
            this.highlightLine4.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine4.Position = new Point(0x99, 0xe7);
            this.highlightLine4.Size = new Size(0x1d1, 0x1f);
            this.leftWindow.addControl(this.highlightLine4);
            this.highlightLine5.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine5.Position = new Point(0x99, 0x10f);
            this.highlightLine5.Size = new Size(0x1d1, 0x1f);
            this.leftWindow.addControl(this.highlightLine5);
            this.highlightLine6.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine6.Position = new Point(0x99, 0x137);
            this.highlightLine6.Size = new Size(0x1d1, 0x1f);
            this.leftWindow.addControl(this.highlightLine6);
            this.highlightLine7.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine7.Position = new Point(0x99, 0x15f);
            this.highlightLine7.Size = new Size(0x1d1, 0x1f);
            this.leftWindow.addControl(this.highlightLine7);
            this.highlightLine8.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine8.Position = new Point(0x99, 0x187);
            this.highlightLine8.Size = new Size(0x1d1, 0x1f);
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
            this.highestPriceRow1.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow1.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow1.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow1.Position = new Point(0x185, -2);
            this.highestPriceRow1.Data = 0;
            this.highestPriceRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow1.CustomTooltipID = 0x32e;
            this.highestPriceRow1.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow1.Alpha = this.highestPriceRow1.Active ? 1f : 0.5f;
            this.highlightLine1.addControl(this.highestPriceRow1);
            this.highestPriceRow2.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow2.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow2.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow2.Position = new Point(0x185, -2);
            this.highestPriceRow2.Data = 1;
            this.highestPriceRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow2.CustomTooltipID = 0x32e;
            this.highestPriceRow2.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow2.Alpha = this.highestPriceRow2.Active ? 1f : 0.5f;
            this.highlightLine2.addControl(this.highestPriceRow2);
            this.highestPriceRow3.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow3.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow3.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow3.Position = new Point(0x185, -2);
            this.highestPriceRow3.Data = 2;
            this.highestPriceRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow3.CustomTooltipID = 0x32e;
            this.highestPriceRow3.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow3.Alpha = this.highestPriceRow3.Active ? 1f : 0.5f;
            this.highlightLine3.addControl(this.highestPriceRow3);
            this.highestPriceRow4.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow4.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow4.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow4.Position = new Point(0x185, -2);
            this.highestPriceRow4.Data = 3;
            this.highestPriceRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow4.CustomTooltipID = 0x32e;
            this.highestPriceRow4.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow4.Alpha = this.highestPriceRow4.Active ? 1f : 0.5f;
            this.highlightLine4.addControl(this.highestPriceRow4);
            this.highestPriceRow5.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow5.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow5.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow5.Position = new Point(0x185, -2);
            this.highestPriceRow5.Data = 4;
            this.highestPriceRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow5.CustomTooltipID = 0x32e;
            this.highestPriceRow5.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow5.Alpha = this.highestPriceRow5.Active ? 1f : 0.5f;
            this.highlightLine5.addControl(this.highestPriceRow5);
            this.highestPriceRow6.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow6.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow6.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow6.Position = new Point(0x185, -2);
            this.highestPriceRow6.Data = 5;
            this.highestPriceRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow6.CustomTooltipID = 0x32e;
            this.highestPriceRow6.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow6.Alpha = this.highestPriceRow6.Active ? 1f : 0.5f;
            this.highlightLine6.addControl(this.highestPriceRow6);
            this.highestPriceRow7.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow7.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow7.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow7.Position = new Point(0x185, -2);
            this.highestPriceRow7.Data = 6;
            this.highestPriceRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow7.CustomTooltipID = 0x32e;
            this.highestPriceRow7.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow7.Alpha = this.highestPriceRow7.Active ? 1f : 0.5f;
            this.highlightLine7.addControl(this.highestPriceRow7);
            this.highestPriceRow8.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[0];
            this.highestPriceRow8.ImageOver = (Image) GFXLibrary.int_hilow_buttons[1];
            this.highestPriceRow8.ImageClick = (Image) GFXLibrary.int_hilow_buttons[2];
            this.highestPriceRow8.Position = new Point(0x185, -2);
            this.highestPriceRow8.Data = 7;
            this.highestPriceRow8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.highestPricedClicked));
            this.highestPriceRow8.CustomTooltipID = 0x32e;
            this.highestPriceRow8.Active = GameEngine.Instance.World.isAccountPremium();
            this.highestPriceRow8.Alpha = this.highestPriceRow8.Active ? 1f : 0.5f;
            this.highlightLine8.addControl(this.highestPriceRow8);
            this.lowestPriceRow1.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow1.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow1.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow1.Position = new Point(0x1a9, -2);
            this.lowestPriceRow1.Data = 0;
            this.lowestPriceRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow1.CustomTooltipID = 0x32f;
            this.lowestPriceRow1.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow1.Alpha = this.highestPriceRow1.Active ? 1f : 0.5f;
            this.highlightLine1.addControl(this.lowestPriceRow1);
            this.lowestPriceRow2.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow2.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow2.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow2.Position = new Point(0x1a9, -2);
            this.lowestPriceRow2.Data = 1;
            this.lowestPriceRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow2.CustomTooltipID = 0x32f;
            this.lowestPriceRow2.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow2.Alpha = this.lowestPriceRow2.Active ? 1f : 0.5f;
            this.highlightLine2.addControl(this.lowestPriceRow2);
            this.lowestPriceRow3.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow3.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow3.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow3.Position = new Point(0x1a9, -2);
            this.lowestPriceRow3.Data = 2;
            this.lowestPriceRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow3.CustomTooltipID = 0x32f;
            this.lowestPriceRow3.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow3.Alpha = this.lowestPriceRow3.Active ? 1f : 0.5f;
            this.highlightLine3.addControl(this.lowestPriceRow3);
            this.lowestPriceRow4.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow4.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow4.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow4.Position = new Point(0x1a9, -2);
            this.lowestPriceRow4.Data = 3;
            this.lowestPriceRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow4.CustomTooltipID = 0x32f;
            this.lowestPriceRow4.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow4.Alpha = this.lowestPriceRow4.Active ? 1f : 0.5f;
            this.highlightLine4.addControl(this.lowestPriceRow4);
            this.lowestPriceRow5.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow5.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow5.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow5.Position = new Point(0x1a9, -2);
            this.lowestPriceRow5.Data = 4;
            this.lowestPriceRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow5.CustomTooltipID = 0x32f;
            this.lowestPriceRow5.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow5.Alpha = this.lowestPriceRow5.Active ? 1f : 0.5f;
            this.highlightLine5.addControl(this.lowestPriceRow5);
            this.lowestPriceRow6.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow6.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow6.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow6.Position = new Point(0x1a9, -2);
            this.lowestPriceRow6.Data = 5;
            this.lowestPriceRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow6.CustomTooltipID = 0x32f;
            this.lowestPriceRow6.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow6.Alpha = this.lowestPriceRow6.Active ? 1f : 0.5f;
            this.highlightLine6.addControl(this.lowestPriceRow6);
            this.lowestPriceRow7.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow7.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow7.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow7.Position = new Point(0x1a9, -2);
            this.lowestPriceRow7.Data = 6;
            this.lowestPriceRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow7.CustomTooltipID = 0x32f;
            this.lowestPriceRow7.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow7.Alpha = this.lowestPriceRow7.Active ? 1f : 0.5f;
            this.highlightLine7.addControl(this.lowestPriceRow7);
            this.lowestPriceRow8.ImageNorm = (Image) GFXLibrary.int_hilow_buttons[3];
            this.lowestPriceRow8.ImageOver = (Image) GFXLibrary.int_hilow_buttons[4];
            this.lowestPriceRow8.ImageClick = (Image) GFXLibrary.int_hilow_buttons[5];
            this.lowestPriceRow8.Position = new Point(0x1a9, -2);
            this.lowestPriceRow8.Data = 7;
            this.lowestPriceRow8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lowestPricedClicked));
            this.lowestPriceRow8.CustomTooltipID = 0x32f;
            this.lowestPriceRow8.Active = GameEngine.Instance.World.isAccountPremium();
            this.lowestPriceRow8.Alpha = this.lowestPriceRow8.Active ? 1f : 0.5f;
            this.highlightLine8.addControl(this.lowestPriceRow8);
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
            this.villageSelectPanelHeader.Image = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectPanelHeader.Position = new Point(3, 3);
            this.villageSelectPanelHeader.Size = new Size(this.villageSelectPanel.Width - 14, this.villageSelectPanelHeader.Image.Height);
            this.villageSelectPanel.addControl(this.villageSelectPanelHeader);
            this.villageSelectPanelLabel.Text = SK.Text("MarketTradeScreen_Recent_Exchanges", "Recent Exchanges");
            this.villageSelectPanelLabel.Color = ARGBColors.Black;
            this.villageSelectPanelLabel.Position = new Point(5, -1);
            this.villageSelectPanelLabel.Size = new Size(this.villageSelectPanelHeader.Size.Width - 10, this.villageSelectPanelHeader.Size.Height);
            this.villageSelectPanelLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectPanelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectPanelHeader.addControl(this.villageSelectPanelLabel);
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
            this.villageSelectVillage3.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage3.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage3.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage3.ImageNorm = null;
            this.villageSelectVillage3.Position = new Point(20, 0x39);
            this.villageSelectVillage3.Text.Text = "Village 3";
            this.villageSelectVillage3.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectVillage3.Text.Position = new Point(5, 0);
            this.villageSelectVillage3.Text.Size = new Size(this.villageSelectVillage3.Text.Size.Width - 10, this.villageSelectVillage3.Text.Size.Height);
            this.villageSelectVillage3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.villageSelectVillage3.TextYOffset = 0;
            this.villageSelectVillage3.Text.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.villageSelectVillage3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageClicked));
            this.villageSelectVillage3.Data = 2;
            this.villageSelectPanel.addControl(this.villageSelectVillage3);
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
            this.worldMapButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.findOnWorldClicked), "StockExchangePanel_find_on_map");
            this.villageSelectPanel.addControl(this.worldMapButton);
            if (GameEngine.Instance.World.UserResearchData.Research_Merchant_Guilds == 0)
            {
                this.leftWindow.Visible = false;
                this.midWindow.Visible = false;
                this.buyWindow.Visible = false;
                this.sellWindow.Visible = false;
                this.infoWindow.Visible = false;
                this.noResearchWindow.Size = new Size(0x2e3, 150);
                this.noResearchWindow.Position = new Point(0x7e, (base.Height - 150) / 2);
                this.mainBackgroundImage.addControl(this.noResearchWindow);
                this.noResearchWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
                this.noResearchText.Text = SK.Text("Trade_Need_Research", "You don't currently have the required 'Merchant Guilds' research level to trade with other villages and exchanges. To begin trading you must research 'Merchant Guilds', place a Market in your village and recruit at least one Merchant.");
                this.noResearchText.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.noResearchText.Position = new Point(20, 0);
                this.noResearchText.Size = new Size(this.noResearchWindow.Width - 40, this.noResearchWindow.Height);
                this.noResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.noResearchText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.noResearchWindow.addControl(this.noResearchText);
                this.currentResource = -1;
            }
            else
            {
                this.leftWindow.Visible = true;
                this.midWindow.Visible = true;
                this.buyWindow.Visible = true;
                this.sellWindow.Visible = true;
                this.infoWindow.Visible = true;
            }
            this.advancedOptions.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
            this.advancedOptions.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
            this.advancedOptions.Position = new Point(20, 450);
            this.advancedOptions.Checked = Program.mySettings.AdvancedTrading;
            this.advancedOptions.CBLabel.Text = SK.Text("StockExchangePanel_advanced_options", "Show Advanced Trade Options");
            this.advancedOptions.CBLabel.Color = ARGBColors.Black;
            this.advancedOptions.CBLabel.Position = new Point(20, -1);
            this.advancedOptions.CBLabel.Size = new Size(this.midWindow.Width, 0x23);
            this.advancedOptions.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.advancedOptions.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.advancedToggle));
            this.midWindow.addControl(this.advancedOptions);
            this.cardbar.Position = new Point(0, 0);
            this.mainBackgroundArea.addControl(this.cardbar);
            this.cardbar.init(1);
            this.updateExchangeHistory();
            this.lastTab = -1;
            this.manageTabs(1);
            this.updateDeliveryTime(-1);
            if (this.selectedStockExchange >= 0)
            {
                this.resetBackupData();
                this.selectStockExchange(this.selectedStockExchange);
                this.selectHighlightLine(0);
            }
            this.updateAdvancedOptions();
            this.update();
        }

        private void initArmouryTab()
        {
            if (GameEngine.Instance.World.FourthAgeWorld)
            {
                this.highlightLine1.Visible = false;
                this.highlightLine2.Visible = false;
                this.highlightLine3.Visible = false;
                this.highlightLine4.Visible = false;
                this.highlightLine5.Visible = false;
                this.highlightLine6.Visible = false;
                this.highlightLine7.Visible = false;
                this.highlightLine8.Visible = false;
                this.fourthAgeMessage.Visible = true;
                this.localHeadingLabel.Visible = false;
                this.lightArea1.Visible = false;
            }
            else
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
            base.Name = "StockExchangePanel";
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
            this.selectedStockExchange = -1;
        }

        private void lowestPricedClicked()
        {
            if (GameEngine.Instance.World.isAccountPremium())
            {
                int data = base.ClickedControl.Data;
                int num2 = 0x5f5e100;
                int selectedStockExchange = this.selectedStockExchange;
                int num4 = -1;
                int villageID = this.selectedStockExchange;
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    villageID = village.VillageID;
                }
                foreach (int num6 in this.closeCapitalsToTest)
                {
                    if (this.stockExchanges[num6] != null)
                    {
                        int num7 = ((StockExchangeInfo) this.stockExchanges[num6]).getLevel(data);
                        if (num7 > num4)
                        {
                            num4 = num7;
                            selectedStockExchange = num6;
                            num2 = GameEngine.Instance.World.getSquareDistance(villageID, num6);
                        }
                        else if (num7 == num4)
                        {
                            int num8 = GameEngine.Instance.World.getSquareDistance(villageID, num6);
                            if (num8 < num2)
                            {
                                selectedStockExchange = num6;
                                num2 = num8;
                            }
                        }
                    }
                }
                this.BACKUP_resource = data;
                this.selectStockExchange(selectedStockExchange);
            }
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
                this.fourthAgeMessage.Visible = false;
                this.localHeadingLabel.Visible = true;
                this.lightArea1.Visible = true;
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

        public void newVillageLoaded()
        {
            this.mainBackgroundImage.invalidate();
        }

        public void resetBackupData()
        {
            this.BACKUP_resource = -1;
            this.BACKUP_sellLevel = 0xc350;
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
                    this.sellTrack.Max = 0xc350;
                    this.sellTrack.Value = 0xc350;
                    GameEngine.Instance.playInterfaceSound("StockExchangePanel_resource_clicked");
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
            this.currentResourcePacketSizeREAL = this.currentResourcePacketSize;
            this.currentResourcePacketSize = CardTypes.adjustTraderCarryLevels(GameEngine.Instance.World.UserCardData, this.currentResourcePacketSize);
            this.buyHeadingLabel.Text = SK.Text("CapitalTradePanel_Buy", "Buy") + " : " + VillageBuildingsData.getResourceNames(this.currentResource);
            this.sellHeadingLabel.Text = SK.Text("CapitalTradePanel_Sell", "Sell") + " : " + VillageBuildingsData.getResourceNames(this.currentResource);
            this.buyHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
            this.sellHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
            this.buyTrack.Max = 0xc350;
            this.sellTrack.Max = 0xc350;
            if (this.lastHighlightResource != this.currentResource)
            {
                this.lastHighlightResource = this.currentResource;
                this.buyTrack.Value = 0xc350;
                this.sellTrack.Value = 0xc350;
            }
            this.showBuySellWindow();
        }

        public void selectStockExchange(int villageID)
        {
            if (villageID == -2)
            {
                villageID = this.selectedStockExchange;
            }
            if (villageID < 0)
            {
                this.selectedStockExchange = -1;
            }
            else
            {
                this.selectedStockExchange = villageID;
                bool flag = true;
                if (GameEngine.Instance.World.isAccountPremium())
                {
                    flag = false;
                    int num = villageID;
                    VillageMap village = GameEngine.Instance.Village;
                    if (village != null)
                    {
                        num = village.VillageID;
                    }
                    List<ClosestCapitalSortItem> list = new List<ClosestCapitalSortItem>();
                    foreach (int num2 in GameEngine.Instance.World.getCapitalList())
                    {
                        if (num2 != villageID)
                        {
                            int num3 = GameEngine.Instance.World.getSquareDistance(num, num2);
                            if ((num3 < 0x9c40) && GameEngine.Instance.World.allowExchangeTrade(num2, num))
                            {
                                ClosestCapitalSortItem item = new ClosestCapitalSortItem {
                                    distance = num3,
                                    villageID = num2
                                };
                                list.Add(item);
                            }
                        }
                    }
                    this.closeCapitalsToTest.Clear();
                    this.closeCapitalsToTest.Add(villageID);
                    list.Sort((Comparison<ClosestCapitalSortItem>) ((a, b) => a.distance.CompareTo(b.distance)));
                    if (list.Count > 20)
                    {
                        list.RemoveRange(20, list.Count - 20);
                    }
                    List<int> list3 = new List<int>();
                    foreach (ClosestCapitalSortItem item2 in list)
                    {
                        this.closeCapitalsToTest.Add(item2.villageID);
                        bool flag2 = true;
                        if (this.stockExchanges[item2.villageID] != null)
                        {
                            StockExchangeInfo info = (StockExchangeInfo) this.stockExchanges[item2.villageID];
                            TimeSpan span = (TimeSpan) (DateTime.Now - info.lastTime);
                            if (span.TotalMinutes < 1.0)
                            {
                                flag2 = false;
                            }
                        }
                        if (flag2)
                        {
                            list3.Add(item2.villageID);
                        }
                    }
                    if (list3.Count > 0)
                    {
                        RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(new RemoteServices.GetStockExchangeData_UserCallBack(this.getStockExchangeDataCallback));
                        RemoteServices.Instance.GetStockExchangePremiumData(villageID, list3.ToArray());
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    bool flag3 = true;
                    if (this.stockExchanges[this.selectedStockExchange] != null)
                    {
                        StockExchangeInfo info2 = (StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
                        TimeSpan span2 = (TimeSpan) (DateTime.Now - info2.lastTime);
                        if (span2.TotalMinutes < 1.0)
                        {
                            flag3 = false;
                        }
                    }
                    if (flag3)
                    {
                        RemoteServices.Instance.set_GetStockExchangeData_UserCallBack(new RemoteServices.GetStockExchangeData_UserCallBack(this.getStockExchangeDataCallback));
                        RemoteServices.Instance.GetStockExchangeData(villageID, true);
                    }
                }
                this.updateDeliveryTime(villageID);
                this.exchangeNameLabel.Text = GameEngine.Instance.World.getExchangeName(villageID);
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
                    this.buyTrack.Max = 0xc350;
                    this.buyTrack.Value = this.BACKUP_buyLevel;
                    this.sellTrack.Max = 0xc350;
                    this.sellTrack.Value = this.BACKUP_sellLevel;
                }
                this.updateValues();
            }
        }

        private void sellClick()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = (TimeSpan) (now - this.lastTradeTime);
            if (span.TotalSeconds >= 3.0)
            {
                this.lastTradeTime = now;
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    this.dirtyStockExchangeInfo(this.selectedStockExchange);
                    village.stockExchangeTrade(this.selectedStockExchange, this.currentResource, this.sellTrack.Value * this.currentResourcePacketSize, false);
                    this.addVillageToHistory(this.selectedStockExchange);
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

        private void setRowInfo(int line, int resource)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            CustomSelfDrawPanel.CSDButton button = this.getRowButton(line);
            button.ImageIcon = (Image) GFXLibrary.getCommodity32DSImage(resource);
            button.Text.Text = VillageBuildingsData.getResourceNames(resource);
            button.Data = resource;
            int numSpaces = GameEngine.Instance.LocalWorldData.traderCarryingLevels[resource];
            button.Text2.Text = CardTypes.adjustTraderCarryLevels(GameEngine.Instance.World.UserCardData, numSpaces).ToString("N", nFI);
            this.getRowHighestButton(line).Data = resource;
            this.getRowLowestButton(line).Data = resource;
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
            if (GameEngine.Instance.World.isAccountPremium() && Program.mySettings.AdvancedTrading)
            {
                this.getRowHighestButton(row).Visible = priceValue >= 0;
                this.getRowLowestButton(row).Visible = priceValue >= 0;
            }
        }

        private void showBuySellWindow()
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            bool visible = this.buyWindow.Visible;
            bool flag2 = this.sellWindow.Visible;
            this.buyWindow.Visible = false;
            this.sellWindow.Visible = false;
            VillageMap village = GameEngine.Instance.Village;
            if ((((village != null) && (this.currentResource >= 0)) && ((this.selectedStockExchange >= 0) && (this.stockExchanges[this.selectedStockExchange] != null))) && (this.currentResource > 0))
            {
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                StockExchangeInfo info2 = (StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
                int num = (int) village.getResourceLevel(this.currentResource);
                int storedLevel = info2.getLevel(this.currentResource);
                int numFreeTraders = this.numFreeTraders;
                int num4 = TradingCalcs.calcGoldCost(localWorldData, storedLevel, this.currentResource, storedLevel - (this.buyTrack.Value * this.currentResourcePacketSize));
                int num5 = TradingCalcs.calcGoldCost(localWorldData, storedLevel, this.currentResource, storedLevel + (this.sellTrack.Value * this.currentResourcePacketSize));
                if (num >= this.currentResourcePacketSize)
                {
                    this.sellWindow.Visible = true;
                    int num6 = num / this.currentResourcePacketSize;
                    if (num6 > numFreeTraders)
                    {
                        num6 = numFreeTraders;
                    }
                    int max = this.sellTrack.Max;
                    if (num6 > max)
                    {
                        this.sellTrack.Max = num6;
                    }
                    else if (num6 < max)
                    {
                        if (this.sellTrack.Value > num6)
                        {
                            this.sellTrack.Value = num6;
                        }
                        this.sellTrack.Max = num6;
                    }
                    int buyCost = num5;
                    buyCost = TradingCalcs.calcSellCost(localWorldData, buyCost) * this.sellTrack.Value;
                    this.sellCostValue.Text = ((buyCost * this.currentResourcePacketSize) / this.currentResourcePacketSizeREAL).ToString("N", nFI);
                    this.sellNumber.Text = (this.sellTrack.Value * this.currentResourcePacketSize).ToString("N", nFI);
                    this.sellMax.Text = (this.sellTrack.Max * this.currentResourcePacketSize).ToString("N", nFI);
                    this.sellHeadingImage.invalidate();
                }
                if (storedLevel >= this.currentResourcePacketSize)
                {
                    this.buyWindow.Visible = true;
                    int num9 = (int) GameEngine.Instance.World.getCurrentGold();
                    int num10 = storedLevel / this.currentResourcePacketSize;
                    int num11 = num9 / num4;
                    if (numFreeTraders > num11)
                    {
                        numFreeTraders = num11;
                    }
                    if (num10 > numFreeTraders)
                    {
                        num10 = numFreeTraders;
                    }
                    int num12 = this.buyTrack.Max;
                    if (num10 > num12)
                    {
                        this.buyTrack.Max = num10;
                    }
                    else if (num10 < num12)
                    {
                        if (this.buyTrack.Value > num10)
                        {
                            this.buyTrack.Value = num10;
                        }
                        this.buyTrack.Max = num10;
                    }
                    num4 = (num4 * this.currentResourcePacketSize) / this.currentResourcePacketSizeREAL;
                    this.buyCostValue.Text = (this.buyTrack.Value * num4).ToString("N", nFI);
                    this.buyNumber.Text = (this.buyTrack.Value * this.currentResourcePacketSize).ToString("N", nFI);
                    this.buyMax.Text = (this.buyTrack.Max * this.currentResourcePacketSize).ToString("N", nFI);
                    this.buyHeadingImage.invalidate();
                }
            }
            if (this.buyWindow.Visible || this.sellWindow.Visible)
            {
                this.stockExchangeImage.Alpha = 0.15f;
            }
            else
            {
                this.stockExchangeImage.Alpha = 1f;
            }
            this.validateBuySellButtons();
            if ((visible != this.buyWindow.Visible) || (flag2 != this.sellWindow.Visible))
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
                this.updateExchangeHistory();
            }
            else
            {
                this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
                this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
                this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
                this.exchangeArrowButton.Data = 0;
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
            this.sellWindow.invalidate();
        }

        public void tradingClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(2);
        }

        public void update()
        {
            if (this.currentResource >= 0)
            {
                this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
                this.currentResourcePacketSizeREAL = this.currentResourcePacketSize;
                this.currentResourcePacketSize = CardTypes.adjustTraderCarryLevels(GameEngine.Instance.World.UserCardData, this.currentResourcePacketSize);
            }
            this.updateValues();
            this.cardbar.update();
            this.updateDeliveryTime(this.selectedStockExchange);
            if (this.lastPremiumType != GameEngine.Instance.World.isAccountPremium())
            {
                this.lastPremiumType = GameEngine.Instance.World.isAccountPremium();
                this.updateAdvancedOptions();
            }
        }

        private void updateAdvancedOptions()
        {
            if (GameEngine.Instance.World.isAccountPremium() && Program.mySettings.AdvancedTrading)
            {
                for (int i = 0; i < 8; i++)
                {
                    CustomSelfDrawPanel.CSDButton button = this.getRowHighestButton(i);
                    CustomSelfDrawPanel.CSDButton button2 = this.getRowLowestButton(i);
                    CustomSelfDrawPanel.CSDLabel label = this.getRowStored(i);
                    CustomSelfDrawPanel.CSDLabel label2 = this.getRowPrice(i);
                    if (label2.Text.Length > 0)
                    {
                        button.Visible = true;
                        button2.Visible = true;
                    }
                    else
                    {
                        button.Visible = false;
                        button2.Visible = false;
                    }
                    label.Position = new Point(0xc6, 1);
                    label2.Position = new Point(0x132, 1);
                }
                this.lightArea2.Position = new Point(0x15, 0x66);
                this.lightArea3.Position = new Point(0x81, 0x66);
            }
            else
            {
                for (int j = 0; j < 8; j++)
                {
                    this.getRowHighestButton(j).Visible = false;
                    this.getRowLowestButton(j).Visible = false;
                    this.getRowStored(j).Position = new Point(230, 1);
                    this.getRowPrice(j).Position = new Point(0x152, 1);
                }
                this.lightArea2.Position = new Point(0x35, 0x66);
                this.lightArea3.Position = new Point(0xa1, 0x66);
            }
            this.advancedOptions.Visible = GameEngine.Instance.World.isAccountPremium();
            this.mainBackgroundImage.invalidate();
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
                int num3 = GameEngine.Instance.World.getVillageRegion(village.VillageID);
                int num4 = point2.X;
                int num5 = point2.Y;
                int num6 = GameEngine.Instance.World.getVillageRegion(villageID);
                double d = 0.0;
                if (num3 != num6)
                {
                    d = ((x - num4) * (x - num4)) + ((y - num5) * (y - num5));
                    d = Math.Sqrt(d) * (localWorldData.traderMoveSpeed * localWorldData.gamePlaySpeed);
                }
                else
                {
                    d = localWorldData.traderStockExchangeSameRegionTime;
                }
                d = GameEngine.Instance.World.UserResearchData.adjustTradeTimes(d) * CardTypes.cards_adjustTradeTimes(GameEngine.Instance.World.UserCardData);
                string str = VillageMap.createBuildTimeString((int) CardTypes.cards_adjustTradeTimesCompleteContract(GameEngine.Instance.World.UserCardData, d));
                this.deliveryTimeAreaLabel.TextDiffOnly = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":  " + str;
            }
            else
            {
                this.deliveryTimeAreaLabel.TextDiffOnly = SK.Text("TRADE_Delivery_Time", "Delivery Time") + ":  ";
            }
        }

        public void updateExchangeHistory()
        {
            for (int i = 0; i < 0x11; i++)
            {
                this.getVillageHistory(i).Visible = false;
                this.getVillageHistoryFavourite(i).Visible = false;
                this.getVillageHistoryDelete(i).Visible = false;
            }
            int line = 0;
            while ((line < 0x11) && (line < exchangeFavourites.Count))
            {
                WorldMap.VillageNameItem item = exchangeFavourites[line];
                CustomSelfDrawPanel.CSDButton button4 = this.getVillageHistory(line);
                button4.Visible = true;
                button4.Text.Text = GameEngine.Instance.World.getExchangeName(item.villageID);
                button4.Data = item.villageID;
                CustomSelfDrawPanel.CSDButton button5 = this.getVillageHistoryFavourite(line);
                button5.ImageNorm = (Image) GFXLibrary.star_market_1;
                button5.Visible = true;
                button5.Data = item.villageID;
                button5.CustomTooltipID = 0x328;
                this.getVillageHistoryDelete(line).Data = item.villageID;
                line++;
            }
            for (int j = 0; (line < 0x11) && (j < exchangeHistory.Count); j++)
            {
                WorldMap.VillageNameItem item2 = exchangeHistory[j];
                bool flag = false;
                foreach (WorldMap.VillageNameItem item3 in exchangeFavourites)
                {
                    if (item3.villageID == item2.villageID)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    CustomSelfDrawPanel.CSDButton button7 = this.getVillageHistory(line);
                    button7.Visible = true;
                    button7.Text.Text = GameEngine.Instance.World.getExchangeName(item2.villageID);
                    button7.Data = item2.villageID;
                    CustomSelfDrawPanel.CSDButton button8 = this.getVillageHistoryFavourite(line);
                    button8.ImageNorm = (Image) GFXLibrary.star_market_2;
                    button8.Visible = true;
                    button8.Data = item2.villageID;
                    button8.CustomTooltipID = 0x329;
                    CustomSelfDrawPanel.CSDButton button9 = this.getVillageHistoryDelete(line);
                    button9.Visible = true;
                    button9.Data = item2.villageID;
                    button9.CustomTooltipID = 810;
                    line++;
                }
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
                StockExchangeInfo info2 = null;
                if ((this.selectedStockExchange >= 0) && (this.stockExchanges[this.selectedStockExchange] != null))
                {
                    info2 = (StockExchangeInfo) this.stockExchanges[this.selectedStockExchange];
                    this.updateDeliveryTime(this.selectedStockExchange);
                }
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                switch (this.lastTab)
                {
                    case 1:
                    {
                        VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                        village.getStockpileLevels(levels);
                        if (info2 != null)
                        {
                            this.setRowValues(0, (int) levels.woodLevel, info2.woodLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(6), 6));
                            this.setRowValues(1, (int) levels.stoneLevel, info2.stoneLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(7), 7));
                            this.setRowValues(2, (int) levels.ironLevel, info2.ironLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(8), 8));
                            this.setRowValues(3, (int) levels.pitchLevel, info2.pitchLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(9), 9));
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
                        if (info2 != null)
                        {
                            this.setRowValues(0, (int) levels2.applesLevel, info2.applesLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(13), 13));
                            this.setRowValues(1, (int) levels2.cheeseLevel, info2.cheeseLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x11), 0x11));
                            this.setRowValues(2, (int) levels2.meatLevel, info2.meatLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x10), 0x10));
                            this.setRowValues(3, (int) levels2.breadLevel, info2.breadLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(14), 14));
                            this.setRowValues(4, (int) levels2.vegLevel, info2.vegLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(15), 15));
                            this.setRowValues(5, (int) levels2.fishLevel, info2.fishLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x12), 0x12));
                            this.setRowValues(6, (int) levels3.aleLevel, info2.aleLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(12), 12));
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
                        if (info2 != null)
                        {
                            this.setRowValues(0, (int) levels4.bowsLevel, info2.bowsLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x1d), 0x1d));
                            this.setRowValues(1, (int) levels4.pikesLevel, info2.pikesLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x1c), 0x1c));
                            this.setRowValues(2, (int) levels4.armourLevel, info2.armourLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x1f), 0x1f));
                            this.setRowValues(3, (int) levels4.swordsLevel, info2.swordsLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(30), 30));
                            this.setRowValues(4, (int) levels4.catapultsLevel, info2.catapultsLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x20), 0x20));
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
                        if (info2 != null)
                        {
                            this.setRowValues(0, (int) levels5.venisonLevel, info2.venisonLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x16), 0x16));
                            this.setRowValues(1, (int) levels5.furnitureLevel, info2.furnitureLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x15), 0x15));
                            this.setRowValues(2, (int) levels5.metalwareLevel, info2.metalwareLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x1a), 0x1a));
                            this.setRowValues(3, (int) levels5.clothesLevel, info2.clothesLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x13), 0x13));
                            this.setRowValues(4, (int) levels5.wineLevel, info2.wineLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x21), 0x21));
                            this.setRowValues(5, (int) levels5.saltLevel, info2.saltLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x17), 0x17));
                            this.setRowValues(6, (int) levels5.spicesLevel, info2.spicesLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x18), 0x18));
                            this.setRowValues(7, (int) levels5.silkLevel, info2.silkLevel, TradingCalcs.calcSellCost(localWorldData, info2.getFakeLevel(0x19), 0x19));
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
            if (this.sellWindow.Visible && (this.sellTrack.Value > 0))
            {
                this.sellButton.Enabled = true;
            }
            else
            {
                this.sellButton.Enabled = false;
            }
        }

        private void villageClicked()
        {
            if (base.ClickedControl != null)
            {
                GameEngine.Instance.playInterfaceSound("StockExchangePanel_village_clicked");
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                this.BACKUP_resource = this.currentResource;
                this.BACKUP_buyLevel = this.buyTrack.Value;
                this.BACKUP_sellLevel = this.sellTrack.Value;
                this.selectStockExchange(clickedControl.Data);
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
                foreach (WorldMap.VillageNameItem item in exchangeFavourites)
                {
                    if (item.villageID == data)
                    {
                        flag = true;
                        exchangeFavourites.Remove(item);
                        break;
                    }
                }
                if (flag)
                {
                    RemoteServices.Instance.UpdateVillageFavourites(3, data);
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
                    RemoteServices.Instance.UpdateVillageFavourites(2, data);
                    WorldMap.VillageNameItem item2 = new WorldMap.VillageNameItem {
                        villageID = data,
                        villageName = GameEngine.Instance.World.getExchangeName(data)
                    };
                    exchangeFavourites.Add(item2);
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
                RemoteServices.Instance.UpdateVillageFavourites(6, data);
                foreach (WorldMap.VillageNameItem item in exchangeHistory)
                {
                    if (item.villageID == data)
                    {
                        exchangeHistory.Remove(item);
                        this.updateExchangeHistory();
                        break;
                    }
                }
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

        public class ClosestCapitalSortItem
        {
            public int distance;
            public int villageID = -1;
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
            public DateTime lastTime = DateTime.MinValue;
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

