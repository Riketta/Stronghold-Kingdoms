namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class CapitalForumPostsPanel : CustomSelfDrawPanel, IDockableControl, IForumReplyParent
    {
        public static int areaID = -1;
        public static int areaType = -1;
        private CustomSelfDrawPanel.CSDButton backButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
        public static string ForumTitle = "";
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static CapitalForumPostsPanel instance = null;
        private List<FactionsPostLine> lineList = new List<FactionsPostLine>();
        private FactionNewPostPopup m_popup;
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDButton newPostButton = new CustomSelfDrawPanel.CSDButton();
        public const int PANEL_ID = 0x30;
        public static long parentForumID = -1L;
        private SparseArray threadArray = new SparseArray();
        public static long ThreadID = -1L;
        public static string ThreadTitle = "";
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public CapitalForumPostsPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addPosts()
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            int position = 0;
            this.lineList.Clear();
            ForumThreadInfoData data = (ForumThreadInfoData) this.threadArray[ThreadID];
            if (data != null)
            {
                foreach (ForumPostData data2 in data.forumPosts)
                {
                    if (y != 0)
                    {
                        y += 5;
                    }
                    FactionsPostLine control = new FactionsPostLine {
                        Position = new Point(0, y)
                    };
                    control.init(data2, position, this, 0);
                    this.wallScrollArea.addControl(control);
                    y += control.Height;
                    this.lineList.Add(control);
                    position++;
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
            this.update();
            base.Invalidate();
        }

        private void backClick()
        {
            switch (areaType)
            {
                case 0:
                    InterfaceMgr.Instance.setVillageTabSubMode(0x51b, false);
                    return;

                case 1:
                    InterfaceMgr.Instance.setVillageTabSubMode(0x4b7, false);
                    return;

                case 2:
                    InterfaceMgr.Instance.setVillageTabSubMode(0x453, false);
                    return;

                case 3:
                    InterfaceMgr.Instance.setVillageTabSubMode(0x3ef, false);
                    return;
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

        public void deleteForumPostCallback(DeleteForumPost_ReturnType returnData)
        {
            if (returnData.Success && (returnData.postID >= 0L))
            {
                ForumThreadInfoData data = (ForumThreadInfoData) this.threadArray[ThreadID];
                if (data != null)
                {
                    foreach (ForumPostData data2 in data.forumPosts)
                    {
                        if (data2.postID == returnData.postID)
                        {
                            data.forumPosts.Remove(data2);
                            this.addPosts();
                            break;
                        }
                    }
                }
            }
        }

        public void deletePost(long postID)
        {
            RemoteServices.Instance.set_DeleteForumPost_UserCallBack(new RemoteServices.DeleteForumPost_UserCallBack(this.deleteForumPostCallback));
            RemoteServices.Instance.DeleteForumPost(areaID, areaType, ThreadTitle, parentForumID, ThreadID, postID);
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

        public void forumThreadCallback(GetForumThread_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (((returnData.forumPosts != null) && (returnData.forumPosts.Count > 0)) && RemoteServices.Instance.UserOptions.profanityFilter)
                {
                    foreach (ForumPost post in returnData.forumPosts)
                    {
                        post.postText = GameEngine.Instance.censorString(post.postText);
                    }
                }
                this.importThread(returnData.forumPosts, returnData.threadID);
            }
        }

        public void importThread(List<ForumPost> posts, long threadID)
        {
            if (posts != null)
            {
                if (this.threadArray[threadID] == null)
                {
                    ForumThreadInfoData data = new ForumThreadInfoData {
                        threadID = threadID
                    };
                    this.threadArray[threadID] = data;
                }
                ForumThreadInfoData data2 = (ForumThreadInfoData) this.threadArray[threadID];
                data2.forumPosts = new List<ForumPostData>();
                foreach (ForumPost post in posts)
                {
                    ForumPostData item = new ForumPostData {
                        text = post.postText,
                        postID = post.postID,
                        postTime = post.date,
                        userName = post.userName,
                        userID = post.userID
                    };
                    if (item.postTime > data2.lastTime)
                    {
                        data2.lastTime = item.postTime;
                    }
                    data2.forumPosts.Add(item);
                }
                this.addPosts();
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
            this.factionLabel.Text = ThreadTitle;
            this.factionLabel.Color = ARGBColors.Black;
            this.factionLabel.Position = new Point(9, -2);
            this.factionLabel.Size = new Size(this.headerLabelsImage.Width - 0x12, this.headerLabelsImage.Height);
            this.factionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.factionLabel);
            InterfaceMgr.Instance.setVillageHeading(ForumTitle);
            this.wallScrollArea.Position = new Point(0x19, 0x26);
            this.wallScrollArea.Size = new Size(0x393, height - 70);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (height - 50) - 20));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x3af, 0x26);
            this.wallScrollBar.Size = new Size(0x18, height - 70);
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.newPostButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.newPostButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.newPostButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.newPostButton.Position = new Point(20, height - 30);
            this.newPostButton.Text.Text = SK.Text("FORUMS_New_Post", "New Post");
            this.newPostButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.newPostButton.TextYOffset = -3;
            this.newPostButton.Text.Color = ARGBColors.Black;
            this.newPostButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.newPostClick), "CapitalForumPostsPanel_new_post");
            this.mainBackgroundImage.addControl(this.newPostButton);
            this.backButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.backButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.backButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.backButton.Position = new Point(840, height - 30);
            this.backButton.Text.Text = SK.Text("FORUMS_Back", "Back");
            this.backButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.backButton.TextYOffset = -3;
            this.backButton.Text.Color = ARGBColors.Black;
            this.backButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.backClick), "CapitalForumPostsPanel_back");
            this.mainBackgroundImage.addControl(this.backButton);
            if (!resized)
            {
                RemoteServices.Instance.set_GetForumThread_UserCallBack(new RemoteServices.GetForumThread_UserCallBack(this.forumThreadCallback));
                if ((this.threadArray[ThreadID] == null) || (((ForumThreadInfoData) this.threadArray[ThreadID]).lastTime.Year < 0x76c))
                {
                    RemoteServices.Instance.GetForumThread(parentForumID, ThreadID, DateTime.MinValue, true);
                }
                else
                {
                    ForumThreadInfoData data = (ForumThreadInfoData) this.threadArray[ThreadID];
                    RemoteServices.Instance.GetForumThread(parentForumID, ThreadID, data.lastTime, false);
                }
            }
            this.addPosts();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CapitalForumPostsPanel";
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
            this.threadArray = new SparseArray();
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

        public void newPost(long threadID, string body)
        {
            RemoteServices.Instance.set_PostToForumThread_UserCallBack(new RemoteServices.PostToForumThread_UserCallBack(this.postToForumThreadCallback));
            RemoteServices.Instance.PostToForumThread(threadID, parentForumID, body);
        }

        private void newPostClick()
        {
            if ((this.m_popup == null) || !this.m_popup.Created)
            {
                this.m_popup = new FactionNewPostPopup();
                this.m_popup.init(ThreadID, this, ThreadTitle);
                this.m_popup.Show();
            }
        }

        public void postToForumThreadCallback(PostToForumThread_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (((returnData.forumPosts != null) && (returnData.forumPosts.Count > 0)) && RemoteServices.Instance.UserOptions.profanityFilter)
                {
                    foreach (ForumPost post in returnData.forumPosts)
                    {
                        post.postText = GameEngine.Instance.censorString(post.postText);
                    }
                }
                this.importThread(returnData.forumPosts, returnData.threadID);
            }
        }

        public void update()
        {
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x26 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class FactionsPostLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton deleteThread = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDExtendingPanel lightArea1 = new CustomSelfDrawPanel.CSDExtendingPanel();
            private CapitalForumPostsPanel m_parent;
            private int m_position = -1000;
            private CapitalForumPostsPanel.ForumPostData m_postData;
            private MyMessageBoxPopUp PopUpRef;
            private CustomSelfDrawPanel.CSDLabel userName = new CustomSelfDrawPanel.CSDLabel();

            public void clickedLine()
            {
            }

            private void copyTextToClipboardClick()
            {
                Clipboard.SetText(this.m_postData.text);
            }

            private void deleteClicked()
            {
                MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                if (MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("FORUMS_Delete_Post", "Delete This Post"), yesNo) == DialogResult.Yes)
                {
                    this.PopUpOkClick();
                }
            }

            public void init(CapitalForumPostsPanel.ForumPostData postData, int position, CapitalForumPostsPanel parent, int yourRank)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_postData = postData;
                Graphics graphics = parent.CreateGraphics();
                Size size = graphics.MeasureString(postData.text, FontManager.GetFont("Arial", 9f, FontStyle.Regular), 840).ToSize();
                graphics.Dispose();
                int height = size.Height + 10;
                if (height < 0x20)
                {
                    height = 0x20;
                }
                this.clearControls();
                this.Size = new Size(890, 0x19 + height);
                this.ClipVisible = true;
                this.lightArea1.Size = new Size(850, height);
                this.lightArea1.Position = new Point(40, 0x19);
                base.addControl(this.lightArea1);
                this.lightArea1.Create((Image) GFXLibrary.int_insetpanel_lighten_top_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_top_right, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_left, (Image) GFXLibrary.int_insetpanel_lighten_middle, (Image) GFXLibrary.int_insetpanel_lighten_bottom_right);
                NumberFormatInfo nFI = GameEngine.NFI;
                this.userName.Text = postData.userName;
                this.userName.Color = ARGBColors.Black;
                this.userName.Position = new Point(9, 3);
                this.userName.Size = new Size(280, 30);
                this.userName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.userName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                base.addControl(this.userName);
                this.dateLabel.Text = postData.postTime.ToShortTimeString() + " " + postData.postTime.ToShortDateString();
                this.dateLabel.Color = ARGBColors.Black;
                this.dateLabel.Position = new Point(0x2e8, 3);
                this.dateLabel.Size = new Size(0xa1, 30);
                this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                base.addControl(this.dateLabel);
                this.bodyLabel.Text = postData.text;
                this.bodyLabel.Color = ARGBColors.Black;
                this.bodyLabel.Position = new Point(5, 5);
                this.bodyLabel.Size = new Size(this.lightArea1.Width - 10, height - 5);
                this.bodyLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.bodyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.bodyLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
                this.lightArea1.addControl(this.bodyLabel);
                if (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator)
                {
                    this.deleteThread.ImageNorm = (Image) GFXLibrary.trashcan_normal;
                    this.deleteThread.ImageOver = (Image) GFXLibrary.trashcan_over;
                    this.deleteThread.ImageClick = (Image) GFXLibrary.trashcan_clicked;
                    this.deleteThread.Position = new Point(870, 2);
                    this.deleteThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClicked), "CapitalForumPostsPanel_delete");
                    base.addControl(this.deleteThread);
                }
                base.invalidate();
            }

            private void PopUpOkClick()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.deletePost(this.m_postData.postID);
                }
            }

            public void update()
            {
            }
        }

        public class ForumPostData
        {
            public long postID = -1L;
            public DateTime postTime = DateTime.MinValue;
            public string text = "";
            public int userID = -1;
            public string userName = "";
        }

        public class ForumThreadInfoData
        {
            public List<CapitalForumPostsPanel.ForumPostData> forumPosts = new List<CapitalForumPostsPanel.ForumPostData>();
            public DateTime lastTime = DateTime.MinValue;
            public long threadID = -1L;
        }
    }
}

