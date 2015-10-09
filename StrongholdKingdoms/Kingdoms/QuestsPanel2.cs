namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using Stronghold.AuthClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class QuestsPanel2 : CustomSelfDrawPanel, IDockableControl
    {
        private ArmyComparer armyComparer = new ArmyComparer();
        private List<WorldMap.LocalArmyData> armyList = new List<WorldMap.LocalArmyData>();
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private int blockYSize;
        private bool[] completedActiveQuests;
        private DateTime completedQuestTime = DateTime.MinValue;
        private IContainer components;
        private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage divider6Image = new CustomSelfDrawPanel.CSDImage();
        private DockableControl dockableControl;
        public bool downloadedQuestInfo;
        public bool downloadingQuestInfo;
        private Panel focusPanel;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private bool inCompleteQuest;
        public static QuestsPanel2 Instance;
        private List<QuestLine> lineList = new List<QuestLine>();
        private CustomSelfDrawPanel.CSDLabel objectivesLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea outgoingScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar outgoingScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        public static int questXPos;
        private CustomSelfDrawPanel.CSDLabel rewardLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel statusLabel = new CustomSelfDrawPanel.CSDLabel();

        public QuestsPanel2()
        {
            Instance = this;
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
            base.clearControls();
            this.closing();
        }

        public void closing()
        {
            InterfaceMgr.Instance.closeDonatePopup();
        }

        public void completeQuest(int quest)
        {
            if (this.inCompleteQuest)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.completedQuestTime);
                if (span.TotalMinutes < 2.0)
                {
                    return;
                }
            }
            this.completedQuestTime = DateTime.Now;
            this.inCompleteQuest = true;
            RemoteServices.Instance.set_CompleteQuest_UserCallBack(new RemoteServices.CompleteQuest_UserCallBack(this.CompleteQuestCallback));
            RemoteServices.Instance.CompleteQuest(quest);
        }

        public void CompleteQuestCallback(CompleteQuest_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.setTutorialInfo(returnData.m_tutorialInfo);
                this.completedActiveQuests = returnData.m_preCompletedQuests;
                if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_QUESTS)
                {
                    this.rebuild();
                }
                if (returnData.questCompleted >= 0)
                {
                    List<Quests.QuestReward> list = Quests.getQuestRewards(returnData.questCompleted, false, null);
                    foreach (Quests.QuestReward reward in list)
                    {
                        List<int> list2;
                        CardTypes.PremiumToken token;
                        switch (reward.type)
                        {
                            case 0x4e20:
                            {
                                list2 = GameEngine.Instance.World.getUserVillageIDList();
                                if (list2.Count == 1)
                                {
                                    break;
                                }
                                GameEngine.Instance.flushVillages();
                                if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_VILLAGE)
                                {
                                    GameEngine.Instance.downloadCurrentVillage();
                                }
                                continue;
                            }
                            case 0x4e21:
                            {
                                GameEngine.Instance.World.addGold((double) reward.amount);
                                continue;
                            }
                            case 0x4e22:
                            {
                                GameEngine.Instance.World.addHonour((double) reward.amount);
                                continue;
                            }
                            case 0x4e23:
                            {
                                GameEngine.Instance.World.addResearchPoints(reward.amount);
                                continue;
                            }
                            case 0x4e24:
                            {
                                if (returnData.cardAdded >= 0)
                                {
                                    if (reward.data == 0x1011)
                                    {
                                        goto Label_01D0;
                                    }
                                    GameEngine.Instance.World.addProfileCard(returnData.cardAdded, CardTypes.getStringFromCard(reward.data));
                                }
                                continue;
                            }
                            case 0x4e25:
                            {
                                continue;
                            }
                            case 0x4e26:
                            {
                                WorldMap world = GameEngine.Instance.World;
                                world.FakeCardPoints += reward.amount;
                                continue;
                            }
                            default:
                            {
                                continue;
                            }
                        }
                        if (list2.Count == 1)
                        {
                            VillageMap map = GameEngine.Instance.getVillage(list2[0]);
                            if (map != null)
                            {
                                map.addResources(reward.data, reward.amount);
                            }
                        }
                        continue;
                    Label_01D0:
                        token = new CardTypes.PremiumToken();
                        token.Reward = 1;
                        token.UserPremiumTokenID = returnData.cardAdded;
                        token.WorldID = RemoteServices.Instance.ProfileWorldID;
                        token.Type = 0x1011;
                        bool flag = false;
                        if (GameEngine.Instance.World.UserCardData.premiumCard <= 0)
                        {
                            XmlRpcCardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
                            XmlRpcCardsRequest req = new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")) {
                                WorldID = RemoteServices.Instance.ProfileWorldID.ToString(),
                                UserCardID = returnData.cardAdded.ToString()
                            };
                            if (token.Type == 0x1010)
                            {
                                req.CardString = "CARDTYPE_PREMIUM";
                            }
                            if (token.Type == 0x1011)
                            {
                                req.CardString = "CARDTYPE_PREMIUM2";
                            }
                            if (token.Type == 0x1012)
                            {
                                req.CardString = "CARDTYPE_PREMIUM30";
                            }
                            XmlRpcCardsResponse response = provider.playPremium(req, 0x1770);
                            if (response.SuccessCode != 1)
                            {
                                flag = true;
                                MyMessageBox.Show(response.Message, "Error playing premium");
                            }
                            else
                            {
                                GameEngine.Instance.World.CardPlayed(-1, token.Type, -1);
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            GameEngine.Instance.World.ProfilePremiumTokens.Add(returnData.cardAdded, token);
                        }
                    }
                    bool flag2 = false;
                    foreach (Quests.QuestReward reward2 in list)
                    {
                        if ((reward2.type == 0x4e24) || (reward2.type == 0x4e26))
                        {
                            flag2 = true;
                        }
                    }
                    if (flag2)
                    {
                        PlayCardsWindow.resetRewardCardTimer();
                    }
                }
            }
            this.inCompleteQuest = false;
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void downloadQuestInfo()
        {
            this.downloadedQuestInfo = false;
            this.downloadingQuestInfo = true;
            this.completedActiveQuests = null;
            RemoteServices.Instance.set_GetQuestStatus_UserCallBack(new RemoteServices.GetQuestStatus_UserCallBack(this.GetQuestStatusCallback));
            RemoteServices.Instance.GetQuestStatus();
        }

        public void GetQuestStatusCallback(GetQuestStatus_ReturnType returnData)
        {
            this.downloadedQuestInfo = true;
            this.downloadingQuestInfo = false;
            if (returnData.Success)
            {
                GameEngine.Instance.World.setTutorialInfo(returnData.m_tutorialInfo);
                this.completedActiveQuests = returnData.m_preCompletedQuests;
                this.rebuild();
            }
        }

        public void init(bool resized)
        {
            int height = base.Height;
            questXPos = base.Location.X;
            Instance = this;
            base.clearControls();
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            this.backgroundImage.Size = new Size(base.Width, height - 40);
            this.backgroundImage.Position = new Point(0, 40);
            base.addControl(this.backgroundImage);
            this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            this.parishNameLabel.Text = SK.Text("QuestPanel_TutorialQuests", "Tutorial Quests");
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.blockYSize = (height - 40) - 0x38;
            this.headerLabelsImage.Size = new Size((base.Width - 0x19) - 0x17, 0x1c);
            this.headerLabelsImage.Position = new Point(0x19, 5);
            this.backgroundImage.addControl(this.headerLabelsImage);
            this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
            this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
            this.divider1Image.Position = new Point(490, 0);
            this.headerLabelsImage.addControl(this.divider1Image);
            this.objectivesLabel.Text = SK.Text("QuestsPanel_Objectives", "Objectives");
            this.objectivesLabel.Color = ARGBColors.Black;
            this.objectivesLabel.Position = new Point(12, -2);
            this.objectivesLabel.Size = new Size(0x143, this.headerLabelsImage.Height);
            this.objectivesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.objectivesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.objectivesLabel);
            this.statusLabel.Text = SK.Text("QuestsPanel_Status", "Status");
            this.statusLabel.Color = ARGBColors.Black;
            this.statusLabel.Position = new Point(0x1f0, -2);
            this.statusLabel.Size = new Size(0xdf, this.headerLabelsImage.Height);
            this.statusLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.statusLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerLabelsImage.addControl(this.statusLabel);
            this.outgoingScrollArea.Position = new Point(0x19, 40);
            this.outgoingScrollArea.Size = new Size(0x393, (this.blockYSize - 40) - 10);
            this.outgoingScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x393, (this.blockYSize - 40) - 10));
            this.backgroundImage.addControl(this.outgoingScrollArea);
            int num2 = this.outgoingScrollBar.Value;
            this.outgoingScrollBar.Position = new Point(0x3af, 40);
            this.outgoingScrollBar.Size = new Size(0x18, (this.blockYSize - 40) - 10);
            this.backgroundImage.addControl(this.outgoingScrollBar);
            this.outgoingScrollBar.Value = 0;
            this.outgoingScrollBar.Max = 100;
            this.outgoingScrollBar.NumVisibleLines = 0x19;
            this.outgoingScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.outgoingScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            if (!resized)
            {
                this.downloadQuestInfo();
            }
            this.rebuild();
            if (resized)
            {
                this.outgoingScrollBar.Value = num2;
            }
        }

        private void InitializeComponent()
        {
            this.focusPanel = new Panel();
            base.SuspendLayout();
            this.focusPanel.BackColor = ARGBColors.Transparent;
            this.focusPanel.ForeColor = ARGBColors.Transparent;
            this.focusPanel.Location = new Point(0x3dc, 3);
            this.focusPanel.Name = "focusPanel";
            this.focusPanel.Size = new Size(1, 1);
            this.focusPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.focusPanel);
            this.MaximumSize = new Size(0x3e0, 0x2710);
            this.MinimumSize = new Size(0x3e0, 0x236);
            base.Name = "QuestsPanel2";
            base.Size = new Size(0x3e0, 0x236);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isQuestComplete(int quest)
        {
            int[] numArray = GameEngine.Instance.World.getCompletedQuests();
            for (int i = 0; i < numArray.Length; i++)
            {
                if (numArray[i] == quest)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isRewardAvailable(int quest)
        {
            int[] numArray = GameEngine.Instance.World.getActiveQuests();
            for (int i = 0; i < numArray.Length; i++)
            {
                if (((this.completedActiveQuests != null) && (i < this.completedActiveQuests.Length)) && (this.completedActiveQuests[i] && (numArray[i] == quest)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void logout()
        {
        }

        public void rebuild()
        {
            int[] numArray = GameEngine.Instance.World.getActiveQuests();
            this.outgoingScrollArea.clearControls();
            int y = 0;
            this.lineList.Clear();
            double num1 = DXTimer.GetCurrentMilliseconds() / 1000.0;
            for (int i = 0; i < numArray.Length; i++)
            {
                int quest = numArray[i];
                int completeState = 0;
                if ((this.completedActiveQuests != null) && (i < this.completedActiveQuests.Length))
                {
                    if (this.completedActiveQuests[i])
                    {
                        completeState = 2;
                    }
                    else
                    {
                        completeState = 1;
                    }
                }
                QuestLine control = new QuestLine();
                if (y != 0)
                {
                    y += 5;
                }
                control.Position = new Point(0, y);
                control.init(quest, this, completeState, i);
                this.outgoingScrollArea.addControl(control);
                y += control.Height;
                this.lineList.Add(control);
            }
            this.outgoingScrollArea.Size = new Size(this.outgoingScrollArea.Width, y);
            if (y < this.outgoingScrollBar.Height)
            {
                this.outgoingScrollBar.Visible = false;
            }
            else
            {
                this.outgoingScrollBar.Visible = true;
                this.outgoingScrollBar.NumVisibleLines = this.outgoingScrollBar.Height;
                this.outgoingScrollBar.Max = y - this.outgoingScrollBar.Height;
            }
            this.outgoingScrollArea.invalidate();
            this.outgoingScrollBar.invalidate();
            this.backgroundImage.invalidate();
            this.update();
        }

        public void update()
        {
            double localTime = DXTimer.GetCurrentMilliseconds() / 1000.0;
            foreach (QuestLine line in this.lineList)
            {
                line.update(localTime);
            }
        }

        private void wallScrollBarMoved()
        {
            int y = this.outgoingScrollBar.Value;
            this.outgoingScrollArea.Position = new Point(this.outgoingScrollArea.X, 40 - y);
            this.outgoingScrollArea.ClipRect = new Rectangle(this.outgoingScrollArea.ClipRect.X, y, this.outgoingScrollArea.ClipRect.Width, this.outgoingScrollArea.ClipRect.Height);
            this.outgoingScrollArea.invalidate();
            this.outgoingScrollBar.invalidate();
        }

        public class ArmyComparer : IComparer<WorldMap.LocalArmyData>
        {
            public int Compare(WorldMap.LocalArmyData x, WorldMap.LocalArmyData y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                if (x.armyID > y.armyID)
                {
                    return 1;
                }
                if (x.armyID < y.armyID)
                {
                    return -1;
                }
                return 0;
            }
        }

        public class QuestLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton collectButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDLabel lblQuestDescription = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblReward = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblStatus = new CustomSelfDrawPanel.CSDLabel();
            private QuestsPanel2 m_parent;
            private int m_quest = -1;

            public void init(int quest, QuestsPanel2 parent, int completeState, int position)
            {
                this.m_quest = quest;
                this.m_parent = parent;
                this.clearControls();
                if ((position & 1) == 0)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_light;
                }
                else
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.lineitem_strip_02_dark;
                }
                this.backgroundImage.Position = new Point(0, 0);
                base.addControl(this.backgroundImage);
                this.Size = this.backgroundImage.Size;
                this.lblQuestDescription.Text = Quests.getQuestText(quest);
                this.lblQuestDescription.Color = ARGBColors.Black;
                this.lblQuestDescription.RolloverColor = ARGBColors.White;
                this.lblQuestDescription.Position = new Point(9, 0);
                this.lblQuestDescription.Size = new Size(480, this.backgroundImage.Height);
                this.lblQuestDescription.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblQuestDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblQuestDescription);
                this.collectButton.Visible = false;
                switch (completeState)
                {
                    case -1:
                        this.lblStatus.Visible = false;
                        break;

                    case 0:
                        this.lblStatus.Text = "?";
                        break;

                    case 1:
                        this.lblStatus.Text = SK.Text("QuestLine_Not_Complete", "Objective not complete");
                        break;

                    case 2:
                        this.lblStatus.Text = SK.Text("QuestLine_Complete", "Objective Complete");
                        this.collectButton.Visible = true;
                        break;
                }
                this.lblStatus.Color = ARGBColors.Black;
                this.lblStatus.RolloverColor = ARGBColors.White;
                this.lblStatus.Position = new Point(0x1f0, 0);
                this.lblStatus.Size = new Size(0x120, this.backgroundImage.Height);
                this.lblStatus.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.lblStatus.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.backgroundImage.addControl(this.lblStatus);
                this.collectButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.collectButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.collectButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.collectButton.Position = new Point(0x306, 4);
                this.collectButton.Text.Text = SK.Text("QuestLine_Collect_Reward", "Collect Reward");
                this.collectButton.Text.Color = ARGBColors.Black;
                this.collectButton.Text.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
                this.collectButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked), "QuestsPanel2_collect");
                this.backgroundImage.addControl(this.collectButton);
            }

            private void lineClicked()
            {
                this.collectButton.Enabled = false;
                this.m_parent.completeQuest(this.m_quest);
            }

            public bool update(double localTime)
            {
                return true;
            }
        }
    }
}

