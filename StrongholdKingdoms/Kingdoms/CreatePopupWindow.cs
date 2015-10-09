namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CreatePopupWindow : Form
    {
        private bool closing;
        private IContainer components;
        private CreatePopupPanel createPopupPanel;

        public CreatePopupWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void CreatePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeCreatePopupWindow();
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

        public void init()
        {
            this.createPopupPanel.init(true);
        }

        private void InitializeComponent()
        {
            this.createPopupPanel = new CreatePopupPanel();
            base.SuspendLayout();
            this.createPopupPanel.Location = new Point(0, 0);
            this.createPopupPanel.Name = "scoutPopupPanel";
            this.createPopupPanel.Size = new Size(700, 0x1bb);
            this.createPopupPanel.StoredGraphics = null;
            this.createPopupPanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(700, 0x1bb);
            base.ControlBox = false;
            base.Controls.Add(this.createPopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(700, 0x1bb);
            this.MinimumSize = new Size(700, 0x1bb);
            base.Name = "ScoutPopupWindow";
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

