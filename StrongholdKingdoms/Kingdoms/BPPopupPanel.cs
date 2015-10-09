namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BPPopupPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        public static Image closeImage;
        public static Image closeImageOver;
        private CustomSelfDrawPanel.CSDButton completeButton = new CustomSelfDrawPanel.CSDButton();
        public static Image completeImage;
        public static Image completeImageOver;
        private IContainer components;
        private bool firstAttemptTried;
        private CustomSelfDrawPanel.CSDLabel headingLabel = new CustomSelfDrawPanel.CSDLabel();
        private ProfileLoginWindow m_parentForm;
        private static bool showOwnWorldsStatus = true;
        private static int showSpecialWorlds = -1;
        private DateTime startedTime = DateTime.MinValue;
        private string strClose = SK.Text("GENERIC_Cancel", "Cancel");
        private string strTryLogin = SK.Text("BIGPOINT_CompleteLogin", "Complete Login");
        private Color WebButtonblue = Color.FromArgb(0x55, 0x91, 0xcb);
        private Color WebButtonGrey = Color.FromArgb(0xe1, 0xe1, 0xe1);
        private int WebButtonheight = 0x16;
        private int WebButtonRadius = 10;
        private Color WebButtonRed = Color.FromArgb(160, 0, 0);
        private Color WebButtonRedFaded = Color.FromArgb(160, 0x60, 0x60);
        private int WebButtonWidth = 120;
        private Color WebButtonYellow = Color.FromArgb(0xe1, 0xe1, 0);
        private Color WebButtonYellow2 = Color.FromArgb(0xff, 0xee, 8);
        private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
        private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);

        public BPPopupPanel()
        {
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void attempt1Failed()
        {
            this.completeButton.Visible = true;
            this.bodyLabel.Visible = true;
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.closeBPPopupWindow();
            if (this.m_parentForm != null)
            {
                this.m_parentForm.BP2_Closed();
            }
        }

        private void completeClick()
        {
            this.closeButton.Enabled = false;
            this.completeButton.Enabled = false;
            if (this.m_parentForm != null)
            {
                this.m_parentForm.bp2_manualLoginAttempt();
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

        public void init(ProfileLoginWindow parentForm)
        {
            this.m_parentForm = parentForm;
            base.clearControls();
            this.BackColor = ARGBColors.White;
            this.headingLabel.Text = SK.Text("BIGPOINT_PleaseLogin", "Please Log into Bigpoint in your Browser");
            this.headingLabel.Position = new Point(0, 10);
            this.headingLabel.Size = new Size(600, 30);
            this.headingLabel.Color = ARGBColors.Black;
            this.headingLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            base.addControl(this.headingLabel);
            this.bodyLabel.Text = SK.Text("BIGPOINT_ClickHere", "Click here once you have completed your Login");
            this.bodyLabel.Position = new Point(0, 80);
            this.bodyLabel.Size = new Size(600, 30);
            this.bodyLabel.Color = ARGBColors.Black;
            this.bodyLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.bodyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.bodyLabel.Visible = false;
            base.addControl(this.bodyLabel);
            this.closeButton.ImageNorm = this.CloseImage;
            this.closeButton.ImageOver = this.CloseImageOver;
            this.closeButton.Position = new Point(370, 160);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "WorldSelectPopupPanel_close");
            base.addControl(this.closeButton);
            this.completeButton.ImageNorm = this.CompleteImage;
            this.completeButton.ImageOver = this.CompleteImageOver;
            this.completeButton.Position = new Point(100, 100);
            this.completeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.completeClick), "WorldSelectPopupPanel_close");
            this.completeButton.Visible = false;
            base.addControl(this.completeButton);
            this.startedTime = DateTime.Now;
            this.firstAttemptTried = false;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        public void update()
        {
            if (!this.firstAttemptTried)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.startedTime);
                if (span.TotalSeconds > 3.0)
                {
                    this.firstAttemptTried = true;
                    if (this.m_parentForm != null)
                    {
                        this.m_parentForm.bp2_autoLoginAttempt();
                    }
                }
            }
        }

        public Image CloseImage
        {
            get
            {
                if (closeImage == null)
                {
                    closeImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
                }
                return closeImage;
            }
        }

        public Image CloseImageOver
        {
            get
            {
                if (closeImageOver == null)
                {
                    closeImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return closeImageOver;
            }
        }

        public Image CompleteImage
        {
            get
            {
                if (completeImage == null)
                {
                    completeImage = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strTryLogin, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
                }
                return completeImage;
            }
        }

        public Image CompleteImageOver
        {
            get
            {
                if (completeImageOver == null)
                {
                    completeImageOver = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strTryLogin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return completeImageOver;
            }
        }
    }
}

