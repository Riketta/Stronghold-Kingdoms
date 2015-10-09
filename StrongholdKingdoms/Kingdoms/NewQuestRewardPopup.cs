namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewQuestRewardPopup : Form
    {
        private IContainer components;
        private NewQuestRewardPanel newQuestRewardPanel;

        public NewQuestRewardPopup()
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

        public void init(int questID, int villageID, NewQuestsPanel parent)
        {
            this.newQuestRewardPanel.Visible = true;
            this.newQuestRewardPanel.init(questID, villageID, parent, this);
        }

        private void InitializeComponent()
        {
            this.newQuestRewardPanel = new NewQuestRewardPanel();
            base.SuspendLayout();
            this.newQuestRewardPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.newQuestRewardPanel.BackColor = ARGBColors.Fuchsia;
            this.newQuestRewardPanel.ClickThru = false;
            this.newQuestRewardPanel.Location = new Point(0, 0);
            this.newQuestRewardPanel.Name = "newQuestRewardPanel";
            this.newQuestRewardPanel.PanelActive = true;
            this.newQuestRewardPanel.Size = new Size(500, 0x1d8);
            this.newQuestRewardPanel.StoredGraphics = null;
            this.newQuestRewardPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.ClientSize = new Size(500, 0x1d8);
            base.ControlBox = false;
            base.Controls.Add(this.newQuestRewardPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "NewQuestRewardPopup";
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
            this.newQuestRewardPanel.update();
        }
    }
}

