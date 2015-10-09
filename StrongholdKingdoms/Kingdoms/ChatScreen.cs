namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public class ChatScreen : CustomSelfDrawPanel, IDockableControl
    {
        private int activeChatRoomIdent = -1;
        private BitmapButton btnClose;
        private BitmapButton btnSend;
        private CheckBox cbChatUpdate;
        private int checkTime = 5;
        private IContainer components;
        private DockableControl dockableControl;
        private bool dontPlayChangeSound;
        private bool inClosing;
        private bool inSend;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DateTime lastRequestTime = DateTime.MinValue;
        private DateTime lastSendTime = DateTime.MinValue;
        private ListBox lbActiveChatters;
        private Label lblLanguage;
        private Label lblRoomName;
        private ListBox lbRooms;
        private SparseArray localChatRooms = new SparseArray();
        private ChatScreenManager m_parent;
        private Panel pnlWikiHelp;
        private List<Chat_RoomID> registeredRooms = new List<Chat_RoomID>();
        private List<ChatRoom> roomsDataSource = new List<ChatRoom>();
        private RichTextBox rtb = new RichTextBox();
        private TextBox tbTextInput;
        private RichTextBox tbTextViewer;

        public ChatScreen()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.tbTextInput.Focus();
            this.dockableControl.setSizeableWindow();
            this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 14f);
            this.lblRoomName.Font = FontManager.GetFont("Microsoft Sans Serif", 14f);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            base.ClickThru = true;
        }

        private void addText(List<Chat_TextEntry> newText)
        {
            Regex regex = new Regex(@"({\\)(.+?)(})|(\\)(.+?)(\b)");
            bool flag = false;
            bool flag2 = false;
            foreach (Chat_TextEntry entry in newText)
            {
                entry.text = regex.Replace(entry.text, "");
                this.rtb.Text = "";
                this.rtb.SelectionColor = ARGBColors.Red;
                this.rtb.SelectionFont = FontManager.GetFont("Arial", 8.25f, FontStyle.Bold);
                this.rtb.AppendText("[ " + entry.username + " - " + entry.postedTime.ToShortTimeString() + " ]     ");
                this.rtb.SelectionFont = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.rtb.SelectionColor = ARGBColors.Black;
                this.rtb.AppendText(entry.text);
                string rtf = this.rtb.Rtf;
                int num = this.createChatRoomIdent(entry.roomType, entry.roomID);
                ChatRoom room = (ChatRoom) this.localChatRooms[num];
                if (room != null)
                {
                    this.rtb.Clear();
                    this.rtb.SelectedRtf = room.text;
                    this.rtb.SelectionStart = this.rtb.TextLength - 1;
                    this.rtb.SelectionLength = 1;
                    this.rtb.SelectedRtf = rtf;
                    room.text = this.rtb.Rtf;
                    if (num == this.activeChatRoomIdent)
                    {
                        int selectionStart = this.tbTextViewer.SelectionStart;
                        int selectionLength = this.tbTextViewer.SelectionLength;
                        this.tbTextViewer.SelectionStart = this.tbTextViewer.Rtf.Length;
                        this.tbTextViewer.SelectionLength = 0;
                        this.tbTextViewer.SelectedRtf = rtf;
                        flag2 = true;
                        room.newText = false;
                        this.tbTextViewer.SelectionStart = selectionStart;
                        this.tbTextViewer.SelectionLength = selectionLength;
                    }
                    else if (!room.newText)
                    {
                        room.newText = true;
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                this.lbRooms.DataSource = null;
                this.lbRooms.DataSource = this.roomsDataSource;
            }
            if (flag2)
            {
                this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
                this.tbTextViewer.ScrollToCaret();
            }
        }

        public bool areListsEqual(List<string> list1, List<string> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            foreach (string str in list1)
            {
                if (!list2.Contains(str))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!this.inClosing)
            {
                this.inClosing = true;
                if (this.m_parent != null)
                {
                    GameEngine.Instance.playInterfaceSound("ChatScreen_close");
                    this.m_parent.close(true, true);
                }
                this.inClosing = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!RemoteServices.Instance.ChatActive)
            {
                this.btnSend.Enabled = false;
            }
            else if (!this.isRealString(this.tbTextInput.Text))
            {
                this.tbTextInput.Text = "";
                this.tbTextInput.Focus();
            }
            else
            {
                this.btnSend.Enabled = false;
                if (this.inSend)
                {
                    int num = 0;
                    while (this.inSend)
                    {
                        num++;
                        Thread.Sleep(1);
                        Program.DoEvents();
                        RemoteServices.Instance.processData();
                        if (num > 0x1388)
                        {
                            break;
                        }
                    }
                }
                this.btnSend.Enabled = false;
                this.inSend = true;
                GameEngine.Instance.playInterfaceSound("ChatScreen_sendchat");
                RemoteServices.Instance.set_Chat_SendText_UserCallBack(new RemoteServices.Chat_SendText_UserCallBack(this.chat_SendText_UserCallBack));
                int roomType = 0;
                int roomID = 0;
                this.splitChatRoomIdent(this.activeChatRoomIdent, ref roomType, ref roomID);
                RemoteServices.Instance.Chat_SendText(this.tbTextInput.Text, roomType, roomID);
                this.tbTextInput.Text = "";
                this.tbTextInput.Focus();
            }
        }

        private List<Chat_RoomID> calcUsersRooms()
        {
            List<Chat_RoomID> list = new List<Chat_RoomID>();
            int userFactionID = RemoteServices.Instance.UserFactionID;
            if (userFactionID >= 0)
            {
                Chat_RoomID mid = new Chat_RoomID {
                    roomType = 5,
                    roomID = userFactionID
                };
                list.Add(mid);
                if (GameEngine.Instance.World.YourFaction != null)
                {
                    int houseID = GameEngine.Instance.World.YourFaction.houseID;
                    if (houseID > 0)
                    {
                        Chat_RoomID mid2 = new Chat_RoomID {
                            roomType = 6,
                            roomID = houseID
                        };
                        list.Add(mid2);
                    }
                }
            }
            foreach (int num3 in GameEngine.Instance.World.getListOfUserParishes())
            {
                Chat_RoomID mid3 = new Chat_RoomID {
                    roomType = 3,
                    roomID = num3
                };
                list.Add(mid3);
            }
            foreach (int num4 in GameEngine.Instance.World.getListOfUserCounties())
            {
                Chat_RoomID mid4 = new Chat_RoomID {
                    roomType = 9,
                    roomID = num4
                };
                list.Add(mid4);
            }
            foreach (int num5 in GameEngine.Instance.World.getListOfUserProvinces())
            {
                Chat_RoomID mid5 = new Chat_RoomID {
                    roomType = 1,
                    roomID = num5
                };
                list.Add(mid5);
            }
            foreach (int num6 in GameEngine.Instance.World.getListOfUserCountries())
            {
                Chat_RoomID mid6 = new Chat_RoomID {
                    roomType = 0,
                    roomID = num6
                };
                list.Add(mid6);
            }
            List<int> list6 = new List<int> { 0 };
            Chat_RoomID item = new Chat_RoomID {
                roomType = 8,
                roomID = 0
            };
            list.Add(item);
            foreach (int num7 in list6)
            {
                Chat_RoomID mid8 = new Chat_RoomID {
                    roomType = 2,
                    roomID = num7
                };
                list.Add(mid8);
            }
            if ((GameEngine.Instance.World.GetGlobalWorldID() >= 700) && (GameEngine.Instance.World.GetGlobalWorldID() < 800))
            {
                Chat_RoomID mid9 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 10
                };
                list.Add(mid9);
                Chat_RoomID mid10 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 12
                };
                list.Add(mid10);
                Chat_RoomID mid11 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 11
                };
                list.Add(mid11);
                Chat_RoomID mid12 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 13
                };
                list.Add(mid12);
                Chat_RoomID mid13 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 14
                };
                list.Add(mid13);
                Chat_RoomID mid14 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 15
                };
                list.Add(mid14);
                Chat_RoomID mid15 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 0x10
                };
                list.Add(mid15);
                Chat_RoomID mid16 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 0x11
                };
                list.Add(mid16);
                Chat_RoomID mid17 = new Chat_RoomID {
                    roomType = 2,
                    roomID = 0x12
                };
                list.Add(mid17);
            }
            if (list.Count == this.registeredRooms.Count)
            {
                bool flag = false;
                foreach (Chat_RoomID mid18 in list)
                {
                    bool flag2 = false;
                    foreach (Chat_RoomID mid19 in this.registeredRooms)
                    {
                        if ((mid18.roomID == mid19.roomID) && (mid18.roomType == mid19.roomType))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    list.Clear();
                }
            }
            return list;
        }

        private void cbChatUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Program.mySettings.NotifyChatUpdate = this.cbChatUpdate.Checked;
            GameEngine.Instance.playInterfaceSound("ChatScreen_notify_toggle");
        }

        private void chat_ReceiveText_UserCallBack(Chat_ReceiveText_ReturnType returnData)
        {
            if (!base.Enabled)
            {
                base.Enabled = true;
            }
            if (returnData.Success)
            {
                if ((returnData.textList != null) && (returnData.textList.Count > 0))
                {
                    if (RemoteServices.Instance.UserOptions.profanityFilter)
                    {
                        foreach (Chat_TextEntry entry in returnData.textList)
                        {
                            entry.text = GameEngine.Instance.censorString(entry.text);
                        }
                    }
                    this.addText(returnData.textList);
                    this.checkTime = 1;
                    GameEngine.Instance.playInterfaceSound("ChatScreen_new_chat");
                    if ((Form.ActiveForm != base.ParentForm) && Program.mySettings.NotifyChatUpdate)
                    {
                        FlashWindow.Start(base.ParentForm);
                    }
                }
                if (returnData.activeUsers != null)
                {
                    this.splitUsersIntoRooms(returnData.activeUsers);
                }
            }
            this.checkTime++;
            if (this.checkTime >= 30)
            {
                this.checkTime = 30;
            }
            this.lastRequestTime = DateTime.Now;
            this.inSend = false;
            if (this.registeredRooms.Count > 0)
            {
                this.btnSend.Enabled = true;
            }
        }

        private void chat_SendText_UserCallBack(Chat_SendText_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if ((returnData.textList != null) && (returnData.textList.Count > 0))
                {
                    if (RemoteServices.Instance.UserOptions.profanityFilter)
                    {
                        foreach (Chat_TextEntry entry in returnData.textList)
                        {
                            entry.text = GameEngine.Instance.censorString(entry.text);
                        }
                    }
                    this.addText(returnData.textList);
                }
                if (returnData.banned)
                {
                    InterfaceMgr.Instance.chatSetBan(true);
                    this.closeClickForm(null, null);
                }
            }
            this.checkTime = 2;
            this.lastRequestTime = DateTime.Now;
            this.btnSend.Enabled = true;
            this.inSend = false;
        }

        public void closeClick()
        {
        }

        public void closeClickForm(object sender, FormClosingEventArgs e)
        {
            if (!this.inClosing)
            {
                this.inClosing = true;
                if (this.m_parent != null)
                {
                    this.m_parent.close(true, true);
                }
                this.inClosing = false;
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        private int createChatRoomIdent(int roomType, int roomID)
        {
            return ((roomID * 10) + roomType);
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y, true, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void dockClick()
        {
        }

        public string getRoomName(int roomType, int roomID)
        {
            string str = "";
            switch (roomType)
            {
                case 0:
                    return (SK.Text("GENERIC_Country", "Country") + " : " + GameEngine.Instance.World.getCountryName(roomID));

                case 1:
                    return (SK.Text("GENERIC_Province", "Province") + " : " + GameEngine.Instance.World.getProvinceName(roomID));

                case 2:
                    switch (roomID)
                    {
                        case 10:
                            return "English Chat";

                        case 11:
                            return "Deutsch Chat";

                        case 12:
                            return "Fran\x00e7ais Chat";

                        case 13:
                            return "Русский Чат";

                        case 14:
                            return "Espa\x00f1ol Chat";

                        case 15:
                            return "Polski Czat";

                        case 0x10:
                            return "T\x00fcrk\x00e7e Sohbet";

                        case 0x11:
                            return "Chat Italiana";

                        case 0x12:
                            return "Bate-papo Portugu\x00eas do Brasil";
                    }
                    return SK.Text("ChatScreen_Global_Chat", "Global Chat");

                case 3:
                    return (SK.Text("GENERIC_Parish", "Parish") + " : " + GameEngine.Instance.World.getParishName(roomID));

                case 4:
                case 7:
                    return str;

                case 5:
                    if (GameEngine.Instance.World.YourFaction != null)
                    {
                        str = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction") + " : " + GameEngine.Instance.World.YourFaction.factionNameAbrv;
                    }
                    return str;

                case 6:
                    return (SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + roomID.ToString());

                case 8:
                    return SK.Text("MENU_Help", "Help");

                case 9:
                    return (SK.Text("GENERIC_County", "County") + " : " + GameEngine.Instance.World.getCountyName(roomID));
            }
            return str;
        }

        public void init(ChatScreenManager parent)
        {
            this.label1.Text = SK.Text("GENERIC_Chat", "Chat");
            this.btnSend.Text = SK.Text("ChatScreen_Send", "Send");
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.lblRoomName.Text = SK.Text("ChatScreen_Chat_Room_Name", "Chat Room Name");
            this.label2.Text = SK.Text("ChatScreen_Users_Online", "Users Online");
            this.label3.Text = SK.Text("ChatScreen_Available_Rooms", "Available Rooms");
            this.label4.Text = SK.Text("ChatScreen_Abuse_Warning", "Personal Abuse or abusing this system (such as spamming or copy / pasting) will result in removal from Stronghold Kingdoms.");
            this.cbChatUpdate.Text = SK.Text("ChatScreen_Notify", "Notify new chat");
            if ((GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1) || ((GameEngine.Instance.World.GetGlobalWorldID() >= 700) && (GameEngine.Instance.World.GetGlobalWorldID() < 800)))
            {
                this.lblLanguage.Text = "";
            }
            else
            {
                switch (GameEngine.Instance.World.WorldDefaultLanguage)
                {
                    case "en":
                        this.lblLanguage.Text = SK.Text("ChatScreen_English_Only", "Languages: English Only");
                        break;

                    case "de":
                        this.lblLanguage.Text = SK.Text("ChatScreen_German_Only", "Languages: German Only");
                        break;

                    case "fr":
                        this.lblLanguage.Text = SK.Text("ChatScreen_French_Only", "Languages: French Only");
                        break;

                    case "ru":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Russian_Only", "Languages: Russian Only");
                        break;

                    case "es":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Spanish_Only", "Languages: Spanish Only");
                        break;

                    case "pl":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Polish_Only", "Languages: Polish Only");
                        break;

                    case "it":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Italian_Only", "Languages: Italian Only");
                        break;

                    case "tr":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Turkish_Only", "Languages: Turkish Only");
                        break;

                    case "pt":
                        this.lblLanguage.Text = SK.Text("ChatScreen_BrazilianPortuguese_Only", "Languages: Brazilian-Portuguese Only");
                        break;
                }
            }
            this.tbTextInput.Visible = true;
            this.btnSend.Visible = true;
            this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
            CustomTooltipManager.addTooltipToSystemControl(this.pnlWikiHelp, 0x1131);
            this.lbActiveChatters.Visible = true;
            this.tbTextViewer.Size = new Size(0x214, 0x183);
            this.m_parent = parent;
            base.clearControls();
            this.initTextWindow();
            this.cbChatUpdate.Checked = Program.mySettings.NotifyChatUpdate;
            if (this.registeredRooms.Count == 0)
            {
                this.btnSend.Enabled = false;
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.tbTextInput = new TextBox();
            this.tbTextViewer = new RichTextBox();
            this.btnSend = new BitmapButton();
            this.btnClose = new BitmapButton();
            this.lblRoomName = new Label();
            this.lbActiveChatters = new ListBox();
            this.lbRooms = new ListBox();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.cbChatUpdate = new CheckBox();
            this.lblLanguage = new Label();
            this.pnlWikiHelp = new Panel();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 0x18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chat";
            this.tbTextInput.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.tbTextInput.ForeColor = ARGBColors.Black;
            this.tbTextInput.Location = new Point(0xe1, 460);
            this.tbTextInput.MaxLength = 500;
            this.tbTextInput.Multiline = true;
            this.tbTextInput.Name = "tbTextInput";
            this.tbTextInput.ScrollBars = ScrollBars.Vertical;
            this.tbTextInput.Size = new Size(0x214, 0x4f);
            this.tbTextInput.TabIndex = 1;
            this.tbTextInput.KeyPress += new KeyPressEventHandler(this.tbTextInput_KeyPress);
            this.tbTextViewer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tbTextViewer.BackColor = Color.FromArgb(220, 220, 220);
            this.tbTextViewer.Location = new Point(0xe1, 0x43);
            this.tbTextViewer.Name = "tbTextViewer";
            this.tbTextViewer.ReadOnly = true;
            this.tbTextViewer.ScrollBars = RichTextBoxScrollBars.Vertical;
            this.tbTextViewer.Size = new Size(0x214, 0x183);
            this.tbTextViewer.TabIndex = 2;
            this.tbTextViewer.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
            this.tbTextViewer.LinkClicked += new LinkClickedEventHandler(this.tbTextViewer_LinkClicked);
            this.btnSend.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnSend.BorderColor = Color.FromArgb(0, 0, 0x8b);
            this.btnSend.BorderDrawing = true;
            this.btnSend.FocusRectangleEnabled = false;
            this.btnSend.Image = null;
            this.btnSend.ImageBorderColor = Color.FromArgb(210, 0x69, 30);
            this.btnSend.ImageBorderEnabled = true;
            this.btnSend.ImageDropShadow = true;
            this.btnSend.ImageFocused = null;
            this.btnSend.ImageInactive = null;
            this.btnSend.ImageMouseOver = null;
            this.btnSend.ImageNormal = null;
            this.btnSend.ImagePressed = null;
            this.btnSend.InnerBorderColor = Color.FromArgb(0xd3, 0xd3, 0xd3);
            this.btnSend.InnerBorderColor_Focus = Color.FromArgb(0xad, 0xd8, 230);
            this.btnSend.InnerBorderColor_MouseOver = Color.FromArgb(0xff, 0xd7, 0);
            this.btnSend.Location = new Point(0x2fb, 0x204);
            this.btnSend.Name = "btnSend";
            this.btnSend.OffsetPressedContent = true;
            this.btnSend.Padding2 = 5;
            this.btnSend.Size = new Size(0x59, 0x17);
            this.btnSend.StretchImage = false;
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.TextDropShadow = false;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);
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
            this.btnClose.Location = new Point(0x37b, 0x204);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(0x59, 0x17);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.lblRoomName.AutoSize = true;
            this.lblRoomName.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblRoomName.Location = new Point(0xdd, 40);
            this.lblRoomName.Name = "lblRoomName";
            this.lblRoomName.Size = new Size(160, 0x18);
            this.lblRoomName.TabIndex = 5;
            this.lblRoomName.Text = "Chat Room Name";
            this.lbActiveChatters.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lbActiveChatters.ForeColor = Color.Black;
            this.lbActiveChatters.FormattingEnabled = true;
            this.lbActiveChatters.Location = new Point(0x309, 0x43);
            this.lbActiveChatters.Name = "lbActiveChatters";
            this.lbActiveChatters.Size = new Size(0xbd, 0x17d);
            this.lbActiveChatters.TabIndex = 6;
            this.lbActiveChatters.DoubleClick += new EventHandler(this.lbActiveChatters_DoubleClick);
            this.lbRooms.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.lbRooms.ForeColor = Color.Black;
            this.lbRooms.FormattingEnabled = true;
            this.lbRooms.Location = new Point(0x15, 0x43);
            this.lbRooms.Name = "lbRooms";
            this.lbRooms.Size = new Size(0xb2, 0x18a);
            this.lbRooms.TabIndex = 7;
            this.lbRooms.SelectedIndexChanged += new EventHandler(this.lbRooms_SelectedIndexChanged);
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x306, 0x33);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Users Online";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x12, 0x33);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x56, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Available Rooms";
            this.label4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label4.Location = new Point(0x15, 0x223);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b1, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Personal Abuse or abusing this system (such as spamming or copy / pasting) will result in removal from Stronghold Kingdoms.";
            this.label4.TextAlign = ContentAlignment.TopCenter;
            this.cbChatUpdate.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.cbChatUpdate.Checked = true;
            this.cbChatUpdate.CheckState = CheckState.Checked;
            this.cbChatUpdate.Location = new Point(0x309, 0x1ce);
            this.cbChatUpdate.Name = "cbChatUpdate";
            this.cbChatUpdate.Size = new Size(0xbd, 0x11);
            this.cbChatUpdate.TabIndex = 11;
            this.cbChatUpdate.Text = "Notify new chat";
            this.cbChatUpdate.UseVisualStyleBackColor = true;
            this.cbChatUpdate.CheckedChanged += new EventHandler(this.cbChatUpdate_CheckedChanged);
            this.lblLanguage.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.lblLanguage.Location = new Point(0x22d, 0x33);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new Size(200, 13);
            this.lblLanguage.TabIndex = 12;
            this.lblLanguage.Text = "English Only";
            this.lblLanguage.TextAlign = ContentAlignment.TopRight;
            this.pnlWikiHelp.Location = new Point(0x3a3, 11);
            this.pnlWikiHelp.Name = "pnlWikiHelp";
            this.pnlWikiHelp.Size = new Size(0x23, 0x23);
            this.pnlWikiHelp.TabIndex = 13;
            this.pnlWikiHelp.MouseLeave += new EventHandler(this.pnlWikiHelp_MouseLeave);
            this.pnlWikiHelp.MouseClick += new MouseEventHandler(this.pnlWikiHelp_MouseClick);
            this.pnlWikiHelp.MouseEnter += new EventHandler(this.pnlWikiHelp_MouseEnter);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0, 0xff, 0xff, 0xff);
            base.Controls.Add(this.pnlWikiHelp);
            base.Controls.Add(this.lblLanguage);
            base.Controls.Add(this.cbChatUpdate);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lbRooms);
            base.Controls.Add(this.lbActiveChatters);
            base.Controls.Add(this.lblRoomName);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnSend);
            base.Controls.Add(this.tbTextViewer);
            base.Controls.Add(this.tbTextInput);
            base.Controls.Add(this.label1);
            this.MaximumSize = new Size(0x7d0, 0x7d0);
            this.MinimumSize = new Size(750, 350);
            base.Name = "ChatScreen";
            base.Size = new Size(0x3e0, 0x236);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        private void initTextWindow()
        {
            this.cbChatUpdate.Checked = Program.mySettings.NotifyChatUpdate;
            this.tbTextViewer.Text = "";
            for (int i = 0; i < 0x1c; i++)
            {
                this.tbTextViewer.Text = this.tbTextViewer.Text + "\r\n";
            }
            this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
            this.tbTextViewer.ScrollToCaret();
        }

        public bool isActive()
        {
            return (base.ParentForm != null);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        private bool isRealString(string text)
        {
            if (this.tbTextInput.Text.Length <= 0)
            {
                return false;
            }
            bool flag = false;
            foreach (char ch in this.tbTextInput.Text)
            {
                if ((((ch != ' ') && (ch != '.')) && ((ch != '*') && (ch != '-'))) && (((ch != '=') && (ch != '+')) && (ch != ',')))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                return false;
            }
            return true;
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void lbActiveChatters_DoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = this.lbActiveChatters.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex < this.lbActiveChatters.Items.Count))
            {
                string str = (string) this.lbActiveChatters.Items[selectedIndex];
                ChatRoom room = (ChatRoom) this.localChatRooms[this.activeChatRoomIdent];
                if (room != null)
                {
                    foreach (Chat_UserInRoom room2 in room.usersDataInRoom)
                    {
                        if (room2.username == str)
                        {
                            GameEngine.Instance.playInterfaceSound("ChatScreen_user_clicked");
                            InterfaceMgr.Instance.changeTab(0);
                            WorldMap.CachedUserInfo userInfo = new WorldMap.CachedUserInfo {
                                userID = room2.userID
                            };
                            InterfaceMgr.Instance.showUserInfoScreen(userInfo);
                            break;
                        }
                    }
                }
            }
        }

        private void lbRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChatRoom selectedItem = (ChatRoom) this.lbRooms.SelectedItem;
            if (selectedItem != null)
            {
                int openRoomIdent = this.createChatRoomIdent(selectedItem.roomType, selectedItem.roomID);
                if (openRoomIdent != this.activeChatRoomIdent)
                {
                    if (!this.dontPlayChangeSound)
                    {
                        GameEngine.Instance.playInterfaceSound("ChatScreen_change_room");
                    }
                    this.openChatRoom(openRoomIdent);
                }
            }
        }

        private void openChatRoom(int openRoomIdent)
        {
            this.dontPlayChangeSound = true;
            ChatRoom room = (ChatRoom) this.localChatRooms[openRoomIdent];
            if (room != null)
            {
                this.tbTextViewer.Clear();
                this.tbTextViewer.Rtf = room.text;
                this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
                this.tbTextViewer.SelectionLength = 0;
                this.tbTextViewer.ScrollToCaret();
                this.activeChatRoomIdent = openRoomIdent;
                room.newText = false;
                this.lblRoomName.Text = room.roomName;
                if (room.roomType == 5)
                {
                    this.lblLanguage.Visible = false;
                }
                else
                {
                    this.lblLanguage.Visible = true;
                }
                this.updateUsersListBox();
                this.lbRooms.DataSource = null;
                this.lbRooms.DataSource = this.roomsDataSource;
            }
            this.dontPlayChangeSound = false;
        }

        public void openFresh(int startingAreaType, int startingAreaID)
        {
            if ((GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1) || ((GameEngine.Instance.World.GetGlobalWorldID() >= 700) && (GameEngine.Instance.World.GetGlobalWorldID() < 800)))
            {
                this.lblLanguage.Text = "";
            }
            else
            {
                switch (GameEngine.Instance.World.WorldDefaultLanguage)
                {
                    case "en":
                        this.lblLanguage.Text = SK.Text("ChatScreen_English_Only", "Languages: English Only");
                        break;

                    case "de":
                        this.lblLanguage.Text = SK.Text("ChatScreen_German_Only", "Languages: German Only");
                        break;

                    case "fr":
                        this.lblLanguage.Text = SK.Text("ChatScreen_French_Only", "Languages: French Only");
                        break;

                    case "ru":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Russian_Only", "Languages: Russian Only");
                        break;

                    case "es":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Spanish_Only", "Languages: Spanish Only");
                        break;

                    case "pl":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Polish_Only", "Languages: Polish Only");
                        break;

                    case "it":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Italian_Only", "Languages: Italian Only");
                        break;

                    case "tr":
                        this.lblLanguage.Text = SK.Text("ChatScreen_Turkish_Only", "Languages: Turkish Only");
                        break;

                    case "pt":
                        this.lblLanguage.Text = SK.Text("ChatScreen_BrazilianPortuguese_Only", "Languages: Brazilian-Portuguese Only");
                        break;
                }
            }
            this.cbChatUpdate.Checked = Program.mySettings.NotifyChatUpdate;
            this.registeredRooms.Clear();
            this.activeChatRoomIdent = -1;
            this.lastRequestTime = DateTime.MinValue;
            base.Enabled = false;
            this.tbTextInput.Visible = true;
            this.btnSend.Visible = true;
            this.update();
            if (startingAreaType >= 0)
            {
                foreach (ChatRoom room in this.roomsDataSource)
                {
                    if ((room.roomType == startingAreaType) && (room.roomID == startingAreaID))
                    {
                        this.lbRooms.SelectedItem = room;
                        break;
                    }
                }
            }
        }

        public void openUpdate()
        {
            this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
            this.tbTextViewer.ScrollToCaret();
        }

        private void pnlWikiHelp_MouseClick(object sender, MouseEventArgs e)
        {
            CustomSelfDrawPanel.WikiLinkControl.openHelpLink(0x1b);
        }

        private void pnlWikiHelp_MouseEnter(object sender, EventArgs e)
        {
            this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_over;
        }

        private void pnlWikiHelp_MouseLeave(object sender, EventArgs e)
        {
            this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
        }

        private void recreateRooms()
        {
            List<ChatRoom> list = new List<ChatRoom>();
            foreach (ChatRoom room in this.localChatRooms)
            {
                bool flag = false;
                foreach (Chat_RoomID mid in this.registeredRooms)
                {
                    if ((room.roomID == mid.roomID) && (room.roomType == mid.roomType))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    list.Add(room);
                }
            }
            foreach (ChatRoom room2 in list)
            {
                this.localChatRooms[this.createChatRoomIdent(room2.roomType, room2.roomID)] = null;
            }
            this.roomsDataSource.Clear();
            foreach (Chat_RoomID mid2 in this.registeredRooms)
            {
                bool flag2 = false;
                foreach (ChatRoom room3 in this.localChatRooms)
                {
                    if ((room3.roomID == mid2.roomID) && (room3.roomType == mid2.roomType))
                    {
                        flag2 = true;
                        this.roomsDataSource.Add(room3);
                        break;
                    }
                }
                if (!flag2)
                {
                    ChatRoom item = new ChatRoom {
                        roomID = mid2.roomID,
                        roomType = mid2.roomType,
                        text = ""
                    };
                    this.rtb.Text = "";
                    this.rtb.SelectionColor = ARGBColors.Red;
                    for (int i = 0; i < 0x1c; i++)
                    {
                        this.rtb.AppendText("\r\n");
                    }
                    item.text = this.rtb.Rtf;
                    item.roomName = this.getRoomName(item.roomType, item.roomID);
                    this.localChatRooms[this.createChatRoomIdent(item.roomType, item.roomID)] = item;
                    this.roomsDataSource.Add(item);
                }
            }
            if (((this.activeChatRoomIdent < 0) || (this.localChatRooms[this.activeChatRoomIdent] == null)) && (this.registeredRooms.Count > 0))
            {
                Chat_RoomID mid3 = this.registeredRooms[0];
                this.activeChatRoomIdent = this.createChatRoomIdent(mid3.roomType, mid3.roomID);
                this.openChatRoom(this.activeChatRoomIdent);
            }
            this.lbRooms.DataSource = this.roomsDataSource;
            if ((this.activeChatRoomIdent >= 0) && (this.localChatRooms[this.activeChatRoomIdent] != null))
            {
                ChatRoom room5 = (ChatRoom) this.localChatRooms[this.activeChatRoomIdent];
                this.lbRooms.SelectedItem = room5;
            }
        }

        private void splitChatRoomIdent(int roomIdent, ref int roomType, ref int roomID)
        {
            roomType = roomIdent % 10;
            roomID = roomIdent / 10;
        }

        private void splitUsersIntoRooms(List<Chat_UserInRoom> activeUsers)
        {
            if (activeUsers.Count != 0)
            {
                foreach (ChatRoom room in this.localChatRooms)
                {
                    List<string> list = new List<string>();
                    List<Chat_UserInRoom> list2 = new List<Chat_UserInRoom>();
                    foreach (Chat_UserInRoom room2 in activeUsers)
                    {
                        if ((room2.roomType == room.roomType) && (room2.roomID == room.roomID))
                        {
                            list.Add(room2.username);
                            list2.Add(room2);
                        }
                    }
                    if (!this.areListsEqual(list, room.usersInRoom))
                    {
                        room.usersInRoom = list;
                        room.usersDataInRoom = list2;
                        if (this.activeChatRoomIdent == this.createChatRoomIdent(room.roomType, room.roomID))
                        {
                            this.updateUsersListBox();
                        }
                    }
                }
            }
        }

        private void tbTextInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') && !GameEngine.shiftPressed)
            {
                if (this.btnSend.Enabled)
                {
                    this.btnSend_Click(null, null);
                }
                e.Handled = true;
            }
        }

        private void tbTextViewer_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string linkText = e.LinkText;
            if (!linkText.ToLowerInvariant().Contains("http://") && !linkText.ToLowerInvariant().Contains("https://"))
            {
                linkText = "http://" + linkText;
            }
            if (MyMessageBox.Show(SK.Text("CHAT_Link_Warning1", "WARNING : You have clicked on an external link which will open a webpage in your browser. The link you have clicked is") + Environment.NewLine + Environment.NewLine + linkText + Environment.NewLine + Environment.NewLine + SK.Text("CHAT_Link_Warning2", "If you are sure you want to open this webpage, click OK, otherwise click cancel.") + Environment.NewLine + Environment.NewLine, SK.Text("CHAT_Open_Link", "Open External Link"), MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Process.Start(linkText);
            }
        }

        public void update()
        {
            if (Form.ActiveForm == base.ParentForm)
            {
                FlashWindow.Stop(base.ParentForm);
            }
            DateTime now = DateTime.Now;
            TimeSpan span = (TimeSpan) (now - this.lastRequestTime);
            if (this.inSend)
            {
                TimeSpan span2 = (TimeSpan) (now - this.lastSendTime);
                if (span2.TotalSeconds > 3.0)
                {
                    this.inSend = false;
                }
            }
            if (((span.TotalSeconds > this.checkTime) && !this.inSend) && RemoteServices.Instance.ChatActive)
            {
                List<Chat_RoomID> roomsToRegister = this.calcUsersRooms();
                this.inSend = true;
                this.lastSendTime = DateTime.Now;
                RemoteServices.Instance.set_Chat_ReceiveText_UserCallBack(new RemoteServices.Chat_ReceiveText_UserCallBack(this.chat_ReceiveText_UserCallBack));
                if (roomsToRegister.Count == 0)
                {
                    RemoteServices.Instance.Chat_GetText(this.registeredRooms, false);
                }
                else
                {
                    RemoteServices.Instance.Chat_GetText(roomsToRegister, true);
                }
                if (roomsToRegister.Count > 0)
                {
                    this.registeredRooms = roomsToRegister;
                    this.recreateRooms();
                }
            }
        }

        private void updateUsersListBox()
        {
            bool flag = false;
            this.lbActiveChatters.Items.Clear();
            ChatRoom room = (ChatRoom) this.localChatRooms[this.activeChatRoomIdent];
            if (room != null)
            {
                room.usersInRoom.Sort();
                foreach (string str in room.usersInRoom)
                {
                    this.lbActiveChatters.Items.Add(str);
                    if (str == RemoteServices.Instance.UserName)
                    {
                        flag = true;
                    }
                }
                if ((!flag && (room.usersInRoom.Count > 0)) && this.tbTextInput.Visible)
                {
                    MyMessageBox.Show(SK.Text("ChatScreen_Dismiss", "You have been dismissed from chat."), SK.Text("ChatScreen_Chat_Warning", "Chat Warning"));
                    this.tbTextInput.Visible = false;
                    this.btnSend.Visible = false;
                    if (this.m_parent != null)
                    {
                        this.m_parent.close(true, true);
                    }
                    this.activeChatRoomIdent = -1;
                    this.localChatRooms = new SparseArray();
                }
            }
        }

        public class ChatRoom
        {
            public bool newText;
            public int roomID = -1;
            public string roomName = "";
            public int roomType = -1;
            public string text = "";
            public List<Chat_UserInRoom> usersDataInRoom = new List<Chat_UserInRoom>();
            public List<string> usersInRoom = new List<string>();

            public override string ToString()
            {
                if (this.newText)
                {
                    return (this.roomName + "*");
                }
                return this.roomName;
            }
        }
    }
}

