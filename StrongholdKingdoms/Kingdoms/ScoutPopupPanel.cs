namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class ScoutPopupPanel : CustomSelfDrawPanel
    {
        private int aiworld_Scout_ID_numScouts = -1;
        private int aiworld_Scout_ID_ownVillage = -1;
        private int aiworld_Scout_ID_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDImage arrowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundBottomEdge = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundRightEdge = new CustomSelfDrawPanel.CSDImage();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage gfxImage = new CustomSelfDrawPanel.CSDImage();
        private bool inLaunch;
        private DateTime lastLaunchTime = DateTime.MinValue;
        private int lastMax = -1;
        private CustomSelfDrawPanel.CSDButton launchButton = new CustomSelfDrawPanel.CSDButton();
        private int m_carryLevel;
        private int m_ownVillage = -1;
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel numLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel scoutCarryingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel scoutHonourLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel scoutingLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool sliderEnabled;
        private CustomSelfDrawPanel.CSDTrackBar sliderImage = new CustomSelfDrawPanel.CSDTrackBar();
        private double storedPreCardDistance;
        private CustomSelfDrawPanel.CSDImage targetImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton targetVillageFavourite = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel timeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage titleImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();

        public ScoutPopupPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void cancelInterdictionCallback(CancelInterdiction_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.inLaunch = true;
                this.closeButton.Enabled = false;
                RemoteServices.Instance.SendScouts(this.aiworld_Scout_ID_ownVillage, this.aiworld_Scout_ID_selectedVillage, this.aiworld_Scout_ID_numScouts);
            }
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
            InterfaceMgr.Instance.closeScoutPopupWindow();
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

        public void init(int villageID, bool reset)
        {
            NumberFormatInfo nFI;
            int scoutResourceCarryLevel;
            Color white = ARGBColors.White;
            Color black = ARGBColors.Black;
            Color color1 = ARGBColors.White;
            this.m_selectedVillage = villageID;
            this.m_ownVillage = InterfaceMgr.Instance.OwnSelectedVillage;
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
            this.cardbar.Position = new Point(0, 4);
            this.mainBackgroundImage.addControl(this.cardbar);
            this.cardbar.init(7);
            this.gfxImage.Image = (Image) GFXLibrary.scout_screen_illustration_01;
            this.gfxImage.Position = new Point(20, 0x47);
            this.mainBackgroundImage.addControl(this.gfxImage);
            this.sliderImage.Position = new Point(0x2c, 0x11c);
            this.sliderImage.Margin = new Rectangle(0x20, 0x3f, 0x20, 0x19);
            this.sliderImage.Value = 0;
            this.sliderImage.Max = 0;
            this.sliderImage.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
            this.mainBackgroundImage.addControl(this.sliderImage);
            this.sliderImage.Create((Image) GFXLibrary.scout_screen_slider, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar);
            this.arrowImage.Image = (Image) GFXLibrary.scout_screen_arrowbox;
            this.arrowImage.Position = new Point(0xee, 0x11c);
            this.mainBackgroundImage.addControl(this.arrowImage);
            this.scoutingLabel.Text = SK.Text("ScoutPopup_Scouting_Target", "Scouting") + " '" + GameEngine.Instance.World.getVillageNameOrType(villageID) + "'";
            this.scoutingLabel.Color = white;
            this.scoutingLabel.DropShadowColor = black;
            this.scoutingLabel.Position = new Point(0, 0xf3);
            this.scoutingLabel.Size = new Size(700, 30);
            this.scoutingLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
            this.scoutingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundImage.addControl(this.scoutingLabel);
            if (AttackTargetsPanel.isFavourite(this.m_selectedVillage))
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
            this.targetVillageFavourite.Position = new Point(650, 0xf4);
            this.targetVillageFavourite.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.villageFavouriteClicked));
            this.targetVillageFavourite.Data = 0;
            this.mainBackgroundImage.addControl(this.targetVillageFavourite);
            this.numLabel.Text = "";
            this.numLabel.Color = white;
            this.numLabel.DropShadowColor = black;
            this.numLabel.Position = new Point(0x3f, 0x17);
            this.numLabel.Size = new Size(0x3b, 0x18);
            this.numLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.numLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.sliderImage.addControl(this.numLabel);
            this.timeLabel.Text = "00:00:00";
            this.timeLabel.Color = white;
            this.timeLabel.DropShadowColor = black;
            this.timeLabel.Position = new Point(-28, 0x17);
            this.timeLabel.Size = new Size(0xbf, 0x18);
            this.timeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.timeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.arrowImage.addControl(this.timeLabel);
            int index = 0;
            int type = GameEngine.Instance.World.getSpecial(villageID);
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

                case 100:
                    if (HolidayPeriods.xmas(VillageMap.getCurrentServerTime()))
                    {
                        index = 0x3b;
                    }
                    else
                    {
                        index = 0x1d;
                    }
                    break;

                case 0x6a:
                    index = 30;
                    break;

                case 0x6b:
                    index = 0x1f;
                    break;

                case 0x6c:
                    index = 0x21;
                    break;

                case 0x6d:
                    index = 0x20;
                    break;

                case 0x70:
                    index = 0x22;
                    break;

                case 0x71:
                    index = 0x23;
                    break;

                case 0x72:
                    index = 0x24;
                    break;

                case 0x73:
                    index = 0x29;
                    break;

                case 0x74:
                    index = 0x25;
                    break;

                case 0x75:
                    index = 40;
                    break;

                case 0x76:
                    index = 0x2a;
                    break;

                case 0x77:
                    index = 0x2d;
                    break;

                case 0x79:
                    index = 0x2c;
                    break;

                case 0x7a:
                    index = 0x26;
                    break;

                case 0x7b:
                    index = 0x2b;
                    break;

                case 0x7c:
                    index = 0x2e;
                    break;

                case 0x7d:
                    index = 0x2f;
                    break;

                case 0x7e:
                    index = 0x30;
                    break;

                case 0x80:
                    index = 0x3d;
                    break;

                case 0x81:
                    index = 60;
                    break;

                case 130:
                    index = 0x3e;
                    break;

                case 0x83:
                    index = 0x3f;
                    break;

                case 0x84:
                    index = 0x40;
                    break;

                case 0x85:
                    index = 0x27;
                    break;

                default:
                    if (GameEngine.Instance.World.isRegionCapital(villageID))
                    {
                        index = 0x31;
                    }
                    else if (GameEngine.Instance.World.isCountyCapital(villageID))
                    {
                        index = 50;
                    }
                    else if (GameEngine.Instance.World.isProvinceCapital(villageID))
                    {
                        index = 0x33;
                    }
                    else if (GameEngine.Instance.World.isCountryCapital(villageID))
                    {
                        index = 0x34;
                    }
                    else
                    {
                        index = GameEngine.Instance.World.getVillageSize(villageID);
                    }
                    break;
            }
            switch (type)
            {
                case 0x6a:
                case 0x6b:
                case 0x6c:
                case 0x6d:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x74:
                case 0x75:
                case 0x76:
                case 0x77:
                case 0x79:
                case 0x7a:
                case 0x7b:
                case 0x7c:
                case 0x7d:
                case 0x7e:
                case 0x80:
                case 0x81:
                case 130:
                case 0x83:
                case 0x84:
                case 0x85:
                {
                    WorldMap.SpecialVillageCache cache = GameEngine.Instance.World.getSpecialVillageData(villageID, false);
                    if (cache == null)
                    {
                        goto Label_0B37;
                    }
                    nFI = GameEngine.NFI;
                    CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                        Text = cache.resourceLevel.ToString("N", nFI),
                        Position = new Point(0x9e, 0x55),
                        Size = new Size(150, 20),
                        Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER,
                        Color = white,
                        DropShadowColor = black,
                        Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular)
                    };
                    this.arrowImage.addControl(control);
                    scoutResourceCarryLevel = GameEngine.Instance.LocalWorldData.ScoutResourceCarryLevel;
                    int num5 = GameEngine.Instance.World.UserResearchData.Research_Foraging;
                    scoutResourceCarryLevel = (CardTypes.adjustForagingLevel(GameEngine.Instance.World.UserCardData, scoutResourceCarryLevel) * ResearchData.foragingResearch[num5]) / 2;
                    switch (type)
                    {
                        case 0x77:
                        case 0x79:
                        case 0x7a:
                        case 0x7b:
                        case 0x7c:
                        case 0x7d:
                        case 0x7e:
                        case 0x80:
                        case 0x81:
                        case 130:
                        case 0x83:
                        case 0x84:
                        case 0x85:
                            scoutResourceCarryLevel /= 10;
                            goto Label_0A92;
                    }
                    break;
                }
                default:
                    goto Label_0B37;
            }
        Label_0A92:
            this.m_carryLevel = scoutResourceCarryLevel;
            this.scoutCarryingLabel.Text = this.m_carryLevel.ToString("N", nFI);
            this.scoutCarryingLabel.Position = new Point(0, 90);
            this.scoutCarryingLabel.Size = new Size(this.sliderImage.Width, 20);
            this.scoutCarryingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.scoutCarryingLabel.Color = white;
            this.scoutCarryingLabel.DropShadowColor = black;
            this.scoutCarryingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.sliderImage.addControl(this.scoutCarryingLabel);
        Label_0B37:
            this.targetImage.Image = (Image) GFXLibrary.scout_screen_icons[index];
            this.targetImage.Position = new Point(0xb5, 5);
            this.arrowImage.addControl(this.targetImage);
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            Point point = GameEngine.Instance.World.getVillageLocation(InterfaceMgr.Instance.OwnSelectedVillage);
            Point point2 = GameEngine.Instance.World.getVillageLocation(villageID);
            int x = point.X;
            int num7 = point.Y;
            int num8 = point2.X;
            int num9 = point2.Y;
            double d = ((x - num8) * (x - num8)) + ((num7 - num9) * (num7 - num9));
            d = Math.Sqrt(d) * ((localWorldData.ScoutsMoveSpeed * localWorldData.gamePlaySpeed) * ResearchData.ScoutTimes[GameEngine.Instance.World.UserResearchData.Research_Horsemanship]);
            this.storedPreCardDistance = d;
            d *= CardTypes.getScoutSpeed(GameEngine.Instance.World.UserCardData);
            string str = VillageMap.createBuildTimeString((int) d);
            this.timeLabel.Text = str;
            this.timeLabel.CustomTooltipID = 0x4e20;
            this.timeLabel.CustomTooltipData = (int) d;
            this.launchButton.ImageNorm = (Image) GFXLibrary.button_with_inset_normal;
            this.launchButton.ImageOver = (Image) GFXLibrary.button_with_inset_over;
            this.launchButton.ImageClick = (Image) GFXLibrary.button_with_inset_pushed;
            this.launchButton.Position = new Point(520, 0x144);
            this.launchButton.Text.Text = SK.Text("ScoutPopup_Go", "Go");
            this.launchButton.Text.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
            this.launchButton.TextYOffset = 1;
            this.launchButton.Text.Color = ARGBColors.Black;
            this.launchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launch), "ScoutPopupPanel_launch");
            this.launchButton.Enabled = false;
            this.mainBackgroundImage.addControl(this.launchButton);
            this.scoutHonourLabel.Text = "";
            this.scoutHonourLabel.Color = white;
            this.scoutHonourLabel.DropShadowColor = black;
            this.scoutHonourLabel.Position = new Point(0, 410);
            this.scoutHonourLabel.Size = new Size(700, 30);
            this.scoutHonourLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
            this.scoutHonourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.scoutHonourLabel.Visible = false;
            this.mainBackgroundImage.addControl(this.scoutHonourLabel);
            if ((type >= 100) && (type <= 0xc7))
            {
                this.scoutHonourLabel.Text = SK.Text("ScoutPopup_No_Honour_Stash_Out_Of_Range", "No Honour will be received, the stash is out of range.");
            }
            else if (type == 5)
            {
                this.scoutHonourLabel.Text = SK.Text("ScoutPopup_No_Honour_Wolf_Lair_Out_Of_Range", "No Honour will be received, the Wolf Lair is out of range.");
            }
            else if (type == 3)
            {
                this.scoutHonourLabel.Text = SK.Text("ScoutPopup_No_Honour_Bandit_Camp_Out_Of_Range", "No Honour will be received, the Bandit Camp is out of range.");
            }
            else if (((type == 7) || (type == 9)) || ((type == 11) || (type == 13)))
            {
                this.scoutHonourLabel.Text = SK.Text("ScoutPopup_No_Honour_AI_castle_Out_Of_Range", "No Honour will be received, the AI Castle is out of range.");
            }
            else if (((type == 15) || (type == 0x11)) || SpecialVillageTypes.IS_TREASURE_CASTLE(type))
            {
                this.scoutHonourLabel.Text = SK.Text("LaunchAttackPopup_Paladin_No_Honour", "No honour will be received for destroying this type of AI castle");
            }
            else
            {
                this.scoutHonourLabel.Text = SK.Text("ScoutPopup_No_Honour_Village_Out_Of_Range", "No Honour will be received, the village is out of range.");
            }
            this.scoutHonourLabel.Visible = GameEngine.Instance.World.isScoutHonourOutOfRange(InterfaceMgr.Instance.OwnSelectedVillage, villageID) && ((type <= 100) || (type > 0xc7));
            this.titleImage.Image = (Image) GFXLibrary.popup_title_bar;
            this.titleImage.Position = new Point(0, 0);
            base.addControl(this.titleImage);
            this.titleLabel.Text = SK.Text("OwnVillagePanel_Send_Out_Scouts", "Send Out Scouts");
            this.titleLabel.Color = Color.FromArgb(0xff, 0xff, 0xff);
            this.titleLabel.DropShadowColor = black;
            this.titleLabel.Position = new Point(20, 5);
            this.titleLabel.Size = new Size(base.Width, 0x20);
            this.titleLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.titleImage.addControl(this.titleLabel);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x293, 5);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "ScoutPopupPanel_close");
            this.titleImage.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.titleImage, 0x22, new Point(0x261, 5));
            if (GameEngine.Instance.getVillage(this.m_ownVillage) != null)
            {
                this.onVillageLoadUpdate(this.m_ownVillage, true);
            }
            else
            {
                GameEngine.Instance.downloadCurrentVillage();
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void launch()
        {
            if (this.sliderEnabled)
            {
                if (this.inLaunch)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastLaunchTime);
                    if (span.TotalSeconds < 20.0)
                    {
                        return;
                    }
                }
                this.inLaunch = true;
                this.lastLaunchTime = DateTime.Now;
                int numScouts = this.sliderImage.Value + 1;
                this.aiworld_Scout_ID_ownVillage = this.m_ownVillage;
                this.aiworld_Scout_ID_selectedVillage = this.m_selectedVillage;
                this.aiworld_Scout_ID_numScouts = numScouts;
                RemoteServices.Instance.set_SendScouts_UserCallBack(new RemoteServices.SendScouts_UserCallBack(this.sendScoutsCallback));
                RemoteServices.Instance.SendScouts(this.m_ownVillage, this.m_selectedVillage, numScouts);
                AllVillagesPanel.travellersChanged();
                VillageMap map = GameEngine.Instance.getVillage(this.m_ownVillage);
                if (map != null)
                {
                    map.addTroops(0, 0, 0, 0, 0, -numScouts);
                }
                this.launchButton.Enabled = false;
                this.closeButton.Enabled = false;
                CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, base.ParentForm);
            }
        }

        public void onVillageLoadUpdate(int villageID, bool initial)
        {
            if (!this.inLaunch && ((this.m_ownVillage == villageID) && (GameEngine.Instance.getVillage(this.m_ownVillage) != null)))
            {
                VillageMap map = GameEngine.Instance.getVillage(this.m_ownVillage);
                if (initial)
                {
                    if (map.m_numScouts > 0)
                    {
                        this.launchButton.Enabled = true;
                        this.sliderImage.Max = map.m_numScouts - 1;
                        this.sliderImage.Value = map.m_numScouts - 1;
                        this.sliderEnabled = true;
                    }
                    else
                    {
                        this.sliderImage.Value = 0;
                        this.sliderImage.Max = 0;
                        this.sliderEnabled = false;
                    }
                    base.Invalidate();
                    this.tracksMoved();
                }
                else if (map.m_numScouts != this.lastMax)
                {
                    if (map.m_numScouts > this.lastMax)
                    {
                        this.sliderImage.Max = map.m_numScouts - 1;
                        if (this.lastMax == 0)
                        {
                            this.sliderImage.Value = map.m_numScouts - 1;
                        }
                    }
                    else
                    {
                        int num = this.sliderImage.Value + 1;
                        if (num > map.m_numScouts)
                        {
                            this.sliderImage.Value = map.m_numScouts - 1;
                            this.sliderImage.Max = map.m_numScouts - 1;
                        }
                        else
                        {
                            this.sliderImage.Max = map.m_numScouts - 1;
                        }
                    }
                    if (map.m_numScouts == 0)
                    {
                        this.launchButton.Enabled = false;
                    }
                    else
                    {
                        this.launchButton.Enabled = true;
                    }
                    this.sliderEnabled = this.launchButton.Enabled;
                    base.Invalidate();
                    this.tracksMoved();
                }
                this.lastMax = map.m_numScouts;
            }
        }

        public void sendScoutsCallback(SendScouts_ReturnType returnData)
        {
            CursorManager.SetCursor(CursorManager.CursorType.Default, base.ParentForm);
            this.inLaunch = false;
            if (!returnData.Success && (returnData.m_errorCode == ErrorCodes.ErrorCode.ATTACKING_VILLAGE_INTERDICT_PROTECTED))
            {
                MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                if (MyMessageBox.Show(SK.Text("GameEngine_Currently_Interdited", "You are currently Interdiction protected") + "\n" + SK.Text("GameEngine_CancelProtection", "Do you wish to cancel this protection?"), SK.Text("GENERIC_Protected", "You Are Protected"), yesNo) == DialogResult.Yes)
                {
                    RemoteServices.Instance.set_CancelInterdiction_UserCallBack(new RemoteServices.CancelInterdiction_UserCallBack(this.cancelInterdictionCallback));
                    RemoteServices.Instance.CancelInterdiction(-returnData.sourceVillage);
                }
                else
                {
                    if (returnData.numScoutsNotTaken > 0)
                    {
                        VillageMap map = GameEngine.Instance.getVillage(returnData.sourceVillage);
                        if (map != null)
                        {
                            map.addTroops(0, 0, 0, 0, 0, returnData.numScoutsNotTaken);
                        }
                    }
                    InterfaceMgr.Instance.closeScoutPopupWindow();
                    InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                    InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                }
            }
            else
            {
                this.closeButton.Enabled = true;
                if (returnData.Success)
                {
                    ArmyReturnData[] armyReturnData = new ArmyReturnData[] { returnData.armyData };
                    GameEngine.Instance.World.doGetArmyData(armyReturnData, null, false);
                    GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_ownVillage, false, false, false, false);
                    InterfaceMgr.Instance.closeScoutPopupWindow();
                    if (returnData.cardData != null)
                    {
                        GameEngine.Instance.World.UserCardData = returnData.cardData;
                    }
                    AttackTargetsPanel.addRecent(returnData.targetVillage);
                }
                if (returnData.numScoutsNotTaken > 0)
                {
                    VillageMap map2 = GameEngine.Instance.getVillage(returnData.sourceVillage);
                    if (map2 != null)
                    {
                        map2.addTroops(0, 0, 0, 0, 0, returnData.numScoutsNotTaken);
                    }
                    if (!returnData.Success)
                    {
                        this.launchButton.Enabled = false;
                    }
                }
                if (returnData.Success)
                {
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
                    InterfaceMgr.Instance.closeScoutPopupWindow();
                }
            }
        }

        private void tracksMoved()
        {
            if (this.sliderEnabled)
            {
                this.numLabel.Text = (this.sliderImage.Value + 1).ToString();
                NumberFormatInfo nFI = GameEngine.NFI;
                this.scoutCarryingLabel.Text = (this.m_carryLevel * (this.sliderImage.Value + 1)).ToString("N", nFI);
            }
            else
            {
                this.numLabel.Text = "0";
                this.scoutCarryingLabel.Text = "";
            }
        }

        public void update()
        {
            this.cardbar.update();
            this.onVillageLoadUpdate(this.m_ownVillage, false);
            this.numLabel.Text = this.numLabel.Text;
            double num = this.storedPreCardDistance * CardTypes.getScoutSpeed(GameEngine.Instance.World.UserCardData);
            if (((int) num) != this.timeLabel.CustomTooltipData)
            {
                string str = VillageMap.createBuildTimeString((int) num);
                this.timeLabel.Text = str;
                this.timeLabel.CustomTooltipID = 0x4e20;
                this.timeLabel.CustomTooltipData = (int) num;
            }
        }

        private void villageFavouriteClicked()
        {
            if (AttackTargetsPanel.isFavourite(this.m_selectedVillage))
            {
                AttackTargetsPanel.removeFavourite(this.m_selectedVillage);
                this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_3;
                this.targetVillageFavourite.CustomTooltipID = 0x7e2;
            }
            else
            {
                AttackTargetsPanel.addFavourite(this.m_selectedVillage);
                this.targetVillageFavourite.ImageNorm = (Image) GFXLibrary.star_market_1;
                this.targetVillageFavourite.CustomTooltipID = 0x83b;
            }
        }
    }
}

