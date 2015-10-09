namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CastleMapPanel : CustomSelfDrawPanel, IDockableControl
    {
        private bool allowDeleteCallback = true;
        private int alphaPulse = 0xff;
        private CustomSelfDrawPanel.CSDImage building1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building1Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage building2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building2Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage building3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building3Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage building4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building4Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage building5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building5Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage building6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building6Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage building7Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building7Label = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage building8Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel building8Label = new CustomSelfDrawPanel.CSDLabel();
        private bool buildingBeingPlaced;
        private CustomSelfDrawPanel.CSDArea captain_castleSelectionBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCaptainButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCaptainDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionCaptainImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionCaptainInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionInset5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton captain_closeButton = new CustomSelfDrawPanel.CSDButton();
        private const int CASTLEPANEL_WINDOW_SIZE = 0x1a6;
        private CustomSelfDrawPanel.CSDButton castlePlace1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlace2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlace3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlace4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlace5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlace6Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlace7Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlace8Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceArcherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceArcherInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceArcherLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea castlePlaceBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage castlePlaceGoldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceGoldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel castlePlaceGuardhouseLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea castlePlaceHeaderArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage castlePlaceInfoImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castlePlaceIronImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceIronLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage castlePlacePanelFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castlePlacePanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton castlePlacePeasantButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlacePeasantInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlacePeasantLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlacePikemanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlacePikemanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlacePikemanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage castlePlacePitchImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlacePitchLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize15Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceStoneImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceStoneLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceSwordsmanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceSwordsmanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceSwordsmanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceTab1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceTab2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceTab3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceTab4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceTab5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceTimeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceTimeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceToggleReinforcementsButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel castlePlaceTypeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceWolfButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceWolfInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceWolfLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage castlePlaceWoodImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceWoodLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castleSelectionArcherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castleSelectionArcherDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionArcherImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionArcherInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionArcherLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea castleSelectionBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton castleSelectionCaptainButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castleSelectionCaptainDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionCaptainImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionCaptainInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton castleSelectionPeasantButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castleSelectionPeasantDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionPeasantImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionPeasantInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionPeasantLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castleSelectionPikemanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castleSelectionPikemanDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionPikemanImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionPikemanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionPikemanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castleSelectionSwordsmanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castleSelectionSwordsmanDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionSwordsmanImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionSwordsmanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionSwordsmanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage castleTotalGoldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleTotalGoldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton commitBuildCancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton commitBuildCommitButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage commitBuildCommitImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage commitBuildGoldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel commitBuildGoldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage commitBuildIronImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel commitBuildIronLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage commitBuildPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage commitBuildPitchImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel commitBuildPitchLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage commitBuildStoneImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel commitBuildStoneLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage commitBuildTimeImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel commitBuildTimeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage commitBuildWoodImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel commitBuildWoodLabel = new CustomSelfDrawPanel.CSDLabel();
        public static bool commitButtonVisible;
        private IContainer components;
        private CustomSelfDrawPanel.CSDFill controlBlockOverlay = new CustomSelfDrawPanel.CSDFill();
        private int currentCastleIcon;
        private int currentCastlePlaceHeight;
        private int currentCastlePlaceTab = -1;
        private int currentUtilHeight;
        private CustomSelfDrawPanel.CSDButton deleteHeaderButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel deleteHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool deleteState;
        private DockableControl dockableControl;
        private DateTime lastCommitClick = DateTime.MinValue;
        private int lastUtilYpos = -1;
        private CustomSelfDrawPanel.CSDButton loadButton = new CustomSelfDrawPanel.CSDButton();
        private OpenFileDialog LoadCampDialog;
        private DateTime m_castleCompletTime = DateTime.Now;
        private bool m_castledCompleted = true;
        private bool nextArcherState;
        private bool nextCaptainState;
        private bool nextPeasantState;
        private bool nextPikemanState;
        private bool nextSwordsmanState;
        private bool overCommitButton;
        private int placementSize = 1;
        private bool placingReinforcements;
        private CustomSelfDrawPanel.CSDButton saveButton = new CustomSelfDrawPanel.CSDButton();
        private SaveFileDialog SaveCampDialog;
        private int sst_lastArchers;
        private int sst_lastCaptains;
        private int sst_lastPeasants;
        private int sst_lastPikeman;
        private int sst_lastSwordsman;
        private int targetCastlePlaceHeight;
        private int targetUtilHeight;
        private CustomSelfDrawPanel.CSDButton utilAdvancedButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton utilDeleteConstructingButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel utilDeleteConstructingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel utilHeaderLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel utilHeaderLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel utilHeaderLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage utilHeaderPanelImage = new CustomSelfDrawPanel.CSDImage();
        private const int UTILPANEL_WINDOW_OPENSIZE = 0xbf;
        private const int UTILPANEL_WINDOW_SIZE = 0x16e;
        private CustomSelfDrawPanel.CSDImage utilPanelFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage utilPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton utilRepairButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel utilRepairLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton utilViewModeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel utilViewModeLabel = new CustomSelfDrawPanel.CSDLabel();
        private const int WINDOWS_EXPAND_SPEED = 50;

        public CastleMapPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void addCastleIcon(int pieceType, BaseImage newImage, BaseImage overImage)
        {
            bool flag = true;
            bool flag2 = false;
            int num = pieceType;
            switch (num)
            {
                case 0x41:
                    num = 0x22;
                    break;

                case 0x42:
                    num = 0x21;
                    break;
            }
            if (this.canPlaceCastleItem(num))
            {
                flag2 = true;
            }
            if (((pieceType == 0x4b) && GameEngine.Instance.Castle.InBuilderMode) && !CastleMap.CreateMode)
            {
                flag2 = false;
            }
            if (flag2)
            {
                if ((((pieceType == 0x2c) || (pieceType == 0x29)) || ((pieceType == 0x2a) || (pieceType == 0x1f))) || ((pieceType == 0x23) || (pieceType == 0x4b)))
                {
                    int num2 = 0;
                    int num3 = 0;
                    if (pieceType == 0x2c)
                    {
                        num3 = GameEngine.Instance.Castle.countBombards();
                        num2 = GameEngine.Instance.Village.m_parishCapitalResearchData.Research_Leadership;
                    }
                    else if (pieceType == 0x29)
                    {
                        num3 = GameEngine.Instance.Castle.countTurrets();
                        num2 = GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Turrets;
                    }
                    else if (pieceType == 0x2a)
                    {
                        num3 = GameEngine.Instance.Castle.countBallistas();
                        num2 = GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Ballista;
                    }
                    else if (pieceType == 0x1f)
                    {
                        num3 = GameEngine.Instance.Castle.countGuardHouses();
                        num2 = 400 / GameEngine.Instance.LocalWorldData.castle_troopsPerGuardHouse;
                        if (!GameEngine.Instance.World.isCapital(GameEngine.Instance.Village.VillageID))
                        {
                            num2 -= 2;
                        }
                        else
                        {
                            num2 -= 5;
                        }
                    }
                    else if (pieceType == 0x23)
                    {
                        num3 = GameEngine.Instance.Castle.countMoat();
                        num2 = GameEngine.Instance.LocalWorldData.Castle_Max_Moat_Tiles;
                    }
                    else if (pieceType == 0x4b)
                    {
                        num3 = GameEngine.Instance.Castle.countPlacedOilPots();
                        num2 = GameEngine.Instance.LocalWorldData.castle_oilPerSmelter * GameEngine.Instance.Castle.countCompletedSmelters();
                    }
                    if (num2 <= num3)
                    {
                        flag = false;
                    }
                    else if ((num2 - num3) > 1)
                    {
                        CustomSelfDrawPanel.CSDImage image = null;
                        CustomSelfDrawPanel.CSDLabel label = null;
                        switch (this.currentCastleIcon)
                        {
                            case 0:
                                image = this.building1Image;
                                label = this.building1Label;
                                break;

                            case 1:
                                image = this.building2Image;
                                label = this.building2Label;
                                break;

                            case 2:
                                image = this.building3Image;
                                label = this.building3Label;
                                break;

                            case 3:
                                image = this.building4Image;
                                label = this.building4Label;
                                break;

                            case 4:
                                image = this.building5Image;
                                label = this.building5Label;
                                break;

                            case 5:
                                image = this.building6Image;
                                label = this.building6Label;
                                break;

                            case 6:
                                image = this.building7Image;
                                label = this.building7Label;
                                break;

                            case 7:
                                image = this.building8Image;
                                label = this.building8Label;
                                break;
                        }
                        if (image != null)
                        {
                            image.Visible = true;
                        }
                        if (label != null)
                        {
                            label.Text = (num2 - num3).ToString();
                        }
                    }
                }
                CustomSelfDrawPanel.CSDButton button = null;
                switch (this.currentCastleIcon)
                {
                    case 0:
                        button = this.castlePlace1Button;
                        break;

                    case 1:
                        button = this.castlePlace2Button;
                        break;

                    case 2:
                        button = this.castlePlace3Button;
                        break;

                    case 3:
                        button = this.castlePlace4Button;
                        break;

                    case 4:
                        button = this.castlePlace5Button;
                        break;

                    case 5:
                        button = this.castlePlace6Button;
                        break;

                    case 6:
                        button = this.castlePlace7Button;
                        break;

                    case 7:
                        button = this.castlePlace8Button;
                        break;

                    default:
                        return;
                }
                if (button != null)
                {
                    button.ImageNorm = (Image) newImage;
                    button.ImageOver = (Image) overImage;
                    button.Visible = true;
                    button.Enabled = flag;
                    button.Data = pieceType;
                    button.CustomTooltipID = 200;
                    button.CustomTooltipData = pieceType;
                }
                this.currentCastleIcon++;
            }
        }

        private void aggressiveStateClick()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                bool state = false;
                switch (data)
                {
                    case 70:
                        state = this.nextPeasantState;
                        break;

                    case 0x47:
                        state = this.nextSwordsmanState;
                        break;

                    case 0x48:
                        state = this.nextArcherState;
                        break;

                    case 0x49:
                        state = this.nextPikemanState;
                        break;

                    case 0x55:
                        state = this.nextCaptainState;
                        break;
                }
                GameEngine.Instance.Castle.setTroopAggressiveMode(data, state);
            }
        }

        private int calcDeleteBarYPos()
        {
            int num = 0x19 + this.currentCastlePlaceHeight;
            if (num < 0x37)
            {
                num = 0x37;
            }
            return num;
        }

        private int calcUtilBarYPos()
        {
            int num = this.calcDeleteBarYPos();
            if (this.commitBuildPanelImage.Visible)
            {
                num += 120;
            }
            return num;
        }

        public void cancelBuildings()
        {
            if (!CastleMap.CreateMode)
            {
                if (GameEngine.Instance.Castle != null)
                {
                    GameEngine.Instance.Castle.cancelBuilderMode();
                }
                this.commitBuildPanelImage.Visible = false;
                commitButtonVisible = false;
                this.utilAdvancedButton.Enabled = true;
                this.setCastlePlaceTab(this.currentCastlePlaceTab);
            }
        }

        public bool canPlaceCastleItem(int pieceType)
        {
            if (CastleMap.CreateMode)
            {
                return true;
            }
            ResearchData researchDataForCurrentVillage = GameEngine.Instance.World.GetResearchDataForCurrentVillage();
            int num = researchDataForCurrentVillage.Research_Fortification;
            int num2 = researchDataForCurrentVillage.Research_Defences;
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            switch (pieceType)
            {
                case 11:
                    if (num < 4)
                    {
                        break;
                    }
                    return true;

                case 12:
                    if (num < 5)
                    {
                        break;
                    }
                    return true;

                case 13:
                    if (num < 7)
                    {
                        break;
                    }
                    return true;

                case 14:
                    if (num < 8)
                    {
                        break;
                    }
                    return true;

                case 0x15:
                    if (num < 2)
                    {
                        break;
                    }
                    return true;

                case 0x1f:
                    if (num2 < 1)
                    {
                        break;
                    }
                    return true;

                case 0x20:
                    if (num2 < 5)
                    {
                        break;
                    }
                    return true;

                case 0x21:
                    if (num < 1)
                    {
                        break;
                    }
                    return true;

                case 0x22:
                    if (num < 3)
                    {
                        break;
                    }
                    return true;

                case 0x23:
                    if (num2 < 7)
                    {
                        break;
                    }
                    return true;

                case 0x24:
                    if (num2 < 2)
                    {
                        break;
                    }
                    return true;

                case 0x25:
                case 0x26:
                    if (num < 6)
                    {
                        break;
                    }
                    return true;

                case 0x27:
                case 40:
                    if (num < 1)
                    {
                        break;
                    }
                    return true;

                case 0x29:
                    return (((GameEngine.Instance.Village == null) || (GameEngine.Instance.Village.m_parishCapitalResearchData == null)) || (GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Turrets > 0));

                case 0x2a:
                    return (((GameEngine.Instance.Village == null) || (GameEngine.Instance.Village.m_parishCapitalResearchData == null)) || (GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Ballista > 0));

                case 0x2b:
                    return (((GameEngine.Instance.Village == null) || (GameEngine.Instance.Village.m_parishCapitalResearchData == null)) || (GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Tunnellors > 0));

                case 0x2c:
                    return (((GameEngine.Instance.Village == null) || (GameEngine.Instance.Village.m_parishCapitalResearchData == null)) || (GameEngine.Instance.Village.m_parishCapitalResearchData.Research_Leadership > 0));

                case 70:
                    return true;

                case 0x47:
                    return true;

                case 0x48:
                    return true;

                case 0x49:
                    return true;

                case 0x4b:
                    if (num2 < 5)
                    {
                        break;
                    }
                    return true;

                case 0x55:
                    if (researchDataForCurrentVillage.Research_Captains <= 0)
                    {
                        break;
                    }
                    return true;
            }
            return false;
        }

        public void castleCommitReturn()
        {
            this.commitBuildCommitButton.Enabled = true;
            this.lastCommitClick = DateTime.MinValue;
            this.controlBlockOverlay.Visible = false;
        }

        private void castleElementDeleteToggle()
        {
            this.deleteState = !this.deleteState;
            if (this.deleteState)
            {
                if ((GameEngine.Instance.Castle != null) && this.allowDeleteCallback)
                {
                    GameEngine.Instance.Castle.startDeleting();
                }
                this.deleteHeaderButton.CustomTooltipID = 0xd6;
                this.deleteHeaderButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_deletemode_on_normal;
                this.deleteHeaderButton.ImageOver = (Image) GFXLibrary.r_building_miltary_deletemode_on_over;
                this.deleteHeaderLabel.Text = SK.Text("CastleMapPanel_On", "On");
                this.deleteState = true;
            }
            else
            {
                if ((GameEngine.Instance.Castle != null) && this.allowDeleteCallback)
                {
                    GameEngine.Instance.Castle.stopPlaceElement();
                }
                this.deleteHeaderButton.CustomTooltipID = 0xd5;
                this.deleteHeaderButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_deletemode_off_normal;
                this.deleteHeaderButton.ImageOver = (Image) GFXLibrary.r_building_miltary_deletemode_off_over;
                this.deleteHeaderLabel.Text = SK.Text("CastleMapPanel_Off", "Off");
                this.deleteState = false;
            }
        }

        public void castleEndBuilderMode()
        {
            this.CastlePanelUpdate();
            this.setCastlePlaceTab(this.currentCastlePlaceTab);
        }

        private void CastlePanelUpdate()
        {
            bool flag = false;
            if (this.commitBuildPanelImage.Visible)
            {
                if (((GameEngine.Instance.Castle != null) && !GameEngine.Instance.Castle.InBuilderMode) && !GameEngine.Instance.Castle.InTroopPlacerMode)
                {
                    commitButtonVisible = false;
                    this.commitBuildPanelImage.Visible = false;
                    this.utilAdvancedButton.Enabled = true;
                    flag = true;
                }
                this.updateCommitValues();
            }
            if (this.currentCastlePlaceHeight != this.targetCastlePlaceHeight)
            {
                if (this.currentCastlePlaceHeight < this.targetCastlePlaceHeight)
                {
                    this.currentCastlePlaceHeight += 50;
                    if (this.currentCastlePlaceHeight > this.targetCastlePlaceHeight)
                    {
                        this.currentCastlePlaceHeight = this.targetCastlePlaceHeight;
                    }
                }
                else
                {
                    this.currentCastlePlaceHeight -= 50;
                    if (this.currentCastlePlaceHeight <= this.targetCastlePlaceHeight)
                    {
                        this.currentCastlePlaceHeight = this.targetCastlePlaceHeight;
                        this.setCastlePlaceTab(-1);
                    }
                }
                this.castlePlacePanelImage.Y = 0x19 - (0x1a6 - this.currentCastlePlaceHeight);
                this.castlePlacePanelImage.ClipRect = new Rectangle(0, 0x1a6 - this.currentCastlePlaceHeight, this.castlePlacePanelImage.Width, this.currentCastlePlaceHeight);
                flag = true;
                float num = ((((float) this.currentCastlePlaceHeight) / 422f) * 2f) - 1f;
                if (num < 0f)
                {
                    num = 0f;
                }
                this.castlePlacePanelFaderImage.Alpha = 1f - num;
            }
            if (this.currentCastlePlaceHeight == 0)
            {
                this.castlePlacePanelImage.Visible = false;
            }
            else
            {
                this.castlePlacePanelImage.Visible = true;
            }
            int num2 = this.calcDeleteBarYPos();
            this.commitBuildPanelImage.Y = num2;
            int num3 = this.calcUtilBarYPos();
            if (this.lastUtilYpos != num3)
            {
                flag = true;
            }
            this.lastUtilYpos = num3;
            this.utilHeaderPanelImage.Y = num3;
            if ((this.currentUtilHeight != this.targetUtilHeight) || flag)
            {
                if (this.currentUtilHeight < this.targetUtilHeight)
                {
                    this.currentUtilHeight += 50;
                    if (this.currentUtilHeight > this.targetUtilHeight)
                    {
                        this.currentUtilHeight = this.targetUtilHeight;
                    }
                }
                else
                {
                    this.currentUtilHeight -= 50;
                    if (this.currentUtilHeight <= this.targetUtilHeight)
                    {
                        this.currentUtilHeight = this.targetUtilHeight;
                    }
                }
                this.utilPanelImage.Y = (0x19 - (0x16e - this.currentUtilHeight)) + num3;
                this.utilPanelImage.ClipRect = new Rectangle(0, 0x16e - this.currentUtilHeight, this.utilPanelImage.Width, this.currentUtilHeight);
                flag = true;
                float num4 = ((((float) this.currentUtilHeight) / 366f) * 2f) - 1f;
                if (num4 < 0f)
                {
                    num4 = 0f;
                }
                this.utilPanelFaderImage.Alpha = 1f - num4;
            }
            if ((this.currentUtilHeight == 0) || (((GameEngine.Instance.Castle != null) && GameEngine.Instance.Castle.InTroopPlacerMode) && !CastleMap.CreateMode))
            {
                this.utilPanelImage.Visible = false;
            }
            else
            {
                this.utilPanelImage.Visible = true;
            }
            if (flag)
            {
                base.Invalidate();
            }
        }

        private void castlePlaceClick()
        {
            CustomSelfDrawPanel.CSDControl overControl = base.OverControl;
            if (overControl != null)
            {
                int data = overControl.Data;
                if (data < 0x45)
                {
                    if ((GameEngine.Instance.Castle != null) && GameEngine.Instance.Castle.startPlaceElement(data))
                    {
                        if (data == 0x41)
                        {
                            data = 0x22;
                        }
                        else if (data == 0x42)
                        {
                            data = 0x21;
                        }
                        GameEngine.Instance.Castle.startPlaceElement_ShowPanel(data, CastlesCommon.getPieceName(data), false);
                        this.buildingBeingPlaced = true;
                    }
                }
                else if ((data < 110) && (GameEngine.Instance.Castle != null))
                {
                    GameEngine.Instance.Castle.startPlacingTroops(data, this.placingReinforcements);
                    GameEngine.Instance.Castle.startPlaceElement_ShowPanel(data, CastlesCommon.getPieceName(data), false);
                    this.buildingBeingPlaced = true;
                }
            }
        }

        private void castlePlaceMouseLeave()
        {
            if (!this.buildingBeingPlaced)
            {
                this.clearCastlePlaceInfo();
            }
        }

        private void castlePlaceMouseOver()
        {
            if (!this.buildingBeingPlaced && (base.OverControl != null))
            {
                int data = base.OverControl.Data;
                if ((data < 110) && (GameEngine.Instance.Castle != null))
                {
                    GameEngine.Instance.Castle.startPlaceElement_ShowPanel(data, CastlesCommon.getPieceName(data), true);
                }
            }
        }

        private void castlePlaceSizeClick()
        {
            CustomSelfDrawPanel.CSDControl overControl = base.OverControl;
            if (overControl != null)
            {
                int data = overControl.Data;
                this.setPlaceSize(data);
            }
        }

        private void castlePlaceTab1Clicked()
        {
            if (this.currentCastlePlaceHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_open");
                this.openCastlePlaceTab();
            }
            else
            {
                if ((this.currentCastlePlaceTab == 0) || (this.currentCastlePlaceTab >= 0x3e8))
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_close");
                    this.closeCastlePlacePanel();
                    return;
                }
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_clicked");
            }
            this.setCastlePlaceTab(0);
        }

        private void castlePlaceTab2Clicked()
        {
            if (this.currentCastlePlaceHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_open");
                this.openCastlePlaceTab();
            }
            else
            {
                if (this.currentCastlePlaceTab == 1)
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_close");
                    this.closeCastlePlacePanel();
                    return;
                }
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_clicked");
            }
            this.setCastlePlaceTab(1);
        }

        private void castlePlaceTab3Clicked()
        {
            if (this.currentCastlePlaceHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_open");
                this.openCastlePlaceTab();
            }
            else
            {
                if (this.currentCastlePlaceTab == 2)
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_close");
                    this.closeCastlePlacePanel();
                    return;
                }
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_clicked");
            }
            this.setCastlePlaceTab(2);
        }

        private void castlePlaceTab4Clicked()
        {
            if (this.currentCastlePlaceHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_open");
                this.openCastlePlaceTab();
            }
            else
            {
                if (this.currentCastlePlaceTab == 3)
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_close");
                    this.closeCastlePlacePanel();
                    return;
                }
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_clicked");
            }
            this.setCastlePlaceTab(3);
        }

        private void castlePlaceTab5Clicked()
        {
            if (this.currentCastlePlaceHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_open");
                this.openCastlePlaceTab();
            }
            else
            {
                if (this.currentCastlePlaceTab == 4)
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_close");
                    this.closeCastlePlacePanel();
                    return;
                }
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_tab_clicked");
            }
            this.setCastlePlaceTab(4);
        }

        public void castleStartBuilderMode()
        {
            if (!CastleMap.CreateMode && (GameEngine.Instance.Castle != null))
            {
                if (GameEngine.Instance.Castle.InBuilderMode)
                {
                    this.commitBuildWoodImage.Visible = true;
                    this.commitBuildWoodLabel.Visible = true;
                    this.commitBuildStoneImage.Visible = true;
                    this.commitBuildStoneLabel.Visible = true;
                    this.commitBuildPitchImage.Visible = true;
                    this.commitBuildPitchLabel.Visible = true;
                    this.commitBuildIronImage.Visible = true;
                    this.commitBuildIronLabel.Visible = true;
                    this.commitBuildGoldImage.Visible = true;
                    this.commitBuildGoldLabel.Visible = true;
                    this.commitBuildTimeImage.Visible = true;
                    this.commitBuildTimeLabel.Visible = true;
                }
                if (GameEngine.Instance.Castle.InTroopPlacerMode)
                {
                    this.commitBuildWoodImage.Visible = false;
                    this.commitBuildWoodLabel.Visible = false;
                    this.commitBuildStoneImage.Visible = false;
                    this.commitBuildStoneLabel.Visible = false;
                    this.commitBuildPitchImage.Visible = false;
                    this.commitBuildPitchLabel.Visible = false;
                    this.commitBuildIronImage.Visible = false;
                    this.commitBuildIronLabel.Visible = false;
                    this.commitBuildGoldImage.Visible = false;
                    this.commitBuildGoldLabel.Visible = false;
                    this.commitBuildTimeImage.Visible = false;
                    this.commitBuildTimeLabel.Visible = false;
                    if (this.currentCastlePlaceTab < 0)
                    {
                        this.castlePlaceTab1Clicked();
                    }
                }
                this.commitBuildPanelImage.Visible = true;
                commitButtonVisible = true;
                this.utilAdvancedButton.Enabled = false;
                this.setCastlePlaceTab(this.currentCastlePlaceTab);
            }
        }

        public void clearCastlePlaceInfo()
        {
            this.buildingBeingPlaced = false;
            this.castlePlaceTypeLabel.Text = SK.Text("CastleMapPanel_Click_To_place", "Click Icons above to place");
            this.castlePlaceTimeLabel.Visible = false;
            this.castlePlaceWoodLabel.Visible = false;
            this.castlePlacePitchLabel.Visible = false;
            this.castlePlaceIronLabel.Visible = false;
            this.castlePlaceStoneLabel.Visible = false;
            this.castlePlaceGoldLabel.Visible = false;
            this.castlePlaceTimeImage.Visible = false;
            this.castlePlaceWoodImage.Visible = false;
            this.castlePlaceStoneImage.Visible = false;
            this.castlePlaceIronImage.Visible = false;
            this.castlePlacePitchImage.Visible = false;
            this.castlePlaceGoldImage.Visible = false;
        }

        public void clearSelectedTroop()
        {
            this.captain_castleSelectionBackgroundArea.Visible = false;
            this.castleSelectionBackgroundArea.Visible = false;
            this.castlePlaceBackgroundArea.Visible = true;
            base.Invalidate();
        }

        private void closeCastlePlacePanel()
        {
            this.targetCastlePlaceHeight = 0;
        }

        private void closeClick()
        {
            GameEngine.Instance.Castle.clearLasso();
            if (!this.commitBuildPanelImage.Visible)
            {
                GameEngine.Instance.Castle.cancelBuilderMode();
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void closeutilPanel()
        {
            this.utilHeaderPanelImage.CustomTooltipID = 0xd7;
            this.targetUtilHeight = 0;
        }

        public void commitBuildings()
        {
            if (!CastleMap.CreateMode && (GameEngine.Instance.Castle != null))
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.lastCommitClick);
                if (span.TotalSeconds > 10.0)
                {
                    this.commitBuildCommitButton.Enabled = false;
                    this.lastCommitClick = DateTime.Now;
                    this.controlBlockOverlay.Visible = true;
                    GameEngine.Instance.Castle.commitCastle();
                }
            }
        }

        private void commitBuildingsLeave()
        {
            this.overCommitButton = false;
        }

        private void commitBuildingsOver()
        {
            this.overCommitButton = true;
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void create()
        {
            this.initCastlePlacePanel();
            this.initCommitBuildPanel();
            this.initUtilBar();
            this.initSelectionPanel();
            this.initSelectionPanel_Captains();
            this.controlBlockOverlay.Position = new Point(0, 0);
            this.controlBlockOverlay.Size = base.Size;
            this.controlBlockOverlay.FillColor = Color.FromArgb(0, ARGBColors.Black);
            this.controlBlockOverlay.Visible = false;
            this.controlBlockOverlay.setClickDelegate(delegate {
            });
            this.controlBlockOverlay.setMouseOverDelegate(delegate {
            }, delegate {
            });
            base.addControl(this.controlBlockOverlay);
        }

        private void DEBUG_load()
        {
            if ((this.LoadCampDialog.ShowDialog() == DialogResult.OK) && (GameEngine.Instance.Castle != null))
            {
                GameEngine.Instance.Castle.loadCamp(this.LoadCampDialog.FileName);
            }
        }

        private void DEBUG_save()
        {
            if ((this.SaveCampDialog.ShowDialog() == DialogResult.OK) && (GameEngine.Instance.Castle != null))
            {
                GameEngine.Instance.Castle.saveCamp(this.SaveCampDialog.FileName);
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

        public void init()
        {
            this.controlBlockOverlay.Size = base.Size;
            this.controlBlockOverlay.Visible = false;
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            if (GameEngine.Instance.World.isCapital(villageID) && GameEngine.Instance.World.isUserRelatedVillage(villageID))
            {
                this.castlePlaceBackgroundArea.Visible = false;
            }
            else
            {
                this.castlePlaceBackgroundArea.Visible = true;
            }
            this.stopDeleting();
            this.targetCastlePlaceHeight = 0;
            this.currentCastlePlaceHeight = 1;
            this.setCastlePlaceTab(-1);
            this.setPlaceSize(1);
            this.setReinforcementsMode(false);
        }

        public void initCastlePlacePanel()
        {
            int y = 0;
            this.castlePlaceBackgroundArea.Position = new Point(0, 0);
            this.castlePlaceBackgroundArea.Size = base.Size;
            base.addControl(this.castlePlaceBackgroundArea);
            this.castlePlacePanelImage.Image = (Image) GFXLibrary.r_building_panel_back;
            this.castlePlacePanelImage.Position = new Point(0, y + 0x19);
            this.castlePlaceBackgroundArea.addControl(this.castlePlacePanelImage);
            this.castlePlaceHeaderArea.Position = new Point(0, y);
            this.castlePlaceHeaderArea.Size = new Size(0xc4, 0x3e);
            this.castlePlaceBackgroundArea.addControl(this.castlePlaceHeaderArea);
            this.castlePlace1Button.Position = new Point(6, 14);
            this.castlePlace1Button.Visible = false;
            this.castlePlace1Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace1Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace1Button);
            this.building1Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building1Image.Alpha = 0.65f;
            this.building1Image.Position = new Point(0x40, 0x3b);
            this.castlePlace1Button.addControl(this.building1Image);
            this.building1Label.Text = "0";
            this.building1Label.Color = ARGBColors.Black;
            this.building1Label.Position = new Point(-1, -1);
            this.building1Label.Size = this.building1Image.Size;
            this.building1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building1Image.addControl(this.building1Label);
            this.castlePlace2Button.Position = new Point(0x58, 14);
            this.castlePlace2Button.Visible = false;
            this.castlePlace2Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace2Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace2Button);
            this.building2Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building2Image.Alpha = 0.65f;
            this.building2Image.Position = new Point(0x40, 0x3b);
            this.castlePlace2Button.addControl(this.building2Image);
            this.building2Label.Text = "0";
            this.building2Label.Color = ARGBColors.Black;
            this.building2Label.Position = new Point(-1, -1);
            this.building2Label.Size = this.building1Image.Size;
            this.building2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building2Image.addControl(this.building2Label);
            this.castlePlace3Button.Position = new Point(6, 0x59);
            this.castlePlace3Button.Visible = false;
            this.castlePlace3Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace3Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace3Button);
            this.building3Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building3Image.Alpha = 0.65f;
            this.building3Image.Position = new Point(0x40, 0x3b);
            this.castlePlace3Button.addControl(this.building3Image);
            this.building3Label.Text = "0";
            this.building3Label.Color = ARGBColors.Black;
            this.building3Label.Position = new Point(-1, -1);
            this.building3Label.Size = this.building1Image.Size;
            this.building3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building3Image.addControl(this.building3Label);
            this.castlePlace4Button.Position = new Point(0x58, 0x59);
            this.castlePlace4Button.Visible = false;
            this.castlePlace4Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace4Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace4Button);
            this.building4Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building4Image.Alpha = 0.65f;
            this.building4Image.Position = new Point(0x40, 0x3b);
            this.castlePlace4Button.addControl(this.building4Image);
            this.building4Label.Text = "0";
            this.building4Label.Color = ARGBColors.Black;
            this.building4Label.Position = new Point(-1, -1);
            this.building4Label.Size = this.building1Image.Size;
            this.building4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building4Image.addControl(this.building4Label);
            this.castlePlace5Button.Position = new Point(6, 0xa4);
            this.castlePlace5Button.Visible = false;
            this.castlePlace5Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace5Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace5Button);
            this.building5Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building5Image.Alpha = 0.65f;
            this.building5Image.Position = new Point(0x40, 0x3b);
            this.castlePlace5Button.addControl(this.building5Image);
            this.building5Label.Text = "0";
            this.building5Label.Color = ARGBColors.Black;
            this.building5Label.Position = new Point(-1, -1);
            this.building5Label.Size = this.building1Image.Size;
            this.building5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building5Image.addControl(this.building5Label);
            this.castlePlace6Button.Position = new Point(0x58, 0xa4);
            this.castlePlace6Button.Visible = false;
            this.castlePlace6Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace6Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace6Button);
            this.building6Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building6Image.Alpha = 0.65f;
            this.building6Image.Position = new Point(0x40, 0x3b);
            this.castlePlace6Button.addControl(this.building6Image);
            this.building6Label.Text = "0";
            this.building6Label.Color = ARGBColors.Black;
            this.building6Label.Position = new Point(-1, -1);
            this.building6Label.Size = this.building1Image.Size;
            this.building6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building6Image.addControl(this.building6Label);
            this.castlePlace7Button.Position = new Point(6, 0xef);
            this.castlePlace7Button.Visible = false;
            this.castlePlace7Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace7Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace7Button);
            this.building7Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building7Image.Alpha = 0.65f;
            this.building7Image.Position = new Point(0x40, 0x3b);
            this.castlePlace7Button.addControl(this.building7Image);
            this.building7Label.Text = "0";
            this.building7Label.Color = ARGBColors.Black;
            this.building7Label.Position = new Point(-1, -1);
            this.building7Label.Size = this.building1Image.Size;
            this.building7Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building7Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building7Image.addControl(this.building7Label);
            this.castlePlace8Button.Position = new Point(0x58, 0xef);
            this.castlePlace8Button.Visible = false;
            this.castlePlace8Button.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlace8Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_generic_place");
            this.castlePlace8Button.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlace8Button);
            this.building8Image.Image = (Image) GFXLibrary.building_icon_circle;
            this.building8Image.Alpha = 0.65f;
            this.building8Image.Position = new Point(0x40, 0x3b);
            this.castlePlace8Button.addControl(this.building8Image);
            this.building8Label.Text = "0";
            this.building8Label.Color = ARGBColors.Black;
            this.building8Label.Position = new Point(-1, -1);
            this.building8Label.Size = this.building1Image.Size;
            this.building8Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.building8Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.building8Image.addControl(this.building8Label);
            this.castlePlacePeasantButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_peasent;
            this.castlePlacePeasantButton.ImageOver = (Image) GFXLibrary.r_building_miltary_peasent_over;
            this.castlePlacePeasantButton.Position = new Point(-9, 14);
            this.castlePlacePeasantButton.Visible = false;
            this.castlePlacePeasantButton.Data = 70;
            this.castlePlacePeasantButton.CustomTooltipID = 200;
            this.castlePlacePeasantButton.CustomTooltipData = 70;
            this.castlePlacePeasantButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlacePeasantButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_place_peasant");
            this.castlePlacePeasantButton.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlacePeasantButton);
            this.castlePlacePeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlacePeasantInset.Position = new Point(0x37, 0x41);
            this.castlePlacePeasantButton.addControl(this.castlePlacePeasantInset);
            this.castlePlacePeasantLabel.Text = "0";
            this.castlePlacePeasantLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlacePeasantLabel.Position = new Point(0, -1);
            this.castlePlacePeasantLabel.Size = this.castlePlacePeasantInset.Size;
            this.castlePlacePeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlacePeasantInset.addControl(this.castlePlacePeasantLabel);
            this.castlePlaceArcherButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_archer;
            this.castlePlaceArcherButton.ImageOver = (Image) GFXLibrary.r_building_miltary_archer_over;
            this.castlePlaceArcherButton.Position = new Point(0x49, 14);
            this.castlePlaceArcherButton.Visible = false;
            this.castlePlaceArcherButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceArcherButton.Data = 0x48;
            this.castlePlaceArcherButton.CustomTooltipID = 200;
            this.castlePlaceArcherButton.CustomTooltipData = 0x48;
            this.castlePlaceArcherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_place_archer");
            this.castlePlaceArcherButton.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlaceArcherButton);
            this.castlePlaceArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceArcherInset.Position = new Point(0x37, 0x41);
            this.castlePlaceArcherButton.addControl(this.castlePlaceArcherInset);
            this.castlePlaceArcherLabel.Text = "0";
            this.castlePlaceArcherLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceArcherLabel.Position = new Point(0, -1);
            this.castlePlaceArcherLabel.Size = this.castlePlaceArcherInset.Size;
            this.castlePlaceArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlaceArcherInset.addControl(this.castlePlaceArcherLabel);
            this.castlePlacePikemanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen;
            this.castlePlacePikemanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen_over;
            this.castlePlacePikemanButton.Position = new Point(-9, 0x59);
            this.castlePlacePikemanButton.Visible = false;
            this.castlePlacePikemanButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlacePikemanButton.Data = 0x49;
            this.castlePlacePikemanButton.CustomTooltipID = 200;
            this.castlePlacePikemanButton.CustomTooltipData = 0x49;
            this.castlePlacePikemanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_place_pikemen");
            this.castlePlacePikemanButton.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlacePikemanButton);
            this.castlePlacePikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlacePikemanInset.Position = new Point(0x37, 0x41);
            this.castlePlacePikemanButton.addControl(this.castlePlacePikemanInset);
            this.castlePlacePikemanLabel.Text = "0";
            this.castlePlacePikemanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlacePikemanLabel.Position = new Point(0, -1);
            this.castlePlacePikemanLabel.Size = this.castlePlacePikemanInset.Size;
            this.castlePlacePikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlacePikemanInset.addControl(this.castlePlacePikemanLabel);
            this.castlePlaceSwordsmanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_swordsman;
            this.castlePlaceSwordsmanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_swordsman_over;
            this.castlePlaceSwordsmanButton.Position = new Point(0x49, 0x59);
            this.castlePlaceSwordsmanButton.Visible = false;
            this.castlePlaceSwordsmanButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceSwordsmanButton.Data = 0x47;
            this.castlePlaceSwordsmanButton.CustomTooltipID = 200;
            this.castlePlaceSwordsmanButton.CustomTooltipData = 0x47;
            this.castlePlaceSwordsmanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_place_swordsmen");
            this.castlePlaceSwordsmanButton.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlaceSwordsmanButton);
            this.castlePlaceSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceSwordsmanInset.Position = new Point(0x37, 0x41);
            this.castlePlaceSwordsmanButton.addControl(this.castlePlaceSwordsmanInset);
            this.castlePlaceSwordsmanLabel.Text = "0";
            this.castlePlaceSwordsmanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceSwordsmanLabel.Position = new Point(0, -1);
            this.castlePlaceSwordsmanLabel.Size = this.castlePlaceSwordsmanInset.Size;
            this.castlePlaceSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlaceSwordsmanInset.addControl(this.castlePlaceSwordsmanLabel);
            this.castlePlaceWolfButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen;
            this.castlePlaceWolfButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen_over;
            this.castlePlaceWolfButton.Position = new Point(-9, 0xa4);
            this.castlePlaceWolfButton.Visible = false;
            this.castlePlaceWolfButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceWolfButton.Data = 0x4d;
            this.castlePlaceWolfButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapPanel_place_captain");
            this.castlePlaceWolfButton.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.castlePlaceMouseLeave));
            this.castlePlacePanelImage.addControl(this.castlePlaceWolfButton);
            this.castlePlaceWolfInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceWolfInset.Position = new Point(0x37, 0x41);
            this.castlePlaceWolfButton.addControl(this.castlePlaceWolfInset);
            this.castlePlaceWolfLabel.Text = "1000";
            this.castlePlaceWolfLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceWolfLabel.Position = new Point(0, -1);
            this.castlePlaceWolfLabel.Size = this.castlePlacePikemanInset.Size;
            this.castlePlaceWolfLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlaceWolfInset.addControl(this.castlePlaceWolfLabel);
            if (!CastleMap.CreateMode)
            {
                this.castlePlaceWolfButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_captain_normal;
                this.castlePlaceWolfButton.ImageOver = (Image) GFXLibrary.r_building_miltary_captain_over;
                this.castlePlaceWolfButton.Data = 0x55;
                this.castlePlaceWolfButton.CustomTooltipID = 200;
                this.castlePlaceWolfButton.CustomTooltipData = 0x55;
            }
            this.castlePlaceToggleReinforcementsButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_flag_normal;
            this.castlePlaceToggleReinforcementsButton.ImageOver = (Image) GFXLibrary.r_building_miltary_flag_over;
            this.castlePlaceToggleReinforcementsButton.Position = new Point(0x58, 0xae);
            this.castlePlaceToggleReinforcementsButton.Visible = false;
            this.castlePlaceToggleReinforcementsButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceToggleReinforcementsButton.CustomTooltipID = 0xcd;
            this.castlePlaceToggleReinforcementsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.reinforcementsClick), "CastleMapPanel_reinforcements_toggle");
            this.castlePlacePanelImage.addControl(this.castlePlaceToggleReinforcementsButton);
            this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
            this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
            this.castlePlaceSize1Button.Position = new Point(0x1a, 0x11d);
            this.castlePlaceSize1Button.Visible = false;
            this.castlePlaceSize1Button.Data = 1;
            this.castlePlaceSize1Button.CustomTooltipID = 0xcf;
            this.castlePlaceSize1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapPanel_placement_1x1");
            this.castlePlacePanelImage.addControl(this.castlePlaceSize1Button);
            this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
            this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
            this.castlePlaceSize3Button.Position = new Point(0x40, 0x11d);
            this.castlePlaceSize3Button.Visible = false;
            this.castlePlaceSize3Button.Data = 3;
            this.castlePlaceSize3Button.CustomTooltipID = 0xd0;
            this.castlePlaceSize3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapPanel_placement_3x3");
            this.castlePlacePanelImage.addControl(this.castlePlaceSize3Button);
            this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
            this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
            this.castlePlaceSize5Button.Position = new Point(0x66, 0x11d);
            this.castlePlaceSize5Button.Visible = false;
            this.castlePlaceSize5Button.Data = 5;
            this.castlePlaceSize5Button.CustomTooltipID = 0xd1;
            this.castlePlaceSize5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapPanel_placement_5x5");
            this.castlePlacePanelImage.addControl(this.castlePlaceSize5Button);
            this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
            this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
            this.castlePlaceSize15Button.Position = new Point(140, 0x11d);
            this.castlePlaceSize15Button.Visible = false;
            this.castlePlaceSize15Button.Data = 15;
            this.castlePlaceSize15Button.CustomTooltipID = 210;
            this.castlePlaceSize15Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapPanel_placement_1x5");
            this.castlePlacePanelImage.addControl(this.castlePlaceSize15Button);
            this.castlePlaceTab1Button.Position = new Point(0, 0);
            this.castlePlaceTab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceTab1Clicked));
            this.castlePlaceTab1Button.CustomTooltipID = 0xc9;
            this.castlePlaceHeaderArea.addControl(this.castlePlaceTab1Button);
            this.castlePlaceTab2Button.Position = new Point(0x29, 0);
            this.castlePlaceTab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceTab2Clicked));
            this.castlePlaceTab2Button.CustomTooltipID = 0xca;
            this.castlePlaceHeaderArea.addControl(this.castlePlaceTab2Button);
            this.castlePlaceTab3Button.Position = new Point(0x4f, 0);
            this.castlePlaceTab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceTab3Clicked));
            this.castlePlaceTab3Button.CustomTooltipID = 0xcb;
            this.castlePlaceHeaderArea.addControl(this.castlePlaceTab3Button);
            this.castlePlaceTab4Button.Position = new Point(0x75, 0);
            this.castlePlaceTab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceTab4Clicked));
            this.castlePlaceTab4Button.CustomTooltipID = 0xcc;
            this.castlePlaceHeaderArea.addControl(this.castlePlaceTab4Button);
            this.castlePlaceTab5Button.Position = new Point(0x9b, 0);
            this.castlePlaceTab5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceTab5Clicked));
            this.castlePlaceTab5Button.CustomTooltipID = 0xdf;
            this.castlePlaceHeaderArea.addControl(this.castlePlaceTab5Button);
            this.castlePlaceInfoImage.Image = (Image) GFXLibrary.r_building_panel_inset_small;
            this.castlePlaceInfoImage.Position = new Point(12, 0x147);
            this.castlePlacePanelImage.addControl(this.castlePlaceInfoImage);
            this.castlePlaceTypeLabel.Text = SK.Text("CastleMapPanel_Click_To_place", "Click Icons above to place");
            this.castlePlaceTypeLabel.Color = ARGBColors.Black;
            this.castlePlaceTypeLabel.Position = new Point(13, 4);
            this.castlePlaceTypeLabel.Size = new Size(0x8d, 20);
            if (((Program.mySettings.LanguageIdent == "tr") || (Program.mySettings.LanguageIdent == "pl")) || ((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "pt")))
            {
                this.castlePlaceTypeLabel.Position = new Point(13, -8);
                this.castlePlaceTypeLabel.Size = new Size(0xa1, 40);
                this.castlePlaceTypeLabel.Font = FontManager.GetFont("Arial", 7.25f);
                this.castlePlaceTypeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            }
            else
            {
                this.castlePlaceTypeLabel.Size = new Size(0x8d, 20);
                this.castlePlaceTypeLabel.Font = FontManager.GetFont("Arial", 8.25f);
            }
            this.castlePlaceInfoImage.addControl(this.castlePlaceTypeLabel);
            this.castlePlaceTimeImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_time;
            this.castlePlaceTimeImage.Position = new Point(13, 0x16);
            this.castlePlaceTimeImage.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castlePlaceTimeImage);
            this.castlePlaceTimeLabel.Text = "";
            this.castlePlaceTimeLabel.Color = ARGBColors.Black;
            this.castlePlaceTimeLabel.Position = new Point(40, 0x1a);
            this.castlePlaceTimeLabel.Size = new Size(120, 20);
            this.castlePlaceTimeLabel.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castlePlaceTimeLabel);
            this.castlePlaceWoodImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_wood;
            this.castlePlaceWoodImage.Position = new Point(0x56, 0x16);
            this.castlePlaceInfoImage.addControl(this.castlePlaceWoodImage);
            this.castlePlaceWoodLabel.Text = "0";
            this.castlePlaceWoodLabel.Color = ARGBColors.Black;
            this.castlePlaceWoodLabel.Position = new Point(0x71, 0x1a);
            this.castlePlaceWoodLabel.Size = new Size(0x2e, 20);
            this.castlePlaceWoodLabel.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castlePlaceWoodLabel);
            this.castlePlaceStoneImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_stone;
            this.castlePlaceStoneImage.Position = new Point(0x56, 0x16);
            this.castlePlaceInfoImage.addControl(this.castlePlaceStoneImage);
            this.castlePlaceStoneLabel.Text = "0";
            this.castlePlaceStoneLabel.Color = ARGBColors.Black;
            this.castlePlaceStoneLabel.Position = new Point(0x71, 0x1a);
            this.castlePlaceStoneLabel.Size = new Size(0x2e, 20);
            this.castlePlaceStoneLabel.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castlePlaceStoneLabel);
            this.castlePlaceIronImage.Image = (Image) GFXLibrary.com_16_iron;
            this.castlePlaceIronImage.Position = new Point(0x56, 0x16);
            this.castlePlaceInfoImage.addControl(this.castlePlaceIronImage);
            this.castlePlaceIronLabel.Text = "0";
            this.castlePlaceIronLabel.Color = ARGBColors.Black;
            this.castlePlaceIronLabel.Position = new Point(0x71, 0x1a);
            this.castlePlaceIronLabel.Size = new Size(0x2e, 20);
            this.castlePlaceIronLabel.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castlePlaceIronLabel);
            this.castlePlacePitchImage.Image = (Image) GFXLibrary.com_16_pitch;
            this.castlePlacePitchImage.Position = new Point(0x56, 0x16);
            this.castlePlaceInfoImage.addControl(this.castlePlacePitchImage);
            this.castlePlacePitchLabel.Text = "0";
            this.castlePlacePitchLabel.Color = ARGBColors.Black;
            this.castlePlacePitchLabel.Position = new Point(0x71, 0x1a);
            this.castlePlacePitchLabel.Size = new Size(0x2e, 20);
            this.castlePlacePitchLabel.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castlePlacePitchLabel);
            this.castlePlaceGoldImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_gold;
            this.castlePlaceGoldImage.Position = new Point(0x56, 0x16);
            this.castlePlaceInfoImage.addControl(this.castlePlaceGoldImage);
            this.castlePlaceGoldLabel.Text = "0";
            this.castlePlaceGoldLabel.Color = ARGBColors.Black;
            this.castlePlaceGoldLabel.Position = new Point(0x71, 0x1a);
            this.castlePlaceGoldLabel.Size = new Size(0x2e, 20);
            this.castlePlaceGoldLabel.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castlePlaceGoldLabel);
            this.castleTotalGoldImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_gold;
            this.castleTotalGoldImage.Position = new Point(0x31, -18);
            this.castlePlaceInfoImage.addControl(this.castleTotalGoldImage);
            this.castleTotalGoldLabel.Text = "0";
            this.castleTotalGoldLabel.Color = ARGBColors.Black;
            this.castleTotalGoldLabel.Position = new Point(0x4c, -14);
            this.castleTotalGoldLabel.Size = new Size(0x56, 20);
            this.castleTotalGoldLabel.Visible = false;
            this.castlePlaceInfoImage.addControl(this.castleTotalGoldLabel);
            this.utilViewModeButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_viewmode_normal;
            this.utilViewModeButton.ImageOver = (Image) GFXLibrary.r_building_miltary_viewmode_over;
            this.utilViewModeButton.ImageClick = (Image) GFXLibrary.r_building_miltary_viewmode_pushed;
            this.utilViewModeButton.Position = new Point(2, 0x2c);
            if (CastleMap.displayCollapsed)
            {
                this.utilViewModeButton.CustomTooltipID = 0xd3;
            }
            else
            {
                this.utilViewModeButton.CustomTooltipID = 0xd4;
            }
            this.utilViewModeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilViewModeClick));
            this.castlePlaceInfoImage.addControl(this.utilViewModeButton);
            this.deleteHeaderButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_deletemode_off_normal;
            this.deleteHeaderButton.ImageOver = (Image) GFXLibrary.r_building_miltary_deletemode_off_over;
            this.deleteHeaderButton.Position = new Point(90, 0x2c);
            this.deleteHeaderButton.Text.Text = SK.Text("GENERIC_Delete", "Delete");
            this.deleteHeaderButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.deleteHeaderButton.Text.Position = new Point(6, -6);
            this.deleteHeaderButton.Text.Size = new Size(this.deleteHeaderButton.Width - 15, this.deleteHeaderButton.Height);
            this.deleteHeaderButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.deleteHeaderButton.TextYOffset = 0;
            this.deleteHeaderButton.Text.Color = ARGBColors.Black;
            this.deleteHeaderButton.CustomTooltipID = 0xd5;
            this.deleteHeaderButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleElementDeleteToggle), "CastleMapPanel_toggle_delete");
            this.castlePlaceInfoImage.addControl(this.deleteHeaderButton);
            this.deleteHeaderLabel.Text = SK.Text("CastleMapPanel_Off", "Off");
            this.deleteHeaderLabel.Color = ARGBColors.Black;
            this.deleteHeaderLabel.Position = new Point(0, 20);
            this.deleteHeaderLabel.Size = new Size(this.deleteHeaderButton.Text.Size.Width + 20, this.deleteHeaderButton.Text.Size.Height);
            this.deleteHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            if ((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "pt"))
            {
                this.deleteHeaderLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            }
            else
            {
                this.deleteHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            }
            this.deleteHeaderButton.addControl(this.deleteHeaderLabel);
            this.castlePlaceGuardhouseLabel.Text = "";
            this.castlePlaceGuardhouseLabel.Color = ARGBColors.Black;
            this.castlePlaceGuardhouseLabel.Visible = false;
            this.castlePlaceGuardhouseLabel.Position = new Point(10, 0x12);
            this.castlePlaceGuardhouseLabel.Size = new Size(160, 0x19);
            this.castlePlaceGuardhouseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.castlePlaceInfoImage.addControl(this.castlePlaceGuardhouseLabel);
            this.castlePlacePanelFaderImage.Image = (Image) GFXLibrary.r_building_panel_back;
            this.castlePlacePanelFaderImage.Position = new Point(0, 0);
            this.castlePlacePanelFaderImage.Alpha = 0f;
            this.castlePlacePanelImage.addControl(this.castlePlacePanelFaderImage);
            this.setCastlePlaceTab(-1);
        }

        public void initCommitBuildPanel()
        {
            int y = this.calcDeleteBarYPos();
            this.commitBuildPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_C;
            this.commitBuildPanelImage.Position = new Point(0, y);
            this.commitBuildPanelImage.Visible = false;
            commitButtonVisible = false;
            base.addControl(this.commitBuildPanelImage);
            this.commitBuildCommitImage.Image = (Image) GFXLibrary.int_but_industry_blank_norm;
            this.commitBuildCommitImage.Position = new Point(11, 0x52);
            this.commitBuildCommitImage.Alpha = 0.8f;
            this.commitBuildPanelImage.addControl(this.commitBuildCommitImage);
            this.overCommitButton = false;
            this.commitBuildCommitButton.ImageNorm = (Image) GFXLibrary.int_but_industry_blank_over;
            this.commitBuildCommitButton.ImageOver = (Image) GFXLibrary.int_but_industry_blank_over;
            this.commitBuildCommitButton.Position = new Point(11, 0x52);
            this.commitBuildCommitButton.Text.Text = SK.Text("CastleMapPanel_Confirm", "Confirm");
            this.commitBuildCommitButton.TextYOffset = -1;
            this.commitBuildCommitButton.Text.Color = ARGBColors.Black;
            this.commitBuildCommitButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.commitBuildings), "CastleMapPanel_confirm");
            this.commitBuildCommitButton.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.commitBuildingsOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.commitBuildingsLeave));
            this.commitBuildCommitButton.CustomTooltipID = 0xdb;
            this.commitBuildPanelImage.addControl(this.commitBuildCommitButton);
            this.commitBuildCancelButton.ImageNorm = (Image) GFXLibrary.int_but_industry_blank_norm;
            this.commitBuildCancelButton.ImageOver = (Image) GFXLibrary.int_but_industry_blank_over;
            this.commitBuildCancelButton.Position = new Point(0x63, 0x52);
            this.commitBuildCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.commitBuildCancelButton.TextYOffset = -1;
            this.commitBuildCancelButton.Text.Color = ARGBColors.Black;
            this.commitBuildCancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelBuildings), "CastleMapPanel_cancel");
            this.commitBuildCancelButton.CustomTooltipID = 220;
            this.commitBuildPanelImage.addControl(this.commitBuildCancelButton);
            this.commitBuildWoodImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_wood;
            this.commitBuildWoodImage.Position = new Point(13, 7);
            this.commitBuildPanelImage.addControl(this.commitBuildWoodImage);
            this.commitBuildWoodLabel.Text = "0";
            this.commitBuildWoodLabel.Color = ARGBColors.Black;
            this.commitBuildWoodLabel.Position = new Point(40, 11);
            this.commitBuildWoodLabel.Size = new Size(0x2e, 20);
            this.commitBuildPanelImage.addControl(this.commitBuildWoodLabel);
            this.commitBuildStoneImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_stone;
            this.commitBuildStoneImage.Position = new Point(13, 0x1a);
            this.commitBuildPanelImage.addControl(this.commitBuildStoneImage);
            this.commitBuildStoneLabel.Text = "0";
            this.commitBuildStoneLabel.Color = ARGBColors.Black;
            this.commitBuildStoneLabel.Position = new Point(40, 30);
            this.commitBuildStoneLabel.Size = new Size(0x2e, 20);
            this.commitBuildPanelImage.addControl(this.commitBuildStoneLabel);
            this.commitBuildIronImage.Image = (Image) GFXLibrary.com_16_iron;
            this.commitBuildIronImage.Position = new Point(0x6a, 7);
            this.commitBuildPanelImage.addControl(this.commitBuildIronImage);
            this.commitBuildIronLabel.Text = "0";
            this.commitBuildIronLabel.Color = ARGBColors.Black;
            this.commitBuildIronLabel.Position = new Point(0x85, 11);
            this.commitBuildIronLabel.Size = new Size(0x2e, 20);
            this.commitBuildPanelImage.addControl(this.commitBuildIronLabel);
            this.commitBuildPitchImage.Image = (Image) GFXLibrary.com_16_pitch;
            this.commitBuildPitchImage.Position = new Point(0x6a, 0x1a);
            this.commitBuildPanelImage.addControl(this.commitBuildPitchImage);
            this.commitBuildPitchLabel.Text = "0";
            this.commitBuildPitchLabel.Color = ARGBColors.Black;
            this.commitBuildPitchLabel.Position = new Point(0x85, 30);
            this.commitBuildPitchLabel.Size = new Size(0x2e, 20);
            this.commitBuildPanelImage.addControl(this.commitBuildPitchLabel);
            this.commitBuildGoldImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_gold;
            this.commitBuildGoldImage.Position = new Point(13, 0x2d);
            this.commitBuildPanelImage.addControl(this.commitBuildGoldImage);
            this.commitBuildGoldLabel.Text = "0";
            this.commitBuildGoldLabel.Color = ARGBColors.Black;
            this.commitBuildGoldLabel.Position = new Point(40, 0x31);
            this.commitBuildGoldLabel.Size = new Size(0x2e, 20);
            this.commitBuildPanelImage.addControl(this.commitBuildGoldLabel);
            this.commitBuildTimeImage.Image = (Image) GFXLibrary.r_building_panel_inset_icon_time;
            this.commitBuildTimeImage.Position = new Point(13, 0x40);
            this.commitBuildPanelImage.addControl(this.commitBuildTimeImage);
            this.commitBuildTimeLabel.Text = "";
            this.commitBuildTimeLabel.Color = ARGBColors.Black;
            this.commitBuildTimeLabel.Position = new Point(40, 0x43);
            this.commitBuildTimeLabel.Size = new Size(120, 20);
            this.commitBuildPanelImage.addControl(this.commitBuildTimeLabel);
        }

        public void initDeleteBar()
        {
            int y = this.calcDeleteBarYPos();
            this.deleteHeaderButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_deletemode_off_normal;
            this.deleteHeaderButton.ImageOver = (Image) GFXLibrary.r_building_miltary_deletemode_off_over;
            this.deleteHeaderButton.Position = new Point(0, y);
            this.deleteHeaderButton.Text.Text = SK.Text("CastleMapPanel_Delete_Off", "Delete: Off");
            this.deleteHeaderButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.deleteHeaderButton.Text.Position = new Point(0x4b, -6);
            this.deleteHeaderButton.Text.Size = new Size(this.deleteHeaderButton.Width - 0x4b, this.deleteHeaderButton.Height);
            this.deleteHeaderButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.deleteHeaderButton.TextYOffset = 0;
            this.deleteHeaderButton.Text.Color = ARGBColors.Black;
            this.deleteHeaderButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleElementDeleteToggle), "CastleMapPanel_toggle_delete");
            this.castlePlaceBackgroundArea.addControl(this.deleteHeaderButton);
        }

        private void InitializeComponent()
        {
            this.LoadCampDialog = new OpenFileDialog();
            this.SaveCampDialog = new SaveFileDialog();
            base.SuspendLayout();
            this.LoadCampDialog.DefaultExt = "camp";
            this.LoadCampDialog.Filter = "Bandit Camps (*.camp)|*.camp";
            this.LoadCampDialog.Title = "Load Bandit Camps";
            this.SaveCampDialog.DefaultExt = "camp";
            this.SaveCampDialog.Filter = "Bandit Camps (*.camp)|*.camp";
            this.SaveCampDialog.Title = "Save bandit Camps";
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "CastleMapPanel";
            base.Size = new Size(0xc4, 0x236);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public void initSelectionPanel()
        {
            this.castleSelectionBackgroundArea.Position = new Point(0, 0);
            this.castleSelectionBackgroundArea.Size = base.Size;
            this.castleSelectionBackgroundArea.Visible = false;
            base.addControl(this.castleSelectionBackgroundArea);
            this.castleSelectionPanelImage.Image = (Image) GFXLibrary.r_building_panel_back;
            this.castleSelectionPanelImage.Position = new Point(0, 0);
            this.castleSelectionBackgroundArea.addControl(this.castleSelectionPanelImage);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x99, 6);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapPanel_selection_close");
            this.castleSelectionBackgroundArea.addControl(this.closeButton);
            this.castleSelectionInset1Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_def_select;
            this.castleSelectionInset1Image.Position = new Point(3, 0x1c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset1Image);
            this.castleSelectionPeasantImage.Image = (Image) GFXLibrary.r_building_miltary_peasent;
            this.castleSelectionPeasantImage.Position = new Point(20, -20);
            this.castleSelectionInset1Image.addControl(this.castleSelectionPeasantImage);
            this.castleSelectionPeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionPeasantInset.Position = new Point(70, 60);
            this.castleSelectionPeasantImage.addControl(this.castleSelectionPeasantInset);
            this.castleSelectionPeasantLabel.Text = "0";
            this.castleSelectionPeasantLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionPeasantLabel.Position = new Point(0, 0);
            this.castleSelectionPeasantLabel.Size = this.castleSelectionPeasantInset.Size;
            this.castleSelectionPeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionPeasantInset.addControl(this.castleSelectionPeasantLabel);
            this.castleSelectionPeasantButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
            this.castleSelectionPeasantButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
            this.castleSelectionPeasantButton.Position = new Point(5, 12);
            this.castleSelectionPeasantButton.Data = 70;
            this.castleSelectionPeasantButton.CustomTooltipID = 0xde;
            this.castleSelectionPeasantButton.CustomTooltipData = 70;
            this.castleSelectionPeasantButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aggressiveStateClick), "CastleMapPanel_toggle_aggressive_peasant");
            this.castleSelectionInset1Image.addControl(this.castleSelectionPeasantButton);
            this.castleSelectionPeasantDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionPeasantDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionPeasantDeleteButton.Position = new Point(0x87, 13);
            this.castleSelectionPeasantDeleteButton.Data = 70;
            this.castleSelectionPeasantDeleteButton.CustomTooltipID = 0xdd;
            this.castleSelectionPeasantDeleteButton.CustomTooltipData = 70;
            this.castleSelectionPeasantDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapPanel_delete_peasants");
            this.castleSelectionInset1Image.addControl(this.castleSelectionPeasantDeleteButton);
            this.castleSelectionInset2Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_def_select;
            this.castleSelectionInset2Image.Position = new Point(3, 0x6c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset2Image);
            this.castleSelectionArcherImage.Image = (Image) GFXLibrary.r_building_miltary_archer;
            this.castleSelectionArcherImage.Position = new Point(20, -20);
            this.castleSelectionInset2Image.addControl(this.castleSelectionArcherImage);
            this.castleSelectionArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionArcherInset.Position = new Point(70, 60);
            this.castleSelectionArcherImage.addControl(this.castleSelectionArcherInset);
            this.castleSelectionArcherLabel.Text = "0";
            this.castleSelectionArcherLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionArcherLabel.Position = new Point(0, 0);
            this.castleSelectionArcherLabel.Size = this.castleSelectionArcherInset.Size;
            this.castleSelectionArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionArcherInset.addControl(this.castleSelectionArcherLabel);
            this.castleSelectionArcherButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
            this.castleSelectionArcherButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
            this.castleSelectionArcherButton.Position = new Point(5, 12);
            this.castleSelectionArcherButton.Data = 0x48;
            this.castleSelectionArcherButton.Enabled = false;
            this.castleSelectionArcherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aggressiveStateClick), "CastleMapPanel_toggle_aggressive_archers");
            this.castleSelectionInset2Image.addControl(this.castleSelectionArcherButton);
            this.castleSelectionArcherDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionArcherDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionArcherDeleteButton.Position = new Point(0x87, 13);
            this.castleSelectionArcherDeleteButton.Data = 0x48;
            this.castleSelectionArcherDeleteButton.CustomTooltipID = 0xdd;
            this.castleSelectionArcherDeleteButton.CustomTooltipData = 0x48;
            this.castleSelectionArcherDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapPanel_delete_archers");
            this.castleSelectionInset2Image.addControl(this.castleSelectionArcherDeleteButton);
            this.castleSelectionInset3Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_def_select;
            this.castleSelectionInset3Image.Position = new Point(3, 0xbc);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset3Image);
            this.castleSelectionPikemanImage.Image = (Image) GFXLibrary.r_building_miltary_pikemen;
            this.castleSelectionPikemanImage.Position = new Point(20, -20);
            this.castleSelectionInset3Image.addControl(this.castleSelectionPikemanImage);
            this.castleSelectionPikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionPikemanInset.Position = new Point(70, 60);
            this.castleSelectionPikemanImage.addControl(this.castleSelectionPikemanInset);
            this.castleSelectionPikemanLabel.Text = "0";
            this.castleSelectionPikemanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionPikemanLabel.Position = new Point(0, 0);
            this.castleSelectionPikemanLabel.Size = this.castleSelectionPikemanInset.Size;
            this.castleSelectionPikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionPikemanInset.addControl(this.castleSelectionPikemanLabel);
            this.castleSelectionPikemanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
            this.castleSelectionPikemanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
            this.castleSelectionPikemanButton.Position = new Point(5, 12);
            this.castleSelectionPikemanButton.Data = 0x49;
            this.castleSelectionPikemanButton.CustomTooltipID = 0xde;
            this.castleSelectionPikemanButton.CustomTooltipData = 0x49;
            this.castleSelectionPikemanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aggressiveStateClick), "CastleMapPanel_toggle_aggressive_pikemen");
            this.castleSelectionInset3Image.addControl(this.castleSelectionPikemanButton);
            this.castleSelectionPikemanDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionPikemanDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionPikemanDeleteButton.Position = new Point(0x87, 13);
            this.castleSelectionPikemanDeleteButton.Data = 0x49;
            this.castleSelectionPikemanDeleteButton.CustomTooltipID = 0xdd;
            this.castleSelectionPikemanDeleteButton.CustomTooltipData = 0x49;
            this.castleSelectionPikemanDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapPanel_delete_pikemen");
            this.castleSelectionInset3Image.addControl(this.castleSelectionPikemanDeleteButton);
            this.castleSelectionInset4Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_def_select;
            this.castleSelectionInset4Image.Position = new Point(3, 0x10c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset4Image);
            this.castleSelectionSwordsmanImage.Image = (Image) GFXLibrary.r_building_miltary_swordsman;
            this.castleSelectionSwordsmanImage.Position = new Point(20, -20);
            this.castleSelectionInset4Image.addControl(this.castleSelectionSwordsmanImage);
            this.castleSelectionSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionSwordsmanInset.Position = new Point(70, 60);
            this.castleSelectionSwordsmanImage.addControl(this.castleSelectionSwordsmanInset);
            this.castleSelectionSwordsmanLabel.Text = "0";
            this.castleSelectionSwordsmanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionSwordsmanLabel.Position = new Point(0, 0);
            this.castleSelectionSwordsmanLabel.Size = this.castleSelectionSwordsmanInset.Size;
            this.castleSelectionSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionSwordsmanInset.addControl(this.castleSelectionSwordsmanLabel);
            this.castleSelectionSwordsmanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
            this.castleSelectionSwordsmanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
            this.castleSelectionSwordsmanButton.Position = new Point(5, 12);
            this.castleSelectionSwordsmanButton.Data = 0x47;
            this.castleSelectionSwordsmanButton.CustomTooltipID = 0xde;
            this.castleSelectionSwordsmanButton.CustomTooltipData = 0x47;
            this.castleSelectionSwordsmanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aggressiveStateClick), "CastleMapPanel_toggle_aggressive_swordsmen");
            this.castleSelectionInset4Image.addControl(this.castleSelectionSwordsmanButton);
            this.castleSelectionSwordsmanDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionSwordsmanDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionSwordsmanDeleteButton.Position = new Point(0x87, 13);
            this.castleSelectionSwordsmanDeleteButton.Data = 0x47;
            this.castleSelectionSwordsmanDeleteButton.CustomTooltipID = 0xdd;
            this.castleSelectionSwordsmanDeleteButton.CustomTooltipData = 0x47;
            this.castleSelectionSwordsmanDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapPanel_delete_swordsmen");
            this.castleSelectionInset4Image.addControl(this.castleSelectionSwordsmanDeleteButton);
            this.castleSelectionInset5Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_def_select;
            this.castleSelectionInset5Image.Position = new Point(3, 0x15c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset5Image);
            this.castleSelectionCaptainImage.Image = (Image) GFXLibrary.r_building_miltary_captain_normal;
            this.castleSelectionCaptainImage.Position = new Point(20, -20);
            this.castleSelectionInset5Image.addControl(this.castleSelectionCaptainImage);
            this.castleSelectionCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionCaptainInset.Position = new Point(70, 60);
            this.castleSelectionCaptainImage.addControl(this.castleSelectionCaptainInset);
            this.castleSelectionCaptainLabel.Text = "0";
            this.castleSelectionCaptainLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionCaptainLabel.Position = new Point(0, 0);
            this.castleSelectionCaptainLabel.Size = this.castleSelectionCaptainInset.Size;
            this.castleSelectionCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionCaptainInset.addControl(this.castleSelectionCaptainLabel);
            this.castleSelectionCaptainButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
            this.castleSelectionCaptainButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
            this.castleSelectionCaptainButton.Position = new Point(5, 12);
            this.castleSelectionCaptainButton.Data = 0x55;
            this.castleSelectionCaptainButton.Enabled = false;
            this.castleSelectionCaptainButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aggressiveStateClick), "CastleMapPanel_toggle_aggressive_captains");
            this.castleSelectionInset5Image.addControl(this.castleSelectionCaptainButton);
            this.castleSelectionCaptainDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionCaptainDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionCaptainDeleteButton.Position = new Point(0x87, 13);
            this.castleSelectionCaptainDeleteButton.Data = 0x55;
            this.castleSelectionCaptainDeleteButton.CustomTooltipID = 0xdd;
            this.castleSelectionCaptainDeleteButton.CustomTooltipData = 0x55;
            this.castleSelectionCaptainDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapPanel_delete_captains");
            this.castleSelectionInset5Image.addControl(this.castleSelectionCaptainDeleteButton);
        }

        public void initSelectionPanel_Captains()
        {
            this.captain_castleSelectionBackgroundArea.Position = new Point(0, 0);
            this.captain_castleSelectionBackgroundArea.Size = base.Size;
            this.captain_castleSelectionBackgroundArea.Visible = false;
            base.addControl(this.captain_castleSelectionBackgroundArea);
            this.captain_castleSelectionPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
            this.captain_castleSelectionPanelImage.Position = new Point(0, 0);
            this.captain_castleSelectionBackgroundArea.addControl(this.captain_castleSelectionPanelImage);
            this.captain_closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.captain_closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.captain_closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.captain_closeButton.Position = new Point(0x99, 6);
            this.captain_closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapPanel_captains_close");
            this.captain_castleSelectionBackgroundArea.addControl(this.captain_closeButton);
            this.captain_castleSelectionInset5Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_def_select;
            this.captain_castleSelectionInset5Image.Position = new Point(3, 0x1c);
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionInset5Image);
            this.captain_castleSelectionCaptainImage.Image = (Image) GFXLibrary.r_building_miltary_captain_normal;
            this.captain_castleSelectionCaptainImage.Position = new Point(20, -20);
            this.captain_castleSelectionInset5Image.addControl(this.captain_castleSelectionCaptainImage);
            this.captain_castleSelectionCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.captain_castleSelectionCaptainInset.Position = new Point(70, 60);
            this.captain_castleSelectionCaptainImage.addControl(this.captain_castleSelectionCaptainInset);
            this.captain_castleSelectionCaptainLabel.Text = "0";
            this.captain_castleSelectionCaptainLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.captain_castleSelectionCaptainLabel.Position = new Point(0, 0);
            this.captain_castleSelectionCaptainLabel.Size = this.captain_castleSelectionCaptainInset.Size;
            this.captain_castleSelectionCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.captain_castleSelectionCaptainInset.addControl(this.captain_castleSelectionCaptainLabel);
            this.captain_castleSelectionCaptainButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
            this.captain_castleSelectionCaptainButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
            this.captain_castleSelectionCaptainButton.Position = new Point(5, 12);
            this.captain_castleSelectionCaptainButton.Data = 0x55;
            this.captain_castleSelectionCaptainButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aggressiveStateClick), "CastleMapPanel_toggle_aggressive_captains");
            this.captain_castleSelectionInset5Image.addControl(this.captain_castleSelectionCaptainButton);
            this.captain_castleSelectionCaptainDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.captain_castleSelectionCaptainDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.captain_castleSelectionCaptainDeleteButton.Position = new Point(0x87, 13);
            this.captain_castleSelectionCaptainDeleteButton.Data = 0x55;
            this.captain_castleSelectionCaptainDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapPanel_delete_captains");
            this.captain_castleSelectionInset5Image.addControl(this.captain_castleSelectionCaptainDeleteButton);
        }

        public void initUtilBar()
        {
            int y = this.calcUtilBarYPos();
            this.utilPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
            this.utilPanelImage.Position = new Point(0, y + 0x19);
            this.castlePlaceBackgroundArea.addControl(this.utilPanelImage);
            this.utilHeaderPanelImage.Image = (Image) GFXLibrary.r_building_miltary_castleinfo_normal;
            this.utilHeaderPanelImage.Position = new Point(0, y);
            this.utilHeaderPanelImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.utilMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.utilMouseLeave));
            this.utilHeaderPanelImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilClicked));
            this.utilHeaderPanelImage.CustomTooltipID = 0xd7;
            this.castlePlaceBackgroundArea.addControl(this.utilHeaderPanelImage);
            this.utilHeaderLabel1.Text = SK.Text("CastleMapPanel_Completion_In", "Completion In");
            this.utilHeaderLabel1.Color = ARGBColors.Black;
            this.utilHeaderLabel1.Position = new Point(50, 11);
            this.utilHeaderLabel1.Size = new Size(140, 20);
            this.utilHeaderLabel1.Visible = false;
            this.utilHeaderLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.utilHeaderLabel1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.utilHeaderPanelImage.addControl(this.utilHeaderLabel1);
            this.utilHeaderLabel2.Text = "00:00:00";
            this.utilHeaderLabel2.Color = ARGBColors.Black;
            this.utilHeaderLabel2.Position = new Point(50, 0x19);
            this.utilHeaderLabel2.Size = new Size(140, 20);
            this.utilHeaderLabel2.Visible = false;
            this.utilHeaderLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.utilHeaderLabel2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.utilHeaderPanelImage.addControl(this.utilHeaderLabel2);
            this.utilHeaderLabel3.Text = SK.Text("CastleMapPanel_Castle_Completed", "Castle Completed");
            this.utilHeaderLabel3.Color = ARGBColors.Black;
            this.utilHeaderLabel3.Position = new Point(40, 0x12);
            this.utilHeaderLabel3.Size = new Size(160, 20);
            this.utilHeaderLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.utilHeaderLabel3.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.utilHeaderPanelImage.addControl(this.utilHeaderLabel3);
            int num2 = 0xaf;
            this.utilRepairLabel.Text = SK.Text("CastleMapPanel_Repair", "Repair");
            this.utilRepairLabel.Color = ARGBColors.Black;
            this.utilRepairLabel.Position = new Point(0x55, num2 + 0x2d);
            this.utilRepairLabel.Size = new Size(110, 20);
            this.utilRepairLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.utilRepairLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.utilPanelImage.addControl(this.utilRepairLabel);
            this.utilRepairButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_repair_normal;
            this.utilRepairButton.ImageOver = (Image) GFXLibrary.r_building_miltary_repair_over;
            this.utilRepairButton.ImageClick = (Image) GFXLibrary.r_building_miltary_repair_pushed;
            this.utilRepairButton.Position = new Point(15, num2 + 0x20);
            this.utilRepairButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilRepairClick), "CastleMapPanel_repair");
            this.utilRepairButton.CustomTooltipID = 0xd9;
            this.utilPanelImage.addControl(this.utilRepairButton);
            this.utilDeleteConstructingLabel.Text = SK.Text("CastleMapPanel_Delete_Constructing", "Delete Constructing");
            this.utilDeleteConstructingLabel.Color = ARGBColors.Black;
            this.utilDeleteConstructingLabel.Position = new Point(0x55, ((num2 + 0x2d) + 50) - 20);
            this.utilDeleteConstructingLabel.Size = new Size(110, 40);
            this.utilDeleteConstructingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.utilDeleteConstructingLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.utilPanelImage.addControl(this.utilDeleteConstructingLabel);
            this.utilDeleteConstructingButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_repair_normal;
            this.utilDeleteConstructingButton.ImageOver = (Image) GFXLibrary.r_building_miltary_repair_over;
            this.utilDeleteConstructingButton.ImageClick = (Image) GFXLibrary.r_building_miltary_repair_pushed;
            this.utilDeleteConstructingButton.Position = new Point(15, (num2 + 0x20) + 50);
            this.utilDeleteConstructingButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilDeleteConstructingClick), "CastleMapPanel_delete_constructing");
            this.utilDeleteConstructingButton.CustomTooltipID = 0xda;
            this.utilPanelImage.addControl(this.utilDeleteConstructingButton);
            this.utilAdvancedButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.utilAdvancedButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.utilAdvancedButton.Position = new Point(0x15, (num2 + 0x20) + 100);
            this.utilAdvancedButton.Text.Text = SK.Text("CastleMapPanel_Delete_Advanced", "Advanced Options");
            this.utilAdvancedButton.TextYOffset = -1;
            this.utilAdvancedButton.Text.Color = ARGBColors.Black;
            this.utilAdvancedButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilAdvancedClick), "CastleMapPanel_advanced_options");
            this.utilAdvancedButton.Enabled = true;
            this.utilPanelImage.addControl(this.utilAdvancedButton);
            this.utilPanelFaderImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
            this.utilPanelFaderImage.Position = new Point(0, 0);
            this.utilPanelFaderImage.Alpha = 0f;
            this.loadButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.loadButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.loadButton.Position = new Point(0x2a, (num2 + 0x20) + 30);
            this.loadButton.Text.Text = "Load";
            this.loadButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.loadButton.TextYOffset = 1;
            this.loadButton.Visible = false;
            this.loadButton.Text.Color = ARGBColors.Black;
            this.loadButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_load));
            this.utilPanelImage.addControl(this.loadButton);
            this.saveButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.saveButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.saveButton.Position = new Point(0x2a, (num2 + 0x20) + 60);
            this.saveButton.Text.Text = "Save";
            this.saveButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.saveButton.TextYOffset = 1;
            this.saveButton.Visible = false;
            this.saveButton.Text.Color = ARGBColors.Black;
            this.saveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_save));
            this.utilPanelImage.addControl(this.saveButton);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void openCastlePlaceTab()
        {
            this.closeutilPanel();
            this.targetCastlePlaceHeight = 0x1a6;
        }

        public void refreshInterface()
        {
            this.controlBlockOverlay.Visible = false;
            if (this.currentCastlePlaceTab >= 0)
            {
                this.setCastlePlaceTab(this.currentCastlePlaceTab);
            }
        }

        private void reinforcementsClick()
        {
            this.setReinforcementsMode(!this.placingReinforcements);
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.recalcCastleLayout();
            }
        }

        private void resetCastleIcons()
        {
            this.clearCastlePlaceInfo();
            this.currentCastleIcon = 0;
            this.castlePlace1Button.Visible = false;
            this.castlePlace2Button.Visible = false;
            this.castlePlace3Button.Visible = false;
            this.castlePlace4Button.Visible = false;
            this.castlePlace5Button.Visible = false;
            this.castlePlace6Button.Visible = false;
            this.castlePlace7Button.Visible = false;
            this.castlePlace8Button.Visible = false;
            this.castlePlacePeasantButton.Visible = false;
            this.castlePlaceArcherButton.Visible = false;
            this.castlePlacePikemanButton.Visible = false;
            this.castlePlaceSwordsmanButton.Visible = false;
            this.castlePlaceWolfButton.Visible = false;
            this.castlePlaceToggleReinforcementsButton.Visible = false;
            this.castlePlaceSize1Button.Visible = false;
            this.castlePlaceSize3Button.Visible = false;
            this.castlePlaceSize5Button.Visible = false;
            this.castlePlaceSize15Button.Visible = false;
            this.castlePlaceGuardhouseLabel.Visible = false;
            this.castlePlaceTimeImage.Visible = false;
            this.castlePlaceWoodImage.Visible = false;
            this.castlePlaceStoneImage.Visible = false;
            this.castlePlaceIronImage.Visible = false;
            this.castlePlacePitchImage.Visible = false;
            this.castlePlaceGoldImage.Visible = false;
            this.building1Image.Visible = false;
            this.building2Image.Visible = false;
            this.building3Image.Visible = false;
            this.building4Image.Visible = false;
            this.building5Image.Visible = false;
            this.building6Image.Visible = false;
            this.building7Image.Visible = false;
            this.building8Image.Visible = false;
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            if (GameEngine.Instance.World.isCapital(villageID))
            {
                this.castleTotalGoldImage.Visible = true;
                this.castleTotalGoldLabel.Visible = true;
            }
            else
            {
                this.castleTotalGoldImage.Visible = false;
                this.castleTotalGoldLabel.Visible = false;
            }
        }

        public void Run()
        {
            this.updateCastleCompleteTime();
            this.CastlePanelUpdate();
            this.alphaPulse += 20;
            if (this.alphaPulse > 0x1ff)
            {
                this.alphaPulse -= 0x1ff;
            }
            int alphaPulse = this.alphaPulse;
            if (alphaPulse > 0xff)
            {
                alphaPulse = 0x1ff - alphaPulse;
            }
            if (!this.overCommitButton)
            {
                this.commitBuildCommitButton.Alpha = ((float) alphaPulse) / 255f;
                this.commitBuildCommitButton.invalidate();
            }
            else
            {
                this.commitBuildCommitButton.Alpha = 1f;
                this.commitBuildCommitButton.invalidate();
            }
        }

        public void setCastleElementInfo(string pieceName, int woodCost, int stoneCost, int ironCost, int pitchCost, int goldCost, string buildTime, bool rollover)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            VillageMap village = GameEngine.Instance.Village;
            this.clearCastlePlaceInfo();
            this.castlePlaceTypeLabel.Text = pieceName;
            this.castlePlaceTypeLabel.Visible = true;
            if (this.currentCastlePlaceTab != 0)
            {
                this.castlePlaceTimeLabel.Visible = true;
                if (buildTime.Length > 8)
                {
                    string str = "";
                    bool flag = false;
                    for (int i = 0; i < buildTime.Length; i++)
                    {
                        char ch = buildTime[i];
                        if (ch == ':')
                        {
                            if (flag)
                            {
                                break;
                            }
                            str = str + ch;
                            flag = true;
                        }
                        else
                        {
                            str = str + ch;
                        }
                    }
                    buildTime = str;
                }
                this.castlePlaceTimeImage.Visible = true;
                this.castlePlaceTimeLabel.Text = buildTime;
                if (woodCost > 0)
                {
                    this.castlePlaceWoodLabel.Text = woodCost.ToString("N", nFI);
                    this.castlePlaceWoodLabel.Visible = true;
                    this.castlePlaceWoodImage.Visible = true;
                    this.castlePlaceWoodLabel.Color = ARGBColors.Black;
                    if ((village != null) && (village.getResourceLevel(6) < woodCost))
                    {
                        this.castlePlaceWoodLabel.Color = ARGBColors.Red;
                    }
                }
                if (stoneCost > 0)
                {
                    this.castlePlaceStoneLabel.Text = stoneCost.ToString("N", nFI);
                    this.castlePlaceStoneLabel.Visible = true;
                    this.castlePlaceStoneImage.Visible = true;
                    this.castlePlaceStoneLabel.Color = ARGBColors.Black;
                    if ((village != null) && (village.getResourceLevel(7) < stoneCost))
                    {
                        this.castlePlaceStoneLabel.Color = ARGBColors.Red;
                    }
                }
                if (pitchCost > 0)
                {
                    this.castlePlacePitchLabel.Text = pitchCost.ToString("N", nFI);
                    this.castlePlacePitchLabel.Visible = true;
                    this.castlePlacePitchImage.Visible = true;
                    this.castlePlacePitchLabel.Color = ARGBColors.Black;
                    if ((village != null) && (village.getResourceLevel(9) < pitchCost))
                    {
                        this.castlePlacePitchLabel.Color = ARGBColors.Red;
                    }
                }
                if (ironCost > 0)
                {
                    this.castlePlaceIronLabel.Text = ironCost.ToString("N", nFI);
                    this.castlePlaceIronLabel.Visible = true;
                    this.castlePlaceIronImage.Visible = true;
                    this.castlePlaceIronLabel.Color = ARGBColors.Black;
                    if ((village != null) && (village.getResourceLevel(8) < ironCost))
                    {
                        this.castlePlaceIronLabel.Color = ARGBColors.Red;
                    }
                }
                double capitalGold = GameEngine.Instance.World.getCurrentGold();
                int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                if (GameEngine.Instance.World.isCapital(villageID) && (GameEngine.Instance.Castle != null))
                {
                    capitalGold = village.m_capitalGold;
                    int goldLevel = (int) capitalGold;
                    VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                    GameEngine.Instance.Castle.adjustLevels(ref levels, ref goldLevel);
                    this.castleTotalGoldLabel.Text = goldLevel.ToString("N", nFI);
                }
                if (goldCost > 0)
                {
                    this.castlePlaceGoldLabel.Text = goldCost.ToString("N", nFI);
                    this.castlePlaceGoldLabel.Visible = true;
                    this.castlePlaceGoldImage.Visible = true;
                    this.castlePlaceGoldLabel.Color = ARGBColors.Black;
                    if (capitalGold < goldCost)
                    {
                        this.castlePlaceGoldLabel.Color = ARGBColors.Red;
                    }
                }
                if (!rollover)
                {
                    this.buildingBeingPlaced = true;
                }
            }
        }

        private void setCastlePlaceTab(int tab)
        {
            this.currentCastlePlaceTab = tab;
            this.castlePlaceTab1Button.ImageNorm = (Image) GFXLibrary.castlebar_unit_normal;
            this.castlePlaceTab1Button.ImageOver = (Image) GFXLibrary.castlebar_unit_over;
            this.castlePlaceTab1Button.ImageClick = (Image) GFXLibrary.castlebar_unit_normal;
            this.castlePlaceTab2Button.ImageNorm = (Image) GFXLibrary.castlebar_wood_normal;
            this.castlePlaceTab2Button.ImageOver = (Image) GFXLibrary.castlebar_wood_over;
            this.castlePlaceTab2Button.ImageClick = (Image) GFXLibrary.castlebar_wood_normal;
            this.castlePlaceTab3Button.ImageNorm = (Image) GFXLibrary.castlebar_stone_normal;
            this.castlePlaceTab3Button.ImageOver = (Image) GFXLibrary.castlebar_stone_overl;
            this.castlePlaceTab3Button.ImageClick = (Image) GFXLibrary.castlebar_stone_normal;
            this.castlePlaceTab4Button.ImageNorm = (Image) GFXLibrary.castlebar_defenses_normal;
            this.castlePlaceTab4Button.ImageOver = (Image) GFXLibrary.castlebar_defenses_over;
            this.castlePlaceTab4Button.ImageClick = (Image) GFXLibrary.castlebar_defenses_normal;
            this.castlePlaceTab5Button.ImageNorm = (Image) GFXLibrary.castlebar_lock_normal;
            this.castlePlaceTab5Button.ImageOver = (Image) GFXLibrary.castlebar_lock_over;
            this.castlePlaceTab5Button.ImageClick = (Image) GFXLibrary.castlebar_lock_normal;
            this.castlePlaceTab1Button.CustomTooltipData = 0;
            this.castlePlaceTab2Button.CustomTooltipData = 0;
            this.castlePlaceTab3Button.CustomTooltipData = 0;
            this.castlePlaceTab4Button.CustomTooltipData = 0;
            this.castlePlaceTab5Button.CustomTooltipData = 0;
            switch (tab)
            {
                case 0:
                    this.castlePlaceTab1Button.CustomTooltipData = 1;
                    this.castlePlaceTab1Button.ImageNorm = (Image) GFXLibrary.castlebar_unit_selected;
                    this.castlePlaceTab1Button.ImageOver = (Image) GFXLibrary.castlebar_unit_selected;
                    this.castlePlaceTab1Button.ImageClick = (Image) GFXLibrary.castlebar_unit_selected;
                    break;

                case 1:
                    this.castlePlaceTab2Button.CustomTooltipData = 1;
                    this.castlePlaceTab2Button.ImageNorm = (Image) GFXLibrary.castlebar_wood_selected;
                    this.castlePlaceTab2Button.ImageOver = (Image) GFXLibrary.castlebar_wood_selected;
                    this.castlePlaceTab2Button.ImageClick = (Image) GFXLibrary.castlebar_wood_selected;
                    break;

                case 2:
                    this.castlePlaceTab3Button.CustomTooltipData = 1;
                    this.castlePlaceTab3Button.ImageNorm = (Image) GFXLibrary.castlebar_stone_selected;
                    this.castlePlaceTab3Button.ImageOver = (Image) GFXLibrary.castlebar_stone_selected;
                    this.castlePlaceTab3Button.ImageClick = (Image) GFXLibrary.castlebar_stone_selected;
                    break;

                case 3:
                    this.castlePlaceTab4Button.CustomTooltipData = 1;
                    this.castlePlaceTab4Button.ImageNorm = (Image) GFXLibrary.castlebar_defenses_selected;
                    this.castlePlaceTab4Button.ImageOver = (Image) GFXLibrary.castlebar_defenses_selected;
                    this.castlePlaceTab4Button.ImageClick = (Image) GFXLibrary.castlebar_defenses_selected;
                    break;

                case 4:
                    this.castlePlaceTab5Button.CustomTooltipData = 1;
                    this.castlePlaceTab5Button.ImageNorm = (Image) GFXLibrary.castlebar_lock_selected;
                    this.castlePlaceTab5Button.ImageOver = (Image) GFXLibrary.castlebar_lock_selected;
                    this.castlePlaceTab5Button.ImageClick = (Image) GFXLibrary.castlebar_lock_selected;
                    break;
            }
            this.resetCastleIcons();
            bool flag = true;
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            if (GameEngine.Instance.World.isCapital(villageID) && !GameEngine.Instance.World.isUserVillage(villageID))
            {
                flag = false;
            }
            if (flag && (GameEngine.Instance.Castle != null))
            {
                switch (tab)
                {
                    case 0:
                        if (!GameEngine.Instance.Castle.InBuilderMode || CastleMap.CreateMode)
                        {
                            if (this.canPlaceCastleItem(70))
                            {
                                this.castlePlacePeasantButton.Visible = true;
                            }
                            if (this.canPlaceCastleItem(0x48))
                            {
                                this.castlePlaceArcherButton.Visible = true;
                            }
                            if (this.canPlaceCastleItem(0x49))
                            {
                                this.castlePlacePikemanButton.Visible = true;
                            }
                            if (this.canPlaceCastleItem(0x47))
                            {
                                this.castlePlaceSwordsmanButton.Visible = true;
                            }
                            if (CastleMap.CreateMode)
                            {
                                this.castlePlaceWolfButton.Visible = true;
                            }
                            else if (this.canPlaceCastleItem(0x55))
                            {
                                this.castlePlaceWolfButton.Visible = true;
                            }
                            this.castlePlaceToggleReinforcementsButton.Visible = true;
                            this.castlePlaceSize1Button.Visible = true;
                            this.castlePlaceSize3Button.Visible = true;
                            this.castlePlaceSize5Button.Visible = true;
                            this.castlePlaceSize15Button.Visible = true;
                            this.castlePlaceGuardhouseLabel.Visible = true;
                        }
                        this.castlePlaceTimeImage.Visible = false;
                        this.castlePlaceWoodImage.Visible = false;
                        this.castlePlaceStoneImage.Visible = false;
                        this.castlePlaceIronImage.Visible = false;
                        this.castlePlacePitchImage.Visible = false;
                        this.castlePlaceGoldImage.Visible = false;
                        this.castleTotalGoldImage.Visible = false;
                        this.castleTotalGoldLabel.Visible = false;
                        break;

                    case 1:
                        if (!GameEngine.Instance.Castle.InTroopPlacerMode || CastleMap.CreateMode)
                        {
                            this.addCastleIcon(0x21, GFXLibrary.r_building_miltary_woodwall, GFXLibrary.r_building_miltary_woodwall_over);
                            this.addCastleIcon(0x42, GFXLibrary.r_building_miltary_woodwallblock, GFXLibrary.r_building_miltary_woodwallblock_over);
                            this.addCastleIcon(0x27, GFXLibrary.r_building_miltary_gatehouse_wood, GFXLibrary.r_building_miltary_gatehouse_wood_over);
                            this.addCastleIcon(0x15, GFXLibrary.r_building_miltary_woodtower, GFXLibrary.r_building_miltary_woodtower_over);
                        }
                        break;

                    case 2:
                        if (!GameEngine.Instance.Castle.InTroopPlacerMode || CastleMap.CreateMode)
                        {
                            this.addCastleIcon(0x22, GFXLibrary.r_building_miltary_stonewall, GFXLibrary.r_building_miltary_stonewall_over);
                            this.addCastleIcon(0x41, GFXLibrary.r_building_miltary_stonewallblock, GFXLibrary.r_building_miltary_stonewallblock_over);
                            this.addCastleIcon(0x25, GFXLibrary.r_building_miltary_gatehouse, GFXLibrary.r_building_miltary_gatehouse_over);
                            this.addCastleIcon(11, GFXLibrary.r_building_miltary_lookouttower, GFXLibrary.r_building_miltary_lookouttower_over);
                            this.addCastleIcon(12, GFXLibrary.r_building_miltary_smalltower, GFXLibrary.r_building_miltary_smalltower_over);
                            this.addCastleIcon(13, GFXLibrary.r_building_miltary_largetower, GFXLibrary.r_building_miltary_largetower_over);
                            this.addCastleIcon(14, GFXLibrary.r_building_miltary_greattower, GFXLibrary.r_building_miltary_greattower_over);
                        }
                        break;

                    case 3:
                        if (!GameEngine.Instance.Castle.InTroopPlacerMode || CastleMap.CreateMode)
                        {
                            this.addCastleIcon(0x1f, GFXLibrary.r_building_miltary_guardhouse, GFXLibrary.r_building_miltary_guardhouse_over);
                            this.addCastleIcon(0x24, GFXLibrary.r_building_miltary_killingpits, GFXLibrary.r_building_miltary_killingpits_over);
                            this.addCastleIcon(0x20, GFXLibrary.r_building_miltary_smelter, GFXLibrary.r_building_miltary_smelter_over);
                        }
                        if (!GameEngine.Instance.Castle.InBuilderMode || CastleMap.CreateMode)
                        {
                            this.addCastleIcon(0x4b, GFXLibrary.r_building_miltary_oilpots, GFXLibrary.r_building_miltary_oilpots_over);
                        }
                        if (!GameEngine.Instance.Castle.InTroopPlacerMode || CastleMap.CreateMode)
                        {
                            this.addCastleIcon(0x23, GFXLibrary.r_building_miltary_moat, GFXLibrary.r_building_miltary_moat_over);
                        }
                        break;

                    case 4:
                        if (!GameEngine.Instance.Castle.InTroopPlacerMode || CastleMap.CreateMode)
                        {
                            this.addCastleIcon(0x29, GFXLibrary.r_building_miltary_turrets, GFXLibrary.r_building_miltary_tunnels_over);
                            this.addCastleIcon(0x2b, GFXLibrary.r_building_miltary_tunnels, GFXLibrary.r_building_miltary_turrets_over);
                            this.addCastleIcon(0x2a, GFXLibrary.r_building_miltary_ballista, GFXLibrary.r_building_miltary_ballista_over);
                            this.addCastleIcon(0x2c, GFXLibrary.r_building_miltary_bombard, GFXLibrary.r_building_miltary_bombard_over);
                        }
                        break;
                }
            }
            this.deleteHeaderButton.Enabled = true;
            if (GameEngine.Instance.World.isCapital(villageID))
            {
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    NumberFormatInfo nFI = GameEngine.NFI;
                    this.castleTotalGoldLabel.Text = ((int) village.m_capitalGold).ToString("N", nFI);
                }
                if (!GameEngine.Instance.World.isUserVillage(villageID))
                {
                    this.deleteHeaderButton.Enabled = false;
                }
            }
        }

        public void setCastleStats(int numGuardHouseSpaces, int numPlacedArchers, int numPlacedPeasants, int numPlacedPikemen, int numPlacedSwordsmen, DateTime castleComplete, bool castleCompleted, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numPots, int numSmelterPlaces, bool castleDamaged, int numPlacedReinforceArchers, int numPlacedReinforcePeasants, int numPlacedReinforcePikemen, int numPlacedReinforceSwordsmen, int numReinforcePeasants, int numReinforceArchers, int numReinforcePikemen, int numReinforceSwordsmen, int numAvailableVassalReinforceDefenderPeasants, int numAvailableVassalReinforceDefenderArchers, int numAvailableVassalReinforceDefenderPikemen, int numAvailableVassalReinforceDefenderSwordsmen, int numPlacedVassalReinforceDefenderArchers, int numPlacedVassalReinforceDefenderPeasants, int numPlacedVassalReinforceDefenderPikemen, int numPlacedVassalReinforceDefenderSwordsmen, int numPlacedCaptains, int numCaptains)
        {
            this.m_castleCompletTime = castleComplete;
            this.m_castledCompleted = castleCompleted;
            this.updateCastleCompleteTime();
            if (CastleMap.CreateMode)
            {
                numPeasants = 0x3e8;
                numArchers = 0x3e8;
                numPikemen = 0x3e8;
                numSwordsmen = 0x3e8;
                numGuardHouseSpaces = 0x2710;
                this.saveButton.Visible = true;
                this.loadButton.Visible = true;
            }
            else
            {
                this.saveButton.Visible = false;
                this.loadButton.Visible = false;
            }
            if (castleDamaged)
            {
                int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                if (GameEngine.Instance.World.isCapital(villageID) && !GameEngine.Instance.World.isUserVillage(villageID))
                {
                    this.utilRepairButton.Enabled = false;
                }
                else
                {
                    this.utilRepairButton.Enabled = true;
                }
            }
            else
            {
                this.utilRepairButton.Enabled = false;
            }
            if (castleCompleted)
            {
                this.utilDeleteConstructingButton.Enabled = false;
            }
            else
            {
                int num2 = InterfaceMgr.Instance.getSelectedMenuVillage();
                if (GameEngine.Instance.World.isCapital(num2) && !GameEngine.Instance.World.isUserVillage(num2))
                {
                    this.utilDeleteConstructingButton.Enabled = false;
                }
                else if ((GameEngine.Instance.Castle != null) && GameEngine.Instance.Castle.isInDeleteConstructing())
                {
                    this.utilDeleteConstructingButton.Enabled = false;
                }
                else
                {
                    this.utilDeleteConstructingButton.Enabled = true;
                }
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            int num3 = (((numPlacedArchers + numPlacedPeasants) + numPlacedPikemen) + numPlacedSwordsmen) + numPlacedCaptains;
            num3 += ((numPlacedReinforceArchers + numPlacedReinforcePeasants) + numPlacedReinforcePikemen) + numPlacedReinforceSwordsmen;
            num3 += ((numPlacedVassalReinforceDefenderArchers + numPlacedVassalReinforceDefenderPeasants) + numPlacedVassalReinforceDefenderPikemen) + numPlacedVassalReinforceDefenderSwordsmen;
            int num4 = numGuardHouseSpaces - num3;
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (!this.placingReinforcements)
            {
                this.castlePlacePeasantLabel.Text = Math.Min(numPeasants, num4).ToString("N", nFI);
                this.castlePlaceArcherLabel.Text = Math.Min(numArchers, num4).ToString("N", nFI);
                this.castlePlacePikemanLabel.Text = Math.Min(numPikemen, num4).ToString("N", nFI);
                this.castlePlaceSwordsmanLabel.Text = Math.Min(numSwordsmen, num4).ToString("N", nFI);
                if (!CastleMap.CreateMode)
                {
                    this.castlePlaceWolfLabel.Text = Math.Min(numCaptains, num4).ToString("N", nFI);
                }
                if ((numPeasants == 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlacePeasantButton.Enabled = false;
                }
                else
                {
                    this.castlePlacePeasantButton.Enabled = true;
                }
                if ((numArchers == 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlaceArcherButton.Enabled = false;
                }
                else
                {
                    this.castlePlaceArcherButton.Enabled = true;
                }
                if ((numPikemen == 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlacePikemanButton.Enabled = false;
                }
                else
                {
                    this.castlePlacePikemanButton.Enabled = true;
                }
                if ((numSwordsmen == 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlaceSwordsmanButton.Enabled = false;
                }
                else
                {
                    this.castlePlaceSwordsmanButton.Enabled = true;
                }
                if (!CastleMap.CreateMode)
                {
                    if ((numCaptains == 0) || (num3 >= numGuardHouseSpaces))
                    {
                        this.castlePlaceWolfButton.Enabled = false;
                    }
                    else
                    {
                        this.castlePlaceWolfButton.Enabled = true;
                    }
                }
            }
            else
            {
                this.castlePlacePeasantLabel.Text = Math.Max(0, Math.Min((numReinforcePeasants + numAvailableVassalReinforceDefenderPeasants) - numPlacedReinforcePeasants, num4)).ToString("N", nFI);
                this.castlePlaceArcherLabel.Text = Math.Max(0, Math.Min((numReinforceArchers + numAvailableVassalReinforceDefenderArchers) - numPlacedReinforceArchers, num4)).ToString("N", nFI);
                this.castlePlacePikemanLabel.Text = Math.Max(0, Math.Min((numReinforcePikemen + numAvailableVassalReinforceDefenderPikemen) - numPlacedReinforcePikemen, num4)).ToString("N", nFI);
                this.castlePlaceSwordsmanLabel.Text = Math.Max(0, Math.Min((numReinforceSwordsmen + numAvailableVassalReinforceDefenderSwordsmen) - numPlacedReinforceSwordsmen, num4)).ToString("N", nFI);
                if (!CastleMap.CreateMode)
                {
                    this.castlePlaceWolfLabel.Text = "0";
                    this.castlePlaceWolfButton.Enabled = false;
                }
                if ((((numReinforcePeasants + numAvailableVassalReinforceDefenderPeasants) - numPlacedReinforcePeasants) <= 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlacePeasantButton.Enabled = false;
                }
                else
                {
                    this.castlePlacePeasantButton.Enabled = true;
                }
                if ((((numReinforceArchers + numAvailableVassalReinforceDefenderArchers) - numPlacedReinforceArchers) <= 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlaceArcherButton.Enabled = false;
                }
                else
                {
                    this.castlePlaceArcherButton.Enabled = true;
                }
                if ((((numReinforcePikemen + numAvailableVassalReinforceDefenderPikemen) - numPlacedReinforcePikemen) <= 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlacePikemanButton.Enabled = false;
                }
                else
                {
                    this.castlePlacePikemanButton.Enabled = true;
                }
                if ((((numReinforceSwordsmen + numAvailableVassalReinforceDefenderSwordsmen) - numPlacedReinforceSwordsmen) <= 0) || (num3 >= numGuardHouseSpaces))
                {
                    this.castlePlaceSwordsmanButton.Enabled = false;
                }
                else
                {
                    this.castlePlaceSwordsmanButton.Enabled = true;
                }
            }
            if (num3 >= numGuardHouseSpaces)
            {
                this.castlePlaceGuardhouseLabel.Color = ARGBColors.Red;
            }
            else
            {
                this.castlePlaceGuardhouseLabel.Color = ARGBColors.Black;
            }
            if (!CastleMap.CreateMode)
            {
                this.castlePlaceGuardhouseLabel.Text = SK.Text("CASTLEMAP_GUARD_HOUSE_CAPACITY", "Guard House Capacity") + " " + num3.ToString("N", nFI) + "/" + numGuardHouseSpaces.ToString("N", nFI);
            }
            else
            {
                this.castlePlaceGuardhouseLabel.Text = CastleMap.Builder_MapX.ToString() + ", " + CastleMap.Builder_MapY.ToString();
            }
        }

        private void setPlaceSize(int size)
        {
            this.placementSize = size;
            switch (size)
            {
                case 1:
                    if (GameEngine.Instance.Castle != null)
                    {
                        GameEngine.Instance.Castle.setPlacementSize(1);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_selected;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_selected;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
                    return;

                case 2:
                case 4:
                    break;

                case 3:
                    if (GameEngine.Instance.Castle != null)
                    {
                        GameEngine.Instance.Castle.setPlacementSize(2);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_selected;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_selected;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
                    return;

                case 5:
                    if (GameEngine.Instance.Castle != null)
                    {
                        GameEngine.Instance.Castle.setPlacementSize(3);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_selected;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_selected;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
                    return;

                case 15:
                    if (GameEngine.Instance.Castle != null)
                    {
                        GameEngine.Instance.Castle.setPlacementSize(4);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_selected;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_selected;
                    break;

                default:
                    return;
            }
        }

        private void setReinforcementsMode(bool reinforcements)
        {
            this.placingReinforcements = reinforcements;
            if (!reinforcements)
            {
                this.castlePlaceToggleReinforcementsButton.CustomTooltipID = 0xcd;
                this.castlePlacePeasantButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_peasent;
                this.castlePlacePeasantButton.ImageOver = (Image) GFXLibrary.r_building_miltary_peasent_over;
                this.castlePlaceArcherButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_archer;
                this.castlePlaceArcherButton.ImageOver = (Image) GFXLibrary.r_building_miltary_archer_over;
                this.castlePlacePikemanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen;
                this.castlePlacePikemanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen_over;
                this.castlePlaceSwordsmanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_swordsman;
                this.castlePlaceSwordsmanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_swordsman_over;
                this.castlePlaceToggleReinforcementsButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_flag_normal;
                this.castlePlaceToggleReinforcementsButton.ImageOver = (Image) GFXLibrary.r_building_miltary_flag_over;
            }
            else
            {
                this.castlePlaceToggleReinforcementsButton.CustomTooltipID = 0xce;
                this.castlePlacePeasantButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_peasent_green;
                this.castlePlacePeasantButton.ImageOver = (Image) GFXLibrary.r_building_miltary_peasent_over_green;
                this.castlePlaceArcherButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_archer_green;
                this.castlePlaceArcherButton.ImageOver = (Image) GFXLibrary.r_building_miltary_archer_over_green;
                this.castlePlacePikemanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen_green;
                this.castlePlacePikemanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen_over_green;
                this.castlePlaceSwordsmanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_swordsman_green;
                this.castlePlaceSwordsmanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_swordsman_over_green;
                this.castlePlaceToggleReinforcementsButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_flag_blue_normal;
                this.castlePlaceToggleReinforcementsButton.ImageOver = (Image) GFXLibrary.r_building_miltary_flag_blue_over;
            }
        }

        public void setSelectedTroop(int numPeasants, int peasantsState, int numArchers, int archersState, int numPikemen, int pikemenState, int numSwordsmen, int swordsmenState, int numCaptains, int captainState)
        {
            if (((!this.castleSelectionBackgroundArea.Visible || (this.sst_lastPeasants != numPeasants)) || ((this.sst_lastArchers != numArchers) || (this.sst_lastPikeman != numPikemen))) || ((this.sst_lastSwordsman != numSwordsmen) || (this.sst_lastCaptains != numCaptains)))
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_open_selected_troops_panel");
            }
            this.sst_lastPeasants = numPeasants;
            this.sst_lastArchers = numArchers;
            this.sst_lastPikeman = numPikemen;
            this.sst_lastSwordsman = numSwordsmen;
            this.sst_lastCaptains = numCaptains;
            if (!this.captain_castleSelectionBackgroundArea.Visible)
            {
                base.Invalidate();
            }
            this.castleSelectionBackgroundArea.Visible = true;
            this.castlePlaceBackgroundArea.Visible = false;
            this.castleSelectionPeasantLabel.Text = numPeasants.ToString();
            this.castleSelectionArcherLabel.Text = numArchers.ToString();
            this.castleSelectionPikemanLabel.Text = numPikemen.ToString();
            this.castleSelectionSwordsmanLabel.Text = numSwordsmen.ToString();
            this.castleSelectionCaptainLabel.Text = numCaptains.ToString();
            this.castleSelectionPeasantDeleteButton.Enabled = numPeasants > 0;
            this.castleSelectionArcherDeleteButton.Enabled = numArchers > 0;
            this.castleSelectionPikemanDeleteButton.Enabled = numPikemen > 0;
            this.castleSelectionSwordsmanDeleteButton.Enabled = numSwordsmen > 0;
            this.castleSelectionCaptainDeleteButton.Enabled = numCaptains > 0;
            this.castleSelectionPeasantButton.Enabled = numPeasants > 0;
            this.castleSelectionArcherButton.Enabled = false;
            this.castleSelectionPikemanButton.Enabled = numPikemen > 0;
            this.castleSelectionSwordsmanButton.Enabled = numSwordsmen > 0;
            this.castleSelectionCaptainButton.Enabled = false;
            if (peasantsState == 0)
            {
                this.castleSelectionPeasantButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
                this.castleSelectionPeasantButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
                this.nextPeasantState = true;
            }
            else if (peasantsState == 1)
            {
                this.castleSelectionPeasantButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_off_normal;
                this.castleSelectionPeasantButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_off_over;
                this.nextPeasantState = false;
            }
            else
            {
                this.castleSelectionPeasantButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_mix_normal;
                this.castleSelectionPeasantButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_mix_over;
                this.nextPeasantState = false;
            }
            if (archersState == 0)
            {
                this.castleSelectionArcherButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
                this.castleSelectionArcherButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
                this.nextArcherState = true;
            }
            else if (archersState == 1)
            {
                this.castleSelectionArcherButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_off_normal;
                this.castleSelectionArcherButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_off_over;
                this.nextArcherState = false;
            }
            else
            {
                this.castleSelectionArcherButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_mix_normal;
                this.castleSelectionArcherButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_mix_over;
                this.nextArcherState = false;
            }
            if (pikemenState == 0)
            {
                this.castleSelectionPikemanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
                this.castleSelectionPikemanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
                this.nextPikemanState = true;
            }
            else if (pikemenState == 1)
            {
                this.castleSelectionPikemanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_off_normal;
                this.castleSelectionPikemanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_off_over;
                this.nextPikemanState = false;
            }
            else
            {
                this.castleSelectionPikemanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_mix_normal;
                this.castleSelectionPikemanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_mix_over;
                this.nextPikemanState = false;
            }
            if (swordsmenState == 0)
            {
                this.castleSelectionSwordsmanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
                this.castleSelectionSwordsmanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
                this.nextSwordsmanState = true;
            }
            else if (swordsmenState == 1)
            {
                this.castleSelectionSwordsmanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_off_normal;
                this.castleSelectionSwordsmanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_off_over;
                this.nextSwordsmanState = false;
            }
            else
            {
                this.castleSelectionSwordsmanButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_mix_normal;
                this.castleSelectionSwordsmanButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_mix_over;
                this.nextSwordsmanState = false;
            }
            if (captainState == 0)
            {
                this.castleSelectionCaptainButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_def_normal;
                this.castleSelectionCaptainButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_def_over;
                this.nextCaptainState = true;
            }
            else if (captainState == 1)
            {
                this.castleSelectionCaptainButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_off_normal;
                this.castleSelectionCaptainButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_off_over;
                this.nextCaptainState = false;
            }
            else
            {
                this.castleSelectionCaptainButton.ImageNorm = (Image) GFXLibrary.castlescreen_stance_mix_normal;
                this.castleSelectionCaptainButton.ImageOver = (Image) GFXLibrary.castlescreen_stance_mix_over;
                this.nextCaptainState = false;
            }
        }

        public void showNewInterface()
        {
            this.currentUtilHeight = 0;
            this.currentCastlePlaceHeight = 0;
            this.closeCastlePlacePanel();
            this.closeutilPanel();
            this.CastlePanelUpdate();
        }

        public void stopDeleting()
        {
            this.allowDeleteCallback = false;
            this.deleteState = true;
            this.castleElementDeleteToggle();
            this.allowDeleteCallback = true;
        }

        private void troopDeleteClick()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                GameEngine.Instance.Castle.startDeleteTroops(data);
            }
        }

        public bool TUTORIAL_openedWoodTab()
        {
            if (this.currentCastlePlaceHeight == 0)
            {
                return false;
            }
            if (this.currentCastlePlaceTab != 1)
            {
                return false;
            }
            return true;
        }

        private void updateCastleCompleteTime()
        {
            if ((GameEngine.Instance.Castle != null) && !CastleMap.CreateMode)
            {
                this.utilHeaderPanelImage.Visible = !GameEngine.Instance.Castle.InTroopPlacerMode;
            }
            if (this.m_castledCompleted)
            {
                this.utilHeaderLabel1.Visible = false;
                this.utilHeaderLabel3.Visible = true;
                this.utilHeaderLabel2.Visible = false;
            }
            else
            {
                this.utilHeaderLabel1.Visible = true;
                this.utilHeaderLabel3.Visible = false;
                this.utilHeaderLabel2.Visible = true;
                TimeSpan span = (TimeSpan) (this.m_castleCompletTime - CastleMap.getCurrentServerTime());
                this.utilHeaderLabel2.Text = VillageMap.createBuildTimeString((int) span.TotalSeconds);
            }
        }

        public void updateCommitValues()
        {
            int goldLevel = 0;
            VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.adjustLevels(ref levels, ref goldLevel);
                this.commitBuildWoodLabel.Text = (0.0 - levels.woodLevel).ToString();
                this.commitBuildStoneLabel.Text = (0.0 - levels.stoneLevel).ToString();
                this.commitBuildIronLabel.Text = (0.0 - levels.ironLevel).ToString();
                this.commitBuildPitchLabel.Text = (0.0 - levels.pitchLevel).ToString();
                this.commitBuildGoldLabel.Text = goldLevel.ToString();
                this.commitBuildTimeLabel.Text = GameEngine.Instance.Castle.GetNewBuildTime();
            }
        }

        private void utilAdvancedClick()
        {
            if (GameEngine.Instance.Castle != null)
            {
                InterfaceMgr.Instance.openAdvancedCastleOptionsPopup(true);
            }
        }

        private void utilClicked()
        {
            this.closeCastlePlacePanel();
            if (this.currentUtilHeight == 0)
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_util_open");
                this.utilHeaderPanelImage.CustomTooltipID = 0xd8;
                this.targetUtilHeight = 0xbf;
            }
            else if (this.currentUtilHeight == 0xbf)
            {
                GameEngine.Instance.playInterfaceSound("CastleMapPanel_util_close");
                this.closeutilPanel();
            }
        }

        private void utilDeleteConstructing()
        {
            this.utilDeleteConstructingButton.Enabled = false;
            GameEngine.Instance.Castle.deleteConstructingElements();
            this.controlBlockOverlay.Visible = true;
        }

        private void utilDeleteConstructingClick()
        {
            if ((GameEngine.Instance.Castle != null) && (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("CastleMapPanel_Delete_All_Constructing", "Delete All Constructing?"), MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                this.utilDeleteConstructing();
            }
        }

        private void utilMouseLeave()
        {
            this.utilHeaderPanelImage.Image = (Image) GFXLibrary.r_building_miltary_castleinfo_normal;
        }

        private void utilMouseOver()
        {
            this.utilHeaderPanelImage.Image = (Image) GFXLibrary.r_building_miltary_castleinfo_over;
        }

        private void utilRepairClick()
        {
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.autoRepairCastle();
                this.utilRepairButton.Enabled = false;
            }
        }

        private void utilViewModeClick()
        {
            if (GameEngine.Instance.Castle != null)
            {
                GameEngine.Instance.Castle.toggleHeight();
                if (CastleMap.displayCollapsed)
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapPanel_toggle_height_low");
                    this.utilViewModeButton.CustomTooltipID = 0xd3;
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("CastleMapPanel_toggle_height_high");
                    this.utilViewModeButton.CustomTooltipID = 0xd4;
                }
            }
        }
    }
}

