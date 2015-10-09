namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class AllArmiesPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private ArmyComparer armyComparer = new ArmyComparer();
        private List<ArmyLine> armyLineList = new List<ArmyLine>();
        private List<ArmyLine> armyLineList2 = new List<ArmyLine>();
        private List<WorldMap.LocalArmyData> armyList = new List<WorldMap.LocalArmyData>();
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundLeftEdge = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private CustomSelfDrawPanel.CSDButton btnDiplomacy = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel diplomacyHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel diplomacyTextLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider7Image = new CustomSelfDrawPanel.CSDImage();
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
        public static AllArmiesPanel2 instance = null;
        private DateTime lastfullUpdate = DateTime.MinValue;
        private MerchantComparer merchantComparer = new MerchantComparer();
        private List<MerchantLine> merchantLineList = new List<MerchantLine>();
        private List<MerchantLine> merchantLineList2 = new List<MerchantLine>();
        private List<WorldMap.LocalTrader> merchantList = new List<WorldMap.LocalTrader>();
        private static int mode = 0;
        private CustomSelfDrawPanel.CSDButton monkButton = new CustomSelfDrawPanel.CSDButton();
        private MonkComparer monkComparer = new MonkComparer();
        private List<MonkLine> monkLineList = new List<MonkLine>();
        private List<MonkLine> monkLineList2 = new List<MonkLine>();
        private List<WorldMap.LocalPerson> monkList = new List<WorldMap.LocalPerson>();
        public static bool MonksUpdated = false;
        private CustomSelfDrawPanel.CSDLabel outGoingArrivesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel outGoingAttacksLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel outGoingCarryingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel outGoingFromLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDLabel outGoingStatusLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private List<ReinforcementLine> reinforcementLineList = new List<ReinforcementLine>();
        private List<ReinforcementLine> reinforcementLineList2 = new List<ReinforcementLine>();
        private List<WorldMap.LocalArmyData> reinforcementList = new List<WorldMap.LocalArmyData>();
        private CustomSelfDrawPanel.CSDButton reinforcementsButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
        private List<ScoutLine> scoutLineList = new List<ScoutLine>();
        private List<ScoutLine> scoutLineList2 = new List<ScoutLine>();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage smallPeasantImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea sortArea1 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea1a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea2 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea2a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea3 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea3a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea4 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea4a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea5 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea5a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea6 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea6a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea7 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea7a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea8 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea8a = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea9 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea sortArea9a = new CustomSelfDrawPanel.CSDArea();
        public static int sortMode = 8;
        private CustomSelfDrawPanel.CSDButton tradeButton = new CustomSelfDrawPanel.CSDButton();
        public static bool TradersUpdated = false;

        public AllArmiesPanel2()
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
            this.armyLineList.Clear();
            this.armyLineList2.Clear();
            this.merchantLineList.Clear();
            this.merchantLineList2.Clear();
            this.monkLineList.Clear();
            this.monkLineList2.Clear();
            this.scoutLineList.Clear();
            this.scoutLineList2.Clear();
            this.reinforcementLineList.Clear();
            this.reinforcementLineList2.Clear();
            long highestArmyIDSeen = -1L;
            int position = 0;
            int num5 = 0;
            double num6 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (WorldMap.LocalArmyData data in this.armyList)
            {
                if (data.numScouts <= 0)
                {
                    if ((GameEngine.Instance.World.isUserVillage(data.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(data.homeVillageID)) && (data.attackType != 13))
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
                            if ((data.localStartTime + (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60)) < num6)
                            {
                                showButton = false;
                            }
                        }
                        control.initSent(position, data.targetVillageID, data.travelFromVillageID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults, data.numScouts, data.serverEndTime, data.armyID, showButton, this, data.lootType >= 0, data.attackType, data.numCaptains);
                        this.outgoingScrollArea.addControl(control);
                        y += control.Height;
                        this.armyLineList.Add(control);
                        position++;
                    }
                    else if (GameEngine.Instance.World.isUserVillage(data.targetVillageID) && (data.lootType < 0))
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
                        line2.initIncoming(num5, data.travelFromVillageID, data.targetVillageID, 0, 0, 0, 0, 0, 0, data.serverEndTime, data.armyID, false, this, false, tutorial, data.attackType, data.numCaptains);
                        this.incomingScrollArea.addControl(line2);
                        num2 += line2.Height;
                        this.armyLineList2.Add(line2);
                        num5++;
                    }
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

        public void addMerchants()
        {
            this.outgoingScrollArea.clearControls();
            this.incomingScrollArea.clearControls();
            int y = 0;
            int num2 = 0;
            this.armyLineList.Clear();
            this.armyLineList2.Clear();
            this.merchantLineList.Clear();
            this.merchantLineList2.Clear();
            this.monkLineList.Clear();
            this.monkLineList2.Clear();
            this.scoutLineList.Clear();
            this.scoutLineList2.Clear();
            this.reinforcementLineList.Clear();
            this.reinforcementLineList2.Clear();
            long highestArmyIDSeen = -1L;
            int position = 0;
            int num5 = 0;
            double num1 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (WorldMap.LocalTrader trader in this.merchantList)
            {
                if (trader.trader.traderState != 0)
                {
                    if (GameEngine.Instance.World.isUserVillage(trader.trader.homeVillageID))
                    {
                        TimeSpan span = (TimeSpan) (trader.serverEndTime - VillageMap.getCurrentServerTime());
                        if (((span.TotalSeconds > 0.0) || (trader.trader.traderState == 0)) || (((trader.trader.traderState == 1) || (trader.trader.traderState == 3)) || (trader.trader.traderState == 5)))
                        {
                            MerchantLine control = new MerchantLine();
                            if (y != 0)
                            {
                                y += 5;
                            }
                            control.Position = new Point(0, y);
                            control.initSent(position, trader.trader.targetVillageID, trader.trader.homeVillageID, trader.trader.numTraders, trader.serverEndTime, trader.traderID, this, (((trader.trader.traderState != 0) && (trader.trader.traderState != 1)) && (trader.trader.traderState != 3)) && (trader.trader.traderState != 5), trader.trader.traderState, trader.trader.resource, trader.trader.amount);
                            this.outgoingScrollArea.addControl(control);
                            y += control.Height;
                            this.merchantLineList.Add(control);
                            position++;
                        }
                    }
                    if ((GameEngine.Instance.World.isUserVillage(trader.trader.targetVillageID) && !GameEngine.Instance.World.isCapital(trader.trader.targetVillageID)) && (((trader.trader.traderState == 0) || (trader.trader.traderState == 1)) || ((trader.trader.traderState == 3) || (trader.trader.traderState == 5))))
                    {
                        TimeSpan span2 = (TimeSpan) (trader.serverEndTime - VillageMap.getCurrentServerTime());
                        if (span2.TotalSeconds > 0.0)
                        {
                            MerchantLine line2 = new MerchantLine();
                            if (num2 != 0)
                            {
                                num2 += 5;
                            }
                            line2.Position = new Point(0, num2);
                            line2.initIncoming(num5, trader.trader.homeVillageID, trader.trader.targetVillageID, 0, trader.serverEndTime, trader.traderID, this, false);
                            this.incomingScrollArea.addControl(line2);
                            num2 += line2.Height;
                            this.merchantLineList2.Add(line2);
                            num5++;
                        }
                    }
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

        public void addMonks()
        {
            this.outgoingScrollArea.clearControls();
            this.incomingScrollArea.clearControls();
            int y = 0;
            int num2 = 0;
            this.armyLineList.Clear();
            this.armyLineList2.Clear();
            this.merchantLineList.Clear();
            this.merchantLineList2.Clear();
            this.monkLineList.Clear();
            this.monkLineList2.Clear();
            this.scoutLineList.Clear();
            this.scoutLineList2.Clear();
            this.reinforcementLineList.Clear();
            this.reinforcementLineList2.Clear();
            long highestArmyIDSeen = -1L;
            int position = 0;
            int num5 = 0;
            double num1 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (WorldMap.LocalPerson person in this.monkList)
            {
                if (((person.person.personType == 4) && (person.person.state > 0)) && (person.parentPerson < 0L))
                {
                    if (GameEngine.Instance.World.isUserVillage(person.person.homeVillageID))
                    {
                        TimeSpan span = (TimeSpan) (person.serverEndTime - VillageMap.getCurrentServerTime());
                        if (span.TotalSeconds > 1.0)
                        {
                            MonkLine control = new MonkLine();
                            if (y != 0)
                            {
                                y += 5;
                            }
                            control.Position = new Point(0, y);
                            control.initSent(position, person.person.targetVillageID, person.person.homeVillageID, person.childrenCount + 1, person.serverEndTime, person.personID, this, person.person.command);
                            this.outgoingScrollArea.addControl(control);
                            y += control.Height;
                            this.monkLineList.Add(control);
                            position++;
                        }
                    }
                    if (GameEngine.Instance.World.isUserVillage(person.person.targetVillageID) && !GameEngine.Instance.World.isCapital(person.person.targetVillageID))
                    {
                        TimeSpan span2 = (TimeSpan) (person.serverEndTime - VillageMap.getCurrentServerTime());
                        if (span2.TotalSeconds > 1.0)
                        {
                            MonkLine line2 = new MonkLine();
                            if (num2 != 0)
                            {
                                num2 += 5;
                            }
                            line2.Position = new Point(0, num2);
                            line2.initIncoming(num5, person.person.homeVillageID, person.person.targetVillageID, 0, person.serverEndTime, person.personID, this);
                            this.incomingScrollArea.addControl(line2);
                            num2 += line2.Height;
                            this.monkLineList2.Add(line2);
                            num5++;
                        }
                    }
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

        public void addReinforcements()
        {
            this.outgoingScrollArea.clearControls();
            this.incomingScrollArea.clearControls();
            int y = 0;
            int num2 = 0;
            this.armyLineList.Clear();
            this.armyLineList2.Clear();
            this.merchantLineList.Clear();
            this.merchantLineList2.Clear();
            this.monkLineList.Clear();
            this.monkLineList2.Clear();
            this.scoutLineList.Clear();
            this.scoutLineList2.Clear();
            this.reinforcementLineList.Clear();
            this.reinforcementLineList2.Clear();
            long highestArmyIDSeen = -1L;
            int position = 0;
            int num5 = 0;
            double num1 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (WorldMap.LocalArmyData data in this.reinforcementList)
            {
                if ((data.attackType != 0x15) || (data.serverEndTime >= VillageMap.getCurrentServerTime()))
                {
                    if (GameEngine.Instance.World.isUserVillage(data.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(data.homeVillageID))
                    {
                        ReinforcementLine control = new ReinforcementLine();
                        if (y != 0)
                        {
                            y += 5;
                        }
                        control.Position = new Point(0, y);
                        control.initSent(position, data.targetVillageID, data.travelFromVillageID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults, data.numScouts, data.serverEndTime, data.armyID, data.attackType == 20, this, data.attackType == 0x15);
                        this.outgoingScrollArea.addControl(control);
                        y += control.Height;
                        this.reinforcementLineList.Add(control);
                        position++;
                    }
                    else if (GameEngine.Instance.World.isUserVillage(data.targetVillageID))
                    {
                        ReinforcementLine line2 = new ReinforcementLine();
                        if (num2 != 0)
                        {
                            num2 += 5;
                        }
                        line2.Position = new Point(0, num2);
                        bool tutorial = false;
                        line2.initIncoming(num5, data.travelFromVillageID, data.targetVillageID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults, data.numScouts, data.serverEndTime, data.armyID, false, this, data.attackType == 0x15, tutorial, 0);
                        this.incomingScrollArea.addControl(line2);
                        num2 += line2.Height;
                        this.reinforcementLineList2.Add(line2);
                        num5++;
                    }
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

        public void addScouts()
        {
            this.outgoingScrollArea.clearControls();
            this.incomingScrollArea.clearControls();
            int y = 0;
            int num2 = 0;
            this.armyLineList.Clear();
            this.armyLineList2.Clear();
            this.merchantLineList.Clear();
            this.merchantLineList2.Clear();
            this.monkLineList.Clear();
            this.monkLineList2.Clear();
            this.scoutLineList.Clear();
            this.scoutLineList2.Clear();
            this.reinforcementLineList.Clear();
            this.reinforcementLineList2.Clear();
            long highestArmyIDSeen = -1L;
            int position = 0;
            int num5 = 0;
            double num6 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (WorldMap.LocalArmyData data in this.armyList)
            {
                if (data.numScouts != 0)
                {
                    if (GameEngine.Instance.World.isUserVillage(data.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(data.homeVillageID))
                    {
                        ScoutLine control = new ScoutLine();
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
                            if ((data.localStartTime + (GameEngine.Instance.LocalWorldData.AttackCancelDuration * 60)) < num6)
                            {
                                showButton = false;
                            }
                        }
                        control.initSent(position, data.targetVillageID, data.travelFromVillageID, data.numPeasants, data.numArchers, data.numPikemen, data.numSwordsmen, data.numCatapults, data.numScouts, data.serverEndTime, data.armyID, showButton, this, data.lootType >= 0);
                        this.outgoingScrollArea.addControl(control);
                        y += control.Height;
                        this.scoutLineList.Add(control);
                        position++;
                    }
                    else if (GameEngine.Instance.World.isUserVillage(data.targetVillageID) && (data.lootType < 0))
                    {
                        ScoutLine line2 = new ScoutLine();
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
                        line2.initIncoming(num5, data.travelFromVillageID, data.targetVillageID, 0, 0, 0, 0, 0, 0, data.serverEndTime, data.armyID, false, this, false, tutorial, data.attackType);
                        this.incomingScrollArea.addControl(line2);
                        num2 += line2.Height;
                        this.scoutLineList2.Add(line2);
                        num5++;
                    }
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

        private void diplomacyClick()
        {
            bool state = true;
            if (RemoteServices.Instance.ReportFilters.diplomacyActive)
            {
                state = false;
            }
            else
            {
                state = true;
            }
            this.btnDiplomacy.Enabled = false;
            RemoteServices.Instance.ReportFilters.diplomacyActive = state;
            RemoteServices.Instance.set_UpdateDiplomacyStatus_UserCallBack(new RemoteServices.UpdateDiplomacyStatus_UserCallBack(this.UpdateDiplomacyStatusCallBack));
            RemoteServices.Instance.UpdateDiplomacyStatus(state);
            this.updateDiplomacyStatus();
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

        public void init(bool resized, int newMode)
        {
            if (newMode >= 0)
            {
                mode = newMode;
            }
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
            if (mode == 0)
            {
                this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Attacks", "Attacks");
            }
            else if (mode == 1)
            {
                this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Merchants", "Merchants");
            }
            else if (mode == 2)
            {
                this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Monks", "Monks");
            }
            else if (mode == 3)
            {
                this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Scouts", "Scouts");
            }
            else if (mode == 4)
            {
                this.parishNameLabel.Text = SK.Text("AllArmiesPanel_Reinforcements", "Reinforcements");
            }
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            CustomSelfDrawPanel.WikiLinkControl.init(this.headerImage, 20, new Point(base.Width - 0x2c, 3));
            this.attackButton.Position = new Point(0x266, 4);
            if (mode == 0)
            {
                this.attackButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[0];
                this.attackButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[0];
                this.attackButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[0];
                this.attackButton.CustomTooltipID = 0;
                this.attackButton.MoveOnClick = false;
                this.attackButton.Active = false;
            }
            else
            {
                this.attackButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[10];
                this.attackButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[5];
                this.attackButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[5];
                this.attackButton.MoveOnClick = true;
                this.attackButton.CustomTooltipID = 0xb54;
                this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToAttacks), "AllArmiesPanel2_attack");
                this.attackButton.Active = true;
            }
            this.headerImage.addControl(this.attackButton);
            this.scoutButton.Position = new Point(0x2a6, 4);
            if (mode == 3)
            {
                this.scoutButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[1];
                this.scoutButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[1];
                this.scoutButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[1];
                this.scoutButton.CustomTooltipID = 0;
                this.scoutButton.MoveOnClick = false;
                this.scoutButton.Active = false;
            }
            else
            {
                this.scoutButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[11];
                this.scoutButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[6];
                this.scoutButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[6];
                this.scoutButton.CustomTooltipID = 0xb55;
                this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToScouts), "AllArmiesPanel2_scouts");
                this.scoutButton.MoveOnClick = true;
                this.scoutButton.Active = true;
            }
            this.headerImage.addControl(this.scoutButton);
            this.reinforcementsButton.Position = new Point(0x2e6, 4);
            if (mode == 4)
            {
                this.reinforcementsButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[2];
                this.reinforcementsButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[2];
                this.reinforcementsButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[2];
                this.reinforcementsButton.CustomTooltipID = 0;
                this.reinforcementsButton.MoveOnClick = false;
                this.reinforcementsButton.Active = false;
            }
            else
            {
                this.reinforcementsButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[12];
                this.reinforcementsButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[7];
                this.reinforcementsButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[7];
                this.reinforcementsButton.CustomTooltipID = 0xb56;
                this.reinforcementsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToReinforcements), "AllArmiesPanel2_reinforcements");
                this.reinforcementsButton.MoveOnClick = true;
                this.reinforcementsButton.Active = true;
            }
            this.headerImage.addControl(this.reinforcementsButton);
            this.tradeButton.Position = new Point(0x326, 4);
            if (mode == 1)
            {
                this.tradeButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[3];
                this.tradeButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[3];
                this.tradeButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[3];
                this.tradeButton.CustomTooltipID = 0;
                this.tradeButton.MoveOnClick = false;
                this.tradeButton.Active = false;
            }
            else
            {
                this.tradeButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[13];
                this.tradeButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[8];
                this.tradeButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[8];
                this.tradeButton.CustomTooltipID = 0xb57;
                this.tradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToMerchants), "AllArmiesPanel2_trade");
                this.tradeButton.MoveOnClick = true;
                this.tradeButton.Active = true;
            }
            this.headerImage.addControl(this.tradeButton);
            this.monkButton.Position = new Point(870, 4);
            if (mode == 2)
            {
                this.monkButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[4];
                this.monkButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[4];
                this.monkButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[4];
                this.monkButton.CustomTooltipID = 0;
                this.monkButton.MoveOnClick = false;
                this.monkButton.Active = false;
            }
            else
            {
                this.monkButton.ImageNorm = (Image) GFXLibrary.attack_tabs_comp[14];
                this.monkButton.ImageOver = (Image) GFXLibrary.attack_tabs_comp[9];
                this.monkButton.ImageClick = (Image) GFXLibrary.attack_tabs_comp[9];
                this.monkButton.CustomTooltipID = 0xb58;
                this.monkButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.toggleToMonks), "AllArmiesPanel2_monks");
                this.monkButton.MoveOnClick = true;
                this.monkButton.Active = true;
            }
            this.headerImage.addControl(this.monkButton);
            this.blockYSize = ((height - 40) - 0x38) / 2;
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 5);
            this.backgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(0xe4, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(450, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.divider3Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider3Image.Position = new Point(0x33c, 0);
            this.headerLabelsImage.addControl(this.divider3Image);
            if (mode == 1)
            {
                this.divider7Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
                this.divider7Image.Position = new Point(570, 0);
                this.headerLabelsImage.addControl(this.divider7Image);
            }
            else if (mode == 2)
            {
                this.divider7Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
                this.divider7Image.Position = new Point(520, 0);
                this.headerLabelsImage.addControl(this.divider7Image);
            }
            if (mode == 0)
            {
                this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Outgoing_Attacks_To", "Outgoing Attacks To");
            }
            else if (mode == 1)
            {
                this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Trades", "Your Merchants To");
            }
            else if (mode == 2)
            {
                this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Monks", "Your Monks To");
            }
            else if (mode == 3)
            {
                this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Scouts", "Your Scouting of");
            }
            else if (mode == 4)
            {
                this.outGoingAttacksLabel.Text = SK.Text("AllArmiesPanel_Your_Reinforcements", "Outgoing Reinforcements");
            }
            this.outGoingAttacksLabel.Color = ARGBColors.Black;
            this.outGoingAttacksLabel.Position = new Point(12, -2);
            this.outGoingAttacksLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.outGoingAttacksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.outGoingAttacksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.outGoingAttacksLabel);
            this.outGoingFromLabel.Text = SK.Text("AllArmiesPanel_From", "From");
            this.outGoingFromLabel.Color = ARGBColors.Black;
            this.outGoingFromLabel.Position = new Point(0xe9, -2);
            this.outGoingFromLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.outGoingFromLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.outGoingFromLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.outGoingFromLabel);
            this.outGoingArrivesLabel.Text = SK.Text("AllArmiesPanel_Arrives", "Arrives");
            this.outGoingArrivesLabel.Color = ARGBColors.Black;
            this.outGoingArrivesLabel.Position = new Point(0x341, -2);
            this.outGoingArrivesLabel.Size = new Size(0x72, this.headerLabelsImage.Height);
            this.outGoingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.outGoingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.outGoingArrivesLabel);
            if (mode == 1)
            {
                this.outGoingCarryingLabel.Text = SK.Text("AllArmiesPanel_Trader_Carrying", "Carrying");
                this.outGoingCarryingLabel.Color = ARGBColors.Black;
                this.outGoingCarryingLabel.Position = new Point(0x1c7, -2);
                this.outGoingCarryingLabel.Size = new Size(110, this.headerLabelsImage.Height);
                this.outGoingCarryingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.outGoingCarryingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.headerLabelsImage.addControl(this.outGoingCarryingLabel);
                this.outGoingStatusLabel.Text = SK.Text("AllArmiesPanel_Trader_Status", "Status");
                this.outGoingStatusLabel.Color = ARGBColors.Black;
                this.outGoingStatusLabel.Position = new Point(0x23f, -2);
                this.outGoingStatusLabel.Size = new Size(250, this.headerLabelsImage.Height);
                this.outGoingStatusLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.outGoingStatusLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.headerLabelsImage.addControl(this.outGoingStatusLabel);
            }
            if (mode == 2)
            {
                this.outGoingStatusLabel.Text = SK.Text("AllArmiesPanel_Monk_Command", "Command");
                this.outGoingStatusLabel.Color = ARGBColors.Black;
                this.outGoingStatusLabel.Position = new Point(0x20d, -2);
                this.outGoingStatusLabel.Size = new Size(250, this.headerLabelsImage.Height);
                this.outGoingStatusLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.outGoingStatusLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.headerLabelsImage.addControl(this.outGoingStatusLabel);
            }
            this.headerLabelsImage2.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage2.Position = new Point(0x19, this.blockYSize + 5);
            this.backgroundImage.addControl(this.headerLabelsImage2);
            this.headerLabelsImage2.Create((Image) GFXLibrary.brown_mail2_field_bar_mail_left, (Image) GFXLibrary.brown_mail2_field_bar_mail_middle, (Image) GFXLibrary.brown_mail2_field_bar_mail_right);
            this.divider4Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider4Image.Position = new Point(0xe4, 0);
            this.headerLabelsImage2.addControl(this.divider4Image);
            this.divider5Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider5Image.Position = new Point(450, 0);
            this.headerLabelsImage2.addControl(this.divider5Image);
            this.divider6Image.Image = (Image) GFXLibrary.brown_mail2_field_bar_mail_divider;
            this.divider6Image.Position = new Point(0x33c, 0);
            this.headerLabelsImage2.addControl(this.divider6Image);
            if (mode == 0)
            {
                this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Attacks_From", "Incoming Attacks From");
            }
            else if (mode == 1)
            {
                this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Trades_from", "Incoming Merchants From");
            }
            else if (mode == 2)
            {
                this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Monks", "Incoming Monks From");
            }
            else if (mode == 3)
            {
                this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Scouts", "Incoming Scouts From");
            }
            else if (mode == 4)
            {
                this.incomingAttacksLabel.Text = SK.Text("AllArmiesPanel_Incoming_Reinforcements", "Incoming Reinforcements From");
            }
            this.incomingAttacksLabel.Color = ARGBColors.Black;
            this.incomingAttacksLabel.Position = new Point(12, -2);
            this.incomingAttacksLabel.Size = new Size(0xe0, this.headerLabelsImage.Height);
            this.incomingAttacksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.incomingAttacksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.incomingAttacksLabel);
            if (mode == 0)
            {
                this.incomingAttackingLabel.Text = SK.Text("GENERIC_Attacking", "Attacking");
            }
            else
            {
                this.incomingAttackingLabel.Text = SK.Text("GENERIC_To", "To");
            }
            this.incomingAttackingLabel.Color = ARGBColors.Black;
            this.incomingAttackingLabel.Position = new Point(0xe9, -2);
            this.incomingAttackingLabel.Size = new Size(0xe0, this.headerLabelsImage.Height);
            this.incomingAttackingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.incomingAttackingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.incomingAttackingLabel);
            this.incomingArrivesLabel.Text = SK.Text("AllArmiesPanel_Arrives", "Arrives");
            this.incomingArrivesLabel.Color = ARGBColors.Black;
            this.incomingArrivesLabel.Position = new Point(0x341, -2);
            this.incomingArrivesLabel.Size = new Size(0x72, this.headerLabelsImage.Height);
            this.incomingArrivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.incomingArrivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.incomingArrivesLabel);
            this.outgoingScrollArea.Position = new Point(0x19, 40);
            this.outgoingScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.outgoingScrollArea);
            int num2 = this.outgoingScrollBar.Value;
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
            int num3 = this.incomingScrollBar.Value;
            this.incomingScrollBar.Position = new Point(0x3af, (0x23 + this.blockYSize) + 5);
            this.incomingScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.incomingScrollBar);
            this.incomingScrollBar.Value = 0;
            this.incomingScrollBar.Max = 100;
            this.incomingScrollBar.NumVisibleLines = 0x19;
            this.incomingScrollBar.Create(null, null, null, (Image) GFXLibrary.brown_24wide_thumb_top, (Image) GFXLibrary.brown_24wide_thumb_middle, (Image) GFXLibrary.brown_24wide_thumb_bottom);
            this.incomingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
            this.smallPeasantImage2.Visible = false;
            if (mode == 0)
            {
                this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
                this.smallPeasantImage.Position = new Point(0x1d9, -10);
                this.smallPeasantImage.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Width - 60, this.smallPeasantImage.Height);
                this.headerLabelsImage.addControl(this.smallPeasantImage);
                this.smallPeasantImage2.Image = (Image) GFXLibrary.barracks_screen_bottom_units;
                this.smallPeasantImage2.Position = new Point(0x15c, -10);
                this.smallPeasantImage2.ClipRect = new Rectangle(this.smallPeasantImage2.Width - 60, 0, 60, this.smallPeasantImage2.Height);
                this.headerLabelsImage.addControl(this.smallPeasantImage2);
                this.smallPeasantImage2.Visible = true;
            }
            else if (mode == 4)
            {
                this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
                this.smallPeasantImage.Position = new Point(0x1f7, -10);
                this.smallPeasantImage.ClipRect = new Rectangle(0, 0, this.smallPeasantImage.Width - 60, this.smallPeasantImage.Height);
                this.headerLabelsImage.addControl(this.smallPeasantImage);
            }
            else if (mode == 3)
            {
                this.smallPeasantImage.Image = (Image) GFXLibrary.armies_screen_troops;
                this.smallPeasantImage.Position = new Point((0x1f7 - (this.smallPeasantImage.Width - 60)) - 8, -10);
                this.smallPeasantImage.ClipRect = new Rectangle(this.smallPeasantImage.Width - 60, 0, 60, this.smallPeasantImage.Height);
                this.headerLabelsImage.addControl(this.smallPeasantImage);
            }
            else if (mode == 2)
            {
                this.smallPeasantImage.Image = (Image) GFXLibrary.monk_icon;
                this.smallPeasantImage.Position = new Point(0x1d4, -10);
                this.headerLabelsImage.addControl(this.smallPeasantImage);
            }
            if ((mode == 0) || (mode == 3))
            {
                SparseArray array = GameEngine.Instance.World.getArmyArray();
                this.armyList.Clear();
                foreach (WorldMap.LocalArmyData data in array)
                {
                    this.armyList.Add(data);
                }
                this.armyList.Sort(this.armyComparer);
                if (mode == 0)
                {
                    this.addArmies();
                }
                else if (mode == 3)
                {
                    this.addScouts();
                }
            }
            else if (mode == 1)
            {
                SparseArray array2 = GameEngine.Instance.World.getTraderArray();
                this.merchantList.Clear();
                foreach (WorldMap.LocalTrader trader in array2)
                {
                    this.merchantList.Add(trader);
                }
                this.merchantList.Sort(this.merchantComparer);
                this.addMerchants();
            }
            else if (mode == 2)
            {
                SparseArray array3 = GameEngine.Instance.World.getPeopleArray();
                this.monkList.Clear();
                foreach (WorldMap.LocalPerson person in array3)
                {
                    this.monkList.Add(person);
                }
                this.monkList.Sort(this.monkComparer);
                this.addMonks();
            }
            else if (mode == 4)
            {
                SparseArray array4 = GameEngine.Instance.World.getReinforcementsArray();
                this.reinforcementList.Clear();
                foreach (WorldMap.LocalArmyData data2 in array4)
                {
                    this.reinforcementList.Add(data2);
                }
                this.reinforcementList.Sort(this.armyComparer);
                this.addReinforcements();
            }
            if (resized)
            {
                this.outgoingScrollBar.Value = num2;
                this.incomingScrollBar.Value = num3;
            }
            if (mode == 0)
            {
                this.diplomacyHeaderLabel.Text = SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy");
                this.diplomacyHeaderLabel.Color = ARGBColors.White;
                this.diplomacyHeaderLabel.DropShadowColor = ARGBColors.Black;
                this.diplomacyHeaderLabel.Position = new Point(20, ((height - 40) - 40) - 2);
                this.diplomacyHeaderLabel.Size = new Size(100, 30);
                this.diplomacyHeaderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.diplomacyHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.backgroundImage.addControl(this.diplomacyHeaderLabel);
                this.diplomacyTextLabel.Text = SK.Text("AllArmiesPanel_Avoiding_Attacks", "Chance of avoiding an Incoming AI / Enemy Attack") + " : ";
                this.diplomacyTextLabel.Color = ARGBColors.White;
                this.diplomacyTextLabel.DropShadowColor = ARGBColors.Black;
                this.diplomacyTextLabel.Position = new Point(0x7d, (height - 40) - 40);
                this.diplomacyTextLabel.Size = new Size(630, 30);
                this.diplomacyTextLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.diplomacyTextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.backgroundImage.addControl(this.diplomacyTextLabel);
                this.btnDiplomacy.ImageNorm = (Image) GFXLibrary.brown_misc_button_blue_210wide_normal;
                this.btnDiplomacy.ImageOver = (Image) GFXLibrary.brown_misc_button_blue_210wide_over;
                this.btnDiplomacy.ImageClick = (Image) GFXLibrary.brown_misc_button_blue_210wide_pushed;
                this.btnDiplomacy.Position = new Point(base.Width - 230, ((height - 40) - 40) - 4);
                this.btnDiplomacy.Text.Text = SK.Text("AllArmiesPanel_Turn_Off_Diplomacy", "Turn Off Diplomacy");
                this.btnDiplomacy.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.btnDiplomacy.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.btnDiplomacy.TextYOffset = -3;
                this.btnDiplomacy.Text.Color = ARGBColors.Black;
                this.btnDiplomacy.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.diplomacyClick), "AllArmiesPanel2_diplomacy");
                this.backgroundImage.addControl(this.btnDiplomacy);
                this.updateDiplomacyStatus();
            }
            if (mode == 3)
            {
                if (((Math.Abs(sortMode) >= 3) && (Math.Abs(sortMode) <= 7)) || (Math.Abs(sortMode) > 9))
                {
                    sortMode = 0;
                }
            }
            else if ((mode == 0) || (mode == 4))
            {
                if ((Math.Abs(sortMode) > 8) && (Math.Abs(sortMode) != 20))
                {
                    sortMode = 0;
                }
            }
            else if (mode == 1)
            {
                if (((Math.Abs(sortMode) >= 3) && (Math.Abs(sortMode) <= 7)) || ((Math.Abs(sortMode) == 9) || (Math.Abs(sortMode) >= 12)))
                {
                    sortMode = 0;
                }
            }
            else if ((mode == 2) && (((Math.Abs(sortMode) >= 3) && (Math.Abs(sortMode) <= 7)) || ((Math.Abs(sortMode) >= 9) && (Math.Abs(sortMode) < 12))))
            {
                sortMode = 0;
            }
            this.sortArea1.Position = new Point(7, 0);
            this.sortArea1.Size = new Size(0xd6, 0x18);
            this.sortArea1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
            this.sortArea1.CustomTooltipData = this.sortArea1.Data = 1;
            this.sortArea1.CustomTooltipID = 0xb59;
            this.headerLabelsImage.addControl(this.sortArea1);
            this.sortArea1a.Position = new Point(7, 0);
            this.sortArea1a.Size = new Size(0xd6, 0x18);
            this.sortArea1a.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
            this.sortArea1a.CustomTooltipData = this.sortArea1a.Data = 2;
            this.sortArea1a.CustomTooltipID = 0xb59;
            this.headerLabelsImage2.addControl(this.sortArea1a);
            this.sortArea2.Position = new Point(0xe5, 0);
            this.sortArea2.Size = new Size(0xd6, 0x18);
            this.sortArea2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
            this.sortArea2.CustomTooltipData = this.sortArea2.Data = 2;
            this.sortArea2.CustomTooltipID = 0xb59;
            this.headerLabelsImage.addControl(this.sortArea2);
            this.sortArea2a.Position = new Point(0xe5, 0);
            this.sortArea2a.Size = new Size(0xd6, 0x18);
            this.sortArea2a.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
            this.sortArea2a.CustomTooltipData = this.sortArea2a.Data = 1;
            this.sortArea2a.CustomTooltipID = 0xb59;
            this.headerLabelsImage2.addControl(this.sortArea2a);
            this.sortArea8.Position = new Point(0x33c, 0);
            this.sortArea8.Size = new Size(100, 0x18);
            this.sortArea8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
            this.sortArea8.CustomTooltipData = this.sortArea8.Data = 8;
            this.sortArea8.CustomTooltipID = 0xb59;
            this.headerLabelsImage.addControl(this.sortArea8);
            this.sortArea8a.Position = new Point(0x33c, 0);
            this.sortArea8a.Size = new Size(100, 0x18);
            this.sortArea8a.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
            this.sortArea8a.CustomTooltipData = this.sortArea8a.Data = 8;
            this.sortArea8a.CustomTooltipID = 0xb59;
            this.headerLabelsImage2.addControl(this.sortArea8a);
            if (mode == 3)
            {
                this.sortArea3.Position = new Point(0x1dd, 0);
                this.sortArea3.Size = new Size(60, 0x18);
                this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea3.CustomTooltipData = this.sortArea3.Data = 9;
                this.sortArea3.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea3);
            }
            else if (mode == 0)
            {
                this.sortArea3.Position = new Point(0x1bf, 0);
                this.sortArea3.Size = new Size(60, 0x18);
                this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea3.CustomTooltipData = this.sortArea3.Data = 3;
                this.sortArea3.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea3);
                this.sortArea4.Position = new Point(0x201, 0);
                this.sortArea4.Size = new Size(60, 0x18);
                this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea4.CustomTooltipData = this.sortArea4.Data = 4;
                this.sortArea4.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea4);
                this.sortArea5.Position = new Point(570, 0);
                this.sortArea5.Size = new Size(60, 0x18);
                this.sortArea5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea5.CustomTooltipData = this.sortArea5.Data = 5;
                this.sortArea5.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea5);
                this.sortArea6.Position = new Point(0x277, 0);
                this.sortArea6.Size = new Size(60, 0x18);
                this.sortArea6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea6.CustomTooltipData = this.sortArea6.Data = 6;
                this.sortArea6.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea6);
                this.sortArea7.Position = new Point(0x2ad, 0);
                this.sortArea7.Size = new Size(60, 0x18);
                this.sortArea7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea7.CustomTooltipData = this.sortArea7.Data = 7;
                this.sortArea7.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea7);
                this.sortArea9.Position = new Point(0x2e3, 0);
                this.sortArea9.Size = new Size(60, 0x18);
                this.sortArea9.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea9.CustomTooltipData = this.sortArea9.Data = 20;
                this.sortArea9.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea9);
            }
            else if (mode == 4)
            {
                this.sortArea3.Position = new Point(0x1dd, 0);
                this.sortArea3.Size = new Size(60, 0x18);
                this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea3.CustomTooltipData = this.sortArea3.Data = 3;
                this.sortArea3.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea3);
                this.sortArea4.Position = new Point(0x21f, 0);
                this.sortArea4.Size = new Size(60, 0x18);
                this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea4.CustomTooltipData = this.sortArea4.Data = 4;
                this.sortArea4.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea4);
                this.sortArea5.Position = new Point(600, 0);
                this.sortArea5.Size = new Size(60, 0x18);
                this.sortArea5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea5.CustomTooltipData = this.sortArea5.Data = 5;
                this.sortArea5.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea5);
                this.sortArea6.Position = new Point(0x295, 0);
                this.sortArea6.Size = new Size(60, 0x18);
                this.sortArea6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea6.CustomTooltipData = this.sortArea6.Data = 6;
                this.sortArea6.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea6);
                this.sortArea7.Position = new Point(0x2cb, 0);
                this.sortArea7.Size = new Size(60, 0x18);
                this.sortArea7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea7.CustomTooltipData = this.sortArea7.Data = 7;
                this.sortArea7.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea7);
            }
            else if (mode == 1)
            {
                this.sortArea3.Position = new Point(0x1c1, 0);
                this.sortArea3.Size = new Size(110, 0x18);
                this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea3.CustomTooltipData = this.sortArea3.Data = 10;
                this.sortArea3.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea3);
                this.sortArea4.Position = new Point(0x239, 0);
                this.sortArea4.Size = new Size(250, 0x18);
                this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea4.CustomTooltipData = this.sortArea4.Data = 11;
                this.sortArea4.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea4);
            }
            else if (mode == 2)
            {
                this.sortArea3.Position = new Point(0x1c9, 0);
                this.sortArea3.Size = new Size(50, 0x18);
                this.sortArea3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea3.CustomTooltipData = this.sortArea3.Data = 12;
                this.sortArea3.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea3);
                this.sortArea4.Position = new Point(0x20a, 0);
                this.sortArea4.Size = new Size(300, 0x18);
                this.sortArea4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortClicked));
                this.sortArea4.CustomTooltipData = this.sortArea4.Data = 13;
                this.sortArea4.CustomTooltipID = 0xb59;
                this.headerLabelsImage.addControl(this.sortArea4);
            }
            TradersUpdated = false;
            MonksUpdated = false;
            base.Invalidate();
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
            base.Name = "AllArmiesPanel2";
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

        public void preInit()
        {
            sortMode = 8;
        }

        public void sortClicked()
        {
            CustomSelfDrawPanel.CSDControl clickedControl = base.ClickedControl;
            if (clickedControl != null)
            {
                int data = clickedControl.Data;
                if (data == sortMode)
                {
                    sortMode = -data;
                }
                else
                {
                    sortMode = data;
                }
                this.init(false, mode);
            }
        }

        public void toggleToAttacks()
        {
            if (this.attackButton.Active)
            {
                this.init(false, 0);
            }
        }

        public void toggleToMerchants()
        {
            if (this.tradeButton.Active)
            {
                this.init(false, 1);
            }
        }

        public void toggleToMonks()
        {
            if (this.monkButton.Active)
            {
                this.init(false, 2);
            }
        }

        public void toggleToReinforcements()
        {
            if (this.reinforcementsButton.Active)
            {
                this.init(false, 4);
            }
        }

        public void toggleToScouts()
        {
            if (this.scoutButton.Active)
            {
                this.init(false, 3);
            }
        }

        public void update()
        {
            bool flag = false;
            double localTime = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (ArmyLine line in this.armyLineList)
            {
                if (!line.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (ArmyLine line2 in this.armyLineList2)
            {
                if (!line2.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (MerchantLine line3 in this.merchantLineList)
            {
                if (!line3.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (MerchantLine line4 in this.merchantLineList2)
            {
                if (!line4.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (MonkLine line5 in this.monkLineList)
            {
                if (!line5.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (MonkLine line6 in this.monkLineList2)
            {
                if (!line6.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (ScoutLine line7 in this.scoutLineList)
            {
                if (!line7.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (ScoutLine line8 in this.scoutLineList2)
            {
                if (!line8.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (ReinforcementLine line9 in this.reinforcementLineList)
            {
                if (!line9.update(localTime))
                {
                    flag = true;
                }
            }
            foreach (ReinforcementLine line10 in this.reinforcementLineList2)
            {
                if (!line10.update(localTime))
                {
                    flag = true;
                }
            }
            if (((flag || (InterfaceMgr.Instance.getMainTabBar().isArmiesFlashing() && (mode == 0))) || ((mode == 1) && TradersUpdated)) || ((mode == 2) && MonksUpdated))
            {
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - this.lastfullUpdate);
                if (span.TotalSeconds > 1.5)
                {
                    this.lastfullUpdate = now;
                    long highestAttackingArmy = -1L;
                    int numAttacks = GameEngine.Instance.World.countIncomingAttacks(ref highestAttackingArmy);
                    InterfaceMgr.Instance.getMainTabBar().incomingAttacks(numAttacks, highestAttackingArmy);
                    this.init(true, mode);
                }
            }
        }

        public void updateDiplomacyStatus()
        {
            if (RemoteServices.Instance.ReportFilters.diplomacyActive)
            {
                this.btnDiplomacy.Text.Text = SK.Text("AllArmiesPanel_Turn_Off_Diplomacy", "Turn Off Diplomacy");
                this.diplomacyTextLabel.Text = SK.Text("AllArmiesPanel_Avoiding_Attacks", "Chance of avoiding an Incoming AI / Enemy Attack") + " : " + ((CardTypes.cards_diplomacyExtraPercent(GameEngine.Instance.World.UserCardData) + ResearchData.diplomacyPercent[GameEngine.Instance.World.UserResearchData.Research_Diplomacy])).ToString() + "%";
            }
            else
            {
                this.btnDiplomacy.Text.Text = SK.Text("AllArmiesPanel_Turn_On_Diplomacy", "Turn On Diplomacy");
                this.diplomacyTextLabel.Text = SK.Text("AllArmiesPanel_Avoiding_Attacks", "Chance of avoiding an Incoming AI / Enemy Attack") + " : 0% (" + SK.Text("AllArmiesPanel_Diplomacy_Is_Off", "Diplomacy is Off") + ")";
            }
        }

        public void UpdateDiplomacyStatusCallBack(UpdateDiplomacyStatus_ReturnType returnData)
        {
            this.btnDiplomacy.Enabled = true;
            bool success = returnData.Success;
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
                if (AllArmiesPanel2.sortMode < 0)
                {
                    WorldMap.LocalArmyData data = x;
                    x = y;
                    y = data;
                }
                bool flag = true;
                if (AllArmiesPanel2.mode == 0)
                {
                    switch (Math.Abs(AllArmiesPanel2.sortMode))
                    {
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 20:
                        {
                            bool flag2 = false;
                            bool flag3 = false;
                            if ((GameEngine.Instance.World.isUserVillage(x.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(x.homeVillageID)) && (x.attackType != 13))
                            {
                                flag2 = true;
                            }
                            if ((GameEngine.Instance.World.isUserVillage(y.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(y.homeVillageID)) && (y.attackType != 13))
                            {
                                flag3 = true;
                            }
                            if (!flag2 && !flag3)
                            {
                                flag = false;
                            }
                            if (!flag2)
                            {
                                return 1;
                            }
                            if (flag3)
                            {
                                break;
                            }
                            return -1;
                        }
                    }
                }
                else if ((AllArmiesPanel2.mode == 3) && (Math.Abs(AllArmiesPanel2.sortMode) == 9))
                {
                    bool flag4 = false;
                    bool flag5 = false;
                    if (GameEngine.Instance.World.isUserVillage(x.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(x.homeVillageID))
                    {
                        flag4 = true;
                    }
                    if (GameEngine.Instance.World.isUserVillage(y.travelFromVillageID) || GameEngine.Instance.World.isUserVillage(y.homeVillageID))
                    {
                        flag5 = true;
                    }
                    if (!flag4 && !flag5)
                    {
                        flag = false;
                    }
                    if (!flag4)
                    {
                        return 1;
                    }
                    if (!flag5)
                    {
                        return -1;
                    }
                }
                if (flag)
                {
                    switch (AllArmiesPanel2.sortMode)
                    {
                        case -20:
                        case 20:
                        {
                            int num8 = x.numCaptains.CompareTo(y.numCaptains);
                            if (num8 == 0)
                            {
                                break;
                            }
                            return num8;
                        }
                        case -9:
                        case 9:
                        {
                            int num10 = x.numScouts.CompareTo(y.numScouts);
                            if (num10 == 0)
                            {
                                break;
                            }
                            return num10;
                        }
                        case -8:
                        case 8:
                        {
                            int num9 = x.serverEndTime.CompareTo(y.serverEndTime);
                            if (num9 != 0)
                            {
                                return num9;
                            }
                            break;
                        }
                        case -7:
                        case 7:
                        {
                            int num7 = x.numCatapults.CompareTo(y.numCatapults);
                            if (num7 == 0)
                            {
                                break;
                            }
                            return num7;
                        }
                        case -6:
                        case 6:
                        {
                            int num6 = x.numSwordsmen.CompareTo(y.numSwordsmen);
                            if (num6 == 0)
                            {
                                break;
                            }
                            return num6;
                        }
                        case -5:
                        case 5:
                        {
                            int num5 = x.numPikemen.CompareTo(y.numPikemen);
                            if (num5 == 0)
                            {
                                break;
                            }
                            return num5;
                        }
                        case -4:
                        case 4:
                        {
                            int num4 = x.numArchers.CompareTo(y.numArchers);
                            if (num4 == 0)
                            {
                                break;
                            }
                            return num4;
                        }
                        case -3:
                        case 3:
                        {
                            int num3 = x.numPeasants.CompareTo(y.numPeasants);
                            if (num3 == 0)
                            {
                                break;
                            }
                            return num3;
                        }
                        case -2:
                        case 2:
                        {
                            string str3 = GameEngine.Instance.World.getVillageName(x.travelFromVillageID);
                            string strB = GameEngine.Instance.World.getVillageName(y.travelFromVillageID);
                            int num2 = str3.CompareTo(strB);
                            if (num2 == 0)
                            {
                                break;
                            }
                            return num2;
                        }
                        case -1:
                        case 1:
                        {
                            string str = GameEngine.Instance.World.getVillageName(x.targetVillageID);
                            string str2 = GameEngine.Instance.World.getVillageName(y.targetVillageID);
                            int num = str.CompareTo(str2);
                            if (num == 0)
                            {
                                break;
                            }
                            return num;
                        }
                    }
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
            private CustomSelfDrawPanel.CSDImage imgCaptain = new CustomSelfDrawPanel.CSDImage();
            private bool incoming = true;
            private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCaptains = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblExtraInfo = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private int leftVillageID = -1;
            private WorldMap.LocalArmyData m_army;
            private long m_armyID = -1L;
            private DateTime m_arrivalTime = DateTime.Now;
            private int m_origLoot = -1;
            private int m_otherVillageID = -1;
            private AllArmiesPanel2 m_parent;
            private int m_position = -1000;
            private bool m_returning;
            private int m_villageID = -1;
            private int rightVillageID = -1;

            public void initIncoming(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning, bool tutorial, int attackType, int numCaptains)
            {
                this.incoming = false;
                this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, false, tutorial, attackType, numCaptains);
            }

            public void initSent(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning, int attackType, int numCaptains)
            {
                this.incoming = true;
                this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, attackType, numCaptains);
            }

            private void initText(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning, bool showTroops, bool tutorial, int attackType, int numCaptains)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_army = GameEngine.Instance.World.getArmy(armyID);
                this.m_origLoot = this.m_army.lootType;
                this.m_armyID = armyID;
                this.m_villageID = villageID;
                this.m_otherVillageID = otherVillage;
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
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
                this.lblTarget.Color = ARGBColors.Black;
                this.lblTarget.RolloverColor = ARGBColors.White;
                this.lblTarget.Position = new Point(0xe9, 0);
                this.lblTarget.Size = new Size(0xe0, this.backgroundImage.Height);
                this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
                this.backgroundImage.addControl(this.lblTarget);
                this.leftVillageID = villageID;
                this.rightVillageID = otherVillage;
                this.lblArrivalTime.Text = "";
                this.lblArrivalTime.Color = ARGBColors.Black;
                this.lblArrivalTime.Position = new Point(0x341, 0);
                this.lblArrivalTime.Size = new Size(0x72, this.backgroundImage.Height);
                this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblArrivalTime);
                if (showTroops)
                {
                    this.lblPeasants.Text = numPeasants.ToString();
                    this.lblPeasants.Color = ARGBColors.Black;
                    this.lblPeasants.Position = new Point(0x1c7, 0);
                    this.lblPeasants.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblPeasants);
                    this.lblArchers.Text = numArchers.ToString();
                    this.lblArchers.Color = ARGBColors.Black;
                    this.lblArchers.Position = new Point(0x203, 0);
                    this.lblArchers.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblArchers);
                    this.lblPikemen.Text = numPikemen.ToString();
                    this.lblPikemen.Color = ARGBColors.Black;
                    this.lblPikemen.Position = new Point(0x23f, 0);
                    this.lblPikemen.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblPikemen);
                    this.lblSwordsmen.Text = numSwordsmen.ToString();
                    this.lblSwordsmen.Color = ARGBColors.Black;
                    this.lblSwordsmen.Position = new Point(0x27b, 0);
                    this.lblSwordsmen.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblSwordsmen);
                    this.lblCatapults.Text = numCatapults.ToString();
                    this.lblCatapults.Color = ARGBColors.Black;
                    this.lblCatapults.Position = new Point(0x2b7, 0);
                    this.lblCatapults.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblCatapults);
                    this.lblCaptains.Text = numCaptains.ToString();
                    this.lblCaptains.Color = ARGBColors.Black;
                    this.lblCaptains.Position = new Point(0x2f3, 0);
                    this.lblCaptains.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblCaptains.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblCaptains.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblCaptains);
                }
                else if (numCaptains > 0)
                {
                    this.imgCaptain.Image = (Image) GFXLibrary.barracks_screen_bottom_units;
                    this.imgCaptain.Position = new Point(200, -10);
                    this.imgCaptain.ClipRect = new Rectangle(this.imgCaptain.Width - 60, 0, 60, this.imgCaptain.Height);
                    this.backgroundImage.addControl(this.imgCaptain);
                }
                if (!this.incoming)
                {
                    if (attackType == 30)
                    {
                        this.lblPeasants.Text = SK.Text("AllArmiesSentLine_Vassal_Support", "Vassal Support");
                        this.lblPeasants.Color = ARGBColors.Black;
                        this.lblPeasants.Position = new Point(0x1c7, 0);
                        this.lblPeasants.Size = new Size(250, this.backgroundImage.Height);
                        this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                        this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                        this.backgroundImage.addControl(this.lblPeasants);
                    }
                    if (attackType == 0x1f)
                    {
                        this.lblPeasants.Text = SK.Text("AllArmiesSentLine_Capital_Support", "Capital Support");
                        this.lblPeasants.Color = ARGBColors.Black;
                        this.lblPeasants.Position = new Point(0x1c7, 0);
                        this.lblPeasants.Size = new Size(250, this.backgroundImage.Height);
                        this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                        this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                        this.backgroundImage.addControl(this.lblPeasants);
                    }
                }
                else
                {
                    bool flag = false;
                    if (attackType == 30)
                    {
                        this.lblExtraInfo.Text = SK.Text("AllArmiesSentLine_Vassal_Support", "Vassal Support");
                        this.lblExtraInfo.Color = ARGBColors.Black;
                        this.lblExtraInfo.Position = new Point(0x1c7, 7);
                        this.lblExtraInfo.Size = new Size(360, this.backgroundImage.Height);
                        this.lblExtraInfo.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                        this.lblExtraInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                        this.backgroundImage.addControl(this.lblExtraInfo);
                        flag = true;
                    }
                    if (attackType == 0x1f)
                    {
                        this.lblExtraInfo.Text = SK.Text("AllArmiesSentLine_Capital_Support", "Capital Support");
                        this.lblExtraInfo.Color = ARGBColors.Black;
                        this.lblExtraInfo.Position = new Point(0x1c7, 7);
                        this.lblExtraInfo.Size = new Size(360, this.backgroundImage.Height);
                        this.lblExtraInfo.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                        this.lblExtraInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                        this.backgroundImage.addControl(this.lblExtraInfo);
                        flag = true;
                    }
                    if (flag)
                    {
                        this.lblPeasants.Position = new Point(0x1e5, -7);
                        this.lblArchers.Position = new Point(0x221, -7);
                        this.lblPikemen.Position = new Point(0x25d, -7);
                        this.lblSwordsmen.Position = new Point(0x299, -7);
                        this.lblCatapults.Position = new Point(0x2d5, -7);
                    }
                }
                this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
                base.invalidate();
            }

            private void lblTarget_Click()
            {
                if (this.rightVillageID >= 0)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
                }
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
                WorldMap.LocalArmyData data = GameEngine.Instance.World.getArmy(this.m_armyID);
                if ((data == null) || (data.lootType != this.m_origLoot))
                {
                    return false;
                }
                DateTime time = VillageMap.getCurrentServerTime();
                TimeSpan span = (TimeSpan) (this.m_arrivalTime - time);
                int secsLeft = (int) (span.TotalSeconds + 0.5);
                if (secsLeft < 1)
                {
                    secsLeft = 0;
                }
                this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
                if (this.m_returning)
                {
                    this.lblArrivalTime.Text = this.lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
                }
                return true;
            }
        }

        public class MerchantComparer : IComparer<WorldMap.LocalTrader>
        {
            public int Compare(WorldMap.LocalTrader x, WorldMap.LocalTrader y)
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
                if (AllArmiesPanel2.sortMode < 0)
                {
                    WorldMap.LocalTrader trader = x;
                    x = y;
                    y = trader;
                }
                bool flag = true;
                if (AllArmiesPanel2.sortMode == 10)
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    if (GameEngine.Instance.World.isUserVillage(x.trader.homeVillageID))
                    {
                        TimeSpan span = (TimeSpan) (x.serverEndTime - VillageMap.getCurrentServerTime());
                        if (((span.TotalSeconds > 0.0) || (x.trader.traderState == 0)) || (((x.trader.traderState == 1) || (x.trader.traderState == 3)) || (x.trader.traderState == 5)))
                        {
                            flag2 = true;
                        }
                    }
                    if (GameEngine.Instance.World.isUserVillage(y.trader.homeVillageID))
                    {
                        TimeSpan span2 = (TimeSpan) (y.serverEndTime - VillageMap.getCurrentServerTime());
                        if (((span2.TotalSeconds > 0.0) || (y.trader.traderState == 0)) || (((y.trader.traderState == 1) || (y.trader.traderState == 3)) || (y.trader.traderState == 5)))
                        {
                            flag3 = true;
                        }
                    }
                    if (!flag2 && !flag3)
                    {
                        flag = false;
                    }
                    if (!flag2)
                    {
                        return 1;
                    }
                    if (!flag3)
                    {
                        return -1;
                    }
                }
                if (flag)
                {
                    switch (AllArmiesPanel2.sortMode)
                    {
                        case -11:
                        case 11:
                        {
                            int num9 = 0;
                            switch (x.trader.traderState)
                            {
                                case 1:
                                    num9 = 1;
                                    break;

                                case 2:
                                case 4:
                                case 6:
                                    num9 = 4;
                                    break;

                                case 3:
                                    num9 = 2;
                                    break;

                                case 5:
                                    num9 = 3;
                                    break;
                            }
                            int num10 = 0;
                            switch (y.trader.traderState)
                            {
                                case 1:
                                    num10 = 1;
                                    break;

                                case 2:
                                case 4:
                                case 6:
                                    num10 = 4;
                                    break;

                                case 3:
                                    num10 = 2;
                                    break;

                                case 5:
                                    num10 = 3;
                                    break;
                            }
                            int num11 = num9.CompareTo(num10);
                            if (num11 != 0)
                            {
                                return num11;
                            }
                            break;
                        }
                        case -10:
                        case 10:
                        {
                            int resource = 0;
                            int num5 = 0;
                            int amount = 0;
                            int num7 = 0;
                            if (((x.trader.traderState == 1) || (x.trader.traderState == 3)) || (x.trader.traderState == 6))
                            {
                                resource = x.trader.resource;
                                amount = x.trader.amount;
                            }
                            if (((y.trader.traderState == 1) || (y.trader.traderState == 3)) || (y.trader.traderState == 6))
                            {
                                num5 = y.trader.resource;
                                num7 = y.trader.amount;
                            }
                            int num8 = resource.CompareTo(num5);
                            if (num8 == 0)
                            {
                                num8 = num7.CompareTo(amount);
                                if (num8 == 0)
                                {
                                    break;
                                }
                            }
                            return num8;
                        }
                        case -8:
                        case 8:
                        {
                            int num3 = x.serverEndTime.CompareTo(y.serverEndTime);
                            if (num3 == 0)
                            {
                                break;
                            }
                            return num3;
                        }
                        case -2:
                        case 2:
                        {
                            string str3 = GameEngine.Instance.World.getVillageName(x.trader.homeVillageID);
                            string strB = GameEngine.Instance.World.getVillageName(y.trader.homeVillageID);
                            int num2 = str3.CompareTo(strB);
                            if (num2 == 0)
                            {
                                break;
                            }
                            return num2;
                        }
                        case -1:
                        case 1:
                        {
                            string str = GameEngine.Instance.World.getVillageName(x.trader.targetVillageID);
                            string str2 = GameEngine.Instance.World.getVillageName(y.trader.targetVillageID);
                            int num = str.CompareTo(str2);
                            if (num == 0)
                            {
                                break;
                            }
                            return num;
                        }
                    }
                }
                if (x.traderID > y.traderID)
                {
                    return 1;
                }
                if (x.traderID < y.traderID)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class MerchantLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel description = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private int leftVillageID = -1;
            private WorldMap.LocalTrader m_army;
            private long m_armyID = -1L;
            private DateTime m_arrivalTime = DateTime.Now;
            private int m_otherVillageID = -1;
            private AllArmiesPanel2 m_parent;
            private int m_position = -1000;
            private int m_realTraderState = -1;
            private bool m_returning;
            private int m_traderState = -1;
            private int m_villageID = -1;
            private CustomSelfDrawPanel.CSDLabel resourceAmount = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage resourceImage = new CustomSelfDrawPanel.CSDImage();
            private int rightVillageID = -1;

            public void initIncoming(int position, int villageID, int otherVillage, int numTraders, DateTime arrivalTime, long armyID, AllArmiesPanel2 parent, bool returning)
            {
                this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, returning, -1, 0, 0);
            }

            public void initSent(int position, int villageID, int otherVillage, int numTraders, DateTime arrivalTime, long armyID, AllArmiesPanel2 parent, bool returning, int traderState, int resource, int amount)
            {
                this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, returning, traderState, resource, amount);
            }

            private void initText(int position, int villageID, int otherVillage, int numTraders, DateTime arrivalTime, long armyID, AllArmiesPanel2 parent, bool returning, int traderState, int resource, int amount)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_traderState = traderState;
                this.m_army = GameEngine.Instance.World.getTrader(armyID);
                this.m_realTraderState = this.m_army.trader.traderState;
                this.m_armyID = armyID;
                this.m_villageID = villageID;
                this.m_otherVillageID = otherVillage;
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
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
                this.lblTarget.Color = ARGBColors.Black;
                this.lblTarget.RolloverColor = ARGBColors.White;
                this.lblTarget.Position = new Point(0xe9, 0);
                this.lblTarget.Size = new Size(0xe0, this.backgroundImage.Height);
                this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
                this.backgroundImage.addControl(this.lblTarget);
                this.leftVillageID = villageID;
                this.rightVillageID = otherVillage;
                if (((traderState == 1) || (traderState == 3)) || (traderState == 6))
                {
                    this.resourceImage.Image = (Image) GFXLibrary.getCommodity32DSImage(resource);
                    this.resourceImage.Position = new Point(0x1c7, -3);
                    this.backgroundImage.addControl(this.resourceImage);
                    NumberFormatInfo nFI = GameEngine.NFI;
                    this.resourceAmount.Text = amount.ToString("N", nFI);
                    this.resourceAmount.Color = ARGBColors.Black;
                    this.resourceAmount.Position = new Point(490, 0);
                    this.resourceAmount.Size = new Size(60, this.backgroundImage.Height);
                    this.resourceAmount.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.resourceAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    this.backgroundImage.addControl(this.resourceAmount);
                }
                if (traderState >= 1)
                {
                    switch (traderState)
                    {
                        case 1:
                            this.description.Text = SK.Text("SelectArmyPanel_Delivering", "Delivering");
                            break;

                        case 2:
                        case 4:
                        case 6:
                            this.description.Text = SK.Text("SelectArmyPanel_Returning", "Returning");
                            break;

                        case 3:
                            this.description.Text = SK.Text("SelectArmyPanel_Selling", "Selling");
                            break;

                        case 5:
                            this.description.Text = SK.Text("SelectArmyPanel_Collecting", "Collecting");
                            break;
                    }
                    this.description.Color = ARGBColors.Black;
                    this.description.Position = new Point(0x23f, 0);
                    this.description.Size = new Size(240, this.backgroundImage.Height);
                    this.description.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.description.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.backgroundImage.addControl(this.description);
                }
                this.lblArrivalTime.Text = "";
                this.lblArrivalTime.Color = ARGBColors.Black;
                this.lblArrivalTime.Position = new Point(0x341, 0);
                this.lblArrivalTime.Size = new Size(0x72, this.backgroundImage.Height);
                this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblArrivalTime);
                this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
                base.invalidate();
            }

            private void lblTarget_Click()
            {
                if (this.rightVillageID >= 0)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
                }
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
                WorldMap.LocalTrader trader = GameEngine.Instance.World.getTrader(this.m_armyID);
                if (trader == null)
                {
                    return false;
                }
                DateTime time = VillageMap.getCurrentServerTime();
                TimeSpan span = (TimeSpan) (this.m_arrivalTime - time);
                int secsLeft = (int) (span.TotalSeconds + 0.5);
                if (secsLeft < 1)
                {
                    secsLeft = 0;
                }
                if (trader.trader.traderState != this.m_realTraderState)
                {
                    return false;
                }
                this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
                if (this.m_returning)
                {
                    this.lblArrivalTime.Text = this.lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
                }
                return true;
            }
        }

        public class MonkComparer : IComparer<WorldMap.LocalPerson>
        {
            public int Compare(WorldMap.LocalPerson x, WorldMap.LocalPerson y)
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
                bool flag = true;
                if (AllArmiesPanel2.sortMode == 10)
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    if ((((x.person.personType != 4) || (x.person.state <= 0)) || (x.parentPerson >= 0L)) && GameEngine.Instance.World.isUserVillage(x.person.homeVillageID))
                    {
                        flag2 = true;
                    }
                    if ((((y.person.personType != 4) || (y.person.state <= 0)) || (y.parentPerson >= 0L)) && GameEngine.Instance.World.isUserVillage(y.person.homeVillageID))
                    {
                        flag3 = true;
                    }
                    if (!flag2 && !flag3)
                    {
                        flag = false;
                    }
                    if (!flag2)
                    {
                        return 1;
                    }
                    if (!flag3)
                    {
                        return -1;
                    }
                }
                if (flag)
                {
                    switch (AllArmiesPanel2.sortMode)
                    {
                        case -13:
                        case 13:
                        {
                            int command = 0;
                            int num8 = 0;
                            if (GameEngine.Instance.World.isUserVillage(x.person.homeVillageID))
                            {
                                command = x.person.command;
                            }
                            if (GameEngine.Instance.World.isUserVillage(y.person.homeVillageID))
                            {
                                num8 = y.person.command;
                            }
                            int num9 = command.CompareTo(num8);
                            if (num9 != 0)
                            {
                                return num9;
                            }
                            break;
                        }
                        case -12:
                        case 12:
                        {
                            int num4 = 0;
                            int num5 = 0;
                            if (GameEngine.Instance.World.isUserVillage(x.person.homeVillageID))
                            {
                                num4 = x.childrenCount + 1;
                            }
                            if (GameEngine.Instance.World.isUserVillage(y.person.homeVillageID))
                            {
                                num5 = y.childrenCount + 1;
                            }
                            int num6 = num5.CompareTo(num4);
                            if (num6 == 0)
                            {
                                break;
                            }
                            return num6;
                        }
                        case -8:
                        case 8:
                        {
                            int num3 = x.serverEndTime.CompareTo(y.serverEndTime);
                            if (num3 == 0)
                            {
                                break;
                            }
                            return num3;
                        }
                        case -2:
                        case 2:
                        {
                            string str3 = GameEngine.Instance.World.getVillageName(x.person.homeVillageID);
                            string strB = GameEngine.Instance.World.getVillageName(y.person.homeVillageID);
                            int num2 = str3.CompareTo(strB);
                            if (num2 == 0)
                            {
                                break;
                            }
                            return num2;
                        }
                        case -1:
                        case 1:
                        {
                            string str = GameEngine.Instance.World.getVillageName(x.person.targetVillageID);
                            string str2 = GameEngine.Instance.World.getVillageName(y.person.targetVillageID);
                            int num = str.CompareTo(str2);
                            if (num == 0)
                            {
                                break;
                            }
                            return num;
                        }
                    }
                }
                if (x.personID > y.personID)
                {
                    return 1;
                }
                if (x.personID < y.personID)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class MonkLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCommand = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblMonks = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private int leftVillageID = -1;
            private WorldMap.LocalTrader m_army;
            private long m_armyID = -1L;
            private DateTime m_arrivalTime = DateTime.Now;
            private int m_otherVillageID = -1;
            private AllArmiesPanel2 m_parent;
            private int m_position = -1000;
            private bool m_returning;
            private int m_villageID = -1;
            private int rightVillageID = -1;

            public void initIncoming(int position, int villageID, int otherVillage, int numTraders, DateTime arrivalTime, long armyID, AllArmiesPanel2 parent)
            {
                this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, 0);
            }

            public void initSent(int position, int villageID, int otherVillage, int numTraders, DateTime arrivalTime, long armyID, AllArmiesPanel2 parent, int command)
            {
                this.initText(position, villageID, otherVillage, numTraders, arrivalTime, armyID, parent, command);
            }

            private void initText(int position, int villageID, int otherVillage, int numMonks, DateTime arrivalTime, long armyID, AllArmiesPanel2 parent, int command)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_army = GameEngine.Instance.World.getTrader(armyID);
                this.m_armyID = armyID;
                this.m_villageID = villageID;
                this.m_otherVillageID = otherVillage;
                this.m_arrivalTime = arrivalTime;
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
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
                this.lblTarget.Color = ARGBColors.Black;
                this.lblTarget.RolloverColor = ARGBColors.White;
                this.lblTarget.Position = new Point(0xe9, 0);
                this.lblTarget.Size = new Size(0xe0, this.backgroundImage.Height);
                this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
                this.backgroundImage.addControl(this.lblTarget);
                this.leftVillageID = villageID;
                this.rightVillageID = otherVillage;
                if (numMonks > 0)
                {
                    this.lblMonks.Text = numMonks.ToString();
                    this.lblMonks.Color = ARGBColors.Black;
                    this.lblMonks.Position = new Point(0x1c7, 0);
                    this.lblMonks.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblMonks.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblMonks.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblMonks);
                }
                if (command > 0)
                {
                    switch (command)
                    {
                        case 1:
                            this.lblCommand.Text = SK.Text("VillageMapPanel_Blessing", "Blessing");
                            break;

                        case 2:
                            this.lblCommand.Text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence");
                            break;

                        case 3:
                            this.lblCommand.Text = SK.Text("VillageMapPanel_Inquisition", "Inquisition");
                            break;

                        case 4:
                            this.lblCommand.Text = SK.Text("SendMonksPanel_Interdiction", "Interdiction");
                            break;

                        case 5:
                            this.lblCommand.Text = SK.Text("SendMonksPanel_Restoration", "Restoration");
                            break;

                        case 6:
                            this.lblCommand.Text = SK.Text("SendMonksPanel_Absolution", "Absolution");
                            break;

                        case 7:
                            this.lblCommand.Text = SK.Text("SendMonksPanel_Excommnunication", "Excommunication");
                            break;

                        case 8:
                            this.lblCommand.Text = SK.Text("SendMonksPanel_Negative_Influence", "Negative Influence");
                            break;
                    }
                    this.lblCommand.Color = ARGBColors.Black;
                    this.lblCommand.Position = new Point(0x20d, 0);
                    this.lblCommand.Size = new Size(300, this.backgroundImage.Height);
                    this.lblCommand.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblCommand.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.backgroundImage.addControl(this.lblCommand);
                }
                this.lblArrivalTime.Text = "";
                this.lblArrivalTime.Color = ARGBColors.Black;
                this.lblArrivalTime.Position = new Point(0x341, 0);
                this.lblArrivalTime.Size = new Size(0x72, this.backgroundImage.Height);
                this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblArrivalTime);
                this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
                base.invalidate();
            }

            private void lblTarget_Click()
            {
                if (this.rightVillageID >= 0)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
                }
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
                if (GameEngine.Instance.World.getPerson(this.m_armyID) == null)
                {
                    return false;
                }
                DateTime time = VillageMap.getCurrentServerTime();
                TimeSpan span = (TimeSpan) (this.m_arrivalTime - time);
                int secsLeft = (int) (span.TotalSeconds + 0.5);
                if (secsLeft < 1)
                {
                    secsLeft = 0;
                    return false;
                }
                this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
                if (this.m_returning)
                {
                    this.lblArrivalTime.Text = this.lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
                }
                return true;
            }
        }

        public class ReinforcementLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private int leftVillageID = -1;
            private WorldMap.LocalArmyData m_army;
            private long m_armyID = -1L;
            private DateTime m_arrivalTime = DateTime.Now;
            private bool m_moving;
            private int m_origLoot = -1;
            private int m_otherVillageID = -1;
            private AllArmiesPanel2 m_parent;
            private int m_position = -1000;
            private bool m_returning;
            private bool m_sent;
            private int m_villageID = -1;
            private int rightVillageID = -1;

            public void initIncoming(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning, bool tutorial, int attackType)
            {
                this.m_sent = false;
                this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, false, tutorial, attackType);
            }

            public void initSent(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning)
            {
                this.m_sent = true;
                this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, 0);
            }

            private void initText(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning, bool showTroops, bool tutorial, int attackType)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_army = GameEngine.Instance.World.getReinforcement(armyID);
                this.m_origLoot = this.m_army.lootType;
                this.m_armyID = armyID;
                this.m_villageID = villageID;
                this.m_otherVillageID = otherVillage;
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
                this.lblVillage.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
                this.lblVillage.Color = ARGBColors.Black;
                this.lblVillage.RolloverColor = ARGBColors.White;
                this.lblVillage.Position = new Point(9, 0);
                this.lblVillage.Size = new Size(0xdf, this.backgroundImage.Height);
                this.lblVillage.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblVillage.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
                this.lblTarget.Color = ARGBColors.Black;
                this.lblTarget.RolloverColor = ARGBColors.White;
                this.lblTarget.Position = new Point(0xe9, 0);
                this.lblTarget.Size = new Size(0xe0, this.backgroundImage.Height);
                this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
                this.backgroundImage.addControl(this.lblTarget);
                this.leftVillageID = villageID;
                this.rightVillageID = otherVillage;
                this.lblArrivalTime.Text = "";
                this.lblArrivalTime.Color = ARGBColors.Black;
                this.lblArrivalTime.Position = new Point(0x341, 0);
                this.lblArrivalTime.Size = new Size(0x72, this.backgroundImage.Height);
                this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblArrivalTime);
                this.lblPeasants.Text = numPeasants.ToString();
                this.lblPeasants.Color = ARGBColors.Black;
                this.lblPeasants.Position = new Point(0x1e5, 0);
                this.lblPeasants.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPeasants.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblPeasants);
                this.lblArchers.Text = numArchers.ToString();
                this.lblArchers.Color = ARGBColors.Black;
                this.lblArchers.Position = new Point(0x221, 0);
                this.lblArchers.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblArchers.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblArchers);
                this.lblPikemen.Text = numPikemen.ToString();
                this.lblPikemen.Color = ARGBColors.Black;
                this.lblPikemen.Position = new Point(0x25d, 0);
                this.lblPikemen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblPikemen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblPikemen);
                this.lblSwordsmen.Text = numSwordsmen.ToString();
                this.lblSwordsmen.Color = ARGBColors.Black;
                this.lblSwordsmen.Position = new Point(0x299, 0);
                this.lblSwordsmen.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblSwordsmen.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblSwordsmen);
                this.lblCatapults.Text = numCatapults.ToString();
                this.lblCatapults.Color = ARGBColors.Black;
                this.lblCatapults.Position = new Point(0x2d5, 0);
                this.lblCatapults.Size = new Size(0x37, this.backgroundImage.Height);
                this.lblCatapults.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.backgroundImage.addControl(this.lblCatapults);
                this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
                base.invalidate();
            }

            private void lblTarget_Click()
            {
                if (this.rightVillageID >= 0)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
                }
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
                if (GameEngine.Instance.World.getReinforcement(this.m_armyID) == null)
                {
                    return false;
                }
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
                    if (this.m_sent && this.m_moving)
                    {
                        this.lblArrivalTime.Text = this.lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
                    }
                }
                return true;
            }
        }

        public class ScoutLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgCarrying = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel lblArrivalTime = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblCarryingAmount = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblTarget = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblVillage = new CustomSelfDrawPanel.CSDLabel();
            private int leftVillageID = -1;
            private WorldMap.LocalArmyData m_army;
            private long m_armyID = -1L;
            private DateTime m_arrivalTime = DateTime.Now;
            private int m_origLoot = -1;
            private int m_otherVillageID = -1;
            private AllArmiesPanel2 m_parent;
            private int m_position = -1000;
            private bool m_returning;
            private int m_villageID = -1;
            private int rightVillageID = -1;

            public void initIncoming(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning, bool tutorial, int attackType)
            {
                this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, false, tutorial, attackType);
            }

            public void initSent(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning)
            {
                this.initText(position, villageID, otherVillage, numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, arrivalTime, armyID, showButton, parent, returning, true, false, 0);
            }

            private void initText(int position, int villageID, int otherVillage, int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numScouts, DateTime arrivalTime, long armyID, bool showButton, AllArmiesPanel2 parent, bool returning, bool showTroops, bool tutorial, int attackType)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_army = GameEngine.Instance.World.getArmy(armyID);
                this.m_origLoot = this.m_army.lootType;
                this.m_armyID = armyID;
                this.m_villageID = villageID;
                this.m_otherVillageID = otherVillage;
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
                this.lblVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblVillage_Click), "AllArmiesPanel2_village");
                this.backgroundImage.addControl(this.lblVillage);
                this.lblTarget.Text = GameEngine.Instance.World.getVillageNameOrType(otherVillage);
                this.lblTarget.Color = ARGBColors.Black;
                this.lblTarget.RolloverColor = ARGBColors.White;
                this.lblTarget.Position = new Point(0xe9, 0);
                this.lblTarget.Size = new Size(0xe0, this.backgroundImage.Height);
                this.lblTarget.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblTarget.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblTarget.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lblTarget_Click), "AllArmiesPanel2_targetVillage");
                this.backgroundImage.addControl(this.lblTarget);
                this.leftVillageID = villageID;
                this.rightVillageID = otherVillage;
                this.lblArrivalTime.Text = "";
                this.lblArrivalTime.Color = ARGBColors.Black;
                this.lblArrivalTime.Position = new Point(0x341, 0);
                this.lblArrivalTime.Size = new Size(0x72, this.backgroundImage.Height);
                this.lblArrivalTime.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblArrivalTime.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblArrivalTime);
                if (showTroops)
                {
                    this.lblScouts.Text = numScouts.ToString();
                    this.lblScouts.Color = ARGBColors.Black;
                    this.lblScouts.Position = new Point(0x1e5, 0);
                    this.lblScouts.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblScouts.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.backgroundImage.addControl(this.lblScouts);
                }
                if ((this.m_origLoot >= 100) && (this.m_origLoot < 0xc7))
                {
                    this.imgCarrying.Image = (Image) GFXLibrary.getCommodity32DSImage(this.m_origLoot - 100);
                    this.imgCarrying.Position = new Point(0x2b7, -3);
                    this.backgroundImage.addControl(this.imgCarrying);
                    NumberFormatInfo nFI = GameEngine.NFI;
                    this.lblCarryingAmount.Text = this.m_army.lootLevel.ToString("N", nFI);
                    this.lblCarryingAmount.Color = ARGBColors.Black;
                    this.lblCarryingAmount.Position = new Point(0x27b, 0);
                    this.lblCarryingAmount.Size = new Size(0x37, this.backgroundImage.Height);
                    this.lblCarryingAmount.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                    this.lblCarryingAmount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    this.backgroundImage.addControl(this.lblCarryingAmount);
                }
                this.update(DXTimer.GetCurrentMilliseconds() / 1000.0);
                base.invalidate();
            }

            private void lblTarget_Click()
            {
                if (this.rightVillageID >= 0)
                {
                    Point point = GameEngine.Instance.World.getVillageLocation(this.rightVillageID);
                    InterfaceMgr.Instance.changeTab(9);
                    InterfaceMgr.Instance.changeTab(0);
                    InterfaceMgr.Instance.closeParishPanel();
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.rightVillageID, false, true, true, false);
                }
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
                WorldMap.LocalArmyData data = GameEngine.Instance.World.getArmy(this.m_armyID);
                if ((data == null) || (data.lootType != this.m_origLoot))
                {
                    return false;
                }
                DateTime time = VillageMap.getCurrentServerTime();
                TimeSpan span = (TimeSpan) (this.m_arrivalTime - time);
                int secsLeft = (int) (span.TotalSeconds + 0.5);
                if (secsLeft < 1)
                {
                    secsLeft = 0;
                }
                this.lblArrivalTime.Text = VillageMap.createBuildTimeString(secsLeft);
                if (this.m_returning)
                {
                    this.lblArrivalTime.Text = this.lblArrivalTime.Text + " (" + SK.Text("AllArmiesSentLine_Return_Abbreviation", "Rtrn") + ")";
                }
                return true;
            }
        }
    }
}

