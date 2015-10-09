namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Drawing;

    public class VillageMapBuilding
    {
        public SpriteWrapper animSprite;
        public int[] applesPileOrder = new int[] { 2, 3, 4 };
        public SpriteWrapper baseSprite;
        public int[] breadPileOrder = new int[] { 13, 14, 0x11, 0x12 };
        public bool buildingActive;
        public long buildingID;
        public Point buildingLocation;
        public int buildingType;
        public double calcRate;
        public int[] capitalResourceLevels;
        public int[] cheesePileOrder = new int[] { 15, 0x10, 0x13, 20 };
        public bool complete;
        public bool completeRequestSent;
        public DateTime completionTime;
        public int data1;
        public int data2;
        public DateTime deletionTime;
        public SpriteWrapper extraAnimSprite1;
        public SpriteWrapper extraAnimSprite2;
        public int[] fishPileOrder = new int[] { 10, 11, 12 };
        public int[] goods16Levels = new int[] { 0, 0, 0x10, 0x60, 0x1a0, 0x4c0, 0x1140, 0x3080, 0xad80, 0x1e600, 0x6c800, 0x12fd00, 0x43d100, 0xbde300, 0x76adf00 };
        public int[] goods48Levels = new int[] { 0, 0, 0x30, 0x120, 0x4e0, 0xe40, 0x33c0, 0x9180, 0x20880, 0x5b200, 0x145800, 0x38f700, 0xcb7300, 0x7f28100 };
        public int[] goodsDividers = new int[] { 
            1, 1, 5, 20, 50, 200, 500, 0x7d0, 0x1388, 0x4e20, 0xc350, 0x30d40, 0x7a120, 0x30d40, 0x4c4b40, 0x1e8480, 
            0x4c4b40, 0x1312d00, 0x2faf080
         };
        public bool gotEmployee;
        public bool goTransparent;
        public VillageMapBuildingGranaryExtension granaryExtension;
        public bool highlighted;
        public VillageMapBuildingInnExtension innExtension;
        public int[] ironPileOrder = new int[] { 15, 11, 13, 14, 4, 7, 8, 10, 12, 0, 1, 2, 3, 5, 6, 9 };
        public double journeyTime;
        public double journeyTime2;
        public DateTime lastCalcTime;
        public double lastDataLevel;
        public bool lastOpenState;
        public bool localComplete = true;
        public int[] meatPileOrder = new int[] { 9, 8, 1, 0 };
        public bool open;
        private bool[] pilesUsed = new bool[0x10];
        public int[] pitchPileOrder = new int[] { 9, 5, 8, 12, 2, 4, 7, 11, 14, 0, 1, 3, 6, 10, 13, 15 };
        public int productionGFXCounter;
        public float productionGFXVelocity = 0.5f;
        public SpriteWrapper productionSprite;
        public int productionState;
        public double productionTime;
        public int randState = -1;
        public VillageMapPerson secondaryWorker;
        public double serverCalcRate;
        public bool serverDeleting;
        public double serverJourneyTime;
        public SpriteWrapper shadowSprite;
        public bool showFullConstructionText;
        public VillageMapBuildingStockpileExtension stockpileExtension;
        public int[] stonePileOrder = new int[] { 0, 1, 2, 4, 3, 7, 11, 8, 5, 6, 10, 13, 15, 14, 12, 9 };
        public Point storageLocation;
        public SpriteWrapper symbolSprite;
        public double tripCalcRate;
        public int[] vegPileOrder = new int[] { 5, 6, 7 };
        public bool weaponContinuance;
        public int[] woodPileOrder = new int[] { 6, 3, 7, 10, 1, 4, 8, 11, 13, 0, 2, 5, 9, 12, 14, 15 };
        public VillageMapPerson worker;
        public bool workerNeedsReInitializing;

        public void createFromReturnData(VillageBuildingReturnData serverBuild)
        {
            this.completeRequestSent = false;
            this.buildingID = serverBuild.buildingID;
            this.buildingLocation = (Point) serverBuild.buildingLocation;
            this.buildingType = serverBuild.buildingType;
            this.serverCalcRate = this.calcRate = serverBuild.calcRate;
            this.serverJourneyTime = serverBuild.journeyTime;
            this.completionTime = serverBuild.completionTime;
            this.lastCalcTime = serverBuild.lastCalcTime;
            this.lastDataLevel = serverBuild.lastDataLevel;
            this.gotEmployee = serverBuild.gotEmployee;
            this.buildingActive = serverBuild.active;
            this.localComplete = true;
            this.deletionTime = serverBuild.deletionTime;
            this.serverDeleting = serverBuild.deleting;
            this.capitalResourceLevels = serverBuild.capitalResourceLevels;
            if (this.baseSprite != null)
            {
                this.baseSprite.clearText();
                this.baseSprite.clearSecondText();
            }
        }

        public int getProductionSpriteNo(int buildingType)
        {
            switch (buildingType)
            {
                case 6:
                    return 0x7a;

                case 7:
                    return 0x75;

                case 8:
                    return 0x6b;

                case 9:
                    return 0x71;

                case 12:
                    return 0x5f;

                case 13:
                    return 0x60;

                case 14:
                    return 0x63;

                case 15:
                    return 0x77;

                case 0x10:
                    return 0x6c;

                case 0x11:
                    return 0x65;

                case 0x12:
                    return 0x67;

                case 0x13:
                    return 0x66;

                case 0x15:
                    return 0x69;

                case 0x16:
                    return 120;

                case 0x17:
                    return 0x72;

                case 0x18:
                    return 0x74;

                case 0x19:
                    return 0x73;

                case 0x1a:
                    return 0x6d;

                case 0x1c:
                    return 0x70;

                case 0x1d:
                    return 0x62;

                case 30:
                    return 0x76;

                case 0x1f:
                    return 0x61;

                case 0x20:
                    return 100;

                case 0x21:
                    return 0x79;
            }
            return 0x7b;
        }

        public void initProductionGFX()
        {
        }

        public void initStorageBuilding(GraphicsMgr gfx, VillageMap vm)
        {
            if (this.buildingType == 2)
            {
                this.updateStockpile(gfx, vm);
            }
            if (this.buildingType == 3)
            {
                this.updateGranary(gfx, vm);
            }
            if (this.buildingType == 0x23)
            {
                this.updateInn(gfx, vm);
            }
        }

        public bool isComplete()
        {
            return (this.complete && this.localComplete);
        }

        public bool isDeleting()
        {
            return this.serverDeleting;
        }

        public bool updateConstructionGFX(double localBaseTime, DateTime serverBaseTime, bool initialUpdate, VillageMap vm)
        {
            if (this.baseSprite != null)
            {
                if (this.serverDeleting)
                {
                    double num = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                    num -= 1.5;
                    DateTime time = serverBaseTime.AddSeconds(num);
                    if (!this.complete && (time.CompareTo(this.completionTime) >= 0))
                    {
                        this.complete = true;
                    }
                    TimeSpan span = (TimeSpan) (this.deletionTime - time);
                    int secsLeft = (int) (span.TotalSeconds - 0.5);
                    if (span.TotalDays > 10.0)
                    {
                        secsLeft = 0x98967f;
                    }
                    if ((secsLeft > 0) && (secsLeft < 0x989680))
                    {
                        if (!vm.ViewOnly)
                        {
                            string text = VillageMap.createBuildTimeString(secsLeft);
                            this.baseSprite.attachText(text, new Point(0, -50), ARGBColors.White, true, true);
                        }
                    }
                    else
                    {
                        this.baseSprite.clearText();
                        this.baseSprite.clearSecondText();
                        if (secsLeft <= 0)
                        {
                            return true;
                        }
                    }
                    this.baseSprite.ColorToUse = Color.FromArgb(0xff, 0xff, 0x80, 0x80);
                    if (this.animSprite != null)
                    {
                        this.animSprite.ColorToUse = this.baseSprite.ColorToUse;
                    }
                    if (this.extraAnimSprite1 != null)
                    {
                        this.extraAnimSprite1.ColorToUse = this.baseSprite.ColorToUse;
                    }
                    if (this.extraAnimSprite2 != null)
                    {
                        this.extraAnimSprite2.ColorToUse = this.baseSprite.ColorToUse;
                    }
                    return false;
                }
                if (this.complete)
                {
                    return false;
                }
                bool flag = false;
                double num3 = (DXTimer.GetCurrentMilliseconds() - localBaseTime) / 1000.0;
                if (initialUpdate)
                {
                    num3 = 0.0;
                }
                num3 -= 3.0;
                DateTime time2 = serverBaseTime.AddSeconds(num3);
                if (time2.CompareTo(this.completionTime) < 0)
                {
                    flag = true;
                }
                if (this.buildingType == 0)
                {
                    flag = false;
                }
                if (flag)
                {
                    if (!this.highlighted)
                    {
                        this.baseSprite.ColorToUse = Color.FromArgb(0x80, 0x80, 0x80, 0x80);
                        if (this.animSprite != null)
                        {
                            this.animSprite.ColorToUse = Color.FromArgb(0x80, 0x80, 0x80, 0x80);
                        }
                        if (this.extraAnimSprite1 != null)
                        {
                            this.extraAnimSprite1.ColorToUse = Color.FromArgb(0x80, 0x80, 0x80, 0x80);
                        }
                        if (this.extraAnimSprite2 != null)
                        {
                            this.extraAnimSprite2.ColorToUse = Color.FromArgb(0x80, 0x80, 0x80, 0x80);
                        }
                    }
                    TimeSpan span2 = (TimeSpan) (this.completionTime - time2);
                    int num4 = (int) (span2.TotalSeconds - 0.5);
                    if ((num4 > 0) && (num4 < 0x989680))
                    {
                        int num6;
                        int num5 = num4;
                        num4 = vm.updateConstructionDisplayTime(num4, this.completionTime, out num6);
                        Color col = (num6 == 1) ? ARGBColors.White : ARGBColors.WhiteSmoke;
                        if (!vm.ViewOnly)
                        {
                            string str2 = VillageMap.createBuildTimeString(num4);
                            if ((num4 != num5) && this.showFullConstructionText)
                            {
                                this.showFullConstructionText = false;
                                string str3 = str2;
                                str2 = str3 + Environment.NewLine + "(" + VillageMap.createBuildTimeString(num5) + ")";
                            }
                            this.baseSprite.attachText(str2, new Point(0, -40), ARGBColors.White, true, true);
                            if (num6 > 0)
                            {
                                this.baseSprite.attachSecondText(num6.ToString(), new Point(0, -55), col, true, true);
                            }
                            else
                            {
                                this.baseSprite.clearSecondText();
                            }
                        }
                    }
                    else
                    {
                        this.baseSprite.clearText();
                        this.baseSprite.clearSecondText();
                    }
                }
                else
                {
                    Color white = ARGBColors.White;
                    this.baseSprite.ColorToUse = white;
                    if (this.animSprite != null)
                    {
                        this.animSprite.ColorToUse = white;
                    }
                    if (this.extraAnimSprite1 != null)
                    {
                        this.extraAnimSprite1.ColorToUse = white;
                    }
                    if (this.extraAnimSprite2 != null)
                    {
                        this.extraAnimSprite2.ColorToUse = white;
                    }
                    this.complete = true;
                    if (!initialUpdate)
                    {
                        this.localComplete = false;
                        return true;
                    }
                    this.baseSprite.clearText();
                    this.baseSprite.clearSecondText();
                }
            }
            return false;
        }

        public void updateGranary(GraphicsMgr gfx, VillageMap vm)
        {
            if (this.baseSprite != null)
            {
                if (this.granaryExtension == null)
                {
                    this.granaryExtension = new VillageMapBuildingGranaryExtension();
                    for (int j = 0; j < 0x15; j++)
                    {
                        this.granaryExtension.cell[j] = new SpriteWrapper();
                        this.granaryExtension.cell[j].Visible = false;
                        this.granaryExtension.cell[j].PosX = 5 + VillageMapBuildingGranaryExtension.granaryLayout[j * 2];
                        this.granaryExtension.cell[j].PosY = -33 + VillageMapBuildingGranaryExtension.granaryLayout[(j * 2) + 1];
                        this.baseSprite.AddChild(this.granaryExtension.cell[j]);
                        this.granaryExtension.showGood(gfx, j, -1, 0);
                    }
                }
                for (int i = 0; i < 0x15; i++)
                {
                    this.granaryExtension.showGood(gfx, i, -1, 0);
                }
                VillageMap.GranaryLevels levels = new VillageMap.GranaryLevels();
                if (vm.getGranaryLevels(levels))
                {
                    if (vm.granaryOpenCount == 0)
                    {
                        this.open = false;
                    }
                    double num3 = ((((levels.applesLevel + levels.breadLevel) + levels.cheeseLevel) + levels.fishLevel) + levels.meatLevel) + levels.vegLevel;
                    if (num3 > 0.0)
                    {
                        if (vm.granaryOpenCount > 0)
                        {
                            this.open = true;
                        }
                        else
                        {
                            return;
                        }
                        int index = 0;
                        if (levels.meatLevel > 416.0)
                        {
                            index = 4;
                        }
                        else if (levels.meatLevel > 96.0)
                        {
                            index = 3;
                        }
                        else if (levels.meatLevel > 16.0)
                        {
                            index = 2;
                        }
                        else if (levels.meatLevel > 0.0)
                        {
                            index = 1;
                        }
                        else
                        {
                            index = 0;
                        }
                        int num5 = 0;
                        if (levels.vegLevel > 96.0)
                        {
                            num5 = 3;
                        }
                        else if (levels.vegLevel > 16.0)
                        {
                            num5 = 2;
                        }
                        else if (levels.vegLevel > 0.0)
                        {
                            num5 = 1;
                        }
                        else
                        {
                            num5 = 0;
                        }
                        int num6 = 0;
                        if (levels.cheeseLevel > 416.0)
                        {
                            num6 = 4;
                        }
                        else if (levels.cheeseLevel > 96.0)
                        {
                            num6 = 3;
                        }
                        else if (levels.cheeseLevel > 16.0)
                        {
                            num6 = 2;
                        }
                        else if (levels.cheeseLevel > 0.0)
                        {
                            num6 = 1;
                        }
                        else
                        {
                            num6 = 0;
                        }
                        int num7 = 0;
                        if (levels.applesLevel > 96.0)
                        {
                            num7 = 3;
                        }
                        else if (levels.applesLevel > 16.0)
                        {
                            num7 = 2;
                        }
                        else if (levels.applesLevel > 0.0)
                        {
                            num7 = 1;
                        }
                        else
                        {
                            num7 = 0;
                        }
                        int num8 = 0;
                        if (levels.fishLevel > 96.0)
                        {
                            num8 = 3;
                        }
                        else if (levels.fishLevel > 16.0)
                        {
                            num8 = 2;
                        }
                        else if (levels.fishLevel > 0.0)
                        {
                            num8 = 1;
                        }
                        else
                        {
                            num8 = 0;
                        }
                        int num9 = 0;
                        if (levels.breadLevel > 832.0)
                        {
                            num9 = 4;
                        }
                        else if (levels.breadLevel > 192.0)
                        {
                            num9 = 3;
                        }
                        else if (levels.breadLevel > 32.0)
                        {
                            num9 = 2;
                        }
                        else if (levels.breadLevel > 0.0)
                        {
                            num9 = 1;
                        }
                        else
                        {
                            num9 = 0;
                        }
                        int cellID = 0;
                        for (int k = 0; k < index; k++)
                        {
                            cellID = this.meatPileOrder[k];
                            if (k != (index - 1))
                            {
                                this.granaryExtension.showGood(gfx, cellID, 0x10, 0x10);
                            }
                            else
                            {
                                int num12 = (((int) levels.meatLevel) - this.goods16Levels[index]) / this.goodsDividers[index];
                                this.granaryExtension.showGood(gfx, cellID, 0x10, Math.Min(num12, 0x10));
                            }
                        }
                        for (int m = 0; m < num5; m++)
                        {
                            cellID = this.vegPileOrder[m];
                            if (m != (num5 - 1))
                            {
                                this.granaryExtension.showGood(gfx, cellID, 15, 0x10);
                            }
                            else
                            {
                                int num14 = (((int) levels.vegLevel) - this.goods16Levels[num5]) / this.goodsDividers[num5];
                                this.granaryExtension.showGood(gfx, cellID, 15, Math.Min(num14, 0x10));
                            }
                        }
                        for (int n = 0; n < num6; n++)
                        {
                            cellID = this.cheesePileOrder[n];
                            if (n != (num6 - 1))
                            {
                                this.granaryExtension.showGood(gfx, cellID, 0x11, 0x10);
                            }
                            else
                            {
                                int num16 = (((int) levels.cheeseLevel) - this.goods16Levels[num6]) / this.goodsDividers[num6];
                                this.granaryExtension.showGood(gfx, cellID, 0x11, Math.Min(num16, 0x10));
                            }
                        }
                        for (int num17 = 0; num17 < num7; num17++)
                        {
                            cellID = this.applesPileOrder[num17];
                            if (num17 != (num7 - 1))
                            {
                                this.granaryExtension.showGood(gfx, cellID, 13, 0x10);
                            }
                            else
                            {
                                int num18 = (((int) levels.applesLevel) - this.goods16Levels[num7]) / this.goodsDividers[num7];
                                this.granaryExtension.showGood(gfx, cellID, 13, Math.Min(num18, 0x10));
                            }
                        }
                        for (int num19 = 0; num19 < num8; num19++)
                        {
                            cellID = this.fishPileOrder[num19];
                            if (num19 != (num8 - 1))
                            {
                                this.granaryExtension.showGood(gfx, cellID, 0x12, 0x10);
                            }
                            else
                            {
                                int num20 = (((int) levels.fishLevel) - this.goods16Levels[num8]) / this.goodsDividers[num8];
                                this.granaryExtension.showGood(gfx, cellID, 0x12, Math.Min(num20, 0x10));
                            }
                        }
                        for (int num21 = 0; num21 < num9; num21++)
                        {
                            cellID = this.breadPileOrder[num21];
                            if (num21 != (num9 - 1))
                            {
                                this.granaryExtension.showGood(gfx, cellID, 14, 0x20);
                            }
                            else
                            {
                                int num22 = (((int) levels.breadLevel) - (this.goods16Levels[num9] * 2)) / this.goodsDividers[num9];
                                this.granaryExtension.showGood(gfx, cellID, 14, Math.Min(num22, 0x20));
                            }
                        }
                    }
                }
            }
        }

        public void updateInn(GraphicsMgr gfx, VillageMap vm)
        {
            if (this.baseSprite != null)
            {
                if (this.innExtension == null)
                {
                    this.innExtension = new VillageMapBuildingInnExtension();
                    for (int j = 0; j < 3; j++)
                    {
                        this.innExtension.cell[j] = new SpriteWrapper();
                        this.innExtension.cell[j].Visible = false;
                        this.innExtension.cell[j].PosX = -80 + VillageMapBuildingInnExtension.innLayout[j * 2];
                        this.innExtension.cell[j].PosY = -44 + VillageMapBuildingInnExtension.innLayout[(j * 2) + 1];
                        this.baseSprite.AddChild(this.innExtension.cell[j]);
                        this.innExtension.showGood(gfx, j, -1, 0);
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    this.innExtension.showGood(gfx, i, -1, 0);
                }
                VillageMap.InnLevels levels = new VillageMap.InnLevels();
                if (vm.getInnLevels(levels))
                {
                    if (levels.aleLevel == 0.0)
                    {
                        if (vm.m_effectiveAleRationsLevel > 0.0)
                        {
                            this.open = true;
                        }
                        else
                        {
                            this.open = false;
                        }
                    }
                    else
                    {
                        this.open = true;
                        int index = 0;
                        if (levels.aleLevel > 416.0)
                        {
                            index = 4;
                        }
                        else if (levels.aleLevel > 96.0)
                        {
                            index = 3;
                        }
                        else if (levels.aleLevel > 16.0)
                        {
                            index = 2;
                        }
                        else if (levels.aleLevel > 0.0)
                        {
                            index = 1;
                        }
                        else
                        {
                            index = 0;
                        }
                        if (index > 3)
                        {
                            index = 3;
                        }
                        for (int k = 0; k < index; k++)
                        {
                            if (k != (index - 1))
                            {
                                this.innExtension.showGood(gfx, k, 12, 0x10);
                            }
                            else
                            {
                                int num6 = (((int) levels.aleLevel) - this.goods16Levels[index]) / this.goodsDividers[index];
                                this.innExtension.showGood(gfx, k, 12, Math.Min(num6, 0x10));
                            }
                        }
                    }
                }
            }
        }

        public void updateProductionGFX(bool reset)
        {
            if (!Program.mySettings.ShowProductionInfo && this.productionSprite.Visible)
            {
                this.productionSprite.Visible = false;
            }
            else if (reset)
            {
                this.productionSprite.SpriteNo = this.getProductionSpriteNo(this.buildingType);
                this.productionSprite.Visible = true;
                this.productionSprite.PosX = 0f;
                this.productionSprite.PosY = -50f;
                this.productionSprite.ColorToUse = Color.FromArgb(0, 0xff, 0xff, 0xff);
                this.productionGFXCounter = 0;
                double payloadSize = GameEngine.Instance.LocalWorldData.getPayloadSize(this.buildingType);
                double num2 = CardTypes.adjustPayloadSize(GameEngine.Instance.World.UserCardData, payloadSize, this.buildingType) - payloadSize;
                if (num2 > 0.99)
                {
                    this.productionSprite.attachText(payloadSize.ToString(), new Point(-15, 15), Color.FromArgb(0, 0xff, 0xff, 0xff), true, true);
                    this.productionSprite.attachSecondText("(+" + num2.ToString() + ")", new Point(10, 15), Color.FromArgb(0, 150, 0xff, 180), true, true);
                }
                else
                {
                    this.productionSprite.attachText(payloadSize.ToString(), new Point(0, 15), ARGBColors.White, true, true);
                }
            }
            else
            {
                if (this.productionGFXCounter <= 50)
                {
                    this.productionSprite.PosY -= 0.5f;
                    this.productionSprite.changeAlpha(80);
                    this.productionSprite.changeTextAlpha(80);
                    this.productionSprite.changeSecondTextAlpha(80);
                }
                else
                {
                    this.productionSprite.PosY -= 2f;
                    this.productionSprite.changeAlpha(-10);
                    this.productionSprite.changeTextAlpha(-10);
                    this.productionSprite.changeSecondTextAlpha(-10);
                }
                this.productionGFXCounter++;
            }
        }

        public void updateStockpile(GraphicsMgr gfx, VillageMap vm)
        {
            if (this.baseSprite != null)
            {
                if (this.stockpileExtension == null)
                {
                    this.stockpileExtension = new VillageMapBuildingStockpileExtension();
                    for (int j = 0; j < 0x10; j++)
                    {
                        this.stockpileExtension.cell[j] = new SpriteWrapper();
                        this.stockpileExtension.cell[j].Visible = false;
                        this.stockpileExtension.cell[j].PosX = -96 + VillageMapBuildingStockpileExtension.stockpileLayout[j * 2];
                        this.stockpileExtension.cell[j].PosY = -43 + VillageMapBuildingStockpileExtension.stockpileLayout[(j * 2) + 1];
                        this.baseSprite.AddChild(this.stockpileExtension.cell[j]);
                        this.stockpileExtension.showGood(gfx, j, -1, 0);
                    }
                }
                for (int i = 0; i < 0x10; i++)
                {
                    this.stockpileExtension.showGood(gfx, i, -1, 0);
                }
                VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                if (!vm.getStockpileLevels(levels))
                {
                    for (int k = 0; k < 0x10; k++)
                    {
                        this.stockpileExtension.showGood(gfx, k, -1, 0);
                    }
                }
                else
                {
                    int index = 0;
                    if (levels.woodLevel > 13333248.0)
                    {
                        index = 12;
                    }
                    else if (levels.woodLevel > 3733248.0)
                    {
                        index = 11;
                    }
                    else if (levels.woodLevel > 1333248.0)
                    {
                        index = 10;
                    }
                    else if (levels.woodLevel > 373248.0)
                    {
                        index = 9;
                    }
                    else if (levels.woodLevel > 133248.0)
                    {
                        index = 8;
                    }
                    else if (levels.woodLevel > 37248.0)
                    {
                        index = 7;
                    }
                    else if (levels.woodLevel > 13248.0)
                    {
                        index = 6;
                    }
                    else if (levels.woodLevel > 3648.0)
                    {
                        index = 5;
                    }
                    else if (levels.woodLevel > 1248.0)
                    {
                        index = 4;
                    }
                    else if (levels.woodLevel > 288.0)
                    {
                        index = 3;
                    }
                    else if (levels.woodLevel > 48.0)
                    {
                        index = 2;
                    }
                    else if (levels.woodLevel > 0.0)
                    {
                        index = 1;
                    }
                    else
                    {
                        index = 0;
                    }
                    int num4 = 0;
                    if (levels.stoneLevel > 13333248.0)
                    {
                        num4 = 12;
                    }
                    else if (levels.stoneLevel > 3733248.0)
                    {
                        num4 = 11;
                    }
                    else if (levels.stoneLevel > 1333248.0)
                    {
                        num4 = 10;
                    }
                    else if (levels.stoneLevel > 373248.0)
                    {
                        num4 = 9;
                    }
                    else if (levels.stoneLevel > 133248.0)
                    {
                        num4 = 8;
                    }
                    else if (levels.stoneLevel > 37248.0)
                    {
                        num4 = 7;
                    }
                    else if (levels.stoneLevel > 13248.0)
                    {
                        num4 = 6;
                    }
                    else if (levels.stoneLevel > 3648.0)
                    {
                        num4 = 5;
                    }
                    else if (levels.stoneLevel > 1248.0)
                    {
                        num4 = 4;
                    }
                    else if (levels.stoneLevel > 288.0)
                    {
                        num4 = 3;
                    }
                    else if (levels.stoneLevel > 48.0)
                    {
                        num4 = 2;
                    }
                    else if (levels.stoneLevel > 0.0)
                    {
                        num4 = 1;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    int num5 = 0;
                    if (levels.ironLevel > 13333248.0)
                    {
                        num5 = 12;
                    }
                    else if (levels.ironLevel > 3733248.0)
                    {
                        num5 = 11;
                    }
                    else if (levels.ironLevel > 1333248.0)
                    {
                        num5 = 10;
                    }
                    else if (levels.ironLevel > 373248.0)
                    {
                        num5 = 9;
                    }
                    else if (levels.ironLevel > 133248.0)
                    {
                        num5 = 8;
                    }
                    else if (levels.ironLevel > 37248.0)
                    {
                        num5 = 7;
                    }
                    else if (levels.ironLevel > 13248.0)
                    {
                        num5 = 6;
                    }
                    else if (levels.ironLevel > 3648.0)
                    {
                        num5 = 5;
                    }
                    else if (levels.ironLevel > 1248.0)
                    {
                        num5 = 4;
                    }
                    else if (levels.ironLevel > 288.0)
                    {
                        num5 = 3;
                    }
                    else if (levels.ironLevel > 48.0)
                    {
                        num5 = 2;
                    }
                    else if (levels.ironLevel > 0.0)
                    {
                        num5 = 1;
                    }
                    else
                    {
                        num5 = 0;
                    }
                    int num6 = 0;
                    if (levels.pitchLevel > 4444416.0)
                    {
                        num6 = 12;
                    }
                    else if (levels.pitchLevel > 1244416.0)
                    {
                        num6 = 11;
                    }
                    else if (levels.pitchLevel > 444416.0)
                    {
                        num6 = 10;
                    }
                    else if (levels.pitchLevel > 124416.0)
                    {
                        num6 = 9;
                    }
                    else if (levels.pitchLevel > 44416.0)
                    {
                        num6 = 8;
                    }
                    else if (levels.pitchLevel > 12416.0)
                    {
                        num6 = 7;
                    }
                    else if (levels.pitchLevel > 4416.0)
                    {
                        num6 = 6;
                    }
                    else if (levels.pitchLevel > 1216.0)
                    {
                        num6 = 5;
                    }
                    else if (levels.pitchLevel > 416.0)
                    {
                        num6 = 4;
                    }
                    else if (levels.pitchLevel > 96.0)
                    {
                        num6 = 3;
                    }
                    else if (levels.pitchLevel > 16.0)
                    {
                        num6 = 2;
                    }
                    else if (levels.pitchLevel > 0.0)
                    {
                        num6 = 1;
                    }
                    else
                    {
                        num6 = 0;
                    }
                    for (int m = 0; m < 0x10; m++)
                    {
                        this.pilesUsed[m] = false;
                    }
                    int num8 = ((index + num4) + num5) + num6;
                    if (num8 > 0x10)
                    {
                        int num9 = 0x10;
                        int num10 = 0;
                        if (index >= 1)
                        {
                            num10++;
                        }
                        if (num4 >= 1)
                        {
                            num10++;
                        }
                        if (num5 >= 1)
                        {
                            num10++;
                        }
                        if (num6 >= 1)
                        {
                            num10++;
                        }
                        num9 -= num10;
                        double num11 = ((double) num9) / ((double) (num8 - num10));
                        PileOrderSort[] sortArray = new PileOrderSort[4];
                        int num12 = 0;
                        if (index > 1)
                        {
                            PileOrderSort sort;
                            sort = new PileOrderSort {
                                origPiles = index - 1,
                                numPiles = index - 1,
                                type = 0
                            };
                            sortArray[num12++] = sort;
                        }
                        if (num4 > 1)
                        {
                            PileOrderSort sort2;
                            sort2 = new PileOrderSort {
                                origPiles = num4 - 1,
                                numPiles = num4 - 1,
                                type = 3
                            };
                            sortArray[num12++] = sort2;
                        }
                        if (num5 > 1)
                        {
                            PileOrderSort sort3;
                            sort3 = new PileOrderSort {
                                origPiles = num5 - 1,
                                numPiles = num5 - 1,
                                type = 4
                            };
                            sortArray[num12++] = sort3;
                        }
                        if (num6 > 1)
                        {
                            PileOrderSort sort4;
                            sort4 = new PileOrderSort {
                                origPiles = num6 - 1,
                                numPiles = num6 - 1,
                                type = 5
                            };
                            sortArray[num12++] = sort4;
                        }
                        if (num12 > 1)
                        {
                            for (int num13 = 0; num13 < (num12 - 1); num13++)
                            {
                                for (int num14 = 0; num14 < (num12 - 1); num14++)
                                {
                                    if (sortArray[num14].numPiles < sortArray[num14 + 1].numPiles)
                                    {
                                        PileOrderSort sort5 = sortArray[num14];
                                        sortArray[num14] = sortArray[num14 + 1];
                                        sortArray[num14 + 1] = sort5;
                                    }
                                }
                            }
                        }
                        int num15 = 0;
                        for (int num16 = 0; num16 < num12; num16++)
                        {
                            sortArray[num16].numPiles = Math.Floor((double) (sortArray[num16].numPiles * num11));
                            num15 += (int) sortArray[num16].numPiles;
                        }
                        if (num15 < num9)
                        {
                            int num17 = num9 - num15;
                            for (int num18 = 0; num17 > 0; num18++)
                            {
                                int num19 = num18 % num12;
                                if (sortArray[num19].numPiles < sortArray[num19].origPiles)
                                {
                                    PileOrderSort sort1 = sortArray[num19];
                                    sort1.numPiles++;
                                    num17--;
                                }
                            }
                        }
                        if (index >= 1)
                        {
                            index = 1;
                        }
                        if (num4 >= 1)
                        {
                            num4 = 1;
                        }
                        if (num5 >= 1)
                        {
                            num5 = 1;
                        }
                        if (num6 >= 1)
                        {
                            num6 = 1;
                        }
                        for (int num20 = 0; num20 < num12; num20++)
                        {
                            int numPiles = (int) sortArray[num20].numPiles;
                            switch (sortArray[num20].type)
                            {
                                case 0:
                                    index += numPiles;
                                    break;

                                case 3:
                                    num4 += numPiles;
                                    break;

                                case 4:
                                    num5 += numPiles;
                                    break;

                                case 5:
                                    num6 += numPiles;
                                    break;
                            }
                        }
                        int num22 = ((index + num4) + num5) + num6;
                        if (num22 != 0x10)
                        {
                            index = 0;
                        }
                    }
                    int num23 = 0;
                    int num24 = 0;
                    for (int n = 0; n < index; n++)
                    {
                        num24 = this.woodPileOrder[num23++];
                        this.pilesUsed[num24] = true;
                        if (n != (index - 1))
                        {
                            this.stockpileExtension.showGood(gfx, num24, 6, 0x30);
                        }
                        else
                        {
                            int num26 = (((int) levels.woodLevel) - this.goods48Levels[index]) / this.goodsDividers[index];
                            this.stockpileExtension.showGood(gfx, num24, 6, Math.Min(num26, 0x30));
                        }
                    }
                    num23 = 0;
                    for (int num27 = 0; num27 < num5; num27++)
                    {
                        do
                        {
                            num24 = this.ironPileOrder[num23++];
                        }
                        while (this.pilesUsed[num24]);
                        this.pilesUsed[num24] = true;
                        if (num27 != (num5 - 1))
                        {
                            this.stockpileExtension.showGood(gfx, num24, 8, 0x30);
                        }
                        else
                        {
                            int num28 = (((int) levels.ironLevel) - this.goods48Levels[num5]) / this.goodsDividers[num5];
                            this.stockpileExtension.showGood(gfx, num24, 8, Math.Min(num28, 0x30));
                        }
                    }
                    num23 = 0;
                    for (int num29 = 0; num29 < num4; num29++)
                    {
                        do
                        {
                            num24 = this.stonePileOrder[num23++];
                        }
                        while (this.pilesUsed[num24]);
                        this.pilesUsed[num24] = true;
                        if (num29 != (num4 - 1))
                        {
                            this.stockpileExtension.showGood(gfx, num24, 7, 0x30);
                        }
                        else
                        {
                            int num30 = (((int) levels.stoneLevel) - this.goods48Levels[num4]) / this.goodsDividers[num4];
                            this.stockpileExtension.showGood(gfx, num24, 7, Math.Min(num30, 0x30));
                        }
                    }
                    num23 = 0;
                    for (int num31 = 0; num31 < num6; num31++)
                    {
                        do
                        {
                            num24 = this.pitchPileOrder[num23++];
                        }
                        while (this.pilesUsed[num24]);
                        this.pilesUsed[num24] = true;
                        if (num31 != (num6 - 1))
                        {
                            this.stockpileExtension.showGood(gfx, num24, 9, 0x10);
                        }
                        else
                        {
                            int num32 = (((int) levels.pitchLevel) - this.goods16Levels[num6]) / this.goodsDividers[num6];
                            this.stockpileExtension.showGood(gfx, num24, 9, Math.Min(num32, 0x10));
                        }
                    }
                }
            }
        }

        public void updateSymbolGFX()
        {
            this.symbolSprite.Visible = false;
            this.symbolSprite.SpriteNo = 0x3a;
            if (VillageBuildingsData.buildingRequiresWorker(this.buildingType) && this.complete)
            {
                if (!this.buildingActive)
                {
                    this.symbolSprite.initAnim(0x3b, 11, 1, 100);
                    this.symbolSprite.Visible = true;
                }
                else if (!this.gotEmployee)
                {
                    this.symbolSprite.Visible = true;
                }
            }
        }

        public bool Visible
        {
            set
            {
                if (this.shadowSprite != null)
                {
                    this.shadowSprite.Visible = value;
                }
                if (this.baseSprite != null)
                {
                    this.baseSprite.Visible = value;
                }
                if (this.worker != null)
                {
                    this.worker.Visible = value;
                }
            }
        }

        public class PileOrderSort
        {
            public double numPiles;
            public double origPiles;
            public int type;
        }
    }
}

