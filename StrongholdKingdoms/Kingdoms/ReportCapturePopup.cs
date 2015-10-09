namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReportCapturePopup : Form
    {
        private IContainer components;
        private int m_mode;
        private ReportCapturePanel reportCapturePanel;
        private ReportDeletePanel reportDeletePanel;

        public ReportCapturePopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int mode)
        {
            this.m_mode = mode;
            if ((mode == 0) || (mode == 1))
            {
                this.reportCapturePanel.Visible = true;
                this.reportDeletePanel.Visible = false;
                this.reportCapturePanel.init(mode, this);
            }
            else if (mode == 2)
            {
                this.reportDeletePanel.Visible = true;
                this.reportCapturePanel.Visible = false;
                this.reportDeletePanel.init(mode, this);
            }
        }

        private void InitializeComponent()
        {
            this.reportCapturePanel = new ReportCapturePanel();
            this.reportDeletePanel = new ReportDeletePanel();
            base.SuspendLayout();
            this.reportCapturePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.reportCapturePanel.BackColor = ARGBColors.Fuchsia;
            this.reportCapturePanel.Location = new Point(0, 0);
            this.reportCapturePanel.Name = "reportCapturePanel";
            this.reportCapturePanel.Size = new Size(0x124, 0x10a);
            this.reportCapturePanel.StoredGraphics = null;
            this.reportCapturePanel.TabIndex = 0;
            this.reportDeletePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.reportDeletePanel.BackColor = ARGBColors.Fuchsia;
            this.reportDeletePanel.Location = new Point(0, 0);
            this.reportDeletePanel.Name = "reportDeletePanel";
            this.reportDeletePanel.Size = new Size(0x124, 0x10a);
            this.reportDeletePanel.StoredGraphics = null;
            this.reportDeletePanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.ClientSize = new Size(0x124, 0x10a);
            base.ControlBox = false;
            base.Controls.Add(this.reportCapturePanel);
            base.Controls.Add(this.reportDeletePanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ReportCapturePopup";
            base.Opacity = 0.95;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Report Capture";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.ResumeLayout(false);
        }

        public void update()
        {
            if ((this.m_mode == 0) || (this.m_mode == 1))
            {
                this.reportCapturePanel.update();
            }
            else if (this.m_mode == 2)
            {
                this.reportDeletePanel.update();
            }
        }
    }
}

