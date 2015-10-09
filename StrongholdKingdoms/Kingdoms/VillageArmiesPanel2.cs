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

    public class VillageArmiesPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private ArmyComparer armyComparer = new ArmyComparer();
        private List<WorldMap.LocalArmyData> armyList = new List<WorldMap.LocalArmyData>();
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel diplomacyHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel diplomacyTextLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel incomingArrivesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel incomingAttackingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel incomingAttacksLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea incomingScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar incomingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        public static VillageArmiesPanel2 instance;
        private List<ArmyLine> lineList = new List<ArmyLine>();
        private List<ArmyLine> lineList2 = new List<ArmyLine>();
        private CustomSelfDrawPanel.CSDLabel outGoingArrivesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel outGoingAttacksLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();

        public VillageArmiesPanel2()
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
            long highestArmyIDSeen = -1L;
            int position = 0;
            int num6 = 0;
            double num7 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (WorldMap.LocalArmyData data in this.armyList)
            {
                if (data.travelFromVillageID == num3)
                {
                    ArmyLine control = new ArmyLine();
                    if (y != 0)
                    {
                        y += 5;
                    }
                    control.Position = new Point(0, y);
                    bool showButton = data.lootType < 0;
                    if (data.localEndTime == 0.0)
                    {
                        showButton = false;
                    }
                    else
                    {
                        double localEndTime = data.localEndTime;
                        if ((data.localStartTime + (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60)) < num7)
                        {
                            showButton = false;
                        }
                    }
                    control.initSent(position, data.targetVillageID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults, data.numScouts, data.serverEndTime, data.armyID, showButton, this, data.lootType >= 0);
                    this.outgoingScrollArea.addControl(control);
                    y += control.Height;
                    this.lineList.Add(control);
                    position++;
                }
                if ((data.targetVillageID == num3) && (data.lootType < 0))
                {
                    ArmyLine line2 = new ArmyLine();
                    if (num2 != 0)
                    {
                        num2 += 5;
                    }
                    if (data.armyID > highestArmyIDSeen)
                    {
                        highestArmyIDSeen = data.armyID;
                    }
                    line2.Position = new Point(0, num2);
                    bool tutorial = false;
                    if (data.attackType == 13)
                    {
                        tutorial = true;
                    }
                    line2.initIncoming(num6, data.travelFromVillageID, 0, 0, 0, 0, 0, 0, data.serverEndTime, data.armyID, false, this, false, tutorial, data.attackType);
                    this.incomingScrollArea.addControl(line2);
                    num2 += line2.Height;
                    this.lineList2.Add(line2);
                    num6++;
                }
            }
            if (highestArmyIDSeen > GameEngine.Instance.World.HighestArmyIDSeen)
            {
                GameEngine.Instance.World.HighestArmyIDSeen = highestArmyIDSeen;
                RemoteServices.Instance.SetHighestArmySeen(highestArmyIDSeen);
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
            InterfaceMgr.Instance.setVillageTabSubMode(4);
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

        private void incomingWallScrollBarMoved()
        {
            int y = this.incomingScrollBar.Value;
            this.incomingScrollArea.Position = new Point(this.incomingScrollArea.X, ((0x23 + this.blockYSize) + 5) - y);
            this.incomingScrollArea.ClipRect = new Rectangle(this.incomingScrollArea.ClipRect.X, y, this.incomingScrollArea.ClipRect.Width, this.incomingScrollArea.ClipRect.Height);
            this.incomingScrollArea.invalidate();
            this.incomingScrollBar.invalidate();
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
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Attacks", "Attacks") + " : " + GameEngine.Instance.World.getVillageNameOrType(villageID);
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.blockYSize = ((height - 40) - 0x38) / 2;
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 5);
            this.backgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(300, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.divider3Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider3Image.Position = new Point(0x2a6, 0);
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
            this.outGoingArrivesLabel.Position = new Point(0x2ab, -2);
            this.outGoingArrivesLabel.Size = new Size(0x72, this.headerLabelsImage.Height);
            this.outGoingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.outGoingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.outGoingArrivesLabel);
            this.headerLabelsImage2.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage2.Position = new Point(0x19, this.blockYSize + 5);
            this.backgroundImage.addControl(this.headerLabelsImage2);
            this.headerLabelsImage2.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider5Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider5Image.Position = new Point(300, 0);
            this.headerLabelsImage2.addControl(this.divider5Image);
            this.divider6Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider6Image.Position = new Point(0x2a6, 0);
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
            this.incomingArrivesLabel.Position = new Point(0x2ab, -2);
            this.incomingArrivesLabel.Size = new Size(0x72, this.headerLabelsImage.Height);
            this.incomingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.incomingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.incomingArrivesLabel);
            this.outgoingScrollArea.Position = new Point(0x19, 40);
            this.outgoingScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.outgoingScrollArea);
            int num3 = this.outgoingScrollBar.Value;
            this.outgoingScrollBar.Position = new Point(0x3af, 40);
            this.outgoingScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.outgoingScrollBar);
            this.outgoingScrollBar.Value = 0;
            this.outgoingScrollBar.Max = 100;
            this.outgoingScrollBar.NumVisibleLines = 0x19;
            this.outgoingScrollBar.Create(null, null, null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
            this.outgoingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.incomingScrollArea.Position = new Point(0x19, (0x23 + this.blockYSize) + 5);
            this.incomingScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.incomingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.incomingScrollArea);
            int num4 = this.incomingScrollBar.Value;
            this.incomingScrollBar.Position = new Point(0x3af, (0x23 + this.blockYSize) + 5);
            this.incomingScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.incomingScrollBar);
            this.incomingScrollBar.Value = 0;
            this.incomingScrollBar.Max = 100;
            this.incomingScrollBar.NumVisibleLines = 0x19;
            this.incomingScrollBar.Create(null, null, null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
            this.incomingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
            this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
            this.smallPeasantImage.Position = new Point(0x143, -10);
            this.headerLabelsImage.addControl(this.smallPeasantImage);
            if (!resized)
            {
                SparseArray array = GameEngine.Instance.World.getArmyArray();
                this.armyList.Clear();
                foreach (WorldMap.LocalArmyData data in array)
                {
                    this.armyList.Add(data);
                }
                this.armyList.Sort(this.armyComparer);
            }
            this.addArmies();
            if (resized)
            {
                this.outgoingScrollBar.Value = num3;
                this.incomingScrollBar.Value = num4;
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
            this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "VillageArmiesPanel2_close");
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
            base.Name = "VillageArmiesPanel2";
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

        public void update()
        {
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

        private void wallScrollBarMoved()
        {
            int y = this.outgoingScrollBar.Value;
            this.outgoingScrollArea.Position = new Point(this.outgoingScrollArea.X, 40 - y);
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
            private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private int leftVillageID = -1;
            private WorldMap.LocalArmyData m_army;
            private long m_armyID = -1L;
            private DateTime m_arrivalTime = DateTime.Now;
            private int m_origLoot = -1;
            private VillageArmiesPanel2 m_parent;
            private int m_position = -1000;
            private bool m_returning;
            private int m_villageID = -1;

            private void cancelCastleAttackCallBack(CancelCastleAttack_ReturnType returnData)
            {
                if (returnData.Success)
                {
                    if (returnData.armyData != null)
                    {
                        ArmyReturnData[] armyReturnData = new ArmyReturnData[] { returnData.armyData };
                        GameEngine.Instance.World.doGetArmyData(armyReturnData, null, false);
                        GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
                        GameEngine.Instance.World.deleteArmy(returnData.oldArmyID);
                        if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(returnData.armyData.targetVillageID)))
                        {
                            GameEngine.Instance.World.setLastTreasureCastleAttackTime(DateTime.MinValue);
                        }
                    }
                    this.btnCancel.Visible = false;
                    if (this.m_parent != null)
                    {
                        this.m_parent.init(false);
                    }
                }
            }

            private void cancelClick()
            {
                if (this.m_army != null)
                {
                    double num = DXTimer.GetCurrentMilliseconds() / 1000.0;
                    double localEndTime = this.m_army.localEndTime;
                    if (((this.m_army.localStartTime + (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60)) >= num) && (this.m_army.lootType < 0))
                    {
                        RemoteServices.Instance.set_CancelCastleAttack_UserCallBack(new RemoteServices.CancelCastleAttack_UserCallBack(this.cancelCastleAttackCallBack));
                        RemoteServices.Instance.CancelCastleAttack(this.m_army.armyID);
                        this.btnCancel.Visible = false;
                    }
                    else
                    {
                        this.btnCancel.Visible = false;
                        if (this.m_parent != null)
                        {
                            this.m_parent.init(false);
                        }
                    }
                }
            }

            public void initIncoming(int position, int villageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, VillageArmiesPanel2 parent, bool returning, bool tutorial, int attackType)
            {
                this.initText(position, villageID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, false, tutorial, attackType);
            }

            public void initSent(int position, int villageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, VillageArmiesPanel2 parent, bool returning)
            {
                this.initText(position, villageID, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, 0);
            }

            private void initText(int position, int villageID, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, VillageArmiesPanel2 parent, bool returning, bool showTroops, bool tutorial, int attackType)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_army = GameEngine.Instance.World.getArmy(armyID);
                this.m_origLoot = this.m_army.lootType;
                this.m_armyID = armyID;
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
                if (!tutorial)
                {
                    this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
                }
                else
                {
                    this.lblVillage.Text = SK.Text("GENERIC_TUTORIAL", "Tutorial");
                }
                this.lblVillage.Color = ARGBColors.Black;
                this.lblVillage.RolloverColor = ARGBColors.White;
                this.lblVillage.Position = new Point(9, 0);
                this.lblVillage.Size = new Size(0xdf, this.backgroundImage.Height);
                this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "VillageArmiesPanel2_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblReturning.Text = SK.Text("VillageArmySentLine_Returning", "Returning");
                this.lblReturning.Color = ARGBColors.Black;
                this.lblReturning.Position = new Point(0x335, 0);
                this.lblReturning.Size = new Size(110, this.backgroundImage.Height);
                this.lblReturning.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblReturning.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblReturning);
                this.lblReturning.Visible = !showButton;
                if (!showButton)
                {
                    if (this.m_returning)
                    {
                        this.lblReturning.Text = SK.Text("VillageArmySentLine_Returning", "Returning");
                    }
                    else
                    {
                        this.lblReturning.Text = SK.Text("GENERIC_Attacking", "Attacking");
                    }
                }
                this.leftVillageID = villageID;
                this.lblArrivalTime.Text = "";
                this.lblArrivalTime.Color = ARGBColors.Black;
                this.lblArrivalTime.Position = new Point(0x2ab, 0);
                this.lblArrivalTime.Size = new Size(0x72, this.backgroundImage.Height);
                this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblArrivalTime);
                if (showTroops)
                {
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
                    this.lblCatapults.Text = numCatapults.ToString();
                    this.lblCatapults.Color = ARGBColors.Black;
                    this.lblCatapults.Position = new Point(0x221, 0);
                    this.lblCatapults.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblCatapults);
                    this.lblScouts.Text = numScouts.ToString();
                    this.lblScouts.Color = ARGBColors.Black;
                    this.lblScouts.Position = new Point(0x25d, 0);
                    this.lblScouts.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblScouts.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblScouts);
                }
                if (attackType == 30)
                {
                    this.lblPeasants.Text = SK.Text("AllArmiesSentLine_Vassal_Support", "Vassal Support");
                    this.lblPeasants.Color = ARGBColors.Black;
                    this.lblPeasants.Position = new Point(0x131, 0);
                    this.lblPeasants.Size = new Size(250, this.backgroundImage.Height);
                    this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.backgroundImage.addControl(this.lblPeasants);
                }
                if (attackType == 0x1f)
                {
                    this.lblPeasants.Text = SK.Text("AllArmiesSentLine_Capital_Support", "Capital Support");
                    this.lblPeasants.Color = ARGBColors.Black;
                    this.lblPeasants.Position = new Point(0x131, 0);
                    this.lblPeasants.Size = new Size(250, this.backgroundImage.Height);
                    this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.backgroundImage.addControl(this.lblPeasants);
                }
                if (showButton)
                {
                    this.btnCancel.ImageNorm = (Image) GFXLibrary.brown_mail2_button_blue_141wide_normal;
                    this.btnCancel.ImageOver = (Image) GFXLibrary.brown_mail2_button_blue_141wide_over;
                    this.btnCancel.ImageClick = (Image) GFXLibrary.brown_mail2_button_blue_141wide_pushed;
                    this.btnCancel.Position = new Point(760, 3);
                    this.btnCancel.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                    this.btnCancel.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.btnCancel.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.btnCancel.TextYOffset = -3;
                    this.btnCancel.Text.Color = ARGBColors.Black;
                    this.btnCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick), "VillageArmiesPanel2_cancel");
                    this.backgroundImage.addControl(this.btnCancel);
                }
                if (!this.update(DXTimer.GetCurrentMilliseconds() / 1000.0))
                {
                    this.btnCancel.Visible = false;
                }
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

            public bool update(double localTime)
            {
                if (this.btnCancel.Visible)
                {
                    if (this.m_army.localEndTime == 0.0)
                    {
                        return false;
                    }
                    if ((this.m_army.localStartTime + (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60)) < localTime)
                    {
                        return false;
                    }
                }
                else
                {
                    WorldMap.LocalArmyData data = GameEngine.Instance.World.getArmy(this.m_armyID);
                    if ((data == null) || (data.lootType != this.m_origLoot))
                    {
                        return false;
                    }
                }
                DateTime time = VillageMap.getCurrentServerTime();
                TimeSpan span = (TimeSpan) (this.m_arrivalTime - time);
                int secsLeft = (int) (span.TotalSeconds + 0.5);
                if (secsLeft < 1)
                {
                    secsLeft = 0;
                }
                this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
                return true;
            }
        }
    }
}

