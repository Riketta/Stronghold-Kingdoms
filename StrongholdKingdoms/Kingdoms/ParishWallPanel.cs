namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class ParishWallPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDExtendingPanel areaWindow = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.ParishChatPanel[] chatAreas;
        private int checkTextUpdateTime = 5;
        private IContainer components;
        private int currentLeaderID = -1;
        private string currentLeaderName = "";
        private int currentParish = -1;
        private DockableControl dockableControl;
        private int electedLeaderID = -1;
        private string electedLeaderName = "";
        private Panel focusPanel;
        private bool forceNextUpdate;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDImage illustrationImage = new CustomSelfDrawPanel.CSDImage();
        private bool initialTextInTextbox = true;
        private bool inSend;
        public static ParishWallPanel instance = null;
        private DateTime lastChatUpdate = DateTime.MinValue;
        private DateTime lastRequestTime = DateTime.MinValue;
        private int lastTab = -1;
        private List<CustomSelfDrawPanel.ParishWallEntry> lineList = new List<CustomSelfDrawPanel.ParishWallEntry>();
        private int m_currentVillage = -1;
        public static int m_userIDOnCurrent = -1;
        private WallInfo[] origWallInfo;
        private SparseArray parishList = new SparseArray();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel stewardLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton tab1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tab6Button = new CustomSelfDrawPanel.CSDButton();
        private TextBox textBox1;
        private CustomSelfDrawPanel.CSDImage textInputImage = new CustomSelfDrawPanel.CSDImage();
        private int[] unreadMessages = new int[6];
        private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private List<WallInfo> wallList = new List<WallInfo>();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public ParishWallPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            this.textBox1.Font = FontManager.GetFont("Microsoft Sans Serif", 9.75f);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        public void backfillPage(int pageID)
        {
            long oldestKnownID = -1L;
            foreach (Chat_TextEntry entry in GameEngine.Instance.World.getParishChat(this.currentParish, pageID, DateTime.MinValue))
            {
                if ((oldestKnownID == -1L) || (entry.textID < oldestKnownID))
                {
                    oldestKnownID = entry.textID;
                }
            }
            RemoteServices.Instance.set_Chat_BackFillParishText_UserCallBack(new RemoteServices.Chat_BackFillParishText_UserCallBack(this.chat_BackFillParishTextCallback));
            DateTime ownedDate = GameEngine.Instance.Village.m_ownedDate;
            if ((ownedDate == DateTime.MaxValue) && RemoteServices.Instance.Admin)
            {
                ownedDate = DateTime.MinValue;
            }
            RemoteServices.Instance.Chat_BackFillParishText(this.currentParish, pageID, oldestKnownID, ownedDate);
        }

        public void chat_BackFillParishTextCallback(Chat_BackFillParishText_ReturnType returnData)
        {
            if ((returnData.Success && (returnData.parishID == this.currentParish)) && (returnData.textList != null))
            {
                if ((RemoteServices.Instance.UserOptions.profanityFilter && (returnData.textList != null)) && (returnData.textList.Count > 0))
                {
                    foreach (Chat_TextEntry entry in returnData.textList)
                    {
                        entry.text = GameEngine.Instance.censorString(entry.text);
                    }
                }
                if (returnData.textList.Count > 0)
                {
                    List<Chat_TextEntry> list = GameEngine.Instance.World.addParishChat(returnData.parishID, returnData.textList);
                    this.chatAreas[returnData.pageID].importText(list.ToArray(), true, -1L);
                }
                else
                {
                    this.chatAreas[returnData.pageID].importText(returnData.textList.ToArray(), true, -1L);
                }
            }
            else
            {
                this.chatAreas[returnData.pageID].freeOldMessagesButton();
            }
        }

        public void chat_ReceiveParishTextCallback(Chat_ReceiveParishText_ReturnType returnData)
        {
            if ((RemoteServices.Instance.UserOptions.profanityFilter && (returnData.textList != null)) && (returnData.textList.Count > 0))
            {
                foreach (Chat_TextEntry entry in returnData.textList)
                {
                    entry.text = GameEngine.Instance.censorString(entry.text);
                }
            }
            if ((returnData.Success && (returnData.parishID == this.currentParish)) && ((returnData.textList != null) && (returnData.textList.Count > 0)))
            {
                this.checkTextUpdateTime = 10;
                this.importText(returnData.textList, returnData.unreadIDs);
            }
            else
            {
                this.importText(returnData.textList, returnData.unreadIDs);
            }
            this.checkTextUpdateTime += 2;
            if (this.checkTextUpdateTime >= 40)
            {
                this.checkTextUpdateTime = 40;
            }
            this.lastRequestTime = DateTime.Now;
            this.inSend = false;
        }

        public void chat_SendParishTextCallback(Chat_SendParishText_ReturnType returnData)
        {
            if (!this.chatAreas[this.lastTab].Locked)
            {
                this.textBox1.Enabled = true;
            }
            this.textBox1.Focus();
            if ((returnData.Success && (returnData.textList != null)) && (returnData.textList.Count > 0))
            {
                if (RemoteServices.Instance.UserOptions.profanityFilter)
                {
                    foreach (Chat_TextEntry entry in returnData.textList)
                    {
                        entry.text = GameEngine.Instance.censorString(entry.text);
                    }
                }
                this.checkTextUpdateTime = 2;
                this.importText(returnData.textList, returnData.unreadIDs);
            }
        }

        private void clearLastTabsUnreads(int pageID)
        {
            long[] readIDs = new long[] { -1L, -1L, -1L, -1L, -1L, -1L };
            int[] numArray2 = GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
            if ((numArray2 != null) && (numArray2[pageID] > 0))
            {
                long readID = GameEngine.Instance.World.getHighestReadID(this.currentParish, pageID);
                RemoteServices.Instance.Chat_MarkParishTextRead(this.currentParish, pageID, readID);
                readIDs[pageID] = readID;
                GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
            InterfaceMgr.Instance.closeDonatePopup();
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        private void createParishWall(WallInfo[] wallInfos)
        {
            this.origWallInfo = wallInfos;
            List<WallInfo> list = new List<WallInfo>();
            this.wallList.Clear();
            foreach (WallInfo info in wallInfos)
            {
                if (info.entryType == 1)
                {
                    bool flag = false;
                    foreach (WallInfo info2 in list)
                    {
                        if (info2.userID == info.userID)
                        {
                            flag = true;
                            info2.fData1 += info.fData1;
                            info2.data4 += info.data4;
                        }
                    }
                    if (!flag)
                    {
                        WallInfo item = new WallInfo {
                            data1 = info.data1,
                            data2 = info.data2,
                            data3 = info.data3,
                            data4 = info.data4,
                            fData1 = info.fData1,
                            entryType = info.entryType,
                            userID = info.userID,
                            username = info.username
                        };
                        list.Add(item);
                        this.wallList.Add(item);
                    }
                }
                else
                {
                    this.wallList.Add(info);
                }
            }
            this.updateWallArea();
        }

        public void deleteWallPost(long id)
        {
            this.chatAreas[0].importText(new Chat_TextEntry[0], false, id);
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

        public void flushData(int parishID)
        {
            this.parishList[parishID] = null;
            GameEngine.Instance.World.flushParishWallDonation(GameEngine.Instance.World.getParishCapital(parishID), RemoteServices.Instance.UserID);
        }

        public void forceUpdateParish()
        {
        }

        public void getParishFrontPageCallback(GetParishFrontPageInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                StoredParishInfo info = (StoredParishInfo) this.parishList[returnData.parishID];
                if (info == null)
                {
                    info = new StoredParishInfo();
                    this.parishList[returnData.parishID] = info;
                }
                info.m_lastUpdateTime = DateTime.Now;
                info.lastReturnData = returnData;
                if (this.currentParish == returnData.parishID)
                {
                    m_userIDOnCurrent = -1;
                    this.electedLeaderID = returnData.leaderID;
                    this.electedLeaderName = returnData.leaderName;
                    this.currentLeaderID = returnData.leaderID;
                    this.currentLeaderName = returnData.leaderName;
                    if (this.currentLeaderID == RemoteServices.Instance.UserID)
                    {
                        foreach (CustomSelfDrawPanel.ParishChatPanel panel in this.chatAreas)
                        {
                            panel.setAsSteward();
                        }
                    }
                    this.createParishWall(returnData.parishWallInfo);
                }
            }
            this.updateLeaderInfo();
        }

        private void importText(List<Chat_TextEntry> importTextList, long[] readIDs)
        {
            List<Chat_TextEntry> list = GameEngine.Instance.World.addParishChat(this.currentParish, importTextList);
            int[] numArray = GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
            List<Chat_TextEntry> list2 = new List<Chat_TextEntry>();
            List<Chat_TextEntry> list3 = new List<Chat_TextEntry>();
            List<Chat_TextEntry> list4 = new List<Chat_TextEntry>();
            List<Chat_TextEntry> list5 = new List<Chat_TextEntry>();
            List<Chat_TextEntry> list6 = new List<Chat_TextEntry>();
            List<Chat_TextEntry> list7 = new List<Chat_TextEntry>();
            if (list != null)
            {
                foreach (Chat_TextEntry entry in list)
                {
                    switch (entry.roomID)
                    {
                        case 0:
                            list2.Add(entry);
                            break;

                        case 1:
                            list3.Add(entry);
                            break;

                        case 2:
                            list4.Add(entry);
                            break;

                        case 3:
                            list5.Add(entry);
                            break;

                        case 4:
                            list6.Add(entry);
                            break;

                        case 5:
                            list7.Add(entry);
                            break;
                    }
                }
            }
            this.chatAreas[0].importText(list2.ToArray(), false, -1L);
            this.chatAreas[1].importText(list3.ToArray(), false, -1L);
            this.chatAreas[2].importText(list4.ToArray(), false, -1L);
            this.chatAreas[3].importText(list5.ToArray(), false, -1L);
            this.chatAreas[4].importText(list6.ToArray(), false, -1L);
            this.chatAreas[5].importText(list7.ToArray(), false, -1L);
            for (int i = 0; i < 6; i++)
            {
                this.chatAreas[i].setUnreads(numArray[i]);
            }
        }

        public void init(bool resized)
        {
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            this.m_currentVillage = villageID;
            int parishID = GameEngine.Instance.World.getParishFromVillageID(villageID);
            int height = base.Height;
            instance = this;
            base.clearControls();
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            CustomSelfDrawPanel.WikiLinkControl.init(this.headerImage, 14, new Point(base.Width - 0x2c, 3));
            this.backgroundImage.Size = new Size(base.Width, height - 40);
            this.backgroundImage.Position = new Point(0, 40);
            base.addControl(this.backgroundImage);
            this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            this.parishNameLabel.Text = GameEngine.Instance.World.getParishName(parishID);
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.illustrationImage.Image = (Image) GFXLibrary.parishwall_village_illlustration_01;
            this.illustrationImage.Position = new Point(0x11, 5);
            this.backgroundImage.addControl(this.illustrationImage);
            this.stewardLabel.Text = SK.Text("ParishWallPanel_Steward", "Steward") + " : ";
            this.stewardLabel.Color = ARGBColors.Black;
            this.stewardLabel.Position = new Point(5, 5);
            this.stewardLabel.Size = new Size(this.illustrationImage.Width - 6, 30);
            this.stewardLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.stewardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.illustrationImage.addControl(this.stewardLabel);
            this.wallInfoImage.Size = new Size(0x18c, height - 170);
            this.wallInfoImage.Position = new Point(8, 0x77);
            this.backgroundImage.addControl(this.wallInfoImage);
            this.wallInfoImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
            this.areaWindow.Size = new Size(0x234, height - 0x4e);
            this.areaWindow.Position = new Point(0x19b, 0x1a);
            this.backgroundImage.addControl(this.areaWindow);
            this.areaWindow.Create((Image) GFXLibrary.parishwall_village_center_tab_outline_top_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_left, null, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_right);
            this.tab1Button.UseTextSize = true;
            this.tab1Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
            this.tab1Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
            this.tab1Button.Position = new Point(0x1a9, 6);
            this.tab1Button.Text.Text = SK.Text("ParishWallPanel_General", "General");
            this.tab1Button.Text.Size = new Size(this.tab1Button.Size.Width, this.tab1Button.Text.Size.Height + 20);
            this.tab1Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.tab1Button.TextYOffset = 3;
            this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.tab1Button.Text.Color = ARGBColors.Black;
            this.tab1Button.Data = 0;
            this.tab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
            this.backgroundImage.addControl(this.tab1Button);
            this.tab2Button.UseTextSize = true;
            this.tab2Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab2Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab2Button.Position = new Point(510, 6);
            this.tab2Button.Text.Text = SK.Text("ParishWallPanel_War", "War");
            this.tab2Button.Text.Size = new Size(this.tab2Button.Size.Width, this.tab2Button.Text.Size.Height + 20);
            this.tab2Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.tab2Button.TextYOffset = 3;
            this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab2Button.Text.Color = ARGBColors.Black;
            this.tab2Button.Data = 1;
            this.tab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
            this.backgroundImage.addControl(this.tab2Button);
            this.tab3Button.UseTextSize = true;
            this.tab3Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab3Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab3Button.Position = new Point(0x253, 6);
            this.tab3Button.Text.Text = SK.Text("ParishWallPanel_inn", "Inn");
            this.tab3Button.Text.Size = new Size(this.tab3Button.Size.Width, this.tab3Button.Text.Size.Height + 20);
            this.tab3Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.tab3Button.TextYOffset = 3;
            this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab3Button.Text.Color = ARGBColors.Black;
            this.tab3Button.Data = 2;
            this.tab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
            this.backgroundImage.addControl(this.tab3Button);
            this.tab4Button.UseTextSize = true;
            this.tab4Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab4Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab4Button.Position = new Point(680, 6);
            this.tab4Button.Text.Text = SK.Text("ParishWallPanel_Steward", "Steward");
            this.tab4Button.Text.Size = new Size(this.tab4Button.Size.Width, this.tab4Button.Text.Size.Height + 20);
            this.tab4Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.tab4Button.TextYOffset = 3;
            this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab4Button.Text.Color = ARGBColors.Black;
            this.tab4Button.Data = 3;
            this.tab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
            this.backgroundImage.addControl(this.tab4Button);
            this.tab5Button.UseTextSize = true;
            this.tab5Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab5Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab5Button.Position = new Point(0x2fd, 6);
            this.tab5Button.Text.Text = SK.Text("ParishWallPanel_Free", "Free");
            this.tab5Button.Text.Size = new Size(this.tab5Button.Size.Width, this.tab5Button.Text.Size.Height + 20);
            this.tab5Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.tab5Button.TextYOffset = 3;
            this.tab5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab5Button.Text.Color = ARGBColors.Black;
            this.tab5Button.Data = 4;
            this.tab5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
            this.backgroundImage.addControl(this.tab5Button);
            this.tab6Button.UseTextSize = true;
            this.tab6Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab6Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab6Button.Position = new Point(850, 6);
            this.tab6Button.Text.Text = SK.Text("ParishWallPanel_Free", "Free");
            this.tab6Button.Text.Size = new Size(this.tab6Button.Size.Width, this.tab6Button.Text.Size.Height + 20);
            this.tab6Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.tab6Button.TextYOffset = 3;
            this.tab6Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab6Button.Text.Color = ARGBColors.Black;
            this.tab6Button.Data = 5;
            this.tab6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabClick), "ParishWallPanel_tab");
            this.backgroundImage.addControl(this.tab6Button);
            this.textInputImage.Image = (Image) GFXLibrary.parishwall_what_say_thou_box;
            this.textInputImage.Position = new Point(0x1b0, 0x2f);
            this.backgroundImage.addControl(this.textInputImage);
            this.wallScrollArea.Position = new Point(15, 15);
            this.wallScrollArea.Size = new Size(0x151, height - 0xbf);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x151, height - 0xbf));
            this.wallInfoImage.addControl(this.wallScrollArea);
            int max = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x166, 15);
            this.wallScrollBar.Size = new Size(0x18, height - 0xbf);
            this.wallInfoImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            if (resized)
            {
                this.updateWallArea();
                if ((max > 0) && this.wallScrollBar.Visible)
                {
                    if (max >= this.wallScrollBar.Max)
                    {
                        max = this.wallScrollBar.Max;
                    }
                    this.wallScrollBar.Value = max;
                    this.wallScrollBarMoved();
                }
                int id = 0;
                foreach (CustomSelfDrawPanel.ParishChatPanel panel4 in this.chatAreas)
                {
                    panel4.Size = new Size(0x216, height - 0x99);
                    this.areaWindow.addControl(panel4);
                    panel4.reset(this, id);
                    if (GameEngine.Instance.Village != null)
                    {
                        panel4.importText(GameEngine.Instance.World.getParishChat(parishID, id, GameEngine.Instance.Village.m_ownedDate).ToArray(), false, -1L);
                    }
                    panel4.scrollToBottom();
                    panel4.Visible = false;
                    id++;
                }
                this.tabEntered(this.lastTab);
                goto Label_1127;
            }
            this.focusPanel.Focus();
            this.initialTextInTextbox = true;
            this.textBox1.Text = SK.Text("ParishWallPanel_Enter_Text_Here", "Enter Text Here");
            StoredParishInfo info = (StoredParishInfo) this.parishList[parishID];
            bool flag = false;
            if (info != null)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - info.m_lastUpdateTime);
                if ((span.TotalMinutes <= 1.0) && (info.lastReturnData != null))
                {
                    goto Label_0D51;
                }
            }
            flag = true;
        Label_0D51:
            if (this.chatAreas == null)
            {
                this.chatAreas = new CustomSelfDrawPanel.ParishChatPanel[6];
                for (int i = 0; i < 6; i++)
                {
                    this.chatAreas[i] = new CustomSelfDrawPanel.ParishChatPanel { Position = new Point(20, 0x44), Size = new Size(0x216, height - 0x99) };
                }
            }
            if ((this.currentParish != parishID) || this.forceNextUpdate)
            {
                this.forceNextUpdate = false;
                this.currentLeaderID = -1;
                this.electedLeaderID = -1;
                this.currentLeaderName = "";
                this.electedLeaderName = "";
                m_userIDOnCurrent = -1;
                this.checkTextUpdateTime = 5;
                int num6 = 0;
                foreach (CustomSelfDrawPanel.ParishChatPanel panel2 in this.chatAreas)
                {
                    this.areaWindow.addControl(panel2);
                    panel2.Visible = false;
                    panel2.reset(this, num6);
                    if (GameEngine.Instance.Village != null)
                    {
                        panel2.importText(GameEngine.Instance.World.getParishChat(parishID, num6, GameEngine.Instance.Village.m_ownedDate).ToArray(), false, -1L);
                    }
                    panel2.scrollToBottom();
                    panel2.Visible = false;
                    num6++;
                }
                long[] readIDs = new long[] { -1L, -1L, -1L, -1L, -1L, -1L };
                int[] numArray2 = GameEngine.Instance.World.setReadIDs(parishID, readIDs);
                if (numArray2 != null)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        this.chatAreas[j].setUnreads(numArray2[j]);
                    }
                }
                this.currentParish = parishID;
                this.tabEntered(0);
            }
            else
            {
                foreach (CustomSelfDrawPanel.ParishChatPanel panel3 in this.chatAreas)
                {
                    panel3.Repopulate = true;
                    panel3.Size = new Size(0x216, height - 0x99);
                    this.areaWindow.addControl(panel3);
                    panel3.Visible = false;
                }
                this.currentParish = parishID;
                this.tabEntered(this.lastTab);
            }
            this.currentParish = parishID;
            if (GameEngine.Instance.Village != null)
            {
                if (flag)
                {
                    RemoteServices.Instance.set_GetParishFrontPageInfo_UserCallBack(new RemoteServices.GetParishFrontPageInfo_UserCallBack(this.getParishFrontPageCallback));
                    RemoteServices.Instance.GetParishFrontPageInfo(this.m_currentVillage, DateTime.MinValue);
                    Thread.Sleep(500);
                }
                else
                {
                    DateTime lastUpdateTime = info.m_lastUpdateTime;
                    this.getParishFrontPageCallback(info.lastReturnData);
                    info.m_lastUpdateTime = lastUpdateTime;
                }
                this.inSend = true;
                RemoteServices.Instance.set_Chat_ReceiveParishText_UserCallBack(new RemoteServices.Chat_ReceiveParishText_UserCallBack(this.chat_ReceiveParishTextCallback));
                RemoteServices.Instance.Chat_ReceiveParishText(this.currentParish, GameEngine.Instance.World.getParishChatNewestPostTime(this.currentParish, GameEngine.Instance.Village.m_ownedDate));
            }
            else
            {
                this.forceNextUpdate = true;
            }
        Label_1127:
            this.updateLeaderInfo();
        }

        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.focusPanel = new Panel();
            base.SuspendLayout();
            this.textBox1.BackColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.textBox1.BorderStyle = BorderStyle.None;
            this.textBox1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox1.ForeColor = ARGBColors.Black;
            this.textBox1.Location = new Point(0x1b7, 0x5e);
            this.textBox1.MaxLength = 100;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x1cd, 0x12);
            this.textBox1.TabIndex = 0x63;
            this.textBox1.Text = "Enter text here";
            this.textBox1.WordWrap = false;
            this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.Enter += new EventHandler(this.textBox1_Enter);
            this.focusPanel.BackColor = ARGBColors.Transparent;
            this.focusPanel.ForeColor = ARGBColors.Transparent;
            this.focusPanel.Location = new Point(0x3dc, 3);
            this.focusPanel.Name = "focusPanel";
            this.focusPanel.Size = new Size(1, 1);
            this.focusPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.focusPanel);
            base.Controls.Add(this.textBox1);
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "ParishWallPanel";
            base.Size = new Size(0x3e0, 0x236);
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

        public void leaving()
        {
            if (this.lastTab >= 0)
            {
                this.clearLastTabsUnreads(this.lastTab);
            }
        }

        public void logout()
        {
            this.parishList.Clear();
            this.currentParish = -1;
        }

        public void sendParishText(string text, int id)
        {
            if (GameEngine.Instance.Village != null)
            {
                text = text.Replace("\n", " ");
                text = text.Replace("\r", " ");
                text = text.Replace("\t", " ");
                RemoteServices.Instance.set_Chat_SendParishText_UserCallBack(new RemoteServices.Chat_SendParishText_UserCallBack(this.chat_SendParishTextCallback));
                RemoteServices.Instance.Chat_SendParishText(text, this.currentParish, id, GameEngine.Instance.World.getParishChatNewestPostTime(this.currentParish, GameEngine.Instance.Village.m_ownedDate));
            }
        }

        public void setTabText(int tabID, string title)
        {
            switch (tabID)
            {
                case 0:
                    this.tab1Button.Text.Text = title;
                    return;

                case 1:
                    this.tab2Button.Text.Text = title;
                    return;

                case 2:
                    this.tab3Button.Text.Text = title;
                    return;

                case 3:
                    this.tab4Button.Text.Text = title;
                    return;

                case 4:
                    this.tab5Button.Text.Text = title;
                    return;

                case 5:
                    this.tab6Button.Text.Text = title;
                    return;
            }
        }

        private void swipeLeft()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabLeft();
        }

        private void swiperight()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabRight();
        }

        private void tabClick()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                if (this.lastTab != data)
                {
                    this.clearLastTabsUnreads(this.lastTab);
                }
                this.tabEntered(data);
            }
        }

        private void tabEntered(int pageID)
        {
            this.lastTab = pageID;
            long[] readIDs = new long[] { -1L, -1L, -1L, -1L, -1L, -1L };
            int[] numArray2 = GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
            if (numArray2 != null)
            {
                for (int j = 0; j < 6; j++)
                {
                    this.chatAreas[j].setUnreads(numArray2[j]);
                }
                if (numArray2[pageID] > 0)
                {
                    long readID = GameEngine.Instance.World.getHighestReadID(this.currentParish, pageID);
                    RemoteServices.Instance.Chat_MarkParishTextRead(this.currentParish, pageID, readID);
                    readIDs[pageID] = readID;
                    GameEngine.Instance.World.setReadIDs(this.currentParish, readIDs);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                this.chatAreas[i].Visible = i == pageID;
            }
            this.tab1Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab1Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab2Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab2Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab3Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab3Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab4Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab4Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab5Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab5Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab6Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab6Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_down;
            this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tab6Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            switch (pageID)
            {
                case 0:
                    this.tab1Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab1Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    break;

                case 1:
                    this.tab2Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab2Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    break;

                case 2:
                    this.tab3Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab3Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    break;

                case 3:
                    this.tab4Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab4Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    break;

                case 4:
                    this.tab5Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab5Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    break;

                case 5:
                    this.tab6Button.ImageNorm = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab6Button.ImageOver = (Image) GFXLibrary.parishwall_village_center_tab_up;
                    this.tab6Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    break;
            }
            if (!this.chatAreas[pageID].Locked)
            {
                this.textBox1.Enabled = true;
            }
            else
            {
                this.textBox1.Enabled = false;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (this.initialTextInTextbox)
            {
                this.initialTextInTextbox = false;
                this.textBox1.Text = "";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.textBox1.Text.Length > 0)
                {
                    this.sendParishText(this.textBox1.Text, this.lastTab);
                    this.textBox1.Text = "";
                    this.textBox1.Enabled = false;
                }
                e.Handled = true;
            }
        }

        public void update()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.lastRequestTime);
            if (((span.TotalSeconds > this.checkTextUpdateTime) && !this.inSend) && (RemoteServices.Instance.ChatActive && (GameEngine.Instance.Village != null)))
            {
                this.inSend = true;
                RemoteServices.Instance.set_Chat_ReceiveParishText_UserCallBack(new RemoteServices.Chat_ReceiveParishText_UserCallBack(this.chat_ReceiveParishTextCallback));
                RemoteServices.Instance.Chat_ReceiveParishText(this.currentParish, GameEngine.Instance.World.getParishChatNewestPostTime(this.currentParish, GameEngine.Instance.Village.m_ownedDate));
            }
        }

        public void updateLeaderInfo()
        {
            this.stewardLabel.Text = SK.Text("ParishWallPanel_Steward", "Steward") + " : " + this.currentLeaderName;
            m_userIDOnCurrent = this.currentLeaderID;
            this.update();
        }

        public void updateWallArea()
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            int lineID = 0;
            foreach (WallInfo info in this.wallList)
            {
                CustomSelfDrawPanel.ParishWallEntry control = new CustomSelfDrawPanel.ParishWallEntry();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(info, lineID, this.m_currentVillage, this);
                this.wallScrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
                lineID++;
            }
            this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, y);
            if (y < this.wallScrollBar.Height)
            {
                this.wallScrollBar.Visible = false;
            }
            else
            {
                this.wallScrollBar.Visible = true;
                this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
                this.wallScrollBar.Max = y - this.wallScrollBar.Height;
            }
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 15 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class StoredParishInfo
        {
            public GetParishFrontPageInfo_ReturnType lastReturnData;
            public DateTime m_lastUpdateTime = DateTime.MinValue;
        }
    }
}

