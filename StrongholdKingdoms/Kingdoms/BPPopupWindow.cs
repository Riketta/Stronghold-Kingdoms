namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BPPopupWindow : Form
    {
        private bool closing;
        private IContainer components;
        private BPPopupPanel createPopupPanel;

        public BPPopupWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public void attempt1Failed()
        {
            this.createPopupPanel.attempt1Failed();
        }

        private void CreatePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeBPPopupWindow();
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

        public void init(ProfileLoginWindow parent)
        {
            this.createPopupPanel.init(parent);
        }

        private void InitializeComponent()
        {
            this.createPopupPanel = new BPPopupPanel();
            base.SuspendLayout();
            this.createPopupPanel.ClickThru = false;
            this.createPopupPanel.Font = new Font("Microsoft Sans Serif", 8.25f);
            this.createPopupPanel.Location = new Point(0, 0);
            this.createPopupPanel.Name = "createPopupPanel";
            this.createPopupPanel.NoDrawBackground = false;
            this.createPopupPanel.PanelActive = true;
            this.createPopupPanel.SelfDrawBackground = false;
            this.createPopupPanel.Size = new Size(600, 200);
            this.createPopupPanel.StoredGraphics = null;
            this.createPopupPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(600, 200);
            base.ControlBox = false;
            base.Controls.Add(this.createPopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(600, 200);
            this.MinimumSize = new Size(600, 200);
            base.Name = "BPPopupWindow";
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

