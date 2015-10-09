namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class HoldBanquetPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDLabel banquetLabel = new CustomSelfDrawPanel.CSDLabel();
        public int[,] banquetLevels = new int[8, 8];
        private CustomSelfDrawPanel.CSDControl banquetRow1 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl banquetRow2 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl banquetRow3 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl banquetRow4 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl banquetRow5 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl banquetRow6 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl banquetRow7 = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDControl banquetRow8 = new CustomSelfDrawPanel.CSDControl();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel heading1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel heading2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel heading3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel heading4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton holdBandquetRow8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage honourImageRow1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourImageRow2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourImageRow3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourImageRow4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourImageRow5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourImageRow6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourImageRow7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourImageRow8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel honourRow1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourRow2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourRow3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourRow4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourRow5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourRow6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourRow7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourRow8 = new CustomSelfDrawPanel.CSDLabel();
        private const int imageSpacing = 0x20;
        public static HoldBanquetPanel Instance;
        private CustomSelfDrawPanel.CSDImage leftBracket1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage leftBracket2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage leftBracket3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage leftBracket4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage leftBracket5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage leftBracket6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage leftBracket7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage leftBracket8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea1 = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea2 = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel lightArea3 = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel mainWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel noResearchText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel noResearchWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel numResourcesRow8 = new CustomSelfDrawPanel.CSDLabel();
        private VillageMap.TownHallLevels oldLevels;
        private CustomSelfDrawPanel.CSDImage resourceLevelImages1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage resourceLevelImages2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage resourceLevelImages3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage resourceLevelImages4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage resourceLevelImages5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage resourceLevelImages6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage resourceLevelImages7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage resourceLevelImages8 = new CustomSelfDrawPanel.CSDImage();
        public int[] resourceLevels = new int[8];
        private CustomSelfDrawPanel.CSDLabel resourceLevelText1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourceLevelText2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourceLevelText3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourceLevelText4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourceLevelText5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourceLevelText6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourceLevelText7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel resourceLevelText8 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel resourcesBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDImage rightBracket1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rightBracket2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rightBracket3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rightBracket4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rightBracket5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rightBracket6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rightBracket7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rightBracket8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type1Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type2Row2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type2Row3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type2Row4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type2Row5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type2Row6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type2Row7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type2Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type3Row3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type3Row4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type3Row5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type3Row6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type3Row7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type3Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type4Row4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type4Row5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type4Row6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type4Row7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type4Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type5Row5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type5Row6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type5Row7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type5Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type6Row6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type6Row7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type6Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type7Row7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type7Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage type8Row8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel xRow1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel xRow2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel xRow3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel xRow4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel xRow5 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel xRow6 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel xRow7 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel xRow8 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage xShadowRow1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage xShadowRow2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage xShadowRow3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage xShadowRow4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage xShadowRow5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage xShadowRow6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage xShadowRow7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage xShadowRow8 = new CustomSelfDrawPanel.CSDImage();

        public HoldBanquetPanel()
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

        private BaseImage getImage(int resource)
        {
            switch (resource)
            {
                case 0:
                    return GFXLibrary.com_32_venison_DS;

                case 1:
                    return GFXLibrary.com_32_furniture_DS;

                case 2:
                    return GFXLibrary.com_32_metalware_DS;

                case 3:
                    return GFXLibrary.com_32_clothes_DS;

                case 4:
                    return GFXLibrary.com_32_wine_DS;

                case 5:
                    return GFXLibrary.com_32_salt_DS;

                case 6:
                    return GFXLibrary.com_32_spices_DS;

                case 7:
                    return GFXLibrary.com_32_silk_DS;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDImage getTypePanel(int banquetSize, int column)
        {
            switch (banquetSize)
            {
                case 1:
                    if (column != 0)
                    {
                        break;
                    }
                    return this.type1Row1;

                case 2:
                    switch (column)
                    {
                        case 0:
                            return this.type1Row2;

                        case 1:
                            return this.type2Row2;
                    }
                    break;

                case 3:
                    switch (column)
                    {
                        case 0:
                            return this.type1Row3;

                        case 1:
                            return this.type2Row3;

                        case 2:
                            return this.type3Row3;
                    }
                    break;

                case 4:
                    switch (column)
                    {
                        case 0:
                            return this.type1Row4;

                        case 1:
                            return this.type2Row4;

                        case 2:
                            return this.type3Row4;

                        case 3:
                            return this.type4Row4;
                    }
                    break;

                case 5:
                    switch (column)
                    {
                        case 0:
                            return this.type1Row5;

                        case 1:
                            return this.type2Row5;

                        case 2:
                            return this.type3Row5;

                        case 3:
                            return this.type4Row5;

                        case 4:
                            return this.type5Row5;
                    }
                    break;

                case 6:
                    switch (column)
                    {
                        case 0:
                            return this.type1Row6;

                        case 1:
                            return this.type2Row6;

                        case 2:
                            return this.type3Row6;

                        case 3:
                            return this.type4Row6;

                        case 4:
                            return this.type5Row6;

                        case 5:
                            return this.type6Row6;
                    }
                    break;

                case 7:
                    switch (column)
                    {
                        case 0:
                            return this.type1Row7;

                        case 1:
                            return this.type2Row7;

                        case 2:
                            return this.type3Row7;

                        case 3:
                            return this.type4Row7;

                        case 4:
                            return this.type5Row7;

                        case 5:
                            return this.type6Row7;

                        case 6:
                            return this.type7Row7;
                    }
                    break;

                case 8:
                    switch (column)
                    {
                        case 0:
                            return this.type1Row8;

                        case 1:
                            return this.type2Row8;

                        case 2:
                            return this.type3Row8;

                        case 3:
                            return this.type4Row8;

                        case 4:
                            return this.type5Row8;

                        case 5:
                            return this.type6Row8;

                        case 6:
                            return this.type7Row8;

                        case 7:
                            return this.type8Row8;
                    }
                    break;
            }
            return null;
        }

        private bool holdBanquet(int mode)
        {
            bool flag = false;
            for (int i = 0; i < 8; i++)
            {
                if (this.banquetLevels[mode, i] > 0)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    RemoteServices.Instance.VillageHoldBanquet(village.VillageID, this.banquetLevels[mode, 0], this.banquetLevels[mode, 1], this.banquetLevels[mode, 2], this.banquetLevels[mode, 3], this.banquetLevels[mode, 4], this.banquetLevels[mode, 5], this.banquetLevels[mode, 6], this.banquetLevels[mode, 7]);
                }
            }
            return flag;
        }

        public void holdBanquetClick()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (this.holdBanquet(clickedControl.Data))
                {
                    clickedControl.Enabled = false;
                }
            }
        }

        public void init()
        {
            Instance = this;
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("BanquetScreen_Banqueting", "Banqueting"));
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 10);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "HoldBanquetPanel_close");
            this.closeButton.CustomTooltipID = 0x3e8;
            base.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 7, new Point(0x382, 10));
            int num2 = GameEngine.Instance.World.UserResearchData.Research_Craftsmanship;
            if (num2 > 0)
            {
                int num3 = 80;
                int x = 20;
                int num5 = 13;
                int num6 = 0;
                int width = ((num2 * num3) + x) + num5;
                if (width < 0x9c)
                {
                    num6 = (0x9c - width) / 2;
                    width = 0x9c;
                    x += num6;
                }
                this.resourcesBox.Size = new Size(width, 0x43);
                this.resourcesBox.Position = new Point((160 + ((8 - num2) * (num3 / 2))) - num6, 0x4f);
                this.mainBackgroundImage.addControl(this.resourcesBox);
                this.resourcesBox.Create((Image) GFXLibrary.int_insetbar_a_left, (Image) GFXLibrary.int_insetbar_a_middle, (Image) GFXLibrary.int_insetbar_a_right);
                if (num2 >= 1)
                {
                    this.resourceLevelImages1.Image = (Image) GFXLibrary.com_64_venison_DS;
                    this.resourceLevelImages1.Position = new Point((x + 0x2a) - 0x2d, -37);
                    this.resourcesBox.addControl(this.resourceLevelImages1);
                    this.resourceLevelText1.Text = "100";
                    this.resourceLevelText1.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText1.Position = new Point(x, 0x2d);
                    this.resourceLevelText1.Size = new Size(0x53, 12);
                    this.resourceLevelText1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText1);
                }
                if (num2 >= 2)
                {
                    this.resourceLevelImages2.Image = (Image) GFXLibrary.com_64_furniture_DS;
                    this.resourceLevelImages2.Position = new Point(((x + 0x2a) - 0x2d) + num3, -37);
                    this.resourcesBox.addControl(this.resourceLevelImages2);
                    this.resourceLevelText2.Text = "100";
                    this.resourceLevelText2.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText2.Position = new Point(x + num3, 0x2d);
                    this.resourceLevelText2.Size = new Size(0x53, 12);
                    this.resourceLevelText2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText2);
                }
                if (num2 >= 3)
                {
                    this.resourceLevelImages3.Image = (Image) GFXLibrary.com_64_metalware_DS;
                    this.resourceLevelImages3.Position = new Point(((x + 0x2a) - 0x2d) + (2 * num3), -37);
                    this.resourcesBox.addControl(this.resourceLevelImages3);
                    this.resourceLevelText3.Text = "100";
                    this.resourceLevelText3.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText3.Position = new Point(x + (2 * num3), 0x2d);
                    this.resourceLevelText3.Size = new Size(0x53, 12);
                    this.resourceLevelText3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText3);
                }
                if (num2 >= 4)
                {
                    this.resourceLevelImages4.Image = (Image) GFXLibrary.com_64_clothes_DS;
                    this.resourceLevelImages4.Position = new Point(((x + 0x2a) - 0x2d) + (3 * num3), -37);
                    this.resourcesBox.addControl(this.resourceLevelImages4);
                    this.resourceLevelText4.Text = "100";
                    this.resourceLevelText4.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText4.Position = new Point(x + (3 * num3), 0x2d);
                    this.resourceLevelText4.Size = new Size(0x53, 12);
                    this.resourceLevelText4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText4);
                }
                if (num2 >= 5)
                {
                    this.resourceLevelImages5.Image = (Image) GFXLibrary.com_64_wine_DS;
                    this.resourceLevelImages5.Position = new Point(((x + 0x2a) - 0x2d) + (4 * num3), -37);
                    this.resourcesBox.addControl(this.resourceLevelImages5);
                    this.resourceLevelText5.Text = "100";
                    this.resourceLevelText5.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText5.Position = new Point(x + (4 * num3), 0x2d);
                    this.resourceLevelText5.Size = new Size(0x53, 12);
                    this.resourceLevelText5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText5);
                }
                if (num2 >= 6)
                {
                    this.resourceLevelImages6.Image = (Image) GFXLibrary.com_64_salt_DS;
                    this.resourceLevelImages6.Position = new Point(((x + 0x2a) - 0x2d) + (5 * num3), -37);
                    this.resourcesBox.addControl(this.resourceLevelImages6);
                    this.resourceLevelText6.Text = "100";
                    this.resourceLevelText6.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText6.Position = new Point(x + (5 * num3), 0x2d);
                    this.resourceLevelText6.Size = new Size(0x53, 12);
                    this.resourceLevelText6.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText6);
                }
                if (num2 >= 7)
                {
                    this.resourceLevelImages7.Image = (Image) GFXLibrary.com_64_spices_DS;
                    this.resourceLevelImages7.Position = new Point(((x + 0x2a) - 0x2d) + (6 * num3), -37);
                    this.resourcesBox.addControl(this.resourceLevelImages7);
                    this.resourceLevelText7.Text = "100";
                    this.resourceLevelText7.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText7.Position = new Point(x + (6 * num3), 0x2d);
                    this.resourceLevelText7.Size = new Size(0x53, 12);
                    this.resourceLevelText7.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText7);
                }
                if (num2 >= 8)
                {
                    this.resourceLevelImages8.Image = (Image) GFXLibrary.com_64_silk_DS;
                    this.resourceLevelImages8.Position = new Point(((x + 0x2a) - 0x2d) + (7 * num3), -37);
                    this.resourcesBox.addControl(this.resourceLevelImages8);
                    this.resourceLevelText8.Text = "100";
                    this.resourceLevelText8.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                    this.resourceLevelText8.Position = new Point(x + (7 * num3), 0x2d);
                    this.resourceLevelText8.Size = new Size(0x53, 12);
                    this.resourceLevelText8.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.resourceLevelText8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.resourcesBox.addControl(this.resourceLevelText8);
                }
                this.mainWindow.Size = new Size(0x2e3, 0x18f);
                this.mainWindow.Position = new Point(0x83, 0x9d);
                this.mainBackgroundImage.addControl(this.mainWindow);
                this.mainWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
                this.heading1.Text = SK.Text("BanquetScreen_Goods", "Goods");
                this.heading1.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.heading1.Position = new Point(0x17, 20);
                this.heading1.Size = new Size(0x15f, 20);
                this.heading1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.heading1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.mainWindow.addControl(this.heading1);
                this.lightArea1.Size = new Size(0x15f, 330);
                this.lightArea1.Position = new Point(0x17, 0x2f);
                this.mainWindow.addControl(this.lightArea1);
                this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
                this.heading2.Text = SK.Text("BanquetScreen_Multiplier", "Multiplier");
                this.heading2.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.heading2.Position = new Point(350, 20);
                this.heading2.Size = new Size(0x71, 20);
                this.heading2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.heading2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.mainWindow.addControl(this.heading2);
                this.lightArea2.Size = new Size(0x35, 330);
                this.lightArea2.Position = new Point(380, 0x2f);
                this.mainWindow.addControl(this.lightArea2);
                this.lightArea2.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
                this.heading3.Text = SK.Text("GENERIC_Honour", "Honour");
                this.heading3.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.heading3.Position = new Point(0x1b7, 20);
                this.heading3.Size = new Size(0x77, 20);
                this.heading3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.heading3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.mainWindow.addControl(this.heading3);
                this.lightArea3.Size = new Size(0x77, 330);
                this.lightArea3.Position = new Point(0x1b7, 0x2f);
                this.mainWindow.addControl(this.lightArea3);
                this.lightArea3.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
                this.heading4.Text = SK.Text("BanquetScreen_Hold_Banquet", "Hold Banquet");
                this.heading4.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.heading4.Position = new Point(0x235, 20);
                this.heading4.Size = new Size(0xa3, 20);
                this.heading4.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.heading4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.mainWindow.addControl(this.heading4);
                this.banquetRow1.Position = new Point(0, 0x37);
                this.banquetRow1.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow1);
                this.banquetRow2.Position = new Point(0, 0x5f);
                this.banquetRow2.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow2);
                this.banquetRow3.Position = new Point(0, 0x87);
                this.banquetRow3.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow3);
                this.banquetRow4.Position = new Point(0, 0xaf);
                this.banquetRow4.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow4);
                this.banquetRow5.Position = new Point(0, 0xd7);
                this.banquetRow5.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow5);
                this.banquetRow6.Position = new Point(0, 0xff);
                this.banquetRow6.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow6);
                this.banquetRow7.Position = new Point(0, 0x127);
                this.banquetRow7.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow7);
                this.banquetRow8.Position = new Point(0, 0x14f);
                this.banquetRow8.Size = new Size(0x2de, 40);
                this.mainWindow.addControl(this.banquetRow8);
                this.leftBracket1.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket1.Position = new Point(0x4a, 2);
                this.banquetRow1.addControl(this.leftBracket1);
                this.rightBracket1.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket1.Position = new Point(120, 2);
                this.banquetRow1.addControl(this.rightBracket1);
                this.type1Row1.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row1.Position = new Point(0x54, -2);
                this.banquetRow1.addControl(this.type1Row1);
                this.numResourcesRow1.Text = "100";
                this.numResourcesRow1.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow1.Position = new Point(0x17, -1);
                this.numResourcesRow1.Size = new Size(0x2d, 40);
                this.numResourcesRow1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow1.addControl(this.numResourcesRow1);
                this.xShadowRow1.Image = (Image) GFXLibrary.int_multiplyer_shadow_x1;
                this.xShadowRow1.Position = new Point(0x18c, 13);
                this.banquetRow1.addControl(this.xShadowRow1);
                this.xRow1.Text = "x1";
                this.xRow1.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow1.Position = new Point(380, -1);
                this.xRow1.Size = new Size(0x35, 40);
                this.xRow1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow1.addControl(this.xRow1);
                this.honourRow1.Text = "= 100";
                this.honourRow1.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow1.Position = new Point(0x1b7, -1);
                this.honourRow1.Size = new Size(0x4b, 40);
                this.honourRow1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow1.addControl(this.honourRow1);
                this.honourImageRow1.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow1.Position = new Point(0x206, -2);
                this.banquetRow1.addControl(this.honourImageRow1);
                this.holdBandquetRow1.Position = new Point(0x235, 1);
                this.holdBandquetRow1.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow1.Text.Text = SK.Text("BanquetScreen_Size_1", "Humble");
                this.holdBandquetRow1.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow1.TextYOffset = -1;
                this.holdBandquetRow1.Text.Color = ARGBColors.Black;
                this.holdBandquetRow1.Data = 0;
                this.holdBandquetRow1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_humble");
                this.banquetRow1.addControl(this.holdBandquetRow1);
                this.holdBandquetRow1.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow1.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                this.leftBracket2.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket2.Position = new Point(0x4a, 2);
                this.banquetRow2.addControl(this.leftBracket2);
                this.rightBracket2.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket2.Position = new Point(0x98, 2);
                this.banquetRow2.addControl(this.rightBracket2);
                this.type1Row2.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row2.Position = new Point(0x54, -2);
                this.banquetRow2.addControl(this.type1Row2);
                this.type2Row2.Image = (Image) GFXLibrary.com_32_furniture_DS;
                this.type2Row2.Position = new Point(0x74, -2);
                this.banquetRow2.addControl(this.type2Row2);
                this.numResourcesRow2.Text = "100";
                this.numResourcesRow2.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow2.Position = new Point(0x17, -1);
                this.numResourcesRow2.Size = new Size(0x2d, 40);
                this.numResourcesRow2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow2.addControl(this.numResourcesRow2);
                this.xShadowRow2.Image = (Image) GFXLibrary.int_multiplyer_shadow_x1;
                this.xShadowRow2.Position = new Point(0x18c, 13);
                this.banquetRow2.addControl(this.xShadowRow2);
                this.xRow2.Text = "x4";
                this.xRow2.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow2.Position = new Point(380, -1);
                this.xRow2.Size = new Size(0x35, 40);
                this.xRow2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow2.addControl(this.xRow2);
                this.honourRow2.Text = "= 100";
                this.honourRow2.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow2.Position = new Point(0x1b7, -1);
                this.honourRow2.Size = new Size(0x4b, 40);
                this.honourRow2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow2.addControl(this.honourRow2);
                this.honourImageRow2.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow2.Position = new Point(0x206, -2);
                this.banquetRow2.addControl(this.honourImageRow2);
                this.holdBandquetRow2.Position = new Point(0x235, 1);
                this.holdBandquetRow2.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow2.Text.Text = SK.Text("BanquetScreen_Size_2", "Modest");
                this.holdBandquetRow2.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow2.TextYOffset = -1;
                this.holdBandquetRow2.Text.Color = ARGBColors.Black;
                this.holdBandquetRow2.Data = 1;
                this.holdBandquetRow2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_modest");
                this.banquetRow2.addControl(this.holdBandquetRow2);
                this.holdBandquetRow2.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow2.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                this.leftBracket3.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket3.Position = new Point(0x4a, 2);
                this.banquetRow3.addControl(this.leftBracket3);
                this.rightBracket3.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket3.Position = new Point(0xb8, 2);
                this.banquetRow3.addControl(this.rightBracket3);
                this.type1Row3.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row3.Position = new Point(0x54, -2);
                this.banquetRow3.addControl(this.type1Row3);
                this.type2Row3.Image = (Image) GFXLibrary.com_32_furniture_DS;
                this.type2Row3.Position = new Point(0x74, -2);
                this.banquetRow3.addControl(this.type2Row3);
                this.type3Row3.Image = (Image) GFXLibrary.com_32_metalware_DS;
                this.type3Row3.Position = new Point(0x94, -2);
                this.banquetRow3.addControl(this.type3Row3);
                this.numResourcesRow3.Text = "100";
                this.numResourcesRow3.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow3.Position = new Point(0x17, -1);
                this.numResourcesRow3.Size = new Size(0x2d, 40);
                this.numResourcesRow3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow3.addControl(this.numResourcesRow3);
                this.xShadowRow3.Image = (Image) GFXLibrary.int_multiplyer_shadow_x1;
                this.xShadowRow3.Position = new Point(0x18c, 13);
                this.banquetRow3.addControl(this.xShadowRow3);
                this.xRow3.Text = "x9";
                this.xRow3.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow3.Position = new Point(380, -1);
                this.xRow3.Size = new Size(0x35, 40);
                this.xRow3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow3.addControl(this.xRow3);
                this.honourRow3.Text = "= 100";
                this.honourRow3.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow3.Position = new Point(0x1b7, -1);
                this.honourRow3.Size = new Size(0x4b, 40);
                this.honourRow3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow3.addControl(this.honourRow3);
                this.honourImageRow3.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow3.Position = new Point(0x206, -2);
                this.banquetRow3.addControl(this.honourImageRow3);
                this.holdBandquetRow3.Position = new Point(0x235, 1);
                this.holdBandquetRow3.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow3.Text.Text = SK.Text("BanquetScreen_Size_3", "Fine");
                this.holdBandquetRow3.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow3.TextYOffset = -1;
                this.holdBandquetRow3.Text.Color = ARGBColors.Black;
                this.holdBandquetRow3.Data = 2;
                this.holdBandquetRow3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_fine");
                this.banquetRow3.addControl(this.holdBandquetRow3);
                this.holdBandquetRow3.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow3.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                this.leftBracket4.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket4.Position = new Point(0x4a, 2);
                this.banquetRow4.addControl(this.leftBracket4);
                this.rightBracket4.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket4.Position = new Point(0xd8, 2);
                this.banquetRow4.addControl(this.rightBracket4);
                this.type1Row4.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row4.Position = new Point(0x54, -2);
                this.banquetRow4.addControl(this.type1Row4);
                this.type2Row4.Image = (Image) GFXLibrary.com_32_furniture_DS;
                this.type2Row4.Position = new Point(0x74, -2);
                this.banquetRow4.addControl(this.type2Row4);
                this.type3Row4.Image = (Image) GFXLibrary.com_32_metalware_DS;
                this.type3Row4.Position = new Point(0x94, -2);
                this.banquetRow4.addControl(this.type3Row4);
                this.type4Row4.Image = (Image) GFXLibrary.com_32_clothes_DS;
                this.type4Row4.Position = new Point(180, -2);
                this.banquetRow4.addControl(this.type4Row4);
                this.numResourcesRow4.Text = "100";
                this.numResourcesRow4.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow4.Position = new Point(0x17, -1);
                this.numResourcesRow4.Size = new Size(0x2d, 40);
                this.numResourcesRow4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow4.addControl(this.numResourcesRow4);
                this.xShadowRow4.Image = (Image) GFXLibrary.int_multiplyer_shadow_x2;
                this.xShadowRow4.Position = new Point(0x18a, 13);
                this.banquetRow4.addControl(this.xShadowRow4);
                this.xRow4.Text = "x16";
                this.xRow4.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow4.Position = new Point(380, -1);
                this.xRow4.Size = new Size(0x35, 40);
                this.xRow4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow4.addControl(this.xRow4);
                this.honourRow4.Text = "= 100";
                this.honourRow4.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow4.Position = new Point(0x1b7, -1);
                this.honourRow4.Size = new Size(0x4b, 40);
                this.honourRow4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow4.addControl(this.honourRow4);
                this.honourImageRow4.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow4.Position = new Point(0x206, -2);
                this.banquetRow4.addControl(this.honourImageRow4);
                this.holdBandquetRow4.Position = new Point(0x235, 1);
                this.holdBandquetRow4.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow4.Text.Text = SK.Text("BanquetScreen_Size_4", "Impressive");
                this.holdBandquetRow4.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow4.TextYOffset = -1;
                this.holdBandquetRow4.Text.Color = ARGBColors.Black;
                this.holdBandquetRow4.Data = 3;
                this.holdBandquetRow4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_impressive");
                this.banquetRow4.addControl(this.holdBandquetRow4);
                this.holdBandquetRow4.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow4.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                this.leftBracket5.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket5.Position = new Point(0x4a, 2);
                this.banquetRow5.addControl(this.leftBracket5);
                this.rightBracket5.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket5.Position = new Point(0xf8, 2);
                this.banquetRow5.addControl(this.rightBracket5);
                this.type1Row5.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row5.Position = new Point(0x54, -2);
                this.banquetRow5.addControl(this.type1Row5);
                this.type2Row5.Image = (Image) GFXLibrary.com_32_furniture_DS;
                this.type2Row5.Position = new Point(0x74, -2);
                this.banquetRow5.addControl(this.type2Row5);
                this.type3Row5.Image = (Image) GFXLibrary.com_32_metalware_DS;
                this.type3Row5.Position = new Point(0x94, -2);
                this.banquetRow5.addControl(this.type3Row5);
                this.type4Row5.Image = (Image) GFXLibrary.com_32_clothes_DS;
                this.type4Row5.Position = new Point(180, -2);
                this.banquetRow5.addControl(this.type4Row5);
                this.type5Row5.Image = (Image) GFXLibrary.com_32_wine_DS;
                this.type5Row5.Position = new Point(0xd4, -2);
                this.banquetRow5.addControl(this.type5Row5);
                this.numResourcesRow5.Text = "100";
                this.numResourcesRow5.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow5.Position = new Point(0x17, -1);
                this.numResourcesRow5.Size = new Size(0x2d, 40);
                this.numResourcesRow5.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow5.addControl(this.numResourcesRow5);
                this.xShadowRow5.Image = (Image) GFXLibrary.int_multiplyer_shadow_x2;
                this.xShadowRow5.Position = new Point(0x18a, 13);
                this.banquetRow5.addControl(this.xShadowRow5);
                this.xRow5.Text = "x35";
                this.xRow5.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow5.Position = new Point(380, -1);
                this.xRow5.Size = new Size(0x35, 40);
                this.xRow5.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow5.addControl(this.xRow5);
                this.honourRow5.Text = "= 100";
                this.honourRow5.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow5.Position = new Point(0x1b7, -1);
                this.honourRow5.Size = new Size(0x4b, 40);
                this.honourRow5.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow5.addControl(this.honourRow5);
                this.honourImageRow5.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow5.Position = new Point(0x206, -2);
                this.banquetRow5.addControl(this.honourImageRow5);
                this.holdBandquetRow5.Position = new Point(0x235, 1);
                this.holdBandquetRow5.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow5.Text.Text = SK.Text("BanquetScreen_Size_5", "Grand");
                this.holdBandquetRow5.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow5.TextYOffset = -1;
                this.holdBandquetRow5.Text.Color = ARGBColors.Black;
                this.holdBandquetRow5.Data = 4;
                this.holdBandquetRow5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_grand");
                this.banquetRow5.addControl(this.holdBandquetRow5);
                this.holdBandquetRow5.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow5.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                this.leftBracket6.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket6.Position = new Point(0x4a, 2);
                this.banquetRow6.addControl(this.leftBracket6);
                this.rightBracket6.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket6.Position = new Point(280, 2);
                this.banquetRow6.addControl(this.rightBracket6);
                this.type1Row6.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row6.Position = new Point(0x54, -2);
                this.banquetRow6.addControl(this.type1Row6);
                this.type2Row6.Image = (Image) GFXLibrary.com_32_furniture_DS;
                this.type2Row6.Position = new Point(0x74, -2);
                this.banquetRow6.addControl(this.type2Row6);
                this.type3Row6.Image = (Image) GFXLibrary.com_32_metalware_DS;
                this.type3Row6.Position = new Point(0x94, -2);
                this.banquetRow6.addControl(this.type3Row6);
                this.type4Row6.Image = (Image) GFXLibrary.com_32_clothes_DS;
                this.type4Row6.Position = new Point(180, -2);
                this.banquetRow6.addControl(this.type4Row6);
                this.type5Row6.Image = (Image) GFXLibrary.com_32_wine_DS;
                this.type5Row6.Position = new Point(0xd4, -2);
                this.banquetRow6.addControl(this.type5Row6);
                this.type6Row6.Image = (Image) GFXLibrary.com_32_salt_DS;
                this.type6Row6.Position = new Point(0xf4, -2);
                this.banquetRow6.addControl(this.type6Row6);
                this.numResourcesRow6.Text = "100";
                this.numResourcesRow6.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow6.Position = new Point(0x17, -1);
                this.numResourcesRow6.Size = new Size(0x2d, 40);
                this.numResourcesRow6.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow6.addControl(this.numResourcesRow6);
                this.xShadowRow6.Image = (Image) GFXLibrary.int_multiplyer_shadow_x2;
                this.xShadowRow6.Position = new Point(0x18a, 13);
                this.banquetRow6.addControl(this.xShadowRow6);
                this.xRow6.Text = "x60";
                this.xRow6.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow6.Position = new Point(380, -1);
                this.xRow6.Size = new Size(0x35, 40);
                this.xRow6.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow6.addControl(this.xRow6);
                this.honourRow6.Text = "= 100";
                this.honourRow6.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow6.Position = new Point(0x1b7, -1);
                this.honourRow6.Size = new Size(0x4b, 40);
                this.honourRow6.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow6.addControl(this.honourRow6);
                this.honourImageRow6.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow6.Position = new Point(0x206, -2);
                this.banquetRow6.addControl(this.honourImageRow6);
                this.holdBandquetRow6.Position = new Point(0x235, 1);
                this.holdBandquetRow6.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow6.Text.Text = SK.Text("BanquetScreen_Size_6", "Magnificent");
                this.holdBandquetRow6.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow6.TextYOffset = -1;
                this.holdBandquetRow6.Text.Color = ARGBColors.Black;
                this.holdBandquetRow6.Data = 5;
                this.holdBandquetRow6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_magnificent");
                this.banquetRow6.addControl(this.holdBandquetRow6);
                this.holdBandquetRow6.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow6.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                this.leftBracket7.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket7.Position = new Point(0x4a, 2);
                this.banquetRow7.addControl(this.leftBracket7);
                this.rightBracket7.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket7.Position = new Point(0x138, 2);
                this.banquetRow7.addControl(this.rightBracket7);
                this.type1Row7.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row7.Position = new Point(0x54, -2);
                this.banquetRow7.addControl(this.type1Row7);
                this.type2Row7.Image = (Image) GFXLibrary.com_32_furniture_DS;
                this.type2Row7.Position = new Point(0x74, -2);
                this.banquetRow7.addControl(this.type2Row7);
                this.type3Row7.Image = (Image) GFXLibrary.com_32_metalware_DS;
                this.type3Row7.Position = new Point(0x94, -2);
                this.banquetRow7.addControl(this.type3Row7);
                this.type4Row7.Image = (Image) GFXLibrary.com_32_clothes_DS;
                this.type4Row7.Position = new Point(180, -2);
                this.banquetRow7.addControl(this.type4Row7);
                this.type5Row7.Image = (Image) GFXLibrary.com_32_wine_DS;
                this.type5Row7.Position = new Point(0xd4, -2);
                this.banquetRow7.addControl(this.type5Row7);
                this.type6Row7.Image = (Image) GFXLibrary.com_32_salt_DS;
                this.type6Row7.Position = new Point(0xf4, -2);
                this.banquetRow7.addControl(this.type6Row7);
                this.type7Row7.Image = (Image) GFXLibrary.com_32_spices_DS;
                this.type7Row7.Position = new Point(0x114, -2);
                this.banquetRow7.addControl(this.type7Row7);
                this.numResourcesRow7.Text = "100";
                this.numResourcesRow7.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow7.Position = new Point(0x17, -1);
                this.numResourcesRow7.Size = new Size(0x2d, 40);
                this.numResourcesRow7.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow7.addControl(this.numResourcesRow7);
                this.xShadowRow7.Image = (Image) GFXLibrary.int_multiplyer_shadow_x3;
                this.xShadowRow7.Position = new Point(0x189, 13);
                this.banquetRow7.addControl(this.xShadowRow7);
                this.xRow7.Text = "x98";
                this.xRow7.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow7.Position = new Point(380, -1);
                this.xRow7.Size = new Size(0x35, 40);
                this.xRow7.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow7.addControl(this.xRow7);
                this.honourRow7.Text = "= 100";
                this.honourRow7.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow7.Position = new Point(0x1b7, -1);
                this.honourRow7.Size = new Size(0x4b, 40);
                this.honourRow7.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow7.addControl(this.honourRow7);
                this.honourImageRow7.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow7.Position = new Point(0x206, -2);
                this.banquetRow7.addControl(this.honourImageRow7);
                this.holdBandquetRow7.Position = new Point(0x235, 1);
                this.holdBandquetRow7.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow7.Text.Text = SK.Text("BanquetScreen_Size_7", "Majestic");
                this.holdBandquetRow7.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow7.TextYOffset = -1;
                this.holdBandquetRow7.Text.Color = ARGBColors.Black;
                this.holdBandquetRow7.Data = 6;
                this.holdBandquetRow7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_majestic");
                this.banquetRow7.addControl(this.holdBandquetRow7);
                this.holdBandquetRow7.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow7.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                this.leftBracket8.Image = (Image) GFXLibrary.int_parenthesis_left;
                this.leftBracket8.Position = new Point(0x4a, 2);
                this.banquetRow8.addControl(this.leftBracket8);
                this.rightBracket8.Image = (Image) GFXLibrary.int_parenthesis_right;
                this.rightBracket8.Position = new Point(0x158, 2);
                this.banquetRow8.addControl(this.rightBracket8);
                this.type1Row8.Image = (Image) GFXLibrary.com_32_venison_DS;
                this.type1Row8.Position = new Point(0x54, -2);
                this.banquetRow8.addControl(this.type1Row8);
                this.type2Row8.Image = (Image) GFXLibrary.com_32_furniture_DS;
                this.type2Row8.Position = new Point(0x74, -2);
                this.banquetRow8.addControl(this.type2Row8);
                this.type3Row8.Image = (Image) GFXLibrary.com_32_metalware_DS;
                this.type3Row8.Position = new Point(0x94, -2);
                this.banquetRow8.addControl(this.type3Row8);
                this.type4Row8.Image = (Image) GFXLibrary.com_32_clothes_DS;
                this.type4Row8.Position = new Point(180, -2);
                this.banquetRow8.addControl(this.type4Row8);
                this.type5Row8.Image = (Image) GFXLibrary.com_32_wine_DS;
                this.type5Row8.Position = new Point(0xd4, -2);
                this.banquetRow8.addControl(this.type5Row8);
                this.type6Row8.Image = (Image) GFXLibrary.com_32_salt_DS;
                this.type6Row8.Position = new Point(0xf4, -2);
                this.banquetRow8.addControl(this.type6Row8);
                this.type7Row8.Image = (Image) GFXLibrary.com_32_spices_DS;
                this.type7Row8.Position = new Point(0x114, -2);
                this.banquetRow8.addControl(this.type7Row8);
                this.type8Row8.Image = (Image) GFXLibrary.com_32_silk_DS;
                this.type8Row8.Position = new Point(0x134, -2);
                this.banquetRow8.addControl(this.type8Row8);
                this.numResourcesRow8.Text = "100";
                this.numResourcesRow8.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.numResourcesRow8.Position = new Point(0x17, -1);
                this.numResourcesRow8.Size = new Size(0x2d, 40);
                this.numResourcesRow8.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.numResourcesRow8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow8.addControl(this.numResourcesRow8);
                this.xShadowRow8.Image = (Image) GFXLibrary.int_multiplyer_shadow_x3;
                this.xShadowRow8.Position = new Point(0x189, 13);
                this.banquetRow8.addControl(this.xShadowRow8);
                this.xRow8.Text = "x160";
                this.xRow8.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                this.xRow8.Position = new Point(380, -1);
                this.xRow8.Size = new Size(0x35, 40);
                this.xRow8.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.xRow8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.banquetRow8.addControl(this.xRow8);
                this.honourRow8.Text = "= 100";
                this.honourRow8.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.honourRow8.Position = new Point(0x1b7, -1);
                this.honourRow8.Size = new Size(0x4b, 40);
                this.honourRow8.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.honourRow8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.banquetRow8.addControl(this.honourRow8);
                this.honourImageRow8.Image = (Image) GFXLibrary.com_32_honor_on_larger_dropshadow;
                this.honourImageRow8.Position = new Point(0x206, -2);
                this.banquetRow8.addControl(this.honourImageRow8);
                this.holdBandquetRow8.Position = new Point(0x235, 1);
                this.holdBandquetRow8.Size = new Size(0xa3, 0x26);
                this.holdBandquetRow8.Text.Text = SK.Text("BanquetScreen_Size_8", "Exquisite");
                this.holdBandquetRow8.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.holdBandquetRow8.TextYOffset = -1;
                this.holdBandquetRow8.Text.Color = ARGBColors.Black;
                this.holdBandquetRow8.Data = 7;
                this.holdBandquetRow8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.holdBanquetClick), "HoldBanquetPanel_exquisite");
                this.banquetRow8.addControl(this.holdBandquetRow8);
                this.holdBandquetRow8.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.holdBandquetRow8.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
                if (GameEngine.Instance.World.ThirdAgeWorld)
                {
                    this.xRow1.Text = this.xRow1.Text + "0";
                    this.xRow2.Text = this.xRow2.Text + "0";
                    this.xRow3.Text = this.xRow3.Text + "0";
                    this.xRow4.Text = this.xRow4.Text + "0";
                    this.xRow5.Text = this.xRow5.Text + "0";
                    this.xRow6.Text = this.xRow6.Text + "0";
                    this.xRow7.Text = this.xRow7.Text + "0";
                    this.xRow8.Text = this.xRow8.Text + "0";
                }
                else if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                {
                    this.xRow1.Text = "x2";
                    this.xRow2.Text = "x8";
                    this.xRow3.Text = "x18";
                    this.xRow4.Text = "x32";
                    this.xRow5.Text = "x70";
                    this.xRow6.Text = "x120";
                    this.xRow7.Text = "x196";
                    this.xRow8.Text = "x320";
                }
            }
            else
            {
                this.noResearchWindow.Size = new Size(0x2e3, 150);
                this.noResearchWindow.Position = new Point(0x7e, (base.Height - 150) / 2);
                this.mainBackgroundImage.addControl(this.noResearchWindow);
                this.noResearchWindow.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
                this.noResearchText.Text = SK.Text("BanquetScreen_Need_Research", "You don't currently have the required 'Banqueting' research level to hold a banquet. To begin holding banquets you must research 'Banqueting', 'Hunting' and place a Hunter's Hut in your village.");
                this.noResearchText.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                this.noResearchText.Position = new Point(20, 0);
                this.noResearchText.Size = new Size(this.noResearchWindow.Width - 40, this.noResearchWindow.Height);
                this.noResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.noResearchText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.noResearchWindow.addControl(this.noResearchText);
            }
            this.cardbar.Position = new Point(0, 0);
            base.addControl(this.cardbar);
            this.cardbar.init(3);
            this.updateLevels(true);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "HoldBanquetPanel2";
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
            if (this.cardbar.update())
            {
                this.updateLevels(true);
            }
            else
            {
                this.updateLevels(false);
            }
        }

        public void updateLevels(bool force)
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                VillageMap.TownHallLevels levels = new VillageMap.TownHallLevels();
                village.getTownHallLevels(levels);
                if (force || (this.oldLevels == null))
                {
                    this.oldLevels = levels;
                }
                else
                {
                    bool flag = false;
                    if (levels.venisonLevel != this.oldLevels.venisonLevel)
                    {
                        flag = true;
                    }
                    if (levels.furnitureLevel != this.oldLevels.furnitureLevel)
                    {
                        flag = true;
                    }
                    if (levels.metalwareLevel != this.oldLevels.metalwareLevel)
                    {
                        flag = true;
                    }
                    if (levels.clothesLevel != this.oldLevels.clothesLevel)
                    {
                        flag = true;
                    }
                    if (levels.wineLevel != this.oldLevels.wineLevel)
                    {
                        flag = true;
                    }
                    if (levels.saltLevel != this.oldLevels.saltLevel)
                    {
                        flag = true;
                    }
                    if (levels.spicesLevel != this.oldLevels.spicesLevel)
                    {
                        flag = true;
                    }
                    if (levels.silkLevel != this.oldLevels.silkLevel)
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        return;
                    }
                    this.oldLevels = levels;
                }
                this.resourceLevels[0] = (int) levels.venisonLevel;
                this.resourceLevels[1] = (int) levels.furnitureLevel;
                this.resourceLevels[2] = (int) levels.metalwareLevel;
                this.resourceLevels[3] = (int) levels.clothesLevel;
                this.resourceLevels[4] = (int) levels.wineLevel;
                this.resourceLevels[5] = (int) levels.saltLevel;
                this.resourceLevels[6] = (int) levels.spicesLevel;
                this.resourceLevels[7] = (int) levels.silkLevel;
                int num = GameEngine.Instance.World.UserResearchData.Research_Craftsmanship;
                this.banquetRow1.Visible = num >= 1;
                this.banquetRow2.Visible = num >= 2;
                this.banquetRow3.Visible = num >= 3;
                this.banquetRow4.Visible = num >= 4;
                this.banquetRow5.Visible = num >= 5;
                this.banquetRow6.Visible = num >= 6;
                this.banquetRow7.Visible = num >= 7;
                this.banquetRow8.Visible = num >= 8;
                for (int i = num; i < 8; i++)
                {
                    this.resourceLevels[i] = 0;
                }
                this.resourceLevelText1.Text = this.resourceLevels[0].ToString("N", nFI);
                this.resourceLevelText2.Text = this.resourceLevels[1].ToString("N", nFI);
                this.resourceLevelText3.Text = this.resourceLevels[2].ToString("N", nFI);
                this.resourceLevelText4.Text = this.resourceLevels[3].ToString("N", nFI);
                this.resourceLevelText5.Text = this.resourceLevels[4].ToString("N", nFI);
                this.resourceLevelText6.Text = this.resourceLevels[5].ToString("N", nFI);
                this.resourceLevelText7.Text = this.resourceLevels[6].ToString("N", nFI);
                this.resourceLevelText8.Text = this.resourceLevels[7].ToString("N", nFI);
                this.holdBandquetRow1.Enabled = false;
                this.holdBandquetRow2.Enabled = false;
                this.holdBandquetRow3.Enabled = false;
                this.holdBandquetRow4.Enabled = false;
                this.holdBandquetRow5.Enabled = false;
                this.holdBandquetRow6.Enabled = false;
                this.holdBandquetRow7.Enabled = false;
                this.holdBandquetRow8.Enabled = false;
                this.holdBandquetRow1.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.holdBandquetRow2.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.holdBandquetRow3.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.holdBandquetRow4.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.holdBandquetRow5.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.holdBandquetRow6.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.holdBandquetRow7.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.holdBandquetRow8.Text.Color = Color.FromArgb(0x7f, ARGBColors.Black);
                this.numResourcesRow1.Text = "0";
                this.honourRow1.Text = "0";
                this.numResourcesRow2.Text = "0";
                this.honourRow2.Text = "0";
                this.numResourcesRow3.Text = "0";
                this.honourRow3.Text = "0";
                this.numResourcesRow4.Text = "0";
                this.honourRow4.Text = "0";
                this.numResourcesRow5.Text = "0";
                this.honourRow5.Text = "0";
                this.numResourcesRow6.Text = "0";
                this.honourRow6.Text = "0";
                this.numResourcesRow7.Text = "0";
                this.honourRow7.Text = "0";
                this.numResourcesRow8.Text = "0";
                this.honourRow8.Text = "0";
                this.honourImageRow1.Alpha = 0.5f;
                this.honourImageRow2.Alpha = 0.5f;
                this.honourImageRow3.Alpha = 0.5f;
                this.honourImageRow4.Alpha = 0.5f;
                this.honourImageRow5.Alpha = 0.5f;
                this.honourImageRow6.Alpha = 0.5f;
                this.honourImageRow7.Alpha = 0.5f;
                this.honourImageRow8.Alpha = 0.5f;
                this.honourRow1.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.honourRow2.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.honourRow3.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.honourRow4.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.honourRow5.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.honourRow6.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.honourRow7.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.honourRow8.Color = Color.FromArgb(0x7f, 0xe0, 0xcb, 0x92);
                this.xRow1.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.xRow2.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.xRow3.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.xRow4.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.xRow5.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.xRow6.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.xRow7.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.xRow8.Color = Color.FromArgb(0x7f, 0x3e, 0xed, 0x2e);
                this.rightBracket1.Position = new Point(0x58, 2);
                this.rightBracket2.Position = new Point(0x58, 2);
                this.rightBracket3.Position = new Point(0x58, 2);
                this.rightBracket4.Position = new Point(0x58, 2);
                this.rightBracket5.Position = new Point(0x58, 2);
                this.rightBracket6.Position = new Point(0x58, 2);
                this.rightBracket7.Position = new Point(0x58, 2);
                this.rightBracket8.Position = new Point(0x58, 2);
                int[] numArray = new int[8];
                for (int j = 0; j < 8; j++)
                {
                    numArray[j] = this.resourceLevels[j];
                    for (int m = 0; m < 8; m++)
                    {
                        this.banquetLevels[m, j] = 0;
                        CustomSelfDrawPanel.CSDImage image = this.getTypePanel(j + 1, m);
                        if (image != null)
                        {
                            image.Visible = false;
                        }
                    }
                }
                for (int k = 0; k < 8; k++)
                {
                    int currentHonourValue = 0x77359400;
                    for (int n = 0; n < 8; n++)
                    {
                        if ((numArray[n] > 0) && (numArray[n] < currentHonourValue))
                        {
                            currentHonourValue = numArray[n];
                        }
                    }
                    if (currentHonourValue == 0x77359400)
                    {
                        break;
                    }
                    int banquetSize = 0;
                    int[] numArray2 = new int[8];
                    for (int num9 = 0; num9 < 8; num9++)
                    {
                        numArray2[num9] = 0;
                    }
                    for (int num10 = 0; num10 < 8; num10++)
                    {
                        if (numArray[num10] >= currentHonourValue)
                        {
                            banquetSize++;
                            numArray[num10] -= currentHonourValue;
                            numArray2[num10] = currentHonourValue;
                        }
                    }
                    int num11 = currentHonourValue;
                    currentHonourValue *= banquetSize;
                    for (int num12 = 0; num12 < 8; num12++)
                    {
                        this.banquetLevels[banquetSize - 1, num12] = numArray2[num12];
                    }
                    if (GameEngine.Instance.World.ThirdAgeWorld)
                    {
                        currentHonourValue *= 10;
                    }
                    else if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                    {
                        currentHonourValue *= 2;
                    }
                    switch (banquetSize)
                    {
                        case 1:
                            this.numResourcesRow1.Text = num11.ToString("N", nFI);
                            this.honourRow1.Text = CardTypes.getBanquetHonourValue(currentHonourValue, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.holdBandquetRow1.Enabled = true;
                            this.honourImageRow1.Alpha = 1f;
                            this.honourRow1.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.xRow1.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.rightBracket1.Position = new Point(120, 2);
                            this.leftBracket1.Alpha = 1f;
                            this.rightBracket1.Alpha = 1f;
                            this.holdBandquetRow1.Text.Color = ARGBColors.Black;
                            break;

                        case 2:
                            this.numResourcesRow2.Text = num11.ToString("N", nFI);
                            this.honourRow2.Text = CardTypes.getBanquetHonourValue(currentHonourValue * 2, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.honourImageRow2.Alpha = 1f;
                            this.xRow2.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.honourRow2.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.holdBandquetRow2.Enabled = true;
                            this.rightBracket2.Position = new Point(0x98, 2);
                            this.leftBracket2.Alpha = 1f;
                            this.rightBracket2.Alpha = 1f;
                            this.holdBandquetRow2.Text.Color = ARGBColors.Black;
                            break;

                        case 3:
                            this.numResourcesRow3.Text = num11.ToString("N", nFI);
                            this.honourRow3.Text = CardTypes.getBanquetHonourValue(currentHonourValue * 3, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.holdBandquetRow3.Enabled = true;
                            this.honourImageRow3.Alpha = 1f;
                            this.honourRow3.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.xRow3.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.rightBracket3.Position = new Point(0xb8, 2);
                            this.leftBracket3.Alpha = 1f;
                            this.rightBracket3.Alpha = 1f;
                            this.holdBandquetRow3.Text.Color = ARGBColors.Black;
                            break;

                        case 4:
                            this.numResourcesRow4.Text = num11.ToString("N", nFI);
                            this.honourRow4.Text = CardTypes.getBanquetHonourValue(currentHonourValue * 4, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.holdBandquetRow4.Enabled = true;
                            this.honourImageRow4.Alpha = 1f;
                            this.honourRow4.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.xRow4.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.rightBracket4.Position = new Point(0xd8, 2);
                            this.leftBracket4.Alpha = 1f;
                            this.rightBracket4.Alpha = 1f;
                            this.holdBandquetRow4.Text.Color = ARGBColors.Black;
                            break;

                        case 5:
                            this.numResourcesRow5.Text = num11.ToString("N", nFI);
                            this.honourRow5.Text = CardTypes.getBanquetHonourValue(currentHonourValue * 7, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.holdBandquetRow5.Enabled = true;
                            this.honourImageRow5.Alpha = 1f;
                            this.honourRow5.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.xRow5.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.rightBracket5.Position = new Point(0xf8, 2);
                            this.leftBracket5.Alpha = 1f;
                            this.rightBracket5.Alpha = 1f;
                            this.holdBandquetRow5.Text.Color = ARGBColors.Black;
                            break;

                        case 6:
                            this.numResourcesRow6.Text = num11.ToString("N", nFI);
                            this.honourRow6.Text = CardTypes.getBanquetHonourValue(currentHonourValue * 10, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.holdBandquetRow6.Enabled = true;
                            this.honourImageRow6.Alpha = 1f;
                            this.honourRow6.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.xRow6.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.rightBracket6.Position = new Point(280, 2);
                            this.leftBracket6.Alpha = 1f;
                            this.rightBracket6.Alpha = 1f;
                            this.holdBandquetRow6.Text.Color = ARGBColors.Black;
                            break;

                        case 7:
                            this.numResourcesRow7.Text = num11.ToString("N", nFI);
                            this.honourRow7.Text = CardTypes.getBanquetHonourValue(currentHonourValue * 14, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.holdBandquetRow7.Enabled = true;
                            this.honourImageRow7.Alpha = 1f;
                            this.honourRow7.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.xRow7.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.rightBracket7.Position = new Point(0x138, 2);
                            this.leftBracket7.Alpha = 1f;
                            this.rightBracket7.Alpha = 1f;
                            this.holdBandquetRow7.Text.Color = ARGBColors.Black;
                            break;

                        case 8:
                            this.numResourcesRow8.Text = num11.ToString("N", nFI);
                            this.honourRow8.Text = CardTypes.getBanquetHonourValue(currentHonourValue * 20, GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.UserCardData).ToString("N", nFI);
                            this.holdBandquetRow8.Enabled = true;
                            this.honourImageRow8.Alpha = 1f;
                            this.honourRow8.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
                            this.xRow8.Color = Color.FromArgb(0x3e, 0xed, 0x2e);
                            this.rightBracket8.Position = new Point(0x158, 2);
                            this.leftBracket8.Alpha = 1f;
                            this.rightBracket8.Alpha = 1f;
                            this.holdBandquetRow8.Text.Color = ARGBColors.Black;
                            break;
                    }
                    int num13 = 0;
                    int num14 = 0;
                    for (int num15 = 0; num15 < banquetSize; num15++)
                    {
                        for (int num16 = num14; num16 < 8; num16++)
                        {
                            if (numArray2[num16] > 0)
                            {
                                CustomSelfDrawPanel.CSDImage image2 = this.getTypePanel(banquetSize, num13++);
                                if (image2 != null)
                                {
                                    image2.Image = (Image) this.getImage(num16);
                                    image2.Visible = true;
                                }
                                num14 = num16 + 1;
                                break;
                            }
                        }
                    }
                }
            }
            base.Invalidate();
        }
    }
}

