namespace Kingdoms
{
    using CommonTypes;
    using StatTracking;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class NewQuestRewardPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDFill backgroundImage = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel captureLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage chargesImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton collectButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton collectGloryButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDButton facebookShareButton = new CustomSelfDrawPanel.CSDButton();
        private string fb_questCaption = "";
        private int fb_questID = -1;
        private string fb_questString = "";
        private CustomSelfDrawPanel.CSDHorzExtendingPanel headerBarImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
        private CustomSelfDrawPanel.CSDVertExtendingPanel insetImage = new CustomSelfDrawPanel.CSDVertExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel lblQuestName = new CustomSelfDrawPanel.CSDLabel();
        private bool m_AppleAvailable = true;
        private bool m_AppleChecked;
        private bool m_awaitingResponse;
        private int m_buildingType;
        private NewQuests.NewQuestDefinition m_questDef;
        private int m_questID = -1;
        private NewQuestsPanel m_questPanel;
        private bool m_StoneAvailable = true;
        private bool m_StoneChecked;
        private int m_villageID = -1;
        private bool m_WoodAvailable = true;
        private bool m_WoodChecked;
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDLabel orLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage questIcon = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDArea questsScrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar questsScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private CustomSelfDrawPanel.CSDImage strip1 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage strip2 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage strip3 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage strip4 = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel targetVillageLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage villageIcon = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel villageNameLabel = new CustomSelfDrawPanel.CSDLabel();

        public NewQuestRewardPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void addVillages(bool autoSelect)
        {
            List<int> list = GameEngine.Instance.World.getUserVillageIDList();
            list.Sort(UserInfoScreen2.villageComparer);
            this.questsScrollArea.clearControls();
            int y = 0;
            for (int i = 0; i < list.Count; i++)
            {
                int villageID = list[i];
                NewQuestVillageLine control = new NewQuestVillageLine {
                    Position = new Point(0, y)
                };
                bool selected = villageID == this.m_villageID;
                control.init(villageID, this, i, selected);
                this.questsScrollArea.addControl(control);
                y += control.Height;
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
            if (autoSelect && (list.Count == 1))
            {
                this.villageSelected(list[0]);
            }
        }

        public void checkResource()
        {
            this.m_awaitingResponse = true;
            RemoteServices.Instance.set_GetResourceLevel_UserCallBack(new RemoteServices.GetResourceLevel_UserCallBack(this.checkResourceCallBack));
            RemoteServices.Instance.GetResourceLevel(this.m_villageID, this.m_buildingType);
        }

        public void checkResourceCallBack(GetResourceLevel_ReturnType returnData)
        {
            this.m_awaitingResponse = false;
            double num = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, this.m_buildingType, false) - returnData.uncappedLevel;
            switch (this.m_buildingType)
            {
                case 6:
                    this.m_WoodChecked = true;
                    this.m_WoodAvailable = Convert.ToInt32(num) >= this.m_questDef.reward_wood;
                    break;

                case 7:
                    this.m_StoneChecked = true;
                    this.m_StoneAvailable = Convert.ToInt32(num) >= this.m_questDef.reward_stone;
                    break;

                case 0x12:
                    this.m_AppleChecked = true;
                    this.m_AppleAvailable = Convert.ToInt32(num) >= this.m_questDef.reward_apples;
                    break;

                default:
                    return;
            }
            this.confirmAvailableSpace();
        }

