namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TutorialPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton advanceButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage advisor = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton collectRewardButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton continueButton = new CustomSelfDrawPanel.CSDButton();
        private const int EXTRA_WIDTH = 110;
        private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage illustration = new CustomSelfDrawPanel.CSDImage();
        private int lastStageID;
        private int lastTutorialID = -1;
        private static Form m_parent = null;
        private CustomSelfDrawPanel.CSDButton minimizeButton = new CustomSelfDrawPanel.CSDButton();
        private int preClosingTutorialID = -6;
        private CustomSelfDrawPanel.CSDButton quitButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel rewardLabel = new CustomSelfDrawPanel.CSDLabel();
        private static List<int> shownPizzazz = new List<int>();
        private CustomSelfDrawPanel.CSDLabel stageLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();

        public TutorialPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void advanceTutorial()
        {
            GameEngine.Instance.World.advanceTutorial();
            if (GameEngine.Instance.World.getTutorialStage() == 0x68)
            {
                int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
                Point point = GameEngine.Instance.World.getVillageLocation(villageID);
                InterfaceMgr.Instance.changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) point.X, (double) point.Y);
                InterfaceMgr.Instance.selectTutorialArmy();
                this.collectRewardButton.Visible = false;
                this.advanceButton.Visible = false;
            }
        }

        public void cancelTutorialQuit()
        {
            this.setText(this.preClosingTutorialID, m_parent, true);
        }

        public void closeTutorial()
        {
            this.preClosingTutorialID = this.lastTutorialID;
            this.setText(-100, m_parent, true);
        }

        public void closing()
        {
        }

        public void collectReward()
        {
            PizzazzPopupWindow.closePizzazz();
            this.collectRewardButton.Visible = false;
            int quest = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
            QuestsPanel2.Instance.completeQuest(quest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool hasCollectableReward()
        {
            switch (GameEngine.Instance.World.getTutorialStage())
            {
                case 2:
                case 3:
                case 5:
                case 6:
                case 7:
                case 8:
                case 10:
                case 11:
                case 12:
                case 0x66:
                case 0x67:
                    return true;
            }
            return false;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Name = "TutorialPanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void invisiUpdate()
        {
            switch (GameEngine.Instance.World.getTutorialStage())
            {
                case 2:
                case 3:
                case 11:
                    this.updateTutorialArrow();
                    return;
            }
        }

        public bool isNextButtonAvailable(ref bool autoAdvance)
        {
            autoAdvance = false;
            switch (GameEngine.Instance.World.getTutorialStage())
            {
                case 1:
                    return true;

                case 2:
                {
                    if (GameEngine.Instance.Village == null)
                    {
                        return false;
                    }
                    bool flag = GameEngine.Instance.Village.allowTutorialAdvance();
                    if (flag)
                    {
                        autoAdvance = true;
                    }
                    return flag;
                }
                case 3:
                    if (GameEngine.Instance.Village == null)
                    {
                        return false;
                    }
                    return GameEngine.Instance.Village.allowTutorialAdvance();

                case 5:
                    if (GameEngine.Instance.World.UserResearchData.Research_Arts <= 1)
                    {
                        return false;
                    }
                    return true;

                case 6:
                    if (((GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE) || (GameEngine.Instance.Village == null)) || !InterfaceMgr.Instance.isVillageHonourTabOpen())
                    {
                        int quest = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
                        return QuestsPanel2.Instance.isQuestComplete(quest);
                    }
                    return true;

                case 7:
                    if (GameEngine.Instance.World.getRank() <= 0)
                    {
                        return false;
                    }
                    return true;

                case 8:
                    if (!GameEngine.Instance.World.isQuestObjectiveComplete(7))
                    {
                        return false;
                    }
                    return true;

                case 10:
                    if ((GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE) || (GameEngine.Instance.Castle == null))
                    {
                        int num3 = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
                        return QuestsPanel2.Instance.isQuestComplete(num3);
                    }
                    return true;

                case 11:
                    if (GameEngine.Instance.Castle == null)
                    {
                        return false;
                    }
                    return GameEngine.Instance.Castle.isTutorialEnclosedComplete();

                case 12:
                    if (!GameEngine.Instance.World.isQuestObjectiveComplete(11))
                    {
                        return false;
                    }
                    return true;

                case 100:
                    if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE)
                    {
                        return false;
                    }
                    autoAdvance = true;
                    return true;

                case 0x65:
                    if ((GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_QUESTS) || !GameEngine.Instance.World.isQuestComplete(1))
                    {
                        return false;
                    }
                    return true;

                case 0x66:
                    if (!GameEngine.Instance.World.isQuestObjectiveComplete(13))
                    {
                        return false;
                    }
                    return true;

                case 0x67:
                    if (!GameEngine.Instance.World.isQuestObjectiveComplete(14))
                    {
                        return false;
                    }
                    return true;

                case 0x68:
                    return true;

                case 0x69:
                    return true;

                case 110:
                    return true;
            }
            return false;
        }

        public static void logout()
        {
            shownPizzazz.Clear();
            InterfaceMgr.Instance.closeTutorialWindow();
            InterfaceMgr.Instance.closeTutorialArrowWindow();
        }

        public static void minimizeTutorial()
        {
            if ((InterfaceMgr.Instance.getTutorialArrowWindow() != null) && (TutorialArrowWindow.lastParent == m_parent))
            {
                InterfaceMgr.Instance.closeTutorialArrowWindow();
            }
            InterfaceMgr.Instance.closeTutorialWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        public void quitTutorial()
        {
            GameEngine.Instance.World.endTutorial();
            InterfaceMgr.Instance.closeTutorialWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
            InterfaceMgr.Instance.closeTutorialArrowWindow();
        }

        public void replaceWithRewardText()
        {
            this.headerLabel.Text = SK.Text("QuestRewardPopup_Reward", "Reward");
            int tutorialStage = GameEngine.Instance.World.getTutorialStage();
            List<Quests.QuestReward> list = Quests.getQuestRewards(Tutorials.getTutorialQuest(tutorialStage), true, GameEngine.NFI);
            string str = "";
            bool flag = true;
            foreach (Quests.QuestReward reward in list)
            {
                if (!flag)
                {
                    str = str + ", ";
                }
                else
                {
                    flag = false;
                }
                str = str + reward.explanation;
            }
            this.rewardLabel.Text = str;
            this.bodyLabel.Text = Environment.NewLine + Tutorials.getTutorialRewardText(tutorialStage);
            this.bodyLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            if (this.bodyLabel.TextSize.Height > 120)
            {
                this.bodyLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            }
            this.illustration.Image = null;
            switch (tutorialStage)
            {
                case 0:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
                    break;

                case 2:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm5;
                    break;

                case 3:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
                    break;

                case 5:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
                    break;

                case 6:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
                    break;

                case 7:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm4;
                    break;

                case 8:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
                    break;

                case 10:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
                    break;

                case 11:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
                    break;

                case 12:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm7;
                    break;

                case 100:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
                    break;

                case 0x66:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm12;
                    break;

                case 0x67:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm7;
                    break;

                case 0x68:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm7;
                    break;

                case 0x69:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
                    break;
            }
            if (!shownPizzazz.Contains(tutorialStage))
            {
                PizzazzPopupWindow.showPizzazzTutorial(tutorialStage);
                shownPizzazz.Add(tutorialStage);
            }
        }

        public void setText(int tutorialID, Form parent, bool force)
        {
            this.lastTutorialID = tutorialID;
            m_parent = parent;
            base.clearControls();
            this.transparentBackground.Size = base.Size;
            this.transparentBackground.FillColor = Color.FromArgb(0xff, 0, 0xff);
            base.addControl(this.transparentBackground);
            this.background.Position = new Point(0, 0);
            this.background.Image = (Image) GFXLibrary.tutorial_background;
            this.background.Size = new Size(this.background.Image.Width, this.background.Image.Height);
            base.addControl(this.background);
            this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
            int index = 0;
            switch (tutorialID)
            {
                case 0:
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
                    break;

                case 2:
                    index = 2;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm6;
                    break;

                case 3:
                    index = 4;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
                    break;

                case 5:
                    index = 5;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
                    break;

                case 6:
                    index = 6;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm11;
                    break;

                case 7:
                    index = 7;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
                    break;

                case 8:
                    index = 8;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm3;
                    break;

                case 10:
                    index = 11;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm11;
                    break;

                case 11:
                    index = 12;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm8;
                    break;

                case 12:
                    index = 13;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm6;
                    break;

                case -25:
                    index = -1;
                    break;

                case 100:
                    index = 1;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm2;
                    break;

                case 0x65:
                    index = 3;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm5;
                    break;

                case 0x66:
                    index = 9;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm5;
                    break;

                case 0x67:
                    index = 10;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
                    break;

                case 0x68:
                    index = 14;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm10;
                    break;

                case 0x69:
                    index = 15;
                    this.advisor.Image = (Image) GFXLibrary.tutorial_longarm1;
                    break;
            }
            this.advisor.Position = new Point(5, (base.Height - this.advisor.Image.Height) - 3);
            base.addControl(this.advisor);
            try
            {
                this.illustration.Image = (Image) GFXLibrary.tutorial_illustrations[index];
                this.illustration.Position = new Point(0x26a, 0x1f);
                this.illustration.ClipRect = new Rectangle(0, 0, 150, 0xac);
                this.background.addControl(this.illustration);
            }
            catch (Exception)
            {
            }
            if (tutorialID == -25)
            {
                this.headerLabel.Text = SK.Text("QuestRewardPopup_Reward", "Reward");
            }
            else
            {
                this.headerLabel.Text = Tutorials.getTutorialHeaderText(tutorialID);
            }
            this.headerLabel.Color = ARGBColors.Black;
            this.headerLabel.Position = new Point(0, 2);
            this.headerLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.headerLabel.Size = new Size(this.background.Width - 30, 40);
            this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.background.addControl(this.headerLabel);
            this.bodyLabel.Text = Tutorials.getTutorialBodyText(tutorialID);
            this.rewardLabel.Text = "";
            this.rewardLabel.Color = ARGBColors.Black;
            this.rewardLabel.Position = new Point(120, 40);
            this.rewardLabel.Font = FontManager.GetFont("Arial", 13f, FontStyle.Bold);
            this.rewardLabel.Size = new Size(510, 0x8a);
            this.rewardLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.background.addControl(this.rewardLabel);
            this.bodyLabel.Color = ARGBColors.Black;
            this.bodyLabel.Position = new Point(120, 0x20);
            this.bodyLabel.Font = FontManager.GetFont("Arial", 13f, FontStyle.Bold);
            this.bodyLabel.Size = new Size(510, 0x8a);
            this.bodyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.background.addControl(this.bodyLabel);
            if (this.bodyLabel.TextSize.Height > 120)
            {
                this.bodyLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                if (this.bodyLabel.TextSize.Height > 120)
                {
                    this.bodyLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                }
            }
            int lastStageID = this.lastStageID;
            for (int i = 0; i < Tutorials.tutorialOrdering.Length; i++)
            {
                if (Tutorials.tutorialOrdering[i] == tutorialID)
                {
                    lastStageID = i;
                    break;
                }
            }
            this.lastStageID = lastStageID;
            this.stageLabel.Text = ((lastStageID + 1)).ToString() + "/15";
            this.stageLabel.Color = ARGBColors.Black;
            this.stageLabel.Position = new Point(0x174, 7);
            this.stageLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.stageLabel.Size = new Size(0x13e, 0x3a);
            this.stageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.background.addControl(this.stageLabel);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(this.background.Size.Width - 40, 0);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeTutorial), "TutorialPanel_close");
            this.background.addControl(this.closeButton);
            this.minimizeButton.ImageNorm = (Image) GFXLibrary.minimize_Normal;
            this.minimizeButton.ImageOver = (Image) GFXLibrary.minimize_Over;
            this.minimizeButton.Position = new Point((this.background.Size.Width - 40) - 40, 0);
            this.minimizeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(TutorialPanel.minimizeTutorial), "TutorialPanel_minimize");
            this.background.addControl(this.minimizeButton);
            bool autoAdvance = false;
            QuestsPanel2.Instance.downloadedQuestInfo = false;
            if (tutorialID != -100)
            {
                if (!this.hasCollectableReward())
                {
                    this.advanceButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
                    this.advanceButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
                    this.advanceButton.Position = new Point(280, 0xa9);
                    this.advanceButton.Text.Text = SK.Text("QuestRewardPopup_Next", "Next");
                    this.advanceButton.TextYOffset = -3;
                    this.advanceButton.Text.Color = ARGBColors.White;
                    this.advanceButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.advanceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.advanceTutorial), "TutorialPanel_advance");
                    this.advanceButton.Visible = true;
                    this.advanceButton.Enabled = this.isNextButtonAvailable(ref autoAdvance);
                    this.background.addControl(this.advanceButton);
                    this.collectRewardButton.Visible = false;
                }
                else
                {
                    this.advanceButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
                    this.advanceButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
                    this.advanceButton.Position = new Point(380, 0xa9);
                    this.advanceButton.Text.Text = SK.Text("QuestRewardPopup_Next", "Next");
                    this.advanceButton.TextYOffset = -3;
                    this.advanceButton.Text.Color = ARGBColors.White;
                    this.advanceButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.advanceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.advanceTutorial), "TutorialPanel_advance");
                    this.advanceButton.Visible = false;
                    this.advanceButton.Enabled = false;
                    this.background.addControl(this.advanceButton);
                    this.collectRewardButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
                    this.collectRewardButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
                    this.collectRewardButton.Position = new Point(280, 0xa9);
                    this.collectRewardButton.Text.Text = SK.Text("QuestRewardPopup_Collect_Reward", "Collect Reward");
                    this.collectRewardButton.TextYOffset = -3;
                    this.collectRewardButton.Text.Color = ARGBColors.White;
                    if (Program.mySettings.LanguageIdent == "fr")
                    {
                        this.collectRewardButton.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
                    }
                    else
                    {
                        this.collectRewardButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    }
                    this.collectRewardButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.collectReward), "TutorialPanel_collect_reward");
                    this.collectRewardButton.Visible = true;
                    if (tutorialID == -25)
                    {
                        this.collectRewardButton.Enabled = true;
                    }
                    else
                    {
                        this.collectRewardButton.Enabled = false;
                    }
                    this.background.addControl(this.collectRewardButton);
                }
            }
            else
            {
                this.cancelButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
                this.cancelButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
                this.cancelButton.Position = new Point(180, 0xa9);
                this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                this.cancelButton.TextYOffset = -3;
                this.cancelButton.Text.Color = ARGBColors.White;
                this.cancelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelTutorialQuit), "TutorialPanel_cancel");
                this.cancelButton.Visible = true;
                this.background.addControl(this.cancelButton);
                this.quitButton.ImageNorm = (Image) GFXLibrary.tutorial_button_normal;
                this.quitButton.ImageOver = (Image) GFXLibrary.tutorial_button_over;
                this.quitButton.Position = new Point(380, 0xa9);
                this.quitButton.Text.Text = SK.Text("QuestRewardPopup_Exit_Tutorial", "Exit Tutorial");
                this.quitButton.TextYOffset = -3;
                this.quitButton.Text.Color = ARGBColors.White;
                this.quitButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.quitButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.quitTutorial), "TutorialPanel_quit");
                this.quitButton.Visible = true;
                this.background.addControl(this.quitButton);
            }
            if (tutorialID == 0x68)
            {
                this.advanceButton.Text.Text = SK.Text("QuestRewardPopup_Complete_The_Tutorial", "Complete the Tutorial");
                if (Program.mySettings.LanguageIdent.ToLower() == "de")
                {
                    this.advanceButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                }
            }
            base.Invalidate();
            if (parent != null)
            {
                parent.Invalidate();
            }
            if (autoAdvance && !GameEngine.Instance.World.TutorialIsAdvancing())
            {
                this.advanceTutorial();
            }
            else
            {
                this.update();
            }
        }

        public void update()
        {
            bool autoAdvance = false;
            if (this.isNextButtonAvailable(ref autoAdvance))
            {
                if (this.hasCollectableReward())
                {
                    if (!this.advanceButton.Enabled && !this.collectRewardButton.Enabled)
                    {
                        if (!QuestsPanel2.Instance.downloadedQuestInfo)
                        {
                            if (((GameEngine.Instance.World.getTutorialStage() != 7) || ((GameEngine.Instance.World.getRank() > 0) && !RankingsPanel.animating)) && !QuestsPanel2.Instance.downloadingQuestInfo)
                            {
                                QuestsPanel2.Instance.downloadQuestInfo();
                            }
                        }
                        else
                        {
                            int quest = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
                            this.collectRewardButton.Enabled = QuestsPanel2.Instance.isRewardAvailable(quest);
                            if (!this.collectRewardButton.Enabled && !GameEngine.Instance.World.TutorialIsAdvancing())
                            {
                                this.advanceTutorial();
                            }
                            else if (this.collectRewardButton.Enabled)
                            {
                                this.replaceWithRewardText();
                            }
                        }
                    }
                    else
                    {
                        bool enabled = this.collectRewardButton.Enabled;
                        int num3 = Tutorials.getTutorialQuest(GameEngine.Instance.World.getTutorialStage());
                        this.collectRewardButton.Enabled = QuestsPanel2.Instance.isRewardAvailable(num3);
                        if (!this.collectRewardButton.Enabled && !GameEngine.Instance.World.TutorialIsAdvancing())
                        {
                            this.advanceTutorial();
                        }
                        else if (this.collectRewardButton.Enabled && !enabled)
                        {
                            this.replaceWithRewardText();
                        }
                    }
                }
                else
                {
                    this.advanceButton.Enabled = true;
                    if (autoAdvance && !GameEngine.Instance.World.TutorialIsAdvancing())
                    {
                        this.advanceTutorial();
                    }
                }
                if (this.advanceButton.Enabled)
                {
                    if (InterfaceMgr.Instance.ParentForm.WindowState != FormWindowState.Maximized)
                    {
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point((this.advanceButton.Position.X + (this.advanceButton.Width / 2)) + this.background.Position.X, ((this.advanceButton.Position.Y + this.advanceButton.Height) + this.background.Position.Y) - 5), AnchorStyles.Left | AnchorStyles.Top, m_parent);
                    }
                    else
                    {
                        TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point((this.advanceButton.Position.X + this.background.Position.X) - 5, ((this.advanceButton.Position.Y + (this.advanceButton.Height / 2)) + this.background.Position.Y) - 1), AnchorStyles.Left | AnchorStyles.Top, m_parent);
                    }
                }
                else if (this.collectRewardButton.Enabled)
                {
                    if (InterfaceMgr.Instance.ParentForm.WindowState != FormWindowState.Maximized)
                    {
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point((this.collectRewardButton.Position.X + (this.collectRewardButton.Width / 2)) + this.background.Position.X, ((this.collectRewardButton.Position.Y + this.collectRewardButton.Height) + this.background.Position.Y) - 5), AnchorStyles.Left | AnchorStyles.Top, m_parent);
                    }
                    else
                    {
                        TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point((this.collectRewardButton.Position.X + this.background.Position.X) - 5, ((this.collectRewardButton.Position.Y + (this.collectRewardButton.Height / 2)) + this.background.Position.Y) - 1), AnchorStyles.Left | AnchorStyles.Top, m_parent);
                    }
                }
                else
                {
                    InterfaceMgr.Instance.closeTutorialArrowWindow();
                }
            }
            else
            {
                this.advanceButton.Enabled = false;
                this.collectRewardButton.Enabled = false;
                this.updateTutorialArrow();
            }
        }

        public void updateTutorialArrow()
        {
            switch (GameEngine.Instance.World.getTutorialStage())
            {
                case -1:
                case 0x68:
                    InterfaceMgr.Instance.closeTutorialArrowWindow();
                    break;

                case 0:
                case 4:
                case 9:
                case 0x6a:
                case 0x6b:
                case 0x6c:
                case 0x6d:
                case 110:
                    break;

                case 1:
                case 100:
                    if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_VILLAGE)
                    {
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17e, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                        return;
                    }
                    InterfaceMgr.Instance.closeTutorialArrowWindow();
                    return;

                case 2:
                    if (((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE) || (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)) && !InterfaceMgr.Instance.isSelectedVillageACapital())
                    {
                        if (InterfaceMgr.Instance.getVillageTabBar().getCurrentTab() != 0)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17d, 0x79), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        if (!InterfaceMgr.Instance.isVillageMapPanelOnFoodTab())
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x5e, (0xe2 + InterfaceMgr.Instance.getVillageMapPanelBuildTabPos()) - 0x37), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        bool flag = true;
                        if ((GameEngine.Instance.Village != null) && (GameEngine.Instance.Village.findBuildingTypeIncludingConstructing(13) != null))
                        {
                            flag = false;
                        }
                        if (flag)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x39, 0x12d), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        InterfaceMgr.Instance.closeTutorialArrowWindow();
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17e, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 3:
                    if (((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE) || (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)) && !InterfaceMgr.Instance.isSelectedVillageACapital())
                    {
                        if (InterfaceMgr.Instance.getVillageTabBar().getCurrentTab() != 0)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17d, 0x79), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        if (!InterfaceMgr.Instance.isVillageMapPanelOnIndustryTab())
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x83, (0xe2 + InterfaceMgr.Instance.getVillageMapPanelBuildTabPos()) - 0x37), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        bool flag2 = true;
                        bool flag3 = false;
                        if ((GameEngine.Instance.Village != null) && (GameEngine.Instance.Village.findBuildingTypeIncludingConstructing(6) != null))
                        {
                            flag2 = false;
                            if ((GameEngine.Instance.Village.findBuildingType(6) != null) && (GameEngine.Instance.Village.findBuildingTypeIncludingConstructing(7) == null))
                            {
                                flag3 = true;
                            }
                        }
                        if (flag2)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x39, 0x12d), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        if (flag3)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x8d, 0x18d), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        InterfaceMgr.Instance.closeTutorialArrowWindow();
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17e, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 5:
                    if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_RESEARCH)
                    {
                        if (ResearchPanel.TUTORIAL_artsTabPos < -9999)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x25d, 0x14e), AnchorStyles.Left | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        if (GameEngine.Instance.World.UserResearchData.researchingType < 0)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(0x2a, 370 + ResearchPanel.TUTORIAL_artsTabPos), AnchorStyles.Left | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        InterfaceMgr.Instance.closeTutorialArrowWindow();
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x116, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 6:
                    if ((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE) && !InterfaceMgr.Instance.isSelectedVillageACapital())
                    {
                        if (!InterfaceMgr.Instance.isVillageHonourTabOpen())
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(0xc3, 0x97 + InterfaceMgr.Instance.getVillageMapPanelHonourTabPos()), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        InterfaceMgr.Instance.closeTutorialArrowWindow();
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17e, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 7:
                    if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_RANKINGS)
                    {
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x368, 0x89), AnchorStyles.None, InterfaceMgr.Instance.ParentForm);
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0xe4, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 8:
                    if (InterfaceMgr.Instance.isCardPopupOpen())
                    {
                        if (InterfaceMgr.Instance.isConfirmPlayCardPopup())
                        {
                            ConfirmPlayCardPopup popup = InterfaceMgr.Instance.getConfirmPlayCardPopup();
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(300, 340), AnchorStyles.Left | AnchorStyles.Top, popup);
                            return;
                        }
                        PlayCardsWindow parentWindow = (PlayCardsWindow) InterfaceMgr.Instance.getCardWindow();
                        if (parentWindow != null)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x69, 0x125), AnchorStyles.Left | AnchorStyles.Top, parentWindow);
                            return;
                        }
                        InterfaceMgr.Instance.closeTutorialArrowWindow();
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(InterfaceMgr.Instance.getTopLeftMenu().getCardAreaXPos() + 0x57, 80), AnchorStyles.Left | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 10:
                    if (((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE) || (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)) && !InterfaceMgr.Instance.isSelectedVillageACapital())
                    {
                        if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
                        {
                            break;
                        }
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x14b, 0x79), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17e, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 11:
                    if (((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE) || (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)) && !InterfaceMgr.Instance.isSelectedVillageACapital())
                    {
                        if (GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x14b, 0x79), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        if (!GameEngine.Instance.Castle.InBuilderMode)
                        {
                            if (!InterfaceMgr.Instance.TUTORIAL_openedWoodTab())
                            {
                                GameEngine.Instance.Castle.tutorialAutoPlace();
                                TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(120, 0xae), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                                return;
                            }
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x8a, 0x108), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(140, 0x2ad), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17e, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 12:
                    if (InterfaceMgr.Instance.isCardPopupOpen())
                    {
                        if (InterfaceMgr.Instance.isConfirmPlayCardPopup())
                        {
                            ConfirmPlayCardPopup popup2 = InterfaceMgr.Instance.getConfirmPlayCardPopup();
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(300, 340), AnchorStyles.Left | AnchorStyles.Top, popup2);
                            return;
                        }
                        PlayCardsWindow window3 = (PlayCardsWindow) InterfaceMgr.Instance.getCardWindow();
                        GameEngine.Instance.World.countPlayableCards(0);
                        if (window3 != null)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x69, 0x125), AnchorStyles.Left | AnchorStyles.Top, window3);
                            return;
                        }
                        InterfaceMgr.Instance.closeTutorialArrowWindow();
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(InterfaceMgr.Instance.getTopLeftMenu().getCardAreaXPos() + 0x57, 80), AnchorStyles.Left | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 0x65:
                    if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS)
                    {
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(((QuestsPanel2.questXPos + 0x289) + 230) - 12, 0xd0), AnchorStyles.Left | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0xb1, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 0x66:
                    if (InterfaceMgr.Instance.isCardPopupOpen())
                    {
                        PlayCardsWindow window2 = (PlayCardsWindow) InterfaceMgr.Instance.getCardWindow();
                        if (window2 == null)
                        {
                            break;
                        }
                        if (!window2.isCardWindowOnManage())
                        {
                            if ((GameEngine.Instance.World.isBigpointAccount || Program.bigpointInstall) || (Program.aeriaInstall || Program.bigpointPartnerInstall))
                            {
                                TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(0x32d, 0x97), AnchorStyles.Left | AnchorStyles.Top, window2);
                                return;
                            }
                            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(0x32d, 0xce), AnchorStyles.Left | AnchorStyles.Top, window2);
                            return;
                        }
                        if (window2.CardPanelManage.PanelMode == ManageCardsPanel.PANEL_MODE_CASH)
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(740, 70), AnchorStyles.Left | AnchorStyles.Top, window2);
                            return;
                        }
                        if (!window2.CardPanelManage.TUTORIAL_cardsInCart())
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x5c, 0x1cb), AnchorStyles.Left | AnchorStyles.Top, window2);
                            return;
                        }
                        TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x273, 170), AnchorStyles.Left | AnchorStyles.Top, window2);
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(InterfaceMgr.Instance.getTopLeftMenu().getCardAreaXPos() + 0x57, 80), AnchorStyles.Left | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 0x67:
                    if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
                    {
                        if (!InterfaceMgr.Instance.isVillageMapPanelOnPopularityBar())
                        {
                            TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(200, 150), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                            return;
                        }
                        TutorialArrowWindow.CreateTutorialArrowWindow(false, new Point(0x40, 0xcb), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                        return;
                    }
                    TutorialArrowWindow.CreateTutorialArrowWindow(true, new Point(0x17e, 0x55), AnchorStyles.Right | AnchorStyles.Top, InterfaceMgr.Instance.ParentForm);
                    return;

                case 0x69:
                    InterfaceMgr.Instance.closeTutorialArrowWindow();
                    return;

                default:
                    return;
            }
        }
    }
}

