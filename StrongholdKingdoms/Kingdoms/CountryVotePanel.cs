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

    public class CountryVotePanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private IContainer components;
        private SparseArray countryList = new SparseArray();
        private List<ParishMember> countryMembers = new List<ParishMember>();
        private int currentCountry = -1;
        private int currentLeaderID = -1;
        private string currentLeaderName = "";
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private int electedLeaderID = -1;
        private string electedLeaderName = "";
        private CustomSelfDrawPanel.CSDLabel eligibleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel FactionsLabel = new CustomSelfDrawPanel.CSDLabel();
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDImage illustrationImage = new CustomSelfDrawPanel.CSDImage();
        public static CountryVotePanel instance;
        private DateTime lastProclamationTime = DateTime.MinValue;
        private List<VoteLine> lineList = new List<VoteLine>();
        private int m_currentVillage = -1;
        private int m_userIDOnCurrent = -1;
        private DateTime nextElectionTime = DateTime.MinValue;
        private ParishMemberComparer parishMemberComparer = new ParishMemberComparer();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton proclamationButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel proclamationLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel stewardLabel = new CustomSelfDrawPanel.CSDLabel();
        private int voteCap = 0x186a0;
        private CustomSelfDrawPanel.CSDLabel voteCapLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel voteCapLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel voteLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel votesAvailableLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel votesAvailableLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel votesReceivedLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool votingAllowed;
        private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private List<WallInfo> wallList = new List<WallInfo>();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public CountryVotePanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        public void addPlayers()
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            int yourVotes = 0;
            if (this.countryMembers != null)
            {
                foreach (ParishMember member in this.countryMembers)
                {
                    if (member.userID == RemoteServices.Instance.UserID)
                    {
                        yourVotes = member.numSpareVotes;
                        break;
                    }
                }
                this.countryMembers.Sort(this.parishMemberComparer);
                int position = 0;
                foreach (ParishMember member2 in this.countryMembers)
                {
                    VoteLine control = new VoteLine();
                    if (y != 0)
                    {
                        y += 5;
                    }
                    control.Position = new Point(0, y);
                    int numVotesReceived = member2.numVotesReceived;
                    if (numVotesReceived > this.voteCap)
                    {
                        numVotesReceived = this.voteCap;
                    }
                    control.init(member2.userName, member2.userID, member2.rank, member2.points, this.votingAllowed, member2.numSpareVotes, numVotesReceived, member2.areaID, member2.factionID, yourVotes, position, this);
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
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
            this.stewardLabel.Text = SK.Text("ParishWallPanel_King", "King") + " : " + this.currentLeaderName;
            this.m_userIDOnCurrent = this.currentLeaderID;
            TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - this.lastProclamationTime);
            if (this.currentLeaderID == RemoteServices.Instance.UserID)
            {
                this.proclamationButton.Visible = true;
                if (span.TotalDays >= 3.0)
                {
                    this.proclamationButton.Enabled = true;
                    this.proclamationLabel.Visible = false;
                }
                else
                {
                    this.proclamationButton.Enabled = false;
                    this.proclamationLabel.Visible = true;
                }
            }
            else
            {
                this.proclamationButton.Visible = false;
                this.proclamationLabel.Visible = false;
            }
            this.update();
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

        public void getCountryElectionInfoCallback(GetCountryElectionInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                StoredCountryInfo info = (StoredCountryInfo) this.countryList[returnData.countryID];
                if (info == null)
                {
                    info = new StoredCountryInfo();
                    this.countryList[returnData.countryID] = info;
                }
                info.m_lastUpdateTime = DateTime.Now;
                info.lastReturnData = returnData;
                if (this.currentCountry == returnData.countryID)
                {
                    this.votingAllowed = returnData.votingAllowed;
                    if (this.countryMembers == null)
                    {
                        this.countryMembers = new List<ParishMember>();
                    }
                    else
                    {
                        this.countryMembers.Clear();
                    }
                    if (returnData.countryMembers == null)
                    {
                        this.votesAvailableLabel.Text = SK.Text("CountryPanel_All_Provinces_Needed", "All Provinces need to be active before an election is held");
                        this.votesAvailableLabel.Position = new Point(0x1f, 12);
                        this.votesAvailableLabel.Size = new Size(400, 100);
                        this.votesAvailableLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
                        this.votesAvailableLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                        this.votesAvailableLabelValue.Visible = false;
                        this.wallInfoImage.invalidate();
                    }
                    else
                    {
                        this.countryMembers.AddRange(returnData.countryMembers);
                        int numSpareVotes = 0;
                        foreach (ParishMember member in this.countryMembers)
                        {
                            if (member.userID == RemoteServices.Instance.UserID)
                            {
                                numSpareVotes = member.numSpareVotes;
                                break;
                            }
                        }
                        this.votesAvailableLabelValue.Text = numSpareVotes.ToString();
                    }
                    this.m_userIDOnCurrent = -1;
                    this.electedLeaderID = returnData.leaderID;
                    this.electedLeaderName = returnData.leaderName;
                    this.lastProclamationTime = returnData.lastProclamation;
                    this.currentLeaderID = returnData.leaderID;
                    this.currentLeaderName = returnData.leaderName;
                    this.voteCapLabelValue.Text = returnData.voteCap.ToString();
                    this.voteCap = returnData.voteCap;
                    this.addPlayers();
                }
            }
        }

        public void init(bool resized)
        {
            int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            this.m_currentVillage = villageID;
            int countryID = GameEngine.Instance.World.getCountryFromVillageID(villageID);
            int height = base.Height;
            instance = this;
            base.clearControls();
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            this.backgroundImage.Size = new Size(base.Width, height - 40);
            this.backgroundImage.Position = new Point(0, 40);
            base.addControl(this.backgroundImage);
            this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            CustomSelfDrawPanel.WikiLinkControl.init(this.backgroundImage, 15, new Point(base.Width - 0x2c, 3));
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 0x81);
            this.backgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(0x5f, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider2Image.Position = new Point(0x16e, 0);
            this.headerLabelsImage.addControl(this.divider2Image);
            this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider3Image.Position = new Point(0x273, 0);
            this.headerLabelsImage.addControl(this.divider3Image);
            this.parishNameLabel.Text = GameEngine.Instance.World.getVillageName(this.m_currentVillage) + " (" + GameEngine.Instance.World.getCountryName(countryID) + ")";
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.illustrationImage.Image = (Image) GFXLibrary.parishwall_village_illlustration_04;
            this.illustrationImage.Position = new Point(0x11, 5);
            this.backgroundImage.addControl(this.illustrationImage);
            this.stewardLabel.Text = SK.Text("ParishWallPanel_King", "King") + " : ";
            this.stewardLabel.Color = ARGBColors.Black;
            this.stewardLabel.Position = new Point(5, 5);
            this.stewardLabel.Size = new Size(this.illustrationImage.Width - 6, 30);
            this.stewardLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.stewardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.illustrationImage.addControl(this.stewardLabel);
            this.proclamationButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.proclamationButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.proclamationButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.proclamationButton.Position = new Point(base.Width - 220, 7);
            this.proclamationButton.Text.Text = SK.Text("Capitials_Proclamation", "Send Proclamation");
            this.proclamationButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.proclamationButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.proclamationButton.TextYOffset = -3;
            this.proclamationButton.Text.Color = ARGBColors.Black;
            this.proclamationButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendProclamation));
            this.proclamationButton.CustomTooltipID = 0x106b;
            this.proclamationButton.Visible = false;
            this.headerImage.addControl(this.proclamationButton);
            this.proclamationLabel.Text = "";
            this.proclamationLabel.Color = ARGBColors.White;
            this.proclamationLabel.DropShadowColor = ARGBColors.Black;
            this.proclamationLabel.Position = new Point(20, 0);
            this.proclamationLabel.Size = new Size((base.Width - 40) - 220, 40);
            this.proclamationLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            this.proclamationLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
            this.proclamationLabel.Visible = false;
            this.headerImage.addControl(this.proclamationLabel);
            this.wallInfoImage.Size = new Size(440, 0x55);
            this.wallInfoImage.Position = new Point(460, 20);
            this.backgroundImage.addControl(this.wallInfoImage);
            this.wallInfoImage.Create((Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.mail2_rounded_rectangle_tan_bottom_right);
            this.wallScrollArea.Position = new Point(0x19, 0x9e);
            this.wallScrollArea.Size = new Size(0x393, height - 0xd4);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, height - 0xd4));
            this.backgroundImage.addControl(this.wallScrollArea);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Position = new Point(0x3af, 0x9e);
            this.wallScrollBar.Size = new Size(0x18, height - 0xd4);
            this.backgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.votesAvailableLabel.Text = SK.Text("GENERIC_Votes_Available", "Votes Available") + " :";
            this.votesAvailableLabel.Color = ARGBColors.Black;
            this.votesAvailableLabel.Position = new Point(0x1f, 0x1b);
            this.votesAvailableLabel.Size = new Size(300, 40);
            this.votesAvailableLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.votesAvailableLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.wallInfoImage.addControl(this.votesAvailableLabel);
            this.votesAvailableLabelValue.Text = "0";
            this.votesAvailableLabelValue.Color = ARGBColors.Black;
            this.votesAvailableLabelValue.Position = new Point(0x133, 0x1b);
            this.votesAvailableLabelValue.Size = new Size(100, 40);
            this.votesAvailableLabelValue.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.votesAvailableLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.votesAvailableLabelValue.Visible = true;
            this.wallInfoImage.addControl(this.votesAvailableLabelValue);
            if (GameEngine.Instance.World.SecondAgeWorld)
            {
                this.votesAvailableLabel.Position = new Point(0x1f, 12);
                this.votesAvailableLabelValue.Position = new Point(0x133, 12);
                this.voteCapLabel.Text = SK.Text("ParishPanel_Current_Vote_cap", "Current Vote Cap") + " :";
                this.voteCapLabel.Color = ARGBColors.Black;
                this.voteCapLabel.Position = new Point(0x1f, 0x2a);
                this.voteCapLabel.Size = new Size(300, 40);
                this.voteCapLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                this.voteCapLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.voteCapLabel.Visible = true;
                this.wallInfoImage.addControl(this.voteCapLabel);
                this.voteCapLabelValue.Text = "0";
                this.voteCapLabelValue.Color = ARGBColors.Black;
                this.voteCapLabelValue.Position = new Point(0x133, 0x2a);
                this.voteCapLabelValue.Size = new Size(100, 40);
                this.voteCapLabelValue.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
                this.voteCapLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
                this.voteCapLabelValue.Visible = true;
                this.wallInfoImage.addControl(this.voteCapLabelValue);
            }
            this.voteLabel.Text = SK.Text("GENERIC_Vote", "Vote");
            this.voteLabel.Color = ARGBColors.Black;
            this.voteLabel.Position = new Point(15, -2);
            this.voteLabel.Size = new Size(0x51, this.headerLabelsImage.Height);
            this.voteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.voteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.voteLabel);
            this.eligibleLabel.Text = SK.Text("GENERIC_Eligible_Candidates", "Eligible Candidates");
            this.eligibleLabel.Color = ARGBColors.Black;
            this.eligibleLabel.Position = new Point(0x6a, -2);
            this.eligibleLabel.Size = new Size(250, this.headerLabelsImage.Height);
            this.eligibleLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.eligibleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.eligibleLabel);
            this.FactionsLabel.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
            this.FactionsLabel.Color = ARGBColors.Black;
            this.FactionsLabel.Position = new Point(0x178, -2);
            this.FactionsLabel.Size = new Size(0xf7, this.headerLabelsImage.Height);
            this.FactionsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.FactionsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.FactionsLabel);
            this.votesReceivedLabel.Text = SK.Text("GENERIC_Votes_Received", "Votes Received");
            this.votesReceivedLabel.Color = ARGBColors.Black;
            this.votesReceivedLabel.Position = new Point(0x27b, -2);
            this.votesReceivedLabel.Size = new Size(300, this.headerLabelsImage.Height);
            this.votesReceivedLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.votesReceivedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.votesReceivedLabel);
            if (resized)
            {
                this.addPlayers();
                return;
            }
            StoredCountryInfo info = (StoredCountryInfo) this.countryList[countryID];
            bool flag = false;
            if (info != null)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - info.m_lastUpdateTime);
                if ((span.TotalMinutes <= 2.0) && (info.lastReturnData != null))
                {
                    goto Label_0CCD;
                }
            }
            flag = true;
        Label_0CCD:
            this.m_currentVillage = villageID;
            if (this.currentCountry != countryID)
            {
                this.countryMembers.Clear();
                this.currentLeaderID = -1;
                this.electedLeaderID = -1;
                this.currentLeaderName = "";
                this.electedLeaderName = "";
                this.m_userIDOnCurrent = -1;
            }
            this.currentCountry = countryID;
            if (flag)
            {
                RemoteServices.Instance.set_GetCountryElectionInfo_UserCallBack(new RemoteServices.GetCountryElectionInfo_UserCallBack(this.getCountryElectionInfoCallback));
                RemoteServices.Instance.GetCountryElectionInfo(this.m_currentVillage);
            }
            this.nextElectionTime = DateTime.MinValue;
            this.votingAllowed = false;
            this.addPlayers();
            if (!flag)
            {
                this.getCountryElectionInfoCallback(info.lastReturnData);
            }
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
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "CountryVotePanel";
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

        private void makeCountryVoteCallback(MakeCountryVote_ReturnType returnData)
        {
            if (returnData.Success && (returnData.returnData != null))
            {
                this.getCountryElectionInfoCallback(returnData.returnData);
                GameEngine.Instance.forceFullTick();
            }
        }

        private void sendProclamation()
        {
            StoredCountryInfo info = (StoredCountryInfo) this.countryList[this.currentCountry];
            if (info != null)
            {
                info.m_lastUpdateTime = DateTime.MinValue;
            }
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
            InterfaceMgr.Instance.getMainTabBar().selectDummyTabFast(0x15);
            InterfaceMgr.Instance.sendProclamation(7, GameEngine.Instance.World.getCountryFromVillageID(this.m_currentVillage));
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
            if (this.proclamationLabel.Visible)
            {
                TimeSpan span = (TimeSpan) (VillageMap.getCurrentServerTime() - this.lastProclamationTime);
                if (span.TotalDays >= 3.0)
                {
                    this.proclamationLabel.Visible = false;
                    this.proclamationButton.Enabled = true;
                }
                else
                {
                    this.proclamationLabel.Text = SK.Text("Proclamations_time_to_go", "Time before next Proclamation : ") + VillageMap.createBuildTimeString(0x3f480 - ((int) span.TotalSeconds));
                }
            }
        }

        public void updateWallArea()
        {
        }

        public void voteChanged(int userID)
        {
            RemoteServices.Instance.set_MakeCountryVote_UserCallBack(new RemoteServices.MakeCountryVote_UserCallBack(this.makeCountryVoteCallback));
            RemoteServices.Instance.MakeCountryVote(this.m_currentVillage, userID);
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x9e - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class ParishMemberComparer : IComparer<ParishMember>
        {
            public int Compare(ParishMember x, ParishMember y)
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
                if (x.numVotesReceived < y.numVotesReceived)
                {
                    return 1;
                }
                if (x.numVotesReceived > y.numVotesReceived)
                {
                    return -1;
                }
                return x.userName.CompareTo(y.userName);
            }
        }

        public class StoredCountryInfo
        {
            public GetCountryElectionInfo_ReturnType lastReturnData;
            public DateTime m_lastUpdateTime = DateTime.MinValue;
        }

        public class VoteLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
            private int m_factionID = -1;
            private CountryVotePanel m_parent;
            private int m_position = -1000;
            private int m_userID = -1;
            private CustomSelfDrawPanel.CSDLabel personName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton voteButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDLabel votesLabel = new CustomSelfDrawPanel.CSDLabel();

            private void factionClick()
            {
                if (this.m_factionID >= 0)
                {
                    GameEngine.Instance.playInterfaceSound("CountryVotePanel_faction");
                    InterfaceMgr.Instance.showFactionPanel(this.m_factionID);
                }
            }

            public void init(string playerName, int userID, int rank, int points, bool votingAllowed, int numSpareVotes, int numReceivedVotes, int parishID, int factionID, int yourVotes, int position, CountryVotePanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_userID = userID;
                this.m_factionID = factionID;
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
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.voteButton.ImageNorm = (Image) GFXLibrary.parishwall_button_vote_checked_normal;
                this.voteButton.ImageOver = (Image) GFXLibrary.parishwall_button_vote_checked_over;
                this.voteButton.Position = new Point(8, 4);
                this.voteButton.Text.Text = SK.Text("GENERIC_Vote", "Vote");
                this.voteButton.Text.Color = ARGBColors.Black;
                this.voteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.voteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked), "CountryVotePanel_vote");
                this.backgroundImage.addControl(this.voteButton);
                if (yourVotes > 0)
                {
                    this.voteButton.Enabled = true;
                }
                else
                {
                    this.voteButton.Enabled = false;
                }
                NumberFormatInfo nFI = GameEngine.NFI;
                int num = 0;
                if (factionID < 0)
                {
                    this.factionName.Text = "";
                }
                else
                {
                    FactionData data = GameEngine.Instance.World.getFaction(factionID);
                    if (data == null)
                    {
                        this.factionName.Text = "";
                    }
                    else
                    {
                        this.factionName.Text = data.factionNameAbrv;
                        int houseID = data.houseID;
                        if (houseID > 0)
                        {
                            this.houseImage.Image = (Image) GFXLibrary.house_flag_001_small;
                            switch (houseID)
                            {
                                case 1:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_001_small;
                                    break;

                                case 2:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_002_small;
                                    break;

                                case 3:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_003_small;
                                    break;

                                case 4:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_004_small;
                                    break;

                                case 5:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_005_small;
                                    break;

                                case 6:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_006_small;
                                    break;

                                case 7:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_007_small;
                                    break;

                                case 8:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_008_small;
                                    break;

                                case 9:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_009_small;
                                    break;

                                case 10:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_010_small;
                                    break;

                                case 11:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_011_small;
                                    break;

                                case 12:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_012_small;
                                    break;

                                case 13:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_013_small;
                                    break;

                                case 14:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_014_small;
                                    break;

                                case 15:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_015_small;
                                    break;

                                case 0x10:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_016_small;
                                    break;

                                case 0x11:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_017_small;
                                    break;

                                case 0x12:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_018_small;
                                    break;

                                case 0x13:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_019_small;
                                    break;

                                case 20:
                                    this.houseImage.Image = (Image) GFXLibrary.house_flag_020_small;
                                    break;
                            }
                            this.houseImage.Position = new Point(0x179, 5);
                            this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
                            this.backgroundImage.addControl(this.houseImage);
                            num = 0x20;
                        }
                    }
                }
                this.factionName.Color = ARGBColors.Black;
                this.factionName.Position = new Point(0x179 + num, 0);
                this.factionName.Size = new Size(210, this.backgroundImage.Height);
                this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                if (factionID >= 0)
                {
                    this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
                }
                this.backgroundImage.addControl(this.factionName);
                this.personName.Text = playerName;
                this.personName.Color = ARGBColors.Black;
                this.personName.Position = new Point(0x88, 0);
                this.personName.Size = new Size(0xe1, this.backgroundImage.Height);
                this.personName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.personName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.personName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClick), "CountryVotePanel_user_clicked");
                this.backgroundImage.addControl(this.personName);
                this.votesLabel.Text = numReceivedVotes.ToString("N", nFI);
                this.votesLabel.Color = ARGBColors.Black;
                this.votesLabel.Position = new Point(0x27b, 0);
                this.votesLabel.Size = new Size(150, this.backgroundImage.Height);
                this.votesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.votesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.votesLabel);
                this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(userID, 0x19, 0x1c);
                if (this.shieldImage.Image != null)
                {
                    this.shieldImage.Position = new Point(0x6a, 1);
                    this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClick));
                    this.backgroundImage.addControl(this.shieldImage);
                }
                base.invalidate();
            }

            public void lineClicked()
            {
                if (this.m_parent != null)
                {
                    this.voteButton.Enabled = false;
                    this.m_parent.voteChanged(this.m_userID);
                }
            }

            private void playerClick()
            {
                if (this.m_userID >= 0)
                {
                    WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                        userID = this.m_userID
                    };
                    InterfaceMgr.Instance.showUserInfoScreen(userInfo);
                }
            }

            public void update()
            {
            }
        }
    }
}

