namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReportDeletePanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel captureLabel = new CustomSelfDrawPanel.CSDLabel();
        private IContainer components;
        private int m_mode;
        private CustomSelfDrawPanel.CSDButton okButton = new CustomSelfDrawPanel.CSDButton();

        public ReportDeletePanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int mode, ReportCapturePopup parent)
        {
            this.m_mode = mode;
            base.clearControls();
            this.backgroundImage.Image = (Image) GFXLibrary.popup_background_01;
            this.backgroundImage.Position = new Point(0, 0);
            base.addControl(this.backgroundImage);
            float pointSize = 9f;
            if (Program.mySettings.LanguageIdent == "pl")
            {
                pointSize = 8f;
            }
            if (mode == 2)
            {
                this.captureLabel.Text = SK.Text("Report_Marking_And_Deleting", "Report Marking and Deleting");
            }
            this.captureLabel.Color = ARGBColors.White;
            this.captureLabel.Position = new Point(13, 7);
            this.captureLabel.Size = new Size(0x14f, 20);
            this.captureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.captureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.backgroundImage.addControl(this.captureLabel);
            this.okButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
            this.okButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
            this.okButton.ImageClick = (Image) GFXLibrary.button_blue_01_in;
            this.okButton.Position = new Point(240, 0x145);
            this.okButton.Text.Text = SK.Text("GENERIC_OK", "OK");
            this.okButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.okButton.Text.Color = ARGBColors.Black;
            this.okButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okClicked), "ReportDeletePanel_ok");
            this.backgroundImage.addControl(this.okButton);
            CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                Text = SK.Text("ReportDeleting_Delete_Reports", "Delete Reports"),
                Color = ARGBColors.Black,
                Position = new Point(0, 50),
                Size = new Size(this.backgroundImage.Width, 20),
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
            };
            this.backgroundImage.addControl(control);
            CustomSelfDrawPanel.CSDButton button = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 70)
            };
            button.Text.Text = SK.Text("ReportDeleting_All", "All");
            button.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            button.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
            button.TextYOffset = -3;
            button.Text.Color = ARGBColors.Black;
            button.setClickDelegate(() => ReportsPanel.Instance.deleteAllReports(), "ReportDeletePanel_delete_all");
            this.backgroundImage.addControl(button);
            CustomSelfDrawPanel.CSDButton button2 = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 100)
            };
            button2.Text.Text = SK.Text("ReportDeleting_All_Shown", "All Shown");
            button2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            button2.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
            button2.TextYOffset = -3;
            button2.Text.Color = ARGBColors.Black;
            button2.setClickDelegate(() => ReportsPanel.Instance.deleteShownReports(), "ReportDeletePanel_delete_shown");
            this.backgroundImage.addControl(button2);
            CustomSelfDrawPanel.CSDButton button3 = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 130)
            };
            button3.Text.Text = SK.Text("ReportDeleting_All_Marked", "All Marked");
            button3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            button3.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
            button3.TextYOffset = -3;
            button3.Text.Color = ARGBColors.Black;
            button3.setClickDelegate(() => ReportsPanel.Instance.deleteMarkedReports(), "ReportDeletePanel_delete_marked");
            this.backgroundImage.addControl(button3);
            CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                Text = SK.Text("ReportDeleting_Mark_As_Read", "Mark Reports As Read"),
                Color = ARGBColors.Black,
                Position = new Point(0, 170),
                Size = new Size(this.backgroundImage.Width, 20),
                Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
            };
            this.backgroundImage.addControl(label2);
            CustomSelfDrawPanel.CSDButton button4 = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 190)
            };
            button4.Text.Text = SK.Text("ReportDeleting_All", "All");
            button4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            button4.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
            button4.TextYOffset = -3;
            button4.Text.Color = ARGBColors.Black;
            button4.setClickDelegate(() => ReportsPanel.Instance.markAsReadAllReports(), "ReportDeletePanel_mark_all_as_read");
            this.backgroundImage.addControl(button4);
            CustomSelfDrawPanel.CSDButton button5 = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 220)
            };
            button5.Text.Text = SK.Text("ReportDeleting_All_Shown", "All Shown");
            button5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            button5.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
            button5.TextYOffset = -3;
            button5.Text.Color = ARGBColors.Black;
            button5.setClickDelegate(() => ReportsPanel.Instance.markAsReadShownReports(), "ReportDeletePanel_mark_shown_as_read");
            this.backgroundImage.addControl(button5);
            CustomSelfDrawPanel.CSDButton button6 = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal,
                ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over,
                ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed,
                Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 250)
            };
            button6.Text.Text = SK.Text("ReportDeleting_All_Marked", "All Marked");
            button6.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            button6.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
            button6.TextYOffset = -3;
            button6.Text.Color = ARGBColors.Black;
            button6.setClickDelegate(() => ReportsPanel.Instance.markAsReadMarkedReports(), "ReportDeletePanel_mark_marked_as_read");
            this.backgroundImage.addControl(button6);
            parent.Size = this.backgroundImage.Size;
            base.Invalidate();
            parent.Invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.Name = "ReportDeletePanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void okClicked()
        {
            if (this.m_mode == 2)
            {
                InterfaceMgr.Instance.closeReportCaptureWindow();
                InterfaceMgr.Instance.ParentForm.TopMost = true;
                InterfaceMgr.Instance.ParentForm.TopMost = false;
            }
        }

        public void update()
        {
        }
    }
}

