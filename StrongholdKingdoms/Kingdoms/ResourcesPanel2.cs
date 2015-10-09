namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class ResourcesPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDArea aleClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage aleImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel aleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea applesClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage applesImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel applesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea armourClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage armourImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel armourLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel armouryHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel armouryLimitLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea bowsClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage bowsImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel bowsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea breadClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage breadImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel breadLabel = new CustomSelfDrawPanel.CSDLabel();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDArea catapultsClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage catapultsImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel catapultsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea cheeseClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage cheeseImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel cheeseLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea clothesClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage clothesImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel clothesLabel = new CustomSelfDrawPanel.CSDLabel();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel dailyProductionHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel dailyProductionValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDArea fishClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage fishImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel fishLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea furnitureClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage furnitureImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel furnitureLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel granaryHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel granaryLimitLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel hallHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel hallLimitLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel innHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel innLimitLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea ironClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage ironImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel ironLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea meatClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage meatImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel meatLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea metalwareClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage metalwareImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel metalwareLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea pikesClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage pikesImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel pikesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea pitchClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage pitchImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel pitchLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourcesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea saltClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage saltImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel saltLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel selectedHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage selectedImage = new CustomSelfDrawPanel.CSDImage();
        private int selectedResource = -1;
        private CustomSelfDrawPanel.CSDArea silkClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage silkImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel silkLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea spicesClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage spicesImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel spicesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel stockpileHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel stockpileLimitLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea stoneClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage stoneImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel stoneLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea swordsClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage swordsImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel swordsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel totalBuildingsHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel totalBuildingsValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea vegClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage vegImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel vegLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea venisonClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage venisonImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel venisonLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wineClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage wineImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel wineLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea woodClickArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage woodImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel woodLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel workingBuildingsHeadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel workingBuildingsValueLabel = new CustomSelfDrawPanel.CSDLabel();

        public ResourcesPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
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

        private int getCap(int resourceType)
        {
            double num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, resourceType, false) * CardTypes.getResourceCapMultiplier(resourceType, GameEngine.Instance.World.UserCardData);
            return (int) num;
        }

        public void init()
        {
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.goods_background;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("ResourcesPanel_Resources", "Resources"));
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.CustomTooltipID = 900;
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ResourcesPanel2_close");
            this.mainBackgroundArea.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 3, new Point(0x382, 10));
            Color color = Color.FromArgb(0xff, 230, 0xa7);
            Color color2 = Color.FromArgb(0x55, 0x4c, 0x37);
            this.stockpileHeaderLabel.Text = SK.Text("BuildingTypes_Stockpile", "Stockpile");
            this.stockpileHeaderLabel.Color = color;
            this.stockpileHeaderLabel.DropShadowColor = color2;
            this.stockpileHeaderLabel.Position = new Point(13, 0x3f);
            this.stockpileHeaderLabel.Size = new Size(0x145, 50);
            this.stockpileHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.stockpileHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.stockpileHeaderLabel);
            this.stockpileLimitLabel.Text = "(0)";
            this.stockpileLimitLabel.Color = Color.FromArgb(0xff, 230, 0xa7);
            this.stockpileLimitLabel.DropShadowColor = Color.FromArgb(0x55, 0x4c, 0x37);
            this.stockpileLimitLabel.Position = new Point(13, 0x53);
            this.stockpileLimitLabel.Size = new Size(0x145, 50);
            this.stockpileLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.stockpileLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.stockpileLimitLabel);
            this.woodLabel.Text = "0";
            this.woodLabel.Color = ARGBColors.Black;
            this.woodLabel.Position = new Point(13, 0x97);
            this.woodLabel.Size = new Size(0x51, 50);
            this.woodLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.woodLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.woodLabel);
            this.stoneLabel.Text = "0";
            this.stoneLabel.Color = ARGBColors.Black;
            this.stoneLabel.Position = new Point(0x5d, 0x97);
            this.stoneLabel.Size = new Size(0x51, 50);
            this.stoneLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.stoneLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.stoneLabel);
            this.ironLabel.Text = "0";
            this.ironLabel.Color = ARGBColors.Black;
            this.ironLabel.Position = new Point(0xad, 0x97);
            this.ironLabel.Size = new Size(0x51, 50);
            this.ironLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.ironLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.ironLabel);
            this.pitchLabel.Text = "0";
            this.pitchLabel.Color = ARGBColors.Black;
            this.pitchLabel.Position = new Point(0xfd, 0x97);
            this.pitchLabel.Size = new Size(0x51, 50);
            this.pitchLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.pitchLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.pitchLabel);
            this.woodImage.Image = (Image) GFXLibrary.com_32_wood_DS;
            this.woodImage.Position = new Point(0x35 - (this.woodImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.woodImage);
            this.stoneImage.Image = (Image) GFXLibrary.com_32_stone_DS;
            this.stoneImage.Position = new Point(0x85 - (this.stoneImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.stoneImage);
            this.ironImage.Image = (Image) GFXLibrary.com_32_iron_DS;
            this.ironImage.Position = new Point(0xd5 - (this.ironImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.ironImage);
            this.pitchImage.Image = (Image) GFXLibrary.com_32_pitch_DS;
            this.pitchImage.Position = new Point(0x125 - (this.pitchImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.pitchImage);
            this.woodClickArea.Position = new Point(this.woodLabel.X, this.woodLabel.Y - 50);
            this.woodClickArea.Size = new Size(80, 70);
            this.woodClickArea.Data = 6;
            this.woodClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.woodClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.woodClickArea);
            this.stoneClickArea.Position = new Point(this.stoneLabel.X, this.stoneLabel.Y - 50);
            this.stoneClickArea.Size = new Size(80, 70);
            this.stoneClickArea.Data = 7;
            this.stoneClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.stoneClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.stoneClickArea);
            this.ironClickArea.Position = new Point(this.ironLabel.X, this.ironLabel.Y - 50);
            this.ironClickArea.Size = new Size(80, 70);
            this.ironClickArea.Data = 8;
            this.ironClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.ironClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.ironClickArea);
            this.pitchClickArea.Position = new Point(this.pitchLabel.X, this.pitchLabel.Y - 50);
            this.pitchClickArea.Size = new Size(80, 70);
            this.pitchClickArea.Data = 9;
            this.pitchClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.pitchClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.pitchClickArea);
            this.hallHeaderLabel.Text = SK.Text("BuildingTypes_Village_Hall", "Village Hall");
            this.hallHeaderLabel.Color = color;
            this.hallHeaderLabel.DropShadowColor = color2;
            this.hallHeaderLabel.Position = new Point(0x15c, 0x3f);
            this.hallHeaderLabel.Size = new Size(0x279, 50);
            this.hallHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.hallHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.hallHeaderLabel);
            this.hallLimitLabel.Text = "(0)";
            this.hallLimitLabel.Color = Color.FromArgb(0xff, 230, 0xa7);
            this.hallLimitLabel.DropShadowColor = Color.FromArgb(0x55, 0x4c, 0x37);
            this.hallLimitLabel.Position = new Point(0x15c, 0x53);
            this.hallLimitLabel.Size = new Size(0x279, 50);
            this.hallLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.hallLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.hallLimitLabel);
            this.venisonLabel.Text = "0";
            this.venisonLabel.Color = ARGBColors.Black;
            this.venisonLabel.Position = new Point(0x15c, 0x97);
            this.venisonLabel.Size = new Size(0x51, 50);
            this.venisonLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.venisonLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.venisonLabel);
            this.furnitureLabel.Text = "0";
            this.furnitureLabel.Color = ARGBColors.Black;
            this.furnitureLabel.Position = new Point(0x1ab, 0x97);
            this.furnitureLabel.Size = new Size(0x51, 50);
            this.furnitureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.furnitureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.furnitureLabel);
            this.metalwareLabel.Text = "0";
            this.metalwareLabel.Color = ARGBColors.Black;
            this.metalwareLabel.Position = new Point(0x1fa, 0x97);
            this.metalwareLabel.Size = new Size(0x51, 50);
            this.metalwareLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.metalwareLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.metalwareLabel);
            this.clothesLabel.Text = "0";
            this.clothesLabel.Color = ARGBColors.Black;
            this.clothesLabel.Position = new Point(0x249, 0x97);
            this.clothesLabel.Size = new Size(0x51, 50);
            this.clothesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.clothesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.clothesLabel);
            this.wineLabel.Text = "0";
            this.wineLabel.Color = ARGBColors.Black;
            this.wineLabel.Position = new Point(0x298, 0x97);
            this.wineLabel.Size = new Size(0x51, 50);
            this.wineLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.wineLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.wineLabel);
            this.saltLabel.Text = "0";
            this.saltLabel.Color = ARGBColors.Black;
            this.saltLabel.Position = new Point(0x2e7, 0x97);
            this.saltLabel.Size = new Size(0x51, 50);
            this.saltLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.saltLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.saltLabel);
            this.spicesLabel.Text = "0";
            this.spicesLabel.Color = ARGBColors.Black;
            this.spicesLabel.Position = new Point(0x336, 0x97);
            this.spicesLabel.Size = new Size(0x51, 50);
            this.spicesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.spicesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.spicesLabel);
            this.silkLabel.Text = "0";
            this.silkLabel.Color = ARGBColors.Black;
            this.silkLabel.Position = new Point(0x385, 0x97);
            this.silkLabel.Size = new Size(0x51, 50);
            this.silkLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.silkLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.silkLabel);
            this.venisonImage.Image = (Image) GFXLibrary.com_32_venison_DS;
            this.venisonImage.Position = new Point(0x183 - (this.venisonImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.venisonImage);
            this.furnitureImage.Image = (Image) GFXLibrary.com_32_furniture_DS;
            this.furnitureImage.Position = new Point(0x1d2 - (this.furnitureImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.furnitureImage);
            this.metalwareImage.Image = (Image) GFXLibrary.com_32_metalware_DS;
            this.metalwareImage.Position = new Point(0x221 - (this.metalwareImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.metalwareImage);
            this.clothesImage.Image = (Image) GFXLibrary.com_32_clothes_DS;
            this.clothesImage.Position = new Point(0x270 - (this.clothesImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.clothesImage);
            this.wineImage.Image = (Image) GFXLibrary.com_32_wine_DS;
            this.wineImage.Position = new Point(0x2bf - (this.wineImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.wineImage);
            this.saltImage.Image = (Image) GFXLibrary.com_32_salt_DS;
            this.saltImage.Position = new Point(0x30e - (this.saltImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.saltImage);
            this.spicesImage.Image = (Image) GFXLibrary.com_32_spices_DS;
            this.spicesImage.Position = new Point(0x35d - (this.spicesImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.spicesImage);
            this.silkImage.Image = (Image) GFXLibrary.com_32_silk_DS;
            this.silkImage.Position = new Point(940 - (this.silkImage.Size.Width / 2), 0x66);
            this.mainBackgroundArea.addControl(this.silkImage);
            this.venisonClickArea.Position = new Point(this.venisonLabel.X, this.venisonLabel.Y - 50);
            this.venisonClickArea.Size = new Size(80, 70);
            this.venisonClickArea.Data = 0x16;
            this.venisonClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.venisonClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.venisonClickArea);
            this.furnitureClickArea.Position = new Point(this.furnitureLabel.X, this.furnitureLabel.Y - 50);
            this.furnitureClickArea.Size = new Size(80, 70);
            this.furnitureClickArea.Data = 0x15;
            this.furnitureClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.furnitureClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.furnitureClickArea);
            this.metalwareClickArea.Position = new Point(this.metalwareLabel.X, this.metalwareLabel.Y - 50);
            this.metalwareClickArea.Size = new Size(80, 70);
            this.metalwareClickArea.Data = 0x1a;
            this.metalwareClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.metalwareClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.metalwareClickArea);
            this.clothesClickArea.Position = new Point(this.clothesLabel.X, this.clothesLabel.Y - 50);
            this.clothesClickArea.Size = new Size(80, 70);
            this.clothesClickArea.Data = 0x13;
            this.clothesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.clothesClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.clothesClickArea);
            this.wineClickArea.Position = new Point(this.wineLabel.X, this.wineLabel.Y - 50);
            this.wineClickArea.Size = new Size(80, 70);
            this.wineClickArea.Data = 0x21;
            this.wineClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.wineClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.wineClickArea);
            this.saltClickArea.Position = new Point(this.saltLabel.X, this.saltLabel.Y - 50);
            this.saltClickArea.Size = new Size(80, 70);
            this.saltClickArea.Data = 0x17;
            this.saltClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.saltClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.saltClickArea);
            this.spicesClickArea.Position = new Point(this.spicesLabel.X, this.spicesLabel.Y - 50);
            this.spicesClickArea.Size = new Size(80, 70);
            this.spicesClickArea.Data = 0x18;
            this.spicesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.spicesClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.spicesClickArea);
            this.silkClickArea.Position = new Point(this.silkLabel.X, this.silkLabel.Y - 50);
            this.silkClickArea.Size = new Size(80, 70);
            this.silkClickArea.Data = 0x19;
            this.silkClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.silkClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.silkClickArea);
            this.granaryHeaderLabel.Text = SK.Text("BuildingTypes_Granary", "Granary");
            this.granaryHeaderLabel.Color = color;
            this.granaryHeaderLabel.DropShadowColor = color2;
            this.granaryHeaderLabel.Position = new Point(13, 0xe1);
            this.granaryHeaderLabel.Size = new Size(0x1de, 50);
            this.granaryHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.granaryHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.granaryHeaderLabel);
            this.granaryLimitLabel.Text = "(0)";
            this.granaryLimitLabel.Color = Color.FromArgb(0xff, 230, 0xa7);
            this.granaryLimitLabel.DropShadowColor = Color.FromArgb(0x55, 0x4c, 0x37);
            this.granaryLimitLabel.Position = new Point(13, 0xf5);
            this.granaryLimitLabel.Size = new Size(0x1de, 50);
            this.granaryLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.granaryLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.granaryLimitLabel);
            this.applesLabel.Text = "0";
            this.applesLabel.Color = ARGBColors.Black;
            this.applesLabel.Position = new Point(13, 0x139);
            this.applesLabel.Size = new Size(0x51, 50);
            this.applesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.applesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.applesLabel);
            this.cheeseLabel.Text = "0";
            this.cheeseLabel.Color = ARGBColors.Black;
            this.cheeseLabel.Position = new Point(0x5d, 0x139);
            this.cheeseLabel.Size = new Size(0x51, 50);
            this.cheeseLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.cheeseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.cheeseLabel);
            this.meatLabel.Text = "0";
            this.meatLabel.Color = ARGBColors.Black;
            this.meatLabel.Position = new Point(0xad, 0x139);
            this.meatLabel.Size = new Size(0x51, 50);
            this.meatLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.meatLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.meatLabel);
            this.vegLabel.Text = "0";
            this.vegLabel.Color = ARGBColors.Black;
            this.vegLabel.Position = new Point(0x14d, 0x139);
            this.vegLabel.Size = new Size(0x51, 50);
            this.vegLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.vegLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.vegLabel);
            this.breadLabel.Text = "0";
            this.breadLabel.Color = ARGBColors.Black;
            this.breadLabel.Position = new Point(0xfd, 0x139);
            this.breadLabel.Size = new Size(0x51, 50);
            this.breadLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.breadLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.breadLabel);
            this.fishLabel.Text = "0";
            this.fishLabel.Color = ARGBColors.Black;
            this.fishLabel.Position = new Point(0x19d, 0x139);
            this.fishLabel.Size = new Size(0x51, 50);
            this.fishLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.fishLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.fishLabel);
            this.applesImage.Image = (Image) GFXLibrary.com_32_apples_DS;
            this.applesImage.Position = new Point(0x35 - (this.applesImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.applesImage);
            this.cheeseImage.Image = (Image) GFXLibrary.com_32_cheese_DS;
            this.cheeseImage.Position = new Point(0x85 - (this.cheeseImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.cheeseImage);
            this.meatImage.Image = (Image) GFXLibrary.com_32_meat_DS;
            this.meatImage.Position = new Point(0xd5 - (this.meatImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.meatImage);
            this.vegImage.Image = (Image) GFXLibrary.com_32_veg_DS;
            this.vegImage.Position = new Point(0x175 - (this.vegImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.vegImage);
            this.breadImage.Image = (Image) GFXLibrary.com_32_bread_DS;
            this.breadImage.Position = new Point(0x125 - (this.breadImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.breadImage);
            this.fishImage.Image = (Image) GFXLibrary.com_32_fish_DS;
            this.fishImage.Position = new Point(0x1c5 - (this.fishImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.fishImage);
            this.applesClickArea.Position = new Point(this.applesLabel.X, this.applesLabel.Y - 50);
            this.applesClickArea.Size = new Size(80, 70);
            this.applesClickArea.Data = 13;
            this.applesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.applesClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.applesClickArea);
            this.cheeseClickArea.Position = new Point(this.cheeseLabel.X, this.cheeseLabel.Y - 50);
            this.cheeseClickArea.Size = new Size(80, 70);
            this.cheeseClickArea.Data = 0x11;
            this.cheeseClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.cheeseClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.cheeseClickArea);
            this.meatClickArea.Position = new Point(this.meatLabel.X, this.meatLabel.Y - 50);
            this.meatClickArea.Size = new Size(80, 70);
            this.meatClickArea.Data = 0x10;
            this.meatClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.meatClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.meatClickArea);
            this.vegClickArea.Position = new Point(this.vegLabel.X, this.vegLabel.Y - 50);
            this.vegClickArea.Size = new Size(80, 70);
            this.vegClickArea.Data = 15;
            this.vegClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.vegClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.vegClickArea);
            this.breadClickArea.Position = new Point(this.breadLabel.X, this.breadLabel.Y - 50);
            this.breadClickArea.Size = new Size(80, 70);
            this.breadClickArea.Data = 14;
            this.breadClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.breadClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.breadClickArea);
            this.fishClickArea.Position = new Point(this.fishLabel.X, this.fishLabel.Y - 50);
            this.fishClickArea.Size = new Size(80, 70);
            this.fishClickArea.Data = 0x12;
            this.fishClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.fishClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.fishClickArea);
            this.innHeaderLabel.Text = SK.Text("BuildingTypes_Inn", "Inn");
            this.innHeaderLabel.Color = color;
            this.innHeaderLabel.DropShadowColor = color2;
            this.innHeaderLabel.Position = new Point(0x1db, 0xe1);
            this.innHeaderLabel.Size = new Size(0x7a, 50);
            this.innHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.innHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.innHeaderLabel);
            this.innLimitLabel.Text = "(0)";
            this.innLimitLabel.Color = Color.FromArgb(0xff, 230, 0xa7);
            this.innLimitLabel.DropShadowColor = Color.FromArgb(0x55, 0x4c, 0x37);
            this.innLimitLabel.Position = new Point(500, 0xf5);
            this.innLimitLabel.Size = new Size(0x48, 50);
            this.innLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.innLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.innLimitLabel);
            this.aleLabel.Text = "0";
            this.aleLabel.Color = ARGBColors.Black;
            this.aleLabel.Position = new Point(0x1db, 0x139);
            this.aleLabel.Size = new Size(0x7a, 50);
            this.aleLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.aleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.aleLabel);
            this.aleImage.Image = (Image) GFXLibrary.com_32_ale_DS;
            this.aleImage.Position = new Point(0x218 - (this.aleImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.aleImage);
            this.aleClickArea.Position = new Point(this.aleLabel.X, this.aleLabel.Y - 50);
            this.aleClickArea.Size = new Size(0x48, 70);
            this.aleClickArea.Data = 12;
            this.aleClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.aleClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.aleClickArea);
            this.armouryHeaderLabel.Text = SK.Text("BuildingTypes_Armoury", "Armoury");
            this.armouryHeaderLabel.Color = color;
            this.armouryHeaderLabel.DropShadowColor = color2;
            this.armouryHeaderLabel.Position = new Point(0x247, 0xe1);
            this.armouryHeaderLabel.Size = new Size(0x18e, 50);
            this.armouryHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.armouryHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.armouryHeaderLabel);
            this.armouryLimitLabel.Text = "(0)";
            this.armouryLimitLabel.Color = Color.FromArgb(0xff, 230, 0xa7);
            this.armouryLimitLabel.DropShadowColor = Color.FromArgb(0x55, 0x4c, 0x37);
            this.armouryLimitLabel.Position = new Point(0x247, 0xf5);
            this.armouryLimitLabel.Size = new Size(0x18e, 50);
            this.armouryLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.armouryLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.armouryLimitLabel);
            this.bowsLabel.Text = "0";
            this.bowsLabel.Color = ARGBColors.Black;
            this.bowsLabel.Position = new Point(0x247, 0x139);
            this.bowsLabel.Size = new Size(0x51, 50);
            this.bowsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.bowsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.bowsLabel);
            this.pikesLabel.Text = "0";
            this.pikesLabel.Color = ARGBColors.Black;
            this.pikesLabel.Position = new Point(0x297, 0x139);
            this.pikesLabel.Size = new Size(0x51, 50);
            this.pikesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.pikesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.pikesLabel);
            this.armourLabel.Text = "0";
            this.armourLabel.Color = ARGBColors.Black;
            this.armourLabel.Position = new Point(0x2e7, 0x139);
            this.armourLabel.Size = new Size(0x51, 50);
            this.armourLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.armourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.armourLabel);
            this.swordsLabel.Text = "0";
            this.swordsLabel.Color = ARGBColors.Black;
            this.swordsLabel.Position = new Point(0x337, 0x139);
            this.swordsLabel.Size = new Size(0x51, 50);
            this.swordsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.swordsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.swordsLabel);
            this.catapultsLabel.Text = "0";
            this.catapultsLabel.Color = ARGBColors.Black;
            this.catapultsLabel.Position = new Point(0x387, 0x139);
            this.catapultsLabel.Size = new Size(0x51, 50);
            this.catapultsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.catapultsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundArea.addControl(this.catapultsLabel);
            this.bowsImage.Image = (Image) GFXLibrary.com_32_bows_DS;
            this.bowsImage.Position = new Point(0x26f - (this.bowsImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.bowsImage);
            this.pikesImage.Image = (Image) GFXLibrary.com_32_pikes_DS;
            this.pikesImage.Position = new Point(0x2bf - (this.pikesImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.pikesImage);
            this.armourImage.Image = (Image) GFXLibrary.com_32_armour_DS;
            this.armourImage.Position = new Point(0x30f - (this.armourImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.armourImage);
            this.swordsImage.Image = (Image) GFXLibrary.com_32_swords_DS;
            this.swordsImage.Position = new Point(0x35f - (this.swordsImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.swordsImage);
            this.catapultsImage.Image = (Image) GFXLibrary.com_32_catapults_DS;
            this.catapultsImage.Position = new Point(0x3af - (this.catapultsImage.Size.Width / 2), 0x108);
            this.mainBackgroundArea.addControl(this.catapultsImage);
            this.bowsClickArea.Position = new Point(this.bowsLabel.X, this.bowsLabel.Y - 50);
            this.bowsClickArea.Size = new Size(80, 70);
            this.bowsClickArea.Data = 0x1d;
            this.bowsClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.bowsClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.bowsClickArea);
            this.pikesClickArea.Position = new Point(this.pikesLabel.X, this.pikesLabel.Y - 50);
            this.pikesClickArea.Size = new Size(80, 70);
            this.pikesClickArea.Data = 0x1c;
            this.pikesClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.pikesClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.pikesClickArea);
            this.armourClickArea.Position = new Point(this.armourLabel.X, this.armourLabel.Y - 50);
            this.armourClickArea.Size = new Size(80, 70);
            this.armourClickArea.Data = 0x1f;
            this.armourClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.armourClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.armourClickArea);
            this.swordsClickArea.Position = new Point(this.swordsLabel.X, this.swordsLabel.Y - 50);
            this.swordsClickArea.Size = new Size(80, 70);
            this.swordsClickArea.Data = 30;
            this.swordsClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.swordsClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.swordsClickArea);
            this.catapultsClickArea.Position = new Point(this.catapultsLabel.X, this.catapultsLabel.Y - 50);
            this.catapultsClickArea.Size = new Size(80, 70);
            this.catapultsClickArea.Data = 0x20;
            this.catapultsClickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.resourceClicked));
            this.catapultsClickArea.CustomTooltipID = 0x385;
            this.mainBackgroundArea.addControl(this.catapultsClickArea);
            this.selectedHeadingLabel.Text = SK.Text("ResourcesPanel_Resources", "Resources");
            this.selectedHeadingLabel.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
            this.selectedHeadingLabel.DropShadowColor = Color.FromArgb(0x4a, 0x43, 0x30);
            this.selectedHeadingLabel.Position = new Point(0x76, 0x16c);
            this.selectedHeadingLabel.Size = new Size(0x3e0, 50);
            this.selectedHeadingLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.selectedHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.selectedHeadingLabel.Visible = false;
            this.mainBackgroundArea.addControl(this.selectedHeadingLabel);
            this.selectedResource = -1;
            this.selectedImage.Image = (Image) GFXLibrary.com_32_fish_DS;
            this.selectedImage.Position = new Point(0x19, 0x162);
            this.selectedImage.Visible = false;
            this.mainBackgroundArea.addControl(this.selectedImage);
            this.dailyProductionHeadingLabel.Text = SK.Text("ResourcesPanel_Daily_Production", "Daily Production") + " :";
            this.dailyProductionHeadingLabel.Color = ARGBColors.Black;
            this.dailyProductionHeadingLabel.Position = new Point(0x3f, 0x1b9);
            this.dailyProductionHeadingLabel.Size = new Size(400, 50);
            this.dailyProductionHeadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.dailyProductionHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.dailyProductionHeadingLabel.Visible = false;
            this.mainBackgroundArea.addControl(this.dailyProductionHeadingLabel);
            this.totalBuildingsHeadingLabel.Text = SK.Text("ResourcesPanel_Number_Of_Buildings", "Number of Buildings") + " :";
            this.totalBuildingsHeadingLabel.Color = ARGBColors.Black;
            this.totalBuildingsHeadingLabel.Position = new Point(0x3f, 0x1d2);
            this.totalBuildingsHeadingLabel.Size = new Size(400, 50);
            this.totalBuildingsHeadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.totalBuildingsHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.totalBuildingsHeadingLabel.Visible = false;
            this.mainBackgroundArea.addControl(this.totalBuildingsHeadingLabel);
            this.workingBuildingsHeadingLabel.Text = SK.Text("ResourcesPanel_Number_Of_Working_Buildings", "Number of Working Buildings") + " :";
            this.workingBuildingsHeadingLabel.Color = ARGBColors.Black;
            this.workingBuildingsHeadingLabel.Position = new Point(0x3f, 0x1eb);
            this.workingBuildingsHeadingLabel.Size = new Size(400, 50);
            this.workingBuildingsHeadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.workingBuildingsHeadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.workingBuildingsHeadingLabel.Visible = false;
            this.mainBackgroundArea.addControl(this.workingBuildingsHeadingLabel);
            this.dailyProductionValueLabel.Text = "0";
            this.dailyProductionValueLabel.Color = ARGBColors.Black;
            this.dailyProductionValueLabel.Position = new Point(330, 0x1b9);
            this.dailyProductionValueLabel.Size = new Size(400, 50);
            this.dailyProductionValueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.dailyProductionValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.dailyProductionValueLabel.Visible = false;
            this.mainBackgroundArea.addControl(this.dailyProductionValueLabel);
            this.totalBuildingsValueLabel.Text = "0";
            this.totalBuildingsValueLabel.Color = ARGBColors.Black;
            this.totalBuildingsValueLabel.Position = new Point(330, 0x1d2);
            this.totalBuildingsValueLabel.Size = new Size(400, 50);
            this.totalBuildingsValueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.totalBuildingsValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.totalBuildingsValueLabel.Visible = false;
            this.mainBackgroundArea.addControl(this.totalBuildingsValueLabel);
            this.workingBuildingsValueLabel.Text = "0";
            this.workingBuildingsValueLabel.Color = ARGBColors.Black;
            this.workingBuildingsValueLabel.Position = new Point(330, 0x1eb);
            this.workingBuildingsValueLabel.Size = new Size(400, 50);
            this.workingBuildingsValueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.workingBuildingsValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.workingBuildingsValueLabel.Visible = false;
            this.mainBackgroundArea.addControl(this.workingBuildingsValueLabel);
            this.cardbar.Position = new Point(0, 0);
            this.mainBackgroundArea.addControl(this.cardbar);
            this.cardbar.init(4);
            this.update();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "ResourcesPanel2";
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

        private void resourceClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDControl clickedControl = base.ClickedControl;
                this.selectedResource = clickedControl.Data;
                switch (this.selectedResource)
                {
                    case 6:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_wood");
                        break;

                    case 7:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_stone");
                        break;

                    case 8:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_iron");
                        break;

                    case 9:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_pitch");
                        break;

                    case 12:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_ale");
                        break;

                    case 13:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_apples");
                        break;

                    case 14:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_bread");
                        break;

                    case 15:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_veg");
                        break;

                    case 0x10:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_meat");
                        break;

                    case 0x11:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_cheese");
                        break;

                    case 0x12:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_fish");
                        break;

                    case 0x13:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_clothes");
                        break;

                    case 0x15:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_furniture");
                        break;

                    case 0x16:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_venison");
                        break;

                    case 0x17:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_salt");
                        break;

                    case 0x18:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_spices");
                        break;

                    case 0x19:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_silk");
                        break;

                    case 0x1a:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_metalware");
                        break;

                    case 0x1c:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_pikes");
                        break;

                    case 0x1d:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_bows");
                        break;

                    case 30:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_swords");
                        break;

                    case 0x1f:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_armour");
                        break;

                    case 0x20:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_catapult");
                        break;

                    case 0x21:
                        GameEngine.Instance.playInterfaceSound("ResourcesPanel2_resource_clicked_wine");
                        break;
                }
                this.selectedHeadingLabel.Visible = true;
                this.selectedImage.Image = (Image) GFXLibrary.getCommodity64DSImage(this.selectedResource);
                this.selectedImage.Visible = true;
                this.dailyProductionHeadingLabel.Visible = true;
                this.dailyProductionValueLabel.Visible = true;
                this.totalBuildingsHeadingLabel.Visible = true;
                this.totalBuildingsValueLabel.Visible = true;
                this.workingBuildingsHeadingLabel.Visible = true;
                this.workingBuildingsValueLabel.Visible = true;
                this.update();
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

        public void update()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                village.getStockpileLevels(levels);
                VillageMap.GranaryLevels levels2 = new VillageMap.GranaryLevels();
                village.getGranaryLevels(levels2);
                VillageMap.ArmouryLevels levels3 = new VillageMap.ArmouryLevels();
                village.getArmouryLevels(levels3);
                VillageMap.TownHallLevels levels4 = new VillageMap.TownHallLevels();
                village.getTownHallLevels(levels4);
                VillageMap.InnLevels levels5 = new VillageMap.InnLevels();
                village.getInnLevels(levels5);
                this.woodLabel.Text = levels.woodLevel.ToString("N", nFI);
                this.stoneLabel.Text = levels.stoneLevel.ToString("N", nFI);
                this.pitchLabel.Text = levels.pitchLevel.ToString("N", nFI);
                this.ironLabel.Text = levels.ironLevel.ToString("N", nFI);
                this.aleLabel.Text = levels5.aleLevel.ToString("N", nFI);
                this.applesLabel.Text = levels2.applesLevel.ToString("N", nFI);
                this.breadLabel.Text = levels2.breadLevel.ToString("N", nFI);
                this.cheeseLabel.Text = levels2.cheeseLevel.ToString("N", nFI);
                this.meatLabel.Text = levels2.meatLevel.ToString("N", nFI);
                this.vegLabel.Text = levels2.vegLevel.ToString("N", nFI);
                this.fishLabel.Text = levels2.fishLevel.ToString("N", nFI);
                this.bowsLabel.Text = levels3.bowsLevel.ToString("N", nFI);
                this.pikesLabel.Text = levels3.pikesLevel.ToString("N", nFI);
                this.swordsLabel.Text = levels3.swordsLevel.ToString("N", nFI);
                this.armourLabel.Text = levels3.armourLevel.ToString("N", nFI);
                this.catapultsLabel.Text = levels3.catapultsLevel.ToString("N", nFI);
                this.clothesLabel.Text = levels4.clothesLevel.ToString("N", nFI);
                this.furnitureLabel.Text = levels4.furnitureLevel.ToString("N", nFI);
                this.saltLabel.Text = levels4.saltLevel.ToString("N", nFI);
                this.wineLabel.Text = levels4.wineLevel.ToString("N", nFI);
                this.venisonLabel.Text = levels4.venisonLevel.ToString("N", nFI);
                this.spicesLabel.Text = levels4.spicesLevel.ToString("N", nFI);
                this.silkLabel.Text = levels4.silkLevel.ToString("N", nFI);
                this.metalwareLabel.Text = levels4.metalwareLevel.ToString("N", nFI);
                this.stockpileLimitLabel.Text = "(" + this.getCap(6).ToString("N", nFI) + ")";
                this.innLimitLabel.Text = "(" + this.getCap(12).ToString("N", nFI) + ")";
                this.granaryLimitLabel.Text = "(" + this.getCap(13).ToString("N", nFI) + ")";
                this.armouryLimitLabel.Text = "(" + this.getCap(0x1d).ToString("N", nFI) + ")";
                this.hallLimitLabel.Text = "(" + this.getCap(0x17).ToString("N", nFI) + ")";
                if (this.selectedResource >= 0)
                {
                    this.selectedHeadingLabel.Text = VillageBuildingsData.getResourceNames(this.selectedResource) + ": " + ((int) village.getResourceLevel(this.selectedResource)).ToString("N", nFI);
                    double num2 = village.getResourceProductionPerDay(this.selectedResource);
                    this.dailyProductionValueLabel.Text = ((int) num2).ToString("N", nFI);
                    this.totalBuildingsValueLabel.Text = village.numBuildingsOfType(this.selectedResource).ToString("N", nFI);
                    this.workingBuildingsValueLabel.Text = village.numWorkingBuildingsOfType(this.selectedResource).ToString("N", nFI);
                }
                this.cardbar.update();
            }
        }
    }
}

