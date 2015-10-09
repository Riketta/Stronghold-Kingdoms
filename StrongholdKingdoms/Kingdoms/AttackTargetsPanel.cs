namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class AttackTargetsPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel favouritesHeader = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDListBox favouritesList = new CustomSelfDrawPanel.CSDListBox();
        private AttackTargetsPopup m_parent;
        private int m_selectedVillageID = -1;
        private CustomSelfDrawPanel.CSDLabel recentHeader = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDListBox recentList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton removeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton scoutButton = new CustomSelfDrawPanel.CSDButton();
        private static List<WorldMap.VillageNameItem> villageFavourites = new List<WorldMap.VillageNameItem>();
        private static List<WorldMap.VillageNameItem> villageHistory = new List<WorldMap.VillageNameItem>();

        public AttackTargetsPanel()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public static void addFavourite(int villageID)
        {
            if (!isFavourite(villageID))
            {
                WorldMap.VillageNameItem item = new WorldMap.VillageNameItem {
                    villageID = villageID
                };
                villageFavourites.Add(item);
                RemoteServices.Instance.UpdateVillageFavourites(4, villageID);
            }
        }

        public static void addFavourites(List<GenericVillageHistoryData> newData)
        {
            villageFavourites.Clear();
            if (newData != null)
            {
                foreach (GenericVillageHistoryData data in newData)
                {
                    WorldMap.VillageNameItem item = new WorldMap.VillageNameItem {
                        villageID = data.villageID
                    };
                    villageFavourites.Add(item);
                }
            }
        }

        public static void addHistory(List<GenericVillageHistoryData> newData)
        {
            villageHistory.Clear();
            if (newData != null)
            {
                foreach (GenericVillageHistoryData data in newData)
                {
                    WorldMap.VillageNameItem item = new WorldMap.VillageNameItem {
                        villageID = data.villageID
                    };
                    villageHistory.Add(item);
                }
            }
        }

        public static void addRecent(int villageID)
        {
            WorldMap.VillageNameItem item = new WorldMap.VillageNameItem {
                villageID = villageID
            };
            villageHistory.Add(item);
        }

        private void attackClicked()
        {
            this.closeClick();
            GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, InterfaceMgr.Instance.OwnSelectedVillage, this.m_selectedVillageID);
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closeAttackTargetsPopup();
        }

        private void favouriteClick(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.recentList.clearSelectedItem();
                if (item.Data >= 0)
                {
                    this.m_selectedVillageID = item.Data;
                    this.removeButton.Visible = true;
                    this.updateButtons();
                }
            }
        }

        private void favouriteDoubleClick(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.recentList.clearSelectedItem();
                if (item.Data >= 0)
                {
                    this.m_selectedVillageID = item.Data;
                    GameEngine.Instance.World.zoomToVillage(item.Data);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(item.Data, false, true, false, false);
                    this.removeButton.Visible = true;
                    this.updateButtons();
                }
            }
        }

        private void fillBoxes()
        {
            List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
            foreach (WorldMap.VillageNameItem item in villageFavourites)
            {
                if (GameEngine.Instance.World.isVillageVisible(item.villageID))
                {
                    CustomSelfDrawPanel.CSDListItem item2 = new CustomSelfDrawPanel.CSDListItem {
                        Text = GameEngine.Instance.World.getVillageNameOrType(item.villageID),
                        Data = item.villageID
                    };
                    items.Add(item2);
                }
            }
            items.Sort((Comparison<CustomSelfDrawPanel.CSDListItem>) ((first, next) => first.Text.CompareTo(next.Text)));
            this.favouritesList.populate(items);
            List<CustomSelfDrawPanel.CSDListItem> list2 = new List<CustomSelfDrawPanel.CSDListItem>();
            foreach (WorldMap.VillageNameItem item3 in villageHistory)
            {
                if (GameEngine.Instance.World.isVillageVisible(item3.villageID))
                {
                    CustomSelfDrawPanel.CSDListItem item4 = new CustomSelfDrawPanel.CSDListItem {
                        Text = GameEngine.Instance.World.getVillageNameOrType(item3.villageID),
                        Data = item3.villageID
                    };
                    list2.Add(item4);
                }
            }
            list2.Sort((Comparison<CustomSelfDrawPanel.CSDListItem>) ((first, next) => first.Text.CompareTo(next.Text)));
            this.recentList.populate(list2);
        }

        public void init(AttackTargetsPopup parent)
        {
            this.m_parent = parent;
            base.Size = this.m_parent.Size;
            this.BackColor = ARGBColors.Transparent;
            CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage {
                Alpha = 0.1f,
                Image = (Image) GFXLibrary.formations_img,
                Scale = 5.0,
                Position = new Point(0, 0),
                Size = base.Size
            };
            base.addControl(control);
            this.favouritesHeader.Text = SK.Text("Attack_Targets_Favourites", "Favourite Targets");
            this.favouritesHeader.Color = ARGBColors.White;
            this.favouritesHeader.Position = new Point(30, 0);
            this.favouritesHeader.Size = new Size(300, 30);
            this.favouritesHeader.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.favouritesHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            control.addControl(this.favouritesHeader);
            this.recentHeader.Text = SK.Text("Attack_Targets_Recent", "Recent Targets");
            this.recentHeader.Color = ARGBColors.White;
            this.recentHeader.Position = new Point(370, 0);
            this.recentHeader.Size = new Size(300, 30);
            this.recentHeader.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.recentHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            control.addControl(this.recentHeader);
            this.favouritesList.Size = new Size(300, 0x156);
            this.favouritesList.Position = new Point(30, 30);
            control.addControl(this.favouritesList);
            this.favouritesList.Create(0x13, 0x12);
            this.favouritesList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.favouriteClick));
            this.favouritesList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.favouriteDoubleClick));
            this.recentList.Size = new Size(300, 0x156);
            this.recentList.Position = new Point(370, 30);
            control.addControl(this.recentList);
            this.recentList.Create(0x13, 0x12);
            this.recentList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.recentClick));
            this.recentList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.recentDoubleClick));
            this.closeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.closeButton.Position = new Point(540, base.Height - 70);
            this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.closeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.closeButton.TextYOffset = -3;
            this.closeButton.Text.Color = ARGBColors.Black;
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
            control.addControl(this.closeButton);
            this.attackButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.attackButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.attackButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.attackButton.Position = new Point(380, base.Height - 70);
            this.attackButton.Text.Text = SK.Text("GENERIC_Attack", "Attack");
            this.attackButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.attackButton.TextYOffset = -3;
            this.attackButton.Text.Color = ARGBColors.Black;
            this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackClicked));
            this.attackButton.Enabled = false;
            control.addControl(this.attackButton);
            this.scoutButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.scoutButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.scoutButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.scoutButton.Position = new Point(220, base.Height - 70);
            this.scoutButton.Text.Text = SK.Text("GENERIC_Scout", "Scout");
            this.scoutButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.scoutButton.TextYOffset = -3;
            this.scoutButton.Text.Color = ARGBColors.Black;
            this.scoutButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.scoutClicked));
            this.scoutButton.Enabled = false;
            control.addControl(this.scoutButton);
            this.removeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.removeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.removeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.removeButton.Position = new Point(30, base.Height - 70);
            this.removeButton.Text.Text = SK.Text("MailScreen_Remove", "Remove");
            this.removeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.removeButton.TextYOffset = -3;
            this.removeButton.Text.Color = ARGBColors.Black;
            this.removeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeClicked));
            this.removeButton.Visible = false;
            control.addControl(this.removeButton);
            this.fillBoxes();
        }

        public static bool isFavourite(int villageID)
        {
            foreach (WorldMap.VillageNameItem item in villageFavourites)
            {
                if (item.villageID == villageID)
                {
                    return true;
                }
            }
            return false;
        }

        private void listDoubleClick(CustomSelfDrawPanel.CSDListItem item)
        {
        }

        private void recentClick(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.favouritesList.clearSelectedItem();
                if (item.Data >= 0)
                {
                    this.m_selectedVillageID = item.Data;
                    this.removeButton.Visible = false;
                    this.updateButtons();
                }
            }
        }

        private void recentDoubleClick(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.favouritesList.clearSelectedItem();
                if (item.Data >= 0)
                {
                    this.m_selectedVillageID = item.Data;
                    GameEngine.Instance.World.zoomToVillage(item.Data);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(item.Data, false, true, false, false);
                    this.removeButton.Visible = false;
                    this.updateButtons();
                }
            }
        }

        private void removeClicked()
        {
            removeFavourite(this.m_selectedVillageID);
            this.fillBoxes();
            this.removeButton.Visible = false;
        }

        public static void removeFavourite(int villageID)
        {
            if (isFavourite(villageID))
            {
                foreach (WorldMap.VillageNameItem item in villageFavourites)
                {
                    if (item.villageID == villageID)
                    {
                        villageFavourites.Remove(item);
                        RemoteServices.Instance.UpdateVillageFavourites(5, villageID);
                        break;
                    }
                }
            }
        }

        private void scoutClicked()
        {
            InterfaceMgr.Instance.openScoutPopupWindow(this.m_selectedVillageID, true);
            this.closeClick();
        }

        private void updateButtons()
        {
            if (this.m_selectedVillageID >= 0)
            {
                if (GameEngine.Instance.World.getSpecial(this.m_selectedVillageID) == 0)
                {
                    if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
                    {
                        this.scoutButton.Enabled = false;
                        if (!GameEngine.Instance.World.isUserVillage(InterfaceMgr.Instance.getSelectedMenuVillage()))
                        {
                            this.attackButton.Enabled = false;
                        }
                        else
                        {
                            this.attackButton.Enabled = true;
                        }
                    }
                    else
                    {
                        this.scoutButton.Enabled = true;
                        this.attackButton.Enabled = true;
                    }
                }
                else
                {
                    this.attackButton.Enabled = GameEngine.Instance.World.isAttackableSpecial(this.m_selectedVillageID);
                    this.scoutButton.Enabled = GameEngine.Instance.World.isScoutableSpecial(this.m_selectedVillageID) && !GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage);
                }
            }
            else
            {
                this.attackButton.Enabled = false;
                this.scoutButton.Enabled = false;
            }
        }

        public static List<WorldMap.VillageNameItem> VillageFavourites
        {
            get
            {
                return villageFavourites;
            }
        }

        public static List<WorldMap.VillageNameItem> VillageHistory
        {
            get
            {
                return villageHistory;
            }
        }
    }
}

