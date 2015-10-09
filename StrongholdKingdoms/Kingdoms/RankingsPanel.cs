namespace Kingdoms
{
    using CommonTypes;
    using StatTracking;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    public class RankingsPanel : CustomSelfDrawPanel, IDockableControl
    {
        private bool allowSharePopup;
        public static bool animating = false;
        private CustomSelfDrawPanel.CSDArea animBack = new CustomSelfDrawPanel.CSDArea();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage cpImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel currentRankLabel = new CustomSelfDrawPanel.CSDLabel();
        private DockableControl dockableControl;
        private bool doingRankAnim;
        private double dx;
        private double dy;
        private CustomSelfDrawPanel.CSDButton facebookShareButton = new CustomSelfDrawPanel.CSDButton();
        private int fadeCount;
        private int fb_newRank = -1;
        private CustomSelfDrawPanel.CSDFill filledArea = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDImage honourSymbol = new CustomSelfDrawPanel.CSDImage();
        private bool ignoreSetCurrent;
        private bool inUpgrade;
        public static DateTime lastProgressDownload = DateTime.MinValue;
        private static int lastRank = 0;
        private static int lastRankSubLevel = 0;
        private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private int majorRankUp;
        private CustomSelfDrawPanel.MedalWindow medalWindow = new CustomSelfDrawPanel.MedalWindow();
        private const int medalWindow_area_x = 9;
        private const int medalWindow_area_y = 0xd5;
        private static bool newIn = false;
        private CustomSelfDrawPanel.CSDLabel nextRankLabel = new CustomSelfDrawPanel.CSDLabel();
        private int numSteps;
        public static AchievementProgress_ReturnType progressData = null;
        private List<CSDSlot> progressSlots = new List<CSDSlot>();
        private CustomSelfDrawPanel.CSDImage rankImage01 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage02 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage03 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage04 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage05 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage06 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage07 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage08 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage09 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage10 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage11 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage12 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage13 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage14 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage15 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage16 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage17 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage18 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage19 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage20 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage21 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage rankImage22 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage scaledImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.MedalWindow selectedMedalWindow = new CustomSelfDrawPanel.MedalWindow();
        private Point startPos;
        private double targetScale = 0.2834008097165992;
        private CustomSelfDrawPanel.CSDArea unscaledArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton upgradeButton = new CustomSelfDrawPanel.CSDButton();
        private int zoomCount;
        private const double zoomStep = 0.02;

        public RankingsPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.NoDrawBackground = true;
        }

        private void achievementProgressCallback(AchievementProgress_ReturnType returnData)
        {
            if (returnData.Success)
            {
                progressData = returnData;
            }
        }

        private void clearRankAnim()
        {
            this.doingRankAnim = false;
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

        private void facebookShareClicked()
        {
            if (this.fb_newRank >= 0)
            {
                string str = string.Concat(new object[] { "http://login.strongholdkingdoms.com/facebook/js_share.php?u=", RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "&eventid=", this.fb_newRank.ToString(), "&desc=", Rankings.getRankingName(this.fb_newRank), "&lang=", Program.mySettings.LanguageIdent, "&worldid=", Program.mySettings.LastWorldID });
                try
                {
                    StatTrackingClient.Instance().ActivateTrigger(30, 0);
                    new Process { StartInfo = { FileName = str } }.Start();
                }
                catch (Exception)
                {
                }
            }
        }

        public static int getProgressValue(int achievement)
        {
            achievement &= 0xfffffff;
            switch (achievement)
            {
                case 1:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_WOLVES_PROTECTOR;

                case 2:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_BANDITS_LAW_BRINGER;

                case 3:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_TROOPS_WARRIOR;

                case 4:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_WOLFLAIRS_WOLF_HUNTER;

                case 5:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_BANDITCAMPS_WEREGILD;

                case 6:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_RATSCASTLE_RATTY_LOST_AGAIN;

                case 7:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_SNAKESCASTLE_SNAKES_DOWNFALL;

                case 8:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_PIGSCASTLE_SQUEALPIGGY;

                case 9:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_WOLFSCASTLE_WOLFBANE;

                case 10:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_FLAGS_FLAG_RAIDER;

                case 11:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_RAZE_FIRESTARTER;

                case 12:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_CAPTURE_CONQUEROR;

                case 13:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_PILLAGE_VIKING;

                case 14:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_RANSACK_VANDAL;

                case 15:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_PALADINCASTLE_EVILLORD;

                case 0x10:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.ATTACKING_TREASURESCASTLE_TREASUREHUNTER;

                case 0x22:
                    return -1;

                case 0x25:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.DEFENDING_KILLATTACKS_VANQUISHER;

                case 0x41:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.CASTLE_FIREBALLISTAS_BALLISTA_CRAZY;

                case 0x42:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.CASTLE_POUROIL_FEEL_THE_HEAT;

                case 0x43:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.CASTLE_STAKE_TRAPS_DEATHTRAP;

                case 100:
                    return (int) GameEngine.Instance.World.getCurrentGold();

                case 0x65:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.VILLAGE_SENDGOODS_CHARITY;

                case 0x81:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.MONKS_CUREDISEASE_HEALER;

                case 130:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.MONKS_INTERDICT_PEACEBRINGER;

                case 0x83:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.MONKS_INFLUENCE_DIPLOMAT;

                case 0xa1:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.SCOUTING_SCOUTRESOURCES_HORSE_MASTER;

                case 0xa2:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.SCOUTING_UNCOVERSTASHES_LIGHTNING_SPEED;

                case 0xa3:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.SCOUTING_PACKETSRETREIVED_MASTER_FORAGER;

                case 0xc2:
                    return -1;

                case 0xc3:
                    return GameEngine.Instance.World.getCurrentFactionDuration();

                case 0xe1:
                    return -1;

                case 0xe2:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.RESEARCH_COMPLETED_LEARNED_SCHOLAR;

                case 0x101:
                    if (progressData != null)
                    {
                        return progressData.MARKET_MAKEGOLD_STOCKBROKER;
                    }
                    break;

                case 0x121:
                    return -1;

                case 290:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.BANQUETING_HONOUR_BANQUET_KING;

                case 0x141:
                    return -1;

                case 0x161:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.PARISH_DONATEPACKETS_TEAM_PLAYER;

                case 0x162:
                    if (progressData == null)
                    {
                        break;
                    }
                    return progressData.PARISH_PLACEBUILDINGS_SKILLED_RULER;

                case 0x181:
                    return GameEngine.Instance.World.numUserParishes();

                case 0x182:
                    return GameEngine.Instance.World.numUserCounties();

                case 0x183:
                    return GameEngine.Instance.World.numUserProvinces();

                case 0x184:
                    return GameEngine.Instance.World.numUserCountries();
            }
            return -1;
        }

        private CustomSelfDrawPanel.CSDImage getRankImage(int rank)
        {
            switch (rank)
            {
                case 0:
                    return this.rankImage01;

                case 1:
                    return this.rankImage02;

                case 2:
                    return this.rankImage03;

                case 3:
                    return this.rankImage04;

                case 4:
                    return this.rankImage05;

                case 5:
                    return this.rankImage06;

                case 6:
                    return this.rankImage07;

                case 7:
                    return this.rankImage08;

                case 8:
                    return this.rankImage09;

                case 9:
                    return this.rankImage10;

                case 10:
                    return this.rankImage11;

                case 11:
                    return this.rankImage12;

                case 12:
                    return this.rankImage13;

                case 13:
                    return this.rankImage14;

                case 14:
                    return this.rankImage15;

                case 15:
                    return this.rankImage16;

                case 0x10:
                    return this.rankImage17;

                case 0x11:
                    return this.rankImage18;

                case 0x12:
                    return this.rankImage19;

                case 0x13:
                    return this.rankImage20;

                case 20:
                    return this.rankImage21;

                case 0x15:
                    return this.rankImage22;
            }
            return null;
        }

        public void init(bool initialCall)
        {
            animating = false;
            base.clearControls();
            this.progressSlots.Clear();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.Position = new Point(0, 0);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundArea.Position = new Point(0, 0);
            this.mainBackgroundArea.Size = new Size(0x3e0, 0x236);
            this.mainBackgroundImage.addControl(this.mainBackgroundArea);
            this.filledArea.Position = new Point(0, 0);
            this.filledArea.Size = this.mainBackgroundArea.Size;
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 0x12, new Point(base.Width - 0x27, 11));
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundArea, 0x2c, new Point(0x1fd, 0xf3));
            this.currentRankLabel.Text = "";
            this.currentRankLabel.Color = Color.FromArgb(0xe0, 0xcb, 0x92);
            this.currentRankLabel.DropShadowColor = Color.FromArgb(0x38, 50, 0x24);
            this.currentRankLabel.Position = new Point(0x1d, 12);
            this.currentRankLabel.Size = new Size(0x3e0, 50);
            if (Program.mySettings.LanguageIdent == "it")
            {
                this.currentRankLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            }
            else
            {
                this.currentRankLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
            }
            this.currentRankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundArea.addControl(this.currentRankLabel);
            this.nextRankLabel.Text = "";
            this.nextRankLabel.Color = ARGBColors.Black;
            this.nextRankLabel.Position = new Point(0, 0x11);
            this.nextRankLabel.Size = new Size(0x2ff, 50);
            if (Program.mySettings.LanguageIdent == "it")
            {
                this.nextRankLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            }
            else
            {
                this.nextRankLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            }
            this.nextRankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundArea.addControl(this.nextRankLabel);
            this.upgradeButton.Position = new Point(0x311, 10);
            this.upgradeButton.Size = new Size(0xa8, 0x26);
            this.upgradeButton.Text.Text = "1,000";
            this.upgradeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.upgradeButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.upgradeButton.TextYOffset = -1;
            this.upgradeButton.Text.Color = ARGBColors.Black;
            this.upgradeButton.ImageIcon = (Image) GFXLibrary.com_32_honour_DS;
            this.upgradeButton.ImageIconPosition = new Point(5, -5);
            this.upgradeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.upgradeClick), "RankingsPanel_rank_up_click");
            this.upgradeButton.CustomTooltipID = 400;
            this.upgradeButton.Enabled = false;
            this.mainBackgroundImage.addControl(this.upgradeButton);
            this.upgradeButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
            this.upgradeButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
            if (GameEngine.Instance.World.getRank() == 0x16)
            {
                this.rankImage01.Image = (Image) GFXLibrary.rank_progression_crown_prince;
                this.rankImage01.Position = new Point(8, 0x35);
                this.rankImage01.CustomTooltipID = 0x191;
                this.rankImage01.CustomTooltipData = 0x16;
                this.filledArea.addControl(this.rankImage01);
            }
            else
            {
                this.rankImage01.Image = (Image) GFXLibrary.rank_images[0x2c];
                this.rankImage01.Position = new Point(10, 0x35);
                this.rankImage01.CustomTooltipID = 0x191;
                this.rankImage01.CustomTooltipData = 0;
                this.filledArea.addControl(this.rankImage01);
                this.rankImage02.Image = (Image) GFXLibrary.rank_images[0x2d];
                this.rankImage02.Position = new Point(this.rankImage01.X + this.rankImage01.Width, 0x35);
                this.rankImage02.CustomTooltipID = 0x191;
                this.rankImage02.CustomTooltipData = 1;
                this.filledArea.addControl(this.rankImage02);
                this.rankImage03.Image = (Image) GFXLibrary.rank_images[0x2e];
                this.rankImage03.Position = new Point(this.rankImage02.X + this.rankImage02.Width, 0x35);
                this.rankImage03.CustomTooltipID = 0x191;
                this.rankImage03.CustomTooltipData = 2;
                this.filledArea.addControl(this.rankImage03);
                this.rankImage04.Image = (Image) GFXLibrary.rank_images[0x2f];
                this.rankImage04.Position = new Point(this.rankImage03.X + this.rankImage03.Width, 0x35);
                this.rankImage04.CustomTooltipID = 0x191;
                this.rankImage04.CustomTooltipData = 3;
                this.filledArea.addControl(this.rankImage04);
                this.rankImage05.Image = (Image) GFXLibrary.rank_images[0x30];
                this.rankImage05.Position = new Point(this.rankImage04.X + this.rankImage04.Width, 0x35);
                this.rankImage05.CustomTooltipID = 0x191;
                this.rankImage05.CustomTooltipData = 4;
                this.filledArea.addControl(this.rankImage05);
                this.rankImage06.Image = (Image) GFXLibrary.rank_images[0x31];
                this.rankImage06.Position = new Point(this.rankImage05.X + this.rankImage05.Width, 0x35);
                this.rankImage06.CustomTooltipID = 0x191;
                this.rankImage06.CustomTooltipData = 5;
                this.filledArea.addControl(this.rankImage06);
                this.rankImage07.Image = (Image) GFXLibrary.rank_images[50];
                this.rankImage07.Position = new Point(this.rankImage06.X + this.rankImage06.Width, 0x35);
                this.rankImage07.CustomTooltipID = 0x191;
                this.rankImage07.CustomTooltipData = 6;
                this.filledArea.addControl(this.rankImage07);
                this.rankImage08.Image = (Image) GFXLibrary.rank_images[0x33];
                this.rankImage08.Position = new Point(this.rankImage07.X + this.rankImage07.Width, 0x35);
                this.rankImage08.CustomTooltipID = 0x191;
                this.rankImage08.CustomTooltipData = 7;
                this.filledArea.addControl(this.rankImage08);
                this.rankImage09.Image = (Image) GFXLibrary.rank_images[0x34];
                this.rankImage09.Position = new Point(this.rankImage08.X + this.rankImage08.Width, 0x35);
                this.rankImage09.CustomTooltipID = 0x191;
                this.rankImage09.CustomTooltipData = 8;
                this.filledArea.addControl(this.rankImage09);
                this.rankImage10.Image = (Image) GFXLibrary.rank_images[0x35];
                this.rankImage10.Position = new Point(this.rankImage09.X + this.rankImage09.Width, 0x35);
                this.rankImage10.CustomTooltipID = 0x191;
                this.rankImage10.CustomTooltipData = 9;
                this.filledArea.addControl(this.rankImage10);
                this.rankImage11.Image = (Image) GFXLibrary.rank_images[0x36];
                this.rankImage11.Position = new Point(this.rankImage10.X + this.rankImage10.Width, 0x35);
                this.rankImage11.CustomTooltipID = 0x191;
                this.rankImage11.CustomTooltipData = 10;
                this.filledArea.addControl(this.rankImage11);
                this.rankImage12.Image = (Image) GFXLibrary.rank_images[0x37];
                this.rankImage12.Position = new Point(this.rankImage11.X + this.rankImage11.Width, 0x35);
                this.rankImage12.CustomTooltipID = 0x191;
                this.rankImage12.CustomTooltipData = 11;
                this.filledArea.addControl(this.rankImage12);
                this.rankImage13.Image = (Image) GFXLibrary.rank_images[0x38];
                this.rankImage13.Position = new Point(this.rankImage12.X + this.rankImage12.Width, 0x35);
                this.rankImage13.CustomTooltipID = 0x191;
                this.rankImage13.CustomTooltipData = 12;
                this.filledArea.addControl(this.rankImage13);
                this.rankImage14.Image = (Image) GFXLibrary.rank_images[0x39];
                this.rankImage14.Position = new Point(this.rankImage13.X + this.rankImage13.Width, 0x35);
                this.rankImage14.CustomTooltipID = 0x191;
                this.rankImage14.CustomTooltipData = 13;
                this.filledArea.addControl(this.rankImage14);
                this.rankImage15.Image = (Image) GFXLibrary.rank_images[0x3a];
                this.rankImage15.Position = new Point(this.rankImage14.X + this.rankImage14.Width, 0x35);
                this.rankImage15.CustomTooltipID = 0x191;
                this.rankImage15.CustomTooltipData = 14;
                this.filledArea.addControl(this.rankImage15);
                this.rankImage16.Image = (Image) GFXLibrary.rank_images[0x3b];
                this.rankImage16.Position = new Point(this.rankImage15.X + this.rankImage15.Width, 0x35);
                this.rankImage16.CustomTooltipID = 0x191;
                this.rankImage16.CustomTooltipData = 15;
                this.filledArea.addControl(this.rankImage16);
                this.rankImage17.Image = (Image) GFXLibrary.rank_images[60];
                this.rankImage17.Position = new Point(this.rankImage16.X + this.rankImage16.Width, 0x35);
                this.rankImage17.CustomTooltipID = 0x191;
                this.rankImage17.CustomTooltipData = 0x10;
                this.filledArea.addControl(this.rankImage17);
                this.rankImage18.Image = (Image) GFXLibrary.rank_images[0x3d];
                this.rankImage18.Position = new Point(this.rankImage17.X + this.rankImage17.Width, 0x35);
                this.rankImage18.CustomTooltipID = 0x191;
                this.rankImage18.CustomTooltipData = 0x11;
                this.filledArea.addControl(this.rankImage18);
                this.rankImage19.Image = (Image) GFXLibrary.rank_images[0x3e];
                this.rankImage19.Position = new Point(this.rankImage18.X + this.rankImage18.Width, 0x35);
                this.rankImage19.CustomTooltipID = 0x191;
                this.rankImage19.CustomTooltipData = 0x12;
                this.filledArea.addControl(this.rankImage19);
                this.rankImage20.Image = (Image) GFXLibrary.rank_images[0x3f];
                this.rankImage20.Position = new Point(this.rankImage19.X + this.rankImage19.Width, 0x35);
                this.rankImage20.CustomTooltipID = 0x191;
                this.rankImage20.CustomTooltipData = 0x13;
                this.filledArea.addControl(this.rankImage20);
                this.rankImage21.Image = (Image) GFXLibrary.rank_images[0x40];
                this.rankImage21.Position = new Point(this.rankImage20.X + this.rankImage20.Width, 0x35);
                this.rankImage21.CustomTooltipID = 0x191;
                this.rankImage21.CustomTooltipData = 20;
                this.filledArea.addControl(this.rankImage21);
                this.rankImage22.Image = (Image) GFXLibrary.rank_images[0x41];
                this.rankImage22.Position = new Point(this.rankImage21.X + this.rankImage21.Width, 0x35);
                this.rankImage22.CustomTooltipID = 0x191;
                this.rankImage22.CustomTooltipData = 0x15;
                this.filledArea.addControl(this.rankImage22);
            }
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            int num = GameEngine.Instance.World.getRankSubLevel();
            int num2 = localWorldData.ranks_Levels[GameEngine.Instance.World.getRank()];
            if (num2 > 100)
            {
                num2 = 1;
            }
            int num3 = 0x39d / num2;
            for (int i = 0; i < num2; i++)
            {
                CSDSlot control = new CSDSlot {
                    Size = new Size(num3 - 4, 0x19),
                    Position = new Point(0x21 + (i * num3), 0xbb)
                };
                this.mainBackgroundImage.addControl(control);
                control.MaxValue = 0x3b9ac9ff;
                control.CurrentValue = control.MaxValue;
                control.init(i < num, i >= (num2 - 1));
                this.progressSlots.Add(control);
            }
            lastRank = -1;
            lastRankSubLevel = -1;
            if (((GameEngine.Instance.World.getRank() >= 1) && !GameEngine.Instance.World.TutorialIsAdvancing()) && (GameEngine.Instance.World.getTutorialStage() == 7))
            {
                GameEngine.Instance.World.forceTutorialToBeShown();
            }
            bool flag = false;
            this.facebookShareButton.ImageNorm = (Image) GFXLibrary.facebookBrownNorm;
            this.facebookShareButton.ImageOver = (Image) GFXLibrary.facebookBrownOver;
            this.facebookShareButton.ImageClick = (Image) GFXLibrary.facebookBrownClick;
            this.facebookShareButton.Position = new Point(0x329, 0xdb);
            this.facebookShareButton.UseTextSize = true;
            this.facebookShareButton.Text.Text = SK.Text("FACEBOOK_Share", "Share");
            this.facebookShareButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.facebookShareButton.Text.Position = new Point(20, 2);
            this.facebookShareButton.Text.Size = new Size(110, 0x15);
            this.facebookShareButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.facebookShareButton.TextYOffset = 0;
            this.facebookShareButton.Text.Color = ARGBColors.Black;
            if (initialCall)
            {
                this.facebookShareButton.Visible = false;
                this.allowSharePopup = true;
            }
            else if ((this.facebookShareButton.Visible && (this.fb_newRank == 9)) && (this.allowSharePopup && !GameEngine.Instance.World.FacebookFreePack))
            {
                flag = true;
                this.allowSharePopup = false;
                GameEngine.Instance.World.FacebookFreePack = true;
            }
            this.facebookShareButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.facebookShareClicked));
            this.mainBackgroundImage.addControl(this.facebookShareButton);
            List<int> userAchievements = RemoteServices.Instance.UserAchievements;
            this.medalWindow.Position = new Point(9, 0xd5);
            this.medalWindow.init(userAchievements, true, false, 0);
            this.mainBackgroundImage.addControl(this.medalWindow);
            this.filledArea.FillColor = Color.FromArgb(0, 0, 0, 0);
            this.mainBackgroundImage.addControl(this.filledArea);
            this.selectedMedalWindow.Position = new Point(0x1eb, 0x139);
            this.selectedMedalWindow.init(new List<int>(), false, false, -150);
            this.mainBackgroundImage.addControl(this.selectedMedalWindow);
            this.selectedMedalWindow.Visible = false;
            this.medalWindow.setChildWindow(this.selectedMedalWindow);
            this.clearRankAnim();
            if (flag)
            {
                if (lastRankSubLevel < 0)
                {
                    lastRank = GameEngine.Instance.World.getRank();
                    lastRankSubLevel = GameEngine.Instance.World.getRankSubLevel();
                }
                this.setCurrentRankings(lastRank, lastRankSubLevel);
                Application.DoEvents();
                Thread.Sleep(30);
                Application.DoEvents();
                Thread.Sleep(30);
                new RankFacebookPopup().ShowDialog(InterfaceMgr.Instance.ParentForm);
                base.Invalidate();
                if (RankFacebookPanel.shareClicked)
                {
                    this.facebookShareClicked();
                }
            }
            TimeSpan span = (TimeSpan) (DateTime.Now - lastProgressDownload);
            if ((progressData == null) || (span.TotalMinutes > 5.0))
            {
                lastProgressDownload = DateTime.Now;
                RemoteServices.Instance.set_AchievementProgress_UserCallBack(new RemoteServices.AchievementProgress_UserCallBack(this.achievementProgressCallback));
                RemoteServices.Instance.AchievementProgress();
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "RankingsPanel2";
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
            progressData = null;
            lastProgressDownload = DateTime.MinValue;
        }

        public void setCurrentRankings(int rank, int rankSubLevel)
        {
            bool newIn = RankingsPanel.newIn;
            RankingsPanel.newIn = false;
            if ((rank != lastRank) || (lastRankSubLevel != rankSubLevel))
            {
                newIn = true;
            }
            if (rank < 0)
            {
                rank = 0;
            }
            else if (rank >= 0x17)
            {
                rank = 0x16;
            }
            lastRank = rank;
            lastRankSubLevel = rankSubLevel;
            NumberFormatInfo nFI = GameEngine.NFI;
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            this.currentRankLabel.Text = Rankings.getRankingName(localWorldData, rank, rankSubLevel, false, RemoteServices.Instance.UserAvatar.male);
            double num = GameEngine.Instance.World.getCurrentHonour();
            if ((rank == 0x15) && (rankSubLevel >= 0x18))
            {
                this.upgradeButton.Text.Text = 0x989680.ToString("N", nFI);
            }
            else if (rank < 0x16)
            {
                this.upgradeButton.Text.Text = localWorldData.ranks_HonourPerLevel[rank].ToString("N", nFI);
            }
            else
            {
                this.upgradeButton.Text.Text = ((int) Rankings.calcHonourForCrownPrince(rankSubLevel)).ToString("N", nFI);
            }
            int num3 = rank;
            int num4 = rankSubLevel;
            num4++;
            if (num4 >= localWorldData.ranks_Levels[rank])
            {
                num4 = 0;
                num3++;
            }
            if (num3 >= 0x17)
            {
                this.upgradeButton.Enabled = false;
            }
            else
            {
                this.nextRankLabel.Text = Rankings.getRankingName(localWorldData, num3, num4, false, RemoteServices.Instance.UserAvatar.male);
                double num5 = localWorldData.ranks_HonourPerLevel[rank];
                if (rank == 0x16)
                {
                    num5 = Rankings.calcHonourForCrownPrince(rankSubLevel);
                }
                else if ((rank == 0x15) && (rankSubLevel >= 0x18))
                {
                    num5 = 10000000.0;
                }
                if (num >= num5)
                {
                    this.upgradeButton.Enabled = true;
                }
                else
                {
                    this.upgradeButton.Enabled = false;
                }
            }
            int num6 = localWorldData.ranks_Levels[rank];
            if (num6 < 100)
            {
                for (int i = rankSubLevel; (i < num6) && (i < this.progressSlots.Count); i++)
                {
                    int currentValue = this.progressSlots[i].CurrentValue;
                    if (num < localWorldData.ranks_HonourPerLevel[rank])
                    {
                        this.progressSlots[i].CurrentValue = (int) num;
                        this.progressSlots[i].MaxValue = localWorldData.ranks_HonourPerLevel[rank];
                    }
                    else
                    {
                        this.progressSlots[i].CurrentValue = this.progressSlots[i].MaxValue;
                    }
                    num -= localWorldData.ranks_HonourPerLevel[rank];
                    if (currentValue != this.progressSlots[i].CurrentValue)
                    {
                        this.progressSlots[i].update();
                    }
                }
            }
            else
            {
                double num9 = Rankings.calcHonourForCrownPrince(rankSubLevel);
                int num10 = 0;
                if (num < num9)
                {
                    this.progressSlots[num10].CurrentValue = (int) num;
                    this.progressSlots[num10].MaxValue = (int) num9;
                }
                else
                {
                    this.progressSlots[num10].CurrentValue = (int) num9;
                    this.progressSlots[num10].MaxValue = (int) num9;
                }
                this.progressSlots[num10].update();
            }
            if (newIn && (rank < 0x16))
            {
                for (int j = 0; j < 0x16; j++)
                {
                    CustomSelfDrawPanel.CSDImage image = this.getRankImage(j);
                    int index = j;
                    if (rank > j)
                    {
                        index += 0x16;
                    }
                    else if (rank < j)
                    {
                        index += 0x2c;
                    }
                    image.Image = (Image) GFXLibrary.rank_images[index];
                }
            }
        }

        public static void setRanking(int rank, int rankSubLevel)
        {
            lastRank = rank;
            lastRankSubLevel = rankSubLevel;
            newIn = true;
        }

        private void startRankAnim(int rank)
        {
            if (rank <= GFXLibrary.RankAnim_Images.Length)
            {
                this.majorRankUp = rank;
                if (rank == 0x16)
                {
                    this.cpImage.Image = (Image) GFXLibrary.rank_progression_crown_prince;
                    this.cpImage.Position = new Point(8, 0x35);
                    this.cpImage.Alpha = 0f;
                    this.filledArea.addControl(this.cpImage);
                }
                this.animBack.Size = base.Size;
                this.animBack.clearControls();
                this.filledArea.addControl(this.animBack);
                this.scaledImage.Scale = 1.0;
                if (rank == GFXLibrary.RankAnim_Images.Length)
                {
                    this.scaledImage.Image = (Image) GFXLibrary.RankAnim_Images23;
                }
                else
                {
                    this.scaledImage.Image = (Image) GFXLibrary.RankAnim_Images[rank];
                }
                this.scaledImage.Position = new Point(0, 0);
                this.scaledImage.Alpha = 0f;
                this.startPos = this.unscaledArea.Position = new Point((base.Width - this.scaledImage.Image.Width) / 2, 0);
                this.animBack.addControl(this.unscaledArea);
                this.targetScale = 0.22364217252396165;
                this.zoomCount = 0;
                switch (rank)
                {
                    case 0:
                        this.dx = 0.0;
                        this.dy = 0.0;
                        this.targetScale = 0.2236024844720497;
                        break;

                    case 1:
                        this.dx = -344.0;
                        this.dy = 88.0;
                        this.targetScale = 0.21693121693121692;
                        break;

                    case 2:
                        this.dx = -309.0;
                        this.dy = 85.0;
                        this.targetScale = 0.22419928825622776;
                        break;

                    case 3:
                        this.dx = -271.0;
                        this.dy = 83.0;
                        this.targetScale = 0.22295081967213115;
                        break;

                    case 4:
                        this.dx = -236.0;
                        this.dy = 87.0;
                        this.targetScale = 0.22;
                        break;

                    case 5:
                        this.dx = -201.0;
                        this.dy = 82.0;
                        this.targetScale = 0.22115384615384615;
                        break;

                    case 6:
                        this.dx = -159.0;
                        this.dy = 80.0;
                        this.targetScale = 0.22468354430379747;
                        break;

                    case 7:
                        this.dx = -108.0;
                        this.dy = 75.0;
                        this.targetScale = 0.2260061919504644;
                        break;

                    case 8:
                        this.dx = -59.0;
                        this.dy = 79.0;
                        this.targetScale = 0.22324159021406728;
                        break;

                    case 9:
                        this.dx = -12.0;
                        this.dy = 77.0;
                        this.targetScale = 0.22383720930232559;
                        break;

                    case 10:
                        this.dx = 38.0;
                        this.dy = 78.0;
                        this.targetScale = 0.22287390029325513;
                        break;

                    case 11:
                        this.dx = 84.0;
                        this.dy = 68.0;
                        this.targetScale = 0.22388059701492538;
                        break;

                    case 12:
                        this.dx = 128.0;
                        this.dy = 70.0;
                        this.targetScale = 0.21965317919075145;
                        break;

                    case 13:
                        this.dx = 161.0;
                        this.dy = 71.0;
                        this.targetScale = 0.21902017291066284;
                        break;

                    case 14:
                        this.dx = 202.0;
                        this.dy = 68.0;
                        this.targetScale = 0.21690140845070421;
                        break;

                    case 15:
                        this.dx = 236.0;
                        this.dy = 64.0;
                        this.targetScale = 0.22131147540983606;
                        break;

                    case 0x10:
                        this.dx = 273.0;
                        this.dy = 65.0;
                        this.targetScale = 0.22465753424657534;
                        break;

                    case 0x11:
                        this.dx = 318.0;
                        this.dy = 62.0;
                        this.targetScale = 0.22459893048128343;
                        break;

                    case 0x12:
                        this.dx = 369.0;
                        this.dy = 57.0;
                        this.targetScale = 0.22564102564102564;
                        break;

                    case 0x13:
                        this.dx = 414.0;
                        this.dy = 61.0;
                        this.targetScale = 0.22422680412371135;
                        break;

                    case 20:
                        this.dx = 462.0;
                        this.dy = 35.0;
                        this.targetScale = 0.28333333333333333;
                        break;

                    case 0x15:
                        this.dx = 523.0;
                        this.dy = 45.0;
                        this.targetScale = 0.26112759643916916;
                        break;

                    case 0x16:
                        this.dx = 54.0;
                        this.dy = 50.0;
                        this.targetScale = 0.62650602409638556;
                        break;
                }
                this.numSteps = (int) ((1.0 - this.targetScale) / 0.02);
                this.dx /= (double) this.numSteps;
                this.dy /= (double) this.numSteps;
                this.unscaledArea.addControl(this.scaledImage);
                this.doingRankAnim = true;
                this.fadeCount = 20;
                animating = true;
            }
        }

        public void update()
        {
            this.setCurrentRankings(lastRank, lastRankSubLevel);
            this.updateRankRanim();
        }

        private void updateRankRanim()
        {
            if (this.doingRankAnim)
            {
                if (this.fadeCount > 0)
                {
                    this.fadeCount--;
                    this.scaledImage.Alpha += 0.05f;
                    this.filledArea.FillColor = Color.FromArgb(160 - (this.fadeCount * 8), 0, 0, 0);
                }
                else if (this.fadeCount < 0)
                {
                    this.fadeCount++;
                    this.filledArea.FillColor = Color.FromArgb(-(this.fadeCount * 8), 0, 0, 0);
                    if (this.fadeCount == 0)
                    {
                        this.doingRankAnim = false;
                        this.filledArea.removeControl(this.animBack);
                        this.filledArea.FillColor = Color.FromArgb(0, 0, 0, 0);
                        this.init(false);
                        base.Invalidate();
                        this.setCurrentRankings(this.majorRankUp, 0);
                    }
                }
                else
                {
                    this.zoomCount++;
                    this.scaledImage.Scale = 1.0 - (this.zoomCount * 0.02);
                    this.unscaledArea.Position = new Point(this.startPos.X + ((int) (this.dx * this.zoomCount)), this.startPos.Y + ((int) (this.dy * this.zoomCount)));
                    if (this.zoomCount >= this.numSteps)
                    {
                        this.setCurrentRankings(this.majorRankUp, 0);
                        this.scaledImage.Scale = this.targetScale;
                        this.scaledImage.Alpha = 0f;
                        base.Invalidate();
                        this.fadeCount = -20;
                    }
                    else
                    {
                        if ((this.zoomCount > (this.numSteps - 20)) && (this.cpImage.Alpha < 1f))
                        {
                            this.cpImage.Alpha += 0.05f;
                        }
                        base.Invalidate();
                    }
                }
            }
        }

        private void upgradeClick()
        {
            if (!this.inUpgrade)
            {
                this.ignoreSetCurrent = false;
                this.inUpgrade = true;
                this.upgradeButton.Enabled = false;
                int rankSubLevel = GameEngine.Instance.World.getRankSubLevel();
                int rank = GameEngine.Instance.World.getRank();
                RemoteServices.Instance.set_UpgradeRank_UserCallBack(new RemoteServices.UpgradeRank_UserCallBack(this.upgradeRankCallBack));
                RemoteServices.Instance.UpgradeRank(rank, rankSubLevel);
                if ((rankSubLevel + 1) >= GameEngine.Instance.LocalWorldData.ranks_Levels[rank])
                {
                    Sound.playVillageEnvironmental(20 + rank, false, true);
                    this.ignoreSetCurrent = true;
                    this.startRankAnim(rank + 1);
                    this.fb_newRank = rank + 1;
                    this.facebookShareButton.Visible = true;
                }
                else
                {
                    GameEngine.Instance.playInterfaceSound("RankingsPanel_subrank_up");
                }
            }
        }

        private void upgradeRankCallBack(UpgradeRank_ReturnType returnData)
        {
            this.inUpgrade = false;
            if (returnData.Success)
            {
                GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                InterfaceMgr.Instance.setHonour(returnData.currentHonourLevel, returnData.rank);
                GameEngine.Instance.World.setRanking(returnData.rank, returnData.rankSubLevel);
                if (!this.ignoreSetCurrent)
                {
                    this.init(false);
                    base.Invalidate();
                    this.setCurrentRankings(returnData.rank, returnData.rankSubLevel);
                }
                GameEngine.Instance.World.setResearchData(returnData.researchData);
                InterfaceMgr.Instance.researchDataChanged(returnData.researchData);
                GameEngine.Instance.World.setPoints(returnData.currentPoints);
                if ((returnData.rank == 1) && (GameEngine.Instance.World.getTutorialStage() == 7))
                {
                    GameEngine.Instance.World.forceTutorialToBeShown();
                }
                GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
            }
            else
            {
                this.upgradeButton.Enabled = true;
            }
        }

        private class CSDSlot : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDHorzExtendingPanel back = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
            public CustomSelfDrawPanel.CSDHorzExtendingPanel bar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
            private int currentValue;
            public CustomSelfDrawPanel.CSDImage divider = new CustomSelfDrawPanel.CSDImage();
            private int maxValue = 1;

            public void init(bool green, bool ending)
            {
                this.back.Size = this.Size;
                this.back.Position = new Point(0, 0);
                base.addControl(this.back);
                this.back.Create((Image) GFXLibrary.honour_rank_slot_left, (Image) GFXLibrary.honour_rank_slot_middle, (Image) GFXLibrary.honour_rank_slot_right);
                this.bar.Position = new Point(2, 4);
                this.bar.Size = new Size(base.Width - 4, base.Height - 7);
                base.addControl(this.bar);
                if (green)
                {
                    this.bar.Create((Image) GFXLibrary.honour_rank_slot_green_left, (Image) GFXLibrary.honour_rank_slot_green_middle, (Image) GFXLibrary.honour_rank_slot_green_right);
                }
                else
                {
                    this.bar.Create((Image) GFXLibrary.honour_rank_slot_yellow_left, (Image) GFXLibrary.honour_rank_slot_yellow_middle, (Image) GFXLibrary.honour_rank_slot_yellow_right);
                }
                if (this.currentValue == 0)
                {
                    this.bar.Visible = false;
                }
                this.divider.Image = (Image) GFXLibrary.honour_rank_slot_divider;
                this.divider.Position = new Point(base.Width - 8, 0);
                if (!ending)
                {
                    base.addControl(this.divider);
                }
                this.update();
            }

            public void update()
            {
                if (this.currentValue <= 0)
                {
                    this.bar.Visible = false;
                }
                else
                {
                    this.bar.Visible = true;
                    int num = (((base.Width - 4) - 30) * this.currentValue) / Math.Max(1, this.maxValue);
                    this.bar.Size = new Size(num + 30, base.Height - 7);
                    this.bar.resize();
                }
                base.invalidate();
            }

            public int CurrentValue
            {
                get
                {
                    return this.currentValue;
                }
                set
                {
                    this.currentValue = value;
                }
            }

            public int MaxValue
            {
                get
                {
                    return this.maxValue;
                }
                set
                {
                    this.maxValue = value;
                }
            }
        }
    }
}

