namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class AllVillagesPanel : CustomSelfDrawPanel, IDockableControl
    {
        private List<VillageSummaryData> allVillageData = new List<VillageSummaryData>();
        private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel borderImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider7Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider8Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        private const int HEADER_SIZE = 0x2f;
        private CustomSelfDrawPanel.CSDImage headerImage1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage headerImage2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage headerImage3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage headerImage4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage headerImage5 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage headerImage6 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage headerImage7 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        public static AllVillagesPanel instance = null;
        private static DateTime lastUpdate = DateTime.MinValue;
        private List<VillageOverviewLine> lineList = new List<VillageOverviewLine>();
        private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private NameComparer nameComparer = new NameComparer();
        private int pageMode;
        private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
        public static List<VillageResourceReturnData> resourceReturnData = new List<VillageResourceReturnData>();
        private CustomSelfDrawPanel.CSDArea rolloverArea1 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea rolloverArea2 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea rolloverArea3 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea rolloverArea4 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea rolloverArea5 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea rolloverArea6 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea rolloverArea7 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDArea rolloverArea8 = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton tabBtnAll = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabBtnResrouce = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabBtnTroops = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabBtnUnits = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton tabBtnVillage = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private static List<DateTime> tooltipDates = new List<DateTime>();
        private CustomSelfDrawPanel.CSDLabel villageLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();

        public AllVillagesPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public int addTooltipDate(DateTime date)
        {
            tooltipDates.Add(date);
            return (tooltipDates.Count - 1);
        }

        public void addVillages()
        {
            this.wallScrollArea.clearControls();
            tooltipDates.Clear();
            int y = 0;
            this.lineList.Clear();
            int position = 0;
            foreach (VillageSummaryData data in this.allVillageData)
            {
                VillageOverviewLine control = new VillageOverviewLine();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(data, position, this.pageMode, data.expanded, this);
                this.wallScrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
                position++;
            }
            this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, y);
            if (y < this.wallScrollBar.Height)
            {
                this.wallScrollBar.Value = 0;
                this.wallScrollBarMoved();
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

        private void clearExpand()
        {
            foreach (VillageSummaryData data in this.allVillageData)
            {
                data.expanded = false;
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

        public void expand(int villageID)
        {
            foreach (VillageSummaryData data in this.allVillageData)
            {
                if (data.villageID == villageID)
                {
                    data.expanded = !data.expanded;
                    break;
                }
            }
            this.addVillages();
        }

        public static string getTooltipDate(int id)
        {
            if ((id >= 0) && (id < tooltipDates.Count))
            {
                DateTime time = tooltipDates[id];
                DateTime time2 = VillageMap.getCurrentServerTime();
                if (time > time2)
                {
                    TimeSpan span = (TimeSpan) (time - time2);
                    int totalSeconds = (int) span.TotalSeconds;
                    return (SK.Text("TOOLTIP_DATE_ends", "Ends") + " : " + VillageMap.createBuildTimeString(totalSeconds));
                }
            }
            return "";
        }

        public void init(bool resized)
        {
            int height = base.Height;
            instance = this;
            base.clearControls();
            if (!resized)
            {
                this.pageMode = 0;
            }
            this.mainBackgroundImage.FillColor = Color.FromArgb(0x86, 0x99, 0xa5);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = new Size(base.Width, height);
            base.addControl(this.mainBackgroundImage);
            this.backgroundFade.Image = (Image) GFXLibrary.background_top;
            this.backgroundFade.Position = new Point(0, 0);
            this.backgroundFade.Size = new Size(base.Width, this.backgroundFade.Image.Height);
            this.mainBackgroundImage.addControl(this.backgroundFade);
            this.titleLabel.Text = SK.Text("AllVillages_village_overview", "Villages Overview");
            this.titleLabel.Color = ARGBColors.Black;
            this.titleLabel.Position = new Point(5, 5);
            this.titleLabel.Size = new Size(0x143, 30);
            this.titleLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
            this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.titleLabel);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x2b, new Point(base.Width - 0x2c, 3));
            this.borderImage.Size = new Size(970, (height - 0x26) - 7);
            this.borderImage.Position = new Point(10, 0x26);
            this.mainBackgroundImage.addControl(this.borderImage);
            this.borderImage.Create((Image) GFXLibrary.parishwall_village_center_tab_outline_top_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_top_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_left, null, (Image) GFXLibrary.parishwall_village_center_tab_outline_middle_right, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_left, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_middle, (Image) GFXLibrary.parishwall_village_center_tab_outline_bottom_right);
            int num2 = 0x87;
            this.tabBtnAll.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnAll.ImageOver = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnAll.ImageClick = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnAll.Position = new Point(0xeb + num2, 12);
            this.tabBtnAll.Text.Text = SK.Text("ALLVillages_Overview_Alt", "Overview");
            this.tabBtnAll.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.tabBtnAll.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.tabBtnAll.TextYOffset = 0;
            this.tabBtnAll.Text.Color = ARGBColors.Black;
            this.tabBtnAll.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabAllClicked));
            this.tabBtnAll.Active = true;
            this.mainBackgroundImage.addControl(this.tabBtnAll);
            this.tabBtnTroops.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnTroops.ImageOver = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnTroops.ImageClick = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnTroops.Position = new Point(370 + num2, 12);
            this.tabBtnTroops.Text.Text = SK.Text("SelectArmyPanel_Troops", "Troops");
            this.tabBtnTroops.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.tabBtnTroops.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.tabBtnTroops.TextYOffset = 0;
            this.tabBtnTroops.Text.Color = ARGBColors.Black;
            this.tabBtnTroops.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabTroopsClicked));
            this.tabBtnTroops.Active = true;
            this.mainBackgroundImage.addControl(this.tabBtnTroops);
            this.tabBtnUnits.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnUnits.ImageOver = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnUnits.ImageClick = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnUnits.Position = new Point(0x1f9 + num2, 12);
            this.tabBtnUnits.Text.Text = SK.Text("UnitsPanel_Units", "Units");
            this.tabBtnUnits.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.tabBtnUnits.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.tabBtnUnits.TextYOffset = 0;
            this.tabBtnUnits.Text.Color = ARGBColors.Black;
            this.tabBtnUnits.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabUnitsClicked));
            this.tabBtnUnits.Active = true;
            this.mainBackgroundImage.addControl(this.tabBtnUnits);
            this.tabBtnVillage.ImageNorm = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnVillage.ImageOver = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnVillage.ImageClick = (Image) GFXLibrary.villageOverTab_down;
            this.tabBtnVillage.Position = new Point(640 + num2, 12);
            this.tabBtnVillage.Text.Text = SK.Text("GENERIC_Village", "Village");
            this.tabBtnVillage.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.tabBtnVillage.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.tabBtnVillage.TextYOffset = 0;
            this.tabBtnVillage.Text.Color = ARGBColors.Black;
            this.tabBtnVillage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tabVillageClicked));
            this.tabBtnVillage.Active = true;
            this.mainBackgroundImage.addControl(this.tabBtnVillage);
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, -19);
            this.headerLabelsImage.Position = new Point(0x19, 0x38);
            this.mainBackgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(290, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.villageLabel.Text = SK.Text("GENERIC_Village", "Village");
            this.villageLabel.Color = ARGBColors.Black;
            this.villageLabel.Position = new Point(15, -3);
            this.villageLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.villageLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.villageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.villageLabel);
            if (this.pageMode == 0)
            {
                this.tabBtnAll.Active = false;
                this.tabBtnAll.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnAll.ImageOver = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnAll.ImageClick = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnAll.TextYOffset = -3;
                this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider2Image.Position = new Point(0x177, 0);
                this.headerLabelsImage.addControl(this.divider2Image);
                this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider3Image.Position = new Point(460, 0);
                this.headerLabelsImage.addControl(this.divider3Image);
                this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider4Image.Position = new Point(0x221, 0);
                this.headerLabelsImage.addControl(this.divider4Image);
                this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider5Image.Position = new Point(630, 0);
                this.headerLabelsImage.addControl(this.divider5Image);
                this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider6Image.Position = new Point(0x2cb, 0);
                this.headerLabelsImage.addControl(this.divider6Image);
                this.divider7Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider7Image.Position = new Point(800, 0);
                this.headerLabelsImage.addControl(this.divider7Image);
                this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[0];
                this.headerImage1.Position = new Point(290, -17);
                this.headerLabelsImage.addControl(this.headerImage1);
                this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[7];
                this.headerImage2.Position = new Point(0x177, -17);
                this.headerLabelsImage.addControl(this.headerImage2);
                this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[8];
                this.headerImage3.Position = new Point(460, -17);
                this.headerLabelsImage.addControl(this.headerImage3);
                this.headerImage4.Image = (Image) GFXLibrary.villageOverviewIcons[6];
                this.headerImage4.Position = new Point(0x221, -17);
                this.headerLabelsImage.addControl(this.headerImage4);
                this.headerImage5.Image = (Image) GFXLibrary.villageOverviewIcons[0x12];
                this.headerImage5.Position = new Point(630, -17);
                this.headerLabelsImage.addControl(this.headerImage5);
                this.headerImage6.Image = (Image) GFXLibrary.villageOverviewIcons[9];
                this.headerImage6.Position = new Point(0x2cb, -17);
                this.headerLabelsImage.addControl(this.headerImage6);
                this.rolloverArea1.Position = this.divider1Image.Position;
                this.rolloverArea1.Size = new Size(this.divider2Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea1.CustomTooltipID = 0x1004;
                this.headerLabelsImage.addControl(this.rolloverArea1);
                this.rolloverArea2.Position = this.divider2Image.Position;
                this.rolloverArea2.Size = new Size(this.divider3Image.Position.X - this.divider2Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea2.CustomTooltipID = 0x1005;
                this.headerLabelsImage.addControl(this.rolloverArea2);
                this.rolloverArea3.Position = this.divider3Image.Position;
                this.rolloverArea3.Size = new Size(this.divider4Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea3.CustomTooltipID = 0x1006;
                this.headerLabelsImage.addControl(this.rolloverArea3);
                this.rolloverArea4.Position = this.divider4Image.Position;
                this.rolloverArea4.Size = new Size(this.divider5Image.Position.X - this.divider4Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea4.CustomTooltipID = 0x1007;
                this.headerLabelsImage.addControl(this.rolloverArea4);
                this.rolloverArea5.Position = this.divider5Image.Position;
                this.rolloverArea5.Size = new Size(this.divider6Image.Position.X - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea5.CustomTooltipID = 0x1008;
                this.headerLabelsImage.addControl(this.rolloverArea5);
                this.rolloverArea6.Position = this.divider6Image.Position;
                this.rolloverArea6.Size = new Size(this.divider7Image.Position.X - this.divider6Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea6.CustomTooltipID = 0x1009;
                this.headerLabelsImage.addControl(this.rolloverArea6);
            }
            else if (this.pageMode == 1)
            {
                this.tabBtnTroops.Active = false;
                this.tabBtnTroops.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnTroops.ImageOver = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnTroops.ImageClick = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnTroops.TextYOffset = -3;
                this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider2Image.Position = new Point(0x177, 0);
                this.headerLabelsImage.addControl(this.divider2Image);
                this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider3Image.Position = new Point(460, 0);
                this.headerLabelsImage.addControl(this.divider3Image);
                this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider4Image.Position = new Point(0x221, 0);
                this.headerLabelsImage.addControl(this.divider4Image);
                this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider5Image.Position = new Point(630, 0);
                this.headerLabelsImage.addControl(this.divider5Image);
                this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider6Image.Position = new Point(0x2cb, 0);
                this.headerLabelsImage.addControl(this.divider6Image);
                this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[0];
                this.headerImage1.Position = new Point(290, -17);
                this.headerLabelsImage.addControl(this.headerImage1);
                this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[1];
                this.headerImage2.Position = new Point(0x177, -17);
                this.headerLabelsImage.addControl(this.headerImage2);
                this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[2];
                this.headerImage3.Position = new Point(460, -17);
                this.headerLabelsImage.addControl(this.headerImage3);
                this.headerImage4.Image = (Image) GFXLibrary.villageOverviewIcons[3];
                this.headerImage4.Position = new Point(0x221, -17);
                this.headerLabelsImage.addControl(this.headerImage4);
                this.headerImage5.Image = (Image) GFXLibrary.villageOverviewIcons[5];
                this.headerImage5.Position = new Point(630, -17);
                this.headerLabelsImage.addControl(this.headerImage5);
                this.headerImage6.Image = (Image) GFXLibrary.villageOverviewIcons[4];
                this.headerImage6.Position = new Point(0x2cb, -17);
                this.headerLabelsImage.addControl(this.headerImage6);
                this.rolloverArea1.Position = this.divider1Image.Position;
                this.rolloverArea1.Size = new Size(this.divider2Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea1.CustomTooltipID = 0x100c;
                this.headerLabelsImage.addControl(this.rolloverArea1);
                this.rolloverArea2.Position = this.divider2Image.Position;
                this.rolloverArea2.Size = new Size(this.divider3Image.Position.X - this.divider2Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea2.CustomTooltipID = 0x100d;
                this.headerLabelsImage.addControl(this.rolloverArea2);
                this.rolloverArea3.Position = this.divider3Image.Position;
                this.rolloverArea3.Size = new Size(this.divider4Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea3.CustomTooltipID = 0x100e;
                this.headerLabelsImage.addControl(this.rolloverArea3);
                this.rolloverArea4.Position = this.divider4Image.Position;
                this.rolloverArea4.Size = new Size(this.divider5Image.Position.X - this.divider4Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea4.CustomTooltipID = 0x100f;
                this.headerLabelsImage.addControl(this.rolloverArea4);
                this.rolloverArea5.Position = this.divider5Image.Position;
                this.rolloverArea5.Size = new Size(this.divider6Image.Position.X - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea5.CustomTooltipID = 0x1010;
                this.headerLabelsImage.addControl(this.rolloverArea5);
                this.rolloverArea6.Position = this.divider6Image.Position;
                this.rolloverArea6.Size = this.rolloverArea1.Size;
                this.rolloverArea6.CustomTooltipID = 0x1011;
                this.headerLabelsImage.addControl(this.rolloverArea6);
            }
            else if (this.pageMode == 2)
            {
                this.tabBtnUnits.Active = false;
                this.tabBtnUnits.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnUnits.ImageOver = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnUnits.ImageClick = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnUnits.TextYOffset = -3;
                this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider3Image.Position = new Point(460, 0);
                this.headerLabelsImage.addControl(this.divider3Image);
                this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider5Image.Position = new Point(630, 0);
                this.headerLabelsImage.addControl(this.divider5Image);
                this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[7];
                this.headerImage1.Position = new Point(330, -17);
                this.headerLabelsImage.addControl(this.headerImage1);
                this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[8];
                this.headerImage2.Position = new Point(500, -17);
                this.headerLabelsImage.addControl(this.headerImage2);
                this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[6];
                this.headerImage3.Position = new Point(670, -17);
                this.headerLabelsImage.addControl(this.headerImage3);
                this.rolloverArea1.Position = this.divider1Image.Position;
                this.rolloverArea1.Size = new Size(this.divider3Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea1.CustomTooltipID = 0x1014;
                this.headerLabelsImage.addControl(this.rolloverArea1);
                this.rolloverArea2.Position = this.divider3Image.Position;
                this.rolloverArea2.Size = new Size(this.divider5Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea2.CustomTooltipID = 0x1015;
                this.headerLabelsImage.addControl(this.rolloverArea2);
                this.rolloverArea3.Position = this.divider5Image.Position;
                this.rolloverArea3.Size = new Size(840 - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea3.CustomTooltipID = 0x1016;
                this.headerLabelsImage.addControl(this.rolloverArea3);
            }
            else if (this.pageMode == 3)
            {
                this.tabBtnVillage.Active = false;
                this.tabBtnVillage.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnVillage.ImageOver = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnVillage.ImageClick = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnVillage.TextYOffset = -3;
                this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider2Image.Position = new Point(0x177, 0);
                this.headerLabelsImage.addControl(this.divider2Image);
                this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider3Image.Position = new Point(460, 0);
                this.headerLabelsImage.addControl(this.divider3Image);
                this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider4Image.Position = new Point(0x221, 0);
                this.headerLabelsImage.addControl(this.divider4Image);
                this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider5Image.Position = new Point(650, 0);
                this.headerLabelsImage.addControl(this.divider5Image);
                this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider6Image.Position = new Point(0x2df, 0);
                this.headerLabelsImage.addControl(this.divider6Image);
                this.divider7Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider7Image.Position = new Point(820, 0);
                this.headerLabelsImage.addControl(this.divider7Image);
                this.headerImage1.Image = (Image) GFXLibrary.villageOverviewIcons[13];
                this.headerImage1.Position = new Point(290, -17);
                this.headerLabelsImage.addControl(this.headerImage1);
                this.headerImage2.Image = (Image) GFXLibrary.villageOverviewIcons[12];
                this.headerImage2.Position = new Point(0x177, -17);
                this.headerLabelsImage.addControl(this.headerImage2);
                this.headerImage3.Image = (Image) GFXLibrary.villageOverviewIcons[14];
                this.headerImage3.Position = new Point(460, -17);
                this.headerLabelsImage.addControl(this.headerImage3);
                this.headerImage4.Image = (Image) GFXLibrary.villageOverviewIcons[15];
                this.headerImage4.Position = new Point(0x22b, -17);
                this.headerLabelsImage.addControl(this.headerImage4);
                this.headerImage5.Image = (Image) GFXLibrary.villageOverviewIcons[0x12];
                this.headerImage5.Position = new Point(650, -17);
                this.headerLabelsImage.addControl(this.headerImage5);
                this.headerImage6.Image = (Image) GFXLibrary.villageOverviewIcons[9];
                this.headerImage6.Position = new Point(0x2df, -17);
                this.headerLabelsImage.addControl(this.headerImage6);
                this.rolloverArea1.Position = this.divider1Image.Position;
                this.rolloverArea1.Size = new Size(this.divider2Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea1.CustomTooltipID = 0x1017;
                this.headerLabelsImage.addControl(this.rolloverArea1);
                this.rolloverArea2.Position = this.divider2Image.Position;
                this.rolloverArea2.Size = new Size(this.divider3Image.Position.X - this.divider2Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea2.CustomTooltipID = 0x1018;
                this.headerLabelsImage.addControl(this.rolloverArea2);
                this.rolloverArea3.Position = this.divider3Image.Position;
                this.rolloverArea3.Size = new Size(this.divider4Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea3.CustomTooltipID = 0x1019;
                this.headerLabelsImage.addControl(this.rolloverArea3);
                this.rolloverArea4.Position = this.divider4Image.Position;
                this.rolloverArea4.Size = new Size(this.divider5Image.Position.X - this.divider4Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea4.CustomTooltipID = 0x101a;
                this.headerLabelsImage.addControl(this.rolloverArea4);
                this.rolloverArea5.Position = this.divider5Image.Position;
                this.rolloverArea5.Size = new Size(this.divider6Image.Position.X - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea5.CustomTooltipID = 0x1008;
                this.headerLabelsImage.addControl(this.rolloverArea5);
                this.rolloverArea6.Position = this.divider6Image.Position;
                this.rolloverArea6.Size = new Size(this.divider7Image.Position.X - this.divider6Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea6.CustomTooltipID = 0x1009;
                this.headerLabelsImage.addControl(this.rolloverArea6);
            }
            else if (this.pageMode == 4)
            {
                this.tabBtnResrouce.Active = false;
                this.tabBtnResrouce.ImageNorm = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnResrouce.ImageOver = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnResrouce.ImageClick = (Image) GFXLibrary.villageOverTab_up;
                this.tabBtnResrouce.TextYOffset = -3;
                this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider2Image.Position = new Point(0x177, 0);
                this.headerLabelsImage.addControl(this.divider2Image);
                this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider3Image.Position = new Point(460, 0);
                this.headerLabelsImage.addControl(this.divider3Image);
                this.divider4Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider4Image.Position = new Point(0x221, 0);
                this.headerLabelsImage.addControl(this.divider4Image);
                this.divider5Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider5Image.Position = new Point(650, 0);
                this.headerLabelsImage.addControl(this.divider5Image);
                this.divider6Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider6Image.Position = new Point(0x2df, 0);
                this.headerLabelsImage.addControl(this.divider6Image);
                this.divider7Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
                this.divider7Image.Position = new Point(820, 0);
                this.headerLabelsImage.addControl(this.divider7Image);
                this.headerImage1.Image = (Image) GFXLibrary.donate_type_food;
                this.headerImage1.setSizeToImage();
                this.headerImage1.Position = new Point((this.divider1Image.X + ((this.divider2Image.X - this.divider1Image.X) / 2)) - (this.headerImage1.Width / 2), -20);
                this.headerLabelsImage.addControl(this.headerImage1);
                this.headerImage2.Image = (Image) GFXLibrary.com_32_ale_DS;
                this.headerImage2.setSizeToImage();
                this.headerImage2.Position = new Point((this.divider2Image.X + ((this.divider3Image.X - this.divider2Image.X) / 2)) - (this.headerImage2.Width / 2), -8);
                this.headerLabelsImage.addControl(this.headerImage2);
                this.headerImage3.Image = (Image) GFXLibrary.com_32_wood_DS;
                this.headerImage3.setSizeToImage();
                this.headerImage3.Position = new Point((this.divider3Image.X + ((this.divider4Image.X - this.divider3Image.X) / 2)) - (this.headerImage3.Width / 2), -8);
                this.headerLabelsImage.addControl(this.headerImage3);
                this.headerImage4.Image = (Image) GFXLibrary.com_32_stone_DS;
                this.headerImage4.setSizeToImage();
                this.headerImage4.Position = new Point((this.divider4Image.X + ((this.divider5Image.X - this.divider4Image.X) / 2)) - (this.headerImage4.Width / 2), -8);
                this.headerLabelsImage.addControl(this.headerImage4);
                this.headerImage5.Image = (Image) GFXLibrary.com_32_iron_DS;
                this.headerImage5.setSizeToImage();
                this.headerImage5.Position = new Point((this.divider5Image.X + ((this.divider6Image.X - this.divider5Image.X) / 2)) - (this.headerImage5.Width / 2), -8);
                this.headerLabelsImage.addControl(this.headerImage5);
                this.headerImage6.Image = (Image) GFXLibrary.com_32_pitch_DS;
                this.headerImage6.setSizeToImage();
                this.headerImage6.Position = new Point((this.divider6Image.X + ((this.divider7Image.X - this.divider6Image.X) / 2)) - (this.headerImage6.Width / 2), -8);
                this.headerLabelsImage.addControl(this.headerImage6);
                this.rolloverArea1.Position = this.divider1Image.Position;
                this.rolloverArea1.Size = new Size(this.divider2Image.Position.X - this.divider1Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea1.CustomTooltipID = 0x90;
                this.headerLabelsImage.addControl(this.rolloverArea1);
                this.rolloverArea2.Position = this.divider2Image.Position;
                this.rolloverArea2.Size = new Size(this.divider3Image.Position.X - this.divider2Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea2.CustomTooltipID = 0x1019;
                this.headerLabelsImage.addControl(this.rolloverArea2);
                this.rolloverArea3.Position = this.divider3Image.Position;
                this.rolloverArea3.Size = new Size(this.divider4Image.Position.X - this.divider3Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea3.CustomTooltipID = 0x8e;
                this.headerLabelsImage.addControl(this.rolloverArea3);
                this.rolloverArea4.Position = this.divider4Image.Position;
                this.rolloverArea4.Size = new Size(this.divider5Image.Position.X - this.divider4Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea4.CustomTooltipID = 0x8f;
                this.headerLabelsImage.addControl(this.rolloverArea4);
                this.rolloverArea5.Position = this.divider5Image.Position;
                this.rolloverArea5.Size = new Size(this.divider6Image.Position.X - this.divider5Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea5.CustomTooltipID = 0x101f;
                this.headerLabelsImage.addControl(this.rolloverArea5);
                this.rolloverArea6.Position = this.divider6Image.Position;
                this.rolloverArea6.Size = new Size(this.divider7Image.Position.X - this.divider6Image.Position.X, this.headerLabelsImage.Size.Height);
                this.rolloverArea6.CustomTooltipID = 0x1020;
                this.headerLabelsImage.addControl(this.rolloverArea6);
            }
            this.wallScrollArea.Position = new Point(0x19, 0x55);
            this.wallScrollArea.Size = new Size(0x38a, (height - 0x55) - 9);
            this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x389, (height - 0x55) - 10));
            this.mainBackgroundImage.addControl(this.wallScrollArea);
            this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
            this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            int num1 = this.wallScrollBar.Value;
            this.wallScrollBar.Position = new Point(0x3a5, 0x55);
            this.wallScrollBar.Size = new Size(0x18, (height - 0x55) - 9);
            this.mainBackgroundImage.addControl(this.wallScrollBar);
            this.wallScrollBar.Value = 0;
            this.wallScrollBar.Max = 100;
            this.wallScrollBar.NumVisibleLines = 0x19;
            this.wallScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            if (!resized)
            {
                if (GameEngine.Instance.World.isAccountPremium())
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - lastUpdate);
                    if (span.TotalSeconds > 30.0)
                    {
                        lastUpdate = DateTime.Now;
                        RemoteServices.Instance.set_PremiumOverview_UserCallBack(new RemoteServices.PremiumOverview_UserCallBack(this.PremiumOverview_callback));
                        RemoteServices.Instance.PremiumOverview();
                    }
                }
                else
                {
                    this.allVillageData.Clear();
                    foreach (int num3 in GameEngine.Instance.World.getUserVillageIDList())
                    {
                        VillageSummaryData item = new VillageSummaryData {
                            villageID = num3,
                            fake = true
                        };
                        this.allVillageData.Add(item);
                    }
                }
            }
            this.addVillages();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "AllVillagesPanel";
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
            this.allVillageData.Clear();
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

        private void PremiumOverview_callback(PremiumOverview_ReturnType returnData)
        {
            if (returnData.Success && (returnData.summaryData != null))
            {
                this.allVillageData = returnData.summaryData;
                this.allVillageData.Sort(this.nameComparer);
                foreach (VillageSummaryData data in this.allVillageData)
                {
                    GameEngine.Instance.World.getTotalTroopsOutOfVillage(data.villageID, ref data.numAttackingPeasants, ref data.numAttackingArchers, ref data.numAttackingPikemen, ref data.numAttackingSwordsmen, ref data.numAttackingCatapults, ref data.numAttackingCaptains, ref data.numReinforcingPeasants, ref data.numReinforcingArchers, ref data.numReinforcingPikemen, ref data.numReinforcingSwordsmen, ref data.numReinforcingCatapults, ref data.numReinforcingCaptains);
                    data.numAttackingScouts = GameEngine.Instance.World.countYourArmyScouts(data.villageID);
                    data.numTravellingMerchants = GameEngine.Instance.World.getTotalMerchantsFromVillage(data.villageID);
                    int athome = 0;
                    int num2 = GameEngine.Instance.World.countVillagePeople(data.villageID, 4, ref athome);
                    data.numTravellingMonks = num2 - athome;
                    data.numMonks = num2;
                }
                foreach (VillageResourceReturnData data2 in returnData.resourceData)
                {
                    resourceReturnData.Add(data2);
                }
            }
            else
            {
                this.allVillageData.Clear();
            }
            this.addVillages();
        }

        private void tabAllClicked()
        {
            this.pageMode = 0;
            this.init(true);
        }

        private void tabResourceClicked()
        {
            this.pageMode = 4;
            this.init(true);
        }

        private void tabTroopsClicked()
        {
            this.pageMode = 1;
            this.init(true);
        }

        private void tabUnitsClicked()
        {
            this.pageMode = 2;
            this.init(true);
        }

        private void tabVillageClicked()
        {
            this.pageMode = 3;
            this.init(true);
        }

        public static void travellersChanged()
        {
            lastUpdate = DateTime.MinValue;
        }

        public void update()
        {
        }

        private void wallScrollBarMoved()
        {
            int y = this.wallScrollBar.Value;
            this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 0x55 - y);
            this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
            this.wallScrollArea.invalidate();
            this.wallScrollBar.invalidate();
        }

        public class NameComparer : IComparer<VillageSummaryData>
        {
            public int Compare(VillageSummaryData x, VillageSummaryData y)
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
                string str = GameEngine.Instance.World.getVillageName(x.villageID).ToLowerInvariant();
                string strB = GameEngine.Instance.World.getVillageName(y.villageID).ToLowerInvariant();
                int num = str.CompareTo(strB);
                if (num != 0)
                {
                    return num;
                }
                if (x.villageID < y.villageID)
                {
                    return -1;
                }
                if (x.villageID > y.villageID)
                {
                    return 1;
                }
                return 0;
            }
        }

        public class VillageOverviewLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDLabel attackingLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel barracksLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDArea clickArea = new CustomSelfDrawPanel.CSDArea();
            private CustomSelfDrawPanel.CSDImage enclosedImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDImage excomdImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDArea excomRollover = new CustomSelfDrawPanel.CSDArea();
            private CustomSelfDrawPanel.CSDButton expandButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDImage idImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDArea idRollover = new CustomSelfDrawPanel.CSDArea();
            private AllVillagesPanel m_parent;
            private int m_position = -1000;
            private VillageSummaryData m_vsd;
            private CustomSelfDrawPanel.CSDImage peaceImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDArea peaceRollover = new CustomSelfDrawPanel.CSDArea();
            private CustomSelfDrawPanel.CSDLabel placedLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel reinforcingLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value1aLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value1bLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value1cLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value1dLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value1Label = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value2aLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value2bLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value2cLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value2dLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value2Label = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value3aLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value3bLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value3cLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value3dLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value3Label = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value4aLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value4bLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value4cLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value4dLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value4Label = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value5aLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value5bLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value5cLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value5dLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value5Label = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value6aLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value6bLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value6cLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value6dLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel value6Label = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel villageName = new CustomSelfDrawPanel.CSDLabel();

            public void clickedLine()
            {
                if (this.m_vsd != null)
                {
                    GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
                    Point point = GameEngine.Instance.World.getVillageLocation(this.m_vsd.villageID);
                    GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_WORLD;
                    InterfaceMgr.Instance.closeParishPanel();
                    InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                    GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_vsd.villageID, false, true, true, false);
                }
            }

            private void expandClick()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.expand(this.m_vsd.villageID);
                }
            }

            public void init(VillageSummaryData vsd, int position, int pageMode, bool expanded, AllVillagesPanel parent)
            {
                this.m_parent = parent;
                this.m_position = position;
                this.ClipVisible = true;
                this.m_vsd = vsd;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(10, 0);
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                base.addControl(this.backgroundImage);
                this.Size = new Size(890, this.backgroundImage.Size.Height);
                int height = GFXLibrary.lineitem_strip_02_light.Height;
                NumberFormatInfo nFI = GameEngine.NFI;
                this.villageName.Text = GameEngine.Instance.World.getVillageName(vsd.villageID);
                this.villageName.Color = ARGBColors.Black;
                this.villageName.Position = new Point(0x13, 0);
                this.villageName.Size = new Size(220, height);
                this.villageName.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                this.villageName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                base.addControl(this.villageName);
                int num2 = 0x113;
                if (pageMode == 0)
                {
                    int num3 = ((((vsd.numAttackingArchers + vsd.numAttackingCaptains) + vsd.numAttackingCatapults) + vsd.numAttackingPeasants) + vsd.numAttackingPikemen) + vsd.numAttackingSwordsmen;
                    num3 += ((((vsd.numLocalArchers + vsd.numLocalCaptains) + vsd.numLocalCatapults) + vsd.numLocalPeasants) + vsd.numLocalPikemen) + vsd.numLocalSwordsmen;
                    num3 += (((vsd.numPlacedArchers + vsd.numPlacedCaptains) + vsd.numPlacedPeasants) + vsd.numPlacedPikemen) + vsd.numPlacedSwordsmen;
                    this.value1Label.Text = (num3 + (((((vsd.numReinforcingArchers + vsd.numReinforcingCaptains) + vsd.numReinforcingCatapults) + vsd.numReinforcingPeasants) + vsd.numReinforcingPikemen) + vsd.numReinforcingSwordsmen)).ToString();
                    if (vsd.fake)
                    {
                        this.value1Label.Text = "?";
                    }
                    this.value1Label.Color = ARGBColors.Black;
                    this.value1Label.Position = new Point(num2 + 5, 0);
                    this.value1Label.Size = new Size(70, height);
                    this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value1Label);
                    this.value2Label.Text = (vsd.numAttackingScouts + vsd.numLocalScouts).ToString();
                    if (vsd.fake)
                    {
                        this.value2Label.Text = "?";
                    }
                    this.value2Label.Color = ARGBColors.Black;
                    this.value2Label.Position = new Point((num2 + 0x55) + 5, 0);
                    this.value2Label.Size = new Size(60, height);
                    this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value2Label);
                    this.value3Label.Text = (vsd.numMerchantsAtHome + vsd.numTravellingMerchants).ToString();
                    if (vsd.fake)
                    {
                        this.value3Label.Text = "?";
                    }
                    this.value3Label.Color = ARGBColors.Black;
                    this.value3Label.Position = new Point((num2 + 170) + 5, 0);
                    this.value3Label.Size = new Size(60, height);
                    this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value3Label);
                    this.value4Label.Text = vsd.numMonks.ToString();
                    if (vsd.fake)
                    {
                        this.value4Label.Text = "?";
                    }
                    this.value4Label.Color = ARGBColors.Black;
                    this.value4Label.Position = new Point((num2 + 0xff) + 5, 0);
                    this.value4Label.Size = new Size(60, height);
                    this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value4Label);
                    int popularityLevel = vsd.popularityLevel;
                    this.value5Label.Text = popularityLevel.ToString();
                    if (vsd.fake)
                    {
                        this.value5Label.Text = "?";
                    }
                    this.value5Label.Color = ARGBColors.Black;
                    if (popularityLevel < 0)
                    {
                        this.value5Label.Color = Color.FromArgb(170, 0, 0);
                    }
                    this.value5Label.Position = new Point((num2 + 340) + 5, 0);
                    this.value5Label.Size = new Size(60, height);
                    this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value5Label);
                    this.value6Label.Text = vsd.numBuildings.ToString();
                    if (vsd.fake)
                    {
                        this.value6Label.Text = "?";
                    }
                    this.value6Label.Color = ARGBColors.Black;
                    this.value6Label.Position = new Point((num2 + 0x1a9) + 5, 0);
                    this.value6Label.Size = new Size(60, height);
                    this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value6Label);
                    if (!vsd.fake)
                    {
                        if (vsd.enclosedKeep)
                        {
                            this.enclosedImage.Image = (Image) GFXLibrary.villageOverviewIcons[10];
                            this.idRollover.CustomTooltipID = 0x100a;
                            this.enclosedImage.Position = new Point(num2 + 510, -15);
                        }
                        else
                        {
                            this.enclosedImage.Image = (Image) GFXLibrary.villageOverviewIcons[11];
                            this.idRollover.CustomTooltipID = 0x100b;
                            this.enclosedImage.Position = new Point((num2 + 510) + 2, -15);
                        }
                        base.addControl(this.enclosedImage);
                        this.idRollover.Size = new Size(0x19, 0x19);
                        this.idRollover.Position = new Point((num2 + 510) + 0x1d, 4);
                        base.addControl(this.idRollover);
                    }
                }
                else if (pageMode == 1)
                {
                    this.value1Label.Text = (((vsd.numAttackingPeasants + vsd.numLocalPeasants) + vsd.numPlacedPeasants) + vsd.numReinforcingPeasants).ToString();
                    if (vsd.fake)
                    {
                        this.value1Label.Text = "?";
                    }
                    this.value1Label.Color = ARGBColors.Black;
                    this.value1Label.Position = new Point(num2 + 5, 0);
                    this.value1Label.Size = new Size(70, height);
                    this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value1Label);
                    this.value2Label.Text = (((vsd.numAttackingArchers + vsd.numLocalArchers) + vsd.numPlacedArchers) + vsd.numReinforcingArchers).ToString();
                    if (vsd.fake)
                    {
                        this.value2Label.Text = "?";
                    }
                    this.value2Label.Color = ARGBColors.Black;
                    this.value2Label.Position = new Point((num2 + 0x55) + 5, 0);
                    this.value2Label.Size = new Size(60, height);
                    this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value2Label);
                    this.value3Label.Text = (((vsd.numAttackingPikemen + vsd.numLocalPikemen) + vsd.numPlacedPikemen) + vsd.numReinforcingPikemen).ToString();
                    if (vsd.fake)
                    {
                        this.value3Label.Text = "?";
                    }
                    this.value3Label.Color = ARGBColors.Black;
                    this.value3Label.Position = new Point((num2 + 170) + 5, 0);
                    this.value3Label.Size = new Size(60, height);
                    this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value3Label);
                    this.value4Label.Text = (((vsd.numAttackingSwordsmen + vsd.numLocalSwordsmen) + vsd.numPlacedSwordsmen) + vsd.numReinforcingSwordsmen).ToString();
                    if (vsd.fake)
                    {
                        this.value4Label.Text = "?";
                    }
                    this.value4Label.Color = ARGBColors.Black;
                    this.value4Label.Position = new Point((num2 + 0xff) + 5, 0);
                    this.value4Label.Size = new Size(60, height);
                    this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value4Label);
                    this.value5Label.Text = ((vsd.numAttackingCatapults + vsd.numLocalCatapults) + vsd.numReinforcingCatapults).ToString();
                    if (vsd.fake)
                    {
                        this.value5Label.Text = "?";
                    }
                    this.value5Label.Color = ARGBColors.Black;
                    this.value5Label.Position = new Point((num2 + 340) + 5, 0);
                    this.value5Label.Size = new Size(60, height);
                    this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value5Label);
                    this.value6Label.Text = (((vsd.numAttackingCaptains + vsd.numLocalCaptains) + vsd.numReinforcingCaptains) + vsd.numPlacedCaptains).ToString();
                    if (vsd.fake)
                    {
                        this.value6Label.Text = "?";
                    }
                    this.value6Label.Color = ARGBColors.Black;
                    this.value6Label.Position = new Point((num2 + 0x1a9) + 5, 0);
                    this.value6Label.Size = new Size(60, height);
                    this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value6Label);
                    if (!vsd.fake)
                    {
                        if (expanded)
                        {
                            this.Size = new Size(890, this.backgroundImage.Size.Height + 100);
                            this.barracksLabel.Text = SK.Text("BARRACKS_In_Barracks", "In Barracks");
                            this.barracksLabel.Color = ARGBColors.Black;
                            this.barracksLabel.Position = new Point(0, 0x1a);
                            this.barracksLabel.Size = new Size(0x127, height);
                            this.barracksLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.barracksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.barracksLabel);
                            this.value1aLabel.Text = vsd.numLocalPeasants.ToString();
                            this.value1aLabel.Color = ARGBColors.Black;
                            this.value1aLabel.Position = new Point(num2 + 5, 0x1a);
                            this.value1aLabel.Size = new Size(70, height);
                            this.value1aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value1aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value1aLabel);
                            this.value2aLabel.Text = vsd.numLocalArchers.ToString();
                            this.value2aLabel.Color = ARGBColors.Black;
                            this.value2aLabel.Position = new Point((num2 + 0x55) + 5, 0x1a);
                            this.value2aLabel.Size = new Size(60, height);
                            this.value2aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value2aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value2aLabel);
                            this.value3aLabel.Text = vsd.numLocalPikemen.ToString();
                            this.value3aLabel.Color = ARGBColors.Black;
                            this.value3aLabel.Position = new Point((num2 + 170) + 5, 0x1a);
                            this.value3aLabel.Size = new Size(60, height);
                            this.value3aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value3aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value3aLabel);
                            this.value4aLabel.Text = vsd.numLocalSwordsmen.ToString();
                            this.value4aLabel.Color = ARGBColors.Black;
                            this.value4aLabel.Position = new Point((num2 + 0xff) + 5, 0x1a);
                            this.value4aLabel.Size = new Size(60, height);
                            this.value4aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value4aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value4aLabel);
                            this.value5aLabel.Text = vsd.numLocalCatapults.ToString();
                            this.value5aLabel.Color = ARGBColors.Black;
                            this.value5aLabel.Position = new Point((num2 + 340) + 5, 0x1a);
                            this.value5aLabel.Size = new Size(60, height);
                            this.value5aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value5aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value5aLabel);
                            this.value6aLabel.Text = vsd.numLocalCaptains.ToString();
                            this.value6aLabel.Color = ARGBColors.Black;
                            this.value6aLabel.Position = new Point((num2 + 0x1a9) + 5, 0x1a);
                            this.value6aLabel.Size = new Size(60, height);
                            this.value6aLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value6aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value6aLabel);
                            this.placedLabel.Text = SK.Text("BARRACKS_In_Castle", "In Castle");
                            this.placedLabel.Color = ARGBColors.Black;
                            this.placedLabel.Position = new Point(0, 0x33);
                            this.placedLabel.Size = new Size(0x127, height);
                            this.placedLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.placedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.placedLabel);
                            this.value1bLabel.Text = vsd.numPlacedPeasants.ToString();
                            this.value1bLabel.Color = ARGBColors.Black;
                            this.value1bLabel.Position = new Point(num2 + 5, 0x33);
                            this.value1bLabel.Size = new Size(70, height);
                            this.value1bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value1bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value1bLabel);
                            this.value2bLabel.Text = vsd.numPlacedArchers.ToString();
                            this.value2bLabel.Color = ARGBColors.Black;
                            this.value2bLabel.Position = new Point((num2 + 0x55) + 5, 0x33);
                            this.value2bLabel.Size = new Size(60, height);
                            this.value2bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value2bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value2bLabel);
                            this.value3bLabel.Text = vsd.numPlacedPikemen.ToString();
                            this.value3bLabel.Color = ARGBColors.Black;
                            this.value3bLabel.Position = new Point((num2 + 170) + 5, 0x33);
                            this.value3bLabel.Size = new Size(60, height);
                            this.value3bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value3bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value3bLabel);
                            this.value4bLabel.Text = vsd.numPlacedSwordsmen.ToString();
                            this.value4bLabel.Color = ARGBColors.Black;
                            this.value4bLabel.Position = new Point((num2 + 0xff) + 5, 0x33);
                            this.value4bLabel.Size = new Size(60, height);
                            this.value4bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value4bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value4bLabel);
                            this.value6bLabel.Text = vsd.numPlacedCaptains.ToString();
                            this.value6bLabel.Color = ARGBColors.Black;
                            this.value6bLabel.Position = new Point((num2 + 0x1a9) + 5, 0x33);
                            this.value6bLabel.Size = new Size(60, height);
                            this.value6bLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value6bLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value6bLabel);
                            this.attackingLabel.Text = SK.Text("GENERIC_Attacking", "Attacking");
                            this.attackingLabel.Color = ARGBColors.Black;
                            this.attackingLabel.Position = new Point(0, 0x4c);
                            this.attackingLabel.Size = new Size(0x127, height);
                            this.attackingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.attackingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.attackingLabel);
                            this.value1cLabel.Text = vsd.numAttackingPeasants.ToString();
                            this.value1cLabel.Color = ARGBColors.Black;
                            this.value1cLabel.Position = new Point(num2 + 5, 0x4c);
                            this.value1cLabel.Size = new Size(70, height);
                            this.value1cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value1cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value1cLabel);
                            this.value2cLabel.Text = vsd.numAttackingArchers.ToString();
                            this.value2cLabel.Color = ARGBColors.Black;
                            this.value2cLabel.Position = new Point((num2 + 0x55) + 5, 0x4c);
                            this.value2cLabel.Size = new Size(60, height);
                            this.value2cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value2cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value2cLabel);
                            this.value3cLabel.Text = vsd.numAttackingPikemen.ToString();
                            this.value3cLabel.Color = ARGBColors.Black;
                            this.value3cLabel.Position = new Point((num2 + 170) + 5, 0x4c);
                            this.value3cLabel.Size = new Size(60, height);
                            this.value3cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value3cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value3cLabel);
                            this.value4cLabel.Text = vsd.numAttackingSwordsmen.ToString();
                            this.value4cLabel.Color = ARGBColors.Black;
                            this.value4cLabel.Position = new Point((num2 + 0xff) + 5, 0x4c);
                            this.value4cLabel.Size = new Size(60, height);
                            this.value4cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value4cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value4cLabel);
                            this.value5cLabel.Text = vsd.numAttackingCatapults.ToString();
                            this.value5cLabel.Color = ARGBColors.Black;
                            this.value5cLabel.Position = new Point((num2 + 340) + 5, 0x4c);
                            this.value5cLabel.Size = new Size(60, height);
                            this.value5cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value5cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value5cLabel);
                            this.value6cLabel.Text = vsd.numAttackingCaptains.ToString();
                            this.value6cLabel.Color = ARGBColors.Black;
                            this.value6cLabel.Position = new Point((num2 + 0x1a9) + 5, 0x4c);
                            this.value6cLabel.Size = new Size(60, height);
                            this.value6cLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value6cLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value6cLabel);
                            this.reinforcingLabel.Text = SK.Text("BARRACKS_Reinforcing", "Reinforcing");
                            this.reinforcingLabel.Color = ARGBColors.Black;
                            this.reinforcingLabel.Position = new Point(0, 0x65);
                            this.reinforcingLabel.Size = new Size(0x127, height);
                            this.reinforcingLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.reinforcingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.reinforcingLabel);
                            this.value1dLabel.Text = vsd.numReinforcingPeasants.ToString();
                            this.value1dLabel.Color = ARGBColors.Black;
                            this.value1dLabel.Position = new Point(num2 + 5, 0x65);
                            this.value1dLabel.Size = new Size(70, height);
                            this.value1dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value1dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value1dLabel);
                            this.value2dLabel.Text = vsd.numReinforcingArchers.ToString();
                            this.value2dLabel.Color = ARGBColors.Black;
                            this.value2dLabel.Position = new Point((num2 + 0x55) + 5, 0x65);
                            this.value2dLabel.Size = new Size(60, height);
                            this.value2dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value2dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value2dLabel);
                            this.value3dLabel.Text = vsd.numReinforcingPikemen.ToString();
                            this.value3dLabel.Color = ARGBColors.Black;
                            this.value3dLabel.Position = new Point((num2 + 170) + 5, 0x65);
                            this.value3dLabel.Size = new Size(60, height);
                            this.value3dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value3dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value3dLabel);
                            this.value4dLabel.Text = vsd.numReinforcingSwordsmen.ToString();
                            this.value4dLabel.Color = ARGBColors.Black;
                            this.value4dLabel.Position = new Point((num2 + 0xff) + 5, 0x65);
                            this.value4dLabel.Size = new Size(60, height);
                            this.value4dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value4dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value4dLabel);
                            this.value5dLabel.Text = vsd.numReinforcingCatapults.ToString();
                            this.value5dLabel.Color = ARGBColors.Black;
                            this.value5dLabel.Position = new Point((num2 + 340) + 5, 0x65);
                            this.value5dLabel.Size = new Size(60, height);
                            this.value5dLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                            this.value5dLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value5dLabel);
                            this.expandButton.ImageNorm = (Image) GFXLibrary.blue_screen_button_array[1];
                            this.expandButton.ImageOver = (Image) GFXLibrary.blue_screen_button_array[3];
                            this.expandButton.ImageClick = (Image) GFXLibrary.blue_screen_button_array[5];
                            this.expandButton.Position = new Point(840, 2);
                            this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClick));
                            base.addControl(this.expandButton);
                        }
                        else
                        {
                            this.expandButton.ImageNorm = (Image) GFXLibrary.blue_screen_button_array[0];
                            this.expandButton.ImageOver = (Image) GFXLibrary.blue_screen_button_array[2];
                            this.expandButton.ImageClick = (Image) GFXLibrary.blue_screen_button_array[4];
                            this.expandButton.Position = new Point(840, 2);
                            this.expandButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.expandClick));
                            base.addControl(this.expandButton);
                        }
                    }
                }
                else if (pageMode == 2)
                {
                    int numAttackingScouts = vsd.numAttackingScouts;
                    int numLocalScouts = vsd.numLocalScouts;
                    this.value1Label.Text = (numAttackingScouts + numLocalScouts).ToString();
                    if (vsd.fake)
                    {
                        this.value1Label.Text = "?";
                    }
                    this.value1Label.Color = ARGBColors.Black;
                    this.value1Label.Position = new Point(0x130, 0);
                    this.value1Label.Size = new Size(70, height);
                    this.value1Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value1Label);
                    if ((numAttackingScouts > 0) && !vsd.fake)
                    {
                        this.value2Label.Text = "(" + numAttackingScouts.ToString() + ")";
                        this.value2Label.Color = ARGBColors.Black;
                        this.value2Label.Position = new Point(0x17b, 0);
                        this.value2Label.Size = new Size(70, height);
                        this.value2Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                        this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                        base.addControl(this.value2Label);
                    }
                    int numTravellingMerchants = vsd.numTravellingMerchants;
                    int numMerchantsAtHome = vsd.numMerchantsAtHome;
                    this.value3Label.Text = (numTravellingMerchants + numMerchantsAtHome).ToString();
                    if (vsd.fake)
                    {
                        this.value3Label.Text = "?";
                    }
                    this.value3Label.Color = ARGBColors.Black;
                    this.value3Label.Position = new Point(0x1da, 0);
                    this.value3Label.Size = new Size(70, height);
                    this.value3Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value3Label);
                    if ((numTravellingMerchants > 0) && !vsd.fake)
                    {
                        this.value4Label.Text = "(" + numTravellingMerchants.ToString() + ")";
                        this.value4Label.Color = ARGBColors.Black;
                        this.value4Label.Position = new Point(0x225, 0);
                        this.value4Label.Size = new Size(70, height);
                        this.value4Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                        this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                        base.addControl(this.value4Label);
                    }
                    int numTravellingMonks = vsd.numTravellingMonks;
                    this.value5Label.Text = vsd.numMonks.ToString();
                    if (vsd.fake)
                    {
                        this.value5Label.Text = "?";
                    }
                    this.value5Label.Color = ARGBColors.Black;
                    this.value5Label.Position = new Point(0x284, 0);
                    this.value5Label.Size = new Size(70, height);
                    this.value5Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                    this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value5Label);
                    if ((numTravellingMonks > 0) && !vsd.fake)
                    {
                        this.value6Label.Text = "(" + numTravellingMonks.ToString() + ")";
                        this.value6Label.Color = ARGBColors.Black;
                        this.value6Label.Position = new Point(0x2cf, 0);
                        this.value6Label.Size = new Size(70, height);
                        this.value6Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
                        this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                        base.addControl(this.value6Label);
                    }
                }
                else if (pageMode == 3)
                {
                    string str = "";
                    int totalPeople = vsd.totalPeople;
                    if (vsd.housingCapacity < vsd.totalPeople)
                    {
                        totalPeople = vsd.housingCapacity;
                    }
                    double num22 = (totalPeople * VillageBuildingsData.getTaxIncomeLevel(vsd.setTaxLevel, GameEngine.Instance.World.UserCardData)) * GameEngine.Instance.LocalWorldData.goldIncomeRate;
                    this.value1Label.Color = ARGBColors.Black;
                    if (num22 > 0.0)
                    {
                        str = "+" + ((int) num22).ToString("N", nFI);
                    }
                    else if (num22 < 0.0)
                    {
                        str = ((int) num22).ToString("N", nFI);
                        this.value1Label.Color = Color.FromArgb(0xff, 200, 0);
                    }
                    else
                    {
                        str = "0";
                    }
                    this.value1Label.Text = str;
                    if (vsd.fake)
                    {
                        this.value1Label.Text = "?";
                    }
                    this.value1Label.Position = new Point(num2 + 5, 0);
                    this.value1Label.Size = new Size(70, height);
                    this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value1Label);
                    str = "";
                    string str2 = "";
                    if (vsd.currentRationsLevel >= 6.0)
                    {
                        str2 = "x4";
                    }
                    else if (vsd.currentRationsLevel >= 5.0)
                    {
                        str2 = "x3";
                    }
                    else if (vsd.currentRationsLevel >= 4.0)
                    {
                        str2 = "x2";
                    }
                    else if (vsd.currentRationsLevel >= 3.0)
                    {
                        str2 = "x1";
                    }
                    else if (vsd.currentRationsLevel >= 2.0)
                    {
                        str2 = "1/2";
                    }
                    else if (vsd.currentRationsLevel >= 1.0)
                    {
                        str2 = "1/4";
                    }
                    else
                    {
                        str2 = "0";
                    }
                    str = str + str2;
                    this.value2Label.Text = str;
                    if (vsd.fake)
                    {
                        this.value2Label.Text = "?";
                    }
                    this.value2Label.Color = ARGBColors.Black;
                    if (vsd.setRationsLevel != vsd.currentRationsLevel)
                    {
                        this.value2Label.Color = Color.FromArgb(170, 0, 0);
                    }
                    this.value2Label.Position = new Point((num2 + 0x55) + 5, 0);
                    this.value2Label.Size = new Size(60, height);
                    this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value2Label);
                    str = "";
                    str2 = "";
                    if (vsd.currentAleLevel >= 4.0)
                    {
                        str2 = "x4";
                    }
                    else if (vsd.currentAleLevel >= 3.0)
                    {
                        str2 = "x3";
                    }
                    else if (vsd.currentAleLevel >= 2.0)
                    {
                        str2 = "x2";
                    }
                    else if (vsd.currentAleLevel >= 1.0)
                    {
                        str2 = "x1";
                    }
                    else
                    {
                        str2 = "0";
                    }
                    str = str + str2;
                    this.value3Label.Text = str;
                    if (vsd.fake)
                    {
                        this.value3Label.Text = "?";
                    }
                    this.value3Label.Color = ARGBColors.Black;
                    if (vsd.setAleLevel != vsd.currentAleLevel)
                    {
                        this.value3Label.Color = Color.FromArgb(170, 0, 0);
                    }
                    this.value3Label.Position = new Point((num2 + 170) + 5, 0);
                    this.value3Label.Size = new Size(60, height);
                    this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value3Label);
                    this.value4Label.Text = vsd.totalPeople.ToString() + " / " + vsd.housingCapacity.ToString() + " ";
                    if (vsd.fake)
                    {
                        this.value4Label.Text = "?";
                    }
                    this.value4Label.Color = ARGBColors.Black;
                    this.value4Label.Position = new Point((num2 + 0xff) + 5, 0);
                    this.value4Label.Size = new Size(0x4b, height);
                    this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value4Label);
                    this.value4aLabel.Text = vsd.sparePeople.ToString();
                    if (vsd.fake)
                    {
                        this.value4aLabel.Text = "?";
                    }
                    this.value4aLabel.Color = ARGBColors.Black;
                    this.value4aLabel.Position = new Point((((num2 + 0xff) + 5) + 11) + 20, 0);
                    this.value4aLabel.Size = new Size(80, height);
                    this.value4aLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value4aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value4aLabel);
                    int num23 = vsd.popularityLevel;
                    this.value5Label.Text = num23.ToString();
                    if (vsd.fake)
                    {
                        this.value5Label.Text = "?";
                    }
                    this.value5Label.Color = ARGBColors.Black;
                    if (num23 < 0)
                    {
                        this.value5Label.Color = Color.FromArgb(170, 0, 0);
                    }
                    this.value5Label.Position = new Point(((num2 + 340) + 5) + 20, 0);
                    this.value5Label.Size = new Size(70, height);
                    this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value5Label);
                    this.value6Label.Text = vsd.numBuildings.ToString();
                    if (vsd.fake)
                    {
                        this.value6Label.Text = "?";
                    }
                    this.value6Label.Color = ARGBColors.Black;
                    this.value6Label.Position = new Point(((num2 + 0x1a9) + 5) + 20, 0);
                    this.value6Label.Size = new Size(60, height);
                    this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                    this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                    base.addControl(this.value6Label);
                    if (!vsd.fake)
                    {
                        int num25 = (num2 + 510) + 20;
                        int x = ((num2 + 510) + 20) + 15;
                        if (vsd.interdictProtectionEndTime > VillageMap.getCurrentServerTime())
                        {
                            this.idImage.Image = (Image) GFXLibrary.villageOverviewIcons[0x10];
                            this.idImage.Position = new Point((int) (((double) num25) / 0.6), -2);
                            this.idImage.setScale(0.6);
                            base.addControl(this.idImage);
                            num25 += 0x1d;
                            this.idRollover.Size = new Size(0x16, 0x16);
                            this.idRollover.Position = new Point(x, 4);
                            this.idRollover.CustomTooltipID = 0x101b;
                            this.idRollover.CustomTooltipData = parent.addTooltipDate(vsd.interdictProtectionEndTime);
                            base.addControl(this.idRollover);
                            x += 0x1d;
                        }
                        if (vsd.excommunicationEndTime > VillageMap.getCurrentServerTime())
                        {
                            this.excomdImage.Image = (Image) GFXLibrary.villageOverviewIcons[0x11];
                            this.excomdImage.Position = new Point((int) (((double) num25) / 0.6), -2);
                            this.excomdImage.setScale(0.6);
                            base.addControl(this.excomdImage);
                            num25 += 0x1d;
                            this.excomRollover.Size = new Size(0x16, 0x16);
                            this.excomRollover.Position = new Point(x, 4);
                            this.excomRollover.CustomTooltipID = 0x101c;
                            this.excomRollover.CustomTooltipData = parent.addTooltipDate(vsd.excommunicationEndTime);
                            base.addControl(this.excomRollover);
                            x += 0x1d;
                        }
                        if (vsd.peaceTimeEndTime > VillageMap.getCurrentServerTime())
                        {
                            this.peaceImage.Image = (Image) GFXLibrary.villageOverviewIcons[0x13];
                            this.peaceImage.Position = new Point((int) (((double) num25) / 0.6), -2);
                            this.peaceImage.setScale(0.6);
                            base.addControl(this.peaceImage);
                            num25 += 0x1d;
                            this.peaceRollover.Size = new Size(0x16, 0x16);
                            this.peaceRollover.Position = new Point(x, 4);
                            this.peaceRollover.CustomTooltipID = 0x101d;
                            this.peaceRollover.CustomTooltipData = parent.addTooltipDate(vsd.peaceTimeEndTime);
                            base.addControl(this.peaceRollover);
                            x += 0x1d;
                        }
                    }
                }
                else if (pageMode == 4)
                {
                    foreach (VillageResourceReturnData data in AllVillagesPanel.resourceReturnData)
                    {
                        if (vsd.villageID == data.villageID)
                        {
                            double d = ((((data.applesLevel + data.breadLevel) + data.cheeseLevel) + data.fishLevel) + data.meatLevel) + data.vegLevel;
                            this.value1Label.Text = Math.Max(0, Convert.ToInt32(Math.Floor(d))).ToString();
                            this.value1Label.Color = ARGBColors.Black;
                            this.value1Label.Position = new Point(num2 + 5, 0);
                            this.value1Label.Size = new Size(70, height);
                            this.value1Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                            this.value1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value1Label);
                            this.value2Label.Text = data.aleLevel.ToString();
                            this.value2Label.Color = ARGBColors.Black;
                            this.value2Label.Position = new Point((num2 + 0x55) + 5, 0);
                            this.value2Label.Size = new Size(60, height);
                            this.value2Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                            this.value2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value2Label);
                            this.value3Label.Text = data.woodLevel.ToString();
                            this.value3Label.Color = ARGBColors.Black;
                            this.value3Label.Position = new Point((num2 + 170) + 5, 0);
                            this.value3Label.Size = new Size(60, height);
                            this.value3Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                            this.value3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value3Label);
                            this.value4Label.Text = data.stoneLevel.ToString();
                            this.value4Label.Color = ARGBColors.Black;
                            this.value4Label.Position = new Point((num2 + 0xff) + 5, 0);
                            this.value4Label.Size = new Size(0x4b, height);
                            this.value4Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                            this.value4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value4Label);
                            this.value5Label.Text = data.ironLevel.ToString();
                            this.value5Label.Color = ARGBColors.Black;
                            this.value5Label.Position = new Point(((num2 + 340) + 5) + 20, 0);
                            this.value5Label.Size = new Size(70, height);
                            this.value5Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                            this.value5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value5Label);
                            this.value6Label.Text = data.pitchLevel.ToString();
                            this.value6Label.Color = ARGBColors.Black;
                            this.value6Label.Position = new Point(((num2 + 0x1a9) + 5) + 20, 0);
                            this.value6Label.Size = new Size(60, height);
                            this.value6Label.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
                            this.value6Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                            base.addControl(this.value6Label);
                        }
                    }
                }
                this.clickArea.Position = new Point(0, 0);
                this.clickArea.Size = new Size(790, this.backgroundImage.Height);
                this.clickArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
                if (vsd.fake)
                {
                    this.clickArea.CustomTooltipID = 0x101e;
                }
                else
                {
                    this.clickArea.CustomTooltipID = 0;
                }
                base.addControl(this.clickArea);
                base.invalidate();
            }

            public void update()
            {
            }
        }
    }
}

