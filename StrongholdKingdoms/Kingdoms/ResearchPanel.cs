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

    public class ResearchPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton buyPointButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage buyPointGold = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel buyPointInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel buyPointText = new CustomSelfDrawPanel.CSDLabel();
        private MyMessageBoxPopUp cancelResearchPopup;
        private CardBarGDI cardbar = new CardBarGDI();
        private IContainer components;
        private int curImageID;
        private int curLabelID;
        private CustomSelfDrawPanel.CSDImage currentResearchBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage currentResearchBackgroundImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton currentResearchCancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage currentResearchImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel currentResearchInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxHeadingText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxRow1Text = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxRow2Text = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel currentResearchInfoBoxRow3Text = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage currentResearchingBarImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel currentResearchText = new CustomSelfDrawPanel.CSDLabel();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDDragPanel dragOverlay = new CustomSelfDrawPanel.CSDDragPanel();
        private CustomSelfDrawPanel.CSDControl dragOverlay2 = new CustomSelfDrawPanel.CSDControl();
        private int[] educationLayout = new int[] { 
            -4, 0, 0x20, 1, 0x21, 4, 0x22, 3, 0x23, 6, 0x24, 5, 40, 2, 0x2a, 8, 
            0x2b, 7, 0x2c, 6, 0x29, 5, 0x2d, 4, 0x2e, 3, 0x3b, 1, 0x30, 5, 0x3a, 6, 
            0x31, 4, 50, 0x11, 0x33, 0x10, 0x35, 14, 0x36, 13, 0x38, 12, 0x37, 11, 0x34, 9, 
            0x48, 10, 0x2f, 3, 60, 2
         };
        private int[] educationLayout2 = new int[] { 
            -4, 0, 0x20, 1, 0x21, 4, 0x22, 3, 0x23, 6, 0x24, 5, 40, 2, 0x2a, 8, 
            0x2b, 7, 0x2c, 6, 0x29, 5, 0x2d, 4, 0x2e, 3, 0x3b, 1, 0x30, 5, 0x3a, 6, 
            0x31, 4, 50, 0x11, 0x33, 0x10, 0x36, 13, 0x38, 12, 0x37, 11, 0x34, 9, 0x48, 10, 
            0x2f, 3, 60, 2
         };
        private int[] educationResearchLayout = new int[] { 
            0x20, 40, 0x2e, 0x2d, 0x29, 0x2c, 0x2b, 0x2a, 0x22, 0x23, 0x24, 0x21, 0x3b, 60, 0x2f, 0x30, 
            0x3a, 0x31, 0x34, 0x48, 0x37, 0x38, 0x36, 0x35, 0x33, 50
         };
        private int[] educationResearchLayout2 = new int[] { 
            0x20, 40, 0x2e, 0x2d, 0x29, 0x2c, 0x2b, 0x2a, 0x22, 0x23, 0x24, 0x21, 0x3b, 60, 0x2f, 0x30, 
            0x3a, 0x31, 0x34, 0x48, 0x37, 0x38, 0x36, 0x33, 50
         };
        private CustomSelfDrawPanel.CSDImage[][] educationRows = new CustomSelfDrawPanel.CSDImage[0x1b][];
        private CustomSelfDrawPanel.CSDImage[][] educationRows2 = new CustomSelfDrawPanel.CSDImage[0x1a][];
        private int[] farmingLayout = new int[] { 
            -2, 0, 0x42, 1, 0x40, 1, 0x41, 1, 0x44, 1, 12, 8, 10, 7, 0x47, 5, 
            70, 3, 0x43, 2, 0x3d, 1, 11, 6, 0x3e, 3
         };
        private int[] farmingResearchLayout = new int[] { 0x42, 0x40, 0x41, 0x44, 0x43, 70, 0x47, 10, 12, 0x3d, 0x3e, 11 };
        private CustomSelfDrawPanel.CSDImage[][] farmingRows = new CustomSelfDrawPanel.CSDImage[13][];
        private bool forceUpdate;
        private List<CustomSelfDrawPanel.CSDImage> imageCache = new List<CustomSelfDrawPanel.CSDImage>();
        private int[] industryLayout = new int[] { 
            -1, 0, 0, 1, 1, 1, 4, 1, 3, 5, 2, 2, 6, 1, 0x26, 9, 
            0x27, 8, 5, 7, 0x45, 6, 7, 5, 9, 4, 8, 3, 0x3f, 2, 13, 1, 
            14, 6, 15, 5, 0x10, 4, 0x11, 3, 0x12, 2
         };
        private int[] industryResearchLayout = new int[] { 
            0, 1, 4, 2, 3, 6, 0x3f, 8, 9, 7, 0x45, 5, 0x27, 0x26, 13, 0x12, 
            0x11, 0x10, 15, 14
         };
        private CustomSelfDrawPanel.CSDImage[][] industryRows = new CustomSelfDrawPanel.CSDImage[0x15][];
        private List<CustomSelfDrawPanel.CSDLabel> labelCache = new List<CustomSelfDrawPanel.CSDLabel>();
        private ResearchData lastData;
        private ResearchData lastDataQueued;
        private int lastResearchTab = -1;
        private int lastScrollXPos;
        private int lastScrollYPos;
        private int lastTab;
        private double m_windowScale = 1.0;
        private int m_windowScaleNotch;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private const int MAX_RESEARCH_LINES = 30;
        private int[] militaryLayout = new int[] { 
            -3, 0, 0x13, 1, 0x54, 7, 20, 6, 0x39, 5, 0x15, 4, 0x16, 3, 0x17, 2, 
            0x18, 1, 0x1a, 6, 0x1b, 5, 0x1c, 4, 0x1d, 3, 30, 2, 0x4b, 1, 0x4c, 3, 
            0x25, 2, 0x4e, 1, 0x55, 2, 0x56, 1, 0x4a, 1, 0x19, 2, 0x52, 4, 0x49, 3
         };
        private int[] militaryResearchLayout = new int[] { 
            0x13, 0x17, 0x16, 0x15, 0x39, 20, 0x54, 0x18, 30, 0x1d, 0x1c, 0x1b, 0x1a, 0x4b, 0x4c, 0x25, 
            0x4e, 0x55, 0x56, 0x4a, 0x19, 0x49, 0x52
         };
        private CustomSelfDrawPanel.CSDImage[][] militaryRows = new CustomSelfDrawPanel.CSDImage[0x18][];
        private const int NUM_EDUCATION_COLUMNS = 0x1a;
        private const int NUM_EDUCATION_ROWS = 0x1b;
        private const int NUM_FARMING_COLUMNS = 0x11;
        private const int NUM_FARMING_ROWS = 13;
        private const int NUM_INDUSTRY_COLUMNS = 0x12;
        private const int NUM_INDUSTRY_ROWS = 0x15;
        private const int NUM_MILITARY_COLUMNS = 20;
        private const int NUM_MILITARY_ROWS = 0x18;
        private const int numOpenColumns = 6;
        private int[] openedResearches = new int[] { 
            0, 1, -1, 7, -1, -1, 1, 1, -1, 6, -1, -1, 4, 1, 2, -1, 
            -1, -1, 4, 4, 3, -1, -1, -1, 2, 1, -1, 8, -1, -1, 3, 1, 
            -1, 9, -1, -1, 6, 1, 0x3f, -1, -1, -1, 6, 2, 8, -1, -1, -1, 
            6, 3, 9, -1, -1, -1, 6, 4, 7, -1, -1, -1, 6, 5, 0x45, -1, 
            -1, -1, 6, 6, 5, -1, -1, -1, 6, 7, 0x27, -1, -1, -1, 6, 8, 
            0x26, -1, -1, -1, 0x3f, 1, -1, 0x16, -1, -1, 8, 1, -1, 0x15, -1, -1, 
            9, 1, -1, 0x1a, -1, -1, 7, 1, -1, 0x13, -1, -1, 0x45, 1, -1, 0x21, 
            -1, -1, 5, 1, -1, 0x17, -1, -1, 0x27, 1, -1, 0x18, -1, -1, 0x26, 1, 
            -1, 0x19, -1, -1, 13, 1, 0x12, -1, -1, -1, 13, 2, 0x11, -1, -1, -1, 
            13, 3, 0x10, -1, -1, -1, 13, 4, 15, -1, -1, -1, 13, 5, 14, -1, 
            -1, -1, 0x12, 1, -1, 0x1d, -1, -1, 0x11, 1, -1, 0x1f, -1, -1, 0x10, 1, 
            -1, 0x1c, -1, -1, 15, 1, -1, 30, -1, -1, 14, 1, -1, 0x20, -1, -1, 
            0x13, 1, 0x17, -1, -1, -1, 0x13, 2, 0x16, -1, -1, -1, 0x13, 3, 0x15, -1, 
            -1, -1, 0x13, 4, 0x39, -1, -1, -1, 0x13, 5, 20, -1, -1, -1, 0x13, 6, 
            0x54, -1, -1, -1, 0x18, 1, 30, -1, -1, -1, 0x18, 2, 0x1d, -1, -1, -1, 
            0x18, 3, 0x1c, -1, -1, -1, 0x18, 4, 0x1b, -1, -1, -1, 0x18, 5, 0x1a, -1, 
            -1, -1, 0x4b, 1, 0x25, -1, -1, -1, 0x4b, 2, 0x4c, -1, -1, -1, 0x15, 1, 
            -1, -1, 0x1f, -1, 0x15, 2, -1, -1, 0x24, -1, 0x15, 4, -1, -1, 60, -1, 
            0x15, 5, -1, -1, 0x20, -1, 0x15, 7, -1, -1, 0x23, -1, 0x15, 8, -1, -1, 
            0x3d, -1, 0x15, 10, -1, -1, 0x3e, -1, 0x17, 1, -1, -1, 0x21, -1, 0x17, 2, 
            -1, -1, 0x15, -1, 0x17, 3, -1, -1, 0x22, -1, 0x17, 4, -1, -1, 11, -1, 
            0x17, 5, -1, -1, 12, -1, 0x17, 6, -1, -1, 0x25, -1, 0x17, 7, -1, -1, 
            13, -1, 0x17, 8, -1, -1, 14, -1, 30, 1, -1, -1, -1, 70, 0x1d, 1, 
            -1, -1, -1, 0x48, 0x1c, 1, -1, -1, -1, 0x49, 0x1b, 1, -1, -1, -1, 0x47, 
            0x1a, 1, -1, -1, -1, 0x4a, 0x4a, 1, 0x19, -1, -1, -1, 0x19, 1, 0x49, -1, 
            -1, -1, 0x19, 2, 0x52, -1, -1, -1, 0x42, 1, -1, 13, -1, -1, 0x40, 1, 
            -1, 0x11, -1, -1, 0x41, 1, -1, 0x12, -1, -1, 0x44, 1, 0x43, -1, -1, -1, 
            0x44, 2, 70, -1, -1, -1, 0x44, 4, 0x47, -1, -1, -1, 0x44, 6, 10, -1, 
            -1, -1, 0x44, 7, 12, -1, -1, -1, 0x43, 1, -1, 12, -1, -1, 70, 1, 
            -1, 14, -1, -1, 0x47, 1, -1, 15, -1, -1, 0x3d, 2, 0x3e, -1, -1, -1, 
            0x3d, 5, 11, -1, -1, -1, 0x3e, 1, -1, 0x10, -1, -1, 0x20, 1, 40, -1, 
            -1, -1, 0x20, 2, 0x22, -1, -1, -1, 0x20, 3, 0x21, -1, -1, -1, 40, 1, 
            0x2e, -1, -1, -1, 40, 2, 0x2d, -1, -1, -1, 40, 3, 0x29, -1, -1, -1, 
            40, 4, 0x2c, -1, -1, -1, 40, 5, 0x2b, -1, -1, -1, 40, 6, 0x2a, -1, 
            -1, -1, 0x22, 3, 0x23, -1, -1, -1, 0x22, 2, 0x24, -1, -1, -1, 0x22, 1, 
            -1, 0x4e, -1, -1, 0x3b, 1, 60, -1, -1, -1, 0x3b, 2, 0x2f, -1, -1, -1, 
            0x3b, 3, 0x31, -1, -1, -1, 0x3b, 4, 0x30, -1, -1, -1, 60, 1, -1, 0x31, 
            -1, -1, 60, 2, -1, 60, -1, -1, 60, 3, -1, 0x3a, -1, -1, 60, 4, 
            -1, 0x26, -1, -1, 60, 5, -1, 0x36, -1, -1, 0x2f, 1, -1, 0x43, -1, -1, 
            0x2f, 2, -1, 0x44, -1, -1, 0x2f, 3, -1, 0x42, -1, -1, 0x2f, 4, -1, 0x45, 
            -1, -1, 0x2f, 5, -1, 0x41, -1, -1, 0x30, 1, 0x3a, -1, -1, -1, 0x3a, 1, 
            -1, 0x3d, -1, -1, 0x3a, 2, -1, 0x3e, -1, -1, 0x3a, 3, -1, 0x40, -1, -1, 
            0x3a, 4, -1, 0x3f, -1, -1, 0x31, 5, 0x34, -1, -1, -1, 0x31, 7, 0x37, -1, 
            -1, -1, 0x31, 8, 0x38, 0x24, -1, -1, 0x31, 9, 0x36, -1, -1, -1, 0x31, 10, 
            0x35, 0x25, -1, -1, 0x31, 12, 0x33, -1, -1, -1, 0x31, 13, 50, -1, -1, -1, 
            0x34, 1, 0x48, -1, -1, -1, 0x31, 2, -1, 70, -1, -1, 0x31, 4, -1, 0x4a, 
            -1, -1, 0x31, 6, -1, 0x22, -1, -1, -1, -1, -1, -1, -1, -1
         };
        private CustomSelfDrawPanel.CSDHorzExtendingPanel pointsInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel pointsText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea queuedResearchArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton queuedResearchButton1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton queuedResearchButton2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton queuedResearchButton3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton queuedResearchButton4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton queuedResearchButton5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton queuedResearchButton6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton queuedResearchButton7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage queuedResearchImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage queuedResearchImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage queuedResearchImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage queuedResearchImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage queuedResearchImage5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage queuedResearchImage6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage queuedResearchImage7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel queuedResearchNoPremiumText = new CustomSelfDrawPanel.CSDLabel();
        private Size realScrollImageSize = new Size();
        private bool researchAllowed;
        private bool rowsCreated;
        private CustomSelfDrawPanel.CSDImage scrollPanelBottomLeftOverlay = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelBottomMiddleOverlay = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelBottomRightOverlay = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelLeftOverlay = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelRightOverlay = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelTopLeftOverlay = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelTopMiddleOverlay = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scrollPanelTopRightOverlay = new CustomSelfDrawPanel.CSDImage();
        private int selectedQueueSlot = -1;
        private CustomSelfDrawPanel.CSDButton[] startResearchButtons = new CustomSelfDrawPanel.CSDButton[30];
        private CustomSelfDrawPanel.CSDImage[] startResearchDots = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDImage[] startResearchDotsBack = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDImage[] startResearchDotsYellow = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDLabel[] startResearchHeader = new CustomSelfDrawPanel.CSDLabel[30];
        private CustomSelfDrawPanel.CSDLabel startResearchHeaderBuildingOpen = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel startResearchHeaderMain = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel startResearchHeaderResearchOpen = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage[] startResearchImages = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDImage[] startResearchOpenBackground = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDImage[] startResearchOpenBuilding = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDImage[] startResearchOpenResearch = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDImage[] startResearchOpenResearchOverlay = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDLabel[] startResearchOpenResearchOverlayLabel = new CustomSelfDrawPanel.CSDLabel[30];
        private CustomSelfDrawPanel.CSDVertScrollBar startResearchScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage[] startResearchShield = new CustomSelfDrawPanel.CSDImage[30];
        private CustomSelfDrawPanel.CSDLabel[] startResearchShieldNumber = new CustomSelfDrawPanel.CSDLabel[30];
        private CustomSelfDrawPanel.CSDLabel[] startResearchText1 = new CustomSelfDrawPanel.CSDLabel[30];
        private CustomSelfDrawPanel.CSDLabel[] startResearchText2 = new CustomSelfDrawPanel.CSDLabel[30];
        private CustomSelfDrawPanel.CSDButton tab1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabModeListButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabModeTreeButton = new CustomSelfDrawPanel.CSDButton();
        private int tabType;
        private const int tileBorderX = 40;
        private const int tileBorderY = 40;
        private const int tileSizeX = 150;
        private const int tileSizeY = 110;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel timeInfoBox = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzProgressBar timeProgressBar = new CustomSelfDrawPanel.CSDHorzProgressBar();
        private CustomSelfDrawPanel.CSDLabel timeProgressText = new CustomSelfDrawPanel.CSDLabel();
        private int tooltipToShow = -1;
        private const int topMoveDownOffset = 0x37;
        public static int TUTORIAL_artsTabPos = -10000;
        private double[] windowScalingValues = new double[] { 1.0, 0.5 };

        public ResearchPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.NoDrawBackground = true;
        }

        private void applyData(ResearchData data)
        {
            int researchType = -1;
            int level = 0;
            this.researchAllowed = true;
            int num3 = data.research_points;
            if (data.research_queueEntries != null)
            {
                num3 -= data.research_queueEntries.Length;
            }
            if (data.researchingType >= 0)
            {
                if (!GameEngine.Instance.World.isAccountPremium() || !data.canDoMoreResearch())
                {
                    this.researchAllowed = false;
                }
                DateTime time = VillageMap.getCurrentServerTime();
                if ((this.selectedQueueSlot >= 0) && ((data.research_queueEntries == null) || (this.selectedQueueSlot >= data.research_queueEntries.Length)))
                {
                    this.selectedQueueSlot = -1;
                }
                if (this.selectedQueueSlot < 0)
                {
                    TimeSpan span = (TimeSpan) (data.research_completionTime - time);
                    int secsLeft = (int) (span.TotalSeconds + 0.5);
                    if (secsLeft < 0)
                    {
                        secsLeft = 0;
                    }
                    this.timeProgressText.Text = SK.Text("Research_Completed_In", "Completed In") + " : " + VillageMap.createBuildTimeString(secsLeft);
                    this.timeProgressText.Visible = true;
                    if (GameEngine.Instance.World.isResearchLagging())
                    {
                        this.timeProgressText.Text = this.timeProgressText.Text + " (" + SK.Text("Research_Lagging", "Research Overdue, Please wait") + ")";
                    }
                    TimeSpan span2 = data.calcResearchTime(data.research_pointCount - 1, GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData);
                    if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                    {
                        span2 = new TimeSpan(span2.Ticks / 2L);
                    }
                    int totalSeconds = (int) span2.TotalSeconds;
                    if (totalSeconds < 1)
                    {
                        totalSeconds = 1;
                    }
                    if ((totalSeconds == 30) && (GameEngine.Instance.World.getTutorialStage() == 5))
                    {
                        totalSeconds = 11;
                    }
                    double curValue = span.TotalSeconds;
                    if (curValue < 0.0)
                    {
                        curValue = 0.0;
                    }
                    curValue = totalSeconds - curValue;
                    if (curValue < 0.0)
                    {
                        curValue = 0.0;
                    }
                    this.timeProgressBar.setValues(curValue, (double) totalSeconds);
                    this.currentResearchCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                    researchType = data.researchingType;
                    level = data.research[data.researchingType];
                }
                else
                {
                    TimeSpan span3 = (TimeSpan) (data.research_completionTime - time);
                    if (this.selectedQueueSlot > 0)
                    {
                        for (int i = 0; i < this.selectedQueueSlot; i++)
                        {
                            TimeSpan span4 = data.calcResearchTime(data.research_pointCount + i, GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData);
                            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                            {
                                span4 = new TimeSpan(span4.Ticks / 2L);
                            }
                            span3 += span4;
                        }
                    }
                    int num8 = (int) (span3.TotalSeconds + 0.5);
                    if (num8 < 0)
                    {
                        num8 = 0;
                    }
                    this.timeProgressText.Text = SK.Text("Research_Starts_In", "Starts In") + " : " + VillageMap.createBuildTimeString(num8);
                    this.timeProgressText.Visible = true;
                    this.timeProgressBar.setValues(0.0, 0.0);
                    researchType = data.research_queueEntries[this.selectedQueueSlot];
                    level = 0;
                    this.currentResearchCancelButton.Text.Text = SK.Text("Research_Remove_From_Queue", "Remove From Queue");
                }
                this.currentResearchCancelButton.Enabled = true;
            }
            else
            {
                TimeSpan span5 = data.calcResearchTime(data.research_pointCount, GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData);
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                {
                    span5 = new TimeSpan(span5.Ticks / 2L);
                }
                int num9 = (int) span5.TotalSeconds;
                this.currentResearchCancelButton.Enabled = false;
                this.timeProgressText.Text = SK.Text("Research_Next_Duration", "Next Research Duration") + " : " + VillageMap.createBuildTimeString(num9);
                this.timeProgressBar.setValues(0.0, 0.0);
            }
            if (this.tooltipToShow >= 0)
            {
                researchType = this.tooltipToShow / 0x3e8;
                level = (this.tooltipToShow % 0x3e8) - 1;
            }
            if (researchType >= 0)
            {
                this.currentResearchInfoBoxHeadingText.Visible = true;
                this.currentResearchInfoBoxHeadingText.Text = ResearchData.getResearchName(researchType);
                this.currentResearchText.Text = this.currentResearchInfoBoxHeadingText.Text;
                this.currentResearchText.Visible = true;
                this.currentResearchImage.Image = (Image) this.getIllustration(researchType);
                if (this.currentResearchImage.Image != null)
                {
                    this.currentResearchImage.Visible = true;
                }
                else
                {
                    this.currentResearchImage.Visible = false;
                }
                this.currentResearchInfoBoxRow1Text.Text = ResearchData.getDescriptionText(researchType, level);
                this.currentResearchInfoBoxRow2Text.Text = "";
                this.currentResearchInfoBoxRow3Text.Text = ResearchData.getEffectText(researchType, level, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1, GameEngine.Instance.LocalWorldData);
                this.currentResearchInfoBoxRow1Text.Visible = true;
                this.currentResearchInfoBoxRow2Text.Visible = this.dragOverlay.Visible;
                this.currentResearchInfoBoxRow3Text.Visible = this.dragOverlay.Visible;
                this.currentResearchBackgroundImage.Visible = true;
                this.currentResearchBackgroundImage2.Visible = false;
                if ((Program.mySettings.LanguageIdent == "tr") && ((((researchType == 14) || (researchType == 0x42)) || ((researchType == 0x2e) || (researchType == 0x29))) || ((researchType == 0x2b) || (researchType == 0x2a))))
                {
                    this.currentResearchText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                }
                else if ((Program.mySettings.LanguageIdent == "pl") && (((researchType == 14) || (researchType == 0x25)) || ((researchType == 0x2d) || (researchType == 50))))
                {
                    this.currentResearchText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                }
                else if ((Program.mySettings.LanguageIdent == "it") && (((researchType == 0x11) || (researchType == 0x43)) || (researchType == 0x29)))
                {
                    this.currentResearchText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                }
                else if ((Program.mySettings.LanguageIdent == "pt") && ((((researchType == 0) || (researchType == 0x27)) || ((researchType == 0x11) || (researchType == 0x42))) || ((((researchType == 0x40) || (researchType == 10)) || ((researchType == 0x2b) || (researchType == 0x2c))) || ((researchType == 0x2d) || (researchType == 0x2e)))))
                {
                    this.currentResearchText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                }
                else if ((Program.mySettings.LanguageIdent == "pt") && ((researchType == 0x22) || (researchType == 0x2a)))
                {
                    this.currentResearchText.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                }
                else
                {
                    this.currentResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                }
            }
            else
            {
                this.currentResearchBackgroundImage2.Visible = true;
                this.currentResearchBackgroundImage.Visible = false;
                this.currentResearchInfoBoxRow1Text.Visible = false;
                this.currentResearchInfoBoxRow2Text.Visible = false;
                this.currentResearchInfoBoxRow3Text.Visible = false;
                this.currentResearchText.Visible = false;
                this.currentResearchImage.Visible = false;
                this.currentResearchInfoBoxHeadingText.Text = SK.Text("Research_No_Current", "No current research");
            }
            this.pointsText.Text = SK.Text("Research_Research_Points", "Research Points") + " : " + num3.ToString();
            NumberFormatInfo nFI = GameEngine.NFI;
            if (num3 <= 0)
            {
                this.researchAllowed = false;
            }
            double num10 = data.calcPointGoldCost(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData);
            this.buyPointText.Text = num10.ToString("N", nFI);
            if (num10 <= GameEngine.Instance.World.getCurrentGold())
            {
                this.buyPointButton.Enabled = true;
            }
            else
            {
                this.buyPointButton.Enabled = false;
            }
            this.queuedResearchImage1.Visible = false;
            this.queuedResearchImage2.Visible = false;
            this.queuedResearchImage3.Visible = false;
            this.queuedResearchImage4.Visible = false;
            this.queuedResearchImage5.Visible = false;
            this.queuedResearchImage6.Visible = false;
            this.queuedResearchImage7.Visible = false;
            this.queuedResearchButton1.Enabled = false;
            this.queuedResearchButton2.Enabled = false;
            this.queuedResearchButton3.Enabled = false;
            this.queuedResearchButton4.Enabled = false;
            this.queuedResearchButton5.Enabled = false;
            this.queuedResearchButton6.Enabled = false;
            this.queuedResearchButton7.Enabled = false;
            if (((data.research_queueEntries != null) && (data.research_queueEntries.Length > 0)) && (data.researchingType >= 0))
            {
                for (int j = 0; j < data.research_queueEntries.Length; j++)
                {
                    CustomSelfDrawPanel.CSDImage image = null;
                    CustomSelfDrawPanel.CSDButton button = null;
                    switch (j)
                    {
                        case 0:
                            image = this.queuedResearchImage1;
                            button = this.queuedResearchButton1;
                            break;

                        case 1:
                            image = this.queuedResearchImage2;
                            button = this.queuedResearchButton2;
                            break;

                        case 2:
                            image = this.queuedResearchImage3;
                            button = this.queuedResearchButton3;
                            break;

                        case 3:
                            image = this.queuedResearchImage4;
                            button = this.queuedResearchButton4;
                            break;

                        case 4:
                            image = this.queuedResearchImage5;
                            button = this.queuedResearchButton5;
                            break;

                        case 5:
                            image = this.queuedResearchImage6;
                            button = this.queuedResearchButton6;
                            break;

                        case 6:
                            image = this.queuedResearchImage7;
                            button = this.queuedResearchButton7;
                            break;
                    }
                    image.Visible = true;
                    image.Image = (Image) this.getIllustration(data.research_queueEntries[j]);
                    image.Size = new Size(image.Size.Width / 2, image.Size.Height / 2);
                    image.CustomTooltipID = 0x12e;
                    image.CustomTooltipData = j;
                    button.Enabled = true;
                    button.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
                    button.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
                    button.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
                    button.CustomTooltipID = 0x12e;
                    button.CustomTooltipData = j;
                }
            }
            if (this.selectedQueueSlot >= 0)
            {
                CustomSelfDrawPanel.CSDButton button2 = null;
                switch (this.selectedQueueSlot)
                {
                    case 0:
                        button2 = this.queuedResearchButton1;
                        break;

                    case 1:
                        button2 = this.queuedResearchButton2;
                        break;

                    case 2:
                        button2 = this.queuedResearchButton3;
                        break;

                    case 3:
                        button2 = this.queuedResearchButton4;
                        break;

                    case 4:
                        button2 = this.queuedResearchButton5;
                        break;

                    case 5:
                        button2 = this.queuedResearchButton6;
                        break;

                    case 6:
                        button2 = this.queuedResearchButton7;
                        break;
                }
                button2.ImageNorm = (Image) GFXLibrary.border_research_ill_selected_normal;
                button2.ImageOver = (Image) GFXLibrary.border_research_ill_selected_normal;
                button2.ImageClick = (Image) GFXLibrary.border_research_ill_selected_normal;
            }
            if (GameEngine.Instance.World.isAccountPremium())
            {
                this.queuedResearchNoPremiumText.Visible = false;
            }
            else
            {
                this.queuedResearchNoPremiumText.Visible = true;
            }
        }

        private void backgroundClick()
        {
            this.selectedQueueSlot = -1;
        }

        private void buyPointClick()
        {
            this.selectedQueueSlot = -1;
            GameEngine.Instance.World.buyResearchPoint();
        }

        private void cancelResearch()
        {
            if (this.selectedQueueSlot < 0)
            {
                GameEngine.Instance.World.doResearch(-1);
            }
            else if (((this.lastData != null) && (this.lastData.research_queueEntries != null)) && (this.selectedQueueSlot < this.lastData.research_queueEntries.Length))
            {
                GameEngine.Instance.World.CancelQueuedResearch(this.lastData.research_queueEntries[this.selectedQueueSlot], this.selectedQueueSlot);
            }
            this.selectedQueueSlot = -1;
            InterfaceMgr.Instance.closeGreyOut();
            this.cancelResearchPopup.Close();
        }

        private void cancelResearchClick()
        {
            if ((this.selectedQueueSlot < 0) || (((this.lastData != null) && (this.lastData.research_queueEntries != null)) && (this.selectedQueueSlot < this.lastData.research_queueEntries.Length)))
            {
                this.closeCancelResearchPopup();
                InterfaceMgr.Instance.openGreyOutWindow(false);
                this.cancelResearchPopup = new MyMessageBoxPopUp();
                this.cancelResearchPopup.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("Research_Cancel_Research", "Cancel Research?"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelResearch));
                this.cancelResearchPopup.Show(InterfaceMgr.Instance.getGreyOutWindow());
            }
        }

        private void closeCancelResearchPopup()
        {
            if (this.cancelResearchPopup != null)
            {
                if (this.cancelResearchPopup.Created)
                {
                    this.cancelResearchPopup.Close();
                }
                InterfaceMgr.Instance.closeGreyOut();
                this.cancelResearchPopup = null;
            }
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

        private void createRows(int startColumn, int startRow, int dx, int dy, int[] layout, CustomSelfDrawPanel.CSDImage[][] rows, int numRows, int numColumns)
        {
            GameEngine.Instance.World.getRank();
            for (int i = 0; i < numRows; i++)
            {
                int researchType = layout[i * 2];
                int num3 = layout[(i * 2) + 1];
                int num4 = 1;
                if (researchType >= 0)
                {
                    num4 += ResearchData.getNumLevels(researchType, 0x16, GameEngine.Instance.LocalWorldData);
                }
                int y = (((i * dy) + startRow) * 110) + 40;
                rows[i] = new CustomSelfDrawPanel.CSDImage[numColumns];
                for (int j = 0; j < num4; j++)
                {
                    int x = ((((j + num3) * dx) + startColumn) * 150) + 40;
                    CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage {
                        Position = new Point(x, y)
                    };
                    rows[i][j + num3] = image;
                    if (j == 0)
                    {
                        image.Data = 0;
                        switch (num3)
                        {
                            case 0:
                            {
                                continue;
                            }
                            case 1:
                            {
                                x = (((((j - 1) + num3) * dx) + startColumn) * 150) + 40;
                                CustomSelfDrawPanel.CSDImage image2 = new CustomSelfDrawPanel.CSDImage {
                                    Position = new Point(x, y)
                                };
                                rows[i][(j - 1) + num3] = image2;
                                for (int m = i - 1; m > 0; m--)
                                {
                                    if (rows[m][(j - 1) + num3] != null)
                                    {
                                        break;
                                    }
                                    int num9 = (((m * dy) + startRow) * 110) + 40;
                                    CustomSelfDrawPanel.CSDImage image3 = new CustomSelfDrawPanel.CSDImage {
                                        Position = new Point(x, num9),
                                        Data = 1
                                    };
                                    rows[m][(j - 1) + num3] = image3;
                                }
                                continue;
                            }
                        }
                        for (int k = i - 1; k > 0; k--)
                        {
                            if (rows[k][j + num3] != null)
                            {
                                break;
                            }
                            int num11 = (((k * dy) + startRow) * 110) + 40;
                            CustomSelfDrawPanel.CSDImage image4 = new CustomSelfDrawPanel.CSDImage {
                                Position = new Point(x, num11),
                                Data = 1
                            };
                            rows[k][j + num3] = image4;
                        }
                    }
                    else
                    {
                        image.Data = 2;
                    }
                }
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

        private void dragWindowMouseLeave()
        {
            CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
        }

        private void dragWindowMouseOver()
        {
            CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
        }

        private void dragWindowMouseWheel(int delta)
        {
            double windowScale = this.m_windowScale;
            if (delta < 0)
            {
                if (this.m_windowScaleNotch > 0)
                {
                    this.m_windowScaleNotch--;
                }
            }
            else if ((delta > 0) && (this.m_windowScaleNotch < (this.windowScalingValues.Length - 1)))
            {
                this.m_windowScaleNotch++;
            }
            this.m_windowScale = this.windowScalingValues[this.m_windowScaleNotch];
            double num2 = this.realScrollImageSize.Width * this.m_windowScale;
            double num3 = this.realScrollImageSize.Height * this.m_windowScale;
            if (num2 < this.scrollPanelImage.ClipRect.Width)
            {
                this.scrollPanelImage.Size = new Size((int) (((double) this.scrollPanelImage.ClipRect.Width) / this.m_windowScale), this.scrollPanelImage.Size.Height);
            }
            else
            {
                this.scrollPanelImage.Size = new Size(this.realScrollImageSize.Width, this.scrollPanelImage.Size.Height);
            }
            if (num3 < this.scrollPanelImage.ClipRect.Height)
            {
                this.scrollPanelImage.Size = new Size(this.scrollPanelImage.Size.Width, (int) (((double) this.scrollPanelImage.ClipRect.Height) / this.m_windowScale));
            }
            else
            {
                this.scrollPanelImage.Size = new Size(this.scrollPanelImage.Size.Width, this.realScrollImageSize.Height);
            }
            if (windowScale != this.m_windowScale)
            {
                this.rescaleWindow(windowScale, this.m_windowScale);
            }
        }

        private BaseImage getBuildingGFX(int building)
        {
            switch (building)
            {
                case 6:
                    return GFXLibrary.r_building_panel_bld_icon_ind_woodcutters_hut;

                case 7:
                    return GFXLibrary.r_building_panel_bld_icon_ind_stone_quarry;

                case 8:
                    return GFXLibrary.r_building_panel_bld_icon_ind_iron_mine;

                case 9:
                    return GFXLibrary.r_building_panel_bld_icon_ind_pitch_rig;

                case 12:
                    return GFXLibrary.r_building_panel_bld_icon_food_brewery;

                case 13:
                    return GFXLibrary.r_building_panel_bld_icon_food_apple_orchard;

                case 14:
                    return GFXLibrary.r_building_panel_bld_icon_food_bakery;

                case 15:
                    return GFXLibrary.r_building_panel_bld_icon_food_vegetable_farm;

                case 0x10:
                    return GFXLibrary.r_building_panel_bld_icon_food_pig_farm;

                case 0x11:
                    return GFXLibrary.r_building_panel_bld_icon_food_dairy_farm;

                case 0x12:
                    return GFXLibrary.r_building_panel_bld_icon_food_fishing_jetty;

                case 0x13:
                    return GFXLibrary.r_building_panel_bld_icon_hon_tailers_workshop;

                case 0x15:
                    return GFXLibrary.r_building_panel_bld_icon_hon_carpenters_workshop;

                case 0x16:
                    return GFXLibrary.r_building_panel_bld_icon_hon_hunters_hut;

                case 0x17:
                    return GFXLibrary.r_building_panel_bld_icon_hon_salt_pan;

                case 0x18:
                    return GFXLibrary.r_building_panel_bld_icon_hon_spice_docs;

                case 0x19:
                    return GFXLibrary.r_building_panel_bld_icon_hon_silk_docs;

                case 0x1a:
                    return GFXLibrary.r_building_panel_bld_icon_hon_metalworks_workshop;

                case 0x1c:
                    return GFXLibrary.r_building_panel_bld_icon_mil_pole_turner;

                case 0x1d:
                    return GFXLibrary.r_building_panel_bld_icon_mil_fletcher;

                case 30:
                    return GFXLibrary.r_building_panel_bld_icon_mil_blacksmith;

                case 0x1f:
                    return GFXLibrary.r_building_panel_bld_icon_mil_armourer;

                case 0x20:
                    return GFXLibrary.r_building_panel_bld_icon_mil_siege_workshop;

                case 0x21:
                    return GFXLibrary.r_building_panel_bld_icon_hon_vinyard;

                case 0x22:
                    return GFXLibrary.r_building_panel_bld_civ_rel_small_church;

                case 0x23:
                    return GFXLibrary.r_building_panel_bld_icon_food_inn;

                case 0x24:
                    return GFXLibrary.r_building_panel_bld_civ_rel_medium_church;

                case 0x25:
                    return GFXLibrary.r_building_panel_bld_civ_rel_large_church;

                case 0x26:
                case 0x29:
                case 0x2a:
                case 0x2b:
                case 0x2c:
                case 0x2d:
                    return GFXLibrary.r_building_panel_bld_civ_dec_small_garden_01;

                case 0x31:
                case 50:
                case 0x33:
                    return GFXLibrary.r_building_panel_bld_civ_dec_large_garden_01png;

                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                    return GFXLibrary.r_building_panel_bld_civ_dec_small_statue_01;

                case 0x3a:
                case 0x3b:
                    return GFXLibrary.r_building_panel_bld_civ_dec_large_statue_01;

                case 60:
                    return GFXLibrary.r_building_panel_bld_civ_dec_dovecote;

                case 0x3d:
                    return GFXLibrary.r_building_panel_bld_jus_stocks;

                case 0x3e:
                    return GFXLibrary.r_building_panel_bld_jus_burning_post;

                case 0x3f:
                    return GFXLibrary.r_building_panel_bld_jus_gibbet;

                case 0x40:
                    return GFXLibrary.r_building_panel_bld_jus_stretching_rack;

                case 0x41:
                    return GFXLibrary.r_building_panel_bld_ent_maypole;

                case 0x42:
                    return GFXLibrary.r_building_panel_bld_ent_dancing_bear;

                case 0x43:
                    return GFXLibrary.r_building_panel_bld_ent_theatre;

                case 0x44:
                    return GFXLibrary.r_building_panel_bld_ent_jesters_court;

                case 0x45:
                    return GFXLibrary.r_building_panel_bld_ent_troubadours_arbor;

                case 70:
                case 0x47:
                case 0x48:
                case 0x49:
                    return GFXLibrary.r_building_panel_bld_civ_rel_small_shrines_01;

                case 0x4a:
                case 0x4b:
                    return GFXLibrary.r_building_panel_bld_civ_rel_large_shrines_01;

                case 0x4e:
                    return GFXLibrary.r_building_panel_bld_icon_ind_market;
            }
            return null;
        }

        private BaseImage getCastleGFX(int castlePiece)
        {
            switch (castlePiece)
            {
                case 11:
                    return GFXLibrary.r_building_miltary_lookouttower;

                case 12:
                    return GFXLibrary.r_building_miltary_smalltower;

                case 13:
                    return GFXLibrary.r_building_miltary_largetower;

                case 14:
                    return GFXLibrary.r_building_miltary_greattower;

                case 0x15:
                    return GFXLibrary.r_building_miltary_woodtower;

                case 0x1f:
                    return GFXLibrary.r_building_miltary_guardhouse;

                case 0x20:
                    return GFXLibrary.r_building_miltary_smelter;

                case 0x21:
                    return GFXLibrary.r_building_miltary_woodwall;

                case 0x22:
                    return GFXLibrary.r_building_miltary_stonewall;

                case 0x23:
                    return GFXLibrary.r_building_miltary_moat;

                case 0x24:
                    return GFXLibrary.r_building_miltary_killingpits;

                case 0x25:
                    return GFXLibrary.r_building_miltary_gatehouse;

                case 60:
                    return GFXLibrary.r_bld_icon_mil_guardhouse_2;

                case 0x3d:
                    return GFXLibrary.r_bld_icon_mil_guardhouse_3;

                case 0x3e:
                    return GFXLibrary.r_bld_icon_mil_guardhouse_4;

                case 70:
                    return GFXLibrary.r_building_miltary_peasent;

                case 0x47:
                    return GFXLibrary.r_building_miltary_swordsman;

                case 0x48:
                    return GFXLibrary.r_building_miltary_archer;

                case 0x49:
                    return GFXLibrary.r_building_miltary_pikemen;

                case 0x4a:
                    return GFXLibrary.r_building_miltary_catapult;
            }
            return null;
        }

        private BaseImage getCircle(int dx, int dy, int mode, int up, int left, int down, int right, int research, int level)
        {
            if (dx < 0)
            {
                int num = left;
                left = right;
                right = num;
            }
            if (dy < 0)
            {
                int num2 = up;
                up = down;
                down = num2;
            }
            if (mode == 0)
            {
                if (((left == 1) && (down == 1)) && (right == 2))
                {
                    return GFXLibrary.mix_gcf_0011_bl_0100;
                }
                if ((((left == 1) && (right == 1)) && ((up == 2) || (down == 2))) && (down == 2))
                {
                    return GFXLibrary.mix_gcf_0101_bl_0010;
                }
                if ((left == 1) && (((up == 2) || (right == 2)) || (down == 2)))
                {
                    if (right == 2)
                    {
                        if (down == 2)
                        {
                            return GFXLibrary.mix_gcf_0001_bl_0110;
                        }
                        return GFXLibrary.mix_gcf_0001_bl_0100;
                    }
                    if (down == 2)
                    {
                        return GFXLibrary.mix_gcf_0001_bl_0010;
                    }
                }
                if (down != 0)
                {
                    if (right != 0)
                    {
                        return GFXLibrary.gcf_0111;
                    }
                    return GFXLibrary.gcf_0011;
                }
                if (right != 0)
                {
                    return GFXLibrary.gcf_0101;
                }
                return GFXLibrary.gcf_0001;
            }
            if (mode == 1)
            {
                if (((up == 0) && (right == 0)) && ((down == 2) && (left == 1)))
                {
                    return GFXLibrary.mix_gch_0001_bl_0010;
                }
                if (((up == 0) && (right == 2)) && ((down == 0) && (left == 1)))
                {
                    return GFXLibrary.mix_gch_0001_bl_0100;
                }
                if (((up == 0) && (right == 2)) && ((down == 2) && (left == 1)))
                {
                    return GFXLibrary.mix_gch_0001_bl_0110;
                }
                if (down != 0)
                {
                    if (right != 0)
                    {
                        return GFXLibrary.gch_0111;
                    }
                    return GFXLibrary.gch_0011;
                }
                if (right != 0)
                {
                    return GFXLibrary.gch_0101;
                }
                return GFXLibrary.gch_0001;
            }
            if (down != 0)
            {
                if (right != 0)
                {
                    return GFXLibrary.bcf_0111;
                }
                return GFXLibrary.bcf_0011;
            }
            if (right != 0)
            {
                return GFXLibrary.bcf_0101;
            }
            return GFXLibrary.bcf_0001;
        }

        private BaseImage getIllBack(int dx, int dy, int up, int left, int down, int right, int research)
        {
            if (dx < 0)
            {
                int num = left;
                left = right;
                right = num;
            }
            if (dy < 0)
            {
                int num2 = up;
                up = down;
                down = num2;
            }
            if (up != 0)
            {
                if (left != 0)
                {
                    if ((left != 2) && (up != 2))
                    {
                        return GFXLibrary.ill_back_gline_1001;
                    }
                    return GFXLibrary.ill_back_bline_1001;
                }
                if (right != 0)
                {
                    if ((right != 2) && (up != 2))
                    {
                        return GFXLibrary.ill_back_gline_1100;
                    }
                    return GFXLibrary.ill_back_bline_1100;
                }
                if (down != 0)
                {
                    if ((down != 2) && (up != 2))
                    {
                        return GFXLibrary.ill_back_gline_1010;
                    }
                    return GFXLibrary.ill_back_bline_1010;
                }
                if (up == 2)
                {
                    return GFXLibrary.ill_back_bline_1000;
                }
                return GFXLibrary.ill_back_gline_1000;
            }
            if (down != 0)
            {
                if (left != 0)
                {
                    if ((left != 2) && (down != 2))
                    {
                        return GFXLibrary.ill_back_gline_0011;
                    }
                    return GFXLibrary.ill_back_bline_0011;
                }
                if (right != 0)
                {
                    if ((right != 2) && (down != 2))
                    {
                        return GFXLibrary.ill_back_gline_0110;
                    }
                    return GFXLibrary.ill_back_bline_0110;
                }
                if (down == 2)
                {
                    return GFXLibrary.ill_back_bline_0010;
                }
                return GFXLibrary.ill_back_gline_0010;
            }
            if (left != 0)
            {
                if (right != 0)
                {
                    if ((left != 2) && (right != 2))
                    {
                        return GFXLibrary.ill_back_gline_0101;
                    }
                    return GFXLibrary.ill_back_bline_0101;
                }
                if (left == 2)
                {
                    return GFXLibrary.ill_back_bline_0001;
                }
                return GFXLibrary.ill_back_gline_0001;
            }
            if (right == 0)
            {
                return GFXLibrary.ill_back_bline_0000;
            }
            if (right == 2)
            {
                return GFXLibrary.ill_back_bline_0100;
            }
            return GFXLibrary.ill_back_gline_0100;
        }

        private BaseImage getIllustration(int researchType)
        {
            switch (researchType)
            {
                case 0:
                    return GFXLibrary.research_ill_stone_quarrying;

                case 1:
                    return GFXLibrary.research_ill_forestry;

                case 2:
                    return GFXLibrary.research_ill_iron_mining;

                case 3:
                    return GFXLibrary.research_ill_pitch_extraction;

                case 4:
                    return GFXLibrary.research_ill_tools;

                case 5:
                    return GFXLibrary.research_ill_salt_working;

                case 6:
                    return GFXLibrary.research_ill_craftsmanship;

                case 7:
                    return GFXLibrary.research_ill_tailoring;

                case 8:
                    return GFXLibrary.research_ill_carpentry;

                case 9:
                    return GFXLibrary.research_ill_metal_working;

                case 10:
                    return GFXLibrary.research_ill_brewing;

                case 11:
                    return GFXLibrary.research_ill_butchery;

                case 12:
                    return GFXLibrary.research_ill_bakery;

                case 13:
                    return GFXLibrary.research_ill_weapon_making;

                case 14:
                    return GFXLibrary.research_ill_siege_mechanics;

                case 15:
                    return GFXLibrary.research_ill_blacksmithing;

                case 0x10:
                    return GFXLibrary.research_ill_pole_turning;

                case 0x11:
                    return GFXLibrary.research_ill_armour_working;

                case 0x12:
                    return GFXLibrary.research_ill_fletching;

                case 0x13:
                    return GFXLibrary.research_ill_castellation;

                case 20:
                    return GFXLibrary.research_ill_construction;

                case 0x15:
                    return GFXLibrary.research_ill_defences;

                case 0x16:
                    return GFXLibrary.research_ill_vaults;

                case 0x17:
                    return GFXLibrary.research_ill_fortification;

                case 0x18:
                    return GFXLibrary.research_ill_command;

                case 0x19:
                    return GFXLibrary.research_ill_captains;

                case 0x1a:
                    return GFXLibrary.research_ill_catapult;

                case 0x1b:
                    return GFXLibrary.research_ill_sword;

                case 0x1c:
                    return GFXLibrary.research_ill_pike;

                case 0x1d:
                    return GFXLibrary.research_ill_longbow;

                case 30:
                    return GFXLibrary.research_ill_conscription;

                case 0x1f:
                    return GFXLibrary.research_ill_espionage;

                case 0x20:
                    return GFXLibrary.research_ill_mathematics;

                case 0x21:
                    return GFXLibrary.research_ill_architecture;

                case 0x22:
                    return GFXLibrary.research_ill_commerce;

                case 0x23:
                    return GFXLibrary.research_ill_trade_agreements;

                case 0x24:
                    return GFXLibrary.research_ill_land_trade;

                case 0x25:
                    return GFXLibrary.research_ill_foraging;

                case 0x26:
                    return GFXLibrary.research_ill_silk_trade;

                case 0x27:
                    return GFXLibrary.research_ill_spice_trade;

                case 40:
                    return GFXLibrary.research_ill_engineering;

                case 0x29:
                    return GFXLibrary.research_ill_hall_capacity;

                case 0x2a:
                    return GFXLibrary.research_ill_housing_capacity;

                case 0x2b:
                    return GFXLibrary.research_ill_armoury_capacity;

                case 0x2c:
                    return GFXLibrary.research_ill_inn_capacity;

                case 0x2d:
                    return GFXLibrary.research_ill_granary_capacity;

                case 0x2e:
                    return GFXLibrary.research_ill_stockpile_capacity;

                case 0x2f:
                    return GFXLibrary.research_ill_literature;

                case 0x30:
                    return GFXLibrary.research_ill_philosophy;

                case 0x31:
                    return GFXLibrary.research_ill_theology;

                case 50:
                    return GFXLibrary.research_ill_extreme_unction;

                case 0x33:
                    return GFXLibrary.research_ill_confession;

                case 0x34:
                    return GFXLibrary.research_ill_ordination;

                case 0x35:
                    return GFXLibrary.research_ill_eucharist;

                case 0x36:
                    return GFXLibrary.research_ill_confirmation;

                case 0x37:
                    return GFXLibrary.research_ill_baptism;

                case 0x38:
                    return GFXLibrary.research_ill_marriage;

                case 0x39:
                    return GFXLibrary.research_ill_diplomacy;

                case 0x3a:
                    return GFXLibrary.research_ill_justice;

                case 0x3b:
                    return GFXLibrary.research_ill_arts;

                case 60:
                    return GFXLibrary.research_ill_gardening;

                case 0x3d:
                    return GFXLibrary.research_ill_animal_husbandry;

                case 0x3e:
                    return GFXLibrary.research_ill_pig_breeding;

                case 0x3f:
                    return GFXLibrary.research_ill_hunting;

                case 0x40:
                    return GFXLibrary.research_ill_dairy_farming;

                case 0x41:
                    return GFXLibrary.research_ill_fishing;

                case 0x42:
                    return GFXLibrary.research_ill_apple_farming;

                case 0x43:
                    return GFXLibrary.research_ill_hops_farming;

                case 0x44:
                    return GFXLibrary.research_ill_plough;

                case 0x45:
                    return GFXLibrary.research_ill_wine_production;

                case 70:
                    return GFXLibrary.research_ill_wheat_farming;

                case 0x47:
                    return GFXLibrary.research_ill_vegetable_cropping;

                case 0x48:
                    return GFXLibrary.research_ill_pilgrimage;

                case 0x49:
                    return GFXLibrary.research_ill_tactics;

                case 0x4a:
                    return GFXLibrary.research_ill_leadership;

                case 0x4b:
                    return GFXLibrary.research_ill_scouts;

                case 0x4c:
                    return GFXLibrary.research_ill_horsemanship;

                case 0x4d:
                    return GFXLibrary.research_ill_surveillance;

                case 0x4e:
                    return GFXLibrary.research_ill_pillage;

                case 0x4f:
                    return GFXLibrary.research_ill_intelligence_gathering;

                case 80:
                    return GFXLibrary.research_ill_counter_surveillance;

                case 0x51:
                    return GFXLibrary.research_ill_bounties;

                case 0x52:
                    return GFXLibrary.research_ill_logistics;

                case 0x53:
                    return GFXLibrary.research_ill_civil_service;

                case 0x54:
                    return GFXLibrary.research_ill_sally_forth;

                case 0x55:
                    return GFXLibrary.research_ill_ransacking;

                case 0x56:
                    return GFXLibrary.research_ill_forced_march;
            }
            return null;
        }

        private CustomSelfDrawPanel.CSDImage getNextImage()
        {
            if (this.curImageID >= this.imageCache.Count)
            {
                CustomSelfDrawPanel.CSDImage item = new CustomSelfDrawPanel.CSDImage();
                this.imageCache.Add(item);
                this.curImageID++;
                return item;
            }
            this.curImageID++;
            return this.imageCache[this.curImageID - 1];
        }

        private CustomSelfDrawPanel.CSDLabel getNextLabel()
        {
            if (this.curLabelID >= this.labelCache.Count)
            {
                CustomSelfDrawPanel.CSDLabel item = new CustomSelfDrawPanel.CSDLabel();
                this.labelCache.Add(item);
                this.curLabelID++;
                return item;
            }
            this.curLabelID++;
            return this.labelCache[this.curLabelID - 1];
        }

        private BaseImage getNumberImage(int number, int colour)
        {
            switch (number)
            {
                case 1:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_1_olive;
                        }
                        return GFXLibrary.tech_number_1_tan;
                    }
                    return GFXLibrary.tech_number_1_green;

                case 2:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_2_olive;
                        }
                        return GFXLibrary.tech_number_2_tan;
                    }
                    return GFXLibrary.tech_number_2_green;

                case 3:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_3_olive;
                        }
                        return GFXLibrary.tech_number_3_tan;
                    }
                    return GFXLibrary.tech_number_3_green;

                case 4:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_4_olive;
                        }
                        return GFXLibrary.tech_number_4_tan;
                    }
                    return GFXLibrary.tech_number_4_green;

                case 5:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_5_olive;
                        }
                        return GFXLibrary.tech_number_5_tan;
                    }
                    return GFXLibrary.tech_number_5_green;

                case 6:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_6_olive;
                        }
                        return GFXLibrary.tech_number_6_tan;
                    }
                    return GFXLibrary.tech_number_6_green;

                case 7:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_7_olive;
                        }
                        return GFXLibrary.tech_number_7_tan;
                    }
                    return GFXLibrary.tech_number_7_green;

                case 8:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_8_olive;
                        }
                        return GFXLibrary.tech_number_8_tan;
                    }
                    return GFXLibrary.tech_number_8_green;

                case 9:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_9_olive;
                        }
                        return GFXLibrary.tech_number_9_tan;
                    }
                    return GFXLibrary.tech_number_9_green;

                case 10:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_10_olive;
                        }
                        return GFXLibrary.tech_number_10_tan;
                    }
                    return GFXLibrary.tech_number_10_green;

                case 11:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_11_olive;
                        }
                        return GFXLibrary.tech_number_11_tan;
                    }
                    return GFXLibrary.tech_number_11_green;

                case 12:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_12_olive;
                        }
                        return GFXLibrary.tech_number_12_tan;
                    }
                    return GFXLibrary.tech_number_12_green;

                case 13:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_13_olive;
                        }
                        return GFXLibrary.tech_number_13_tan;
                    }
                    return GFXLibrary.tech_number_13_green;

                case 14:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_14_olive;
                        }
                        return GFXLibrary.tech_number_14_tan;
                    }
                    return GFXLibrary.tech_number_14_green;

                case 15:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_15_olive;
                        }
                        return GFXLibrary.tech_number_15_tan;
                    }
                    return GFXLibrary.tech_number_15_green;

                case 0x10:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_number_16_olive;
                        }
                        return GFXLibrary.tech_number_16_tan;
                    }
                    return GFXLibrary.tech_number_16_green;

                case 0x11:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[20];
                        }
                        return GFXLibrary.tech_numbers[0];
                    }
                    return GFXLibrary.tech_numbers[10];

                case 0x12:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x15];
                        }
                        return GFXLibrary.tech_numbers[1];
                    }
                    return GFXLibrary.tech_numbers[11];

                case 0x13:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x16];
                        }
                        return GFXLibrary.tech_numbers[2];
                    }
                    return GFXLibrary.tech_numbers[12];

                case 20:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x17];
                        }
                        return GFXLibrary.tech_numbers[3];
                    }
                    return GFXLibrary.tech_numbers[13];

                case 0x15:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x18];
                        }
                        return GFXLibrary.tech_numbers[4];
                    }
                    return GFXLibrary.tech_numbers[14];

                case 0x16:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x19];
                        }
                        return GFXLibrary.tech_numbers[5];
                    }
                    return GFXLibrary.tech_numbers[15];

                case 0x17:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x1a];
                        }
                        return GFXLibrary.tech_numbers[6];
                    }
                    return GFXLibrary.tech_numbers[0x10];

                case 0x18:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x1b];
                        }
                        return GFXLibrary.tech_numbers[7];
                    }
                    return GFXLibrary.tech_numbers[0x11];

                case 0x19:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x1c];
                        }
                        return GFXLibrary.tech_numbers[8];
                    }
                    return GFXLibrary.tech_numbers[0x12];

                case 0x1a:
                    if (colour != 0)
                    {
                        if (colour == 1)
                        {
                            return GFXLibrary.tech_numbers[0x1d];
                        }
                        return GFXLibrary.tech_numbers[9];
                    }
                    return GFXLibrary.tech_numbers[0x13];
            }
            return GFXLibrary.tech_number_1_green;
        }

        private int getOpenedResearch(int research, int level, ref int openedBuilding, ref int openedCastleBuilding, ref int openedTroop)
        {
            for (int i = 0; this.openedResearches[i * 6] >= 0; i++)
            {
                if ((research == this.openedResearches[i * 6]) && (level == this.openedResearches[(i * 6) + 1]))
                {
                    openedBuilding = this.openedResearches[(i * 6) + 3];
                    openedCastleBuilding = this.openedResearches[(i * 6) + 4];
                    openedTroop = this.openedResearches[(i * 6) + 5];
                    if (((research == 0x31) && (this.openedResearches[(i * 6) + 2] == 0x35)) && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1))
                    {
                        return -1;
                    }
                    return this.openedResearches[(i * 6) + 2];
                }
            }
            return -1;
        }

        private BaseImage getTransitionCircle(int dx, int dy, int mode, int up, int left, int down, int right, int research, int level)
        {
            if (dx < 0)
            {
                int num = left;
                left = right;
                right = num;
            }
            if (dy < 0)
            {
                int num2 = up;
                up = down;
                down = num2;
            }
            if (((left == 1) && (down == 1)) && (right == 2))
            {
                return GFXLibrary.ycf_0g1G;
            }
            if ((((left == 1) && (right == 1)) && ((up == 2) || (down == 2))) && (down == 2))
            {
                return GFXLibrary.ycf_01gG;
            }
            if ((left == 1) && (((up == 2) || (right == 2)) || (down == 2)))
            {
                if (right == 2)
                {
                    if (down == 2)
                    {
                        return GFXLibrary.ycf_0ggG;
                    }
                    return GFXLibrary.mix_ycf_000G_bl_0100;
                }
                if (down == 2)
                {
                    return GFXLibrary.ycf_00gG;
                }
            }
            if (down != 0)
            {
                if (right != 0)
                {
                    return GFXLibrary.ycf_011G;
                }
                return GFXLibrary.ycf_001G;
            }
            if (right != 0)
            {
                return GFXLibrary.ycf_010G;
            }
            return GFXLibrary.ycf_000G;
        }

        private BaseImage getYellowCircle(int dx, int dy, int mode, int up, int left, int down, int right, int research, int level)
        {
            if (dx < 0)
            {
                int num = left;
                left = right;
                right = num;
            }
            if (dy < 0)
            {
                int num2 = up;
                up = down;
                down = num2;
            }
            if (mode == 0)
            {
                if (((left == 1) && (down == 1)) && (right == 2))
                {
                    return GFXLibrary.mix_ycf_0011_bl_0100;
                }
                if ((((left == 1) && (right == 1)) && ((up == 2) || (down == 2))) && (down == 2))
                {
                    return GFXLibrary.mix_ycf_0101_bl_0010;
                }
                if ((left == 1) && (((up == 2) || (right == 2)) || (down == 2)))
                {
                    if (right == 2)
                    {
                        if (down == 2)
                        {
                            return GFXLibrary.mix_ycf_0001_bl_0110;
                        }
                        return GFXLibrary.mix_ycf_0001_bl_0100;
                    }
                    if (down == 2)
                    {
                        return GFXLibrary.mix_ycf_0001_bl_0010;
                    }
                }
                if (down != 0)
                {
                    if (right != 0)
                    {
                        return GFXLibrary.ycf_0111;
                    }
                    return GFXLibrary.ycf_0011;
                }
                if (right != 0)
                {
                    return GFXLibrary.ycf_0101;
                }
                return GFXLibrary.ycf_0001;
            }
            if (mode != 1)
            {
                return GFXLibrary.bcf_0001;
            }
            if (((up == 0) && (right == 0)) && ((down == 2) && (left == 1)))
            {
                return GFXLibrary.mix_ych_0001_bl_0010;
            }
            if (((up == 0) && (right == 2)) && ((down == 0) && (left == 1)))
            {
                return GFXLibrary.mix_ych_0001_bl_0100;
            }
            if (((up == 0) && (right == 2)) && ((down == 2) && (left == 1)))
            {
                return GFXLibrary.mix_ych_0001_bl_0110;
            }
            return GFXLibrary.ych_0001;
        }

        public void init()
        {
            base.clearControls();
            base.Size = InterfaceMgr.Instance.getMainWindowSize();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_001;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            this.mainBackgroundImage.ClipRect = new Rectangle(0, 0, base.Size.Width, base.Size.Height);
            this.mainBackgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.backgroundClick));
            base.addControl(this.mainBackgroundImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x11, new Point(base.Width - 0x2c, 3));
            int num = 0x2a;
            this.currentResearchBackgroundImage.Image = (Image) GFXLibrary.ill_back_bline_0000;
            this.currentResearchBackgroundImage.Position = new Point(0x13, (2 + num) + 20);
            this.mainBackgroundImage.addControl(this.currentResearchBackgroundImage);
            this.currentResearchBackgroundImage2.Image = (Image) GFXLibrary.research_ill_none;
            this.currentResearchBackgroundImage2.Position = new Point(0x13, (2 + num) + 20);
            this.mainBackgroundImage.addControl(this.currentResearchBackgroundImage2);
            this.currentResearchImage.Position = new Point(4, 8);
            this.currentResearchImage.Visible = false;
            this.currentResearchBackgroundImage.addControl(this.currentResearchImage);
            this.currentResearchingBarImage.Image = (Image) GFXLibrary.ill_back_green_textback;
            this.currentResearchingBarImage.Position = new Point(4, 0x44);
            this.currentResearchBackgroundImage.addControl(this.currentResearchingBarImage);
            this.currentResearchText.Text = "";
            this.currentResearchText.Color = ARGBColors.Black;
            this.currentResearchText.Position = new Point(0, 0);
            this.currentResearchText.Size = new Size(this.currentResearchingBarImage.Width, this.currentResearchingBarImage.Height);
            this.currentResearchText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.currentResearchText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.currentResearchingBarImage.addControl(this.currentResearchText);
            this.currentResearchCancelButton.ImageNorm = (Image) GFXLibrary.techtree_button_normal;
            this.currentResearchCancelButton.ImageOver = (Image) GFXLibrary.techtree_button_over;
            this.currentResearchCancelButton.ImageClick = (Image) GFXLibrary.techtree_button_in;
            this.currentResearchCancelButton.Position = new Point(0x11, 0x7b + num);
            this.currentResearchCancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.currentResearchCancelButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.currentResearchCancelButton.TextYOffset = 1;
            this.currentResearchCancelButton.Text.Color = ARGBColors.Black;
            this.currentResearchCancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelResearchClick), "ResearchPanel_cancel");
            this.mainBackgroundImage.addControl(this.currentResearchCancelButton);
            this.currentResearchInfoBox.Size = new Size((((base.Width - 450) + 2) + 15) + 15, 0x5b);
            this.currentResearchInfoBox.Position = new Point(0xb3, 0x1b + num);
            this.mainBackgroundImage.addControl(this.currentResearchInfoBox);
            this.currentResearchInfoBox.Create((Image) GFXLibrary.tech_tree_inset_tall_left, (Image) GFXLibrary.tech_tree_inset_tall_mid, (Image) GFXLibrary.tech_tree_inset_tall_right);
            this.currentResearchInfoBoxHeadingText.Text = "";
            this.currentResearchInfoBoxHeadingText.Color = Color.FromArgb(0xfe, 230, 0xc0);
            this.currentResearchInfoBoxHeadingText.Position = new Point(20, 8);
            this.currentResearchInfoBoxHeadingText.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
            this.currentResearchInfoBoxHeadingText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.currentResearchInfoBoxHeadingText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.currentResearchInfoBox.addControl(this.currentResearchInfoBoxHeadingText);
            this.currentResearchInfoBoxRow1Text.Text = "Row of Text 1";
            this.currentResearchInfoBoxRow1Text.Color = Color.FromArgb(0xfe, 230, 0xc0);
            this.currentResearchInfoBoxRow1Text.Position = new Point(20, 30);
            this.currentResearchInfoBoxRow1Text.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
            this.currentResearchInfoBoxRow1Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.currentResearchInfoBoxRow1Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.currentResearchInfoBox.addControl(this.currentResearchInfoBoxRow1Text);
            this.currentResearchInfoBoxRow2Text.Text = "Row of Text 2";
            this.currentResearchInfoBoxRow2Text.Color = Color.FromArgb(0xfe, 230, 0xc0);
            this.currentResearchInfoBoxRow2Text.Position = new Point(20, 0x31);
            this.currentResearchInfoBoxRow2Text.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
            this.currentResearchInfoBoxRow2Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.currentResearchInfoBoxRow2Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.currentResearchInfoBox.addControl(this.currentResearchInfoBoxRow2Text);
            this.currentResearchInfoBoxRow3Text.Text = "Row of Text 3";
            this.currentResearchInfoBoxRow3Text.Color = Color.FromArgb(0xfe, 230, 0xc0);
            this.currentResearchInfoBoxRow3Text.Position = new Point(20, 0x42);
            this.currentResearchInfoBoxRow3Text.Size = new Size(this.currentResearchInfoBox.Width - 40, this.currentResearchingBarImage.Height);
            this.currentResearchInfoBoxRow3Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.currentResearchInfoBoxRow3Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.currentResearchInfoBox.addControl(this.currentResearchInfoBoxRow3Text);
            this.queuedResearchArea.Position = new Point(5, 0x38);
            this.queuedResearchArea.Size = new Size(500, 50);
            this.currentResearchInfoBox.addControl(this.queuedResearchArea);
            this.queuedResearchImage1.Image = (Image) GFXLibrary.research_ill_wine_production;
            this.queuedResearchImage1.Size = new Size(this.queuedResearchImage1.Size.Width / 2, this.queuedResearchImage1.Size.Height / 2);
            this.queuedResearchImage1.Position = new Point(5, 5);
            this.queuedResearchImage1.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchImage1);
            this.queuedResearchImage2.Image = (Image) GFXLibrary.research_ill_wine_production;
            this.queuedResearchImage2.Size = new Size(this.queuedResearchImage2.Size.Width / 2, this.queuedResearchImage2.Size.Height / 2);
            this.queuedResearchImage2.Position = new Point(0x56, 5);
            this.queuedResearchImage2.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchImage2);
            this.queuedResearchImage3.Image = (Image) GFXLibrary.research_ill_wine_production;
            this.queuedResearchImage3.Size = new Size(this.queuedResearchImage3.Size.Width / 2, this.queuedResearchImage3.Size.Height / 2);
            this.queuedResearchImage3.Position = new Point(0xa7, 5);
            this.queuedResearchImage3.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchImage3);
            this.queuedResearchImage4.Image = (Image) GFXLibrary.research_ill_wine_production;
            this.queuedResearchImage4.Size = new Size(this.queuedResearchImage4.Size.Width / 2, this.queuedResearchImage4.Size.Height / 2);
            this.queuedResearchImage4.Position = new Point(0xf8, 5);
            this.queuedResearchImage4.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchImage4);
            this.queuedResearchImage5.Image = (Image) GFXLibrary.research_ill_wine_production;
            this.queuedResearchImage5.Size = new Size(this.queuedResearchImage5.Size.Width / 2, this.queuedResearchImage5.Size.Height / 2);
            this.queuedResearchImage5.Position = new Point(0x149, 5);
            this.queuedResearchImage5.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchImage5);
            this.queuedResearchImage6.Image = (Image) GFXLibrary.research_ill_wine_production;
            this.queuedResearchImage6.Size = new Size(this.queuedResearchImage6.Size.Width / 2, this.queuedResearchImage6.Size.Height / 2);
            this.queuedResearchImage6.Position = new Point(410, 5);
            this.queuedResearchImage6.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchImage6);
            this.queuedResearchImage7.Image = (Image) GFXLibrary.research_ill_wine_production;
            this.queuedResearchImage7.Size = new Size(this.queuedResearchImage7.Size.Width / 2, this.queuedResearchImage7.Size.Height / 2);
            this.queuedResearchImage7.Position = new Point(0x1eb, 5);
            this.queuedResearchImage7.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchImage7);
            this.queuedResearchButton1.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
            this.queuedResearchButton1.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton1.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton1.Position = new Point(4, 4);
            this.queuedResearchButton1.Data = 0;
            this.queuedResearchButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
            this.queuedResearchArea.addControl(this.queuedResearchButton1);
            this.queuedResearchButton2.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
            this.queuedResearchButton2.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton2.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton2.Position = new Point(0x55, 4);
            this.queuedResearchButton2.Data = 1;
            this.queuedResearchButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
            this.queuedResearchArea.addControl(this.queuedResearchButton2);
            this.queuedResearchButton3.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
            this.queuedResearchButton3.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton3.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton3.Position = new Point(0xa6, 4);
            this.queuedResearchButton3.Data = 2;
            this.queuedResearchButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
            this.queuedResearchArea.addControl(this.queuedResearchButton3);
            this.queuedResearchButton4.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
            this.queuedResearchButton4.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton4.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton4.Position = new Point(0xf7, 4);
            this.queuedResearchButton4.Data = 3;
            this.queuedResearchButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
            this.queuedResearchArea.addControl(this.queuedResearchButton4);
            this.queuedResearchButton5.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
            this.queuedResearchButton5.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton5.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton5.Position = new Point(0x148, 4);
            this.queuedResearchButton5.Data = 4;
            this.queuedResearchButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
            this.queuedResearchArea.addControl(this.queuedResearchButton5);
            this.queuedResearchButton6.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
            this.queuedResearchButton6.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton6.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton6.Position = new Point(0x199, 4);
            this.queuedResearchButton6.Data = 5;
            this.queuedResearchButton6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
            this.queuedResearchButton6.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchButton6);
            this.queuedResearchButton7.ImageNorm = (Image) GFXLibrary.research_border_research_ill_normal;
            this.queuedResearchButton7.ImageOver = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton7.ImageClick = (Image) GFXLibrary.research_border_research_ill_over;
            this.queuedResearchButton7.Position = new Point(490, 4);
            this.queuedResearchButton7.Data = 6;
            this.queuedResearchButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.queuedResearchClick), "ResearchPanel_queued_clicked");
            this.queuedResearchButton7.Visible = false;
            this.queuedResearchArea.addControl(this.queuedResearchButton7);
            this.queuedResearchNoPremiumText.Text = SK.Text("Research_Queue_Premium", "Research Queue requires a Premium Token");
            this.queuedResearchNoPremiumText.Color = Color.FromArgb(0xfe, 230, 0xc0);
            int width = 0x94 + (base.Width - 0x3e0);
            if (width < 0xaf)
            {
                if ((Program.mySettings.LanguageIdent == "de") || (Program.mySettings.LanguageIdent == "fr"))
                {
                    width = 0xb8;
                }
                else
                {
                    width = 0xaf;
                }
                this.queuedResearchNoPremiumText.Position = new Point(0x18f, -10);
            }
            else
            {
                this.queuedResearchNoPremiumText.Position = new Point(0x199, -10);
            }
            this.queuedResearchNoPremiumText.Size = new Size(width, 0x3a);
            this.queuedResearchNoPremiumText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.queuedResearchNoPremiumText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.queuedResearchArea.addControl(this.queuedResearchNoPremiumText);
            this.timeInfoBox.Size = new Size((((base.Width - 450) + 2) + 15) + 15, 0x23);
            this.timeInfoBox.Position = new Point(0xb3, 0x7d + num);
            this.mainBackgroundImage.addControl(this.timeInfoBox);
            this.timeInfoBox.Create((Image) GFXLibrary.tech_tree_inset_left, (Image) GFXLibrary.tech_tree_inset_mid, (Image) GFXLibrary.tech_tree_inset_right);
            this.timeProgressBar.Size = new Size(this.timeInfoBox.Size.Width - 14, 0x16);
            this.timeProgressBar.Position = new Point(7, 7);
            this.timeProgressBar.Offset = new Point(5, 3);
            this.timeInfoBox.addControl(this.timeProgressBar);
            this.timeProgressBar.Create((Image) GFXLibrary.tech_tree_progbar_olive_left, (Image) GFXLibrary.tech_tree_progbar_olive_mid, (Image) GFXLibrary.tech_tree_progbar_olive_right, (Image) GFXLibrary.tech_tree_progbar_green_left, (Image) GFXLibrary.tech_tree_progbar_green_mid, (Image) GFXLibrary.tech_tree_progbar_green_right);
            this.timeProgressText.Text = "";
            this.timeProgressText.Color = ARGBColors.Black;
            this.timeProgressText.Position = new Point(0, 0);
            this.timeProgressText.Size = new Size(this.timeInfoBox.Width, this.timeInfoBox.Height);
            this.timeProgressText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.timeProgressText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.timeInfoBox.addControl(this.timeProgressText);
            this.buyPointInfoBox.Size = new Size(0xd0, 0x5b);
            this.buyPointInfoBox.Position = new Point((base.Width - 0xf4) + 15, 0x1b + num);
            this.mainBackgroundImage.addControl(this.buyPointInfoBox);
            this.buyPointInfoBox.Create((Image) GFXLibrary.tech_tree_inset_tall_left, (Image) GFXLibrary.tech_tree_inset_tall_mid, (Image) GFXLibrary.tech_tree_inset_tall_right);
            this.buyPointGold.Image = (Image) GFXLibrary.com_32_money;
            this.buyPointGold.Position = new Point(0x30, 8);
            this.buyPointInfoBox.addControl(this.buyPointGold);
            this.buyPointText.Text = "";
            this.buyPointText.Color = Color.FromArgb(0xfe, 230, 0xc0);
            this.buyPointText.Position = new Point(0x61, -6);
            this.buyPointText.Size = new Size(100, 60);
            this.buyPointText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.buyPointText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.buyPointInfoBox.addControl(this.buyPointText);
            this.buyPointButton.ImageNorm = (Image) GFXLibrary.techtree_button_normal;
            this.buyPointButton.ImageOver = (Image) GFXLibrary.techtree_button_over;
            this.buyPointButton.ImageClick = (Image) GFXLibrary.techtree_button_in;
            this.buyPointButton.Position = new Point(0x1a, 0x2c);
            this.buyPointButton.Text.Text = SK.Text("Research_Buy_Point", "Buy Point");
            this.buyPointButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.buyPointButton.TextYOffset = 1;
            this.buyPointButton.Text.Color = ARGBColors.Black;
            this.buyPointButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.buyPointClick), "ResearchPanel_buy_point");
            this.buyPointInfoBox.addControl(this.buyPointButton);
            this.pointsInfoBox.Size = new Size(0xd0, 0x23);
            this.pointsInfoBox.Position = new Point((base.Width - 0xf4) + 15, 0x7d + num);
            this.mainBackgroundImage.addControl(this.pointsInfoBox);
            this.pointsInfoBox.Create((Image) GFXLibrary.tech_tree_inset_left, (Image) GFXLibrary.tech_tree_inset_mid, (Image) GFXLibrary.tech_tree_inset_right);
            this.pointsText.Text = "";
            this.pointsText.Color = Color.FromArgb(0xfe, 230, 0xc0);
            this.pointsText.Position = new Point(0, 0);
            this.pointsText.Size = new Size(this.pointsInfoBox.Width, this.pointsInfoBox.Height);
            this.pointsText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.pointsText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.pointsInfoBox.addControl(this.pointsText);
            this.scrollPanelImage.Image = (Image) GFXLibrary.body_background_002;
            this.scrollPanelImage.Tile = true;
            this.scrollPanelImage.Position = new Point(20, 0xf2);
            this.scrollPanelImage.Size = new Size(base.Width - 40, (base.Height - 0xcd) - 0x37);
            this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(base.Width - 40, (base.Height - 0xcd) - 0x37));
            this.mainBackgroundImage.addControl(this.scrollPanelImage);
            this.dragOverlay.Position = this.scrollPanelImage.Position;
            this.dragOverlay.Size = this.scrollPanelImage.Size;
            this.dragOverlay.Visible = true;
            this.dragOverlay.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.windowDragged));
            this.dragOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.dragWindowMouseWheel));
            this.dragOverlay.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.dragWindowMouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.dragWindowMouseLeave));
            this.mainBackgroundImage.addControl(this.dragOverlay);
            this.dragOverlay2.Position = this.scrollPanelImage.Position;
            this.dragOverlay2.Size = this.scrollPanelImage.Size;
            this.dragOverlay2.Visible = false;
            this.dragOverlay2.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.listWindowMouseWheel));
            this.mainBackgroundImage.addControl(this.dragOverlay2);
            int x = 20;
            int y = 0xf2;
            int num5 = base.Width - 20;
            int num6 = ((base.Height - 0xcd) - 0x37) + y;
            this.scrollPanelTopLeftOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_topleft;
            this.scrollPanelTopLeftOverlay.Position = new Point(x, y);
            this.mainBackgroundImage.addControl(this.scrollPanelTopLeftOverlay);
            this.scrollPanelTopRightOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_topright;
            this.scrollPanelTopRightOverlay.Position = new Point(num5 - this.scrollPanelTopRightOverlay.Image.Width, y);
            this.mainBackgroundImage.addControl(this.scrollPanelTopRightOverlay);
            this.scrollPanelTopMiddleOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_top;
            this.scrollPanelTopMiddleOverlay.Position = new Point(x + this.scrollPanelTopLeftOverlay.Image.Width, y);
            this.scrollPanelTopMiddleOverlay.Size = new Size(((num5 - x) - this.scrollPanelTopRightOverlay.Image.Width) - this.scrollPanelTopLeftOverlay.Image.Width, this.scrollPanelTopMiddleOverlay.Image.Height);
            this.scrollPanelTopMiddleOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelTopMiddleOverlay.Size.Width, this.scrollPanelTopMiddleOverlay.Size.Height);
            this.mainBackgroundImage.addControl(this.scrollPanelTopMiddleOverlay);
            this.scrollPanelBottomLeftOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_bottomleft;
            this.scrollPanelBottomLeftOverlay.Position = new Point(x, num6 - this.scrollPanelBottomLeftOverlay.Image.Height);
            this.mainBackgroundImage.addControl(this.scrollPanelBottomLeftOverlay);
            this.scrollPanelLeftOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_left;
            this.scrollPanelLeftOverlay.Position = new Point(x, y + this.scrollPanelTopLeftOverlay.Image.Height);
            this.scrollPanelLeftOverlay.Size = new Size(this.scrollPanelLeftOverlay.Image.Width, ((num6 - y) - this.scrollPanelTopLeftOverlay.Image.Height) - this.scrollPanelBottomLeftOverlay.Image.Height);
            this.scrollPanelLeftOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelLeftOverlay.Size.Width, this.scrollPanelLeftOverlay.Size.Height);
            this.mainBackgroundImage.addControl(this.scrollPanelLeftOverlay);
            this.scrollPanelBottomRightOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_bottomright;
            this.scrollPanelBottomRightOverlay.Position = new Point(num5 - this.scrollPanelBottomRightOverlay.Image.Width, num6 - this.scrollPanelBottomRightOverlay.Image.Height);
            this.mainBackgroundImage.addControl(this.scrollPanelBottomRightOverlay);
            this.scrollPanelRightOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_right;
            this.scrollPanelRightOverlay.Position = new Point(num5 - this.scrollPanelRightOverlay.Image.Width, y + this.scrollPanelTopRightOverlay.Image.Height);
            this.scrollPanelRightOverlay.Size = new Size(this.scrollPanelRightOverlay.Image.Width, ((num6 - y) - this.scrollPanelTopRightOverlay.Image.Height) - this.scrollPanelBottomRightOverlay.Image.Height);
            this.scrollPanelRightOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelRightOverlay.Size.Width, this.scrollPanelRightOverlay.Size.Height);
            this.mainBackgroundImage.addControl(this.scrollPanelRightOverlay);
            this.scrollPanelBottomMiddleOverlay.Image = (Image) GFXLibrary.techtree_inset_edge_bottom;
            this.scrollPanelBottomMiddleOverlay.Position = new Point(x + this.scrollPanelBottomLeftOverlay.Image.Width, num6 - this.scrollPanelBottomMiddleOverlay.Image.Height);
            this.scrollPanelBottomMiddleOverlay.Size = new Size(((num5 - x) - this.scrollPanelBottomRightOverlay.Image.Width) - this.scrollPanelBottomLeftOverlay.Image.Width, this.scrollPanelBottomMiddleOverlay.Image.Height);
            this.scrollPanelBottomMiddleOverlay.ClipRect = new Rectangle(0, 0, this.scrollPanelBottomMiddleOverlay.Size.Width, this.scrollPanelBottomMiddleOverlay.Size.Height);
            this.mainBackgroundImage.addControl(this.scrollPanelBottomMiddleOverlay);
            this.tab1Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_01_normal;
            this.tab1Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_01_highlight;
            this.tab1Button.Position = new Point(x, 0xd8);
            this.tab1Button.Text.Text = SK.Text("Research_Industry", "Industry");
            this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab1Button.TextYOffset = -13;
            this.tab1Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab1Button.Data = 0;
            this.tab1Button.ClickArea = new Rectangle(0, 0, this.tab1Button.ImageNorm.Width, 0x19);
            this.tab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_industry_tab");
            this.mainBackgroundImage.addControl(this.tab1Button);
            this.tab2Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab2Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            this.tab2Button.Position = new Point((this.tab1Button.Position.X + this.tab1Button.Width) + 2, 0xd8);
            this.tab2Button.Text.Text = SK.Text("Research_Military", "Military");
            this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab2Button.TextYOffset = 0;
            this.tab2Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab2Button.Data = 1;
            this.tab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_military_tab");
            this.mainBackgroundImage.addControl(this.tab2Button);
            this.tab3Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab3Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            this.tab3Button.Position = new Point((this.tab2Button.Position.X + this.tab2Button.Width) + 2, 0xd8);
            this.tab3Button.Text.Text = SK.Text("Research_Farming", "Farming");
            this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab3Button.TextYOffset = 0;
            this.tab3Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab3Button.Data = 2;
            this.tab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_farming_tab");
            this.mainBackgroundImage.addControl(this.tab3Button);
            this.tab4Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab4Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            this.tab4Button.Position = new Point((this.tab3Button.Position.X + this.tab3Button.Width) + 2, 0xd8);
            this.tab4Button.Text.Text = SK.Text("Research_Education", "Education");
            this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab4Button.TextYOffset = 0;
            this.tab4Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab4Button.Data = 3;
            this.tab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClicked), "ResearchPanel_education_tab");
            this.mainBackgroundImage.addControl(this.tab4Button);
            this.tab5Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab5Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            this.tab5Button.Position = new Point((this.tab4Button.Position.X + this.tab4Button.Width) + 2, 0xd8);
            this.tab5Button.Text.Text = SK.Text("Research_Explore_Tree", "Explore Research Tree");
            this.tab5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab5Button.TextYOffset = 0;
            this.tab5Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab5Button.Data = 4;
            this.tab5Button.Visible = false;
            this.tab5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabModeClicked));
            this.mainBackgroundImage.addControl(this.tab5Button);
            this.tabModeTreeButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_tree_normal;
            this.tabModeTreeButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_tree_normal;
            this.tabModeTreeButton.Position = new Point(num5 - this.tabModeTreeButton.Width, 0xd8);
            this.tabModeTreeButton.Data = 1;
            this.tabModeTreeButton.ClickArea = new Rectangle(0, 0, this.tabModeTreeButton.Width, 0x19);
            this.tabModeTreeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabModeClicked), "ResearchPanel_tree_mode");
            this.tabModeTreeButton.CustomTooltipID = 0x12d;
            this.mainBackgroundImage.addControl(this.tabModeTreeButton);
            this.tabModeListButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_list_normal;
            this.tabModeListButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_list_normal;
            this.tabModeListButton.Position = new Point((this.tabModeTreeButton.Position.X - this.tabModeListButton.Width) - 2, 0xd8);
            this.tabModeListButton.Data = 0;
            this.tabModeListButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabModeClicked), "ResearchPanel_list_mode");
            this.tabModeListButton.CustomTooltipID = 300;
            this.mainBackgroundImage.addControl(this.tabModeListButton);
            this.cardbar.Position = new Point(0, 0);
            this.mainBackgroundImage.addControl(this.cardbar);
            this.cardbar.init(12);
            this.manageTabs(this.lastTab);
            this.forceUpdate = true;
        }

        private void initEducationTab()
        {
            this.lastResearchTab = 3;
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
            {
                this.initTab(this.educationResearchLayout);
            }
            else
            {
                this.initTab(this.educationResearchLayout2);
            }
        }

        private void initExploreTab(int mode)
        {
            int num;
            this.queuedResearchArea.Visible = false;
            this.currentResearchInfoBox.Create((Image) GFXLibrary.tech_tree_inset_tall_left, (Image) GFXLibrary.tech_tree_inset_tall_mid, (Image) GFXLibrary.tech_tree_inset_tall_right);
            this.scrollPanelImage.clearControls();
            this.startResearchScrollBar.clearControls();
            this.mainBackgroundImage.removeControl(this.startResearchScrollBar);
            this.dragOverlay.Visible = true;
            this.dragOverlay2.Visible = false;
            if (!this.rowsCreated)
            {
                this.rowsCreated = true;
                this.createRows(0, 0, 1, 1, this.industryLayout, this.industryRows, 0x15, 0x12);
                this.createRows(0, 0, 1, 1, this.farmingLayout, this.farmingRows, 13, 0x11);
                this.createRows(0, 0, 1, 1, this.militaryLayout, this.militaryRows, 0x18, 30);
                this.createRows(0, 0, 1, 1, this.educationLayout, this.educationRows, 0x1b, 0x1a);
                this.createRows(0, 0, 1, 1, this.educationLayout2, this.educationRows2, 0x1a, 0x1a);
            }
            this.resetImageCache();
            this.resetLabelCache();
            switch (mode)
            {
                case 0:
                    this.scrollPanelImage.Size = new Size(0xadc, 0x956);
                    this.updateRows(0, 0, 1, 1, this.industryLayout, this.industryRows, 0x15);
                    goto Label_0258;

                case 1:
                    num = 20;
                    if (GameEngine.Instance.World.getRank() != 0x16)
                    {
                        num -= 2;
                        break;
                    }
                    num += 5;
                    break;

                case 2:
                    this.scrollPanelImage.Size = new Size(0xa46, 0x5e6);
                    this.updateRows(0, 0, 1, 1, this.farmingLayout, this.farmingRows, 13);
                    goto Label_0258;

                case 3:
                    this.scrollPanelImage.Size = new Size(0xf8c, 0xbea);
                    if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                    {
                        this.updateRows(0, 0, 1, 1, this.educationLayout2, this.educationRows2, 0x1a);
                    }
                    else
                    {
                        this.updateRows(0, 0, 1, 1, this.educationLayout, this.educationRows, 0x1b);
                    }
                    goto Label_0258;

                default:
                    goto Label_0258;
            }
            this.scrollPanelImage.Size = new Size((num * 150) + 80, 0xaa0);
            this.updateRows(0, 0, 1, 1, this.militaryLayout, this.militaryRows, 0x18);
        Label_0258:
            this.realScrollImageSize = this.scrollPanelImage.Size;
            double windowScale = this.m_windowScale;
            this.scrollPanelImage.setChildrensScale(windowScale);
            this.scrollPanelImage.Position = new Point(20, 0xf2);
            this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(base.Width - 40, (base.Height - 0xcd) - 0x37));
            int num3 = 0;
            int num4 = 0;
            this.lastScrollXPos = 0;
            this.lastScrollYPos = 0;
            this.scrollPanelImage.Position = new Point(20 - num3, 0xf2 - num4);
            this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X + num3, this.scrollPanelImage.ClipRect.Y + num4, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
            this.dragWindowMouseWheel(0);
            base.Invalidate();
        }

        private void initFarmingTab()
        {
            this.lastResearchTab = 2;
            this.initTab(this.farmingResearchLayout);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "ResearchPanel";
            base.SizeChanged += new EventHandler(this.ResearchPanel2_SizeChanged);
            base.ResumeLayout(false);
        }

        private void initIndustryTab()
        {
            this.lastResearchTab = 0;
            this.initTab(this.industryResearchLayout);
        }

        private void initMilitaryTab()
        {
            this.lastResearchTab = 1;
            this.initTab(this.militaryResearchLayout);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        private void initTab(int[] researchlist)
        {
            this.queuedResearchArea.Visible = true;
            this.currentResearchInfoBox.Create((Image) GFXLibrary.research_tech_tree_inset_54_tall_left, (Image) GFXLibrary.research_tech_tree_inset_54_tall_mid, (Image) GFXLibrary.research_tech_tree_inset_54_tall_right);
            this.currentResearchInfoBoxRow2Text.Visible = false;
            this.currentResearchInfoBoxRow3Text.Visible = false;
            this.dragOverlay.Visible = false;
            this.dragOverlay2.Visible = true;
            this.scrollPanelImage.clearControls();
            this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(base.Width - 40, (base.Height - 0xcd) - 0x37));
            for (int i = 0; i < 30; i++)
            {
                if (this.startResearchButtons[i] == null)
                {
                    this.startResearchButtons[i] = new CustomSelfDrawPanel.CSDButton();
                }
                this.startResearchButtons[i].Visible = false;
                if (this.startResearchImages[i] == null)
                {
                    this.startResearchImages[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchImages[i].Visible = false;
                if (this.startResearchOpenBackground[i] == null)
                {
                    this.startResearchOpenBackground[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchOpenBackground[i].Visible = false;
                if (this.startResearchHeader[i] == null)
                {
                    this.startResearchHeader[i] = new CustomSelfDrawPanel.CSDLabel();
                }
                this.startResearchHeader[i].Visible = false;
                if (this.startResearchText1[i] == null)
                {
                    this.startResearchText1[i] = new CustomSelfDrawPanel.CSDLabel();
                }
                this.startResearchText1[i].Visible = false;
                if (this.startResearchText2[i] == null)
                {
                    this.startResearchText2[i] = new CustomSelfDrawPanel.CSDLabel();
                }
                this.startResearchText2[i].Visible = false;
                if (this.startResearchDotsBack[i] == null)
                {
                    this.startResearchDotsBack[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchDotsBack[i].Visible = false;
                if (this.startResearchDots[i] == null)
                {
                    this.startResearchDots[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchDots[i].Visible = false;
                if (this.startResearchDotsYellow[i] == null)
                {
                    this.startResearchDotsYellow[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchDotsYellow[i].Visible = false;
                if (this.startResearchOpenResearch[i] == null)
                {
                    this.startResearchOpenResearch[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchOpenResearch[i].Visible = false;
                if (this.startResearchOpenResearchOverlay[i] == null)
                {
                    this.startResearchOpenResearchOverlay[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchOpenResearchOverlay[i].Visible = false;
                if (this.startResearchOpenResearchOverlayLabel[i] == null)
                {
                    this.startResearchOpenResearchOverlayLabel[i] = new CustomSelfDrawPanel.CSDLabel();
                }
                this.startResearchOpenResearchOverlayLabel[i].Visible = false;
                if (this.startResearchOpenBuilding[i] == null)
                {
                    this.startResearchOpenBuilding[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchOpenBuilding[i].Visible = false;
                if (this.startResearchShield[i] == null)
                {
                    this.startResearchShield[i] = new CustomSelfDrawPanel.CSDImage();
                }
                this.startResearchShield[i].Visible = false;
                if (this.startResearchShieldNumber[i] == null)
                {
                    this.startResearchShieldNumber[i] = new CustomSelfDrawPanel.CSDLabel();
                }
                this.startResearchShieldNumber[i].Visible = false;
            }
            if (this.lastData != null)
            {
                int rank = GameEngine.Instance.World.getRank();
                int subRank = GameEngine.Instance.World.getRankSubLevel();
                int rankNeeded = -1;
                int height = 0x2c;
                int y = 0x22;
                int index = 0;
                bool special = false;
                foreach (int num8 in researchlist)
                {
                    if ((this.lastData.research[num8] >= ResearchData.getNumLevels(num8, rank, GameEngine.Instance.LocalWorldData)) || !this.lastDataQueued.isResearchStepOpen(num8, this.lastDataQueued.research[num8], rank, subRank, ref rankNeeded, ref special))
                    {
                        continue;
                    }
                    CustomSelfDrawPanel.CSDButton control = this.startResearchButtons[index];
                    if (this.researchAllowed || (num8 != this.lastDataQueued.researchingType))
                    {
                        control.ImageNorm = (Image) GFXLibrary.tech_list_but_big_normal;
                        control.ImageOver = (Image) GFXLibrary.tech_list_but_big_over;
                        control.ImageClick = (Image) GFXLibrary.tech_list_but_big_in;
                    }
                    else
                    {
                        control.ImageNorm = (Image) GFXLibrary.tech_list_but_big_over;
                        control.ImageOver = (Image) GFXLibrary.tech_list_but_big_over;
                        control.ImageClick = (Image) GFXLibrary.tech_list_but_big_over;
                    }
                    control.Position = new Point(20, y);
                    control.Data = num8;
                    control.Visible = true;
                    control.Enabled = this.researchAllowed && (this.lastDataQueued.research[num8] < ResearchData.getNumLevels(num8, rank, GameEngine.Instance.LocalWorldData));
                    control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.researchClicked), "ResearchPanel_do_research");
                    this.scrollPanelImage.addControl(control);
                    CustomSelfDrawPanel.CSDImage image = this.startResearchImages[index];
                    image.Image = (Image) this.getIllustration(num8);
                    image.Position = new Point(7, 7);
                    image.Visible = true;
                    control.addControl(image);
                    CustomSelfDrawPanel.CSDLabel label = this.startResearchHeader[index];
                    label.Text = ResearchData.getResearchName(num8);
                    label.Color = ARGBColors.Black;
                    label.Position = new Point(150, 5);
                    label.Size = new Size(0x163, 30);
                    label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    label.Visible = true;
                    control.addControl(label);
                    CustomSelfDrawPanel.CSDLabel label2 = this.startResearchText1[index];
                    label2.Text = ResearchData.getDescriptionText(num8, this.lastDataQueued.research[num8]);
                    label2.Color = ARGBColors.Black;
                    label2.Position = new Point(150, 0x17);
                    label2.Size = new Size(0x1d1, 30);
                    label2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    label2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    label2.Visible = true;
                    control.addControl(label2);
                    CustomSelfDrawPanel.CSDLabel label3 = this.startResearchText2[index];
                    label3.Text = ResearchData.getEffectText(num8, this.lastDataQueued.research[num8], GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1, GameEngine.Instance.LocalWorldData);
                    label3.Color = ARGBColors.Black;
                    label3.Position = new Point(150, 0x35);
                    label3.Size = new Size(0x1d1, 30);
                    label3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    label3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    label3.Visible = true;
                    control.addControl(label3);
                    int num9 = ResearchData.getNumLevels(num8, rank, GameEngine.Instance.LocalWorldData);
                    if (num9 <= 0x10)
                    {
                        image = this.startResearchDotsBack[index];
                        switch (num9)
                        {
                            case 4:
                                image.Image = (Image) GFXLibrary.tech_tree_dots_black_x04;
                                break;

                            case 5:
                                image.Image = (Image) GFXLibrary.tech_tree_dots_black_x05;
                                break;

                            case 8:
                                image.Image = (Image) GFXLibrary.tech_tree_dots_black_x08;
                                break;

                            case 10:
                                image.Image = (Image) GFXLibrary.tech_tree_dots_black_x10;
                                break;

                            case 13:
                                image.Image = (Image) GFXLibrary.tech_tree_dots_black_x13;
                                break;

                            case 15:
                                image.Image = (Image) GFXLibrary.tech_tree_dots_black_x15;
                                break;

                            default:
                                image.Image = (Image) GFXLibrary.tech_tree_dots_black_x16;
                                break;
                        }
                        image.Position = new Point((control.Width - 10) - image.Image.Width, 11);
                        image.Visible = true;
                        control.addControl(image);
                        int num10 = this.lastDataQueued.research[num8];
                        int num11 = this.lastData.research[num8];
                        if ((num10 > 0) && (num10 != num11))
                        {
                            CustomSelfDrawPanel.CSDImage image2 = this.startResearchDotsYellow[index];
                            image2.Image = (Image) GFXLibrary.tech_tree_dots_yellow_x16;
                            image2.Position = new Point(0, 0);
                            image2.ClipRect = new Rectangle(0, 0, -2 + (num10 * 10), image2.Height);
                            image2.Visible = true;
                            image.addControl(image2);
                        }
                        if (num11 > 0)
                        {
                            CustomSelfDrawPanel.CSDImage image3 = this.startResearchDots[index];
                            image3.Image = (Image) GFXLibrary.tech_tree_dots_green_x16;
                            image3.Position = new Point(0, 0);
                            image3.ClipRect = new Rectangle(0, 0, -2 + (num11 * 10), image3.Height);
                            image3.Visible = true;
                            image.addControl(image3);
                        }
                    }
                    image = this.startResearchOpenBackground[index];
                    image.Image = (Image) GFXLibrary.tech_list_insets_X2;
                    image.Position = new Point(0x290, y);
                    image.Visible = true;
                    this.scrollPanelImage.addControl(image);
                    int openedBuilding = -1;
                    int openedCastleBuilding = -1;
                    int openedTroop = -1;
                    int researchType = this.getOpenedResearch(num8, this.lastDataQueued.research[num8] + 1, ref openedBuilding, ref openedCastleBuilding, ref openedTroop);
                    if (researchType > 0)
                    {
                        CustomSelfDrawPanel.CSDImage image4 = this.startResearchOpenResearch[index];
                        image4.Image = (Image) this.getIllustration(researchType);
                        if (image4.Image != null)
                        {
                            image4.Tooltip = researchType * 0x3e8;
                            image4.Position = new Point(8, 7);
                            image4.Visible = true;
                            image.addControl(image4);
                            this.lastDataQueued.isResearchStepOpen(researchType, 0, rank, subRank, ref rankNeeded, ref special);
                            if (rankNeeded >= 0)
                            {
                                CustomSelfDrawPanel.CSDImage image5 = this.startResearchShield[index];
                                image5.Image = (Image) GFXLibrary.ill_shield;
                                image5.Position = new Point(0x69, 2);
                                image5.Visible = true;
                                image4.addControl(image5);
                                CustomSelfDrawPanel.CSDLabel label4 = this.startResearchShieldNumber[index];
                                if (rankNeeded >= 100)
                                {
                                    label4.Text = ((rankNeeded - 100) + 1).ToString();
                                }
                                else
                                {
                                    label4.Text = rankNeeded.ToString();
                                }
                                label4.Color = ARGBColors.White;
                                label4.Position = new Point(0, -2);
                                label4.Size = image5.Size;
                                label4.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                                label4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                                label4.Visible = true;
                                image5.addControl(label4);
                            }
                            CustomSelfDrawPanel.CSDImage image6 = this.startResearchOpenResearchOverlay[index];
                            image6.Image = (Image) GFXLibrary.research_ill_overlay;
                            image6.Position = new Point(0, 40);
                            image6.Visible = true;
                            image6.Alpha = 0.5f;
                            image4.addControl(image6);
                            CustomSelfDrawPanel.CSDLabel label5 = this.startResearchOpenResearchOverlayLabel[index];
                            label5.Text = ResearchData.getResearchName(researchType);
                            label5.Color = ARGBColors.White;
                            label5.Position = new Point(0, 0);
                            label5.Size = image6.Size;
                            if ((Program.mySettings.LanguageIdent == "tr") && ((((researchType == 14) || (researchType == 0x42)) || ((researchType == 0x2e) || (researchType == 0x29))) || ((researchType == 0x2b) || (researchType == 0x2a))))
                            {
                                label5.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            }
                            else if ((Program.mySettings.LanguageIdent == "pl") && (((researchType == 14) || (researchType == 0x25)) || ((researchType == 0x2d) || (researchType == 50))))
                            {
                                label5.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            }
                            else if ((Program.mySettings.LanguageIdent == "it") && (((researchType == 0x11) || (researchType == 0x43)) || (researchType == 0x29)))
                            {
                                label5.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            }
                            else if ((Program.mySettings.LanguageIdent == "pt") && ((((researchType == 0) || (researchType == 0x27)) || ((researchType == 0x11) || (researchType == 0x42))) || ((((researchType == 0x40) || (researchType == 10)) || ((researchType == 0x2b) || (researchType == 0x2c))) || ((researchType == 0x2d) || (researchType == 0x2e)))))
                            {
                                label5.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            }
                            else if ((Program.mySettings.LanguageIdent == "pt") && ((researchType == 0x22) || (researchType == 0x2a)))
                            {
                                label5.Font = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
                            }
                            else if (((researchType == 0x2d) || (researchType == 0x2b)) && (Program.mySettings.LanguageIdent == "de"))
                            {
                                label5.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            }
                            else
                            {
                                label5.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                            }
                            label5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            label5.Visible = true;
                            image6.addControl(label5);
                        }
                    }
                    if (((openedBuilding > 0) || (openedCastleBuilding > 0)) || (openedTroop > 0))
                    {
                        CustomSelfDrawPanel.CSDImage image7 = this.startResearchOpenBuilding[index];
                        if (openedBuilding > 0)
                        {
                            image7.Image = (Image) this.getBuildingGFX(openedBuilding);
                        }
                        if (openedCastleBuilding > 0)
                        {
                            image7.Image = (Image) this.getCastleGFX(openedCastleBuilding);
                        }
                        if (openedTroop > 0)
                        {
                            image7.Image = (Image) this.getCastleGFX(openedTroop);
                        }
                        if (image7.Image != null)
                        {
                            image7.Position = new Point(0xc5 - (image7.Image.Size.Width / 2), 0x2a - (image7.Image.Size.Height / 2));
                            image7.Visible = true;
                            image.addControl(image7);
                        }
                    }
                    if (num8 == 0x3b)
                    {
                        TUTORIAL_artsTabPos = y;
                    }
                    index++;
                    y += 80;
                    height += 80;
                }
                this.startResearchHeaderMain.Text = SK.Text("Research_Choose_Next", "Choose Next Research");
                this.startResearchHeaderMain.Color = ARGBColors.Black;
                this.startResearchHeaderMain.Position = new Point(0xb7, 12);
                this.startResearchHeaderMain.Size = new Size(400, 60);
                this.startResearchHeaderMain.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.startResearchHeaderMain.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.scrollPanelImage.addControl(this.startResearchHeaderMain);
                this.startResearchHeaderResearchOpen.Text = SK.Text("Research_Allows", "Allows");
                this.startResearchHeaderResearchOpen.Color = ARGBColors.Black;
                this.startResearchHeaderResearchOpen.Position = new Point(0x290, 12);
                this.startResearchHeaderResearchOpen.Size = new Size(200, 60);
                this.startResearchHeaderResearchOpen.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.startResearchHeaderResearchOpen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.scrollPanelImage.addControl(this.startResearchHeaderResearchOpen);
                this.startResearchHeaderBuildingOpen.Text = SK.Text("Research_Opens", "Opens");
                this.startResearchHeaderBuildingOpen.Color = ARGBColors.Black;
                this.startResearchHeaderBuildingOpen.Position = new Point(0x32d, 12);
                this.startResearchHeaderBuildingOpen.Size = new Size(200, 60);
                this.startResearchHeaderBuildingOpen.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.startResearchHeaderBuildingOpen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.scrollPanelImage.addControl(this.startResearchHeaderBuildingOpen);
                int num16 = height;
                this.startResearchScrollBar.clearControls();
                this.mainBackgroundImage.removeControl(this.startResearchScrollBar);
                if (height <= this.scrollPanelImage.ClipRect.Height)
                {
                    height = this.scrollPanelImage.ClipRect.Height;
                    this.startResearchScrollBar.Visible = false;
                }
                else
                {
                    this.startResearchScrollBar.Visible = true;
                    this.startResearchScrollBar.Position = new Point(((base.Width - 20) - 10) - 0x20, 0xff);
                    this.startResearchScrollBar.Size = new Size(0x20, (this.scrollPanelImage.ClipRect.Height - 13) - 13);
                    this.mainBackgroundImage.addControl(this.startResearchScrollBar);
                    this.startResearchScrollBar.Value = 0;
                    this.startResearchScrollBar.Max = num16 - this.scrollPanelImage.ClipRect.Height;
                    this.startResearchScrollBar.NumVisibleLines = this.scrollPanelImage.ClipRect.Height;
                    this.startResearchScrollBar.OffsetTL = new Point(1, 5);
                    this.startResearchScrollBar.OffsetBR = new Point(0, -10);
                    this.startResearchScrollBar.Create((Image) GFXLibrary.scroll_inset_top, (Image) GFXLibrary.scroll_inset_mid, (Image) GFXLibrary.scroll_inset_bottom, (Image) GFXLibrary.scroll_thumb_top, (Image) GFXLibrary.scroll_thumb_mid, (Image) GFXLibrary.scroll_thumb_bottom);
                    this.startResearchScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
                }
                this.scrollPanelImage.Size = new Size(this.scrollPanelImage.Size.Width, height);
                this.scrollPanelImage.Position = new Point(20, 0xf2);
                this.scrollPanelImage.invalidate();
            }
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isResearchOnEducationTab()
        {
            return (this.lastResearchTab == 3);
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void listWindowMouseWheel(int delta)
        {
            if (this.startResearchScrollBar.Visible)
            {
                if (delta < 0)
                {
                    this.startResearchScrollBar.scrollDown();
                }
                else if (delta > 0)
                {
                    this.startResearchScrollBar.scrollUp();
                }
            }
        }

        private void manageTabs(int tab)
        {
            this.lastTab = tab;
            this.tab1Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_01_normal;
            this.tab1Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_01_highlight;
            this.tab2Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab2Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            this.tab3Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab3Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            this.tab4Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab4Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            this.tab5Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_normal;
            this.tab5Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
            if (this.tabType == 0)
            {
                this.tabModeListButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_list_highlight;
                this.tabModeListButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_list_highlight;
                this.tabModeTreeButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_tree_normal;
                this.tabModeTreeButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_tree_highlight;
            }
            else
            {
                this.tabModeListButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_list_normal;
                this.tabModeListButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_list_highlight;
                this.tabModeTreeButton.ImageNorm = (Image) GFXLibrary.tech_tree_tab_tree_highlight;
                this.tabModeTreeButton.ImageOver = (Image) GFXLibrary.tech_tree_tab_tree_highlight;
            }
            this.tab1Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab2Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab3Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab4Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            this.tab5Button.Text.Color = Color.FromArgb(0xcd, 0x9d, 0x31);
            switch (tab)
            {
                case 0:
                    this.tab1Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_01_highlight;
                    this.tab1Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_01_highlight;
                    this.tab1Button.Text.Color = ARGBColors.White;
                    if (this.tabType != 0)
                    {
                        this.initExploreTab(0);
                        return;
                    }
                    this.initIndustryTab();
                    return;

                case 1:
                    this.tab2Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab2Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab2Button.Text.Color = ARGBColors.White;
                    if (this.tabType != 0)
                    {
                        this.initExploreTab(1);
                        return;
                    }
                    this.initMilitaryTab();
                    return;

                case 2:
                    this.tab3Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab3Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab3Button.Text.Color = ARGBColors.White;
                    if (this.tabType != 0)
                    {
                        this.initExploreTab(2);
                        return;
                    }
                    this.initFarmingTab();
                    return;

                case 3:
                    this.tab4Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab4Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab4Button.Text.Color = ARGBColors.White;
                    if (this.tabType != 0)
                    {
                        this.initExploreTab(3);
                        return;
                    }
                    this.initEducationTab();
                    return;

                case 4:
                    this.tab5Button.ImageNorm = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab5Button.ImageOver = (Image) GFXLibrary.tech_tree_tab_highlight;
                    this.tab5Button.Text.Color = ARGBColors.White;
                    this.initExploreTab(0);
                    return;
            }
        }

        private void queuedResearchClick()
        {
            CustomSelfDrawPanel.CSDControl clickedControl = base.ClickedControl;
            if (clickedControl != null)
            {
                int data = clickedControl.Data;
                this.selectedQueueSlot = data;
            }
        }

        private void rescaleWindow(double oldScale, double newScale)
        {
            this.scrollPanelImage.setChildrensScale(newScale);
            int num = (int) (((double) (-(this.scrollPanelImage.Position.X - 20) + (this.scrollPanelImage.ClipRect.Width / 2))) / oldScale);
            int num2 = (int) (((double) (-((this.scrollPanelImage.Position.Y - 0xbb) - 0x37) + (this.scrollPanelImage.ClipRect.Height / 2))) / oldScale);
            this.lastScrollXPos = num;
            this.lastScrollYPos = num2;
            this.scrollPanelImage.ClipRect = new Rectangle(new Point(0, 0), new Size(base.Width - 40, (base.Height - 0xcd) - 0x37));
            int num3 = ((int) (num * newScale)) - (this.scrollPanelImage.ClipRect.Width / 2);
            int num4 = ((int) (num2 * newScale)) - (this.scrollPanelImage.ClipRect.Height / 2);
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (num3 > ((this.scrollPanelImage.Size.Width * newScale) - this.scrollPanelImage.ClipRect.Width))
            {
                num3 = (((int) (this.scrollPanelImage.Size.Width * newScale)) - this.scrollPanelImage.ClipRect.Width) - 1;
            }
            if (num4 > ((this.scrollPanelImage.Size.Height * newScale) - this.scrollPanelImage.ClipRect.Height))
            {
                num3 = (((int) (this.scrollPanelImage.Size.Height * newScale)) - this.scrollPanelImage.ClipRect.Height) - 1;
            }
            this.scrollPanelImage.Position = new Point(20 - num3, 0xf2 - num4);
            this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X + num3, this.scrollPanelImage.ClipRect.Y + num4, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
            this.scrollPanelImage.invalidate();
        }

        private void researchClicked()
        {
            this.selectedQueueSlot = -1;
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                GameEngine.Instance.World.doResearch(data);
            }
        }

        private void ResearchPanel2_SizeChanged(object sender, EventArgs e)
        {
            this.updateBasedOnResearchData(this.lastData, true);
            base.Invalidate();
        }

        private void resetImageCache()
        {
            this.curImageID = 0;
        }

        private void resetLabelCache()
        {
            this.curLabelID = 0;
        }

        private void scrollBarMoved()
        {
            int y = this.startResearchScrollBar.Value;
            this.scrollPanelImage.Position = new Point(this.scrollPanelImage.Position.X, 0xf2 - y);
            this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X, y, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
            this.scrollPanelImage.invalidate();
            this.scrollPanelBottomMiddleOverlay.invalidate();
            this.scrollPanelBottomLeftOverlay.invalidate();
        }

        private void tabClicked()
        {
            this.selectedQueueSlot = -1;
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data != this.lastTab)
                {
                    this.manageTabs(clickedControl.Data);
                }
            }
        }

        private void tabModeClicked()
        {
            this.selectedQueueSlot = -1;
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                if (clickedControl.Data != this.tabType)
                {
                    this.tabType = clickedControl.Data;
                    this.manageTabs(this.lastTab);
                }
            }
        }

        public void update(bool fullTick)
        {
            this.cardbar.update();
            this.tooltipToShow = -1;
            int data = 0;
            if (base.getToolTip(ref data))
            {
                this.tooltipToShow = data;
            }
            if (fullTick && (this.lastData != null))
            {
                this.updateBasedOnResearchData(this.lastData, false);
            }
        }

        public void updateBasedOnResearchData(ResearchData data, bool localForce)
        {
            if (data != null)
            {
                this.lastData = data;
                this.lastDataQueued = this.lastData;
                if (this.lastData.researchingType >= 0)
                {
                    this.lastDataQueued = data.copyAndAdd(data.researchingType, false);
                    if (data.research_queueEntries != null)
                    {
                        foreach (int num in data.research_queueEntries)
                        {
                            this.lastDataQueued = this.lastDataQueued.copyAndAdd(num, true);
                        }
                    }
                }
                this.applyData(data);
                if (((this.lastData != data) || this.forceUpdate) || localForce)
                {
                    int num2 = this.startResearchScrollBar.Value;
                    if (!this.startResearchScrollBar.Visible)
                    {
                        num2 = 0;
                    }
                    this.init();
                    this.forceUpdate = false;
                    this.applyData(data);
                    this.startResearchScrollBar.Value = num2;
                    this.scrollBarMoved();
                }
            }
        }

        private void updateRows(int startColumn, int startRow, int dx, int dy, int[] layout, CustomSelfDrawPanel.CSDImage[][] rows, int numRows)
        {
            int rank = GameEngine.Instance.World.getRank();
            int subRank = GameEngine.Instance.World.getRankSubLevel();
            Font font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            Font font2 = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            Font font3 = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            Font font4 = FontManager.GetFont("Arial", 7.5f, FontStyle.Regular);
            for (int i = 0; i < numRows; i++)
            {
                int researchType = layout[i * 2];
                int num5 = layout[(i * 2) + 1];
                int num6 = 1;
                if (researchType >= 0)
                {
                    num6 += ResearchData.getNumLevels(researchType, rank, GameEngine.Instance.LocalWorldData);
                }
                for (int j = 0; j < num6; j++)
                {
                    int num30;
                    CustomSelfDrawPanel.CSDImage control = rows[i][j + num5];
                    control.clearControls();
                    this.scrollPanelImage.addControl(control);
                    if (j == 0)
                    {
                        if (num5 == 0)
                        {
                            control.Image = (Image) this.getIllBack(dx, dy, 0, 0, 1, 0, -1);
                            CustomSelfDrawPanel.CSDImage image2 = this.getNextImage();
                            image2.Position = new Point(3, 7);
                            string str = "";
                            switch (researchType)
                            {
                                case -4:
                                    image2.Image = (Image) GFXLibrary.research_ill_education;
                                    str = SK.Text("Research_Education", "Education");
                                    break;

                                case -3:
                                    image2.Image = (Image) GFXLibrary.research_ill_military;
                                    str = SK.Text("Research_Military", "Military");
                                    break;

                                case -2:
                                    image2.Image = (Image) GFXLibrary.research_ill_farming;
                                    str = SK.Text("Research_Farming", "Farming");
                                    break;

                                case -1:
                                    image2.Image = (Image) GFXLibrary.research_ill_industry;
                                    str = SK.Text("Research_Industry", "Industry");
                                    break;
                            }
                            control.addControl(image2);
                            CustomSelfDrawPanel.CSDLabel label = this.getNextLabel();
                            label.Text = str;
                            label.Color = ARGBColors.Black;
                            label.Position = new Point(6, 0x47);
                            label.Size = new Size(0x87, 30);
                            label.Font = font;
                            label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                            control.addControl(label);
                            continue;
                        }
                        control.Tooltip = researchType * 0x3e8;
                        int num8 = 1;
                        int num9 = -1;
                        bool flag = false;
                        int num10 = -1;
                        bool flag2 = false;
                        bool flag3 = false;
                        if (!this.lastDataQueued.isResearchStepOpen(researchType, j, rank, subRank, ref num9, ref flag))
                        {
                            num8 = 2;
                        }
                        else if (!this.lastData.isResearchStepOpen(researchType, j, rank, subRank, ref num10, ref flag2))
                        {
                            flag3 = true;
                        }
                        if (num5 == 1)
                        {
                            bool flag4 = false;
                            if ((this.lastDataQueued.research[researchType] > 0) && (this.lastData.research[researchType] == 0))
                            {
                                flag4 = true;
                            }
                            if (flag4)
                            {
                                control.Image = (Image) GFXLibrary.ill_back_yline_0101;
                            }
                            else
                            {
                                control.Image = (Image) this.getIllBack(dx, dy, 0, 1, 0, num8, researchType);
                            }
                        }
                        else if (flag3)
                        {
                            control.Image = (Image) GFXLibrary.ill_back_yline_1100;
                        }
                        else
                        {
                            control.Image = (Image) this.getIllBack(dx, dy, 1, 0, 0, num8, researchType);
                        }
                        CustomSelfDrawPanel.CSDImage image3 = this.getNextImage();
                        image3.Image = (Image) this.getIllustration(researchType);
                        image3.Position = new Point(3, 7);
                        control.addControl(image3);
                        if (num8 != 2)
                        {
                            CustomSelfDrawPanel.CSDImage image4 = this.getNextImage();
                            if (!flag3)
                            {
                                image4.Image = (Image) GFXLibrary.ill_back_green_textback;
                            }
                            else
                            {
                                image4.Image = (Image) GFXLibrary.ill_back_yellow_textback;
                            }
                            image4.Position = new Point(4, 0x44);
                            control.addControl(image4);
                        }
                        else if (num9 > 0)
                        {
                            CustomSelfDrawPanel.CSDImage image5 = this.getNextImage();
                            image5.Image = (Image) GFXLibrary.ill_shield;
                            image5.Position = new Point(0x69, 2);
                            image5.Visible = true;
                            image3.addControl(image5);
                            CustomSelfDrawPanel.CSDLabel label2 = this.getNextLabel();
                            if (num9 >= 100)
                            {
                                num30 = (num9 - 100) + 1;
                                label2.Text = num30.ToString();
                            }
                            else
                            {
                                label2.Text = num9.ToString();
                            }
                            label2.Color = ARGBColors.White;
                            label2.Position = new Point(0, -2);
                            label2.Size = image5.Size;
                            label2.Font = font2;
                            label2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            label2.Visible = true;
                            image5.addControl(label2);
                        }
                        int num11 = 0;
                        CustomSelfDrawPanel.CSDLabel label3 = this.getNextLabel();
                        if ((Program.mySettings.LanguageIdent == "tr") && ((((researchType == 14) || (researchType == 0x42)) || ((researchType == 0x2e) || (researchType == 0x29))) || ((researchType == 0x2b) || (researchType == 0x2a))))
                        {
                            label3.Font = font3;
                        }
                        else if ((Program.mySettings.LanguageIdent == "pl") && (((researchType == 14) || (researchType == 0x25)) || ((researchType == 0x2d) || (researchType == 50))))
                        {
                            label3.Font = font3;
                        }
                        else if ((Program.mySettings.LanguageIdent == "it") && (((researchType == 0x11) || (researchType == 0x43)) || (researchType == 0x29)))
                        {
                            label3.Font = font3;
                        }
                        else if ((Program.mySettings.LanguageIdent == "pt") && ((((researchType == 0) || (researchType == 0x27)) || ((researchType == 0x11) || (researchType == 0x42))) || ((((researchType == 0x40) || (researchType == 10)) || ((researchType == 0x2b) || (researchType == 0x2c))) || ((researchType == 0x2d) || (researchType == 0x2e)))))
                        {
                            label3.Font = font3;
                        }
                        else if ((Program.mySettings.LanguageIdent == "pt") && ((researchType == 0x22) || (researchType == 0x2a)))
                        {
                            label3.Font = font4;
                            num11 = -5;
                        }
                        else if (((researchType == 0x42) && (Program.mySettings.LanguageIdent == "en")) || (((researchType == 0x2d) || (researchType == 0x2b)) && (Program.mySettings.LanguageIdent == "de")))
                        {
                            label3.Font = font3;
                        }
                        else
                        {
                            label3.Font = font;
                        }
                        label3.Text = ResearchData.getResearchName(researchType);
                        label3.Color = ARGBColors.Black;
                        label3.Position = new Point(6, 0x47 + num11);
                        label3.Size = new Size(0x87, 30);
                        label3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                        control.addControl(label3);
                        if (num5 == 1)
                        {
                            CustomSelfDrawPanel.CSDImage image6 = rows[i][(j - 1) + num5];
                            int num12 = 0;
                            if ((i < (numRows - 1)) && (rows[i + 1][(j - 1) + num5] != null))
                            {
                                num12 = 1;
                            }
                            if (num12 == 1)
                            {
                                image6.Image = (Image) GFXLibrary.gline_1110;
                            }
                            else
                            {
                                image6.Image = (Image) GFXLibrary.gline_1100;
                            }
                            this.scrollPanelImage.addControl(image6);
                            for (int k = i - 1; k > 0; k--)
                            {
                                CustomSelfDrawPanel.CSDImage image8 = rows[k][(j - 1) + num5];
                                if (image8 != null)
                                {
                                    if (image8.Data != 1)
                                    {
                                        break;
                                    }
                                    image8.Image = (Image) GFXLibrary.gline_vertical;
                                    this.scrollPanelImage.addControl(image8);
                                }
                            }
                        }
                        else
                        {
                            for (int m = i - 1; m > 0; m--)
                            {
                                CustomSelfDrawPanel.CSDImage image9 = rows[m][j + num5];
                                if (image9 != null)
                                {
                                    if (image9.Data != 1)
                                    {
                                        break;
                                    }
                                    if (flag3)
                                    {
                                        image9.Image = (Image) GFXLibrary.yline_vertical;
                                    }
                                    else if (num8 == 1)
                                    {
                                        image9.Image = (Image) GFXLibrary.gline_vertical;
                                    }
                                    else
                                    {
                                        image9.Image = (Image) GFXLibrary.bline_vertical;
                                    }
                                    this.scrollPanelImage.addControl(image9);
                                }
                            }
                        }
                        continue;
                    }
                    control.Tooltip = (researchType * 0x3e8) + j;
                    int down = 0;
                    if (i < (numRows - 1))
                    {
                        CustomSelfDrawPanel.CSDImage image10 = rows[i + 1][j + num5];
                        if ((image10 != null) && ((image10.Data == 0) || (image10.Data == 1)))
                        {
                            down = 1;
                        }
                    }
                    int right = 0;
                    if (j != (num6 - 1))
                    {
                        right = 1;
                    }
                    int mode = 0;
                    bool flag5 = false;
                    bool flag6 = false;
                    int rankNeeded = -1;
                    bool special = false;
                    if (this.lastDataQueued.research[researchType] == (j - 1))
                    {
                        if (this.lastData.research[researchType] != (j - 1))
                        {
                            flag5 = true;
                        }
                        if (this.lastDataQueued.isResearchStepOpen(researchType, j - 1, rank, subRank, ref rankNeeded, ref special))
                        {
                            mode = 1;
                            if (right != 0)
                            {
                                right = 2;
                            }
                            if (down != 0)
                            {
                                down = 2;
                            }
                            if (!this.lastData.isResearchStepOpen(researchType, j - 1, rank, subRank, ref rankNeeded, ref special))
                            {
                                flag5 = true;
                            }
                        }
                        else
                        {
                            mode = 2;
                            if ((rankNeeded > 0) && special)
                            {
                                flag5 = false;
                                CustomSelfDrawPanel.CSDImage image11 = this.getNextImage();
                                image11.Image = (Image) GFXLibrary.ill_shield;
                                image11.Position = new Point(0x4b - (image11.Image.Width / 2), 7);
                                image11.Visible = true;
                                control.addControl(image11);
                                CustomSelfDrawPanel.CSDLabel label4 = this.getNextLabel();
                                if (rankNeeded >= 100)
                                {
                                    num30 = (rankNeeded - 100) + 1;
                                    label4.Text = num30.ToString();
                                }
                                else
                                {
                                    label4.Text = rankNeeded.ToString();
                                }
                                label4.Color = ARGBColors.White;
                                label4.Position = new Point(0, -2);
                                label4.Size = image11.Size;
                                label4.Font = font2;
                                label4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                                label4.Visible = true;
                                image11.addControl(label4);
                            }
                        }
                        int openedBuilding = -1;
                        int openedCastleBuilding = -1;
                        int openedTroop = -1;
                        this.getOpenedResearch(researchType, j, ref openedBuilding, ref openedCastleBuilding, ref openedTroop);
                        if (((openedBuilding >= 0) || (openedCastleBuilding > 0)) || (openedTroop > 0))
                        {
                            CustomSelfDrawPanel.CSDImage image12 = this.getNextImage();
                            if (openedBuilding >= 0)
                            {
                                image12.Image = (Image) this.getBuildingGFX(openedBuilding);
                            }
                            if (openedCastleBuilding >= 0)
                            {
                                image12.Image = (Image) this.getCastleGFX(openedCastleBuilding);
                            }
                            if (openedTroop >= 0)
                            {
                                image12.Image = (Image) this.getCastleGFX(openedTroop);
                            }
                            if (image12.Image != null)
                            {
                                image12.Position = new Point(0x70 - (image12.Image.Width / 2), 0x52 - (image12.Image.Height / 2));
                                image12.Visible = true;
                                control.addControl(image12);
                            }
                        }
                    }
                    else if (this.lastDataQueued.research[researchType] < (j - 1))
                    {
                        mode = 2;
                        this.lastDataQueued.isResearchStepOpen(researchType, j - 1, rank, subRank, ref rankNeeded, ref special);
                        if ((rankNeeded > 0) && special)
                        {
                            CustomSelfDrawPanel.CSDImage image13 = this.getNextImage();
                            image13.Image = (Image) GFXLibrary.ill_shield;
                            image13.Position = new Point(0x4b - (image13.Image.Width / 2), 7);
                            image13.Visible = true;
                            control.addControl(image13);
                            CustomSelfDrawPanel.CSDLabel label5 = this.getNextLabel();
                            if (rankNeeded >= 100)
                            {
                                label5.Text = ((rankNeeded - 100) + 1).ToString();
                            }
                            else
                            {
                                label5.Text = rankNeeded.ToString();
                            }
                            label5.Color = ARGBColors.White;
                            label5.Position = new Point(0, -2);
                            label5.Size = image13.Size;
                            label5.Font = font2;
                            label5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            label5.Visible = true;
                            image13.addControl(label5);
                        }
                        int num22 = -1;
                        int num23 = -1;
                        int num24 = -1;
                        this.getOpenedResearch(researchType, j, ref num22, ref num23, ref num24);
                        if (((num22 >= 0) || (num23 > 0)) || (num24 > 0))
                        {
                            CustomSelfDrawPanel.CSDImage image14 = this.getNextImage();
                            if (num22 >= 0)
                            {
                                image14.Image = (Image) this.getBuildingGFX(num22);
                            }
                            if (num23 >= 0)
                            {
                                image14.Image = (Image) this.getCastleGFX(num23);
                            }
                            if (num24 >= 0)
                            {
                                image14.Image = (Image) this.getCastleGFX(num24);
                            }
                            if (image14.Image != null)
                            {
                                image14.Position = new Point(0x70 - (image14.Image.Width / 2), 0x52 - (image14.Image.Height / 2));
                                image14.Visible = true;
                                control.addControl(image14);
                            }
                        }
                    }
                    else
                    {
                        if (this.lastData.research[researchType] == (j - 1))
                        {
                            if (j > 1)
                            {
                                flag6 = true;
                            }
                            flag5 = true;
                        }
                        else if (this.lastData.research[researchType] < (j - 1))
                        {
                            flag5 = true;
                        }
                        mode = 0;
                        if (down != 0)
                        {
                            int num25 = 0;
                            int num26 = -1;
                            int num27 = -1;
                            int num28 = this.getOpenedResearch(researchType, j, ref num25, ref num26, ref num27);
                            if (num28 >= 0)
                            {
                                int num29 = -1;
                                bool flag8 = false;
                                if (!this.lastDataQueued.isResearchStepOpen(num28, 0, rank, subRank, ref num29, ref flag8))
                                {
                                    down = 2;
                                }
                            }
                        }
                        if ((right != 0) && !this.lastDataQueued.isResearchStepOpen(researchType, j, rank, subRank, ref rankNeeded, ref special))
                        {
                            right = 2;
                        }
                    }
                    if (flag6)
                    {
                        control.Image = (Image) this.getTransitionCircle(dx, dy, mode, 0, 1, down, right, researchType, j - 1);
                    }
                    else if (!flag5)
                    {
                        control.Image = (Image) this.getCircle(dx, dy, mode, 0, 1, down, right, researchType, j - 1);
                    }
                    else
                    {
                        control.Image = (Image) this.getYellowCircle(dx, dy, mode, 0, 1, down, right, researchType, j - 1);
                    }
                    CustomSelfDrawPanel.CSDImage image15 = this.getNextImage();
                    if (!flag5)
                    {
                        image15.Image = (Image) this.getNumberImage(j, mode);
                    }
                    else
                    {
                        image15.Image = (Image) this.getNumberImage(j, 2);
                    }
                    image15.Position = new Point((control.Size.Width / 2) - (image15.Size.Width / 2), (control.Size.Height / 2) - (image15.Size.Height / 2));
                    control.addControl(image15);
                }
            }
        }

        private void windowDragged()
        {
            int num = -this.dragOverlay.XDiff;
            int num2 = -this.dragOverlay.YDiff;
            num *= 2;
            num2 *= 2;
            if ((this.scrollPanelImage.ClipRect.X + num) < 0)
            {
                num = -this.scrollPanelImage.ClipRect.X;
            }
            if ((this.scrollPanelImage.ClipRect.Y + num2) < 0)
            {
                num2 = -this.scrollPanelImage.ClipRect.Y;
            }
            double windowScale = this.m_windowScale;
            int num4 = (int) (this.scrollPanelImage.Size.Width * windowScale);
            int num5 = (int) (this.scrollPanelImage.Size.Height * windowScale);
            if ((this.scrollPanelImage.ClipRect.X + num) > (num4 - this.scrollPanelImage.ClipRect.Width))
            {
                num -= (this.scrollPanelImage.ClipRect.X + num) - (num4 - this.scrollPanelImage.ClipRect.Width);
            }
            if ((this.scrollPanelImage.ClipRect.Y + num2) > (num5 - this.scrollPanelImage.ClipRect.Height))
            {
                num2 -= (this.scrollPanelImage.ClipRect.Y + num2) - (num5 - this.scrollPanelImage.ClipRect.Height);
            }
            this.scrollPanelImage.Position = new Point(this.scrollPanelImage.Position.X - num, this.scrollPanelImage.Position.Y - num2);
            this.scrollPanelImage.ClipRect = new Rectangle(this.scrollPanelImage.ClipRect.X + num, this.scrollPanelImage.ClipRect.Y + num2, this.scrollPanelImage.ClipRect.Width, this.scrollPanelImage.ClipRect.Height);
            this.scrollPanelImage.invalidate();
            this.lastScrollXPos = (int) (((double) (-(this.scrollPanelImage.Position.X - 20) + (this.scrollPanelImage.ClipRect.Width / 2))) / windowScale);
            this.lastScrollYPos = (int) (((double) (-((this.scrollPanelImage.Position.Y - 0xbb) - 0x37) + (this.scrollPanelImage.ClipRect.Height / 2))) / windowScale);
        }
    }
}

