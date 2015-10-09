namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewQuestsCompletedWindow : Form
    {
        private IContainer components;
        private NewQuestsCompletedPanel newQuestsCompletedPanel;

        public NewQuestsCompletedWindow()
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

        public void init(Form parent, List<int> quests, bool forQuestList, string questTag, int questID)
        {
            if (parent != null)
            {
                base.Location = new Point(parent.Location.X + ((parent.Width - base.Width) / 2), parent.Location.Y + ((parent.Height - base.Height) / 2));
            }
            this.newQuestsCompletedPanel.setCompletedQuests(quests);
            this.newQuestsCompletedPanel.init(this, forQuestList, questTag, questID);
        }

        private void InitializeComponent()
        {
            this.newQuestsCompletedPanel = new NewQuestsCompletedPanel();
            base.SuspendLayout();
            this.newQuestsCompletedPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.newQuestsCompletedPanel.ClickThru = false;
            this.newQuestsCompletedPanel.Location = new Point(0, 0);
            this.newQuestsCompletedPanel.Name = "newQuestsCompletedPanel";
            this.newQuestsCompletedPanel.PanelActive = true;
            this.newQuestsCompletedPanel.Size = new Size(0x1e9, 350);
            this.newQuestsCompletedPanel.StoredGraphics = null;
            this.newQuestsCompletedPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x1e9, 350);
            base.Controls.Add(this.newQuestsCompletedPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            this.MaximumSize = new Size(700, 0x1bb);
            this.MinimumSize = new Size(0x1db, 350);
            base.Name = "NewQuestsCompletedWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.ResumeLayout(false);
        }
    }
}

