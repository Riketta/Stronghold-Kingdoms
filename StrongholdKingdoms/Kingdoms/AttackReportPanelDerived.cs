namespace Kingdoms
{
    using CommonTypes;
    using StatTracking;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class AttackReportPanelDerived : GenericReportPanelBasic
    {
        private CustomSelfDrawPanel.CSDArea areaResources = new CustomSelfDrawPanel.CSDArea();
        private ReportBattleValuesPanel attackerValuesPanel;
        private CustomSelfDrawPanel.CSDButton btnShowResources = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnViewBattle = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton btnViewResult = new CustomSelfDrawPanel.CSDButton();
        private ReportBattleValuesPanel defenderValuesPanel;
        private CustomSelfDrawPanel.CSDImage imgWheelPrize = new CustomSelfDrawPanel.CSDImage();
        private DateTime lastViewTime = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDLabel lblFlagCaptured = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblHonour = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblResult = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblSpoils = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblTargetVillageInfo = new CustomSelfDrawPanel.CSDLabel();
        private Point mapTarget = new Point(-1, -1);
        private ReportResourcePanel resourcesPanel = new ReportResourcePanel();
        private double targetZoomLevel;

        public AttackReportPanelDerived()
        {
            base.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.Size = new Size(580, 600);
        }

        private void attackerDoubleClick()
        {
            if (base.m_returnData != null)
            {
                Point point = GameEngine.Instance.World.getVillageLocation(base.m_returnData.attackingVillage);
                double targetZoom = 10000.0;
                if (point.X != -1)
                {
                    GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                    InterfaceMgr.Instance.changeTab(0);
                    GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) point.X, (double) point.Y);
                    base.m_parent.closeControl(true);
                    InterfaceMgr.Instance.reactiveMainWindow();
                    InterfaceMgr.Instance.displaySelectedVillagePanel(base.m_returnData.attackingVillage, false, true, false, false);
                }
            }
        }

        private void defenderDoubleClick()
        {
            if (base.m_returnData != null)
            {
                Point point = GameEngine.Instance.World.getVillageLocation(base.m_returnData.defendingVillage);
                double targetZoom = 10000.0;
                if (point.X != -1)
                {
                    GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                    InterfaceMgr.Instance.changeTab(0);
                    GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) point.X, (double) point.Y);
                    base.m_parent.closeControl(true);
                    InterfaceMgr.Instance.reactiveMainWindow();
                    InterfaceMgr.Instance.displaySelectedVillagePanel(base.m_returnData.defendingVillage, false, true, false, false);
                }
            }
        }

        public override void init(IDockableControl parent, Size size, object back)
        {
            base.init(parent, size, back);
            this.btnViewBattle.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnViewBattle.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnViewBattle.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnViewBattle.setSizeToImage();
            this.btnViewBattle.Position = new Point((base.Width / 2) - (this.btnViewBattle.Width / 2), base.btnClose.Y);
            this.btnViewBattle.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnViewBattle.TextYOffset = -2;
            this.btnViewBattle.Text.Color = ARGBColors.Black;
            this.btnViewBattle.Enabled = true;
            this.btnViewBattle.Visible = true;
            this.btnViewBattle.Text.Text = SK.Text("Reports_View_Battle", "View Battle");
            this.btnViewBattle.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewBattleClick), "Reports_View_Battle");
            this.btnViewResult.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnViewResult.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnViewResult.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnViewResult.setSizeToImage();
            this.btnViewResult.Position = new Point((base.Width / 2) - (this.btnViewResult.Width / 2), base.btnDelete.Y);
            this.btnViewResult.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnViewResult.TextYOffset = -2;
            this.btnViewResult.Text.Color = ARGBColors.Black;
            this.btnViewResult.Enabled = true;
            this.btnViewResult.Visible = true;
            this.btnViewResult.Text.Text = SK.Text("Reports_View_Reports", "View Result");
            this.btnViewResult.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewResultClick), "Reports_View_Result");
            this.btnShowResources.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnShowResources.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnShowResources.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnShowResources.setSizeToImage();
            this.btnShowResources.Position = new Point((base.Width / 2) - (this.btnViewResult.Width / 2), base.btnDelete.Y);
            this.btnShowResources.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnShowResources.TextYOffset = -2;
            this.btnShowResources.Text.Color = ARGBColors.Black;
            this.btnShowResources.Enabled = true;
            this.btnShowResources.Visible = false;
            this.btnShowResources.Text.Text = SK.Text("Reports_Show_Resources", "Show Resources");
            this.btnShowResources.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showResourcesClick), "Reports_Show_Resources");
            base.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
            base.btnUtility.Visible = true;
            this.lblResult.Text = "";
            this.lblResult.Color = ARGBColors.Black;
            this.lblResult.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.lblResult.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblResult.Position = new Point(0, base.lblSecondaryText.Rectangle.Bottom);
            this.lblResult.Size = new Size(base.Width, 0x1a);
            base.lblDate.Y = this.lblResult.Rectangle.Bottom;
            string header = SK.Text("GENERIC_Attackers", "Attackers");
            this.attackerValuesPanel = new ReportBattleValuesPanel(this, new Size((base.btnClose.X - base.btnForward.Rectangle.Right) - 4, 180));
            this.attackerValuesPanel.Position = new Point(base.btnForward.X, base.lblDate.Rectangle.Bottom);
            this.attackerValuesPanel.init(header, true, true);
            header = SK.Text("GENERIC_Defenders", "Defenders");
            this.defenderValuesPanel = new ReportBattleValuesPanel(this, new Size((base.btnClose.X - base.btnForward.Rectangle.Right) - 4, 180));
            this.defenderValuesPanel.Position = new Point((base.btnClose.Rectangle.Right - 2) - this.defenderValuesPanel.Width, base.lblDate.Rectangle.Bottom);
            this.defenderValuesPanel.init(header, true, false);
            this.lblSpoils.Text = "";
            this.lblSpoils.Color = ARGBColors.Black;
            this.lblSpoils.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblSpoils.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblSpoils.Position = new Point(0, this.attackerValuesPanel.Rectangle.Bottom);
            this.lblSpoils.Size = new Size(base.Width, 0x1a);
            this.lblTargetVillageInfo.Text = "";
            this.lblTargetVillageInfo.Color = ARGBColors.Black;
            this.lblTargetVillageInfo.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblTargetVillageInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblTargetVillageInfo.Position = new Point(0, this.lblSpoils.Rectangle.Bottom);
            this.lblTargetVillageInfo.Size = new Size(base.Width, 0x1a);
            this.lblHonour.Text = "";
            this.lblHonour.Color = ARGBColors.Black;
            this.lblHonour.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblHonour.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblHonour.Position = new Point(0, this.lblTargetVillageInfo.Rectangle.Bottom);
            this.lblHonour.Size = new Size(base.Width, 0x1a);
            this.lblFlagCaptured.Text = "";
            this.lblFlagCaptured.Color = ARGBColors.Black;
            this.lblFlagCaptured.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblFlagCaptured.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblFlagCaptured.Position = new Point(0, this.lblHonour.Rectangle.Bottom);
            this.lblFlagCaptured.Size = new Size(base.Width, 0x1a);
            this.resourcesPanel.Size = this.attackerValuesPanel.Size;
            this.resourcesPanel.Width *= 2;
            this.resourcesPanel.Position = this.attackerValuesPanel.Position;
            this.resourcesPanel.X = (base.Width / 2) - (this.resourcesPanel.Width / 2);
            this.resourcesPanel.init();
            this.resourcesPanel.Visible = false;
            if (base.imgBackground.Image != null)
            {
                base.imgBackground.addControl(this.attackerValuesPanel);
                base.imgBackground.addControl(this.defenderValuesPanel);
                base.imgBackground.addControl(this.btnViewBattle);
                base.imgBackground.addControl(this.btnViewResult);
                base.imgBackground.addControl(this.btnShowResources);
                base.imgBackground.addControl(this.lblResult);
                base.imgBackground.addControl(this.lblSpoils);
                base.imgBackground.addControl(this.lblTargetVillageInfo);
                base.imgBackground.addControl(this.lblHonour);
                base.imgBackground.addControl(this.lblFlagCaptured);
                base.imgBackground.addControl(this.resourcesPanel);
            }
            else
            {
                base.addControl(this.attackerValuesPanel);
                base.addControl(this.defenderValuesPanel);
                base.addControl(this.btnViewBattle);
                base.addControl(this.btnViewResult);
                base.addControl(this.btnShowResources);
                base.addControl(this.lblResult);
                base.addControl(this.lblSpoils);
                base.addControl(this.lblTargetVillageInfo);
                base.addControl(this.lblHonour);
                base.addControl(this.lblFlagCaptured);
                base.addControl(this.resourcesPanel);
            }
        }

        private void initResourceArea(GetReport_ReturnType returnData)
        {
            this.areaResources.Size = new Size(this.attackerValuesPanel.Width * 2, this.attackerValuesPanel.Height);
            this.areaResources.Position = new Point((base.Width / 2) - (this.areaResources.Width / 2), this.attackerValuesPanel.Y);
            CustomSelfDrawPanel.CSDLine control = new CustomSelfDrawPanel.CSDLine {
                Position = new Point(1, 1),
                Size = new Size(this.areaResources.Width - 2, 0)
            };
            CustomSelfDrawPanel.CSDLine line2 = new CustomSelfDrawPanel.CSDLine {
                Position = new Point(1, this.areaResources.Height - 1),
                Size = new Size(this.areaResources.Width, 0)
            };
            this.areaResources.addControl(control);
            this.areaResources.addControl(line2);
            this.areaResources.Visible = false;
        }

        private void setCapitalFlags(GetReport_ReturnType returnData, out bool fromCap, out bool toCap)
        {
            fromCap = false;
            toCap = false;
            switch (returnData.reportType)
            {
                case 0x3a:
                case 0x3b:
                case 60:
                case 0x3d:
                case 0x7b:
                case 0x7c:
                case 0x7d:
                case 1:
                case 0x18:
                case 0x19:
                    if ((returnData.attackingVillage < 0) || !GameEngine.Instance.World.isRegionCapital(returnData.attackingVillage))
                    {
                        break;
                    }
                    base.reportOwner = GameEngine.Instance.World.getParishNameFromVillageID(returnData.attackingVillage);
                    fromCap = true;
                    return;

                case 0x3e:
                case 0x3f:
                case 0x40:
                case 0x41:
                case 0x4f:
                case 3:
                    if ((returnData.defendingVillage >= 0) && GameEngine.Instance.World.isRegionCapital(returnData.defendingVillage))
                    {
                        base.reportOwner = GameEngine.Instance.World.getParishNameFromVillageID(returnData.defendingVillage);
                        toCap = true;
                    }
                    break;

                case 2:
                    break;

                default:
                    return;
            }
        }

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            this.setData(returnData, true);
        }

        private void setData(GetReport_ReturnType returnData, bool updateForwarding)
        {
            bool flag;
            bool flag2;
            if (!base.m_returnData.snapshotAvailable)
            {
                base.m_returnData.wasAlreadyRead = true;
            }
            this.lblResult.Text = SK.Text("GENERIC_The_Attacker_Wins", "The Attacker Wins");
            this.attackerValuesPanel.setData(base.m_returnData, true);
            this.defenderValuesPanel.setData(base.m_returnData, false);
            bool flag3 = false;
            this.setCapitalFlags(returnData, out flag, out flag2);
            switch (returnData.reportType)
            {
                case 1:
                    if (flag)
                    {
                        base.lblMainText.Text = base.reportOwner;
                        break;
                    }
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    break;

                case 3:
                case 0x3e:
                case 0x3f:
                case 0x40:
                case 0x41:
                case 0x4f:
                    this.lblSpoils.Text = "";
                    switch (returnData.reportType)
                    {
                        case 0x3e:
                            base.lblMainText.Text = SK.Text("GENERIC_CharacterName_Rat", "Rat");
                            break;

                        case 0x3f:
                            base.lblMainText.Text = SK.Text("GENERIC_CharacterName_Snake", "Snake");
                            break;

                        case 0x40:
                            base.lblMainText.Text = SK.Text("GENERIC_CharacterName_Pig", "Pig");
                            break;

                        case 0x41:
                            base.lblMainText.Text = SK.Text("GENERIC_CharacterName_Wolf", "Wolf");
                            break;

                        case 0x4f:
                            base.lblMainText.Text = SK.Text("GENERIC_CharacterName_The_Enemy", "The Enemy");
                            break;

                        case 3:
                            if (returnData.otherUser.Length == 0)
                            {
                                base.lblMainText.Text = SK.Text("GENERIC_An_Unknown_Player", "An Unknown Player");
                            }
                            else
                            {
                                base.lblMainText.Text = returnData.otherUser;
                            }
                            base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                            break;
                    }
                    if (returnData.genericData11 < 0)
                    {
                        this.lblHonour.Text = SK.Text("GENERIC_Honour_Cost", "Honour Cost") + " : " + returnData.genericData11.ToString("N", base.nfi);
                    }
                    else
                    {
                        this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", base.nfi);
                    }
                    base.lblSubTitle.Text = SK.Text("Reports_Attacks_Village", "Attacks");
                    if (flag2)
                    {
                        base.lblSecondaryText.Text = base.reportOwner;
                    }
                    else if (base.reportOwner.Length == 0)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
                    }
                    else
                    {
                        base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    }
                    if (returnData.successStatus)
                    {
                        this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
                    }
                    else if ((returnData.genericData20 == 0) && (returnData.genericData21 >= 0))
                    {
                        if ((returnData.genericData30 == 11) || (returnData.genericData30 == 13))
                        {
                            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Was_Attacked", "Was attacked");
                        }
                        else
                        {
                            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Captured", "Captured");
                        }
                    }
                    else if ((returnData.genericData20 == 10) && (returnData.genericData21 >= 0))
                    {
                        this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Made_a_Vassal", "Made a vassal");
                    }
                    else if ((returnData.genericData20 == 5) && (returnData.genericData21 >= 0))
                    {
                        this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Razed", "Has been razed");
                    }
                    else if (returnData.genericData20 == 6)
                    {
                        this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Raided", "Has been raided");
                        this.lblSpoils.Text = returnData.genericData21.ToString("N", base.nfi) + " " + SK.Text("GENERIC_Gold_Raided", "Gold raided");
                    }
                    else if ((returnData.genericData20 == 0) && (returnData.genericData21 == -25))
                    {
                        this.lblTargetVillageInfo.Text = SK.Text("GENERIC_Peacetime_Fail", "Attack Failed, village under Peace Time");
                    }
                    else
                    {
                        this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Was_Attacked", "Was attacked");
                    }
                    if (returnData.genericData20 == 2)
                    {
                        this.lblSpoils.Text = ((((((((returnData.genericData22 + returnData.genericData23) + returnData.genericData24) + returnData.genericData25) + returnData.genericData26) + returnData.genericData27) + returnData.genericData28) + returnData.genericData29)).ToString("N", base.nfi) + " " + SK.Text("GENERIC_Resources_Lost", "Resources lost");
                        this.btnShowResources.Visible = true;
                    }
                    else if (returnData.genericData20 > 0x3e8)
                    {
                        if (returnData.genericData22 >= 0)
                        {
                            this.lblSpoils.Text = VillageBuildingsData.getBuildingName(returnData.genericData22);
                        }
                        if (returnData.genericData23 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData23);
                        }
                        if (returnData.genericData24 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData24);
                        }
                        if (returnData.genericData25 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData25);
                        }
                        if (returnData.genericData26 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData26);
                        }
                        if (returnData.genericData27 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData27);
                        }
                        if (returnData.genericData28 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData28);
                        }
                        if (returnData.genericData29 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData29);
                        }
                        this.lblSpoils.Text = this.lblSpoils.Text + " - " + SK.Text("GENERIC_Destroyed", "Destroyed");
                    }
                    else if (returnData.genericData20 == 0x3e8)
                    {
                        this.lblSpoils.Text = SK.Text("GENERIC_You_Had_No_buildings_Destroyed", "You had no buildings that could be destroyed.");
                    }
                    else if (returnData.genericData20 == 1)
                    {
                        this.lblSpoils.Text = SK.Text("GENERIC_The_Attack_Failed", "The attack failed.");
                    }
                    goto Label_138E;

                case 0x18:
                case 0x19:
                case 0x3a:
                case 0x3b:
                case 60:
                case 0x3d:
                case 0x7b:
                case 0x7c:
                case 0x7d:
                    if (!flag)
                    {
                        base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    }
                    else
                    {
                        base.lblMainText.Text = base.reportOwner;
                    }
                    base.lblSubTitle.Text = SK.Text("Reports_Attacks_Village", "Attacks");
                    switch (returnData.reportType)
                    {
                        case 0x18:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_A_Bandit_Camp", "A Bandit Camp");
                            break;

                        case 0x19:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_A_Wolf_Lair", "A Wolf Lair");
                            break;

                        case 0x3a:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
                            break;

                        case 0x3b:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
                            break;

                        case 60:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
                            break;

                        case 0x3d:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
                            break;

                        case 0x7b:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                            break;

                        case 0x7c:
                            base.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                            break;

                        case 0x7d:
                        {
                            string[] strArray = new string[] { SK.Text("GENERIC_Treasure_Castle", "Treasure Castle"), " ", SK.Text("GENERIC_TREASURE_CASTLE_LEVEL", "Level"), " : ", (returnData.genericData31 + 1).ToString() };
                            base.lblSecondaryText.Text = string.Concat(strArray);
                            if ((returnData.genericData29 >= 100) && returnData.wasAlreadyRead)
                            {
                                this.defenderValuesPanel.addChests(returnData.genericData29 - 100);
                            }
                            break;
                        }
                    }
                    this.lblSpoils.Text = "";
                    this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", base.nfi);
                    if (!returnData.successStatus)
                    {
                        this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
                    }
                    else
                    {
                        if ((GameEngine.Instance.LocalWorldData.AIWorld && (returnData.genericData20 == 0)) && ((returnData.genericData21 >= 0) && (returnData.genericData30 == 1)))
                        {
                            this.lblTargetVillageInfo.Text = base.lblSecondaryText.Text + " - " + SK.Text("GENERIC_Captured", "Captured");
                        }
                        if (((returnData.reportType == 0x7d) && (returnData.genericData20 >= 700)) && (returnData.genericData20 < 710))
                        {
                            this.lblHonour.Text = "";
                            switch (returnData.genericData20)
                            {
                                case 700:
                                    this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins1", "Treasure Found : Tier 1 Wheel Spin");
                                    this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[0];
                                    break;

                                case 0x2bd:
                                    this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins2", "Treasure Found : Tier 2 Wheel Spin");
                                    this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[1];
                                    break;

                                case 0x2be:
                                    this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins3", "Treasure Found : Tier 3 Wheel Spin");
                                    this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[2];
                                    break;

                                case 0x2bf:
                                    this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins4", "Treasure Found : Tier 4 Wheel Spin");
                                    this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[3];
                                    break;

                                case 0x2c0:
                                    this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins5", "Treasure Found : Tier 5 Wheel Spin");
                                    this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[4];
                                    break;
                            }
                            this.imgWheelPrize.Position = new Point(0xe1, 430);
                            if (base.imgBackground.Image != null)
                            {
                                base.imgBackground.addControl(this.imgWheelPrize);
                            }
                            else
                            {
                                base.addControl(this.imgWheelPrize);
                            }
                        }
                    }
                    if ((returnData.genericData21 == 1) && ((returnData.genericData20 < 700) || (returnData.genericData20 >= 710)))
                    {
                        if (!returnData.wasAlreadyRead)
                        {
                            this.viewResultFunction(false);
                            return;
                        }
                        flag3 = true;
                    }
                    goto Label_138E;

                default:
                    goto Label_138E;
            }
            base.lblSubTitle.Text = SK.Text("Reports_Attacks_Village", "Attacks");
            if (returnData.otherUser.Length == 0)
            {
                if (returnData.defendingVillage < 0)
                {
                    base.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
                }
                else
                {
                    base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                }
            }
            else
            {
                base.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
            }
            this.lblSpoils.Text = "";
            if (!returnData.successStatus)
            {
                this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
            }
            else if ((returnData.genericData20 == 0) && (returnData.genericData21 >= 0))
            {
                if ((returnData.genericData30 == 11) || (returnData.genericData30 == 13))
                {
                    this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Successfully_Attacked", "Successfully attacked");
                }
                else
                {
                    this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Captured", "Captured");
                }
            }
            else if ((returnData.genericData20 == 10) && (returnData.genericData21 >= 0))
            {
                this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Made_a_Vassal", "Made a vassal");
            }
            else if ((returnData.genericData20 == 5) && (returnData.genericData21 >= 0))
            {
                this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Razed", "Has been razed");
            }
            else if (returnData.genericData20 == 6)
            {
                this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Raided", "Has been raided");
                this.lblSpoils.Text = returnData.genericData21.ToString("N", base.nfi) + " " + SK.Text("GENERIC_Gold_Raided", "Gold raided");
            }
            else if ((returnData.genericData20 == 0) && (returnData.genericData21 == -25))
            {
                this.lblTargetVillageInfo.Text = SK.Text("GENERIC_Peacetime_Fail", "Attack Failed, village under Peace Time");
            }
            else
            {
                this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Successfully_Attacked", "Successfully attacked");
            }
            if (returnData.genericData11 < 0)
            {
                this.lblHonour.Text = SK.Text("GENERIC_Honour_Cost", "Honour Cost") + " : " + returnData.genericData11.ToString("N", base.nfi);
            }
            else
            {
                this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", base.nfi);
            }
            if (returnData.genericData20 != 0)
            {
                if (returnData.genericData20 == 2)
                {
                    this.lblSpoils.Text = ((((((((returnData.genericData22 + returnData.genericData23) + returnData.genericData24) + returnData.genericData25) + returnData.genericData26) + returnData.genericData27) + returnData.genericData28) + returnData.genericData29)).ToString("N", base.nfi) + " " + SK.Text("GENERIC_Resources_Taken", "Resources taken");
                    this.btnShowResources.Visible = true;
                }
                else if ((returnData.genericData20 < 500) || (returnData.genericData20 >= 0x3e8))
                {
                    if (returnData.genericData20 > 0x3e8)
                    {
                        if (returnData.genericData22 >= 0)
                        {
                            this.lblSpoils.Text = VillageBuildingsData.getBuildingName(returnData.genericData22);
                        }
                        if (returnData.genericData23 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData23);
                        }
                        if (returnData.genericData24 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData24);
                        }
                        if (returnData.genericData25 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData25);
                        }
                        if (returnData.genericData26 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData26);
                        }
                        if (returnData.genericData27 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData27);
                        }
                        if (returnData.genericData28 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData28);
                        }
                        if (returnData.genericData29 >= 0)
                        {
                            this.lblSpoils.Text = this.lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData29);
                        }
                        this.lblSpoils.Text = this.lblSpoils.Text + " - " + SK.Text("GENERIC_Destroyed", "Destroyed");
                    }
                    else if (returnData.genericData20 == 0x3e8)
                    {
                        this.lblSpoils.Text = SK.Text("GENERIC_No_Destroyable_Buildings", "There were no destroyable buildings.");
                    }
                    else if (returnData.genericData20 == 1)
                    {
                        this.lblSpoils.Text = SK.Text("GENERIC_Attack_Failed", "This attack failed.");
                    }
                }
            }
        Label_138E:
            if (returnData.defendingVillage >= 0)
            {
                this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
                this.targetZoomLevel = 10000.0;
                base.btnUtility.Visible = true;
            }
            else
            {
                base.btnUtility.Visible = false;
            }
            if (!returnData.wasAlreadyRead)
            {
                this.lblResult.Text = "";
                this.btnShowResources.Visible = false;
                this.lblSpoils.Text = "";
                this.lblTargetVillageInfo.Text = "";
                this.lblHonour.Text = "";
                this.btnViewResult.Visible = true;
                this.imgWheelPrize.Visible = false;
            }
            else
            {
                this.btnViewResult.Visible = false;
                this.imgWheelPrize.Visible = true;
            }
            if (!returnData.snapshotAvailable)
            {
                this.btnViewBattle.Visible = false;
            }
            else
            {
                this.btnViewBattle.Visible = true;
            }
            if (flag3)
            {
                this.btnViewBattle.Visible = false;
                this.btnViewResult.Visible = false;
                this.lblHonour.Visible = false;
                this.lblSpoils.Visible = false;
                switch (returnData.reportType)
                {
                    case 0x18:
                        this.lblTargetVillageInfo.Text = SK.Text("Reports_Bandit_Camp_Cleared", "The Bandit Camp had already been cleared.");
                        break;

                    case 0x19:
                        this.lblTargetVillageInfo.Text = SK.Text("Reports_Wolf_Lair_Cleared", "The Wolf Lair had already been cleared.");
                        break;

                    case 0x3a:
                    case 0x3b:
                    case 60:
                    case 0x3d:
                    case 0x7b:
                    case 0x7c:
                    case 0x7d:
                        this.lblTargetVillageInfo.Text = SK.Text("Reports_Castle_Cleared", "The Castle had already been cleared.");
                        break;
                }
                if (returnData.genericData31 >= 0x2710)
                {
                    this.lblFlagCaptured.Visible = true;
                }
            }
        }

        private void showResourcesClick()
        {
            GameEngine.Instance.playInterfaceSound("AttackReportPanel_show_resources");
            this.resourcesPanel.Visible = !this.resourcesPanel.Visible;
            this.attackerValuesPanel.Visible = !this.attackerValuesPanel.Visible;
            this.defenderValuesPanel.Visible = !this.defenderValuesPanel.Visible;
            if (this.resourcesPanel.Visible)
            {
                this.btnShowResources.Text.Text = SK.Text("Reports_Hide_Resources", "Hide Resources");
                this.resourcesPanel.setData(base.m_returnData);
            }
            else
            {
                this.btnShowResources.Text.Text = SK.Text("Reports_Show_Resources", "Show Resources");
            }
        }

        protected override void utilityClick()
        {
            if (this.mapTarget.X != -1)
            {
                GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                InterfaceMgr.Instance.changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
                base.m_parent.closeControl(true);
                InterfaceMgr.Instance.reactiveMainWindow();
            }
        }

        private void viewBattle(ViewBattle_ReturnType returnData)
        {
            InterfaceMgr.bgdBlurEnabled = false;
            base.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
            int campMode = 0;
            if (base.m_returnData.reportType == 0x18)
            {
                campMode = 1;
            }
            else if (base.m_returnData.reportType == 0x19)
            {
                campMode = 2;
            }
            int pillageInfo = -1;
            int ransackCount = -1;
            int raidCount = -1;
            switch (base.m_returnData.genericData30)
            {
                case 2:
                case 4:
                case 5:
                case 6:
                case 7:
                    pillageInfo = base.m_returnData.genericData31;
                    if (pillageInfo > 0x270f)
                    {
                        pillageInfo -= 0x2710;
                    }
                    break;

                case 3:
                    ransackCount = base.m_returnData.genericData31;
                    if (ransackCount > 0x270f)
                    {
                        ransackCount -= 0x2710;
                    }
                    break;

                case 12:
                    raidCount = base.m_returnData.genericData31;
                    if (raidCount > 0x270f)
                    {
                        raidCount -= 0x2710;
                    }
                    break;
            }
            Sound.playBattleMusic();
            GameEngine.Instance.InitBattle(returnData.castleMapSnapshot, returnData.damageMapSnapshot, returnData.castleTroopsSnapshot, returnData.attackMapSnapshot, returnData.keepLevel, returnData.defenderResearchData, returnData.attackerResearchData, campMode, pillageInfo, ransackCount, raidCount, base.m_returnData.genericData30, base.m_returnData.defendingVillage, base.m_returnData, returnData.landType);
        }

        private void viewBattleCallback(ViewBattle_ReturnType returnData)
        {
            if (returnData.Success && (base.m_returnData != null))
            {
                InterfaceMgr.Instance.setReportAlreadyRead(base.m_returnData.reportID);
                InterfaceMgr.Instance.setReportData(returnData, base.m_returnData.reportID);
                this.viewBattle(returnData);
            }
            else
            {
                base.m_returnData.snapshotAvailable = false;
            }
        }

        private void viewBattleClick()
        {
            GameEngine.Instance.playInterfaceSound("AttackReportPanel_view_battle");
            ViewBattle_ReturnType returnData = (ViewBattle_ReturnType) InterfaceMgr.Instance.getReportData(base.reportID);
            if (returnData == null)
            {
                RemoteServices.Instance.set_ViewBattle_UserCallBack(new RemoteServices.ViewBattle_UserCallBack(this.viewBattleCallback));
                RemoteServices.Instance.ViewBattle(base.reportID);
            }
            else
            {
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - this.lastViewTime);
                if (span.TotalSeconds > 2.0)
                {
                    this.lastViewTime = now;
                    StatTrackingClient.Instance().ActivateTrigger(5, null);
                    this.viewBattle(returnData);
                }
            }
        }

        private void viewResultClick()
        {
            this.viewResultFunction(true);
        }

        private void viewResultFunction(bool playSound)
        {
            if (playSound)
            {
                if (base.m_returnData.successStatus)
                {
                    GameEngine.Instance.playInterfaceSound("AttackReportPanel_view_result_win");
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("AttackReportPanel_view_result_lose");
                }
            }
            base.m_returnData.wasAlreadyRead = true;
            this.setData(base.m_returnData, false);
            InterfaceMgr.Instance.setReportAlreadyRead(base.m_returnData.reportID);
        }

        private class ReportResourcePanel : CustomSelfDrawPanel.CSDArea
        {
            private CustomSelfDrawPanel.CSDImage imgResource1 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgResource2 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgResource3 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgResource4 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgResource5 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgResource6 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgResource7 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage imgResource8 = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel lblHeader = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource1 = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource2 = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource3 = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource4 = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource5 = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource6 = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource7 = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblResource8 = new CustomSelfDrawPanel.CSDLabel();

            public void init()
            {
                this.lblHeader.Text = SK.Text("GENERIC_Resources", "Resources");
                this.lblHeader.Color = ARGBColors.Black;
                this.lblHeader.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.lblHeader.Position = new Point(0, 0);
                this.lblHeader.Size = new Size(base.Width, 0x18);
                this.imgResource1.Image = (Image) GFXLibrary.com_32_wood_DS;
                this.imgResource1.setSizeToImage();
                this.imgResource1.Position = new Point(((base.Width / 2) - 80) - this.imgResource1.Width, this.lblHeader.Rectangle.Bottom + 5);
                this.imgResource2.Size = this.imgResource1.Size;
                this.imgResource2.Position = new Point((base.Width / 2) + 80, this.lblHeader.Rectangle.Bottom + 5);
                this.imgResource3.Size = this.imgResource1.Size;
                this.imgResource3.Position = new Point(this.imgResource1.X, this.imgResource1.Rectangle.Bottom + 2);
                this.imgResource4.Size = this.imgResource1.Size;
                this.imgResource4.Position = new Point(this.imgResource2.X, this.imgResource2.Rectangle.Bottom + 2);
                this.imgResource5.Size = this.imgResource1.Size;
                this.imgResource5.Position = new Point(this.imgResource1.X, this.imgResource3.Rectangle.Bottom + 2);
                this.imgResource6.Size = this.imgResource1.Size;
                this.imgResource6.Position = new Point(this.imgResource2.X, this.imgResource4.Rectangle.Bottom + 2);
                this.imgResource7.Size = this.imgResource1.Size;
                this.imgResource7.Position = new Point(this.imgResource1.X, this.imgResource5.Rectangle.Bottom + 2);
                this.imgResource8.Size = this.imgResource1.Size;
                this.imgResource8.Position = new Point(this.imgResource2.X, this.imgResource6.Rectangle.Bottom + 2);
                this.lblResource1.Text = "";
                this.lblResource1.Color = ARGBColors.Black;
                this.lblResource1.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblResource1.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource1.Y);
                this.lblResource1.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                this.lblResource2.Text = "";
                this.lblResource2.Color = ARGBColors.Black;
                this.lblResource2.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.lblResource2.Position = new Point(base.Width / 2, this.imgResource2.Y);
                this.lblResource2.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                this.lblResource3.Text = "";
                this.lblResource3.Color = ARGBColors.Black;
                this.lblResource3.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblResource3.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource3.Y);
                this.lblResource3.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                this.lblResource4.Text = "";
                this.lblResource4.Color = ARGBColors.Black;
                this.lblResource4.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.lblResource4.Position = new Point(base.Width / 2, this.imgResource4.Y);
                this.lblResource4.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                this.lblResource5.Text = "";
                this.lblResource5.Color = ARGBColors.Black;
                this.lblResource5.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblResource5.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource5.Y);
                this.lblResource5.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                this.lblResource6.Text = "";
                this.lblResource6.Color = ARGBColors.Black;
                this.lblResource6.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.lblResource6.Position = new Point(base.Width / 2, this.imgResource6.Y);
                this.lblResource6.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                this.lblResource7.Text = "";
                this.lblResource7.Color = ARGBColors.Black;
                this.lblResource7.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.lblResource7.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource7.Y);
                this.lblResource7.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                this.lblResource8.Text = "";
                this.lblResource8.Color = ARGBColors.Black;
                this.lblResource8.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.lblResource8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.lblResource8.Position = new Point(base.Width / 2, this.imgResource8.Y);
                this.lblResource8.Size = new Size((base.Width / 2) - this.lblResource1.X, this.imgResource1.Height);
                base.addControl(this.lblHeader);
                base.addControl(this.imgResource1);
                base.addControl(this.lblResource1);
                base.addControl(this.imgResource2);
                base.addControl(this.lblResource2);
                base.addControl(this.imgResource3);
                base.addControl(this.lblResource3);
                base.addControl(this.imgResource4);
                base.addControl(this.lblResource4);
                base.addControl(this.imgResource5);
                base.addControl(this.lblResource5);
                base.addControl(this.imgResource6);
                base.addControl(this.lblResource6);
                base.addControl(this.imgResource7);
                base.addControl(this.lblResource7);
                base.addControl(this.imgResource8);
                base.addControl(this.lblResource8);
                foreach (CustomSelfDrawPanel.CSDControl control in base.Controls)
                {
                    control.Visible = false;
                }
            }

            public void setData(GetReport_ReturnType data)
            {
                this.lblHeader.Visible = true;
                switch (data.genericData30)
                {
                    case 2:
                        this.imgResource1.Image = (Image) GFXLibrary.com_32_wood_DS;
                        this.lblResource1.Text = data.genericData22.ToString();
                        this.lblResource1.Visible = true;
                        this.imgResource1.Visible = true;
                        this.imgResource2.Image = (Image) GFXLibrary.com_32_stone_DS;
                        this.lblResource2.Text = data.genericData23.ToString();
                        this.lblResource2.Visible = true;
                        this.imgResource2.Visible = true;
                        this.imgResource3.Image = (Image) GFXLibrary.com_32_iron_DS;
                        this.lblResource3.Text = data.genericData24.ToString();
                        this.lblResource3.Visible = true;
                        this.imgResource3.Visible = true;
                        this.imgResource4.Image = (Image) GFXLibrary.com_32_pitch_DS;
                        this.lblResource4.Text = data.genericData25.ToString();
                        this.lblResource4.Visible = true;
                        this.imgResource4.Visible = true;
                        return;

                    case 3:
                        break;

                    case 4:
                        this.imgResource1.Image = (Image) GFXLibrary.com_32_apples_DS;
                        this.lblResource1.Text = data.genericData22.ToString();
                        this.imgResource1.Visible = true;
                        this.lblResource1.Visible = true;
                        this.imgResource2.Image = (Image) GFXLibrary.com_32_bread_DS;
                        this.lblResource2.Text = data.genericData23.ToString();
                        this.imgResource2.Visible = true;
                        this.lblResource2.Visible = true;
                        this.imgResource3.Image = (Image) GFXLibrary.com_32_cheese_DS;
                        this.lblResource3.Text = data.genericData24.ToString();
                        this.imgResource3.Visible = true;
                        this.lblResource3.Visible = true;
                        this.imgResource4.Image = (Image) GFXLibrary.com_32_meat_DS;
                        this.lblResource4.Text = data.genericData25.ToString();
                        this.imgResource4.Visible = true;
                        this.lblResource4.Visible = true;
                        this.imgResource5.Image = (Image) GFXLibrary.com_32_fish_DS;
                        this.lblResource5.Text = data.genericData26.ToString();
                        this.imgResource5.Visible = true;
                        this.lblResource5.Visible = true;
                        this.imgResource6.Image = (Image) GFXLibrary.com_32_veg_DS;
                        this.lblResource6.Text = data.genericData27.ToString();
                        this.imgResource6.Visible = true;
                        this.lblResource6.Visible = true;
                        return;

                    case 5:
                        this.imgResource1.Image = (Image) GFXLibrary.com_32_furniture_DS;
                        this.lblResource1.Text = data.genericData22.ToString();
                        this.imgResource1.Visible = true;
                        this.lblResource1.Visible = true;
                        this.imgResource2.Image = (Image) GFXLibrary.com_32_clothes_DS;
                        this.lblResource2.Text = data.genericData23.ToString();
                        this.imgResource2.Visible = true;
                        this.lblResource2.Visible = true;
                        this.imgResource3.Image = (Image) GFXLibrary.com_32_venison_DS;
                        this.lblResource3.Text = data.genericData24.ToString();
                        this.imgResource3.Visible = true;
                        this.lblResource3.Visible = true;
                        this.imgResource4.Image = (Image) GFXLibrary.com_32_wine_DS;
                        this.lblResource4.Text = data.genericData25.ToString();
                        this.imgResource4.Visible = true;
                        this.lblResource4.Visible = true;
                        this.imgResource5.Image = (Image) GFXLibrary.com_32_salt_DS;
                        this.lblResource5.Text = data.genericData26.ToString();
                        this.imgResource5.Visible = true;
                        this.lblResource5.Visible = true;
                        this.imgResource6.Image = (Image) GFXLibrary.com_32_metalware_DS;
                        this.lblResource6.Text = data.genericData27.ToString();
                        this.imgResource6.Visible = true;
                        this.lblResource6.Visible = true;
                        this.imgResource7.Image = (Image) GFXLibrary.com_32_spices_DS;
                        this.lblResource7.Text = data.genericData28.ToString();
                        this.imgResource7.Visible = true;
                        this.lblResource7.Visible = true;
                        this.imgResource8.Image = (Image) GFXLibrary.com_32_silk_DS;
                        this.lblResource8.Text = data.genericData29.ToString();
                        this.imgResource8.Visible = true;
                        this.lblResource8.Visible = true;
                        return;

                    case 6:
                        this.imgResource1.Image = (Image) GFXLibrary.com_32_ale_DS;
                        this.lblResource1.Text = data.genericData22.ToString();
                        this.imgResource1.Visible = true;
                        this.lblResource1.Visible = true;
                        return;

                    case 7:
                        this.imgResource1.Image = (Image) GFXLibrary.com_32_bows_DS;
                        this.lblResource1.Text = data.genericData22.ToString();
                        this.imgResource1.Visible = true;
                        this.lblResource1.Visible = true;
                        this.imgResource2.Image = (Image) GFXLibrary.com_32_pikes_DS;
                        this.lblResource2.Text = data.genericData23.ToString();
                        this.imgResource2.Visible = true;
                        this.lblResource2.Visible = true;
                        this.imgResource3.Image = (Image) GFXLibrary.com_32_swords_DS;
                        this.lblResource3.Text = data.genericData24.ToString();
                        this.imgResource3.Visible = true;
                        this.lblResource3.Visible = true;
                        this.imgResource4.Image = (Image) GFXLibrary.com_32_armour_DS;
                        this.lblResource4.Text = data.genericData25.ToString();
                        this.imgResource4.Visible = true;
                        this.lblResource4.Visible = true;
                        this.imgResource5.Image = (Image) GFXLibrary.com_32_catapults_DS;
                        this.lblResource5.Text = data.genericData26.ToString();
                        this.imgResource5.Visible = true;
                        this.lblResource5.Visible = true;
                        break;

                    default:
                        return;
                }
            }
        }
    }
}

