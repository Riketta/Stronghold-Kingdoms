namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class NewQuestsPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton abandonButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDButton completeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel completedQuestsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage completeGlow = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage completeGlow2 = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private DockableControl dockableControl;
        private Panel focusPanel;
        private int glowValue;
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDExtendingPanel insetImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        public static NewQuestsPanel Instance = null;
        private DateTime lastFullUpdateTime = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDLabel lblQuestDescription = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();
        private List<NewQuestLine> lineList = new List<NewQuestLine>();
        private static DateTime m_lastQuestReportingUpdate = DateTime.MinValue;
        private int m_selectedQuest = -1;
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDLabel parishNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel progressTextLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage questIcon = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage questImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDHorzProgressBar questProgressBar = new CustomSelfDrawPanel.CSDHorzProgressBar();
        private CustomSelfDrawPanel.CSDArea questsScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar questsScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDLabel timeLeftLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel tutorialText = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage underlayImage = new CustomSelfDrawPanel.CSDImage();

        public NewQuestsPanel()
        {
            Instance = this;
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.focusPanel.Focus();
        }

        private void abandonQuest()
        {
            if (GameEngine.Instance.World.getNewQuestData() != null)
            {
                this.abandonButton.Enabled = false;
                RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
                RemoteServices.Instance.AbandonNewQuest(-1);
            }
        }

        private void abandonQuest(int questID)
        {
            RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
            RemoteServices.Instance.AbandonNewQuest(questID);
        }

        public static void addRewardIcons(CustomSelfDrawPanel.CSDControl parentControl, Point baseLocation, NewQuests.NewQuestDefinition def, int gloryMode)
        {
            if (def != null)
            {
                CustomSelfDrawPanel.CSDLabel control = null;
                if (gloryMode > 0)
                {
                    control = new CustomSelfDrawPanel.CSDLabel {
                        Color = ARGBColors.Black,
                        Position = baseLocation,
                        Size = new Size(110, GFXLibrary.quest_rewards[0].Height),
                        Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold),
                        Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
                    };
                    parentControl.addControl(control);
                    baseLocation.X += 100;
                }
                int num = 0;
                if ((def.reward_charges.Length > 0) && (gloryMode == 1))
                {
                    parentControl.addControl(createRewardIcon(9, -1, new Point(baseLocation.X, baseLocation.Y + 2), 0xc8c));
                    baseLocation.X += 60;
                    num++;
                    if (((def.getRewardGlory() > 0) && (gloryMode != 0)) && ((GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1) && (gloryMode > 0)))
                    {
                        CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                            Text = "+",
                            Color = ARGBColors.Black,
                            Position = new Point((baseLocation.X - 0x12) - 2, (baseLocation.Y + 12) - 2),
                            Size = new Size(50, 30),
                            Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold),
                            Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
                        };
                        parentControl.addControl(label2);
                        baseLocation.X += 30;
                    }
                }
                if (gloryMode >= 0)
                {
                    if (def.reward_honour > 0)
                    {
                        parentControl.addControl(createRewardIcon(1, def.reward_honour, baseLocation, 0xc83));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_gold > 0)
                    {
                        parentControl.addControl(createRewardIcon(2, def.reward_gold, baseLocation, 0xc84));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_wood > 0)
                    {
                        parentControl.addControl(createRewardIcon(3, def.reward_wood, baseLocation, 0xc85));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_stone > 0)
                    {
                        parentControl.addControl(createRewardIcon(4, def.reward_stone, baseLocation, 0xc86));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_apples > 0)
                    {
                        parentControl.addControl(createRewardIcon(12, def.reward_apples, baseLocation, 0xc8e));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_card_pack > 0)
                    {
                        parentControl.addControl(createRewardIcon(6, def.reward_card_pack, baseLocation, 0xc88));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_2day_premium > 0)
                    {
                        parentControl.addControl(createRewardIcon(7, def.reward_2day_premium, baseLocation, 0xc89));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_faithpoints > 0)
                    {
                        parentControl.addControl(createRewardIcon(8, def.reward_faithpoints, baseLocation, 0xc8a));
                        baseLocation.X += 120;
                        num++;
                    }
                    if (def.reward_tickets > 0)
                    {
                        parentControl.addControl(createRewardIcon(10, def.reward_tickets, baseLocation, 0xc8d));
                        baseLocation.X += 120;
                        num++;
                    }
                }
                if (((def.getRewardGlory() > 0) && (gloryMode != 0)) && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset != 1))
                {
                    if (gloryMode > 0)
                    {
                        CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel {
                            Text = SK.Text("QUESTS_or", "Or"),
                            Color = ARGBColors.Black,
                            Position = new Point(baseLocation.X, baseLocation.Y + 12),
                            Size = new Size(50, 30),
                            Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold),
                            Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER
                        };
                        parentControl.addControl(label3);
                        baseLocation.X += 50;
                    }
                    parentControl.addControl(createRewardIcon(0, def.getRewardGlory(), baseLocation, 0xc8b));
                    baseLocation.X += 120;
                    num++;
                }
                if (control != null)
                {
                    if (num == 1)
                    {
                        control.Text = SK.Text("QUEST_Reward", "Reward");
                    }
                    else
                    {
                        control.Text = SK.Text("QUEST_Rewards", "Rewards");
                    }
                }
            }
        }

        private int bitCount(int n)
        {
            int num = 0;
            while (n != 0)
            {
                num++;
                n &= n - 1;
            }
            return num;
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

        private void completeAbandonNewQuestCallback(CompleteAbandonNewQuest_ReturnType returnData)
        {
            this.abandonButton.Enabled = true;
            if (returnData.Success)
            {
                if (returnData.goldGiven)
                {
                    GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                }
                if (returnData.honourGiven)
                {
                    GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                }
                if (returnData.fpGiven)
                {
                    GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                }
                if (returnData.premiumCardsGiven)
                {
                    CardTypes.PremiumToken token = new CardTypes.PremiumToken {
                        Reward = 1,
                        UserPremiumTokenID = returnData.premiumCardID,
                        WorldID = RemoteServices.Instance.ProfileWorldID,
                        Type = 0x1011
                    };
                    GameEngine.Instance.World.ProfilePremiumTokens.Add(returnData.premiumCardID, token);
                }
                if (returnData.cardPacksGiven > 0)
                {
                    int key = 1;
                    if (GameEngine.Instance.World.ProfileUserCardPacks.ContainsKey(key))
                    {
                        CardTypes.UserCardPack pack = GameEngine.Instance.World.ProfileUserCardPacks[key];
                        pack.Count += returnData.cardPacksGiven;
                    }
                    else
                    {
                        CardTypes.UserCardPack pack2 = new CardTypes.UserCardPack {
                            PackID = key,
                            Count = returnData.cardPacksGiven
                        };
                        GameEngine.Instance.World.ProfileUserCardPacks[key] = pack2;
                    }
                }
                if (returnData.gloryGiven)
                {
                    GameEngine.Instance.World.clearGloryHistory();
                }
                if (returnData.villageUpdated >= 0)
                {
                    GameEngine.Instance.flushVillage(returnData.villageUpdated);
                }
                if (returnData.ticketsGiven > 0)
                {
                    GameEngine.Instance.World.addTickets(-1, returnData.ticketsGiven);
                }
            }
            else if (returnData.m_errorCode == ErrorCodes.ErrorCode.NEW_QUESTS_FAILED_REWARD)
            {
                MyMessageBox.Show(SK.Text("QUESTS_failed_reward_body", "We have been unable to give the correct reward for this quest, please wait a few minutes and try again. If this doesn't work, please contact support."), SK.Text("QUESTS_failed_reward", "Quest Reward Error"));
            }
            if (returnData.m_newQuestsData != null)
            {
                GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
                this.init(true);
            }
        }

        private static void completeAbandonNewQuestCallbackStatic(CompleteAbandonNewQuest_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (returnData.goldGiven)
                {
                    GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
                }
                if (returnData.honourGiven)
                {
                    GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
                }
                if (returnData.fpGiven)
                {
                    GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                }
                if (returnData.premiumCardsGiven)
                {
                    CardTypes.PremiumToken token = new CardTypes.PremiumToken {
                        Reward = 1,
                        UserPremiumTokenID = returnData.premiumCardID,
                        WorldID = RemoteServices.Instance.ProfileWorldID,
                        Type = 0x1011
                    };
                    GameEngine.Instance.World.ProfilePremiumTokens.Add(returnData.premiumCardID, token);
                }
                if (returnData.cardPacksGiven > 0)
                {
                    int key = 1;
                    if (GameEngine.Instance.World.ProfileUserCardPacks.ContainsKey(key))
                    {
                        CardTypes.UserCardPack pack = GameEngine.Instance.World.ProfileUserCardPacks[key];
                        pack.Count += returnData.cardPacksGiven;
                    }
                    else
                    {
                        CardTypes.UserCardPack pack2 = new CardTypes.UserCardPack {
                            PackID = key,
                            Count = returnData.cardPacksGiven
                        };
                        GameEngine.Instance.World.ProfileUserCardPacks[key] = pack2;
                    }
                }
                if (returnData.gloryGiven)
                {
                    GameEngine.Instance.World.clearGloryHistory();
                }
                if (returnData.villageUpdated >= 0)
                {
                    GameEngine.Instance.flushVillage(returnData.villageUpdated);
                }
                if (returnData.ticketsGiven > 0)
                {
                    GameEngine.Instance.World.addTickets(-1, returnData.ticketsGiven);
                }
            }
            else if (returnData.m_errorCode == ErrorCodes.ErrorCode.NEW_QUESTS_FAILED_REWARD)
            {
                MyMessageBox.Show(SK.Text("QUESTS_failed_reward_body", "We have been unable to give the correct reward for this quest, please wait a few minutes and try again. If this doesn't work, please contact support."), SK.Text("QUESTS_failed_reward", "Quest Reward Error"));
            }
            if (returnData.m_newQuestsData != null)
            {
                GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
            }
        }

        private void completeQuest()
        {
            NewQuestsData data = GameEngine.Instance.World.getNewQuestData();
            if (data != null)
            {
                InterfaceMgr.Instance.openNewQuestRewardPopup(data.questID, -1, this);
            }
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public static CustomSelfDrawPanel.CSDImage createRewardIcon(int iconID, int value, Point Location, int tooltipID)
        {
            CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.quest_rewards[iconID],
                Position = Location,
                CustomTooltipID = tooltipID
            };
            if (value >= 0)
            {
                CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                    Text = value.ToString(),
                    Color = ARGBColors.Black,
                    Position = new Point(0x2f, 0),
                    Size = new Size(80, image.Image.Height),
                    Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold),
                    Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT,
                    CustomTooltipID = tooltipID
                };
                image.addControl(control);
            }
            return image;
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

        public void doCompleteQuest(int questID, int villageID, bool glory)
        {
            this.completeButton.Enabled = false;
            RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
            RemoteServices.Instance.CompleteNewQuest(questID, glory, villageID);
        }

        public void expandQuest(int quest)
        {
            this.m_selectedQuest = quest;
            int num = this.questsScrollBar.Value;
            this.rebuild(quest);
            this.questsScrollBar.Value = num;
        }

        private void friendClicked()
        {
            string fileName = URLs.InviteAFriendURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "") + "&lang=" + Program.mySettings.LanguageIdent.ToLower() + "&colour=" + GFXLibrary.invite_ad_colour.ToString();
            try
            {
                Process.Start(fileName);
            }
            catch (Exception)
            {
                MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
            }
        }

        private void getQuestDataCallback(GetQuestData_ReturnType returnData)
        {
            if (returnData.Success)
            {
                GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
                this.init(true);
                handleClientSideQuestReporting(true);
            }
        }

        public static void handleClientSideQuestReporting(bool timeRestricted)
        {
            NewQuestsData questData = GameEngine.Instance.World.getNewQuestData();
            if (((questData == null) || (questData.questID < 0)) || (questData.completionState != 0))
            {
                return;
            }
            int questID = questData.questID;
            if (questID <= 0x43)
            {
                if (questID <= 0x22)
                {
                    switch (questID)
                    {
                        case 20:
                        case 0x22:
                        case 4:
                        case 5:
                        case 0x10:
                            goto Label_00DC;
                    }
                    return;
                }
                if (questID > 0x30)
                {
                    switch (questID)
                    {
                        case 0x40:
                        case 0x43:
                            goto Label_00DC;
                    }
                    return;
                }
                if ((questID != 0x2a) && (questID != 0x30))
                {
                    return;
                }
            }
            else
            {
                if (questID <= 0x7d)
                {
                    switch (questID)
                    {
                        case 0x7a:
                        case 0x7d:
                        case 0x63:
                        case 0x65:
                        case 0x54:
                            goto Label_00DC;

                        case 100:
                            return;
                    }
                    return;
                }
                if (questID <= 0x94)
                {
                    if ((questID != 140) && (questID != 0x94))
                    {
                        return;
                    }
                }
                else if ((questID != 0x9d) && (questID != 0xa7))
                {
                    return;
                }
            }
        Label_00DC:
            if (isQuestComplete(questData))
            {
                if (timeRestricted)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - m_lastQuestReportingUpdate);
                    if (span.TotalSeconds < 300.0)
                    {
                        return;
                    }
                }
                m_lastQuestReportingUpdate = DateTime.Now;
                RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(NewQuestsPanel.completeAbandonNewQuestCallbackStatic));
                RemoteServices.Instance.AbandonNewQuest(-3);
            }
        }

        public void init(bool resized)
        {
            int height = base.Height;
            Instance = this;
            base.clearControls();
            this.headerImage.Size = new Size(base.Width, 40);
            this.headerImage.Position = new Point(0, 0);
            base.addControl(this.headerImage);
            this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
            CustomSelfDrawPanel.WikiLinkControl.init(this.headerImage, 0x13, new Point(base.Width - 0x2c, 3));
            this.backgroundImage.Size = new Size(base.Width, height - 40);
            this.backgroundImage.Position = new Point(0, 40);
            base.addControl(this.backgroundImage);
            this.backgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
            this.underlayImage.Image = (Image) GFXLibrary.quest_screen_warm;
            this.underlayImage.Position = new Point(6, 0);
            this.backgroundImage.addControl(this.underlayImage);
            this.questImage.Image = (Image) GFXLibrary.quest_screen_top;
            this.questImage.Position = new Point(0x15, 0x12);
            this.backgroundImage.addControl(this.questImage);
            this.parishNameLabel.Text = SK.Text("QuestPanel_Quests", "Quests");
            this.parishNameLabel.Color = ARGBColors.White;
            this.parishNameLabel.DropShadowColor = ARGBColors.Black;
            this.parishNameLabel.Position = new Point(20, 0);
            this.parishNameLabel.Size = new Size(base.Width - 40, 40);
            this.parishNameLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.parishNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.headerImage.addControl(this.parishNameLabel);
            this.questsScrollArea.Position = new Point(40, 230);
            this.questsScrollArea.Size = new Size(880, ((height - 230) - 20) - 40);
            this.questsScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(880, ((height - 230) - 20) - 40));
            this.backgroundImage.addControl(this.questsScrollArea);
            this.insetImage.Position = new Point(0x15, 220);
            this.insetImage.Size = new Size(0x3b3, (((height - 230) - 20) - 40) + 20);
            this.backgroundImage.addControl(this.insetImage);
            this.insetImage.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
            int num2 = this.questsScrollBar.Value;
            this.questsScrollBar.Position = new Point(930, 230);
            this.questsScrollBar.Size = new Size(0x18, ((height - 230) - 20) - 40);
            this.backgroundImage.addControl(this.questsScrollBar);
            this.questsScrollBar.Value = 0;
            this.questsScrollBar.Max = 100;
            this.questsScrollBar.NumVisibleLines = 0x19;
            this.questsScrollBar.Create(null, null, null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
            this.questsScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
            this.mouseWheelOverlay.Position = this.questsScrollArea.Position;
            this.mouseWheelOverlay.Size = this.questsScrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.backgroundImage.addControl(this.mouseWheelOverlay);
            NewQuestsData questData = GameEngine.Instance.World.getNewQuestData();
            if ((questData == null) || (questData.questID < 0))
            {
                this.lblQuestName.Text = SK.Text("QUESTS_No_Active_Quest", "No Active Quest");
                this.lblQuestName.Color = ARGBColors.Black;
                this.lblQuestName.Position = new Point(170, 0x13);
                this.lblQuestName.Size = new Size(700, 30);
                this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                this.questImage.addControl(this.lblQuestName);
                goto Label_11ED;
            }
            int data = 0;
            int parameter = 1;
            int num5 = 0;
            NewQuests.NewQuestDefinition def = NewQuests.getNewQuestDef(questData.questID);
            addRewardIcons(this.questImage, new Point(170, 0x4b), def, 1);
            this.questIcon.Image = (Image) GFXLibrary.quest_icons[Math.Min(def.questType, GFXLibrary.quest_icons.Length - 1)];
            this.questIcon.Position = new Point(170, 0x10);
            this.questImage.addControl(this.questIcon);
            this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + def.tagString);
            this.lblQuestName.Color = ARGBColors.Black;
            this.lblQuestName.Position = new Point(220, 0x13);
            this.lblQuestName.Size = new Size(700, 30);
            this.lblQuestName.Font = FontManager.GetFont("Arial", 13f, FontStyle.Bold);
            this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.questImage.addControl(this.lblQuestName);
            NumberFormatInfo nFI = GameEngine.NFI;
            this.lblQuestDescription.Text = SK.Text("QUEST_PANEL_DESCRIPTION_OBJECTIVE", "Objective");
            this.lblQuestDescription.Text = this.lblQuestDescription.Text + " - ";
            switch (questData.questID)
            {
                case 0x22:
                case 0x30:
                case 4:
                case 0x10:
                case 0x65:
                case 0x7a:
                case 0x40:
                case 0x54:
                    this.lblQuestDescription.Text = this.lblQuestDescription.Text + SK.Text("QUESTS_Spread_New_description", "Learn about invite a friend");
                    break;

                default:
                    if (def.parameter > 0)
                    {
                        this.lblQuestDescription.Text = this.lblQuestDescription.Text + SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + def.tagString) + " : " + def.parameter.ToString("N", nFI);
                    }
                    else
                    {
                        this.lblQuestDescription.Text = this.lblQuestDescription.Text + SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + def.tagString);
                    }
                    break;
            }
            this.lblQuestDescription.Color = ARGBColors.Black;
            this.lblQuestDescription.RolloverColor = ARGBColors.White;
            this.lblQuestDescription.Position = new Point(220, 0x2a);
            this.lblQuestDescription.Size = new Size(740, 50);
            this.lblQuestDescription.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.lblQuestDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.lblQuestDescription.Data = questData.questID;
            this.lblQuestDescription.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OpenFurtherInformation));
            this.lblQuestDescription.Tag = def.tagString;
            this.questImage.addControl(this.lblQuestDescription);
            if ((questData.completionState < 0) || !isQuestComplete(questData))
            {
                this.abandonButton.Enabled = true;
                if (((def.parameter > 0) || (questData.questID == 0x42)) || ((questData.questID == 0x92) || (questData.questID == 0x1d)))
                {
                    switch (questData.questID)
                    {
                        case 20:
                        case 5:
                        case 0x2a:
                        case 0x43:
                        case 0x7d:
                        case 0x63:
                        case 0x9d:
                        case 0xa7:
                        case 0x94:
                        case 140:
                        {
                            double num7 = GameEngine.Instance.World.getCurrentGold() - questData.startingData;
                            num5 = (int) num7;
                            if (num7 < 0.0)
                            {
                                num7 = 0.0;
                            }
                            data = (int) num7;
                            parameter = def.parameter;
                            goto Label_0C4E;
                        }
                        case 0x1d:
                            data = this.bitCount(questData.data);
                            parameter = 4;
                            num5 = data;
                            goto Label_0C4E;

                        case 4:
                        case 0x10:
                        case 0x22:
                        case 0x40:
                        case 0x30:
                        case 0x7a:
                        case 0x65:
                        case 0x54:
                            data = questData.data;
                            parameter = def.parameter;
                            num5 = data;
                            goto Label_0C4E;

                        case 0x42:
                            data = this.bitCount(questData.data);
                            parameter = 6;
                            num5 = data;
                            goto Label_0C4E;

                        case 0x92:
                            data = this.bitCount(questData.data);
                            parameter = 8;
                            num5 = data;
                            goto Label_0C4E;
                    }
                    data = questData.data;
                    parameter = def.parameter;
                    num5 = data;
                }
            }
            else
            {
                this.completeGlow2.Image = (Image) GFXLibrary.quest_button_glow;
                this.completeGlow2.Position = new Point(0x278, 0x84);
                this.completeGlow2.Alpha = 1f;
                this.questImage.addControl(this.completeGlow2);
                this.completeGlow.Image = (Image) GFXLibrary.quest_button_glow;
                this.completeGlow.Position = new Point(0x278, 0x84);
                this.completeGlow.Alpha = 1f;
                this.questImage.addControl(this.completeGlow);
                this.glowValue = 0;
                this.abandonButton.Enabled = false;
                this.completeButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                this.completeButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                this.completeButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                this.completeButton.Position = new Point(0x288, 0x95);
                this.completeButton.Text.Text = SK.Text("QUESTS_Complete", "Complete");
                this.completeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                this.completeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.completeButton.TextYOffset = -3;
                this.completeButton.Text.Color = ARGBColors.Black;
                this.completeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.completeQuest), "NewQuests_Complete_Clicked");
                this.completeButton.Enabled = true;
                this.questImage.addControl(this.completeButton);
                switch (questData.questID)
                {
                    case 0x42:
                        data = parameter = 6;
                        break;

                    case 0x43:
                    case 0x2a:
                    case 5:
                    case 20:
                    case 0x63:
                    case 0x7d:
                    case 140:
                    case 0x94:
                    case 0x9d:
                    case 0xa7:
                        data = parameter = def.parameter;
                        break;

                    case 0x1d:
                        data = parameter = 4;
                        break;

                    case 0x92:
                        data = parameter = 8;
                        break;

                    default:
                        data = parameter = def.parameter;
                        break;
                }
                if (parameter == 0)
                {
                    parameter = data = 1;
                }
                num5 = data;
            }
        Label_0C4E:
            this.questProgressBar.Position = new Point(0xa2, 0x7c);
            this.questProgressBar.Size = new Size(0x2fe, 0x16);
            this.questProgressBar.Offset = new Point(0, 0);
            this.questImage.addControl(this.questProgressBar);
            this.questProgressBar.Create(null, null, null, (Image) GFXLibrary.quest_screen_progbar_left, (Image) GFXLibrary.quest_screen_progbar_mid, (Image) GFXLibrary.quest_screen_progbar_right);
            this.questProgressBar.setValues((double) data, (double) parameter);
            this.progressTextLabel.Text = num5.ToString("N", nFI) + " / " + parameter.ToString("N", nFI);
            this.progressTextLabel.Color = ARGBColors.White;
            this.progressTextLabel.Position = new Point(0, -1);
            this.progressTextLabel.Size = new Size(this.questProgressBar.Width, this.questProgressBar.Height);
            this.progressTextLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.progressTextLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.questProgressBar.addControl(this.progressTextLabel);
            this.abandonButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.abandonButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.abandonButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.abandonButton.Position = new Point(0x31e, 0x95);
            this.abandonButton.Text.Text = SK.Text("QUESTS_Abandon", "Abandon");
            this.abandonButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.abandonButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.abandonButton.TextYOffset = -3;
            this.abandonButton.Text.Color = ARGBColors.Black;
            this.abandonButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.abandonQuest), "NewQuests_Abandon_Started_Quest_Clicked");
            this.abandonButton.Enabled = true;
            this.questImage.addControl(this.abandonButton);
            switch (questData.questID)
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
                        CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton {
                            ImageNorm = (Image) GFXLibrary.banner_ad_friend_quest,
                            OverBrighten = true,
                            Position = new Point(0x98, 5)
                        };
                        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.friendClicked), "LogoutPanel_invite_a_friend");
                        this.questImage.addControl(control);
                        this.lblQuestDescription.Text = "";
                        this.lblQuestName.Text = "";
                    }
                    else
                    {
                        this.lblQuestDescription.Text = SK.Text("QUESTS_Spread_New_description", "Learn about Invite a Friend");
                    }
                    break;
            }
            if (def.timed > 0)
            {
                if (questData.completionState == 0)
                {
                    TimeSpan span = new TimeSpan(def.timed, 0, 0);
                    TimeSpan span2 = span - (VillageMap.getCurrentServerTime() - questData.startTime);
                    int totalSeconds = (int) span2.TotalSeconds;
                    this.timeLeftLabel.Text = SK.Text("QUESTS_TimeRemaining", "Time Remaining") + " : " + VillageMap.createBuildTimeStringFull(totalSeconds);
                    this.timeLeftLabel.Color = ARGBColors.Black;
                    this.timeLeftLabel.Position = new Point(170, 0x91);
                    this.timeLeftLabel.Size = new Size(760, 50);
                    this.timeLeftLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.timeLeftLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    this.timeLeftLabel.Visible = true;
                    this.questImage.addControl(this.timeLeftLabel);
                }
                else if (questData.completionState < 0)
                {
                    this.timeLeftLabel.Text = SK.Text("QUESTS_QuestFailed", "Quest Failed");
                    this.timeLeftLabel.Color = ARGBColors.Black;
                    this.timeLeftLabel.Position = new Point(170, 0x91);
                    this.timeLeftLabel.Size = new Size(760, 50);
                    this.timeLeftLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.timeLeftLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                    this.timeLeftLabel.Visible = true;
                    this.questImage.addControl(this.timeLeftLabel);
                }
            }
        Label_11ED:
            this.completedQuestsLabel.Text = SK.Text("QUESTS_CompletedQuests", "Completed Quests") + " : " + questData.totalCompleted.ToString();
            this.completedQuestsLabel.Color = ARGBColors.Black;
            this.completedQuestsLabel.RolloverColor = ARGBColors.White;
            this.completedQuestsLabel.Position = new Point(170, 0xa5);
            this.completedQuestsLabel.Size = new Size(460, 50);
            this.completedQuestsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.completedQuestsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.completedQuestsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showCompletedQuests));
            this.questImage.addControl(this.completedQuestsLabel);
            this.tutorialText.Text = SK.Text("Quest_Tutorial_Inprogress", "The Tutorial is currently in progress. Please finish or quit the Tutorial to access Quests.");
            this.tutorialText.Color = ARGBColors.Black;
            this.tutorialText.Position = this.questsScrollArea.Position;
            this.tutorialText.Size = this.questsScrollArea.Size;
            this.tutorialText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.tutorialText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.questImage.addControl(this.tutorialText);
            if (!resized)
            {
                this.m_selectedQuest = -1;
                TimeSpan span3 = (TimeSpan) (DateTime.Now - this.lastFullUpdateTime);
                RemoteServices.Instance.set_GetQuestData_UserCallBack(new RemoteServices.GetQuestData_UserCallBack(this.getQuestDataCallback));
                if (span3.TotalMinutes > 1.0)
                {
                    RemoteServices.Instance.GetQuestData(true);
                }
                else
                {
                    RemoteServices.Instance.GetQuestData(false);
                }
            }
            this.rebuild(this.m_selectedQuest);
            if (resized)
            {
                this.questsScrollBar.Value = num2;
                this.questsScrollBar.scrollDown(0);
                this.wallScrollBarMoved();
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
            base.Name = "NewQuestsPanel";
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

        public static bool isQuestComplete(NewQuestsData questData)
        {
            if ((questData.completionState > 0) && (questData.questID >= 0))
            {
                return true;
            }
            NewQuests.NewQuestDefinition definition = NewQuests.getNewQuestDef(questData.questID);
            if (definition != null)
            {
                switch (questData.questID)
                {
                    case 0x42:
                        if (questData.data != 0x3f)
                        {
                            break;
                        }
                        return true;

                    case 0x43:
                    case 0x2a:
                    case 5:
                    case 20:
                    case 0x63:
                    case 0x7d:
                    case 140:
                    case 0x94:
                    case 0x9d:
                    case 0xa7:
                    {
                        double num2 = GameEngine.Instance.World.getCurrentGold() - questData.startingData;
                        if (num2 < definition.parameter)
                        {
                            break;
                        }
                        return true;
                    }
                    case 0x1d:
                        if (questData.data != 15)
                        {
                            break;
                        }
                        return true;

                    case 0x92:
                        if (questData.data != 0xff)
                        {
                            break;
                        }
                        return true;

                    default:
                        if (definition.parameter == 0)
                        {
                            if (questData.data > 0)
                            {
                                return true;
                            }
                        }
                        else if (questData.data >= definition.parameter)
                        {
                            return true;
                        }
                        break;
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

        public void OpenFurtherInformation()
        {
            int data = CustomSelfDrawPanel.StaticClickedControl.Data;
            InterfaceMgr.Instance.openNewQuestFurtherTextPopup((string) CustomSelfDrawPanel.StaticClickedControl.Tag, data);
        }

        public void rebuild(int activeQuest)
        {
            bool allowStart = true;
            NewQuestsData data = GameEngine.Instance.World.getNewQuestData();
            if (data != null)
            {
                if (data.questID >= 0)
                {
                    allowStart = false;
                }
                int[] availableQuests = data.availableQuests;
                if (availableQuests == null)
                {
                    availableQuests = new int[0];
                }
                this.questsScrollArea.clearControls();
                int y = 0;
                this.lineList.Clear();
                if (!GameEngine.Instance.World.isTutorialActive())
                {
                    double num1 = DXTimer.GetCurrentMilliseconds() / 1000.0;
                    int index = 0;
                    while (index < availableQuests.Length)
                    {
                        int quest = availableQuests[index];
                        if (quest > 0)
                        {
                            NewQuestLine control = new NewQuestLine();
                            if (y != 0)
                            {
                                y += 5;
                            }
                            control.Position = new Point(0, y);
                            control.init(quest, this, index, activeQuest, allowStart, false);
                            this.questsScrollArea.addControl(control);
                            y += control.Height;
                            this.lineList.Add(control);
                        }
                        index++;
                    }
                    if (data.numAbandonedQuests > 0)
                    {
                        NewQuestLine line2 = new NewQuestLine();
                        if (y != 0)
                        {
                            y += 5;
                        }
                        line2.Position = new Point(0, y);
                        line2.init(-data.numAbandonedQuests, this, index, activeQuest, allowStart, false);
                        this.questsScrollArea.addControl(line2);
                        y += line2.Height;
                        this.lineList.Add(line2);
                    }
                    this.tutorialText.Visible = false;
                }
                else
                {
                    this.tutorialText.Visible = true;
                }
                y += 5;
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
                this.backgroundImage.invalidate();
                this.update();
            }
        }

        private void restoreQuests()
        {
            RemoteServices.Instance.set_CompleteAbandonNewQuest_UserCallBack(new RemoteServices.CompleteAbandonNewQuest_UserCallBack(this.completeAbandonNewQuestCallback));
            RemoteServices.Instance.AbandonNewQuest(-2);
        }

        private void showCompletedQuests()
        {
            InterfaceMgr.Instance.openNewQuestsCompletedPopup(null);
        }

        private void startNewQuestCallback(StartNewQuest_ReturnType returnData)
        {
            bool success = returnData.Success;
            if (returnData.m_newQuestsData != null)
            {
                GameEngine.Instance.World.setNewQuestData(returnData.m_newQuestsData);
                this.init(true);
            }
        }

        protected void startQuest(int questID)
        {
            RemoteServices.Instance.set_StartNewQuest_UserCallBack(new RemoteServices.StartNewQuest_UserCallBack(this.startNewQuestCallback));
            RemoteServices.Instance.StartNewQuest(questID);
        }

        public void update()
        {
            NewQuestsData data = GameEngine.Instance.World.getNewQuestData();
            if (((data != null) && (data.questID >= 0)) && (data.completionState == 0))
            {
                NewQuests.NewQuestDefinition definition = NewQuests.getNewQuestDef(data.questID);
                if (definition.timed > 0)
                {
                    TimeSpan span = new TimeSpan(definition.timed, 0, 0);
                    TimeSpan span2 = span - (VillageMap.getCurrentServerTime() - data.startTime);
                    int totalSeconds = (int) span2.TotalSeconds;
                    if (totalSeconds < 0)
                    {
                        totalSeconds = 0;
                    }
                    this.timeLeftLabel.TextDiffOnly = SK.Text("QUESTS_TimeRemaining", "Time Remaining") + " : " + VillageMap.createBuildTimeStringFull(totalSeconds);
                }
                else
                {
                    this.timeLeftLabel.Visible = false;
                }
            }
            else if (data.completionState < 0)
            {
                this.timeLeftLabel.TextDiffOnly = SK.Text("QUESTS_QuestFailed", "Quest Failed");
                this.timeLeftLabel.Visible = true;
            }
            else
            {
                this.timeLeftLabel.Visible = false;
            }
            this.glowValue += 15;
            if (this.glowValue > 0x1ff)
            {
                this.glowValue -= 0x1ff;
            }
            int glowValue = this.glowValue;
            if (glowValue > 0xff)
            {
                glowValue = 0x1ff - glowValue;
            }
            this.completeGlow.Alpha = ((float) glowValue) / 255f;
            this.completeGlow.invalidate();
        }

        private void wallScrollBarMoved()
        {
            int y = this.questsScrollBar.Value;
            this.questsScrollArea.Position = new Point(this.questsScrollArea.X, 230 - y);
            this.questsScrollArea.ClipRect = new Rectangle(this.questsScrollArea.ClipRect.X, y, this.questsScrollArea.ClipRect.Width, this.questsScrollArea.ClipRect.Height);
            this.questsScrollArea.invalidate();
            this.questsScrollBar.invalidate();
        }

        public class NewQuestLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDButton abandonQuestButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton furtherTextButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDLabel lblObjective = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblQuestDescription = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();
            private int m_activeQuest = -1;
            private NewQuestsPanel m_parent;
            private int m_quest = -1;
            private CustomSelfDrawPanel.CSDImage questImage = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDButton startQuestButton = new CustomSelfDrawPanel.CSDButton();

            private void abandonQuest()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.abandonQuest(this.m_quest);
                }
            }

            public void init(int quest, NewQuestsPanel parent, int position, int activeQuest, bool allowStart, bool completed)
            {
                this.m_quest = quest;
                this.m_activeQuest = activeQuest;
                this.m_parent = parent;
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
                if (!completed)
                {
                    this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                }
                base.addControl(this.backgroundImage);
                if (quest < 0)
                {
                    this.lblQuestName.Text = SK.Text("QUESTS_AbandonedQuests", "Abandoned Quests : ") + quest.ToString();
                    this.lblQuestName.Color = ARGBColors.Black;
                    if (activeQuest != quest)
                    {
                        this.lblQuestName.RolloverColor = ARGBColors.White;
                    }
                    this.lblQuestName.Position = new Point(9, 0);
                    this.lblQuestName.Size = new Size(700, this.backgroundImage.Height);
                    this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.backgroundImage.addControl(this.lblQuestName);
                    this.startQuestButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
                    this.startQuestButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
                    this.startQuestButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
                    this.startQuestButton.Position = new Point(670, 6);
                    this.startQuestButton.Text.Text = SK.Text("QUESTS_Restore", "Restore");
                    this.startQuestButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                    this.startQuestButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                    this.startQuestButton.TextYOffset = -3;
                    this.startQuestButton.Text.Color = ARGBColors.Black;
                    this.startQuestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreQuests), "NewQuests_Restore_Quests_Clicked");
                    this.backgroundImage.addControl(this.startQuestButton);
                    this.Size = new Size(880, 60);
                }
                else
                {
                    NewQuests.NewQuestDefinition def = NewQuests.getNewQuestDef(quest);
                    if (!completed)
                    {
                        base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    }
                    if ((activeQuest != quest) || completed)
                    {
                        this.Size = new Size(880, 60);
                    }
                    else
                    {
                        this.Size = new Size(880, 130);
                        NumberFormatInfo nFI = GameEngine.NFI;
                        switch (def.ID)
                        {
                            case 0x22:
                            case 0x30:
                            case 4:
                            case 0x10:
                            case 0x65:
                            case 0x7a:
                            case 0x40:
                            case 0x54:
                                this.lblQuestDescription.Text = this.lblQuestDescription.Text + SK.Text("QUESTS_Spread_New_description", "Learn about Invite a Friend");
                                break;

                            default:
                                if (def.parameter > 0)
                                {
                                    this.lblQuestDescription.Text = this.lblQuestDescription.Text + SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + def.tagString) + " : " + def.parameter.ToString("N", nFI);
                                }
                                else
                                {
                                    this.lblQuestDescription.Text = this.lblQuestDescription.Text + SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + def.tagString);
                                }
                                break;
                        }
                        this.lblQuestDescription.Color = ARGBColors.Black;
                        this.lblQuestDescription.RolloverColor = ARGBColors.White;
                        this.lblQuestDescription.Position = new Point(0xaf, 0x39);
                        this.lblQuestDescription.Size = new Size(760, 50);
                        this.lblQuestDescription.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
                        this.lblQuestDescription.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                        this.lblQuestDescription.Data = def.ID;
                        this.lblQuestDescription.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.OpenFurtherInformation));
                        this.lblQuestDescription.Tag = def.tagString;
                        base.addControl(this.lblQuestDescription);
                        this.lblObjective.Text = SK.Text("QUEST_PANEL_DESCRIPTION_OBJECTIVE", "Objective");
                        this.lblObjective.Color = ARGBColors.Black;
                        this.lblObjective.Position = new Point(70, 0x39);
                        this.lblObjective.Size = new Size(760, 50);
                        this.lblObjective.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
                        this.lblObjective.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                        base.addControl(this.lblObjective);
                        NewQuestsPanel.addRewardIcons(this, new Point(70, 0x5f), def, 1);
                    }
                    this.questImage.Image = (Image) GFXLibrary.quest_icons[Math.Min(def.questType, GFXLibrary.quest_icons.Length - 1)];
                    this.questImage.Position = new Point(0, 6);
                    if (!completed)
                    {
                        this.questImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    }
                    base.addControl(this.questImage);
                    this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + def.tagString);
                    this.lblQuestName.Color = ARGBColors.Black;
                    if ((activeQuest != quest) && !completed)
                    {
                        this.lblQuestName.RolloverColor = ARGBColors.White;
                    }
                    this.lblQuestName.Position = new Point(9, 0);
                    this.lblQuestName.Size = new Size(700, this.backgroundImage.Height);
                    this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                    this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    if (!completed)
                    {
                        this.lblQuestName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    }
                    this.backgroundImage.addControl(this.lblQuestName);
                    if (allowStart && !completed)
                    {
                        this.startQuestButton.ImageNorm = (Image) GFXLibrary.quest_checkboxes[0];
                        this.startQuestButton.ImageOver = (Image) GFXLibrary.quest_checkboxes[2];
                        this.startQuestButton.MoveOnClick = true;
                        this.startQuestButton.Position = new Point(0x2cb, 6);
                        this.startQuestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.startQuest), "NewQuests_Start_Quest_Clicked");
                        this.startQuestButton.CustomTooltipID = 0xc81;
                        this.backgroundImage.addControl(this.startQuestButton);
                    }
                    if (!completed)
                    {
                        this.abandonQuestButton.ImageNorm = (Image) GFXLibrary.quest_checkboxes[1];
                        this.abandonQuestButton.ImageOver = (Image) GFXLibrary.quest_checkboxes[3];
                        this.abandonQuestButton.MoveOnClick = true;
                        this.abandonQuestButton.Position = new Point(0x2fd, 6);
                        this.abandonQuestButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.abandonQuest), "NewQuests_Abandon_Clicked");
                        this.abandonQuestButton.CustomTooltipID = 0xc82;
                        this.backgroundImage.addControl(this.abandonQuestButton);
                    }
                }
            }

            private void lineClicked()
            {
                if (this.m_activeQuest != this.m_quest)
                {
                    GameEngine.Instance.playInterfaceSound("NewQuests_Expand_Quest_Description");
                    this.m_parent.expandQuest(this.m_quest);
                }
            }

            public void OpenFurtherInformation()
            {
                int data = CustomSelfDrawPanel.StaticClickedControl.Data;
                InterfaceMgr.Instance.openNewQuestFurtherTextPopup((string) CustomSelfDrawPanel.StaticClickedControl.Tag, data);
            }

            private void restoreQuests()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.restoreQuests();
                }
            }

            private void startQuest()
            {
                if (this.m_parent != null)
                {
                    this.m_parent.startQuest(this.m_quest);
                }
            }

            public bool update(double localTime)
            {
                return true;
            }
        }
    }
}

