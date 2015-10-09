namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class MailScreen : CustomSelfDrawPanel, IDockableControl
    {
        private bool aggressiveBlocking;
        private MailAttachmentPopup attachmentWindow;
        private bool blockButtonAvailable;
        private List<string> blockedList = new List<string>();
        private bool blockListChanged;
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private int currentSearchTab = -1;
        private int currentThreadLength;
        private MyMessageBoxPopUp deleteFolderPopUp;
        private MyMessageBoxPopUp DeleteThreadPopUp;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton dockButton = new CustomSelfDrawPanel.CSDButton();
        private bool doUpdateSendButton;
        private bool downloaded3Days = true;
        private bool downloadedAll = true;
        private bool downloadedThisMonth = true;
        private bool downloadedThisWeek = true;
        private bool downloadedYesterday = true;
        private static bool factionClose;
        private int flashFolderCount;
        private const int flashFolderSpeed = 6;
        private static bool fromFaction;
        private bool gotFolders;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel headerLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private bool inDisplayThread;
        private bool initialRequest = true;
        private DateTime keyScrollTimer = DateTime.MinValue;
        private DateTime lastClicked = DateTime.MinValue;
        private int lastLineClicked = -1;
        private int lastMailItemClicked = -1;
        private int lastMailLineClicked = -1;
        private string lastSubject = "";
        private DateTime lastTimeThreadsReceived = DateTime.MinValue;
        private double lastUpdateTime;
        private MailFolderLine m_flashFolderLine;
        private List<MailFolderItem> m_mailFolders = new List<MailFolderItem>();
        private bool m_moveThreadMode;
        private MailScreenManager m_parent;
        private List<MailThreadListItem> m_preSortedALLHeader = new List<MailThreadListItem>();
        private List<MailThreadListItem> m_preSortedHeaders = new List<MailThreadListItem>();
        private SparseArray m_storedThreadHeaders = new SparseArray();
        private SparseArray m_storedThreads = new SparseArray();
        private CustomSelfDrawPanel.CSDArea mailCreateFolderArea = new CustomSelfDrawPanel.CSDArea();
        private string[] mailFavourites;
        private DateTime mailLineDoubleClick = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDImage mailList_createFolderBack = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton mailList_createFolderCancel = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mailList_createFolderHeader = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel mailList_createFolderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton mailList_createFolderOK = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailList_downArrow = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea mailList_folderArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_folderHeaderImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel mailList_folderHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private MailFolderLine mailList_folderLine01 = new MailFolderLine();
        private MailFolderLine mailList_folderLine02 = new MailFolderLine();
        private MailFolderLine mailList_folderLine03 = new MailFolderLine();
        private MailFolderLine mailList_folderLine04 = new MailFolderLine();
        private MailFolderLine mailList_folderLine05 = new MailFolderLine();
        private MailFolderLine mailList_folderLine06 = new MailFolderLine();
        private MailFolderLine mailList_folderLine07 = new MailFolderLine();
        private MailFolderLine mailList_folderLine08 = new MailFolderLine();
        private MailFolderLine mailList_folderLine09 = new MailFolderLine();
        private MailFolderLine mailList_folderLine10 = new MailFolderLine();
        private MailFolderLine mailList_folderLine11 = new MailFolderLine();
        private MailFolderLine mailList_folderLine12 = new MailFolderLine();
        private MailFolderLine mailList_folderLine13 = new MailFolderLine();
        private MailFolderLine mailList_folderLine14 = new MailFolderLine();
        private MailFolderLine mailList_folderLine15 = new MailFolderLine();
        private MailFolderLine mailList_folderLine16 = new MailFolderLine();
        private MailFolderLine mailList_folderLine17 = new MailFolderLine();
        private MailFolderLine mailList_folderLine18 = new MailFolderLine();
        private MailFolderLine mailList_folderLine19 = new MailFolderLine();
        private MailFolderLine mailList_folderLine20 = new MailFolderLine();
        private MailFolderLine mailList_folderLine21 = new MailFolderLine();
        private MailFolderLine mailList_folderLine22 = new MailFolderLine();
        private MailFolderLine mailList_folderLine23 = new MailFolderLine();
        private MailFolderLine mailList_folderLine24 = new MailFolderLine();
        private MailFolderLine mailList_folderLine25 = new MailFolderLine();
        private MailFolderLine mailList_folderLine26 = new MailFolderLine();
        private MailFolderLine mailList_folderLine27 = new MailFolderLine();
        private CustomSelfDrawPanel.CSDImage mailList_folderShadowB = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_folderShadowBL = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_folderShadowBR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_folderShadowR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_folderShadowTR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea mailList_iconArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton mailList_iconNewMail = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mailList_iconSelected = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton mailList_iconSelectedArchive = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mailList_iconSelectedBack = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton mailList_iconSelectedBlockPlayer = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailList_iconSelectedDelete = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mailList_iconSelectedIcon = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel mailList_iconSelectedLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton mailList_iconSelectedMoveThread = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailList_iconSelectedOpen = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailList_iconSelectedRead = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailList_iconSelectedUnread = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea mailList_listArea = new CustomSelfDrawPanel.CSDArea();
        private MailListLine mailList_listLine01 = new MailListLine();
        private MailListLine mailList_listLine02 = new MailListLine();
        private MailListLine mailList_listLine03 = new MailListLine();
        private MailListLine mailList_listLine04 = new MailListLine();
        private MailListLine mailList_listLine05 = new MailListLine();
        private MailListLine mailList_listLine06 = new MailListLine();
        private MailListLine mailList_listLine07 = new MailListLine();
        private MailListLine mailList_listLine08 = new MailListLine();
        private MailListLine mailList_listLine09 = new MailListLine();
        private MailListLine mailList_listLine10 = new MailListLine();
        private MailListLine mailList_listLine11 = new MailListLine();
        private MailListLine mailList_listLine12 = new MailListLine();
        private MailListLine mailList_listLine13 = new MailListLine();
        private MailListLine mailList_listLine14 = new MailListLine();
        private MailListLine mailList_listLine15 = new MailListLine();
        private MailListLine mailList_listLine16 = new MailListLine();
        private MailListLine mailList_listLine17 = new MailListLine();
        private MailListLine mailList_listLine18 = new MailListLine();
        private MailListLine mailList_listLine19 = new MailListLine();
        private MailListLine mailList_listLine20 = new MailListLine();
        private MailListLine mailList_listLine21 = new MailListLine();
        private MailListLine mailList_listLine22 = new MailListLine();
        private MailListLine mailList_listLine23 = new MailListLine();
        private MailListLine mailList_listLine24 = new MailListLine();
        private MailListLine mailList_listLine25 = new MailListLine();
        private MailListLine mailList_listLine26 = new MailListLine();
        private MailListLine mailList_listLine27 = new MailListLine();
        private CustomSelfDrawPanel.CSDImage mailList_listShadowB = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_listShadowBL = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_listShadowBR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_listShadowR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailList_listShadowTR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage1 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage3 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage4 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel mailList_mainHeaderLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mailList_mainHeaderLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mailList_mainHeaderLabel4 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton mailList_manageBlocked = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDControl mailList_mouseWheelArea = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDButton mailList_MoveFolderCancel = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel mailList_MoveFolderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDVertScrollBar mailList_scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage mailList_scrollTabLines = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton mailList_upArrow = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel mailList_userFilterLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea mailListArea = new CustomSelfDrawPanel.CSDArea();
        private bool mailSent;
        private CustomSelfDrawPanel.CSDButton mailThread_downArrow = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mailThread_fromShield = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea mailThread_iconArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton mailThread_iconBack = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mailThread_iconSelected = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailThread_iconSelectedBack = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedBlockPoster = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedForward = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage mailThread_iconSelectedIcon = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel mailThread_iconSelectedLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedReply = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedReportMail = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedView = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea mailThread_listArea = new CustomSelfDrawPanel.CSDArea();
        private MailThreadLine mailThread_listLine01 = new MailThreadLine();
        private MailThreadLine mailThread_listLine02 = new MailThreadLine();
        private MailThreadLine mailThread_listLine03 = new MailThreadLine();
        private MailThreadLine mailThread_listLine04 = new MailThreadLine();
        private MailThreadLine mailThread_listLine05 = new MailThreadLine();
        private MailThreadLine mailThread_listLine06 = new MailThreadLine();
        private MailThreadLine mailThread_listLine07 = new MailThreadLine();
        private MailThreadLine mailThread_listLine08 = new MailThreadLine();
        private MailThreadLine mailThread_listLine09 = new MailThreadLine();
        private MailThreadLine mailThread_listLine10 = new MailThreadLine();
        private MailThreadLine mailThread_listLine11 = new MailThreadLine();
        private MailThreadLine mailThread_listLine12 = new MailThreadLine();
        private MailThreadLine mailThread_listLine13 = new MailThreadLine();
        private MailThreadLine mailThread_listLine14 = new MailThreadLine();
        private MailThreadLine mailThread_listLine15 = new MailThreadLine();
        private MailThreadLine mailThread_listLine16 = new MailThreadLine();
        private MailThreadLine mailThread_listLine17 = new MailThreadLine();
        private MailThreadLine mailThread_listLine18 = new MailThreadLine();
        private MailThreadLine mailThread_listLine19 = new MailThreadLine();
        private MailThreadLine mailThread_listLine20 = new MailThreadLine();
        private MailThreadLine mailThread_listLine21 = new MailThreadLine();
        private MailThreadLine mailThread_listLine22 = new MailThreadLine();
        private MailThreadLine mailThread_listLine23 = new MailThreadLine();
        private MailThreadLine mailThread_listLine24 = new MailThreadLine();
        private MailThreadLine mailThread_listLine25 = new MailThreadLine();
        private MailThreadLine mailThread_listLine26 = new MailThreadLine();
        private MailThreadLine mailThread_listLine27 = new MailThreadLine();
        private CustomSelfDrawPanel.CSDImage mailThread_listShadowB = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailThread_listShadowBL = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailThread_listShadowBR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailThread_listShadowR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage mailThread_listShadowTR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDFill mailThread_mailBodyBack = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDScrollLabel mailThread_mailBodyText = new CustomSelfDrawPanel.CSDScrollLabel();
        private CustomSelfDrawPanel.CSDFill mailThread_mailHeaderBack = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderDateLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderDateValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderFromLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderFromNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailThread_mainHeaderImage1 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailThread_mainHeaderImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel mailThread_mainHeaderImage3 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel mailThread_mainHeaderLabel1 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mailThread_mainHeaderLabel2 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel mailThread_mainHeaderLabel3 = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl mailThread_mouseWheelArea = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDButton mailThread_openAttachments = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDVertScrollBar mailThread_scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage mailThread_scrollTabLines = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton mailThread_upArrow = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea mailThreadArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton mailThreadBody_downArrow = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDControl mailThreadBody_mouseWheelArea = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDVertScrollBar mailThreadBody_scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage mailThreadBody_scrollTabLines = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton mailThreadBody_upArrow = new CustomSelfDrawPanel.CSDButton();
        private MailThreadComparer mailThreadComparer = new MailThreadComparer();
        private MailThreadHeaderComparer mailThreadHeaderComparer = new MailThreadHeaderComparer();
        private string[] mailUsersHistory;
        private CustomSelfDrawPanel.CSDExtendingPanel mainBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDArea mainBodyArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea mainHeaderArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton newMail_addAttachments = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage newMail_bodyShadowB = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage newMail_bodyShadowBL = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage newMail_bodyShadowBR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage newMail_bodyShadowR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage newMail_bodyShadowTR = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage newMail_breakbar = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea newMail_iconArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton newMail_iconBackButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage newMail_iconBackground = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton newMail_iconFavouritesAddButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDListBox newMail_iconFavouritesList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton newMail_iconFavouritesRemoveButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton newMail_iconFindAddButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton newMail_iconFindFavouritesButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDListBox newMail_iconFindList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton newMail_iconKnownAddButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton newMail_iconKnownFavouritesButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDListBox newMail_iconKnownList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton newMail_iconRecentAddButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton newMail_iconRecentFavouritesButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDListBox newMail_iconRecentList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton newMail_iconSendMail = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea newMail_iconTab1Area = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton newMail_iconTab1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea newMail_iconTab2Area = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton newMail_iconTab2Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea newMail_iconTab3Area = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton newMail_iconTab3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea newMail_iconTab4Area = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton newMail_iconTab4Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDFill newMail_mailBodyBack = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDFill newMail_mailHeaderBack = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDArea newMail_newMailArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton newMail_removeRecipient = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLine newMail_separater = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDLine newMail_separater2 = new CustomSelfDrawPanel.CSDLine();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel newMail_SubjectBorder = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel newMail_SubjectLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel newMail_ToLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDListBox newMail_ToList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDArea newMailArea = new CustomSelfDrawPanel.CSDArea();
        private bool open3Days = true;
        private bool openAll;
        private bool openThisMonth;
        private bool openThisWeek = true;
        private bool openYesterday = true;
        private List<MailLink> outputListExtUnity = new List<MailLink>();
        private bool proclamation;
        private List<string> recipients = new List<string>();
        private bool reportButtonAvailable;
        private long selectedFolderID = -1L;
        private long selectedMailItemID = -1000L;
        private long selectedMailThreadID = -1000L;
        private List<long> selectedMailThreadIDList = new List<long>();
        private string selectedUserName = "";
        private bool sendAsForward;
        private long sendThreadID = -1L;
        private int specialArea;
        private int specialType;
        private TextBox tbFindInput;
        private TextBox tbMain;
        private TextBox tbNewFolder;
        private TextBox tbSubject;
        private TextBox tbUserFilter;

        public MailScreen()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            this.tbMain.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
            this.tbSubject.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void addBreakBars()
        {
            DateTime time = VillageMap.getCurrentServerTime();
            MailThreadListItem item = new MailThreadListItem {
                mailThreadID = -1L,
                mailTime = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1)
            };
            this.m_storedThreadHeaders[-1] = item;
            MailThreadListItem item2 = new MailThreadListItem {
                mailThreadID = -2L
            };
            item2.mailTime = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1).AddDays(-1.0);
            this.m_storedThreadHeaders[-2] = item2;
            MailThreadListItem item3 = new MailThreadListItem {
                mailThreadID = -3L
            };
            item3.mailTime = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1).AddDays(-3.0);
            this.m_storedThreadHeaders[-3] = item3;
            MailThreadListItem item4 = new MailThreadListItem {
                mailThreadID = -4L
            };
            item4.mailTime = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1).AddDays(-7.0);
            this.m_storedThreadHeaders[-4] = item4;
        }

        private void addFavouritesNameToRecipients()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconFavouritesList.getSelectedItem();
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void addFindNameToFavourites()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconFindList.getSelectedItem();
            if (item != null)
            {
                this.addNameToFavourites(item.Text);
            }
        }

        private void addFindNameToRecipients()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconFindList.getSelectedItem();
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void addKnownNameToFavourites()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconKnownList.getSelectedItem();
            if (item != null)
            {
                this.addNameToFavourites(item.Text);
            }
        }

        private void addKnownNameToRecipients()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconKnownList.getSelectedItem();
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void addNameToFavourites(string name)
        {
            List<string> list = new List<string>();
            if (this.mailFavourites != null)
            {
                foreach (string str in this.mailFavourites)
                {
                    if (str == name)
                    {
                        return;
                    }
                    list.Add(str);
                }
            }
            list.Add(name);
            this.mailFavourites = list.ToArray();
            RemoteServices.Instance.AddUserToFavourites(name);
            this.fillFavouritesList();
        }

        private void addNameToRecent(string name)
        {
            List<string> list = new List<string>();
            if (this.mailUsersHistory != null)
            {
                foreach (string str in this.mailUsersHistory)
                {
                    if (str == name)
                    {
                        return;
                    }
                    list.Add(str);
                }
            }
            list.Add(name);
            this.mailUsersHistory = list.ToArray();
            this.fillRecentList();
        }

        private void addNameToRecipients(string name)
        {
            if (!this.recipients.Contains(name) && (this.recipients.Count < 40))
            {
                this.recipients.Add(name);
                this.populateToList();
            }
        }

        private void addRecentNameToFavourites()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconRecentList.getSelectedItem();
            if (item != null)
            {
                this.addNameToFavourites(item.Text);
            }
        }

        private void addRecentNameToRecipients()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconRecentList.getSelectedItem();
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void bodyTextHeightChanged(int textHeight)
        {
            this.mailThreadBody_scrollBar.Value = 0;
            this.mailThreadBody_scrollBar.Max = Math.Max(0, textHeight - this.mailThread_mailBodyText.Height);
            this.mailThreadBody_scrollBar.invalidate();
            this.mailThread_mailBodyText.VerticalOffset = 0;
        }

        private void changeSearchTab(int tab, bool fromClick)
        {
            this.currentSearchTab = tab;
            this.newMail_iconTab1Button.ImageNorm = (Image) GFXLibrary.mail2_users_find_normal;
            this.newMail_iconTab1Button.ImageOver = (Image) GFXLibrary.mail2_users_find_over;
            this.newMail_iconTab1Button.ImageClick = (Image) GFXLibrary.mail2_users_find_normal;
            this.newMail_iconTab2Button.ImageNorm = (Image) GFXLibrary.mail2_users_recent_normal;
            this.newMail_iconTab2Button.ImageOver = (Image) GFXLibrary.mail2_users_recent_over;
            this.newMail_iconTab2Button.ImageClick = (Image) GFXLibrary.mail2_users_recent_normal;
            this.newMail_iconTab3Button.ImageNorm = (Image) GFXLibrary.mail2_users_favourites_normal;
            this.newMail_iconTab3Button.ImageOver = (Image) GFXLibrary.mail2_users_favourites_over;
            this.newMail_iconTab3Button.ImageClick = (Image) GFXLibrary.mail2_users_favourites_normal;
            this.newMail_iconTab4Button.ImageNorm = (Image) GFXLibrary.mail2_users_groups_normal;
            this.newMail_iconTab4Button.ImageOver = (Image) GFXLibrary.mail2_users_groups_over;
            this.newMail_iconTab4Button.ImageClick = (Image) GFXLibrary.mail2_users_groups_normal;
            this.newMail_iconTab1Area.Visible = false;
            this.newMail_iconTab2Area.Visible = false;
            this.newMail_iconTab3Area.Visible = false;
            this.newMail_iconTab4Area.Visible = false;
            switch (tab)
            {
                case 0:
                    this.newMail_iconTab1Button.ImageNorm = (Image) GFXLibrary.mail2_users_find_selected;
                    this.newMail_iconTab1Button.ImageOver = (Image) GFXLibrary.mail2_users_find_selected;
                    this.newMail_iconTab1Button.ImageClick = (Image) GFXLibrary.mail2_users_find_selected;
                    this.newMail_iconTab1Area.Visible = true;
                    break;

                case 1:
                    this.newMail_iconTab2Button.ImageNorm = (Image) GFXLibrary.mail2_users_recent_selected;
                    this.newMail_iconTab2Button.ImageOver = (Image) GFXLibrary.mail2_users_recent_selected;
                    this.newMail_iconTab2Button.ImageClick = (Image) GFXLibrary.mail2_users_recent_selected;
                    this.newMail_iconTab2Area.Visible = true;
                    break;

                case 2:
                    this.newMail_iconTab3Button.ImageNorm = (Image) GFXLibrary.mail2_users_favourites_selected;
                    this.newMail_iconTab3Button.ImageOver = (Image) GFXLibrary.mail2_users_favourites_selected;
                    this.newMail_iconTab3Button.ImageClick = (Image) GFXLibrary.mail2_users_favourites_selected;
                    this.newMail_iconTab3Area.Visible = true;
                    break;

                case 3:
                    this.newMail_iconTab4Button.ImageNorm = (Image) GFXLibrary.mail2_users_groups_selected;
                    this.newMail_iconTab4Button.ImageOver = (Image) GFXLibrary.mail2_users_groups_selected;
                    this.newMail_iconTab4Button.ImageClick = (Image) GFXLibrary.mail2_users_groups_selected;
                    this.newMail_iconTab4Area.Visible = true;
                    break;
            }
            this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
            switch (tab)
            {
                case 0:
                    if (this.newMail_iconFindList.getSelectedItem() != null)
                    {
                        this.newMail_iconFindAddButton.Enabled = true;
                        this.newMail_iconFindFavouritesButton.Enabled = true;
                        break;
                    }
                    this.newMail_iconFindAddButton.Enabled = false;
                    this.newMail_iconFindFavouritesButton.Enabled = false;
                    break;

                case 1:
                    this.fillRecentList();
                    if (this.newMail_iconRecentList.getSelectedItem() != null)
                    {
                        this.newMail_iconRecentAddButton.Enabled = true;
                        this.newMail_iconRecentFavouritesButton.Enabled = true;
                        return;
                    }
                    this.newMail_iconRecentAddButton.Enabled = false;
                    this.newMail_iconRecentFavouritesButton.Enabled = false;
                    return;

                case 2:
                    this.fillFavouritesList();
                    if (this.newMail_iconFavouritesList.getSelectedItem() != null)
                    {
                        this.newMail_iconFavouritesAddButton.Enabled = true;
                        this.newMail_iconFavouritesRemoveButton.Enabled = true;
                        return;
                    }
                    this.newMail_iconFavouritesAddButton.Enabled = false;
                    this.newMail_iconFavouritesRemoveButton.Enabled = false;
                    return;

                case 3:
                    this.fillKnownList();
                    if (this.newMail_iconKnownList.getSelectedItem() != null)
                    {
                        this.newMail_iconKnownAddButton.Enabled = true;
                        this.newMail_iconKnownFavouritesButton.Enabled = true;
                        return;
                    }
                    this.newMail_iconKnownAddButton.Enabled = false;
                    this.newMail_iconKnownFavouritesButton.Enabled = false;
                    return;

                default:
                    return;
            }
            if (fromClick)
            {
                this.tbFindInput.Focus();
            }
        }

        private void clearMailThread()
        {
            for (int i = 0; i < 0x1b; i++)
            {
                MailThreadLine line = this.getMailThreadLine(i);
                line.BodyText.Text = "";
                line.Sender.Text = "";
                line.DateLabel.Text = "";
                line.Icon.Image = null;
                line.reset();
                line.BodyText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            }
            this.mailThread_mailHeaderFromNameLabel.Text = "";
            this.mailThread_mailHeaderDateValueLabel.Text = "";
            this.mailThread_fromShield.Visible = false;
        }

        public void clearStoredMail()
        {
            this.m_storedThreadHeaders = new SparseArray();
            this.m_storedThreads = new SparseArray();
            this.initialRequest = true;
            this.gotFolders = false;
            this.downloadedYesterday = false;
            this.downloaded3Days = false;
            this.downloadedThisWeek = false;
            this.downloadedThisMonth = false;
            this.downloadedAll = false;
            this.lastTimeThreadsReceived = DateTime.Now.AddYears(-50);
        }

        public void closeAttachmentsPopup(bool clearContents)
        {
            if (this.attachmentWindow != null)
            {
                if (clearContents)
                {
                    this.attachmentWindow.clearContents(true);
                }
                this.attachmentWindow.closeControl(true);
            }
        }

        public void closeClick()
        {
            if (this.attachmentWindow != null)
            {
                this.attachmentWindow.closeControl(true);
                this.attachmentWindow = null;
            }
            if (this.m_parent != null)
            {
                if (this.m_parent.isDocked())
                {
                    if (!factionClose)
                    {
                        InterfaceMgr.Instance.changeTab(0);
                    }
                    else
                    {
                        GameEngine.Instance.setNextFactionPage(0x3e7);
                        InterfaceMgr.Instance.changeTab(8);
                    }
                }
                else
                {
                    this.m_parent.close(true);
                }
            }
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        private void CloseDeleteFolderPopUp()
        {
            if (this.deleteFolderPopUp != null)
            {
                if (this.deleteFolderPopUp.Created)
                {
                    this.deleteFolderPopUp.Close();
                }
                InterfaceMgr.Instance.closeGreyOut();
                this.deleteFolderPopUp = null;
            }
        }

        private void CloseDeleteThreadPopUp()
        {
            if (this.DeleteThreadPopUp != null)
            {
                if (this.DeleteThreadPopUp.Created)
                {
                    this.DeleteThreadPopUp.Close();
                }
                InterfaceMgr.Instance.closeGreyOut();
                this.DeleteThreadPopUp = null;
            }
        }

        private void closeMoveMail()
        {
            this.m_moveThreadMode = false;
            this.mailList_listArea.Visible = true;
            this.mailList_iconArea.Visible = true;
            this.mailList_scrollBar.Visible = true;
            this.mailList_scrollTabLines.Visible = true;
            this.mailList_upArrow.Visible = true;
            this.mailList_downArrow.Visible = true;
            this.mailList_listShadowTR.Visible = true;
            this.mailList_listShadowR.Visible = true;
            this.mailList_listShadowBR.Visible = true;
            this.mailList_listShadowB.Visible = true;
            this.mailList_listShadowBL.Visible = true;
            this.mailList_MoveFolderLabel.Visible = false;
            this.mailList_MoveFolderCancel.Visible = false;
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        private void copyTextToClipboardClick()
        {
            try
            {
                if ((base.ClickedControl != null) && (base.ClickedControl.GetType() == typeof(CustomSelfDrawPanel.CSDLabel)))
                {
                    CustomSelfDrawPanel.CSDLabel clickedControl = (CustomSelfDrawPanel.CSDLabel) base.ClickedControl;
                    Clipboard.SetText(clickedControl.Text);
                }
                if ((base.ClickedControl != null) && (base.ClickedControl.GetType() == typeof(CustomSelfDrawPanel.CSDScrollLabel)))
                {
                    CustomSelfDrawPanel.CSDScrollLabel label2 = (CustomSelfDrawPanel.CSDScrollLabel) base.ClickedControl;
                    Clipboard.SetText(label2.Text);
                }
            }
            catch (Exception)
            {
            }
        }

        private void createMailFolderCallback(CreateMailFolder_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.m_mailFolders.Clear();
                this.m_mailFolders.AddRange(returnData.folders);
                this.updateFolderList();
            }
        }

        private void DeleteThreadOkClick()
        {
            foreach (long num in this.selectedMailThreadIDList)
            {
                RemoteServices.Instance.DeleteMailThread(num);
                Thread.Sleep(200);
                this.m_storedThreads[num] = null;
                this.m_storedThreadHeaders[num] = null;
            }
            this.saveMail();
            this.selectedMailThreadID = -1000L;
            this.selectedMailThreadIDList.Clear();
            this.preSortThreadHeaders();
            this.repopulateTable();
            InterfaceMgr.Instance.closeGreyOut();
            this.DeleteThreadPopUp.Close();
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y, false, true);
        }

        private void displayThread(long threadID, bool fromOpen)
        {
            if (!this.inDisplayThread)
            {
                this.inDisplayThread = true;
                if (this.m_storedThreads[threadID] == null)
                {
                    this.returnToMailList();
                    this.inDisplayThread = false;
                }
                else
                {
                    if (fromOpen)
                    {
                        this.selectedMailItemID = -1L;
                    }
                    MailThreadItem[] itemArray = (MailThreadItem[]) this.m_storedThreads[threadID];
                    MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[threadID];
                    int length = itemArray.Length;
                    this.currentThreadLength = length;
                    for (int i = 0; i < itemArray.Length; i++)
                    {
                        MailThreadItem item2 = itemArray[i];
                        if (this.blockedList.Contains(item2.otherUser))
                        {
                            length--;
                        }
                    }
                    int num3 = this.mailThread_scrollBar.Max + 0x1b;
                    if (num3 > length)
                    {
                        int num4 = Math.Max(0, length - 0x1b);
                        if (this.mailThread_scrollBar.Value > num4)
                        {
                            this.mailThread_scrollBar.Value = num4;
                        }
                        this.mailThread_scrollBar.Max = num4;
                    }
                    else
                    {
                        this.mailThread_scrollBar.Max = Math.Max(0, length - 0x1b);
                    }
                    this.clearMailThread();
                    char[] separator = new char[] { '\n', '\r' };
                    int index = this.mailThread_scrollBar.Value;
                    int lineClicked = -1;
                    for (int j = 0; j < itemArray.Length; j++)
                    {
                        MailThreadItem item3 = itemArray[j];
                        if (!this.blockedList.Contains(item3.otherUser) && !item3.readStatus)
                        {
                            lineClicked = j;
                        }
                    }
                    for (int k = 0; k < 0x1b; k++)
                    {
                        this.getMailThreadLine(k).Data = -1;
                    }
                    int lineID = 0;
                    while ((lineID < 0x1b) && (index < itemArray.Length))
                    {
                        MailThreadItem item4 = itemArray[index];
                        bool flag = false;
                        if (this.blockedList.Contains(item4.otherUser))
                        {
                            lineID--;
                        }
                        else
                        {
                            MailThreadLine line2 = this.getMailThreadLine(lineID);
                            line2.Data = index;
                            string otherUser = SK.Text("MAILSCREEN_NO_RECIPIENTS", "No Recipients?");
                            if ((item4.otherUser != null) && (item4.otherUser.Length > 0))
                            {
                                otherUser = item4.otherUser;
                            }
                            if (((item != null) && item.readOnly) && (item.specialType == 1))
                            {
                                otherUser = SK.Text("The_Kingdoms_Team", "The Kingdoms Team");
                            }
                            string body = item4.body;
                            if (((item != null) && item.readOnly) && (item.specialType == 1))
                            {
                                body = this.languageSplitString(body);
                            }
                            string[] strArray = body.Split(separator);
                            if ((strArray.Length > 0) && !flag)
                            {
                                string str3 = this.parseAttachmentString(strArray[0], false);
                                line2.BodyText.Text = str3;
                                if (str3 != strArray[0])
                                {
                                    line2.hasAttachment = true;
                                }
                            }
                            else
                            {
                                line2.BodyText.Text = "                   * " + SK.Text("MailBlock_blocked", "Blocked") + " *";
                            }
                            line2.Sender.Text = otherUser;
                            line2.Date = item4.mailTime;
                            if (item4.readStatus)
                            {
                                line2.Icon.Image = (Image) GFXLibrary.mail_letter_icon_open;
                            }
                            else
                            {
                                line2.Icon.Image = (Image) GFXLibrary.mail_letter_icon_closed;
                                line2.BodyText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                            }
                            if (item4.mailID == this.selectedMailItemID)
                            {
                                line2.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                                line2.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                            }
                            line2.invalidate();
                        }
                        lineID++;
                        index++;
                    }
                    if (this.selectedMailItemID >= 0L)
                    {
                        this.mailThread_iconSelected.Visible = true;
                        this.mailThread_iconSelectedBack.Visible = true;
                    }
                    else
                    {
                        this.mailThread_iconSelected.Visible = false;
                        this.mailThread_iconSelectedBack.Visible = false;
                    }
                    this.mailThread_scrollBar.recalc();
                    this.mailThread_scrollBar.invalidate();
                    this.mailThread_scrollBarMoved();
                    if ((length > 0) && fromOpen)
                    {
                        bool flag2 = true;
                        if (lineClicked >= 0)
                        {
                            this.showMailItem(lineClicked);
                            flag2 = false;
                        }
                        index = this.mailThread_scrollBar.Value;
                        int num10 = 0;
                        while ((num10 < 0x1b) && (index < itemArray.Length))
                        {
                            MailThreadItem item5 = itemArray[index];
                            if (this.blockedList.Contains(item5.otherUser))
                            {
                                num10--;
                            }
                            else
                            {
                                if (flag2)
                                {
                                    this.showMailItem(index);
                                    flag2 = false;
                                    lineClicked = index;
                                }
                                MailThreadLine line3 = this.getMailThreadLine(num10);
                                if (lineClicked == index)
                                {
                                    this.selectedMailItemID = item5.mailID;
                                    line3.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                                    line3.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                                    line3.invalidate();
                                    this.mailThread_iconSelected.Visible = true;
                                    this.mailThread_iconSelectedBack.Visible = true;
                                    if (item5.otherUser != RemoteServices.Instance.UserName)
                                    {
                                        this.mailThread_iconSelectedBlockPoster.Enabled = true;
                                        this.selectedUserName = item5.otherUser;
                                        if (this.blockedList.Contains(this.selectedUserName))
                                        {
                                            this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailBlock_manage", "Manage Blocked Users");
                                        }
                                        else
                                        {
                                            this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
                                        }
                                    }
                                    else
                                    {
                                        this.mailThread_iconSelectedBlockPoster.Enabled = false;
                                        this.selectedUserName = "";
                                        this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
                                    }
                                    break;
                                }
                            }
                            num10++;
                            index++;
                        }
                    }
                    this.inDisplayThread = false;
                }
            }
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
            if (this.m_parent != null)
            {
                this.m_parent.setAsReopen();
                if (this.m_parent.isDocked())
                {
                    this.m_parent.open(false, true);
                    InterfaceMgr.Instance.changeTab(0);
                }
                else
                {
                    this.m_parent.setAsDocked();
                    InterfaceMgr.Instance.getMainTabBar().selectDummyTab(0x15);
                }
            }
        }

        private bool doesFolderAlreadyExist()
        {
            if (this.tbNewFolder.Text.Length == 0)
            {
                return true;
            }
            foreach (MailFolderItem item in this.m_mailFolders)
            {
                if (string.Compare(item.title, this.tbNewFolder.Text, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void fillFavouritesList()
        {
            List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
            if (this.mailFavourites != null)
            {
                foreach (string str in this.mailFavourites)
                {
                    CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                        Text = str
                    };
                    items.Add(item);
                }
            }
            this.newMail_iconFavouritesList.populate(items);
        }

        private void fillKnownList()
        {
            List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
            bool uptodate = false;
            if (RemoteServices.Instance.UserFactionID >= 0)
            {
                FactionMemberData[] dataArray = GameEngine.Instance.World.getFactionMemberData(RemoteServices.Instance.UserFactionID, ref uptodate);
                if (dataArray != null)
                {
                    foreach (FactionMemberData data in dataArray)
                    {
                        if (data.userID != RemoteServices.Instance.UserID)
                        {
                            CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                                Text = data.userName
                            };
                            items.Add(item);
                        }
                    }
                }
            }
            List<UserRelationship> userRelations = GameEngine.Instance.World.UserRelations;
            if ((userRelations != null) && (userRelations.Count > 0))
            {
                foreach (UserRelationship relationship in userRelations)
                {
                    if (relationship.friendly)
                    {
                        CustomSelfDrawPanel.CSDListItem item2 = new CustomSelfDrawPanel.CSDListItem {
                            Text = relationship.userName
                        };
                        items.Add(item2);
                    }
                }
            }
            this.newMail_iconKnownList.populate(items);
        }

        private void fillRecentList()
        {
            List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
            if (this.mailUsersHistory != null)
            {
                foreach (string str in this.mailUsersHistory)
                {
                    CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                        Text = str
                    };
                    items.Add(item);
                }
            }
            this.newMail_iconRecentList.populate(items);
        }

        private void flagMailCallback(FlagMailRead_ReturnType returnData)
        {
            bool success = returnData.Success;
        }

        private void flagUpdateSendButton()
        {
            this.doUpdateSendButton = true;
        }

        private void folderLineClicked()
        {
            if (base.ClickedControl != null)
            {
                int data = base.ClickedControl.Data;
                if (data == 0)
                {
                    if (!this.m_moveThreadMode)
                    {
                        this.selectedFolderID = -1L;
                    }
                    else
                    {
                        long targetFolderID = -1L;
                        if (this.m_storedThreadHeaders[this.selectedMailThreadID] != null)
                        {
                            MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[this.selectedMailThreadID];
                            if (targetFolderID != item.folderID)
                            {
                                this.moveMail(this.selectedMailThreadIDList, targetFolderID, this.getFolderLine(0));
                            }
                            else
                            {
                                this.closeMoveMail();
                            }
                        }
                    }
                    this.preSortThreadHeaders();
                    this.repopulateTable();
                    this.selectedMailThreadID = -1000L;
                    this.selectedMailThreadIDList.Clear();
                }
                else
                {
                    data--;
                    int count = this.m_mailFolders.Count;
                    if (count >= 0x18)
                    {
                        count = 0x18;
                    }
                    if (data < count)
                    {
                        if (!this.m_moveThreadMode)
                        {
                            this.selectedFolderID = this.m_mailFolders[data].mailFolderID;
                        }
                        else
                        {
                            long mailFolderID = this.m_mailFolders[data].mailFolderID;
                            if (this.m_storedThreadHeaders[this.selectedMailThreadID] != null)
                            {
                                MailThreadListItem item2 = (MailThreadListItem) this.m_storedThreadHeaders[this.selectedMailThreadID];
                                if (mailFolderID != item2.folderID)
                                {
                                    this.moveMail(this.selectedMailThreadIDList, mailFolderID, this.getFolderLine(data + 1));
                                }
                                else
                                {
                                    this.closeMoveMail();
                                }
                            }
                        }
                        this.preSortThreadHeaders();
                        this.repopulateTable();
                        this.selectedMailThreadID = -1000L;
                        this.selectedMailThreadIDList.Clear();
                    }
                    else if (!this.m_moveThreadMode)
                    {
                        if (data == count)
                        {
                            this.mailListArea.Visible = false;
                            this.mailThreadArea.Visible = false;
                            this.newMailArea.Visible = false;
                            this.mailCreateFolderArea.Visible = true;
                            this.tbNewFolder.Text = "";
                            this.tbNewFolder.Visible = true;
                            this.tbNewFolder.MaxLength = 10;
                            this.mailList_createFolderOK.Enabled = false;
                        }
                        else
                        {
                            this.CloseDeleteFolderPopUp();
                            InterfaceMgr.Instance.openGreyOutWindow(false, base.ParentForm);
                            this.deleteFolderPopUp = new MyMessageBoxPopUp();
                            this.deleteFolderPopUp.init(SK.Text("MailScreen_Wish_To_Remove_Folder", "Do you wish to remove this folder?"), SK.Text("MailScreen_Remove_Mail_Folder", "Remove Mail Folder?"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PopUpOkClick));
                            this.deleteFolderPopUp.Show(InterfaceMgr.Instance.getGreyOutWindow());
                        }
                    }
                }
            }
        }

        private string generateAttachmentsString()
        {
            if (this.attachmentWindow == null)
            {
                return "";
            }
            List<MailLink> list = this.attachmentWindow.getLinks();
            if (list.Count == 0)
            {
                return "";
            }
            string str = "<!attch!>";
            foreach (MailLink link in list)
            {
                str = str + link.linkType.ToString() + "#";
                str = str + link.objectName + "#";
                str = str + link.objectID.ToString();
                str = str + "%";
            }
            return str;
        }

        public List<string> getBlockedList()
        {
            List<string> list = new List<string>();
            list.AddRange(this.blockedList);
            return list;
        }

        private MailFolderLine getFolderLine(int lineID)
        {
            switch (lineID)
            {
                case 0:
                    return this.mailList_folderLine01;

                case 1:
                    return this.mailList_folderLine02;

                case 2:
                    return this.mailList_folderLine03;

                case 3:
                    return this.mailList_folderLine04;

                case 4:
                    return this.mailList_folderLine05;

                case 5:
                    return this.mailList_folderLine06;

                case 6:
                    return this.mailList_folderLine07;

                case 7:
                    return this.mailList_folderLine08;

                case 8:
                    return this.mailList_folderLine09;

                case 9:
                    return this.mailList_folderLine10;

                case 10:
                    return this.mailList_folderLine11;

                case 11:
                    return this.mailList_folderLine12;

                case 12:
                    return this.mailList_folderLine13;

                case 13:
                    return this.mailList_folderLine14;

                case 14:
                    return this.mailList_folderLine15;

                case 15:
                    return this.mailList_folderLine16;

                case 0x10:
                    return this.mailList_folderLine17;

                case 0x11:
                    return this.mailList_folderLine18;

                case 0x12:
                    return this.mailList_folderLine19;

                case 0x13:
                    return this.mailList_folderLine20;

                case 20:
                    return this.mailList_folderLine21;

                case 0x15:
                    return this.mailList_folderLine22;

                case 0x16:
                    return this.mailList_folderLine23;

                case 0x17:
                    return this.mailList_folderLine24;

                case 0x18:
                    return this.mailList_folderLine25;

                case 0x19:
                    return this.mailList_folderLine26;

                case 0x1a:
                    return this.mailList_folderLine27;
            }
            return null;
        }

        private MailListLine getMailListLine(int lineID)
        {
            switch (lineID)
            {
                case 0:
                    return this.mailList_listLine01;

                case 1:
                    return this.mailList_listLine02;

                case 2:
                    return this.mailList_listLine03;

                case 3:
                    return this.mailList_listLine04;

                case 4:
                    return this.mailList_listLine05;

                case 5:
                    return this.mailList_listLine06;

                case 6:
                    return this.mailList_listLine07;

                case 7:
                    return this.mailList_listLine08;

                case 8:
                    return this.mailList_listLine09;

                case 9:
                    return this.mailList_listLine10;

                case 10:
                    return this.mailList_listLine11;

                case 11:
                    return this.mailList_listLine12;

                case 12:
                    return this.mailList_listLine13;

                case 13:
                    return this.mailList_listLine14;

                case 14:
                    return this.mailList_listLine15;

                case 15:
                    return this.mailList_listLine16;

                case 0x10:
                    return this.mailList_listLine17;

                case 0x11:
                    return this.mailList_listLine18;

                case 0x12:
                    return this.mailList_listLine19;

                case 0x13:
                    return this.mailList_listLine20;

                case 20:
                    return this.mailList_listLine21;

                case 0x15:
                    return this.mailList_listLine22;

                case 0x16:
                    return this.mailList_listLine23;

                case 0x17:
                    return this.mailList_listLine24;

                case 0x18:
                    return this.mailList_listLine25;

                case 0x19:
                    return this.mailList_listLine26;

                case 0x1a:
                    return this.mailList_listLine27;
            }
            return null;
        }

        private MailThreadLine getMailThreadLine(int lineID)
        {
            switch (lineID)
            {
                case 0:
                    return this.mailThread_listLine01;

                case 1:
                    return this.mailThread_listLine02;

                case 2:
                    return this.mailThread_listLine03;

                case 3:
                    return this.mailThread_listLine04;

                case 4:
                    return this.mailThread_listLine05;

                case 5:
                    return this.mailThread_listLine06;

                case 6:
                    return this.mailThread_listLine07;

                case 7:
                    return this.mailThread_listLine08;

                case 8:
                    return this.mailThread_listLine09;

                case 9:
                    return this.mailThread_listLine10;

                case 10:
                    return this.mailThread_listLine11;

                case 11:
                    return this.mailThread_listLine12;

                case 12:
                    return this.mailThread_listLine13;

                case 13:
                    return this.mailThread_listLine14;

                case 14:
                    return this.mailThread_listLine15;

                case 15:
                    return this.mailThread_listLine16;

                case 0x10:
                    return this.mailThread_listLine17;

                case 0x11:
                    return this.mailThread_listLine18;

                case 0x12:
                    return this.mailThread_listLine19;

                case 0x13:
                    return this.mailThread_listLine20;

                case 20:
                    return this.mailThread_listLine21;

                case 0x15:
                    return this.mailThread_listLine22;

                case 0x16:
                    return this.mailThread_listLine23;

                case 0x17:
                    return this.mailThread_listLine24;

                case 0x18:
                    return this.mailThread_listLine25;

                case 0x19:
                    return this.mailThread_listLine26;

                case 0x1a:
                    return this.mailThread_listLine27;
            }
            return null;
        }

        public void getMailUserSearchCallback(GetMailUserSearch_ReturnType returnData)
        {
            if (returnData.Success)
            {
                List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
                if (returnData.mailUsersSearch != null)
                {
                    foreach (string str in returnData.mailUsersSearch)
                    {
                        CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                            Text = str
                        };
                        items.Add(item);
                    }
                }
                this.newMail_iconFindList.populate(items);
                if (this.newMail_iconFindList.getSelectedItem() == null)
                {
                    this.newMail_iconFindAddButton.Enabled = false;
                    this.newMail_iconFindFavouritesButton.Enabled = false;
                }
                else
                {
                    this.newMail_iconFindAddButton.Enabled = true;
                    this.newMail_iconFindFavouritesButton.Enabled = true;
                }
            }
        }

        public void init(MailScreenManager parent)
        {
            this.m_parent = parent;
            base.clearControls();
            factionClose = fromFaction;
            fromFaction = false;
            this.mainBackgroundImage.Size = new Size(base.Width, base.Height - 40);
            this.mainBackgroundImage.Position = new Point(0, 40);
            base.addControl(this.mainBackgroundImage);
            this.mainBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            this.mainBodyArea.Position = new Point(0, 5);
            this.mainBodyArea.Size = new Size(0x3e0, 0x209);
            this.mainBackgroundImage.addControl(this.mainBodyArea);
            this.mainHeaderArea.Position = new Point(0, -40);
            this.mainHeaderArea.Size = new Size(0x3e0, 0x2d);
            this.mainBackgroundImage.addControl(this.mainHeaderArea);
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            this.mainHeaderArea.addControl(this.headerImage);
            this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            this.headerLabel.Text = SK.Text("MailScreen_Mail", "Mail");
            this.headerLabel.Color = ARGBColors.White;
            this.headerLabel.DropShadowColor = ARGBColors.Black;
            this.headerLabel.Position = new Point(9, 5);
            this.headerLabel.Size = new Size(700, 50);
            this.headerLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
            this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainHeaderArea.addControl(this.headerLabel);
            this.headerLabel2.Text = "";
            this.headerLabel2.Color = ARGBColors.White;
            this.headerLabel2.DropShadowColor = ARGBColors.Black;
            this.headerLabel2.Size = new Size(700, 0x18);
            if (Program.mySettings.LanguageIdent == "de")
            {
                this.headerLabel2.Position = new Point(280, 12);
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                this.headerLabel2.Position = new Point(230, 12);
            }
            else if (Program.mySettings.LanguageIdent == "es")
            {
                this.headerLabel2.Position = new Point(230, 12);
            }
            else if (Program.mySettings.LanguageIdent == "tr")
            {
                this.headerLabel2.Position = new Point(230, 12);
            }
            else if (Program.mySettings.LanguageIdent == "it")
            {
                this.headerLabel2.Position = new Point(330, 12);
                this.headerLabel2.Size = new Size(570, 0x18);
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                this.headerLabel2.Position = new Point(300, 12);
                this.headerLabel2.Size = new Size(600, 0x18);
            }
            else if (Program.mySettings.LanguageIdent == "pl")
            {
                this.headerLabel2.Position = new Point(280, 12);
                this.headerLabel2.Size = new Size(620, 0x18);
            }
            else
            {
                this.headerLabel2.Position = new Point(200, 12);
            }
            this.headerLabel2.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.headerLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainHeaderArea.addControl(this.headerLabel2);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x3b4, 4);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MailScreen_close");
            this.closeButton.CustomTooltipID = 0x1f6;
            this.mainHeaderArea.addControl(this.closeButton);
            this.dockButton.ImageNorm = (Image) GFXLibrary.mail2_detach_attach_window_normal;
            this.dockButton.ImageOver = (Image) GFXLibrary.mail2_detach_attach_window_over;
            this.dockButton.ImageClick = (Image) GFXLibrary.mail2_detach_attach_window_in;
            this.dockButton.Position = new Point(0x38c, 4);
            this.dockButton.CustomTooltipID = 500;
            this.dockButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.dockClick), "MailScreen_dock");
            this.mainHeaderArea.addControl(this.dockButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainHeaderArea, 0x1a, new Point(0x364, 3));
            this.mailListArea.Position = new Point(0, 0);
            this.mailListArea.Size = this.mainBodyArea.Size;
            this.mailListArea.Visible = false;
            this.mainBodyArea.addControl(this.mailListArea);
            this.mailList_folderArea.Position = new Point(15, 8);
            this.mailList_folderArea.Size = new Size(0x66, 0x1f8);
            this.mailListArea.addControl(this.mailList_folderArea);
            Size size = this.mailList_folderArea.Size;
            Point position = this.mailList_folderArea.Position;
            this.mailList_folderShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
            this.mailList_folderShadowTR.Position = new Point(position.X + size.Width, position.Y);
            this.mailListArea.addControl(this.mailList_folderShadowTR);
            this.mailList_folderShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
            this.mailList_folderShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
            this.mailListArea.addControl(this.mailList_folderShadowBR);
            this.mailList_folderShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
            this.mailList_folderShadowBL.Position = new Point(position.X, position.Y + size.Height);
            this.mailListArea.addControl(this.mailList_folderShadowBL);
            this.mailList_folderShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
            this.mailList_folderShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
            this.mailList_folderShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
            this.mailListArea.addControl(this.mailList_folderShadowR);
            this.mailList_folderShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
            this.mailList_folderShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
            this.mailList_folderShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
            this.mailListArea.addControl(this.mailList_folderShadowB);
            this.mailList_folderHeaderImage.Size = new Size(0x66, 0x12);
            this.mailList_folderHeaderImage.Position = new Point(0, 0);
            this.mailList_folderArea.addControl(this.mailList_folderHeaderImage);
            this.mailList_folderHeaderImage.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailList_folderHeaderLabel.Text = SK.Text("MailScreen_Folder", "Folder");
            this.mailList_folderHeaderLabel.Color = ARGBColors.Black;
            this.mailList_folderHeaderLabel.Position = new Point(0, 0);
            this.mailList_folderHeaderLabel.Size = new Size(this.mailList_folderHeaderImage.Width, this.mailList_folderHeaderImage.Height);
            this.mailList_folderHeaderLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.mailList_folderHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mailList_folderHeaderImage.addControl(this.mailList_folderHeaderLabel);
            for (int i = 0; i < 0x1b; i++)
            {
                MailFolderLine control = this.getFolderLine(i);
                control.Position = new Point(0, 0x11 + (i * 0x12));
                control.Size = new Size(this.mailList_folderArea.Size.Width, 0x12);
                control.Text.Text = "";
                control.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                control.Data = i;
                control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.folderLineClicked), "MailScreen_change_folder");
                this.mailList_folderArea.addControl(control);
                control.setup();
            }
            MailFolderLine line2 = this.getFolderLine(0);
            line2.Text.Text = SK.Text("MailScreen_Inbox", "Inbox");
            line2.Icon.Image = (Image) GFXLibrary.mail_folder_icon_open;
            this.mailList_listArea.Position = new Point(0x7f, 8);
            this.mailList_listArea.Size = new Size(0x26d, 0x1f8);
            this.mailListArea.addControl(this.mailList_listArea);
            size = this.mailList_listArea.Size;
            position = this.mailList_listArea.Position;
            size.Width += 0x10;
            this.mailList_listShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
            this.mailList_listShadowTR.Position = new Point(position.X + size.Width, position.Y);
            this.mailListArea.addControl(this.mailList_listShadowTR);
            this.mailList_listShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
            this.mailList_listShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
            this.mailListArea.addControl(this.mailList_listShadowBR);
            this.mailList_listShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
            this.mailList_listShadowBL.Position = new Point(position.X, position.Y + size.Height);
            this.mailListArea.addControl(this.mailList_listShadowBL);
            this.mailList_listShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
            this.mailList_listShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
            this.mailList_listShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
            this.mailListArea.addControl(this.mailList_listShadowR);
            this.mailList_listShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
            this.mailList_listShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
            this.mailList_listShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
            this.mailListArea.addControl(this.mailList_listShadowB);
            this.mailList_mainHeaderImage1.Size = new Size(0x16, 0x12);
            this.mailList_mainHeaderImage1.Position = new Point(0, 0);
            this.mailList_listArea.addControl(this.mailList_mainHeaderImage1);
            this.mailList_mainHeaderImage1.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailList_mainHeaderImage2.Size = new Size(0xf1, 0x12);
            this.mailList_mainHeaderImage2.Position = new Point(0x16, 0);
            this.mailList_listArea.addControl(this.mailList_mainHeaderImage2);
            this.mailList_mainHeaderImage2.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailList_mainHeaderLabel2.Text = SK.Text("MailScreen_Subject", "Subject");
            this.mailList_mainHeaderLabel2.Color = ARGBColors.Black;
            this.mailList_mainHeaderLabel2.Position = new Point(0x15, 0);
            this.mailList_mainHeaderLabel2.Size = new Size(this.mailList_mainHeaderImage2.Width - 0x15, this.mailList_mainHeaderImage2.Height);
            this.mailList_mainHeaderLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.mailList_mainHeaderLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailList_mainHeaderImage2.addControl(this.mailList_mainHeaderLabel2);
            this.mailList_mainHeaderImage3.Size = new Size(120, 0x12);
            this.mailList_mainHeaderImage3.Position = new Point(0x107, 0);
            this.mailList_listArea.addControl(this.mailList_mainHeaderImage3);
            this.mailList_mainHeaderImage3.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailList_mainHeaderLabel3.Text = SK.Text("MailScreen_Date", "Date");
            this.mailList_mainHeaderLabel3.Color = ARGBColors.Black;
            this.mailList_mainHeaderLabel3.Position = new Point(8, 0);
            this.mailList_mainHeaderLabel3.Size = new Size(this.mailList_mainHeaderImage3.Width - 8, this.mailList_mainHeaderImage3.Height);
            this.mailList_mainHeaderLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.mailList_mainHeaderLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailList_mainHeaderImage3.addControl(this.mailList_mainHeaderLabel3);
            this.mailList_mainHeaderImage4.Size = new Size(0xee, 0x12);
            this.mailList_mainHeaderImage4.Position = new Point(0x17f, 0);
            this.mailList_listArea.addControl(this.mailList_mainHeaderImage4);
            this.mailList_mainHeaderImage4.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailList_mainHeaderLabel4.Text = SK.Text("MailScreen_From_To", "From / To");
            this.mailList_mainHeaderLabel4.Color = ARGBColors.Black;
            this.mailList_mainHeaderLabel4.Position = new Point(8, 0);
            this.mailList_mainHeaderLabel4.Size = new Size(this.mailList_mainHeaderImage4.Width - 8, this.mailList_mainHeaderImage4.Height);
            this.mailList_mainHeaderLabel4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.mailList_mainHeaderLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailList_mainHeaderImage4.addControl(this.mailList_mainHeaderLabel4);
            for (int j = 0; j < 0x1b; j++)
            {
                MailListLine line3 = this.getMailListLine(j);
                line3.Position = new Point(0, 0x11 + (j * 0x12));
                line3.Size = new Size(0x26d, 0x12);
                line3.Subject.Text = "";
                line3.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                line3.Sender.Text = "";
                line3.Sender.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                line3.Date = DateTime.MinValue;
                line3.DateLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                line3.Data = j;
                line3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailLineClicked));
                this.mailList_listArea.addControl(line3);
                line3.setup();
            }
            this.lastMailLineClicked = -1;
            this.mailList_scrollBar.Position = new Point(0x26e, 0x11);
            this.mailList_scrollBar.Size = new Size(0x10, ((this.mailList_listArea.Size.Height - 0x11) - 0x11) - 1);
            this.mailList_listArea.addControl(this.mailList_scrollBar);
            this.mailList_scrollBar.Value = 0;
            this.mailList_scrollBar.Max = 0;
            this.mailList_scrollBar.NumVisibleLines = 0x1b;
            this.mailList_scrollBar.TabMinSize = 0x1a;
            this.mailList_scrollBar.OffsetTL = new Point(0, 0);
            this.mailList_scrollBar.OffsetBR = new Point(0, 0);
            this.mailList_scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
            this.mailList_scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.mailList_scrollBarValueMoved));
            this.mailList_scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.mailList_scrollBarMoved));
            this.mailList_upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
            this.mailList_upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
            this.mailList_upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
            this.mailList_upArrow.Position = new Point(this.mailList_scrollBar.Position.X, 0);
            this.mailList_upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ScrollUp), "MailScreen_scroll_up");
            this.mailList_listArea.addControl(this.mailList_upArrow);
            this.mailList_downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
            this.mailList_downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
            this.mailList_downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
            this.mailList_downArrow.Position = new Point(this.mailList_scrollBar.Position.X, this.mailList_scrollBar.Position.Y + this.mailList_scrollBar.Size.Height);
            this.mailList_downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ScrollDown), "MailScreen_scroll_down");
            this.mailList_listArea.addControl(this.mailList_downArrow);
            this.mailList_scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
            this.mailList_scrollTabLines.Position = new Point(this.mailList_scrollBar.TabPosition.X, ((this.mailList_scrollBar.TabSize - 8) / 2) + this.mailList_scrollBar.TabPosition.Y);
            this.mailList_scrollBar.addControl(this.mailList_scrollTabLines);
            this.mailList_mouseWheelArea.Position = new Point(0, 0);
            this.mailList_mouseWheelArea.Size = this.mailList_listArea.Size;
            this.mailList_mouseWheelArea.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mailList_MouseWheel));
            this.mailList_listArea.addControl(this.mailList_mouseWheelArea);
            this.mailList_iconArea.Position = new Point(0x308, 8);
            this.mailList_iconArea.Size = new Size(0xd1, 0x1f8);
            this.mailListArea.addControl(this.mailList_iconArea);
            this.mailList_iconNewMail.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
            this.mailList_iconNewMail.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
            this.mailList_iconNewMail.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
            this.mailList_iconNewMail.Position = new Point(6, 0x13);
            this.mailList_iconNewMail.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailList_iconNewMail.Text.Position = new Point(0x3f, 0);
            this.mailList_iconNewMail.Text.Size = new Size(0x6b, this.mailList_iconNewMail.Text.Size.Height);
            this.mailList_iconNewMail.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.mailList_iconNewMail.TextYOffset = -6;
            this.mailList_iconNewMail.Text.Text = SK.Text("MailScreen_New_Mail", "New Mail");
            this.mailList_iconNewMail.Text.Color = ARGBColors.Black;
            this.mailList_iconNewMail.ImageIcon = (Image) GFXLibrary.mail2_folder_icon_64_open;
            this.mailList_iconNewMail.ImageIconPosition = new Point(5, -24);
            this.mailList_iconNewMail.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_NewMail), "MailScreen_new_mail");
            this.mailList_iconArea.addControl(this.mailList_iconNewMail);
            this.mailList_manageBlocked.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_manageBlocked.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_manageBlocked.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_manageBlocked.Position = new Point(0x16, 460);
            this.mailList_manageBlocked.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_manageBlocked.TextYOffset = -2;
            this.mailList_manageBlocked.Text.Text = SK.Text("MailBlock_manage", "Manage Blocked Users");
            this.mailList_manageBlocked.Text.Color = ARGBColors.Black;
            this.mailList_manageBlocked.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_BlockUser2), "MailScreen_block");
            this.mailList_iconArea.addControl(this.mailList_manageBlocked);
            this.mailList_userFilterLabel.Text = SK.Text("MailBlock_username_filter", "Filter By Username");
            this.mailList_userFilterLabel.Position = new Point(0, 0x197);
            this.mailList_userFilterLabel.Size = new Size(0xc4, 20);
            this.mailList_userFilterLabel.Color = ARGBColors.Black;
            this.mailList_userFilterLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_userFilterLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mailList_iconArea.addControl(this.mailList_userFilterLabel);
            int y = 160;
            this.mailList_iconSelectedBack.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
            this.mailList_iconSelectedBack.Position = new Point(6, 0x77 - y);
            this.mailList_iconSelectedBack.ClipRect = new Rectangle(0, y, this.mailList_iconSelectedBack.Image.Width, 0x16e - y);
            this.mailList_iconSelectedBack.Visible = false;
            this.mailList_iconArea.addControl(this.mailList_iconSelectedBack);
            this.mailList_iconSelected.Image = (Image) GFXLibrary.mail2_large_button_normal;
            this.mailList_iconSelected.Position = new Point(6, 0x5e);
            this.mailList_iconSelected.Visible = false;
            this.mailList_iconArea.addControl(this.mailList_iconSelected);
            this.mailList_iconSelectedIcon.Image = (Image) GFXLibrary.mail2_mail_icon;
            this.mailList_iconSelectedIcon.Position = new Point(6, -24);
            this.mailList_iconSelected.addControl(this.mailList_iconSelectedIcon);
            this.mailList_iconSelectedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailList_iconSelectedLabel.Position = new Point(0x39, -6);
            this.mailList_iconSelectedLabel.Size = new Size(this.mailList_iconSelected.Size.Width - 0x3f, this.mailList_iconSelected.Size.Height);
            this.mailList_iconSelectedLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.mailList_iconSelectedLabel.Text = SK.Text("MailScreen_Selected_Mail", "Selected Mail");
            this.mailList_iconSelectedLabel.Color = ARGBColors.Black;
            this.mailList_iconSelected.addControl(this.mailList_iconSelectedLabel);
            this.mailList_iconSelectedOpen.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_iconSelectedOpen.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_iconSelectedOpen.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_iconSelectedOpen.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 120);
            this.mailList_iconSelectedOpen.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_iconSelectedOpen.TextYOffset = -2;
            this.mailList_iconSelectedOpen.Text.Text = SK.Text("MailScreen_Open", "Open");
            this.mailList_iconSelectedOpen.Text.Color = ARGBColors.Black;
            this.mailList_iconSelectedOpen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_OpenMail), "MailScreen_open_mail");
            this.mailList_iconSelectedBack.addControl(this.mailList_iconSelectedOpen);
            this.mailList_iconSelectedUnread.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_iconSelectedUnread.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_iconSelectedUnread.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_iconSelectedUnread.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 90);
            if ((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "tr"))
            {
                this.mailList_iconSelectedUnread.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            }
            else
            {
                this.mailList_iconSelectedUnread.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            }
            this.mailList_iconSelectedUnread.TextYOffset = -2;
            this.mailList_iconSelectedUnread.Text.Text = SK.Text("MailScreen_Mark_As_Unread", "Mark as Unread");
            this.mailList_iconSelectedUnread.Text.Color = ARGBColors.Black;
            this.mailList_iconSelectedUnread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_MarkAsUnRead), "MailScreen_mark_as_unread");
            this.mailList_iconSelectedBack.addControl(this.mailList_iconSelectedUnread);
            this.mailList_iconSelectedRead.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_iconSelectedRead.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_iconSelectedRead.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_iconSelectedRead.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 60);
            this.mailList_iconSelectedRead.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_iconSelectedRead.TextYOffset = -2;
            this.mailList_iconSelectedRead.Text.Text = SK.Text("MailScreen_Mark_As_Read", "Mark as Read");
            this.mailList_iconSelectedRead.Text.Color = ARGBColors.Black;
            this.mailList_iconSelectedRead.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_MarkAsRead), "MailScreen_mark_as_read");
            this.mailList_iconSelectedBack.addControl(this.mailList_iconSelectedRead);
            this.mailList_iconSelectedMoveThread.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_iconSelectedMoveThread.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_iconSelectedMoveThread.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_iconSelectedMoveThread.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 30);
            this.mailList_iconSelectedMoveThread.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_iconSelectedMoveThread.TextYOffset = -2;
            this.mailList_iconSelectedMoveThread.Text.Text = SK.Text("MailScreen_Move_This_Thread", "Move This Thread");
            this.mailList_iconSelectedMoveThread.Text.Color = ARGBColors.Black;
            this.mailList_iconSelectedMoveThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_MoveThread), "MailScreen_move_thread");
            this.mailList_iconSelectedBack.addControl(this.mailList_iconSelectedMoveThread);
            this.mailList_iconSelectedDelete.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_iconSelectedDelete.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_iconSelectedDelete.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_iconSelectedDelete.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50);
            this.mailList_iconSelectedDelete.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_iconSelectedDelete.TextYOffset = -2;
            this.mailList_iconSelectedDelete.Text.Text = SK.Text("MailScreen_Delete_Thread", "Delete Thread");
            this.mailList_iconSelectedDelete.Text.Color = ARGBColors.Black;
            this.mailList_iconSelectedDelete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_DeleteThread), "MailScreen_delete_thread");
            this.mailList_iconSelectedBack.addControl(this.mailList_iconSelectedDelete);
            this.mailList_MoveFolderLabel.Text = "<- " + SK.Text("MailScreen_Select_Target_Folder", "Select Target Folder for the Selected Mail.");
            this.mailList_MoveFolderLabel.Color = ARGBColors.White;
            this.mailList_MoveFolderLabel.DropShadowColor = ARGBColors.Black;
            this.mailList_MoveFolderLabel.Position = new Point(140, 30);
            this.mailList_MoveFolderLabel.Size = new Size(500, 100);
            this.mailList_MoveFolderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.mailList_MoveFolderLabel.Visible = false;
            this.mailListArea.addControl(this.mailList_MoveFolderLabel);
            this.mailList_MoveFolderCancel.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
            this.mailList_MoveFolderCancel.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
            this.mailList_MoveFolderCancel.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
            this.mailList_MoveFolderCancel.Position = new Point(0x30e, 0x1b);
            this.mailList_MoveFolderCancel.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mailList_MoveFolderCancel.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.mailList_MoveFolderCancel.TextYOffset = -6;
            this.mailList_MoveFolderCancel.Text.Text = SK.Text("MailScreen_Cancel_Move", "Cancel Move");
            this.mailList_MoveFolderCancel.Text.Color = ARGBColors.Black;
            this.mailList_MoveFolderCancel.Visible = false;
            this.mailList_MoveFolderCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_CancelMove), "MailScreen_cancel_move");
            this.mailListArea.addControl(this.mailList_MoveFolderCancel);
            this.mailThreadArea.Position = new Point(0, 0);
            this.mailThreadArea.Size = this.mainBodyArea.Size;
            this.mailThreadArea.Visible = false;
            this.mainBodyArea.addControl(this.mailThreadArea);
            this.newMailArea.Position = new Point(0, 0);
            this.newMailArea.Size = this.mainBodyArea.Size;
            this.newMailArea.Visible = false;
            this.tbMain.Visible = this.newMailArea.Visible;
            this.tbUserFilter.Visible = this.mailListArea.Visible;
            this.tbSubject.Visible = this.newMailArea.Visible;
            this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
            this.tbSubject.MaxLength = 100;
            this.mainBodyArea.addControl(this.newMailArea);
            this.mailCreateFolderArea.Position = new Point(0, 0);
            this.mailCreateFolderArea.Size = this.mainBodyArea.Size;
            this.mailCreateFolderArea.Visible = false;
            this.mainBodyArea.addControl(this.mailCreateFolderArea);
            y = 0xcc;
            this.mailList_createFolderBack.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
            int x = (this.mailCreateFolderArea.Size.Width - this.mailList_iconSelectedBack.Image.Width) / 2;
            int num5 = 50;
            this.mailList_createFolderBack.Position = new Point(x, (0x77 - y) + num5);
            this.mailList_createFolderBack.ClipRect = new Rectangle(0, y, this.mailList_iconSelectedBack.Image.Width, 0x16e - y);
            this.mailCreateFolderArea.addControl(this.mailList_createFolderBack);
            this.mailList_createFolderHeader.Image = (Image) GFXLibrary.mail2_large_button_normal;
            this.mailList_createFolderHeader.Position = new Point(x, 0x5e + num5);
            this.mailCreateFolderArea.addControl(this.mailList_createFolderHeader);
            this.mailList_createFolderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mailList_createFolderLabel.Position = new Point(0, 0);
            this.mailList_createFolderLabel.Size = new Size(this.mailList_createFolderHeader.Size.Width, this.mailList_createFolderHeader.Size.Height - 8);
            this.mailList_createFolderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mailList_createFolderLabel.Text = SK.Text("MailScreen_Create_New_Folder", "Create New Folder");
            this.mailList_createFolderLabel.Color = ARGBColors.Black;
            this.mailList_createFolderHeader.addControl(this.mailList_createFolderLabel);
            this.mailList_createFolderOK.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_createFolderOK.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_createFolderOK.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_createFolderOK.Position = new Point(14, (this.mailList_createFolderBack.Height - 50) - 30);
            this.mailList_createFolderOK.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_createFolderOK.TextYOffset = -2;
            this.mailList_createFolderOK.Text.Text = SK.Text("MailScreen_Create_Folder", "Create Folder");
            this.mailList_createFolderOK.Text.Color = ARGBColors.Black;
            this.mailList_createFolderOK.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_CreateFolder), "MailScreen_create_folder");
            this.mailList_createFolderBack.addControl(this.mailList_createFolderOK);
            this.mailList_createFolderCancel.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailList_createFolderCancel.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailList_createFolderCancel.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailList_createFolderCancel.Position = new Point(14, this.mailList_createFolderBack.Height - 50);
            this.mailList_createFolderCancel.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailList_createFolderCancel.TextYOffset = -2;
            this.mailList_createFolderCancel.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.mailList_createFolderCancel.Text.Color = ARGBColors.Black;
            this.mailList_createFolderCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_CancelCreateFolder), "MailScreen_cancel_create_folder");
            this.mailList_createFolderBack.addControl(this.mailList_createFolderCancel);
            this.mailThread_listArea.Position = new Point(15, 8);
            this.mailThread_listArea.Size = new Size(0x2ec, 0x1f8);
            this.mailThreadArea.addControl(this.mailThread_listArea);
            size = this.mailThread_listArea.Size;
            position = this.mailThread_listArea.Position;
            this.mailThread_listShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
            this.mailThread_listShadowTR.Position = new Point(position.X + size.Width, position.Y);
            this.mailThreadArea.addControl(this.mailThread_listShadowTR);
            this.mailThread_listShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
            this.mailThread_listShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
            this.mailThreadArea.addControl(this.mailThread_listShadowBR);
            this.mailThread_listShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
            this.mailThread_listShadowBL.Position = new Point(position.X, position.Y + size.Height);
            this.mailThreadArea.addControl(this.mailThread_listShadowBL);
            this.mailThread_listShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
            this.mailThread_listShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
            this.mailThread_listShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
            this.mailThreadArea.addControl(this.mailThread_listShadowR);
            this.mailThread_listShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
            this.mailThread_listShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
            this.mailThread_listShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
            this.mailThreadArea.addControl(this.mailThread_listShadowB);
            this.mailThread_mainHeaderImage1.Size = new Size(250, 0x12);
            this.mailThread_mainHeaderImage1.Position = new Point(0, 0);
            this.mailThread_listArea.addControl(this.mailThread_mainHeaderImage1);
            this.mailThread_mainHeaderImage1.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailThread_mainHeaderLabel1.Text = SK.Text("MailScreen_Subject", "Subject");
            this.mailThread_mainHeaderLabel1.Color = ARGBColors.Black;
            this.mailThread_mainHeaderLabel1.Position = new Point(4, 0);
            this.mailThread_mainHeaderLabel1.Size = new Size(this.mailThread_mainHeaderImage1.Width - 0x15, this.mailThread_mainHeaderImage1.Height);
            this.mailThread_mainHeaderLabel1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.mailThread_mainHeaderLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailThread_mainHeaderImage1.addControl(this.mailThread_mainHeaderLabel1);
            this.mailThread_mainHeaderImage2.Size = new Size(0x5e, 0x12);
            this.mailThread_mainHeaderImage2.Position = new Point(250, 0);
            this.mailThread_listArea.addControl(this.mailThread_mainHeaderImage2);
            this.mailThread_mainHeaderImage2.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailThread_mainHeaderLabel2.Text = SK.Text("MailScreen_Date", "Date");
            this.mailThread_mainHeaderLabel2.Color = ARGBColors.Black;
            this.mailThread_mainHeaderLabel2.Position = new Point(4, 0);
            this.mailThread_mainHeaderLabel2.Size = new Size(this.mailThread_mainHeaderImage2.Width - 8, this.mailThread_mainHeaderImage2.Height);
            this.mailThread_mainHeaderLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.mailThread_mainHeaderLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailThread_mainHeaderImage2.addControl(this.mailThread_mainHeaderLabel2);
            this.mailThread_mainHeaderImage3.Size = new Size(0x70, 0x12);
            this.mailThread_mainHeaderImage3.Position = new Point(0x158, 0);
            this.mailThread_listArea.addControl(this.mailThread_mainHeaderImage3);
            this.mailThread_mainHeaderImage3.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
            this.mailThread_mainHeaderLabel3.Text = SK.Text("MailScreen_From", "From");
            this.mailThread_mainHeaderLabel3.Color = ARGBColors.Black;
            this.mailThread_mainHeaderLabel3.Position = new Point(4, 0);
            this.mailThread_mainHeaderLabel3.Size = new Size(this.mailThread_mainHeaderImage3.Width - 8, this.mailThread_mainHeaderImage3.Height);
            this.mailThread_mainHeaderLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.mailThread_mainHeaderLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.mailThread_mainHeaderImage3.addControl(this.mailThread_mainHeaderLabel3);
            for (int k = 0; k < 0x1b; k++)
            {
                MailThreadLine line4 = this.getMailThreadLine(k);
                line4.Position = new Point(0, 0x11 + (k * 0x12));
                line4.Size = new Size(0x1c8, 0x12);
                line4.BodyText.Text = "";
                line4.BodyText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                line4.BodyText.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
                line4.Sender.Text = "";
                line4.Sender.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                line4.Sender.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
                line4.Date = DateTime.MinValue;
                line4.DateLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
                line4.Data = k;
                line4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailItemClicked));
                this.mailThread_listArea.addControl(line4);
                line4.setup();
            }
            this.lastMailItemClicked = -1;
            this.mailThread_scrollBar.Position = new Point(0x1c8, 0x11);
            this.mailThread_scrollBar.Size = new Size(0x10, ((this.mailThread_listArea.Size.Height - 0x11) - 0x11) - 1);
            this.mailThread_listArea.addControl(this.mailThread_scrollBar);
            this.mailThread_scrollBar.Value = 0;
            this.mailThread_scrollBar.Max = 0;
            this.mailThread_scrollBar.NumVisibleLines = 0x1b;
            this.mailThread_scrollBar.TabMinSize = 0x1a;
            this.mailThread_scrollBar.OffsetTL = new Point(0, 0);
            this.mailThread_scrollBar.OffsetBR = new Point(0, 0);
            this.mailThread_scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
            this.mailThread_scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.mailThread_scrollBarValueMoved));
            this.mailThread_scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.mailThread_scrollBarMoved));
            this.mailThread_upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
            this.mailThread_upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
            this.mailThread_upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
            this.mailThread_upArrow.Position = new Point(this.mailThread_scrollBar.Position.X, 0);
            this.mailThread_upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThread_ScrollUp), "MailScreen_scroll_up");
            this.mailThread_listArea.addControl(this.mailThread_upArrow);
            this.mailThread_downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
            this.mailThread_downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
            this.mailThread_downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
            this.mailThread_downArrow.Position = new Point(this.mailThread_scrollBar.Position.X, this.mailThread_scrollBar.Position.Y + this.mailThread_scrollBar.Size.Height);
            this.mailThread_downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThread_ScrollDown), "MailScreen_scroll_down");
            this.mailThread_listArea.addControl(this.mailThread_downArrow);
            this.mailThread_scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
            this.mailThread_scrollTabLines.Position = new Point(this.mailThread_scrollBar.TabPosition.X, ((this.mailThread_scrollBar.TabSize - 8) / 2) + this.mailThread_scrollBar.TabPosition.Y);
            this.mailThread_scrollBar.addControl(this.mailThread_scrollTabLines);
            this.mailThread_mailHeaderBack.Position = new Point(0x1d7, 0);
            this.mailThread_mailHeaderBack.Size = new Size(0x115, 0x25);
            this.mailThread_mailHeaderBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
            this.mailThread_listArea.addControl(this.mailThread_mailHeaderBack);
            this.mailThread_mailBodyBack.Position = new Point(0x1d7, 0x26);
            this.mailThread_mailBodyBack.Size = new Size(0x115, 0x1d2);
            this.mailThread_mailBodyBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
            this.mailThread_listArea.addControl(this.mailThread_mailBodyBack);
            this.mailThread_mailHeaderFromLabel.Text = SK.Text("MailScreen_From", "From") + " :";
            this.mailThread_mailHeaderFromLabel.Color = ARGBColors.Black;
            this.mailThread_mailHeaderFromLabel.Position = new Point(6, 3);
            this.mailThread_mailHeaderFromLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 10, 20);
            this.mailThread_mailHeaderFromLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.mailThread_mailHeaderFromLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mailThread_mailHeaderBack.addControl(this.mailThread_mailHeaderFromLabel);
            this.mailThread_mailHeaderFromNameLabel.Text = "";
            this.mailThread_mailHeaderFromNameLabel.Color = ARGBColors.Black;
            this.mailThread_mailHeaderFromNameLabel.Position = new Point(0x38, 3);
            this.mailThread_mailHeaderFromNameLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 60, 20);
            this.mailThread_mailHeaderFromNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.mailThread_mailHeaderFromNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mailThread_mailHeaderFromNameLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
            this.mailThread_mailHeaderBack.addControl(this.mailThread_mailHeaderFromNameLabel);
            this.mailThread_mailHeaderDateLabel.Text = SK.Text("MailScreen_Date", "Date") + " :";
            this.mailThread_mailHeaderDateLabel.Color = ARGBColors.Black;
            this.mailThread_mailHeaderDateLabel.Position = new Point(6, 20);
            this.mailThread_mailHeaderDateLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 10, 20);
            this.mailThread_mailHeaderDateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.mailThread_mailHeaderDateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mailThread_mailHeaderBack.addControl(this.mailThread_mailHeaderDateLabel);
            this.mailThread_mailHeaderDateValueLabel.Text = "";
            this.mailThread_mailHeaderDateValueLabel.Color = ARGBColors.Black;
            this.mailThread_mailHeaderDateValueLabel.Position = new Point(0x38, 20);
            this.mailThread_mailHeaderDateValueLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 60, 20);
            this.mailThread_mailHeaderDateValueLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.mailThread_mailHeaderDateValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mailThread_mailHeaderBack.addControl(this.mailThread_mailHeaderDateValueLabel);
            this.mailThread_fromShield.Image = null;
            this.mailThread_fromShield.Position = new Point(0xf2, 3);
            this.mailThread_fromShield.Visible = false;
            this.mailThread_mailHeaderBack.addControl(this.mailThread_fromShield);
            this.mailThread_mailBodyText.Text = "";
            this.mailThread_mailBodyText.Color = ARGBColors.Black;
            this.mailThread_mailBodyText.Position = new Point(4, 4);
            this.mailThread_mailBodyText.Size = new Size((this.mailThread_mailBodyBack.Width - 8) - 0x10, this.mailThread_mailBodyBack.Height - 8);
            this.mailThread_mailBodyText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailThread_mailBodyText.setTextHeightChangedCallback(new CustomSelfDrawPanel.CSDScrollLabel.CSD_TextHeightChanged(this.bodyTextHeightChanged));
            this.mailThread_mailBodyText.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
            this.mailThread_mailBodyText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailTextClicked));
            this.mailThread_mailBodyBack.addControl(this.mailThread_mailBodyText);
            this.mailThreadBody_scrollBar.Position = new Point(this.mailThread_mailBodyBack.Width - 0x10, 0x11);
            this.mailThreadBody_scrollBar.Size = new Size(0x10, ((this.mailThread_mailBodyBack.Size.Height - 0x11) - 0x11) - 1);
            this.mailThread_mailBodyBack.addControl(this.mailThreadBody_scrollBar);
            this.mailThreadBody_scrollBar.Value = 0;
            this.mailThreadBody_scrollBar.Max = 0;
            this.mailThreadBody_scrollBar.NumVisibleLines = this.mailThread_mailBodyText.Height;
            this.mailThreadBody_scrollBar.TabMinSize = 0x1a;
            this.mailThreadBody_scrollBar.OffsetTL = new Point(0, 0);
            this.mailThreadBody_scrollBar.OffsetBR = new Point(0, 0);
            this.mailThreadBody_scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
            this.mailThreadBody_scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.mailThreadBody_scrollBarValueMoved));
            this.mailThreadBody_scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.mailThreadBody_scrollBarMoved));
            this.mailThreadBody_upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
            this.mailThreadBody_upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
            this.mailThreadBody_upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
            this.mailThreadBody_upArrow.Position = new Point(this.mailThreadBody_scrollBar.Position.X, 0);
            this.mailThreadBody_upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThreadBody_ScrollUp), "MailScreen_scroll_up");
            this.mailThread_mailBodyBack.addControl(this.mailThreadBody_upArrow);
            this.mailThreadBody_downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
            this.mailThreadBody_downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
            this.mailThreadBody_downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
            this.mailThreadBody_downArrow.Position = new Point(this.mailThreadBody_scrollBar.Position.X, this.mailThreadBody_scrollBar.Position.Y + this.mailThreadBody_scrollBar.Size.Height);
            this.mailThreadBody_downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThreadBody_ScrollDown), "MailScreen_scroll_down");
            this.mailThread_mailBodyBack.addControl(this.mailThreadBody_downArrow);
            this.mailThreadBody_scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
            this.mailThreadBody_scrollTabLines.Position = new Point(this.mailThreadBody_scrollBar.TabPosition.X, ((this.mailThreadBody_scrollBar.TabSize - 8) / 2) + this.mailThreadBody_scrollBar.TabPosition.Y);
            this.mailThreadBody_scrollBar.addControl(this.mailThreadBody_scrollTabLines);
            this.mailThread_mouseWheelArea.Position = new Point(0, 0);
            this.mailThread_mouseWheelArea.Size = new Size(0x1d7, this.mailList_listArea.Size.Height);
            this.mailThread_mouseWheelArea.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mailThread_MouseWheel));
            this.mailThread_listArea.addControl(this.mailThread_mouseWheelArea);
            this.mailThreadBody_mouseWheelArea.Position = new Point(0x1d7, 0);
            this.mailThreadBody_mouseWheelArea.Size = new Size(0x11c, this.mailList_listArea.Size.Height);
            this.mailThreadBody_mouseWheelArea.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mailThreadBody_MouseWheel));
            this.mailThread_listArea.addControl(this.mailThreadBody_mouseWheelArea);
            this.mailThread_iconArea.Position = new Point(0x308, 8);
            this.mailThread_iconArea.Size = new Size(0xd1, 0x1f8);
            this.mailThreadArea.addControl(this.mailThread_iconArea);
            this.mailThread_iconBack.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
            this.mailThread_iconBack.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
            this.mailThread_iconBack.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
            this.mailThread_iconBack.Position = new Point(6, 0x13);
            this.mailThread_iconBack.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            if (((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "tr")) || ((Program.mySettings.LanguageIdent == "it") || (Program.mySettings.LanguageIdent == "pt")))
            {
                this.mailThread_iconBack.Text.Position = new Point(0x37, 0);
                this.mailThread_iconBack.Text.Size = new Size(this.mailThread_iconBack.Size.Width - 0x37, this.mailThread_iconBack.Size.Height);
            }
            else if (Program.mySettings.LanguageIdent == "de")
            {
                this.mailThread_iconBack.Text.Position = new Point(0x37, 0);
            }
            else
            {
                this.mailThread_iconBack.Text.Position = new Point(0x3f, 0);
            }
            this.mailThread_iconBack.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mailThread_iconBack.TextYOffset = -6;
            this.mailThread_iconBack.Text.Text = SK.Text("MailScreen_Back_To_Mail_List", "Back To Mail List");
            this.mailThread_iconBack.Text.Color = ARGBColors.Black;
            this.mailThread_iconBack.ImageIcon = (Image) GFXLibrary.mail2_mail_icon;
            this.mailThread_iconBack.ImageIconPosition = new Point(5, -24);
            this.mailThread_iconBack.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnToMailList), "MailScreen_back_to_mail_list");
            this.mailThread_iconArea.addControl(this.mailThread_iconBack);
            y = 100;
            this.mailThread_iconSelectedBack.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
            this.mailThread_iconSelectedBack.Position = new Point(6, 0x77 - y);
            this.mailThread_iconSelectedBack.ClipRect = new Rectangle(0, y, this.mailList_iconSelectedBack.Image.Width, 0x16e - y);
            this.mailThread_iconSelectedBack.Visible = false;
            this.mailThread_iconArea.addControl(this.mailThread_iconSelectedBack);
            this.mailThread_iconSelected.Image = (Image) GFXLibrary.mail2_large_button_normal;
            this.mailThread_iconSelected.Position = new Point(6, 0x5e);
            this.mailThread_iconSelected.Visible = false;
            this.mailThread_iconArea.addControl(this.mailThread_iconSelected);
            this.mailThread_iconSelectedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mailThread_iconSelectedLabel.Position = new Point(0, 0);
            this.mailThread_iconSelectedLabel.Size = new Size(this.mailList_iconSelected.Size.Width, this.mailList_iconSelected.Size.Height - 6);
            this.mailThread_iconSelectedLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.mailThread_iconSelectedLabel.Text = SK.Text("MailScreen_Selected_Mail", "Selected Mail");
            this.mailThread_iconSelectedLabel.Color = ARGBColors.Black;
            this.mailThread_iconSelected.addControl(this.mailThread_iconSelectedLabel);
            this.mailThread_iconSelectedReply.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailThread_iconSelectedReply.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailThread_iconSelectedReply.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailThread_iconSelectedReply.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 150);
            this.mailThread_iconSelectedReply.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailThread_iconSelectedReply.TextYOffset = -2;
            this.mailThread_iconSelectedReply.Text.Text = SK.Text("MailScreen_Reply_To_Thread", "Reply To Thread");
            this.mailThread_iconSelectedReply.Text.Color = ARGBColors.Black;
            this.mailThread_iconSelectedReply.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThread_reply), "MailScreen_reply");
            this.mailThread_iconSelectedBack.addControl(this.mailThread_iconSelectedReply);
            this.mailThread_iconSelectedView.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailThread_iconSelectedView.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailThread_iconSelectedView.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailThread_iconSelectedView.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 180);
            this.mailThread_iconSelectedView.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailThread_iconSelectedView.TextYOffset = -2;
            this.mailThread_iconSelectedView.Text.Text = SK.Text("MailScreen_View_Mail_Post", "View");
            this.mailThread_iconSelectedView.Text.Color = ARGBColors.Black;
            this.mailThread_iconSelectedView.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailTextClicked), "MailScreen_reply");
            this.mailThread_iconSelectedBack.addControl(this.mailThread_iconSelectedView);
            this.mailThread_iconSelectedForward.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailThread_iconSelectedForward.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailThread_iconSelectedForward.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailThread_iconSelectedForward.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 120);
            this.mailThread_iconSelectedForward.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailThread_iconSelectedForward.TextYOffset = -2;
            this.mailThread_iconSelectedForward.Text.Text = SK.Text("MailScreen_Forward_Thread", "Forward Thread");
            this.mailThread_iconSelectedForward.Text.Color = ARGBColors.Black;
            this.mailThread_iconSelectedForward.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ForwardMail), "MailScreen_forward");
            this.mailThread_iconSelectedBack.addControl(this.mailThread_iconSelectedForward);
            this.mailThread_iconSelectedBlockPoster.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailThread_iconSelectedBlockPoster.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailThread_iconSelectedBlockPoster.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailThread_iconSelectedBlockPoster.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 90);
            this.mailThread_iconSelectedBlockPoster.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailThread_iconSelectedBlockPoster.TextYOffset = -2;
            this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
            this.mailThread_iconSelectedBlockPoster.Text.Color = ARGBColors.Black;
            this.mailThread_iconSelectedBlockPoster.Enabled = false;
            this.mailThread_iconSelectedBlockPoster.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_BlockUser), "MailScreen_block");
            this.mailThread_iconSelectedBack.addControl(this.mailThread_iconSelectedBlockPoster);
            this.mailThread_iconSelectedReportMail.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailThread_iconSelectedReportMail.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailThread_iconSelectedReportMail.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailThread_iconSelectedReportMail.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 60);
            this.mailThread_iconSelectedReportMail.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailThread_iconSelectedReportMail.TextYOffset = -2;
            this.mailThread_iconSelectedReportMail.Text.Text = SK.Text("MailScreen_Report_This_Mail", "Report This Mail");
            this.mailThread_iconSelectedReportMail.Text.Color = ARGBColors.Black;
            this.mailThread_iconSelectedReportMail.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ReportMail), "MailScreen_report");
            this.mailThread_iconSelectedReportMail.CustomTooltipID = 0x1f7;
            this.mailThread_iconSelectedBack.addControl(this.mailThread_iconSelectedReportMail);
            this.mailThread_openAttachments.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.mailThread_openAttachments.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.mailThread_openAttachments.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.mailThread_openAttachments.Position = new Point(14, (this.mailList_iconSelectedBack.Height - 50) - 30);
            this.mailThread_openAttachments.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.mailThread_openAttachments.TextYOffset = -2;
            this.mailThread_openAttachments.Text.Text = SK.Text("MailScreen_Open_Attachments", "Open Targets");
            this.mailThread_openAttachments.Text.Color = ARGBColors.Black;
            this.mailThread_openAttachments.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_OpenAttachmentWindow), "MailScreen_attachments");
            this.mailThread_openAttachments.CustomTooltipID = 0x202;
            this.mailThread_iconSelectedBack.addControl(this.mailThread_openAttachments);
            this.newMail_newMailArea.Position = new Point(0x10, 6);
            this.newMail_newMailArea.Size = new Size(0x2ec, 0x1f8);
            this.newMailArea.addControl(this.newMail_newMailArea);
            this.newMail_mailHeaderBack.Position = new Point(0, 0);
            this.newMail_mailHeaderBack.Size = new Size(0x2ec, 0x21);
            this.newMail_mailHeaderBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
            this.newMail_newMailArea.addControl(this.newMail_mailHeaderBack);
            this.newMail_mailBodyBack.Position = new Point(0, 0x22);
            this.newMail_mailBodyBack.Size = new Size(0x2ec, 470);
            this.newMail_mailBodyBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
            this.newMail_newMailArea.addControl(this.newMail_mailBodyBack);
            this.newMail_breakbar.Image = (Image) GFXLibrary.mail_horizontal_bar;
            this.newMail_breakbar.Position = new Point(0, 0x1a);
            this.newMail_newMailArea.addControl(this.newMail_breakbar);
            size = this.newMail_newMailArea.Size;
            position = this.newMail_newMailArea.Position;
            this.newMail_bodyShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
            this.newMail_bodyShadowTR.Position = new Point(position.X + size.Width, position.Y);
            this.newMailArea.addControl(this.newMail_bodyShadowTR);
            this.newMail_bodyShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
            this.newMail_bodyShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
            this.newMailArea.addControl(this.newMail_bodyShadowBR);
            this.newMail_bodyShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
            this.newMail_bodyShadowBL.Position = new Point(position.X, position.Y + size.Height);
            this.newMailArea.addControl(this.newMail_bodyShadowBL);
            this.newMail_bodyShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
            this.newMail_bodyShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
            this.newMail_bodyShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
            this.newMailArea.addControl(this.newMail_bodyShadowR);
            this.newMail_bodyShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
            this.newMail_bodyShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
            this.newMail_bodyShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
            this.newMailArea.addControl(this.newMail_bodyShadowB);
            this.newMail_SubjectBorder.Size = new Size(0x297, 0x11);
            this.newMail_SubjectBorder.Position = new Point(0x4e, 5);
            this.newMail_mailHeaderBack.addControl(this.newMail_SubjectBorder);
            this.newMail_SubjectBorder.Create((Image) GFXLibrary.mail_inset_white_left, (Image) GFXLibrary.mail_inset_white_middle, (Image) GFXLibrary.mail_inset_white_right);
            this.newMail_ToLabel.Text = SK.Text("MailScreen_To", "To") + " :";
            this.newMail_ToLabel.Color = ARGBColors.Black;
            this.newMail_ToLabel.Position = new Point(4, 7);
            this.newMail_ToLabel.Size = new Size(0x4b, 20);
            this.newMail_ToLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.newMail_ToLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.newMail_mailBodyBack.addControl(this.newMail_ToLabel);
            this.newMail_SubjectLabel.Text = SK.Text("MailScreen_Subject", "Subject") + " :";
            this.newMail_SubjectLabel.Color = ARGBColors.Black;
            this.newMail_SubjectLabel.Position = new Point(6, 5);
            this.newMail_SubjectLabel.Size = new Size(0x4b, 20);
            this.newMail_SubjectLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.newMail_SubjectLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.newMail_mailHeaderBack.addControl(this.newMail_SubjectLabel);
            this.newMail_separater.Position = new Point(170, 0);
            this.newMail_separater.Size = new Size(0, this.newMail_mailBodyBack.Size.Height);
            this.newMail_separater.LineColor = Color.FromArgb(0xb9, 0x9b, 0x7f);
            this.newMail_mailBodyBack.addControl(this.newMail_separater);
            this.newMail_separater2.Position = new Point(0, 0x1a0);
            this.newMail_separater2.Size = new Size(170, 0);
            this.newMail_separater2.LineColor = Color.FromArgb(0xb9, 0x9b, 0x7f);
            this.newMail_mailBodyBack.addControl(this.newMail_separater2);
            this.newMail_ToList.Position = new Point(1, 30);
            this.newMail_ToList.Size = new Size(0xab, 0x156);
            this.newMail_mailBodyBack.addControl(this.newMail_ToList);
            this.newMail_ToList.Create(0x13, 0x12);
            this.newMail_ToList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_ToLineClicked));
            this.newMail_iconArea.Position = new Point(0x308, 8);
            this.newMail_iconArea.Size = new Size(0xd1, 0x1f8);
            this.newMailArea.addControl(this.newMail_iconArea);
            this.newMail_iconBackButton.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
            this.newMail_iconBackButton.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
            this.newMail_iconBackButton.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
            this.newMail_iconBackButton.Position = new Point(6, 0x13);
            this.newMail_iconBackButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            if (((Program.mySettings.LanguageIdent == "pl") || (Program.mySettings.LanguageIdent == "pt")) || ((Program.mySettings.LanguageIdent == "tr") || (Program.mySettings.LanguageIdent == "it")))
            {
                this.newMail_iconBackButton.Text.Position = new Point(0x37, 0);
                this.newMail_iconBackButton.Text.Size = new Size(this.newMail_iconBackButton.Size.Width - 0x37, this.newMail_iconBackButton.Size.Height);
            }
            else if (Program.mySettings.LanguageIdent == "de")
            {
                this.newMail_iconBackButton.Text.Position = new Point(0x37, 0);
            }
            else
            {
                this.newMail_iconBackButton.Text.Position = new Point(0x3f, 0);
            }
            this.newMail_iconBackButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.newMail_iconBackButton.TextYOffset = -6;
            this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail_List", "Back To Mail List");
            this.newMail_iconBackButton.Text.Color = ARGBColors.Black;
            this.newMail_iconBackButton.ImageIcon = (Image) GFXLibrary.mail2_mail_icon;
            this.newMail_iconBackButton.ImageIconPosition = new Point(5, -24);
            this.newMail_iconBackButton.ClickArea = new Rectangle(0, 0, this.newMail_iconBackButton.Size.Width, this.newMail_iconBackButton.Size.Height - 11);
            this.newMail_iconBackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnFromNewMail), "MailScreen_back_to_mail_list");
            this.newMail_iconArea.addControl(this.newMail_iconBackButton);
            y = 3;
            this.newMail_iconBackground.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
            this.newMail_iconBackground.Position = new Point(6, 0x5f - y);
            this.newMail_iconBackground.ClipRect = new Rectangle(0, y, this.newMail_iconBackground.Image.Width, 0x16e - y);
            this.newMail_iconBackground.Visible = true;
            this.newMail_iconArea.addControl(this.newMail_iconBackground);
            this.newMail_iconTab1Area.Position = new Point(0, y + 0x22);
            this.newMail_iconTab1Area.Size = new Size(this.newMail_iconBackground.Size.Width, (0x1a6 - y) - 0x22);
            this.newMail_iconBackground.addControl(this.newMail_iconTab1Area);
            this.newMail_iconTab2Area.Position = new Point(0, y + 0x22);
            this.newMail_iconTab2Area.Size = new Size(this.newMail_iconBackground.Size.Width, (0x1a6 - y) - 0x22);
            this.newMail_iconBackground.addControl(this.newMail_iconTab2Area);
            this.newMail_iconTab3Area.Position = new Point(0, y + 0x22);
            this.newMail_iconTab3Area.Size = new Size(this.newMail_iconBackground.Size.Width, (0x1a6 - y) - 0x22);
            this.newMail_iconBackground.addControl(this.newMail_iconTab3Area);
            this.newMail_iconTab4Area.Position = new Point(0, y + 0x22);
            this.newMail_iconTab4Area.Size = new Size(this.newMail_iconBackground.Size.Width, (0x1a6 - y) - 0x22);
            this.newMail_iconBackground.addControl(this.newMail_iconTab4Area);
            this.newMail_iconTab1Button.Position = new Point(6, 70);
            this.newMail_iconTab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab1Clicked), "MailScreen_tab_1");
            this.newMail_iconTab1Button.CustomTooltipID = 0x1f9;
            this.newMail_iconArea.addControl(this.newMail_iconTab1Button);
            this.newMail_iconTab2Button.Position = new Point(0x39, 70);
            this.newMail_iconTab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab2Clicked), "MailScreen_tab_2");
            this.newMail_iconTab2Button.CustomTooltipID = 0x1fa;
            this.newMail_iconArea.addControl(this.newMail_iconTab2Button);
            this.newMail_iconTab3Button.Position = new Point(0x68, 70);
            this.newMail_iconTab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab3Clicked), "MailScreen_tab_3");
            this.newMail_iconTab3Button.CustomTooltipID = 0x1fb;
            this.newMail_iconArea.addControl(this.newMail_iconTab3Button);
            this.newMail_iconTab4Button.Position = new Point(0x97, 70);
            this.newMail_iconTab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab4Clicked), "MailScreen_tab_4");
            this.newMail_iconTab4Button.CustomTooltipID = 0x1fc;
            this.newMail_iconArea.addControl(this.newMail_iconTab4Button);
            this.newMail_iconFindList.Position = new Point(0x11, 0x1f);
            this.newMail_iconFindList.Size = new Size(160, 0xd8);
            this.newMail_iconTab1Area.addControl(this.newMail_iconFindList);
            this.newMail_iconFindList.Create(12, 0x12);
            this.newMail_iconFindList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FindLineClicked));
            this.newMail_iconFindList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FindLineDoubleClicked));
            this.newMail_iconFindAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconFindAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconFindAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconFindAddButton.Position = new Point(14, 290);
            this.newMail_iconFindAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconFindAddButton.TextYOffset = -2;
            this.newMail_iconFindAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
            this.newMail_iconFindAddButton.Text.Color = ARGBColors.Black;
            this.newMail_iconFindAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addFindNameToRecipients), "MailScreen_add");
            this.newMail_iconTab1Area.addControl(this.newMail_iconFindAddButton);
            this.newMail_iconFindFavouritesButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconFindFavouritesButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconFindFavouritesButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconFindFavouritesButton.Position = new Point(14, 260);
            this.newMail_iconFindFavouritesButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconFindFavouritesButton.TextYOffset = -2;
            this.newMail_iconFindFavouritesButton.Text.Text = SK.Text("MailScreen_Add_To_Favourites", "Add To Favourites");
            this.newMail_iconFindFavouritesButton.Text.Color = ARGBColors.Black;
            this.newMail_iconFindFavouritesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addFindNameToFavourites), "MailScreen_add_to_favourites");
            this.newMail_iconTab1Area.addControl(this.newMail_iconFindFavouritesButton);
            this.changeSearchTab(0, false);
            this.newMail_iconRecentList.Position = new Point(0x11, 13);
            this.newMail_iconRecentList.Size = new Size(160, 0xea);
            this.newMail_iconTab2Area.addControl(this.newMail_iconRecentList);
            this.newMail_iconRecentList.Create(13, 0x12);
            this.newMail_iconRecentList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_RecentLineClicked));
            this.newMail_iconRecentList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_RecentLineDoubleClicked));
            this.newMail_iconRecentAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconRecentAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconRecentAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconRecentAddButton.Position = new Point(14, 290);
            this.newMail_iconRecentAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconRecentAddButton.TextYOffset = -2;
            this.newMail_iconRecentAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
            this.newMail_iconRecentAddButton.Text.Color = ARGBColors.Black;
            this.newMail_iconRecentAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addRecentNameToRecipients), "MailScreen_add");
            this.newMail_iconTab2Area.addControl(this.newMail_iconRecentAddButton);
            this.newMail_iconRecentFavouritesButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconRecentFavouritesButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconRecentFavouritesButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconRecentFavouritesButton.Position = new Point(14, 260);
            this.newMail_iconRecentFavouritesButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconRecentFavouritesButton.TextYOffset = -2;
            this.newMail_iconRecentFavouritesButton.Text.Text = SK.Text("MailScreen_Add_To_Favourites", "Add To Favourites");
            this.newMail_iconRecentFavouritesButton.Text.Color = ARGBColors.Black;
            this.newMail_iconRecentFavouritesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addRecentNameToFavourites), "MailScreen_add_to_favourites");
            this.newMail_iconTab2Area.addControl(this.newMail_iconRecentFavouritesButton);
            this.newMail_iconFavouritesList.Position = new Point(0x11, 13);
            this.newMail_iconFavouritesList.Size = new Size(160, 0xea);
            this.newMail_iconTab3Area.addControl(this.newMail_iconFavouritesList);
            this.newMail_iconFavouritesList.Create(13, 0x12);
            this.newMail_iconFavouritesList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FavouritesLineClicked));
            this.newMail_iconFavouritesList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FavouritesLineDoubleClicked));
            this.newMail_iconFavouritesRemoveButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconFavouritesRemoveButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconFavouritesRemoveButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconFavouritesRemoveButton.Position = new Point(14, 260);
            this.newMail_iconFavouritesRemoveButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconFavouritesRemoveButton.TextYOffset = -2;
            this.newMail_iconFavouritesRemoveButton.Text.Text = SK.Text("MailScreen_Removes_From_Favourites", "Remove From Favourites");
            this.newMail_iconFavouritesRemoveButton.Text.Color = ARGBColors.Black;
            this.newMail_iconFavouritesRemoveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeNameFromFavourites), "MailScreen_remove_from_favourites");
            this.newMail_iconTab3Area.addControl(this.newMail_iconFavouritesRemoveButton);
            this.newMail_iconFavouritesAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconFavouritesAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconFavouritesAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconFavouritesAddButton.Position = new Point(14, 290);
            this.newMail_iconFavouritesAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconFavouritesAddButton.TextYOffset = -2;
            this.newMail_iconFavouritesAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
            this.newMail_iconFavouritesAddButton.Text.Color = ARGBColors.Black;
            this.newMail_iconFavouritesAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addFavouritesNameToRecipients), "MailScreen_add");
            this.newMail_iconTab3Area.addControl(this.newMail_iconFavouritesAddButton);
            this.newMail_iconKnownList.Position = new Point(0x11, 13);
            this.newMail_iconKnownList.Size = new Size(160, 0xea);
            this.newMail_iconTab4Area.addControl(this.newMail_iconKnownList);
            this.newMail_iconKnownList.Create(13, 0x12);
            this.newMail_iconKnownList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_KnownLineClicked));
            this.newMail_iconKnownList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_KnownLineDoubleClicked));
            this.newMail_iconKnownAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconKnownAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconKnownAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconKnownAddButton.Position = new Point(14, 290);
            this.newMail_iconKnownAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconKnownAddButton.TextYOffset = -2;
            this.newMail_iconKnownAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
            this.newMail_iconKnownAddButton.Text.Color = ARGBColors.Black;
            this.newMail_iconKnownAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addKnownNameToRecipients), "MailScreen_add");
            this.newMail_iconTab4Area.addControl(this.newMail_iconKnownAddButton);
            this.newMail_iconKnownFavouritesButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
            this.newMail_iconKnownFavouritesButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
            this.newMail_iconKnownFavouritesButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
            this.newMail_iconKnownFavouritesButton.Position = new Point(14, 260);
            this.newMail_iconKnownFavouritesButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_iconKnownFavouritesButton.TextYOffset = -2;
            this.newMail_iconKnownFavouritesButton.Text.Text = SK.Text("MailScreen_Add_To_Favourites", "Add To Favourites");
            this.newMail_iconKnownFavouritesButton.Text.Color = ARGBColors.Black;
            this.newMail_iconKnownFavouritesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addKnownNameToFavourites), "MailScreen_add_to_favourites");
            this.newMail_iconTab4Area.addControl(this.newMail_iconKnownFavouritesButton);
            this.newMail_iconSendMail.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
            this.newMail_iconSendMail.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
            this.newMail_iconSendMail.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
            this.newMail_iconSendMail.Position = new Point(6, 0x1c8);
            this.newMail_iconSendMail.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.newMail_iconSendMail.Text.Position = new Point(0x3f, 0);
            this.newMail_iconSendMail.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.newMail_iconSendMail.TextYOffset = -6;
            this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Send_Mail", "Send Mail");
            this.newMail_iconSendMail.Text.Color = ARGBColors.Black;
            this.newMail_iconSendMail.ImageIcon = (Image) GFXLibrary.mail_folder_icon_64_open;
            this.newMail_iconSendMail.ImageIconPosition = new Point(5, -8);
            this.newMail_iconSendMail.Enabled = false;
            this.newMail_iconSendMail.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendMail), "MailScreen_send_mail");
            this.newMail_iconArea.addControl(this.newMail_iconSendMail);
            this.newMail_removeRecipient.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.newMail_removeRecipient.ImageOver = (Image) GFXLibrary.button_132_over;
            this.newMail_removeRecipient.ImageClick = (Image) GFXLibrary.button_132_in;
            this.newMail_removeRecipient.Position = new Point(0x13, 0x179);
            this.newMail_removeRecipient.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_removeRecipient.TextYOffset = -2;
            this.newMail_removeRecipient.Text.Text = SK.Text("MailScreen_Remove", "Remove");
            this.newMail_removeRecipient.Text.Color = ARGBColors.Black;
            this.newMail_removeRecipient.Enabled = false;
            this.newMail_removeRecipient.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeNameFromRecipients), "MailScreen_remove");
            this.newMail_mailBodyBack.addControl(this.newMail_removeRecipient);
            this.newMail_addAttachments.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.newMail_addAttachments.ImageOver = (Image) GFXLibrary.button_132_over;
            this.newMail_addAttachments.ImageClick = (Image) GFXLibrary.button_132_in;
            this.newMail_addAttachments.Position = new Point(0x13, this.newMail_separater2.Y + 8);
            this.newMail_addAttachments.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.newMail_addAttachments.TextYOffset = -2;
            this.newMail_addAttachments.Text.Text = SK.Text("MailScreen_Attachments", "Targets");
            this.newMail_addAttachments.Text.Color = ARGBColors.Black;
            this.newMail_addAttachments.Enabled = true;
            this.newMail_addAttachments.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openNewAttachmentsPopup), "MailScreen_openAddAttachments");
            this.newMail_mailBodyBack.addControl(this.newMail_addAttachments);
        }

        private void InitializeComponent()
        {
            this.tbMain = new TextBox();
            this.tbSubject = new TextBox();
            this.tbFindInput = new TextBox();
            this.tbNewFolder = new TextBox();
            this.tbUserFilter = new TextBox();
            base.SuspendLayout();
            this.tbMain.BackColor = Color.FromArgb(0xeb, 0xf5, 0xfd);
            this.tbMain.BorderStyle = BorderStyle.None;
            this.tbMain.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbMain.ForeColor = ARGBColors.Black;
            this.tbMain.Location = new Point(0xbf, 0x56);
            this.tbMain.MaxLength = 0x1770;
            this.tbMain.Multiline = true;
            this.tbMain.Name = "tbMain";
            this.tbMain.ScrollBars = ScrollBars.Vertical;
            this.tbMain.Size = new Size(0x23d, 0x1d4);
            this.tbMain.TabIndex = 1;
            this.tbMain.TextChanged += new EventHandler(this.tbMain_TextChanged);
            this.tbSubject.BackColor = Color.FromArgb(0xf7, 0xfc, 0xfe);
            this.tbSubject.BorderStyle = BorderStyle.None;
            this.tbSubject.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tbSubject.ForeColor = ARGBColors.Black;
            this.tbSubject.Location = new Point(0x62, 0x3a);
            this.tbSubject.MaxLength = 150;
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new Size(0x291, 13);
            this.tbSubject.TabIndex = 0;
            this.tbSubject.TextChanged += new EventHandler(this.tbSubject_TextChanged);
            this.tbFindInput.BackColor = Color.FromArgb(0xf7, 0xfc, 0xfe);
            this.tbFindInput.ForeColor = Color.FromArgb(0, 0, 0);
            this.tbFindInput.Location = new Point(0x31f, 0xbb);
            this.tbFindInput.MaxLength = 50;
            this.tbFindInput.Name = "tbFindInput";
            this.tbFindInput.Size = new Size(160, 20);
            this.tbFindInput.TabIndex = 11;
            this.tbFindInput.TextChanged += new EventHandler(this.tbFindInput_TextChanged);
            this.tbFindInput.KeyUp += new KeyEventHandler(this.tbFindInput_KeyUp);
            this.tbFindInput.KeyPress += new KeyPressEventHandler(this.tbFindInput_KeyPress);
            this.tbNewFolder.BackColor = Color.FromArgb(0xf7, 0xfc, 0xfe);
            this.tbNewFolder.ForeColor = Color.FromArgb(0, 0, 0);
            this.tbNewFolder.Location = new Point(0x1ab, 0xf9);
            this.tbNewFolder.MaxLength = 0x13;
            this.tbNewFolder.Name = "tbNewFolder";
            this.tbNewFolder.Size = new Size(0x89, 20);
            this.tbNewFolder.TabIndex = 12;
            this.tbNewFolder.Visible = false;
            this.tbNewFolder.TextChanged += new EventHandler(this.tbNewFolder_TextChanged);
            this.tbNewFolder.KeyPress += new KeyPressEventHandler(this.tbNewFolder_KeyPress);
            this.tbUserFilter.BackColor = Color.FromArgb(0xf7, 0xfc, 0xfe);
            this.tbUserFilter.ForeColor = Color.FromArgb(0, 0, 0);
            this.tbUserFilter.Location = new Point(0x31f, 480);
            this.tbUserFilter.MaxLength = 50;
            this.tbUserFilter.Name = "tbUserFilter";
            this.tbUserFilter.Size = new Size(160, 20);
            this.tbUserFilter.TabIndex = 13;
            this.tbUserFilter.TextChanged += new EventHandler(this.tbUserFilter_TextChanged);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.tbUserFilter);
            base.Controls.Add(this.tbNewFolder);
            base.Controls.Add(this.tbFindInput);
            base.Controls.Add(this.tbSubject);
            base.Controls.Add(this.tbMain);
            this.MaximumSize = new Size(0x3e0, 0x236);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "MailScreen";
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

        private string languageSplitString(string str)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            string[] strArray = str.Split(new char[] { '\x00a7' });
            Thread.CurrentThread.CurrentCulture = CultureInfo.InstalledUICulture;
            if (strArray.Length <= 0)
            {
                return str;
            }
            string str2 = "";
            switch (Program.mySettings.LanguageIdent.ToLower())
            {
                case "en":
                    if (strArray.Length >= 1)
                    {
                        str2 = strArray[0];
                    }
                    break;

                case "de":
                    if (strArray.Length >= 2)
                    {
                        str2 = strArray[1];
                    }
                    break;

                case "fr":
                    if (strArray.Length >= 3)
                    {
                        str2 = strArray[2];
                    }
                    break;

                case "ru":
                    if (strArray.Length >= 4)
                    {
                        str2 = strArray[3];
                    }
                    break;

                case "es":
                    if (strArray.Length >= 5)
                    {
                        str2 = strArray[4];
                    }
                    break;

                case "pl":
                    if (strArray.Length >= 6)
                    {
                        str2 = strArray[5];
                    }
                    break;

                case "pt":
                    if (strArray.Length >= 7)
                    {
                        str2 = strArray[6];
                    }
                    break;

                case "it":
                    if (strArray.Length >= 8)
                    {
                        str2 = strArray[7];
                    }
                    break;

                case "tr":
                    if (strArray.Length >= 9)
                    {
                        str2 = strArray[8];
                    }
                    break;
            }
            if (((str2.Length == 4) && (strArray.Length > 1)) && ((str2[0] == '<') && (str2[3] == '>')))
            {
                char ch = str2[1];
                char ch2 = str2[2];
                switch (ch)
                {
                    case 'A':
                        switch (ch2)
                        {
                            case 'A':
                                return "Welcome to Stronghold Kingdoms!";

                            case 'B':
                                return "Willkommen bei Stronghold Kingdoms!";

                            case 'C':
                                return "Bienvenue dans Stronghold Kingdoms !";

                            case 'D':
                                return "Добро пожаловать в Stronghold Kingdoms!";

                            case 'E':
                                return "\x00a1Bienvenido a Stronghold Kingdoms!";

                            case 'F':
                                return "Witaj w grze Twierdza: Kr\x00f3lestwa!";

                            case 'G':
                                return "Boas-vindas ao Stronghold Kingdoms!";

                            case 'H':
                                return "Ti do il benvenuto a Stronghold Kingdoms!";

                            case 'I':
                                return "Stronghold Kingdoms’a hoş geldiniz!";
                        }
                        return str2;

                    case 'B':
                        switch (ch2)
                        {
                            case 'A':
                                return "Congratulations! You have completed the Stronghold Kingdoms Tutorial.";

                            case 'B':
                                return "Herzlichen Gl\x00fcckwunsch! Sie haben das Stronghold Kingdoms Tutorial abgeschlossen.";

                            case 'C':
                                return "F\x00e9licitations ! Vous venez de compl\x00e9ter le tutoriel de Stronghold Kingdoms.";

                            case 'D':
                                return "Поздравляем! Вы прошли обучение Stronghold Kingdoms. ";

                            case 'E':
                                return "\x00a1Felicidades! Has completado el Tutorial de Stronghold Kingdoms";

                            case 'F':
                                return "Gratulacje! Udało Ci się ukończyć samouczek Twierdza: Kr\x00f3lestwa.";

                            case 'G':
                                return "Parab\x00e9ns! Voc\x00ea completou o tutorial do Stronghold Kingdoms.";

                            case 'H':
                                return "Congratulazioni! Hai completato il tutorial di Stronghold Kingdoms.";

                            case 'I':
                                return "Tebrikler! Stronghold Kingdoms Eğitim B\x00f6l\x00fcm\x00fc’n\x00fc tamamladınız.";
                        }
                        return str2;

                    case 'C':
                        switch (ch2)
                        {
                            case 'A':
                                return "Welcome to the Second Age";

                            case 'B':
                                return "Willkommen in der zweiten Epoche!";

                            case 'C':
                                return "Bienvenue dans la Deuxi\x00e8me \x00c8re";

                            case 'D':
                                return "Добро пожаловать во Вторую Эпоху!";

                            case 'E':
                                return "Bienvenido a la Segunda Edad";

                            case 'F':
                                return "Witaj w Drugiej Epoce";

                            case 'G':
                                return "Boas-vindas \x00e0 Segunda Era";

                            case 'H':
                                return "Un caloroso benvenuto alla Seconda Epoca";

                            case 'I':
                                return "İkinci \x00c7ağ’a hoş geldiniz!";
                        }
                        return str2;

                    case 'D':
                        switch (ch2)
                        {
                            case 'A':
                                return "Welcome to the Third Age";

                            case 'B':
                                return "Willkommen in der dritten Epoche!";

                            case 'C':
                                return "Bienvenue dans la Troisi\x00e8me \x00c8re";

                            case 'D':
                                return "Добро пожаловать в Третью Эпоху";

                            case 'E':
                                return "Tercera Edad Correo In-Game:";

                            case 'F':
                                return "Witaj w Trzeciej Epoce";

                            case 'G':
                                return "Boas-vindas \x00e0 Terceira Era";

                            case 'H':
                                return "Un caloroso benvenuto alla Terza Epoca";

                            case 'I':
                                return "\x00dc\x00e7\x00fcnc\x00fc \x00c7ağ’a Hoşgeldiniz!";
                        }
                        return str2;

                    case 'E':
                        switch (ch2)
                        {
                            case 'A':
                                return "Welcome to Domination World";

                            case 'B':
                                return "Willkommen auf der Domination Welt";

                            case 'C':
                                return "Bienvenue sur le Monde de Domination !";

                            case 'D':
                                return "Добро пожаловать в Мир Domination!";

                            case 'E':
                                return "Bienvenido al Mundo Domination.";

                            case 'F':
                                return "Witaj w Swiecie Domination!";

                            case 'G':
                                return "Boas vindas ao Mundo de Domination";

                            case 'H':
                                return "Benvenuto nel Mondo Domination";

                            case 'I':
                                return "Domination D\x00fcnyasina Hosgeldiniz";
                        }
                        return str2;

                    case 'F':
                        return SK.Text("WorldSelect_4thAge_Header", "Welcome to the Fourth Age!");

                    case 'G':
                        return (SK.Text("FourthAge_Mail_01", "My Liege! How glad I am to see you in one piece. The brutal Third Age brought with it higher caps for villages, increased Honour production and more, however my spies tell me the Fourth Age is not quite what we were expecting… Although there are similarities, such as an increased village cap, there is also much to learn. Your forces move faster than ever before, Houses and Factions have adopted a new structure to encourage combat and the cost of Interdiction has been raised. I’ve also heard word of a new Military School and Bombard, both of which could help us crush our enemies and take the crown.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_02", "Core to the Fourth Age is the new rule set, which is now in effect:") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_03", "1. Crown Princes may now own up to 40 villages.") + Environment.NewLine + SK.Text("FourthAge_Mail_04", "2. Army and Scout movement speeds are three times faster than in the First Age.") + Environment.NewLine + SK.Text("FourthAge_Mail_05", "3. Weapons can no longer be sold or purchased at Markets.") + Environment.NewLine + SK.Text("FourthAge_Mail_06", "4. Goods have been cleared from all Markets, with prices reset to their starting level.") + Environment.NewLine + SK.Text("FourthAge_Mail_07", "5. The Faith Point cost for Interdiction has been increased.") + Environment.NewLine + SK.Text("FourthAge_Mail_08", "6. A Military School can be built in a parish, which gives access to Bombards.") + Environment.NewLine + SK.Text("FourthAge_Mail_09", "7. All in-game Factions and Houses have been disbanded.") + Environment.NewLine + SK.Text("FourthAge_Mail_10", "8. All capital forums and walls will be cleared.") + Environment.NewLine + SK.Text("FourthAge_Mail_11", "9. Houses are limited to 3 Factions, with the first Faction to apply accepted automatically and all other factions voted in.") + Environment.NewLine + SK.Text("FourthAge_Mail_12", "10. Certain upgradable Parish buildings can now gain 5 additional levels.") + Environment.NewLine + SK.Text("FourthAge_Mail_13", "11. Players who own more than 10 villages will no longer have to pay the extra honour cost to regain a village, if one of their villages is captured.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_14", "As you can see my Lord, the Fourth Age may prove challenging. However I believe there is much to gain, whether we fight or trade our way to victory.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_15", "Good luck Sire"));

                    case 'H':
                        return str2;

                    case 'I':
                        return SK.Text("WorldSelect_5thAge_Header", "Welcome to the Fifth Age!");

                    case 'J':
                        return (SK.Text("FifthAge_Mail_01", "Greetings Sire!  It is good to see you have made it through the trials of the Fourth Age and ready to face the challenges that the Fifth Age brings!  Hopefully you have brought many loyal friends and staunch allies along with you in to this new era as well, for you will need them if you are to succeed!") + Environment.NewLine + Environment.NewLine + SK.Text("FifthAge_Mail_02", "Core to the Fifth Age is the new rule set, which is now in effect.") + Environment.NewLine + Environment.NewLine + SK.Text("FifthAge_Mail_03", "1. Military Schools can be upgraded to level 5.") + Environment.NewLine + SK.Text("FifthAge_Mail_04", "2. Treasure Castles are twice as likely to appear.") + Environment.NewLine + SK.Text("FifthAge_Mail_05", "3. All factions and houses have been disbanded.") + Environment.NewLine + SK.Text("FifthAge_Mail_06", "4. All capital forums and walls have been cleared.") + Environment.NewLine + SK.Text("FifthAge_Mail_07", "5. Only members of a House can be candidates for County elections.") + Environment.NewLine + SK.Text("FifthAge_Mail_08", "6. To be eligible for the office of sheriff, a candidate must belong to a House which controls at least 30% of the parishes in the county.") + Environment.NewLine + SK.Text("FifthAge_Mail_09", "7. Glory gained overall is increased by one-third.") + Environment.NewLine + SK.Text("FifthAge_Mail_10", "8. Large Houses gain Glory more slowly than small Houses.  A House with 60 members will gain one-third the amount of Glory that would be normally gained.") + Environment.NewLine + SK.Text("FifthAge_Mail_11", "As you can see Sire, you will need to maneuver skillfully in this new political arena and spread your villages and influence all over the realm in order to assure victory for you and your chosen comrades in arms.") + Environment.NewLine + Environment.NewLine + SK.Text("FourthAge_Mail_15", "Good luck Sire"));
                }
            }
            return str2;
        }

        public void loadBlockedList()
        {
            int userID = RemoteServices.Instance.UserID;
            string str = GameEngine.getSettingsPath(false);
            FileStream input = null;
            BinaryReader reader = null;
            this.blockedList.Clear();
            this.aggressiveBlocking = false;
            try
            {
                input = new FileStream(str + @"\MailBlockList-" + userID.ToString() + ".dat", FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(input);
                new SparseArray();
                new SparseArray();
                int num2 = reader.ReadInt32();
                for (int i = 0; i < num2; i++)
                {
                    string item = reader.ReadString();
                    this.blockedList.Add(item);
                }
                try
                {
                    this.aggressiveBlocking = reader.ReadBoolean();
                }
                catch (Exception)
                {
                }
                reader.Close();
                input.Close();
            }
            catch (Exception)
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (input != null)
                    {
                        input.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void loadMail()
        {
            int userID = RemoteServices.Instance.UserID;
            string str = GameEngine.getSettingsPath(false);
            FileStream input = null;
            BinaryReader reader = null;
            try
            {
                input = new FileStream(str + @"\MailData-" + userID.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat", FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(input);
                SparseArray array = new SparseArray();
                SparseArray array2 = new SparseArray();
                long ticks = reader.ReadInt64();
                int num3 = reader.ReadInt32();
                for (int i = 0; i < num3; i++)
                {
                    long num5 = reader.ReadInt64();
                    if (num5 >= 0L)
                    {
                        MailThreadListItem item = new MailThreadListItem {
                            mailThreadID = num5,
                            folderID = reader.ReadInt64()
                        };
                        long num6 = reader.ReadInt64();
                        item.mailTime = new DateTime(num6);
                        item.mailTimeAsDouble = reader.ReadDouble();
                        int num7 = reader.ReadInt32();
                        if (num7 > 0)
                        {
                            List<string> list = new List<string>();
                            for (int k = 0; k < num7; k++)
                            {
                                string str2 = reader.ReadString();
                                list.Add(str2);
                            }
                            item.otherUser = list.ToArray();
                        }
                        else
                        {
                            item.otherUser = new string[0];
                        }
                        item.readStatus = reader.ReadBoolean();
                        item.subject = reader.ReadString();
                        int num9 = reader.ReadInt32();
                        if (num9 == -5)
                        {
                            item.readOnly = reader.ReadBoolean();
                            item.specialType = reader.ReadInt32();
                            item.specialArea = reader.ReadInt32();
                            num9 = reader.ReadInt32();
                        }
                        List<MailThreadItem> list2 = new List<MailThreadItem>();
                        bool flag = false;
                        DateTime maxValue = DateTime.MaxValue;
                        for (int j = 0; j < num9; j++)
                        {
                            MailThreadItem item2 = new MailThreadItem {
                                body = reader.ReadString(),
                                mailID = reader.ReadInt64()
                            };
                            long num11 = reader.ReadInt64();
                            item2.mailTime = new DateTime(num11);
                            item2.mailTimeAsDouble = reader.ReadDouble();
                            item2.otherUser = reader.ReadString();
                            item2.otherUserID = reader.ReadInt32();
                            item2.readStatus = reader.ReadBoolean();
                            list2.Add(item2);
                            if (item2.mailTime > maxValue)
                            {
                                flag = true;
                            }
                            else
                            {
                                maxValue = item2.mailTime;
                            }
                        }
                        if (num9 > 0)
                        {
                            if (flag)
                            {
                                list2.Sort(this.mailThreadComparer);
                            }
                            array[item.mailThreadID] = list2.ToArray();
                        }
                        array2[item.mailThreadID] = item;
                    }
                }
                reader.Close();
                input.Close();
                this.m_storedThreadHeaders = array2;
                this.m_storedThreads = array;
                this.lastTimeThreadsReceived = new DateTime(ticks);
            }
            catch (Exception)
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (input != null)
                    {
                        input.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void logout()
        {
            this.gotFolders = false;
            this.initialRequest = true;
        }

        public void mailFoldersCallback(GetMailFolders_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.m_mailFolders.Clear();
                this.m_mailFolders.AddRange(returnData.folders);
                this.updateFolderList();
            }
        }

        private void mailItemClicked()
        {
            if (base.ClickedControl != null)
            {
                int data = base.ClickedControl.Data;
                if (data >= 0)
                {
                    this.mailItemClicked(data);
                }
            }
        }

        private void mailItemClicked(int lineClicked)
        {
            try
            {
                this.lastMailItemClicked = lineClicked;
                DateTime now = DateTime.Now;
                MailThreadItem[] itemArray = (MailThreadItem[]) this.m_storedThreads[this.selectedMailThreadID];
                this.selectedMailItemID = itemArray[lineClicked].mailID;
                GameEngine.Instance.playInterfaceSound("MailScreen_mail_post_clicked");
                this.showMailItem(lineClicked);
            }
            catch (Exception)
            {
            }
        }

        private void mailLineClicked()
        {
            if (base.ClickedControl != null)
            {
                int data = base.ClickedControl.Data;
                if (data >= 0)
                {
                    this.mailLineClicked(data, true);
                }
            }
        }

        private void mailLineClicked(int lineClicked, bool closeSections)
        {
            if (lineClicked >= 0)
            {
                try
                {
                    this.lastMailLineClicked = lineClicked;
                    bool shiftPressed = GameEngine.shiftPressed;
                    bool keyControlled = GameEngine.Instance.GFX.keyControlled;
                    if (shiftPressed)
                    {
                        keyControlled = false;
                    }
                    DateTime now = DateTime.Now;
                    long mailThreadID = this.m_preSortedHeaders[lineClicked].mailThreadID;
                    if (((mailThreadID == this.selectedMailThreadID) && !shiftPressed) && !keyControlled)
                    {
                        TimeSpan span = (TimeSpan) (now - this.mailLineDoubleClick);
                        if (span.TotalSeconds < 2.0)
                        {
                            GameEngine.Instance.playInterfaceSound("MailScreen_thread_opened");
                            this.openMailThread(this.selectedMailThreadID);
                            this.mailLineDoubleClick = DateTime.MinValue;
                            return;
                        }
                    }
                    if ((!shiftPressed && !keyControlled) || (this.selectedMailThreadID < 0L))
                    {
                        this.selectedMailThreadID = mailThreadID;
                        if (!closeSections && (this.selectedMailThreadID < 0L))
                        {
                            return;
                        }
                        this.selectedMailThreadIDList.Clear();
                        this.selectedMailThreadIDList.Add(this.selectedMailThreadID);
                        if (this.selectedMailThreadID < 0L)
                        {
                            long selectedMailThreadID = this.selectedMailThreadID;
                            if ((selectedMailThreadID <= -1L) && (selectedMailThreadID >= -5L))
                            {
                                switch (((int) (selectedMailThreadID - -5L)))
                                {
                                    case 0:
                                        GameEngine.Instance.playInterfaceSound("MailScreen_thread_toggled_old");
                                        this.openAll = !this.openAll;
                                        if (!this.downloadedAll)
                                        {
                                            RemoteServices.Instance.GetMailThreadList(true, 5, this.lastTimeThreadsReceived);
                                            this.downloadedYesterday = true;
                                            this.downloaded3Days = true;
                                            this.downloadedThisWeek = true;
                                            this.downloadedThisMonth = true;
                                            this.downloadedAll = true;
                                        }
                                        break;

                                    case 1:
                                        GameEngine.Instance.playInterfaceSound("MailScreen_thread_toggled_old");
                                        this.openThisMonth = !this.openThisMonth;
                                        if (!this.downloadedThisMonth)
                                        {
                                            RemoteServices.Instance.GetMailThreadList(true, 4, this.lastTimeThreadsReceived);
                                            this.downloadedYesterday = true;
                                            this.downloaded3Days = true;
                                            this.downloadedThisWeek = true;
                                            this.downloadedThisMonth = true;
                                        }
                                        break;

                                    case 2:
                                        GameEngine.Instance.playInterfaceSound("MailScreen_thread_toggled_old");
                                        this.openThisWeek = !this.openThisWeek;
                                        if (!this.downloadedThisWeek)
                                        {
                                            RemoteServices.Instance.GetMailThreadList(true, 3, this.lastTimeThreadsReceived);
                                            this.downloadedYesterday = true;
                                            this.downloaded3Days = true;
                                            this.downloadedThisWeek = true;
                                        }
                                        break;

                                    case 3:
                                        GameEngine.Instance.playInterfaceSound("MailScreen_thread_toggled_old");
                                        this.open3Days = !this.open3Days;
                                        if (!this.downloaded3Days)
                                        {
                                            RemoteServices.Instance.GetMailThreadList(true, 2, this.lastTimeThreadsReceived);
                                            this.downloadedYesterday = true;
                                            this.downloaded3Days = true;
                                        }
                                        break;

                                    case 4:
                                        GameEngine.Instance.playInterfaceSound("MailScreen_thread_toggled_old");
                                        this.openYesterday = !this.openYesterday;
                                        if (!this.downloadedYesterday)
                                        {
                                            RemoteServices.Instance.GetMailThreadList(true, 1, this.lastTimeThreadsReceived);
                                            this.downloadedYesterday = true;
                                        }
                                        break;
                                }
                            }
                            this.preSortThreadHeaders();
                            this.selectedMailThreadID = -1000L;
                            this.selectedMailThreadIDList.Clear();
                        }
                        else
                        {
                            GameEngine.Instance.playInterfaceSound("MailScreen_main_line_clicked");
                            this.mailLineDoubleClick = now;
                        }
                    }
                    else if (shiftPressed)
                    {
                        if (mailThreadID >= 0L)
                        {
                            long num2 = this.selectedMailThreadID;
                            long num3 = mailThreadID;
                            if (num2 != num3)
                            {
                                bool flag3 = false;
                                foreach (MailThreadListItem item in this.m_preSortedHeaders)
                                {
                                    bool flag4 = flag3;
                                    if ((item.mailThreadID == num2) || (item.mailThreadID == num3))
                                    {
                                        if (!flag3)
                                        {
                                            flag3 = true;
                                            flag4 = true;
                                        }
                                        else
                                        {
                                            flag3 = false;
                                            flag4 = true;
                                        }
                                    }
                                    if (flag4 && !this.selectedMailThreadIDList.Contains(item.mailThreadID))
                                    {
                                        this.selectedMailThreadIDList.Add(item.mailThreadID);
                                    }
                                }
                            }
                        }
                    }
                    else if (keyControlled && (mailThreadID >= 0L))
                    {
                        if (this.selectedMailThreadIDList.Contains(mailThreadID))
                        {
                            this.selectedMailThreadIDList.Remove(mailThreadID);
                            if (this.selectedMailThreadID == mailThreadID)
                            {
                                if (this.selectedMailThreadIDList.Count > 0)
                                {
                                    this.selectedMailThreadID = this.selectedMailThreadIDList[0];
                                }
                                else
                                {
                                    this.selectedMailThreadID = -1L;
                                }
                            }
                        }
                        else
                        {
                            this.selectedMailThreadIDList.Add(mailThreadID);
                        }
                    }
                    this.repopulateTable();
                    if (this.selectedMailThreadIDList.Count > 1)
                    {
                        this.mailList_iconSelectedOpen.Enabled = false;
                        this.mailList_iconSelectedMoveThread.Text.Text = SK.Text("MailScreen_Move_These_Threads", "Move These Threads");
                        this.mailList_iconSelectedDelete.Text.Text = SK.Text("MailScreen_Delete_Threads", "Delete Threads");
                    }
                    else
                    {
                        this.mailList_iconSelectedOpen.Enabled = true;
                        this.mailList_iconSelectedMoveThread.Text.Text = SK.Text("MailScreen_Move_This_Thread", "Move This Thread");
                        this.mailList_iconSelectedDelete.Text.Text = SK.Text("MailScreen_Delete_Thread", "Delete Thread");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void mailList_BlockUser()
        {
            if (this.selectedUserName.Length > 0)
            {
                this.blockListChanged = false;
                MailUserBlockPopup.ShowPopup(this, this.selectedUserName);
                if (this.blockListChanged)
                {
                    this.showMailItem(this.lastLineClicked);
                }
            }
        }

        private void mailList_BlockUser2()
        {
            this.blockListChanged = false;
            MailUserBlockPopup.ShowPopup(this, "");
            if (this.blockListChanged)
            {
                this.repopulateTable();
            }
        }

        private void mailList_CancelCreateFolder()
        {
            this.returnToMailList();
        }

        private void mailList_CancelMove()
        {
            this.closeMoveMail();
        }

        private void mailList_CreateFolder()
        {
            if (!this.doesFolderAlreadyExist())
            {
                RemoteServices.Instance.set_CreateMailFolder_UserCallBack(new RemoteServices.CreateMailFolder_UserCallBack(this.createMailFolderCallback));
                RemoteServices.Instance.CreateMailFolder(this.tbNewFolder.Text);
            }
            this.returnToMailList();
        }

        private void mailList_DeleteThread()
        {
            if (this.selectedMailThreadID >= 0L)
            {
                this.CloseDeleteThreadPopUp();
                InterfaceMgr.Instance.openGreyOutWindow(false, base.ParentForm);
                this.DeleteThreadPopUp = new MyMessageBoxPopUp();
                if (this.selectedMailThreadIDList.Count == 1)
                {
                    this.DeleteThreadPopUp.init(SK.Text("MailScreen_Delete_This_Thread", "Delete this thread?"), SK.Text("MailScreen_Confirmation", "Confirmation"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DeleteThreadOkClick));
                }
                else
                {
                    this.DeleteThreadPopUp.init(SK.Text("MailScreen_Delete_All_Threads", "Delete ALL selected threads?"), SK.Text("MailScreen_Confirmation", "Confirmation"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DeleteThreadOkClick));
                }
                this.DeleteThreadPopUp.Show(InterfaceMgr.Instance.getGreyOutWindow());
            }
        }

        private void mailList_ForwardMail()
        {
            this.proclamation = false;
            this.sendThreadID = this.selectedMailThreadID;
            this.sendAsForward = true;
            this.changeSearchTab(0, false);
            this.newMail_iconTab1Button.Visible = true;
            this.newMail_iconTab2Button.Visible = true;
            this.newMail_iconTab3Button.Visible = true;
            this.newMail_iconTab4Button.Visible = true;
            this.newMail_iconBackground.Visible = true;
            this.recipients.Clear();
            this.populateToList();
            this.newMail_ToList.Enabled = true;
            this.newMail_removeRecipient.Enabled = false;
            this.mailListArea.Visible = false;
            this.mailThreadArea.Visible = false;
            this.newMailArea.Visible = true;
            this.mailCreateFolderArea.Visible = false;
            this.tbMain.Visible = this.newMailArea.Visible;
            this.tbUserFilter.Visible = this.mailListArea.Visible;
            this.tbSubject.Visible = this.newMailArea.Visible;
            this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
            this.tbSubject.Enabled = false;
            this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
            this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail", "Back To Mail");
            this.headerLabel.Text = SK.Text("MailScreen_Forward", "Forward");
            this.headerLabel2.Text = "";
            this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Forward", "Forward");
            this.newMail_iconSendMail.Visible = true;
            this.tbMain.Text = "";
            MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[this.selectedMailThreadID];
            if (item != null)
            {
                this.tbSubject.Text = SK.Text("MailScreen_Forward_Abbreviation", "FW") + " : " + item.subject;
            }
            this.tbMain.Focus();
            this.updateSendButton();
        }

        private void mailList_MarkAsRead()
        {
            if (this.selectedMailThreadID >= 0L)
            {
                MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[this.selectedMailThreadID];
                if ((item != null) && (!item.readStatus || (this.selectedMailThreadIDList.Count > 0)))
                {
                    foreach (long num in this.selectedMailThreadIDList)
                    {
                        item = (MailThreadListItem) this.m_storedThreadHeaders[num];
                        if ((item != null) && !item.readStatus)
                        {
                            RemoteServices.Instance.FlagThreadRead(num);
                            item.readStatus = true;
                            if (this.m_storedThreads[num] != null)
                            {
                                MailThreadItem[] itemArray = (MailThreadItem[]) this.m_storedThreads[num];
                                foreach (MailThreadItem item2 in itemArray)
                                {
                                    item2.readStatus = true;
                                }
                            }
                        }
                    }
                    this.repopulateTable();
                }
            }
        }

        private void mailList_MarkAsUnRead()
        {
            if ((this.selectedMailThreadID >= 0L) && (((MailThreadListItem) this.m_storedThreadHeaders[this.selectedMailThreadID]) != null))
            {
                foreach (long num in this.selectedMailThreadIDList)
                {
                    MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[num];
                    if (item != null)
                    {
                        RemoteServices.Instance.FlagThreadUnread(num);
                        item.readStatus = false;
                        if (this.m_storedThreads[num] != null)
                        {
                            MailThreadItem[] itemArray = (MailThreadItem[]) this.m_storedThreads[num];
                            foreach (MailThreadItem item2 in itemArray)
                            {
                                item2.readStatus = false;
                            }
                        }
                    }
                }
                this.repopulateTable();
            }
        }

        private void mailList_MouseWheel(int delta)
        {
            if (delta < 0)
            {
                this.mailList_scrollBar.scrollDown();
            }
            else if (delta > 0)
            {
                this.mailList_scrollBar.scrollUp();
            }
        }

        private void mailList_MoveThread()
        {
            this.m_moveThreadMode = true;
            this.mailList_listArea.Visible = false;
            this.mailList_iconArea.Visible = false;
            this.mailList_scrollBar.Visible = false;
            this.mailList_scrollTabLines.Visible = false;
            this.mailList_upArrow.Visible = false;
            this.mailList_downArrow.Visible = false;
            this.mailList_listShadowTR.Visible = false;
            this.mailList_listShadowR.Visible = false;
            this.mailList_listShadowBR.Visible = false;
            this.mailList_listShadowB.Visible = false;
            this.mailList_listShadowBL.Visible = false;
            this.mailList_MoveFolderLabel.Visible = true;
            this.mailList_MoveFolderCancel.Visible = true;
            this.updateFolderList();
        }

        private void mailList_NewMail()
        {
            if (this.attachmentWindow == null)
            {
                MailAttachmentPopup popup = new MailAttachmentPopup(this);
                popup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), null);
                this.attachmentWindow = popup;
            }
            this.attachmentWindow.setReadOnly(false);
            this.openNewMail("", "");
        }

        private void mailList_OpenAttachmentWindow()
        {
            if (this.attachmentWindow != null)
            {
                this.attachmentWindow.setReadOnly(true);
                this.attachmentWindow.display(true, null, 0, 0);
            }
        }

        private void mailList_OpenMail()
        {
            if (this.selectedMailThreadID >= 0L)
            {
                this.openMailThread(this.selectedMailThreadID);
            }
        }

        private void mailList_ReportMail()
        {
            MailAbuseSubmissionForm form = new MailAbuseSubmissionForm();
            form.initProperties(false, SK.Text("Report_Mail_Abuse_Heading", "Report Mail Abuse"), null);
            form.InitReportData(this, this.selectedMailItemID, this.selectedMailThreadID, this.selectedUserName);
            form.display(true, null, 0, 0);
        }

        private void mailList_scrollBarMoved()
        {
            this.mailList_scrollTabLines.Position = new Point(this.mailList_scrollBar.TabPosition.X, ((this.mailList_scrollBar.TabSize - 8) / 2) + this.mailList_scrollBar.TabPosition.Y);
        }

        private void mailList_scrollBarValueMoved()
        {
            this.repopulateTable();
        }

        private void mailList_ScrollDown()
        {
            this.mailList_scrollBar.scrollDown();
        }

        private void mailList_ScrollUp()
        {
            this.mailList_scrollBar.scrollUp();
        }

        private void mailList_selectLineOpenAttachments()
        {
            this.mailLineClicked();
            this.mailList_OpenAttachmentWindow();
        }

        public void mailRecipientsCallback(GetMailRecipientsHistory_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.mailFavourites = returnData.mailFavourites;
                this.mailUsersHistory = returnData.mailUsersHistory;
            }
        }

        private void mailTextClicked()
        {
            ViewMailPopup popup = new ViewMailPopup();
            popup.init(this, this.lastSubject, this.mailThread_mailBodyText.Text, this.mailThread_mailHeaderFromNameLabel.Text, this.mailThread_mailHeaderDateValueLabel.Text);
            popup.ShowDialog(base.ParentForm);
        }

        private void mailThread_MouseWheel(int delta)
        {
            if (delta < 0)
            {
                this.mailThread_scrollBar.scrollDown();
            }
            else if (delta > 0)
            {
                this.mailThread_scrollBar.scrollUp();
            }
        }

        private void mailThread_reply()
        {
            if (this.attachmentWindow == null)
            {
                MailAttachmentPopup popup = new MailAttachmentPopup(this);
                popup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), null);
                this.attachmentWindow = popup;
            }
            this.attachmentWindow.setReadOnly(false);
            this.attachmentWindow.clearContents(true);
            this.proclamation = false;
            this.sendThreadID = this.selectedMailThreadID;
            this.sendAsForward = false;
            this.changeSearchTab(-1, false);
            this.newMail_iconTab1Button.Visible = false;
            this.newMail_iconTab2Button.Visible = false;
            this.newMail_iconTab3Button.Visible = false;
            this.newMail_iconTab4Button.Visible = false;
            this.newMail_iconBackground.Visible = false;
            this.populateToFromCurrentMail();
            this.newMail_ToList.Enabled = false;
            this.mailListArea.Visible = false;
            this.mailThreadArea.Visible = false;
            this.newMailArea.Visible = true;
            this.mailCreateFolderArea.Visible = false;
            this.tbMain.Visible = this.newMailArea.Visible;
            this.tbUserFilter.Visible = this.mailListArea.Visible;
            this.tbSubject.Visible = this.newMailArea.Visible;
            this.tbSubject.Enabled = false;
            this.tbFindInput.Visible = false;
            this.newMail_removeRecipient.Enabled = false;
            this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
            this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail", "Back To Mail");
            this.headerLabel.Text = SK.Text("MailScreen_Reply", "Reply");
            this.headerLabel2.Text = "";
            this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Reply", "Reply");
            this.newMail_iconSendMail.Visible = true;
            this.tbMain.Text = "";
            this.tbMain.Focus();
            this.updateSendButton();
        }

        private void mailThread_scrollBarMoved()
        {
            this.mailThread_scrollTabLines.Position = new Point(this.mailThread_scrollBar.TabPosition.X, ((this.mailThread_scrollBar.TabSize - 8) / 2) + this.mailThread_scrollBar.TabPosition.Y);
        }

        private void mailThread_scrollBarValueMoved()
        {
            this.displayThread(this.selectedMailThreadID, false);
        }

        private void mailThread_ScrollDown()
        {
            this.mailThread_scrollBar.scrollDown();
        }

        private void mailThread_ScrollUp()
        {
            this.mailThread_scrollBar.scrollUp();
        }

        private void mailThreadBody_MouseWheel(int delta)
        {
            if (delta < 0)
            {
                this.mailThreadBody_scrollBar.scrollDown();
            }
            else if (delta > 0)
            {
                this.mailThreadBody_scrollBar.scrollUp();
            }
        }

        private void mailThreadBody_scrollBarMoved()
        {
            this.mailThreadBody_scrollTabLines.Position = new Point(this.mailThreadBody_scrollBar.TabPosition.X, ((this.mailThreadBody_scrollBar.TabSize - 8) / 2) + this.mailThreadBody_scrollBar.TabPosition.Y);
        }

        private void mailThreadBody_scrollBarValueMoved()
        {
            this.mailThread_mailBodyText.VerticalOffset = this.mailThreadBody_scrollBar.Value;
        }

        private void mailThreadBody_ScrollDown()
        {
            this.mailThreadBody_scrollBar.scrollDown();
        }

        private void mailThreadBody_ScrollUp()
        {
            this.mailThreadBody_scrollBar.scrollUp();
        }

        public void mailThreadCallback(GetMailThread_ReturnType returnData)
        {
            if (returnData.Success && this.mailThreadArea.Visible)
            {
                if (returnData.items != null)
                {
                    if ((returnData.items.Count > 0) && RemoteServices.Instance.UserOptions.profanityFilter)
                    {
                        foreach (MailThreadItem item in returnData.items)
                        {
                            item.body = GameEngine.Instance.censorString(item.body);
                        }
                    }
                    if (this.m_storedThreads[returnData.threadID] == null)
                    {
                        List<MailThreadItem> list = new List<MailThreadItem>();
                        list.AddRange(returnData.items);
                        list.Sort(this.mailThreadComparer);
                        this.m_storedThreads[returnData.threadID] = list.ToArray();
                    }
                    else
                    {
                        List<MailThreadItem> list2 = new List<MailThreadItem>();
                        MailThreadItem[] collection = (MailThreadItem[]) this.m_storedThreads[returnData.threadID];
                        list2.AddRange(collection);
                        list2.AddRange(returnData.items);
                        list2.Sort(this.mailThreadComparer);
                        this.m_storedThreads[returnData.threadID] = list2.ToArray();
                    }
                    this.saveMail();
                }
                if (this.selectedMailThreadID == returnData.threadID)
                {
                    this.displayThread(returnData.threadID, true);
                    MailThreadListItem item2 = (MailThreadListItem) this.m_storedThreadHeaders[returnData.threadID];
                    if (item2 != null)
                    {
                        if (item2.readOnly)
                        {
                            this.mailThread_iconSelectedReply.Enabled = false;
                            this.mailThread_iconSelectedForward.Enabled = false;
                            if (item2.specialType == 1)
                            {
                                this.mailThread_iconSelectedBlockPoster.Enabled = false;
                            }
                        }
                        else
                        {
                            this.mailThread_iconSelectedReply.Enabled = true;
                            this.mailThread_iconSelectedForward.Enabled = true;
                        }
                    }
                }
            }
        }

        public void mailThreadListCallback(GetMailThreadList_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.lastTimeThreadsReceived = returnData.currentSystemTime;
                if (returnData.items != null)
                {
                    if ((returnData.items.Count > 0) && RemoteServices.Instance.UserOptions.profanityFilter)
                    {
                        foreach (MailThreadListItem item in returnData.items)
                        {
                            item.subject = GameEngine.Instance.censorString(item.subject);
                        }
                    }
                    int count = returnData.items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        this.m_storedThreadHeaders[returnData.items[i].mailThreadID] = returnData.items[i];
                    }
                }
                this.addBreakBars();
                this.preSortThreadHeaders();
                this.repopulateTable();
                if (returnData.items != null)
                {
                    this.saveMail();
                }
            }
        }

        public void mailTo(string username)
        {
            this.openNewMail("", "");
            this.addNameToRecipients(username);
        }

        public void mailTo(string[] usernames)
        {
            this.openNewMail("", "");
            foreach (string str in usernames)
            {
                this.addNameToRecipients(str);
            }
        }

        private void moveMail(List<long> threadIDs, long targetFolderID, MailFolderLine folderLine)
        {
            this.closeMoveMail();
            foreach (long num in threadIDs)
            {
                MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[num];
                item.folderID = targetFolderID;
                RemoteServices.Instance.MoveToMailFolder(num, targetFolderID);
                this.saveMail();
            }
            this.selectedMailThreadIDList.Clear();
            this.selectedMailThreadID = -1000L;
            this.m_flashFolderLine = folderLine;
            this.flashFolderCount = 0;
        }

        private void newMail_FavouritesLineClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.newMail_iconFavouritesAddButton.Enabled = true;
                this.newMail_iconFavouritesRemoveButton.Enabled = true;
            }
            else
            {
                this.newMail_iconFavouritesAddButton.Enabled = false;
                this.newMail_iconFavouritesRemoveButton.Enabled = false;
            }
        }

        private void newMail_FavouritesLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void newMail_FindLineClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.newMail_iconFindAddButton.Enabled = true;
                this.newMail_iconFindFavouritesButton.Enabled = true;
            }
            else
            {
                this.newMail_iconFindAddButton.Enabled = false;
                this.newMail_iconFindFavouritesButton.Enabled = false;
            }
        }

        private void newMail_FindLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void newMail_KnownLineClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.newMail_iconKnownAddButton.Enabled = true;
                this.newMail_iconKnownFavouritesButton.Enabled = true;
            }
            else
            {
                this.newMail_iconKnownAddButton.Enabled = false;
                this.newMail_iconKnownFavouritesButton.Enabled = false;
            }
        }

        private void newMail_KnownLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void newMail_RecentLineClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.newMail_iconRecentAddButton.Enabled = true;
                this.newMail_iconRecentFavouritesButton.Enabled = true;
            }
            else
            {
                this.newMail_iconRecentAddButton.Enabled = false;
                this.newMail_iconRecentFavouritesButton.Enabled = false;
            }
        }

        private void newMail_RecentLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.addNameToRecipients(item.Text);
            }
        }

        private void newMail_ToLineClicked(CustomSelfDrawPanel.CSDListItem item)
        {
            if (item != null)
            {
                this.newMail_removeRecipient.Enabled = true;
            }
            else
            {
                this.newMail_removeRecipient.Enabled = false;
            }
        }

        public void open(bool fresh, bool fromSelf)
        {
            this.headerLabel2.Size = new Size(700, 0x18);
            if (Program.mySettings.LanguageIdent == "de")
            {
                this.headerLabel2.Position = new Point(280, 12);
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                this.headerLabel2.Position = new Point(230, 12);
            }
            else if (Program.mySettings.LanguageIdent == "es")
            {
                this.headerLabel2.Position = new Point(230, 12);
            }
            else if (Program.mySettings.LanguageIdent == "tr")
            {
                this.headerLabel2.Position = new Point(230, 12);
            }
            else if (Program.mySettings.LanguageIdent == "it")
            {
                this.headerLabel2.Position = new Point(330, 12);
                this.headerLabel2.Size = new Size(570, 0x18);
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                this.headerLabel2.Position = new Point(300, 12);
                this.headerLabel2.Size = new Size(600, 0x18);
            }
            else if (Program.mySettings.LanguageIdent == "pl")
            {
                this.headerLabel2.Position = new Point(280, 12);
                this.headerLabel2.Size = new Size(620, 0x18);
            }
            else
            {
                this.headerLabel2.Position = new Point(200, 12);
            }
            if (this.initialRequest)
            {
                this.loadMail();
                this.loadBlockedList();
            }
            if (fresh)
            {
                if (!fromSelf)
                {
                    this.selectedFolderID = -1L;
                }
                this.closeMoveMail();
                if (!this.gotFolders)
                {
                    RemoteServices.Instance.set_GetMailFolders_UserCallBack(new RemoteServices.GetMailFolders_UserCallBack(this.mailFoldersCallback));
                    RemoteServices.Instance.GetMailFolders();
                    Thread.Sleep(500);
                    this.gotFolders = true;
                }
                else
                {
                    this.updateFolderList();
                }
                RemoteServices.Instance.set_FlagMailRead_UserCallBack(new RemoteServices.FlagMailRead_UserCallBack(this.flagMailCallback));
                RemoteServices.Instance.set_GetMailThreadList_UserCallBack(new RemoteServices.GetMailThreadList_UserCallBack(this.mailThreadListCallback));
                bool initialRequest = this.initialRequest;
                if (this.initialRequest && (this.lastTimeThreadsReceived > DateTime.Now.AddYears(-49)))
                {
                    this.initialRequest = false;
                }
                RemoteServices.Instance.GetMailThreadList(this.initialRequest, 5, this.lastTimeThreadsReceived);
                if (!fromSelf)
                {
                    this.selectedMailThreadID = -1000L;
                    this.selectedMailThreadIDList.Clear();
                    this.mailList_iconSelected.Visible = false;
                    this.mailList_iconSelectedBack.Visible = false;
                }
                this.mailListArea.Visible = true;
                this.mailThreadArea.Visible = false;
                this.newMailArea.Visible = false;
                this.mailCreateFolderArea.Visible = false;
                this.tbMain.Visible = this.newMailArea.Visible;
                this.tbUserFilter.Visible = this.mailListArea.Visible;
                this.tbSubject.Visible = this.newMailArea.Visible;
                this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
                this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
                this.headerLabel.Text = SK.Text("MailScreen_Mail", "Mail");
                this.headerLabel2.Text = "";
                if (!fromSelf && (initialRequest || this.mailSent))
                {
                    Thread.Sleep(500);
                    RemoteServices.Instance.set_GetMailRecipientsHistory_UserCallBack(new RemoteServices.GetMailRecipientsHistory_UserCallBack(this.mailRecipientsCallback));
                    RemoteServices.Instance.GetMailRecipientsHistory();
                }
                this.initialRequest = false;
                this.mailSent = false;
            }
            if (this.m_parent != null)
            {
                if (this.m_parent.isDocked())
                {
                    this.dockButton.CustomTooltipID = 500;
                    this.dockButton.ImageNorm = (Image) GFXLibrary.mail2_detach_window_normal;
                    this.dockButton.ImageOver = (Image) GFXLibrary.mail2_detach_window_over;
                    this.dockButton.ImageClick = (Image) GFXLibrary.mail2_detach_window_in;
                }
                else
                {
                    this.dockButton.CustomTooltipID = 0x1f5;
                    this.dockButton.ImageNorm = (Image) GFXLibrary.mail2_detach_attach_window_normal;
                    this.dockButton.ImageOver = (Image) GFXLibrary.mail2_detach_attach_window_over;
                    this.dockButton.ImageClick = (Image) GFXLibrary.mail2_detach_attach_window_in;
                }
            }
            this.update();
        }

        private void openMailThread(long threadID)
        {
            if (this.m_storedThreadHeaders[threadID] != null)
            {
                MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[threadID];
                if (item != null)
                {
                    this.mailListArea.Visible = false;
                    this.mailThreadArea.Visible = true;
                    this.newMailArea.Visible = false;
                    this.mailCreateFolderArea.Visible = false;
                    this.tbMain.Visible = this.newMailArea.Visible;
                    this.tbUserFilter.Visible = this.mailListArea.Visible;
                    this.tbSubject.Visible = this.newMailArea.Visible;
                    this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
                    this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
                    this.selectedMailItemID = -1000L;
                    this.headerLabel.Text = SK.Text("MailScreen_Mail_Thread", "Mail Thread") + " : ";
                    string subject = item.subject;
                    if (item.readOnly)
                    {
                        switch (item.specialType)
                        {
                            case 1:
                                subject = this.languageSplitString(item.subject);
                                break;

                            case 2:
                                subject = SK.Text("MailScreen_House_Proclamation", "House Proclamation");
                                break;

                            case 3:
                                subject = "";
                                break;

                            case 4:
                                subject = SK.Text("MailScreen_Parish_Proclamation", "Parish Proclamation") + " : " + GameEngine.Instance.World.getParishName(item.specialArea);
                                break;

                            case 5:
                                subject = SK.Text("MailScreen_County_Proclamation", "County Proclamation") + " : " + GameEngine.Instance.World.getCountyName(item.specialArea);
                                break;

                            case 6:
                                subject = SK.Text("MailScreen_Province_Proclamation", "Province Proclamation") + " : " + GameEngine.Instance.World.getProvinceName(item.specialArea);
                                break;

                            case 7:
                                subject = SK.Text("MailScreen_Country_Proclamation", "Country Proclamation") + " : " + GameEngine.Instance.World.getCountryName(item.specialArea);
                                break;
                        }
                    }
                    this.lastSubject = subject;
                    this.headerLabel2.Text = "\"" + subject + "\"";
                    this.mailThread_mailBodyText.Text = "";
                    this.mailThread_mailHeaderDateValueLabel.Text = "";
                    this.mailThread_mailHeaderFromNameLabel.Text = "";
                    this.mailThread_fromShield.Visible = false;
                    if (item.readOnly)
                    {
                        this.mailThread_iconSelectedReply.Enabled = false;
                        this.mailThread_iconSelectedForward.Enabled = false;
                        if (item.specialType == 1)
                        {
                            this.mailThread_iconSelectedBlockPoster.Enabled = false;
                            this.mailThread_iconSelectedReportMail.Enabled = false;
                        }
                        else
                        {
                            this.mailThread_iconSelectedBlockPoster.Enabled = true;
                            this.mailThread_iconSelectedReportMail.Enabled = true;
                        }
                    }
                    else
                    {
                        this.mailThread_iconSelectedReply.Enabled = true;
                        this.mailThread_iconSelectedForward.Enabled = true;
                        this.mailThread_iconSelectedBlockPoster.Enabled = true;
                        this.mailThread_iconSelectedReportMail.Enabled = true;
                    }
                    this.reportButtonAvailable = this.mailThread_iconSelectedReportMail.Enabled;
                    this.blockButtonAvailable = this.mailThread_iconSelectedBlockPoster.Enabled;
                    long highestSegmentID = -1L;
                    int localCount = 0;
                    if (this.m_storedThreads[threadID] != null)
                    {
                        MailThreadItem[] itemArray = (MailThreadItem[]) this.m_storedThreads[threadID];
                        localCount = itemArray.Length;
                        foreach (MailThreadItem item2 in itemArray)
                        {
                            if (item2.mailID > highestSegmentID)
                            {
                                highestSegmentID = item2.mailID;
                            }
                        }
                        this.displayThread(threadID, false);
                    }
                    else
                    {
                        this.clearMailThread();
                    }
                    RemoteServices.Instance.set_GetMailThread_UserCallBack(new RemoteServices.GetMailThread_UserCallBack(this.mailThreadCallback));
                    RemoteServices.Instance.GetMailThread(threadID, localCount, highestSegmentID);
                }
            }
        }

        private void openNewAttachmentsPopup()
        {
            if (this.attachmentWindow != null)
            {
                this.attachmentWindow.display(true, null, 0, 0);
            }
        }

        private void openNewMail(string subject, string body)
        {
            this.proclamation = false;
            if (this.attachmentWindow == null)
            {
                MailAttachmentPopup popup = new MailAttachmentPopup(this);
                popup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), null);
                this.attachmentWindow = popup;
            }
            if (this.attachmentWindow != null)
            {
                this.attachmentWindow.clearContents(true);
                this.attachmentWindow.setReadOnly(false);
            }
            this.sendThreadID = -1L;
            this.sendAsForward = false;
            this.tbFindInput.Text = "";
            this.changeSearchTab(0, false);
            this.newMail_iconTab1Button.Visible = true;
            this.newMail_iconTab2Button.Visible = true;
            this.newMail_iconTab3Button.Visible = true;
            this.newMail_iconTab4Button.Visible = true;
            this.newMail_iconBackground.Visible = true;
            this.recipients.Clear();
            this.populateToList();
            this.newMail_ToList.Enabled = true;
            this.newMail_removeRecipient.Enabled = false;
            this.mailListArea.Visible = false;
            this.mailThreadArea.Visible = false;
            this.newMailArea.Visible = true;
            this.mailCreateFolderArea.Visible = false;
            this.tbMain.Visible = this.newMailArea.Visible;
            this.tbUserFilter.Visible = this.mailListArea.Visible;
            this.tbSubject.Visible = this.newMailArea.Visible;
            this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
            this.tbSubject.Enabled = true;
            this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
            this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail_List", "Back To Mail List");
            this.headerLabel.Text = SK.Text("MailScreen_New_Mail", "New Mail");
            this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Send_Mail", "Send Mail");
            this.newMail_iconSendMail.Visible = true;
            this.headerLabel2.Text = "";
            if (body.Length > 0)
            {
                this.tbMain.Text = this.parseAttachmentString(body, true);
            }
            else
            {
                this.tbMain.Text = body;
            }
            this.tbSubject.Text = subject;
            this.tbMain.Focus();
            this.updateSendButton();
        }

        private string parseAttachmentString(string bodyText, bool applyLinks)
        {
            string[] separator = new string[] { "<!attch!>" };
            char[] chArray = new char[] { '%' };
            char[] chArray2 = new char[] { '#' };
            string[] strArray2 = bodyText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.mailThread_openAttachments.Enabled = false;
            this.mailThread_openAttachments.Visible = false;
            if ((strArray2.Length > 1) && applyLinks)
            {
                this.mailThread_openAttachments.Enabled = true;
                this.mailThread_openAttachments.Visible = true;
                List<MailLink> inputList = new List<MailLink>();
                foreach (string str in strArray2[1].Split(chArray))
                {
                    if (str != "")
                    {
                        string[] strArray4 = str.Split(chArray2);
                        if (strArray4.Length > 0)
                        {
                            try
                            {
                                MailLink item = new MailLink {
                                    linkType = Convert.ToInt32(strArray4[0]),
                                    objectName = strArray4[1]
                                };
                                if (strArray4.Length > 2)
                                {
                                    item.objectID = Convert.ToInt32(strArray4[2]);
                                }
                                inputList.Add(item);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                if (this.attachmentWindow == null)
                {
                    MailAttachmentPopup popup = new MailAttachmentPopup(this);
                    popup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), null);
                    this.attachmentWindow = popup;
                }
                this.attachmentWindow.clearContents(true);
                this.attachmentWindow.SetLinks(inputList, true);
            }
            if (strArray2.Length > 0)
            {
                return strArray2[0];
            }
            return bodyText;
        }

        private void populateToFromCurrentMail()
        {
            this.recipients.Clear();
            MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[this.selectedMailThreadID];
            if (item != null)
            {
                if (item.otherUser != null)
                {
                    this.recipients.AddRange(item.otherUser);
                }
                this.tbSubject.Text = item.subject;
            }
            this.populateToList();
        }

        private void populateToList()
        {
            List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
            foreach (string str in this.recipients)
            {
                CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                    Text = str
                };
                items.Add(item);
            }
            this.newMail_ToList.populate(items);
            this.updateSendButton();
        }

        private void PopUpOkClick()
        {
            RemoteServices.Instance.set_RemoveMailFolder_UserCallBack(new RemoteServices.RemoveMailFolder_UserCallBack(this.removeMailFolderCallback));
            RemoteServices.Instance.RemoveMailFolder(this.selectedFolderID);
            InterfaceMgr.Instance.closeGreyOut();
            this.deleteFolderPopUp.Close();
        }

        public void preSortThreadHeaders()
        {
            this.m_preSortedHeaders.Clear();
            foreach (MailThreadListItem item in this.m_storedThreadHeaders)
            {
                if ((item.folderID == this.selectedFolderID) || (item.mailThreadID < 0L))
                {
                    this.m_preSortedHeaders.Add(item);
                }
            }
            this.m_preSortedHeaders.Sort(this.mailThreadHeaderComparer);
            this.m_preSortedALLHeader.Clear();
            foreach (MailThreadListItem item2 in this.m_preSortedHeaders)
            {
                this.m_preSortedALLHeader.Add(item2);
            }
            DateTime time = VillageMap.getCurrentServerTime();
            DateTime time2 = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1);
            DateTime time3 = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1).AddDays(-1.0);
            DateTime time4 = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1).AddDays(-3.0);
            DateTime time5 = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1).AddDays(-7.0);
            DateTime time6 = new DateTime(time.Year, time.Month, time.Day, 0, 0, 1).AddYears(-50);
            List<MailThreadListItem> list = new List<MailThreadListItem>();
            if (!this.openYesterday)
            {
                foreach (MailThreadListItem item3 in this.m_preSortedHeaders)
                {
                    if (((item3.mailThreadID >= 0L) && (item3.mailTime < time2)) && (item3.mailTime > time3))
                    {
                        list.Add(item3);
                    }
                }
            }
            if (!this.open3Days)
            {
                foreach (MailThreadListItem item4 in this.m_preSortedHeaders)
                {
                    if (((item4.mailThreadID >= 0L) && (item4.mailTime < time3)) && (item4.mailTime > time4))
                    {
                        list.Add(item4);
                    }
                }
            }
            if (!this.openThisWeek)
            {
                foreach (MailThreadListItem item5 in this.m_preSortedHeaders)
                {
                    if (((item5.mailThreadID >= 0L) && (item5.mailTime < time4)) && (item5.mailTime > time5))
                    {
                        list.Add(item5);
                    }
                }
            }
            if (!this.openThisMonth)
            {
                foreach (MailThreadListItem item6 in this.m_preSortedHeaders)
                {
                    if (((item6.mailThreadID >= 0L) && (item6.mailTime < time5)) && (item6.mailTime > time6))
                    {
                        list.Add(item6);
                    }
                }
            }
            foreach (MailThreadListItem item7 in list)
            {
                this.m_preSortedHeaders.Remove(item7);
            }
        }

        public void refreshMail()
        {
            if (this.mailListArea.Visible && this.mailList_listArea.Visible)
            {
                this.open(true, true);
            }
        }

        private void removeMailFolderCallback(RemoveMailFolder_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.m_mailFolders.Clear();
                this.m_mailFolders.AddRange(returnData.folders);
                foreach (MailThreadListItem item in this.m_storedThreadHeaders)
                {
                    if (item.folderID == returnData.deletedFolderID)
                    {
                        item.folderID = -1L;
                    }
                }
                this.selectedFolderID = -1L;
                this.preSortThreadHeaders();
                this.repopulateTable();
            }
        }

        private void removeNameFromFavourites()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_iconFavouritesList.getSelectedItem();
            if (item != null)
            {
                List<string> list = new List<string>();
                if (this.mailFavourites != null)
                {
                    foreach (string str in this.mailFavourites)
                    {
                        if (!(str == item.Text))
                        {
                            list.Add(str);
                        }
                    }
                }
                if (this.mailFavourites.Length != list.Count)
                {
                    this.mailFavourites = list.ToArray();
                    RemoteServices.Instance.RemoveUserFromFavourites(item.Text);
                    this.fillFavouritesList();
                }
            }
        }

        private void removeNameFromRecipients()
        {
            CustomSelfDrawPanel.CSDListItem item = this.newMail_ToList.getSelectedItem();
            if (item != null)
            {
                string text = item.Text;
                if (this.recipients.Contains(text))
                {
                    this.recipients.Remove(text);
                    this.populateToList();
                    if (this.recipients.Count == 0)
                    {
                        this.newMail_removeRecipient.Enabled = false;
                    }
                }
            }
        }

        private void repopulateTable()
        {
            int num = this.mailList_scrollBar.Max + 0x1b;
            if (this.m_preSortedHeaders != null)
            {
                int count = this.m_preSortedHeaders.Count;
                for (int k = 0; k < this.m_preSortedHeaders.Count; k++)
                {
                    MailThreadListItem item = this.m_preSortedHeaders[k];
                    if (item != null)
                    {
                        bool flag = false;
                        bool flag2 = false;
                        if (item.otherUser != null)
                        {
                            if (item.otherUser.Length > 0)
                            {
                                int num4 = 0;
                                for (int m = 0; m < item.otherUser.Length; m++)
                                {
                                    if (this.blockedList.Contains(item.otherUser[m]))
                                    {
                                        num4++;
                                    }
                                    if (this.tbUserFilter.Text.Length > 0)
                                    {
                                        if (item.otherUser[m].ToLowerInvariant().Contains(this.tbUserFilter.Text.ToLowerInvariant()))
                                        {
                                            flag2 = true;
                                        }
                                    }
                                    else
                                    {
                                        flag2 = true;
                                    }
                                }
                                if (this.aggressiveBlocking)
                                {
                                    if (num4 > 0)
                                    {
                                        flag = true;
                                    }
                                }
                                else if (num4 == item.otherUser.Length)
                                {
                                    flag = true;
                                }
                            }
                            else if ((this.tbUserFilter.Text.Length > 0) && item.readOnly)
                            {
                                flag2 = false;
                            }
                            else
                            {
                                flag2 = true;
                            }
                        }
                        else
                        {
                            flag2 = true;
                        }
                        if (flag || !flag2)
                        {
                            count--;
                        }
                    }
                }
                if (num > count)
                {
                    int num6 = Math.Max(0, count - 0x1b);
                    if (this.mailList_scrollBar.Value > num6)
                    {
                        this.mailList_scrollBar.Value = num6;
                    }
                    this.mailList_scrollBar.Max = num6;
                }
                else
                {
                    this.mailList_scrollBar.Max = Math.Max(0, count - 0x1b);
                }
            }
            else
            {
                this.mailList_scrollBar.Max = 0;
            }
            for (int i = 0; i < 0x1b; i++)
            {
                MailListLine line = this.getMailListLine(i);
                line.Subject.Text = "";
                line.Sender.Text = "";
                line.DateLabel.Text = "";
                line.Icon.Image = null;
                line.reset();
                line.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            }
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            if (this.m_preSortedALLHeader != null)
            {
                for (int n = 0; n < this.m_preSortedALLHeader.Count; n++)
                {
                    MailThreadListItem item2 = this.m_preSortedALLHeader[n];
                    if (item2 != null)
                    {
                        if (item2.mailThreadID == -1L)
                        {
                            num12 = 1;
                        }
                        else if (item2.mailThreadID == -2L)
                        {
                            num12 = 2;
                        }
                        else if (item2.mailThreadID == -3L)
                        {
                            num12 = 3;
                        }
                        else if (item2.mailThreadID == -4L)
                        {
                            num12 = 4;
                        }
                        else if (!item2.readStatus)
                        {
                            switch (num12)
                            {
                                case 1:
                                    num8++;
                                    break;

                                case 2:
                                    num9++;
                                    break;

                                case 3:
                                    num10++;
                                    break;

                                case 4:
                                    num11++;
                                    break;
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < 0x1b; j++)
            {
                this.getMailListLine(j).Data = -1;
            }
            if (this.m_preSortedHeaders != null)
            {
                int num15 = this.mailList_scrollBar.Value;
                int lineID = 0;
                while ((lineID < 0x1b) && (num15 < this.m_preSortedHeaders.Count))
                {
                    string userName;
                    MailThreadListItem item3 = this.m_preSortedHeaders[num15];
                    MailListLine line3 = this.getMailListLine(lineID);
                    line3.Data = num15;
                    if ((item3 == null) || (line3 == null))
                    {
                        goto Label_0AE0;
                    }
                    if (item3.mailThreadID >= 0L)
                    {
                        goto Label_068E;
                    }
                    long mailThreadID = item3.mailThreadID;
                    if ((mailThreadID <= -1L) && (mailThreadID >= -5L))
                    {
                        switch (((int) (mailThreadID - -5L)))
                        {
                            case 0:
                                line3.Subject.Text = SK.Text("MailScreen_Date_All", "Date: All");
                                if (this.openAll)
                                {
                                    goto Label_0605;
                                }
                                line3.Icon.Image = (Image) GFXLibrary.mail_plus;
                                break;

                            case 1:
                                goto Label_054A;

                            case 2:
                                goto Label_04C4;

                            case 3:
                                goto Label_043E;

                            case 4:
                                goto Label_03B8;
                        }
                    }
                    goto Label_061B;
                Label_03B8:
                    line3.Subject.Text = SK.Text("MailScreen_Date_Yesterday", "Date: Yesterday");
                    if (num8 > 0)
                    {
                        CustomSelfDrawPanel.CSDLabel subject = line3.Subject;
                        subject.Text = subject.Text + " (" + num8.ToString() + ")";
                    }
                    if (!this.openYesterday)
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_plus;
                    }
                    else
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_minus;
                    }
                    goto Label_061B;
                Label_043E:
                    line3.Subject.Text = SK.Text("MailScreen_Date_Last_3_Days", "Date: Last 3 Days");
                    if (num9 > 0)
                    {
                        CustomSelfDrawPanel.CSDLabel label2 = line3.Subject;
                        label2.Text = label2.Text + " (" + num9.ToString() + ")";
                    }
                    if (!this.open3Days)
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_plus;
                    }
                    else
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_minus;
                    }
                    goto Label_061B;
                Label_04C4:
                    line3.Subject.Text = SK.Text("MailScreen_Date_Last_7_Days", "Date: Last 7 Days");
                    if (num10 > 0)
                    {
                        CustomSelfDrawPanel.CSDLabel label3 = line3.Subject;
                        label3.Text = label3.Text + " (" + num10.ToString() + ")";
                    }
                    if (!this.openThisWeek)
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_plus;
                    }
                    else
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_minus;
                    }
                    goto Label_061B;
                Label_054A:
                    line3.Subject.Text = SK.Text("MailScreen_Date_Last_30_Days", "Date: Last 30 Days");
                    if (num11 > 0)
                    {
                        CustomSelfDrawPanel.CSDLabel label4 = line3.Subject;
                        label4.Text = label4.Text + " (" + num11.ToString() + ")";
                    }
                    if (!this.openThisMonth)
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_plus;
                    }
                    else
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_minus;
                    }
                    goto Label_061B;
                Label_0605:
                    line3.Icon.Image = (Image) GFXLibrary.mail_minus;
                Label_061B:
                    line3.Sender.Text = "";
                    line3.DateLabel.Text = "";
                    line3.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                    line3.LineColor = CustomSelfDrawPanel.MailSelectedColor;
                    line3.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                    line3.LineOverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                    line3.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                    goto Label_0AD9;
                Label_068E:
                    userName = SK.Text("MAILSCREEN_NO_RECIPIENTS", "No Recipients?");
                    bool flag3 = false;
                    bool flag4 = false;
                    int num17 = 0;
                    bool flag5 = false;
                    if (item3.otherUser != null)
                    {
                        if (item3.otherUser.Length > 0)
                        {
                            flag4 = true;
                            userName = item3.otherUser[0];
                            for (int num18 = 0; num18 < item3.otherUser.Length; num18++)
                            {
                                if (this.blockedList.Contains(item3.otherUser[num18]))
                                {
                                    num17++;
                                }
                                if (this.tbUserFilter.Text.Length > 0)
                                {
                                    if (item3.otherUser[num18].ToLowerInvariant().Contains(this.tbUserFilter.Text.ToLowerInvariant()))
                                    {
                                        flag5 = true;
                                    }
                                }
                                else
                                {
                                    flag5 = true;
                                }
                            }
                            if (num17 > 0)
                            {
                                flag3 = true;
                            }
                        }
                        else if ((this.tbUserFilter.Text.Length > 0) && item3.readOnly)
                        {
                            flag5 = false;
                        }
                        else
                        {
                            flag5 = true;
                        }
                        for (int num19 = 1; num19 < item3.otherUser.Length; num19++)
                        {
                            userName = userName + ", " + item3.otherUser[num19];
                        }
                    }
                    else
                    {
                        flag5 = true;
                    }
                    if (!flag3 && flag5)
                    {
                        line3.Subject.Text = item3.subject;
                        if (item3.readOnly)
                        {
                            switch (item3.specialType)
                            {
                                case 1:
                                    userName = SK.Text("The_Kingdoms_Team", "The Kingdoms Team");
                                    line3.Subject.Text = this.languageSplitString(item3.subject);
                                    break;

                                case 2:
                                    line3.Subject.Text = SK.Text("MailScreen_House_Proclamation", "House Proclamation");
                                    if (!flag4)
                                    {
                                        userName = RemoteServices.Instance.UserName;
                                    }
                                    break;

                                case 3:
                                    line3.Subject.Text = "";
                                    break;

                                case 4:
                                    line3.Subject.Text = SK.Text("MailScreen_Parish_Proclamation", "Parish Proclamation") + " : " + GameEngine.Instance.World.getParishName(item3.specialArea);
                                    if (!flag4)
                                    {
                                        userName = RemoteServices.Instance.UserName;
                                    }
                                    break;

                                case 5:
                                    line3.Subject.Text = SK.Text("MailScreen_County_Proclamation", "County Proclamation") + " : " + GameEngine.Instance.World.getCountyName(item3.specialArea);
                                    if (!flag4)
                                    {
                                        userName = RemoteServices.Instance.UserName;
                                    }
                                    break;

                                case 6:
                                    line3.Subject.Text = SK.Text("MailScreen_Province_Proclamation", "Province Proclamation") + " : " + GameEngine.Instance.World.getProvinceName(item3.specialArea);
                                    if (!flag4)
                                    {
                                        userName = RemoteServices.Instance.UserName;
                                    }
                                    break;

                                case 7:
                                    line3.Subject.Text = SK.Text("MailScreen_Country_Proclamation", "Country Proclamation") + " : " + GameEngine.Instance.World.getCountryName(item3.specialArea);
                                    if (!flag4)
                                    {
                                        userName = RemoteServices.Instance.UserName;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (!flag5)
                        {
                            lineID--;
                            goto Label_0AE0;
                        }
                        if (this.aggressiveBlocking)
                        {
                            lineID--;
                            goto Label_0AE0;
                        }
                        if (num17 == item3.otherUser.Length)
                        {
                            lineID--;
                            goto Label_0AE0;
                        }
                        line3.Subject.Text = "         * " + SK.Text("MailBlock_blocked", "Blocked") + " *";
                    }
                    line3.Sender.Text = userName;
                    line3.Date = item3.mailTime;
                    if (item3.readStatus)
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_letter_icon_open;
                    }
                    else
                    {
                        line3.Icon.Image = (Image) GFXLibrary.mail_letter_icon_closed;
                        line3.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
                    }
                    if ((item3.mailThreadID == this.selectedMailThreadID) || this.selectedMailThreadIDList.Contains(item3.mailThreadID))
                    {
                        line3.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                        line3.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                    }
                Label_0AD9:
                    line3.invalidate();
                Label_0AE0:
                    lineID++;
                    num15++;
                }
            }
            if (this.selectedMailThreadID >= 0L)
            {
                this.mailList_iconSelected.Visible = true;
                this.mailList_iconSelectedBack.Visible = true;
            }
            else
            {
                this.mailList_iconSelected.Visible = false;
                this.mailList_iconSelectedBack.Visible = false;
            }
            this.mailList_scrollBar.recalc();
            this.mailList_scrollBar.invalidate();
            this.mailList_scrollBarMoved();
            this.updateFolderList();
        }

        public void ReportMailCallback(ReportMail_ReturnType returnData)
        {
            if (returnData.Success)
            {
                MyMessageBox.Show(SK.Text("MailScreen_Has_Been_Reported", "This mail has been successfully reported."), SK.Text("MailScreen_Abuse_Report", "Abuse Report"));
            }
        }

        private void returnFromNewMail()
        {
            if (this.sendThreadID < 0L)
            {
                this.returnToMailList();
            }
            else
            {
                this.openMailThread(this.sendThreadID);
            }
        }

        private void returnToMailList()
        {
            this.mailListArea.Visible = true;
            this.mailThreadArea.Visible = false;
            this.newMailArea.Visible = false;
            this.mailCreateFolderArea.Visible = false;
            this.tbMain.Visible = this.newMailArea.Visible;
            this.tbUserFilter.Visible = this.mailListArea.Visible;
            this.tbSubject.Visible = this.newMailArea.Visible;
            this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
            this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
            this.headerLabel.Text = SK.Text("MailScreen_Mail", "Mail");
            this.headerLabel2.Text = "";
            this.repopulateTable();
            base.Focus();
        }

        public void saveBlockedList()
        {
            int userID = RemoteServices.Instance.UserID;
            string str = GameEngine.getSettingsPath(true);
            try
            {
                FileInfo info = new FileInfo(str + @"\MailBlockList-" + userID.ToString() + ".dat") {
                    IsReadOnly = false
                };
            }
            catch (Exception)
            {
            }
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                output = new FileStream(str + @"\MailBlockList-" + userID.ToString() + ".dat", FileMode.Create);
                writer = new BinaryWriter(output);
                int count = this.blockedList.Count;
                writer.Write(count);
                foreach (string str2 in this.blockedList)
                {
                    writer.Write(str2);
                }
                writer.Write(this.aggressiveBlocking);
                writer.Close();
                output.Close();
            }
            catch (Exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void saveMail()
        {
            int userID = RemoteServices.Instance.UserID;
            string str = GameEngine.getSettingsPath(true);
            try
            {
                FileInfo info = new FileInfo(str + @"\MailData-" + userID.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat") {
                    IsReadOnly = false
                };
            }
            catch (Exception)
            {
            }
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                output = new FileStream(str + @"\MailData-" + userID.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat", FileMode.Create);
                writer = new BinaryWriter(output);
                writer.Write(this.lastTimeThreadsReceived.Ticks);
                int count = this.m_storedThreadHeaders.Count;
                writer.Write(count);
                foreach (MailThreadListItem item in this.m_storedThreadHeaders)
                {
                    writer.Write(item.mailThreadID);
                    if (item.mailThreadID >= 0L)
                    {
                        writer.Write(item.folderID);
                        writer.Write(item.mailTime.Ticks);
                        writer.Write(item.mailTimeAsDouble);
                        int length = 0;
                        if (item.otherUser != null)
                        {
                            length = item.otherUser.Length;
                        }
                        writer.Write(length);
                        if (item.otherUser != null)
                        {
                            foreach (string str2 in item.otherUser)
                            {
                                writer.Write(str2);
                            }
                        }
                        writer.Write(item.readStatus);
                        writer.Write(item.subject);
                        int num4 = -5;
                        writer.Write(num4);
                        writer.Write(item.readOnly);
                        writer.Write(item.specialType);
                        writer.Write(item.specialArea);
                        MailThreadItem[] itemArray = (MailThreadItem[]) this.m_storedThreads[item.mailThreadID];
                        if (itemArray == null)
                        {
                            int num5 = 0;
                            writer.Write(num5);
                        }
                        else
                        {
                            int num6 = itemArray.Length;
                            writer.Write(num6);
                            foreach (MailThreadItem item2 in itemArray)
                            {
                                writer.Write(item2.body);
                                writer.Write(item2.mailID);
                                writer.Write(item2.mailTime.Ticks);
                                writer.Write(item2.mailTimeAsDouble);
                                writer.Write(item2.otherUser);
                                writer.Write(item2.otherUserID);
                                writer.Write(item2.readStatus);
                            }
                        }
                    }
                }
                writer.Close();
                output.Close();
            }
            catch (Exception exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
                MyMessageBox.Show(SK.Text("MailScreen_Saving_Problem", "A problem occurred saving 'MailData.data'") + "\n\n" + exception.ToString(), SK.Text("WorldMapLoader_DataSaveError_Header", "Data Save Error"));
            }
        }

        private void searchTab1Clicked()
        {
            this.changeSearchTab(0, true);
        }

        private void searchTab2Clicked()
        {
            this.changeSearchTab(1, true);
        }

        private void searchTab3Clicked()
        {
            this.changeSearchTab(2, true);
        }

        private void searchTab4Clicked()
        {
            this.changeSearchTab(3, true);
        }

        private void sendMail()
        {
            RemoteServices.Instance.set_SendMail_UserCallBack(new RemoteServices.SendMail_UserCallBack(this.sendMailCallback));
            RemoteServices.Instance.set_SendSpecialMail_UserCallBack(new RemoteServices.SendSpecialMail_UserCallBack(this.sendSpecialMailCallback));
            this.newMail_iconSendMail.Visible = false;
            foreach (string str in this.recipients)
            {
                this.addNameToRecent(str);
            }
            string body = this.tbMain.Text + this.generateAttachmentsString();
            if (!this.proclamation)
            {
                RemoteServices.Instance.SendMail(this.tbSubject.Text, body, this.recipients.ToArray(), this.sendThreadID, this.sendAsForward);
            }
            else
            {
                RemoteServices.Instance.SendSpecialMail(this.specialType, this.specialArea, this.tbSubject.Text, body);
            }
            this.mailSent = true;
        }

        public void sendMailCallback(SendMail_ReturnType returnData)
        {
            this.newMail_iconSendMail.Visible = true;
            if (returnData.Success)
            {
                this.open(true, true);
            }
            else
            {
                switch (returnData.m_errorCode)
                {
                    case ErrorCodes.ErrorCode.MAIL_SUBJECT_TOO_LONG:
                    case ErrorCodes.ErrorCode.MAIL_NO_SUBJECT:
                    case ErrorCodes.ErrorCode.MAIL_NO_BODY:
                    case ErrorCodes.ErrorCode.MAIL_NO_VALID_RECIPIENTS:
                        MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
                        return;
                }
                MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
                InterfaceMgr.Instance.refreshForMail(false);
            }
        }

        public void sendProclamation(int mailType, int areaID)
        {
            this.openNewMail("", "");
            this.proclamation = true;
            this.specialType = mailType;
            this.specialArea = areaID;
            this.newMail_iconBackground.Visible = false;
            this.tbSubject.Text = "";
            this.tbSubject.Enabled = false;
            this.tbFindInput.Visible = false;
            this.newMail_iconTab1Button.Visible = false;
            this.newMail_iconTab2Button.Visible = false;
            this.newMail_iconTab3Button.Visible = false;
            this.newMail_iconTab4Button.Visible = false;
            switch (mailType)
            {
                case 1:
                    this.tbSubject.Enabled = true;
                    break;

                case 2:
                    this.tbSubject.Text = SK.Text("MailScreen_House_Proclamation", "House Proclamation");
                    this.headerLabel.Text = SK.Text("MailScreen_Send_House_Proclamation", "Send House Proclamation");
                    break;

                case 3:
                    this.tbSubject.Text = "";
                    break;

                case 4:
                    this.tbSubject.Text = SK.Text("MailScreen_Parish_Proclamation", "Parish Proclamation") + " : " + GameEngine.Instance.World.getParishName(areaID);
                    this.headerLabel.Text = SK.Text("MailScreen_Send_Parish_Proclamation", "Send Parish Proclamation");
                    break;

                case 5:
                    this.tbSubject.Text = SK.Text("MailScreen_County_Proclamation", "County Proclamation") + " : " + GameEngine.Instance.World.getCountyName(areaID);
                    this.headerLabel.Text = SK.Text("MailScreen_Send_County_Proclamation", "Send County Proclamation");
                    break;

                case 6:
                    this.tbSubject.Text = SK.Text("MailScreen_Province_Proclamation", "Province Proclamation") + " : " + GameEngine.Instance.World.getProvinceName(areaID);
                    this.headerLabel.Text = SK.Text("MailScreen_Send_Province_Proclamation", "Send Province Proclamation");
                    break;

                case 7:
                    this.tbSubject.Text = SK.Text("MailScreen_Country_Proclamation", "Country Proclamation") + " : " + GameEngine.Instance.World.getCountryName(areaID);
                    this.headerLabel.Text = SK.Text("MailScreen_Send_Country_Proclamation", "Send Country Proclamation");
                    break;
            }
            this.updateSendButton();
        }

        public void sendSpecialMailCallback(SendSpecialMail_ReturnType returnData)
        {
            this.newMail_iconSendMail.Visible = true;
            if (returnData.Success)
            {
                this.open(true, true);
                if (this.specialType == 2)
                {
                    try
                    {
                        FactionData yourFaction = GameEngine.Instance.World.YourFaction;
                        int index = 0;
                        if (yourFaction != null)
                        {
                            index = yourFaction.houseID;
                        }
                        GameEngine.Instance.World.HouseInfo[index].lastProclomationDate = VillageMap.getCurrentServerTime();
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                switch (returnData.m_errorCode)
                {
                    case ErrorCodes.ErrorCode.MAIL_SUBJECT_TOO_LONG:
                    case ErrorCodes.ErrorCode.MAIL_NO_SUBJECT:
                    case ErrorCodes.ErrorCode.MAIL_NO_BODY:
                    case ErrorCodes.ErrorCode.MAIL_NO_VALID_RECIPIENTS:
                        MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
                        return;
                }
                MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
                InterfaceMgr.Instance.refreshForMail(false);
            }
        }

        public static void setFromFaction()
        {
            fromFaction = true;
        }

        private void showMailItem(int lineClicked)
        {
            this.lastLineClicked = lineClicked;
            MailThreadItem[] itemArray = (MailThreadItem[]) this.m_storedThreads[this.selectedMailThreadID];
            MailThreadListItem item = (MailThreadListItem) this.m_storedThreadHeaders[this.selectedMailThreadID];
            if (!itemArray[lineClicked].readStatus)
            {
                itemArray[lineClicked].readStatus = true;
                bool flag = true;
                foreach (MailThreadItem item2 in itemArray)
                {
                    if (!item2.readStatus)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag && (this.m_storedThreadHeaders[this.selectedMailThreadID] != null))
                {
                    item.readStatus = true;
                }
                RemoteServices.Instance.FlagMailRead(itemArray[lineClicked].mailID);
                this.saveMail();
            }
            this.displayThread(this.selectedMailThreadID, false);
            this.mailThreadBody_scrollBar.Value = 0;
            this.mailThreadBody_scrollBar.Max = 0;
            this.mailThread_mailHeaderFromNameLabel.Text = itemArray[lineClicked].otherUser;
            this.mailThread_fromShield.Image = GameEngine.Instance.World.getWorldShield(itemArray[lineClicked].otherUserID, 0x19, 0x1c);
            this.mailThread_fromShield.Visible = this.mailThread_fromShield.Image != null;
            if (((item != null) && item.readOnly) && (item.specialType == 1))
            {
                this.mailThread_mailBodyText.Text = this.languageSplitString(itemArray[lineClicked].body);
                this.mailThread_mailHeaderFromNameLabel.Text = SK.Text("The_Kingdoms_Team", "The Kingdoms Team");
                this.mailThread_fromShield.Visible = false;
            }
            else if (!this.blockedList.Contains(itemArray[lineClicked].otherUser))
            {
                this.mailThread_mailBodyText.Text = this.parseAttachmentString(itemArray[lineClicked].body, true);
            }
            else
            {
                this.mailThread_mailBodyText.Text = "* " + SK.Text("MailBlock_blocked", "Blocked") + " *";
            }
            this.mailThread_mailHeaderDateValueLabel.Text = itemArray[lineClicked].mailTime.ToShortDateString() + " " + itemArray[lineClicked].mailTime.Hour.ToString("00") + ":" + itemArray[lineClicked].mailTime.Minute.ToString("00");
            if (itemArray[lineClicked].otherUser != RemoteServices.Instance.UserName)
            {
                this.mailThread_iconSelectedBlockPoster.Enabled = true;
                this.selectedUserName = itemArray[lineClicked].otherUser;
                if (this.blockedList.Contains(this.selectedUserName))
                {
                    this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailBlock_manage", "Manage Blocked Users");
                }
                else
                {
                    this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
                }
            }
            else
            {
                this.mailThread_iconSelectedBlockPoster.Enabled = false;
                this.selectedUserName = "";
                this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
            }
            if (((itemArray[lineClicked].otherUserID == RemoteServices.Instance.UserID) && !RemoteServices.Instance.Admin) && !RemoteServices.Instance.Moderator)
            {
                this.mailThread_iconSelectedReportMail.Enabled = false;
            }
            else
            {
                this.mailThread_iconSelectedReportMail.Enabled = this.reportButtonAvailable;
            }
        }

        private void tbFindInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void tbFindInput_KeyUp(object sender, KeyEventArgs e)
        {
            this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
            if (this.tbFindInput.Text.Length == 0)
            {
            }
        }

        private void tbFindInput_TextChanged(object sender, EventArgs e)
        {
            this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
            if (this.tbFindInput.Text.Length == 0)
            {
            }
        }

        private void tbMain_TextChanged(object sender, EventArgs e)
        {
            this.flagUpdateSendButton();
        }

        private void tbNewFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void tbNewFolder_TextChanged(object sender, EventArgs e)
        {
            if (this.doesFolderAlreadyExist())
            {
                this.mailList_createFolderOK.Enabled = false;
            }
            else
            {
                this.mailList_createFolderOK.Enabled = this.tbNewFolder.Text.Length > 0;
            }
        }

        private void tbSubject_TextChanged(object sender, EventArgs e)
        {
            this.flagUpdateSendButton();
        }

        private void tbUserFilter_TextChanged(object sender, EventArgs e)
        {
            this.repopulateTable();
        }

        public void update()
        {
            bool scrollUp = GameEngine.scrollUp;
            bool scrollDown = GameEngine.scrollDown;
            if (this.mailListArea.Visible)
            {
                if (scrollUp)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.keyScrollTimer);
                    if (span.TotalMilliseconds > 50.0)
                    {
                        this.keyScrollTimer = DateTime.Now;
                        if ((this.lastMailLineClicked >= 0) && (this.lastMailLineClicked > 0))
                        {
                            if (this.lastMailLineClicked <= this.mailList_scrollBar.Value)
                            {
                                this.mailList_scrollBar.Value--;
                            }
                            this.lastMailLineClicked--;
                            this.mailLineDoubleClick = DateTime.MinValue;
                            this.mailLineClicked(this.lastMailLineClicked, false);
                        }
                    }
                }
                else if (scrollDown)
                {
                    TimeSpan span2 = (TimeSpan) (DateTime.Now - this.keyScrollTimer);
                    if (span2.TotalMilliseconds > 50.0)
                    {
                        this.keyScrollTimer = DateTime.Now;
                        if ((this.lastMailLineClicked >= 0) && (this.lastMailLineClicked < (this.m_preSortedHeaders.Count - 1)))
                        {
                            this.lastMailLineClicked++;
                            if (this.lastMailLineClicked > (this.mailList_scrollBar.Value + 0x1a))
                            {
                                this.mailList_scrollBar.Value++;
                            }
                            this.mailLineDoubleClick = DateTime.MinValue;
                            this.mailLineClicked(this.lastMailLineClicked, false);
                        }
                    }
                }
            }
            if (this.mailThreadArea.Visible)
            {
                if (scrollUp)
                {
                    TimeSpan span3 = (TimeSpan) (DateTime.Now - this.keyScrollTimer);
                    if (span3.TotalMilliseconds > 50.0)
                    {
                        this.keyScrollTimer = DateTime.Now;
                        if ((this.lastMailItemClicked >= 0) && (this.lastMailItemClicked > 0))
                        {
                            if (this.lastMailItemClicked <= this.mailThread_scrollBar.Value)
                            {
                                this.mailThread_scrollBar.Value--;
                            }
                            this.lastMailItemClicked--;
                            this.mailLineDoubleClick = DateTime.MinValue;
                            this.mailItemClicked(this.lastMailItemClicked);
                        }
                    }
                }
                else if (scrollDown)
                {
                    TimeSpan span4 = (TimeSpan) (DateTime.Now - this.keyScrollTimer);
                    if (span4.TotalMilliseconds > 50.0)
                    {
                        this.keyScrollTimer = DateTime.Now;
                        if ((this.lastMailItemClicked >= 0) && (this.lastMailItemClicked < (this.currentThreadLength - 1)))
                        {
                            this.lastMailItemClicked++;
                            if (this.lastMailItemClicked > (this.mailThread_scrollBar.Value + 0x1a))
                            {
                                this.mailThread_scrollBar.Value++;
                            }
                            this.mailLineDoubleClick = DateTime.MinValue;
                            this.mailItemClicked(this.lastMailItemClicked);
                        }
                    }
                }
            }
            if (this.newMailArea.Visible && this.newMail_iconTab1Area.Visible)
            {
                this.updateSearch();
            }
            if ((this.attachmentWindow != null) && this.attachmentWindow.Visible)
            {
                this.attachmentWindow.update();
            }
            if (this.m_flashFolderLine != null)
            {
                if ((this.flashFolderCount % 6) == 0)
                {
                    this.m_flashFolderLine.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                }
                else if ((this.flashFolderCount % 6) == 3)
                {
                    this.m_flashFolderLine.BodyColor = CustomSelfDrawPanel.MailSelectedOverColor;
                }
                this.flashFolderCount++;
                if (this.flashFolderCount == 30)
                {
                    this.m_flashFolderLine = null;
                    this.flashFolderCount = 0;
                    this.updateFolderList();
                }
            }
            if (this.doUpdateSendButton)
            {
                this.updateSendButton();
            }
        }

        public void updateBlockedList(List<string> newList)
        {
            this.blockedList.Clear();
            this.blockedList.AddRange(newList);
            this.blockListChanged = true;
            this.saveBlockedList();
        }

        public void updateFolderList()
        {
            for (int i = 0; i < 0x1b; i++)
            {
                MailFolderLine line = this.getFolderLine(i);
                line.Text.Text = "";
                line.reset();
            }
            int num2 = 0;
            int[] numArray = new int[Math.Max(1, this.m_mailFolders.Count)];
            foreach (MailThreadListItem item in this.m_storedThreadHeaders)
            {
                if (!item.readStatus)
                {
                    if (item.folderID == -1L)
                    {
                        num2++;
                    }
                    else
                    {
                        for (int j = 0; j < this.m_mailFolders.Count; j++)
                        {
                            if (this.m_mailFolders[j].mailFolderID == item.folderID)
                            {
                                numArray[j]++;
                                break;
                            }
                        }
                    }
                }
            }
            MailFolderLine line2 = this.getFolderLine(0);
            line2.Text.Text = SK.Text("MailScreen_Inbox", "Inbox");
            if (num2 > 0)
            {
                CustomSelfDrawPanel.CSDLabel text = line2.Text;
                text.Text = text.Text + " (" + num2.ToString() + ")";
            }
            if (this.selectedFolderID == -1L)
            {
                line2.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_open;
                line2.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                line2.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
            }
            else
            {
                line2.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_closed;
            }
            line2.invalidate();
            int lineID = 1;
            MailFolderItem item2 = null;
            foreach (MailFolderItem item3 in this.m_mailFolders)
            {
                MailFolderLine line3 = this.getFolderLine(lineID);
                line3.Text.Text = item3.title;
                if (numArray[lineID - 1] > 0)
                {
                    CustomSelfDrawPanel.CSDLabel label2 = line3.Text;
                    label2.Text = label2.Text + " (" + numArray[lineID - 1].ToString() + ")";
                }
                if (this.selectedFolderID == item3.mailFolderID)
                {
                    item2 = item3;
                    line3.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_open;
                    line3.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                    line3.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                }
                else
                {
                    line3.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_closed;
                }
                line3.invalidate();
                lineID++;
                if (lineID >= 0x19)
                {
                    break;
                }
            }
            if (!this.m_moveThreadMode)
            {
                MailFolderLine line4 = this.getFolderLine(lineID);
                line4.Text.Text = SK.Text("MailScreen_New_Folder", "New Folder");
                line4.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_plus;
                line4.invalidate();
                lineID++;
                bool flag = true;
                if ((item2 == null) || (this.selectedFolderID < 0L))
                {
                    flag = false;
                }
                else if (item2.title == "Archive")
                {
                    flag = false;
                }
                if (flag)
                {
                    MailFolderLine line5 = this.getFolderLine(lineID);
                    if (line5 != null)
                    {
                        line5.Text.Text = SK.Text("MailScreen_Remove_Folder", "Remove Folder");
                        line5.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_delete;
                        line5.invalidate();
                    }
                }
            }
        }

        private void updateSearch()
        {
            if (this.lastUpdateTime != 0.0)
            {
                double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
                if ((currentMilliseconds - this.lastUpdateTime) > 1000.0)
                {
                    if (this.tbFindInput.Text.Length == 0)
                    {
                        this.lastUpdateTime = 0.0;
                    }
                    else if ((((this.tbFindInput.Text.Length == 1) || (this.tbFindInput.Text.Length == 2)) && ((currentMilliseconds - this.lastUpdateTime) > 2000.0)) || (this.tbFindInput.Text.Length > 2))
                    {
                        this.lastUpdateTime = 0.0;
                        RemoteServices.Instance.set_GetMailUserSearch_UserCallBack(new RemoteServices.GetMailUserSearch_UserCallBack(this.getMailUserSearchCallback));
                        RemoteServices.Instance.GetMailUserSearch(this.tbFindInput.Text);
                    }
                }
            }
        }

        private void updateSendButton()
        {
            this.doUpdateSendButton = false;
            bool flag = false;
            if ((((this.tbMain.Text.Length > 0) && (this.recipients.Count > 0)) && ((this.sendThreadID >= 0L) || (this.tbSubject.Text.Length > 0))) || (this.proclamation && (this.tbMain.Text.Length > 0)))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            if (flag != this.newMail_iconSendMail.Enabled)
            {
                this.newMail_iconSendMail.Enabled = flag;
                this.newMail_iconSendMail.invalidate();
            }
        }

        public bool AggressiveBlocking
        {
            get
            {
                return this.aggressiveBlocking;
            }
            set
            {
                this.aggressiveBlocking = value;
                this.blockListChanged = true;
            }
        }

        private class MailFolderLine : CustomSelfDrawPanel.CSDControl
        {
            private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
            private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
            private Color lineColor = CustomSelfDrawPanel.MailLineColor;
            private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
            private Color overColor = CustomSelfDrawPanel.MailOverColor;
            private bool setupComplete;

            private void mouseLeave()
            {
                this.main.FillColor = this.bodyColor;
            }

            private void mouseOver()
            {
                if (this.label.Text.Length > 0)
                {
                    this.main.FillColor = this.overColor;
                }
            }

            public void reset()
            {
                this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
                this.lineColor = CustomSelfDrawPanel.MailLineColor;
                this.main.FillColor = this.bodyColor;
                this.line.FillColor = this.lineColor;
                this.icon.Image = null;
            }

            public void setup()
            {
                this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
                this.main.FillColor = this.bodyColor;
                this.line.Position = new Point(0, this.Size.Height - 1);
                this.line.Size = new Size(this.Size.Width, 1);
                this.line.FillColor = this.lineColor;
                base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
                this.label.Position = new Point(0x13, 2);
                this.label.Size = new Size(this.Size.Width - 0x13, this.Size.Height - 4);
                this.label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.icon.Position = new Point(2, 0);
                base.addControl(this.main);
                base.addControl(this.line);
                this.main.addControl(this.label);
                this.main.addControl(this.icon);
                this.setupComplete = true;
            }

            public Color BodyColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.bodyColor != value)
                        {
                            this.main.invalidate();
                        }
                        this.main.FillColor = value;
                    }
                    this.bodyColor = value;
                }
            }

            public CustomSelfDrawPanel.CSDImage Icon
            {
                get
                {
                    return this.icon;
                }
            }

            public Color LineColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.lineColor != value)
                        {
                            this.line.invalidate();
                        }
                        this.line.FillColor = value;
                    }
                    this.lineColor = value;
                }
            }

            public Color OverColor
            {
                set
                {
                    this.overColor = value;
                }
            }

            public CustomSelfDrawPanel.CSDLabel Text
            {
                get
                {
                    return this.label;
                }
            }
        }

        private class MailListLine : CustomSelfDrawPanel.CSDControl
        {
            private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
            private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
            private Color lineColor = CustomSelfDrawPanel.MailLineColor;
            private Color lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
            private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
            private Color overColor = CustomSelfDrawPanel.MailOverColor;
            private CustomSelfDrawPanel.CSDLabel senderLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDFill sep1 = new CustomSelfDrawPanel.CSDFill();
            private CustomSelfDrawPanel.CSDFill sep2 = new CustomSelfDrawPanel.CSDFill();
            private CustomSelfDrawPanel.CSDFill sep3 = new CustomSelfDrawPanel.CSDFill();
            private bool setupComplete;
            private CustomSelfDrawPanel.CSDLabel subjectLabel = new CustomSelfDrawPanel.CSDLabel();

            private void mouseLeave()
            {
                this.main.FillColor = this.bodyColor;
                this.line.FillColor = this.lineColor;
                this.sep1.FillColor = this.lineColor;
                this.sep2.FillColor = this.lineColor;
                this.sep3.FillColor = this.lineColor;
            }

            private void mouseOver()
            {
                if (this.subjectLabel.Text.Length > 0)
                {
                    this.main.FillColor = this.overColor;
                    this.line.FillColor = this.lineOverColor;
                    this.sep1.FillColor = this.lineOverColor;
                    this.sep2.FillColor = this.lineOverColor;
                    this.sep3.FillColor = this.lineOverColor;
                }
            }

            public void reset()
            {
                this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
                this.lineColor = CustomSelfDrawPanel.MailLineColor;
                this.overColor = CustomSelfDrawPanel.MailOverColor;
                this.lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
                this.main.FillColor = this.bodyColor;
                this.line.FillColor = this.lineColor;
            }

            public void setup()
            {
                this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
                this.main.FillColor = this.bodyColor;
                this.line.Position = new Point(0, this.Size.Height - 1);
                this.line.Size = new Size(this.Size.Width, 1);
                this.line.FillColor = this.lineColor;
                this.sep1.Position = new Point(0x15, 0);
                this.sep1.Size = new Size(1, this.Size.Height - 1);
                this.sep1.FillColor = this.lineColor;
                this.sep2.Position = new Point(0x106, 0);
                this.sep2.Size = new Size(1, this.Size.Height - 1);
                this.sep2.FillColor = this.lineColor;
                this.sep3.Position = new Point(0x17e, 0);
                this.sep3.Size = new Size(1, this.Size.Height - 1);
                this.sep3.FillColor = this.lineColor;
                this.subjectLabel.Position = new Point(0x2b, 2);
                this.subjectLabel.Size = new Size(0xdb, this.Size.Height - 4);
                this.subjectLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.dateLabel.Position = new Point(0x109, 0);
                this.dateLabel.Size = new Size(0x76, this.Size.Height);
                this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.senderLabel.Position = new Point(0x181, 2);
                this.senderLabel.Size = new Size(this.Size.Width - 0x181, this.Size.Height - 4);
                this.senderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.icon.Position = new Point(0x17, 2);
                base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
                base.addControl(this.main);
                base.addControl(this.line);
                base.addControl(this.sep1);
                base.addControl(this.sep2);
                base.addControl(this.sep3);
                this.main.addControl(this.subjectLabel);
                this.main.addControl(this.dateLabel);
                this.main.addControl(this.senderLabel);
                this.main.addControl(this.icon);
                this.setupComplete = true;
            }

            public Color BodyColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.bodyColor != value)
                        {
                            this.main.invalidate();
                        }
                        this.main.FillColor = value;
                    }
                    this.bodyColor = value;
                }
            }

            public DateTime Date
            {
                set
                {
                    if ((this.Subject.Text.Length > 0) || (this.Sender.Text.Length > 0))
                    {
                        this.dateLabel.Text = value.ToShortDateString() + "  " + value.Hour.ToString("00") + ":" + value.Minute.ToString("00");
                    }
                }
            }

            public CustomSelfDrawPanel.CSDLabel DateLabel
            {
                get
                {
                    return this.dateLabel;
                }
            }

            public CustomSelfDrawPanel.CSDImage Icon
            {
                get
                {
                    return this.icon;
                }
            }

            public Color LineColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.lineColor != value)
                        {
                            this.line.invalidate();
                        }
                        this.line.FillColor = value;
                    }
                    this.lineColor = value;
                }
            }

            public Color LineOverColor
            {
                set
                {
                    this.lineOverColor = value;
                }
            }

            public Color OverColor
            {
                set
                {
                    this.overColor = value;
                }
            }

            public CustomSelfDrawPanel.CSDLabel Sender
            {
                get
                {
                    return this.senderLabel;
                }
            }

            public CustomSelfDrawPanel.CSDLabel Subject
            {
                get
                {
                    return this.subjectLabel;
                }
            }
        }

        public class MailThreadComparer : IComparer<MailThreadItem>
        {
            public int Compare(MailThreadItem x, MailThreadItem y)
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
                return y.mailTime.CompareTo(x.mailTime);
            }
        }

        public class MailThreadHeaderComparer : IComparer<MailThreadListItem>
        {
            public int Compare(MailThreadListItem x, MailThreadListItem y)
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
                return y.mailTime.CompareTo(x.mailTime);
            }
        }

        private class MailThreadLine : CustomSelfDrawPanel.CSDControl
        {
            public CustomSelfDrawPanel.CSDImage attachmentIcon = new CustomSelfDrawPanel.CSDImage();
            public bool attachmentPresent;
            private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
            private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
            private Color lineColor = CustomSelfDrawPanel.MailLineColor;
            private Color lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
            private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
            private Color overColor = CustomSelfDrawPanel.MailOverColor;
            private CustomSelfDrawPanel.CSDLabel senderLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDFill sep1 = new CustomSelfDrawPanel.CSDFill();
            private CustomSelfDrawPanel.CSDFill sep2 = new CustomSelfDrawPanel.CSDFill();
            private bool setupComplete;
            private CustomSelfDrawPanel.CSDLabel subjectLabel = new CustomSelfDrawPanel.CSDLabel();

            private void attachmentClick()
            {
            }

            private void mouseLeave()
            {
                this.main.FillColor = this.bodyColor;
                this.line.FillColor = this.lineColor;
                this.sep1.FillColor = this.lineColor;
                this.sep2.FillColor = this.lineColor;
            }

            private void mouseOver()
            {
                if (this.subjectLabel.Text.Length > 0)
                {
                    this.main.FillColor = this.overColor;
                    this.line.FillColor = this.lineOverColor;
                    this.sep1.FillColor = this.lineOverColor;
                    this.sep2.FillColor = this.lineOverColor;
                }
            }

            public void reset()
            {
                this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
                this.lineColor = CustomSelfDrawPanel.MailLineColor;
                this.overColor = CustomSelfDrawPanel.MailOverColor;
                this.lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
                this.main.FillColor = this.bodyColor;
                this.line.FillColor = this.lineColor;
                this.hasAttachment = false;
            }

            public void setup()
            {
                this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
                this.main.FillColor = this.bodyColor;
                this.line.Position = new Point(0, this.Size.Height - 1);
                this.line.Size = new Size(this.Size.Width, 1);
                this.line.FillColor = this.lineColor;
                this.attachmentIcon.Image = (Image) GFXLibrary.mail2_attach_icon;
                this.attachmentIcon.Size = new Size(this.Size.Height, this.Size.Height);
                this.attachmentIcon.Position = new Point(0, 0);
                this.attachmentIcon.Visible = false;
                this.sep1.Position = new Point(0xf9 + this.Size.Height, 0);
                this.sep1.Size = new Size(1, this.Size.Height - 1);
                this.sep1.FillColor = this.lineColor;
                this.sep2.Position = new Point(0x157 + this.Size.Height, 0);
                this.sep2.Size = new Size(1, this.Size.Height - 1);
                this.sep2.FillColor = this.lineColor;
                this.subjectLabel.Position = new Point(0x17 + this.Size.Height, 2);
                this.subjectLabel.Size = new Size(0xe3, this.Size.Height - 4);
                this.subjectLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.dateLabel.Position = new Point(0xfd + this.Size.Height, 0);
                this.dateLabel.Size = new Size(0x5f, this.Size.Height);
                this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.senderLabel.Position = new Point(0x15b + this.Size.Height, 2);
                this.senderLabel.Size = new Size(this.Size.Width - 0x15b, this.Size.Height - 4);
                this.senderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.icon.Position = new Point(3 + this.Size.Height, 0);
                base.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
                base.addControl(this.main);
                base.addControl(this.line);
                base.addControl(this.attachmentIcon);
                base.addControl(this.sep1);
                base.addControl(this.sep2);
                this.main.addControl(this.subjectLabel);
                this.main.addControl(this.dateLabel);
                this.main.addControl(this.senderLabel);
                this.main.addControl(this.icon);
                this.setupComplete = true;
            }

            public Color BodyColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.bodyColor != value)
                        {
                            this.main.invalidate();
                        }
                        this.main.FillColor = value;
                    }
                    this.bodyColor = value;
                }
            }

            public CustomSelfDrawPanel.CSDLabel BodyText
            {
                get
                {
                    return this.subjectLabel;
                }
            }

            public DateTime Date
            {
                set
                {
                    if ((this.BodyText.Text.Length > 0) || (this.Sender.Text.Length > 0))
                    {
                        this.dateLabel.Text = value.ToShortDateString() + " " + value.Hour.ToString("00") + ":" + value.Minute.ToString("00");
                    }
                }
            }

            public CustomSelfDrawPanel.CSDLabel DateLabel
            {
                get
                {
                    return this.dateLabel;
                }
            }

            public bool hasAttachment
            {
                set
                {
                    this.attachmentPresent = value;
                    this.attachmentIcon.Visible = value;
                }
            }

            public CustomSelfDrawPanel.CSDImage Icon
            {
                get
                {
                    return this.icon;
                }
            }

            public Color LineColor
            {
                set
                {
                    if (this.setupComplete)
                    {
                        if (this.lineColor != value)
                        {
                            this.line.invalidate();
                        }
                        this.line.FillColor = value;
                    }
                    this.lineColor = value;
                }
            }

            public Color LineOverColor
            {
                set
                {
                    this.lineOverColor = value;
                }
            }

            public Color OverColor
            {
                set
                {
                    this.overColor = value;
                }
            }

            public CustomSelfDrawPanel.CSDLabel Sender
            {
                get
                {
                    return this.senderLabel;
                }
            }
        }
    }
}

