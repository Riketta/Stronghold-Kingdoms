namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class GenericReportPanelBasic : CustomSelfDrawPanel, IMailUserInterface
    {
        protected int borderOffset;
        protected CustomSelfDrawPanel.CSDExtendingPanel borderPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
        protected CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
        protected CustomSelfDrawPanel.CSDButton btnDelete = new CustomSelfDrawPanel.CSDButton();
        protected CustomSelfDrawPanel.CSDButton btnForward = new CustomSelfDrawPanel.CSDButton();
        protected CustomSelfDrawPanel.CSDButton btnUtility = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private List<string> forwardRecipients = new List<string>();
        private static bool historyNeedsRefresh = true;
        protected CustomSelfDrawPanel.CSDImage imgBackground = new CustomSelfDrawPanel.CSDImage();
        protected CustomSelfDrawPanel.CSDImage imgFurther = new CustomSelfDrawPanel.CSDImage();
        protected CustomSelfDrawPanel.CSDLabel lblDate = new CustomSelfDrawPanel.CSDLabel();
        protected CustomSelfDrawPanel.CSDLabel lblFurther = new CustomSelfDrawPanel.CSDLabel();
        protected CustomSelfDrawPanel.CSDLabel lblMainText = new CustomSelfDrawPanel.CSDLabel();
        protected CustomSelfDrawPanel.CSDLabel lblSecondaryText = new CustomSelfDrawPanel.CSDLabel();
        protected CustomSelfDrawPanel.CSDLabel lblSubTitle = new CustomSelfDrawPanel.CSDLabel();
        protected IDockableControl m_parent;
        protected GetReport_ReturnType m_returnData;
        private static string[] mailFavourites = null;
        private static string[] mailUsersHistory = null;
        protected NumberFormatInfo nfi = GameEngine.NFI;
        protected long reportID = -1L;
        protected string reportOwner = "";

        public GenericReportPanelBasic()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.Size = new Size(580, 320);
        }

        public void addRecipient(string recipient)
        {
            if ((recipient.Length > 0) && !this.forwardRecipients.Contains(recipient))
            {
                this.forwardRecipients.Add(recipient);
            }
        }

        public void closeClick()
        {
            GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
            this.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }

        public void deleteClick()
        {
            if (this.m_returnData != null)
            {
                GameEngine.Instance.playInterfaceSound("ReportsGeneric_delete");
                if (InterfaceMgr.Instance.deleteReport(this.m_returnData.reportID))
                {
                    this.m_parent.closeControl(true);
                    InterfaceMgr.Instance.reactiveMainWindow();
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

        public static void ForceHistoryRefresh()
        {
            historyNeedsRefresh = true;
        }

        private void forwardClick()
        {
            GameEngine.Instance.playInterfaceSound("ReportsGeneric_forward");
            this.forwardRecipients.Clear();
            base.Enabled = false;
            MailUserPopup popup = new MailUserPopup();
            popup.setAsReportForward();
            popup.setParent(this, MailUsersHistory, MailFavourites, null);
            popup.Show();
        }

        public void forwardReportCallback(ForwardReport_ReturnType returnData)
        {
            if (returnData.Success)
            {
                MyMessageBox.Show(SK.Text("Reports_Forwarded", "Report Forwarded"), SK.Text("Reports_Reports", "Reports"));
                InterfaceMgr.Instance.reactiveMainWindow();
                if (base.ParentForm != null)
                {
                    base.ParentForm.TopMost = true;
                    base.ParentForm.Focus();
                    base.ParentForm.BringToFront();
                    base.ParentForm.TopMost = false;
                }
            }
        }

        public void init(IDockableControl parent, Size size)
        {
            this.init(parent, size, null);
        }

        public virtual void init(IDockableControl parent, Size size, object back)
        {
            this.imgBackground.Image = (Image) back;
            this.imgBackground.Alpha = 0.1f;
            this.m_parent = parent;
            base.Size = size;
            this.borderOffset = 20;
            this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
            this.btnForward.Text.Text = SK.Text("GENERIC_Forward", "Forward");
            this.btnDelete.Text.Text = SK.Text("GENERIC_Delete", "Delete");
            this.lblMainText.Color = ARGBColors.Black;
            this.lblMainText.Position = new Point(this.borderOffset, 3 + this.borderOffset);
            this.lblMainText.Size = new Size(base.Width - (this.borderOffset * 2), 0x39);
            this.lblMainText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.lblMainText.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.lblSubTitle.Color = ARGBColors.Black;
            this.lblSubTitle.Position = new Point(20, this.lblMainText.Rectangle.Bottom);
            this.lblSubTitle.Size = new Size(base.Width - 40, 0x1a);
            this.lblSubTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblSubTitle.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblSecondaryText.Color = ARGBColors.Black;
            this.lblSecondaryText.Position = new Point(20, this.lblSubTitle.Rectangle.Bottom);
            this.lblSecondaryText.Size = new Size(base.Width - 40, 60);
            this.lblSecondaryText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblSecondaryText.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
            this.lblDate.Color = ARGBColors.Black;
            this.lblDate.Position = new Point(0, this.lblSecondaryText.Rectangle.Bottom);
            this.lblDate.Size = new Size(base.Width, 30);
            this.lblDate.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblDate.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.btnClose.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnClose.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnClose.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnClose.setSizeToImage();
            this.btnClose.Position = new Point(((base.Width - this.btnClose.Width) - 5) - this.borderOffset, ((base.Bottom - this.btnClose.Height) - 8) - this.borderOffset);
            this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnClose.TextYOffset = -2;
            this.btnClose.Text.Color = ARGBColors.Black;
            this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Report_Close");
            this.btnClose.Enabled = true;
            this.btnDelete.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnDelete.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnDelete.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnDelete.setSizeToImage();
            this.btnDelete.Position = new Point(this.btnClose.Position.X, (this.btnClose.Position.Y - this.btnDelete.Height) - 3);
            this.btnDelete.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnDelete.TextYOffset = -2;
            this.btnDelete.Text.Color = ARGBColors.Black;
            this.btnDelete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClick), "Report_Delete");
            this.btnDelete.Enabled = true;
            this.btnForward.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnForward.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnForward.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnForward.setSizeToImage();
            this.btnForward.Position = new Point(5 + this.borderOffset, ((base.Bottom - this.btnForward.Height) - 8) - this.borderOffset);
            this.btnForward.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnForward.TextYOffset = -2;
            this.btnForward.Text.Color = ARGBColors.Black;
            this.btnForward.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forwardClick), "Report_Forward");
            this.btnForward.Enabled = true;
            this.btnUtility.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnUtility.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnUtility.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnUtility.setSizeToImage();
            this.btnUtility.Position = new Point(this.btnForward.Position.X, (this.btnForward.Position.Y - this.btnUtility.Height) - 2);
            this.btnUtility.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnUtility.TextYOffset = -2;
            this.btnUtility.Text.Color = ARGBColors.Black;
            this.btnUtility.Enabled = true;
            this.btnUtility.Visible = false;
            this.lblFurther.Text = "";
            this.lblFurther.Color = ARGBColors.Black;
            this.lblFurther.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblFurther.Visible = false;
            this.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblFurther.Position = new Point(this.btnForward.Rectangle.Right + 5, this.btnDelete.Y);
            this.lblFurther.Size = new Size((this.btnDelete.X - this.btnForward.Rectangle.Right) - 10, base.Height - this.lblFurther.Y);
            this.imgFurther.Tile = false;
            this.imgFurther.Visible = false;
            this.initBorder(GFXLibrary.panel_border_top_left, GFXLibrary.panel_border_top, GFXLibrary.panel_border_left);
            if (this.imgBackground.Image != null)
            {
                this.imgBackground.Tile = true;
                this.imgBackground.Position = new Point(0, 0);
                this.imgBackground.Size = base.Size;
                base.addControl(this.imgBackground);
                this.imgBackground.addControl(this.btnClose);
                this.imgBackground.addControl(this.btnForward);
                this.imgBackground.addControl(this.btnUtility);
                this.imgBackground.addControl(this.btnDelete);
                this.imgBackground.addControl(this.lblMainText);
                this.imgBackground.addControl(this.lblSubTitle);
                this.imgBackground.addControl(this.lblSecondaryText);
                this.imgBackground.addControl(this.lblDate);
                this.imgBackground.addControl(this.borderPanel);
            }
            else
            {
                base.addControl(this.btnClose);
                base.addControl(this.btnForward);
                base.addControl(this.btnUtility);
                base.addControl(this.btnDelete);
                base.addControl(this.lblMainText);
                base.addControl(this.lblSubTitle);
                base.addControl(this.lblSecondaryText);
                base.addControl(this.lblDate);
                base.addControl(this.borderPanel);
            }
        }

        public void initBorder(BaseImage cornerImage, BaseImage horizontalSideImage, BaseImage verticalSideImage)
        {
            Image topLeftImage = (Image) cornerImage;
            Image topRightImage = (Image) topLeftImage.Clone();
            topRightImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            ((Image) topLeftImage.Clone()).RotateFlip(RotateFlipType.Rotate180FlipNone);
            ((Image) topLeftImage.Clone()).RotateFlip(RotateFlipType.Rotate180FlipX);
            Image leftImage = (Image) verticalSideImage;
            Image rightImage = (Image) leftImage.Clone();
            rightImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Image topMidImage = (Image) horizontalSideImage;
            Image bottomLeftImage = (Image) topMidImage.Clone();
            bottomLeftImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            this.borderPanel.Size = base.Size;
            this.borderPanel.Create(topLeftImage, topMidImage, topRightImage, leftImage, null, rightImage, bottomLeftImage, bottomLeftImage, topMidImage);
        }

        protected void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
        }

        public static bool isHistoryRefreshNeeded()
        {
            return historyNeedsRefresh;
        }

        public void mailRecipientsCallback(GetMailRecipientsHistory_ReturnType returnData)
        {
            if (returnData.Success)
            {
                MailFavourites = returnData.mailFavourites;
                MailUsersHistory = returnData.mailUsersHistory;
                this.btnForward.Enabled = true;
            }
        }

        public void popupClosed(bool ok)
        {
            if (ok && (this.forwardRecipients.Count > 0))
            {
                RemoteServices.Instance.set_ForwardReport_UserCallBack(new RemoteServices.ForwardReport_UserCallBack(this.forwardReportCallback));
                RemoteServices.Instance.ForwardReport(this.reportID, this.forwardRecipients.ToArray());
                ForceHistoryRefresh();
            }
            base.Enabled = true;
        }

        public virtual void setData(GetReport_ReturnType returnData)
        {
            if (isHistoryRefreshNeeded())
            {
                RemoteServices.Instance.set_GetMailRecipientsHistory_UserCallBack(new RemoteServices.GetMailRecipientsHistory_UserCallBack(this.mailRecipientsCallback));
                RemoteServices.Instance.GetMailRecipientsHistory();
                this.btnForward.Enabled = false;
            }
            else
            {
                this.btnForward.Enabled = true;
            }
            this.m_returnData = returnData;
            this.reportID = returnData.reportID;
            NumberFormatInfo nFI = GameEngine.NFI;
            this.lblDate.Text = returnData.reportTime.ToString();
            this.reportOwner = returnData.reportAboutUser;
            if ((this.reportOwner == null) || (this.reportOwner.Length == 0))
            {
                this.reportOwner = RemoteServices.Instance.UserName;
            }
            this.lblMainText.Text = this.reportOwner;
            this.btnUtility.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilityClick), "Reports_Utility");
        }

        protected void showFurtherInfo()
        {
            this.lblFurther.Visible = true;
            this.imgFurther.Visible = true;
            if (this.imgBackground.Image != null)
            {
                this.imgBackground.addControl(this.lblFurther);
                this.imgBackground.addControl(this.imgFurther);
            }
            else
            {
                base.addControl(this.lblFurther);
                base.addControl(this.imgFurther);
            }
        }

        protected virtual void utilityClick()
        {
        }

        public static string[] MailFavourites
        {
            get
            {
                return mailFavourites;
            }
            set
            {
                mailFavourites = value;
                historyNeedsRefresh = false;
            }
        }

        public static string[] MailUsersHistory
        {
            get
            {
                return mailUsersHistory;
            }
            set
            {
                mailUsersHistory = value;
                historyNeedsRefresh = false;
            }
        }
    }
}

