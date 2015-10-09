namespace Kingdoms
{
    using DXGraphics;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class FactionTabBar2 : CustomSelfDrawPanel.CSDControl
    {
        private bool ignore;
        private BaseImage[] images;
        private int lastSoundTab = -2;
        private int lastTab = -1;
        public bool lastVillageCapital;
        private TabChangeCallback tabChangeCallback;
        private CustomSelfDrawPanel.CSDTabControl tabControl1 = new CustomSelfDrawPanel.CSDTabControl();

        public void changeTab(int tabID)
        {
            this.lastSoundTab = tabID;
            this.updateShownTabs();
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

        public void forceChangeTab(int tabID)
        {
            this.lastSoundTab = tabID;
            this.updateShownTabs();
            this.tabControl1.SelectedIndex = 3;
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
            GameEngine.Instance.forceFactionTabChange();
            this.tabControl1.SelectedIndex = tabID;
        }

        public int getCurrentTab()
        {
            return this.tabControl1.SelectedIndex;
        }

        public void init()
        {
            this.clearControls();
            this.tabControl1.Position = new Point(0x132, 3);
            base.addControl(this.tabControl1);
            this.initImages();
            int width = this.tabControl1.Create(4, this.images);
            this.tabControl1.setCallback(0, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage1_Enter), 0x8fc);
            this.tabControl1.setCallback(1, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage2_Enter), 0x8fd);
            this.tabControl1.setCallback(2, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage3_Enter), 0x8fe);
            this.tabControl1.setCallback(3, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage4_Enter), 0);
            this.tabControl1.setSoundCallback(new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabControl1_Click));
            if (width > 0)
            {
                this.Size = new Size(width, this.images[0].Height);
                this.tabControl1.Size = new Size(width, this.images[0].Height);
            }
        }

        public void initImages()
        {
            this.images = new BaseImage[] { GFXLibrary.FactionTabBar_1_Normal, GFXLibrary.FactionTabBar_1_Selected, GFXLibrary.FactionTabBar_2_Normal, GFXLibrary.FactionTabBar_2_Selected, GFXLibrary.FactionTabBar_3_Normal, GFXLibrary.FactionTabBar_3_Selected, GFXLibrary.FactionTabBar_3_Normal, GFXLibrary.FactionTabBar_3_Selected };
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
                GameEngine.Instance.playInterfaceSound("FactionScreen_faction_tabbar_item_clicked");
            }
        }

        private void tabPage1_Enter()
        {
            if ((this.lastTab != 0) && !this.ignore)
            {
                this.lastTab = 0;
                this.tabChangeCallback(0);
            }
        }

        private void tabPage2_Enter()
        {
            if ((this.lastTab != 1) && !this.ignore)
            {
                this.lastTab = 1;
                this.tabChangeCallback(1);
            }
        }

        private void tabPage3_Enter()
        {
            if ((this.lastTab != 2) && !this.ignore)
            {
                this.lastTab = 2;
                this.tabChangeCallback(2);
            }
        }

        private void tabPage4_Enter()
        {
            if ((this.lastTab != 3) && !this.ignore)
            {
                this.lastTab = 3;
            }
        }

        public void updateShownTabs()
        {
        }

        public delegate void TabChangeCallback(int tabID);
    }
}

