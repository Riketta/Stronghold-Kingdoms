namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewQuestsCompletedPanel : CustomSelfDrawPanel
    {
        private int _questID;
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private List<int> completedQuests;
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
        private bool isQuestList = true;
        private Form m_parent;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDImage overlayImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea questsScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar questsScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private string questText;

        public NewQuestsCompletedPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            this.m_parent.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(Form parent, bool forQuestList, string questTag, int questID)
        {
            this.m_parent = parent;
            base.clearControls();
            this.isQuestList = forQuestList;
            this.questText = questTag;
            this._questID = questID;
            this.mainBackgroundImage.Image = (Image) GFXLibrary.mail2_mail_panel_middle_middle;
            this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), base.Size);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            base.addControl(this.mainBackgroundImage);
            this.questsScrollArea.Position = new Point(0x1b, 30);
            this.questsScrollArea.Size = new Size(0x199, 0x130);
            this.questsScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x199, 0x130));
            this.mainBackgroundImage.addControl(this.questsScrollArea);
            this.questsScrollBar.Position = new Point(0x1bc, 0x23);
            this.questsScrollBar.Size = new Size(0x18, 0x126);
            this.mainBackgroundImage.addControl(this.questsScrollBar);
            this.questsScrollBar.Value = 0;
            this.questsScrollBar.Max = 100;
            this.questsScrollBar.NumVisibleLines = 0x19;
            this.questsScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.questsScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.mouseWheelOverlay.Position = this.questsScrollArea.Position;
            this.mouseWheelOverlay.Size = this.questsScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
            this.overlayImage.Image = (Image) GFXLibrary.char_achievementOverlay;
            this.overlayImage.Position = new Point(0, 0);
            this.mainBackgroundImage.addControl(this.overlayImage);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(base.Width - 40, 0);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "NewQuestsCompletedPanel_close");
            this.overlayImage.addControl(this.closeButton);
            if (forQuestList)
            {
                this.headerLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                this.headerLabel.Text = SK.Text("QUESTS_CompletedQuests", "Completed Quests");
            }
            else
            {
                this.headerLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                this.headerLabel.Text = SK.NoStoreText("Z_QUESTS_" + this.questText) + " - ";
                this.headerLabel.Text = this.headerLabel.Text + SK.Text("QUESTS_FurtherDetails", "Further Information");
            }
            this.headerLabel.Position = new Point(0, 0);
            this.headerLabel.Size = new Size(base.Width, 30);
            this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.headerLabel.Color = ARGBColors.White;
            this.headerLabel.DropShadowColor = ARGBColors.Black;
            this.overlayImage.addControl(this.headerLabel);
            this.rebuild();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void mouseWheelMoved(int delta)
        {
            if (delta < 0)
            {
                this.questsScrollBar.scrollDown(40);
            }
            else if (delta > 0)
            {
                this.questsScrollBar.scrollUp(40);
            }
        }

        public void rebuild()
        {
            int[] completedQuests = null;
            int y = 0;
            this.questsScrollArea.clearControls();
            if (this.isQuestList)
            {
                if (this.completedQuests == null)
                {
                    NewQuestsData data = GameEngine.Instance.World.getNewQuestData();
                    if (data == null)
                    {
                        return;
                    }
                    completedQuests = data.completedQuests;
                }
                else
                {
                    completedQuests = this.completedQuests.ToArray();
                }
                for (int i = 0; i < completedQuests.Length; i++)
                {
                    int quest = completedQuests[i];
                    NewQuestLine control = new NewQuestLine();
                    if (y != 0)
                    {
                        y += 5;
                    }
                    control.Position = new Point(0, y);
                    control.init(quest, i);
                    this.questsScrollArea.addControl(control);
                    y += control.Height;
                }
            }
            else
            {
                CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel();
                switch (this._questID)
                {
                    case 0x22:
                    case 0x30:
                    case 4:
                    case 0x10:
                    case 0x65:
                    case 0x7a:
                    case 0x40:
                    case 0x54:
                        if ((!GameEngine.Instance.World.isBigpointAccount && !Program.aeriaInstall) && (!Program.bigpointPartnerInstall && !Program.arcInstall))
                        {
                            label.Text = SK.Text("QUESTS_IAF_Help1", "Learn about how inviting your friends to the game can earn you up to $160 worth of crowns.");
                        }
                        else
                        {
                            label.Text = SK.Text("QUESTS_IAF_Help2", "Why not invite a friend to play Kingdoms? They can fight alongside you and you will help us to further develop the game. ");
                        }
                        break;

                    default:
                        label.Text = SK.NoStoreText("Z_QUEST_HELP_" + this.questText);
                        break;
                }
                label.Color = ARGBColors.Black;
                label.Position = new Point(0x24, 30);
                label.Size = new Size(this.questsScrollArea.Width, this.questsScrollArea.Height);
                label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.mainBackgroundImage.addControl(label);
            }
            this.questsScrollArea.Size = new Size(this.questsScrollArea.Width, y);
            if (y < this.questsScrollBar.Height)
            {
                this.questsScrollBar.Visible = false;
            }
            else
            {
                this.questsScrollBar.Visible = true;
                this.questsScrollBar.NumVisibleLines = this.questsScrollBar.Height;
                this.questsScrollBar.Max = y - this.questsScrollBar.Height;
            }
            this.questsScrollArea.invalidate();
            this.questsScrollBar.invalidate();
            this.mainBackgroundImage.invalidate();
        }

        public void setCompletedQuests(List<int> quests)
        {
            this.completedQuests = quests;
        }

        private void wallScrollBarMoved()
        {
            int y = this.questsScrollBar.Value;
            this.questsScrollArea.Position = new Point(this.questsScrollArea.X, 30 - y);
            this.questsScrollArea.ClipRect = new Rectangle(this.questsScrollArea.ClipRect.X, y, this.questsScrollArea.ClipRect.Width, this.questsScrollArea.ClipRect.Height);
            this.mainBackgroundImage.invalidate();
        }

        public class NewQuestLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDImage questImage = new CustomSelfDrawPanel.CSDImage();

            public void init(int quest, int position)
            {
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.quest_screen_bar1;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.quest_screen_bar2;
                }
                this.backgroundImage.Position = new Point(60, 11);
                base.addControl(this.backgroundImage);
                NewQuests.NewQuestDefinition definition = NewQuests.getNewQuestDef(quest);
                this.Size = new Size(0x1bc, 60);
                this.questImage.Image = (Image) GFXLibrary.quest_icons[Math.Min(definition.questType, GFXLibrary.quest_icons.Length - 1)];
                this.questImage.Position = new Point(0, 6);
                base.addControl(this.questImage);
                this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + definition.tagString);
                this.lblQuestName.Color = ARGBColors.Black;
                this.lblQuestName.Position = new Point(9, 0);
                this.lblQuestName.Size = new Size(400, this.backgroundImage.Height);
                this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblQuestName);
            }
        }
    }
}

