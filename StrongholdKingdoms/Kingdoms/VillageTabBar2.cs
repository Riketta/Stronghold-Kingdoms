namespace Kingdoms
{
    using DXGraphics;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class VillageTabBar2 : CustomSelfDrawPanel.CSDControl
    {
        private bool ignore;
        private BaseImage[] images;
        private int lastSoundTab = -2;
        private int lastTab = -1;
        public bool lastVillageCapital;
        private TabChangeCallback tabChangeCallback;
        private CustomSelfDrawPanel.CSDTabControl tabControl1 = new CustomSelfDrawPanel.CSDTabControl();
        private bool viaTabClick;

        public void changeTab(int tabID)
        {
            this.lastSoundTab = tabID;
            this.updateShownTabs();
            this.tabControl1.SelectedIndex = tabID;
        }

        public void changeTabGfxOnly(int tabID)
        {
            if (!this.viaTabClick)
            {
                this.lastSoundTab = tabID;
            }
            this.ignore = true;
            this.lastTab = tabID;
            this.tabControl1.SelectedIndex = tabID;
            this.ignore = false;
        }

        public void changeTabLeft()
        {
            if (this.tabControl1.SelectedIndex <= 2)
            {
                this.changeTab(7);
            }
            else
            {
                this.changeTab(this.tabControl1.SelectedIndex - 1);
            }
        }

        public void changeTabRight()
        {
            if (this.tabControl1.SelectedIndex >= 7)
            {
                this.changeTab(2);
            }
            else
            {
                this.changeTab(this.tabControl1.SelectedIndex + 1);
            }
        }

        public void forceChangeTab(int tabID)
        {
            this.lastSoundTab = tabID;
            this.updateShownTabs();
            this.tabControl1.SelectedIndex = 9;
            this.ignore = true;
            if (tabID != 0)
            {
                this.tabControl1.SelectedIndex = 0;
            }
            else
            {
                this.tabControl1.SelectedIndex = 1;
            }
            this.ignore = false;
            this.tabControl1.SelectedIndex = tabID;
        }

        public int getCurrentTab()
        {
            return this.tabControl1.SelectedIndex;
        }

        public void init()
        {
            this.clearControls();
            this.tabControl1.Position = new Point(0x33, 3);
            base.addControl(this.tabControl1);
            this.initImages();
            int width = this.tabControl1.Create(10, this.images);
            this.tabControl1.setCallback(0, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage1_Enter), 40);
            this.tabControl1.setCallback(1, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage2_Enter), 0x29);
            this.tabControl1.setCallback(2, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage3_Enter), 0x2a);
            this.tabControl1.setCallback(3, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage4_Enter), 0x2b);
            this.tabControl1.setCallback(4, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage5_Enter), 0x2c);
            this.tabControl1.setCallback(5, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage6_Enter), 0x2d);
            this.tabControl1.setCallback(6, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage7_Enter), 0x2f);
            this.tabControl1.setCallback(7, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage8_Enter), 0x30);
            this.tabControl1.setCallback(8, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage9_Enter), 0);
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
                    GFXLibrary.VillageTabBar_1_Normal, GFXLibrary.VillageTabBar_1_Selected, GFXLibrary.VillageTabBar_2_Normal, GFXLibrary.VillageTabBar_2_Selected, GFXLibrary.VillageTabBar_3_Normal, GFXLibrary.VillageTabBar_3_Selected, GFXLibrary.VillageTabBar_5_Normal, GFXLibrary.VillageTabBar_5_Selected, GFXLibrary.VillageTabBar_7_Normal, GFXLibrary.VillageTabBar_7_Selected, GFXLibrary.VillageTabBar_6_Normal, GFXLibrary.VillageTabBar_6_Selected, GFXLibrary.VillageTabBar_8_Normal, GFXLibrary.VillageTabBar_8_Selected, GFXLibrary.VillageTabBar_9_Normal, GFXLibrary.VillageTabBar_9_Selected, 
                    GFXLibrary.VillageTabBar_4_Normal, GFXLibrary.VillageTabBar_4_Selected
                 };
            }
        }

        public void registerTabChangeCallback(TabChangeCallback newTabChangeCallback)
        {
            this.tabChangeCallback = newTabChangeCallback;
        }

        private void tabControl1_Click()
        {
            if (this.lastSoundTab != this.lastTab)
            {
                this.lastSoundTab = this.lastTab;
                GameEngine.Instance.playInterfaceSound("VillageScreen_village_tabbar_item_clicked");
            }
        }

        private void tabPage1_Enter()
        {
            if ((this.lastTab != 0) && !this.ignore)
            {
                this.lastTab = 0;
                this.viaTabClick = true;
                this.tabChangeCallback(0);
                this.viaTabClick = false;
            }
        }

        private void tabPage10_Enter()
        {
            if ((this.lastTab != 9) && !this.ignore)
            {
                this.lastTab = 9;
            }
        }

        private void tabPage2_Enter()
        {
            if ((this.lastTab != 1) && !this.ignore)
            {
                this.lastTab = 1;
                this.viaTabClick = true;
                this.tabChangeCallback(1);
                this.viaTabClick = false;
            }
        }

        private void tabPage3_Enter()
        {
            if ((this.lastTab != 2) && !this.ignore)
            {
                this.lastTab = 2;
                this.viaTabClick = true;
                this.tabChangeCallback(2);
                this.viaTabClick = false;
            }
        }

        private void tabPage4_Enter()
        {
            if ((this.lastTab != 3) && !this.ignore)
            {
                this.lastTab = 3;
                this.viaTabClick = true;
                this.tabChangeCallback(3);
                this.viaTabClick = false;
            }
        }

        private void tabPage5_Enter()
        {
            if ((this.lastTab != 4) && !this.ignore)
            {
                this.lastTab = 4;
                this.viaTabClick = true;
                this.tabChangeCallback(4);
                this.viaTabClick = false;
            }
        }

        private void tabPage6_Enter()
        {
            if ((this.lastTab != 5) && !this.ignore)
            {
                this.lastTab = 5;
                this.viaTabClick = true;
                this.tabChangeCallback(5);
                this.viaTabClick = false;
            }
        }

        private void tabPage7_Enter()
        {
            if ((this.lastTab != 6) && !this.ignore)
            {
                this.lastTab = 6;
                this.viaTabClick = true;
                this.tabChangeCallback(6);
                this.viaTabClick = false;
            }
        }

        private void tabPage8_Enter()
        {
            if ((this.lastTab != 7) && !this.ignore)
            {
                this.lastTab = 7;
                this.viaTabClick = true;
                this.tabChangeCallback(7);
                this.viaTabClick = false;
            }
        }

        private void tabPage9_Enter()
        {
            if ((this.lastTab != 8) && !this.ignore)
            {
                this.lastTab = 8;
                this.viaTabClick = true;
                this.tabChangeCallback(8);
                this.viaTabClick = false;
            }
        }

        public void updateShownTabs()
        {
            InterfaceMgr.Instance.updateVillageInfoBar();
            if (InterfaceMgr.Instance.isSelectedVillageACapital())
            {
                this.lastVillageCapital = true;
            }
            else
            {
                this.lastVillageCapital = false;
            }
            if (InterfaceMgr.Instance.isSelectedVillageACapital())
            {
                this.images[10] = GFXLibrary.VillageTabBar_INFO_Normal;
                this.images[11] = GFXLibrary.VillageTabBar_INFO_Selected;
                this.images[12] = GFXLibrary.VillageTabBar_VOTE_Normal;
                this.images[13] = GFXLibrary.VillageTabBar_VOTE_Selected;
                this.images[14] = GFXLibrary.VillageTabBar_FORUM_Normal;
                this.images[15] = GFXLibrary.VillageTabBar_FORUM_Selected;
                this.tabControl1.setTooltip(5, 0x31);
                this.tabControl1.setTooltip(6, 50);
                this.tabControl1.setTooltip(7, 0x33);
            }
            else
            {
                this.images[10] = GFXLibrary.VillageTabBar_6_Normal;
                this.images[11] = GFXLibrary.VillageTabBar_6_Selected;
                this.images[12] = GFXLibrary.VillageTabBar_8_Normal;
                this.images[13] = GFXLibrary.VillageTabBar_8_Selected;
                this.images[14] = GFXLibrary.VillageTabBar_9_Normal;
                this.images[15] = GFXLibrary.VillageTabBar_9_Selected;
                this.tabControl1.setTooltip(5, 0x2d);
                this.tabControl1.setTooltip(6, 0x2f);
                this.tabControl1.setTooltip(7, 0x30);
            }
            this.tabControl1.updateImageArray(this.images);
        }

        public delegate void TabChangeCallback(int tabID);
    }
}

