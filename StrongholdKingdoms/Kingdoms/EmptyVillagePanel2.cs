namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class EmptyVillagePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton attackButton_AI = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.MRHP_Background backGround_AI = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.MRHP_Background backGround_Charter = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.MRHP_Background backGround_Enemy = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.MRHP_Background backGround_Resources = new CustomSelfDrawPanel.MRHP_Background();
        private CustomSelfDrawPanel.CSDButton buyVillageButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castleButton_AI = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel charterLabel = new CustomSelfDrawPanel.CSDLabel();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDImage goldImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel goldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage honourImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel honourLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel invasionLabel = new CustomSelfDrawPanel.CSDLabel();
        private WorldMap.SpecialVillageCache lastData;
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDButton scoutButton_AI = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton scoutButton_Resources = new CustomSelfDrawPanel.CSDButton();
        private bool special;
        private CustomSelfDrawPanel.CSDImage travelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel travelTimeDescLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel travelTimeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel treasureCastleTimeoutLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool wasAiShort;
        private bool wasTall = true;
        private CustomSelfDrawPanel.WikiLinkControl wikiLink;

        public EmptyVillagePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private void btnAttack_Click()
        {
            if (InterfaceMgr.Instance.SelectedVillage >= 0)
            {
                int selectedVillage = InterfaceMgr.Instance.SelectedVillage;
                GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, selectedVillage);
            }
        }

        private void btnBuyVillage_Click()
        {
            if (this.buyVillageButton.Active)
            {
                if (GameEngine.Instance.World.canUserOwnMoreVillages() && GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    int selectedVillage = InterfaceMgr.Instance.SelectedVillage;
                    if (selectedVillage >= 0)
                    {
                        double villageGoldCost = GameEngine.Instance.LocalWorldData.villageGoldCost;
                        double num3 = GameEngine.Instance.World.calcVillageDistance(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage) * GameEngine.Instance.LocalWorldData.villageCostDistanceMultiplier;
                        villageGoldCost *= num3 + 1.0;
                        if (GameEngine.Instance.World.getCurrentGold() >= villageGoldCost)
                        {
                            int num4 = 0;
                            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
                            {
                                num4 = ResearchData.getVillageBuyHonourCost(GameEngine.Instance.World.numVillagesOwned());
                                if (((num4 > 0) && GameEngine.Instance.World.FourthAgeWorld) && (GameEngine.Instance.World.numVillagesOwned() < GameEngine.Instance.World.MostAge4Villages))
                                {
                                    num4 = 0;
                                }
                            }
                            if ((num4 <= 0) || (GameEngine.Instance.World.getCurrentHonour() >= num4))
                            {
                                InterfaceMgr.Instance.openBuyVillageWindow(selectedVillage, true);
                            }
                            else
                            {
                                MyMessageBox.Show(SK.Text("EmptyVillagePanel_Not_Enough_Honour", "Not enough honour"), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
                            }
                        }
                        else
                        {
                            MyMessageBox.Show(SK.Text("EmptyVillagePanel_Not_Enough_Gold", "Not enough gold"), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
                        }
                    }
                    else
                    {
                        MyMessageBox.Show(SK.Text("EmptyVillagePanel_Not_Enough_Gold", "Not enough gold"), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
                    }
                }
                else
                {
                    MyMessageBox.Show(SK.Text("EmptyVillagePanel_No_More_Villages", "You cannot own more villages."), SK.Text("EmptyVillagePanel_Buy_Village_Error", "Buy Village Error"));
                }
            }
        }

        private void btnScout_Click()
        {
            if (InterfaceMgr.Instance.SelectedVillage >= 0)
            {
                InterfaceMgr.Instance.openScoutPopupWindow(InterfaceMgr.Instance.SelectedVillage, true);
            }
        }

        private void castleClick()
        {
            RemoteServices.Instance.set_ViewCastle_UserCallBack(new RemoteServices.ViewCastle_UserCallBack(this.viewCastleCallback));
            RemoteServices.Instance.ViewCastle_Village(InterfaceMgr.Instance.SelectedVillage);
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
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

        public void forceDisable()
        {
            this.buyVillageButton.Enabled = false;
            this.attackButton_AI.Enabled = false;
            this.scoutButton_AI.Enabled = false;
            this.scoutButton_Resources.Enabled = false;
        }

        public void init(int villageID)
        {
            if (!GameEngine.Instance.LocalWorldData.AIWorld)
            {
                if (this.wasAiShort)
                {
                    this.wasAiShort = false;
                    base.Size = new Size(0xc7, 0x111);
                }
            }
            else
            {
                switch (GameEngine.Instance.World.getSpecial(villageID))
                {
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                        this.wasAiShort = true;
                        base.Size = new Size(0xc7, 0xd5);
                        goto Label_00A8;
                }
                this.wasAiShort = false;
                base.Size = new Size(0xc7, 0x111);
            }
        Label_00A8:
            this.wasTall = this.isTallTreasureChestPanel(villageID);
            int num = 0;
            if (this.wasTall)
            {
                num = 60;
            }
            base.clearControls();
            CustomSelfDrawPanel.CSDImage image = this.backGround_AI.init(this.wasTall, 0x2710);
            base.addControl(this.backGround_AI);
            this.attackButton_AI.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
            this.attackButton_AI.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
            this.attackButton_AI.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
            this.attackButton_AI.Position = new Point(0x40, 0x4f + num);
            this.attackButton_AI.CustomTooltipID = 0x96b;
            this.attackButton_AI.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "EmptyVillagePanel2_attack");
            image.addControl(this.attackButton_AI);
            this.scoutButton_AI.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.scoutButton_AI.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
            this.scoutButton_AI.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x11];
            this.scoutButton_AI.Position = new Point(0x63, 0x4f + num);
            this.scoutButton_AI.CustomTooltipID = 0x96c;
            this.scoutButton_AI.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "EmptyVillagePanel2_scout");
            image.addControl(this.scoutButton_AI);
            this.castleButton_AI.ImageNorm = (Image) GFXLibrary.mrhp_reports;
            this.castleButton_AI.OverBrighten = true;
            this.castleButton_AI.MoveOnClick = true;
            this.castleButton_AI.Position = new Point(80, 0x2f + num);
            this.castleButton_AI.CustomTooltipID = 0x98d;
            this.castleButton_AI.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castleClick), "EmptyVillagePanel2_castle");
            image.addControl(this.castleButton_AI);
            this.treasureCastleTimeoutLabel.Text = "";
            this.treasureCastleTimeoutLabel.Color = ARGBColors.Black;
            this.treasureCastleTimeoutLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.treasureCastleTimeoutLabel.Position = new Point(10, 50);
            this.treasureCastleTimeoutLabel.Size = new Size(image.Width - 20, 80);
            this.treasureCastleTimeoutLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.treasureCastleTimeoutLabel.Visible = false;
            image.addControl(this.treasureCastleTimeoutLabel);
            if (!this.wasTall)
            {
                image.Image = (Image) GFXLibrary.mrhp_world_panel_132;
            }
            image = this.backGround_Enemy.init(false, 0x2710);
            base.addControl(this.backGround_Enemy);
            this.backGround_Enemy.hideBackground();
            this.invasionLabel.Text = "";
            this.invasionLabel.Color = ARGBColors.Black;
            this.invasionLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.invasionLabel.Position = new Point(0x39, 0x21);
            this.invasionLabel.Size = new Size(this.backGround_Enemy.Width - 20, 80);
            this.invasionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.invasionLabel.Visible = false;
            this.backGround_Enemy.addControl(this.invasionLabel);
            image = this.backGround_Resources.init(false, 0x2710);
            base.addControl(this.backGround_Resources);
            this.scoutButton_Resources.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
            this.scoutButton_Resources.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
            this.scoutButton_Resources.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0x11];
            this.scoutButton_Resources.Position = new Point(80, 0x31);
            this.scoutButton_Resources.Enabled = false;
            this.scoutButton_Resources.CustomTooltipID = 0x98b;
            this.scoutButton_Resources.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnScout_Click), "EmptyVillagePanel2_scout_stash");
            image.addControl(this.scoutButton_Resources);
            image = this.backGround_Charter.init(true, 0x2710);
            base.addControl(this.backGround_Charter);
            this.charterLabel.Text = SK.Text("EmptyVillagePanel_Cost", "Cost to found this village");
            this.charterLabel.Color = ARGBColors.Black;
            this.charterLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.charterLabel.Position = new Point(0, 0x2a);
            this.charterLabel.Size = new Size(image.Width, 40);
            this.charterLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            image.addControl(this.charterLabel);
            this.goldImage.Image = (Image) GFXLibrary.com_32_money;
            this.goldImage.Position = new Point(0x69, 0x3a);
            image.addControl(this.goldImage);
            this.goldLabel.Text = "0,000,000";
            this.goldLabel.Color = ARGBColors.Black;
            this.goldLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.goldLabel.Position = new Point(0, 0x42);
            this.goldLabel.Size = new Size(100, 40);
            this.goldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            image.addControl(this.goldLabel);
            this.honourImage.Image = (Image) GFXLibrary.com_32_honour;
            this.honourImage.Position = new Point(0x69, 0x62);
            image.addControl(this.honourImage);
            this.honourLabel.Text = "0,000,000";
            this.honourLabel.Color = ARGBColors.Black;
            this.honourLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.honourLabel.Position = new Point(0, 0x6a);
            this.honourLabel.Size = new Size(100, 40);
            this.honourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            image.addControl(this.honourLabel);
            this.travelTimeDescLabel.Text = SK.Text("EmptyVillagePanel_TravelTime", "Time to reach this Charter");
            this.travelTimeDescLabel.Color = ARGBColors.Black;
            this.travelTimeDescLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.travelTimeDescLabel.Position = new Point(0, 0x94);
            this.travelTimeDescLabel.Size = new Size(image.Width, 40);
            this.travelTimeDescLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            image.addControl(this.travelTimeDescLabel);
            this.travelTimeLabel.Text = "0:00";
            this.travelTimeLabel.Color = ARGBColors.Black;
            this.travelTimeLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.travelTimeLabel.Position = new Point(0, 0xb1);
            this.travelTimeLabel.Size = new Size(100, 40);
            this.travelTimeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            image.addControl(this.travelTimeLabel);
            this.travelImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x20];
            this.travelImage.Position = new Point(0x69, 0xa1);
            image.addControl(this.travelImage);
            this.buyVillageButton.ImageNorm = (Image) GFXLibrary.mrhp_button_150x25[0];
            this.buyVillageButton.ImageOver = (Image) GFXLibrary.mrhp_button_150x25[1];
            this.buyVillageButton.ImageClick = (Image) GFXLibrary.mrhp_button_150x25[2];
            this.buyVillageButton.Position = new Point(0x1a, 0xd7);
            this.buyVillageButton.Text.Text = SK.Text("EmptyVillagePanel_Buy_Village", "Purchase");
            this.buyVillageButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.buyVillageButton.TextYOffset = -3;
            this.buyVillageButton.Text.Color = ARGBColors.Black;
            this.buyVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnBuyVillage_Click), "EmptyVillagePanel2_buy_village");
            image.addControl(this.buyVillageButton);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "EmptyVillagePanel2";
            base.Size = new Size(0xc7, 0x111);
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

        private bool isTallTreasureChestPanel(int villageID)
        {
            if ((GameEngine.Instance.World.isSpecial(villageID) && GameEngine.Instance.World.isAttackableSpecial(villageID)) && SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(villageID)))
            {
                TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime());
                int num2 = WorldMap.TreasureCastle_AttackGap;
                if (span.TotalSeconds < num2)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void update()
        {
            this.backGround_AI.update();
            this.backGround_Charter.update();
            this.backGround_Enemy.update();
            this.backGround_Resources.update();
            this.buyVillageButton.CustomTooltipID = 0;
            if ((GameEngine.Instance.World.canUserOwnMoreVillages() && GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage())) && !GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
            {
                if (!this.buyVillageButton.Active)
                {
                    this.buyVillageButton.invalidate();
                }
                this.buyVillageButton.Active = true;
                this.buyVillageButton.Alpha = 1f;
            }
            else
            {
                if (this.buyVillageButton.Active)
                {
                    this.buyVillageButton.invalidate();
                }
                this.buyVillageButton.Active = false;
                this.buyVillageButton.Alpha = 0.2f;
                if (!GameEngine.Instance.World.canUserOwnMoreVillages())
                {
                    if ((GameEngine.Instance.World.numVillagesAllowed() <= 1) && ((GameEngine.Instance.World.getRank() + 1) < 12))
                    {
                        this.buyVillageButton.CustomTooltipID = 0x9ca;
                    }
                    else
                    {
                        this.buyVillageButton.CustomTooltipID = 0x9c8;
                    }
                }
                else if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()) || GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.getSelectedMenuVillage()))
                {
                    this.buyVillageButton.CustomTooltipID = 0x9c9;
                }
            }
            this.updateTreasureCastleTimeout();
            if (this.special)
            {
                WorldMap.SpecialVillageCache specialData = GameEngine.Instance.World.getSpecialVillageData(this.m_selectedVillage, false);
                if (this.lastData != specialData)
                {
                    this.updateSpecialData(specialData);
                }
            }
        }

        public void updateEmptyVillageText(int selectedVillage)
        {
            bool flag = false;
            bool flag2 = false;
            if (GameEngine.Instance.World.isSpecial(selectedVillage) && GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
            {
                bool flag3 = this.isTallTreasureChestPanel(selectedVillage);
                if (flag3 != this.wasTall)
                {
                    this.init(selectedVillage);
                    flag = true;
                }
                flag2 = flag3;
            }
            if (!flag && GameEngine.Instance.LocalWorldData.AIWorld)
            {
                bool flag4 = false;
                switch (GameEngine.Instance.World.getSpecial(selectedVillage))
                {
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                        flag4 = true;
                        break;

                    default:
                        flag4 = false;
                        break;
                }
                if (flag4 != this.wasAiShort)
                {
                    this.init(selectedVillage);
                }
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            this.m_selectedVillage = selectedVillage;
            this.buyVillageButton.Enabled = true;
            this.attackButton_AI.Enabled = true;
            this.scoutButton_AI.Enabled = true;
            this.scoutButton_Resources.Enabled = true;
            this.treasureCastleTimeoutLabel.Visible = false;
            this.backGround_AI.Visible = false;
            this.backGround_Enemy.Visible = false;
            this.backGround_Resources.Visible = false;
            this.backGround_Charter.Visible = false;
            this.special = false;
            this.invasionLabel.Visible = false;
            this.backGround_AI.removeWikiLink(this.wikiLink);
            this.wikiLink = null;
            int type = GameEngine.Instance.World.getSpecial(selectedVillage);
            if (SpecialVillageTypes.IS_TREASURE_CASTLE(type))
            {
                this.wikiLink = this.backGround_AI.addWikiLink(0x31);
            }
            else if ((type == 15) || (type == 0x11))
            {
                this.wikiLink = this.backGround_AI.addWikiLink(50);
            }
            if (!GameEngine.Instance.World.isSpecial(selectedVillage))
            {
                this.backGround_Charter.Visible = true;
                this.backGround_Charter.updateHeading(SK.Text("EmptyVillagePanel_Available_Village", "New Village Charter"));
                this.backGround_Charter.updatePanelTypeFromVillageID(selectedVillage);
                this.backGround_Charter.stretchBackground();
                base.Parent.Invalidate();
                double villageGoldCost = GameEngine.Instance.LocalWorldData.villageGoldCost;
                double num4 = GameEngine.Instance.World.calcVillageDistance(InterfaceMgr.Instance.getSelectedMenuVillage(), selectedVillage) * GameEngine.Instance.LocalWorldData.villageCostDistanceMultiplier;
                villageGoldCost *= num4 + 1.0;
                int numOwnedVillages = GameEngine.Instance.World.numVillagesOwned();
                int num6 = (int) villageGoldCost;
                num6 *= numOwnedVillages;
                villageGoldCost = num6;
                this.goldLabel.Text = ((int) villageGoldCost).ToString("N", nFI);
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                Point point = GameEngine.Instance.World.getVillageLocation(InterfaceMgr.Instance.OwnSelectedVillage);
                Point point2 = GameEngine.Instance.World.getVillageLocation(selectedVillage);
                double d = ((point.X - point2.X) * (point.X - point2.X)) + ((point.Y - point2.Y) * (point.Y - point2.Y));
                d = Math.Sqrt(d) * ((localWorldData.CaptainsMoveSpeed * localWorldData.gamePlaySpeed) * ResearchData.CaptainTimes[GameEngine.Instance.World.UserResearchData.Research_Courtiers]);
                d *= CardTypes.getArmySpeed(GameEngine.Instance.World.UserCardData);
                string str = VillageMap.createBuildTimeString((int) d);
                this.travelTimeLabel.Text = str;
                this.travelTimeLabel.CustomTooltipID = 0x4e20;
                this.travelTimeLabel.CustomTooltipData = (int) d;
                int num8 = 0;
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
                {
                    num8 = ResearchData.getVillageBuyHonourCost(numOwnedVillages);
                    if (((num8 > 0) && GameEngine.Instance.World.FourthAgeWorld) && (numOwnedVillages < GameEngine.Instance.World.MostAge4Villages))
                    {
                        num8 = 0;
                    }
                }
                if (num8 > 0)
                {
                    this.honourImage.Visible = true;
                    this.honourLabel.Visible = true;
                    this.honourLabel.Text = num8.ToString("N", nFI);
                }
                else
                {
                    this.honourImage.Visible = false;
                    this.honourLabel.Visible = false;
                }
            }
            else
            {
                this.special = true;
                if (GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
                {
                    this.backGround_AI.Visible = true;
                    this.backGround_AI.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
                    this.backGround_AI.updatePanelTypeFromVillageID(selectedVillage);
                    if (SpecialVillageTypes.IS_TREASURE_CASTLE(type))
                    {
                        if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                        {
                            this.attackButton_AI.Enabled = false;
                        }
                        if (flag2)
                        {
                            this.updateTreasureCastleTimeout();
                            this.treasureCastleTimeoutLabel.Visible = true;
                            this.attackButton_AI.Enabled = false;
                        }
                    }
                }
                else if ((type >= 100) && (type <= 0xc7))
                {
                    this.backGround_Resources.Visible = true;
                    this.backGround_Resources.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
                    this.backGround_Resources.updatePanelTypeFromVillageID(selectedVillage);
                    this.scoutButton_Resources.Enabled = false;
                }
                else
                {
                    this.backGround_Enemy.Visible = true;
                    if (type == 30)
                    {
                        switch (GameEngine.Instance.World.getAIInvasionMarkerState(selectedVillage))
                        {
                            case 0:
                                this.backGround_Enemy.updateHeading(SK.Text("Invasion_None", "No Invasion Sighted"));
                                break;

                            case 1:
                            {
                                this.backGround_Enemy.updateHeading(SK.Text("Invasion_Planned", "Invasion Sighted"));
                                DateTime time = GameEngine.Instance.World.getNextAIInvasionDate(selectedVillage);
                                if (time != DateTime.MinValue)
                                {
                                    TimeSpan span = (TimeSpan) (time - VillageMap.getCurrentServerTime());
                                    this.invasionLabel.Visible = true;
                                    this.invasionLabel.Text = VillageMap.createBuildTimeString((int) span.TotalSeconds);
                                }
                                break;
                            }
                            case 2:
                                this.backGround_Enemy.updateHeading(SK.Text("Invasion_Inprogress", "Invasion In Progress"));
                                break;
                        }
                    }
                    else
                    {
                        this.backGround_Enemy.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
                    }
                    this.backGround_Enemy.updatePanelTypeFromVillageID(selectedVillage);
                }
                if (!GameEngine.Instance.World.isScoutableSpecial(selectedVillage) || GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                {
                    this.scoutButton_AI.Enabled = false;
                    this.scoutButton_Resources.Enabled = false;
                }
                else
                {
                    this.scoutButton_AI.Enabled = true;
                    this.scoutButton_Resources.Enabled = true;
                }
            }
        }

        public void updateSpecialData(WorldMap.SpecialVillageCache specialData)
        {
            string subHeading = "";
            this.lastData = specialData;
            if (((specialData != null) && (specialData.resourceType > 0)) && (specialData.resourceLevel > 0))
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                subHeading = specialData.resourceLevel.ToString("N", nFI);
            }
            if (subHeading.Length > 0)
            {
                this.backGround_Resources.updateSubHeading(subHeading);
            }
            else
            {
                this.backGround_Resources.updateSubHeading("");
                this.scoutButton_Resources.Enabled = false;
            }
            if (!GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
            {
                this.scoutButton_Resources.Enabled = true;
            }
            else
            {
                this.scoutButton_Resources.Enabled = false;
            }
        }

        private void updateTreasureCastleTimeout()
        {
            if (GameEngine.Instance.World.isSpecial(this.m_selectedVillage) && GameEngine.Instance.World.isAttackableSpecial(this.m_selectedVillage))
            {
                TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime());
                int num = WorldMap.TreasureCastle_AttackGap;
                if (span.TotalSeconds < num)
                {
                    this.treasureCastleTimeoutLabel.TextDiffOnly = SK.Text("EmptyVillage_NextAttackAvailable", "Next Attack Available in") + " " + VillageMap.createBuildTimeString(num - ((int) span.TotalSeconds));
                }
                else
                {
                    this.treasureCastleTimeoutLabel.TextDiffOnly = "";
                    if (this.treasureCastleTimeoutLabel.Visible && !GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                    {
                        this.attackButton_AI.Enabled = true;
                    }
                }
            }
        }

        public void viewCastleCallback(ViewCastle_ReturnType returnData)
        {
            if (returnData.Success)
            {
                int num = GameEngine.Instance.World.getSpecial(InterfaceMgr.Instance.SelectedVillage);
                this.closeControl(true);
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
                int villageID = returnData.villageID;
                int campMode = 0;
                switch (num)
                {
                    case 3:
                        campMode = 1;
                        villageID = -2;
                        break;

                    case 5:
                        campMode = 2;
                        villageID = -3;
                        break;
                }
                GameEngine.Instance.InitCastleView(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, campMode, returnData.defencesLevel, villageID, returnData.landType);
                CastleMapBattlePanel2.fromWorld();
                InterfaceMgr.Instance.castleBattleTimes(returnData.lastCastleTime, returnData.lastTroopTime);
            }
        }
    }
}

