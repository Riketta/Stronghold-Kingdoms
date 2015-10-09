namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WorldSelectPopupWindow : Form
    {
        private bool closing;
        private IContainer components;
        private WorldSelectPopupPanel createPopupPanel;

        public WorldSelectPopupWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void CreatePopupPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeWorldSelectPopupWindow();
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

        public void init(int villageID, bool reset)
        {
            this.createPopupPanel.init(villageID, reset);
        }

        private void InitializeComponent()
        {
            this.createPopupPanel = new WorldSelectPopupPanel();
            base.SuspendLayout();
            this.createPopupPanel.Location = new Point(0, 0);
            this.createPopupPanel.Name = "scoutPopupPanel";
            this.createPopupPanel.Size = new Size(0x338, 0x25d);
            this.createPopupPanel.StoredGraphics = null;
            this.createPopupPanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x338, 0x25d);
            base.ControlBox = false;
            base.Controls.Add(this.createPopupPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(0x338, 0x25d);
            this.MinimumSize = new Size(0x338, 0x25d);
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

