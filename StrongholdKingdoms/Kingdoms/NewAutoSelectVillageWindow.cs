namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewAutoSelectVillageWindow : Form
    {
        public bool closing;
        private IContainer components;
        private NewAutoSelectVillagePanel customPanel;

        public NewAutoSelectVillageWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public void closePopup()
        {
            this.customPanel.closePopup();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int countyToJoin)
        {
            this.customPanel.init(countyToJoin, this);
            Form parentForm = InterfaceMgr.Instance.ParentForm;
            base.Location = new Point((parentForm.Location.X + (parentForm.Width / 2)) - (base.Width / 2), (parentForm.Location.Y + (parentForm.Height / 2)) - (base.Height / 2));
        }

        private void InitializeComponent()
        {
            this.customPanel = new NewAutoSelectVillagePanel();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.ClickThru = false;
            this.customPanel.Location = new Point(0, 0);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.Size = new Size(0x271, 0x29c);
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Fuchsia;
            base.ClientSize = new Size(0x271, 0x29c);
            base.ControlBox = false;
            base.Controls.Add(this.customPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(10, 10);
            base.Name = "NewAutoSelectVillageWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "NewAutoSelectVillageWindow";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.FormClosing += new FormClosingEventHandler(this.NewAutoSelectVillageWindow_FormClosing);
            base.ResumeLayout(false);
        }

        private void NewAutoSelectVillageWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                GameEngine.Instance.closeNoVillagePopup(false);
                LoggingOutPopup.open(true, false, false, false, false, false, false, 0, 100, false, false, false, false, false, false, 500, 500, 500, 500, 250);
            }
        }

        public void update()
        {
            this.customPanel.update();
        }
    }
}

