namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VillageReinforcementsPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton archerEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel archerName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel archerSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel archerStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar archerTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private ArmyComparer armyComparer = new ArmyComparer();
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnSend = new CustomSelfDrawPanel.CSDButton();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton catapultEditButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDTrackBar currentTrack;
        private CustomSelfDrawPanel.CSDLabel diplomacyHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel diplomacyTextLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private const int extraYForCards = 0x4c;
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel incomingArrivesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel incomingAttackingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel incomingAttacksLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea incomingScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar incomingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        public static VillageReinforcementsPanel2 instance;
        private List<ArmyLine> lineList = new List<ArmyLine>();
        private List<ArmyLine> lineList2 = new List<ArmyLine>();
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDLabel outGoingArrivesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel outGoingAttacksLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel peasantName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton peasantsEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel peasantSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel peasantStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar peasantsTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDButton pikemanEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel pikemanName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pikemanSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pikemanStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar pikemanTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private ReinforcementsRetrievalPopup popup;
        private List<WorldMap.LocalArmyData> reinforcementList = new List<WorldMap.LocalArmyData>();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton swordsmanEditButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel swordsmanName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel swordsmanSendValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel swordsmanStoredValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar swordsmanTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDImage targetVillageImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel targetVillageLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel targetVillageName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage trackBackImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage villageBackImage = new CustomSelfDrawPanel.CSDImage();
        private int yOffset;

        public VillageReinforcementsPanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        public void addArmies()
        {
            this.outgoingScrollArea.clearControls();
            this.incomingScrollArea.clearControls();
            int y = 0;
            int num2 = 0;
            this.lineList.Clear();
            this.lineList2.Clear();
            int num3 = InterfaceMgr.Instance.getSelectedMenuVillage();
            int position = 0;
            int num5 = 0;
            double num1 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (WorldMap.LocalArmyData data in this.reinforcementList)
            {
                if (data.homeVillageID == num3)
                {
                    ArmyLine control = new ArmyLine();
                    if (y != 0)
                    {
                        y += 5;
                    }
                    control.Position = new Point(0, y);
                    control.initSent(position, data.targetVillageID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults, 0, data.serverEndTime, data.armyID, data.attackType == 20, this, data.attackType == 0x15);
                    this.outgoingScrollArea.addControl(control);
                    y += control.Height;
                    this.lineList.Add(control);
                    position++;
                }
                if ((data.targetVillageID == num3) && (data.attackType == 20))
                {
                    ArmyLine line2 = new ArmyLine();
                    if (num2 != 0)
                    {
                        num2 += 5;
                    }
                    line2.Position = new Point(0, num2);
                    line2.initIncoming(num5, data.homeVillageID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults, 0, data.serverEndTime, data.armyID, true, this, data.attackType == 0x15);
                    this.incomingScrollArea.addControl(line2);
                    num2 += line2.Height;
                    this.lineList2.Add(line2);
                    num5++;
                }
            }
            this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, y);
            if (y < this.outgoingScrollBar.Height)
            {
                this.outgoingScrollBar.Visible = false;
            }
            else
            {
                this.outgoingScrollBar.Visible = true;
                this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
                this.outgoingScrollBar.Max = y - this.outgoingScrollBar.Height;
            }
            this.outgoingScrollArea.invalidate();
            this.outgoingScrollBar.invalidate();
            this.incomingScrollArea.Size = new Size(this.incomingScrollArea.Width, num2);
            if (num2 < this.incomingScrollBar.Height)
            {
                this.incomingScrollBar.Visible = false;
            }
            else
            {
                this.incomingScrollBar.Visible = true;
                this.incomingScrollBar.NumVisibleLines = this.incomingScrollBar.Height;
                this.incomingScrollBar.Max = num2 - this.incomingScrollBar.Height;
            }
            this.incomingScrollArea.invalidate();
            this.incomingScrollBar.invalidate();
            this.backgroundImage.invalidate();
            this.update();
        }

        private void closeClick()
        {
            if (!InterfaceMgr.Instance.isSelectedVillageACapital())
            {
                InterfaceMgr.Instance.setVillageTabSubMode(4);
            }
            else
            {
                InterfaceMgr.Instance.setVillageTabSubMode(0x3ec);
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closePopup()
        {
            if ((this.popup != null) && this.popup.Created)
            {
                this.popup.Close();
                this.popup = null;
            }
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
            int num = 0xfc;
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
            }
            InterfaceMgr.Instance.setFloatingValueSentDelegate(new InterfaceMgr.FloatingValueSent(this.setTrackCB));
            Point point = InterfaceMgr.Instance.ParentForm.PointToScreen(new Point(base.Location.X + 870, base.Location.Y + num));
            FloatingInput.open(point.X, point.Y, this.currentTrack.Value, this.currentTrack.Max, InterfaceMgr.Instance.ParentForm);
        }

        private void incomingWallScrollBarMoved()
        {
            int y = this.incomingScrollBar.Value;
            this.incomingScrollArea.Position = new Point(this.incomingScrollArea.X, (((0x23 + this.blockYSize) + 5) + this.yOffset) - y);
            this.incomingScrollArea.ClipRect = new Rectangle(this.incomingScrollArea.ClipRect.X, y, this.incomingScrollArea.ClipRect.Width, this.incomingScrollArea.ClipRect.Height);
            this.incomingScrollArea.invalidate();
            this.incomingScrollBar.invalidate();
        }

        public void init(bool resized)
        {
            int height = base.Height;
            this.yOffset = 0;
            if (this.m_selectedVillage >= 0)
            {
                this.yOffset = 0x114;
            }
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
            if (this.m_selectedVillage >= 0)
            {
                this.cardbar.Position = new Point(0, 4);
                this.backgroundImage.addControl(this.cardbar);
                this.cardbar.init(6);
            }
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.CreateX((Image) GFXLibrary.mail_top_drag_bar_left, (Image) GFXLibrary.mail_top_drag_bar_middle, (Image) GFXLibrary.mail_top_drag_bar_right, -2, 2);
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            this.parishNameLabel.Text = SK.Text("GENERIC_Reinforcements", "Reinforcements") + " : " + GameEngine.Instance.World.getVillageNameOrType(villageID);
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            if (this.m_selectedVillage >= 0)
            {
                this.villageBackImage.Image = (Image) GFXLibrary.reinforce_back_left;
                this.villageBackImage.Position = new Point(0x69, 0x51);
                this.backgroundImage.addControl(this.villageBackImage);
                this.targetVillageLabel.Text = SK.Text("VillageReinforcementsPanel_Target_Village", "Target Village");
                this.targetVillageLabel.Color = ARGBColors.Black;
                this.targetVillageLabel.Position = new Point(0, 0x16);
                this.targetVillageLabel.Size = new Size(this.villageBackImage.Width, 40);
                this.targetVillageLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.targetVillageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.villageBackImage.addControl(this.targetVillageLabel);
                this.targetVillageName.Text = GameEngine.Instance.World.getVillageName(this.m_selectedVillage);
                this.targetVillageName.Color = ARGBColors.Black;
                this.targetVillageName.Position = new Point(0, 0x4e);
                this.targetVillageName.Size = new Size(this.villageBackImage.Width, 80);
                this.targetVillageName.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.targetVillageName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
                this.villageBackImage.addControl(this.targetVillageName);
                this.targetVillageImage.Image = (Image) GFXLibrary.scout_screen_icons[GameEngine.Instance.World.getVillageSize(this.m_selectedVillage)];
                this.targetVillageImage.Position = new Point(0x30, 0x2a);
                this.villageBackImage.addControl(this.targetVillageImage);
                this.trackBackImage.Image = (Image) GFXLibrary.reinforce_back_right;
                this.trackBackImage.Position = new Point(0x1ab, 80);
                this.backgroundImage.addControl(this.trackBackImage);
                int y = 14;
                this.peasantName.Text = SK.Text("GENERIC_Peasants", "Peasants");
                this.peasantName.Position = new Point(-50, y);
                this.peasantName.Size = new Size(0x8e, 40);
                this.peasantName.Color = ARGBColors.Black;
                this.peasantName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.peasantName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.peasantName);
                this.archerName.Text = SK.Text("GENERIC_Archers", "Archers");
                this.archerName.Position = new Point(-50, y + 40);
                this.archerName.Size = new Size(0x8e, 40);
                this.archerName.Color = ARGBColors.Black;
                this.archerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.archerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.archerName);
                this.pikemanName.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
                this.pikemanName.Position = new Point(-50, y + 80);
                this.pikemanName.Size = new Size(0x8e, 40);
                this.pikemanName.Color = ARGBColors.Black;
                this.pikemanName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.pikemanName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.pikemanName);
                this.swordsmanName.Text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
                this.swordsmanName.Position = new Point(-50, y + 120);
                this.swordsmanName.Size = new Size(0x8e, 40);
                this.swordsmanName.Color = ARGBColors.Black;
                this.swordsmanName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.swordsmanName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.swordsmanName);
                this.peasantStoredValue.Text = "0";
                this.peasantStoredValue.Position = new Point(0x38, y);
                this.peasantStoredValue.Size = new Size(0x8e, 40);
                this.peasantStoredValue.Color = ARGBColors.Black;
                this.peasantStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.peasantStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.peasantStoredValue);
                this.archerStoredValue.Text = "0";
                this.archerStoredValue.Position = new Point(0x38, y + 40);
                this.archerStoredValue.Size = new Size(0x8e, 40);
                this.archerStoredValue.Color = ARGBColors.Black;
                this.archerStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.archerStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.archerStoredValue);
                this.pikemanStoredValue.Text = "0";
                this.pikemanStoredValue.Position = new Point(0x38, y + 80);
                this.pikemanStoredValue.Size = new Size(0x8e, 40);
                this.pikemanStoredValue.Color = ARGBColors.Black;
                this.pikemanStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.pikemanStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.pikemanStoredValue);
                this.swordsmanStoredValue.Text = "0";
                this.swordsmanStoredValue.Position = new Point(0x38, y + 120);
                this.swordsmanStoredValue.Size = new Size(0x8e, 40);
                this.swordsmanStoredValue.Color = ARGBColors.Black;
                this.swordsmanStoredValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.swordsmanStoredValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.swordsmanStoredValue);
                this.peasantSendValue.Text = "0";
                this.peasantSendValue.Position = new Point(0x38, y);
                this.peasantSendValue.Size = new Size(0x192, 40);
                this.peasantSendValue.Color = ARGBColors.Black;
                this.peasantSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.peasantSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.peasantSendValue);
                this.archerSendValue.Text = "0";
                this.archerSendValue.Position = new Point(0x38, y + 40);
                this.archerSendValue.Size = new Size(0x192, 40);
                this.archerSendValue.Color = ARGBColors.Black;
                this.archerSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.archerSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.archerSendValue);
                this.pikemanSendValue.Text = "0";
                this.pikemanSendValue.Position = new Point(0x38, y + 80);
                this.pikemanSendValue.Size = new Size(0x192, 40);
                this.pikemanSendValue.Color = ARGBColors.Black;
                this.pikemanSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.pikemanSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.pikemanSendValue);
                this.swordsmanSendValue.Text = "0";
                this.swordsmanSendValue.Position = new Point(0x38, y + 120);
                this.swordsmanSendValue.Size = new Size(0x192, 40);
                this.swordsmanSendValue.Color = ARGBColors.Black;
                this.swordsmanSendValue.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.swordsmanSendValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.trackBackImage.addControl(this.swordsmanSendValue);
                this.peasantsTrack.Position = new Point(0xcf, 15);
                this.peasantsTrack.Size = new Size(0xcb, 0x17);
                this.peasantsTrack.Max = 100;
                if (!resized)
                {
                    this.peasantsTrack.Value = 0;
                }
                this.peasantsTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
                this.trackBackImage.addControl(this.peasantsTrack);
                this.peasantsTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
                this.peasantsEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
                this.peasantsEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
                this.peasantsEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
                this.peasantsEditButton.MoveOnClick = true;
                this.peasantsEditButton.OverBrighten = true;
                this.peasantsEditButton.Position = new Point(420, 12);
                this.peasantsEditButton.Data = 1;
                this.peasantsEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
                this.trackBackImage.addControl(this.peasantsEditButton);
                this.archerTrack.Position = new Point(0xcf, 0x37);
                this.archerTrack.Size = new Size(0xcb, 0x17);
                this.archerTrack.Max = 100;
                if (!resized)
                {
                    this.archerTrack.Value = 0;
                }
                this.archerTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
                this.trackBackImage.addControl(this.archerTrack);
                this.archerTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
                this.archerEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
                this.archerEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
                this.archerEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
                this.archerEditButton.MoveOnClick = true;
                this.archerEditButton.OverBrighten = true;
                this.archerEditButton.Position = new Point(420, 0x34);
                this.archerEditButton.Data = 2;
                this.archerEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
                this.trackBackImage.addControl(this.archerEditButton);
                this.pikemanTrack.Position = new Point(0xcf, 0x5f);
                this.pikemanTrack.Size = new Size(0xcb, 0x17);
                this.pikemanTrack.Max = 100;
                if (!resized)
                {
                    this.pikemanTrack.Value = 0;
                }
                this.pikemanTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
                this.trackBackImage.addControl(this.pikemanTrack);
                this.pikemanTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
                this.pikemanEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
                this.pikemanEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
                this.pikemanEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
                this.pikemanEditButton.MoveOnClick = true;
                this.pikemanEditButton.OverBrighten = true;
                this.pikemanEditButton.Position = new Point(420, 0x5c);
                this.pikemanEditButton.Data = 3;
                this.pikemanEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
                this.trackBackImage.addControl(this.pikemanEditButton);
                this.swordsmanTrack.Position = new Point(0xcf, 0x87);
                this.swordsmanTrack.Size = new Size(0xcb, 0x17);
                this.swordsmanTrack.Max = 100;
                if (!resized)
                {
                    this.swordsmanTrack.Value = 0;
                }
                this.swordsmanTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
                this.trackBackImage.addControl(this.swordsmanTrack);
                this.swordsmanTrack.Create(null, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
                this.swordsmanEditButton.ImageNorm = (Image) GFXLibrary.faction_pen;
                this.swordsmanEditButton.ImageOver = (Image) GFXLibrary.faction_pen;
                this.swordsmanEditButton.ImageClick = (Image) GFXLibrary.faction_pen;
                this.swordsmanEditButton.MoveOnClick = true;
                this.swordsmanEditButton.OverBrighten = true;
                this.swordsmanEditButton.Position = new Point(420, 0x84);
                this.swordsmanEditButton.Data = 4;
                this.swordsmanEditButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editSendValue), "SendArmyPanel_editValue");
                this.trackBackImage.addControl(this.swordsmanEditButton);
                this.btnSend.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
                this.btnSend.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
                this.btnSend.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
                this.btnSend.Position = new Point(360, 0xa5);
                this.btnSend.Text.Text = SK.Text("VassalArmiesPanel_", "Send");
                this.btnSend.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.btnSend.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.btnSend.TextYOffset = -3;
                this.btnSend.Text.Color = ARGBColors.Black;
                this.btnSend.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendClick), "VillageReinforcementsPanel2_send");
                this.btnSend.Enabled = false;
                this.trackBackImage.addControl(this.btnSend);
                this.updateValues();
            }
            this.blockYSize = (((height - this.yOffset) - 40) - 0x38) / 2;
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 5 + this.yOffset);
            this.backgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(300, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.divider3Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider3Image.Position = new Point(0x229, 0);
            this.headerLabelsImage.addControl(this.divider3Image);
            this.outGoingAttacksLabel.Text = SK.Text("VillageArmiesPanel_Target_Village", "Target Village");
            this.outGoingAttacksLabel.Color = ARGBColors.Black;
            this.outGoingAttacksLabel.Position = new Point(12, -2);
            this.outGoingAttacksLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.outGoingAttacksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.outGoingAttacksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.outGoingAttacksLabel);
            this.outGoingArrivesLabel.Text = SK.Text("AllArmiesPanel_Arrives", "Arrives");
            this.outGoingArrivesLabel.Color = ARGBColors.Black;
            this.outGoingArrivesLabel.Position = new Point(0x22e, -2);
            this.outGoingArrivesLabel.Size = new Size(0x72, this.headerLabelsImage.Height);
            this.outGoingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.outGoingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.outGoingArrivesLabel);
            this.headerLabelsImage2.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage2.Position = new Point(0x19, (this.blockYSize + 5) + this.yOffset);
            this.backgroundImage.addControl(this.headerLabelsImage2);
            this.headerLabelsImage2.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider5Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider5Image.Position = new Point(300, 0);
            this.headerLabelsImage2.addControl(this.divider5Image);
            this.divider6Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider6Image.Position = new Point(0x229, 0);
            this.headerLabelsImage2.addControl(this.divider6Image);
            this.incomingAttacksLabel.Text = SK.Text("VillageArmiesPanel_From", "From") + ":";
            this.incomingAttacksLabel.Color = ARGBColors.Black;
            this.incomingAttacksLabel.Position = new Point(12, -2);
            this.incomingAttacksLabel.Size = new Size(0xe0, this.headerLabelsImage.Height);
            this.incomingAttacksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.incomingAttacksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.incomingAttacksLabel);
            this.incomingArrivesLabel.Text = SK.Text("AllArmiesPanel_Arrives", "Arrives");
            this.incomingArrivesLabel.Color = ARGBColors.Black;
            this.incomingArrivesLabel.Position = new Point(0x22e, -2);
            this.incomingArrivesLabel.Size = new Size(0x72, this.headerLabelsImage.Height);
            this.incomingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.incomingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.incomingArrivesLabel);
            this.outgoingScrollArea.Position = new Point(0x19, 40 + this.yOffset);
            this.outgoingScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.outgoingScrollArea);
            int num4 = this.outgoingScrollBar.Value;
            this.outgoingScrollBar.Position = new Point(0x3af, 40 + this.yOffset);
            this.outgoingScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.outgoingScrollBar);
            this.outgoingScrollBar.Value = 0;
            this.outgoingScrollBar.Max = 100;
            this.outgoingScrollBar.NumVisibleLines = 0x19;
            this.outgoingScrollBar.Create(null, null, null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
            this.outgoingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.incomingScrollArea.Position = new Point(0x19, ((0x23 + this.blockYSize) + 5) + this.yOffset);
            this.incomingScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.incomingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.incomingScrollArea);
            int num5 = this.incomingScrollBar.Value;
            this.incomingScrollBar.Position = new Point(0x3af, ((0x23 + this.blockYSize) + 5) + this.yOffset);
            this.incomingScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.incomingScrollBar);
            this.incomingScrollBar.Value = 0;
            this.incomingScrollBar.Max = 100;
            this.incomingScrollBar.NumVisibleLines = 0x19;
            this.incomingScrollBar.Create(null, null, null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
            this.incomingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
            this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
            this.smallPeasantImage.Position = new Point(0x143, -10);
            this.smallPeasantImage.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Width - 120, this.smallPeasantImage.Height);
            this.headerLabelsImage.addControl(this.smallPeasantImage);
            this.smallPeasantImage2.Image = (Image) GFXLibrary.armies_screen_troops;
            this.smallPeasantImage2.Position = new Point(0x143, -10);
            this.smallPeasantImage2.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Width - 120, this.smallPeasantImage.Height);
            this.headerLabelsImage2.addControl(this.smallPeasantImage2);
            if (!resized)
            {
                SparseArray array = GameEngine.Instance.World.getReinforcementsArray();
                this.reinforcementList.Clear();
                foreach (WorldMap.LocalArmyData data in array)
                {
                    this.reinforcementList.Add(data);
                }
                this.reinforcementList.Sort(this.armyComparer);
            }
            this.addArmies();
            if (resized)
            {
                this.outgoingScrollBar.Value = num4;
                this.incomingScrollBar.Value = num5;
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
            this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "VillageReinforcementsPanel2_close");
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
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "VillageReinforcementsPanel2";
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

        public void resume()
        {
            this.setReinforcementVillage(this.m_selectedVillage);
        }

        private void sendClick()
        {
            int num = ((this.peasantsTrack.Value + this.archerTrack.Value) + this.pikemanTrack.Value) + this.swordsmanTrack.Value;
            if (num > 0)
            {
                VillageMap village = GameEngine.Instance.Village;
                if (village != null)
                {
                    RemoteServices.Instance.set_SendReinforcements_UserCallBack(new RemoteServices.SendReinforcements_UserCallBack(this.sendReinforcementsCallback));
                    RemoteServices.Instance.SendReinforcements(village.VillageID, this.m_selectedVillage, this.peasantsTrack.Value, this.archerTrack.Value, this.pikemanTrack.Value, this.swordsmanTrack.Value, 0);
                }
            }
        }

        public void sendReinforcementsCallback(SendReinforcements_ReturnType returnData)
        {
            if (returnData.Success)
            {
                VillageMap village = GameEngine.Instance.Village;
                if ((village != null) && (village.VillageID == returnData.villageID))
                {
                    village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
                }
                GameEngine.Instance.World.addReinforcementArmy(returnData.armyData);
                this.resume();
            }
        }

        public void setReinforcementVillage(int villageID)
        {
            if (villageID >= 0)
            {
                this.m_selectedVillage = villageID;
            }
            else
            {
                this.m_selectedVillage = -1;
            }
            this.init(false);
        }

        private void setTrackCB(int value)
        {
            if (this.currentTrack != null)
            {
                this.currentTrack.Value = value;
                this.updateSlider();
            }
        }

        public void showPopup(long reinforcementID)
        {
            this.closePopup();
            WorldMap.LocalArmyData data = GameEngine.Instance.World.getReinforcement(reinforcementID);
            if (data != null)
            {
                this.popup = new ReinforcementsRetrievalPopup();
                this.popup.init(this, reinforcementID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults);
                this.popup.Show();
            }
        }

        public void tracksMoved()
        {
            this.updateSlider();
        }

        public void update()
        {
            if (this.m_selectedVillage >= 0)
            {
                this.cardbar.update();
            }
            bool flag = false;
            double localTime = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (ArmyLine line in this.lineList)
            {
                if (!line.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (ArmyLine line2 in this.lineList2)
            {
                if (!line2.update(localTime))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                this.init(false);
            }
        }

        public void updateSlider()
        {
            this.peasantSendValue.Text = this.peasantsTrack.Value.ToString();
            this.archerSendValue.Text = this.archerTrack.Value.ToString();
            this.pikemanSendValue.Text = this.pikemanTrack.Value.ToString();
            this.swordsmanSendValue.Text = this.swordsmanTrack.Value.ToString();
            int num = ((this.peasantsTrack.Value + this.archerTrack.Value) + this.pikemanTrack.Value) + this.swordsmanTrack.Value;
            if (num > 0)
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
                this.peasantsTrack.Max = village.m_numPeasants;
                this.archerTrack.Max = village.m_numArchers;
                this.pikemanTrack.Max = village.m_numPikemen;
                this.swordsmanTrack.Max = village.m_numSwordsmen;
                this.updateSlider();
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.outgoingScrollBar.Value;
            this.outgoingScrollArea.Position = new Point(this.outgoingScrollArea.X, (40 + this.yOffset) - y);
            this.outgoingScrollArea.ClipRect = new Rectangle(this.outgoingScrollArea.ClipRect.X, y, this.outgoingScrollArea.ClipRect.Width, this.outgoingScrollArea.ClipRect.Height);
            this.outgoingScrollArea.invalidate();
            this.outgoingScrollBar.invalidate();
        }

        public class ArmyComparer : IComparer<WorldMap.LocalArmyData>
        {
            public int Compare(WorldMap.LocalArmyData x, WorldMap.LocalArmyData y)
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
                if (x.armyID > y.armyID)
                {
                    return 1;
                }
                if (x.armyID < y.armyID)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class ArmyLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton btnCancel = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblReturning = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private int leftVillageID = -1;
            private DateTime m_arrivalTime = DateTime.Now;
            private bool m_moving;
            private VillageReinforcementsPanel2 m_parent;
            private int m_position = -1000;
            private WorldMap.LocalArmyData m_reinforcement;
            private long m_reinforcementID = -1L;
            private bool m_returning;
            private bool m_sent;
            private int m_villageID = -1;

            private void cancelClick()
            {
                if (this.m_sent)
                {
                    if (this.m_parent != null)
                    {
                        RemoteServices.Instance.set_ReturnReinforcements_UserCallBack(new RemoteServices.ReturnReinforcements_UserCallBack(this.returnReinforcementsCallBack));
                        this.m_parent.showPopup(this.m_reinforcementID);
                    }
                }
                else
                {
                    RemoteServices.Instance.set_ReturnReinforcements_UserCallBack(new RemoteServices.ReturnReinforcements_UserCallBack(this.returnReinforcementsCallBack));
                    RemoteServices.Instance.ReturnReinforcements(this.m_reinforcementID);
                }
            }

            public void initIncoming(int position, int villageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, VillageReinforcementsPanel2 parent, bool returning)
            {
                this.m_sent = false;
                this.initText(position, villageID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, 0);
            }

            public void initSent(int position, int villageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, VillageReinforcementsPanel2 parent, bool returning)
            {
                this.m_sent = true;
                this.initText(position, villageID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, 0);
            }

            private void initText(int position, int villageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, VillageReinforcementsPanel2 parent, bool returning, bool showTroops, bool tutorial, int attackType)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_reinforcement = GameEngine.Instance.World.getReinforcement(armyID);
                this.m_reinforcementID = armyID;
                this.m_villageID = villageID;
                this.m_arrivalTime = arrivalTime;
                this.m_returning = returning;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.brown_lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.brown_lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(0, 0);
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
                this.lblVillage.Color = ARGBColors.Black;
                this.lblVillage.RolloverColor = ARGBColors.White;
                this.lblVillage.Position = new Point(9, 0);
                this.lblVillage.Size = new Size(0xdf, this.backgroundImage.Height);
                this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageReinforcementsPanel2_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblReturning.Text = SK.Text("VillageArmySentLine_Returning", "Returning");
                this.lblReturning.Color = ARGBColors.Black;
                this.lblReturning.Position = new Point(0x335, 0);
                this.lblReturning.Size = new Size(110, this.backgroundImage.Height);
                this.lblReturning.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblReturning.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblReturning);
                DateTime time = VillageMap.getCurrentServerTime();
                if (this.m_arrivalTime > time)
                {
                    this.m_moving = true;
                    if (!this.m_sent)
                    {
                        showButton = false;
                    }
                }
                else
                {
                    this.m_moving = false;
                }
                if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    showButton = false;
                }
                this.lblReturning.Visible = !showButton;
                if (!showButton)
                {
                    if (this.m_returning)
                    {
                        this.lblReturning.Text = SK.Text("VillageArmySentLine_Returning", "Returning");
                    }
                    else
                    {
                        this.lblReturning.Text = "";
                    }
                }
                this.leftVillageID = villageID;
                this.lblArrivalTime.Text = "";
                this.lblArrivalTime.Color = ARGBColors.Black;
                this.lblArrivalTime.Position = new Point(0x22e, 0);
                this.lblArrivalTime.Size = new Size(0xae, this.backgroundImage.Height);
                this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblArrivalTime);
                this.lblPeasants.Text = numPeasants.ToString();
                this.lblPeasants.Color = ARGBColors.Black;
                this.lblPeasants.Position = new Point(0x131, 0);
                this.lblPeasants.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblPeasants);
                this.lblArchers.Text = numArchers.ToString();
                this.lblArchers.Color = ARGBColors.Black;
                this.lblArchers.Position = new Point(0x16d, 0);
                this.lblArchers.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblArchers);
                this.lblPikemen.Text = numPikemen.ToString();
                this.lblPikemen.Color = ARGBColors.Black;
                this.lblPikemen.Position = new Point(0x1a9, 0);
                this.lblPikemen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblPikemen);
                this.lblSwordsmen.Text = numSwordsmen.ToString();
                this.lblSwordsmen.Color = ARGBColors.Black;
                this.lblSwordsmen.Position = new Point(0x1e5, 0);
                this.lblSwordsmen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblSwordsmen);
                if (showButton)
                {
                    this.btnCancel.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
                    this.btnCancel.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
                    this.btnCancel.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
                    this.btnCancel.Position = new Point(760, 3);
                    if (this.m_sent)
                    {
                        this.btnCancel.Text.Text = SK.Text("VillageReinforcementSentLine_Retrieve", "Retrieve");
                    }
                    else
                    {
                        this.btnCancel.Text.Text = SK.Text("VillageReinforcementSentLine_Return", "Return");
                    }
                    this.btnCancel.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.btnCancel.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.btnCancel.TextYOffset = -3;
                    this.btnCancel.Text.Color = ARGBColors.Black;
                    this.btnCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "VillageReinforcementsPanel2_cancel");
                    this.backgroundImage.addControl(this.btnCancel);
                }
                this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
                base.invalidate();
            }

            private void lblVillage_Click()
            {
                if (this.leftVillageID >= 0)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(this.leftVillageID);
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.leftVillageID, false, true, true, false);
                }
            }

            private void returnReinforcementsCallBack(ReturnReinforcements_ReturnType returnData)
            {
                if (returnData.Success)
                {
                    if (returnData.armyData != null)
                    {
                        GameEngine.Instance.World.addReinforcementArmy(returnData.armyData);
                    }
                    if (returnData.armyData2 != null)
                    {
                        GameEngine.Instance.World.addReinforcementArmy(returnData.armyData2);
                    }
                    if (this.m_parent != null)
                    {
                        this.m_parent.init(false);
                    }
                }
            }

            public bool update(double localTime)
            {
                DateTime time = VillageMap.getCurrentServerTime();
                if (this.m_arrivalTime.AddSeconds(5.0) < time)
                {
                    this.lblArrivalTime.Text = "";
                    if (this.m_returning && this.m_moving)
                    {
                        this.m_moving = false;
                        return false;
                    }
                }
                else
                {
                    TimeSpan span = (TimeSpan) (this.m_arrivalTime - time);
                    int secsLeft = (int) (span.TotalSeconds + 0.5);
                    if (secsLeft < 1)
                    {
                        secsLeft = 0;
                    }
                    this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
                }
                return true;
            }
        }
    }
}

