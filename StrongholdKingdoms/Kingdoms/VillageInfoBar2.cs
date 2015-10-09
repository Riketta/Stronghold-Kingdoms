namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;
    using System.Globalization;

    public class VillageInfoBar2 : CustomSelfDrawPanel.CSDControl
    {
        private CustomSelfDrawPanel.CSDImage imgBed = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgFlag = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgFood = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgGold = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgPeople = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgStone = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgWood = new CustomSelfDrawPanel.CSDImage();
        private bool lastGranary = true;
        private bool lastStockpile = true;
        private bool lastViewOnly;
        private CustomSelfDrawPanel.CSDLabel lblFoodLevel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblFoodName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblHeading = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblPeople = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblStoneLevel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblWoodLevel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblWoodName = new CustomSelfDrawPanel.CSDLabel();

        public void hide()
        {
            base.Visible = false;
            base.invalidate();
        }

        public void init()
        {
            this.clearControls();
            this.lblWoodName.Text = SK.Text("VillageInfoBar_No_Stockpile", "No Stockpile");
            this.lblWoodName.Position = new Point(3, 0);
            this.lblWoodName.Size = new Size(250, 0x1d);
            this.lblWoodName.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblWoodName.Color = ARGBColors.Yellow;
            this.lblWoodName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblWoodName.CustomTooltipID = 0x8e;
            this.lblWoodName.Visible = false;
            base.addControl(this.lblWoodName);
            this.lblWoodLevel.Text = "";
            this.lblWoodLevel.Position = new Point(0x2c, 3);
            this.lblWoodLevel.Size = new Size(0x4b, 0x1d);
            this.lblWoodLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblWoodLevel.Color = ARGBColors.Yellow;
            this.lblWoodLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblWoodLevel.CustomTooltipID = 0x8e;
            base.addControl(this.lblWoodLevel);
            this.lblStoneLevel.Text = "";
            this.lblStoneLevel.Position = new Point(0xa5, 3);
            this.lblStoneLevel.Size = new Size(0x4b, 0x1d);
            this.lblStoneLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblStoneLevel.Color = ARGBColors.Yellow;
            this.lblStoneLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblStoneLevel.CustomTooltipID = 0x8f;
            base.addControl(this.lblStoneLevel);
            this.lblFoodLevel.Text = "";
            this.lblFoodLevel.Position = new Point(0x11e, 3);
            this.lblFoodLevel.Size = new Size(0x4e, 0x1d);
            this.lblFoodLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblFoodLevel.Color = ARGBColors.Yellow;
            this.lblFoodLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblFoodLevel.CustomTooltipID = 0x90;
            base.addControl(this.lblFoodLevel);
            this.lblFoodName.Text = SK.Text("VillageInfoBar_No_Granary", "No Granary");
            this.lblFoodName.Position = new Point(0x10d, 3);
            this.lblFoodName.Size = new Size(0xce, 0x1d);
            this.lblFoodName.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblFoodName.Color = ARGBColors.Yellow;
            this.lblFoodName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblFoodName.CustomTooltipID = 0x8e;
            this.lblFoodName.Visible = false;
            base.addControl(this.lblFoodName);
            this.lblPeople.Text = "";
            this.lblPeople.Position = new Point(0x1a2, 3);
            this.lblPeople.Size = new Size(0x33, 0x1d);
            this.lblPeople.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblPeople.Color = ARGBColors.Yellow;
            this.lblPeople.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblPeople.CustomTooltipID = 0x8d;
            base.addControl(this.lblPeople);
            this.lblPeasants.Text = "";
            this.lblPeasants.Position = new Point(0x1f7, 3);
            this.lblPeasants.Size = new Size(0x1b, 0x1d);
            this.lblPeasants.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblPeasants.Color = ARGBColors.Yellow;
            this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblPeasants.CustomTooltipID = 0x8d;
            base.addControl(this.lblPeasants);
            this.lblHeading.Text = "";
            this.lblHeading.Position = new Point(0, 2);
            this.lblHeading.Size = new Size(500, 0x1d);
            this.lblHeading.Font = FontManager.GetFont("Microsoft Sans Serif", 18f);
            this.lblHeading.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
            this.lblHeading.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lblHeading.Visible = false;
            base.addControl(this.lblHeading);
            this.imgWood.Image = (Image) GFXLibrary.com_32_wood;
            this.imgWood.Position = new Point(7, 0);
            this.imgWood.CustomTooltipID = 0x8e;
            base.addControl(this.imgWood);
            this.imgStone.Image = (Image) GFXLibrary.com_32_stone;
            this.imgStone.Position = new Point(0x80, 0);
            this.imgStone.CustomTooltipID = 0x8f;
            base.addControl(this.imgStone);
            this.imgFood.Image = (Image) GFXLibrary.com_32_food;
            this.imgFood.Position = new Point(0xf9, 0);
            this.imgFood.CustomTooltipID = 0x90;
            base.addControl(this.imgFood);
            this.imgBed.Image = (Image) GFXLibrary.population_bed;
            this.imgBed.Position = new Point(370, 0);
            this.imgBed.CustomTooltipID = 0x8d;
            base.addControl(this.imgBed);
            this.imgPeople.Image = (Image) GFXLibrary.population_head;
            this.imgPeople.Position = new Point(0x1d5, 0);
            this.imgPeople.CustomTooltipID = 0x8d;
            base.addControl(this.imgPeople);
            this.imgGold.Image = (Image) GFXLibrary.com_32_money;
            this.imgGold.Position = new Point(7, 0);
            this.imgGold.CustomTooltipID = 0x91;
            base.addControl(this.imgGold);
            this.imgFlag.Image = (Image) GFXLibrary.flag_blue_icon;
            this.imgFlag.Position = new Point(0x80, 8);
            this.imgFlag.CustomTooltipID = 0x92;
            base.addControl(this.imgFlag);
        }

        public bool isVisible()
        {
            return base.Visible;
        }

        public void removeHeading()
        {
            this.lblHeading.Visible = false;
        }

        public void setDisplayedLevels(int woodLevel, int clayLevel, int stoneLevel, int foodLevel, bool gotStockpile, bool gotGranary, int totalPeople, int housingCapacity, int spareWorkers, bool viewOnly, int capitalGold, int villageID, int numFlags)
        {
            if (!this.lblHeading.Visible)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                if (GameEngine.Instance.World.isCapital(villageID))
                {
                    this.lblWoodLevel.Visible = true;
                    this.lblStoneLevel.Visible = true;
                    this.lblWoodName.Visible = false;
                    this.lblFoodLevel.Visible = false;
                    this.lblFoodName.Visible = false;
                    this.imgFood.Visible = false;
                    this.imgWood.Visible = false;
                    this.imgStone.Visible = false;
                    this.lblPeasants.Visible = false;
                    this.lblPeople.Visible = false;
                    this.imgPeople.Visible = false;
                    this.imgBed.Visible = false;
                    this.imgGold.Visible = true;
                    this.imgFlag.Visible = true;
                    this.lblWoodLevel.TextDiffOnly = capitalGold.ToString("N", nFI);
                    this.lblStoneLevel.TextDiffOnly = numFlags.ToString("N", nFI);
                    this.lblWoodLevel.CustomTooltipID = 0x91;
                    this.lblStoneLevel.CustomTooltipID = 0x92;
                }
                else
                {
                    this.imgGold.Visible = false;
                    this.lblPeasants.Visible = true;
                    this.lblPeople.Visible = true;
                    this.lblWoodLevel.Visible = true;
                    this.lblStoneLevel.Visible = true;
                    this.lblWoodName.Visible = false;
                    this.lblFoodLevel.Visible = true;
                    this.imgFood.Visible = true;
                    this.imgWood.Visible = true;
                    this.imgStone.Visible = true;
                    this.imgPeople.Visible = true;
                    this.imgBed.Visible = true;
                    this.imgFlag.Visible = false;
                    this.lblWoodLevel.CustomTooltipID = 0x8e;
                    this.lblStoneLevel.CustomTooltipID = 0x8f;
                    if (!viewOnly)
                    {
                        if (this.lastViewOnly)
                        {
                            this.lastStockpile = !gotStockpile;
                            this.lastGranary = !gotGranary;
                            this.lastViewOnly = false;
                        }
                        this.lblWoodLevel.TextDiffOnly = woodLevel.ToString("N", nFI);
                        this.lblStoneLevel.TextDiffOnly = stoneLevel.ToString("N", nFI);
                        this.lblFoodLevel.TextDiffOnly = foodLevel.ToString("N", nFI);
                        this.lastStockpile = gotStockpile;
                        if (gotStockpile)
                        {
                            this.lblWoodLevel.Visible = true;
                            this.lblStoneLevel.Visible = true;
                            this.imgWood.Visible = true;
                            this.imgStone.Visible = true;
                            this.lblWoodName.Visible = false;
                        }
                        else
                        {
                            this.lblWoodLevel.Visible = false;
                            this.lblStoneLevel.Visible = false;
                            this.imgWood.Visible = false;
                            this.imgStone.Visible = false;
                            this.lblWoodName.Visible = true;
                        }
                        this.lastGranary = gotGranary;
                        if (gotGranary)
                        {
                            this.lblFoodLevel.Visible = true;
                            this.lblFoodName.Visible = false;
                            this.imgFood.Visible = true;
                        }
                        else
                        {
                            this.lblFoodLevel.Visible = false;
                            this.lblFoodName.Visible = true;
                            this.imgFood.Visible = false;
                        }
                    }
                    else
                    {
                        this.lastViewOnly = true;
                        this.lblWoodLevel.Visible = false;
                        this.lblStoneLevel.Visible = false;
                        this.lblWoodName.Visible = false;
                        this.lblFoodLevel.Visible = false;
                        this.lblFoodName.Visible = false;
                        this.imgFood.Visible = false;
                        this.imgWood.Visible = false;
                        this.imgStone.Visible = false;
                    }
                    this.lblPeople.TextDiffOnly = totalPeople.ToString() + "/" + housingCapacity.ToString();
                    this.lblPeasants.TextDiffOnly = spareWorkers.ToString();
                }
            }
        }

        public void setHeading(string text)
        {
            this.lblWoodName.Visible = false;
            this.lblFoodLevel.Visible = false;
            this.lblFoodName.Visible = false;
            this.imgFood.Visible = false;
            this.imgWood.Visible = false;
            this.imgStone.Visible = false;
            this.lblPeasants.Visible = false;
            this.lblPeople.Visible = false;
            this.imgPeople.Visible = false;
            this.imgBed.Visible = false;
            this.lblStoneLevel.Visible = false;
            this.lblWoodLevel.Visible = false;
            this.imgGold.Visible = false;
            this.lblFoodName.Visible = false;
            this.imgFlag.Visible = false;
            this.lblHeading.Text = text;
            this.lblHeading.Visible = true;
        }

        public void show()
        {
            base.Visible = true;
            base.invalidate();
        }
    }
}

