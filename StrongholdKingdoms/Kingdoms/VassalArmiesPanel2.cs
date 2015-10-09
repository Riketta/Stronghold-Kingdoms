namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VassalArmiesPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private bool allowSliderUpdate = true;
        private CustomSelfDrawPanel.CSDButton archerEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel archerName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel archerName2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel archerSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel archerStationedValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel archerStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar archerTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDLabel atVassalLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnSend = new CustomSelfDrawPanel.CSDButton();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton catapultEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel catapultName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel catapultName2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel catapultSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel catapultStationedValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel catapultStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar catapultTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private IContainer components;
        private CustomSelfDrawPanel.CSDTrackBar currentTrack;
        private DockableControl dockableControl;
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static VassalArmiesPanel2 instance;
        private int m_maxCanSend;
        private int m_playerVillageID = -1;
        private int m_vassalVillageID = -1;
        private CustomSelfDrawPanel.CSDLabel maxValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel maxValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel peasantName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel peasantName2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton peasantsEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel peasantSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel peasantStationedValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel peasantStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar peasantsTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDButton pikemanEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel pikemanName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pikemanName2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pikemanSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pikemanStationedValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pikemanStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar pikemanTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDLabel stationedTotalLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel stationedTotalValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton swordsmanEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel swordsmanName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel swordsmanName2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel swordsmanSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel swordsmanStationedValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel swordsmanStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar swordsmanTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDImage trackBackImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel troopsAvailableToSendLabel = new CustomSelfDrawPanel.CSDLabel();

        public VassalArmiesPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.setVillageTabSubMode(8);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
            InterfaceMgr.Instance.closeDonatePopup();
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

        private void editSendValue()
        {
            CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
            int num = 0x110;
            switch (clickedControl.Data)
            {
                case 1:
                    this.currentTrack = this.peasantsTrack;
                    break;

                case 2:
                    this.currentTrack = this.archerTrack;
                    num += 40;
                    break;

                case 3:
                    this.currentTrack = this.pikemanTrack;
                    num += 80;
                    break;

                case 4:
                    this.currentTrack = this.swordsmanTrack;
                    num += 120;
                    break;

                case 5:
                    this.currentTrack = this.catapultTrack;
                    num += 160;
                    break;
            }
            InterfaceMgr.Instance.setFloatingValueSentDelegate(new InterfaceMgr.FloatingValueSent(this.setTrackCB));
            Point point = InterfaceMgr.Instance.ParentForm.PointToScreen(new Point(base.Location.X + 0x30f, base.Location.Y + num));
            FloatingInput.open(point.X, point.Y, this.currentTrack.Value, this.currentTrack.Max, InterfaceMgr.Instance.ParentForm);
        }

        public void getVassalArmyInfoCallback(GetVassalArmyInfo_ReturnType returnData)
        {
            if (((returnData != null) && returnData.Success) && (this.m_vassalVillageID == returnData.vassalVillageID))
            {
                this.peasantStationedValue.Text = ((returnData.numStationedTroops_Peasants + returnData.numAttackingTroops_Peasants) + returnData.numEnrouteTroops_Peasants).ToString();
                this.archerStationedValue.Text = ((returnData.numStationedTroops_Archers + returnData.numAttackingTroops_Archers) + returnData.numEnrouteTroops_Archers).ToString();
                this.pikemanStationedValue.Text = ((returnData.numStationedTroops_Pikemen + returnData.numAttackingTroops_Pikemen) + returnData.numEnrouteTroops_Pikemen).ToString();
                this.swordsmanStationedValue.Text = ((returnData.numStationedTroops_Swordsmen + returnData.numAttackingTroops_Swordsmen) + returnData.numEnrouteTroops_Swordsmen).ToString();
                this.catapultStationedValue.Text = ((returnData.numStationedTroops_Catapults + returnData.numAttackingTroops_Catapults) + returnData.numEnrouteTroops_Catapults).ToString();
                int num = ResearchData.commandResearchTroopLevels[GameEngine.Instance.World.userResearchData.Research_Command];
                this.maxValue.Text = num.ToString();
                int totalTroops = returnData.TotalTroops;
                this.stationedTotalValue.Text = totalTroops.ToString();
                this.m_maxCanSend = num - totalTroops;
                if (this.m_maxCanSend < 0)
                {
                    this.m_maxCanSend = 0;
                }
                this.peasantsTrack.Value = 0;
                this.archerTrack.Value = 0;
                this.pikemanTrack.Value = 0;
                this.swordsmanTrack.Value = 0;
                this.catapultTrack.Value = 0;
                this.peasantsTrack.Max = 0;
                this.archerTrack.Max = 0;
                this.pikemanTrack.Max = 0;
                this.swordsmanTrack.Max = 0;
                this.catapultTrack.Max = 0;
                this.updateValues();
                this.btnSend.Enabled = false;
            }
        }

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            this.backgroundImage.Image = (Image) GFXLibrary.body_background_002;
            this.backgroundImage.Size = new Size(base.Width, height - 40);
            this.backgroundImage.Tile = true;
            this.backgroundImage.Position = new Point(0, 40);
            base.addControl(this.backgroundImage);
            this.backgroundLeftEdge.Image = (Image) GFXLibrary.body_background_canvas_left_edge;
            this.backgroundLeftEdge.Position = new Point(0, 0);
            this.backgroundLeftEdge.Size = new Size(this.backgroundLeftEdge.Image.Width, height - 40);
            this.backgroundLeftEdge.Tile = true;
            this.backgroundImage.addControl(this.backgroundLeftEdge);
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.CreateX((Image) GFXLibrary.mail_top_drag_bar_left, (Image) GFXLibrary.mail_top_drag_bar_middle, (Image) GFXLibrary.mail_top_drag_bar_right, -2, 2);
            this.parishNameLabel.Text = SK.Text("Vassal_Manage_Vassal_Troops", "Manage Vassal Troops") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_vassalVillageID);
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.cardbar.Position = new Point(0, 4);
            this.backgroundImage.addControl(this.cardbar);
            this.cardbar.init(6);
            this.atVassalLabel.Text = SK.Text("Vassal_StationedTroops", "Stationed Troops");
            this.atVassalLabel.Position = new Point(0x73, 0x3a);
            this.atVassalLabel.Size = new Size(220, 40);
            this.atVassalLabel.Color = ARGBColors.Black;
            this.atVassalLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.atVassalLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
            this.backgroundImage.addControl(this.atVassalLabel);
            this.troopsAvailableToSendLabel.Text = SK.Text("Vassal_AvailableToSend", "Troops Available to Send");
            this.troopsAvailableToSendLabel.Position = new Point(0x163, 0x3a);
            this.troopsAvailableToSendLabel.Size = new Size(520, 40);
            this.troopsAvailableToSendLabel.Color = ARGBColors.Black;
            this.troopsAvailableToSendLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.troopsAvailableToSendLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_LEFT;
            this.backgroundImage.addControl(this.troopsAvailableToSendLabel);
            this.trackBackImage.Image = (Image) GFXLibrary.reinforce_Vassal_screen_back;
            this.trackBackImage.Position = new Point(100, 100);
            this.backgroundImage.addControl(this.trackBackImage);
            int y = 14;
            this.peasantName2.Text = SK.Text("GENERIC_Peasants", "Peasants");
            this.peasantName2.Position = new Point(-50, y);
            this.peasantName2.Size = new Size(0x8e, 40);
            this.peasantName2.Color = ARGBColors.Black;
            this.peasantName2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.peasantName2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.peasantName2);
            this.archerName2.Text = SK.Text("GENERIC_Archers", "Archers");
            this.archerName2.Position = new Point(-50, y + 40);
            this.archerName2.Size = new Size(0x8e, 40);
            this.archerName2.Color = ARGBColors.Black;
            this.archerName2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.archerName2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.archerName2);
            this.pikemanName2.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
            this.pikemanName2.Position = new Point(-50, y + 80);
            this.pikemanName2.Size = new Size(0x8e, 40);
            this.pikemanName2.Color = ARGBColors.Black;
            this.pikemanName2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pikemanName2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.pikemanName2);
            this.swordsmanName2.Text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
            this.swordsmanName2.Position = new Point(-50, y + 120);
            this.swordsmanName2.Size = new Size(0x8e, 40);
            this.swordsmanName2.Color = ARGBColors.Black;
            this.swordsmanName2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.swordsmanName2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.swordsmanName2);
            this.catapultName2.Text = SK.Text("GENERIC_Catapults", "Catapults");
            this.catapultName2.Position = new Point(-50, y + 160);
            this.catapultName2.Size = new Size(0x8e, 40);
            this.catapultName2.Color = ARGBColors.Black;
            this.catapultName2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.catapultName2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.catapultName2);
            this.stationedTotalLabel.Text = SK.Text("GENERIC_Total_Troops", "Total Troops");
            this.stationedTotalLabel.Position = new Point(-50, y + 200);
            this.stationedTotalLabel.Size = new Size(0x8e, 40);
            this.stationedTotalLabel.Color = ARGBColors.Black;
            this.stationedTotalLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.stationedTotalLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.stationedTotalLabel);
            this.maxValueLabel.Text = SK.Text("GENERIC_Max_Troops", "Max Troops");
            this.maxValueLabel.Position = new Point(-50, y + 240);
            this.maxValueLabel.Size = new Size(0x8e, 40);
            this.maxValueLabel.Color = ARGBColors.Black;
            this.maxValueLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.maxValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.maxValueLabel);
            this.peasantStationedValue.Text = "0";
            this.peasantStationedValue.Position = new Point(0x38, y);
            this.peasantStationedValue.Size = new Size(0x8e, 40);
            this.peasantStationedValue.Color = ARGBColors.Black;
            this.peasantStationedValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.peasantStationedValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.peasantStationedValue);
            this.archerStationedValue.Text = "0";
            this.archerStationedValue.Position = new Point(0x38, y + 40);
            this.archerStationedValue.Size = new Size(0x8e, 40);
            this.archerStationedValue.Color = ARGBColors.Black;
            this.archerStationedValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.archerStationedValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.archerStationedValue);
            this.pikemanStationedValue.Text = "0";
            this.pikemanStationedValue.Position = new Point(0x38, y + 80);
            this.pikemanStationedValue.Size = new Size(0x8e, 40);
            this.pikemanStationedValue.Color = ARGBColors.Black;
            this.pikemanStationedValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pikemanStationedValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.pikemanStationedValue);
            this.swordsmanStationedValue.Text = "0";
            this.swordsmanStationedValue.Position = new Point(0x38, y + 120);
            this.swordsmanStationedValue.Size = new Size(0x8e, 40);
            this.swordsmanStationedValue.Color = ARGBColors.Black;
            this.swordsmanStationedValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.swordsmanStationedValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.swordsmanStationedValue);
            this.catapultStationedValue.Text = "0";
            this.catapultStationedValue.Position = new Point(0x38, y + 160);
            this.catapultStationedValue.Size = new Size(0x8e, 40);
            this.catapultStationedValue.Color = ARGBColors.Black;
            this.catapultStationedValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.catapultStationedValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.catapultStationedValue);
            this.stationedTotalValue.Text = "0";
            this.stationedTotalValue.Position = new Point(0x38, y + 200);
            this.stationedTotalValue.Size = new Size(0x8e, 40);
            this.stationedTotalValue.Color = ARGBColors.Black;
            this.stationedTotalValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.stationedTotalValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.stationedTotalValue);
            this.maxValue.Text = "0";
            this.maxValue.Position = new Point(0x38, y + 240);
            this.maxValue.Size = new Size(0x8e, 40);
            this.maxValue.Color = ARGBColors.Black;
            this.maxValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.maxValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.maxValue);
            this.peasantName.Text = SK.Text("GENERIC_Peasants", "Peasants");
            this.peasantName.Position = new Point(190, y);
            this.peasantName.Size = new Size(0x8e, 40);
            this.peasantName.Color = ARGBColors.Black;
            this.peasantName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.peasantName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.peasantName);
            this.archerName.Text = SK.Text("GENERIC_Archers", "Archers");
            this.archerName.Position = new Point(190, y + 40);
            this.archerName.Size = new Size(0x8e, 40);
            this.archerName.Color = ARGBColors.Black;
            this.archerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.archerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.archerName);
            this.pikemanName.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
            this.pikemanName.Position = new Point(190, y + 80);
            this.pikemanName.Size = new Size(0x8e, 40);
            this.pikemanName.Color = ARGBColors.Black;
            this.pikemanName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pikemanName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.pikemanName);
            this.swordsmanName.Text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
            this.swordsmanName.Position = new Point(190, y + 120);
            this.swordsmanName.Size = new Size(0x8e, 40);
            this.swordsmanName.Color = ARGBColors.Black;
            this.swordsmanName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.swordsmanName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.swordsmanName);
            this.catapultName.Text = SK.Text("GENERIC_Catapults", "Catapults");
            this.catapultName.Position = new Point(190, y + 160);
            this.catapultName.Size = new Size(0x8e, 40);
            this.catapultName.Color = ARGBColors.Black;
            this.catapultName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.catapultName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.catapultName);
            this.peasantStoredValue.Text = "0";
            this.peasantStoredValue.Position = new Point(0x128, y);
            this.peasantStoredValue.Size = new Size(0x8e, 40);
            this.peasantStoredValue.Color = ARGBColors.Black;
            this.peasantStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.peasantStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.peasantStoredValue);
            this.archerStoredValue.Text = "0";
            this.archerStoredValue.Position = new Point(0x128, y + 40);
            this.archerStoredValue.Size = new Size(0x8e, 40);
            this.archerStoredValue.Color = ARGBColors.Black;
            this.archerStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.archerStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.archerStoredValue);
            this.pikemanStoredValue.Text = "0";
            this.pikemanStoredValue.Position = new Point(0x128, y + 80);
            this.pikemanStoredValue.Size = new Size(0x8e, 40);
            this.pikemanStoredValue.Color = ARGBColors.Black;
            this.pikemanStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pikemanStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.pikemanStoredValue);
            this.swordsmanStoredValue.Text = "0";
            this.swordsmanStoredValue.Position = new Point(0x128, y + 120);
            this.swordsmanStoredValue.Size = new Size(0x8e, 40);
            this.swordsmanStoredValue.Color = ARGBColors.Black;
            this.swordsmanStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.swordsmanStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.swordsmanStoredValue);
            this.catapultStoredValue.Text = "0";
            this.catapultStoredValue.Position = new Point(0x128, y + 160);
            this.catapultStoredValue.Size = new Size(0x8e, 40);
            this.catapultStoredValue.Color = ARGBColors.Black;
            this.catapultStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.catapultStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.catapultStoredValue);
            this.peasantSendValue.Text = "0";
            this.peasantSendValue.Position = new Point(0x128, y);
            this.peasantSendValue.Size = new Size(0x192, 40);
            this.peasantSendValue.Color = ARGBColors.Black;
            this.peasantSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.peasantSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.peasantSendValue);
            this.archerSendValue.Text = "0";
            this.archerSendValue.Position = new Point(0x128, y + 40);
            this.archerSendValue.Size = new Size(0x192, 40);
            this.archerSendValue.Color = ARGBColors.Black;
            this.archerSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.archerSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.archerSendValue);
            this.pikemanSendValue.Text = "0";
            this.pikemanSendValue.Position = new Point(0x128, y + 80);
            this.pikemanSendValue.Size = new Size(0x192, 40);
            this.pikemanSendValue.Color = ARGBColors.Black;
            this.pikemanSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.pikemanSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.pikemanSendValue);
            this.swordsmanSendValue.Text = "0";
            this.swordsmanSendValue.Position = new Point(0x128, y + 120);
            this.swordsmanSendValue.Size = new Size(0x192, 40);
            this.swordsmanSendValue.Color = ARGBColors.Black;
            this.swordsmanSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.swordsmanSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.swordsmanSendValue);
            this.catapultSendValue.Text = "0";
            this.catapultSendValue.Position = new Point(0x128, y + 160);
            this.catapultSendValue.Size = new Size(0x192, 40);
            this.catapultSendValue.Color = ARGBColors.Black;
            this.catapultSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.catapultSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.trackBackImage.addControl(this.catapultSendValue);
            this.peasantsTrack.Position = new Point(0x1bf, 15);
            this.peasantsTrack.Size = new Size(0xcb, 0x17);
            this.peasantsTrack.Max = 100;
            if (!resized)
            {
                this.peasantsTrack.Value = 0;
            }
            this.peasantsTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved0));
            this.trackBackImage.addControl(this.peasantsTrack);
            this.peasantsTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
            this.peasantsEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.peasantsEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.peasantsEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.peasantsEditButton.MoveOnClick = true;
            this.peasantsEditButton.OverBrighten = true;
            this.peasantsEditButton.Position = new Point(0x293, 12);
            this.peasantsEditButton.Data = 1;
            this.peasantsEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
            this.trackBackImage.addControl(this.peasantsEditButton);
            this.archerTrack.Position = new Point(0x1bf, 0x37);
            this.archerTrack.Size = new Size(0xcb, 0x17);
            this.archerTrack.Max = 100;
            if (!resized)
            {
                this.archerTrack.Value = 0;
            }
            this.archerTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved1));
            this.trackBackImage.addControl(this.archerTrack);
            this.archerTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
            this.archerEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.archerEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.archerEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.archerEditButton.MoveOnClick = true;
            this.archerEditButton.OverBrighten = true;
            this.archerEditButton.Position = new Point(0x293, 0x34);
            this.archerEditButton.Data = 2;
            this.archerEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
            this.trackBackImage.addControl(this.archerEditButton);
            this.pikemanTrack.Position = new Point(0x1bf, 0x5f);
            this.pikemanTrack.Size = new Size(0xcb, 0x17);
            this.pikemanTrack.Max = 100;
            if (!resized)
            {
                this.pikemanTrack.Value = 0;
            }
            this.pikemanTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved2));
            this.trackBackImage.addControl(this.pikemanTrack);
            this.pikemanTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
            this.pikemanEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.pikemanEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.pikemanEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.pikemanEditButton.MoveOnClick = true;
            this.pikemanEditButton.OverBrighten = true;
            this.pikemanEditButton.Position = new Point(0x293, 0x5c);
            this.pikemanEditButton.Data = 3;
            this.pikemanEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
            this.trackBackImage.addControl(this.pikemanEditButton);
            this.swordsmanTrack.Position = new Point(0x1bf, 0x87);
            this.swordsmanTrack.Size = new Size(0xcb, 0x17);
            this.swordsmanTrack.Max = 100;
            if (!resized)
            {
                this.swordsmanTrack.Value = 0;
            }
            this.swordsmanTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved3));
            this.trackBackImage.addControl(this.swordsmanTrack);
            this.swordsmanTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
            this.swordsmanEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.swordsmanEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.swordsmanEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.swordsmanEditButton.MoveOnClick = true;
            this.swordsmanEditButton.OverBrighten = true;
            this.swordsmanEditButton.Position = new Point(0x293, 0x84);
            this.swordsmanEditButton.Data = 4;
            this.swordsmanEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
            this.trackBackImage.addControl(this.swordsmanEditButton);
            this.catapultTrack.Position = new Point(0x1bf, 0xaf);
            this.catapultTrack.Size = new Size(0xcb, 0x17);
            this.catapultTrack.Max = 100;
            if (!resized)
            {
                this.catapultTrack.Value = 0;
            }
            this.catapultTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved4));
            this.trackBackImage.addControl(this.catapultTrack);
            this.catapultTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
            this.catapultEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
            this.catapultEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
            this.catapultEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
            this.catapultEditButton.MoveOnClick = true;
            this.catapultEditButton.OverBrighten = true;
            this.catapultEditButton.Position = new Point(0x293, 0xac);
            this.catapultEditButton.Data = 5;
            this.catapultEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
            this.trackBackImage.addControl(this.catapultEditButton);
            this.btnSend.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
            this.btnSend.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
            this.btnSend.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
            this.btnSend.Position = new Point(600, 0xcd);
            this.btnSend.Text.Text = SK.Text("VassalArmiesPanel_", "Send");
            this.btnSend.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnSend.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnSend.TextYOffset = -3;
            this.btnSend.Text.Color = ARGBColors.Black;
            this.btnSend.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendClick), "VassalArmiesPanel2_send");
            this.btnSend.Enabled = false;
            this.trackBackImage.addControl(this.btnSend);
            if (GameEngine.Instance.World.isVassal(this.m_playerVillageID, this.m_vassalVillageID))
            {
                this.btnSend.Visible = true;
            }
            else
            {
                this.btnSend.Visible = false;
            }
            this.updateValues();
            if (!resized)
            {
                RemoteServices.Instance.set_GetVassalArmyInfo_UserCallBack(new RemoteServices.GetVassalArmyInfo_UserCallBack(this.getVassalArmyInfoCallback));
                RemoteServices.Instance.GetVassalArmyInfo(this.m_vassalVillageID, 0, -1);
            }
            this.btnClose.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
            this.btnClose.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
            this.btnClose.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
            this.btnClose.Position = new Point(base.Width - 230, ((height - 40) - 40) - 4);
            this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.btnClose.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnClose.TextYOffset = -3;
            this.btnClose.Text.Color = ARGBColors.Black;
            this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "VassalArmiesPanel2_close");
            this.backgroundImage.addControl(this.btnClose);
        }

        private void InitializeComponent()
        {
            this.focusPanel = new Panel();
            base.SuspendLayout();
            this.focusPanel.BackColor = ARGBColors.Transparent;
            this.focusPanel.ForeColor = ARGBColors.Transparent;
            this.focusPanel.Location = new Point(0x3dc, 3);
            this.focusPanel.Name = "focusPanel";
            this.focusPanel.Size = new Size(1, 1);
            this.focusPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.focusPanel);
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "VassalArmiesPanel2";
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

        public void logout()
        {
        }

        private void sendClick()
        {
            int num = (((this.peasantsTrack.Value + this.archerTrack.Value) + this.pikemanTrack.Value) + this.swordsmanTrack.Value) + this.catapultTrack.Value;
            if (num > 0)
            {
                VillageMap map = GameEngine.Instance.getVillage(this.m_playerVillageID);
                if (map != null)
                {
                    RemoteServices.Instance.set_SendTroopsToVassal_UserCallBack(new RemoteServices.SendTroopsToVassal_UserCallBack(this.sendTroopsToVassalCallback));
                    RemoteServices.Instance.SendTroopsToVassal(map.VillageID, this.m_vassalVillageID, this.peasantsTrack.Value, this.archerTrack.Value, this.pikemanTrack.Value, this.swordsmanTrack.Value, this.catapultTrack.Value);
                    this.allowSliderUpdate = false;
                    this.peasantsTrack.Value = 0;
                    this.archerTrack.Value = 0;
                    this.pikemanTrack.Value = 0;
                    this.swordsmanTrack.Value = 0;
                    this.catapultTrack.Value = 0;
                    this.updateSliderValues(0);
                    this.allowSliderUpdate = true;
                    this.btnSend.Enabled = false;
                }
            }
        }

        public void sendTroopsToVassalCallback(SendTroopsToVassal_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.vassalArmyReturnData != null)
                {
                    this.getVassalArmyInfoCallback(returnData.vassalArmyReturnData);
                }
                if (returnData.armyData != null)
                {
                    ArmyReturnData[] armyReturnData = new ArmyReturnData[] { returnData.armyData };
                    GameEngine.Instance.World.doGetArmyData(armyReturnData, null, false);
                    VillageMap map = GameEngine.Instance.getVillage(returnData.villageID);
                    if (map != null)
                    {
                        map.addTroops(-returnData.numPeasants, -returnData.numArchers, -returnData.numPikemen, -returnData.numSwordsmen, -returnData.numCatapults);
                    }
                    this.updateValues();
                }
            }
        }

        private void setTrackCB(int value)
        {
            if (this.currentTrack != null)
            {
                this.currentTrack.Value = value;
                this.updateSlider();
            }
        }

        public void setVassalArmiesVillage(int villageID)
        {
            if (villageID >= 0)
            {
                this.m_vassalVillageID = villageID;
                this.m_playerVillageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            }
            else
            {
                this.m_vassalVillageID = -1;
            }
        }

        public void tracksMoved0()
        {
            if (this.allowSliderUpdate)
            {
                this.allowSliderUpdate = false;
                this.updateSliderValues(0);
                this.allowSliderUpdate = true;
            }
        }

        public void tracksMoved1()
        {
            if (this.allowSliderUpdate)
            {
                this.allowSliderUpdate = false;
                this.updateSliderValues(1);
                this.allowSliderUpdate = true;
            }
        }

        public void tracksMoved2()
        {
            if (this.allowSliderUpdate)
            {
                this.allowSliderUpdate = false;
                this.updateSliderValues(2);
                this.allowSliderUpdate = true;
            }
        }

        public void tracksMoved3()
        {
            if (this.allowSliderUpdate)
            {
                this.allowSliderUpdate = false;
                this.updateSliderValues(3);
                this.allowSliderUpdate = true;
            }
        }

        public void tracksMoved4()
        {
            if (this.allowSliderUpdate)
            {
                this.allowSliderUpdate = false;
                this.updateSliderValues(4);
                this.allowSliderUpdate = true;
            }
        }

        public void update()
        {
            this.cardbar.update();
        }

        public void updateSlider()
        {
            this.peasantSendValue.Text = this.peasantsTrack.Value.ToString();
            this.archerSendValue.Text = this.archerTrack.Value.ToString();
            this.pikemanSendValue.Text = this.pikemanTrack.Value.ToString();
            this.swordsmanSendValue.Text = this.swordsmanTrack.Value.ToString();
            this.catapultSendValue.Text = this.catapultTrack.Value.ToString();
            int num = (((this.peasantsTrack.Value + this.archerTrack.Value) + this.pikemanTrack.Value) + this.swordsmanTrack.Value) + this.catapultTrack.Value;
            if (num > 0)
            {
                this.btnSend.Enabled = true;
            }
            else
            {
                this.btnSend.Enabled = false;
            }
        }

        public void updateSliderValues(int type)
        {
            int num = (((this.peasantsTrack.Value + this.archerTrack.Value) + this.pikemanTrack.Value) + this.swordsmanTrack.Value) + this.catapultTrack.Value;
            int num2 = num;
            if (num > this.m_maxCanSend)
            {
                if (this.m_maxCanSend == 0)
                {
                    this.peasantsTrack.Value = 0;
                    this.archerTrack.Value = 0;
                    this.pikemanTrack.Value = 0;
                    this.swordsmanTrack.Value = 0;
                    this.catapultTrack.Value = 0;
                    return;
                }
                num -= this.m_maxCanSend;
                int num3 = 0;
                while (num > 0)
                {
                    if (num3 != type)
                    {
                        switch (num3)
                        {
                            case 0:
                                if (this.peasantsTrack.Value > 0)
                                {
                                    this.peasantsTrack.Value--;
                                    num--;
                                }
                                break;

                            case 1:
                                if (this.archerTrack.Value > 0)
                                {
                                    this.archerTrack.Value--;
                                    num--;
                                }
                                break;

                            case 2:
                                if (this.pikemanTrack.Value > 0)
                                {
                                    this.pikemanTrack.Value--;
                                    num--;
                                }
                                break;

                            case 3:
                                if (this.swordsmanTrack.Value > 0)
                                {
                                    this.swordsmanTrack.Value--;
                                    num--;
                                }
                                break;

                            case 4:
                                if (this.catapultTrack.Value > 0)
                                {
                                    this.catapultTrack.Value--;
                                    num--;
                                }
                                break;
                        }
                    }
                    num3++;
                    if (num3 >= 5)
                    {
                        num3 = 0;
                    }
                }
            }
            this.peasantSendValue.Text = this.peasantsTrack.Value.ToString();
            this.archerSendValue.Text = this.archerTrack.Value.ToString();
            this.pikemanSendValue.Text = this.pikemanTrack.Value.ToString();
            this.swordsmanSendValue.Text = this.swordsmanTrack.Value.ToString();
            this.catapultSendValue.Text = this.catapultTrack.Value.ToString();
            if (num2 > 0)
            {
                this.btnSend.Enabled = true;
            }
            else
            {
                this.btnSend.Enabled = false;
            }
        }

        public void updateValues()
        {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
                this.peasantStoredValue.Text = village.m_numPeasants.ToString();
                this.archerStoredValue.Text = village.m_numArchers.ToString();
                this.pikemanStoredValue.Text = village.m_numPikemen.ToString();
                this.swordsmanStoredValue.Text = village.m_numSwordsmen.ToString();
                this.catapultStoredValue.Text = village.m_numCatapults.ToString();
                this.peasantsTrack.Max = Math.Max(Math.Min(village.m_numPeasants, this.m_maxCanSend), 0);
                this.archerTrack.Max = Math.Max(Math.Min(village.m_numArchers, this.m_maxCanSend), 0);
                this.pikemanTrack.Max = Math.Max(Math.Min(village.m_numPikemen, this.m_maxCanSend), 0);
                this.swordsmanTrack.Max = Math.Max(Math.Min(village.m_numSwordsmen, this.m_maxCanSend), 0);
                this.catapultTrack.Max = Math.Max(Math.Min(village.m_numCatapults, this.m_maxCanSend), 0);
                this.updateSlider();
            }
        }
    }
}