        private void CompleteQuest()
        {
            this.m_questPanel.doCompleteQuest(this.m_questID, this.m_villageID, false);
            InterfaceMgr.Instance.closeNewQuestRewardPopup();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        public void confirmAvailableSpace()
        {
            if (!this.m_awaitingResponse)
            {
                if (!this.m_AppleChecked && (this.m_questDef.reward_apples > 0))
                {
                    this.m_buildingType = 0x12;
                    this.checkResource();
                }
                else if (!this.m_WoodChecked && (this.m_questDef.reward_wood > 0))
                {
                    this.m_buildingType = 6;
                    this.checkResource();
                }
                else if (!this.m_StoneChecked && (this.m_questDef.reward_stone > 0))
                {
                    this.m_buildingType = 7;
                    this.checkResource();
                }
                else if (((this.m_AppleAvailable && this.m_WoodAvailable) && this.m_StoneAvailable) || (MyMessageBox.Show(SK.Text("Quest_Reward_Insufficient_Space", "You do not have enough room to store all of the reward at this village. Are you sure you want to send the reward to this village?"), SK.Text("Quest_Reward_Insufficient_Space_header", "Insufficient Space"), MessageBoxButtons.YesNo) != DialogResult.No))
                {
                    this.CompleteQuest();
                }
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

        private void facebookShareClicked()
        {
            if (this.fb_questID >= 0)
            {
                object[] objArray = new object[] { "http://login.strongholdkingdoms.com/facebook/js_share.php?u=", RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "&eventid=", (this.fb_questID + 100).ToString(), "&desc=", this.fb_questString, "&lang=", Program.mySettings.LanguageIdent, "&worldid=", Program.mySettings.LastWorldID, "&caption=", this.fb_questCaption };
                string str = string.Concat(objArray);
                try
                {
                    StatTrackingClient.Instance().ActivateTrigger(30, 0);
                    new Process { StartInfo = { FileName = str } }.Start();
                }
                catch (Exception)
                {
                }
            }
        }

        public void init(int questID, int villageID, NewQuestsPanel questPanel, NewQuestRewardPopup parent)
        {
            this.m_questID = questID;
            this.m_villageID = -1;
            this.m_questPanel = questPanel;
            base.clearControls();
            bool flag = false;
            if (GameEngine.Instance.World.YourHouse > 0)
            {
                flag = true;
            }
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
            {
                flag = false;
            }
            List<int> list = GameEngine.Instance.World.getUserVillageIDList();
            int height = 200;
            NewQuests.NewQuestDefinition def = NewQuests.getNewQuestDef(questID);
            this.fb_questID = def.questType - 1;
            NumberFormatInfo nFI = GameEngine.NFI;
            if (def.parameter > 0)
            {
                this.fb_questCaption = SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + def.tagString) + " : " + def.parameter.ToString("N", nFI);
            }
            else
            {
                this.fb_questCaption = SK.NoStoreText("Z_QUEST_DESCRIPTIONS_" + def.tagString);
            }
            if ((((def.reward_apples > 0) || (def.reward_stone > 0)) || (def.reward_wood > 0)) && (list.Count > 1))
            {
                if ((def.getRewardGlory() > 0) && flag)
                {
                    height = 0x1ec;
                }
                else
                {
                    height = 410;
                }
            }
            else if ((def.getRewardGlory() > 0) && flag)
            {
                height = 270;
            }
            else
            {
                height = 200;
            }
            parent.Size = new Size(550, height);
            this.headerBarImage.Position = new Point(0, 0);
            this.headerBarImage.Size = new Size(base.Width, 30);
            base.addControl(this.headerBarImage);
            this.headerBarImage.Create((Image) GFXLibrary.messageboxtop_left, (Image) GFXLibrary.messageboxtop_middle, (Image) GFXLibrary.messageboxtop_right);
            this.backgroundImage.SpecialGradient = true;
            this.backgroundImage.Position = new Point(0, 30);
            this.backgroundImage.Size = new Size(base.Width, height - 30);
            base.addControl(this.backgroundImage);
            this.captureLabel.Text = SK.Text("QuestLine_Collect_Reward", "Collect Reward");
            this.captureLabel.Color = ARGBColors.White;
            this.captureLabel.Position = new Point(13, 7);
            this.captureLabel.Size = new Size(0x14f, 20);
            this.captureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.captureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.headerBarImage.addControl(this.captureLabel);
            this.strip1.Image = (Image) GFXLibrary.quest_popup_hz_strip_02;
            this.strip1.Position = new Point(4, 4);
            this.backgroundImage.addControl(this.strip1);
            this.questIcon.Image = (Image) GFXLibrary.quest_icons[Math.Min(def.questType, GFXLibrary.quest_icons.Length - 1)];
            this.questIcon.Position = new Point(12, 12);
            this.backgroundImage.addControl(this.questIcon);
            this.fb_questString = this.lblQuestName.Text = SK.NoStoreText("Z_QUESTS_" + def.tagString);
            this.lblQuestName.Color = ARGBColors.Black;
            this.lblQuestName.Position = new Point(70, 0x1a);
            this.lblQuestName.Size = new Size(700, 30);
            this.lblQuestName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            this.lblQuestName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.backgroundImage.addControl(this.lblQuestName);
            this.facebookShareButton.ImageNorm = (Image) GFXLibrary.facebookBlueNorm;
            this.facebookShareButton.ImageOver = (Image) GFXLibrary.facebookBlueOver;
            this.facebookShareButton.ImageClick = (Image) GFXLibrary.facebookBlueClick;
            this.facebookShareButton.Position = new Point(0xeb, height - 0x4b);
            this.facebookShareButton.UseTextSize = true;
            this.facebookShareButton.Text.Text = SK.Text("FACEBOOK_Share", "Share");
            this.facebookShareButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.facebookShareButton.Text.Position = new Point(20, 2);
            this.facebookShareButton.Text.Size = new Size(110, 0x15);
            this.facebookShareButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.facebookShareButton.TextYOffset = 0;
            this.facebookShareButton.Text.Color = ARGBColors.Black;
            this.facebookShareButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.facebookShareClicked));
            this.backgroundImage.addControl(this.facebookShareButton);
            this.strip2.Image = (Image) GFXLibrary.quest_popup_hz_strip_01;
            this.strip2.Position = new Point(0x18, 0x4f);
            this.backgroundImage.addControl(this.strip2);
            NewQuestsPanel.addRewardIcons(this.backgroundImage, new Point(30, 80), def, 0);
            this.collectButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.collectButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.collectButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.collectButton.Position = new Point(0x181, 0x57);
            this.collectButton.Text.Text = SK.Text("QUESTS_Collect", "Collect");
            this.collectButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.collectButton.Text.Color = ARGBColors.Black;
            this.collectButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okClicked), "NewQuests_Collect_Clicked");
            this.backgroundImage.addControl(this.collectButton);
            if (def.reward_charges.Length > 0)
            {
                this.chargesImage.Image = (Image) GFXLibrary.quest_rewards[9];
                this.chargesImage.Position = new Point(0x19, height - 0x52);
                this.chargesImage.CustomTooltipID = 0xc8c;
                this.backgroundImage.addControl(this.chargesImage);
            }
            if (((def.reward_apples > 0) || (def.reward_stone > 0)) || (def.reward_wood > 0))
            {
                if (list.Count > 1)
                {
                    this.strip4.Image = (Image) GFXLibrary.quest_popup_hz_strip_03;
                    this.strip4.Position = new Point(0x18, 0x97);
                    this.backgroundImage.addControl(this.strip4);
                    this.collectButton.Enabled = false;
                    this.villageIcon.Image = (Image) GFXLibrary.char_village_icons[5];
                    this.villageIcon.Position = new Point(30, 0x94);
                    this.backgroundImage.addControl(this.villageIcon);
                    if (Program.mySettings.LanguageIdent == "fr")
                    {
                        this.targetVillageLabel.Text = "Village Cible";
                    }
                    else
                    {
                        this.targetVillageLabel.Text = SK.Text("VillageArmiesPanel_Target_Village", "Target Village");
                    }
                    this.targetVillageLabel.Color = ARGBColors.Black;
                    this.targetVillageLabel.Position = new Point(0, 130);
                    this.targetVillageLabel.Size = new Size(base.Width, 30);
                    this.targetVillageLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.targetVillageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                    this.backgroundImage.addControl(this.targetVillageLabel);
                    this.villageNameLabel.Text = SK.Text("QUESTS_Pick_a_Village", "Pick a Village");
                    this.villageNameLabel.Color = ARGBColors.Black;
                    this.villageNameLabel.Position = new Point(90, 0x92);
                    this.villageNameLabel.Size = new Size(base.Width - 90, this.villageIcon.Height);
                    this.villageNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                    this.villageNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                    this.backgroundImage.addControl(this.villageNameLabel);
                    this.questsScrollArea.Position = new Point(0x3d, 200);
                    this.questsScrollArea.Size = new Size(390, 0x73);
                    this.questsScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(390, 0x73));
                    this.backgroundImage.addControl(this.questsScrollArea);
                    this.insetImage.Position = new Point(0x37, 0xc6);
                    this.insetImage.Size = new Size(440, 0x77);
                    this.backgroundImage.addControl(this.insetImage);
                    this.insetImage.Create((Image) GFXLibrary.quest_popup_inset_top, (Image) GFXLibrary.quest_popup_inset_middle, (Image) GFXLibrary.quest_popup_inset_bottom);
                    this.questsScrollBar.Position = new Point(0x1cd, 0xcd);
                    this.questsScrollBar.Size = new Size(0x18, 0x69);
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
                    this.addVillages(true);
                }
                else if (list.Count > 0)
                {
                    this.m_villageID = list[0];
                }
            }
            if ((def.getRewardGlory() > 0) && flag)
            {
                this.orLabel.Text = SK.Text("QUESTS_or", "Or");
                this.orLabel.Color = ARGBColors.Black;
                this.orLabel.Position = new Point(0, height - 0x91);
                this.orLabel.Size = new Size(base.Width, 30);
                this.orLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
                this.orLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                this.backgroundImage.addControl(this.orLabel);
                this.strip3.Image = (Image) GFXLibrary.quest_popup_hz_strip_01;
                this.strip3.Position = new Point(0x18, (height - 0x7b) - 1);
                this.backgroundImage.addControl(this.strip3);
                NewQuestsPanel.addRewardIcons(this.backgroundImage, new Point(30, height - 0x7b), def, -1);
                this.collectGloryButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.collectGloryButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.collectGloryButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.collectGloryButton.Position = new Point(0x13c, ((height - 0x7b) + 8) - 1);
                this.collectGloryButton.Text.Text = SK.Text("QUESTS_Collect_Glory", "Collect Glory");
                this.collectGloryButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
                this.collectGloryButton.Text.Color = ARGBColors.Black;
                this.collectGloryButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okGloryClicked), "NewQuests_Collect_Glory_Clicked");
                this.backgroundImage.addControl(this.collectGloryButton);
            }
            this.cancelButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
            this.cancelButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
            this.cancelButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
            this.cancelButton.Position = new Point(0x181, height - 0x4b);
            this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.cancelButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
            this.cancelButton.Text.Color = ARGBColors.Black;
            this.cancelButton.setClickDelegate(delegate {
                InterfaceMgr.Instance.closeNewQuestRewardPopup();
                InterfaceMgr.Instance.ParentForm.TopMost = true;
                InterfaceMgr.Instance.ParentForm.TopMost = false;
            }, "NewQuests_Cancel");
            this.backgroundImage.addControl(this.cancelButton);
            base.Invalidate();
            parent.Invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.Name = "NewQuestRewardPanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
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

        public void okClicked()
        {
            this.m_questDef = NewQuests.getNewQuestDef(this.m_questID);
            VillageMap map = GameEngine.Instance.getVillage(this.m_villageID);
            if (map == null)
            {
                this.confirmAvailableSpace();
            }
            else
            {
                VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
                VillageMap.GranaryLevels levels2 = new VillageMap.GranaryLevels();
                map.getStockpileLevels(levels);
                map.getGranaryLevels(levels2);
                bool flag = false;
                double num = 0.0;
                double num2 = 0.0;
                if (this.m_questDef.reward_apples > 0)
                {
                    num2 = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, 0x12, false) * CardTypes.getResourceCapMultiplier(0x12, GameEngine.Instance.World.UserCardData);
                    num = num2 - levels2.fishLevel;
                    if (Convert.ToInt32(num) < this.m_questDef.reward_apples)
                    {
                        flag = true;
                    }
                }
                if ((this.m_questDef.reward_stone > 0) && !flag)
                {
                    num2 = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, 7, false) * CardTypes.getResourceCapMultiplier(7, GameEngine.Instance.World.UserCardData);
                    num = num2 - levels.stoneLevel;
                    if (Convert.ToInt32(num) < this.m_questDef.reward_stone)
                    {
                        flag = true;
                    }
                }
                if ((this.m_questDef.reward_wood > 0) && !flag)
                {
                    num2 = GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, 6, false) * CardTypes.getResourceCapMultiplier(6, GameEngine.Instance.World.UserCardData);
                    num = num2 - levels.woodLevel;
                    if (Convert.ToInt32(num) < this.m_questDef.reward_wood)
                    {
                        flag = true;
                    }
                }
                if (!flag || (MyMessageBox.Show(SK.Text("Quest_Reward_Insufficient_Space", "You do not have enough room to store all of the reward at this village. Are you sure you want to send the reward to this village?"), SK.Text("Quest_Reward_Insufficient_Space_header", "Insufficient Space"), MessageBoxButtons.YesNo) != DialogResult.No))
                {
                    this.CompleteQuest();
                }
            }
        }

        public void okGloryClicked()
        {
            if (MyMessageBox.Show(SK.Text("Quest_Reward_Glory_Confirm", "If you select this option you will receive glory points, but no other rewards. Do you wish to continue?"), SK.Text("Quest_Reward_Glory_Title", "Confirm Selection"), MessageBoxButtons.YesNo) != DialogResult.No)
            {
                this.StillRecieveGloryPoints();
            }
        }

        private void StillRecieveGloryPoints()
        {
            this.m_questPanel.doCompleteQuest(this.m_questID, -1, true);
            InterfaceMgr.Instance.closeNewQuestRewardPopup();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        public void update()
        {
        }

        public void villageSelected(int villageID)
        {
            this.villageNameLabel.Text = GameEngine.Instance.World.getVillageName(villageID);
            this.collectButton.Enabled = true;
            this.m_villageID = villageID;
            int index = GameEngine.Instance.World.getVillageSize(villageID);
            this.villageIcon.Image = (Image) GFXLibrary.char_village_icons[index];
            int num2 = this.questsScrollBar.Value;
            this.addVillages(false);
            this.questsScrollBar.Value = num2;
            this.questsScrollBar.scrollDown(0);
            this.wallScrollBarMoved();
            base.Invalidate();
        }

        private void wallScrollBarMoved()
        {
            int y = this.questsScrollBar.Value;
            this.questsScrollArea.Position = new Point(this.questsScrollArea.X, 200 - y);
            this.questsScrollArea.ClipRect = new Rectangle(this.questsScrollArea.ClipRect.X, y, this.questsScrollArea.ClipRect.Width, this.questsScrollArea.ClipRect.Height);
            this.questsScrollArea.invalidate();
            this.questsScrollBar.invalidate();
            this.insetImage.invalidate();
        }

        public class NewQuestVillageLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
            private NewQuestRewardPanel m_parent;
            private int m_villageID = -1;
            private CustomSelfDrawPanel.CSDImage villageIcon = new CustomSelfDrawPanel.CSDImage();
            private CustomSelfDrawPanel.CSDLabel villageName = new CustomSelfDrawPanel.CSDLabel();

            public void init(int villageID, NewQuestRewardPanel parent, int position, bool selected)
            {
                this.m_villageID = villageID;
                this.m_parent = parent;
                this.clearControls();
                if (selected)
                {
                    this.backgroundImage.Image = (Image) GFXLibrary.quest_popup_inset_highlight;
                    this.backgroundImage.Position = new Point(0, 5);
                    this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                    base.addControl(this.backgroundImage);
                }
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                this.Size = new Size(390, 0x22);
                int index = GameEngine.Instance.World.getVillageSize(villageID);
                this.villageIcon.Image = (Image) GFXLibrary.char_village_icons[index];
                this.villageIcon.Position = new Point(0, -8);
                this.villageIcon.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                base.addControl(this.villageIcon);
                this.villageName.Text = GameEngine.Instance.World.getVillageName(villageID);
                this.villageName.Color = ARGBColors.Black;
                this.villageName.Position = new Point(50, 0);
                this.villageName.Size = new Size(330, base.Height);
                this.villageName.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.villageName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.villageName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
                base.addControl(this.villageName);
            }

            private void lineClicked()
            {
                GameEngine.Instance.playInterfaceSound("NewQuests_Village_Clicked");
                this.m_parent.villageSelected(this.m_villageID);
            }

            public bool update(double localTime)
            {
                return true;
            }
        }
    }
}

