namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ConfirmPlayCardPopup : Form
    {
        private bool closing;
        private IContainer components;
        private ConfirmPlayCardPanel confirmPanel;

        public ConfirmPlayCardPopup()
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

        public void init(CardTypes.CardDefinition def, ConfirmPlayCardPanel.CardClickPlayDelegate callback)
        {
            this.confirmPanel.init(def, callback);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(ConfirmPlayCardPopup));
            this.confirmPanel = new ConfirmPlayCardPanel();
            base.SuspendLayout();
            this.confirmPanel.Location = new Point(0, 0);
            this.confirmPanel.Name = "confirmPanel";
            this.confirmPanel.Size = new Size(400, 400);
            this.confirmPanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(400, 400);
            base.ControlBox = false;
            base.Controls.Add(this.confirmPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ConfirmPlayCardPopup";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "ConfirmPlayCardPopup";
            base.FormClosing += new FormClosingEventHandler(this.ConfirmPlayCardPopup_FormClosing);
            base.ResumeLayout(false);
        }

        public void update()
        {
            this.confirmPanel.update();
        }
    }
}

