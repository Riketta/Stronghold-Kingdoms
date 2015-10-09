namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class StatsPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel bestRankingsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton bottomButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel categoryDescription = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel categoryLabel = new CustomSelfDrawPanel.CSDLabel();
        private int categoryScrollPos;
        private CustomSelfDrawPanel.CSDButton clearSearchButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private int currentCategory = -1;
        private int currentUserLine = -10000;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton downButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage fixedBarImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton fixedButton1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton fixedButton2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton fixedButton3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton fixedButton4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton fixedButton5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton fixedButton7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel fixedIconBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private Panel focusPanel;
        private bool initialTextInTextbox = true;
        private bool inSearchResults;
        private LeaderBoardSearchResults m_results = new LeaderBoardSearchResults();
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private StatsEntry mainEntry1 = new StatsEntry();
        private StatsEntry mainEntry10 = new StatsEntry();
        private StatsEntry mainEntry11 = new StatsEntry();
        private StatsEntry mainEntry12 = new StatsEntry();
        private StatsEntry mainEntry13 = new StatsEntry();
        private StatsEntry mainEntry14 = new StatsEntry();
        private StatsEntry mainEntry15 = new StatsEntry();
        private StatsEntry mainEntry16 = new StatsEntry();
        private StatsEntry mainEntry17 = new StatsEntry();
        private StatsEntry mainEntry18 = new StatsEntry();
        private StatsEntry mainEntry19 = new StatsEntry();
        private StatsEntry mainEntry2 = new StatsEntry();
        private StatsEntry mainEntry20 = new StatsEntry();
        private StatsEntry mainEntry3 = new StatsEntry();
        private StatsEntry mainEntry4 = new StatsEntry();
        private StatsEntry mainEntry5 = new StatsEntry();
        private StatsEntry mainEntry6 = new StatsEntry();
        private StatsEntry mainEntry7 = new StatsEntry();
        private StatsEntry mainEntry8 = new StatsEntry();
        private StatsEntry mainEntry9 = new StatsEntry();
        private CustomSelfDrawPanel.CSDImage mainInsetBottomImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mainInsetMidImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mainInsetTopBottomImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mainInsetTopMiddleImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mainInsetTopTopImage = new CustomSelfDrawPanel.CSDImage();
        public const int MIN_TEXT_LENGTH_FOR_SEARCH = 4;
        public static int NUM_VISIBLE_LINES = 9;
        private int numExtraTopLines;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel scrollIconBar = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDButton searchButton = new CustomSelfDrawPanel.CSDButton();
        private TextBox searchInput;
        private CustomSelfDrawPanel.CSDImage searchInsetImage = new CustomSelfDrawPanel.CSDImage();
        private int searchLocation;
        private CustomSelfDrawPanel.CSDVertExtendingPanel secondInsetImage = new CustomSelfDrawPanel.CSDVertExtendingPanel();
        private SelfStatsEntry selfEntry1 = new SelfStatsEntry();
        private SelfStatsEntry selfEntry2 = new SelfStatsEntry();
        private SelfStatsEntry selfEntry3 = new SelfStatsEntry();
        private SelfStatsEntry selfEntry4 = new SelfStatsEntry();
        private SelfStatsEntry selfEntry5 = new SelfStatsEntry();
        private SelfStatsEntry selfEntry6 = new SelfStatsEntry();
        private SelfStatsEntry selfEntry7 = new SelfStatsEntry();
        private CustomSelfDrawPanel.CSDButton topButton = new CustomSelfDrawPanel.CSDButton();
        private StatsEntry topEntry1 = new StatsEntry();
        private StatsEntry topEntry2 = new StatsEntry();
        private StatsEntry topEntry3 = new StatsEntry();
        private StatsEntry topEntry4 = new StatsEntry();
        private StatsEntry topEntry5 = new StatsEntry();
        private StatsEntry topEntry6 = new StatsEntry();
        private StatsEntry topEntry7 = new StatsEntry();
        private CustomSelfDrawPanel.CSDButton upButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel updateLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton variButton1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton10 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButton9 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButtonLeft = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton variButtonRight = new CustomSelfDrawPanel.CSDButton();

        public StatsPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            this.searchInput.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, FontStyle.Regular);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void categoryClicked()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                int currentCategory = this.currentCategory;
                if (data < 0)
                {
                    this.currentCategory = data;
                }
                else
                {
                    this.currentCategory = this.mapVariButtonPositionToType(data + this.categoryScrollPos);
                }
                if (currentCategory != this.currentCategory)
                {
                    GameEngine.Instance.playInterfaceSound("StatsPanel_category_changed");
                    this.updateVariIcons();
                    this.newCategory();
                }
            }
        }

        public void categoryLeftClicked()
        {
            if (this.categoryScrollPos > 0)
            {
                this.categoryScrollPos--;
                this.updateVariIcons();
            }
        }

        public void categoryRightClicked()
        {
            if (this.categoryScrollPos < 8)
            {
                this.categoryScrollPos++;
                this.updateVariIcons();
            }
        }

        public void changeCategory(int category)
        {
            if (category != this.currentCategory)
            {
                this.currentCategory = category;
                this.updateVariIcons();
                this.newCategory();
            }
        }

        private void clearSearchClicked()
        {
            this.clearSearchButton.Visible = false;
            this.inSearchResults = false;
            this.updateEntries();
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

        public static string getCategoryDescription(int category)
        {
            switch (category)
            {
                case -6:
                    return SK.Text("Stats_Most_Villages_Owned", "Most Villages owned");

                case -5:
                    return SK.Text("Stats_Highest_Rank", "Highest Rank");

                case -4:
                    return SK.Text("Stats_Most_Parish_Flags", "Most Parish Flags");

                case -3:
                    return SK.Text("Stats_Most_House_Points", "Most House Points");

                case -2:
                    return SK.Text("Stats_Most_Faction_Points", "Most Faction Points");

                case -1:
                    return SK.Text("Stats_Most_Points", "Most points");

                case 0:
                    return SK.Text("Stats_Most_Pillaged", "Most goods pillaged from others");

                case 1:
                    return SK.Text("Stats_Most_Invaders_Killed", "Most invading troops killed at the castle walls");

                case 2:
                    return SK.Text("Stats_Most_Ransacked", "Most buildings ransacked in someone else's castle");

                case 3:
                    return SK.Text("Stats_Most_Wolves_Killed", "Most wolves killed");

                case 4:
                    return SK.Text("Stats_Most_Bandits_Killed", "Most bandits killed");

                case 5:
                    return SK.Text("Stats_Most_AI_Killed", "Most AI Troops Killed");

                case 6:
                    return SK.Text("Stats_Most_Goods_Traded", "Most goods traded");

                case 7:
                    return SK.Text("Stats_Most_Goods_Scouted", "Most goods scouted from the map");

                case 8:
                    return SK.Text("Stats_Most_Stockpike_Goods", "Most stockpile goods produced (updated daily)");

                case 9:
                    return SK.Text("Stats_Most_Food_Produced", "Most foods produced (updated daily)");

                case 10:
                    return SK.Text("Stats_Most_Ale_Produced", "Most Ale produced (updated daily)");

                case 11:
                    return SK.Text("Stats_Most_Weapons_Produced", "Most weapons produced (updated daily)");

                case 12:
                    return SK.Text("Stats_Most_Banquetting_Oroduced", "Most banqueting goods produced (updated daily)");

                case 13:
                    return SK.Text("Stats_Most_Quests_Completed", "Most Quests Completed");

                case 14:
                    return SK.Text("Stats_Most_Dontations", "Most Capital Donations (as 'Packets')");

                case 15:
                    return SK.Text("Stats_Most_Captures", "Most Villages Captured");

                case 0x10:
                    return SK.Text("Stats_Most_Razes", "Most Villages Razed");

                case 0x11:
                    return SK.Text("Stats_Most_Glory", "Most Glory Gained");
            }
            return "";
        }

        public static string getCategoryTitle(int category)
        {
            switch (category)
            {
                case -6:
                    return SK.Text("STATS_CATEGORY_TITLE_NUMVILLAGES", "Villages");

                case -5:
                    return SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");

                case -4:
                    return SK.Text("STATS_CATEGORY_TITLE_PARISH_FLAGS", "Parish Flags");

                case -3:
                    return SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House");

                case -2:
                    return SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");

                case -1:
                    return SK.Text("STATS_CATEGORY_TITLE_POINTS", "Points");

                case 0:
                    return SK.Text("STATS_CATEGORY_TITLE_PILLAGER", "Pillager");

                case 1:
                    return SK.Text("STATS_CATEGORY_TITLE_DEFENDER", "Defender");

                case 2:
                    return SK.Text("STATS_CATEGORY_TITLE_DESTROYER", "Destroyer");

                case 3:
                    return SK.Text("STATS_CATEGORY_TITLE_WOLFSBANE", "Wolfs Bane");

                case 4:
                    return SK.Text("STATS_CATEGORY_TITLE_BANDIT_KILLER", "Bandit Killer");

                case 5:
                    return SK.Text("STATS_CATEGORY_TITLE_AI_KILLER", "AI Killer");

                case 6:
                    return SK.Text("STATS_CATEGORY_TITLE_MERCHANT", "Merchant");

                case 7:
                    return SK.Text("STATS_CATEGORY_TITLE_FORAGER", "Forager");

                case 8:
                    return SK.Text("STATS_CATEGORY_TITLE_WORKER", "Worker");

                case 9:
                    return SK.Text("STATS_CATEGORY_TITLE_FARMER", "Farmer");

                case 10:
                    return SK.Text("STATS_CATEGORY_TITLE_BREWER", "Brewer");

                case 11:
                    return SK.Text("STATS_CATEGORY_TITLE_WEAPONSMITH", "Weaponsmith");

                case 12:
                    return SK.Text("STATS_CATEGORY_TITLE_BANQUETTER", "Banquetter");

                case 13:
                    return SK.Text("STATS_CATEGORY_TITLE_QUESTER", "Quester");

                case 14:
                    return SK.Text("STATS_CATEGORY_TITLE_DONATER", "Donator");

                case 15:
                    return SK.Text("STATS_CATEGORY_TITLE_CAPTURE", "Conqueror");

                case 0x10:
                    return SK.Text("STATS_CATEGORY_TITLE_RAZE", "Annihilator");

                case 0x11:
                    return SK.Text("STATS_CATEGORY_TITLE_GLORY", "Glory Hunter");
            }
            return "";
        }

        private int getMaxSearchResults()
        {
            return this.m_results.entries.Count;
        }

        private int getSearchEntry(int entryID)
        {
            if (entryID >= this.m_results.entries.Count)
            {
                return -999999;
            }
            return this.m_results.entries[entryID];
        }

        private CustomSelfDrawPanel.CSDButton getVariButton(int i)
        {
            switch (i)
            {
                case 0:
                    return this.variButton1;

                case 1:
                    return this.variButton2;

                case 2:
                    return this.variButton3;

                case 3:
                    return this.variButton4;

                case 4:
                    return this.variButton5;

                case 5:
                    return this.variButton6;

                case 6:
                    return this.variButton7;

                case 7:
                    return this.variButton8;

                case 8:
                    return this.variButton9;

                case 9:
                    return this.variButton10;
            }
            return this.variButton1;
        }

        public void init(bool resized)
        {
            base.clearControls();
            this.focusPanel.Focus();
            if (!resized)
            {
                this.currentUserLine = -10000;
                this.initialTextInTextbox = true;
                this.searchInput.Text = SK.Text("Stats_Seaarch", "Search");
                this.inSearchResults = false;
            }
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.categoryLabel.Text = "[" + SK.Text("Stats_Category", "Category") + "]";
            this.categoryLabel.Color = ARGBColors.White;
            this.categoryLabel.DropShadowColor = ARGBColors.Black;
            this.categoryLabel.Position = new Point(0x23, 11);
            this.categoryLabel.Size = new Size(300, 0x23);
            this.categoryLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.categoryLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.categoryLabel);
            this.categoryDescription.Text = "[" + SK.Text("Stats_Description", "Description") + "]";
            this.categoryDescription.Color = ARGBColors.White;
            this.categoryDescription.DropShadowColor = ARGBColors.Black;
            if ((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "it"))
            {
                this.categoryDescription.Position = new Point(100, 3);
                this.categoryDescription.Size = new Size(360, 50);
                this.categoryDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                this.categoryDescription.Position = new Point(100, 3);
                this.categoryDescription.Size = new Size(300, 50);
                this.categoryDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            }
            else
            {
                this.categoryDescription.Position = new Point(100, 0x12);
                this.categoryDescription.Size = new Size(500, 30);
                this.categoryDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            }
            this.categoryDescription.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.mainBackgroundImage.addControl(this.categoryDescription);
            int num = base.Height - 0x252;
            this.mainInsetTopTopImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_top_top;
            this.mainInsetTopTopImage.Position = new Point(30, 0x67);
            this.mainBackgroundImage.addControl(this.mainInsetTopTopImage);
            this.mainInsetTopMiddleImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_top_middle;
            this.mainInsetTopMiddleImage.Position = new Point(30, 0x8f);
            this.mainInsetTopMiddleImage.Size = new Size(this.mainInsetTopMiddleImage.Image.Width, 90 + (num / 2));
            this.mainBackgroundImage.addControl(this.mainInsetTopMiddleImage);
            this.mainInsetTopBottomImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_top_bottom;
            this.mainInsetTopBottomImage.Position = new Point(30, 0xe9 + (num / 2));
            this.mainBackgroundImage.addControl(this.mainInsetTopBottomImage);
            this.mainInsetMidImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_middle;
            this.mainInsetMidImage.Position = new Point(30, 0x134 + (num / 2));
            this.mainInsetMidImage.Size = new Size(this.mainInsetMidImage.Image.Width, 0xde + (num / 2));
            this.mainBackgroundImage.addControl(this.mainInsetMidImage);
            this.mainInsetBottomImage.Image = (Image) GFXLibrary.int_statsscreen_maininset_bottom;
            this.mainInsetBottomImage.Position = new Point(30, 530 + ((num / 2) * 2));
            this.mainBackgroundImage.addControl(this.mainInsetBottomImage);
            this.searchInsetImage.Image = (Image) GFXLibrary.int_statsscreen_search_inset;
            this.searchInsetImage.Position = new Point(0x27e, 9);
            this.mainBackgroundImage.addControl(this.searchInsetImage);
            this.searchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_button_normal;
            this.searchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_button_over;
            this.searchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_button_pushed;
            this.searchButton.Position = new Point(0xf4, 4);
            this.searchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchClicked), "StatsPanel_search");
            this.searchInsetImage.addControl(this.searchButton);
            this.clearSearchButton.ImageNorm = (Image) GFXLibrary.int_statsscreen_search_clear_button_normal;
            this.clearSearchButton.ImageOver = (Image) GFXLibrary.int_statsscreen_search_clear_button_over;
            this.clearSearchButton.ImageClick = (Image) GFXLibrary.int_statsscreen_search_clear_button_pushed;
            this.clearSearchButton.Position = new Point(0xf1 - this.clearSearchButton.ImageNorm.Size.Width, 4);
            this.clearSearchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearSearchClicked), "StatsPanel_clear_search");
            this.clearSearchButton.Visible = this.inSearchResults;
            this.searchInsetImage.addControl(this.clearSearchButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x1c, new Point(0x256, 8));
            this.fixedIconBar.Position = new Point(0x25, 0x39);
            this.fixedIconBar.Size = new Size(0x13a, 1);
            this.mainBackgroundImage.addControl(this.fixedIconBar);
            this.fixedIconBar.Create((Image) GFXLibrary.int_statsscreen_iconbar_left, (Image) GFXLibrary.int_statsscreen_iconbar_middle, (Image) GFXLibrary.int_statsscreen_iconbar_right);
            this.scrollIconBar.Position = new Point(0x177, 0x39);
            this.scrollIconBar.Size = new Size(0x23c, 1);
            this.mainBackgroundImage.addControl(this.scrollIconBar);
            this.scrollIconBar.Create((Image) GFXLibrary.int_statsscreen_iconbar_left, (Image) GFXLibrary.int_statsscreen_iconbar_middle, (Image) GFXLibrary.int_statsscreen_iconbar_right);
            this.fixedButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.fixedButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.fixedButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.fixedButton1.Position = new Point(-14, -7);
            this.fixedButton1.Data = -1;
            this.fixedButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.fixedButton1.CustomTooltipID = 0x514;
            this.fixedButton1.CustomTooltipData = -1;
            this.fixedIconBar.addControl(this.fixedButton1);
            this.fixedButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_rank_normal;
            this.fixedButton2.ImageOver = (Image) GFXLibrary.catagory_icons_rank_over;
            this.fixedButton2.ImageClick = (Image) GFXLibrary.catagory_icons_rank_pushed;
            this.fixedButton2.Position = new Point(0x2e, -7);
            this.fixedButton2.Data = -5;
            this.fixedButton2.CustomTooltipID = 0x514;
            this.fixedButton2.CustomTooltipData = -5;
            this.fixedButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.fixedIconBar.addControl(this.fixedButton2);
            this.fixedButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_villages_normal;
            this.fixedButton3.ImageOver = (Image) GFXLibrary.catagory_icons_villages_over;
            this.fixedButton3.ImageClick = (Image) GFXLibrary.catagory_icons_villages_pushed;
            this.fixedButton3.Position = new Point(0x6a, -7);
            this.fixedButton3.Data = -6;
            this.fixedButton3.CustomTooltipID = 0x514;
            this.fixedButton3.CustomTooltipData = -6;
            this.fixedButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.fixedIconBar.addControl(this.fixedButton3);
            this.fixedButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_factions_normal;
            this.fixedButton4.ImageOver = (Image) GFXLibrary.catagory_icons_factions_over;
            this.fixedButton4.ImageClick = (Image) GFXLibrary.catagory_icons_factions_pushed;
            this.fixedButton4.Position = new Point(0xa6, -7);
            this.fixedButton4.Data = -2;
            this.fixedButton4.CustomTooltipID = 0x514;
            this.fixedButton4.CustomTooltipData = -2;
            this.fixedButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.fixedIconBar.addControl(this.fixedButton4);
            this.fixedButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_houses_normal;
            this.fixedButton5.ImageOver = (Image) GFXLibrary.catagory_icons_houses_over;
            this.fixedButton5.ImageClick = (Image) GFXLibrary.catagory_icons_houses_pushed;
            this.fixedButton5.Position = new Point(0xe2, -7);
            this.fixedButton5.Data = -3;
            this.fixedButton5.CustomTooltipID = 0x514;
            this.fixedButton5.CustomTooltipData = -3;
            this.fixedButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.fixedIconBar.addControl(this.fixedButton5);
            this.fixedButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_parishflags_normal;
            this.fixedButton7.ImageOver = (Image) GFXLibrary.catagory_icons_parishflags_over;
            this.fixedButton7.ImageClick = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
            this.fixedButton7.Position = new Point(0x11e, -7);
            this.fixedButton7.Data = -4;
            this.fixedButton7.CustomTooltipID = 0x514;
            this.fixedButton7.CustomTooltipData = -4;
            this.fixedButton7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.fixedIconBar.addControl(this.fixedButton7);
            this.variButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton1.Position = new Point(0x26, -7);
            this.variButton1.Data = 0;
            this.variButton1.CustomTooltipID = 0x514;
            this.variButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton1);
            this.variButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton2.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton2.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton2.Position = new Point(0x58, -7);
            this.variButton2.Data = 1;
            this.variButton2.CustomTooltipID = 0x514;
            this.variButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton2);
            this.variButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton3.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton3.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton3.Position = new Point(0x8a, -7);
            this.variButton3.Data = 2;
            this.variButton3.CustomTooltipID = 0x514;
            this.variButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton3);
            this.variButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton4.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton4.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton4.Position = new Point(0xbc, -7);
            this.variButton4.Data = 3;
            this.variButton4.CustomTooltipID = 0x514;
            this.variButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton4);
            this.variButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton5.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton5.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton5.Position = new Point(0xee, -7);
            this.variButton5.Data = 4;
            this.variButton5.CustomTooltipID = 0x514;
            this.variButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton5);
            this.variButton6.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton6.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton6.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton6.Position = new Point(0x120, -7);
            this.variButton6.Data = 5;
            this.variButton6.CustomTooltipID = 0x514;
            this.variButton6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton6);
            this.variButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton7.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton7.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton7.Position = new Point(0x152, -7);
            this.variButton7.Data = 6;
            this.variButton7.CustomTooltipID = 0x514;
            this.variButton7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton7);
            this.variButton8.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton8.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton8.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton8.Position = new Point(0x184, -7);
            this.variButton8.Data = 7;
            this.variButton8.CustomTooltipID = 0x514;
            this.variButton8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton8);
            this.variButton9.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton9.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton9.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton9.Position = new Point(0x1b6, -7);
            this.variButton9.Data = 8;
            this.variButton9.CustomTooltipID = 0x514;
            this.variButton9.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton9);
            this.variButton10.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.variButton10.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.variButton10.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.variButton10.Position = new Point(0x1e8, -7);
            this.variButton10.Data = 9;
            this.variButton10.CustomTooltipID = 0x514;
            this.variButton10.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryClicked));
            this.scrollIconBar.addControl(this.variButton10);
            this.variButtonLeft.ImageNorm = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_left_normal;
            this.variButtonLeft.ImageOver = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_left_over;
            this.variButtonLeft.ImageClick = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_left_pressed;
            this.variButtonLeft.Position = new Point(6, -10);
            this.variButtonLeft.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryLeftClicked), "StatsPanel_category_left");
            this.scrollIconBar.addControl(this.variButtonLeft);
            this.variButtonRight.ImageNorm = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_right_normal;
            this.variButtonRight.ImageOver = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_right_over;
            this.variButtonRight.ImageClick = (Image) GFXLibrary.int_statsscreen_iconbar_arrow_right_pressed;
            this.variButtonRight.Position = new Point(0x21a, -10);
            this.variButtonRight.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.categoryRightClicked), "StatsPanel_category_right");
            this.scrollIconBar.addControl(this.variButtonRight);
            this.mainEntry1.Position = new Point(0x27, 0x72);
            this.mainBackgroundImage.addControl(this.mainEntry1);
            this.mainEntry2.Position = new Point(0x27, 0xa3);
            this.mainBackgroundImage.addControl(this.mainEntry2);
            this.mainEntry3.Position = new Point(0x27, 0xd4);
            this.mainBackgroundImage.addControl(this.mainEntry3);
            this.mainEntry4.Position = new Point(0x27, 0x105);
            this.mainBackgroundImage.addControl(this.mainEntry4);
            this.mainEntry5.Position = new Point(0x27, 310);
            this.mainBackgroundImage.addControl(this.mainEntry5);
            this.mainEntry6.Position = new Point(0x27, 0x167);
            this.mainBackgroundImage.addControl(this.mainEntry6);
            this.mainEntry7.Position = new Point(0x27, 0x198);
            this.mainBackgroundImage.addControl(this.mainEntry7);
            this.mainEntry8.Position = new Point(0x27, 0x1c9);
            this.mainBackgroundImage.addControl(this.mainEntry8);
            this.mainEntry9.Position = new Point(0x27, 0x1fa);
            this.mainBackgroundImage.addControl(this.mainEntry9);
            this.mainEntry10.Position = new Point(0x27, 0x22b);
            this.mainBackgroundImage.addControl(this.mainEntry10);
            this.mainEntry11.Position = new Point(0x27, 0x25c);
            this.mainBackgroundImage.addControl(this.mainEntry11);
            this.mainEntry12.Position = new Point(0x27, 0x28d);
            this.mainBackgroundImage.addControl(this.mainEntry12);
            this.mainEntry13.Position = new Point(0x27, 0x2be);
            this.mainBackgroundImage.addControl(this.mainEntry13);
            this.mainEntry14.Position = new Point(0x27, 0x2ef);
            this.mainBackgroundImage.addControl(this.mainEntry14);
            this.mainEntry15.Position = new Point(0x27, 800);
            this.mainBackgroundImage.addControl(this.mainEntry15);
            this.mainEntry16.Position = new Point(0x27, 0x351);
            this.mainBackgroundImage.addControl(this.mainEntry16);
            this.mainEntry17.Position = new Point(0x27, 0x382);
            this.mainBackgroundImage.addControl(this.mainEntry17);
            this.mainEntry18.Position = new Point(0x27, 0x3b3);
            this.mainBackgroundImage.addControl(this.mainEntry18);
            this.mainEntry19.Position = new Point(0x27, 0x3e4);
            this.mainBackgroundImage.addControl(this.mainEntry19);
            this.mainEntry20.Position = new Point(0x27, 0x415);
            this.mainBackgroundImage.addControl(this.mainEntry20);
            this.topEntry1.Position = new Point(0x210, 0x72);
            this.topEntry1.setAsTopEntry();
            this.mainBackgroundImage.addControl(this.topEntry1);
            this.topEntry2.Position = new Point(0x210, 0xa3);
            this.topEntry2.setAsTopEntry();
            this.mainBackgroundImage.addControl(this.topEntry2);
            this.topEntry3.Position = new Point(0x210, 0xd4);
            this.topEntry3.setAsTopEntry();
            this.mainBackgroundImage.addControl(this.topEntry3);
            this.topEntry4.Position = new Point(0x210, 0x105);
            this.topEntry4.setAsTopEntry();
            this.mainBackgroundImage.addControl(this.topEntry4);
            this.topEntry5.Position = new Point(0x210, 310);
            this.topEntry5.setAsTopEntry();
            this.mainBackgroundImage.addControl(this.topEntry5);
            this.topEntry6.Position = new Point(0x210, 0x167);
            this.topEntry6.setAsTopEntry();
            this.mainBackgroundImage.addControl(this.topEntry6);
            this.topEntry7.Position = new Point(0x210, 0x198);
            this.topEntry7.setAsTopEntry();
            this.mainBackgroundImage.addControl(this.topEntry7);
            NUM_VISIBLE_LINES = 9 + (num / 0x31);
            this.numExtraTopLines = (num / 2) / 0x31;
            if (!resized)
            {
                this.mainEntry1.init(this.currentCategory, -1000, 0);
                this.mainEntry2.init(this.currentCategory, -1001, 1);
                this.mainEntry3.init(this.currentCategory, -1002, 2);
                this.mainEntry4.init(this.currentCategory, -1003, 3);
                this.mainEntry5.init(this.currentCategory, -1004, 4);
                this.mainEntry6.init(this.currentCategory, -1005, 5);
                this.mainEntry7.init(this.currentCategory, -1006, 6);
                this.mainEntry8.init(this.currentCategory, -1007, 7);
                this.mainEntry9.init(this.currentCategory, -1008, 8);
                if (NUM_VISIBLE_LINES >= 10)
                {
                    this.mainEntry10.init(this.currentCategory, -1009, 9);
                }
                if (NUM_VISIBLE_LINES >= 11)
                {
                    this.mainEntry11.init(this.currentCategory, -1009, 10);
                }
                if (NUM_VISIBLE_LINES >= 12)
                {
                    this.mainEntry12.init(this.currentCategory, -1009, 11);
                }
                if (NUM_VISIBLE_LINES >= 13)
                {
                    this.mainEntry13.init(this.currentCategory, -1009, 12);
                }
                if (NUM_VISIBLE_LINES >= 14)
                {
                    this.mainEntry14.init(this.currentCategory, -1009, 13);
                }
                if (NUM_VISIBLE_LINES >= 15)
                {
                    this.mainEntry15.init(this.currentCategory, -1009, 14);
                }
                if (NUM_VISIBLE_LINES >= 0x10)
                {
                    this.mainEntry16.init(this.currentCategory, -1009, 15);
                }
                if (NUM_VISIBLE_LINES >= 0x11)
                {
                    this.mainEntry17.init(this.currentCategory, -1009, 0x10);
                }
                if (NUM_VISIBLE_LINES >= 0x12)
                {
                    this.mainEntry18.init(this.currentCategory, -1009, 0x11);
                }
                if (NUM_VISIBLE_LINES >= 0x13)
                {
                    this.mainEntry19.init(this.currentCategory, -1009, 0x12);
                }
                if (NUM_VISIBLE_LINES >= 20)
                {
                    this.mainEntry20.init(this.currentCategory, -1009, 0x13);
                }
                this.topEntry1.init(this.currentCategory, 1, 0);
                this.topEntry2.init(this.currentCategory, 2, 1);
                this.topEntry3.init(this.currentCategory, 3, 2);
                if (this.numExtraTopLines > 0)
                {
                    this.topEntry4.init(this.currentCategory, 4, 3);
                }
                if (this.numExtraTopLines > 1)
                {
                    this.topEntry5.init(this.currentCategory, 5, 4);
                }
                if (this.numExtraTopLines > 2)
                {
                    this.topEntry6.init(this.currentCategory, 6, 5);
                }
                if (this.numExtraTopLines > 3)
                {
                    this.topEntry7.init(this.currentCategory, 7, 6);
                }
            }
            else
            {
                this.updateEntries();
            }
            int num2 = num / 2;
            this.secondInsetImage.Position = new Point(0x228, 0x13c + num2);
            this.secondInsetImage.Size = new Size(1, 0xf8 + (num / 2));
            this.mainBackgroundImage.addControl(this.secondInsetImage);
            this.secondInsetImage.Create((Image) GFXLibrary.int_statsscreen_secondinset_top, (Image) GFXLibrary.int_statsscreen_secondinset_middle, (Image) GFXLibrary.int_statsscreen_secondinset_bottom);
            this.selfEntry1.Position = new Point(0x23c, 0x152 + num2);
            this.selfEntry1.init(0, this);
            this.mainBackgroundImage.addControl(this.selfEntry1);
            this.selfEntry2.Position = new Point(0x23c, 0x198 + num2);
            this.selfEntry2.init(1, this);
            this.mainBackgroundImage.addControl(this.selfEntry2);
            this.selfEntry3.Position = new Point(0x23c, 0x1de + num2);
            this.selfEntry3.init(2, this);
            this.mainBackgroundImage.addControl(this.selfEntry3);
            this.selfEntry4.Position = new Point(0x23c, 0x224 + num2);
            this.selfEntry4.init(3, this);
            this.mainBackgroundImage.addControl(this.selfEntry4);
            this.selfEntry5.Position = new Point(0x23c, 0x26a + num2);
            this.selfEntry5.init(4, this);
            this.mainBackgroundImage.addControl(this.selfEntry5);
            this.selfEntry6.Position = new Point(0x23c, 0x2b0 + num2);
            this.selfEntry6.init(5, this);
            this.mainBackgroundImage.addControl(this.selfEntry6);
            this.selfEntry7.Position = new Point(0x23c, 0x2f6 + num2);
            this.selfEntry7.init(6, this);
            this.mainBackgroundImage.addControl(this.selfEntry7);
            int num3 = (num / 2) / 70;
            this.selfEntry4.Visible = num3 >= 1;
            this.selfEntry5.Visible = num3 >= 2;
            this.selfEntry6.Visible = num3 >= 3;
            this.selfEntry7.Visible = num3 >= 4;
            this.topButton.ImageNorm = (Image) GFXLibrary.page_top_norrmal;
            this.topButton.ImageOver = (Image) GFXLibrary.page_top_over;
            this.topButton.ImageClick = (Image) GFXLibrary.page_top_pushed;
            this.topButton.Position = new Point(0x1d0, 0x72);
            this.topButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollToTopClicked), "StatsPanel_scroll_top");
            this.mainBackgroundImage.addControl(this.topButton);
            this.upButton.ImageNorm = (Image) GFXLibrary.page_up_normal;
            this.upButton.ImageOver = (Image) GFXLibrary.page_up_over;
            this.upButton.ImageClick = (Image) GFXLibrary.page_up_pushed;
            this.upButton.Position = new Point(0x1d0, (this.topButton.Position.Y + 2) + this.topButton.ImageNorm.Height);
            this.upButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollUpClicked), "StatsPanel_scroll_up");
            this.mainBackgroundImage.addControl(this.upButton);
            this.bottomButton.ImageNorm = (Image) GFXLibrary.page_bottom_normal;
            this.bottomButton.ImageOver = (Image) GFXLibrary.page_bottom_over;
            this.bottomButton.ImageClick = (Image) GFXLibrary.page_bottom_pushed;
            this.bottomButton.Position = new Point(0x1d0, (base.Height - 0x2a) - this.bottomButton.ImageNorm.Height);
            this.bottomButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollToBottomClicked), "StatsPanel_scroll_bottom");
            this.mainBackgroundImage.addControl(this.bottomButton);
            this.downButton.ImageNorm = (Image) GFXLibrary.page_down_normal;
            this.downButton.ImageOver = (Image) GFXLibrary.page_down_over;
            this.downButton.ImageClick = (Image) GFXLibrary.page_down_pushed;
            this.downButton.Position = new Point(0x1d0, (this.bottomButton.Position.Y - 2) - this.downButton.ImageNorm.Height);
            this.downButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scrollDownClicked), "StatsPanel_scroll_down");
            this.mainBackgroundImage.addControl(this.downButton);
            this.updateLabel.Text = "";
            this.updateLabel.Color = ARGBColors.Black;
            this.updateLabel.Position = new Point(50, base.Height - 0x16);
            this.updateLabel.Size = new Size(500, 0x19);
            this.updateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.updateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.updateLabel);
            this.bestRankingsLabel.Text = SK.Text("Stats_Best_Ranking", "Your Best Rankings");
            this.bestRankingsLabel.Color = ARGBColors.White;
            this.bestRankingsLabel.Position = new Point(570, 0x124 + (num / 2));
            this.bestRankingsLabel.Size = new Size(300, 0x19);
            this.bestRankingsLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.bestRankingsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.bestRankingsLabel);
            this.updateVariIcons();
            if (!resized)
            {
                this.newCategory();
            }
        }

        private void InitializeComponent()
        {
            this.searchInput = new TextBox();
            this.focusPanel = new Panel();
            base.SuspendLayout();
            this.searchInput.BackColor = Color.FromArgb(140, 0x99, 0xa1);
            this.searchInput.BorderStyle = BorderStyle.None;
            this.searchInput.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.searchInput.Location = new Point(0x28d, 15);
            this.searchInput.MaxLength = 40;
            this.searchInput.Multiline = true;
            this.searchInput.Name = "searchInput";
            this.searchInput.Size = new Size(200, 0x16);
            this.searchInput.TabIndex = 100;
            this.searchInput.Text = "Search";
            this.searchInput.WordWrap = false;
            this.searchInput.KeyPress += new KeyPressEventHandler(this.searchInput_KeyPress);
            this.searchInput.Enter += new EventHandler(this.searchInput_Enter);
            this.focusPanel.BackColor = ARGBColors.Transparent;
            this.focusPanel.ForeColor = ARGBColors.Transparent;
            this.focusPanel.Location = new Point(0x3dc, 3);
            this.focusPanel.Name = "focusPanel";
            this.focusPanel.Size = new Size(1, 1);
            this.focusPanel.TabIndex = 1;
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.focusPanel);
            base.Controls.Add(this.searchInput);
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x252);
            base.Name = "StatsPanel";
            base.Size = new Size(0x3e0, 0x252);
            base.ResumeLayout(false);
            base.PerformLayout();
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

        public int mapVariButtonPositionToType(int position)
        {
            return position;
        }

        public void newCategory()
        {
            this.inSearchResults = false;
            if (GameEngine.Instance.World.isLeaderboardCategoryPopulated(this.currentCategory))
            {
                this.currentUserLine = GameEngine.Instance.World.findSelfInLeaderboard(this.currentCategory) - (NUM_VISIBLE_LINES / 2);
                int num = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
                if (this.currentUserLine > (num - (NUM_VISIBLE_LINES - 1)))
                {
                    this.currentUserLine = num - (NUM_VISIBLE_LINES - 1);
                }
                if (this.currentUserLine < 1)
                {
                    this.currentUserLine = 1;
                }
            }
            else
            {
                this.currentUserLine = -1000;
            }
            this.updateEntries();
        }

        public void scrollDownClicked()
        {
            if (!GameEngine.Instance.World.downloadingLeaderboard())
            {
                if (!this.inSearchResults)
                {
                    int num = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
                    int position = this.currentUserLine + NUM_VISIBLE_LINES;
                    if (position > (num - (NUM_VISIBLE_LINES - 1)))
                    {
                        position = num - (NUM_VISIBLE_LINES - 1);
                    }
                    if (position < 1)
                    {
                        position = 1;
                    }
                    if (position != this.currentUserLine)
                    {
                        this.currentUserLine = position;
                        this.updateEntries();
                        if (!GameEngine.Instance.World.downloadingLeaderboard())
                        {
                            GameEngine.Instance.World.leaderboardLookLower(this.currentCategory, position, NUM_VISIBLE_LINES);
                        }
                    }
                }
                else
                {
                    int num3 = this.getMaxSearchResults() - 1;
                    int num4 = this.searchLocation + NUM_VISIBLE_LINES;
                    if (num4 > (num3 - NUM_VISIBLE_LINES))
                    {
                        num4 = num3 - (NUM_VISIBLE_LINES - 1);
                        if (num4 < 0)
                        {
                            num4 = 0;
                        }
                    }
                    if (num4 != this.searchLocation)
                    {
                        this.searchLocation = num4;
                        this.updateEntries();
                    }
                }
            }
        }

        public void scrollToBottomClicked()
        {
            if (!GameEngine.Instance.World.downloadingLeaderboard())
            {
                if (!this.inSearchResults)
                {
                    int num = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
                    if ((num - (NUM_VISIBLE_LINES - 1)) != this.currentUserLine)
                    {
                        this.currentUserLine = num - (NUM_VISIBLE_LINES - 1);
                        if (this.currentUserLine > 1)
                        {
                            this.updateEntries();
                        }
                        else
                        {
                            this.currentUserLine = 1;
                        }
                    }
                }
                else
                {
                    int num2 = this.getMaxSearchResults() - 1;
                    if ((num2 - (NUM_VISIBLE_LINES - 1)) != this.searchLocation)
                    {
                        this.searchLocation = num2 - (NUM_VISIBLE_LINES - 1);
                        if (this.searchLocation < 0)
                        {
                            this.searchLocation = 0;
                        }
                        this.updateEntries();
                    }
                }
            }
        }

        public void scrollToTopClicked()
        {
            if (!GameEngine.Instance.World.downloadingLeaderboard())
            {
                if (!this.inSearchResults)
                {
                    if (this.currentUserLine != 1)
                    {
                        this.currentUserLine = 1;
                        this.updateEntries();
                    }
                }
                else if (this.searchLocation != 0)
                {
                    this.searchLocation = 0;
                    this.updateEntries();
                }
            }
        }

        public void scrollUpClicked()
        {
            if (!GameEngine.Instance.World.downloadingLeaderboard())
            {
                if (!this.inSearchResults)
                {
                    int position = this.currentUserLine - NUM_VISIBLE_LINES;
                    if (position < 1)
                    {
                        position = 1;
                    }
                    if (position != this.currentUserLine)
                    {
                        this.currentUserLine = position;
                        this.updateEntries();
                        if (!GameEngine.Instance.World.downloadingLeaderboard() && (position != 1))
                        {
                            GameEngine.Instance.World.leaderboardLookHigher(this.currentCategory, position, NUM_VISIBLE_LINES);
                        }
                    }
                }
                else
                {
                    int num2 = this.searchLocation - NUM_VISIBLE_LINES;
                    if (num2 < 0)
                    {
                        num2 = 0;
                    }
                    if (num2 != this.searchLocation)
                    {
                        this.searchLocation = num2;
                        this.updateEntries();
                    }
                }
            }
        }

        public void searchClicked()
        {
            if (!GameEngine.Instance.World.downloadingLeaderboard())
            {
                if (((this.searchInput.Text.Length >= 4) && (this.currentCategory != -3)) && ((this.currentCategory != -4) && !this.initialTextInTextbox))
                {
                    GameEngine.Instance.World.leaderboardSearch(this.currentCategory, this.searchInput.Text);
                }
                else if (this.initialTextInTextbox || (this.searchInput.Text.Length == 0))
                {
                    this.inSearchResults = false;
                    this.updateEntries();
                }
            }
        }

        public void searchComplete(LeaderBoardSearchResults results)
        {
            this.m_results = results;
            this.inSearchResults = true;
            this.searchLocation = 0;
            this.currentUserLine = 1;
            this.updateEntries();
        }

        private void searchInput_Enter(object sender, EventArgs e)
        {
            if (this.initialTextInTextbox)
            {
                this.initialTextInTextbox = false;
                this.searchInput.Text = "";
            }
        }

        private void searchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.searchClicked();
                e.Handled = true;
            }
        }

        public void setButtonGFX(CustomSelfDrawPanel.CSDButton button, int position)
        {
            int num = this.mapVariButtonPositionToType(position);
            button.CustomTooltipData = num;
            BaseImage image = null;
            BaseImage image2 = null;
            BaseImage image3 = null;
            button.Enabled = true;
            switch (num)
            {
                case 0:
                    image = GFXLibrary.catagory_icons_pillager_normal;
                    image2 = GFXLibrary.catagory_icons_pillager_over;
                    image3 = GFXLibrary.catagory_icons_pillager_pushed;
                    break;

                case 1:
                    image = GFXLibrary.catagory_icons_defender_normal;
                    image2 = GFXLibrary.catagory_icons_defender_over;
                    image3 = GFXLibrary.catagory_icons_defender_pushed;
                    break;

                case 2:
                    image = GFXLibrary.catagory_icons_destroyer_normal;
                    image2 = GFXLibrary.catagory_icons_destroyer_over;
                    image3 = GFXLibrary.catagory_icons_destroyer_pushed;
                    break;

                case 3:
                    image = GFXLibrary.catagory_icons_wolfbane_normal;
                    image2 = GFXLibrary.catagory_icons_wolfbane_over;
                    image3 = GFXLibrary.catagory_icons_wolfbane_pushed;
                    break;

                case 4:
                    image = GFXLibrary.catagory_icons_banditslayer_normal;
                    image2 = GFXLibrary.catagory_icons_banditslayer_over;
                    image3 = GFXLibrary.catagory_icons_banditslayer_pushed;
                    break;

                case 5:
                    image = GFXLibrary.catagory_icons_aikiller_normal;
                    image2 = GFXLibrary.catagory_icons_aikiller_over;
                    image3 = GFXLibrary.catagory_icons_aikiller_pushed;
                    break;

                case 6:
                    image = GFXLibrary.catagory_icons_merchant_normal;
                    image2 = GFXLibrary.catagory_icons_merchant_over;
                    image3 = GFXLibrary.catagory_icons_merchant_pushed;
                    break;

                case 7:
                    image = GFXLibrary.catagory_icons_forger_normal;
                    image2 = GFXLibrary.catagory_icons_forger_over;
                    image3 = GFXLibrary.catagory_icons_forger_pushed;
                    break;

                case 8:
                    image = GFXLibrary.catagory_icons_worker_normal;
                    image2 = GFXLibrary.catagory_icons_worker_over;
                    image3 = GFXLibrary.catagory_icons_worker_pushed;
                    break;

                case 9:
                    image = GFXLibrary.catagory_icons_farmer_normal;
                    image2 = GFXLibrary.catagory_icons_farmer_over;
                    image3 = GFXLibrary.catagory_icons_farmer_pushed;
                    break;

                case 10:
                    image = GFXLibrary.catagory_icons_brewer_normal;
                    image2 = GFXLibrary.catagory_icons_brewer_over;
                    image3 = GFXLibrary.catagory_icons_brewer_pushed;
                    break;

                case 11:
                    image = GFXLibrary.catagory_icons_blacksmith_normal;
                    image2 = GFXLibrary.catagory_icons_blacksmith_over;
                    image3 = GFXLibrary.catagory_icons_blacksmith_pushed;
                    break;

                case 12:
                    image = GFXLibrary.catagory_icons_banquet_normal;
                    image2 = GFXLibrary.catagory_icons_banquet_over;
                    image3 = GFXLibrary.catagory_icons_banquet_pushed;
                    break;

                case 13:
                    image = GFXLibrary.catagory_icons_achiever_normal;
                    image2 = GFXLibrary.catagory_icons_achiever_over;
                    image3 = GFXLibrary.catagory_icons_achiever_pushed;
                    break;

                case 14:
                    image = GFXLibrary.catagory_icons_donator_normal;
                    image2 = GFXLibrary.catagory_icons_donator_over;
                    image3 = GFXLibrary.catagory_icons_donator_pushed;
                    break;

                case 15:
                    image = GFXLibrary.catagory_icons_capture_normal;
                    image2 = GFXLibrary.catagory_icons_capture_over;
                    image3 = GFXLibrary.catagory_icons_capture_pushed;
                    break;

                case 0x10:
                    image = GFXLibrary.catagory_icons_raze_normal;
                    image2 = GFXLibrary.catagory_icons_raze_over;
                    image3 = GFXLibrary.catagory_icons_raze_pushed;
                    break;

                case 0x11:
                    image = GFXLibrary.catagory_icons_glory_normal;
                    image2 = GFXLibrary.catagory_icons_glory_over;
                    image3 = GFXLibrary.catagory_icons_glory_pushed;
                    break;
            }
            if (num == this.currentCategory)
            {
                button.ImageNorm = (Image) image3;
                button.ImageOver = (Image) image3;
                button.ImageClick = (Image) image3;
            }
            else
            {
                button.ImageNorm = (Image) image;
                button.ImageOver = (Image) image2;
                button.ImageClick = (Image) image3;
            }
            this.categoryLabel.Text = getCategoryTitle(this.currentCategory);
            this.categoryDescription.Text = getCategoryDescription(this.currentCategory);
            Graphics graphics = base.CreateGraphics();
            Size size = graphics.MeasureString(this.categoryLabel.Text, this.categoryLabel.Font, 0x186a0).ToSize();
            graphics.Dispose();
            this.categoryDescription.Position = new Point((this.categoryLabel.X + size.Width) + 5, this.categoryDescription.Y);
        }

        public void update()
        {
            if ((this.currentUserLine == -1000) && GameEngine.Instance.World.isLeaderboardCategoryPopulated(this.currentCategory))
            {
                int num = GameEngine.Instance.World.getMaxLeaderboardEntries(this.currentCategory);
                this.currentUserLine = GameEngine.Instance.World.findSelfInLeaderboard(this.currentCategory) - (NUM_VISIBLE_LINES / 2);
                if (this.currentUserLine > (num - (NUM_VISIBLE_LINES - 1)))
                {
                    this.currentUserLine = num - (NUM_VISIBLE_LINES - 1);
                }
                if (this.currentUserLine < 1)
                {
                    this.currentUserLine = 1;
                }
                this.updateEntries();
            }
            this.mainEntry1.update();
            this.mainEntry2.update();
            this.mainEntry3.update();
            this.mainEntry4.update();
            this.mainEntry5.update();
            this.mainEntry6.update();
            this.mainEntry7.update();
            this.mainEntry8.update();
            this.mainEntry9.update();
            if (NUM_VISIBLE_LINES >= 10)
            {
                this.mainEntry10.update();
            }
            if (NUM_VISIBLE_LINES >= 11)
            {
                this.mainEntry11.update();
            }
            if (NUM_VISIBLE_LINES >= 12)
            {
                this.mainEntry12.update();
            }
            if (NUM_VISIBLE_LINES >= 13)
            {
                this.mainEntry13.update();
            }
            if (NUM_VISIBLE_LINES >= 14)
            {
                this.mainEntry14.update();
            }
            if (NUM_VISIBLE_LINES >= 15)
            {
                this.mainEntry15.update();
            }
            if (NUM_VISIBLE_LINES >= 0x10)
            {
                this.mainEntry16.update();
            }
            if (NUM_VISIBLE_LINES >= 0x11)
            {
                this.mainEntry17.update();
            }
            if (NUM_VISIBLE_LINES >= 0x12)
            {
                this.mainEntry18.update();
            }
            if (NUM_VISIBLE_LINES >= 0x13)
            {
                this.mainEntry19.update();
            }
            if (NUM_VISIBLE_LINES >= 20)
            {
                this.mainEntry20.update();
            }
            this.topEntry1.update();
            this.topEntry2.update();
            this.topEntry3.update();
            if (this.numExtraTopLines > 0)
            {
                this.topEntry4.update();
            }
            if (this.numExtraTopLines > 1)
            {
                this.topEntry5.update();
            }
            if (this.numExtraTopLines > 2)
            {
                this.topEntry6.update();
            }
            if (this.numExtraTopLines > 3)
            {
                this.topEntry7.update();
            }
            if (((this.initialTextInTextbox || (this.searchInput.Text.Length < 4)) || ((this.currentCategory == -2) || (this.currentCategory == -3))) || ((this.currentCategory == -4) || GameEngine.Instance.World.downloadingLeaderboard()))
            {
                if (((this.currentCategory == -4) || (this.currentCategory == -3)) || ((this.currentCategory == -2) || GameEngine.Instance.World.downloadingLeaderboard()))
                {
                    this.searchButton.Enabled = false;
                }
                else if (this.initialTextInTextbox || (this.searchInput.Text.Length == 0))
                {
                    this.searchButton.Enabled = true;
                }
                else
                {
                    this.searchButton.Enabled = false;
                }
            }
            else
            {
                this.searchButton.Enabled = true;
            }
            this.clearSearchButton.Visible = this.inSearchResults;
            if (GameEngine.Instance.World.downloadingLeaderboard())
            {
                this.topButton.Enabled = false;
                this.upButton.Enabled = false;
                this.downButton.Enabled = false;
                this.bottomButton.Enabled = false;
            }
            else
            {
                this.topButton.Enabled = true;
                this.upButton.Enabled = true;
                this.downButton.Enabled = true;
                this.bottomButton.Enabled = true;
            }
            DateTime time = GameEngine.Instance.World.getLastLeaderboardUpdate();
            if (time == DateTime.MinValue)
            {
                this.updateLabel.Visible = false;
            }
            else
            {
                this.updateLabel.Visible = true;
                this.updateLabel.Text = "(" + SK.Text("Stats_Last_Updated", "last updated") + "   :   " + time.ToShortDateString() + ":" + time.ToShortTimeString() + ")";
            }
            if (GameEngine.Instance.World.areSelfStandingsDirty())
            {
                this.selfEntry1.init(0, this);
                this.selfEntry2.init(1, this);
                this.selfEntry3.init(2, this);
                this.selfEntry4.init(3, this);
                this.selfEntry5.init(4, this);
                this.selfEntry6.init(5, this);
                this.selfEntry7.init(6, this);
            }
        }

        public void updateEntries()
        {
            if (!this.inSearchResults)
            {
                this.mainEntry1.init(this.currentCategory, this.currentUserLine, 0);
                this.mainEntry2.init(this.currentCategory, this.currentUserLine + 1, 1);
                this.mainEntry3.init(this.currentCategory, this.currentUserLine + 2, 2);
                this.mainEntry4.init(this.currentCategory, this.currentUserLine + 3, 3);
                this.mainEntry5.init(this.currentCategory, this.currentUserLine + 4, 4);
                this.mainEntry6.init(this.currentCategory, this.currentUserLine + 5, 5);
                this.mainEntry7.init(this.currentCategory, this.currentUserLine + 6, 6);
                this.mainEntry8.init(this.currentCategory, this.currentUserLine + 7, 7);
                this.mainEntry9.init(this.currentCategory, this.currentUserLine + 8, 8);
                if (NUM_VISIBLE_LINES >= 10)
                {
                    this.mainEntry10.init(this.currentCategory, this.currentUserLine + 9, 9);
                }
                if (NUM_VISIBLE_LINES >= 11)
                {
                    this.mainEntry11.init(this.currentCategory, this.currentUserLine + 10, 10);
                }
                if (NUM_VISIBLE_LINES >= 12)
                {
                    this.mainEntry12.init(this.currentCategory, this.currentUserLine + 11, 11);
                }
                if (NUM_VISIBLE_LINES >= 13)
                {
                    this.mainEntry13.init(this.currentCategory, this.currentUserLine + 12, 12);
                }
                if (NUM_VISIBLE_LINES >= 14)
                {
                    this.mainEntry14.init(this.currentCategory, this.currentUserLine + 13, 13);
                }
                if (NUM_VISIBLE_LINES >= 15)
                {
                    this.mainEntry15.init(this.currentCategory, this.currentUserLine + 14, 14);
                }
                if (NUM_VISIBLE_LINES >= 0x10)
                {
                    this.mainEntry16.init(this.currentCategory, this.currentUserLine + 15, 15);
                }
                if (NUM_VISIBLE_LINES >= 0x11)
                {
                    this.mainEntry17.init(this.currentCategory, this.currentUserLine + 0x10, 0x10);
                }
                if (NUM_VISIBLE_LINES >= 0x12)
                {
                    this.mainEntry18.init(this.currentCategory, this.currentUserLine + 0x11, 0x11);
                }
                if (NUM_VISIBLE_LINES >= 0x13)
                {
                    this.mainEntry19.init(this.currentCategory, this.currentUserLine + 0x12, 0x12);
                }
                if (NUM_VISIBLE_LINES >= 20)
                {
                    this.mainEntry20.init(this.currentCategory, this.currentUserLine + 0x13, 0x13);
                }
            }
            else
            {
                this.mainEntry1.init(this.currentCategory, this.getSearchEntry(this.searchLocation), 0);
                this.mainEntry2.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 1), 1);
                this.mainEntry3.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 2), 2);
                this.mainEntry4.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 3), 3);
                this.mainEntry5.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 4), 4);
                this.mainEntry6.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 5), 5);
                this.mainEntry7.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 6), 6);
                this.mainEntry8.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 7), 7);
                this.mainEntry9.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 8), 8);
                if (NUM_VISIBLE_LINES >= 10)
                {
                    this.mainEntry10.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 9), 9);
                }
                if (NUM_VISIBLE_LINES >= 11)
                {
                    this.mainEntry11.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 10), 10);
                }
                if (NUM_VISIBLE_LINES >= 12)
                {
                    this.mainEntry12.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 11), 11);
                }
                if (NUM_VISIBLE_LINES >= 13)
                {
                    this.mainEntry13.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 12), 12);
                }
                if (NUM_VISIBLE_LINES >= 14)
                {
                    this.mainEntry14.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 13), 13);
                }
                if (NUM_VISIBLE_LINES >= 15)
                {
                    this.mainEntry15.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 14), 14);
                }
                if (NUM_VISIBLE_LINES >= 0x10)
                {
                    this.mainEntry16.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 15), 15);
                }
                if (NUM_VISIBLE_LINES >= 0x11)
                {
                    this.mainEntry17.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 0x10), 0x10);
                }
                if (NUM_VISIBLE_LINES >= 0x12)
                {
                    this.mainEntry18.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 0x11), 0x11);
                }
                if (NUM_VISIBLE_LINES >= 0x13)
                {
                    this.mainEntry19.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 0x12), 0x12);
                }
                if (NUM_VISIBLE_LINES >= 20)
                {
                    this.mainEntry20.init(this.currentCategory, this.getSearchEntry(this.searchLocation + 0x13), 0x13);
                }
            }
            this.topEntry1.init(this.currentCategory, 1, 0);
            this.topEntry2.init(this.currentCategory, 2, 1);
            this.topEntry3.init(this.currentCategory, 3, 2);
            if (this.numExtraTopLines > 0)
            {
                this.topEntry4.init(this.currentCategory, 4, 3);
            }
            if (this.numExtraTopLines > 1)
            {
                this.topEntry5.init(this.currentCategory, 5, 4);
            }
            if (this.numExtraTopLines > 2)
            {
                this.topEntry6.init(this.currentCategory, 6, 5);
            }
            if (this.numExtraTopLines > 3)
            {
                this.topEntry7.init(this.currentCategory, 7, 6);
            }
        }

        private void updateVariIcons()
        {
            this.fixedButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_normal;
            this.fixedButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_over;
            this.fixedButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
            this.fixedButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_rank_normal;
            this.fixedButton2.ImageOver = (Image) GFXLibrary.catagory_icons_rank_over;
            this.fixedButton2.ImageClick = (Image) GFXLibrary.catagory_icons_rank_pushed;
            this.fixedButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_villages_normal;
            this.fixedButton3.ImageOver = (Image) GFXLibrary.catagory_icons_villages_over;
            this.fixedButton3.ImageClick = (Image) GFXLibrary.catagory_icons_villages_pushed;
            this.fixedButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_factions_normal;
            this.fixedButton4.ImageOver = (Image) GFXLibrary.catagory_icons_factions_over;
            this.fixedButton4.ImageClick = (Image) GFXLibrary.catagory_icons_factions_pushed;
            this.fixedButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_houses_normal;
            this.fixedButton5.ImageOver = (Image) GFXLibrary.catagory_icons_houses_over;
            this.fixedButton5.ImageClick = (Image) GFXLibrary.catagory_icons_houses_pushed;
            this.fixedButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_parishflags_normal;
            this.fixedButton7.ImageOver = (Image) GFXLibrary.catagory_icons_parishflags_over;
            this.fixedButton7.ImageClick = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
            for (int i = 0; i < 10; i++)
            {
                CustomSelfDrawPanel.CSDButton button = this.getVariButton(i);
                this.setButtonGFX(button, i + this.categoryScrollPos);
            }
            if (this.currentCategory < 0)
            {
                switch (this.currentCategory)
                {
                    case -6:
                        this.fixedButton3.ImageNorm = (Image) GFXLibrary.catagory_icons_villages_pushed;
                        this.fixedButton3.ImageOver = (Image) GFXLibrary.catagory_icons_villages_pushed;
                        this.fixedButton3.ImageClick = (Image) GFXLibrary.catagory_icons_villages_pushed;
                        break;

                    case -5:
                        this.fixedButton2.ImageNorm = (Image) GFXLibrary.catagory_icons_rank_pushed;
                        this.fixedButton2.ImageOver = (Image) GFXLibrary.catagory_icons_rank_pushed;
                        this.fixedButton2.ImageClick = (Image) GFXLibrary.catagory_icons_rank_pushed;
                        break;

                    case -4:
                        this.fixedButton7.ImageNorm = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
                        this.fixedButton7.ImageOver = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
                        this.fixedButton7.ImageClick = (Image) GFXLibrary.catagory_icons_parishflags_pushed;
                        break;

                    case -3:
                        this.fixedButton5.ImageNorm = (Image) GFXLibrary.catagory_icons_houses_pushed;
                        this.fixedButton5.ImageOver = (Image) GFXLibrary.catagory_icons_houses_pushed;
                        this.fixedButton5.ImageClick = (Image) GFXLibrary.catagory_icons_houses_pushed;
                        break;

                    case -2:
                        this.fixedButton4.ImageNorm = (Image) GFXLibrary.catagory_icons_factions_pushed;
                        this.fixedButton4.ImageOver = (Image) GFXLibrary.catagory_icons_factions_pushed;
                        this.fixedButton4.ImageClick = (Image) GFXLibrary.catagory_icons_factions_pushed;
                        break;

                    case -1:
                        this.fixedButton1.ImageNorm = (Image) GFXLibrary.catagory_icons_points_pushed;
                        this.fixedButton1.ImageOver = (Image) GFXLibrary.catagory_icons_points_pushed;
                        this.fixedButton1.ImageClick = (Image) GFXLibrary.catagory_icons_points_pushed;
                        break;
                }
            }
            if (this.categoryScrollPos <= 0)
            {
                this.variButtonLeft.Enabled = false;
            }
            else
            {
                this.variButtonLeft.Enabled = true;
            }
            if (this.categoryScrollPos >= 8)
            {
                this.variButtonRight.Enabled = false;
            }
            else
            {
                this.variButtonRight.Enabled = true;
            }
        }

        public class SelfStatsEntry : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDLabel amountLine = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel categoryLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage changeImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel changeLabel = new CustomSelfDrawPanel.CSDLabel();
            private int m_category = -1000;
            private StatsPanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDImage sectionImage = new CustomSelfDrawPanel.CSDImage();

            public void init(int position, StatsPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                LeaderBoardSelfRankings rankings = GameEngine.Instance.World.getLeaderboardSelfStanding(position);
                this.clearControls();
                this.m_category = -1000;
                if ((rankings != null) && (rankings.value != 0))
                {
                    if ((position & 1) == 0)
                    {
                        this.backgroundImage.Image = (Image) GFXLibrary.int_statsscreen_secondinset_bar_darker;
                    }
                    else
                    {
                        this.backgroundImage.Image = (Image) GFXLibrary.int_statsscreen_secondinset_bar_lighter;
                    }
                    this.m_category = rankings.category;
                    this.backgroundImage.Position = new Point(0, 0);
                    this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    base.addControl(this.backgroundImage);
                    this.Size = this.backgroundImage.Size;
                    NumberFormatInfo nFI = GameEngine.NFI;
                    switch (rankings.category)
                    {
                        case -6:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_villages_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Villages", "Villages") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case -5:
                        {
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_rank_pushed;
                            int rank = rankings.value / 100;
                            int rankSubLevel = rankings.value % 100;
                            this.amountLine.Text = "(" + Rankings.getRankingName(GameEngine.Instance.LocalWorldData, rank, rankSubLevel, true) + ")";
                            break;
                        }
                        case -1:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_points_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Points", "Points") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 0:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_pillager_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Resources_Pillages", "Resources Pillaged") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 1:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_defender_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Attacked_Killed", "Attackers Killed") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 2:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_destroyer_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Buildings_Destroyed", "Buildings Destroyed") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 3:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_wolfbane_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Wolves_Killed", "Wolves Killed") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 4:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_banditslayer_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Bandits_Killed", "Bandits Killed") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 5:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_aikiller_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_AI_Killed", "AI Troops Killed") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 6:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_merchant_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Traded", "Packets Traded") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 7:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_forger_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Foraged", "Packets Foraged") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 8:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_worker_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 9:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_farmer_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 10:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_brewer_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 11:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_blacksmith_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 12:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_banquet_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Produced", "Packets Produced") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 13:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_achiever_pushed;
                            this.amountLine.Text = "(" + SK.Text("User_Quests_Complete", "Quests Completed") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 14:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_donator_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Packets_Donated", "Packets Donated") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 15:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_capture_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Villages_Captured", "Villages Captured") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 0x10:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_raze_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Villages_Razed", "Villages Razed") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;

                        case 0x11:
                            this.sectionImage.Image = (Image) GFXLibrary.catagory_icons_glory_pushed;
                            this.amountLine.Text = "(" + SK.Text("Stats_Glory_Generated", "Glory Generated") + " : " + rankings.value.ToString("N", nFI) + ")";
                            break;
                    }
                    this.sectionImage.Position = new Point(15, 14);
                    this.sectionImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    this.backgroundImage.addControl(this.sectionImage);
                    if (rankings.oldPlace != rankings.place)
                    {
                        string str = "";
                        if (rankings.oldPlace >= rankings.place)
                        {
                            str = (rankings.oldPlace - rankings.place).ToString();
                            this.changeImage.Image = (Image) GFXLibrary.arrow_up;
                        }
                        else
                        {
                            str = (rankings.place - rankings.oldPlace).ToString();
                            this.changeImage.Image = (Image) GFXLibrary.arrow_down;
                        }
                        this.changeImage.Position = new Point(0x14d, 12);
                        this.changeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                        this.backgroundImage.addControl(this.changeImage);
                        this.changeLabel.Text = str;
                        this.changeLabel.Color = ARGBColors.White;
                        this.changeLabel.Position = new Point(0, 12);
                        this.changeLabel.Size = new Size(0x14d, 0x19);
                        this.changeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                        this.changeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                        this.changeLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                        this.backgroundImage.addControl(this.changeLabel);
                    }
                    this.categoryLabel.Text = StatsPanel.getCategoryTitle(rankings.category) + " - " + SK.Text("Stats_Ranked", "Ranked") + " " + rankings.place.ToString("N", nFI);
                    this.categoryLabel.Color = ARGBColors.White;
                    this.categoryLabel.Position = new Point(0x4c, 12);
                    this.categoryLabel.Size = new Size(0x113, 0x19);
                    this.categoryLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                    this.categoryLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    this.categoryLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    this.backgroundImage.addControl(this.categoryLabel);
                    this.amountLine.Color = ARGBColors.White;
                    this.amountLine.Position = new Point(0x4c, 0x25);
                    this.amountLine.Size = new Size(0x113, this.backgroundImage.Height);
                    this.amountLine.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.amountLine.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    this.amountLine.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    this.backgroundImage.addControl(this.amountLine);
                    base.invalidate();
                }
            }

            public void lineClicked()
            {
                if ((this.m_category != -1000) && (this.m_parent != null))
                {
                    GameEngine.Instance.playInterfaceSound("StatsPanel_entry_clicked");
                    this.m_parent.changeCategory(this.m_category);
                }
            }

            public void update()
            {
            }
        }

        public class StatsEntry : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
            private int m_category = -1000;
            private int m_entryID = -1;
            private int m_position = -1000;
            private int m_screenLine;
            private bool m_validData;
            private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel positionLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
            private bool topEntry;
            private CustomSelfDrawPanel.CSDLabel valueLabel = new CustomSelfDrawPanel.CSDLabel();

            public void init(int category, int leaderboardPosition, int screenLocation)
            {
                this.m_category = category;
                this.m_position = leaderboardPosition;
                this.m_screenLine = screenLocation;
                this.m_validData = false;
                LeaderBoardEntryData data = null;
                if (leaderboardPosition != -999999)
                {
                    data = GameEngine.Instance.World.getLeaderboardEntry(category, leaderboardPosition, StatsPanel.NUM_VISIBLE_LINES);
                    if (data != null)
                    {
                        this.m_validData = true;
                    }
                }
                else
                {
                    data = new LeaderBoardEntryData {
                        dummy = true
                    };
                    this.m_validData = true;
                }
                this.clearControls();
                if ((screenLocation & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.int_statsscreen_listbar_darker;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.int_statsscreen_listbar_lighter;
                }
                this.backgroundImage.Position = new Point(0, 0);
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.houseImage.Image = (Image) GFXLibrary.house_flag_001;
                this.houseImage.Position = new Point(0x44, 0);
                this.houseImage.Visible = false;
                this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.houseImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
                this.backgroundImage.addControl(this.houseImage);
                this.playerName.Text = SK.Text("Stats_Getting_Data", "Getting Data");
                this.playerName.Color = ARGBColors.Black;
                this.playerName.Position = new Point(0x81, 0);
                this.playerName.Size = new Size(0xbc, this.backgroundImage.Height);
                this.playerName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.playerName.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
                this.backgroundImage.addControl(this.playerName);
                this.positionLabel.Text = "0";
                this.positionLabel.Color = ARGBColors.Black;
                this.positionLabel.Position = new Point(6, 0);
                this.positionLabel.Size = new Size(0x38, this.backgroundImage.Height);
                this.positionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.positionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.positionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.positionLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
                this.backgroundImage.addControl(this.positionLabel);
                this.valueLabel.Text = "0";
                this.valueLabel.Color = ARGBColors.Black;
                this.valueLabel.Position = new Point(9, 0);
                this.valueLabel.Size = new Size(380, this.backgroundImage.Height);
                this.valueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.valueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.valueLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.valueLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
                this.backgroundImage.addControl(this.valueLabel);
                int playerID = -1;
                if (data != null)
                {
                    if (data.dummy)
                    {
                        this.playerName.Text = "";
                        this.valueLabel.Text = "";
                        this.positionLabel.Text = "";
                        this.houseImage.Visible = false;
                    }
                    else
                    {
                        this.playerName.Color = ARGBColors.Black;
                        this.valueLabel.Color = ARGBColors.Black;
                        this.positionLabel.Color = ARGBColors.Black;
                        if (((this.m_category >= 0) || (this.m_category == -1)) || ((this.m_category == -5) || (this.m_category == -6)))
                        {
                            if (data.entryID == RemoteServices.Instance.UserID)
                            {
                                this.playerName.Color = ARGBColors.White;
                                this.valueLabel.Color = ARGBColors.White;
                                this.positionLabel.Color = ARGBColors.White;
                            }
                            playerID = data.entryID;
                        }
                        else if (this.m_category == -2)
                        {
                            if (data.entryID == RemoteServices.Instance.UserFactionID)
                            {
                                this.playerName.Color = ARGBColors.White;
                                this.valueLabel.Color = ARGBColors.White;
                                this.positionLabel.Color = ARGBColors.White;
                            }
                        }
                        else if (this.m_category == -3)
                        {
                            if (data.entryID == GameEngine.Instance.World.getHouse(RemoteServices.Instance.UserFactionID))
                            {
                                this.playerName.Color = ARGBColors.White;
                                this.valueLabel.Color = ARGBColors.White;
                                this.positionLabel.Color = ARGBColors.White;
                            }
                        }
                        else if (this.m_category == -4)
                        {
                            foreach (int num2 in GameEngine.Instance.World.getUserVillageIDList())
                            {
                                if (GameEngine.Instance.World.isCapital(num2))
                                {
                                    if (!GameEngine.Instance.World.isRegionCapital(num2))
                                    {
                                        continue;
                                    }
                                    int num3 = GameEngine.Instance.World.getParishFromVillageID(num2);
                                    if (data.entryID != num3)
                                    {
                                        continue;
                                    }
                                    this.playerName.Color = ARGBColors.White;
                                    this.valueLabel.Color = ARGBColors.White;
                                    this.positionLabel.Color = ARGBColors.White;
                                    break;
                                }
                                int num4 = GameEngine.Instance.World.getParishFromVillageID(num2);
                                if (data.entryID == num4)
                                {
                                    this.playerName.Color = ARGBColors.White;
                                    this.valueLabel.Color = ARGBColors.White;
                                    this.positionLabel.Color = ARGBColors.White;
                                    break;
                                }
                            }
                        }
                        this.playerName.Text = data.name;
                        NumberFormatInfo nFI = GameEngine.NFI;
                        if (this.m_category != -5)
                        {
                            this.valueLabel.Text = data.value.ToString("N", nFI);
                            this.valueLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                        }
                        else
                        {
                            int rank = data.value / 100;
                            int rankSubLevel = data.value % 100;
                            if (rank >= 0x16)
                            {
                                rank = 0x16;
                                rankSubLevel = data.value - 0x898;
                            }
                            this.valueLabel.Text = Rankings.getRankingName(GameEngine.Instance.LocalWorldData, rank, rankSubLevel, data.male);
                            this.valueLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                            if (Program.mySettings.LanguageIdent == "it")
                            {
                                this.valueLabel.Position = new Point(0x10d, 0);
                                this.valueLabel.Size = new Size(120, this.backgroundImage.Height);
                            }
                        }
                        this.m_entryID = data.entryID;
                        this.positionLabel.Text = data.standing.ToString("N", nFI);
                        if (data.house > 0)
                        {
                            this.houseImage.Visible = true;
                            switch (data.house)
                            {
                                case 1:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_001;
                                    break;

                                case 2:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_002;
                                    break;

                                case 3:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_003;
                                    break;

                                case 4:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_004;
                                    break;

                                case 5:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_005;
                                    break;

                                case 6:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_006;
                                    break;

                                case 7:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_007;
                                    break;

                                case 8:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_008;
                                    break;

                                case 9:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_009;
                                    break;

                                case 10:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_010;
                                    break;

                                case 11:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_011;
                                    break;

                                case 12:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_012;
                                    break;

                                case 13:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_013;
                                    break;

                                case 14:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_014;
                                    break;

                                case 15:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_015;
                                    break;

                                case 0x10:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_016;
                                    break;

                                case 0x11:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_017;
                                    break;

                                case 0x12:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_018;
                                    break;

                                case 0x13:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_019;
                                    break;

                                case 20:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_020;
                                    break;
                            }
                        }
                        if ((playerID >= 0) && this.topEntry)
                        {
                            this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(playerID, 0x19, 0x1c);
                            if (this.shieldImage.Image != null)
                            {
                                this.shieldImage.Position = new Point(0x10, 8);
                                this.shieldImage.Visible = true;
                                this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                                this.shieldImage.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineRightClicked));
                                this.backgroundImage.addControl(this.shieldImage);
                            }
                        }
                    }
                }
            }

            public void lineClicked()
            {
                GameEngine.Instance.playInterfaceSound("StatsPanel_entry_clicked");
                switch (this.m_category)
                {
                    case -4:
                        break;

                    case -3:
                        if (this.m_entryID < 0)
                        {
                            break;
                        }
                        InterfaceMgr.Instance.showHousePanel(this.m_entryID);
                        return;

                    case -2:
                        if (this.m_entryID < 0)
                        {
                            break;
                        }
                        InterfaceMgr.Instance.showFactionPanel(this.m_entryID);
                        return;

                    default:
                        if (this.m_entryID >= 0)
                        {
                            InterfaceMgr.Instance.changeTab(0);
                            WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                                userID = this.m_entryID
                            };
                            InterfaceMgr.Instance.showUserInfoScreen(userInfo);
                        }
                        break;
                }
            }

            public void lineRightClicked()
            {
                if (((base.csd != null) && (base.csd.ClickedControl != null)) && (base.csd.ClickedControl.Parent != null))
                {
                    CustomSelfDrawPanel.CSDControl parent = base.csd.ClickedControl.Parent;
                    while ((parent != null) && (parent.GetType() != typeof(StatsPanel.StatsEntry)))
                    {
                        parent = parent.Parent;
                    }
                    if (parent != null)
                    {
                        GameEngine.Instance.playInterfaceSound("StatsPanel_entry_clicked");
                        Clipboard.SetText(((StatsPanel.StatsEntry) parent).playerName.Text);
                    }
                }
            }

            public void setAsTopEntry()
            {
                this.topEntry = true;
            }

            public void update()
            {
                if (!this.m_validData && (GameEngine.Instance.World.getLeaderboardEntry(this.m_category, this.m_position, StatsPanel.NUM_VISIBLE_LINES) != null))
                {
                    this.init(this.m_category, this.m_position, this.m_screenLine);
                }
            }
        }
    }
}

