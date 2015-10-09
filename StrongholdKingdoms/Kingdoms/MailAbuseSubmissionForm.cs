namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MailAbuseSubmissionForm : UserControl, IDockableControl
    {
        private BitmapButton btnClose;
        private BitmapButton btnReport;
        private ComboBox cbReason;
        private IContainer components;
        private DockableControl dockableControl;
        private Label lblAdvice;
        private Label lblBlockReminder;
        private Label lblReason;
        private Label lblTextTitle;
        private Label lblTitle;
        private Panel panel1;
        private MailScreen parentMailscreen;
        private bool reasonSelected;
        private long selectedMailItemID = -1L;
        private long selectedMailThreadID = -1L;
        private string selectedUserName = "";
        private bool summaryProvided;
        private TextBox tbDescription;

        public MailAbuseSubmissionForm()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
            this.lblTextTitle.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
            this.lblTitle.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, FontStyle.Bold);
            this.lblAdvice.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
            this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
            this.btnReport.Text = SK.Text("REPORT_ABUSE_Title", "Report Mail");
            this.lblTitle.Text = SK.Text("REPORT_ABUSE_Title", "Report Mail");
            this.lblAdvice.Text = SK.Text("REPORT_ABUSE_Advice", "If you believe that the contents of this mail need to be investigated by the game administration, you may report it using this form. Please select a reason for doing so from the choices listed below, and provide a summary as to why you feel investigation is required. Please note that being attacked through game mechanics is not a valid reason for reporting, and use of this system for inappropriate reasons will not be tolerated.");
            this.lblReason.Text = SK.Text("REPORT_ABUSE_SelectReason", "Reason for reporting:");
            this.lblTextTitle.Text = SK.Text("REPORT_ABUSE_SummaryTitle", "Summary of the issue (minimum 50 characters):");
            this.lblBlockReminder.Text = SK.Text("REPORT_ABUSE_BlockReminder", "Please note that, on sending the report, the user in question will automatically be added to your 'Blocked Users' list");
            this.cbReason.Items.Add(SK.Text("REPORT_ABUSE_PleaseSelect", "Please select"));
            this.cbReason.Items.Add(SK.Text("REPORT_ABUSE_Inappropriate", "Inappropriate Conduct"));
            this.cbReason.Items.Add(SK.Text("REPORT_ABUSE_Personal", "Threats outside the game"));
            this.cbReason.Items.Add(SK.Text("REPORT_ABUSE_Spam", "Advertisements or Links"));
            this.cbReason.Items.Add(SK.Text("REPORT_ABUSE_Scam", "Scam or Phishing Attempt"));
            this.cbReason.Items.Add(SK.Text("REPORT_ABUSE_Proclamation", "Offensive Proclamation"));
            this.cbReason.SelectedIndex = 0;
            this.btnReport.Enabled = false;
            this.cbReason.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
            this.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (this.reasonSelected && this.summaryProvided)
            {
                RemoteServices.Instance.set_ReportMail_UserCallBack(new RemoteServices.ReportMail_UserCallBack(this.parentMailscreen.ReportMailCallback));
                RemoteServices.Instance.ReportMail(this.selectedMailItemID, this.selectedMailThreadID, this.cbReason.SelectedItem.ToString(), this.tbDescription.Text);
                GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
                this.closeControl(true);
                InterfaceMgr.Instance.reactiveMainWindow();
                if (this.selectedUserName.Length > 0)
                {
                    MailUserBlockPopup popup = new MailUserBlockPopup();
                    popup.init(this.parentMailscreen, this.selectedUserName);
                    popup.ShowDialog(InterfaceMgr.Instance.ParentForm);
                    popup.Dispose();
                }
            }
        }

        private void cbReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbReason.SelectedIndex != 0)
            {
                this.reasonSelected = true;
                if (this.summaryProvided)
                {
                    this.btnReport.Enabled = true;
                }
            }
            else
            {
                this.reasonSelected = false;
                this.btnReport.Enabled = false;
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

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblAdvice = new Label();
            this.panel1 = new Panel();
            this.lblTitle = new Label();
            this.lblBlockReminder = new Label();
            this.tbDescription = new TextBox();
            this.lblTextTitle = new Label();
            this.cbReason = new ComboBox();
            this.lblReason = new Label();
            this.btnReport = new BitmapButton();
            this.btnClose = new BitmapButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblAdvice.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblAdvice.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblAdvice.Location = new Point(12, 0x1c);
            this.lblAdvice.Name = "lblAdvice";
            this.lblAdvice.Size = new Size(0x1de, 0x90);
            this.lblAdvice.TabIndex = 0;
            this.lblAdvice.Text = "Nothing";
            this.lblAdvice.TextAlign = ContentAlignment.MiddleCenter;
            this.panel1.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.lblAdvice);
            this.panel1.Location = new Point(0x1c, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1fb, 0xb0);
            this.panel1.TabIndex = 2;
            this.lblTitle.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.Location = new Point(3, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0x1f1, 0x16);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Nothing";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblBlockReminder.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblBlockReminder.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblBlockReminder.Location = new Point(0x33, 0x22b);
            this.lblBlockReminder.Name = "lblBlockReminder";
            this.lblBlockReminder.Size = new Size(0x1cd, 0x34);
            this.lblBlockReminder.TabIndex = 4;
            this.lblBlockReminder.Text = "Please note that, on sending the report, the user in question will automatically be added to your \"Block Users\" list";
            this.lblBlockReminder.TextAlign = ContentAlignment.MiddleCenter;
            this.tbDescription.BackColor = ARGBColors.White;
            this.tbDescription.Location = new Point(0x20, 270);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ScrollBars = ScrollBars.Vertical;
            this.tbDescription.Size = new Size(0x1f2, 0x11a);
            this.tbDescription.TabIndex = 7;
            this.tbDescription.TextChanged += new EventHandler(this.tbDescription_TextChanged);
            this.lblTextTitle.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblTextTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblTextTitle.Location = new Point(14, 0xf7);
            this.lblTextTitle.Name = "lblTextTitle";
            this.lblTextTitle.Size = new Size(0x217, 20);
            this.lblTextTitle.TabIndex = 8;
            this.lblTextTitle.Text = "Summary of the issue:";
            this.lblTextTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.cbReason.FormattingEnabled = true;
            this.cbReason.Location = new Point(0xcf, 0xca);
            this.cbReason.Name = "cbReason";
            this.cbReason.Size = new Size(0xec, 0x15);
            this.cbReason.TabIndex = 9;
            this.cbReason.SelectedIndexChanged += new EventHandler(this.cbReason_SelectedIndexChanged);
            this.lblReason.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblReason.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblReason.Location = new Point(0x21, 0xcb);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new Size(0xa8, 20);
            this.lblReason.TabIndex = 10;
            this.lblReason.Text = "Reason for reporting:";
            this.lblReason.TextAlign = ContentAlignment.MiddleRight;
            this.btnReport.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnReport.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.btnReport.BorderColor = ARGBColors.DarkBlue;
            this.btnReport.BorderDrawing = true;
            this.btnReport.Enabled = false;
            this.btnReport.FocusRectangleEnabled = false;
            this.btnReport.Image = null;
            this.btnReport.ImageBorderColor = ARGBColors.Chocolate;
            this.btnReport.ImageBorderEnabled = true;
            this.btnReport.ImageDropShadow = true;
            this.btnReport.ImageFocused = null;
            this.btnReport.ImageInactive = null;
            this.btnReport.ImageMouseOver = null;
            this.btnReport.ImageNormal = null;
            this.btnReport.ImagePressed = null;
            this.btnReport.InnerBorderColor = ARGBColors.LightGray;
            this.btnReport.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnReport.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnReport.Location = new Point(15, 0x26f);
            this.btnReport.Name = "btnReport";
            this.btnReport.OffsetPressedContent = true;
            this.btnReport.Padding2 = 5;
            this.btnReport.Size = new Size(0x6f, 0x17);
            this.btnReport.StretchImage = false;
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Report Mail";
            this.btnReport.TextDropShadow = false;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new EventHandler(this.btnReport_Click);
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            this.btnClose.BorderColor = ARGBColors.DarkBlue;
            this.btnClose.BorderDrawing = true;
            this.btnClose.FocusRectangleEnabled = false;
            this.btnClose.Image = null;
            this.btnClose.ImageBorderColor = ARGBColors.Chocolate;
            this.btnClose.ImageBorderEnabled = true;
            this.btnClose.ImageDropShadow = true;
            this.btnClose.ImageFocused = null;
            this.btnClose.ImageInactive = null;
            this.btnClose.ImageMouseOver = null;
            this.btnClose.ImageNormal = null;
            this.btnClose.ImagePressed = null;
            this.btnClose.InnerBorderColor = ARGBColors.LightGray;
            this.btnClose.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnClose.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnClose.Location = new Point(0x1d5, 0x26f);
            this.btnClose.Name = "btnClose";
            this.btnClose.OffsetPressedContent = true;
            this.btnClose.Padding2 = 5;
            this.btnClose.Size = new Size(0x4c, 0x17);
            this.btnClose.StretchImage = false;
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.TextDropShadow = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Controls.Add(this.cbReason);
            base.Controls.Add(this.lblReason);
            base.Controls.Add(this.lblTextTitle);
            base.Controls.Add(this.tbDescription);
            base.Controls.Add(this.btnReport);
            base.Controls.Add(this.lblBlockReminder);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.panel1);
            base.Name = "MailAbuseSubmissionForm";
            base.Size = new Size(0x233, 0x293);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public void InitReportData(MailScreen parentScreen, long itemID, long threadID, string userName)
        {
            this.parentMailscreen = parentScreen;
            this.selectedMailItemID = itemID;
            this.selectedMailThreadID = threadID;
            this.selectedUserName = userName;
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void tbDescription_TextChanged(object sender, EventArgs e)
        {
            if (this.tbDescription.Text.Length >= 50)
            {
                this.summaryProvided = true;
                if (this.reasonSelected)
                {
                    this.btnReport.Enabled = true;
                }
            }
            else
            {
                this.summaryProvided = false;
                this.btnReport.Enabled = false;
            }
        }
    }
}

