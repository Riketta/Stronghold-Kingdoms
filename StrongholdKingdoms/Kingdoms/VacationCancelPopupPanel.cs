namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VacationCancelPopupPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        public static Image cancelImage;
        public static Image cancelImageOver;
        private CustomSelfDrawPanel.CSDLabel cancelInLabel = new CustomSelfDrawPanel.CSDLabel();
        public static Image closeImage;
        public static Image closeImageOver;
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel expiresInLabel = new CustomSelfDrawPanel.CSDLabel();
        public static Image headerImage;
        private CustomSelfDrawPanel.CSDImage HeaderTitle;
        private int m_secondsLeft;
        private int m_secondsLeftToCancel;
        private DateTime m_startTime = DateTime.Now;
        private string strCancel = SK.Text("Vacation_Cancel", "Cancel Vacation Mode");
        private string strClose = SK.Text("LogoutPanel_Logout", "Logout");
        private string TextCreateHeader = SK.Text("VACATION_CANCEL_HEADER", "Vacation Mode is Active");
        private Color WebButtonblue = Color.FromArgb(0x55, 0x91, 0xcb);
        private Color WebButtonGrey = Color.FromArgb(0xe1, 0xe1, 0xe1);
        private int WebButtonheight = 0x16;
        private int WebButtonRadius = 10;
        private Color WebButtonRed = Color.FromArgb(160, 0, 0);
        private Color WebButtonYellow = Color.FromArgb(0xe1, 0xe1, 0);
        private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
        private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);

        public VacationCancelPopupPanel()
        {
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void AddControlToPanel(CustomSelfDrawPanel.CSDControl c)
        {
            base.addControl(c);
        }

        private void cancelClick()
        {
            MyMessageBox.setForcedForm(base.ParentForm);
            if (MyMessageBox.Show(SK.Text("Vacation_Cancel_MessageBox_Body", "Are you sure you wish to Cancel Vacation Mode?"), SK.Text("Vacation_Cancel_MessageBox_Header", "Cancel Vacation Mode"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.profileLogin.cancelVacationMode();
            }
        }

        private void closeClick()
        {
            Program.profileLogin.btnExit_Click();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int secondsLeft, int secondsLeftToCancel, bool canCancel)
        {
            secondsLeft += 30;
            secondsLeftToCancel += 30;
            this.m_secondsLeft = secondsLeft;
            this.m_secondsLeftToCancel = secondsLeftToCancel;
            base.clearControls();
            base.Controls.Clear();
            this.BackColor = ARGBColors.White;
            this.HeaderTitle = new CustomSelfDrawPanel.CSDImage();
            this.HeaderTitle.Image = this.HeaderImage;
            this.HeaderTitle.Position = new Point((base.Width - this.HeaderTitle.Width) / 2, 0x20);
            this.AddControlToPanel(this.HeaderTitle);
            this.expiresInLabel.Text = SK.Text("VACATION_Expires_In", "Vacation Mode Expires in") + " - " + VillageMap.createBuildTimeString(secondsLeft);
            this.expiresInLabel.Position = new Point(0, 120);
            this.expiresInLabel.Size = new Size(base.Width, 30);
            this.expiresInLabel.Color = ARGBColors.Black;
            this.expiresInLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.expiresInLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.AddControlToPanel(this.expiresInLabel);
            this.cancelButton.ImageNorm = this.CancelImage;
            this.cancelButton.ImageOver = this.CancelImageOver;
            this.cancelButton.Position = new Point(0x6b, 200);
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick));
            this.cancelButton.Visible = false;
            this.AddControlToPanel(this.cancelButton);
            if (canCancel)
            {
                this.cancelButton.Visible = false;
            }
            else
            {
                this.cancelInLabel.Text = SK.Text("VACATION_Cancel_In", "You can cancel in") + " - " + VillageMap.createBuildTimeString(secondsLeftToCancel);
                this.cancelInLabel.Position = new Point(0, 200);
                this.cancelInLabel.Size = new Size(base.Width, 30);
                this.cancelInLabel.Color = ARGBColors.Black;
                this.cancelInLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.cancelInLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.cancelInLabel.Visible = true;
                this.AddControlToPanel(this.cancelInLabel);
            }
            CustomSelfDrawPanel.CSDButton c = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = this.CloseImage,
                ImageOver = this.CloseImageOver,
                Position = new Point(400, 300)
            };
            c.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
            this.AddControlToPanel(c);
            base.Invalidate();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        public void update()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this.m_startTime);
            int secsLeft = this.m_secondsLeft - ((int) span.TotalSeconds);
            if (secsLeft < 0)
            {
                InterfaceMgr.Instance.closeVacationCancelPopupWindow();
            }
            else
            {
                this.expiresInLabel.TextDiffOnly = SK.Text("VACATION_Expires_In", "Vacation Mode Expires in") + " - " + VillageMap.createBuildTimeString(secsLeft);
            }
            int num2 = this.m_secondsLeftToCancel - ((int) span.TotalSeconds);
            if (num2 < 0)
            {
                if (!this.cancelButton.Visible)
                {
                    this.cancelButton.Visible = true;
                    this.cancelInLabel.Visible = false;
                }
            }
            else
            {
                this.cancelInLabel.Text = SK.Text("VACATION_Cancel_In", "You can cancel in") + " - " + VillageMap.createBuildTimeString(num2);
            }
        }

        public Image CancelImage
        {
            get
            {
                if (cancelImage == null)
                {
                    cancelImage = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
                }
                return cancelImage;
            }
        }

        public Image CancelImageOver
        {
            get
            {
                if (cancelImageOver == null)
                {
                    cancelImageOver = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
                }
                return cancelImageOver;
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

        public Image HeaderImage
        {
            get
            {
                if (headerImage == null)
                {
                    headerImage = WebStyleButtonImage.Generate(500, 30, this.TextCreateHeader, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
                }
                return headerImage;
            }
        }
    }
}

