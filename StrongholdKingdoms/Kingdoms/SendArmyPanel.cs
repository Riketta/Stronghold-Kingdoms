namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class SendArmyPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton actionButton_Capture = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton_GoldRaid = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton_Pillage = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton_Ransack = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton_Raze = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton_Vandalise = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage arrowImage = new CustomSelfDrawPanel.CSDImage();
        private int attackTypeRef;
        private CustomSelfDrawPanel.CSDImage backgroundBottomEdge = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundRightEdge = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage buttonIndentImage = new CustomSelfDrawPanel.CSDImage();
        private bool capitalToCapital;
        private CustomSelfDrawPanel.CSDImage captureCostImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel captureCostLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel captureCostValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private int currentCommand = -1;
        private int currentPillageType;
        private CustomSelfDrawPanel.CSDLabel errorLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage gfxImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage honourPenaltyImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel honourPenaltyLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel honourPenaltyValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private DateTime lastLaunchTime = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDButton launchButton = new CustomSelfDrawPanel.CSDButton();
        private BattleHonourData m_battleHonourData;
        private bool m_captureAllowed = true;
        private int m_captureHonourCost;
        private int m_fromVillage = -1;
        private CastleMapAttackerSetupPanel m_parent;
        private int m_selectedPenalty;
        private int m_toVillage = -1;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private int maxGoldRaidValue = 1;
        private int maxPillageValue = 1;
        private int maxRansackValue = 1;
        private CustomSelfDrawPanel.CSDLabel needCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool noCaptain;
        private int pillageValueRef;
        private CustomSelfDrawPanel.CSDButton sliderButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel sliderHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDTrackBar sliderImage = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDLabel sliderValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private double storedPreCardDistance;
        private CustomSelfDrawPanel.CSDImage targetImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton targetVillageFavourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel targetVillageLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel timeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage titleImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool toCapital;
        private CustomSelfDrawPanel.CSDLabel tooltipLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel villageActionLabel = new CustomSelfDrawPanel.CSDLabel();

        public SendArmyPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void Attack()
        {
            GameEngine.Instance.CastleAttackerSetup.setupLaunchArmy(this.attackTypeRef, this.pillageValueRef, 0);
            GameEngine.Instance.CastleAttackerSetup.launchArmy();
            this.m_parent.launched();
            GameEngine.Instance.EnableMouseClicks();
            InterfaceMgr.Instance.closeLaunchAttackPopup();
        }

        public void changeCommand()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                this.updateButtons(data);
            }
        }

        private void closeClick()
        {
            GameEngine.Instance.EnableMouseClicks();
            InterfaceMgr.Instance.closeLaunchAttackPopup();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int parentFromVillage, int fromVillageID, int toVillageID, string villageName, double distance, BattleHonourData honourData, bool gotCaptain, CastleMapAttackerSetupPanel parent)
        {
            this.m_fromVillage = parentFromVillage;
            this.m_toVillage = toVillageID;
            this.m_parent = parent;
            this.m_battleHonourData = honourData;
            this.m_selectedPenalty = 0;
            this.toCapital = false;
            this.m_captureHonourCost = 0;
            base.clearControls();
            int y = 0x27;
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), base.Size);
            this.mainBackgroundImage.Position = new Point(0, y);
            this.mainBackgroundImage.Size = new Size(base.Size.Width, base.Size.Height - y);
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.backgroundBottomEdge.Image = (Image) GFXLibrary.popup_border_bottom;
            this.backgroundBottomEdge.Position = new Point(0, base.Height - 2);
            base.addControl(this.backgroundBottomEdge);
            this.backgroundRightEdge.Image = (Image) GFXLibrary.popup_border_rhs;
            this.backgroundRightEdge.Position = new Point(base.Width - 2, y);
            base.addControl(this.backgroundRightEdge);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x293, 5);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendArmyPanel_close");
            this.titleImage.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.titleImage, 0x21, new Point(0x261, 5));
            this.cardbar.Position = new Point(0, 4);
            this.mainBackgroundImage.addControl(this.cardbar);
            this.cardbar.init(6);
            this.gfxImage.Image = (Image) GFXLibrary.send_army_illustration;
            this.gfxImage.Position = new Point(0x19, 0x4d);
            this.mainBackgroundImage.addControl(this.gfxImage);
            this.targetVillageLabel.Text = villageName;
            this.targetVillageLabel.Color = ARGBColors.White;
            this.targetVillageLabel.DropShadowColor = ARGBColors.Black;
            this.targetVillageLabel.Position = new Point(5, 10);
            this.targetVillageLabel.Size = new Size(((this.gfxImage.Width - 10) - 14) - 20, 0x20);
            this.targetVillageLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.targetVillageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.gfxImage.addControl(this.targetVillageLabel);
            if (AttackTargetsPanel.isFavourite(toVillageID))
            {
                this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_1;
                this.targetVillageFavourite.CustomTooltipID = 0x83b;
            }
            else
            {
                this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_3;
                this.targetVillageFavourite.CustomTooltipID = 0x7e2;
            }
            this.targetVillageFavourite.OverBrighten = true;
            this.targetVillageFavourite.Position = new Point((this.gfxImage.Width - 20) - 0x10, 10);
            this.targetVillageFavourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.targetVillageFavourite.Data = 0;
            this.gfxImage.addControl(this.targetVillageFavourite);
            this.sliderImage.Position = new Point(0x111, 0x130);
            this.sliderImage.Margin = new Rectangle(90, 70, 0x13, 0x19);
            this.sliderImage.Value = 0;
            this.sliderImage.Max = 10;
            this.sliderImage.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
            this.mainBackgroundImage.addControl(this.sliderImage);
            this.sliderImage.Create((Image) GFXLibrary.send_army_slider, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar);
            this.sliderValueLabel.Text = "100%";
            this.sliderValueLabel.Color = ARGBColors.White;
            this.sliderValueLabel.DropShadowColor = ARGBColors.Black;
            this.sliderValueLabel.Position = new Point(11, 0x41);
            this.sliderValueLabel.Size = new Size(0x40, 0x20);
            this.sliderValueLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.sliderValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.sliderImage.addControl(this.sliderValueLabel);
            this.sliderHeaderLabel.Text = "";
            this.sliderHeaderLabel.Color = ARGBColors.White;
            this.sliderHeaderLabel.DropShadowColor = ARGBColors.Black;
            this.sliderHeaderLabel.Position = new Point(0x3f, 15);
            this.sliderHeaderLabel.Size = new Size(0x87, 0x20);
            this.sliderHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.sliderHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.sliderImage.addControl(this.sliderHeaderLabel);
            this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x18];
            this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[0x18];
            this.sliderButton.Position = new Point(-5, -8);
            this.sliderButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sliderClick), "SendArmyPanel_change_type");
            this.sliderImage.addControl(this.sliderButton);
            this.arrowImage.Image = (Image) GFXLibrary.send_army_timer;
            this.arrowImage.Position = new Point(0x21, 0x130);
            this.mainBackgroundImage.addControl(this.arrowImage);
            this.buttonIndentImage.Image = (Image) GFXLibrary.monk_screen_buttongroup_inset;
            this.buttonIndentImage.Position = new Point(0x1f7, 0x4d);
            this.mainBackgroundImage.addControl(this.buttonIndentImage);
            this.villageActionLabel.Text = "";
            this.villageActionLabel.Color = ARGBColors.White;
            this.villageActionLabel.DropShadowColor = ARGBColors.Black;
            this.villageActionLabel.Position = new Point(0x1f, 0xf3);
            this.villageActionLabel.Size = new Size(340, 30);
            this.villageActionLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.villageActionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.villageActionLabel);
            this.tooltipLabel.Text = "";
            this.tooltipLabel.Color = ARGBColors.White;
            this.tooltipLabel.DropShadowColor = ARGBColors.Black;
            this.tooltipLabel.Position = new Point(0x1f, 0x10a);
            this.tooltipLabel.Size = new Size(340, 60);
            this.tooltipLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tooltipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.tooltipLabel);
            this.honourPenaltyLabel.Text = SK.Text("LaunchAttackPopup_Honour_Penalty", "Honour Penalty");
            this.honourPenaltyLabel.Color = ARGBColors.White;
            this.honourPenaltyLabel.DropShadowColor = ARGBColors.Black;
            this.honourPenaltyLabel.Position = new Point(270, 0xf7);
            this.honourPenaltyLabel.Size = new Size(180, 60);
            this.honourPenaltyLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.honourPenaltyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.honourPenaltyLabel);
            this.honourPenaltyValueLabel.Text = "0,000,000";
            this.honourPenaltyValueLabel.Color = Color.FromArgb(0x12, 0xff, 0);
            this.honourPenaltyValueLabel.DropShadowColor = ARGBColors.Black;
            this.honourPenaltyValueLabel.Position = new Point(270, 0x10b);
            this.honourPenaltyValueLabel.Size = new Size(180, 60);
            this.honourPenaltyValueLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.honourPenaltyValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.honourPenaltyValueLabel);
            this.honourPenaltyImage.Image = (Image) GFXLibrary.com_32_honour;
            this.honourPenaltyImage.Position = new Point(450, 0xf7);
            base.addControl(this.honourPenaltyImage);
            this.captureCostLabel.Text = SK.Text("LaunchAttackPopup_Honour_Capture", "Capture Cost");
            this.captureCostLabel.Color = ARGBColors.White;
            this.captureCostLabel.DropShadowColor = ARGBColors.Black;
            this.captureCostLabel.Position = new Point(270, 0x11f);
            this.captureCostLabel.Size = new Size(180, 60);
            this.captureCostLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.captureCostLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.captureCostLabel);
            this.captureCostValueLabel.Text = "10,000,000";
            this.captureCostValueLabel.Color = Color.FromArgb(0x12, 0xff, 0);
            this.captureCostValueLabel.DropShadowColor = ARGBColors.Black;
            this.captureCostValueLabel.Position = new Point(270, 0x133);
            this.captureCostValueLabel.Size = new Size(180, 60);
            this.captureCostValueLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.captureCostValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.captureCostValueLabel);
            this.captureCostImage.Image = (Image) GFXLibrary.com_32_honour;
            this.captureCostImage.Position = new Point(450, 0x11f + y);
            base.addControl(this.captureCostImage);
            this.needCaptainLabel.Text = SK.Text("LaunchAttackPopup_Need_Captain", "Need Captain");
            this.needCaptainLabel.Color = ARGBColors.White;
            this.needCaptainLabel.DropShadowColor = ARGBColors.Black;
            this.needCaptainLabel.Position = new Point(500, 0x166);
            this.needCaptainLabel.Size = new Size(180, 0x20);
            this.needCaptainLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.needCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.needCaptainLabel.Visible = false;
            this.mainBackgroundImage.addControl(this.needCaptainLabel);
            this.storedPreCardDistance = distance;
            distance *= CardTypes.getArmySpeed(GameEngine.Instance.World.UserCardData);
            string str = VillageMap.createBuildTimeString((int) distance);
            this.timeLabel.Text = str;
            this.timeLabel.Color = ARGBColors.White;
            this.timeLabel.DropShadowColor = ARGBColors.Black;
            this.timeLabel.Position = new Point(0, 0x17);
            this.timeLabel.Size = new Size(0xbf, 0x18);
            this.timeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.timeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.arrowImage.addControl(this.timeLabel);
            this.errorLabel.Text = "Error Message Here";
            this.errorLabel.Color = ARGBColors.White;
            this.errorLabel.DropShadowColor = ARGBColors.Black;
            this.errorLabel.Position = new Point(0, 0x19b);
            this.errorLabel.Size = new Size(this.mainBackgroundImage.Width, 0x20);
            this.errorLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.errorLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundImage.addControl(this.errorLabel);
            this.actionButton_GoldRaid.Enabled = false;
            bool flag = true;
            bool flag2 = true;
            this.updateButtons(-1);
            this.actionButton_Vandalise.Position = new Point(10, 12);
            this.actionButton_Vandalise.Data = 0;
            this.actionButton_Vandalise.CustomTooltipID = 0x834;
            this.actionButton_Vandalise.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_vandalise");
            this.buttonIndentImage.addControl(this.actionButton_Vandalise);
            this.actionButton_Pillage.Position = new Point(0x54, 12);
            this.actionButton_Pillage.Data = 1;
            this.actionButton_Pillage.CustomTooltipID = 0x836;
            this.actionButton_Pillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_pillage");
            this.buttonIndentImage.addControl(this.actionButton_Pillage);
            this.actionButton_Ransack.Position = new Point(10, 0x63);
            this.actionButton_Ransack.Data = 2;
            this.actionButton_Ransack.CustomTooltipID = 0x837;
            this.actionButton_Ransack.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_ransack");
            this.buttonIndentImage.addControl(this.actionButton_Ransack);
            this.actionButton_Raze.Position = new Point(0x54, 0x63);
            this.actionButton_Raze.Data = 3;
            this.actionButton_Raze.CustomTooltipID = 0x838;
            this.actionButton_Raze.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_raze");
            this.buttonIndentImage.addControl(this.actionButton_Raze);
            this.actionButton_Capture.Position = new Point(10, 0xba);
            this.actionButton_Capture.Data = 4;
            this.actionButton_Capture.CustomTooltipID = 0x835;
            this.actionButton_Capture.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_capture");
            this.buttonIndentImage.addControl(this.actionButton_Capture);
            this.actionButton_GoldRaid.Position = new Point(0x54, 0xba);
            this.actionButton_GoldRaid.Data = 5;
            this.actionButton_GoldRaid.CustomTooltipID = 0x839;
            this.actionButton_GoldRaid.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendArmyPanel_command_gold_raid");
            this.buttonIndentImage.addControl(this.actionButton_GoldRaid);
            int index = 0;
            int type = GameEngine.Instance.World.getSpecial(toVillageID);
            switch (type)
            {
                case 3:
                case 4:
                    index = 0x18;
                    break;

                case 5:
                case 6:
                    index = 0x19;
                    break;

                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    index = 0x1c;
                    break;

                case 15:
                case 0x10:
                case 0x11:
                case 0x12:
                    index = 0x35;
                    break;

                case 40:
                case 0x29:
                case 0x2a:
                case 0x2b:
                case 0x2c:
                case 0x2d:
                case 0x2e:
                case 0x2f:
                case 0x30:
                case 0x31:
                case 50:
                    index = 0x36;
                    break;

                case 0x33:
                case 0x34:
                case 0x35:
                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                case 0x3a:
                case 0x3b:
                case 60:
                    index = 0x37;
                    break;

                case 0x3d:
                case 0x3e:
                case 0x3f:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 70:
                    index = 0x38;
                    break;

                case 0x47:
                case 0x48:
                case 0x49:
                case 0x4a:
                case 0x4b:
                case 0x4c:
                case 0x4d:
                case 0x4e:
                case 0x4f:
                case 80:
                    index = 0x39;
                    break;

                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x59:
                case 90:
                    index = 0x3a;
                    break;

                default:
                    if (GameEngine.Instance.World.isRegionCapital(toVillageID))
                    {
                        index = 0x31;
                    }
                    else if (GameEngine.Instance.World.isCountyCapital(toVillageID))
                    {
                        index = 50;
                    }
                    else if (GameEngine.Instance.World.isProvinceCapital(toVillageID))
                    {
                        index = 0x33;
                    }
                    else if (GameEngine.Instance.World.isCountryCapital(toVillageID))
                    {
                        index = 0x34;
                    }
                    else
                    {
                        index = GameEngine.Instance.World.getVillageSize(toVillageID);
                    }
                    break;
            }
            this.targetImage.Image = (Image) GFXLibrary.scout_screen_icons[index];
            this.targetImage.Position = new Point(0x8f, 15);
            this.arrowImage.addControl(this.targetImage);
            this.maxPillageValue = ResearchData.pillageLevels[GameEngine.Instance.World.UserResearchData.Research_Pillaging];
            this.maxRansackValue = ResearchData.ransackLevels[GameEngine.Instance.World.UserResearchData.Research_Ransack];
            this.maxGoldRaidValue = 50;
            this.launchButton.ImageNorm = (Image) GFXLibrary.button_with_inset_normal;
            this.launchButton.ImageOver = (Image) GFXLibrary.button_with_inset_over;
            this.launchButton.ImageClick = (Image) GFXLibrary.button_with_inset_pushed;
            this.launchButton.Position = new Point(520, 0x179);
            this.launchButton.Text.Text = SK.Text("ScoutPopup_Go", "Go");
            this.launchButton.Text.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
            this.launchButton.TextYOffset = 1;
            this.launchButton.Text.Color = ARGBColors.Black;
            this.launchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launch), "SendArmyPanel_launch");
            this.launchButton.Enabled = false;
            this.mainBackgroundImage.addControl(this.launchButton);
            bool flag3 = false;
            int num4 = GameEngine.Instance.World.getRank();
            if ((GameEngine.Instance.World.isCapital(fromVillageID) && GameEngine.Instance.World.isSpecial(toVillageID)) && SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(toVillageID)))
            {
                flag = false;
                this.actionButton_Capture.Visible = false;
                this.actionButton_Pillage.Visible = false;
                this.actionButton_Ransack.Visible = false;
                flag2 = false;
                this.actionButton_Raze.Visible = false;
                this.actionButton_GoldRaid.Visible = false;
                this.launchButton.Enabled = false;
                flag3 = true;
            }
            else if (GameEngine.Instance.World.isCapital(toVillageID))
            {
                this.toCapital = true;
                this.actionButton_Capture.Enabled = false;
                this.actionButton_Pillage.Enabled = false;
                this.actionButton_Ransack.Enabled = false;
                this.actionButton_Raze.Enabled = false;
                flag2 = false;
                if (GameEngine.Instance.World.isCapital(fromVillageID))
                {
                    this.capitalToCapital = true;
                    this.actionButton_GoldRaid.Enabled = true;
                }
                else
                {
                    this.actionButton_GoldRaid.Enabled = false;
                }
            }
            else
            {
                if (GameEngine.Instance.World.canUserOwnMoreVillages() && !GameEngine.Instance.World.isUserVillage(toVillageID))
                {
                    this.actionButton_Capture.Enabled = true;
                    NumberFormatInfo nFI = GameEngine.NFI;
                    if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1)
                    {
                        this.m_captureHonourCost = ResearchData.getVillageBuyHonourCost(GameEngine.Instance.World.numVillagesOwned());
                        if (((this.m_captureHonourCost > 0) && GameEngine.Instance.World.FourthAgeWorld) && (GameEngine.Instance.World.numVillagesOwned() < GameEngine.Instance.World.MostAge4Villages))
                        {
                            this.m_captureHonourCost = 0;
                        }
                    }
                    this.captureCostValueLabel.Text = this.m_captureHonourCost.ToString("N", nFI);
                }
                else
                {
                    this.actionButton_Capture.Enabled = false;
                }
                if (((GameEngine.Instance.World.getCurrentHonour() > 0.0) && ((GameEngine.Instance.World.getVillageUserID(toVillageID) >= 0) || (GameEngine.Instance.LocalWorldData.AIWorld && GameEngine.Instance.World.isSpecialAIPlayer(toVillageID)))) && (num4 >= (GameEngine.Instance.LocalWorldData.RazeMinRank - 1)))
                {
                    this.actionButton_Raze.Enabled = true;
                }
                else
                {
                    this.actionButton_Raze.Enabled = false;
                }
                if (GameEngine.Instance.World.isCapital(fromVillageID))
                {
                    flag = false;
                    this.actionButton_Capture.Visible = false;
                    this.actionButton_Pillage.Visible = false;
                    this.actionButton_Ransack.Visible = false;
                    flag2 = false;
                    this.actionButton_Raze.Visible = false;
                    this.actionButton_GoldRaid.Visible = false;
                    this.launchButton.Enabled = true;
                    this.actionButton_Vandalise.CustomTooltipID = 0x83a;
                    this.updateButtons(0);
                }
                else if (GameEngine.Instance.LocalWorldData.AIWorld && GameEngine.Instance.World.isSpecialAIPlayer(toVillageID))
                {
                    this.actionButton_Pillage.Visible = false;
                    this.actionButton_Ransack.Visible = false;
                    this.actionButton_GoldRaid.Visible = false;
                    this.actionButton_Raze.Visible = false;
                    this.actionButton_Vandalise.CustomTooltipID = 0x83a;
                    this.actionButton_Capture.Position = new Point(0x54, 12);
                }
                else if (!GameEngine.Instance.World.isSpecial(toVillageID) && (GameEngine.Instance.World.getVillageUserID(toVillageID) >= 0))
                {
                    if (GameEngine.Instance.World.UserResearchData.Research_Ransack == 0)
                    {
                        this.actionButton_Ransack.Enabled = false;
                    }
                    else
                    {
                        this.actionButton_Ransack.Enabled = true;
                    }
                }
                else
                {
                    flag = false;
                    this.actionButton_Capture.Visible = false;
                    this.actionButton_Pillage.Visible = false;
                    this.actionButton_Ransack.Visible = false;
                    flag2 = false;
                    this.actionButton_Raze.Visible = false;
                    this.actionButton_GoldRaid.Visible = false;
                    this.launchButton.Enabled = true;
                    this.actionButton_Vandalise.CustomTooltipID = 0x83a;
                    this.updateButtons(0);
                }
                if (parentFromVillage != fromVillageID)
                {
                    flag = false;
                    this.actionButton_Capture.Visible = false;
                    flag2 = false;
                    this.actionButton_Raze.Visible = false;
                }
            }
            if ((flag2 || (flag && !this.capitalToCapital)) && !gotCaptain)
            {
                this.noCaptain = true;
            }
            this.titleImage.Image = (Image) GFXLibrary.popup_title_bar;
            this.titleImage.Position = new Point(0, 0);
            base.addControl(this.titleImage);
            this.titleLabel.Text = SK.Text("GENERIC_Launch_Attack", "Launch Attack");
            this.titleLabel.Color = ARGBColors.White;
            this.titleLabel.DropShadowColor = ARGBColors.Black;
            this.titleLabel.Position = new Point(20, 5);
            this.titleLabel.Size = new Size(base.Width, 0x20);
            this.titleLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.titleImage.addControl(this.titleLabel);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x293, 5);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendArmyPanel_close");
            this.titleImage.addControl(this.closeButton);
            if (flag3)
            {
                this.errorLabel.Visible = true;
                this.errorLabel.Text = SK.Text("LaunchAttackPopup_Not_Attack_TC_From_Capitals", "You cannot attack Treasure Castles from Capitals.");
            }
            else if ((type >= 100) && (type <= 0xc7))
            {
                this.errorLabel.Text = SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Stash", "No Honour will be received, the stash is out of range.");
            }
            else if (type == 5)
            {
                this.errorLabel.Text = SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Wolf", "No Honour will be received, the Wolf Lair is out of range.");
            }
            else if (type == 3)
            {
                this.errorLabel.Text = SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Bandit", "No Honour will be received, the Bandit Camp is out of range.");
            }
            else if (((type == 7) || (type == 9)) || ((type == 11) || (type == 13)))
            {
                this.errorLabel.Text = SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_AI", "No Honour will be received, the AI Castle is out of range.");
            }
            else
            {
                this.errorLabel.Text = SK.Text("LaunchAttackPopup_No_Honour_Out_Of_Range_Village", "No Honour will be received, the village is out of range.");
            }
            this.errorLabel.Visible = GameEngine.Instance.World.isScoutHonourOutOfRange(fromVillageID, toVillageID);
            if (((type == 15) || (type == 0x11)) || SpecialVillageTypes.IS_TREASURE_CASTLE(type))
            {
                this.errorLabel.Visible = true;
                this.errorLabel.Text = SK.Text("LaunchAttackPopup_Paladin_No_Honour", "No honour will be received for destroying this type of AI castle");
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void launch()
        {
            int num = 0;
            int num2 = 0;
            int captureHonourCost = 0;
            if (this.currentCommand == 4)
            {
                if (this.m_captureAllowed)
                {
                    num = 1;
                    captureHonourCost = this.m_captureHonourCost;
                }
                else
                {
                    num = 11;
                }
            }
            if ((this.currentCommand == 5) && this.capitalToCapital)
            {
                num = 12;
                num2 = this.sliderImage.Value + 1;
            }
            if (this.currentCommand == 3)
            {
                num = 9;
            }
            if (this.currentCommand == 1)
            {
                switch (this.currentPillageType)
                {
                    case 1:
                        num = 4;
                        break;

                    case 2:
                        num = 6;
                        break;

                    case 3:
                        num = 5;
                        break;

                    case 4:
                        num = 7;
                        break;

                    default:
                        num = 2;
                        break;
                }
                num2 = this.sliderImage.Value + 1;
            }
            if (this.currentCommand == 2)
            {
                num = 3;
                num2 = this.sliderImage.Value + 1;
            }
            if (this.currentCommand == 0)
            {
                num = 11;
            }
            this.attackTypeRef = num;
            this.pillageValueRef = num2;
            if ((this.m_selectedPenalty > 0) || (captureHonourCost > 0))
            {
                MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                NumberFormatInfo nFI = GameEngine.NFI;
                string[] strArray = new string[] { SK.Text("LaunchAttackPopup_Penalty_Warning", "This attack will cost you an Honour Penalty."), Environment.NewLine, SK.Text("GENERIC_Honour_Cost", "Honour Cost"), " : ", (this.m_selectedPenalty + captureHonourCost).ToString("N", nFI), Environment.NewLine, SK.Text("LaunchAttackPopup_Continue", "Continue?") };
                if (MyMessageBox.Show(string.Concat(strArray), SK.Text("LaunchAttackPopup_Confirm_Attack", "Confirm Attack"), yesNo) != DialogResult.Yes)
                {
                    return;
                }
            }
            this.Attack();
        }

        private void showHonourPenalty(int penalty)
        {
            if (!this.toCapital)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                this.honourPenaltyValueLabel.Text = penalty.ToString("N", nFI);
                this.m_selectedPenalty = penalty;
                this.honourPenaltyValueLabel.Visible = true;
                this.honourPenaltyLabel.Visible = true;
                this.honourPenaltyImage.Visible = true;
                this.honourPenaltyValueLabel.Color = Color.FromArgb(0x12, 0xff, 0);
                if ((penalty > 0) && (GameEngine.Instance.World.getCurrentHonour() <= 0.0))
                {
                    this.launchButton.Enabled = false;
                    this.honourPenaltyValueLabel.Color = Color.FromArgb(0xff, 0x12, 0);
                }
            }
        }

        private void showHonourPenalty(int penalty, int captureCost)
        {
            if (!this.toCapital)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                this.honourPenaltyValueLabel.Text = penalty.ToString("N", nFI);
                this.m_selectedPenalty = penalty;
                this.honourPenaltyValueLabel.Visible = true;
                this.honourPenaltyLabel.Visible = true;
                this.honourPenaltyImage.Visible = true;
                this.honourPenaltyValueLabel.Color = Color.FromArgb(0x12, 0xff, 0);
                this.captureCostValueLabel.Color = Color.FromArgb(0x12, 0xff, 0);
                if (((penalty > 0) && (GameEngine.Instance.World.getCurrentHonour() <= 0.0)) || ((captureCost > 0) && (GameEngine.Instance.World.getCurrentHonour() < captureCost)))
                {
                    this.launchButton.Enabled = false;
                    this.honourPenaltyValueLabel.Color = Color.FromArgb(0xff, 0x12, 0);
                    this.captureCostValueLabel.Color = Color.FromArgb(0xff, 0x12, 0);
                }
            }
        }

        private void sliderClick()
        {
            if (this.currentCommand == 1)
            {
                this.currentPillageType++;
                if (this.currentPillageType >= 5)
                {
                    this.currentPillageType = 0;
                }
                if (this.currentPillageType == 0)
                {
                    this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Stockpile", "Stockpile");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x18];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[30];
                }
                else if (this.currentPillageType == 1)
                {
                    this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Granary", "Granary");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x19];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[0x1f];
                }
                else if (this.currentPillageType == 2)
                {
                    this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Inn", "Inn");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x22];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[0x23];
                }
                else if (this.currentPillageType == 3)
                {
                    this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Village_Hall", "Village Hall");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x1b];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[0x21];
                }
                else if (this.currentPillageType == 4)
                {
                    this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Armoury", "Armoury");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x1a];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[0x20];
                }
            }
        }

        private void tracksMoved()
        {
            if (this.currentCommand == 2)
            {
                this.sliderValueLabel.Text = (this.sliderImage.Value + 1).ToString();
            }
            else
            {
                this.sliderValueLabel.Text = ((this.sliderImage.Value + 1)).ToString() + "%";
            }
        }

        public void update()
        {
            this.cardbar.update();
            double num = this.storedPreCardDistance * CardTypes.getArmySpeed(GameEngine.Instance.World.UserCardData);
            string str = VillageMap.createBuildTimeString((int) num);
            this.timeLabel.TextDiffOnly = str;
        }

        public void updateButtons(int type)
        {
            this.currentCommand = type;
            this.actionButton_Vandalise.ImageNorm = (Image) GFXLibrary.send_army_buttons[1];
            this.actionButton_Vandalise.ImageOver = (Image) GFXLibrary.send_army_buttons[7];
            this.actionButton_Pillage.ImageNorm = (Image) GFXLibrary.send_army_buttons[5];
            this.actionButton_Pillage.ImageOver = (Image) GFXLibrary.send_army_buttons[11];
            this.actionButton_Ransack.ImageNorm = (Image) GFXLibrary.send_army_buttons[2];
            this.actionButton_Ransack.ImageOver = (Image) GFXLibrary.send_army_buttons[8];
            this.actionButton_Raze.ImageNorm = (Image) GFXLibrary.send_army_buttons[4];
            this.actionButton_Raze.ImageOver = (Image) GFXLibrary.send_army_buttons[10];
            this.actionButton_Capture.ImageNorm = (Image) GFXLibrary.send_army_buttons[3];
            this.actionButton_Capture.ImageOver = (Image) GFXLibrary.send_army_buttons[9];
            this.actionButton_GoldRaid.ImageNorm = (Image) GFXLibrary.send_army_buttons[0];
            this.actionButton_GoldRaid.ImageOver = (Image) GFXLibrary.send_army_buttons[6];
            this.gfxImage.Visible = true;
            this.m_selectedPenalty = 0;
            this.sliderImage.Visible = false;
            this.arrowImage.Visible = true;
            this.tooltipLabel.Visible = true;
            this.villageActionLabel.Visible = true;
            this.needCaptainLabel.Visible = false;
            this.honourPenaltyImage.Visible = false;
            this.honourPenaltyLabel.Visible = false;
            this.honourPenaltyValueLabel.Visible = false;
            this.captureCostImage.Visible = false;
            this.captureCostLabel.Visible = false;
            this.captureCostValueLabel.Visible = false;
            switch (type)
            {
                case 0:
                    this.actionButton_Vandalise.ImageNorm = (Image) GFXLibrary.send_army_buttons[13];
                    this.actionButton_Vandalise.ImageOver = (Image) GFXLibrary.send_army_buttons[0x13];
                    this.launchButton.Enabled = true;
                    if (this.actionButton_Vandalise.CustomTooltipID != 0x834)
                    {
                        this.villageActionLabel.Text = SK.Text("GENERIC_Attack", "Attack");
                        break;
                    }
                    this.villageActionLabel.Text = SK.Text("LaunchAttackPopup_Vandalise", "Vandalise");
                    break;

                case 1:
                {
                    this.actionButton_Pillage.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x11];
                    this.actionButton_Pillage.ImageOver = (Image) GFXLibrary.send_army_buttons[0x17];
                    this.sliderImage.Visible = true;
                    this.launchButton.Enabled = true;
                    this.sliderImage.Value = 0;
                    this.sliderImage.Max = this.maxPillageValue - 1;
                    this.sliderHeaderLabel.Text = SK.Text("BuildingTypes_Stockpile", "Stockpile");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x18];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[30];
                    this.currentPillageType = 0;
                    this.villageActionLabel.Text = SK.Text("GENERIC_Pillage", "Pillage");
                    this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Pillage_tooltip", "Steal resources from an enemy.");
                    int num3 = 0;
                    if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
                    {
                        if (this.m_battleHonourData != null)
                        {
                            this.m_battleHonourData.attackType = 2;
                            num3 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1);
                        }
                        if (num3 > 0)
                        {
                            this.showHonourPenalty(num3);
                        }
                    }
                    goto Label_0950;
                }
                case 2:
                {
                    this.actionButton_Ransack.ImageNorm = (Image) GFXLibrary.send_army_buttons[14];
                    this.actionButton_Ransack.ImageOver = (Image) GFXLibrary.send_army_buttons[20];
                    this.sliderImage.Visible = true;
                    this.launchButton.Enabled = true;
                    this.sliderImage.Value = 0;
                    this.sliderImage.Max = this.maxRansackValue - 1;
                    this.sliderHeaderLabel.Text = SK.Text("LaunchAttackPopup_Max_Buildings", "Max Buildings");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x1d];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[0x1d];
                    this.villageActionLabel.Text = SK.Text("GENERIC_Ransack", "Ransack");
                    this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Ransack_tooltip", "Destroy enemy village buildings.");
                    int num4 = 0;
                    if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
                    {
                        if (this.m_battleHonourData != null)
                        {
                            this.m_battleHonourData.attackType = 3;
                            num4 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1);
                        }
                        if (num4 > 0)
                        {
                            this.showHonourPenalty(num4);
                        }
                    }
                    goto Label_0950;
                }
                case 3:
                {
                    this.actionButton_Raze.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x10];
                    this.actionButton_Raze.ImageOver = (Image) GFXLibrary.send_army_buttons[0x16];
                    if (!this.noCaptain)
                    {
                        this.launchButton.Enabled = true;
                    }
                    else
                    {
                        this.launchButton.Enabled = false;
                    }
                    this.needCaptainLabel.Visible = this.noCaptain;
                    this.villageActionLabel.Text = SK.Text("GENERIC_Raze", "Raze");
                    this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Raze_tooltip", "Completely destroy target.");
                    int num5 = 0;
                    if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
                    {
                        if (this.m_battleHonourData != null)
                        {
                            this.m_battleHonourData.attackType = 9;
                            num5 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1);
                        }
                        if (num5 > 0)
                        {
                            this.showHonourPenalty(num5);
                        }
                    }
                    goto Label_0950;
                }
                case 4:
                    this.actionButton_Capture.ImageNorm = (Image) GFXLibrary.send_army_buttons[15];
                    this.actionButton_Capture.ImageOver = (Image) GFXLibrary.send_army_buttons[0x15];
                    if (!this.noCaptain)
                    {
                        this.launchButton.Enabled = true;
                    }
                    else
                    {
                        this.launchButton.Enabled = false;
                    }
                    this.needCaptainLabel.Visible = this.noCaptain;
                    this.villageActionLabel.Text = SK.Text("GENERIC_Capture", "Capture");
                    this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Capture_tooltip", "Captures enemy village.");
                    if (this.m_captureHonourCost > 0)
                    {
                        this.captureCostLabel.Visible = true;
                        this.captureCostValueLabel.Visible = true;
                        this.captureCostImage.Visible = true;
                    }
                    if (!GameEngine.Instance.World.isCapital(this.m_toVillage) && !this.capitalToCapital)
                    {
                        int num6 = 0;
                        if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
                        {
                            if (this.m_battleHonourData != null)
                            {
                                if (this.m_captureAllowed)
                                {
                                    this.m_battleHonourData.attackType = 1;
                                }
                                else
                                {
                                    this.m_battleHonourData.attackType = 11;
                                }
                                num6 = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1);
                            }
                            if ((num6 > 0) || (this.m_captureHonourCost > 0))
                            {
                                this.showHonourPenalty(num6, this.m_captureHonourCost);
                            }
                        }
                    }
                    goto Label_0950;

                case 5:
                    this.actionButton_GoldRaid.ImageNorm = (Image) GFXLibrary.send_army_buttons[12];
                    this.actionButton_GoldRaid.ImageOver = (Image) GFXLibrary.send_army_buttons[0x12];
                    this.sliderImage.Visible = true;
                    this.launchButton.Enabled = true;
                    this.sliderImage.Value = 0;
                    this.sliderImage.Max = this.maxGoldRaidValue - 1;
                    this.sliderHeaderLabel.Text = SK.Text("GENERIC_Gold", "Gold");
                    this.sliderButton.ImageNorm = (Image) GFXLibrary.send_army_buttons[0x1c];
                    this.sliderButton.ImageOver = (Image) GFXLibrary.send_army_buttons[0x1c];
                    this.villageActionLabel.Text = SK.Text("GENERIC_Gold_Raid", "Gold Raid");
                    this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Goldraid_tooltip", "Steals gold from capital.");
                    goto Label_0950;

                default:
                    goto Label_0950;
            }
            if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(this.m_toVillage)))
            {
                this.villageActionLabel.Text = SK.Text("LaunchAttackPopup_Attack_tooltip_treasure_castle", "Attack a Treasure Castle.");
                this.tooltipLabel.Size = new Size(640, 60);
                this.buttonIndentImage.Visible = false;
                this.gfxImage.Position = new Point(120, 0x4d);
                this.tooltipLabel.Text = SK.Text("CastleMap_TC_Message", "Treasure chests are below ground and cannot be seen by troops until they are on an immediately adjacent tile, otherwise they march to the keep as normal.");
            }
            else
            {
                this.tooltipLabel.Text = SK.Text("LaunchAttackPopup_Attack_tooltip", "Attack an enemy castle.");
            }
            int penalty = 0;
            if (!GameEngine.Instance.World.isCapital(this.m_fromVillage))
            {
                if (this.m_battleHonourData != null)
                {
                    this.m_battleHonourData.attackType = 11;
                    penalty = CastlesCommon.calcBattleHonourCost(this.m_battleHonourData, GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1);
                }
                if (penalty > 0)
                {
                    this.showHonourPenalty(penalty);
                }
            }
        Label_0950:
            this.tracksMoved();
        }

        private void villageFavouriteClicked()
        {
            if (AttackTargetsPanel.isFavourite(this.m_toVillage))
            {
                AttackTargetsPanel.removeFavourite(this.m_toVillage);
                this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_3;
                this.targetVillageFavourite.CustomTooltipID = 0x7e2;
            }
            else
            {
                AttackTargetsPanel.addFavourite(this.m_toVillage);
                this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_1;
                this.targetVillageFavourite.CustomTooltipID = 0x83b;
            }
        }
    }
}

