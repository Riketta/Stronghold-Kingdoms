namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Windows.Forms;

    public class UserInfoScreen : UserControl, IDockableControl
    {
        private BitmapButton btnAchievements;
        private BitmapButton btnApplyGold;
        private BitmapButton btnApplyHonour;
        private BitmapButton btnApplyRP;
        private BitmapButton btnChatBan1;
        private BitmapButton btnChatBan3;
        private BitmapButton btnChatBan7;
        private BitmapButton btnChatBanClear;
        private BitmapButton btnChatBanPerma;
        private BitmapButton btnClose;
        private BitmapButton btnEditAvatar;
        private BitmapButton btnFixAchievements;
        private BitmapButton btnFlushCaches;
        private BitmapButton btnForumBan1;
        private BitmapButton btnForumBan3;
        private BitmapButton btnForumBan7;
        private BitmapButton btnForumBanClear;
        private BitmapButton btnForumBanPerma;
        private BitmapButton btnGiveQuests;
        private BitmapButton btnInviteToFaction;
        private BitmapButton btnKick;
        private BitmapButton btnMailBan1;
        private BitmapButton btnMailBan3;
        private BitmapButton btnMailBan7;
        private BitmapButton btnMailBanClear;
        private BitmapButton btnMailBanPerma;
        private BitmapButton btnMakeModerator;
        private BitmapButton btnRemoveModerator;
        private BitmapButton btnSendMail;
        private BitmapButton btnWalBan1;
        private BitmapButton btnWalBan3;
        private BitmapButton btnWalBan7;
        private BitmapButton btnWalBanClear;
        private BitmapButton btnWalBanPerma;
        private IContainer components;
        private DockableControl dockableControl;
        private GroupBox gbModerator;
        private UserControl imgAvatar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblFaction;
        private Label lblGold;
        private Label lblHonour;
        private Label lblIsAdmin;
        private Label lblIsModerator;
        private Label lblPoints;
        private Label lblRank;
        private Label lblRP;
        private Label lblStanding;
        private Label lblUserName;
        private List<UserinfoScreenLine> lineList = new List<UserinfoScreenLine>();
        private string m_reasonString = "";
        private int m_userID = -1;
        private WorldMap.CachedUserInfo m_userInfo;
        private int[] m_villages;
        private Panel pnlVillages;
        private TextBox tbGold;
        private TextBox tbHonour;
        private TextBox tbRP;
        private TextBox tbStuff;

        public UserInfoScreen()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            this.lblUserName.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, FontStyle.Bold);
            this.lblRank.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.lblPoints.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.lblStanding.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.label3.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.label2.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.lblFaction.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            this.label5.Font = FontManager.GetFont("Microsoft Sans Serif", 10f);
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void addVillages(int[] villages)
        {
            int y = this.pnlVillages.AutoScrollPosition.Y;
            this.lineList.Clear();
            this.pnlVillages.SuspendLayout();
            this.pnlVillages.Controls.Clear();
            int num = 0;
            if (villages != null)
            {
                foreach (int num2 in villages)
                {
                    UserinfoScreenLine line = new UserinfoScreenLine {
                        Location = new Point(0, num)
                    };
                    line.init(GameEngine.Instance.World.getVillageName(num2), num2);
                    this.pnlVillages.Controls.Add(line);
                    num += line.Height;
                    this.lineList.Add(line);
                }
            }
            this.pnlVillages.ResumeLayout(false);
            this.pnlVillages.PerformLayout();
        }

        private bool areEqual(int[] villages1, int[] villages2)
        {
            if ((villages1 != null) || (villages2 != null))
            {
                if ((villages1 == null) || (villages2 == null))
                {
                    return false;
                }
                if (villages1.Length != villages2.Length)
                {
                    return false;
                }
                List<int> list = new List<int>();
                list.AddRange(villages2);
                foreach (int num in villages1)
                {
                    if (!list.Contains(num))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void bitmapButton1_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_edit_avatar");
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
        }

        private void btnAchievements_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_achievements");
            InterfaceMgr.Instance.openAchievements(this.m_userInfo.achievements);
        }

        private void btnApplyGold_Click(object sender, EventArgs e)
        {
            if (RemoteServices.Instance.Admin)
            {
                int duration = getInt32FromString(this.tbGold.Text);
                if ((duration > 0) && (duration < 0xf4240))
                {
                    this.sendCommandToServer(0x15, duration);
                }
                else
                {
                    MyMessageBox.Show("Out of range", "Admin Error");
                }
            }
        }

        private void btnApplyHonour_Click(object sender, EventArgs e)
        {
            if (RemoteServices.Instance.Admin)
            {
                int duration = getInt32FromString(this.tbHonour.Text);
                if ((duration > 0) && (duration < 0x989680))
                {
                    this.sendCommandToServer(0x16, duration);
                }
                else
                {
                    MyMessageBox.Show("Out of range", "Admin Error");
                }
            }
        }

        private void btnApplyRP_Click(object sender, EventArgs e)
        {
            if (RemoteServices.Instance.Admin)
            {
                int duration = getInt32FromString(this.tbRP.Text);
                if ((duration > 0) && (duration < 5))
                {
                    this.sendCommandToServer(0x17, duration);
                }
                else
                {
                    MyMessageBox.Show("Out of range", "Admin Error");
                }
            }
        }

        private void btnChatBan1_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(1, 1);
        }

        private void btnChatBan3_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(1, 3);
        }

        private void btnChatBan7_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(1, 7);
        }

        private void btnChatBanClear_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(1, -1);
        }

        private void btnChatBanPerma_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(1, 0xe42);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_close");
            InterfaceMgr.Instance.closeParishPanel();
        }

        private void btnFixAchievements_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(0x2a, 0);
        }

        private void btnFlushCaches_Click(object sender, EventArgs e)
        {
            if (RemoteServices.Instance.Admin)
            {
                this.sendCommandToServer(0x1f, 0);
            }
        }

        private void btnForumBan1_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(3, 1);
        }

        private void btnForumBan3_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(3, 3);
        }

        private void btnForumBan7_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(3, 7);
        }

        private void btnForumBanClear_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(3, -1);
        }

        private void btnForumBanPerma_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(3, 0xe42);
        }

        private void btnGiveQuests_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(0x2b, 0);
        }

        private void btnInviteToFaction_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction_invite");
            InterfaceMgr.Instance.clearControls();
            InterfaceMgr.Instance.inviteToFaction(this.m_userInfo.userName);
        }

        private void btnKick_Click(object sender, EventArgs e)
        {
            RemoteServices.Instance.Chat_Admin_Command(5, this.m_userID);
        }

        private void btnMailBan1_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(2, 1);
        }

        private void btnMailBan3_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(2, 3);
        }

        private void btnMailBan7_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(2, 7);
        }

        private void btnMailBanClear_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(2, -1);
        }

        private void btnMailBanPerma_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(2, 0xe42);
        }

        private void btnMakeModerator_Click(object sender, EventArgs e)
        {
        }

        private void btnRemoveModerator_Click(object sender, EventArgs e)
        {
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("UserInfoScreen_send_mail");
            if (this.m_userInfo != null)
            {
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x15);
                InterfaceMgr.Instance.mailTo(this.m_userInfo.userID, this.m_userInfo.userName);
            }
        }

        private void btnWalBan1_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(4, 1);
        }

        private void btnWalBan3_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(4, 3);
        }

        private void btnWalBan7_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(4, 7);
        }

        private void btnWalBanClear_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(4, -1);
        }

        private void btnWalBanPerma_Click(object sender, EventArgs e)
        {
            this.sendCommandToServer(4, 0xe42);
        }

        public void clear()
        {
            this.m_villages = null;
            this.m_userInfo = null;
            this.imgAvatar.BackgroundImage = null;
            this.lblFaction.ForeColor = ARGBColors.Black;
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
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

        public static int getInt32FromString(string text)
        {
            if (text.Length != 0)
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch (Exception)
                {
                }
            }
            return 0;
        }

        public void init(WorldMap.CachedUserInfo userInfo)
        {
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.lblUserName.Text = "";
            this.lblRank.Text = "";
            this.label1.Text = SK.Text("GENERIC_Points", "Points") + " : ";
            this.label3.Text = SK.Text("UserInfoScreen_Rank", "Rank") + " : ";
            this.label2.Text = SK.Text("UserInfoScreen_Villages", "Villages");
            this.label5.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction") + " : ";
            this.btnSendMail.Text = SK.Text("UserInfoScreen_Send_Mail", "Send Mail");
            this.btnAchievements.Text = SK.Text("GENERIC_Achievements", "Achievements");
            this.btnInviteToFaction.Text = SK.Text("UserInfoScreen_InviteToFaction", "Invite To Faction");
            this.btnEditAvatar.Text = SK.Text("MENU_Edit_Avatar", "Edit Avatar");
            if (userInfo == null)
            {
                userInfo = new WorldMap.CachedUserInfo();
                userInfo.userID = this.m_userID;
            }
            this.m_userID = userInfo.userID;
            NumberFormatInfo nFI = GameEngine.NFI;
            WorldMap.VillageRolloverInfo villageInfo = null;
            GameEngine.Instance.World.retrieveUserData(-1, userInfo.userID, ref villageInfo, ref userInfo, true, true);
            this.btnEditAvatar.Visible = this.m_userID == RemoteServices.Instance.UserID;
            this.m_userInfo = userInfo;
            if (userInfo != null)
            {
                this.lblUserName.Text = userInfo.userName;
                this.lblPoints.Text = userInfo.points.ToString("N", nFI);
                if (userInfo.standing >= 0)
                {
                    this.lblStanding.Text = userInfo.standing.ToString("N", nFI);
                }
                else
                {
                    this.lblStanding.Text = "?";
                }
                if (userInfo.avatarData != null)
                {
                    this.lblRank.Text = Rankings.getRankingName(userInfo.rank, userInfo.avatarData.male);
                }
                else
                {
                    this.lblRank.Text = Rankings.getRankingName(userInfo.rank);
                }
                if (userInfo.factionID >= 0)
                {
                    FactionData data = GameEngine.Instance.World.getFaction(userInfo.factionID);
                    if (data != null)
                    {
                        this.lblFaction.Text = data.factionNameAbrv;
                    }
                    else
                    {
                        this.lblFaction.Text = "";
                    }
                }
                else
                {
                    this.lblFaction.Text = "";
                }
                if ((userInfo.avatarData != null) && (this.imgAvatar.BackgroundImage == null))
                {
                    this.imgAvatar.BackgroundImage = Avatar.CreateAvatar(userInfo.avatarData, ARGBColors.Transparent);
                }
                if (!this.areEqual(userInfo.villages, this.m_villages))
                {
                    this.m_villages = userInfo.villages;
                    this.addVillages(this.m_villages);
                }
                if (!RemoteServices.Instance.Admin && !RemoteServices.Instance.Moderator)
                {
                    this.lblIsAdmin.Visible = false;
                    this.lblIsModerator.Visible = false;
                    this.gbModerator.Visible = false;
                }
                else
                {
                    this.gbModerator.Visible = true;
                    this.lblIsAdmin.Visible = RemoteServices.Instance.Admin;
                    this.lblIsModerator.Visible = RemoteServices.Instance.Moderator;
                    this.btnChatBan7.Visible = RemoteServices.Instance.Admin;
                    this.btnChatBanPerma.Visible = RemoteServices.Instance.Admin;
                    this.btnMailBanClear.Visible = RemoteServices.Instance.Admin;
                    this.btnMailBan1.Visible = RemoteServices.Instance.Admin;
                    this.btnMailBan3.Visible = RemoteServices.Instance.Admin;
                    this.btnMailBan7.Visible = RemoteServices.Instance.Admin;
                    this.btnMailBanPerma.Visible = RemoteServices.Instance.Admin;
                    this.btnForumBan7.Visible = RemoteServices.Instance.Admin;
                    this.btnForumBanPerma.Visible = RemoteServices.Instance.Admin;
                    this.btnWalBan7.Visible = RemoteServices.Instance.Admin;
                    this.btnWalBanPerma.Visible = RemoteServices.Instance.Admin;
                    this.btnMakeModerator.Visible = false;
                    this.btnRemoveModerator.Visible = false;
                    this.lblGold.Visible = RemoteServices.Instance.Admin;
                    this.lblHonour.Visible = RemoteServices.Instance.Admin;
                    this.lblRP.Visible = RemoteServices.Instance.Admin;
                    this.tbGold.Visible = RemoteServices.Instance.Admin;
                    this.tbHonour.Visible = RemoteServices.Instance.Admin;
                    this.tbRP.Visible = RemoteServices.Instance.Admin;
                    this.btnApplyGold.Visible = RemoteServices.Instance.Admin;
                    this.btnApplyHonour.Visible = RemoteServices.Instance.Admin;
                    this.btnApplyRP.Visible = RemoteServices.Instance.Admin;
                    this.btnFlushCaches.Visible = RemoteServices.Instance.Admin;
                    if (RemoteServices.Instance.Admin)
                    {
                        this.tbStuff.Text = userInfo.stuff.Replace("-", "");
                    }
                    this.btnFixAchievements.Visible = RemoteServices.Instance.Admin;
                    this.btnGiveQuests.Visible = RemoteServices.Instance.Admin;
                }
            }
            else
            {
                this.lblUserName.Text = "";
                this.lblPoints.Text = "0";
                this.lblStanding.Text = "0";
                this.lblRank.Text = "";
                this.lblFaction.Text = "";
                this.imgAvatar.BackgroundImage = null;
                this.lblIsAdmin.Visible = false;
                this.lblIsModerator.Visible = false;
                this.gbModerator.Visible = false;
            }
            int num = GameEngine.Instance.World.getYourFactionRank();
            bool flag = false;
            if (((GameEngine.Instance.World.YourFaction != null) && (this.m_userInfo != null)) && (this.m_userInfo.userID != RemoteServices.Instance.UserID))
            {
                FactionMemberData[] factionMembers = GameEngine.Instance.World.FactionMembers;
                if (((factionMembers != null) && (num > 0)) && (factionMembers.Length < GameEngine.Instance.LocalWorldData.Faction_MaxMembers))
                {
                    flag = true;
                }
            }
            this.btnInviteToFaction.Visible = flag;
        }

        private void InitializeComponent()
        {
            this.pnlVillages = new Panel();
            this.lblUserName = new Label();
            this.lblRank = new Label();
            this.label1 = new Label();
            this.lblPoints = new Label();
            this.lblStanding = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.lblFaction = new Label();
            this.label5 = new Label();
            this.imgAvatar = new UserControl();
            this.lblIsAdmin = new Label();
            this.lblIsModerator = new Label();
            this.gbModerator = new GroupBox();
            this.btnFixAchievements = new BitmapButton();
            this.tbStuff = new TextBox();
            this.btnKick = new BitmapButton();
            this.btnFlushCaches = new BitmapButton();
            this.btnApplyRP = new BitmapButton();
            this.tbRP = new TextBox();
            this.lblRP = new Label();
            this.btnApplyHonour = new BitmapButton();
            this.btnApplyGold = new BitmapButton();
            this.tbHonour = new TextBox();
            this.tbGold = new TextBox();
            this.lblHonour = new Label();
            this.lblGold = new Label();
            this.btnRemoveModerator = new BitmapButton();
            this.btnMakeModerator = new BitmapButton();
            this.btnWalBanClear = new BitmapButton();
            this.btnWalBanPerma = new BitmapButton();
            this.btnWalBan7 = new BitmapButton();
            this.btnWalBan3 = new BitmapButton();
            this.label9 = new Label();
            this.btnWalBan1 = new BitmapButton();
            this.btnForumBanClear = new BitmapButton();
            this.btnForumBanPerma = new BitmapButton();
            this.btnForumBan7 = new BitmapButton();
            this.btnForumBan3 = new BitmapButton();
            this.label8 = new Label();
            this.btnForumBan1 = new BitmapButton();
            this.btnMailBanClear = new BitmapButton();
            this.btnMailBanPerma = new BitmapButton();
            this.btnMailBan7 = new BitmapButton();
            this.btnMailBan3 = new BitmapButton();
            this.label7 = new Label();
            this.btnMailBan1 = new BitmapButton();
            this.btnChatBanClear = new BitmapButton();
            this.btnChatBanPerma = new BitmapButton();
            this.btnChatBan7 = new BitmapButton();
            this.btnChatBan3 = new BitmapButton();
            this.label6 = new Label();
            this.btnChatBan1 = new BitmapButton();
            this.label4 = new Label();
            this.btnEditAvatar = new BitmapButton();
            this.btnInviteToFaction = new BitmapButton();
            this.btnAchievements = new BitmapButton();
            this.btnSendMail = new BitmapButton();
            this.btnClose = new BitmapButton();
            this.btnGiveQuests = new BitmapButton();
            this.gbModerator.SuspendLayout();
            base.SuspendLayout();
            this.pnlVillages.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pnlVillages.AutoScroll = true;
            this.pnlVillages.BackColor = Color.FromArgb(0x60, 0x6d, 0x76);
            this.pnlVillages.Location = new Point(0x17, 0x13d);
            this.pnlVillages.Name = "pnlVillages";
            this.pnlVillages.Size = new Size(0x150, 0xdd);
            this.pnlVillages.TabIndex = 9;
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblUserName.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblUserName.Location = new Point(0x24, 0x13);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new Size(0x5d, 20);
            this.lblUserName.TabIndex = 10;
            this.lblUserName.Text = "UserName";
            this.lblUserName.MouseClick += new MouseEventHandler(this.lblUserName_MouseClick);
            this.lblRank.AutoSize = true;
            this.lblRank.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblRank.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblRank.Location = new Point(0x25, 0x31);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new Size(0x2b, 0x11);
            this.lblRank.TabIndex = 11;
            this.lblRank.Text = "name";
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x25, 0x6c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 0x11);
            this.label1.TabIndex = 12;
            this.label1.Text = "Points : ";
            this.lblPoints.AutoSize = true;
            this.lblPoints.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblPoints.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblPoints.Location = new Point(0x71, 0x6c);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new Size(0x10, 0x11);
            this.lblPoints.TabIndex = 13;
            this.lblPoints.Text = "0";
            this.lblStanding.AutoSize = true;
            this.lblStanding.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblStanding.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblStanding.Location = new Point(0x71, 0x94);
            this.lblStanding.Name = "lblStanding";
            this.lblStanding.Size = new Size(0x10, 0x11);
            this.lblStanding.TabIndex = 15;
            this.lblStanding.Text = "0";
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(0x25, 0x94);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x31, 0x11);
            this.label3.TabIndex = 14;
            this.label3.Text = "Rank :";
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(0x1d, 0x129);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x39, 0x11);
            this.label2.TabIndex = 0x10;
            this.label2.Text = "Villages";
            this.lblFaction.AutoSize = true;
            this.lblFaction.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblFaction.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Underline, GraphicsUnit.Point, 0);
            this.lblFaction.Location = new Point(0x71, 0xeb);
            this.lblFaction.Name = "lblFaction";
            this.lblFaction.Size = new Size(0x18, 0x11);
            this.lblFaction.TabIndex = 0x12;
            this.lblFaction.Text = "....";
            this.lblFaction.MouseLeave += new EventHandler(this.lblFaction_MouseLeave);
            this.lblFaction.MouseClick += new MouseEventHandler(this.lblFaction_MouseClick);
            this.lblFaction.MouseEnter += new EventHandler(this.lblFaction_MouseEnter);
            this.label5.AutoSize = true;
            this.label5.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(0x25, 0xeb);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3e, 0x11);
            this.label5.TabIndex = 0x11;
            this.label5.Text = "Faction :";
            this.imgAvatar.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.imgAvatar.Location = new Point(0x268, 0x13);
            this.imgAvatar.Name = "imgAvatar";
            this.imgAvatar.Size = new Size(0x9a, 500);
            this.imgAvatar.TabIndex = 0x13;
            this.lblIsAdmin.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblIsAdmin.AutoSize = true;
            this.lblIsAdmin.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblIsAdmin.Location = new Point(0x181, 0xd5);
            this.lblIsAdmin.Name = "lblIsAdmin";
            this.lblIsAdmin.Size = new Size(0x2f, 13);
            this.lblIsAdmin.TabIndex = 0x15;
            this.lblIsAdmin.Text = "Is Admin";
            this.lblIsAdmin.Visible = false;
            this.lblIsModerator.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblIsModerator.AutoSize = true;
            this.lblIsModerator.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblIsModerator.Location = new Point(0x1b6, 0xd5);
            this.lblIsModerator.Name = "lblIsModerator";
            this.lblIsModerator.Size = new Size(0x42, 13);
            this.lblIsModerator.TabIndex = 0x16;
            this.lblIsModerator.Text = "Is Moderator";
            this.lblIsModerator.Visible = false;
            this.gbModerator.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.gbModerator.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.gbModerator.Controls.Add(this.btnGiveQuests);
            this.gbModerator.Controls.Add(this.btnFixAchievements);
            this.gbModerator.Controls.Add(this.tbStuff);
            this.gbModerator.Controls.Add(this.btnKick);
            this.gbModerator.Controls.Add(this.btnFlushCaches);
            this.gbModerator.Controls.Add(this.btnApplyRP);
            this.gbModerator.Controls.Add(this.tbRP);
            this.gbModerator.Controls.Add(this.lblRP);
            this.gbModerator.Controls.Add(this.btnApplyHonour);
            this.gbModerator.Controls.Add(this.btnApplyGold);
            this.gbModerator.Controls.Add(this.tbHonour);
            this.gbModerator.Controls.Add(this.tbGold);
            this.gbModerator.Controls.Add(this.lblHonour);
            this.gbModerator.Controls.Add(this.lblGold);
            this.gbModerator.Controls.Add(this.btnRemoveModerator);
            this.gbModerator.Controls.Add(this.btnMakeModerator);
            this.gbModerator.Controls.Add(this.btnWalBanClear);
            this.gbModerator.Controls.Add(this.btnWalBanPerma);
            this.gbModerator.Controls.Add(this.btnWalBan7);
            this.gbModerator.Controls.Add(this.btnWalBan3);
            this.gbModerator.Controls.Add(this.label9);
            this.gbModerator.Controls.Add(this.btnWalBan1);
            this.gbModerator.Controls.Add(this.btnForumBanClear);
            this.gbModerator.Controls.Add(this.btnForumBanPerma);
            this.gbModerator.Controls.Add(this.btnForumBan7);
            this.gbModerator.Controls.Add(this.btnForumBan3);
            this.gbModerator.Controls.Add(this.label8);
            this.gbModerator.Controls.Add(this.btnForumBan1);
            this.gbModerator.Controls.Add(this.btnMailBanClear);
            this.gbModerator.Controls.Add(this.btnMailBanPerma);
            this.gbModerator.Controls.Add(this.btnMailBan7);
            this.gbModerator.Controls.Add(this.btnMailBan3);
            this.gbModerator.Controls.Add(this.label7);
            this.gbModerator.Controls.Add(this.btnMailBan1);
            this.gbModerator.Controls.Add(this.btnChatBanClear);
            this.gbModerator.Controls.Add(this.btnChatBanPerma);
            this.gbModerator.Controls.Add(this.btnChatBan7);
            this.gbModerator.Controls.Add(this.btnChatBan3);
            this.gbModerator.Controls.Add(this.label6);
            this.gbModerator.Controls.Add(this.btnChatBan1);
            this.gbModerator.Controls.Add(this.label4);
            this.gbModerator.Location = new Point(0x16d, 0xe5);
            this.gbModerator.Name = "gbModerator";
            this.gbModerator.Size = new Size(0xf5, 0x145);
            this.gbModerator.TabIndex = 0x17;
            this.gbModerator.TabStop = false;
            this.gbModerator.Text = "Moderating Functions";
            this.gbModerator.Visible = false;
            this.btnFixAchievements.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnFixAchievements.BorderDrawing = true;
            this.btnFixAchievements.FocusRectangleEnabled = false;
            this.btnFixAchievements.Image = null;
            this.btnFixAchievements.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnFixAchievements.ImageBorderEnabled = true;
            this.btnFixAchievements.ImageDropShadow = true;
            this.btnFixAchievements.ImageFocused = null;
            this.btnFixAchievements.ImageInactive = null;
            this.btnFixAchievements.ImageMouseOver = null;
            this.btnFixAchievements.ImageNormal = null;
            this.btnFixAchievements.ImagePressed = null;
            this.btnFixAchievements.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnFixAchievements.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnFixAchievements.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnFixAchievements.Location = new Point(12, 0x12e);
            this.btnFixAchievements.Name = "btnFixAchievements";
            this.btnFixAchievements.OffsetPressedContent = true;
            this.btnFixAchievements.Padding2 = 5;
            this.btnFixAchievements.Size = new Size(0x6c, 0x17);
            this.btnFixAchievements.StretchImage = false;
            this.btnFixAchievements.TabIndex = 0x29;
            this.btnFixAchievements.Text = "Fix Achievements";
            this.btnFixAchievements.TextDropShadow = false;
            this.btnFixAchievements.UseVisualStyleBackColor = true;
            this.btnFixAchievements.Click += new EventHandler(this.btnFixAchievements_Click);
            this.tbStuff.Location = new Point(12, 280);
            this.tbStuff.Name = "tbStuff";
            this.tbStuff.ReadOnly = true;
            this.tbStuff.Size = new Size(0xe3, 20);
            this.tbStuff.TabIndex = 40;
            this.btnKick.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnKick.BorderDrawing = true;
            this.btnKick.FocusRectangleEnabled = false;
            this.btnKick.Image = null;
            this.btnKick.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnKick.ImageBorderEnabled = true;
            this.btnKick.ImageDropShadow = true;
            this.btnKick.ImageFocused = null;
            this.btnKick.ImageInactive = null;
            this.btnKick.ImageMouseOver = null;
            this.btnKick.ImageNormal = null;
            this.btnKick.ImagePressed = null;
            this.btnKick.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnKick.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnKick.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnKick.Location = new Point(0xd6, 0x1f);
            this.btnKick.Name = "btnKick";
            this.btnKick.OffsetPressedContent = true;
            this.btnKick.Padding2 = 5;
            this.btnKick.Size = new Size(0x19, 0x17);
            this.btnKick.StretchImage = false;
            this.btnKick.TabIndex = 0x27;
            this.btnKick.Text = "K";
            this.btnKick.TextDropShadow = false;
            this.btnKick.UseVisualStyleBackColor = true;
            this.btnKick.Click += new EventHandler(this.btnKick_Click);
            this.btnFlushCaches.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnFlushCaches.BorderDrawing = true;
            this.btnFlushCaches.FocusRectangleEnabled = false;
            this.btnFlushCaches.Image = null;
            this.btnFlushCaches.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnFlushCaches.ImageBorderEnabled = true;
            this.btnFlushCaches.ImageDropShadow = true;
            this.btnFlushCaches.ImageFocused = null;
            this.btnFlushCaches.ImageInactive = null;
            this.btnFlushCaches.ImageMouseOver = null;
            this.btnFlushCaches.ImageNormal = null;
            this.btnFlushCaches.ImagePressed = null;
            this.btnFlushCaches.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnFlushCaches.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnFlushCaches.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnFlushCaches.Location = new Point(0x2c, 0xfe);
            this.btnFlushCaches.Name = "btnFlushCaches";
            this.btnFlushCaches.OffsetPressedContent = true;
            this.btnFlushCaches.Padding2 = 5;
            this.btnFlushCaches.Size = new Size(0x97, 0x17);
            this.btnFlushCaches.StretchImage = false;
            this.btnFlushCaches.TabIndex = 0x26;
            this.btnFlushCaches.Text = "Flush Client Village Cache";
            this.btnFlushCaches.TextDropShadow = false;
            this.btnFlushCaches.UseVisualStyleBackColor = true;
            this.btnFlushCaches.Click += new EventHandler(this.btnFlushCaches_Click);
            this.btnApplyRP.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnApplyRP.BorderDrawing = true;
            this.btnApplyRP.FocusRectangleEnabled = false;
            this.btnApplyRP.Image = null;
            this.btnApplyRP.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnApplyRP.ImageBorderEnabled = true;
            this.btnApplyRP.ImageDropShadow = true;
            this.btnApplyRP.ImageFocused = null;
            this.btnApplyRP.ImageInactive = null;
            this.btnApplyRP.ImageMouseOver = null;
            this.btnApplyRP.ImageNormal = null;
            this.btnApplyRP.ImagePressed = null;
            this.btnApplyRP.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnApplyRP.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnApplyRP.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnApplyRP.Location = new Point(0xb6, 0xe4);
            this.btnApplyRP.Name = "btnApplyRP";
            this.btnApplyRP.OffsetPressedContent = true;
            this.btnApplyRP.Padding2 = 5;
            this.btnApplyRP.Size = new Size(0x39, 0x17);
            this.btnApplyRP.StretchImage = false;
            this.btnApplyRP.TabIndex = 0x25;
            this.btnApplyRP.Text = "Give";
            this.btnApplyRP.TextDropShadow = false;
            this.btnApplyRP.UseVisualStyleBackColor = true;
            this.btnApplyRP.Click += new EventHandler(this.btnApplyRP_Click);
            this.tbRP.Location = new Point(0x4c, 0xe5);
            this.tbRP.Name = "tbRP";
            this.tbRP.Size = new Size(100, 20);
            this.tbRP.TabIndex = 0x24;
            this.lblRP.AutoSize = true;
            this.lblRP.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblRP.Location = new Point(9, 0xe9);
            this.lblRP.Name = "lblRP";
            this.lblRP.Size = new Size(0x16, 13);
            this.lblRP.TabIndex = 0x23;
            this.lblRP.Text = "RP";
            this.btnApplyHonour.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnApplyHonour.BorderDrawing = true;
            this.btnApplyHonour.FocusRectangleEnabled = false;
            this.btnApplyHonour.Image = null;
            this.btnApplyHonour.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnApplyHonour.ImageBorderEnabled = true;
            this.btnApplyHonour.ImageDropShadow = true;
            this.btnApplyHonour.ImageFocused = null;
            this.btnApplyHonour.ImageInactive = null;
            this.btnApplyHonour.ImageMouseOver = null;
            this.btnApplyHonour.ImageNormal = null;
            this.btnApplyHonour.ImagePressed = null;
            this.btnApplyHonour.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnApplyHonour.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnApplyHonour.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnApplyHonour.Location = new Point(0xb6, 0xca);
            this.btnApplyHonour.Name = "btnApplyHonour";
            this.btnApplyHonour.OffsetPressedContent = true;
            this.btnApplyHonour.Padding2 = 5;
            this.btnApplyHonour.Size = new Size(0x39, 0x17);
            this.btnApplyHonour.StretchImage = false;
            this.btnApplyHonour.TabIndex = 0x22;
            this.btnApplyHonour.Text = "Give";
            this.btnApplyHonour.TextDropShadow = false;
            this.btnApplyHonour.UseVisualStyleBackColor = true;
            this.btnApplyHonour.Click += new EventHandler(this.btnApplyHonour_Click);
            this.btnApplyGold.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnApplyGold.BorderDrawing = true;
            this.btnApplyGold.FocusRectangleEnabled = false;
            this.btnApplyGold.Image = null;
            this.btnApplyGold.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnApplyGold.ImageBorderEnabled = true;
            this.btnApplyGold.ImageDropShadow = true;
            this.btnApplyGold.ImageFocused = null;
            this.btnApplyGold.ImageInactive = null;
            this.btnApplyGold.ImageMouseOver = null;
            this.btnApplyGold.ImageNormal = null;
            this.btnApplyGold.ImagePressed = null;
            this.btnApplyGold.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnApplyGold.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnApplyGold.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnApplyGold.Location = new Point(0xb6, 0xb0);
            this.btnApplyGold.Name = "btnApplyGold";
            this.btnApplyGold.OffsetPressedContent = true;
            this.btnApplyGold.Padding2 = 5;
            this.btnApplyGold.Size = new Size(0x39, 0x17);
            this.btnApplyGold.StretchImage = false;
            this.btnApplyGold.TabIndex = 0x21;
            this.btnApplyGold.Text = "Give";
            this.btnApplyGold.TextDropShadow = false;
            this.btnApplyGold.UseVisualStyleBackColor = true;
            this.btnApplyGold.Click += new EventHandler(this.btnApplyGold_Click);
            this.tbHonour.Location = new Point(0x4c, 0xcb);
            this.tbHonour.Name = "tbHonour";
            this.tbHonour.Size = new Size(100, 20);
            this.tbHonour.TabIndex = 0x20;
            this.tbGold.Location = new Point(0x4c, 0xb2);
            this.tbGold.Name = "tbGold";
            this.tbGold.Size = new Size(100, 20);
            this.tbGold.TabIndex = 0x1f;
            this.lblHonour.AutoSize = true;
            this.lblHonour.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblHonour.Location = new Point(9, 0xcf);
            this.lblHonour.Name = "lblHonour";
            this.lblHonour.Size = new Size(0x2a, 13);
            this.lblHonour.TabIndex = 30;
            this.lblHonour.Text = "Honour";
            this.lblGold.AutoSize = true;
            this.lblGold.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.lblGold.Location = new Point(9, 0xb5);
            this.lblGold.Name = "lblGold";
            this.lblGold.Size = new Size(0x1d, 13);
            this.lblGold.TabIndex = 0x1d;
            this.lblGold.Text = "Gold";
            this.btnRemoveModerator.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnRemoveModerator.BorderDrawing = true;
            this.btnRemoveModerator.FocusRectangleEnabled = false;
            this.btnRemoveModerator.Image = null;
            this.btnRemoveModerator.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnRemoveModerator.ImageBorderEnabled = true;
            this.btnRemoveModerator.ImageDropShadow = true;
            this.btnRemoveModerator.ImageFocused = null;
            this.btnRemoveModerator.ImageInactive = null;
            this.btnRemoveModerator.ImageMouseOver = null;
            this.btnRemoveModerator.ImageNormal = null;
            this.btnRemoveModerator.ImagePressed = null;
            this.btnRemoveModerator.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnRemoveModerator.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnRemoveModerator.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnRemoveModerator.Location = new Point(0x7f, 0x93);
            this.btnRemoveModerator.Name = "btnRemoveModerator";
            this.btnRemoveModerator.OffsetPressedContent = true;
            this.btnRemoveModerator.Padding2 = 5;
            this.btnRemoveModerator.Size = new Size(0x70, 0x17);
            this.btnRemoveModerator.StretchImage = false;
            this.btnRemoveModerator.TabIndex = 0x1c;
            this.btnRemoveModerator.Text = "Remove Moderator";
            this.btnRemoveModerator.TextDropShadow = false;
            this.btnRemoveModerator.UseVisualStyleBackColor = true;
            this.btnRemoveModerator.Click += new EventHandler(this.btnRemoveModerator_Click);
            this.btnMakeModerator.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnMakeModerator.BorderDrawing = true;
            this.btnMakeModerator.FocusRectangleEnabled = false;
            this.btnMakeModerator.Image = null;
            this.btnMakeModerator.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnMakeModerator.ImageBorderEnabled = true;
            this.btnMakeModerator.ImageDropShadow = true;
            this.btnMakeModerator.ImageFocused = null;
            this.btnMakeModerator.ImageInactive = null;
            this.btnMakeModerator.ImageMouseOver = null;
            this.btnMakeModerator.ImageNormal = null;
            this.btnMakeModerator.ImagePressed = null;
            this.btnMakeModerator.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnMakeModerator.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnMakeModerator.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnMakeModerator.Location = new Point(9, 0x93);
            this.btnMakeModerator.Name = "btnMakeModerator";
            this.btnMakeModerator.OffsetPressedContent = true;
            this.btnMakeModerator.Padding2 = 5;
            this.btnMakeModerator.Size = new Size(0x70, 0x17);
            this.btnMakeModerator.StretchImage = false;
            this.btnMakeModerator.TabIndex = 0x1a;
            this.btnMakeModerator.Text = "Make Moderator";
            this.btnMakeModerator.TextDropShadow = false;
            this.btnMakeModerator.UseVisualStyleBackColor = true;
            this.btnMakeModerator.Click += new EventHandler(this.btnMakeModerator_Click);
            this.btnWalBanClear.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnWalBanClear.BorderDrawing = true;
            this.btnWalBanClear.FocusRectangleEnabled = false;
            this.btnWalBanClear.Image = null;
            this.btnWalBanClear.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnWalBanClear.ImageBorderEnabled = true;
            this.btnWalBanClear.ImageDropShadow = true;
            this.btnWalBanClear.ImageFocused = null;
            this.btnWalBanClear.ImageInactive = null;
            this.btnWalBanClear.ImageMouseOver = null;
            this.btnWalBanClear.ImageNormal = null;
            this.btnWalBanClear.ImagePressed = null;
            this.btnWalBanClear.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnWalBanClear.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnWalBanClear.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnWalBanClear.Location = new Point(0xb2, 0x76);
            this.btnWalBanClear.Name = "btnWalBanClear";
            this.btnWalBanClear.OffsetPressedContent = true;
            this.btnWalBanClear.Padding2 = 5;
            this.btnWalBanClear.Size = new Size(30, 0x17);
            this.btnWalBanClear.StretchImage = false;
            this.btnWalBanClear.TabIndex = 0x18;
            this.btnWalBanClear.Text = "Clr";
            this.btnWalBanClear.TextDropShadow = false;
            this.btnWalBanClear.UseVisualStyleBackColor = true;
            this.btnWalBanClear.Click += new EventHandler(this.btnWalBanClear_Click);
            this.btnWalBanPerma.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnWalBanPerma.BorderDrawing = true;
            this.btnWalBanPerma.FocusRectangleEnabled = false;
            this.btnWalBanPerma.Image = null;
            this.btnWalBanPerma.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnWalBanPerma.ImageBorderEnabled = true;
            this.btnWalBanPerma.ImageDropShadow = true;
            this.btnWalBanPerma.ImageFocused = null;
            this.btnWalBanPerma.ImageInactive = null;
            this.btnWalBanPerma.ImageMouseOver = null;
            this.btnWalBanPerma.ImageNormal = null;
            this.btnWalBanPerma.ImagePressed = null;
            this.btnWalBanPerma.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnWalBanPerma.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnWalBanPerma.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnWalBanPerma.Location = new Point(0x8a, 0x76);
            this.btnWalBanPerma.Name = "btnWalBanPerma";
            this.btnWalBanPerma.OffsetPressedContent = true;
            this.btnWalBanPerma.Padding2 = 5;
            this.btnWalBanPerma.Size = new Size(0x22, 0x17);
            this.btnWalBanPerma.StretchImage = false;
            this.btnWalBanPerma.TabIndex = 0x17;
            this.btnWalBanPerma.Text = "Prm";
            this.btnWalBanPerma.TextDropShadow = false;
            this.btnWalBanPerma.UseVisualStyleBackColor = true;
            this.btnWalBanPerma.Click += new EventHandler(this.btnWalBanPerma_Click);
            this.btnWalBan7.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnWalBan7.BorderDrawing = true;
            this.btnWalBan7.FocusRectangleEnabled = false;
            this.btnWalBan7.Image = null;
            this.btnWalBan7.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnWalBan7.ImageBorderEnabled = true;
            this.btnWalBan7.ImageDropShadow = true;
            this.btnWalBan7.ImageFocused = null;
            this.btnWalBan7.ImageInactive = null;
            this.btnWalBan7.ImageMouseOver = null;
            this.btnWalBan7.ImageNormal = null;
            this.btnWalBan7.ImagePressed = null;
            this.btnWalBan7.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnWalBan7.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnWalBan7.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnWalBan7.Location = new Point(0x6b, 0x76);
            this.btnWalBan7.Name = "btnWalBan7";
            this.btnWalBan7.OffsetPressedContent = true;
            this.btnWalBan7.Padding2 = 5;
            this.btnWalBan7.Size = new Size(0x19, 0x17);
            this.btnWalBan7.StretchImage = false;
            this.btnWalBan7.TabIndex = 0x16;
            this.btnWalBan7.Text = "7";
            this.btnWalBan7.TextDropShadow = false;
            this.btnWalBan7.UseVisualStyleBackColor = true;
            this.btnWalBan7.Click += new EventHandler(this.btnWalBan7_Click);
            this.btnWalBan3.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnWalBan3.BorderDrawing = true;
            this.btnWalBan3.FocusRectangleEnabled = false;
            this.btnWalBan3.Image = null;
            this.btnWalBan3.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnWalBan3.ImageBorderEnabled = true;
            this.btnWalBan3.ImageDropShadow = true;
            this.btnWalBan3.ImageFocused = null;
            this.btnWalBan3.ImageInactive = null;
            this.btnWalBan3.ImageMouseOver = null;
            this.btnWalBan3.ImageNormal = null;
            this.btnWalBan3.ImagePressed = null;
            this.btnWalBan3.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnWalBan3.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnWalBan3.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnWalBan3.Location = new Point(0x4c, 0x76);
            this.btnWalBan3.Name = "btnWalBan3";
            this.btnWalBan3.OffsetPressedContent = true;
            this.btnWalBan3.Padding2 = 5;
            this.btnWalBan3.Size = new Size(0x19, 0x17);
            this.btnWalBan3.StretchImage = false;
            this.btnWalBan3.TabIndex = 0x15;
            this.btnWalBan3.Text = "3";
            this.btnWalBan3.TextDropShadow = false;
            this.btnWalBan3.UseVisualStyleBackColor = true;
            this.btnWalBan3.Click += new EventHandler(this.btnWalBan3_Click);
            this.label9.AutoSize = true;
            this.label9.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label9.Location = new Point(8, 0x7b);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1c, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Wall";
            this.btnWalBan1.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnWalBan1.BorderDrawing = true;
            this.btnWalBan1.FocusRectangleEnabled = false;
            this.btnWalBan1.Image = null;
            this.btnWalBan1.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnWalBan1.ImageBorderEnabled = true;
            this.btnWalBan1.ImageDropShadow = true;
            this.btnWalBan1.ImageFocused = null;
            this.btnWalBan1.ImageInactive = null;
            this.btnWalBan1.ImageMouseOver = null;
            this.btnWalBan1.ImageNormal = null;
            this.btnWalBan1.ImagePressed = null;
            this.btnWalBan1.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnWalBan1.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnWalBan1.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnWalBan1.Location = new Point(0x2d, 0x76);
            this.btnWalBan1.Name = "btnWalBan1";
            this.btnWalBan1.OffsetPressedContent = true;
            this.btnWalBan1.Padding2 = 5;
            this.btnWalBan1.Size = new Size(0x19, 0x17);
            this.btnWalBan1.StretchImage = false;
            this.btnWalBan1.TabIndex = 0x13;
            this.btnWalBan1.Text = "1";
            this.btnWalBan1.TextDropShadow = false;
            this.btnWalBan1.UseVisualStyleBackColor = true;
            this.btnWalBan1.Click += new EventHandler(this.btnWalBan1_Click);
            this.btnForumBanClear.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnForumBanClear.BorderDrawing = true;
            this.btnForumBanClear.FocusRectangleEnabled = false;
            this.btnForumBanClear.Image = null;
            this.btnForumBanClear.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnForumBanClear.ImageBorderEnabled = true;
            this.btnForumBanClear.ImageDropShadow = true;
            this.btnForumBanClear.ImageFocused = null;
            this.btnForumBanClear.ImageInactive = null;
            this.btnForumBanClear.ImageMouseOver = null;
            this.btnForumBanClear.ImageNormal = null;
            this.btnForumBanClear.ImagePressed = null;
            this.btnForumBanClear.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnForumBanClear.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnForumBanClear.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnForumBanClear.Location = new Point(0xb2, 0x59);
            this.btnForumBanClear.Name = "btnForumBanClear";
            this.btnForumBanClear.OffsetPressedContent = true;
            this.btnForumBanClear.Padding2 = 5;
            this.btnForumBanClear.Size = new Size(30, 0x17);
            this.btnForumBanClear.StretchImage = false;
            this.btnForumBanClear.TabIndex = 0x12;
            this.btnForumBanClear.Text = "Clr";
            this.btnForumBanClear.TextDropShadow = false;
            this.btnForumBanClear.UseVisualStyleBackColor = true;
            this.btnForumBanClear.Click += new EventHandler(this.btnForumBanClear_Click);
            this.btnForumBanPerma.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnForumBanPerma.BorderDrawing = true;
            this.btnForumBanPerma.FocusRectangleEnabled = false;
            this.btnForumBanPerma.Image = null;
            this.btnForumBanPerma.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnForumBanPerma.ImageBorderEnabled = true;
            this.btnForumBanPerma.ImageDropShadow = true;
            this.btnForumBanPerma.ImageFocused = null;
            this.btnForumBanPerma.ImageInactive = null;
            this.btnForumBanPerma.ImageMouseOver = null;
            this.btnForumBanPerma.ImageNormal = null;
            this.btnForumBanPerma.ImagePressed = null;
            this.btnForumBanPerma.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnForumBanPerma.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnForumBanPerma.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnForumBanPerma.Location = new Point(0x8a, 0x59);
            this.btnForumBanPerma.Name = "btnForumBanPerma";
            this.btnForumBanPerma.OffsetPressedContent = true;
            this.btnForumBanPerma.Padding2 = 5;
            this.btnForumBanPerma.Size = new Size(0x22, 0x17);
            this.btnForumBanPerma.StretchImage = false;
            this.btnForumBanPerma.TabIndex = 0x11;
            this.btnForumBanPerma.Text = "Prm";
            this.btnForumBanPerma.TextDropShadow = false;
            this.btnForumBanPerma.UseVisualStyleBackColor = true;
            this.btnForumBanPerma.Click += new EventHandler(this.btnForumBanPerma_Click);
            this.btnForumBan7.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnForumBan7.BorderDrawing = true;
            this.btnForumBan7.FocusRectangleEnabled = false;
            this.btnForumBan7.Image = null;
            this.btnForumBan7.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnForumBan7.ImageBorderEnabled = true;
            this.btnForumBan7.ImageDropShadow = true;
            this.btnForumBan7.ImageFocused = null;
            this.btnForumBan7.ImageInactive = null;
            this.btnForumBan7.ImageMouseOver = null;
            this.btnForumBan7.ImageNormal = null;
            this.btnForumBan7.ImagePressed = null;
            this.btnForumBan7.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnForumBan7.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnForumBan7.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnForumBan7.Location = new Point(0x6b, 0x59);
            this.btnForumBan7.Name = "btnForumBan7";
            this.btnForumBan7.OffsetPressedContent = true;
            this.btnForumBan7.Padding2 = 5;
            this.btnForumBan7.Size = new Size(0x19, 0x17);
            this.btnForumBan7.StretchImage = false;
            this.btnForumBan7.TabIndex = 0x10;
            this.btnForumBan7.Text = "7";
            this.btnForumBan7.TextDropShadow = false;
            this.btnForumBan7.UseVisualStyleBackColor = true;
            this.btnForumBan7.Click += new EventHandler(this.btnForumBan7_Click);
            this.btnForumBan3.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnForumBan3.BorderDrawing = true;
            this.btnForumBan3.FocusRectangleEnabled = false;
            this.btnForumBan3.Image = null;
            this.btnForumBan3.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnForumBan3.ImageBorderEnabled = true;
            this.btnForumBan3.ImageDropShadow = true;
            this.btnForumBan3.ImageFocused = null;
            this.btnForumBan3.ImageInactive = null;
            this.btnForumBan3.ImageMouseOver = null;
            this.btnForumBan3.ImageNormal = null;
            this.btnForumBan3.ImagePressed = null;
            this.btnForumBan3.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnForumBan3.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnForumBan3.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnForumBan3.Location = new Point(0x4c, 0x59);
            this.btnForumBan3.Name = "btnForumBan3";
            this.btnForumBan3.OffsetPressedContent = true;
            this.btnForumBan3.Padding2 = 5;
            this.btnForumBan3.Size = new Size(0x19, 0x17);
            this.btnForumBan3.StretchImage = false;
            this.btnForumBan3.TabIndex = 15;
            this.btnForumBan3.Text = "3";
            this.btnForumBan3.TextDropShadow = false;
            this.btnForumBan3.UseVisualStyleBackColor = true;
            this.btnForumBan3.Click += new EventHandler(this.btnForumBan3_Click);
            this.label8.AutoSize = true;
            this.label8.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label8.Location = new Point(8, 0x5e);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x24, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Forum";
            this.btnForumBan1.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnForumBan1.BorderDrawing = true;
            this.btnForumBan1.FocusRectangleEnabled = false;
            this.btnForumBan1.Image = null;
            this.btnForumBan1.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnForumBan1.ImageBorderEnabled = true;
            this.btnForumBan1.ImageDropShadow = true;
            this.btnForumBan1.ImageFocused = null;
            this.btnForumBan1.ImageInactive = null;
            this.btnForumBan1.ImageMouseOver = null;
            this.btnForumBan1.ImageNormal = null;
            this.btnForumBan1.ImagePressed = null;
            this.btnForumBan1.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnForumBan1.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnForumBan1.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnForumBan1.Location = new Point(0x2d, 0x59);
            this.btnForumBan1.Name = "btnForumBan1";
            this.btnForumBan1.OffsetPressedContent = true;
            this.btnForumBan1.Padding2 = 5;
            this.btnForumBan1.Size = new Size(0x19, 0x17);
            this.btnForumBan1.StretchImage = false;
            this.btnForumBan1.TabIndex = 13;
            this.btnForumBan1.Text = "1";
            this.btnForumBan1.TextDropShadow = false;
            this.btnForumBan1.UseVisualStyleBackColor = true;
            this.btnForumBan1.Click += new EventHandler(this.btnForumBan1_Click);
            this.btnMailBanClear.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnMailBanClear.BorderDrawing = true;
            this.btnMailBanClear.FocusRectangleEnabled = false;
            this.btnMailBanClear.Image = null;
            this.btnMailBanClear.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnMailBanClear.ImageBorderEnabled = true;
            this.btnMailBanClear.ImageDropShadow = true;
            this.btnMailBanClear.ImageFocused = null;
            this.btnMailBanClear.ImageInactive = null;
            this.btnMailBanClear.ImageMouseOver = null;
            this.btnMailBanClear.ImageNormal = null;
            this.btnMailBanClear.ImagePressed = null;
            this.btnMailBanClear.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnMailBanClear.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnMailBanClear.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnMailBanClear.Location = new Point(0xb2, 60);
            this.btnMailBanClear.Name = "btnMailBanClear";
            this.btnMailBanClear.OffsetPressedContent = true;
            this.btnMailBanClear.Padding2 = 5;
            this.btnMailBanClear.Size = new Size(30, 0x17);
            this.btnMailBanClear.StretchImage = false;
            this.btnMailBanClear.TabIndex = 12;
            this.btnMailBanClear.Text = "Clr";
            this.btnMailBanClear.TextDropShadow = false;
            this.btnMailBanClear.UseVisualStyleBackColor = true;
            this.btnMailBanClear.Click += new EventHandler(this.btnMailBanClear_Click);
            this.btnMailBanPerma.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnMailBanPerma.BorderDrawing = true;
            this.btnMailBanPerma.FocusRectangleEnabled = false;
            this.btnMailBanPerma.Image = null;
            this.btnMailBanPerma.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnMailBanPerma.ImageBorderEnabled = true;
            this.btnMailBanPerma.ImageDropShadow = true;
            this.btnMailBanPerma.ImageFocused = null;
            this.btnMailBanPerma.ImageInactive = null;
            this.btnMailBanPerma.ImageMouseOver = null;
            this.btnMailBanPerma.ImageNormal = null;
            this.btnMailBanPerma.ImagePressed = null;
            this.btnMailBanPerma.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnMailBanPerma.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnMailBanPerma.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnMailBanPerma.Location = new Point(0x8a, 60);
            this.btnMailBanPerma.Name = "btnMailBanPerma";
            this.btnMailBanPerma.OffsetPressedContent = true;
            this.btnMailBanPerma.Padding2 = 5;
            this.btnMailBanPerma.Size = new Size(0x22, 0x17);
            this.btnMailBanPerma.StretchImage = false;
            this.btnMailBanPerma.TabIndex = 11;
            this.btnMailBanPerma.Text = "Prm";
            this.btnMailBanPerma.TextDropShadow = false;
            this.btnMailBanPerma.UseVisualStyleBackColor = true;
            this.btnMailBanPerma.Click += new EventHandler(this.btnMailBanPerma_Click);
            this.btnMailBan7.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnMailBan7.BorderDrawing = true;
            this.btnMailBan7.FocusRectangleEnabled = false;
            this.btnMailBan7.Image = null;
            this.btnMailBan7.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnMailBan7.ImageBorderEnabled = true;
            this.btnMailBan7.ImageDropShadow = true;
            this.btnMailBan7.ImageFocused = null;
            this.btnMailBan7.ImageInactive = null;
            this.btnMailBan7.ImageMouseOver = null;
            this.btnMailBan7.ImageNormal = null;
            this.btnMailBan7.ImagePressed = null;
            this.btnMailBan7.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnMailBan7.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnMailBan7.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnMailBan7.Location = new Point(0x6b, 60);
            this.btnMailBan7.Name = "btnMailBan7";
            this.btnMailBan7.OffsetPressedContent = true;
            this.btnMailBan7.Padding2 = 5;
            this.btnMailBan7.Size = new Size(0x19, 0x17);
            this.btnMailBan7.StretchImage = false;
            this.btnMailBan7.TabIndex = 10;
            this.btnMailBan7.Text = "7";
            this.btnMailBan7.TextDropShadow = false;
            this.btnMailBan7.UseVisualStyleBackColor = true;
            this.btnMailBan7.Click += new EventHandler(this.btnMailBan7_Click);
            this.btnMailBan3.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnMailBan3.BorderDrawing = true;
            this.btnMailBan3.FocusRectangleEnabled = false;
            this.btnMailBan3.Image = null;
            this.btnMailBan3.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnMailBan3.ImageBorderEnabled = true;
            this.btnMailBan3.ImageDropShadow = true;
            this.btnMailBan3.ImageFocused = null;
            this.btnMailBan3.ImageInactive = null;
            this.btnMailBan3.ImageMouseOver = null;
            this.btnMailBan3.ImageNormal = null;
            this.btnMailBan3.ImagePressed = null;
            this.btnMailBan3.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnMailBan3.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnMailBan3.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnMailBan3.Location = new Point(0x4c, 60);
            this.btnMailBan3.Name = "btnMailBan3";
            this.btnMailBan3.OffsetPressedContent = true;
            this.btnMailBan3.Padding2 = 5;
            this.btnMailBan3.Size = new Size(0x19, 0x17);
            this.btnMailBan3.StretchImage = false;
            this.btnMailBan3.TabIndex = 9;
            this.btnMailBan3.Text = "3";
            this.btnMailBan3.TextDropShadow = false;
            this.btnMailBan3.UseVisualStyleBackColor = true;
            this.btnMailBan3.Click += new EventHandler(this.btnMailBan3_Click);
            this.label7.AutoSize = true;
            this.label7.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label7.Location = new Point(8, 0x41);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1a, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Mail";
            this.btnMailBan1.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnMailBan1.BorderDrawing = true;
            this.btnMailBan1.FocusRectangleEnabled = false;
            this.btnMailBan1.Image = null;
            this.btnMailBan1.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnMailBan1.ImageBorderEnabled = true;
            this.btnMailBan1.ImageDropShadow = true;
            this.btnMailBan1.ImageFocused = null;
            this.btnMailBan1.ImageInactive = null;
            this.btnMailBan1.ImageMouseOver = null;
            this.btnMailBan1.ImageNormal = null;
            this.btnMailBan1.ImagePressed = null;
            this.btnMailBan1.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnMailBan1.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnMailBan1.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnMailBan1.Location = new Point(0x2d, 60);
            this.btnMailBan1.Name = "btnMailBan1";
            this.btnMailBan1.OffsetPressedContent = true;
            this.btnMailBan1.Padding2 = 5;
            this.btnMailBan1.Size = new Size(0x19, 0x17);
            this.btnMailBan1.StretchImage = false;
            this.btnMailBan1.TabIndex = 7;
            this.btnMailBan1.Text = "1";
            this.btnMailBan1.TextDropShadow = false;
            this.btnMailBan1.UseVisualStyleBackColor = true;
            this.btnMailBan1.Click += new EventHandler(this.btnMailBan1_Click);
            this.btnChatBanClear.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnChatBanClear.BorderDrawing = true;
            this.btnChatBanClear.FocusRectangleEnabled = false;
            this.btnChatBanClear.Image = null;
            this.btnChatBanClear.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnChatBanClear.ImageBorderEnabled = true;
            this.btnChatBanClear.ImageDropShadow = true;
            this.btnChatBanClear.ImageFocused = null;
            this.btnChatBanClear.ImageInactive = null;
            this.btnChatBanClear.ImageMouseOver = null;
            this.btnChatBanClear.ImageNormal = null;
            this.btnChatBanClear.ImagePressed = null;
            this.btnChatBanClear.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnChatBanClear.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnChatBanClear.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnChatBanClear.Location = new Point(0xb2, 0x1f);
            this.btnChatBanClear.Name = "btnChatBanClear";
            this.btnChatBanClear.OffsetPressedContent = true;
            this.btnChatBanClear.Padding2 = 5;
            this.btnChatBanClear.Size = new Size(30, 0x17);
            this.btnChatBanClear.StretchImage = false;
            this.btnChatBanClear.TabIndex = 6;
            this.btnChatBanClear.Text = "Clr";
            this.btnChatBanClear.TextDropShadow = false;
            this.btnChatBanClear.UseVisualStyleBackColor = true;
            this.btnChatBanClear.Click += new EventHandler(this.btnChatBanClear_Click);
            this.btnChatBanPerma.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnChatBanPerma.BorderDrawing = true;
            this.btnChatBanPerma.FocusRectangleEnabled = false;
            this.btnChatBanPerma.Image = null;
            this.btnChatBanPerma.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnChatBanPerma.ImageBorderEnabled = true;
            this.btnChatBanPerma.ImageDropShadow = true;
            this.btnChatBanPerma.ImageFocused = null;
            this.btnChatBanPerma.ImageInactive = null;
            this.btnChatBanPerma.ImageMouseOver = null;
            this.btnChatBanPerma.ImageNormal = null;
            this.btnChatBanPerma.ImagePressed = null;
            this.btnChatBanPerma.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnChatBanPerma.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnChatBanPerma.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnChatBanPerma.Location = new Point(0x8a, 0x1f);
            this.btnChatBanPerma.Name = "btnChatBanPerma";
            this.btnChatBanPerma.OffsetPressedContent = true;
            this.btnChatBanPerma.Padding2 = 5;
            this.btnChatBanPerma.Size = new Size(0x22, 0x17);
            this.btnChatBanPerma.StretchImage = false;
            this.btnChatBanPerma.TabIndex = 5;
            this.btnChatBanPerma.Text = "Prm";
            this.btnChatBanPerma.TextDropShadow = false;
            this.btnChatBanPerma.UseVisualStyleBackColor = true;
            this.btnChatBanPerma.Click += new EventHandler(this.btnChatBanPerma_Click);
            this.btnChatBan7.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnChatBan7.BorderDrawing = true;
            this.btnChatBan7.FocusRectangleEnabled = false;
            this.btnChatBan7.Image = null;
            this.btnChatBan7.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnChatBan7.ImageBorderEnabled = true;
            this.btnChatBan7.ImageDropShadow = true;
            this.btnChatBan7.ImageFocused = null;
            this.btnChatBan7.ImageInactive = null;
            this.btnChatBan7.ImageMouseOver = null;
            this.btnChatBan7.ImageNormal = null;
            this.btnChatBan7.ImagePressed = null;
            this.btnChatBan7.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnChatBan7.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnChatBan7.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnChatBan7.Location = new Point(0x6b, 0x1f);
            this.btnChatBan7.Name = "btnChatBan7";
            this.btnChatBan7.OffsetPressedContent = true;
            this.btnChatBan7.Padding2 = 5;
            this.btnChatBan7.Size = new Size(0x19, 0x17);
            this.btnChatBan7.StretchImage = false;
            this.btnChatBan7.TabIndex = 4;
            this.btnChatBan7.Text = "7";
            this.btnChatBan7.TextDropShadow = false;
            this.btnChatBan7.UseVisualStyleBackColor = true;
            this.btnChatBan7.Click += new EventHandler(this.btnChatBan7_Click);
            this.btnChatBan3.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnChatBan3.BorderDrawing = true;
            this.btnChatBan3.FocusRectangleEnabled = false;
            this.btnChatBan3.Image = null;
            this.btnChatBan3.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnChatBan3.ImageBorderEnabled = true;
            this.btnChatBan3.ImageDropShadow = true;
            this.btnChatBan3.ImageFocused = null;
            this.btnChatBan3.ImageInactive = null;
            this.btnChatBan3.ImageMouseOver = null;
            this.btnChatBan3.ImageNormal = null;
            this.btnChatBan3.ImagePressed = null;
            this.btnChatBan3.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnChatBan3.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnChatBan3.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnChatBan3.Location = new Point(0x4c, 0x1f);
            this.btnChatBan3.Name = "btnChatBan3";
            this.btnChatBan3.OffsetPressedContent = true;
            this.btnChatBan3.Padding2 = 5;
            this.btnChatBan3.Size = new Size(0x19, 0x17);
            this.btnChatBan3.StretchImage = false;
            this.btnChatBan3.TabIndex = 3;
            this.btnChatBan3.Text = "3";
            this.btnChatBan3.TextDropShadow = false;
            this.btnChatBan3.UseVisualStyleBackColor = true;
            this.btnChatBan3.Click += new EventHandler(this.btnChatBan3_Click);
            this.label6.AutoSize = true;
            this.label6.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label6.Location = new Point(8, 0x24);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Chat";
            this.btnChatBan1.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnChatBan1.BorderDrawing = true;
            this.btnChatBan1.FocusRectangleEnabled = false;
            this.btnChatBan1.Image = null;
            this.btnChatBan1.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnChatBan1.ImageBorderEnabled = true;
            this.btnChatBan1.ImageDropShadow = true;
            this.btnChatBan1.ImageFocused = null;
            this.btnChatBan1.ImageInactive = null;
            this.btnChatBan1.ImageMouseOver = null;
            this.btnChatBan1.ImageNormal = null;
            this.btnChatBan1.ImagePressed = null;
            this.btnChatBan1.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnChatBan1.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnChatBan1.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnChatBan1.Location = new Point(0x2d, 0x1f);
            this.btnChatBan1.Name = "btnChatBan1";
            this.btnChatBan1.OffsetPressedContent = true;
            this.btnChatBan1.Padding2 = 5;
            this.btnChatBan1.Size = new Size(0x19, 0x17);
            this.btnChatBan1.StretchImage = false;
            this.btnChatBan1.TabIndex = 1;
            this.btnChatBan1.Text = "1";
            this.btnChatBan1.TextDropShadow = false;
            this.btnChatBan1.UseVisualStyleBackColor = true;
            this.btnChatBan1.Click += new EventHandler(this.btnChatBan1_Click);
            this.label4.AutoSize = true;
            this.label4.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            this.label4.Location = new Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Bans (Days)";
            this.btnEditAvatar.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnEditAvatar.BorderDrawing = true;
            this.btnEditAvatar.FocusRectangleEnabled = false;
            this.btnEditAvatar.Image = null;
            this.btnEditAvatar.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnEditAvatar.ImageBorderEnabled = true;
            this.btnEditAvatar.ImageDropShadow = true;
            this.btnEditAvatar.ImageFocused = null;
            this.btnEditAvatar.ImageInactive = null;
            this.btnEditAvatar.ImageMouseOver = null;
            this.btnEditAvatar.ImageNormal = null;
            this.btnEditAvatar.ImagePressed = null;
            this.btnEditAvatar.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnEditAvatar.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnEditAvatar.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnEditAvatar.Location = new Point(0x273, 0x209);
            this.btnEditAvatar.Name = "btnEditAvatar";
            this.btnEditAvatar.OffsetPressedContent = true;
            this.btnEditAvatar.Padding2 = 5;
            this.btnEditAvatar.Size = new Size(0x83, 0x17);
            this.btnEditAvatar.StretchImage = false;
            this.btnEditAvatar.TabIndex = 0x1a;
            this.btnEditAvatar.Text = "Edit Avatar";
            this.btnEditAvatar.TextDropShadow = false;
            this.btnEditAvatar.UseVisualStyleBackColor = true;
            this.btnEditAvatar.Visible = false;
            this.btnEditAvatar.Click += new EventHandler(this.bitmapButton1_Click);
            this.btnInviteToFaction.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnInviteToFaction.BorderDrawing = true;
            this.btnInviteToFaction.FocusRectangleEnabled = false;
            this.btnInviteToFaction.Image = null;
            this.btnInviteToFaction.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnInviteToFaction.ImageBorderEnabled = true;
            this.btnInviteToFaction.ImageDropShadow = true;
            this.btnInviteToFaction.ImageFocused = null;
            this.btnInviteToFaction.ImageInactive = null;
            this.btnInviteToFaction.ImageMouseOver = null;
            this.btnInviteToFaction.ImageNormal = null;
            this.btnInviteToFaction.ImagePressed = null;
            this.btnInviteToFaction.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnInviteToFaction.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnInviteToFaction.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnInviteToFaction.Location = new Point(0x1df, 0x61);
            this.btnInviteToFaction.Name = "btnInviteToFaction";
            this.btnInviteToFaction.OffsetPressedContent = true;
            this.btnInviteToFaction.Padding2 = 5;
            this.btnInviteToFaction.Size = new Size(0x83, 0x21);
            this.btnInviteToFaction.StretchImage = false;
            this.btnInviteToFaction.TabIndex = 0x19;
            this.btnInviteToFaction.Text = "Invite To Faction";
            this.btnInviteToFaction.TextDropShadow = false;
            this.btnInviteToFaction.UseVisualStyleBackColor = true;
            this.btnInviteToFaction.Visible = false;
            this.btnInviteToFaction.Click += new EventHandler(this.btnInviteToFaction_Click);
            this.btnAchievements.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnAchievements.BorderDrawing = true;
            this.btnAchievements.FocusRectangleEnabled = false;
            this.btnAchievements.Image = null;
            this.btnAchievements.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnAchievements.ImageBorderEnabled = true;
            this.btnAchievements.ImageDropShadow = true;
            this.btnAchievements.ImageFocused = null;
            this.btnAchievements.ImageInactive = null;
            this.btnAchievements.ImageMouseOver = null;
            this.btnAchievements.ImageNormal = null;
            this.btnAchievements.ImagePressed = null;
            this.btnAchievements.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnAchievements.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnAchievements.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnAchievements.Location = new Point(0x1df, 0x3a);
            this.btnAchievements.Name = "btnAchievements";
            this.btnAchievements.OffsetPressedContent = true;
            this.btnAchievements.Padding2 = 5;
            this.btnAchievements.Size = new Size(0x83, 0x21);
            this.btnAchievements.StretchImage = false;
            this.btnAchievements.TabIndex = 0x18;
            this.btnAchievements.Text = "Achievements";
            this.btnAchievements.TextDropShadow = false;
            this.btnAchievements.UseVisualStyleBackColor = true;
            this.btnAchievements.Click += new EventHandler(this.btnAchievements_Click);
            this.btnSendMail.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnSendMail.BorderDrawing = true;
            this.btnSendMail.FocusRectangleEnabled = false;
            this.btnSendMail.Image = null;
            this.btnSendMail.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnSendMail.ImageBorderEnabled = true;
            this.btnSendMail.ImageDropShadow = true;
            this.btnSendMail.ImageFocused = null;
            this.btnSendMail.ImageInactive = null;
            this.btnSendMail.ImageMouseOver = null;
            this.btnSendMail.ImageNormal = null;
            this.btnSendMail.ImagePressed = null;
            this.btnSendMail.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnSendMail.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnSendMail.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnSendMail.Location = new Point(0x1df, 0x13);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.OffsetPressedContent = true;
            this.btnSendMail.Padding2 = 5;
            this.btnSendMail.Size = new Size(0x83, 0x21);
            this.btnSendMail.StretchImage = false;
            this.btnSendMail.TabIndex = 20;
            this.btnSendMail.Text = "Send Mail";
            this.btnSendMail.TextDropShadow = false;
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new EventHandler(this.btnSendMail_Click);
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnClose.BorderDrawing = true;
            this.btnClose.FocusRectangleEnabled = false;
            this.btnClose.Image = null;
            this.btnClose.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnClose.ImageBorderEnabled = true;
            this.btnClose.ImageDropShadow = true;
            this.btnClose.ImageFocused = null;
            this.btnClose.ImageInactive = null;
            this.btnClose.ImageMouseOver = null;
            this.btnClose.ImageNormal = null;
            this.btnClose.ImagePressed = null;
            this.btnClose.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnClose.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnClose.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnClose.Location = new Point(0x2b7, 0x213);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnGiveQuests.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnGiveQuests.BorderDrawing = true;
            this.btnGiveQuests.FocusRectangleEnabled = false;
            this.btnGiveQuests.Image = null;
            this.btnGiveQuests.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnGiveQuests.ImageBorderEnabled = true;
            this.btnGiveQuests.ImageDropShadow = true;
            this.btnGiveQuests.ImageFocused = null;
            this.btnGiveQuests.ImageInactive = null;
            this.btnGiveQuests.ImageMouseOver = null;
            this.btnGiveQuests.ImageNormal = null;
            this.btnGiveQuests.ImagePressed = null;
            this.btnGiveQuests.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnGiveQuests.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnGiveQuests.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnGiveQuests.Location = new Point(0x7f, 0x12e);
            this.btnGiveQuests.Name = "btnGiveQuests";
            this.btnGiveQuests.OffsetPressedContent = true;
            this.btnGiveQuests.Padding2 = 5;
            this.btnGiveQuests.Size = new Size(0x6c, 0x17);
            this.btnGiveQuests.StretchImage = false;
            this.btnGiveQuests.TabIndex = 0x2a;
            this.btnGiveQuests.Text = "Give Quests";
            this.btnGiveQuests.TextDropShadow = false;
            this.btnGiveQuests.UseVisualStyleBackColor = true;
            this.btnGiveQuests.Click += new EventHandler(this.btnGiveQuests_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.Controls.Add(this.btnEditAvatar);
            base.Controls.Add(this.btnInviteToFaction);
            base.Controls.Add(this.btnAchievements);
            base.Controls.Add(this.gbModerator);
            base.Controls.Add(this.lblIsModerator);
            base.Controls.Add(this.lblIsAdmin);
            base.Controls.Add(this.btnSendMail);
            base.Controls.Add(this.imgAvatar);
            base.Controls.Add(this.lblFaction);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblStanding);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lblPoints);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.lblRank);
            base.Controls.Add(this.lblUserName);
            base.Controls.Add(this.pnlVillages);
            base.Controls.Add(this.btnClose);
            this.MinimumSize = new Size(0x318, 0x236);
            base.Name = "UserInfoScreen";
            base.Size = new Size(0x318, 0x236);
            base.Paint += new PaintEventHandler(this.UserInfoScreen_Paint);
            base.Resize += new EventHandler(this.UserInfoScreen_Resize);
            this.gbModerator.ResumeLayout(false);
            this.gbModerator.PerformLayout();
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

        private void lblFaction_MouseClick(object sender, MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Left) && (this.m_userInfo != null)) && (this.m_userInfo.factionID >= 0))
            {
                GameEngine.Instance.playInterfaceSound("UserInfoScreen_faction");
                InterfaceMgr.Instance.closeParishPanel();
                InterfaceMgr.Instance.showFactionPanel(this.m_userInfo.factionID);
            }
            if (e.Button == MouseButtons.Right)
            {
                Clipboard.SetText(((Label) sender).Text);
            }
        }

        private void lblFaction_MouseEnter(object sender, EventArgs e)
        {
            this.lblFaction.ForeColor = ARGBColors.Blue;
        }

        private void lblFaction_MouseLeave(object sender, EventArgs e)
        {
            this.lblFaction.ForeColor = ARGBColors.Black;
        }

        private void lblUserName_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Clipboard.SetText(((Label) sender).Text);
            }
        }

        private void sendCommandToServer(int command, int duration)
        {
            if ((this.m_userID != RemoteServices.Instance.UserID) && (RemoteServices.Instance.Admin || RemoteServices.Instance.Moderator))
            {
                this.m_reasonString = "";
                ReasonPopup popup = new ReasonPopup();
                popup.init(this);
                popup.ShowDialog();
                if (this.m_reasonString.Length > 0)
                {
                    RemoteServices.Instance.SendCommands(this.m_userID, command, duration, this.m_reasonString);
                }
                else
                {
                    MyMessageBox.Show("Not reason given", "Admin Error");
                }
            }
            else
            {
                MyMessageBox.Show("Command not sent", "Admin Error");
            }
        }

        public void setReasonString(string reasonString)
        {
            this.m_reasonString = reasonString;
        }

        public void update()
        {
            this.init(this.m_userInfo);
        }

        private void UserInfoScreen_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0x56, 0x62, 0x6a), Color.FromArgb(0x9f, 180, 0xc1), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, rect);
            brush.Dispose();
        }

        private void UserInfoScreen_Resize(object sender, EventArgs e)
        {
            base.Invalidate();
        }
    }
}

