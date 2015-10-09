namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CapitalResourcesPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDImage illustration = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage ironImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel ironLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage pitchImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel pitchLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel stockpileHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel stockpileLimitLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage stoneImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel stoneLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage woodImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel woodLabel = new CustomSelfDrawPanel.CSDLabel();

        public CapitalResourcesPanel2()
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
            return (int) GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, resourceType, true);
        }

        public void init()
        {
            base.clearControls();
            NumberFormatInfo nFI = GameEngine.NFI;
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.CustomTooltipID = 900;
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CapitalResourcesPanel2_close");
            this.mainBackgroundArea.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 11, new Point(0x382, 10));
            Color color = Color.FromArgb(0xe0, 0xcb, 0x92);
            Color color2 = Color.FromArgb(0x4a, 0x43, 0x30);
            this.lightPanel.Position = new Point(0x9d, 0x57);
            this.lightPanel.Size = new Size(0x157, 390);
            this.mainBackgroundArea.addControl(this.lightPanel);
            this.lightPanel.Create((Image) GFXLibrary.lite_9slice_panel_top_left, (Image) GFXLibrary.lite_9slice_panel_top_mid, (Image) GFXLibrary.lite_9slice_panel_top_right, (Image) GFXLibrary.lite_9slice_panel_mid_left, (Image) GFXLibrary.lite_9slice_panel_mid_mid, (Image) GFXLibrary.lite_9slice_panel_mid_right, (Image) GFXLibrary.lite_9slice_panel_bottom_left, (Image) GFXLibrary.lite_9slice_panel_bottom_mid, (Image) GFXLibrary.lite_9slice_panel_bottom_right);
            this.illustration.Image = (Image) GFXLibrary.donate_illustration;
            this.illustration.Position = new Point(0x201, 0x57);
            this.mainBackgroundArea.addControl(this.illustration);
            this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Parish_Resources", "Parish Resources");
            this.stockpileHeaderLabel.Color = color;
            this.stockpileHeaderLabel.DropShadowColor = color2;
            this.stockpileHeaderLabel.Position = new Point(9, 9);
            this.stockpileHeaderLabel.Size = new Size(0x3e0, 50);
            this.stockpileHeaderLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
            this.stockpileHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundArea.addControl(this.stockpileHeaderLabel);
            this.stockpileLimitLabel.Text = SK.Text("ResourcesPanel_Parish_Capacity", "Capacity of the Warehouse") + ": " + 0x186a0.ToString("N", nFI);
            this.stockpileLimitLabel.Color = color;
            this.stockpileLimitLabel.DropShadowColor = color2;
            this.stockpileLimitLabel.Position = new Point(0x20b, 0x1a2);
            this.stockpileLimitLabel.Size = new Size(0x145, 50);
            this.stockpileLimitLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.stockpileLimitLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundArea.addControl(this.stockpileLimitLabel);
            this.woodLabel.Text = "0";
            this.woodLabel.Color = color;
            this.woodLabel.DropShadowColor = color2;
            this.woodLabel.Position = new Point(120, 50);
            this.woodLabel.Size = new Size(200, 50);
            this.woodLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.woodLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lightPanel.addControl(this.woodLabel);
            this.stoneLabel.Text = "0";
            this.stoneLabel.Color = color;
            this.stoneLabel.DropShadowColor = color2;
            this.stoneLabel.Position = new Point(120, 0x87);
            this.stoneLabel.Size = new Size(200, 50);
            this.stoneLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.stoneLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lightPanel.addControl(this.stoneLabel);
            this.ironLabel.Text = "0";
            this.ironLabel.Color = color;
            this.ironLabel.DropShadowColor = color2;
            this.ironLabel.Position = new Point(120, 220);
            this.ironLabel.Size = new Size(200, 50);
            this.ironLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.ironLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lightPanel.addControl(this.ironLabel);
            this.pitchLabel.Text = "0";
            this.pitchLabel.Color = color;
            this.pitchLabel.DropShadowColor = color2;
            this.pitchLabel.Position = new Point(120, 0x131);
            this.pitchLabel.Size = new Size(200, 50);
            this.pitchLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.pitchLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lightPanel.addControl(this.pitchLabel);
            this.woodImage.Image = (Image) GFXLibrary.getCommodity64DSImage(6);
            this.woodImage.Position = new Point(0x12, 0x18);
            this.lightPanel.addControl(this.woodImage);
            this.stoneImage.Image = (Image) GFXLibrary.getCommodity64DSImage(7);
            this.stoneImage.Position = new Point(0x12, 0x6d);
            this.lightPanel.addControl(this.stoneImage);
            this.ironImage.Image = (Image) GFXLibrary.getCommodity64DSImage(8);
            this.ironImage.Position = new Point(0x12, 0xc2);
            this.lightPanel.addControl(this.ironImage);
            this.pitchImage.Image = (Image) GFXLibrary.getCommodity64DSImage(9);
            this.pitchImage.Position = new Point(0x12, 0x117);
            this.lightPanel.addControl(this.pitchImage);
            this.update();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CapitalResourcesPanel2";
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
                if (GameEngine.Instance.World.isRegionCapital(village.VillageID))
                {
                    this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Parish_Resources", "Parish Resources");
                }
                else if (GameEngine.Instance.World.isCountyCapital(village.VillageID))
                {
                    this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_County_Resources", "County Resources");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(village.VillageID))
                {
                    this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Province_Resources", "Province Resources");
                }
                else if (GameEngine.Instance.World.isCountryCapital(village.VillageID))
                {
                    this.stockpileHeaderLabel.Text = SK.Text("ResourcesPanel_Country_Resources", "Country Resources");
                }
                NumberFormatInfo nFI = GameEngine.NFI;
                VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                village.getStockpileLevels(levels);
                this.woodLabel.Text = SK.Text("ResourceTypeWood", "Wood") + ": " + levels.woodLevel.ToString("N", nFI);
                this.stoneLabel.Text = SK.Text("ResourceType_Stone", "Stone") + ": " + levels.stoneLevel.ToString("N", nFI);
                this.pitchLabel.Text = SK.Text("ResourceType_Pitch", "Pitch") + ": " + levels.pitchLevel.ToString("N", nFI);
                this.ironLabel.Text = SK.Text("ResourceType_Iron", "Iron") + ": " + levels.ironLevel.ToString("N", nFI);
            }
        }
    }
}

