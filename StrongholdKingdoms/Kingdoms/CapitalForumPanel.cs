namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CapitalForumPanel : CustomSelfDrawPanel, IDockableControl, IForumPostParent
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private long currentlySelectedForum = -1L;
        private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private SparseArray forumArray = new SparseArray();
        public ForumComparer forumComparer = new ForumComparer();
        private SparseArray forumThreadArray = new SparseArray();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static CapitalForumPanel instance;
        private DateTime lastRefreshTime = DateTime.MinValue;
        private List<ForumThreadLine> lineList = new List<ForumThreadLine>();
        private FactionNewTopicPopup m_popup;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDButton newTopicButton = new CustomSelfDrawPanel.CSDButton();
        private string OrigForumName = "";
        public const int PANEL_ID = 0x2d;
        private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
        private int selectedAreaID = -1;
        private int selectedAreaType = -1;
        private ThreadComparer threadComparer = new ThreadComparer();
        private CustomSelfDrawPanel.CSDLabel threadTitleLabel = new CustomSelfDrawPanel.CSDLabel();
        public string titleForum = "";
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public CapitalForumPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void clearForum()
        {
            this.forumArray = new SparseArray();
            this.lastRefreshTime = DateTime.MinValue;
            this.forumThreadArray = new SparseArray();
            this.currentlySelectedForum = -1L;
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

        public void deleteForumThreadCallback(DeleteForumThread_ReturnType returnData)
        {
            if (returnData.Success && (returnData.threadID >= 0L))
            {
                ForumInfoData data = (ForumInfoData) this.forumThreadArray[returnData.forumID];
                if (data != null)
                {
                    foreach (ForumThreadData data2 in data.forumThreads)
                    {
                        if (data2.threadID == returnData.threadID)
                        {
                            data.forumThreads.Remove(data2);
                            this.updateForum();
                            break;
                        }
                    }
                }
            }
        }

        public void deleteTopic(long threadID)
        {
            RemoteServices.Instance.set_DeleteForumThread_UserCallBack(new RemoteServices.DeleteForumThread_UserCallBack(this.deleteForumThreadCallback));
            RemoteServices.Instance.DeleteForumThread(this.selectedAreaID, this.selectedAreaType, this.OrigForumName, this.currentlySelectedForum, threadID);
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

        private void forumSelectedClick()
        {
            long dataL = base.ClickedControl.DataL;
            if (dataL != this.currentlySelectedForum)
            {
                this.currentlySelectedForum = dataL;
                this.initForum();
            }
        }

        public void forumThreadListCallback(GetForumThreadList_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (((returnData.forumThreadInfo != null) && (returnData.forumThreadInfo.Count > 0)) && RemoteServices.Instance.UserOptions.profanityFilter)
                {
                    foreach (ForumThreadInfo info in returnData.forumThreadInfo)
                    {
                        info.threadTitle = GameEngine.Instance.censorString(info.threadTitle);
                    }
                }
                this.importThreadList(returnData.forumThreadInfo, returnData.forumID);
            }
        }

        public void getForumListCallback(GetForumList_ReturnType returnData)
        {
            if (returnData.Success)
            {
                foreach (ForumInfo info in returnData.forumInfo)
                {
                    ForumData data = new ForumData {
                        areaID = info.areaID,
                        areaType = info.areaType,
                        forumID = info.forumID,
                        forumTitle = info.forumTitle,
                        lastTime = info.lastDate,
                        numPosts = info.numPosts,
                        numReadPosts = info.numReadPosts
                    };
                    this.forumArray[data.forumID] = data;
                }
                this.lastRefreshTime = DateTime.Now;
                this.initForum();
            }
        }

        public void importThreadList(List<ForumThreadInfo> threadData, long forumID)
        {
            if (threadData != null)
            {
                if (this.forumThreadArray[forumID] == null)
                {
                    ForumInfoData data = new ForumInfoData {
                        forumID = forumID
                    };
                    this.forumThreadArray[forumID] = data;
                }
                ForumInfoData data2 = (ForumInfoData) this.forumThreadArray[forumID];
                foreach (ForumThreadInfo info in threadData)
                {
                    ForumThreadData item = new ForumThreadData {
                        title = info.threadTitle,
                        threadID = info.threadID,
                        lastTime = info.lastDate,
                        userName = info.userName,
                        read = info.threadRead
                    };
                    if (item.lastTime > data2.lastTime)
                    {
                        data2.lastTime = item.lastTime;
                    }
                    bool flag = false;
                    for (int i = 0; i < data2.forumThreads.Count; i++)
                    {
                        ForumThreadData data4 = data2.forumThreads[i];
                        if (data4.threadID == item.threadID)
                        {
                            data2.forumThreads[i] = item;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        data2.forumThreads.Add(item);
                    }
                }
                data2.forumThreads.Sort(this.threadComparer);
                this.updateForum();
            }
        }

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 9);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(0x19f, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(0x225, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            if (this.selectedAreaType == 3)
            {
                this.titleForum = SK.Text("ParishForumPanel_Parish_Forum", "Parish Forum");
            }
            if (this.selectedAreaType == 2)
            {
                this.titleForum = SK.Text("ParishForumPanel_County_Forum", "County Forum");
            }
            if (this.selectedAreaType == 1)
            {
                this.titleForum = SK.Text("ParishForumPanel_Province_Forum", "Province Forum");
            }
            if (this.selectedAreaType == 0)
            {
                this.titleForum = SK.Text("ParishForumPanel_Country_Forum", "Country Forum");
            }
            InterfaceMgr.Instance.setVillageHeading(this.titleForum);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x10, new Point((base.Width - 30) + 2, 7), true);
            this.newTopicButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.newTopicButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.newTopicButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.newTopicButton.Position = new Point(20, height - 30);
            this.newTopicButton.Text.Text = SK.Text("FORUMS_New_Topic", "New Topic");
            this.newTopicButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.newTopicButton.TextYOffset = -3;
            this.newTopicButton.Text.Color = ARGBColors.Black;
            this.newTopicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.newTopicClick), "CapitalForumPanel_new_topic");
            this.mainBackgroundImage.addControl(this.newTopicButton);
            this.threadTitleLabel.Text = SK.Text("FactionInvites_Thread_Title", "Thread Title");
            this.threadTitleLabel.Color = ARGBColors.Black;
            this.threadTitleLabel.Position = new Point(9, -2);
            this.threadTitleLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.threadTitleLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.threadTitleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.threadTitleLabel);
            this.playersLabel.Text = SK.Text("VillageMapPanel_Player", "Player");
            this.playersLabel.Color = ARGBColors.Black;
            this.playersLabel.Position = new Point(420, -2);
            this.playersLabel.Size = new Size(140, this.headerLabelsImage.Height);
            this.playersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.playersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.playersLabel);
            this.dateLabel.Text = SK.Text("FactionInvites_Last_Post_Date", "Last Post Date");
            this.dateLabel.Color = ARGBColors.Black;
            this.dateLabel.Position = new Point(0x22a, -2);
            this.dateLabel.Size = new Size(160, this.headerLabelsImage.Height);
            this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.dateLabel);
            this.wallScrollArea.Position = new Point(0x19, 0x26);
            this.wallScrollArea.Size = new Size(0x393, height - 80);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, ((height - 50) - 90) + 60));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x3af, 0x26);
            this.wallScrollBar.Size = new Size(0x18, height - 80);
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            if (resized)
            {
                this.initForum();
            }
            else
            {
                bool flag = false;
                foreach (ForumData data in this.forumArray)
                {
                    if ((data.areaID == this.selectedAreaID) && (data.areaType == this.selectedAreaType))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    RemoteServices.Instance.set_GetForumList_UserCallBack(new RemoteServices.GetForumList_UserCallBack(this.getForumListCallback));
                    RemoteServices.Instance.GetForumList(this.selectedAreaID, this.selectedAreaType);
                }
                else
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastRefreshTime);
                    if (span.TotalMinutes > 5.0)
                    {
                        RemoteServices.Instance.set_GetForumList_UserCallBack(new RemoteServices.GetForumList_UserCallBack(this.getForumListCallback));
                        RemoteServices.Instance.GetForumList(this.selectedAreaID, this.selectedAreaType);
                    }
                    this.initForum();
                }
            }
        }

        public void initForum()
        {
            List<ForumData> list = new List<ForumData>();
            foreach (ForumData data in this.forumArray)
            {
                if ((data.areaID == this.selectedAreaID) && (data.areaType == this.selectedAreaType))
                {
                    list.Add(data);
                }
            }
            list.Sort(this.forumComparer);
            bool flag = false;
            foreach (ForumData data2 in list)
            {
                if (data2.forumID == this.currentlySelectedForum)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                if (list.Count > 0)
                {
                    this.currentlySelectedForum = list[0].forumID;
                }
                else
                {
                    this.currentlySelectedForum = -1L;
                }
            }
            if (this.currentlySelectedForum >= 0L)
            {
                RemoteServices.Instance.set_GetForumThreadList_UserCallBack(new RemoteServices.GetForumThreadList_UserCallBack(this.forumThreadListCallback));
                if ((this.forumThreadArray[this.currentlySelectedForum] == null) || (((ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime.Year < 0x76c))
                {
                    RemoteServices.Instance.GetForumThreadList(this.currentlySelectedForum, DateTime.MinValue, true);
                }
                else
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - ((ForumInfoData) this.forumThreadArray[this.currentlySelectedForum]).lastTime);
                    if (span.TotalMinutes > 1.0)
                    {
                        ForumInfoData data3 = (ForumInfoData) this.forumThreadArray[this.currentlySelectedForum];
                        RemoteServices.Instance.GetForumThreadList(this.currentlySelectedForum, data3.lastTime, false);
                    }
                }
            }
            this.updateForum();
            this.mainBackgroundImage.invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CapitalForumPanel";
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
        }

        private void mouseWheelMoved(int delta)
        {
            if (this.wallScrollBar.Visible)
            {
                if (delta < 0)
                {
                    this.wallScrollBar.scrollDown(40);
                }
                else if (delta > 0)
                {
                    this.wallScrollBar.scrollUp(40);
                }
            }
        }

        public void newForumThreadCallback(NewForumThread_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (((returnData.forumThreadInfo != null) && (returnData.forumThreadInfo.Count > 0)) && RemoteServices.Instance.UserOptions.profanityFilter)
                {
                    foreach (ForumThreadInfo info in returnData.forumThreadInfo)
                    {
                        info.threadTitle = GameEngine.Instance.censorString(info.threadTitle);
                    }
                }
                this.importThreadList(returnData.forumThreadInfo, returnData.forumID);
            }
        }

        public void newTopic(long forumID, string heading, string body)
        {
            RemoteServices.Instance.set_NewForumThread_UserCallBack(new RemoteServices.NewForumThread_UserCallBack(this.newForumThreadCallback));
            RemoteServices.Instance.NewForumThread(forumID, heading, body);
        }

        private void newTopicClick()
        {
            if ((this.m_popup == null) || !this.m_popup.Created)
            {
                this.m_popup = new FactionNewTopicPopup();
                this.m_popup.init(this.currentlySelectedForum, this);
                this.m_popup.Show();
            }
        }

        public void setArea(int parishID, int areaType)
        {
            this.selectedAreaID = parishID;
            this.selectedAreaType = areaType;
        }

        private void swipeLeft()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabLeft();
        }

        private void swiperight()
        {
            InterfaceMgr.Instance.getVillageTabBar().changeTabRight();
        }

        public void update()
        {
        }

        public void updateForum()
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            if (this.currentlySelectedForum >= 0L)
            {
                ForumInfoData data = (ForumInfoData) this.forumThreadArray[this.currentlySelectedForum];
                if ((data != null) && (data.forumThreads != null))
                {
                    int position = 0;
                    foreach (ForumThreadData data2 in data.forumThreads)
                    {
                        ForumThreadLine control = new ForumThreadLine();
                        if (y != 0)
                        {
                            y += 5;
                        }
                        control.Position = new Point(0, y);
                        control.init(data2, position, this);
                        this.wallScrollArea.addControl(control);
                        y += control.Height;
                        this.lineList.Add(control);
                        position++;
                    }
                }
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
            this.mainBackgroundImage.invalidate();
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x26 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class ForumComparer : IComparer<CapitalForumPanel.ForumData>
        {
            public int Compare(CapitalForumPanel.ForumData x, CapitalForumPanel.ForumData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.forumID > y.forumID)
                {
                    return 1;
                }
                if (x.forumID < y.forumID)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class ForumData
        {
            public int areaID = -1;
            public int areaType = -1;
            public long forumID = -1L;
            public string forumTitle = "";
            public DateTime lastTime = DateTime.MinValue;
            public int numPosts;
            public int numReadPosts;
        }

        public class ForumInfoData
        {
            public long forumID = -1L;
            public List<CapitalForumPanel.ForumThreadData> forumThreads = new List<CapitalForumPanel.ForumThreadData>();
            public DateTime lastTime = DateTime.MinValue;
        }

        public class ForumThreadData
        {
            public DateTime lastTime = DateTime.MinValue;
            public bool read;
            public long threadID = -1L;
            public string title = "";
            public string userName = "";
        }

        public class ForumThreadLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
            private MyMessageBoxPopUp deletePostPopUp;
            private CustomSelfDrawPanel.CSDButton deleteThread = new CustomSelfDrawPanel.CSDButton();
            private CapitalForumPanel.ForumThreadData m_ForumThreadData;
            private CapitalForumPanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDLabel threadTitleLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel userLabel = new CustomSelfDrawPanel.CSDLabel();

            private void ClosePopUp()
            {
                if (this.deletePostPopUp != null)
                {
                    if (this.deletePostPopUp.Created)
                    {
                        this.deletePostPopUp.Close();
                    }
                    InterfaceMgr.Instance.closeGreyOut();
                    this.deletePostPopUp = null;
                }
            }

            private void deleteClicked()
            {
                this.ClosePopUp();
                InterfaceMgr.Instance.openGreyOutWindow(false);
                this.deletePostPopUp = new MyMessageBoxPopUp();
                this.deletePostPopUp.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Topic", "Delete This Topic"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
                this.deletePostPopUp.Show(InterfaceMgr.Instance.getGreyOutWindow());
            }

            public void init(CapitalForumPanel.ForumThreadData threadData, int position, CapitalForumPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_ForumThreadData = threadData;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(0, 0);
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                base.addControl(this.backgroundImage);
                this.Size = new Size(900, this.backgroundImage.Size.Height);
                this.ClipVisible = true;
                FontStyle regular = FontStyle.Regular;
                if (!threadData.read)
                {
                    regular = FontStyle.Bold;
                }
                this.threadTitleLabel.Text = threadData.title;
                this.threadTitleLabel.Color = ARGBColors.Black;
                this.threadTitleLabel.Position = new Point(8, 0);
                this.threadTitleLabel.Size = new Size(410, this.backgroundImage.Height);
                this.threadTitleLabel.Font = FontManager.GetFont("Arial", 9f, regular);
                this.threadTitleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.threadTitleLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.addControl(this.threadTitleLabel);
                this.userLabel.Text = threadData.userName;
                this.userLabel.Color = ARGBColors.Black;
                this.userLabel.Position = new Point(420, 0);
                this.userLabel.Size = new Size(0x86, this.backgroundImage.Height);
                this.userLabel.Font = FontManager.GetFont("Arial", 9f, regular);
                this.userLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.userLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.addControl(this.userLabel);
                this.dateLabel.Text = threadData.lastTime.ToShortTimeString() + " " + threadData.lastTime.ToShortDateString();
                this.dateLabel.Color = ARGBColors.Black;
                this.dateLabel.Position = new Point(0x22a, 0);
                this.dateLabel.Size = new Size(0xab, this.backgroundImage.Height);
                this.dateLabel.Font = FontManager.GetFont("Arial", 9f, regular);
                this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.dateLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.addControl(this.dateLabel);
                base.invalidate();
                if (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator)
                {
                    this.deleteThread.ImageNorm = (Image) GFXLibrary.trashcan_normal;
                    this.deleteThread.ImageOver = (Image) GFXLibrary.trashcan_over;
                    this.deleteThread.ImageClick = (Image) GFXLibrary.trashcan_clicked;
                    this.deleteThread.Position = new Point(870, 4);
                    this.deleteThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "CapitalForumPanel_delete");
                    this.backgroundImage.addControl(this.deleteThread);
                }
            }

            public void lineClicked()
            {
                GameEngine.Instance.playInterfaceSound("CapitalForumPanel_thread");
                InterfaceMgr.Instance.showCapitalForumPosts(this.m_ForumThreadData.threadID, this.m_parent.currentlySelectedForum, this.m_ForumThreadData.title, this.m_parent.selectedAreaID, this.m_parent.selectedAreaType, this.m_parent.titleForum);
            }

            private void PopUpOkClick()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.deleteTopic(this.m_ForumThreadData.threadID);
                }
                InterfaceMgr.Instance.closeGreyOut();
                this.deletePostPopUp.Close();
            }

            public void update()
            {
            }
        }

        public class ThreadComparer : IComparer<CapitalForumPanel.ForumThreadData>
        {
            public int Compare(CapitalForumPanel.ForumThreadData x, CapitalForumPanel.ForumThreadData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.lastTime < y.lastTime)
                {
                    return 1;
                }
                if (x.lastTime > y.lastTime)
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}

