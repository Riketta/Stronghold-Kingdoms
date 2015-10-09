namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class VacationCancelPopupWindow : Form
    {
        private bool closing;
        private IContainer components;
        private VacationCancelPopupPanel createPopupPanel;

        public VacationCancelPopupWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void CreatePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeVacationCancelPopupWindow();
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

        public void init(int secondsLeft, int secondsLeftToCancel, bool canCancel)
        {
            this.createPopupPanel.init(secondsLeft, secondsLeftToCancel, canCancel);
        }

        private void InitializeComponent()
        {
            this.createPopupPanel = new VacationCancelPopupPanel();
            base.SuspendLayout();
            this.createPopupPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.createPopupPanel.ClickThru = false;
            this.createPopupPanel.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.createPopupPanel.Location = new Point(0, 0);
            this.createPopupPanel.Name = "createPopupPanel";
            this.createPopupPanel.PanelActive = true;
            this.createPopupPanel.Size = new Size(0x267, 0x15b);
            this.createPopupPanel.StoredGraphics = null;
            this.createPopupPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x267, 0x15b);
            base.ControlBox = false;
            base.Controls.Add(this.createPopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(0x267, 0x15b);
            this.MinimumSize = new Size(0x267, 0x15b);
            base.Name = "VacationCancelPopupWindow";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "ScoutPopupWindow";
            base.FormClosing += new FormClosingEventHandler(this.CreatePopupPanel_FormClosing);
            base.ResumeLayout(false);
        }

        public void update()
        {
            this.createPopupPanel.update();
        }
    }
}

