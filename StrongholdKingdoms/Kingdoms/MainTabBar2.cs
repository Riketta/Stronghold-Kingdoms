namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class MainTabBar2 : CustomSelfDrawPanel.CSDControl
    {
        private int alphaPulse = 0xff;
        private static int dummyMode;
        private bool ignore;
        private BaseImage[] images;
        private bool lastArmyFlashing;
        private int lastAttacks = 0;
        private static int lastDummyMode;
        private bool lastNewMail = false;
        private bool lastNewQuests = false;
        private bool lastNewReports = false;
        private int lastSoundTab = -2;
        private int lastTab = -1;
        private int lastWidth = -1;
        private bool refresh = false;
        private TabChangeCallback tabChangeCallback;
        private CustomSelfDrawPanel.CSDTabControl tabControl1 = new CustomSelfDrawPanel.CSDTabControl();

        public void changeTab(int tabID)
        {
            this.lastSoundTab = tabID;
            this.ignore = true;
            if (tabID == 0)
            {
                this.tabControl1.SelectedIndex = 0;
                this.tabControl1.SelectedIndex = 1;
            }
            else
            {
                this.tabControl1.SelectedIndex = 1;
                this.tabControl1.SelectedIndex = 0;
            }
            this.ignore = false;
            this.lastTab = tabID + 1;
            this.tabControl1.SelectedIndex = tabID;
        }

        public void changeTabGfxOnly(int tabID)
        {
            this.lastSoundTab = tabID;
            this.ignore = true;
            this.lastTab = tabID;
            this.tabControl1.SelectedIndex = tabID;
            this.ignore = false;
        }

        public int getCurrentTab()
        {
            return this.lastTab;
        }

        public void incomingAttacks(int numAttacks, long highestArmyID)
        {
            if (numAttacks > 0)
            {
                this.images[12] = GFXLibrary.tab_5b_normal;
                this.images[13] = GFXLibrary.tab_5b_selected;
                this.tabControl1.setTabText(6, numAttacks.ToString());
            }
            else
            {
                this.images[12] = GFXLibrary.tab_5_normal;
                this.images[13] = GFXLibrary.tab_5_selected;
                this.tabControl1.setTabText(6, "");
            }
            this.tabControl1.updateImageArray(this.images);
            long highestArmyIDSeen = GameEngine.Instance.World.HighestArmyIDSeen;
            bool flag = false;
            if ((highestArmyID > highestArmyIDSeen) && (numAttacks > 0))
            {
                this.tabControl1.addOverlayImages(6, GFXLibrary.tab_5b_normal_bright, GFXLibrary.tab_5b_selected_bright, 0xff);
                flag = true;
            }
            else
            {
                this.tabControl1.addOverlayImages(6, null, null, 0xff);
            }
            if (this.lastAttacks != numAttacks)
            {
                this.refresh = true;
            }
            if (this.lastArmyFlashing != flag)
            {
                this.refresh = true;
            }
            this.lastAttacks = numAttacks;
            this.lastArmyFlashing = flag;
        }

        public void init()
        {
            this.clearControls();
            this.tabControl1.Position = new Point(0, 3);
            base.addControl(this.tabControl1);
            this.initImages();
            int width = this.tabControl1.Create(10, this.images);
            this.tabControl1.setCallback(0, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage1_Enter), 0x16);
            this.tabControl1.setCallback(1, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage2_Enter), 0x17);
            this.tabControl1.setCallback(2, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage3_Enter), 0x20);
            this.tabControl1.setCallback(3, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage4_Enter), 0x18);
            this.tabControl1.setCallback(4, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage5_Enter), 0x19);
            this.tabControl1.setCallback(5, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage6_Enter), 0x1f);
            this.tabControl1.setCallback(6, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage7_Enter), 0x1a);
            this.tabControl1.setCallback(7, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage8_Enter), 0x1b);
            this.tabControl1.setCallback(8, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage9_Enter), 30);
            this.tabControl1.setCallback(9, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage10_Enter), 0);
            this.tabControl1.setSoundCallback(new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabControl1_Click));
            if (width > 0)
            {
                this.Size = new Size(width, this.images[0].Height);
                this.tabControl1.Size = new Size(width, this.images[0].Height);
            }
        }

        public void initImages()
        {
            if (this.images == null)
            {
                this.images = new BaseImage[] { 
                    GFXLibrary.tab_world_normal, GFXLibrary.tab_world_selected, GFXLibrary.tab_village_normal, GFXLibrary.tab_village_selected, GFXLibrary.tab_capital_normal, GFXLibrary.tab_capital_selected, GFXLibrary.tab_3_normal, GFXLibrary.tab_3_selected, GFXLibrary.tab_4_normal, GFXLibrary.tab_4_selected, GFXLibrary.tab_quest_normal, GFXLibrary.tab_quest_selected, GFXLibrary.tab_5_normal, GFXLibrary.tab_5_selected, GFXLibrary.tab_6_normal, GFXLibrary.tab_6_selected, 
                    GFXLibrary.tab_9_normal, GFXLibrary.tab_9_selected
                 };
            }
        }

        public bool isArmiesFlashing()
        {
            return this.lastArmyFlashing;
        }

        public void newMail(bool newMail)
        {
            InterfaceMgr.Instance.getMainMenuBar().newMail(newMail);
            if (this.lastNewMail != newMail)
            {
                this.refresh = true;
            }
            this.lastNewMail = newMail;
        }

        public void newPoliticsPost(bool newPost)
        {
        }

        public void newQuestsCompleted(bool newQuests)
        {
            if (newQuests && GameEngine.Instance.World.isTutorialActive())
            {
                newQuests = false;
            }
            if (newQuests)
            {
                this.images[10] = GFXLibrary.tab_quest_normal;
                this.tabControl1.addOverlayImages(5, GFXLibrary.tab_quest_glow, null, 0xff);
                this.tabControl1.updateImageArray(this.images);
            }
            else
            {
                this.images[10] = GFXLibrary.tab_quest_normal;
                this.tabControl1.addOverlayImages(5, null, null, 0xff);
                this.tabControl1.updateImageArray(this.images);
            }
            if (this.lastNewQuests != newQuests)
            {
                this.refresh = true;
            }
            this.lastNewQuests = newQuests;
        }

        public void newReports(bool newReport)
        {
            if (newReport)
            {
                this.images[14] = GFXLibrary.tab_6B_normal;
                this.tabControl1.addOverlayImages(7, GFXLibrary.tab_6B_normal_bright, null, 0xff);
                this.tabControl1.updateImageArray(this.images);
            }
            else
            {
                this.images[14] = GFXLibrary.tab_6_normal;
                this.tabControl1.addOverlayImages(7, null, null, 0xff);
                this.tabControl1.updateImageArray(this.images);
            }
            if (this.lastNewReports != newReport)
            {
                this.refresh = true;
            }
            this.lastNewReports = newReport;
        }

        public void registerTabChangeCallback(TabChangeCallback newTabChangeCallback)
        {
            this.tabChangeCallback = newTabChangeCallback;
        }

        public void selectDummyTab(int mode)
        {
            GameEngine.Instance.ResetVillageIfChangedFromCapital();
            dummyMode = mode;
            if (this.tabControl1.SelectedIndex == 9)
            {
                this.tabChangeCallback(9);
            }
            else
            {
                this.tabControl1.SelectedIndex = 9;
            }
        }

        public void selectDummyTabFast(int mode)
        {
            dummyMode = mode;
            if (this.tabControl1.SelectedIndex == 9)
            {
                this.tabChangeCallback(9);
            }
            else
            {
                this.tabControl1.SelectedIndex = 9;
            }
        }

        private void tabControl1_Click()
        {
            if (this.lastSoundTab != this.lastTab)
            {
                this.lastSoundTab = this.lastTab;
                GameEngine.Instance.playInterfaceSound("WorldMapScreen_main_tabbar_item_clicked");
                Sound.playDelayedInterfaceSound("WorldMapScreen_main_tabbar_item_clicked_" + this.lastTab.ToString(), 100);
            }
        }

        private void tabPage1_Enter()
        {
            if ((this.lastTab != 0) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xc9));
                this.lastTab = 0;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(0);
            }
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
        }

        private void tabPage10_Enter()
        {
            if ((this.lastTab != 9) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(210));
                this.lastTab = 9;
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(9);
            }
        }

        private void tabPage2_Enter()
        {
            if ((this.lastTab != 1) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xca));
                this.lastTab = 1;
                GameEngine.Instance.forceResetVillageIfChangedFromCapital();
                this.tabChangeCallback(1);
            }
        }

        private void tabPage2_Leave(object sender, EventArgs e)
        {
        }

        private void tabPage3_Enter()
        {
            if ((this.lastTab != 2) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xcb));
                this.lastTab = 2;
                this.tabChangeCallback(2);
            }
        }

        private void tabPage4_Enter()
        {
            if ((this.lastTab != 3) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xcc));
                this.lastTab = 3;
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(3);
            }
        }

        public void tabPage5_Enter()
        {
            if ((this.lastTab != 4) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xcd));
                this.lastTab = 4;
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(4);
            }
        }

        private void tabPage6_Enter()
        {
            if ((this.lastTab != 5) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xce));
                this.lastTab = 5;
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(5);
            }
        }

        private void tabPage7_Enter()
        {
            if ((this.lastTab != 6) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xcf));
                this.lastTab = 6;
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(6);
            }
        }

        private void tabPage8_Enter()
        {
            if ((this.lastTab != 7) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xd0));
                this.lastTab = 7;
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(7);
                this.newReports(false);
            }
        }

        private void tabPage9_Enter()
        {
            if ((this.lastTab != 8) && !this.ignore)
            {
                GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(0xd1));
                this.lastTab = 8;
                GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
                GameEngine.Instance.ResetVillageIfChangedFromCapital();
                this.tabChangeCallback(8);
            }
        }

        public void update()
        {
            this.alphaPulse += 20;
            if (this.alphaPulse > 0x1ff)
            {
                this.alphaPulse -= 0x1ff;
            }
            int alphaPulse = this.alphaPulse;
            if (alphaPulse > 0xff)
            {
                alphaPulse = 0x1ff - alphaPulse;
            }
            if (this.lastNewReports)
            {
                this.refresh = true;
                this.tabControl1.setOverlayAlpha(7, alphaPulse);
            }
            if (this.lastNewMail)
            {
                InterfaceMgr.Instance.getMainMenuBar().setMailAlpha(((double) alphaPulse) / 255.0);
            }
            if (this.lastArmyFlashing)
            {
                this.refresh = true;
                this.tabControl1.setOverlayAlpha(6, alphaPulse);
            }
            if (this.lastNewQuests)
            {
                this.refresh = true;
                this.tabControl1.setOverlayAlpha(5, alphaPulse);
            }
            double num2 = GameEngine.Instance.World.getCurrentHonour();
            int index = GameEngine.Instance.World.getRank();
            int subRank = GameEngine.Instance.World.getRankSubLevel();
            int num5 = GameEngine.Instance.LocalWorldData.ranks_HonourPerLevel[index];
            if (index == 0x16)
            {
                num5 = (int) Rankings.calcHonourForCrownPrince(subRank);
            }
            else if ((index == 0x15) && (subRank >= 0x18))
            {
                num5 = 0x989680;
            }
            int num1 = GameEngine.Instance.LocalWorldData.ranks_Levels[index];
            if (num2 >= num5)
            {
                if (this.images[8] != GFXLibrary.tab_4b_normal)
                {
                    this.images[8] = GFXLibrary.tab_4b_normal;
                    this.images[9] = GFXLibrary.tab_4b_selected;
                    this.tabControl1.updateImageArray(this.images);
                }
            }
            else if (this.images[8] != GFXLibrary.tab_4_normal)
            {
                this.images[8] = GFXLibrary.tab_4_normal;
                this.images[9] = GFXLibrary.tab_4_selected;
                this.tabControl1.updateImageArray(this.images);
            }
            if (this.refresh)
            {
                this.refresh = false;
            }
        }

        public void updateResearchTime(ResearchData data)
        {
            int width = -1;
            if ((data != null) && (data.researchingType >= 0))
            {
                DateTime time = VillageMap.getCurrentServerTime();
                TimeSpan span = (TimeSpan) (data.research_completionTime - time);
                int totalSeconds = (int) span.TotalSeconds;
                TimeSpan span2 = data.calcResearchTime(data.research_pointCount - 1, GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData);
                if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
                {
                    span2 = new TimeSpan(span2.Ticks / 2L);
                }
                int num3 = (int) span2.TotalSeconds;
                if ((num3 == 30) && (GameEngine.Instance.World.getTutorialStage() == 5))
                {
                    num3 = 11;
                }
                this.images[6] = GFXLibrary.tab_3b_normal;
                this.images[7] = GFXLibrary.tab_3b_selected;
                this.tabControl1.addOverlayImages(3, GFXLibrary.tab_3c_normal, GFXLibrary.tab_3c_selected, 0xff);
                width = 3 + ((0x2c * (num3 - totalSeconds)) / num3);
                this.tabControl1.setOverlayWidth(3, width);
                this.refresh = true;
            }
            else
            {
                this.images[6] = GFXLibrary.tab_3_normal;
                this.images[7] = GFXLibrary.tab_3_selected;
                this.tabControl1.addOverlayImages(3, null, null, 0xff);
            }
            this.tabControl1.updateImageArray(this.images);
            if (width != this.lastWidth)
            {
                this.refresh = true;
            }
            this.lastWidth = width;
        }

        public static int DummyMode
        {
            get
            {
                return dummyMode;
            }
            set
            {
                dummyMode = value;
            }
        }

        public static int LastDummyMode
        {
            get
            {
                return lastDummyMode;
            }
            set
            {
                lastDummyMode = value;
            }
        }

        public delegate void TabChangeCallback(int tabID);
    }
}

