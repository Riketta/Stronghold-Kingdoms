namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using StatTracking;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class VillageMapPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDArea aleExtensionArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton aleHigherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton aleLowerButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel alePopLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDColorBar aleRationsBar = new CustomSelfDrawPanel.CSDColorBar();
        private CustomSelfDrawPanel.CSDLabel aleRationsDay2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel aleRationsDay2ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel aleRationsDayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel aleRationsDayValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel aleRationsLine1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel arrivesInLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel arrivesInTimeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton buildCapitalHelp = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage buildDonationTypeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage buildGoldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel buildGoldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea buildHeaderArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage buildInfoImage = new CustomSelfDrawPanel.CSDImage();
        private const int BUILDING_WINDOW_SIZE = 0x1a6;
        private CustomSelfDrawPanel.CSDButton building1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton building2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton building3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton building4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton building5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton building6Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building6Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton building7Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building7Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building7Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton building8Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage building8Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building8Label = new CustomSelfDrawPanel.CSDLabel();
        private bool buildingBeingPlaced;
        private CustomSelfDrawPanel.CSDArea buildingExtensionArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel buildingPopLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel buildingsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage buildPanelFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage buildPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage buildStoneImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel buildStoneLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton buildTab1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton buildTab2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton buildTab3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton buildTab4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton buildTab5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage buildTimeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel buildTimeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea buildTooltipArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel buildTypeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage buildWoodImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel buildWoodLabel = new CustomSelfDrawPanel.CSDLabel();
        private const int CAPITAL_GIVERS_WINDOW_SIZE = 0x14f;
        private CustomSelfDrawPanel.CSDLabel capitalLastTaxRateLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalLastTaxRateValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalOutgoingPerDayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalOutgoingPerDayValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage capitalPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDColorBar capitalTaxBar = new CustomSelfDrawPanel.CSDColorBar();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver10Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver10ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver1ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver2ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver3ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver4ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver5ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver6Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver6ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver7Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver7ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver8Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver8ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver9Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiver9ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiverOthersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxGiverOthersValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton capitalTaxHigherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel capitalTaxLine1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton capitalTaxLowerButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel capitalTaxPerDayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxPerDayValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel capitalTaxTopGivers = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage capitalTop10HeaderGlowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage capitalTop10HeaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage capitalTop10PanelFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage capitalTop10PanelImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private int currentBuildingHeight;
        private int currentBuildingIcon;
        private int currentEventID;
        private int currentExtensionHeight;
        private int currentHeight;
        private int currentInBuildingHeight;
        private int currentInfo1Height;
        private int currentSelectedBuildingType = -1;
        private int currentTab = -1;
        private int currentTopGiversHeight;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDImage eventBarImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel eventCountLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eventDaysLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eventExtPopLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eventHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton eventHigherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton eventLowerButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage eventPopImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea eventsExtensionArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel eventsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eventsPopLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eventTimeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel eventTitleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage eventTypeImage = new CustomSelfDrawPanel.CSDImage();
        private const int EXTENSION_WINDOW_OPEN_POS = 0x7a;
        private const int EXTENSION_WINDOW_SIZE = 0x90;
        private CustomSelfDrawPanel.CSDLabel extensionHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage extensionImage = new CustomSelfDrawPanel.CSDImage();
        private int extensionType;
        private CustomSelfDrawPanel.CSDImage extrasHeaderPanelGlowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage extrasHeaderPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel foodTypesEatenLabel = new CustomSelfDrawPanel.CSDLabel();
        private int glowFade;
        private CustomSelfDrawPanel.CSDImage headerGlowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage headerImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel housingCapacityLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel housingCapacityValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea housingExtensionArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel housingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel housingOccupancyLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel housingOccupancyValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel housingPopLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage immChangeImage = new CustomSelfDrawPanel.CSDImage();
        private const int INBUILDING_CAPITAL_WINDOW_SIZE = 290;
        private const int INBUILDING_WINDOW_SIZE = 0x14f;
        private CustomSelfDrawPanel.CSDButton inBuildingAllIndustryOnButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingCapitalResourceImage8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel1a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel1b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel1c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel2a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel2b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel2c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel3a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel3b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel3c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel4a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel4b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel4c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel5a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel5b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel5c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel6a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel6b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel6c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel7a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel7b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel7c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel8a = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel8b = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLabel8c = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLevelLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCapitalResourceLevelLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCompleteLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingCompleteLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton inBuildingDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton inBuildingDonateButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage inBuildingFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton inBuildingGenericButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton inBuildingGenericButton2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage inBuildingHeaderPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton inBuildingHelpButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton inBuildingIndustryAllOnButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton inBuildingIndustryThisOnButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage inBuildingMakeWeaponImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingMakeWeaponImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingMakeWeaponImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingMakeWeaponImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel inBuildingMakeWeaponLabel0 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingMakeWeaponLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingMakeWeaponLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingMakeWeaponLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingMakeWeaponLabel4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingMakeWeaponLabel5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel inBuildingMakeWeaponLabel6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton inBuildingMoveButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel inBuildingName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage inBuildingPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage inBuildingTypeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage indent1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage indent2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage indent3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage indent4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage indent5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage indent6Image = new CustomSelfDrawPanel.CSDImage();
        private const int INFO1_WINDOW_SIZE = 0x14f;
        private CustomSelfDrawPanel.CSDLabel info1ArtsAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1ArtsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1BanquetDate = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1BlackLine1aLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1BlackLine1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1BlackLine2aLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1BlackLine2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1CardsAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1CardsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1ChurchAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1ChurchLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1DecorativeAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1DecorativeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1HeaderHonourAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage info1HeaderHonourImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage info1HeaderPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel info1HonourCalc = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1HonourCalc2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1HonourPerDayAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1HonourPerDayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1JusticeAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1JusticeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage info1PanelFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage info1PanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel info1ParishAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1ParishLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage info1PopImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel info1PopularityAmount = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel info1PopularityLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage info2HeaderPanelGlowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage info2HeaderPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage info3HeaderPanelGlowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage info3HeaderPanelImage = new CustomSelfDrawPanel.CSDImage();
        private DateTime lastTabScroll = DateTime.MinValue;
        private bool m_villageIsCapital;
        private CustomSelfDrawPanel.CSDLabel negativeBuildingsHeader = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel negativeBuildingsLabel = new CustomSelfDrawPanel.CSDLabel();
        private int nextExtensionType;
        private CustomSelfDrawPanel.CSDLabel numFoodTypesEatenLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage panelFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage panelImage = new CustomSelfDrawPanel.CSDImage();
        private ParishTaxComparerNegative parishTaxComparerNegative = new ParishTaxComparerNegative();
        private ParishTaxComparerPositive parishTaxComparerPositive = new ParishTaxComparerPositive();
        private CustomSelfDrawPanel.CSDLabel parishTaxDayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel parishTaxDayValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage popImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popImage5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popImage6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popIndent1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popIndent2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popIndent3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popIndent4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popIndent5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage popIndent6Image = new CustomSelfDrawPanel.CSDImage();
        private const int POPULARITY_WINDOW_SIZE = 0x14f;
        private CustomSelfDrawPanel.CSDLabel popularityLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel populationLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel populationValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel positiveBuildingsHeader = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel positiveBuildingsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDColorBar rationsBar = new CustomSelfDrawPanel.CSDColorBar();
        private CustomSelfDrawPanel.CSDLabel rationsDay2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rationsDay2ValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rationsDayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rationsDayValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea rationsExtensionArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton rationsHigherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel rationsLine1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton rationsLowerButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel rationsPopLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool reopenExtension;
        private VillageMapBuilding selectedBuilding;
        public const int SUBMENU_BACK = 0x7d0;
        public const int SUBMENU_CAPITAL_BANQUET = 0x45a;
        public const int SUBMENU_CAPITAL_BANQUET_FOOD = 0x45c;
        public const int SUBMENU_CAPITAL_FOOD = 0x459;
        public const int SUBMENU_CAPITAL_RESOURCE = 0x458;
        public const int SUBMENU_CAPITAL_WEAPONS = 0x45b;
        public const int SUBMENU_DECORATIVE = 0x3e9;
        public const int SUBMENU_ENTERTAINMENT = 0x3eb;
        public const int SUBMENU_JUSTICE = 0x3ea;
        public const int SUBMENU_LARGE_GARDEN = 0x3f0;
        public const int SUBMENU_LARGE_SHRINE = 0x3ed;
        public const int SUBMENU_LARGE_STATUE = 0x457;
        public const int SUBMENU_MEDIUM_GARDEN = 0x3ef;
        public const int SUBMENU_RELIGIOUS = 0x3e8;
        public const int SUBMENU_SMALL_GARDEN = 0x3ee;
        public const int SUBMENU_SMALL_SHRINE = 0x3ec;
        public const int SUBMENU_SMALL_STATUE = 0x3f2;
        public const int SUBMENU_WATER_GARDEN = 0x3f1;
        private int targetBuildingHeight;
        private int targetExtensionHeight;
        private int targetHeight;
        private int targetInBuildingHeight;
        private int targetInfo1Height;
        private int targetTopGiversHeight;
        private CustomSelfDrawPanel.CSDColorBar taxBar = new CustomSelfDrawPanel.CSDColorBar();
        private CustomSelfDrawPanel.CSDLabel taxDayLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel taxDayValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea taxExtensionArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel taxExtentionLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton taxHigherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel taxLine1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton taxLowerButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage taxLowerButtonGlow = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel taxPopLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool viewOnly;
        private const int WINDOWS_EXPAND_SPEED = 50;

        public VillageMapPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void addBuildingIcon(int buildingType, BaseImage newImage, BaseImage overImage)
        {
            CustomSelfDrawPanel.CSDImage image;
            CustomSelfDrawPanel.CSDLabel label;
            bool flag;
            int num2;
            int num3;
            int num4;
            if ((this.m_villageIsCapital || (buildingType == 2)) || ((GameEngine.Instance.Village == null) || (GameEngine.Instance.Village.countBuildingType(2) != 0)))
            {
                if (GameEngine.Instance.Village == null)
                {
                    return;
                }
                image = null;
                label = null;
                flag = true;
                if ((buildingType >= GameEngine.Instance.LocalWorldData.constrMaxCount.Length) || (buildingType < 0))
                {
                    goto Label_0199;
                }
                if ((buildingType == 0x73) && !GameEngine.Instance.World.FourthAgeWorld)
                {
                    flag = false;
                }
                int capitalType = GameEngine.Instance.World.getCapitalType(GameEngine.Instance.Village.VillageID);
                num2 = GameEngine.Instance.LocalWorldData.getConstrMaxCount(buildingType, capitalType);
                num3 = GameEngine.Instance.Village.countBuildingType(buildingType);
                if (num2 <= num3)
                {
                    flag = false;
                    goto Label_0199;
                }
                if ((num2 - num3) <= 1)
                {
                    goto Label_0199;
                }
                switch (this.currentBuildingIcon)
                {
                    case 0:
                        image = this.building1Image;
                        label = this.building1Label;
                        goto Label_017E;

                    case 1:
                        image = this.building2Image;
                        label = this.building2Label;
                        goto Label_017E;

                    case 2:
                        image = this.building3Image;
                        label = this.building3Label;
                        goto Label_017E;

                    case 3:
                        image = this.building4Image;
                        label = this.building4Label;
                        goto Label_017E;

                    case 4:
                        image = this.building5Image;
                        label = this.building5Label;
                        goto Label_017E;

                    case 5:
                        image = this.building6Image;
                        label = this.building6Label;
                        goto Label_017E;

                    case 6:
                        image = this.building7Image;
                        label = this.building7Label;
                        goto Label_017E;

                    case 7:
                        image = this.building8Image;
                        label = this.building8Label;
                        goto Label_017E;
                }
            }
            return;
        Label_017E:
            image.Visible = true;
            label.Text = (num2 - num3).ToString();
        Label_0199:
            num4 = GameEngine.Instance.Village.VillageMapType;
            bool flag2 = false;
            if (buildingType == 0x7d0)
            {
                flag2 = true;
                this.currentBuildingIcon = 7;
            }
            else if (buildingType >= 0x3e8)
            {
                if (this.testSubMenu(buildingType, num4))
                {
                    flag2 = true;
                }
            }
            else if ((this.m_villageIsCapital && VillageBuildingsData.isThisBuildingTypeAvailableInCapital(buildingType, num4)) || (!this.m_villageIsCapital && VillageBuildingsData.isThisBuildingTypeAvailable(buildingType, num4, GameEngine.Instance.World.UserResearchData)))
            {
                flag2 = true;
            }
            if (!flag2)
            {
                if (image != null)
                {
                    image.Visible = false;
                }
            }
            else
            {
                CustomSelfDrawPanel.CSDButton button = null;
                switch (this.currentBuildingIcon)
                {
                    case 0:
                        button = this.building1Button;
                        break;

                    case 1:
                        button = this.building2Button;
                        break;

                    case 2:
                        button = this.building3Button;
                        break;

                    case 3:
                        button = this.building4Button;
                        break;

                    case 4:
                        button = this.building5Button;
                        break;

                    case 5:
                        button = this.building6Button;
                        break;

                    case 6:
                        button = this.building7Button;
                        break;

                    case 7:
                        button = this.building8Button;
                        break;

                    default:
                        return;
                }
                button.ImageNorm = (Image) newImage;
                button.ImageOver = (Image) overImage;
                button.Visible = true;
                button.Enabled = flag;
                button.Data = buildingType;
                button.CustomTooltipID = 100;
                button.CustomTooltipData = buildingType;
                this.currentBuildingIcon++;
            }
        }

        private void aleHigherClicked()
        {
            if (!this.ViewOnly)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeStats(0, 0, 1);
                }
                if (this.isExtensionOpen())
                {
                    this.extensionType = 2;
                    this.initExtentsion(this.extensionType);
                }
                else
                {
                    this.openExtension(2);
                }
            }
        }

        private void aleLowerClicked()
        {
            if (!this.ViewOnly)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeStats(0, 0, -1);
                }
                if (this.isExtensionOpen())
                {
                    this.extensionType = 2;
                    this.initExtentsion(this.extensionType);
                }
                else
                {
                    this.openExtension(2);
                }
            }
        }

        private void btnResources_Click(object sender, EventArgs e)
        {
        }

        private void buildingTab1Clicked()
        {
            if (!this.ViewOnly)
            {
                if (this.currentBuildingHeight == 0)
                {
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_open");
                    this.openBuildingTab();
                }
                else
                {
                    if ((this.currentTab == 0) || (this.currentTab >= 0x3e8))
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_close");
                        this.closeBuildingPanel();
                        return;
                    }
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_clicked");
                }
                this.setBuildingTab(0);
            }
        }

        private void buildingTab2Clicked()
        {
            if (!this.ViewOnly)
            {
                if (this.currentBuildingHeight == 0)
                {
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_open");
                    this.openBuildingTab();
                }
                else
                {
                    if (this.currentTab == 1)
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_close");
                        this.closeBuildingPanel();
                        return;
                    }
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_clicked");
                }
                this.setBuildingTab(1);
            }
        }

        private void buildingTab3Clicked()
        {
            if (!this.ViewOnly)
            {
                if (this.currentBuildingHeight == 0)
                {
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_open");
                    this.openBuildingTab();
                }
                else
                {
                    if (this.currentTab == 2)
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_close");
                        this.closeBuildingPanel();
                        return;
                    }
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_clicked");
                }
                this.setBuildingTab(2);
            }
        }

        private void buildingTab4Clicked()
        {
            if (!this.ViewOnly)
            {
                if (this.currentBuildingHeight == 0)
                {
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_open");
                    this.openBuildingTab();
                }
                else
                {
                    if (this.currentTab == 3)
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_close");
                        this.closeBuildingPanel();
                        return;
                    }
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_clicked");
                }
                this.setBuildingTab(3);
            }
        }

        private void buildingTab5Clicked()
        {
            if (!this.ViewOnly)
            {
                if (this.currentBuildingHeight == 0)
                {
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_open");
                    this.openBuildingTab();
                }
                else
                {
                    if (this.currentTab == 4)
                    {
                        GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_close");
                        this.closeBuildingPanel();
                        return;
                    }
                    GameEngine.Instance.playInterfaceSound("VillageMapPanel_buildings_tab_clicked");
                }
                this.setBuildingTab(4);
            }
        }

        private int calcBuildTabYPos()
        {
            int num = (0x19 + this.currentHeight) + Math.Max(this.currentExtensionHeight - 0x19, 0);
            if (num < 0x37)
            {
                num = 0x37;
            }
            return num;
        }

        private int calcCapitalBuildTabYPos()
        {
            return 0x87;
        }

        private int calcInfo2TabYPos()
        {
            int num = this.calcInfoTabYPos();
            int num2 = 0x19 + this.currentInfo1Height;
            if (num2 < 0x37)
            {
                num2 = 0x37;
            }
            return (num2 + num);
        }

        public int calcInfoTabYPos()
        {
            int num = this.calcBuildTabYPos();
            int num2 = 0x19 + this.currentBuildingHeight;
            if (num2 < 0x37)
            {
                num2 = 0x37;
            }
            return (num2 + num);
        }

        private int calcTop10YPos()
        {
            int num = 0x19 + this.currentBuildingHeight;
            if (num < 0x37)
            {
                num = 0x37;
            }
            return (num + 0x87);
        }

        private void capitalTaxHigherClicked()
        {
            if (!this.ViewOnly && (GameEngine.Instance.Village != null))
            {
                GameEngine.Instance.Village.changeStats(0, 0, 0, 1);
            }
        }

        private void capitalTaxLowerClicked()
        {
            if (!this.ViewOnly && (GameEngine.Instance.Village != null))
            {
                GameEngine.Instance.Village.changeStats(0, 0, 0, -1);
            }
        }

        public void clearBuildingInfo()
        {
            this.buildingBeingPlaced = false;
            this.buildTypeLabel.Visible = false;
            this.buildTimeLabel.Visible = false;
            this.buildWoodLabel.Visible = false;
            this.buildGoldLabel.Visible = false;
            this.buildStoneLabel.Visible = false;
            this.buildCapitalHelp.Visible = false;
            this.buildDonationTypeImage.Visible = false;
        }

        private void closeBuildingPanel()
        {
            this.targetBuildingHeight = 0;
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void closeInBuildingPanel()
        {
            this.selectedBuilding = null;
            if ((this.targetInBuildingHeight == 0) && (this.currentInBuildingHeight == 0))
            {
                this.inBuildingHeaderPanelImage.Visible = false;
                this.inBuildingPanelImage.Visible = false;
            }
            this.targetInBuildingHeight = 0;
            if (GameEngine.Instance.Village != null)
            {
                GameEngine.Instance.Village.clearColouredBuildings();
            }
        }

        public void closeInfo1Panel()
        {
            this.info1HeaderPanelImage.CustomTooltipID = 0x6f;
            this.targetInfo1Height = 0;
        }

        private void closePopularityPanel()
        {
            this.headerImage.CustomTooltipID = 0x79;
            this.targetHeight = 0;
            this.targetExtensionHeight = 0;
            this.reopenExtension = false;
        }

        private void closeTopGivers()
        {
            this.targetTopGiversHeight = 0;
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void create()
        {
            this.initPopularityPanel();
            this.initBuildingPanel();
            this.initInfo1Panel();
            this.initInfo2Panel();
            this.initInfo3Panel();
            this.initExtrasPanel();
            this.initInBuildingPanel();
            this.initCapitalPanel();
        }

        public void deleteBuildingClick()
        {
            if (GameEngine.Instance.Village == null)
            {
                return;
            }
            if (VillageMap.isMovingBuilding())
            {
                return;
            }
            if (GameEngine.Instance.World.isCapital(GameEngine.Instance.Village.VillageID))
            {
                if (VillageMap.getCurrentServerTime() >= GameEngine.Instance.Village.m_captialNextDelete)
                {
                    DialogResult result;
                    MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                    if (GameEngine.Instance.World.SecondAgeWorld)
                    {
                        result = MyMessageBox.Show(SK.Text("VillageMapPanel_Delete_Area_You_Sure", "Are you sure you want to delete this building?") + Environment.NewLine + Environment.NewLine + SK.Text("VillageMapPanel_Warning_No_Returned_Resources", "Warning this will not return any resources or flags") + Environment.NewLine + Environment.NewLine + SK.Text("VillageMapPanel_Warning_No_Bonus_Retained", "If this building is deleted, the parish will not retain the bonus or the ability that it confers"), SK.Text("VillageMapPanel_Delete_Bulding", "Delete Building"), yesNo);
                    }
                    else
                    {
                        result = MyMessageBox.Show(SK.Text("VillageMapPanel_Delete_Area_You_Sure", "Are you sure you want to delete this building?") + Environment.NewLine + Environment.NewLine + SK.Text("VillageMapPanel_Warning_No_Returned_Resources", "Warning this will not return any resources or flags"), SK.Text("VillageMapPanel_Delete_Bulding", "Delete Building"), yesNo);
                    }
                    if (result == DialogResult.Yes)
                    {
                        goto Label_012E;
                    }
                }
                return;
            }
        Label_012E:
            if (!this.selectedBuilding.isDeleting())
            {
                if (GameEngine.Instance.Village.getNumDeleting() < 3)
                {
                    GameEngine.Instance.Village.deleteBuilding(this.selectedBuilding);
                }
            }
            else
            {
                GameEngine.Instance.Village.cancelDeleteBuilding(this.selectedBuilding);
            }
            this.inBuildingDeleteButton.Visible = false;
            this.showInBuildingInfo(null);
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

        public void donateClick()
        {
            if (GameEngine.Instance.Village != null)
            {
                Sound.stopVillageEnvironmental();
                InterfaceMgr.Instance.setVillageTabSubMode(0x3fd);
                InterfaceMgr.Instance.capitalDonateResourcesInit(GameEngine.Instance.Village.VillageID, this.selectedBuilding);
            }
        }

        public void eventShownHigherClicked()
        {
            this.currentEventID++;
        }

        public void eventShownLowerClicked()
        {
            this.currentEventID--;
        }

        private void extrasClicked()
        {
            this.closeBuildingPanel();
            this.closePopularityPanel();
            this.closeInBuildingPanel();
            this.closeInfo1Panel();
            this.extrasHeaderPanelImage.Visible = false;
            this.info1HeaderPanelImage.Visible = true;
            this.info2HeaderPanelImage.Visible = true;
            this.info3HeaderPanelImage.Visible = true;
        }

        private void extrasMouseLeave()
        {
            this.extrasHeaderPanelGlowImage.Visible = false;
        }

        private void extrasMouseOver()
        {
            this.extrasHeaderPanelGlowImage.Visible = true;
        }

        private void genericButtonClicked()
        {
            Sound.stopVillageEnvironmental();
            if (((this.selectedBuilding.buildingType == 2) || (this.selectedBuilding.buildingType == 3)) || (this.selectedBuilding.buildingType == 0x23))
            {
                InterfaceMgr.Instance.setVillageTabSubMode(5);
            }
            else if (this.selectedBuilding.buildingType == 0x4e)
            {
                InterfaceMgr.Instance.setVillageTabSubMode(2);
            }
            else if (this.selectedBuilding.buildingType == 0)
            {
                InterfaceMgr.Instance.setVillageTabSubMode(1);
            }
        }

        private void genericButtonClicked2()
        {
            Sound.stopVillageEnvironmental();
            if (this.selectedBuilding.buildingType == 0x4e)
            {
                InterfaceMgr.Instance.setVillageTabSubMode(3);
            }
            else if (this.selectedBuilding.buildingType == 0)
            {
                InterfaceMgr.Instance.setVillageTabSubMode(5);
            }
        }

        public BaseImage getCapitalBuildingDonationTypeImage(int buildingType)
        {
            BaseImage image = null;
            switch (buildingType)
            {
                case 0x4f:
                case 80:
                case 0x51:
                case 0x52:
                case 90:
                case 0x5b:
                case 0x5c:
                case 0x5d:
                case 0x5e:
                case 0x6b:
                case 110:
                    return GFXLibrary.donate_type_food;

                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x59:
                case 0x5f:
                case 0x60:
                case 0x61:
                case 0x62:
                case 0x63:
                case 100:
                case 0x65:
                case 0x66:
                case 0x69:
                case 0x6d:
                    return GFXLibrary.donate_type_resources;

                case 0x67:
                case 0x71:
                case 0x72:
                case 0x75:
                case 0x76:
                case 0x77:
                    return GFXLibrary.donate_type_banquet;

                case 0x68:
                case 0x6c:
                case 0x74:
                case 120:
                    return image;

                case 0x6a:
                case 0x6f:
                case 0x70:
                case 0x73:
                case 0x79:
                case 0x7a:
                case 0x7b:
                    return GFXLibrary.donate_type_weapons;
            }
            return image;
        }

        public CustomSelfDrawPanel.CSDLabel getCapitalNameLabel(int index)
        {
            switch (index)
            {
                case 0:
                    return this.capitalTaxGiver1Label;

                case 1:
                    return this.capitalTaxGiver2Label;

                case 2:
                    return this.capitalTaxGiver3Label;

                case 3:
                    return this.capitalTaxGiver4Label;

                case 4:
                    return this.capitalTaxGiver5Label;

                case 5:
                    return this.capitalTaxGiver6Label;

                case 6:
                    return this.capitalTaxGiver7Label;

                case 7:
                    return this.capitalTaxGiver8Label;

                case 8:
                    return this.capitalTaxGiver9Label;

                case 9:
                    return this.capitalTaxGiver10Label;
            }
            return this.capitalTaxGiverOthersLabel;
        }

        public CustomSelfDrawPanel.CSDLabel getCapitalValueLabel(int index)
        {
            switch (index)
            {
                case 0:
                    return this.capitalTaxGiver1ValueLabel;

                case 1:
                    return this.capitalTaxGiver2ValueLabel;

                case 2:
                    return this.capitalTaxGiver3ValueLabel;

                case 3:
                    return this.capitalTaxGiver4ValueLabel;

                case 4:
                    return this.capitalTaxGiver5ValueLabel;

                case 5:
                    return this.capitalTaxGiver6ValueLabel;

                case 6:
                    return this.capitalTaxGiver7ValueLabel;

                case 7:
                    return this.capitalTaxGiver8ValueLabel;

                case 8:
                    return this.capitalTaxGiver9ValueLabel;

                case 9:
                    return this.capitalTaxGiver10ValueLabel;
            }
            return this.capitalTaxGiverOthersValueLabel;
        }

        public VillageMapBuilding getInBuildingBuilding()
        {
            return this.selectedBuilding;
        }

        public static BaseImage getSmallResourceIcon(int buildingType)
        {
            switch (buildingType)
            {
                case 6:
                    return GFXLibrary.com_16_wood;

                case 7:
                    return GFXLibrary.com_16_stone;

                case 8:
                    return GFXLibrary.com_16_iron;

                case 9:
                    return GFXLibrary.com_16_pitch;

                case 13:
                    return GFXLibrary.com_16_apples;

                case 14:
                    return GFXLibrary.com_16_bread;

                case 15:
                    return GFXLibrary.com_16_veg;

                case 0x10:
                    return GFXLibrary.com_16_meat;

                case 0x11:
                    return GFXLibrary.com_16_cheese;

                case 0x12:
                    return GFXLibrary.com_16_fish;

                case 0x13:
                    return GFXLibrary.com_16_clothing;

                case 0x15:
                    return GFXLibrary.com_16_furniture;

                case 0x16:
                    return GFXLibrary.com_16_venison;

                case 0x17:
                    return GFXLibrary.com_16_salt;

                case 0x18:
                    return GFXLibrary.com_16_spice;

                case 0x19:
                    return GFXLibrary.com_16_silk;

                case 0x1a:
                    return GFXLibrary.com_16_metalwork;

                case 0x1c:
                    return GFXLibrary.com_16_pikes;

                case 0x1d:
                    return GFXLibrary.com_16_bows;

                case 30:
                    return GFXLibrary.com_16_swords;

                case 0x1f:
                    return GFXLibrary.com_16_armour;

                case 0x20:
                    return GFXLibrary.com_16_catapults;

                case 0x21:
                    return GFXLibrary.com_16_wine;
            }
            return null;
        }

        private void headerClicked()
        {
            this.headerImage.CustomTooltipID = 0x7a;
            this.closeBuildingPanel();
            this.closeInBuildingPanel();
            this.closeInfo1Panel();
            if (this.currentHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_popularity_header_open");
                this.targetHeight = 0x14f;
                this.openExtension(0);
                if (GameEngine.Instance.World.getTutorialStage() == 0x67)
                {
                    this.taxLowerButtonGlow.Visible = true;
                }
                else
                {
                    this.taxLowerButtonGlow.Visible = false;
                }
            }
            else if (this.currentHeight == 0x14f)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_popularity_header_close");
                this.closePopularityPanel();
            }
            this.showExtras();
        }

        private void headerMouseLeave()
        {
            this.headerGlowImage.Visible = false;
        }

        private void headerMouseOver()
        {
            this.headerGlowImage.Visible = true;
        }

        public void helpClicked()
        {
            if (this.currentSelectedBuildingType >= 0)
            {
                CapitalHelpBox.openHelpBox(this.currentSelectedBuildingType);
            }
        }

        public void InBuildingPanelUpdate()
        {
            if (GameEngine.Instance.Village != null)
            {
                if (this.selectedBuilding != null)
                {
                    if (!GameEngine.Instance.Village.isValidBuilding(this.selectedBuilding))
                    {
                        this.closeInBuildingPanel();
                    }
                    else if (this.inBuildingCompleteLabel.Visible)
                    {
                        if (!this.selectedBuilding.isDeleting() && this.selectedBuilding.isComplete())
                        {
                            this.inBuildingCompleteLabel.Visible = false;
                            this.inBuildingCompleteLabel2.Visible = false;
                        }
                    }
                    else
                    {
                        switch (this.selectedBuilding.buildingType)
                        {
                            case 0x1c:
                            case 0x1d:
                            case 30:
                            case 0x1f:
                            case 0x20:
                                this.updateWeaponProductionInfo();
                                break;
                        }
                    }
                }
                if (GameEngine.tabPressed)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastTabScroll);
                    if ((span.TotalMilliseconds > 500.0) || GameEngine.tabReleased)
                    {
                        this.lastTabScroll = DateTime.Now;
                        VillageMapBuilding building = null;
                        if (!GameEngine.Instance.GFX.keyControlled)
                        {
                            StatTrackingClient.Instance().ActivateTrigger(0x1b, true);
                            building = GameEngine.Instance.Village.getNextBuilding(this.selectedBuilding);
                        }
                        else
                        {
                            StatTrackingClient.Instance().ActivateTrigger(0x1b, false);
                            building = GameEngine.Instance.Village.getPreviousBuilding(this.selectedBuilding);
                        }
                        if ((building != null) && (building != this.selectedBuilding))
                        {
                            this.showInBuildingInfo(building);
                        }
                    }
                    GameEngine.tabReleased = false;
                }
            }
        }

        private void inBuildngClicked()
        {
            this.closeInBuildingPanel();
        }

        private void inBuildngMouseLeave()
        {
            this.inBuildingHeaderPanelImage.Image = (Image) GFXLibrary.r_building_bar_building_info_norm;
        }

        private void inBuildngMouseOver()
        {
            this.inBuildingHeaderPanelImage.Image = (Image) GFXLibrary.r_building_bar_building_info_over;
        }

        private void info1Clicked()
        {
            if (!GameEngine.Instance.World.isQuestObjectiveComplete(0x2711))
            {
                GameEngine.Instance.World.handleQuestObjectiveHappening(0x2711);
            }
            else
            {
                GameEngine.Instance.World.advanceTutorialOLD();
            }
            this.info1HeaderPanelImage.CustomTooltipID = 0x70;
            this.closeBuildingPanel();
            this.closePopularityPanel();
            this.closeInBuildingPanel();
            if (this.currentInfo1Height == 0)
            {
                this.targetInfo1Height = 0x14f;
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_honour_header_open");
            }
            else if (this.currentInfo1Height == 0x14f)
            {
                this.closeInfo1Panel();
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_honour_header_close");
            }
        }

        private void info1MouseLeave()
        {
            this.info1HeaderPanelImage.Image = (Image) GFXLibrary.infobar_01;
        }

        private void info1MouseOver()
        {
            this.info1HeaderPanelImage.Image = (Image) GFXLibrary.infobar_01_over;
        }

        private void info2Clicked()
        {
            this.closeBuildingPanel();
            this.closePopularityPanel();
            this.closeInBuildingPanel();
            this.closeInfo1Panel();
        }

        private void info2MouseLeave()
        {
            this.info2HeaderPanelGlowImage.Visible = false;
        }

        private void info2MouseOver()
        {
            this.info2HeaderPanelGlowImage.Visible = true;
        }

        private void info3Clicked()
        {
            this.closeBuildingPanel();
            this.closePopularityPanel();
            this.closeInBuildingPanel();
            this.closeInfo1Panel();
        }

        private void info3MouseLeave()
        {
            this.info3HeaderPanelGlowImage.Visible = false;
        }

        private void info3MouseOver()
        {
            this.info3HeaderPanelGlowImage.Visible = true;
        }

        public void initBuildingPanel()
        {
            int y = this.calcBuildTabYPos();
            this.buildPanelImage.Image = (Image) GFXLibrary.r_building_panel_back;
            this.buildPanelImage.Position = new Point(0, y + 0x19);
            base.addControl(this.buildPanelImage);
            this.building1Button.Position = new Point(6, 14);
            this.building1Button.Visible = false;
            this.building1Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building1Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building1Button);
            this.building1Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building1Image.Alpha = 0.65f;
            this.building1Image.Position = new Point(0x40, 0x3b);
            this.building1Button.addControl(this.building1Image);
            this.building1Label.Text = "0";
            this.building1Label.Color = ARGBColors.Black;
            this.building1Label.Position = new Point(0, -2);
            this.building1Label.Size = this.building1Image.Size;
            this.building1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building1Image.addControl(this.building1Label);
            this.building2Button.Position = new Point(0x58, 14);
            this.building2Button.Visible = false;
            this.building2Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building2Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building2Button);
            this.building2Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building2Image.Alpha = 0.65f;
            this.building2Image.Position = new Point(0x40, 0x3b);
            this.building2Button.addControl(this.building2Image);
            this.building2Label.Text = "0";
            this.building2Label.Color = ARGBColors.Black;
            this.building2Label.Position = new Point(0, -2);
            this.building2Label.Size = this.building2Image.Size;
            this.building2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building2Image.addControl(this.building2Label);
            this.building3Button.Position = new Point(6, 0x59);
            this.building3Button.Visible = false;
            this.building3Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building3Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building3Button);
            this.building3Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building3Image.Alpha = 0.65f;
            this.building3Image.Position = new Point(0x40, 0x3b);
            this.building3Button.addControl(this.building3Image);
            this.building3Label.Text = "0";
            this.building3Label.Color = ARGBColors.Black;
            this.building3Label.Position = new Point(0, -2);
            this.building3Label.Size = this.building3Image.Size;
            this.building3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building3Image.addControl(this.building3Label);
            this.building4Button.Position = new Point(0x58, 0x59);
            this.building4Button.Visible = false;
            this.building4Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building4Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building4Button);
            this.building4Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building4Image.Alpha = 0.65f;
            this.building4Image.Position = new Point(0x40, 0x3b);
            this.building4Button.addControl(this.building4Image);
            this.building4Label.Text = "0";
            this.building4Label.Color = ARGBColors.Black;
            this.building4Label.Position = new Point(0, -2);
            this.building4Label.Size = this.building4Image.Size;
            this.building4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building4Image.addControl(this.building4Label);
            this.building5Button.Position = new Point(6, 0xa4);
            this.building5Button.Visible = false;
            this.building5Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building5Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building5Button);
            this.building5Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building5Image.Alpha = 0.65f;
            this.building5Image.Position = new Point(0x40, 0x3b);
            this.building5Button.addControl(this.building5Image);
            this.building5Label.Text = "0";
            this.building5Label.Color = ARGBColors.Black;
            this.building5Label.Position = new Point(0, -2);
            this.building5Label.Size = this.building5Image.Size;
            this.building5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building5Image.addControl(this.building5Label);
            this.building6Button.Position = new Point(0x58, 0xa4);
            this.building6Button.Visible = false;
            this.building6Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building6Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building6Button);
            this.building6Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building6Image.Alpha = 0.65f;
            this.building6Image.Position = new Point(0x40, 0x3b);
            this.building6Button.addControl(this.building6Image);
            this.building6Label.Text = "0";
            this.building6Label.Color = ARGBColors.Black;
            this.building6Label.Position = new Point(0, -2);
            this.building6Label.Size = this.building6Image.Size;
            this.building6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building6Image.addControl(this.building6Label);
            this.building7Button.Position = new Point(6, 0xef);
            this.building7Button.Visible = false;
            this.building7Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building7Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building7Button);
            this.building7Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building7Image.Alpha = 0.65f;
            this.building7Image.Position = new Point(0x40, 0x3b);
            this.building7Button.addControl(this.building7Image);
            this.building7Label.Text = "0";
            this.building7Label.Color = ARGBColors.Black;
            this.building7Label.Position = new Point(0, -2);
            this.building7Label.Size = this.building7Image.Size;
            this.building7Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building7Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building7Image.addControl(this.building7Label);
            this.building8Button.Position = new Point(0x58, 0xef);
            this.building8Button.Visible = false;
            this.building8Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.building8Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.placeBuildingClick), "VillageMapPanel_place_building");
            this.building8Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.placeBuildingMouseLeave));
            this.buildPanelImage.addControl(this.building8Button);
            this.building8Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building8Image.Alpha = 0.65f;
            this.building8Image.Position = new Point(0x40, 0x3b);
            this.building8Button.addControl(this.building8Image);
            this.building8Label.Text = "0";
            this.building8Label.Color = ARGBColors.Black;
            this.building8Label.Position = new Point(0, -2);
            this.building8Label.Size = this.building8Image.Size;
            this.building8Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building8Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building8Image.addControl(this.building8Label);
            this.buildHeaderArea.Position = new Point(0, y);
            this.buildHeaderArea.Size = new Size(0xc4, 0x3e);
            base.addControl(this.buildHeaderArea);
            this.setBuildingTab(-1);
            this.buildTab1Button.Position = new Point(0, 0);
            this.buildTab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buildingTab1Clicked));
            this.buildHeaderArea.addControl(this.buildTab1Button);
            this.buildTab2Button.Position = new Point(0x2c, 0);
            this.buildTab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buildingTab2Clicked));
            this.buildHeaderArea.addControl(this.buildTab2Button);
            this.buildTab3Button.Position = new Point(0x51, 0);
            this.buildTab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buildingTab3Clicked));
            this.buildHeaderArea.addControl(this.buildTab3Button);
            this.buildTab4Button.Position = new Point(0x76, 0);
            this.buildTab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buildingTab4Clicked));
            this.buildHeaderArea.addControl(this.buildTab4Button);
            this.buildTab5Button.Position = new Point(0x9a, 0);
            this.buildTab5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buildingTab5Clicked));
            this.buildHeaderArea.addControl(this.buildTab5Button);
            this.buildInfoImage.Image = (Image) GFXLibrary.r_building_panel_inset;
            this.buildInfoImage.Position = new Point(12, 0x147);
            this.buildPanelImage.addControl(this.buildInfoImage);
            this.buildTypeLabel.Text = "";
            this.buildTypeLabel.Color = ARGBColors.Black;
            this.buildTypeLabel.Position = new Point(13, 4);
            this.buildTypeLabel.Visible = false;
            if (((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "tr")) || ((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "pt")))
            {
                this.buildTypeLabel.Size = new Size(0xa1, 20);
                this.buildTypeLabel.Font = FontManager.GetFont("Arial", 7.5f);
            }
            else
            {
                this.buildTypeLabel.Size = new Size(0x8d, 20);
                this.buildTypeLabel.Font = FontManager.GetFont("Arial", 8.25f);
            }
            this.buildInfoImage.addControl(this.buildTypeLabel);
            this.buildTimeImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_time;
            this.buildTimeImage.Position = new Point(13, 0x16);
            this.buildInfoImage.addControl(this.buildTimeImage);
            this.buildTimeLabel.Text = "";
            this.buildTimeLabel.Color = ARGBColors.Black;
            this.buildTimeLabel.Position = new Point(40, 0x1a);
            this.buildTimeLabel.Size = new Size(120, 20);
            this.buildTimeLabel.Visible = false;
            this.buildInfoImage.addControl(this.buildTimeLabel);
            this.buildWoodImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_wood;
            this.buildWoodImage.Position = new Point(13, 40);
            this.buildInfoImage.addControl(this.buildWoodImage);
            this.buildWoodLabel.Text = "0";
            this.buildWoodLabel.Color = ARGBColors.Black;
            this.buildWoodLabel.Position = new Point(40, 0x2c);
            this.buildWoodLabel.Size = new Size(0x2e, 20);
            this.buildWoodLabel.Visible = false;
            this.buildInfoImage.addControl(this.buildWoodLabel);
            this.buildStoneImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_stone;
            this.buildStoneImage.Position = new Point(13, 0x3a);
            this.buildInfoImage.addControl(this.buildStoneImage);
            this.buildStoneLabel.Text = "0";
            this.buildStoneLabel.Color = ARGBColors.Black;
            this.buildStoneLabel.Position = new Point(40, 0x3e);
            this.buildStoneLabel.Size = new Size(0x2e, 20);
            this.buildStoneLabel.Visible = false;
            this.buildInfoImage.addControl(this.buildStoneLabel);
            this.buildGoldImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_gold;
            this.buildGoldImage.Position = new Point(0x56, 40);
            this.buildInfoImage.addControl(this.buildGoldImage);
            this.buildGoldLabel.Text = "0";
            this.buildGoldLabel.Color = ARGBColors.Black;
            this.buildGoldLabel.Position = new Point(0x71, 0x2c);
            this.buildGoldLabel.Size = new Size(0x2e, 20);
            this.buildGoldLabel.Visible = false;
            this.buildInfoImage.addControl(this.buildGoldLabel);
            this.buildTooltipArea.Position = new Point(13, 0x16);
            this.buildTooltipArea.Size = new Size(150, 0x38);
            this.buildTooltipArea.CustomTooltipID = 140;
            this.buildInfoImage.addControl(this.buildTooltipArea);
            this.buildCapitalHelp.ImageNorm = (Image) GFXLibrary.help_normal;
            this.buildCapitalHelp.ImageOver = (Image) GFXLibrary.help_over;
            this.buildCapitalHelp.ImageClick = (Image) GFXLibrary.help_pushed;
            this.buildCapitalHelp.Position = new Point(0x8f, 0x40);
            this.buildCapitalHelp.Visible = false;
            this.buildCapitalHelp.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.helpClicked), "VillageMapPanel_help");
            this.buildInfoImage.addControl(this.buildCapitalHelp);
            this.buildDonationTypeImage.Image = (Image) this.getCapitalBuildingDonationTypeImage(0x67);
            this.buildDonationTypeImage.setSizeToImage();
            this.buildDonationTypeImage.Position = new Point(this.buildCapitalHelp.Rectangle.Right - this.buildDonationTypeImage.Width, this.buildTimeLabel.Y - 0x11);
            this.buildDonationTypeImage.CustomTooltipID = 0x93;
            this.buildInfoImage.addControl(this.buildDonationTypeImage);
            this.buildDonationTypeImage.Visible = false;
            this.buildPanelFaderImage.Image = (Image) GFXLibrary.r_building_panel_back;
            this.buildPanelFaderImage.Position = new Point(0, 0);
            this.buildPanelFaderImage.Alpha = 0f;
            this.buildPanelImage.addControl(this.buildPanelFaderImage);
            this.clearBuildingInfo();
            this.currentBuildingHeight = 1;
            this.targetBuildingHeight = 0;
        }

        public void initCapitalPanel()
        {
            this.capitalPanelImage.Image = (Image) GFXLibrary.int_tax_panel_back_semipopulated;
            this.capitalPanelImage.Position = new Point(0, 0);
            base.addControl(this.capitalPanelImage);
            this.capitalTaxLowerButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
            this.capitalTaxLowerButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
            this.capitalTaxLowerButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
            this.capitalTaxLowerButton.Position = new Point(0x84, 8);
            this.capitalTaxLowerButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.capitalTaxLowerClicked), "VillageMapPanel_capital_tax_lower");
            this.capitalPanelImage.addControl(this.capitalTaxLowerButton);
            this.capitalTaxHigherButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
            this.capitalTaxHigherButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
            this.capitalTaxHigherButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
            this.capitalTaxHigherButton.Position = new Point(0x9c, 8);
            this.capitalTaxHigherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.capitalTaxHigherClicked), "VillageMapPanel_capital_tax_higher");
            this.capitalPanelImage.addControl(this.capitalTaxHigherButton);
            this.capitalTaxLine1Label.Text = "0";
            this.capitalTaxLine1Label.Color = ARGBColors.Black;
            this.capitalTaxLine1Label.Position = new Point(0x26, 0x10);
            this.capitalTaxLine1Label.Size = new Size(80, 20);
            this.capitalTaxLine1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.capitalTaxLine1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.capitalPanelImage.addControl(this.capitalTaxLine1Label);
            this.capitalTaxBar.setImages((Image) GFXLibrary.r_popularity_panel_colorbar_green_back, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_right, (Image) GFXLibrary.r_popularity_panel_colorbar_red_back, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_right);
            this.capitalTaxBar.Number = 0.0;
            this.capitalTaxBar.MaxValue = 9.0;
            this.capitalTaxBar.Position = new Point(0x87, 0x1f);
            this.capitalPanelImage.addControl(this.capitalTaxBar);
            this.capitalTaxPerDayLabel.Text = SK.Text("VillageMapPanel_Tithe_Per_Day", "Tithe Per Day") + ":";
            this.capitalTaxPerDayLabel.Color = ARGBColors.Black;
            this.capitalTaxPerDayLabel.Position = new Point(20, 0x3e);
            this.capitalTaxPerDayLabel.Size = new Size(0xa7, 0x11);
            if (Program.mySettings.LanguageIdent == "pl")
            {
                this.capitalTaxPerDayLabel.Font = FontManager.GetFont("Arial", 7.5f);
            }
            else
            {
                this.capitalTaxPerDayLabel.Font = FontManager.GetFont("Arial", 8.25f);
            }
            this.capitalPanelImage.addControl(this.capitalTaxPerDayLabel);
            this.capitalTaxPerDayValueLabel.Text = "0";
            this.capitalTaxPerDayValueLabel.Color = ARGBColors.Black;
            this.capitalTaxPerDayValueLabel.Position = new Point(100, 0x3e);
            this.capitalTaxPerDayValueLabel.Size = new Size((0xb1 - this.capitalTaxPerDayValueLabel.X) - 10, 0x11);
            this.capitalTaxPerDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalPanelImage.addControl(this.capitalTaxPerDayValueLabel);
            this.capitalLastTaxRateLabel.Text = SK.Text("VillageMapPanel_Yesterdays_Tithe", "Yesterday's Tithe Rate") + ":";
            this.capitalLastTaxRateLabel.Color = ARGBColors.Black;
            this.capitalLastTaxRateLabel.Position = new Point(20, 0x51);
            this.capitalLastTaxRateLabel.Size = new Size(0xa7, 0x11);
            if (Program.mySettings.LanguageIdent == "pl")
            {
                this.capitalLastTaxRateLabel.Font = FontManager.GetFont("Arial", 7.5f);
            }
            else
            {
                this.capitalLastTaxRateLabel.Font = FontManager.GetFont("Arial", 8.25f);
            }
            this.capitalPanelImage.addControl(this.capitalLastTaxRateLabel);
            this.capitalLastTaxRateValueLabel.Text = "";
            this.capitalLastTaxRateValueLabel.Color = ARGBColors.Black;
            this.capitalLastTaxRateValueLabel.Position = new Point(100, 0x51);
            this.capitalLastTaxRateValueLabel.Size = new Size((0xb1 - this.capitalLastTaxRateValueLabel.X) - 10, 0x11);
            this.capitalLastTaxRateValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalPanelImage.addControl(this.capitalLastTaxRateValueLabel);
            this.capitalOutgoingPerDayLabel.Text = SK.Text("VillageMapPanel_County_Tithe", "County Tithe") + ":";
            this.capitalOutgoingPerDayLabel.Color = ARGBColors.Black;
            this.capitalOutgoingPerDayLabel.Position = new Point(20, 100);
            this.capitalOutgoingPerDayLabel.Size = new Size(0xa7, 0x11);
            if (Program.mySettings.LanguageIdent == "pl")
            {
                this.capitalOutgoingPerDayLabel.Font = FontManager.GetFont("Arial", 7.5f);
            }
            else
            {
                this.capitalOutgoingPerDayLabel.Font = FontManager.GetFont("Arial", 8.25f);
            }
            this.capitalPanelImage.addControl(this.capitalOutgoingPerDayLabel);
            this.capitalOutgoingPerDayValueLabel.Text = "0";
            this.capitalOutgoingPerDayValueLabel.Color = ARGBColors.Red;
            this.capitalOutgoingPerDayValueLabel.Position = new Point(100, 100);
            this.capitalOutgoingPerDayValueLabel.Size = new Size((0xb1 - this.capitalOutgoingPerDayValueLabel.X) - 10, 0x11);
            this.capitalOutgoingPerDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalPanelImage.addControl(this.capitalOutgoingPerDayValueLabel);
            this.capitalTop10PanelImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.capitalTop10PanelImage.Position = new Point(0, 0x127);
            base.addControl(this.capitalTop10PanelImage);
            this.capitalTop10HeaderImage.Image = (Image) GFXLibrary.r_building_bar_building_info_norm;
            this.capitalTop10HeaderImage.Position = new Point(0, 270);
            this.capitalTop10HeaderImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.topGiversMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.topGiversMouseLeave));
            this.capitalTop10HeaderImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openTopGivers));
            base.addControl(this.capitalTop10HeaderImage);
            this.capitalTop10HeaderGlowImage.Image = (Image) GFXLibrary.infobar_02_over;
            this.capitalTop10HeaderGlowImage.Position = new Point(0, 0);
            this.capitalTop10HeaderGlowImage.Visible = false;
            this.capitalTop10HeaderImage.addControl(this.capitalTop10HeaderGlowImage);
            this.capitalTaxTopGivers.Text = SK.Text("VillageMapPanel_Top_Givers", "Top Tax Payers");
            this.capitalTaxTopGivers.Color = ARGBColors.Black;
            this.capitalTaxTopGivers.Position = new Point(0, -5);
            this.capitalTaxTopGivers.Size = this.capitalTop10HeaderImage.Size;
            this.capitalTaxTopGivers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.capitalTaxTopGivers.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.capitalTop10HeaderImage.addControl(this.capitalTaxTopGivers);
            this.capitalTaxGiver1Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver1Label.Color = ARGBColors.Black;
            this.capitalTaxGiver1Label.Position = new Point(20, 0x71);
            this.capitalTaxGiver1Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver1Label);
            this.capitalTaxGiver1ValueLabel.Text = "0";
            this.capitalTaxGiver1ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver1ValueLabel.Position = new Point(100, 0x71);
            this.capitalTaxGiver1ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver1ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver1ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver1ValueLabel);
            this.capitalTaxGiver2Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver2Label.Color = ARGBColors.Black;
            this.capitalTaxGiver2Label.Position = new Point(20, 130);
            this.capitalTaxGiver2Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver2Label);
            this.capitalTaxGiver2ValueLabel.Text = "0";
            this.capitalTaxGiver2ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver2ValueLabel.Position = new Point(100, 130);
            this.capitalTaxGiver2ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver2ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver2ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver2ValueLabel);
            this.capitalTaxGiver3Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver3Label.Color = ARGBColors.Black;
            this.capitalTaxGiver3Label.Position = new Point(20, 0x93);
            this.capitalTaxGiver3Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver3Label);
            this.capitalTaxGiver3ValueLabel.Text = "0";
            this.capitalTaxGiver3ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver3ValueLabel.Position = new Point(100, 0x93);
            this.capitalTaxGiver3ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver3ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver3ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver3ValueLabel);
            this.capitalTaxGiver4Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver4Label.Color = ARGBColors.Black;
            this.capitalTaxGiver4Label.Position = new Point(20, 0xa4);
            this.capitalTaxGiver4Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver4Label);
            this.capitalTaxGiver4ValueLabel.Text = "0";
            this.capitalTaxGiver4ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver4ValueLabel.Position = new Point(100, 0xa4);
            this.capitalTaxGiver4ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver4ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver4ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver4ValueLabel);
            this.capitalTaxGiver5Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver5Label.Color = ARGBColors.Black;
            this.capitalTaxGiver5Label.Position = new Point(20, 0xb5);
            this.capitalTaxGiver5Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver5Label);
            this.capitalTaxGiver5ValueLabel.Text = "0";
            this.capitalTaxGiver5ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver5ValueLabel.Position = new Point(100, 0xb5);
            this.capitalTaxGiver5ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver5ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver5ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver5ValueLabel);
            this.capitalTaxGiver6Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver6Label.Color = ARGBColors.Black;
            this.capitalTaxGiver6Label.Position = new Point(20, 0xc6);
            this.capitalTaxGiver6Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver6Label);
            this.capitalTaxGiver6ValueLabel.Text = "0";
            this.capitalTaxGiver6ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver6ValueLabel.Position = new Point(100, 0xc6);
            this.capitalTaxGiver6ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver6ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver6ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver6ValueLabel);
            this.capitalTaxGiver7Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver7Label.Color = ARGBColors.Black;
            this.capitalTaxGiver7Label.Position = new Point(20, 0xd7);
            this.capitalTaxGiver7Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver7Label);
            this.capitalTaxGiver7ValueLabel.Text = "0";
            this.capitalTaxGiver7ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver7ValueLabel.Position = new Point(100, 0xd7);
            this.capitalTaxGiver7ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver7ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver7ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver7ValueLabel);
            this.capitalTaxGiver8Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver8Label.Color = ARGBColors.Black;
            this.capitalTaxGiver8Label.Position = new Point(20, 0xe8);
            this.capitalTaxGiver8Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver8Label);
            this.capitalTaxGiver8ValueLabel.Text = "0";
            this.capitalTaxGiver8ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver8ValueLabel.Position = new Point(100, 0xe8);
            this.capitalTaxGiver8ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver8ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver8ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver8ValueLabel);
            this.capitalTaxGiver9Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver9Label.Color = ARGBColors.Black;
            this.capitalTaxGiver9Label.Position = new Point(20, 0xf9);
            this.capitalTaxGiver9Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver9Label);
            this.capitalTaxGiver9ValueLabel.Text = "0";
            this.capitalTaxGiver9ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver9ValueLabel.Position = new Point(100, 0xf9);
            this.capitalTaxGiver9ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver9ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver9ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver9ValueLabel);
            this.capitalTaxGiver10Label.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.capitalTaxGiver10Label.Color = ARGBColors.Black;
            this.capitalTaxGiver10Label.Position = new Point(20, 0x10a);
            this.capitalTaxGiver10Label.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver10Label);
            this.capitalTaxGiver10ValueLabel.Text = "0";
            this.capitalTaxGiver10ValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiver10ValueLabel.Position = new Point(100, 0x10a);
            this.capitalTaxGiver10ValueLabel.Size = new Size((0xb1 - this.capitalTaxGiver10ValueLabel.X) - 10, 0x11);
            this.capitalTaxGiver10ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiver10ValueLabel);
            this.capitalTaxGiverOthersLabel.Text = SK.Text("VillageMapPanel_Others", "Others");
            this.capitalTaxGiverOthersLabel.Color = ARGBColors.Black;
            this.capitalTaxGiverOthersLabel.Position = new Point(20, 0x127);
            this.capitalTaxGiverOthersLabel.Size = new Size(0xa7, 0x11);
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiverOthersLabel);
            this.capitalTaxGiverOthersValueLabel.Text = "0";
            this.capitalTaxGiverOthersValueLabel.Color = ARGBColors.Black;
            this.capitalTaxGiverOthersValueLabel.Position = new Point(100, 0x127);
            this.capitalTaxGiverOthersValueLabel.Size = new Size((0xb1 - this.capitalTaxGiverOthersValueLabel.X) - 10, 0x11);
            this.capitalTaxGiverOthersValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.capitalTop10PanelImage.addControl(this.capitalTaxGiverOthersValueLabel);
            this.capitalTop10PanelFaderImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.capitalTop10PanelFaderImage.Position = new Point(0, 0);
            this.capitalTop10PanelFaderImage.Alpha = 0f;
            this.capitalTop10PanelImage.addControl(this.capitalTop10PanelFaderImage);
            this.showCapitalData(0, 0, null, 0, 0);
        }

        private void initExtentsion(int type)
        {
            switch (type)
            {
                case 0:
                    this.indent1Image.CustomTooltipID = 0x7c;
                    break;

                case 1:
                    this.indent2Image.CustomTooltipID = 0x7e;
                    break;

                case 2:
                    this.indent3Image.CustomTooltipID = 0x80;
                    break;

                case 3:
                    this.indent4Image.CustomTooltipID = 130;
                    break;

                case 4:
                    this.indent5Image.CustomTooltipID = 0x84;
                    break;

                case 5:
                    this.indent6Image.CustomTooltipID = 0x86;
                    break;
            }
            this.taxExtensionArea.Visible = false;
            this.rationsExtensionArea.Visible = false;
            this.aleExtensionArea.Visible = false;
            this.buildingExtensionArea.Visible = false;
            this.housingExtensionArea.Visible = false;
            this.eventsExtensionArea.Visible = false;
            switch (type)
            {
                case 0:
                    this.taxExtensionArea.Visible = true;
                    return;

                case 1:
                    this.rationsExtensionArea.Visible = true;
                    return;

                case 2:
                    this.aleExtensionArea.Visible = true;
                    return;

                case 3:
                    this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Housing", "Housing");
                    this.housingExtensionArea.Visible = true;
                    return;

                case 4:
                    this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Buildings", "Buildings");
                    this.buildingExtensionArea.Visible = true;
                    return;

                case 5:
                    this.extensionHeaderLabel.Text = "";
                    this.eventsExtensionArea.Visible = true;
                    return;
            }
        }

        public void initExtrasPanel()
        {
            int y = this.calcInfoTabYPos();
            this.extrasHeaderPanelImage.Image = (Image) GFXLibrary.extrasbar_01;
            this.extrasHeaderPanelImage.Position = new Point(0, y);
            this.extrasHeaderPanelImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.extrasMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.extrasMouseLeave));
            this.extrasHeaderPanelImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.extrasClicked), "VillageMapPanel_extras_header");
            this.extrasHeaderPanelImage.Visible = false;
            this.extrasHeaderPanelImage.CustomTooltipID = 110;
            base.addControl(this.extrasHeaderPanelImage);
            this.extrasHeaderPanelGlowImage.Image = (Image) GFXLibrary.extrasbar_01_over;
            this.extrasHeaderPanelGlowImage.Position = new Point(0, 0);
            this.extrasHeaderPanelGlowImage.Visible = false;
            this.extrasHeaderPanelImage.addControl(this.extrasHeaderPanelGlowImage);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "VillageMapPanel";
            base.Size = new Size(0xc4, 0x236);
            base.ResumeLayout(false);
        }

        public void initInBuildingPanel()
        {
            int y = ((this.calcInfoTabYPos() + 0x37) + 0x37) + 0x37;
            this.inBuildingPanelImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.inBuildingPanelImage.Position = new Point(0, 0x19);
            base.addControl(this.inBuildingPanelImage);
            this.inBuildingPanelImage.Visible = false;
            this.inBuildingHeaderPanelImage.Image = (Image) GFXLibrary.r_building_bar_building_info_norm;
            this.inBuildingHeaderPanelImage.Position = new Point(0, y);
            this.inBuildingHeaderPanelImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.inBuildngMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.inBuildngMouseLeave));
            this.inBuildingHeaderPanelImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.inBuildngClicked), "VillageMapPanel_inbuilding_header");
            this.inBuildingHeaderPanelImage.CustomTooltipID = 0x71;
            this.inBuildingHeaderPanelImage.CustomTooltipData = -1;
            base.addControl(this.inBuildingHeaderPanelImage);
            this.inBuildingHeaderPanelImage.Visible = false;
            this.inBuildingTypeImage.Position = new Point(0, -23);
            this.inBuildingHeaderPanelImage.addControl(this.inBuildingTypeImage);
            this.inBuildingMoveButton.Position = new Point(0x81, 2);
            this.inBuildingMoveButton.ImageNorm = (Image) GFXLibrary.but_move_building_normal;
            this.inBuildingMoveButton.ImageOver = (Image) GFXLibrary.but_move_building_over;
            this.inBuildingMoveButton.ImageClick = (Image) GFXLibrary.but_move_building_pushed;
            this.inBuildingMoveButton.CustomTooltipID = 0x72;
            this.inBuildingMoveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.moveBuildingClick), "VillageMapPanel_move_building");
            this.inBuildingHeaderPanelImage.addControl(this.inBuildingMoveButton);
            this.inBuildingHelpButton.Position = new Point(100, 0x12);
            this.inBuildingHelpButton.ImageNorm = (Image) GFXLibrary.help_normal;
            this.inBuildingHelpButton.ImageOver = (Image) GFXLibrary.help_over;
            this.inBuildingHelpButton.ImageClick = (Image) GFXLibrary.help_pushed;
            this.inBuildingHelpButton.Visible = false;
            this.inBuildingHelpButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.helpClicked), "VillageMapPanel_inbulding_help");
            this.inBuildingHeaderPanelImage.addControl(this.inBuildingHelpButton);
            this.inBuildingName.Text = "";
            this.inBuildingName.Color = ARGBColors.Black;
            this.inBuildingName.DropShadowColor = ARGBColors.White;
            this.inBuildingName.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 40);
            this.inBuildingName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.inBuildingName.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.inBuildingName.Position = new Point(8, 70);
            this.inBuildingTypeImage.addControl(this.inBuildingName);
            this.inBuildingCompleteLabel.Text = "";
            this.inBuildingCompleteLabel.Color = ARGBColors.Black;
            this.inBuildingCompleteLabel.Position = new Point(8, 0x34);
            this.inBuildingCompleteLabel.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingCompleteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.inBuildingCompleteLabel.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingCompleteLabel);
            this.inBuildingCompleteLabel2.Text = "";
            this.inBuildingCompleteLabel2.Color = ARGBColors.Black;
            this.inBuildingCompleteLabel2.Position = new Point(8, 70);
            this.inBuildingCompleteLabel2.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingCompleteLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.inBuildingCompleteLabel2.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingCompleteLabel2);
            this.inBuildingDonateButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.inBuildingDonateButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.inBuildingDonateButton.Position = new Point(0x15, 240);
            this.inBuildingDonateButton.Text.Text = SK.Text("VillageMapPanel_Donate_Resources", "Donate Resources");
            this.inBuildingDonateButton.TextYOffset = -1;
            this.inBuildingDonateButton.Text.Color = ARGBColors.Black;
            this.inBuildingDonateButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.donateClick), "VillageMapPanel_donate");
            this.inBuildingDonateButton.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingDonateButton);
            this.inBuildingDeleteButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.inBuildingDeleteButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.inBuildingDeleteButton.Position = new Point(0x15, 0x113);
            this.inBuildingDeleteButton.Text.Text = SK.Text("VillageMapPanel_Delete_This_Building", "Delete this Building");
            this.inBuildingDeleteButton.TextYOffset = -1;
            this.inBuildingDeleteButton.Text.Color = ARGBColors.Black;
            this.inBuildingDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteBuildingClick), "VillageMapPanel_delete");
            this.inBuildingDeleteButton.Enabled = true;
            if (Program.mySettings.LanguageIdent == "it")
            {
                this.inBuildingDeleteButton.Text.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
            }
            else
            {
                this.inBuildingDeleteButton.Text.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            }
            this.inBuildingPanelImage.addControl(this.inBuildingDeleteButton);
            if (this.m_villageIsCapital && (VillageMap.getCurrentServerTime() < GameEngine.Instance.Village.m_captialNextDelete))
            {
                this.inBuildingDeleteButton.Enabled = false;
                string str = SK.Text("VillageMapPanel_Next_Delete", "Next Delete : ") + " < ";
                TimeSpan span = (TimeSpan) (GameEngine.Instance.Village.m_captialNextDelete - VillageMap.getCurrentServerTime());
                int num2 = (int) (span.TotalHours + 1.0);
                str = str + num2.ToString() + " ";
                if (num2 == 1)
                {
                    str = str + SK.Text("VillageMapPanel_Hour", "Hour");
                }
                else
                {
                    str = str + SK.Text("VillageMapPanel_Hours", "Hours");
                }
                this.inBuildingDeleteButton.Text.Text = str;
            }
            this.inBuildingAllIndustryOnButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.inBuildingAllIndustryOnButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.inBuildingAllIndustryOnButton.Position = new Point(0x15, 0xee);
            this.inBuildingAllIndustryOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_All_On", "Turn All On");
            this.inBuildingAllIndustryOnButton.TextYOffset = -1;
            this.inBuildingAllIndustryOnButton.Text.Color = ARGBColors.Black;
            if (Program.mySettings.LanguageIdent == "pt")
            {
                this.inBuildingAllIndustryOnButton.Text.Font = FontManager.GetFont("Arial", 7.5f);
            }
            else
            {
                this.inBuildingAllIndustryOnButton.Text.Font = FontManager.GetFont("Arial", 8.25f);
            }
            this.inBuildingAllIndustryOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnAllIndustryOnClicked), "VillageMapPanel_all_on");
            this.inBuildingPanelImage.addControl(this.inBuildingAllIndustryOnButton);
            this.inBuildingIndustryAllOnButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.inBuildingIndustryAllOnButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.inBuildingIndustryAllOnButton.Position = new Point(0x15, 0xc9);
            this.inBuildingIndustryAllOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_All_On", "Turn All On");
            this.inBuildingIndustryAllOnButton.TextYOffset = -1;
            this.inBuildingIndustryAllOnButton.Text.Color = ARGBColors.Black;
            this.inBuildingIndustryAllOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnAllOnClicked), "VillageMapPanel_all_on");
            this.inBuildingPanelImage.addControl(this.inBuildingIndustryAllOnButton);
            this.inBuildingIndustryThisOnButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.inBuildingIndustryThisOnButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.inBuildingIndustryThisOnButton.Position = new Point(0x15, 0xa4);
            this.inBuildingIndustryThisOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_This_On", "Turn This On");
            this.inBuildingIndustryThisOnButton.TextYOffset = -1;
            this.inBuildingIndustryThisOnButton.Text.Color = ARGBColors.Black;
            this.inBuildingIndustryThisOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnThisOnClicked), "VillageMapPanel_this_on");
            this.inBuildingPanelImage.addControl(this.inBuildingIndustryThisOnButton);
            this.inBuildingFaderImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.inBuildingFaderImage.Position = new Point(0, 0);
            this.inBuildingFaderImage.Alpha = 0f;
            this.inBuildingPanelImage.addControl(this.inBuildingFaderImage);
            this.currentInBuildingHeight = 1;
            this.targetInBuildingHeight = 0;
            this.inBuildingGenericButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.inBuildingGenericButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.inBuildingGenericButton.Position = new Point(0x15, 0x9e);
            this.inBuildingGenericButton.Text.Text = "";
            this.inBuildingGenericButton.TextYOffset = -1;
            this.inBuildingGenericButton.Text.Color = ARGBColors.Black;
            this.inBuildingGenericButton.Visible = false;
            this.inBuildingGenericButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.genericButtonClicked), "VillageMapPanel_generic_1");
            this.inBuildingPanelImage.addControl(this.inBuildingGenericButton);
            this.inBuildingGenericButton2.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.inBuildingGenericButton2.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.inBuildingGenericButton2.Position = new Point(0x15, 0x75);
            this.inBuildingGenericButton2.Text.Text = "";
            this.inBuildingGenericButton2.TextYOffset = -1;
            this.inBuildingGenericButton2.Text.Color = ARGBColors.Black;
            this.inBuildingGenericButton2.Visible = false;
            this.inBuildingGenericButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.genericButtonClicked2), "VillageMapPanel_generic_2");
            this.inBuildingPanelImage.addControl(this.inBuildingGenericButton2);
            this.inBuildingMakeWeaponLabel0.Text = SK.Text("VillageMapPanel_Producing_Per_Day", "Producing Per Day");
            this.inBuildingMakeWeaponLabel0.Color = ARGBColors.Black;
            this.inBuildingMakeWeaponLabel0.Position = new Point(8, 0x52);
            this.inBuildingMakeWeaponLabel0.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingMakeWeaponLabel0.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingMakeWeaponLabel0.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponLabel0);
            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Production_Efficiency", "Production Efficiency");
            this.inBuildingMakeWeaponLabel1.Color = ARGBColors.Black;
            this.inBuildingMakeWeaponLabel1.Position = new Point(8, 100);
            this.inBuildingMakeWeaponLabel1.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingMakeWeaponLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.inBuildingMakeWeaponLabel1.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponLabel1);
            this.inBuildingMakeWeaponLabel2.Text = "100%";
            this.inBuildingMakeWeaponLabel2.Color = ARGBColors.Black;
            this.inBuildingMakeWeaponLabel2.Position = new Point(8, 100);
            this.inBuildingMakeWeaponLabel2.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingMakeWeaponLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingMakeWeaponLabel2.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponLabel2);
            this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Production_Efficiency", "Production Efficiency");
            this.inBuildingMakeWeaponLabel3.Color = ARGBColors.Black;
            this.inBuildingMakeWeaponLabel3.Position = new Point(8, 0x76);
            this.inBuildingMakeWeaponLabel3.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingMakeWeaponLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.inBuildingMakeWeaponLabel3.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponLabel3);
            this.inBuildingMakeWeaponLabel4.Text = "100%";
            this.inBuildingMakeWeaponLabel4.Color = ARGBColors.Black;
            this.inBuildingMakeWeaponLabel4.Position = new Point(8, 0x76);
            this.inBuildingMakeWeaponLabel4.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingMakeWeaponLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingMakeWeaponLabel4.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponLabel4);
            this.inBuildingMakeWeaponLabel5.Text = SK.Text("VillageMapPanel_Cost_Each", "Cost Each");
            this.inBuildingMakeWeaponLabel5.Color = ARGBColors.Black;
            this.inBuildingMakeWeaponLabel5.Position = new Point(8, 0x88);
            this.inBuildingMakeWeaponLabel5.Size = new Size(this.inBuildingPanelImage.Width - 0x18, 20);
            this.inBuildingMakeWeaponLabel5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.inBuildingMakeWeaponLabel5.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponLabel5);
            this.inBuildingMakeWeaponLabel6.Text = "0";
            this.inBuildingMakeWeaponLabel6.Color = ARGBColors.Black;
            this.inBuildingMakeWeaponLabel6.Position = new Point(8, 0x88);
            this.inBuildingMakeWeaponLabel6.Size = new Size((this.inBuildingPanelImage.Width - 0x18) - 0x12, 20);
            this.inBuildingMakeWeaponLabel6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingMakeWeaponLabel6.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponLabel6);
            this.inBuildingMakeWeaponImage1.Position = new Point((this.inBuildingPanelImage.Width - 20) - 0x10, 0x76);
            this.inBuildingMakeWeaponImage1.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponImage1);
            this.inBuildingMakeWeaponImage2.Position = new Point(((this.inBuildingPanelImage.Width - 20) - 0x10) - 0x10, 0x76);
            this.inBuildingMakeWeaponImage2.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponImage2);
            this.inBuildingMakeWeaponImage3.Position = new Point(((this.inBuildingPanelImage.Width - 20) - 0x10) + 4, 0x88);
            this.inBuildingMakeWeaponImage3.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponImage3);
            this.inBuildingMakeWeaponImage4.Position = new Point((((this.inBuildingPanelImage.Width - 20) - 0x10) + 2) - 0x10, 0x88);
            this.inBuildingMakeWeaponImage4.Visible = false;
            this.inBuildingPanelImage.addControl(this.inBuildingMakeWeaponImage4);
            this.inBuildingCapitalResourceLevelLabel1.Text = SK.Text("VillageMapPanel_Current_Level", "Current Level");
            this.inBuildingCapitalResourceLevelLabel1.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLevelLabel1.Position = new Point(20, 0x4e);
            this.inBuildingCapitalResourceLevelLabel1.Size = new Size(120, 20);
            this.inBuildingCapitalResourceLevelLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.inBuildingCapitalResourceLevelLabel1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLevelLabel1);
            this.inBuildingCapitalResourceLevelLabel2.Text = "0";
            this.inBuildingCapitalResourceLevelLabel2.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLevelLabel2.Position = new Point(130, 0x4e);
            this.inBuildingCapitalResourceLevelLabel2.Size = new Size(40, 20);
            this.inBuildingCapitalResourceLevelLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLevelLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLevelLabel2);
            this.inBuildingCapitalResourceLabel1a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel1a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel1a.Position = new Point(0x1c, 0x6c);
            this.inBuildingCapitalResourceLabel1a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel1a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel1a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel1a);
            this.inBuildingCapitalResourceLabel1b.Text = "/";
            this.inBuildingCapitalResourceLabel1b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel1b.Position = new Point(0x67, 0x6c);
            this.inBuildingCapitalResourceLabel1b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel1b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel1b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel1b);
            this.inBuildingCapitalResourceLabel1c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel1c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel1c.Position = new Point(0x6c, 0x6c);
            this.inBuildingCapitalResourceLabel1c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel1c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel1c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel1c);
            this.inBuildingCapitalResourceImage1.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage1.Position = new Point(15, 0x6c);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage1);
            this.inBuildingCapitalResourceLabel2a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel2a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel2a.Position = new Point(0x1c, 0x7c);
            this.inBuildingCapitalResourceLabel2a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel2a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel2a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel2a);
            this.inBuildingCapitalResourceLabel2b.Text = "/";
            this.inBuildingCapitalResourceLabel2b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel2b.Position = new Point(0x67, 0x7c);
            this.inBuildingCapitalResourceLabel2b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel2b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel2b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel2b);
            this.inBuildingCapitalResourceLabel2c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel2c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel2c.Position = new Point(0x6c, 0x7c);
            this.inBuildingCapitalResourceLabel2c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel2c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel2c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel2c);
            this.inBuildingCapitalResourceImage2.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage2.Position = new Point(15, 0x7c);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage2);
            this.inBuildingCapitalResourceLabel3a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel3a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel3a.Position = new Point(0x1c, 140);
            this.inBuildingCapitalResourceLabel3a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel3a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel3a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel3a);
            this.inBuildingCapitalResourceLabel3b.Text = "/";
            this.inBuildingCapitalResourceLabel3b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel3b.Position = new Point(0x67, 140);
            this.inBuildingCapitalResourceLabel3b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel3b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel3b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel3b);
            this.inBuildingCapitalResourceLabel3c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel3c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel3c.Position = new Point(0x6c, 140);
            this.inBuildingCapitalResourceLabel3c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel3c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel3c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel3c);
            this.inBuildingCapitalResourceImage3.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage3.Position = new Point(15, 140);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage3);
            this.inBuildingCapitalResourceLabel4a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel4a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel4a.Position = new Point(0x1c, 0x9c);
            this.inBuildingCapitalResourceLabel4a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel4a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel4a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel4a);
            this.inBuildingCapitalResourceLabel4b.Text = "/";
            this.inBuildingCapitalResourceLabel4b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel4b.Position = new Point(0x67, 0x9c);
            this.inBuildingCapitalResourceLabel4b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel4b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel4b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel4b);
            this.inBuildingCapitalResourceLabel4c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel4c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel4c.Position = new Point(0x6c, 0x9c);
            this.inBuildingCapitalResourceLabel4c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel4c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel4c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel4c);
            this.inBuildingCapitalResourceImage4.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage4.Position = new Point(15, 0x9c);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage4);
            this.inBuildingCapitalResourceLabel5a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel5a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel5a.Position = new Point(0x1c, 0xac);
            this.inBuildingCapitalResourceLabel5a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel5a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel5a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel5a);
            this.inBuildingCapitalResourceLabel5b.Text = "/";
            this.inBuildingCapitalResourceLabel5b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel5b.Position = new Point(0x67, 0xac);
            this.inBuildingCapitalResourceLabel5b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel5b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel5b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel5b);
            this.inBuildingCapitalResourceLabel5c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel5c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel5c.Position = new Point(0x6c, 0xac);
            this.inBuildingCapitalResourceLabel5c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel5c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel5c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel5c);
            this.inBuildingCapitalResourceImage5.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage5.Position = new Point(15, 0xac);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage5);
            this.inBuildingCapitalResourceLabel6a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel6a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel6a.Position = new Point(0x1c, 0xbc);
            this.inBuildingCapitalResourceLabel6a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel6a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel6a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel6a);
            this.inBuildingCapitalResourceLabel6b.Text = "/";
            this.inBuildingCapitalResourceLabel6b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel6b.Position = new Point(0x67, 0xbc);
            this.inBuildingCapitalResourceLabel6b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel6b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel6b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel6b);
            this.inBuildingCapitalResourceLabel6c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel6c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel6c.Position = new Point(0x6c, 0xbc);
            this.inBuildingCapitalResourceLabel6c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel6c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel6c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel6c);
            this.inBuildingCapitalResourceImage6.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage6.Position = new Point(15, 0xbc);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage6);
            this.inBuildingCapitalResourceLabel7a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel7a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel7a.Position = new Point(0x1c, 0xcc);
            this.inBuildingCapitalResourceLabel7a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel7a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel7a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel7a);
            this.inBuildingCapitalResourceLabel7b.Text = "/";
            this.inBuildingCapitalResourceLabel7b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel7b.Position = new Point(0x67, 0xcc);
            this.inBuildingCapitalResourceLabel7b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel7b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel7b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel7b);
            this.inBuildingCapitalResourceLabel7c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel7c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel7c.Position = new Point(0x6c, 0xcc);
            this.inBuildingCapitalResourceLabel7c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel7c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel7c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel7c);
            this.inBuildingCapitalResourceImage7.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage7.Position = new Point(15, 0xcc);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage7);
            this.inBuildingCapitalResourceLabel8a.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel8a.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel8a.Position = new Point(0x1c, 220);
            this.inBuildingCapitalResourceLabel8a.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel8a.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel8a.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel8a);
            this.inBuildingCapitalResourceLabel8b.Text = "/";
            this.inBuildingCapitalResourceLabel8b.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel8b.Position = new Point(0x67, 220);
            this.inBuildingCapitalResourceLabel8b.Size = new Size(10, 20);
            this.inBuildingCapitalResourceLabel8b.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.inBuildingCapitalResourceLabel8b.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel8b);
            this.inBuildingCapitalResourceLabel8c.Text = "1,400,000";
            this.inBuildingCapitalResourceLabel8c.Color = ARGBColors.Black;
            this.inBuildingCapitalResourceLabel8c.Position = new Point(0x6c, 220);
            this.inBuildingCapitalResourceLabel8c.Size = new Size(70, 20);
            this.inBuildingCapitalResourceLabel8c.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.inBuildingCapitalResourceLabel8c.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceLabel8c);
            this.inBuildingCapitalResourceImage8.Image = (Image) GFXLibrary.com_16_iron;
            this.inBuildingCapitalResourceImage8.Position = new Point(15, 220);
            this.inBuildingPanelImage.addControl(this.inBuildingCapitalResourceImage8);
        }

        public void initInfo1Panel()
        {
            int y = this.calcInfoTabYPos();
            this.info1PanelImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.info1PanelImage.Position = new Point(0, y + 0x19);
            base.addControl(this.info1PanelImage);
            this.info1HeaderPanelImage.Image = (Image) GFXLibrary.infobar_01;
            this.info1HeaderPanelImage.Position = new Point(0, y);
            this.info1HeaderPanelImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.info1MouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.info1MouseLeave));
            this.info1HeaderPanelImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.info1Clicked));
            this.info1HeaderPanelImage.CustomTooltipID = 0x6f;
            base.addControl(this.info1HeaderPanelImage);
            this.info1HeaderHonourImage.Image = (Image) GFXLibrary.com_32_honour;
            this.info1HeaderHonourImage.Position = new Point(15, 8);
            this.info1HeaderPanelImage.addControl(this.info1HeaderHonourImage);
            this.info1HeaderHonourAmount.Text = "";
            this.info1HeaderHonourAmount.Color = ARGBColors.Black;
            this.info1HeaderHonourAmount.Position = new Point(0x1c, 0x11);
            this.info1HeaderHonourAmount.Size = new Size(0x95, 20);
            this.info1HeaderHonourAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1HeaderHonourAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1HeaderPanelImage.addControl(this.info1HeaderHonourAmount);
            int num2 = 0x19;
            this.info1ChurchLabel.Text = SK.Text("VillageMapPanel_Churches", "Churches");
            this.info1ChurchLabel.Color = ARGBColors.Black;
            this.info1ChurchLabel.Position = new Point(15, 0x23);
            this.info1ChurchLabel.Size = new Size(0x7a, 20);
            this.info1ChurchLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1PanelImage.addControl(this.info1ChurchLabel);
            this.info1ChurchAmount.Text = "0";
            this.info1ChurchAmount.Color = ARGBColors.Black;
            this.info1ChurchAmount.Position = new Point(0x5f, 0x23);
            this.info1ChurchAmount.Size = new Size(0x52, 20);
            this.info1ChurchAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1ChurchAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1ChurchAmount);
            this.info1DecorativeLabel.Text = SK.Text("VillageMapPanel_Decorative", "Decorative");
            this.info1DecorativeLabel.Color = ARGBColors.Black;
            this.info1DecorativeLabel.Position = new Point(15, 0x23 + num2);
            this.info1DecorativeLabel.Size = new Size(120, 20);
            this.info1DecorativeLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1PanelImage.addControl(this.info1DecorativeLabel);
            this.info1DecorativeAmount.Text = "0";
            this.info1DecorativeAmount.Color = ARGBColors.Black;
            this.info1DecorativeAmount.Position = new Point(0x5f, 0x23 + num2);
            this.info1DecorativeAmount.Size = new Size(0x52, 20);
            this.info1DecorativeAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1DecorativeAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1DecorativeAmount);
            this.info1JusticeLabel.Text = SK.Text("VillageMapPanel_Justice", "Justice");
            this.info1JusticeLabel.Color = ARGBColors.Black;
            this.info1JusticeLabel.Position = new Point(15, 0x23 + (num2 * 2));
            this.info1JusticeLabel.Size = new Size(0x7a, 20);
            this.info1JusticeLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1PanelImage.addControl(this.info1JusticeLabel);
            this.info1JusticeAmount.Text = "0";
            this.info1JusticeAmount.Color = ARGBColors.Black;
            this.info1JusticeAmount.Position = new Point(0x5f, 0x23 + (num2 * 2));
            this.info1JusticeAmount.Size = new Size(0x52, 20);
            this.info1JusticeAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1JusticeAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1JusticeAmount);
            this.info1ArtsLabel.Text = SK.Text("VillageMapPanel_Arts_Research", "Arts Research");
            this.info1ArtsLabel.Color = ARGBColors.Black;
            if (Program.mySettings.LanguageIdent == "pl")
            {
                this.info1ArtsLabel.Position = new Point(15, (0x23 + (num2 * 3)) - 10);
                this.info1ArtsLabel.Size = new Size(0x7a, 40);
                this.info1ArtsLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                this.info1ArtsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            }
            else
            {
                this.info1ArtsLabel.Position = new Point(15, 0x23 + (num2 * 3));
                this.info1ArtsLabel.Size = new Size(0x7a, 20);
                this.info1ArtsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.info1ArtsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            }
            this.info1PanelImage.addControl(this.info1ArtsLabel);
            this.info1ArtsAmount.Text = "0";
            this.info1ArtsAmount.Color = ARGBColors.Black;
            this.info1ArtsAmount.Position = new Point(0x5f, 0x23 + (num2 * 3));
            this.info1ArtsAmount.Size = new Size(0x52, 20);
            this.info1ArtsAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1ArtsAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1ArtsAmount);
            this.info1ParishLabel.Text = SK.Text("VillageMapPanel_Parish_Bonus", "Parish Bonus");
            this.info1ParishLabel.Color = ARGBColors.Black;
            if ((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "pt"))
            {
                this.info1ParishLabel.Position = new Point(15, (0x23 + (num2 * 4)) - 10);
                this.info1ParishLabel.Size = new Size(0x7a, 40);
                this.info1ParishLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                this.info1ParishLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            }
            else
            {
                this.info1ParishLabel.Position = new Point(15, 0x23 + (num2 * 4));
                this.info1ParishLabel.Size = new Size(0x7a, 20);
                this.info1ParishLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.info1ParishLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            }
            this.info1PanelImage.addControl(this.info1ParishLabel);
            this.info1ParishAmount.Text = "0";
            this.info1ParishAmount.Color = ARGBColors.Black;
            this.info1ParishAmount.Position = new Point(0x5f, 0x23 + (num2 * 4));
            this.info1ParishAmount.Size = new Size(0x52, 20);
            this.info1ParishAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1ParishAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1ParishAmount);
            this.info1CardsLabel.Text = SK.Text("VillageMapPanel_Cards", "Cards");
            this.info1CardsLabel.Color = ARGBColors.Black;
            this.info1CardsLabel.Position = new Point(15, 0x23 + (num2 * 5));
            this.info1CardsLabel.Size = new Size(0x7a, 20);
            this.info1CardsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1PanelImage.addControl(this.info1CardsLabel);
            this.info1CardsAmount.Text = "0";
            this.info1CardsAmount.Color = ARGBColors.Black;
            this.info1CardsAmount.Position = new Point(0x5f, 0x23 + (num2 * 5));
            this.info1CardsAmount.Size = new Size(0x52, 20);
            this.info1CardsAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1CardsAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1CardsAmount);
            this.info1BlackLine1Label.Text = "----------";
            this.info1BlackLine1Label.Color = ARGBColors.Black;
            this.info1BlackLine1Label.Position = new Point(15, (0x23 + (num2 * 6)) - 5);
            this.info1BlackLine1Label.Size = new Size(0xa2, 20);
            this.info1BlackLine1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1BlackLine1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1BlackLine1Label);
            this.info1BlackLine1aLabel.Text = "----------";
            this.info1BlackLine1aLabel.Color = ARGBColors.Black;
            this.info1BlackLine1aLabel.Position = new Point(12, (0x23 + (num2 * 6)) - 5);
            this.info1BlackLine1aLabel.Size = new Size(0xa2, 20);
            this.info1BlackLine1aLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1BlackLine1aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1BlackLine1aLabel);
            this.info1PopularityLabel.Text = SK.Text("VillageMapPanel_Multiplier", "Multiplier");
            this.info1PopularityLabel.Color = ARGBColors.Black;
            this.info1PopularityLabel.Position = new Point(15, 0x23 + (num2 * 7));
            this.info1PopularityLabel.Size = new Size(120, 20);
            this.info1PopularityLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1PanelImage.addControl(this.info1PopularityLabel);
            this.info1PopularityAmount.Text = "0";
            this.info1PopularityAmount.Color = ARGBColors.Black;
            this.info1PopularityAmount.Position = new Point(0x5f, 0x23 + (num2 * 7));
            this.info1PopularityAmount.Size = new Size(0x52, 20);
            this.info1PopularityAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1PopularityAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1PopularityAmount);
            this.info1HonourCalc2.Text = "";
            this.info1HonourCalc2.Color = ARGBColors.Black;
            this.info1HonourCalc2.Position = new Point(80, 0x23 + (num2 * 8));
            this.info1HonourCalc2.Size = new Size(70, 20);
            this.info1HonourCalc2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1HonourCalc2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1HonourCalc2);
            this.info1BlackLine2Label.Text = "----------";
            this.info1BlackLine2Label.Color = ARGBColors.Black;
            this.info1BlackLine2Label.Position = new Point(15, 0x23 + (num2 * 9));
            this.info1BlackLine2Label.Size = new Size(0xa2, 20);
            this.info1BlackLine2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1BlackLine2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1BlackLine2Label);
            this.info1BlackLine2aLabel.Text = "----------";
            this.info1BlackLine2aLabel.Color = ARGBColors.Black;
            this.info1BlackLine2aLabel.Position = new Point(12, 0x23 + (num2 * 9));
            this.info1BlackLine2aLabel.Size = new Size(0xa2, 20);
            this.info1BlackLine2aLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1BlackLine2aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1BlackLine2aLabel);
            this.info1HonourPerDayLabel.Text = SK.Text("VillageMapPanel_Honour_Per_Day", "Honour per Day");
            this.info1HonourPerDayLabel.Color = ARGBColors.Black;
            this.info1HonourPerDayLabel.Position = new Point(15, 0x23 + (num2 * 10));
            this.info1HonourPerDayLabel.Size = new Size(0xa2, 20);
            this.info1HonourPerDayLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1PanelImage.addControl(this.info1HonourPerDayLabel);
            this.info1HonourPerDayAmount.Text = "0";
            this.info1HonourPerDayAmount.Color = ARGBColors.Black;
            this.info1HonourPerDayAmount.Position = new Point(0x5f, 0x23 + (num2 * 10));
            this.info1HonourPerDayAmount.Size = new Size(0x52, 20);
            this.info1HonourPerDayAmount.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.info1HonourPerDayAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.info1PanelImage.addControl(this.info1HonourPerDayAmount);
            this.info1PopImage.Image = (Image) GFXLibrary.popularityFace;
            this.info1PopImage.Position = new Point(150, (0x23 + (num2 * 8)) - 7);
            this.info1PanelImage.addControl(this.info1PopImage);
            this.info1PanelFaderImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.info1PanelFaderImage.Position = new Point(0, 0);
            this.info1PanelFaderImage.Alpha = 0f;
            this.info1PanelImage.addControl(this.info1PanelFaderImage);
            this.currentInfo1Height = 1;
            this.targetInfo1Height = 0;
        }

        public void initInfo2Panel()
        {
            int y = this.calcInfo2TabYPos();
            this.info2HeaderPanelImage.Image = (Image) GFXLibrary.infobar_02;
            this.info2HeaderPanelImage.Position = new Point(0, y);
            this.info2HeaderPanelImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.info2MouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.info2MouseLeave));
            this.info2HeaderPanelImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.info2Clicked));
            base.addControl(this.info2HeaderPanelImage);
            this.info2HeaderPanelGlowImage.Image = (Image) GFXLibrary.infobar_02_over;
            this.info2HeaderPanelGlowImage.Position = new Point(0, 0);
            this.info2HeaderPanelGlowImage.Visible = false;
            this.info2HeaderPanelImage.addControl(this.info2HeaderPanelGlowImage);
        }

        public void initInfo3Panel()
        {
            int y = this.calcInfo2TabYPos() + 0x37;
            this.info3HeaderPanelImage.Image = (Image) GFXLibrary.infobar_03;
            this.info3HeaderPanelImage.Position = new Point(0, y);
            this.info3HeaderPanelImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.info3MouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.info3MouseLeave));
            this.info3HeaderPanelImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.info3Clicked));
            base.addControl(this.info3HeaderPanelImage);
            this.info3HeaderPanelGlowImage.Image = (Image) GFXLibrary.infobar_03_over;
            this.info3HeaderPanelGlowImage.Position = new Point(0, 0);
            this.info3HeaderPanelGlowImage.Visible = false;
            this.info3HeaderPanelImage.addControl(this.info3HeaderPanelGlowImage);
        }

        public void initPopularityPanel()
        {
            base.clearControls();
            this.extensionImage.Image = (Image) GFXLibrary.r_popularity_panel_extension_back;
            this.extensionImage.Position = new Point(0, 0x19);
            base.addControl(this.extensionImage);
            this.panelImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.panelImage.Position = new Point(0, 0x19);
            base.addControl(this.panelImage);
            this.indent1Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a;
            this.indent1Image.Position = new Point(8, 0x23);
            this.indent1Image.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openTaxExtension));
            this.indent1Image.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openTaxExtensionOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openTaxExtensionLeave));
            this.indent1Image.CustomTooltipID = 0x7b;
            this.panelImage.addControl(this.indent1Image);
            this.popIndent1Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            this.popIndent1Image.Position = new Point(6, 6);
            this.indent1Image.addControl(this.popIndent1Image);
            this.popImage1.Image = (Image) GFXLibrary.r_popularity_panel_icon_taxes;
            this.popImage1.Position = new Point(0x27, -10);
            this.indent1Image.addControl(this.popImage1);
            this.taxLine1Label.Text = "";
            this.taxLine1Label.Color = ARGBColors.Black;
            this.taxLine1Label.Position = new Point(0x22, 1);
            this.taxLine1Label.Size = new Size((this.indent1Image.Width - 0x18) - 0x1f, this.indent1Image.Height);
            this.taxLine1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.taxLine1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.indent1Image.addControl(this.taxLine1Label);
            this.taxPopLabel.Text = "";
            this.taxPopLabel.Color = ARGBColors.White;
            this.taxPopLabel.DropShadowColor = ARGBColors.Black;
            this.taxPopLabel.Position = new Point(0, 0);
            this.taxPopLabel.Size = new Size(this.popIndent1Image.Width, this.popIndent1Image.Height);
            this.taxPopLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.taxPopLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.popIndent1Image.addControl(this.taxPopLabel);
            this.indent2Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a;
            this.indent2Image.Position = new Point(8, 0x54);
            this.indent2Image.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openRationsExtension));
            this.indent2Image.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openRationsExtensionOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openRationsExtensionLeave));
            this.indent2Image.CustomTooltipID = 0x7d;
            this.panelImage.addControl(this.indent2Image);
            this.popIndent2Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            this.popIndent2Image.Position = new Point(6, 6);
            this.indent2Image.addControl(this.popIndent2Image);
            this.popImage2.Image = (Image) GFXLibrary.r_popularity_panel_icon_rations;
            this.popImage2.Position = new Point(0x27, -10);
            this.indent2Image.addControl(this.popImage2);
            this.rationsLine1Label.Text = "";
            this.rationsLine1Label.Color = ARGBColors.Black;
            this.rationsLine1Label.Position = new Point(0x22, 1);
            this.rationsLine1Label.Size = new Size((this.indent2Image.Width - 0x18) - 0x1f, this.indent2Image.Height);
            this.rationsLine1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.rationsLine1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.indent2Image.addControl(this.rationsLine1Label);
            this.rationsPopLabel.Text = "";
            this.rationsPopLabel.Color = ARGBColors.White;
            this.rationsPopLabel.DropShadowColor = ARGBColors.Black;
            this.rationsPopLabel.Position = new Point(0, 0);
            this.rationsPopLabel.Size = new Size(this.popIndent2Image.Width, this.popIndent2Image.Height);
            this.rationsPopLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.rationsPopLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.popIndent2Image.addControl(this.rationsPopLabel);
            this.indent3Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a;
            this.indent3Image.Position = new Point(8, 0x85);
            this.indent3Image.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openAleExtension));
            this.indent3Image.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openAleExtensionOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openAleExtensionLeave));
            this.indent3Image.CustomTooltipID = 0x7f;
            this.panelImage.addControl(this.indent3Image);
            this.popIndent3Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            this.popIndent3Image.Position = new Point(6, 6);
            this.indent3Image.addControl(this.popIndent3Image);
            this.popImage3.Image = (Image) GFXLibrary.r_popularity_panel_icon_ale;
            this.popImage3.Position = new Point(0x27, -10);
            this.indent3Image.addControl(this.popImage3);
            this.aleRationsLine1Label.Text = "";
            this.aleRationsLine1Label.Color = ARGBColors.Black;
            this.aleRationsLine1Label.Position = new Point(0x22, 1);
            this.aleRationsLine1Label.Size = new Size((this.indent3Image.Width - 0x18) - 0x1f, this.indent3Image.Height);
            this.aleRationsLine1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.aleRationsLine1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.indent3Image.addControl(this.aleRationsLine1Label);
            this.alePopLabel.Text = "";
            this.alePopLabel.Color = ARGBColors.White;
            this.alePopLabel.DropShadowColor = ARGBColors.Black;
            this.alePopLabel.Position = new Point(0, 0);
            this.alePopLabel.Size = new Size(this.popIndent3Image.Width, this.popIndent3Image.Height);
            this.alePopLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.alePopLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.popIndent3Image.addControl(this.alePopLabel);
            this.indent4Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b;
            this.indent4Image.Position = new Point(8, 0xb6);
            this.indent4Image.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openHousingExtension));
            this.indent4Image.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openHousingExtensionOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openHousingExtensionLeave));
            this.indent4Image.CustomTooltipID = 0x81;
            this.panelImage.addControl(this.indent4Image);
            this.popIndent4Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            this.popIndent4Image.Position = new Point(6, 5);
            this.indent4Image.addControl(this.popIndent4Image);
            this.popImage4.Image = (Image) GFXLibrary.r_popularity_panel_icon_housing;
            this.popImage4.Position = new Point(0x27, -10);
            this.indent4Image.addControl(this.popImage4);
            this.housingLabel.Text = "";
            this.housingLabel.Color = ARGBColors.Black;
            this.housingLabel.Position = new Point(0x27, 0);
            this.housingLabel.Size = new Size((this.indent4Image.Width - 0x18) - 0x1f, this.indent4Image.Height);
            this.housingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.housingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.indent4Image.addControl(this.housingLabel);
            this.housingPopLabel.Text = "";
            this.housingPopLabel.Color = ARGBColors.White;
            this.housingPopLabel.DropShadowColor = ARGBColors.Black;
            this.housingPopLabel.Position = new Point(0, 0);
            this.housingPopLabel.Size = new Size(this.popIndent4Image.Width, this.popIndent4Image.Height);
            this.housingPopLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.housingPopLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.popIndent4Image.addControl(this.housingPopLabel);
            this.indent5Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b;
            this.indent5Image.Position = new Point(8, 0xe7);
            this.indent5Image.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openBuildingExtension));
            this.indent5Image.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openBuildingExtensionOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openBuildingExtensionLeave));
            this.indent5Image.CustomTooltipID = 0x83;
            this.panelImage.addControl(this.indent5Image);
            this.popIndent5Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            this.popIndent5Image.Position = new Point(6, 5);
            this.indent5Image.addControl(this.popIndent5Image);
            this.popImage5.Image = (Image) GFXLibrary.r_popularity_panel_icon_buildings;
            this.popImage5.Position = new Point(0x27, -10);
            this.indent5Image.addControl(this.popImage5);
            this.buildingPopLabel.Text = "";
            this.buildingPopLabel.Color = ARGBColors.White;
            this.buildingPopLabel.DropShadowColor = ARGBColors.Black;
            this.buildingPopLabel.Position = new Point(0, 0);
            this.buildingPopLabel.Size = new Size(this.popIndent5Image.Width, this.popIndent5Image.Height);
            this.buildingPopLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.buildingPopLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.popIndent5Image.addControl(this.buildingPopLabel);
            this.buildingsLabel.Text = "";
            this.buildingsLabel.Color = ARGBColors.Black;
            this.buildingsLabel.Position = new Point(0x27, 0);
            this.buildingsLabel.Size = new Size((this.indent5Image.Width - 0x18) - 0x1f, this.indent5Image.Height);
            this.buildingsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.buildingsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.indent5Image.addControl(this.buildingsLabel);
            this.indent6Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b;
            this.indent6Image.Position = new Point(8, 280);
            this.indent6Image.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openEventsExtension));
            this.indent6Image.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openEventsExtensionOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.openEventsExtensionLeave));
            this.indent6Image.CustomTooltipID = 0x85;
            this.panelImage.addControl(this.indent6Image);
            this.popIndent6Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            this.popIndent6Image.Position = new Point(6, 5);
            this.indent6Image.addControl(this.popIndent6Image);
            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_plague;
            this.popImage6.Position = new Point(0x30, 4);
            this.popImage6.Visible = false;
            this.indent6Image.addControl(this.popImage6);
            this.eventsPopLabel.Text = "";
            this.eventsPopLabel.Color = ARGBColors.White;
            this.eventsPopLabel.DropShadowColor = ARGBColors.Black;
            this.eventsPopLabel.Position = new Point(0, 0);
            this.eventsPopLabel.Size = new Size(this.popIndent5Image.Width, this.popIndent5Image.Height);
            this.eventsPopLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.eventsPopLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.popIndent6Image.addControl(this.eventsPopLabel);
            this.eventsLabel.Text = "";
            this.eventsLabel.Color = ARGBColors.Black;
            this.eventsLabel.Position = new Point(0x27, 0);
            this.eventsLabel.Size = new Size((this.indent6Image.Width - 0x18) - 0x1f, this.indent6Image.Height);
            this.eventsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.eventsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.indent6Image.addControl(this.eventsLabel);
            this.taxLowerButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
            this.taxLowerButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
            this.taxLowerButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
            this.taxLowerButton.Position = new Point(0x87, 40);
            this.taxLowerButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.taxLowerClicked), "VillageMapPanel_tax_lower");
            this.taxLowerButton.CustomTooltipID = 0x74;
            this.panelImage.addControl(this.taxLowerButton);
            this.taxHigherButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
            this.taxHigherButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
            this.taxHigherButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
            this.taxHigherButton.Position = new Point(0x9f, 40);
            this.taxHigherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.taxHigherClicked), "VillageMapPanel_tax_higher");
            this.taxHigherButton.CustomTooltipID = 0x73;
            this.panelImage.addControl(this.taxHigherButton);
            this.taxBar.setImages((Image) GFXLibrary.r_popularity_panel_colorbar_green_back, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_right, (Image) GFXLibrary.r_popularity_panel_colorbar_red_back, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_right);
            this.taxBar.Number = 0.0;
            this.taxBar.MaxValue = 10.0;
            this.taxBar.Position = new Point(0x8a, 0x3f);
            this.panelImage.addControl(this.taxBar);
            this.taxLowerButtonGlow.Image = (Image) GFXLibrary.tutorial_button_glow;
            this.taxLowerButtonGlow.Position = new Point(0x81, 0x22);
            this.taxLowerButtonGlow.Visible = false;
            this.panelImage.addControl(this.taxLowerButtonGlow);
            this.rationsLowerButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
            this.rationsLowerButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
            this.rationsLowerButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
            this.rationsLowerButton.Position = new Point(0x87, 0x59);
            this.rationsLowerButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rationsLowerClicked), "VillageMapPanel_rations_lower");
            this.rationsLowerButton.CustomTooltipID = 0x76;
            this.panelImage.addControl(this.rationsLowerButton);
            this.rationsHigherButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
            this.rationsHigherButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
            this.rationsHigherButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
            this.rationsHigherButton.Position = new Point(0x9f, 0x59);
            this.rationsHigherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.rationsHigherClicked), "VillageMapPanel_rations_higher");
            this.rationsHigherButton.CustomTooltipID = 0x75;
            this.panelImage.addControl(this.rationsHigherButton);
            this.rationsBar.setImages((Image) GFXLibrary.r_popularity_panel_colorbar_green_back, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_right, (Image) GFXLibrary.r_popularity_panel_colorbar_red_back, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_right);
            this.rationsBar.Number = 0.0;
            this.rationsBar.MaxValue = 60.0;
            this.rationsBar.Position = new Point(0x8a, 0x70);
            this.panelImage.addControl(this.rationsBar);
            this.aleLowerButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_minus_norm;
            this.aleLowerButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_minus_over;
            this.aleLowerButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_minus_in;
            this.aleLowerButton.Position = new Point(0x87, 0x8a);
            this.aleLowerButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aleLowerClicked), "VillageMapPanel_ale_lower");
            this.aleLowerButton.CustomTooltipID = 120;
            this.panelImage.addControl(this.aleLowerButton);
            this.aleHigherButton.ImageNorm = (Image) GFXLibrary.r_popularity_panel_but_plus_norm;
            this.aleHigherButton.ImageOver = (Image) GFXLibrary.r_popularity_panel_but_plus_over;
            this.aleHigherButton.ImageClick = (Image) GFXLibrary.r_popularity_panel_but_plus_in;
            this.aleHigherButton.Position = new Point(0x9f, 0x8a);
            this.aleHigherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aleHigherClicked), "VillageMapPanel_ale_higher");
            this.aleHigherButton.CustomTooltipID = 0x77;
            this.panelImage.addControl(this.aleHigherButton);
            this.aleRationsBar.setImages((Image) GFXLibrary.r_popularity_panel_colorbar_green_back, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_green_bar_right, (Image) GFXLibrary.r_popularity_panel_colorbar_red_back, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_left, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_mid, (Image) GFXLibrary.r_popularity_panel_colorbar_red_bar_right);
            this.aleRationsBar.Number = 0.0;
            this.aleRationsBar.MaxValue = 40.0;
            this.aleRationsBar.Position = new Point(0x8a, 0xa1);
            this.panelImage.addControl(this.aleRationsBar);
            this.headerImage.Image = (Image) GFXLibrary.r_popularity_bar_back_green;
            this.headerImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.headerClicked));
            this.headerImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.headerMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.headerMouseLeave));
            this.headerImage.Position = new Point(0, 0);
            this.headerImage.CustomTooltipID = 0x79;
            base.addControl(this.headerImage);
            this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_stand;
            this.immChangeImage.Position = new Point(90, 5);
            this.headerImage.addControl(this.immChangeImage);
            this.arrivesInLabel.Text = "";
            this.arrivesInLabel.Color = ARGBColors.Black;
            this.arrivesInLabel.Position = new Point(0x6a, 10);
            this.arrivesInLabel.Size = new Size(0x52, 20);
            this.arrivesInLabel.Visible = false;
            this.arrivesInLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.headerImage.addControl(this.arrivesInLabel);
            this.arrivesInTimeLabel.Text = "";
            this.arrivesInTimeLabel.Color = ARGBColors.Black;
            this.arrivesInTimeLabel.Position = new Point(0x6a, 0x1a);
            this.arrivesInTimeLabel.Size = new Size(0x52, 20);
            this.arrivesInTimeLabel.Visible = false;
            this.arrivesInTimeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.headerImage.addControl(this.arrivesInTimeLabel);
            this.popularityLabel.Text = "";
            this.popularityLabel.Color = ARGBColors.White;
            this.popularityLabel.DropShadowColor = ARGBColors.Black;
            this.popularityLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.popularityLabel.Position = new Point(5, 0x10);
            this.popularityLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.popularityLabel.Size = new Size(0x38, 0x18);
            this.headerImage.addControl(this.popularityLabel);
            this.headerGlowImage.Image = (Image) GFXLibrary.r_popularity_bar_back_glow;
            this.headerGlowImage.Position = new Point(0, 0);
            this.headerGlowImage.Visible = false;
            base.addControl(this.headerGlowImage);
            this.panelFaderImage.Image = (Image) GFXLibrary.r_popularity_panel_back;
            this.panelFaderImage.Position = new Point(0, 0);
            this.panelFaderImage.Alpha = 0f;
            this.panelImage.addControl(this.panelFaderImage);
            this.currentHeight = 1;
            this.targetHeight = 0;
            this.extensionHeaderLabel.Text = "";
            this.extensionHeaderLabel.Color = ARGBColors.Black;
            if (Program.mySettings.LanguageIdent == "pt")
            {
                this.extensionHeaderLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            }
            else
            {
                this.extensionHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            }
            this.extensionHeaderLabel.Position = new Point(0, 0x2d);
            this.extensionHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.extensionHeaderLabel.Size = new Size(0xc2, 0x18);
            this.extensionImage.addControl(this.extensionHeaderLabel);
            this.taxExtensionArea.Size = new Size(0xa7, 0x73);
            this.taxExtensionArea.Position = new Point(12, 0x2d);
            this.taxExtensionArea.Visible = false;
            this.extensionImage.addControl(this.taxExtensionArea);
            this.taxExtentionLabel.Text = SK.Text("VillageMapPanel_Daily_Finances", "Daily Finances");
            this.taxExtentionLabel.Color = ARGBColors.Black;
            this.taxExtentionLabel.Position = new Point(10, 0x19);
            this.taxExtentionLabel.Size = new Size(0xa7, 0x11);
            this.taxExtensionArea.addControl(this.taxExtentionLabel);
            if (Program.mySettings.LanguageIdent == "pt")
            {
                this.taxDayLabel.Text = SK.Text("VillageMapPanel_Tax_Income", "Tax Income");
                this.taxDayLabel.Color = ARGBColors.Black;
                this.taxDayLabel.Position = new Point(10, 0x29);
                this.taxDayLabel.Size = new Size(0xa7, 0x11);
                this.taxExtensionArea.addControl(this.taxDayLabel);
                this.taxDayValueLabel.Text = "";
                this.taxDayValueLabel.Color = ARGBColors.Black;
                this.taxDayValueLabel.Position = new Point(100, 0x35);
                this.taxDayValueLabel.Size = new Size((0xa7 - this.taxDayValueLabel.X) - 10, 0x11);
                this.taxDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.taxExtensionArea.addControl(this.taxDayValueLabel);
                this.parishTaxDayLabel.Text = SK.Text("VillageMapPanel_Parish_Tithe", "Parish Tithe");
                this.parishTaxDayLabel.Color = ARGBColors.Black;
                this.parishTaxDayLabel.Position = new Point(10, 0x42);
                this.parishTaxDayLabel.Size = new Size(0xa7, 0x11);
                this.taxExtensionArea.addControl(this.parishTaxDayLabel);
                this.parishTaxDayValueLabel.Text = "";
                this.parishTaxDayValueLabel.Color = ARGBColors.Black;
                this.parishTaxDayValueLabel.Position = new Point(100, 0x42);
                this.parishTaxDayValueLabel.Size = new Size((0xa7 - this.taxDayValueLabel.X) - 10, 0x11);
                this.parishTaxDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.taxExtensionArea.addControl(this.parishTaxDayValueLabel);
            }
            else
            {
                this.taxDayLabel.Text = SK.Text("VillageMapPanel_Tax_Income", "Tax Income");
                this.taxDayLabel.Color = ARGBColors.Black;
                this.taxDayLabel.Position = new Point(10, 0x2d);
                this.taxDayLabel.Size = new Size(0xa7, 0x11);
                this.taxExtensionArea.addControl(this.taxDayLabel);
                this.taxDayValueLabel.Text = "";
                this.taxDayValueLabel.Color = ARGBColors.Black;
                this.taxDayValueLabel.Position = new Point(100, 0x2d);
                this.taxDayValueLabel.Size = new Size((0xa7 - this.taxDayValueLabel.X) - 10, 0x11);
                this.taxDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.taxExtensionArea.addControl(this.taxDayValueLabel);
                this.parishTaxDayLabel.Text = SK.Text("VillageMapPanel_Parish_Tithe", "Parish Tithe");
                this.parishTaxDayLabel.Color = ARGBColors.Black;
                this.parishTaxDayLabel.Position = new Point(10, 0x3d);
                this.parishTaxDayLabel.Size = new Size(0xa7, 0x11);
                this.taxExtensionArea.addControl(this.parishTaxDayLabel);
                this.parishTaxDayValueLabel.Text = "";
                this.parishTaxDayValueLabel.Color = ARGBColors.Black;
                this.parishTaxDayValueLabel.Position = new Point(100, 0x3d);
                this.parishTaxDayValueLabel.Size = new Size((0xa7 - this.taxDayValueLabel.X) - 10, 0x11);
                this.parishTaxDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.taxExtensionArea.addControl(this.parishTaxDayValueLabel);
            }
            this.rationsExtensionArea.Size = new Size(0xa7, 0x73);
            this.rationsExtensionArea.Position = new Point(12, 0x2d);
            this.rationsExtensionArea.Visible = false;
            this.extensionImage.addControl(this.rationsExtensionArea);
            if (((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "tr")) || ((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "pt")))
            {
                this.rationsDayLabel.Text = SK.Text("VillageMapPanel_Food_Eaten", "Food Eaten Per Day") + ":";
                this.rationsDayLabel.Color = ARGBColors.Black;
                this.rationsDayLabel.Position = new Point(5, 0x13);
                this.rationsDayLabel.Size = new Size(0xa7, 0x11);
                this.rationsDayLabel.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                this.rationsExtensionArea.addControl(this.rationsDayLabel);
                this.rationsDayValueLabel.Text = "";
                this.rationsDayValueLabel.Color = ARGBColors.Black;
                this.rationsDayValueLabel.Position = new Point(100, 0x1c);
                this.rationsDayValueLabel.Size = new Size((0xa7 - this.rationsDayValueLabel.X) - 10, 0x11);
                this.rationsDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.rationsDayValueLabel.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                this.rationsExtensionArea.addControl(this.rationsDayValueLabel);
                this.rationsDay2Label.Text = SK.Text("VillageMapPanel_Food_Made", "Food Made Per Day") + ":";
                this.rationsDay2Label.Color = ARGBColors.Black;
                this.rationsDay2Label.Position = new Point(5, 0x27);
                this.rationsDay2Label.Size = new Size(0xa7, 0x11);
                this.rationsDay2Label.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                this.rationsExtensionArea.addControl(this.rationsDay2Label);
                this.rationsDay2ValueLabel.Text = "";
                this.rationsDay2ValueLabel.Color = ARGBColors.Black;
                this.rationsDay2ValueLabel.Position = new Point(100, 0x30);
                this.rationsDay2ValueLabel.Size = new Size((0xa7 - this.rationsDay2ValueLabel.X) - 10, 0x11);
                this.rationsDay2ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.rationsDay2ValueLabel.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                this.rationsExtensionArea.addControl(this.rationsDay2ValueLabel);
                this.foodTypesEatenLabel.Text = SK.Text("VillageMapPanel_Types_Fully_Eaten", "Types Fully Eaten");
                this.foodTypesEatenLabel.Color = ARGBColors.Black;
                this.foodTypesEatenLabel.Position = new Point(5, 0x3d);
                this.foodTypesEatenLabel.Size = new Size(0xa7, 0x11);
                this.foodTypesEatenLabel.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                this.rationsExtensionArea.addControl(this.foodTypesEatenLabel);
                this.numFoodTypesEatenLabel.Text = "0";
                this.numFoodTypesEatenLabel.Color = ARGBColors.Black;
                this.numFoodTypesEatenLabel.Position = new Point(100, 70);
                this.numFoodTypesEatenLabel.Size = new Size((0xa7 - this.numFoodTypesEatenLabel.X) - 10, 0x11);
                this.numFoodTypesEatenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.numFoodTypesEatenLabel.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                this.rationsExtensionArea.addControl(this.numFoodTypesEatenLabel);
            }
            else
            {
                this.rationsDayLabel.Text = SK.Text("VillageMapPanel_Food_Eaten", "Food Eaten Per Day") + ":";
                this.rationsDayLabel.Color = ARGBColors.Black;
                this.rationsDayLabel.Position = new Point(10, 0x19);
                this.rationsDayLabel.Size = new Size(0xa7, 0x11);
                this.rationsExtensionArea.addControl(this.rationsDayLabel);
                this.rationsDayValueLabel.Text = "";
                this.rationsDayValueLabel.Color = ARGBColors.Black;
                this.rationsDayValueLabel.Position = new Point(100, 0x19);
                this.rationsDayValueLabel.Size = new Size((0xa7 - this.rationsDayValueLabel.X) - 10, 0x11);
                this.rationsDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.rationsExtensionArea.addControl(this.rationsDayValueLabel);
                this.rationsDay2Label.Text = SK.Text("VillageMapPanel_Food_Made", "Food Made Per Day") + ":";
                this.rationsDay2Label.Color = ARGBColors.Black;
                this.rationsDay2Label.Position = new Point(10, 0x29);
                this.rationsDay2Label.Size = new Size(0xa7, 0x11);
                this.rationsExtensionArea.addControl(this.rationsDay2Label);
                this.rationsDay2ValueLabel.Text = "";
                this.rationsDay2ValueLabel.Color = ARGBColors.Black;
                this.rationsDay2ValueLabel.Position = new Point(100, 0x29);
                this.rationsDay2ValueLabel.Size = new Size((0xa7 - this.rationsDay2ValueLabel.X) - 10, 0x11);
                this.rationsDay2ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.rationsExtensionArea.addControl(this.rationsDay2ValueLabel);
                this.foodTypesEatenLabel.Text = SK.Text("VillageMapPanel_Types_Fully_Eaten", "Types Fully Eaten");
                this.foodTypesEatenLabel.Color = ARGBColors.Black;
                this.foodTypesEatenLabel.Position = new Point(10, 0x39);
                this.foodTypesEatenLabel.Size = new Size(0xa7, 0x11);
                this.rationsExtensionArea.addControl(this.foodTypesEatenLabel);
                this.numFoodTypesEatenLabel.Text = "0";
                this.numFoodTypesEatenLabel.Color = ARGBColors.Black;
                this.numFoodTypesEatenLabel.Position = new Point(100, 0x39);
                this.numFoodTypesEatenLabel.Size = new Size((0xa7 - this.numFoodTypesEatenLabel.X) - 10, 0x11);
                this.numFoodTypesEatenLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.rationsExtensionArea.addControl(this.numFoodTypesEatenLabel);
            }
            this.aleExtensionArea.Size = new Size(0xa7, 0x73);
            this.aleExtensionArea.Position = new Point(12, 0x2d);
            this.aleExtensionArea.Visible = false;
            this.extensionImage.addControl(this.aleExtensionArea);
            if (((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "tr")) || ((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "pt")))
            {
                this.aleRationsDayLabel.Text = SK.Text("VillageMapPanel_Ale_Drunk_Per_Day", "Ale Drank Per Day") + ":";
                this.aleRationsDayLabel.Color = ARGBColors.Black;
                this.aleRationsDayLabel.Position = new Point(10, 0x19);
                this.aleRationsDayLabel.Size = new Size(0xa7, 0x11);
                this.aleExtensionArea.addControl(this.aleRationsDayLabel);
                this.aleRationsDayValueLabel.Text = "";
                this.aleRationsDayValueLabel.Color = ARGBColors.Black;
                this.aleRationsDayValueLabel.Position = new Point(100, 0x24);
                this.aleRationsDayValueLabel.Size = new Size((0xa7 - this.aleRationsDayValueLabel.X) - 10, 0x11);
                this.aleRationsDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.aleExtensionArea.addControl(this.aleRationsDayValueLabel);
                this.aleRationsDay2Label.Text = SK.Text("VillageMapPanel_Ale_Made_Per_Day", "Ale Made Per Day") + ":";
                this.aleRationsDay2Label.Color = ARGBColors.Black;
                this.aleRationsDay2Label.Position = new Point(10, 0x31);
                this.aleRationsDay2Label.Size = new Size(0xa7, 0x11);
                this.aleExtensionArea.addControl(this.aleRationsDay2Label);
                this.aleRationsDay2ValueLabel.Text = "";
                this.aleRationsDay2ValueLabel.Color = ARGBColors.Black;
                this.aleRationsDay2ValueLabel.Position = new Point(100, 60);
                this.aleRationsDay2ValueLabel.Size = new Size((0xa7 - this.aleRationsDay2ValueLabel.X) - 10, 0x11);
                this.aleRationsDay2ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.aleExtensionArea.addControl(this.aleRationsDay2ValueLabel);
            }
            else
            {
                this.aleRationsDayLabel.Text = SK.Text("VillageMapPanel_Ale_Drunk_Per_Day", "Ale Drank Per Day") + ":";
                this.aleRationsDayLabel.Color = ARGBColors.Black;
                this.aleRationsDayLabel.Position = new Point(10, 0x19);
                this.aleRationsDayLabel.Size = new Size(0xa7, 0x11);
                this.aleExtensionArea.addControl(this.aleRationsDayLabel);
                this.aleRationsDayValueLabel.Text = "";
                this.aleRationsDayValueLabel.Color = ARGBColors.Black;
                this.aleRationsDayValueLabel.Position = new Point(100, 0x19);
                this.aleRationsDayValueLabel.Size = new Size((0xa7 - this.aleRationsDayValueLabel.X) - 10, 0x11);
                this.aleRationsDayValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.aleExtensionArea.addControl(this.aleRationsDayValueLabel);
                this.aleRationsDay2Label.Text = SK.Text("VillageMapPanel_Ale_Made_Per_Day", "Ale Made Per Day") + ":";
                this.aleRationsDay2Label.Color = ARGBColors.Black;
                this.aleRationsDay2Label.Position = new Point(10, 0x29);
                this.aleRationsDay2Label.Size = new Size(0xa7, 0x11);
                this.aleExtensionArea.addControl(this.aleRationsDay2Label);
                this.aleRationsDay2ValueLabel.Text = "";
                this.aleRationsDay2ValueLabel.Color = ARGBColors.Black;
                this.aleRationsDay2ValueLabel.Position = new Point(100, 0x29);
                this.aleRationsDay2ValueLabel.Size = new Size((0xa7 - this.aleRationsDay2ValueLabel.X) - 10, 0x11);
                this.aleRationsDay2ValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.aleExtensionArea.addControl(this.aleRationsDay2ValueLabel);
            }
            this.buildingExtensionArea.Size = new Size(0xa7, 0x73);
            this.buildingExtensionArea.Position = new Point(12, 0x2d);
            this.buildingExtensionArea.Visible = false;
            this.extensionImage.addControl(this.buildingExtensionArea);
            if (((Program.mySettings.LanguageIdent == "pt") || (Program.mySettings.LanguageIdent == "pl")) || ((Program.mySettings.LanguageIdent == "tr") || (Program.mySettings.LanguageIdent == "it")))
            {
                this.positiveBuildingsHeader.Text = SK.Text("VillageMapPanel_Popular_Buildings", "Popular Buildings") + ":";
                this.positiveBuildingsHeader.Color = ARGBColors.Black;
                this.positiveBuildingsHeader.Position = new Point(10, 0x19);
                this.positiveBuildingsHeader.Size = new Size(0xa7, 0x11);
                this.buildingExtensionArea.addControl(this.positiveBuildingsHeader);
                this.positiveBuildingsLabel.Text = "";
                this.positiveBuildingsLabel.Color = ARGBColors.Black;
                this.positiveBuildingsLabel.Position = new Point(100, 0x24);
                this.positiveBuildingsLabel.Size = new Size((0xa7 - this.positiveBuildingsLabel.X) - 10, 0x11);
                this.positiveBuildingsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.buildingExtensionArea.addControl(this.positiveBuildingsLabel);
                this.negativeBuildingsHeader.Text = SK.Text("VillageMapPanel_Unpopular_Buildings", "Unpopular Buildings") + ":";
                this.negativeBuildingsHeader.Color = ARGBColors.Black;
                this.negativeBuildingsHeader.Position = new Point(10, 0x31);
                this.negativeBuildingsHeader.Size = new Size(0xa7, 0x11);
                this.buildingExtensionArea.addControl(this.negativeBuildingsHeader);
                this.negativeBuildingsLabel.Text = "";
                this.negativeBuildingsLabel.Color = ARGBColors.Black;
                this.negativeBuildingsLabel.Position = new Point(100, 60);
                this.negativeBuildingsLabel.Size = new Size((0xa7 - this.negativeBuildingsLabel.X) - 10, 0x11);
                this.negativeBuildingsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.buildingExtensionArea.addControl(this.negativeBuildingsLabel);
            }
            else
            {
                this.positiveBuildingsHeader.Text = SK.Text("VillageMapPanel_Popular_Buildings", "Popular Buildings") + ":";
                this.positiveBuildingsHeader.Color = ARGBColors.Black;
                this.positiveBuildingsHeader.Position = new Point(10, 0x19);
                this.positiveBuildingsHeader.Size = new Size(0xa7, 0x11);
                this.buildingExtensionArea.addControl(this.positiveBuildingsHeader);
                this.positiveBuildingsLabel.Text = "";
                this.positiveBuildingsLabel.Color = ARGBColors.Black;
                this.positiveBuildingsLabel.Position = new Point(100, 0x19);
                this.positiveBuildingsLabel.Size = new Size((0xa7 - this.positiveBuildingsLabel.X) - 10, 0x11);
                this.positiveBuildingsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.buildingExtensionArea.addControl(this.positiveBuildingsLabel);
                this.negativeBuildingsHeader.Text = SK.Text("VillageMapPanel_Unpopular_Buildings", "Unpopular Buildings") + ":";
                this.negativeBuildingsHeader.Color = ARGBColors.Black;
                this.negativeBuildingsHeader.Position = new Point(10, 0x29);
                this.negativeBuildingsHeader.Size = new Size(0xa7, 0x11);
                this.buildingExtensionArea.addControl(this.negativeBuildingsHeader);
                this.negativeBuildingsLabel.Text = "";
                this.negativeBuildingsLabel.Color = ARGBColors.Black;
                this.negativeBuildingsLabel.Position = new Point(100, 0x29);
                this.negativeBuildingsLabel.Size = new Size((0xa7 - this.negativeBuildingsLabel.X) - 10, 0x11);
                this.negativeBuildingsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.buildingExtensionArea.addControl(this.negativeBuildingsLabel);
            }
            this.housingExtensionArea.Size = new Size(0xa7, 0x73);
            this.housingExtensionArea.Position = new Point(12, 0x2d);
            this.housingExtensionArea.Visible = false;
            this.extensionImage.addControl(this.housingExtensionArea);
            if (Program.mySettings.LanguageIdent == "pt")
            {
                this.populationLabel.Text = SK.Text("VillageMapPanel_Population", "Population") + ":";
                this.populationLabel.Color = ARGBColors.Black;
                this.populationLabel.Position = new Point(10, 0x13);
                this.populationLabel.Size = new Size(0xa7, 0x11);
                this.housingExtensionArea.addControl(this.populationLabel);
                this.populationValueLabel.Text = "";
                this.populationValueLabel.Color = ARGBColors.Black;
                this.populationValueLabel.Position = new Point(100, 0x1c);
                this.populationValueLabel.Size = new Size((0xa7 - this.populationValueLabel.X) - 10, 0x11);
                this.populationValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.housingExtensionArea.addControl(this.populationValueLabel);
                this.housingCapacityLabel.Text = SK.Text("VillageMapPanel_Housing_Capacity", "Housing Capacity") + ":";
                this.housingCapacityLabel.Color = ARGBColors.Black;
                this.housingCapacityLabel.Position = new Point(10, 0x27);
                this.housingCapacityLabel.Size = new Size(0xa7, 0x11);
                this.housingExtensionArea.addControl(this.housingCapacityLabel);
                this.housingCapacityValueLabel.Text = "";
                this.housingCapacityValueLabel.Color = ARGBColors.Black;
                this.housingCapacityValueLabel.Position = new Point(100, 0x30);
                this.housingCapacityValueLabel.Size = new Size((0xa7 - this.housingCapacityValueLabel.X) - 10, 0x11);
                this.housingCapacityValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.housingExtensionArea.addControl(this.housingCapacityValueLabel);
                this.housingOccupancyLabel.Text = SK.Text("VillageMapPanel_Housing_Occupancy", "Housing Occupancy") + ":";
                this.housingOccupancyLabel.Color = ARGBColors.Black;
                this.housingOccupancyLabel.Position = new Point(10, 0x3d);
                this.housingOccupancyLabel.Size = new Size(0xa7, 0x11);
                this.housingExtensionArea.addControl(this.housingOccupancyLabel);
                this.housingOccupancyValueLabel.Text = "";
                this.housingOccupancyValueLabel.Color = ARGBColors.Black;
                this.housingOccupancyValueLabel.Position = new Point(100, 70);
                this.housingOccupancyValueLabel.Size = new Size((0xa7 - this.housingOccupancyValueLabel.X) - 10, 0x11);
                this.housingOccupancyValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.housingExtensionArea.addControl(this.housingOccupancyValueLabel);
            }
            else
            {
                this.populationLabel.Text = SK.Text("VillageMapPanel_Population", "Population") + ":";
                this.populationLabel.Color = ARGBColors.Black;
                this.populationLabel.Position = new Point(10, 0x19);
                this.populationLabel.Size = new Size(0xa7, 0x11);
                this.housingExtensionArea.addControl(this.populationLabel);
                this.populationValueLabel.Text = "";
                this.populationValueLabel.Color = ARGBColors.Black;
                this.populationValueLabel.Position = new Point(100, 0x19);
                this.populationValueLabel.Size = new Size((0xa7 - this.populationValueLabel.X) - 10, 0x11);
                this.populationValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.housingExtensionArea.addControl(this.populationValueLabel);
                this.housingCapacityLabel.Text = SK.Text("VillageMapPanel_Housing_Capacity", "Housing Capacity") + ":";
                this.housingCapacityLabel.Color = ARGBColors.Black;
                this.housingCapacityLabel.Position = new Point(10, 0x29);
                this.housingCapacityLabel.Size = new Size(0xa7, 0x11);
                this.housingExtensionArea.addControl(this.housingCapacityLabel);
                this.housingCapacityValueLabel.Text = "";
                this.housingCapacityValueLabel.Color = ARGBColors.Black;
                this.housingCapacityValueLabel.Position = new Point(100, 0x29);
                this.housingCapacityValueLabel.Size = new Size((0xa7 - this.housingCapacityValueLabel.X) - 10, 0x11);
                this.housingCapacityValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.housingExtensionArea.addControl(this.housingCapacityValueLabel);
                this.housingOccupancyLabel.Text = SK.Text("VillageMapPanel_Housing_Occupancy", "Housing Occupancy") + ":";
                this.housingOccupancyLabel.Color = ARGBColors.Black;
                this.housingOccupancyLabel.Position = new Point(10, 0x39);
                this.housingOccupancyLabel.Size = new Size(0xa7, 0x11);
                this.housingExtensionArea.addControl(this.housingOccupancyLabel);
                if ((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "it"))
                {
                    this.housingOccupancyValueLabel.Text = "";
                    this.housingOccupancyValueLabel.Color = ARGBColors.Black;
                    this.housingOccupancyValueLabel.Position = new Point(100, 0x44);
                    this.housingOccupancyValueLabel.Size = new Size((0xa7 - this.housingOccupancyValueLabel.X) - 10, 0x11);
                    this.housingOccupancyValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                    this.housingExtensionArea.addControl(this.housingOccupancyValueLabel);
                }
                else
                {
                    this.housingOccupancyValueLabel.Text = "";
                    this.housingOccupancyValueLabel.Color = ARGBColors.Black;
                    this.housingOccupancyValueLabel.Position = new Point(100, 0x39);
                    this.housingOccupancyValueLabel.Size = new Size((0xa7 - this.housingOccupancyValueLabel.X) - 10, 0x11);
                    this.housingOccupancyValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                    this.housingExtensionArea.addControl(this.housingOccupancyValueLabel);
                }
            }
            this.eventsExtensionArea.Size = new Size(0xa7, 0x73);
            this.eventsExtensionArea.Position = new Point(12, 0x2d);
            this.eventsExtensionArea.Visible = false;
            this.extensionImage.addControl(this.eventsExtensionArea);
            this.eventHeaderLabel.Text = SK.Text("VillageMapPanel_Events", "Events");
            this.eventHeaderLabel.Color = ARGBColors.Black;
            this.eventHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.eventHeaderLabel.Position = new Point(0, 0);
            this.eventHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.eventHeaderLabel.Size = new Size(0xc2, 0x18);
            this.eventsExtensionArea.addControl(this.eventHeaderLabel);
            this.eventCountLabel.Text = "0/0";
            this.eventCountLabel.Color = ARGBColors.Black;
            this.eventCountLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.eventCountLabel.Position = new Point(0x38, 0);
            this.eventCountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.eventCountLabel.Size = new Size(110, 0x18);
            this.eventsExtensionArea.addControl(this.eventCountLabel);
            this.eventPopImage.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            this.eventPopImage.Position = new Point(5, 0x12);
            this.eventsExtensionArea.addControl(this.eventPopImage);
            this.eventExtPopLabel.Text = "0";
            this.eventExtPopLabel.Color = ARGBColors.White;
            this.eventExtPopLabel.DropShadowColor = ARGBColors.Black;
            this.eventExtPopLabel.Position = new Point(0, 0);
            this.eventExtPopLabel.Size = new Size(this.eventPopImage.Width, this.eventPopImage.Height);
            this.eventExtPopLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.eventExtPopLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.eventPopImage.addControl(this.eventExtPopLabel);
            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_plague;
            this.eventTypeImage.Position = new Point(0x30, 0x12);
            this.eventsExtensionArea.addControl(this.eventTypeImage);
            this.eventBarImage.Image = (Image) GFXLibrary.r_popularity_panel_events_textbar_green;
            this.eventBarImage.Position = new Point(5, 0x2e);
            this.eventsExtensionArea.addControl(this.eventBarImage);
            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Test_Events", "Test Event");
            this.eventTitleLabel.Color = ARGBColors.White;
            this.eventTitleLabel.Position = new Point(5, 0);
            this.eventTitleLabel.Size = new Size(this.eventBarImage.Width - 10, this.eventBarImage.Height);
            this.eventTitleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.eventTitleLabel.Font = FontManager.GetFont("Arial", 6.5f, FontStyle.Regular);
            this.eventBarImage.addControl(this.eventTitleLabel);
            this.eventDaysLabel.Text = "00d";
            this.eventDaysLabel.Color = ARGBColors.Black;
            this.eventDaysLabel.Position = new Point(0, 0x3e);
            this.eventDaysLabel.Size = new Size(0x3f, 0x19);
            this.eventDaysLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.eventDaysLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.eventsExtensionArea.addControl(this.eventDaysLabel);
            this.eventTimeLabel.Text = "00h:00m:00s";
            this.eventTimeLabel.Color = ARGBColors.Black;
            this.eventTimeLabel.Position = new Point(0x40, 0x42);
            this.eventTimeLabel.Size = new Size(100, 0x19);
            this.eventTimeLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            this.eventTimeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.eventsExtensionArea.addControl(this.eventTimeLabel);
            this.eventLowerButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_left_norm;
            this.eventLowerButton.ImageOver = (Image) GFXLibrary.r_arrow_small_left_over;
            this.eventLowerButton.Position = new Point(0x45, 3);
            this.eventLowerButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.eventShownLowerClicked), "VillageMapPanel_events_lower");
            this.eventsExtensionArea.addControl(this.eventLowerButton);
            this.eventHigherButton.ImageNorm = (Image) GFXLibrary.r_arrow_small_right_norm;
            this.eventHigherButton.ImageOver = (Image) GFXLibrary.r_arrow_small_right_over;
            this.eventHigherButton.Position = new Point(0x8a, 3);
            this.eventHigherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.eventShownHigherClicked), "VillageMapPanel_events_higher");
            this.eventsExtensionArea.addControl(this.eventHigherButton);
            this.eventPopImage.Visible = false;
            this.eventTypeImage.Visible = false;
            this.eventBarImage.Visible = false;
            this.eventDaysLabel.Visible = false;
            this.eventTimeLabel.Visible = false;
            this.currentExtensionHeight = 1;
            this.targetExtensionHeight = 0;
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        private bool isExtensionOpen()
        {
            return (this.currentExtensionHeight == 0x7a);
        }

        public bool isHonourTabOpen()
        {
            return (this.targetInfo1Height == 0x14f);
        }

        public bool isInBuildingPanelOpen()
        {
            if (this.currentInBuildingHeight == 0)
            {
                return false;
            }
            return true;
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVillageMapPanelOnFoodTab()
        {
            return (this.currentTab == 2);
        }

        public bool isVillageMapPanelOnIndustryTab()
        {
            return (this.currentTab == 1);
        }

        public bool isVillageMapPanelOnPopularityBar()
        {
            return (this.currentHeight > 0);
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void moveBuildingClick()
        {
            if ((GameEngine.Instance.Village != null) && (this.selectedBuilding != null))
            {
                GameEngine.Instance.Village.startMoveBuildings(this.selectedBuilding);
            }
        }

        private void openAleExtension()
        {
            int num = 2;
            if (this.currentExtensionHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_open_ale");
            }
            else if (this.isExtensionOpen() && (this.extensionType != num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_extension_changed");
            }
            else if (this.isExtensionOpen() && (this.extensionType == num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_close_ale");
            }
            this.openExtension(2);
        }

        private void openAleExtensionLeave()
        {
            this.popImage3.Image = (Image) GFXLibrary.r_popularity_panel_icon_ale;
            this.indent3Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a;
        }

        private void openAleExtensionOver()
        {
            this.popImage3.Image = (Image) GFXLibrary.r_popularity_panel_icon_ale_over;
            this.indent3Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a_over;
        }

        private void openBuildingExtension()
        {
            int num = 4;
            if (this.currentExtensionHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_open_buildings_ext");
            }
            else if (this.isExtensionOpen() && (this.extensionType != num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_extension_changed");
            }
            else if (this.isExtensionOpen() && (this.extensionType == num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_close_buildings_ext");
            }
            this.openExtension(4);
        }

        private void openBuildingExtensionLeave()
        {
            this.indent5Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b;
            this.popImage5.Image = (Image) GFXLibrary.r_popularity_panel_icon_buildings;
        }

        private void openBuildingExtensionOver()
        {
            this.indent5Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b_over;
            this.popImage5.Image = (Image) GFXLibrary.r_popularity_panel_icon_buildings_over;
        }

        private void openBuildingTab()
        {
            this.closeTopGivers();
            this.closeInBuildingPanel();
            this.closePopularityPanel();
            this.closeInfo1Panel();
            this.targetBuildingHeight = 0x1a6;
            this.showExtras();
        }

        private void openEventsExtension()
        {
            int num = 5;
            if (this.currentExtensionHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_open_events");
            }
            else if (this.isExtensionOpen() && (this.extensionType != num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_extension_changed");
            }
            else if (this.isExtensionOpen() && (this.extensionType == num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_close_events");
            }
            this.openExtension(5);
        }

        private void openEventsExtensionLeave()
        {
            this.indent6Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b;
        }

        private void openEventsExtensionOver()
        {
            this.indent6Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b_over;
        }

        private void openExtension(int newType)
        {
            this.indent1Image.CustomTooltipID = 0x7b;
            this.indent2Image.CustomTooltipID = 0x7d;
            this.indent3Image.CustomTooltipID = 0x7f;
            this.indent4Image.CustomTooltipID = 0x81;
            this.indent5Image.CustomTooltipID = 0x83;
            this.indent6Image.CustomTooltipID = 0x85;
            if (this.currentExtensionHeight == 0)
            {
                this.extensionType = newType;
                this.targetExtensionHeight = 0x7a;
                this.initExtentsion(newType);
            }
            else if (this.isExtensionOpen() && (this.extensionType != newType))
            {
                this.extensionType = newType;
                this.initExtentsion(newType);
            }
            else if (this.isExtensionOpen() && (this.extensionType == newType))
            {
                this.targetExtensionHeight = 0;
            }
        }

        private void openHousingExtension()
        {
            int num = 3;
            if (this.currentExtensionHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_open_housing");
            }
            else if (this.isExtensionOpen() && (this.extensionType != num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_extension_changed");
            }
            else if (this.isExtensionOpen() && (this.extensionType == num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_close_housing");
            }
            this.openExtension(3);
        }

        private void openHousingExtensionLeave()
        {
            this.indent4Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b;
            this.popImage4.Image = (Image) GFXLibrary.r_popularity_panel_icon_housing;
        }

        private void openHousingExtensionOver()
        {
            this.indent4Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_b_over;
            this.popImage4.Image = (Image) GFXLibrary.r_popularity_panel_icon_housing_over;
        }

        private void openRationsExtension()
        {
            int num = 1;
            if (this.currentExtensionHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_open_rations");
            }
            else if (this.isExtensionOpen() && (this.extensionType != num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_extension_changed");
            }
            else if (this.isExtensionOpen() && (this.extensionType == num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_close_rations");
            }
            this.openExtension(1);
        }

        private void openRationsExtensionLeave()
        {
            this.indent2Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a;
            this.popImage2.Image = (Image) GFXLibrary.r_popularity_panel_icon_rations;
        }

        private void openRationsExtensionOver()
        {
            this.indent2Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a_over;
            this.popImage2.Image = (Image) GFXLibrary.r_popularity_panel_icon_rations_over;
        }

        private void openTaxExtension()
        {
            int num = 0;
            if (this.currentExtensionHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_open_tax");
            }
            else if (this.isExtensionOpen() && (this.extensionType != num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_extension_changed");
            }
            else if (this.isExtensionOpen() && (this.extensionType == num))
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_close_tax");
            }
            this.openExtension(0);
        }

        private void openTaxExtensionLeave()
        {
            this.indent1Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a;
            this.popImage1.Image = (Image) GFXLibrary.r_popularity_panel_icon_taxes;
        }

        private void openTaxExtensionOver()
        {
            this.indent1Image.Image = (Image) GFXLibrary.r_popularity_panel_indent_a_over;
            this.popImage1.Image = (Image) GFXLibrary.r_popularity_panel_icon_taxes_over;
        }

        private void openTopGivers()
        {
            this.closeBuildingPanel();
            this.closeInBuildingPanel();
            if (this.currentTopGiversHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_top_10");
                this.targetTopGiversHeight = 0x14f;
            }
            else if (this.currentTopGiversHeight == 0x14f)
            {
                GameEngine.Instance.playInterfaceSound("VillageMapPanel_top_10_close");
                this.closeTopGivers();
            }
        }

        private void placeBuildingClick()
        {
            CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
            int data = clickedControl.Data;
            if (data < 0x7c)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.startPlaceBuilding(data, false);
                    GameEngine.Instance.Village.startPlaceBuilding_ShowPanel(data, VillageBuildingsData.getBuildingName(data), true);
                    this.buildingBeingPlaced = true;
                    InterfaceMgr.Instance.closeTutorialWindow();
                }
            }
            else if (data == 0x7d0)
            {
                switch (this.currentTab)
                {
                    case 0x3ec:
                    case 0x3ed:
                        this.setBuildingTab(0x3e8);
                        return;

                    case 0x3ee:
                    case 0x3ef:
                    case 0x3f0:
                    case 0x3f1:
                    case 0x3f2:
                    case 0x457:
                        this.setBuildingTab(0x3e9);
                        return;

                    case 0x458:
                    case 0x459:
                    case 0x45a:
                    case 0x45b:
                    case 0x45c:
                        this.setBuildingTab(3);
                        return;
                }
                this.setBuildingTab(0);
            }
            else
            {
                this.setBuildingTab(data);
            }
        }

        private void placeBuildingMouseLeave()
        {
            if (!this.buildingBeingPlaced)
            {
                this.clearBuildingInfo();
            }
        }

        private void placeBuildingMouseOver()
        {
            if (!this.buildingBeingPlaced && (base.OverControl != null))
            {
                CustomSelfDrawPanel.CSDButton overControl = (CustomSelfDrawPanel.CSDButton) base.OverControl;
                int data = overControl.Data;
                if (data >= 0x7c)
                {
                    switch (data)
                    {
                        case 0x3e8:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Religious_Buildings", "Religious Buildings"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3e9:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Decorative_Buildings", "Decorative Buildings"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3ea:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Justice_Buildings", "Justice Buildings"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3eb:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Entertainment_Buildings", "Entertainment Buildings"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3ec:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Small_Shrines", "Small Shrines"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3ed:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Large_Shrines", "Large Shrines"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3ee:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Formal_Gardens", "Formal Gardens"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3ef:
                        case 0x3f1:
                            return;

                        case 0x3f0:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Flower_Beds", "Flower Beds"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x3f2:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Gilded_Statues", "Gilded Statues"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x457:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Stone_Statues", "Stone Statues"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;

                        case 0x7d0:
                            this.setBuildingInfo(SK.Text("VillageMapPanel_Back", "Back"), 0, 0, 0, 0, 0, "", -1, -1);
                            return;
                    }
                }
                else if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.startPlaceBuilding_ShowPanel(data, VillageBuildingsData.getBuildingName(data), false);
                }
            }
        }

        private void PopularityPanelUpdate()
        {
            bool flag = false;
            if (!this.m_villageIsCapital)
            {
                if (this.currentHeight != this.targetHeight)
                {
                    if (this.currentHeight < this.targetHeight)
                    {
                        this.currentHeight += 50;
                        if (this.currentHeight > this.targetHeight)
                        {
                            this.currentHeight = this.targetHeight;
                        }
                    }
                    else
                    {
                        this.currentHeight -= 50;
                        if (this.currentHeight < this.targetHeight)
                        {
                            this.currentHeight = this.targetHeight;
                        }
                    }
                    this.panelImage.Y = 0x19 - (0x14f - this.currentHeight);
                    this.panelImage.ClipRect = new Rectangle(0, 0x14f - this.currentHeight, this.panelImage.Width, this.currentHeight);
                    flag = true;
                    float num = ((((float) this.currentHeight) / 335f) * 2f) - 1f;
                    if (num < 0f)
                    {
                        num = 0f;
                    }
                    this.panelFaderImage.Alpha = 1f - num;
                }
                if (this.currentHeight == 0)
                {
                    this.panelImage.Visible = false;
                }
                else
                {
                    this.panelImage.Visible = true;
                }
                if ((this.currentExtensionHeight != this.targetExtensionHeight) || flag)
                {
                    if (this.currentExtensionHeight < this.targetExtensionHeight)
                    {
                        this.currentExtensionHeight += 50;
                        if (this.currentExtensionHeight > this.targetExtensionHeight)
                        {
                            this.currentExtensionHeight = this.targetExtensionHeight;
                        }
                    }
                    else
                    {
                        this.currentExtensionHeight -= 50;
                        if (this.currentExtensionHeight <= this.targetExtensionHeight)
                        {
                            this.currentExtensionHeight = this.targetExtensionHeight;
                            if (this.reopenExtension)
                            {
                                this.targetExtensionHeight = 0x7a;
                                this.reopenExtension = false;
                                this.extensionType = this.nextExtensionType;
                                this.initExtentsion(this.extensionType);
                            }
                        }
                    }
                    this.extensionImage.Y = this.currentHeight - (0x90 - this.currentExtensionHeight);
                    this.extensionImage.ClipRect = new Rectangle(0, 0x90 - this.currentExtensionHeight, this.extensionImage.Width, this.currentExtensionHeight);
                    flag = true;
                }
                if (this.currentExtensionHeight == 0)
                {
                    this.extensionImage.Visible = false;
                }
                else
                {
                    this.extensionImage.Visible = true;
                }
                int num2 = this.calcBuildTabYPos();
                this.buildHeaderArea.Y = num2;
                if ((this.currentBuildingHeight != this.targetBuildingHeight) || flag)
                {
                    if (this.currentBuildingHeight < this.targetBuildingHeight)
                    {
                        this.currentBuildingHeight += 50;
                        if (this.currentBuildingHeight > this.targetBuildingHeight)
                        {
                            this.currentBuildingHeight = this.targetBuildingHeight;
                        }
                    }
                    else
                    {
                        this.currentBuildingHeight -= 50;
                        if (this.currentBuildingHeight <= this.targetBuildingHeight)
                        {
                            this.currentBuildingHeight = this.targetBuildingHeight;
                            if (this.targetBuildingHeight == 0)
                            {
                                this.setBuildingTab(-1);
                            }
                        }
                    }
                    this.buildPanelImage.Y = (0x19 - (0x1a6 - this.currentBuildingHeight)) + num2;
                    this.buildPanelImage.ClipRect = new Rectangle(0, 0x1a6 - this.currentBuildingHeight, this.buildPanelImage.Width, this.currentBuildingHeight);
                    flag = true;
                    float num3 = ((((float) this.currentBuildingHeight) / 422f) * 2f) - 1f;
                    if (num3 < 0f)
                    {
                        num3 = 0f;
                    }
                    this.buildPanelFaderImage.Alpha = 1f - num3;
                }
                if (this.currentBuildingHeight == 0)
                {
                    this.buildPanelImage.Visible = false;
                }
                else
                {
                    this.buildPanelImage.Visible = true;
                }
                int num4 = this.calcInfoTabYPos();
                int num5 = (num4 + Math.Max(this.currentInfo1Height + 0x19, 0x37)) - 0x37;
                this.info1HeaderPanelImage.Y = num5;
                if ((this.currentInBuildingHeight != this.targetInBuildingHeight) || flag)
                {
                    if (this.currentInBuildingHeight < this.targetInBuildingHeight)
                    {
                        this.currentInBuildingHeight += 50;
                        if (this.currentInBuildingHeight > this.targetInBuildingHeight)
                        {
                            this.currentInBuildingHeight = this.targetInBuildingHeight;
                        }
                    }
                    else
                    {
                        this.currentInBuildingHeight -= 50;
                        if (this.currentInBuildingHeight <= this.targetInBuildingHeight)
                        {
                            this.currentInBuildingHeight = this.targetInBuildingHeight;
                            if (this.targetInBuildingHeight == 0)
                            {
                                this.inBuildingHeaderPanelImage.Visible = false;
                                this.inBuildingPanelImage.Visible = false;
                            }
                        }
                    }
                    this.inBuildingPanelImage.Y = (0x19 - (0x14f - this.currentInBuildingHeight)) + num5;
                    this.inBuildingPanelImage.Y += 0x37;
                    this.inBuildingHeaderPanelImage.Y = num5 + 0x37;
                    this.inBuildingPanelImage.ClipRect = new Rectangle(0, 0x14f - this.currentInBuildingHeight, this.inBuildingPanelImage.Width, this.currentInBuildingHeight);
                    flag = true;
                    float num6 = ((((float) this.currentInBuildingHeight) / 335f) * 2f) - 1f;
                    if (num6 < 0f)
                    {
                        num6 = 0f;
                    }
                    this.inBuildingFaderImage.Alpha = 1f - num6;
                }
                if (this.currentInBuildingHeight == 0)
                {
                    this.inBuildingPanelImage.Visible = false;
                }
                else
                {
                    this.inBuildingPanelImage.Visible = true;
                }
                this.info1HeaderPanelImage.Y = num4;
                if ((this.currentInfo1Height != this.targetInfo1Height) || flag)
                {
                    if (this.currentInfo1Height < this.targetInfo1Height)
                    {
                        this.currentInfo1Height += 50;
                        if (this.currentInfo1Height > this.targetInfo1Height)
                        {
                            this.currentInfo1Height = this.targetInfo1Height;
                        }
                    }
                    else
                    {
                        this.currentInfo1Height -= 50;
                        if (this.currentInfo1Height <= this.targetInfo1Height)
                        {
                            this.currentInfo1Height = this.targetInfo1Height;
                        }
                    }
                    this.info1PanelImage.Y = (0x19 - (0x14f - this.currentInfo1Height)) + num4;
                    int currentResolution = GameEngine.Instance.CurrentResolution;
                    this.info1PanelImage.ClipRect = new Rectangle(0, 0x14f - this.currentInfo1Height, this.info1PanelImage.Width, this.currentInfo1Height);
                    flag = true;
                    float num7 = ((((float) this.currentInfo1Height) / 335f) * 2f) - 1f;
                    if (num7 < 0f)
                    {
                        num7 = 0f;
                    }
                    this.info1PanelFaderImage.Alpha = 1f - num7;
                }
                if (this.currentInfo1Height == 0)
                {
                    this.info1PanelImage.Visible = false;
                }
                else
                {
                    this.info1PanelImage.Visible = true;
                }
                int num8 = this.calcInfo2TabYPos();
                this.info2HeaderPanelImage.Y = num8;
                this.info3HeaderPanelImage.Y = num8 + 0x37;
                this.extrasHeaderPanelImage.Y = num4;
            }
            else
            {
                int num9 = this.calcCapitalBuildTabYPos();
                this.buildHeaderArea.Y = num9;
                if ((this.currentBuildingHeight != this.targetBuildingHeight) || flag)
                {
                    if (this.currentBuildingHeight < this.targetBuildingHeight)
                    {
                        this.currentBuildingHeight += 50;
                        if (this.currentBuildingHeight > this.targetBuildingHeight)
                        {
                            this.currentBuildingHeight = this.targetBuildingHeight;
                        }
                    }
                    else
                    {
                        this.currentBuildingHeight -= 50;
                        if (this.currentBuildingHeight <= this.targetBuildingHeight)
                        {
                            this.currentBuildingHeight = this.targetBuildingHeight;
                            if (this.targetBuildingHeight == 0)
                            {
                                this.setBuildingTab(-1);
                            }
                        }
                    }
                    this.buildPanelImage.Y = (0x19 - (0x1a6 - this.currentBuildingHeight)) + num9;
                    this.buildPanelImage.ClipRect = new Rectangle(0, 0x1a6 - this.currentBuildingHeight, this.buildPanelImage.Width, this.currentBuildingHeight);
                    flag = true;
                    float num10 = ((((float) this.currentBuildingHeight) / 422f) * 2f) - 1f;
                    if (num10 < 0f)
                    {
                        num10 = 0f;
                    }
                    this.buildPanelFaderImage.Alpha = 1f - num10;
                }
                if (this.currentBuildingHeight == 0)
                {
                    this.buildPanelImage.Visible = false;
                }
                else
                {
                    this.buildPanelImage.Visible = true;
                }
                int num11 = this.calcTop10YPos();
                int num12 = num11;
                this.capitalTop10HeaderImage.Y = num11;
                if (((GameEngine.Instance.Village != null) && (this.currentTopGiversHeight > 0)) && (!GameEngine.Instance.World.isRegionCapital(GameEngine.Instance.Village.VillageID) && (this.targetTopGiversHeight > 0)))
                {
                    this.targetTopGiversHeight = 0;
                    this.currentTopGiversHeight = 1;
                }
                if ((this.currentTopGiversHeight != this.targetTopGiversHeight) || flag)
                {
                    if (this.currentTopGiversHeight < this.targetTopGiversHeight)
                    {
                        this.currentTopGiversHeight += 50;
                        if (this.currentTopGiversHeight > this.targetTopGiversHeight)
                        {
                            this.currentTopGiversHeight = this.targetTopGiversHeight;
                        }
                    }
                    else
                    {
                        this.currentTopGiversHeight -= 50;
                        if (this.currentTopGiversHeight <= this.targetTopGiversHeight)
                        {
                            this.currentTopGiversHeight = this.targetTopGiversHeight;
                        }
                    }
                    this.capitalTop10PanelImage.Y = (0x19 - (0x1a6 - this.currentTopGiversHeight)) + num11;
                    this.capitalTop10PanelImage.ClipRect = new Rectangle(0, 0x1a6 - this.currentTopGiversHeight, this.capitalTop10PanelImage.Width, this.currentTopGiversHeight);
                    flag = true;
                    float num13 = ((((float) this.currentTopGiversHeight) / 422f) * 2f) - 1f;
                    if (num13 < 0f)
                    {
                        num13 = 0f;
                    }
                    this.capitalTop10PanelFaderImage.Alpha = 1f - num13;
                }
                if (this.currentTopGiversHeight == 0)
                {
                    this.capitalTop10PanelImage.Visible = false;
                }
                else
                {
                    this.capitalTop10PanelImage.Visible = true;
                    num12 += Math.Max(this.currentTopGiversHeight - 0x37, 0);
                }
                if ((this.currentInBuildingHeight != this.targetInBuildingHeight) || flag)
                {
                    if (this.currentInBuildingHeight < this.targetInBuildingHeight)
                    {
                        this.currentInBuildingHeight += 50;
                        if (this.currentInBuildingHeight > this.targetInBuildingHeight)
                        {
                            this.currentInBuildingHeight = this.targetInBuildingHeight;
                        }
                    }
                    else
                    {
                        this.currentInBuildingHeight -= 50;
                        if (this.currentInBuildingHeight <= this.targetInBuildingHeight)
                        {
                            this.currentInBuildingHeight = this.targetInBuildingHeight;
                            if (this.targetInBuildingHeight == 0)
                            {
                                this.inBuildingHeaderPanelImage.Visible = false;
                                this.inBuildingPanelImage.Visible = false;
                            }
                        }
                    }
                    this.inBuildingPanelImage.Y = (0x19 - (0x14f - this.currentInBuildingHeight)) + num12;
                    this.inBuildingPanelImage.Y += 0x37;
                    this.inBuildingHeaderPanelImage.Y = num12 + 0x37;
                    this.inBuildingPanelImage.ClipRect = new Rectangle(0, 0x14f - this.currentInBuildingHeight, this.inBuildingPanelImage.Width, this.currentInBuildingHeight);
                    flag = true;
                    float num14 = ((((float) this.currentInBuildingHeight) / 290f) * 2f) - 1f;
                    if (num14 < 0f)
                    {
                        num14 = 0f;
                    }
                    this.inBuildingFaderImage.Alpha = 1f - num14;
                }
                if (this.currentInBuildingHeight == 0)
                {
                    this.inBuildingPanelImage.Visible = false;
                }
                else
                {
                    this.inBuildingPanelImage.Visible = true;
                }
            }
            if (flag)
            {
                base.Invalidate();
            }
        }

        private void rationsHigherClicked()
        {
            if (!this.ViewOnly)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeStats(0, 1, 0);
                }
                if (this.isExtensionOpen())
                {
                    this.extensionType = 1;
                    this.initExtentsion(this.extensionType);
                }
                else
                {
                    this.openExtension(1);
                }
            }
        }

        private void rationsLowerClicked()
        {
            if (!this.ViewOnly)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeStats(0, -1, 0);
                }
                if (this.isExtensionOpen())
                {
                    this.extensionType = 1;
                    this.initExtentsion(this.extensionType);
                }
                else
                {
                    this.openExtension(1);
                }
            }
        }

        public void refreshCurrentTab()
        {
            this.setBuildingTab(this.currentTab);
        }

        private void resetBuildingIcons()
        {
            this.currentBuildingIcon = 0;
            this.building1Button.Visible = false;
            this.building2Button.Visible = false;
            this.building3Button.Visible = false;
            this.building4Button.Visible = false;
            this.building5Button.Visible = false;
            this.building6Button.Visible = false;
            this.building7Button.Visible = false;
            this.building8Button.Visible = false;
            this.building1Image.Visible = false;
            this.building2Image.Visible = false;
            this.building3Image.Visible = false;
            this.building4Image.Visible = false;
            this.building5Image.Visible = false;
            this.building6Image.Visible = false;
            this.building7Image.Visible = false;
            this.building8Image.Visible = false;
        }

        public void run()
        {
            if (this.taxLowerButtonGlow.Visible)
            {
                this.glowFade++;
                if (this.glowFade < 20)
                {
                    this.taxLowerButtonGlow.Alpha = ((((float) this.glowFade) / 20f) / 2f) + 0.5f;
                }
                else if (this.glowFade < 40)
                {
                    this.taxLowerButtonGlow.Alpha = ((((float) (40 - this.glowFade)) / 20f) / 2f) + 0.5f;
                }
                else
                {
                    this.taxLowerButtonGlow.Alpha = 0.5f;
                    this.glowFade = 0;
                }
                this.taxLowerButtonGlow.invalidate();
            }
            this.PopularityPanelUpdate();
            this.InBuildingPanelUpdate();
        }

        public void setBuildingInfo(string buildingName, int woodCost, int stoneCost, int clayCost, int goldCost, int flagsNeeded, string buildTime, int buildingType, int realBuildingType)
        {
            int matchedCard = -1;
            if (CardTypes.isFreeBuildingPlacement(GameEngine.Instance.World.UserCardData, realBuildingType, ref matchedCard))
            {
                woodCost = 0;
                stoneCost = 0;
                clayCost = 0;
                goldCost = 0;
                flagsNeeded = 0;
                buildTime = "";
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            VillageMap village = GameEngine.Instance.Village;
            this.clearBuildingInfo();
            this.buildTypeLabel.Text = buildingName;
            this.buildTimeLabel.Text = buildTime;
            this.buildTypeLabel.Visible = true;
            this.buildTimeLabel.Visible = true;
            if (woodCost > 0)
            {
                this.buildWoodLabel.Text = woodCost.ToString("N", nFI);
                this.buildWoodLabel.Visible = true;
                this.buildWoodLabel.Color = ARGBColors.Black;
                if ((village != null) && (village.getResourceLevel(6) < woodCost))
                {
                    this.buildWoodLabel.Color = ARGBColors.Red;
                }
            }
            if (stoneCost > 0)
            {
                this.buildStoneLabel.Text = stoneCost.ToString("N", nFI);
                this.buildStoneLabel.Visible = true;
                this.buildStoneLabel.Color = ARGBColors.Black;
                if ((village != null) && (village.getResourceLevel(7) < stoneCost))
                {
                    this.buildStoneLabel.Color = ARGBColors.Red;
                }
            }
            if (goldCost > 0)
            {
                this.buildGoldLabel.Text = goldCost.ToString("N", nFI);
                this.buildGoldLabel.Visible = true;
                this.buildGoldLabel.Color = ARGBColors.Black;
                double capitalGold = 0.0;
                if (village != null)
                {
                    if (!GameEngine.Instance.World.isCapital(village.VillageID))
                    {
                        capitalGold = GameEngine.Instance.World.getCurrentGold();
                    }
                    else
                    {
                        capitalGold = village.m_capitalGold;
                    }
                }
                if (capitalGold < goldCost)
                {
                    this.buildGoldLabel.Color = ARGBColors.Red;
                }
            }
            if (flagsNeeded > 0)
            {
                this.buildStoneLabel.Text = flagsNeeded.ToString("N", nFI);
                this.buildStoneLabel.Visible = true;
                this.buildStoneLabel.Color = ARGBColors.Black;
                if ((village != null) && (village.numParishFlags() < flagsNeeded))
                {
                    this.buildStoneLabel.Color = ARGBColors.Red;
                }
            }
            if (buildingType >= 0x4f)
            {
                this.buildCapitalHelp.Visible = true;
                this.currentSelectedBuildingType = buildingType;
            }
            BaseImage image = this.getCapitalBuildingDonationTypeImage(realBuildingType);
            if (image != null)
            {
                this.buildDonationTypeImage.Visible = true;
                this.buildDonationTypeImage.Image = (Image) image;
            }
            else
            {
                this.buildDonationTypeImage.Visible = false;
            }
        }

        private void setBuildingTab(int tab)
        {
            this.buildTab1Button.CustomTooltipData = 0;
            this.buildTab2Button.CustomTooltipData = 0;
            this.buildTab3Button.CustomTooltipData = 0;
            this.buildTab4Button.CustomTooltipData = 0;
            this.buildTab5Button.CustomTooltipData = 0;
            this.currentTab = tab;
            if (!this.m_villageIsCapital)
            {
                this.buildTab1Button.Position = new Point(0, 0);
                this.buildTab2Button.Position = new Point(0x2c, 0);
                this.buildTab3Button.Position = new Point(0x51, 0);
                this.buildTab4Button.Position = new Point(0x76, 0);
                this.buildTab5Button.Position = new Point(0x9a, 0);
                this.buildTab5Button.Visible = true;
                this.buildTab1Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab1_norm;
                this.buildTab1Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab1_over;
                this.buildTab1Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab1_in;
                this.buildTab1Button.CustomTooltipID = 0x65;
                this.buildTab2Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab2_norm;
                this.buildTab2Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab2_over;
                this.buildTab2Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab2_in;
                this.buildTab2Button.CustomTooltipID = 0x66;
                this.buildTab3Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab3_norm;
                this.buildTab3Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab3_over;
                this.buildTab3Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab3_in;
                this.buildTab3Button.CustomTooltipID = 0x67;
                this.buildTab4Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab4_norm;
                this.buildTab4Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab4_over;
                this.buildTab4Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab4_in;
                this.buildTab4Button.CustomTooltipID = 0x68;
                this.buildTab5Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab5_norm;
                this.buildTab5Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab5_over;
                this.buildTab5Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab5_in;
                this.buildTab5Button.CustomTooltipID = 0x69;
            }
            else
            {
                this.buildTab1Button.Position = new Point(0, 0);
                this.buildTab2Button.Position = new Point(0x33, 0);
                this.buildTab3Button.Position = new Point(0x62, 0);
                this.buildTab4Button.Position = new Point(0x91, 0);
                this.buildTab5Button.Visible = false;
                this.buildTab1Button.ImageNorm = (Image) GFXLibrary.townscreen_castle_normal;
                this.buildTab1Button.ImageOver = (Image) GFXLibrary.townscreen_castle_over;
                this.buildTab1Button.ImageClick = (Image) GFXLibrary.townscreen_castle_over;
                this.buildTab1Button.CustomTooltipID = 0x6a;
                this.buildTab2Button.ImageNorm = (Image) GFXLibrary.townscreen_army_normal;
                this.buildTab2Button.ImageOver = (Image) GFXLibrary.townscreen_army_over;
                this.buildTab2Button.ImageClick = (Image) GFXLibrary.townscreen_army_over;
                this.buildTab2Button.CustomTooltipID = 0x6b;
                this.buildTab3Button.ImageNorm = (Image) GFXLibrary.townscreen_civil_normal;
                this.buildTab3Button.ImageOver = (Image) GFXLibrary.townscreen_civil_over;
                this.buildTab3Button.ImageClick = (Image) GFXLibrary.townscreen_civil_over;
                this.buildTab3Button.CustomTooltipID = 0x6c;
                this.buildTab4Button.ImageNorm = (Image) GFXLibrary.townscreen_guild_normal;
                this.buildTab4Button.ImageOver = (Image) GFXLibrary.townscreen_guild_over;
                this.buildTab4Button.ImageClick = (Image) GFXLibrary.townscreen_guild_over;
                this.buildTab4Button.CustomTooltipID = 0x6d;
            }
            int num = tab;
            switch (tab)
            {
                case 0x458:
                case 0x459:
                case 0x45a:
                case 0x45b:
                case 0x45c:
                    num = 3;
                    break;
            }
            switch (num)
            {
                case -1:
                    break;

                case 1:
                    this.buildTab2Button.CustomTooltipData = 1;
                    if (this.m_villageIsCapital)
                    {
                        this.buildTab2Button.ImageNorm = (Image) GFXLibrary.townscreen_army_selected;
                        this.buildTab2Button.ImageOver = (Image) GFXLibrary.townscreen_army_selected;
                        this.buildTab2Button.ImageClick = (Image) GFXLibrary.townscreen_army_selected;
                    }
                    else
                    {
                        this.buildTab2Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab2_arrow_norm;
                        this.buildTab2Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab2_arrow_over;
                        this.buildTab2Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab2_arrow_in;
                    }
                    break;

                case 2:
                    this.buildTab3Button.CustomTooltipData = 1;
                    if (this.m_villageIsCapital)
                    {
                        this.buildTab3Button.ImageNorm = (Image) GFXLibrary.townscreen_civil_selected;
                        this.buildTab3Button.ImageOver = (Image) GFXLibrary.townscreen_civil_selected;
                        this.buildTab3Button.ImageClick = (Image) GFXLibrary.townscreen_civil_selected;
                    }
                    else
                    {
                        this.buildTab3Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab3_arrow_norm;
                        this.buildTab3Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab3_arrow_over;
                        this.buildTab3Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab3_arrow_in;
                    }
                    break;

                case 3:
                    this.buildTab4Button.CustomTooltipData = 1;
                    if (this.m_villageIsCapital)
                    {
                        this.buildTab4Button.ImageNorm = (Image) GFXLibrary.townscreen_guild_selected;
                        this.buildTab4Button.ImageOver = (Image) GFXLibrary.townscreen_guild_selected;
                        this.buildTab4Button.ImageClick = (Image) GFXLibrary.townscreen_guild_selected;
                    }
                    else
                    {
                        this.buildTab4Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab4_arrow_norm;
                        this.buildTab4Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab4_arrow_over;
                        this.buildTab4Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab4_arrow_in;
                    }
                    break;

                case 4:
                    this.buildTab5Button.CustomTooltipData = 1;
                    this.buildTab5Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab5_arrow_norm;
                    this.buildTab5Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab5_arrow_over;
                    this.buildTab5Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab5_arrow_in;
                    break;

                default:
                    this.buildTab1Button.CustomTooltipData = 1;
                    if (!this.m_villageIsCapital)
                    {
                        this.buildTab1Button.ImageNorm = (Image) GFXLibrary.r_building_bar_tab1_arrow_norm;
                        this.buildTab1Button.ImageOver = (Image) GFXLibrary.r_building_bar_tab1_arrow_over;
                        this.buildTab1Button.ImageClick = (Image) GFXLibrary.r_building_bar_tab1_arrow_in;
                    }
                    else
                    {
                        this.buildTab1Button.ImageNorm = (Image) GFXLibrary.townscreen_castle_selected;
                        this.buildTab1Button.ImageOver = (Image) GFXLibrary.townscreen_castle_selected;
                        this.buildTab1Button.ImageClick = (Image) GFXLibrary.townscreen_castle_selected;
                    }
                    break;
            }
            this.resetBuildingIcons();
            switch (tab)
            {
                case 0:
                    if (this.m_villageIsCapital)
                    {
                        this.addBuildingIcon(0x67, GFXLibrary.townbuilding_architectsguild_normal, GFXLibrary.townbuilding_architectsguild_over);
                        this.addBuildingIcon(0x68, GFXLibrary.townbuilding_Labourersbillets_normal, GFXLibrary.townbuilding_Labourersbillets_over);
                        this.addBuildingIcon(0x69, GFXLibrary.townbuilding_castellanshouse_normal, GFXLibrary.townbuilding_castellanshouse_over);
                        this.addBuildingIcon(0x6a, GFXLibrary.townbuilding_sergeantsatarmsoffice_normal, GFXLibrary.townbuilding_sergeantsatarmsoffice_over);
                        this.addBuildingIcon(0x6b, GFXLibrary.townbuilding_stables_normal, GFXLibrary.townbuilding_stables_over);
                        this.addBuildingIcon(0x79, GFXLibrary.townbuilding_turretmaker_normal, GFXLibrary.townbuilding_turretmaker_over);
                        this.addBuildingIcon(0x7a, GFXLibrary.townbuilding_tunnellorsguild_normal, GFXLibrary.townbuilding_tunnellorsguild_over);
                        this.addBuildingIcon(0x7b, GFXLibrary.townbuilding_ballistamaker_normal, GFXLibrary.townbuilding_ballistamaker_over);
                        return;
                    }
                    switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
                    {
                        case 3:
                        case 4:
                            this.addBuildingIcon(1, GFXLibrary.r_building_panel_bld_civ_house_2, GFXLibrary.r_building_panel_bld_civ_house_2_over);
                            goto Label_07EE;

                        case 5:
                        case 6:
                            this.addBuildingIcon(1, GFXLibrary.r_building_panel_bld_civ_house_3, GFXLibrary.r_building_panel_bld_civ_house_3_over);
                            goto Label_07EE;

                        case 7:
                        case 8:
                            this.addBuildingIcon(1, GFXLibrary.r_building_panel_bld_civ_house_4, GFXLibrary.r_building_panel_bld_civ_house_4_over);
                            goto Label_07EE;

                        case 9:
                        case 10:
                            this.addBuildingIcon(1, GFXLibrary.r_building_panel_bld_civ_house_5, GFXLibrary.r_building_panel_bld_civ_house_5_over);
                            goto Label_07EE;
                    }
                    this.addBuildingIcon(1, GFXLibrary.r_building_panel_bld_civ_house_1, GFXLibrary.r_building_panel_bld_civ_house_1_over);
                    break;

                case 1:
                    if (this.m_villageIsCapital)
                    {
                        this.addBuildingIcon(0x6c, GFXLibrary.townbuilding_barracks_normal, GFXLibrary.townbuilding_barracks_over);
                        this.addBuildingIcon(0x6d, GFXLibrary.townbuilding_peasntshall_normal, GFXLibrary.townbuilding_peasntshall_over);
                        this.addBuildingIcon(110, GFXLibrary.townbuilding_archeryrange_normal, GFXLibrary.townbuilding_archeryrange_over);
                        this.addBuildingIcon(0x6f, GFXLibrary.townbuilding_pikemandrillyard_normal, GFXLibrary.townbuilding_pikemandrillyard_over);
                        this.addBuildingIcon(0x70, GFXLibrary.townbuilding_combatarena_normal, GFXLibrary.townbuilding_combatarena_over);
                        this.addBuildingIcon(0x71, GFXLibrary.townbuilding_siegeengineersguild_normal, GFXLibrary.townbuilding_siegeengineersguild_over);
                        this.addBuildingIcon(0x73, GFXLibrary.townbuilding_militaryschool_normal, GFXLibrary.townbuilding_militaryschool_over);
                        this.addBuildingIcon(0x74, GFXLibrary.townbuilding_supplydepot_normal, GFXLibrary.townbuilding_supplydepot_over);
                        return;
                    }
                    this.addBuildingIcon(2, GFXLibrary.r_building_panel_bld_icon_ind_stockpile, GFXLibrary.r_building_panel_bld_icon_ind_stockpile_over);
                    this.addBuildingIcon(6, GFXLibrary.r_building_panel_bld_icon_ind_woodcutters_hut, GFXLibrary.r_building_panel_bld_icon_ind_woodcutters_hut_over);
                    this.addBuildingIcon(7, GFXLibrary.r_building_panel_bld_icon_ind_stone_quarry, GFXLibrary.r_building_panel_bld_icon_ind_stone_quarry_over);
                    this.addBuildingIcon(8, GFXLibrary.r_building_panel_bld_icon_ind_iron_mine, GFXLibrary.r_building_panel_bld_icon_ind_iron_mine_over);
                    this.addBuildingIcon(9, GFXLibrary.r_building_panel_bld_icon_ind_pitch_rig, GFXLibrary.r_building_panel_bld_icon_ind_pitch_rig_over);
                    this.addBuildingIcon(0x4e, GFXLibrary.r_building_panel_bld_icon_ind_market, GFXLibrary.r_building_panel_bld_icon_ind_market_over);
                    return;

                case 2:
                    if (this.m_villageIsCapital)
                    {
                        this.addBuildingIcon(0x75, GFXLibrary.townbuilding_townhall_normal, GFXLibrary.townbuilding_townhall_over);
                        this.addBuildingIcon(0x76, GFXLibrary.townbuilding_church_normal, GFXLibrary.townbuilding_church_over);
                        this.addBuildingIcon(0x77, GFXLibrary.townbuilding_towngarden_normal, GFXLibrary.townbuilding_towngarden_over);
                        this.addBuildingIcon(120, GFXLibrary.townbuilding_statue_normal, GFXLibrary.townbuilding_statue_over);
                        return;
                    }
                    this.addBuildingIcon(3, GFXLibrary.r_building_panel_bld_icon_food_granary, GFXLibrary.r_building_panel_bld_icon_food_granary_over);
                    this.addBuildingIcon(0x23, GFXLibrary.r_building_panel_bld_icon_food_inn, GFXLibrary.r_building_panel_bld_icon_food_inn_over);
                    this.addBuildingIcon(13, GFXLibrary.r_building_panel_bld_icon_food_apple_orchard, GFXLibrary.r_building_panel_bld_icon_food_apple_orchard_over);
                    this.addBuildingIcon(0x11, GFXLibrary.r_building_panel_bld_icon_food_dairy_farm, GFXLibrary.r_building_panel_bld_icon_food_dairy_farm_over);
                    this.addBuildingIcon(0x10, GFXLibrary.r_building_panel_bld_icon_food_pig_farm, GFXLibrary.r_building_panel_bld_icon_food_pig_farm_over);
                    this.addBuildingIcon(14, GFXLibrary.r_building_panel_bld_icon_food_bakery, GFXLibrary.r_building_panel_bld_icon_food_bakery_over);
                    this.addBuildingIcon(15, GFXLibrary.r_building_panel_bld_icon_food_vegetable_farm, GFXLibrary.r_building_panel_bld_icon_food_vegetable_farm_over);
                    this.addBuildingIcon(0x12, GFXLibrary.r_building_panel_bld_icon_food_fishing_jetty, GFXLibrary.r_building_panel_bld_icon_food_fishing_jetty_over);
                    this.addBuildingIcon(12, GFXLibrary.r_building_panel_bld_icon_food_brewery, GFXLibrary.r_building_panel_bld_icon_food_brewery_over);
                    return;

                case 3:
                    if (this.m_villageIsCapital)
                    {
                        this.addBuildingIcon(0x458, GFXLibrary.townbuilding_resource_normal, GFXLibrary.townbuilding_resource_over);
                        this.addBuildingIcon(0x459, GFXLibrary.townbuilding_food_ale_normal, GFXLibrary.townbuilding_food_ale_over);
                        this.addBuildingIcon(0x45a, GFXLibrary.townbuilding_banquette_normal, GFXLibrary.townbuilding_banquette_over);
                        this.addBuildingIcon(0x45b, GFXLibrary.townbuilding_weapons_normal, GFXLibrary.townbuilding_weapons_over);
                        this.addBuildingIcon(0x45c, GFXLibrary.townbuilding_banquette_food_normal, GFXLibrary.townbuilding_banquette_food_over);
                        return;
                    }
                    this.addBuildingIcon(0x1d, GFXLibrary.r_building_panel_bld_icon_mil_fletcher, GFXLibrary.r_building_panel_bld_icon_mil_fletcher_over);
                    this.addBuildingIcon(0x1f, GFXLibrary.r_building_panel_bld_icon_mil_armourer, GFXLibrary.r_building_panel_bld_icon_mil_armourer_over);
                    this.addBuildingIcon(0x1c, GFXLibrary.r_building_panel_bld_icon_mil_pole_turner, GFXLibrary.r_building_panel_bld_icon_mil_pole_turner_over);
                    this.addBuildingIcon(30, GFXLibrary.r_building_panel_bld_icon_mil_blacksmith, GFXLibrary.r_building_panel_bld_icon_mil_blacksmith_over);
                    this.addBuildingIcon(0x20, GFXLibrary.r_building_panel_bld_icon_mil_siege_workshop, GFXLibrary.r_building_panel_bld_icon_mil_siege_workshop_over);
                    return;

                case 4:
                    if (!this.m_villageIsCapital)
                    {
                        this.addBuildingIcon(0x16, GFXLibrary.r_building_panel_bld_icon_hon_hunters_hut, GFXLibrary.r_building_panel_bld_icon_hon_hunters_hut_over);
                        this.addBuildingIcon(0x15, GFXLibrary.r_building_panel_bld_icon_hon_carpenters_workshop, GFXLibrary.r_building_panel_bld_icon_hon_carpenters_workshop_over);
                        this.addBuildingIcon(0x1a, GFXLibrary.r_building_panel_bld_icon_hon_metalworks_workshop, GFXLibrary.r_building_panel_bld_icon_hon_metalworks_workshop_over);
                        this.addBuildingIcon(0x13, GFXLibrary.r_building_panel_bld_icon_hon_tailers_workshop, GFXLibrary.r_building_panel_bld_icon_hon_tailers_workshop_over);
                        this.addBuildingIcon(0x21, GFXLibrary.r_building_panel_bld_icon_hon_vinyard, GFXLibrary.r_building_panel_bld_icon_hon_vinyard_over);
                        this.addBuildingIcon(0x17, GFXLibrary.r_building_panel_bld_icon_hon_salt_pan, GFXLibrary.r_building_panel_bld_icon_hon_salt_pan_over);
                        this.addBuildingIcon(0x18, GFXLibrary.r_building_panel_bld_icon_hon_spice_docs, GFXLibrary.r_building_panel_bld_icon_hon_spice_docs_over);
                        this.addBuildingIcon(0x19, GFXLibrary.r_building_panel_bld_icon_hon_silk_docs, GFXLibrary.r_building_panel_bld_icon_hon_silk_docs_over);
                    }
                    return;

                case 0x3e8:
                    this.addBuildingIcon(0x3ec, GFXLibrary.r_building_panel_bld_civ_rel_shrine_sml_variant, GFXLibrary.r_building_panel_bld_civ_rel_shrine_sml_variant_over);
                    this.addBuildingIcon(0x3ed, GFXLibrary.r_building_panel_bld_civ_rel_shrine_lrg_variant, GFXLibrary.r_building_panel_bld_civ_rel_shrine_lrg_variant_over);
                    this.addBuildingIcon(0x22, GFXLibrary.r_building_panel_bld_civ_rel_small_church, GFXLibrary.r_building_panel_bld_civ_rel_small_church_over);
                    this.addBuildingIcon(0x24, GFXLibrary.r_building_panel_bld_civ_rel_medium_church, GFXLibrary.r_building_panel_bld_civ_rel_medium_church_over);
                    this.addBuildingIcon(0x25, GFXLibrary.r_building_panel_bld_civ_rel_large_church, GFXLibrary.r_building_panel_bld_civ_rel_large_church_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3e9:
                    this.addBuildingIcon(0x3f0, GFXLibrary.r_building_panel_bld_civ_dec_garden_lrg_variant, GFXLibrary.r_building_panel_bld_civ_dec_garden_lrg_variant_over);
                    this.addBuildingIcon(0x3ee, GFXLibrary.r_building_panel_bld_civ_dec_garden_sml_variant, GFXLibrary.r_building_panel_bld_civ_dec_garden_sml_variant_over);
                    this.addBuildingIcon(0x457, GFXLibrary.r_building_panel_bld_civ_dec_statue_lrg_variant, GFXLibrary.r_building_panel_bld_civ_dec_statue_lrg_variant_over);
                    this.addBuildingIcon(0x3f2, GFXLibrary.r_building_panel_bld_civ_dec_statue_sml_variant, GFXLibrary.r_building_panel_bld_civ_dec_statue_sml_variant_over);
                    this.addBuildingIcon(60, GFXLibrary.r_building_panel_bld_civ_dec_dovecote, GFXLibrary.r_building_panel_bld_civ_dec_dovecote_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3ea:
                    this.addBuildingIcon(0x3d, GFXLibrary.r_building_panel_bld_jus_stocks, GFXLibrary.r_building_panel_bld_jus_stocks_over);
                    this.addBuildingIcon(0x3e, GFXLibrary.r_building_panel_bld_jus_burning_post, GFXLibrary.r_building_panel_bld_jus_burning_post_over);
                    this.addBuildingIcon(0x40, GFXLibrary.r_building_panel_bld_jus_stretching_rack, GFXLibrary.r_building_panel_bld_jus_stretching_rack_over);
                    this.addBuildingIcon(0x3f, GFXLibrary.r_building_panel_bld_jus_gibbet, GFXLibrary.r_building_panel_bld_jus_gibbet_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3eb:
                    this.addBuildingIcon(0x43, GFXLibrary.r_building_panel_bld_ent_theatre, GFXLibrary.r_building_panel_bld_ent_theatre_over);
                    this.addBuildingIcon(0x44, GFXLibrary.r_building_panel_bld_ent_jesters_court, GFXLibrary.r_building_panel_bld_ent_jesters_court_over);
                    this.addBuildingIcon(0x42, GFXLibrary.r_building_panel_bld_ent_dancing_bear, GFXLibrary.r_building_panel_bld_ent_dancing_bear_over);
                    this.addBuildingIcon(0x45, GFXLibrary.r_building_panel_bld_ent_troubadours_arbor, GFXLibrary.r_building_panel_bld_ent_troubadours_arbor_over);
                    this.addBuildingIcon(0x41, GFXLibrary.r_building_panel_bld_ent_maypole, GFXLibrary.r_building_panel_bld_ent_maypole_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3ec:
                    this.addBuildingIcon(70, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_01, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_01_over);
                    this.addBuildingIcon(0x47, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_02, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_02_over);
                    this.addBuildingIcon(0x48, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_03, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_03_over);
                    this.addBuildingIcon(0x49, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_04, GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_04_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3ed:
                    this.addBuildingIcon(0x4a, GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_01, GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_01_over);
                    this.addBuildingIcon(0x4b, GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_02, GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_02_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3ee:
                    this.addBuildingIcon(0x26, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_01, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_01_over);
                    this.addBuildingIcon(0x29, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_02, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_02_over);
                    this.addBuildingIcon(0x2a, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_03, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_03_over);
                    this.addBuildingIcon(0x2b, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_04, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_04_over);
                    this.addBuildingIcon(0x2c, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_05, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_05_over);
                    this.addBuildingIcon(0x2d, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_06, GFXLibrary.r_building_panel_bld_civ_dec_small_garden_06_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3ef:
                case 0x3f1:
                    return;

                case 0x3f0:
                    this.addBuildingIcon(0x31, GFXLibrary.r_building_panel_bld_civ_dec_large_garden_01png, GFXLibrary.r_building_panel_bld_civ_dec_large_garden_01png_over);
                    this.addBuildingIcon(50, GFXLibrary.r_building_panel_bld_civ_dec_large_garden_02, GFXLibrary.r_building_panel_bld_civ_dec_large_garden_02_over);
                    this.addBuildingIcon(0x33, GFXLibrary.r_building_panel_bld_civ_dec_large_garden_03, GFXLibrary.r_building_panel_bld_civ_dec_large_garden_03_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x3f2:
                    this.addBuildingIcon(0x36, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_01, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_01_over);
                    this.addBuildingIcon(0x37, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_02, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_02_over);
                    this.addBuildingIcon(0x38, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_03, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_03_over);
                    this.addBuildingIcon(0x39, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_04, GFXLibrary.r_building_panel_bld_civ_dec_small_statue_04_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x457:
                    this.addBuildingIcon(0x3a, GFXLibrary.r_building_panel_bld_civ_dec_large_statue_01, GFXLibrary.r_building_panel_bld_civ_dec_large_statue_01_over);
                    this.addBuildingIcon(0x3b, GFXLibrary.r_building_panel_bld_civ_dec_large_statue_02, GFXLibrary.r_building_panel_bld_civ_dec_large_statue_02_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x458:
                    this.addBuildingIcon(0x4f, GFXLibrary.townbuilding_Woodcutter_normal, GFXLibrary.townbuilding_Woodcutter_over);
                    this.addBuildingIcon(80, GFXLibrary.townbuilding_stonequarry_normal, GFXLibrary.townbuilding_stonequarry_over);
                    this.addBuildingIcon(0x51, GFXLibrary.townbuilding_iron_normal, GFXLibrary.townbuilding_iron_over);
                    this.addBuildingIcon(0x52, GFXLibrary.townbuilding_pitch_normal, GFXLibrary.townbuilding_pitch_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x459:
                    this.addBuildingIcon(0x54, GFXLibrary.townbuilding_apples_normal, GFXLibrary.townbuilding_apples_over);
                    this.addBuildingIcon(0x55, GFXLibrary.townbuilding_cheese_normal, GFXLibrary.townbuilding_cheese_over);
                    this.addBuildingIcon(0x56, GFXLibrary.townbuilding_meat_normal, GFXLibrary.townbuilding_meat_over);
                    this.addBuildingIcon(0x57, GFXLibrary.townbuilding_bread_normal, GFXLibrary.townbuilding_bread_over);
                    this.addBuildingIcon(0x58, GFXLibrary.townbuilding_veg_normal, GFXLibrary.townbuilding_veg_over);
                    this.addBuildingIcon(0x59, GFXLibrary.townbuilding_fish_normal, GFXLibrary.townbuilding_fish_over);
                    this.addBuildingIcon(0x53, GFXLibrary.townbuilding_ale_normal, GFXLibrary.townbuilding_ale_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x45a:
                    this.addBuildingIcon(0x62, GFXLibrary.townbuilding_carpenter_normal, GFXLibrary.townbuilding_carpenter_over);
                    this.addBuildingIcon(100, GFXLibrary.townbuilding_metalware_normal, GFXLibrary.townbuilding_metalware_over);
                    this.addBuildingIcon(0x63, GFXLibrary.townbuilding_tailor_normal, GFXLibrary.townbuilding_tailor_over);
                    this.addBuildingIcon(0x66, GFXLibrary.townbuilding_silk_normal, GFXLibrary.townbuilding_silk_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x45b:
                    this.addBuildingIcon(90, GFXLibrary.townbuilding_bows_normal, GFXLibrary.townbuilding_bows_over);
                    this.addBuildingIcon(0x5b, GFXLibrary.townbuilding_pikes_normal, GFXLibrary.townbuilding_pikes_over);
                    this.addBuildingIcon(0x5c, GFXLibrary.townbuilding_armour_normal, GFXLibrary.townbuilding_armour_over);
                    this.addBuildingIcon(0x5d, GFXLibrary.townbuilding_sword_normal, GFXLibrary.townbuilding_sword_over);
                    this.addBuildingIcon(0x5e, GFXLibrary.townbuilding_catapults_normal, GFXLibrary.townbuilding_catapults_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                case 0x45c:
                    this.addBuildingIcon(0x5f, GFXLibrary.townbuilding_venison_normal, GFXLibrary.townbuilding_venison_over);
                    this.addBuildingIcon(0x60, GFXLibrary.townbuilding_wine_normal, GFXLibrary.townbuilding_wine_over);
                    this.addBuildingIcon(0x61, GFXLibrary.townbuilding_salt_normal, GFXLibrary.townbuilding_salt_over);
                    this.addBuildingIcon(0x65, GFXLibrary.townbuilding_spice_normal, GFXLibrary.townbuilding_spice_over);
                    this.addBuildingIcon(0x7d0, GFXLibrary.r_building_panel_bld_back, GFXLibrary.r_building_panel_bld_back_over);
                    return;

                default:
                    return;
            }
        Label_07EE:
            this.addBuildingIcon(0x3e9, GFXLibrary.r_building_panel_bld_civ_dec_sub_category, GFXLibrary.r_building_panel_bld_civ_dec_sub_category_over);
            this.addBuildingIcon(0x3eb, GFXLibrary.r_building_panel_bld_civ_ent_sub_category, GFXLibrary.r_building_panel_bld_civ_ent_sub_category_over);
            this.addBuildingIcon(0x3e8, GFXLibrary.r_building_panel_bld_civ_rel_sub_category, GFXLibrary.r_building_panel_bld_civ_rel_sub_category_over);
            this.addBuildingIcon(0x3ea, GFXLibrary.r_building_panel_bld_civ_jus_sub_category, GFXLibrary.r_building_panel_bld_civ_jus_sub_category_over);
        }

        public void showAsVillage(bool asVillage)
        {
            this.m_villageIsCapital = !asVillage;
            this.panelImage.Visible = asVillage;
            this.headerImage.Visible = asVillage;
            this.extensionImage.Visible = asVillage;
            this.buildPanelImage.Visible = true;
            this.buildHeaderArea.Visible = true;
            this.info1HeaderPanelImage.Visible = asVillage;
            this.info1HeaderHonourImage.Visible = asVillage;
            this.info1PanelImage.Visible = asVillage;
            this.info2HeaderPanelImage.Visible = asVillage;
            this.info3HeaderPanelImage.Visible = asVillage;
            this.inBuildingHeaderPanelImage.Visible = false;
            this.inBuildingPanelImage.Visible = false;
            this.capitalPanelImage.Visible = !asVillage;
            this.capitalTop10PanelImage.Visible = !asVillage;
            this.capitalTop10HeaderImage.Visible = !asVillage;
            this.capitalTaxLowerButton.Enabled = true;
            this.capitalTaxHigherButton.Enabled = true;
            if (!asVillage)
            {
                if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    this.capitalTaxLowerButton.Enabled = false;
                    this.capitalTaxHigherButton.Enabled = false;
                    this.buildPanelImage.Visible = false;
                    this.buildHeaderArea.Visible = false;
                }
                this.buildGoldImage.Position = new Point(13, 40);
                this.buildGoldLabel.Position = new Point(40, 0x2c);
                this.buildWoodImage.Visible = false;
                this.buildStoneImage.Image = (Image) GFXLibrary.flag_blue_icon;
                this.inBuildingCapitalResourceLevelLabel1.Visible = true;
                this.inBuildingCapitalResourceLevelLabel2.Visible = true;
            }
            else
            {
                this.buildGoldImage.Position = new Point(0x56, 40);
                this.buildGoldLabel.Position = new Point(0x71, 0x2c);
                this.buildWoodImage.Visible = true;
                this.buildStoneImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_stone;
                this.inBuildingCapitalResourceLevelLabel1.Visible = false;
                this.inBuildingCapitalResourceLevelLabel2.Visible = false;
            }
            this.inBuildingCapitalResourceLevelLabel1.Visible = false;
            this.inBuildingCapitalResourceLevelLabel2.Visible = false;
            this.inBuildingCapitalResourceLabel1a.Visible = false;
            this.inBuildingCapitalResourceLabel1b.Visible = false;
            this.inBuildingCapitalResourceLabel1c.Visible = false;
            this.inBuildingCapitalResourceImage1.Visible = false;
            this.inBuildingCapitalResourceLabel2a.Visible = false;
            this.inBuildingCapitalResourceLabel2b.Visible = false;
            this.inBuildingCapitalResourceLabel2c.Visible = false;
            this.inBuildingCapitalResourceImage2.Visible = false;
            this.inBuildingCapitalResourceLabel3a.Visible = false;
            this.inBuildingCapitalResourceLabel3b.Visible = false;
            this.inBuildingCapitalResourceLabel3c.Visible = false;
            this.inBuildingCapitalResourceImage3.Visible = false;
            this.inBuildingCapitalResourceLabel4a.Visible = false;
            this.inBuildingCapitalResourceLabel4b.Visible = false;
            this.inBuildingCapitalResourceLabel4c.Visible = false;
            this.inBuildingCapitalResourceImage4.Visible = false;
            this.inBuildingCapitalResourceLabel5a.Visible = false;
            this.inBuildingCapitalResourceLabel5b.Visible = false;
            this.inBuildingCapitalResourceLabel5c.Visible = false;
            this.inBuildingCapitalResourceImage5.Visible = false;
            this.inBuildingCapitalResourceLabel6a.Visible = false;
            this.inBuildingCapitalResourceLabel6b.Visible = false;
            this.inBuildingCapitalResourceLabel6c.Visible = false;
            this.inBuildingCapitalResourceImage6.Visible = false;
            this.inBuildingCapitalResourceLabel7a.Visible = false;
            this.inBuildingCapitalResourceLabel7b.Visible = false;
            this.inBuildingCapitalResourceLabel7c.Visible = false;
            this.inBuildingCapitalResourceImage7.Visible = false;
            this.inBuildingCapitalResourceLabel8a.Visible = false;
            this.inBuildingCapitalResourceLabel8b.Visible = false;
            this.inBuildingCapitalResourceLabel8c.Visible = false;
            this.inBuildingCapitalResourceImage8.Visible = false;
        }

        public void showBuildingInfo(int numPositiveBuildings, int numNegativeBuildings, int numPopularityBuildings)
        {
            double num = VillageBuildingsData.getBuildingsTypePopularityLevel(numPositiveBuildings, numNegativeBuildings, GameEngine.Instance.World.UserCardData);
            this.buildingPopLabel.Text = num.ToString();
            if (num < 0.0)
            {
                this.popIndent5Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_red;
                this.buildingsLabel.Color = ARGBColors.Red;
            }
            else if (num > 0.0)
            {
                this.popIndent5Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
                this.buildingsLabel.Color = ARGBColors.Green;
            }
            else
            {
                this.popIndent5Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_tan;
                this.buildingsLabel.Color = ARGBColors.Black;
            }
            this.buildingsLabel.Text = numPopularityBuildings.ToString();
            this.negativeBuildingsLabel.Text = "";
            if (numPositiveBuildings > 0)
            {
                this.positiveBuildingsLabel.Text = "+";
            }
            else
            {
                this.positiveBuildingsLabel.Text = "";
            }
            if (numNegativeBuildings > 0)
            {
                this.negativeBuildingsLabel.Text = "-";
            }
            else
            {
                this.negativeBuildingsLabel.Text = "";
            }
            this.positiveBuildingsLabel.Text = this.positiveBuildingsLabel.Text + numPositiveBuildings.ToString();
            this.negativeBuildingsLabel.Text = this.negativeBuildingsLabel.Text + numNegativeBuildings.ToString();
        }

        public void showCapitalData(int taxSetting, int parishTax, ParishTaxCalc[] parishTaxPeople, int parentCapitalTaxRate, int lastCapitalTaxRate)
        {
            if (GameEngine.Instance.LocalWorldData != null)
            {
                int num1 = GameEngine.Instance.LocalWorldData.Alternate_Ruleset;
            }
            this.capitalTaxBar.Number = taxSetting;
            switch (taxSetting)
            {
                case -3:
                    this.capitalTaxLine1Label.Text = "x-3";
                    break;

                case -2:
                    this.capitalTaxLine1Label.Text = "x-2";
                    break;

                case -1:
                    this.capitalTaxLine1Label.Text = "x-1";
                    break;

                case 0:
                    this.capitalTaxLine1Label.Text = "0";
                    break;

                case 1:
                    this.capitalTaxLine1Label.Text = "x1";
                    break;

                case 2:
                    this.capitalTaxLine1Label.Text = "x2";
                    break;

                case 3:
                    this.capitalTaxLine1Label.Text = "x3";
                    break;

                case 4:
                    this.capitalTaxLine1Label.Text = "x4";
                    break;

                case 5:
                    this.capitalTaxLine1Label.Text = "x5";
                    break;

                case 6:
                    this.capitalTaxLine1Label.Text = "x6";
                    break;

                case 7:
                    this.capitalTaxLine1Label.Text = "x7";
                    break;

                case 8:
                    this.capitalTaxLine1Label.Text = "x8";
                    break;

                case 9:
                    this.capitalTaxLine1Label.Text = "x9";
                    break;

                default:
                    this.capitalTaxLine1Label.Text = "x" + taxSetting.ToString();
                    break;
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            this.capitalTaxPerDayValueLabel.Text = parishTax.ToString("N", nFI);
            switch (lastCapitalTaxRate)
            {
                case -3:
                    this.capitalLastTaxRateValueLabel.Text = "x-3";
                    break;

                case -2:
                    this.capitalLastTaxRateValueLabel.Text = "x-2";
                    break;

                case -1:
                    this.capitalLastTaxRateValueLabel.Text = "x-1";
                    break;

                case 0:
                    this.capitalLastTaxRateValueLabel.Text = "0";
                    break;

                case 1:
                    this.capitalLastTaxRateValueLabel.Text = "x1";
                    break;

                case 2:
                    this.capitalLastTaxRateValueLabel.Text = "x2";
                    break;

                case 3:
                    this.capitalLastTaxRateValueLabel.Text = "x3";
                    break;

                case 4:
                    this.capitalLastTaxRateValueLabel.Text = "x4";
                    break;

                case 5:
                    this.capitalLastTaxRateValueLabel.Text = "x5";
                    break;

                case 6:
                    this.capitalLastTaxRateValueLabel.Text = "x6";
                    break;

                case 7:
                    this.capitalLastTaxRateValueLabel.Text = "x7";
                    break;

                case 8:
                    this.capitalLastTaxRateValueLabel.Text = "x8";
                    break;

                case 9:
                    this.capitalLastTaxRateValueLabel.Text = "x9";
                    break;

                default:
                    this.capitalLastTaxRateValueLabel.Text = "x" + lastCapitalTaxRate.ToString();
                    break;
            }
            for (int i = 0; i < 11; i++)
            {
                CustomSelfDrawPanel.CSDLabel label = this.getCapitalNameLabel(i);
                CustomSelfDrawPanel.CSDLabel label2 = this.getCapitalValueLabel(i);
                label.Visible = false;
                label2.Visible = false;
            }
            List<ParishTaxCalc> list = new List<ParishTaxCalc>();
            if (parishTaxPeople != null)
            {
                foreach (ParishTaxCalc calc in parishTaxPeople)
                {
                    list.Add(calc);
                }
            }
            if (taxSetting > 0)
            {
                list.Sort(this.parishTaxComparerPositive);
                this.capitalTaxTopGivers.Text = SK.Text("VillageMapPanel_Top_Givers", "Top Tax Payers");
            }
            else if (taxSetting < 0)
            {
                list.Sort(this.parishTaxComparerNegative);
                this.capitalTaxTopGivers.Text = SK.Text("VillageMapPanel_Top_Receivers", "Top Receivers");
            }
            else
            {
                this.capitalTaxTopGivers.Text = SK.Text("VillageMapPanel_Parish_Members", "Parish Members");
            }
            for (int j = 0; (j < 10) && (j < list.Count); j++)
            {
                CustomSelfDrawPanel.CSDLabel label3 = this.getCapitalNameLabel(j);
                CustomSelfDrawPanel.CSDLabel label4 = this.getCapitalValueLabel(j);
                label3.Visible = true;
                label4.Visible = true;
                ParishTaxCalc calc2 = list[j];
                label3.Text = calc2.userName;
                int tax = calc2.tax;
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                {
                    tax *= 2;
                }
                if (tax >= 0)
                {
                    label4.Text = tax.ToString("N", nFI);
                }
                else
                {
                    label4.Text = tax.ToString("N", nFI);
                }
            }
            if (list.Count > 10)
            {
                int num4 = 0;
                for (int k = 10; k < list.Count; k++)
                {
                    ParishTaxCalc calc3 = list[k];
                    int num6 = calc3.tax;
                    if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                    {
                        num6 *= 2;
                    }
                    num4 += num6;
                }
                CustomSelfDrawPanel.CSDLabel label5 = this.getCapitalNameLabel(10);
                CustomSelfDrawPanel.CSDLabel label6 = this.getCapitalValueLabel(10);
                label5.Visible = true;
                label6.Visible = true;
                label6.Text = num4.ToString("N", nFI);
            }
            if (GameEngine.Instance.World.isRegionCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
            {
                this.capitalTop10PanelImage.Visible = true;
                this.capitalTop10HeaderImage.Visible = true;
                this.capitalOutgoingPerDayLabel.Text = SK.Text("VillageMapPanel_County_Tithe", "County Tithe") + ":";
                this.capitalOutgoingPerDayLabel.Visible = true;
                this.capitalOutgoingPerDayValueLabel.Visible = true;
                this.capitalOutgoingPerDayValueLabel.Text = (parentCapitalTaxRate * GameEngine.Instance.LocalWorldData.BaseTaxForAreaCounty).ToString("N", nFI);
            }
            else
            {
                this.capitalTop10PanelImage.Visible = false;
                this.capitalTop10HeaderImage.Visible = false;
                if (GameEngine.Instance.World.isCountyCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    this.capitalOutgoingPerDayLabel.Text = SK.Text("VillageMapPanel_Province_Tithe", "Province Tithe") + ":";
                    this.capitalOutgoingPerDayLabel.Visible = true;
                    this.capitalOutgoingPerDayValueLabel.Visible = true;
                    this.capitalOutgoingPerDayValueLabel.Text = (parentCapitalTaxRate * GameEngine.Instance.LocalWorldData.BaseTaxForAreaProvince).ToString("N", nFI);
                }
                else if (GameEngine.Instance.World.isProvinceCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    this.capitalOutgoingPerDayLabel.Text = SK.Text("VillageMapPanel_Country_Tithe", "Country Tithe") + ":";
                    this.capitalOutgoingPerDayLabel.Visible = true;
                    this.capitalOutgoingPerDayValueLabel.Visible = true;
                    this.capitalOutgoingPerDayValueLabel.Text = (parentCapitalTaxRate * GameEngine.Instance.LocalWorldData.BaseTaxForAreaCountry).ToString("N", nFI);
                }
                else if (GameEngine.Instance.World.isCountryCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    this.capitalOutgoingPerDayLabel.Visible = false;
                    this.capitalOutgoingPerDayValueLabel.Visible = false;
                }
            }
        }

        public void showDayRates(double goldDayRate, double dailyFoodConsumption, double dailyAleConsumption, double foodProductionRate, double aleProductionRate, int parishTax)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            this.taxDayValueLabel.Text = "0";
            if (goldDayRate > 0.0)
            {
                this.taxDayValueLabel.Text = "+" + goldDayRate.ToString("N", nFI);
            }
            else if (goldDayRate < 0.0)
            {
                this.taxDayValueLabel.Text = goldDayRate.ToString("N", nFI);
            }
            this.taxLine1Label.Text = this.taxDayValueLabel.Text;
            if (parishTax >= 0)
            {
                this.parishTaxDayLabel.Text = SK.Text("VillageMapPanel_Parish_Tithe", "Parish Tithe");
                this.parishTaxDayValueLabel.Text = parishTax.ToString("N", nFI);
                this.parishTaxDayValueLabel.Color = ARGBColors.Red;
            }
            else
            {
                this.parishTaxDayLabel.Text = SK.Text("VillageMapPanel_Parish_Bribe", "Parish Bribe");
                this.parishTaxDayValueLabel.Text = parishTax.ToString("N", nFI);
                this.parishTaxDayValueLabel.Color = ARGBColors.Black;
            }
            this.rationsDayValueLabel.Text = "0";
            if (dailyFoodConsumption > 0.0)
            {
                this.rationsDayValueLabel.Text = ((int) (dailyFoodConsumption + 0.01)).ToString("N", nFI);
            }
            this.rationsDay2ValueLabel.Text = ((int) (foodProductionRate + 0.01)).ToString("N", nFI);
            this.aleRationsDayValueLabel.Text = "0";
            if (dailyAleConsumption > 0.0)
            {
                this.aleRationsDayValueLabel.Text = ((int) (dailyAleConsumption + 0.01)).ToString("N", nFI);
            }
            this.aleRationsDay2ValueLabel.Text = ((int) (aleProductionRate + 0.01)).ToString("N", nFI);
        }

        public void showExtras()
        {
            if (!this.m_villageIsCapital)
            {
                this.extrasHeaderPanelImage.Visible = false;
                this.info1HeaderPanelImage.Visible = true;
                this.info2HeaderPanelImage.Visible = false;
                this.info3HeaderPanelImage.Visible = false;
            }
        }

        public void showGoldChange(double currentGold, double goldRate)
        {
        }

        public void showHonour()
        {
        }

        public void showHonourBanquet(double honourAmount, DateTime lastBanquetDate)
        {
        }

        public void showHonourBuildings(int numChurches, int numChapels, int numCathedrals, int numSmallGardens, int numLargeGardens, int numSmallStatues, int numLargeStatues, int numDovecotes, int numStocks, int numBurningPosts, int numGibbets, int numRacks, double popularitylevel, int parishBonus)
        {
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            double num = 0.0;
            if (numChapels > 0)
            {
                num += localWorldData.HonourBuilding_Chapel * numChapels;
            }
            if (numChurches > 0)
            {
                num += localWorldData.HonourBuilding_Church * numChurches;
            }
            if (numCathedrals > 0)
            {
                num += localWorldData.HonourBuilding_Cathedral * numCathedrals;
            }
            double d = 0.0;
            double num3 = 0.0;
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                VillageMapBuilding building = village.findBuildingType(0);
                if (building != null)
                {
                    foreach (VillageMapBuilding building2 in village.Buildings)
                    {
                        if (!building2.isComplete())
                        {
                            continue;
                        }
                        double honour = 0.0;
                        bool flag = true;
                        switch (building2.buildingType)
                        {
                            case 0x26:
                            case 0x29:
                            case 0x2a:
                            case 0x2b:
                            case 0x2c:
                            case 0x2d:
                                honour = localWorldData.HonourBuilding_SmallGarden;
                                break;

                            case 0x31:
                            case 50:
                            case 0x33:
                                honour = localWorldData.HonourBuilding_LargeGarden;
                                break;

                            case 0x36:
                            case 0x37:
                            case 0x38:
                            case 0x39:
                                honour = localWorldData.HonourBuilding_SmallStatue;
                                break;

                            case 0x3a:
                            case 0x3b:
                                honour = localWorldData.HonourBuilding_LargeStatue;
                                break;

                            case 60:
                                honour = localWorldData.HonourBuilding_Dovecote;
                                break;

                            case 0x3d:
                                flag = false;
                                honour = localWorldData.HonourBuilding_Stocks;
                                break;

                            case 0x3e:
                                flag = false;
                                honour = localWorldData.HonourBuilding_BurningPost;
                                break;

                            case 0x3f:
                                flag = false;
                                honour = localWorldData.HonourBuilding_Gibbet;
                                break;

                            case 0x40:
                                flag = false;
                                honour = localWorldData.HonourBuilding_Rack;
                                break;
                        }
                        if (honour != 0.0)
                        {
                            double num5 = VillageBuildingsData.calcHonourRateBasedOnDistance(honour, building.buildingLocation, building2.buildingLocation);
                            if (flag)
                            {
                                d += num5;
                            }
                            else
                            {
                                num3 += num5;
                            }
                        }
                    }
                }
            }
            double num6 = CardTypes.getPopToHonourFromCards(GameEngine.Instance.World.UserCardData);
            if (GameEngine.Instance.World.ThirdAgeWorld)
            {
                num *= 4.0;
                d *= 4.0;
                num3 *= 4.0;
            }
            double num7 = ResearchData.artsResearchAffect[GameEngine.Instance.World.UserResearchData.Research_Arts];
            double num8 = ((((num + d) + num3) + num7) + parishBonus) + num6;
            double num9 = 0.0;
            if (popularitylevel > 0.0)
            {
                num9 = popularitylevel * num8;
            }
            NumberFormatInfo provider = GameEngine.NFI_D;
            NumberFormatInfo nFI = GameEngine.NFI;
            this.info1ChurchAmount.Text = num.ToString("N", nFI);
            if (d == Math.Floor(d))
            {
                this.info1DecorativeAmount.Text = d.ToString("N", nFI);
            }
            else
            {
                this.info1DecorativeAmount.Text = d.ToString("N", provider);
            }
            if (num3 == Math.Floor(num3))
            {
                this.info1JusticeAmount.Text = num3.ToString("N", nFI);
            }
            else
            {
                this.info1JusticeAmount.Text = num3.ToString("N", provider);
            }
            this.info1ArtsAmount.Text = ((int) num7).ToString("N", nFI);
            if (num8 == Math.Floor(num8))
            {
                this.info1PopularityAmount.Text = num8.ToString("N", nFI);
            }
            else
            {
                this.info1PopularityAmount.Text = num8.ToString("N", provider);
            }
            this.info1ParishAmount.Text = parishBonus.ToString("N", nFI);
            this.info1CardsAmount.Text = num6.ToString("N", nFI);
            this.info1HonourCalc2.Text = " x  " + Math.Max((int) popularitylevel, 0).ToString("N", nFI);
            double viewHonour = num8 * ((int) popularitylevel);
            if (this.ViewOnly)
            {
                if (GameEngine.Instance.Village != null)
                {
                    viewHonour = GameEngine.Instance.Village.ViewHonour;
                }
                else
                {
                    viewHonour = 0.0;
                }
            }
            if (viewHonour < 0.0)
            {
                viewHonour = 0.0;
            }
            this.info1HeaderHonourAmount.Text = ((int) viewHonour).ToString("N", nFI);
            if (num9 == Math.Floor(num9))
            {
                this.info1HonourPerDayAmount.Text = num9.ToString("N", nFI);
            }
            else
            {
                this.info1HonourPerDayAmount.Text = num9.ToString("N", provider);
            }
        }

        public void showHonourPopularity(double popularity)
        {
        }

        public void showInBuildingInfo(VillageMapBuilding building)
        {
            int num13;
            if (building == null)
            {
                this.closeInBuildingPanel();
                return;
            }
            VillageMap village = GameEngine.Instance.Village;
            NumberFormatInfo nFI = GameEngine.NFI;
            this.closeTopGivers();
            this.closeBuildingPanel();
            this.closePopularityPanel();
            this.closeInfo1Panel();
            this.showExtras();
            if (village != null)
            {
                village.highlightBuilding(building);
            }
            int y = 0;
            this.inBuildingDonateButton.Visible = false;
            this.inBuildingName.Text = VillageBuildingsData.getBuildingName(building.buildingType);
            if (!this.m_villageIsCapital)
            {
                this.inBuildingHeaderPanelImage.CustomTooltipData = -1;
                this.inBuildingHelpButton.Visible = false;
                y = this.calcInfoTabYPos() + 0x37;
                this.targetInBuildingHeight = 0x14f;
                this.inBuildingHeaderPanelImage.Position = new Point(0, y);
                this.inBuildingPanelImage.Position = new Point(0, y + 0x19);
                this.inBuildingCompleteLabel.Position = new Point(8, 0x34);
                this.inBuildingCompleteLabel2.Position = new Point(8, 70);
                this.inBuildingDeleteButton.Visible = true;
                if (building.isDeleting())
                {
                    this.inBuildingDeleteButton.Text.Text = SK.Text("VillageMapPanel_Cancel_Delete", "Cancel Delete");
                    this.inBuildingDeleteButton.Enabled = true;
                }
                else
                {
                    this.inBuildingDeleteButton.Text.Text = SK.Text("VillageMapPanel_Delete_This_Building", "Delete this Building");
                    if (village.getNumDeleting() >= 3)
                    {
                        this.inBuildingDeleteButton.Enabled = false;
                    }
                    else
                    {
                        this.inBuildingDeleteButton.Enabled = true;
                    }
                }
            }
            else
            {
                this.inBuildingHeaderPanelImage.CustomTooltipData = building.buildingType;
                this.inBuildingHelpButton.Visible = true;
                this.currentSelectedBuildingType = building.buildingType;
                y = this.calcTop10YPos() + 0x37;
                this.targetInBuildingHeight = 290;
                this.inBuildingHeaderPanelImage.Position = new Point(0, y);
                int num3 = 0x2d;
                this.inBuildingPanelImage.Position = new Point(0, (y + 0x19) - num3);
                this.inBuildingCompleteLabel.Position = new Point(8, 0x1b + num3);
                this.inBuildingCompleteLabel2.Position = new Point(8, 0x2d + num3);
                if (GameEngine.Instance.World.isUserVillage(village.VillageID))
                {
                    this.inBuildingDeleteButton.Enabled = true;
                    this.inBuildingDeleteButton.Visible = true;
                    if (VillageMap.getCurrentServerTime() < GameEngine.Instance.Village.m_captialNextDelete)
                    {
                        this.inBuildingDeleteButton.Enabled = false;
                        string str = SK.Text("VillageMapPanel_Next_Delete", "Next Delete : ") + " < ";
                        TimeSpan span = (TimeSpan) (GameEngine.Instance.Village.m_captialNextDelete - VillageMap.getCurrentServerTime());
                        int num4 = (int) (span.TotalHours + 1.0);
                        str = str + num4.ToString() + " ";
                        if (num4 == 1)
                        {
                            str = str + SK.Text("VillageMapPanel_Hour", "Hour");
                        }
                        else
                        {
                            str = str + SK.Text("VillageMapPanel_Hours", "Hours");
                        }
                        this.inBuildingDeleteButton.Text.Text = str;
                    }
                    else
                    {
                        this.inBuildingDeleteButton.Text.Text = SK.Text("VillageMapPanel_Delete_This_Building", "Delete this Building");
                    }
                }
                else
                {
                    this.inBuildingDeleteButton.Visible = false;
                }
            }
            this.inBuildingHeaderPanelImage.Visible = true;
            this.inBuildingPanelImage.Visible = true;
            this.inBuildingGenericButton.Visible = false;
            this.inBuildingGenericButton2.Visible = false;
            this.inBuildingMakeWeaponLabel0.Visible = false;
            this.inBuildingMakeWeaponLabel1.Visible = false;
            this.inBuildingMakeWeaponLabel2.Visible = false;
            this.inBuildingMakeWeaponLabel3.Visible = false;
            this.inBuildingMakeWeaponLabel4.Visible = false;
            this.inBuildingMakeWeaponLabel5.Visible = false;
            this.inBuildingMakeWeaponLabel6.Visible = false;
            this.inBuildingMakeWeaponImage1.Visible = false;
            this.inBuildingMakeWeaponImage2.Visible = false;
            this.inBuildingMakeWeaponImage3.Visible = false;
            this.inBuildingMakeWeaponImage4.Visible = false;
            this.selectedBuilding = building;
            if (((this.selectedBuilding != null) && (this.selectedBuilding.buildingType != 0)) && ((GameEngine.Instance.World.isAccountPremium() && !this.m_villageIsCapital) || (this.m_villageIsCapital && GameEngine.Instance.World.isUserVillage(village.VillageID))))
            {
                this.inBuildingMoveButton.Visible = true;
            }
            else
            {
                this.inBuildingMoveButton.Visible = false;
            }
            BaseImage image = null;
            switch (this.selectedBuilding.buildingType)
            {
                case 0:
                {
                    this.inBuildingMoveButton.Visible = false;
                    int num7 = GameEngine.Instance.World.getRank();
                    if (num7 >= 10)
                    {
                        if (num7 < 15)
                        {
                            image = GFXLibrary.r_building_panel_bld_civ_hall_2;
                        }
                        else if (num7 < 0x15)
                        {
                            image = GFXLibrary.r_building_panel_bld_civ_hall_3;
                        }
                        else
                        {
                            image = GFXLibrary.r_building_panel_bld_civ_hall_3;
                        }
                    }
                    else
                    {
                        image = GFXLibrary.r_building_panel_bld_civ_hall_1;
                    }
                    this.inBuildingDeleteButton.Visible = false;
                    if (!this.ViewOnly)
                    {
                        this.inBuildingGenericButton.Visible = true;
                        this.inBuildingGenericButton.Text.Text = SK.Text("VillageMapPanel_Hold_Banquet", "Hold Banquet");
                    }
                    if (building.isComplete())
                    {
                        this.inBuildingGenericButton2.Visible = true;
                        this.inBuildingGenericButton2.Text.Text = SK.Text("VillageMapPanel_Resources", "Resources");
                    }
                    goto Label_10D1;
                }
                case 1:
                case 0x27:
                case 40:
                case 0x4c:
                case 0x4d:
                    switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
                    {
                        case 3:
                        case 4:
                            image = GFXLibrary.r_building_panel_bld_civ_house_2;
                            goto Label_06DF;

                        case 5:
                        case 6:
                            image = GFXLibrary.r_building_panel_bld_civ_house_3;
                            goto Label_06DF;

                        case 7:
                        case 8:
                            image = GFXLibrary.r_building_panel_bld_civ_house_4;
                            goto Label_06DF;

                        case 9:
                        case 10:
                            image = GFXLibrary.r_building_panel_bld_civ_house_5;
                            goto Label_06DF;
                    }
                    image = GFXLibrary.r_building_panel_bld_civ_house_1;
                    break;

                case 2:
                    image = GFXLibrary.r_building_panel_bld_icon_ind_stockpile;
                    this.inBuildingDeleteButton.Visible = false;
                    if (building.isComplete())
                    {
                        this.inBuildingGenericButton.Visible = true;
                        this.inBuildingGenericButton.Text.Text = SK.Text("VillageMapPanel_Resources", "Resources");
                    }
                    goto Label_10D1;

                case 3:
                    image = GFXLibrary.r_building_panel_bld_icon_food_granary;
                    this.inBuildingDeleteButton.Visible = false;
                    if (building.isComplete())
                    {
                        this.inBuildingGenericButton.Visible = true;
                        this.inBuildingGenericButton.Text.Text = SK.Text("VillageMapPanel_Resources", "Resources");
                    }
                    goto Label_10D1;

                case 6:
                    image = GFXLibrary.r_building_panel_bld_icon_ind_woodcutters_hut;
                    goto Label_10D1;

                case 7:
                    image = GFXLibrary.r_building_panel_bld_icon_ind_stone_quarry;
                    goto Label_10D1;

                case 8:
                    image = GFXLibrary.r_building_panel_bld_icon_ind_iron_mine;
                    goto Label_10D1;

                case 9:
                    image = GFXLibrary.r_building_panel_bld_icon_ind_pitch_rig;
                    goto Label_10D1;

                case 12:
                    image = GFXLibrary.r_building_panel_bld_icon_food_brewery;
                    goto Label_10D1;

                case 13:
                    image = GFXLibrary.r_building_panel_bld_icon_food_apple_orchard;
                    goto Label_10D1;

                case 14:
                    image = GFXLibrary.r_building_panel_bld_icon_food_bakery;
                    goto Label_10D1;

                case 15:
                    image = GFXLibrary.r_building_panel_bld_icon_food_vegetable_farm;
                    goto Label_10D1;

                case 0x10:
                    image = GFXLibrary.r_building_panel_bld_icon_food_pig_farm;
                    goto Label_10D1;

                case 0x11:
                    image = GFXLibrary.r_building_panel_bld_icon_food_dairy_farm;
                    goto Label_10D1;

                case 0x12:
                    image = GFXLibrary.r_building_panel_bld_icon_food_fishing_jetty;
                    goto Label_10D1;

                case 0x13:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_tailers_workshop;
                    goto Label_10D1;

                case 0x15:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_carpenters_workshop;
                    goto Label_10D1;

                case 0x16:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_hunters_hut;
                    goto Label_10D1;

                case 0x17:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_salt_pan;
                    goto Label_10D1;

                case 0x18:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_spice_docs;
                    goto Label_10D1;

                case 0x19:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_silk_docs;
                    goto Label_10D1;

                case 0x1a:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_metalworks_workshop;
                    goto Label_10D1;

                case 0x1c:
                    if (building.isComplete())
                    {
                        this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Max_Made_Per_Day", "Max. Made Per Day");
                        this.inBuildingMakeWeaponLabel1.Visible = true;
                        this.inBuildingMakeWeaponLabel2.Visible = true;
                        if (building.serverCalcRate <= 0.0)
                        {
                            this.inBuildingMakeWeaponLabel2.Text = "0";
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel2.Text = village.getWeaponsPerDayValue(building).ToString();
                        }
                        this.updateWeaponProductionInfo();
                    }
                    image = GFXLibrary.r_building_panel_bld_icon_mil_pole_turner;
                    goto Label_10D1;

                case 0x1d:
                    if (building.isComplete())
                    {
                        this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Max_Made_Per_Day", "Max. Made Per Day");
                        this.inBuildingMakeWeaponLabel1.Visible = true;
                        this.inBuildingMakeWeaponLabel2.Visible = true;
                        if (building.serverCalcRate <= 0.0)
                        {
                            this.inBuildingMakeWeaponLabel2.Text = "0";
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel2.Text = village.getWeaponsPerDayValue(building).ToString();
                        }
                        this.updateWeaponProductionInfo();
                    }
                    image = GFXLibrary.r_building_panel_bld_icon_mil_fletcher;
                    goto Label_10D1;

                case 30:
                    if (building.isComplete())
                    {
                        this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Max_Made_Per_Day", "Max. Made Per Day");
                        this.inBuildingMakeWeaponLabel1.Visible = true;
                        this.inBuildingMakeWeaponLabel2.Visible = true;
                        if (building.serverCalcRate <= 0.0)
                        {
                            this.inBuildingMakeWeaponLabel2.Text = "0";
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel2.Text = village.getWeaponsPerDayValue(building).ToString();
                        }
                        this.updateWeaponProductionInfo();
                    }
                    image = GFXLibrary.r_building_panel_bld_icon_mil_blacksmith;
                    goto Label_10D1;

                case 0x1f:
                    if (building.isComplete())
                    {
                        this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Max_Made_Per_Day", "Max. Made Per Day");
                        this.inBuildingMakeWeaponLabel1.Visible = true;
                        this.inBuildingMakeWeaponLabel2.Visible = true;
                        if (building.serverCalcRate <= 0.0)
                        {
                            this.inBuildingMakeWeaponLabel2.Text = "0";
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel2.Text = village.getWeaponsPerDayValue(building).ToString();
                        }
                        this.updateWeaponProductionInfo();
                    }
                    image = GFXLibrary.r_building_panel_bld_icon_mil_armourer;
                    goto Label_10D1;

                case 0x20:
                    if (building.isComplete())
                    {
                        this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Max_Made_Per_Day", "Max. Made Per Day");
                        this.inBuildingMakeWeaponLabel1.Visible = true;
                        this.inBuildingMakeWeaponLabel2.Visible = true;
                        if (building.serverCalcRate <= 0.0)
                        {
                            this.inBuildingMakeWeaponLabel2.Text = "0";
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel2.Text = village.getWeaponsPerDayValueD(building).ToString("0.#");
                        }
                        this.updateWeaponProductionInfo();
                    }
                    image = GFXLibrary.r_building_panel_bld_icon_mil_siege_workshop;
                    goto Label_10D1;

                case 0x21:
                    image = GFXLibrary.r_building_panel_bld_icon_hon_vinyard;
                    goto Label_10D1;

                case 0x22:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_small_church;
                    goto Label_10D1;

                case 0x23:
                    image = GFXLibrary.r_building_panel_bld_icon_food_inn;
                    this.inBuildingDeleteButton.Visible = false;
                    if (building.isComplete())
                    {
                        this.inBuildingGenericButton.Visible = true;
                        this.inBuildingGenericButton.Text.Text = SK.Text("VillageMapPanel_Resources", "Resources");
                    }
                    goto Label_10D1;

                case 0x24:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_medium_church;
                    goto Label_10D1;

                case 0x25:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_large_church;
                    goto Label_10D1;

                case 0x26:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_garden_01;
                    goto Label_10D1;

                case 0x29:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_garden_02;
                    goto Label_10D1;

                case 0x2a:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_garden_03;
                    goto Label_10D1;

                case 0x2b:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_garden_04;
                    goto Label_10D1;

                case 0x2c:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_garden_05;
                    goto Label_10D1;

                case 0x2d:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_garden_06;
                    goto Label_10D1;

                case 0x31:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_large_garden_01png;
                    goto Label_10D1;

                case 50:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_large_garden_02;
                    goto Label_10D1;

                case 0x33:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_large_garden_03;
                    goto Label_10D1;

                case 0x36:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_statue_01;
                    goto Label_10D1;

                case 0x37:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_statue_02;
                    goto Label_10D1;

                case 0x38:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_statue_03;
                    goto Label_10D1;

                case 0x39:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_small_statue_04;
                    goto Label_10D1;

                case 0x3a:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_large_statue_01;
                    goto Label_10D1;

                case 0x3b:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_large_statue_02;
                    goto Label_10D1;

                case 60:
                    image = GFXLibrary.r_building_panel_bld_civ_dec_dovecote;
                    goto Label_10D1;

                case 0x3d:
                    image = GFXLibrary.r_building_panel_bld_jus_stocks;
                    goto Label_10D1;

                case 0x3e:
                    image = GFXLibrary.r_building_panel_bld_jus_burning_post;
                    goto Label_10D1;

                case 0x3f:
                    image = GFXLibrary.r_building_panel_bld_jus_gibbet;
                    goto Label_10D1;

                case 0x40:
                    image = GFXLibrary.r_building_panel_bld_jus_stretching_rack;
                    goto Label_10D1;

                case 0x41:
                    image = GFXLibrary.r_building_panel_bld_ent_maypole;
                    goto Label_10D1;

                case 0x42:
                    image = GFXLibrary.r_building_panel_bld_ent_dancing_bear;
                    goto Label_10D1;

                case 0x43:
                    image = GFXLibrary.r_building_panel_bld_ent_theatre;
                    goto Label_10D1;

                case 0x44:
                    image = GFXLibrary.r_building_panel_bld_ent_jesters_court;
                    goto Label_10D1;

                case 0x45:
                    image = GFXLibrary.r_building_panel_bld_ent_troubadours_arbor;
                    goto Label_10D1;

                case 70:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_01;
                    goto Label_10D1;

                case 0x47:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_02;
                    goto Label_10D1;

                case 0x48:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_03;
                    goto Label_10D1;

                case 0x49:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_04;
                    goto Label_10D1;

                case 0x4a:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_01;
                    goto Label_10D1;

                case 0x4b:
                    image = GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_02;
                    goto Label_10D1;

                case 0x4e:
                    image = GFXLibrary.r_building_panel_bld_icon_ind_market;
                    if (building.isComplete() && !this.ViewOnly)
                    {
                        this.inBuildingGenericButton.Visible = true;
                        this.inBuildingGenericButton.Text.Text = SK.Text("VillageMapPanel_Transfer", "Transfer");
                        this.inBuildingGenericButton2.Visible = true;
                        this.inBuildingGenericButton2.Text.Text = SK.Text("VillageMapPanel_Stock_Exchange", "Stock Exchange");
                    }
                    goto Label_10D1;

                case 0x4f:
                    image = GFXLibrary.townbuilding_Woodcutter_normal;
                    goto Label_10D1;

                case 80:
                    image = GFXLibrary.townbuilding_stonequarry_normal;
                    goto Label_10D1;

                case 0x51:
                    image = GFXLibrary.townbuilding_iron_normal;
                    goto Label_10D1;

                case 0x52:
                    image = GFXLibrary.townbuilding_pitch_normal;
                    goto Label_10D1;

                case 0x53:
                    image = GFXLibrary.townbuilding_ale_normal;
                    goto Label_10D1;

                case 0x54:
                    image = GFXLibrary.townbuilding_apples_normal;
                    goto Label_10D1;

                case 0x55:
                    image = GFXLibrary.townbuilding_cheese_normal;
                    goto Label_10D1;

                case 0x56:
                    image = GFXLibrary.townbuilding_meat_normal;
                    goto Label_10D1;

                case 0x57:
                    image = GFXLibrary.townbuilding_bread_normal;
                    goto Label_10D1;

                case 0x58:
                    image = GFXLibrary.townbuilding_veg_normal;
                    goto Label_10D1;

                case 0x59:
                    image = GFXLibrary.townbuilding_fish_normal;
                    goto Label_10D1;

                case 90:
                    image = GFXLibrary.townbuilding_bows_normal;
                    goto Label_10D1;

                case 0x5b:
                    image = GFXLibrary.townbuilding_pikes_normal;
                    goto Label_10D1;

                case 0x5c:
                    image = GFXLibrary.townbuilding_armour_normal;
                    goto Label_10D1;

                case 0x5d:
                    image = GFXLibrary.townbuilding_sword_normal;
                    goto Label_10D1;

                case 0x5e:
                    image = GFXLibrary.townbuilding_catapults_normal;
                    goto Label_10D1;

                case 0x5f:
                    image = GFXLibrary.townbuilding_venison_normal;
                    goto Label_10D1;

                case 0x60:
                    image = GFXLibrary.townbuilding_wine_normal;
                    goto Label_10D1;

                case 0x61:
                    image = GFXLibrary.townbuilding_salt_normal;
                    goto Label_10D1;

                case 0x62:
                    image = GFXLibrary.townbuilding_carpenter_normal;
                    goto Label_10D1;

                case 0x63:
                    image = GFXLibrary.townbuilding_tailor_normal;
                    goto Label_10D1;

                case 100:
                    image = GFXLibrary.townbuilding_metalware_normal;
                    goto Label_10D1;

                case 0x65:
                    image = GFXLibrary.townbuilding_spice_normal;
                    goto Label_10D1;

                case 0x66:
                    image = GFXLibrary.townbuilding_silk_normal;
                    goto Label_10D1;

                case 0x67:
                    image = GFXLibrary.townbuilding_architectsguild_normal;
                    goto Label_10D1;

                case 0x68:
                    image = GFXLibrary.townbuilding_Labourersbillets_normal;
                    goto Label_10D1;

                case 0x69:
                    image = GFXLibrary.townbuilding_castellanshouse_normal;
                    goto Label_10D1;

                case 0x6a:
                    image = GFXLibrary.townbuilding_sergeantsatarmsoffice_normal;
                    goto Label_10D1;

                case 0x6b:
                    image = GFXLibrary.townbuilding_stables_normal;
                    goto Label_10D1;

                case 0x6c:
                    image = GFXLibrary.townbuilding_barracks_normal;
                    goto Label_10D1;

                case 0x6d:
                    image = GFXLibrary.townbuilding_peasntshall_normal;
                    goto Label_10D1;

                case 110:
                    image = GFXLibrary.townbuilding_archeryrange_normal;
                    goto Label_10D1;

                case 0x6f:
                    image = GFXLibrary.townbuilding_pikemandrillyard_normal;
                    goto Label_10D1;

                case 0x70:
                    image = GFXLibrary.townbuilding_combatarena_normal;
                    goto Label_10D1;

                case 0x71:
                    image = GFXLibrary.townbuilding_siegeengineersguild_normal;
                    goto Label_10D1;

                case 0x72:
                    image = GFXLibrary.townbuilding_officersquarters_normal;
                    goto Label_10D1;

                case 0x73:
                    image = GFXLibrary.townbuilding_militaryschool_normal;
                    goto Label_10D1;

                case 0x74:
                    image = GFXLibrary.townbuilding_supplydepot_normal;
                    goto Label_10D1;

                case 0x75:
                    image = GFXLibrary.townbuilding_townhall_normal;
                    goto Label_10D1;

                case 0x76:
                    image = GFXLibrary.townbuilding_church_normal;
                    goto Label_10D1;

                case 0x77:
                    image = GFXLibrary.townbuilding_towngarden_normal;
                    goto Label_10D1;

                case 120:
                    image = GFXLibrary.townbuilding_statue_normal;
                    goto Label_10D1;

                case 0x79:
                    image = GFXLibrary.townbuilding_turretmaker_normal;
                    goto Label_10D1;

                case 0x7a:
                    image = GFXLibrary.townbuilding_tunnellorsguild_normal;
                    goto Label_10D1;

                case 0x7b:
                    image = GFXLibrary.townbuilding_ballistamaker_normal;
                    goto Label_10D1;

                default:
                    goto Label_10D1;
            }
        Label_06DF:
            if (village != null)
            {
                int num5 = ResearchData.researchHousingLevels[GameEngine.Instance.World.userResearchData.Research_HousingCapacity];
                VillageMapBuilding building2 = village.findBuildingType(0);
                if (building2 != null)
                {
                    int dist = VillageBuildingsData.getMapDistance(building2.buildingLocation, building.buildingLocation);
                    num5 += VillageBuildingsData.getHousingCapacityBasedOnDistance(GameEngine.Instance.LocalWorldData, dist);
                    this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Capacity", "Capacity");
                    this.inBuildingMakeWeaponLabel2.Text = num5.ToString();
                    this.inBuildingMakeWeaponLabel1.Visible = true;
                    this.inBuildingMakeWeaponLabel2.Visible = true;
                }
            }
        Label_10D1:
            num13 = 1;
            if (GameEngine.Instance.World.ThirdAgeWorld)
            {
                num13 = 4;
            }
            switch (this.selectedBuilding.buildingType)
            {
                case 6:
                case 7:
                case 8:
                case 9:
                case 12:
                case 13:
                case 14:
                case 15:
                case 0x10:
                case 0x11:
                case 0x12:
                case 0x13:
                case 0x15:
                case 0x16:
                case 0x17:
                case 0x18:
                case 0x19:
                case 0x1a:
                case 0x21:
                {
                    if ((building.calcRate == 0.0) || !building.complete)
                    {
                        goto Label_1C60;
                    }
                    this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_This_Building", "This Building");
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_All", "All");
                    this.inBuildingMakeWeaponLabel0.Visible = true;
                    this.inBuildingMakeWeaponLabel1.Visible = true;
                    this.inBuildingMakeWeaponLabel2.Visible = true;
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    if (building.calcRate <= 0.0)
                    {
                        this.inBuildingMakeWeaponLabel2.Text = "0";
                        break;
                    }
                    double num25 = 86400.0 / building.calcRate;
                    double num26 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(building.buildingType), building.buildingType);
                    num25 *= num26;
                    int num38 = (int) num25;
                    this.inBuildingMakeWeaponLabel2.Text = num38.ToString("N", nFI);
                    break;
                }
                case 0x22:
                    this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                    this.inBuildingMakeWeaponLabel2.Text = (GameEngine.Instance.LocalWorldData.HonourBuilding_Chapel * num13).ToString();
                    this.inBuildingMakeWeaponLabel1.Visible = true;
                    this.inBuildingMakeWeaponLabel2.Visible = true;
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Faith_Points", "Faith Points");
                    this.inBuildingMakeWeaponLabel4.Text = GameEngine.Instance.LocalWorldData.FaithPoints_Chapel.ToString();
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    goto Label_1C60;

                case 0x24:
                    this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                    this.inBuildingMakeWeaponLabel2.Text = (GameEngine.Instance.LocalWorldData.HonourBuilding_Church * num13).ToString();
                    this.inBuildingMakeWeaponLabel1.Visible = true;
                    this.inBuildingMakeWeaponLabel2.Visible = true;
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Faith_Points", "Faith Points");
                    this.inBuildingMakeWeaponLabel4.Text = GameEngine.Instance.LocalWorldData.FaithPoints_Church.ToString();
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    goto Label_1C60;

                case 0x25:
                    this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                    this.inBuildingMakeWeaponLabel2.Text = (GameEngine.Instance.LocalWorldData.HonourBuilding_Cathedral * num13).ToString();
                    this.inBuildingMakeWeaponLabel1.Visible = true;
                    this.inBuildingMakeWeaponLabel2.Visible = true;
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Faith_Points", "Faith Points");
                    this.inBuildingMakeWeaponLabel4.Text = GameEngine.Instance.LocalWorldData.FaithPoints_Cathedral.ToString();
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    goto Label_1C60;

                case 0x26:
                case 0x29:
                case 0x2a:
                case 0x2b:
                case 0x2c:
                case 0x2d:
                    if (village != null)
                    {
                        VillageMapBuilding building3 = village.findBuildingType(0);
                        if (building3 != null)
                        {
                            double num14 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_SmallGarden, building3.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num14.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x31:
                case 50:
                case 0x33:
                    if (village != null)
                    {
                        VillageMapBuilding building4 = village.findBuildingType(0);
                        if (building4 != null)
                        {
                            double num15 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_LargeGarden, building4.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num15.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                    if (village != null)
                    {
                        VillageMapBuilding building5 = village.findBuildingType(0);
                        if (building5 != null)
                        {
                            double num16 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_SmallStatue, building5.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num16.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x3a:
                case 0x3b:
                    if (village != null)
                    {
                        VillageMapBuilding building6 = village.findBuildingType(0);
                        if (building6 != null)
                        {
                            double num17 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_LargeStatue, building6.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num17.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 60:
                    if (village != null)
                    {
                        VillageMapBuilding building7 = village.findBuildingType(0);
                        if (building7 != null)
                        {
                            double num18 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Dovecote, building7.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num18.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x3d:
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Popularity_Penalty", "Popularity Penalty");
                    this.inBuildingMakeWeaponLabel4.Text = "-5";
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    if (village != null)
                    {
                        VillageMapBuilding building8 = village.findBuildingType(0);
                        if (building8 != null)
                        {
                            double num19 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Stocks, building8.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num19.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x3e:
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Popularity_Penalty", "Popularity Penalty");
                    this.inBuildingMakeWeaponLabel4.Text = "-5";
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    if (village != null)
                    {
                        VillageMapBuilding building9 = village.findBuildingType(0);
                        if (building9 != null)
                        {
                            double num20 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_BurningPost, building9.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num20.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x3f:
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Popularity_Penalty", "Popularity Penalty");
                    this.inBuildingMakeWeaponLabel4.Text = "-5";
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    if (village != null)
                    {
                        VillageMapBuilding building10 = village.findBuildingType(0);
                        if (building10 != null)
                        {
                            double num21 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Gibbet, building10.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num21.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x40:
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Popularity_Penalty", "Popularity Penalty");
                    this.inBuildingMakeWeaponLabel4.Text = "-5";
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    if (village != null)
                    {
                        VillageMapBuilding building11 = village.findBuildingType(0);
                        if (building11 != null)
                        {
                            double num22 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Rack, building11.buildingLocation, building.buildingLocation) * num13;
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Honour_Bonus", "Honour Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num22.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                    if (village != null)
                    {
                        VillageMapBuilding building12 = village.findBuildingType(0);
                        if (building12 != null)
                        {
                            int num23 = VillageBuildingsData.getMapDistance(building12.buildingLocation, building.buildingLocation);
                            int num24 = VillageBuildingsData.getBuildingPopularityBasedOnDistance(GameEngine.Instance.LocalWorldData, num23);
                            this.inBuildingMakeWeaponLabel1.Text = SK.Text("VillageMapPanel_Popularity_Bonus", "Popularity Bonus");
                            this.inBuildingMakeWeaponLabel2.Text = num24.ToString();
                            this.inBuildingMakeWeaponLabel1.Visible = true;
                            this.inBuildingMakeWeaponLabel2.Visible = true;
                        }
                    }
                    goto Label_1C60;

                case 70:
                case 0x47:
                case 0x48:
                case 0x49:
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Faith_Points", "Faith Points");
                    this.inBuildingMakeWeaponLabel4.Text = GameEngine.Instance.LocalWorldData.FaithPoints_SmallShrine.ToString();
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    goto Label_1C60;

                case 0x4a:
                case 0x4b:
                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Faith_Points", "Faith Points");
                    this.inBuildingMakeWeaponLabel4.Text = GameEngine.Instance.LocalWorldData.FaithPoints_LargeShrine.ToString();
                    this.inBuildingMakeWeaponLabel3.Visible = true;
                    this.inBuildingMakeWeaponLabel4.Visible = true;
                    goto Label_1C60;

                default:
                    goto Label_1C60;
            }
            if (village != null)
            {
                double num27 = village.getResourceProductionPerDay(building.buildingType);
                this.inBuildingMakeWeaponLabel4.Text = ((int) num27).ToString("N", nFI);
            }
        Label_1C60:
            if (!VillageBuildingsData.buildingRequiresWorker(building.buildingType))
            {
                this.inBuildingIndustryAllOnButton.Visible = false;
                this.inBuildingIndustryThisOnButton.Visible = false;
                this.inBuildingAllIndustryOnButton.Visible = false;
            }
            else
            {
                this.inBuildingIndustryAllOnButton.Visible = true;
                this.inBuildingIndustryThisOnButton.Visible = true;
                this.inBuildingAllIndustryOnButton.Visible = true;
            }
            if (building.buildingActive)
            {
                this.inBuildingIndustryThisOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_This_Off", "Turn This Off");
                this.inBuildingIndustryAllOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_All_Off", "Turn All Off");
                this.inBuildingAllIndustryOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_All_Industries_Off", "Turn All Industries Off");
                this.inBuildingAllIndustryOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnAllIndustryOnClicked), "VillageMapPanel_all_industry_off");
                this.inBuildingIndustryAllOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnAllOnClicked), "VillageMapPanel_all_off");
                this.inBuildingIndustryThisOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnThisOnClicked), "VillageMapPanel_this_off");
            }
            else
            {
                this.inBuildingIndustryThisOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_This_On", "Turn This On");
                this.inBuildingIndustryAllOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_All_On", "Turn All On");
                this.inBuildingAllIndustryOnButton.Text.Text = SK.Text("VillageMapPanel_Turn_All_Industries_On", "Turn All Industries On");
                this.inBuildingAllIndustryOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnAllIndustryOnClicked), "VillageMapPanel_all_industry_on");
                this.inBuildingIndustryAllOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnAllOnClicked), "VillageMapPanel_all_on");
                this.inBuildingIndustryThisOnButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.turnThisOnClicked), "VillageMapPanel_this_on");
            }
            this.inBuildingCompleteLabel.Visible = false;
            this.inBuildingCompleteLabel2.Visible = false;
            if (!building.isComplete())
            {
                this.inBuildingCompleteLabel.Visible = true;
                this.inBuildingCompleteLabel2.Visible = true;
                this.inBuildingCompleteLabel.Text = SK.Text("VillageMapPanel_Completion_@", "Completion @");
                DateTime time = VillageMap.getCurrentServerTime();
                if (building.completionTime.Date == time.Date)
                {
                    this.inBuildingCompleteLabel2.Text = SK.Text("VillageMapPanel_Today", "Today") + " " + building.completionTime.ToLongTimeString();
                }
                else
                {
                    this.inBuildingCompleteLabel2.Text = building.completionTime.ToShortDateString() + " " + building.completionTime.ToLongTimeString();
                }
            }
            else if (building.isDeleting())
            {
                this.inBuildingCompleteLabel.Visible = true;
                this.inBuildingCompleteLabel2.Visible = true;
                this.inBuildingCompleteLabel.Text = SK.Text("VillageMapPanel_Deletion_@", "Deletion @");
                DateTime time2 = VillageMap.getCurrentServerTime();
                if (building.deletionTime.Date == time2.Date)
                {
                    this.inBuildingCompleteLabel2.Text = SK.Text("VillageMapPanel_Today", "Today") + " " + building.deletionTime.ToLongTimeString();
                }
                else
                {
                    this.inBuildingCompleteLabel2.Text = building.deletionTime.ToShortDateString() + " " + building.deletionTime.ToLongTimeString();
                }
            }
            this.inBuildingTypeImage.Image = (Image) image;
            if (this.ViewOnly)
            {
                this.inBuildingDeleteButton.Visible = false;
                this.inBuildingIndustryAllOnButton.Visible = false;
                this.inBuildingAllIndustryOnButton.Visible = false;
                this.inBuildingIndustryThisOnButton.Visible = false;
                this.inBuildingGenericButton.Visible = false;
                this.inBuildingGenericButton2.Visible = false;
            }
            if (this.m_villageIsCapital)
            {
                this.inBuildingName.Text = "";
                this.inBuildingCapitalResourceLevelLabel1.Visible = false;
                this.inBuildingCapitalResourceLevelLabel2.Visible = false;
                this.inBuildingCapitalResourceLabel1a.Visible = false;
                this.inBuildingCapitalResourceLabel1b.Visible = false;
                this.inBuildingCapitalResourceLabel1c.Visible = false;
                this.inBuildingCapitalResourceImage1.Visible = false;
                this.inBuildingCapitalResourceLabel2a.Visible = false;
                this.inBuildingCapitalResourceLabel2b.Visible = false;
                this.inBuildingCapitalResourceLabel2c.Visible = false;
                this.inBuildingCapitalResourceImage2.Visible = false;
                this.inBuildingCapitalResourceLabel3a.Visible = false;
                this.inBuildingCapitalResourceLabel3b.Visible = false;
                this.inBuildingCapitalResourceLabel3c.Visible = false;
                this.inBuildingCapitalResourceImage3.Visible = false;
                this.inBuildingCapitalResourceLabel4a.Visible = false;
                this.inBuildingCapitalResourceLabel4b.Visible = false;
                this.inBuildingCapitalResourceLabel4c.Visible = false;
                this.inBuildingCapitalResourceImage4.Visible = false;
                this.inBuildingCapitalResourceLabel5a.Visible = false;
                this.inBuildingCapitalResourceLabel5b.Visible = false;
                this.inBuildingCapitalResourceLabel5c.Visible = false;
                this.inBuildingCapitalResourceImage5.Visible = false;
                this.inBuildingCapitalResourceLabel6a.Visible = false;
                this.inBuildingCapitalResourceLabel6b.Visible = false;
                this.inBuildingCapitalResourceLabel6c.Visible = false;
                this.inBuildingCapitalResourceImage6.Visible = false;
                this.inBuildingCapitalResourceLabel7a.Visible = false;
                this.inBuildingCapitalResourceLabel7b.Visible = false;
                this.inBuildingCapitalResourceLabel7c.Visible = false;
                this.inBuildingCapitalResourceImage7.Visible = false;
                this.inBuildingCapitalResourceLabel8a.Visible = false;
                this.inBuildingCapitalResourceLabel8b.Visible = false;
                this.inBuildingCapitalResourceLabel8c.Visible = false;
                this.inBuildingCapitalResourceImage8.Visible = false;
                if (building.isComplete())
                {
                    int rank = 0;
                    if (village.m_parishCapitalResearchData != null)
                    {
                        rank = village.m_parishCapitalResearchData.getCapitalResourceFromBuildingType(this.selectedBuilding.buildingType);
                    }
                    this.inBuildingCapitalResourceLevelLabel2.Text = rank.ToString("N", nFI);
                    int buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 0);
                    if (rank < 0)
                    {
                        buildingType = -1;
                    }
                    if (buildingType >= 0)
                    {
                        this.inBuildingCapitalResourceLevelLabel1.Visible = true;
                        this.inBuildingCapitalResourceLevelLabel2.Visible = true;
                        this.inBuildingDonateButton.Visible = true;
                    }
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 0))
                    {
                        int num30 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 0, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num30 > 0)
                        {
                            this.inBuildingCapitalResourceLabel1a.Visible = true;
                            this.inBuildingCapitalResourceLabel1b.Visible = true;
                            this.inBuildingCapitalResourceLabel1c.Visible = true;
                            this.inBuildingCapitalResourceImage1.Visible = true;
                            this.inBuildingCapitalResourceImage1.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel1a.Text = this.selectedBuilding.capitalResourceLevels[0].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel1c.Text = num30.ToString("N", nFI);
                        }
                    }
                    buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 1);
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 1))
                    {
                        int num31 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 1, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num31 > 0)
                        {
                            this.inBuildingCapitalResourceLabel2a.Visible = true;
                            this.inBuildingCapitalResourceLabel2b.Visible = true;
                            this.inBuildingCapitalResourceLabel2c.Visible = true;
                            this.inBuildingCapitalResourceImage2.Visible = true;
                            this.inBuildingCapitalResourceImage2.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel2a.Text = this.selectedBuilding.capitalResourceLevels[1].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel2c.Text = num31.ToString("N", nFI);
                        }
                    }
                    buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 2);
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 2))
                    {
                        int num32 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 2, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num32 > 0)
                        {
                            this.inBuildingCapitalResourceLabel3a.Visible = true;
                            this.inBuildingCapitalResourceLabel3b.Visible = true;
                            this.inBuildingCapitalResourceLabel3c.Visible = true;
                            this.inBuildingCapitalResourceImage3.Visible = true;
                            this.inBuildingCapitalResourceImage3.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel3a.Text = this.selectedBuilding.capitalResourceLevels[2].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel3c.Text = num32.ToString("N", nFI);
                        }
                    }
                    buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 3);
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 3))
                    {
                        int num33 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 3, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num33 > 0)
                        {
                            this.inBuildingCapitalResourceLabel4a.Visible = true;
                            this.inBuildingCapitalResourceLabel4b.Visible = true;
                            this.inBuildingCapitalResourceLabel4c.Visible = true;
                            this.inBuildingCapitalResourceImage4.Visible = true;
                            this.inBuildingCapitalResourceImage4.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel4a.Text = this.selectedBuilding.capitalResourceLevels[3].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel4c.Text = num33.ToString("N", nFI);
                        }
                    }
                    buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 4);
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 4))
                    {
                        int num34 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 4, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num34 > 0)
                        {
                            this.inBuildingCapitalResourceLabel5a.Visible = true;
                            this.inBuildingCapitalResourceLabel5b.Visible = true;
                            this.inBuildingCapitalResourceLabel5c.Visible = true;
                            this.inBuildingCapitalResourceImage5.Visible = true;
                            this.inBuildingCapitalResourceImage5.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel5a.Text = this.selectedBuilding.capitalResourceLevels[4].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel5c.Text = num34.ToString("N", nFI);
                        }
                    }
                    buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 5);
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 5))
                    {
                        int num35 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 5, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num35 > 0)
                        {
                            this.inBuildingCapitalResourceLabel6a.Visible = true;
                            this.inBuildingCapitalResourceLabel6b.Visible = true;
                            this.inBuildingCapitalResourceLabel6c.Visible = true;
                            this.inBuildingCapitalResourceImage6.Visible = true;
                            this.inBuildingCapitalResourceImage6.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel6a.Text = this.selectedBuilding.capitalResourceLevels[5].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel6c.Text = num35.ToString("N", nFI);
                        }
                    }
                    buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 6);
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 6))
                    {
                        int num36 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 6, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num36 > 0)
                        {
                            this.inBuildingCapitalResourceLabel7a.Visible = true;
                            this.inBuildingCapitalResourceLabel7b.Visible = true;
                            this.inBuildingCapitalResourceLabel7c.Visible = true;
                            this.inBuildingCapitalResourceImage7.Visible = true;
                            this.inBuildingCapitalResourceImage7.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel7a.Text = this.selectedBuilding.capitalResourceLevels[6].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel7c.Text = num36.ToString("N", nFI);
                        }
                    }
                    buildingType = VillageBuildingsData.getRequiredResourceType(this.selectedBuilding.buildingType, 7);
                    if ((buildingType >= 0) && (this.selectedBuilding.capitalResourceLevels.Length > 7))
                    {
                        int num37 = VillageBuildingsData.getRequiredResourceTypeLevel(this.selectedBuilding.buildingType, 7, rank, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld);
                        if (num37 > 0)
                        {
                            this.inBuildingCapitalResourceLabel8a.Visible = true;
                            this.inBuildingCapitalResourceLabel8b.Visible = true;
                            this.inBuildingCapitalResourceLabel8c.Visible = true;
                            this.inBuildingCapitalResourceImage8.Visible = true;
                            this.inBuildingCapitalResourceImage8.Image = (Image) getSmallResourceIcon(buildingType);
                            this.inBuildingCapitalResourceLabel8a.Text = this.selectedBuilding.capitalResourceLevels[7].ToString("N", nFI);
                            this.inBuildingCapitalResourceLabel8c.Text = num37.ToString("N", nFI);
                        }
                    }
                }
            }
            base.Invalidate();
        }

        public void showMigration(int popularity, string timeLeftString, int totalPeople, int housingCapacity)
        {
            if (popularity >= 0)
            {
                this.headerImage.Image = (Image) GFXLibrary.r_popularity_bar_back_green;
            }
            else if (popularity < 0)
            {
                this.headerImage.Image = (Image) GFXLibrary.r_popularity_bar_back_red;
            }
            else
            {
                this.headerImage.Image = (Image) GFXLibrary.r_popularity_bar_back_yellow;
            }
            this.popularityLabel.Text = popularity.ToString();
            if (popularity == 0)
            {
                this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_stand;
            }
            else if (popularity < 0)
            {
                int num = VillageBuildingsData.getImmigrationNumberOfPeasantsLeaving(popularity);
                if (num <= 1)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out;
                }
                else if (num <= 2)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x2;
                }
                else if (num <= 3)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x3;
                }
                else if (num <= 4)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x4;
                }
                else if (num <= 5)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x5;
                }
                else if (num <= 6)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x6;
                }
                else if (num <= 7)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x7;
                }
                else if (num <= 8)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x8;
                }
                else if (num <= 9)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x9;
                }
                else if (num <= 10)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_out_x10;
                }
            }
            else
            {
                int num2 = VillageBuildingsData.getImmigrationNumberOfPeasants(popularity);
                if (num2 <= 1)
                {
                    this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_in;
                }
                else
                {
                    switch (num2)
                    {
                        case 2:
                            this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_in_x2;
                            break;

                        case 3:
                            this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_in_x3;
                            break;

                        case 4:
                            this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_in_x4;
                            break;

                        case 5:
                            this.immChangeImage.Image = (Image) GFXLibrary.r_popularity_bar_walker_in_x5;
                            break;
                    }
                }
            }
            if (timeLeftString.Length > 0)
            {
                this.arrivesInLabel.Visible = true;
                this.arrivesInTimeLabel.Visible = true;
                this.arrivesInTimeLabel.Text = timeLeftString;
                if (popularity > 0)
                {
                    this.arrivesInLabel.Text = SK.Text("VillageMapPanel_Arrives_In_X", "Arrives in") + " :";
                }
                else
                {
                    this.arrivesInLabel.Text = SK.Text("VillageMapPanel_Leaves_In_X", "Leaves in") + " :";
                }
            }
            else
            {
                this.arrivesInLabel.Visible = false;
                this.arrivesInTimeLabel.Visible = false;
            }
            if (housingCapacity > 0)
            {
                this.housingLabel.Text = (((totalPeople * 100) / housingCapacity)).ToString() + "%";
            }
            this.housingOccupancyValueLabel.Text = this.housingLabel.Text;
            double num3 = VillageBuildingsData.getHousingPopularityLevel(totalPeople, housingCapacity);
            this.housingPopLabel.Text = num3.ToString();
            if (num3 < 0.0)
            {
                this.popIndent4Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_red;
            }
            else if (num3 > 0.0)
            {
                this.popIndent4Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            }
            else
            {
                this.popIndent4Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_tan;
            }
            this.housingCapacityValueLabel.Text = housingCapacity.ToString();
            this.populationValueLabel.Text = totalPeople.ToString();
        }

        public void showNewInterface()
        {
            this.closeInBuildingPanel();
            this.closeBuildingPanel();
            this.closePopularityPanel();
            this.closeInfo1Panel();
            this.showExtras();
            this.currentEventID = 0;
            this.currentHeight = 1;
            this.currentInBuildingHeight = 1;
            this.currentExtensionHeight = 1;
            this.currentBuildingHeight = 1;
        }

        public void showPopEvents(PopEventData[] popEvents, DateTime curTime)
        {
            int num = 0;
            bool flag = false;
            if (popEvents != null)
            {
                this.eventsLabel.Text = popEvents.Length.ToString();
            }
            else
            {
                this.eventsLabel.Text = "0";
            }
            int num2 = 0;
            if (popEvents.Length == 2)
            {
                num2 = 1;
            }
            else if (popEvents.Length == 2)
            {
                num2 = 2;
            }
            if ((popEvents != null) && (popEvents.Length > 0))
            {
                PopEventData data = null;
                PopEventData data2 = null;
                PopEventData data3 = null;
                foreach (PopEventData data4 in popEvents)
                {
                    num += data4.eventEffect;
                    if ((data2 == null) || (data4.eventEffect < data2.eventEffect))
                    {
                        data2 = data4;
                    }
                    if ((data3 == null) || (data4.eventEffect > data3.eventEffect))
                    {
                        data3 = data4;
                    }
                }
                if (num >= 0)
                {
                    data = data3;
                }
                else
                {
                    data = data2;
                }
                if (data != null)
                {
                    flag = true;
                    switch (data.eventType)
                    {
                        case 0x2711:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_5;
                            break;

                        case 0x2712:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_3;
                            break;

                        case 0x2713:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_5;
                            break;

                        case 0x271b:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_5;
                            break;

                        case 0x271c:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_4;
                            break;

                        case 0x271d:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_3;
                            break;

                        case 0x271e:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_2;
                            break;

                        case 0x271f:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_1;
                            break;

                        case 0x2720:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_neutral;
                            break;

                        case 0x2721:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_1;
                            break;

                        case 0x2722:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_2;
                            break;

                        case 0x2723:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_3;
                            break;

                        case 0x2724:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_4;
                            break;

                        case 0x2725:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_5;
                            break;

                        case 1:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_beginner;
                            break;

                        case 0x2775:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_blessing;
                            break;

                        case 0x2776:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_inquisition;
                            break;

                        case 0x2b0c:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle;
                            break;

                        case 0x4e21:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle;
                            break;

                        case 0x2af9:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_bandits;
                            break;

                        case 0x2afa:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_wolves;
                            break;

                        case 0x2afb:
                            if (num >= 0)
                            {
                                this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle_green;
                            }
                            else
                            {
                                this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle;
                            }
                            break;

                        case 0x2b02:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_plague;
                            break;

                        default:
                            this.popImage6.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_plague;
                            break;
                    }
                    this.eventsLabel.Visible = true;
                    this.popImage6.Visible = true;
                    this.eventPopImage.Visible = true;
                    this.eventTypeImage.Visible = true;
                    this.eventBarImage.Visible = true;
                    this.eventDaysLabel.Visible = true;
                    this.eventTimeLabel.Visible = true;
                    if (this.currentEventID >= popEvents.Length)
                    {
                        this.currentEventID = 0;
                    }
                    else if (this.currentEventID < 0)
                    {
                        this.currentEventID = popEvents.Length - 1;
                    }
                    PopEventData data5 = popEvents[this.currentEventID];
                    bool flag2 = true;
                    switch (data5.eventType)
                    {
                        case 0x2711:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Bad_Weather", "Bad Weather");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_5;
                            break;

                        case 0x2712:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Good_Weather", "Good Weather");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_3;
                            break;

                        case 0x2713:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Drought", "Drought");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_5;
                            break;

                        case 0x271b:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Storms", "Storms");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_5;
                            break;

                        case 0x271c:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Heavy_Rain", "Heavy Rain");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_4;
                            break;

                        case 0x271d:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Rain", "Rain");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_3;
                            break;

                        case 0x271e:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Showers", "Showers");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_2;
                            break;

                        case 0x271f:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Drizzle", "Drizzle");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_bad_1;
                            break;

                        case 0x2720:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Overcast", "Overcast");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_neutral;
                            break;

                        case 0x2721:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Break_In_The_Cloud", "Break in the Cloud");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_1;
                            break;

                        case 0x2722:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Sunny_Spells", "Sunny Spells");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_2;
                            break;

                        case 0x2723:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Fine_Weather", "Fine Weather");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_3;
                            break;

                        case 0x2724:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Bright_Sunshine", "Bright Sunshine");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_4;
                            break;

                        case 0x2725:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Heatwave", "Heatwave");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_weather_good_5;
                            break;

                        case 0x2775:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Blessing", "Blessing");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_blessing;
                            break;

                        case 0x2776:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Inquisition", "Inquisition");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_inquisition;
                            break;

                        case 0x2b0c:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Castle_Not_Enclosed", "Castle Not Enclosed");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle;
                            flag2 = false;
                            break;

                        case 0x4e21:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Festival", "Festival");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle;
                            break;

                        case 0x2af9:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Bandit_Camps", "Bandit Camps");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            flag2 = false;
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_bandits;
                            break;

                        case 0x2afa:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Wolf_Lairs", "Wolf Lairs");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            flag2 = false;
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_wolves;
                            break;

                        case 0x2afb:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_AI_Castles", "AI Castles");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            flag2 = false;
                            if (data5.eventEffect < 0)
                            {
                                this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle;
                            }
                            else
                            {
                                this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_castle_green;
                            }
                            break;

                        case 0x2b02:
                            this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Disease", "Disease");
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            flag2 = false;
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_plague;
                            break;

                        default:
                            if ((this.currentEventID + num2) == 0)
                            {
                                this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Beginner_Help", "Beginner Help");
                            }
                            else if ((this.currentEventID + num2) == 1)
                            {
                                this.eventTitleLabel.Text = SK.Text("VillageMapPanel_Welcome_Bonus", "Welcome Bonus");
                            }
                            else if ((this.currentEventID + num2) == 2)
                            {
                                this.eventTitleLabel.Text = SK.Text("VillageMapPanel_New_Player_Bonus", "New Player Bonus");
                            }
                            this.eventExtPopLabel.Text = data5.eventEffect.ToString();
                            this.eventTypeImage.Image = (Image) GFXLibrary.r_popularity_panel_events_illustration_beginner;
                            break;
                    }
                    if (flag2)
                    {
                        TimeSpan span = (TimeSpan) (data5.endTime - curTime);
                        int totalSeconds = (int) span.TotalSeconds;
                        if (totalSeconds < 0)
                        {
                            totalSeconds = 0;
                        }
                        int num4 = totalSeconds % 60;
                        int num5 = (totalSeconds / 60) % 60;
                        int num6 = (totalSeconds / 0xe10) % 0x18;
                        int num7 = totalSeconds / 0x15180;
                        string str = "";
                        string str2 = "";
                        str = str + num7.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
                        if (num6 < 10)
                        {
                            str2 = str2 + "0";
                        }
                        str2 = str2 + num6.ToString() + SK.Text("VillageMap_Hour_Abbrev", "h") + ":";
                        if (num5 < 10)
                        {
                            str2 = str2 + "0";
                        }
                        str2 = str2 + num5.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m") + ":";
                        if (num4 < 10)
                        {
                            str2 = str2 + "0";
                        }
                        str2 = str2 + num4.ToString() + SK.Text("VillageMap_Second_Abbrev", "s");
                        this.eventDaysLabel.Text = str;
                        this.eventTimeLabel.Text = str2;
                    }
                    else
                    {
                        this.eventDaysLabel.Text = "";
                        this.eventTimeLabel.Text = "";
                    }
                    if (data5.eventEffect < 0)
                    {
                        this.eventPopImage.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_red;
                        this.eventBarImage.Image = (Image) GFXLibrary.r_popularity_panel_events_textbar_red;
                    }
                    else if (data5.eventEffect >= 0)
                    {
                        this.eventPopImage.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
                        this.eventBarImage.Image = (Image) GFXLibrary.r_popularity_panel_events_textbar_green;
                    }
                    this.eventCountLabel.Text = ((this.currentEventID + 1)).ToString() + "/" + popEvents.Length.ToString();
                }
            }
            if (!flag)
            {
                this.eventPopImage.Visible = false;
                this.eventTypeImage.Visible = false;
                this.eventBarImage.Visible = false;
                this.eventsLabel.Visible = false;
                this.eventDaysLabel.Visible = false;
                this.eventTimeLabel.Visible = false;
                this.popImage6.Visible = false;
            }
            this.eventsPopLabel.Text = num.ToString();
            if (num < 0)
            {
                this.popIndent6Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_red;
            }
            else if (num > 0)
            {
                this.popIndent6Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            }
            else
            {
                this.popIndent6Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_tan;
            }
        }

        public void showStats(int taxLevel, int rationsLevel, int aleRationsLevel, int popularity, double popularityChange, string timeLeftString, double effectiveRationsLevel, int numFoodTypesEaten, double effectiveAleRationsLevel, double housingChangeLevel)
        {
            if (this.extensionType == 0)
            {
                switch (taxLevel)
                {
                    case 0:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Large_Bribe", "Large Bribe");
                        break;

                    case 1:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Medium_Bribe", "Medium Bribe");
                        break;

                    case 2:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Small_Bribe", "Small Bribe");
                        break;

                    case 3:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_No_Tax", "No Tax");
                        break;

                    case 4:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Low_Tax", "Low Tax");
                        break;

                    case 5:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Normal_Tax", "Normal Tax");
                        break;

                    case 6:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_High_Tax", "High Tax");
                        break;

                    case 7:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Very_High_Tax", "Very High Tax");
                        break;

                    case 8:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Extreme_Tax", "Extreme Tax");
                        break;

                    case 9:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Cruel_Tax", "Cruel Tax");
                        break;

                    case 10:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Very_Cruel_Tax", "Very Cruel Tax");
                        break;

                    case 11:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Extremely_Cruel_Tax", "Extremely Cruel Tax");
                        break;

                    case 12:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Savage_Tax", "Savage Tax");
                        break;

                    case 13:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Tax_Levels_Diabolic_Tax", "Diabolic Tax");
                        break;
                }
            }
            string str = "";
            switch (rationsLevel)
            {
                case 0:
                    str = "0";
                    break;

                case 1:
                    str = "1/4";
                    break;

                case 2:
                    str = "1/2";
                    break;

                case 3:
                    str = "x1";
                    break;

                case 4:
                    str = "x2";
                    break;

                case 5:
                    str = "x3";
                    break;

                case 6:
                    str = "x4";
                    break;
            }
            if (this.extensionType == 1)
            {
                switch (rationsLevel)
                {
                    case 0:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_No_Rations", "No Rations");
                        break;

                    case 1:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Meagre_Rations", "Meagre Rations");
                        break;

                    case 2:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Half_Rations", "Half Rations");
                        break;

                    case 3:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Normal_Rations", "Normal Rations");
                        break;

                    case 4:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Double_Rations", "Double Rations");
                        break;

                    case 5:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Triple_Rations", "Triple Rations");
                        break;

                    case 6:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Quad_Rations", "Quad Rations");
                        break;
                }
            }
            if (rationsLevel == effectiveRationsLevel)
            {
                this.rationsLine1Label.Color = ARGBColors.Black;
                this.rationsBar.clearMarker();
            }
            else
            {
                this.rationsLine1Label.Color = ARGBColors.Red;
                this.rationsBar.setMarker((double) (rationsLevel * 10));
            }
            this.rationsLine1Label.Text = str;
            string str2 = "";
            switch (aleRationsLevel)
            {
                case 0:
                    str2 = "0";
                    break;

                case 1:
                    str2 = "x1";
                    break;

                case 2:
                    str2 = "x2";
                    break;

                case 3:
                    str2 = "x3";
                    break;

                case 4:
                    str2 = "x4";
                    break;
            }
            if (this.extensionType == 2)
            {
                switch (aleRationsLevel)
                {
                    case 0:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_No_Ale_Rations", "No Ale Rations");
                        break;

                    case 1:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Low_Ale_Rations", "Low Ale Rations");
                        break;

                    case 2:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Medium_Ale_Rations", "Medium Ale Rations");
                        break;

                    case 3:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_High_Ale_Rations", "High Ale Rations");
                        break;

                    case 4:
                        this.extensionHeaderLabel.Text = SK.Text("VillageMapPanel_Very_High_Ale_Rations", "Very High Ale Rations");
                        break;
                }
            }
            if (aleRationsLevel == effectiveAleRationsLevel)
            {
                this.aleRationsLine1Label.Color = ARGBColors.Black;
                this.aleRationsBar.clearMarker();
            }
            else
            {
                this.aleRationsLine1Label.Color = ARGBColors.Red;
                this.aleRationsBar.setMarker((double) (aleRationsLevel * 10));
            }
            this.aleRationsLine1Label.Text = str2;
            this.numFoodTypesEatenLabel.Text = numFoodTypesEaten.ToString();
            double num = VillageBuildingsData.getTaxPopularityLevel(taxLevel);
            this.taxPopLabel.Text = num.ToString();
            this.taxBar.Number = taxLevel;
            this.taxBar.MaxValue = CardTypes.getMaxTaxLevel(GameEngine.Instance.World.UserCardData);
            if (num < 0.0)
            {
                this.popIndent1Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_red;
            }
            else if (num > 0.0)
            {
                this.popIndent1Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            }
            else
            {
                this.popIndent1Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_tan;
            }
            double num2 = VillageBuildingsData.getRationsPopularityLevel(effectiveRationsLevel, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData);
            if (effectiveRationsLevel > 0.0)
            {
                num2 += VillageBuildingsData.getNumFoodTypesEatenPopularityLevel(numFoodTypesEaten);
            }
            this.rationsPopLabel.Text = num2.ToString();
            this.rationsBar.Number = (int) (effectiveRationsLevel * 10.0);
            if (num2 < 0.0)
            {
                this.popIndent2Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_red;
            }
            else if (num2 > 0.0)
            {
                this.popIndent2Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            }
            else
            {
                this.popIndent2Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_tan;
            }
            double num3 = VillageBuildingsData.getAleRationsPopularityLevel(effectiveAleRationsLevel, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData);
            this.alePopLabel.Text = num3.ToString();
            this.aleRationsBar.Number = ((int) effectiveAleRationsLevel) * 10;
            if (num3 < 0.0)
            {
                this.popIndent3Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_red;
            }
            else if (num3 > 0.0)
            {
                this.popIndent3Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_green;
            }
            else
            {
                this.popIndent3Image.Image = (Image) GFXLibrary.r_popularity_panel_circle_inset_tan;
            }
        }

        public void stopIndustryEnabled()
        {
            this.inBuildingAllIndustryOnButton.Enabled = true;
            this.inBuildingIndustryAllOnButton.Enabled = true;
            this.inBuildingIndustryThisOnButton.Enabled = true;
        }

        private void stopIndustryResend()
        {
            this.inBuildingAllIndustryOnButton.Enabled = false;
            this.inBuildingIndustryAllOnButton.Enabled = false;
            this.inBuildingIndustryThisOnButton.Enabled = false;
        }

        private void taxHigherClicked()
        {
            if (!this.ViewOnly)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeStats(1, 0, 0);
                }
                if (this.isExtensionOpen())
                {
                    this.extensionType = 0;
                    this.initExtentsion(this.extensionType);
                }
                else
                {
                    this.openExtension(0);
                }
            }
        }

        private void taxLowerClicked()
        {
            if (!this.ViewOnly)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeStats(-1, 0, 0);
                    if (!GameEngine.Instance.World.TutorialIsAdvancing() && (GameEngine.Instance.World.getTutorialStage() == 0x67))
                    {
                        GameEngine.Instance.World.handleQuestObjectiveHappening(14);
                    }
                }
                if (this.isExtensionOpen())
                {
                    this.extensionType = 0;
                    this.initExtentsion(this.extensionType);
                }
                else
                {
                    this.openExtension(0);
                }
            }
        }

        private bool testSubMenu(int buildingType, int landType)
        {
            switch (buildingType)
            {
                case 0x3e8:
                    if (!this.testSubMenu(0x3ec, landType))
                    {
                        return (this.testSubMenu(0x3ed, landType) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x22, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x24, landType, GameEngine.Instance.World.UserResearchData) || VillageBuildingsData.isThisBuildingTypeAvailable(0x25, landType, GameEngine.Instance.World.UserResearchData))));
                    }
                    return true;

                case 0x3e9:
                    if (!this.testSubMenu(0x3ee, landType))
                    {
                        return (this.testSubMenu(0x3f0, landType) || (this.testSubMenu(0x3f2, landType) || (this.testSubMenu(0x457, landType) || VillageBuildingsData.isThisBuildingTypeAvailable(60, landType, GameEngine.Instance.World.UserResearchData))));
                    }
                    return true;

                case 0x3ea:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(0x3d, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailable(0x3e, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x3f, landType, GameEngine.Instance.World.UserResearchData) || VillageBuildingsData.isThisBuildingTypeAvailable(0x40, landType, GameEngine.Instance.World.UserResearchData)));
                    }
                    return true;

                case 0x3eb:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(0x41, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailable(0x42, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x43, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x44, landType, GameEngine.Instance.World.UserResearchData) || VillageBuildingsData.isThisBuildingTypeAvailable(0x45, landType, GameEngine.Instance.World.UserResearchData))));
                    }
                    return true;

                case 0x3ec:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(70, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailable(0x47, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x48, landType, GameEngine.Instance.World.UserResearchData) || VillageBuildingsData.isThisBuildingTypeAvailable(0x49, landType, GameEngine.Instance.World.UserResearchData)));
                    }
                    return true;

                case 0x3ed:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(0x4a, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return VillageBuildingsData.isThisBuildingTypeAvailable(0x4b, landType, GameEngine.Instance.World.UserResearchData);
                    }
                    return true;

                case 0x3ee:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(0x26, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailable(0x29, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x2a, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x2b, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x2c, landType, GameEngine.Instance.World.UserResearchData) || VillageBuildingsData.isThisBuildingTypeAvailable(0x2d, landType, GameEngine.Instance.World.UserResearchData)))));
                    }
                    return true;

                case 0x3ef:
                    return false;

                case 0x3f0:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(0x31, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailable(50, landType, GameEngine.Instance.World.UserResearchData) || VillageBuildingsData.isThisBuildingTypeAvailable(0x33, landType, GameEngine.Instance.World.UserResearchData));
                    }
                    return true;

                case 0x3f1:
                    return false;

                case 0x3f2:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(0x36, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailable(0x37, landType, GameEngine.Instance.World.UserResearchData) || (VillageBuildingsData.isThisBuildingTypeAvailable(0x38, landType, GameEngine.Instance.World.UserResearchData) || VillageBuildingsData.isThisBuildingTypeAvailable(0x39, landType, GameEngine.Instance.World.UserResearchData)));
                    }
                    return true;

                case 0x457:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailable(0x3a, landType, GameEngine.Instance.World.UserResearchData))
                    {
                        return VillageBuildingsData.isThisBuildingTypeAvailable(0x3b, landType, GameEngine.Instance.World.UserResearchData);
                    }
                    return true;

                case 0x458:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x4f, landType))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(80, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x51, landType) || VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x52, landType)));
                    }
                    return true;

                case 0x459:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x54, landType))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x56, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x55, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x57, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x58, landType) || VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x59, landType)))));
                    }
                    return true;

                case 0x45a:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x62, landType))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(100, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x63, landType) || VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x66, landType)));
                    }
                    return true;

                case 0x45b:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailableInCapital(90, landType))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x5b, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x5c, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x5d, landType) || VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x5e, landType))));
                    }
                    return true;

                case 0x45c:
                    if (!VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x5f, landType))
                    {
                        return (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x60, landType) || (VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x61, landType) || VillageBuildingsData.isThisBuildingTypeAvailableInCapital(0x65, landType)));
                    }
                    return true;
            }
            return false;
        }

        private void topGiversMouseLeave()
        {
            this.capitalTop10HeaderGlowImage.Visible = false;
        }

        private void topGiversMouseOver()
        {
            this.capitalTop10HeaderGlowImage.Visible = true;
        }

        private void turnAllIndustryOnClicked()
        {
            if (!this.selectedBuilding.buildingActive)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 5);
                }
                this.showInBuildingInfo(this.selectedBuilding);
            }
            else
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 4);
                }
                this.showInBuildingInfo(this.selectedBuilding);
            }
            this.stopIndustryResend();
        }

        private void turnAllOffClicked()
        {
            if (GameEngine.Instance.Village != null)
            {
                GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 0);
            }
            this.showInBuildingInfo(this.selectedBuilding);
            this.stopIndustryResend();
        }

        private void turnAllOnClicked()
        {
            if (!this.selectedBuilding.buildingActive)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 1);
                }
                this.showInBuildingInfo(this.selectedBuilding);
            }
            else
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 0);
                }
                this.showInBuildingInfo(this.selectedBuilding);
            }
            this.stopIndustryResend();
        }

        private void turnThisOffClicked()
        {
            if (this.selectedBuilding.buildingActive)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 2);
                }
                this.showInBuildingInfo(this.selectedBuilding);
            }
        }

        private void turnThisOnClicked()
        {
            if (!this.selectedBuilding.buildingActive)
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 3);
                }
                this.showInBuildingInfo(this.selectedBuilding);
            }
            else
            {
                if (GameEngine.Instance.Village != null)
                {
                    GameEngine.Instance.Village.changeBuildngActivity(this.selectedBuilding, 2);
                }
                this.showInBuildingInfo(this.selectedBuilding);
            }
            this.stopIndustryResend();
        }

        public int TUTORIAL_getBuildTabYPos()
        {
            return this.calcBuildTabYPos();
        }

        private void updateForViewOnly()
        {
            if (this.ViewOnly)
            {
                this.taxHigherButton.Enabled = false;
                this.taxLowerButton.Enabled = false;
                this.rationsLowerButton.Enabled = false;
                this.rationsHigherButton.Enabled = false;
                this.aleLowerButton.Enabled = false;
                this.aleHigherButton.Enabled = false;
                this.buildTab1Button.Enabled = false;
                this.buildTab2Button.Enabled = false;
                this.buildTab3Button.Enabled = false;
                this.buildTab4Button.Enabled = false;
                this.buildTab5Button.Enabled = false;
            }
            else
            {
                this.taxHigherButton.Enabled = true;
                this.taxLowerButton.Enabled = true;
                this.rationsLowerButton.Enabled = true;
                this.rationsHigherButton.Enabled = true;
                this.aleLowerButton.Enabled = true;
                this.aleHigherButton.Enabled = true;
                this.buildTab1Button.Enabled = true;
                this.buildTab2Button.Enabled = true;
                this.buildTab3Button.Enabled = true;
                this.buildTab4Button.Enabled = true;
                this.buildTab5Button.Enabled = true;
            }
            base.Invalidate();
        }

        private void updateWeaponProductionInfo()
        {
            this.inBuildingMakeWeaponLabel3.Visible = false;
            this.inBuildingMakeWeaponLabel4.Visible = false;
            this.inBuildingMakeWeaponImage1.Visible = false;
            this.inBuildingMakeWeaponImage2.Visible = false;
            this.inBuildingMakeWeaponLabel6.Size = new Size((this.inBuildingPanelImage.Width - 0x18) - 0x12, 20);
            VillageMap village = GameEngine.Instance.Village;
            if ((this.selectedBuilding != null) && (village != null))
            {
                double num = 0.0;
                VillageMap.ArmouryLevels levels = new VillageMap.ArmouryLevels();
                village.getArmouryLevels(levels);
                switch (this.selectedBuilding.buildingType)
                {
                    case 0x1c:
                        num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.selectedBuilding.buildingType, false) * CardTypes.getResourceCapMultiplier(this.selectedBuilding.buildingType, GameEngine.Instance.World.UserCardData);
                        if (num > levels.pikesLevel)
                        {
                            if ((levels.pikesLeftToMake > 0) && (village.m_productionRate_Pikes > 0.0))
                            {
                                this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Next_ready", "Next Ready");
                                DateTime time4 = VillageMap.getCurrentServerTime();
                                DateTime time5 = village.m_productionEnd_Pikes;
                                DateTime time6 = time5;
                                double num4 = 1.0 / village.m_productionRate_Pikes;
                                TimeSpan span2 = new TimeSpan(0, 0, -((int) num4));
                                while (time6 > time4)
                                {
                                    time5 = time6;
                                    time6 = time6.Add(span2);
                                }
                                TimeSpan span7 = (TimeSpan) (time5 - time4);
                                int totalSeconds = (int) span7.TotalSeconds;
                                if (totalSeconds < 0)
                                {
                                    totalSeconds = 0;
                                }
                                this.inBuildingMakeWeaponLabel4.Text = VillageMap.createBuildTimeString(totalSeconds);
                                this.inBuildingMakeWeaponLabel3.Visible = true;
                                this.inBuildingMakeWeaponLabel4.Visible = true;
                            }
                            else if (village.m_productionRate_Pikes > 0.0)
                            {
                                VillageMap.StockpileLevels levels3 = new VillageMap.StockpileLevels();
                                village.getStockpileLevels(levels3);
                                if (levels3.woodLevel < GameEngine.Instance.LocalWorldData.weaponCost_Pike)
                                {
                                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Requires", "Requires");
                                    this.inBuildingMakeWeaponLabel3.Visible = true;
                                    this.inBuildingMakeWeaponImage1.Image = (Image) GFXLibrary.com_16_wood;
                                    this.inBuildingMakeWeaponImage1.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Armoury_Full", "Armoury Full");
                            this.inBuildingMakeWeaponLabel3.Visible = true;
                        }
                        this.inBuildingMakeWeaponLabel5.Visible = true;
                        this.inBuildingMakeWeaponLabel6.Text = GameEngine.Instance.LocalWorldData.weaponCost_Pike.ToString();
                        this.inBuildingMakeWeaponLabel6.Visible = true;
                        this.inBuildingMakeWeaponImage3.Image = (Image) GFXLibrary.com_16_wood;
                        this.inBuildingMakeWeaponImage3.Visible = true;
                        return;

                    case 0x1d:
                        num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.selectedBuilding.buildingType, false) * CardTypes.getResourceCapMultiplier(this.selectedBuilding.buildingType, GameEngine.Instance.World.UserCardData);
                        if (num > levels.bowsLevel)
                        {
                            if ((levels.bowsLeftToMake > 0) && (village.m_productionRate_Bows > 0.0))
                            {
                                this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Next_ready", "Next Ready");
                                DateTime time = VillageMap.getCurrentServerTime();
                                DateTime time2 = village.m_productionEnd_Bows;
                                DateTime time3 = time2;
                                double num2 = 1.0 / village.m_productionRate_Bows;
                                TimeSpan span = new TimeSpan(0, 0, -((int) num2));
                                while (time3 > time)
                                {
                                    time2 = time3;
                                    time3 = time3.Add(span);
                                }
                                TimeSpan span6 = (TimeSpan) (time2 - time);
                                int secsLeft = (int) span6.TotalSeconds;
                                if (secsLeft < 0)
                                {
                                    secsLeft = 0;
                                }
                                this.inBuildingMakeWeaponLabel4.Text = VillageMap.createBuildTimeString(secsLeft);
                                this.inBuildingMakeWeaponLabel3.Visible = true;
                                this.inBuildingMakeWeaponLabel4.Visible = true;
                            }
                            else if (village.m_productionRate_Bows > 0.0)
                            {
                                VillageMap.StockpileLevels levels2 = new VillageMap.StockpileLevels();
                                village.getStockpileLevels(levels2);
                                if (levels2.woodLevel < GameEngine.Instance.LocalWorldData.weaponCost_Bow)
                                {
                                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Requires", "Requires");
                                    this.inBuildingMakeWeaponLabel3.Visible = true;
                                    this.inBuildingMakeWeaponImage1.Image = (Image) GFXLibrary.com_16_wood;
                                    this.inBuildingMakeWeaponImage1.Visible = true;
                                }
                            }
                            break;
                        }
                        this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Armoury_Full", "Armoury Full");
                        this.inBuildingMakeWeaponLabel3.Visible = true;
                        break;

                    case 30:
                        num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.selectedBuilding.buildingType, false) * CardTypes.getResourceCapMultiplier(this.selectedBuilding.buildingType, GameEngine.Instance.World.UserCardData);
                        if (num > levels.swordsLevel)
                        {
                            if ((levels.swordsLeftToMake > 0) && (village.m_productionRate_Swords > 0.0))
                            {
                                this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Next_ready", "Next Ready");
                                DateTime time7 = VillageMap.getCurrentServerTime();
                                DateTime time8 = village.m_productionEnd_Swords;
                                DateTime time9 = time8;
                                double num6 = 1.0 / village.m_productionRate_Swords;
                                TimeSpan span3 = new TimeSpan(0, 0, -((int) num6));
                                while (time9 > time7)
                                {
                                    time8 = time9;
                                    time9 = time9.Add(span3);
                                }
                                TimeSpan span8 = (TimeSpan) (time8 - time7);
                                int num7 = (int) span8.TotalSeconds;
                                if (num7 < 0)
                                {
                                    num7 = 0;
                                }
                                this.inBuildingMakeWeaponLabel4.Text = VillageMap.createBuildTimeString(num7);
                                this.inBuildingMakeWeaponLabel3.Visible = true;
                                this.inBuildingMakeWeaponLabel4.Visible = true;
                            }
                            else if (village.m_productionRate_Swords > 0.0)
                            {
                                VillageMap.StockpileLevels levels4 = new VillageMap.StockpileLevels();
                                village.getStockpileLevels(levels4);
                                if (levels4.ironLevel < GameEngine.Instance.LocalWorldData.weaponCost_Sword)
                                {
                                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Requires", "Requires");
                                    this.inBuildingMakeWeaponLabel3.Visible = true;
                                    this.inBuildingMakeWeaponImage1.Image = (Image) GFXLibrary.com_16_iron;
                                    this.inBuildingMakeWeaponImage1.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Armoury_Full", "Armoury Full");
                            this.inBuildingMakeWeaponLabel3.Visible = true;
                        }
                        this.inBuildingMakeWeaponLabel5.Visible = true;
                        this.inBuildingMakeWeaponLabel6.Text = GameEngine.Instance.LocalWorldData.weaponCost_Sword.ToString();
                        this.inBuildingMakeWeaponLabel6.Visible = true;
                        this.inBuildingMakeWeaponImage3.Image = (Image) GFXLibrary.com_16_iron;
                        this.inBuildingMakeWeaponImage3.Visible = true;
                        return;

                    case 0x1f:
                        num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.selectedBuilding.buildingType, false) * CardTypes.getResourceCapMultiplier(this.selectedBuilding.buildingType, GameEngine.Instance.World.UserCardData);
                        if (num > levels.armourLevel)
                        {
                            if ((levels.armourLeftToMake > 0) && (village.m_productionRate_Armour > 0.0))
                            {
                                this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Next_ready", "Next Ready");
                                DateTime time10 = VillageMap.getCurrentServerTime();
                                DateTime time11 = village.m_productionEnd_Armour;
                                DateTime time12 = time11;
                                double num8 = 1.0 / village.m_productionRate_Armour;
                                TimeSpan span4 = new TimeSpan(0, 0, -((int) num8));
                                while (time12 > time10)
                                {
                                    time11 = time12;
                                    time12 = time12.Add(span4);
                                }
                                TimeSpan span9 = (TimeSpan) (time11 - time10);
                                int num9 = (int) span9.TotalSeconds;
                                if (num9 < 0)
                                {
                                    num9 = 0;
                                }
                                this.inBuildingMakeWeaponLabel4.Text = VillageMap.createBuildTimeString(num9);
                                this.inBuildingMakeWeaponLabel3.Visible = true;
                                this.inBuildingMakeWeaponLabel4.Visible = true;
                            }
                            else if (village.m_productionRate_Armour > 0.0)
                            {
                                VillageMap.StockpileLevels levels5 = new VillageMap.StockpileLevels();
                                village.getStockpileLevels(levels5);
                                if (levels5.ironLevel < GameEngine.Instance.LocalWorldData.weaponCost_Armour)
                                {
                                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Requires", "Requires");
                                    this.inBuildingMakeWeaponLabel3.Visible = true;
                                    this.inBuildingMakeWeaponImage1.Image = (Image) GFXLibrary.com_16_iron;
                                    this.inBuildingMakeWeaponImage1.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Armoury_Full", "Armoury Full");
                            this.inBuildingMakeWeaponLabel3.Visible = true;
                        }
                        this.inBuildingMakeWeaponLabel5.Visible = true;
                        this.inBuildingMakeWeaponLabel6.Text = GameEngine.Instance.LocalWorldData.weaponCost_Armour.ToString();
                        this.inBuildingMakeWeaponLabel6.Visible = true;
                        this.inBuildingMakeWeaponImage3.Image = (Image) GFXLibrary.com_16_iron;
                        this.inBuildingMakeWeaponImage3.Visible = true;
                        return;

                    case 0x20:
                        num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.selectedBuilding.buildingType, false) * CardTypes.getResourceCapMultiplier(this.selectedBuilding.buildingType, GameEngine.Instance.World.UserCardData);
                        if (num > levels.catapultsLevel)
                        {
                            if ((levels.catapultsLeftToMake > 0) && (village.m_productionRate_Catapults > 0.0))
                            {
                                this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Next_ready", "Next Ready");
                                DateTime time13 = VillageMap.getCurrentServerTime();
                                DateTime time14 = village.m_productionEnd_Catapults;
                                DateTime time15 = time14;
                                double num10 = 1.0 / village.m_productionRate_Catapults;
                                TimeSpan span5 = new TimeSpan(0, 0, -((int) num10));
                                while (time15 > time13)
                                {
                                    time14 = time15;
                                    time15 = time15.Add(span5);
                                }
                                TimeSpan span10 = (TimeSpan) (time14 - time13);
                                int num11 = (int) span10.TotalSeconds;
                                if (num11 < 0)
                                {
                                    num11 = 0;
                                }
                                this.inBuildingMakeWeaponLabel4.Text = VillageMap.createBuildTimeString(num11);
                                this.inBuildingMakeWeaponLabel3.Visible = true;
                                this.inBuildingMakeWeaponLabel4.Visible = true;
                            }
                            else if (village.m_productionRate_Catapults > 0.0)
                            {
                                VillageMap.StockpileLevels levels6 = new VillageMap.StockpileLevels();
                                village.getStockpileLevels(levels6);
                                bool flag = false;
                                if (levels6.woodLevel < GameEngine.Instance.LocalWorldData.weaponCost_Catapult)
                                {
                                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Requires", "Requires");
                                    this.inBuildingMakeWeaponLabel3.Visible = true;
                                    this.inBuildingMakeWeaponImage1.Image = (Image) GFXLibrary.com_16_wood;
                                    this.inBuildingMakeWeaponImage1.Visible = true;
                                    flag = true;
                                }
                                if (levels6.stoneLevel < GameEngine.Instance.LocalWorldData.weaponCost_Catapult2)
                                {
                                    this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Requires", "Requires");
                                    this.inBuildingMakeWeaponLabel3.Visible = true;
                                    if (!flag)
                                    {
                                        this.inBuildingMakeWeaponImage1.Image = (Image) GFXLibrary.com_16_stone;
                                        this.inBuildingMakeWeaponImage1.Visible = true;
                                    }
                                    else
                                    {
                                        this.inBuildingMakeWeaponImage2.Image = (Image) GFXLibrary.com_16_stone;
                                        this.inBuildingMakeWeaponImage2.Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.inBuildingMakeWeaponLabel3.Text = SK.Text("VillageMapPanel_Armoury_Full", "Armoury Full");
                            this.inBuildingMakeWeaponLabel3.Visible = true;
                        }
                        this.inBuildingMakeWeaponLabel6.Size = new Size(((this.inBuildingPanelImage.Width - 0x18) - 0x12) - 0x10, 20);
                        this.inBuildingMakeWeaponLabel5.Visible = true;
                        this.inBuildingMakeWeaponLabel6.Text = GameEngine.Instance.LocalWorldData.weaponCost_Catapult.ToString();
                        this.inBuildingMakeWeaponLabel6.Visible = true;
                        this.inBuildingMakeWeaponImage3.Image = (Image) GFXLibrary.com_16_stone;
                        this.inBuildingMakeWeaponImage3.Visible = true;
                        this.inBuildingMakeWeaponImage4.Image = (Image) GFXLibrary.com_16_wood;
                        this.inBuildingMakeWeaponImage4.Visible = true;
                        return;

                    default:
                        return;
                }
                this.inBuildingMakeWeaponLabel5.Visible = true;
                this.inBuildingMakeWeaponLabel6.Text = GameEngine.Instance.LocalWorldData.weaponCost_Bow.ToString();
                this.inBuildingMakeWeaponLabel6.Visible = true;
                this.inBuildingMakeWeaponImage3.Image = (Image) GFXLibrary.com_16_wood;
                this.inBuildingMakeWeaponImage3.Visible = true;
            }
        }

        public void villageReshowAfterStockpilePlaced()
        {
            if (this.currentTab >= 0)
            {
                this.setBuildingTab(this.currentTab);
            }
            else
            {
                this.setBuildingTab(1);
            }
        }

        public int CurrentTab
        {
            get
            {
                return this.CurrentTab;
            }
        }

        public bool ViewOnly
        {
            get
            {
                return this.viewOnly;
            }
            set
            {
                this.viewOnly = value;
                this.updateForViewOnly();
            }
        }

        public class ParishTaxComparerNegative : IComparer<ParishTaxCalc>
        {
            public int Compare(ParishTaxCalc y, ParishTaxCalc x)
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
                if (x.tax < y.tax)
                {
                    return 1;
                }
                if (x.tax > y.tax)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class ParishTaxComparerPositive : IComparer<ParishTaxCalc>
        {
            public int Compare(ParishTaxCalc x, ParishTaxCalc y)
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
                if (x.tax < y.tax)
                {
                    return 1;
                }
                if (x.tax > y.tax)
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}

