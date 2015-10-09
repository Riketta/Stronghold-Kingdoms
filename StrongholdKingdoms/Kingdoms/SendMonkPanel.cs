namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class SendMonkPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton actionButton1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton actionButton7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage arrowImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundBottomEdge = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage backgroundRightEdge = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage buttonIndentImage = new CustomSelfDrawPanel.CSDImage();
        private CardBarGDI cardbar = new CardBarGDI();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton closeInfluenceButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private CustomSelfDrawPanel.CSDLabel costLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel costValueLabel = new CustomSelfDrawPanel.CSDLabel();
        private int currentCommand = -1;
        private int currentPointsCost;
        private bool excommunicated;
        private DateTime excommunicationTime = DateTime.MinValue;
        private CustomSelfDrawPanel.CSDImage gfxImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel influenceHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage influenceIndent = new CustomSelfDrawPanel.CSDImage();
        private bool inLaunch;
        private DateTime lastLaunchTime = DateTime.MinValue;
        private int lastMax = -1;
        private bool launchAllowed;
        private CustomSelfDrawPanel.CSDButton launchButton = new CustomSelfDrawPanel.CSDButton();
        private List<MonkVoteLine> lineList = new List<MonkVoteLine>();
        private int m_ownVillage = -1;
        private int m_selectedVillage = -1;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private int maxMonks;
        private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
        private CustomSelfDrawPanel.CSDButton negativeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel numLabel = new CustomSelfDrawPanel.CSDLabel();
        private ParishMemberComparer parishMemberComparer = new ParishMemberComparer();
        private List<ParishMember> parishMembers = new List<ParishMember>();
        private CustomSelfDrawPanel.CSDButton positiveButton = new CustomSelfDrawPanel.CSDButton();
        private bool positiveInfluence = true;
        private CustomSelfDrawPanel.CSDArea scrollArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
        private bool sliderEnabled;
        private CustomSelfDrawPanel.CSDTrackBar sliderImage = new CustomSelfDrawPanel.CSDTrackBar();
        private double storedPreCardDistance;
        private bool targetCapital;
        private CustomSelfDrawPanel.CSDImage targetImage = new CustomSelfDrawPanel.CSDImage();
        private int targetUserRank = -1;
        private CustomSelfDrawPanel.CSDLabel timeLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage titleImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel tooltipLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel villageActionLabel = new CustomSelfDrawPanel.CSDLabel();
        private int voteCap = 0x186a0;
        private int votedUser = -1;

        public SendMonkPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void addPlayers()
        {
            this.scrollArea.clearControls();
            this.lineList.Clear();
            int y = 0;
            this.scrollBar.Visible = false;
            if (this.parishMembers != null)
            {
                foreach (ParishMember member in this.parishMembers)
                {
                    if (y != 0)
                    {
                        CustomSelfDrawPanel.CSDLine line = new CustomSelfDrawPanel.CSDLine {
                            Position = new Point(0, y - 1),
                            LineColor = Color.FromArgb(60, 60, 60),
                            Size = new Size(0x181, 0)
                        };
                        this.scrollArea.addControl(line);
                    }
                    MonkVoteLine control = new MonkVoteLine {
                        Position = new Point(0, y)
                    };
                    control.init(member.userName, member.userID, member.rank, member.points, true, member.numSpareVotes, member.numVotesReceived, member.factionID, this.votedUser, this);
                    this.scrollArea.addControl(control);
                    y += control.Height;
                    this.lineList.Add(control);
                }
                if (y > 300)
                {
                    this.scrollBar.Visible = true;
                    this.scrollBar.Max = y - 300;
                }
            }
            this.scrollArea.invalidate();
            this.influenceIndent.invalidate();
        }

        public void changeCommand()
        {
            if (base.ClickedControl != null)
            {
                CustomSelfDrawPanel.CSDButton clickedControl = (CustomSelfDrawPanel.CSDButton) base.ClickedControl;
                int data = clickedControl.Data;
                this.updateButtons(data);
            }
        }

        private void closeClick()
        {
            InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
            InterfaceMgr.Instance.closeSendMonkWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        private void closeInfluenceClick()
        {
            this.updateButtons(this.currentCommand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void getCountyElectionInfoCallback(GetCountyElectionInfo_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (this.parishMembers == null)
                {
                    this.parishMembers = new List<ParishMember>();
                }
                else
                {
                    this.parishMembers.Clear();
                }
                if (returnData.countyMembers != null)
                {
                    this.parishMembers.AddRange(returnData.countyMembers);
                }
                this.parishMembers.Sort(this.parishMemberComparer);
                if (this.parishMembers.Count > 0)
                {
                    this.votedUser = this.parishMembers[0].userID;
                }
                this.voteCap = returnData.voteCap;
                this.addPlayers();
            }
        }

        public void getExcommunicationStatusCallback(GetExcommunicationStatus_ReturnType returnData)
        {
            if (returnData.Success)
            {
                this.targetUserRank = returnData.targetUserRank;
                this.excommunicated = returnData.excommunicated;
                this.excommunicationTime = returnData.excommunicationTime;
                if (this.excommunicated)
                {
                    this.launchButton.Enabled = false;
                    this.updateButtons(-1);
                    this.tooltipLabel.Text = SK.Text("SendMonksPanel_You_Are_Excommunicated", "You are Excommunicated, you cannot issue any commands.") + " " + SK.Text("SendMonksPanel_Excommunication_Expires_in", "Excommunication Expires in") + " :";
                }
            }
        }

        public void getParishMembersListCallback(GetParishMembersList_ReturnType returnData)
        {
            if (returnData.Success)
            {
                if (this.parishMembers == null)
                {
                    this.parishMembers = new List<ParishMember>();
                }
                else
                {
                    this.parishMembers.Clear();
                }
                if (returnData.parishMembers != null)
                {
                    this.parishMembers.AddRange(returnData.parishMembers);
                }
                this.parishMembers.Sort(this.parishMemberComparer);
                if (this.parishMembers.Count > 0)
                {
                    this.votedUser = this.parishMembers[0].userID;
                }
                this.voteCap = returnData.voteCap;
                this.addPlayers();
            }
        }

        public void init(int villageID)
        {
            this.m_selectedVillage = villageID;
            this.m_ownVillage = InterfaceMgr.Instance.OwnSelectedVillage;
            base.clearControls();
            int y = 0x27;
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), base.Size);
            this.mainBackgroundImage.Position = new Point(0, y);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.backgroundBottomEdge.Image = (Image) GFXLibrary.popup_border_bottom;
            this.backgroundBottomEdge.Position = new Point(0, base.Height - 2);
            base.addControl(this.backgroundBottomEdge);
            this.backgroundRightEdge.Image = (Image) GFXLibrary.popup_border_rhs;
            this.backgroundRightEdge.Position = new Point(base.Width - 2, y);
            base.addControl(this.backgroundRightEdge);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x293, 5);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendMonkPanel_close");
            this.titleImage.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this.titleImage, 0x23, new Point(0x261, 5));
            this.cardbar.Position = new Point(0, 4);
            this.mainBackgroundImage.addControl(this.cardbar);
            this.cardbar.init(8);
            this.gfxImage.Image = (Image) GFXLibrary.illustration_monks;
            this.gfxImage.Position = new Point(0x19, 0x4d);
            this.mainBackgroundImage.addControl(this.gfxImage);
            this.sliderImage.Position = new Point(0x25, 0x130);
            this.sliderImage.Margin = new Rectangle(0x20, 0x3f, 0x20, 0x19);
            this.sliderImage.Value = 0;
            this.sliderImage.Max = 0;
            this.sliderImage.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
            this.mainBackgroundImage.addControl(this.sliderImage);
            this.sliderImage.Create((Image) GFXLibrary.monk_screen_slider, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar, (Image) GFXLibrary.scout_screen_slider_bar);
            this.arrowImage.Image = (Image) GFXLibrary.scout_screen_arrowbox;
            this.arrowImage.Position = new Point(0xdb, 0x130);
            this.mainBackgroundImage.addControl(this.arrowImage);
            this.buttonIndentImage.Image = (Image) GFXLibrary.monk_screen_buttongroup_inset;
            this.buttonIndentImage.Position = new Point(0x1f7, 0x4d);
            this.mainBackgroundImage.addControl(this.buttonIndentImage);
            this.influenceIndent.Image = (Image) GFXLibrary.monk_screen_playerlist_inset;
            this.influenceIndent.Position = new Point(0x19, 0x4d);
            this.influenceIndent.Visible = false;
            this.mainBackgroundImage.addControl(this.influenceIndent);
            this.villageActionLabel.Text = GameEngine.Instance.World.getVillageNameOrType(villageID);
            this.villageActionLabel.Color = ARGBColors.White;
            this.villageActionLabel.DropShadowColor = ARGBColors.Black;
            this.villageActionLabel.Position = new Point(0x24, 0xf3);
            this.villageActionLabel.Size = new Size(430, 30);
            this.villageActionLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.villageActionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.mainBackgroundImage.addControl(this.villageActionLabel);
            this.tooltipLabel.Text = "";
            this.tooltipLabel.Color = ARGBColors.White;
            this.tooltipLabel.DropShadowColor = ARGBColors.Black;
            this.tooltipLabel.Position = new Point(0x24, 270);
            this.tooltipLabel.Size = new Size(430, 0x20);
            this.tooltipLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.tooltipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.mainBackgroundImage.addControl(this.tooltipLabel);
            this.costLabel.Text = SK.Text("SendMonksPanel_Faith_Points_Cost", "Faith Points Cost");
            this.costLabel.Color = ARGBColors.White;
            this.costLabel.DropShadowColor = ARGBColors.Black;
            this.costLabel.Position = new Point(0x1c4, 0x166);
            this.costLabel.Size = new Size(180, 0x20);
            this.costLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.costLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.mainBackgroundImage.addControl(this.costLabel);
            this.costValueLabel.Text = "0";
            this.costValueLabel.Color = Color.FromArgb(0x12, 0xff, 0);
            this.costValueLabel.DropShadowColor = ARGBColors.Black;
            this.costValueLabel.Position = new Point(0x27b, 0x166);
            this.costValueLabel.Size = new Size(60, 0x20);
            this.costValueLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.costValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.mainBackgroundImage.addControl(this.costValueLabel);
            this.numLabel.Text = "";
            this.numLabel.Color = ARGBColors.White;
            this.numLabel.DropShadowColor = ARGBColors.Black;
            this.numLabel.Position = new Point(0x3f, 0x17);
            this.numLabel.Size = new Size(0x3b, 0x18);
            this.numLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
            this.numLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.sliderImage.addControl(this.numLabel);
            this.timeLabel.Text = "00:00:00";
            this.timeLabel.Color = ARGBColors.White;
            this.timeLabel.DropShadowColor = ARGBColors.Black;
            this.timeLabel.Position = new Point(-28, 0x17);
            this.timeLabel.Size = new Size(0xbf, 0x18);
            this.timeLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.timeLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.arrowImage.addControl(this.timeLabel);
            this.updateButtons(-1);
            this.actionButton1.Position = new Point(0x30, 4);
            this.actionButton1.Data = 2;
            this.actionButton1.CustomTooltipID = 0x7d0;
            this.actionButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_influence");
            this.buttonIndentImage.addControl(this.actionButton1);
            this.actionButton2.Position = new Point(14, 0x3e);
            this.actionButton2.Data = 4;
            this.actionButton2.CustomTooltipID = 0x7d3;
            this.actionButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_interdicts");
            this.buttonIndentImage.addControl(this.actionButton2);
            this.actionButton3.Position = new Point(0x58, 0x3e);
            this.actionButton3.Data = 5;
            this.actionButton3.CustomTooltipID = 0x7d4;
            this.actionButton3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_restoration");
            this.buttonIndentImage.addControl(this.actionButton3);
            this.actionButton4.Position = new Point(14, 0x81);
            this.actionButton4.Data = 1;
            this.actionButton4.CustomTooltipID = 0x7d1;
            this.actionButton4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_blessing");
            this.buttonIndentImage.addControl(this.actionButton4);
            this.actionButton5.Position = new Point(0x58, 0x81);
            this.actionButton5.Data = 3;
            this.actionButton5.CustomTooltipID = 0x7d2;
            this.actionButton5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_inquistion");
            this.buttonIndentImage.addControl(this.actionButton5);
            this.actionButton6.Position = new Point(14, 0xc4);
            this.actionButton6.Data = 6;
            this.actionButton6.CustomTooltipID = 0x7d5;
            this.actionButton6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_absolution");
            this.buttonIndentImage.addControl(this.actionButton6);
            this.actionButton7.Position = new Point(0x58, 0xc4);
            this.actionButton7.Data = 7;
            this.actionButton7.CustomTooltipID = 0x7d6;
            this.actionButton7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.changeCommand), "SendMonkPanel_excommunication");
            this.buttonIndentImage.addControl(this.actionButton7);
            int index = 0;
            switch (GameEngine.Instance.World.getSpecial(villageID))
            {
                case 3:
                case 4:
                    index = 0x18;
                    break;

                case 5:
                case 6:
                    index = 0x19;
                    break;

                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    index = 0x1c;
                    break;

                case 15:
                case 0x10:
                case 0x11:
                case 0x12:
                    index = 0x35;
                    break;

                case 40:
                case 0x29:
                case 0x2a:
                case 0x2b:
                case 0x2c:
                case 0x2d:
                case 0x2e:
                case 0x2f:
                case 0x30:
                case 0x31:
                case 50:
                    index = 0x36;
                    break;

                case 0x33:
                case 0x34:
                case 0x35:
                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                case 0x3a:
                case 0x3b:
                case 60:
                    index = 0x37;
                    break;

                case 0x3d:
                case 0x3e:
                case 0x3f:
                case 0x40:
                case 0x41:
                case 0x42:
                case 0x43:
                case 0x44:
                case 0x45:
                case 70:
                    index = 0x38;
                    break;

                case 0x47:
                case 0x48:
                case 0x49:
                case 0x4a:
                case 0x4b:
                case 0x4c:
                case 0x4d:
                case 0x4e:
                case 0x4f:
                case 80:
                    index = 0x39;
                    break;

                case 0x51:
                case 0x52:
                case 0x53:
                case 0x54:
                case 0x55:
                case 0x56:
                case 0x57:
                case 0x58:
                case 0x59:
                case 90:
                    index = 0x3a;
                    break;

                case 100:
                    index = 0x1d;
                    break;

                case 0x6a:
                    index = 30;
                    break;

                case 0x6b:
                    index = 0x1f;
                    break;

                case 0x6c:
                    index = 0x21;
                    break;

                case 0x6d:
                    index = 0x20;
                    break;

                case 0x70:
                    index = 0x22;
                    break;

                case 0x71:
                    index = 0x23;
                    break;

                case 0x72:
                    index = 0x24;
                    break;

                case 0x73:
                    index = 0x29;
                    break;

                case 0x74:
                    index = 0x25;
                    break;

                case 0x75:
                    index = 40;
                    break;

                case 0x76:
                    index = 0x2a;
                    break;

                case 0x77:
                    index = 0x2d;
                    break;

                case 0x79:
                    index = 0x2c;
                    break;

                case 0x7a:
                    index = 0x26;
                    break;

                case 0x7b:
                    index = 0x2b;
                    break;

                case 0x7c:
                    index = 0x2e;
                    break;

                case 0x7d:
                    index = 0x2f;
                    break;

                case 0x7e:
                    index = 0x30;
                    break;

                case 0x85:
                    index = 0x27;
                    break;

                default:
                    if (GameEngine.Instance.World.isRegionCapital(villageID))
                    {
                        index = 0x31;
                    }
                    else if (GameEngine.Instance.World.isCountyCapital(villageID))
                    {
                        index = 50;
                    }
                    else if (GameEngine.Instance.World.isProvinceCapital(villageID))
                    {
                        index = 0x33;
                    }
                    else if (GameEngine.Instance.World.isCountryCapital(villageID))
                    {
                        index = 0x34;
                    }
                    else
                    {
                        index = GameEngine.Instance.World.getVillageSize(villageID);
                    }
                    break;
            }
            this.targetImage.Image = (Image) GFXLibrary.scout_screen_icons[index];
            this.targetImage.Position = new Point(0xb5, 5);
            this.arrowImage.addControl(this.targetImage);
            this.scrollArea.Position = new Point(0x19, 0x24);
            this.scrollArea.Size = new Size(0x181, 300);
            this.scrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(0x181, 300));
            this.influenceIndent.addControl(this.scrollArea);
            this.mouseWheelOverlay.Position = this.scrollArea.Position;
            this.mouseWheelOverlay.Size = this.scrollArea.Size;
            this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
            this.influenceIndent.addControl(this.mouseWheelOverlay);
            this.scrollBar.Position = new Point(0x1a7, 0x2f);
            this.scrollBar.Size = new Size(0x20, 0x120);
            this.influenceIndent.addControl(this.scrollBar);
            this.scrollBar.Value = 0;
            this.scrollBar.Max = 0;
            this.scrollBar.NumVisibleLines = 300;
            this.scrollBar.Create(null, null, null, (Image) GFXLibrary.scroll_thumb_top, (Image) GFXLibrary.scroll_thumb_mid, (Image) GFXLibrary.scroll_thumb_bottom);
            this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
            this.closeInfluenceButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeInfluenceButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeInfluenceButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeInfluenceButton.Position = new Point(0x19f, 1);
            this.closeInfluenceButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeInfluenceClick), "SendMonkPanel_close_influence");
            this.influenceIndent.addControl(this.closeInfluenceButton);
            this.positiveButton.ImageNorm = (Image) GFXLibrary.monk_screen_button_array[0];
            this.positiveButton.ImageOver = (Image) GFXLibrary.monk_screen_button_array[2];
            this.positiveButton.ImageClick = (Image) GFXLibrary.monk_screen_button_array[4];
            this.positiveButton.Position = new Point(350, 6);
            this.positiveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.positiveClick), "SendMonkPanel_influence_positive");
            this.influenceIndent.addControl(this.positiveButton);
            this.negativeButton.ImageNorm = (Image) GFXLibrary.monk_screen_button_array[1];
            this.negativeButton.ImageOver = (Image) GFXLibrary.monk_screen_button_array[3];
            this.negativeButton.ImageClick = (Image) GFXLibrary.monk_screen_button_array[5];
            this.negativeButton.Position = new Point(380, 6);
            this.negativeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.negativeClick), "SendMonkPanel_influence_negative");
            this.influenceIndent.addControl(this.negativeButton);
            this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_positive", "Select Player to Positively Influence");
            this.influenceHeaderLabel.Color = ARGBColors.White;
            this.influenceHeaderLabel.DropShadowColor = ARGBColors.Black;
            this.influenceHeaderLabel.Position = new Point(15, 4);
            this.influenceHeaderLabel.Size = new Size(0x152, 0x1c);
            this.influenceHeaderLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
            this.influenceHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.influenceIndent.addControl(this.influenceHeaderLabel);
            WorldData localWorldData = GameEngine.Instance.LocalWorldData;
            Point point = GameEngine.Instance.World.getVillageLocation(InterfaceMgr.Instance.OwnSelectedVillage);
            Point point2 = GameEngine.Instance.World.getVillageLocation(villageID);
            int x = point.X;
            int num5 = point.Y;
            int num6 = point2.X;
            int num7 = point2.Y;
            double d = ((x - num6) * (x - num6)) + ((num5 - num7) * (num5 - num7));
            d = Math.Sqrt(d) * (GameEngine.Instance.LocalWorldData.PriestMoveSpeed * GameEngine.Instance.LocalWorldData.gamePlaySpeed);
            d = GameEngine.Instance.World.UserResearchData.adjustPriestTimes(d);
            this.storedPreCardDistance = d;
            d *= CardTypes.adjustMonkSpeed(GameEngine.Instance.World.UserCardData);
            string str = VillageMap.createBuildTimeString((int) d);
            this.timeLabel.Text = str;
            this.timeLabel.CustomTooltipID = 0x4e20;
            this.timeLabel.CustomTooltipData = (int) d;
            this.launchButton.ImageNorm = (Image) GFXLibrary.button_with_inset_normal;
            this.launchButton.ImageOver = (Image) GFXLibrary.button_with_inset_over;
            this.launchButton.ImageClick = (Image) GFXLibrary.button_with_inset_pushed;
            this.launchButton.Position = new Point(520, 0x179);
            this.launchButton.Text.Text = SK.Text("ScoutPopup_Go", "Go");
            this.launchButton.Text.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
            this.launchButton.TextYOffset = 1;
            this.launchButton.Text.Color = ARGBColors.Black;
            this.launchButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launch), "SendMonkPanel_launch");
            this.launchButton.Enabled = false;
            this.mainBackgroundImage.addControl(this.launchButton);
            this.targetCapital = false;
            bool flag = GameEngine.Instance.World.isCapital(this.m_selectedVillage);
            bool flag2 = false;
            if (flag)
            {
                this.targetCapital = true;
                if (GameEngine.Instance.World.isRegionCapital(this.m_selectedVillage))
                {
                    flag2 = true;
                }
            }
            if (flag)
            {
                if ((GameEngine.Instance.World.UserResearchData.Research_Confirmation > 0) && flag2)
                {
                    this.actionButton5.Enabled = true;
                }
                else
                {
                    this.actionButton5.Enabled = false;
                }
                if ((GameEngine.Instance.World.UserResearchData.Research_Marriage > 0) && flag2)
                {
                    this.actionButton4.Enabled = true;
                }
                else
                {
                    this.actionButton4.Enabled = false;
                }
                if ((GameEngine.Instance.World.UserResearchData.Research_Baptism > 0) && flag2)
                {
                    this.actionButton3.Enabled = true;
                }
                else
                {
                    this.actionButton3.Enabled = false;
                }
                if ((GameEngine.Instance.World.UserResearchData.Research_Ordination > 0) && (flag2 || ((GameEngine.Instance.World.SecondAgeWorld || (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)) && GameEngine.Instance.World.isCountyCapital(this.m_selectedVillage))))
                {
                    this.actionButton1.Enabled = true;
                }
                else
                {
                    this.actionButton1.Enabled = false;
                }
                this.actionButton6.Enabled = false;
                this.actionButton7.Enabled = false;
            }
            else
            {
                this.actionButton5.Enabled = false;
                this.actionButton4.Enabled = false;
                this.actionButton3.Enabled = false;
                this.actionButton1.Enabled = false;
                if ((GameEngine.Instance.World.UserResearchData.Research_Confession > 0) && (this.m_ownVillage != this.m_selectedVillage))
                {
                    this.actionButton6.Enabled = true;
                }
                else
                {
                    this.actionButton6.Enabled = false;
                }
                if ((GameEngine.Instance.World.UserResearchData.Research_ExtremeUnction > 0) && (this.m_ownVillage != this.m_selectedVillage))
                {
                    this.actionButton7.Enabled = true;
                }
                else
                {
                    this.actionButton7.Enabled = false;
                }
            }
            if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
            {
                this.actionButton2.Enabled = false;
            }
            else if (GameEngine.Instance.World.UserResearchData.Research_Eucharist > 0)
            {
                this.actionButton2.Enabled = true;
            }
            else
            {
                this.actionButton2.Enabled = false;
            }
            this.titleImage.Image = (Image) GFXLibrary.popup_title_bar;
            this.titleImage.Position = new Point(0, 0);
            base.addControl(this.titleImage);
            this.titleLabel.Text = SK.Text("GENERIC_Send_Monks", "Send Monks");
            this.titleLabel.Color = ARGBColors.White;
            this.titleLabel.DropShadowColor = ARGBColors.Black;
            this.titleLabel.Position = new Point(20, 5);
            this.titleLabel.Size = new Size(base.Width, 0x20);
            this.titleLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
            this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.titleImage.addControl(this.titleLabel);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x293, 5);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "SendMonkPanel_close");
            this.titleImage.addControl(this.closeButton);
            RemoteServices.Instance.set_GetExcommunicationStatus_UserCallBack(new RemoteServices.GetExcommunicationStatus_UserCallBack(this.getExcommunicationStatusCallback));
            RemoteServices.Instance.GetExcommunicationStatus(this.m_ownVillage, this.m_selectedVillage);
            if (flag)
            {
                if (GameEngine.Instance.World.isRegionCapital(this.m_selectedVillage))
                {
                    RemoteServices.Instance.set_GetParishMembersList_UserCallBack(new RemoteServices.GetParishMembersList_UserCallBack(this.getParishMembersListCallback));
                    RemoteServices.Instance.GetParishMembersList(this.m_selectedVillage);
                }
                else if (GameEngine.Instance.World.isCountyCapital(this.m_selectedVillage))
                {
                    RemoteServices.Instance.set_GetCountyElectionInfo_UserCallBack(new RemoteServices.GetCountyElectionInfo_UserCallBack(this.getCountyElectionInfoCallback));
                    RemoteServices.Instance.GetCountyElectionInfo(this.m_selectedVillage);
                }
            }
            if (GameEngine.Instance.getVillage(this.m_ownVillage) != null)
            {
                this.onVillageLoadUpdate(this.m_ownVillage, true);
            }
            else
            {
                GameEngine.Instance.downloadCurrentVillage();
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void launch()
        {
            if (this.sliderEnabled)
            {
                if (this.inLaunch)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - this.lastLaunchTime);
                    if (span.TotalSeconds < 20.0)
                    {
                        return;
                    }
                }
                this.inLaunch = true;
                this.lastLaunchTime = DateTime.Now;
                if (this.sendMonks())
                {
                    this.launchButton.Enabled = false;
                    this.closeButton.Enabled = false;
                    CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, base.ParentForm);
                }
                else
                {
                    this.inLaunch = false;
                }
            }
        }

        private void mouseWheelMoved(int delta)
        {
            if (delta < 0)
            {
                this.scrollBar.scrollDown(6);
            }
            else if (delta > 0)
            {
                this.scrollBar.scrollUp(6);
            }
        }

        private void negativeClick()
        {
            this.positiveInfluence = false;
            this.villageActionLabel.Text = SK.Text("SendMonksPanel_Negative_Influence", "Negative Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
            this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_negative", "Select Player to Negatively Influence");
            int num = this.sliderImage.Value + 1;
            if (this.maxMonks == 0)
            {
                num = 0;
            }
            int num3 = CardTypes.getInfluenceMultipier(GameEngine.Instance.World.UserCardData) * num;
            this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + " (" + num3.ToString() + " ";
            if (num3 != 1)
            {
                this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + SK.Text("SendMonksPanel_X_Votes", "votes");
            }
            else
            {
                this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + SK.Text("SendMonksPanel_X_Vote", "vote");
            }
            this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + ")";
        }

        public void onVillageLoadUpdate(int villageID, bool initial)
        {
            if (!this.inLaunch && ((this.m_ownVillage == villageID) && (GameEngine.Instance.getVillage(this.m_ownVillage) != null)))
            {
                int athome = 0;
                GameEngine.Instance.World.countVillagePeople(this.m_ownVillage, 4, ref athome);
                if (!GameEngine.Instance.World.userResearchData.canCreateMonks())
                {
                    athome = 0;
                }
                this.maxMonks = athome;
                if (initial)
                {
                    if (athome > 0)
                    {
                        if (!this.excommunicated)
                        {
                            this.launchButton.Enabled = true;
                            this.launchAllowed = true;
                        }
                        else
                        {
                            this.launchButton.Enabled = false;
                        }
                        this.sliderImage.Max = athome - 1;
                        this.sliderImage.Value = 0;
                        this.sliderEnabled = true;
                    }
                    else
                    {
                        this.sliderImage.Value = 0;
                        this.sliderImage.Max = 0;
                        this.sliderEnabled = false;
                        this.launchButton.Enabled = false;
                    }
                    base.Invalidate();
                    this.tracksMoved();
                }
                else if (athome != this.lastMax)
                {
                    if (athome > this.lastMax)
                    {
                        this.sliderImage.Max = athome - 1;
                        if (this.lastMax <= 0)
                        {
                            this.sliderImage.Value = athome - 1;
                        }
                    }
                    else
                    {
                        int num2 = this.sliderImage.Value + 1;
                        if (num2 > athome)
                        {
                            this.sliderImage.Value = athome - 1;
                            this.sliderImage.Max = athome - 1;
                        }
                        else
                        {
                            this.sliderImage.Max = athome - 1;
                        }
                    }
                    if ((athome == 0) || this.excommunicated)
                    {
                        this.launchButton.Enabled = false;
                    }
                    else
                    {
                        this.launchButton.Enabled = true;
                        this.launchAllowed = true;
                    }
                    this.sliderEnabled = this.launchButton.Enabled;
                    base.Invalidate();
                    this.tracksMoved();
                }
                this.lastMax = athome;
                this.addPlayers();
            }
        }

        private void positiveClick()
        {
            this.positiveInfluence = true;
            this.villageActionLabel.Text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
            this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_positive", "Select Player to Positively Influence");
            int num = this.sliderImage.Value + 1;
            if (this.maxMonks == 0)
            {
                num = 0;
            }
            int num3 = CardTypes.getInfluenceMultipier(GameEngine.Instance.World.UserCardData) * num;
            this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + " (" + num3.ToString() + " ";
            if (num3 != 1)
            {
                this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + SK.Text("SendMonksPanel_X_Votes", "votes");
            }
            else
            {
                this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + SK.Text("SendMonksPanel_X_Vote", "vote");
            }
            this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + ")";
        }

        public void radioClicked(int clickedUserID)
        {
            this.votedUser = clickedUserID;
            foreach (MonkVoteLine line in this.lineList)
            {
                line.setState(this.votedUser);
            }
        }

        private void scrollBarMoved()
        {
            int y = this.scrollBar.Value;
            this.scrollArea.Position = new Point(this.scrollArea.X, 0x24 - y);
            this.scrollArea.ClipRect = new Rectangle(this.scrollArea.ClipRect.X, y, this.scrollArea.ClipRect.Width, this.scrollArea.ClipRect.Height);
            this.scrollArea.invalidate();
            this.influenceIndent.invalidate();
        }

        public bool sendMonks()
        {
            int number = this.sliderImage.Value + 1;
            if (number <= 0)
            {
                return false;
            }
            int data = -1;
            if ((this.currentCommand == 2) && (this.votedUser < 0))
            {
                return false;
            }
            if (this.currentCommand == 2)
            {
                if (!this.positiveInfluence)
                {
                    this.currentCommand = 8;
                }
                data = this.votedUser;
            }
            if (this.currentCommand == 2)
            {
                foreach (ParishMember member in this.parishMembers)
                {
                    if (member.userID == this.votedUser)
                    {
                        if ((member.numVotesReceived + number) <= this.voteCap)
                        {
                            break;
                        }
                        MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
                        if (MyMessageBox.Show(SK.Text("SendMonksPanel_Are_You_Sure_positive", "Are you sure? This Positive Influence may waste monks."), SK.Text("SendMonksPanel_Confirm_Influence", "Confirm Influence"), yesNo) == DialogResult.Yes)
                        {
                            break;
                        }
                        return false;
                    }
                }
            }
            else if (this.currentCommand == 8)
            {
                foreach (ParishMember member2 in this.parishMembers)
                {
                    if (member2.userID == this.votedUser)
                    {
                        if ((member2.numVotesReceived - number) >= 0)
                        {
                            break;
                        }
                        MessageBoxButtons buts = MessageBoxButtons.YesNo;
                        if (MyMessageBox.Show(SK.Text("SendMonksPanel_Are_You_Sure_Negative", "Are you sure? This Negative Influence may waste monks."), SK.Text("SendMonksPanel_Confirm_Influence", "Confirm Influence"), buts) == DialogResult.Yes)
                        {
                            break;
                        }
                        return false;
                    }
                }
            }
            RemoteServices.Instance.set_SendPeople_UserCallBack(new RemoteServices.SendPeople_UserCallBack(this.sendPeopleCallback));
            RemoteServices.Instance.SendPeople(this.m_ownVillage, this.m_selectedVillage, 4, number, this.currentCommand, data);
            AllVillagesPanel.travellersChanged();
            return true;
        }

        public void sendPeopleCallback(SendPeople_ReturnType returnData)
        {
            try
            {
                if (returnData.Success)
                {
                    GameEngine.Instance.World.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
                    GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
                    InterfaceMgr.Instance.getMainTabBar().changeTab(9);
                    InterfaceMgr.Instance.getMainTabBar().changeTab(0);
                    InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_selectedVillage, false, true, false, false);
                    InterfaceMgr.Instance.closeMonksPanel();
                }
                else
                {
                    CursorManager.SetCursor(CursorManager.CursorType.Default, base.ParentForm);
                    if (returnData.m_errorCode == ErrorCodes.ErrorCode.PEOPLE_INTERDICT_RANK_TOO_HIGH)
                    {
                        MyMessageBox.Show(SK.Text("SendMonksPanel_Rank_Too_High", "The Target Village Rank is too high."), SK.Text("GENERIC_Error", "Error"));
                    }
                    this.inLaunch = false;
                    this.closeButton.Enabled = true;
                    this.updatePointsCost();
                }
            }
            catch (Exception)
            {
            }
        }

        private void tracksMoved()
        {
            if (this.sliderEnabled)
            {
                this.numLabel.Text = (this.sliderImage.Value + 1).ToString();
            }
            else
            {
                this.numLabel.Text = "0";
            }
            this.updatePointsCost();
        }

        public void update()
        {
            this.cardbar.update();
            this.onVillageLoadUpdate(this.m_ownVillage, false);
            this.numLabel.Text = this.numLabel.Text;
            if (this.excommunicated)
            {
                DateTime time = VillageMap.getCurrentServerTime();
                TimeSpan span = (TimeSpan) (this.excommunicationTime - time);
                int totalSeconds = (int) span.TotalSeconds;
                if (totalSeconds < -5)
                {
                    this.excommunicated = false;
                    this.init(this.m_selectedVillage);
                }
                else
                {
                    if (totalSeconds < 0)
                    {
                        totalSeconds = 0;
                    }
                    this.tooltipLabel.Text = SK.Text("SendMonksPanel_You_Are_Excommunicated", "You are Excommunicated, you cannot issue any commands.") + " " + SK.Text("SendMonksPanel_Excommunication_Expires_in", "Excommunication Expires in") + " : " + VillageMap.createBuildTimeString(totalSeconds);
                }
            }
            double num2 = this.storedPreCardDistance * CardTypes.adjustMonkSpeed(GameEngine.Instance.World.UserCardData);
            if (((int) num2) != this.timeLabel.CustomTooltipData)
            {
                string str = VillageMap.createBuildTimeString((int) num2);
                this.timeLabel.Text = str;
                this.timeLabel.CustomTooltipData = (int) num2;
            }
        }

        public void updateButtons(int type)
        {
            this.currentCommand = type;
            this.actionButton1.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[0];
            this.actionButton1.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[7];
            this.actionButton2.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[1];
            this.actionButton2.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[8];
            this.actionButton3.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[2];
            this.actionButton3.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[9];
            this.actionButton4.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[3];
            this.actionButton4.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[10];
            this.actionButton5.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[4];
            this.actionButton5.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[11];
            this.actionButton6.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[5];
            this.actionButton6.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[12];
            this.actionButton7.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[6];
            this.actionButton7.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[13];
            bool visible = this.influenceIndent.Visible;
            this.influenceIndent.Visible = false;
            this.gfxImage.Visible = true;
            this.sliderImage.Visible = true;
            this.arrowImage.Visible = true;
            this.tooltipLabel.Visible = true;
            this.villageActionLabel.Visible = true;
            switch (type)
            {
                case 1:
                    this.actionButton4.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[0x11];
                    this.actionButton4.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[0x18];
                    this.villageActionLabel.Text = SK.Text("VillageMapPanel_Blessing", "Blessing") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                    break;

                case 2:
                case 8:
                {
                    this.actionButton1.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[14];
                    this.actionButton1.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[0x15];
                    if (((this.currentCommand != 2) && (this.currentCommand != 8)) || !visible)
                    {
                        this.influenceIndent.Visible = true;
                        this.gfxImage.Visible = false;
                        this.sliderImage.Visible = false;
                        this.arrowImage.Visible = false;
                        this.tooltipLabel.Visible = false;
                        this.villageActionLabel.Visible = false;
                    }
                    if (this.positiveInfluence)
                    {
                        this.villageActionLabel.Text = SK.Text("SendMonksPanel_Positive_Influence", "Positive Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                        this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_positive", "Select Player to Positively Influence");
                    }
                    else
                    {
                        this.villageActionLabel.Text = SK.Text("SendMonksPanel_Negative_Influencs", "Negative Influence") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                        this.influenceHeaderLabel.Text = SK.Text("SendMonksPanel_Select_negative", "Select Player to Negatively Influence");
                    }
                    int num = this.sliderImage.Value + 1;
                    if (this.maxMonks == 0)
                    {
                        num = 0;
                    }
                    int num3 = CardTypes.getInfluenceMultipier(GameEngine.Instance.World.UserCardData) * num;
                    this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + " (" + num3.ToString() + " ";
                    if (num3 != 1)
                    {
                        this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + SK.Text("SendMonksPanel_X_Votes", "votes");
                    }
                    else
                    {
                        this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + SK.Text("SendMonksPanel_X_Vote", "vote");
                    }
                    this.influenceHeaderLabel.Text = this.influenceHeaderLabel.Text + ")";
                    break;
                }
                case 3:
                    this.actionButton5.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[0x12];
                    this.actionButton5.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[0x19];
                    this.villageActionLabel.Text = SK.Text("VillageMapPanel_Inquisition", "Inquisition") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                    break;

                case 4:
                    this.actionButton2.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[15];
                    this.actionButton2.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[0x16];
                    this.villageActionLabel.Text = SK.Text("SendMonksPanel_Interdiction", "Interdiction") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                    break;

                case 5:
                    this.actionButton3.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[0x10];
                    this.actionButton3.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[0x17];
                    this.villageActionLabel.Text = SK.Text("SendMonksPanel_Restoration", "Restoration") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                    break;

                case 6:
                    this.actionButton6.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[0x13];
                    this.actionButton6.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[0x1a];
                    this.villageActionLabel.Text = SK.Text("SendMonksPanel_Absolution", "Absolution") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                    break;

                case 7:
                    this.actionButton7.ImageNorm = (Image) GFXLibrary.monk_screen_button_array_75x75[20];
                    this.actionButton7.ImageOver = (Image) GFXLibrary.monk_screen_button_array_75x75[0x1b];
                    this.villageActionLabel.Text = SK.Text("SendMonksPanel_Excommnunication", "Excommunication") + " : " + GameEngine.Instance.World.getVillageNameOrType(this.m_selectedVillage);
                    break;
            }
            this.updatePointsCost();
        }

        private void updatePointsCost()
        {
            int basePoints = 0;
            int num2 = this.sliderImage.Value + 1;
            if (this.maxMonks == 0)
            {
                num2 = 0;
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            switch (this.currentCommand)
            {
                case 1:
                    basePoints = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Blessings;
                    if (!this.excommunicated)
                    {
                        int index = GameEngine.Instance.World.UserResearchData.Research_Marriage;
                        if (index < 1)
                        {
                            index = 1;
                        }
                        double num4 = ResearchData.blessingTimes[index];
                        num4 *= CardTypes.getBlessingMultipier(GameEngine.Instance.World.UserCardData);
                        this.tooltipLabel.Text = SK.Text("SendMonksPanel_Increase_Popularity", "Increase Popularity within the Parish by :") + num2.ToString() + " (" + SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + num4.ToString("N", nFI) + " " + SK.Text("ResearchEffect_X_Hours", "hours") + ")";
                    }
                    break;

                case 2:
                case 8:
                    basePoints = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Influence;
                    if (GameEngine.Instance.World.isCountyCapital(this.m_selectedVillage))
                    {
                        basePoints *= 2;
                    }
                    if (!this.excommunicated)
                    {
                        int num12 = CardTypes.getInfluenceMultipier(GameEngine.Instance.World.UserCardData) * num2;
                        if (num12 != 1)
                        {
                            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Send_Influence", "Influence Election by :") + " " + num12.ToString() + " " + SK.Text("SendMonksPanel_X_Votes", "votes");
                        }
                        else
                        {
                            this.tooltipLabel.Text = SK.Text("SendMonksPanel_Send_Influence", "Influence Election by :") + " " + num12.ToString() + " " + SK.Text("SendMonksPanel_X_Vote", "vote");
                        }
                    }
                    break;

                case 3:
                    basePoints = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Inquisition;
                    if (!this.excommunicated)
                    {
                        int num5 = GameEngine.Instance.World.UserResearchData.Research_Confirmation;
                        if (num5 < 1)
                        {
                            num5 = 1;
                        }
                        double num6 = ResearchData.confirmationTimes[num5];
                        num6 *= CardTypes.getInquisitionMultipier(GameEngine.Instance.World.UserCardData);
                        this.tooltipLabel.Text = SK.Text("SendMonksPanel_Descrease_Popularity", "Decrease Popularity within the Parish by :") + num2.ToString() + " (" + SK.Text("TOOLTIP_CARD_DURATION", "Duration") + " : " + num6.ToString("N", nFI) + " " + SK.Text("ResearchEffect_X_Hours", "hours") + ")";
                    }
                    break;

                case 4:
                    basePoints = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Interdicts;
                    if (!this.excommunicated)
                    {
                        int currentLevel = num2 * 4;
                        currentLevel = CardTypes.adjustInterdictionLevel(GameEngine.Instance.World.UserCardData, currentLevel);
                        this.tooltipLabel.Text = SK.Text("SendMonksPanel_Protect", "Protect the Village from attack for :") + " " + currentLevel.ToString() + " " + SK.Text("ResearchEffect_X_Hours", "hours");
                    }
                    if (this.targetCapital)
                    {
                        basePoints *= 10;
                    }
                    else
                    {
                        basePoints = TradingCalcs.adjustInterdictionCostByTargetRank(basePoints, this.targetUserRank, GameEngine.Instance.World.SecondAgeWorld);
                    }
                    break;

                case 5:
                    basePoints = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Restoration;
                    if (!this.excommunicated)
                    {
                        int num14 = GameEngine.Instance.World.UserResearchData.Research_Baptism;
                        if (num14 < 1)
                        {
                            num14 = 1;
                        }
                        int num15 = num2 * ResearchData.baptismRestoreAmount[num14];
                        this.tooltipLabel.Text = SK.Text("SendMonksPanel_Remove_Disease", "Points of Disease healed :") + " " + CardTypes.adjustRestorationLevel(GameEngine.Instance.World.UserCardData, num15).ToString();
                    }
                    break;

                case 6:
                    basePoints = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Absolution;
                    if (!this.excommunicated)
                    {
                        int num7 = GameEngine.Instance.World.UserResearchData.Research_Confession;
                        if (num7 < 1)
                        {
                            num7 = 1;
                        }
                        double num8 = ResearchData.confessionTimes[num7] * num2;
                        num8 = CardTypes.adjustAbsolutionLevel(GameEngine.Instance.World.UserCardData, num8);
                        this.tooltipLabel.Text = SK.Text("SendMonksPanel_Reduce_Excommunication", "Reduce Excommunication Time in Village by :") + " " + num8.ToString("N", nFI) + " " + SK.Text("ResearchEffect_X_Hours", "hours");
                    }
                    break;

                case 7:
                    basePoints = GameEngine.Instance.LocalWorldData.MonkCommandPointsCost_Excommunication;
                    if (!this.excommunicated)
                    {
                        int num9 = GameEngine.Instance.World.UserResearchData.Research_ExtremeUnction;
                        if (num9 < 1)
                        {
                            num9 = 1;
                        }
                        double num10 = ResearchData.extremeUnctionTimes[num9] * num2;
                        num10 = CardTypes.adjustExcommunicationLevel(GameEngine.Instance.World.UserCardData, num10);
                        this.tooltipLabel.Text = SK.Text("SendMonksPanel_Remove_Powers", "Remove Church powers from the Village for :") + " " + num10.ToString("N", nFI) + " " + SK.Text("ResearchEffect_X_Hours", "hours");
                    }
                    break;
            }
            basePoints *= num2;
            this.currentPointsCost = basePoints;
            this.costValueLabel.Text = basePoints.ToString();
            if (basePoints <= GameEngine.Instance.World.getCurrentFaithPoints())
            {
                this.costValueLabel.Color = Color.FromArgb(0x12, 0xff, 0);
                if ((this.launchAllowed && (basePoints > 0)) && !this.excommunicated)
                {
                    this.launchButton.Enabled = true;
                }
                else
                {
                    this.launchButton.Enabled = false;
                }
            }
            else
            {
                this.costValueLabel.Color = Color.FromArgb(0xfc, 0, 12);
                this.launchButton.Enabled = false;
            }
        }

        public class MonkVoteLine : CustomSelfDrawPanel.CSDControl
        {
            private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
            private int m_factionID = -1;
            private SendMonkPanel m_parent;
            private int m_userID = -1;
            private bool m_votingAllowed;
            private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
            private CustomSelfDrawPanel.CSDButton radioButton = new CustomSelfDrawPanel.CSDButton();
            private CustomSelfDrawPanel.CSDLabel votesLabel = new CustomSelfDrawPanel.CSDLabel();

            public void init(string playerName, int userID, int rank, int points, bool votingAllowed, int numSpareVotes, int numReceivedVotes, int factionID, int votedUser, SendMonkPanel parent)
            {
                this.Size = new Size(0x181, 0x19);
                this.m_parent = parent;
                this.m_userID = userID;
                this.m_factionID = factionID;
                this.m_votingAllowed = votingAllowed;
                base.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
                if (votedUser != userID)
                {
                    this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[2];
                    this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[1];
                    this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[1];
                    this.radioButton.Active = true;
                }
                else
                {
                    this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[0];
                    this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[0];
                    this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[0];
                    this.radioButton.Active = false;
                }
                this.radioButton.Position = new Point(0, 2);
                this.radioButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
                base.addControl(this.radioButton);
                this.nameLabel.Text = "";
                this.nameLabel.Color = ARGBColors.White;
                this.nameLabel.Position = new Point(20, 0);
                this.nameLabel.Size = new Size(0xaf, 0x19);
                this.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.nameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
                base.addControl(this.nameLabel);
                this.factionLabel.Color = ARGBColors.White;
                this.factionLabel.Position = new Point(200, 0);
                this.factionLabel.Size = new Size(150, 0x19);
                this.factionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
                this.factionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
                base.addControl(this.factionLabel);
                this.votesLabel.Color = ARGBColors.White;
                this.votesLabel.Position = new Point(350, 0);
                this.votesLabel.Size = new Size(0x23, 0x19);
                this.votesLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                this.votesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.votesLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.radioClicked));
                base.addControl(this.votesLabel);
                this.nameLabel.Text = playerName;
                NumberFormatInfo nFI = GameEngine.NFI;
                this.votesLabel.Text = numReceivedVotes.ToString("N", nFI);
                if (factionID >= 0)
                {
                    FactionData data = GameEngine.Instance.World.getFaction(factionID);
                    if (data != null)
                    {
                        this.factionLabel.Text = data.factionNameAbrv;
                    }
                    else
                    {
                        this.factionLabel.Text = "";
                    }
                }
                else
                {
                    this.factionLabel.Text = "";
                }
                base.invalidate();
            }

            public void lineClicked()
            {
            }

            public void radioClicked()
            {
                if (this.radioButton.Active && (this.m_parent != null))
                {
                    GameEngine.Instance.playInterfaceSound("SendMonkPanel_select_village");
                    this.m_parent.radioClicked(this.m_userID);
                }
            }

            public void setState(int selectedUserID)
            {
                if (selectedUserID != this.m_userID)
                {
                    this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[2];
                    this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[1];
                    this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[1];
                    this.radioButton.Active = true;
                }
                else
                {
                    this.radioButton.ImageNorm = (Image) GFXLibrary.radio_green[0];
                    this.radioButton.ImageOver = (Image) GFXLibrary.radio_green[0];
                    this.radioButton.ImageClick = (Image) GFXLibrary.radio_green[0];
                    this.radioButton.Active = false;
                }
            }

            public void update()
            {
            }
        }

        public class ParishMemberComparer : IComparer<ParishMember>
        {
            public int Compare(ParishMember x, ParishMember y)
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
                if (x.numVotesReceived < y.numVotesReceived)
                {
                    return 1;
                }
                if (x.numVotesReceived > y.numVotesReceived)
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}

