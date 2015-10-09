namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class ReportsPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDLabel downloadingLabel = new CustomSelfDrawPanel.CSDLabel();
        private ReportFilterList filters = new ReportFilterList();
        private long[] folderIDList;
        private string[] folderNamesList;
        private CustomSelfDrawPanel.CSDLabel headingLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage iconBar = new CustomSelfDrawPanel.CSDImage();
        private List<long> idListRef;
        private static bool inDownloadReports = false;
        private static DateTime InDownloadReportsTime = DateTime.MinValue;
        private bool initialLoad = true;
        public static ReportsPanel Instance = null;
        private DateTime lastReportOpenedTime = DateTime.MinValue;
        private List<ReportsEntry> lineList = new List<ReportsEntry>();
        private long[] m_moveArray;
        private SparseArray m_storedReportBodies = new SparseArray();
        private SparseArray m_storedReportData = new SparseArray();
        private SparseArray m_storedReportHeaders = new SparseArray();
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private int numToShow = 0x19;
        private int originalScrollPos;
        private IDockableControl popupWindow;
        private int readFilterTypeDownloaded = 3;
        private CustomSelfDrawPanel.CSDButton reportCaptureButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton reportDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private List<ReportPanelEntry> reportEntries = new List<ReportPanelEntry>();
        private CustomSelfDrawPanel.CSDButton reportFilterButton = new CustomSelfDrawPanel.CSDButton();
        private ReportsComparer reportsMainComparer = new ReportsComparer();
        private CustomSelfDrawPanel.CSDArea scrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private bool showForwardedMessagesOnly;
        private bool showParishAttacks = true;
        private bool showReadMessages = true;
        private bool showVillageLost = true;

        public ReportsPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.NoDrawBackground = true;
        }

        public bool areFiltersClear()
        {
            if (!this.filters.attacks)
            {
                return false;
            }
            if (!this.filters.defense)
            {
                return false;
            }
            if (!this.filters.enemyWarnings)
            {
                return false;
            }
            if (!this.filters.reinforcements)
            {
                return false;
            }
            if (!this.filters.scouting)
            {
                return false;
            }
            if (!this.filters.foraging)
            {
                return false;
            }
            if (!this.filters.trade)
            {
                return false;
            }
            if (!this.filters.vassals)
            {
                return false;
            }
            if (!this.filters.religion)
            {
                return false;
            }
            if (!this.filters.research)
            {
                return false;
            }
            if (!this.filters.elections)
            {
                return false;
            }
            if (!this.filters.factions)
            {
                return false;
            }
            if (!this.filters.cards)
            {
                return false;
            }
            if (!this.filters.achievements)
            {
                return false;
            }
            if (!this.filters.buyVillages)
            {
                return false;
            }
            if (!this.filters.quests)
            {
                return false;
            }
            if (!this.filters.spins)
            {
                return false;
            }
            if (!this.showReadMessages)
            {
                return false;
            }
            if (!this.showParishAttacks)
            {
                return false;
            }
            if (this.showForwardedMessagesOnly)
            {
                return false;
            }
            return true;
        }

        public void clearAllReports()
        {
            this.numToShow = 0x19;
            this.originalScrollPos = 0;
            this.scrollBar.Value = 0;
            this.filters = new ReportFilterList();
            this.showReadMessages = true;
            this.showParishAttacks = true;
            this.ShowForwardedMessagesOnly = false;
            if (!this.initialLoad)
            {
                this.saveReports();
            }
            this.initialLoad = true;
            if (this.m_storedReportHeaders != null)
            {
                this.m_storedReportHeaders.Clear();
            }
            if (this.m_storedReportBodies != null)
            {
                this.m_storedReportBodies.Clear();
            }
            if (this.m_storedReportData != null)
            {
                this.m_storedReportData.Clear();
            }
            GenericReportPanelBasic.MailFavourites = null;
            GenericReportPanelBasic.MailUsersHistory = null;
            GenericReportPanelBasic.ForceHistoryRefresh();
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

        public void deleteAllReports()
        {
            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            List<long> list = new List<long>();
            foreach (ReportListItem item in this.m_storedReportHeaders)
            {
                list.Add(Math.Abs(item.reportID));
            }
            this.idListRef = list;
            if (list.Count > 0)
            {
                MyMessageBox.setCustomSounds("ReportDeletePanel_delete_all_Deleting", "");
            }
            else
            {
                MyMessageBox.setCustomSounds("ReportDeletePanel_delete_all_Nothing_To_delete", "");
            }
            switch (MyMessageBox.Show(SK.Text("ReportsPanel_DeleteAllReports", "Delete All Reports?"), SK.Text("ReportsPanel_ConfirmDelete", "Confirm Delete?"), yesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0))
            {
                case DialogResult.No:
                    return;

                case DialogResult.Yes:
                    this.deleteAllReportsTrue();
                    break;
            }
        }

        private void deleteAllReportsTrue()
        {
            if (this.idListRef.Count > 0)
            {
                long[] reportsToDelete = this.idListRef.ToArray();
                RemoteServices.Instance.DeleteReports(reportsToDelete);
                foreach (long num in reportsToDelete)
                {
                    this.m_storedReportHeaders[num] = null;
                }
                this.repopulateTable(0);
            }
        }

        public void deleteMarkedReports()
        {
            List<long> list = new List<long>();
            foreach (ReportsEntry entry in this.lineList)
            {
                if (((entry != null) && (entry.m_entry != null)) && entry.markedImage.Checked)
                {
                    list.Add(Math.Abs(entry.m_entry.item.reportID));
                }
            }
            this.idListRef = list;
            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            if (list.Count > 0)
            {
                MyMessageBox.setCustomSounds("ReportDeletePanel_delete_marked_Deleting", "");
            }
            else
            {
                MyMessageBox.setCustomSounds("ReportDeletePanel_delete_marked_Nothing_To_delete", "");
            }
            DialogResult result = MyMessageBox.Show(SK.Text("ReportsPanel_DeleteMarkedReports", "Delete Marked Reports?"), SK.Text("ReportsPanel_ConfirmDelete", "Confirm Delete?"), yesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0);
            MyMessageBox.resetCustomSounds();
            switch (result)
            {
                case DialogResult.No:
                    return;

                case DialogResult.Yes:
                    this.deleteMarkedReportsTrue();
                    break;
            }
        }

        private void deleteMarkedReportsTrue()
        {
            if (this.idListRef.Count > 0)
            {
                long[] reportsToDelete = this.idListRef.ToArray();
                RemoteServices.Instance.DeleteReports(reportsToDelete);
                foreach (long num in reportsToDelete)
                {
                    this.m_storedReportHeaders[num] = null;
                }
                this.repopulateTable(0);
            }
        }

        public void deleteOrMoveReportsCallback(DeleteReports_ReturnType returnData)
        {
        }

        public void deleteReport(long reportToDelete)
        {
            long[] reportsToDelete = new long[] { reportToDelete };
            RemoteServices.Instance.DeleteReports(reportsToDelete);
            foreach (ReportListItem item in this.m_storedReportHeaders)
            {
                if (Math.Abs(item.reportID) == reportToDelete)
                {
                    this.m_storedReportHeaders[Math.Abs(item.reportID)] = null;
                    break;
                }
            }
            this.repopulateTable(this.readFilterTypeDownloaded);
        }

        public void deleteReportFolder(string folderName, int mode)
        {
            long folderID = this.getReportFolderID(folderName);
            if (folderID >= 0L)
            {
                RemoteServices.Instance.deleteReportFolder(folderID, mode);
                if (mode == 3)
                {
                    foreach (ReportListItem item in this.m_storedReportHeaders)
                    {
                        if (item.folderID == folderID)
                        {
                            item.folderID = -1L;
                        }
                    }
                }
            }
        }

        public void deleteShownReports()
        {
            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            List<long> list = new List<long>();
            foreach (ReportsEntry entry in this.lineList)
            {
                if ((entry != null) && (entry.m_entry != null))
                {
                    list.Add(Math.Abs(entry.m_entry.item.reportID));
                }
            }
            this.idListRef = list;
            if (list.Count > 0)
            {
                MyMessageBox.setCustomSounds("ReportDeletePanel_delete_shown_Deleting", "");
            }
            else
            {
                MyMessageBox.setCustomSounds("ReportDeletePanel_delete_shown_Nothing_To_delete", "");
            }
            switch (MyMessageBox.Show(SK.Text("ReportsPanel_Delete_All_Shown_Reports", "Delete All Shown Reports?"), SK.Text("ReportsPanel_ConfirmDelete", "Confirm Delete?"), yesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0))
            {
                case DialogResult.No:
                    return;

                case DialogResult.Yes:
                    this.deleteShownReportsTrue();
                    break;
            }
        }

        private void deleteShownReportsTrue()
        {
            if (this.idListRef.Count > 0)
            {
                long[] reportsToDelete = this.idListRef.ToArray();
                RemoteServices.Instance.DeleteReports(reportsToDelete);
                foreach (long num in reportsToDelete)
                {
                    this.m_storedReportHeaders[num] = null;
                }
                this.repopulateTable(0);
            }
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

        public long findHighestReportID()
        {
            long num = -1L;
            foreach (ReportListItem item in this.m_storedReportHeaders)
            {
                if (Math.Abs(item.reportID) > num)
                {
                    num = Math.Abs(item.reportID);
                }
            }
            return num;
        }

        public void getReport(ReportPanelEntry entry)
        {
            long reportID = Math.Abs(entry.item.reportID);
            entry.item.readStatus = true;
            if (this.m_storedReportHeaders[reportID] != null)
            {
                ReportListItem item = (ReportListItem) this.m_storedReportHeaders[reportID];
                item.readStatus = true;
                this.m_storedReportHeaders[reportID] = item;
            }
            if (this.m_storedReportBodies[reportID] != null)
            {
                this.showReport((GetReport_ReturnType) this.m_storedReportBodies[reportID]);
            }
            else
            {
                DateTime now = DateTime.Now;
                TimeSpan span = (TimeSpan) (now - this.lastReportOpenedTime);
                if (span.TotalSeconds > 2.0)
                {
                    this.lastReportOpenedTime = now;
                    RemoteServices.Instance.GetReport(reportID);
                }
            }
        }

        public object getReportData(long reportID)
        {
            return this.m_storedReportData[reportID];
        }

        public long getReportFolderID()
        {
            return -1L;
        }

        public long getReportFolderID(string folderName)
        {
            if (this.folderNamesList != null)
            {
                int index = 0;
                foreach (string str in this.folderNamesList)
                {
                    if (str == folderName)
                    {
                        return this.folderIDList[index];
                    }
                    index++;
                }
            }
            return -1L;
        }

        public void init(bool resized)
        {
            if (this.initialLoad)
            {
                this.loadReports();
                this.initialLoad = false;
                inDownloadReports = false;
            }
            Instance = this;
            base.clearControls();
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.headingLabel.Text = SK.Text("Reports_Reports", "Reports");
            this.headingLabel.Color = ARGBColors.White;
            this.headingLabel.DropShadowColor = ARGBColors.Black;
            this.headingLabel.Position = new Point(15, 8);
            this.headingLabel.Size = new Size(830, 0x23);
            this.headingLabel.Font = FontManager.GetFont("Arial", 24f, FontStyle.Regular);
            this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.headingLabel);
            this.downloadingLabel.Text = SK.Text("Reports_Downloading_Reports", "Download Reports....");
            this.downloadingLabel.Color = ARGBColors.White;
            this.downloadingLabel.DropShadowColor = ARGBColors.Black;
            this.downloadingLabel.Position = new Point(15, 60);
            this.downloadingLabel.Size = new Size(830, 30);
            this.downloadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.downloadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.downloadingLabel.Visible = false;
            this.mainBackgroundImage.addControl(this.downloadingLabel);
            this.iconBar.Image = (Image) GFXLibrary.iconband;
            this.iconBar.Position = new Point(0x2f0, 13);
            this.mainBackgroundImage.addControl(this.iconBar);
            this.reportCaptureButton.ImageNorm = (Image) GFXLibrary.icon_capture;
            this.reportCaptureButton.ImageOver = (Image) GFXLibrary.icon_capture_over;
            this.reportCaptureButton.Position = new Point(10, -15);
            this.reportCaptureButton.setClickDelegate(() => InterfaceMgr.Instance.openReportCaptureWindow(0), "ReportsPanel_capture");
            this.reportCaptureButton.CustomTooltipID = 0x5dd;
            this.iconBar.addControl(this.reportCaptureButton);
            if (this.areFiltersClear())
            {
                this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter;
                this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_over;
            }
            else
            {
                this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter_selected;
                this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_selected_over;
            }
            this.reportFilterButton.Position = new Point(0x44, -15);
            this.reportFilterButton.setClickDelegate(() => InterfaceMgr.Instance.openReportCaptureWindow(1), "ReportsPanel_filter");
            this.reportFilterButton.CustomTooltipID = 0x5dc;
            this.iconBar.addControl(this.reportFilterButton);
            this.reportDeleteButton.ImageNorm = (Image) GFXLibrary.icon_trash;
            this.reportDeleteButton.ImageOver = (Image) GFXLibrary.icon_trash_over;
            this.reportDeleteButton.Position = new Point(0x7e, -15);
            this.reportDeleteButton.setClickDelegate(() => InterfaceMgr.Instance.openReportCaptureWindow(2), "ReportsPanel_delete");
            this.reportDeleteButton.CustomTooltipID = 0x5de;
            this.iconBar.addControl(this.reportDeleteButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x15, new Point(base.Width - 0x26, 10));
            int height = base.Height;
            this.scrollArea.Position = new Point(20, 60);
            this.scrollArea.Size = new Size(930, height - 60);
            this.scrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(930, height - 60));
            this.mainBackgroundImage.addControl(this.scrollArea);
            this.mouseWheelOverlay.Position = this.scrollArea.Position;
            this.mouseWheelOverlay.Size = this.scrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            this.originalScrollPos = this.scrollBar.Value;
            this.scrollBar.Visible = false;
            this.scrollBar.Position = new Point(950, 60);
            this.scrollBar.Size = new Size(0x18, height - 60);
            this.mainBackgroundImage.addControl(this.scrollBar);
            this.scrollBar.Value = 0;
            this.scrollBar.Max = 100;
            this.scrollBar.NumVisibleLines = 0x19;
            this.scrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
            if (!resized)
            {
                RemoteServices.Instance.set_GetReportsList_UserCallBack(new RemoteServices.GetReportsList_UserCallBack(this.reportListCallback));
                RemoteServices.Instance.set_GetReport_UserCallBack(new RemoteServices.GetReport_UserCallBack(this.reportCallback));
                RemoteServices.Instance.set_DeleteOrMoveReports_UserCallBack(new RemoteServices.DeleteReports_UserCallBack(this.deleteOrMoveReportsCallback));
                RemoteServices.Instance.set_ManageReportFolders_UserCallBack(new RemoteServices.ManageReportFolders_UserCallBack(this.manageReportFoldersCallback));
                if (inDownloadReports)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - InDownloadReportsTime);
                    if (span.TotalSeconds > 30.0)
                    {
                        inDownloadReports = false;
                    }
                }
                if (!inDownloadReports)
                {
                    inDownloadReports = true;
                    long clientHighest = this.findHighestReportID();
                    RemoteServices.Instance.GetReportsList(this.readFilterTypeDownloaded, clientHighest);
                }
                this.downloadingLabel.Visible = true;
            }
            else
            {
                this.repopulateTable(this.readFilterTypeDownloaded);
                if (this.originalScrollPos > this.scrollBar.Max)
                {
                    this.originalScrollPos = this.scrollBar.Max;
                }
                this.scrollBar.Value = this.originalScrollPos;
                this.scrollBarMoved();
            }
            base.Focus();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "ReportsPanel";
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

        public void loadReports()
        {
            bool flag = false;
            int userID = RemoteServices.Instance.UserID;
            string str = GameEngine.getSettingsPath(false);
            FileStream input = null;
            BinaryReader reader = null;
            try
            {
                input = new FileStream(str + @"\ReportData-" + userID.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat", FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(input);
                SparseArray array = new SparseArray();
                SparseArray array2 = new SparseArray();
                SparseArray array3 = new SparseArray();
                int num2 = reader.ReadInt32();
                if (num2 < 0)
                {
                    flag = true;
                    num2 = reader.ReadInt32();
                }
                for (int i = 0; i < num2; i++)
                {
                    long num4 = reader.ReadInt64();
                    ReportListItem item = new ReportListItem {
                        reportID = num4
                    };
                    num4 = Math.Abs(num4);
                    item.otherUser = reader.ReadString();
                    item.reportAboutUser = reader.ReadString();
                    item.folderID = reader.ReadInt64();
                    item.sourceVillage = reader.ReadInt32();
                    item.targetVillage = reader.ReadInt32();
                    item.reportType = reader.ReadInt16();
                    item.readStatus = reader.ReadBoolean();
                    item.successStatus = reader.ReadBoolean();
                    long ticks = reader.ReadInt64();
                    item.reportTime = new DateTime(ticks);
                    array[num4] = item;
                    if (reader.ReadBoolean())
                    {
                        GetReport_ReturnType type = new GetReport_ReturnType();
                        type.SetAsSucceeded();
                        type.reportID = num4;
                        type.otherUser = reader.ReadString();
                        type.reportAboutUser = reader.ReadString();
                        type.reportAboutUserID = reader.ReadInt32();
                        long num6 = reader.ReadInt64();
                        type.reportTime = new DateTime(num6);
                        type.reportType = reader.ReadInt16();
                        type.successStatus = reader.ReadBoolean();
                        type.snapshotAvailable = reader.ReadBoolean();
                        type.wasAlreadyRead = reader.ReadBoolean();
                        type.genericData1 = reader.ReadInt32();
                        type.attackingVillage = reader.ReadInt32();
                        type.defendingVillage = reader.ReadInt32();
                        type.genericData2 = reader.ReadInt32();
                        type.genericData3 = reader.ReadInt32();
                        type.genericData4 = reader.ReadInt32();
                        type.genericData5 = reader.ReadInt32();
                        type.genericData6 = reader.ReadInt32();
                        type.genericData7 = reader.ReadInt32();
                        type.genericData8 = reader.ReadInt32();
                        type.genericData9 = reader.ReadInt32();
                        type.genericData10 = reader.ReadInt32();
                        type.genericData11 = reader.ReadInt32();
                        type.genericData12 = reader.ReadInt32();
                        type.genericData13 = reader.ReadInt32();
                        type.genericData14 = reader.ReadInt32();
                        type.genericData15 = reader.ReadInt32();
                        type.genericData16 = reader.ReadInt32();
                        type.genericData17 = reader.ReadInt32();
                        type.genericData18 = reader.ReadInt32();
                        type.genericData19 = reader.ReadInt32();
                        type.genericData20 = reader.ReadInt32();
                        type.genericData21 = reader.ReadInt32();
                        type.genericData22 = reader.ReadInt32();
                        type.genericData23 = reader.ReadInt32();
                        type.genericData24 = reader.ReadInt32();
                        type.genericData25 = reader.ReadInt32();
                        type.genericData26 = reader.ReadInt32();
                        type.genericData27 = reader.ReadInt32();
                        type.genericData28 = reader.ReadInt32();
                        type.genericData29 = reader.ReadInt32();
                        type.genericData30 = reader.ReadInt32();
                        type.genericData31 = reader.ReadInt32();
                        type.genericData32 = reader.ReadInt32();
                        type.genericData33 = reader.ReadInt32();
                        type.genericData34 = reader.ReadInt32();
                        type.genericData35 = reader.ReadInt32();
                        array2[num4] = type;
                    }
                    if (reader.ReadBoolean())
                    {
                        ViewBattle_ReturnType type2 = new ViewBattle_ReturnType();
                        int count = reader.ReadInt32();
                        if (count > 0)
                        {
                            type2.castleMapSnapshot = new byte[count];
                            type2.castleMapSnapshot = reader.ReadBytes(count);
                        }
                        int num8 = reader.ReadInt32();
                        if (num8 > 0)
                        {
                            type2.damageMapSnapshot = new byte[num8];
                            type2.damageMapSnapshot = reader.ReadBytes(num8);
                        }
                        int num9 = reader.ReadInt32();
                        if (num9 > 0)
                        {
                            type2.castleTroopsSnapshot = new byte[num9];
                            type2.castleTroopsSnapshot = reader.ReadBytes(num9);
                        }
                        int num10 = reader.ReadInt32();
                        if (num10 > 0)
                        {
                            type2.attackMapSnapshot = new byte[num10];
                            type2.attackMapSnapshot = reader.ReadBytes(num10);
                        }
                        type2.keepLevel = reader.ReadInt32();
                        if (flag)
                        {
                            type2.landType = reader.ReadInt32();
                        }
                        if (reader.ReadBoolean())
                        {
                            type2.defenderResearchData = new CastleResearchData();
                            type2.defenderResearchData.Read(reader);
                        }
                        if (reader.ReadBoolean())
                        {
                            type2.attackerResearchData = new CastleResearchData();
                            type2.attackerResearchData.Read(reader);
                        }
                        array3[num4] = type2;
                    }
                }
                reader.Close();
                input.Close();
                this.m_storedReportHeaders = array;
                this.m_storedReportBodies = array2;
                this.m_storedReportData = array3;
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

        private void manageReportFoldersCallback(ManageReportFolders_ReturnType returnData)
        {
        }

        public void markAsReadAllReports()
        {
            List<long> list = new List<long>();
            foreach (ReportListItem item in this.m_storedReportHeaders)
            {
                if (!item.readStatus)
                {
                    list.Add(Math.Abs(item.reportID));
                }
            }
            if (list.Count > 0)
            {
                long[] reportsToMark = list.ToArray();
                RemoteServices.Instance.MarkReportsRead(reportsToMark);
                foreach (long num in reportsToMark)
                {
                    ReportListItem item2 = (ReportListItem) this.m_storedReportHeaders[num];
                    if (item2 != null)
                    {
                        item2.readStatus = true;
                    }
                }
                this.repopulateTable(0);
            }
        }

        public void markAsReadMarkedReports()
        {
            List<long> list = new List<long>();
            foreach (ReportsEntry entry in this.lineList)
            {
                if (((entry != null) && (entry.m_entry != null)) && entry.markedImage.Checked)
                {
                    list.Add(Math.Abs(entry.m_entry.item.reportID));
                }
            }
            if (list.Count > 0)
            {
                long[] reportsToMark = list.ToArray();
                RemoteServices.Instance.MarkReportsRead(reportsToMark);
                foreach (long num in reportsToMark)
                {
                    ReportListItem item = (ReportListItem) this.m_storedReportHeaders[num];
                    if (item != null)
                    {
                        item.readStatus = true;
                    }
                }
                this.repopulateTable(0);
            }
        }

        public void markAsReadShownReports()
        {
            List<long> list = new List<long>();
            foreach (ReportsEntry entry in this.lineList)
            {
                if ((entry != null) && (entry.m_entry != null))
                {
                    list.Add(Math.Abs(entry.m_entry.item.reportID));
                }
            }
            if (list.Count > 0)
            {
                List<long> list2 = new List<long>();
                foreach (long num in list)
                {
                    ReportListItem item = (ReportListItem) this.m_storedReportHeaders[num];
                    if ((item != null) && !item.readStatus)
                    {
                        list2.Add(num);
                    }
                }
                list = list2;
            }
            if (list.Count > 0)
            {
                long[] reportsToMark = list.ToArray();
                RemoteServices.Instance.MarkReportsRead(reportsToMark);
                foreach (long num2 in reportsToMark)
                {
                    ReportListItem item2 = (ReportListItem) this.m_storedReportHeaders[num2];
                    if (item2 != null)
                    {
                        item2.readStatus = true;
                    }
                }
                this.repopulateTable(0);
            }
        }

        private void mouseWheelMoved(int delta)
        {
            if (delta < 0)
            {
                this.scrollBar.scrollDown(40);
            }
            else if (delta > 0)
            {
                this.scrollBar.scrollUp(40);
            }
        }

        public void moveReports(string destFolderName)
        {
            long folderID = this.getReportFolderID(destFolderName);
            if ((this.m_moveArray != null) && (this.m_moveArray.Length > 0))
            {
                RemoteServices.Instance.MoveReports(this.m_moveArray, folderID);
                foreach (long num2 in this.m_moveArray)
                {
                    if (this.m_storedReportHeaders[num2] != null)
                    {
                        ReportListItem item = (ReportListItem) this.m_storedReportHeaders[num2];
                        item.folderID = folderID;
                        this.m_storedReportHeaders[num2] = item;
                    }
                }
            }
        }

        public bool queryDeleteReport(long reportID)
        {
            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            MyMessageBox.setCustomSounds("Reports_single_delete_clicked", "");
            DialogResult result = MyMessageBox.Show(SK.Text("SendMonksPanel_Delete_This_Report", "Delete this report?"), SK.Text("SendMonksPanel_Delete_Report", "Delete Report"), yesNo);
            MyMessageBox.resetCustomSounds();
            if (result == DialogResult.Yes)
            {
                this.deleteReport(reportID);
                return true;
            }
            return false;
        }

        public void recalcDisplayedGrid()
        {
            int max = this.scrollBar.Value;
            this.reportEntries.Sort(this.reportsMainComparer);
            this.scrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            int count = this.reportEntries.Count;
            int lineID = 0;
            lineID = 0;
            while ((lineID < count) && (lineID < this.numToShow))
            {
                ReportPanelEntry entry = this.reportEntries[lineID];
                ReportListItem item = entry.item;
                int reportType = item.reportType;
                string otherUser = "";
                if (item.otherUser != null)
                {
                    otherUser = item.otherUser;
                }
                string reportAboutUser = "";
                if ((item.reportAboutUser != null) && (item.reportAboutUser.Length > 0))
                {
                    reportAboutUser = item.reportAboutUser;
                }
                else
                {
                    reportAboutUser = RemoteServices.Instance.UserName;
                }
                int targetVillage = item.targetVillage;
                int sourceVillage = item.sourceVillage;
                string text = "";
                string forwardedString = "";
                string str5 = reportAboutUser;
                if (reportAboutUser != RemoteServices.Instance.UserName)
                {
                    forwardedString = SK.Text("Reports_Forwarded_By", "Forwarded by") + " : ";
                }
                bool flag = false;
                bool flag2 = false;
                switch (reportType)
                {
                    case 1:
                    case 0x18:
                    case 0x19:
                    case 0x3a:
                    case 0x3b:
                    case 60:
                    case 0x3d:
                    case 0x7b:
                    case 0x7c:
                    case 0x7d:
                        if ((sourceVillage >= 0) && GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            reportAboutUser = GameEngine.Instance.World.getParishNameFromVillageID(sourceVillage);
                            flag = true;
                        }
                        break;

                    case 3:
                    case 0x3e:
                    case 0x3f:
                    case 0x40:
                    case 0x41:
                    case 0x4f:
                        if ((targetVillage >= 0) && GameEngine.Instance.World.isRegionCapital(targetVillage))
                        {
                            reportAboutUser = GameEngine.Instance.World.getParishNameFromVillageID(targetVillage);
                            flag2 = true;
                        }
                        break;
                }
                switch (reportType)
                {
                    case 1:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage) || !GameEngine.Instance.World.isRegionCapital(targetVillage))
                        {
                            break;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_parish_parish", "Your Parish Attacks Parish : ") + GameEngine.Instance.World.getVillageName(targetVillage);
                        goto Label_1B6F;

                    case 2:
                        if (otherUser.Length != 0)
                        {
                            goto Label_060E;
                        }
                        text = text + SK.Text("ReportsPanel_Parish_Attack_Player", "Your Parish Attacks Player : ");
                        if (targetVillage < 0)
                        {
                            goto Label_05F1;
                        }
                        text = text + GameEngine.Instance.World.getVillageName(targetVillage);
                        goto Label_1B6F;

                    case 3:
                        if (!GameEngine.Instance.World.isRegionCapital(targetVillage))
                        {
                            goto Label_0E70;
                        }
                        text = text + SK.Text("ReportsPanel_Player_Attacks_parish", "Player attacks your Parish : ");
                        goto Label_0F0F;

                    case 4:
                        text = text + SK.Text("ReportsPanel_Parish_Attacks", "Parish attacks your castle : ");
                        if (otherUser.Length != 0)
                        {
                            goto Label_0F83;
                        }
                        text = text + SK.Text("ReportsPanel_Unknown_Player", "An Unknown Player");
                        goto Label_1B6F;

                    case 15:
                        text = text + SK.Text("ReportsPanel_Lost_Vassal", "You have lost a vassal : ") + otherUser;
                        goto Label_1B6F;

                    case 0x10:
                        text = text + SK.Text("ReportsPanel_No_Liege_Lord", "You no longer have a liege lord");
                        goto Label_1B6F;

                    case 0x11:
                        text = text + SK.Text("ReportsPanel_Reinforcements_Arrived", "Reinforcements have arrived from : ") + otherUser;
                        goto Label_1B6F;

                    case 0x12:
                        if (otherUser == "")
                        {
                            otherUser = SK.Text("ReportsPanel_An_Empty_Village", "An empty village");
                        }
                        text = text + SK.Text("ReportsPanel_Reinforcements_Returned", "Reinforcements have returned from : ") + otherUser;
                        goto Label_1B6F;

                    case 0x13:
                        text = text + SK.Text("ReportsPanel_Reinforcements_Retrieved", "Reinforcements have been retrieved by : ") + otherUser;
                        goto Label_1B6F;

                    case 20:
                        text = text + SK.Text("ReportsPanel_Research_Complete", "Research Task Complete");
                        goto Label_1B6F;

                    case 0x15:
                        if (otherUser.Length != 0)
                        {
                            goto Label_1146;
                        }
                        text = text + SK.Text("ReportsPanel_Scouted_Out", "Your Scouts Scout-Out Village : ");
                        if (targetVillage < 0)
                        {
                            goto Label_1129;
                        }
                        text = text + GameEngine.Instance.World.getVillageName(targetVillage);
                        goto Label_1B6F;

                    case 0x16:
                        text = text + SK.Text("ReportsPanel_Scouted", "Player scouts your castle : ");
                        if (otherUser.Length != 0)
                        {
                            goto Label_12CE;
                        }
                        text = text + SK.Text("ReportsPanel_Unknown_Player", "An Unknown Player");
                        goto Label_1B6F;

                    case 0x17:
                        text = text + SK.Text("ReportsPanel_Stash_Foraged", "Stash Foraged");
                        goto Label_1B6F;

                    case 0x18:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_0BB1;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Bandit_parish", "Your Parish Attacks a Bandit Camp");
                        goto Label_1B6F;

                    case 0x19:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_0C8E;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Wolf_Lair_parish", "Your Parish Attacks a Wolf Lair");
                        goto Label_1B6F;

                    case 0x1a:
                        text = text + SK.Text("ReportsPanel_Bandit_Camp_Scouted", "Bandit Camp Scouted");
                        goto Label_1B6F;

                    case 0x1b:
                        text = text + SK.Text("ReportsPanel_Wolf_Lair_Scouted", "Wolf Lair Scouted");
                        goto Label_1B6F;

                    case 0x1c:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_1307;
                        }
                        text = text + SK.Text("ReportsPanel_New_Steward", "New Steward in Parish : ") + otherUser;
                        goto Label_1B6F;

                    case 0x1d:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_161D;
                        }
                        text = text + "Spy Report - Command 'Player 1' of " + otherUser;
                        goto Label_1B6F;

                    case 30:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_1749;
                        }
                        text = text + "Spy Report - Command 'Village 1' of " + otherUser;
                        goto Label_1B6F;

                    case 0x1f:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_1681;
                        }
                        text = text + "Spy Report - Command 'Castle 1' of " + otherUser;
                        goto Label_1B6F;

                    case 0x20:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_17AD;
                        }
                        text = text + "Spy Report - Command 'Army 1' of " + otherUser;
                        goto Label_1B6F;

                    case 0x21:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_177B;
                        }
                        text = text + "Spy Report - Command 'Village 2' of " + otherUser;
                        goto Label_1B6F;

                    case 0x22:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_16B3;
                        }
                        text = text + "Spy Report - Command 'Castle 2' of " + otherUser;
                        goto Label_1B6F;

                    case 0x23:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_164F;
                        }
                        text = text + "Spy Report - Command 'Player 2' of " + otherUser;
                        goto Label_1B6F;

                    case 0x24:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_16E5;
                        }
                        text = text + "Spy Report - Command 'Castle 3' of " + otherUser;
                        goto Label_1B6F;

                    case 0x25:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_17DF;
                        }
                        text = text + "Spy Report - Command 'Army 2' of " + otherUser;
                        goto Label_1B6F;

                    case 0x26:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_1717;
                        }
                        text = text + "Spy Report - Command 'Castle 4' of " + otherUser;
                        goto Label_1B6F;

                    case 0x27:
                        text = text + "Spy Report - Command Failed ";
                        goto Label_1B6F;

                    case 40:
                        text = text + "Spy Report - No Spies Found ";
                        goto Label_1B6F;

                    case 0x2e:
                        text = text + SK.Text("ReportsPanel_Liege_Lord_Offer_FRom", "Offer to be your liege lord from : ") + otherUser;
                        goto Label_1B6F;

                    case 0x2f:
                        text = text + SK.Text("ReportsPanel_Vassal_Request_Accepted", "Vassal Request accepted by : ") + otherUser;
                        goto Label_1B6F;

                    case 0x30:
                        text = text + SK.Text("ReportsPanel_Vassal_request_Declined", "Vassal Request declined by : ") + otherUser;
                        goto Label_1B6F;

                    case 0x31:
                        text = text + SK.Text("ReportsPanel_Liege_Lord_Offer_Withdrawn", "Liege lord offer withdrawn by : ") + otherUser;
                        goto Label_1B6F;

                    case 50:
                        text = text + SK.Text("ReportsPanel_Faction_Invite", "Faction Invite");
                        goto Label_1B6F;

                    case 0x35:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_134D;
                        }
                        text = text + SK.Text("ReportsPanel_New_Sheriff", "New Sheriff in County : ") + otherUser;
                        goto Label_1B6F;

                    case 0x36:
                        text = text + SK.Text("ReportsPanel_Rat_Castle_Scouted", "Rat's Castle Scouted");
                        goto Label_1B6F;

                    case 0x37:
                        text = text + SK.Text("ReportsPanel_Snake_Castle_Scouted", "Snake's Castle Scouted");
                        goto Label_1B6F;

                    case 0x38:
                        text = text + SK.Text("ReportsPanel_Pig_Castle_Scouted", "Pig's Castle Scouted");
                        goto Label_1B6F;

                    case 0x39:
                        text = text + SK.Text("ReportsPanel_Wolf_Castle_Scouted", "Wolf's Castle Scouted");
                        goto Label_1B6F;

                    case 0x3a:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_0666;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Rat_parish", "Your Parish Attacks the Rat");
                        goto Label_1B6F;

                    case 0x3b:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_0743;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Snake_parish", "Your Parish Attacks the Snake");
                        goto Label_1B6F;

                    case 60:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_0820;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Pig_parish", "Your Parish Attacks the Pig");
                        goto Label_1B6F;

                    case 0x3d:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_08FD;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Wolf_parish", "Your Parish Attacks the Wolf");
                        goto Label_1B6F;

                    case 0x3e:
                        text = text + SK.Text("ReportsPanel_Rat_Attacks", "The Rat Attacks");
                        goto Label_1B6F;

                    case 0x3f:
                        text = text + SK.Text("ReportsPanel_Snake_Attacks", "The Snake Attacks");
                        goto Label_1B6F;

                    case 0x40:
                        text = text + SK.Text("ReportsPanel_Pig_Attacks", "The Pig Attacks");
                        goto Label_1B6F;

                    case 0x41:
                        text = text + SK.Text("ReportsPanel_Wolf_Attacks", "The Wolf Attacks");
                        goto Label_1B6F;

                    case 0x42:
                        text = text + SK.Text("ReportsPanel_Monk_Influence", "Monk Influence");
                        goto Label_1B6F;

                    case 0x43:
                        text = text + SK.Text("ReportsPanel_Monk_Restoration", "Monk Restoration");
                        goto Label_1B6F;

                    case 0x44:
                    case 0x69:
                        text = text + SK.Text("ReportsPanel_Monk_Interdiction", "Monk Interdiction");
                        goto Label_1B6F;

                    case 0x45:
                    case 0x68:
                        text = text + SK.Text("ReportsPanel_Monk_Inquisition", "Monk Inquisition");
                        goto Label_1B6F;

                    case 70:
                    case 0x5b:
                        text = text + SK.Text("ReportsPanel_Monk_Excommunication", "Monk Excommunication");
                        goto Label_1B6F;

                    case 0x47:
                    case 0x67:
                        text = text + SK.Text("ReportsPanel_Monk_Absolution", "Monk Absolution");
                        goto Label_1B6F;

                    case 0x48:
                        text = text + SK.Text("ReportsPanel_Monk_Blessing", "Monk Blessing");
                        goto Label_1B6F;

                    case 0x49:
                        text = text + SK.Text("ReportsPanel_Goods_Sent_From", "Goods Sent From : ") + otherUser;
                        goto Label_1B6F;

                    case 0x4a:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_1393;
                        }
                        text = text + SK.Text("ReportsPanel_New_Governor", "New Governor in Province : ") + otherUser;
                        goto Label_1B6F;

                    case 0x4b:
                        if (otherUser.Length <= 0)
                        {
                            goto Label_13D9;
                        }
                        text = text + SK.Text("ReportsPanel_New_King", "New King in Country : ") + otherUser;
                        goto Label_1B6F;

                    case 0x4c:
                        text = text + SK.Text("ReportsPanel_Card_Expires", "Card Expires");
                        goto Label_1B6F;

                    case 0x4d:
                        text = text + SK.Text("ReportsPanel_Instant_Card_Played", "Instant Card Played");
                        goto Label_1B6F;

                    case 0x4e:
                        text = text + SK.Text("ReportsPanel_Auto_Trade_Sent", "Auto Trade Sent");
                        goto Label_1B6F;

                    case 0x4f:
                        text = text + SK.Text("ReportsPanel_Enemy_Attacks", "The Enemy Attacks");
                        goto Label_1B6F;

                    case 80:
                        text = text + SK.Text("ReportsPanel_Enemy_Arrive", "The enemy arrives in our parish!");
                        goto Label_1B6F;

                    case 0x51:
                        text = text + SK.Text("ReportsPanel_Enemy_First", "Enemy probes castle defences");
                        goto Label_1B6F;

                    case 0x52:
                        text = text + SK.Text("ReportsPanel_Enemy_Normal", "Enemy launches attack");
                        goto Label_1B6F;

                    case 0x53:
                        text = text + SK.Text("ReportsPanel_Enemy_Prefinal", "Enemy troops advancing in large numbers");
                        goto Label_1B6F;

                    case 0x54:
                        text = text + SK.Text("ReportsPanel_Enemy_Final", "Enemy launches final attack");
                        goto Label_1B6F;

                    case 0x55:
                        text = text + SK.Text("ReportsPanel_Enemy_Leave", "The enemy is vanquished!");
                        goto Label_1B6F;

                    case 0x56:
                        text = text + SK.Text("ReportsPanel_Diplomacy", "The enemy attack was stopped by Diplomacy");
                        goto Label_1B6F;

                    case 0x57:
                        text = text + SK.Text("ReportsPanel_Rat_Diplomacy", "The Rat's attack was stopped by Diplomacy");
                        goto Label_1B6F;

                    case 0x58:
                        text = text + SK.Text("ReportsPanel_Snake_Diplomacy", "The Snake's attack was stopped by Diplomacy");
                        goto Label_1B6F;

                    case 0x59:
                        text = text + SK.Text("ReportsPanel_Pig_Diplomacy", "The Pig's attack was stopped by Diplomacy");
                        goto Label_1B6F;

                    case 90:
                        text = text + SK.Text("ReportsPanel_Wolf_Diplomacy", "The Wolf's attack was stopped by Diplomacy");
                        goto Label_1B6F;

                    case 0x5c:
                        text = text + SK.Text("ReportsPanel_Achievement_Attained", "Achievement Attained");
                        goto Label_1B6F;

                    case 0x5d:
                        text = text + SK.Text("ReportsPanel_Buy_Village_Success", "Village Charter Purchased");
                        goto Label_1B6F;

                    case 0x5e:
                    case 0x5f:
                    case 0x60:
                        text = text + SK.Text("ReportsPanel_Buy_Village_Failed", "Village Charter Purchase Failed");
                        goto Label_1B6F;

                    case 0x63:
                        text = text + SK.Text("ReportsPanel_Card_Used", "Card Used and Expired");
                        goto Label_1B6F;

                    case 100:
                        text = text + SK.Text("ReportsPanel_QuestCompleted", "Quest Completed");
                        goto Label_1B6F;

                    case 0x65:
                        text = text + SK.Text("ReportsPanel_Quest Failed", "Quest Failed");
                        goto Label_1B6F;

                    case 0x66:
                        text = text + SK.Text("Reports_Spins", "Wheel Spin Prize");
                        goto Label_1B6F;

                    case 0x6a:
                        text = text + SK.Text("ReportsPanel_Monk_Ended", "Monk Interdiction Ended");
                        goto Label_1B6F;

                    case 0x6b:
                        text = text + SK.Text("ReportsPanel_Faction_Member_Join", "New Faction Member") + " : " + otherUser;
                        goto Label_1B6F;

                    case 0x6c:
                        if (!(otherUser == ""))
                        {
                            goto Label_14CE;
                        }
                        text = text + SK.Text("ReportsPanel_Faction_Member_Leave_Self", "You are no longer a member of a faction");
                        goto Label_1B6F;

                    case 0x6d:
                        if (!(otherUser == ""))
                        {
                            goto Label_143E;
                        }
                        text = text + SK.Text("ReportsPanel_Faction_Member_Dismissed_Self", "You have been dismissed from your faction");
                        goto Label_1B6F;

                    case 110:
                        text = text + SK.Text("ReportsPanel_Faction_Promotion", "Faction Promotion");
                        goto Label_1B6F;

                    case 0x6f:
                        text = text + SK.Text("ReportsPanel_Faction_Demotion", "Faction Demotion");
                        goto Label_1B6F;

                    case 0x70:
                        text = text + SK.Text("ReportsPanel_Faction_Relationship_Change", "Faction Relationship Change");
                        goto Label_1B6F;

                    case 0x71:
                        text = text + SK.Text("ReportsPanel_Faction_House_Change", "House Membership Change");
                        goto Label_1B6F;

                    case 0x72:
                        text = text + SK.Text("ReportsPanel_House_Relationship_Change", "House Relationship Change");
                        goto Label_1B6F;

                    case 0x73:
                        text = text + SK.Text("ReportsPanel_Faction_Application", "A Player has applied to your Faction") + " : " + otherUser;
                        goto Label_1B6F;

                    case 0x74:
                        text = text + SK.Text("ReportsPanel_Faction_Application_accepted", "Your faction application has been accepted");
                        goto Label_1B6F;

                    case 0x75:
                        text = text + SK.Text("ReportsPanel_Faction_Application_rejected", "Your faction application has been rejected");
                        goto Label_1B6F;

                    case 0x76:
                        text = text + SK.Text("ReportsPanel_Faction_Member_Dismissed_Self", "You have been dismissed from your faction");
                        goto Label_1B6F;

                    case 120:
                        text = text + SK.Text("ReportsPanel_Faction_Glory_Obtained", "Your house has been awarded glory points");
                        goto Label_1B6F;

                    case 0x79:
                        text = text + SK.Text("ReportsPanel_Paladin_Castle_Scouted", "Paladin's Castle Scouted");
                        goto Label_1B6F;

                    case 0x7a:
                        text = text + SK.Text("ReportsPanel_Paladin_Castle_Scouted", "Paladin's Castle Scouted");
                        goto Label_1B6F;

                    case 0x7b:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_09DA;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Paladin_parish", "Your Parish Attacks the Paladin's Castle");
                        goto Label_1B6F;

                    case 0x7c:
                        if (!GameEngine.Instance.World.isRegionCapital(sourceVillage))
                        {
                            goto Label_0AB7;
                        }
                        text = text + SK.Text("ReportsPanel_Attack_Paladin_parish", "Your Parish Attacks the Paladin's Castle");
                        goto Label_1B6F;

                    case 0x7d:
                        text = text + SK.Text("ReportsPanel_Attack_Treasure_Castle", "Your Troops Attack a Treasure Castle");
                        goto Label_1B6F;

                    case 0x7e:
                        text = text + SK.Text("ReportsPanel_Treasure_Castle_Scouted", "Treasure Castle Scouted");
                        goto Label_1B6F;

                    case 0x7f:
                    case 0x80:
                        text = text + SK.Text("Reports_VillageLost", "Village Lost");
                        goto Label_1B6F;

                    case 0x81:
                        text = text + SK.Text("Reports_AI_Spins", "Wheel Spin Bonus from AI Razing");
                        goto Label_1B6F;

                    case 130:
                        text = text + SK.Text("Reports_Forage_Spins", "Wheel Spin Bonus from Foraging");
                        goto Label_1B6F;

                    case 0x83:
                        text = text + SK.Text("Reports_AI_Spins_capture", "Wheel Spin Bonus from AI Capture");
                        goto Label_1B6F;

                    default:
                        goto Label_1B6F;
                }
                if (otherUser.Length == 0)
                {
                    if (GameEngine.Instance.World.isRegionCapital(sourceVillage))
                    {
                        text = text + SK.Text("ReportsPanel_Attack_Player_parish", "Your Parish Attacks Player : ");
                    }
                    else if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                    {
                        text = text + SK.Text("ReportsPanel_Attack_Player_county", "Your County Attacks Player : ");
                    }
                    else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                    {
                        text = text + SK.Text("ReportsPanel_Attack_Player_province", "Your Province Attacks Player : ");
                    }
                    else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                    {
                        text = text + SK.Text("ReportsPanel_Attack_Player_country", "Your Country Attacks Player : ");
                    }
                    else
                    {
                        text = text + SK.Text("ReportsPanel_Attack_Player", "Your Troops Attack Player : ");
                    }
                    if (targetVillage >= 0)
                    {
                        text = text + GameEngine.Instance.World.getVillageName(targetVillage);
                    }
                    else
                    {
                        text = text + SK.Text("ReportsPanel_An_Empty_Village", "An empty village");
                    }
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Village", "Your Troops Attack Village : ") + otherUser;
                }
                goto Label_1B6F;
            Label_05F1:
                text = text + SK.Text("ReportsPanel_An_Empty_Village", "An empty village");
                goto Label_1B6F;
            Label_060E:
                text = text + SK.Text("ReportsPanel_Parish_Attack_Village", "Your Parish Attacks Village : ") + otherUser;
                goto Label_1B6F;
            Label_0666:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Rat_county", "Your County Attacks the Rat");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Rat_province", "Your Province Attacks the Rat");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Rat_country", "Your Country Attacks the Rat");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Rat", "Your Troops Attack the Rat");
                }
                goto Label_1B6F;
            Label_0743:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Snake_county", "Your County Attacks the Snake");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Snake_province", "Your Province Attacks the Snake");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Snake_country", "Your Country Attacks the Snake");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Snake", "Your Troops Attack the Snake");
                }
                goto Label_1B6F;
            Label_0820:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Pig_county", "Your County Attacks the Pig");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Pig_province", "Your Province Attacks the Pig");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Pig_country", "Your Country Attacks the Pig");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Pig", "Your Troops Attack the Pig");
                }
                goto Label_1B6F;
            Label_08FD:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf_county", "Your County Attacks the Wolf");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf_province", "Your Province Attacks the Wolf");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf_country", "Your Country Attacks the Wolf");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf", "Your Troops Attack the Wolf");
                }
                goto Label_1B6F;
            Label_09DA:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin_county", "Your County Attacks the Paladin's Castle");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin_province", "Your Province Attacks the Paladin's Castle");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin_country", "Your Country Attacks the Paladin's Castle");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin", "Your Troops Attack the Paladin's Castle");
                }
                goto Label_1B6F;
            Label_0AB7:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin_county", "Your County Attacks the Paladin's Castle");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin_province", "Your Province Attacks the Paladin's Castle");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin_country", "Your Country Attacks the Paladin's Castle");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Paladin", "Your Troops Attack the Paladin's Castle");
                }
                goto Label_1B6F;
            Label_0BB1:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Bandit_county", "Your County Attacks a Bandit Camp");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Bandit_province", "Your Province Attacks a Bandit Camp");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Bandit_country", "Your Country Attacks a Bandit Camp");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Bandit", "Your Troops Attack a Bandit Camp");
                }
                goto Label_1B6F;
            Label_0C8E:
                if (GameEngine.Instance.World.isCountyCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf_Lair_county", "Your County Attacks a Wolf Lair");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf_Lair_province", "Your Province Attacks a Wolf Lair");
                }
                else if (GameEngine.Instance.World.isCountryCapital(sourceVillage))
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf_Lair_country", "Your Country Attacks a Wolf Lair");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Attack_Wolf_Lair", "Your Troops Attack a Wolf Lair");
                }
                goto Label_1B6F;
            Label_0E70:
                if (GameEngine.Instance.World.isCountyCapital(targetVillage))
                {
                    text = text + SK.Text("ReportsPanel_Player_Attacks_county", "Player attacks your County : ");
                }
                else if (GameEngine.Instance.World.isProvinceCapital(targetVillage))
                {
                    text = text + SK.Text("ReportsPanel_Player_Attacks_province", "Player attacks your Province : ");
                }
                else if (GameEngine.Instance.World.isCountryCapital(targetVillage))
                {
                    text = text + SK.Text("ReportsPanel_Player_Attacks_country", "Player attacks your Country : ");
                }
                else
                {
                    text = text + SK.Text("ReportsPanel_Player_Attacks", "Player attacks your castle : ");
                }
            Label_0F0F:
                if (otherUser.Length == 0)
                {
                    text = text + SK.Text("ReportsPanel_Unknown_Player", "An Unknown Player");
                }
                else
                {
                    text = text + otherUser;
                }
                goto Label_1B6F;
            Label_0F83:
                text = text + otherUser;
                goto Label_1B6F;
            Label_1129:
                text = text + SK.Text("ReportsPanel_An_Empty_Village", "An empty village");
                goto Label_1B6F;
            Label_1146:
                text = text + SK.Text("ReportsPanel_Scouts", "Your Scouts Scout-Out Player : ") + otherUser;
                goto Label_1B6F;
            Label_12CE:
                text = text + otherUser;
                goto Label_1B6F;
            Label_1307:
                text = text + SK.Text("ReportsPanel_No_Parish_Winner", "No Winner of Parish Election");
                goto Label_1B6F;
            Label_134D:
                text = text + SK.Text("ReportsPanel_No_County_Winner", "No Winner of County Election");
                goto Label_1B6F;
            Label_1393:
                text = text + SK.Text("ReportsPanel_No_Province_Winner", "No Winner of Province Election");
                goto Label_1B6F;
            Label_13D9:
                text = text + SK.Text("ReportsPanel_No_Country_Winner", "No Winner of Country Election");
                goto Label_1B6F;
            Label_143E:
                text = text + SK.Text("ReportsPanel_Faction_Member_Dismissed", "Faction Member Dismissed") + " : " + otherUser;
                goto Label_1B6F;
            Label_14CE:
                text = text + SK.Text("ReportsPanel_Faction_Member_Leave", "Faction Member Leaves") + " : " + otherUser;
                goto Label_1B6F;
            Label_161D:
                text = text + "Spy Report - Command 'Player 1'";
                goto Label_1B6F;
            Label_164F:
                text = text + "Spy Report - Command 'Player 2'";
                goto Label_1B6F;
            Label_1681:
                text = text + "Spy Report - Command 'Castle 1'";
                goto Label_1B6F;
            Label_16B3:
                text = text + "Spy Report - Command 'Castle 2'";
                goto Label_1B6F;
            Label_16E5:
                text = text + "Spy Report - Command 'Castle 3'";
                goto Label_1B6F;
            Label_1717:
                text = text + "Spy Report - Command 'Castle 4'";
                goto Label_1B6F;
            Label_1749:
                text = text + "Spy Report - Command 'Village 1'";
                goto Label_1B6F;
            Label_177B:
                text = text + "Spy Report - Command 'Village 2'";
                goto Label_1B6F;
            Label_17AD:
                text = text + "Spy Report - Command 'Army 1'";
                goto Label_1B6F;
            Label_17DF:
                text = text + "Spy Report - Command 'Army 2'";
            Label_1B6F:
                if (((reportAboutUser != RemoteServices.Instance.UserName) && !flag) && !flag2)
                {
                    forwardedString = forwardedString + reportAboutUser;
                }
                else if ((flag || flag2) && (forwardedString.Length > 0))
                {
                    forwardedString = forwardedString + str5;
                }
                ReportsEntry control = new ReportsEntry();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(entry, text, forwardedString, lineID, this);
                this.scrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
                lineID++;
            }
            if (count > this.numToShow)
            {
                ReportsEntry entry3 = new ReportsEntry();
                if (y != 0)
                {
                    y += 5;
                }
                entry3.Position = new Point(0, y);
                entry3.init(null, SK.Text("ReportsPanel_Show_More_Reports", "Show More Reports"), "", lineID, this);
                this.scrollArea.addControl(entry3);
                y += entry3.Height;
                this.lineList.Add(entry3);
            }
            this.scrollArea.Size = new Size(this.scrollArea.Width, y);
            if (y < this.scrollBar.Height)
            {
                this.scrollBar.Visible = false;
                this.scrollBar.Max = 0;
            }
            else
            {
                this.scrollBar.Visible = true;
                this.scrollBar.NumVisibleLines = this.scrollBar.Height;
                this.scrollBar.Max = y - this.scrollBar.Height;
            }
            if (max > this.scrollBar.Max)
            {
                max = this.scrollBar.Max;
            }
            this.scrollBar.Value = max;
            this.scrollBarMoved();
            this.scrollArea.invalidate();
            this.scrollBar.invalidate();
            base.Invalidate();
        }

        public void repopulateTable(int readFilter)
        {
            DateTime time = VillageMap.getCurrentServerTime();
            this.getReportFolderID();
            this.reportEntries.Clear();
            foreach (ReportListItem item in this.m_storedReportHeaders)
            {
                // TODO: что-то странное с этим временем
                // DateTime time1 = time - item.reportTime;
                if ((!this.showReadMessages && item.readStatus) || (this.showForwardedMessagesOnly && ((item.reportAboutUser == null) || (item.reportAboutUser.Length == 0))))
                {
                    continue;
                }
                bool flag = false;
                switch (item.reportType)
                {
                    case 1:
                    case 2:
                    case 0x18:
                    case 0x19:
                    case 0x3a:
                    case 0x3b:
                    case 60:
                    case 0x3d:
                    case 0x7b:
                    case 0x7c:
                    case 0x7d:
                        if (!GameEngine.Instance.World.isCapital(item.sourceVillage))
                        {
                            break;
                        }
                        if (!this.ShowParishAttacks)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 3:
                    case 4:
                    case 0x3e:
                    case 0x3f:
                    case 0x40:
                    case 0x41:
                    case 0x4f:
                    case 0x56:
                    case 0x57:
                    case 0x58:
                    case 0x59:
                    case 90:
                        if (!this.filters.defense)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        flag = true;
                        goto Label_0453;

                    case 13:
                    case 14:
                    case 15:
                    case 0x10:
                    case 0x2e:
                    case 0x2f:
                    case 0x30:
                    case 0x31:
                        if (!this.filters.vassals)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x11:
                    case 0x12:
                    case 0x13:
                        if (!this.filters.reinforcements)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 20:
                        if (!this.filters.research)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x15:
                    case 0x16:
                    case 0x1a:
                    case 0x1b:
                    case 0x36:
                    case 0x37:
                    case 0x38:
                    case 0x39:
                    case 0x79:
                    case 0x7a:
                    case 0x7e:
                        if (!this.filters.scouting)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x17:
                        if (!this.filters.foraging)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x1c:
                    case 0x33:
                    case 0x34:
                    case 0x35:
                    case 0x4a:
                    case 0x4b:
                        if (!this.filters.elections)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x1d:
                    case 30:
                    case 0x1f:
                    case 0x20:
                    case 0x21:
                    case 0x22:
                    case 0x23:
                    case 0x24:
                    case 0x25:
                    case 0x26:
                    case 0x27:
                    case 40:
                        flag = true;
                        goto Label_0453;

                    case 50:
                    case 0x6b:
                    case 0x6c:
                    case 0x6d:
                    case 110:
                    case 0x6f:
                    case 0x70:
                    case 0x73:
                    case 0x74:
                    case 0x75:
                    case 0x76:
                        if (!this.filters.factions)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x42:
                    case 0x43:
                    case 0x44:
                    case 0x45:
                    case 70:
                    case 0x47:
                    case 0x48:
                    case 0x5b:
                    case 0x67:
                    case 0x68:
                    case 0x69:
                    case 0x6a:
                        if (!this.filters.religion)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x49:
                    case 0x4e:
                        if (!this.filters.trade)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x4c:
                    case 0x4d:
                    case 0x63:
                        if (!this.filters.cards)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 80:
                    case 0x51:
                    case 0x52:
                    case 0x53:
                    case 0x54:
                    case 0x55:
                        if (!this.filters.enemyWarnings)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x5c:
                        if (!this.filters.achievements)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x5d:
                    case 0x5e:
                    case 0x5f:
                    case 0x60:
                        if (!this.filters.buyVillages)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 100:
                    case 0x65:
                        if (!this.filters.quests)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x66:
                    case 0x81:
                    case 130:
                    case 0x83:
                        if (!this.filters.spins)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x71:
                    case 0x72:
                    case 120:
                        if (!this.filters.house)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    case 0x7f:
                    case 0x80:
                        if (!this.ShowVillageLost)
                        {
                            flag = true;
                        }
                        goto Label_0453;

                    default:
                        goto Label_0453;
                }
                if (!this.filters.attacks)
                {
                    flag = true;
                }
            Label_0453:
                if (!flag)
                {
                    ReportPanelEntry entry = new ReportPanelEntry {
                        item = item
                    };
                    this.reportEntries.Add(entry);
                }
            }
            this.recalcDisplayedGrid();
        }

        public void reportCallback(GetReport_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.m_storedReportBodies[returnData.reportID] = returnData;
                this.showReport(returnData);
            }
            else if (returnData.m_errorCode == ErrorCodes.ErrorCode.REPORTS_NO_REPORT)
            {
                MyMessageBox.Show(SK.Text("ReportsPanel_Been_Deleted", "This Report has been deleted"), SK.Text("ReportsPanel_Report_Error", "Report Error"));
            }
        }

        public void reportListCallback(GetReportsList_ReturnType returnData)
        {
            this.downloadingLabel.Visible = false;
            inDownloadReports = false;
            if (returnData.Success)
            {
                int count = returnData.reports.Count;
                for (int i = 0; i < count; i++)
                {
                    if (this.m_storedReportHeaders[Math.Abs(returnData.reports[i].reportID)] == null)
                    {
                        this.m_storedReportHeaders[Math.Abs(returnData.reports[i].reportID)] = returnData.reports[i];
                    }
                }
                this.repopulateTable(returnData.readFilter);
                if (this.originalScrollPos > this.scrollBar.Max)
                {
                    this.originalScrollPos = this.scrollBar.Max;
                }
                this.scrollBar.Value = this.originalScrollPos;
                this.scrollBarMoved();
            }
        }

        public void saveReports()
        {
            int userID = RemoteServices.Instance.UserID;
            string str = GameEngine.getSettingsPath(true);
            try
            {
                FileInfo info = new FileInfo(str + @"\ReportData-" + userID.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat") {
                    IsReadOnly = false
                };
            }
            catch (Exception)
            {
            }
            FileStream output = null;
            BinaryWriter w = null;
            try
            {
                output = new FileStream(str + @"\ReportData-" + userID.ToString() + "-" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + ".dat", FileMode.Create);
                w = new BinaryWriter(output);
                int num2 = -1;
                w.Write(num2);
                int count = this.m_storedReportHeaders.Count;
                w.Write(count);
                foreach (ReportListItem item in this.m_storedReportHeaders)
                {
                    long num4 = Math.Abs(item.reportID);
                    w.Write(item.reportID);
                    w.Write(item.otherUser);
                    w.Write(item.reportAboutUser);
                    w.Write(item.folderID);
                    w.Write(item.sourceVillage);
                    w.Write(item.targetVillage);
                    w.Write(item.reportType);
                    w.Write(item.readStatus);
                    w.Write(item.successStatus);
                    long ticks = item.reportTime.Ticks;
                    w.Write(ticks);
                    if (this.m_storedReportBodies[num4] == null)
                    {
                        w.Write(false);
                    }
                    else
                    {
                        w.Write(true);
                        GetReport_ReturnType type = (GetReport_ReturnType) this.m_storedReportBodies[num4];
                        w.Write(type.otherUser);
                        w.Write(type.reportAboutUser);
                        w.Write(type.reportAboutUserID);
                        long num6 = type.reportTime.Ticks;
                        w.Write(num6);
                        w.Write(type.reportType);
                        w.Write(type.successStatus);
                        w.Write(type.snapshotAvailable);
                        w.Write(type.wasAlreadyRead);
                        w.Write(type.genericData1);
                        w.Write(type.attackingVillage);
                        w.Write(type.defendingVillage);
                        w.Write(type.genericData2);
                        w.Write(type.genericData3);
                        w.Write(type.genericData4);
                        w.Write(type.genericData5);
                        w.Write(type.genericData6);
                        w.Write(type.genericData7);
                        w.Write(type.genericData8);
                        w.Write(type.genericData9);
                        w.Write(type.genericData10);
                        w.Write(type.genericData11);
                        w.Write(type.genericData12);
                        w.Write(type.genericData13);
                        w.Write(type.genericData14);
                        w.Write(type.genericData15);
                        w.Write(type.genericData16);
                        w.Write(type.genericData17);
                        w.Write(type.genericData18);
                        w.Write(type.genericData19);
                        w.Write(type.genericData20);
                        w.Write(type.genericData21);
                        w.Write(type.genericData22);
                        w.Write(type.genericData23);
                        w.Write(type.genericData24);
                        w.Write(type.genericData25);
                        w.Write(type.genericData26);
                        w.Write(type.genericData27);
                        w.Write(type.genericData28);
                        w.Write(type.genericData29);
                        w.Write(type.genericData30);
                        w.Write(type.genericData31);
                        w.Write(type.genericData32);
                        w.Write(type.genericData33);
                        w.Write(type.genericData34);
                        w.Write(type.genericData35);
                    }
                    if (this.m_storedReportData[num4] == null)
                    {
                        w.Write(false);
                    }
                    else
                    {
                        w.Write(true);
                        ViewBattle_ReturnType type2 = (ViewBattle_ReturnType) this.m_storedReportData[num4];
                        if (type2.castleMapSnapshot == null)
                        {
                            w.Write(0);
                        }
                        else
                        {
                            w.Write(type2.castleMapSnapshot.Length);
                            w.Write(type2.castleMapSnapshot, 0, type2.castleMapSnapshot.Length);
                        }
                        if (type2.damageMapSnapshot == null)
                        {
                            w.Write(0);
                        }
                        else
                        {
                            w.Write(type2.damageMapSnapshot.Length);
                            w.Write(type2.damageMapSnapshot, 0, type2.damageMapSnapshot.Length);
                        }
                        if (type2.castleTroopsSnapshot == null)
                        {
                            w.Write(0);
                        }
                        else
                        {
                            w.Write(type2.castleTroopsSnapshot.Length);
                            w.Write(type2.castleTroopsSnapshot, 0, type2.castleTroopsSnapshot.Length);
                        }
                        if (type2.attackMapSnapshot == null)
                        {
                            w.Write(0);
                        }
                        else
                        {
                            w.Write(type2.attackMapSnapshot.Length);
                            w.Write(type2.attackMapSnapshot, 0, type2.attackMapSnapshot.Length);
                        }
                        w.Write(type2.keepLevel);
                        w.Write(type2.landType);
                        if (type2.defenderResearchData == null)
                        {
                            w.Write(false);
                        }
                        else
                        {
                            w.Write(true);
                            type2.defenderResearchData.Write(w);
                        }
                        if (type2.attackerResearchData == null)
                        {
                            w.Write(false);
                        }
                        else
                        {
                            w.Write(true);
                            type2.attackerResearchData.Write(w);
                        }
                    }
                }
                w.Close();
                output.Close();
            }
            catch (Exception exception)
            {
                try
                {
                    if (w != null)
                    {
                        w.Close();
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
                MyMessageBox.Show("A problem occurred saving 'ReportData.data'\n\n" + exception.ToString(), "Data Save Error");
            }
        }

        private void scrollBarMoved()
        {
            int y = this.scrollBar.Value;
            this.scrollArea.Position = new Point(this.scrollArea.X, 60 - y);
            this.scrollArea.ClipRect = new Rectangle(this.scrollArea.ClipRect.X, y, this.scrollArea.ClipRect.Width, this.scrollArea.ClipRect.Height);
            this.scrollArea.invalidate();
            this.scrollBar.invalidate();
        }

        public void setReportAlreadyRead(long reportID)
        {
            ((GetReport_ReturnType) this.m_storedReportBodies[reportID]).wasAlreadyRead = true;
        }

        public void setReportData(object reportData, long reportID)
        {
            this.m_storedReportData[reportID] = reportData;
        }

        public void showMoreReports()
        {
            if (this.numToShow < 100)
            {
                this.numToShow += 50;
            }
            else if (this.numToShow < 300)
            {
                this.numToShow += 100;
            }
            else if (this.numToShow < 0x3e8)
            {
                this.numToShow += 200;
            }
            else
            {
                this.numToShow += 500;
            }
            this.repopulateTable(0);
        }

        public void showReport(GetReport_ReturnType returnData)
        {
            if ((this.popupWindow != null) && this.popupWindow.isVisible())
            {
                this.popupWindow.closeControl(true);
                this.popupWindow = null;
            }
            GenericReportPanelBasic contentPanel = null;
            switch (returnData.reportType)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 0x18:
                case 0x19:
                case 0x3a:
                case 0x3b:
                case 60:
                case 0x3d:
                case 0x3e:
                case 0x3f:
                case 0x40:
                case 0x41:
                case 0x4f:
                case 0x7b:
                case 0x7c:
                case 0x7d:
                    contentPanel = new AttackReportPanelDerived();
                    break;

                case 15:
                case 0x10:
                case 0x2e:
                case 0x2f:
                case 0x30:
                case 0x31:
                    contentPanel = new VassalReportPanelDerived();
                    break;

                case 0x11:
                case 0x12:
                case 0x13:
                    contentPanel = new ReinforcementsReportPanelDerived();
                    break;

                case 20:
                    contentPanel = new ResearchCompleteReportPanelDerived();
                    break;

                case 0x15:
                case 0x16:
                case 0x17:
                case 0x1a:
                case 0x1b:
                case 0x1f:
                case 0x20:
                case 0x22:
                case 0x24:
                case 0x26:
                case 0x27:
                case 40:
                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                case 0x79:
                case 0x7a:
                case 0x7e:
                    contentPanel = new ScoutReportPanelDerived();
                    break;

                case 0x1c:
                case 0x35:
                case 0x4a:
                case 0x4b:
                    contentPanel = new ParishElectionReportPanelDerived();
                    break;

                case 50:
                case 0x6b:
                case 0x6c:
                case 0x6d:
                case 110:
                case 0x6f:
                case 0x70:
                case 0x71:
                case 0x72:
                case 0x73:
                case 0x74:
                case 0x75:
                case 0x76:
                case 120:
                    contentPanel = new FactionReportPanelDerived();
                    break;

                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 70:
                case 0x47:
                case 0x48:
                case 0x5b:
                case 0x67:
                case 0x68:
                case 0x69:
                case 0x6a:
                    contentPanel = new ReligiousReportPanelDerived();
                    break;

                case 0x49:
                case 0x4e:
                    contentPanel = new TradeReportPanelDerived();
                    break;

                case 0x4c:
                case 0x4d:
                case 0x63:
                    contentPanel = new CardExpiryReportPanelDerived();
                    break;

                case 80:
                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x59:
                case 90:
                    contentPanel = new EnemyAttackWarningReportPanelDerived();
                    break;

                case 0x5c:
                    contentPanel = new AchievementReportPanelDerived();
                    break;

                case 0x5d:
                case 0x5e:
                case 0x5f:
                case 0x60:
                    contentPanel = new VillageCharterReportPanelDerived();
                    break;

                case 100:
                case 0x65:
                    contentPanel = new QuestReportPanelDerived();
                    break;

                case 0x66:
                case 0x81:
                case 130:
                case 0x83:
                    contentPanel = new QuestReportPanelDerived();
                    break;

                case 0x7f:
                case 0x80:
                    contentPanel = new VillageLostReportPanelDerived();
                    break;
            }
            if (contentPanel != null)
            {
                GenericReportPopup popup = new GenericReportPopup(contentPanel);
                popup.initProperties(false, SK.Text("ReportsPanel_Report", "Report"), null);
                popup.setData(returnData);
                popup.display(true, null, 0, 0);
                this.popupWindow = popup;
            }
        }

        public void update()
        {
        }

        public void updateFilters()
        {
            this.repopulateTable(this.readFilterTypeDownloaded);
            if (this.areFiltersClear())
            {
                this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter;
                this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_over;
            }
            else
            {
                this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter_selected;
                this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_selected_over;
            }
        }

        public ReportFilterList Filters
        {
            get
            {
                return this.filters;
            }
        }

        public bool ShowForwardedMessagesOnly
        {
            get
            {
                return this.showForwardedMessagesOnly;
            }
            set
            {
                this.showForwardedMessagesOnly = value;
            }
        }

        public bool ShowParishAttacks
        {
            get
            {
                return this.showParishAttacks;
            }
            set
            {
                this.showParishAttacks = value;
            }
        }

        public bool ShowReadMessages
        {
            get
            {
                return this.showReadMessages;
            }
            set
            {
                this.showReadMessages = value;
            }
        }

        public bool ShowVillageLost
        {
            get
            {
                return this.showVillageLost;
            }
            set
            {
                this.showVillageLost = value;
            }
        }

        public class ReportPanelEntry
        {
            public ReportListItem item;
        }

        public class ReportsComparer : IComparer<ReportsPanel.ReportPanelEntry>
        {
            public int Compare(ReportsPanel.ReportPanelEntry x, ReportsPanel.ReportPanelEntry y)
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
                if (Math.Abs(x.item.reportID) < Math.Abs(y.item.reportID))
                {
                    return 1;
                }
                if (Math.Abs(x.item.reportID) > Math.Abs(y.item.reportID))
                {
                    return -1;
                }
                return 0;
            }
        }

        public class ReportsEntry : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel descriptionLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel forwardedLabel = new CustomSelfDrawPanel.CSDLabel();
            public ReportsPanel.ReportPanelEntry m_entry;
            private ReportsPanel m_parent;
            public CustomSelfDrawPanel.CSDCheckBox markedImage = new CustomSelfDrawPanel.CSDCheckBox();
            private CustomSelfDrawPanel.CSDImage symbolImage = new CustomSelfDrawPanel.CSDImage();

            public void init(ReportsPanel.ReportPanelEntry entry, string text, string forwardedString, int lineID, ReportsPanel parent)
            {
                int num = -1;
                this.m_entry = entry;
                this.m_parent = parent;
                this.ClipVisible = true;
                this.clearControls();
                if (forwardedString.Length == 0)
                {
                    if ((lineID & 1) == 0)
                    {
                        this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                    }
                    else
                    {
                        this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                    }
                }
                else if ((lineID & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_01_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_01_dark;
                }
                this.backgroundImage.Position = new Point(0, 0);
                this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseRollOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseRollLeave));
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                if (entry != null)
                {
                    text = text + "   |";
                }
                this.descriptionLabel.Text = text;
                this.descriptionLabel.Color = ARGBColors.Black;
                this.descriptionLabel.Position = new Point(0x55, 1);
                this.descriptionLabel.Size = new Size(830, this.backgroundImage.Height);
                if (entry != null)
                {
                    if (entry.item.readStatus)
                    {
                        this.descriptionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                    }
                    else
                    {
                        this.descriptionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    }
                }
                else
                {
                    this.descriptionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    num = 0;
                }
                this.descriptionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.descriptionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.addControl(this.descriptionLabel);
                if (entry == null)
                {
                    goto Label_08F3;
                }
                Graphics graphics = parent.CreateGraphics();
                Size size = graphics.MeasureString(text, this.descriptionLabel.Font, 830).ToSize();
                graphics.Dispose();
                DateTime time = VillageMap.getCurrentServerTime();
                DateTime reportTime = entry.item.reportTime;
                TimeSpan span = (TimeSpan) (time - reportTime);
                string str = "";
                if (Program.mySettings.LanguageIdent == "de")
                {
                    str = "vor ";
                }
                if (span.TotalMinutes < 1.0)
                {
                    int totalSeconds = (int) span.TotalSeconds;
                    if (totalSeconds <= 0)
                    {
                        totalSeconds = 1;
                    }
                    if (totalSeconds != 1)
                    {
                        this.dateLabel.Text = str + totalSeconds.ToString() + " " + SK.Text("ReportsPanel_seconds_ago", "seconds ago");
                    }
                    else
                    {
                        this.dateLabel.Text = str + totalSeconds.ToString() + " " + SK.Text("ReportsPanel_second_ago", "second ago");
                    }
                }
                else if (span.TotalHours < 1.0)
                {
                    int totalMinutes = (int) span.TotalMinutes;
                    if (totalMinutes <= 0)
                    {
                        totalMinutes = 1;
                    }
                    if (totalMinutes != 1)
                    {
                        this.dateLabel.Text = str + totalMinutes.ToString() + " " + SK.Text("ReportsPanel_minutes_ago", "minutes ago");
                    }
                    else
                    {
                        this.dateLabel.Text = str + totalMinutes.ToString() + " " + SK.Text("ReportsPanel_minute_ago", "minute ago");
                    }
                }
                else if (span.TotalHours < 24.0)
                {
                    int totalHours = (int) span.TotalHours;
                    if (totalHours <= 0)
                    {
                        totalHours = 1;
                    }
                    if (totalHours != 1)
                    {
                        this.dateLabel.Text = str + totalHours.ToString() + " " + SK.Text("ReportsPanel_hours_ago", "hours ago");
                    }
                    else
                    {
                        this.dateLabel.Text = str + totalHours.ToString() + " " + SK.Text("ReportsPanel_hour_ago", "hour ago");
                    }
                }
                else
                {
                    int totalDays = (int) span.TotalDays;
                    if (totalDays <= 0)
                    {
                        totalDays = 1;
                    }
                    if (totalDays != 1)
                    {
                        this.dateLabel.Text = str + totalDays.ToString() + " " + SK.Text("ReportsPanel_days_ago", "days ago");
                    }
                    else
                    {
                        this.dateLabel.Text = str + totalDays.ToString() + " " + SK.Text("ReportsPanel_day_ago", "day ago");
                    }
                }
                switch (entry.item.reportType)
                {
                    case 1:
                    case 2:
                    case 0x18:
                    case 0x19:
                    case 0x3a:
                    case 0x3b:
                    case 60:
                    case 0x3d:
                    case 0x7b:
                    case 0x7c:
                    case 0x7d:
                        if (entry.item.readStatus)
                        {
                            if (entry.item.successStatus)
                            {
                                num = 2;
                            }
                            else
                            {
                                num = 3;
                            }
                            break;
                        }
                        num = 1;
                        break;

                    case 3:
                    case 4:
                    case 0x3e:
                    case 0x3f:
                    case 0x40:
                    case 0x41:
                    case 0x4f:
                    case 0x56:
                    case 0x57:
                    case 0x58:
                    case 0x59:
                    case 90:
                        if (entry.item.readStatus)
                        {
                            if (entry.item.successStatus)
                            {
                                num = 5;
                            }
                            else
                            {
                                num = 6;
                            }
                        }
                        else
                        {
                            num = 4;
                        }
                        if (entry.item.reportID < 0L)
                        {
                            num += 30;
                        }
                        goto Label_0778;

                    case 13:
                    case 14:
                    case 15:
                    case 0x10:
                    case 0x2e:
                    case 0x2f:
                    case 0x30:
                    case 0x31:
                        num = 7;
                        goto Label_0778;

                    case 0x11:
                    case 0x12:
                    case 0x13:
                        num = 8;
                        goto Label_0778;

                    case 20:
                        num = 9;
                        goto Label_0778;

                    case 0x15:
                    case 0x16:
                    case 0x1a:
                    case 0x1b:
                    case 0x36:
                    case 0x37:
                    case 0x38:
                    case 0x39:
                    case 0x79:
                    case 0x7a:
                    case 0x7e:
                        num = 10;
                        goto Label_0778;

                    case 0x17:
                        num = 11;
                        goto Label_0778;

                    case 0x1c:
                    case 0x33:
                    case 0x34:
                    case 0x35:
                    case 0x4a:
                    case 0x4b:
                        num = 12;
                        goto Label_0778;

                    case 50:
                    case 0x6b:
                    case 0x6c:
                    case 0x6d:
                    case 110:
                    case 0x6f:
                    case 0x70:
                    case 0x71:
                    case 0x72:
                    case 0x73:
                    case 0x74:
                    case 0x75:
                    case 0x76:
                    case 120:
                        num = 13;
                        goto Label_0778;

                    case 0x42:
                    case 0x43:
                    case 0x44:
                    case 0x45:
                    case 70:
                    case 0x47:
                    case 0x48:
                    case 0x6a:
                        num = 14;
                        goto Label_0778;

                    case 0x49:
                    case 0x4e:
                        num = 15;
                        goto Label_0778;

                    case 0x4c:
                    case 0x4d:
                    case 0x63:
                        num = 0x10;
                        goto Label_0778;

                    case 80:
                    case 0x51:
                    case 0x52:
                    case 0x53:
                    case 0x54:
                    case 0x55:
                        num = 0x11;
                        goto Label_0778;

                    case 0x5b:
                    case 0x67:
                    case 0x68:
                    case 0x69:
                        num = 0x12;
                        goto Label_0778;

                    case 0x5c:
                        num = 0x13;
                        goto Label_0778;

                    case 0x5d:
                        num = 20;
                        goto Label_0778;

                    case 0x5e:
                    case 0x5f:
                    case 0x60:
                        num = 0x15;
                        goto Label_0778;

                    case 100:
                        num = 0x16;
                        goto Label_0778;

                    case 0x65:
                        num = 0x17;
                        goto Label_0778;

                    case 0x66:
                    case 0x81:
                    case 130:
                    case 0x83:
                        num = 0x18;
                        goto Label_0778;

                    case 0x7f:
                    case 0x80:
                        num = 0x19;
                        goto Label_0778;

                    default:
                        goto Label_0778;
                }
                if (entry.item.reportID < 0L)
                {
                    num += 30;
                }
            Label_0778:
                this.dateLabel.Color = Color.FromArgb(50, 50, 50);
                this.dateLabel.Position = new Point(0x55 + size.Width, 1);
                this.dateLabel.Size = new Size(830, this.backgroundImage.Height);
                if (entry.item.readStatus)
                {
                    this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                }
                else
                {
                    this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                }
                this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.dateLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.backgroundImage.addControl(this.dateLabel);
                if (forwardedString.Length > 0)
                {
                    this.forwardedLabel.Text = forwardedString;
                    this.forwardedLabel.Color = Color.FromArgb(50, 50, 50);
                    this.forwardedLabel.Position = new Point(100, 0x10);
                    this.forwardedLabel.Size = new Size(830, this.backgroundImage.Height);
                    this.forwardedLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.forwardedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.forwardedLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    this.backgroundImage.addControl(this.forwardedLabel);
                }
            Label_08F3:
                if (this.m_entry != null)
                {
                    if (forwardedString.Length == 0)
                    {
                        this.markedImage.Position = new Point(60, 0);
                    }
                    else
                    {
                        this.markedImage.Position = new Point(60, 5);
                    }
                    this.markedImage.CheckedImage = (Image) GFXLibrary.checkbox_checked;
                    this.markedImage.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
                    this.markedImage.Checked = false;
                    this.markedImage.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.markedToggled));
                    this.backgroundImage.addControl(this.markedImage);
                }
                if (num >= 0)
                {
                    switch (num)
                    {
                        case 0:
                            this.symbolImage.Image = (Image) GFXLibrary.icon_arrow_down;
                            this.symbolImage.Position = new Point(15, 3);
                            break;

                        case 1:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x27];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 2:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x29];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 3:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x2b];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 4:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x26];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 5:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[40];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 6:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x2a];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 7:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x23];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 8:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[12];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 9:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x24];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 10:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[8];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 11:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[2];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 12:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[5];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 13:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x25];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 14:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[3];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 15:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[1];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x10:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x22];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x11:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[4];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x12:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[9];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x13:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x2e];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 20:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x2f];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x15:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x30];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x16:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x31];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x17:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[50];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x18:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x33];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x19:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x3a];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x1f:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x35];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x20:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x37];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x21:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x39];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x22:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x34];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x23:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x36];
                            this.symbolImage.Position = new Point(15, -5);
                            break;

                        case 0x24:
                            this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[0x38];
                            this.symbolImage.Position = new Point(15, -5);
                            break;
                    }
                    this.backgroundImage.addControl(this.symbolImage);
                }
            }

            public void lineClicked()
            {
                if (this.m_parent != null)
                {
                    if (this.m_entry != null)
                    {
                        GameEngine.Instance.playInterfaceSound("ReportsPanel_report");
                        this.m_parent.getReport(this.m_entry);
                        this.descriptionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                        this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                        this.backgroundImage.invalidate();
                    }
                    else
                    {
                        GameEngine.Instance.playInterfaceSound("ReportsPanel_more");
                        this.m_parent.showMoreReports();
                    }
                }
            }

            public void markedToggled()
            {
                if (this.markedImage.Checked)
                {
                    this.backgroundImage.Colorise = ARGBColors.Green;
                }
                else
                {
                    this.backgroundImage.Colorise = ARGBColors.White;
                }
            }

            public void mouseRollLeave()
            {
            }

            public void mouseRollOver()
            {
            }

            public void update()
            {
            }
        }
    }
}

