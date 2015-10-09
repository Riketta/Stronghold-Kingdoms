namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GloryPanel : CustomSelfDrawPanel, IDockableControl
    {
        private int[] bands = new int[] { 
            0, 0, 1, 0x13, 0, 0, 2, 0x12, 0, 1, 1, 0x12, 0, 1, 2, 0x11, 
            0, 1, 3, 0x10, 0, 1, 4, 15, 0, 1, 5, 14, 0, 1, 6, 13, 
            0, 2, 6, 12, 1, 2, 6, 11, 1, 3, 6, 10, 1, 4, 6, 9, 
            1, 5, 6, 8, 1, 6, 6, 7, 2, 6, 6, 6
         };
        private bool bandsMade;
        private int bandYPos0;
        private int bandYPos100;
        private int bandYPos1000;
        private int bandYPos10000;
        private int bandYPos100000;
        private int bandYPos1000000;
        private CustomSelfDrawPanel.CSDLine bottomLine = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine bottomLineS = new CustomSelfDrawPanel.CSDLine();
        private IContainer components;
        private DockableControl dockableControl;
        private int[][] filledBands = new int[15][];
        private CustomSelfDrawPanel.CSDImage flag0ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag0ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag0ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag0ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag0ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag10ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag10ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag10ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag10ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag10ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag11ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag11ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag11ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag11ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag11ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag12ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag12ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag12ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag12ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag12ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag13ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag13ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag13ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag13ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag13ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag14ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag14ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag14ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag14ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag14ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag15ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag15ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag15ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag15ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag15ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag16ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag16ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag16ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag16ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag16ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag17ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag17ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag17ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag17ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag17ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag18ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag18ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag18ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag18ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag18ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag19ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag19ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag19ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag19ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag19ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag1ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag1ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag1ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag1ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag1ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag2ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag2ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag2ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag2ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag2ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag3ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag3ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag3ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag3ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag3ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag4ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag4ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag4ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag4ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag4ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag5ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag5ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag5ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag5ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag5ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag6ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag6ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag6ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag6ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag6ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag7ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag7ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag7ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag7ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag7ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag8ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag8ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag8ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag8ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag8ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag9ImageStar1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag9ImageStar2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag9ImageStar3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag9ImageStar4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flag9ImageStar5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage0 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage10 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage11 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage12 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage13 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage14 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage15 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage16 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage17 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage18 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage19 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagImage9 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage0 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage10 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage11 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage12 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage13 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage14 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage15 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage16 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage17 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage18 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage19 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage8 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage flagpoleImage9 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel gloryRoundEnding = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton gloryWinnerButton = new CustomSelfDrawPanel.CSDButton();
        private int[,] lastHousePoints = new int[20, 2];
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel mark0Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mark1000000Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mark100000Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mark10000Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mark1000Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mark100Label = new CustomSelfDrawPanel.CSDLabel();
        private int[] markPositionPercents = new int[] { 0, 10, 15, 20, 0x19, 30 };
        private const int NUM_BANDS = 15;
        private int[] readOrder = new int[] { 
            0x12, 0x10, 14, 12, 10, 8, 6, 4, 2, 0, 1, 3, 5, 7, 9, 11, 
            13, 15, 0x11, 0x13
         };
        private CustomSelfDrawPanel.CSDLine scaleMark0Line = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark0LineS = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark1000000Line = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark1000000LineS = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark100000Line = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark100000LineS = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark10000Line = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark10000LineS = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark1000Line = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark1000LineS = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark100Line = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleMark100LineS = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleVertLine = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine scaleVertLineS = new CustomSelfDrawPanel.CSDLine();
        private int[] starSteps = new int[] { 0, -1, 1, -2, 2 };
        private bool thirdAge;
        private CustomSelfDrawPanel.CSDLine topLine = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine topLineS = new CustomSelfDrawPanel.CSDLine();
        private const int VERT_XPOS = 0x41;
        private const int VERT_XPOS2 = 0x3b;
        private const int VERT_XPOS3 = 0x3b;
        private CustomSelfDrawPanel.CSDArea viewableArea = new CustomSelfDrawPanel.CSDArea();

        public GloryPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
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

        public CustomSelfDrawPanel.CSDImage getFlagImage(int flag)
        {
            switch (flag)
            {
                case 0:
                    return this.flagImage0;

                case 1:
                    return this.flagImage1;

                case 2:
                    return this.flagImage2;

                case 3:
                    return this.flagImage3;

                case 4:
                    return this.flagImage4;

                case 5:
                    return this.flagImage5;

                case 6:
                    return this.flagImage6;

                case 7:
                    return this.flagImage7;

                case 8:
                    return this.flagImage8;

                case 9:
                    return this.flagImage9;

                case 10:
                    return this.flagImage10;

                case 11:
                    return this.flagImage11;

                case 12:
                    return this.flagImage12;

                case 13:
                    return this.flagImage13;

                case 14:
                    return this.flagImage14;

                case 15:
                    return this.flagImage15;

                case 0x10:
                    return this.flagImage16;

                case 0x11:
                    return this.flagImage17;

                case 0x12:
                    return this.flagImage18;

                case 0x13:
                    return this.flagImage19;
            }
            return this.flagImage0;
        }

        public CustomSelfDrawPanel.CSDImage getFlagpoleImage(int flag)
        {
            switch (flag)
            {
                case 0:
                    return this.flagpoleImage0;

                case 1:
                    return this.flagpoleImage1;

                case 2:
                    return this.flagpoleImage2;

                case 3:
                    return this.flagpoleImage3;

                case 4:
                    return this.flagpoleImage4;

                case 5:
                    return this.flagpoleImage5;

                case 6:
                    return this.flagpoleImage6;

                case 7:
                    return this.flagpoleImage7;

                case 8:
                    return this.flagpoleImage8;

                case 9:
                    return this.flagpoleImage9;

                case 10:
                    return this.flagpoleImage10;

                case 11:
                    return this.flagpoleImage11;

                case 12:
                    return this.flagpoleImage12;

                case 13:
                    return this.flagpoleImage13;

                case 14:
                    return this.flagpoleImage14;

                case 15:
                    return this.flagpoleImage15;

                case 0x10:
                    return this.flagpoleImage16;

                case 0x11:
                    return this.flagpoleImage17;

                case 0x12:
                    return this.flagpoleImage18;

                case 0x13:
                    return this.flagpoleImage19;
            }
            return this.flagpoleImage0;
        }

        public void GetHouseGloryPointsCallBack(GetHouseGloryPoints_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.HouseGloryPoints = returnData.gloryPoints;
                GameEngine.Instance.World.HouseGloryRoundData = returnData.gloryRoundData;
                this.init();
            }
        }

        public CustomSelfDrawPanel.CSDImage getStar(int flag, int star)
        {
            switch (flag)
            {
                case 0:
                    switch (star)
                    {
                        case 0:
                            return this.flag0ImageStar1;

                        case 1:
                            return this.flag0ImageStar2;

                        case 2:
                            return this.flag0ImageStar3;

                        case 3:
                            return this.flag0ImageStar4;

                        case 4:
                            return this.flag0ImageStar5;
                    }
                    break;

                case 1:
                    switch (star)
                    {
                        case 0:
                            return this.flag1ImageStar1;

                        case 1:
                            return this.flag1ImageStar2;

                        case 2:
                            return this.flag1ImageStar3;

                        case 3:
                            return this.flag1ImageStar4;

                        case 4:
                            return this.flag1ImageStar5;
                    }
                    break;

                case 2:
                    switch (star)
                    {
                        case 0:
                            return this.flag2ImageStar1;

                        case 1:
                            return this.flag2ImageStar2;

                        case 2:
                            return this.flag2ImageStar3;

                        case 3:
                            return this.flag2ImageStar4;

                        case 4:
                            return this.flag2ImageStar5;
                    }
                    break;

                case 3:
                    switch (star)
                    {
                        case 0:
                            return this.flag3ImageStar1;

                        case 1:
                            return this.flag3ImageStar2;

                        case 2:
                            return this.flag3ImageStar3;

                        case 3:
                            return this.flag3ImageStar4;

                        case 4:
                            return this.flag3ImageStar5;
                    }
                    break;

                case 4:
                    switch (star)
                    {
                        case 0:
                            return this.flag4ImageStar1;

                        case 1:
                            return this.flag4ImageStar2;

                        case 2:
                            return this.flag4ImageStar3;

                        case 3:
                            return this.flag4ImageStar4;

                        case 4:
                            return this.flag4ImageStar5;
                    }
                    break;

                case 5:
                    switch (star)
                    {
                        case 0:
                            return this.flag5ImageStar1;

                        case 1:
                            return this.flag5ImageStar2;

                        case 2:
                            return this.flag5ImageStar3;

                        case 3:
                            return this.flag5ImageStar4;

                        case 4:
                            return this.flag5ImageStar5;
                    }
                    break;

                case 6:
                    switch (star)
                    {
                        case 0:
                            return this.flag6ImageStar1;

                        case 1:
                            return this.flag6ImageStar2;

                        case 2:
                            return this.flag6ImageStar3;

                        case 3:
                            return this.flag6ImageStar4;

                        case 4:
                            return this.flag6ImageStar5;
                    }
                    break;

                case 7:
                    switch (star)
                    {
                        case 0:
                            return this.flag7ImageStar1;

                        case 1:
                            return this.flag7ImageStar2;

                        case 2:
                            return this.flag7ImageStar3;

                        case 3:
                            return this.flag7ImageStar4;

                        case 4:
                            return this.flag7ImageStar5;
                    }
                    break;

                case 8:
                    switch (star)
                    {
                        case 0:
                            return this.flag8ImageStar1;

                        case 1:
                            return this.flag8ImageStar2;

                        case 2:
                            return this.flag8ImageStar3;

                        case 3:
                            return this.flag8ImageStar4;

                        case 4:
                            return this.flag8ImageStar5;
                    }
                    break;

                case 9:
                    switch (star)
                    {
                        case 0:
                            return this.flag9ImageStar1;

                        case 1:
                            return this.flag9ImageStar2;

                        case 2:
                            return this.flag9ImageStar3;

                        case 3:
                            return this.flag9ImageStar4;

                        case 4:
                            return this.flag9ImageStar5;
                    }
                    break;

                case 10:
                    switch (star)
                    {
                        case 0:
                            return this.flag10ImageStar1;

                        case 1:
                            return this.flag10ImageStar2;

                        case 2:
                            return this.flag10ImageStar3;

                        case 3:
                            return this.flag10ImageStar4;

                        case 4:
                            return this.flag10ImageStar5;
                    }
                    break;

                case 11:
                    switch (star)
                    {
                        case 0:
                            return this.flag11ImageStar1;

                        case 1:
                            return this.flag11ImageStar2;

                        case 2:
                            return this.flag11ImageStar3;

                        case 3:
                            return this.flag11ImageStar4;

                        case 4:
                            return this.flag11ImageStar5;
                    }
                    break;

                case 12:
                    switch (star)
                    {
                        case 0:
                            return this.flag12ImageStar1;

                        case 1:
                            return this.flag12ImageStar2;

                        case 2:
                            return this.flag12ImageStar3;

                        case 3:
                            return this.flag12ImageStar4;

                        case 4:
                            return this.flag12ImageStar5;
                    }
                    break;

                case 13:
                    switch (star)
                    {
                        case 0:
                            return this.flag13ImageStar1;

                        case 1:
                            return this.flag13ImageStar2;

                        case 2:
                            return this.flag13ImageStar3;

                        case 3:
                            return this.flag13ImageStar4;

                        case 4:
                            return this.flag13ImageStar5;
                    }
                    break;

                case 14:
                    switch (star)
                    {
                        case 0:
                            return this.flag14ImageStar1;

                        case 1:
                            return this.flag14ImageStar2;

                        case 2:
                            return this.flag14ImageStar3;

                        case 3:
                            return this.flag14ImageStar4;

                        case 4:
                            return this.flag14ImageStar5;
                    }
                    break;

                case 15:
                    switch (star)
                    {
                        case 0:
                            return this.flag15ImageStar1;

                        case 1:
                            return this.flag15ImageStar2;

                        case 2:
                            return this.flag15ImageStar3;

                        case 3:
                            return this.flag15ImageStar4;

                        case 4:
                            return this.flag15ImageStar5;
                    }
                    break;

                case 0x10:
                    switch (star)
                    {
                        case 0:
                            return this.flag16ImageStar1;

                        case 1:
                            return this.flag16ImageStar2;

                        case 2:
                            return this.flag16ImageStar3;

                        case 3:
                            return this.flag16ImageStar4;

                        case 4:
                            return this.flag16ImageStar5;
                    }
                    break;

                case 0x11:
                    switch (star)
                    {
                        case 0:
                            return this.flag17ImageStar1;

                        case 1:
                            return this.flag17ImageStar2;

                        case 2:
                            return this.flag17ImageStar3;

                        case 3:
                            return this.flag17ImageStar4;

                        case 4:
                            return this.flag17ImageStar5;
                    }
                    break;

                case 0x12:
                    switch (star)
                    {
                        case 0:
                            return this.flag18ImageStar1;

                        case 1:
                            return this.flag18ImageStar2;

                        case 2:
                            return this.flag18ImageStar3;

                        case 3:
                            return this.flag18ImageStar4;

                        case 4:
                            return this.flag18ImageStar5;
                    }
                    break;

                case 0x13:
                    switch (star)
                    {
                        case 0:
                            return this.flag19ImageStar1;

                        case 1:
                            return this.flag19ImageStar2;

                        case 2:
                            return this.flag19ImageStar3;

                        case 3:
                            return this.flag19ImageStar4;

                        case 4:
                            return this.flag19ImageStar5;
                    }
                    break;
            }
            return this.flag0ImageStar1;
        }

        public void gloryWinnerClick()
        {
            InterfaceMgr.Instance.openGloryVictoryPopup();
        }

        public void init()
        {
            this.thirdAge = GameEngine.Instance.World.ThirdAgeWorld;
            int num = 100;
            int num2 = 0x3e8;
            int num3 = 0x2710;
            int num4 = 0x186a0;
            int aiWorldGloryWinLevel = 0xf4240;
            if (GameEngine.Instance.LocalWorldData.AIWorld)
            {
                num = 0x3e8;
                num2 = 0x2710;
                num3 = 0x186a0;
                num4 = 0xf4240;
                aiWorldGloryWinLevel = GameEngine.Instance.World.aiWorldGloryWinLevel;
            }
            else if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
            {
                num = 0x3e8;
                num2 = 0x2710;
                num3 = 0x186a0;
                num4 = 0xf4240;
                aiWorldGloryWinLevel = 0x2faf080;
            }
            else if (this.thirdAge)
            {
                num = 0x3e8;
                num2 = 0x2710;
                num3 = 0x186a0;
                num4 = 0xf4240;
                aiWorldGloryWinLevel = 0x3d0900;
            }
            if (GameEngine.Instance.World.testGloryPointsUpdate())
            {
                RemoteServices.Instance.set_GetHouseGloryPoints_UserCallBack(new RemoteServices.GetHouseGloryPoints_UserCallBack(this.GetHouseGloryPointsCallBack));
                RemoteServices.Instance.GetHouseGloryPoints();
            }
            int num6 = 0;
            int num7 = 0;
            for (int i = 0; i < 20; i++)
            {
                if (!GameEngine.Instance.World.HouseInfo[i + 1].loser)
                {
                    this.lastHousePoints[i, 0] = GameEngine.Instance.World.HouseGloryPoints[i + 1];
                    if (this.lastHousePoints[i, 0] > num6)
                    {
                        num6 = this.lastHousePoints[i, 0];
                    }
                }
                else
                {
                    this.lastHousePoints[i, 0] = -1;
                    num7++;
                }
                this.lastHousePoints[i, 1] = i;
            }
            for (int j = 0; j < 0x13; j++)
            {
                for (int n = 0; n < 0x13; n++)
                {
                    if (this.lastHousePoints[n, 0] < this.lastHousePoints[n + 1, 0])
                    {
                        int num11 = this.lastHousePoints[n, 0];
                        this.lastHousePoints[n, 0] = this.lastHousePoints[n + 1, 0];
                        this.lastHousePoints[n + 1, 0] = num11;
                        num11 = this.lastHousePoints[n, 1];
                        this.lastHousePoints[n, 1] = this.lastHousePoints[n + 1, 1];
                        this.lastHousePoints[n + 1, 1] = num11;
                    }
                }
            }
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.glory_background;
            this.mainBackgroundImage.Width = 0x640;
            this.mainBackgroundImage.Height = 0x400;
            int width = base.Width;
            int height = base.Height;
            this.mainBackgroundImage.Position = new Point((width - 0x640) / 2, -(0x400 - height));
            base.addControl(this.mainBackgroundImage);
            this.viewableArea.Position = new Point((0x640 - width) / 2, 0x400 - height);
            this.viewableArea.Size = new Size(base.Size.Width, base.Size.Height - 50);
            this.mainBackgroundImage.addControl(this.viewableArea);
            int y = 0x19;
            height -= 50;
            CustomSelfDrawPanel.WikiLinkControl.init(this.viewableArea, 0x16, new Point(base.Width - 0x26, 0x1a));
            if ((GameEngine.Instance.World.HouseGloryRoundData != null) && (GameEngine.Instance.World.HouseGloryRoundData.winnerHouseID > 0))
            {
                this.gloryWinnerButton.Position = new Point(((this.viewableArea.Width - 200) - 15) - 30, y);
                this.gloryWinnerButton.Size = new Size(200, 0x26);
                this.gloryWinnerButton.Text.Text = SK.Text("TEMP_ViewWinner", "Last Glory Round Result");
                this.gloryWinnerButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                if (Program.mySettings.LanguageIdent == "it")
                {
                    this.gloryWinnerButton.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                }
                else
                {
                    this.gloryWinnerButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                }
                this.gloryWinnerButton.TextYOffset = -1;
                this.gloryWinnerButton.Text.Color = ARGBColors.Black;
                this.gloryWinnerButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gloryWinnerClick), "Glory_view_result");
                this.viewableArea.addControl(this.gloryWinnerButton);
                this.gloryWinnerButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
                this.gloryWinnerButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            }
            if (num6 >= aiWorldGloryWinLevel)
            {
                DateTime time = VillageMap.getCurrentServerTime();
                DateTime time2 = new DateTime(time.Year, time.Month, time.Day, 8, 0, 0);
                if (time > time2)
                {
                    time2 = time2.AddDays(1.0);
                }
                TimeSpan span = (TimeSpan) (time2 - time);
                int totalHours = (int) span.TotalHours;
                string str = SK.Text("GloryPanel_EndingSoon", "Glory Round Ending Soon");
                if (totalHours > 1)
                {
                    string str2 = str;
                    str = (str2 + " ( " + SK.Text("GloryPanel_Approximately", "Approximately") + " : " + totalHours.ToString() + " ") + SK.Text("Reports_Hours", "hours") + " )";
                }
                this.gloryRoundEnding.Text = str;
                this.gloryRoundEnding.Color = ARGBColors.White;
                this.gloryRoundEnding.DropShadowColor = ARGBColors.Black;
                this.gloryRoundEnding.Position = new Point(100, 2);
                this.gloryRoundEnding.Size = new Size(700, 20);
                this.gloryRoundEnding.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.gloryRoundEnding.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
                this.viewableArea.addControl(this.gloryRoundEnding);
            }
            this.scaleVertLine.Position = new Point(0x41, y);
            this.scaleVertLine.Size = new Size(0, height - y);
            this.scaleVertLine.LineColor = ARGBColors.Black;
            this.viewableArea.addControl(this.scaleVertLine);
            this.scaleMark0LineS.Position = new Point(0x3b, height - 1);
            this.scaleMark0LineS.Size = new Size(6, 0);
            this.scaleMark0LineS.LineColor = ARGBColors.Black;
            this.viewableArea.addControl(this.scaleMark0LineS);
            int num16 = ((height - y) * this.markPositionPercents[1]) / 100;
            this.scaleMark100Line.Position = new Point(0x3b, height - num16);
            this.scaleMark100Line.Size = new Size(6, 0);
            this.scaleMark100Line.LineColor = ARGBColors.Black;
            this.viewableArea.addControl(this.scaleMark100Line);
            num16 = ((height - y) * (this.markPositionPercents[1] + this.markPositionPercents[2])) / 100;
            this.scaleMark1000Line.Position = new Point(0x3b, height - num16);
            this.scaleMark1000Line.Size = new Size(6, 0);
            this.scaleMark1000Line.LineColor = ARGBColors.Black;
            this.viewableArea.addControl(this.scaleMark1000Line);
            num16 = ((height - y) * ((this.markPositionPercents[1] + this.markPositionPercents[2]) + this.markPositionPercents[3])) / 100;
            this.scaleMark10000Line.Position = new Point(0x3b, height - num16);
            this.scaleMark10000Line.Size = new Size(6, 0);
            this.scaleMark10000Line.LineColor = ARGBColors.Black;
            this.viewableArea.addControl(this.scaleMark10000Line);
            num16 = ((height - y) * (((this.markPositionPercents[1] + this.markPositionPercents[2]) + this.markPositionPercents[3]) + this.markPositionPercents[4])) / 100;
            this.scaleMark100000Line.Position = new Point(0x3b, height - num16);
            this.scaleMark100000Line.Size = new Size(6, 0);
            this.scaleMark100000Line.LineColor = ARGBColors.Black;
            this.viewableArea.addControl(this.scaleMark100000Line);
            num16 = height - y;
            this.scaleMark1000000Line.Position = new Point(0x3b, height - num16);
            this.scaleMark1000000Line.Size = new Size(6, 0);
            this.scaleMark1000000Line.LineColor = ARGBColors.Black;
            this.viewableArea.addControl(this.scaleMark1000000Line);
            this.scaleVertLineS.Position = new Point(0x40, y - 1);
            this.scaleVertLineS.Size = new Size(0, height - y);
            this.scaleVertLineS.LineColor = ARGBColors.White;
            this.viewableArea.addControl(this.scaleVertLineS);
            this.topLineS.Position = new Point(0x42, y - 1);
            this.topLineS.Size = new Size((base.Width - 0x41) - 1, 0);
            this.topLineS.LineColor = ARGBColors.Yellow;
            this.viewableArea.addControl(this.topLineS);
            this.bottomLineS.Position = new Point(0x42, height - 1);
            this.bottomLineS.Size = new Size((base.Width - 0x41) - 1, 0);
            this.bottomLineS.LineColor = ARGBColors.Yellow;
            this.viewableArea.addControl(this.bottomLineS);
            this.scaleMark0Line.Position = new Point(0x3a, (height - 1) - 1);
            this.scaleMark0Line.Size = new Size(6, 0);
            this.scaleMark0Line.LineColor = ARGBColors.White;
            this.viewableArea.addControl(this.scaleMark0Line);
            this.bandYPos0 = height;
            this.mark0Label.Text = "0";
            this.mark0Label.Color = ARGBColors.White;
            this.mark0Label.DropShadowColor = ARGBColors.Black;
            this.mark0Label.Position = new Point(0, (height - 20) + 2);
            this.mark0Label.Size = new Size(0x3b, 20);
            this.mark0Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mark0Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.viewableArea.addControl(this.mark0Label);
            num16 = ((height - y) * this.markPositionPercents[1]) / 100;
            this.bandYPos100 = this.bandYPos0 - num16;
            this.scaleMark100LineS.Position = new Point(0x3a, (height - num16) - 1);
            this.scaleMark100LineS.Size = new Size(6, 0);
            this.scaleMark100LineS.LineColor = ARGBColors.White;
            this.viewableArea.addControl(this.scaleMark100LineS);
            this.mark100Label.Text = num.ToString();
            this.mark100Label.Color = ARGBColors.White;
            this.mark100Label.DropShadowColor = ARGBColors.Black;
            this.mark100Label.Position = new Point(0, (height - num16) - 9);
            this.mark100Label.Size = new Size(0x3b, 20);
            this.mark100Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mark100Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.viewableArea.addControl(this.mark100Label);
            num16 = ((height - y) * (this.markPositionPercents[1] + this.markPositionPercents[2])) / 100;
            this.bandYPos1000 = this.bandYPos0 - num16;
            this.scaleMark1000LineS.Position = new Point(0x3a, (height - num16) - 1);
            this.scaleMark1000LineS.Size = new Size(6, 0);
            this.scaleMark1000LineS.LineColor = ARGBColors.White;
            this.viewableArea.addControl(this.scaleMark1000LineS);
            this.mark1000Label.Text = num2.ToString();
            this.mark1000Label.Color = ARGBColors.White;
            this.mark1000Label.DropShadowColor = ARGBColors.Black;
            this.mark1000Label.Position = new Point(0, (height - num16) - 9);
            this.mark1000Label.Size = new Size(0x3b, 20);
            this.mark1000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mark1000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.viewableArea.addControl(this.mark1000Label);
            num16 = ((height - y) * ((this.markPositionPercents[1] + this.markPositionPercents[2]) + this.markPositionPercents[3])) / 100;
            this.bandYPos10000 = this.bandYPos0 - num16;
            this.scaleMark10000LineS.Position = new Point(0x3a, (height - num16) - 1);
            this.scaleMark10000LineS.Size = new Size(6, 0);
            this.scaleMark10000LineS.LineColor = ARGBColors.White;
            this.viewableArea.addControl(this.scaleMark10000LineS);
            this.mark10000Label.Text = num3.ToString();
            this.mark10000Label.Color = ARGBColors.White;
            this.mark10000Label.DropShadowColor = ARGBColors.Black;
            this.mark10000Label.Position = new Point(0, (height - num16) - 9);
            this.mark10000Label.Size = new Size(0x3b, 20);
            this.mark10000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mark10000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.viewableArea.addControl(this.mark10000Label);
            num16 = ((height - y) * (((this.markPositionPercents[1] + this.markPositionPercents[2]) + this.markPositionPercents[3]) + this.markPositionPercents[4])) / 100;
            this.bandYPos100000 = this.bandYPos0 - num16;
            this.scaleMark100000LineS.Position = new Point(0x3a, (height - num16) - 1);
            this.scaleMark100000LineS.Size = new Size(6, 0);
            this.scaleMark100000LineS.LineColor = ARGBColors.White;
            this.viewableArea.addControl(this.scaleMark100000LineS);
            this.mark100000Label.Text = num4.ToString();
            this.mark100000Label.Color = ARGBColors.White;
            this.mark100000Label.DropShadowColor = ARGBColors.Black;
            this.mark100000Label.Position = new Point(0, (height - num16) - 9);
            this.mark100000Label.Size = new Size(0x3b, 20);
            this.mark100000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mark100000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.viewableArea.addControl(this.mark100000Label);
            num16 = height - y;
            this.bandYPos1000000 = this.bandYPos0 - num16;
            this.scaleMark1000000LineS.Position = new Point(0x3a, (height - num16) - 1);
            this.scaleMark1000000LineS.Size = new Size(6, 0);
            this.scaleMark1000000LineS.LineColor = ARGBColors.White;
            this.viewableArea.addControl(this.scaleMark1000000LineS);
            this.mark1000000Label.Text = aiWorldGloryWinLevel.ToString();
            this.mark1000000Label.Color = ARGBColors.White;
            this.mark1000000Label.DropShadowColor = ARGBColors.Black;
            if (aiWorldGloryWinLevel >= 0x989680)
            {
                this.mark1000000Label.Position = new Point(-11, (height - num16) - 9);
                this.mark1000000Label.Size = new Size(0x45, 20);
            }
            else
            {
                this.mark1000000Label.Position = new Point(0, (height - num16) - 9);
                this.mark1000000Label.Size = new Size(0x3b, 20);
            }
            this.mark1000000Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mark1000000Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_RIGHT;
            this.viewableArea.addControl(this.mark1000000Label);
            int num17 = ((base.Width - 0x41) - 5) - 5;
            num17 += num7 * 0x2d;
            this.makeSizes();
            int index = 14;
            for (int k = 0; k < 15; k++)
            {
                if (num17 < this.filledBands[k][20])
                {
                    index = k - 1;
                    break;
                }
            }
            int x = 70;
            int num21 = num7 / 2;
            int num22 = 20 - (num7 - num21);
            for (int m = num21; m < num22; m++)
            {
                int num24 = this.lastHousePoints[this.readOrder[m], 1] + 1;
                int num25 = this.lastHousePoints[this.readOrder[m], 0];
                int num26 = num25;
                int num27 = 0;
                if (num25 < num)
                {
                    num27 = this.bandYPos0;
                    num25 *= this.bandYPos0 - this.bandYPos100;
                    num25 /= num;
                    num27 -= num25;
                }
                else if (num25 < num2)
                {
                    num27 = this.bandYPos100;
                    num25 -= num;
                    num25 *= this.bandYPos100 - this.bandYPos1000;
                    num25 /= num2 - num;
                    num27 -= num25;
                }
                else if (num25 < num3)
                {
                    num27 = this.bandYPos1000;
                    num25 -= num2;
                    num25 *= this.bandYPos1000 - this.bandYPos10000;
                    num25 /= num3 - num2;
                    num27 -= num25;
                }
                else if (num25 < num4)
                {
                    num27 = this.bandYPos10000;
                    num25 -= num3;
                    num25 *= this.bandYPos10000 - this.bandYPos100000;
                    num25 /= num4 - num3;
                    num27 -= num25;
                }
                else if (num25 < aiWorldGloryWinLevel)
                {
                    num27 = this.bandYPos100000;
                    num25 -= num4;
                    num25 *= this.bandYPos100000 - this.bandYPos1000000;
                    num25 /= aiWorldGloryWinLevel - num4;
                    num27 -= num25;
                }
                else
                {
                    num27 = this.bandYPos1000000;
                }
                int num28 = this.filledBands[index][this.readOrder[m]];
                CustomSelfDrawPanel.CSDImage control = this.getFlagImage(m);
                control.Position = new Point(x, num27);
                CustomSelfDrawPanel.CSDImage image2 = this.getFlagpoleImage(m);
                int numVictories = GameEngine.Instance.World.HouseInfo[num24].numVictories;
                int num30 = numVictories;
                if (numVictories > 5)
                {
                    numVictories = 5;
                }
                BaseImage image3 = null;
                int num31 = 0;
                int num32 = 0;
                int num33 = 0;
                int num34 = x;
                switch (num28)
                {
                    case 0:
                        image2.Position = new Point((x + 11) + 1, num27 + 500);
                        x += 0x2d;
                        control.Image = (Image) GFXLibrary.glory_flags_small[num24 - 1];
                        image2.Image = (Image) GFXLibrary.glory_thin_pole;
                        image3 = GFXLibrary.glory_star_small;
                        num31 = 14;
                        num32 = 9;
                        num33 = 4;
                        break;

                    case 1:
                        image2.Position = new Point(x + 0x13, num27 + 500);
                        x += 60;
                        control.Image = (Image) GFXLibrary.glory_flags_med[num24 - 1];
                        image2.Image = (Image) GFXLibrary.glory_thin_pole;
                        image3 = GFXLibrary.glory_star_small;
                        num31 = 20;
                        num32 = 12;
                        num33 = 4;
                        break;

                    case 2:
                        image2.Position = new Point((x + 30) - 5, num27 + 500);
                        x += 90;
                        control.Image = (Image) GFXLibrary.glory_flags_large[num24 - 1];
                        image2.Image = (Image) GFXLibrary.glory_thick_pole;
                        image3 = GFXLibrary.glory_star_large;
                        num31 = 0x1d;
                        num32 = 0x10;
                        num33 = 7;
                        break;

                    case 3:
                        image2.Position = new Point((x + 40) - 6, num27 + 500);
                        x += 110;
                        control.Image = (Image) GFXLibrary.glory_flags_largest[num24 - 1];
                        image2.Image = (Image) GFXLibrary.glory_thick_pole;
                        image3 = GFXLibrary.glory_star_large;
                        num31 = 0x27;
                        num32 = 20;
                        num33 = 7;
                        break;
                }
                control.CustomTooltipID = (0x6a4 + num24) - 1;
                control.CustomTooltipData = num26;
                control.Data = num24;
                control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewHouse), "GloryPanel_view_house");
                this.viewableArea.addControl(image2);
                this.viewableArea.addControl(control);
                for (int num35 = 0; num35 < numVictories; num35++)
                {
                    CustomSelfDrawPanel.CSDImage image4 = this.getStar(num24 - 1, num35);
                    image4.Image = (Image) image3;
                    image4.Position = new Point((num34 + num31) + (this.starSteps[num35] * num32), num27 - num33);
                    this.viewableArea.addControl(image4);
                    CustomSelfDrawPanel.CSDImage image5 = image4;
                    int num36 = 5;
                    for (int num37 = 5; num36 < 100; num37 *= -1)
                    {
                        if (((num35 + num36) + 1) <= num30)
                        {
                            CustomSelfDrawPanel.CSDImage image6 = new CustomSelfDrawPanel.CSDImage {
                                Image = (Image) image3,
                                Position = new Point(num37, -8)
                            };
                            image5.addControl(image6);
                            image5 = image6;
                        }
                        num36 += 5;
                    }
                }
            }
            base.Invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x640, 0x400);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "GloryPanel";
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

        public void makeSizes()
        {
            if (!this.bandsMade)
            {
                this.bandsMade = true;
                for (int i = 0; i < this.bands.Length; i += 4)
                {
                    int num2 = this.bands[i];
                    int num3 = this.bands[i + 1];
                    int num4 = this.bands[i + 2];
                    int num5 = this.bands[i + 3];
                    int[] numArray = new int[0x15];
                    int index = 0;
                    int num7 = 0;
                    for (int j = 0; j < num2; j++)
                    {
                        numArray[index++] = 3;
                        num7 += 110;
                    }
                    for (int k = 0; k < num3; k++)
                    {
                        numArray[index++] = 2;
                        num7 += 90;
                    }
                    for (int m = 0; m < num4; m++)
                    {
                        numArray[index++] = 1;
                        num7 += 60;
                    }
                    for (int n = 0; n < num5; n++)
                    {
                        numArray[index++] = 0;
                        num7 += 0x2d;
                    }
                    numArray[index] = num7;
                    this.filledBands[i / 4] = numArray;
                }
            }
        }

        public void update()
        {
        }

        public void viewHouse()
        {
            if (base.ClickedControl != null)
            {
                int data = base.ClickedControl.Data;
                InterfaceMgr.Instance.showHousePanel(data);
            }
        }
    }
}

