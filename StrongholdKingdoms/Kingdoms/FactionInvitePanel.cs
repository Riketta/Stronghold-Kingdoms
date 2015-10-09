namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class FactionInvitePanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDLabel applicationsNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl appMouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDArea appScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar appScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private int blockYSize;
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static FactionInvitePanel instance;
        private List<FactionInviteLine> lineList = new List<FactionInviteLine>();
        private List<FactionInviteLine> lineList2 = new List<FactionInviteLine>();
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        public const int PANEL_ID = 0x29;
        private CustomSelfDrawPanel.CSDLabel playerNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public FactionInvitePanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addPlayers()
        {
            this.wallScrollArea.clearControls();
            this.appScrollArea.clearControls();
            int y = 0;
            int num2 = 0;
            this.lineList.Clear();
            this.lineList2.Clear();
            if (GameEngine.Instance.World.getRank() >= 6)
            {
                int position = 0;
                FactionInviteData[] factionInvites = GameEngine.Instance.World.FactionInvites;
                if (factionInvites != null)
                {
                    foreach (FactionInviteData data in factionInvites)
                    {
                        FactionData factionData = GameEngine.Instance.World.getFaction(data.factionID);
                        if (factionData != null)
                        {
                            FactionInviteLine control = new FactionInviteLine();
                            if (y != 0)
                            {
                                y += 5;
                            }
                            control.Position = new Point(0, y);
                            control.init(factionData, position, this, true);
                            this.wallScrollArea.addControl(control);
                            y += control.Height;
                            this.lineList.Add(control);
                            position++;
                        }
                    }
                }
                int num4 = 0;
                List<FactionInviteData> factionApplications = GameEngine.Instance.World.FactionApplications;
                if (factionApplications != null)
                {
                    foreach (FactionInviteData data3 in factionApplications)
                    {
                        FactionData data4 = GameEngine.Instance.World.getFaction(data3.factionID);
                        if (data4 != null)
                        {
                            FactionInviteLine line2 = new FactionInviteLine();
                            if (num2 != 0)
                            {
                                num2 += 5;
                            }
                            line2.Position = new Point(0, num2);
                            line2.init(data4, num4, this, false);
                            this.appScrollArea.addControl(line2);
                            num2 += line2.Height;
                            this.lineList2.Add(line2);
                            num4++;
                        }
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
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
            this.appScrollArea.Size = new Size(this.appScrollArea.Width, num2);
            if (y < this.appScrollBar.Height)
            {
                this.appScrollBar.Visible = false;
            }
            else
            {
                this.appScrollBar.Visible = true;
                this.appScrollBar.NumVisibleLines = this.appScrollBar.Height;
                this.appScrollBar.Max = num2 - this.appScrollBar.Height;
            }
            this.appScrollArea.invalidate();
            this.appScrollBar.invalidate();
            this.update();
            base.Invalidate();
        }

        private void appMouseWheelMoved(int delta)
        {
            if (this.appScrollBar.Visible)
            {
                if (delta < 0)
                {
                    this.appScrollBar.scrollDown(40);
                }
                else if (delta > 0)
                {
                    this.appScrollBar.scrollUp(40);
                }
            }
        }

        private void appScrollBarMoved()
        {
            int y = this.appScrollBar.Value;
            this.appScrollArea.Position = new Point(this.appScrollArea.X, (0x26 + this.blockYSize) - y);
            this.appScrollArea.ClipRect = new Rectangle(this.appScrollArea.ClipRect.X, y, this.appScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.appScrollArea.invalidate();
            this.appScrollBar.invalidate();
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
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

        public void init(bool resized)
        {
            int height = base.Height;
            this.blockYSize = height / 2;
            instance = this;
            base.clearControls();
            this.sidebar.addSideBar(0, this);
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 9);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.playerNameLabel.Text = SK.Text("FactionsPanel_Users", "Invites");
            this.playerNameLabel.Color = ARGBColors.Black;
            this.playerNameLabel.Position = new Point(9, -2);
            this.playerNameLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.playerNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.playerNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.playerNameLabel);
            this.headerLabelsImage2.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage2.Position = new Point(0x19, this.blockYSize);
            this.mainBackgroundImage.addControl(this.headerLabelsImage2);
            this.headerLabelsImage2.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.applicationsNameLabel.Text = SK.Text("FactionInvites_Applications", "Applications");
            this.applicationsNameLabel.Color = ARGBColors.Black;
            this.applicationsNameLabel.Position = new Point(9, -2);
            this.applicationsNameLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.applicationsNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.applicationsNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage2.addControl(this.applicationsNameLabel);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Faction_Invites", "Faction Invites"));
            this.wallScrollArea.Position = new Point(0x19, 0x26);
            this.wallScrollArea.Size = new Size(0x2c1, this.blockYSize - 50);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, this.blockYSize - 50));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x2dd, 0x26);
            this.wallScrollBar.Size = new Size(0x18, this.blockYSize - 50);
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.appScrollArea.Position = new Point(0x19, 0x26 + this.blockYSize);
            this.appScrollArea.Size = new Size(0x2c1, this.blockYSize - 50);
            this.appScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, this.blockYSize - 50));
            this.mainBackgroundImage.addControl(this.appScrollArea);
            this.appMouseWheelOverlay.Position = this.appScrollArea.Position;
            this.appMouseWheelOverlay.Size = this.appScrollArea.Size;
            this.appMouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.appMouseWheelMoved));
            this.mainBackgroundImage.addControl(this.appMouseWheelOverlay);
            int num2 = this.appScrollBar.Value;
            this.appScrollBar.Visible = false;
            this.appScrollBar.Position = new Point(0x2dd, 0x26 + this.blockYSize);
            this.appScrollBar.Size = new Size(0x18, this.blockYSize - 50);
            this.mainBackgroundImage.addControl(this.appScrollBar);
            this.appScrollBar.Value = 0;
            this.appScrollBar.Max = 100;
            this.appScrollBar.NumVisibleLines = 0x19;
            this.appScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.appScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.appScrollBarMoved));
            if (GameEngine.Instance.World.getRank() < 6)
            {
                this.rankLabel.Text = SK.Text("FACTION_INVITE_rank", "You don't currently have the required Rank (7) to join a Faction.");
                this.rankLabel.Color = ARGBColors.Black;
                this.rankLabel.Position = new Point(0, 50);
                this.rankLabel.Size = this.wallScrollArea.Size;
                this.rankLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.mainBackgroundImage.addControl(this.rankLabel);
            }
            if (!resized)
            {
                CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
            }
            this.addPlayers();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "FactionInvitePanel";
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

        public void update()
        {
            this.sidebar.update();
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x26 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class FactionInviteLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDButton acceptButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton declineButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
            private FactionData m_factionData;
            private FactionInvitePanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();

            public void acceptClicked()
            {
                this.declineButton.Enabled = false;
                this.acceptButton.Enabled = false;
                RemoteServices.Instance.set_FactionReplyToInvite_UserCallBack(new RemoteServices.FactionReplyToInvite_UserCallBack(this.factionReplyToInviteCallback));
                RemoteServices.Instance.FactionReplyToInvite(this.m_factionData.factionID, true);
            }

            public void appCancelClicked()
            {
                this.declineButton.Enabled = false;
                RemoteServices.Instance.set_FactionApplication_UserCallBack(new RemoteServices.FactionApplication_UserCallBack(this.factionApplicationCallback));
                RemoteServices.Instance.FactionApplicationCancel(this.m_factionData.factionID);
            }

            public void clickedLine()
            {
                GameEngine.Instance.playInterfaceSound("FactionInvitePanel_faction_clicked");
                InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
            }

            public void declineClicked()
            {
                this.declineButton.Enabled = false;
                this.acceptButton.Enabled = false;
                RemoteServices.Instance.set_FactionReplyToInvite_UserCallBack(new RemoteServices.FactionReplyToInvite_UserCallBack(this.factionReplyToInviteCallback));
                RemoteServices.Instance.FactionReplyToInvite(this.m_factionData.factionID, false);
            }

            public void factionApplicationCallback(FactionApplication_ReturnType returnData)
            {
                if (returnData.Success)
                {
                    GameEngine.Instance.World.FactionInvites = returnData.invites;
                    GameEngine.Instance.World.FactionApplications = returnData.applications;
                    this.m_parent.init(false);
                }
                else
                {
                    this.declineButton.Enabled = true;
                }
            }

            public void factionReplyToInviteCallback(FactionReplyToInvite_ReturnType returnData)
            {
                if (returnData.m_errorCode == ErrorCodes.ErrorCode.FACTION_FULL)
                {
                    MyMessageBox.Show(SK.Text("FactionsPanel_Faction_Full", "The Faction is full."), SK.Text("GENERIC_Error", "Error"));
                }
                if (returnData.Success)
                {
                    GameEngine.Instance.World.FactionMembers = returnData.members;
                    GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                    GameEngine.Instance.World.FactionInvites = returnData.invites;
                    GameEngine.Instance.World.FactionApplications = returnData.applications;
                    if (returnData.yourFaction != null)
                    {
                        GameEngine.Instance.World.updateYourVillageFactions(returnData.yourFaction.factionID);
                        if (returnData.decline)
                        {
                            this.m_parent.init(false);
                            return;
                        }
                        GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
                        GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
                        GameEngine.Instance.World.HouseAllies = returnData.yourHouseAllies;
                        GameEngine.Instance.World.HouseEnemies = returnData.yourHouseEnemies;
                    }
                    else
                    {
                        GameEngine.Instance.World.updateYourVillageFactions(-1);
                    }
                    GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
                    InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
                }
                else
                {
                    this.declineButton.Enabled = true;
                    this.acceptButton.Enabled = true;
                }
            }

            public void init(FactionData factionData, int position, FactionInvitePanel parent, bool invite)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_factionData = factionData;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(60, 0);
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.flagImage.createFromFlagData(factionData.flagData);
                this.flagImage.Position = new Point(0, 0);
                this.flagImage.Scale = 0.25;
                this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                base.addControl(this.flagImage);
                NumberFormatInfo nFI = GameEngine.NFI;
                this.playerName.Text = factionData.factionName;
                this.playerName.Color = ARGBColors.Black;
                this.playerName.Position = new Point(9, 0);
                this.playerName.Size = new Size(280, this.backgroundImage.Height);
                this.playerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.playerName);
                if (invite)
                {
                    if (RemoteServices.Instance.UserFactionID < 0)
                    {
                        this.acceptButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                        this.acceptButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                        this.acceptButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                        this.acceptButton.Position = new Point(350, 0);
                        this.acceptButton.Text.Text = SK.Text("FactionInviteLine_Accept", "Accept");
                        this.acceptButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                        this.acceptButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                        this.acceptButton.TextYOffset = -3;
                        this.acceptButton.Text.Color = ARGBColors.Black;
                        this.acceptButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.acceptClicked), "FactionInvitePanel_accept_clicked");
                        this.backgroundImage.addControl(this.acceptButton);
                    }
                    this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                    this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                    this.declineButton.Position = new Point(500, 0);
                    this.declineButton.Text.Text = SK.Text("FactionInviteLine_Decline", "Decline");
                    this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.declineButton.TextYOffset = -3;
                    this.declineButton.Text.Color = ARGBColors.Black;
                    this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineClicked), "FactionInvitePanel_declined_clicked");
                    this.backgroundImage.addControl(this.declineButton);
                }
                else
                {
                    this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                    this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                    this.declineButton.Position = new Point(500, 0);
                    this.declineButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                    this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.declineButton.TextYOffset = -3;
                    this.declineButton.Text.Color = ARGBColors.Black;
                    this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.appCancelClicked), "FactionInvitePanel_declined_clicked");
                    this.backgroundImage.addControl(this.declineButton);
                }
                base.invalidate();
            }

            public void update()
            {
            }
        }
    }
}

