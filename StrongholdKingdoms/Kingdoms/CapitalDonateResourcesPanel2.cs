namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CapitalDonateResourcesPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage buildingImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel buildingTypeName = new CustomSelfDrawPanel.CSDLabel();
        public static string capitalTooltipText = "";
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel currentLevelName = new CustomSelfDrawPanel.CSDLabel();
        public int[,] currentLevelsNeeded = new int[8, 2];
        private int currentResource = -1;
        private int currentResourcePacketSize = 1;
        private int currentSelectedRow = -1;
        private int currentSelectedVillageID = -1;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel effectDescription = new CustomSelfDrawPanel.CSDLabel();
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
        private CustomSelfDrawPanel.CSDImage illustration = new CustomSelfDrawPanel.CSDImage();
        public static CapitalDonateResourcesPanel2 instance = null;
        private DateTime lastTradeTime = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDLabel lblCurrentEffectLevelLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblCurrentLevelEffect = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblNextLevelEffect = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblNextLevelEffectLabel = new CustomSelfDrawPanel.CSDLabel();
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
        private VillageMapBuilding m_building;
        private int m_capitalVillageID = -1;
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
        private CustomSelfDrawPanel.CSDButton selectRow1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton selectRow8 = new CustomSelfDrawPanel.CSDButton();
        private bool sendAllowed;
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
        private CustomSelfDrawPanel.CSDButton stockExchangeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel storedHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel8 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel topWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDImage tradeWithImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel tradeWithLabel = new CustomSelfDrawPanel.CSDLabel();
        private List<VillageDonateInfo> villageInfo;
        private CustomSelfDrawPanel.CSDButton villageOwnPageDown = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageOwnPageUp = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage villageSelectPanel = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel villageSelectPanelLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton villageSelectPanelTab1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectPanelTab2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage10 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage11 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage12 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage13 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage14 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage15 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage16 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage17 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton villageSelectVillage9 = new CustomSelfDrawPanel.CSDButton();
        private int villageTabOwnPage;
        private CustomSelfDrawPanel.CSDButton worldMapButton = new CustomSelfDrawPanel.CSDButton();

        public CapitalDonateResourcesPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void btnDonate_Click()
        {
            int amount = this.sendTrack.Value;
            int currentResource = this.currentResource;
            int currentSelectedVillageID = this.currentSelectedVillageID;
            if (((amount > 0) && (currentResource >= 0)) && (currentSelectedVillageID >= 0))
            {
                RemoteServices.Instance.set_DonateCapitalGoods_UserCallBack(new RemoteServices.DonateCapitalGoods_UserCallBack(this.DonateCapitalGoodsCallback));
                RemoteServices.Instance.DonateCapitalGoods(this.m_capitalVillageID, currentSelectedVillageID, currentResource, amount, this.m_building.buildingType, this.m_building.buildingID);
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

        private void DonateCapitalGoodsCallback(DonateCapitalGoods_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.villageInfo = returnData.villageInfo;
                VillageMap village = GameEngine.Instance.Village;
                if ((village != null) && (returnData.updateBuildingData != null))
                {
                    List<VillageBuildingReturnData> newBuildings = new List<VillageBuildingReturnData> {
                        returnData.updateBuildingData
                    };
                    village.importVillageBuildings(newBuildings, false);
                    village.m_parishCapitalResearchData = returnData.researchData;
                }
                this.updateScreenInfo(this.m_building, village, false);
                this.updateVillageView(this.currentSelectedVillageID);
                InterfaceMgr.Instance.flushParishFrontPageInfo(GameEngine.Instance.World.getParishFromVillageID(this.currentSelectedVillageID));
                this.sendTrack.Max = 0x1dcd6500;
                this.sendTrack.Value = 0x1dcd6500;
                this.showSendWindow(false);
                this.currentSelectedRow = -1;
                this.currentResource = -1;
                this.selectHighlightLine(this.currentSelectedRow);
                this.mainBackgroundImage.invalidate();
            }
        }

        private void editSendValue()
        {
            InterfaceMgr.Instance.setFloatingValueSentDelegate(new InterfaceMgr.FloatingValueSent(this.floatingValueCB));
            Point point = InterfaceMgr.Instance.ParentForm.PointToScreen(new Point((620 + base.Location.X) + 0xd9, ((460 + base.Location.Y) + 120) - 50));
            FloatingInput.open(point.X, point.Y, this.sendTrack.Value, this.sendTrack.Max, InterfaceMgr.Instance.ParentForm);
        }

        private void exchangeArrowClick()
        {
            if (this.exchangeArrowButton.Data == 0)
            {
                this.showVillagePanel(true);
            }
            else
            {
                this.showVillagePanel(false);
            }
        }

        private void floatingValueCB(int value)
        {
            this.sendTrack.Value = value;
            NumberFormatInfo nFI = GameEngine.NFI;
            this.sendMax.Text = this.sendTrack.Max.ToString("N", nFI);
            this.sendNumber.Text = this.sendTrack.Value.ToString("N", nFI);
            this.sendNumberPackets.Text = SK.Text("DonateScreen_Packets", "Packets") + " : " + ((this.sendTrack.Value / this.currentResourcePacketSize)).ToString("N", nFI);
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

        public void GetVillageInfoForDonateCapitalGoods_callback(GetVillageInfoForDonateCapitalGoods_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.villageInfo = returnData.villageInfo;
                this.updateVillageHistory();
                this.updateVillageView(-1);
                this.showVillagePanel(false);
                if ((this.villageInfo != null) && (this.villageInfo.Count > 0))
                {
                    this.updateLocalValues(this.villageInfo[0].villageID);
                }
            }
        }

        public void init()
        {
        }

        public void init(int villageID, VillageMapBuilding selectedBuilding)
        {
            this.m_capitalVillageID = villageID;
            this.m_building = selectedBuilding;
            instance = this;
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("DonateScreen_Donate_to", "Donate to Capital Building"));
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CapitalDonateResourcesPanel2_close");
            this.closeButton.CustomTooltipID = 0x321;
            this.mainBackgroundArea.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 0x2a, new Point(0x382, 10));
            this.illustration.Image = (Image) GFXLibrary.donate_illustration;
            this.illustration.Position = new Point(0x26e, 0x29);
            this.mainBackgroundArea.addControl(this.illustration);
            this.midWindow.Size = new Size(0xe4, 0x199);
            this.midWindow.Position = new Point(0x16e, 0x90);
            this.mainBackgroundArea.addControl(this.midWindow);
            this.midWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.leftWindow.Size = new Size(0x14f, 0x199);
            this.leftWindow.Position = new Point(0x24, 0x90);
            this.mainBackgroundArea.addControl(this.leftWindow);
            this.leftWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.topWindow.Size = new Size(0x22d, 0x7c);
            this.topWindow.Position = new Point(0x24, 14);
            this.topWindow.CustomTooltipID = 0x76c;
            this.mainBackgroundArea.addControl(this.topWindow);
            this.topWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
            this.buildingImage.Image = (Image) GFXLibrary.townbuilding_archeryrange_normal;
            this.buildingImage.Position = new Point(0, 0);
            this.topWindow.addControl(this.buildingImage);
            int y = 11;
            this.buildingTypeName.Text = "";
            this.buildingTypeName.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.buildingTypeName.DropShadowColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.buildingTypeName.Position = new Point(0x60, y);
            this.buildingTypeName.Size = new Size(240, 30);
            this.buildingTypeName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.buildingTypeName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.topWindow.addControl(this.buildingTypeName);
            this.currentLevelName.Text = SK.Text("DonateScreen_Current_Level", "Current Level") + " : ";
            this.currentLevelName.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.currentLevelName.DropShadowColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.currentLevelName.Position = new Point(0x150, y);
            this.currentLevelName.Size = new Size(240, 30);
            this.currentLevelName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.currentLevelName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.topWindow.addControl(this.currentLevelName);
            this.lblCurrentEffectLevelLabel.Text = SK.Text("DonateScreen_Current_Level_Effect", "Current Level Effect") + " : ";
            this.lblCurrentEffectLevelLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.lblCurrentEffectLevelLabel.DropShadowColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblCurrentEffectLevelLabel.Position = new Point(0x60, y + 0x19);
            this.lblCurrentEffectLevelLabel.Size = new Size(160, 50);
            this.lblCurrentEffectLevelLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.lblCurrentEffectLevelLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.topWindow.addControl(this.lblCurrentEffectLevelLabel);
            this.lblCurrentLevelEffect.Text = "";
            this.lblCurrentLevelEffect.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.lblCurrentLevelEffect.DropShadowColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblCurrentLevelEffect.Position = new Point(0x105, y + 0x19);
            this.lblCurrentLevelEffect.Size = new Size(280, 50);
            this.lblCurrentLevelEffect.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.lblCurrentLevelEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.topWindow.addControl(this.lblCurrentLevelEffect);
            this.lblNextLevelEffectLabel.Text = SK.Text("DonateScreen_Next_Level_Effect", "Next Level Effect") + " : ";
            this.lblNextLevelEffectLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.lblNextLevelEffectLabel.DropShadowColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblNextLevelEffectLabel.Position = new Point(0x60, y + 0x3d);
            this.lblNextLevelEffectLabel.Size = new Size(160, 50);
            this.lblNextLevelEffectLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.lblNextLevelEffectLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.topWindow.addControl(this.lblNextLevelEffectLabel);
            this.lblNextLevelEffect.Text = "";
            this.lblNextLevelEffect.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.lblNextLevelEffect.DropShadowColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.lblNextLevelEffect.Position = new Point(0x105, y + 0x3d);
            this.lblNextLevelEffect.Size = new Size(280, 50);
            this.lblNextLevelEffect.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.lblNextLevelEffect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.topWindow.addControl(this.lblNextLevelEffect);
            this.lightArea1.Size = new Size(0x61, 0x149);
            this.lightArea1.Position = new Point(0xd8, 0x3e);
            this.leftWindow.addControl(this.lightArea1);
            this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.localHeadingLabel.Text = SK.Text("TRADE_Local", "Local");
            this.localHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.localHeadingLabel.Position = new Point(0, -35);
            this.localHeadingLabel.Size = new Size(0x61, 30);
            this.localHeadingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.localHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea1.addControl(this.localHeadingLabel);
            this.lightArea2.Size = new Size(0xba, 0x149);
            this.lightArea2.Position = new Point(0x15, 0x3e);
            this.midWindow.addControl(this.lightArea2);
            this.lightArea2.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
            this.storedHeadingLabel.Text = SK.Text("DonateScreen_For_Level", "For Level") + " : 6";
            this.storedHeadingLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.storedHeadingLabel.Position = new Point(0, -65);
            this.storedHeadingLabel.Size = new Size(0xba, 30);
            this.storedHeadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.storedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lightArea2.addControl(this.storedHeadingLabel);
            this.exchangeNameBar.Size = new Size(0xcd, 0x1f);
            this.exchangeNameBar.Position = new Point(11, 9);
            this.exchangeNameBar.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick), "CapitalDonateResourcesPanel2_select");
            this.leftWindow.addControl(this.exchangeNameBar);
            this.exchangeNameBar.Create((Image) GFXLibrary.int_lineitem_inset_left, (Image) GFXLibrary.int_lineitem_inset_middle, (Image) GFXLibrary.int_lineitem_inset_right);
            this.exchangeNameLabel.Text = SK.Text("TRADE_Select_Village", "Select Village");
            this.exchangeNameLabel.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.exchangeNameLabel.Position = new Point(0x11, 7);
            this.exchangeNameLabel.Size = new Size((this.exchangeNameBar.Size.Width - 0x11) - 20, this.exchangeNameBar.Size.Height - 13);
            this.exchangeNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.exchangeNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.exchangeNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick), "CapitalDonateResourcesPanel2_select");
            this.exchangeNameBar.addControl(this.exchangeNameLabel);
            this.exchangeArrowButton.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
            this.exchangeArrowButton.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
            this.exchangeArrowButton.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
            this.exchangeArrowButton.Position = new Point(0xb5, 7);
            this.exchangeArrowButton.MoveOnClick = false;
            this.exchangeArrowButton.Data = 0;
            this.exchangeArrowButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.exchangeArrowClick), "CapitalDonateResourcesPanel2_select");
            this.exchangeNameBar.addControl(this.exchangeArrowButton);
            this.sendWindow.Size = new Size(0x150, 0x91);
            this.sendWindow.Position = new Point(0x26e, 0x174);
            this.sendWindow.Visible = false;
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
            this.sendNumberPackets.Text = SK.Text("DonateScreen_Packets", "Packets") + " : 0";
            this.sendNumberPackets.Color = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.sendNumberPackets.Position = new Point(-17, 12);
            this.sendNumberPackets.Size = new Size(150, 30);
            this.sendNumberPackets.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sendNumberPackets.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.sendSubWindow.addControl(this.sendNumberPackets);
            this.sendButton.Position = new Point(0xb1, 0x5e);
            this.sendButton.Size = new Size(0x99, 0x26);
            this.sendButton.Text.Text = SK.Text("MarketTradeScreen_Send", "Send");
            this.sendButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.sendButton.TextYOffset = -1;
            this.sendButton.Text.Color = ARGBColors.Black;
            this.sendButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendClick), "CapitalDonateResourcesPanel2_send");
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
            this.sendEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.sendEditButton.MoveOnClick = true;
            this.sendEditButton.OverBrighten = true;
            this.sendEditButton.Position = new Point(7, 5);
            this.sendEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "CapitalDonateResourcesPanel2_editValue");
            this.sendSubWindow.addControl(this.sendEditButton);
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
            this.highlightLine1.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine1.Position = new Point(0x99, 0x47);
            this.highlightLine1.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine1);
            this.highlightLine2.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine2.Position = new Point(0x99, 0x6f);
            this.highlightLine2.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine2);
            this.highlightLine3.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine3.Position = new Point(0x99, 0x97);
            this.highlightLine3.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine3);
            this.highlightLine4.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine4.Position = new Point(0x99, 0xbf);
            this.highlightLine4.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine4);
            this.highlightLine5.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine5.Position = new Point(0x99, 0xe7);
            this.highlightLine5.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine5);
            this.highlightLine6.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine6.Position = new Point(0x99, 0x10f);
            this.highlightLine6.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine6);
            this.highlightLine7.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine7.Position = new Point(0x99, 0x137);
            this.highlightLine7.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine7);
            this.highlightLine8.Image = (Image) GFXLibrary.int_white_highlight_bar;
            this.highlightLine8.Position = new Point(0x99, 0x15f);
            this.highlightLine8.Size = new Size(400, 0x1f);
            this.leftWindow.addControl(this.highlightLine8);
            this.selectRow1.Position = new Point(-134, -3);
            this.selectRow1.Size = new Size(0xbf, 0x26);
            this.selectRow1.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow1.Text.Position = new Point(0x47, 0);
            this.selectRow1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow1.TextYOffset = -1;
            this.selectRow1.Text.Color = ARGBColors.Black;
            this.selectRow1.ImageIconPosition = new Point(0x1a, -3);
            this.selectRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine1.addControl(this.selectRow1);
            this.selectRow1.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow1.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow2.Position = new Point(-134, -3);
            this.selectRow2.Size = new Size(0xbf, 0x26);
            this.selectRow2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow2.Text.Position = new Point(0x47, 0);
            this.selectRow2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow2.TextYOffset = -1;
            this.selectRow2.Text.Color = ARGBColors.Black;
            this.selectRow2.ImageIconPosition = new Point(0x1a, -3);
            this.selectRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine2.addControl(this.selectRow2);
            this.selectRow2.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow2.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow3.Position = new Point(-134, -3);
            this.selectRow3.Size = new Size(0xbf, 0x26);
            this.selectRow3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow3.Text.Position = new Point(0x47, 0);
            this.selectRow3.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow3.TextYOffset = -1;
            this.selectRow3.Text.Color = ARGBColors.Black;
            this.selectRow3.ImageIconPosition = new Point(0x1a, -3);
            this.selectRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine3.addControl(this.selectRow3);
            this.selectRow3.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow3.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow4.Position = new Point(-134, -3);
            this.selectRow4.Size = new Size(0xbf, 0x26);
            this.selectRow4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow4.Text.Position = new Point(0x47, 0);
            this.selectRow4.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow4.TextYOffset = -1;
            this.selectRow4.Text.Color = ARGBColors.Black;
            this.selectRow4.ImageIconPosition = new Point(0x1a, -3);
            this.selectRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine4.addControl(this.selectRow4);
            this.selectRow4.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow4.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow5.Position = new Point(-134, -3);
            this.selectRow5.Size = new Size(0xbf, 0x26);
            this.selectRow5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow5.Text.Position = new Point(0x47, 0);
            this.selectRow5.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow5.TextYOffset = -1;
            this.selectRow5.Text.Color = ARGBColors.Black;
            this.selectRow5.ImageIconPosition = new Point(0x1a, -3);
            this.selectRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine5.addControl(this.selectRow5);
            this.selectRow5.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow5.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow6.Position = new Point(-134, -3);
            this.selectRow6.Size = new Size(0xbf, 0x26);
            this.selectRow6.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow6.Text.Position = new Point(0x47, 0);
            this.selectRow6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow6.TextYOffset = -1;
            this.selectRow6.Text.Color = ARGBColors.Black;
            this.selectRow6.ImageIconPosition = new Point(0x1a, -3);
            this.selectRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine6.addControl(this.selectRow6);
            this.selectRow6.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow6.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow7.Position = new Point(-134, -3);
            this.selectRow7.Size = new Size(0xbf, 0x26);
            this.selectRow7.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow7.Text.Position = new Point(0x47, 0);
            this.selectRow7.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow7.TextYOffset = -1;
            this.selectRow7.Text.Color = ARGBColors.Black;
            this.selectRow7.ImageIconPosition = new Point(0x1a, -3);
            this.selectRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rowClicked));
            this.highlightLine7.addControl(this.selectRow7);
            this.selectRow7.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.selectRow7.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            this.selectRow8.Position = new Point(-134, -3);
            this.selectRow8.Size = new Size(0xbf, 0x26);
            this.selectRow8.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.selectRow8.Text.Position = new Point(0x47, 0);
            this.selectRow8.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.selectRow8.TextYOffset = -1;
            this.selectRow8.Text.Color = ARGBColors.Black;
            this.selectRow8.ImageIconPosition = new Point(0x1a, -3);
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
            this.storedLabel1.Text = "/";
            this.storedLabel1.Color = ARGBColors.Black;
            this.storedLabel1.Position = new Point(0x65, 1);
            this.storedLabel1.Size = new Size(0xba, 0x1f);
            this.storedLabel1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine1.addControl(this.storedLabel1);
            this.storedLabel2.Text = "/";
            this.storedLabel2.Color = ARGBColors.Black;
            this.storedLabel2.Position = new Point(0x65, 1);
            this.storedLabel2.Size = new Size(0xba, 0x1f);
            this.storedLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine2.addControl(this.storedLabel2);
            this.storedLabel3.Text = "/";
            this.storedLabel3.Color = ARGBColors.Black;
            this.storedLabel3.Position = new Point(0x65, 1);
            this.storedLabel3.Size = new Size(0xba, 0x1f);
            this.storedLabel3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine3.addControl(this.storedLabel3);
            this.storedLabel4.Text = "/";
            this.storedLabel4.Color = ARGBColors.Black;
            this.storedLabel4.Position = new Point(0x65, 1);
            this.storedLabel4.Size = new Size(0xba, 0x1f);
            this.storedLabel4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine4.addControl(this.storedLabel4);
            this.storedLabel5.Text = "/";
            this.storedLabel5.Color = ARGBColors.Black;
            this.storedLabel5.Position = new Point(0x65, 1);
            this.storedLabel5.Size = new Size(0xba, 0x1f);
            this.storedLabel5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine5.addControl(this.storedLabel5);
            this.storedLabel6.Text = "/";
            this.storedLabel6.Color = ARGBColors.Black;
            this.storedLabel6.Position = new Point(0x65, 1);
            this.storedLabel6.Size = new Size(0xba, 0x1f);
            this.storedLabel6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine6.addControl(this.storedLabel6);
            this.storedLabel7.Text = "?";
            this.storedLabel7.Color = ARGBColors.Black;
            this.storedLabel7.Position = new Point(0x65, 1);
            this.storedLabel7.Size = new Size(0xba, 0x1f);
            this.storedLabel7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine7.addControl(this.storedLabel7);
            this.storedLabel8.Text = "/";
            this.storedLabel8.Color = ARGBColors.Black;
            this.storedLabel8.Position = new Point(0x65, 1);
            this.storedLabel8.Size = new Size(0xba, 0x1f);
            this.storedLabel8.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.storedLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.highlightLine8.addControl(this.storedLabel8);
            this.priceLabel1.Text = "0";
            this.priceLabel1.Color = ARGBColors.Black;
            this.priceLabel1.Position = new Point(0x120, 1);
            this.priceLabel1.Size = new Size(0x61, 0x1f);
            this.priceLabel1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine1.addControl(this.priceLabel1);
            this.priceLabel2.Text = "0";
            this.priceLabel2.Color = ARGBColors.Black;
            this.priceLabel2.Position = new Point(0x120, 1);
            this.priceLabel2.Size = new Size(0x61, 0x1f);
            this.priceLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine2.addControl(this.priceLabel2);
            this.priceLabel3.Text = "0";
            this.priceLabel3.Color = ARGBColors.Black;
            this.priceLabel3.Position = new Point(0x120, 1);
            this.priceLabel3.Size = new Size(0x61, 0x1f);
            this.priceLabel3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine3.addControl(this.priceLabel3);
            this.priceLabel4.Text = "0";
            this.priceLabel4.Color = ARGBColors.Black;
            this.priceLabel4.Position = new Point(0x120, 1);
            this.priceLabel4.Size = new Size(0x61, 0x1f);
            this.priceLabel4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine4.addControl(this.priceLabel4);
            this.priceLabel5.Text = "0";
            this.priceLabel5.Color = ARGBColors.Black;
            this.priceLabel5.Position = new Point(0x120, 1);
            this.priceLabel5.Size = new Size(0x61, 0x1f);
            this.priceLabel5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine5.addControl(this.priceLabel5);
            this.priceLabel6.Text = "0";
            this.priceLabel6.Color = ARGBColors.Black;
            this.priceLabel6.Position = new Point(0x120, 1);
            this.priceLabel6.Size = new Size(0x61, 0x1f);
            this.priceLabel6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine6.addControl(this.priceLabel6);
            this.priceLabel7.Text = "0";
            this.priceLabel7.Color = ARGBColors.Black;
            this.priceLabel7.Position = new Point(0x120, 1);
            this.priceLabel7.Size = new Size(0x61, 0x1f);
            this.priceLabel7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine7.addControl(this.priceLabel7);
            this.priceLabel8.Text = "0";
            this.priceLabel8.Color = ARGBColors.Black;
            this.priceLabel8.Position = new Point(0x120, 1);
            this.priceLabel8.Size = new Size(0x61, 0x1f);
            this.priceLabel8.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.priceLabel8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.highlightLine8.addControl(this.priceLabel8);
            this.villageSelectPanel.Image = (Image) GFXLibrary.int_villagelist_panel;
            this.villageSelectPanel.Size = new Size(this.villageSelectPanel.Image.Width, 0x151);
            this.villageSelectPanel.Position = new Point(0x35, 180);
            this.villageSelectPanel.Visible = false;
            this.mainBackgroundArea.addControl(this.villageSelectPanel);
            this.villageSelectPanelTab1.ImageNorm = (Image) GFXLibrary.tab_villagename_forward;
            this.villageSelectPanelTab1.ImageOver = (Image) GFXLibrary.tab_villagename_forward;
            this.villageSelectPanelTab1.ImageClick = (Image) GFXLibrary.tab_villagename_forward;
            this.villageSelectPanelTab1.Position = new Point(0, 3);
            this.villageSelectPanelTab1.Text.Text = SK.Text("GENERIC_Villages", "Villages");
            this.villageSelectPanelTab1.TextYOffset = -1;
            this.villageSelectPanelTab1.Data = 0;
            this.villageSelectPanelTab1.MoveOnClick = false;
            this.villageSelectPanelTab1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.villageSelectPanelTab1.Text.Color = ARGBColors.Black;
            this.villageSelectPanel.addControl(this.villageSelectPanelTab1);
            this.villageSelectVillage1.ImageNorm = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage1.ImageOver = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage1.ImageClick = (Image) GFXLibrary.int_villagelist_panel_highlight;
            this.villageSelectVillage1.ImageNorm = null;
            this.villageSelectVillage1.Position = new Point(3, 0x15);
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
            this.villageSelectVillage2.Position = new Point(3, 0x27);
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
            this.villageSelectVillage3.Position = new Point(3, 0x39);
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
            this.villageSelectVillage4.Position = new Point(3, 0x4b);
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
            this.villageSelectVillage5.Position = new Point(3, 0x5d);
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
            this.villageSelectVillage6.Position = new Point(3, 0x6f);
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
            this.villageSelectVillage7.Position = new Point(3, 0x81);
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
            this.villageSelectVillage8.Position = new Point(3, 0x93);
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
            this.villageSelectVillage9.Position = new Point(3, 0xa5);
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
            this.villageSelectVillage10.Position = new Point(3, 0xb7);
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
            this.villageSelectVillage11.Position = new Point(3, 0xc9);
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
            this.villageSelectVillage12.Position = new Point(3, 0xdb);
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
            this.villageSelectVillage13.Position = new Point(3, 0xed);
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
            this.villageSelectVillage14.Position = new Point(3, 0xff);
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
            this.villageSelectVillage15.Position = new Point(3, 0x111);
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
            this.villageSelectVillage16.Position = new Point(3, 0x123);
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
            this.villageSelectVillage17.Position = new Point(3, 0x135);
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
            this.villageOwnPageUp.ImageNorm = (Image) GFXLibrary.int_button_droparrow_up_normal;
            this.villageOwnPageUp.ImageOver = (Image) GFXLibrary.int_button_droparrow_up_over;
            this.villageOwnPageUp.ImageClick = (Image) GFXLibrary.int_button_droparrow_up_down;
            this.villageOwnPageUp.Position = new Point(0x87, 0x13a);
            this.villageOwnPageUp.MoveOnClick = false;
            this.villageOwnPageUp.Data = 0;
            this.villageOwnPageUp.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnPageClicked), "CapitalDonateResourcesPanel2_page_up");
            this.villageSelectPanel.addControl(this.villageOwnPageUp);
            this.villageOwnPageDown.ImageNorm = (Image) GFXLibrary.int_button_droparrow_normal;
            this.villageOwnPageDown.ImageOver = (Image) GFXLibrary.int_button_droparrow_over;
            this.villageOwnPageDown.ImageClick = (Image) GFXLibrary.int_button_droparrow_down;
            this.villageOwnPageDown.Position = new Point(0xa5, 0x13a);
            this.villageOwnPageDown.MoveOnClick = false;
            this.villageOwnPageDown.Data = 1;
            this.villageOwnPageDown.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnPageClicked), "CapitalDonateResourcesPanel2_page_down");
            this.villageSelectPanel.addControl(this.villageOwnPageDown);
            this.villageTabOwnPage = 0;
            this.updateVillageHistory();
            this.sendAllowed = true;
            this.currentSelectedVillageID = -1;
            this.currentSelectedRow = -1;
            this.currentResource = -1;
            VillageMap village = GameEngine.Instance.Village;
            if ((village != null) && (village.m_parishCapitalResearchData != null))
            {
                if (village.VillageID != villageID)
                {
                    return;
                }
                this.updateScreenInfo(selectedBuilding, village, true);
                RemoteServices.Instance.set_GetVillageInfoForDonateCapitalGoods_UserCallBack(new RemoteServices.GetVillageInfoForDonateCapitalGoods_UserCallBack(this.GetVillageInfoForDonateCapitalGoods_callback));
                RemoteServices.Instance.GetVillageInfoForDonateCapitalGoods(villageID, selectedBuilding.buildingType);
            }
            this.validateSendButtons();
            this.update();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CapitalDonateResourcesPanel22";
            base.Size = new Size(0x3e0, 0x236);
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

        private void rowClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data != this.currentResource)
                {
                    this.sendTrack.Max = 0x1dcd6500;
                    this.sendTrack.Value = 0x1dcd6500;
                    GameEngine.Instance.playInterfaceSound("CapitalDonateResourcesPanel2_line_clicked");
                    this.selectHighlightLine(this.getLineFromResource(clickedControl.Data));
                    base.Invalidate();
                }
            }
        }

        private void selectHighlightLine(int line)
        {
            this.currentSelectedRow = line;
            this.highlightLine1.Image = null;
            this.highlightLine2.Image = null;
            this.highlightLine3.Image = null;
            this.highlightLine4.Image = null;
            this.highlightLine5.Image = null;
            this.highlightLine6.Image = null;
            this.highlightLine7.Image = null;
            this.highlightLine8.Image = null;
            if ((line >= 0) && (line < 8))
            {
                CustomSelfDrawPanel.CSDButton button = this.getRowButton(line);
                this.currentResource = button.Data;
                CustomSelfDrawPanel.CSDImage image = this.getRowHighlight(line);
                image.Image = (Image) GFXLibrary.int_white_highlight_bar;
                image.Size = new Size(400, 0x1f);
            }
            if ((this.currentResource >= 0) && (this.currentResource < 0x7c))
            {
                this.currentResourcePacketSize = GameEngine.Instance.LocalWorldData.traderCarryingLevels[this.currentResource];
                this.sendHeadingLabel.Text = SK.Text("CapitalDonate_Donate", "Donate") + " : " + VillageBuildingsData.getResourceNames(this.currentResource);
                this.sendHeadingImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.currentResource);
            }
            this.sendTrack.Max = 0x1dcd6500;
            this.sendTrack.Value = 0x1dcd6500;
            this.showSendWindow(true);
        }

        private void sendClick()
        {
            this.validateSendButtons();
            if (this.sendButton.Visible)
            {
                this.sendButton.Visible = false;
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - this.lastTradeTime);
                if (span.TotalSeconds >= 2.0)
                {
                    this.lastTradeTime = now;
                    this.btnDonate_Click();
                }
            }
        }

        private void setDonateRowInfo(int line, int resource)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            CustomSelfDrawPanel.CSDButton button = this.getRowButton(line);
            button.ImageIcon = (Image) GFXLibrary.getCommodity32DSImage(resource);
            button.Text.Text = VillageBuildingsData.getResourceNames(resource);
            button.Data = resource;
        }

        private void setRowInfo(int line, int resource)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            CustomSelfDrawPanel.CSDButton button = this.getRowButton(line);
            button.ImageIcon = (Image) GFXLibrary.getCommodity32DSImage(resource);
            button.Text.Text = VillageBuildingsData.getResourceNames(resource);
            button.Data = resource;
        }

        private void showSendWindow(bool autoSetLevel)
        {
            if (!this.sendAllowed)
            {
                this.sendWindow.Visible = false;
            }
            else if ((this.currentSelectedVillageID < 0) || (this.currentSelectedRow < 0))
            {
                this.sendWindow.Visible = false;
            }
            else
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                this.sendWindow.Visible = true;
                foreach (VillageDonateInfo info2 in this.villageInfo)
                {
                    if (info2.villageID != this.currentSelectedVillageID)
                    {
                        continue;
                    }
                    if (this.sendTrack.Value >= 0x1dcd6500)
                    {
                        this.sendTrack.Value = 0;
                        int num = 0;
                        switch (this.currentSelectedRow)
                        {
                            case 0:
                                num = info2.resourceLevel1;
                                break;

                            case 1:
                                num = info2.resourceLevel2;
                                break;

                            case 2:
                                num = info2.resourceLevel3;
                                break;

                            case 3:
                                num = info2.resourceLevel4;
                                break;

                            case 4:
                                num = info2.resourceLevel5;
                                break;

                            case 5:
                                num = info2.resourceLevel6;
                                break;

                            case 6:
                                num = info2.resourceLevel7;
                                break;

                            case 7:
                                num = info2.resourceLevel8;
                                break;
                        }
                        if (num < 0)
                        {
                            num = 0;
                        }
                        this.sendTrack.Max = num;
                        if ((autoSetLevel && (this.currentSelectedRow >= 0)) && (this.currentSelectedRow < 8))
                        {
                            int num2 = this.currentLevelsNeeded[this.currentSelectedRow, 0];
                            int num3 = this.currentLevelsNeeded[this.currentSelectedRow, 1];
                            if (num2 < num3)
                            {
                                int num4 = num3 - num2;
                                if (num >= num4)
                                {
                                    this.sendTrack.Value = num4;
                                }
                                else
                                {
                                    this.sendTrack.Value = num;
                                }
                            }
                        }
                    }
                    this.sendMax.Text = this.sendTrack.Max.ToString("N", nFI);
                    this.sendNumber.Text = this.sendTrack.Value.ToString("N", nFI);
                    this.sendNumberPackets.Text = SK.Text("DonateScreen_Packets", "Packets") + " : " + ((this.sendTrack.Value / this.currentResourcePacketSize)).ToString("N", nFI);
                    break;
                }
                this.validateSendButtons();
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

        private void tracksMoved()
        {
            this.showSendWindow(false);
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
        }

        private void updateLocalValues(int villageid)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            VillageDonateInfo info2 = null;
            foreach (VillageDonateInfo info3 in this.villageInfo)
            {
                if (info3.villageID == villageid)
                {
                    info2 = info3;
                    break;
                }
            }
            if (info2 != null)
            {
                this.localLabel1.Text = info2.resourceLevel1.ToString("N", nFI);
                this.localLabel2.Text = info2.resourceLevel2.ToString("N", nFI);
                this.localLabel3.Text = info2.resourceLevel3.ToString("N", nFI);
                this.localLabel4.Text = info2.resourceLevel4.ToString("N", nFI);
                this.localLabel5.Text = info2.resourceLevel5.ToString("N", nFI);
                this.localLabel6.Text = info2.resourceLevel6.ToString("N", nFI);
                this.localLabel7.Text = info2.resourceLevel7.ToString("N", nFI);
                this.localLabel8.Text = info2.resourceLevel8.ToString("N", nFI);
                this.currentSelectedVillageID = info2.villageID;
                this.exchangeNameLabel.Text = GameEngine.Instance.World.getVillageName(info2.villageID);
                this.sendTrack.Max = 0x1dcd6500;
                this.sendTrack.Value = 0x1dcd6500;
                this.showSendWindow(false);
            }
        }

        public void updateScreenInfo(VillageMapBuilding selectedBuilding, VillageMap vm, bool resetRadios)
        {
            int num6;
            int num8;
            if ((vm == null) || (selectedBuilding == null))
            {
                return;
            }
            this.lblNextLevelEffect.Text = "";
            this.lblCurrentLevelEffect.Text = "";
            NumberFormatInfo nFI = GameEngine.NFI;
            int index = 0;
            index = vm.m_parishCapitalResearchData.getCapitalResourceFromBuildingType(selectedBuilding.buildingType);
            capitalTooltipText = VillageBuildingsData.getCapitalBuildingHelpText(selectedBuilding.buildingType);
            int researchType = ResearchData.getCapitalResearchFromBuildingType(selectedBuilding.buildingType);
            int numLevels = ResearchData.getNumLevels(researchType);
            switch (researchType)
            {
                case 0x31:
                case 60:
                    numLevels = 10;
                    break;

                default:
                    if (researchType == 0x4a)
                    {
                        if (GameEngine.Instance.World.FifthAgeWorld)
                        {
                            numLevels = 5;
                        }
                        else if (GameEngine.Instance.World.FourthAgeWorld)
                        {
                            numLevels = 3;
                        }
                    }
                    break;
            }
            if (GameEngine.Instance.World.FourthAgeWorld)
            {
                numLevels = ResearchData.fourthAgeCapitalResearchLevels(numLevels, researchType);
            }
            else if (GameEngine.Instance.World.ThirdAgeWorld)
            {
                numLevels = ResearchData.thirdAgeCapitalResearchLevels(numLevels, researchType);
            }
            if (index >= numLevels)
            {
                this.sendAllowed = false;
                this.storedHeadingLabel.Visible = false;
            }
            else
            {
                this.storedHeadingLabel.Visible = true;
            }
            this.storedHeadingLabel.Text = SK.Text("DonateScreen_For_Level", "For Level") + " : " + ((index + 1)).ToString();
            this.sendAllowed = true;
            BaseImage image = GFXLibrary.townbuilding_archeryrange_normal;
            switch (selectedBuilding.buildingType)
            {
                case 0x4f:
                    image = GFXLibrary.townbuilding_Woodcutter_normal;
                    break;

                case 80:
                    image = GFXLibrary.townbuilding_stonequarry_normal;
                    break;

                case 0x51:
                    image = GFXLibrary.townbuilding_iron_normal;
                    break;

                case 0x52:
                    image = GFXLibrary.townbuilding_pitch_normal;
                    break;

                case 0x53:
                    image = GFXLibrary.townbuilding_ale_normal;
                    break;

                case 0x54:
                    image = GFXLibrary.townbuilding_apples_normal;
                    break;

                case 0x55:
                    image = GFXLibrary.townbuilding_cheese_normal;
                    break;

                case 0x56:
                    image = GFXLibrary.townbuilding_meat_normal;
                    break;

                case 0x57:
                    image = GFXLibrary.townbuilding_bread_normal;
                    break;

                case 0x58:
                    image = GFXLibrary.townbuilding_veg_normal;
                    break;

                case 0x59:
                    image = GFXLibrary.townbuilding_fish_normal;
                    break;

                case 90:
                    image = GFXLibrary.townbuilding_bows_normal;
                    break;

                case 0x5b:
                    image = GFXLibrary.townbuilding_pikes_normal;
                    break;

                case 0x5c:
                    image = GFXLibrary.townbuilding_armour_normal;
                    break;

                case 0x5d:
                    image = GFXLibrary.townbuilding_sword_normal;
                    break;

                case 0x5e:
                    image = GFXLibrary.townbuilding_catapults_normal;
                    break;

                case 0x5f:
                    image = GFXLibrary.townbuilding_venison_normal;
                    break;

                case 0x60:
                    image = GFXLibrary.townbuilding_wine_normal;
                    break;

                case 0x61:
                    image = GFXLibrary.townbuilding_salt_normal;
                    break;

                case 0x62:
                    image = GFXLibrary.townbuilding_carpenter_normal;
                    break;

                case 0x63:
                    image = GFXLibrary.townbuilding_tailor_normal;
                    break;

                case 100:
                    image = GFXLibrary.townbuilding_metalware_normal;
                    break;

                case 0x65:
                    image = GFXLibrary.townbuilding_spice_normal;
                    break;

                case 0x66:
                    image = GFXLibrary.townbuilding_silk_normal;
                    break;

                case 0x67:
                    image = GFXLibrary.townbuilding_architectsguild_normal;
                    break;

                case 0x68:
                    image = GFXLibrary.townbuilding_Labourersbillets_normal;
                    break;

                case 0x69:
                    image = GFXLibrary.townbuilding_castellanshouse_normal;
                    break;

                case 0x6a:
                    image = GFXLibrary.townbuilding_sergeantsatarmsoffice_normal;
                    break;

                case 0x6b:
                    image = GFXLibrary.townbuilding_stables_normal;
                    break;

                case 0x6c:
                    image = GFXLibrary.townbuilding_barracks_normal;
                    break;

                case 0x6d:
                    image = GFXLibrary.townbuilding_peasntshall_normal;
                    break;

                case 110:
                    image = GFXLibrary.townbuilding_archeryrange_normal;
                    break;

                case 0x6f:
                    image = GFXLibrary.townbuilding_pikemandrillyard_normal;
                    break;

                case 0x70:
                    image = GFXLibrary.townbuilding_combatarena_normal;
                    break;

                case 0x71:
                    image = GFXLibrary.townbuilding_siegeengineersguild_normal;
                    break;

                case 0x72:
                    image = GFXLibrary.townbuilding_officersquarters_normal;
                    break;

                case 0x73:
                    image = GFXLibrary.townbuilding_militaryschool_normal;
                    break;

                case 0x74:
                    image = GFXLibrary.townbuilding_supplydepot_normal;
                    break;

                case 0x75:
                    image = GFXLibrary.townbuilding_townhall_normal;
                    break;

                case 0x76:
                    image = GFXLibrary.townbuilding_church_normal;
                    break;

                case 0x77:
                    image = GFXLibrary.townbuilding_towngarden_normal;
                    break;

                case 120:
                    image = GFXLibrary.townbuilding_statue_normal;
                    break;

                case 0x79:
                    image = GFXLibrary.townbuilding_turretmaker_normal;
                    break;

                case 0x7a:
                    image = GFXLibrary.townbuilding_tunnellorsguild_normal;
                    break;

                case 0x7b:
                    image = GFXLibrary.townbuilding_ballistamaker_normal;
                    break;
            }
            this.buildingImage.Image = (Image) image;
            this.buildingTypeName.Text = VillageBuildingsData.getBuildingName(selectedBuilding.buildingType);
            this.currentLevelName.Text = SK.Text("DonateScreen_Current_Level", "Current Level") + " : " + index.ToString();
            switch (selectedBuilding.buildingType)
            {
                case 0x4f:
                case 80:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x59:
                case 90:
                case 0x5b:
                case 0x5c:
                case 0x5d:
                case 0x5e:
                case 0x5f:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 100:
                case 0x65:
                case 0x66:
                    if ((index < ResearchData.ParishResearchIncreases_Guilds.Length) && (index < numLevels))
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = "+" + ((int) (((ResearchData.ParishResearchIncreases_Guilds[index + 1] - 1.0) + 9.9999997473787516E-06) * 100.0)).ToString() + "%";
                    }
                    if (index > 0)
                    {
                        if (index >= ResearchData.ParishResearchIncreases_Guilds.Length)
                        {
                            index = ResearchData.ParishResearchIncreases_Guilds.Length - 1;
                        }
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = "+" + ((int) (((ResearchData.ParishResearchIncreases_Guilds[index] - 1.0) + 9.9999997473787516E-06) * 100.0)).ToString() + "%";
                    }
                    goto Label_1731;

                case 0x67:
                    if (index < 8)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        switch ((index + 1))
                        {
                            case 1:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Wood_Walls", "Allows access to Wooden Walls and Wooden Gate Houses");
                                goto Label_0738;

                            case 2:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Wooden_Platforms", "Allows access to Wooden Platforms");
                                goto Label_0738;

                            case 3:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Stone_Walls", "Allows access to Stone Walls");
                                goto Label_0738;

                            case 4:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Lookout_Tower", "Allows access to Lookout Tower");
                                goto Label_0738;

                            case 5:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Small_Tower", "Allows access to Small Tower");
                                goto Label_0738;

                            case 6:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Gate_House", "Allows access to Gate House");
                                goto Label_0738;

                            case 7:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Large_Tower", "Allows access to Large Tower");
                                goto Label_0738;

                            case 8:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Great_Tower", "Allows access to Great Tower");
                                goto Label_0738;
                        }
                    }
                    break;

                case 0x69:
                    if (index < 10)
                    {
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Keep_Level", "Keep Level") + " : " + ((index + 1)).ToString();
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                    }
                    if (index > 0)
                    {
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Keep_Level", "Keep Level") + " : " + index.ToString();
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                    }
                    goto Label_1731;

                case 0x6a:
                    if (index < 10)
                    {
                        switch ((index + 1))
                        {
                            case 1:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Guard_Houses", "Access to Guard Houses.") + " ";
                                break;

                            case 3:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Killing_Pits", "Access to Killing Pits.") + " ";
                                break;

                            case 4:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Upgrade_Guard_Houses_Wood", "Upgrade Guard Houses to Wood.") + " ";
                                break;

                            case 5:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Smelters", "Access to Smelters and Oil Pots.") + " ";
                                break;

                            case 7:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Moats", "Access to Moats.") + " ";
                                break;

                            case 8:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Upgrade_Guard_Houses_Stone", "Upgrade Guard Houses to Stone.") + " ";
                                break;

                            case 10:
                                this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Upgrade_Guard_Houses_Iron", "Upgrade Guard Houses with Iron Hoardings.") + " ";
                                break;
                        }
                        this.lblNextLevelEffect.Text = this.lblNextLevelEffect.Text + SK.Text("CapitalDonateResourcesPanel_Boost_To_Armour", "Boost to armour") + " : ";
                        double num4 = ResearchData.defencesResearch[index + 1];
                        num4 = (1.0 - num4) * 100.0;
                        this.lblNextLevelEffect.Text = this.lblNextLevelEffect.Text + ((int) num4).ToString() + "%";
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                    }
                    if (index > 0)
                    {
                        switch (index)
                        {
                            case 1:
                                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Guard_Houses", "Access to Guard Houses.") + " ";
                                break;

                            case 3:
                                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Killing_Pits", "Access to Killing Pits.") + " ";
                                break;

                            case 4:
                                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Upgrade_Guard_Houses_Wood", "Upgrade Guard Houses to Wood.") + " ";
                                break;

                            case 5:
                                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Smelters", "Access to Smelters and Oil Pots.") + " ";
                                break;

                            case 7:
                                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Access_Moats", "Access to Moats.") + " ";
                                break;

                            case 8:
                                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Upgrade_Guard_Houses_Stone", "Upgrade Guard Houses to Stone.") + " ";
                                break;

                            case 10:
                                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Upgrade_Guard_Houses_Iron", "Upgrade Guard Houses with Iron Hoardings.") + " ";
                                break;
                        }
                        this.lblCurrentLevelEffect.Text = this.lblCurrentLevelEffect.Text + SK.Text("CapitalDonateResourcesPanel_Boost_To_Armour", "Boost to armour") + " : ";
                        double num5 = ResearchData.defencesResearch[index];
                        num5 = (1.0 - num5) * 100.0;
                        this.lblCurrentLevelEffect.Text = this.lblCurrentLevelEffect.Text + ((int) num5).ToString() + "%";
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                    }
                    goto Label_1731;

                case 0x6b:
                    if (index < 10)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Knights", "Knights") + " : " + ((index + 1)).ToString();
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Knights", "Knights") + " : " + index.ToString();
                    }
                    goto Label_1731;

                case 0x6d:
                    if ((index < 10) && (index > 1))
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = "+" + ((int) (((ResearchData.conscriptionBonus[index + 1] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = "+" + ((int) (((ResearchData.conscriptionBonus[index] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    goto Label_1731;

                case 110:
                    if ((index < 10) && (index > 1))
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = "+" + ((int) (((ResearchData.longBowBonus[index + 1] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = "+" + ((int) (((ResearchData.longBowBonus[index] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    goto Label_1731;

                case 0x6f:
                    if ((index < 10) && (index > 1))
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = "+" + ((int) (((ResearchData.pikeBonus[index + 1] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = "+" + ((int) (((ResearchData.pikeBonus[index] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    goto Label_1731;

                case 0x70:
                    if ((index < 10) && (index > 1))
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = "+" + ((int) (((ResearchData.swordBonus[index + 1] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = "+" + ((int) (((ResearchData.swordBonus[index] - 1f) + 1E-05f) * 100f)).ToString() + "%";
                    }
                    goto Label_1731;

                case 0x71:
                    if ((index < 10) && (index > 1))
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = "+" + ((int) (((1.0 - ResearchData.catapultFireRate[index + 1]) + 9.9999997473787516E-06) * 100.0)).ToString() + "%";
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = "+" + ((int) (((1.0 - ResearchData.catapultFireRate[index]) + 9.9999997473787516E-06) * 100.0)).ToString() + "%";
                    }
                    goto Label_1731;

                case 0x73:
                    if (index < 1)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_MilitarySchool", "Bombards") + " : " + ((index + 1)).ToString();
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_MilitarySchool", "Bombards") + " : " + index.ToString();
                    }
                    goto Label_1731;

                case 0x75:
                {
                    if (index >= 10)
                    {
                        goto Label_126F;
                    }
                    num6 = 4;
                    int num7 = vm.numCapitalBuildings();
                    switch (num7)
                    {
                        case 0:
                            num6 = 4;
                            goto Label_11EC;

                        case 1:
                            num6 = 6;
                            goto Label_11EC;

                        case 2:
                            num6 = 8;
                            goto Label_11EC;

                        case 3:
                            num6 = 10;
                            goto Label_11EC;

                        case 4:
                            num6 = 12;
                            goto Label_11EC;

                        case 5:
                            num6 = 14;
                            goto Label_11EC;

                        case 6:
                            num6 = 0x10;
                            goto Label_11EC;

                        case 7:
                            num6 = 0x12;
                            goto Label_11EC;

                        case 8:
                            num6 = 20;
                            goto Label_11EC;

                        case 9:
                            num6 = 0x16;
                            goto Label_11EC;

                        case 10:
                            num6 = 0x18;
                            goto Label_11EC;
                    }
                    num6 = 0x18 + (num7 - 10);
                    goto Label_11EC;
                }
                case 0x76:
                    if (index < numLevels)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Faith_Points_Per_Day", "Faith Points Per Day") + " : " + (((index + 1) * 6)).ToString();
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Faith_Points_Per_Day", "Faith Points Per Day") + " : " + ((index * 6)).ToString();
                    }
                    goto Label_1731;

                case 0x77:
                    if (index < numLevels)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Popularity_Honour_Multiplier", "Popularity-Honour Multiplier") + " : +" + ((index + 1)).ToString();
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Popularity_Honour_Multiplier", "Popularity-Honour Multiplier") + " : +" + index.ToString();
                    }
                    goto Label_1731;

                case 0x79:
                    if (index < numLevels)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Turrets", "Turrets") + " : " + ((index + 1)).ToString();
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Turrets", "Turrets") + " : " + index.ToString();
                    }
                    goto Label_1731;

                case 0x7a:
                    if (index < numLevels)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Tunneller_Peasants", "Tunneller Peasants") + " : " + (((index + 1) * 5)).ToString();
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Tunneller_Peasants", "Tunneller Peasants") + " : " + ((index * 5)).ToString();
                    }
                    goto Label_1731;

                case 0x7b:
                    if (index < numLevels)
                    {
                        this.lblNextLevelEffectLabel.Visible = true;
                        this.lblNextLevelEffect.Visible = true;
                        this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Ballista_Towers", "Ballista Towers") + " : " + ((index + 1)).ToString();
                    }
                    if (index > 0)
                    {
                        this.lblCurrentEffectLevelLabel.Visible = true;
                        this.lblCurrentLevelEffect.Visible = true;
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Ballista_Towers", "Ballista Towers") + " : " + index.ToString();
                    }
                    goto Label_1731;

                default:
                    goto Label_1731;
            }
        Label_0738:
            if (index > 0)
            {
                this.lblCurrentEffectLevelLabel.Visible = true;
                this.lblCurrentLevelEffect.Visible = true;
                switch (index)
                {
                    case 1:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Wood_Walls", "Allows access to Wooden Walls and Wooden Gate Houses");
                        break;

                    case 2:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Wooden_Platforms", "Allows access to Wooden Platforms");
                        break;

                    case 3:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Stone_Walls", "Allows access to Stone Walls");
                        break;

                    case 4:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Lookout_Tower", "Allows access to Lookout Tower");
                        break;

                    case 5:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Small_Tower", "Allows access to Small Tower");
                        break;

                    case 6:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Gate_House", "Allows access to Gate House");
                        break;

                    case 7:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Large_Tower", "Allows access to Large Tower");
                        break;

                    case 8:
                        this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Allows_Great_Tower", "Allows access to Great Tower");
                        break;
                }
            }
            goto Label_1731;
        Label_11EC:
            num8 = num6 * 60;
            num8 = (int) (num8 * ResearchData.ParishTownHallIncreases_Guilds[index + 1]);
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
            {
                num8 /= 4;
            }
            TimeSpan span = new TimeSpan(0, num8, 0);
            this.lblNextLevelEffectLabel.Visible = true;
            this.lblNextLevelEffect.Visible = true;
            this.lblNextLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Construction_Time", "Construction Time") + " : " + VillageMap.createBuildTimeString((int) span.TotalSeconds);
        Label_126F:
            if (index > 0)
            {
                int num9 = 4;
                int num10 = vm.numCapitalBuildings();
                switch (num10)
                {
                    case 0:
                        num9 = 4;
                        break;

                    case 1:
                        num9 = 6;
                        break;

                    case 2:
                        num9 = 8;
                        break;

                    case 3:
                        num9 = 10;
                        break;

                    case 4:
                        num9 = 12;
                        break;

                    case 5:
                        num9 = 14;
                        break;

                    case 6:
                        num9 = 0x10;
                        break;

                    case 7:
                        num9 = 0x12;
                        break;

                    case 8:
                        num9 = 20;
                        break;

                    case 9:
                        num9 = 0x16;
                        break;

                    case 10:
                        num9 = 0x18;
                        break;

                    default:
                        num9 = 0x18 + (num10 - 10);
                        break;
                }
                int minutes = num9 * 60;
                minutes = (int) (minutes * ResearchData.ParishTownHallIncreases_Guilds[index]);
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                {
                    minutes /= 4;
                }
                TimeSpan span2 = new TimeSpan(0, minutes, 0);
                this.lblCurrentEffectLevelLabel.Visible = true;
                this.lblCurrentLevelEffect.Visible = true;
                this.lblCurrentLevelEffect.Text = SK.Text("CapitalDonateResourcesPanel_Construction_Time", "Construction Time") + " : " + VillageMap.createBuildTimeString((int) span2.TotalSeconds);
            }
        Label_1731:
            this.highlightLine1.Visible = false;
            this.highlightLine2.Visible = false;
            this.highlightLine3.Visible = false;
            this.highlightLine4.Visible = false;
            this.highlightLine5.Visible = false;
            this.highlightLine6.Visible = false;
            this.highlightLine7.Visible = false;
            this.highlightLine8.Visible = false;
            this.highlightLine1.Image = null;
            this.highlightLine2.Image = null;
            this.highlightLine3.Image = null;
            this.highlightLine4.Image = null;
            this.highlightLine5.Image = null;
            this.highlightLine6.Image = null;
            this.highlightLine7.Image = null;
            this.highlightLine8.Image = null;
            int resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 0);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 0))
            {
                int num13 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 0, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num13 > 0)
                {
                    this.highlightLine1.Visible = true;
                    this.storedLabel1.Text = selectedBuilding.capitalResourceLevels[0].ToString("N", nFI) + " / ";
                    this.priceLabel1.Text = num13.ToString("N", nFI);
                    this.setDonateRowInfo(0, resource);
                    this.currentLevelsNeeded[0, 0] = selectedBuilding.capitalResourceLevels[0];
                    this.currentLevelsNeeded[0, 1] = num13;
                }
            }
            resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 1);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 1))
            {
                int num14 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 1, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num14 > 0)
                {
                    this.highlightLine2.Visible = true;
                    this.storedLabel2.Text = selectedBuilding.capitalResourceLevels[1].ToString("N", nFI) + " / ";
                    this.priceLabel2.Text = num14.ToString("N", nFI);
                    this.setDonateRowInfo(1, resource);
                    this.currentLevelsNeeded[1, 0] = selectedBuilding.capitalResourceLevels[1];
                    this.currentLevelsNeeded[1, 1] = num14;
                }
            }
            resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 2);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 2))
            {
                int num15 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 2, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num15 > 0)
                {
                    this.highlightLine3.Visible = true;
                    this.storedLabel3.Text = selectedBuilding.capitalResourceLevels[2].ToString("N", nFI) + " / ";
                    this.priceLabel3.Text = num15.ToString("N", nFI);
                    this.setDonateRowInfo(2, resource);
                    this.currentLevelsNeeded[2, 0] = selectedBuilding.capitalResourceLevels[2];
                    this.currentLevelsNeeded[2, 1] = num15;
                }
            }
            resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 3);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 3))
            {
                int num16 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 3, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num16 > 0)
                {
                    this.highlightLine4.Visible = true;
                    this.storedLabel4.Text = selectedBuilding.capitalResourceLevels[3].ToString("N", nFI) + " / ";
                    this.priceLabel4.Text = num16.ToString("N", nFI);
                    this.setDonateRowInfo(3, resource);
                    this.currentLevelsNeeded[3, 0] = selectedBuilding.capitalResourceLevels[3];
                    this.currentLevelsNeeded[3, 1] = num16;
                }
            }
            resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 4);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 4))
            {
                int num17 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 4, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num17 > 0)
                {
                    this.highlightLine5.Visible = true;
                    this.storedLabel5.Text = selectedBuilding.capitalResourceLevels[4].ToString("N", nFI) + " / ";
                    this.priceLabel5.Text = num17.ToString("N", nFI);
                    this.setDonateRowInfo(4, resource);
                    this.currentLevelsNeeded[4, 0] = selectedBuilding.capitalResourceLevels[4];
                    this.currentLevelsNeeded[4, 1] = num17;
                }
            }
            resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 5);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 5))
            {
                int num18 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 5, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num18 > 0)
                {
                    this.highlightLine6.Visible = true;
                    this.storedLabel6.Text = selectedBuilding.capitalResourceLevels[5].ToString("N", nFI) + " / ";
                    this.priceLabel6.Text = num18.ToString("N", nFI);
                    this.setDonateRowInfo(5, resource);
                    this.currentLevelsNeeded[5, 0] = selectedBuilding.capitalResourceLevels[5];
                    this.currentLevelsNeeded[5, 1] = num18;
                }
            }
            resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 6);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 6))
            {
                int num19 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 6, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num19 > 0)
                {
                    this.highlightLine7.Visible = true;
                    this.storedLabel7.Text = selectedBuilding.capitalResourceLevels[6].ToString("N", nFI) + " / ";
                    this.priceLabel7.Text = num19.ToString("N", nFI);
                    this.setDonateRowInfo(6, resource);
                    this.currentLevelsNeeded[6, 0] = selectedBuilding.capitalResourceLevels[6];
                    this.currentLevelsNeeded[6, 1] = num19;
                }
            }
            resource = VillageBuildingsData.getRequiredResourceType(selectedBuilding.buildingType, 7);
            if ((resource >= 0) && (selectedBuilding.capitalResourceLevels.Length > 7))
            {
                int num20 = VillageBuildingsData.getRequiredResourceTypeLevel(selectedBuilding.buildingType, 7, index, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                if (num20 > 0)
                {
                    this.highlightLine8.Visible = true;
                    this.storedLabel8.Text = selectedBuilding.capitalResourceLevels[7].ToString("N", nFI) + " / ";
                    this.priceLabel8.Text = num20.ToString("N", nFI);
                    this.setDonateRowInfo(7, resource);
                    this.currentLevelsNeeded[7, 0] = selectedBuilding.capitalResourceLevels[7];
                    this.currentLevelsNeeded[7, 1] = num20;
                }
            }
        }

        public void updateVillageHistory()
        {
            for (int i = 0; i < 0x11; i++)
            {
                this.getVillageHistory(i).Visible = false;
            }
            if (this.villageInfo != null)
            {
                int line = 0;
                int num3 = this.villageTabOwnPage * 0x10;
                while ((line < 0x10) && (num3 < this.villageInfo.Count))
                {
                    if (num3 < this.villageInfo.Count)
                    {
                        VillageDonateInfo info = this.villageInfo[num3];
                        CustomSelfDrawPanel.CSDButton button2 = this.getVillageHistory(line);
                        button2.Visible = true;
                        button2.Text.Text = GameEngine.Instance.World.getVillageName(info.villageID);
                        button2.Data = info.villageID;
                    }
                    num3++;
                    line++;
                }
                if (this.villageInfo.Count > 0x10)
                {
                    this.villageOwnPageDown.Visible = true;
                    this.villageOwnPageUp.Visible = true;
                    if (this.villageTabOwnPage == 0)
                    {
                        this.villageOwnPageUp.Visible = false;
                    }
                    else if (this.villageTabOwnPage >= ((this.villageInfo.Count - 1) / 0x10))
                    {
                        this.villageOwnPageDown.Visible = false;
                    }
                }
                else
                {
                    this.villageOwnPageDown.Visible = false;
                    this.villageOwnPageUp.Visible = false;
                }
            }
        }

        private void updateVillageView(int selectedVillageID)
        {
            if (this.villageInfo != null)
            {
                this.updateLocalValues(selectedVillageID);
            }
            this.validateSendButtons();
        }

        private void validateSendButtons()
        {
            bool flag = false;
            if (((this.currentSelectedVillageID >= 0) && (this.currentSelectedRow >= 0)) && (this.sendTrack.Value > 0))
            {
                flag = true;
            }
            this.sendButton.Visible = flag;
        }

        private void villageClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                GameEngine.Instance.playInterfaceSound("CapitalDonateResourcesPanel2_village_clicked");
                this.updateLocalValues(clickedControl.Data);
                this.showVillagePanel(false);
                if (this.currentSelectedRow >= 0)
                {
                    this.selectHighlightLine(this.currentSelectedRow);
                    this.sendWindow.invalidate();
                }
                this.validateSendButtons();
            }
        }
    }
}

