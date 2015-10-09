namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class FactionOfficersPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton applicationButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel applicationsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDImage backImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage barImage3 = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton editButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel factionMottoLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel factionNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDFactionFlagImage flagimage = new CustomSelfDrawPanel.CSDFactionFlagImage();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
        public static FactionOfficersPanel instance;
        private CustomSelfDrawPanel.CSDButton inviteButton = new CustomSelfDrawPanel.CSDButton();
        private string invitedUserName = "";
        private CustomSelfDrawPanel.CSDLabel leadershipVoteLabel = new CustomSelfDrawPanel.CSDLabel();
        private List<FactionMemberLineOfficer> lineList = new List<FactionMemberLineOfficer>();
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel membersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel membersLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        public const int PANEL_ID = 0x2e;
        private CustomSelfDrawPanel.CSDLabel playerNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pointsHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pointsHeaderLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankHeaderLabelValue = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
        private TextBox tbInviteName;
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel villagesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDExtendingPanel wallInfoImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public FactionOfficersPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addPlayers(FactionMemberData[] fmd)
        {
            this.wallScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            int position = 0;
            if (fmd != null)
            {
                int numOfficers = 0;
                foreach (FactionMemberData data in fmd)
                {
                    if (data.status == 2)
                    {
                        numOfficers++;
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    int num5 = 1;
                    switch (i)
                    {
                        case 1:
                            num5 = 2;
                            break;

                        case 2:
                            num5 = 0;
                            break;

                        case 3:
                            num5 = -1;
                            break;

                        case 4:
                            num5 = -3;
                            break;
                    }
                    foreach (FactionMemberData data2 in fmd)
                    {
                        if (data2.status == num5)
                        {
                            FactionMemberLineOfficer control = new FactionMemberLineOfficer();
                            if (y != 0)
                            {
                                y += 5;
                            }
                            control.Position = new Point(0, y);
                            control.init(data2, position, this, true, numOfficers);
                            this.wallScrollArea.addControl(control);
                            y += control.Height;
                            this.lineList.Add(control);
                            position++;
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
            this.update();
            base.Invalidate();
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

        public void editClicked()
        {
            InterfaceMgr.Instance.showEditFactionPanel();
        }

        public void factionApplicationProcessingCallback(FactionApplicationProcessing_ReturnType returnData)
        {
            this.applicationButton.Enabled = true;
            if (returnData.members != null)
            {
                GameEngine.Instance.World.FactionMembers = returnData.members;
                GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                this.init(false);
            }
        }

        public void factionSendInviteCallback(FactionSendInvite_ReturnType returnData)
        {
            if (returnData.members != null)
            {
                GameEngine.Instance.World.FactionMembers = returnData.members;
                this.addPlayers(GameEngine.Instance.World.FactionMembers);
            }
            if (returnData.Success)
            {
                MyMessageBox.Show(SK.Text("FactionsPanel_Invited", "Player Successfully Invited") + Environment.NewLine + Environment.NewLine + this.invitedUserName, SK.Text("FactionsPanel_Invited_Header", "Player Invited"));
                this.tbInviteName.Text = "";
                this.inviteButton.Enabled = false;
            }
            else
            {
                switch (returnData.m_errorCode)
                {
                    case ErrorCodes.ErrorCode.FACTION_ALREADY_IN_FACTION:
                        MyMessageBox.Show(SK.Text("FactionsPanel_Already_In_Faction", "This user is already in this faction."), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
                        return;

                    case ErrorCodes.ErrorCode.FACTION_INVALID_INVITE:
                        break;

                    case ErrorCodes.ErrorCode.FACTION_INVITE_ALREADY_EXISTS:
                        MyMessageBox.Show(SK.Text("FactionsPanel_Already_Has_Invite", "This User already has an invite."), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
                        return;

                    case ErrorCodes.ErrorCode.FACTION_FULL:
                        MyMessageBox.Show(SK.Text("FactionsPanel_Faction_Full", "The Faction is full."), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
                        return;

                    case ErrorCodes.ErrorCode.FACTION_UNKNOWN_USER:
                        MyMessageBox.Show(SK.Text("FactionsPanel_Unknown_User", "Unknown User"), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
                        return;

                    case ErrorCodes.ErrorCode.FACTION_INVITEE_TOO_LOW:
                        MyMessageBox.Show(SK.Text("FactionsPanel_Rank_Too_Low", "User's rank too low"), SK.Text("FactionsPanel_Invite_Error", "Invite Error"));
                        break;

                    default:
                        return;
                }
            }
        }

        private void getViewFactionDataCallback(GetViewFactionData_ReturnType returnData)
        {
            if (returnData.Success)
            {
                NumberFormatInfo nFI = GameEngine.NFI;
                this.addPlayers(returnData.members);
                if (returnData.factionData != null)
                {
                    GameEngine.Instance.World.setFactionMemberData(returnData.factionData.factionID, returnData.members);
                }
                GameEngine.Instance.World.setFactionData(returnData.factionData);
                GameEngine.Instance.World.FactionAllies = returnData.yourAllies;
                GameEngine.Instance.World.FactionEnemies = returnData.yourEnemies;
            }
        }

        public void houseClicked()
        {
            FactionData yourFaction = GameEngine.Instance.World.YourFaction;
            if ((yourFaction != null) && (yourFaction.houseID > 0))
            {
                InterfaceMgr.Instance.showHousePanel(yourFaction.houseID);
            }
        }

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            NumberFormatInfo nFI = GameEngine.NFI;
            this.sidebar.addSideBar(3, this);
            FactionData yourFaction = GameEngine.Instance.World.YourFaction;
            if (yourFaction == null)
            {
                yourFaction = new FactionData();
            }
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width - 200, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width - 200, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.backImage1.Image = (Image) GFXLibrary.faction_tanback;
            this.backImage1.Position = new Point((this.mainBackgroundImage.Size.Width - this.backImage1.Size.Width) - 0x19, 12);
            this.mainBackgroundImage.addControl(this.backImage1);
            this.backImage2.Image = (Image) GFXLibrary.faction_title_band;
            this.backImage2.Position = new Point(20, 20);
            this.mainBackgroundImage.addControl(this.backImage2);
            this.barImage1.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
            this.barImage1.Position = new Point(0x114, 70);
            this.mainBackgroundImage.addControl(this.barImage1);
            this.barImage2.Image = (Image) GFXLibrary.faction_bar_tan_1_lighter;
            this.barImage2.Position = new Point(0x114, 0x5e);
            this.mainBackgroundImage.addControl(this.barImage2);
            this.barImage3.Image = (Image) GFXLibrary.faction_bar_tan_1_heavier;
            this.barImage3.Position = new Point(0x114, 0x76);
            this.mainBackgroundImage.addControl(this.barImage3);
            this.factionNameLabel.Text = yourFaction.factionName;
            this.factionNameLabel.Color = ARGBColors.Black;
            this.factionNameLabel.Position = new Point(0xcd, 10);
            this.factionNameLabel.Size = new Size(600, 40);
            this.factionNameLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Regular);
            this.factionNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.factionNameLabel);
            int num2 = GameEngine.Instance.World.getYourFactionRank();
            this.factionMottoLabel.Text = "\"" + yourFaction.factionMotto + "\"";
            this.factionMottoLabel.Color = ARGBColors.Black;
            if (num2 == 1)
            {
                this.factionMottoLabel.Position = new Point(230, 0x29);
            }
            else
            {
                this.factionMottoLabel.Position = new Point(0xcd, 0x29);
            }
            this.factionMottoLabel.Size = new Size(600, 40);
            this.factionMottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.factionMottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.factionMottoLabel);
            this.applicationButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
            this.applicationButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
            this.applicationButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
            this.applicationButton.Position = new Point(0x18, 0x7e);
            if (yourFaction.openForApplications)
            {
                this.applicationButton.Text.Text = SK.Text("FactionInvites_Accepting_Apps", "Accepting");
            }
            else
            {
                this.applicationButton.Text.Text = SK.Text("FactionInvites_Not_Accepting_App", "Not Accepting");
            }
            this.applicationButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.applicationButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.applicationButton.TextYOffset = -3;
            this.applicationButton.Text.Color = ARGBColors.Black;
            this.applicationButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.setApplicationModeClicked));
            this.applicationButton.Enabled = true;
            this.mainBackgroundImage.addControl(this.applicationButton);
            this.applicationsLabel.Text = SK.Text("FactionInvites_Applications", "Applications");
            this.applicationsLabel.Color = ARGBColors.Black;
            this.applicationsLabel.Position = new Point(0x18, 0x60);
            this.applicationsLabel.Size = this.applicationButton.Size;
            this.applicationsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.applicationsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundImage.addControl(this.applicationsLabel);
            if (num2 == 1)
            {
                this.editButton.ImageNorm = (Image) GFXLibrary.faction_pen;
                this.editButton.ImageOver = (Image) GFXLibrary.faction_pen;
                this.editButton.ImageClick = (Image) GFXLibrary.faction_pen;
                this.editButton.Position = new Point(0xcd, 0x29);
                this.editButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "FactionOfficersPanel_edit");
                this.mainBackgroundImage.addControl(this.editButton);
            }
            if (yourFaction.houseID > 0)
            {
                this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + yourFaction.houseID.ToString();
                this.houseLabel.Color = ARGBColors.Black;
                this.houseLabel.Position = new Point(0x23f, 110);
                this.houseLabel.Size = new Size(200, 50);
                this.houseLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.houseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionOfficersPanel_house");
                this.mainBackgroundImage.addControl(this.houseLabel);
                this.houseImage.Image = (Image) GFXLibrary.house_circles_large[yourFaction.houseID - 1];
                this.houseImage.Position = new Point(0x2a3 - (this.houseImage.Image.Width / 2), (0x41 - (this.houseImage.Image.Height / 2)) + 8);
                this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "FactionOfficersPanel_house");
                this.mainBackgroundImage.addControl(this.houseImage);
            }
            this.membersLabel.Text = SK.Text("FactionInvites_Members", "Members");
            this.membersLabel.Color = ARGBColors.Black;
            this.membersLabel.Position = new Point(0x11c, 0x49);
            this.membersLabel.Size = new Size(600, 40);
            this.membersLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.membersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.membersLabel);
            this.membersLabelValue.Text = yourFaction.numMembers.ToString();
            this.membersLabelValue.Color = ARGBColors.Black;
            this.membersLabelValue.Position = new Point(30, 0x49);
            this.membersLabelValue.Size = new Size(0x1e2, 40);
            this.membersLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.membersLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.membersLabelValue);
            this.rankHeaderLabel.Text = SK.Text("STATS_CATEGORY_TITLE_RANK", "Rank");
            this.rankHeaderLabel.Color = ARGBColors.Black;
            this.rankHeaderLabel.Position = new Point(0x11c, 0x79);
            this.rankHeaderLabel.Size = new Size(600, 40);
            this.rankHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.rankHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.rankHeaderLabel);
            this.rankHeaderLabelValue.Text = (GameEngine.Instance.World.getYourFactionRank() + 1).ToString("N", nFI);
            this.rankHeaderLabelValue.Color = ARGBColors.Black;
            this.rankHeaderLabelValue.Position = new Point(30, 0x79);
            this.rankHeaderLabelValue.Size = new Size(0x1e2, 40);
            this.rankHeaderLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.rankHeaderLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.rankHeaderLabelValue);
            this.pointsHeaderLabel.Text = SK.Text("FactionsPanel_Points", "Points");
            this.pointsHeaderLabel.Color = ARGBColors.Black;
            this.pointsHeaderLabel.Position = new Point(0x11c, 0x61);
            this.pointsHeaderLabel.Size = new Size(600, 40);
            this.pointsHeaderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.pointsHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.pointsHeaderLabel);
            this.pointsHeaderLabelValue.Text = yourFaction.points.ToString("N", nFI);
            this.pointsHeaderLabelValue.Color = ARGBColors.Black;
            this.pointsHeaderLabelValue.Position = new Point(30, 0x61);
            this.pointsHeaderLabelValue.Size = new Size(0x1e2, 40);
            this.pointsHeaderLabelValue.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.pointsHeaderLabelValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.pointsHeaderLabelValue);
            this.headerLabelsImage.Size = new Size(((base.Width - 0x19) - 0x17) - 200, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 0x9f);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.playerNameLabel.Text = SK.Text("UserInfoPanel_", "Player Name");
            this.playerNameLabel.Color = ARGBColors.Black;
            this.playerNameLabel.Position = new Point(9, -2);
            this.playerNameLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.playerNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.playerNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.playerNameLabel);
            this.leadershipVoteLabel.Text = SK.Text("FactionsPanel_Leadership_Vote", "Leadership Vote");
            this.leadershipVoteLabel.Color = ARGBColors.Black;
            this.leadershipVoteLabel.Position = new Point(0x1bc, -2);
            this.leadershipVoteLabel.Size = new Size(300, this.headerLabelsImage.Height);
            this.leadershipVoteLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.leadershipVoteLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabelsImage.addControl(this.leadershipVoteLabel);
            InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Faction_Officers", "Faction Officers"));
            this.inviteButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.inviteButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.inviteButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.inviteButton.Position = new Point(20, height - 30);
            this.inviteButton.Text.Text = SK.Text("FactionsPanel_Invite_User", "Invite User");
            this.inviteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.inviteButton.TextYOffset = -3;
            this.inviteButton.Text.Color = ARGBColors.Black;
            this.inviteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.inviteClick), "FactionOfficersPanel_invite");
            this.mainBackgroundImage.addControl(this.inviteButton);
            this.wallScrollArea.Position = new Point(0x19, 0xbc);
            this.wallScrollArea.Size = new Size(0x2c1, ((height - 50) - 150) - 40);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x2c1, ((height - 50) - 150) - 40));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            this.flagimage.createFromFlagData(yourFaction.flagData);
            this.flagimage.Position = new Point(0x23, 6);
            this.flagimage.Scale = 0.5;
            this.flagimage.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
            if (num2 == 1)
            {
                this.flagimage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editClicked), "FactionOfficersPanel_edit");
            }
            else
            {
                this.flagimage.setClickDelegate(null);
            }
            this.mainBackgroundImage.addControl(this.flagimage);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Visible = false;
            this.wallScrollBar.Position = new Point(0x2dd, 0xbc);
            this.wallScrollBar.Size = new Size(0x18, ((height - 50) - 150) - 40);
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            bool uptodate = false;
            FactionMemberData[] fmd = GameEngine.Instance.World.getFactionMemberData(yourFaction.factionID, ref uptodate);
            if (!resized && !uptodate)
            {
                RemoteServices.Instance.set_GetViewFactionData_UserCallBack(new RemoteServices.GetViewFactionData_UserCallBack(this.getViewFactionDataCallback));
                RemoteServices.Instance.GetViewFactionData(yourFaction.factionID);
            }
            this.addPlayers(fmd);
        }

        private void InitializeComponent()
        {
            this.tbInviteName = new TextBox();
            base.SuspendLayout();
            this.tbInviteName.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.tbInviteName.Location = new Point(0xbc, 0x21b);
            this.tbInviteName.Name = "tbInviteName";
            this.tbInviteName.Size = new Size(0xf5, 20);
            this.tbInviteName.TabIndex = 7;
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.tbInviteName);
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "FactionOfficersPanel";
            base.Size = new Size(0x3e0, 0x236);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public void inviteClick()
        {
            if (this.tbInviteName.Text.Length > 0)
            {
                this.inviteToFaction(this.tbInviteName.Text);
            }
        }

        public void inviteToFaction(string username)
        {
            this.invitedUserName = username;
            RemoteServices.Instance.set_FactionSendInvite_UserCallBack(new RemoteServices.FactionSendInvite_UserCallBack(this.factionSendInviteCallback));
            RemoteServices.Instance.FactionSendInvite(username);
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

        public void setApplicationModeClicked()
        {
            FactionData yourFaction = GameEngine.Instance.World.YourFaction;
            if (yourFaction == null)
            {
                yourFaction = new FactionData();
            }
            this.applicationButton.Enabled = false;
            RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
            RemoteServices.Instance.FactionApplicationSetMode(!yourFaction.openForApplications);
        }

        public void update()
        {
            this.sidebar.update();
            if (this.tbInviteName.Text.Length == 0)
            {
                this.inviteButton.Enabled = false;
            }
            else
            {
                this.inviteButton.Enabled = true;
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0xbc - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class FactionMemberLineOfficer : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDButton acceptButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton declineButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDButton dismissButton = new CustomSelfDrawPanel.CSDButton();
            private FactionMemberData m_factionMemberData;
            private FactionOfficersPanel m_parent;
            private int m_position = -1000;
            private CustomSelfDrawPanel.CSDImage officerImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel pendingLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel playerName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton promoteButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDCheckBox voteCheck = new CustomSelfDrawPanel.CSDCheckBox();

            public void acceptAppClicked()
            {
                this.declineButton.Enabled = false;
                RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
                RemoteServices.Instance.FactionApplicationAccept(this.m_factionMemberData.userID);
            }

            public void changeRank(int rank)
            {
                RemoteServices.Instance.set_FactionChangeMemberStatus_UserCallBack(new RemoteServices.FactionChangeMemberStatus_UserCallBack(this.factionChangeMemberStatusCallback));
                RemoteServices.Instance.FactionChangeMemberStatus(this.m_factionMemberData.userID, rank);
            }

            public void checkToggled()
            {
                if (this.voteCheck.Checked)
                {
                    this.voteLeaderChange(this.m_factionMemberData.userID);
                }
            }

            public void clickedLine()
            {
                GameEngine.Instance.playInterfaceSound("FactionOfficersPanel_user_clicked");
                WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                    userID = this.m_factionMemberData.userID
                };
                InterfaceMgr.Instance.showUserInfoScreen(userInfo);
            }

            public void declineAppClicked()
            {
                this.declineButton.Enabled = false;
                RemoteServices.Instance.set_FactionApplicationProcessing_UserCallBack(new RemoteServices.FactionApplicationProcessing_UserCallBack(this.factionApplicationProcessingCallback));
                RemoteServices.Instance.FactionApplicationReject(this.m_factionMemberData.userID);
            }

            public void declineClicked()
            {
                this.declineButton.Enabled = false;
                RemoteServices.Instance.set_FactionWithdrawInvite_UserCallBack(new RemoteServices.FactionWithdrawInvite_UserCallBack(this.factionWithdrawInviteCallback));
                RemoteServices.Instance.FactionWithdrawInvite(this.m_factionMemberData.userID);
            }

            public void dismissMember()
            {
                this.dismissButton.Enabled = false;
                RemoteServices.Instance.set_FactionChangeMemberStatus_UserCallBack(new RemoteServices.FactionChangeMemberStatus_UserCallBack(this.factionChangeMemberStatusCallback));
                RemoteServices.Instance.FactionChangeMemberStatus(this.m_factionMemberData.userID, -2);
            }

            public void factionApplicationProcessingCallback(FactionApplicationProcessing_ReturnType returnData)
            {
                if (returnData.m_errorCode == ErrorCodes.ErrorCode.FACTION_FULL)
                {
                    MyMessageBox.Show(SK.Text("FactionsPanel_Faction_Full", "The Faction is full."), SK.Text("GENERIC_Error", "Error"));
                }
                this.declineButton.Enabled = true;
                if (returnData.members != null)
                {
                    GameEngine.Instance.World.FactionMembers = returnData.members;
                    GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                    this.m_parent.init(false);
                }
            }

            public void factionChangeMemberStatusCallback(FactionChangeMemberStatus_ReturnType returnData)
            {
                if (returnData.Success)
                {
                    GameEngine.Instance.World.FactionMembers = returnData.members;
                    GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                    this.m_parent.init(false);
                }
                else
                {
                    this.promoteButton.Enabled = true;
                    this.dismissButton.Enabled = true;
                }
            }

            public void factionLeadershipVoteCallback(FactionLeadershipVote_ReturnType returnData)
            {
                if (returnData.Success)
                {
                    GameEngine.Instance.World.YourFactionVote = returnData.yourLeaderVote;
                    if (returnData.leaderChanged)
                    {
                        RemoteServices.Instance.UserFactionID = returnData.yourFaction.factionID;
                        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
                        GameEngine.Instance.World.FactionMembers = returnData.members;
                        GameEngine.Instance.World.FactionInvites = returnData.invites;
                        GameEngine.Instance.World.FactionApplications = returnData.applications;
                    }
                    this.m_parent.init(false);
                }
            }

            public void factionWithdrawInviteCallback(FactionWithdrawInvite_ReturnType returnData)
            {
                this.declineButton.Enabled = true;
                if (returnData.members != null)
                {
                    GameEngine.Instance.World.FactionMembers = returnData.members;
                    this.m_parent.init(false);
                }
            }

            public void init(FactionMemberData factionData, int position, FactionOfficersPanel parent, bool ownFaction, int numOfficers)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.m_factionMemberData = factionData;
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
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                NumberFormatInfo nFI = GameEngine.NFI;
                if ((factionData.status == 1) || (factionData.status == 2))
                {
                    if (factionData.status == 1)
                    {
                        this.officerImage.Image = (Image) GFXLibrary.faction_leaders[1];
                        this.officerImage.CustomTooltipID = 0x901;
                    }
                    else
                    {
                        this.officerImage.Image = (Image) GFXLibrary.faction_leaders[0];
                        this.officerImage.CustomTooltipID = 0x902;
                    }
                    this.officerImage.Position = new Point(9, 2);
                    this.officerImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.officerImage);
                }
                this.playerName.Text = factionData.userName;
                this.playerName.Color = ARGBColors.Black;
                this.playerName.Position = new Point(0x27, 0);
                this.playerName.Size = new Size(280, this.backgroundImage.Height);
                this.playerName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                this.playerName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.playerName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                this.backgroundImage.addControl(this.playerName);
                if (factionData.status == -1)
                {
                    this.pendingLabel.Text = SK.Text("FactionsInvites_Invite_Pending", "Invitation Pending");
                    this.pendingLabel.Color = ARGBColors.DarkRed;
                    this.pendingLabel.Position = new Point(300, 0);
                    this.pendingLabel.Size = new Size(500, this.backgroundImage.Height);
                    this.pendingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.pendingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.pendingLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.pendingLabel);
                    switch (GameEngine.Instance.World.getYourFactionRank())
                    {
                        case 1:
                        case 2:
                            this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                            this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                            this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                            this.declineButton.Position = new Point(0x20d, 0);
                            this.declineButton.Text.Text = SK.Text("FactionMemberLine_Cancel_Invite", "Cancel Invite");
                            this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                            this.declineButton.TextYOffset = -3;
                            this.declineButton.Text.Color = ARGBColors.Black;
                            this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineClicked), "FactionOfficersPanel_decline");
                            this.backgroundImage.addControl(this.declineButton);
                            break;
                    }
                }
                else if (factionData.status == -3)
                {
                    this.pendingLabel.Text = SK.Text("FactionsInvites_Application", "Application");
                    this.pendingLabel.Color = ARGBColors.DarkRed;
                    this.pendingLabel.Position = new Point(270, 0);
                    this.pendingLabel.Size = new Size(500, this.backgroundImage.Height);
                    this.pendingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.pendingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.pendingLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                    this.backgroundImage.addControl(this.pendingLabel);
                    switch (GameEngine.Instance.World.getYourFactionRank())
                    {
                        case 1:
                        case 2:
                            this.acceptButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                            this.acceptButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                            this.acceptButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                            this.acceptButton.Position = new Point(370, 0);
                            this.acceptButton.Text.Text = SK.Text("FactionInviteLine_Accept", "Accept");
                            this.acceptButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            this.acceptButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                            this.acceptButton.TextYOffset = -3;
                            this.acceptButton.Text.Color = ARGBColors.Black;
                            this.acceptButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.acceptAppClicked), "FactionOfficersPanel_decline");
                            this.backgroundImage.addControl(this.acceptButton);
                            this.declineButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                            this.declineButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                            this.declineButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                            this.declineButton.Position = new Point(0x20d, 0);
                            this.declineButton.Text.Text = SK.Text("FactionInviteLine_Decline", "Decline");
                            this.declineButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            this.declineButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                            this.declineButton.TextYOffset = -3;
                            this.declineButton.Text.Color = ARGBColors.Black;
                            this.declineButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.declineAppClicked), "FactionOfficersPanel_decline");
                            this.backgroundImage.addControl(this.declineButton);
                            break;
                    }
                }
                else
                {
                    int num3 = GameEngine.Instance.World.getYourFactionRank();
                    if (factionData.status != 1)
                    {
                        if (num3 == 1)
                        {
                            this.promoteButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                            this.promoteButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                            this.promoteButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                            this.promoteButton.Position = new Point(300, 0);
                            this.promoteButton.Text.Text = SK.Text("FactionMemberLine_Cancel_Invite", "Cancel Invite");
                            if (factionData.status == 0)
                            {
                                this.promoteButton.Text.Text = SK.Text("FactionMemberLine_Promote_To_Officer", "Promote To Officer");
                                if (numOfficers >= GameEngine.Instance.LocalWorldData.Faction_MaxSergeants)
                                {
                                    this.promoteButton.Enabled = false;
                                }
                            }
                            else
                            {
                                this.promoteButton.Text.Text = SK.Text("FactionMemberLine_Demote_To_Commoner", "Demote To Commoner");
                            }
                            this.promoteButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            this.promoteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                            this.promoteButton.TextYOffset = -3;
                            this.promoteButton.Text.Color = ARGBColors.Black;
                            this.promoteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.promoteClicked), "FactionOfficersPanel_promote");
                            this.backgroundImage.addControl(this.promoteButton);
                        }
                        if (factionData.status == 0)
                        {
                            this.dismissButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                            this.dismissButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                            this.dismissButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                            this.dismissButton.Position = new Point(0x20d, 0);
                            this.dismissButton.Text.Text = SK.Text("FactionMemberLine_Dismiss", "Dismiss");
                            this.dismissButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                            this.dismissButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                            this.dismissButton.TextYOffset = -3;
                            this.dismissButton.Text.Color = ARGBColors.Black;
                            this.dismissButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.dismissMember), "FactionOfficersPanel_dismiss");
                            this.backgroundImage.addControl(this.dismissButton);
                        }
                    }
                    if ((factionData.status == 1) || (factionData.status == 2))
                    {
                        this.voteCheck.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                        this.voteCheck.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                        this.voteCheck.Position = new Point(0x249, 5);
                        if ((factionData.userID == GameEngine.Instance.World.YourFactionVote) || ((GameEngine.Instance.World.YourFactionVote == -1) && (factionData.status == 1)))
                        {
                            this.voteCheck.Checked = true;
                        }
                        else
                        {
                            this.voteCheck.Checked = false;
                            this.voteCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
                        }
                        this.backgroundImage.addControl(this.voteCheck);
                    }
                }
                base.invalidate();
            }

            public void promoteClicked()
            {
                this.promoteButton.Enabled = false;
                if (this.m_factionMemberData.status == 0)
                {
                    this.changeRank(2);
                }
                else
                {
                    this.changeRank(0);
                }
            }

            public void update()
            {
            }

            private void voteLeaderChange(int userID)
            {
                RemoteServices.Instance.set_FactionLeadershipVote_UserCallBack(new RemoteServices.FactionLeadershipVote_UserCallBack(this.factionLeadershipVoteCallback));
                RemoteServices.Instance.FactionLeadershipVote(RemoteServices.Instance.UserFactionID, userID);
            }
        }
    }
}

