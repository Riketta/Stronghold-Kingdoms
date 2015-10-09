namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FactionNewForumPanel : CustomSelfDrawPanel, IDockableControl, IForumPostParent
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton createForumButton = new CustomSelfDrawPanel.CSDButton();
        private long currentlySelectedForum = -1L;
        private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton deleteForumButton = new CustomSelfDrawPanel.CSDButton();
        private MyMessageBoxPopUp deletePostPopUp;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton forum1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton forum2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton forum3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton forum4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton forum5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton forum6Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton forum7Button = new CustomSelfDrawPanel.CSDButton();
        private SparseArray forumArray = new SparseArray();
        public ForumComparer forumComparer = new ForumComparer();
        private int[] forums1Positions = new int[] { 0x146, 0x18 };
        private int[] forums2Positions = new int[] { 0xfb, 0x18, 0x192, 0x18 };
        private int[] forums3Positions = new int[] { 0xaf, 0x18, 0x146, 0x18, 0x1dd, 0x18 };
        private int[] forums4Positions = new int[] { 100, 0x18, 0xfb, 0x18, 0x192, 0x18, 0x229, 0x18 };
        private int[] forums5Positions = new int[] { 0xaf, 7, 0x146, 7, 0x1dd, 7, 0xfb, 40, 0x192, 40 };
        private int[] forums6Positions = new int[] { 0xaf, 7, 0x146, 7, 0x1dd, 7, 0xaf, 40, 0x146, 40, 0x1dd, 40 };
        private int[] forums7Positions = new int[] { 100, 7, 0xfb, 7, 0x192, 7, 0x229, 7, 0xaf, 40, 0x146, 40, 0x1dd, 40 };
        private SparseArray forumThreadArray = new SparseArray();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static FactionNewForumPanel instance;
        private DateTime lastRefreshTime = DateTime.MinValue;
        private List<ForumThreadLine> lineList = new List<ForumThreadLine>();
        private FactionNewForumPopup m_forumPopup;
        private FactionNewTopicPopup m_popup;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDButton newTopicButton = new CustomSelfDrawPanel.CSDButton();
        private string OrigForumName = "";
        public const int PANEL_ID = 0x2d;
        private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
        private int selectedAreaID = -1;
        private int selectedAreaType = -1;
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private ThreadComparer threadComparer = new ThreadComparer();
        private CustomSelfDrawPanel.CSDLabel threadTitleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public FactionNewForumPanel()
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

        public void closing()
        {
            InterfaceMgr.Instance.closeDonatePopup();
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void createForumCallback(CreateForum_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.forumInfo != null)
                {
                    ForumData data = new ForumData {
                        areaID = returnData.forumInfo.areaID,
                        areaType = returnData.forumInfo.areaType,
                        forumID = returnData.forumInfo.forumID,
                        forumTitle = returnData.forumInfo.forumTitle,
                        lastTime = returnData.forumInfo.lastDate,
                        numPosts = returnData.forumInfo.numPosts,
                        numReadPosts = returnData.forumInfo.numReadPosts
                    };
                    this.forumArray[data.forumID] = data;
                }
                this.initForum();
            }
        }

        private void createForumClick()
        {
            if ((this.m_forumPopup == null) || !this.m_forumPopup.Created)
            {
                this.m_forumPopup = new FactionNewForumPopup();
                this.m_forumPopup.init(this);
                this.m_forumPopup.Show();
                this.m_forumPopup.setFocus();
            }
        }

        public void createNewForum(string forumName)
        {
            RemoteServices.Instance.set_CreateForum_UserCallBack(new RemoteServices.CreateForum_UserCallBack(this.createForumCallback));
            RemoteServices.Instance.CreateForum(this.selectedAreaID, this.selectedAreaType, forumName);
        }

        public void deleteForumCallback(DeleteForum_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.forumID >= 0L)
                {
                    this.forumArray[returnData.forumID] = null;
                }
                this.currentlySelectedForum = -1L;
                this.initForum();
            }
        }

        private void deleteForumClicked()
        {
            this.ClosePopUp();
            InterfaceMgr.Instance.openGreyOutWindow(false);
            this.deletePostPopUp = new MyMessageBoxPopUp();
            this.deletePostPopUp.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Sub_Forum", "Delete Sub Forum"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
            this.deletePostPopUp.Show(InterfaceMgr.Instance.getGreyOutWindow());
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
            RemoteServices.Instance.DeleteForumThread(this.selectedAreaID, 5, this.OrigForumName, this.currentlySelectedForum, threadID);
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
            this.sidebar.addSideBar(6, this);
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 0x45);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(0x19f, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(0x225, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsPanel_Faction_Forum", "Faction Forum"));
            this.newTopicButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.newTopicButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.newTopicButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.newTopicButton.Position = new Point(20, height - 30);
            this.newTopicButton.Text.Text = SK.Text("FORUMS_New_Topic", "New Topic");
            this.newTopicButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.newTopicButton.TextYOffset = -3;
            this.newTopicButton.Text.Color = ARGBColors.Black;
            this.newTopicButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.newTopicClick), "FactionNewForumPanel_new_topic");
            this.mainBackgroundImage.addControl(this.newTopicButton);
            this.createForumButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.createForumButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.createForumButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.createForumButton.Position = new Point(330, height - 30);
            this.createForumButton.Text.Text = SK.Text("FORUMS_Create_New_Sub_Forum", "Create New Sub Forum");
            this.createForumButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.createForumButton.TextYOffset = -3;
            this.createForumButton.Text.Color = ARGBColors.Black;
            this.createForumButton.Visible = false;
            this.createForumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.createForumClick), "FactionNewForumPanel_create_forum");
            this.mainBackgroundImage.addControl(this.createForumButton);
            this.deleteForumButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.deleteForumButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.deleteForumButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.deleteForumButton.Position = new Point(560, height - 30);
            this.deleteForumButton.Text.Text = SK.Text("FORUMS_Delete_Sub_Forum", "Delete Sub Forum");
            this.deleteForumButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.deleteForumButton.TextYOffset = -3;
            this.deleteForumButton.Text.Color = ARGBColors.Black;
            this.deleteForumButton.Visible = false;
            this.deleteForumButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteForumClicked), "FactionNewForumPanel_delete_forum");
            this.mainBackgroundImage.addControl(this.deleteForumButton);
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
            this.wallScrollArea.Position = new Point(0x19, 0x62);
            this.wallScrollArea.Size = new Size(0x2c1, height - 140);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, (height - 50) - 90));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x2dd, 0x62);
            this.wallScrollBar.Size = new Size(0x18, height - 140);
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
                this.selectedAreaID = RemoteServices.Instance.UserFactionID;
                this.selectedAreaType = 5;
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
            this.mainBackgroundImage.removeControl(this.forum1Button);
            this.mainBackgroundImage.removeControl(this.forum2Button);
            this.mainBackgroundImage.removeControl(this.forum3Button);
            this.mainBackgroundImage.removeControl(this.forum4Button);
            this.mainBackgroundImage.removeControl(this.forum5Button);
            this.mainBackgroundImage.removeControl(this.forum6Button);
            this.mainBackgroundImage.removeControl(this.forum7Button);
            List<ForumData> list = new List<ForumData>();
            foreach (ForumData data in this.forumArray)
            {
                if ((data.areaID == RemoteServices.Instance.UserFactionID) && (data.areaType == 5))
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
            int count = list.Count;
            int[] numArray = null;
            switch (count)
            {
                case 1:
                    numArray = this.forums1Positions;
                    break;

                case 2:
                    numArray = this.forums2Positions;
                    break;

                case 3:
                    numArray = this.forums3Positions;
                    break;

                case 4:
                    numArray = this.forums4Positions;
                    break;

                case 5:
                    numArray = this.forums5Positions;
                    break;

                case 6:
                    numArray = this.forums6Positions;
                    break;

                case 7:
                    numArray = this.forums7Positions;
                    break;
            }
            if (count >= 1)
            {
                this.forum1Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.forum1Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.forum1Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.forum1Button.Position = new Point(numArray[0], numArray[1]);
                this.forum1Button.Text.Text = SK.Text("FORUMS_General", "General");
                this.forum1Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.forum1Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.forum1Button.TextYOffset = -3;
                this.forum1Button.Text.Color = ARGBColors.Black;
                this.forum1Button.Text.clearDropShadow();
                this.forum1Button.DataL = list[0].forumID;
                this.forum1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
                this.mainBackgroundImage.addControl(this.forum1Button);
                this.forum1Button.Active = true;
                if (this.forum1Button.DataL == this.currentlySelectedForum)
                {
                    this.forum1Button.Active = false;
                    this.forum1Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum1Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum1Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum1Button.Text.Text);
                    this.forum1Button.Text.Color = ARGBColors.White;
                    this.forum1Button.Text.DropShadowColor = ARGBColors.Black;
                    this.OrigForumName = list[0].forumTitle;
                }
            }
            if (count >= 2)
            {
                this.forum2Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.forum2Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.forum2Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.forum2Button.Position = new Point(numArray[2], numArray[3]);
                this.forum2Button.Text.Text = list[1].forumTitle;
                this.forum2Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.forum2Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.forum2Button.TextYOffset = -3;
                this.forum2Button.Text.Color = ARGBColors.Black;
                this.forum2Button.Text.clearDropShadow();
                this.forum2Button.DataL = list[1].forumID;
                this.forum2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
                this.mainBackgroundImage.addControl(this.forum2Button);
                if (this.forum2Button.DataL == this.currentlySelectedForum)
                {
                    this.forum2Button.Active = false;
                    this.forum2Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum2Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum2Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum2Button.Text.Text);
                    this.forum2Button.Text.Color = ARGBColors.White;
                    this.forum2Button.Text.DropShadowColor = ARGBColors.Black;
                    this.OrigForumName = list[1].forumTitle;
                }
            }
            if (count >= 3)
            {
                this.forum3Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.forum3Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.forum3Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.forum3Button.Position = new Point(numArray[4], numArray[5]);
                this.forum3Button.Text.Text = list[2].forumTitle;
                this.forum3Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.forum3Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.forum3Button.TextYOffset = -3;
                this.forum3Button.Text.Color = ARGBColors.Black;
                this.forum3Button.Text.clearDropShadow();
                this.forum3Button.DataL = list[2].forumID;
                this.forum3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
                this.mainBackgroundImage.addControl(this.forum3Button);
                if (this.forum3Button.DataL == this.currentlySelectedForum)
                {
                    this.forum3Button.Active = false;
                    this.forum3Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum3Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum3Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum3Button.Text.Text);
                    this.forum3Button.Text.Color = ARGBColors.White;
                    this.forum3Button.Text.DropShadowColor = ARGBColors.Black;
                    this.OrigForumName = list[2].forumTitle;
                }
            }
            if (count >= 4)
            {
                this.forum4Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.forum4Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.forum4Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.forum4Button.Position = new Point(numArray[6], numArray[7]);
                this.forum4Button.Text.Text = list[3].forumTitle;
                this.forum4Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.forum4Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.forum4Button.TextYOffset = -3;
                this.forum4Button.Text.Color = ARGBColors.Black;
                this.forum4Button.Text.clearDropShadow();
                this.forum4Button.DataL = list[3].forumID;
                this.forum4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
                this.mainBackgroundImage.addControl(this.forum4Button);
                if (this.forum4Button.DataL == this.currentlySelectedForum)
                {
                    this.forum4Button.Active = false;
                    this.forum4Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum4Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum4Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum4Button.Text.Text);
                    this.forum4Button.Text.Color = ARGBColors.White;
                    this.forum4Button.Text.DropShadowColor = ARGBColors.Black;
                    this.OrigForumName = list[3].forumTitle;
                }
            }
            if (count >= 5)
            {
                this.forum5Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.forum5Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.forum5Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.forum5Button.Position = new Point(numArray[8], numArray[9]);
                this.forum5Button.Text.Text = list[4].forumTitle;
                this.forum5Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.forum5Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.forum5Button.TextYOffset = -3;
                this.forum5Button.Text.Color = ARGBColors.Black;
                this.forum5Button.Text.clearDropShadow();
                this.forum5Button.DataL = list[4].forumID;
                this.forum5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
                this.mainBackgroundImage.addControl(this.forum5Button);
                if (this.forum5Button.DataL == this.currentlySelectedForum)
                {
                    this.forum5Button.Active = false;
                    this.forum5Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum5Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum5Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum5Button.Text.Text);
                    this.forum5Button.Text.Color = ARGBColors.White;
                    this.forum5Button.Text.DropShadowColor = ARGBColors.Black;
                    this.OrigForumName = list[4].forumTitle;
                }
            }
            if (count >= 6)
            {
                this.forum6Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.forum6Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.forum6Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.forum6Button.Position = new Point(numArray[10], numArray[11]);
                this.forum6Button.Text.Text = list[5].forumTitle;
                this.forum6Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.forum6Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.forum6Button.TextYOffset = -3;
                this.forum6Button.Text.Color = ARGBColors.Black;
                this.forum6Button.Text.clearDropShadow();
                this.forum6Button.DataL = list[5].forumID;
                this.forum6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
                this.mainBackgroundImage.addControl(this.forum6Button);
                if (this.forum6Button.DataL == this.currentlySelectedForum)
                {
                    this.forum6Button.Active = false;
                    this.forum6Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum6Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum6Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum6Button.Text.Text);
                    this.forum6Button.Text.Color = ARGBColors.White;
                    this.forum6Button.Text.DropShadowColor = ARGBColors.Black;
                    this.OrigForumName = list[5].forumTitle;
                }
            }
            if (count >= 7)
            {
                this.forum7Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.forum7Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.forum7Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.forum7Button.Position = new Point(numArray[12], numArray[13]);
                this.forum7Button.Text.Text = list[6].forumTitle;
                this.forum7Button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.forum7Button.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.forum7Button.TextYOffset = -3;
                this.forum7Button.Text.Color = ARGBColors.Black;
                this.forum7Button.Text.clearDropShadow();
                this.forum7Button.DataL = list[6].forumID;
                this.forum7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forumSelectedClick), "FactionNewForumPanel_change_forum");
                this.mainBackgroundImage.addControl(this.forum7Button);
                if (this.forum7Button.DataL == this.currentlySelectedForum)
                {
                    this.forum7Button.Active = false;
                    this.forum7Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum7Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.forum7Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionsSidebar_Forum", "Forum") + " : " + this.forum7Button.Text.Text);
                    this.forum7Button.Text.Color = ARGBColors.White;
                    this.forum7Button.Text.DropShadowColor = ARGBColors.Black;
                    this.OrigForumName = list[6].forumTitle;
                }
            }
            if (this.currentlySelectedForum >= 0L)
            {
                switch (GameEngine.Instance.World.getYourFactionRank())
                {
                    case 1:
                    case 2:
                        if (count < (GameEngine.Instance.LocalWorldData.Faction_MaxUserForums + 1))
                        {
                            this.createForumButton.Visible = true;
                        }
                        else
                        {
                            this.createForumButton.Visible = false;
                        }
                        if (this.forum1Button.DataL != this.currentlySelectedForum)
                        {
                            this.deleteForumButton.Visible = true;
                        }
                        else
                        {
                            this.deleteForumButton.Visible = false;
                        }
                        break;

                    default:
                        this.createForumButton.Visible = false;
                        this.deleteForumButton.Visible = false;
                        break;
                }
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
            base.Name = "FactionNewForumPanel";
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

        private void PopUpOkClick()
        {
            RemoteServices.Instance.set_DeleteForum_UserCallBack(new RemoteServices.DeleteForum_UserCallBack(this.deleteForumCallback));
            RemoteServices.Instance.DeleteForum(this.selectedAreaID, 5, this.currentlySelectedForum);
            InterfaceMgr.Instance.closeGreyOut();
            this.deletePostPopUp.Close();
        }

        public void update()
        {
            this.sidebar.update();
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
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x62 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class ForumComparer : IComparer<FactionNewForumPanel.ForumData>
        {
            public int Compare(FactionNewForumPanel.ForumData x, FactionNewForumPanel.ForumData y)
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
            public List<FactionNewForumPanel.ForumThreadData> forumThreads = new List<FactionNewForumPanel.ForumThreadData>();
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
            private CustomSelfDrawPanel.CSDButton deleteThread = new CustomSelfDrawPanel.CSDButton();
            private FactionNewForumPanel.ForumThreadData m_ForumThreadData;
            private FactionNewForumPanel m_parent;
            private int m_position = -1000;
            private MyMessageBoxPopUp PopUp;
            private CustomSelfDrawPanel.CSDLabel threadTitleLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel userLabel = new CustomSelfDrawPanel.CSDLabel();

            private void ClosePopUp()
            {
                if (this.PopUp != null)
                {
                    if (this.PopUp.Created)
                    {
                        this.PopUp.Close();
                    }
                    InterfaceMgr.Instance.closeGreyOut();
                    this.PopUp = null;
                }
            }

            private void deleteClicked()
            {
                this.ClosePopUp();
                InterfaceMgr.Instance.openGreyOutWindow(false);
                this.PopUp = new MyMessageBoxPopUp();
                this.PopUp.init(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Topic", "Delete This Topic"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
                this.PopUp.Show(InterfaceMgr.Instance.getGreyOutWindow());
            }

            public void init(FactionNewForumPanel.ForumThreadData threadData, int position, FactionNewForumPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_ForumThreadData = threadData;
                this.ClipVisible = true;
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
                this.Size = this.backgroundImage.Size;
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
                int num = GameEngine.Instance.World.getYourFactionRank();
                if (((num == 1) || (num == 2)) || (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator))
                {
                    this.deleteThread.ImageNorm = (Image) GFXLibrary.trashcan_normal;
                    this.deleteThread.ImageOver = (Image) GFXLibrary.trashcan_over;
                    this.deleteThread.ImageClick = (Image) GFXLibrary.trashcan_clicked;
                    this.deleteThread.Position = new Point(680, 4);
                    this.deleteThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "FactionNewForumPanel_delete_thread");
                    this.backgroundImage.addControl(this.deleteThread);
                }
            }

            public void lineClicked()
            {
                GameEngine.Instance.playInterfaceSound("FactionNewForumPanel_thread_clicked");
                InterfaceMgr.Instance.showFactionForumPosts(this.m_ForumThreadData.threadID, this.m_parent.currentlySelectedForum, this.m_ForumThreadData.title, SK.Text("FactionsPanel_Faction_Forum", "Faction Forum"));
            }

            private void PopUpOkClick()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.deleteTopic(this.m_ForumThreadData.threadID);
                }
                InterfaceMgr.Instance.closeGreyOut();
                this.PopUp.Close();
            }

            public void update()
            {
            }
        }

        public class ThreadComparer : IComparer<FactionNewForumPanel.ForumThreadData>
        {
            public int Compare(FactionNewForumPanel.ForumThreadData x, FactionNewForumPanel.ForumThreadData y)
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

