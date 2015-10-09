namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QuestRewardPopup : MyFormBase
    {
        private BitmapButton btnOK;
        private IContainer components;
        private Label label1;
        private Label lblInfo;
        private Label lblReward;

        public QuestRewardPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("QuestRewardPopup_ok");
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int quest)
        {
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            this.label1.Text = SK.Text("QuestRewardPopup_Reward", "Reward") + " : ";
            this.Text = base.Title = SK.Text("QuestRewardPopup_Quest_Reward", "Quest Reward");
            bool flag = false;
            List<Quests.QuestReward> list = Quests.getQuestRewards(quest, true, GameEngine.NFI);
            string str = "";
            bool flag2 = true;
            foreach (Quests.QuestReward reward in list)
            {
                if (!flag2)
                {
                    str = str + ", ";
                }
                else
                {
                    flag2 = false;
                }
                str = str + reward.explanation;
                if ((reward.type == 0x4e24) || (reward.type == 0x4e26))
                {
                    flag = true;
                }
            }
            this.lblReward.Text = str;
            int tutorialID = Tutorials.getQuestsTutorialStage(quest);
            if (tutorialID == -1)
            {
                this.lblInfo.Text = "";
            }
            else
            {
                this.lblInfo.Text = Tutorials.getTutorialRewardText(tutorialID);
            }
            if (flag)
            {
                PlayCardsWindow.resetRewardCardTimer();
            }
        }

        private void InitializeComponent()
        {
            this.btnOK = new BitmapButton();
            this.label1 = new Label();
            this.lblReward = new Label();
            this.lblInfo = new Label();
            base.SuspendLayout();
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.BorderColor = ARGBColors.DarkBlue;
            this.btnOK.BorderDrawing = true;
            this.btnOK.FocusRectangleEnabled = false;
            this.btnOK.Image = null;
            this.btnOK.ImageBorderColor = ARGBColors.Chocolate;
            this.btnOK.ImageBorderEnabled = true;
            this.btnOK.ImageDropShadow = true;
            this.btnOK.ImageFocused = null;
            this.btnOK.ImageInactive = null;
            this.btnOK.ImageMouseOver = null;
            this.btnOK.ImageNormal = null;
            this.btnOK.ImagePressed = null;
            this.btnOK.InnerBorderColor = ARGBColors.LightGray;
            this.btnOK.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnOK.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnOK.Location = new Point(0x125, 0xce);
            this.btnOK.Name = "btnOK";
            this.btnOK.OffsetPressedContent = true;
            this.btnOK.Padding2 = 5;
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.StretchImage = false;
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.TextDropShadow = false;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label1.AutoSize = true;
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.Location = new Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Reward : ";
            this.lblReward.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblReward.BackColor = ARGBColors.Transparent;
            this.lblReward.Location = new Point(0x47, 50);
            this.lblReward.Name = "lblReward";
            this.lblReward.Size = new Size(0x129, 0x74);
            this.lblReward.TabIndex = 2;
            this.lblReward.Text = "...";
            this.lblInfo.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblInfo.BackColor = ARGBColors.Transparent;
            this.lblInfo.Location = new Point(12, 0x74);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(0x164, 0x53);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "....";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(380, 0xed);
            base.Controls.Add(this.lblInfo);
            base.Controls.Add(this.lblReward);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnOK);
            base.Icon = Resources.shk_icon;
            base.Name = "QuestRewardPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quest Reward";
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.lblReward, 0);
            base.Controls.SetChildIndex(this.lblInfo, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

