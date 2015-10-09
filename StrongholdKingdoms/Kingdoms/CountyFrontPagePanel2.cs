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

    public class CountyFrontPagePanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDLabel activityLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDButton btnChat = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private SparseArray countyList = new SparseArray();
        private int currentCounty = -1;
        private int currentLeaderID = -1;
        private string currentLeaderName = "";
        private DockableControl dockableControl;
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDLabel goldLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel goldValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static CountyFrontPagePanel2 instance;
        private List<CustomSelfDrawPanel.ParishWallEntry> lineList = new List<CustomSelfDrawPanel.ParishWallEntry>();
        private int m_currentVillage = -1;
        private int m_userIDOnCurrent = -1;
        private DateTime nextElectionTime = DateTime.MinValue;
        private WallInfo[] origWallInfo;
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sheriffLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel sheriffName = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel taxLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel taxValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private List<WallInfo> wallList = new List<WallInfo>();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDExtendingPanel windowImage = new CustomSelfDrawPanel.CSDExtendingPanel();

        public CountyFrontPagePanel2()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        private void chatClick()
        {
            if (this.currentCounty >= 0)
            {
                InterfaceMgr.Instance.initChatPanel(2, this.currentCounty);
            }
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

        public void getCountyFrontPageInfoCallback(GetCountyFrontPageInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                StoredCountyInfo info = (StoredCountyInfo) this.countyList[returnData.countyID];
                if (info == null)
                {
                    info = new StoredCountyInfo();
                    this.countyList[returnData.countyID] = info;
                }
                info.m_lastUpdateTime = DateTime.Now;
                info.lastReturnData = returnData;
                if (this.currentCounty == returnData.countyID)
                {
                    this.m_userIDOnCurrent = -1;
                    this.currentLeaderID = returnData.leaderID;
                    this.currentLeaderName = returnData.leaderName;
                    this.updateLeaderInfo();
                    NumberFormatInfo nFI = GameEngine.NFI;
                    switch (returnData.taxRate)
                    {
                        case 0:
                            this.taxValue.Text = "0";
                            break;

                        case 1:
                            this.taxValue.Text = "x1";
                            break;

                        case 2:
                            this.taxValue.Text = "x2";
                            break;

                        case 3:
                            this.taxValue.Text = "x3";
                            break;

                        case 4:
                            this.taxValue.Text = "x4";
                            break;

                        case 5:
                            this.taxValue.Text = "x5";
                            break;

                        case 6:
                            this.taxValue.Text = "x6";
                            break;

                        case 7:
                            this.taxValue.Text = "x7";
                            break;

                        case 8:
                            this.taxValue.Text = "x8";
                            break;

                        case 9:
                            this.taxValue.Text = "x9";
                            break;
                    }
                    this.goldValue.Text = returnData.gold.ToString("N", nFI);
                    this.createParishWall(returnData.countyWallInfo);
                }
            }
        }

        public void init()
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            int countyID = GameEngine.Instance.World.getCountyFromVillageID(villageID);
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            CustomSelfDrawPanel.WikiLinkControl.init(this.headerImage, 14, new Point(base.Width - 0x2c, 3));
            this.backgroundImage.Size = new Size(base.Width, height - 40);
            this.backgroundImage.Position = new Point(0, 40);
            base.addControl(this.backgroundImage);
            this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            this.parishNameLabel.Text = SK.Text("GENERIC_County", "County") + " : " + GameEngine.Instance.World.getCountyName(countyID) + " (" + GameEngine.Instance.World.getVillageName(villageID) + ")";
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.windowImage.Size = new Size(400, 150);
            this.windowImage.Position = new Point(0x1ed, 130);
            this.backgroundImage.addControl(this.windowImage);
            this.windowImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
            this.sheriffLabel.Text = SK.Text("CountyFrontPagePanel_Current_Sheriff", "Current Sheriff");
            this.sheriffLabel.Position = new Point(30, 0x1a);
            this.sheriffLabel.Size = new Size(250, 40);
            this.sheriffLabel.Color = ARGBColors.Black;
            this.sheriffLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.sheriffLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.windowImage.addControl(this.sheriffLabel);
            this.goldLabel.Text = SK.Text("GENERIC_Current_Gold", "Current Gold");
            this.goldLabel.Position = new Point(30, 0x42);
            this.goldLabel.Size = new Size(250, 40);
            this.goldLabel.Color = ARGBColors.Black;
            this.goldLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.goldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.windowImage.addControl(this.goldLabel);
            this.taxLabel.Text = SK.Text("GENERIC_Tax_Rate", "Tax Rate");
            this.taxLabel.Position = new Point(30, 0x6a);
            this.taxLabel.Size = new Size(250, 40);
            this.taxLabel.Color = ARGBColors.Black;
            this.taxLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.taxLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.windowImage.addControl(this.taxLabel);
            this.sheriffName.Text = "";
            this.sheriffName.Position = new Point(170, 0x1a);
            this.sheriffName.Size = new Size(200, 40);
            this.sheriffName.Color = ARGBColors.Black;
            this.sheriffName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.sheriffName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.windowImage.addControl(this.sheriffName);
            this.goldValue.Text = "";
            this.goldValue.Position = new Point(170, 0x42);
            this.goldValue.Size = new Size(200, 40);
            this.goldValue.Color = ARGBColors.Black;
            this.goldValue.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.goldValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.windowImage.addControl(this.goldValue);
            this.taxValue.Text = "";
            this.taxValue.Position = new Point(170, 0x6a);
            this.taxValue.Size = new Size(200, 40);
            this.taxValue.Color = ARGBColors.Black;
            this.taxValue.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.taxValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.windowImage.addControl(this.taxValue);
            this.activityLabel.Text = SK.Text("WALL_recent_activity", "Recent Activity");
            this.activityLabel.Position = new Point(8, -16);
            this.activityLabel.Size = new Size(0x184, 40);
            this.activityLabel.Color = ARGBColors.Black;
            this.activityLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.activityLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.backgroundImage.addControl(this.activityLabel);
            this.wallInfoImage.Size = new Size(0x18c, height - 80);
            this.wallInfoImage.Position = new Point(8, 0x1d);
            this.backgroundImage.addControl(this.wallInfoImage);
            this.wallInfoImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
            this.wallScrollArea.Position = new Point(15, 15);
            this.wallScrollArea.Size = new Size(0x151, height - 0x65);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x151, height - 0x65));
            this.wallInfoImage.addControl(this.wallScrollArea);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x166, 15);
            this.wallScrollBar.Size = new Size(0x18, height - 0x65);
            this.wallInfoImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            StoredCountyInfo info = (StoredCountyInfo) this.countyList[countyID];
            bool flag = false;
            if (info != null)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - info.m_lastUpdateTime);
                if ((span.TotalMinutes <= 2.0) && (info.lastReturnData != null))
                {
                    goto Label_08B9;
                }
            }
            flag = true;
        Label_08B9:
            this.m_currentVillage = villageID;
            if (this.currentCounty != countyID)
            {
                this.currentLeaderID = -1;
                this.currentLeaderName = "";
                this.m_userIDOnCurrent = -1;
            }
            this.currentCounty = countyID;
            if (flag)
            {
                RemoteServices.Instance.set_GetCountyFrontPageInfo_UserCallBack(new RemoteServices.GetCountyFrontPageInfo_UserCallBack(this.getCountyFrontPageInfoCallback));
                RemoteServices.Instance.GetCountyFrontPageInfo(this.m_currentVillage);
            }
            this.updateLeaderInfo();
            if (!flag)
            {
                this.getCountyFrontPageInfoCallback(info.lastReturnData);
            }
            this.btnChat.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.btnChat.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.btnChat.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.btnChat.Position = new Point(base.Width - 230, base.Height - 90);
            this.btnChat.Text.Text = SK.Text("GENERIC_Chat", "Chat");
            this.btnChat.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.btnChat.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.btnChat.TextYOffset = -3;
            this.btnChat.Text.Color = ARGBColors.Black;
            this.btnChat.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.chatClick), "CountyFrontPagePanel2_chat");
            this.backgroundImage.addControl(this.btnChat);
        }

        private void InitializeComponent()
        {
            this.focusPanel = new Panel();
            base.SuspendLayout();
            this.focusPanel.BackColor = ARGBColors.Transparent;
            this.focusPanel.ForeColor = ARGBColors.Transparent;
            this.focusPanel.Location = new Point(0x3dc, 3);
            this.focusPanel.Name = "focusPanel";
            this.focusPanel.Size = new Size(1, 1);
            this.focusPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.focusPanel);
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CountyFrontPagePanel2";
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
            this.countyList.Clear();
        }

        public void update()
        {
        }

        public void updateLeaderInfo()
        {
            this.sheriffName.Text = this.currentLeaderName;
            this.m_userIDOnCurrent = this.currentLeaderID;
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

        public class StoredCountyInfo
        {
            public GetCountyFrontPageInfo_ReturnType lastReturnData;
            public DateTime m_lastUpdateTime = DateTime.MinValue;
        }
    }
}

