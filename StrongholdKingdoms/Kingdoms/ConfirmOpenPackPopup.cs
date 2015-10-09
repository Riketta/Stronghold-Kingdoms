namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfirmOpenPackPopup : Form
    {
        private bool closing;
        private IContainer components;
        private ConfirmOpenPackPanel confirmPanel;

        public ConfirmOpenPackPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void ConfirmPlayCardPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeConfirmPlayCardPopup();
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

        public void init(CustomSelfDrawPanel.UICardPack pack, ConfirmOpenPackPanel.CardClickPlayDelegate callback)
        {
            this.confirmPanel.init(pack, callback);
        }

        private void InitializeComponent()
        {
            this.confirmPanel = new ConfirmOpenPackPanel();
            base.SuspendLayout();
            this.confirmPanel.ClickThru = false;
            this.confirmPanel.Location = new Point(0, 0);
            this.confirmPanel.Name = "confirmPanel";
            this.confirmPanel.PanelActive = true;
            this.confirmPanel.Size = new Size(400, 280);
            this.confirmPanel.StoredGraphics = null;
            this.confirmPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(400, 280);
            base.ControlBox = false;
            base.Controls.Add(this.confirmPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ConfirmOpenPackPopup";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "ConfirmPlayCardPopup";
            base.FormClosing += new FormClosingEventHandler(this.ConfirmPlayCardPopup_FormClosing);
            base.ResumeLayout(false);
        }

        public void update()
        {
            this.confirmPanel.update();
        }

        public int Multiple
        {
            get
            {
                return this.confirmPanel.Multiple;
            }
        }
    }
}

